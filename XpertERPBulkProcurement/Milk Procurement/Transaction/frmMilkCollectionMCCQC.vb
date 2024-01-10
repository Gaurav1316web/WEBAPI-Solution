Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class frmMilkCollectionMCCQC
    Inherits FrmMainTranScreen
    Dim CorrectionApply As Boolean = False
    Dim OneTimeCheck As Boolean = False
    Private Sub FrmPrefixImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtDateReport.Value = txtDate.Value
        RadPageView1.SelectedPage = RadPageViewPage1
        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.settAutoAttachment = True
        UcAttachment1.isDeleteTheAttachment = False
    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            CorrectionApply = False
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            Dim FileName As String = ""
            Dim SafeFileName As String = ""
            If transportSql.importExcel(FileName, SafeFileName, gv1, "DATE", "S NO", "NO.", " BMC Name", "R.NO.", "DCS", "FAT", "SNF", "CLR", "ACIDITY", "REMARKS") Then
                Dim repoError As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoError.FormatString = ""
                repoError.HeaderText = "Error"
                repoError.Name = "Error"
                repoError.ReadOnly = True
                gv1.MasterTemplate.Columns.Add(repoError)

                Dim repoIsOK As GridViewDecimalColumn = New GridViewDecimalColumn()
                repoIsOK.FormatString = ""
                repoIsOK.DecimalPlaces = 0
                repoIsOK.HeaderText = "IsOK"
                repoIsOK.Name = "IsOK"
                repoIsOK.Minimum = 0
                repoIsOK.Maximum = 2
                repoIsOK.ReadOnly = True
                repoIsOK.IsVisible = False
                gv1.MasterTemplate.Columns.Add(repoIsOK)

                repoIsOK = New GridViewDecimalColumn()
                repoIsOK.FormatString = ""
                repoIsOK.DecimalPlaces = 0
                repoIsOK.HeaderText = "PKID"
                repoIsOK.Name = "PKID"
                repoIsOK.ReadOnly = True
                repoIsOK.IsVisible = False
                gv1.MasterTemplate.Columns.Add(repoIsOK)

                repoIsOK = New GridViewDecimalColumn()
                repoIsOK.FormatString = ""
                repoIsOK.DecimalPlaces = 0
                repoIsOK.HeaderText = "Qty"
                repoIsOK.Name = "Qty"
                repoIsOK.ReadOnly = True
                repoIsOK.IsVisible = False
                gv1.MasterTemplate.Columns.Add(repoIsOK)

                repoIsOK = New GridViewDecimalColumn()
                repoIsOK.FormatString = ""
                repoIsOK.DecimalPlaces = 0
                repoIsOK.HeaderText = "Gaze Qty"
                repoIsOK.Name = "Gaze_Qty"
                repoIsOK.ReadOnly = True
                repoIsOK.IsVisible = False
                gv1.MasterTemplate.Columns.Add(repoIsOK)

                gv1.BestFitColumns()
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                gv1.Columns("FAT").ReadOnly = False
                gv1.Columns("SNF").ReadOnly = False

                gv1.AllowAddNewRow = False
                gv1.ShowGroupPanel = False
                gv1.ShowFilteringRow = True
                gv1.EnableFiltering = True
                gv1.ShowGroupPanel = False
                gv1.AllowColumnReorder = False
                gv1.AllowRowReorder = False
                gv1.EnableSorting = False
                gv1.MasterTemplate.ShowRowHeaderColumn = False
                repoError.Width = 500

                UcAttachment1.BlankAllControls()
                UcAttachment1.LoadData(clsCommon.GetPrintDate(txtDate.Value, "yyyy/MM/dd"))
                UcAttachment1.AddAttachment(FileName, SafeFileName)


            Else
                gv1.Columns.Clear()
            End If
        Catch ex As Exception

            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnVerify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerify.Click
        Try
            AllowToSave(True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function GetQuery(ByVal TranDate As DateTime, ByVal PendingStatus As Integer) As String
        Dim qry As String = "Select tspl_Milk_collection_MCC.Document_No,tspl_Milk_collection_MCC_Detail.PK_Id,TSPL_MILK_COLLECTION_MCC.Route_Code,tspl_Milk_collection_MCC_Detail.Sample_No,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,tspl_Milk_collection_MCC_Detail.Gaze_Qty,tspl_Milk_collection_MCC_Detail.Qty,tspl_Milk_collection_MCC_Detail.FAT,tspl_Milk_collection_MCC_Detail.FATKG,tspl_Milk_collection_MCC_Detail.SNF,tspl_Milk_collection_MCC_Detail.SNFKG 
From tspl_Milk_collection_MCC_Detail 
Left outer join tspl_Milk_collection_MCC on tspl_Milk_collection_MCC.Document_No=tspl_Milk_collection_MCC_Detail.Document_No
Left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=tspl_Milk_collection_MCC_Detail.MCC_Code
where Convert(Date, tspl_Milk_collection_MCC.Document_Date,103) ='" + clsCommon.GetPrintDate(TranDate, "dd/MMM/yyyy") + "' and Status=0"
        If PendingStatus = 1 Then ''ALL
            qry += " and 2=2 "
        ElseIf PendingStatus = 2 Then ''PEnding
            qry += " and (isnull(TSPL_MILK_COLLECTION_MCC_DETAIL.FAT,0)=0 or isnull(TSPL_MILK_COLLECTION_MCC_DETAIL.SNF,0)=0 )"
        ElseIf PendingStatus = 3 Then ''QC Done
            qry += " and (isnull(TSPL_MILK_COLLECTION_MCC_DETAIL.FAT,0)<>0 and isnull(TSPL_MILK_COLLECTION_MCC_DETAIL.SNF,0)<>0 )"
        End If
        Return qry
    End Function
    Function AllowToSave(ByVal isStartProgressBar As Boolean) As Boolean
        Try
            CorrectionApply = False
            If isStartProgressBar Then
                clsCommon.ProgressBarPercentShow()
            End If
            Dim strRouteNo As String = ""
            Dim dt As DataTable = Nothing
            For ii As Integer = 0 To gv1.RowCount - 1
                clsCommon.ProgressBarPercentUpdate(ii * 100 / gv1.RowCount - 1, "Verifing " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(gv1.RowCount - 1))
                If ii = 0 Then
                    Try
                        Dim strDate As String = clsCommon.myCstr(gv1.Rows(ii).Cells("DATE").Value).Replace("00:00:00", "").Replace(" ", "")

                        Dim strSep As String = ""
                        If strDate.Contains("/") Then
                            strSep = "/"
                        ElseIf strDate.Contains("\") Then
                            strSep = "\"
                        ElseIf strDate.Contains(".") Then
                            strSep = "."
                        ElseIf strDate.Contains("-") Then
                            strSep = "-"
                        End If
                        Dim strBreak As String() = clsCommon.myCstr(strDate).Split(New String() {strSep}, StringSplitOptions.None)
                        txtDate.Value = New Date(strBreak(2), strBreak(1), strBreak(0))
                    Catch ex As Exception
                        Throw New Exception("Invalid Date " + clsCommon.myCstr(gv1.Rows(ii).Cells("DATE").Value))
                    End Try
                    dt = clsDBFuncationality.GetDataTable(GetQuery(txtDate.Value, 1))
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("No Pending Data found of [" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "]")
                    End If
                End If
                gv1.CurrentRow = gv1.Rows(ii)
                gv1.Rows(ii).Cells("PKID").Value = 0
                gv1.Rows(ii).Cells("Error").Value = ""
                gv1.Rows(ii).Cells("IsOK").Value = 0

                Dim strRouteNoTemp As String = clsCommon.myCstr(gv1.Rows(ii).Cells("R.NO.").Value)
                If clsCommon.myLen(strRouteNoTemp) > 0 Then
                    strRouteNo = strRouteNoTemp
                End If
                Dim strMCC As String = clsCommon.myCstr(gv1.Rows(ii).Cells("DCS").Value)
                Dim strSampleNo As String = ""
                If strMCC.Contains("-") Then
                    Dim strBreak As String() = clsCommon.myCstr(strMCC).Split(New String() {"-"}, StringSplitOptions.None)
                    If strBreak.Length > 1 Then
                        strMCC = strBreak(0)
                        strSampleNo = strBreak(1)
                    End If
                End If
                Dim qry As String = "Route_Code = '" + strRouteNo + "' and Mcc_Code_VLC_Uploader='" + strMCC + "'"
                If clsCommon.myLen(strSampleNo) > 0 Then
                    qry += " and Sample_No='" + strSampleNo + "'"
                End If
                Try
                    Dim dtTemp As DataTable = dt.Select(qry).CopyToDataTable()
                    If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                        gv1.Rows(ii).Cells("Error").Value += "Weight Not found."
                        gv1.Rows(ii).Cells("IsOK").Value = 2
                        Continue For
                    End If
                    If dtTemp.Rows.Count > 1 Then
                        gv1.Rows(ii).Cells("Error").Value += "DCS Have more than one weight entry."
                        gv1.Rows(ii).Cells("IsOK").Value = 2
                        Continue For
                    End If
                    gv1.Rows(ii).Cells("PKID").Value = clsCommon.myCDecimal(dtTemp.Rows(0)("PK_Id"))
                    gv1.Rows(ii).Cells("Qty").Value = clsCommon.myCDecimal(dtTemp.Rows(0)("Qty"))
                    gv1.Rows(ii).Cells("Gaze_Qty").Value = clsCommon.myCDecimal(dtTemp.Rows(0)("Gaze_Qty"))
                    gv1.Rows(ii).Cells("IsOK").Value = 1
                Catch ex As Exception
                    gv1.Rows(ii).Cells("Error").Value = "Missing Sample"
                    gv1.Rows(ii).Cells("IsOK").Value = 2
                End Try
            Next
            If isStartProgressBar Then
                clsCommon.ProgressBarPercentHide()
            End If
            UcAttachment1.AllowToSave()
        Catch ex As Exception
            If isStartProgressBar Then
                clsCommon.ProgressBarPercentHide()
            End If
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            clsCommon.ProgressBarPercentShow()
            If AllowToSave(False) Then
                If gv1.Rows.Count > 0 Then
                    Dim dictionary As New List(Of clsMilkCollectionMCCDetail)
                    For ii As Integer = 0 To gv1.RowCount - 1
                        clsCommon.ProgressBarPercentUpdate(ii * 50 / gv1.RowCount - 1, "Fetching " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(gv1.RowCount - 1))
                        If clsCommon.myCdbl(gv1.Rows(ii).Cells("IsOK").Value) = 1 Then
                            Dim obj As New clsMilkCollectionMCCDetail()
                            obj.PK_Id = clsCommon.myCDecimal(gv1.Rows(ii).Cells("PKID").Value)
                            If clsCommon.myCDecimal(gv1.Rows(ii).Cells("Gaze_Qty").Value) > 0 Then
                                obj.Qty = (clsCommon.myCDecimal(gv1.Rows(ii).Cells("Gaze_Qty").Value) * (1.0 + ((clsCommon.myCDecimal(gv1.Rows(ii).Cells("CLR").Value)) / 1000)))
                                obj.Qty = Math.Round(obj.Qty, 0, MidpointRounding.AwayFromZero)
                            Else
                                obj.Qty = clsCommon.myCDecimal(gv1.Rows(ii).Cells("Qty").Value)
                            End If
                            obj.FAT = clsCommon.myCDecimal(gv1.Rows(ii).Cells("FAT").Value)
                            obj.SNF = clsCommon.myCDecimal(gv1.Rows(ii).Cells("SNF").Value)
                            obj.FATKG = Math.Round(obj.Qty * obj.FAT / 100, 3, MidpointRounding.ToEven)
                            obj.SNFKG = Math.Round(obj.Qty * obj.SNF / 100, 3, MidpointRounding.ToEven)
                            dictionary.Add(obj)
                        End If
                    Next
                    clsCommon.ProgressBarPercentHide()
                    If clsCommon.MyMessageBoxShow(Me, "Updating [" + clsCommon.myCstr(dictionary.Count) + "] Samples" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        clsCommon.ProgressBarPercentShow()
                        ''Saveing Data
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        Try
                            For ii As Integer = 0 To dictionary.Count - 1
                                clsCommon.ProgressBarPercentUpdate(ii * 100 / dictionary.Count - 1, "Saving " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(dictionary.Count - 1))

                                Dim coll As New Hashtable()
                                clsCommon.AddColumnsForChange(coll, "Qty", dictionary(ii).Qty)
                                clsCommon.AddColumnsForChange(coll, "FAT", dictionary(ii).FAT)
                                clsCommon.AddColumnsForChange(coll, "SNF", dictionary(ii).SNF)
                                clsCommon.AddColumnsForChange(coll, "FATKG", dictionary(ii).FATKG)
                                clsCommon.AddColumnsForChange(coll, "SNFKG", dictionary(ii).SNFKG)
                                Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Machine_Fat,Machine_SNF from TSPL_MILK_COLLECTION_MCC_DETAIL where PK_Id='" + clsCommon.myCstr(dictionary(ii).PK_Id) + "' and Machine_FAT is null", trans)
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    clsCommon.AddColumnsForChange(coll, "Machine_FAT", dictionary(ii).FAT)
                                    clsCommon.AddColumnsForChange(coll, "Machine_SNF", dictionary(ii).SNF)
                                End If
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_MCC_DETAIL", OMInsertOrUpdate.Update, "PK_Id='" + clsCommon.myCstr(dictionary(ii).PK_Id) + "' ", trans)
                            Next
                            UcAttachment1.SaveData(clsCommon.GetPrintDate(txtDate.Value, "yyyy/MM/dd"), False, trans)
                            trans.Commit()
                        Catch ex As Exception
                            trans.Rollback()
                            Throw New Exception(ex.Message)
                        End Try
                    End If
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow(Me, "QC Updated successfully", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_RowFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gv1.RowFormatting
        Try
            If clsCommon.myCdbl(e.RowElement.RowInfo.Cells("IsOK").Value) = 1 Then
                e.RowElement.DrawFill = True
                e.RowElement.GradientStyle = GradientStyles.Solid
                e.RowElement.ForeColor = Color.Black
                e.RowElement.BackColor = Color.LightGreen
            ElseIf clsCommon.myCdbl(e.RowElement.RowInfo.Cells("IsOK").Value) = 2 Then
                e.RowElement.DrawFill = True
                e.RowElement.GradientStyle = GradientStyles.Solid
                e.RowElement.ForeColor = Color.Black
                e.RowElement.BackColor = Color.MistyRose
            Else
                e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
                e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local)
                e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local)
                e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadButton3_Click_1(sender As Object, e As EventArgs) Handles RadButton3.Click
        Try

            gv2.DataSource = Nothing
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(GetQuery(txtDateReport.Value, IIf(rbtnPending.IsChecked, 2, IIf(rbtnAllQCDone.IsChecked, 3, 1))))
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found")
            End If
            UcAttachment1.BlankAllControls()
            UcAttachment1.LoadData(clsCommon.GetPrintDate(txtDateReport.Value, "yyyy/MM/dd"))
            gv2.DataSource = dt
            gv2.GroupDescriptors.Clear()
            gv2.ShowGroupPanel = False
            gv2.AllowAddNewRow = False
            gv2.EnableFiltering = True
            gv2.ShowFilteringRow = True
            gv2.MasterTemplate.SummaryRowsBottom.Clear()
            For ii As Integer = 0 To gv2.Columns.Count - 1
                gv2.Columns(ii).ReadOnly = True
                gv2.Columns(ii).IsVisible = True
            Next
            gv2.BestFitColumns()
            gv2.Columns("Document_No").HeaderText = "Document"
            gv2.Columns("PK_Id").HeaderText = "PKID"
            gv2.Columns("PK_Id").IsVisible = False
            gv2.Columns("Route_Code").HeaderText = "Route No"
            gv2.Columns("Sample_No").HeaderText = "Sample No"
            gv2.Columns("Mcc_Code_VLC_Uploader").HeaderText = "BMC"
            gv2.Columns("Gaze_Qty").HeaderText = "Gaze Qty"
            gv2.Columns("Qty").HeaderText = "Qty"
            gv2.Columns("FAT").HeaderText = "FAT"
            gv2.Columns("FATKG").HeaderText = "FAT Kg"
            gv2.Columns("SNF").HeaderText = "SNF"
            gv2.Columns("SNFKG").HeaderText = "SNF Kg"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnCorrection_Click(sender As Object, e As EventArgs) Handles btnCorrection.Click
        Try
            If OneTimeCheck = False Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.SIRC
                frm.strCode = clsFixedParameterCode.UpdatePassword
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    CorrectionApply = True
                    OneTimeCheck = True
                End If
            Else
                CorrectionApply = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column Is gv1.Columns("FAT") Then
                gv1.CurrentRow.Cells("FAT").ReadOnly = Not CorrectionApply
            ElseIf e.Column Is gv1.Columns("SNF") Then
                gv1.CurrentRow.Cells("SNF").ReadOnly = Not CorrectionApply
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        If gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub
    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Milk BMC QC ", gv1, arrHeader, "Milk BMC QC", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class

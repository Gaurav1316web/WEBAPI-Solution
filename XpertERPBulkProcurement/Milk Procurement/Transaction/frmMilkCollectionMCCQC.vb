Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class frmMilkCollectionMCCQC
    Inherits FrmMainTranScreen
    Dim CorrectionApply As Boolean = False
    Dim OneTimeCheck As Boolean = False
    Const colCheck As String = "colCheck"

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
            If transportSql.importExcel(FileName, SafeFileName, gv1, "DATE", "S NO", "NO.", " BMC Name", "R.NO.", "DCS", "FAT", "SNF", "CLR", "ACIDITY", "REMARKS", "Retesting(Y/N)") Then
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
    '    Function GetQuery(ByVal TranDate As DateTime, ByVal PendingStatus As Integer) As String
    '        Dim qry As String = "Select tspl_Milk_collection_MCC.Document_No,tspl_Milk_collection_MCC_Detail.PK_Id,TSPL_MILK_COLLECTION_MCC.Route_Code,tspl_Milk_collection_MCC_Detail.Sample_No,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,tspl_Milk_collection_MCC_Detail.Gaze_Qty,tspl_Milk_collection_MCC_Detail.Qty,tspl_Milk_collection_MCC_Detail.FAT,tspl_Milk_collection_MCC_Detail.FATKG,tspl_Milk_collection_MCC_Detail.SNF,tspl_Milk_collection_MCC_Detail.SNFKG 
    'From tspl_Milk_collection_MCC_Detail 
    'Left outer join tspl_Milk_collection_MCC on tspl_Milk_collection_MCC.Document_No=tspl_Milk_collection_MCC_Detail.Document_No
    'Left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=tspl_Milk_collection_MCC_Detail.MCC_Code
    'where Convert(Date, tspl_Milk_collection_MCC.Document_Date,103) ='" + clsCommon.GetPrintDate(TranDate, "dd/MMM/yyyy") + "' and Status=0"
    '        If PendingStatus = 1 Then ''ALL
    '            qry += " and 2=2 "
    '        ElseIf PendingStatus = 2 Then ''PEnding
    '            qry += " and (isnull(TSPL_MILK_COLLECTION_MCC_DETAIL.FAT,0)=0 or isnull(TSPL_MILK_COLLECTION_MCC_DETAIL.SNF,0)=0 )"
    '        ElseIf PendingStatus = 3 Then ''QC Done
    '            qry += " and (isnull(TSPL_MILK_COLLECTION_MCC_DETAIL.FAT,0)<>0 and isnull(TSPL_MILK_COLLECTION_MCC_DETAIL.SNF,0)<>0 )"
    '        End If
    '        Return qry
    '    End Function
    Function AllowToSave(ByVal isStartProgressBar As Boolean) As Boolean
        Try
            CorrectionApply = False
            If isStartProgressBar Then
                clsCommon.ProgressBarPercentShow()
            End If
            Dim strRouteNo As String = ""
            Dim dt As DataTable = Nothing
            For ii As Integer = 0 To gv1.RowCount - 1
                If ii = 296 Then
                    Dim x As Integer = 0
                End If
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
                    dt = clsDBFuncationality.GetDataTable(clsMilkCollectionMCC.GetQuery(txtDate.Value, 1, False))
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
                Dim qry1 As String = "Route_Code = '" + strRouteNo + "' and Mcc_Code_VLC_Uploader='" + strMCC + "' "

                Dim qry2 As String = Nothing
                If clsCommon.myLen(strSampleNo) > 0 Then
                    qry2 = " Sample_No='" + strSampleNo + "'"
                End If
                Try
                    Dim dtTemp As DataTable = Nothing
                    ' First filtering step with qry1
                    Dim initialFilteredRows = dt.Select(qry1)

                    ' Check if there are matching rows for qry1
                    If initialFilteredRows.Any() Then
                        dtTemp = initialFilteredRows.CopyToDataTable()
                    Else
                        gv1.Rows(ii).Cells("Error").Value += $"BMC ({strMCC}) Not Found In Route No ({strRouteNo})."
                        gv1.Rows(ii).Cells("IsOK").Value = 2
                        dtTemp = Nothing
                        Continue For
                    End If

                    Dim strRetesting As String = clsCommon.myCstr(gv1.Rows(ii).Cells("Retesting(Y/N)").Value)
                    If clsCommon.myLen(strRetesting) > 0 Then
                        If Not (clsCommon.CompairString(strRetesting, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(strRetesting, "N") = CompairStringResult.Equal) Then
                            gv1.Rows(ii).Cells("Error").Value += "Required Retesting should have value Y/N"
                            gv1.Rows(ii).Cells("IsOK").Value = 2
                        End If
                    End If

                    ' Second filtering step with qry2 on dtTemp
                    Dim secondFilteredRows = dtTemp.Select(qry2)

                    ' Check if there are matching rows for qry2
                    If secondFilteredRows.Any() Then
                        dtTemp = secondFilteredRows.CopyToDataTable()
                    Else
                        gv1.Rows(ii).Cells("Error").Value += $"Sample No ({strSampleNo}) Of BMC ({strMCC}) Not Found On Route No ({strRouteNo})."
                        gv1.Rows(ii).Cells("IsOK").Value = 2
                        dtTemp = Nothing
                        Continue For
                    End If

                    ' Check if there is more than one row in dtTemp after filtering with qry2
                    If dtTemp.Rows.Count > 1 Then
                        gv1.Rows(ii).Cells("Error").Value += "BMC have more than one entry or missing sample."
                        gv1.Rows(ii).Cells("IsOK").Value = 2
                        dtTemp = Nothing
                        Continue For
                    End If


                    gv1.Rows(ii).Cells("PKID").Value = clsCommon.myCDecimal(dtTemp.Rows(0)("PK_Id"))
                    gv1.Rows(ii).Cells("Qty").Value = clsCommon.myCDecimal(dtTemp.Rows(0)("Qty"))
                    gv1.Rows(ii).Cells("Gaze_Qty").Value = clsCommon.myCDecimal(dtTemp.Rows(0)("Gaze_Qty"))
                    gv1.Rows(ii).Cells("IsOK").Value = 1
                Catch ex As Exception
                    Dim errMsg As New ArrayList()
                    Dim errqry As String = "Route_Code = '" + strRouteNo + "' "
                    Try
                        Dim dtTemp As DataTable = dt.Select(errqry).CopyToDataTable()
                    Catch
                        errMsg.Add("Route [ Correct Route : " + clsCommon.myCstr(dt.Rows(ii)("Route_Code")) + "]")
                    End Try

                    errqry = "  Mcc_Code_VLC_Uploader='" + strMCC + "'"
                    Try
                        Dim dtTemp As DataTable = dt.Select(errqry).CopyToDataTable()
                    Catch
                        errMsg.Add("DCS")
                    End Try

                    errqry = " Route_Code = '" + strRouteNo + "' and  Mcc_Code_VLC_Uploader='" + strMCC + "' and Sample_No='" + strSampleNo + "'"
                    Try
                        Dim dtTemp As DataTable = dt.Select(errqry).CopyToDataTable()
                    Catch
                        errMsg.Add("Sample " + strSampleNo)
                    End Try

                    gv1.Rows(ii).Cells("Error").Value = "Mismatch " + clsCommon.GetMulcallString(errMsg).Replace("'", "")
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
                            obj.Required_Retesting = (clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells("Retesting(Y/N)").Value), "Y") = CompairStringResult.Equal)
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
                            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                                obj.Remark = clsCommon.myCstr(gv1.Rows(ii).Cells("RemarkS").Value)
                            End If
                            dictionary.Add(obj)
                            End If
                    Next
                    clsCommon.ProgressBarPercentHide()
                    If clsCommon.MyMessageBoxShow(Me, "Updating [" + clsCommon.myCstr(dictionary.Count) + "] Samples" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        clsCommon.ProgressBarPercentShow()
                        ''Saveing Data
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        Dim ArrPKID As New List(Of String)

                        Try
                            For ii As Integer = 0 To dictionary.Count - 1
                                clsCommon.ProgressBarPercentUpdate(ii * 100 / dictionary.Count - 1, "Saving " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(dictionary.Count - 1))

                                Dim coll As New Hashtable()
                                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                                    clsCommon.AddColumnsForChange(coll, "Remark", dictionary(ii).Remark)
                                End If
                                clsCommon.AddColumnsForChange(coll, "Required_Retesting", IIf(dictionary(ii).Required_Retesting, 1, 0), True)
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
                                clsCommon.AddColumnsForChange(coll, "Original_Qty", dictionary(ii).Qty)
                                clsCommon.AddColumnsForChange(coll, "Original_FATKg", dictionary(ii).FATKG)
                                clsCommon.AddColumnsForChange(coll, "Original_SNFKg", dictionary(ii).SNFKG)
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_MCC_DETAIL", OMInsertOrUpdate.Update, "PK_Id='" + clsCommon.myCstr(dictionary(ii).PK_Id) + "' ", trans)
                                ArrPKID.Add(clsCommon.myCstr(dictionary(ii).PK_Id))

                                'SendSMSandEmail(ii, trans)
                            Next
                            UcAttachment1.SaveData(clsCommon.GetPrintDate(txtDate.Value, "yyyy/MM/dd"), False, trans)

                            If ArrPKID IsNot Nothing AndAlso ArrPKID.Count > 0 Then
                                Dim qry As String = "select distinct Document_No from TSPL_MILK_COLLECTION_MCC_DETAIL where PK_Id in (" + clsCommon.GetMulcallString(ArrPKID) + ")"
                                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                For Each dr As DataRow In dt.Rows
                                    clsMilkCollectionMCC.HistoryUpdate(clsCommon.myCstr(dr("Document_No")), trans)
                                Next
                            End If

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
    Sub FormatGridGv2()
        Dim flag As Boolean = True
        For ii As Integer = 0 To gv2.Columns.Count - 1
            If clsCommon.CompairString(gv2.Columns(ii).Name, colCheck) = CompairStringResult.Equal Then
                flag = False
                Exit For
            End If
        Next
        If flag Then
            Dim repocolCheckBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            repocolCheckBox.FormatString = ""
            repocolCheckBox.HeaderText = "Check"
            repocolCheckBox.Name = colCheck
            repocolCheckBox.Width = 100
            repocolCheckBox.ReadOnly = False
            gv2.MasterTemplate.Columns.Add(repocolCheckBox)

            gv2.AllowAddNewRow = False
        End If
        gv2.Columns(colCheck).ReadOnly = Not chkSms.Checked
    End Sub


    Private Sub RadButton3_Click_1(sender As Object, e As EventArgs) Handles RadButton3.Click
        Try

            gv2.DataSource = Nothing
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(clsMilkCollectionMCC.GetQuery(txtDateReport.Value, IIf(rbtnPending.IsChecked, 2, IIf(rbtnAllQCDone.IsChecked, 3, 1)), False))
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
            'If chkSms.Checked Then
            '    FormatGridGv2()
            'End If
            FormatGridGv2()
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
            gv2.Columns("Status").HeaderText = "Status"
            gv2.Columns("Milk_Type").HeaderText = "Milk Type"
            'If chkSms.Checked Then
            '    FormatGridGv2()
            'End If
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
        Dim frm1 As New frmMyDateTimePicker2
        frm1.Text = "Select Date"
        frm1.RetValue = Me.txtDate.Value
        frm1.ShowDialog()
        txtDate.Value = frm1.RetValue
        If frm1.RetValue IsNot Nothing Then
            'Dim qry As String = "Select '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "' as [DATE], '1'as [S NO], '1' as [NO.], 'gadota' as [BMC Name]," &
            '   " '1000' as [R.NO.], '1-1' as [DCS], '7' as [FAT], '9' as [SNF], '27' as [CLR],'0.125' as [ACIDITY],'' as [REMARKS]"
            Dim qry As String = "Select convert(varchar, Document_Date,103) as [DATE], ROW_NUMBER() OVER (ORDER BY Document_Date) as [S NO],
 TSPL_MILK_COLLECTION_MCC_DETAIL.SNo  AS  [NO.],TSPL_MCC_MASTER.MCC_NAME as [BMC Name],TSPL_MILK_COLLECTION_MCC.Route_Code as  [R.NO.],TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader+'-'+cast(TSPL_MILK_COLLECTION_MCC_DETAIL.Sample_No as varchar) as [DCS],cast(TSPL_MILK_COLLECTION_MCC_DETAIL.FAT as varchar) as FAT,cast(TSPL_MILK_COLLECTION_MCC_DETAIL.SNF as varchar) as SNF,'' as  [CLR],'' as [ACIDITY],TSPL_MILK_COLLECTION_MCC_DETAIL.Remark as [REMARKS],'' [Retesting(Y/N)]
 from  TSPL_MILK_COLLECTION_MCC_DETAIL
 left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code
 left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No  
where  convert(date, Document_Date,103)='" + clsCommon.GetPrintDate(frm1.RetValue, "dd/MMM/yyyy") + "' and  isnull(TSPL_MILK_COLLECTION_MCC.Status,0)=0   and isnull(TSPL_MILK_COLLECTION_MCC_DETAIL.Milk_Not_Picked,0)=0"
            transportSql.OpenExporttoExcel(qry, Me)
        End If

    End Sub


    Private Sub SendSMSandEmail(ByVal RoWNum As Integer, ByVal trans As SqlTransaction)
        Try
            Dim strPhoneno As String = Nothing
            Dim dtContent As DataTable = Nothing
            If clsCommon.CompairString(Form_ID, clsUserMgtCode.MilkCollectionMCCSample) = CompairStringResult.Equal Then
                dtContent = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.MilkCollectionMCCSample + "'", trans)
            End If

            Dim DCSCode() As String
            DCSCode = (clsCommon.myCstr(gv1.Rows(RoWNum).Cells("DCS").Value).Split("-"))

            Dim Qry As String = "Select (Case When IsNull(TSPL_VENDOR_MASTER.Phone1,'')='' Then TSPL_VENDOR_MASTER.Phone2 Else TSPL_VENDOR_MASTER.Phone1 End) As DCS_Contact, 
                                TSPL_VENDOR_MASTER.Vendor_Name from TSPL_VENDOR_MASTER
                                Left Outer join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code
                                where TSPL_VENDOR_MASTER.Form_Type='VSP' And (Case When IsNull(TSPL_VENDOR_MASTER.Phone1,'')='' Then TSPL_VENDOR_MASTER.Phone2 Else TSPL_VENDOR_MASTER.Phone1 End) Not In ('Null','','(+__)__________') And TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader='" + clsCommon.myCstr(DCSCode(0)) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            strPhoneno = clsCommon.myCstr(dt.Rows(0)("DCS_Contact"))


            Dim objEmailH As New clsEMailHead()
            objEmailH.arrEMail = New List(Of String)()
            Dim objSMSH As New clsSMSHead()
            objSMSH.arrMobilNo = New List(Of String)()
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then
                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                    objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCUploaderCode, clsCommon.myCstr(DCSCode(0)))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCName, clsCommon.myCstr(dt.Rows(0)("Vendor_Name")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderFat, clsCommon.myCstr(gv1.Rows(RoWNum).Cells("FAT").Value))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderSNF, clsCommon.myCstr(gv1.Rows(RoWNum).Cells("SNF").Value))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Form_Code, MyBase.Form_ID)

                    If clsCommon.myLen(strPhoneno) > 0 Then
                        strPhoneno = strPhoneno.Replace("(", "").Replace(")", "").Replace("+", "").Replace("_____", "").Replace("____", "").Replace("___", "").Replace("__", "").Replace("_", "")
                    End If
                    If clsCommon.myLen(strPhoneno) >= 10 Then
                        objSMSH.arrMobilNo.Add(clsCommon.myCstr(strPhoneno))
                    End If

                    If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                        objSMSH.SaveData(clsUserMgtCode.MilkCollectionMCCSample, objSMSH, trans)
                    End If
                End If
            End If

            objSMSH = Nothing
            'If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
            '    clsCommon.MyMessageBoxShow(Me,"SMS Send Successfully", Me.Text)
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub VisibleSendButton()
        If rbtnPending.IsChecked Then
            chkSms.Visible = True
            chkSms.Enabled = True
        Else
            chkSms.Visible = False
            chkSms.Enabled = False
            chkSms.IsChecked = False
        End If
    End Sub
    Private Sub rbtnPending_CheckStateChanged(sender As Object, e As EventArgs) Handles rbtnPending.CheckStateChanged
        VisibleSendButton()
    End Sub

    Private Sub rbtnAllQCDone_CheckStateChanged(sender As Object, e As EventArgs) Handles rbtnAllQCDone.CheckStateChanged
        VisibleSendButton()
        VisibleSms()
    End Sub

    Private Sub rbtnAll_CheckStateChanged(sender As Object, e As EventArgs) Handles rbtnAll.CheckStateChanged
        VisibleSendButton()
        VisibleSms()
    End Sub

    Private Sub chkSms_CheckStateChanged(sender As Object, e As EventArgs) Handles chkSms.CheckStateChanged
        VisibleSms()
    End Sub
    Sub VisibleSms()
        If chkSms.IsChecked Then
            GroupBox2.Visible = True
        Else
            GroupBox2.Visible = False
        End If
    End Sub

    ' Private Sub btnSendSms_Click(ByVal RoWNum As Integer, ByVal trans As SqlTransaction)
    Private Sub btnSendSms_Click(sender As Object, e As EventArgs) Handles btnSendSms.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            'Dim Qry As String = Nothing
            Dim strPhoneno As String = Nothing
            Dim dtContent As DataTable = Nothing
            Dim strBMC As String
            Dim excStr As String = Nothing
            If clsCommon.CompairString(Form_ID, clsUserMgtCode.MilkCollectionMCCSample) = CompairStringResult.Equal Then
                dtContent = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.MilkCollectionMCCSample + "4" + "'", trans)
                excStr = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ES_Trans_Type_4 from TSPL_PROGRAM_MASTER where Program_Code='" + clsUserMgtCode.MilkCollectionMCCSample + "4" + "'", trans))
            End If

            Dim DCSCode() As String
            Dim objEmailH As New clsEMailHead()
            objEmailH.arrEMail = New List(Of String)()
            Dim objSMSH As New clsSMSHead()
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then
                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                    For Each grow In gv2.Rows
                        If clsCommon.myCBool(grow.Cells("colCheck").Value) Then
                            objSMSH.arrMobilNo = New List(Of String)()
                            strBMC = Nothing
                            strBMC = clsCommon.myCstr(grow.Cells("Mcc_Code_VLC_Uploader").Value)
                            strPhoneno = Nothing
                            Dim Qry1 As String = "Select (Case When IsNull(TSPL_VENDOR_MASTER.Phone1,'')='' Then TSPL_VENDOR_MASTER.Phone2 Else TSPL_VENDOR_MASTER.Phone1 End) As DCS_Contact, 
                                TSPL_VENDOR_MASTER.Vendor_Name from TSPL_VENDOR_MASTER
                                Left Outer join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code
                                where TSPL_VENDOR_MASTER.Form_Type='VSP' And (Case When IsNull(TSPL_VENDOR_MASTER.Phone1,'')='' Then TSPL_VENDOR_MASTER.Phone2 Else TSPL_VENDOR_MASTER.Phone1 End) Not In ('Null','','(+__)__________') And TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader = '" + strBMC + "'"
                            Dim dtt As DataTable = clsDBFuncationality.GetDataTable(Qry1, trans)
                            strPhoneno = clsCommon.myCstr(dtt.Rows(0)("DCS_Contact"))

                            DCSCode = (clsCommon.myCstr(grow.Cells("Mcc_Code_VLC_Uploader").Value).Split("-"))
                            objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCUploaderCode, clsCommon.myCstr(DCSCode(0)))
                            'objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCUploaderCode, clsCommon.myCstr(dtContent(0)))

                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCName, clsCommon.myCstr(dtt.Rows(0)("Vendor_Name")))
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderFat, clsCommon.myCstr(grow.Cells("FAT").Value))
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderSNF, clsCommon.myCstr(grow.Cells("SNF").Value))
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderQty, clsCommon.myCstr(grow.Cells("QTY").Value))
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.vehicleNo, clsCommon.myCstr(grow.Cells("vehicleNo").Value))
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.SampleNo, clsCommon.myCstr(grow.Cells("SampleNo").Value))

                            'objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderCLR, clsCommon.myCstr(grow.Cells("CNF").Value))

                            ' objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ES_Trans_Type_3, excStr)

                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Form_Code, MyBase.Form_ID = "4")
                            If clsCommon.myLen(strPhoneno) > 0 Then
                                strPhoneno = strPhoneno.Replace("(", "").Replace(")", "").Replace("+", "").Replace("_____", "").Replace("____", "").Replace("___", "").Replace("__", "").Replace("_", "")
                            End If
                            If clsCommon.myLen(strPhoneno) >= 10 Then
                                objSMSH.arrMobilNo.Add(clsCommon.myCstr(strPhoneno))
                            End If

                            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                                objSMSH.SaveData(clsUserMgtCode.MilkCollectionMCCSample + "4", objSMSH, trans)
                            End If
                        End If
                    Next
                    trans.Commit()
                End If
            End If
            objSMSH = Nothing
            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "SMS Send Successfully", Me.Text)
                If gv2 IsNot Nothing AndAlso gv2.Rows.Count > 0 Then
                    For i As Integer = 0 To gv2.Rows.Count - 1
                        gv2.Rows(i).Cells("ColCheck").Value = False
                    Next
                End If
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnSelectedAll_Click(sender As Object, e As EventArgs) Handles btnSelectedAll.Click
        Try
            If gv2 IsNot Nothing AndAlso gv2.Rows.Count > 0 Then
                For i As Integer = 0 To gv2.Rows.Count - 1
                    gv2.Rows(i).Cells("ColCheck").Value = True
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnUnSelectedAll_Click(sender As Object, e As EventArgs) Handles btnUnSelectedAll.Click
        Try
            If gv2 IsNot Nothing AndAlso gv2.Rows.Count > 0 Then
                For i As Integer = 0 To gv2.Rows.Count - 1
                    gv2.Rows(i).Cells("ColCheck").Value = False
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkSms_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkSms.ToggleStateChanged
        Try
            gv2.Columns(colCheck).ReadOnly = Not chkSms.Checked
        Catch ex As Exception
        End Try
    End Sub
End Class

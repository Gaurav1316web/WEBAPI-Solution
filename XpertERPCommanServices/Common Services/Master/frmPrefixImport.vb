Imports common
Imports System.Data.SqlClient

Public Class FrmPrefixImport

    Private Sub FrmPrefixImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboFiscalYear.DataSource = FrmPrefixGenerationNew.GetFiscalYears()
        cboFiscalYear.ValueMember = "Code"
        cboFiscalYear.DisplayMember = "Name"

        cboFiscalYear.SelectedValue = clsCommon.myCstr(DateTime.Now.Year)
    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            transportSql.importExcel(gv1, "Doc_Type", "Doc_Trans_Type", "Location_Code", "Doc_Prfeix", "Next_Number", "Separator", "Is_Change_Monthly", "Curr_Month", "Is_Change_Daily", "Curr_Date", "dontDisplayYearInSeries", "MinSizeofSeries", "Year_Separator", "Short_Fiscal_Year")


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


            gv1.BestFitColumns()
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
            Next
            gv1.AllowAddNewRow = True
            gv1.ShowGroupPanel = False
            gv1.ShowFilteringRow = True
            gv1.EnableFiltering = True
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            repoError.Width = 500
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnVerify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerify.Click
        AllowToSave(True)
    End Sub

    Function AllowToSave(ByVal isStartProgressBar As Boolean) As Boolean


        If isStartProgressBar Then
            clsCommon.ProgressBarPercentShow()
        End If


        For ii As Integer = 0 To gv1.RowCount - 1
            clsCommon.ProgressBarPercentUpdate(ii * 100 / gv1.RowCount - 1, "Verifing " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(gv1.RowCount - 1))
            gv1.CurrentRow = gv1.Rows(ii)
            gv1.Rows(ii).Cells("Error").Value = ""
            gv1.Rows(ii).Cells("IsOK").Value = 1
            Dim strTranCodeOuter As String = clsCommon.myCstr(gv1.Rows(ii).Cells("Doc_Type").Value)
            Dim strTranTypeOuter As String = clsCommon.myCstr(gv1.Rows(ii).Cells("Doc_Trans_Type").Value)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Doc_Type,Is_Location_Wise, Is_State_Wise from TSPL_DOCUMENT_TYPE where Doc_Type='" + strTranCodeOuter + "'")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                gv1.Rows(ii).Cells("Error").Value += "Doc Type is not correct."
                gv1.Rows(ii).Cells("IsOK").Value = 2
                Continue For
            End If
            Dim isStateReadOnly As Boolean = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_State_Wise")) > 0, False, True)
            Dim isLocationReadOnly As Boolean = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Location_Wise")) > 0, False, True)
            Dim isTransactionTypeReadOnly As Boolean = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select case when SUM(1)>0 then 1 else 0 end  from TSPL_DOCUMENT_TYPE where TSPL_DOCUMENT_TYPE.Doc_Type='" + strTranCodeOuter + "' and len(isnull(Doc_Trans_Type,''))>0")) > 0, False, True)
            'IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select case when SUM(1)>1 then 1 else 0 end  from TSPL_DOCUMENT_TYPE where TSPL_DOCUMENT_TYPE.Doc_Type='" + strTranCodeOuter + "'")) > 0, False, True)
            

            dt = clsDBFuncationality.GetDataTable("select 1 from TSPL_DOCUMENT_TYPE where Doc_Type='" + strTranCodeOuter + "' and Doc_Trans_Type='" + strTranTypeOuter + "'")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                gv1.Rows(ii).Cells("Error").Value += "Doc Trans Type is not correct."
                gv1.Rows(ii).Cells("IsOK").Value = 2
            End If
            If clsCommon.myLen(gv1.Rows(ii).Cells("Location_Code").Value) > 0 Then
                If Not isStateReadOnly Then
                    dt = clsDBFuncationality.GetDataTable("select 1   from TSPL_STATE_MASTER where STATE_CODE ='" + clsCommon.myCstr(gv1.Rows(ii).Cells("Location_Code").Value) + "'")
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        gv1.Rows(ii).Cells("Error").Value += "state Code is not correct."
                        gv1.Rows(ii).Cells("IsOK").Value = 2
                    End If
                Else
                    dt = clsDBFuncationality.GetDataTable("select 1 from TSPL_GL_SEGMENT_CODE Where 2=2 and Seg_No='7' and Segment_code ='" + clsCommon.myCstr(gv1.Rows(ii).Cells("Location_Code").Value) + "'")
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        gv1.Rows(ii).Cells("Error").Value += "Location Code is not correct."
                        gv1.Rows(ii).Cells("IsOK").Value = 2
                    End If
                End If
              
            End If

            If Not isLocationReadOnly AndAlso clsCommon.myLen(gv1.Rows(ii).Cells("Location_Code").Value) <= 0 Then
                gv1.Rows(ii).Cells("Error").Value += "Location is mendatory"
                gv1.Rows(ii).Cells("IsOK").Value = 2
            End If
            If Not isTransactionTypeReadOnly AndAlso clsCommon.myLen(gv1.Rows(ii).Cells("Doc_Trans_Type").Value) <= 0 Then
                gv1.Rows(ii).Cells("Error").Value += "Transaction type is mendatory"
                gv1.Rows(ii).Cells("IsOK").Value = 2
            End If

            If clsCommon.myCdbl(gv1.Rows(ii).Cells("dontDisplayYearInSeries").Value) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells("Short_Fiscal_Year").Value) > 0 Then
                gv1.Rows(ii).Cells("Error").Value += "Select one at a time(Don't Add Fiscal Year/Short Fiscal Year)"
                gv1.Rows(ii).Cells("IsOK").Value = 2
            End If

            For jj As Integer = 0 To gv1.RowCount - 1
                If ii = jj Then
                    Continue For
                End If
                Dim strTranCode As String = clsCommon.myCstr(gv1.Rows(jj).Cells("Doc_Type").Value)
                Dim strTranType As String = clsCommon.myCstr(gv1.Rows(jj).Cells("Doc_Trans_Type").Value)
                Dim strLocation As String = clsCommon.myCstr(gv1.Rows(jj).Cells("Location_Code").Value)

                Dim strPrefix As String = clsCommon.myCstr(gv1.Rows(jj).Cells("Doc_Prfeix").Value)
                If clsCommon.myCdbl(gv1.Rows(ii).Cells("Is_Change_Monthly").Value) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells("Doc_Trans_Type").Value), strTranType) = CompairStringResult.Equal _
                    AndAlso clsCommon.CompairString(strTranCode, strTranCodeOuter) = CompairStringResult.Equal _
                    AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells("Location_Code").Value), strLocation) = CompairStringResult.Equal _
                    AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells("Curr_Month").Value) = clsCommon.myCdbl(gv1.Rows(jj).Cells("Curr_Month").Value) Then
                        gv1.Rows(ii).Cells("Error").Value += "Repeted Month"
                        gv1.Rows(ii).Cells("IsOK").Value = 2
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells("Doc_Prfeix").Value), strPrefix) = CompairStringResult.Equal _
                    AndAlso clsCommon.CompairString(strTranCode, strTranCodeOuter) = CompairStringResult.Equal _
                    AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells("Curr_Month").Value) = clsCommon.myCdbl(gv1.Rows(jj).Cells("Curr_Month").Value) Then
                        gv1.Rows(ii).Cells("Error").Value += "Repeated Prefix"
                        gv1.Rows(ii).Cells("IsOK").Value = 2
                    End If
                ElseIf clsCommon.myCdbl(gv1.Rows(ii).Cells("Is_Change_Daily").Value) > 0 Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells("Curr_Date").Value) <= 0 Then
                        gv1.Rows(ii).Cells("Error").Value += "Current date not found."
                        gv1.Rows(ii).Cells("IsOK").Value = 2
                    End If
                    Dim dtOuterDate As Date = clsCommon.myCDate(gv1.Rows(ii).Cells("Curr_Date").Value, "dd/MMM/yyyy")
                    Dim dtInnerDate As Date = clsCommon.myCDate(gv1.Rows(jj).Cells("Curr_Date").Value, "dd/MMM/yyyy")
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells("Doc_Trans_Type").Value), strTranType) = CompairStringResult.Equal _
                    AndAlso clsCommon.CompairString(strTranCode, strTranCodeOuter) = CompairStringResult.Equal _
                    AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells("Location_Code").Value), strLocation) = CompairStringResult.Equal _
                    AndAlso dtOuterDate = dtInnerDate Then
                        gv1.Rows(ii).Cells("Error").Value += "Repeted Date."
                        gv1.Rows(ii).Cells("IsOK").Value = 2
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells("Doc_Prfeix").Value), strPrefix) = CompairStringResult.Equal _
                    AndAlso clsCommon.CompairString(strTranCode, strTranCodeOuter) = CompairStringResult.Equal _
                    AndAlso dtOuterDate = dtInnerDate Then
                        gv1.Rows(ii).Cells("Error").Value += "Repeted Prefix."
                        gv1.Rows(ii).Cells("IsOK").Value = 2
                    End If
                Else
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells("Doc_Trans_Type").Value), strTranType) = CompairStringResult.Equal _
                    AndAlso clsCommon.CompairString(strTranCode, strTranCodeOuter) = CompairStringResult.Equal _
                    AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells("Location_Code").Value), strLocation) = CompairStringResult.Equal Then
                        gv1.Rows(ii).Cells("Error").Value += "Repeted Row"
                        gv1.Rows(ii).Cells("IsOK").Value = 2
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells("Doc_Prfeix").Value), strPrefix) = CompairStringResult.Equal _
                    AndAlso clsCommon.CompairString(strTranCode, strTranCodeOuter) = CompairStringResult.Equal Then
                        gv1.Rows(ii).Cells("Error").Value += "Repeted Prefix"
                        gv1.Rows(ii).Cells("IsOK").Value = 2
                    End If
                End If
            Next
        Next
        If isStartProgressBar Then
            clsCommon.ProgressBarPercentHide()
        End If
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            clsCommon.ProgressBarPercentShow()
            If AllowToSave(False) Then
                If gv1.Rows.Count > 0 Then
                    Dim dictionary As New Dictionary(Of String, List(Of clsDocPrefix))
                    For ii As Integer = 0 To gv1.RowCount - 1
                        clsCommon.ProgressBarPercentUpdate(ii * 50 / gv1.RowCount - 1, "Fetching " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(gv1.RowCount - 1))
                        If clsCommon.myCdbl(gv1.Rows(ii).Cells("IsOK").Value) = 1 Then
                            Dim strDocType As String = clsCommon.myCstr(gv1.Rows(ii).Cells("Doc_Type").Value)
                            Dim arr As New List(Of clsDocPrefix)
                            If dictionary.ContainsKey(strDocType.ToUpper()) Then
                                arr = dictionary(strDocType.ToUpper())
                                dictionary.Remove(strDocType.ToUpper())
                            End If

                            Dim obj As New clsDocPrefix()
                            obj.Doc_Type = clsCommon.myCstr(gv1.Rows(ii).Cells("Doc_Type").Value)
                            obj.Doc_Trans_Type = clsCommon.myCstr(gv1.Rows(ii).Cells("Doc_Trans_Type").Value)
                            obj.OldDocTransType = clsCommon.myCstr(gv1.Rows(ii).Cells("Doc_Trans_Type").Value)
                            obj.Location_Code = clsCommon.myCstr(gv1.Rows(ii).Cells("Location_Code").Value)
                            obj.OldLocationCode = clsCommon.myCstr(gv1.Rows(ii).Cells("Location_Code").Value)
                            obj.Doc_Prfeix = clsCommon.myCstr(gv1.Rows(ii).Cells("Doc_Prfeix").Value)
                            obj.Next_Number = clsCommon.myCdbl(gv1.Rows(ii).Cells("Next_Number").Value)
                            obj.Separator = clsCommon.myCstr(gv1.Rows(ii).Cells("Separator").Value)
                            obj.Is_Change_Monthly = IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells("Is_Change_Monthly").Value) = 1, True, False)
                            If obj.Is_Change_Monthly Then
                                obj.Curr_Month = clsCommon.myCdbl(gv1.Rows(ii).Cells("Curr_Month").Value)
                            End If
                            obj.Is_Change_Daily = IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells("Is_Change_Daily").Value) = 1, True, False)
                            If obj.Is_Change_Daily Then
                                obj.Curr_Date = clsCommon.myCDate(gv1.Rows(ii).Cells("Curr_Date").Value)
                            End If
                            obj.dontDisplayYearInSeries = IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells("dontDisplayYearInSeries").Value) = 1, True, False)
                            obj.Year_Separator = clsCommon.myCstr(gv1.Rows(ii).Cells("Year_Separator").Value)
                            obj.MinSizeofSeries = clsCommon.myCdbl(gv1.Rows(ii).Cells("MinSizeofSeries").Value)
                            obj.Short_Fiscal_Year = IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells("Short_Fiscal_Year").Value) = 1, True, False)
                            If clsCommon.myLen(obj.Doc_Prfeix) > 0 Then
                                Dim qry As String = "select 1 from TSPL_DOCPREFIX_MASTER where 2=2 and  Doc_Type= '" + obj.Doc_Type + "' and Doc_Trans_Type=ISNULL('" + obj.Doc_Trans_Type + "','')  and Location_Code=isnull('" + obj.Location_Code + "','')"
                                If cboFiscalYear.Enabled = True Then
                                    qry += " and Fin_Year='" + clsCommon.myCstr(clsCommon.myCdbl(cboFiscalYear.SelectedValue)) + "'"
                                End If
                                Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry)
                                If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                                    obj.isNewEntry = False
                                Else
                                    obj.isNewEntry = True
                                End If
                                arr.Add(obj)
                                dictionary.Add(strDocType.ToUpper(), arr)
                            End If
                        End If
                    Next

                    ''Saveing Data
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Try
                        For ii As Integer = 0 To dictionary.Keys.Count - 1
                            clsCommon.ProgressBarPercentUpdate(ii * 100 / dictionary.Keys.Count - 1, "Saving " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(dictionary.Keys.Count - 1))
                            Dim strKey As String = dictionary.Keys(ii)
                            clsDocPrefix.SaveData(dictionary(strKey).Item(0).Doc_Type, IIf(cboFiscalYear.Enabled = True, Convert.ToInt32(clsCommon.myCdbl(cboFiscalYear.SelectedValue)), 0), dictionary(strKey), trans)
                        Next
                        trans.Commit()
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception(ex.Message)
                    End Try
                End If
                clsCommon.ProgressBarPercentHide()
                common.clsCommon.MyMessageBoxShow(Me, "Counters set successfully", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

End Class


'============BM00000007900=============================
Imports System.Data.SqlClient
Imports common
Imports System.IO
Public Class FrmVlcDataUploadar
    Inherits FrmMainTranScreen
    Public Const colSelect As String = "colSelect"
    Public Const colVLCCode As String = "VLCCode"
    Public Const colVlcName As String = "VlcName"
    Public Const colRouteNo As String = "RouteNo"
    Public Const colMpId As String = "MpId"
    Public Const colMpName As String = "MpName"
    Public Const colQty As String = "Qty"
    Public Const colFAT As String = "FAT"
    Public Const colSNF As String = "SNF"
    Public Const colRate As String = "Rate"
    Public Const colAmount As String = "Amount"
    Public Const colWater As String = "Water"
    Public Const colMilkType As String = "MilkType"
    Public Const colDate As String = "colDate"
    Public Const colShift As String = "colShift"
    Public Const colMccId As String = "colMccId"
    Public dateofFile As String = String.Empty
    Public shiftoffile As String = String.Empty
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public dtable As DataTable = Nothing
    Public obj As clsVlcDataUploader = Nothing
    Dim arrObj As List(Of clsVlcDataUploader) = Nothing
    Dim IsinsideLoadData As Boolean = False
    Dim settBennyImportAutoCreateMP As Boolean = False
    Dim settBennyImportPickRateFromPrice As Boolean = False

    Private Sub FrmVlcDataUploadar_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        settBennyImportAutoCreateMP = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BennyImportAutoCreateMP, clsFixedParameterCode.BennyImportAutoCreateMP, Nothing)) = 1)
        settBennyImportPickRateFromPrice = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BennyImportPickRateFromPrice, clsFixedParameterCode.BennyImportPickRateFromPrice, Nothing)) = 1)
        SetUserMgmtNew()
        multipleDelteVisible(False)
        reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If

    End Sub

    Sub ResetDataTableDt()
        dtable = Nothing
        dtable = New DataTable()
        dtable.Columns.Add(colVLCCode)
        dtable.Columns.Add(colMpId)
        dtable.Columns.Add(colQty)
        dtable.Columns.Add(colFAT)
        dtable.Columns.Add(colSNF)
        dtable.Columns.Add(colWater)
        dtable.Columns.Add(colMilkType)
        dtable.Columns.Add(colDate)
        dtable.Columns.Add(colShift)
        dtable.Columns.Add(colMccId)
        dtable.Columns.Add(colRouteNo)
        dtable.Columns.Add(colRate)
        dtable.Columns.Add(colAmount)
    End Sub
    Sub reset()
        fndDocNo.Value = ""
        fndDocNo.MyReadOnly = False
        Dim dt As Date = clsCommon.GETSERVERDATE()
        dtpDocDate.Value = dt
        txtMccCode.Text = ""
        lblMccName.Text = ""
        dtpDate.Value = dt
        txtShift.Text = ""
        txtBrowse.Text = ""
        LoadBlankGridVLC()
        LoadBlankGridData()
        ResetDataTableDt()
        btnDelete.Enabled = False
        btnSave.Enabled = True
        btnSave.Text = "Save"
        IsinsideLoadData = False
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmVlcdataUploadar)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadBlankGridVLC()
        gvVLC.Rows.Clear()
        gvVLC.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvVLC.MasterTemplate.Columns.Add(repoSelect)


        Dim repoVLCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVLCCode.FormatString = ""
        repoVLCCode.HeaderText = "VLC Code"
        repoVLCCode.Name = colVLCCode
        repoVLCCode.Width = 100
        repoVLCCode.ReadOnly = True
        gvVLC.MasterTemplate.Columns.Add(repoVLCCode)

        Dim repoVLCName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVLCName.FormatString = ""
        repoVLCName.HeaderText = "VLC Name"
        repoVLCName.Name = colVlcName
        repoVLCName.Width = 150
        repoVLCName.ReadOnly = True
        gvVLC.MasterTemplate.Columns.Add(repoVLCName)


        Dim repoRouteNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRouteNo.FormatString = ""
        repoRouteNo.HeaderText = "Route No"
        repoRouteNo.Name = colRouteNo
        repoRouteNo.Width = 100
        repoRouteNo.ReadOnly = True
        gvVLC.MasterTemplate.Columns.Add(repoRouteNo)






        gvVLC.AllowAddNewRow = False
        gvVLC.AllowDeleteRow = False
        gvVLC.ShowGroupPanel = False
        gvVLC.AllowColumnReorder = True
        gvVLC.AllowRowReorder = True
        gvVLC.EnableSorting = False
        gvVLC.MasterTemplate.ShowRowHeaderColumn = False
        gvVLC.MasterTemplate.ShowColumnHeaders = True
        gvVLC.EnableAlternatingRowColor = True
        gvVLC.EnableFiltering = True
        gvVLC.ShowFilteringRow = True
        gvVLC.TableElement.TableHeaderHeight = 40
        gvVLC.BestFitColumns()
    End Sub

    Sub LoadBlankGridData()
        gvData.Rows.Clear()
        gvData.Columns.Clear()

        'mp id,mPnAME,qTY,fAT,Snf,Water,Milk type


        Dim repoMpId As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMpId.FormatString = ""
        repoMpId.HeaderText = "MP ID"
        repoMpId.Name = colMpId
        repoMpId.Width = 100
        repoMpId.ReadOnly = True
        gvData.MasterTemplate.Columns.Add(repoMpId)

        Dim repoMpName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMpName.FormatString = ""
        repoMpName.HeaderText = "MP Name"
        repoMpName.Name = colMpName
        repoMpName.Width = 150
        repoMpName.ReadOnly = True
        gvData.MasterTemplate.Columns.Add(repoMpName)


        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Qty"
        repoQty.Name = colQty
        repoQty.Width = 100
        repoQty.ReadOnly = True
        gvData.MasterTemplate.Columns.Add(repoQty)

        Dim repoFAT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFAT.FormatString = ""
        repoFAT.HeaderText = "FAT %"
        repoFAT.Name = colFAT
        repoFAT.Width = 100
        repoFAT.ReadOnly = True
        gvData.MasterTemplate.Columns.Add(repoFAT)

        Dim repoSNF As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNF.FormatString = ""
        repoSNF.HeaderText = "SNF %"
        repoSNF.Name = colSNF
        repoSNF.Width = 100
        repoSNF.ReadOnly = True
        gvData.MasterTemplate.Columns.Add(repoSNF)

        Dim repoWater As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoWater.FormatString = ""
        repoWater.HeaderText = "Water"
        repoWater.Name = colWater
        repoWater.Width = 100
        repoWater.ReadOnly = True
        gvData.MasterTemplate.Columns.Add(repoWater)



        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colRate
        repoRate.Width = 100
        repoRate.ReadOnly = True
        gvData.MasterTemplate.Columns.Add(repoRate)

        Dim repoAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = colAmount
        repoAmount.Width = 100
        repoAmount.ReadOnly = True
        gvData.MasterTemplate.Columns.Add(repoAmount)

        Dim repoVLCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVLCCode.FormatString = ""
        repoVLCCode.HeaderText = "VLC Code"
        repoVLCCode.Name = colVLCCode
        repoVLCCode.Width = 0
        repoVLCCode.ReadOnly = True
        repoVLCCode.IsVisible = False
        gvData.MasterTemplate.Columns.Add(repoVLCCode)

        Dim repoMilktype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMilktype.FormatString = ""
        repoMilktype.HeaderText = "Milk Type"
        repoMilktype.Name = colMilkType
        repoMilktype.Width = 100
        repoMilktype.ReadOnly = True
        gvData.MasterTemplate.Columns.Add(repoMilktype)

        repoMilktype = New GridViewTextBoxColumn()
        repoMilktype.FormatString = ""
        repoMilktype.HeaderText = "Shift"
        repoMilktype.Name = colShift
        repoMilktype.Width = 100
        repoMilktype.ReadOnly = True
        gvData.MasterTemplate.Columns.Add(repoMilktype)

        gvData.AllowAddNewRow = False
        gvData.AllowDeleteRow = False
        gvData.ShowGroupPanel = False
        gvData.AllowColumnReorder = True
        gvData.AllowRowReorder = True
        gvData.EnableSorting = False
        gvData.MasterTemplate.ShowRowHeaderColumn = False
        gvData.MasterTemplate.ShowColumnHeaders = True
        gvData.EnableAlternatingRowColor = True
        gvData.EnableFiltering = True
        gvData.ShowFilteringRow = True
        gvData.TableElement.TableHeaderHeight = 40
        gvData.BestFitColumns()

        If gvData.MasterView.SummaryRows.Count = 0 Then
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim TranQty As New GridViewSummaryItem(colQty, "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TranQty)
            Dim TranAmount As New GridViewSummaryItem(colAmount, "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TranAmount)
            gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gvData.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        End If
    End Sub

    Sub LoadData(ByVal str As String, ByVal navtype As NavigatorType)
        IsinsideLoadData = True
        arrObj = clsVlcDataUploader.getData(str, navtype)
        If arrObj IsNot Nothing AndAlso arrObj.Count > 0 Then
            btnSave.Enabled = False
            fndDocNo.Value = arrObj(0).Doc_No
            dtpDocDate.Value = arrObj(0).Doc_Date
            dtpDate.Value = arrObj(0).File_Date
            txtShift.Text = arrObj(0).shift
            txtMccCode.Text = arrObj(0).MCC_Code
            lblMccName.Text = clsMccMaster.getMccNameOnMccCodeForVLCUplader(txtMccCode.Text, Nothing)
            ResetDataTableDt()
            LoadBlankGridVLC()
            LoadBlankGridData()
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select distinct vlc_code from tspl_vlc_data_uploader where doc_no='" & arrObj(0).Doc_No & "'")
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For j As Integer = 0 To dt1.Rows.Count - 1
                    gvVLC.Rows.AddNew()
                    gvVLC.Rows(j).Cells(colSelect).Value = True
                    gvVLC.Rows(j).Cells(colVLCCode).Value = dt1.Rows(j)("vlc_code").ToString
                    gvVLC.Rows(j).Cells(colVlcName).Value = clsfrmVLCMaster.getVLcNameOnVLcCodeForVLCUplader(gvVLC.Rows(j).Cells(colVLCCode).Value, txtMccCode.Text, Nothing)
                    gvVLC.Rows(j).Cells(colRouteNo).Value = clsfrmVLCMaster.getRouteNoOnVLcCodeForVLCUplader(gvVLC.Rows(j).Cells(colVLCCode).Value, txtMccCode.Text, Nothing)
                Next
            End If
            Dim ValidatedCount As Integer = 1
            ValidatedCount = arrObj.Count - 1
            For i As Integer = 0 To arrObj.Count - 1
                ''richa agarwal 27/01/2015 BM00000008759
                'dtable.Rows.Add(arrObj(i).VLC_CODE, arrObj(i).MP_CODE, arrObj(i).qty.ToString, arrObj(i).fat.ToString, arrObj(i).snf.ToString, arrObj(i).water.ToString, arrObj(i).Milk_Type, arrObj(i).File_Date, arrObj(i).shift, arrObj(i).MCC_Code, arrObj(i).Route_No, arrObj(i).Rate, arrObj(i).Amount)
                'btnrefresh_Click(Nothing, Nothing)

                gvData.Rows.AddNew()
                gvData.Rows(i).Cells(colMpId).Value = arrObj(i).MP_CODE
                gvData.Rows(i).Cells(colMpName).Value = clsMpMaster.getMPNameOnMPCodeForVLCUplader(arrObj(i).MP_CODE, clsfrmVLCMaster.getVLcCodeForVLCUplader(arrObj(i).VLC_CODE, arrObj(i).MCC_Code, Nothing), Nothing)
                gvData.Rows(i).Cells(colQty).Value = clsCommon.myCdbl(arrObj(i).qty.ToString)
                gvData.Rows(i).Cells(colFAT).Value = clsCommon.myCdbl(arrObj(i).fat.ToString)
                gvData.Rows(i).Cells(colSNF).Value = clsCommon.myCdbl(arrObj(i).snf.ToString)
                gvData.Rows(i).Cells(colRate).Value = clsCommon.myCdbl(arrObj(i).Rate)
                gvData.Rows(i).Cells(colAmount).Value = clsCommon.myCdbl(arrObj(i).Amount)
                gvData.Rows(i).Cells(colWater).Value = clsCommon.myCdbl(arrObj(i).water.ToString)
                gvData.Rows(i).Cells(colMilkType).Value = arrObj(i).Milk_Type.ToString
                gvData.Rows(i).Cells(colShift).Value = arrObj(i).shift.ToString
                ''------------------------------
            Next
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            fndDocNo.MyReadOnly = True
        Else
            btnSave.Enabled = True
            btnDelete.Enabled = False
            btnSave.Text = "Save"
            fndDocNo.MyReadOnly = False
        End If
        IsinsideLoadData = False
    End Sub

    Sub SaveData()
        Dim isNewEntry As Boolean = False
        Dim docCode As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                isNewEntry = True
            Else
                isNewEntry = False
            End If
            Dim dt As Date = clsCommon.GETSERVERDATE(trans)

            If isNewEntry Then
                docCode = clsERPFuncationality.GetNextCode(trans, dtpDocDate.Value, clsDocType.vlcDataUploader, "", clsfrmVLCMaster.getMccCodeOnMccCodeForVLCUplader(txtMccCode.Text, trans))
                If clsCommon.myLen(docCode) <= 0 Then
                    Throw New Exception("Error In Document  No Genertion")
                End If
            Else
                docCode = clsCommon.myCstr(fndDocNo.Value)
            End If

            Dim strSelectedVlc As String = String.Empty
            If gvVLC.Rows.Count > 0 Then
                For i As Integer = 0 To gvVLC.Rows.Count - 1
                    If gvVLC.Rows(i).Cells(colSelect).Value = True Then
                        strSelectedVlc = strSelectedVlc & "'" & gvVLC.Rows(i).Cells(colVLCCode).Value & "',"
                    End If
                Next
                If clsCommon.myLen(strSelectedVlc) > 0 Then
                    strSelectedVlc = Microsoft.VisualBasic.Left(strSelectedVlc, Microsoft.VisualBasic.Len(strSelectedVlc) - 1)
                Else
                    Throw New Exception("Please select at least one VLC")
                End If
                dtable.DefaultView.RowFilter = ""
                dtable.DefaultView.RowFilter = colVLCCode & " in ( " & strSelectedVlc & ")"
                arrObj = New List(Of clsVlcDataUploader)
                Dim rowGv As Integer = 0
                Dim ValidatedCount As Integer = 0
                ValidatedCount = (dtable.DefaultView.ToTable).Rows.Count - 1
                ''richa agarwal 26 Sep,2019 done to resolve arithmetic overflow exception
                If ValidatedCount = 0 Then
                    ValidatedCount = 1
                End If


                For j As Integer = 0 To (dtable.DefaultView.ToTable).Rows.Count - 1
                    If clsCommon.myLen((dtable.DefaultView.ToTable).Rows(j)(colMpId)) <= 0 Then
                    Else
                        obj = New clsVlcDataUploader()
                        obj.Doc_No = docCode
                        obj.Doc_Date = clsCommon.GetPrintDate(dtpDocDate.Value, "dd/MMM/yyyy")
                        obj.isNewEntry = isNewEntry
                        obj.MCC_Code = clsfrmVLCMaster.getMccCodeOnMccCodeForVLCUplader(clsCommon.myCstr((dtable.DefaultView.ToTable).Rows(j)(colMccId)), trans)
                        obj.VLC_CODE = clsCommon.myCstr((dtable.DefaultView.ToTable).Rows(j)(colVLCCode))
                        obj.Route_No = clsCommon.myCstr((dtable.DefaultView.ToTable).Rows(j)(colRouteNo))
                        obj.File_Date = clsCommon.GetPrintDate(dtpDate.Value, "dd/MMM/yyyy")
                        obj.shift = clsCommon.myCstr((dtable.DefaultView.ToTable).Rows(j)(colShift))
                        obj.MP_CODE = (dtable.DefaultView.ToTable).Rows(j)(colMpId)
                        obj.qty = clsCommon.myCdbl((dtable.DefaultView.ToTable).Rows(j)(colQty))
                        obj.fat = clsCommon.myCdbl((dtable.DefaultView.ToTable).Rows(j)(colFAT))
                        obj.snf = clsCommon.myCdbl((dtable.DefaultView.ToTable).Rows(j)(colSNF))
                        obj.Rate = clsCommon.myCdbl((dtable.DefaultView.ToTable).Rows(j)(colRate))
                        obj.Amount = clsCommon.myCdbl((dtable.DefaultView.ToTable).Rows(j)(colAmount))
                        obj.water = clsCommon.myCdbl((dtable.DefaultView.ToTable).Rows(j)(colWater))
                        obj.Milk_Type = (dtable.DefaultView.ToTable).Rows(j)(colMilkType)
                        obj.Modified_By = objCommonVar.CurrentUserCode
                        obj.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                        obj.Comp_Code = objCommonVar.CurrentCompanyCode
                        If isNewEntry Then
                            obj.Created_By = objCommonVar.CurrentUserCode
                            obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                        End If

                        Dim dtTemp As DataTable = ClsVLCDataUploaderManual.GetQueryDT("PDU", clsCommon.GetPrintDate(dtpDate.Value, "dd/MMM/yyyy"), obj.shift, obj.MCC_Code, "", obj.VLC_CODE, obj.Route_No, obj.MP_CODE, "", trans, obj.qty, obj.fat, obj.snf)
                        If dtTemp.Rows.Count > 0 Then
                            Throw New Exception("Dublicate Data is Entered for MCC-" & obj.MCC_Code & " and  Uploader Code-" & obj.VLC_CODE & " and  Date-" & clsCommon.GetPrintDate(dtpDate.Value, "dd/MMM/yyyy") & " and  Shift-" & obj.shift & " through Uploader.")
                        End If
                        arrObj.Add(obj)
                    End If
                Next

                If clsVlcDataUploader.saveData(arrObj, trans) Then
                    trans.Commit()
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                    fndDocNo.MyReadOnly = True
                    If rbtnFile.IsChecked Then
                        clsCommon.MyMessageBoxShow(Me, "Data Uploaded Successfully", Me.Text)
                        LoadData(docCode, NavigatorType.Current)
                    End If
                    Exit Sub
                Else
                    btnSave.Text = "Save"
                    btnDelete.Enabled = False
                    fndDocNo.MyReadOnly = False
                    Throw New Exception("Data could not uploaded ")
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            If rbtnFile.IsChecked Then
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Else
                Throw New Exception(ex.Message)
            End If
        End Try
    End Sub
    Sub deleteData()
        Try
            clsVlcDataUploader.deleteData(fndDocNo.Value, Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Function allowToSave() As Boolean
        Try
            '====================Added by preeti gupta==================
            Dim MCCCode As String = clsDBFuncationality.getSingleValue("select mcc_code from TSPL_MCC_MASTER where mcc_code_VLC_uploader='" & txtMccCode.Text & "'")
            clsLockMPPaymentCycle.LockMPTransaction(MCCCode, dtpDate.Value)
            '===========================================================            '== KUNAL > TICKET : BM00000009575 ============
            If AllowFutureDateTransaction(dtpDocDate.Value, Nothing) = False Then
                dtpDocDate.Focus()
                Return False
            End If

            If clsCommon.myLen(txtMccCode.Text) <= 0 Then
                Throw New Exception("MCC Code not found in file")
            End If
            'If clsCommon.myLen(txtBrowse.Text) <= 0 Then
            '    Throw New Exception("please Select a File to upload")
            'End If
            If gvData.Rows.Count <= 0 Then
                Throw New Exception("Please select atleast one data to upload")
            End If

        Catch ex As Exception
            If rbtnFile.IsChecked Then
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Else
                Throw New Exception(ex.Message)
            End If
        End Try
        Return True
    End Function

    Private Sub FrmVlcDataUploadar_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave_Click(sender, e)

        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose_Click(sender, e)
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                         "========Table Name=========" + Environment.NewLine +
                         "tspl_vlc_data_uploader" + Environment.NewLine +
                         "TSPL_VLC_DATA_UPLOADER_DETAIL" + Environment.NewLine +
                         "=========Setting Name======" + Environment.NewLine +
                         "isFarmerPaymentCycle")
            If RadButton238.Visible Then
                multipleDelteVisible(False)
            Else
                Dim dbpwd As String = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" + clsFixedParameterCode.VillageDataReverse + "' and TYPE='" + clsFixedParameterType.VillageDataReverse + "'")
                Dim pwd As New FrmPWD(Nothing)
                pwd.strCode = clsFixedParameterCode.VillageDataReverse
                pwd.strType = clsFixedParameterType.VillageDataReverse
                pwd.ShowDialog()
                If pwd.isPasswordCorrect Then
                    multipleDelteVisible(True)
                End If
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try

            If clsCommon.myLen(fndDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select a document to delete", Me.Text)
                Exit Sub
            End If
            Dim MCCCode As String = clsDBFuncationality.getSingleValue("select mcc_code from TSPL_MCC_MASTER where mcc_code_VLC_uploader='" & txtMccCode.Text & "'")
            clsLockMPPaymentCycle.LockMPTransaction(MCCCode, dtpDate.Value)
            If myMessages.deleteConfirm() Then
                deleteData()
                myMessages.delete()
                reset()
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myLen(txtBrowse.Text) <= 0 Then
                Throw New Exception("Please Select file to upload")
            End If
            ''RICHA AGARWAL 25/01/2016 BM00000008715
            Dim fileDate As String = Nothing
            Dim fileShift As String = Nothing
            If txtBrowse.Text.Contains(".BDF") = True Or txtBrowse.Text.Contains(".bdf") = True Then
                FunInportBDFFile()
                Exit Sub
            ElseIf txtBrowse.Text.Contains(".TXT") = True Or txtBrowse.Text.Contains(".txt") = True Then
                If clsCommon.myLen(Me.Tag) <> 11 Then
                    Throw New Exception("Please Select a valid file to upload" & Environment.NewLine & "File should have extention '.TXT'" & Environment.NewLine & "File name Should be a date ended with E/M for shift as 'DDMMYYE.TXT'")
                End If
                fileDate = Microsoft.VisualBasic.Left(Me.Tag, 2) & "-" & Microsoft.VisualBasic.Mid(Me.Tag, 3, 2) & "-" & Microsoft.VisualBasic.Mid(Me.Tag, 5, 2)
                fileShift = Microsoft.VisualBasic.Mid(Me.Tag, 7, 1)
                If Not IsDate(fileDate) Then
                    Throw New Exception("Please Select a valid file to upload" & Environment.NewLine & "File should have extention '.TXT'" & Environment.NewLine & "File name Should be a date ended with E/M for shift as 'DDMMYYE.TXT'")
                End If
                If Not (clsCommon.CompairString(fileShift, "M") = CompairStringResult.Equal OrElse clsCommon.CompairString(fileShift, "E") = CompairStringResult.Equal) Then
                    Throw New Exception("Please Select a valid file to upload" & Environment.NewLine & "File should have extention '.TXT'" & Environment.NewLine & "File name Should be a date ended with E/M for shift as 'DDMMYYE.TXT'")

                End If
            ElseIf txtBrowse.Text.Contains(".BIP") = True Or txtBrowse.Text.Contains(".bip") = True Then
                If clsCommon.myLen(Me.Tag) <> 12 Then
                    Throw New Exception("Please Select a valid file to upload" & Environment.NewLine & "File should have extention '.BIP'" & Environment.NewLine & "File name Should be a date ended with E/M for shift as 'VYYMMDDE.EIP'")
                End If
                fileDate = Microsoft.VisualBasic.Mid(Me.Tag, 6, 2) & "-" & Microsoft.VisualBasic.Mid(Me.Tag, 4, 2) & "-" & Microsoft.VisualBasic.Mid(Me.Tag, 2, 2)
                fileShift = Microsoft.VisualBasic.Mid(Me.Tag, 8, 1)
                If Not IsDate(fileDate) Then
                    Throw New Exception("Please Select a valid file to upload" & Environment.NewLine & "File should have extention '.BIP'" & Environment.NewLine & "File name Should be a date ended with E/M for shift as 'VYYMMDDE.EIP'")
                End If
                If Not (clsCommon.CompairString(fileShift, "M") = CompairStringResult.Equal OrElse clsCommon.CompairString(fileShift, "E") = CompairStringResult.Equal) Then
                    Throw New Exception("Please Select a valid file to upload" & Environment.NewLine & "File should have extention '.BIP'" & Environment.NewLine & "File name Should be a date ended with E/M for shift as 'VYYMMDDE.EIP'")
                End If
            Else
                If clsCommon.myLen(Me.Tag) <> 11 Then
                    Throw New Exception("Please Select a valid file to upload" & Environment.NewLine & "File should have extention '.EIP'" & Environment.NewLine & "File name Should be a date ended with E/M for shift as 'DDMMYYE.EIP'")
                End If
                fileDate = Microsoft.VisualBasic.Left(Me.Tag, 2) & "-" & Microsoft.VisualBasic.Mid(Me.Tag, 3, 2) & "-" & Microsoft.VisualBasic.Mid(Me.Tag, 5, 2)
                fileShift = Microsoft.VisualBasic.Mid(Me.Tag, 7, 1)
                If Not IsDate(fileDate) Then
                    Throw New Exception("Please Select a valid file to upload" & Environment.NewLine & "File should have extention '.EIP'" & Environment.NewLine & "File name Should be a date ended with E/M for shift as 'DDMMYYE.EIP'")
                End If
                If Not (clsCommon.CompairString(fileShift, "M") = CompairStringResult.Equal OrElse clsCommon.CompairString(fileShift, "E") = CompairStringResult.Equal) Then
                    Throw New Exception("Please Select a valid file to upload" & Environment.NewLine & "File should have extention '.EIP'" & Environment.NewLine & "File name Should be a date ended with E/M for shift as 'DDMMYYE.EIP'")
                End If
            End If
            dateofFile = fileDate
            shiftoffile = fileShift
            loadVlcDetail()
        Catch ex As Exception
            If rbtnFile.IsChecked Then
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Else
                Throw New Exception(ex.Message)
            End If
        End Try
    End Sub

    Sub FunInportBDFFile()
        Try
            LoadBlankGridVLC()
            LoadBlankGridData()
            If Not File.Exists(Application.StartupPath + "\XpertBennyDecrptor.exe") Then
                Throw New Exception("Please add File - XpertBennyDecrptor.exe.")
            End If
            If txtBrowse.Text.Contains(" ") Then
                Throw New Exception("Please Remove Blank space of your folder becuase it is having Blank Space")
            End If
            Dim strOPFile As String = "C:\\ERPTempFolder\BSP.CSV"
            If rbtnFolder.IsChecked Then
                strOPFile = "C:\\ERPTempFolder\BSP" + clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddHHmmssffffff") + ".CSV"
            End If
            If File.Exists(strOPFile) Then
                File.Delete(strOPFile)
            End If

            Process.Start(Application.StartupPath + "\XpertBennyDecrptor.exe", "-i " + txtBrowse.Text + " -o " + strOPFile + " -s , -f")
            Dim dt As DataTable = transportSql.GetExcelData(strOPFile, Path.GetFileName(txtBrowse.Text))
            Dim strVLCCode As String = Nothing
            Dim strMCCCode As String = Nothing
            Dim ii As Integer = 0
            Dim qry As String
            Dim dtMasterData As DataTable = Nothing
            dtable.Rows.Clear()
            Try
                For ii = 0 To dt.Rows.Count - 1
                    If clsCommon.myCdbl(dt.Rows(ii)("Quantity")) <= 0 Then
                        Continue For
                    End If
                    Dim strMPUploaderCode As String = clsCommon.myCstr(dt.Rows(ii)("Extended_Code"))
                    If strMPUploaderCode.Contains("...") Then
                        Continue For
                        Try
                            Dim strMPUploaderCodeNew As String = clsCommon.myCstr(dt.Rows(ii - 1)("Extended_Code"))
                            If strMPUploaderCodeNew.Contains("...") Then
                                strMPUploaderCodeNew = clsCommon.myCstr(dt.Rows(ii + 1)("Extended_Code"))
                            End If
                            strMPUploaderCodeNew = strMPUploaderCodeNew.Substring(0, strMPUploaderCodeNew.Length - 3)
                            strMPUploaderCode = strMPUploaderCodeNew + "000"
                        Catch ex As Exception
                        End Try
                        If strMPUploaderCode.Contains("...") Then
                            Throw New Exception("Invalid MP Uploader Code [" + strMPUploaderCode + "]")
                        End If
                    End If
                    If settBennyImportAutoCreateMP Then
                        strMPUploaderCode = clsCommon.myCstr(dt.Rows(ii)("CP_CODE")) + strMPUploaderCode
                    End If
aaa:                qry = "select TSPL_MP_MASTER.MP_Code_VLC_Uploader,TSPL_MP_MASTER.MP_Code,TSPL_MP_MASTER.MP_Name,TSPL_MP_MASTER.VLC_Code,TSPL_VLC_MASTER_HEAD.VLc_Code_vlc_uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code ,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME" + Environment.NewLine +
                    "from TSPL_MP_MASTER" + Environment.NewLine +
                    "left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code" + Environment.NewLine +
                    "left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.MCC" + Environment.NewLine +
                    "where MP_Code_VLC_Uploader='" + strMPUploaderCode + "'"
                    dtMasterData = clsDBFuncationality.GetDataTable(qry)
                    If dtMasterData Is Nothing OrElse dtMasterData.Rows.Count <= 0 Then
                        If settBennyImportAutoCreateMP Then
                            Dim obj = New clsMpMaster()
                            obj.isNewEntry = True
                            Dim dtServer As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
                            obj.MP_Code = clsERPFuncationality.GetNextCode(Nothing, dtServer, clsDocType.MPMaster, "", "")
                            If clsCommon.myLen(obj.MP_Code) <= 0 Then
                                Throw New Exception("Error In Document Code Genertion")
                            End If
                            obj.MP_CODE_VLC_UPLOADER = strMPUploaderCode
                            obj.MCC_Code = clsCommon.myCstr(dt.Rows(ii)("CP_CODE")).Replace("V0", "")
                            qry = "select VLC_Code from tspl_vlc_master_head where VLC_Code_VLC_Uploader='" + obj.MCC_Code + "'"
                            Dim dtVLCCode As DataTable = clsDBFuncationality.GetDataTable(qry)
                            If dtVLCCode Is Nothing OrElse dtVLCCode.Rows.Count <= 0 Then
                                Throw New Exception("Invalid VLC uploader Code [" + obj.MCC_Code + "]")
                            End If
                            If dtVLCCode.Rows.Count > 1 Then
                                Throw New Exception("VLC uploader Code [" + obj.MCC_Code + "] is not unique.")
                            End If
                            obj.MCC_Code = clsCommon.myCstr(dtVLCCode.Rows(0)("VLC_Code"))
                            obj.Modified_By = objCommonVar.CurrentUserCode
                            obj.Modified_Date = clsCommon.GetPrintDate(dtServer, "dd/MMM/yyyy")
                            obj.Comp_Code = objCommonVar.CurrentCompanyCode
                            If obj.isNewEntry Then
                                obj.Created_By = objCommonVar.CurrentUserCode
                                obj.Created_Date = clsCommon.GetPrintDate(dtServer, "dd/MMM/yyyy")
                            End If
                            obj.Form_Id = clsUserMgtCode.frmMPMaster
                            clsMpMaster.SaveData(obj, Nothing)
                            GoTo aaa
                        Else
                            Throw New Exception("Invalid MP uploader Code [" + clsCommon.myCstr(dt.Rows(ii)("Extended_Code")) + "]")
                        End If
                    ElseIf dtMasterData.Rows.Count > 1 Then
                        Throw New Exception("MP uploader Code [" + clsCommon.myCstr(dt.Rows(ii)("Extended_Code")) + "] is not unique.")
                    End If
                    If clsCommon.myLen(strMCCCode) > 0 Then
                        If Not clsCommon.CompairString(strMCCCode, clsCommon.myCstr(dtMasterData.Rows(0)("Mcc_Code_VLC_Uploader"))) = CompairStringResult.Equal Then
                            Throw New Exception("MCC Uploader [" + strMCCCode + "],[" + clsCommon.myCstr(dtMasterData.Rows(0)("Mcc_Code_VLC_Uploader")) + "] is not same for All MP")
                        End If
                    End If
                    strMCCCode = clsCommon.myCstr(dtMasterData.Rows(0)("Mcc_Code_VLC_Uploader"))
                    txtMccCode.Text = strMCCCode
                    txtMccCode.Tag = clsCommon.myCstr(dtMasterData.Rows(0)("Mcc"))
                    lblMccName.Text = clsCommon.myCstr(dtMasterData.Rows(0)("MCC_NAME"))
                    If Not clsCommon.CompairString(strVLCCode, clsCommon.myCstr(dtMasterData.Rows(0)("VLc_Code_vlc_uploader"))) = CompairStringResult.Equal Then
                        gvVLC.Rows.AddNew()
                        gvVLC.Rows(gvVLC.Rows.Count - 1).Cells(colSelect).Value = True
                        gvVLC.Rows(gvVLC.Rows.Count - 1).Cells(colVLCCode).Value = clsCommon.myCstr(dtMasterData.Rows(0)("VLc_Code_vlc_uploader"))
                        gvVLC.Rows(gvVLC.Rows.Count - 1).Cells(colVlcName).Value = clsCommon.myCstr(dtMasterData.Rows(0)("VLC_Name"))
                        gvVLC.Rows(gvVLC.Rows.Count - 1).Cells(colRouteNo).Value = clsCommon.myCstr(dtMasterData.Rows(0)("Route_Code"))

                        dtpDocDate.Value = clsCommon.myCDate(dt.Rows(ii)("Date"))
                        dtpDate.Value = dtpDocDate.Value
                        txtShift.Text = clsCommon.myCstr(dt.Rows(ii)("SHIFT"))
                    End If
                    strVLCCode = clsCommon.myCstr(dtMasterData.Rows(0)("VLc_Code_vlc_uploader"))
                    gvData.Rows.AddNew()
                    gvData.Rows(gvData.Rows.Count - 1).Cells(colMpId).Value = clsCommon.myCstr(dtMasterData.Rows(0)("MP_Code_VLC_Uploader"))
                    gvData.Rows(gvData.Rows.Count - 1).Cells(colMpName).Value = clsCommon.myCstr(dtMasterData.Rows(0)("MP_Name"))
                    gvData.Rows(gvData.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dt.Rows(ii)("Quantity"))
                    gvData.Rows(gvData.Rows.Count - 1).Cells(colWater).Value = clsCommon.myCdbl(dt.Rows(ii)("AWM"))
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("MILK_Type")), "COW") = CompairStringResult.Equal Then
                        gvData.Rows(gvData.Rows.Count - 1).Cells(colMilkType).Value = "C"
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("MILK_Type")), "BUFFALO") = CompairStringResult.Equal Then
                        gvData.Rows(gvData.Rows.Count - 1).Cells(colMilkType).Value = "B"
                    Else
                        gvData.Rows(gvData.Rows.Count - 1).Cells(colMilkType).Value = "M"
                    End If

                    gvData.Rows(gvData.Rows.Count - 1).Cells(colVLCCode).Value = strVLCCode

                    Dim FAT As Decimal = Math.Truncate(clsCommon.myCdbl(dt.Rows(ii)("FAT")) * 10) / 10
                    Dim SNF As Decimal = Math.Truncate(clsCommon.myCdbl(dt.Rows(ii)("SNF")) * 10) / 10

                    gvData.Rows(gvData.Rows.Count - 1).Cells(colFAT).Value = FAT
                    gvData.Rows(gvData.Rows.Count - 1).Cells(colSNF).Value = SNF
                    Dim dblRate As Decimal = clsCommon.myCdbl(dt.Rows(ii)("RATE"))
                    Dim dblAmt As Decimal = clsCommon.myCdbl(dt.Rows(ii)("Amount"))
                    If settBennyImportPickRateFromPrice Then
                        dblRate = clsEkoPro.getRateFromUploaderShiftWise(FAT, SNF, clsCommon.myCstr(dtMasterData.Rows(0)("Mcc")), clsCommon.myCstr(dtMasterData.Rows(0)("VLC_Code")), clsCommon.myCstr(dt.Rows(ii)("SHIFT")), dtpDocDate.Value, Nothing, "M", settBennyImportPickRateFromPrice)
                        dblAmt = Math.Round(dblRate * clsCommon.myCdbl(dt.Rows(ii)("Quantity")), 2, MidpointRounding.ToEven)
                    End If
                    gvData.Rows(gvData.Rows.Count - 1).Cells(colRate).Value = dblRate
                    gvData.Rows(gvData.Rows.Count - 1).Cells(colAmount).Value = dblAmt
                    gvData.Rows(gvData.Rows.Count - 1).Cells(colShift).Value = clsCommon.myCstr(dt.Rows(ii)("SHIFT"))
                    dtable.Rows.Add(strVLCCode, clsCommon.myCstr(dtMasterData.Rows(0)("MP_Code_VLC_Uploader")), clsCommon.myCdbl(dt.Rows(ii)("Quantity")), clsCommon.myCdbl(dt.Rows(ii)("FAT")), clsCommon.myCdbl(dt.Rows(ii)("SNF")), clsCommon.myCdbl(dt.Rows(ii)("AWM")), clsCommon.myCstr(gvData.Rows(gvData.Rows.Count - 1).Cells(colMilkType).Value), clsCommon.myCstr(dt.Rows(ii)("Date")), clsCommon.myCstr(dt.Rows(ii)("SHIFT")), strMCCCode, clsCommon.myCstr(dtMasterData.Rows(0)("Route_Code")), dblRate, dblAmt)
                Next
            Catch ex As Exception
                Throw New Exception("Error at Row No [" + clsCommon.myCstr(ii + 1) + "]" + Environment.NewLine + ex.Message)
            End Try
        Catch ex As Exception
            LoadBlankGridVLC()
            LoadBlankGridData()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub
    Sub loadVlcDetail()
        ResetDataTableDt()
        LoadBlankGridVLC()
        LoadBlankGridData()
        Dim mccCode As String = String.Empty
        Dim mpCode As String = String.Empty
        Dim mpName As String = String.Empty
        Dim mccName As String = String.Empty
        Dim vlcCode As String = String.Empty
        Dim vlcName As String = String.Empty
        Dim qty As Double = 0
        Dim fat As Double = 0
        Dim snf As Double = 0
        Dim Rate As Double = 0
        Dim Amount As Double = 0
        Dim milkType As String = String.Empty
        Dim water As Double = 0
        Dim shift As String = String.Empty
        Dim routeId As String = String.Empty
        Dim MPId As String = String.Empty
        Dim dt As String = String.Empty
        Dim i As Integer = 1
        Dim objReader As New System.IO.StreamReader(txtBrowse.Text)
        Dim strData As String = String.Empty
        Dim strDataPrev As String = String.Empty
        Dim strVlcNotFound As String = String.Empty
        Dim strMPNotFound As String = String.Empty
        Dim strMCCNotFound As String = String.Empty
        Do While objReader.Peek() <> -1
            If i <> 1 Then
                strDataPrev = strData
            End If
            ''RICHA AGARWAL 25/01/2016 BM00000008715
            If txtBrowse.Text.Contains(".TXT") = True Or txtBrowse.Text.Contains(".txt") = True Or txtBrowse.Text.Contains(".BIP") = True Or txtBrowse.Text.Contains(".bip") = True Then
                strData = objReader.ReadLine() + "----"
            Else
                strData = objReader.ReadLine()
            End If

            ''-----------------------


            If clsCommon.myLen(strData) <= 0 Then
                Exit Do
            End If
            If i = 1 Or clsCommon.CompairString(Microsoft.VisualBasic.Left(strDataPrev, 3), "END") = CompairStringResult.Equal Then
                vlcCode = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(strData, Microsoft.VisualBasic.Len(strData) - 4), 7)
                If clsDBFuncationality.getSingleValue("select count(*) from TSPL_VLC_MASTER_HEAD where VLc_Code_vlc_uploader='" & Microsoft.VisualBasic.Right(vlcCode, 4) & "'") = 0 Then
                    strVlcNotFound = strVlcNotFound & vlcCode & ","
                    vlcCode = ""
                End If
                mccCode = Microsoft.VisualBasic.Left(vlcCode, 3)
                If clsDBFuncationality.getSingleValue("select count(*) from TSPL_MCC_MASTER where MCC_Code_vlc_uploader='" & mccCode & "'") = 0 Then
                    strMCCNotFound = strMCCNotFound & mccCode & ","
                    mccCode = ""
                End If

                routeId = clsfrmVLCMaster.getRouteNoOnVLcCodeForVLCUplader(Microsoft.VisualBasic.Right(vlcCode, 4), mccCode, Nothing)
                mpCode = ""
                milkType = ""
                fat = 0
                snf = 0
                water = 0
                qty = 0
                Rate = 0
                Amount = 0
                If clsCommon.myLen(vlcCode) > 0 And clsCommon.myLen(mccCode) > 0 Then
                    dtable.Rows.Add(Microsoft.VisualBasic.Right(vlcCode, 4), mpCode, qty.ToString, fat.ToString, snf.ToString, water.ToString, milkType, dateofFile, shiftoffile, mccCode, routeId, Rate.ToString, Amount.ToString)
                End If
            ElseIf (Not clsCommon.CompairString(Microsoft.VisualBasic.Left(strData, 3), "END") = CompairStringResult.Equal) And clsCommon.myLen(Microsoft.VisualBasic.Right(vlcCode, 4)) > 0 And clsCommon.myLen(mccCode) > 0 Then
                strData = Microsoft.VisualBasic.Left(strData, Microsoft.VisualBasic.Len(strData) - 4)
                mpCode = Microsoft.VisualBasic.Left(strData, 3)
                If clsDBFuncationality.getSingleValue("select count(*) from TSPL_MP_MASTER where MP_Code_vlc_uploader='" & mpCode & "' and vlc_Code='" & clsfrmVLCMaster.getVLcCodeForVLCUplader(Microsoft.VisualBasic.Right(vlcCode, 4), mccCode, Nothing) & "'") = 0 Then
                    strMPNotFound = strMPNotFound & mpCode & "(" & vlcCode & ")" & ","
                    mpCode = ""
                End If
                mpName = clsMpMaster.getMPNameOnMPCodeForVLCUplader(mpCode, clsCommon.myCstr(clsfrmVLCMaster.getVLcCodeForVLCUplader(Microsoft.VisualBasic.Right(vlcCode, 4), mccCode, Nothing)), Nothing)
                milkType = Microsoft.VisualBasic.Mid(strData, 4, 1)
                fat = clsCommon.myCdbl(Microsoft.VisualBasic.Mid(strData, 5, 2) & "." & Microsoft.VisualBasic.Mid(strData, 7, 1))
                snf = clsCommon.myCdbl(Microsoft.VisualBasic.Mid(strData, 8, 2) & "." & Microsoft.VisualBasic.Mid(strData, 10, 1))
                water = clsCommon.myCdbl(Microsoft.VisualBasic.Mid(strData, 11, 2) & "." & Microsoft.VisualBasic.Mid(strData, 13, 1))
                qty = clsCommon.myCdbl(Microsoft.VisualBasic.Mid(strData, 13, 3) & "." & Microsoft.VisualBasic.Mid(strData, 16, 2))
                Rate = clsEkoPro.getRateFromUploaderShiftWise(clsCommon.myCdbl(fat), clsCommon.myCdbl(snf), clsCommon.myCstr(clsMccMaster.getMccCodeForVLCUplader(mccCode, Nothing)), clsCommon.myCstr(clsfrmVLCMaster.getVLcCodeForVLCUplader(Microsoft.VisualBasic.Right(vlcCode, 4), mccCode, Nothing)), IIf(clsCommon.myCstr(shiftoffile).ToString.Contains("M"), "M", "E"), clsCommon.myCDate(dateofFile), Nothing, "M", settBennyImportPickRateFromPrice)
                Amount = Rate * qty

                If clsCommon.myLen(Microsoft.VisualBasic.Right(vlcCode, 4)) > 0 And clsCommon.myLen(mccCode) > 0 And clsCommon.myLen(mpCode) > 0 Then
                    dtable.Rows.Add(Microsoft.VisualBasic.Right(vlcCode, 4), mpCode, qty.ToString, fat.ToString, snf.ToString, water.ToString, milkType, dateofFile, shiftoffile, mccCode, routeId, Rate, Amount)
               End If
            End If
            i = i + 1
        Loop

        txtMccCode.Text = mccCode
        txtMccCode.Tag = clsMccMaster.getMccCodeForVLCUplader(mccCode, Nothing)
        lblMccName.Text = clsMccMaster.getMccNameOnMccCodeForVLCUplader(mccCode, Nothing)
        txtShift.Text = shiftoffile
        dtpDate.Value = clsCommon.myCDate(dateofFile)
        Dim rowVlc As Integer = 0
        Dim rowData As Integer = 0
        For j As Integer = 0 To dtable.Rows.Count - 1
            If clsCommon.myLen(dtable.Rows(j)(colMpId)) <= 0 Then
                gvVLC.Rows.AddNew()
                gvVLC.Rows(rowVlc).Cells(colSelect).Value = True
                gvVLC.Rows(rowVlc).Cells(colVLCCode).Value = dtable.Rows(j)(colVLCCode)
                gvVLC.Rows(rowVlc).Cells(colVlcName).Value = clsfrmVLCMaster.getVLcNameOnVLcCodeForVLCUplader(dtable.Rows(j)(colVLCCode), mccCode, Nothing)
                gvVLC.Rows(rowVlc).Cells(colRouteNo).Value = dtable.Rows(j)(colRouteNo)
                rowVlc = rowVlc + 1
            Else
                gvData.Rows.AddNew()
                gvData.Rows(rowData).Cells(colMpId).Value = dtable.Rows(j)(colMpId)
                gvData.Rows(rowData).Cells(colMpName).Value = clsMpMaster.getMPNameOnMPCodeForVLCUplader(dtable.Rows(j)(colMpId), clsCommon.myCstr(clsfrmVLCMaster.getVLcCodeForVLCUplader(Microsoft.VisualBasic.Right(vlcCode, 4), mccCode, Nothing)), Nothing)
                gvData.Rows(rowData).Cells(colQty).Value = clsCommon.myCdbl(dtable.Rows(j)(colQty))

                gvData.Rows(rowData).Cells(colWater).Value = clsCommon.myCdbl(dtable.Rows(j)(colWater))
                gvData.Rows(rowData).Cells(colMilkType).Value = dtable.Rows(j)(colMilkType)
                gvData.Rows(rowData).Cells(colVLCCode).Value = clsfrmVLCMaster.getVLcCodeForVLCUplader(dtable.Rows(j)(colVLCCode), txtMccCode.Text, Nothing)

                gvData.Rows(rowData).Cells(colFAT).Value = clsCommon.myCdbl(dtable.Rows(j)(colFAT))
                gvData.Rows(rowData).Cells(colSNF).Value = clsCommon.myCdbl(dtable.Rows(j)(colSNF))

                gvData.Rows(rowData).Cells(colRate).Value = clsCommon.myCdbl(dtable.Rows(j)(colRate))
                gvData.Rows(rowData).Cells(colAmount).Value = clsCommon.myCdbl(dtable.Rows(j)(colAmount))
                rowData = rowData + 1
            End If
        Next

        If clsCommon.myLen(strVlcNotFound) > 0 Then
            clsCommon.MyMessageBoxShow("following VLC Code For Uploader not found in master  " & Environment.NewLine & strVlcNotFound)
        End If
        If clsCommon.myLen(strMCCNotFound) > 0 Then
            clsCommon.MyMessageBoxShow("following MCC Code For Uploader not found in master  " & Environment.NewLine & strMCCNotFound)
        End If
        If clsCommon.myLen(strMPNotFound) > 0 Then
            clsCommon.MyMessageBoxShow("following MP Code For Uploader not found in master  " & Environment.NewLine & strMPNotFound)
        End If
        ''stuti regarding memory leakage
        objReader.Close()
        objReader.Dispose()
    End Sub



    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If allowToSave() Then SaveData()
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        OpenFileDialog.FileName = ""
        OpenFileDialog.Filter = "EIP Files (*.EIP)|*.EIP|BIP Files (*.BIP)|*.BIP|BDF Files (*.BDF)|*.BDF|Text Files (*.TXT)|*.TXT"
        If OpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            txtBrowse.Text = OpenFileDialog.FileName
            Me.Tag = OpenFileDialog.SafeFileName
        Else
            Exit Sub
        End If
    End Sub



    Private Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Dim strSelectedVlc As String = String.Empty
        If gvVLC.Rows.Count > 0 Then
            For i As Integer = 0 To gvVLC.Rows.Count - 1
                If gvVLC.Rows(i).Cells(colSelect).Value = True Then
                    strSelectedVlc = strSelectedVlc & "'" & gvVLC.Rows(i).Cells(colVLCCode).Value & "',"
                End If
            Next
            'If gvVLC.CurrentRow.Cells(colSelect).Value = True Then
            '    strSelectedVlc = strSelectedVlc & "'" & gvVLC.CurrentRow.Cells(colVLCCode).Value & "',"
            'Else
            '    strSelectedVlc = strSelectedVlc.Replace("'" & gvVLC.CurrentRow.Cells(colVLCCode).Value & "',", "")
            'End If
            If clsCommon.myLen(strSelectedVlc) > 0 Then
                strSelectedVlc = Microsoft.VisualBasic.Left(strSelectedVlc, Microsoft.VisualBasic.Len(strSelectedVlc) - 1)
            Else
                LoadBlankGridData()
                Exit Sub
            End If

            dtable.DefaultView.RowFilter = ""
            dtable.DefaultView.RowFilter = colVLCCode & " in ( " & strSelectedVlc & ")"
            LoadBlankGridData()
            Dim rowGv As Integer = 0


            For j As Integer = 0 To (dtable.DefaultView.ToTable).Rows.Count - 1
                If clsCommon.myLen(dtable.Rows(j)(colMpId)) <= 0 Then
                Else

                    gvData.Rows.AddNew()
                    gvData.Rows(rowGv).Cells(colMpId).Value = (dtable.DefaultView.ToTable).Rows(j)(colMpId)
                    gvData.Rows(rowGv).Cells(colMpName).Value = clsMpMaster.getMPNameOnMPCodeForVLCUplader((dtable.DefaultView.ToTable).Rows(j)(colMpId), clsfrmVLCMaster.getVLcCodeForVLCUplader((dtable.DefaultView.ToTable).Rows(j)(colVLCCode), (dtable.DefaultView.ToTable).Rows(j)(colMccId), Nothing), Nothing)
                    gvData.Rows(rowGv).Cells(colQty).Value = clsCommon.myCdbl((dtable.DefaultView.ToTable).Rows(j)(colQty))
                    gvData.Rows(rowGv).Cells(colFAT).Value = clsCommon.myCdbl((dtable.DefaultView.ToTable).Rows(j)(colFAT))
                    gvData.Rows(rowGv).Cells(colSNF).Value = clsCommon.myCdbl((dtable.DefaultView.ToTable).Rows(j)(colSNF))
                    gvData.Rows(rowGv).Cells(colRate).Value = clsCommon.myCdbl((dtable.DefaultView.ToTable).Rows(j)(colRate))
                    gvData.Rows(rowGv).Cells(colAmount).Value = clsCommon.myCdbl((dtable.DefaultView.ToTable).Rows(j)(colAmount))
                    gvData.Rows(rowGv).Cells(colWater).Value = clsCommon.myCdbl((dtable.DefaultView.ToTable).Rows(j)(colWater))
                    gvData.Rows(rowGv).Cells(colMilkType).Value = (dtable.DefaultView.ToTable).Rows(j)(colMilkType)
                    rowGv = rowGv + 1
                End If
            Next

        End If
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        For i As Integer = 0 To gvVLC.Rows.Count - 1
            gvVLC.Rows(i).Cells(colSelect).Value = True
        Next
    End Sub

    Private Sub fndDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocNo._MYNavigator
        LoadData(fndDocNo.Value, NavType)
    End Sub

    Private Sub fndDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocNo._MYValidating
        fndDocNo.Value = clsVlcDataUploader.getFinder("", fndDocNo.Value, isButtonClicked)
        LoadData(fndDocNo.Value, NavigatorType.Current)
    End Sub

    'Private Sub gvData_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvData.CellValueChanged
    '    Try
    '        If IsinsideLoadData = False Then
    '            IsinsideLoadData = True
    '            If e.Column Is gvData.Columns(colFAT) Or e.Column Is gvData.Columns(colSNF) Then
    '                If clsCommon.myCdbl(gvData.CurrentRow.Cells(colFAT).Value) > 0 And clsCommon.myCdbl(gvData.CurrentRow.Cells(colSNF).Value) > 0 Then
    '                    gvData.CurrentRow.Cells(colRate).Value = clsEkoPro.getRateFromUploaderShiftWise(clsCommon.myCdbl(gvData.CurrentRow.Cells(colFAT).Value), clsCommon.myCdbl(gvData.CurrentRow.Cells(colSNF).Value), clsCommon.myCstr(txtMccCode.Tag), clsCommon.myCstr(gvData.CurrentRow.Cells(colVLCCode).Value), IIf(clsCommon.myCstr(txtShift.Text).ToString.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value))
    '                    gvData.CurrentRow.Cells(colAmount).Value = clsCommon.myCdbl(gvData.CurrentRow.Cells(colRate).Value) * clsCommon.myCdbl(gvData.CurrentRow.Cells(colQty).Value)
    '                End If
    '            End If
    '            IsinsideLoadData = False
    '        End If

    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.ToString)
    '    End Try
    'End Sub

    'Sanjay Ticket No- ERO/29/05/19-000627 ,Add Excel PDF & Export,Total in Data Grid
    Private Sub QExcel_Click(sender As Object, e As EventArgs) Handles QExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmVlcdataUploadar & "'"))
            arrHeader.Add("Doc No : " + fndDocNo.Value)
            arrHeader.Add("Doc Date : " + clsCommon.GetPrintDate(dtpDocDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("MCC Name: " + lblMccName.Text)
            arrHeader.Add("Date : " + clsCommon.GetPrintDate(dtpDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Shift : " + txtShift.Text)
            transportSql.QuickExportToExcel(gvData, "", Me.Text, , arrHeader)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub QPDF_Click(sender As Object, e As EventArgs) Handles QPDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmVlcdataUploadar & "'"))
            arrHeader.Add("Doc No : " + fndDocNo.Value)
            arrHeader.Add("Doc Date : " + clsCommon.GetPrintDate(dtpDocDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("MCC Name: " + lblMccName.Text)
            arrHeader.Add("Date : " + clsCommon.GetPrintDate(dtpDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Shift : " + txtShift.Text)
            clsCommon.MyExportToPDF(Me.Text, gvData, arrHeader, Me.Text, MyBase.Form_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub multipleDelteVisible(ByVal val As Boolean)
        MyLabel34.Visible = val
        TxtMultiSelectFinder3.Visible = val
        RadButton238.Visible = val
    End Sub

    Private Sub TxtMultiSelectFinder3__My_Click(sender As Object, e As EventArgs) Handles TxtMultiSelectFinder3._My_Click
        Dim qry As String = "select * from (" + Environment.NewLine + _
        "select Doc_No as Code ,Doc_Date,MCC_Code,shift from tspl_vlc_data_uploader group by Doc_No,Doc_Date,MCC_Code,shift" + Environment.NewLine + _
        ")xx" + Environment.NewLine + _
        "where not exists(select 1  from TSPL_LOCK_MP_PC where  Convert(date,xx.Doc_Date,103)  between Cast(TSPL_LOCK_MP_PC.FROM_DATE as Date)   and Cast(TSPL_LOCK_MP_PC.TO_DATE as Date) and TSPL_LOCK_MP_PC.MCC_Code =xx.MCC_Code And TSPL_LOCK_MP_PC.POSTED = 'Y')"
        TxtMultiSelectFinder3.arrValueMember = clsCommon.ShowMultipleSelectForm("VLDU@MulD", qry, "Code", "Code", TxtMultiSelectFinder3.arrValueMember, TxtMultiSelectFinder3.arrDispalyMember)
    End Sub

    Private Sub RadButton238_Click(sender As Object, e As EventArgs) Handles RadButton238.Click
        Try
            If TxtMultiSelectFinder3.arrValueMember Is Nothing OrElse TxtMultiSelectFinder3.arrValueMember.Count <= 0 Then
                Throw New Exception("Please select at least one document to delete")
            End If

            If (clsCommon.MyMessageBoxShow("Delete " + clsCommon.myCstr(TxtMultiSelectFinder3.arrValueMember.Count) + "Documents." + Environment.NewLine + "Are You sure?", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question)) = System.Windows.Forms.DialogResult.Yes Then
                clsVlcDataUploader.deleteData(TxtMultiSelectFinder3.arrValueMember)
                If (ClsVLCDataUploaderManual.DeleteData(TxtMultiSelectFinder3.arrValueMember)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    TxtMultiSelectFinder3.arrValueMember = Nothing
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnFolder_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnFolder.ToggleStateChanged, rbtnFile.ToggleStateChanged
        If rbtnFile.IsChecked Then
            SplitContainer5.Panel2Collapsed = True
            SplitContainer5.Panel1Collapsed = False
        Else
            SplitContainer5.Panel2Collapsed = False
            SplitContainer5.Panel1Collapsed = True
        End If
    End Sub

    Private Sub btnFolderBrowse_Click(sender As Object, e As EventArgs) Handles btnFolderBrowse.Click
        Try
            txtFoderPath.Text = ""
            FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
            FolderBrowserDialog1.ShowNewFolderButton = False
            If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
                txtFoderPath.Text = FolderBrowserDialog1.SelectedPath
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub GetDirectories(ByVal StartPath As String, ByRef DirectoryList As ArrayList, ByRef arrPat As List(Of String))
        Dim Dirs() As String = Directory.GetDirectories(StartPath)
        DirectoryList.AddRange(Dirs)
        For Each Dir As String In Dirs
            GetDirectories(Dir, DirectoryList, arrPat)
        Next
        For Each item As String In DirectoryList
            If Not arrPat.Contains(item) Then
                arrPat.Add(item)
            End If
        Next
    End Sub

    'Private Sub btnFolderGo_Click(sender As Object, e As EventArgs) Handles btnFolderGo.Click
    '    Try
    '        If clsCommon.myLen(txtFoderPath.Text) > 0 Then
    '            Dim DirList As New ArrayList
    '            Dim arrPat As New List(Of String)
    '            arrPat.Add(txtFoderPath.Text)
    '            GetDirectories(txtFoderPath.Text, DirList, arrPat)
    '            Dim x As Integer = 0
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub
    Private Sub btnFolderGo_Click(sender As Object, e As EventArgs) Handles btnFolderGo.Click
        Dim logFile As String = "ProDataErrorLog.txt"
        clsCommon.ProgressBarUpdate("Checking for log file...")
        If System.IO.File.Exists(logFile) Then
            Dim stream As New IO.StreamWriter(logFile, False)
            stream.WriteLine("")
            stream.Close()
        Else
            Dim fs As IO.FileStream = System.IO.File.Create(logFile)
            fs.Close()
        End If
        Dim exc As String = ""
        Dim flag As Boolean = True
        Try
            Dim arrPat As New List(Of String)
            If clsCommon.myLen(txtFoderPath.Text) > 0 Then
                Dim DirList As New ArrayList
                arrPat.Add(txtFoderPath.Text)
                GetDirectories(txtFoderPath.Text, DirList, arrPat)
            End If
            clsCommon.ProgressBarPercentShow()
            For cntFolder As Integer = 0 To arrPat.Count - 1
                Try
                    If clsCommon.myLen(arrPat(cntFolder)) > 0 Then
                        Dim strFileSize As String = ""
                        Dim di As New IO.DirectoryInfo(arrPat(cntFolder))
                        Dim aryFi As IO.FileInfo() = di.GetFiles("*.BDF")
                        Dim fi As IO.FileInfo
                        Dim ii As Integer = 0
                        Dim Total As Integer = aryFi.Count
                        'clsCommon.ProgressBarPercentUpdate(((cntFolder + 1) * 100 / (arrPat.Count)), "Folder [" & clsCommon.myCstr(cntFolder + 1) & "/" & clsCommon.myCstr(arrPat.Count) & "]")
                        For Each fi In aryFi
                            Try
                                ii += 1
                                clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Folder [" & clsCommon.myCstr(cntFolder + 1) & "/" & clsCommon.myCstr(arrPat.Count) & "]  Files [" & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "]")
                                flag = False
                                btnReset.PerformClick()
                                txtBrowse.Text = fi.FullName
                                btnGo.PerformClick()
                                btnSave.PerformClick()
                            Catch ex As Exception
                                exc += Environment.NewLine + "Error in File " + arrPat(cntFolder) + "\" + fi.Name + Environment.NewLine + ex.Message
                            End Try
                        Next

                    End If
                Catch ex1 As Exception
                End Try
            Next
            clsCommon.ProgressBarPercentHide()
            If flag Then
                clsCommon.MyMessageBoxShow(Me, "No File Found to Imported", Me.Text)
            ElseIf clsCommon.myLen(exc) > 0 Then
                Dim objWriter As New System.IO.StreamWriter(logFile, True)
                objWriter.WriteLine(exc)
                objWriter.Close()
                Throw New Exception("Not All files imported.")
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Imported Successfully", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Dim objreader As New System.IO.StringReader(logFile)
            If objreader IsNot Nothing AndAlso clsCommon.myLen(objreader) > 0 Then
                Dim str As String = clsCommon.myCstr(System.IO.File.ReadAllText(logFile))
                If clsCommon.myLen(str) > 0 Then
                    System.Diagnostics.Process.Start(logFile)
                End If
            End If
        Finally
            btnReset.PerformClick()
        End Try
    End Sub
End Class

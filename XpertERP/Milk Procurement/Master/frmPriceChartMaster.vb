'--------Created By Monika 29/05/2014
'-----------------BM00000003159----------Monika
Imports common
Imports System.Data.SqlClient
Public Class FrmPriceChartMaster
    Inherits FrmMainTranScreen
    ''by balwinder on BM00000009672 for back calculation.
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ErrorControl As clsErrorControl = New clsErrorControl()
    Dim formtype As String = Nothing
    Dim isNewEntry As Boolean = True
    Dim MilkPricePostedData As Boolean
    Dim strPricChartPass As String = Nothing
    Dim isCLRInsteadOfSNF As Boolean = False
    Dim dclCorrectionFactor As Decimal = 0

    Public Const colPer As String = "colPer"
    Public Const colAmt As String = "colAmt"
#End Region

#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        formtype = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub FrmPriceChartMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        If formtype = "PCM_BLK-MST" Then
            formtype = "BULK"
            Panel1.Visible = False
        Else
            formtype = "MCC"
            Panel1.Visible = True
        End If
        LoadCombobox()
        Reset()
        gvSNFDed.Rows.AddNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
        dclCorrectionFactor = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
        If clsCommon.CompairString(clsUserMgtCode.frmPriceChartMaster, Me.Form_ID) = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, Nothing)) > 0 Then
                isCLRInsteadOfSNF = True
            End If
        End If
        If isCLRInsteadOfSNF Then
            txtsnf.Enabled = False
            SplitContainer3.FixedPanel = FixedPanel.Panel1
            SplitContainer3.IsSplitterFixed = True
        Else
            SplitContainer3.Panel2Collapsed = True
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        If formtype = "PCM_BLK-MST" Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmPriceChartMaster_Bulk)
            strPricChartPass = clsUserMgtCode.frmPriceChartMaster_Bulk
        Else
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmPriceChartMaster)
            strPricChartPass = clsUserMgtCode.frmPriceChartMaster
        End If

        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
            End If

            Me.Close()
            Exit Sub
        End If
        MilkPricePostedData = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowMilkItemMasterPostedData, clsFixedParameterCode.AllowMilkItemMasterPostedData, Nothing)) = 1, True, False)
        btnPost.Visible = MyBase.isPostFlag AndAlso MilkPricePostedData
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        'RadMenu1.Visible = MyBase.isExport
        If MyBase.isExport = True Then
            btnimport.Enabled = True
            btnexport.Enabled = True
        Else
            btnimport.Enabled = False
            btnexport.Enabled = False
        End If
    End Sub

    Sub Reset()
        btnsave.Enabled = True
        btnPost.Enabled = False

        cmbaxis.Text = "None"
        cmbmatrix.Text = "None"
        cmbratetype.SelectedValue = "Effective Rate" 'cmbratetype.SelectedValue = ""
        txtdeclrd_rate.Text = ""
        txteffctv_rate.Text = ""
        txtsnf_ratio.Text = ""
        fndcode.Value = ""
        txtname.Text = ""
        txtratio.Text = ""
        txtfat.Text = ""
        txtsnf.Text = ""
        txtrate.Text = ""
        txtCLR.Value = 0
        txtefctdate.Text = clsCommon.GETSERVERDATE()
        txtinactvdate.Text = clsCommon.GETSERVERDATE().AddDays(7)
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        isNewEntry = True
        fndcode.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        loadBlankGrid(gvSNFDed)
    End Sub

    Private Function GetDedution(ByVal strFATOrSNF As String, ByVal gridView As common.UserControls.MyRadGridView) As Dictionary(Of Decimal, Decimal)
        Dim dclPer As Decimal
        Dim dclValue As Decimal
        Dim ArrDed As New Dictionary(Of Decimal, Decimal)
        For ii As Integer = 0 To gridView.RowCount - 1
            dclPer = Math.Round(clsCommon.myCdbl(gridView.Rows(ii).Cells(colPer).Value), 1, MidpointRounding.ToEven)
            dclValue = Math.Round(clsCommon.myCdbl(gridView.Rows(ii).Cells(colAmt).Value), 2, MidpointRounding.ToEven)
            If dclPer > 0 AndAlso dclValue > 0 Then
                If ArrDed.ContainsKey(dclPer) Then
                    Throw New Exception("Repeated " + strFATOrSNF + " Percentage -" + clsCommon.myCstr(dclPer) + " At Row no -" + clsCommon.myCstr(ii))
                Else
                    ArrDed.Add(dclPer, dclValue)
                End If
            End If
        Next
        Return ArrDed
    End Function

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtname.Text) <= 0 Then
                txtname.Focus()
                txtname.Select()
                ErrorControl.SetError(txtname, "Description Of Milk Price Is Mandatory")
                Throw New Exception("Please Fill Description Of Milk Price Master")
            Else
                ErrorControl.ResetError(txtname)
            End If

            If clsCommon.myLen(txtefctdate.Text) <= 0 Then
                txtefctdate.Focus()
                txtefctdate.Select()
                ErrorControl.SetError(txtefctdate, "Effective Date Is Mandatory")
                Throw New Exception("Enter Effective Date")
            Else
                ErrorControl.ResetError(txtefctdate)
            End If

            If clsCommon.myLen(txtinactvdate.Text) > 0 Then
                If Not clsCommon.myCDate(txtinactvdate.Text) >= clsCommon.myCDate(txtefctdate.Text) Then
                    txtinactvdate.Focus()
                    txtinactvdate.Select()
                    ErrorControl.SetError(txtinactvdate, "Inactive Date Should Be Greater Or Equal To Effective Date")
                    Throw New Exception("Inactive Date Should Be Greater Or Equal To Effective Date")
                Else
                    ErrorControl.ResetError(txtinactvdate)
                End If
            End If

            ''richa agarwal 27 Sep, 2016 BM00000009819
            'Dim strpricecode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select distinct TSPL_MILK_PRICE_MASTER.Price_Code from TSPL_MILK_PRICE_MASTER left outer join TSPL_FAT_SNF_UPLOADER_MASTER on TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code =TSPL_MILK_PRICE_MASTER.Price_Code left outer join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.Price_Code =TSPL_FAT_SNF_UPLOADER_MASTER.Code where TSPL_MILK_PRICE_MASTER.Price_Code ='" & clsCommon.myCstr(fndcode.Value) & "' and FAT_Pers =" & clsCommon.myCdbl(txtfat.Value) & " and SNF_Pers =" & clsCommon.myCdbl(txtsnf.Value) & " and ISNULL(TSPL_MILK_SAMPLE_DETAIL.Price_Code,'')<>''"))
            'If clsCommon.myLen(strpricecode) > 0 Then
            '    If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select Effective_Date  from TSPL_MILK_PRICE_MASTER where Price_Code ='" & clsCommon.myCstr(fndcode.Value) & "'")) <> clsCommon.myCDate(txtefctdate.Text) Then
            '        ErrorControl.SetError(txtefctdate, "Effective Date cannot be updated because it is used on price chart.")
            '        Throw New Exception("Effective Date cannot be updated because it is used on price chart.")
            '    Else
            '        ErrorControl.ResetError(txtefctdate)
            '    End If
            'End If

            'If formtype = "MCC" AndAlso clsCommon.CompairString(cmbratetype.SelectedValue, "") = CompairStringResult.Equal Then
            '    'cmbratetype.Focused()
            '    cmbratetype.Select()
            '    ErrorControl.SetError(cmbratetype, "Please Select Rate Type")
            '    Throw New Exception("Please Select Rate Type")
            'Else
            '    ErrorControl.ResetError(cmbratetype)
            'End If

            'If formtype = "MCC" AndAlso clsCommon.CompairString(cmbaxis.Text, "None") = CompairStringResult.Equal Then
            '    cmbaxis.Select()
            '    ErrorControl.SetError(cmbaxis, "Please Select Axis Type Single/Double")
            '    Throw New Exception("Please Select Axis Type Single/Double")
            'Else
            '    ErrorControl.ResetError(cmbaxis)
            'End If

            'If formtype = "MCC" AndAlso clsCommon.CompairString(cmbmatrix.Text, "None") = CompairStringResult.Equal Then
            '    cmbmatrix.Select()
            '    ErrorControl.SetError(cmbmatrix, "Please Select Matrix Type")
            '    Throw New Exception("Please Select Matrix Type")
            'Else
            '    ErrorControl.ResetError(cmbmatrix)
            'End If

            If clsCommon.myCdbl(txtdeclrd_rate.Text) <= 0 Then
                txtdeclrd_rate.Focus()
                txtdeclrd_rate.Select()
                ErrorControl.SetError(txtdeclrd_rate, "Please Fill Declared Rate")
                Throw New Exception("Please Fill Declared Rate")
            Else
                ErrorControl.ResetError(txtdeclrd_rate)
            End If

            If clsCommon.myCdbl(txtdeclrd_rate.Text) < clsCommon.myCdbl(txtrate.Text) Then
                txtdeclrd_rate.Focus()
                txtdeclrd_rate.Select()
                ErrorControl.SetError(txtdeclrd_rate, "Declared Rate should be Greater then or Equal to Effective Rate.")
                Throw New Exception("Declared Rate should be Greater then or Equal to Effective Rate.")
            Else
                ErrorControl.ResetError(txtdeclrd_rate)
            End If
            'If clsCommon.myCdbl(txteffctv_rate.Text) <= 0 Then
            '    txteffctv_rate.Focus()
            '    txteffctv_rate.Select()
            '    ErrorControl.SetError(txteffctv_rate, "Please Fill Effective Rate")
            '    Throw New Exception("Please Fill Effective Rate")
            'Else
            '    ErrorControl.ResetError(txteffctv_rate)
            'End If

            If clsCommon.myCdbl(txtratio.Text) < 0 Then
                txtratio.Focus()
                txtratio.Select()
                ErrorControl.SetError(txtratio, "FAT Ratio Can not be -ve")
                Throw New Exception("FAT Ratio Can not be -ve")
            Else
                ErrorControl.ResetError(txtratio)
            End If
            If clsCommon.myCdbl(txtsnf_ratio.Text) < 0 Then
                txtsnf_ratio.Focus()
                txtsnf_ratio.Select()
                ErrorControl.SetError(txtsnf_ratio, "SNF Ratio Can not be -ve")
                Throw New Exception("SNF Ratio Can not be -ve")
            Else
                ErrorControl.ResetError(txtratio)
            End If
            If (clsCommon.myCdbl(txtratio.Text) + clsCommon.myCdbl(txtsnf_ratio.Text)) <> 100 Then
                txtratio.Focus()
                txtratio.Select()
                ErrorControl.SetError(txtratio, "Please Fill Ratio Of SNF And FAT" + Environment.NewLine + "There Sum Should be Equal To 100")
                Throw New Exception("Please Fill Ratio Of SNF And FAT." + Environment.NewLine + "There Sum Should be Equal To 100")
            Else
                ErrorControl.ResetError(txtratio)
            End If

            If clsCommon.myLen(txtfat.Text) <= 0 Then
                txtfat.Focus()
                txtfat.Select()
                ErrorControl.SetError(txtfat, "Please Fill FAT Percentage")
                Throw New Exception("Please Fill FAT Percentage")
            Else
                ErrorControl.ResetError(txtfat)
            End If
            If isCLRInsteadOfSNF Then
                If clsCommon.myLen(txtCLR.value) <= 0 Then
                    txtCLR.Focus()
                    txtCLR.Select()
                    ErrorControl.SetError(txtCLR, "Please Fill CLR")
                    Throw New Exception("Please Fill CLR")
                Else
                    ErrorControl.ResetError(txtCLR)
                End If
            End If

            If clsCommon.myLen(txtsnf.Text) <= 0 Then
                txtsnf.Focus()
                txtsnf.Select()
                ErrorControl.SetError(txtsnf, "Please Fill SNF Percentage")
                Throw New Exception("Please Fill SNF Percentage")
            Else
                ErrorControl.ResetError(txtsnf)
            End If

            If clsCommon.myLen(txtrate.Text) <= 0 Then
                txtrate.Focus()
                txtrate.Select()
                ErrorControl.SetError(txtrate, "Please Fill Milk Rate")
                Throw New Exception("Please Fill Milk Rate")
            Else
                ErrorControl.ResetError(txtrate)
            End If

            Dim ArrSNFDed As Dictionary(Of Decimal, Decimal) = GetDedution("SNF", gvSNFDed)

            CalculateSNFFromCLR()
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Sub SaveData()
        Try

            If clsCommon.myLen(strPricChartPass) > 0 Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(strPricChartPass, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
            End If

            Dim obj As New clsfrmPriceChartMaster()

            obj.code = clsCommon.myCstr(fndcode.Value)
            obj.desc = clsCommon.myCstr(txtname.Text.Replace("'", "`"))
            obj.effct_dt = (txtefctdate.Text)

            If txtinactvdate.Text IsNot Nothing AndAlso clsCommon.myLen(txtinactvdate.Text) <= 0 Then
                txtinactvdate.Text = clsCommon.myCDate(txtefctdate.Text).AddDays(7) 'clsCommon.GETSERVERDATE()
            End If

            obj.inactv_dt = (txtinactvdate.Text)

            obj.ratio = clsCommon.myCstr(txtratio.Text)
            obj.snf_ratio = clsCommon.myCstr(txtsnf_ratio.Text)
            obj.fat_pers = clsCommon.myCdbl(txtfat.Text)
            obj.CLR = txtCLR.Value
            obj.snf_pers = clsCommon.myCdbl(txtsnf.Text)
            obj.rate = clsCommon.myCdbl(txtrate.Text)
            obj.ratetype = clsCommon.myCstr(cmbratetype.SelectedValue)
            obj.declrd_rate = clsCommon.myCdbl(txtdeclrd_rate.Text)
            obj.effctv_rate = clsCommon.myCdbl(txteffctv_rate.Text)
            obj.axis = clsCommon.myCstr(cmbaxis.Text)
            obj.matrix = clsCommon.myCstr(cmbmatrix.Text)
            obj.price_type = formtype

            obj.arrSNFDed = New List(Of clsPriceChartSNFDed)
            For ii As Integer = 0 To gvSNFDed.RowCount - 1
                Dim objTS As New clsPriceChartSNFDed
                objTS.Per = clsCommon.myCdbl(gvSNFDed.Rows(ii).Cells(colPer).Value)
                objTS.Amount = clsCommon.myCdbl(gvSNFDed.Rows(ii).Cells(colAmt).Value)
                If objTS.Amount > 0 AndAlso objTS.Per > 0 Then
                    obj.arrSNFDed.Add(objTS)
                End If
            Next


            If clsfrmPriceChartMaster.SaveData(obj.code, isNewEntry, obj) Then
                If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                End If
                fndcode.Value = obj.code
                fndcode.MyReadOnly = True
                btnsave.Text = "Update"
                btndelete.Enabled = True
                btnPost.Enabled = True
                UcAttachment1.SaveData(fndcode.Value)
                If Not clsCommon.MyMessageBoxShow("Want To Maintained Milk Price History?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    Return
                End If

                If clsfrmPriceChartMaster.HistoryData(obj.code, formtype) Then
                    clsCommon.MyMessageBoxShow(Me, "History Saved Successfully", Me.Text)
                End If

            Else
                fndcode.MyReadOnly = False
                btnsave.Text = "Save"
                btndelete.Enabled = False
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub
    '' against [BM00000009598] 
    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()

    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim desc As String = ""
            If (myMessages.postConfirm()) Then

                If (clsfrmPriceChartMaster.PostData(MyBase.Form_ID, fndcode.Value)) Then
                    msg = "Successfully Posted"
                Else

                End If
                common.clsCommon.MyMessageBoxShow(msg)
                LoadData(fndcode.Value, NavigatorType.Current)


            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadCombobox()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Declared Rate"
        dr("Name") = "Declared Rate"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Effective Rate"
        dr("Name") = "Effective Rate"
        dt.Rows.Add(dr)

        cmbratetype.DataSource = Nothing
        cmbratetype.DataSource = dt
        cmbratetype.DisplayMember = "Name"
        cmbratetype.ValueMember = "Code"
    End Sub

    Private Sub FrmPriceChartMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            btnnew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Dim qry As String = ""
        If clsCommon.myLen(fndcode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select First Price Code For Deletion", Me.Text)
            fndcode.Focus()
            fndcode.Select()
            Return
        Else
            qry = "select count(*) from tspl_milk_price_master where price_code='" + fndcode.Value + "' and Price_Type='" + formtype + "'"
            Dim check As Integer = CInt(clsDBFuncationality.getSingleValue(qry))

            If check <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Filled Price Code Not Found For Deletion", Me.Text)
                fndcode.Focus()
                fndcode.Select()
                Return
            End If
        End If

        If Not clsCommon.MyMessageBoxShow("Are You Sure,Want To Delete The Milk Price Code " + clsCommon.myCstr(fndcode.Value) + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            Return
        End If

        Try
            qry = "delete from TSPL_MILK_PRICE_MASTER where price_code='" + clsCommon.myCstr(fndcode.Value) + "' and Price_Type='" + formtype + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)

            clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    'Private Sub txtfat_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
    '    Try
    '        If clsCommon.myLen(txtfat.Text) > 0 Then
    '            Convert.ToDecimal(txtfat.Text)
    '        End If
    '    Catch ex As Exception
    '        txtfat.Text = "0.00"
    '        txtfat.Focus()
    '        txtfat.Select()
    '        clsCommon.MyMessageBoxShow("Enter Numeric Value Only.", Me.Text)
    '    End Try
    'End Sub

    'Private Sub txtsnf_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
    '    Try
    '        If clsCommon.myLen(txtsnf.Text) > 0 Then
    '            Convert.ToDecimal(txtsnf.Text)
    '        End If
    '    Catch ex As Exception
    '        txtsnf.Text = "0.00"
    '        txtsnf.Focus()
    '        txtsnf.Select()
    '        clsCommon.MyMessageBoxShow("Enter Numeric Value Only.", Me.Text)
    '    End Try
    'End Sub

    'Private Sub txtrate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
    '    Try
    '        If clsCommon.myLen(txtrate.Text) > 0 Then
    '            Convert.ToDecimal(txtrate.Text)
    '        End If
    '    Catch ex As Exception
    '        txtrate.Text = "0.00"
    '        txtrate.Focus()
    '        txtrate.Select()
    '        clsCommon.MyMessageBoxShow("Enter Numeric Value Only.", Me.Text)
    '    End Try
    'End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try

            Dim obj As clsfrmPriceChartMaster = clsfrmPriceChartMaster.GetData(strCode, NavType, formtype)
            isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.code) > 0 Then
                isNewEntry = False
                fndcode.Value = obj.code
                txtname.Text = obj.desc
                txtratio.Text = obj.ratio
                txtfat.Text = obj.fat_pers
                txtsnf.Text = obj.snf_pers
                txtCLR.Value = obj.CLR
                txtrate.Text = obj.rate
                txtefctdate.Text = obj.effct_dt
                txtinactvdate.Text = obj.inactv_dt
                cmbratetype.SelectedValue = obj.ratetype
                txtdeclrd_rate.Text = obj.declrd_rate
                txteffctv_rate.Text = obj.effctv_rate
                cmbaxis.Text = obj.axis
                cmbmatrix.Text = obj.matrix
                txtsnf_ratio.Text = obj.snf_ratio
                loadBlankGrid(gvSNFDed)
                If obj.arrSNFDed IsNot Nothing AndAlso obj.arrSNFDed.Count > 0 Then
                    For Each objts As clsPriceChartSNFDed In obj.arrSNFDed
                        gvSNFDed.Rows.AddNew()
                        gvSNFDed.Rows(gvSNFDed.Rows.Count - 1).Cells(colPer).Value = objts.Per
                        gvSNFDed.Rows(gvSNFDed.Rows.Count - 1).Cells(colAmt).Value = objts.Amount
                    Next
                End If
                gvSNFDed.Rows.AddNew()

                UcAttachment1.LoadData(fndcode.Value)

                btnsave.Text = "Update"
                btndelete.Enabled = True
                fndcode.MyReadOnly = True
                If (clsCommon.myCdbl(obj.Posted)) = 1 Then
                    btnPost.Enabled = False
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                Else
                    btnPost.Enabled = True
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                End If
                'UcAttachment1.SaveData(fndcode.Value)
            Else
                Reset()
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndcode._MYNavigator
        LoadData(clsCommon.myCstr(fndcode.Value), NavType)
    End Sub

    Private Sub fndcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcode._MYValidating
        Dim qry As String = ""
        If formtype = "MCC" Then
            qry = "select price_code as Code,Description,Rate_Type as [Rate Type],axis_type as [Axis Type],matrix_type as [Matrix Type],Effective_Date as [Effective Date],Inactive_Date as [Inactive Date],Ratio as [FAT Ratio],snf_ratio as [SNF Ratio],FAT_Pers as [FAT %],SNF_Pers as [SNF %],Milk_Rate as [Milk Rate],Created_By as [Created By],Created_Date as [Created Date],Modified_By as [Modified By],Modified_Date as [Modified Date],case isnull(Posted,0) when '0' Then 'Pending' When '1' Then 'Approved' else '' end as 'Status' from TSPL_MILK_PRICE_MASTER"
        Else
            qry = "select price_code as Code,Description,Effective_Date as [Effective Date],Inactive_Date as [Inactive Date],Ratio as [FAT Ratio],snf_ratio as [SNF Ratio],FAT_Pers as [FAT %],SNF_Pers as [SNF %],Milk_Rate as [Milk Rate],Created_By as [Created By],Created_Date as [Created Date],Modified_By as [Modified By],Modified_Date as [Modified Date],case isnull(Posted,0) when '0' Then 'Pending' else 'Approved' end as 'Status' from TSPL_MILK_PRICE_MASTER"
        End If
        Dim whrcls As String = " Price_Type='" + formtype + "'"
        fndcode.Value = clsCommon.ShowSelectForm("PRCFND", qry, "Code", whrcls, fndcode.Value, "Code", isButtonClicked)

        If clsCommon.myLen(fndcode.Value) > 0 Then
            LoadData(fndcode.Value, NavigatorType.Current)
        Else
            Reset()
        End If
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Dim qry As String = "select count(*) from tspl_milk_price_master where price_type='" + formtype + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        Dim whrcls As String = ""

        If formtype = "MCC" Then
            If check > 0 Then
                qry = "select price_code as Code,Description,Rate_Type as [Rate Type],axis_type as [Axis Type],matrix_type as [Matrix Type],Convert (varchar,Effective_Date,103)  as [Effective Date],convert (varchar,Inactive_Date,103) as [Inactive Date],Ratio as [FAT Ratio],snf_ratio as [SNF Ratio],FAT_Pers as [FAT %],SNF_Pers as [SNF %],Milk_Rate as [Milk Rate],Declared_Rate as [Declared Rate],price_type as [Price_Type] from TSPL_MILK_PRICE_MASTER "
                whrcls = " and [price_type]='" + formtype + "'"
            Else
                qry = "select '' as Code,'' as Description,'' as [Rate Type],'' as [Axis Type],'' as [Matrix Type],'' as [Effective Date],'' as [Inactive Date],0 as [FAT Ratio],0 as [SNF Ratio],0 as [FAT %],0 as [SNF %],0 as [Milk Rate],0 as [Declared Rate],'MCC' as [Price_Type]"
                whrcls = ""
            End If
        Else
            If check > 0 Then
                qry = "select price_code as Code,Description,Effective_Date as [Effective Date],Inactive_Date as [Inactive Date],Ratio as [FAT Ratio],snf_ratio as [SNF Ratio],FAT_Pers as [FAT %],SNF_Pers as [SNF %],Milk_Rate as [Milk Rate],Declared_Rate as [Declared Rate],price_type as [Price_Type] from TSPL_MILK_PRICE_MASTER "
                whrcls = " and [price_type]='" + formtype + "'"
            Else
                qry = "select '' as Code,'' as Description,'' as [Effective Date],'' as [Inactive Date],0 as [FAT Ratio],0 as [SNF Ratio],0 as [FAT %],0 as [SNF %],0 as [Milk Rate],0 as [Declared Rate],'BULK' as [Price_Type]"
                whrcls = ""
            End If
        End If
        ListImpExpColumnsMandatory = New List(Of String)({"Description", "Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Code"})
        transportSql.ExporttoExcel(qry, whrcls, "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If formtype = "MCC" Then
            If transportSql.importExcel(gv, "Code", "Description", "Rate Type", "Axis Type", "Matrix Type", "Effective Date", "Inactive Date", "FAT Ratio", "SNF Ratio", "FAT %", "SNF %", "Milk Rate", "Price_Type", "Declared Rate") Then
                Try

                    Dim obj As clsfrmPriceChartMaster = Nothing

                    Dim counter As Integer = 1

                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        'counter += 1

                        obj = New clsfrmPriceChartMaster()
                        obj.code = clsCommon.myCstr(grow.Cells("code").Value)
                        'If clsCommon.myLen(obj.code) <= 0 Then 'Or clsCommon.myLen(obj.code) > 30 Then
                        '    obj.code = clsERPFuncationality.GetNextCode(Nothing, txtefctdate.Value, clsDocType.MILKPRCMASTER, "", "") '    Throw New Exception("Length Of Price Code Should Not Exceed 30 Characters At Line No. " + clsCommon.myCstr(counter) + ".")
                        'End If

                        obj.desc = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(obj.desc) <= 0 Or clsCommon.myLen(obj.desc) > 150 Then
                            Throw New Exception("Length Of Price Description Should Not Exceed 150 Characters At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If

                        obj.ratetype = clsCommon.myCstr(grow.Cells("Rate Type").Value)
                        'If clsCommon.myLen(obj.ratetype) <= 0 Or (clsCommon.CompairString(obj.ratetype, "Declared Rate") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.ratetype, "Effective Rate") <> CompairStringResult.Equal) Then
                        '    Throw New Exception("Please Fill Rate Type And It Should Be 'Declared Rate' Or 'Effective Rate' At Line No. " + clsCommon.myCstr(counter) + ".")
                        'End If

                        obj.declrd_rate = clsCommon.myCdbl(grow.Cells("Declared Rate").Value)
                        obj.effctv_rate = 0
                        'obj.declrd_rate = clsCommon.myCdbl(grow.Cells("Declared Rate").Value)
                        'If clsCommon.myCdbl(obj.declrd_rate) <= 0 Then
                        '    Throw New Exception("Declared Rate Should be Greater Than 0(zero) At Line No. " + clsCommon.myCstr(counter) + ".")
                        'End If

                        'obj.effctv_rate = clsCommon.myCdbl(grow.Cells("Effective Rate").Value)
                        'If clsCommon.myCdbl(obj.effctv_rate) <= 0 Then
                        '    Throw New Exception("Effective Rate Should be Greater Than 0(zero) At Line No. " + clsCommon.myCstr(counter) + ".")
                        'End If

                        obj.axis = clsCommon.myCstr(grow.Cells("axis type").Value)
                        obj.matrix = clsCommon.myCstr(grow.Cells("matrix type").Value)

                        'If clsCommon.CompairString(obj.axis, "Single") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.axis, "Double") <> CompairStringResult.Equal Then
                        '    Throw New Exception("Please Fill Single/Double For Axis Type At Line No. " + clsCommon.myCstr(counter) + "")
                        'End If

                        'If clsCommon.myLen(obj.matrix) <= 0 Then
                        '    Throw New Exception("Please Fill Matrix Type Like 4.0/8.5 At Line No. " + clsCommon.myCstr(counter) + "")
                        'ElseIf clsCommon.myLen(obj.matrix) > 10 Then
                        '    Throw New Exception("Length Of Matrix Type Should Not Exceed Max.10 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                        'End If
                        Dim effectiveDate As String = clsCommon.myCstr((grow.Cells("Effective Date").Value))
                        If IsDate(effectiveDate) = False Then
                            Throw New Exception("Invalid Formate of Effective Date.Date Formate should be dd/mm/yyyy.At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If
                        obj.effct_dt = clsCommon.myCDate(grow.Cells("Effective Date").Value)

                        Dim inactiveDate As String = clsCommon.myCstr((grow.Cells("Inactive Date").Value))
                        If IsDate(inactiveDate) = False Then
                            Throw New Exception("Invalid Formate of Inactive Date.Date Formate should be dd/mm/yyyy.At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If
                        obj.inactv_dt = clsCommon.myCDate(grow.Cells("Inactive Date").Value)
                        If Not clsCommon.myCDate(obj.inactv_dt) >= clsCommon.myCDate(obj.effct_dt) Then
                            Throw New Exception("Inactive Date Should Be Greater Or Equal To Effective Date.At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If
                        'Try
                        '    obj.inactv_dt = (grow.Cells("Inactive Date").Value)
                        'Catch exx As Exception
                        '    obj.inactv_dt = clsCommon.GETSERVERDATE()
                        'End Try
                        obj.ratio = clsCommon.myCstr(grow.Cells("FAT Ratio").Value)
                        obj.snf_ratio = clsCommon.myCstr(grow.Cells("SNF Ratio").Value)
                        If (clsCommon.myCdbl(obj.ratio) + clsCommon.myCdbl(obj.snf_ratio)) < 100 Then
                            Throw New Exception("Sum of ratio of fat and snf should be equal to 100 ,see at line no. " + clsCommon.myCstr(counter) + "")
                        End If

                        obj.fat_pers = clsCommon.myCdbl(grow.Cells("FAT %").Value)
                        obj.snf_pers = clsCommon.myCdbl(grow.Cells("SNF %").Value)
                        obj.rate = clsCommon.myCdbl(grow.Cells("Milk Rate").Value)
                        obj.price_type = formtype

                        Dim isposted As Boolean = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_MILK_PRICE_MASTER where price_code='" + obj.code + "' and price_type='" + obj.price_type + "' and Posted = 1"))
                        If isposted = True Then
                            Throw New Exception("Price Code ('" + obj.code + "') already posted.You can not update this price code.,see at line no. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim qry As String = "select count(*) from TSPL_MILK_PRICE_MASTER where price_code='" + obj.code + "' and price_type='" + obj.price_type + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                        If check > 0 Then
                            isNewEntry = False
                        Else
                            isNewEntry = True
                        End If
                        If clsfrmPriceChartMaster.SaveData(obj.code, isNewEntry, obj) Then
                            counter += 1
                        End If
                    Next

            clsCommon.ProgressBarHide()
            Reset()
                    clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
                Catch ex As Exception
                clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
        End If
        End If

        If formtype = "BULK" Then
            If transportSql.importExcel(gv, "Code", "Description", "Effective Date", "Inactive Date", "FAT Ratio", "SNF Ratio", "FAT %", "SNF %", "Milk Rate", "Price_Type") Then
                Try

                    Dim obj As clsfrmPriceChartMaster = Nothing

                    Dim counter As Integer = 1

                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        'counter += 1

                        obj = New clsfrmPriceChartMaster()
                        obj.code = clsCommon.myCstr(grow.Cells("code").Value)
                        If clsCommon.myLen(obj.code) <= 0 Or clsCommon.myLen(obj.code) > 30 Then
                            Throw New Exception("Length Of Price Code Should Not Exceed 30 Characters At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If

                        obj.desc = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(obj.desc) <= 0 Or clsCommon.myLen(obj.desc) > 150 Then
                            Throw New Exception("Length Of Price Description Should Not Exceed 150 Characters At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If

                        obj.declrd_rate = 0
                        obj.effctv_rate = 0

                        obj.effct_dt = (grow.Cells("Effective Date").Value)
                        Try
                            obj.inactv_dt = (grow.Cells("Inactive Date").Value)
                        Catch exx As Exception
                            obj.inactv_dt = clsCommon.GETSERVERDATE()
                        End Try
                        obj.ratio = clsCommon.myCstr(grow.Cells("FAT Ratio").Value)
                        obj.snf_ratio = clsCommon.myCstr(grow.Cells("SNF Ratio").Value)
                        If (clsCommon.myCdbl(obj.ratio) + clsCommon.myCdbl(obj.snf_ratio)) < 100 Then
                            Throw New Exception("Sum of ratio of fat and snf should be equal to 100 ,see at line no. " + clsCommon.myCstr(counter) + "")
                        End If

                        obj.fat_pers = clsCommon.myCdbl(grow.Cells("FAT %").Value)
                        obj.snf_pers = clsCommon.myCdbl(grow.Cells("SNF %").Value)
                        obj.rate = clsCommon.myCdbl(grow.Cells("Milk Rate").Value)
                        obj.price_type = formtype

                        Dim qry As String = "select count(*) from TSPL_MILK_PRICE_MASTER where price_code='" + obj.code + "' and price_type='" + obj.price_type + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                        If check > 0 Then
                            isNewEntry = False
                        Else
                            isNewEntry = True
                        End If
                        If clsfrmPriceChartMaster.SaveData(obj.code, isNewEntry, obj) Then
                            counter += 1
                        End If
                    Next

                    clsCommon.ProgressBarHide()
                    Reset()
                    clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
                Catch ex As Exception
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            End If
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
        gvSNFDed.Rows.AddNew()
    End Sub

    Private Sub txtratio_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtratio.Validating
        Try
            Convert.ToDecimal(txtratio.Text)
            ErrorControl.ResetError(txtratio)
        Catch ex As Exception
            ErrorControl.SetError(txtratio, "Fill numeric value only.")
            txtratio.Focus()
            txtratio.Select()
            txtratio.Text = "0"
            clsCommon.MyMessageBoxShow(Me, "Fill numeric value only.", Me.Text)
        End Try
        Try
            txtsnf_ratio.Text = 100 - clsCommon.myCdbl(txtratio.Text)
            If (clsCommon.myCdbl(txtratio.Text) + clsCommon.myCdbl(txtsnf_ratio.Text)) <> 100 AndAlso clsCommon.myCdbl(txtsnf_ratio.Text) > 0 Then
                txtratio.Focus()
                Throw New Exception("Please Fill Ratio Of SNF And FAT." + Environment.NewLine + "There Sum Should be Equal To 100")
            End If
        Catch exx As Exception
            clsCommon.MyMessageBoxShow(Me, exx.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtsnf_ratio_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtsnf_ratio.Validating
        Try
            Convert.ToDecimal(txtsnf_ratio.Text)
            ErrorControl.ResetError(txtsnf_ratio)
        Catch ex As Exception
            ErrorControl.SetError(txtsnf_ratio, "Fill numeric value only.")
            txtsnf_ratio.Focus()
            txtsnf_ratio.Select()
            txtsnf_ratio.Text = "0"
            clsCommon.MyMessageBoxShow(Me, "Fill numeric value only.", Me.Text)
        End Try
        Try
            txtratio.Text = 100 - clsCommon.myCdbl(txtsnf_ratio.Text)
            If (clsCommon.myCdbl(txtratio.Text) + clsCommon.myCdbl(txtsnf_ratio.Text)) <> 100 AndAlso clsCommon.myCdbl(txtratio.Text) > 0 Then
                txtsnf_ratio.Focus()
                Throw New Exception("Please Fill Ratio Of SNF And FAT." + Environment.NewLine + "There Sum Should be Equal To 100")
            End If
        Catch exx As Exception
            clsCommon.MyMessageBoxShow(exx.Message)
        End Try
    End Sub
    '===========added by shivani against[BM00000009459]
    Sub funPrint()
        Try
            If clsCommon.myLen(fndcode.Value) > 0 Then
                Dim qry As String = " Select TSPL_COMPANY_MASTER.Comp_Name as Company_Name,TSPL_COMPANY_MASTER.Add1 as Address1,TSPL_COMPANY_MASTER.Add2 as Address2,  TSPL_COMPANY_MASTER.Add3 as Address3,TSPL_MILK_PRICE_MASTER.Price_Code,Convert(varchar,TSPL_MILK_PRICE_MASTER.Effective_Date ,103) as Effective_Date ,Convert(varchar,TSPL_MILK_PRICE_MASTER.Inactive_Date  ,103) as Inactive_Date,Convert(decimal(18,2),TSPL_MILK_PRICE_MASTER.Ratio) as Fat_Ratio ,TSPL_MILK_PRICE_MASTER.SNF_Ratio  ,TSPL_MILK_PRICE_MASTER.FAT_Pers ,TSPL_MILK_PRICE_MASTER.SNF_Pers  ,TSPL_MILK_PRICE_MASTER.Declared_Rate ,TSPL_MILK_PRICE_MASTER.Milk_Rate as Effective_Rate  ,TSPL_MILK_PRICE_MASTER.Description  , TSPL_MILK_PRICE_MASTER.Created_By,convert (varchar,TSPL_MILK_PRICE_MASTER.Created_Date,103) as Created_Date ,case when TSPL_MILK_PRICE_MASTER.Posted=1 then TSPL_MILK_PRICE_MASTER.Modified_By else '' end as Posted_By,case when TSPL_MILK_PRICE_MASTER.Posted=1 then convert (varchar, TSPL_MILK_PRICE_MASTER.Modified_Date,103) else '' end as Posted_Date, Convert(varchar,TSPL_MILK_PRICE_MASTER_HISTORY.Effective_Date ,103) as Effective_DateHis, Convert(varchar,TSPL_MILK_PRICE_MASTER_HISTORY.Inactive_Date  ,103) as Inactive_DateHis ,TSPL_MILK_PRICE_MASTER_HISTORY.FAT_Pers  as  FAT_PersHis,  TSPL_MILK_PRICE_MASTER_HISTORY.SNF_Pers  as SNF_Pers_his,TSPL_MILK_PRICE_MASTER_HISTORY.Ratio   as Fat_RatioHis,TSPL_MILK_PRICE_MASTER_HISTORY.Snf_Ratio as Snf_RatioHis,TSPL_MILK_PRICE_MASTER_HISTORY.Declared_Rate  as Declared_RateHis,TSPL_MILK_PRICE_MASTER_HISTORY.Effective_Rate  as Effective_RateHis,TSPL_MILK_PRICE_MASTER_HISTORY.Modified_By as ModifiedBy,TSPL_MILK_PRICE_MASTER_HISTORY.Modified_Date as Modified_Date from TSPL_MILK_PRICE_MASTER " & _
                " Left outer Join TSPL_MILK_PRICE_MASTER_HISTORY on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_MILK_PRICE_MASTER_HISTORY.Price_Code  " & _
                " Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_MILK_PRICE_MASTER.Comp_Code  where 1=1 and  TSPL_MILK_PRICE_MASTER.Price_Code='" + fndcode.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptMilkPriceChartMaster", "Milk Price Chart")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No document for print", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        funPrint()
    End Sub

    Private Sub txtCLR_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtCLR.Validating
        CalculateSNFFromCLR()
    End Sub

    Sub CalculateSNFFromCLR()
        If isCLRInsteadOfSNF Then
            txtCLR.Value = clsERPFuncationality.myDclInZeroPointFive(txtCLR.Value)
            txtsnf.Value = Math.Round(clsEkoPro.getSnfOnCalculation(txtfat.Value, txtCLR.Value, dclCorrectionFactor), 1, MidpointRounding.ToEven)
        End If
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(fndcode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Price Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(fndcode.Value, "Price_Code", "TSPL_MILK_PRICE_MASTER")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Sub loadBlankGrid(ByVal gv As common.UserControls.MyRadGridView)
        Try
            gv.Rows.Clear()
            gv.Columns.Clear()
            Dim repoDeciCol As GridViewDecimalColumn
            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colPer
            repoDeciCol.Width = 100
            repoDeciCol.DecimalPlaces = 1
            repoDeciCol.Minimum = 0
            repoDeciCol.Maximum = 100
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "Percent"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colAmt
            repoDeciCol.Width = 100
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 0
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "Deduction Amount"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            gv.AllowDeleteRow = True
            gv.AllowAddNewRow = False
            gv.ShowGroupPanel = False
            gv.AllowColumnReorder = False
            gv.AllowRowReorder = False
            gv.EnableSorting = False
            gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv.MasterTemplate.ShowRowHeaderColumn = False
            gv.TableElement.TableHeaderHeight = 40
            gv.AutoSizeRows = False
            gv.AllowRowReorder = True

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvSNF_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gvSNFDed.UserDeletedRow
    End Sub

    Private Sub gvSNF_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gvSNFDed.UserDeletingRow
        If Not myMessages.deleteConfirm() Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gvSNFDed_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvSNFDed.CurrentColumnChanged
        If gvSNFDed.RowCount > 0 Then
            Dim intCurrRow As Integer = gvSNFDed.CurrentRow.Index
            If intCurrRow = gvSNFDed.Rows.Count - 1 Then
                gvSNFDed.Rows.AddNew()
                gvSNFDed.CurrentRow = gvSNFDed.Rows(intCurrRow)
            End If
        End If
    End Sub
End Class

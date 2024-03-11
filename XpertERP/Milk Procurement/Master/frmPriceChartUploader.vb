Imports common
Imports System.Data.SqlClient

Public Class FrmPriceChartUploader
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ErrorControl As clsErrorControl = New clsErrorControl()
    Dim IsInsieLoadData As Boolean
    Dim fat_Pers As Double = 0
    Dim Snf_pers As Double = 0
    Dim MilkPricePostedData As Boolean
    Dim isAutoSelectMCCRouteVLC As Boolean = False
    Dim settMilkCollectionPickBulkRoute As Boolean = False
#End Region

    Sub Reset()
        IsInsieLoadData = False
        cmbrate.Items.Clear()
        FndPriceCode.Value = Nothing
        cmbrate.Items.Add("None")
        cmbratetype.Text = "None"
        cmbmatrix.Text = "None"
        cmbaxis.Text = "None"
        cmbrate.Text = "None"
        Me.CmbShift.DataSource = ClsOpenMCCShift.GetShift
        Me.CmbShift.DisplayMember = "Name"
        Me.CmbShift.ValueMember = "Code"
        Me.CmbShift.SelectedIndex = -1
        cmbratetype.Enabled = True
        cmbaxis.Enabled = True
        cmbmatrix.Enabled = True
        BtnPost.Enabled = True
        btnUpdates.Enabled = True
        BtnSaveCharge.Enabled = True
        btnUpdates.Enabled = False
        cmbrate.Enabled = True
        txtDocNo.Value = ""
        txtDocNo.MyReadOnly = False
        txtdate.Text = clsCommon.GETSERVERDATE()
        txtCreatedDate.Text = clsCommon.GETSERVERDATE()
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        gvCharges.Rows.Clear()
        LoadBlank_Charges_Grid()
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        txtfat.Value = 0
        txtsnf.Value = 0
        txtInactiveFrom.Checked = False
        txtCircularNo.Text = ""
        txtMCC.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing
        txtVLC.arrValueMember = Nothing
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmPriceChartUploader)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
            End If
            Me.Close()
            Exit Sub
        End If
        BtnPost.Visible = MyBase.isPostFlag
        ' btnexcel.Visible = MyBase.isExport
        If MyBase.isExport = True Then
            btnimport.Enabled = True
            btnexport.Enabled = True
            ExportSelectedCode.Enabled = True
        Else
            btnexport.Enabled = False
            btnimport.Enabled = False
            ExportSelectedCode.Enabled = False
        End If
    End Sub

    Private Sub FrmPriceChartUploader_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+R for Refresh the Window")
        ButtonToolTip.SetToolTip(btnshow, "Press Alt+S  for Show Data In Grid")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")

        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage2.Item.Visibility = ElementVisibility.Visible
        settMilkCollectionPickBulkRoute = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionPickBulkRoute, clsFixedParameterCode.MilkCollectionPickBulkRoute, Nothing)) = 1)
        MilkPricePostedData = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowMilkItemMasterPostedData, clsFixedParameterCode.AllowMilkItemMasterPostedData, Nothing)) = 1, True, False)
        isAutoSelectMCCRouteVLC = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoSelectMCCRouteVLC, clsFixedParameterCode.AutoSelectMCCRouteVLC, Nothing)) = 1, True, False)
        UcAttachment1.isDeleteTheAttachment = False
        objCommonVar.SepratePriceChartForCow = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SepratePriceChartForCowMilk, clsFixedParameterCode.SepratePriceChartForCowMilk, Nothing)) = 1
        If objCommonVar.DisplayTypeInMilkReceipt Then
            cboDockCollectionMilkType.DataSource = clsMilkReceiptMCC.GetMilkType()
        Else
            cboDockCollectionMilkType.DataSource = clsMilkReceiptMCC.GetDockCollectionMilkType(False, True)
        End If
        cboDockCollectionMilkType.ValueMember = "Code"
        cboDockCollectionMilkType.DisplayMember = "Name"
        cboDockCollectionMilkType.SelectedValue = "M"

        cboDockCollectionMilkType.Enabled = (objCommonVar.SepratePriceChartForCow OrElse objCommonVar.DisplayTypeInMilkReceipt)
    End Sub

    Private Sub btnshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnshow.Click
        LoadData()
    End Sub

    Sub LoadData()
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Code Of Price Chart Uploader", Me.Text)
            txtDocNo.Focus()
            txtDocNo.Select()
            ErrorControl.SetError(txtDocNo, "Please Select Code Of Price Chart Uploader")
            Return
        Else
            ErrorControl.ResetError(txtDocNo)
        End If

        cmbrate.Items.Clear()
        cmbrate.Items.Add("None")


        Dim qry As String = "select count(*) from TSPL_FAT_SNF_UPLOADER_MASTER where code='" + clsCommon.myCstr(txtDocNo.Value) + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Return
        End If


        qry = "select distinct snf from TSPL_FAT_SNF_UPLOADER_MASTER where code='" + clsCommon.myCstr(txtDocNo.Value) + "' order by snf"
        Dim snfdt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim values As String = ""
        Dim Sumvalues As String = ""

        If snfdt IsNot Nothing AndAlso snfdt.Rows.Count > 0 Then
            For Each dr As DataRow In snfdt.Rows
                values = values + ",[" + clsCommon.myCstr(dr("snf")) + "]"
                Sumvalues = Sumvalues + ",max(aa1.[" + clsCommon.myCstr(dr("snf")) + "]) as [" + clsCommon.myCstr(dr("snf")) + "]"
            Next
        End If

        Try
            If values.Substring(0, 1) = "," Then
                values = values.Substring(1, values.Length - 1)
            End If
        Catch ex As Exception
            values = ""
        End Try

        Try
            If Sumvalues.Substring(0, 1) = "," Then
                Sumvalues = Sumvalues.Substring(1, Sumvalues.Length - 1)
            End If
        Catch ex As Exception
            Sumvalues = ""
        End Try


        If clsCommon.myLen(values) > 0 Then
            qry = "select   aa1.FAT," + Sumvalues + " from (select FAT," + values + " from (select ROW_NUMBER() over(PARTITION by fat order by fat) as sno,FAT,rate,snf from tspl_fat_snf_uploader_master where code='" + clsCommon.myCstr(txtDocNo.Value) + "') as s pivot(max(rate) for snf in (" + values + ")) as t)aa1 group by aa1.FAT"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.DataSource = dt
                gv.Columns("fat").HeaderText = "FAT \ SNF"
                gv.Columns("fat").Width = 90
                gv.Columns("fat").IsPinned = True
                gv.ReadOnly = True
                gv.MasterTemplate.ShowRowHeaderColumn = False
                'gv.BestFitColumns()

                gv.AllowDeleteRow = False
                gv.AllowAddNewRow = False
                gv.ShowGroupPanel = False
                gv.AllowColumnReorder = False
                gv.AllowRowReorder = False
                gv.EnableSorting = False
                gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
                'gv.AutoSizeRows = True
            End If

            qry = "select distinct Date,TSPL_FAT_SNF_UPLOADER_MASTER.CircularNo,TSPL_FAT_SNF_UPLOADER_MASTER.Is_InActive,TSPL_FAT_SNF_UPLOADER_MASTER.Rate_Type,TSPL_FAT_SNF_UPLOADER_MASTER.Axis_Type,TSPL_FAT_SNF_UPLOADER_MASTER.Matrix_Type,TSPL_FAT_SNF_UPLOADER_MASTER.Price_code,Milk_Rate as [Effective Rate],Declared_Rate as [Declared Rate],TSPL_FAT_SNF_UPLOADER_MASTER.Dock_Collection_Milk_Type,TSPL_FAT_SNF_UPLOADER_MASTER.In_Active_From from TSPL_FAT_SNF_UPLOADER_MASTER left join TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.price_code=TSPL_FAT_SNF_UPLOADER_MASTER.price_code  where code='" + clsCommon.myCstr(txtDocNo.Value) + "'"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                cboDockCollectionMilkType.SelectedValue = clsCommon.myCstr(dt.Rows(0)("Dock_Collection_Milk_Type"))
                txtdate.Text = clsCommon.myCDate(dt.Rows(0)("Date"))
                cmbratetype.Text = clsCommon.myCstr(dt.Rows(0)("rate_type"))
                cmbaxis.Text = clsCommon.myCstr(dt.Rows(0)("axis_type"))
                cmbmatrix.Text = clsCommon.myCstr(dt.Rows(0)("matrix_type"))
                cmbratetype.Enabled = False
                FndPriceCode.Value = clsCommon.myCstr(dt.Rows(0)("Price_code"))
                qry = "select FAT_Pers,SNF_Pers  from TSPL_MILK_PRICE_MASTER  where Price_Code ='" & FndPriceCode.Value & "'"
                Dim dtt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
                    txtfat.Value = clsCommon.myCstr(dtt.Rows(0)("FAT_Pers"))
                    txtsnf.Value = clsCommon.myCstr(dtt.Rows(0)("SNF_Pers"))
                End If
                cmbaxis.Enabled = False
                cmbmatrix.Enabled = False

                cmbrate.Items.Clear()
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Is_InActive")), "1") = CompairStringResult.Equal Then
                    txtInactiveFrom.Checked = 1
                    txtInactiveFrom.Value = clsCommon.myCDate(dt.Rows(0)("In_Active_From"))
                Else
                    txtInactiveFrom.Checked = 0
                    txtInactiveFrom.Value = clsCommon.GETSERVERDATE()
                End If
                txtCircularNo.Text = clsCommon.myCstr(dt.Rows(0)("CircularNo"))

                If Not cmbrate.Items.Contains(clsCommon.myCdbl(dt.Rows(0)("Effective Rate"))) Then
                    cmbrate.Items.Add(clsCommon.myCdbl(dt.Rows(0)("Effective Rate")))
                End If
                cmbrate.Text = clsCommon.myCdbl(dt.Rows(0)("Effective Rate"))
                CmbStandardRate.Items.Clear()
                If Not CmbStandardRate.Items.Contains(clsCommon.myCdbl(dt.Rows(0)("Declared Rate"))) Then
                    CmbStandardRate.Items.Add(clsCommon.myCdbl(dt.Rows(0)("Declared Rate")))
                End If
                CmbStandardRate.Text = clsCommon.myCdbl(dt.Rows(0)("Declared Rate"))
            End If

            'If clsCommon.CompairString(cmbratetype.Text, "None") <> CompairStringResult.Equal Then
            '    Dim xspilt() As String
            '    xspilt = cmbmatrix.Text.Split("/")
            '    Dim compr_fat As String = clsCommon.myCstr(xspilt(0))
            '    Dim compr_snf As String = clsCommon.myCstr(xspilt(1))
            '    compr_fat = txtfat.Value
            '    compr_snf = txtsnf.Value
            '    qry = "select distinct rate from tspl_fat_snf_uploader_master where code='" + clsCommon.myCstr(txtDocNo.Value) + "' and fat='" + clsCommon.myCstr(compr_fat) + "' and snf='" + clsCommon.myCstr(compr_snf) + "'"
            '    Dim xrate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            '    cmbrate.Items.Add(xrate)
            '    cmbrate.Text = xrate
            '    cmbrate.Enabled = False
            'End If

            '=======================MCC========================
            qry = "select MCC_CODE from TSPL_FAT_SNF_UPLOADER_MCC where code='" + txtDocNo.Value + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim arr As New ArrayList()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("MCC_CODE")))
                Next
                txtMCC.arrValueMember = arr
            End If

            '===========VLC==================================
            qry = "select coalesce(posted,'0') from tspl_fat_snf_Uploader_master where code='" & clsCommon.myCstr(txtDocNo.Value) & "'"
            Dim isPosted As String = clsDBFuncationality.getSingleValue(qry)

            qry = "select TSPL_FAT_SNF_UPLOADER_VLC.VLC_Code, TSPL_VLC_MASTER_HEAD.Route_Code from TSPL_FAT_SNF_UPLOADER_VLC left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_FAT_SNF_UPLOADER_VLC.VLC_Code where code='" + txtDocNo.Value + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            arr = New ArrayList()
            Dim arrVLC As New ArrayList()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    arrVLC.Add(clsCommon.myCstr(dr("VLC_Code")))
                    If Not arr.Contains(clsCommon.myCstr(dr("Route_Code"))) Then
                        arr.Add(clsCommon.myCstr(dr("Route_Code")))
                    End If
                Next
            End If
            txtVLC.arrValueMember = arrVLC
            txtRoute.arrValueMember = arr
            UcAttachment1.LoadData(txtDocNo.Value)
            GetChargesData(txtDocNo.Value, NavigatorType.Current)
            If isPosted = "1" Then
                BtnPost.Enabled = False
                BtnSaveCharge.Enabled = False
                btnUpdates.Enabled = True
            Else
                BtnPost.Enabled = True
                BtnSaveCharge.Enabled = True
                btnUpdates.Enabled = True
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Return
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Dim qry As String = "select '4.0' as [FAT/SNF],'23' as '8','28' as '8.1','30' as '8.2','32' as '8.3'union all select  '4.1','24','28','30','32' union all select  '4.2','26','28','30','32'"
        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        If clsCommon.myLen(txtdate.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Fill Date", Me.Text)
            txtdate.Focus()
            txtdate.Select()
            Return
        End If

        If Not settMilkCollectionPickBulkRoute Then
            If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select at least one MCC or VLC", Me.Text)
                Return
            End If
            If txtRoute.arrValueMember Is Nothing OrElse txtRoute.arrValueMember.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select at least one Route", Me.Text)
                Return
            End If
            If txtVLC.arrValueMember Is Nothing OrElse txtVLC.arrValueMember.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select at least one VLC", Me.Text)
                Return
            End If
        End If

        If clsCommon.myLen(FndPriceCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Price Code", Me.Text)
            FndPriceCode.Focus()
            Return
        End If
        If clsCommon.myLen(CmbShift.SelectedValue) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Shift", Me.Text)
            CmbShift.Focus()
            Return
        End If
        If Not (objCommonVar.SepratePriceChartForCow Or objCommonVar.DisplayTypeInMilkReceipt) Then
            cboDockCollectionMilkType.SelectedValue = "M"
        End If

        '===================added by rohit to make charge detail mandatory=================
        '============Check Total Charge Rate==============
        Dim totcharge As Decimal = 0
        For Each row As GridViewRowInfo In gvCharges.Rows
            totcharge += row.Cells("COLRate").Value
        Next
        'If totcharge > 0 Then
        If Math.Round(clsCommon.myCdbl(CmbStandardRate.Text) - clsCommon.myCdbl(cmbrate.Text), 2) <> totcharge Then
            clsCommon.MyMessageBoxShow("Please Fill Charges Correctly.it's Rate Total should be [" & Math.Round(clsCommon.myCdbl(CmbStandardRate.Text) - clsCommon.myCdbl(cmbrate.Text), 2) & "]", Me.Text)
            gvCharges.Focus()
            gvCharges.Select()
            ErrorControl.SetError(gvCharges, "Please Fill Charges Correctly.it's Rate Total should be [" & Math.Round(clsCommon.myCdbl(CmbStandardRate.Text) - clsCommon.myCdbl(cmbrate.Text), 2) & "]")
            Exit Sub
        Else
            ErrorControl.ResetError(gvCharges)
        End If



        Dim ratetype As String = clsCommon.myCstr(cmbratetype.Text)
        Dim axistype As String = clsCommon.myCstr(cmbaxis.Text)
        Dim matrixtype As String = clsCommon.myCstr(cmbmatrix.Text)
        Dim compr_fat As String = ""
        Dim compr_snf As String = ""
        Dim compr_rate As Decimal = Nothing

        Try
            compr_rate = clsCommon.myCdbl(cmbrate.Text)

            If clsCommon.myCstr(cmbrate.Text) = "None" AndAlso clsCommon.CompairString(cmbmatrix.Text, "None") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(cmbaxis.Text, "None") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(cmbratetype.Text, "None") <> CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Rate First Before Upload", Me.Text)
                cmbrate.Select()
                ErrorControl.SetError(cmbrate, "Please Select Rate First Before Upload")
                Return
            Else
                ErrorControl.ResetError(cmbrate)
            End If
        Catch exx As Exception
            compr_rate = 0
        End Try
        '-------------------------------------------

        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)

        Dim OFDFileName As String = ""
        Dim OFDSafeFileName As String = ""
        Dim columnsname As String = transportSql.GetExcelColumnsName(gv1, OFDFileName, OFDSafeFileName)
        Dim currentdate As Date = Nothing
        Dim isSaved As Boolean = True
        'Dim code As String = ""


        compr_fat = fat_Pers.ToString()
        compr_snf = Snf_pers.ToString()

        currentdate = Convert.ToDateTime(txtdate.Text)

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        'Dim qry As String = "select max(code) from TSPL_FAT_SNF_UPLOADER_MASTER"
        'code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        'If clsCommon.myLen(code) > 0 Then
        '    code = clsCommon.myCstr(clsCommon.incval(code))
        'Else
        '    code = "PCU000001"
        'End If

        Dim xrangematch As Boolean = False
        Try
            Dim Code As String = clsERPFuncationality.GetNextCode(trans, currentdate, clsDocType.MatrixPriceChart, "", "")
            If clsCommon.myLen(Code) < 0 Then
                Throw New Exception("Error in Code Generation")
            End If
            If clsCommon.myLen(columnsname) > 0 Then
                If gv1.Rows.Count > 0 Then
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        If clsCommon.myLen(gv1.Rows(ii).Cells(0).Value) <= 0 Then
                            Continue For
                        End If
                        Dim snf As Decimal = Nothing
                        Dim fat As Decimal = Nothing
                        Try
                            fat = Math.Truncate(clsCommon.myCdbl(gv1.Rows(ii).Cells(0).Value) * 10) / 10
                        Catch exx As Exception
                        End Try
                        If fat >= 0 Then
                            For jj As Integer = 1 To gv1.ColumnCount - 1
                                Dim rate As Decimal = Nothing
                                Try
                                    rate = Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(jj).Value), 3)
                                Catch exx As Exception
                                End Try
                                Try
                                    snf = Math.Truncate(clsCommon.myCdbl(gv1.Columns(jj).HeaderText.Replace("#", ".")) * 10) / 10
                                Catch exx As Exception
                                End Try

                                If clsCommon.myCdbl(compr_fat) > 0 AndAlso clsCommon.myCdbl(fat) = clsCommon.myCdbl(compr_fat) AndAlso clsCommon.myCdbl(snf) = clsCommon.myCdbl(compr_snf) Then
                                    If clsCommon.myCdbl(compr_rate) = clsCommon.myCdbl(rate) Then
                                        xrangematch = True
                                    Else
                                        Throw New Exception("Uploading Chart Does Not Match The Format of Above Selected Options" + Environment.NewLine + "Please Check The Sheet Or Change The Options." + Environment.NewLine + "It should have " & cmbrate.Text & " Rate for Fat/SNF " & fat & "/" & snf & " ")
                                    End If
                                End If

                                Dim coll As New Hashtable()
                                clsCommon.AddColumnsForChange(coll, "Code", Code)
                                clsCommon.AddColumnsForChange(coll, "date", clsCommon.GetPrintDate(currentdate, "dd/MMM/yyyy"))
                                clsCommon.AddColumnsForChange(coll, "fat", fat)
                                clsCommon.AddColumnsForChange(coll, "snf", snf)
                                clsCommon.AddColumnsForChange(coll, "rate", rate)
                                clsCommon.AddColumnsForChange(coll, "Price_Code", FndPriceCode.Value)
                                clsCommon.AddColumnsForChange(coll, "Price_Code_Shift", CmbShift.SelectedValue)
                                clsCommon.AddColumnsForChange(coll, "CircularNo", txtCircularNo.Text)
                                clsCommon.AddColumnsForChange(coll, "created_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                                clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                                clsCommon.AddColumnsForChange(coll, "modified_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                                clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                                clsCommon.AddColumnsForChange(coll, "Planning_Code", Nothing, True)
                                clsCommon.AddColumnsForChange(coll, "Dock_Collection_Milk_Type", clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
                                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FAT_SNF_UPLOADER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                            Next
                        End If
                    Next
                End If
            Else
                Throw New Exception("No Data Found For Transfer")
            End If


            '===============save mcc data======================================
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                For Each strvalue As String In txtMCC.arrValueMember
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "code", Code)
                    clsCommon.AddColumnsForChange(coll, "mcc_code", strvalue, True)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FAT_SNF_UPLOADER_MCC", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            '==================save vlc data======================

            If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                For Each strvalue As String In txtVLC.arrValueMember
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "code", Code)
                    clsCommon.AddColumnsForChange(coll, "vlc_code", strvalue, True)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FAT_SNF_UPLOADER_VLC", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            funInsertCharges(Code, trans)
            If Not xrangematch Then
                Throw New Exception("Standart Rate not found in excel sheet")
            End If


            trans.Commit()
            txtDocNo.Value = Code

            UcAttachment1.AddAttachment(OFDFileName, OFDSafeFileName)
            UcAttachment1.SaveData(Code)
            clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
            LoadData()
            btnUpdate.Enabled = True
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Me.Controls.Remove(gv1)
    End Sub

    Sub LoadBlank_Charges_Grid()
        Try
            gvCharges.Rows.Clear()
            gvCharges.Columns.Clear()
            gvCharges.Columns.Add("COLChargeCode", "Charge Code")
            gvCharges.Columns.Add("COLChargeDESC", "Description")
            gvCharges.Columns.Add("COLRate", "Rate")
            gvCharges.Columns("COLChargeCode").Width = 100
            gvCharges.Columns("COLChargeDESC").Width = 200
            gvCharges.Columns("COLRate").Width = 100
            gvCharges.Columns("COLChargeDESC").ReadOnly = True

            gvCharges.AllowAddNewRow = True
            gvCharges.MasterTemplate.AddNewRowPosition = SystemRowPosition.Bottom
            gvCharges.AllowEditRow = True
            gvCharges.AllowDeleteRow = True
            gvCharges.AllowRowResize = False
            gvCharges.AllowRowReorder = False
            gvCharges.AllowColumnResize = True
            gvCharges.AllowColumnChooser = False
            gvCharges.AllowAutoSizeColumns = False
            gvCharges.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub funInsertCharges(ByVal strDocNo As String, ByVal trans As SqlTransaction)
        Dim sQuery As String = ""
        sQuery = "delete from TSPL_FAT_SNF_UPLOADER_Chart_Detail where Price_Code='" & strDocNo & "'"
        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        For Each row As GridViewRowInfo In gvCharges.Rows
            If clsCommon.myLen(row.Cells("COLChargeCode").Value) > 0 Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Price_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Charge_CODE", clsCommon.myCstr(row.Cells("COLChargeCode").Value))
                clsCommon.AddColumnsForChange(coll, "Rate", clsCommon.myCstr(row.Cells("COLRate").Value))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FAT_SNF_UPLOADER_Chart_Detail", OMInsertOrUpdate.Insert, "TSPL_FAT_SNF_UPLOADER_Chart_Detail.Price_CODE='" + strDocNo + "'", trans)
            End If
        Next
    End Sub

    Public Sub GetChargesData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            IsInsieLoadData = True
            Dim whrcls As String = ""
            Dim objtr As New clsVendorMaster
            gvCharges.Rows.Clear()
            whrcls = " and cm.Price_code ='" & strCode & "'"
            Dim qry As String = "select Charge_CODE,Description,GL_CODE,GL_DESC,RaTE from TSPL_FAT_SNF_UPLOADER_Chart_Detail cm inner join " _
            & " TSPL_Charge_Category cc on cc.Charge_Cat_Code=Charge_Code " & whrcls & ""

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim obj As New List(Of clsVendorMaster)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    obj.Add(objtr)
                    gvCharges.Rows.AddNew()
                    gvCharges.Rows(gvCharges.Rows.Count - 1).Cells("COlChargeCode").Value = clsCommon.myCstr(dr("Charge_CODE"))
                    gvCharges.Rows(gvCharges.Rows.Count - 1).Cells("COlChargeDesc").Value = clsCommon.myCstr(dr("Description"))
                    gvCharges.Rows(gvCharges.Rows.Count - 1).Cells("COlRate").Value = clsCommon.myCstr(dr("Rate"))
                Next
            End If
            IsInsieLoadData = False
        Catch ex As Exception
            IsInsieLoadData = False
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gvCharges_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvCharges.CellValueChanged
        Try
            If Not IsInsieLoadData And e.Column Is gvCharges.Columns("COLChargeCode") Then
                Dim str As String = clsItemchargecategorymaster.getFinder("", "", True)
                If str <> "" Then
                    IsInsieLoadData = True
                    gvCharges.CurrentRow.Cells("COLChargeCode").Value = str
                    Dim objCategory As New clsItemchargecategorymaster
                    objCategory = clsItemchargecategorymaster.GetData(str, NavigatorType.Current)
                    If Not IsNothing(objCategory) Then
                        gvCharges.CurrentRow.Cells("COLChargeCode").Value = objCategory.chrcategorycode
                        gvCharges.CurrentRow.Cells("COLChargeDesc").Value = objCategory.chrcategorydesc
                    End If
                    IsInsieLoadData = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub BtnSaveCharge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveCharge.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Price Code First.", Me.Text)
                Exit Sub
            End If
            '============Check Total Charge Rate==============
            Dim totcharge As Decimal = 0
            For Each row As GridViewRowInfo In gvCharges.Rows
                totcharge += row.Cells("COLRate").Value
            Next
            If totcharge > 0 Then
                If Math.Round(clsCommon.myCdbl(CmbStandardRate.Text) - clsCommon.myCdbl(cmbrate.Text), 2) <> totcharge Then
                    clsCommon.MyMessageBoxShow(Me, "Please Fill Charges Correctly.it's Rate Total should be [" & Math.Round(clsCommon.myCdbl(CmbStandardRate.Text) - clsCommon.myCdbl(cmbrate.Text), 2) & "]", Me.Text)
                    gvCharges.Focus()
                    gvCharges.Select()
                    ErrorControl.SetError(gvCharges, "Please Fill Charges Correctly.it's Rate Total should be [" & Math.Round(clsCommon.myCdbl(CmbStandardRate.Text) - clsCommon.myCdbl(cmbrate.Text), 2) & "]")
                    Return
                Else
                    ErrorControl.ResetError(gvCharges)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Please Fill Charges Correctly.it's Rate Total should be [" & Math.Round(clsCommon.myCdbl(CmbStandardRate.Text) - clsCommon.myCdbl(cmbrate.Text), 2) & "]", Me.Text)
                gvCharges.Focus()
                gvCharges.Select()
                ErrorControl.SetError(gvCharges, "Please Fill Charges Correctly.it's Rate Total should be [" & Math.Round(clsCommon.myCdbl(CmbStandardRate.Text) - clsCommon.myCdbl(cmbrate.Text), 2) & "]")
                Return
            End If

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.FrmPriceChartUploader, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            '======================================================================
            funInsertCharges(txtDocNo.Value, Nothing)
            clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully.", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub FndPriceCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndPriceCode._MYValidating
        Dim qry As String = ""
        qry = "select price_code as Code,Description,Rate_Type as [Rate Type],axis_type as [Axis Type],matrix_type as [Matrix Type],Effective_Date as [Effective Date],Milk_Rate as [Effective Rate],Declared_Rate as [Declared Rate],Inactive_Date as [Inactive Date],Ratio as [FAT Ratio],snf_ratio as [SNF Ratio],FAT_Pers as [FAT %],SNF_Pers as [SNF %],Milk_Rate as [Milk Rate],Created_By as [Created By],Created_Date as [Created Date],Modified_By as [Modified By],Modified_Date as [Modified Date] from TSPL_MILK_PRICE_MASTER"
        If MilkPricePostedData = True Then
            qry += " where Posted='1'"
        End If
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("PRC_FND", qry)
        If Not IsNothing(dr) Then
            FndPriceCode.Value = clsCommon.myCstr(dr("Code"))
            cmbratetype.SelectedValue = clsCommon.myCstr(dr("Rate Type"))
            cmbaxis.SelectedValue = clsCommon.myCstr(dr("Axis Type"))
            cmbmatrix.SelectedValue = clsCommon.myCstr(dr("Matrix Type"))
            fat_Pers = clsCommon.myCstr(dr("FAT %"))
            Snf_pers = clsCommon.myCstr(dr("SNF %"))
            txtfat.Value = fat_Pers
            txtsnf.Value = Snf_pers
            cmbrate.Items.Clear()
            If Not cmbrate.Items.Contains(clsCommon.myCdbl(dr("Effective Rate"))) Then
                cmbrate.Items.Add(clsCommon.myCdbl(dr("Effective Rate")))
            End If
            cmbrate.Text = clsCommon.myCdbl(dr("Effective Rate"))
            CmbStandardRate.Items.Clear()
            If Not CmbStandardRate.Items.Contains(clsCommon.myCdbl(dr("Declared Rate"))) Then
                CmbStandardRate.Items.Add(clsCommon.myCdbl(dr("Declared Rate")))
            End If
            CmbStandardRate.Text = clsCommon.myCdbl(dr("Declared Rate"))
        Else
            fat_Pers = 0
            Snf_pers = 0
        End If
    End Sub

    Private Sub ExportSelectedCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportSelectedCode.Click
        Dim arrHeader As New List(Of String)
        arrHeader.Add(txtDocNo.Value)
        clsCommon.MyExportToExcelGrid("Price Chart Uploader", gv, arrHeader, Me.Text)
        clsCommon.MyMessageBoxShow(Me, "Sheet Exported Successfully..", Me.Text)
    End Sub

    Private Sub BtnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGo.Click
        Try
            If clsCommon.myCdbl(TxtFindFAT.Text) > 0 And clsCommon.myCdbl(TxtFindSNF.Text) > 0 And clsCommon.myCdbl(TxtFindRate.Text) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please either select FAT & SNF or Rate to Find.", Me.Text)
                Exit Sub
            End If
            If clsCommon.myCdbl(TxtFindFAT.Text) > 0 And clsCommon.myCdbl(TxtFindSNF.Text) > 0 Then
                For Each col As GridViewColumn In gv.Columns
                    If col.Name = "" & TxtFindSNF.Text & "" Then
                        For Each row As GridViewRowInfo In gv.Rows
                            If row.Cells("FAT").Value = Math.Round(clsCommon.myCdbl(TxtFindFAT.Text), 2) Then
                                gv.CurrentColumn = gv.Columns(col.Name)
                                gv.CurrentRow = row
                            End If
                        Next
                    End If
                Next
            End If
            If clsCommon.myCdbl(TxtFindRate.Text) > 0 Then
                For Each col As GridViewColumn In gv.Columns
                    For Each row As GridViewRowInfo In gv.Rows
                        If row.Cells(col.Name).Value = clsCommon.myCstr(Math.Round(clsCommon.myCdbl(TxtFindRate.Text), 2)) Then
                            gv.CurrentColumn = gv.Columns(col.Name)
                            gv.CurrentRow = row
                            Exit Sub
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub BtnPost_Click(sender As Object, e As EventArgs) Handles BtnPost.Click
        Try
            Dim str As String = "update tspl_fat_snf_Uploader_master set posted='1' where code='" & clsCommon.myCstr(txtDocNo.Value) & "'"
            clsDBFuncationality.ExecuteNonQuery(str)
            clsCommon.MyMessageBoxShow(Me, "Price Chart Posted Successfully...", Me.Text)
            BtnPost.Enabled = False
            BtnSaveCharge.Enabled = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If txtInactiveFrom.Checked Then
                If clsCommon.GetDateWithStartTime(txtInactiveFrom.Value) < clsCommon.GetDateWithStartTime(txtdate.Value) Then
                    Throw New Exception("In-active Date should be greater than date")
                End If
                Dim qry As String = "Select top 1 posted from tspl_fat_snf_Uploader_master where code='" & clsCommon.myCstr(txtDocNo.Value) & "'"
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 0 Then
                    Throw New Exception("Please Post the document first")
                End If
                Dim str As String = "update TSPL_FAT_SNF_UPLOADER_MASTER set Is_InActive=1,In_Active_From='" + clsCommon.GetPrintDate(txtInactiveFrom.Value, "dd/MMM/yyyy") + "' where code='" & clsCommon.myCstr(txtDocNo.Value) & "'"
                clsDBFuncationality.ExecuteNonQuery(str)
                clsCommon.MyMessageBoxShow(Me, "Price In-Actived Successfully...", Me.Text)
                BtnSaveCharge.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME from TSPL_MCC_MASTER"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, Nothing)
        RefreshRoute()
        RefreshVLC()
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select MCC")
            End If
            Dim qry As String = "select Route_Code,Route_Name,TSPL_MCC_ROUTE_MASTER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME from TSPL_MCC_ROUTE_MASTER left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_MCC_ROUTE_MASTER.MCC_Code " & _
             " where TSPL_MCC_ROUTE_MASTER.MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("PCURoute", qry, "Route_Code", "Route_Name", txtRoute.arrValueMember, Nothing)
            RefreshVLC()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        Try
            If txtRoute.arrValueMember Is Nothing OrElse txtRoute.arrValueMember.Count <= 0 Then
                txtRoute.Focus()
                Throw New Exception("Please select at least route")
            End If
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [VLC Uploader Code],TSPL_VLC_MASTER_HEAD.VSP_Code as [VSP Code],TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VENDOR_MASTER.Vendor_Group_Code as [Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Group Name],TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_VLC_MASTER_HEAD.MCC as MCC_Code,TSPL_MCC_MASTER.MCC_NAME from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.MCC left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=TSPL_VENDOR_MASTER.Vendor_Group_Code where TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") and TSPL_VLC_MASTER_HEAD.Active='1' "
            txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC", qry, "VLC_Code", "VLC_Name", txtVLC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub RefreshRoute()
        If isAutoSelectMCCRouteVLC Then
            Dim qry As String = "select Route_Code from TSPL_MCC_ROUTE_MASTER where  MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtRoute.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("Route_Code")))
                Next
                txtRoute.arrValueMember = arr
            End If
            RefreshVLC()
        Else
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                Dim qry As String = "select Route_Code from TSPL_MCC_ROUTE_MASTER where Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                txtRoute.arrValueMember = Nothing
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim arr As New ArrayList
                    For Each dr As DataRow In dt.Rows
                        arr.Add(clsCommon.myCstr(dr("Route_Code")))
                    Next
                    txtRoute.arrValueMember = arr
                End If
            End If
        End If
    End Sub

    Sub RefreshVLC()
        If isAutoSelectMCCRouteVLC Then
            Dim qry As String = "select VLC_Code from TSPL_VLC_MASTER_HEAD where Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtVLC.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("VLC_Code")))
                Next
                txtVLC.arrValueMember = arr
            End If
        Else
            If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                Dim qry As String = "select VLC_Code from TSPL_VLC_MASTER_HEAD where  VLC_Code in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  and Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                txtVLC.arrValueMember = Nothing
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim arr As New ArrayList
                    For Each dr As DataRow In dt.Rows
                        arr.Add(clsCommon.myCstr(dr("VLC_Code")))
                    Next
                    txtVLC.arrValueMember = arr
                End If
            End If
        End If

    End Sub

    Private Sub btnUpdates_Click(sender As Object, e As EventArgs) Handles btnUpdates.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Please select Price code")
            End If
            If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
                Throw New Exception("Please select at least one MCC")
            End If
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "PWD"
            frm.strCode = "UserPWD"
            frm.ShowDialog()
            If Not frm.isPasswordCorrect Then
                Exit Sub
            End If

            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name " + Environment.NewLine + _
            " from TSPL_VLC_MASTER_HEAD " + Environment.NewLine + _
            " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code " + Environment.NewLine + _
            " where TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") and TSPL_VLC_MASTER_HEAD.Active='1' and len( isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 " + Environment.NewLine + _
            " and not exists (select 1 from  TSPL_FAT_SNF_UPLOADER_VLC where Code='" + txtDocNo.Value + "' and TSPL_FAT_SNF_UPLOADER_VLC.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code)"
            Dim arr As ArrayList = clsCommon.ShowMultipleSelectForm("AddVLC", qry, "VLC_Code", "", Nothing, Nothing)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                qry = "select top 40 xx.VLC_Code,max(TSPL_VLC_MASTER_HEAD.VLC_Name) as VLC_Name,max(VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader,MAX(Code) as Code,max(Date) as Date from (" + Environment.NewLine + _
                 "select xx.code,VLC_Code,Date from " + Environment.NewLine + _
                 "(select Code,max(Date) as Date from TSPL_FAT_SNF_UPLOADER_MASTER where Dock_Collection_Milk_Type='M' group by code )xx" + Environment.NewLine + _
                 "left outer join TSPL_FAT_SNF_UPLOADER_VLC on TSPL_FAT_SNF_UPLOADER_VLC.Code=xx.Code" + Environment.NewLine + _
                 "where TSPL_FAT_SNF_UPLOADER_VLC.VLC_Code in (" + clsCommon.GetMulcallString(arr) + ") and Date>'" + clsCommon.GetPrintDate(txtdate.Value, "dd/MMM/yyyy") + "'" + Environment.NewLine + _
                 ")xx left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=xx.VLC_Code group by xx.VLC_Code"
                Dim dtP As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtP IsNot Nothing AndAlso dtP.Rows.Count > 0 Then
                    qry = "Latest price is already tag with (Show 40 VLC) " + Environment.NewLine
                    For Each dr As DataRow In dtP.Rows
                        qry += "VLC: " + clsCommon.myCstr(dr("VLC_Code_VLC_Uploader")) + "[" + dr("VLC_Name") + "]" + "{" + dr("VLC_Code") + "} Price Code: " + clsCommon.myCstr(dr("Code")) + " Date: " + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.myCDate(dr("Date")), "dd/MM/yyyy")) + Environment.NewLine
                    Next
                    qry += "The Price will be applicable only for back date entries" + Environment.NewLine + "Do you want to continue..."
                    If clsCommon.MyMessageBoxShow(qry, Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Exclamation) = System.Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If

                If clsCommon.MyMessageBoxShow("Add " + clsCommon.myCstr(arr.Count) + " VLC in current price." + Environment.NewLine + " Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Try
                        Dim dt As DateTime = clsCommon.GETSERVERDATE(trans)
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            For Each strvalue As String In arr
                                Dim coll As New Hashtable()
                                clsCommon.AddColumnsForChange(coll, "Code", txtDocNo.Value)
                                clsCommon.AddColumnsForChange(coll, "VLC_Code", strvalue)
                                clsCommon.AddColumnsForChange(coll, "Added_By", objCommonVar.CurrentUserCode)
                                clsCommon.AddColumnsForChange(coll, "Added_On", clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm tt"))
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FAT_SNF_UPLOADER_VLC", OMInsertOrUpdate.Insert, "", trans)
                            Next
                            End If
                        trans.Commit()
                        clsCommon.MyMessageBoxShow(Me, "VLC Added Surressfully", Me.Text)
                        LoadData()
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception(ex.Message)
                        End Try
                    End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = " select code from ( select code from TSPL_FAT_SNF_UPLOADER_MASTER group by code )xx where 2=2 "
            Dim whrClas As String = ""
            Select Case NavType
                Case NavigatorType.First
                    qry += " and code = (select MIN(code) from TSPL_FAT_SNF_UPLOADER_MASTER where 1=1 " + whrClas + ")"
                Case NavigatorType.Last
                    qry += " and code = (select Max(code) from TSPL_FAT_SNF_UPLOADER_MASTER where 1=1 " + whrClas + ")"
                Case NavigatorType.Next
                    qry += " and code = (select Min(code) from TSPL_FAT_SNF_UPLOADER_MASTER where code>'" + txtDocNo.Value + "' " + whrClas + ")"
                Case NavigatorType.Previous
                    qry += " and code = (select Max(code) from TSPL_FAT_SNF_UPLOADER_MASTER where code<'" + txtDocNo.Value + "' " + whrClas + ")"
                Case NavigatorType.Current
                    qry += " and code = '" + txtDocNo.Value + "'"
            End Select
            txtDocNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            LoadNew()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select distinct TSPL_FAT_SNF_UPLOADER_MASTER.Code,TSPL_FAT_SNF_UPLOADER_MASTER.Date,TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code as [Price Chart],TSPL_MILK_PRICE_MASTER.Description ,TSPL_FAT_SNF_UPLOADER_MASTER.created_by as [Created By],TSPL_FAT_SNF_UPLOADER_MASTER.created_date as [Created Date],TSPL_FAT_SNF_UPLOADER_MASTER.modified_by as [Modified By],TSPL_FAT_SNF_UPLOADER_MASTER.modified_date as [Modified Date],Planning_Code as [Against Plan Code],TSPL_FAT_SNF_UPLOADER_MASTER.Posted from TSPL_FAT_SNF_UPLOADER_MASTER left outer Join TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code "
        txtDocNo.Value = clsCommon.ShowSelectForm("CHTFND", qry, "Code", "", txtDocNo.Value, "Code", isButtonClicked)
        LoadNew()
    End Sub

    Sub LoadNew()
        If clsCommon.myLen(txtDocNo.Value) > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()

            UcAttachment1.Form_ID = Me.Form_ID
            UcAttachment1.BlankAllControls()
            txtdate.Text = clsDBFuncationality.getSingleValue("select date from TSPL_FAT_SNF_UPLOADER_MASTER where code='" + txtDocNo.Value + "'")
            txtCreatedDate.Text = clsDBFuncationality.getSingleValue("select Created_Date from TSPL_FAT_SNF_UPLOADER_MASTER where code='" + txtDocNo.Value + "'")
            Me.CmbShift.DataSource = ClsOpenMCCShift.GetShift
            Me.CmbShift.DisplayMember = "Name"
            Me.CmbShift.ValueMember = "Code"
            CmbShift.SelectedValue = clsDBFuncationality.getSingleValue("select coalesce(Price_Code_Shift,'M') from TSPL_FAT_SNF_UPLOADER_MASTER where code='" + txtDocNo.Value + "'")
            LoadData()
        Else
            Reset()
        End If
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Reset()
    End Sub

    Private Sub BtnVLCAddHist_Click(sender As Object, e As EventArgs) Handles btnVLCAddHist.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Please Select Code Of Price Chart Uploader")
            End If
            Dim qry As String = "select TSPL_FAT_SNF_UPLOADER_VLC.VLC_Code as [VLC Code],TSPL_USER_MASTER.User_Name as [Added By],convert(varchar,TSPL_FAT_SNF_UPLOADER_VLC.Added_On,113) as [Added On] " &
            " From TSPL_FAT_SNF_UPLOADER_VLC left outer Join TSPL_USER_MASTER On TSPL_USER_MASTER.user_code=TSPL_FAT_SNF_UPLOADER_VLC.Added_By " &
            " where TSPL_FAT_SNF_UPLOADER_VLC.Added_By is not null and TSPL_FAT_SNF_UPLOADER_VLC.code='" + txtDocNo.Value + "'"
            Dim frm As New FrmFreeGrid
            frm.dt = clsDBFuncationality.GetDataTable(qry)
            If frm.dt Is Nothing OrElse frm.dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No data Found", Me.Text)
                Exit Sub
            End If
            frm.strFormName = "Add More VLC History"
            frm.WindowState = FormWindowState.Maximized
            frm.ReportID = "AddMoreVLCH"
            frm.Show()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

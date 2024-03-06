Imports System.Data.SqlClient
Imports common

Public Class FrmPriceChartUploader_MCC
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isAutoSelectMCCRouteVLC As Boolean = False
    Dim settMilkCollectionPickBulkRoute As Boolean = False
    Dim ErrorControl As clsErrorControl = New clsErrorControl()
    Dim fat_Pers As Double = 0
    Dim Snf_pers As Double = 0
#End Region

    Private Sub FrmPriceChartUploader_MCC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME from TSPL_MCC_MASTER"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, Nothing)
        RefreshVLC()
    End Sub

    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        Try
            If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select at least MCC")
            End If
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [VLC Uploader Code],TSPL_VLC_MASTER_HEAD.VSP_Code as [VSP Code],TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VENDOR_MASTER.Vendor_Group_Code as [Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Group Name],TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_VLC_MASTER_HEAD.MCC as MCC_Code,TSPL_MCC_MASTER.MCC_NAME from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.MCC left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=TSPL_VENDOR_MASTER.Vendor_Group_Code where TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") and TSPL_VLC_MASTER_HEAD.Active='1' "
            txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC", qry, "VLC_Code", "VLC_Name", txtVLC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub RefreshVLC()
        If isAutoSelectMCCRouteVLC Then
            Dim qry As String = "select VLC_Code from TSPL_VLC_MASTER_HEAD where MCC in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
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
                Dim qry As String = "select VLC_Code from TSPL_VLC_MASTER_HEAD where  VLC_Code in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  and MCC in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")  "
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

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        'If clsCommon.myLen(txtDocNo.Value) <= 0 Then
        '    clsCommon.MyMessageBoxShow(Me, "Please Fill Doc. No.", Me.Text)
        '    txtDocNo.Focus()
        '    txtDocNo.Select()
        '    Return
        'End If
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
            If txtVLC.arrValueMember Is Nothing OrElse txtVLC.arrValueMember.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select at least one VLC", Me.Text)
                Return
            End If
        End If

        If clsCommon.myLen(CmbShift.SelectedValue) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Shift", Me.Text)
            CmbShift.Focus()
            Return
        End If
        If Not (objCommonVar.SepratePriceChartForCow Or objCommonVar.DisplayTypeInMilkReceipt) Then
            cboDockCollectionMilkType.SelectedValue = "M"
        End If
        Dim axistype As String = clsCommon.myCstr(cmbaxis.Text)
        Dim matrixtype As String = clsCommon.myCstr(cmbmatrix.Text)
        Dim compr_fat As String = ""
        Dim compr_snf As String = ""
        Dim compr_rate As Decimal = Nothing
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Dim OFDFileName As String = ""
        Dim OFDSafeFileName As String = ""

        Dim columnsname As String = transportSql.GetExcelColumnsName(gv1, OFDFileName, OFDSafeFileName)
        Dim currentdate As Date = Nothing
        Dim isSaved As Boolean = True
        compr_fat = fat_Pers.ToString()
        compr_snf = Snf_pers.ToString()
        currentdate = Convert.ToDateTime(txtdate.Text)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim xrangematch As Boolean = False
        Try
            Dim Code As String = clsERPFuncationality.GetNextCode(trans, currentdate, clsDocType.MatrixPriceChart, "", "")
            If clsCommon.myLen(Code) < 0 Then
                Throw New Exception("Error in Code Generation")
            End If
            '===============save MP data======================================
            Dim col As New Hashtable()
            clsCommon.AddColumnsForChange(col, "code", Code)
            clsCommon.AddColumnsForChange(col, "DATE", clsCommon.GetPrintDate(currentdate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(col, "APP_SHIFT", clsCommon.myCstr(CmbShift.SelectedValue))
            clsCommon.AddColumnsForChange(col, "Created_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
            clsCommon.AddColumnsForChange(col, "Created_Date", clsCommon.GetPrintDate(DateTime.Now, "yyyy-MM-dd"))
            clsCommon.AddColumnsForChange(col, "Modified_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
            clsCommon.AddColumnsForChange(col, "Modified_Date", clsCommon.GetPrintDate(DateTime.Now, "yyyy-MM-dd"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(col, "TSPL_FAT_SNF_UPLOADER_MP", OMInsertOrUpdate.Insert, "", trans)

            '===============save BMC data======================================
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                For Each strvalue As String In txtMCC.arrValueMember
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "code", Code)
                    clsCommon.AddColumnsForChange(coll, "BMC_CODE", strvalue, True)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FAT_SNF_UPLOADER_MP_BMC", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            '==================save DCS data======================

            If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                For Each strvalue As String In txtVLC.arrValueMember
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "code", Code)
                    clsCommon.AddColumnsForChange(coll, "DCS_CODE", strvalue, True)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FAT_SNF_UPLOADER_MP_DCS", OMInsertOrUpdate.Insert, "", trans)
                Next
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

                                'If clsCommon.myCdbl(compr_fat) > 0 AndAlso clsCommon.myCdbl(fat) = clsCommon.myCdbl(compr_fat) AndAlso clsCommon.myCdbl(snf) = clsCommon.myCdbl(compr_snf) Then
                                'If clsCommon.myCdbl(compr_rate) = clsCommon.myCdbl(rate) Then
                                '        xrangematch = True
                                '    Else
                                '        Throw New Exception("Uploading Chart Does Not Match The Format of Above Selected Options" + Environment.NewLine + "Please Check The Sheet Or Change The Options." + Environment.NewLine + "It should have  Rate for Fat/SNF " & fat & "/" & snf & " ")
                                '    End If
                                'End If
                                '===============save Rate======================================
                                Dim coll As New Hashtable()
                                clsCommon.AddColumnsForChange(coll, "Code", Code)
                                clsCommon.AddColumnsForChange(coll, "FAT", fat)
                                clsCommon.AddColumnsForChange(coll, "SNF", snf)
                                clsCommon.AddColumnsForChange(coll, "RATE", rate)
                                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FAT_SNF_UPLOADER_Rate", OMInsertOrUpdate.Insert, "", trans)
                            Next
                        End If
                    Next
                End If
            Else
                Throw New Exception("No Data Found For Transfer")
            End If
            'funInsertCharges(Code, trans)
            'If Not xrangematch Then
            '    Throw New Exception("Standart Rate not found in excel sheet")
            'End If
            trans.Commit()
            txtDocNo.Value = Code
            clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
            LoadData()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        Try
            If clsCommon.myCdbl(TxtFindFAT.Text) > 0 And clsCommon.myCdbl(TxtFindSNF.Text) > 0 And clsCommon.myCdbl(TxtFindRate.Text) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please either select FAT & SNF or Rate to Find.", Me.Text)
                Exit Sub
            End If
            If clsCommon.myCdbl(TxtFindFAT.Text) > 0 And clsCommon.myCdbl(TxtFindSNF.Text) > 0 Then
                For Each col As GridViewColumn In gvViewScreen.Columns
                    If col.Name = "" & TxtFindSNF.Text & "" Then
                        For Each row As GridViewRowInfo In gvViewScreen.Rows
                            If row.Cells("FAT").Value = Math.Round(clsCommon.myCdbl(TxtFindFAT.Text), 2) Then
                                gvViewScreen.CurrentColumn = gvViewScreen.Columns(col.Name)
                                gvViewScreen.CurrentRow = row
                            End If
                        Next
                    End If
                Next
            End If
            If clsCommon.myCdbl(TxtFindRate.Text) > 0 Then
                For Each col As GridViewColumn In gvViewScreen.Columns
                    For Each row As GridViewRowInfo In gvViewScreen.Rows
                        If row.Cells(col.Name).Value = clsCommon.myCstr(Math.Round(clsCommon.myCdbl(TxtFindRate.Text), 2)) Then
                            gvViewScreen.CurrentColumn = gvViewScreen.Columns(col.Name)
                            gvViewScreen.CurrentRow = row
                            Exit Sub
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = " select CODE from ( select code from TSPL_FAT_SNF_UPLOADER_MP group by CODE )xx where 2=2 "
            Dim whrClas As String = ""
            Select Case NavType
                Case NavigatorType.First
                    qry += " and CODE = (select MIN(code) from TSPL_FAT_SNF_UPLOADER_MP where 1=1 " + whrClas + ")"
                Case NavigatorType.Last
                    qry += " and CODE = (select Max(code) from TSPL_FAT_SNF_UPLOADER_MP where 1=1 " + whrClas + ")"
                Case NavigatorType.Next
                    qry += " and CODE = (select Min(code) from TSPL_FAT_SNF_UPLOADER_MP where CODE>'" + txtDocNo.Value + "' " + whrClas + ")"
                Case NavigatorType.Previous
                    qry += " and CODE = (select Max(code) from TSPL_FAT_SNF_UPLOADER_MP where CODE<'" + txtDocNo.Value + "' " + whrClas + ")"
                Case NavigatorType.Current
                    qry += " and CODE = '" + txtDocNo.Value + "'"
            End Select
            txtDocNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            LoadNew()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select distinct TSPL_FAT_SNF_UPLOADER_MP.CODE,TSPL_FAT_SNF_UPLOADER_MP.DATE,TSPL_FAT_SNF_UPLOADER_MP.CREATED_BY as [Created By],TSPL_FAT_SNF_UPLOADER_MP.CREATED_DATE as [Created Date],TSPL_FAT_SNF_UPLOADER_MP.MODIFIED_BY as [Modified By],TSPL_FAT_SNF_UPLOADER_MP.MODIFIED_DATE as [Modified Date] from TSPL_FAT_SNF_UPLOADER_MP"
        txtDocNo.Value = clsCommon.ShowSelectForm("CHTFND", qry, "CODE", "", txtDocNo.Value, "CODE", isButtonClicked)
        LoadNew()
    End Sub

    Sub LoadNew()
        If clsCommon.myLen(txtDocNo.Value) > 0 Then
            gvViewScreen.DataSource = Nothing
            gvViewScreen.Rows.Clear()
            gvViewScreen.Columns.Clear()
            txtdate.Text = clsDBFuncationality.getSingleValue("select date from TSPL_FAT_SNF_UPLOADER_MP where CODE='" + txtDocNo.Value + "'")
            txtCreatedDate.Text = clsDBFuncationality.getSingleValue("select Created_Date from TSPL_FAT_SNF_UPLOADER_MP where CODE='" + txtDocNo.Value + "'")
            Me.CmbShift.DataSource = ClsOpenMCCShift.GetShift
            Me.CmbShift.DisplayMember = "Name"
            Me.CmbShift.ValueMember = "Code"
            CmbShift.SelectedValue = clsDBFuncationality.getSingleValue("select coalesce(APP_SHIFT,'M') from TSPL_FAT_SNF_UPLOADER_MP where CODE='" + txtDocNo.Value + "'")
            LoadData()
        Else
            Reset()
        End If
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
        Dim qry As String = "select count(*) from TSPL_FAT_SNF_UPLOADER_Rate where CODE='" + clsCommon.myCstr(txtDocNo.Value) + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Return
        End If


        qry = "select distinct SNF from TSPL_FAT_SNF_UPLOADER_Rate where CODE='" + clsCommon.myCstr(txtDocNo.Value) + "' order by snf"
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
            qry = "select   aa1.FAT," + Sumvalues + " from (select FAT," + values + " from (select ROW_NUMBER() over(PARTITION by fat order by fat) as sno,FAT,rate,snf from TSPL_FAT_SNF_UPLOADER_Rate where code='" + clsCommon.myCstr(txtDocNo.Value) + "') as s pivot(max(rate) for snf in (" + values + ")) as t)aa1 group by aa1.FAT"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvViewScreen.DataSource = dt
                gvViewScreen.Columns("fat").HeaderText = "FAT \ SNF"
                gvViewScreen.Columns("fat").Width = 90
                gvViewScreen.Columns("fat").IsPinned = True
                gvViewScreen.ReadOnly = True
                gvViewScreen.MasterTemplate.ShowRowHeaderColumn = False
                'gv.BestFitColumns()

                gvViewScreen.AllowDeleteRow = False
                gvViewScreen.AllowAddNewRow = False
                gvViewScreen.ShowGroupPanel = False
                gvViewScreen.AllowColumnReorder = False
                gvViewScreen.AllowRowReorder = False
                gvViewScreen.EnableSorting = False
                gvViewScreen.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
                'gv.AutoSizeRows = True
            End If

            qry = "select distinct Date,TSPL_FAT_SNF_UPLOADER_MASTER.CircularNo,TSPL_FAT_SNF_UPLOADER_MASTER.Is_InActive,TSPL_FAT_SNF_UPLOADER_MASTER.Rate_Type,TSPL_FAT_SNF_UPLOADER_MASTER.Axis_Type,TSPL_FAT_SNF_UPLOADER_MASTER.Matrix_Type,TSPL_FAT_SNF_UPLOADER_MASTER.Price_code,Milk_Rate as [Effective Rate],Declared_Rate as [Declared Rate],TSPL_FAT_SNF_UPLOADER_MASTER.Dock_Collection_Milk_Type,TSPL_FAT_SNF_UPLOADER_MASTER.In_Active_From from TSPL_FAT_SNF_UPLOADER_MASTER left join TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.price_code=TSPL_FAT_SNF_UPLOADER_MASTER.price_code  where code='" + clsCommon.myCstr(txtDocNo.Value) + "'"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                cboDockCollectionMilkType.SelectedValue = clsCommon.myCstr(dt.Rows(0)("Dock_Collection_Milk_Type"))
                txtdate.Text = clsCommon.myCDate(dt.Rows(0)("Date"))
                cmbaxis.Text = clsCommon.myCstr(dt.Rows(0)("axis_type"))
                cmbmatrix.Text = clsCommon.myCstr(dt.Rows(0)("matrix_type"))
                cmbaxis.Enabled = False
                cmbmatrix.Enabled = False
                'If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Is_InActive")), "1") = CompairStringResult.Equal Then
                '    txtInactiveFrom.Checked = 1
                '    txtInactiveFrom.Value = clsCommon.myCDate(dt.Rows(0)("In_Active_From"))
                'Else
                '    txtInactiveFrom.Checked = 0
                '    txtInactiveFrom.Value = clsCommon.GETSERVERDATE()
                'End If
                'txtCircularNo.Text = clsCommon.myCstr(dt.Rows(0)("CircularNo"))

                'If Not cmbrate.Items.Contains(clsCommon.myCdbl(dt.Rows(0)("Effective Rate"))) Then
                '    cmbrate.Items.Add(clsCommon.myCdbl(dt.Rows(0)("Effective Rate")))
                'End If
                'cmbrate.Text = clsCommon.myCdbl(dt.Rows(0)("Effective Rate"))
                'CmbStandardRate.Items.Clear()
                'If Not CmbStandardRate.Items.Contains(clsCommon.myCdbl(dt.Rows(0)("Declared Rate"))) Then
                '    CmbStandardRate.Items.Add(clsCommon.myCdbl(dt.Rows(0)("Declared Rate")))
                'End If
                'CmbStandardRate.Text = clsCommon.myCdbl(dt.Rows(0)("Declared Rate"))
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
            qry = "select coalesce(posted,'0') from TSPL_FAT_SNF_UPLOADER_Rate where code='" & clsCommon.myCstr(txtDocNo.Value) & "'"
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
            'txtRoute.arrValueMember = arr
            'UcAttachment1.LoadData(txtDocNo.Value)
            'GetChargesData(txtDocNo.Value, NavigatorType.Current)
            If isPosted = "1" Then
                btnPost.Enabled = False

            Else
                btnPost.Enabled = True

            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Return
        End If
    End Sub


    Sub Reset()
        'IsInsieLoadData = False
        'cmbrate.Items.Clear()
        'FndPriceCode.Value = Nothing
        'cmbrate.Items.Add("None")
        'cmbratetype.Text = "None"
        cmbmatrix.Text = "None"
        cmbaxis.Text = "None"
        'cmbrate.Text = "None"
        Me.CmbShift.DataSource = ClsOpenMCCShift.GetShift
        Me.CmbShift.DisplayMember = "Name"
        Me.CmbShift.ValueMember = "Code"
        Me.CmbShift.SelectedIndex = -1
        'cmbratetype.Enabled = True
        cmbaxis.Enabled = True
        cmbmatrix.Enabled = True
        'BtnPost.Enabled = True
        'btnUpdates.Enabled = True
        'BtnSaveCharge.Enabled = True
        'btnUpdates.Enabled = False
        'cmbrate.Enabled = True
        txtDocNo.Value = ""
        txtDocNo.MyReadOnly = False
        txtdate.Text = clsCommon.GETSERVERDATE()
        txtCreatedDate.Text = clsCommon.GETSERVERDATE()
        gvViewScreen.DataSource = Nothing
        gvViewScreen.Rows.Clear()
        gvViewScreen.Columns.Clear()
        'gvCharges.Rows.Clear()
        'LoadBlank_Charges_Grid()
        'UcAttachment1.Form_ID = Me.Form_ID
        'UcAttachment1.BlankAllControls()
        'txtfat.Value = 0
        'txtsnf.Value = 0
        'txtInactiveFrom.Checked = False
        'txtCircularNo.Text = ""
        txtMCC.arrValueMember = Nothing
        'txtRoute.arrValueMember = Nothing
        txtVLC.arrValueMember = Nothing
    End Sub



    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Reset()
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            Dim str As String = "update TSPL_FAT_SNF_UPLOADER_Rate set Posted='1' where code='" & clsCommon.myCstr(txtDocNo.Value) & "'"
            clsDBFuncationality.ExecuteNonQuery(str)
            clsCommon.MyMessageBoxShow(Me, "Price Chart Posted Successfully...", Me.Text)
            btnPost.Enabled = False
            'BtnSaveCharge.Enabled = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click

    End Sub
End Class

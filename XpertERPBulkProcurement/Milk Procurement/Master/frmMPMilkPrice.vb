Imports System.Data.SqlClient
Imports common

Public Class frmMPMilkPrice
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ErrorControl As clsErrorControl = New clsErrorControl()

#End Region

    Private Sub FrmPriceChartUploader_MCC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
        SetUserMgmtNew()
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnImport.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnExport.Visible = MyBase.isExport
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
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Try
            If clsCommon.myLen(txtdate.Text) <= 0 Then
                txtdate.Focus()
                Throw New Exception("Please Fill Date")
            End If
            If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select at least one BMC/MCC")
            End If
            If txtVLC.arrValueMember Is Nothing OrElse txtVLC.arrValueMember.Count <= 0 Then
                Throw New Exception("Please select at least one DCS")
            End If
            If clsCommon.myLen(CmbShift.SelectedValue) <= 0 Then
                CmbShift.Focus()
                Throw New Exception("Please select Shift")
            End If
            If Not (objCommonVar.SepratePriceChartForCow Or objCommonVar.DisplayTypeInMilkReceipt) Then
                cboDockCollectionMilkType.SelectedValue = "M"
            End If
            Dim axistype As String = clsCommon.myCstr(cmbaxis.Text)
            Dim matrixtype As String = clsCommon.myCstr(cmbmatrix.Text)


            Dim gv1 As New RadGridView()
            Me.Controls.Add(gv1)
            Dim OFDFileName As String = ""
            Dim OFDSafeFileName As String = ""

            Dim columnsname As String = transportSql.GetExcelColumnsName(gv1, OFDFileName, OFDSafeFileName)
            Dim currentdate As Date = Nothing

            currentdate = Convert.ToDateTime(txtdate.Text)
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            clsCommon.ProgressBarPercentShow()
            Try
                '===============save MP data======================================
                Dim col As New Hashtable()
                clsCommon.AddColumnsForChange(col, "DATE", clsCommon.GetPrintDate(currentdate, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(col, "APP_SHIFT", clsCommon.myCstr(CmbShift.SelectedValue))
                clsCommon.AddColumnsForChange(col, "Created_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                clsCommon.AddColumnsForChange(col, "Created_Date", clsCommon.GetPrintDate(DateTime.Now, "yyyy-MM-dd"))
                clsCommon.AddColumnsForChange(col, "Modified_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                clsCommon.AddColumnsForChange(col, "Modified_Date", clsCommon.GetPrintDate(DateTime.Now, "yyyy-MM-dd"))
                clsCommonFunctionality.UpdateDataTable(col, "TSPL_MP_MILK_PRICE", OMInsertOrUpdate.Insert, "", trans)
                Dim Code As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select max(Code) from TSPL_MP_MILK_PRICE", trans))
                If Code <= 0 Then
                    Throw New Exception("Error in code generation")
                End If
                '===============save BMC data======================================
                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    For ii As Integer = 0 To txtMCC.arrValueMember.Count - 1
                        clsCommon.ProgressBarPercentUpdate(ii + 1, txtMCC.arrValueMember.Count, "Importing BMC")

                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "code", Code)
                        clsCommon.AddColumnsForChange(coll, "BMC_CODE", txtMCC.arrValueMember(ii), True)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_MILK_PRICE_BMC", OMInsertOrUpdate.Insert, "", trans)
                    Next
                End If

                '==================save DCS data======================

                If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                    For ii As Integer = 0 To txtVLC.arrValueMember.Count - 1
                        clsCommon.ProgressBarPercentUpdate(ii + 1, txtVLC.arrValueMember.Count, "Importing DCS")

                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "code", Code)
                        clsCommon.AddColumnsForChange(coll, "DCS_CODE", txtVLC.arrValueMember(ii), True)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_MILK_PRICE_DCS", OMInsertOrUpdate.Insert, "", trans)
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
                                    clsCommon.ProgressBarPercentUpdate(ii + 1, gv1.Rows.Count, "Importing FAT/SNF Matrix Row[" + clsCommon.myCstr(ii) + "] Column[" + clsCommon.myCstr(jj) + "]")

                                    Dim rate As Decimal = Nothing
                                    Try
                                        rate = Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(jj).Value), 3)
                                    Catch exx As Exception
                                    End Try
                                    Try
                                        snf = Math.Truncate(clsCommon.myCdbl(gv1.Columns(jj).HeaderText.Replace("#", ".")) * 10) / 10
                                    Catch exx As Exception
                                    End Try


                                    Dim coll As New Hashtable()
                                    clsCommon.AddColumnsForChange(coll, "Code", Code)
                                    clsCommon.AddColumnsForChange(coll, "FAT", fat)
                                    clsCommon.AddColumnsForChange(coll, "SNF", snf)
                                    clsCommon.AddColumnsForChange(coll, "RATE", rate)
                                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_MILK_PRICE_RATE", OMInsertOrUpdate.Insert, "", trans)
                                Next
                            End If
                        Next
                    End If
                Else
                    Throw New Exception("No Data Found For Transfer")
                End If
                trans.Commit()
                clsCommon.ProgressBarPercentHide()
                txtDocNo.Value = Code
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
                LoadData()
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        Try
            If clsCommon.myCdbl(TxtFindFAT.Text) > 0 And clsCommon.myCdbl(TxtFindSNF.Text) > 0 And clsCommon.myCdbl(TxtFindRate.Text) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please either select FAT/SNF or Rate to Search.", Me.Text)
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
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = " select CODE from ( select code from TSPL_MP_MILK_PRICE group by CODE )xx where 2=2 "
            Dim whrClas As String = ""
            Select Case NavType
                Case NavigatorType.First
                    qry += " and CODE = (select MIN(code) from TSPL_MP_MILK_PRICE where 1=1 " + whrClas + ")"
                Case NavigatorType.Last
                    qry += " and CODE = (select Max(code) from TSPL_MP_MILK_PRICE where 1=1 " + whrClas + ")"
                Case NavigatorType.Next
                    qry += " and CODE = (select Min(code) from TSPL_MP_MILK_PRICE where CODE>'" + txtDocNo.Value + "' " + whrClas + ")"
                Case NavigatorType.Previous
                    qry += " and CODE = (select Max(code) from TSPL_MP_MILK_PRICE where CODE<'" + txtDocNo.Value + "' " + whrClas + ")"
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
        Dim qry As String = "select distinct TSPL_MP_MILK_PRICE.CODE,TSPL_MP_MILK_PRICE.DATE,TSPL_MP_MILK_PRICE.CREATED_BY as [Created By],TSPL_MP_MILK_PRICE.CREATED_DATE as [Created Date],TSPL_MP_MILK_PRICE.MODIFIED_BY as [Modified By],TSPL_MP_MILK_PRICE.MODIFIED_DATE as [Modified Date] from TSPL_MP_MILK_PRICE"
        txtDocNo.Value = clsCommon.ShowSelectForm("CHTFND", qry, "CODE", "", txtDocNo.Value, "CODE", isButtonClicked)
        LoadNew()
    End Sub

    Sub LoadNew()
        If clsCommon.myLen(txtDocNo.Value) > 0 Then
            gvViewScreen.DataSource = Nothing
            gvViewScreen.Rows.Clear()
            gvViewScreen.Columns.Clear()
            txtdate.Text = clsDBFuncationality.getSingleValue("select date from TSPL_MP_MILK_PRICE where CODE='" + txtDocNo.Value + "'")
            txtCreatedDate.Text = clsDBFuncationality.getSingleValue("select Created_Date from TSPL_MP_MILK_PRICE where CODE='" + txtDocNo.Value + "'")
            Me.CmbShift.DataSource = ClsOpenMCCShift.GetShift
            Me.CmbShift.DisplayMember = "Name"
            Me.CmbShift.ValueMember = "Code"
            CmbShift.SelectedValue = clsDBFuncationality.getSingleValue("select coalesce(APP_SHIFT,'M') from TSPL_MP_MILK_PRICE where CODE='" + txtDocNo.Value + "'")
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
        Dim qry As String = "select count(*) from TSPL_MP_MILK_PRICE_RATE where CODE='" + clsCommon.myCstr(txtDocNo.Value) + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Return
        End If


        qry = "select distinct SNF from TSPL_MP_MILK_PRICE_RATE where CODE='" + clsCommon.myCstr(txtDocNo.Value) + "' order by snf"
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
            qry = "select   aa1.FAT," + Sumvalues + " from (select FAT," + values + " from (select ROW_NUMBER() over(PARTITION by fat order by fat) as sno,FAT,rate,snf from TSPL_MP_MILK_PRICE_RATE where code='" + clsCommon.myCstr(txtDocNo.Value) + "') as s pivot(max(rate) for snf in (" + values + ")) as t)aa1 group by aa1.FAT"
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

            End If



            '=======================MCC========================
            qry = "select BMC_Code from TSPL_MP_MILK_PRICE_BMC where code='" + txtDocNo.Value + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim arr As New ArrayList()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("BMC_Code")))
                Next
                txtMCC.arrValueMember = arr
            End If

            '===========VLC==================================


            qry = "select TSPL_MP_MILK_PRICE_DCS.DCS_Code from TSPL_MP_MILK_PRICE_DCS  where code = '" + txtDocNo.Value + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)

            Dim arrVLC As New ArrayList()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    arrVLC.Add(clsCommon.myCstr(dr("DCS_Code")))

                Next
            End If
            txtVLC.arrValueMember = arrVLC

            qry = "select  Status from TSPL_MP_MILK_PRICE where code='" & txtDocNo.Value & "'"
            Dim intStatus As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
            If intStatus = 1 Then
                btnPost.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Return
        End If
    End Sub


    Sub Reset()
        cmbmatrix.Text = "None"
        cmbaxis.Text = "None"
        Me.CmbShift.DataSource = ClsOpenMCCShift.GetShift
        Me.CmbShift.DisplayMember = "Name"
        Me.CmbShift.ValueMember = "Code"
        Me.CmbShift.SelectedIndex = -1
        cmbaxis.Enabled = True
        cmbmatrix.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDocNo.Value = ""
        txtDocNo.MyReadOnly = False
        txtdate.Text = clsCommon.GETSERVERDATE()
        txtCreatedDate.Text = clsCommon.GETSERVERDATE()
        gvViewScreen.DataSource = Nothing
        gvViewScreen.Rows.Clear()
        gvViewScreen.Columns.Clear()
        txtMCC.arrValueMember = Nothing
        txtVLC.arrValueMember = Nothing
    End Sub



    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Reset()
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If (myMessages.postConfirm()) Then
                    Dim str As String = "update TSPL_MP_MILK_PRICE set Status=1,Posted_By='" + objCommonVar.CurrentUserCode + "'
,Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm:ss tt") + "' 
where code='" & clsCommon.myCstr(txtDocNo.Value) & "'"
                    clsDBFuncationality.ExecuteNonQuery(str)
                    clsCommon.MyMessageBoxShow(Me, "Successfully Posted.", Me.Text)
                    LoadData()
                    btnPost.Enabled = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try

            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If (myMessages.deleteConfirm()) Then
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Try
                        Dim qry As String = "Delete from TSPL_MP_MILK_PRICE where Code='" + txtDocNo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        qry = "Delete from TSPL_MP_MILK_PRICE_BMC where Code='" + txtDocNo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        qry = "Delete from TSPL_MP_MILK_PRICE_DCS where Code='" + txtDocNo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        qry = "Delete from TSPL_MP_MILK_PRICE_RATE where Code='" + txtDocNo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        trans.Commit()
                        clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                        Reset()
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception(ex.Message)
                    End Try
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Info)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim qry As String = "select '4.0' as [FAT/SNF],'23' as '8','28' as '8.1','30' as '8.2','32' as '8.3'union all select  '4.1','24','28','30','32' union all select  '4.2','26','28','30','32'"
        transportSql.ExporttoExcel(qry, Me)
    End Sub
End Class

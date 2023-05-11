Imports common
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.IO
Public Class FrmVSPIncentiveTagging
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean
    Dim isnewentry As Boolean = False
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Public Const RowTypeM As String = "M"
    Public Const RowTypeE As String = "E"
    Const colVSPCode As String = "COLVSPCODE"
    Const colselect As String = "colselect"
    Const colRowType As String = "COLTYPE"
    Const colType As String = "COLTYPEShift"
    Const colVSPName As String = "COLVSPNAME"
    Const colStartDate As String = "COLStartDate"
    Const colStartShift As String = "StartShift"
    Const colINCENTIVE_CODE As String = "colINCENTIVE_CODE"
    Const colEndDate As String = "ColEndDate"
    Const colEndShift As String = "ColEndShift"
    Const colComplete As String = "COMPLETE"
    Dim repoComplete As GridViewTextBoxColumn
#End Region
   
    Private Sub FndMCC__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndMCC._MYValidating
        Dim qry As String = ""
        qry = "Select MCC_Code,MCC_Name from TSPL_MCC_Master"
        FndMCC.Value = clsCommon.ShowSelectForm("TSPL_MCC_Master", qry, "MCC_Code", "", FndMCC.Value, "TSPL_MCC_Master.MCC_Code", isButtonClicked)
        If clsCommon.myLen(FndMCC.Value) > 0 Then
            lblMCCName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MCC_Name from TSPL_MCC_Master where MCC_Code ='" + FndMCC.Value + "'"))
        Else
            lblMCCName.Text = ""
        End If
    End Sub

    'Private Sub FndIncentive__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
    '    Dim qry As String = ""
    '    qry = "Select Incentive_Code,Description from TSPL_INCENTIVE_MASTER_HEAD"
    '    FndIncentive.Value = clsCommon.ShowSelectForm("TSPL_INCENTIVE_MASTER_HEAD", qry, "Incentive_Code", "", FndIncentive.Value, "TSPL_INCENTIVE_MASTER_HEAD.Incentive_Code", isButtonClicked)
    '    If clsCommon.myLen(FndIncentive.Value) > 0 Then
    '        lblIncentiveName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description from TSPL_INCENTIVE_MASTER_HEAD where Incentive_Code='" + FndIncentive.Value + "' "))
    '    End If
    'End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        btnSave.Enabled = True
        Dim objList As List(Of clsVSPIncentiveTagging) = clsVSPIncentiveTagging.GetData(strCode, NavTyep, Nothing)
        Dim arrRoute As ArrayList = Nothing
        Dim arrIncentive As ArrayList = Nothing
        Dim arrVSP As ArrayList = Nothing
        If (objList IsNot Nothing AndAlso objList.Count > 0) Then
            arrRoute = New ArrayList()
            arrIncentive = New ArrayList
            arrVSP = New ArrayList

            For Each obj As clsVSPIncentiveTagging In objList
                If Not arrRoute.Contains(obj.Route_code) Then
                    arrRoute.Add(obj.Route_CODE)
                End If
                If Not arrIncentive.Contains(obj.INCENTIVE_CODE) Then
                    arrIncentive.Add(obj.INCENTIVE_CODE)
                End If
                If Not arrVSP.Contains(obj.VSP_CODE) Then
                    arrVSP.Add(obj.VSP_CODE)
                End If
            Next
           
            For Each obj As clsVSPIncentiveTagging In objList
                Dim ii As Int16 = 0
                fndDocCode.Value = obj.Doc_Code
                Me.FndMCC.Value = clsCommon.myCstr(obj.MCC_Code)
                Me.lblMCCName.Text = clsCommon.myCstr(obj.Mcc_Name)
                gv.Rows.AddNew()
                gv.Rows(gv.Rows.Count - 1).Cells(colselect).Value = clsCommon.myCBool(obj.VSPSelect)
                gv.Rows(gv.Rows.Count - 1).Cells(colVSPCode).Value = clsCommon.myCstr(obj.VSP_CODE)
                gv.Rows(gv.Rows.Count - 1).Cells(colVSPName).Value = clsCommon.myCstr(obj.VSP_Name)
                gv.Rows(gv.Rows.Count - 1).Cells(colINCENTIVE_CODE).Value = clsCommon.myCstr(obj.INCENTIVE_CODE)
                gv.Rows(gv.Rows.Count - 1).Cells(colStartDate).Value = clsCommon.myCDate(obj.StartDate)
                gv.Rows(gv.Rows.Count - 1).Cells(colType).Value = clsCommon.myCstr(obj.StartShift)
                gv.Rows(gv.Rows.Count - 1).Cells(colEndDate).Value = clsCommon.myCDate(obj.EndDate)
                gv.Rows(gv.Rows.Count - 1).Cells(colRowType).Value = clsCommon.myCstr(obj.EndShift)
                'Exit For
            Next
            Me.txtIncentiveMult.arrValueMember = arrIncentive
            txtRoute.arrValueMember = arrRoute
            txtVSP.arrValueMember = arrVSP
            isnewentry = False
            btnSave.Text = "Update"
            'Load_Report(strCode)
        End If
        'txtRoute.arrValueMember = arrRoute
    End Sub
    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim reposelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        reposelect.FormatString = ""
        reposelect.HeaderText = "Select"
        reposelect.Name = colselect
        gv.MasterTemplate.Columns.Add(reposelect)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "VSP Code"
        repoICode.Name = colVSPCode
        repoICode.Width = 100
        gv.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "VSP Name"
        repoIName.Name = colVSPName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIName)

        Dim repoIncentive As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIncentive.FormatString = ""
        repoIncentive.HeaderText = "Incentive Name"
        repoIncentive.Name = colINCENTIVE_CODE
        repoIncentive.Width = 80
        repoIncentive.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIncentive)

        Dim repoStartDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoStartDate.Format = DateTimePickerFormat.Custom
        repoStartDate.CustomFormat = "dd-MM-yyyy"
        repoStartDate.FormatString = "{0:d}"
        repoStartDate.HeaderText = "Start Date"
        repoStartDate.Name = colStartDate
        repoStartDate.Width = 80
        repoStartDate.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoStartDate)

        Dim repoStartShift As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoStartShift.FormatString = ""
        repoStartShift.HeaderText = "Shift"
        repoStartShift.Name = colType
        repoStartShift.Width = 50
        repoStartShift.ReadOnly = False
        repoStartShift.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoStartShift.DataSource = GetTypeShift()
        repoStartShift.ValueMember = "Code"
        repoStartShift.DisplayMember = "Code"
        gv.MasterTemplate.Columns.Add(repoStartShift)

        Dim repoenddate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoenddate.Format = DateTimePickerFormat.Custom
        repoenddate.CustomFormat = "dd-MM-yyyy"
        repoenddate.FormatString = "{0:d}"
        repoenddate.FormatString = ""
        repoenddate.HeaderText = "End Date"
        repoenddate.Name = colEndDate
        repoenddate.Width = 80
        repoenddate.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoenddate)


        repoComplete = New GridViewTextBoxColumn()
        repoComplete.FormatString = ""
        repoComplete.HeaderText = "Complete"
        repoComplete.Width = 70
        repoComplete.Name = colComplete
        repoComplete.ReadOnly = True
        repoComplete.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoComplete)

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Shift"
        repoRowType.Name = colRowType
        repoRowType.Width = 50
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetItemType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Code"
        gv.MasterTemplate.Columns.Add(repoRowType)


        clsCustomFieldGrid.LoadBlankGrid(gv, MyBase.ArrDetailFields)

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40
        ReStoreGridLayout()
    End Sub
    Private Function GetItemType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = RowTypeM
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = RowTypeE
        dt.Rows.Add(dr)

        Return dt
    End Function
    Private Function GetTypeShift() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = RowTypeM
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = RowTypeE
        dt.Rows.Add(dr)

        Return dt
    End Function
    Public Sub Load_Report(Optional ByVal strdoc As String = Nothing)
        Try
            LoadBlankGrid()
            Dim sQuery As String = ""
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                sQuery = " select TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE,convert(varchar,TSPL_INCENTIVE_MASTER_HEAD.START_DATE,103) as Start_date,TSPL_INCENTIVE_MASTER_HEAD.Starting_Shift,TSPL_INCENTIVE_MASTER_HEAD.Ending_Shift,convert(varchar,TSPL_INCENTIVE_MASTER_HEAD.END_DATE,103) as End_Date " + Environment.NewLine + _
           " from TSPL_VLC_MASTER_HEAD  " + Environment.NewLine + _
           " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code= TSPL_VLC_MASTER_Head.MCC " + Environment.NewLine + _
           " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_Head.VSP_Code " + Environment.NewLine + _
           " cross join TSPL_INCENTIVE_MASTER_HEAD " + Environment.NewLine + _
           " where  TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") and TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE in (" + clsCommon.GetMulcallString(txtIncentiveMult.arrValueMember) + ") order by VSP_Code"
            Else
                sQuery = "select  TSPL_VSP_INCENTIVE_Detail.Doc_Code,TSPL_VSP_INCENTIVE_Detail.Mcc_Code,Mcc_Name, tspl_Incentive_Master_Head.description as Incentive_name,TSPL_VSP_INCENTIVE_Detail.Incentive_Code,VLC_Code,VLC_Name,TSPL_VENDOR_MASTER.Vendor_Code as VSP_Code, Vendor_Name,TSPL_VLC_MASTER_Head.Route_Code,convert(varchar,TSPL_INCENTIVE_MASTER_HEAD.START_DATE,103) as Start_date,TSPL_INCENTIVE_MASTER_HEAD.Starting_Shift,TSPL_INCENTIVE_MASTER_HEAD.Ending_Shift,convert(varchar,TSPL_INCENTIVE_MASTER_HEAD.END_DATE,103) as End_Date from TSPL_MCC_MASTER inner join TSPL_VSP_INCENTIVE_Detail on TSPL_VSP_INCENTIVE_Detail.mcc_code=TSPL_MCC_MASTER.mcc_code inner join  tspl_Incentive_Master_Head on tspl_Incentive_Master_Head.incentive_Code=TSPL_VSP_INCENTIVE_Detail.incentive_Code inner join TSPL_VLC_MASTER_Head  on  TSPL_VLC_MASTER_Head.mcc = TSPL_MCC_MASTER.MCC_Code inner join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_Head.VSP_Code and TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSP_INCENTIVE_Detail.VSP_Code  AND DOC_CODE = '" + strdoc + "'"
            End If


            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(sQuery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                For Each dr As DataRow In dtgv.Rows
                    gv.Rows.AddNew()
                    gv.CurrentRow.Cells(colVSPCode).Value = clsCommon.myCstr(dr("VSP_Code"))
                    gv.CurrentRow.Cells(colVSPName).Value = clsCommon.myCstr(dr("VSP Name"))
                    gv.CurrentRow.Cells(colStartDate).Value = clsCommon.myCstr(dr("START_DATE"))
                    gv.CurrentRow.Cells(colType).Value = clsCommon.myCstr(dr("Starting_Shift"))
                    gv.CurrentRow.Cells(colINCENTIVE_CODE).Value = clsCommon.myCstr(dr("INCENTIVE_CODE"))
                    gv.CurrentRow.Cells(colRowType).Value = clsCommon.myCstr(dr("Ending_Shift"))
                    gv.CurrentRow.Cells(colEndDate).Value = clsCommon.myCstr(dr("End_Date"))
                    'gv.CurrentRow.Cells(colEndDate).Value = clsCommon.myCstr(dr("END_DATE"))
                Next
                'gv.DataSource = Nothing
                'gv.Rows.Clear()
                'gv.Columns.Clear()
                'gv.DataSource = dtgv
                'gv.GroupDescriptors.Clear()
                'gv.MasterTemplate.SummaryRowsBottom.Clear()
                'gv.BestFitColumns()
                ' FormatGrid()
                'RadPageView1.SelectedPage = RadPageViewPage2
                ReStoreGridLayout()
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If

        Catch ex As Exception

            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub FormatGrid()
        LoadBlankGrid()
        gv.TableElement.TableHeaderHeight = 20
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("VSP_Code").IsVisible = True
        gv.Columns("VSP_Code").Width = 100
        gv.Columns("VSP_Code").HeaderText = " VSP Code"

        gv.Columns("Vendor_Name").IsVisible = True
        gv.Columns("Vendor_Name").Width = 100
        gv.Columns("Vendor_Name").HeaderText = "Vendor Name"

        gv.Columns("START_DATE").IsVisible = True
        gv.Columns("START_DATE").Width = 100
        gv.Columns("START_DATE").HeaderText = "Start Date"
        gv.Columns("START_DATE").FormatString = "{0:d}"

        gv.Columns("Starting_Shift").IsVisible = True
        gv.Columns("Starting_Shift").Width = 100
        gv.Columns("Starting_Shift").HeaderText = "Shift"

        gv.Columns("END_DATE").IsVisible = True
        gv.Columns("END_DATE").Width = 100
        gv.Columns("END_DATE").HeaderText = "End Date"
        gv.Columns("END_DATE").FormatString = "{0:d}"

        gv.Columns("Ending_Shift").IsVisible = True
        gv.Columns("Ending_Shift").Width = 100
        gv.Columns("Ending_Shift").HeaderText = "Shift"


    End Sub
    Sub SaveData()
        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.FrmVSPIncentiveTagging, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            Dim VSP As New clsVSPIncentiveTagging
            Dim arr As New List(Of clsVSPIncentiveTagging)
            Dim count As Integer = 0
            Dim i As Integer = 0
            Dim J As Integer = 0

            'If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
            '    For i = 0 To txtVSP.arrValueMember.Count - 1
            '        For J = 0 To txtIncentiveMult.arrValueMember.Count - 1
            '            VSP = New clsVSPIncentiveTagging()
            '            VSP.VSP_CODE = clsCommon.myCstr(txtVSP.arrValueMember(i))
            '            VSP.Doc_Code = fndDocCode.Value
            '            VSP.INCENTIVE_CODE = clsCommon.myCstr(txtIncentiveMult.arrValueMember(J))
            '            VSP.MCC_Code = FndMCC.Value
            '            arr.Add(VSP)
            '        Next

            '    Next
            'End If

            For Each grow As GridViewRowInfo In gv.Rows
                If clsCommon.myCBool(grow.Cells(colselect).Value) Then
                    VSP = New clsVSPIncentiveTagging()
                    VSP.VSP_CODE = grow.Cells(colVSPCode).Value
                    VSP.Doc_Code = fndDocCode.Value
                    VSP.MCC_Code = FndMCC.Value
                    VSP.INCENTIVE_CODE = grow.Cells(colINCENTIVE_CODE).Value
                    VSP.StartDate = clsCommon.myCDate(grow.Cells(colStartDate).Value)
                    VSP.StartShift = clsCommon.myCstr(grow.Cells(colType).Value)
                    VSP.EndDate = clsCommon.myCDate(grow.Cells(colEndDate).Value)
                    VSP.EndShift = clsCommon.myCstr(grow.Cells(colRowType).Value)
                    VSP.VSPSelect = clsCommon.myCBool(grow.Cells(colselect).Value)

                    arr.Add(VSP)
                End If
            Next

            If arr Is Nothing OrElse arr.Count <= 0 Then
                Throw New Exception("Please select atleast one row in grid.")
            End If

            If (clsVSPIncentiveTagging.SaveData(VSP, arr, isnewentry)) Then
                clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                fndDocCode.Value = VSP.Doc_Code
                btnSave.Text = "Update"
            Else
                btnSave.Text = "Save"
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.FrmVSPIncentiveTagging)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag

    End Sub

    Private Sub FrmVSPIncentiveTagging_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isnewentry = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")

        'ButtonToolTip.SetToolTip(butnClose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Sub funReset()
        isNewEntry = True
        fndDocCode.MyReadOnly = False
        fndDocCode.Value = Nothing
        fndDocCode.Focus()
        txtIncentiveMult.arrValueMember = Nothing
        FndMCC.Value = Nothing        
        lblMCCName.Text = Nothing
        btnSave.Text = "Save"
        btnsave.Enabled = True
        txtVSP.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing

        LoadBlankGrid()
        gv.Rows.Clear()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnReset.Click
        Try
            funReset()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub fndDocCode__MYNavigator1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocCode._MYNavigator
        Try
            LoadData(fndDocCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndDocCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocCode._MYValidating
        Try
            fndDocCode.Value = clsCommon.myCstr(clsVSPIncentiveTagging.GetFinder("", fndDocCode.Value, isButtonClicked))
            LoadData(fndDocCode.Value, NavigatorType.Current)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim qry As String = "select count(*) from TSPL_VSP_INCENTIVE_Detail"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        Dim whrcls As String = " where 2=2 "
        If check > 0 Then
            qry = "select TSPL_VSP_INCENTIVE_Detail.MCC_code as [MCC Code],MCC_Name as [MCC Name],TSPL_VLC_MASTER_Head.VSP_Code as [VSP Code],Vendor_Name as [VSP Name],TSPL_VSP_INCENTIVE_Detail.Incentive_code as [Incentive Code],Description as [Incentive Name],Doc_Code as [Document Code] ,VLC_Code as [VLC code],VLC_Name as [VLC Name],TSPL_VSP_INCENTIVE_Detail.StartDate as [Start Date],TSPL_VSP_INCENTIVE_Detail.StartShift as [Start Shift],TSPL_VSP_INCENTIVE_Detail.EndDate as [End Date],TSPL_VSP_INCENTIVE_Detail.EndShift as [End Shift] from TSPL_MCC_MASTER inner join TSPL_VSP_INCENTIVE_Detail on TSPL_VSP_INCENTIVE_Detail.mcc_code=TSPL_MCC_MASTER.mcc_code inner join  tspl_Incentive_Master_Head on tspl_Incentive_Master_Head.incentive_Code=TSPL_VSP_INCENTIVE_Detail.incentive_Code inner join TSPL_VLC_MASTER_Head  on  TSPL_VLC_MASTER_Head.mcc = TSPL_MCC_MASTER.MCC_Code inner join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_Head.VSP_Code and TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSP_INCENTIVE_Detail.VSP_Code  "
            ' qry = "select  TSPL_VSP_INCENTIVE_Detail.Doc_Code,TSPL_VSP_INCENTIVE_Detail.Mcc_Code,Mcc_Name, tspl_Incentive_Master_Head.description as Incentive_name,TSPL_VSP_INCENTIVE_Detail.Incentive_Code,VLC_Code,VLC_Name,TSPL_VENDOR_MASTER.Vendor_Code as VSP_Code, Vendor_Name,TSPL_VLC_MASTER_Head.Route_Code,convert(varchar,TSPL_INCENTIVE_MASTER_HEAD.START_DATE,103) as Start_date,TSPL_INCENTIVE_MASTER_HEAD.Starting_Shift,TSPL_INCENTIVE_MASTER_HEAD.Ending_Shift,convert(varchar,TSPL_INCENTIVE_MASTER_HEAD.END_DATE,103) as End_Date from TSPL_MCC_MASTER inner join TSPL_VSP_INCENTIVE_Detail on TSPL_VSP_INCENTIVE_Detail.mcc_code=TSPL_MCC_MASTER.mcc_code inner join  tspl_Incentive_Master_Head on tspl_Incentive_Master_Head.incentive_Code=TSPL_VSP_INCENTIVE_Detail.incentive_Code inner join TSPL_VLC_MASTER_Head  on  TSPL_VLC_MASTER_Head.mcc = TSPL_MCC_MASTER.MCC_Code inner join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_Head.VSP_Code and TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSP_INCENTIVE_Detail.VSP_Code "
            If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
                whrcls += " and TSPL_VSP_INCENTIVE_Detail.VSP_Code in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")  "
            End If
        Else
            qry = " select '' as [MCC Code],'' as [MCC Name],''  as [VSP Code],''  as [VSP Name],'' as [Incentive Code],'' as [Incentive Name] ,'' as [Document Code] ,'' as [VLC code], '' as [VLC Name],'' as [Start Date],'' as [Start Shift],'' as [End Date],'' as [End Shift]"
        End If
        'transportsql.exporttoexcelnew(qry, me)
        ListImpExpColumnsMandatory = New List(Of String)({"Document Code", "VSP Code", "VSP Name", "MCC Code", "MCC Name", "Incentive Code", "Incentive Name", "Start Date", "Start Shift", "End Date", "End Shift"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Document Code"})
        transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "MCC Code", " MCC Name", " VSP Code", "VSP Name", "Incentive Code", " Incentive Name", " Document Code", " VLC code", " VLC Name", "Start Date", "Start Shift", "End Date", "End Date") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try

                Dim code As String = "'"
                Dim incentivecode As String = ""
                Dim incentivename As String = ""
                Dim vspcode As String = ""
                Dim vspname As String = ""
                Dim mcccode As String = ""
                Dim mccname As String = ""
                Dim vlccode As String = ""
                Dim vlcname As String = ""

                Dim StartDate As String = ""
                Dim StartShift As String = ""
                Dim EndDate As String = ""
                Dim EndShift As String = ""

                clsCommon.ProgressBarShow()
                Dim counter As Integer = 1
                For Each grow As GridViewRowInfo In gv.Rows

                    code = clsCommon.myCstr(grow.Cells("Document Code").Value)
                    If clsCommon.myLen(code) <= 0 Then
                        Throw New Exception("Fill  Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(code) > 30 Then
                        Throw New Exception("Length Of  Code Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    vspcode = clsCommon.myCstr(grow.Cells("VSP Code").Value)
                    vspname = clsCommon.myCstr(grow.Cells("VSP Name").Value)
                    If clsCommon.myLen(vspcode) <= 0 AndAlso clsCommon.myLen(vspname) <= 0 Then
                        Throw New Exception("Fill VSP Code/VSP Name At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(vspcode) > 12 Then
                        Throw New Exception("Length Of VSP Code Should Not Exceed 12 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim qry As String = ""
                    Dim check As Integer = 0
                    If clsCommon.myLen(vspcode) > 0 Then
                        qry = "select count(*) from tspl_vendor_master where form_type='VSP' and vendor_code='" + vspcode + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                        If check <= 0 Then
                            Throw New Exception("VSP Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of VSP Master")
                        End If
                    End If

                    mcccode = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                    mccname = clsCommon.myCstr(grow.Cells("MCC Name").Value)

                    If clsCommon.myLen(mcccode) <= 0 AndAlso clsCommon.myLen(mccname) <= 0 Then
                        Throw New Exception("Fill MCC Code/MCC Name At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(mcccode) > 30 Then
                        Throw New Exception("Length Of MCC Code Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(mcccode) > 0 Then
                        qry = "select count(*) from tspl_mcc_master where mcc_code='" + mcccode + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                        If check <= 0 Then
                            Throw New Exception("MCC Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of MCC Master")
                        End If
                    End If

                    incentivecode = clsCommon.myCstr(grow.Cells("Incentive Code").Value)
                    incentivename = clsCommon.myCstr(grow.Cells("Incentive Name").Value)

                    If clsCommon.myLen(incentivecode) <= 0 AndAlso clsCommon.myLen(incentivename) <= 0 Then
                        Throw New Exception("Fill Incentive Code/Incentive Name At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(incentivecode) > 30 Then
                        Throw New Exception("Length Of Incentive Code Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(incentivecode) > 0 Then
                        qry = "select count(*) from tspl_Incentive_Master_Head where Incentive_code='" + incentivecode + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                        If check <= 0 Then
                            Throw New Exception("Incentive Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Incentive Master")
                        End If
                    End If
                    StartDate = clsCommon.myCDate(grow.Cells("Start Date").Value)
                    StartShift = clsCommon.myCstr(grow.Cells("Start Shift").Value)
                    EndDate = clsCommon.myCDate(grow.Cells("End Date").Value)
                    EndShift = clsCommon.myCstr(grow.Cells("End Shift").Value)
                    If clsCommon.myLen(StartDate) <= 0 Then
                        Throw New Exception("Fill Start Date,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(StartShift) <= 0 Then
                        Throw New Exception("Fill End Shift M and E,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(EndDate) <= 0 Then
                        Throw New Exception("Fill Start Date,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(EndShift) <= 0 Then
                        Throw New Exception("Fill End Shift M and E,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim isSaved As Boolean = True
                    qry = "select count(*) from TSPL_VSP_INCENTIVE_Detail where Doc_Code='" + code + "'"
                    check = clsDBFuncationality.getSingleValue(qry, trans)

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Doc_Code", code)


                    clsCommon.AddColumnsForChange(coll, "VSP_Code", vspcode)
                    clsCommon.AddColumnsForChange(coll, "INCENTIVE_CODE", incentivecode)

                    clsCommon.AddColumnsForChange(coll, "MCC_Code", mcccode)

                    clsCommon.AddColumnsForChange(coll, "StartDate", StartDate)
                    clsCommon.AddColumnsForChange(coll, "StartShift", StartShift)
                    clsCommon.AddColumnsForChange(coll, "EndDate", EndDate)
                    clsCommon.AddColumnsForChange(coll, "EndShift", EndShift)
                    clsCommon.AddColumnsForChange(coll, "VSPSelect", "1")

                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")))

                    If check <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_INCENTIVE_Detail", OMInsertOrUpdate.Insert, "", trans)

                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_INCENTIVE_Detail", OMInsertOrUpdate.Update, " TSPL_VSP_INCENTIVE_Detail.Doc_Code='" + code + "'", trans)

                    End If

                    counter += 1
                    clsCommon.ProgressBarUpdate("Imported Receords  : " & counter & "/" & gv.Rows.Count)
                Next
                clsCommon.ProgressBarHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)
                'Load_Report(code)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            If clsCommon.myLen(FndMCC.Value) <= 0 Then
                FndMCC.Focus()
                Throw New Exception("Please select MCC")
            End If
            Dim qry As String = "select Route_Code,Route_Name from TSPL_MCC_ROUTE_MASTER where MCC_Code in ('" + FndMCC.Value + "')"
            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("PCURoute", qry, "Route_Code", "Route_Name", txtRoute.arrValueMember, Nothing)
            RefreshVSP()
            Load_Report()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub RefreshVSP()
        If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
            Dim qry As String = " select TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name " + Environment.NewLine + _
           " from TSPL_VLC_MASTER_HEAD  " + Environment.NewLine + _
           " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code= TSPL_VLC_MASTER_Head.MCC " + Environment.NewLine + _
           " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_Head.VSP_Code " + Environment.NewLine + _
           " where  TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") and VSP_Code in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtVSP.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("VSP_Code")))
                Next
                txtVSP.arrValueMember = arr
            End If
        End If
    End Sub

    Private Sub txtVSP__My_Click(sender As Object, e As EventArgs) Handles txtVSP._My_Click
        Try
            If txtRoute.arrValueMember Is Nothing OrElse txtRoute.arrValueMember.Count <= 0 Then
                txtRoute.Focus()
                Throw New Exception("Please select at least route")
            End If
            Dim qry As String = " select TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code ,TSPL_MCC_ROUTE_MASTER.Route_Name" + Environment.NewLine + _
           " from TSPL_VLC_MASTER_HEAD  " + Environment.NewLine + _
           " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code= TSPL_VLC_MASTER_Head.MCC " + Environment.NewLine + _
           " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_Head.VSP_Code " + Environment.NewLine + _
           " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code " + _
           " where  TSPL_VLC_MASTER_HEAD.Active='1' and  TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            txtVSP.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC", qry, "VSP_Code", "VSP Name", txtVSP.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtIncentiveMult__My_Click(sender As Object, e As EventArgs) Handles txtIncentiveMult._My_Click
        Dim qry As String = " select INCENTIVE_CODE as Code,DESCRIPTION as Name,INCENTIVE_DATE as Date,INCENTIVE_TYPE as IncentiveType,SCHEME_FOR as [Scheme For],Calc_Type as [Calculation Type],Rate_Type as [Rate Type],Qty_Type as [Quantity Type] from TSPL_INCENTIVE_MASTER_HEAD "
        ' '' get already selected data
        'Dim qrySel As String = "select Vendor_Code,INCENTIVE_CODE from TSPL_VSP_INCENTIVE where Vendor_Code='" & fndvendorNo.Value & "'"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qrySel)
        'Dim arr As New ArrayList
        'For Each dr As DataRow In dt.Rows
        '    arr.Add(clsCommon.myCstr(dr.Item("INCENTIVE_CODE")))
        'Next
        'If txtIncentiveMult.arrValueMember IsNot Nothing AndAlso txtIncentiveMult.arrValueMember.Count <= 0 Then
        '    txtIncentiveMult.arrValueMember = arr
        'End If
        txtIncentiveMult.arrValueMember = clsCommon.ShowMultipleSelectForm("IncenMulSel", qry, "Code", "Name", txtIncentiveMult.arrValueMember, txtIncentiveMult.arrDispalyMember)
    End Sub
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        If clsCommon.CompairString(btnSelect.Text, "Select All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(colselect).Value = True
            Next
            btnSelect.Text = "UnSelect All"
        Else
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(colselect).Value = False
            Next
            btnSelect.Text = "Select All"
        End If
    End Sub
End Class

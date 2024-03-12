Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

' Ticket No : BHA/10/10/18-000615  by prabhakar - Create new report 
Public Class rptUserScreenRightsReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Const colUserCode As String = "User Code"
    Const colUserName As String = "User Name"
    Const colModuleCode As String = "Module Code"
    Const colModuleName As String = "Module Name"
    Const colSubModuleCode As String = "Sub Module Code"
    Const colSubModuleName As String = "Sub Module Name"
    Const colGroupCode As String = "Group Code"
    Const colGroupName As String = "Group Name"
    Const colProgramCode As String = "Program Code"
    Const ColProgramName As String = "Program Name"

    Const colRead As String = "Read"
    Const colModify As String = "Modify"
    Const colDelete As String = "Delete"
    Const colAuthorize As String = "Authorize"
    Const colReverseUnPost As String = "Reverse And UnPost"
    Const colExport As String = "Export"
    Const colPrint As String = "Print"
    Const colCancel As String = "Cancel"
    Const colCancelPostedTransation As String = "Cancel Posted Transation"
    Const colQuickBulkExport As String = "Quick Bulk Export"
    Const colModifyOnPassword As String = "Modify On Password"
    Const colAmendment As String = "Amendment"
    Const colModifiedBy As String = "Modified By"
    Const colModifiedOn As String = "Modified On"

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub



    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
      
    End Sub
    Sub Reset()
        txtUser.arrValueMember = Nothing
        txtUserGroup.arrValueMember = Nothing
        txtModule.arrValueMember = Nothing
        txtScreen.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        'LoadBlankGrid()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            'Ticket No-BHA/06/06/19-000901 Sanjay
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim dt As New DataTable
            ' read_flag , modify_flag , delete_flag , authorized_flag , Reverse_Flag , Export_Flag
            ' Print_Flag ,Cancel_Flag , Cancel_Flag_After_Posting , QucikExport_Flag , isModifyonPassword , is_Amendment
            ' Modified_By , Modified_Date
            qry = " select TBL_USER_GROUP.User_Code as [User Code],TSPL_USER_MASTER.User_Name as [User Name],TBL_MODULE.Program_Code as [Module Code], TBL_MODULE.Program_Name as [Module Name] ,TBL_SMODULE.PROGRAM_CODE as [Sub Module Code],TBL_SMODULE.Program_Name as [Sub Module Name], " & _
                  " tspl_group_program_Mapping.Group_Code as [Group Code],tspl_user_group_master.Group_Desc as [Group Name],TSPL_PROGRAM_MASTER.Program_Code as [Screen Code],case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end  as [Screen Name] ,  " & _
                  " cast(tspl_group_program_Mapping.read_flag as bit) as [Read],cast(tspl_group_program_Mapping.modify_flag as bit) as [Modify],cast(tspl_group_program_Mapping.delete_flag as bit) as [Delete],cast(tspl_group_program_Mapping.authorized_flag as bit) as Authorize, cast(tspl_group_program_Mapping.Reverse_Flag as bit) as [Reverse & UnPost], cast(tspl_group_program_Mapping.Export_Flag as bit) as Export, " & _
                  " cast(tspl_group_program_Mapping.Print_Flag as bit) as [Print],cast(tspl_group_program_Mapping.Cancel_Flag as bit) as [Cancel],cast(tspl_group_program_Mapping.Cancel_Flag_After_Posting as bit) as [Cancel Posted Transation],cast(tspl_group_program_Mapping.QucikExport_Flag as bit) as [Quick Bulk Export],cast(tspl_group_program_Mapping.isModifyonPassword as bit) as [Modify On Password],cast(tspl_group_program_Mapping.is_Amendment as bit) as [Amendment] " & _
                  " , tspl_group_program_Mapping.Modify_By as [Modified By] , convert (varchar, tspl_group_program_Mapping.Modify_Date ,103) as [Modified On] " & _
                  " from TSPL_PROGRAM_MASTER " & _
                  " left outer join tspl_group_program_Mapping on tspl_group_program_Mapping.Program_code = TSPL_PROGRAM_MASTER.Program_Code " & _
                  " inner join  (select Group_Code, User_Code from tspl_User_group_Mapping   " & _
                  " ) as TBL_USER_GROUP on TBL_USER_GROUP.Group_Code = tspl_group_program_Mapping.Group_Code  " & _
                  " left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('SM')) as TBL_SMODULE on TBL_SMODULE.Program_Code = TSPL_PROGRAM_MASTER.Parent_Code " & _
                  " left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('M')) as TBL_MODULE on TBL_MODULE.Program_Code = TBL_SMODULE.Parent_Code " & _
                  " left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code =TBL_USER_GROUP.User_Code " & _
                  " left outer join tspl_user_group_master on tspl_user_group_master.Group_Code = tspl_group_program_Mapping.Group_Code " & _
                  " where not (TSPL_PROGRAM_MASTER.Type in ('M','SM') or TSPL_PROGRAM_MASTER.Parent_Code is null)  and  TSPL_PROGRAM_MASTER.Program_Code not in (" + clsCommon.GetMulcallString(MDI.arrExcluded) + ") "
            If txtUser.arrValueMember IsNot Nothing AndAlso txtUser.arrValueMember.Count > 0 Then
                qry += " and TBL_USER_GROUP.User_Code  in (" + clsCommon.GetMulcallString(txtUser.arrValueMember) + ")"
            End If
            If txtUserGroup.arrValueMember IsNot Nothing AndAlso txtUserGroup.arrValueMember.Count > 0 Then
                qry += " and tspl_group_program_Mapping.Group_Code  in (" + clsCommon.GetMulcallString(txtUserGroup.arrValueMember) + ")"
            End If
            If txtModule.arrValueMember IsNot Nothing AndAlso txtModule.arrValueMember.Count > 0 Then
                qry += " and TBL_MODULE.Program_Code  in (" + clsCommon.GetMulcallString(txtModule.arrValueMember) + ")"
            Else
                qry += " and TBL_MODULE.Program_Code  in (select  distinct Module_Name from TSPL_MODULE_PERMISSION)"
            End If
            If txtScreen.arrValueMember IsNot Nothing AndAlso txtScreen.arrValueMember.Count > 0 Then
                qry += " and TSPL_PROGRAM_MASTER.Program_Code  in (" + clsCommon.GetMulcallString(txtScreen.arrValueMember) + ")"
            End If
            qry += " order by TBL_USER_GROUP.User_Code ,TSPL_PROGRAM_MASTER .SNO, TBL_MODULE.Program_Code , TBL_SMODULE.PROGRAM_CODE  "


            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            Gv1.AllowAddNewRow = False
            Gv1.ShowGroupPanel = False
            Gv1.AllowColumnReorder = False
            Gv1.AllowRowReorder = False
            Gv1.EnableSorting = False
            Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            Gv1.MasterTemplate.ShowRowHeaderColumn = False

            'LoadBlankGrid()
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                    Gv1.Columns(ii).FormatString = ""
                    Gv1.Columns(ii).Width = 130
                    If Gv1.Columns(ii).GetType.Name = "GridViewCheckBoxColumn" Then
                        Gv1.Columns(ii).AllowFiltering = False
                    End If
                Next

                'Read.AllowFiltering = False

                'For Each dr As DataRow In dt.Rows
                '    Gv1.Rows.AddNew()
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUserCode).Value = clsCommon.myCstr(dr("User_Code"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUserName).Value = clsCommon.myCstr(dr("User_Name"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colModuleCode).Value = clsCommon.myCstr(dr("Module_Code"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colModuleName).Value = clsCommon.myCstr(dr("Module_Name"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSubModuleCode).Value = clsCommon.myCstr(dr("Sub_Module_Code"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSubModuleName).Value = clsCommon.myCstr(dr("Sub_Module_Name"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colGroupCode).Value = clsCommon.myCstr(dr("Group_Code"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colGroupName).Value = clsCommon.myCstr(dr("Group_Desc"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colProgramCode).Value = clsCommon.myCstr(dr("Program_Code"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColProgramName).Value = clsCommon.myCstr(dr("Program_Name"))

                '    ' read_flag , modify_flag , delete_flag , authorized_flag , Reverse_Flag , Export_Flag
                '    ' Print_Flag ,Cancel_Flag , Cancel_Flag_After_Posting , QucikExport_Flag , isModifyonPassword , is_Amendment
                '    ' Modified_By , Modified_Date
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colRead).Value = clsCommon.myCdbl(dr("read_flag"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colModify).Value = clsCommon.myCdbl(dr("modify_flag"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDelete).Value = clsCommon.myCdbl(dr("delete_flag"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colAuthorize).Value = clsCommon.myCdbl(dr("authorized_flag"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colReverseUnPost).Value = clsCommon.myCdbl(dr("Reverse_Flag"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colExport).Value = clsCommon.myCdbl(dr("Export_Flag"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colPrint).Value = clsCommon.myCdbl(dr("Print_Flag"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCancel).Value = clsCommon.myCdbl(dr("Cancel_Flag"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCancelPostedTransation).Value = clsCommon.myCdbl(dr("Cancel_Flag_After_Posting"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colQuickBulkExport).Value = clsCommon.myCdbl(dr("QucikExport_Flag"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colModifyOnPassword).Value = clsCommon.myCdbl(dr("isModifyonPassword"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colAmendment).Value = clsCommon.myCdbl(dr("is_Amendment"))

                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colModifiedBy).Value = clsCommon.myCstr(dr("Modified_By"))
                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colModifiedOn).Value = clsCommon.myCstr(dr("Modified_Date"))

                'Next

                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
   
   
   
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub


    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            ' arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptUserScreenRightsReport & "'"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtUser.arrDispalyMember IsNot Nothing AndAlso txtUser.arrDispalyMember.Count > 0 Then
                arrHeader.Add("User : " + clsCommon.GetMulcallStringWithComma(txtUser.arrDispalyMember))
            End If
            If txtUserGroup.arrDispalyMember IsNot Nothing AndAlso txtUserGroup.arrDispalyMember.Count > 0 Then
                arrHeader.Add("User Group : " + clsCommon.GetMulcallStringWithComma(txtUserGroup.arrDispalyMember))
            End If
            If txtModule.arrDispalyMember IsNot Nothing AndAlso txtModule.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Module : " + clsCommon.GetMulcallStringWithComma(txtModule.arrDispalyMember))
            End If
            If txtScreen.arrDispalyMember IsNot Nothing AndAlso txtScreen.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Screen : " + clsCommon.GetMulcallStringWithComma(txtScreen.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("User Screen Rights Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("User Screen Rights Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub txtUser__My_Click(sender As Object, e As EventArgs) Handles txtUser._My_Click
        Dim strUserGroupCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select top 1  STUFF((SELECT ',' +'[' +Group_Code+']' FROM tspl_user_group_master FOR XML PATH ('')), 1, 1, '')  from tspl_user_group_master "))
        Dim qry As String = "  select User_Code," + strUserGroupCode + " from  ( " & _
                            "  select tspl_User_group_Mapping.User_Code, tspl_User_group_Mapping.Group_Code,tspl_User_group_Mapping.Group_Code as Group_Code2 " & _
                            "  from tspl_User_group_Mapping " & _
                            "  ) d " & _
                            "  Pivot " & _
                            "  ( " & _
                            "  max(Group_Code2) " & _
                            "  for Group_Code in (" + strUserGroupCode + ") " & _
                            "  ) piv; "
        txtUser.arrValueMember = clsCommon.ShowMultipleSelectForm("Usere@MulitSection", qry, "User_Code", "User_Code", txtUser.arrValueMember, txtUser.arrDispalyMember)
    End Sub

    Sub LoadBlankGrid()

        Gv1.Rows.Clear()
        Gv1.Columns.Clear()

        Dim UserCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        UserCode.FormatString = ""
        UserCode.HeaderText = "User Code"
        UserCode.Name = colUserCode
        UserCode.Width = 130
        UserCode.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(UserCode)

        Dim UserName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        UserName.FormatString = ""
        UserName.HeaderText = "User Name"
        UserName.Name = colUserName
        UserName.Width = 130
        UserName.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(UserName)



        Dim ModuleCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ModuleCode.FormatString = ""
        ModuleCode.HeaderText = "Module Code"
        ModuleCode.Name = colModuleCode
        ModuleCode.Width = 130
        ModuleCode.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ModuleCode)

        Dim ModuleName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ModuleName.FormatString = ""
        ModuleName.HeaderText = "Module Code"
        ModuleName.Name = colModuleName
        ModuleName.Width = 130
        ModuleName.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ModuleName)

        Dim SubModuleCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SubModuleCode.FormatString = ""
        SubModuleCode.HeaderText = "Sub Module Code"
        SubModuleCode.Name = colSubModuleCode
        SubModuleCode.Width = 130
        SubModuleCode.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(SubModuleCode)

        Dim SubModuleName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SubModuleName.FormatString = ""
        SubModuleName.HeaderText = "Sub Module Name"
        SubModuleName.Name = colSubModuleName
        SubModuleName.Width = 130
        SubModuleName.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(SubModuleName)

        Dim GroupCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        GroupCode.FormatString = ""
        GroupCode.HeaderText = "Group Code"
        GroupCode.Name = colGroupCode
        GroupCode.Width = 130
        GroupCode.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(GroupCode)

        Dim GroupName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        GroupName.FormatString = ""
        GroupName.HeaderText = "Group Name"
        GroupName.Name = colGroupName
        GroupName.Width = 130
        GroupName.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(GroupName)

        Dim ProgramCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ProgramCode.FormatString = ""
        ProgramCode.HeaderText = "Screen Code"
        ProgramCode.Name = colProgramCode
        ProgramCode.Width = 130
        ProgramCode.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ProgramCode)

        Dim ProgramName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ProgramName.FormatString = ""
        ProgramName.HeaderText = "Screen Name"
        ProgramName.Name = ColProgramName
        ProgramName.Width = 130
        ProgramName.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(ProgramName)

        Dim Read As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        Read.FormatString = ""
        Read.HeaderText = "Read"
        Read.Name = colRead
        Read.Width = 130
        Read.ReadOnly = True
        Read.AllowFiltering = False
        Gv1.MasterTemplate.Columns.Add(Read)

        Dim Modify As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        Modify.FormatString = ""
        Modify.HeaderText = "Modify"
        Modify.Name = colModify
        Modify.Width = 130
        Modify.ReadOnly = True
        Modify.AllowFiltering = False
        Gv1.MasterTemplate.Columns.Add(Modify)

        Dim Delete As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        Delete.FormatString = ""
        Delete.HeaderText = "Delete"
        Delete.Name = colDelete
        Delete.Width = 130
        Delete.ReadOnly = True
        Delete.AllowFiltering = False
        Gv1.MasterTemplate.Columns.Add(Delete)

        Dim Authorize As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        Authorize.FormatString = ""
        Authorize.HeaderText = "Authorize"
        Authorize.Name = colAuthorize
        Authorize.Width = 130
        Authorize.ReadOnly = True
        Authorize.AllowFiltering = False
        Gv1.MasterTemplate.Columns.Add(Authorize)

        Dim ReverseUnPost As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        ReverseUnPost.FormatString = ""
        ReverseUnPost.HeaderText = "Reverse & UnPost"
        ReverseUnPost.Name = colReverseUnPost
        ReverseUnPost.Width = 130
        ReverseUnPost.ReadOnly = True
        ReverseUnPost.AllowFiltering = False
        Gv1.MasterTemplate.Columns.Add(ReverseUnPost)

        Dim Export As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        Export.FormatString = ""
        Export.HeaderText = "Export"
        Export.Name = colExport
        Export.Width = 130
        Export.ReadOnly = True
        Export.AllowFiltering = False
        Gv1.MasterTemplate.Columns.Add(Export)

        Dim Print As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        Print.FormatString = ""
        Print.HeaderText = "Print"
        Print.Name = colPrint
        Print.Width = 130
        Print.ReadOnly = True
        Print.AllowFiltering = False
        Gv1.MasterTemplate.Columns.Add(Print)

        Dim Cancel As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        Cancel.FormatString = ""
        Cancel.HeaderText = "Cancel"
        Cancel.Name = colCancel
        Cancel.Width = 130
        Cancel.ReadOnly = True
        Cancel.AllowFiltering = False
        Gv1.MasterTemplate.Columns.Add(Cancel)

        Dim CancelPostedTransation As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        CancelPostedTransation.FormatString = ""
        CancelPostedTransation.HeaderText = "Cancel Posted Transation"
        CancelPostedTransation.Name = colCancelPostedTransation
        CancelPostedTransation.Width = 130
        CancelPostedTransation.ReadOnly = True
        CancelPostedTransation.AllowFiltering = False
        Gv1.MasterTemplate.Columns.Add(CancelPostedTransation)

        Dim QuickBulkExport As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        QuickBulkExport.FormatString = ""
        QuickBulkExport.HeaderText = "Quick Bulk Export"
        QuickBulkExport.Name = colQuickBulkExport
        QuickBulkExport.Width = 130
        QuickBulkExport.ReadOnly = True
        QuickBulkExport.AllowFiltering = False
        Gv1.MasterTemplate.Columns.Add(QuickBulkExport)

        Dim ModifyOnPassword As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        ModifyOnPassword.FormatString = ""
        ModifyOnPassword.HeaderText = "Modify On Password"
        ModifyOnPassword.Name = colModifyOnPassword
        ModifyOnPassword.Width = 130
        ModifyOnPassword.ReadOnly = True
        ModifyOnPassword.AllowFiltering = False
        Gv1.MasterTemplate.Columns.Add(ModifyOnPassword)

        Dim Amendment As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        Amendment.FormatString = ""
        Amendment.HeaderText = "Amendment"
        Amendment.Name = colAmendment
        Amendment.Width = 130
        Amendment.ReadOnly = True
        Amendment.AllowFiltering = False
        Gv1.MasterTemplate.Columns.Add(Amendment)

        Dim ModifiedBy As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ModifiedBy.FormatString = ""
        ModifiedBy.HeaderText = "Modified By"
        ModifiedBy.Name = colModifiedBy
        ModifiedBy.Width = 130
        ModifiedBy.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ModifiedBy)

        Dim ModifiedOn As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ModifiedOn.FormatString = ""
        ModifiedOn.HeaderText = "Modified On"
        ModifiedOn.Name = colModifiedOn
        ModifiedOn.Width = 130
        ModifiedOn.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ModifiedOn)

        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = False
        Gv1.AllowColumnReorder = False
        Gv1.AllowRowReorder = False
        Gv1.EnableSorting = False
        Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub txtUserGroup__My_Click(sender As Object, e As EventArgs) Handles txtUserGroup._My_Click
        Dim qry As String = " select tspl_user_group_master.Group_Code as Code, tspl_user_group_master.Group_Desc as Name from tspl_user_group_master "
        txtUserGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("Group@MulitSection", qry, "Code", "Name", txtUserGroup.arrValueMember, txtUserGroup.arrDispalyMember)
    End Sub

    Private Sub txtModule__My_Click(sender As Object, e As EventArgs) Handles txtModule._My_Click
        Dim qry As String = "select Program_Code as Code,Program_Name as Name from TSPL_PROGRAM_MASTER where  TYPE='M' and TSPL_PROGRAM_MASTER.Program_Code not in (" + clsCommon.GetMulcallString(MDI.arrExcluded) + ")  and  TSPL_PROGRAM_MASTER.Program_Code in (select  distinct Module_Name from TSPL_MODULE_PERMISSION) order by SNo"
        txtModule.arrValueMember = clsCommon.ShowMultipleSelectForm("Module@MulitSection", qry, "Code", "Name", txtModule.arrValueMember, txtModule.arrDispalyMember)
    End Sub

    Private Sub txtScreen__My_Click(sender As Object, e As EventArgs) Handles txtScreen._My_Click
        Dim qry As String = " select TSPL_PROGRAM_MASTER.Program_Code as Code,case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end  as Name , TBL_MODULE.Program_Name as [Module Name] from TSPL_PROGRAM_MASTER " & _
                            " left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('SM')) as TBL_SMODULE on TBL_SMODULE.Program_Code = TSPL_PROGRAM_MASTER.Parent_Code  " & _
                            " left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('M')) as TBL_MODULE on TBL_MODULE.Program_Code = TBL_SMODULE.Parent_Code " & _
                            " where  TBL_MODULE.Program_Code in (select  distinct Module_Name from TSPL_MODULE_PERMISSION ) and  not TSPL_PROGRAM_MASTER.Type in ('M','SM')  and TSPL_PROGRAM_MASTER.Program_Code not in (" + clsCommon.GetMulcallString(MDI.arrExcluded) + ")   order by SNo "
        txtScreen.arrValueMember = clsCommon.ShowMultipleSelectForm("Screen@MulitSection", qry, "Code", "Name", txtScreen.arrValueMember, txtScreen.arrDispalyMember)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
End Class

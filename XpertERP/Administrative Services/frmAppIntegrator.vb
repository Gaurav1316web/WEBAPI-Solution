Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection
Public Class frmAppIntegrator
    Inherits FrmMainTranScreen
    Public Const colSelect As String = "colSelect"
    Public Const colObjectName As String = "colObjectName"
    Public Const colObjectType As String = "colObjectType"
    Public Const colIntegrationType As String = "colIntegrationType"
    Public Const colModuleCode As String = "colModuleCode"
    Public Const colModuleDesc As String = "colModuleDesc"
    Public Const colSubModuleCode As String = "colSubModuleCode"
    Public Const colSubModuleDesc As String = "colSubModuleDesc"
    Public Const colFormId As String = "colFormId"
    Public Const colScreenCaption As String = "colScreenCaption"
    Public Const colVersionID As String = "colVersionID"

    Public Const colAsmPath As String = "colAsmPath"
    Dim repoComboColumn As GridViewComboBoxColumn
    Dim repoTextColumn As GridViewTextBoxColumn
    Dim repoCheckBoxColumn As GridViewCheckBoxColumn
    Dim isLoad As Boolean = True
    Public isCellValueChanged = False
    Public CheckedCount As Integer = 0
    Public Verion As String = String.Empty
    Public isLoading As Boolean = False
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try

            clsERPFuncationality.closeForm(Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function allowToSave() As Boolean
        Try
            'balwinder()
            If gv.Rows.Count <= 0 Then
                Throw New Exception("Now Rows Found in Grid, Please Select a DLL/EXE")
            End If
            CheckedCount = 0
            For i As Integer = 0 To gv.Rows.Count - 1
                If gv.Rows(i).Cells(colSelect).Value Then
                    CheckedCount = CheckedCount + 1
                    If clsCommon.myLen(gv.Rows(i).Cells(colIntegrationType).Value) <= 0 Then
                        Throw New Exception("Please Select Integration Type Either New or Existing at Row No " & (i + 1))
                    End If

                    If clsCommon.CompairString(gv.Rows(i).Cells(colIntegrationType).Value, "New") = CompairStringResult.Equal AndAlso clsCommon.myLen(gv.Rows(i).Cells(colModuleCode).Value) <= 0 Then
                        Throw New Exception("Please Select Module Code at Row No " & (i + 1))
                    End If

                    If clsCommon.CompairString(gv.Rows(i).Cells(colIntegrationType).Value, "New") = CompairStringResult.Equal AndAlso clsCommon.myLen(gv.Rows(i).Cells(colSubModuleCode).Value) <= 0 Then
                        Throw New Exception("Please Select sub Module Code at Row No " & (i + 1))
                    End If

                    If clsCommon.myLen(gv.Rows(i).Cells(colFormId).Value) <= 0 Then
                        Throw New Exception("Please Fill Form ID at Row No " & (i + 1))
                    End If

                    If clsCommon.myLen(gv.Rows(i).Cells(colFormId).Value) > 12 Then
                        Throw New Exception("Form Id Lenght can be Maximum 12 at Row No " & (i + 1))
                    End If

                    If clsCommon.myLen(gv.Rows(i).Cells(colScreenCaption).Value) <= 0 Then
                        Throw New Exception("Please Fill Screen Caption at Row No " & (i + 1))
                    End If
                End If
            Next

            If CheckedCount <= 0 Then
                Throw New Exception("No Rows Selected To " & ddlType.Text)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function AddCustomLinks(ByVal strProgramCode As String, ByVal strProgramDescription As String, ByVal strParent_Code As String, addAs As String, FilePath As String, FormName As String, ByVal trans As SqlTransaction) As Boolean
        Dim strParentSNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select SNo from TSPL_PROGRAM_MASTER where Program_Code='" + strParent_Code + "'", trans))
        Dim qry As String = "select MAX(SNo) from TSPL_PROGRAM_MASTER where SNo like   '" + strParentSNo + "+.100%'"
        Dim strSNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        If clsCommon.myLen(strSNo) <= 0 Then
            strSNo = strParentSNo + ".1001"
        Else
            strSNo = clsCommon.incval(strSNo)
        End If
        ProgramCodeNew.InsertDefaultValue(strProgramCode, strProgramDescription, strSNo, strParent_Code, "", 30, 1, addAs, FilePath, FormName, trans)
        Return True
    End Function
    Sub SaveData()
        Try
            Dim strFileName As String = ""
            If allowToSave() Then
                If clsCommon.MyMessageBoxShow(ME,"Want to " & ddlType.Text & "  " & CheckedCount & " Number of Screens", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    If CheckedCount > 0 Then
                        If clsCommon.CompairString(ddlType.Text, "Integrate") = CompairStringResult.Equal Then
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Description", "")
                            Dim filePath As String = txtFilePath.Text
                            'Dim strRelativePath As String = Application.StartupPath
                            strFileName = Path.GetFileName(filePath)
                            'Dim StrActualPath As String = Microsoft.VisualBasic.Left(filePath, filePath.LastIndexOf("\"))
                            'If clsCommon.CompairString(strRelativePath, StrActualPath) <> CompairStringResult.Equal Then
                            'If File.Exists(strRelativePath & "\" & strFileName) Then
                            'File.Delete(strRelativePath & "\" & strFileName)
                            'End If
                            'File.Copy(filePath, strRelativePath & "\" & strFileName)
                            'End If
                            'txtFilePath.Text = strRelativePath & "\" & strFileName

                            clsCommon.AddColumnsForChange(coll, "FileName", strFileName)
                            clsCommon.AddColumnsForChange(coll, "FileData", Nothing, True)
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOM_EXE_DEPLOYEEMENT_MASTER", OMInsertOrUpdate.Insert, "", Nothing)
                            Dim code As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT isnull(max(Auto_ID ),0) FROM  TSPL_CUSTOM_EXE_DEPLOYEEMENT_MASTER"))
                            If code > 0 Then
                                Dim bData As Byte()
                                Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(txtFilePath.Text))
                                bData = br.ReadBytes(br.BaseStream.Length)
                                Dim str As String
                                str = " UPDATE TSPL_CUSTOM_EXE_DEPLOYEEMENT_MASTER set FileData = @BLOBData where Auto_Id='" & code & "'"
                                Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection)
                                Dim prm As New SqlParameter("@BLOBData", bData)
                                cmd.Parameters.Add(prm)
                                cmd.ExecuteNonQuery()
                                br.Close() ' done by stuti reagrding memory leakage
                            End If
                        End If
                    End If
                    For i As Integer = 0 To gv.Rows.Count - 1
                        If gv.Rows(i).Cells(colSelect).Value Then
                            If clsCommon.CompairString(ddlType.Text, "Dis-Integrate") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(gv.Rows(i).Cells(colIntegrationType).Value, "Existing") = CompairStringResult.Equal Then
                                    Dim qry As String = "  Update tspl_program_master set IsLoadFromOtherAssembly=0,addas='',OtherAssemblyFilePathAndName='', FormName='', Re_Name='',IntegrationVersion='' where Program_code='" & gv.Rows(i).Cells(colFormId).Value & "'"
                                    clsDBFuncationality.ExecuteNonQuery(qry)
                                ElseIf clsCommon.CompairString(gv.Rows(i).Cells(colIntegrationType).Value, "New") = CompairStringResult.Equal Then
                                    Dim qry As String = "  delete from tspl_program_master  where Program_code='" & gv.Rows(i).Cells(colFormId).Value & "'"
                                    clsDBFuncationality.ExecuteNonQuery(qry)
                                End If
                            Else

                                If clsCommon.CompairString(gv.Rows(i).Cells(colIntegrationType).Value, "Existing") = CompairStringResult.Equal Then
                                    Dim qry As String = "  Update tspl_program_master set IsLoadFromOtherAssembly=1,addas='" & Microsoft.VisualBasic.Left(gv.Rows(i).Cells(colIntegrationType).Value, 1) & "',OtherAssemblyFilePathAndName='" & strFileName & "', FormName='" & gv.Rows(i).Cells(colObjectName).Value & "', Re_Name='" & gv.Rows(i).Cells(colScreenCaption).Value & "',IntegrationVersion='" & gv.Rows(i).Cells(colVersionID).Value & "' where Program_code='" & gv.Rows(i).Cells(colFormId).Value & "'"
                                    clsDBFuncationality.ExecuteNonQuery(qry)
                                ElseIf clsCommon.CompairString(gv.Rows(i).Cells(colIntegrationType).Value, "New") = CompairStringResult.Equal Then
                                    AddCustomLinks(gv.Rows(i).Cells(colFormId).Value, gv.Rows(i).Cells(colScreenCaption).Value, gv.Rows(i).Cells(colSubModuleCode).Value, Microsoft.VisualBasic.Left(gv.Rows(i).Cells(colIntegrationType).Value, 1), strFileName, gv.Rows(i).Cells(colObjectName).Value, Nothing)
                                    Dim qry As String = "  Update tspl_program_master set IntegrationVersion='" & gv.Rows(i).Cells(colVersionID).Value & "' where Program_code='" & gv.Rows(i).Cells(colFormId).Value & "'"
                                    clsDBFuncationality.ExecuteNonQuery(qry)
                                End If
                                CallStartupFunction(strFileName)

                            End If
                        End If

                    Next
                    clsCommon.MyMessageBoxShow(Me, "Saved Successfully", Me.Text)
                    Reset()
                    MDI.LoadImageList()
                    MDI.LoadMenu()
                Else
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message,me.text)
        End Try
    End Sub

    Sub LoadAsseblyFromFile()

    End Sub

    Sub LoadBlankGrid(Optional isLoadExisting As Boolean = False)
        Try
            gv.Rows.Clear()
            gv.Columns.Clear()
            repoCheckBoxColumn = New GridViewCheckBoxColumn()
            repoCheckBoxColumn.Name = colSelect
            repoCheckBoxColumn.Width = 60
            repoCheckBoxColumn.HeaderText = ""
            repoCheckBoxColumn.IsPinned = True
            gv.MasterTemplate.Columns.Add(repoCheckBoxColumn)
            If isLoadExisting Then
                repoTextColumn = New GridViewTextBoxColumn()
                repoTextColumn.Name = colAsmPath
                repoTextColumn.Width = 150
                repoTextColumn.HeaderText = "Assembly Path"
                repoTextColumn.ReadOnly = True
                repoTextColumn.IsPinned = True
                gv.MasterTemplate.Columns.Add(repoTextColumn)
            End If
            repoTextColumn = New GridViewTextBoxColumn()
            repoTextColumn.Name = colObjectName
            repoTextColumn.Width = 150
            repoTextColumn.HeaderText = "Object Name"
            repoTextColumn.ReadOnly = True
            repoTextColumn.IsPinned = True
            gv.MasterTemplate.Columns.Add(repoTextColumn)

            repoTextColumn = New GridViewTextBoxColumn()
            repoTextColumn.Name = colVersionID
            repoTextColumn.Width = 100
            repoTextColumn.HeaderText = "Version ID"
            repoTextColumn.ReadOnly = True
            repoTextColumn.IsPinned = True
            gv.MasterTemplate.Columns.Add(repoTextColumn)


            If Not isLoadExisting Then
                repoTextColumn = New GridViewTextBoxColumn()
                repoTextColumn.Name = colObjectType
                repoTextColumn.Width = 150
                repoTextColumn.HeaderText = "Object Type"
                repoTextColumn.IsPinned = True
                repoTextColumn.ReadOnly = True
                gv.MasterTemplate.Columns.Add(repoTextColumn)
            End If

            repoComboColumn = New GridViewComboBoxColumn()
            repoComboColumn.Name = colIntegrationType
            repoComboColumn.Width = 150
            repoComboColumn.DataSource = FillIntegrationType()
            repoComboColumn.ReadOnly = False
            repoComboColumn.DisplayMember = "Value"
            repoComboColumn.ValueMember = "Value"
            repoComboColumn.HeaderText = "IntegrationType"
            repoComboColumn.IsPinned = True
            gv.MasterTemplate.Columns.Add(repoComboColumn)



            repoTextColumn = New GridViewTextBoxColumn()
            repoTextColumn.Name = colModuleCode
            repoTextColumn.Width = 150
            repoTextColumn.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoTextColumn.TextImageRelation = TextImageRelation.TextBeforeImage
            repoTextColumn.HeaderText = "Module Code"
            repoTextColumn.ReadOnly = False
            gv.MasterTemplate.Columns.Add(repoTextColumn)


            repoTextColumn = New GridViewTextBoxColumn()
            repoTextColumn.Name = colModuleDesc
            repoTextColumn.Width = 150
            repoTextColumn.HeaderText = "Module Desc"
            repoTextColumn.ReadOnly = True
            gv.MasterTemplate.Columns.Add(repoTextColumn)



            repoTextColumn = New GridViewTextBoxColumn()
            repoTextColumn.Name = colSubModuleCode
            repoTextColumn.Width = 150
            repoTextColumn.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoTextColumn.TextImageRelation = TextImageRelation.TextBeforeImage
            repoTextColumn.HeaderText = "Sub Module Code"
            repoTextColumn.ReadOnly = False
            gv.MasterTemplate.Columns.Add(repoTextColumn)


            repoTextColumn = New GridViewTextBoxColumn()
            repoTextColumn.Name = colSubModuleDesc
            repoTextColumn.Width = 150
            repoTextColumn.HeaderText = "Sub Module Desc"
            repoTextColumn.ReadOnly = True
            gv.MasterTemplate.Columns.Add(repoTextColumn)




            repoTextColumn = New GridViewTextBoxColumn()
            repoTextColumn.Name = colFormId
            repoTextColumn.Width = 150
            repoTextColumn.HeaderText = "Screen Code"
            repoTextColumn.ReadOnly = False
            gv.MasterTemplate.Columns.Add(repoTextColumn)


            repoTextColumn = New GridViewTextBoxColumn()
            repoTextColumn.Name = colScreenCaption
            repoTextColumn.Width = 150
            repoTextColumn.HeaderText = "Screen Desc"
            repoTextColumn.ReadOnly = False
            gv.MasterTemplate.Columns.Add(repoTextColumn)


            gv.AllowAddNewRow = False
            gv.AllowDeleteRow = False
            gv.AllowRowReorder = False
            gv.ShowGroupPanel = False
            gv.EnableFiltering = True
            gv.EnableSorting = True
            gv.EnableGrouping = False
            gv.AllowColumnChooser = True
            gv.AllowColumnReorder = True
            gv.ShowFilteringRow = True



        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Function FillIntegrationType() As DataTable
        Dim dt As DataTable
        Dim qry As String = " select '' as value union all select 'New' as value union all select 'Existing' as value "
        dt = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

    Sub LoadFileDetailFromDataBase()
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Child.Program_code as FormId,isnull(Child.IntegrationVersion,'') as IntegrationVersion ,Child.OtherAssemblyFilePathAndName as AsmPath,Child.FormName as AsmModule,Child.IsLoadFromOtherAssembly as IsLoadFromOther,Child.addAs as InteAs,Child.Program_Name as FormName,Child.Parent_code as SubModuleCode,TSPL_PROGRAM_MASTER.Program_Name as SubmoduleName,TSPL_PROGRAM_MASTER.Parent_Code  as ModuleCode ,Module.Program_Name as ModuleName from  TSPL_PROGRAM_MASTER as  Child left outer join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code=Child.Parent_Code left outer join TSPL_PROGRAM_MASTER as Module on Module.Program_Code=TSPL_PROGRAM_MASTER.Parent_Code where isnull(TSPL_PROGRAM_MASTER.Parent_Code,'') not in('ExpertERP','') and Child.IsLoadFromOtherAssembly=1")
            If dt IsNot Nothing Then
                LoadBlankGrid(True)
                For i As Integer = 0 To dt.Rows.Count - 1
                    gv.Rows.Add(0, dt.Rows(i)("AsmPath"), dt.Rows(i)("AsmModule"), clsCommon.myCstr(dt.Rows(i)("IntegrationVersion")), IIf(clsCommon.CompairString(dt.Rows(i)("InteAs"), "E") = CompairStringResult.Equal, "Existing", "New"), dt.Rows(i)("ModuleCode"), dt.Rows(i)("ModuleName"), dt.Rows(i)("SubModuleCode"), dt.Rows(i)("SubmoduleName"), dt.Rows(i)("FormId"), dt.Rows(i)("FormName"))
                Next
                For i As Integer = 1 To gv.Columns.Count - 1
                    gv.Columns(i).ReadOnly = True
                Next
                'gv.BestFitColumns()
                'gv.AutoSizeRows = True
                If gv.Rows.Count > 0 Then
                    btnSave.Enabled = True
                    gv.Enabled = True
                Else
                    Reset()
                    ddlType.Text = "Dis-Integrate"
                    clsCommon.MyMessageBoxShow(Me, "No Component Found Integrated to be Dis-Integrated.", Me.Text)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myLen(ddlType.Text) > 0 AndAlso clsCommon.CompairString(ddlType.Text, "Dis-Integrate") = CompairStringResult.Equal Then
                ddlType.Enabled = False
                btnGo.Enabled = False
                txtFilePath.Enabled = False
                txtFilePath.Text = ""
                txtFilePath.ReadOnly = True
                btnLoad.Enabled = False
                btnBrowse.Enabled = False
                LoadFileDetailFromDataBase()
            ElseIf clsCommon.myLen(ddlType.Text) > 0 Then
                ddlType.Enabled = False
                btnGo.Enabled = False
                txtFilePath.Enabled = True
                txtFilePath.Text = ""
                txtFilePath.ReadOnly = True
                btnLoad.Enabled = True
                btnBrowse.Enabled = True
            Else
                Reset()
                Throw New Exception("Please select Type as Integrate/Dis-Integrate")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        ddlType.Enabled = True
        btnGo.Enabled = True
        txtFilePath.Enabled = False
        txtFilePath.Text = ""
        txtFilePath.ReadOnly = False
        btnLoad.Enabled = False
        btnBrowse.Enabled = False
        btnSave.Enabled = False
        ddlType.SelectedIndex = 0
        btnClose.Enabled = True
        LoadBlankGrid()
        btnReset.Enabled = True
    End Sub

    Private Sub frmAppIntegrator_Load(sender As Object, e As EventArgs) Handles Me.Load
        reset()
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Try

            Dim sfd As System.Windows.Forms.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
            Dim filePath As String
            sfd.FileName = txtFilePath.Text
            sfd.Filter = "Dll Files (*.dll) |*.dll;|Exe Files (*.exe)|*.exe"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If

            If clsCommon.myLen(filePath) > 0 Then

                Dim strRelativePath As String = Application.StartupPath
                Dim strFileName As String = Path.GetFileName(filePath)
                Dim StrActualPath As String = Microsoft.VisualBasic.Left(filePath, filePath.LastIndexOf("\"))
                If clsCommon.CompairString(strRelativePath, StrActualPath) <> CompairStringResult.Equal Then
                    If File.Exists(strRelativePath & "\" & strFileName) Then
                        File.Delete(strRelativePath & "\" & strFileName)
                    End If
                    File.Copy(filePath, strRelativePath & "\" & strFileName)
                End If
                txtFilePath.Text = strRelativePath & "\" & strFileName
                btnSave.Enabled = False
                btnGo.Enabled = False
                btnBrowse.Enabled = False
                btnLoad.Enabled = True
                ddlType.Enabled = False
                txtFilePath.Enabled = False
            Else
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadAssembly(FilePath As String)
        Try
            isLoading = True
            Dim AsmToLoad As Assembly = Nothing
            Dim AsmModule() As [Module] = Nothing
            Dim obj As Object = Nothing
            AsmToLoad = Assembly.LoadFile(FilePath)
            Dim strFileName As String = Path.GetFileName(FilePath)
            AsmModule = AsmToLoad.GetLoadedModules
            isLoad = True
            For Each Tp As System.Type In AsmModule(0).GetTypes()
                If Tp.IsClass AndAlso Tp.IsPublic AndAlso (clsCommon.CompairString(Tp.BaseType.Name, "Form") = CompairStringResult.Equal OrElse clsCommon.CompairString(Tp.BaseType.Name, "RadForm") = CompairStringResult.Equal OrElse clsCommon.CompairString(Tp.BaseType.Name, "RadForm1") = CompairStringResult.Equal OrElse clsCommon.CompairString(Tp.BaseType.Name, "FrmMainTranScreen") = CompairStringResult.Equal) Then
                    Dim qry As String = "select parent.Parent_Code as MCode ,tspl_program_master.*  from tspl_program_master left outer join tspl_program_master parent on parent.Program_Code=tspl_program_master.Parent_Code  where isnull(tspl_program_master.OtherAssemblyFilePathAndName,'')='" & strFileName & "' and isnull(tspl_program_master.FormName,'')='" & Tp.FullName & "' and isnull(tspl_program_master.IsLoadFromOtherAssembly,0)=1"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        gv.Rows.Add(True, Tp.FullName, IIf(clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("IntegrationVersion"))) = 0, Verion, clsCommon.myCstr(dt.Rows(0)("IntegrationVersion"))), Tp.BaseType.Name, IIf(clsCommon.CompairString(dt.Rows(0)("Addas"), "E") = CompairStringResult.Equal, "Existing", "New"), dt.Rows(0)("MCode"), ProgramCodeNew.GetProgramName(dt.Rows(0)("MCode")), dt.Rows(0)("Parent_Code"), ProgramCodeNew.GetProgramName(dt.Rows(0)("Parent_Code")), dt.Rows(0)("Program_Code"), dt.Rows(0)("Program_Name"))

                        For i As Integer = 0 To gv.Columns.Count - 1
                            gv.Rows(gv.Rows.Count - 1).Cells(i).ReadOnly = True
                        Next

                    Else
                        'Try
                        '    Dim FormName As String = clsCommon.myCstr(Tp.FullName)
                        '    Dim classType As Type = AsmToLoad.[GetType](FormName, False, True)
                        '    obj = AsmToLoad.CreateInstance(FormName, True)
                        '    Dim frm As FrmMainTranScreen = TryCast(obj, FrmMainTranScreen)
                        gv.Rows.Add(False, Tp.FullName, Verion, Tp.BaseType.Name, "", "", "", "", "", "", "")
                        'Catch ex As Exception
                        'End Try

                    End If
                End If
            Next
            isLoad = False
            If gv.Rows.Count > 0 Then
                btnSave.Enabled = True
            Else
                btnSave.Enabled = False
            End If
            isLoading = False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            If clsCommon.myLen(txtFilePath.Text) <= 0 Then
                Throw New Exception("Please select the Assembly first.")
            End If
            Dim AsmToLoad As Assembly = Nothing
            Dim AsmModule() As [Module] = Nothing
            Dim obj As Object = Nothing
            AsmToLoad = Assembly.LoadFile(txtFilePath.Text)
            Dim myAssemblyName As AssemblyName = AsmToLoad.GetName()
            Verion = clsCommon.myCstr(myAssemblyName.Version).Trim()
            Dim aDescAttr As AssemblyDescriptionAttribute = AssemblyDescriptionAttribute.GetCustomAttribute(AsmToLoad, GetType(AssemblyDescriptionAttribute))

            If clsCommon.CompairString(aDescAttr.Description.ToString, objCommonVar.CurrentCompanyCode) <> CompairStringResult.Equal Then
                Throw New Exception("The Assembly You are mapping is not belonging to This Client")
            End If
            btnLoad.Enabled = False
            gv.Enabled = True
            isLoading = True
            LoadAssembly(txtFilePath.Text)
            isLoading = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv.CellFormatting
        If Not isLoading Then
            If clsCommon.CompairString(gv.Rows(e.RowIndex).Cells(colIntegrationType).Value, "New") = CompairStringResult.Equal AndAlso (Not gv.Rows(e.RowIndex).Cells(colIntegrationType).ReadOnly) Then
                gv.Rows(e.RowIndex).Cells(colModuleCode).ReadOnly = False
                gv.Rows(e.RowIndex).Cells(colSubModuleCode).ReadOnly = False
                gv.Rows(e.RowIndex).Cells(colFormId).ReadOnly = False
                gv.Rows(e.RowIndex).Cells(colScreenCaption).ReadOnly = False
                'isLoad = True
                'gv.Rows(e.RowIndex).Cells(colFormId).Value = ""
                'isLoad = False
            ElseIf clsCommon.CompairString(gv.Rows(e.RowIndex).Cells(colIntegrationType).Value, "Existing") = CompairStringResult.Equal AndAlso (Not gv.Rows(e.RowIndex).Cells(colIntegrationType).ReadOnly) Then
                gv.Rows(e.RowIndex).Cells(colModuleCode).ReadOnly = True
                gv.Rows(e.RowIndex).Cells(colSubModuleCode).ReadOnly = True
                gv.Rows(e.RowIndex).Cells(colFormId).ReadOnly = False
                gv.Rows(e.RowIndex).Cells(colScreenCaption).ReadOnly = True
            Else
                gv.Rows(e.RowIndex).Cells(colModuleCode).ReadOnly = True
                gv.Rows(e.RowIndex).Cells(colSubModuleCode).ReadOnly = True
                gv.Rows(e.RowIndex).Cells(colFormId).ReadOnly = True
                gv.Rows(e.RowIndex).Cells(colScreenCaption).ReadOnly = True
            End If
        End If
    End Sub

    Private Sub gv_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isLoad Then
            isLoad = True
            If (e.Column Is gv.Columns(colModuleCode) OrElse e.Column Is gv.Columns(colSubModuleCode) OrElse e.Column Is gv.Columns(colFormId) OrElse e.Column Is gv.Columns(colIntegrationType)) AndAlso e.RowIndex >= 0 Then
                isCellValueChanged = True
                If e.Column Is gv.Columns(colModuleCode) Then
                    OpenModule(e.RowIndex)
                End If

                If e.Column Is gv.Columns(colSubModuleCode) Then
                    OpenSubModule(e.RowIndex)
                End If

                If e.Column Is gv.Columns(colFormId) Then
                    OpenScreen(e.RowIndex)
                End If

                If e.Column Is gv.Columns(colIntegrationType) Then
                    gv.Rows(e.RowIndex).Cells(colModuleCode).Value = ""
                    gv.Rows(e.RowIndex).Cells(colModuleDesc).Value = ""
                    gv.Rows(e.RowIndex).Cells(colSubModuleDesc).Value = ""
                    gv.Rows(e.RowIndex).Cells(colSubModuleCode).Value = ""
                    gv.Rows(e.RowIndex).Cells(colFormId).Value = ""
                    gv.Rows(e.RowIndex).Cells(colScreenCaption).Value = ""
                End If
                isCellValueChanged = False
            End If
            isLoad = False
        End If
    End Sub

    Sub OpenModule(index As Integer)
        Try
            Dim qry As String = " select Program_Code ,Program_Name  from TSPL_PROGRAM_MASTER   "
            gv.Rows(index).Cells(colModuleCode).Value = clsCommon.ShowSelectForm("RptModuleFnd", qry, "Program_Code", " isnull(type,'')='M' ", gv.Rows(index).Cells(colModuleCode).Value)
            If clsCommon.myLen(gv.Rows(index).Cells(colModuleCode).Value) > 0 Then
                qry = " select  Program_Name  from TSPL_PROGRAM_MASTER where  Program_Code='" & gv.Rows(index).Cells(colModuleCode).Value & "'"
                gv.Rows(index).Cells(colModuleDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                isLoad = True
                gv.Rows(index).Cells(colSubModuleDesc).Value = ""
                gv.Rows(index).Cells(colSubModuleCode).Value = ""
                gv.Rows(index).Cells(colFormId).Value = ""
                gv.Rows(index).Cells(colScreenCaption).Value = ""
                isLoad = False
            Else
                isLoad = True
                gv.Rows(index).Cells(colSubModuleDesc).Value = ""
                gv.Rows(index).Cells(colSubModuleCode).Value = ""
                gv.Rows(index).Cells(colFormId).Value = ""
                gv.Rows(index).Cells(colScreenCaption).Value = ""
                gv.Rows(index).Cells(colModuleDesc).Value = ""
                gv.Rows(index).Cells(colModuleCode).Value = ""
                isLoad = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenSubModule(index As Integer)
        Try
            If clsCommon.myLen(gv.Rows(index).Cells(colModuleCode).Value) <= 0 Then
                Throw New Exception("Please Select Module First")
            End If
            Dim qry As String = " select Program_Code ,Program_Name  from TSPL_PROGRAM_MASTER    "
            gv.Rows(index).Cells(colSubModuleCode).Value = clsCommon.ShowSelectForm("RptSubModuleFnd", qry, "Program_Code", "  isnull(type,'')='SM' and Parent_Code='" & gv.Rows(index).Cells(colModuleCode).Value & "' ", gv.Rows(index).Cells(colSubModuleCode).Value)
            If clsCommon.myLen(gv.Rows(index).Cells(colSubModuleCode).Value) > 0 Then
                qry = " select  Program_Name  from TSPL_PROGRAM_MASTER where  Program_Code='" & gv.Rows(index).Cells(colSubModuleCode).Value & "'"
                gv.Rows(index).Cells(colSubModuleDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                isLoad = True
                gv.Rows(index).Cells(colFormId).Value = ""
                gv.Rows(index).Cells(colScreenCaption).Value = ""
                isLoad = False
            Else
                isLoad = True
                gv.Rows(index).Cells(colSubModuleDesc).Value = ""
                gv.Rows(index).Cells(colSubModuleCode).Value = ""
                gv.Rows(index).Cells(colFormId).Value = ""
                gv.Rows(index).Cells(colScreenCaption).Value = ""
                isLoad = False
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenScreen(index As Integer)
        Try
            If clsCommon.CompairString(gv.Rows(index).Cells(colIntegrationType).Value, "New") = CompairStringResult.Equal Then

                If clsCommon.myLen(gv.Rows(index).Cells(colSubModuleCode).Value) <= 0 Then
                    Throw New Exception("Please Select Sub Module First")
                End If
                'Dim qry As String = " select Program_Code ,Program_Name  from TSPL_PROGRAM_MASTER    "
                'gv.Rows(index).Cells(colFormId).Value = clsCommon.ShowSelectForm("RptFormIdFnd", qry, "Program_Code", "  Parent_Code='" & gv.Rows(index).Cells(colSubModuleCode).Value & "' ", gv.Rows(index).Cells(colFormId).Value)
                'If clsCommon.myLen(gv.Rows(index).Cells(colFormId).Value) > 0 Then
                '    qry = " select  Program_Name  from TSPL_PROGRAM_MASTER where  Program_Code='" & gv.Rows(index).Cells(colFormId).Value & "'"
                '    gv.Rows(index).Cells(colScreenCaption).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                'Else
                '    isLoad = True
                '    gv.Rows(index).Cells(colFormId).Value = ""
                '    gv.Rows(index).Cells(colScreenCaption).Value = ""
                '    isLoad = False
                'End If
            ElseIf clsCommon.CompairString(gv.Rows(index).Cells(colIntegrationType).Value, "Existing") = CompairStringResult.Equal Then
                Dim qry As String = " select Child.Program_code as FormId,Child.Program_Name as FormName,Child.Parent_code as SubModuleCode,TSPL_PROGRAM_MASTER.Program_Name as SubmoduleName,TSPL_PROGRAM_MASTER.Parent_Code  as ModuleCode ,Module.Program_Name as ModuleName from  TSPL_PROGRAM_MASTER as  Child left outer join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code=Child.Parent_Code left outer join TSPL_PROGRAM_MASTER as Module on Module.Program_Code=TSPL_PROGRAM_MASTER.Parent_Code where isnull(TSPL_PROGRAM_MASTER.Parent_Code,'') not in('ExpertERP','')  "
                Dim dt As DataRow = clsCommon.ShowSelectFormForRow("RptFormIdFnd", qry)
                If dt IsNot Nothing Then
                    isLoad = True
                    gv.Rows(index).Cells(colModuleCode).Value = dt("ModuleCode")
                    gv.Rows(index).Cells(colModuleDesc).Value = dt("ModuleName")
                    gv.Rows(index).Cells(colSubModuleCode).Value = dt("SubModuleCode")
                    gv.Rows(index).Cells(colSubModuleDesc).Value = dt("SubmoduleName")
                    gv.Rows(index).Cells(colFormId).Value = dt("FormId")
                    gv.Rows(index).Cells(colScreenCaption).Value = dt("FormName")

                    isLoad = False
                Else
                    isLoad = True
                    gv.Rows(index).Cells(colModuleCode).Value = ""
                    gv.Rows(index).Cells(colModuleDesc).Value = ""
                    gv.Rows(index).Cells(colSubModuleCode).Value = ""
                    gv.Rows(index).Cells(colSubModuleDesc).Value = ""
                    gv.Rows(index).Cells(colFormId).Value = ""
                    gv.Rows(index).Cells(colScreenCaption).Value = ""
                    isLoad = False
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        SaveData()

    End Sub

    Public Shared Sub CallStartupFunction(AsmName As String)
        Try
            Try
                clsCreateAllTables.InvokeMethodSlow(AsmName, "clsCreateAllTableCustom", "CreateAllTable", Nothing)
            Catch ex As Exception
            End Try

            Try
                clsCreateAllTables.InvokeMethodSlow(AsmName, "clsAllStoreProcedureCustom", "CreateAllStoreProcedure", Nothing)
            Catch ex As Exception
            End Try


            Try
                clsCreateAllTables.InvokeMethodSlow(AsmName, "clsFixedParameterCustom", "FixedParameterValues", Nothing)
            Catch ex As Exception
            End Try

            Try
                clsCreateAllTables.InvokeMethodSlow(AsmName, "clsAutoIntegration", "ProgramCodeNewCustomize", Nothing)
            Catch ex As Exception
            End Try

            Try
                Dim myAssembly As Assembly = Assembly.GetExecutingAssembly()
                Dim myAssemblyName As AssemblyName = myAssembly.GetName()
                Dim CurrEXEVersion As String = clsCommon.myCstr(myAssemblyName.Version).Trim()
                Dim obj(0 To 1) As Object
                obj(0) = CurrEXEVersion
                obj(1) = Nothing
                clsCreateAllTables.InvokeMethodSlow(AsmName, "clsPostCreateTable", "Post_AlterOrUpdateAllTables", obj)
                obj = Nothing
            Catch ex As Exception
            End Try

            Try
                clsCreateAllTables.InvokeMethodSlow(AsmName, "clsCancelTableClassCustom", "CreateAllTable", Nothing)
            Catch ex As Exception
            End Try

            Try
                clsCreateAllTables.InvokeMethodSlow(AsmName, "clsCancelTableClassCustom", "CancelConditionTableValues", Nothing)
            Catch ex As Exception
            End Try

            Try
                clsCreateAllTables.InvokeMethodSlow(AsmName, "clsCancelTableClassCustom", "CancelConditionTableValues", Nothing)
            Catch ex As Exception
            End Try

        Catch ex As Exception
        End Try
    End Sub
End Class
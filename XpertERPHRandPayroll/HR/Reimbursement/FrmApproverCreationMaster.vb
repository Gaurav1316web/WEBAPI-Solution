'' =============Created by Rohit gupta on May 12,2015
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class FrmApproverCreationMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoReqIssueNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoCCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoCCDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoReqQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoReturnQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoPenQty As GridViewDecimalColumn = New GridViewDecimalColumn()

    Const ColEmp_Code As String = "ColEmp_Code"
    Const ColEmp_Name As String = "ColEmp_Name"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public DocumentNo As String = Nothing
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmApproverCreationMaster)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")

        'End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag

    End Sub

    Private Sub FrmApproverCreationMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Master")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Master")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Master")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnRequistionItems, "Press Ctrl+F7 for Select Purchase Requistion Items")
        RadPageView1.SelectedPage = RadPageViewPage1


        AddNew()
        SetLength()

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If

        ''End of For Custom Fields
        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment
        If clsCommon.myLen(DocumentNo) > 0 Then
            LoadData(DocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub SetLength()
        txtRemarks.MaxLength = 200
    End Sub


    Sub BlankAllControls()

        txtDate.Value = clsCommon.GETSERVERDATE()

        txtRemarks.Text = ""
        'UsLock1.Status = ERPTransactionStatus.Pending
        txtRequestBy.Enabled = True
        txtRequestBy.Value = ""
        lblRequestBy.Text = ""
        'txtDepartment.Value = ""
        'lblDepartment.Text = ""

        'fndReqNo.Value = ""
        'lblReqDate.Text = ""

        'added by priti
        'fndReqNo.Enabled = True
        'fndReqNo.Visible = True
        'lblReqDate.Visible = True
        'lblReq.Visible = True
        'lblReq.Text = "Requisition No"

        ' ended by priti

        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()

    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()


        repoICode = New GridViewTextBoxColumn()
        repoReqIssueNo = New GridViewTextBoxColumn()

        repoICode = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Employee Code"
        repoICode.Name = ColEmp_Code
        repoICode.Width = 200
        repoICode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoICode)

        repoReqIssueNo = New GridViewTextBoxColumn()
        repoReqIssueNo.FormatString = ""
        repoReqIssueNo.HeaderText = "Employee Name"
        repoReqIssueNo.Name = ColEmp_Name
        repoReqIssueNo.Width = 200
        gv1.MasterTemplate.Columns.Add(repoReqIssueNo)


        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = True
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        ReStoreGridLayout()














        '======================gv2======================
        gv2.Rows.Clear()
        gv2.Columns.Clear()


        repoICode = New GridViewTextBoxColumn()
        repoReqIssueNo = New GridViewTextBoxColumn()

        repoICode = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Employee Code"
        repoICode.Name = ColEmp_Code
        repoICode.Width = 200
        repoICode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoICode)

        repoReqIssueNo = New GridViewTextBoxColumn()
        repoReqIssueNo.FormatString = ""
        repoReqIssueNo.HeaderText = "Employee Name"
        repoReqIssueNo.Name = ColEmp_Name
        repoReqIssueNo.Width = 200
        gv2.MasterTemplate.Columns.Add(repoReqIssueNo)


        clsCustomFieldGrid.LoadBlankGrid(gv2, MyBase.ArrDetailFields)

        gv2.AllowDeleteRow = True
        gv2.AllowAddNewRow = True
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        gv2.TableElement.TableHeaderHeight = 40
        '=====================================
    End Sub

    Sub AddNew()
        'lblReq2.Visible = False
        'lblReq3.Visible = False
        BlankAllControls()
        isInsideLoadData = True
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Enabled = True
        txtDate.Focus()
        LoadBlankGrid()
        'If clsCommon.myLen(fndReqNo.Value) <= 0 Then
        gv1.Rows.AddNew()
        gv2.Rows.AddNew()
        'End If
        'chkWithoutRefNo.Checked = False
        'chkWithoutRefNo.Enabled = False
        'chkWithoutRefNoChanged()
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()

    End Sub

    Function AllowToSave() As Boolean
        Try



            If clsCommon.myLen(txtRequestBy.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Employee Code", Me.Text)
                txtRequestBy.Focus()
                Return False
            End If

            Return True
            '-----------Added By ---Pankaj Kumar---on-----04/05/2012--For Inserting Vehicle Mannually----------
        Catch
        End Try
        Return True
    End Function

    Sub SaveData(ByVal ChekPostBTn As Boolean)
        Try
            ''
            If (AllowToSave()) Then
                Dim obj As New clsApproverCreation()
                obj.Doc_Date = txtDate.Value

                obj.Emp_Code = txtRequestBy.Value
                obj.Remarks = txtRemarks.Text
                'obj.Req_IssueNo = fndReqNo.Value
                'obj.RequisitionNo = lblReq3.Text
                'obj.Dept = txtDepartment.Value
                'obj.Dept_Desc = lblDepartment.Text

                obj.Arr = New List(Of clsApproverCreationDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsApproverCreationDetail()


                    objTr.Emp_Code = clsCommon.myCstr(txtRequestBy.Value)
                    objTr.Req_Emp_Code = clsCommon.myCstr(grow.Cells(ColEmp_Code).Value)
                    obj.Arr.Add(objTr)
                Next

                obj.ArrExp = New List(Of clsApproverCreationExpenseDetail)
                For Each grow As GridViewRowInfo In gv2.Rows
                    Dim objTr As New clsApproverCreationExpenseDetail()


                    objTr.Emp_Code = clsCommon.myCstr(txtRequestBy.Value)
                    objTr.exp_Emp_Code = clsCommon.myCstr(grow.Cells(ColEmp_Code).Value)
                    obj.ArrExp.Add(objTr)
                Next

                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                    clsCustomFieldGrid.GetData(obj.arrCustomFields, gv1, MyBase.ArrDetailFields, ColEmp_Code)
                End If
                ''End of For Custom Fields

                If (obj.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Emp_Code)
                    If ChekPostBTn = False Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If

                    LoadData(obj.Emp_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True

            BlankAllControls()
            LoadBlankGrid()

            Dim obj As New clsApproverCreation()
            obj = clsApproverCreation.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Emp_Code) > 0) Then
                'If obj.Status = ERPTransactionStatus.Approved Then
                '    btnSave.Enabled = False
                '    btnPost.Enabled = False
                '    btnDelete.Enabled = False
                'End If
                'UsLock1.Status = obj.Status
                isNewEntry = False
                btnSave.Text = "Update"
                txtDate.Value = obj.Doc_Date

                txtRemarks.Text = obj.Remarks
                txtRequestBy.Value = obj.Emp_Code
                lblRequestBy.Text = obj.Emp_Name

                LoadBlankGrid()
                If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                    isInsideLoadData = False
                    For Each obj1 As clsApproverCreationDetail In obj.Arr
                        gv1.Rows.AddNew()

                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColEmp_Code).Value = obj1.Req_Emp_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColEmp_Name).Value = obj1.Req_Emp_Name
                    Next
                    isInsideLoadData = True
                Else
                    gv1.Rows.AddNew()
                End If

                If (obj.ArrExp IsNot Nothing AndAlso obj.ArrExp.Count > 0) Then
                    isInsideLoadData = False
                    For Each obj1 As clsApproverCreationExpenseDetail In obj.ArrExp
                        gv2.Rows.AddNew()

                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColEmp_Code).Value = obj1.exp_Emp_Code
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColEmp_Name).Value = obj1.exp_Emp_Name
                    Next
                    isInsideLoadData = True
                Else
                    gv2.Rows.AddNew()
                End If
                UcAttachment1.LoadData(obj.Emp_Code)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.BlankAllControls()
                    UcCustomFields1.LoadData(obj.Emp_Code)
                End If
                'If clsCommon.myLen(fndReqNo.Value) > 0 Then
                '    LoadReqDataHead(fndReqNo.Value)
                '    gv1.Rows.Clear()
                'End If
                'txtDepartment.Value = obj.Dept
                'lblDepartment.Text = obj.Dept_Desc

                'TxtMachinery.Value = obj.Machine_Id
                'lblMachineDesc.Text = clsDBFuncationality.getSingleValue("select Description From TSPL_GL_SEGMENT_CODE Where Seg_No= '5' AND Segment_Code= '" + TxtMachinery.Value + "'")


                'added by priti
                'fndReqNo.Enabled = False
                'added by priti on 25/07/2013  to allow  for wrong entry
                'txtToLocation.Enabled = False
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.Emp_Code)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.Emp_Code, MyBase.Form_ID, gv1)
                ''End of For Custom Fields
                UcAttachment1.LoadData(obj.Emp_Code)
            End If


            ''For Custom Fields


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = True
        End Try
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub FrmApproverCreationMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub


    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub txtRequestBy__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRequestBy._MYValidating
        AddNew()
        Dim obj As clsEmployeeMaster = clsEmployeeMaster.FinderForEmployee(txtRequestBy.Value, isButtonClicked)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
            LoadData(obj.EMP_CODE, NavigatorType.Current)
            txtRequestBy.Value = obj.EMP_CODE
            lblRequestBy.Text = obj.Emp_Name
        Else
            txtRequestBy.Value = ""
            lblRequestBy.Text = ""
            AddNew()
        End If
    End Sub


    Private Sub gv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F4 Then

        End If
    End Sub

    Private Sub BtnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnsaveLayout.Click
        '    If clsCommon.myLen(GetReportID()) > 0 Then
        gv1.MasterTemplate.FilterDescriptors.Clear()
        Dim obj As New clsGridLayout()
        obj.ReportID = "MilkITMGrid"
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout = New MemoryStream()
        gv1.SaveLayout(obj.GridLayout)
        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        obj.GridColumns = gv1.ColumnCount
        If obj.SaveData() Then
            common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
        End If
        ''richa agarwal regarding memory leakage
        obj.GridLayout.Close()
        obj.GridLayout.Dispose()
        ''---------------
        'End If
    End Sub

    Private Sub BtnDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDeleteLayout.Click
        clsGridLayout.DeleteData("MilkITMGrid", objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            'If clsCommon.myLen("LoadinMainGrid") > 0 Then
            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData("MilkITMGrid", "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                    gv1.Columns(ii).IsVisible = False
                    gv1.Columns(ii).VisibleInColumnChooser = True
                Next

                gv1.LoadLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
            'End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub


    Private Sub gv1_CellValueChanged1(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If isInsideLoadData Then
                isInsideLoadData = False
                If e.Column Is gv1.Columns(ColEmp_Code) Then
                    Dim obj As clsEmployeeMaster = LoadEmployee()
                    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
                        gv1.CurrentRow.Cells(ColEmp_Code).Value = obj.EMP_CODE
                        gv1.CurrentRow.Cells(ColEmp_Name).Value = obj.Emp_Name
                    End If
                End If
                isInsideLoadData = True
            End If
        Catch ex As Exception
            isInsideLoadData = False
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Function LoadEmployee()
        Dim obj As clsEmployeeMaster = clsEmployeeMaster.FinderForEmployee(txtRequestBy.Value, True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
            Return obj
        Else
            Return Nothing
        End If
    End Function

    Private Sub gv2_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv2.CellValueChanged
        Try
            If isInsideLoadData Then
                isInsideLoadData = False

                If e.Column Is gv2.Columns(ColEmp_Code) Then
                    Dim obj As clsEmployeeMaster = LoadEmployee()
                    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
                        gv2.CurrentRow.Cells(ColEmp_Code).Value = obj.EMP_CODE
                        gv2.CurrentRow.Cells(ColEmp_Name).Value = obj.Emp_Name
                    End If
                End If
                isInsideLoadData = True
            End If
        Catch ex As Exception
            isInsideLoadData = False
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub btnDelete_Click1(sender As Object, e As EventArgs) Handles btnDelete.Click
        clsApproverCreation.DeleteData(txtRequestBy.Value)
        clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully.", Me.Text)
        LoadData(txtRequestBy.Value, NavigatorType.Current)
    End Sub
End Class


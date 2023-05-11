'--preeti gupta-ticket no.[BM00000003133]
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Threading
Imports System.Data.Sql
Imports common
Public Class FrmTemplateCreation
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

#Region "Variables"
    Public custCode As String
    Public custName As String
    Public CustAddress As String
    Public Route As String
    Const colLineNo As String = "COLLNO"
    Const colCheck As String = "Check"
    Const colRouteCode As String = "RouteCode"
    Const colCustId As String = "CustId"
    Const colCustName As String = "CustName "
    Const colCustAddress As String = "CustAddress"
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Public Arr As ArrayList
    Public arrCust As New List(Of clsFilterCustomer)
#End Region

    Private Sub FrmTemplateCreation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Keys.Tab Or e.KeyCode = Keys.Enter) AndAlso TxtTmplateId.Focus Then
            txtDesc.Focus()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteTemplate()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Public Sub SetLength()
        TxtTmplateId.MyMaxLength = 12
        txtDesc.MaxLength = 50

    End Sub

    Private Sub FrmTemplateCreation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLength()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")

        SetUserMgmtNew()
        LoadBlankGrid()
        dtpStartDate.Value = clsCommon.GETSERVERDATE
        TxtTmplateId.MyReadOnly = True
        btnsave.Enabled = True
        TxtTmplateId.Value = ""
        txtDesc.Text = ""
        TxtTmplateId.MyReadOnly = False
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnTmplateCreation)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        ' btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    '---------------------Added By -----Pankaj Kumar-------------on--29/03/2012-------------
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "TMP-CR"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnsave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btndelete.Enabled = False
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    '    '-----------------------------------Code Ends Here-------------------------------
    'End Function


    Sub LoadBlankGrid()
        dgvCustomer.AddNewRowPosition = SystemRowPosition.Bottom
        dgvCustomer.Rows.Clear()
        dgvCustomer.Columns.Clear()

        'Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoLineNo = New GridViewDecimalColumn()
        'repoLineNo.FormatString = ""
        'repoLineNo.HeaderText = "Line No"
        'repoLineNo.Name = colLineNo
        'repoLineNo.Width = 50
        'repoLineNo.ReadOnly = True
        'repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'dgvCustomer.MasterTemplate.Columns.Add(repoLineNo)

        Dim CustId As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CustId.FormatString = ""
        CustId.HeaderText = "Outlet Id"
        CustId.Name = colCustId
        CustId.ReadOnly = True
        CustId.Width = 121
        dgvCustomer.MasterTemplate.Columns.Add(CustId)

        Dim RouteCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RouteCode.FormatString = ""
        RouteCode.HeaderText = "Route"
        RouteCode.Name = colRouteCode
        RouteCode.ReadOnly = True
        RouteCode.Width = 100
        dgvCustomer.MasterTemplate.Columns.Add(RouteCode)

        Dim CustName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CustName.FormatString = ""
        CustName.HeaderText = "Outlet"
        CustName.Name = colCustName
        CustName.Width = 201
        CustName.ReadOnly = True
        dgvCustomer.MasterTemplate.Columns.Add(CustName)

        Dim CustAddress As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CustAddress.FormatString = ""
        CustAddress.HeaderText = "Address"
        CustAddress.Name = colCustAddress
        CustAddress.Width = 251
        CustAddress.ReadOnly = True
        dgvCustomer.MasterTemplate.Columns.Add(CustAddress)

        dgvCustomer.AllowDeleteRow = True
        dgvCustomer.AllowAddNewRow = False
        dgvCustomer.ShowGroupPanel = False
        dgvCustomer.AllowColumnReorder = False
        dgvCustomer.AllowRowReorder = False
        dgvCustomer.EnableSorting = False
        dgvCustomer.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvCustomer.MasterTemplate.ShowRowHeaderColumn = False
        dgvCustomer.TableElement.TableHeaderHeight = 30
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If (AllowToSave()) Then

                Dim Arr As New List(Of clsTmplateCreation)
                For Each grow As GridViewRowInfo In dgvCustomer.Rows
                    Dim obj As New clsTmplateCreation()
                    obj.TmplateId = TxtTmplateId.Value
                    obj.Description = txtDesc.Text
                    obj.StartDate = clsCommon.myCDate(dtpStartDate.Value.Date, "dd-MMM-yyyy")
                    obj.Cust_Code = clsCommon.myCstr(grow.Cells(colCustId).Value)
                    Arr.Add(obj)
                Next
                If (Arr Is Nothing OrElse Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Add atleast one 'Customer'")
                    Return
                End If

                If (clsTmplateCreation.SaveData(TxtTmplateId.Value, Arr)) Then
                    If btnsave.Text = "Save" Then
                        common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    ElseIf btnsave.Text = "Update" Then
                        common.clsCommon.MyMessageBoxShow("Data Updated Successfully")
                    End If
                    LoadData(TxtTmplateId.Value, NavigatorType.Current)
                    TxtTmplateId.MyReadOnly = False
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(TxtTmplateId.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please  Insert 'Template id'")
                TxtTmplateId.Focus()
                Return False
            End If

            Dim arrCustomer As New List(Of String)()
            For ii As Integer = 0 To dgvCustomer.Rows.Count - 1
                Dim strCustCode As String = clsCommon.myCstr(dgvCustomer.Rows(ii).Cells(colCustId).Value)
                Dim strCustName As String = clsCommon.myCstr(dgvCustomer.Rows(ii).Cells(colCustName).Value)
                For jj As Integer = 0 To dgvCustomer.Rows.Count - 1
                    If (ii = jj) Then
                        Continue For
                    End If
                    If (clsCommon.CompairString(strCustCode, clsCommon.myCstr(dgvCustomer.Rows(jj).Cells(colCustId).Value)) = CompairStringResult.Equal) Then
                        common.clsCommon.MyMessageBoxShow("Already selected Item " + strCustCode.Trim() + "( " + strCustName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + "")
                        Return False
                    End If
                Next

                If Not arrCustomer.Contains(strCustCode) Then
                    arrCustomer.Add(strCustCode)
                End If
            Next


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function


    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub

    Public Sub Reset()
        TxtTmplateId.MyReadOnly = False
        btnsave.Enabled = True
        TxtTmplateId.Value = ""
        txtDesc.Text = ""
        dtpStartDate.Value = clsCommon.GETSERVERDATE
        LoadBlankGrid()
        btnsave.Text = "Save"
    End Sub

    Private Sub TxtCategoryCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtTmplateId._MYValidating
        If TxtTmplateId.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Distinct Tmplate_Id AS code, Description   from TSPL_CUSTOMER_TEMPLATE_MASTER"
            TxtTmplateId.Value = clsCommon.ShowSelectForm("TemplatFiltrFND", qry, "Code", "", TxtTmplateId.Value, "Code", isButtonClicked)
            LoadData(TxtTmplateId.Value, NavigatorType.Current)
        End If
    End Sub


    Private Sub TxtTmplateId__MYNavigator_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles TxtTmplateId._MYNavigator
        TxtTmplateId.MyReadOnly = True
        Try
            LoadData(TxtTmplateId.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        TxtTmplateId.MyReadOnly = False
    End Sub


    Private Sub TxtTmplateId_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtTmplateId.Leave
        Dim qry As String = "Select * from TSPL_CUSTOMER_TEMPLATE_MASTER Where Tmplate_Id='" + TxtTmplateId.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count <= 0 Then
            txtDesc.Text = ""
            txtDesc.Focus()
            dtpStartDate.Value = clsCommon.GETSERVERDATE
            LoadBlankGrid()
            btnsave.Text = "Save"
        Else
            LoadData(TxtTmplateId.Value, NavigatorType.Current)
        End If
    End Sub

    Public Sub LoadData(ByVal strTmplateId As String, ByVal navType As common.NavigatorType)
        Try
            isInsideLoadData = True
            Dim Qry As String = "select TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id, TSPL_CUSTOMER_TEMPLATE_MASTER.Description, TSPL_CUSTOMER_TEMPLATE_MASTER.start_Date, TSPL_CUSTOMER_MASTER.Route_No, TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name, Case when len(TSPL_CUSTOMER_MASTER.Add1)>0 then TSPL_CUSTOMER_MASTER.Add1 else '' end +case when len(TSPL_CUSTOMER_MASTER.Add2)>0 then ','  else  '' end  +case when len(TSPL_CUSTOMER_MASTER.Add2)>0 then TSPL_CUSTOMER_MASTER.Add2  else  '' end + case when len(TSPL_CUSTOMER_MASTER.Add3)>0 then ','  else  '' end +case when len(TSPL_CUSTOMER_MASTER.Add3)>0 then TSPL_CUSTOMER_MASTER.Add3  else  '' end as CustAddress   from TSPL_CUSTOMER_TEMPLATE_MASTER Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id=TSPL_CUSTOMER_MASTER.Cust_Code Where 1=1 "
            Select Case navType
                Case NavigatorType.First
                    Qry += " and TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id=(select MIN(Tmplate_Id) from TSPL_CUSTOMER_TEMPLATE_MASTER)"
                Case NavigatorType.Last
                    Qry += " and TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id=(select Max(Tmplate_Id) from TSPL_CUSTOMER_TEMPLATE_MASTER)"
                Case NavigatorType.Next
                    Qry += " and TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id=(select Min(Tmplate_Id) from TSPL_CUSTOMER_TEMPLATE_MASTER where Tmplate_Id > '" + strTmplateId + "')"
                Case NavigatorType.Previous
                    Qry += " and TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id=(select Max(Tmplate_Id) from TSPL_CUSTOMER_TEMPLATE_MASTER where Tmplate_Id < '" + strTmplateId + "')"
                Case NavigatorType.Current
                    Qry += " and TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id='" + strTmplateId + "'"
            End Select
            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(Qry)
            LoadBlankGrid()

            If dt.Rows.Count <= 0 Then
                Return
            End If

            ' Dim lno As Integer
            TxtTmplateId.Value = clsCommon.myCstr(dt.Rows(0)("Tmplate_Id"))
            txtDesc.Text = clsCommon.myCstr(dt.Rows(0)("description"))
            dtpStartDate.Value = clsCommon.GetPrintDate(dt.Rows(0)("start_Date"))
            For Each dr As DataRow In dt.Rows
                'lno = lno + 1
                dgvCustomer.Rows.AddNew()
                'dgvCustomer.Rows(dgvCustomer.Rows.Count - 1).Cells(colLineNo).Value = lno
                dgvCustomer.Rows(dgvCustomer.Rows.Count - 1).Cells(colCustId).Value = clsCommon.myCstr(dr("Cust_Code"))
                dgvCustomer.Rows(dgvCustomer.Rows.Count - 1).Cells(colRouteCode).Value = clsCommon.myCstr(dr("Route_No"))
                dgvCustomer.Rows(dgvCustomer.Rows.Count - 1).Cells(colCustName).Value = clsCommon.myCstr(dr("Customer_Name"))
                dgvCustomer.Rows(dgvCustomer.Rows.Count - 1).Cells(colCustAddress).Value = clsCommon.myCstr(dr("CustAddress"))

            Next
            btnsave.Text = "Update"
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try

    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteTemplate()
    End Sub

    Public Sub DeleteTemplate()
        If clsCommon.myLen(TxtTmplateId.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        If (myMessages.deleteConfirm()) Then
            If clsTmplateCreation.DeleteData(TxtTmplateId.Value) Then
                common.clsCommon.MyMessageBoxShow("Data Deleted Successfully")
                TxtTmplateId.MyReadOnly = True
                Reset()
            End If
        End If
    End Sub


    Private Sub RadMenuItemExportt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemExportt.Click
        OpenInterface()
    End Sub

    Public Sub OpenInterface()
        Dim frm As New FrmTemplateExport3()
        frm.ShowDialog()
    End Sub

    Private Sub RadMenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemImport.Click
        ImportFromExcel()
    End Sub

    Public Sub ImportFromExcel()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Template Id", "Description", "Start Date", "Customer Id", "Created By", "Created Date", "Modify By", "Modified date", "Company") Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim TmplateId As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If TmplateId.Length > 12 Then
                        Throw New Exception("Check the length of 'Template Id'.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim Desc As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If Desc.Length > 50 Then
                        Throw New Exception("Check the length of 'Description'.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim StartDate As String = Nothing
                    If (grow.Cells(2).Value IsNot DBNull.Value AndAlso clsCommon.myLen(grow.Cells(2).Value) > 0 And clsCommon.myLen(grow.Cells(2).Value) < 11) Then
                        StartDate = clsCommon.GetPrintDate((grow.Cells(2).Value), "dd-MM-yyyy")
                    Else
                        Throw New Exception("Please insert Start Date in Format- i.e. (yyyy-MM-dd)")
                    End If

                    Dim CustId As String = clsCommon.myCstr(grow.Cells(3).Value)
                    If CustId.Length > 12 Then
                        Throw New Exception("Check the length of 'Customer Id'.")
                        trans.Rollback()
                        Exit Sub
                    ElseIf CustId.Length <= 0 Then
                        Throw New Exception("'Customer Id' Cann't Be Left Blank.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim CreatedBy As String = clsCommon.myCstr(grow.Cells(4).Value)
                    If CreatedBy.Length > 12 Then
                        Throw New Exception("Check the length of 'Created By'.")
                        trans.Rollback()
                        Exit Sub
                    End If


                    Dim ModifyBy As String = clsCommon.myCstr(grow.Cells(6).Value)
                    If ModifyBy.Length > 12 Then
                        Throw New Exception("Check the length of 'Modified By '.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim CompCode As String = clsCommon.myCstr(grow.Cells(8).Value)
                    If CompCode.Length > 8 Then
                        Throw New Exception("Check the length of 'Company Code'.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim sql1 As String = "select count(*) from TSPL_CUSTOMER_TEMPLATE_MASTER where Tmplate_id='" + TmplateId + "' AND Cust_Id='" + CustId + "'"
                    Dim i As Integer = CInt(clsDBFuncationality.getSingleValue(sql1, trans))

                    If (i = 0) Then
                        Dim qry As String = "insert into TSPL_CUSTOMER_TEMPLATE_MASTER( Tmplate_Id , Description, Start_Date, Cust_Id, Created_By, Created_Date, Modified_By, Modified_Date, comp_code ) values('" + Convert.ToString(TmplateId) + "','" + Convert.ToString(Desc) + "', " + IIf(clsCommon.myLen(StartDate) > 0, "Convert(Date, '" + StartDate + "', 103)", "Null") + ", '" + Convert.ToString(CustId) + "','" + Convert.ToString(CreatedBy) + "', '" + (clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")).ToString() + "', '" + Convert.ToString(ModifyBy) + "', '" + (clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")).ToString() + "', '" + Convert.ToString(CompCode) + "')"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    Else
                        Dim qry As String = "update TSPL_CUSTOMER_TEMPLATE_MASTER set Description= '" + Convert.ToString(Desc) + "'  , Start_Date=" + IIf(clsCommon.myLen(StartDate) > 0, "Convert(Date, '" + StartDate + "', 103)", "Null") + ", Modified_By= '" + Convert.ToString(ModifyBy) + "', Modified_Date= '" + (clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")).ToString() + "', comp_code='" + Convert.ToString(CompCode) + "' where Tmplate_Id= '" + Convert.ToString(TmplateId) + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            Finally
                clsCommon.ProgressBarHide()
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub


    Private Sub btnAddCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCustomer.Click
        Dim frm As New FrmCustomerSelector()
        If dgvCustomer.RowCount > 0 Then
            frm.ArrIn = New List(Of String)
            For ii As Integer = 0 To dgvCustomer.Rows.Count - 1
                Dim str As String = clsCommon.myCstr(dgvCustomer.Rows(ii).Cells(colCustId).Value)
                If clsCommon.myLen(str) > 0 Then
                    frm.ArrIn.Add(str)
                End If
            Next
        End If
        frm.ShowDialog()

        If frm.Arr IsNot Nothing AndAlso frm.Arr.Count > 0 Then
            For Each obj As clsCustomerMaster In frm.Arr
                dgvCustomer.Rows.AddNew()
                dgvCustomer.Rows(dgvCustomer.Rows.Count - 1).Cells(colCustId).Value = obj.Cust_Code
                dgvCustomer.Rows(dgvCustomer.Rows.Count - 1).Cells(colRouteCode).Value = obj.Route_No
                dgvCustomer.Rows(dgvCustomer.Rows.Count - 1).Cells(colCustName).Value = obj.Customer_Name
                dgvCustomer.Rows(dgvCustomer.Rows.Count - 1).Cells(colCustAddress).Value = obj.address
            Next
            TxtTmplateId.MyReadOnly = True
        End If
    End Sub

 
    Private Sub RadMenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemExport.Click

    End Sub
End Class

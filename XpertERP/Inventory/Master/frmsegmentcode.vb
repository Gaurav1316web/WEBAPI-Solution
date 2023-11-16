Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Telerik.Collections.Generic
Imports Excel = Microsoft.Office.Interop.Excel
Imports common
Imports System.IO
'Start date =18/5/2011
'End date =20/5/2011
'Last modify date = 12/10/2011 by Vipin(Adding filter on the grid view columns)
'Database =TSPLERP
' Tables=Tspl_gl_segment_code
'' UPDATION by richa agarwal ticket no BM00000005720
'sanjay Ticket No-TEC/20/06/19-000561 Save & Delete Layout
'Sanjay Add Email Department
Public Class Frmsegmentcode
    Inherits FrmMainTranScreen
#Region "Variables"
    'This cunstructer is used to send usercode and compcode in tables
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dr As SqlDataReader
    Dim ds As New DataSet
    Dim dt As DataTable
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.segmentCode)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied")
            Me.Close()
            Exit Sub
        End If
        rdbtnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If rdbtnsave.Visible = True Then
            rdmenuimport.Enabled = True
            rdmenuexport.Enabled = True
        Else
            rdmenuimport.Enabled = False
            rdmenuexport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        rdbtndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Private Sub Frmsegmentcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            rdgdvsegmentcode.AddNewRowPosition = SystemRowPosition.Bottom

            Dim obj As New GridViewTextBoxColumn()
            obj.Name = "CODE"
            rdgdvsegmentcode.Columns.Add(obj)
            obj.HeaderText = "Segment Code"
            obj.Width = 125

            Dim obj1 As New GridViewTextBoxColumn()
            rdgdvsegmentcode.Columns.Add(obj1)
            obj1.HeaderText = "Description"
            obj1.Width = 225

            Dim obj2 As New GridViewTextBoxColumn()
            obj2.Name = "ACCOUNTCODE"
            rdgdvsegmentcode.Columns.Add(obj2)
            obj2.HeaderText = "Fiscal Year End Account"
            obj2.Width = 200

            Dim obj3 As New GridViewComboBoxColumn()
            rdgdvsegmentcode.Columns.Add(obj3)
            obj3.HeaderText = "GIT"
            obj3.Width = 125
            obj3.DataSource = GetGITType()
            obj3.ValueMember = "Code"
            obj3.DisplayMember = "Name"

            Dim obj4 As New GridViewTextBoxColumn()
            obj4.Name = "STATE_CODE"
            rdgdvsegmentcode.Columns.Add(obj4)
            obj4.HeaderText = "State Code"
            obj4.Width = 125
            obj4.IsVisible = True

            Dim obj5 As New GridViewTextBoxColumn()
            obj5.Name = "STATE_NAME"
            rdgdvsegmentcode.Columns.Add(obj5)
            obj5.HeaderText = "State Name"
            obj5.ReadOnly = True
            obj5.Width = 125
            obj5.IsVisible = True
            ''

            Dim obj6 As New GridViewTextBoxColumn()
            obj6.Name = "Email_Department"
            rdgdvsegmentcode.Columns.Add(obj6)
            obj6.HeaderText = "Email Department"
            obj6.Width = 125
            obj6.IsVisible = True

            obj2 = New GridViewTextBoxColumn()
            obj2.Name = "ACCOUNTCODECHECK"
            obj2.HeaderText = "Account Code Check"
            obj2.IsVisible = False
            obj2.ReadOnly = True
            rdgdvsegmentcode.Columns.Add(obj2)

            obj = New GridViewTextBoxColumn()
            obj.Name = "SegmentCodeCheck"
            obj.HeaderText = "Segment Code Check"
            obj.IsVisible = False
            obj.ReadOnly = True
            rdgdvsegmentcode.Columns.Add(obj)

            Dim query As String = "select seg_name As[Segment Name]from tspl_gl_segment where seg_no not in (select Account_segment from tspl_glsetting )"
            dt = clsDBFuncationality.GetDataTable(query)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    rddrplstsegmentcode.Items.Add(dr(0).ToString())
                Next
            End If


            Dim strvalue As String = connectSql.RunScalar("select seg_name from tspl_gl_segment where seg_no = '2'")
            rddrplstsegmentcode.Text = strvalue
            rdgdvsegmentcode.Enabled = True
            rdbtnsave.Enabled = True
            rdbtndelete.Enabled = True
            Dim str1 As String
            str1 = clsDBFuncationality.getSingleValue("Select seg_useinclosing from tspl_gl_segment where Seg_name ='" + rddrplstsegmentcode.Text + "'")
            If str1 = "Y" Then
                funacc_insert()
            Else
                funinsert()
            End If

            Dim LocSeg As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(seg_name,'') As SegName from tspl_gl_segment where seg_no = '3'"))
            If clsCommon.CompairString(rddrplstsegmentcode.Text, LocSeg) = CompairStringResult.Equal Then
                rdgdvsegmentcode.Columns("Email_Department").IsVisible = True
            Else
                rdgdvsegmentcode.Columns("Email_Department").IsVisible = False
            End If

            Dim int1 As Integer
            int1 = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Seg_length from tspl_gl_segment where Seg_name ='" + rddrplstsegmentcode.Text.ToString() + "'"))
            Dim int As Integer
            If clsCommon.myLen(int1) > 0 Then
                int = Convert.ToInt32(int1)
            End If

            Dim lngth As GridViewTextBoxColumn = TryCast(Me.rdgdvsegmentcode.Columns(0), GridViewTextBoxColumn)
            lngth.MaxLength = int
            Dim dgvobj As GridViewTextBoxColumn = TryCast(Me.rdgdvsegmentcode.Columns(1), GridViewTextBoxColumn)
            dgvobj.MaxLength = 50
            ButtonToolTip.SetToolTip(rdbtnsave, "Press Alt+S for Save/Update Trasnaction")
            ButtonToolTip.SetToolTip(rdbtndelete, "Press Alt+D Delete Trasnaction")
            ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
            HideShowGridCols(False)
            PageSetupReport_ID = MyBase.Form_ID + rddrplstsegmentcode.Text
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub Frmsegmentcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rdbtnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rdbtndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    '' Anubhooti 15-Sep-2014 BM00000003782 
    Private Function GetGITType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Name") = "Yes"
        dr("Code") = "Y"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Name") = "No"
        dr("Code") = "N"
        dt.Rows.Add(dr)

        Return dt
    End Function
    Sub HideShowGridCols(ByVal ShowOrHide As Boolean)
        Try
            rdgdvsegmentcode.Columns("STATE_CODE").IsVisible = ShowOrHide
            rdgdvsegmentcode.Columns("STATE_NAME").IsVisible = ShowOrHide
        Catch ex As Exception
        End Try
    End Sub
    

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub
    'This function is used to bind segment code,description column in gridview.
    Public Sub funinsert()
        rdgdvsegmentcode.Columns(2).IsVisible = True
        Dim query As String
        rdgdvsegmentcode.AutoGenerateColumns = False
        Dim LocSeg As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(seg_name,'') As SegName from tspl_gl_segment where seg_no = '7'"))
        If clsCommon.CompairString(rddrplstsegmentcode.Text, LocSeg) = CompairStringResult.Equal Then
            rdgdvsegmentcode.Columns(3).IsVisible = True
        Else
            rdgdvsegmentcode.Columns(3).IsVisible = False
        End If
        query = "select segment_code as [Segment Code],description as [Description], Account_code as [Account Code], GIT , SM.STATE_CODE , SM.STATE_NAME, SC.Email_Department,Account_code as ACCOUNTCODECHECK,segment_code as SegmentCodeCheck from tspl_Gl_segment_code SC LEFT JOIN TSPL_STATE_MASTER SM ON SC.STATE_CODE = SM.STATE_CODE where Segment_name ='" + rddrplstsegmentcode.Text.ToString() + "'"
        transportSql.FillGridView(query, rdgdvsegmentcode)
        rdgdvsegmentcode.Columns(0).FieldName = "Segment Code"
        rdgdvsegmentcode.Columns(1).FieldName = "Description"
        rdgdvsegmentcode.Columns(2).FieldName = "Account Code"
        '' Anubhooti 15-Sep-2014 BM00000003782
        rdgdvsegmentcode.Columns(3).FieldName = "GIT"
        rdgdvsegmentcode.Columns(4).FieldName = "STATE_CODE"
        rdgdvsegmentcode.Columns(5).FieldName = "State_Name"
        rdgdvsegmentcode.Columns(6).FieldName = "Email_Department"
        rdgdvsegmentcode.Columns("SegmentCodeCheck").FieldName = "SegmentCodeCheck"
        'rdgdvsegmentcode.CurrentRow.Cells("GIT").Value = "N"
        rdgdvsegmentcode.AutoGenerateColumns = False
        rdgdvsegmentcode.Columns(0).Width = 100
        rdgdvsegmentcode.Columns(1).Width = 357
        rdgdvsegmentcode.Columns(3).Width = 125
        rdgdvsegmentcode.Columns(0).IsPinned = True
        If clsCommon.CompairString(rddrplstsegmentcode.Text, "Visi\PMX") = CompairStringResult.Equal Then
            rdgdvsegmentcode.Columns(0).ReadOnly = True
        Else
            rdgdvsegmentcode.Columns(0).ReadOnly = False
        End If

    End Sub
    'This function is used to bind segment code,description,Account Code column in gridview.
    Public Sub funacc_insert()
        rdgdvsegmentcode.Columns(2).IsVisible = True
        Dim query As String
        Dim LocSeg As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(seg_name,'') As SegName from tspl_gl_segment where seg_no = '7'"))
        If clsCommon.CompairString(rddrplstsegmentcode.Text, LocSeg) = CompairStringResult.Equal Then
            rdgdvsegmentcode.Columns(3).IsVisible = True
        Else
            rdgdvsegmentcode.Columns(3).IsVisible = False
        End If
        rdgdvsegmentcode.AutoGenerateColumns = False
        query = "select segment_code as [Segment Code],description as [Description], Account_code as [Account Code], GIT , SM.STATE_CODE , SM.STATE_NAME,SC.Email_Department,Account_code as ACCOUNTCODECHECK,segment_code as SegmentCodeCheck from tspl_Gl_segment_code SC LEFT JOIN TSPL_STATE_MASTER SM ON SC.STATE_CODE = SM.STATE_CODE where Segment_name = '" + rddrplstsegmentcode.Text.ToString() + "'"
        transportSql.FillGridView(query, rdgdvsegmentcode)
        rdgdvsegmentcode.Columns(0).FieldName = "Segment Code"
        rdgdvsegmentcode.Columns(1).FieldName = "Description"
        rdgdvsegmentcode.Columns(2).FieldName = "Account Code"
        rdgdvsegmentcode.Columns(3).FieldName = "GIT"
        rdgdvsegmentcode.Columns(4).FieldName = "STATE_CODE"
        rdgdvsegmentcode.Columns(5).FieldName = "STATE_NAME"
        rdgdvsegmentcode.Columns(6).FieldName = "Email_Department"
        rdgdvsegmentcode.Columns("ACCOUNTCODECHECK").FieldName = "ACCOUNTCODECHECK"
        rdgdvsegmentcode.Columns("SegmentCodeCheck").FieldName = "SegmentCodeCheck"
        rdgdvsegmentcode.AutoGenerateColumns = False
        rdgdvsegmentcode.Columns(0).Width = 110
        rdgdvsegmentcode.Columns(1).Width = 225
        rdgdvsegmentcode.Columns(2).Width = 125
        rdgdvsegmentcode.Columns(0).IsPinned = True
        rdgdvsegmentcode.Columns("STATE_CODE").IsVisible = True
        rdgdvsegmentcode.Columns("STATE_NAME").IsVisible = True

    End Sub
    Function AllowToSave() As Boolean
        'Dim obj As New ClsVehicleMaster()
        ' FillData()
        Dim VehicleSeg As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(seg_name,'') As SegName from tspl_gl_segment where seg_no = '2'"))
        rdbtnsave.Focus()
        Dim linno As Integer = 0
        If clsCommon.myLen(VehicleSeg) > 0 Then
            If clsCommon.CompairString(rddrplstsegmentcode.Text, VehicleSeg) = CompairStringResult.Equal Then
                For Each grow As GridViewRowInfo In rdgdvsegmentcode.Rows
                    linno += 1
                    If clsCommon.myLen(grow.Cells(0).Value) > 0 Then
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(1).Value)) > 0 Then
                            If clsCommon.myCstr(grow.Cells(1).Value.ToString().TrimEnd().Contains(" ")) = True Then
                                clsCommon.MyMessageBoxShow(Me, "Please check ! description (" & clsCommon.myCstr(grow.Cells(1).Value) & ") should not contain space at line no " & clsCommon.myCstr(linno) & ".")
                                Return False
                            End If
                        End If
                        If clsCommon.myCstr(grow.Cells(0).Value.ToString().TrimEnd().Contains(" ")) = True Then
                            clsCommon.MyMessageBoxShow(Me, "Please check ! vehicle code (" & clsCommon.myCstr(grow.Cells(0).Value) & ") should not contain space at line no " & clsCommon.myCstr(linno) & ".")
                            Return False
                        End If
                        ''richa agarwal against ticket no. BM00000004358
                        If clsCommon.myLen(grow.Cells(0).Value) > 0 Then
                            Dim index As Integer = 0
                            index = clsCommon.myCstr(grow.Cells(0).Value).IndexOf(" ")

                            If index > 0 AndAlso index < clsCommon.myLen(grow.Cells(0).Value) Then
                                clsCommon.MyMessageBoxShow(Me, "Please check ! vehicle code (" & clsCommon.myCstr(grow.Cells(0).Value) & ") should not contain space at line no " & clsCommon.myCstr(linno) & ".")
                                Return False
                            End If
                        End If
                        If clsCommon.myLen(grow.Cells(1).Value) > 0 Then
                            Dim index As Integer = 0
                            index = clsCommon.myCstr(grow.Cells(1).Value).IndexOf(" ")

                            If index > 0 AndAlso index < clsCommon.myLen(grow.Cells(1).Value) Then
                                clsCommon.MyMessageBoxShow(Me, "Please check ! despcription (" & clsCommon.myCstr(grow.Cells(1).Value) & ") should not contain space at line no " & clsCommon.myCstr(linno) & ".")
                                Return False
                            End If
                        End If
                        ''-------richa code ends here----------
                    Else
                        If clsCommon.myLen(grow.Cells(1).Value) > 0 Then
                            If clsCommon.myLen(grow.Cells(0).Value) <= 0 Then
                                clsCommon.MyMessageBoxShow(Me, "Please check ! code should not be blank at line no " & clsCommon.myCstr(linno) & ".")
                                Return False

                            End If
                        End If
                    End If

                Next
            End If
        End If

        Dim lngth As GridViewTextBoxColumn = TryCast(Me.rdgdvsegmentcode.Columns(0), GridViewTextBoxColumn)
        Dim intColumnLength As Integer = lngth.MaxLength
        For ii As Integer = 0 To rdgdvsegmentcode.RowCount - 1
            If Not clsCommon.CompairString(clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells(0).Value), clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells("SegmentCodeCheck").Value)) = CompairStringResult.Equal Then
                rdgdvsegmentcode.CurrentRow = rdgdvsegmentcode.Rows(ii)
                rdgdvsegmentcode.CurrentColumn = rdgdvsegmentcode.Columns(0)

                If clsCommon.myLen(rdgdvsegmentcode.Rows(ii).Cells(0).Value) <> intColumnLength Then
                    clsCommon.MyMessageBoxShow(Me, "Segment code Value [ " + clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells(0).Value) + " ] length should be " + clsCommon.myCstr(intColumnLength) + " characters")
                    Return False
                End If

                If clsCommon.myLen(rdgdvsegmentcode.Rows(ii).Cells("SegmentCodeCheck").Value) > 0 Then
                    Dim qry As String = "Select Seg_No from tspl_GL_segment where Seg_name ='" + rddrplstsegmentcode.Text + "'"
                    Dim dt As DataTable = Nothing
                    Dim SegNo As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                    If SegNo = 2 Then ''Vehicle master
                        qry = "select top 1 Vehicle_Id from tspl_vehicle_master where Vehicle_Id='" + clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells("SegmentCodeCheck").Value) + "'"
                        dt = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Segment code New Value [ " + clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells(0).Value) + " ],Old Value [ " + clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells("SegmentCodeCheck").Value) + " ] Cannot change becuase segment is used in vehicle master")
                            Return False
                        End If
                    ElseIf SegNo = 3 Then ''Department master
                        qry = "select top 1 DEPARTMENT_CODE from TSPL_DEPARTMENT_MASTER where DEPARTMENT_CODE='" + clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells("SegmentCodeCheck").Value) + "'"
                        dt = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Segment code New Value [ " + clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells(0).Value) + " ],Old Value [ " + clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells("SegmentCodeCheck").Value) + " ] Cannot change becuase segment is used in Department master")
                            Return False
                        End If
                    ElseIf SegNo = 4 Then ''Employee master
                        qry = "select top 1 EMP_CODE from TSPL_EMPLOYEE_MASTER where EMP_CODE ='" + clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells("SegmentCodeCheck").Value) + "'"
                        dt = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Segment code New Value [ " + clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells(0).Value) + " ],Old Value [ " + clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells("SegmentCodeCheck").Value) + " ] Cannot change becuase segment is used in employee master")
                            Return False
                        End If
                    ElseIf SegNo = 7 Then ''Location master
                        qry = "select top 1 Loc_Segment_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code ='" + clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells("SegmentCodeCheck").Value) + "'"
                        dt = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Segment code New Value [ " + clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells(0).Value) + " ],Old Value [ " + clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells("SegmentCodeCheck").Value) + " ] Cannot change becuase segment is used in location master")
                            Return False
                        End If
                        qry = "select top 1 Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Seg_Code7 ='" + clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells("SegmentCodeCheck").Value) + "'"
                        dt = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Segment code New Value [ " + clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells(0).Value) + " ],Old Value [ " + clsCommon.myCstr(rdgdvsegmentcode.Rows(ii).Cells("SegmentCodeCheck").Value) + " ] Cannot change becuase segment is used in GL Account")
                            Return False
                        End If
                    End If
                End If

                
            End If
        Next
        Return True
    End Function
    'This Function is used to save data in tables
    Public Sub funSave()
        Dim trans As SqlTransaction = Nothing
        Try
            If AllowToSave() Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.segmentCode, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                trans = clsDBFuncationality.GetTransactin()

                Dim query As String = "Delete from tspl_gl_segment_code where segment_name='" + rddrplstsegmentcode.Text + "'"
                clsDBFuncationality.ExecuteNonQuery(query, trans)

                Dim st As String
                st = clsDBFuncationality.getSingleValue("select seg_no from tspl_gl_segment where seg_name='" + rddrplstsegmentcode.Text + "'", trans)
                Dim str As String = ""
                If clsCommon.myLen(st) > 0 Then
                    str = st
                End If
                Dim stateCode As String = Nothing
                Dim segCodeState As String = Nothing
                Dim EmailDepartment As String = Nothing
                rdgdvsegmentcode.AllowAddNewRow = True
                For i As Integer = 0 To rdgdvsegmentcode.Rows.Count - 1
                    Dim str1 As String = clsCommon.myCstr(rdgdvsegmentcode.Rows(i).Cells(2).Value)

                    If (clsCommon.myCstr(rdgdvsegmentcode.Rows(i).Cells(4).Value) IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(rdgdvsegmentcode.Rows(i).Cells(4).Value)) > 0) Then
                        stateCode = clsCommon.myCstr(rdgdvsegmentcode.Rows(i).Cells(4).Value)
                    End If

                    stateCode = clsCommon.myCstr(rdgdvsegmentcode.Rows(i).Cells(4).Value)
                    segCodeState = clsCommon.myCstr(rdgdvsegmentcode.Rows(i).Cells(0).Value)

                    If (clsCommon.myCstr(rdgdvsegmentcode.Rows(i).Cells(6).Value) IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(rdgdvsegmentcode.Rows(i).Cells(6).Value)) > 0) Then
                        EmailDepartment = clsCommon.myCstr(rdgdvsegmentcode.Rows(i).Cells(6).Value)
                    End If
                    EmailDepartment = clsCommon.myCstr(rdgdvsegmentcode.Rows(i).Cells(6).Value)
                    'If String.IsNullOrEmpty(str1) Then
                    '    str1 = "NULL"
                    'End If
                    Dim SegCode As String
                    If clsCommon.CompairString(rddrplstsegmentcode.Text, "Visi\PMX") = CompairStringResult.Equal Then
                        Dim qry As String = "select MAX(Segment_code) from TSPL_GL_SEGMENT_CODE where Seg_No=" + str + " "
                        SegCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.myLen(SegCode) > 0 Then
                            SegCode = clsCommon.incval(SegCode)
                        Else
                            SegCode = "VSI00000001"
                        End If
                    Else
                        SegCode = clsCommon.myCstr(rdgdvsegmentcode.Rows(i).Cells(0).Value)
                    End If
                    '' Anubhooti 15-Sep-2014 BM00000003782
                    Dim StrGIT As String = clsCommon.myCstr(rdgdvsegmentcode.Rows(i).Cells(3).Value)
                    'If clsCommon.CompairString(StrGIT, "No") = CompairStringResult.Equal Then
                    '    StrGIT = "N"
                    'Else
                    '    StrGIT = "Y"
                    'End If


                    Dim SegDesc As String = clsCommon.myCstr(rdgdvsegmentcode.Rows(i).Cells(1).Value)
                    clsDBFuncationality.SaveAStorePorcedure(trans, "sp_tspl_gl_segmentcode_insert", New SqlParameter("@segno", str), New SqlParameter("@segmentname", rddrplstsegmentcode.Text.ToString()), New SqlParameter("@segmentcode", SegCode), New SqlParameter("@desc", SegDesc), New SqlParameter("@acccode", str1), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                    '' Anubhooti 15-Sep-2014 BM00000003782
                    clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_GL_SEGMENT_CODE SET GIT='" & StrGIT & "' WHERE Seg_No=" & str & " and Segment_name ='" & rddrplstsegmentcode.Text.ToString() & "' and Segment_code ='" & SegCode & "'", trans)
                    'KUNAL > DATE: 9-11-2016 > REQUEST NO : KLREQ000718 > TICKET :BM00000009477
                    clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_GL_SEGMENT_CODE SET STATE_CODE = '" & stateCode & "' WHERE  Segment_name ='" & rddrplstsegmentcode.Text.ToString() & "' and Segment_code ='" & segCodeState & "'", trans)

                    clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_GL_SEGMENT_CODE SET Email_Department = '" & EmailDepartment & "' WHERE  Segment_name ='" & rddrplstsegmentcode.Text.ToString() & "' and Segment_code ='" & segCodeState & "'", trans)
                    '---------Added By--Pankaj kumar---For Change in Master Table-----------------
                    If clsCommon.CompairString(rddrplstsegmentcode.Text, "Employees") = CompairStringResult.Equal Then
                        ChangeEmpDesc(trans, SegCode, SegDesc)
                    ElseIf clsCommon.CompairString(rddrplstsegmentcode.Text, "Vehicles") = CompairStringResult.Equal Then
                        ChangeVehicleDesc(trans, SegCode, SegDesc)
                    ElseIf clsCommon.CompairString(rddrplstsegmentcode.Text, "Visi\PMX") = CompairStringResult.Equal Then
                        ChangeVisiDesc(trans, SegCode, SegDesc)
                    End If
                    '-----------------------------------------------------------------------------
                Next
                myMessages.insert()
                trans.Commit()
                FillData()
                PageSetupReport_ID = MyBase.Form_ID + rddrplstsegmentcode.Text
                ReStoreGridLayout()
            End If
        Catch ex As Exception
            myExceptions(ex)
            trans.Rollback()
        End Try
    End Sub
    '---------Added By--Pankaj kumar---For Change in Master Table-----------------
    Public Shared Sub ChangeVehicleDesc(ByVal trans As SqlTransaction, ByVal vehicleCode As String, ByVal vehicleDesc As String)
        Try
            Dim qry As String = "Update TSPL_VEHICLE_MASTER set Number='" + vehicleDesc + "', Description='" + vehicleDesc + "' Where Vehicle_Id='" + vehicleCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Shared Sub ChangeEmpDesc(ByVal trans As SqlTransaction, ByVal empCode As String, ByVal empDesc As String)
        Try
            Dim qry As String = "Update TSPL_EMPLOYEE_MASTER set Emp_Name='" + empDesc + "' Where EMP_CODE='" + empCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim qry1 As String = "Update  TSPL_LOCATION_MASTER set Location_Desc='" + empDesc + "' Where Location_Code='" + empCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry1, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Shared Sub ChangeVisiDesc(ByVal trans As SqlTransaction, ByVal visiCode As String, ByVal visiDesc As String)
        Try
            Dim qry As String = "Update TSPL_VISI_MASTER set VisiMake='" + visiDesc + "' Where Visi_Id='" + visiCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    '---------------------------Code Ends Here-----------------------------------
    Private Sub rdgdvsegmentcode_CellClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
        Try
            If e.ColumnIndex >= 0 Then
                Dim i As Integer
                For i = 0 To rdgdvsegmentcode.Rows.Count - 1
                    If e.RowIndex >= 0 Then

                        If IsDBNull(rdgdvsegmentcode.Rows(i).Cells(0).Value) Then


                            rdgdvsegmentcode.Rows(i).Cells(0).ReadOnly = False
                        Else
                            rdgdvsegmentcode.Rows(i).Cells(0).ReadOnly = True
                        End If
                    End If
                Next

            End If
            'If Not IsDBNull(rdgdvsegmentcode.CurrentRow.Cells(0).Value) Then
            '    rdgdvsegmentcode.CurrentRow.Cells(1).ReadOnly = False

            'Else
            '    rdgdvsegmentcode.CurrentRow.Cells(1).ReadOnly = True


            'End If

        Catch ex As Exception
            myExceptions(ex)

        End Try

    End Sub
    Private Sub rdgdvsegmentcode_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
        If e.RowIndex >= 0 And e.ColumnIndex = 1 Or e.ColumnIndex = 2 Then
            If rdgdvsegmentcode.Columns.Count = 3 Then

                funSave()

            End If
            For i As Integer = 0 To rdgdvsegmentcode.Rows.Count - 1
                If e.RowIndex >= 0 Then

                    If IsDBNull(rdgdvsegmentcode.Rows(i).Cells(0).Value) Then


                        rdgdvsegmentcode.Rows(i).Cells(0).ReadOnly = False
                        rdgdvsegmentcode.Rows(i).Cells(0).ReadOnly = False
                    Else
                        rdgdvsegmentcode.Rows(i).Cells(0).ReadOnly = True

                    End If


                End If

            Next

        End If


    End Sub
    Private Sub rdbtnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        funSave()
    End Sub
    Private Sub rdbtndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtndelete.Click

        DeleteData()
    End Sub
    Sub DeleteData()
        If deleteConfirm() Then
            fundelete()

        End If

    End Sub
    'This functon is used to delete data.
    'modify by shipra add check on delete fun on 19-10-2012
    Public Sub fundelete()
        Dim check As String = ""
        If String.IsNullOrEmpty(rdgdvsegmentcode.Rows(0).Cells(2).Value) Then


            If Not IsDBNull(rdgdvsegmentcode.SelectedRows) Then
                Dim st As String = clsDBFuncationality.getSingleValue("select seg_no from tspl_gl_segment where seg_name='" + rddrplstsegmentcode.Text + "'")
                Dim str As String = "null"
                'str gives segment from master
                Dim str1 As String = rdgdvsegmentcode.CurrentRow.Cells(0).Value

                'str1 gives segment_code value 
                If clsCommon.myLen(st) > 0 Then
                    str = st
                    If (str = 2) Then
                        Dim qry As String = "Select Segment_code  from TSPL_GL_SEGMENT_CODE  where TSPL_GL_SEGMENT_CODE.Segment_code   not in(select * from(select Account_Seg_Code2 as AA  from TSPL_GL_ACCOUNTS where ISNULL(Account_Seg_Code2,'')<>''    union all select Vehicle_Id  as AA from TSPL_VEHICLE_MASTER)a where ISNULL(AA,'')<>''  ) and TSPL_GL_SEGMENT_CODE .Seg_No =2 and  TSPL_GL_SEGMENT_CODE.segment_code='" + str1 + "'"
                        check = clsDBFuncationality.getSingleValue(qry)
                        If (str1 <> check) Then
                            common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted.It is used in another process")
                            Exit Sub
                        End If
                    End If
                    If (str = 3) Then
                        Dim qry As String = " Select Segment_code  from TSPL_GL_SEGMENT_CODE  where TSPL_GL_SEGMENT_CODE.Segment_code   not in(select * from(select Account_Seg_Code3 as AA  from TSPL_GL_ACCOUNTS where ISNULL(Account_Seg_Code3,'')<>'')a where ISNULL(AA,'')<>''  ) and TSPL_GL_SEGMENT_CODE .Seg_No =3 and  TSPL_GL_SEGMENT_CODE.segment_code='" + str1 + "'"
                        check = clsDBFuncationality.getSingleValue(qry)
                        If (str1 <> check) Then
                            common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted.It is used in another process")
                            Exit Sub
                        End If
                    End If
                    If (str = 4) Then
                        Dim qry As String = "Select Segment_code  from TSPL_GL_SEGMENT_CODE  where TSPL_GL_SEGMENT_CODE.Segment_code   not in(select * from(select Account_Seg_Code4 as AA  from TSPL_GL_ACCOUNTS where ISNULL(Account_Seg_Code4,'')<>''    union all select emp_code  as AA from TSPL_EMPLOYEE_MASTER)a where ISNULL(AA,'')<>''  ) and TSPL_GL_SEGMENT_CODE .Seg_No =4 and  TSPL_GL_SEGMENT_CODE.segment_code='" + str1 + "'"
                        check = clsDBFuncationality.getSingleValue(qry)
                        If (str1 <> check) Then
                            common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted.It is used in another process")
                            Exit Sub
                        End If
                    End If
                    If (str = 5) Then
                        Dim qry As String = "Select Segment_code  from TSPL_GL_SEGMENT_CODE  where TSPL_GL_SEGMENT_CODE.Segment_code   not in(select * from(select Account_Seg_Code5 as AA  from TSPL_GL_ACCOUNTS where ISNULL(Account_Seg_Code5,'')<>'')a where ISNULL(AA,'')<>''  ) and TSPL_GL_SEGMENT_CODE .Seg_No =5 and  TSPL_GL_SEGMENT_CODE.segment_code='" + str1 + "'"
                        check = clsDBFuncationality.getSingleValue(qry)
                        If (str1 <> check) Then
                            common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted.It is used in another process")
                            Exit Sub
                        End If
                    End If
                    If (str = 6) Then
                        Dim qry As String = "Select Segment_code  from TSPL_GL_SEGMENT_CODE  where TSPL_GL_SEGMENT_CODE.Segment_code   not in(select * from(select Account_Seg_Code6 as AA  from TSPL_GL_ACCOUNTS where ISNULL(Account_Seg_Code6,'')<>''    union all select Visi_Id    as AA from TSPL_VISI_MASTER)a where ISNULL(AA,'')<>''  ) and TSPL_GL_SEGMENT_CODE .Seg_No =6 and  TSPL_GL_SEGMENT_CODE.segment_code='" + str1 + "'"
                        check = clsDBFuncationality.getSingleValue(qry)
                        If (str1 <> check) Then
                            common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted.It is used in another process")
                            Exit Sub
                        End If
                    End If
                    If (str = 7) Then
                        Dim qry As String = "Select Segment_code  from TSPL_GL_SEGMENT_CODE  where TSPL_GL_SEGMENT_CODE.Segment_code   not in(select * from(select Account_Seg_Code7 as AA  from TSPL_GL_ACCOUNTS where ISNULL(Account_Seg_Code7,'')<>''    union all select Loc_Segment_Code   as AA from TSPL_LOCATION_MASTER)a where ISNULL(AA,'')<>''  ) and TSPL_GL_SEGMENT_CODE .Seg_No =7 and  TSPL_GL_SEGMENT_CODE.segment_code='" + str1 + "'"
                        check = clsDBFuncationality.getSingleValue(qry)
                        If (str1 <> check) Then
                            common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted.It is used in another process")
                            Exit Sub
                        End If

                    End If
                End If


                connectSql.RunSp("sp_tspl_gl_segment_delete", New SqlParameter("@segno", str), New SqlParameter("@segmentname", rddrplstsegmentcode.Text), New SqlParameter("@segmentcode", check))
                rdgdvsegmentcode.Rows.Remove(rdgdvsegmentcode.CurrentRow)
                myMessages.delete()
            End If

        ElseIf rdgdvsegmentcode.Columns.Count = 3 And Not String.IsNullOrEmpty(rdgdvsegmentcode.Rows(0).Cells(2).Value) Then
            If Not IsDBNull(rdgdvsegmentcode.SelectedRows) Then
                Dim st As String = clsDBFuncationality.getSingleValue("select seg_no from tspl_gl_segment where seg_name='" + rddrplstsegmentcode.Text + "'")
                Dim str As String = "null"
                If clsCommon.myLen(st) > 0 Then
                    str = st
                End If
                Dim str1 As String = rdgdvsegmentcode.CurrentRow.Cells(0).Value
                If (str = 2) Then
                    Dim qry As String = "Select Segment_code  from TSPL_GL_SEGMENT_CODE  where TSPL_GL_SEGMENT_CODE.Segment_code   not in(select * from(select Account_Seg_Code2 as AA  from TSPL_GL_ACCOUNTS where ISNULL(Account_Seg_Code2,'')<>''    union all select Vehicle_Id  as AA from TSPL_VEHICLE_MASTER)a where ISNULL(AA,'')<>''  ) and TSPL_GL_SEGMENT_CODE .Seg_No =2 and  TSPL_GL_SEGMENT_CODE.segment_code='" + str1 + "'"
                    check = clsDBFuncationality.getSingleValue(qry)
                    If (str1 <> check) Then
                        common.clsCommon.MyMessageBoxShow("This Record Cannot be deleted.It is used in another process")
                        Exit Sub
                    End If
                End If
                If (str = 3) Then
                    Dim qry As String = " Select Segment_code  from TSPL_GL_SEGMENT_CODE  where TSPL_GL_SEGMENT_CODE.Segment_code   not in(select * from(select Account_Seg_Code3 as AA  from TSPL_GL_ACCOUNTS where ISNULL(Account_Seg_Code3,'')<>'')a where ISNULL(AA,'')<>''  ) and TSPL_GL_SEGMENT_CODE .Seg_No =3 and  TSPL_GL_SEGMENT_CODE.segment_code='" + str1 + "'"
                    check = clsDBFuncationality.getSingleValue(qry)
                    If (str1 <> check) Then
                        common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted.It is used in another process")
                        Exit Sub
                    End If
                End If
                If (str = 4) Then
                    Dim qry As String = "Select Segment_code  from TSPL_GL_SEGMENT_CODE  where TSPL_GL_SEGMENT_CODE.Segment_code   not in(select * from(select Account_Seg_Code4 as AA  from TSPL_GL_ACCOUNTS where ISNULL(Account_Seg_Code4,'')<>''    union all select emp_code  as AA from TSPL_EMPLOYEE_MASTER)a where ISNULL(AA,'')<>''  ) and TSPL_GL_SEGMENT_CODE .Seg_No =4 and  TSPL_GL_SEGMENT_CODE.segment_code='" + str1 + "'"
                    check = clsDBFuncationality.getSingleValue(qry)
                    If (str1 <> check) Then
                        common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted.It is used in another process")
                        Exit Sub
                    End If
                End If
                If (str = 5) Then
                    Dim qry As String = "Select Segment_code  from TSPL_GL_SEGMENT_CODE  where TSPL_GL_SEGMENT_CODE.Segment_code   not in(select * from(select Account_Seg_Code5 as AA  from TSPL_GL_ACCOUNTS where ISNULL(Account_Seg_Code5,'')<>'')a where ISNULL(AA,'')<>''  ) and TSPL_GL_SEGMENT_CODE .Seg_No =5 and  TSPL_GL_SEGMENT_CODE.segment_code='" + str1 + "'"
                    check = clsDBFuncationality.getSingleValue(qry)
                    If (str1 <> check) Then
                        common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted.It is used in another process")
                        Exit Sub
                    End If
                End If
                If (str = 6) Then
                    Dim qry As String = "Select Segment_code  from TSPL_GL_SEGMENT_CODE  where TSPL_GL_SEGMENT_CODE.Segment_code   not in(select * from(select Account_Seg_Code6 as AA  from TSPL_GL_ACCOUNTS where ISNULL(Account_Seg_Code6,'')<>''    union all select Visi_Id    as AA from TSPL_VISI_MASTER)a where ISNULL(AA,'')<>''  ) and TSPL_GL_SEGMENT_CODE .Seg_No =6 and  TSPL_GL_SEGMENT_CODE.segment_code='" + str1 + "'"
                    check = clsDBFuncationality.getSingleValue(qry)
                    If (str1 <> check) Then
                        common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted.It is used in another process")
                        Exit Sub
                    End If
                End If
                If (str = 7) Then
                    Dim qry As String = "Select Segment_code  from TSPL_GL_SEGMENT_CODE  where TSPL_GL_SEGMENT_CODE.Segment_code   not in(select * from(select Account_Seg_Code7 as AA  from TSPL_GL_ACCOUNTS where ISNULL(Account_Seg_Code7,'')<>''    union all select Location_Code   as AA from TSPL_LOCATION_MASTER)a where ISNULL(AA,'')<>''  ) and TSPL_GL_SEGMENT_CODE .Seg_No =7 and  TSPL_GL_SEGMENT_CODE.segment_code='" + str1 + "'"
                    check = clsDBFuncationality.getSingleValue(qry)
                    If (str1 <> check) Then
                        common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted.It is used in another process")
                        Exit Sub
                    End If

                End If
                connectSql.RunSp("sp_tspl_gl_segment_delete", New SqlParameter("@segno", str), New SqlParameter("@segmentname", rddrplstsegmentcode.Text), New SqlParameter("@segmentcode", check))
                rdgdvsegmentcode.Rows.Remove(rdgdvsegmentcode.CurrentRow)
                myMessages.delete()

            End If


        End If

    End Sub
    Private Sub rdmenuimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuimport.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        If transportSql.importExcel(dgv, "Segment No", "Segment Name", "Segment Code", "Description", "Account Code", "GIT", "State Code", "Email Department") Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each dgrv As GridViewRowInfo In dgv.Rows

                    Dim strsegmentno As String = clsCommon.myCstr(dgrv.Cells(0).Value)
                    Dim strsegmentname As String = clsCommon.myCstr(dgrv.Cells(1).Value)
                    Dim strsegmentcode As String = clsCommon.myCstr(dgrv.Cells(2).Value)
                    Dim strdescription As String = clsCommon.myCstr(dgrv.Cells(3).Value)
                    Dim straccountcode As String = clsCommon.myCstr(dgrv.Cells(4).Value)
                    Dim strGIT As String = clsCommon.myCstr(dgrv.Cells(5).Value)
                    Dim strStateCode As String = clsCommon.myCstr(dgrv.Cells("State Code").Value)
                    Dim strEmailDepartment As String = clsCommon.myCstr(dgrv.Cells("Email Department").Value)
                    If String.IsNullOrEmpty(strsegmentno) Or clsCommon.myCstr(strsegmentno) > 10 Then
                        Throw New Exception("Segment Name has some incorrect values")
                    End If

                    If clsCommon.myLen(strGIT) > 0 Then
                        If clsCommon.CompairString(strGIT.ToUpper().Trim(), "Y") = CompairStringResult.Equal Or clsCommon.CompairString(strGIT.ToUpper().Trim(), "N") = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("GIT should be between 'Y' or 'N'.")
                        End If
                    End If
                    If clsCommon.CompairString(strsegmentno, "7") = CompairStringResult.Equal Then
                        If String.IsNullOrEmpty(strStateCode) = True Then
                            Throw New Exception("State Code Can't be blank for Segment Code : " + strsegmentcode + "")
                        End If
                        Dim isValidStateCode As Boolean = clsDBFuncationality.getSingleValue("select count (*) from tspl_state_Master where state_Code = '" + strStateCode + "'", trans)
                        If isValidStateCode = False Then
                            Throw New Exception("Invalid State Code For Segment Code : " + strsegmentcode + "")
                        End If
                    Else
                        strStateCode = ""
                    End If

                    Dim sql1 As String = "select count(*)from tspl_gl_segment_code where seg_no='" + strsegmentno + "'and segment_name='" + strsegmentname + "'and segment_code='" + strsegmentcode + "'"
                    Dim i As Integer = CInt(clsDBFuncationality.getSingleValue(sql1, trans))
                    If (i = 0) Then
                        connectSql.RunSpTransaction(trans, "sp_tspl_gl_segmentcode_insert", New SqlParameter("@segno", strsegmentno), New SqlParameter("@segmentname", strsegmentname), New SqlParameter("@segmentcode", strsegmentcode), New SqlParameter("@desc", strdescription), New SqlParameter("@acccode", straccountcode), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                        '' Anubhooti 15-Sep-2014 BM00000003782
                        'clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_GL_SEGMENT_CODE SET GIT='" & strGIT & "', STATE_CODE =  " + IIf(clsCommon.myLen(strStateCode) > 0, "'" + strStateCode + "'", "null") + " WHERE Seg_No=" & strsegmentno & " and Segment_name ='" & strsegmentname & "' and Segment_code ='" & strsegmentcode & "'", trans)
                    Else
                        connectSql.RunSpTransaction(trans, "sp_tspl_gl_segment_update", New SqlParameter("@segno", strsegmentno), New SqlParameter("@segmentname", strsegmentname), New SqlParameter("@segmentcode", strsegmentcode), New SqlParameter("@desc", strdescription), New SqlParameter("@acccode", straccountcode))
                        '' Anubhooti 15-Sep-2014 BM00000003782
                        'clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_GL_SEGMENT_CODE SET GIT='" & strGIT & "' , STATE_CODE = '" + strStateCode + "' WHERE Seg_No=" & strsegmentno & " and Segment_name ='" & strsegmentname & "' and Segment_code ='" & strsegmentcode & "'", trans)

                    End If
                    clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_GL_SEGMENT_CODE SET GIT='" & strGIT & "', STATE_CODE =  " + IIf(clsCommon.myLen(strStateCode) > 0, "'" + strStateCode + "'", "null") + " WHERE Seg_No=" & strsegmentno & " and Segment_name ='" & strsegmentname & "' and Segment_code ='" & strsegmentcode & "'", trans)
                    clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_GL_SEGMENT_CODE SET Email_Department =  " + IIf(clsCommon.myLen(strEmailDepartment) > 0, "'" + strEmailDepartment + "'", "null") + " WHERE Seg_No=" & strsegmentno & " and Segment_name ='" & strsegmentname & "' and Segment_code ='" & strsegmentcode & "'", trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(dgv)
    End Sub

    Private Sub rdmenuexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuexport.Click

    End Sub

    Private Sub rdmenuexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuexit.Click
        Me.Close()
    End Sub
    Private Sub rdgdvsegmentcode_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs)
        Dim dt As GridViewTextBoxColumn = TryCast(rdgdvsegmentcode.Columns(0), GridViewTextBoxColumn)
        dt.ColumnCharacterCasing = CharacterCasing.Upper
    End Sub

    Private Sub rdgdvsegmentcode_CellValidating(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs)
        Dim length As Integer = connectSql.RunScalar("Select Seg_length from tspl_gl_segment where Seg_name ='" + rddrplstsegmentcode.Text + "'")
        If e.ColumnIndex = 0 Then
            If Not String.IsNullOrEmpty(e.Value) Then
                Dim str As String = e.Value.ToString()
                If str.Length <> length Then
                    common.clsCommon.MyMessageBoxShow(Me, "Segment Code Length should be " + Convert.ToString(length) + "")
                    e.Cancel = True
                End If
            End If
        End If

    End Sub
    Private Sub rddrplstsegmentcode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles rddrplstsegmentcode.SelectedIndexChanged
        FillData()
        PageSetupReport_ID = MyBase.Form_ID + rddrplstsegmentcode.Text
        ReStoreGridLayout()
    End Sub

    Private Sub FillData()
        Try
            rdgdvsegmentcode.Enabled = True
            rdbtnsave.Enabled = True
            rdbtndelete.Enabled = True
            Dim st As String
            st = clsDBFuncationality.getSingleValue("Select seg_useinclosing from tspl_GL_Segment where Seg_name ='" + rddrplstsegmentcode.Text + "'")
            Dim str As String = ""
            If clsCommon.myLen(st) > 0 Then
                str = st.Trim()
            End If
            If str = "Y" Then
                funacc_insert()
                rdgdvsegmentcode.Columns("STATE_CODE").IsVisible = True
                rdgdvsegmentcode.Columns("STATE_NAME").IsVisible = True
            Else
                rdgdvsegmentcode.Columns(2).IsVisible = False
                rdgdvsegmentcode.Columns("STATE_CODE").IsVisible = False
                rdgdvsegmentcode.Columns("STATE_NAME").IsVisible = False

                funinsert()

            End If

            Dim LocSeg As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(seg_name,'') As SegName from tspl_gl_segment where seg_no = '3'"))
            If clsCommon.CompairString(rddrplstsegmentcode.Text, LocSeg) = CompairStringResult.Equal Then
                rdgdvsegmentcode.Columns("Email_Department").IsVisible = True
            Else
                rdgdvsegmentcode.Columns("Email_Department").IsVisible = False
            End If
            Dim str1 As Integer
            str1 = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Seg_length from tspl_GL_segment where Seg_name ='" + rddrplstsegmentcode.Text + "'"))
            Dim int As Integer
            If clsCommon.myLen(str1) > 0 Then
                int = Convert.ToInt32(str1)
            End If

            Dim lngth As GridViewTextBoxColumn = TryCast(Me.rdgdvsegmentcode.Columns(0), GridViewTextBoxColumn)
            lngth.MaxLength = Convert.ToInt32(int)

        Catch ex As Exception
            myExceptions(ex)

        End Try
    End Sub

    Private Sub rdgdvsegmentcode_CellBeginEdit(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellCancelEventArgs)
        If e.ColumnIndex = 1 Then
            If String.IsNullOrEmpty(rdgdvsegmentcode.CurrentRow.Cells(0).Value) Then
                e.Cancel = True
            Else
                e.Cancel = False
            End If
        End If
    End Sub
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'if funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "SEG-CODE-M"
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
    '            'rdbtnsave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            'rdbtndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Sub AllCOl_Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllCOl_Export.Click
        Dim query As String = "Select seg_no as 'Segment No',segment_name as 'Segment Name',segment_code as 'Segment Code',Description as 'Description',Account_code as 'Account Code',GIT, STATE_CODE as [State Code],Email_Department as [Email Department] from tspl_gl_segment_code"
        ListImpExpColumnsMandatory = New List(Of String)({"Segment No", "Segment Code", "State Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Segment No", "Segment Code"})
        transportSql.ExporttoExcel(query, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub SetCol_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetCol_export.Click
        Dim frmset_Segment As New FrmExport_SegmentCode()
        frmset_Segment.Show()
    End Sub

    Private Sub SplitContainer1_Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub

    Private Sub rdgdvsegmentcode_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles rdgdvsegmentcode.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Do you want to delete current row?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Private Sub rdgdvsegmentcode_CellValueChanged_1(sender As Object, e As GridViewCellEventArgs) Handles rdgdvsegmentcode.CellValueChanged
        Try
            If rdgdvsegmentcode.CurrentRow.Index >= 0 Then
                If (Not isInsideLoadData) Then
                    If Not isCellValueChangedOpen Then
                        isCellValueChangedOpen = True
                        OpenUOMList(False)
                     isCellValueChangedOpen = False
                    End If
                End If
            End If

            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.ColumnIndex = 4 Then
                        OpenStates()
                    End If
                    isCellValueChangedOpen = False
                End If
            End If

           

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
            isCellValueChangedOpen = False
        End Try
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strCode As String = clsCommon.myCstr(rdgdvsegmentcode.CurrentRow.Cells("ACCOUNTCODE").Value)
        If clsCommon.myLen(strCode) > 0 Then
            Dim strWhrclas As String = "Account_Seg_Code7 = '" + clsCommon.myCstr(rdgdvsegmentcode.CurrentRow.Cells("CODE").Value) + "'" + _
            " And TSPL_ACCOUNT_MAIN_GROUPS.Group_Type = 'Retained Earnings'"
            rdgdvsegmentcode.CurrentRow.Cells("ACCOUNTCODE").Value = clsGLAccount.getFinder(strWhrclas, strCode, isButtonClick)
        End If
    End Sub

    Private Sub OpenStates()
        Try
            Dim qry As String = "select STATE_CODE , STATE_NAME , COUNTRY_CODE  from TSPL_STATE_MASTER"

            Dim strCode As String = clsCommon.myCstr(rdgdvsegmentcode.CurrentRow.Cells("STATE_CODE").Value)
            If clsCommon.myLen(strCode) > 0 Then
                rdgdvsegmentcode.CurrentRow.Cells("STATE_CODE").Value = clsCommon.ShowSelectForm("rpSegCdState", qry, "STATE_CODE", "", "STATE_NAME", "STATE_CODE", True)
                If (rdgdvsegmentcode.CurrentRow.Cells("STATE_CODE").Value) IsNot Nothing Then
                    rdgdvsegmentcode.CurrentRow.Cells("STATE_NAME").Value = clsDBFuncationality.getSingleValue("select STATE_NAME from TSPL_STATE_MASTER WHERE STATE_CODE = '" + rdgdvsegmentcode.CurrentRow.Cells("STATE_CODE").Value + "'")
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdgdvsegmentcode_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles rdgdvsegmentcode.CellFormatting
        Try
            If e.Column Is rdgdvsegmentcode.Columns("ACCOUNTCODE") Then
                If clsCommon.myLen(rdgdvsegmentcode.CurrentRow.Cells("ACCOUNTCODECHECK").Value) > 0 Then
                    rdgdvsegmentcode.CurrentRow.Cells("ACCOUNTCODE").ReadOnly = True
                Else
                    rdgdvsegmentcode.CurrentRow.Cells("ACCOUNTCODE").ReadOnly = False
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub rmiSaveLayout_Click(sender As Object, e As EventArgs) Handles rmiSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            rdgdvsegmentcode.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            rdgdvsegmentcode.SaveLayout(obj.GridLayout)
            obj.GridColumns = rdgdvsegmentcode.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If

            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmiDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmiDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= rdgdvsegmentcode.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To rdgdvsegmentcode.Columns.Count - 1 Step ii + 1
                        rdgdvsegmentcode.Columns(ii).IsVisible = False
                        rdgdvsegmentcode.Columns(ii).VisibleInColumnChooser = True
                    Next
                    rdgdvsegmentcode.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
End Class

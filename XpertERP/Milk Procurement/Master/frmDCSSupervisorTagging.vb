Imports common
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class frmDCSSupervisorTagging
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim arrLoc As String = Nothing
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    Dim IsInsieLoadData As Boolean
    Const colSNO As String = "SNo"
    Dim Prev As Integer = 0
#End Region

    Private Sub frmDCSSupervisorTagging_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S For Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        Reset()
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        If Not (MyBase.isReadFlag) Then
            btnexport.Visibility = ElementVisibility.Collapsed
        End If
        If Not (MyBase.isModifyFlag) Then
            btnimport.Visibility = ElementVisibility.Collapsed
        End If
    End Sub

    Sub Reset()
        txtmcccode.Value = ""
        txtmccname.Text = ""
        IsInsieLoadData = False
        gvVLC.Rows.Clear()
        gvVLC.Columns.Clear()
        btnsave.Text = "Save"
        btndelete.Enabled = False
        isNewEntry = True
        LoadBlankVLC_Grid()
        MCCLOCATIONFINDER()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub



    Function AllowToSave() As Boolean
        Try

            If clsCommon.myLen(txtmcccode.Value) <= 0 Then
                txtmcccode.Focus()
                txtmcccode.Select()
                Errorcontrol.SetError(txtmccname, "Please Select MCC Detail")
                Throw New Exception("Please Select MCC Detail")
            Else
                Errorcontrol.ResetError(txtmccname)
            End If


            'For Each Row As GridViewRowInfo In gvVLC.Rows
            '    If clsCommon.CompairString(Row.Cells("colSupervisor_Code").Value, "") = CompairStringResult.Equal Then
            '        Errorcontrol.SetError(gvVLC, "Please Fill Supervisor")
            '        Throw New Exception("Please Fill Supervisor")
            '    End If
            'Next

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsDCSSupervisor()
                obj.MCC_Code = clsCommon.myCstr(txtmcccode.Value)

                Dim objgenset As New clsDCSSupervisorTagging
                obj.arr = New List(Of clsDCSSupervisorTagging)

                ' Dim arrSNO As New List(Of Integer)
                For irow As Integer = 0 To gvVLC.Rows.Count - 1

                    objgenset = New clsDCSSupervisorTagging
                    objgenset.MCC_Code = clsCommon.myCstr(txtmcccode.Value)
                    objgenset.VLC_CODE = clsCommon.myCstr(gvVLC.Rows(irow).Cells("COLVLC_Code").Value)
                    objgenset.SNo = clsCommon.myCdbl(gvVLC.Rows(irow).Cells(colSNO).Value)
                    objgenset.Supervisor_Code = clsCommon.myCstr(gvVLC.Rows(irow).Cells("colSupervisor_Code").Value)

                    If clsCommon.myLen(objgenset.Supervisor_Code) > 0 Then
                        obj.arr.Add(objgenset)
                    End If

                Next

                'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

                If clsDCSSupervisor.SaveData(txtmcccode.Value, obj) Then
                    If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                    End If
                    LoadData(obj.MCC_Code)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If clsCommon.myLen(txtmcccode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select MCC For Deletion", Me.Text)
            txtmcccode.Focus()
            txtmcccode.Select()
            Errorcontrol.SetError(txtmcccode, "Please Select MCC Code For Deletion")
            Return
        Else
            Errorcontrol.ResetError(txtmcccode)
        End If

        Dim qry As String = "Select count(*) from TSPL_VLC_Supervisor_Tagging where MCC_code='" + txtmcccode.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Data Found For Deletion", Me.Text)
            Return
        End If

        If Not clsCommon.MyMessageBoxShow("Are You Sure,Want To Delete The Supervisor Of MCC " + txtmcccode.Value + "?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            Return
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            qry = "delete from TSPL_VLC_Supervisor_Tagging where mcc_code='" + txtmcccode.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
            trans.Commit()
            Reset()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Sub LoadData(ByVal strCode As String)
        Try
            'No_of_Vlc = 0
            IsInsieLoadData = True
            Dim obj As clsDCSSupervisor = clsDCSSupervisor.GetData(strCode)
            isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.MCC_Code) > 0 Then
                isNewEntry = False
                txtmcccode.Value = obj.MCC_Code
                txtmccname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_code='" + txtmcccode.Value + "'"))
                btnsave.Text = "Update"
                btndelete.Enabled = True
                LoadBlankVLC_Grid()
                If obj.arr.Count > 0 Then
                    For i As Integer = 0 To obj.arr.Count - 1
                        gvVLC.Rows.Add(obj.arr.Item(i).SNo, obj.arr.Item(i).VLC_Uploader_code, obj.arr.Item(i).VLC_CODE, obj.arr.Item(i).VLC_NAME, obj.arr.Item(i).Supervisor_Code, obj.arr.Item(i).Supervisor_Name)
                        'No_of_Vlc += 1
                    Next
                End If
            Else
                Reset()
            End If
            IsInsieLoadData = False
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankVLC_Grid()
        Try
            gvVLC.Rows.Clear()
            gvVLC.Columns.Clear()

            Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoLineNo.HeaderText = "SNo"
            repoLineNo.Name = colSNO
            repoLineNo.Width = 50
            repoLineNo.ShowUpDownButtons = False
            repoLineNo.Step = 0
            repoLineNo.Minimum = 0
            repoLineNo.FormatString = "{0:n0}"
            repoLineNo.DecimalPlaces = 0
            repoLineNo.ReadOnly = True
            repoLineNo.IsVisible = True
            repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gvVLC.MasterTemplate.Columns.Add(repoLineNo)
            gvVLC.Columns.Add("COLUploaderCode", "Uploader Code")
            gvVLC.Columns.Add("COLVLC_Code", "VLC Code")
            gvVLC.Columns.Add("COLVLC_NAME", "VLC Name")
            gvVLC.Columns.Add("colSupervisor_Code", "Supervisor Code")
            gvVLC.Columns.Add("colSupervisor_Name", "Supervisor Name")



            gvVLC.Columns("COLUploaderCode").Width = 100
            gvVLC.Columns("COLVLC_Code").Width = 150
            gvVLC.Columns("COLVLC_NAME").Width = 150
            gvVLC.Columns("colSupervisor_Code").Width = 150
            gvVLC.Columns("colSupervisor_Name").Width = 150

            gvVLC.Columns("COLUploaderCode").ReadOnly = True
            gvVLC.Columns("COLVLC_Code").ReadOnly = True
            gvVLC.Columns("COLVLC_NAME").ReadOnly = True
            gvVLC.Columns("colSupervisor_Code").ReadOnly = False
            gvVLC.Columns("colSupervisor_Name").ReadOnly = True

            gvVLC.AllowAddNewRow = False
            gvVLC.MasterTemplate.AddNewRowPosition = SystemRowPosition.Bottom
            gvVLC.AllowEditRow = True
            gvVLC.AllowDeleteRow = True
            gvVLC.AllowRowResize = False
            gvVLC.EnableSorting = False
            gvVLC.AllowRowReorder = False
            gvVLC.AllowColumnResize = True
            gvVLC.AllowColumnChooser = False
            gvVLC.AllowAutoSizeColumns = True
            gvVLC.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadDataForMapping()
        Try
            Dim qry As String = "select ROW_NUMBER() OVER(PARTITION BY 1 ORDER BY TSPL_VLC_MASTER_HEAD.VLC_CODE) AS Sno
                ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_UPLOADER,TSPL_VLC_MASTER_HEAD.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_NAME
                from TSPL_VLC_MASTER_HEAD where mcc='" + txtmcccode.Value + "' order by TSPL_VLC_MASTER_HEAD.VLC_CODE"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gvVLC.Rows.AddNew()
                    gvVLC.Rows(gvVLC.Rows.Count - 1).Cells(colSNO).Value = clsCommon.myCstr(dr("Sno"))
                    gvVLC.Rows(gvVLC.Rows.Count - 1).Cells("COLUploaderCode").Value = clsCommon.myCstr(dr("VLC_CODE_VLC_UPLOADER"))
                    gvVLC.Rows(gvVLC.Rows.Count - 1).Cells("COLVLC_Code").Value = clsCommon.myCstr(dr("VLC_Code"))
                    gvVLC.Rows(gvVLC.Rows.Count - 1).Cells("COLVLC_NAME").Value = clsCommon.myCstr(dr("VLC_NAME"))
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub txtmcccode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtmcccode._MYValidating

        'Dim qry As String = "select tspl_location_master.location_category from tspl_user_master left outer join tspl_location_master on tspl_location_master.location_code=tspl_user_master.default_location where tspl_user_master.user_code='" + objCommonVar.CurrentUserCode + "'"
        'Dim value As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        'If clsCommon.CompairString(value, "MCC") = CompairStringResult.Equal Then
        '    txtmcccode.Value = clsMccMaster.getFinder(" created_by='" + objCommonVar.CurrentUserCode + "'", txtmcccode.Value, isButtonClicked)
        'Else
        Dim whrCls As String = ""
        'If clsCommon.myLen(arrLoc) > 0 Then
        '    whrCls = " TSPL_MCC_MASTER.mcc_code in (" + arrLoc + ")"
        'End If

        txtmcccode.Value = clsMccMaster.getFinder(whrCls, txtmcccode.Value, isButtonClicked)
        'End If

        If clsCommon.myLen(txtmcccode.Value) > 0 Then
            txtmccname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_code='" + txtmcccode.Value + "'"))
            LoadBlankVLC_Grid()

            Dim qry As String = "select count(1) as tt 
                from TSPL_VLC_Supervisor_Tagging                 
                where TSPL_VLC_Supervisor_Tagging.mcc_Code='" & txtmcccode.Value & "'"

            Dim dblCount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If (dblCount > 0) Then
                LoadData(txtmcccode.Value)
            Else
                LoadDataForMapping()
            End If
        Else
                txtmccname.Text = ""
        End If
    End Sub



    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        'Dim qry As String = "select count(*) from tspl_mcc_route_master"
        'Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        'If check > 0 Then
        '    qry = "select TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as [Route Name],TSPL_MCC_ROUTE_MASTER.vehicle_code as [Vehicle Code],TSPL_MCC_ROUTE_MASTER.mcc_code as [MCC Code],tspl_mcc_master.mcc_name as [MCC Name],TSPL_MCC_ROUTE_MASTER.KiloMeter,TSPL_MCC_ROUTE_MASTER.supervisor_name as [Supervisor Code],TSPL_MCC_ROUTE_MASTER.contact_no as [Contact No],TSPL_MCC_ROUTE_MASTER.email_id as [Email ID],TSPL_MCC_ROUTE_MASTER.effective_date as [Effective Date] from TSPL_MCC_ROUTE_MASTER left outer join tspl_mcc_master on Tspl_mcc_master.mcc_code=TSPL_MCC_ROUTE_MASTER.mcc_code left outer join tspl_country_master on TSPL_MCC_ROUTE_MASTER.country_code=tspl_country_master.country_code left outer join tspl_state_master on tspl_state_master.state_code=TSPL_MCC_ROUTE_MASTER.state_code left outer join tspl_city_master on tspl_city_master.city_code=TSPL_MCC_ROUTE_MASTER.city_code"
        '    ''Removed
        '    ''''',TSPL_MCC_ROUTE_MASTER.add1 as Address1,TSPL_MCC_ROUTE_MASTER.add2 as Address2,TSPL_MCC_ROUTE_MASTER.add3 as Address3
        '    ''''',TSPL_MCC_ROUTE_MASTER.country_code as [Country Code],tspl_country_master.country_name as [Country Name],TSPL_MCC_ROUTE_MASTER.state_code as [State Code],tspl_state_master.state_name as [State Name],TSPL_MCC_ROUTE_MASTER.city_code as [City Code],tspl_city_master.city_name as [City Name],
        'Else
        '    qry = "select '' as Code,'' as [Route Name],'' as [Vehicle Code],'' as [MCC Code],'' as [MCC Name],'' as KiloMeter,'' as [Supervisor Code],'' as [Contact No],'' as [Email ID],'' as [Effective Date]"
        '    '''' Removed
        '    '''''''' as Address1,'' as Address2,'' as Address3,
        '    '''''''' as [Country Code],'' as [Country Name],'' as [State Code],'' as [State Name],'' as [City Code],'' as [City Name],
        'End If

        'transportSql.ExporttoExcel(qry, "", "", Me, "MCC_Code", "VLC_CODE")

        Try
            Dim qry As String
            qry = "select TSPL_VLC_MASTER_HEAD.MCC as Mcc_code,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_Supervisor_Tagging.Supervisor_code 
                    from TSPL_VLC_MASTER_HEAD 
                    left join TSPL_VLC_Supervisor_Tagging on TSPL_VLC_MASTER_HEAD.vlc_code=TSPL_VLC_Supervisor_Tagging.vlc_code
                    left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.emp_code=TSPL_VLC_Supervisor_Tagging.Supervisor_code"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub


    Private Sub frmDCSSupervisorTagging_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    '------------BM00000003414-------------------
    Private Sub MCCLOCATIONFINDER()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(True)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                arrLoc = obj.arrLocCodes
            Else
                'fndMCCCode.Enabled = False
                'Throw New Exception("Please Set Default Location Of LogIn User")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub gvVLC_CellBeginEdit(sender As Object, e As GridViewCellCancelEventArgs) Handles gvVLC.CellBeginEdit
        Try
            If gvVLC.CurrentRow.Cells(colSNO).Value IsNot Nothing Then
                Prev = gvVLC.CurrentRow.Cells(colSNO).Value
            End If
            If gvVLC.CurrentRow.Cells(colSNO).Value Is Nothing Then
                Dim lastIndex = gvVLC.Rows.Count - 1
                Prev = lastIndex + 1
                If lastIndex >= 0 Then
                    gvVLC.Rows(lastIndex).Cells(colSNO).Value = Prev
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub gvVLC_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvVLC.CellValueChanged
        Try
            If Not IsInsieLoadData Then
                IsInsieLoadData = True
                If e.Column Is gvVLC.Columns("colSupervisor_Code") Then
                    Dim sQuery As String = "select EMP_CODE as [Code],Emp_Name as [Employee Name] from tspl_employee_master"
                    Dim whrcls As String = " emp_status='Active' "

                    Dim str As String = clsCommon.ShowSelectForm("DCS@Sup", sQuery, "Code", whrcls, gvVLC.CurrentRow.Cells(e.Column.Name).Value, "", False)
                    If str <> "" Then
                        gvVLC.CurrentRow.Cells("colSupervisor_Code").Value = str
                        gvVLC.CurrentRow.Cells("colSupervisor_Name").Value = clsEmployeeMaster.GetName(str, Nothing)
                    End If
                End If
                IsInsieLoadData = False
            End If
        Catch ex As Exception
            IsInsieLoadData = False
        End Try
    End Sub

    Private Sub btnimport_Click(sender As Object, e As EventArgs) Handles btnimport.Click
        Dim gvCharges As New RadGridView()
        Me.Controls.Add(gvCharges)
        Dim countDefaultUOM As Integer = 0
        Dim boolresult As Boolean = False

        boolresult = transportSql.importExcel(gvCharges, "Mcc_code", "VLC_Code", "Supervisor_code")

        If boolresult Then
            Dim isSaved As Boolean = True
            Dim currentdate As Date = Date.Today
            'Dim trans As SqlTransaction = Nothing
            clsCommon.ProgressBarShow()
            Dim arrMccCode As New List(Of String)
            Dim arrVlcCode As New List(Of String)
            Try
                'trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gvCharges.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    'Dim coll As New Hashtable()
                    Dim MCC_Code As String = clsCommon.myCstr(grow.Cells("MCC_Code").Value)
                    Dim MCCCode As String = clsCommon.myCstr(grow.Cells("MCC_Code").Value)
                    If clsCommon.myLen(MCCCode) >= 0 Then
                        MCC_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MCC_CODE from TSPL_MCC_MASTER Where MCC_CODE ='" + MCCCode + "'"))
                        If clsCommon.myLen(MCC_Code) <= 0 Then
                            Throw New Exception("MCC Code '" + MCCCode + "' does not exist .Please make its master first at line no '" + LineNo + "'")
                        End If
                    Else
                        Throw New Exception("Please insert MCC code at line no '" + LineNo + "' ")
                    End If
                    If Not arrMccCode.Contains(MCCCode) Then
                        arrMccCode.Add(MCCCode)
                    End If



                    Dim strVLCCode As String = clsCommon.myCstr(grow.Cells("VLC_CODE").Value)
                    Dim VLCCode As String = clsCommon.myCstr(grow.Cells("VLC_CODE").Value)
                    Dim VLCMCCMap As String = ""
                    If clsCommon.myLen(VLCCode) > 0 Then
                        strVLCCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select VLC_Code from TSPL_VLC_MASTER_HEAD Where VLC_Code ='" + VLCCode + "'"))
                        If clsCommon.myLen(strVLCCode) <= 0 Then
                            Throw New Exception("VLC Code '" + VLCCode + "' does not exist .Please make its master first at line no '" + LineNo + "'")
                        End If

                        VLCMCCMap = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select VLC_Code from TSPL_VLC_MASTER_HEAD Where VLC_Code ='" + VLCCode + "' and mcc ='" + MCCCode + "'"))
                        If clsCommon.myLen(VLCMCCMap) <= 0 Then
                            Throw New Exception("VLC Code '" + VLCCode + "' not mapped with MCC Code'" + MCCCode + "' at line no '" + LineNo + "'")
                        End If
                    Else
                        Throw New Exception("Please fill VLC code at line no '" + LineNo + "' ")
                    End If
                    If Not arrVlcCode.Contains(VLCCode) Then
                        arrVlcCode.Add(VLCCode)
                    Else
                        Throw New Exception("Repeted VLC code at line no '" + LineNo + "' ")
                    End If

                    Dim SupervisorCode As String = clsCommon.myCstr(grow.Cells("Supervisor_Code").Value)
                    Dim Supervisor_Code As String = clsCommon.myCstr(grow.Cells("Supervisor_Code").Value)
                    If clsCommon.myLen(SupervisorCode) > 0 Then
                        Supervisor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select EMP_Code from TSPL_EMPLOYEE_MASTER Where EMP_Code ='" + SupervisorCode + "'"))
                        If clsCommon.myLen(Supervisor_Code) <= 0 Then
                            Throw New Exception("Supervisor Code '" + SupervisorCode + "' does not exist .Please make its master first at line no '" + LineNo + "'")
                        End If

                        'Else
                        '    Throw New Exception("Please fill Supervisor code at line no '" + LineNo + "' ")
                    End If

                Next

                If arrMccCode.Count > 1 Then
                    Throw New Exception("Please Insert one MCC data at a time.")
                End If

                Dim obj As New clsDCSSupervisor()
                obj.MCC_Code = clsCommon.myCstr(arrMccCode.Item(0))

                Dim objgenset As New clsDCSSupervisorTagging
                obj.arr = New List(Of clsDCSSupervisorTagging)

                For irow As Integer = 0 To gvCharges.Rows.Count - 1
                    objgenset = New clsDCSSupervisorTagging
                    objgenset.MCC_Code = clsCommon.myCstr(gvCharges.Rows(irow).Cells("MCC_Code").Value)
                    objgenset.VLC_CODE = clsCommon.myCstr(gvCharges.Rows(irow).Cells("VLC_Code").Value)
                    objgenset.SNo = clsCommon.myCdbl(irow + 1)
                    objgenset.Supervisor_Code = clsCommon.myCstr(gvCharges.Rows(irow).Cells("Supervisor_Code").Value)

                    If clsCommon.myLen(objgenset.Supervisor_Code) > 0 Then
                        obj.arr.Add(objgenset)
                    End If

                Next


                If clsDCSSupervisor.SaveData(obj.MCC_Code, obj) Then

                    clsCommon.ProgressBarHide()
                    'trans.Commit()
                    RadMessageBox.Show(Me, "Data Imported Successfully.", Me.Text)
                    txtmcccode.Value = obj.MCC_Code
                    LoadData(txtmcccode.Value)
                Else
                    Throw New Exception("Error in Import")
                End If
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                'trans.Rollback()
                RadMessageBox.Show(Me, ex.Message, Me.Text)
            Finally
                Me.Controls.Remove(gvCharges)
                'arrRoute = Nothing
            End Try
        End If

    End Sub
End Class

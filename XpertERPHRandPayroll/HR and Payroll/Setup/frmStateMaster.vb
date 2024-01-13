'--11/10/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmStateMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim AllowGSTApplicable As Boolean = False

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Public Sub Save()
        If AllowToSave() Then
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmStateMaster1, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            Dim obj As New clsStateMaster()
            obj.Code = txtCode.Value
            obj.Name = txtName.Text
            ''preeti gupta Ticket no[4259]---
            obj.IsWayBillRequired = IIf(ChkIsWayBillReqd.Checked, True, False)
            '-----
            obj.COUNTRY_CODE = txtCountry.Value
            obj.IsDefault = IIf(chkIsDefault.Checked = True, 1, 0)
            If AllowGSTApplicable = True Then
                obj.GST_UT = IIf(ChkGSTUT.Checked, True, False)
                obj.GSTStateCode = txtGSTStateCode.Text
            End If
            
            Dim Arr As New List(Of clsStateMaster)
            Dim ii As Integer = 0
            For ii = 0 To GV.Rows.Count - 1
                Dim objTr As New clsStateMaster()
                Dim status As Boolean = False

                status = clsCommon.myCBool(GV.Rows(ii).Cells("Select").Value)

                If status = True Then
                    objTr.regioncode = clsCommon.myCstr(GV.Rows(ii).Cells("code").Value)
                    objTr.regionname = clsCommon.myCstr(GV.Rows(ii).Cells("region name").Value)

                    If clsCommon.myLen(objTr.regioncode) > 0 Then
                        Arr.Add(objTr)
                    End If
                End If
            Next

            If (obj.SaveData(obj, isNewEntry, Arr)) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadData(obj.Code, NavigatorType.Current)

                btnSave.Text = "Update"
                btnDelete.Enabled = True
            Else
                btnSave.Text = "Save"
                btnDelete.Enabled = False
                'Else
                '    common.clsCommon.MyMessageBoxShow("This '" & obj.Code & "' already exist ")
            End If

        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsStateMaster()
        obj = clsStateMaster.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            funReset()
            isNewEntry = False
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            txtCode.Value = obj.Code
            txtName.Text = obj.Name
            txtCountry.Value = obj.COUNTRY_CODE
            lblCountry.Text = obj.COUNTRY_NAME
            ChkIsWayBillReqd.Checked = obj.IsWayBillRequired
            chkIsDefault.Checked = IIf(obj.IsDefault = 1, True, False)
            If AllowGSTApplicable = True Then
                ChkGSTUT.Checked = obj.GST_UT
                txtGSTStateCode.Text = obj.GSTStateCode
            End If
            
            '-----------------------------------do work for region-------------------------
            LoadBlankGrid()
            Dim Arr As List(Of clsStateMaster) = clsStateMaster.GetDataALL(txtCode.Value)
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each objTr As clsStateMaster In Arr
                    GV.Rows.AddNew()

                    GV.Rows(GV.Rows.Count - 1).Cells("code").Value = objTr.regioncode
                    GV.Rows(GV.Rows.Count - 1).Cells("region name").Value = objTr.regionname
                    GV.Rows(GV.Rows.Count - 1).Cells("select").Value = clsCommon.myCBool(IIf(objTr.status = "Y", True, False))
                Next

            End If
            '---------------------------------------------------------------------

        End If

        If GV.Rows.Count = 0 Then
            LoadGrid()
        End If

    End Sub

    Sub LoadBlankGrid()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Region Name", GetType(String))
        dt.Columns.Add("Select", GetType(Boolean))

        GV.DataSource = Nothing
        GV.Rows.Clear()
        GV.Columns.Clear()

        GV.DataSource = dt

        GV.Columns("Code").Width = 150
        GV.Columns("Code").ReadOnly = True

        GV.Columns("Region Name").Width = 250
        GV.Columns("Region Name").ReadOnly = True

        GV.Columns("Select").ReadOnly = False
    End Sub

    Function AllowToSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue("Code")
            txtCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtName.Text) <= 0 Then
            myMessages.blankValue("State Name")
            txtName.Focus()
            Return False
        ElseIf clsCommon.myLen(txtCountry.Value) <= 0 Then
            myMessages.blankValue("Country Name")
            txtCountry.Focus()
            Return False
        End If
        If AllowGSTApplicable = True Then
            If ChkGSTUT.Checked = True Then
                If clsCommon.myLen(txtGSTStateCode.Text) <= 0 Then
                    myMessages.blankValue("Please fill GST State Code.")
                    txtGSTStateCode.Focus()
                    Return False
                End If
               
            End If
            Dim qry As String = ""
            If clsCommon.myCdbl(txtGSTStateCode.Text) > 0 Then
                qry = clsDBFuncationality.getSingleValue("select State_Code from tspl_state_master where GST_State_Code='" & txtGSTStateCode.Text & "' and State_Code not in ('" & txtCode.Value & "')")
            End If

            If clsCommon.myLen(qry) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "GST State Code already Used. ", Me.Text)
                txtGSTStateCode.Focus()
                Return False
            End If
        End If
        
        Return True
    End Function


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        Dim discCode As String
        discCode = clsDBFuncationality.getSingleValue("select STATE_CODE  from tspl_city_master  where STATE_CODE ='" & txtCode.Value & "'", Me.Text)
        If clsCommon.myLen(discCode) > 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "This record can't be deleted.It is used in another process,Me.text")
            Exit Sub
        End If
        discCode = clsDBFuncationality.getSingleValue("select STATE_CODE  from tspl_village_master  where STATE_CODE ='" & txtCode.Value & "'", Me.Text)
        If clsCommon.myLen(discCode) > 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "This record can't be deleted.It is used in another process", Me.Text)
            Exit Sub
        End If
        ' Code Ends 
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsStateMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub frmStateMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True

        funReset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        AllowGSTApplicable = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GSTApplicable, clsFixedParameterCode.GSTApplicable, Nothing)) = 1, True, False)
        If AllowGSTApplicable = True Then
            RadGroupBox5.Enabled = True
            txtGSTStateCode.Enabled = True
            ChkGSTUT.Enabled = True
        End If

    End Sub

    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.frmStateMaster1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            MenuItemImport.Enabled = True
            MenuItemExport.Enabled = True
        Else
            MenuItemImport.Enabled = False
            MenuItemExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub LoadGrid()
        Dim qry As String = "select region_code as Code,region_name as [Region Name] from tspl_region_master"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        dt.Columns.Add("Select", GetType(Boolean))

        GV.DataSource = dt

        GV.Columns("Code").Width = 150
        GV.Columns("Code").ReadOnly = True

        GV.Columns("Region Name").Width = 250
        GV.Columns("Region Name").ReadOnly = True

        GV.Columns("Select").ReadOnly = False

    End Sub

    Sub funReset()
        isNewEntry = True

        GV.DataSource = Nothing
        GV.Rows.Clear()
        GV.Columns.Clear()

        LoadGrid()

        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtName.Text = ""
        txtCountry.Value = ""
        lblCountry.Text = ""
        ChkIsWayBillReqd.Checked = False
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        txtGSTStateCode.Text = ""
        ChkGSTUT.Checked = False
        chkIsDefault.Checked = False
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_STATE_MASTER where STATE_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = "select STATE_CODE AS Code, STATE_NAME as Name from TSPL_STATE_MASTER"
            'txtCode.Value = clsCommon.ShowSelectForm("STATE_MASTER", qry, "Code", "", txtCode.Value, "STATE_CODE", isButtonClicked)
            txtCode.Value = clsStateMaster.getFinder("", txtCode.Value, isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If


    End Sub

    Sub funFill()

    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmStateMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub txtCountry__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCountry._MYValidating
        'Dim qry As String = "select COUNTRY_CODE AS Code, COUNTRY_NAME AS Name from TSPL_COUNTRY_MASTER"
        'txtCountry.Value = clsCommon.ShowSelectForm("COUNTRY_FINDER", qry, "Code", "", txtCode.Value, "COUNTRY_CODE", isButtonClicked)
        txtCountry.Value = clsCountryMaster.getFinder("", txtCountry.Value, isButtonClicked)
        lblCountry.Text = clsCountryMaster.GetName(txtCountry.Value, Nothing)
    End Sub

    Private Sub MenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim inputs() As String = {}

        If AllowGSTApplicable = True Then
            inputs = {"Code", "State Name", "Country Code", "Is Way Bill Req", "IS GST UT", "GST State Code"}
        Else
            inputs = {"Code", "State Name", "Country Code", "Is Way Bill Req"}
        End If

        Dim Strs As List(Of String) = New List(Of String)(inputs)

        If transportSql.importExcel(gv, Strs.ToArray()) Then
            ' Dim trans As SqlTransaction
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsStateMaster()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect.")
                    End If
                    obj.Code = strCode

                    Dim strName As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If strName.Length > 100 Or (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("Name can not be blank or incorrect.")
                    End If
                    obj.Name = strName

                    Dim strCOUNTRY_CODE As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If strCOUNTRY_CODE.Length > 30 Then
                        Throw New Exception("Description can not be blank or incorrect.")
                    End If
                    obj.COUNTRY_CODE = strCOUNTRY_CODE

                    Dim strIsWayBillReq As String = clsCommon.myCdbl(grow.Cells(3).Value)
                    'If strIsWayBillReq > 1 OrElse strIsWayBillReq < 0 Then
                    '    Throw New Exception("Descripti can not be blank or incorrect.")
                    'End If
                    obj.IsWayBillRequired = strIsWayBillReq


                    If AllowGSTApplicable = True Then
                        Dim GSTUT As String = clsCommon.myCdbl(grow.Cells(4).Value)
                        obj.GST_UT = GSTUT

                        Dim GSTStateCode As String = clsCommon.myCstr(grow.Cells(5).Value)
                        Dim qry As String = ""
                        If clsCommon.myLen(GSTStateCode) > 0 Then
                            qry = clsDBFuncationality.getSingleValue("select State_Code from tspl_state_master where GST_State_Code='" & GSTStateCode & "' and State_Code not in ('" & obj.Code & "')")
                        End If

                        If clsCommon.myLen(Qry) > 0 Then
                            Throw New Exception("GST State Code already Used. ")
                        End If
                        obj.GSTStateCode = GSTStateCode
                    End If
                    
                    obj.SaveData(obj, clsStateMaster.CheckNewEntry(obj.Code), Nothing)
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub


    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemExport.Click
        Dim str As String
        str = "select STATE_CODE AS Code, STATE_NAME as [State Name], COUNTRY_CODE as [Country Code],is_waybill_reqd as [Is Way Bill Req]"
        If AllowGSTApplicable = True Then
            str += " ,Is_GST_UT as [IS GST UT], GST_STATE_Code as [GST State Code] "
        End If
        str += " from TSPL_STATE_MASTER"
        ListImpExpColumnsMandatory = New List(Of String)({"Code", "State Name"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Code"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)

    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemClose.Click
        funClose()
    End Sub

    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        Dim str As String
        str = "select STATE_CODE,Region_Code from TSPL_STATE_MASTER_DETAIL"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "STATE_CODE", "Region_Code") Then
            Dim ii As Integer = 0
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
            Try
                clsCommon.ProgressBarShow()
                Dim arr As New List(Of String)
                For Each grow As GridViewRowInfo In gv.Rows
                    If Not arr.Contains(clsCommon.myCstr(grow.Cells("STATE_CODE").Value)) Then
                        arr.Add(clsCommon.myCstr(grow.Cells("STATE_CODE").Value))
                    End If
                Next
                If arr Is Nothing OrElse arr.Count <= 0 Then
                    Throw New Exception("No Data found to import having state.")
                End If
                Qry = "delete from TSPL_STATE_MASTER_DETAIL where state_code in (" + clsCommon.GetMulcallString(arr) + ") "
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                For Each grow As GridViewRowInfo In gv.Rows
                    ii += 1
                    Dim strstate As String = clsCommon.myCstr(grow.Cells("STATE_CODE").Value)
                    Dim strRegion As String = clsCommon.myCstr(grow.Cells("Region_Code").Value)
                    If clsCommon.myLen(strstate) > 0 AndAlso clsCommon.myLen(strRegion) > 0 Then
                        Dim dtState As DataTable = clsDBFuncationality.GetDataTable("select STATE_CODE,STATE_NAME,COUNTRY_CODE from TSPL_STATE_MASTER where STATE_CODE='" + strstate + "'", trans)
                        If dtState Is Nothing OrElse dtState.Rows.Count <= 0 Then
                            Throw New Exception("State code is not exists")
                        End If
                        Dim dtRegion As DataTable = clsDBFuncationality.GetDataTable("select REGION_CODE,REGION_NAME from TSPL_REGION_MASTER where REGION_CODE='" + strRegion + "'", trans)
                        If dtRegion Is Nothing OrElse dtRegion.Rows.Count <= 0 Then
                            Throw New Exception("Region code is not exists")
                        End If

                        Dim coll1 As New Hashtable()
                        clsCommon.AddColumnsForChange(coll1, "state_code", clsCommon.myCstr(dtState.Rows(0)("STATE_CODE")))
                        clsCommon.AddColumnsForChange(coll1, "state_name", clsCommon.myCstr(dtState.Rows(0)("STATE_NAME")))
                        clsCommon.AddColumnsForChange(coll1, "country_code", clsCommon.myCstr(dtState.Rows(0)("COUNTRY_CODE")))
                        clsCommon.AddColumnsForChange(coll1, "created_by", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll1, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll1, "modified_by", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll1, "modified_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll1, "region_code", clsCommon.myCstr(dtRegion.Rows(0)("REGION_CODE")))
                        clsCommon.AddColumnsForChange(coll1, "region_name", clsCommon.myCstr(dtRegion.Rows(0)("REGION_NAME")))
                        clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_STATE_MASTER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Error at row no " + clsCommon.myCstr(ii) + Environment.NewLine + ex.Message)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

   
End Class

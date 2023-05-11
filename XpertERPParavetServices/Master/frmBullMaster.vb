Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common

Public Class FrmBullMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt("BULLMST")
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub AddNew()
        txtBullMaster.Value = ""
        dtpBullDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtDescription.Text = ""
        txtBullProfileId.Text = ""
        txtCattleType.Value = ""
        lblCattleType.Text = ""
        txtNoOfStraws.Text = ""
        dtpDOB.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        cboBullStatus.Text = ""
        txtSiteId.Text = ""
        txtDamsYield.Text = ""
        txtBreedDetails.Text = ""
        txtBreedInfo.Text = ""
        txtBullMaster.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        txtBullMaster.Focus()
    End Sub
    Private Sub FrmBullMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        AddNew()
        LoadBullStatus()
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
        GC.Collect()
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As clsBullMaster = clsBullMaster.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtBullMaster.Value = obj.Bull_No
            dtpBullDate.Value = clsCommon.GetPrintDate(obj.Bull_Date, "dd/MM/yyyy")
            txtDescription.Text = obj.Bull_Desc
            txtBullProfileId.Text = obj.Bull_Profile_Id
            txtCattleType.Value = obj.Cattle_Type
            If clsCommon.myLen(obj.Cattle_Type) > 0 Then
                lblCattleType.Text = clsDBFuncationality.getSingleValue(" select  Cattle_Type_Name from  TSPL_CATTLE_TYPE_MASTER where Cattle_Type_Code='" & obj.Cattle_Type & "'")
            End If
            txtNoOfStraws.Text = obj.No_of_Straws
            dtpDOB.Value = clsCommon.GetPrintDate(obj.DOB, "dd/MM/yyyy")
            cboBullStatus.Text = obj.Status
            txtSiteId.Text = obj.Site_Id
            txtDamsYield.Text = obj.Dams_Yield
            txtBreedDetails.Text = obj.Breed_Details
            txtBreedInfo.Text = obj.Breed_info
            txtBullMaster.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
        End If
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtBullMaster.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtBullMaster.Value)) > 16 Then
                myMessages.blankValue("Bull No")
                txtBullMaster.Focus()
                txtBullMaster.Select()
                Errorcontrol.SetError(txtBullMaster, "Bull No")
                Return False
            Else
                Errorcontrol.ResetError(txtBullMaster)
            End If
            If clsCommon.myLen(clsCommon.myCstr(txtBullProfileId.Text)) <= 0 Then
                myMessages.blankValue("Bull Profile Id")
                txtBullProfileId.Focus()
                txtBullProfileId.Select()
                Errorcontrol.SetError(txtBullProfileId, "Bull Profile Id")
                Return False
            Else
                Errorcontrol.ResetError(txtBullProfileId)
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtCattleType.Value)) <= 0 Then
                myMessages.blankValue("Cattle Type")
                txtCattleType.Focus()
                txtCattleType.Select()
                Errorcontrol.SetError(txtCattleType, "Cattle Type")
                Return False
            Else
                Errorcontrol.ResetError(txtCattleType)
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtNoOfStraws.Text)) <= 0 Then
                myMessages.blankValue("No of Straws")
                txtNoOfStraws.Focus()
                txtNoOfStraws.Select()
                Errorcontrol.SetError(txtNoOfStraws, "No of Straws")
                Return False
            Else
                Errorcontrol.ResetError(txtNoOfStraws)
            End If

            If clsCommon.myLen(clsCommon.myCstr(cboBullStatus.Text)) <= 0 Then
                myMessages.blankValue("Status")
                cboBullStatus.Focus()
                cboBullStatus.Select()
                Errorcontrol.SetError(cboBullStatus, "Status")
                Return False
            Else
                Errorcontrol.ResetError(cboBullStatus)
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Function

    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsBullMaster()
                obj.Bull_No = txtBullMaster.Value
                obj.Bull_Date = dtpBullDate.Value
                obj.Bull_Desc = txtDescription.Text
                obj.Bull_Profile_Id = txtBullProfileId.Text
                obj.Cattle_Type = txtCattleType.Value
                obj.No_of_Straws = txtNoOfStraws.Text
                obj.DOB = dtpDOB.Value
                obj.Status = cboBullStatus.Text
                obj.Site_Id = txtSiteId.Text
                obj.Dams_Yield = txtDamsYield.Text
                obj.Breed_Details = txtBreedDetails.Text
                obj.Breed_info = txtBreedInfo.Text
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Bull_No) from TSPL_Paravet_Bull_Master WHERE Bull_No ='" + obj.Bull_No + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (clsBullMaster.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.Bull_No, NavigatorType.Current)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                Else
                    btnsave.Text = "Save"
                    btndelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtBullMaster.Value) <= 0 Then
                Throw New Exception("Bull No not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure? do you want to delete this Bull No ('" + txtBullMaster.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_Paravet_Bull_Master WHERE Bull_No='" + txtBullMaster.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow("Deleted Successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Code is in use")
            Else
                clsCommon.MyMessageBoxShow(ex.Message)
            End If
        End Try
    End Sub
    Private Sub txtBullMaster__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtBullMaster._MYNavigator
        Try
            LoadData(txtBullMaster.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtBullMaster__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBullMaster._MYValidating
        Dim str As String = "select count(*) from TSPL_Paravet_Bull_Master where Bull_No ='" + txtBullMaster.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtBullMaster.MyReadOnly = False
        Else
            txtBullMaster.MyReadOnly = True
        End If

        If txtBullMaster.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select Bull_No As [Bull_No],Bull_Desc as [Bull Desc],Bull_Date As [Bull Date],Bull_Profile_Id as [Bull Profile Id],Cattle_Type as [Cattle Type], No_of_Straws as [No of Straws] ,DOB ,Status   from TSPL_Paravet_Bull_Master "
            txtBullMaster.Value = clsCommon.ShowSelectForm("TSPL_Paravet_Bull_Master", qry, "Bull_No", "", txtBullMaster.Value, "TSPL_Paravet_Bull_Master.Bull_No", isButtonClicked)
            If clsCommon.myLen(txtBullMaster.Value) > 0 Then
                Dim objOT As clsBullMaster
                objOT = clsBullMaster.GetData(txtBullMaster.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtBullMaster.Value, NavigatorType.Current)
                End If
            Else
                AddNew()
            End If
        End If
    End Sub
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        AddNew()
    End Sub
    Private Sub FrmBullMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
            GC.Collect()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub
    Private Sub txtCattleType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCattleType._MYValidating
        Dim query As String = "  select Cattle_Type_Code as [Code], Cattle_Type_Name as [Description] from TSPL_CATTLE_TYPE_MASTER "
        txtCattleType.Value = clsCommon.ShowSelectForm("CattleTypevald", query, "Code", "", txtCattleType.Value, "Code", isButtonClicked)
        Dim desc As String = " select  Cattle_Type_Name from  TSPL_CATTLE_TYPE_MASTER where Cattle_Type_Code='" & txtCattleType.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblCattleType.Text = clsCommon.myCstr(dt.Rows(0)("Cattle_Type_Name"))
        Else
            lblCattleType.Text = ""
        End If
    End Sub

    Sub LoadBullStatus()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("Active", "Active")
        dt.Rows.Add("Inactive", "Inactive")
        cboBullStatus.DataSource = dt
        cboBullStatus.DisplayMember = "Name"
        cboBullStatus.ValueMember = "Code"
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Dim str As String
        str = "select Bull_No as [Bull No],Bull_Desc as [Bull Desc],Bull_Date as [Bull No Date] ,Bull_Profile_Id as [Bull Profile Id],Cattle_Type as [Cattle Type] ,No_of_Straws as [No of Straws],convert(varchar,DOB,103) as DOB ,Status ,Site_Id as [Site Id],Dams_Yield as [Dams Yield],Breed_Details as [Breed Details],Breed_info as [Breed info]  from TSPL_Paravet_Bull_Master "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Bull No", "Bull Desc", "Bull No Date", "Bull Profile Id", "Cattle Type", "No of Straws", "DOB", "Status", "Site Id", "Dams Yield", "Breed Details", "Breed info") Then
            Dim linno As Integer = 1
            Try
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New clsBullMaster()
                    Dim strBullNo As String = clsCommon.myCstr(grow.Cells("Bull No").Value)
                    Dim strBullDesc As String = clsCommon.myCstr(grow.Cells("Bull Desc").Value)
                    Dim strBullNoDate As Date = clsCommon.myCstr(grow.Cells("Bull No Date").Value)
                    Dim strBullProfileId As String = clsCommon.myCstr(grow.Cells("Bull Profile Id").Value)
                    Dim strCattleType As String = clsCommon.myCstr(grow.Cells("Cattle Type").Value)
                    Dim strNoofStraws As String = clsCommon.myCstr(grow.Cells("No of Straws").Value)
                    Dim strDOB As String = clsCommon.myCstr(grow.Cells("DOB").Value)
                    Dim strStatus As String = clsCommon.myCstr(grow.Cells("Status").Value)
                    Dim strSiteId As String = clsCommon.myCstr(grow.Cells("Site Id").Value)
                    Dim strDamsYield As String = clsCommon.myCstr(grow.Cells("Dams Yield").Value)
                    Dim strBreedDetails As String = clsCommon.myCstr(grow.Cells("Breed Details").Value)
                    Dim strBreedInfo As String = clsCommon.myCstr(grow.Cells("Breed info").Value)

                    linno += 1
                    If clsCommon.myLen(strBullNo) <= 0 Then
                        Throw New Exception("Bull No should not be left blankat line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(strBullNo) > 30 Then
                        Throw New Exception("Please check ! length of Bull No should be 30 at line no. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strBullDesc) <= 0 Then
                        Throw New Exception("Description should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(strBullDesc) > 150 Then
                        Throw New Exception("Please check ! length of description should be 200 at line no. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strStatus) <= 0 Then
                        Throw New Exception("Status should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                       
                    End If
                    If clsCommon.myLen(strCattleType) <= 0 Then
                        Throw New Exception("Cattle Type should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")

                    End If
                    If clsCommon.myLen(strCattleType) > 0 Then
                        Dim Values As String = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_CATTLE_TYPE_MASTER where Cattle_Type_Code=  '" & strCattleType & "'  ")
                        If Values <= 0 Then
                            Throw New Exception("Invalid Cattle Type Code,Line No " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    If (DateTime.Compare(DateTime.Now, strDOB) < 0) Then
                        Throw New Exception("DOB Date not greter then Current Date at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If (clsCommon.CompairString(strStatus.ToUpper(), "ACTIVE") <> CompairStringResult.Equal) Then
                        If (clsCommon.CompairString(strStatus.ToUpper(), "INACTVE") <> CompairStringResult.Equal) Then
                            Throw New Exception("Bull Status Should be Active or Inactive at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If



                    If clsCommon.myLen(strBullNo) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from tspl_Paravet_Bull_Master where Bull_No='" + strBullNo + "' ") > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If

                    obj.Bull_No = strBullNo
                    obj.Bull_Date = clsCommon.GetPrintDate(strBullNoDate, "dd/MMM/yyyy")
                    obj.Bull_Desc = strBullDesc
                    obj.Bull_Profile_Id = strBullProfileId
                    obj.Cattle_Type = strCattleType
                    obj.No_of_Straws = strNoofStraws
                    obj.DOB = clsCommon.GetPrintDate(strDOB, "dd/MMM/yyyy")
                    obj.Status = strStatus
                    obj.Site_Id = strSiteId
                    obj.Dams_Yield = strDamsYield
                    obj.Breed_Details = strBreedDetails
                    obj.Breed_info = strBreedInfo
                    clsBullMaster.SaveData(obj, IsNewEntry)

                Next
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
End Class

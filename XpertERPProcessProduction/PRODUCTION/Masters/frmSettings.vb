Imports common
Imports System.Data.SqlClient
'''' <summary>
'''' ''''''''''''''''''''''''''''''''BM00000000457''''''''''''''''''
'''' </summary>
'''' <remarks></remarks>
Public Class FrmSettings
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim formtype As String = Nothing

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        formtype = formid
    End Sub
    Private Sub FrmSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(rdbtnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")

        LoadData()
    End Sub

#Region "Functions"
    
    Private Sub SetUserMgmtNew()

        If formtype = clsUserMgtCode.SETTSTD Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.SETTSTD)
        ElseIf formtype = clsUserMgtCode.SETTPep Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.SETTPep)
        End If
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        rdbtnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag

    End Sub

    Sub LoadData()


        LoadIssue()
        LoadReceipt()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As ClsMFSeetings = ClsMFSeetings.GetData(trans)
            If obj IsNot Nothing Then
                isNewEntry = False
                If obj.ALLOW_AUTO_CLOSE_MO_DURING_RECEIPT = True Then
                    ChkAutoclose.Checked = True
                Else
                    ChkAutoclose.Checked = False
                End If

                txtmixing_charge.Text = obj.MixingCHrage
                txtAutoClose.Text = clsCommon.myCstr(obj.AUTO_CLOSE_TOLERANCE)
                txtIssue.Text = clsCommon.myCstr(obj.ISSUE_TOLRC)
                txtReceipt.Text = clsCommon.myCstr(obj.REC_TOLRC)
                If obj.ACTIVATE_MO_SERIES = True Then
                    chkActivate.Checked = True
                Else
                    chkActivate.Checked = False
                End If
                If obj.ALLOW_6DEC_STD_UNIT_COST = True Then
                    chk6decimal.Checked = True
                Else
                    chk6decimal.Checked = False
                End If
                If obj.ALLOW_RECEIVE_WITHOUT_ISSUANCE = True Then
                    chkReceive.Checked = True
                Else
                    chkReceive.Checked = False
                End If

                ddlissue.SelectedValue = clsCommon.myCstr(obj.EXCEED_ISSUE_TOLRC)
                ddlReceipt.SelectedValue = clsCommon.myCstr(obj.REC_TOLRC)
                fndProduction.Value = clsCommon.myCstr(obj.AREA_CODE)
                fndLocation.Value = clsCommon.myCstr(obj.LOCATION_CODE)
                txtCost.Text = clsCommon.myCstr(obj.IC_COST_ITEMS_DURING)
                txtProduction.Text = clsDBFuncationality.getSingleValue("Select DESCRIPTION  from TSPL_MF_PRODUCTION_AREA where AREA_CODE='" + fndProduction.Value + "' ", trans)
                txtLocation.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "' ", trans)
                trans.Commit()
            End If

        Catch ex As Exception
            trans.Rollback()
        End Try
    End Sub
  
    Sub SaveData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowToSave() Then
                Dim obj As New ClsMFSeetings()
                obj.MixingCHrage = clsCommon.myCdbl(txtmixing_charge.Text)
                obj.ALLOW_AUTO_CLOSE_MO_DURING_RECEIPT = clsCommon.myCstr(IIf(ChkAutoclose.Checked, "1", "0"))
                obj.AUTO_CLOSE_TOLERANCE = clsCommon.myCstr(txtAutoClose.Text)
                obj.ACTIVATE_MO_SERIES = clsCommon.myCstr(IIf(chkActivate.Checked, "1", "0"))
                obj.ALLOW_6DEC_STD_UNIT_COST = clsCommon.myCstr(IIf(chk6decimal.Checked, "1", "0"))
                obj.ALLOW_RECEIVE_WITHOUT_ISSUANCE = clsCommon.myCstr(IIf(chkReceive.Checked, "1", "0"))
                obj.EXCEED_ISSUE_TOLRC = clsCommon.myCstr(ddlissue.SelectedValue)
                obj.REC_TOLRC = clsCommon.myCstr(txtReceipt.Text)
                obj.EXCEED_REC_TOLRC = clsCommon.myCstr(ddlReceipt.SelectedValue)
                obj.ISSUE_TOLRC = clsCommon.myCstr(txtIssue.Text)

                obj.AREA_CODE = clsCommon.myCstr(fndProduction.Value)
                obj.LOCATION_CODE = clsCommon.myCstr(fndLocation.Value)
                If (txtCost.Text = "") Then
                    obj.IC_COST_ITEMS_DURING = "Posting"
                Else
                    obj.IC_COST_ITEMS_DURING = clsCommon.myCstr(txtCost.Text)
                End If
              
                If (ClsMFSeetings.SaveData(obj, trans)) Then
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    'LoadData()

                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
       
        Return True
    End Function
    Private Sub LoadIssue()
        ddlissue.DataSource = DATATABLEFILL()
        ddlissue.ValueMember = "Code"
        ddlissue.DisplayMember = "Name"
    End Sub
    Private Sub LoadReceipt()
        ddlReceipt.DataSource = DATATABLEFILL()
        ddlReceipt.ValueMember = "Code"
        ddlReceipt.DisplayMember = "Name"
    End Sub
    Public Function DATATABLEFILL() As DataTable
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Warning"
        dr("Name") = "Warning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "None"
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Error"
        dr("Name") = "Error"
        dt.Rows.Add(dr)
        Return dt
    End Function
#End Region
#Region "Finders"
    Private Sub fndProduction__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndProduction._MYValidating
        Dim qry As String = "Select AREA_CODE as Code,DESCRIPTION as Description from TSPL_MF_PRODUCTION_AREA"
        fndProduction.Value = clsCommon.ShowSelectForm("REC_CONfnd", qry, "AccountCode", "", fndProduction.Value, "", isButtonClicked)
        txtProduction.Text = clsDBFuncationality.getSingleValue("select description from TSPL_MF_PRODUCTION_AREA where AREA_CODE='" + fndProduction.Value + "' ")
    End Sub

    Private Sub fndLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocation._MYValidating
        Dim qry As String = "Select Location_Code as Code,Location_Desc as Description from TSPL_LOCATION_MASTER"
        fndLocation.Value = clsCommon.ShowSelectForm("REC_CONfnd2", qry, "Code", "", fndLocation.Value, "", isButtonClicked)
        txtLocation.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "' ")
    End Sub
#End Region
#Region "EVENTS"
    Private Sub FrmSettings_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rdbtnsave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
#End Region

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub rdbtnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnsave.Click
        SaveData()
    End Sub
End Class

Imports common
Imports System.Data.SqlClient

'''' <summary>
'''' ''''
'''' </summary>
'''' <remarks></remarks>

''''''''''''''''''''''''''''''''''''''''''Ticket No:BM00000000456''''''''''''''''''''''''''''''''''''''''''''''''Created by Shipra on 13/09/13''''''
Public Class FrmAccountSetting
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim formtype As String = Nothing

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        formtype = formid
    End Sub
    Private Sub FrmAccountSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(rdbtnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(rdbtndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnnew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        LoadData("", NavigatorType.Current)
    End Sub
#Region "Finders"


    Private Sub fndWIP__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndWIP._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndWIP.Value = clsCommon.ShowSelectForm("REC_CONfnd", qry, "AccountCode", " ControlAccount ='Y' ", fndWIP.Value, "", isButtonClicked)
        txtWIP.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndWIP.Value + "' ")
    End Sub

    Private Sub fndSetupLabor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSetupLabor._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndSetupLabor.Value = clsCommon.ShowSelectForm("REC_CONfnd1", qry, "AccountCode", " ControlAccount ='Y' ", fndSetupLabor.Value, "", isButtonClicked)
        txtSetupLabor.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndSetupLabor.Value + "' ")
    End Sub


    Private Sub fndRunLabor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndRunLabor._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndRunLabor.Value = clsCommon.ShowSelectForm("REC_CONfnd2", qry, "AccountCode", " ControlAccount ='Y' ", fndRunLabor.Value, "", isButtonClicked)
        txtRunLabor.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndRunLabor.Value + "' ")

    End Sub

    Private Sub fndSubContract__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSubContract._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndSubContract.Value = clsCommon.ShowSelectForm("REC_CONfnd3", qry, "AccountCode", " ControlAccount ='Y' ", fndSubContract.Value, "", isButtonClicked)
        txtSubContract.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndSubContract.Value + "' ")
    End Sub

    Private Sub fndOverhead__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndOverhead._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndOverhead.Value = clsCommon.ShowSelectForm("REC_CONfnd4", qry, "AccountCode", " ControlAccount ='Y' ", fndOverhead.Value, "", isButtonClicked)
        txtOverhead.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndOverhead.Value + "' ")
    End Sub

    Private Sub fndMaterialVariance__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndMaterialVariance._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndMaterialVariance.Value = clsCommon.ShowSelectForm("REC_CONfnd5", qry, "AccountCode", " ControlAccount ='Y' ", fndMaterialVariance.Value, "", isButtonClicked)
        txtMaterialVariance.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndMaterialVariance.Value + "' ")
    End Sub

    Private Sub fndProductionVariance__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndProductionVariance._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndProductionVariance.Value = clsCommon.ShowSelectForm("REC_CONfnd6", qry, "AccountCode", " ControlAccount ='Y' ", fndProductionVariance.Value, "", isButtonClicked)
        txtProductionvariance.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndProductionVariance.Value + "' ")
    End Sub
    Private Sub fndWIPCategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndWIPCategory._MYValidating
        Dim qry As String = "select category_code as [Code],description as [Description] from TSPL_MF_CATEGORY "
        'Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        'fndWIPCategory.Value = clsCommon.ShowSelectForm("REC_CONfnd6", qry, "AccountCode", " ControlAccount ='Y' ", fndWIPCategory.Value, "", isButtonClicked)
        fndWIPCategory.Value = clsCommon.ShowSelectForm("REC_CONfnd6", qry, "Code", "  ", fndWIPCategory.Value, "", isButtonClicked)
        txtWIPCategory.Text = clsDBFuncationality.getSingleValue("select description from TSPL_MF_CATEGORY where category_code='" + fndWIPCategory.Value + "' ")
    End Sub
    Private Sub fndaccountsetcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndaccountsetcode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_MF_ACCOUNTSETS where ACCOUNT_SET_CODE ='" + fndaccountsetcode.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 AndAlso isButtonClicked = False Then
                fndaccountsetcode.MyReadOnly = False
            Else
                fndaccountsetcode.MyReadOnly = True
            End If
            If fndaccountsetcode.MyReadOnly OrElse isButtonClicked Then

                'Dim qry As String = " select ACCOUNT_SET_CODE as Code,  DESCRIPTION as 'Description' from TSPL_MF_ACCOUNTSETS "
                'fndaccountsetcode.Value = clsCommon.ShowSelectForm("TSPL_MF_ACCOUNTSETS", qry, "Code", "", fndaccountsetcode.Value, "", isButtonClicked)
                fndaccountsetcode.Value = clsAccountSetting.getFinder("", fndaccountsetcode.Value, isButtonClicked)
                If fndaccountsetcode.Value <> "" Then
                    LoadData(fndaccountsetcode.Value, NavigatorType.Current)
                Else
                    Reset()
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message())
        End Try
    End Sub

    Private Sub fndaccountsetcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndaccountsetcode._MYNavigator
        Try
            LoadData(fndaccountsetcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
#End Region
#Region "Functions"
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsMFAccountSet.DeleteData(fndaccountsetcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Private Sub SetUserMgmtNew()        
        If formtype = clsUserMgtCode.ACCSETMFGSTD Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.ACCSETMFGSTD)
        ElseIf formtype = clsUserMgtCode.ACCSETMFGDairy Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.ACCSETMFGDairy)
        ElseIf formtype = clsUserMgtCode.ACCSETMFGPepsi Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.ACCSETMFGPepsi)
        End If
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        rdbtnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        rdbtndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsMFAccountSet = ClsMFAccountSet.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            fndaccountsetcode.Value = obj.ACCOUNT_SET_CODE
            txtAccdescription.Text = obj.DESCRIPTION
            fndMaterialVariance.Value = obj.GL_MATERIAL_VARIANCE
            fndOverhead.Value = obj.GL_OVERHEAD
            fndProductionVariance.Value = obj.GL_PRODUCTION_VARIANCE
            fndRunLabor.Value = obj.GL_RUN_LABOR
            fndSetupLabor.Value = obj.GL_SETUP_LABOR
            fndSubContract.Value = obj.GL_SUBCONTRACT
            fndWIP.Value = obj.GL_WIP
            fndWIPCategory.Value = obj.WIP_CATEGORY
            txtWIP.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndWIP.Value + "' ")
            txtSetupLabor.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndSetupLabor.Value + "' ")
            txtRunLabor.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndRunLabor.Value + "' ")
            txtSubContract.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndSubContract.Value + "' ")
            txtOverhead.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndOverhead.Value + "' ")
            txtMaterialVariance.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndMaterialVariance.Value + "' ")
            txtProductionvariance.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndProductionVariance.Value + "' ")
            txtWIPCategory.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndWIPCategory.Value + "' ")
            fndaccountsetcode.MyReadOnly = True
        End If
    End Sub
    Sub Reset()
        fndaccountsetcode.Value = ""
        txtAccdescription.Text = ""
        fndaccountsetcode.MyReadOnly = False
        fndMaterialVariance.Value = ""
        txtMaterialVariance.Text = ""
        fndMaterialVariance.MyReadOnly = False
        fndOverhead.Value = ""
        txtOverhead.Text = ""
        fndOverhead.MyReadOnly = False
        fndProductionVariance.Value = ""
        txtProductionvariance.Text = ""
        fndProductionVariance.MyReadOnly = False
        fndRunLabor.Value = ""
        txtRunLabor.Text = ""
        fndRunLabor.MyReadOnly = False
        fndSetupLabor.Value = ""
        txtSetupLabor.Text = ""
        fndSetupLabor.MyReadOnly = False
        fndSubContract.Value = ""
        txtSubContract.Text = ""
        fndSubContract.MyReadOnly = False
        fndWIP.Value = ""
        txtWIP.Text = ""
        fndWIP.MyReadOnly = False
        fndWIPCategory.Value = ""
        txtWIPCategory.Text = ""
        fndWIPCategory.MyReadOnly = False
    End Sub
    Sub SaveData()
        If AllowToSave() Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try

                Dim obj As New ClsMFAccountSet()
                obj.ACCOUNT_SET_CODE = fndaccountsetcode.Value
                obj.DESCRIPTION = txtAccdescription.Text
                obj.GL_MATERIAL_VARIANCE = fndMaterialVariance.Value
                obj.GL_OVERHEAD = fndOverhead.Value
                obj.GL_PRODUCTION_VARIANCE = fndProductionVariance.Value
                obj.GL_RUN_LABOR = fndRunLabor.Value
                obj.GL_SETUP_LABOR = fndSetupLabor.Value
                obj.GL_SUBCONTRACT = fndSubContract.Value
                obj.GL_WIP = fndWIP.Value
                obj.WIP_CATEGORY = fndWIPCategory.Value

                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(ACCOUNT_SET_CODE) from TSPL_MF_ACCOUNTSETS where ACCOUNT_SET_CODE='" + obj.ACCOUNT_SET_CODE + "'", trans)
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (ClsMFAccountSet.SaveData(obj, isNewEntry, trans)) Then
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.ACCOUNT_SET_CODE, NavigatorType.Current)

                End If
            Catch ex As Exception
                trans.Rollback()
                clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            End Try
        End If
    End Sub
    Private Function AllowToSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If clsCommon.myLen(clsCommon.myCstr(fndaccountsetcode.Value)) <= 0 Then
                fndaccountsetcode.Focus()
                Throw New Exception("Please Fill AccountSet Code")
            End If
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtAccdescription.Text)) <= 0 Then
            txtAccdescription.Focus()
            Throw New Exception("Please Fill Description")
        End If
        Return True
    End Function
#End Region

#Region " Events"


    Private Sub rdbtnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnsave.Click
        SaveData()
    End Sub

    Private Sub rdbtndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtndelete.Click
        funDelete()
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub rdbtnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnnew.Click
        Reset()
    End Sub

    Private Sub FrmAccountSetting_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso rdbtnnew.Enabled Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rdbtnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rdbtndelete.Enabled Then
            funDelete()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
#End Region


End Class

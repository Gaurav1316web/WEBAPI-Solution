Imports common
Imports System.Data.SqlClient
Public Class FrmBMCCategory
    Inherits FrmMainTranScreen

#Region "Variables"
    Public TransactionNo As String
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Private Sub CreateTab()
        Try
            Dim coll As Dictionary(Of String, String)
            coll = New Dictionary(Of String, String)()
            coll.Add("Document_Code", "Varchar(30) not null  PRIMARY KEY")
            coll.Add("Document_Date", "datetime not NULL")
            coll.Add("Description", "varchar(50) Not NULL")
            coll.Add("Created_By", "varchar(12) NOT NULL")
            coll.Add("Created_Date", "Datetime NOT NULL")
            coll.Add("Modified_By", "varchar(12) NOT NULL")
            coll.Add("Modified_Date", "Datetime NOT NULL")
            clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BMC_Category_Master", coll, "", True, False, "", "Document_Code", "Document_Date", True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FiscalYear)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_BMC_Category_Master where Document_Code='" + txtDocNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If

            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        AddNew()
        Dim obj As ClsBMCCategory = ClsBMCCategory.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtDocNo.Value = obj.Document_Code
            txtDescription.Text = obj.Description
            txtDate.Value = obj.Document_Date
        End If
    End Sub

    Private Sub AddNew()
        txtDocNo.Value = ""
        txtDescription.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()

        isNewEntry = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        txtDocNo.MyReadOnly = False

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.FiscalYear, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            Dim obj As New ClsBMCCategory()
            obj.Document_Code = txtDocNo.Value
            obj.Description = txtDescription.Text
            obj.Document_Date = txtDate.Value

            If (obj.SaveData(obj, isNewEntry)) Then
                clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                LoadData(obj.Document_Code, NavigatorType.Current)
                btnSave.Enabled = False
                objCommonVar.RefreshCommonVar()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim str As String = "select count(*) from TSPL_BMC_Category_Master where Document_Code ='" + txtDocNo.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            txtDocNo.MyReadOnly = False
        Else
            txtDocNo.MyReadOnly = True
        End If
        If txtDocNo.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Document_Code as Code,Description as Name from TSPL_BMC_Category_Master"
            Dim whrClas As String = " Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "

            txtDocNo.Value = clsCommon.ShowSelectForm("TSPL_BMC_Category_Master", qry, "Code", whrClas, txtDocNo.Value, "", isButtonClicked)
            LoadData(txtDocNo.Value, NavigatorType.Current)
        End If


    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        Me.Close()

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        DeleteData()

    End Sub

    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception(" Code not found to delete")

            End If
            If clsCommon.MyMessageBoxShow(Me, "Delete the Current Transaction." + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "delete from TSPL_BMC_Category_Master where Document_Code='" + txtDocNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow(Me, "Successfully Deleted", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, "Current Code is in use")
        End Try
    End Sub
End Class
Imports common
Imports System.Data.SqlClient
Imports common.Controls
Imports XpertERPEngine

Public Class FrmFixedSetting
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim formtype As String = Nothing
    Dim strFixedAssetSettUserCode As String = ""
#Region "User Defined Functions and Subroutines"
    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.fixedsetting)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

#End Region
    Sub LoadRoundType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr = dt.NewRow()
        dr("Code") = "Round Type"
        dr("Name") = "Round Type"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Upper"
        dr("Name") = "Upper"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Round"
        dr("Name") = "Round"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Lower"
        dr("Name") = "Lower"
        dt.Rows.Add(dr)




        cboRoundType.DataSource = dt
        cboRoundType.ValueMember = "Code"
        cboRoundType.DisplayMember = "Name"
    End Sub
    Sub SetControlsTag()

    End Sub
    Sub LoadData()
        Try

            For Each ctrl As Control In RadGroupBox1.Controls
                If ctrl.GetType Is GetType(MyCheckBox) Then
                    Dim chkBox As MyCheckBox = TryCast(ctrl, MyCheckBox)
                    If clsCommon.myLen(chkBox.Tag) >= 0 AndAlso clsCommon.myLen(chkBox.Tag1) >= 0 Then
                        chkBox.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(chkBox.Tag, chkBox.Tag1, Nothing)) = 1, True, False)
                    End If
                End If
            Next
            txtDecimal.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.AllowDecimalInFixedAsset + "' and code='" + clsFixedParameterCode.AllowDecimalInFixedAsset + "'"))
            cboRoundType.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.AllowRoundInFixedAsset + "' and code='" + clsFixedParameterCode.AllowRoundInFixedAsset + "'"))

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SaveData()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(strFixedAssetSettUserCode, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            For Each ctrl As Control In RadGroupBox1.Controls
                If ctrl.GetType Is GetType(MyCheckBox) Then
                    Dim chkBox As MyCheckBox = TryCast(ctrl, MyCheckBox)
                    If clsCommon.myLen(chkBox.Tag) >= 0 AndAlso clsCommon.myLen(chkBox.Tag1) >= 0 Then
                        clsFixedParameter.UpdateData(chkBox.Tag, chkBox.Tag1, IIf(chkBox.Checked, "1", "0"), trans)
                    End If
                End If
            Next
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_FIXED_PARAMETER Set description='" + clsCommon.myCstr(txtDecimal.Value) + "' where TYPE='" + clsFixedParameterType.AllowDecimalInFixedAsset + " ' and Code='" + clsFixedParameterCode.AllowDecimalInFixedAsset + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_FIXED_PARAMETER Set description='" + clsCommon.myCstr(cboRoundType.SelectedValue) + "' where TYPE='" + clsFixedParameterType.AllowRoundInFixedAsset + " ' and Code='" + clsFixedParameterCode.AllowRoundInFixedAsset + "'", trans)
            trans.Commit()
            clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)

        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub FrmFixedSetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(rdbtnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        SetControlsTag()
        LoadRoundType()
        LoadData()
    End Sub

    Private Sub rdbtnsave_Click(sender As Object, e As EventArgs) Handles rdbtnsave.Click
        SaveData()
    End Sub

    Private Sub rdbtnclose_Click(sender As Object, e As EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub FrmFixedSetting_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso rdbtnsave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
End Class

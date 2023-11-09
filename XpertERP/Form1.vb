Imports System.Data.SqlClient
Imports common
Public Class frmChangeCaption

    Public strProgramCode As String = ""
    Public strProgramType = ""

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        lblNwProgramCode.Visible = False
        txtNewProgramCode.Visible = False
        Me.MaximumSize = New Size(383, 143)
        Me.MinimumSize = New Size(383, 143)
        SplitContainer1.Panel2Collapsed = True
        Dim qry As String = "select Program_Code,Program_Name,Re_Name,right ( Customise_SNo,2) as Customise_SNo, Type from TSPL_PROGRAM_MASTER where Program_Code='" + strProgramCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            lblProgramCode.Text = strProgramCode
            lblDescription.Text = clsCommon.myCstr(dt.Rows(0)("Program_Name"))
            txtCaption.Text = clsCommon.myCstr(dt.Rows(0)("Re_Name"))
            txtCustomiseSno.Text = clsCommon.myCdbl(dt.Rows(0)("Customise_SNo"))
            strProgramType = clsCommon.myCstr(dt.Rows(0)("Type"))
        End If
        If clsCommon.CompairString(strProgramType, "M") = CompairStringResult.Equal Or clsCommon.CompairString(strProgramType, "SM") = CompairStringResult.Equal Then
            txtCustomiseSno.ReadOnly = True
            txtCustomiseSno.Visible = False
            MyLabel1.Visible = False
        Else
            txtCustomiseSno.ReadOnly = False
            txtCustomiseSno.Enabled = True
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(txtCustomiseSno.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Customise Sno can't be blank.", Me.Text)
            End If
            If clsCommon.myLen(txtCustomiseSno.Text) = 1 Then
                txtCustomiseSno.Text = "0" + clsCommon.myCstr(txtCustomiseSno.Text)
            End If


            ' Ticket : ERO/07/06/19-000640 by prabhakar
            Dim qry As String = "update TSPL_PROGRAM_MASTER set "
            If clsCommon.myLen(txtCaption.Text) > 0 Then
                qry += " Re_Name = '" + txtCaption.Text + "' "
            Else
                qry += " Re_Name = null "
            End If

            If Not (clsCommon.CompairString(strProgramType, "M") = CompairStringResult.Equal Or clsCommon.CompairString(strProgramType, "SM") = CompairStringResult.Equal) Then
                qry += ",Customise_SNo = stuff(Customise_SNo,9,3,'" + txtCustomiseSno.Text + "') "
                If rbtnAddNew.Checked OrElse rbtnMove.Checked Then
                    If clsCommon.myLen(txtModule.Value) <= 0 Then
                        Throw New Exception("Please select Module")
                    End If
                    If clsCommon.myLen(txtSubModule.Value) <= 0 Then
                        Throw New Exception("Please select Sub Module")
                    End If
                    If rbtnMove.Checked Then
                        qry += ",Parent_Code = '" + txtSubModule.Value + "'"
                    End If
                End If
            End If
            qry += " where Program_Code ='" + lblProgramCode.Text + "'"
            If rbtnAddNew.Checked Then
                If clsCommon.myLen(txtCaption.Text) <= 0 Then
                    Throw New Exception("Please enter Caption")
                End If

                If clsCommon.myLen(txtNewProgramCode.Text) <= 0 Then
                    txtNewProgramCode.Focus()
                    Throw New Exception("Please provide New Program Code")
                End If
                qry = "select SNo  from tspl_Program_MAster where Type='SM' and Program_Code='" + txtSubModule.Value + "'"
                Dim strParSNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, tran)) + "." + txtCustomiseSno.Text
                qry = lblProgramCode.Text
                Dim strOrgProgCode As String = ""
                Do
                    strOrgProgCode = qry
                    qry = "select Program_Code_Original from tspl_Program_MAster where Program_Code='" + strOrgProgCode + "'"
                    qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, tran))

                Loop While clsCommon.myLen(qry) > 0

                qry = "select * from tspl_Program_MAster where Program_Code='" + lblProgramCode.Text + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Invalid Program Code [" + lblProgramCode.Text + "]")
                End If
                ProgramCodeNew.InsertDefaultValue(txtNewProgramCode.Text, txtCaption.Text, strParSNo, txtSubModule.Value, clsCommon.myCstr(dt.Rows(0)("Type")), clsCommon.myCDecimal(dt.Rows(0)("Image_Number")), 0, "", "", "", clsCommon.myCDecimal(dt.Rows(0)("Is_SMS_Applied")), clsCommon.myCDecimal(dt.Rows(0)("Is_EMAIL_Applied")), clsCommon.myCDecimal(dt.Rows(0)("Is_Notification_Applied")), "", tran, clsCommon.myCstr(dt.Rows(0)("ES_Trans_Type_1")), clsCommon.myCstr(dt.Rows(0)("ES_Trans_Type_2")), clsCommon.myCstr(dt.Rows(0)("ES_Trans_Type_3")), clsCommon.myCstr(dt.Rows(0)("ES_Trans_Type_4")), clsCommon.myCstr(dt.Rows(0)("ES_Trans_Type_5")), True, strOrgProgCode)

            Else
                clsDBFuncationality.ExecuteNonQuery(qry, tran)
            End If
            tran.Commit()
            clsCommon.MyMessageBoxShow(Me, "Data Successfully Saved", Me.Text)
            MDI.IsOriginalName = False
            Me.isCancel_Flag = False
            Me.Close()
        Catch ex As Exception
            tran.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Me.isCancel_Flag = True
        Me.Close()
    End Sub



    Private Sub RadButton1_Click_1(sender As Object, e As EventArgs) Handles RadButton1.Click
        If SplitContainer1.Panel2Collapsed Then
            Dim frmPWD As New FrmPWD(Nothing)
            frmPWD.strType = clsFixedParameterType.SettlementBankOnlyPWD
            frmPWD.strCode = clsFixedParameterCode.SettlementBankOnlyPWD
            frmPWD.ShowDialog()
            If frmPWD.isPasswordCorrect Then
                RadButton1.Text = "Hide"
                Me.MaximumSize = New Size(383, 265)
                Me.MinimumSize = New Size(383, 265)
                SplitContainer1.Panel2Collapsed = False
            End If
        Else
            RadButton1.Text = "Advance"
            Me.MaximumSize = New Size(383, 143)
            Me.MinimumSize = New Size(383, 143)
            SplitContainer1.Panel2Collapsed = True
        End If

    End Sub

    Private Sub txtDCS__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtModule._MYValidating
        Try
            Dim qry As String = "select Code,Name from ( select Program_Code as Code,case when Re_Name is null then Program_Name else Re_Name end as Name from tspl_Program_MAster where Type='M')x"
            txtModule.Value = clsCommon.ShowSelectForm("ChanM#F", qry, "Code", "", txtModule.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub TxtFinder1__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSubModule._MYValidating
        Try
            If clsCommon.myLen(txtModule.Value) <= 0 Then
                Throw New Exception("Please select Moudle")
            End If
            Dim qry As String = "select Code,Name from (select Program_Code as Code,case when Re_Name is null then Program_Name else Re_Name end as Name  from tspl_Program_MAster where Type='SM' and Parent_Code='" + txtModule.Value + "')x"
            txtSubModule.Value = clsCommon.ShowSelectForm("ChanSM#F", qry, "Code", "", txtSubModule.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rbtnAddNew_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnAddNew.CheckedChanged
        If rbtnAddNew.Checked Then
            lblNwProgramCode.Visible = True
            txtNewProgramCode.Visible = True
        Else
            lblNwProgramCode.Visible = False
            txtNewProgramCode.Visible = False
        End If
    End Sub
End Class
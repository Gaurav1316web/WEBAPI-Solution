Imports common

Public Class frmMandatoryFieldChecker
    Const Current_Label_Name As String = "Current_Label_Name"
    Const Lable_Id As String = "Lable_Id"
    Const colFormName As String = "Form_Name"
    Const colNew_Label_Name As String = "New_Label_Name"
    Const Is_Reset As String = "Reset"


    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsClientFormLableDetails
    Private ObjList As New List(Of clsClientFormLableDetails)
    Private isCellValueChangedOpen As Boolean = False
    Public formnam As FrmMainTranScreen
    Public formcode As String

    Dim dtLabelData As DataTable = Nothing


    Private Sub frmMandatoryFieldChecker_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        ' LoadModuleType()
        CheckmandatoryField(formnam)
        Me.CenterToParent()
        'Dim ds As New DataSet
        'ds.Tables.Add(dt)
        'gvLabelSetting.DataSource = ds.Tables(0)
    End Sub



    Public Function CheckmandatoryField(ByRef formname As FrmMainTranScreen, Optional ByVal contrl As Control = Nothing)
        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If ctrl.HasChildren = True Then
                    If CheckmandatoryField(Me, ctrl) = False Then
                        Return False
                    End If
                End If

                If TypeOf (ctrl) Is common.UserControls.txtFinder Then
                    Try
                        Dim cttrl = CType(ctrl, common.UserControls.txtFinder)
                        If cttrl.MendatroryField And cttrl.Visible And cttrl.Value = "" Then
                            Return False
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try

                ElseIf TypeOf (ctrl) Is common.UserControls.txtNavigator Then
                    Try
                        Dim cttrl = CType(ctrl, common.UserControls.txtNavigator)
                        If cttrl.MendatroryField And cttrl.Visible And cttrl.Value = "" Then
                            Return False
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try

                ElseIf TypeOf (ctrl) Is common.Controls.MyTextBox Then
                    Try
                        Dim cttrl = CType(ctrl, common.Controls.MyTextBox)
                        If cttrl.MendatroryField And cttrl.Visible And cttrl.Text = "" Then
                            Return False
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                ElseIf TypeOf (ctrl) Is common.Controls.MyComboBox Then
                    Try
                        Dim cttrl = CType(ctrl, common.Controls.MyComboBox)
                        If cttrl.MendatroryField And cttrl.Visible And cttrl.Text = "" Then
                            Return False
                        End If

                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            Next
        Else
            For Each ctrl As Control In contrl.Controls
                If ctrl.HasChildren = True Then
                    If CheckmandatoryField(Me, ctrl) = False Then
                        Return False
                    End If

                End If
                'If ctrl.Name.ToLower.Contains("loc") Then
                '    MessageBox.Show("Get It")
                'End If
                If TypeOf (ctrl) Is common.UserControls.txtFinder Then
                    Try
                        Dim cttrl = CType(ctrl, common.UserControls.txtFinder)
                        If cttrl.MendatroryField And cttrl.visible And cttrl.Value = "" Then
                            Return False
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try

                ElseIf TypeOf (ctrl) Is common.UserControls.txtNavigator Then
                    Try
                        Dim cttrl = CType(ctrl, common.UserControls.txtNavigator)
                        If cttrl.MendatroryField And cttrl.visible And cttrl.Value = "" Then
                            Return False
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try

                ElseIf TypeOf (ctrl) Is common.Controls.MyTextBox Then
                    Try
                        Dim cttrl = CType(ctrl, common.Controls.MyTextBox)
                        If cttrl.MendatroryField And cttrl.visible And cttrl.text = "" Then
                            Return False
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                ElseIf TypeOf (ctrl) Is common.Controls.MyComboBox Then
                    Try
                        Dim cttrl = CType(ctrl, common.Controls.MyComboBox)
                        If cttrl.MendatroryField And cttrl.visible And cttrl.text = "" Then
                            Return False
                        End If

                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            Next
        End If
        Return True
        'gvLabelSetting.DataSource = dt
    End Function

End Class
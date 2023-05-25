''  work done against ticket no. TEC/14/01/19-000395
Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls.UI

Public Class frmSetting
#Region "variables"
    Public strFormID As String = ""
    Public isDataSaved As Boolean = False
#End Region
    Private Sub FrmCheckBoxGrid_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim qry As String = "select is_sms_applied,is_email_applied,is_notification_applied from tspl_program_master where program_code='" + strFormID + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                chkSMS.Checked = (clsCommon.myCdbl(dt.Rows(0)("is_sms_applied")) = 1)
                chkEMail.Checked = (clsCommon.myCdbl(dt.Rows(0)("is_email_applied")) = 1)
                chkNotification.Checked = (clsCommon.myCdbl(dt.Rows(0)("is_notification_applied")) = 1)

            End If
            qry = "select " + Environment.NewLine +
        " case when TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.Control_Type=0 then cast(TSPL_FIXED_PARAMETER.Description as bit)  else cast(0 as bit) end as CheckBox," + Environment.NewLine +
        " case when TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.Control_Type=1 then  TSPL_FIXED_PARAMETER.Description else null  end as TextBox," + Environment.NewLine +
        " case when TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.Control_Type=2 then  cast( TSPL_FIXED_PARAMETER.Description as Decimal(18,2)) else 0 end as NumBox,TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Type,TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Code,TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.Control_Type ,TSPL_FIXED_PARAMETER.Specification " + Environment.NewLine +
        " from TSPL_FIXED_PARAMETER_PROGRAM_MAPPING " + Environment.NewLine +
        " left outer join TSPL_FIXED_PARAMETER on TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Type =TSPL_FIXED_PARAMETER.Type and TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Code=TSPL_FIXED_PARAMETER.Code" + Environment.NewLine +
        " where TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.program_code='" + strFormID + "'"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry)
                gv.DataSource = dt
                SetGridFormat()
            Else
                clsCommon.MyMessageBoxShow("No Setting found")
                btnCancelPressed()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    

    Sub SetGridFormat()
        gv.Columns("FP_Type").ReadOnly = True
        gv.Columns("FP_Type").HeaderText = "Type"
        gv.Columns("FP_Type").Width = 200

        gv.Columns("FP_Code").ReadOnly = True
        gv.Columns("FP_Code").HeaderText = "Code"
        gv.Columns("FP_Code").Width = 200

        gv.Columns("Control_Type").ReadOnly = True
        gv.Columns("Control_Type").HeaderText = "Type"
        gv.Columns("Control_Type").IsVisible = False

        gv.Columns("CheckBox").ReadOnly = False
        gv.Columns("CheckBox").HeaderText = "Check Box"
        gv.Columns("CheckBox").Width = 100
        gv.Columns("CheckBox").IsVisible = False

        gv.Columns("TextBox").ReadOnly = False
        gv.Columns("TextBox").HeaderText = "Text Box"
        gv.Columns("TextBox").Width = 100
        gv.Columns("TextBox").IsVisible = False

        gv.Columns("NumBox").ReadOnly = False
        gv.Columns("NumBox").HeaderText = "Numeric Box"
        gv.Columns("NumBox").Width = 100
        gv.Columns("NumBox").IsVisible = False

        gv.Columns("Specification").ReadOnly = True
        gv.Columns("Specification").HeaderText = "Specification"
        gv.Columns("Specification").Width = 200
        gv.Columns("Specification").IsVisible = True

      
        For ii As Integer = 0 To gv.Rows.Count - 1
            If clsCommon.myCdbl(gv.Rows(ii).Cells("Control_Type").Value) = 0 Then
                gv.Columns("CheckBox").IsVisible = True
            ElseIf clsCommon.myCdbl(gv.Rows(ii).Cells("Control_Type").Value) = 1 Then
                gv.Columns("TextBox").IsVisible = True
            ElseIf clsCommon.myCdbl(gv.Rows(ii).Cells("Control_Type").Value) = 2 Then
                gv.Columns("NumBox").IsVisible = True
            End If
        Next


        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.MasterTemplate.ShowColumnHeaders = True
        gv.EnableAlternatingRowColor = True
        gv.EnableFiltering = True
        gv.ShowFilteringRow = True
        gv.TableElement.TableHeaderHeight = 30
    End Sub

    Private Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        btnOkPressed()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnCancelPressed()
    End Sub

    Private Sub FrmCheckBoxGrid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOkPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancelPressed()
        End If
    End Sub

    Sub btnCancelPressed()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Sub btnOkPressed()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            For Each grow As GridViewRowInfo In gv.Rows
                Dim obj As clsFixedParameter = New clsFixedParameter()
                obj.Code = clsCommon.myCstr(grow.Cells("FP_Code").Value)
                obj.Type = clsCommon.myCstr(grow.Cells("FP_Type").Value)
                obj.Specification = clsCommon.myCstr(grow.Cells("Specification").Value)

                Dim CheckObjectValue As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where code='" + obj.Code + "' and Type='" + obj.Type + "'", trans))

                If clsCommon.myCdbl(grow.Cells("Control_Type").Value) = 0 Then
                    obj.Description = IIf(clsCommon.myCBool(grow.Cells("CheckBox").Value) = True, "1", "0")

                ElseIf clsCommon.myCdbl(grow.Cells("Control_Type").Value) = 1 Then
                    obj.Description = clsCommon.myCstr(grow.Cells("TextBox").Value)
                    'obj.Description = clsCommon.myCstr(grow.Cells("TextBox").Tag)
                ElseIf clsCommon.myCdbl(grow.Cells("Control_Type").Value) = 2 Then
                    obj.Description = clsCommon.myCdbl(grow.Cells("NumBox").Value)
                End If

                If clsCommon.CompairString(obj.Description, CheckObjectValue) = CompairStringResult.Equal Then
                Else
                    clsFixedParameter.UpdateFixedParameter(obj, trans, False)
                End If


            Next
            isDataSaved = True
            trans.Commit()
            objCommonVar.RefreshCommonVar()
            clsERPFuncationality.closeForm(Me)
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        clsERPFuncationalityOLD.ShowHistoryData(gv.CurrentRow.Cells("FP_Type").Value, "Type", "TSPL_FIXED_PARAMETER", "", Nothing, "Code", gv.CurrentRow.Cells("FP_Code").Value)

    End Sub


    Private Sub gv_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv.CellFormatting
        Try
            Try
                If e.Row.Index >= 0 Then
                    If e.Column Is gv.Columns("CheckBox") Then
                        If clsCommon.myCdbl(gv.CurrentRow.Cells("Control_Type").Value) = 0 Then
                            gv.CurrentRow.Cells("CheckBox").ReadOnly = False
                            gv.CurrentRow.Cells("TextBox").ReadOnly = True
                            gv.CurrentRow.Cells("NumBox").ReadOnly = True
                        End If
                    ElseIf e.Column Is gv.Columns("TextBox") Then
                        If clsCommon.myCdbl(gv.CurrentRow.Cells("Control_Type").Value) = 1 Then
                            gv.CurrentRow.Cells("CheckBox").ReadOnly = True
                            gv.CurrentRow.Cells("TextBox").ReadOnly = False
                            gv.CurrentRow.Cells("NumBox").ReadOnly = True
                        End If
                    ElseIf e.Column Is gv.Columns("NumBox") Then
                        If clsCommon.myCdbl(gv.CurrentRow.Cells("Control_Type").Value) = 2 Then
                            gv.CurrentRow.Cells("CheckBox").ReadOnly = True
                            gv.CurrentRow.Cells("TextBox").ReadOnly = True
                            gv.CurrentRow.Cells("NumBox").ReadOnly = False
                        End If
                    End If
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

  
    Private Sub gv_ValueChanging(sender As Object, e As ValueChangingEventArgs) Handles gv.ValueChanging
        ' If e.NewValue Then
        '  btnOkPressed()
        'End If
    End Sub
End Class

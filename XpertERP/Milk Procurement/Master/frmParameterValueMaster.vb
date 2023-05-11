Imports common

Imports System.Data.SqlClient
Imports System.IO


Public Class FrmParameterValueMaster
    Inherits FrmMainTranScreen
    Public Const colSlNo As String = "SLNO"
    Public Const colValue As String = "Value"
    Public Const colSpecification As String = "Specification"
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Sub loadBlankGv()
        Try
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.Columns.Add(colSlNo, "SL.NO")
            gv.Columns.Add(colValue, "Value")
            gv.Columns.Add(colSpecification, "Specification")
            gv.Columns(colSlNo).Width = 100
            gv.Columns(colSlNo).ReadOnly = True
            gv.Columns(colValue).Width = 250
            gv.Columns(colSpecification).Width = 500
            gv.AllowAddNewRow = True
            gv.AddNewRowPosition = SystemRowPosition.Bottom
            gv.AllowEditRow = True
            gv.AllowDeleteRow = True
            gv.AllowRowResize = False
            gv.AllowRowReorder = False
            gv.AllowColumnResize = False
            gv.AllowColumnChooser = False
            gv.AllowAutoSizeColumns = False
            gv.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub reset()
        LoadParameters()
        ddlParamCode.Text = ""
        txtParamDesc.Text = ""
        loadBlankGv()
        btnSave.Text = "Save"
        btnDelete.Enabled = True

    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.ParameterValueMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmParameterValueMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            btnSave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmParameterValueMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub
    Function allowToSave() As Boolean
        Dim rowno As Integer = -1
        rowno = chkDuplicateValue()
        If rowno > -1 Then
            clsCommon.MyMessageBoxShow("Duplicate value at Row no. " & (rowno + 1))
            Return False
        End If
        Return True
    End Function

    Sub SaveData()
        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.ParameterValueMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            Dim obj As clsParameterValueMaster = Nothing
            Dim arrObj As New List(Of clsParameterValueMaster)
            For i As Integer = 0 To gv.Rows.Count - 1
                If clsCommon.myLen(gv.Rows(i).Cells(colValue).Value) > 0 Then
                    obj = New clsParameterValueMaster()
                    obj.Parameter_CODE = clsCommon.myCstr(ddlParamCode.Text)
                    obj.Value = clsCommon.myCstr(gv.Rows(i).Cells(colValue).Value)
                    obj.Specification = clsCommon.myCstr(gv.Rows(i).Cells(colSpecification).Value)
                    arrObj.Add(obj)
                End If
            Next
            If arrObj.Count > 0 Then
                If clsParameterValueMaster.SaveData(arrObj) Then
                    LoadParameterValues()
                    myMessages.insert()
                End If
            Else
                Throw New Exception("No value Found To save")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub deleteData()
        Try
            If clsCommon.myLen(ddlParamCode.Text) > 0 Then
                If myMessages.deleteConfirm() Then
                    If clsParameterValueMaster.DeleteData(ddlParamCode.Text) Then
                        LoadParameterValues()
                        myMessages.delete()
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Function chkDuplicateValue() As Integer
        Dim strValue As String = String.Empty
        For i As Integer = 0 To gv.Rows.Count - 2
            strValue = gv.Rows(i).Cells(colValue).Value.ToString
            For j As Integer = i + 1 To gv.Rows.Count - 1
                If clsCommon.CompairString(gv.Rows(j).Cells(colValue).Value, strValue) = CompairStringResult.Equal Then
                    Return j
                End If
            Next
        Next
        Return -1
    End Function
    Sub SetSerialNo()
        For i As Integer = 0 To gv.Rows.Count - 1
            gv.Rows(i).Cells(colSlNo).Value = (i + 1)
        Next
    End Sub

    Private Sub gv_UserAddedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv.UserAddedRow
        SetSerialNo()
    End Sub

    Private Sub gv_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv.UserDeletedRow
        SetSerialNo()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If allowToSave() Then SaveData()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        deleteData()
    End Sub
    Sub LoadParameters()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select distinct code,Description from (select code,Description from TSPL_PARAMETER_MASTER where Nature='A' union all select code,Description from TSPL_QC_LOG_SHEET_MASTER where Nature='A')ax")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            ddlParamCode.DataSource = dt
            ddlParamCode.DisplayMember = "code"
            ddlParamCode.ValueMember = "code"
        End If
    End Sub
    Sub LoadParameterValues()
        loadBlankGv()
        If clsCommon.myLen(ddlParamCode.Text) > 0 Then
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Value,isnull(Specification,'') as Specification from tspl_Parameter_value_master where parameter_code='" & ddlParamCode.Text & "'")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                loadBlankGv()
                For i As Integer = 0 To dt.Rows.Count - 1
                    gv.Rows.AddNew()
                    gv.Rows(i).Cells(colSlNo).Value = (i + 1)
                    gv.Rows(i).Cells(colValue).Value = dt.Rows(i)("Value").ToString
                    gv.Rows(i).Cells(colSpecification).Value = dt.Rows(i)("Specification").ToString
                Next
            End If
        End If
    End Sub

    Private Sub ddlParamCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlParamCode.TextChanged
        If clsCommon.myLen(ddlParamCode.Text) > 0 Then
            txtParamDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Description  from TSPL_PARAMETER_MASTER where Code='" & ddlParamCode.Text & "'"))
            If clsCommon.myLen(txtParamDesc.Text) <= 0 Then
                txtParamDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Description  from TSPL_QC_LOG_SHEET_MASTER where Code='" & ddlParamCode.Text & "'"))
            End If
            LoadParameterValues()
        End If
    End Sub
End Class

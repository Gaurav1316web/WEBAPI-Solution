Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class frmGeneratePaymentCycle
    Inherits FrmMainTranScreen

#Region "Variable"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Public Sub Save()
        Try
            If AllowToSave() Then
                clsGenratePaymentCycles.SaveData(txtFiscalYear.Value, txtMCC.arrValueMember)
                clsCommon.MyMessageBoxShow(Me, "Payment Cycles Generated Successfully", Me.Text)
                LoadData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData()
        Try
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            Dim qry As String = "select TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_PAYMENT_CYCLE_GENERATED.Code,TSPL_PAYMENT_CYCLE_GENERATED.Name,TSPL_PAYMENT_CYCLE_GENERATED.Fiscal_Code,convert(varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) as From_Date,convert(varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date ,103) as To_Date 
            from TSPL_PAYMENT_CYCLE_GENERATED 
            left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code WHERE TSPL_PAYMENT_CYCLE_GENERATED.Fiscal_Code='" + txtFiscalYear.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") order by MCC_Code,Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                gv1.DataSource = dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                    gv1.Columns(ii).IsVisible = False
                Next
                gv1.ShowGroupPanel = False
                gv1.ShowFilteringRow = True
                gv1.Columns("MCC_Code").IsVisible = True
                gv1.Columns("MCC_Code").Width = 100
                gv1.Columns("MCC_Code").HeaderText = "MCC Code"

                gv1.Columns("MCC_NAME").IsVisible = True
                gv1.Columns("MCC_NAME").Width = 100
                gv1.Columns("MCC_NAME").HeaderText = "MCC"

                gv1.Columns("Code").HeaderText = "Code"

                gv1.Columns("Name").IsVisible = True
                gv1.Columns("Name").Width = 100
                gv1.Columns("Name").HeaderText = "Cycle No"

                gv1.Columns("Fiscal_Code").IsVisible = True
                gv1.Columns("Fiscal_Code").Width = 100
                gv1.Columns("Fiscal_Code").HeaderText = "Fiscal Code"

                gv1.Columns("From_Date").IsVisible = True
                gv1.Columns("From_Date").Width = 100
                gv1.Columns("From_Date").HeaderText = "From Date"

                gv1.Columns("To_Date").IsVisible = True
                gv1.Columns("To_Date").Width = 100
                gv1.Columns("To_Date").HeaderText = "To Date"
            Else
                Throw New Exception("No Data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
            txtFiscalYear.Focus()
            Throw New Exception("Please select Fiscal Year")
        ElseIf txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
            txtMCC.Focus()
            Throw New Exception("Please select at lease one MCC")
        End If
        Return True
    End Function

    Private Sub frmPaymentCycleMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        funReset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmPaymentCycleMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funClose()
    End Sub

    Private Sub txtFiscalYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Dim qry As String = "select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER"
        txtFiscalYear.Value = clsCommon.ShowSelectForm("GPC", qry, "Fiscal_Code", "", txtFiscalYear.Value, "", isButtonClicked)
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where 2=2"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("GPC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        LoadData()
    End Sub
End Class

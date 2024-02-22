Imports common
Imports System.Data.SqlClient

Public Class FrmDBTPayment
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonTooltip As New ToolTip()
    Dim Is_Load As Boolean = False
    Dim AllowDateChanged As Boolean = False
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmMilkVSPPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Is_Load = True
        ButtonTooltip.SetToolTip(btnClose, "Press Alt+C for Close the Window")
        ButtonTooltip.SetToolTip(btnGenerateBill, "Press Alt+R for Refresh the Data")
        SetUserMgmtNew()

        txtMonth.Value = clsCommon.GETSERVERDATE()

        Is_Load = False
        AllowDateChanged = True

        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.isDeleteTheAttachment = False
        UcAttachment1.settAutoAttachment = True
    End Sub
    Private Sub txtUnion__My_Click(sender As Object, e As EventArgs) Handles txtUnion._My_Click
        Dim qry As String = "select  DataBase_Name as Code from TSPL_MASTER.dbo.TSPL_APP_LOCATION order by DataBase_Name"
        txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("dbDBTPa23", qry, "Code", "", txtUnion.arrValueMember, Nothing)
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnGenerateBill.Visible = MyBase.isPostFlag

    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            Dim qry As String = "select CAST(0 as Bit) as Sel,PK_Id,DB_Name,Document_Code,Document_Date,From_Date,To_Date 
from TSPL_DBT_NEFT_RCDF 
where isnull([Status],0)=0 "
            If txtUnion.arrValueMember IsNot Nothing AndAlso txtUnion.arrValueMember.Count > 0 Then
                qry += "and [DB_NAME] in (" + clsCommon.GetMulcallString(txtUnion.arrValueMember) + ")"
            End If
            If txtMonth.Checked Then
                qry += "and DATEPART(month, From_Date)=" + clsCommon.myCstr(txtMonth.Value.Month) + " and DATEPART(YEAR, From_Date)=" + clsCommon.myCstr(txtMonth.Value.Year) + " "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gvDetail.DataSource = Nothing
            gvDetail.DataSource = dt
            gvDetail.BestFitColumns()
            'gvDetail.ReadOnly = True
            SetGridFormationOFGV()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormationOFGV()
        gvDetail.ShowGroupPanel = False
        gvDetail.GroupDescriptors.Clear()
        gvDetail.AllowAddNewRow = False
        gvDetail.TableElement.TableHeaderHeight = 40
        gvDetail.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvDetail.Columns.Count - 1
            gvDetail.Columns(ii).ReadOnly = True
            gvDetail.Columns(ii).IsVisible = False
        Next

        gvDetail.Columns("Sel").IsVisible = True
        gvDetail.Columns("Sel").Width = 30
        gvDetail.Columns("Sel").HeaderText = " "
        gvDetail.Columns("Sel").ReadOnly = False

        gvDetail.Columns("PK_Id").HeaderText = "PK ID"
        gvDetail.Columns("PK_Id").IsVisible = False

        gvDetail.Columns("DB_Name").IsVisible = True
        gvDetail.Columns("DB_Name").Width = 100
        gvDetail.Columns("DB_Name").HeaderText = "Union"

        gvDetail.Columns("Document_Code").IsVisible = True
        gvDetail.Columns("Document_Code").Width = 150
        gvDetail.Columns("Document_Code").HeaderText = "DBT NEFT No"

        gvDetail.Columns("Document_Date").IsVisible = True
        gvDetail.Columns("Document_Date").Width = 100
        gvDetail.Columns("Document_Date").HeaderText = "DBT NEFT Date"
        gvDetail.Columns("Document_Date").FormatString = "{0:dd/MM/yyyy}"

        gvDetail.Columns("From_Date").IsVisible = True
        gvDetail.Columns("From_Date").Width = 100
        gvDetail.Columns("From_Date").HeaderText = "From Date"
        gvDetail.Columns("From_Date").FormatString = "{0:dd/MM/yyyy}"

        gvDetail.Columns("To_Date").IsVisible = True
        gvDetail.Columns("To_Date").Width = 100
        gvDetail.Columns("To_Date").HeaderText = "To Date"
        gvDetail.Columns("To_Date").FormatString = "{0:dd/MM/yyyy}"
    End Sub

    Private Sub btnGenerateBill_Click(sender As Object, e As EventArgs) Handles btnGenerateBill.Click
        Try
            Dim arr As New List(Of Integer)
            For ii As Integer = 0 To gvDetail.Rows.Count - 1
                If clsCommon.myCBool(gvDetail.Rows(ii).Cells("Sel").Value) Then
                    arr.Add(clsCommon.myCDecimal(gvDetail.Rows(ii).Cells("PK_Id").Value))
                End If
            Next
            If arr.Count > 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Approve " + clsCommon.myCstr(arr.Count) + " Documents" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                    clsDBTNEFT.PostDataRCDF(arr)
                    clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully", Me.Text)
                    LoadData()
                End If
            Else
                Throw New Exception("Please select at least one document.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Private Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub FrmDBTPayment_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub gvDetail_DoubleClick(sender As Object, e As EventArgs) Handles gvDetail.DoubleClick
        Try
            Dim strCode As String = clsCommon.myCstr(gvDetail.CurrentRow.Cells("PK_ID").Value)
            If clsCommon.myLen(strCode) > 0 Then
                UcAttachment1.BlankAllControls()
                UcAttachment1.LoadData(strCode)
                GroupBox1.Visible = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        GroupBox1.Visible = False
    End Sub
End Class

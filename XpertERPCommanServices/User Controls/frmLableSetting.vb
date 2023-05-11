Imports common

Public Class frmLableSetting
#Region "Variable"
    Public chqSetVar As ChequeSettingVar
    Public caption As String = Nothing
    Public fontName As String = Nothing
    Public Mytop As Double = 0
    Public Myleft As Double = 0
    Public Mywidth As Double = 0
    Public Myheight As Double = 0
    Public charPerLine As Integer = 30
    Public decimalPlace As Integer = 0
    Public fontSize As Double = 0
    Public isBold As Boolean = False
    Public isVisible As Integer = 1
    Public DateStyle As String = clsDateStyle.ddMMMyyyy
    Public isxxxxxxBefore As Boolean = False
    Public isxxxxxxAfter As Boolean = False
    Public CharSpace As Integer = 0
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub aisButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles aisButton1.Click
        caption = txtCaption.Text
        charPerLine = CInt(txtChrpln.Value)
        decimalPlace = CInt(txtDec.Value)
        fontName = txtFontName.Text
        fontSize = txtSize.Value
        Myheight = txtHeight.Value
        Myleft = txtLeft.Value
        Mytop = txtTop.Value
        Mywidth = txtWidth.Value
        isBold = chkBold.Checked
        isxxxxxxBefore = chkBeforexxxx.Checked
        isxxxxxxAfter = chkAfterxxxx.Checked
        CharSpace = clsCommon.myCdbl(cboCharSpace.SelectedValue)
        isVisible = If((chkIsVisible.Checked), 1, 0)

        If clsCommon.myLen(clsCommon.myCstr(cboDateStyle.SelectedValue)) = 0 Then
            DateStyle = clsDateStyle.ddMMMyyyy_Slash
        Else
            DateStyle = clsCommon.myCstr(cboDateStyle.SelectedValue)
        End If

        Me.Close()
    End Sub

    Public Sub enableDesible()
        txtCaption.Enabled = False
        cboDateStyle.Enabled = False
        chkIsVisible.Enabled = False
        cboCharSpace.Visible = False
        MyLabel1.Visible = False
        Select Case chqSetVar
            Case ChequeSettingVar.ForCompany, ChequeSettingVar.AccountNo, ChequeSettingVar.Sign
                txtCaption.Enabled = True
                chkIsVisible.Enabled = True
                Exit Select
            Case ChequeSettingVar.NotOver
                chkIsVisible.Enabled = True
                Exit Select
            Case ChequeSettingVar.[Date]
                cboDateStyle.Enabled = True
                cboCharSpace.Visible = True
                MyLabel1.Visible = True
                Exit Select
            Case Else
                Exit Select
        End Select
    End Sub

    Private Sub frmLableSetting_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        fillDateStyleData()
        fillCharSpace()
        enableDesible()

        txtCaption.Text = caption
        txtChrpln.Value = charPerLine
        txtDec.Value = decimalPlace
        txtFontName.Text = fontName
        txtSize.Value = fontSize
        txtHeight.Value = Myheight
        txtLeft.Value = Myleft
        txtTop.Value = Mytop
        txtWidth.Value = Mywidth
        chkBold.Checked = isBold
        chkIsVisible.Checked = If((isVisible = 1), True, False)

        'txtCaption.Enabled = false;
        cboDateStyle.SelectedValue = DateStyle

        chkBeforexxxx.Checked = isxxxxxxBefore
        chkAfterxxxx.Checked = isxxxxxxAfter
        cboCharSpace.SelectedValue = CharSpace
    End Sub

    Public Sub fillCharSpace()
        Dim selVal As String = clsCommon.myCstr(cboCharSpace.SelectedValue)

        Dim tbl As New DataTable("DateStyle")
        tbl.Columns.Add(New DataColumn("Code", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("Name", GetType(String)))
        Dim dr As DataRow = tbl.NewRow()
        dr("Code") = 0
        dr("Name") = ""
        tbl.Rows.Add(dr)
        For ii As Integer = 1 To 10
            dr = tbl.NewRow()
            dr("Code") = ii
            dr("Name") = clsCommon.myCstr(ii)
            tbl.Rows.Add(dr)
        Next


        cboCharSpace.DataSource = tbl
        cboCharSpace.DisplayMember = "Name"
        cboCharSpace.ValueMember = "Code"

        If clsCommon.myLen(selVal) > 0 Then
            cboCharSpace.SelectedValue = selVal
        Else
            cboCharSpace.SelectedIndex = -1
        End If
    End Sub

    Public Sub fillDateStyleData()
        Dim selVal As String = clsCommon.myCstr(cboDateStyle.SelectedValue)

        Dim ds As New DataSet()
        ds = clsLableSetting.getDateStyleDataSet()
        cboDateStyle.DataSource = ds.Tables(0)
        cboDateStyle.DisplayMember = "Name"
        cboDateStyle.ValueMember = "Code"

        If clsCommon.myLen(selVal) > 0 Then
            cboDateStyle.SelectedValue = selVal
        Else
            cboDateStyle.SelectedIndex = -1
        End If
    End Sub

    Private Sub aisButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles aisButton2.Click
        Me.Close()
    End Sub

    Private Sub txtFontName_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles txtFontName.Enter
        Dim fontDialog As New FontDialog()
        If txtSize.Value > 0 AndAlso clsCommon.myLen(txtFontName.Text) > 0 Then
            Try
                Dim bold As FontStyle = FontStyle.Regular
                If isBold Then
                    bold = FontStyle.Bold
                End If
                fontDialog.Font = New Font(txtFontName.Text, CSng(txtSize.Value), bold)
            Catch
            End Try
        End If
        If fontDialog.ShowDialog() = DialogResult.OK Then
            txtFontName.Text = fontDialog.Font.Name
            txtSize.Value = fontDialog.Font.Size
            chkBold.Checked = fontDialog.Font.Bold
        End If
    End Sub

    Private Sub cboDateStyle_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles cboDateStyle.Enter
        fillDateStyleData()
    End Sub


    Private Sub cboCharSpace_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCharSpace.Enter
        fillCharSpace()
    End Sub
End Class

Imports System
Imports System.Data.SqlClient
Imports common
Imports XpertERPEngine
'DEVELOPED by Abhishek kumar
'Created date - 5/june/2012
' End Date = 5/June/2012
'--preeti gupta-ticket no.[BM00000003133]
Public Class FrmViewPunchingInvoice
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As New DataTable()
    Dim obj As New ClsViewPunchingInvoice()
    Private isNewEntry As Boolean = False
    Dim QRY As String
    Dim userCode, companyCode As String
    Private isInsideLoadData As Boolean = False
    Public PunchedInvoice As Double = 0
    Public Balance As Double = 0

    Private Sub FrmViewPunchingInvoice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            ' Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub FrmViewPunchingInvoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fndTransferNo.MyMaxLength = 30

        AddNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        'ButtonToolTip.SetToolTip(btnDelet, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")



        SetUserMgmtNew()
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmViewPunchingInvoice)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub BlankControl()
        fndTransferNo.Value = Nothing
        txtNoOfCashMemo.Value = 0
        lblPunchedInvoice.Text = 0
        lblBalance.Text = 0

    End Sub
    Sub AddNew()
        BlankControl()
        fndTransferNo.Focus()
        fndTransferNo.MyReadOnly = False
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(fndTransferNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Transfer NO")
            fndTransferNo.Focus()
            Return False
        End If

        If (txtNoOfCashMemo.Value) = 0 Then
            common.clsCommon.MyMessageBoxShow("Please Enter No Of Cash Memo")
            txtNoOfCashMemo.Focus()
            Return False
        End If
        Return True
    End Function

    Sub SaveData()

        If clsCommon.myLen(fndTransferNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Transfer NO")
            fndTransferNo.Focus()
            Exit Sub
        End If

        Try
            If (AllowToSave()) Then
                Dim obj As New ClsViewPunchingInvoice()
                obj.TransferNo = (fndTransferNo.Value).ToString
                obj.NoOfCashMemo = clsCommon.myCdbl(txtNoOfCashMemo.Text)
                PunchedInvoice = GetNoOfInvoice(fndTransferNo.Value)
                lblPunchedInvoice.Text = PunchedInvoice
                Balance = clsCommon.myCdbl(txtNoOfCashMemo.Value) - clsCommon.myCdbl(PunchedInvoice)
                lblBalance.Text = Balance
                If (obj.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.TransferNo, NavigatorType.Current)

                Else

                End If
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal TransferNo As String, ByVal navType As common.NavigatorType)
        Try
            btnSave.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            ' btnSave.Text = "Update"
            BlankControl()
            fndTransferNo.MyReadOnly = True
            Dim obj As New ClsViewPunchingInvoice()
            obj = ClsViewPunchingInvoice.GetData(TransferNo, navType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.TransferNo) > 0) Then
                fndTransferNo.Value = obj.TransferNo
                txtNoOfCashMemo.Value = obj.NoOfCashMemo
                lblPunchedInvoice.Text = obj.PunchedInvoice
                lblBalance.Text = obj.Balance
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub fndTransferNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTransferNo._MYValidating
        If fndTransferNo.Value <> "" Then
            fndTransferNo.MyReadOnly = True
        End If

        If fndTransferNo.MyReadOnly Or isButtonClicked Then
            Dim qry As String = "select Transfer_No as [Code] ,Transfer_Date  from TSPL_TRANSFER_HEAD  "
            Dim whrcls As String = "Post ='Y' and To_Location in (select Location_Code  from TSPL_LOCATION_MASTER where Location_Type ='Logical') "
            fndTransferNo.Value = clsCommon.ShowSelectForm("ViewOfCashFND", qry, "Code", whrcls, fndTransferNo.Value, "Code", isButtonClicked)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select NoOfCashMemo  from TSPL_ViewPunchingInvoice  where TransferNO ='" + fndTransferNo.Value + "'")
            For Each row As DataRow In dt.Rows
                isNewEntry = False
                txtNoOfCashMemo.Value = row("NoOfCashMemo").ToString()
            Next
            PunchedInvoice = GetNoOfInvoice(fndTransferNo.Value)
            lblPunchedInvoice.Text = PunchedInvoice
            Balance = clsCommon.myCdbl(txtNoOfCashMemo.Value) - clsCommon.myCdbl(PunchedInvoice)
            lblBalance.Text = Balance
        End If
    End Sub

    Private Sub fndTransferNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndTransferNo._MYNavigator
        Try
            LoadData(fndTransferNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndTransferNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndTransferNo.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Public Function GetNoOfInvoice(ByVal TransferNo As String) As Integer
        Dim qry As String = "select Count(Sale_Invoice_No)as NoOfInvoice   from TSPL_SALE_INVOICE_HEAD inner join TSPL_SHIPMENT_MASTER on TSPL_SALE_INVOICE_HEAD .Shipment_No =TSPL_SHIPMENT_MASTER .Shipment_No inner join TSPL_TRANSFER_HEAD On TSPL_SHIPMENT_MASTER .Transfer_No  =TSPL_TRANSFER_HEAD .Transfer_No where TSPL_TRANSFER_HEAD.Transfer_No ='" & TransferNo & "' "
        Dim NoOfInvoice As Integer = clsDBFuncationality.getSingleValue(qry)
        Return NoOfInvoice
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        AddNew()
    End Sub

    Private Sub txtNoOfCashMemo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNoOfCashMemo.Leave
        If txtNoOfCashMemo.Value > 0 Then
            PunchedInvoice = GetNoOfInvoice(fndTransferNo.Value)
            lblPunchedInvoice.Text = PunchedInvoice
            Balance = clsCommon.myCdbl(txtNoOfCashMemo.Value) - clsCommon.myCdbl(PunchedInvoice)
            lblBalance.Text = Balance
        End If
    End Sub
End Class

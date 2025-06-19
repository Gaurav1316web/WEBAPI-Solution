Imports common
Imports XpertERPEngine
Public Class frmMakeSavingPaymentSelectDocs
#Region "Varibales"
    Public strDCSUploaderNo As String = ""
    Public strDCSName As String = ""
    Public InType As Integer = 0
    Public IsCancelClicked As Boolean = True

    Public ArrInSaving As List(Of clsMakeSavingPaymentSaving) = Nothing
    Public ArrInSale As List(Of clsMakeSavingPaymentDCSSale) = Nothing
    Public ArrInDeduction As List(Of clsMakeSavingPaymentDeduction) = Nothing

    Public ArrOutSaving As List(Of clsMakeSavingPaymentSaving) = Nothing
    Public ArrOutSale As List(Of clsMakeSavingPaymentDCSSale) = Nothing
    Public ArrOutDeduction As List(Of clsMakeSavingPaymentDeduction) = Nothing

    Const colSelect As String = "colSelect"
    Const colDocNo As String = "colDocNo"
    Const colDocDate As String = "colDocDate"
    Const colAmount As String = "colAmount"
    Public colReduceDedAmt As String = "colReduceDedAmt"
#End Region
    Private Sub FrmPOItemTaxDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblItemName.Text = strDCSUploaderNo + " [ " + strDCSName + " ]"
        If Not (InType = 1 Or InType = 2 OrElse InType = 3) Then
            Throw New Exception("Invalid InType")
            Me.Close()
        End If
        LoadBlankGrid()
        LoadData()
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        If InType = 2 OrElse InType = 3 Then
        End If

        Dim repoCheckBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Select"
        repoCheckBox.Name = colSelect
        repoCheckBox.Width = 80
        repoCheckBox.ReadOnly = False
        repoCheckBox.IsVisible = True
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoCheckBox)


        Dim repoTaxAuthCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthCode.FormatString = ""
        repoTaxAuthCode.HeaderText = "Document No"
        repoTaxAuthCode.Name = colDocNo
        repoTaxAuthCode.Width = 150
        repoTaxAuthCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTaxAuthCode)



        Dim repoExpiry As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoExpiry.Format = DateTimePickerFormat.Custom
        repoExpiry.CustomFormat = "dd-MM-yyyy"
        repoExpiry.HeaderText = "Document Date"
        repoExpiry.FormatString = "{0:d}"
        repoExpiry.Name = colDocDate
        repoExpiry.WrapText = True
        repoExpiry.ReadOnly = True
        repoExpiry.Width = 100
        gv1.MasterTemplate.Columns.Add(repoExpiry)



        Dim repoTaxBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "Amount"
        repoTaxBaseAmt.Name = colAmount
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        Dim repoTaxRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate.FormatString = ""
        repoTaxRate.HeaderText = "Reduce Deduction"
        repoTaxRate.Name = colReduceDedAmt
        repoTaxRate.Width = 100
        repoTaxRate.ReadOnly = False
        repoTaxRate.IsVisible = (InType = 2 OrElse InType = 3)
        repoTaxRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate)

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub
    Sub LoadData()
        If InType = 1 Then
            If ArrInSaving IsNot Nothing AndAlso ArrInSaving.Count > 0 Then
                For Each obj As clsMakeSavingPaymentSaving In ArrInSaving
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSelect).Value = obj.IsSelect
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDocNo).Value = obj.AP_Invoice_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDocDate).Value = obj.DocDate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = obj.Amount
                Next
            End If
        ElseIf InType = 2 Then
            If ArrInSale IsNot Nothing AndAlso ArrInSale.Count > 0 Then
                For Each obj As clsMakeSavingPaymentDCSSale In ArrInSale
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSelect).Value = obj.IsSelect
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDocNo).Value = obj.AR_Invoice_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDocDate).Value = obj.DocDate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = obj.Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReduceDedAmt).Value = obj.Red_Ded_Amount
                Next
            End If
        ElseIf InType = 3 Then
            If ArrInDeduction IsNot Nothing AndAlso ArrInDeduction.Count > 0 Then
                For Each obj As clsMakeSavingPaymentDeduction In ArrInDeduction
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSelect).Value = obj.IsSelect
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDocNo).Value = obj.AP_Invoice_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDocDate).Value = obj.DocDate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = obj.Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReduceDedAmt).Value = obj.Red_Ded_Amount
                Next
            End If
        End If
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If InType = 1 Then
            ArrOutSaving = New List(Of clsMakeSavingPaymentSaving)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim obj As New clsMakeSavingPaymentSaving()
                obj.IsSelect = clsCommon.myCBool(gv1.Rows(ii).Cells(colSelect).Value)
                obj.AP_Invoice_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDocNo).Value)
                obj.DocDate = clsCommon.myCDate(gv1.Rows(ii).Cells(colDocDate).Value)
                obj.Amount = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colAmount).Value)
                ArrOutSaving.Add(obj)
            Next
        ElseIf InType = 2 Then
            ArrOutSale = New List(Of clsMakeSavingPaymentDCSSale)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim obj As New clsMakeSavingPaymentDCSSale()
                obj.IsSelect = clsCommon.myCBool(gv1.Rows(ii).Cells(colSelect).Value)
                obj.AR_Invoice_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDocNo).Value)
                obj.DocDate = clsCommon.myCDate(gv1.Rows(ii).Cells(colDocDate).Value)
                obj.Amount = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colAmount).Value)
                obj.Red_Ded_Amount = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colReduceDedAmt).Value)
                ArrOutSale.Add(obj)
            Next
        ElseIf InType = 3 Then
            ArrOutDeduction = New List(Of clsMakeSavingPaymentDeduction)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim obj As New clsMakeSavingPaymentDeduction()
                obj.IsSelect = clsCommon.myCBool(gv1.Rows(ii).Cells(colSelect).Value)
                obj.AP_Invoice_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDocNo).Value)
                obj.DocDate = clsCommon.myCDate(gv1.Rows(ii).Cells(colDocDate).Value)
                obj.Amount = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colAmount).Value)
                obj.Red_Ded_Amount = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colReduceDedAmt).Value)
                ArrOutDeduction.Add(obj)
            Next
        End If
        IsCancelClicked = False
        Me.Close()
    End Sub
End Class

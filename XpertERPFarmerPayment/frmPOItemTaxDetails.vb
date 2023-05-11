Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Public Class FrmPOItemTaxDetails
#Region "Varibales"
    Public strLineNo As String = ""
    Public strItemCode As String = ""
    Public strItemName As String = ""
    Public dblTotTax As Double = 0
    Public dblAmtAfterDis As Double = 0
    Public Without_State_Condition As Boolean = False
    Public IslocationSegment As Boolean = False

    Public strTaxGroup As String = ""
    Public strTransLocation As String = ""
    Public strTaxType As String = ""
    Public strVendorCustomerCode As String = ""

    Public ArrIn As List(Of clsTempItemTaxDetails) = Nothing
    Public ArrOut As List(Of clsTempItemTaxDetails) = Nothing
    Public obj As New clsPurchaseOrderHead()

    Const colTaxAutCode As String = "TAXAUTCODE"
    Const colTaxAutName As String = "TAXAUTNAME"
    Const colIsTaxable As String = "ISTAXABLE"
    Const colTaxRate As String = "TAXRATE"
    Const colIsSurTax As String = "ISSURTAX"
    Const colSurTaxCode As String = "SURTAXCODE"
    Const colBaseAmt As String = "TAXBASEAMT"
    Const colTaxAmt As String = "TAXAMT"
#End Region

    Private Sub FrmPOItemTaxDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

    End Sub

    Private Sub FrmPOItemTaxDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblItemName.Text = strItemName
        lblLineNo.Text = strLineNo
        lblTotalTax.Text = clsCommon.myCstr(dblTotTax)
        LoadBlankGrid()
        LoadData()
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoTaxAuthCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthCode.FormatString = ""
        repoTaxAuthCode.HeaderText = "Tax Authority Code"
        repoTaxAuthCode.Name = colTaxAutCode
        repoTaxAuthCode.Width = 150
        repoTaxAuthCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTaxAuthCode)

        Dim repoTaxAuthName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthName.FormatString = ""
        repoTaxAuthName.HeaderText = "Tax Authority"
        repoTaxAuthName.Name = colTaxAutName
        repoTaxAuthName.Width = 200
        repoTaxAuthName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTaxAuthName)

        Dim repoTaxBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "Base Amount"
        repoTaxBaseAmt.Name = colBaseAmt
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        Dim repoTaxRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate.FormatString = ""
        repoTaxRate.HeaderText = "Tax Rate"
        repoTaxRate.Name = colTaxRate
        repoTaxRate.Width = 100
        repoTaxRate.ReadOnly = True
        repoTaxRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate)

        Dim repoIsSurTax As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax.HeaderText = "Is Surtax"
        repoIsSurTax.Name = colIsSurTax
        repoIsSurTax.Width = 80
        repoIsSurTax.ReadOnly = True
        repoIsSurTax.IsVisible = False
        repoIsSurTax.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax)

        Dim repoSurTaxCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode.FormatString = ""
        repoSurTaxCode.HeaderText = "Surtax"
        repoSurTaxCode.Name = colSurTaxCode
        repoSurTaxCode.Width = 100
        repoSurTaxCode.ReadOnly = True
        repoSurTaxCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode)

        Dim repoIsTaxable As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable.HeaderText = "Is Taxable"
        repoIsTaxable.Name = colIsTaxable
        repoIsTaxable.Width = 80
        repoIsTaxable.ReadOnly = True
        repoIsTaxable.IsVisible = False
        repoIsTaxable.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable)

        Dim repoTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt.FormatString = ""
        repoTaxAmt.HeaderText = "Tax Amount"
        repoTaxAmt.Name = colTaxAmt
        repoTaxAmt.Width = 100
        repoTaxAmt.ReadOnly = True
        repoTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt)

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Sub LoadData()
        If ArrIn IsNot Nothing AndAlso ArrIn.Count > 0 Then
            For Each obj As clsTempItemTaxDetails In ArrIn
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAutCode).Value = obj.AuthorityCode
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAutName).Value = obj.AuthorityName
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIsTaxable).Value = obj.IsTaxable
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate).Value = obj.Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSurTax).Value = obj.isSurTax
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSurTaxCode).Value = obj.SurTax
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBaseAmt).Value = obj.BaseAmt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt).Value = obj.TaxAmt
            Next
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ArrOut = Nothing
        Me.Close()
    End Sub






    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If clsCommon.CompairString(gv1.CurrentCell.ColumnInfo.Name, colTaxRate) = CompairStringResult.Equal Then
                Dim dblNewRate As Double = 0
                If clsCommon.CompairString(strTaxType, "T") = CompairStringResult.Equal Then
                    dblNewRate = clsLocationWiseTax.FinderForTaxRateForTransfer(strTransLocation, strTaxGroup, clsCommon.myCstr(gv1.CurrentRow.Cells(colTaxAutCode).Value), strVendorCustomerCode, strTaxType)
                Else
                    dblNewRate = clsLocationWiseTax.FinderForTaxRate(strTransLocation, strTaxGroup, clsCommon.myCstr(gv1.CurrentRow.Cells(colTaxAutCode).Value), strVendorCustomerCode, strTaxType, Without_State_Condition, IslocationSegment)
                End If
                'Dim dblNewRate As Double = clsLocationWiseTax.FinderForTaxRate(strTransLocation, strTaxGroup, clsCommon.myCstr(gv1.CurrentRow.Cells(colTaxAutCode).Value), strVendorCustomerCode, strTaxType, Without_State_Condition, IslocationSegment)
                gv1.CurrentRow.Cells(colTaxRate).Value = dblNewRate
                UpdateCurrentRow()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub UpdateCurrentRow()
        Dim arrTaxableAuth As New List(Of String)
        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colTaxAutCode).Value)
            Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate).Value)
            Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(ii).Cells(colIsSurTax).Value)
            Dim strSurTaxCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSurTaxCode).Value)
            Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.Rows(ii).Cells(colIsTaxable).Value)
            Dim dblBaseAmt As Double = 0
            Dim dblTaxAmt As Double = 0
            If IsSurTax Then
                Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(ii, strSurTaxCode)
                dblBaseAmt = dblSurTaxAmt
            Else
                Dim dblOtherTaxAmt As Double = 0
                If IsTaxable Then
                    dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(ii, arrTaxableAuth)
                End If
                dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
            End If
            gv1.Rows(ii).Cells(colBaseAmt).Value = Math.Round(dblBaseAmt, 2)
            dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
            gv1.Rows(ii).Cells(colTaxAmt).Value = Math.Round(dblTaxAmt, IIf(objCommonVar.IsRoundOffTaxToZeroDecimal, 0, 2))
            If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                arrTaxableAuth.Add(strTaxCode.ToUpper())
            End If
        Next
        SetTotalTaxAmt()
    End Sub

    Private Function GetCurrentRowSurTaxAmt(ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
        For ii As Integer = 0 To intEndCol - 1
            If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colTaxAutCode).Value)) = CompairStringResult.Equal Then
                Return clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt).Value)
            End If
        Next
        Return 0
    End Function

    Private Function GetCurrentRowOtherTaxAmt(ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = 0 To intEndCol - 1
                If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv1.Rows(ii).Cells(colTaxAutCode).Value)) = CompairStringResult.Equal Then
                    dblRet = dblRet + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt).Value)
                End If
            Next
        Next
        Return dblRet
    End Function

    Private Sub SetTotalTaxAmt()
        dblTotTax = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            dblTotTax = dblTotTax + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt).Value)
        Next
        lblTotalTax.Text = clsCommon.myFormat(dblTotTax)
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        ArrOut = New List(Of clsTempItemTaxDetails)
        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim obj As New clsTempItemTaxDetails()
            obj.AuthorityCode = clsCommon.myCstr(gv1.Rows(ii).Cells(colTaxAutCode).Value)
            obj.AuthorityName = clsCommon.myCstr(gv1.Rows(ii).Cells(colTaxAutName).Value)
            obj.IsTaxable = clsCommon.myCBool(gv1.Rows(ii).Cells(colIsTaxable).Value)
            obj.Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate).Value)
            obj.isSurTax = clsCommon.myCBool(gv1.Rows(ii).Cells(colIsSurTax).Value)
            obj.SurTax = clsCommon.myCstr(gv1.Rows(ii).Cells(colSurTaxCode).Value)
            obj.BaseAmt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colBaseAmt).Value)
            obj.TaxAmt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt).Value)
            ArrOut.Add(obj)
        Next
        Me.Close()
    End Sub
End Class

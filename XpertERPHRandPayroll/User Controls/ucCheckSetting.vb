Imports common
Imports System.Data.Sql
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class ucCheckSetting

#Region "Variables"
    Public strBankCode As String = ""
    Public LedgerCode As String = ""

    Private mouseDownX As Integer = 0
    Private mouseDownY As Integer = 0
    Private mouseUpX As Integer = 0
    Private mouseUpY As Integer = 0
    Private lbl As common.Controls.MyLabel = Nothing

    Public Const InchToPixelConversionWidth As Integer = 80
    Public Const InchToPixelConversionHeight As Integer = 50

    Public objName1 As New clsLableSetting()
    Public objName2 As New clsLableSetting()
    Public objAmt As New clsLableSetting()
    Public objDate As New clsLableSetting()
    Public objAmtwrd1 As New clsLableSetting()
    Public objAmtwrd2 As New clsLableSetting()
    Public objLine1 As New clsLableSetting()
    Public objforCompany As New clsLableSetting()
    Public objSign As New clsLableSetting()
    Public objNotOver As New clsLableSetting()
    Public objAccPay As New clsLableSetting()
    Public objLine2 As New clsLableSetting()
    Public objNotForHighValue As New clsLableSetting()
    Public objAccNo As New clsLableSetting()
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub formLoad()
        objName1.CodeValue = ChequeSettingVar.NameLine1
        objName2.CodeValue = ChequeSettingVar.NameLine2
        objAmt.CodeValue = ChequeSettingVar.Amount
        objDate.CodeValue = ChequeSettingVar.[Date]
        objAmtwrd1.CodeValue = ChequeSettingVar.Amtword1
        objAmtwrd2.CodeValue = ChequeSettingVar.Amtword2
        objLine1.CodeValue = ChequeSettingVar.Line1
        objforCompany.CodeValue = ChequeSettingVar.ForCompany
        objSign.CodeValue = ChequeSettingVar.Sign
        objNotOver.CodeValue = ChequeSettingVar.NotOver
        objAccPay.CodeValue = ChequeSettingVar.AccountPay
        objLine2.CodeValue = ChequeSettingVar.Line2
        objNotForHighValue.CodeValue = ChequeSettingVar.NotForHighValue
        objAccNo.CodeValue = ChequeSettingVar.AccountNo

        Dim tmp As New frmLableSetting()
        setLableValues(tmp, lblAcPay)
        getLableValues(tmp, lblAcPay)

        setLableValues(tmp, lblAmt)
        getLableValues(tmp, lblAmt)

        setLableValues(tmp, lblAmtWrd1)
        getLableValues(tmp, lblAmtWrd1)

        setLableValues(tmp, lblAmtWrd2)
        getLableValues(tmp, lblAmtWrd2)

        setLableValues(tmp, lblDate)
        getLableValues(tmp, lblDate)

        setLableValues(tmp, lblForCompany)
        getLableValues(tmp, lblForCompany)

        setLableValues(tmp, lblLine1)
        getLableValues(tmp, lblLine1)

        setLableValues(tmp, lblLine2)
        getLableValues(tmp, lblLine2)

        setLableValues(tmp, lblNameline1)
        getLableValues(tmp, lblNameline1)

        setLableValues(tmp, lblNameline2)
        getLableValues(tmp, lblNameline2)

        setLableValues(tmp, lblNotOver)
        getLableValues(tmp, lblNotOver)

        setLableValues(tmp, lblSign)
        getLableValues(tmp, lblSign)

        setLableValues(tmp, lblNotForHighValue)
        getLableValues(tmp, lblNotForHighValue)

        setLableValues(tmp, lblAccNo)
        getLableValues(tmp, lblAccNo)

        If common.clsCommon.myLen(strBankCode) > 0 Then
            LoadData(strBankCode)
        End If
    End Sub

    Public Sub LoadData(ByVal bcode As String)
        objName1.CodeValue = ChequeSettingVar.NameLine1
        objName2.CodeValue = ChequeSettingVar.NameLine2
        objAmt.CodeValue = ChequeSettingVar.Amount
        objDate.CodeValue = ChequeSettingVar.[Date]
        objAmtwrd1.CodeValue = ChequeSettingVar.Amtword1
        objAmtwrd2.CodeValue = ChequeSettingVar.Amtword2
        objLine1.CodeValue = ChequeSettingVar.Line1
        objforCompany.CodeValue = ChequeSettingVar.ForCompany
        objSign.CodeValue = ChequeSettingVar.Sign
        objNotOver.CodeValue = ChequeSettingVar.NotOver
        objAccPay.CodeValue = ChequeSettingVar.AccountPay
        objLine2.CodeValue = ChequeSettingVar.Line2

        objNotForHighValue.CodeValue = ChequeSettingVar.NotForHighValue
        objAccNo.CodeValue = ChequeSettingVar.AccountNo

        Dim arrLable As New List(Of clsLableSetting)()
        arrLable = clsLableSetting.ReturnData(bcode)

        If arrLable IsNot Nothing AndAlso arrLable.Count > 0 Then
            For Each objLable As clsLableSetting In arrLable
                Select Case objLable.CodeValue
                    Case ChequeSettingVar.NameLine1
                        objName1 = objLable
                        Exit Select
                    Case ChequeSettingVar.NameLine2
                        objName2 = objLable
                        Exit Select
                    Case ChequeSettingVar.Amount
                        objAmt = objLable
                        Exit Select
                    Case ChequeSettingVar.[Date]
                        objDate = objLable
                        Exit Select
                    Case ChequeSettingVar.Amtword1
                        objAmtwrd1 = objLable
                        Exit Select
                    Case ChequeSettingVar.Amtword2
                        objAmtwrd2 = objLable
                        Exit Select
                    Case ChequeSettingVar.ForCompany
                        objforCompany = objLable
                        Exit Select
                    Case ChequeSettingVar.Sign
                        objSign = objLable
                        Exit Select
                    Case ChequeSettingVar.NotOver
                        objNotOver = objLable
                        Exit Select
                    Case ChequeSettingVar.AccountPay
                        objAccPay = objLable
                        Exit Select
                    Case ChequeSettingVar.Line1
                        objLine1 = objLable
                        Exit Select
                    Case ChequeSettingVar.Line2
                        objLine2 = objLable
                        Exit Select
                    Case ChequeSettingVar.NotForHighValue
                        objNotForHighValue = objLable
                        Exit Select
                    Case ChequeSettingVar.AccountNo
                        objAccNo = objLable
                        Exit Select
                End Select
            Next
            txtLftMargin.Value = objName1.pageLeft
            txttpMargin.Value = objName1.pageTop

            If objName1.PaperOrientation = PaperOrientation.Landscape Then
                rdoLandscape.IsChecked = True
            Else
                rdoPotrait.IsChecked = True
            End If

            displayValues()
        End If
    End Sub

    Private Sub displayValues()
        'lblName1
        lblNameline1.Text = objName1.caption
        lblNameline1.Left = CInt(Math.Ceiling(objName1.left * InchToPixelConversionWidth))
        lblNameline1.Top = CInt(Math.Ceiling(objName1.top * InchToPixelConversionHeight))
        lblNameline1.Height = CInt(Math.Ceiling(objName1.height * InchToPixelConversionHeight))
        lblNameline1.Width = CInt(Math.Ceiling(objName1.width * InchToPixelConversionWidth))

        'lblName2
        lblNameline2.Text = objName2.caption
        lblNameline2.Left = CInt(Math.Ceiling(objName2.left * InchToPixelConversionWidth))
        lblNameline2.Top = CInt(Math.Ceiling(objName2.top * InchToPixelConversionHeight))
        lblNameline2.Height = CInt(Math.Ceiling(objName2.height * InchToPixelConversionHeight))
        lblNameline2.Width = CInt(Math.Ceiling(objName2.width * InchToPixelConversionWidth))

        'lblAcPay
        lblAcPay.Text = objAccPay.caption
        lblAcPay.Left = CInt(Math.Ceiling(objAccPay.left * InchToPixelConversionWidth))
        lblAcPay.Top = CInt(Math.Ceiling(objAccPay.top * InchToPixelConversionHeight))
        lblAcPay.Height = CInt(Math.Ceiling(objAccPay.height * InchToPixelConversionHeight))
        lblAcPay.Width = CInt(Math.Ceiling(objAccPay.width * InchToPixelConversionWidth))

        'lblAmt
        lblAmt.Text = objAmt.caption
        lblAmt.Left = CInt(Math.Ceiling(objAmt.left * InchToPixelConversionWidth))
        lblAmt.Top = CInt(Math.Ceiling(objAmt.top * InchToPixelConversionHeight))
        lblAmt.Height = CInt(Math.Ceiling(objAmt.height * InchToPixelConversionHeight))
        lblAmt.Width = CInt(Math.Ceiling(objAmt.width * InchToPixelConversionWidth))

        'lblAmtWrd1
        lblAmtWrd1.Text = objAmtwrd1.caption
        lblAmtWrd1.Left = CInt(Math.Ceiling(objAmtwrd1.left * InchToPixelConversionWidth))
        lblAmtWrd1.Top = CInt(Math.Ceiling(objAmtwrd1.top * InchToPixelConversionHeight))
        lblAmtWrd1.Height = CInt(Math.Ceiling(objAmtwrd1.height * InchToPixelConversionHeight))
        lblAmtWrd1.Width = CInt(Math.Ceiling(objAmtwrd1.width * InchToPixelConversionWidth))

        'lblAmtWrd2
        lblAmtWrd2.Text = objAmtwrd2.caption
        lblAmtWrd2.Left = CInt(Math.Ceiling(objAmtwrd2.left * InchToPixelConversionWidth))
        lblAmtWrd2.Top = CInt(Math.Ceiling(objAmtwrd2.top * InchToPixelConversionHeight))
        lblAmtWrd2.Height = CInt(Math.Ceiling(objAmtwrd2.height * InchToPixelConversionHeight))
        lblAmtWrd2.Width = CInt(Math.Ceiling(objAmtwrd2.width * InchToPixelConversionWidth))

        'lblDate
        lblDate.Text = objDate.caption
        lblDate.Left = CInt(Math.Ceiling(objDate.left * InchToPixelConversionWidth))
        lblDate.Top = CInt(Math.Ceiling(objDate.top * InchToPixelConversionHeight))
        lblDate.Height = CInt(Math.Ceiling(objDate.height * InchToPixelConversionHeight))
        lblDate.Width = CInt(Math.Ceiling(objDate.width * InchToPixelConversionWidth))

        'lblForCompany
        lblForCompany.Text = objforCompany.caption
        lblForCompany.Left = CInt(Math.Ceiling(objforCompany.left * InchToPixelConversionWidth))
        lblForCompany.Top = CInt(Math.Ceiling(objforCompany.top * InchToPixelConversionHeight))
        lblForCompany.Height = CInt(Math.Ceiling(objforCompany.height * InchToPixelConversionHeight))
        lblForCompany.Width = CInt(Math.Ceiling(objforCompany.width * InchToPixelConversionWidth))

        'lblLine1
        lblLine1.Text = objLine1.caption
        lblLine1.Left = CInt(Math.Ceiling(objLine1.left * InchToPixelConversionWidth))
        lblLine1.Top = CInt(Math.Ceiling(objLine1.top * InchToPixelConversionHeight))
        lblLine1.Height = CInt(Math.Ceiling(objLine1.height * InchToPixelConversionHeight))
        lblLine1.Width = CInt(Math.Ceiling(objLine1.width * InchToPixelConversionWidth))

        'lblLine2
        lblLine2.Text = objLine2.caption
        lblLine2.Left = CInt(Math.Ceiling(objLine2.left * InchToPixelConversionWidth))
        lblLine2.Top = CInt(Math.Ceiling(objLine2.top * InchToPixelConversionHeight))
        lblLine2.Height = CInt(Math.Ceiling(objLine2.height * InchToPixelConversionHeight))
        lblLine2.Width = CInt(Math.Ceiling(objLine2.width * InchToPixelConversionWidth))

        'lblNotOver
        lblNotOver.Text = objNotOver.caption
        lblNotOver.Left = CInt(Math.Ceiling(objNotOver.left * InchToPixelConversionWidth))
        lblNotOver.Top = CInt(Math.Ceiling(objNotOver.top * InchToPixelConversionHeight))
        lblNotOver.Height = CInt(Math.Ceiling(objNotOver.height * InchToPixelConversionHeight))
        lblNotOver.Width = CInt(Math.Ceiling(objNotOver.width * InchToPixelConversionWidth))

        'lblSign
        lblSign.Text = objSign.caption
        lblSign.Left = CInt(Math.Ceiling(objSign.left * InchToPixelConversionWidth))
        lblSign.Top = CInt(Math.Ceiling(objSign.top * InchToPixelConversionHeight))
        lblSign.Height = CInt(Math.Ceiling(objSign.height * InchToPixelConversionHeight))
        lblSign.Width = CInt(Math.Ceiling(objSign.width * InchToPixelConversionWidth))

        'lblNotForHighValue
        lblNotForHighValue.Text = objNotForHighValue.caption
        lblNotForHighValue.Left = CInt(Math.Ceiling(objNotForHighValue.left * InchToPixelConversionWidth))
        lblNotForHighValue.Top = CInt(Math.Ceiling(objNotForHighValue.top * InchToPixelConversionHeight))
        lblNotForHighValue.Height = CInt(Math.Ceiling(objNotForHighValue.height * InchToPixelConversionHeight))
        lblNotForHighValue.Width = CInt(Math.Ceiling(objNotForHighValue.width * InchToPixelConversionWidth))

        'lblAccNo
        lblAccNo.Text = objAccNo.caption
        lblAccNo.Left = CInt(Math.Ceiling(objAccNo.left * InchToPixelConversionWidth))
        lblAccNo.Top = CInt(Math.Ceiling(objAccNo.top * InchToPixelConversionHeight))
        lblAccNo.Height = CInt(Math.Ceiling(objAccNo.height * InchToPixelConversionHeight))
        lblAccNo.Width = CInt(Math.Ceiling(objAccNo.width * InchToPixelConversionWidth))
    End Sub

    Private Sub lblName_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles lblAcPay.MouseDown, lblForCompany.MouseDown, lblSign.MouseDown, lblLine1.MouseDown, lblLine2.MouseDown, lblAmtWrd1.MouseDown, lblNotForHighValue.MouseDown, lblAccNo.MouseDown, lblNotOver.MouseDown, lblAmtWrd2.MouseDown, lblNameline2.MouseDown, lblAmt.MouseDown, lblDate.MouseDown, lblNameline1.MouseDown
        mouseDownX = e.X
        mouseDownY = e.Y
        lbl = DirectCast(sender, common.Controls.MyLabel)
        'lbl.BorderStyle = BorderStyle.FixedSingle;

    End Sub

    Private Sub lblName_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles lblAcPay.MouseUp, lblForCompany.MouseUp, lblSign.MouseUp, lblLine1.MouseUp, lblLine2.MouseUp, lblAmtWrd1.MouseUp, lblNotForHighValue.MouseUp, lblAccNo.MouseUp, lblNotOver.MouseUp, lblAmtWrd2.MouseUp, lblNameline2.MouseUp, lblAmt.MouseUp, lblDate.MouseUp, lblNameline1.MouseUp
        mouseUpX = e.X
        mouseUpY = e.Y

        lbl.Left += mouseUpX - mouseDownX
        lbl.Top += mouseUpY - mouseDownY
        If lbl.Top <= 0 Then
            lbl.Top = 2
        ElseIf lbl.Top >= grpChque.Size.Height - lbl.Height Then
            lbl.Top = grpChque.Size.Height - lbl.Height - 2
        End If
        If lbl.Left <= 0 Then
            lbl.Left = 3
        ElseIf lbl.Left >= grpChque.Size.Width - lbl.Width Then
            lbl.Left = grpChque.Width - lbl.Width - 2
        End If

        Dim frm As New frmLableSetting()
        setLableValues(frm, lbl)
        getLableValues(frm, lbl)

        ' grpChque.DoDragDrop(sender, DragDropEffects.Move);
        'lbl.BorderStyle = BorderStyle.None;
    End Sub

    Private Sub grpChque_DragLeave(ByVal sender As Object, ByVal e As EventArgs) Handles grpChque.DragLeave
        If sender.[GetType]() Is GetType(common.Controls.MyLabel) Then
            Dim lbl As New common.Controls.MyLabel()
            lbl = DirectCast(sender, common.Controls.MyLabel)
            lbl.Left += mouseUpX - mouseDownX
            lbl.Top += mouseUpY - mouseDownY
        End If
    End Sub

    Private Sub lblNameline1_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lblAcPay.DoubleClick, lblForCompany.DoubleClick, lblSign.DoubleClick, lblLine1.DoubleClick, lblLine2.DoubleClick, lblAmtWrd1.DoubleClick, lblNotForHighValue.DoubleClick, lblAccNo.DoubleClick, lblNotOver.DoubleClick, lblAmtWrd2.DoubleClick, lblNameline2.DoubleClick, lblAmt.DoubleClick, lblDate.DoubleClick, lblNameline1.DoubleClick
        Dim lblSet As New common.Controls.MyLabel()
        Dim frm As New frmLableSetting()
        setLableValues(frm, lbl)
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
        getLableValues(frm, lbl)
    End Sub

    Private Sub getLableValues(ByVal frm As frmLableSetting, ByVal lbl As common.Controls.MyLabel)
        lbl.Text = frm.caption
        lbl.Left = CInt(Math.Ceiling(frm.Myleft * InchToPixelConversionWidth))
        lbl.Top = CInt(Math.Ceiling(frm.Mytop * InchToPixelConversionHeight))
        lbl.Height = CInt(Math.Ceiling(frm.Myheight * InchToPixelConversionHeight))
        lbl.Width = CInt(Math.Ceiling(frm.Mywidth * InchToPixelConversionWidth))
        'lbl.Font.Name = frm.fontName;
        'lbl.Font.Bold = frm.isBold;
        If lbl Is lblNameline1 Then
            objName1.CodeValue = ChequeSettingVar.NameLine1
            objName1.fontSize = frm.fontSize
            objName1.charPerLine = frm.charPerLine
            objName1.decimalPlace = frm.decimalPlace
            objName1.caption = frm.caption
            objName1.left = frm.Myleft
            objName1.top = frm.Mytop
            objName1.height = frm.Myheight
            objName1.width = frm.Mywidth
            objName1.fontName = frm.fontName
            objName1.isBold = frm.isBold
            objName1.IsVisible = frm.isVisible
            objName1.DateStyle = frm.DateStyle
            objName1.isxxxxxxBefore = frm.isxxxxxxBefore
            objName1.isxxxxxxAfter = frm.isxxxxxxAfter
        ElseIf lbl Is lblNameline2 Then
            objName2.CodeValue = ChequeSettingVar.NameLine2
            objName2.fontSize = frm.fontSize
            objName2.charPerLine = frm.charPerLine
            objName2.decimalPlace = frm.decimalPlace
            objName2.caption = frm.caption
            objName2.left = frm.Myleft
            objName2.top = frm.Mytop
            objName2.height = frm.Myheight
            objName2.width = frm.Mywidth
            objName2.fontName = frm.fontName
            objName2.isBold = frm.isBold
            objName2.IsVisible = frm.isVisible
            objName2.DateStyle = frm.DateStyle
            objName2.isxxxxxxBefore = frm.isxxxxxxBefore
            objName2.isxxxxxxAfter = frm.isxxxxxxAfter
        ElseIf lbl Is lblAcPay Then
            objAccPay.CodeValue = ChequeSettingVar.AccountPay
            objAccPay.fontSize = frm.fontSize
            objAccPay.charPerLine = frm.charPerLine
            objAccPay.decimalPlace = frm.decimalPlace
            objAccPay.caption = frm.caption
            objAccPay.left = frm.Myleft
            objAccPay.top = frm.Mytop
            objAccPay.height = frm.Myheight
            objAccPay.width = frm.Mywidth
            objAccPay.fontName = frm.fontName
            objAccPay.isBold = frm.isBold
            objAccPay.IsVisible = frm.isVisible
            objAccPay.DateStyle = frm.DateStyle
            objAccPay.isxxxxxxBefore = frm.isxxxxxxBefore
            objAccPay.isxxxxxxAfter = frm.isxxxxxxAfter
        ElseIf lbl Is lblAmt Then
            objAmt.CodeValue = ChequeSettingVar.Amount
            objAmt.fontSize = frm.fontSize
            objAmt.charPerLine = frm.charPerLine
            objAmt.decimalPlace = frm.decimalPlace
            objAmt.caption = frm.caption
            objAmt.left = frm.Myleft
            objAmt.top = frm.Mytop
            objAmt.height = frm.Myheight
            objAmt.width = frm.Mywidth
            objAmt.fontName = frm.fontName
            objAmt.isBold = frm.isBold
            objAmt.IsVisible = frm.isVisible
            objAmt.DateStyle = frm.DateStyle
            objAmt.isxxxxxxBefore = frm.isxxxxxxBefore
            objAmt.isxxxxxxAfter = frm.isxxxxxxAfter
        ElseIf lbl Is lblAmtWrd1 Then
            objAmtwrd1.CodeValue = ChequeSettingVar.Amtword1
            objAmtwrd1.fontSize = frm.fontSize
            objAmtwrd1.charPerLine = frm.charPerLine
            objAmtwrd1.decimalPlace = frm.decimalPlace
            objAmtwrd1.caption = frm.caption
            objAmtwrd1.left = frm.Myleft
            objAmtwrd1.top = frm.Mytop
            objAmtwrd1.height = frm.Myheight
            objAmtwrd1.width = frm.Mywidth
            objAmtwrd1.fontName = frm.fontName
            objAmtwrd1.isBold = frm.isBold
            objAmtwrd1.IsVisible = frm.isVisible
            objAmtwrd1.DateStyle = frm.DateStyle
            objAmtwrd1.isxxxxxxBefore = frm.isxxxxxxBefore
            objAmtwrd1.isxxxxxxAfter = frm.isxxxxxxAfter
        ElseIf lbl Is lblAmtWrd2 Then
            objAmtwrd2.CodeValue = ChequeSettingVar.Amtword2
            objAmtwrd2.fontSize = frm.fontSize
            objAmtwrd2.charPerLine = frm.charPerLine
            objAmtwrd2.decimalPlace = frm.decimalPlace
            objAmtwrd2.caption = frm.caption
            objAmtwrd2.left = frm.Myleft
            objAmtwrd2.top = frm.Mytop
            objAmtwrd2.height = frm.Myheight
            objAmtwrd2.width = frm.Mywidth
            objAmtwrd2.fontName = frm.fontName
            objAmtwrd2.isBold = frm.isBold
            objAmtwrd2.IsVisible = frm.isVisible
            objAmtwrd2.DateStyle = frm.DateStyle
            objAmtwrd2.isxxxxxxBefore = frm.isxxxxxxBefore
            objAmtwrd2.isxxxxxxAfter = frm.isxxxxxxAfter
        ElseIf lbl Is lblDate Then
            objDate.CodeValue = ChequeSettingVar.[Date]
            objDate.fontSize = frm.fontSize
            objDate.charPerLine = frm.charPerLine
            objDate.decimalPlace = frm.decimalPlace
            objDate.caption = frm.caption
            objDate.left = frm.Myleft
            objDate.top = frm.Mytop
            objDate.height = frm.Myheight
            objDate.width = frm.Mywidth
            objDate.fontName = frm.fontName
            objDate.isBold = frm.isBold
            objDate.IsVisible = frm.isVisible
            objDate.DateStyle = frm.DateStyle
            objDate.isxxxxxxBefore = frm.isxxxxxxBefore
            objDate.isxxxxxxAfter = frm.isxxxxxxAfter
            objDate.CharSpace = frm.CharSpace
        ElseIf lbl Is lblLine1 Then
            objLine1.CodeValue = ChequeSettingVar.Line1
            lblLine2.Text = frm.caption
            objLine2.caption = frm.caption
            objLine1.fontSize = frm.fontSize
            objLine1.charPerLine = frm.charPerLine
            objLine1.decimalPlace = frm.decimalPlace
            objLine1.caption = frm.caption
            objLine1.left = frm.Myleft
            objLine1.top = frm.Mytop
            objLine1.height = frm.Myheight
            objLine1.width = frm.Mywidth
            objLine1.fontName = frm.fontName
            objLine1.isBold = frm.isBold
            objLine1.IsVisible = frm.isVisible
            objLine1.DateStyle = frm.DateStyle
            objLine1.isxxxxxxBefore = frm.isxxxxxxBefore
            objLine1.isxxxxxxAfter = frm.isxxxxxxAfter
        ElseIf lbl Is lblLine2 Then
            objLine2.CodeValue = ChequeSettingVar.Line2
            lblLine1.Text = frm.caption
            objLine1.caption = frm.caption
            objLine2.fontSize = frm.fontSize
            objLine2.charPerLine = frm.charPerLine
            objLine2.decimalPlace = frm.decimalPlace
            objLine2.caption = frm.caption
            objLine2.left = frm.Myleft
            objLine2.top = frm.Mytop
            objLine2.height = frm.Myheight
            objLine2.width = frm.Mywidth
            objLine2.fontName = frm.fontName
            objLine2.isBold = frm.isBold
            objLine2.IsVisible = frm.isVisible
            objLine2.DateStyle = frm.DateStyle
            objLine2.isxxxxxxBefore = frm.isxxxxxxBefore
            objLine2.isxxxxxxAfter = frm.isxxxxxxAfter
        ElseIf lbl Is lblForCompany Then
            objforCompany.CodeValue = ChequeSettingVar.ForCompany
            objforCompany.fontSize = frm.fontSize
            objforCompany.charPerLine = frm.charPerLine
            objforCompany.decimalPlace = frm.decimalPlace
            objforCompany.caption = frm.caption
            objforCompany.left = frm.Myleft
            objforCompany.top = frm.Mytop
            objforCompany.height = frm.Myheight
            objforCompany.width = frm.Mywidth
            objforCompany.fontName = frm.fontName
            objforCompany.isBold = frm.isBold
            objforCompany.IsVisible = frm.isVisible
            objforCompany.DateStyle = frm.DateStyle
            objforCompany.isxxxxxxBefore = frm.isxxxxxxBefore
            objforCompany.isxxxxxxAfter = frm.isxxxxxxAfter
        ElseIf lbl Is lblSign Then
            objSign.CodeValue = ChequeSettingVar.Sign
            objSign.fontSize = frm.fontSize
            objSign.charPerLine = frm.charPerLine
            objSign.decimalPlace = frm.decimalPlace
            objSign.caption = frm.caption
            objSign.left = frm.Myleft
            objSign.top = frm.Mytop
            objSign.height = frm.Myheight
            objSign.width = frm.Mywidth
            objSign.fontName = frm.fontName
            objSign.isBold = frm.isBold
            objSign.IsVisible = frm.isVisible
            objSign.DateStyle = frm.DateStyle
            objSign.isxxxxxxBefore = frm.isxxxxxxBefore
            objSign.isxxxxxxAfter = frm.isxxxxxxAfter
        ElseIf lbl Is lblNotOver Then
            objNotOver.CodeValue = ChequeSettingVar.NotOver
            objNotOver.fontSize = frm.fontSize
            objNotOver.charPerLine = frm.charPerLine
            objNotOver.decimalPlace = frm.decimalPlace
            objNotOver.caption = frm.caption
            objNotOver.left = frm.Myleft
            objNotOver.top = frm.Mytop
            objNotOver.height = frm.Myheight
            objNotOver.width = frm.Mywidth
            objNotOver.fontName = frm.fontName
            objNotOver.isBold = frm.isBold
            objNotOver.IsVisible = frm.isVisible
            objNotOver.DateStyle = frm.DateStyle
            objNotOver.isxxxxxxBefore = frm.isxxxxxxBefore
            objNotOver.isxxxxxxAfter = frm.isxxxxxxAfter
        ElseIf lbl Is lblNotForHighValue Then
            objNotForHighValue.CodeValue = ChequeSettingVar.NotForHighValue
            objNotForHighValue.fontSize = frm.fontSize
            objNotForHighValue.charPerLine = frm.charPerLine
            objNotForHighValue.decimalPlace = frm.decimalPlace
            objNotForHighValue.caption = frm.caption
            objNotForHighValue.left = frm.Myleft
            objNotForHighValue.top = frm.Mytop
            objNotForHighValue.height = frm.Myheight
            objNotForHighValue.width = frm.Mywidth
            objNotForHighValue.fontName = frm.fontName
            objNotForHighValue.isBold = frm.isBold
            objNotForHighValue.IsVisible = frm.isVisible
            objNotForHighValue.DateStyle = frm.DateStyle
            objNotForHighValue.isxxxxxxBefore = frm.isxxxxxxBefore
            objNotForHighValue.isxxxxxxAfter = frm.isxxxxxxAfter
        ElseIf lbl Is lblAccNo Then
            objAccNo.CodeValue = ChequeSettingVar.AccountNo
            objAccNo.fontSize = frm.fontSize
            objAccNo.charPerLine = frm.charPerLine
            objAccNo.decimalPlace = frm.decimalPlace
            objAccNo.caption = frm.caption
            objAccNo.left = frm.Myleft
            objAccNo.top = frm.Mytop
            objAccNo.height = frm.Myheight
            objAccNo.width = frm.Mywidth
            objAccNo.fontName = frm.fontName
            objAccNo.isBold = frm.isBold
            objAccNo.IsVisible = frm.isVisible
            objAccNo.DateStyle = frm.DateStyle
            objAccNo.isxxxxxxBefore = frm.isxxxxxxBefore
            objAccNo.isxxxxxxAfter = frm.isxxxxxxAfter
        End If
    End Sub

    Private Sub setLableValues(ByVal frm As frmLableSetting, ByVal lbl As common.Controls.MyLabel)
        frm.caption = lbl.Text
        frm.Myleft = lbl.Left / clsCommon.myCdbl(InchToPixelConversionWidth)
        frm.Mytop = lbl.Top / clsCommon.myCdbl(InchToPixelConversionHeight)
        frm.Myheight = lbl.Height / clsCommon.myCdbl(InchToPixelConversionHeight)
        frm.Mywidth = lbl.Width / clsCommon.myCdbl(InchToPixelConversionWidth)
        'frm.fontName = lbl.Font.Name;
        'frm.isBold = lbl.Font.Bold;

        If lbl Is lblNameline1 Then
            frm.chqSetVar = ChequeSettingVar.NameLine1
            frm.fontSize = objName1.fontSize
            frm.charPerLine = objName1.charPerLine
            frm.decimalPlace = objName1.decimalPlace
            frm.fontName = objName1.fontName
            frm.isBold = objName1.isBold
            frm.isVisible = objName1.IsVisible
            frm.DateStyle = objName1.DateStyle
            frm.isxxxxxxAfter = objName1.isxxxxxxAfter
            frm.isxxxxxxBefore = objName1.isxxxxxxBefore

        ElseIf lbl Is lblNameline2 Then
            frm.chqSetVar = ChequeSettingVar.NameLine2
            frm.fontSize = objName2.fontSize
            frm.charPerLine = objName2.charPerLine
            frm.decimalPlace = objName2.decimalPlace
            frm.fontName = objName2.fontName
            frm.isBold = objName2.isBold
            frm.isVisible = objName2.IsVisible
            frm.DateStyle = objName2.DateStyle
            frm.isxxxxxxAfter = objName2.isxxxxxxAfter
            frm.isxxxxxxBefore = objName2.isxxxxxxBefore

        ElseIf lbl Is lblAcPay Then
            frm.chqSetVar = ChequeSettingVar.AccountPay
            frm.fontSize = objAccPay.fontSize
            frm.charPerLine = objAccPay.charPerLine
            frm.decimalPlace = objAccPay.decimalPlace
            frm.fontName = objAccPay.fontName
            frm.isBold = objAccPay.isBold
            frm.isVisible = objAccPay.IsVisible
            frm.DateStyle = objAccPay.DateStyle
            frm.isxxxxxxAfter = objAccPay.isxxxxxxAfter
            frm.isxxxxxxBefore = objAccPay.isxxxxxxBefore
        ElseIf lbl Is lblAmt Then
            frm.chqSetVar = ChequeSettingVar.Amount
            frm.fontSize = objAmt.fontSize
            frm.charPerLine = objAmt.charPerLine
            frm.decimalPlace = objAmt.decimalPlace
            frm.fontName = objAmt.fontName
            frm.isBold = objAmt.isBold
            frm.isVisible = objAmt.IsVisible
            frm.DateStyle = objAmt.DateStyle
            frm.isxxxxxxAfter = objAmt.isxxxxxxAfter
            frm.isxxxxxxBefore = objAmt.isxxxxxxBefore
        ElseIf lbl Is lblAmtWrd1 Then
            frm.chqSetVar = ChequeSettingVar.Amtword1
            frm.fontSize = objAmtwrd1.fontSize
            frm.charPerLine = objAmtwrd1.charPerLine
            frm.decimalPlace = objAmtwrd1.decimalPlace
            frm.fontName = objAmtwrd1.fontName
            frm.isBold = objAmtwrd1.isBold
            frm.isVisible = objAmtwrd1.IsVisible
            frm.DateStyle = objAmtwrd1.DateStyle
            frm.isxxxxxxAfter = objAmtwrd1.isxxxxxxAfter
            frm.isxxxxxxBefore = objAmtwrd1.isxxxxxxBefore
        ElseIf lbl Is lblAmtWrd2 Then
            frm.chqSetVar = ChequeSettingVar.Amtword2
            frm.fontSize = objAmtwrd2.fontSize
            frm.charPerLine = objAmtwrd2.charPerLine
            frm.decimalPlace = objAmtwrd2.decimalPlace
            frm.fontName = objAmtwrd2.fontName
            frm.isBold = objAmtwrd2.isBold
            frm.isVisible = objAmtwrd2.IsVisible
            frm.DateStyle = objAmtwrd2.DateStyle
            frm.isxxxxxxAfter = objAmtwrd2.isxxxxxxAfter
            frm.isxxxxxxBefore = objAmtwrd2.isxxxxxxBefore
        ElseIf lbl Is lblDate Then
            frm.chqSetVar = ChequeSettingVar.[Date]
            frm.fontSize = objDate.fontSize
            frm.charPerLine = objDate.charPerLine
            frm.decimalPlace = objDate.decimalPlace
            frm.fontName = objDate.fontName
            frm.isBold = objDate.isBold
            frm.isVisible = objDate.IsVisible
            frm.DateStyle = objDate.DateStyle
            frm.isxxxxxxAfter = objDate.isxxxxxxAfter
            frm.isxxxxxxBefore = objDate.isxxxxxxBefore
            frm.CharSpace = objDate.CharSpace
        ElseIf lbl Is lblLine1 Then
            frm.chqSetVar = ChequeSettingVar.Line1
            frm.fontSize = objLine1.fontSize
            frm.charPerLine = objLine1.charPerLine
            frm.decimalPlace = objLine1.decimalPlace
            frm.fontName = objLine1.fontName
            frm.isBold = objLine1.isBold
            frm.isVisible = objLine1.IsVisible
            frm.DateStyle = objLine1.DateStyle
            frm.isxxxxxxAfter = objLine1.isxxxxxxAfter
            frm.isxxxxxxBefore = objLine1.isxxxxxxBefore
        ElseIf lbl Is lblLine2 Then
            frm.chqSetVar = ChequeSettingVar.Line2
            frm.fontSize = objLine2.fontSize
            frm.charPerLine = objLine2.charPerLine
            frm.decimalPlace = objLine2.decimalPlace
            frm.fontName = objLine2.fontName
            frm.isBold = objLine2.isBold
            frm.isVisible = objLine2.IsVisible
            frm.DateStyle = objLine2.DateStyle
            frm.isxxxxxxAfter = objLine2.isxxxxxxAfter
            frm.isxxxxxxBefore = objLine2.isxxxxxxBefore
        ElseIf lbl Is lblForCompany Then
            frm.chqSetVar = ChequeSettingVar.ForCompany
            frm.fontSize = objforCompany.fontSize
            frm.charPerLine = objforCompany.charPerLine
            frm.decimalPlace = objforCompany.decimalPlace
            frm.fontName = objforCompany.fontName
            frm.isBold = objforCompany.isBold
            frm.isVisible = objforCompany.IsVisible
            frm.DateStyle = objforCompany.DateStyle
            frm.isxxxxxxAfter = objforCompany.isxxxxxxAfter
            frm.isxxxxxxBefore = objforCompany.isxxxxxxBefore
        ElseIf lbl Is lblSign Then
            frm.chqSetVar = ChequeSettingVar.Sign
            frm.fontSize = objSign.fontSize
            frm.charPerLine = objSign.charPerLine
            frm.decimalPlace = objSign.decimalPlace
            frm.fontName = objSign.fontName
            frm.isBold = objSign.isBold
            frm.isVisible = objSign.IsVisible
            frm.DateStyle = objSign.DateStyle
            frm.isxxxxxxAfter = objSign.isxxxxxxAfter
            frm.isxxxxxxBefore = objSign.isxxxxxxBefore
        ElseIf lbl Is lblNotOver Then
            frm.chqSetVar = ChequeSettingVar.NotOver
            frm.fontSize = objNotOver.fontSize
            frm.charPerLine = objNotOver.charPerLine
            frm.decimalPlace = objNotOver.decimalPlace
            frm.fontName = objNotOver.fontName
            frm.isBold = objNotOver.isBold
            frm.isVisible = objNotOver.IsVisible
            frm.DateStyle = objNotOver.DateStyle
            frm.isxxxxxxAfter = objNotOver.isxxxxxxAfter
            frm.isxxxxxxBefore = objNotOver.isxxxxxxBefore
        ElseIf lbl Is lblNotForHighValue Then
            frm.chqSetVar = ChequeSettingVar.NotForHighValue
            frm.fontSize = objNotForHighValue.fontSize
            frm.charPerLine = objNotForHighValue.charPerLine
            frm.decimalPlace = objNotForHighValue.decimalPlace
            frm.fontName = objNotForHighValue.fontName
            frm.isBold = objNotForHighValue.isBold
            frm.isVisible = objNotForHighValue.IsVisible
            frm.DateStyle = objNotForHighValue.DateStyle
            frm.isxxxxxxAfter = objNotForHighValue.isxxxxxxAfter
            frm.isxxxxxxBefore = objNotForHighValue.isxxxxxxBefore
        ElseIf lbl Is lblAccNo Then
            frm.chqSetVar = ChequeSettingVar.AccountNo
            frm.fontSize = objAccNo.fontSize
            frm.charPerLine = objAccNo.charPerLine
            frm.decimalPlace = objAccNo.decimalPlace
            frm.fontName = objAccNo.fontName
            frm.isBold = objAccNo.isBold
            frm.isVisible = objAccNo.IsVisible
            frm.DateStyle = objAccNo.DateStyle
            frm.isxxxxxxAfter = objAccNo.isxxxxxxAfter
            frm.isxxxxxxBefore = objAccNo.isxxxxxxBefore
        End If
    End Sub

    Public Function saveSetting(ByVal isShowMsg As Boolean) As Boolean
        Try
            Dim arrLable As New List(Of clsLableSetting)()
            arrLable.Add(objAccPay)
            arrLable.Add(objAmt)
            arrLable.Add(objAmtwrd1)
            arrLable.Add(objAmtwrd2)
            arrLable.Add(objDate)
            arrLable.Add(objforCompany)
            arrLable.Add(objLine1)
            arrLable.Add(objLine2)
            arrLable.Add(objName1)
            arrLable.Add(objName2)
            arrLable.Add(objNotOver)
            arrLable.Add(objSign)
            arrLable.Add(objNotForHighValue)
            arrLable.Add(objAccNo)

            For Each obj As clsLableSetting In arrLable
                obj.pageLeft = txtLftMargin.Value
                obj.pageTop = txttpMargin.Value
                If rdoPotrait.IsChecked Then
                    obj.PaperOrientation = PaperOrientation.Portrait
                ElseIf rdoLandscape.IsChecked Then
                    obj.PaperOrientation = PaperOrientation.Landscape
                End If
            Next
            clsLableSetting.SaveData(strBankCode, arrLable)
            If isShowMsg Then
                clsCommon.MyMessageBoxShow(Me, "Setting saved successfully", Me.Text)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Sub aisButton3_Click(ByVal sender As Object, ByVal e As EventArgs)
        LoadData(strBankCode)
    End Sub

    Public Sub NewData()
        objName1 = New clsLableSetting()
        objName2 = New clsLableSetting()
        objAmt = New clsLableSetting()
        objDate = New clsLableSetting()
        objAmtwrd1 = New clsLableSetting()
        objAmtwrd2 = New clsLableSetting()
        objforCompany = New clsLableSetting()
        objSign = New clsLableSetting()
        objNotOver = New clsLableSetting()
        objAccPay = New clsLableSetting()
        objLine1 = New clsLableSetting()
        objLine2 = New clsLableSetting()
        objNotForHighValue = New clsLableSetting()
        objAccNo = New clsLableSetting()

        strBankCode = ""
        lblBank.Text = ""
        txtLftMargin.Value = 0
        txttpMargin.Value = 0

        rdoPotrait.IsChecked = True
        ' 
        ' lblAcPay
        ' 
        Me.lblAcPay.Location = New System.Drawing.Point(44, 131)
        Me.lblAcPay.Size = New System.Drawing.Size(80, 16)
        Me.lblAcPay.Text = "A/c Payee"
        ' 
        ' lblForCompany
        ' 
        Me.lblForCompany.Location = New System.Drawing.Point(381, 130)
        Me.lblForCompany.Size = New System.Drawing.Size(179, 16)
        Me.lblForCompany.Text = "for company"
        ' 
        ' lblSign
        ' 
        Me.lblSign.Location = New System.Drawing.Point(381, 169)
        Me.lblSign.Size = New System.Drawing.Size(179, 16)
        Me.lblSign.Text = "Authorized"
        ' 
        ' lblLine1
        ' 
        Me.lblLine1.Location = New System.Drawing.Point(191, 149)
        Me.lblLine1.Size = New System.Drawing.Size(172, 16)
        Me.lblLine1.Text = "_________________________"
        ' 
        ' lblLine2
        ' 
        Me.lblLine2.Location = New System.Drawing.Point(191, 164)
        Me.lblLine2.Size = New System.Drawing.Size(172, 16)
        Me.lblLine2.Text = "_________________________"
        ' 
        ' lblAmtWrd1
        ' 
        Me.lblAmtWrd1.Location = New System.Drawing.Point(130, 77)
        Me.lblAmtWrd1.Size = New System.Drawing.Size(172, 16)
        Me.lblAmtWrd1.Text = "Amount in Words  Line1"
        ' 
        ' lblNotOver
        ' 
        Me.lblNotOver.Location = New System.Drawing.Point(44, 149)
        Me.lblNotOver.Size = New System.Drawing.Size(135, 16)
        Me.lblNotOver.Text = "Not Over Rs"
        ' 
        ' lblAmtWrd2
        ' 
        Me.lblAmtWrd2.Location = New System.Drawing.Point(130, 95)
        Me.lblAmtWrd2.Size = New System.Drawing.Size(172, 16)
        Me.lblAmtWrd2.Text = "Amount in Words  Line2"
        ' 
        ' lblNameline2
        ' 
        Me.lblNameline2.Location = New System.Drawing.Point(44, 59)
        Me.lblNameline2.Size = New System.Drawing.Size(126, 16)
        Me.lblNameline2.Text = "Name Line2"
        ' 
        ' lblAmt
        ' 
        Me.lblAmt.Location = New System.Drawing.Point(508, 95)
        Me.lblAmt.Size = New System.Drawing.Size(80, 16)
        Me.lblAmt.Text = "Amount"
        ' 
        ' lblDate
        ' 
        Me.lblDate.Location = New System.Drawing.Point(533, 23)
        Me.lblDate.Size = New System.Drawing.Size(80, 16)
        Me.lblDate.Text = "Date"
        ' 
        ' lblNameline1
        ' 
        Me.lblNameline1.Location = New System.Drawing.Point(44, 40)
        Me.lblNameline1.Size = New System.Drawing.Size(126, 16)
        Me.lblNameline1.Text = "Name Line1"

        ' 
        ' lblNameline1
        ' 
        Me.lblNotForHighValue.Location = New System.Drawing.Point(44, 168)
        Me.lblNotForHighValue.Size = New System.Drawing.Size(135, 16)
        Me.lblNotForHighValue.Text = "Not For High Value"

        ' 
        ' lblNameline1
        ' 
        Me.lblAccNo.Location = New System.Drawing.Point(44, 112)
        Me.lblAccNo.Size = New System.Drawing.Size(63, 16)
        Me.lblAccNo.Text = "A/c No."
    End Sub

    Private Sub lblNameline_MouseHover(ByVal sender As Object, ByVal e As EventArgs) Handles lblAcPay.MouseHover, lblForCompany.MouseHover, lblSign.MouseHover, lblLine1.MouseHover, lblLine2.MouseHover, lblAmtWrd1.MouseHover, lblNotForHighValue.MouseHover, lblAccNo.MouseHover, lblNotOver.MouseHover, lblAmtWrd2.MouseHover, lblNameline2.MouseHover, lblAmt.MouseHover, lblDate.MouseHover, lblNameline1.MouseHover
        lbl = DirectCast(sender, common.Controls.MyLabel)
        'lbl.BorderStyle = BorderStyle.Fixed3D
    End Sub

    Private Sub lblNameline_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles lblAcPay.MouseLeave, lblForCompany.MouseLeave, lblSign.MouseLeave, lblLine1.MouseLeave, lblLine2.MouseLeave, lblAmtWrd1.MouseLeave, lblNotForHighValue.MouseLeave, lblAccNo.MouseLeave, lblNotOver.MouseLeave, lblAmtWrd2.MouseLeave, lblNameline2.MouseLeave, lblAmt.MouseLeave, lblDate.MouseLeave, lblNameline1.MouseLeave
        lbl = DirectCast(sender, common.Controls.MyLabel)
        'lbl.BorderStyle = BorderStyle.None
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        saveSetting(True)
    End Sub



    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click
        '
        'lblAcPay
        '
        Me.lblAcPay.AllowDrop = True
        Me.lblAcPay.AutoSize = False
        Me.lblAcPay.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcPay.Location = New System.Drawing.Point(38, 106)
        Me.lblAcPay.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblAcPay.Name = "lblAcPay"
        Me.lblAcPay.Size = New System.Drawing.Size(70, 18)
        Me.lblAcPay.TabIndex = 3
        Me.lblAcPay.Text = "A/c Payee"
        '
        'lblForCompany
        '
        Me.lblForCompany.AllowDrop = True
        Me.lblForCompany.AutoSize = False
        Me.lblForCompany.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblForCompany.Location = New System.Drawing.Point(327, 106)
        Me.lblForCompany.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblForCompany.Name = "lblForCompany"
        Me.lblForCompany.Size = New System.Drawing.Size(87, 18)
        Me.lblForCompany.TabIndex = 3
        Me.lblForCompany.Text = "for company"
        '
        'lblSign
        '
        Me.lblSign.AllowDrop = True
        Me.lblSign.AutoSize = False
        Me.lblSign.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSign.Location = New System.Drawing.Point(327, 137)
        Me.lblSign.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblSign.Name = "lblSign"
        Me.lblSign.Size = New System.Drawing.Size(77, 18)
        Me.lblSign.TabIndex = 3
        Me.lblSign.Text = "Authorized"
        '
        'lblLine1
        '
        Me.lblLine1.AllowDrop = True
        Me.lblLine1.AutoSize = False
        Me.lblLine1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLine1.Location = New System.Drawing.Point(164, 121)
        Me.lblLine1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblLine1.Name = "lblLine1"
        Me.lblLine1.Size = New System.Drawing.Size(193, 18)
        Me.lblLine1.TabIndex = 3
        Me.lblLine1.Text = "_________________________"
        Me.lblLine1.Visible = False
        '
        'lblLine2
        '
        Me.lblLine2.AllowDrop = True
        Me.lblLine2.AutoSize = False
        Me.lblLine2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLine2.Location = New System.Drawing.Point(164, 133)
        Me.lblLine2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblLine2.Name = "lblLine2"
        Me.lblLine2.Size = New System.Drawing.Size(193, 18)
        Me.lblLine2.TabIndex = 3
        Me.lblLine2.Text = "_________________________"
        Me.lblLine2.Visible = False
        '
        'lblAmtWrd1
        '
        Me.lblAmtWrd1.AllowDrop = True
        Me.lblAmtWrd1.AutoSize = False
        Me.lblAmtWrd1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtWrd1.Location = New System.Drawing.Point(111, 63)
        Me.lblAmtWrd1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblAmtWrd1.Name = "lblAmtWrd1"
        Me.lblAmtWrd1.Size = New System.Drawing.Size(161, 18)
        Me.lblAmtWrd1.TabIndex = 3
        Me.lblAmtWrd1.Text = "Amount in Words  Line1"
        '
        'lblNotForHighValue
        '
        Me.lblNotForHighValue.AllowDrop = True
        Me.lblNotForHighValue.AutoSize = False
        Me.lblNotForHighValue.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotForHighValue.Location = New System.Drawing.Point(38, 136)
        Me.lblNotForHighValue.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblNotForHighValue.Name = "lblNotForHighValue"
        Me.lblNotForHighValue.Size = New System.Drawing.Size(127, 18)
        Me.lblNotForHighValue.TabIndex = 3
        Me.lblNotForHighValue.Text = "Not For High Value"
        '
        'lblAccNo
        '
        Me.lblAccNo.AllowDrop = True
        Me.lblAccNo.AutoSize = False
        Me.lblAccNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccNo.Location = New System.Drawing.Point(38, 91)
        Me.lblAccNo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblAccNo.Name = "lblAccNo"
        Me.lblAccNo.Size = New System.Drawing.Size(53, 18)
        Me.lblAccNo.TabIndex = 3
        Me.lblAccNo.Text = "A/c No."
        '
        'lblNotOver
        '
        Me.lblNotOver.AllowDrop = True
        Me.lblNotOver.AutoSize = False
        Me.lblNotOver.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotOver.Location = New System.Drawing.Point(38, 121)
        Me.lblNotOver.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblNotOver.Name = "lblNotOver"
        Me.lblNotOver.Size = New System.Drawing.Size(119, 18)
        Me.lblNotOver.TabIndex = 3
        Me.lblNotOver.Text = "Not Over Rupees "
        '
        'lblAmtWrd2
        '
        Me.lblAmtWrd2.AllowDrop = True
        Me.lblAmtWrd2.AutoSize = False
        Me.lblAmtWrd2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtWrd2.Location = New System.Drawing.Point(111, 77)
        Me.lblAmtWrd2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblAmtWrd2.Name = "lblAmtWrd2"
        Me.lblAmtWrd2.Size = New System.Drawing.Size(161, 18)
        Me.lblAmtWrd2.TabIndex = 3
        Me.lblAmtWrd2.Text = "Amount in Words  Line2"
        '
        'lblNameline2
        '
        Me.lblNameline2.AllowDrop = True
        Me.lblNameline2.AutoSize = False
        Me.lblNameline2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNameline2.Location = New System.Drawing.Point(38, 48)
        Me.lblNameline2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblNameline2.Name = "lblNameline2"
        Me.lblNameline2.Size = New System.Drawing.Size(82, 18)
        Me.lblNameline2.TabIndex = 3
        Me.lblNameline2.Text = "Name Line2"
        '
        'lblAmt
        '
        Me.lblAmt.AllowDrop = True
        Me.lblAmt.AutoSize = False
        Me.lblAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmt.Location = New System.Drawing.Point(435, 77)
        Me.lblAmt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblAmt.Name = "lblAmt"
        Me.lblAmt.Size = New System.Drawing.Size(57, 18)
        Me.lblAmt.TabIndex = 3
        Me.lblAmt.Text = "Amount"
        '
        'lblDate
        '
        Me.lblDate.AllowDrop = True
        Me.lblDate.AutoSize = False
        Me.lblDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(457, 19)
        Me.lblDate.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(36, 18)
        Me.lblDate.TabIndex = 3
        Me.lblDate.Text = "Date"
        '
        'lblNameline1
        '
        Me.lblNameline1.AllowDrop = True
        Me.lblNameline1.AutoSize = False
        Me.lblNameline1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNameline1.Location = New System.Drawing.Point(38, 29)
        Me.lblNameline1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblNameline1.Name = "lblNameline1"
        Me.lblNameline1.Size = New System.Drawing.Size(82, 18)
        Me.lblNameline1.TabIndex = 3
        Me.lblNameline1.Text = "Name Line1"
    End Sub
End Class

Public Class clsLableSetting
#Region "Variable"
    Public CodeValue As ChequeSettingVar
    Public caption As String = Nothing
    Public fontName As String = Nothing
    Public top As Double = 0
    Public left As Double = 0
    Public width As Double = 0
    Public height As Double = 0
    Public charPerLine As Integer = 0
    Public decimalPlace As Integer = 0
    Public fontSize As Double = 0
    Public isBold As Boolean = False
    Public pageTop As Double = 0
    Public pageLeft As Double = 0
    Public IsVisible As Integer = 1
    Public DateStyle As String = clsDateStyle.ddMMyyyy_Desh
    Public PaperOrientation As PaperOrientation = PaperOrientation.Portrait
    Public LblType As PrintingLableType = PrintingLableType.Caption

    Public isxxxxxxBefore As Boolean = False
    Public isxxxxxxAfter As Boolean = False
    Public CharSpace As Integer = 0
#End Region

    Public Shared Function getDateStyleDataSet() As DataSet
        Dim ds As New DataSet("dstyle")
        Dim tbl As New DataTable("DateStyle")

        tbl.Columns.Add(New DataColumn("Code", GetType(String)))
        tbl.Columns.Add(New DataColumn("Name", GetType(String)))

        Dim dr As DataRow = tbl.NewRow()
        dr("Code") = "dd-MMM-yyyy"
        dr("Name") = "dd-MMM-yyyy"
        tbl.Rows.Add(dr)

        dr = tbl.NewRow()
        dr("Code") = "dd/MMM/yyyy"
        dr("Name") = "dd/MMM/yyyy"
        tbl.Rows.Add(dr)

        dr = tbl.NewRow()
        dr("Code") = "dd-MM-yyyy"
        dr("Name") = "dd-MM-yyyy"
        tbl.Rows.Add(dr)

        dr = tbl.NewRow()
        dr("Code") = "dd/MM/yyyy"
        dr("Name") = "dd/MM/yyyy"
        tbl.Rows.Add(dr)

        dr = tbl.NewRow()
        dr("Code") = "dd-MM-yy"
        dr("Name") = "dd-MM-yy"
        tbl.Rows.Add(dr)

        dr = tbl.NewRow()
        dr("Code") = "dd/MM/yy"
        dr("Name") = "dd/MM/yy"
        tbl.Rows.Add(dr)

        dr = tbl.NewRow()
        dr("Code") = "ddMMMyyyy"
        dr("Name") = "ddMMMyyyy"
        tbl.Rows.Add(dr)

        dr = tbl.NewRow()
        dr("Code") = "ddMMyyyy"
        dr("Name") = "ddMMyyyy"
        tbl.Rows.Add(dr)

        dr = tbl.NewRow()
        dr("Code") = "ddMMyy"
        dr("Name") = "ddMMyy"
        tbl.Rows.Add(dr)

        ds.Tables.Add(tbl)

        Return ds
    End Function

    Public Shared Function ReturnData(ByVal bcode As String) As List(Of clsLableSetting)
        Try
            Dim arrLable As New List(Of clsLableSetting)()
            Dim objLable As clsLableSetting

            Dim qry As String = "select * from TSPL_CHEQUE_SETTING where BCODE = '" + bcode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing Then
                For Each dr As DataRow In dt.Rows
                    objLable = New clsLableSetting()
                    objLable.CodeValue = DirectCast(CInt(clsCommon.myCdbl(dr("CodeValue"))), ChequeSettingVar)
                    objLable.caption = clsCommon.myCstr(dr("Name"))
                    objLable.fontName = clsCommon.myCstr(dr("FontName"))
                    objLable.fontSize = clsCommon.myCdbl(dr("FontSize"))
                    objLable.top = clsCommon.myCdbl(dr("FrmTop"))
                    objLable.left = clsCommon.myCdbl(dr("FrmLeft"))
                    objLable.height = clsCommon.myCdbl(dr("FrmHeight"))
                    objLable.width = clsCommon.myCdbl(dr("FrmWidth"))
                    objLable.charPerLine = CInt(clsCommon.myCdbl(dr("CharPerLine")))
                    objLable.decimalPlace = CInt(clsCommon.myCdbl(dr("DecimalPlaces")))
                    objLable.pageLeft = clsCommon.myCdbl(dr("PageLeft"))
                    objLable.pageTop = clsCommon.myCdbl(dr("PageTop"))

                    objLable.PaperOrientation = DirectCast(CInt(clsCommon.myCdbl(dr("PaperOrientation"))), PaperOrientation)
                    objLable.DateStyle = clsCommon.myCstr(dr("DateStyle"))
                    objLable.IsVisible = clsCommon.myCdbl(dr("IsVisible"))

                    objLable.isxxxxxxAfter = IIf(clsCommon.myCdbl(dr("isxxxxxxAfter")) = 1, True, False)
                    objLable.isxxxxxxBefore = IIf(clsCommon.myCdbl(dr("isxxxxxxBefore")) = 1, True, False)
                    objLable.CharSpace = clsCommon.myCdbl(dr("CharSpace"))

                    If CInt(clsCommon.myCdbl(dr("isBold"))) = 1 Then
                        objLable.isBold = True
                    Else
                        objLable.isBold = False
                    End If

                    arrLable.Add(objLable)
                Next
            End If
            Return arrLable
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal bcode As String, ByVal arrLableSet As List(Of clsLableSetting)) As Boolean
        Dim trans As SqlClient.SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_CHEQUE_SETTING where BCODE = '" + bcode + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As Hashtable
            For Each objLable As clsLableSetting In arrLableSet
                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "BCODE", bcode)
                clsCommon.AddColumnsForChange(coll, "CodeValue", CInt(objLable.CodeValue))
                If clsCommon.myLen(objLable.caption) = 0 Then
                    objLable.caption = ""
                End If
                clsCommon.AddColumnsForChange(coll, "Name", objLable.caption)
                clsCommon.AddColumnsForChange(coll, "FrmTop", objLable.top)
                clsCommon.AddColumnsForChange(coll, "FrmHeight", objLable.height)
                clsCommon.AddColumnsForChange(coll, "FrmLeft", objLable.left)
                clsCommon.AddColumnsForChange(coll, "FrmWidth", objLable.width)
                clsCommon.AddColumnsForChange(coll, "FontName", objLable.fontName)
                clsCommon.AddColumnsForChange(coll, "FontSize", objLable.fontSize)

                If objLable.isBold Then
                    clsCommon.AddColumnsForChange(coll, "isBold", 1)
                Else
                    clsCommon.AddColumnsForChange(coll, "isBold", 0)
                End If
                clsCommon.AddColumnsForChange(coll, "CharPerLine", objLable.charPerLine)
                clsCommon.AddColumnsForChange(coll, "DecimalPlaces", objLable.decimalPlace)
                clsCommon.AddColumnsForChange(coll, "PageTop", objLable.pageTop)
                clsCommon.AddColumnsForChange(coll, "PageLeft", objLable.pageLeft)
                If objLable.PaperOrientation = PaperOrientation.DefaultPaperOrientation Then
                    objLable.PaperOrientation = PaperOrientation.Portrait
                End If
                clsCommon.AddColumnsForChange(coll, "PaperOrientation", CInt(objLable.PaperOrientation))
                If clsCommon.myLen(objLable.DateStyle) = 0 Then
                    objLable.DateStyle = clsDateStyle.ddMMMyyyy_Slash
                End If
                clsCommon.AddColumnsForChange(coll, "DateStyle", objLable.DateStyle)
                clsCommon.AddColumnsForChange(coll, "IsVisible", objLable.IsVisible)

                clsCommon.AddColumnsForChange(coll, "isxxxxxxBefore", IIf(objLable.isxxxxxxBefore, 1, 0))
                clsCommon.AddColumnsForChange(coll, "isxxxxxxAfter", IIf(objLable.isxxxxxxAfter, 1, 0))
                clsCommon.AddColumnsForChange(coll, "CharSpace", objLable.CharSpace)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CHEQUE_SETTING", OMInsertOrUpdate.Insert, "", trans)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message, ex)
        End Try
    End Function
End Class

Public Class clsrptChequePrint
#Region "Variables"
    Private printAmt As Double = 0
    Private Name As String = Nothing
    Private chequeDate As String = Nothing
    Private company As String = Nothing
    Private leftMargin As Integer = 0
    Private topMargin As Integer = 0
    Private objrptCheque As rptCheque = Nothing
    Private arrLableSetting As List(Of clsLableSetting) = Nothing
#End Region


    Public Sub PrintCheque(ByVal dsData As DataTable, ByVal isAcPayee As Boolean, ByVal isNotForHighValue As Boolean)
        Try
            objrptCheque = New rptCheque()
            objrptCheque.PrintOptions.PaperSize = CrystalDecisions.[Shared].PaperSize.PaperA4
            objrptCheque.Section3.SectionFormat.EnableSuppress = False
            Dim amtLen As Integer = 0
            Dim AmtLine As Boolean = False
            If dsData IsNot Nothing AndAlso dsData.Rows.Count > 0 Then
                Dim qry As String = "select * from TSPL_CHEQUE_SETTING where BCODE='" + clsCommon.myCstr(dsData.Rows(0)("Bank_Code")) + "' " + " order by FrmTop, FrmLeft "
                Dim dsSetting As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dsSetting IsNot Nothing AndAlso dsSetting.Rows.Count > 0 Then
                    If DirectCast(CInt(clsCommon.myCdbl(dsSetting.Rows(0)("PaperOrientation"))), PaperOrientation) = PaperOrientation.Portrait Then
                        objrptCheque.PrintOptions.PaperOrientation = CrystalDecisions.[Shared].PaperOrientation.Portrait
                    ElseIf DirectCast(CInt(clsCommon.myCdbl(dsSetting.Rows(0)("PaperOrientation"))), PaperOrientation) = PaperOrientation.Landscape Then
                        objrptCheque.PrintOptions.PaperOrientation = CrystalDecisions.[Shared].PaperOrientation.Landscape
                    End If

                    Dim lm As Double = clsCommon.myCdbl(dsSetting.Rows(0)("PAGELEFT"))
                    leftMargin = CInt(lm * 1440.0)

                    Dim tm As Double = clsCommon.myCdbl(dsSetting.Rows(0)("PAGETOP"))
                    topMargin = CInt(tm * 1440.0)

                    For Each dr As DataRow In dsSetting.Rows
                        Dim fld As ChequeSettingVar = DirectCast(CInt(clsCommon.myCdbl(dr("CodeValue"))), ChequeSettingVar)
                        Dim txt As CrystalDecisions.CrystalReports.Engine.TextObject = Nothing

                        Dim isVisible As Boolean = clsCommon.myCBool(dr("isVisible"))

                        If fld = ChequeSettingVar.AccountPay Then
                            isVisible = isAcPayee
                        End If
                        If fld = ChequeSettingVar.NotForHighValue Then
                            isVisible = isNotForHighValue
                        End If

                        If isVisible Then
                            Dim PayTo As String = clsCommon.myCstr(dsData.Rows(0)("Pay_To"))
                            Dim charPerline As Integer = CInt(clsCommon.myCdbl(dr("CharPerLine")))
                            Dim amt As Double = clsCommon.myCdbl(dsData.Rows(0)("Check_Amount"))
                            Dim amtInWords As String = clsCommon.changeCurrencyToWords(amt)
                            Select Case fld
                                Case ChequeSettingVar.NameLine1
                                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtNameLine1"), CrystalDecisions.CrystalReports.Engine.TextObject)
                                    txt.Text = getLineText(PayTo, charPerline)
                                    Exit Select
                                Case ChequeSettingVar.NameLine2
                                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtNameLine2"), CrystalDecisions.CrystalReports.Engine.TextObject)
                                    Dim printStr As String = getLineText(PayTo, charPerline)
                                    txt.Text = PayTo.Substring(clsCommon.myLen(printStr))
                                    Exit Select
                                Case ChequeSettingVar.Amount
                                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAmt"), CrystalDecisions.CrystalReports.Engine.TextObject)
                                    txt.Text = clsCommon.myFormat(clsCommon.myCdbl(dsData.Rows(0)("Check_Amount")))
                                    Exit Select
                                Case ChequeSettingVar.[Date]
                                    Dim strSpace As String = clsCommon.myCstr(CInt(clsCommon.myCdbl(dr("CharSpace"))))
                                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtDate" + strSpace), CrystalDecisions.CrystalReports.Engine.TextObject)
                                    Dim DateStyle As String = clsCommon.myCstr(dr("DateStyle"))
                                    txt.Text = clsCommon.GetPrintDate(clsCommon.myCDate(dsData.Rows(0)("check_Date")), DateStyle)
                                    Exit Select
                                Case ChequeSettingVar.Amtword1
                                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAmtinWrd1"), CrystalDecisions.CrystalReports.Engine.TextObject)
                                    txt.Text = getLineText(amtInWords, charPerline)
                                    Exit Select
                                Case ChequeSettingVar.Amtword2
                                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAmtinWrd2"), CrystalDecisions.CrystalReports.Engine.TextObject)
                                    Dim printamt As String = getLineText(amtInWords, charPerline)
                                    txt.Text = clsCommon.myCstr(amtInWords).Substring(CInt(clsCommon.myLen(printamt)))
                                    Exit Select
                                Case ChequeSettingVar.ForCompany
                                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtForCmp"), CrystalDecisions.CrystalReports.Engine.TextObject)
                                    txt.Text = clsCommon.myCstr(dr("Name"))
                                    Exit Select
                                Case ChequeSettingVar.Sign
                                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAuthSign"), CrystalDecisions.CrystalReports.Engine.TextObject)
                                    txt.Text = clsCommon.myCstr(dr("Name"))
                                    Exit Select
                                Case ChequeSettingVar.NotOver
                                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtNotOver"), CrystalDecisions.CrystalReports.Engine.TextObject)
                                    Dim caption As String = clsCommon.myCstr(dr("Name")).Trim()
                                    If clsCommon.myLen(caption) > 0 Then
                                        ' caption = (caption & Convert.ToString(": ")) + clsCommon.myFormat(amt + 1)
                                        caption = (caption & Convert.ToString(": ")) + clsCommon.myFormat(amt)
                                    End If
                                    txt.Text = caption
                                    Exit Select
                                Case ChequeSettingVar.AccountPay
                                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAcPay"), CrystalDecisions.CrystalReports.Engine.TextObject)
                                    txt.Text = clsCommon.myCstr(dr("Name"))
                                    Exit Select
                                Case ChequeSettingVar.Line1
                                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtLine1"), CrystalDecisions.CrystalReports.Engine.TextObject)
                                    'txt.Text = clsCommon.mycstr(dr["Name"]);
                                    txt.Text = ""
                                    Exit Select
                                Case ChequeSettingVar.Line2
                                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtLine2"), CrystalDecisions.CrystalReports.Engine.TextObject)
                                    'txt.Text = clsCommon.mycstr(dr["Name"]);
                                    txt.Text = ""
                                    Exit Select
                                Case ChequeSettingVar.AccountNo
                                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAcNo"), CrystalDecisions.CrystalReports.Engine.TextObject)
                                    txt.Text = clsCommon.myCstr(dr("Name"))
                                    Exit Select
                                Case ChequeSettingVar.NotForHighValue
                                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtNotForHighValue"), CrystalDecisions.CrystalReports.Engine.TextObject)
                                    txt.Text = clsCommon.myCstr(dr("Name"))
                                    Exit Select
                            End Select

                            If clsCommon.myLen(txt.Text) > 0 Then
                                If clsCommon.myCdbl(dr("isxxxxxxBefore")) = 1 Then
                                    txt.Text = "xxxx" + txt.Text
                                End If
                                If clsCommon.myCdbl(dr("isxxxxxxAfter")) = 1 Then
                                    txt.Text = txt.Text + "xxxx"
                                End If
                            End If


                            txt.ObjectFormat.EnableSuppress = False

                            Dim fontName As String = clsCommon.myCstr(dr("FontName"))
                            Dim fontSize As Single = CSng(clsCommon.myCdbl(dr("FontSize")))
                            Dim fontStyle As System.Drawing.FontStyle = System.Drawing.FontStyle.Regular
                            If CInt(clsCommon.myCdbl(dr("isBold"))) = 1 Then
                                fontStyle = System.Drawing.FontStyle.Bold
                            End If
                            If clsCommon.myLen(fontName) = 0 Then
                                fontName = "Arial"
                            End If
                            If fontSize = 0 Then
                                fontSize = 9.75F
                            End If

                            Dim wwidth As Integer = 0

                            txt.Left = CInt(clsCommon.myCdbl(dr("FrmLeft")) * 1440)
                            txt.Top = CInt(clsCommon.myCdbl(dr("FrmTop")) * 1440)
                            'txt.Height = (int)clsCommon.myCdbl(dr["FrmHeight"]) * 1440;

                            wwidth = CInt(clsCommon.myCdbl(dr("FrmWidth")) * 1440)
                            Dim txtwidth As Integer = CInt(fontSize * CInt(clsCommon.myLen(txt.Text))) * 11

                            If wwidth < txtwidth AndAlso ((txt.Left + txtwidth) < (8 * 1440)) Then
                                txt.Width = txtwidth
                            Else
                                txt.Width = wwidth
                            End If

                            txt.ApplyFont(New System.Drawing.Font(fontName, fontSize, fontStyle))

                        End If
                    Next
                End If
            End If
            clsAisCrystalReportPageMargin.ShowCrystalReport(objrptCheque, Nothing, False, leftMargin, topMargin, True, "")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Function getLineText(ByVal pStr As String, ByVal charPerline As Integer) As String
        Dim strlinetxt As String = ""
        Try
            Dim len As Integer = CInt(clsCommon.myLen(pStr))
            If len >= charPerline Then
                If charPerline > 0 Then
                    Dim charStr As String = pStr.Substring(0, charPerline)
                    Dim lastwordIdx As Integer = charStr.LastIndexOf(" "c)
                    If lastwordIdx = -1 Then
                        strlinetxt = charStr
                    Else
                        strlinetxt = charStr.Substring(0, lastwordIdx)
                    End If
                Else
                    strlinetxt = pStr
                End If
            Else
                strlinetxt = pStr
            End If
        Catch
        End Try
        Return strlinetxt
    End Function
End Class

Public Enum PaperOrientation
    DefaultPaperOrientation = 0
    Portrait = 1
    Landscape = 2
End Enum

Public Enum PrintingLableType
    Caption = 1
    Field = 2
    Space = 3
End Enum

Public Enum ChequeSettingVar
    NameLine1 = 0
    NameLine2 = 1
    Amount = 2
    [Date] = 3
    Amtword1 = 4
    Amtword2 = 5
    ForCompany = 6
    Sign = 7
    NotOver = 8
    AccountPay = 9
    Line1 = 10
    Line2 = 11
    AccountNo = 12
    NotForHighValue = 13
End Enum


Public NotInheritable Class clsDateStyle
#Region "Variables"
    Public Const ddMMMyyyy_Desh As String = "dd-MMM-yyyy"
    Public Const ddMMMyyyy_Slash As String = "dd/MMM/yyyy"
    Public Const ddMMyyyy_Desh As String = "dd-MM-yyyy"
    Public Const ddMMyyyy_Slash As String = "dd/MM/yyyy"
    Public Const ddMMyy_Desh As String = "dd-MM-yy"
    Public Const ddMMyy_Slash As String = "dd/MM/yy"
    Public Const ddMMMyyyy As String = "ddMMMyyyy"
    Public Const ddMMyyyy As String = "ddMMyyyy"
    Public Const ddMMyy As String = "ddMMyy"
#End Region

    Private Sub New()
    End Sub
End Class


Public Class clsAisCrystalReportPageMargin
#Region "Variables"
    Public Left As Integer
    Public Top As Integer
    Public Bottom As Integer
    Public Right As Integer
#End Region

    Public Shared Sub ShowCrystalReport(ByVal Report As CrystalDecisions.CrystalReports.Engine.ReportClass, ByVal frm As RadForm, ByVal MDI As Boolean, ByVal left As Integer, ByVal top As Integer, ByVal right As Integer, ByVal bottom As Integer, ByVal preview As Boolean, ByVal caption As String)
        Dim objPageSett As New clsAisCrystalReportPageMargin
        objPageSett.Left = left
        objPageSett.Top = top
        objPageSett.Right = right
        objPageSett.Bottom = bottom
        ShowCrystalReport(Report, frm, MDI, objPageSett, preview, caption)
    End Sub

    Public Shared Sub ShowCrystalReport(ByVal Report As CrystalDecisions.CrystalReports.Engine.ReportClass, ByVal frm As RadForm, ByVal MDI As Boolean, ByVal left As Integer, ByVal top As Integer, ByVal preview As Boolean, ByVal caption As String)
        Dim objPageSett As New clsAisCrystalReportPageMargin
        objPageSett.Left = left
        objPageSett.Top = top
        objPageSett.Right = -1
        objPageSett.Bottom = -1
        ShowCrystalReport(Report, frm, MDI, objPageSett, preview, caption)
    End Sub

    Public Shared Sub ShowCrystalReport(ByVal Report As CrystalDecisions.CrystalReports.Engine.ReportClass, ByVal frm As RadForm, ByVal MDI As Boolean, ByVal preview As Boolean, ByVal caption As String)
        ShowCrystalReport(Report, frm, MDI, Nothing, preview, caption)
    End Sub

    Public Shared Sub ShowCrystalReport(ByVal Report As CrystalDecisions.CrystalReports.Engine.ReportClass, ByVal frm As RadForm, ByVal MDI As Boolean, ByVal objPageMargin As clsAisCrystalReportPageMargin, ByVal preview As Boolean, ByVal caption As String)
        Try
            If (objPageMargin IsNot Nothing) Then
                Dim PageMargin As CrystalDecisions.Shared.PageMargins = Report.PrintOptions.PageMargins
                PageMargin.leftMargin = objPageMargin.Left
                PageMargin.topMargin = objPageMargin.Top

                If Not (objPageMargin.Right = -1) Then
                    PageMargin.rightMargin = objPageMargin.Right
                End If
                If Not (objPageMargin.Bottom = -1) Then
                    PageMargin.bottomMargin = objPageMargin.Bottom
                End If
                Report.PrintOptions.ApplyPageMargins(PageMargin)
            End If

            If Not (preview) Then
                Report.PrintToPrinter(1, False, 0, 0)
                Exit Sub
            End If
            Dim objPreviewCrystal As New frmCrystalReportViewer
            objPreviewCrystal.Text = caption
            objPreviewCrystal.objReport = Report

            If (MDI) Then
                objPreviewCrystal.MdiParent = frm
                objPreviewCrystal.Show()
            Else
                objPreviewCrystal.ShowDialog(frm)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

End Class

Public Class clsrptChequePrintMultiple
#Region "Variables"
    Private printAmt As Double = 0
    Private Name As String = Nothing
    Private chequeDate As String = Nothing
    Private company As String = Nothing
    Private leftMargin As Integer = 0
    Private topMargin As Integer = 0
    Private objrptCheque As rptChequeMultiple = Nothing
    Private arrLableSetting As List(Of clsLableSetting) = Nothing
    Dim dsRpt As dsRptChequeMultiple = Nothing
#End Region

    Public Sub PrintCheque(ByVal arr As List(Of clsPrintCheck))
        Try
            objrptCheque = New rptChequeMultiple()
            objrptCheque.PrintOptions.PaperSize = CrystalDecisions.[Shared].PaperSize.PaperA4
            objrptCheque.Section3.SectionFormat.EnableSuppress = False
            Dim amtLen As Integer = 0
            Dim AmtLine As Boolean = False
            Dim dict As New Dictionary(Of ChequeSettingVar, DataRow)

            Dim qry As String = "select * from TSPL_CHEQUE_SETTING where BCODE='" + arr(0).BANK_CODE + "' " + " order by FrmTop, FrmLeft "
            Dim dsSetting As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dsSetting Is Nothing OrElse dsSetting.Rows.Count <= 0 Then
                Throw New Exception("Please set bank check setting for bank " + arr(0).BANK_CODE)
            End If

            If dsSetting IsNot Nothing AndAlso dsSetting.Rows.Count > 0 Then
                If DirectCast(CInt(clsCommon.myCdbl(dsSetting.Rows(0)("PaperOrientation"))), PaperOrientation) = PaperOrientation.Portrait Then
                    objrptCheque.PrintOptions.PaperOrientation = CrystalDecisions.[Shared].PaperOrientation.Portrait
                ElseIf DirectCast(CInt(clsCommon.myCdbl(dsSetting.Rows(0)("PaperOrientation"))), PaperOrientation) = PaperOrientation.Landscape Then
                    objrptCheque.PrintOptions.PaperOrientation = CrystalDecisions.[Shared].PaperOrientation.Landscape
                End If

                Dim lm As Double = clsCommon.myCdbl(dsSetting.Rows(0)("PAGELEFT"))
                leftMargin = CInt(lm * 1440.0)

                Dim tm As Double = clsCommon.myCdbl(dsSetting.Rows(0)("PAGETOP"))
                topMargin = CInt(tm * 1440.0)

                For Each dr As DataRow In dsSetting.Rows
                    Dim fld As ChequeSettingVar = DirectCast(CInt(clsCommon.myCdbl(dr("CodeValue"))), ChequeSettingVar)
                    dict.Add(fld, dr)
                    Dim txt As CrystalDecisions.CrystalReports.Engine.FieldObject = Nothing
                    Dim isVisible As Boolean = clsCommon.myCBool(dr("isVisible"))
                    If isVisible Then
                        Select Case fld
                            Case ChequeSettingVar.NameLine1
                                txt = DirectCast(objrptCheque.Section3.ReportObjects("txtNameLine1"), CrystalDecisions.CrystalReports.Engine.FieldObject)
                                Exit Select
                            Case ChequeSettingVar.NameLine2
                                txt = DirectCast(objrptCheque.Section3.ReportObjects("txtNameLine2"), CrystalDecisions.CrystalReports.Engine.FieldObject)
                                Exit Select
                            Case ChequeSettingVar.Amount
                                txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAmt"), CrystalDecisions.CrystalReports.Engine.FieldObject)
                                Exit Select
                            Case ChequeSettingVar.[Date]
                                Dim strSpace As String = clsCommon.myCstr(CInt(clsCommon.myCdbl(dr("CharSpace"))))
                                txt = DirectCast(objrptCheque.Section3.ReportObjects("txtDate" + strSpace), CrystalDecisions.CrystalReports.Engine.FieldObject)
                                Exit Select
                            Case ChequeSettingVar.Amtword1
                                txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAmtinWrd1"), CrystalDecisions.CrystalReports.Engine.FieldObject)
                                Exit Select
                            Case ChequeSettingVar.Amtword2
                                txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAmtinWrd2"), CrystalDecisions.CrystalReports.Engine.FieldObject)
                                Exit Select
                            Case ChequeSettingVar.ForCompany
                                txt = DirectCast(objrptCheque.Section3.ReportObjects("txtForCmp"), CrystalDecisions.CrystalReports.Engine.FieldObject)
                                Exit Select
                            Case ChequeSettingVar.Sign
                                txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAuthSign"), CrystalDecisions.CrystalReports.Engine.FieldObject)
                                Exit Select
                            Case ChequeSettingVar.NotOver
                                txt = DirectCast(objrptCheque.Section3.ReportObjects("txtNotOver"), CrystalDecisions.CrystalReports.Engine.FieldObject)
                                Exit Select
                            Case ChequeSettingVar.AccountPay
                                txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAcPay"), CrystalDecisions.CrystalReports.Engine.FieldObject)
                                Exit Select
                            Case ChequeSettingVar.Line1
                                txt = DirectCast(objrptCheque.Section3.ReportObjects("txtLine1"), CrystalDecisions.CrystalReports.Engine.FieldObject)
                                Exit Select
                            Case ChequeSettingVar.Line2
                                txt = DirectCast(objrptCheque.Section3.ReportObjects("txtLine2"), CrystalDecisions.CrystalReports.Engine.FieldObject)
                                Exit Select
                            Case ChequeSettingVar.AccountNo
                                txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAcNo"), CrystalDecisions.CrystalReports.Engine.FieldObject)
                                Exit Select
                            Case ChequeSettingVar.NotForHighValue
                                txt = DirectCast(objrptCheque.Section3.ReportObjects("txtNotForHighValue"), CrystalDecisions.CrystalReports.Engine.FieldObject)
                                Exit Select
                        End Select



                        txt.ObjectFormat.EnableSuppress = False

                        Dim fontName As String = clsCommon.myCstr(dr("FontName"))
                        Dim fontSize As Single = CSng(clsCommon.myCdbl(dr("FontSize")))
                        Dim fontStyle As System.Drawing.FontStyle = System.Drawing.FontStyle.Regular
                        If CInt(clsCommon.myCdbl(dr("isBold"))) = 1 Then
                            fontStyle = System.Drawing.FontStyle.Bold
                        End If
                        If clsCommon.myLen(fontName) = 0 Then
                            fontName = "Arial"
                        End If
                        If fontSize = 0 Then
                            fontSize = 9.75F
                        End If

                        Dim wwidth As Integer = 0

                        txt.Left = CInt(clsCommon.myCdbl(dr("FrmLeft")) * 1440)
                        txt.Top = CInt(clsCommon.myCdbl(dr("FrmTop")) * 1440)

                        wwidth = CInt(clsCommon.myCdbl(dr("FrmWidth")) * 1440)
                        'Dim txtwidth As Integer = CInt(fontSize * CInt(clsCommon.myLen(txt.Text))) * 11

                        'If wwidth < txtwidth AndAlso ((txt.Left + txtwidth) < (8 * 1440)) Then
                        'txt.Width = txtwidth
                        'Else
                        txt.Width = wwidth
                        'End If

                        txt.ApplyFont(New System.Drawing.Font(fontName, fontSize, fontStyle))
                    End If


                Next
            End If

            dsRpt = New dsRptChequeMultiple()
            dsRpt.Tables("dt").Clear()
            Dim drRpt As DataRow
            Dim strTemp As String = String.Empty
            For Each objtr As clsPrintCheck In arr
                drRpt = dsRpt.Tables("dt").NewRow()
                drRpt("DocNo") = objtr.DOCUMENT_NO

                strTemp = getLineText(objtr.Pay_To, clsCommon.myCdbl(dict(ChequeSettingVar.NameLine1)("CharPerLine")))
                drRpt("txtNameLine1") = iSxxxBerforeOrAfter(strTemp, dict(ChequeSettingVar.NameLine1))
                strTemp = getLineText(objtr.Pay_To, clsCommon.myCdbl(dict(ChequeSettingVar.NameLine2)("CharPerLine")))
                drRpt("txtNameLine2") = iSxxxBerforeOrAfter(strTemp.Substring(clsCommon.myLen(strTemp)), dict(ChequeSettingVar.NameLine2))
                drRpt("txtAmt") = iSxxxBerforeOrAfter(clsCommon.myFormat(objtr.Amount), dict(ChequeSettingVar.Amount))
                strTemp = clsCommon.myCstr(dict(ChequeSettingVar.[Date])("DateStyle"))
                strTemp = iSxxxBerforeOrAfter(clsCommon.GetPrintDate(objtr.DOCUMENT_Date, strTemp), dict(ChequeSettingVar.[Date]))
                drRpt("txtDate1") = strTemp
                drRpt("txtDate2") = strTemp
                drRpt("txtDate3") = strTemp
                drRpt("txtDate4") = strTemp
                drRpt("txtDate5") = strTemp
                drRpt("txtDate6") = strTemp
                drRpt("txtDate7") = strTemp
                drRpt("txtDate8") = strTemp
                drRpt("txtDate9") = strTemp
                drRpt("txtDate10") = strTemp
                strTemp = getLineText(clsCommon.changeCurrencyToWords(objtr.Amount), clsCommon.myCdbl(dict(ChequeSettingVar.Amtword1)("CharPerLine")))
                drRpt("txtAmtinWrd1") = iSxxxBerforeOrAfter(strTemp, dict(ChequeSettingVar.Amtword1))
                strTemp = getLineText(clsCommon.changeCurrencyToWords(objtr.Amount), clsCommon.myCdbl(dict(ChequeSettingVar.Amtword2)("CharPerLine")))
                drRpt("txtAmtinWrd2") = iSxxxBerforeOrAfter(strTemp.Substring(clsCommon.myLen(strTemp)), dict(ChequeSettingVar.Amtword2))
                drRpt("txtForCmp") = iSxxxBerforeOrAfter(clsCommon.myCstr(dict(ChequeSettingVar.ForCompany)("Name")), dict(ChequeSettingVar.ForCompany))
                drRpt("txtAuthSign") = iSxxxBerforeOrAfter(clsCommon.myCstr(dict(ChequeSettingVar.Sign)("Name")), dict(ChequeSettingVar.Sign))
                strTemp = clsCommon.myCstr(dict(ChequeSettingVar.NotOver)("Name"))
                If clsCommon.myLen(strTemp) > 0 Then
                    strTemp = (strTemp & Convert.ToString(": ")) + clsCommon.myFormat(objtr.Amount + 1)
                End If
                drRpt("txtNotOver") = iSxxxBerforeOrAfter(strTemp, dict(ChequeSettingVar.NameLine2))

                If objtr.Account_Payee = 1 Then
                    drRpt("txtAcPay") = iSxxxBerforeOrAfter(clsCommon.myCstr(dict(ChequeSettingVar.AccountPay)("Name")), dict(ChequeSettingVar.AccountPay))
                End If
                'drRpt("txtLine1") = "" ''objtr.DOCUMENT_NO
                'drRpt("txtLine2") = "" ''objtr.DOCUMENT_NO
                drRpt("txtAcNo") = iSxxxBerforeOrAfter(clsCommon.myCstr(dict(ChequeSettingVar.AccountNo)("Name")), dict(ChequeSettingVar.AccountNo))
                If objtr.Not_For_High_Value = 1 Then
                    drRpt("txtNotForHighValue") = iSxxxBerforeOrAfter(clsCommon.myCstr(dict(ChequeSettingVar.NotForHighValue)("Name")), dict(ChequeSettingVar.NotForHighValue))
                End If
                dsRpt.Tables("dt").Rows.Add(drRpt)
            Next

            objrptCheque.SetDataSource(dsRpt)
            clsAisCrystalReportPageMargin.ShowCrystalReport(objrptCheque, Nothing, False, leftMargin, topMargin, True, "")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Function iSxxxBerforeOrAfter(ByVal strValue As String, ByVal dr As DataRow) As String
        If clsCommon.myLen(strValue) > 0 Then
            If clsCommon.myCdbl(dr("isxxxxxxBefore")) = 1 Then
                strValue = "xxxx" + strValue
            End If
            If clsCommon.myCdbl(dr("isxxxxxxAfter")) = 1 Then
                strValue = strValue + "xxxx"
            End If
        End If
        If Not clsCommon.myCBool(dr("isVisible")) Then
            strValue = ""
        End If
        Return strValue
    End Function




    Private Function getLineText(ByVal pStr As String, ByVal charPerline As Integer) As String
        Dim strlinetxt As String = ""
        Try
            Dim len As Integer = CInt(clsCommon.myLen(pStr))
            If len >= charPerline Then
                If charPerline > 0 Then
                    Dim charStr As String = pStr.Substring(0, charPerline)
                    Dim lastwordIdx As Integer = charStr.LastIndexOf(" "c)
                    If lastwordIdx = -1 Then
                        strlinetxt = charStr
                    Else
                        strlinetxt = charStr.Substring(0, lastwordIdx)
                    End If
                Else
                    strlinetxt = pStr
                End If
            Else
                strlinetxt = pStr
            End If
        Catch
        End Try
        Return strlinetxt
    End Function
End Class





Public Class clsMyPrintDocument
    Inherits RadPrintDocument
    Public Property LeftUpperText() As String
        Get
            Return m_LeftUpperText
        End Get
        Set(value As String)
            m_LeftUpperText = value
        End Set
    End Property
    Private m_LeftUpperText As String
    Public Property LeftUpperFont() As Font
        Get
            Return m_LeftUpperFont
        End Get
        Set(value As Font)
            m_LeftUpperFont = value
        End Set
    End Property
    Private m_LeftUpperFont As Font
    Public Property LeftMiddleText() As String
        Get
            Return m_LeftMiddleText
        End Get
        Set(value As String)
            m_LeftMiddleText = value
        End Set
    End Property
    Private m_LeftMiddleText As String
    Public Property LeftMiddleFont() As Font
        Get
            Return m_LeftMiddleFont
        End Get
        Set(value As Font)
            m_LeftMiddleFont = value
        End Set
    End Property
    Private m_LeftMiddleFont As Font
    Public Property LeftLowerText() As String
        Get
            Return m_LeftLowerText
        End Get
        Set(value As String)
            m_LeftLowerText = value
        End Set
    End Property
    Private m_LeftLowerText As String
    Public Property LeftLowerFont() As Font
        Get
            Return m_LeftLowerFont
        End Get
        Set(value As Font)
            m_LeftLowerFont = value
        End Set
    End Property
    Private m_LeftLowerFont As Font
    Protected Overrides Sub PrintHeader(args As System.Drawing.Printing.PrintPageEventArgs)
        MyBase.PrintHeader(args)
        Dim headerRect As New Rectangle(args.MarginBounds.Location, New Size(args.MarginBounds.Width, Me.HeaderHeight))
        Dim stringFormat As New StringFormat()
        stringFormat.Alignment = StringAlignment.Near
        args.Graphics.DrawString(Me.LeftUpperText, Me.LeftUpperFont, Brushes.Black, New Rectangle(headerRect.X, headerRect.Y, headerRect.Width / 3, headerRect.Height / 3), stringFormat)
        args.Graphics.DrawString(Me.LeftMiddleText, Me.LeftMiddleFont, Brushes.Black, New Rectangle(headerRect.X, headerRect.Y + headerRect.Height / 3, headerRect.Width / 3, headerRect.Height / 3), stringFormat)
        args.Graphics.DrawString(Me.LeftLowerText, Me.LeftLowerFont, Brushes.Black, New Rectangle(headerRect.X, headerRect.Y + (headerRect.Height) * 2 / 3, headerRect.Width / 3, headerRect.Height / 3), stringFormat)
        args.Graphics.DrawLine(New Pen(Brushes.Black), headerRect.Location, New Point(headerRect.Location.X + headerRect.Width, headerRect.Location.Y))
    End Sub

    Public Function ContentContainer() As Object
        Throw New NotImplementedException()
    End Function
End Class


Public Class CustomPrintDocument3HeaderWithLogo
    Inherits RadPrintDocument

    Protected Overrides Sub PrintHeader(args As System.Drawing.Printing.PrintPageEventArgs)
        Dim headerRect As Rectangle = New Rectangle(args.MarginBounds.Location, New Size(args.MarginBounds.Width, HeaderHeight))
        Dim stringFormat As StringFormat = New StringFormat()
        stringFormat.LineAlignment = StringAlignment.Center
        Dim leftString As String = If((Me.ReverseHeaderOnEvenPages AndAlso Me.PrintedPage Mod 2 = 0), Me.RightHeader, Me.LeftHeader)
        Dim rightString As String = If((Me.ReverseHeaderOnEvenPages AndAlso Me.PrintedPage Mod 2 = 0), Me.LeftHeader, Me.RightHeader)

        If Me.HasLogoInHeaderFooterString(leftString) AndAlso Me.Logo IsNot Nothing Then
            Me.PrintLogo(args.Graphics, New Rectangle(headerRect.X, headerRect.Y, headerRect.Width / 3, headerRect.Height))
        End If

        stringFormat.Alignment = StringAlignment.Near
        args.Graphics.DrawString(ParseHeaderFooterString(leftString), Me.HeaderFont, Brushes.Black, headerRect, stringFormat)

        If Me.HasLogoInHeaderFooterString(Me.MiddleHeader) AndAlso Me.Logo IsNot Nothing Then
            Me.PrintLogo(args.Graphics, New Rectangle(headerRect.X + (headerRect.Width / 3), headerRect.Y, headerRect.Width / 3, headerRect.Height))
        End If

        stringFormat.Alignment = StringAlignment.Center
        args.Graphics.DrawString(ParseHeaderFooterString(Me.MiddleHeader), Me.HeaderFont, Brushes.Black, headerRect, stringFormat)

        If Me.HasLogoInHeaderFooterString(rightString) AndAlso Me.Logo IsNot Nothing Then
            Me.PrintLogo(args.Graphics, New Rectangle(headerRect.Right - (headerRect.Width / 3), headerRect.Y, headerRect.Width / 3, headerRect.Height))
        End If

        stringFormat.Alignment = StringAlignment.Far
        args.Graphics.DrawString(ParseHeaderFooterString(rightString), Me.HeaderFont, Brushes.Black, headerRect, stringFormat)

    End Sub
    Protected Overrides Sub PrintLogo(g As Graphics, rect As Rectangle)
        rect.Width = 10
        rect.Height = 10
        MyBase.PrintLogo(g, rect)
    End Sub
End Class

Public Class CustomPrintDocumentHeaderWithLogo
    Inherits RadPrintDocument

    'Private m_LeftUpperText As String
    'Private m_LeftUpperFont As Font
    'Private m_LeftMiddleText As String
    'Private m_LeftMiddleFont As Font
    Private m_LeftLowerText As String
    Private m_LeftLowerFont As Font

    Private m_MiddleUpperText As String
    Private m_MiddleUpperFont As Font
    Private m_MiddleMiddleText As String
    Private m_MiddleMiddleFont As Font
    Private m_MiddleLowerText As String
    Private m_MiddleLowerFont As Font

    'Private m_RightUpperText As String
    'Private m_RightUpperFont As Font
    'Private m_RightMiddleText As String
    'Private m_RightMiddleFont As Font
    Private m_RightLowerText As String
    Private m_RightLowerFont As Font
    'Public Property LeftUpperText() As String
    '    Get
    '        Return m_LeftUpperText
    '    End Get
    '    Set(value As String)
    '        m_LeftUpperText = value
    '    End Set
    'End Property

    'Public Property LeftUpperFont() As Font
    '    Get
    '        Return m_LeftUpperFont
    '    End Get
    '    Set(value As Font)
    '        m_LeftUpperFont = value
    '    End Set
    'End Property

    'Public Property LeftMiddleText() As String
    '    Get
    '        Return m_LeftMiddleText
    '    End Get
    '    Set(value As String)
    '        m_LeftMiddleText = value
    '    End Set
    'End Property

    'Public Property LeftMiddleFont() As Font
    '    Get
    '        Return m_LeftMiddleFont
    '    End Get
    '    Set(value As Font)
    '        m_LeftMiddleFont = value
    '    End Set
    'End Property

    Public Property LeftLowerText() As String
        Get
            Return m_LeftLowerText
        End Get
        Set(value As String)
            m_LeftLowerText = value
        End Set
    End Property

    Public Property LeftLowerFont() As Font
        Get
            Return m_LeftLowerFont
        End Get
        Set(value As Font)
            m_LeftLowerFont = value
        End Set
    End Property


    ''Middle
    Public Property MiddleUpperText() As String
        Get
            Return m_MiddleUpperText
        End Get
        Set(value As String)
            m_MiddleUpperText = value
        End Set
    End Property

    Public Property MiddleUpperFont() As Font
        Get
            Return m_MiddleUpperFont
        End Get
        Set(value As Font)
            m_MiddleUpperFont = value
        End Set
    End Property

    Public Property MiddleMiddleText() As String
        Get
            Return m_MiddleMiddleText
        End Get
        Set(value As String)
            m_MiddleMiddleText = value
        End Set
    End Property

    Public Property MiddleMiddleFont() As Font
        Get
            Return m_MiddleMiddleFont
        End Get
        Set(value As Font)
            m_MiddleMiddleFont = value
        End Set
    End Property

    Public Property MiddleLowerText() As String
        Get
            Return m_MiddleLowerText
        End Get
        Set(value As String)
            m_MiddleLowerText = value
        End Set
    End Property

    Public Property MiddleLowerFont() As Font
        Get
            Return m_MiddleLowerFont
        End Get
        Set(value As Font)
            m_MiddleLowerFont = value
        End Set
    End Property

    ''Middle

    'Public Property RightUpperText() As String
    '    Get
    '        Return m_RightUpperText
    '    End Get
    '    Set(value As String)
    '        m_RightUpperText = value
    '    End Set
    'End Property

    'Public Property RightUpperFont() As Font
    '    Get
    '        Return m_RightUpperFont
    '    End Get
    '    Set(value As Font)
    '        m_RightUpperFont = value
    '    End Set
    'End Property

    'Public Property RightMiddleText() As String
    '    Get
    '        Return m_RightMiddleText
    '    End Get
    '    Set(value As String)
    '        m_RightMiddleText = value
    '    End Set
    'End Property

    'Public Property RightMiddleFont() As Font
    '    Get
    '        Return m_RightMiddleFont
    '    End Get
    '    Set(value As Font)
    '        m_RightMiddleFont = value
    '    End Set
    'End Property

    Public Property RightLowerText() As String
        Get
            Return m_RightLowerText
        End Get
        Set(value As String)
            m_RightLowerText = value
        End Set
    End Property

    Public Property RightLowerFont() As Font
        Get
            Return m_RightLowerFont
        End Get
        Set(value As Font)
            m_RightLowerFont = value
        End Set
    End Property


    Protected Overrides Sub PrintHeader(args As System.Drawing.Printing.PrintPageEventArgs)
        Dim headerRect As Rectangle = New Rectangle(args.MarginBounds.Location, New Size(args.MarginBounds.Width, HeaderHeight))
        Dim stringFormat As StringFormat = New StringFormat()


        If Me.HasLogoInHeaderFooterString(Me.MiddleUpperText) AndAlso Me.Logo IsNot Nothing Then
            Me.PrintLogo(args.Graphics, New Rectangle(headerRect.X + (headerRect.Width / 3), headerRect.Y, headerRect.Width / 3, headerRect.Height * 3 / 5))
        End If

        stringFormat.Alignment = StringAlignment.Center
        stringFormat.LineAlignment = StringAlignment.Far
        args.Graphics.DrawString(Me.MiddleMiddleText, Me.MiddleMiddleFont, Brushes.Black, New Rectangle(headerRect.X, headerRect.Y + (headerRect.Height) * 3 / 5, headerRect.Width, headerRect.Height * 1 / 5), stringFormat)

        stringFormat.Alignment = StringAlignment.Near
        stringFormat.LineAlignment = StringAlignment.Far
        args.Graphics.DrawString(Me.LeftLowerText, Me.LeftLowerFont, Brushes.Black, New Rectangle(headerRect.X, headerRect.Y + (headerRect.Height) * 4 / 5, headerRect.Width / 3, headerRect.Height * 1 / 5), stringFormat)

        stringFormat.Alignment = StringAlignment.Center
        stringFormat.LineAlignment = StringAlignment.Far
        args.Graphics.DrawString(Me.MiddleLowerText, Me.MiddleLowerFont, Brushes.Black, New Rectangle(headerRect.X + (headerRect.Width) * 1 / 3, headerRect.Y + (headerRect.Height) * 4 / 5, headerRect.Width / 3, headerRect.Height * 1 / 5), stringFormat)

        stringFormat.Alignment = StringAlignment.Far
        stringFormat.LineAlignment = StringAlignment.Far
        args.Graphics.DrawString(Me.RightLowerText, Me.RightLowerFont, Brushes.Black, New Rectangle(headerRect.X + (headerRect.Width) * 2 / 3, headerRect.Y + (headerRect.Height) * 4 / 5, headerRect.Width / 3, headerRect.Height * 1 / 5), stringFormat)
    End Sub
    Protected Overrides Sub PrintLogo(g As Graphics, rect As Rectangle)
        MyBase.PrintLogo(g, rect)
        rect.Height = 10
        rect.Width = 10
    End Sub
End Class



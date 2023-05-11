Imports System.Globalization
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Primitives

Module globalFunc
    'Dim i As Integer
    ' Prefix for tax code in tax master
    Public Function noofZero(ByVal zeroNO As Integer) As String
        Dim zeros As String = ""
        Dim i As Integer
        For i = 1 To zeroNO
            zeros += "0"
        Next i
        Return zeros
    End Function
    Public Function codePrefix(ByVal prefix As String) As String
        Return prefix.Trim() + "-"
    End Function

    Public Function TrapKey(ByVal KCode As String) As Boolean
        If (KCode >= 48 And KCode <= 57) Or KCode = 8 Or KCode = 46 Then
            TrapKey = False
        Else
            TrapKey = True
        End If
    End Function

    Private Sub PositionWindow(ByVal _reporterWin As RadForm)
        Dim screenHeight As Integer = Screen.PrimaryScreen.WorkingArea.Height
        Dim screenWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width
        _reporterWin.Location = New Point(screenWidth / 2, screenHeight / 2)
    End Sub
    Public Sub mandatoryText(ByVal ParamArray textbox() As RadTextBox)
        For Each txt As RadTextBox In textbox
            DirectCast(txt.TextBoxElement.Children(1), FillPrimitive).BackColor = Color.LightGoldenrodYellow
            DirectCast(txt.GetChildAt(0).GetChildAt(0), RadTextBoxItem).BackColor = Color.LightGoldenrodYellow
            txt.TextBoxElement.Border.ForeColor = Color.Goldenrod
        Next


    End Sub
    Public Sub mandatoryDropdown(ByVal ParamArray dropdown() As RadDropDownList)
        For Each ddl As RadDropDownList In dropdown
            ddl.DropDownListElement.TextBox.BackColor = Color.LightGoldenrodYellow
            ddl.DropDownListElement.EditableElement.BackColor = Color.LightGoldenrodYellow
            ddl.DropDownListElement.EditableElement.BorderColor = Color.Goldenrod
        Next
    End Sub
End Module

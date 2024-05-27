Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports common
Imports System.Runtime.InteropServices

Public Class clsLanguage
#Region "Variables"
    Public listBoxLanguages As New List(Of String)
#End Region

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Shared Function LoadKeyboardLayout(pwszKLID As String, Flags As UInteger) As IntPtr
    End Function

    Public Shared Function ActivateKeyboardLayout(hkl As IntPtr, Flags As UInteger) As IntPtr
    End Function

    Public Shared Sub SetKeyboardLayout(layout As String)
        Try
            Dim hkl As IntPtr = LoadKeyboardLayout(layout, 1)
            If hkl <> IntPtr.Zero Then
                ActivateKeyboardLayout(hkl, 0)
            Else
                MessageBox.Show("Failed to load keyboard layout " & layout, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Public Shared Function ListInstalledInputLanguages() As Boolean
        Dim checkLang As Boolean = False
        Try
            Dim obj As New clsLanguage()
            obj.listBoxLanguages.Clear()
            ' Iterate through the installed input languages and add them to the ListBox
            For Each lang As InputLanguage In InputLanguage.InstalledInputLanguages
                obj.listBoxLanguages.Add(lang.Culture.EnglishName & " - " & lang.Culture.Name)
            Next

            If obj.listBoxLanguages.Count > 0 Then
                For Each langList As String In obj.listBoxLanguages
                    If langList.Contains("Hindi") Then
                        checkLang = True
                        Exit For
                    End If
                Next
            Else
                checkLang = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return checkLang
    End Function

End Class

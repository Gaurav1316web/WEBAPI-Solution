Imports System.Windows.Forms
Imports System.Data
Imports System.Text.RegularExpressions
Imports common

Imports System.IO
Imports System.Configuration
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel

Public Class frmPicture
   
    Public Function GetImage(ByVal tb_name As String, ByVal Get_Column_Name As String, ByVal Set_Column_Name As String, ByVal Set_Column_Value As String)
        Try
            '=============Rohit Add Code to Display Image====================
            Dim Filename As Byte() = clsDBFuncationality.getSingleValue("select " & Get_Column_Name & " from " & tb_name & " where " & Set_Column_Name & "='" & Set_Column_Value & "'")
            Using ms As New IO.MemoryStream(CType(Filename, Byte()))
                Dim img As Image = Image.FromStream(ms)
                PicImage.Image = img
            End Using
            Return True
            '=============================================
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, "Image Not Found..", Me.Text)
            Return False
            Me.Close()
        End Try
    End Function

    Private Sub BtnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub
End Class
Imports common
Imports System.IO
Imports CgtFpAccessCSD200Dotnet
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices



Public Class frmFreeImage
    Inherits Telerik.WinControls.UI.RadForm

    Public strCode As String
    Private csd200Obj As CgtFpAccessCSD200Dotnet.MMMCogentCSD200APIs

    Private Sub frmFreeImage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            PictureBox1.Image = Nothing
            Dim qry As String = "select Biometric_Image from TSPL_USER_MASTER where User_Code='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows(0)("Biometric_Image") IsNot DBNull.Value Then
                    Dim data As Byte() = DirectCast(dt.Rows(0)("Biometric_Image"), Byte())
                    Dim ms As New MemoryStream(data)
                    PictureBox1.Image = Image.FromStream(ms)
                    ms.Close()
                    ms.Dispose()
                End If
            End If

            csd200Obj = New MMMCogentCSD200APIs()
            csd200Obj.initializeScanner()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        Initialize(True)
    End Sub

    Private Sub Initialize(ByVal showMsgBox As Boolean)
        Try
            'If Not csd200Obj.isDeviceConnected() Then
            '    clsCommon.MyMessageBoxShow("Please Connect Device...")
            'Else
            'If Not Me.csd200Obj.isDeviceInitialized() Then
            Dim num As Integer = Me.csd200Obj.initializeScanner()
            If num <> CSD200APICodes.SUCCESS Then
                If showMsgBox Then
                    clsCommon.MyMessageBoxShow("3M Cogent CSD200 scanner initialization failed. Error Code: " + clsCommon.myCstr(num), "3M Cogent CSD200")
                End If
                Return
            End If
            'End If
            If showMsgBox Then
                clsCommon.MyMessageBoxShow(Me, "Initialization Successful", "3M Cogent CSD200", Me.Text)
            End If
            'End If
        Catch exception As Exception
            clsCommon.MyMessageBoxShow(Me, exception.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If PictureBox1.Image IsNot Nothing Then
            Dim ms As New MemoryStream()
            PictureBox1.Image.Save(ms, ImageFormat.Bmp)
            Dim data As Byte() = ms.GetBuffer()
            clsDBFuncationality.UpdateImage("Biometric_Image", data, "TSPL_USER_MASTER", "User_Code='" + strCode + "'")
            ms.Close()
            ms.Dispose()
            FormClose()
        Else
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_USER_MASTER set Biometric_Image=null where User_Code='" + strCode + "'")
        End If
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        FormClose()
    End Sub

    Sub FormClose()
        csd200Obj.deInitializeScanner()
        Me.Close()
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            CaptureMethod()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub CaptureMethod()
        Try
            Dim num As Integer = -1
            Dim captureBytes As Byte() = Nothing
            Dim width As Integer = 0
            Dim height As Integer = 0
            Dim isoTemplateBytes As Byte() = Nothing
            Dim nfiq As Integer = 0
            PictureBox1.Image = Nothing
            PictureBox1.Refresh()
            num = Me.csd200Obj.captureFP(&H7530, captureBytes, width, height, nfiq, isoTemplateBytes)
            If (num = CSD200APICodes.SUCCESS) AndAlso (captureBytes IsNot Nothing) Then
                PictureBox1.Image = CreateGreyscaleBitmap(captureBytes, width, height)
            ElseIf num = CSD200APICodes.ERROR_TIMEOUT Then
                clsCommon.MyMessageBoxShow(Me, "Fingerprint Capture Timeout", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "Fingerprint Capture Failed. ErrorCode: " + num)
            End If
        Catch exception As Exception
            MessageBox.Show(exception.Message)
        End Try
    End Sub

    Public Shared Function CreateGreyscaleBitmap(buffer As Byte(), width As Integer, height As Integer) As Bitmap
        Try
            Dim bitmap As New Bitmap(width, height, PixelFormat.Format8bppIndexed)
            Dim bitmapdata As BitmapData = bitmap.LockBits(New Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed)
            Marshal.Copy(buffer, 0, bitmapdata.Scan0, width * height)
            bitmap.UnlockBits(bitmapdata)
            Dim palette As ColorPalette = bitmap.Palette
            For i As Integer = 0 To 255
                palette.Entries(i) = Color.FromArgb(i, i, i)
            Next
            bitmap.Palette = palette
            Return bitmap
        Catch exception As Exception
            MessageBox.Show(exception.Message + ":" + exception.StackTrace)
            Return Nothing
        End Try
    End Function
End Class
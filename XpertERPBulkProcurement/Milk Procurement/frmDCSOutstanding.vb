Public Class frmDCSOutstanding
    Inherits FrmMainTranScreen
#Region "Variables"
    Public VLC_Code_VLC_Uploader As String = Nothing
    Public VLC_Code As String = Nothing
    Public VLC_name As String = Nothing
    Public Vendor_Code As String = Nothing
    Public TransDate As String = Nothing

#End Region
    Private Sub frmDCSOutstanding_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setDCSBalance()
    End Sub
    Sub setDCSBalance()
        UcDCSBalance1.DCSCode = VLC_Code
        UcDCSBalance1.DCSName = VLC_name
        UcDCSBalance1.VendorCode = Vendor_Code
        UcDCSBalance1.DCSUploaderCode = VLC_Code_VLC_Uploader
        UcDCSBalance1.TransDate = TransDate
        UcDCSBalance1.RefreshData()
    End Sub
End Class
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports common.UserControls
Inherits FrmMainTranScreen
Public Class Bmc_Collection_Report



    Sub ControlEnableDisable(ByVal isEnable As Boolean)
        fromDate.Enabled = isEnable
        dtpToDate.Enabled = isEnable
    End Sub
    Private Sub rptMobileAppMilkCollection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = dtpToDate.Value.AddMonths(-1)
        Reset()
    End Sub
End Class
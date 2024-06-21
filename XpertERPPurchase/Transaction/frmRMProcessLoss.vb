Imports common
Public Class frmRMProcessLoss
    Inherits FrmMainTranScreen
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        AddNew()
    End Sub
    Sub AddNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtLoc.Value = ""
        lblloc.Text = ""
        gv1.DataSource = Nothing
    End Sub
    Private Sub txtLoc__MYValidating_1(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLoc._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            'Else
            '    WhrCls += "  and  Location_Code in ('RCDF')"
        End If
        txtLoc.Value = clsCommon.ShowSelectForm("ShipmentMasteidfndr", qry, "Code", WhrCls, txtLoc.Value, "Code", isButtonClicked)
        lblloc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLoc.Value + "'"))
    End Sub

    Private Sub frmRMProcessLoss_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddNew()
    End Sub
End Class
Imports common
Public Class rptUnionStock
    Inherits FrmMainTranScreen


    Private Sub txtBillToLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBillToLocation._MYValidating
        Dim qry As String = " Select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim whrcls As String = " Location_Type='Physical' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls += " and Location_Code in(" + objCommonVar.strCurrUserLocations + ") "
        End If
        txtBillToLocation.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", whrcls, txtBillToLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER Where Location_Code= '" + txtBillToLocation.Value + "'"))
    End Sub
    Private Sub TxtItem__My_Click(sender As Object, e As EventArgs) Handles TxtItem._My_Click
        Dim qry As String = " Select Item_Code as ItemCode,Item_Desc as ItemDescription from TSPL_ITEM_MASTER order by Item_Code "
        TxtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "ItemCode", "ItemDescription", TxtItem.arrValueMember, TxtItem.arrDispalyMember)
    End Sub

    Private Sub TxtItemType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtItemType._MYValidating
        Try
            Dim qry As String = " SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER "
            TxtItemType.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", "", TxtItemType.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtBillToLocation.Value = ""
        TxtItem.arrValueMember = Nothing
        lblBillToLocation.Text = ""
        TxtItemType.Value = ""
        rbtnDetail.IsChecked = True
        rbtnSummary.IsChecked = False
    End Sub
    Private Sub rptUnionStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        reset()
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim dt As DataTable = Nothing
            Dim Qry As String = ""
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        reset()
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click

    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click

    End Sub
End Class
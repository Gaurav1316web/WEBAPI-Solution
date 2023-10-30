Imports System.Data.SqlClient
Imports common
Imports System.IO
Public Class frmBulkSaleAcknowledgement
#Region "Variable"
    Private isNewEntry As Boolean = False
#End Region
    Private Sub frmBulkSaleAcknowledgement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddNew()
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Public Sub AddNew()
        fndDocNo.Value = Nothing
        txtDate.Value = clsCommon.GETSERVERDATE()
        UsLock1.Status = ERPTransactionStatus.Pending
        fndBulkSaleNo.Value = Nothing
        txtQty.Value = Nothing
        txtFAT.Value = Nothing
        txtFATKg.Text = Nothing
        txtSNF.Value = Nothing
        txtSNFKg.Text = Nothing
        txtFATRate.Value = Nothing
        txtSNFRate.Value = Nothing
        txtQtySale.Text = Nothing
        txtSaleFAT.Text = Nothing
        txtSaleFATKg.Text = Nothing
        txtSaleSNF.Text = Nothing
        txtSaleSNFKg.Text = Nothing
        txtRemarks.Text = Nothing
        txtSaleFATRate.Text = Nothing
        txtSaleSNFRate.Text = Nothing
        txtSaleAmount.Text = Nothing
        txtAmount.Value = Nothing
        txtDiffAmount.Value = Nothing
        btnPost.Enabled = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        fndBulkSaleNo.Enabled = True
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub
    Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub fndBulkSaleNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBulkSaleNo._MYValidating
        Try
            Dim Code As String = Nothing
            Dim chkQry As String = "select Bulk_Dispatch_Document from TSPL_BULK_SALE_ACKNOWLEDGEMENT Group By Bulk_Dispatch_Document"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(chkQry)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If clsCommon.myLen(Code) > 0 Then
                        Code += ",'" + dt.Rows(i)("Bulk_Dispatch_Document") + "'"
                    Else
                        Code = "'" + dt.Rows(i)("Bulk_Dispatch_Document") + "'"
                    End If
                Next
            End If
            Dim whrCls As String = " TSPL_Dispatch_BulkSale.Document_No Not In (" + Code + ")"
            Dim qry As String = "Select TSPL_Dispatch_BulkSale.Document_No as Code,Convert(varchar,TSPL_Dispatch_BulkSale.Document_Date,103) as [Dispatch Date],TSPL_Dispatch_BulkSale.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_Dispatch_BulkSale.Tanker_Code as [Tanker Code],TSPL_Dispatch_BulkSale.QC_Code as [QC Code],TSPL_Dispatch_BulkSale.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_Dispatch_BulkSale.Price_Code as [Price Code],TSPL_Dispatch_BulkSale.Dip_marking as [Dip Marking],TSPL_Dispatch_BulkSale.Challan_No as [Challan No],case when TSPL_Dispatch_BulkSale.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_Dispatch_BulkSale left outer Join TSPL_CUSTOMER_MASTER on TSPL_Dispatch_BulkSale.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_Dispatch_BulkSale.Location_Code =TSPL_LOCATION_MASTER.Location_Code"
            fndBulkSaleNo.Value = clsCommon.ShowSelectForm("DispatchBulkSale", qry, "Code", whrCls, fndBulkSaleNo.Value, "", isButtonClicked)
            LoadDataBulkSale(fndBulkSaleNo.Value, NavigatorType.Current)
            qry = Nothing
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub fndBulkSaleNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndBulkSaleNo._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_Dispatch_BulkSale where Document_No='" + fndBulkSaleNo.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                fndBulkSaleNo.MyReadOnly = True
            ElseIf check <= 0 Then
                fndBulkSaleNo.MyReadOnly = False
            End If
            LoadDataBulkSale(fndBulkSaleNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            qry = Nothing
        End Try
    End Sub
    Sub LoadDataBulkSale(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As clsBulkSaleAcknowledgement = Nothing
            obj = clsBulkSaleAcknowledgement.DispatchGetData(fndBulkSaleNo.Value, NavTyep)
            If obj IsNot Nothing Then
                fndBulkSaleNo.Value = obj.Bulk_Dispatch_Document
                txtQtySale.Text = obj.Dispatch_QTY
                txtSaleFAT.Text = obj.Dispatch_FAT
                txtSaleFATKg.Text = obj.Dispatch_FATKG
                txtSaleSNF.Text = obj.Dispatch_SNF
                txtSaleSNFKg.Text = obj.Dispatch_SNFKG
                txtSaleFATRate.Text = obj.Dispatch_FATRate
                txtFATRate.Value = obj.Dispatch_FATRate
                txtSaleSNFRate.Text = obj.Dispatch_SNFRate
                txtSNFRate.Value = obj.Dispatch_SNFRate
                txtSaleAmount.Text = obj.Dispatch_Amount
                CalculateAmount()
                CalculateDiffAmount()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            Dim obj As New clsBulkSaleAcknowledgement()
            obj.Document_No = fndDocNo.Value
            obj.Document_Date = txtDate.Value
            obj.Bulk_Dispatch_Document = fndBulkSaleNo.Value
            obj.Qty = txtQty.Value
            obj.FAT = txtFAT.Value
            obj.FAT_KG = txtFATKg.Text
            obj.SNF = txtSNF.Value
            obj.SNF_KG = txtSNFKg.Text
            obj.FAT_Rate = txtFATRate.Value
            obj.SNF_Rate = txtSNFRate.Value
            obj.Amount = txtAmount.Value
            obj.Remarks = txtRemarks.Text
            If clsCommon.myLen(fndDocNo.Value) <= 0 Then
                isNewEntry = True
            End If
            If clsBulkSaleAcknowledgement.SaveData(obj, isNewEntry) Then
                clsCommon.MyMessageBoxShow("Data Saved Successfully.", Me.Text)
                LoadDataBulkSaleAck(obj.Document_No, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            If (deleteConfirm()) Then
                If (clsBulkSaleAcknowledgement.DeleteData(fndDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully.", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDocNo._MYValidating
        Try
            Dim qry As String = "Select Document_No As [Document Code],Document_Date As [Document Date],Bulk_Dispatch_Document,Qty,FAT,FAT_KG,SNF,SNF_KG,FAT_Rate,Status From TSPL_BULK_SALE_ACKNOWLEDGEMENT "
            fndDocNo.Value = clsCommon.ShowSelectForm("BulkSaleAck", qry, "Document Code", "", fndDocNo.Value, "", isButtonClicked)
            LoadDataBulkSaleAck(fndDocNo.Value, NavigatorType.Current)
            qry = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_BULK_SALE_ACKNOWLEDGEMENT where Document_No='" + fndDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                fndDocNo.MyReadOnly = False
            Else
                fndDocNo.MyReadOnly = True
            End If
            LoadDataBulkSaleAck(fndDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub LoadDataBulkSaleAck(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As clsBulkSaleAcknowledgement = Nothing
            obj = clsBulkSaleAcknowledgement.GetData(strCode, NavTyep)
            If obj IsNot Nothing Then
                fndDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                If obj.Status = 1 Then
                    btnPost.Enabled = False
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                    fndBulkSaleNo.Enabled = False
                Else
                    btnPost.Enabled = True
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                    fndBulkSaleNo.Enabled = True
                End If
                fndBulkSaleNo.Value = obj.Bulk_Dispatch_Document
                If clsCommon.myLen(fndBulkSaleNo.Value) > 0 Then
                    LoadDataBulkSale(fndBulkSaleNo.Value, NavigatorType.Current)
                End If
                txtQty.Value = obj.Qty
                txtFAT.Value = obj.FAT
                txtFATKg.Text = obj.FAT_KG
                txtSNF.Value = obj.SNF
                txtSNFKg.Text = obj.SNF_KG
                txtFATRate.Value = obj.FAT_Rate
                txtSNFRate.Value = obj.SNF_Rate
                txtAmount.Value = obj.Amount
                txtRemarks.Text = obj.Remarks
            Else
                AddNew()
            End If
            CalculateDiffAmount()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If (myMessages.postConfirm()) Then
                If (clsBulkSaleAcknowledgement.PostData(fndDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully posted")
                    LoadDataBulkSaleAck(fndDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtFAT_Validated(sender As Object, e As EventArgs) Handles txtFAT.Validated
        Try
            If txtQty.Value > 0 AndAlso txtFAT.Value > 0 Then
                txtFATKg.Text = Math.Round((txtQty.Value / 100) * txtFAT.Value, 3)
            Else
                txtFATKg.Text = 0
            End If
            CalculateAmount()
            CalculateDiffAmount()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtSNF_Validated(sender As Object, e As EventArgs) Handles txtSNF.Validated
        Try
            If txtQty.Value > 0 AndAlso txtSNF.Value > 0 Then
                txtSNFKg.Text = Math.Round((txtQty.Value / 100) * txtSNF.Value, 3)
            Else
                txtSNFKg.Text = 0
            End If
            CalculateAmount()
            CalculateDiffAmount()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtQty_Validated(sender As Object, e As EventArgs) Handles txtQty.Validated
        Try
            If txtQty.Value > 0 AndAlso txtFAT.Value > 0 Then
                txtFATKg.Text = Math.Round((txtQty.Value / 100) * txtFAT.Value, 3)
            Else
                txtFATKg.Text = 0
            End If

            If txtQty.Value > 0 AndAlso txtSNF.Value > 0 Then
                txtSNFKg.Text = Math.Round((txtQty.Value / 100) * txtSNF.Value, 3)
            Else
                txtSNFKg.Text = 0
            End If
            CalculateAmount()
            CalculateDiffAmount()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub CalculateAmount()
        Try
            If clsCommon.myCDecimal(txtFATKg.Text) > 0 AndAlso clsCommon.myCDecimal(txtSNFKg.Text) > 0 Then
                txtAmount.Value = Math.Round((clsCommon.myCDecimal(txtFATKg.Text) * clsCommon.myCDecimal(txtFATRate.Value)) + (clsCommon.myCDecimal(txtSNFKg.Text) * clsCommon.myCDecimal(txtSNFRate.Value)), 2)
            Else
                txtAmount.Value = 0
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub CalculateDiffAmount()
        Try
            If clsCommon.myCDecimal(txtSaleAmount.Text) > 0 AndAlso clsCommon.myCDecimal(txtAmount.Value) > 0 Then
                txtDiffAmount.Value = Math.Round(clsCommon.myCDecimal(txtSaleAmount.Text) - clsCommon.myCDecimal(txtAmount.Value), 2)
            Else
                txtDiffAmount.Value = 0
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class
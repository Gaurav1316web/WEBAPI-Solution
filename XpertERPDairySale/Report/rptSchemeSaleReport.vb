Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports Telerik.WinControls.UI.Export

Public Class rptSchemeSaleReport
    Inherits FrmMainTranScreen

#Region "Variable"
    Dim AreaWiseBilling As Boolean = False
    Dim StrPermission As String
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            Dim qry As String = ""
            Dim qry1 As String = ""
            Dim whrcls As String = ""
            Dim strShift As String = ""
            Dim whrclsShift As String = ""
            Dim itemNames1 As String = Nothing
            Dim SchitemNames1 As String = Nothing
            Dim ItemName7 As String = Nothing
            Dim SchItemName7 As String = Nothing

            If txtDistributor.arrValueMember IsNot Nothing Then
                whrcls += "  TSPL_SD_SALE_INVOICE_HEAD.Customer_Code In (" + clsCommon.GetMulcallString(txtDistributor.arrValueMember) + ")"
            End If

            qry = "SELECT max(TSPL_ITEM_MASTER.Item_Code)Item_Code,
                max(TSPL_ITEM_MASTER.Short_Description)Short_Description,max(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq
            FROM TSPL_SD_SALE_INVOICE_DETAIL 
			left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE             left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
            where   TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' "
            If txtDistributor.arrValueMember IsNot Nothing Then
                qry += "  TSPL_SD_SALE_INVOICE_HEAD.Customer_Code In (" + clsCommon.GetMulcallString(txtDistributor.arrValueMember) + ")"
            End If

            qry += " and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >=Convert(date,'" & txtToDate.Value & "',103) 
            and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= Convert(date,'" & txtToDate.Value & "',103) 
			group by TSPL_ITEM_MASTER.Item_Code"

            qry1 = "SELECT max(TSPL_ITEM_MASTER.Item_Code)Item_Code,
                max(TSPL_ITEM_MASTER.Short_Description)Short_Description,max(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq
            FROM TSPL_SD_SALE_INVOICE_DETAIL 
			left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE             left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
            where   TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' "
            If txtDistributor.arrValueMember IsNot Nothing Then
                qry1 += "  TSPL_SD_SALE_INVOICE_HEAD.Customer_Code In (" + clsCommon.GetMulcallString(txtDistributor.arrValueMember) + ")"
            End If
            qry1 += " and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >=Convert(date,'" & txtToDate.Value & "',103) 
            and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= Convert(date,'" & txtToDate.Value & "',103) 
			group by TSPL_ITEM_MASTER.Item_Code"

            Dim dtitemName As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim dtSchemeItem As DataTable = clsDBFuncationality.GetDataTable(qry1)
            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    If i = 0 Then
                        itemNames1 += "[" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "] "
                        ItemName7 += "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]"
                    End If
                Next
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Telerik.Collections.Generic
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports common
Imports XpertERPEngine

Public Class FrmSettlementReport
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode As String
    Dim sql As String
    Dim dr As SqlDataReader
    Private preInvQty As Decimal = 0
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim StrQuery, StrQuery1, StrQuery2, StrQuery3, StrQuery4, StrQuery5, StrQuery6, StrQuery7, StrQuery8, StrQuery9, StrQuery10, StrQuery11 As String

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.Settlement)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                l1User = dr(0).ToString()
                l2User = dr(1).ToString()
                l3User = dr(2).ToString()
                l4User = dr(3).ToString()
                l5User = dr(4).ToString()

            Next
        End If
    End Sub
    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click

        print()
        
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
   
        reset()
    End Sub
    Sub reset()
        LoadLocation()
        LoadLoadOut()
        LoadTransfer()
        dtpFdate.Value = clsCommon.GETSERVERDATE
        DtpTodate.Value = clsCommon.GETSERVERDATE
        chkLocationAll.IsChecked = True
        chktransferAll.IsChecked = True
        chkLoadoutAll.IsChecked = True
        rdbTransferwise.IsChecked = True
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged, chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Sub LoadLocation()
       
        Dim qry As String = "select Location_Code as [Location],Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type='Physical'"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location"
        cbgLocation.DisplayMember = "Location Description"
    End Sub

    Sub LoadTransfer()
       
        Dim qry As String = "select Transfer_No as 'Transfer No',convert(Varchar(10),transfer_date,103) as [Transfer Date] from TSPL_TRANSFER_HEAD inner join TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code  where Transfer_Type='LO' and location_Type='logical'  ORDER BY 'Transfer No' DESC"
        cbgTransfer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgTransfer.ValueMember = "Transfer No"
        cbgTransfer.DisplayMember = "transfer_date"
    End Sub

    Sub LoadLoadOut()
       
        Dim qry As String = "Select Shipment_No as 'Shipment No',CONVERT(varchar(15), Shipment_Date , 103) as [Shipment Date],(select top 1 isnull(Sale_Invoice_No, 'No Invoice')  from TSPL_SALE_INVOICE_HEAD where TSPL_SALE_INVOICE_HEAD.Shipment_No = TSPL_SHIPMENT_MASTER.Shipment_No   ) as [Sale Invoice No] FROM TSPL_SHIPMENT_MASTER where TSPL_SHIPMENT_MASTER.Shipment_Type='sale' ORDER BY Shipment_No Desc"
        cbgLoadOut.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLoadOut.ValueMember = "Shipment No"
        cbgLoadOut.DisplayMember = "Sale Invoice No"
    End Sub

    Private Sub FrmSettlementReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            'resetForm()
            reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'deletedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnprint.Enabled Then
            print()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()


        End If
    End Sub
    Sub print()
        If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
            Return
        ElseIf ChkTransferSelect.IsChecked = True AndAlso cbgTransfer.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Transfer or select ALL")
            Return
        ElseIf chkLoadoutSelect.IsChecked = True AndAlso cbgLoadOut.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one LoadOut or select ALL")
            Return

        End If

        Dim strLocAll, strTransferAll, strLoadout As String
        If chkLocationAll.IsChecked = True Then
            strLocAll = "Y"
        Else
            strLocAll = "N"
        End If
        If chktransferAll.IsChecked = True Then
            strTransferAll = "Y"
        Else
            strTransferAll = "N"
        End If
        If chkLoadoutAll.IsChecked = True Then
            strLoadout = "Y"
        Else
            strLoadout = "N"
        End If

        If rdbTransferwise.IsChecked = True Then
            Dim un1 As String
            If strTransferAll = "Y" Then
                StrQuery = "SELECT TSPL_SALE_INVOICE_DETAIL.Location, TSPL_TRANSFER_HEAD.Transfer_No, TSPL_TRANSFER_HEAD.Transfer_Date, " & _
"TSPL_TRANSFER_HEAD.Vehicle_Code, TSPL_TRANSFER_HEAD.Salesmancode, c.Emp_Name AS salesmanName, " & _
"TSPL_TRANSFER_DETAIL.Item_Code, TSPL_TRANSFER_DETAIL.Item_Desc,case when TSPL_TRANSFER_DETAIL.uom <> 'SH' then TSPL_TRANSFER_DETAIL.Item_Qty else 0 end as Item_Qty,(TSPL_TRANSFER_DETAIL.Item_Qty * (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.TPT_Value + TSPL_TRANSFER_DETAIL.Empty_Value ) ) as LoadOutamt," & _
" 0 as Shipped_Qty,CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y' ) THEN 0 ELSE isnull(Invoice_Qty,0) END AS InvQty, " & _
" CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y'  or " & _
" TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y') THEN TSPL_SALE_INVOICE_DETAIL.Invoice_Qty ELSE 0 END AS FOCqty, " & _
"CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y'  or " & _
" TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y' ) THEN 0 ELSE isnull((TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value),0) END AS InvAMt, " & _
"CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y'  or " & _
"TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y') THEN case " & _
"when ((select excisable from tspl_location_master where TSPL_LOCATION_MASTER.Location_Code=TSPL_SALE_INVOICE_DETAIL.Location)='T' ) " & _
" then 0 else TSPL_SALE_INVOICE_DETAIL.Empty_Value end  ELSE 0 END AS FOCamt, 0 AS LoadInQty, " & _
"0 AS LoadInAMt, " & _
"(TSPL_TRANSFER_DETAIL.MRP * TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor) as mrp, case when TSPL_SALE_INVOICE_DETAIL.Unit_code IS null then TSPL_TRANSFER_DETAIL.Uom else " & _
"TSPL_SALE_INVOICE_DETAIL.Unit_code end as Unit_code,TSPL_TRANSFER_DETAIL.Basic_Price, " & _
" case when TSPL_SALE_INVOICE_DETAIL.Unit_code IS null then " & _
"(select top 1 Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and " & _
" TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_TRANSFER_DETAIL.Uom) else (select top 1  Conversion_Factor from TSPL_ITEM_UOM_DETAIL where " & _
"TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and " & _
" TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code) end as Conversion_Factor," & _
" TSPL_TRANSFER_DETAIL.Empty_Value,TSPL_TRANSFER_DETAIL.BasicPrice_WithTax,TSPL_TRANSFER_DETAIL.TPT_Value,(SELECT top 1  Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL " & _
"WHERE TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_TRANSFER_DETAIL.Item_Code AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_TRANSFER_DETAIL.Uom) as Transfer_Convert_F " & _
",(isnull(TSPL_SALE_INVOICE_HEAD.Shell_Qty,0) * 100) as ShellQty,'T' as Type " & _
" FROM  TSPL_TRANSFER_HEAD INNER JOIN " & _
                      " TSPL_EMPLOYEE_MASTER AS c ON TSPL_TRANSFER_HEAD.Salesmancode = c.EMP_CODE LEFT OUTER JOIN " & _
                      " TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.To_Location = TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
                      " TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No LEFT OUTER JOIN " & _
                      " TSPL_ITEM_UOM_DETAIL AS TSPL_ITEM_UOM_DETAIL_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL_1.Item_Code AND " & _
                      " TSPL_TRANSFER_DETAIL.Uom = TSPL_ITEM_UOM_DETAIL_1.UOM_Code LEFT OUTER JOIN " & _
                      " TSPL_SHIPMENT_MASTER AS d LEFT OUTER JOIN " & _
                      " TSPL_SALE_INVOICE_HEAD ON d.Shipment_No = TSPL_SALE_INVOICE_HEAD.Shipment_No LEFT OUTER JOIN " & _
                      " TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No ON " & _
                      " TSPL_TRANSFER_HEAD.Transfer_No = d.Transfer_No AND TSPL_TRANSFER_DETAIL.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code  " & _
"WHERE (TSPL_TRANSFER_HEAD.Transfer_Type = 'LO') AND (TSPL_LOCATION_MASTER.Location_Type = 'logical')  and convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) >= convert(date, '" & dtpFdate.Value & "',103) AND " & _
 "convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) <= convert(date, '" & DtpTodate.Value & "',103) "
                un1 = "union all "
                StrQuery5 = "SELECT '' AS Location,a.Load_Out_No as Transfer_No, a.Transfer_Date, a.Vehicle_Code, a.Salesmancode," & _
    " '' AS salesmanName, b.Item_Code, b.Item_Desc, 0 as Item_Qty, " & _
    " 0 AS LoadOutamt, 0 AS Shipped_Qty, 0 AS InvQty, 0 AS FOCqty, 0 AS InvAMt, 0 AS FOCamt,case when b.UOM <> 'SH' then  (b.LoadIn_Qty/Conversion_Factor) + b.Burst/Conversion_Factor + b.Leak/Conversion_Factor  +  b.shortage/Conversion_Factor else 0 end  AS LoadInQty, " & _
    "((b.LoadIn_Qty ) * (b.BasicPrice_WithTax + b.TPT_Value + b.Empty_Value)) AS LoadInAMt, (b.MRP*TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor) AS mrp, " & _
    " b.Uom AS Unit_code, b.Basic_Price,TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor AS Conversion_Factor, b.Empty_Value, b.BasicPrice_WithTax, b.TPT_Value," & _
    "(SELECT top 1  Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL WHERE (Item_Code = b.Item_Code) AND (UOM_Code = b.Uom)) AS Transfer_Convert_F " & _
    ",0 as ShellQty,'T' as Type FROM TSPL_ITEM_UOM_DETAIL AS TSPL_ITEM_UOM_DETAIL_1 INNER JOIN " & _
    "TSPL_TRANSFER_HEAD AS a INNER JOIN " & _
    "TSPL_TRANSFER_DETAIL AS b ON a.Transfer_No = b.Transfer_No ON TSPL_ITEM_UOM_DETAIL_1.Item_Code = b.Item_Code AND " & _
    "TSPL_ITEM_UOM_DETAIL_1.UOM_Code = b.Uom WHERE a.Transfer_Type = 'LI' and convert(date,a.Transfer_Date,103) >= convert(date, '" & dtpFdate.Value & "',103) AND " & _
                                "convert(date,a.Transfer_Date,103) <= convert(date, '" & DtpTodate.Value & "',103) "
                StrQuery1 = "SELECT  c.Route_No,c.Sale_Invoice_No, c.Sale_Invoice_Date," & _
                           "(SELECT isnull(SUM(Applied_Amount),0) FROM TSPL_RECEIPT_DETAIL WHERE (Document_No = c.Sale_Invoice_No)) as Receip_amt, " & _
                           "ISNULL((select sum(isnull(Adjustment_Amount,0)) from TSPL_Receipt_Adjustment_Header where Doc_No=c.Sale_Invoice_No),0)  AS Receip_adjustment_amt, " & _
                           "(select ISNULL(SUM(isnull(Item_Cost,0) + isnull(Breakage_Cost,0)),0) from TSPL_ADJUSTMENT_HEADER, " & _
                           "TSPL_ADJUSTMENT_DETAIL  WHERE TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No AND Document_No=C.Sale_Invoice_No) as Receipt_Empty, " & _
                           "TSPL_SHIPMENT_MASTER.Transfer_No ,c.Inv_Tax_Amt, c.Inv_Detail_Total_Amt, " & _
                           "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                           "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN 0 ELSE (Invoice_Qty * TSPL_SALE_INVOICE_DETAIL.Item_Net_Amt) END as InvAmt,CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                           "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN 0 ELSE (Invoice_Qty/Conversion_Factor) END AS InvQty, " & _
                           " c.Empty_Value  as Empty_Value,c.TPT, " & _
                           "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN TSPL_SALE_INVOICE_DETAIL.Empty_Value ELSE 0 END as FOCValue, " & _
                           "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN  (Invoice_Qty/Conversion_Factor) else 0 END AS FOCQty,c.Cust_Name FROM TSPL_SALE_INVOICE_HEAD AS c INNER JOIN " & _
                           "TSPL_SHIPMENT_MASTER ON c.Shipment_No = TSPL_SHIPMENT_MASTER.Shipment_No INNER JOIN " & _
                           "TSPL_SALE_INVOICE_DETAIL ON c.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " & _
                           "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                           "TSPL_SALE_INVOICE_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code and convert(date,c.Sale_Invoice_Date,103) >= convert(date, '" & dtpFdate.Value & "',103) AND " & _
 "convert(date,c.Sale_Invoice_Date,103) <= convert(date, '" & DtpTodate.Value & "',103) "
                StrQuery2 = "select TSPL_TRANSFER_HEAD.Load_Out_No as Load_Out_No,TSPL_TRANSFER_HEAD.Transfer_Date,TSPL_TRANSFER_HEAD.Load_Out_No as Transfer_No, " & _
                            "TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc,(TSPL_TRANSFER_DETAIL.LoadIn_Qty/Conversion_Factor) + TSPL_TRANSFER_DETAIL.Burst + TSPL_TRANSFER_DETAIL.Leak  +  TSPL_TRANSFER_DETAIL.shortage as LoadIn_Qty,TSPL_TRANSFER_DETAIL.Uom,TSPL_TRANSFER_DETAIL.MRP, " & _
                            "case when TSPL_TRANSFER_DETAIL.LoadIn_Qty > 0 then ((TSPL_TRANSFER_DETAIL.LoadIn_Qty ) * (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.TPT_Value + TSPL_TRANSFER_DETAIL.Empty_Value)) else 0 end as Loadinamt " & _
                            "from TSPL_TRANSFER_HEAD INNER JOIN " & _
                            "TSPL_TRANSFER_DETAIL  ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
                            "TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND TSPL_TRANSFER_DETAIL.Uom = TSPL_ITEM_UOM_DETAIL.UOM_Code where " & _
                            "Transfer_Type='LI'  and convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) >= convert(date, '" & dtpFdate.Value & "',103) AND " & _
 "convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) <= convert(date, '" & DtpTodate.Value & "',103) "
                StrQuery3 = "select a.Payment_No,convert(date,a.Payment_Date,103) as Payment_Date ,a.Payment_Amount,case when a.Apply_To='' then a.loadoutNo else a.Apply_To end as Apply_To from TSPL_PAYMENT_HEADER a where (a.Apply_By='Load Out/Transfer'  or LoadOutNo <> '')  " & _
                "and convert(date,a.Payment_Date,103) >= convert(date, '" & dtpFdate.Value & "',103) AND " & _
 "convert(date,a.Payment_Date,103) <= convert(date, '" & DtpTodate.Value & "',103) "

                StrQuery4 = "select a.Adjustment_No,convert(date,a.Adjustment_Date,103) as Adjustment_Date,b.Adjustment_Type,b.Item_Code, " & _
                "b.Item_Description,b.Item_Quantity,a.Document_No,b.Item_Cost,b.Breakage,b.Breakage_Cost,TSPL_SHIPMENT_MASTER.Transfer_No,b.Unit_Code from " & _
                            "TSPL_ADJUSTMENT_HEADER a ,TSPL_ADJUSTMENT_DETAIL b,TSPL_SALE_INVOICE_HEAD, " & _
                            "TSPL_SHIPMENT_MASTER where a.Adjustment_No=b.Adjustment_No and a.Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No and TSPL_SALE_INVOICE_HEAD.Shipment_No=TSPL_SHIPMENT_MASTER.Shipment_No " & _
                            "and convert(date,a.Adjustment_Date,103) >= convert(date, '" & dtpFdate.Value & "',103) AND " & _
 "convert(date,a.Adjustment_Date,103) <= convert(date, '" & DtpTodate.Value & "',103) "

            Else
                StrQuery = "SELECT TSPL_SALE_INVOICE_DETAIL.Location, TSPL_TRANSFER_HEAD.Transfer_No, TSPL_TRANSFER_HEAD.Transfer_Date, " & _
"TSPL_TRANSFER_HEAD.Vehicle_Code, TSPL_TRANSFER_HEAD.Salesmancode, c.Emp_Name AS salesmanName, " & _
"TSPL_TRANSFER_DETAIL.Item_Code, TSPL_TRANSFER_DETAIL.Item_Desc,case when TSPL_TRANSFER_DETAIL.uom <> 'SH' then TSPL_TRANSFER_DETAIL.Item_Qty else 0 end as Item_Qty,(TSPL_TRANSFER_DETAIL.Item_Qty * (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.TPT_Value + TSPL_TRANSFER_DETAIL.Empty_Value ) ) as LoadOutamt," & _
" 0 as Shipped_Qty,CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y' ) THEN 0 ELSE isnull(Invoice_Qty,0) END AS InvQty, " & _
" CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y'  or " & _
" TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y') THEN TSPL_SALE_INVOICE_DETAIL.Invoice_Qty ELSE 0 END AS FOCqty, " & _
"CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y'  or " & _
" TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y' ) THEN 0 ELSE isnull((TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value),0) END AS InvAMt, " & _
"CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y'  or " & _
"TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y') THEN case " & _
"when ((select excisable from tspl_location_master where TSPL_LOCATION_MASTER.Location_Code=TSPL_SALE_INVOICE_DETAIL.Location)='T' ) " & _
" then 0 else TSPL_SALE_INVOICE_DETAIL.Empty_Value end  ELSE 0 END AS FOCamt, 0 AS LoadInQty, " & _
"0 AS LoadInAMt, " & _
"(TSPL_TRANSFER_DETAIL.MRP * TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor) as mrp, case when TSPL_SALE_INVOICE_DETAIL.Unit_code IS null then TSPL_TRANSFER_DETAIL.Uom else " & _
"TSPL_SALE_INVOICE_DETAIL.Unit_code end as Unit_code,TSPL_TRANSFER_DETAIL.Basic_Price, " & _
" case when TSPL_SALE_INVOICE_DETAIL.Unit_code IS null then " & _
"(select top 1 Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and " & _
" TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_TRANSFER_DETAIL.Uom) else (select top 1  Conversion_Factor from TSPL_ITEM_UOM_DETAIL where " & _
"TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and " & _
" TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code) end as Conversion_Factor," & _
" TSPL_TRANSFER_DETAIL.Empty_Value,TSPL_TRANSFER_DETAIL.BasicPrice_WithTax,TSPL_TRANSFER_DETAIL.TPT_Value,(SELECT top 1  Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL " & _
"WHERE TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_TRANSFER_DETAIL.Item_Code AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_TRANSFER_DETAIL.Uom) as Transfer_Convert_F " & _
",(isnull(TSPL_SALE_INVOICE_HEAD.Shell_Qty,0) * 100) as ShellQty,'T' as Type " & _
" FROM  TSPL_TRANSFER_HEAD INNER JOIN " & _
                      " TSPL_EMPLOYEE_MASTER AS c ON TSPL_TRANSFER_HEAD.Salesmancode = c.EMP_CODE LEFT OUTER JOIN " & _
                      " TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.To_Location = TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
                      " TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No LEFT OUTER JOIN " & _
                      " TSPL_ITEM_UOM_DETAIL AS TSPL_ITEM_UOM_DETAIL_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL_1.Item_Code AND " & _
                      " TSPL_TRANSFER_DETAIL.Uom = TSPL_ITEM_UOM_DETAIL_1.UOM_Code LEFT OUTER JOIN " & _
                      " TSPL_SHIPMENT_MASTER AS d LEFT OUTER JOIN " & _
                      " TSPL_SALE_INVOICE_HEAD ON d.Shipment_No = TSPL_SALE_INVOICE_HEAD.Shipment_No LEFT OUTER JOIN " & _
                      " TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No ON " & _
                      " TSPL_TRANSFER_HEAD.Transfer_No = d.Transfer_No AND TSPL_TRANSFER_DETAIL.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code  " & _
"WHERE (TSPL_TRANSFER_HEAD.Transfer_Type = 'LO') AND (TSPL_LOCATION_MASTER.Location_Type = 'logical') "
                un1 = "union all "
                StrQuery5 = "SELECT '' AS Location,a.Load_Out_No as Transfer_No, a.Transfer_Date, a.Vehicle_Code, a.Salesmancode," & _
    " '' AS salesmanName, b.Item_Code, b.Item_Desc, 0 as Item_Qty, " & _
    " 0 AS LoadOutamt, 0 AS Shipped_Qty, 0 AS InvQty, 0 AS FOCqty, 0 AS InvAMt, 0 AS FOCamt,case when b.UOM <> 'SH' then (b.LoadIn_Qty/Conversion_Factor) + b.Burst/Conversion_Factor + b.Leak/Conversion_Factor  +  b.shortage/Conversion_Factor else 0 end   AS LoadInQty, " & _
    "((b.LoadIn_Qty ) * (b.BasicPrice_WithTax + b.TPT_Value + b.Empty_Value)) AS LoadInAMt, (b.MRP*TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor) AS mrp, " & _
    " b.Uom AS Unit_code, b.Basic_Price,TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor AS Conversion_Factor, b.Empty_Value, b.BasicPrice_WithTax, b.TPT_Value," & _
    "(SELECT top 1  Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL WHERE (Item_Code = b.Item_Code) AND (UOM_Code = b.Uom)) AS Transfer_Convert_F " & _
    ",0 as ShellQty,'T' as Type FROM TSPL_ITEM_UOM_DETAIL AS TSPL_ITEM_UOM_DETAIL_1 INNER JOIN " & _
    "TSPL_TRANSFER_HEAD AS a INNER JOIN " & _
    "TSPL_TRANSFER_DETAIL AS b ON a.Transfer_No = b.Transfer_No ON TSPL_ITEM_UOM_DETAIL_1.Item_Code = b.Item_Code AND " & _
    "TSPL_ITEM_UOM_DETAIL_1.UOM_Code = b.Uom WHERE a.Transfer_Type = 'LI'  "
                StrQuery1 = "SELECT c.Route_No, c.Sale_Invoice_No, c.Sale_Invoice_Date," & _
                           "(SELECT isnull(SUM(Applied_Amount),0) FROM TSPL_RECEIPT_DETAIL WHERE (Document_No = c.Sale_Invoice_No)) as Receip_amt, " & _
                           "ISNULL((select sum(isnull(Adjustment_Amount,0)) from TSPL_Receipt_Adjustment_Header where Doc_No=c.Sale_Invoice_No),0)  AS Receip_adjustment_amt, " & _
                           "(select ISNULL(SUM(isnull(Item_Cost,0) + isnull(Breakage_Cost,0)),0) from TSPL_ADJUSTMENT_HEADER, " & _
                           "TSPL_ADJUSTMENT_DETAIL  WHERE TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No AND Document_No=C.Sale_Invoice_No) as Receipt_Empty, " & _
                           "TSPL_SHIPMENT_MASTER.Transfer_No ,c.Inv_Tax_Amt, c.Inv_Detail_Total_Amt, " & _
                           "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                           "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN 0 ELSE (Invoice_Qty * TSPL_SALE_INVOICE_DETAIL.Item_Net_Amt) END as InvAmt,CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                           "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN 0 ELSE (Invoice_Qty/Conversion_Factor) END AS InvQty, " & _
                           " c.Empty_Value  as Empty_Value,c.TPT, " & _
                           "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN TSPL_SALE_INVOICE_DETAIL.Empty_Value ELSE 0 END as FOCValue, " & _
                           "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN  (Invoice_Qty/Conversion_Factor) else 0 END AS FOCQty,c.Cust_Name FROM TSPL_SALE_INVOICE_HEAD AS c INNER JOIN " & _
                           "TSPL_SHIPMENT_MASTER ON c.Shipment_No = TSPL_SHIPMENT_MASTER.Shipment_No INNER JOIN " & _
                           "TSPL_SALE_INVOICE_DETAIL ON c.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " & _
                           "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                           "TSPL_SALE_INVOICE_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                           "where   TSPL_SHIPMENT_MASTER.Shipment_Type='Transfer'"
                StrQuery2 = "select a.Transfer_No as Load_Out_No,a.Transfer_Date,a.Load_Out_No as Transfer_No, " & _
                            "b.Item_Code,b.Item_Desc,(b.LoadIn_Qty/Conversion_Factor) + b.Burst + b.Leak  +  b.shortage as LoadIn_Qty,b.Uom,b.MRP, " & _
                            "case when b.LoadIn_Qty > 0 then ((b.LoadIn_Qty ) * (b.BasicPrice_WithTax + b.TPT_Value + b.Empty_Value)) else 0 end as Loadinamt " & _
                            "from TSPL_TRANSFER_HEAD AS a INNER JOIN " & _
                            "TSPL_TRANSFER_DETAIL AS b ON a.Transfer_No = b.Transfer_No INNER JOIN " & _
                            "TSPL_ITEM_UOM_DETAIL ON b.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND b.Uom = TSPL_ITEM_UOM_DETAIL.UOM_Code where " & _
                            "Transfer_Type='LI' "
                StrQuery3 = "select a.Payment_No,convert(date,a.Payment_Date,103) as Payment_Date,a.Payment_Amount,case when a.Apply_To='' then a.loadoutNo else a.Apply_To end as Apply_To from TSPL_PAYMENT_HEADER a where a.Apply_By='Load Out/Transfer'  "

                StrQuery4 = "select a.Adjustment_No,convert(date,a.Adjustment_Date,103) as Adjustment_Date,b.Adjustment_Type,b.Item_Code, " & _
                "b.Item_Description,b.Item_Quantity,a.Document_No,b.Item_Cost,b.Breakage,b.Breakage_Cost,TSPL_SHIPMENT_MASTER.Transfer_No,b.Unit_Code from " & _
                            "TSPL_ADJUSTMENT_HEADER a ,TSPL_ADJUSTMENT_DETAIL b,TSPL_SALE_INVOICE_HEAD, " & _
                            "TSPL_SHIPMENT_MASTER where a.Adjustment_No=b.Adjustment_No and a.Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No and TSPL_SALE_INVOICE_HEAD.Shipment_No=TSPL_SHIPMENT_MASTER.Shipment_No "

                If strTransferAll = "N" Then
                    StrQuery += " and TSPL_TRANSFER_HEAD.Transfer_No in (" + clsCommon.GetMulcallString(cbgTransfer.CheckedValue) + ") "
                    StrQuery5 += " and  a.Load_Out_No in (" + clsCommon.GetMulcallString(cbgTransfer.CheckedValue) + ") "
                    StrQuery1 += " and TSPL_SHIPMENT_MASTER.Transfer_No in (" + clsCommon.GetMulcallString(cbgTransfer.CheckedValue) + ") "
                    StrQuery2 += " and a.Load_Out_No in (" + clsCommon.GetMulcallString(cbgTransfer.CheckedValue) + ") "
                    StrQuery3 += " and a.Apply_To in (" + clsCommon.GetMulcallString(cbgTransfer.CheckedValue) + ") "
                    StrQuery4 += " and TSPL_SHIPMENT_MASTER.Transfer_No in (" + clsCommon.GetMulcallString(cbgTransfer.CheckedValue) + ") "
                    StrQuery = StrQuery & un1 & StrQuery5
                End If
            End If

            If strTransferAll = "Y" And strLocAll = "N" Then
                StrQuery += " and TSPL_TRANSFER_HEAD.From_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                StrQuery5 += " and  a.To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "

                StrQuery = StrQuery & un1 & StrQuery5
            End If
            StrQuery = StrQuery & un1 & StrQuery5
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funsubreport(CrystalReportFolder.SalesReport, StrQuery, StrQuery1, StrQuery2, StrQuery3, StrQuery4, "crptSettlement", "Settlement Report", "crptSettlementReceiptDetails.rpt", "crptSettlementLoadin.rpt", "crptSettlementpayment.rpt", "crptSettlementAdjustment.rpt")
        Else
            If strLoadout = "Y" Then
                StrQuery = "SELECT TSPL_SALE_INVOICE_DETAIL.Location, d.Shipment_No AS Transfer_No, " & _
                     "d.Shipment_Date AS Transfer_Date, d.Vehicle_Code, d.Salesman_Code, " & _
                      "c.Emp_Name AS salesmanName, TSPL_SHIPMENT_DETAILS.Item_Code, " & _
                      "TSPL_SHIPMENT_DETAILS.Item_Desc,TSPL_SHIPMENT_DETAILS.Shipped_Qty AS Item_Qty, " & _
                      " CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') " & _
                      "THEN isnull(TSPL_SHIPMENT_DETAILS.empty_value,0)  + (isnull(TSPL_SHIPMENT_DETAILS.Basic_Rate,0) * isnull(TSPL_SHIPMENT_DETAILS.Shipped_Qty,0)) ELSE (isnull(TSPL_SHIPMENT_DETAILS.Total_Item_Amt,0) + isnull(TSPL_SHIPMENT_DETAILS.empty_value,0)) end AS LoadOutamt, " & _
                      "TSPL_SHIPMENT_DETAILS.Shipped_Qty, CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') THEN 0 ELSE isnull(Invoice_Qty, 0) END AS InvQty, " & _
                      "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') THEN TSPL_SALE_INVOICE_DETAIL.Invoice_Qty ELSE 0 END AS FOCqty, " & _
                      "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') " & _
                      "THEN 0 ELSE isnull((TSPL_SALE_INVOICE_DETAIL.Total_net_Amt + TSPL_SALE_INVOICE_DETAIL.total_tpt + TSPL_SALE_INVOICE_DETAIL.Total_Tax_Amt " & _
                      " + TSPL_SALE_INVOICE_DETAIL.Empty_Value), 0) END AS InvAMt, CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') THEN CASE WHEN " & _
                      "((SELECT excisable FROM tspl_location_master " & _
                      "WHERE TSPL_LOCATION_MASTER.Location_Code = TSPL_SALE_INVOICE_DETAIL.Location) = 'T') " & _
                      "THEN TSPL_SALE_INVOICE_DETAIL.Empty_Value + (isnull(TSPL_SHIPMENT_DETAILS.Basic_Rate,0) * isnull(TSPL_SHIPMENT_DETAILS.Shipped_Qty,0)) ELSE TSPL_SALE_INVOICE_DETAIL.Empty_Value + (isnull(TSPL_SHIPMENT_DETAILS.Basic_Rate,0) * isnull(TSPL_SHIPMENT_DETAILS.Shipped_Qty,0)) " & _
                      "END ELSE 0 END AS FOCamt, 0 AS LoadInQty, 0 AS LoadInAMt, (TSPL_SALE_INVOICE_DETAIL.MRP_Amt * TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor) AS mrp, " & _
                      "CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code IS NULL " & _
                      "THEN TSPL_SHIPMENT_DETAILS.Unit_code ELSE TSPL_SALE_INVOICE_DETAIL.Unit_code END AS Unit_code, " & _
                      "TSPL_SALE_INVOICE_DETAIL.Basic_Rate, CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code IS NULL THEN " & _
                      "(SELECT top 1 Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL " & _
                      "WHERE TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SHIPMENT_DETAILS.Item_Code AND " & _
                      "TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_SHIPMENT_DETAILS.Unit_code) ELSE " & _
                      "(SELECT top 1  Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL " & _
                      "WHERE TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code AND " & _
                      "TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_SALE_INVOICE_DETAIL.Unit_code) END AS Conversion_Factor, 0 AS Empty_Value," & _
                      "0 AS BasicPrice_WithTax, TSPL_SHIPMENT_DETAILS.TPT AS TPT_Value," & _
                      "(SELECT top 1 Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL " & _
                      "WHERE (Item_Code = TSPL_SHIPMENT_DETAILS.Item_Code) AND (UOM_Code = TSPL_SHIPMENT_DETAILS.Unit_code)) AS Transfer_Convert_F " & _
                      ",(isnull(TSPL_SALE_INVOICE_HEAD.Shell_Qty,0) * 100) as ShellQty,'L' as Type " & _
                      "FROM TSPL_ITEM_UOM_DETAIL AS TSPL_ITEM_UOM_DETAIL_1 INNER JOIN " & _
                      "TSPL_SHIPMENT_DETAILS INNER JOIN " & _
                      "TSPL_SHIPMENT_MASTER AS d ON TSPL_SHIPMENT_DETAILS.Shipment_No = d.Shipment_No INNER JOIN " & _
                      "TSPL_EMPLOYEE_MASTER AS c ON d.Salesman_Code = c.EMP_CODE ON " & _
                      "TSPL_ITEM_UOM_DETAIL_1.Item_Code = TSPL_SHIPMENT_DETAILS.Item_Code AND " & _
                      "TSPL_ITEM_UOM_DETAIL_1.UOM_Code = TSPL_SHIPMENT_DETAILS.Unit_code INNER JOIN " & _
                      "TSPL_SALE_INVOICE_DETAIL INNER JOIN " & _
                      "TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ON " & _
                      "TSPL_SHIPMENT_DETAILS.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code AND " & _
                      "TSPL_SHIPMENT_DETAILS.Unit_code = TSPL_SALE_INVOICE_DETAIL.Unit_code And " & _
                      "TSPL_SHIPMENT_DETAILS.Location = TSPL_SALE_INVOICE_DETAIL.Location AND " & _
                      "d.Shipment_No = TSPL_SALE_INVOICE_HEAD.Shipment_No AND " & _
                      "TSPL_SHIPMENT_DETAILS.Shipment_Id = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id "
                StrQuery1 = "SELECT  c.Sale_Invoice_No, c.Sale_Invoice_Date," & _
                         "(SELECT isnull(SUM(Applied_Amount),0) FROM TSPL_RECEIPT_DETAIL WHERE (Document_No = c.Sale_Invoice_No)) as Receip_amt, " & _
                           "ISNULL((select sum(isnull(Adjustment_Amount,0)) from TSPL_Receipt_Adjustment_Header where Doc_No=c.Sale_Invoice_No),0)  AS Receip_adjustment_amt, " & _
                           "(select ISNULL(SUM(isnull(Item_Cost,0) + isnull(Breakage_Cost,0)),0) from TSPL_ADJUSTMENT_HEADER, " & _
                           "TSPL_ADJUSTMENT_DETAIL  WHERE TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No AND Document_No=C.Sale_Invoice_No) as Receipt_Empty, " & _
                           "TSPL_SHIPMENT_MASTER.Shipment_No as Transfer_No ,c.Inv_Tax_Amt, c.Inv_Detail_Total_Amt, " & _
                           "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                           "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN 0 ELSE (Invoice_Qty * TSPL_SALE_INVOICE_DETAIL.Item_Net_Amt) END as InvAmt,CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                           "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN 0 ELSE (Invoice_Qty/Conversion_Factor) END AS InvQty," & _
                           "c.Empty_Value as Empty_Value, " & _
                           "c.TPT,CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN TSPL_SALE_INVOICE_DETAIL.Empty_Value ELSE 0 END as FOCValue, " & _
                           "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN  (Invoice_Qty/Conversion_Factor) else 0 END AS FOCQty,c.Cust_Name FROM TSPL_SALE_INVOICE_HEAD AS c INNER JOIN " & _
                           "TSPL_SHIPMENT_MASTER ON c.Shipment_No = TSPL_SHIPMENT_MASTER.Shipment_No INNER JOIN " & _
                           "TSPL_SALE_INVOICE_DETAIL ON c.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " & _
                           "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                           "TSPL_SALE_INVOICE_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code "
                StrQuery2 = "select a.Transfer_No as Load_Out_No,a.Transfer_Date,a.Load_Out_No as Transfer_No, " & _
                             "b.Item_Code,b.Item_Desc,(b.LoadIn_Qty/Conversion_Factor) + b.Burst + b.Leak  +  b.shortage as LoadIn_Qty,b.Uom,b.MRP, " & _
                             "case when b.LoadIn_Qty > 0 then ((b.LoadIn_Qty ) * (b.BasicPrice_WithTax + b.TPT_Value + b.Empty_Value)) else 0 end as Loadinamt " & _
                             "from TSPL_TRANSFER_HEAD AS a INNER JOIN " & _
                             "TSPL_TRANSFER_DETAIL AS b ON a.Transfer_No = b.Transfer_No INNER JOIN " & _
                             "TSPL_ITEM_UOM_DETAIL ON b.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND b.Uom = TSPL_ITEM_UOM_DETAIL.UOM_Code where " & _
                             "Transfer_Type='Sale' "
                StrQuery3 = "select a.Payment_No,convert(date,a.Payment_Date,103) as Payment_Date,a.Payment_Amount,case when a.Apply_To='' then a.loadoutNo else a.Apply_To end as Apply_To from TSPL_PAYMENT_HEADER a where a.Apply_By='Sale'  "

                StrQuery4 = "select a.Adjustment_No,convert(date,a.Adjustment_Date,103) as Adjustment_Date,b.Adjustment_Type,b.Item_Code, " & _
                "b.Item_Description,b.Item_Quantity,a.Document_No,b.Item_Cost,b.Breakage,b.Breakage_Cost,TSPL_SHIPMENT_MASTER.Shipment_No as Transfer_No,b.Unit_Code from " & _
                            "TSPL_ADJUSTMENT_HEADER a ,TSPL_ADJUSTMENT_DETAIL b,TSPL_SALE_INVOICE_HEAD, " & _
                            "TSPL_SHIPMENT_MASTER where a.Adjustment_No=b.Adjustment_No and a.Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No and " & _
                            "TSPL_SALE_INVOICE_HEAD.Shipment_No=TSPL_SHIPMENT_MASTER.Shipment_No "

            Else
                StrQuery = "SELECT TSPL_SALE_INVOICE_DETAIL.Location, d.Shipment_No AS Transfer_No, " & _
                     "d.Shipment_Date AS Transfer_Date, d.Vehicle_Code, d.Salesman_Code, " & _
                      "c.Emp_Name AS salesmanName, TSPL_SHIPMENT_DETAILS.Item_Code, " & _
                      "TSPL_SHIPMENT_DETAILS.Item_Desc,TSPL_SHIPMENT_DETAILS.Shipped_Qty AS Item_Qty, " & _
                      " CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') " & _
                      "THEN isnull(TSPL_SHIPMENT_DETAILS.empty_value,0)  + (isnull(TSPL_SHIPMENT_DETAILS.Basic_Rate,0) * isnull(TSPL_SHIPMENT_DETAILS.Shipped_Qty,0)) ELSE (isnull(TSPL_SHIPMENT_DETAILS.Total_Item_Amt,0) + isnull(TSPL_SHIPMENT_DETAILS.empty_value,0)) end AS LoadOutamt, " & _
                      "TSPL_SHIPMENT_DETAILS.Shipped_Qty, CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') THEN 0 ELSE isnull(Invoice_Qty, 0) END AS InvQty, " & _
                      "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') THEN TSPL_SALE_INVOICE_DETAIL.Invoice_Qty ELSE 0 END AS FOCqty, " & _
                      "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') " & _
                      "THEN 0 ELSE isnull((TSPL_SALE_INVOICE_DETAIL.Total_net_Amt + TSPL_SALE_INVOICE_DETAIL.total_tpt + TSPL_SALE_INVOICE_DETAIL.Total_Tax_Amt " & _
                      " + TSPL_SALE_INVOICE_DETAIL.Empty_Value), 0) END AS InvAMt, CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR " & _
                      "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') THEN CASE WHEN " & _
                      "((SELECT excisable FROM tspl_location_master " & _
                      "WHERE TSPL_LOCATION_MASTER.Location_Code = TSPL_SALE_INVOICE_DETAIL.Location) = 'T') " & _
                      "THEN TSPL_SALE_INVOICE_DETAIL.Empty_Value + (isnull(TSPL_SHIPMENT_DETAILS.Basic_Rate,0) * isnull(TSPL_SHIPMENT_DETAILS.Shipped_Qty,0)) ELSE TSPL_SALE_INVOICE_DETAIL.Empty_Value + (isnull(TSPL_SHIPMENT_DETAILS.Basic_Rate,0) * isnull(TSPL_SHIPMENT_DETAILS.Shipped_Qty,0)) " & _
                      "END ELSE 0 END AS FOCamt, 0 AS LoadInQty, 0 AS LoadInAMt, (TSPL_SALE_INVOICE_DETAIL.MRP_Amt * TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor) AS mrp, " & _
                      "CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code IS NULL " & _
                      "THEN TSPL_SHIPMENT_DETAILS.Unit_code ELSE TSPL_SALE_INVOICE_DETAIL.Unit_code END AS Unit_code, " & _
                      "TSPL_SALE_INVOICE_DETAIL.Basic_Rate, CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code IS NULL THEN " & _
                      "(SELECT top 1 Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL " & _
                      "WHERE TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SHIPMENT_DETAILS.Item_Code AND " & _
                      "TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_SHIPMENT_DETAILS.Unit_code) ELSE " & _
                      "(SELECT top 1  Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL " & _
                      "WHERE TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code AND " & _
                      "TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_SALE_INVOICE_DETAIL.Unit_code) END AS Conversion_Factor, 0 AS Empty_Value," & _
                      "0 AS BasicPrice_WithTax, TSPL_SHIPMENT_DETAILS.TPT AS TPT_Value," & _
                      "(SELECT top 1 Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL " & _
                      "WHERE (Item_Code = TSPL_SHIPMENT_DETAILS.Item_Code) AND (UOM_Code = TSPL_SHIPMENT_DETAILS.Unit_code)) AS Transfer_Convert_F " & _
                      ",(isnull(TSPL_SALE_INVOICE_HEAD.Shell_Qty,0) * 100) as ShellQty,'L' as Type " & _
                      "FROM TSPL_ITEM_UOM_DETAIL AS TSPL_ITEM_UOM_DETAIL_1 INNER JOIN " & _
                      "TSPL_SHIPMENT_DETAILS INNER JOIN " & _
                      "TSPL_SHIPMENT_MASTER AS d ON TSPL_SHIPMENT_DETAILS.Shipment_No = d.Shipment_No INNER JOIN " & _
                      "TSPL_EMPLOYEE_MASTER AS c ON d.Salesman_Code = c.EMP_CODE ON " & _
                      "TSPL_ITEM_UOM_DETAIL_1.Item_Code = TSPL_SHIPMENT_DETAILS.Item_Code AND " & _
                      "TSPL_ITEM_UOM_DETAIL_1.UOM_Code = TSPL_SHIPMENT_DETAILS.Unit_code INNER JOIN " & _
                      "TSPL_SALE_INVOICE_DETAIL INNER JOIN " & _
                      "TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ON " & _
                      "TSPL_SHIPMENT_DETAILS.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code AND " & _
                      "TSPL_SHIPMENT_DETAILS.Unit_code = TSPL_SALE_INVOICE_DETAIL.Unit_code And " & _
                      "TSPL_SHIPMENT_DETAILS.Location = TSPL_SALE_INVOICE_DETAIL.Location AND " & _
                      "d.Shipment_No = TSPL_SALE_INVOICE_HEAD.Shipment_No AND " & _
                      "TSPL_SHIPMENT_DETAILS.Shipment_Id = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id where d.Shipment_Type='Sale'"
                StrQuery1 = "SELECT  c.Sale_Invoice_No, c.Sale_Invoice_Date," & _
                         "(SELECT isnull(SUM(Applied_Amount),0) FROM TSPL_RECEIPT_DETAIL WHERE (Document_No = c.Sale_Invoice_No)) as Receip_amt, " & _
                           "ISNULL((select sum(isnull(Adjustment_Amount,0)) from TSPL_Receipt_Adjustment_Header where Doc_No=c.Sale_Invoice_No),0)  AS Receip_adjustment_amt, " & _
                           "(select ISNULL(SUM(isnull(Item_Cost,0) + isnull(Breakage_Cost,0)),0) from TSPL_ADJUSTMENT_HEADER, " & _
                           "TSPL_ADJUSTMENT_DETAIL  WHERE TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No AND Document_No=C.Sale_Invoice_No) as Receipt_Empty, " & _
                           "TSPL_SHIPMENT_MASTER.Shipment_No as Transfer_No ,c.Inv_Tax_Amt, c.Inv_Detail_Total_Amt, " & _
                           "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                           "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN 0 ELSE (Invoice_Qty * TSPL_SALE_INVOICE_DETAIL.Item_Net_Amt) END as InvAmt,CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                           "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN 0 ELSE (Invoice_Qty/Conversion_Factor) END AS InvQty," & _
                           "c.Empty_Value as Empty_Value, " & _
                           "c.TPT,CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN TSPL_SALE_INVOICE_DETAIL.Empty_Value ELSE 0 END as FOCValue, " & _
                           "CASE WHEN (TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y'  ) THEN  (Invoice_Qty/Conversion_Factor) else 0 END AS FOCQty,c.Cust_Name FROM TSPL_SALE_INVOICE_HEAD AS c INNER JOIN " & _
                           "TSPL_SHIPMENT_MASTER ON c.Shipment_No = TSPL_SHIPMENT_MASTER.Shipment_No INNER JOIN " & _
                           "TSPL_SALE_INVOICE_DETAIL ON c.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " & _
                           "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                           "TSPL_SALE_INVOICE_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                           "where  TSPL_SHIPMENT_MASTER.Shipment_Type='Sale'"
                StrQuery2 = "select a.Transfer_No as Load_Out_No,a.Transfer_Date,a.Load_Out_No as Transfer_No, " & _
                             "b.Item_Code,b.Item_Desc,(b.LoadIn_Qty/Conversion_Factor) + b.Burst + b.Leak  +  b.shortage as LoadIn_Qty,b.Uom,b.MRP, " & _
                             "case when b.LoadIn_Qty > 0 then ((b.LoadIn_Qty ) * (b.BasicPrice_WithTax + b.TPT_Value + b.Empty_Value)) else 0 end as Loadinamt " & _
                             "from TSPL_TRANSFER_HEAD AS a INNER JOIN " & _
                             "TSPL_TRANSFER_DETAIL AS b ON a.Transfer_No = b.Transfer_No INNER JOIN " & _
                             "TSPL_ITEM_UOM_DETAIL ON b.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND b.Uom = TSPL_ITEM_UOM_DETAIL.UOM_Code where " & _
                             "Transfer_Type='Sale' "
                StrQuery3 = "select a.Payment_No,convert(date,a.Payment_Date,103) as Payment_Date,a.Payment_Amount,case when a.Apply_To='' then a.loadoutNo else a.Apply_To end as Apply_To from TSPL_PAYMENT_HEADER a where a.Apply_By='Sale'  "


                StrQuery4 = "select a.Adjustment_No,convert(date,a.Adjustment_Date,103) as Adjustment_Date,b.Adjustment_Type,b.Item_Code, " & _
                "b.Item_Description,b.Item_Quantity,a.Document_No,b.Item_Cost,b.Breakage,b.Breakage_Cost,TSPL_SHIPMENT_MASTER.Shipment_No as Transfer_No,b.Unit_Code from " & _
                            "TSPL_ADJUSTMENT_HEADER a ,TSPL_ADJUSTMENT_DETAIL b,TSPL_SALE_INVOICE_HEAD, " & _
                            "TSPL_SHIPMENT_MASTER where a.Adjustment_No=b.Adjustment_No and a.Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No and " & _
                            "TSPL_SALE_INVOICE_HEAD.Shipment_No=TSPL_SHIPMENT_MASTER.Shipment_No "
                If strLoadout = "N" Then
                    StrQuery += " and d.Shipment_No in (" + clsCommon.GetMulcallString(cbgLoadOut.CheckedValue) + ") "
                    StrQuery1 += " and TSPL_SHIPMENT_MASTER.Shipment_No in (" + clsCommon.GetMulcallString(cbgLoadOut.CheckedValue) + ") "
                    StrQuery4 += " and TSPL_SHIPMENT_MASTER.Shipment_No in (" + clsCommon.GetMulcallString(cbgLoadOut.CheckedValue) + ") "
                End If
            End If

            If strLoadout = "Y" And strLocAll = "N" Then
                StrQuery += " and d.Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funsubreport(CrystalReportFolder.SalesReport, StrQuery, StrQuery1, StrQuery2, StrQuery3, StrQuery4, "crptSettlement", "Settlement Report", "crptSettlementReceiptDetails.rpt", "crptSettlementLoadin.rpt", "crptSettlementpayment.rpt", "crptSettlementAdjustment.rpt")
        End If
    End Sub

  
    Private Sub FrmSettlementReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")




        SetUserMgmtNew()
        LoadLocation()
        LoadLoadOut()
        LoadTransfer()
        dtpFdate.Value = clsCommon.GETSERVERDATE
        DtpTodate.Value = clsCommon.GETSERVERDATE
        chkLocationAll.IsChecked = True
        chktransferAll.IsChecked = True
        chkLoadoutAll.IsChecked = True
        rdbTransferwise.IsChecked = True
    End Sub

    Private Sub chktransferAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktransferAll.ToggleStateChanged
        cbgTransfer.Enabled = Not chktransferAll.IsChecked
    End Sub

    Private Sub chkLoadoutAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLoadoutAll.ToggleStateChanged
        cbgLoadOut.Enabled = Not chkLoadoutAll.IsChecked
    End Sub

End Class

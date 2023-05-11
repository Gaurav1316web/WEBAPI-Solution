Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Public Class FrmAdditionalCharge1
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery As String
    Dim dr As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        dr = clsDBFuncationality.GetDataTable(sql)

        l1User = dr.Rows(0)(0).ToString()
        l2User = dr.Rows(0)(1).ToString()
        l3User = dr.Rows(0)(2).ToString()
        l4User = dr.Rows(0)(3).ToString()
        l5User = dr.Rows(0)(4).ToString()
    End Sub
    Sub LoadAddCharge()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Code,Description from TSPL_Additional_Charges"
        cbgAddCost.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgAddCost.ValueMember = "Code"
        cbgAddCost.DisplayMember = "Description"
    End Sub

    Private Sub chkAddCostAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAddCostAll.ToggleStateChanged
        cbgAddCost.Enabled = Not chkAddCostAll.IsChecked
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.AddCharge)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

        '         btnclose.Visible = MyBase.isDeleteFlag

    End Sub


    Private Sub FrmAdditionalCharge1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        LoadAddCharge()
        chkAddCostAll.IsChecked = True
        'serverDate()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Reset the Window")
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ADD-CHARGE"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

        Reset()
    End Sub
    Sub Reset()

        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        LoadAddCharge()
        chkAddCostAll.IsChecked = True
    End Sub
    Sub PrintData()
        If chkAddCostSelect.IsChecked = True AndAlso cbgAddCost.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Additional COst or select ALL")
            Return
        End If
        Dim strAddCostAll As String = ""
        Dim additionalcharges As String = ""
        Dim Stradditionalcharges As String = ""
        Dim fromdate1 As String = clsCommon.myCDate(fromDate.Value, "dd/MM/yyyy")
        Dim Todate1 As String = clsCommon.myCDate(ToDate.Value, "dd/MM/yyyy")

        If chkAddCostSelect.IsChecked = True AndAlso cbgAddCost.CheckedValue.Count > 0 Then
            additionalcharges = "'" + clsCommon.GetMulcallString(cbgAddCost.CheckedValue) + "'"
            Stradditionalcharges = additionalcharges.Replace("'", "")
        End If
        If chkAddCostAll.IsChecked = True Then
            strAddCostAll = "Y"
        Else
            strAddCostAll = "N"
        End If
        Dim strSql1 As String = "select '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges,TSPL_SRN_HEAD.SRN_Date, Item_Code,Item_Desc,TSPL_SRN_HEAD.SRN_No,Amount," & _
        "case when (select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code=TSPL_SRN_DETAIL.TAX1)='Y' then 0 " & _
        "else TSPL_SRN_DETAIL.TAX1_Amt end as Tax1mt, " & _
        "case when (select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code=TSPL_SRN_DETAIL.TAX2)='Y' then 0 " & _
        "else TSPL_SRN_DETAIL.TAX2_Amt end as Tax2mt, " & _
        "case when (select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code=TSPL_SRN_DETAIL.TAX3)='Y' then 0 " & _
        "else TSPL_SRN_DETAIL.TAX3_Amt end as Tax3mt , " & _
        "case when (select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code=TSPL_SRN_DETAIL.TAX4)='Y' then 0 " & _
        "else TSPL_SRN_DETAIL.TAX4_Amt end as Tax4mt , " & _
        "case when (select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code=TSPL_SRN_DETAIL.TAX5)='Y' then 0 " & _
        "else TSPL_SRN_DETAIL.TAX5_Amt end as Tax5mt, " & _
        "case when (select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code=TSPL_SRN_DETAIL.TAX6)='Y' then 0 " & _
        "else TSPL_SRN_DETAIL.TAX6_Amt end as Tax6mt, " & _
        "case when (select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code=TSPL_SRN_DETAIL.TAX7)='Y' then 0 " & _
        "else TSPL_SRN_DETAIL.TAX7_Amt end as Tax7mt, " & _
        "case when (select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code=TSPL_SRN_DETAIL.TAX8)='Y' then 0 " & _
        "else TSPL_SRN_DETAIL.TAX8_Amt end as Tax8mt, " & _
        "case when (select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code=TSPL_SRN_DETAIL.TAX9)='Y' then 0 " & _
        "else TSPL_SRN_DETAIL.TAX9_Amt end as Tax9mt, " & _
        "case when (select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code=TSPL_SRN_DETAIL.TAX10)='Y' then 0 " & _
        "else TSPL_SRN_DETAIL.TAX10_Amt end as Tax10mt " & _
        "from TSPL_SRN_HEAD inner join TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Row_Type='Misc' and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >=  convert(date,'" & fromDate.Value & "',103) AND  " & _
        "  convert(date,TSPL_SRN_HEAD.SRN_Date,103) <= convert(date, '" & ToDate.Value & "',103) "

        Dim Un1 As String = "Union all "

        Dim strSql2 As String = "select '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges, TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date as SRN_Date,AddChargeCode as Item_Code,AddChargeDesc as Item_Desc,RefDocNo,Amount,0 as Tax1mt, " & _
        "0 as Tax2mt,0 as Tax3mt,0 as Tax4mt,0 as Tax5mt,0 as Tax6mt,0 as Tax7mt,0 as Tax8mt,0 as Tax9mt, " & _
        "0 as Tax10mt from TSPL_VENDOR_INVOICE_HEAD inner join TSPL_VENDOR_INVOICE_DETAIL on " & _
        "TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No where RefDocType='S' and AddChargeCode <> ''  and convert(date,Invoice_Entry_Date,103) >=  convert(date,'" & fromDate.Value & "',103) AND  " & _
        "convert(date,Invoice_Entry_Date,103) <= convert(date, '" & ToDate.Value & "',103) "

        Dim str1 As String = "select '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges,TSPL_SRN_HEAD.SRN_Date, TSPL_SRN_HEAD.Add_Charge_Code1 as Item_Code,TSPL_SRN_HEAD.Add_Charge_Name1 as Item_Desc, " & _
        "TSPL_SRN_HEAD.SRN_No,Add_Charge_Amt1 as Amount,0 as Tax1mt,0 as Tax2mt,0 as Tax3mt ,0 as Tax4mt,0 as Tax5mt, " & _
        "0 as Tax6mt,0 as Tax7mt,0 as Tax8mt,0 Tax9mt,0 as Tax10mt from TSPL_SRN_HEAD inner join " & _
        "TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Add_Charge_Code1 <> '' and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >=  convert(date,'" & fromDate.Value & "',103) AND  " & _
        "  convert(date,TSPL_SRN_HEAD.SRN_Date,103) <= convert(date, '" & ToDate.Value & "',103) "

        Dim str2 As String = "select  '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges,TSPL_SRN_HEAD.SRN_Date,TSPL_SRN_HEAD.Add_Charge_Code2 as Item_Code,TSPL_SRN_HEAD.Add_Charge_Name2 as Item_Desc, " & _
       "TSPL_SRN_HEAD.SRN_No,Add_Charge_Amt2 as Amount,0 as Tax1mt,0 as Tax2mt,0 as Tax3mt ,0 as Tax4mt,0 as Tax5mt, " & _
       "0 as Tax6mt,0 as Tax7mt,0 as Tax8mt,0 Tax9mt,0 as Tax10mt from TSPL_SRN_HEAD inner join " & _
       "TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Add_Charge_Code2 <> '' and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >=  convert(date,'" & fromDate.Value & "',103) AND  " & _
        "  convert(date,TSPL_SRN_HEAD.SRN_Date,103) <= convert(date, '" & ToDate.Value & "',103) "

        Dim str3 As String = "select '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges,TSPL_SRN_HEAD.SRN_Date, TSPL_SRN_HEAD.Add_Charge_Code3 as Item_Code,TSPL_SRN_HEAD.Add_Charge_Name3 as Item_Desc, " & _
       "TSPL_SRN_HEAD.SRN_No,Add_Charge_Amt3 as Amount,0 as Tax1mt,0 as Tax2mt,0 as Tax3mt ,0 as Tax4mt,0 as Tax5mt, " & _
       "0 as Tax6mt,0 as Tax7mt,0 as Tax8mt,0 Tax9mt,0 as Tax10mt from TSPL_SRN_HEAD inner join " & _
       "TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Add_Charge_Code3 <> '' and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >=  convert(date,'" & fromDate.Value & "',103) AND  " & _
        "  convert(date,TSPL_SRN_HEAD.SRN_Date,103) <= convert(date, '" & ToDate.Value & "',103) "

        Dim str4 As String = "select '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges, TSPL_SRN_HEAD.SRN_Date,TSPL_SRN_HEAD.Add_Charge_Code4 as Item_Code,TSPL_SRN_HEAD.Add_Charge_Name4 as Item_Desc, " & _
       "TSPL_SRN_HEAD.SRN_No,Add_Charge_Amt1 as Amount,0 as Tax1mt,0 as Tax2mt,0 as Tax3mt ,0 as Tax4mt,0 as Tax5mt, " & _
       "0 as Tax6mt,0 as Tax7mt,0 as Tax8mt,0 Tax9mt,0 as Tax10mt from TSPL_SRN_HEAD inner join " & _
       "TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Add_Charge_Code4 <> '' and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >=  convert(date,'" & fromDate.Value & "',103) AND  " & _
        "  convert(date,TSPL_SRN_HEAD.SRN_Date,103) <= convert(date, '" & ToDate.Value & "',103) "

        Dim str5 As String = "select '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges, TSPL_SRN_HEAD.SRN_Date, TSPL_SRN_HEAD.Add_Charge_Code5 as Item_Code,TSPL_SRN_HEAD.Add_Charge_Name5 as Item_Desc, " & _
       "TSPL_SRN_HEAD.SRN_No,Add_Charge_Amt5 as Amount,0 as Tax1mt,0 as Tax2mt,0 as Tax3mt ,0 as Tax4mt,0 as Tax5mt, " & _
       "0 as Tax6mt,0 as Tax7mt,0 as Tax8mt,0 Tax9mt,0 as Tax10mt from TSPL_SRN_HEAD inner join " & _
       "TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Add_Charge_Code5 <> '' and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >=  convert(date,'" & fromDate.Value & "',103) AND  " & _
        "  convert(date,TSPL_SRN_HEAD.SRN_Date,103) <= convert(date, '" & ToDate.Value & "',103) "

        Dim str6 As String = "select '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges, TSPL_SRN_HEAD.SRN_Date, TSPL_SRN_HEAD.Add_Charge_Code6 as Item_Code,TSPL_SRN_HEAD.Add_Charge_Name6 as Item_Desc, " & _
       "TSPL_SRN_HEAD.SRN_No,Add_Charge_Amt6 as Amount,0 as Tax1mt,0 as Tax2mt,0 as Tax3mt ,0 as Tax4mt,0 as Tax5mt, " & _
       "0 as Tax6mt,0 as Tax7mt,0 as Tax8mt,0 Tax9mt,0 as Tax10mt from TSPL_SRN_HEAD inner join " & _
       "TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Add_Charge_Code6 <> '' and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >=  convert(date,'" & fromDate.Value & "',103) AND  " & _
        "  convert(date,TSPL_SRN_HEAD.SRN_Date,103) <= convert(date, '" & ToDate.Value & "',103) "

        Dim str7 As String = "select '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges, TSPL_SRN_HEAD.SRN_Date, TSPL_SRN_HEAD.Add_Charge_Code7 as Item_Code,TSPL_SRN_HEAD.Add_Charge_Name7 as Item_Desc, " & _
       "TSPL_SRN_HEAD.SRN_No,Add_Charge_Amt7 as Amount,0 as Tax1mt,0 as Tax2mt,0 as Tax3mt ,0 as Tax4mt,0 as Tax5mt, " & _
       "0 as Tax6mt,0 as Tax7mt,0 as Tax8mt,0 Tax9mt,0 as Tax10mt from TSPL_SRN_HEAD inner join " & _
       "TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Add_Charge_Code7 <> '' and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >=  convert(date,'" & fromDate.Value & "',103) AND  " & _
        "  convert(date,TSPL_SRN_HEAD.SRN_Date,103) <= convert(date, '" & ToDate.Value & "',103) "

        Dim str8 As String = "select '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges, TSPL_SRN_HEAD.SRN_Date, TSPL_SRN_HEAD.Add_Charge_Code8 as Item_Code,TSPL_SRN_HEAD.Add_Charge_Name8 as Item_Desc, " & _
       "TSPL_SRN_HEAD.SRN_No,Add_Charge_Amt8 as Amount,0 as Tax1mt,0 as Tax2mt,0 as Tax3mt ,0 as Tax4mt,0 as Tax5mt, " & _
       "0 as Tax6mt,0 as Tax7mt,0 as Tax8mt,0 Tax9mt,0 as Tax10mt from TSPL_SRN_HEAD inner join " & _
       "TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Add_Charge_Code8 <> '' and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >=  convert(date,'" & fromDate.Value & "',103) AND  " & _
        "  convert(date,TSPL_SRN_HEAD.SRN_Date,103) <= convert(date, '" & ToDate.Value & "',103) "

        Dim str9 As String = "select '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges, TSPL_SRN_HEAD.SRN_Date, TSPL_SRN_HEAD.Add_Charge_Code9 as Item_Code,TSPL_SRN_HEAD.Add_Charge_Name9 as Item_Desc, " & _
       "TSPL_SRN_HEAD.SRN_No,Add_Charge_Amt9 as Amount,0 as Tax1mt,0 as Tax2mt,0 as Tax3mt ,0 as Tax4mt,0 as Tax5mt, " & _
       "0 as Tax6mt,0 as Tax7mt,0 as Tax8mt,0 Tax9mt,0 as Tax10mt from TSPL_SRN_HEAD inner join " & _
       "TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Add_Charge_Code9 <> '' and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >=  convert(date,'" & fromDate.Value & "',103) AND  " & _
        "  convert(date,TSPL_SRN_HEAD.SRN_Date,103) <= convert(date, '" & ToDate.Value & "',103) "

        Dim str10 As String = "select '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges, TSPL_SRN_HEAD.SRN_Date, TSPL_SRN_HEAD.Add_Charge_Code10 as Item_Code,TSPL_SRN_HEAD.Add_Charge_Name10 as Item_Desc, " & _
       "TSPL_SRN_HEAD.SRN_No,Add_Charge_Amt10 as Amount,0 as Tax1mt,0 as Tax2mt,0 as Tax3mt ,0 as Tax4mt,0 as Tax5mt, " & _
       "0 as Tax6mt,0 as Tax7mt,0 as Tax8mt,0 Tax9mt,0 as Tax10mt from TSPL_SRN_HEAD inner join " & _
       "TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Add_Charge_Code10 <> '' and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >=  convert(date,'" & fromDate.Value & "',103) AND  " & _
        "  convert(date,TSPL_SRN_HEAD.SRN_Date,103) <= convert(date, '" & ToDate.Value & "',103) "

        '-----------------------------------------------------------------------------------------------------------------
        Dim strSql3 As String = "select '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges, TSPL_SRN_HEAD.SRN_No,SRN_Date as DocDate,Inv_No as BillNo,TSPL_SRN_DETAIL.Item_Code, " & _
        "TSPL_ITEM_MASTER.Item_Desc,SRN_Qty,Total_AddtionalCost_PerUnit as [Item_Cost],Amount,Vendor_Code,Vendor_Name from TSPL_SRN_HEAD inner join TSPL_SRN_DETAIL on " & _
        "TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SRN_DETAIL.Item_Code  where Row_Type='Item'  " & _
        "and (select COUNT(a.item_code) from TSPL_SRN_DETAIL a where a.Row_Type='misc'  and a.SRN_No=tspl_srn_head.SRN_No ) > 0  and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
        "  convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=  convert(date,'" & ToDate.Value & "',103) "

        Dim strSql4 As String = "select '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges, RefDocNo as SRN_No,convert(date,Invoice_Entry_Date,103) as DocDate," & _
        "Vendor_Invoice_No as BillNo,TSPL_VENDOR_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,SRN_Qty,Item_Cost,TSPL_SRN_DETAIL.Amount,Vendor_Code, " & _
        "Vendor_Name from TSPL_VENDOR_INVOICE_HEAD inner join TSPL_VENDOR_INVOICE_DETAIL on " & _
        "TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No inner join " & _
        "TSPL_SRN_DETAIL on TSPL_VENDOR_INVOICE_HEAD.RefDocNo=TSPL_SRN_DETAIL.SRN_No  left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_VENDOR_INVOICE_DETAIL.Item_code " & _
        " where RefDocType='S' and convert(date,Invoice_Entry_Date,103) >=  convert(date,'" & fromDate.Value & "',103) AND  " & _
        "convert(date,Invoice_Entry_Date,103) <=  convert(date,'" & ToDate.Value & "',103) "

        Dim str11 As String = "select '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges, TSPL_SRN_HEAD.SRN_No,SRN_Date as DocDate,Inv_No as BillNo,TSPL_SRN_DETAIL.Item_Code, " & _
        "TSPL_ITEM_MASTER.Item_Desc,SRN_Qty,Total_AddtionalCost_PerUnit as [Item_Cost],Total_AddtionalCost_PerUnit *SRN_Qty as [Amount],Vendor_Code,Vendor_Name from TSPL_SRN_HEAD inner join TSPL_SRN_DETAIL on " & _
        "TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SRN_DETAIL.Item_Code where Row_Type='Item'  and (Add_Charge_Code1 <> '' or Add_Charge_Code2 <> ''  " & _
        " or Add_Charge_Code3 <> '' or Add_Charge_Code4 <> '' or Add_Charge_Code5 <> '' or Add_Charge_Code6 <> '' " & _
        " or Add_Charge_Code7 <> '' or Add_Charge_Code8 <> '' or Add_Charge_Code9 <> '' or Add_Charge_Code10 <> '') " & _
        "and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
        "convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=  convert(date,'" & ToDate.Value & "',103) "



        ' ''Dim strSql3 As String = "select '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges, TSPL_SRN_HEAD.SRN_No,SRN_Date as DocDate,Inv_No as BillNo,Item_Code, " & _
        ' ''"Item_Desc,SRN_Qty,Item_Cost,Amount,Vendor_Code,Vendor_Name from TSPL_SRN_HEAD inner join TSPL_SRN_DETAIL on " & _
        ' ''"TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Row_Type='Item'  " & _
        ' ''"and (select COUNT(a.item_code) from TSPL_SRN_DETAIL a where a.Row_Type='misc'  and a.SRN_No=tspl_srn_head.SRN_No ) > 0  and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
        ' ''"  convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=  convert(date,'" & ToDate.Value & "',103) "

        ' ''Dim strSql4 As String = "select '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges, RefDocNo as SRN_No,convert(date,Invoice_Entry_Date,103) as DocDate," & _
        ' ''"Vendor_Invoice_No as BillNo,Item_Code,Item_Desc,SRN_Qty,Item_Cost,TSPL_SRN_DETAIL.Amount,Vendor_Code, " & _
        ' ''"Vendor_Name from TSPL_VENDOR_INVOICE_HEAD inner join TSPL_VENDOR_INVOICE_DETAIL on " & _
        ' ''"TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No inner join " & _
        ' ''"TSPL_SRN_DETAIL on TSPL_VENDOR_INVOICE_HEAD.RefDocNo=TSPL_SRN_DETAIL.SRN_No  where RefDocType='S' and convert(date,Invoice_Entry_Date,103) >=  convert(date,'" & fromDate.Value & "',103) AND  " & _
        ' ''"convert(date,Invoice_Entry_Date,103) <=  convert(date,'" & ToDate.Value & "',103) "

        ' ''Dim str11 As String = "select '" + fromdate1 + "' as FromDate,'" + Todate1 + "' as ToDate, '" + Stradditionalcharges + "' as Stradditionalcharges, TSPL_SRN_HEAD.SRN_No,SRN_Date as DocDate,Inv_No as BillNo,Item_Code, " & _
        ' ''"Item_Desc,SRN_Qty,Item_Cost,Amount,Vendor_Code,Vendor_Name from TSPL_SRN_HEAD inner join TSPL_SRN_DETAIL on " & _
        ' ''"TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Row_Type='Item'  and (Add_Charge_Code1 <> '' or Add_Charge_Code2 <> ''  " & _
        ' ''" or Add_Charge_Code3 <> '' or Add_Charge_Code4 <> '' or Add_Charge_Code5 <> '' or Add_Charge_Code6 <> '' " & _
        ' ''" or Add_Charge_Code7 <> '' or Add_Charge_Code8 <> '' or Add_Charge_Code9 <> '' or Add_Charge_Code10 <> '') " & _
        ' ''"and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
        ' ''"convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=  convert(date,'" & ToDate.Value & "',103) "

        'Dim str12 As String = "select TSPL_SRN_HEAD.SRN_No,SRN_Date as DocDate,Inv_No as BillNo,Item_Code, " & _
        '"Item_Desc,SRN_Qty,Item_Cost,Amount,Vendor_Code,Vendor_Name from TSPL_SRN_HEAD inner join TSPL_SRN_DETAIL on " & _
        '"TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Row_Type='Item'  and Add_Charge_Code2 <> ''  " & _
        '"and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
        '"convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=  convert(date,'" & ToDate.Value & "',103) "

        'Dim str13 As String = "select TSPL_SRN_HEAD.SRN_No,SRN_Date as DocDate,Inv_No as BillNo,Item_Code, " & _
        '"Item_Desc,SRN_Qty,Item_Cost,Amount,Vendor_Code,Vendor_Name from TSPL_SRN_HEAD inner join TSPL_SRN_DETAIL on " & _
        '"TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Row_Type='Item'  and Add_Charge_Code3 <> ''  " & _
        '"and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
        '"convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=  convert(date,'" & ToDate.Value & "',103) "

        'Dim str14 As String = "select TSPL_SRN_HEAD.SRN_No,SRN_Date as DocDate,Inv_No as BillNo,Item_Code, " & _
        '"Item_Desc,SRN_Qty,Item_Cost,Amount,Vendor_Code,Vendor_Name from TSPL_SRN_HEAD inner join TSPL_SRN_DETAIL on " & _
        '"TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Row_Type='Item'  and Add_Charge_Code4 <> ''  " & _
        '"and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
        '"convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=  convert(date,'" & ToDate.Value & "',103) "

        'Dim str15 As String = "select TSPL_SRN_HEAD.SRN_No,SRN_Date as DocDate,Inv_No as BillNo,Item_Code, " & _
        '"Item_Desc,SRN_Qty,Item_Cost,Amount,Vendor_Code,Vendor_Name from TSPL_SRN_HEAD inner join TSPL_SRN_DETAIL on " & _
        '"TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Row_Type='Item'  and Add_Charge_Code5 <> ''  " & _
        '"and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
        '"convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=  convert(date,'" & ToDate.Value & "',103) "

        'Dim str16 As String = "select TSPL_SRN_HEAD.SRN_No,SRN_Date as DocDate,Inv_No as BillNo,Item_Code, " & _
        '"Item_Desc,SRN_Qty,Item_Cost,Amount,Vendor_Code,Vendor_Name from TSPL_SRN_HEAD inner join TSPL_SRN_DETAIL on " & _
        '"TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Row_Type='Item'  and Add_Charge_Code6 <> ''  " & _
        '"and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
        '"convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=  convert(date,'" & ToDate.Value & "',103) "

        'Dim str17 As String = "select TSPL_SRN_HEAD.SRN_No,SRN_Date as DocDate,Inv_No as BillNo,Item_Code, " & _
        '"Item_Desc,SRN_Qty,Item_Cost,Amount,Vendor_Code,Vendor_Name from TSPL_SRN_HEAD inner join TSPL_SRN_DETAIL on " & _
        '"TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Row_Type='Item'  and Add_Charge_Code7 <> ''  " & _
        '"and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
        '"convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=  convert(date,'" & ToDate.Value & "',103) "

        'Dim str18 As String = "select TSPL_SRN_HEAD.SRN_No,SRN_Date as DocDate,Inv_No as BillNo,Item_Code, " & _
        '"Item_Desc,SRN_Qty,Item_Cost,Amount,Vendor_Code,Vendor_Name from TSPL_SRN_HEAD inner join TSPL_SRN_DETAIL on " & _
        '"TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Row_Type='Item'  and Add_Charge_Code8 <> ''  " & _
        '"and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
        '"convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=  convert(date,'" & ToDate.Value & "',103) "

        'Dim str19 As String = "select TSPL_SRN_HEAD.SRN_No,SRN_Date as DocDate,Inv_No as BillNo,Item_Code, " & _
        '"Item_Desc,SRN_Qty,Item_Cost,Amount,Vendor_Code,Vendor_Name from TSPL_SRN_HEAD inner join TSPL_SRN_DETAIL on " & _
        '"TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Row_Type='Item'  and Add_Charge_Code9 <> ''  " & _
        '"and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
        '"convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=  convert(date,'" & ToDate.Value & "',103) "

        'Dim str20 As String = "select TSPL_SRN_HEAD.SRN_No,SRN_Date as DocDate,Inv_No as BillNo,Item_Code, " & _
        '"Item_Desc,SRN_Qty,Item_Cost,Amount,Vendor_Code,Vendor_Name from TSPL_SRN_HEAD inner join TSPL_SRN_DETAIL on " & _
        '"TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where Row_Type='Item'  and Add_Charge_Code10 <> ''  " & _
        '"and convert(date,TSPL_SRN_HEAD.SRN_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
        '"convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=  convert(date,'" & ToDate.Value & "',103) "

        If strAddCostAll = "N" Then
            strSql1 += " and TSPL_SRN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgAddCost.CheckedValue) + ") "
            strSql2 += " and TSPL_VENDOR_INVOICE_DETAIL.AddChargeCode in (" + clsCommon.GetMulcallString(cbgAddCost.CheckedValue) + ") "
            str1 += " and TSPL_SRN_HEAD.Add_Charge_Code1 in (" + clsCommon.GetMulcallString(cbgAddCost.CheckedValue) + ") "
            str2 += " and TSPL_SRN_HEAD.Add_Charge_Code2 in (" + clsCommon.GetMulcallString(cbgAddCost.CheckedValue) + ") "
            str3 += " and TSPL_SRN_HEAD.Add_Charge_Code3 in (" + clsCommon.GetMulcallString(cbgAddCost.CheckedValue) + ") "
            str4 += " and TSPL_SRN_HEAD.Add_Charge_Code4 in (" + clsCommon.GetMulcallString(cbgAddCost.CheckedValue) + ") "
            str5 += " and TSPL_SRN_HEAD.Add_Charge_Code5 in (" + clsCommon.GetMulcallString(cbgAddCost.CheckedValue) + ") "
            str6 += " and TSPL_SRN_HEAD.Add_Charge_Code6 in (" + clsCommon.GetMulcallString(cbgAddCost.CheckedValue) + ") "
            str7 += " and TSPL_SRN_HEAD.Add_Charge_Code7 in (" + clsCommon.GetMulcallString(cbgAddCost.CheckedValue) + ") "
            str8 += " and TSPL_SRN_HEAD.Add_Charge_Code8 in (" + clsCommon.GetMulcallString(cbgAddCost.CheckedValue) + ") "
            str9 += " and TSPL_SRN_HEAD.Add_Charge_Code9 in (" + clsCommon.GetMulcallString(cbgAddCost.CheckedValue) + ") "
            str10 += " and TSPL_SRN_HEAD.Add_Charge_Code10 in (" + clsCommon.GetMulcallString(cbgAddCost.CheckedValue) + ") "
            'strSql3 += " and TSPL_SRN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgAddCost.CheckedValue) + ") "
            'strSql4 += " and TSPL_VENDOR_INVOICE_DETAIL.AddChargeCode in (" + clsCommon.GetMulcallString(cbgAddCost.CheckedValue) + ") "
            'str11 += " and TSPL_SRN_HEAD.Add_Charge_Code1 in (" + clsCommon.GetMulcallString(cbgAddCost.CheckedValue) + ") "
        End If
        strQuery = "Select FinalQry.* from ( " & strSql1 & Un1 & strSql2 & Un1 & str1 & Un1 & str2 & Un1 & str3 & Un1 & str4 & Un1 & str5 & Un1 & str6 & Un1 & str7 & Un1 & str8 & Un1 & str9 & Un1 & str10 & ")FinalQry  order by FinalQry.SRN_Date"

        Dim strQuery2 As String = strSql3 & Un1 & strSql4 & Un1 & str11 & " order by DocDate "
        Dim frmCRV As New frmCrystalReportViewer()
        frmCRV.funsubreport(CrystalReportFolder.PurchaseOrder, strQuery, strQuery2, "crptAdditionalCharge", "Additional Charge Report", "crptAddCostDetail.rpt")
        frmCRV = Nothing
    End Sub
    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub

    Private Sub FrmAdditionalCharge1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class

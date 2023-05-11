
''''              Modified by = Priti (10/04/2012)
Imports XpertERPEngine
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
Public Class FrmReportForQuickSettlement
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode As String
    Dim sql As String

    Private preInvQty As Decimal = 0

    Dim ButtonToolTip As ToolTip = New ToolTip()


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.reportQuickSettlement)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                'dr.Read()
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
    Sub print()
        Dim strQuery, strSql1, strSql2, strSql3, Un1 As String
        strSql1 = "select TSPL_TRANSFER_HEAD.Transfer_No as Transfer_No,Transfer_Date,TSPL_TRANSFER_DETAIL.Item_Code,Item_Desc,Item_Qty/Conversion_Factor as LoadOutQty, " & _
        "(TSPL_TRANSFER_DETAIL.Item_Qty * (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.TPT_Value + TSPL_TRANSFER_DETAIL.Empty_Value ) ) as LoadOutAmt, " & _
        "0 as LoadInQty,0 as LoadInAmt,0 as EmptyIn,0 as EmtyAmt,convert(date,'" & dtpFdate.Value & "',103) as StartDate, " & _
        "convert(date,'" & DtpTodate.Value & "',103) as EndDate,(TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.TPT_Value + TSPL_TRANSFER_DETAIL.Empty_Value ) as Outrate,0 as InRate,MRP from TSPL_TRANSFER_HEAD inner join TSPL_TRANSFER_DETAIL on " & _
        "TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No inner join TSPL_ITEM_UOM_DETAIL on " & _
        "TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
        "TSPL_TRANSFER_DETAIL.Uom=TSPL_ITEM_UOM_DETAIL.UOM_Code inner join TSPL_LOCATION_MASTER on " & _
        "TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code where Transfer_Type='LO' and " & _
        "Location_Type='Logical'"
        Un1 = "Union All "
        strSql2 = "select TSPL_TRANSFER_HEAD.Load_Out_No as Transfer_No,Transfer_Date,TSPL_TRANSFER_DETAIL.Item_Code, " & _
        "Item_Desc,0 as LoadOutQty,0 as LoadOutAmt,(LoadIn_Qty/Conversion_Factor ) + Burst/Conversion_Factor + Shortage/Conversion_Factor + Leak/Conversion_Factor  as LoadInQty," & _
        " ((TSPL_TRANSFER_DETAIL.LoadIn_Qty + Burst + Shortage + Leak) * (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.TPT_Value + TSPL_TRANSFER_DETAIL.Empty_Value  ) )   as LoadInAmt, " & _
        "0 as EmptyIn,0 as EmtyAmt,convert(date,'" & dtpFdate.Value & "',103) as StartDate,convert(date,'" & DtpTodate.Value & "',103) as EndDate,0 as OutRate,(TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.TPT_Value + TSPL_TRANSFER_DETAIL.Empty_Value ) as InRate,MRP from " & _
        "TSPL_TRANSFER_HEAD inner join TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No inner join " & _
        "TSPL_ITEM_UOM_DETAIL on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
        "TSPL_TRANSFER_DETAIL.Uom=TSPL_ITEM_UOM_DETAIL.UOM_Code  inner join TSPL_LOCATION_MASTER on " & _
        "TSPL_TRANSFER_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code where Transfer_Type='LI' and " & _
        "Location_Type='Logical'"
        strSql3 = "select Document_No as Transfer_No,TSPL_ADJUSTMENT_HEADER.entrydatetime as Transfer_Date, " & _
        "TSPL_ADJUSTMENT_DETAIL.Item_Code,TSPL_ADJUSTMENT_DETAIL.Item_Description,0 as LoadOutQty, " & _
        "0 as LoadOutAmt,0 as LoadInQty,0  as LoadInAmt,TSPL_ADJUSTMENT_DETAIL.Item_Quantity as EmptyIn, " & _
        "TSPL_ADJUSTMENT_DETAIL.Item_Cost as EmtyAmt,convert(date,'" & dtpFdate.Value & "',103) as StartDate,convert(date,'" & DtpTodate.Value & "',103) as EndDate,0 as OutRate,0 as InRate,mrp " & _
        "from TSPL_ADJUSTMENT_HEADER inner join TSPL_ADJUSTMENT_DETAIL on " & _
        "TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No inner join " & _
        "TSPL_ITEM_UOM_DETAIL on TSPL_ADJUSTMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
        "TSPL_ADJUSTMENT_DETAIL.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code inner join TSPL_TRANSFER_HEAD on " & _
        "TSPL_ADJUSTMENT_HEADER.Document_No=TSPL_TRANSFER_HEAD.Transfer_No inner join TSPL_LOCATION_MASTER on " & _
        "TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code  where Reference_Document='Load Out/Transfer' and Location_Type='Logical'"

        If fndTransfer.txtValue.Text = "" Then
            strSql1 += " and  TSPL_TRANSFER_HEAD.Transfer_Date > = CONVERT(date,'" & dtpFdate.Value & "',103) and TSPL_TRANSFER_HEAD.Transfer_Date < = CONVERT(date,'" & DtpTodate.Value & "',103)"
            strSql2 += " and  TSPL_TRANSFER_HEAD.Transfer_Date > = CONVERT(date,'" & dtpFdate.Value & "',103) and TSPL_TRANSFER_HEAD.Transfer_Date < = CONVERT(date,'" & DtpTodate.Value & "',103)"
            strSql3 += " and  TSPL_ADJUSTMENT_HEADER.entrydatetime > = CONVERT(date,'" & dtpFdate.Value & "',103) and TSPL_ADJUSTMENT_HEADER.entrydatetime < = CONVERT(date,'" & DtpTodate.Value & "',103)"
        Else
            strSql1 += " and  TSPL_TRANSFER_HEAD.Transfer_No  = '" & fndTransfer.txtValue.Text & "'"
            strSql2 += " and  TSPL_TRANSFER_HEAD.Load_Out_No  = '" & fndTransfer.txtValue.Text & "'"
            strSql3 += " and  TSPL_ADJUSTMENT_HEADER.Document_No  = '" & fndTransfer.txtValue.Text & "'"
        End If

        strQuery = strSql1 & Un1 & strSql2 & Un1 & strSql3

        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptForQuickSettlement", "Report for Quick Settlement")
    End Sub
    Private Sub fndTransfer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndTransfer.Load
        fndTransfer.ConnectionString = connectSql.SqlCon()
        fndTransfer.Query = "select Transfer_No as [Transfer No],convert(date,transfer_date,103) as [Transfer Date] from TSPL_TRANSFER_HEAD  inner join TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code where Location_Type='Logical'"
        fndTransfer.ValueToSelect = "Transfer No"
        fndTransfer.ValueToSelect1 = "Transfer No"
        fndTransfer.Caption = "Transfer No"
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmReportForQuickSettlement_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")



        SetUserMgmtNew()
        dtpFdate.Value = clsCommon.GETSERVERDATE
        DtpTodate.Value = clsCommon.GETSERVERDATE
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        dtpFdate.Value = clsCommon.GETSERVERDATE
        DtpTodate.Value = clsCommon.GETSERVERDATE
        fndTransfer.txtValue.Text = ""
    End Sub

    Private Sub FrmReportForQuickSettlement_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            ' printreport()
            print()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
End Class

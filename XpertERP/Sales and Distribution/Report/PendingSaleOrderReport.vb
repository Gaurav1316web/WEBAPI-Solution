Imports common

Public Class PendingSaleOrderReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub rdbtnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnprint.Click
       
        print()
    End Sub
    Sub print()
        Dim fromdate As String = clsCommon.GetPrintDate(dtpfrmdatte.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy")
        funprint(fromdate, todate, chkDoc_select1.IsChecked, cbgdoc.CheckedValue)
    End Sub
    Sub funprint(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList)


        Dim fdate As String = clsCommon.GetPrintDate(dtpfrmdatte.Value, "yyyy/MM/dd")
        Dim tdate As String = clsCommon.GetPrintDate(dtptodate.Value, "yyyy/MM/dd")

        Dim qry As String
        qry = "select saleorderno,MAX(saleorderdate) as saleorderdate ,sum(saleorderqty)as saleorderqty,sum(saleorderamount) as saleorderamount,max(loadoutno) as loadoutno,max(loadoutdate) as loadoutdate,sum(loadoutqty) as loadoutqty,sum(loadoutamount) as  loadoutamount ,sum(saleorderqty)  -sum(loadoutqty)   as  pendingqty,SUM(saleorderamount )-SUM(loadoutamount )as pendingamount from(select  TSPL_SALES_ORDER_HEAD.Order_No as saleorderno  ,TSPL_SALES_ORDER_HEAD.Order_Date  as saleorderdate  ,TSPL_SALES_ORDER_DETAILS.Order_Qty as saleorderqty,TSPL_SALES_ORDER_DETAILS.Total_MRP_Amt  as saleorderamount,'' as loadoutno,'' as loadoutdate,0 as loadoutqty,0 as loadoutamount,1 as chk from TSPL_SALES_ORDER_DETAILS left outer join  TSPL_SALES_ORDER_HEAD  on   TSPL_SALES_ORDER_DETAILS.Order_No = TSPL_SALES_ORDER_HEAD.Order_No   where convert(varchar(11),TSPL_SALES_ORDER_HEAD.Order_Date,111) >= '" + fdate + "'  and convert(varchar(11),TSPL_SALES_ORDER_HEAD.Order_Date,111) <='" + tdate + "'"

        If chkDoc_select1.IsChecked = True AndAlso cbgdoc.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Customer")
            Return

        Else
            If txtSaleOrderNo.Value <> "" Then
                qry += " and TSPL_SALES_ORDER_HEAD.Order_No ='" + txtSaleOrderNo.Value + "'"
            End If
            If chkDoc_select1.IsChecked = True Then
                qry += "and TSPL_SALES_ORDER_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
            End If

        End If
        qry += " union all select TSPL_SHIPMENT_MASTER.Order_No  as saleorderno ,'' as saleorderdate,0 as saleorderqty,0 as saleorderamount, TSPL_SHIPMENT_MASTER.Shipment_No as loadoutno ,CONVERT(varchar(10), TSPL_SHIPMENT_MASTER.Shipment_Date,103) as loadoutdate  , TSPL_SHIPMENT_DETAILS.Shipped_Qty as loadoutqty ,TSPL_SHIPMENT_DETAILS.Total_MRP_Amt  as loadoutamount ,0 as chk  from    TSPL_SHIPMENT_DETAILS left outer join    TSPL_SHIPMENT_MASTER   on TSPL_SHIPMENT_DETAILS.Shipment_No =tSPL_SHIPMENT_MASTER.Shipment_No )  bb " & _
               " group by  saleorderno having sum(bb.chk)>0 and sum(saleorderqty-loadoutqty)<>0 "






        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "PendingSaleOrder", "Pending SaleOrder Report")

    End Sub


    Private Sub txtTransferNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSaleOrderNo._MYValidating
        Dim qry As String = "select Order_No  AS [OrderNumber] ,Order_Date as [OrderDate] from TSPL_SALES_ORDER_HEAD "
        txtSaleOrderNo.Value = clsCommon.ShowSelectForm("PendingSaleOrder", qry, "OrderNumber", "", txtSaleOrderNo.Value, "OrderDate", isButtonClicked)


    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.PendingSaleOrderReport)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub PendingSaleOrderReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            'resetForm()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
            'deletedata()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.R AndAlso btnPrint.Enabled Then
            '    'PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub PendingSaleOrderReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        chkdocAll1.IsChecked = True
        dtpfrmdatte.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        LoadCustomer()
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(rdbtnprint, "Press Alt+P for Print ")

        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "PD-SLO-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
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
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Sub LoadCustomer()
        Dim qry As String = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER "
        cbgdoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgdoc.ValueMember = "Customer Code"
        cbgdoc.DisplayMember = "Customer Code"


    End Sub

  
    Private Sub chkdocAll1_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkdocAll1.ToggleStateChanged, chkDoc_select1.ToggleStateChanged
        cbgdoc.Enabled = Not chkdocAll1.IsChecked

    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub


    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()

    End Sub
    Sub reset()
        txtSaleOrderNo.Value = ""
        dtpfrmdatte.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        chkdocAll1.IsChecked = True
    End Sub
End Class

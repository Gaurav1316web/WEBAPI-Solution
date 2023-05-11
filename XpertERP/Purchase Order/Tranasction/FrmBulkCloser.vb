''created by Richa Agarwal 01 August,2016   

Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports System.Data.SqlClient

Public Class FrmBulkCloser
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim strQry As String = Nothing
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmBulkCloser_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave_Click(btnsave, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub FrmBulkCloser_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmBulkCloser)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
    End Sub

    Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        strQry = "select xxx.Loc_Segment_Code as Code,TSPL_GL_SEGMENT_CODE.Description as Name  from" & _
         " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx" & _
        " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7'" & _
        " order by xxx.Loc_Segment_Code"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocationSelector@BC", strQry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        strQry = "select TSPL_VENDOR_MASTER.Vendor_Code as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name,TSPL_VENDOR_MASTER.Parent_Vendor_Code as [Parent Code],P1.Vendor_Name as [Parent Name]   from TSPL_VENDOR_MASTER  Left Outer Join TSPL_VENDOR_MASTER P1 on TSPL_VENDOR_MASTER.Parent_Vendor_Code =P1.Vendor_Code  where TSPL_VENDOR_MASTER.Status='N'   order by TSPL_VENDOR_MASTER.Vendor_Code"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("VendorSelector@BC", strQry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            If rdbAgainstPurchaseOrder.IsChecked Then
                If TxtPO.arrValueMember IsNot Nothing AndAlso TxtPO.arrValueMember.Count > 0 Then
                    Dim response = MsgBox("Are you sure want to close the Purchase Order", MsgBoxStyle.YesNo, "Attention")
                    If response = MsgBoxResult.Yes Then
                        ClosePurchaseOrder()
                        clsCommon.MyMessageBoxShow("Purchase Order closed successfully")
                    End If
                Else
                    TxtPO.Focus()
                    Throw New Exception("Purchase Order No not found to Close")
                End If
            Else
                If txtPurchaseIndent.arrValueMember IsNot Nothing AndAlso txtPurchaseIndent.arrValueMember.Count > 0 Then
                    Dim response = MsgBox("Are you sure want to close the Purchase Indent", MsgBoxStyle.YesNo, "Attention")
                    If response = MsgBoxResult.Yes Then
                        ClosePurchasIndent()
                        clsCommon.MyMessageBoxShow("Purchase Indent closed successfully")
                    End If
                Else
                    txtPurchaseIndent.Focus()
                    Throw New Exception("Purchase Indent No not found to Close")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub


    Sub ClosePurchaseOrder()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strClosedDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim qry As String = "Update TSPL_PURCHASE_ORDER_HEAD set close_yn='Y' ,Closed_By='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "',Closed_Date='" + strClosedDate + "' where PurchaseOrder_No in (" + clsCommon.GetMulcallString(TxtPO.arrValueMember) + ")  "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub ClosePurchasIndent()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "Update  TSPL_REQUISITION_HEAD set close_yn='Y' where Requisition_Id  in (" + clsCommon.GetMulcallString(txtPurchaseIndent.arrValueMember) + ")  "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub Reset()
        btnsave.Text = "Proceed to Close"
        btnsave.Enabled = True
        isNewEntry = True
        dtpFromdate.Value = clsCommon.GETSERVERDATE().AddMonths(-1)
        dtptodate.Value = clsCommon.GETSERVERDATE()
        TxtPO.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtVendor.arrValueMember = Nothing
        txtPurchaseIndent.arrValueMember = Nothing
    End Sub

    Private Sub rdbAgainstPurchaseOrder_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbAgainstPurchaseOrder.ToggleStateChanged
        If rdbAgainstPurchaseOrder.IsChecked Then
            TxtPO.Enabled = True
            txtPurchaseIndent.Enabled = False
            TxtPO.arrValueMember = Nothing
            txtPurchaseIndent.arrValueMember = Nothing
        Else
            TxtPO.Enabled = False
            txtPurchaseIndent.Enabled = True
            TxtPO.arrValueMember = Nothing
            txtPurchaseIndent.arrValueMember = Nothing
        End If
    End Sub

    Private Sub TxtPO__My_Click(sender As Object, e As EventArgs) Handles TxtPO._My_Click
        Try
            If clsCommon.myCDate(dtpFromdate.Value) > clsCommon.myCDate(dtptodate.Value) Then
                Throw New Exception("From date cannot be greater than To date")
            End If
            strQry = " Select TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as [PO No] ,convert (varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) as Date ,TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location as Location,TSPL_LOCATION_MASTER.Location_Desc as [Location Name] ,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as [Vendor Code],TSPL_PURCHASE_ORDER_HEAD.Vendor_Name as [Vendor Name] ,TSPL_PURCHASE_ORDER_HEAD.PO_Total_Amt as [Document Amount] ,Case when TSPL_PURCHASE_ORDER_HEAD.Status=1 then 'Approved' else 'Pending' end as Status from TSPL_PURCHASE_ORDER_HEAD Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location  " &
            "WHERE TSPL_PURCHASE_ORDER_HEAD.close_yn ='N' AND  CONVERT(DATE,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) >= '" & clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MMM/yyyy") & "' AND CONVERT(DATE,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) <= '" & clsCommon.GetPrintDate(dtptodate.Value, "dd/MMM/yyyy") & "' "
            'Asked by Shruti mam, TSPL_PURCHASE_ORDER_HEAD.Status =1 and
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                strQry += " and TSPL_PURCHASE_ORDER_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")  "
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strQry += " and TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
            End If
            TxtPO.arrValueMember = clsCommon.ShowMultipleSelectForm("POSelector@BC", strQry, "PO No", "PO No", TxtPO.arrValueMember, TxtPO.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        
    End Sub

    Private Sub txtPurchaseIndent__My_Click(sender As Object, e As EventArgs) Handles txtPurchaseIndent._My_Click
        Try
            If clsCommon.myCDate(dtpFromdate.Value) > clsCommon.myCDate(dtptodate.Value) Then
                Throw New Exception("From date cannot be greater than To date")
            End If

            strQry = " Select TSPL_REQUISITION_HEAD.Requisition_Id  as [PI No] ,convert (varchar,TSPL_REQUISITION_HEAD.Requisition_Date,103) as Date ,TSPL_REQUISITION_HEAD.Location as Location,TSPL_LOCATION_MASTER.Location_Desc as [Location Name] ,TSPL_REQUISITION_HEAD.Total_RQ_Amt as [Document Amount] ,Case when TSPL_REQUISITION_HEAD.Status=1 then 'Approved' else 'Pending' end as Status from TSPL_REQUISITION_HEAD Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_REQUISITION_HEAD.Location " & _
            " where TSPL_REQUISITION_HEAD.Status =1 and TSPL_REQUISITION_HEAD.close_yn ='N' AND  CONVERT(DATE,TSPL_REQUISITION_HEAD.Requisition_Date,103) >= '" & clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MMM/yyyy") & "' AND CONVERT(DATE,TSPL_REQUISITION_HEAD.Requisition_Date,103) <= '" & clsCommon.GetPrintDate(dtptodate.Value, "dd/MMM/yyyy") & "' "
           
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strQry += " and TSPL_REQUISITION_HEAD.Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
            End If
            txtPurchaseIndent.arrValueMember = clsCommon.ShowMultipleSelectForm("PISelector@BC", strQry, "PI No", "PI No", txtPurchaseIndent.arrValueMember, txtPurchaseIndent.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub
End Class
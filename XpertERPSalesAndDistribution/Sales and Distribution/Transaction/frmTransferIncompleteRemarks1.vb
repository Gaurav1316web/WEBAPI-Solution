Imports System
Imports System.Data.SqlClient
Imports common
Imports XpertERPEngine

'DEVELOPED by Abhishek kumar
'Created date - 22/june/2012
' End Date = 22/June/2012

Public Class FrmTransferIncompleteRemarks1
    Inherits FrmMainTranScreen
    Dim dt As New DataTable()
    Dim obj As New ClsTransferIncompleteRemarks
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim QRY As String
    Dim userCode, companyCode As String
    Private isInsideLoadData As Boolean = False
    Public Sub SetLength()
        fndTransferNo.MyMaxLength = 12
        txtRouteNo.MaxLength = 12
        txtSalesManCode.MaxLength = 12
        txtRouteDesc.MaxLength = 150
        txtSalesmanDesc.MaxLength = 50
    End Sub
    Private Sub FrmTransferIncompleteRemarks1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        SetLength()
        blankAllControl()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N Adding New Trasnaction")
        'ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")



    End Sub

   
    Public Sub blankAllControl()
        fndTransferNo.Value = Nothing
        dtptransferDate.Value = clsCommon.GETSERVERDATE()
        txtRouteNo.Text = ""
        isNewEntry = True
        txtRouteDesc.Text = ""
        txtSalesManCode.Text = ""
        txtSalesmanDesc.Text = ""
        rtftxtQuickRemarks.Rtf = ""
        rtftxtInvoiceremarks.Rtf = ""
        txtRouteNo.ReadOnly = True
        txtRouteDesc.ReadOnly = True
        txtSalesManCode.ReadOnly = True
        txtSalesmanDesc.ReadOnly = True
        dtptransferDate.Enabled = False


    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmTransferIncompleteRemarks1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(fndTransferNo.Value) = 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Transfer No ")
            rtftxtQuickRemarks.Focus()
        ElseIf clsCommon.myLen(rtftxtQuickRemarks.Text) = 0 Then
            common.clsCommon.MyMessageBoxShow("Please Enter QuickSettlement Remakrs ")
            rtftxtQuickRemarks.Focus()
            Return False
        ElseIf clsCommon.myLen(rtftxtInvoiceremarks.Text) = 0 Then
            common.clsCommon.MyMessageBoxShow("Please Enter Invoice Remakrs ")
            rtftxtInvoiceremarks.Focus()
            Return False
        End If
        Return True
    End Function

    Sub SaveData()

        Try
            If (AllowToSave()) Then
                Dim obj As New ClsTransferIncompleteRemarks()
                obj.TransferNo = clsCommon.myCstr(fndTransferNo.Value)
                obj.QuickSettlementRemarks = clsCommon.myCstr(rtftxtQuickRemarks.Text)
                obj.Invoiceremarks = clsCommon.myCstr(rtftxtInvoiceremarks.Text)

                If (obj.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.TransferNo, NavigatorType.Current)
                    btndelete.Enabled = True

                Else

                End If
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal Code As String, ByVal navType As common.NavigatorType)
        Try
            btnSave.Enabled = True
            btndelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            ' btnSave.Text = "Update"
            fndTransferNo.MyReadOnly = True
            Dim obj As New ClsTransferIncompleteRemarks()
            obj = ClsTransferIncompleteRemarks.GetData(Code, navType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.TransferNo) > 0) Then
                fndTransferNo.Value = obj.TransferNo
                dtptransferDate.Value = clsCommon.myCDate(obj.transferDate, "dd/MM/yyyy")
                txtRouteNo.Text = obj.routeNo
                txtRouteDesc.Text = obj.routdesc
                txtSalesManCode.Text = obj.saleCode
                txtSalesmanDesc.Text = obj.saledesc
                rtftxtQuickRemarks.Text = obj.QuickSettlementRemarks
                rtftxtInvoiceremarks.Text = obj.Invoiceremarks
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub DeleteData()
        Try
            If clsCommon.myLen(fndTransferNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
                Exit Sub
            End If
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (ClsTransferIncompleteRemarks.DeleteData(fndTransferNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully")
                    blankAllControl()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.fndTransferNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub fndTransferNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndTransferNo._MYNavigator
        Try
            LoadData(fndTransferNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndTransferNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTransferNo._MYValidating
        If fndTransferNo.MyReadOnly Or isButtonClicked Then
            Dim qry As String = "select Transfer_No as Code,Transfer_Date as TransferDate   from TSPL_TRANSFER_HEAD "
            Dim whr As String = "Sale_Invoice_Completed  =0 or Quick_Settlement ='N'and Post='Y'"
            fndTransferNo.Value = clsCommon.ShowSelectForm("TransferNofnd", qry, "Code", whr, fndTransferNo.Value, "Code", isButtonClicked)
            Dim dt As DataTable = getrouteNo(fndTransferNo.Value)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                dtptransferDate.Value = clsCommon.myCDate(dt.Rows(0)("Transfer_Date"), "dd/MM/yyyy")
                txtRouteNo.Text = clsCommon.myCstr(dt.Rows(0)("Route_No"))
                txtRouteDesc.Text = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
                txtSalesManCode.Text = clsCommon.myCstr(dt.Rows(0)("Salesmancode"))
                txtSalesmanDesc.Text = clsCommon.myCstr(dt.Rows(0)("SalesManDesc"))
                Dim obj As New ClsTransferIncompleteRemarks()
                obj = ClsTransferIncompleteRemarks.GetData(fndTransferNo.Value, NavigatorType.Current)
                If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.TransferNo) > 0) Then
                    rtftxtQuickRemarks.Text = obj.QuickSettlementRemarks
                    rtftxtInvoiceremarks.Text = obj.Invoiceremarks
                    isNewEntry = False
                End If
                btndelete.Enabled = True
            End If
        End If
    End Sub

    Public Shared Function getrouteNo(ByVal transferNo As String) As DataTable
        Dim qry As String = "SELECT  TSPL_TRANSFER_HEAD .Transfer_Date ,TSPL_TRANSFER_HEAD .Route_No ,TSPL_TRANSFER_HEAD .Route_Desc ,TSPL_TRANSFER_HEAD .Salesmancode , (select Emp_Name  from TSPL_EMPLOYEE_MASTER  where TSPL_EMPLOYEE_MASTER .EMP_CODE =TSPL_TRANSFER_HEAD .Salesmancode  and Emp_type ='Salesman'   )as SalesManDesc from TSPL_TRANSFER_HEAD  where Transfer_No ='" & transferNo & "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt1

    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        blankAllControl()
    End Sub

 
    Private Sub FrmTransferIncompleteRemarks1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            blankAllControl()
            'Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
End Class

'--25/08/2012--Updation By--[Pankaj Kumar]---Applied GL Security For Document Finder------Fwd By-Ranjana Mam
'-------- 04/11/2012 07:45 PM
'-by vipin for check post status on update on 04/02/2013
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Threading
Imports common
Public Class FrmSettlementEntry
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public clicked As Boolean = False

#Region "Variables"
    Const colLineNo As String = "COLLNO"
    Const colSettlementCode As String = "colSettlementCode"
    Const colSettlementName As String = "COLTYPE"
    Const colAmount As String = "colAmount"
    Const colRemarks As String = "colRemarks"
    Const colGLAccount As String = "colGLAccount"
    Const colGLAccDesc As String = "colGLAccDesc"

#End Region

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)


        Dim repoSettlementCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSettlementCode.FormatString = ""
        repoSettlementCode.HeaderText = "Settlement Code"
        repoSettlementCode.Name = colSettlementCode
        repoSettlementCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoSettlementCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoSettlementCode.Width = 150
        gv1.MasterTemplate.Columns.Add(repoSettlementCode)

        Dim repoSettlementname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSettlementname.FormatString = ""
        repoSettlementname.HeaderText = "Name"
        repoSettlementname.Name = colSettlementName
        'repoSettlementname.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoSettlementname.TextImageRelation = TextImageRelation.TextBeforeImage
        repoSettlementname.Width = 200
        gv1.MasterTemplate.Columns.Add(repoSettlementname)

        Dim repoAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmount = New GridViewDecimalColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = colAmount
        repoAmount.IsVisible = True
        'repoAmount.Minimum = 0
        repoAmount.Width = 90
        repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmount.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoAmount)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        'repoRemarks.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoRemarks.TextImageRelation = TextImageRelation.TextBeforeImage
        repoRemarks.Width = 250
        gv1.MasterTemplate.Columns.Add(repoRemarks)


        Dim repoGLAcc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGLAcc.FormatString = ""
        repoGLAcc.HeaderText = "GL Account"
        repoGLAcc.Name = colGLAccount
        repoGLAcc.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoGLAcc.TextImageRelation = TextImageRelation.TextBeforeImage
        repoGLAcc.Width = 100
        repoGLAcc.IsVisible = False
        repoGLAcc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoGLAcc)


        Dim repoGLAccDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGLAccDesc.FormatString = ""
        repoGLAccDesc.HeaderText = "GL Account Name"
        repoGLAccDesc.Name = colGLAccDesc
        ' repoGLAccDesc.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoGLAccDesc.TextImageRelation = TextImageRelation.TextBeforeImage
        repoGLAccDesc.Width = 100
        repoGLAccDesc.IsVisible = False
        repoGLAccDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoGLAccDesc)


        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = True
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(clicked)
    End Sub
    Private Function funUpdate() As Boolean
        Dim netAmount As Decimal = 0
        Dim SettlementAmt As Decimal = 0
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strMsqQuery As String = " select count(Transfer_No)  from tspl_transfer_head where Salesmancode ='" + fndSalesMan.Value + "' and Transfer_Type ='LO'  and Item_Type ='Full' and Post='Y' and To_Location in (select Location_Code  from TSPL_LOCATION_MASTER where Location_Type ='Logical') and Transfer_No not in (select distinct LoadOutNo   from TSPL_PAYMENT_HEADER where  LoadOutNo is not null ) and Posting_Date = convert(date,'" + dtSettlement.Value + "',103)"
            Dim Count As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strMsqQuery, trans))
            Count = Count
            If Count > 0 Then
                common.clsCommon.MyMessageBoxShow("" + Count.ToString() + " LoadOut's are Pending For " + txtSalesManName.Text + " For Dated:" + dtSettlement.Value + "", "Settlement Entry", MessageBoxButtons.OK, RadMessageIcon.Info)
            End If
            Dim SettlementDate As String = Format(dtSettlement.Value, "dd/MM/yyyy")
            Dim strbankacct As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + clsFixedParameter.GetData(clsFixedParameterType.LOReceiptDefaultBankForSettlement, clsFixedParameterCode.LOReceiptDefaultBankForSettlement, trans) + "'", trans))
            If fndLocation.Value IsNot Nothing Or fndLocation.Value <> "" Then
                strbankacct = clsERPFuncationality.ChangeGLAccountLocationSegment(strbankacct, fndLocation.Value, False, trans)
            End If
            'Dim Bankname As String
            Dim Bank As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANK_CODE  from TSPL_BANK_MASTER  where BANKACC ='" + strbankacct + "'", trans))
            If Bank Is Nothing Or Bank = "" Then
                Throw New Exception("Bank Not Found For Location " + fndLocation.Value + "")
            End If
            Dim PaymentType As String = clsFixedParameter.GetData(clsFixedParameterType.LOReceiptPaymentTypeForSettlement, clsFixedParameterCode.LOReceiptPaymentTypeForSettlement, trans)
            clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_PAYMENT_HEADER_UPDATE", New SqlParameter("@paymentno", Me.fndSettlementEntry.Value), New SqlParameter("@debitacct", ""), New SqlParameter("@creditacct", ""), New SqlParameter("@paymentdate", SettlementDate), New SqlParameter("@paymentpostdate", SettlementDate), New SqlParameter("@bankcode", Bank), New SqlParameter("@paymenttype", "MI"), New SqlParameter("@vendorcode", ""), New SqlParameter("@vendorname", ""), New SqlParameter("@remitto", "Settlement For LoadOut No.:" + fndloadno.Value + " of SalesMan " + fndSalesMan.Value + ". "), New SqlParameter("@entrydesc", Me.txtDesc.Text), New SqlParameter("@reference", ""), New SqlParameter("@narration", ""), New SqlParameter("@paymentcode", PaymentType), New SqlParameter("@chequeno", ""), New SqlParameter("@chequedate", SettlementDate), New SqlParameter("@paymentamount", Me.txtSettlementAmt.Text), New SqlParameter("@vendoraccountset", ""), New SqlParameter("@applyby", ""), New SqlParameter("@applyto", ""), New SqlParameter("@post", "N"), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", objCommonVar.CurrentUserCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode()))
            clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_PAYMENT_DETAIL_DELETE", New SqlParameter("@paymentno", Me.fndSettlementEntry.Value))
            connectSql.RunSqlTransaction(trans, "update TSPL_PAYMENT_HEADER set loadoutno='" + fndloadno.Value + "' , salesman_code='" + fndSalesMan.Value + "', salesman_name='" + txtSalesManName.Text + "' , route_NO='" + fndRouteNo.Value + "' , route_description='" + txtRouteDesc.Text + "',location_code='" + fndLocation.Value + "' , location_description='" + txtLocationDesc.Text + "' where Payment_No ='" + fndSettlementEntry.Value + "'")
            For i As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(i).Cells(colSettlementCode).Value) > 0 Then
                    Dim Acc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(gv1.Rows(i).Cells(colGLAccount).Value, fndLocation.Value, False, trans)
                    Dim AccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, "select Description  from TSPL_GL_ACCOUNTS where Account_Code ='" + Acc + "'"))
                    Dim SettlementType As String = clsDBFuncationality.getSingleValue("SELECT Calculate FROM dbo.tspl_SettleMent_Master WHERE SettleMentCode='" + gv1.Rows(i).Cells(colSettlementCode).Value + "'", trans)
                    SettlementAmt = 0
                    If SettlementType = "S" Then
                        SettlementAmt = gv1.Rows(i).Cells(colAmount).Value
                    Else
                        SettlementAmt = gv1.Rows(i).Cells(colAmount).Value * -1
                    End If

                    clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_PAYMENT_DETAIL_INSERT", New SqlParameter("@paymentno", Me.fndSettlementEntry.Value), New SqlParameter("@paymentline", gv1.Rows(i).Cells(colLineNo).Value), New SqlParameter("@apply", ""), New SqlParameter("@vendorinvoiceno", ""), New SqlParameter("@paymenttype", "MI"), New SqlParameter("@documentno", ""), New SqlParameter("@pendingbalance", 0), New SqlParameter("@appliedamount", 0), New SqlParameter("@originalamount", 0), New SqlParameter("@tdsamoount", 0), New SqlParameter("@netbalance", SettlementAmt), New SqlParameter("@accountcode", Acc), New SqlParameter("@description", AccDesc), New SqlParameter("@remark", gv1.Rows(i).Cells(colRemarks).Value), New SqlParameter("@comment", ""))
                    Dim Q As String = "update TSPL_PAYMENT_DETAIL set settlement_code='" + gv1.Rows(i).Cells(colSettlementCode).Value + "' , settlement_description='" + gv1.Rows(i).Cells(colSettlementName).Value + "' where Payment_No ='" + Me.fndSettlementEntry.Value + "' and Payment_Line_No ='" + gv1.Rows(i).Cells(colLineNo).Value.ToString() + "' and Account_Code ='" + Acc + "'"
                    connectSql.RunSqlTransaction(trans, Q)
                    netAmount = netAmount + SettlementAmt
                End If
            Next
            connectSql.RunSqlTransaction(trans, "UPDATE dbo.TSPL_PAYMENT_HEADER SET Payment_Amount=" + clsCommon.myCstr(netAmount) + " WHERE Payment_No='" + clsCommon.myCstr(Me.fndSettlementEntry.Value) + "'")
            trans.Commit()
            If clicked = False Then
                myMessages.update()
            End If

            Return True
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Private Function fundelete() As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            connectSql.RunSpTransaction(trans, "SP_TSPL_PAYMENT_DETAIL_DELETE", New SqlParameter("@paymentno", Me.fndSettlementEntry.Value))
            connectSql.RunSpTransaction(trans, "SP_TSPL_PAYMENT_HEADER_DELETE", New SqlParameter("@paymentno", Me.fndSettlementEntry.Value))
            trans.Commit()
            myMessages.delete()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function
    Private Sub SaveData(ByVal clicked As Boolean)
        If fndloadno.Value <> "" Then
            'If clsCommon.myCdbl(txtSettlementAmt.Text) > 0 Then
            If btnSave.Text = "Save" Then
                If funInsert() Then
                    btnSave.Text = "Update"
                    FillData()
                End If
            ElseIf btnSave.Text = "Update" Then


                Dim strchk As String = "select Posted from TSPL_PAYMENT_HEADER where Payment_No='" + fndSettlementEntry.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "P" Then
                    clsCommon.MyMessageBoxShow("Transection already posted")
                    Exit Sub
                End If



                If funUpdate() Then
                    FillData()
                End If
            End If
            'Else
            '    common.clsCommon.MyMessageBoxShow("Settlement Amount Should Be More than Zero!", "Settlement Entry", MessageBoxButtons.OK, RadMessageIcon.Info)
            'End If
        Else
            common.clsCommon.MyMessageBoxShow("Select LoadOut No!", "Settlement Entry", MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub
    Private Function funInsert() As Boolean
        Dim netAmount As Decimal = 0
        Dim SettlementAmt As Decimal = 0
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strMsqQuery As String = " select count(Transfer_No)  from tspl_transfer_head where Salesmancode ='" + fndSalesMan.Value + "' and Transfer_Type ='LO'  and Item_Type ='Full' and Post='Y' and To_Location in (select Location_Code  from TSPL_LOCATION_MASTER where Location_Type ='Logical') and Transfer_No not in (select distinct LoadOutNo   from TSPL_PAYMENT_HEADER where  LoadOutNo is not null ) and Posting_Date = convert(date,'" + dtSettlement.Value + "',103)"
            Dim Count As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strMsqQuery, trans))
            Count = Count - 1
            If Count > 0 Then
                common.clsCommon.MyMessageBoxShow("" + Count.ToString() + " LoadOut's are Pending For " + txtSalesManName.Text + " For Dated:" + dtSettlement.Value + "", "Settlement Entry", MessageBoxButtons.OK, RadMessageIcon.Info)
            End If
            Dim SettlementDate As String = Format(dtSettlement.Value, "dd/MM/yyyy")
            Dim qry As String = "select Bank_type,BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + clsFixedParameter.GetData(clsFixedParameterType.LOReceiptDefaultBankForSettlement, clsFixedParameterCode.LOReceiptDefaultBankForSettlement, trans) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            '    Throw New Exception("Bank: SETTLEMENT Not found")
            'End If
            Dim strDocumentNo As String = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
            strDocumentNo = clsERPFuncationality.ChangeGLAccountLocationSegment(strDocumentNo, fndLocation.Value, False, trans)
            If (strDocumentNo.Length >= 3) Then
                strDocumentNo = strDocumentNo.Substring(strDocumentNo.Length - 3, 3)
                If (IsNumeric(strDocumentNo)) Then
                    Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                End If
            Else
                Throw New Exception("Bank Master's Bank Account should be have location segment Type")
            End If
            Dim strBankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))
            Dim STRPAYmentno As String = ""
            If clsCommon.CompairString(strBankType, "B") = CompairStringResult.Equal Then
                STRPAYmentno = clsERPFuncationality.GetNextCode(trans, dtSettlement.Value, clsDocType.Payment, clsDocTransactionType.Bank, strDocumentNo, True)
            ElseIf clsCommon.CompairString(strBankType, "C") = CompairStringResult.Equal Then
                STRPAYmentno = clsERPFuncationality.GetNextCode(trans, dtSettlement.Value, clsDocType.Payment, clsDocTransactionType.Cash, strDocumentNo, True)
            ElseIf clsCommon.CompairString(strBankType, "P") = CompairStringResult.Equal Then
                STRPAYmentno = clsERPFuncationality.GetNextCode(trans, dtSettlement.Value, clsDocType.Payment, clsDocTransactionType.PettyCash, strDocumentNo, True)
            ElseIf clsCommon.CompairString(strBankType, "O") = CompairStringResult.Equal Then
                STRPAYmentno = clsERPFuncationality.GetNextCode(trans, dtSettlement.Value, clsDocType.Payment, clsDocTransactionType.Others, strDocumentNo, True)
            ElseIf clsCommon.CompairString(strBankType, "S") = CompairStringResult.Equal Then
                STRPAYmentno = clsERPFuncationality.GetNextCode(trans, dtSettlement.Value, clsDocType.Payment, clsDocTransactionType.Others, strDocumentNo, True)
            Else
                Throw New Exception("Plase set the Bank Type for Bank SETTLEMENT")
            End If
            If clsCommon.myLen(STRPAYmentno) <= 0 Then
                Throw New Exception("Error in Genenrating Settlement No.")
            End If
            Dim strbankacct As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='SETTLEMENT'", trans))
            If fndLocation.Value IsNot Nothing Or fndLocation.Value <> "" Then
                strbankacct = clsERPFuncationality.ChangeGLAccountLocationSegment(strbankacct, fndLocation.Value, False, trans)
            End If
            ' Dim Bankname As String
            Dim Bank As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANK_CODE  from TSPL_BANK_MASTER  where BANKACC ='" + strbankacct + "'", trans))
            If Bank Is Nothing Or Bank = "" Then
                Throw New Exception("Bank Not Found For Location " + fndLocation.Value + "")
            End If
            Dim PaymentType As String = clsFixedParameter.GetData(clsFixedParameterType.LOReceiptPaymentTypeForSettlement, clsFixedParameterCode.LOReceiptPaymentTypeForSettlement, trans)
            clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_PAYMENT_HEADER_INSERT", New SqlParameter("@paymentno", STRPAYmentno), New SqlParameter("@debitacct", ""), New SqlParameter("@creditacct", ""), New SqlParameter("@paymentdate", SettlementDate), New SqlParameter("@paymentpostdate", SettlementDate), New SqlParameter("@bankcode", Bank), New SqlParameter("@paymenttype", "MI"), New SqlParameter("@vendorcode", ""), New SqlParameter("@vendorname", ""), New SqlParameter("@remitto", "" + fndloadno.Value + " - LoadOut No of Settlement and Sales Man " + fndSalesMan.Value + ". "), New SqlParameter("@entrydesc", Me.txtDesc.Text), New SqlParameter("@reference", ""), New SqlParameter("@narration", ""), New SqlParameter("@paymentcode", PaymentType), New SqlParameter("@chequeno", ""), New SqlParameter("@chequedate", SettlementDate), New SqlParameter("@paymentamount", Me.txtSettlementAmt.Text), New SqlParameter("@vendoraccountset", ""), New SqlParameter("@applyby", ""), New SqlParameter("@applyto", ""), New SqlParameter("@post", "N"), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", objCommonVar.CurrentUserCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
            connectSql.RunSqlTransaction(trans, "update TSPL_PAYMENT_HEADER set loadoutno='" + fndloadno.Value + "' , salesman_code='" + fndSalesMan.Value + "', salesman_name='" + txtSalesManName.Text + "' , route_NO='" + fndRouteNo.Value + "' , route_description='" + txtRouteDesc.Text + "',location_code='" + fndLocation.Value + "' , location_description='" + txtLocationDesc.Text + "' where Payment_No ='" + STRPAYmentno + "'")
            For i As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(i).Cells(colSettlementCode).Value) > 0 Then
                    Dim Acc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(gv1.Rows(i).Cells(colGLAccount).Value, fndLocation.Value, False, trans)
                    Dim AccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, "select Description  from TSPL_GL_ACCOUNTS where Account_Code ='" + Acc + "'"))
                    Dim SettlementType As String = clsDBFuncationality.getSingleValue("SELECT Calculate FROM dbo.tspl_SettleMent_Master WHERE SettleMentCode='" + gv1.Rows(i).Cells(colSettlementCode).Value + "'", trans)
                    SettlementAmt = 0
                    If SettlementType = "S" Then
                        SettlementAmt = gv1.Rows(i).Cells(colAmount).Value
                    Else
                        SettlementAmt = gv1.Rows(i).Cells(colAmount).Value * -1
                    End If
                    clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_PAYMENT_DETAIL_INSERT", New SqlParameter("@paymentno", STRPAYmentno), New SqlParameter("@paymentline", gv1.Rows(i).Cells(colLineNo).Value), New SqlParameter("@apply", ""), New SqlParameter("@vendorinvoiceno", ""), New SqlParameter("@paymenttype", "MI"), New SqlParameter("@documentno", ""), New SqlParameter("@pendingbalance", 0), New SqlParameter("@appliedamount", 0), New SqlParameter("@originalamount", 0), New SqlParameter("@tdsamoount", 0), New SqlParameter("@netbalance", SettlementAmt), New SqlParameter("@accountcode", Acc), New SqlParameter("@description", AccDesc), New SqlParameter("@remark", gv1.Rows(i).Cells(colRemarks).Value), New SqlParameter("@comment", ""))
                    Dim Q As String = "update TSPL_PAYMENT_DETAIL set settlement_code='" + gv1.Rows(i).Cells(colSettlementCode).Value + "' , settlement_description='" + gv1.Rows(i).Cells(colSettlementName).Value + "' where Payment_No ='" + STRPAYmentno + "' and Payment_Line_No ='" + gv1.Rows(i).Cells(colLineNo).Value.ToString() + "' and Account_Code ='" + Acc + "'"
                    connectSql.RunSqlTransaction(trans, Q)
                    netAmount = netAmount + SettlementAmt
                End If

            Next
            connectSql.RunSqlTransaction(trans, "UPDATE dbo.TSPL_PAYMENT_HEADER SET Payment_Amount=" + clsCommon.myCstr(netAmount) + " WHERE Payment_No='" + clsCommon.myCstr(STRPAYmentno) + "'")
            trans.Commit()
            If clicked = False Then
                myMessages.insert()
            End If

            fndSettlementEntry.Value = STRPAYmentno
            Return True
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        clicked = True
        PostDataNew()
    End Sub
    Private Sub setGridFocus()
        Try
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Delete()
    End Sub
    Private Sub Delete()
        Dim Reason As String = ""
        If myMessages.deleteConfirm Then
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
            If fundelete() Then
                saveCancelLog(Reason, "Delete", Nothing)
            End If
            AddNew()
        End If
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.fndSettlementEntry.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
    Private Sub PostDataNew()
        Try

            If myMessages.postConfirm Then
                SaveData(clicked)
                If clsPayment.PostData(fndSettlementEntry.Value) Then

                    AlterQuickSettlement()
                    myMessages.post()
                    FillData()
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        clicked = False
    End Sub
    Private Sub AlterQuickSettlement()
        Dim SettlementCode As String = Nothing
        Dim SettlementDesc As String = Nothing
        Dim Amount As Decimal = 0
        Dim QuickSettlementId As String = Nothing
        Dim UpDateOrInsertFlag As Integer = 0
        Dim Remarks As String = Nothing
        For Each row As GridViewRowInfo In gv1.Rows
            UpDateOrInsertFlag = 0
            SettlementCode = clsCommon.myCstr(row.Cells(colSettlementCode).Value)
            SettlementDesc = clsCommon.myCstr(row.Cells(colSettlementName).Value)
            Amount = clsCommon.myCdbl(row.Cells(colAmount).Value)
            If Amount < 0 Then
                Amount = Amount * -1
            End If
            Remarks = clsCommon.myCstr(row.Cells(colRemarks).Value)
            QuickSettlementId = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select quick_settlement_id from tspl_quicksettlement where transfer_number='" + fndloadno.Value + "'"))
            UpDateOrInsertFlag = clsDBFuncationality.getSingleValue("select 1 from tspl_quicksettlement_detail where settlement_code ='" + SettlementCode + "' and Quick_settlement_id='" + QuickSettlementId + "'")
            If UpDateOrInsertFlag = 1 Then
                clsDBFuncationality.ExecuteNonQuery("update tspl_quicksettlement_detail set amount=amount+" + clsCommon.myCstr(Amount) + " ,remarks='" + Remarks + "' where  quick_settlement_id='" + QuickSettlementId + "' and settlement_code='" + SettlementCode + "'")
            Else
                clsDBFuncationality.ExecuteNonQuery("insert into tspl_quicksettlement_detail (Quick_settlement_id,settlement_code,description,amount,remarks)values('" + QuickSettlementId + "','" + SettlementCode + "','" + SettlementDesc + "'," + clsCommon.myCstr(Amount) + ",'" + Remarks + "')")
            End If
        Next
        clsDBFuncationality.ExecuteNonQuery("update tspl_quicksettlement set comments=comments+' :" + txtDesc.Text + "'+' :Settlement Done By " + objCommonVar.CurrentUserCode + " On Date " + clsCommon.GETSERVERDATE() + "' where quick_settlement_id='" + QuickSettlementId + "'")
    End Sub
#Region "Print Report"

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub

    Private Sub PrintData()
        If clsCommon.myLen(fndSettlementEntry.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Document no to Print", Me.Text)
            Return
        End If
        Dim arr As New ArrayList()
        arr.Add(fndSettlementEntry.Value)
        FrmSettlementEntry.funReport("", "", arr, Nothing)
    End Sub

    Public Shared Sub funReport(ByVal fromDate As String, ByVal toDate As String, ByVal ArrDocument As ArrayList, ByVal ArrVendor As ArrayList)
        Try
            Dim dt1 As DataTable
            'Dim qryParticual = "select max(EntryDesc) from("
            '' qryParticual += "select TSPL_PAYMENT_DETAIL.Vendor_Invoice_No+ case when len(RTRIM(TSPL_PAYMENT_DETAIL.Vendor_Invoice_No))>0 then ', ' else ' ' end +Vendor_Invoice_Date  as EntryDesc from tspl_payment_header join TSPL_PAYMENT_DETAIL on tspl_payment_header.Payment_No =TSPL_PAYMENT_DETAIL.Payment_No  join TSPL_VENDOR_INVOICE_HEAD  on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_PAYMENT_DETAIL.Document_No where tspl_payment_header.Payment_No = Final.Payment_No"
            'qryParticual += "select TSPL_PAYMENT_DETAIL.Vendor_Invoice_No+ case when len(RTRIM(TSPL_PAYMENT_DETAIL.Vendor_Invoice_No))>0 then ', ' else ' ' end +Vendor_Invoice_Date  as EntryDesc from tspl_payment_header join TSPL_PAYMENT_DETAIL on tspl_payment_header.Payment_No =TSPL_PAYMENT_DETAIL.Payment_No  join TSPL_VENDOR_INVOICE_HEAD  on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_PAYMENT_DETAIL.Document_No where tspl_payment_header.Payment_No in (" + clsCommon.GetMulcallString(ArrDocument) + ")"
            'qryParticual += ") xxx"

            Dim qry As String = "select Final.Payment_No as PaymentNo,Payment_Date as PaymentDate,case when Vendor_Name =Remit_To then Vendor_Name else case when len(isnull(Vendor_Name,''))>0 and len(isnull(Remit_To,''))>0 then Vendor_Name+' / '+Remit_To else Vendor_Name+''+ Remit_To   end end  as VendorNameRemitTO,Cheque_No ,CASE WHEN Payment_Code ='Check' then 'Bank Payment Voucher' else CASE WHEN Payment_Code ='Cash' then 'Cash Payment Voucher'  ELSE CASE WHEN tspl_payment_header.Payment_Code=(select Bank_Code  from TSPL_PAYMENT_HEADER WHERE tspl_payment_header.Payment_No in (" + clsCommon.GetMulcallString(ArrDocument) + ") )THEN  (select Bank_Code  from TSPL_PAYMENT_HEADER WHERE Payment_No in ( " + clsCommon.GetMulcallString(ArrDocument) + " ))+' Voucher'" & _
  " END END end  as [VoucherType], tspl_payment_header.Payment_Code,tspl_payment_header.BANK_CODE,TSPL_BANK_MASTER.DESCRIPTION as BankName, (select BANKACC from TSPL_BANK_MASTER where Bank_Code =tspl_payment_header.BANK_CODE) as Bank_acct,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE) from tspl_location_master where loc_segment_code =(substring (TSPL_BANK_MASTER .BANKACC ,LEN(TSPL_BANK_MASTER .BANKACC)-2,3))and TSPL_LOCATION_MASTER .Location_Type ='Physical')as Compaddress1, TSPL_USER_MASTER.User_Name as CreatedBy,Case when tspl_payment_header.Posted='P' then tspl_payment_header.Modify_By when tspl_payment_header.Posted='N' then null end as AuthorisedBy,TSPL_COMPANY_MASTER.Comp_Name as CompanyCode,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2, TSPL_PAYMENT_HEADER.Payment_Type , final.ACCode as AccountCode,final.Description as AccountName,Final .Entry_Desc,final.Remarks,final.Comment ,Final.DrAmt as Debit,final.CrAmt as Credit ,(select Description from TSPL_GL_SEGMENT_CODE where  Seg_No='7' and Segment_code=substring (final.ACCode,LEN(final.ACCode)-2,3)) as SegName,Final .saleinvoiceno ,Final .salesmancode ,Final .salesmanname,final.txtno,final.EntryDesc, Final.Settlement_code, Final.Settlement_Description, Final.LoadOutNo, Final.Salesman_Name from ( "
            qry += " select tspl_payment_header.Payment_No, TSPL_BANK_MASTER.BANKACC as ACCode,TSPL_GL_ACCOUNTS.Description,tspl_payment_header.Entry_Desc,'' as Remarks,'' as Comment,cASE WHEN Payment_Amount+isnull(TDS_Amount,0)<= 0 THEN  -1* (Payment_Amount+isnull(TDS_Amount,0)) END as DrAmt,"
            qry += " CASE WHEN Payment_Amount+isnull(TDS_Amount,0)> 0 THEN Payment_Amount+isnull(TDS_Amount,0) ELSE 0 END as CrAmt,1 as OrderDRCR,'' as saleinvoiceno,'' as salesmancode,'' as salesmanname,'' as txtno,'' as EntryDesc, '' as Settlement_code, '' as Settlement_Description, LoadOutNo, Salesman_Name   from tspl_payment_header  left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=tspl_payment_header.Bank_Code  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_BANK_MASTER.BANKACC      "
            qry += " union all"
            qry += "  select TSPL_PAYMENT_HEADER .Payment_No ,Debit_Account as ACCode, Description,'' as Entry_Desc,'' as Remarks,'' as Comment,TSPL_PAYMENT_HEADER.Payment_Amount as DrAmt,0 as CrAmt,0 as OrderDRCR,'' as saleinvoiceno,'' as salesmancode,'' as salesmanname,'' as txtno,'' as EntryDesc, '' as Settlement_code, '' as Settlement_Description, LoadOutNo, Salesman_Name from TSPL_PAYMENT_HEADER  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PAYMENT_HEADER.Debit_Account where TSPL_PAYMENT_HEADER.Payment_Type='OA'"
            qry += " union all"
            qry += " select tspl_payment_header.Payment_No,Debit_Account as ACCode, Description,'' as Entry_Desc,'' as Remarks,'' as Comment,TSPL_PAYMENT_HEADER.Payment_Amount as DrAmt,0 as CrAmt,0 as OrderDRCR,'' as saleinvoiceno,'' as salesmancode,'' as salesmanname,'' as txtno,'' as EntryDesc, '' as Settlement_code, '' as Settlement_Description, LoadOutNo, Salesman_Name from TSPL_PAYMENT_HEADER  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PAYMENT_HEADER.Debit_Account where TSPL_PAYMENT_HEADER.Payment_Type='AV'    "
            qry += " union all"
            qry += " select distinct tspl_payment_header.Payment_No, TSPL_PAYMENT_HEADER.Debit_Account as Account_Code,(select Description from TSPL_GL_ACCOUNTS where Account_Code=TSPL_PAYMENT_HEADER.Debit_Account) as Description,tspl_payment_header.Entry_Desc,'' as Remarks,TSPL_PAYMENT_DETAIL.Comment,TSPL_PAYMENT_DETAIL.Applied_Amount as DrAmt,0 as CrAmt,0 as OrderDRCR,'' as saleinvoiceno,'' as salesmancode,'' as salesmanname,'' as txtno ,TSPL_PAYMENT_DETAIL.Vendor_Invoice_No+ case when len(RTRIM(TSPL_PAYMENT_DETAIL.Vendor_Invoice_No))>0 then ', ' else ' ' end +Vendor_Invoice_Date  as EntryDesc, Settlement_code, Settlement_Description , LoadOutNo, Salesman_Name  from tspl_payment_header  "
            qry += " left outer join TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_HEADER .Payment_No =TSPL_PAYMENT_DETAIL .Payment_No left outer join  TSPL_VENDOR_INVOICE_HEAD  on TSPL_PAYMENT_DETAIL.Document_No =TSPL_VENDOR_INVOICE_HEAD.Document_No where TSPL_PAYMENT_HEADER.Payment_Type ='PY'"
            qry += "  union all select tspl_payment_header.Payment_No,(select bm.Tax_Account  from TSPL_TDS_BRANCH_MASTER bm join TSPL_REMITTANCE r on bm.Branch_Code = r.Branch_Code where r.Document_No = TSPL_PAYMENT_HEADER.Payment_No ) as ACCode,( select Description  from TSPL_GL_ACCOUNTS where Account_Code =(select bm.Tax_Account  from TSPL_TDS_BRANCH_MASTER bm join TSPL_REMITTANCE r on bm.Branch_Code = r.Branch_Code where r.Document_No = TSPL_PAYMENT_HEADER.Payment_No )) Description,'' as Entry_Desc,'' as Remarks,'' as Comment,TSPL_PAYMENT_HEADER.TDS_Amount  as DrAmt,0 as CrAmt,0 as OrderDRCR,'' as saleinvoiceno,'' as salesmancode,'' as salesmanname,'' as txtno,'' as EntryDesc, '' as  Settlement_code, '' as Settlement_Description, LoadOutNo, Salesman_Name from TSPL_PAYMENT_HEADER  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PAYMENT_HEADER.Debit_Account  where TSPL_PAYMENT_HEADER.Payment_Type='OA'  or TSPL_PAYMENT_HEADER.Payment_Type='AV'  "
            qry += " UNION all "
            qry += " select tspl_payment_header.Payment_No, Account_Code,TSPL_PAYMENT_DETAIL.Description ,tspl_payment_header.Entry_Desc,TSPL_PAYMENT_DETAIL .Remarks ,'' as Comment,(Case when Net_Balance >0 then Net_Balance else 0.0 end) as DrAmt,(Case when Net_Balance < 0 then Net_Balance *-1  else 0 end) As CrAmt,0 as OrderDRCR ," & _
            " (case when TSPL_PAYMENT_HEADER.Payment_Type='MI'  and ( TSPL_PAYMENT_HEADER.apply_by='sale invoice' or TSPL_PAYMENT_HEADER.apply_by='Load Out' )     then apply_to else '' end ) as saleinvoiceno," & _
            " (case when TSPL_PAYMENT_HEADER.Payment_Type='MI'  and TSPL_PAYMENT_HEADER.apply_by='sale invoice'   then TSPL_SALE_INVOICE_HEAD .Salesman_Code  else " & _
            " case when TSPL_PAYMENT_HEADER.Payment_Type='MI'  and TSPL_PAYMENT_HEADER.apply_by='Load Out'   then  TSPL_SHIPMENT_MASTER.Salesman_Code else  '' end end  ) as salesmancode," & _
            "(case when TSPL_PAYMENT_HEADER.Payment_Type='MI'  and TSPL_PAYMENT_HEADER.apply_by='sale invoice'     then a.emp_name else " & _
            " case when TSPL_PAYMENT_HEADER.Payment_Type='MI'  and TSPL_PAYMENT_HEADER.apply_by='Load Out' then b.emp_name  else '' end end ) as emp_name  ,(case when TSPL_PAYMENT_HEADER.Payment_Type='MI'  and TSPL_PAYMENT_HEADER.apply_by='sale invoice' then   'Sale Invoice No' else case when TSPL_PAYMENT_HEADER.Payment_Type='MI'  and TSPL_PAYMENT_HEADER.apply_by='Load Out' then 'Load Out No' else ''end end )  as txtno,'' as EntryDesc, Settlement_code, Settlement_Description, LoadOutNo, Salesman_Name  " & _
            " from TSPL_PAYMENT_DETAIL " & _
            " left outer join tspl_payment_header on tspl_payment_header.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No " & _
            " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_PAYMENT_HEADER.Apply_To =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No "
            qry += " left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No =TSPL_PAYMENT_HEADER.Apply_To      " & _
            " left outer join TSPL_EMPLOYEE_MASTER as a on TSPL_SALE_INVOICE_HEAD.Salesman_Code = a.EMP_CODE   " & _
            " left outer join TSPL_EMPLOYEE_MASTER as b   on   TSPL_SHIPMENT_MASTER.Salesman_Code =b.EMP_CODE where Account_Code not in ('NULL')"
            qry += " ) Final left outer join tspl_payment_header on TSPL_PAYMENT_HEADER.Payment_No=final.Payment_No left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=tspl_payment_header.Bank_Code  left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code=tspl_payment_header.Created_By  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_PAYMENT_HEADER.Comp_Code  where 2=2 "

            If ArrDocument IsNot Nothing AndAlso ArrDocument.Count > 0 Then
                qry += "and  tspl_payment_header.Payment_No in (" + clsCommon.GetMulcallString(ArrDocument) + ")"
            End If

            If ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
                qry += "and tspl_payment_header.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ")"
            End If

            If clsCommon.myLen(fromDate) > 0 Then
                qry += " and convert(date,tspl_payment_header.payment_date,103) >= convert(date,'" + fromDate + "',103) "
            End If
            If clsCommon.myLen(toDate) > 0 Then
                qry += " and convert(date,tspl_payment_header.payment_date,103) <= convert(date,'" + toDate + "',103) "
            End If
            qry += "  and tspl_payment_header.payment_type not in ('AD')  order by final.Payment_No,OrderDRCR  "

            dt1 = clsDBFuncationality.GetDataTable(qry)
            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt1, "crptSettlementEntry", "Payment Details")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
#End Region

    Private Sub FrmSettlementEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
                If gv1.CurrentColumn Is gv1.Columns(colSettlementCode) Then
                    OpenICodeList(True)
                End If
                setGridFocus()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
                AddNew()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
                SaveData(clicked)
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
                PostDataNew()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
                Me.Close()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
                Delete()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.R AndAlso MyBase.isDeleteFlag Then
                PrintData()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub SetLength()
        fndSettlementEntry.MyMaxLength = 30
        txtDesc.MaxLength = 100
        txtSalesManName.MaxLength = 200
        txtRouteDesc.MaxLength = 200
        txtLocationDesc.MaxLength = 200

    End Sub
    Private Sub FrmSettlementEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLength()
        AddNew()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        If clsCommon.myLen(Me.Tag) > 0 Then
            fndSettlementEntry.Value = clsCommon.myCstr(Me.Tag)
            FillData()
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmSettlementEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub AddNew()
        BlankAllControls()
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        gv1.Rows.AddNew()
        gv1.Rows(0).Cells(colLineNo).Value = 1
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        AddNew()
    End Sub
    Sub BlankAllControls()
        dtSettlement.Value = System.DateTime.Now.Date
        fndSettlementEntry.Value = ""
        fndloadno.Value = ""
        fndRouteNo.Value = ""
        fndSalesMan.Value = ""
        fndLocation.Value = ""
        txtAppliedAmt.Text = "0.00"
        txtBalanceAmt.Text = "0.00"
        txtDesc.Text = ""
        txtLocationDesc.Text = ""
        txtRouteDesc.Text = ""
        txtSalesManName.Text = ""
        txtSettlementAmt.Text = "0.00"
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        LoadBlankGrid()
    End Sub

    Private Sub fndloadno__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndloadno._MYValidating
        Dim qry As String = "select Transfer_No  , tspl_transfer_head.Transfer_Date as [Entry Date],From_Location , To_Location ,Route_No as [Route No],Price_Code as [Price Code] ,toloc_desc as [SalesMan]  from tspl_transfer_head left outer join tspl_quicksettlement on tspl_transfer_head.transfer_no =tspl_quicksettlement.transfer_number "
        Dim whrcls As String = "Transfer_Type ='LO' and Item_Type ='Full' and tspl_transfer_head.Post='Y' and To_Location in (select Location_Code  from TSPL_LOCATION_MASTER where Location_Type ='Logical')  and tspl_quicksettlement.post='Y'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls += " and From_Location in ( select location_code from TSPL_LOCATION_MASTER where Location_Type ='physical'and  location_code in (" + objCommonVar.strCurrUserLocations + "))"
        End If
        fndloadno.Value = clsCommon.ShowSelectForm("LOadOutFND", qry, "Transfer_No", whrcls, fndloadno.Value, "Transfer_No", isButtonClicked)
        If clsCommon.myLen(fndloadno.Value) > 0 Then
            FillAdditionalInfo()
        End If
    End Sub
    Sub FillAdditionalInfo()
        dtSettlement.Value = clsCommon.GetPrintDate(connectSql.RunScalar("select  Transfer_Date from tspl_transfer_head where Transfer_No ='" + fndloadno.Value + "'"), "dd/MMM/yyyy")
        fndRouteNo.Value = clsCommon.myCstr(connectSql.RunScalar("select  Route_No as [Route No] from tspl_transfer_head where Transfer_No ='" + fndloadno.Value + "'"))
        txtRouteDesc.Text = clsCommon.myCstr(connectSql.RunScalar("select Route_Desc  from TSPL_ROUTE_MASTER where Route_No ='" + fndRouteNo.Value + "'"))
        fndSalesMan.Value = clsCommon.myCstr(connectSql.RunScalar("select  To_Location  from tspl_transfer_head where Transfer_No ='" + fndloadno.Value + "'"))
        txtSalesManName.Text = clsCommon.myCstr(connectSql.RunScalar("select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" + fndSalesMan.Value + "'"))
        fndLocation.Value = clsCommon.myCstr(connectSql.RunScalar("select  From_Location  from tspl_transfer_head where Transfer_No ='" + fndloadno.Value + "'"))
        txtLocationDesc.Text = clsCommon.myCstr(connectSql.RunScalar("select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"))
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        If e.Column.Name = colSettlementCode Then
            OpenICodeList(False)
        End If
        If e.Column.Name = colAmount Then
            txtSettlementAmt.Text = 0.0
            For Each dr As GridViewRowInfo In gv1.Rows
                txtSettlementAmt.Text = clsCommon.myCdbl(txtSettlementAmt.Text) + clsCommon.myCdbl(dr.Cells(colAmount).Value)
            Next
            setGridFocus()
        End If
    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select SettleMentCode ,Description ,Account_Code ,Account_Description  from tspl_SettleMent_Master "
        Dim whrCls As String = "len(Account_Code)>0 and tspl_SettleMent_Master.SettleMentCode not in (select Settlement_code  from TSPL_PAYMENT_HEADER inner join TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_HEADER.Payment_No =TSPL_PAYMENT_DETAIL.Payment_No  where LoadOutNo ='" + fndloadno.Value + "' ) "
        gv1.CurrentRow.Cells(colSettlementCode).Value = clsCommon.ShowSelectForm("SetMasterFND", qry, "SettleMentCode", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colSettlementCode).Value), "SettleMentCode", isButtonClick)
        gv1.CurrentRow.Cells(colSettlementName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from tspl_SettleMent_Master where SettleMentCode='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colSettlementCode).Value) + "'"))
        gv1.CurrentRow.Cells(colGLAccount).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Code from tspl_SettleMent_Master where SettleMentCode='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colSettlementCode).Value) + "'"))
        gv1.CurrentRow.Cells(colGLAccDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Description from tspl_SettleMent_Master where SettleMentCode='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colSettlementCode).Value) + "'"))
        setGridFocus()
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(colLineNo).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub txtAppliedAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAppliedAmt.TextChanged
        ' txtBalanceAmt.Text = clsCommon.myCdbl(txtSettlementAmt.Text) - clsCommon.myCdbl(txtAppliedAmt.Text)
    End Sub

    Private Sub fndSettlementEntry__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndSettlementEntry._MYNavigator
        Dim qry As String = "select  TSPL_PAYMENT_HEADER.Payment_No   from TSPL_PAYMENT_HEADER  Where 2=2"
        Dim WhrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += " AND Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_PAYMENT_HEADER.Payment_No=(select MIN(Payment_No) from TSPL_PAYMENT_HEADER  where LEN(LoadOutNo)>0 and Payment_Type ='MI' " + WhrCls + " )"
            Case NavigatorType.Last
                qry += " and TSPL_PAYMENT_HEADER.Payment_No=(select MAX(Payment_No) from TSPL_PAYMENT_HEADER where  LEN(LoadOutNo)>0 and Payment_Type ='MI' " + WhrCls + ")"
            Case NavigatorType.Next
                qry += " and TSPL_PAYMENT_HEADER.Payment_No=(select Min(Payment_No) from TSPL_PAYMENT_HEADER where  LEN(LoadOutNo)>0 and Payment_Type ='MI' " + WhrCls + "  and  Payment_No > '" + fndSettlementEntry.Value + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_PAYMENT_HEADER.Payment_No=(select Max(Payment_No) from TSPL_PAYMENT_HEADER where  LEN(LoadOutNo)>0 and Payment_Type ='MI' " + WhrCls + "  and  Payment_No < '" + fndSettlementEntry.Value + "')"
            Case NavigatorType.Current
                qry += " and TSPL_PAYMENT_HEADER.Payment_No='" + fndSettlementEntry.Value + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            fndSettlementEntry.Value = clsCommon.myCstr(dt.Rows(0)("Payment_No"))
            FillData()
        End If
    End Sub

    Private Sub fndSettlementEntry__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSettlementEntry._MYValidating
        Dim Qry As String = " SELECT distinct    p.Payment_No AS PaymentNo, p.Payment_Date AS [Payment Date], " & _
                           "  (CASE p.Payment_Type WHEN 'PY' THEN 'Payment' WHEN 'MI' THEN 'Misc Payment' WHEN 'AV' THEN 'Advance' WHEN 'AD' THEN 'Apply Document' ELSE 'On Account'  " & _
                           " END) AS [Payment Type], b.DESCRIPTION AS Description, p.Remit_To AS [Remit To],(SELECT     CASE WHEN Posted = 'N' THEN 'UnPosted' ELSE 'Posted' END AS Expr1) AS Status " & _
                           " ,Payment_Date ,LoadOutNo ,Location_Code,Location_Description  ,P.Salesman_Code ,Salesman_Name,tspl_quicksettlement.quick_settlement_id as [Quick Settlement] FROM TSPL_PAYMENT_HEADER AS p left outer JOIN TSPL_BANK_MASTER AS b ON p.Bank_Code = b.BANK_CODE LEFT OUTER JOIN TSPL_PAYMENT_DETAIL AS PL ON PL.Payment_No = p.Payment_No " & _
                           " left outer join TSPL_VENDOR_INVOICE_DETAIL IL on IL.Document_No =PL.Document_No left outer join tspl_quicksettlement on p.LoadOutNo=tspl_quicksettlement.transfer_number"
        Dim WhrCls As String = " len(p.LoadOutNo)>0 and p.Payment_Type ='MI'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += " AND p.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        fndSettlementEntry.Value = clsCommon.ShowSelectForm("PaymntSelctrFND", Qry, "PaymentNo", WhrCls, fndSettlementEntry.Value, "PaymentNo", isButtonClicked)
        If fndSettlementEntry.Value.Length > 0 Then
            FillData()
        End If
    End Sub
    Sub FillData()
        Try
            Dim Qry As String = "select  Payment_Date ,Entry_Desc,Payment_Amount ,Posted,LoadOutNo ,Salesman_Code,Salesman_Name,Route_NO,Route_Description ,Location_Code ,Location_Description  from TSPL_PAYMENT_HEADER where Payment_No='" + fndSettlementEntry.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            Dim PostStatus As String = ""
            If dt.Rows.Count > 0 Then
                dtSettlement.Value = dt.Rows(0).Item("Payment_Date").ToString()
                txtDesc.Text = dt.Rows(0).Item("Entry_Desc").ToString()
                fndloadno.Value = dt.Rows(0).Item("LoadOutNo").ToString()
                fndSalesMan.Value = dt.Rows(0).Item("Salesman_Code").ToString()
                txtSalesManName.Text = dt.Rows(0).Item("Salesman_Name").ToString()
                fndRouteNo.Value = dt.Rows(0).Item("Route_NO").ToString()
                txtRouteDesc.Text = dt.Rows(0).Item("Route_Description").ToString()
                fndLocation.Value = dt.Rows(0).Item("Location_Code").ToString()
                txtLocationDesc.Text = dt.Rows(0).Item("Location_Description").ToString()
                txtSettlementAmt.Text = dt.Rows(0).Item("Payment_Amount").ToString()
                txtAppliedAmt.Text = dt.Rows(0).Item("Payment_Amount").ToString()
                txtBalanceAmt.Text = 0
                PostStatus = dt.Rows(0).Item("Posted").ToString()
            End If
            gv1.AutoGenerateColumns = False
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            transportSql.FillGridView("select Payment_Line_No,Settlement_code ,Settlement_Description , Account_Code , Description , Remarks, Net_Balance  from TSPL_PAYMENT_DETAIL  where Payment_No = '" + fndSettlementEntry.Value + "'", gv1)
            gv1.Columns(colLineNo).FieldName = "Payment_Line_No"
            gv1.Columns(colGLAccount).FieldName = "Account_Code"
            gv1.Columns(colGLAccDesc).FieldName = "Description"
            gv1.Columns(colAmount).FieldName = "Net_Balance"
            gv1.Columns(colRemarks).FieldName = "Remarks"
            gv1.Columns(colSettlementCode).FieldName = "Settlement_code"
            gv1.Columns(colSettlementName).FieldName = "Settlement_Description"
            If PostStatus = "P" Then
                btnSave.Text = "Update"
                btnSave.Enabled = False
                btnDelete.Enabled = False
                btnPost.Enabled = False
            Else
                btnSave.Text = "Update"
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtSettlementAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSettlementAmt.TextChanged
        ' txtBalanceAmt.Text = clsCommon.myCdbl(txtSettlementAmt.Text) - clsCommon.myCdbl(txtAppliedAmt.Text)
    End Sub

    Private Sub btnEmptySettlement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmptySettlement.Click
        If fndloadno.Value <> "" And fndRouteNo.Value <> "" Then
            '' Dim frm As New FrmAdjustments1("1," + fndloadno.Value + "", fndloadno.Value, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, fndSalesMan.Value, txtSalesManName.Text, fndLocation.Value, txtLocationDesc.Text)

            Dim frm As New frmAdjustmentEmpty()
            frm.strLoadoutNo = fndloadno.Value
            'frm.strCustomer = custcode
            frm.strSalesman = fndSalesMan.Value
            frm.strLocation = fndLocation.Value
            frm.dtTransDate = dtSettlement.Value
            'frm.strVehicleCode = txtVehicleCode.Value
            'frm.Show()
            frm.ShowDialog()
        End If
    End Sub


End Class

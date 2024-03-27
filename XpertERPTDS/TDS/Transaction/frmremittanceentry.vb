'by vipin for post status check on update..

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports common
Imports System.Threading
Imports XpertERPEngine

Public Class Frmremittanceentry
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isNewEntry As Boolean = False
    Dim userCode As String = String.Empty
    Dim companyCode As String = String.Empty
    Dim query As String = String.Empty
    Dim strvendorcode As String = String.Empty
    Dim strdeductioncode As String = String.Empty
    Dim strvendorname As String = String.Empty
    Dim tds_per As Decimal = 0
    Dim surcharge_per As Decimal = 0
    Dim edu_cess_per As Decimal = 0
    Dim sec_edu_cess_per As Decimal = 0
    Dim postdate As String = String.Empty
    Dim paymentdate As String = String.Empty
    Dim chequedate As String = String.Empty
    Dim remittancedate As String = String.Empty
    Dim challandate As String = String.Empty
    Dim dr As SqlDataReader
    Dim ds As New DataSet()
    Dim dt As New DataTable()
    Dim btntooltip As ToolTip = New ToolTip


    Const colSNo As String = "COLSNO"
    Const colDocNo As String = "COLDOCNO"
    Const colDocDate As String = "COLDOCDATE"
    Const colDocType As String = "COLDOCTYPE"
    Const colDocAmt As String = "COLDOCAMT"
    Const colServiceType As String = "COLSERVICETYPE"
    Const colActTDSBase As String = "COLACTTDSBASE"
    Const colActTDS As String = "COLACTTDS"
    Const colActSurcharge As String = "COLACTSURCHARGE"
    Const colActEduCess As String = "COLACTEDUCESS"
    Const colActSecEduCess As String = "COLACTSECEDUCESS"
    Const colActTotTDS As String = "COLACTTOTTDS"
    Const colCalTDSBase As String = "COLCALTDSBASE"
    Const colCalTDS As String = "COLCALTDS"
    Const colCalSurcharge As String = "COLCALSURCHARGE"
    Const colCalEduCess As String = "COLCALEDUCESS"
    Const colCalSecEduCess As String = "COLCALSECEDUCESS"
    Const colCalTotTDS As String = "COLCALTOTTDS"
    Const colVendorCode As String = "COLVENDORCODE"
    Const colVendorName As String = "COLVENDORNAME"
    Const colTDSPer As String = "COLTDSPER"
    Const colSurPer As String = "COLSURPER"
    Const colEduCessPer As String = "COLEDUCESSPER"
    Const colSecEduCessPer As String = "COLSECEDUCESSPER"
    Const colFiscalYear As String = "COLFISCALYEAR"
    Const colQuarter As String = "COLQUARTER"
    Const colDeductionCode As String = "COLDEDUCTIONCODE"
#End Region
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.remittanceentry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Private Sub funinsert()

    End Sub

    Private Sub fundelete()
        Dim trans As SqlTransaction = Nothing
        Try
            connectSql.OpenConnection()
            trans = connectSql.OpenConnection.BeginTransaction()
            connectSql.RunSqlTransaction(trans, "delete from TSPL_REMITTANCE_ENTRY where Remittance_Code = '" + fndremittance.Value + "'")
            trans.Commit()
            myMessages.delete()
        Catch ex As Exception
            myMessages.myExceptions(ex)
            trans.Rollback()

        End Try
    End Sub

    ''To fill the according to the remittance code
    Private Sub funfill()
        LoadBlankGrid()
        Dim arr As List(Of clsRemittance) = clsRemittance.GetDataForMainRemittance(fndremittance.Value)
        Dim dblTotAmt As Double = 0
        Dim dblTaxAmt As Double = 0
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            txtsectioncode.Text = arr(0).Section_Code
            txtsectiondesc.Text = arr(0).Section_Description
            txtbranchcode.Text = arr(0).Branch_Code
            txtfiscalyear.Text = arr(0).Fiscal_Year
            dtpremittancedate.Text = clsCommon.GETSERVERDATE()
            ddlfiscalquarter.Text = arr(0).Quarter
            strdeductioncode = arr(0).Deduction_Code
            UsLock1.Status = ERPTransactionStatus.Pending
            Dim SNO As Integer = 1
            For Each obj As clsRemittance In arr
                gv1.Rows.AddNew()
                gv1.Rows(gv1.RowCount - 1).Cells(colSNo).Value = gv1.RowCount
                gv1.Rows(gv1.RowCount - 1).Cells(colDocNo).Value = obj.Document_No
                gv1.Rows(gv1.RowCount - 1).Cells(colDocDate).Value = obj.Document_Date
                gv1.Rows(gv1.RowCount - 1).Cells(colDocType).Value = obj.Document_Type
                gv1.Rows(gv1.RowCount - 1).Cells(colDocAmt).Value = obj.Document_Amount
                gv1.Rows(gv1.RowCount - 1).Cells(colServiceType).Value = obj.Service_Type
                gv1.Rows(gv1.RowCount - 1).Cells(colActTDSBase).Value = obj.Actual_TDS_Base
                gv1.Rows(gv1.RowCount - 1).Cells(colActTDS).Value = obj.Actual_TDS
                gv1.Rows(gv1.RowCount - 1).Cells(colActSurcharge).Value = obj.Actual_Surcharge
                gv1.Rows(gv1.RowCount - 1).Cells(colActEduCess).Value = obj.Actual_Edu_Cess
                gv1.Rows(gv1.RowCount - 1).Cells(colActSecEduCess).Value = obj.Actual_Sec_Educess
                gv1.Rows(gv1.RowCount - 1).Cells(colActTotTDS).Value = obj.Actual_Total_TDS
                gv1.Rows(gv1.RowCount - 1).Cells(colCalTDSBase).Value = obj.Calculated_TDS_Base
                gv1.Rows(gv1.RowCount - 1).Cells(colCalTDS).Value = obj.Calculated_TDS
                gv1.Rows(gv1.RowCount - 1).Cells(colCalSurcharge).Value = obj.Calculated_Surcharge
                gv1.Rows(gv1.RowCount - 1).Cells(colCalEduCess).Value = obj.Calculated_Edu_Cess
                gv1.Rows(gv1.RowCount - 1).Cells(colCalSecEduCess).Value = obj.Calculated_Sec_Educess
                gv1.Rows(gv1.RowCount - 1).Cells(colCalTotTDS).Value = obj.Calculated_Total_TDS
                gv1.Rows(gv1.RowCount - 1).Cells(colVendorCode).Value = obj.Vendor_Code
                gv1.Rows(gv1.RowCount - 1).Cells(colVendorName).Value = obj.Vendor_Name
                gv1.Rows(gv1.RowCount - 1).Cells(colTDSPer).Value = obj.TDS_Per
                gv1.Rows(gv1.RowCount - 1).Cells(colSurPer).Value = obj.Surcharge_Per
                gv1.Rows(gv1.RowCount - 1).Cells(colEduCessPer).Value = obj.Edu_Cess_Per
                gv1.Rows(gv1.RowCount - 1).Cells(colSecEduCessPer).Value = obj.Sec_Educess_Per
                gv1.Rows(gv1.RowCount - 1).Cells(colFiscalYear).Value = obj.Fiscal_Year
                gv1.Rows(gv1.RowCount - 1).Cells(colQuarter).Value = obj.Quarter
                gv1.Rows(gv1.RowCount - 1).Cells(colDeductionCode).Value = obj.Deduction_Code
                dblTotAmt += obj.Actual_Total_TDS
            Next
            txtamttoremit.Text = clsCommon.myCstr(dblTotAmt)
            txttaxamt.Text = clsCommon.myCstr(dblTotAmt)
        End If
    End Sub

    ''To Authorised the user 
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "REMIT-ENTRY"
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
    '            btnsave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Sub Frmremittanceentry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
        postdate = Format(dtpposting.Value, "dd/MM/yyyy")
        paymentdate = Format(dtppayment.Value, "dd/MM/yyyy")
        chequedate = Format(dtpcheque.Value, "dd/MM/yyyy")
        remittancedate = Format(dtpremittancedate.Value, "dd/MM/yyyy")
        challandate = Format(dtpchallan.Value, "dd/MM/yyyy")


        btndelete.Enabled = False
        funreset()
        ' Edited by Abhishek
        btntooltip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        btntooltip.SetToolTip(btnpost, "Press Alt+P Post Trasnaction")
        btntooltip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        btntooltip.SetToolTip(btnclose, "Press Esc Close the Window")
        btntooltip.SetToolTip(btnreset, "Press Alt+N Adding New Transaction")
        btntooltip.SetToolTip(btnvoidremittance, "Press Alt+V for VoidRemmittance")

        If clsCommon.myLen(Me.Tag) > 0 Then
            fndremittance.Value = clsCommon.myCstr(Me.Tag)
            RemittanceChanged()
        End If
       
    End Sub

    Public Sub RemittanceChanged()
        query = "SELECT Remittance_Main_code FROM TSPL_REMITTANCE WHERE Remittance_Main_Code IN(select Remittance_Code  from TSPL_REMITTANCE_ENTRY WHERE Remittance_Code = '" + fndremittance.Value + "')"
        If Not String.IsNullOrEmpty(connectSql.RunScalar(query)) Then
            funfillremittance()
            btnsave.Text = "Update"
            btnpost.Enabled = True
            btnvoidremittance.Enabled = True
            btndelete.Enabled = True
            isNewEntry = False
        Else
            funfill()
            btnsave.Text = "Save"
            btndelete.Enabled = False
        End If
        query = "select posted from TSPL_REMITTANCE_ENTRY where Remittance_Code = '" + fndremittance.Value + "'"
        Dim post As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
        If post = "N" Then
            UsLock1.Status = ERPTransactionStatus.Pending

        ElseIf post = "Y" Then
            funpostdata()
            UsLock1.Status = ERPTransactionStatus.Approved
        End If
    End Sub

    Private Sub paymentcodechanged()
        Dim strcheckcode As String = String.Empty
        If Not String.IsNullOrEmpty(connectSql.RunScalar("select Payment_Type  from TSPL_PAYMENT_CODE  where Payment_Code ='" + fndpaymentcode.Value + "'")) Then
            strcheckcode = connectSql.RunScalar("select Payment_Type  from TSPL_PAYMENT_CODE  where Payment_Code ='" + fndpaymentcode.Value + "'")
        End If
        If Not String.IsNullOrEmpty(strcheckcode) Then
            If clsCommon.CompairString(strcheckcode, "Cheque") = CompairStringResult.Equal Then
                txtchequeno.Visible = True
                dtpcheque.Visible = True
                lblchequedate.Visible = True
                lblchequeno.Visible = True
            Else
                txtchequeno.Visible = False
                dtpcheque.Visible = False
                lblchequedate.Visible = False
                lblchequeno.Visible = False
            End If
        End If
    End Sub

    Private Sub txtbranchcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbranchcode.TextChanged
        If Not String.IsNullOrEmpty(connectSql.RunScalar("select Branch_Name  from TSPL_TDS_BRANCH_MASTER where Branch_Code = '" + txtbranchcode.Text + "'")) Then
            txtbranchdesc.Text = connectSql.RunScalar("select Branch_Name  from TSPL_TDS_BRANCH_MASTER where Branch_Code = '" + txtbranchcode.Text + "'")
        End If
    End Sub

    Private Sub funpost()
        clsRemitanceEntry.PostData(fndremittance.Value)
    End Sub

    Private Sub funreset()
        isNewEntry = True
        fndbankcode.Value = ""
        fndpaymentcode.Value = ""
        fndremittance.Value = ""
        txtbranchdesc.Text = ""
        txtbsrcode.Text = ""
        txtbsrname.Text = ""
        txtchallanno.Text = ""
        txtchequeno.Text = ""
        txtdesc.Text = ""
        txtsectioncode.Text = ""
        txtsectiondesc.Text = ""
        txtfiscalyear.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        txtremitto.Text = ""
        txtbankdesc.Text = ""
        dtpchallan.Value = connectSql.serverDate()
        dtpcheque.Value = connectSql.serverDate()
        dtppayment.Value = connectSql.serverDate()
        dtpposting.Value = connectSql.serverDate()
        dtpremittancedate.Value = connectSql.serverDate()
        txtbranchcode.Text = ""
        btnsave.Text = "Save"
        btndelete.Enabled = True
        btnsave.Enabled = True
        LoadBlankGrid()
    End Sub

    ''To call the insert funtion
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        savedata()
    End Sub

    Public Function AllowToSave() As Boolean


        If btnsave.Text = "Update" Then
            Dim strchk As String = "select Posted from TSPL_REMITTANCE_ENTRY where Remittance_Code='" + fndremittance.Value + "'"
            Dim chkpost As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strchk))
            If chkpost = "Y" Then
                clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                Return False
            End If
        End If


        If fndremittance.Value = "" Then
            myMessages.blankValue(Me, "Remittance Code", Me.Text)
            fndremittance.Focus()
            Return False
        ElseIf fndbankcode.Value = "" Then
            myMessages.blankValue(Me, "Bank Code", Me.Text)
            fndbankcode.Focus()
            Return False
        ElseIf fndpaymentcode.Value = "" Then
            myMessages.blankValue(Me, "Payment Code", Me.Text)
            fndpaymentcode.Focus()
            Return False
        ElseIf txtremitto.Text = "" Then
            myMessages.blankValue(Me, "Remit TO", Me.Text)
            txtremitto.Focus()
            Return False
        End If
        Return True
    End Function

    Public Sub savedata()
        If AllowToSave() Then
            Try
                Dim obj As New clsRemitanceEntry
                obj.Remittance_Code = fndremittance.Value
                obj.Remittance_Date = dtpremittancedate.Value

                obj.Bank_Code = fndbankcode.Value
                obj.Amt_To_Remit = clsCommon.myCdbl(txtamttoremit.Text)
                obj.Remit_To = txtremitto.Text
                obj.AP_Posting_Date = dtpposting.Value
                obj.AP_Payment_Date = dtppayment.Value
                obj.Payment_Code = fndpaymentcode.Value
                obj.Cheque_No = txtchequeno.Text
                If clsCommon.myLen(txtchequeno.Text) > 0 Then
                    obj.Cheque_Date = dtpcheque.Value
                End If
                obj.BSR_Code = txtbsrcode.Text
                obj.BSR_Name = txtbsrname.Text
                obj.Challan_No = txtchallanno.Text
                If clsCommon.myLen(txtchallanno.Text) > 0 Then
                    obj.Challan_Date = dtpchallan.Value
                End If
                obj.Section_Code = txtsectioncode.Text
                obj.Section_Description = txtsectiondesc.Text
                obj.Branch_Code = txtbranchcode.Text
                'obj.Select_By = fndremittance.Value
                'obj.Remit_TDS = fndremittance.Value
                obj.Tax_Amount = clsCommon.myCdbl(txttaxamt.Text)
                obj.Description = txtdesc.Text
                obj.Arr = New List(Of clsRemitanceEntryDetail)
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    Dim objtr As New clsRemitanceEntryDetail
                    objtr.SNO = gv1.Rows(ii).Cells(colSNo).Value
                    objtr.Document_No = gv1.Rows(ii).Cells(colDocNo).Value
                    objtr.Document_Date = gv1.Rows(ii).Cells(colDocDate).Value
                    objtr.Document_Type = gv1.Rows(ii).Cells(colDocType).Value
                    objtr.Document_Amount = gv1.Rows(ii).Cells(colDocAmt).Value
                    objtr.Service_Type = gv1.Rows(ii).Cells(colServiceType).Value
                    objtr.Actual_TDS_Base = gv1.Rows(ii).Cells(colActTDSBase).Value
                    objtr.Actual_TDS = gv1.Rows(ii).Cells(colActTDS).Value
                    objtr.Actual_Surcharge = gv1.Rows(ii).Cells(colActSurcharge).Value
                    objtr.Actual_Edu_Cess = gv1.Rows(ii).Cells(colActEduCess).Value
                    objtr.Actual_Sec_Educess = gv1.Rows(ii).Cells(colActSecEduCess).Value
                    objtr.Actual_Total_TDS = gv1.Rows(ii).Cells(colActTotTDS).Value
                    objtr.Calculated_TDS_Base = gv1.Rows(ii).Cells(colCalTDSBase).Value
                    objtr.Calculated_TDS = gv1.Rows(ii).Cells(colCalTDS).Value
                    objtr.Calculated_Surcharge = gv1.Rows(ii).Cells(colCalSurcharge).Value
                    objtr.Calculated_Edu_Cess = gv1.Rows(ii).Cells(colCalEduCess).Value
                    objtr.Calculated_Sec_Educess = gv1.Rows(ii).Cells(colCalSecEduCess).Value
                    objtr.Calculated_Total_TDS = gv1.Rows(ii).Cells(colCalTotTDS).Value
                    objtr.Vendor_Code = gv1.Rows(ii).Cells(colVendorCode).Value
                    objtr.Vendor_Name = gv1.Rows(ii).Cells(colVendorName).Value
                    objtr.TDS_Per = gv1.Rows(ii).Cells(colTDSPer).Value
                    objtr.Surcharge_Per = gv1.Rows(ii).Cells(colSurPer).Value
                    objtr.Edu_Cess_Per = gv1.Rows(ii).Cells(colEduCessPer).Value
                    objtr.Sec_Educess_Per = gv1.Rows(ii).Cells(colSecEduCessPer).Value
                    objtr.Fiscal_Year = gv1.Rows(ii).Cells(colFiscalYear).Value
                    objtr.Quarter = gv1.Rows(ii).Cells(colQuarter).Value
                    objtr.Deduction_Code = gv1.Rows(ii).Cells(colDeductionCode).Value
                    obj.Arr.Add(objtr)
                Next
                If obj.SaveData(obj, isNewEntry) Then
                    myMessages.insert()
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                    btnpost.Enabled = True
                    btnvoidremittance.Enabled = True
                    RemittanceChanged()
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try

        End If
    End Sub

    ''To call the delete function
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        deletedata()
    End Sub

    Public Sub deletedata()
        If fndremittance.Value = "" Then
            myMessages.blankValue(Me, "Remittance Code", Me.Text)
            fndremittance.Focus()
        Else
            If myMessages.deleteConfirm() Then
                fundelete()
                funreset()
            End If
        End If
    End Sub

    Private Sub btnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpost.Click
        postdata()
    End Sub

    Public Sub postdata()
        Try

            If myMessages.postConfirm() Then
                If fndremittance.Value <> "" And btnsave.Text = "Update" Then
                    funpost()
                    funpostdata()
                    RemittanceChanged()
                Else
                    common.clsCommon.MyMessageBoxShow(Me, "Please save before then post the remittance entry", Me.Text)

                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        funreset()
    End Sub

    Private Sub funfillremittance()
        LoadBlankGrid()
        Dim obj As clsRemitanceEntry = clsRemitanceEntry.GetData(fndremittance.Value)

        fndremittance.Value = obj.Remittance_Code
        dtpremittancedate.Value = obj.Remittance_Date

        fndbankcode.Value = obj.Bank_Code
        txtamttoremit.Text = obj.Amt_To_Remit
        txtremitto.Text = obj.Remit_To
        dtpposting.Value = obj.AP_Posting_Date
        dtppayment.Value = obj.AP_Payment_Date
        fndpaymentcode.Value = obj.Payment_Code
        txtchequeno.Text = obj.Cheque_No
        If obj.Cheque_Date.HasValue Then
            dtpcheque.Value = obj.Cheque_Date
        End If
        txtbsrcode.Text = obj.BSR_Code
        txtbsrname.Text = obj.BSR_Name
        txtchallanno.Text = obj.Challan_No
        If obj.Challan_Date.HasValue Then
            dtpchallan.Value = obj.Challan_Date
        End If
        txtsectioncode.Text = obj.Section_Code
        txtsectiondesc.Text = obj.Section_Description
        obj.Branch_Code = txtbranchcode.Text
        'fndremittance.Value=obj.Select_By 
        'fndremittance.Value=obj.Remit_TDS  
        txttaxamt.Text = obj.Tax_Amount
        txtdesc.Text = obj.Description

        If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
            For Each objtr As clsRemitanceEntryDetail In obj.Arr
                gv1.Rows.AddNew()
                gv1.Rows(gv1.RowCount - 1).Cells(colSNo).Value = objtr.SNO
                gv1.Rows(gv1.RowCount - 1).Cells(colDocNo).Value = objtr.Document_No
                gv1.Rows(gv1.RowCount - 1).Cells(colDocDate).Value = objtr.Document_Date
                gv1.Rows(gv1.RowCount - 1).Cells(colDocType).Value = objtr.Document_Type
                gv1.Rows(gv1.RowCount - 1).Cells(colDocAmt).Value = objtr.Document_Amount
                gv1.Rows(gv1.RowCount - 1).Cells(colServiceType).Value = objtr.Service_Type
                gv1.Rows(gv1.RowCount - 1).Cells(colActTDSBase).Value = objtr.Actual_TDS_Base
                gv1.Rows(gv1.RowCount - 1).Cells(colActTDS).Value = objtr.Actual_TDS
                gv1.Rows(gv1.RowCount - 1).Cells(colActSurcharge).Value = objtr.Actual_Surcharge
                gv1.Rows(gv1.RowCount - 1).Cells(colActEduCess).Value = objtr.Actual_Edu_Cess
                gv1.Rows(gv1.RowCount - 1).Cells(colActSecEduCess).Value = objtr.Actual_Sec_Educess
                gv1.Rows(gv1.RowCount - 1).Cells(colActTotTDS).Value = objtr.Actual_Total_TDS
                gv1.Rows(gv1.RowCount - 1).Cells(colCalTDSBase).Value = objtr.Calculated_TDS_Base
                gv1.Rows(gv1.RowCount - 1).Cells(colCalTDS).Value = objtr.Calculated_TDS
                gv1.Rows(gv1.RowCount - 1).Cells(colCalSurcharge).Value = objtr.Calculated_Surcharge
                gv1.Rows(gv1.RowCount - 1).Cells(colCalEduCess).Value = objtr.Calculated_Edu_Cess
                gv1.Rows(gv1.RowCount - 1).Cells(colCalSecEduCess).Value = objtr.Calculated_Sec_Educess
                gv1.Rows(gv1.RowCount - 1).Cells(colCalTotTDS).Value = objtr.Calculated_Total_TDS
                gv1.Rows(gv1.RowCount - 1).Cells(colVendorCode).Value = objtr.Vendor_Code
                gv1.Rows(gv1.RowCount - 1).Cells(colVendorName).Value = objtr.Vendor_Name
                gv1.Rows(gv1.RowCount - 1).Cells(colTDSPer).Value = objtr.TDS_Per
                gv1.Rows(gv1.RowCount - 1).Cells(colSurPer).Value = objtr.Surcharge_Per
                gv1.Rows(gv1.RowCount - 1).Cells(colEduCessPer).Value = objtr.Edu_Cess_Per
                gv1.Rows(gv1.RowCount - 1).Cells(colSecEduCessPer).Value = objtr.Sec_Educess_Per
                gv1.Rows(gv1.RowCount - 1).Cells(colFiscalYear).Value = objtr.Fiscal_Year
                gv1.Rows(gv1.RowCount - 1).Cells(colQuarter).Value = objtr.Quarter
                gv1.Rows(gv1.RowCount - 1).Cells(colDeductionCode).Value = objtr.Deduction_Code

            Next
        End If
    End Sub

    ''Private Sub funfillremittance()
    ''    query = "select Vendor_Code , Bank_Code ,Amt_To_Remit, Remit_To , AP_Posting_Date , AP_Payment_Date , Payment_Code, Cheque_No, Cheque_Date ,BSR_Code, BSR_Name ,Challan_No, Challan_Date , Posted , Vendor_Name , Document_No , Document_Date ,Document_Type ,Document_Amount , Service_Type , Actual_TDS_Base ,Calculated_TDS_Base , Actual_TDS , Calculated_TDS ,Actual_Surcharge, Calculated_Surcharge , Actual_Edu_Cess , Calculated_Edu_Cess , Actual_Sec_Educess , Calculated_Sec_Educess , Actual_Total_TDS , Calculated_Total_TDS ,Section_Code , Section_Description , Branch_Code ,Select_By, TDS_Per,Surcharge_Per ,Edu_Cess_Per , Fiscal_Year , Quarter , Deduction_Code ,remittance_date  from  TSPL_REMITTANCE_ENTRY where Remittance_Code = '" + fndremittance.Value + "'"
    ''    dr = connectSql.RunSqlReturnDR(query)
    ''    If dr.HasRows Then
    ''        LoadBlankGrid()
    ''        While dr.Read()
    ''            Dim i As GridViewRowInfo ''='' dgvremittance.Rows.AddNew()
    ''            txtsectioncode.Text = Convert.ToString(dr("Section_Code"))
    ''            txtsectiondesc.Text = Convert.ToString(dr("Section_Description"))
    ''            txtbranchcode.Text = Convert.ToString(dr("Branch_Code"))
    ''            txtfiscalyear.Text = Convert.ToString(dr("Fiscal_Year"))
    ''            dtpremittancedate.Value = Convert.ToString(dr("remittance_date"))
    ''            ddlfiscalquarter.Text = Convert.ToString(dr("Quarter"))
    ''            strvendorcode = Convert.ToString(dr("Vendor_Code"))
    ''            strdeductioncode = Convert.ToString(dr("Deduction_Code"))
    ''            fndbankcode.Value = Convert.ToString(dr("Bank_Code"))
    ''            fndpaymentcode.Value = Convert.ToString(dr("Payment_Code"))
    ''            dtpposting.Text = Convert.ToString(dr("AP_Posting_Date"))
    ''            dtppayment.Text = Convert.ToString(dr("AP_Payment_Date"))
    ''            txtbsrcode.Text = Convert.ToString(dr("BSR_Code"))
    ''            txtbsrname.Text = Convert.ToString(dr("BSR_Name"))
    ''            txtchallanno.Text = Convert.ToString(dr("Challan_No"))
    ''            dtpchallan.Text = Convert.ToString(dr("Challan_Date"))

    ''            If clsCommon.CompairString(Convert.ToString(dr("Posted")), "N") = CompairStringResult.Equal Then
    ''                UsLock1.Status = ERPTransactionStatus.Pending
    ''            Else
    ''                UsLock1.Status = ERPTransactionStatus.Approved
    ''            End If
    ''            strvendorname = Convert.ToString(dr("Vendor_Name"))
    ''            txtbranchcode.Text = Convert.ToString(dr("Branch_Code"))
    ''            txttaxamt.Text = Convert.ToString(dr("Calculated_Total_TDS"))
    ''            txtamttoremit.Text = Convert.ToString(dr("Calculated_Total_TDS"))
    ''            i.Cells("document_no").Value = Convert.ToString(dr("Document_No"))
    ''            i.Cells("document_date").Value = Convert.ToString(dr("Document_Date"))
    ''            i.Cells("document_type").Value = Convert.ToString(dr("Document_Type"))
    ''            i.Cells("document_amt").Value = Convert.ToString(dr("Document_Amount"))
    ''            i.Cells("vendor_no").Value = Convert.ToString(dr("Vendor_Name"))
    ''            i.Cells("service_type").Value = Convert.ToString(dr("Service_Type"))
    ''            i.Cells("actual_tds_base").Value = Convert.ToString(dr("Actual_TDS_Base"))
    ''            i.Cells("calculated_tds_base").Value = Convert.ToString(dr("Calculated_TDS_Base"))
    ''            i.Cells("actual_tds").Value = Convert.ToString(dr("Actual_TDS"))
    ''            i.Cells("calculated_tds").Value = Convert.ToString(dr("Calculated_TDS"))
    ''            i.Cells("actual_surcharge").Value = Convert.ToString(dr("Actual_Surcharge"))
    ''            i.Cells("calculated_surcharge").Value = Convert.ToString(dr("Calculated_Surcharge"))
    ''            i.Cells("actual_edu_cess").Value = Convert.ToString(dr("Actual_Edu_Cess"))
    ''            i.Cells("calculated_edu_cess").Value = Convert.ToString(dr("Calculated_Edu_Cess"))
    ''            i.Cells("actual_sec_edu_cess").Value = Convert.ToString(dr("Actual_Sec_Educess"))
    ''            i.Cells("calculated_sec_edu_cess").Value = Convert.ToString(dr("Calculated_Sec_Educess"))
    ''            i.Cells("actual_total_tds").Value = Convert.ToString(dr("Actual_Total_TDS"))
    ''            i.Cells("calculated_total_tds").Value = Convert.ToString(dr("Actual_Total_TDS"))
    ''        End While
    ''    End If
    ''    dr.Close()
    ''End Sub

    Private Sub btnvoidremittance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnvoidremittance.Click
        viewtds()
    End Sub

    Public Sub viewtds()
        If fndremittance.Value <> "" And btnsave.Text = "Update" Then
            query = "update TSPL_REMITTANCE set Remit_TDS = 'N',Remittance_Main_code=null  WHERE Remittance_Main_code = '" + fndremittance.Value + "'"
            connectSql.RunSql(query)
            common.clsCommon.MyMessageBoxShow(Me, "Void Remittance Successfully", Me.Text)
            funpostdata()
        Else
            myMessages.blankValue(Me, "Remittance Code", Me.Text)
            fndremittance.Focus()

        End If
    End Sub

    Private Sub funpostdata()
        btnsave.Enabled = False
        btndelete.Enabled = False
        btnpost.Enabled = False
        btnvoidremittance.Enabled = False
        btnsave.Text = "Update"
        btndelete.Text = "Delete"
        btnpost.Text = "Post"
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        closeform()
    End Sub

    Public Sub closeform()
        Me.Close()
    End Sub

    Private Sub Frmremittanceentry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnreset.Enabled Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnpost.Enabled Then
            postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            deletedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.V AndAlso btnvoidremittance.Enabled Then
            viewtds()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso MyBase.isDeleteFlag Then
            Close()

        End If
    End Sub

    Private Sub fndremittance__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndremittance._MYValidating
        Dim qry As String = "select distinct Remittance_Main_code as Code from TSPL_REMITTANCE"
        Dim whrclas As String = "Remit_TDS = 'Y' and len(isnull(Remittance_Main_code ,''))>0"
        fndremittance.Value = clsCommon.ShowSelectForm("Creatrem", qry, "Code", whrclas, fndremittance.Value, "", isButtonClicked)
        RemittanceChanged()
    End Sub

    Private Sub fndbankcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndbankcode._MYValidating
        Dim qry As String = "select BANK_CODE as  Code, DESCRIPTION  from TSPL_BANK_MASTER"
        fndbankcode.Value = clsCommon.ShowSelectForm("CreRfnd", qry, "Code", "", fndbankcode.Value, "", isButtonClicked)
        txtbankdesc.Text = clsDBFuncationality.getSingleValue("select DESCRIPTION from TSPL_BANK_MASTER where BANK_CODE='" + fndbankcode.Value + "'")
    End Sub

    Private Sub fndpaymentcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndpaymentcode._MYValidating
        Dim qry As String = "select Payment_Code as Code, Payment_Desc as [Description], Payment_Type  as [Payment Type]  from TSPL_PAYMENT_CODE"
        fndpaymentcode.Value = clsCommon.ShowSelectForm("CreRemitPid", qry, "Code", "", fndpaymentcode.Value, "", isButtonClicked)
        ''txtbankdesc.Text = clsDBFuncationality.getSingleValue("select Payment_Type from TSPL_PAYMENT_CODE where Payment_Code='" + fndbankcode.Value + "'")
        paymentcodechanged()
    End Sub

    Private Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoSNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNo.FormatString = ""
        repoSNo.HeaderText = "SNo"
        repoSNo.Name = colSNo
        repoSNo.Width = 30
        repoSNo.ReadOnly = True
        repoSNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNo)

        Dim repoDocNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocNo.FormatString = ""
        repoDocNo.HeaderText = "Document No"
        repoDocNo.Name = colDocNo
        repoDocNo.Width = 100
        repoDocNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDocNo)

        Dim repoDocDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocDate.FormatString = ""
        repoDocDate.HeaderText = "Document Date"
        repoDocDate.Name = colDocDate
        repoDocDate.Width = 100
        repoDocDate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDocDate)

        Dim repoDocType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocType.FormatString = ""
        repoDocType.HeaderText = "Document Type"
        repoDocType.Name = colDocType
        repoDocType.Width = 100
        repoDocType.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDocType)

        Dim repoDocAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDocAmt.FormatString = ""
        repoDocAmt.HeaderText = "Amount"
        repoDocAmt.Name = colDocAmt
        repoDocAmt.Width = 100
        repoDocAmt.ReadOnly = True
        repoDocAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDocAmt)

        Dim repoSerType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSerType.FormatString = ""
        repoSerType.HeaderText = "Service Type"
        repoSerType.Name = colServiceType
        repoSerType.Width = 100
        repoSerType.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSerType)

        Dim repoActTDSBase As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoActTDSBase.FormatString = ""
        repoActTDSBase.HeaderText = "Actual TDS Base"
        repoActTDSBase.Name = colActTDSBase
        repoActTDSBase.Width = 100
        repoActTDSBase.ReadOnly = True
        repoActTDSBase.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoActTDSBase)

        Dim repoActTDS As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoActTDS.FormatString = ""
        repoActTDS.HeaderText = "Actual TDS"
        repoActTDS.Name = colActTDS
        repoActTDS.Width = 100
        repoActTDS.ReadOnly = True
        repoActTDS.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoActTDS)

        Dim repoActSurcharge As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoActSurcharge.FormatString = ""
        repoActSurcharge.HeaderText = "Actual Surcharge"
        repoActSurcharge.Name = colActSurcharge
        repoActSurcharge.Width = 100
        repoActSurcharge.ReadOnly = True
        repoActSurcharge.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoActSurcharge)

        Dim repoActEduCess As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoActEduCess.FormatString = ""
        repoActEduCess.HeaderText = "Actual Edu Cess"
        repoActEduCess.Name = colActEduCess
        repoActEduCess.Width = 100
        repoActEduCess.ReadOnly = True
        repoActEduCess.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoActEduCess)

        Dim repoActSecEduCess As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoActSecEduCess.FormatString = ""
        repoActSecEduCess.HeaderText = "Actual Secondry Edu Cess"
        repoActSecEduCess.Name = colActSecEduCess
        repoActSecEduCess.Width = 100
        repoActSecEduCess.ReadOnly = True
        repoActSecEduCess.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoActSecEduCess)

        Dim repoActTotTDS As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoActTotTDS = New GridViewDecimalColumn()
        repoActTotTDS.FormatString = ""
        repoActTotTDS.HeaderText = "Actual Total TDS"
        repoActTotTDS.Name = colActTotTDS
        repoActTotTDS.Width = 100
        repoActTotTDS.ReadOnly = True
        repoActTotTDS.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoActTotTDS)

        Dim repoCalTDSBase As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCalTDSBase.FormatString = ""
        repoCalTDSBase.HeaderText = "Calculated TDS Base"
        repoCalTDSBase.Name = colCalTDSBase
        repoCalTDSBase.Width = 100
        repoCalTDSBase.ReadOnly = True
        repoCalTDSBase.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCalTDSBase)

        Dim repoCalTDS As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCalTDS.FormatString = ""
        repoCalTDS.HeaderText = "Calculated TDS"
        repoCalTDS.Name = colCalTDS
        repoCalTDS.Width = 100
        repoCalTDS.ReadOnly = True
        repoCalTDS.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCalTDS)

        Dim repoCalSurcharge As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCalSurcharge.FormatString = ""
        repoCalSurcharge.HeaderText = "Calual Surcharge"
        repoCalSurcharge.Name = colCalSurcharge
        repoCalSurcharge.Width = 100
        repoCalSurcharge.ReadOnly = True
        repoCalSurcharge.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCalSurcharge)

        Dim repoCalEduCess As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCalEduCess.FormatString = ""
        repoCalEduCess.HeaderText = "Calculated Edu Cess"
        repoCalEduCess.Name = colCalEduCess
        repoCalEduCess.Width = 100
        repoCalEduCess.ReadOnly = True
        repoCalEduCess.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCalEduCess)

        Dim repoCalSecEduCess As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCalSecEduCess.FormatString = ""
        repoCalSecEduCess.HeaderText = "Calculated Secondry Edu Cess"
        repoCalSecEduCess.Name = colCalSecEduCess
        repoCalSecEduCess.Width = 100
        repoCalSecEduCess.ReadOnly = True
        repoCalSecEduCess.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCalSecEduCess)

        Dim repoCalTotTDS As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCalTotTDS.FormatString = ""
        repoCalTotTDS.HeaderText = "Calculated Total TDS"
        repoCalTotTDS.Name = colCalTotTDS
        repoCalTotTDS.Width = 100
        repoCalTotTDS.ReadOnly = True
        repoCalTotTDS.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCalTotTDS)

        Dim repoVendorCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorCode.FormatString = ""
        repoVendorCode.HeaderText = "Vendor Code"
        repoVendorCode.Name = colVendorCode
        repoVendorCode.Width = 100
        repoVendorCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVendorCode)

        Dim repoVendorName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "Vendor Name"
        repoVendorName.Name = colVendorName
        repoVendorName.Width = 100
        repoVendorName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVendorName)


        Dim repoTDSPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTDSPer.FormatString = ""
        repoTDSPer.HeaderText = "TDS %"
        repoTDSPer.Name = colTDSPer
        repoTDSPer.Width = 100
        repoTDSPer.ReadOnly = True
        repoTDSPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTDSPer)

        Dim repoSurPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSurPer.FormatString = ""
        repoSurPer.HeaderText = "Surtax %"
        repoSurPer.Name = colSurPer
        repoSurPer.Width = 100
        repoSurPer.ReadOnly = True
        repoSurPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSurPer)

        Dim repoEduCessPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoEduCessPer.FormatString = ""
        repoEduCessPer.HeaderText = "Edu Cess %"
        repoEduCessPer.Name = colEduCessPer
        repoEduCessPer.Width = 100
        repoEduCessPer.ReadOnly = True
        repoEduCessPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoEduCessPer)

        Dim repoSecEduCessPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSecEduCessPer.FormatString = ""
        repoSecEduCessPer.HeaderText = "Secondary Edu Cess %"
        repoSecEduCessPer.Name = colSecEduCessPer
        repoSecEduCessPer.Width = 100
        repoSecEduCessPer.ReadOnly = True
        repoSecEduCessPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSecEduCessPer)



        Dim repoFiscalYear As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFiscalYear.FormatString = ""
        repoFiscalYear.HeaderText = "Fiscal Year"
        repoFiscalYear.Name = colFiscalYear
        repoFiscalYear.Width = 100
        repoFiscalYear.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoFiscalYear)

        Dim repoQuarter As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQuarter.FormatString = ""
        repoQuarter.HeaderText = "Quarter"
        repoQuarter.Name = colQuarter
        repoQuarter.Width = 100
        repoQuarter.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoQuarter)

        Dim repoDeductionCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDeductionCode.FormatString = ""
        repoDeductionCode.HeaderText = "Deduction Code"
        repoDeductionCode.Name = colDeductionCode
        repoDeductionCode.Width = 100
        repoDeductionCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDeductionCode)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub btnTaxChallan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTaxChallan.Click
        Try

            If fndremittance.Value = "" Then
                common.clsCommon.MyMessageBoxShow(Me, "Select the Remittance Code", Me.Text)
                Exit Sub
            End If


            Dim qry As String = "select distinct TSPL_REMITTANCE_ENTRY.Remittance_Code,convert(varchar(12),TSPL_REMITTANCE_ENTRY.Remittance_Date,103) as RDate,TSPL_REMITTANCE_ENTRY.Section_Code,TSPL_REMITTANCE_ENTRY.Bank_Code,TSPL_REMITTANCE_ENTRY.Amt_To_Remit,reverse(TSPL_REMITTANCE_ENTRY.Amt_To_Remit) AS RevAmt,TSPL_REMITTANCE_ENTRY.Payment_Code,TSPL_REMITTANCE_ENTRY.Cheque_No,convert(varchar(12),TSPL_REMITTANCE_ENTRY.Cheque_Date,103) as ChDate,TSPL_REMITTANCE_ENTRY_DETAIL.Fiscal_Year,substring(TSPL_REMITTANCE_ENTRY_DETAIL.Fiscal_Year,3,12) as Fyear ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Tan_No,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Pincode ,TSPL_BANK_MASTER.DESCRIPTION as Bank from TSPL_REMITTANCE_ENTRY " & _
   " left outer join TSPL_REMITTANCE_ENTRY_DETAIL on TSPL_REMITTANCE_ENTRY.Remittance_Code=TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code " & _
" left outer join TSPL_COMPANY_MASTER on TSPL_REMITTANCE_ENTRY.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " & _
" left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_REMITTANCE_ENTRY.Bank_Code where TSPL_REMITTANCE_ENTRY.Remittance_Code='" + fndremittance.Value + "' "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)


            Dim frmCrystalReportViewer As New frmCrystalReportViewer
            frmCrystalReportViewer.funreport(CrystalReportFolder.TDS, dt, "Taxchallan281", "TDS Tax Challan")


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnUpdateChallanAndBSR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateChallanAndBSR.Click
        Try
            Dim qry As String = "update TSPL_REMITTANCE_ENTRY set BSR_Code='" + txtbsrcode.Text + "',BSR_Name='" + txtbsrname.Text + "',Challan_No='" + txtchallanno.Text + "',Challan_Date='" + clsCommon.GetPrintDate(dtpchallan.Value, "dd/MMM/yyyy") + "' where Remittance_Code='" + fndremittance.Value + "'"
            If (clsDBFuncationality.ExecuteNonQuery(qry)) Then
                common.clsCommon.MyMessageBoxShow(Me, "BSR and Challan Details Updated successfully", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
        
    End Sub
End Class

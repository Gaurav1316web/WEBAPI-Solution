Imports common
Imports System
Imports Telerik.WinControls.UI
Imports System.Net.Mail
Imports System.Net
Imports Telerik.WinControls
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class frmCorrectionforWrongEntry
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim Is_RGP_After_PO As Boolean = False
    Dim arrLoc As String = Nothing
#End Region
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
        'btnPrint.Visible = MyBase.isPrintFlag
        'btncancel.Visible = MyBase.isCancel_Flag_After_Posting
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As New clsGRNHead()
            obj = clsGRNHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.GRN_No) > 0) Then
                txtDocNo.Value = obj.GRN_No
                txtRefNo.Text = obj.Ref_No
                txtBillToLocation.Value = obj.Bill_To_Location
                lblBillToLocation.Text = obj.BillToLocationName
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name
                txtItemCode.Text = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_GRN_DETAIL where GRN_No = '" + obj.GRN_No + "'  ")
                txtItemName.Text = clsDBFuncationality.getSingleValue("select Item_Code from TSPL_GRN_DETAIL where GRN_No = '" + obj.GRN_No + "'  ")
                txtItemType.Text = obj.Item_Type
                txtReqNo.Text = clsDBFuncationality.getSingleValue("select Against_PO from TSPL_GRN_HEAD where GRN_No = '" + obj.GRN_No + "'")
                txtDate.Value = obj.GRN_Date
                txtVehicleNo.Text = obj.VehicleNo
                txtGRNo.Text = obj.GRNo
                txtLRNo.Text = obj.LR_No
                txtChallanNo.Text = obj.Invoiceno
                If obj.InvoiceDate IsNot Nothing AndAlso clsCommon.myLen(obj.InvoiceDate) > 0 AndAlso IsDate(obj.InvoiceDate) Then
                    txtInvoiceDate.Value = obj.InvoiceDate
                End If
                txtGENo.Text = obj.GENo
                'txtGEDate.Value = obj.GEDate
                'If obj.GEDate.HasValue Then
                '    txtGEDate.Value = obj.GEDate
                'End If
                TxtWeighment.Text = clsDBFuncationality.getSingleValue(" Select Weighment_Code from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_PO_WEIGHTMENT_HEAD ON TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No= TSPL_GRN_HEAD.GRN_No where Against_GRN_No = '" + obj.GRN_No + "'  ")
                WeighmetDate.Value = clsDBFuncationality.getSingleValue(" Select Weighment_Date from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_PO_WEIGHTMENT_HEAD ON TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No= TSPL_GRN_HEAD.GRN_No  where Against_GRN_No = '" + obj.GRN_No + "'  ")
                txtMRN.Text = clsDBFuncationality.getSingleValue(" Select MRN_No from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_MRN_HEAD ON TSPL_MRN_HEAD.Against_GRN= TSPL_GRN_HEAD.GRN_No where Against_GRN = '" + obj.GRN_No + "'  ")
                MRNDate.Value = clsDBFuncationality.getSingleValue(" Select MRN_Date from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_MRN_HEAD ON TSPL_MRN_HEAD.Against_GRN= TSPL_GRN_HEAD.GRN_No where Against_GRN = '" + obj.GRN_No + "'  ")
                txtSRN.Text = clsDBFuncationality.getSingleValue("Select SRN_No from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_SRN_HEAD ON TSPL_SRN_HEAD.Against_GRN= TSPL_GRN_HEAD.GRN_No where Against_GRN = '" + obj.GRN_No + "' ")
                obj.SRNNo = txtSRN.Text
                SRNDate.Value = clsDBFuncationality.getSingleValue("Select SRN_Date from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_SRN_HEAD ON TSPL_SRN_HEAD.Against_GRN= TSPL_GRN_HEAD.GRN_No where Against_GRN = '" + obj.GRN_No + "' ")
                txtPINo.Text = clsDBFuncationality.getSingleValue(" Select PI_No from TSPL_PI_HEAD where Against_GRN='" + obj.GRN_No + "'")
                txtPenalty.Text = clsDBFuncationality.getSingleValue(" select Document_No from TSPL_TENDER_PENALTY_DETAIL where SRN_No= '" + obj.SRNNo + "'")
                obj.penalty = txtPenalty.Text
                obj.PINo = txtPINo.Text
                If clsCommon.myLen(obj.penalty) > 0 Then
                    txtDate.ReadOnly = True
                    'clsCommon.MyMessageBoxShow("Penalty Created GrnDate Cannot be changed")
                Else
                    txtDate.ReadOnly = False
                End If
                If obj.PINo IsNot Nothing AndAlso obj.PINo.Count > 0 Then
                    txtInvoiceDate.Enabled = False
                    txtChallanNo.Enabled = False
                Else
                    txtInvoiceDate.Enabled = True
                    txtChallanNo.Enabled = True
                End If
                'TxtWeighment.Text = obj.WeighmentNo
                obj.WeighmentNo = TxtWeighment.Text
                If clsCommon.myLen(obj.WeighmentNo) <= 0 Then
                    MRNDate.ReadOnly = False
                Else
                    MRNDate.ReadOnly = True
                End If
            Else
                AddNew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Dim qry As String = "select TSPL_GRN_HEAD.GRN_No as Code,FORMAT(CAST(GRN_Date AS DATETIME),'dd/MM/yyyy hh:mm tt') AS Date,TSPL_GRN_HEAD.Vendor_Code as [Vendor Code], TSPL_GRN_HEAD.Vendor_Name as Vendor,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],GRN_Total_Amt as Amount,case when TSPL_GRN_HEAD.IsCancel=1 then 'Cancel' when TSPL_GRN_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status],TSPL_GRN_HEAD.Against_PO as [Against PO Code] "
            If Is_RGP_After_PO Then
                qry += ",TSPL_GRN_HEAD.Against_RGP_No as [Against RGP Code] "
            End If
            qry += " ,TSPL_PURCHASE_ORDER_HEAD.RefTendorNo as [Tendor No]
                 ,TSPL_GRN_HEAD.VehicleNo as [Vehicle No]
                 ,case when VisualQCRequired.Is_Visual_QC=0 then 'Not Applicable' when TSPL_GRN_HEAD.VisualQCStatus=1 then 'Ok' when TSPL_GRN_HEAD.VisualQCStatus='2' then 'Not Ok' when TSPL_GRN_HEAD.VisualQCStatus='3' then 'Partial Ok'  when TSPL_GRN_HEAD.VisualQCStatus='4' then 'On Hold' else 'Pending' end as [Visual QC Status]
                 ,case when VisualQCRequired.Is_Visual_QC=0 then 'Not Applicable' when TSPL_GRN_HEAD.VisualQCStatusSecond=1 then 'Ok' when TSPL_GRN_HEAD.VisualQCStatusSecond='2' then 'Not Ok' when TSPL_GRN_HEAD.VisualQCStatusSecond='3' then 'Partial Ok'  when TSPL_GRN_HEAD.VisualQCStatusSecond='4' then 'On Hold' else 'Pending' end as [Visual QC Second Status]
                 from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_GRN_HEAD.Vendor_Code 
                 left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_HEAD.Against_PO 
                left outer join (
                SELECT TSPL_GRN_DETAIL.GRN_No as GRN_No,MAX(TSPL_ITEM_MASTER.Visual_QC) AS Is_Visual_QC FROM TSPL_GRN_DETAIL
                LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE=TSPL_GRN_DETAIL.ITEM_CODE
                GROUP BY TSPL_GRN_DETAIL.GRN_No) as VisualQCRequired on TSPL_GRN_HEAD.grn_no=VisualQCRequired.GRN_No"
            Dim whrClas As String = "  2=2  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrClas += " and TSPL_GRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            LoadData(clsCommon.ShowSelectForm("GRNFND", qry, "Code", whrClas, txtDocNo.Value, "GRN_Date desc", isButtonClicked), NavigatorType.Current)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator

        Try
            Dim qst As String = "select count(*) from TSPL_GRN_HEAD where GRN_No='" + txtDocNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        UpdateD()
    End Sub

    Sub UpdateD()
        Try
            Dim obj As New clsGRNHead()
            Dim vehicleNo As String = txtVehicleNo.Text
            Dim trans As SqlTransaction = Nothing
            obj.GRN_No = txtDocNo.Value

            If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, PurchaseOrder_Date,103) from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No ='" + txtReqNo.Text + "' and isnull(TSPL_PURCHASE_ORDER_HEAD.ISCANCEL,0)=0")) > clsCommon.myCDate(txtDate.Value) Then
                txtDate.Focus()
                clsCommon.MyMessageBoxShow("Date cannot be less than from PO Date")
            End If
            obj.GRN_Date = clsCommon.myCDate(txtDate.Value, "yyyy/MMM/dd hh:mm:ss.ttt")
            obj.VehicleNo = txtVehicleNo.Text
            obj.LR_No = txtLRNo.Text
            obj.Item_Code = txtItemName.Text
            obj.Item_Desc = txtItemCode.Text
            obj.Invoiceno = txtChallanNo.Text
            obj.InvoiceDate = clsCommon.myCDate(txtInvoiceDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
            If obj.InvoiceDate > clsCommon.GETSERVERDATE Then
                clsCommon.MyMessageBoxShow("InvoiceDate should be less than Server Date")
                txtInvoiceDate.Focus()
                Exit Sub
            Else
                obj.InvoiceDate = clsCommon.myCDate(txtInvoiceDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
            End If
            obj.GRNo = txtGRNo.Text
            obj.GENo = txtGENo.Text
            'obj.GEDate = clsCommon.myCDate(txtGEDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
            'If obj.GEDate <= obj.GRN_Date Then
            '    obj.GEDate = clsCommon.myCDate(txtGEDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
            'Else
            '    clsCommon.MyMessageBoxShow("GateEntryDate should be less than or Equalto GRN Date")
            '    txtGEDate.Focus()
            '    Exit Sub
            'End If
            obj.WeighmentNo = TxtWeighment.Text
            obj.WeighmentDate = clsCommon.myCDate(WeighmetDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
            obj.MRNNo = txtMRN.Text
            'obj.MRNDate = clsCommon.myCDate(MRNDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
            obj.MRNDate = clsCommon.myCDate(MRNDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
            obj.PINo = txtPINo.Text
            obj.SRNDate = SRNDate.Value
            If clsCommon.myLen(obj.WeighmentNo) > 0 Then
                If obj.WeighmentDate >= obj.GRN_Date Then
                    obj.WeighmentDate = clsCommon.myCDate(WeighmetDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
                Else
                    clsCommon.MyMessageBoxShow("WeighmentDate should be greater than or Equalto GRN Date")
                    WeighmetDate.Focus()
                    Exit Sub
                End If

                If obj.WeighmentDate < obj.SRNDate Then
                    obj.WeighmentDate = clsCommon.myCDate(WeighmetDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
                Else
                    clsCommon.MyMessageBoxShow("WeighmentDate should be lesser than SRNDate")
                    WeighmetDate.Focus()
                    Exit Sub
                End If
                If clsCommon.myLen(obj.SRNNo) <= 0 Then
                    If obj.WeighmentDate <= clsCommon.GETSERVERDATE() Then
                        obj.WeighmentDate = clsCommon.myCDate(WeighmetDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
                    Else
                        clsCommon.MyMessageBoxShow("WeighmentDate cannot be greater than serverdate")
                        WeighmetDate.Focus()
                        Exit Sub
                    End If
                End If
            End If
            If clsCommon.myLen(obj.WeighmentNo) <= 0 Then
                MRNDate.ReadOnly = False
                If obj.MRNDate >= obj.GRN_Date Then
                    obj.MRNDate = clsCommon.myCDate(MRNDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
                Else
                    clsCommon.MyMessageBoxShow("MRNDate cannot be lesser than GRNdate")
                    MRNDate.Focus()
                    Exit Sub
                End If
                If obj.MRNDate < obj.SRNDate Then
                    obj.MRNDate = clsCommon.myCDate(MRNDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
                Else
                    clsCommon.MyMessageBoxShow("MRNDate cannot be greater than SRNdate")
                    MRNDate.Focus()
                    Exit Sub
                End If
                If clsCommon.myLen(obj.SRNNo) <= 0 Then
                    If obj.MRNDate <= clsCommon.GETSERVERDATE() Then
                        obj.MRNDate = clsCommon.myCDate(MRNDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
                    Else
                        clsCommon.MyMessageBoxShow("SRNDate cannot be greater than serverdate")
                        MRNDate.Focus()
                        Exit Sub
                    End If
                End If
            End If
            If clsGRNHead.UpdateData(obj, trans) Then
                common.clsCommon.MyMessageBoxShow("Data Updated Successfully")
                LoadData(txtDocNo.Value, NavigatorType.Current)
            Else
                common.clsCommon.MyMessageBoxShow("No Updation")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        'txtBillToLocation.Value = ""
        txtChallanNo.Text = ""
        txtDate.Value = Nothing
        txtDocNo.Value = ""
        'txtGEDate.Value = Nothing
        txtGENo.Text = ""
        txtGRNo.Text = ""
        txtInvoiceDate.Value = Nothing
        txtItemCode.Text = ""
        txtItemName.Text = ""
        txtItemType.Text = ""
        txtLRNo.Text = ""
        txtRefNo.Text = ""
        txtVehicleNo.Text = ""
        txtVendorNo.Value = ""
        'lblBillToLocation.Text = ""
        lblVendorName.Text = ""
        txtPINo.Text = ""
        txtMRN.Text = ""
        TxtWeighment.Text = ""
        MRNDate.Value = Nothing
        WeighmetDate.Value = Nothing
        txtReqNo.Text = ""
        txtSRN.Text = ""
        SRNDate.Value = Nothing
    End Sub

    Private Sub frmCorrectionforWrongEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadPageView1.SelectedPage = RadPageViewPage1
        funReset()
        funResetLCF()
        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
        End If
        'If RadPageView1.SelectedPage.Text = "Gate Received Note" Then
        '    btnUpdate.Enabled = True
        'Else
        '    btnUpdate.Enabled = False
        'End If
    End Sub

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                'txtlocationcode.Value = obj.Default_LocCode
                'txtlocationname.Text = obj.Default_LocName
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    ''STD PRODUCTION ENTRY
    Sub LoadDataSPE(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        Dim obj As New clsStanderdProductionEntry
        obj = clsStanderdProductionEntry.GetData(strCode, arrLoc, NavTyep)
        txtCode.Value = obj.PROD_ENTRY_CODE
        Me.dtpDate.Value = obj.PROD_DATE
    End Sub
    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim check As Boolean = False
        check = clsStanderdProductionEntry.CheckValidCode(Me.txtCode.Value)

        If check Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly Or isButtonClicked Then
            txtCode.Value = clsCommon.myCstr(clsStanderdProductionEntry.GetFinder(" TSPL_SPP_PRODUCTION_ENTRY.location_code in (" + arrLoc + ")", txtCode.Value, isButtonClicked))
            LoadDataSPE(txtCode.Value, NavigatorType.Current)
        Else
            funReset()
        End If
    End Sub

    Sub funReset()
        txtCode.Value = Nothing
        dtpDate.Value = Nothing
        LOCATIONRIGTHS()
    End Sub
    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadDataSPE(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub DeleteSPE_Click(sender As Object, e As EventArgs) Handles DeleteSPE.Click
        DeleteDataSPE()
    End Sub

    Sub DeleteDataSPE()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
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
                If (clsStanderdProductionEntry.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub UnpostSPE_Click(sender As Object, e As EventArgs) Handles UnpostSPE.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Select document for unpost.")
            End If

            Dim qry As String = "select count(*) from TSPL_SPP_PRODUCTION_ENTRY where Posted='0' and PROD_ENTRY_CODE='" + txtCode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                Throw New Exception("Current document is not posted.")
            End If

            If common.clsCommon.MyMessageBoxShow("Amend and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' reason for reverse
                Dim Reason As String = ""
                'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Amendment"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Throw New Exception("Fill amendment remarks.")
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If


                If clsStanderdProductionEntry.UnpostData(txtCode.Value, Me.Form_ID) Then
                    '------------------
                    Dim obj As New clsCancelLog
                    obj.Program_Code = Me.Form_ID
                    obj.DOCUMENT_NO = clsCommon.myCstr(txtCode.Value)
                    obj.REASON = Reason
                    obj.ACTIVITY_TYPE = Nothing
                    If clsCancelLog.SaveData(obj, True, Nothing) Then
                        common.clsCommon.MyMessageBoxShow("Successfully Unpost and Recreated", Me.Text)
                        'btnunpost.Visible = False
                        LoadDataSPE(txtCode.Value, NavigatorType.Current)
                    End If
                    '-----------------------------
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub OpenSPE_Click(sender As Object, e As EventArgs) Handles OpenSPE.Click
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmStanderdProductionEntry, txtCode.Value)
    End Sub

    Private Sub btnSPE_Click(sender As Object, e As EventArgs) Handles btnSPE.Click
        txtCode.Value = Nothing
        dtpDate.Value = Nothing
    End Sub

    ''LCF(BILLOFMATERIAL)
    Private Sub TxtCodeLCF__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtCodeLCF._MYValidating
        Dim WhrCls As String = ""
        If clsCommon.myLen(arrLoc) > 0 Then
            WhrCls += " and Location_Code in (" + arrLoc + ")"
        End If

        Dim str As String = "select count(*) from TSPL_MF_BOM_HEAD where BOM_CODE ='" + TxtCodeLCF.Value + "' and trans_type='BOM' " + WhrCls + " "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            TxtCodeLCF.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            TxtCodeLCF.MyReadOnly = True
        End If
        If TxtCodeLCF.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String = "SELECT T1.BOM_CODE AS Code,T1.DESCRIPTION,T1.LOCATION_CODE,T1.BOM_DATE,T1.REVISION_NO,T1.START_DATE,T1.END_DATE,T1.STATUS,"
            qry += " T1.IS_DEFAULT,T1.ATTACHED_DOC,T1.ATTACHED_DOC_PATH,T1.PROD_ITEM_CODE,T2.ITEM_DESC AS PROD_ITEM_NAME,T1.PROD_QUANTITY,T1.PROD_ITEM_UNIT_CODE,"
            qry += " T1.MIN_BATCH_SIZE,T1.MODIFIED_BY AS APPROVED_BY,T1.Created_By FROM TSPL_MF_BOM_HEAD  T1 INNER JOIN TSPL_ITEM_MASTER T2  ON T1.PROD_ITEM_CODE=T2.ITEM_CODE "
            WhrCls = " T1.trans_type='BOM' "
            If clsCommon.myLen(arrLoc) > 0 Then
                WhrCls += " and T1.Location_Code in (" + arrLoc + ")"
            End If
            TxtCodeLCF.Value = clsCommon.ShowSelectForm("TSPL_MF_BOM_HEAD", qry, "Code", WhrCls, TxtCodeLCF.Value, " convert(date, BOM_DATE,103) desc , Code desc ", isButtonClicked)
            If TxtCodeLCF.Value <> "" Then
                LoadDataLCF(TxtCodeLCF.Value, NavigatorType.Current)
            Else
                funResetLCF()
            End If
        End If

    End Sub

    Private Sub TxtCodeLCF__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles TxtCodeLCF._MYNavigator
        Try
            LoadDataLCF(TxtCodeLCF.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadDataLCF(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funResetLCF()
        Dim obj As New clsBillOfMaterial
        obj = clsBillOfMaterial.GetData(strCode, NavTyep)
        TxtCodeLCF.Value = obj.BOM_CODE
        LCFDate.Value = obj.BOM_DATE
    End Sub

    Sub funResetLCF()
        TxtCodeLCF.Value = Nothing
        LCFDate.Value = Nothing
        LOCATIONRIGTHS()
    End Sub


    Private Sub DeleteLCF_Click(sender As Object, e As EventArgs) Handles DeleteLCF.Click
        DeleteDataLCF()
    End Sub

    Sub DeleteDataLCF()
        If clsCommon.myLen(TxtCodeLCF.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDeleteLCF()
    End Sub

    Sub funDeleteLCF()
        Try
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
                If (clsBillOfMaterial.DeleteData(TxtCodeLCF.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funResetLCF()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub btnLCF_Click(sender As Object, e As EventArgs) Handles btnLCF.Click
        TxtCodeLCF.Value = Nothing
        LCFDate.Value = Nothing
    End Sub

    Private Sub UnpostLCF_Click(sender As Object, e As EventArgs) Handles UnpostLCF.Click
        Try
            If clsCommon.myLen(TxtCodeLCF.Value) <= 0 Then
                Throw New Exception("Select document for unpost.")
            End If

            Dim qry As String = "select count(*) from TSPL_MF_BOM_HEAD where Posted='0' and BOM_CODE='" + TxtCodeLCF.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                Throw New Exception("Current document is not posted.")
            End If

            If common.clsCommon.MyMessageBoxShow("Amend and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' reason for reverse
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Amendment"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Throw New Exception("Fill amendment remarks.")
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If

                If clsBillOfMaterial.UnpostData(TxtCodeLCF.Value, Me.Form_ID) Then
                    Dim obj As New clsCancelLog
                    obj.Program_Code = Me.Form_ID
                    obj.DOCUMENT_NO = clsCommon.myCstr(TxtCodeLCF.Value)
                    obj.REASON = Reason
                    obj.ACTIVITY_TYPE = Nothing
                    If clsCancelLog.SaveData(obj, True, Nothing) Then
                        common.clsCommon.MyMessageBoxShow("Successfully Unpost and Recreated", Me.Text)
                        LoadDataLCF(TxtCodeLCF.Value, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub OpenLCF_Click(sender As Object, e As EventArgs) Handles OpenLCF.Click
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBillOfMaterialCosting, TxtCodeLCF.Value)
    End Sub

    ''INTERMEDIATE SALE

    Private Sub txtShipment__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtShipment._MYValidating
        Dim qry As String = "select shipment_No as Code,CONVERT(varchar(10), shipment_Date,103)+' '+ CONVERT(varchar(5), shipment_Date,114) as Date,cust_Code as [Customer Code], cust_Name as Customer,ship_Total_Amt as Amount,case when ispost='0' then 'Pending' else 'Approved' end as [Status],(select top 1 invoice_No from TSPL_SCRAPINVOICE_HEAD where TSPL_SCRAPINVOICE_HEAD.shipment_No=tspl_scrapsale_head.shipment_No) as [Invoice No] from tspl_scrapsale_head"

        Dim whrClas As String = " Doc_Type ='S' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " and loc_code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        LoadDataSH(clsCommon.ShowSelectForm("ScrpCodFiltrFND", qry, "Code", whrClas, txtShipment.Value, "shipment_Date desc", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtShipment__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtShipment._MYNavigator
        Try
            Dim qst As String = "select count(*) from tspl_scrapsale_head where shipment_No='" + txtShipment.Value + "' and Doc_Type='S' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadDataSH(txtShipment.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadDataSH(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As New ClsScrapSaleHead()
        obj = ClsScrapSaleHead.GetData(strCode, NavTyep)
        txtShipment.Value = obj.shipment_No
        dtpshipment.Value = clsCommon.myCDate(obj.shipment_Date)
        lblInvoiceNoS.Text = obj.strInvoiceNo
        lblIRNSale.Text = obj.EInvoiceIRNNo
    End Sub

    Private Sub btnshipment_Click(sender As Object, e As EventArgs) Handles btnshipment.Click
        txtShipment.Value = Nothing
        dtpshipment.Value = Nothing
        lblInvoiceNoS.Text = ""
        lblIRNSale.Text = ""
    End Sub

    Private Sub DeleteShipment_Click(sender As Object, e As EventArgs) Handles DeleteShipment.Click
        funDeleteSH()
    End Sub

    Sub funDeleteSH()
        Try
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
                If (ClsScrapSaleHead.DeleteData(txtShipment.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNewSH()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Sub AddNewSH()
        txtShipment.Value = Nothing
        dtpshipment.Value = Nothing
        lblInvoiceNoS.Text = ""
        lblIRNSale.Text = ""
    End Sub

    Private Sub UnpostShipment_Click(sender As Object, e As EventArgs) Handles UnpostShipment.Click
        Try
            If clsCommon.myLen(txtShipment.Value) <= 0 Then
                Throw New Exception("Document Number not found to do this operation")
            End If
            If clsCommon.myLen(lblInvoiceNoS.Text) <= 0 Then
                Throw New Exception("Invoice Number not found to do this operation")
            End If
            Dim Reason As String = ""
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                '' REASON FOR Reverse 
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                If ClsScrapInvoiceHead.ReverseAndUnpost(txtShipment.Value, lblInvoiceNoS.Text) Then
                    saveCancelLog(Reason, "Reverse And Recreate", Nothing)
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadDataSH(txtShipment.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub OpenShipment_Click(sender As Object, e As EventArgs) Handles OpenShipment.Click
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, txtShipment.Value)
    End Sub

    ''DISPATCH
    Private Sub TxtDispatch__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtDispatch._MYValidating
        Dim strwherecls As String = ""
        strwherecls = Xtra.CustomerPermission()
        Dim qry As String = "select TSPL_SD_SHIPMENT_HEAD.Document_Code as Code,CONVERT(varchar(10), TSPL_SD_SHIPMENT_HEAD.Document_Date,103)+' '+ CONVERT(varchar(5), TSPL_SD_SHIPMENT_HEAD.Document_Date,114) as Date,TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceCode,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as InvoiceDate,TSPL_SD_SALE_INVOICE_HEAD.IRN_No as InvoiceNumber,TSPL_SD_SHIPMENT_HEAD.Customer_Code  as [Customer Code], Customer_Name as Customer,TSPL_SD_SHIPMENT_HEAD.Comments,TSPL_SD_SHIPMENT_HEAD.Total_Amt as Amount,case when TSPL_SD_SHIPMENT_HEAD.Status=0 then 'Pending' else 'Approved' end as [Status],TSPL_USER_MASTER.User_Name as [User Name] from TSPL_SD_SHIPMENT_HEAD
                             left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No left join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code =TSPL_SD_SHIPMENT_HEAD.Created_By left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code "
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + strwherecls + ") "
        ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        ElseIf clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + strwherecls + ")"
        End If
        '-----------------------------------------------------
        LoadDataDispatch(clsCommon.ShowSelectForm("ShipmentCode", qry, "Code", whrClas, TxtDispatch.Value, "Code", isButtonClicked), NavigatorType.Current)
    End Sub

    Sub LoadDataDispatch(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As New clsSNShipmentHead()
        obj = clsSNShipmentHead.GetData(strCode, NavTyep)
        TxtDispatch.Value = obj.Document_Code
        DateDispatch.Value = obj.Document_Date
        obj.Document_Code = TxtDispatch.Value
        txtSaleInvoice.Text = clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where Against_Shipment_No='" + obj.Document_Code + "'")
        DateSI.Value = clsDBFuncationality.getSingleValue("select Document_Date from TSPL_SD_SALE_INVOICE_HEAD where Against_Shipment_No='" + obj.Document_Code + "'")
        lblSI.Text = clsDBFuncationality.getSingleValue("select Against_Shipment_No from TSPL_SD_SALE_INVOICE_HEAD where Against_Shipment_No='" + obj.Document_Code + "'")
        lblIRNSI.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(IRN_No, '') AS IRN_No FROM TSPL_SD_SALE_INVOICE_HEAD WHERE Against_Shipment_No='" + obj.Document_Code + "'")
    End Sub

    Private Sub TxtDispatch__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles TxtDispatch._MYNavigator
        Try
            Dim strwherecls As String = ""
            Dim qst As String = ""
            strwherecls = Xtra.CustomerPermission()
            If clsCommon.myLen(strwherecls) = 0 Then
                qst = "select count(*) from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + TxtDispatch.Value + "'"
            Else
                qst = "select count(*) from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + TxtDispatch.Value + "' and TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + strwherecls + ")"

            End If
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                TxtDispatch.MyReadOnly = False
            Else
                TxtDispatch.MyReadOnly = True
            End If
            LoadDataDispatch(TxtDispatch.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDispatch_Click(sender As Object, e As EventArgs) Handles btnDispatch.Click
        TxtDispatch.Value = Nothing
        DateDispatch.Value = Nothing
        txtSaleInvoice.Text = ""
        DateSI.Value = ""
        lblSI.Text = ""
        lblIRNSI.Text = ""
    End Sub

    Private Sub DeleteDispatch_Click(sender As Object, e As EventArgs) Handles DeleteDispatch.Click
        DeleteDispach()
    End Sub

    Sub DeleteDispach()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, clsCommon.myCstr(TxtDispatch.Value))
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

                If (clsSNShipmentHead.DeleteData(TxtDispatch.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNewDispach()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub AddNewDispach()
        TxtDispatch.Value = Nothing
        DateDispatch.Value = Nothing
    End Sub
    Private Sub UnpostDispatch_Click(sender As Object, e As EventArgs) Handles UnpostDispatch.Click
        Try
            If clsCommon.myLen(TxtDispatch.Value) <= 0 Then
                Throw New Exception("Select document for unpost.")
            End If

            Dim qry As String = "select count(*) from TSPL_SD_SHIPMENT_HEAD where Status='0' and Document_Code='" + TxtDispatch.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                Throw New Exception("Current document is not posted.")
            End If

            If common.clsCommon.MyMessageBoxShow("Amend and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' reason for reverse
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Amendment"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Throw New Exception("Fill amendment remarks.")
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If

                If clsSNShipmentHead.UnpostData(TxtDispatch.Value, Me.Form_ID) Then
                    Dim obj As New clsCancelLog
                    obj.Program_Code = Me.Form_ID
                    obj.DOCUMENT_NO = clsCommon.myCstr(TxtDispatch.Value)
                    obj.REASON = Reason
                    obj.ACTIVITY_TYPE = Nothing
                    If clsCancelLog.SaveData(obj, True, Nothing) Then
                        common.clsCommon.MyMessageBoxShow("Successfully Unpost and Recreated", Me.Text)
                        LoadDataDispatch(TxtDispatch.Value, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub OpenDispatch_Click(sender As Object, e As EventArgs) Handles OpenDispatch.Click
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNShipment, TxtDispatch.Value)
    End Sub

    ''SALE INVOICE
    'Private Sub TxtSI__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtSI._MYValidating
    '    Dim strwherecls As String = ""
    '    strwherecls = Xtra.CustomerPermission()
    '    Dim qry As String = "select Document_Code as Code,CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date,Customer_Code as [Customer Code], Customer_Name as Customer,TSPL_SD_SALE_INVOICE_HEAD.Comments,Total_Amt as Amount,case when TSPL_SD_SALE_INVOICE_HEAD.Status=0 then 'Pending' else 'Approved' end as [Status],Against_Shipment_No as [Shipment No],TSPL_USER_MASTER.User_Name as [User Name] from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code left join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code =TSPL_SD_SALE_INVOICE_HEAD.Created_By"
    '    Dim whrClas As String = ""
    '    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
    '        whrClas = " Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and Invoice_Type in ('T','R') and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + strwherecls + ") "
    '    ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
    '        whrClas = " Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and Invoice_Type in ('T','R')"
    '    ElseIf clsCommon.myLen(strwherecls) > 0 Then
    '        whrClas = " TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + strwherecls + ") and Invoice_Type in ('T','R')"
    '    Else
    '        whrClas = "  Invoice_Type in ('T','R')"
    '    End If

    '    LoadDataSI(clsCommon.ShowSelectForm("ShipmentCofndInvoice", qry, "Code", whrClas, TxtSI.Value, "Code", isButtonClicked), NavigatorType.Current)
    'End Sub

    'Sub LoadDataSI(ByVal strCode As String, ByVal NavTyep As NavigatorType)
    '    Dim obj As New clsSNInvoiceHead()
    '    obj = clsSNInvoiceHead.GetData(strCode, "'T','R'", NavTyep)
    '    TxtSI.Value = obj.Document_Code
    '    DateSI.Value = obj.Document_Date
    '    lblSI.Text = obj.Against_Shipment_No
    '    lblIRNSI.Text = obj.EInvoiceIRNNo
    'End Sub

    'Private Sub TxtSI__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles TxtSI._MYNavigator
    '    Try
    '        Dim strwherecls As String = ""
    '        Dim qst As String = ""
    '        Dim strcondition As String = ""
    '        strwherecls = Xtra.CustomerPermission()
    '        If clsCommon.myLen(strwherecls) > 0 Then
    '            strcondition = "and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + strwherecls + ")"
    '        End If
    '        qst = "select count(*) from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" + TxtSI.Value + "'   and Invoice_Type in ('T','R') " + strcondition + " "

    '        '-----------------------------------------------------
    '        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
    '        If count = 0 Then
    '            TxtSI.MyReadOnly = False
    '        Else
    '            TxtSI.MyReadOnly = True
    '        End If
    '        LoadDataSI(TxtSI.Value, NavType)
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    ''SALE INVOICE
    Private Sub btnSI_Click(sender As Object, e As EventArgs) Handles btnSI.Click
        'TxtSI.Value = Nothing
        txtSaleInvoice.Text = ""
        DateSI.Value = Nothing
        lblSI.Text = ""
        lblIRNSI.Text = ""
    End Sub

    Private Sub DeleteSI_Click(sender As Object, e As EventArgs) Handles DeleteSI.Click
        DeleteDataSI()
    End Sub

    Sub DeleteDataSI()
        Try
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
                If (clsSNInvoiceHead.DeleteData(txtSaleInvoice.Text)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNewSI()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub AddNewSI()
        'TxtSI.Value = Nothing
        txtSaleInvoice.Text = ""
        DateSI.Value = Nothing
        lblSI.Text = ""
        lblIRNSI.Text = ""
    End Sub

    Private Sub UnpostSI_Click(sender As Object, e As EventArgs) Handles UnpostSI.Click
        Try
            If clsCommon.myLen(txtSaleInvoice.Text) <= 0 Then
                Throw New Exception("Select document for unpost.")
            End If

            Dim qry As String = "select count(*) from TSPL_SD_SALE_INVOICE_HEAD where Status='0' and Document_Code='" + TxtSI.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                Throw New Exception("Current document is not posted.")
            End If

            If common.clsCommon.MyMessageBoxShow("Amend and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' reason for reverse
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Amendment"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Throw New Exception("Fill amendment remarks.")
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If

                If clsSNInvoiceHead.UnpostData(txtSaleInvoice.Text, Me.Form_ID) Then
                    Dim obj As New clsCancelLog
                    obj.Program_Code = Me.Form_ID
                    obj.DOCUMENT_NO = clsCommon.myCstr(txtSaleInvoice.Text)
                    obj.REASON = Reason
                    obj.ACTIVITY_TYPE = Nothing
                    If clsCancelLog.SaveData(obj, True, Nothing) Then
                        common.clsCommon.MyMessageBoxShow("Successfully Unpost and Recreated", Me.Text)
                        'LoadDataSI(txtSaleInvoice.Text, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub OpenSI_Click(sender As Object, e As EventArgs) Handles OpenSI.Click
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, txtSaleInvoice.Text)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


End Class
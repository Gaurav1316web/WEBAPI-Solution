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
'Imports MyNamespace


Public Class frmDeletionForEntry
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim Is_RGP_After_PO As Boolean = False
    Private isInsideLoadData As Boolean = False

    Const colCheck As String = "colCheck"
    Const colDocument_No As String = "colDocument_No"
    Const colDocument_Date As String = "colDocument_Date"
    Const colCheckPI As String = "colCheckPI"
    Const colPI_No As String = "colPI_No"
    Const colPI_Date As String = "colPI_Date"
    Dim DocNo As String = Nothing
    Dim DocNoPi As String = Nothing


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

    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Addnew()
            Dim obj As New clsGRNHead()
            obj = clsGRNHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.GRN_No) > 0) Then
                txtDocNo.Value = obj.GRN_No
                txtDate.Value = obj.GRN_Date
                txtBillToLocation.Value = obj.Bill_To_Location
                lblBillToLocation.Text = obj.BillToLocationName
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name
                txtVehicleNo.Text = obj.VehicleNo
                txtItemName.Text = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_GRN_DETAIL where GRN_No = '" + obj.GRN_No + "'  ")
                txtItemCode.Text = clsDBFuncationality.getSingleValue("select Item_Code from TSPL_GRN_DETAIL where GRN_No = '" + obj.GRN_No + "'  ")
                txtRefNo.Text = obj.Ref_No
                txtqc1.Text = clsDBFuncationality.getSingleValue(" select (case when VisualQCStatus=0 then 'Pending' else (case when VisualQCStatus=1 then 'Ok' else (case when VisualQCStatus=2 then 'Not Ok' else (case when VisualQCStatus=3 then 'Partial Ok'else (case when VisualQCStatus=4 then 'On Hold' else  'Under deviation'end) end) end)end)end) as Staus1 from TSPL_GRN_HEAD where GRN_No = '" + obj.GRN_No + "'   ")
                txtqc2.Text = clsDBFuncationality.getSingleValue(" select (case when VisualQCStatusSecond=0 then 'Pending' else (case when VisualQCStatusSecond=1 then 'Ok' else (case when VisualQCStatusSecond=2 then 'Not Ok' else (case when VisualQCStatusSecond=3 then 'Partial Ok'else (case when VisualQCStatusSecond=4 then 'On Hold' else  'Under deviation'end) end) end)end)end) as Staus2 from TSPL_GRN_HEAD where GRN_No = '" + obj.GRN_No + "'  ")
                TxtWeighment.Text = clsDBFuncationality.getSingleValue(" Select Weighment_Code from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_PO_WEIGHTMENT_HEAD ON TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No= TSPL_GRN_HEAD.GRN_No where Against_GRN_No = '" + obj.GRN_No + "'  ")
                WeighmetDate.Value = clsDBFuncationality.getSingleValue(" Select Weighment_Date from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_PO_WEIGHTMENT_HEAD ON TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No= TSPL_GRN_HEAD.GRN_No  where Against_GRN_No = '" + obj.GRN_No + "'  ")
                txttMRN.Text = clsDBFuncationality.getSingleValue(" Select MRN_No from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_MRN_HEAD ON TSPL_MRN_HEAD.Against_GRN= TSPL_GRN_HEAD.GRN_No where Against_GRN = '" + obj.GRN_No + "'  ")
                MRNDatee.Value = clsDBFuncationality.getSingleValue(" Select MRN_Date from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_MRN_HEAD ON TSPL_MRN_HEAD.Against_GRN= TSPL_GRN_HEAD.GRN_No where Against_GRN = '" + obj.GRN_No + "'  ")
                obj.MRNNo = txttMRN.Text
                txtNic.Text = clsDBFuncationality.getSingleValue("select Document_No from TSPL_NIR_QC where MRN_No = '" + obj.MRNNo + "' ")
                NicDate.Value = clsDBFuncationality.getSingleValue("select Document_Date from TSPL_NIR_QC where MRN_No = '" + obj.MRNNo + "' ")
                txtWet.Text = clsDBFuncationality.getSingleValue("select Document_Code from TSPL_QC_CHECK_HEAD where Gate_Entry_No = '" + obj.GRN_No + "'")
                WetDate.Value = clsDBFuncationality.getSingleValue("select Document_Date from TSPL_QC_CHECK_HEAD where Gate_Entry_No = '" + obj.GRN_No + "'")
                txttSRN.Text = clsDBFuncationality.getSingleValue("Select SRN_No from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_SRN_HEAD ON TSPL_SRN_HEAD.Against_GRN= TSPL_GRN_HEAD.GRN_No where Against_GRN = '" + obj.GRN_No + "' ")
                SRNDatee.Value = clsDBFuncationality.getSingleValue("Select SRN_Date from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_SRN_HEAD ON TSPL_SRN_HEAD.Against_GRN= TSPL_GRN_HEAD.GRN_No where Against_GRN = '" + obj.GRN_No + "' ")
                obj.SRNNo = txttSRN.Text
                txtPenalty.Text = clsDBFuncationality.getSingleValue("select TSPL_TENDER_PENALTY_DETAIL.Document_No  from TSPL_TENDER_PENALTY left outer join TSPL_TENDER_PENALTY_DETAIL on TSPL_TENDER_PENALTY_DETAIL.Document_No = TSPL_TENDER_PENALTY.Document_No where SRN_No='" + obj.SRNNo + "'")
                PenaltyDate.Value = clsDBFuncationality.getSingleValue("select TSPL_TENDER_PENALTY.Document_Date  from TSPL_TENDER_PENALTY left outer join TSPL_TENDER_PENALTY_DETAIL on TSPL_TENDER_PENALTY_DETAIL.Document_No = TSPL_TENDER_PENALTY.Document_No where SRN_No='" + obj.SRNNo + "'")
                txtPINo.Text = clsDBFuncationality.getSingleValue(" Select PI_No from TSPL_PI_DETAIL where GRN_ID='" + obj.GRN_No + "'")
                obj.PINo = txtPINo.Text
                PIDate.Value = clsDBFuncationality.getSingleValue("select MAX(PI_Date) from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_DETAIL.PI_No=TSPL_PI_HEAD.PI_No where TSPL_PI_DETAIL.PI_No='" + obj.PINo + "' GROUP BY TSPL_PI_DETAIL.PI_No")
                PIGrid()
                RALGrid()
            Else
                Addnew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
        End Try
    End Sub

    Public Sub RALGrid()
        Try
            If Not String.IsNullOrEmpty(txtPenalty.Text) Then
                Dim qryral As String = ""
                Dim dt As New DataTable()
                qryral = "select distinct TSPL_TENDER_PENALTY_DETAIL.Document_No,Document_Date from TSPL_TENDER_PENALTY_DETAIL
                   left outer join TSPL_TENDER_PENALTY on TSPL_TENDER_PENALTY.Document_No=TSPL_TENDER_PENALTY_DETAIL.Document_No
                   where   TSPL_TENDER_PENALTY.Location_Code='" + txtBillToLocation.Value + "' AND TSPL_TENDER_PENALTY.Item_Code='" + txtItemCode.Text + "' 
                   AND TSPL_TENDER_PENALTY.Vendor_Code='" + txtVendorNo.Value + "'   AND TSPL_TENDER_PENALTY.Tender_No='" + txtRefNo.Text + "' AND TSPL_TENDER_PENALTY_DETAIL.Document_No>='" + txtPenalty.Text + "' order by Document_No desc "

                If clsCommon.myLen(qryral) > 0 Then
                    dt = clsDBFuncationality.GetDataTable(qryral)
                End If

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Gv1.DataSource = Nothing
                    Gv1.GroupDescriptors.Clear()
                    Gv1.SummaryRowsBottom.Clear()
                    Gv1.DataSource = dt
                    Gv1.BestFitColumns()
                    For Each row As DataRow In dt.Rows
                        'Gv1.Rows.AddNew()
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCheck).Value = False
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDocument_No).Value = clsCommon.myCstr(row("Document_No"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDocument_Date).Value = clsCommon.myCstr(row("Document_Date"))
                    Next

                    FormatGrid()
                Else
                    Gv1.DataSource = Nothing
                    Gv1.Rows.Clear()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub PIGrid()
        Try
            If Not String.IsNullOrEmpty(txtPenalty.Text) Then
                Dim qrypi As String = ""
                Dim dt1 As New DataTable()
                qrypi = "SELECT Distinct TSPL_PI_DETAIL.PI_No,PI_Date FROM TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_DETAIL.PI_No = TSPL_PI_HEAD.PI_No
                     WHERE SRN_Id IN (select  DISTINCT SRN_No from TSPL_TENDER_PENALTY_DETAIL
                     left outer join TSPL_TENDER_PENALTY on TSPL_TENDER_PENALTY.Document_No=TSPL_TENDER_PENALTY_DETAIL.Document_No
                     where   TSPL_TENDER_PENALTY.Location_Code='" + txtBillToLocation.Value + "' AND TSPL_TENDER_PENALTY.Item_Code='" + txtItemCode.Text + "' 
                     AND TSPL_TENDER_PENALTY.Vendor_Code='" + txtVendorNo.Value + "'  AND TSPL_TENDER_PENALTY.Tender_No='" + txtRefNo.Text + "' AND TSPL_TENDER_PENALTY_DETAIL.Document_No>='" + txtPenalty.Text + "')order by TSPL_PI_DETAIL.PI_No desc "

                If clsCommon.myLen(qrypi) > 0 Then
                    dt1 = clsDBFuncationality.GetDataTable(qrypi)
                End If

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    Gv2.DataSource = Nothing
                    Gv2.GroupDescriptors.Clear()
                    Gv2.SummaryRowsBottom.Clear()
                    Gv2.DataSource = dt1
                    Gv2.BestFitColumns()

                    For Each row As DataRow In dt1.Rows
                        'Gv2.Rows.AddNew()
                        Gv2.Rows(Gv2.Rows.Count - 1).Cells(colCheckPI).Value = False
                        Gv2.Rows(Gv2.Rows.Count - 1).Cells(colPI_No).Value = clsCommon.myCstr(row("PI_No"))
                        Gv2.Rows(Gv2.Rows.Count - 1).Cells(colPI_Date).Value = clsCommon.myCstr(row("PI_Date"))
                    Next
                    FormatGridGv2()
                Else
                    Gv2.DataSource = Nothing
                    Gv2.Rows.Clear()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Sub FormatGrid()

        Gv1.Rows.Clear()
        Gv1.Columns.Clear()

        Dim repocolCheckBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repocolCheckBox.FormatString = ""
        repocolCheckBox.HeaderText = "Check"
        repocolCheckBox.Name = colCheck
        repocolCheckBox.Width = 100
        repocolCheckBox.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repocolCheckBox)

        Gv1.AllowAddNewRow = False
    End Sub

    Sub FormatGridGv2()
        Gv2.Rows.Clear()
        Gv2.Columns.Clear()

        Dim repocolCheckBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repocolCheckBox.FormatString = ""
        repocolCheckBox.HeaderText = "Check"
        repocolCheckBox.Name = colCheckPI
        repocolCheckBox.Width = 100
        repocolCheckBox.ReadOnly = False
        Gv2.MasterTemplate.Columns.Add(repocolCheckBox)

        Gv2.AllowAddNewRow = False

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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub

    Sub Addnew()
        txtDocNo.Value = ""
        txtDate.Value = Nothing
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        txtVehicleNo.Text = ""
        txtItemCode.Text = ""
        txtItemName.Text = ""
        txtRefNo.Text = ""
        txtqc1.Text = ""
        txtqc2.Text = ""
        TxtWeighment.Text = ""
        WeighmetDate.Value = Nothing
        txttMRN.Text = ""
        MRNDatee.Value = Nothing
        txtNic.Text = ""
        NicDate.Value = Nothing
        txtWet.Text = ""
        WetDate.Value = Nothing
        txttSRN.Text = ""
        SRNDatee.Value = Nothing
        txtPenalty.Text = ""
        PenaltyDate.Value = Nothing
        txtPINo.Text = ""
        PIDate.Value = Nothing
        Gv1.DataSource = Nothing
        Gv2.DataSource = Nothing
        FormatGrid()
        FormatGridGv2()
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    ''GATE RECEIVED NOTE
    Private Sub btnUnpostgrn_Click(sender As Object, e As EventArgs) Handles btnUnpostgrn.Click
        Unpostgrn()
    End Sub

    Sub Unpostgrn()
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsGRNHead.ReverseAndUnpost(txtDocNo.Value)
                    clsCommon.MyMessageBoxShow("Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDeletegrn_Click(sender As Object, e As EventArgs) Handles btnDeletegrn.Click
        DeleteGrn()
    End Sub

    Sub DeleteGrn()
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
                If (clsGRNHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    Addnewgrn()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub GrnOpen_Click(sender As Object, e As EventArgs) Handles GrnOpen.Click
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGRN, txtDocNo.Value)
    End Sub

    Sub Addnewgrn()
        txtDocNo.Value = ""
        txtDate.Value = Nothing
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        txtVehicleNo.Text = ""
        txtItemCode.Text = ""
        txtItemName.Text = ""
        txtRefNo.Text = ""
        txtqc1.Text = ""
        txtqc2.Text = ""
    End Sub

    ''WEIGHMENT
    Private Sub btnDeleteW_Click(sender As Object, e As EventArgs) Handles btnDeleteW.Click
        DeleteWeighment()
    End Sub

    Sub DeleteWeighment()
        Try
            If (myMessages.deleteConfirm()) Then
                clsPOWeighment.DeleteData(TxtWeighment.Text)
                clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
                AddnewW()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnweighmentopen_Click(sender As Object, e As EventArgs) Handles btnweighmentopen.Click
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.POWeighment, TxtWeighment.Text)
    End Sub
    Sub AddnewW()
        TxtWeighment.Text = ""
        WeighmetDate.Value = Nothing
    End Sub

    Private Sub btnUnpostW_Click(sender As Object, e As EventArgs) Handles btnUnpostW.Click
        Try
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            qry = "select  MRN_No from TSPL_MRN_DETAIL where GRN_Id ='" + txtDocNo.Value + "'"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsCommon.MyMessageBoxShow("MRN is created")
                Exit Sub
            End If
            qry = "update TSPL_PO_WEIGHTMENT_HEAD set status=0 where weighment_code='" + TxtWeighment.Text + "' "
            clsDBFuncationality.ExecuteNonQuery(qry)
            clsCommon.MyMessageBoxShow("Tansaction unposted succesffuly", Me.Text)
            LoadData(txtDocNo.Value, NavigatorType.Current)
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    ''MATERIAL RECEIVED NOTE
    Private Sub btnnDeletemrn_Click(sender As Object, e As EventArgs) Handles btnnDeletemrn.Click
        DeleteMRN()
    End Sub

    Sub DeleteMRN()
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
                If (clsMRNHead.DeleteData(txttMRN.Text)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddnewMrn()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub AddnewMrn()
        txttMRN.Text = ""
        MRNDatee.Value = Nothing
    End Sub

    Private Sub Unposttmrn_Click(sender As Object, e As EventArgs) Handles Unposttmrn.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsMRNHead.ReverseAndUnpost(txttMRN.Text)
                    clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub Opennmrn_Click(sender As Object, e As EventArgs) Handles Opennmrn.Click
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnMRN, txttMRN.Text)
    End Sub


    ''NIRQC
    Private Sub DeleteNir_Click(sender As Object, e As EventArgs) Handles DeleteNir.Click
        NIRQC()
    End Sub

    Sub NIRQC()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsNIRQC.DeleteData(txtNic.Text)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddnewNir()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Sub AddnewNir()
        txtNic.Text = ""
        NicDate.Value = Nothing
    End Sub

    Private Sub UnpostNir_Click(sender As Object, e As EventArgs) Handles UnpostNir.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsNIRQC.ReverseAndUnpost(txtNic.Text)
                    clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub OpenNir_Click(sender As Object, e As EventArgs) Handles OpenNir.Click
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.NIRQC, txtNic.Text)
    End Sub

    ''WETQC
    Private Sub DeleteWet_Click(sender As Object, e As EventArgs) Handles DeleteWet.Click
        Try
            If myMessages.deleteConfirm() Then
                If clsQualityCheckForSRNHead.DeleteData(txtWet.Text, "") Then
                    myMessages.delete()
                    addnewWet()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub addnewWet()
        txtWet.Text = ""
        WetDate.Value = Nothing
    End Sub

    Private Sub UnpostWet_Click(sender As Object, e As EventArgs) Handles UnpostWet.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsQualityCheckForSRNHead.ReverseAndUnpost(txtWet.Text) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub OpenWet_Click(sender As Object, e As EventArgs) Handles OpenWet.Click
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmQualityCheckForSRN, txtWet.Text)
    End Sub

    ''STORE RECEIVED NOTE

    Private Sub DeleteeSrn_Click(sender As Object, e As EventArgs) Handles DeleteeSrn.Click
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
                If (clsSRNHead.DeleteData(txttSRN.Text)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddnewSRN()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub AddnewSRN()
        txttSRN.Text = ""
        SRNDatee.Value = Nothing
    End Sub

    Private Sub Unposttsrn_Click(sender As Object, e As EventArgs) Handles Unposttsrn.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                If clsSRNHead.ReverseAndUnpost(txttSRN.Text) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Opennsrn_Click(sender As Object, e As EventArgs) Handles Opennsrn.Click
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, txttSRN.Text)
    End Sub

    ''RAL PENALTY
    Private Sub DeleteRal_Click(sender As Object, e As EventArgs) Handles DeleteRal.Click
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
                If (clsTenderPenalty.DeleteData(Gv1.CurrentRow.Cells(1).Value.ToString())) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    RALGrid()
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub Unpostral_Click(sender As Object, e As EventArgs) Handles Unpostral.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsTenderPenalty.ReverseAndUnpost(Gv1.CurrentRow.Cells(1).Value.ToString())
                    clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Openral_Click(sender As Object, e As EventArgs) Handles Openral.Click
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.TenderShortPenalty, Gv1.CurrentRow.Cells(1).Value.ToString())
    End Sub

    ''PURCHASE INVOICE
    Private Sub Deletepi_Click(sender As Object, e As EventArgs) Handles Deletepi.Click
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
                If (clsPurchaseInvoiceHead.DeleteData(Gv2.CurrentRow.Cells(1).Value.ToString())) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    PIGrid()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Unpostpi_Click(sender As Object, e As EventArgs) Handles Unpostpi.Click
        Try
            If clsCommon.MyMessageBoxShow("Do you want to Reverse and unpost the current Document" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '' reason for reverse
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If

                If clsPurchaseInvoiceHead.ReverseAndUnpost(Gv2.CurrentRow.Cells(1).Value.ToString()) Then
                    saveCancelLog(Reason, "Reverse and Recreate", Nothing)
                    clsCommon.MyMessageBoxShow(Me, "Transaction unposted succesffuly", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Openpi_Click(sender As Object, e As EventArgs) Handles Openpi.Click
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseInvoice, Gv2.CurrentRow.Cells(1).Value.ToString())
    End Sub

    Private Sub frmDeletionForEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
        End If
        FormatGrid()
        FormatGridGv2()
    End Sub


End Class
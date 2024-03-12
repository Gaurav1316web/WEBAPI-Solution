Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class FrmSecondarySettingForQC
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim AllowRandomOnlyOneSecondaryQC As Integer = 0
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim isFlag As Boolean = False
    Public Const colSlNo As String = "SLNO"
    Public Const colQCFATPER As String = "colQCFATPER"
    Public Const colQCSNFPER As String = "colQCSNFPER"
    Public Const colFATPER As String = "colFATPER"
    Public Const colSNFPER As String = "colSNFPER"
    Public Const colNETWEIGHT As String = "colNETWEIGHT"
    Public Const colADDWEIGHTPER As String = "colADDWEIGHTPER"
    Public Const colCALCULATEDADDWEIGHT As String = "colCALCULATEDADDWEIGHT"
    Public Const colTOTALWEIGHT As String = "colTOTALWEIGHT"
    Dim arrLoc As String = Nothing
    Public Shared Alocation As String = Nothing
    Public Const colChamberDesc As String = "colChamberDesc"
    Public DocumentNo As String = ""
    Dim Qry As String
#End Region
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmSecondarySettingForQC_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub FrmSecondarySettingForQC_Load(sender As Object, e As EventArgs) Handles Me.Load
        AllowRandomOnlyOneSecondaryQC = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowRandomOnlyOneSecondaryQC, clsFixedParameterCode.AllowRandomOnlyOneSecondaryQC, Nothing))
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N New Transaction")
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.SecondarySettingForQC)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        btnReverse.Visible = False
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If

    End Sub
    Sub Reset()
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
       
        txtDocNo.MyReadOnly = False
        FndQCNo.Value = ""
        fndTankerNo.Value = ""
        fndGateEntryNo.Value = ""
        dtpQCInDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        dtpQCOutDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        dtpGateEntryDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        txtChallanNo.Text = ""
        txtDipValue.Text = ""
        dtpChallanDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")

        lblStatusValue.Text = ""
        txtWeighmentNo.Text = ""
        dtpWeighmentDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        fndVendor.Value = ""
        lblVendorName.Text = ""
        fndLocation.Value = ""
        lblLocationName.Text = ""

        TxtDeductionAmount.Value = 0
        TxtDeductionAmount.Enabled = False
        ''-----------------------------
        loadBlankItemGrid()
        lblPending.Status = ERPTransactionStatus.Pending
        txtDispControlSampleFAT.Text = ""
        txtDispControlSampleSNF.Text = ""
        txtRcptControlSampleFAT.Text = ""
        txtRcptControlSampleSNF.Text = ""

        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
        If DateTime = "1" Then
            txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpQCInDateTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpQCOutDateTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpGateEntryDateTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            txtDate.CustomFormat = "dd/MM/yyyy"
            dtpQCInDateTime.CustomFormat = "dd/MM/yyyy"
            dtpQCOutDateTime.CustomFormat = "dd/MM/yyyy"
            dtpGateEntryDateTime.CustomFormat = "dd/MM/yyyy"
        End If

        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnsave.Enabled = True
        isNewEntry = True
        LOCATIONRIGTHS()
    End Sub
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try

            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                'lblLocationCode.Value = obj.Default_LocCode
                'LblLocationName.Text = obj.Default_LocName
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
                Alocation = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub
    Sub loadBlankItemGrid()

        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing

        Dim lineNo As New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "SL. No."
        lineNo.Name = colSlNo
        lineNo.Width = 60
        lineNo.ReadOnly = True
        lineNo.WrapText = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(lineNo)


        Dim ChamberDesc As New GridViewTextBoxColumn()
        ChamberDesc.FormatString = ""
        ChamberDesc.HeaderText = "Chamber Desc"
        ChamberDesc.Name = colChamberDesc
        ChamberDesc.Width = 60
        ChamberDesc.ReadOnly = True
        ChamberDesc.WrapText = True
        ChamberDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(ChamberDesc)
       

        Dim QCFATPER As New GridViewDecimalColumn
        QCFATPER.FormatString = "{0:n2}"
        QCFATPER.HeaderText = "QC FAT %"
        QCFATPER.Name = colQCFATPER
        QCFATPER.Width = 75
        QCFATPER.ReadOnly = True
        QCFATPER.WrapText = True
        QCFATPER.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(QCFATPER)

        Dim QCSNFPER As New GridViewDecimalColumn
        QCSNFPER.FormatString = "{0:n2}"
        QCSNFPER.HeaderText = "QC SNF %"
        QCSNFPER.Name = colQCSNFPER
        QCSNFPER.Width = 75
        QCSNFPER.ReadOnly = True
        QCSNFPER.WrapText = True
        QCSNFPER.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(QCSNFPER)

        Dim fatper As New GridViewDecimalColumn
        fatper.FormatString = "{0:n2}"
        fatper.HeaderText = "FAT %"
        fatper.Name = colFATPER
        fatper.Width = 75
        fatper.ReadOnly = False
        fatper.WrapText = True
        fatper.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(fatper)

        Dim snfper As New GridViewDecimalColumn
        snfper.FormatString = "{0:n2}"
        snfper.HeaderText = "SNF %"
        snfper.Name = colSNFPER
        snfper.Width = 75
        snfper.ReadOnly = False
        snfper.WrapText = True
        snfper.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(snfper)

        Dim NETWEIGHT As New GridViewDecimalColumn
        NETWEIGHT.FormatString = "{0:n3}"
        NETWEIGHT.HeaderText = "NET WEIGHT"
        NETWEIGHT.DecimalPlaces = 3
        NETWEIGHT.Name = colNETWEIGHT
        NETWEIGHT.Width = 130
        NETWEIGHT.ReadOnly = True
        NETWEIGHT.WrapText = True
        NETWEIGHT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(NETWEIGHT)



        Dim ADDWEIGHTPER As New GridViewDecimalColumn
        ADDWEIGHTPER.FormatString = "{0:n2}"
        ADDWEIGHTPER.HeaderText = "ADDITIONAL WEIGHT %"
        ADDWEIGHTPER.Name = colADDWEIGHTPER
        ADDWEIGHTPER.Width = 180
        ADDWEIGHTPER.ReadOnly = True
        ADDWEIGHTPER.WrapText = True
        ADDWEIGHTPER.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(ADDWEIGHTPER)

        ''=================================================
        Dim CALCULATEDADDWEIGHT As New GridViewDecimalColumn
        CALCULATEDADDWEIGHT.FormatString = "{0:n2}"
        CALCULATEDADDWEIGHT.HeaderText = "CALCULATED ADDITIONAL WEIGHT"
        CALCULATEDADDWEIGHT.Name = colCALCULATEDADDWEIGHT
        CALCULATEDADDWEIGHT.Width = 180
        CALCULATEDADDWEIGHT.ReadOnly = True
        CALCULATEDADDWEIGHT.WrapText = True
        CALCULATEDADDWEIGHT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(CALCULATEDADDWEIGHT)

        Dim TOTALWEIGHT As New GridViewDecimalColumn
        TOTALWEIGHT.FormatString = "{0:n2}"
        TOTALWEIGHT.HeaderText = "TOTAL WEIGHT"
        TOTALWEIGHT.Name = colTOTALWEIGHT
        TOTALWEIGHT.Width = 180
        TOTALWEIGHT.ReadOnly = True
        TOTALWEIGHT.WrapText = True
        TOTALWEIGHT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(TOTALWEIGHT)

      
        gv1.Rows.AddNew()
        gv1.Rows(0).Cells(colSlNo).Value = "1"
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.AllowRowReorder = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = False
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        gv1.AllowColumnReorder = True
        ' gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        ' gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

    End Sub
    Private Function AllowToSave() As Boolean
        Try
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If
            If clsCommon.myLen(FndQCNo.Value) <= 0 Then
                FndQCNo.Focus()
                Throw New Exception("QC No cannot be left blank")
            End If

            For i As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(i).Cells(colFATPER).Value) <= 0 Then
                    Throw New Exception("Fat% cannot be left blank")
                End If
                If clsCommon.myCdbl(gv1.Rows(i).Cells(colFATPER).Value) = 0 Then
                    Throw New Exception("Fat% cannot be zero")
                End If
                If clsCommon.myLen(gv1.Rows(i).Cells(colSNFPER).Value) <= 0 Then
                    Throw New Exception("SNF% cannot be left blank")
                End If
                If clsCommon.myCdbl(gv1.Rows(i).Cells(colSNFPER).Value) = 0 Then
                    Throw New Exception("SNF% cannot be zero")
                End If
            Next
            Return True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Sub SaveData()
        Dim obj As New ClsSecondarySettingForQC()
        Dim objTr As New ClsSecondarySettingForQCDetail
        Try
            If AllowToSave() Then

                obj.Document_No = clsCommon.myCstr(txtDocNo.Value)
                obj.Document_Date = clsCommon.myCDate(txtDate.Value)
                obj.QC_No = clsCommon.myCstr(FndQCNo.Value)
                obj.QC_In_Date_Time = clsCommon.myCDate(dtpQCInDateTime.Value)
                obj.QC_Out_Date_Time = clsCommon.myCDate(dtpQCOutDateTime.Value)
                obj.Gate_Entry_No = clsCommon.myCstr(fndGateEntryNo.Value)
                obj.Gate_Entry_Date_And_Time = clsCommon.myCDate(dtpGateEntryDateTime.Value)
                obj.Challan_No = clsCommon.myCstr(txtChallanNo.Text)
                obj.Challan_Date = clsCommon.myCDate(dtpChallanDate.Value)
                obj.Weighment_No = clsCommon.myCstr(txtWeighmentNo.Text)
                obj.Weighment_Date = clsCommon.myCDate(dtpWeighmentDate.Value)
                obj.Tanker_No = clsCommon.myCstr(fndTankerNo.Value)
                obj.location_Code = clsCommon.myCstr(fndLocation.Value)
                obj.Vendor_Code = clsCommon.myCstr(fndVendor.Value)
                obj.Vendor_Desc = clsCommon.myCstr(lblVendorName.Text)
                obj.Location_Desc = clsCommon.myCstr(lblLocationName.Text)
                obj.Dip_Value = clsCommon.myCdbl(txtDipValue.Text)
                obj.Receipt_Control_FAT = clsCommon.myCdbl(txtRcptControlSampleFAT.Text)
                obj.Receipt_Control_SNF = clsCommon.myCdbl(txtRcptControlSampleSNF.Text)
                obj.Dispatch_Control_FAT = clsCommon.myCdbl(txtDispControlSampleFAT.Text)
                obj.Dispatch_Control_SNF = clsCommon.myCdbl(txtDispControlSampleSNF.Text)
                obj.arrSecondarySettingDetail = New List(Of ClsSecondarySettingForQCDetail)


                For Each grow As GridViewRowInfo In gv1.Rows
                    objTr = New ClsSecondarySettingForQCDetail()
                    objTr.LINE_NO = clsCommon.myCdbl(grow.Cells(colSlNo).Value)
                    objTr.CHAMBER_DESC = clsCommon.myCstr(grow.Cells(colChamberDesc).Value)
                    objTr.Document_No = clsCommon.myCstr(obj.Document_No)
                    objTr.QCFatPer = clsCommon.myCstr(grow.Cells(colQCFATPER).Value)
                    objTr.QCSNFPer = clsCommon.myCstr(grow.Cells(colQCSNFPER).Value)
                    objTr.FatPer = clsCommon.myCdbl(grow.Cells(colFATPER).Value)
                    objTr.SNFPer = clsCommon.myCdbl(grow.Cells(colSNFPER).Value)

                    objTr.NetWeight = clsCommon.myCdbl(grow.Cells(colNETWEIGHT).Value)
                    objTr.AdditinalWeightper = clsCommon.myCdbl(grow.Cells(colADDWEIGHTPER).Value)
                    objTr.CalculatedAdditionalWeight = clsCommon.myCdbl(grow.Cells(colCALCULATEDADDWEIGHT).Value)
                    objTr.TotalWeight = clsCommon.myCdbl(grow.Cells(colTOTALWEIGHT).Value)
                    obj.arrSecondarySettingDetail.Add(objTr)
                Next


                If (ClsSecondarySettingForQC.SaveData(obj, isNewEntry)) Then
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                        LoadData(obj.Document_No, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
            objTr = Nothing
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim dt As DataTable = Nothing
        Dim obj As ClsSecondarySettingForQC = Nothing
        Try
            obj = ClsSecondarySettingForQC.GetData(strCode, arrLoc, NavTyep)

            isInsideLoadData = True
            Reset()
            If obj IsNot Nothing Then
                isNewEntry = False

                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
               
                FndQCNo.Value = obj.QC_No
                dtpQCInDateTime.Value = obj.QC_In_Date_Time
                dtpQCOutDateTime.Value = obj.QC_Out_Date_Time
                fndGateEntryNo.Value = obj.Gate_Entry_No
                dtpGateEntryDateTime.Value = obj.Gate_Entry_Date_And_Time
                txtChallanNo.Text = obj.Challan_No

                dtpChallanDate.Value = obj.Challan_Date
                txtWeighmentNo.Text = obj.Weighment_No
                dtpWeighmentDate.Value = obj.Weighment_Date
                fndTankerNo.Value = obj.Tanker_No
                fndLocation.Value = obj.location_Code
                fndVendor.Value = obj.Vendor_Code

                lblVendorName.Text = obj.Vendor_Desc
                lblLocationName.Text = obj.Location_Desc
                txtDipValue.Text = obj.Dip_Value
                txtRcptControlSampleFAT.Text = obj.Receipt_Control_FAT
                txtRcptControlSampleSNF.Text = obj.Receipt_Control_SNF
                txtDispControlSampleFAT.Text = obj.Dispatch_Control_FAT
                txtDispControlSampleSNF.Text = obj.Dispatch_Control_SNF

                If obj.arrSecondarySettingDetail IsNot Nothing AndAlso obj.arrSecondarySettingDetail.Count > 0 Then
                    For Each objTr As ClsSecondarySettingForQCDetail In obj.arrSecondarySettingDetail
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = objTr.LINE_NO
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.CHAMBER_DESC
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQCFATPER).Value = objTr.QCFatPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQCSNFPER).Value = objTr.QCSNFPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPER).Value = objTr.FatPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPER).Value = objTr.SNFPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colNETWEIGHT).Value = objTr.NetWeight
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colADDWEIGHTPER).Value = objTr.AdditinalWeightper
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCALCULATEDADDWEIGHT).Value = objTr.CalculatedAdditionalWeight
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTOTALWEIGHT).Value = objTr.TotalWeight
                        gv1.Rows.AddNew()
                    Next
                    gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                Else
                    gv1.DataSource = Nothing
                End If
                txtDocNo.MyReadOnly = True
                btnsave.Text = "Update"
               
                If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                    btnPost.Enabled = False
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    lblPending.Status = ERPTransactionStatus.Approved
                Else
                    btnPost.Enabled = True
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                    lblPending.Status = ERPTransactionStatus.Pending
                End If
            Else
                Reset()

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            dt = Nothing
            obj = Nothing
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            isFlag = True
            Dim intWeighmentPost As Integer = 0
            If (myMessages.postConfirm()) Then
                If clsCommon.myLen(txtWeighmentNo.Text) > 0 Then
                    intWeighmentPost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isPosted from TSPL_Weighment_Detail where Weighment_No='" & txtWeighmentNo.Text & "'"))
                End If
                SaveData()
                If (ClsSecondarySettingForQC.PostData(MyBase.Form_ID, arrLoc, txtDocNo.Value)) Then
                    If intWeighmentPost = 1 Then
                        Dim frm As New FrmBulkMilkSRN
                        frm.SetUserMgmt(clsUserMgtCode.frmBulkMilkSRN)
                        frm.Show()
                        frm.fndWeighmentNo.Value = txtWeighmentNo.Text
                        frm.loadWeighmentData(txtWeighmentNo.Text)
                        frm.SaveData(False, True)
                        Dim SRNNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select SRN_NO from TSPL_Bulk_MILK_SRN where Weighment_No='" & txtWeighmentNo.Text & "'"))
                        If clsCommon.myLen(SRNNo) > 0 Then
                            clsBulkMilkSRN.postData(SRNNo, "M-SRN-B")
                            common.clsCommon.MyMessageBoxShow(Me, "Secondary QC Successfully posted", Me.Text)
                        Else
                            Dim qry = "Update TSPL_SECONDARY_SETTING_QC_HEAD set Posted=0 where Document_No='" + txtDocNo.Value + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry)
                            common.clsCommon.MyMessageBoxShow("Secondary QC not posted.Please map Price chart for SRN.", Me.Text)
                        End If
                        frm.Close()
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Secondary QC Successfully posted", Me.Text)
                    End If
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
        End Try
    End Sub
    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (ClsSecondarySettingForQC.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub
    Private Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_SECONDARY_SETTING_QC_HEAD where Document_No='" + txtDocNo.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocNo.MyReadOnly = False
            End If

            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        ' Dim qry As String = "Select TSPL_Dispatch_BulkSale.Document_No as Code,Convert(varchar,TSPL_Dispatch_BulkSale.Document_Date,103) as [Dispatch Date],TSPL_Dispatch_BulkSale.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_Dispatch_BulkSale.Tanker_Code as [Tanker Code],TSPL_Dispatch_BulkSale.QC_Code as [QC Code],TSPL_Dispatch_BulkSale.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_Dispatch_BulkSale.Price_Code as [Price Code],TSPL_Dispatch_BulkSale.Dip_marking as [Dip Marking],TSPL_Dispatch_BulkSale.Challan_No as [Challan No],case when TSPL_Dispatch_BulkSale.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_Dispatch_BulkSale left outer Join TSPL_CUSTOMER_MASTER on TSPL_Dispatch_BulkSale.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_Dispatch_BulkSale.Location_Code =TSPL_LOCATION_MASTER.Location_Code"
        Dim qry As String = " Select TSPL_SECONDARY_SETTING_QC_HEAD.Document_No as [Code],Convert(varchar,TSPL_SECONDARY_SETTING_QC_HEAD.Document_Date,103) as [Date],TSPL_SECONDARY_SETTING_QC_HEAD.QC_No,Gate_Entry_No as [Gate Entry No],Weighment_No as [Weighment No] ,case when TSPL_SECONDARY_SETTING_QC_HEAD.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_SECONDARY_SETTING_QC_HEAD"
        txtDocNo.Value = clsCommon.ShowSelectForm("QCSecondarysetting", qry, "Code", " TSPL_SECONDARY_SETTING_QC_HEAD.Location_Code in (" + arrLoc + ")", txtDocNo.Value, "TSPL_SECONDARY_SETTING_QC_HEAD.Document_Date desc", isButtonClicked)
        LoadData(txtDocNo.Value, NavigatorType.Current)
        qry = Nothing
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Reset()
    End Sub

    Private Sub FndQCNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndQCNo._MYValidating
        Dim strwhrclause As String = ""
        Dim qry As String = ""
        If AllowRandomOnlyOneSecondaryQC = 0 Then
            qry = " Select DISTINCT TSPL_QUALITY_CHECK.QC_No AS Code,TSPL_QUALITY_CHECK.QC_In_Date_Time,TSPL_QUALITY_CHECK.QC_Out_Date_Time,TSPL_QUALITY_CHECK.Gate_Entry_No ,TSPL_QUALITY_CHECK.Challan_No,TSPL_QUALITY_CHECK.Challan_Date,TSPL_QUALITY_CHECK.Tanker_No ,TSPL_QUALITY_CHECK.location_Code,TSPL_QUALITY_CHECK.Location_Desc,TSPL_QUALITY_CHECK.Vendor_Code,TSPL_QUALITY_CHECK.Vendor_Desc ,TSPL_QUALITY_CHECK.Dip_Value,TSPL_QUALITY_CHECK.Weighment_No,TSPL_QUALITY_CHECK.Weighment_Date ,TSPL_QUALITY_CHECK.DeductionAmount,TSPL_QUALITY_CHECK.Receipt_Control_FAT ,TSPL_QUALITY_CHECK.Receipt_Control_SNF  from TSPL_QUALITY_CHECK left outer join TSPL_QUALITY_CHEMBER_DETAILS on TSPL_QUALITY_CHEMBER_DETAILS.QC_No=TSPL_QUALITY_CHECK.QC_No  left outer join TSPL_MILK_GRADE_MASTER on  TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE=TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE  "
            ' Dim strwhrclause As String = " TSPL_QUALITY_CHECK.location_Code in (" + arrLoc + ") and TSPL_QUALITY_CHECK.Doc_Type ='BulkProc' and TSPL_QUALITY_CHECK.isPosted ='1' and TSPL_MILK_GRADE_MASTER.GRADE_TYPE in ('A','A+','A++') and TSPL_QUALITY_CHECK.QC_No not in (Select QC_No from TSPL_SECONDARY_SETTING_QC_HEAD where Document_No<>'" + txtDocNo.Value + "' and TSPL_SECONDARY_SETTING_QC_HEAD.Location_Code in (" + arrLoc + ") ) "
            ' Changed By Prabhakar 24/11/2016
            strwhrclause = " TSPL_QUALITY_CHECK.location_Code in (" + arrLoc + ") and TSPL_QUALITY_CHECK.Doc_Type ='BulkProc' and is_Param_Accepted <> 0  and TSPL_QUALITY_CHECK.isPosted ='1'  and TSPL_QUALITY_CHECK.QC_No not in (Select QC_No from TSPL_SECONDARY_SETTING_QC_HEAD where Document_No<>'" + txtDocNo.Value + "' and TSPL_SECONDARY_SETTING_QC_HEAD.Location_Code in (" + arrLoc + ") ) "
            FndQCNo.Value = clsCommon.ShowSelectForm("QCSecondarysettingfinder", qry, "Code", strwhrclause, FndQCNo.Value, "", isButtonClicked)
        Else
            qry = " Select top 1 TSPL_QUALITY_CHECK.QC_No AS Code,TSPL_QUALITY_CHECK.QC_In_Date_Time,TSPL_QUALITY_CHECK.QC_Out_Date_Time,TSPL_QUALITY_CHECK.Gate_Entry_No ,TSPL_QUALITY_CHECK.Challan_No,TSPL_QUALITY_CHECK.Challan_Date,TSPL_QUALITY_CHECK.Tanker_No ,TSPL_QUALITY_CHECK.location_Code,TSPL_QUALITY_CHECK.Location_Desc,TSPL_QUALITY_CHECK.Vendor_Code,TSPL_QUALITY_CHECK.Vendor_Desc ,TSPL_QUALITY_CHECK.Dip_Value,TSPL_QUALITY_CHECK.Weighment_No,TSPL_QUALITY_CHECK.Weighment_Date ,TSPL_QUALITY_CHECK.DeductionAmount,TSPL_QUALITY_CHECK.Receipt_Control_FAT ,TSPL_QUALITY_CHECK.Receipt_Control_SNF  from TSPL_QUALITY_CHECK left outer join TSPL_QUALITY_CHEMBER_DETAILS on TSPL_QUALITY_CHEMBER_DETAILS.QC_No=TSPL_QUALITY_CHECK.QC_No  left outer join TSPL_MILK_GRADE_MASTER on  TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE=TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE  "
            ' Dim strwhrclause As String = " TSPL_QUALITY_CHECK.location_Code in (" + arrLoc + ") and TSPL_QUALITY_CHECK.Doc_Type ='BulkProc' and TSPL_QUALITY_CHECK.isPosted ='1' and TSPL_MILK_GRADE_MASTER.GRADE_TYPE in ('A','A+','A++') and TSPL_QUALITY_CHECK.QC_No not in (Select QC_No from TSPL_SECONDARY_SETTING_QC_HEAD where Document_No<>'" + txtDocNo.Value + "' and TSPL_SECONDARY_SETTING_QC_HEAD.Location_Code in (" + arrLoc + ") ) "
            ' Changed By Prabhakar 24/11/2016
            strwhrclause = " TSPL_QUALITY_CHECK.location_Code in (" + arrLoc + ") and TSPL_QUALITY_CHECK.Doc_Type ='BulkProc' and is_Param_Accepted <> 0  and TSPL_QUALITY_CHECK.isPosted ='1'  and TSPL_QUALITY_CHECK.QC_No not in (Select QC_No from TSPL_SECONDARY_SETTING_QC_HEAD where Document_No<>'" + txtDocNo.Value + "' and TSPL_SECONDARY_SETTING_QC_HEAD.Location_Code in (" + arrLoc + ") ) "
            FndQCNo.Value = clsCommon.ShowSelectForm("QCSecondarysettingfinder", qry, "Code", strwhrclause, FndQCNo.Value, "NEWID()", isButtonClicked)
        End If

        isInsideLoadData = True
        GetTableValue()
        isInsideLoadData = False
        strwhrclause = Nothing
    End Sub
    Private Sub GetTableValue()
        Dim dt As DataTable = Nothing
        Qry = " Select TSPL_QUALITY_CHEMBER_DETAILS.Line_No,TSPL_QUALITY_CHECK.Gate_Entry_Date_And_Time,TSPL_QUALITY_CHEMBER_DETAILS.Chamber_Desc,TSPL_QUALITY_CHECK.QC_No ,TSPL_QUALITY_CHECK.QC_In_Date_Time,TSPL_QUALITY_CHECK.QC_Out_Date_Time,TSPL_QUALITY_CHECK.Gate_Entry_No ,TSPL_QUALITY_CHECK.Challan_No,TSPL_QUALITY_CHECK.Challan_Date,TSPL_QUALITY_CHECK.Tanker_No ,TSPL_QUALITY_CHECK.location_Code,TSPL_QUALITY_CHECK.Location_Desc,TSPL_QUALITY_CHECK.Vendor_Code,TSPL_QUALITY_CHECK.Vendor_Desc ,TSPL_QUALITY_CHECK.Dip_Value,TSPL_QUALITY_CHECK.Weighment_No,TSPL_QUALITY_CHECK.Weighment_Date ,TSPL_QUALITY_CHECK.DeductionAmount,TSPL_QUALITY_CHECK.Receipt_Control_FAT ,TSPL_QUALITY_CHECK.Receipt_Control_SNF,TSPL_QUALITY_CHEMBER_DETAILS.Chamber_Qty AS [NetWeight],TSPL_QUALITY_CHEMBER_DETAILS.Line_No   from TSPL_QUALITY_CHECK left outer join TSPL_QUALITY_CHEMBER_DETAILS on TSPL_QUALITY_CHEMBER_DETAILS.QC_No=TSPL_QUALITY_CHECK.QC_No " & _
        "  left outer join TSPL_MILK_GRADE_MASTER on  TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE=TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE  where  TSPL_QUALITY_CHECK.QC_No='" + FndQCNo.Value + "'  "   ' Change By Prabhakar 25/11/2016  and  TSPL_MILK_GRADE_MASTER.GRADE_TYPE in ('A','A+','A++')

        dt = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
            dtpQCInDateTime.Value = clsCommon.myCDate(dt.Rows(0)("QC_In_Date_Time"))
            dtpQCOutDateTime.Value = clsCommon.myCDate(dt.Rows(0)("QC_Out_Date_Time"))
            fndGateEntryNo.Value = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
            dtpGateEntryDateTime.Value = clsCommon.myCDate(dt.Rows(0)("Gate_Entry_Date_And_Time"))
            txtChallanNo.Text = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
            dtpChallanDate.Value = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))
            fndTankerNo.Value = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            fndLocation.Value = clsCommon.myCstr(dt.Rows(0)("location_Code"))
            lblLocationName.Text = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            fndVendor.Value = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Desc"))
            txtDipValue.Text = clsCommon.myCdbl(dt.Rows(0)("Dip_Value"))
            txtWeighmentNo.Text = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
            If dt.Rows(0)("Weighment_Date") IsNot DBNull.Value Then
                dtpWeighmentDate.Value = clsCommon.myCDate(dt.Rows(0)("Weighment_Date"))
            Else
                dtpWeighmentDate.Value = clsCommon.GETSERVERDATE()
            End If

            TxtDeductionAmount.Value = clsCommon.myCdbl(dt.Rows(0)("DeductionAmount"))
            txtRcptControlSampleFAT.Text = clsCommon.myCdbl(dt.Rows(0)("Receipt_Control_FAT"))
            txtRcptControlSampleSNF.Text = clsCommon.myCdbl(dt.Rows(0)("Receipt_Control_SNF"))
            loadBlankItemGrid()
            Dim intLineNo As Integer = 0
            For i As Integer = 0 To dt.Rows.Count - 1
                intLineNo += 1
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = clsCommon.myCdbl(dt.Rows(i)("Line_No"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colChamberDesc).Value = clsCommon.myCstr(dt.Rows(i)("Chamber_Desc"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colQCFATPER).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("  Select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No ='" & clsCommon.myCstr(FndQCNo.Value) & "' and TSPL_QC_Parameter_Detail.Param_Type ='FAT' and line_no='" & intLineNo & "'"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colQCSNFPER).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("  Select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No ='" & clsCommon.myCstr(FndQCNo.Value) & "' and TSPL_QC_Parameter_Detail.Param_Type ='SNF' and line_no='" & intLineNo & "'"))
                If clsCommon.myLen(txtWeighmentNo.Text) = 0 Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colNETWEIGHT).Value = clsCommon.myCdbl(dt.Rows(i)("NetWeight"))
                Else
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colNETWEIGHT).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Net_Weight from TSPL_WEIGHMENT_CHEMBER_DETAILS where Weighment_No='" & txtWeighmentNo.Text & "' and Line_No='" & gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value & "' and Chamber_Desc='" & gv1.Rows(gv1.Rows.Count - 1).Cells(colChamberDesc).Value & "'"))

                End If

                If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAdditionalWeightinPercentage, clsFixedParameterCode.AllowAdditionalWeightinPercentage, Nothing)), "1") = CompairStringResult.Equal Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colADDWEIGHTPER).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnterAdditionalWeight, clsFixedParameterCode.EnterAdditionalWeight, Nothing))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCALCULATEDADDWEIGHT).Value = (clsCommon.myCdbl(dt.Rows(i)("NetWeight")) * clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnterAdditionalWeight, clsFixedParameterCode.EnterAdditionalWeight, Nothing))) / 100
                Else
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCALCULATEDADDWEIGHT).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnterAdditionalWeight, clsFixedParameterCode.EnterAdditionalWeight, Nothing))
                End If
                'gv1.Rows(gv1.Rows.Count - 1).Cells(colTOTALWEIGHT).Value = gv1.Rows(gv1.Rows.Count - 1).Cells(colCALCULATEDADDWEIGHT).Value + gv1.Rows(gv1.Rows.Count - 1).Cells(colADDWEIGHTPER).Value
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTOTALWEIGHT).Value = gv1.Rows(gv1.Rows.Count - 1).Cells(colCALCULATEDADDWEIGHT).Value + gv1.Rows(gv1.Rows.Count - 1).Cells(colNETWEIGHT).Value
                gv1.Rows.AddNew()
            Next
            gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
        End If
      
        dt = Nothing
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If ClsSecondarySettingForQC.ReverseAndUnpost(txtDocNo.Value, arrLoc) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
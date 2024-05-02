Imports common
Imports System.Data.SqlClient

Public Class FrmMCCTankerGateOut
    Inherits FrmMainTranScreen
    Public isLoadData As Boolean = False
    Dim obj As clsMCCTankerGateOut = Nothing
    Dim isNewEntry As Boolean = True
    ' Ticket No : BHA/27/06/18-000091 By Prabhakar
    ''BHA/04/07/18-000130 by balwider add tanker finder in mcc tanker gate out.
    Dim settFirstGateOutProcessForBulkProcument As Boolean = False
    Dim MccPlantSelectionOptionInMccTankerGateOut As Boolean = False
    Dim ShowTankerWithoutCheckingAnyValidation As Boolean = False
    Dim CreateMCCTankerGateOutBasedOnBulkRouteMaster As Boolean = False
    Const colCCCode As String = "PCCODE"
    Const colCCName As String = "PCNAME"
    Const colCCSEQ As String = "PCSEQ"
    Public arrMCC As ArrayList
    Public arrMCCName As ArrayList
    Private Sub FrmMCCTankerGateOut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'Dim coll As Dictionary(Of String, String)
            'coll = New Dictionary(Of String, String)()
            'coll.Add("Bulk_Route_Code", "Varchar(30) null references TSPL_BULK_ROUTE_MASTER(ROUTE_NO)")
            'coll.Add("Opening_Km", "decimal(18,2) null default 0")
            'clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MCC_TANKER_GATE_OUT", coll, Nothing, False, False, "", "GATE_OUT_NO", "GATE_OUT_DATE")

            SetUserMgmtNew()
            settFirstGateOutProcessForBulkProcument = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FirstGateOutProcessForMCCBulkProcument, clsFixedParameterCode.FirstGateOutProcessForMCCBulkProcument, Nothing))
            If Not settFirstGateOutProcessForBulkProcument Then
                Throw New Exception("MCC Tanker Gate out Not Required") ''BHA/27/06/18-000092 by balwinder on 05/07/2018
            End If
            MccPlantSelectionOptionInMccTankerGateOut = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MccPlantSelectionOptionInMccTankerGateOut, clsFixedParameterCode.MccPlantSelectionOptionInMccTankerGateOut, Nothing)) = 0, False, True)
            ShowTankerWithoutCheckingAnyValidation = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowTankerWithoutCheckingAnyValidation, clsFixedParameterCode.ShowTankerWithoutCheckingAnyValidation, Nothing)) = 1, True, False)
            CreateMCCTankerGateOutBasedOnBulkRouteMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateMCCTankerGateOutBasedOnBulkRouteMaster, clsFixedParameterCode.CreateMCCTankerGateOutBasedOnBulkRouteMaster, Nothing)) = 1, True, False)
            If MccPlantSelectionOptionInMccTankerGateOut = True Then
                grpMcc_Plant.Visible = True
                TxtTankerCapacity.Visible = True
                lblDriver.Visible = True
                txtDriver.Visible = True
                lblPhoneNo.Visible = True
                txt_Phone_No.Visible = True
                lblRemarks.Visible = True
                txtRemarks.Visible = True
                btn_cancel.Visible = True
                btnPrint.Visible = True
            Else
                grpMcc_Plant.Visible = False
                TxtTankerCapacity.Visible = False
                lblDriver.Visible = False
                txtDriver.Visible = False
                lblPhoneNo.Visible = False
                txt_Phone_No.Visible = False
                lblRemarks.Visible = False
                txtRemarks.Visible = False
                btn_cancel.Visible = False
                btnPrint.Visible = False
            End If
            If CreateMCCTankerGateOutBasedOnBulkRouteMaster = True Then
                mulMccCode.Visible = False
                txtRoute.Visible = True
                MyLabel1.Text = "Route Code"
            Else
                txtRoute.Visible = False
                mulMccCode.Visible = True
            End If
            Reset()
            btnUnpost.Visible = False

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            If clsCommon.CompairString(ex.Message, "MCC Tanker Gate out Not Required") = CompairStringResult.Equal Then
                Me.Close()
            End If
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        If MyBase.isReverse Then
            btnUnpost.Enabled = True
        Else
            btnUnpost.Enabled = False
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If allowToSave() Then SaveData()
    End Sub
    Function allowToSave() As Boolean
        Try
            'If clsCommon.myLen(txtMccCode.Value) <= 0 Then
            '    Throw New Exception("Mcc Code Can't left blank")
            'End If

            If mulMccCode.arrValueMember IsNot Nothing AndAlso mulMccCode.arrValueMember.Count > 1 Then
                'Squence number mandatory
                Dim arrSequence As New List(Of Integer)
                For i As Integer = 0 To dgv.Rows.Count - 1

                    If clsCommon.myCdbl(dgv.Rows(i).Cells(colCCSEQ).Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Please enter Mcc squence no At Line No. " & " : " + clsCommon.myCstr(i + 1) + "", Me.Text)
                        Return False
                    End If

                    If arrSequence.Contains(clsCommon.myCstr(dgv.Rows(i).Cells(colCCSEQ).Value)) Then
                        common.clsCommon.MyMessageBoxShow("Same Mcc sequence no Repeated (" + clsCommon.myCstr(dgv.Rows(i).Cells(colCCSEQ).Value) + ") At Line No. " + clsCommon.myCstr(i + 1) + "", Me.Text)
                        Return False
                    Else
                        arrSequence.Add(clsCommon.myCstr(dgv.Rows(i).Cells(colCCSEQ).Value))
                    End If

                Next
            End If

            If clsCommon.myLen(txtLocationCode.Value) <= 0 Then
                Throw New Exception("Location Code Can't left blank")
            End If
            If clsCommon.myLen(txtTankerNo.Value) <= 0 Then
                Throw New Exception("Tanker No Can't left blank")
            End If
            If chkForContractor.Checked = True Then
                If clsCommon.myCdbl(txtOpKM.Value) <= 0 Then
                    Throw New Exception("Please enter Opening KM")
                End If
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Sub SaveData()
        Try
            obj = New clsMCCTankerGateOut
            obj.GATE_OUT_NO = clsCommon.myCstr(fndGateOutNo.Value)
            obj.GATE_OUT_DATE = clsCommon.GetPrintDate(txtGateOutDate.Value, "dd/MMM/yyyy hh:mm:ss tt")
            obj.TANKER_NO = txtTankerNo.Value
            obj.Distance_of_Route = clsCommon.myCdbl(txtDistanceOfRoute.Text)
            obj.Bulk_Route_Code = txtRoute.Value
            obj.Opening_Km = clsCommon.myCdbl(txtOpKM.Text)
            If mulMccCode.arrValueMember IsNot Nothing AndAlso mulMccCode.arrValueMember.Count = 1 Then
                obj.MCC_CODE = clsCommon.myCstr(mulMccCode.arrValueMember(0))
            ElseIf (mulMccCode.arrValueMember IsNot Nothing AndAlso mulMccCode.arrValueMember.Count > 1) OrElse (CreateMCCTankerGateOutBasedOnBulkRouteMaster = True) Then
                For Each grow As GridViewRowInfo In dgv.Rows
                    If CInt(grow.Cells(colCCSEQ).Value) = 1 Then
                        obj.MCC_CODE = clsCommon.myCstr(grow.Cells(colCCCode).Value)
                    ElseIf CInt(grow.Cells(colCCSEQ).Value) = 2 Then
                        obj.MCC_CODE2 = clsCommon.myCstr(grow.Cells(colCCCode).Value)
                    ElseIf CInt(grow.Cells(colCCSEQ).Value) = 3 Then
                        obj.MCC_CODE3 = clsCommon.myCstr(grow.Cells(colCCCode).Value)
                    End If
                Next
            End If

            'obj.MCC_CODE = txtMccCode.Value
            obj.LOCATION_CODE = clsCommon.myCstr(txtLocationCode.Value)
            If MccPlantSelectionOptionInMccTankerGateOut = True Then
                obj.Mcc_Plant = clsCommon.myCstr(IIf(chkMCC.IsChecked = True, "Mcc", IIf(chkPlant.IsChecked = True, "Plant", "")))
                obj.Storage_Capacity = clsCommon.myCdbl(TxtTankerCapacity.Value)
                obj.TO_LOCATION_CODE = clsCommon.myCstr(txtToLocationCode.Value)
                obj.PhoneNo = clsCommon.myCstr(txt_Phone_No.Text)
                obj.DriverName = clsCommon.myCstr(txtDriver.Text)
                obj.Remarks = clsCommon.myCstr(txtRemarks.Text)
            End If
            If chkForContractor.Checked = True Then
                obj.TO_LOCATION_CODE = clsCommon.myCstr(txtToLocationCode.Value)
                obj.TollAmount = txtTollAmount.Value
                obj.IsContractor = IIf(chkForContractor.Checked = True, 1, 0)
            End If


            If clsMCCTankerGateOut.saveData(obj, isNewEntry) Then
                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                End If
                LoadData(obj.GATE_OUT_NO, NavigatorType.Current)

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(fndGateOutNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If

            If clsCommon.MyMessageBoxShow("Post the current document - " + fndGateOutNo.Value + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsMCCTankerGateOut.PostData(fndGateOutNo.Value)
                clsCommon.MyMessageBoxShow("Sucessfully Posted", Me.Text)
                LoadData(fndGateOutNo.Value, NavigatorType.Current)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Sub LoadData(ByVal str As String, ByVal navtype As NavigatorType)
        Try
            Reset()
            obj = clsMCCTankerGateOut.GetData(str, navtype)
            If obj IsNot Nothing Then
                isLoadData = True
                isNewEntry = False
                fndGateOutNo.Value = obj.GATE_OUT_NO
                txtGateOutDate.Value = obj.GATE_OUT_DATE
                arrMCC = Nothing
                arrMCC = New ArrayList()
                arrMCCName = Nothing
                arrMCCName = New ArrayList()
                Dim strMCCName As String = ""
                If clsCommon.myLen(obj.MCC_CODE) > 0 Then
                    arrMCC.Add(obj.MCC_CODE)
                    strMCCName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code in ('" + obj.MCC_CODE + "' )"))
                    arrMCCName.Add(strMCCName)
                End If
                If clsCommon.myLen(obj.MCC_CODE2) > 0 Then
                    arrMCC.Add(obj.MCC_CODE2)
                    strMCCName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code in ('" + obj.MCC_CODE2 + "' )"))
                    arrMCCName.Add(strMCCName)
                End If
                If clsCommon.myLen(obj.MCC_CODE3) > 0 Then
                    arrMCC.Add(obj.MCC_CODE3)
                    strMCCName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code in ('" + obj.MCC_CODE3 + "' )"))
                    arrMCCName.Add(strMCCName)
                End If
                If clsCommon.CompairString(obj.Mcc_Plant, "Mcc") = CompairStringResult.Equal Then
                    chkMCC.IsChecked = True
                ElseIf clsCommon.CompairString(obj.Mcc_Plant, "Plant") = CompairStringResult.Equal Then
                    chkPlant.IsChecked = True
                End If
                If CreateMCCTankerGateOutBasedOnBulkRouteMaster = True Then
                    txtRoute.Value = obj.Bulk_Route_Code
                    lblMccCode.Text = clsCommon.GetMulcallStringWithComma(clsBulkRoutMasterMCC.GetData(txtRoute.Value))
                Else
                    mulMccCode.arrValueMember = arrMCC
                    mulMccCode.arrDispalyMember = arrMCCName
                    lblMccCode.Text = clsCommon.GetMulcallStringWithComma(mulMccCode.arrDispalyMember)
                End If


                If arrMCC.Count >= 1 Then
                    SetDataBaseGrid()
                    For Each grow As GridViewRowInfo In dgv.Rows
                        If (clsCommon.myCstr(grow.Cells(colCCCode).Value) = clsCommon.myCstr(obj.MCC_CODE)) Then
                            grow.Cells(colCCSEQ).Value = 1
                        ElseIf (clsCommon.myCstr(grow.Cells(colCCCode).Value) = clsCommon.myCstr(obj.MCC_CODE2)) Then
                            grow.Cells(colCCSEQ).Value = 2
                        ElseIf (clsCommon.myCstr(grow.Cells(colCCCode).Value) = clsCommon.myCstr(obj.MCC_CODE3)) Then
                            grow.Cells(colCCSEQ).Value = 3
                        End If
                    Next
                End If


                'txtMccCode.Value = obj.MCC_CODE
                'lblMccCode.Text = obj.MCC_DESC
                txtLocationCode.Value = obj.LOCATION_CODE
                lblLocationName.Text = obj.LOCATION_DESC
                lblPending.Status = IIf(clsCommon.myCdbl(obj.IS_POSTED) = 1 AndAlso clsCommon.myCdbl(obj.IsCancel) <> 1, ERPTransactionStatus.Approved, IIf(clsCommon.myCdbl(obj.IsCancel) = 1, ERPTransactionStatus.Cancel, ERPTransactionStatus.Pending))
                txtTankerNo.Value = obj.TANKER_NO
                lblTankerNo.Text = getTransporterName(txtTankerNo.Value)

                txtToLocationCode.Value = clsCommon.myCstr(obj.TO_LOCATION_CODE)
                lblToLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Desc  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + Convert.ToString(txtToLocationCode.Value) + "'"))
                TxtTankerCapacity.Value = clsCommon.myCdbl(obj.Storage_Capacity)
                txtDriver.Text = clsCommon.myCstr(obj.DriverName)
                txt_Phone_No.Text = clsCommon.myCstr(obj.PhoneNo)
                txtRemarks.Text = clsCommon.myCstr(obj.Remarks)
                txtDistanceOfRoute.Text = clsCommon.myCdbl(obj.Distance_of_Route)
                txtOpKM.Text = clsCommon.myCdbl(obj.Opening_Km)
                txtTollAmount.Value = obj.TollAmount
                chkForContractor.Checked = IIf(obj.IsContractor = 1, True, False)
                If obj.IS_POSTED = common.ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False

                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT IsCancel  FROM TSPL_MCC_TANKER_GATE_OUT WHERE Location_Code = '" + Convert.ToString(txtToLocationCode.Value) + "'")) = 0 Then
                        btn_cancel.Enabled = True
                    End If

                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                End If

                btnSave.Text = "Update"
                fndGateOutNo.MyReadOnly = True
                isLoadData = False
            End If
        Catch ex As Exception
            isLoadData = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        txtDistanceOfRoute.Text = "0"
        fndGateOutNo.Value = ""
        txtGateOutDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm:ss tt")
        mulMccCode.arrValueMember = Nothing
        mulMccCode.arrDispalyMember = Nothing
        chkForContractor.Checked = False
        arrMCC = Nothing
        arrMCCName = Nothing
        dgv.Rows.Clear()
        dgv.Columns.Clear()
        'txtMccCode.Value = ""
        lblMccCode.Text = ""
        txtLocationCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location  from TSPL_USER_MASTER   where user_code='" & objCommonVar.CurrentUserCode & "'"))
        lblLocationName.Text = clsLocation.GetName(txtLocationCode.Value, Nothing)
        btnSave.Enabled = True
        btnPost.Enabled = False
        btnDelete.Enabled = False
        isNewEntry = True
        btnSave.Text = "Save"
        fndGateOutNo.MyReadOnly = False
        txtTankerNo.Value = ""
        lblTankerNo.Text = ""
        TxtTankerCapacity.Value = 0
        txtToLocationCode.Value = ""
        txtDriver.Text = ""
        txt_Phone_No.Text = ""
        txtRemarks.Text = ""
        txtRoute.Value = ""
        txtOpKM.Text = "0"
        lblPending.Status = ERPTransactionStatus.Pending
        btn_cancel.Enabled = False
        chkForContractor.Checked = True
        chkForContractor.Checked = False
        txtTollAmount.ReadOnly = True
        txtToLocationCode.Enabled = True
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub fndGateOutNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndGateOutNo._MYNavigator
        LoadData(fndGateOutNo.Value, NavType)
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        deleteData()

    End Sub

    Sub deleteData()
        Try
            If clsCommon.myLen(fndGateOutNo.Value) <= 0 Then
                Throw New Exception("No document found to Delete")
            End If

            If myMessages.deleteConfirm() Then
                clsMCCTankerGateOut.deleteData(fndGateOutNo.Value)
                myMessages.delete()
                Reset()

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    'Private Sub txtMccCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMccCode._MYValidating
    '    Try
    '        Dim whrCls As String = String.Empty
    '        If chkPlant.IsChecked = True Then
    '            whrCls = "isnull(Type,'')='PLANT'"
    '            Dim qry As String = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER.Location_Desc as [Name] from TSPL_LOCATION_MASTER "
    '            txtMccCode.Value = clsCommon.ShowSelectForm("MCCMSTGate1", qry, "Code", whrCls, txtMccCode.Value, "Code", isButtonClicked)
    '            lblMccCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Desc  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + Convert.ToString(txtMccCode.Value) + "'"))
    '        Else
    '            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
    '                whrCls = "  mcc_code in (" & objCommonVar.strCurrUserLocations & ") "
    '            End If
    '            txtMccCode.Value = clsMccMaster.getFinder(whrCls, txtMccCode.Value, isButtonClicked)
    '            lblMccCode.Text = clsLocation.GetName(txtMccCode.Value, Nothing)
    '        End If

    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
    '    End Try

    'End Sub


    Private Sub txtLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocationCode._MYValidating
        Dim whrclas As String = ""
        Dim qry As String = "Select  TSPL_LOCATION_MASTER.Location_Code as Code,TSPL_LOCATION_MASTER.Location_Desc as Description  from TSPL_LOCATION_MASTER  "
        If clsCommon.myLen(clsCommon.myCstr(objCommonVar.strCurrUserLocations)) > 0 Then
            whrclas = "TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocationCode.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("USERLOCATION", qry, "Code", whrclas, txtLocationCode.Value, "Code", isButtonClicked))
        lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Desc  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + Convert.ToString(txtLocationCode.Value) + "'"))
    End Sub

    Private Sub fndGateOutNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndGateOutNo._MYValidating
        If isButtonClicked Then
            fndGateOutNo.Value = clsMCCTankerGateOut.getGateOutFinder("", fndGateOutNo.Value, isButtonClicked)
            LoadData(fndGateOutNo.Value, NavigatorType.Current)
        ElseIf fndGateOutNo.MyReadOnly OrElse fndGateOutNo.Value IsNot Nothing Then
            LoadData(fndGateOutNo.Value, NavigatorType.Current)
        Else
            Reset()
        End If

    End Sub

    Private Sub txtTankerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTankerNo._MYValidating
        If ShowTankerWithoutCheckingAnyValidation = True Then
            txtTankerNo.Value = clsCommon.ShowSelectForm("TsKdRNO1", "select Tanker_No as [TankerNo] from TSPL_TANKER_MASTER", "TankerNo", "", txtTankerNo.Value, "TankerNo", isButtonClicked)
        Else
            txtTankerNo.Value = clsCommon.ShowSelectForm("TsKdRNO", clsMCCTankerGateOut.GetPendingTankerNoQry(fndGateOutNo.Value), "TankerNo", "", txtTankerNo.Value, "TankerNo", isButtonClicked)
        End If

        lblTankerNo.Text = getTransporterName(txtTankerNo.Value)
        TxtTankerCapacity.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Storage_Capacity from TSPL_TANKER_MASTER where Tanker_No='" & txtTankerNo.Value & "'"))
    End Sub

    Function getTransporterName(ByVal strTankerNo As String) As String
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name   from TSPL_vendor_master where Vendor_Code=(select Tanker_Transporter_Code  from TSPL_TANKER_MASTER where Tanker_No='" & strTankerNo & "' )"))
        Return str
    End Function

    Private Sub TxtToLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtToLocationCode._MYValidating
        Try
            Dim whrclas As String = ""
            Dim qry As String = "Select  TSPL_LOCATION_MASTER.Location_Code as Code,TSPL_LOCATION_MASTER.Location_Desc as Description  from TSPL_LOCATION_MASTER  "
            If clsCommon.myLen(clsCommon.myCstr(objCommonVar.strCurrUserLocations)) > 0 Then
                whrclas = "TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtToLocationCode.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("USERLOC1", qry, "Code", whrclas, txtToLocationCode.Value, "Code", isButtonClicked))

            If clsCommon.myLen(txtToLocationCode.Value) > 0 Then
                lblToLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Desc  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + Convert.ToString(txtToLocationCode.Value) + "'"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub ChkMCC_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMCC.ToggleStateChanged
        If isLoadData = False Then
            Reset()
        End If
    End Sub

    Private Sub ChkPlant_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkPlant.ToggleStateChanged
        If isLoadData = False Then
            Reset()
        End If
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(fndGateOutNo.Value) <= 0 Then
                Throw New Exception("Not found anything to print")
            Else

                Dim qry As String = ""
                Dim frmCRV As New frmCrystalReportViewer()

                qry = "select  O.Tanker_No  , isnull(o.GATE_OUT_NO ,'') [Gate-Out No] , O.comp_code [Company Code] , c.Comp_Name [Comp Desc] , CONCAT(c.Add1 , ' ' , c.Add2 , ' ', c.Add3 , ' , ', c.State ) as [Company Address],c.Logo_Img2,O.DriverName,z.MCC_NAME as AllocateToName ,O.PhoneNo,O.Remarks,O.Storage_Capacity from TSPL_MCC_TANKER_GATE_OUT O LEFT JOIN TSPL_COMPANY_MASTER C on O.comp_code = c.Comp_Code LEFT JOIN (select MCC_Code,mcc_name from TSPL_MCC_MASTER union all select TSPL_LOCATION_MASTER.Location_Code ,TSPL_LOCATION_MASTER.Location_Desc  from TSPL_LOCATION_MASTER)z on z.MCC_Code=O.LOCATION_CODE where O.GATE_OUT_NO in ('" + fndGateOutNo.Value + "')"

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "crptMccTankerGateOutPrint", "Gate Out", clsCommon.myCDate(txtGateOutDate.Value))

                frmCRV = Nothing
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        Try
            Dim qry As String = ""
            qry = "select distinct Chalan_NO from tspl_mcc_dispatch_challan where Against_Gate_Out='" + fndGateOutNo.Value + "'"
            Dim dt As DataTable = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                qry = "Gate Out No used in following Tanker Dispatch"
                For Each dr As DataRow In dt.Rows
                    qry += Environment.NewLine + clsCommon.myCstr(dr("Chalan_NO"))
                Next
                qry += Environment.NewLine + "Can't cancel it"
                Throw New Exception(qry)
            End If

            If clsMCCTankerGateOut.Cancel(fndGateOutNo.Value) Then
                clsCommon.MyMessageBoxShow("Gate Out cancelled successfully!", Me.Text)
                LoadData(fndGateOutNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub MulMccCode__My_Click(sender As Object, e As EventArgs) Handles mulMccCode._My_Click
        'Dim StrQry As String = "select Zone_Code as Code , Description as Name from TSPL_ZONE_MASTER"
        'mulMccCode.arrValueMember = clsCommon.ShowMultipleSelectForm("Mul1MccCode@GateOut", StrQry, "Code", "Name", mulMccCode.arrValueMember, mulMccCode.arrDispalyMember)

        Try
            Dim qry As String = String.Empty
            Dim whrCls As String = String.Empty
            If chkPlant.IsChecked = True Then
                'whrCls = "isnull(Type,'')='PLANT'"
                qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER.Location_Desc as [Name] from TSPL_LOCATION_MASTER where isnull(Type,'')='PLANT'"
                'txtMccCode.Value = clsCommon.ShowSelectForm("MCCMSTGate1", qry, "Code", whrCls, txtMccCode.Value, "Code", isButtonClicked)
                'lblMccCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Desc  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + Convert.ToString(txtMccCode.Value) + "'"))
                mulMccCode.arrValueMember = clsCommon.ShowMultipleSelectForm("Mul1Plant@GateOut", qry, "Code", "Name", mulMccCode.arrValueMember, mulMccCode.arrDispalyMember)

                If mulMccCode.arrValueMember IsNot Nothing AndAlso mulMccCode.arrValueMember.Count > 1 Then
                    mulMccCode.arrValueMember = Nothing
                    Throw New Exception("Select one Plant at a time.")
                End If
                lblMccCode.Text = clsCommon.GetMulcallStringWithComma(mulMccCode.arrDispalyMember)
            Else
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = "  mcc_code in (" & objCommonVar.strCurrUserLocations & ") "
                End If
                'txtMccCode.Value = clsMccMaster.getFinder(whrCls, txtMccCode.Value, isButtonClicked)
                'lblMccCode.Text = clsLocation.GetName(txtMccCode.Value, Nothing)
                qry = " select tspl_mcc_master.MCC_Code as [Code] ,tspl_mcc_master.MCC_Type as [Mcc Type] ,tspl_mcc_master.MCC_NAME as [Mcc Name] ,tspl_mcc_master.Chilling_Vendor as [Chilling Vendor] ,tspl_mcc_master.Add1 as [Address1] ,tspl_mcc_master.Add2 as [Address2] ,tspl_mcc_master.Tehsil as [Tehsil] ,tspl_mcc_master.City_code as [City Code] ,tspl_mcc_master.State_Code as [State Code] ,tspl_mcc_master.Country_code as [Country Code] ,tspl_mcc_master.Pin_code as [Pin Code],tspl_mcc_master.Pan_No as [Pan No] ,tspl_mcc_master.Telphone as [Telphone] ,tspl_mcc_master.Email as [Email] ,tspl_mcc_master.Fax as [Fax] ,tspl_mcc_master.MCC_Area as [Mcc Area] ,tspl_mcc_master.Area_Of_Store as [Area Of Store] ,tspl_mcc_master.Area_Of_Office as [Area Of Office] ,tspl_mcc_master.Open_Area_For_tanker as [Open Area For Tanker] ,tspl_mcc_master.Area_Of_LAB as [Area Of Lab] ,tspl_mcc_master.No_Of_SILO as [No Of Silo] ,tspl_mcc_master.Total_Storage_capacity as [Total Storage Capacity] ,tspl_mcc_master.Area_Of_Receiving_DOCK as [Area Of Receiving Dock] ,tspl_mcc_master.No_Of_Chiller as [No Of Chiller] ,tspl_mcc_master.Chiller_Brand_Name as [Chiller Brand Name] ,tspl_mcc_master.Chiller_Capacity as [Chiller Capacity] ,tspl_mcc_master.No_Of_MilkPump as [No Of Milkpump] ,tspl_mcc_master.MilkPump_Capacity as [Milkpump Capacity] ,tspl_mcc_master.DripSaver as [Drip Saver] ,tspl_mcc_master.CanWasher as [Can Washer] ,tspl_mcc_master.CanScrubber as [Can Scrubber] ,tspl_mcc_master.FSSAI_NO as [FSSAI No] ,tspl_mcc_master.ETP as [ETP] ,tspl_mcc_master.Earthing as [Earthing] ,tspl_mcc_master.Coil_Length as [Coil Length] ,tspl_mcc_master.Electricity_Connection as [Electricity Connection] ,tspl_mcc_master.Boiler as [Boiler] ,tspl_mcc_master.NoOfDG as [No. of DG] ,tspl_mcc_master.NoOfCompressor as [No. of Compressor] ,tspl_mcc_master.PayeeName as [Payee Name] ,tspl_mcc_master.BankName as [Bank Name] ,tspl_mcc_master.BankBranch as [Bank Branch] ,tspl_mcc_master.BankCityCode as [Bank City Code] ,tspl_mcc_master.BankStateCode as [Bank State Code] ,tspl_mcc_master.IFCICode as [IFCI Code] ,tspl_mcc_master.AccountNO as [Account No] ,tspl_mcc_master.Created_By as [Created By] ,tspl_mcc_master.Created_Date as [Created Date] ,tspl_mcc_master.Modified_By as [Modified By] ,tspl_mcc_master.Modified_Date as [Modified Date] ,tspl_mcc_master.Comp_Code as [Company Code],tspl_mcc_master.mcc_code_vlc_uploader as [MCC Code For VLC Uploder],tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER.Location_Desc AS [Plant Name] From tspl_mcc_master LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=tspl_mcc_master.Plant_Code"
                If clsCommon.myLen(whrCls) > 0 Then
                    qry = qry + " where " + whrCls
                End If

                mulMccCode.arrValueMember = clsCommon.ShowMultipleSelectForm("Mul1MccCode@GateOut", qry, "Code", "Mcc Name", mulMccCode.arrValueMember, mulMccCode.arrDispalyMember)

                If mulMccCode.arrValueMember IsNot Nothing AndAlso mulMccCode.arrValueMember.Count > 3 Then
                    mulMccCode.arrValueMember = Nothing
                    Throw New Exception("Maximum three MCC select at a time.")
                End If
                lblMccCode.Text = clsCommon.GetMulcallStringWithComma(mulMccCode.arrDispalyMember)
                If mulMccCode.arrValueMember IsNot Nothing AndAlso mulMccCode.arrValueMember.Count > 1 Then
                    SetDataBaseGrid()
                End If

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Sub SetDataBaseGrid()
        Try
            dgv.Rows.Clear()
            dgv.Columns.Clear()

            'Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            'repoSelect.FormatString = ""
            'repoSelect.HeaderText = "Select"
            'repoSelect.Name = colSelect
            'repoSelect.Width = 50
            'repoSelect.ReadOnly = False
            'repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            'dgv.MasterTemplate.Columns.Add(repoSelect)

            Dim repoCostCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoCostCode.FormatString = ""
            repoCostCode.HeaderText = "Mcc Code"
            repoCostCode.Name = colCCCode
            repoCostCode.Width = 150
            repoCostCode.ReadOnly = True
            dgv.MasterTemplate.Columns.Add(repoCostCode)

            Dim repoCostName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoCostName.FormatString = ""
            repoCostName.HeaderText = "Mcc Name"
            repoCostName.Name = colCCName
            repoCostName.Width = 250
            repoCostName.ReadOnly = True
            dgv.MasterTemplate.Columns.Add(repoCostName)

            Dim repoDecimalColumn As GridViewDecimalColumn = Nothing
            repoDecimalColumn = New GridViewDecimalColumn()
            repoDecimalColumn.Name = colCCSEQ
            repoDecimalColumn.Width = 105
            repoDecimalColumn.FormatString = "{0:n0}"
            repoDecimalColumn.DecimalPlaces = 0
            repoDecimalColumn.HeaderText = "Sequence"
            repoDecimalColumn.ReadOnly = False
            dgv.MasterTemplate.Columns.Add(repoDecimalColumn)
            Dim qry As String = ""
            If CreateMCCTankerGateOutBasedOnBulkRouteMaster = True Then
                qry = "select TSPL_BULK_ROUTE_MASTER_MCC.MCC_Code as [Code] ,tspl_mcc_master.MCC_NAME as [Name] from TSPL_BULK_ROUTE_MASTER_MCC left join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_BULK_ROUTE_MASTER_MCC.mcc_code where TSPL_BULK_ROUTE_MASTER_MCC.Route_no ='" + txtRoute.Value + "'"
            Else
                qry = "select tspl_mcc_master.MCC_Code as [Code] ,tspl_mcc_master.MCC_NAME as [Name] from tspl_mcc_master where MCC_Code in (" + clsCommon.GetMulcallString(mulMccCode.arrValueMember) + ")"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    dgv.Rows.AddNew()
                    'dgv.Rows(dgv.Rows.Count - 1).Cells(colSelect).Value = False
                    dgv.Rows(dgv.Rows.Count - 1).Cells(colCCCode).Value = clsCommon.myCstr(dr("Code"))
                    dgv.Rows(dgv.Rows.Count - 1).Cells(colCCName).Value = clsCommon.myCstr(dr("Name"))
                    dgv.Rows(dgv.Rows.Count - 1).Cells(colCCSEQ).Value = Nothing
                Next
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub FrmMCCTankerGateOut_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnUnpost.Visible = True
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnUnpost_Click(sender As Object, e As EventArgs) Handles btnUnpost.Click
        Try
            If clsCommon.myLen(fndGateOutNo.Value) > 0 Then
                Dim qry As String = "select 1 from TSPL_MCC_TANKER_GATE_OUT where GATE_OUT_NO='" + fndGateOutNo.Value + "' and IS_POSTED=1"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Transaction status should be posted.")
                End If
                qry = "select 1 from TSPL_MCC_TANKER_GATE_OUT where GATE_OUT_NO='" + fndGateOutNo.Value + "' and IsCancel=1"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Throw New Exception("Can't unpost it because document already cancelled")
                End If

                qry = "select distinct Chalan_NO from tspl_mcc_dispatch_challan where Against_Gate_Out='" + fndGateOutNo.Value + "'"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    qry = "Gate Out No used in following Tanker Dispatch"
                    For Each dr As DataRow In dt.Rows
                        qry += Environment.NewLine + clsCommon.myCstr(dr("Chalan_NO"))
                    Next
                    qry += Environment.NewLine + "Can't unpost it"
                    Throw New Exception(qry)
                End If
                If clsCommon.MyMessageBoxShow("Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    qry = "update TSPL_MCC_TANKER_GATE_OUT set IS_POSTED=0,Posted_Date=null, Posted_By = null where GATE_OUT_NO='" + fndGateOutNo.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                    clsCommon.MyMessageBoxShow("Tansaction unposted succesffuly", Me.Text)
                    LoadData(fndGateOutNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRoute._MYValidating
        Try
            Dim whrclas As String = ""
            Dim qry As String = "Select  TSPL_BULK_ROUTE_MASTER.route_no as Code,TSPL_BULK_ROUTE_MASTER.route_name as Description  from TSPL_BULK_ROUTE_MASTER  "
            'If clsCommon.myLen(clsCommon.myCstr(objCommonVar.strCurrUserLocations)) > 0 Then
            '    whrclas = "TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            'End If
            If chkForContractor.Checked = True Then
                whrclas = " TSPL_BULK_ROUTE_MASTER.IsContractor=1 "
            Else
                whrclas = " TSPL_BULK_ROUTE_MASTER.IsContractor=0 "
            End If
            txtRoute.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("MTGOBRM", qry, "Code", whrclas, txtRoute.Value, "Code", isButtonClicked))

            If clsCommon.myLen(txtRoute.Value) > 0 Then
                lblMccCode.Text = clsCommon.GetMulcallStringWithComma(clsBulkRoutMasterMCC.GetData(txtRoute.Value))
                txtDistanceOfRoute.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT distance FROM TSPL_BULK_ROUTE_MASTER WHERE route_no = '" + txtRoute.Value + "'"))
                txtToLocationCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_code FROM TSPL_BULK_ROUTE_MASTER WHERE route_no = '" + txtRoute.Value + "'"))
                lblToLocationName.Text = clsCommon.myCstr(clsLocation.GetName(txtToLocationCode.Value, Nothing))
                txtTollAmount.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TollAmount FROM TSPL_BULK_ROUTE_MASTER WHERE route_no = '" + txtRoute.Value + "'"))
                SetDataBaseGrid()
            Else
                lblMccCode.Text = ""
                If chkForContractor.Checked = True Then
                    txtToLocationCode.Value = ""
                    lblToLocationName.Text = ""
                End If
                txtTollAmount.Value = 0
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkForContainer_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkForContractor.ToggleStateChanged
        If chkForContractor.Checked = True Then
            lblToLocation.Visible = True
            lblToLocationName.Visible = True
            txtToLocationCode.Visible = True
            txtToLocationCode.Enabled = False

        Else
            lblToLocation.Visible = False
            lblToLocationName.Visible = False
            txtToLocationCode.Visible = False
            txtToLocationCode.Enabled = True
        End If
    End Sub
End Class

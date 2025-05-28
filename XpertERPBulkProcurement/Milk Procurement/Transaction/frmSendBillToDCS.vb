Imports System.ComponentModel
Imports System.Data.SqlClient
Imports common
Imports System.IO
Imports System.Net

Public Class frmSendBillToDCS
#Region "Variables"
    Dim arrFilePath As List(Of String) = Nothing
    Dim settFileUpload As Boolean = False
    Dim corrFactor As Double
#End Region

    Private Sub frmSendBillToDCS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try



            txtREILToDate.Value = clsCommon.GETSERVERDATE().AddDays(-1)
            txtREILToDate.MaxDate = txtREILToDate.Value
            txtREILFromDate.Value = txtREILToDate.Value.AddDays(-1)
            GroupBox1.Visible = (clsCommon.CompairString(objCommonVar.CurrComp_Code1, "AJM") = CompairStringResult.Equal)

            'txtDate.Value = clsCommon.GETSERVERDATE()
            'txtQCDate.Value = clsCommon.GETSERVERDATE()
            'txtTankerQCDate.Value = clsCommon.GETSERVERDATE()
            'txtCrateEntryDate.Value = clsCommon.GETSERVERDATE()
            'LoadShift()
            txtSendBill.Text = 0
            txtRemainingBill.Text = 0
            btnPrintBillMobUser.Visible = MyBase.isPostFlag
            btnSendBill.Visible = MyBase.isPostFlag

            settFileUpload = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.FileUpload, clsUserMgtCode.frmSendBillToDCS, Nothing)) = 1)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndPaymentProcessDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPaymentProcessDocNo._MYValidating
        fndPaymentProcessDocNo.Value = clsPaymentProcessHead.getFinder("FarmType='PP' And isPrePosted=1", fndPaymentProcessDocNo.Value, isButtonClicked)
        If clsCommon.myLen(fndPaymentProcessDocNo.Value) > 0 Then
            CalculateSendOrRemainingBill()
        End If
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        fndPaymentProcessDocNo.Value = Nothing
        txtMultDCS.arrValueMember = Nothing
        txtSendBill.Text = 0
        txtRemainingBill.Text = 0
    End Sub

    Function ReturnDCSQry() As String
        Dim qry As String = "select TSPL_PAYMENT_PROCESS_DETAIL.Doc_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,
                                 TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE,TSPL_PAYMENT_PROCESS_DETAIL.VSP_NAME,TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No,TSPL_Vendor_Master.Zone_Code 
                                 from TSPL_PAYMENT_PROCESS_DETAIL 
                                 left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No
                                 left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                 Left Outer Join TSPL_Vendor_Master On TSPL_Vendor_Master.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
                                 where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + fndPaymentProcessDocNo.Value + "' "
        Return qry
    End Function



    Private Sub btnPrintBillMobUser_Click(sender As Object, e As EventArgs) Handles btnPrintBillMobUser.Click
        Try
            clsCommon.ProgressBarPercentShow()
            If Not settFileUpload Then
                Throw New Exception("This functionality is not for you.")
            End If
            Dim qry As String = ReturnDCSQry()
            'If chkInactive.Checked = True Then
            '    qry += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is null  "
            'Else
            qry += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is null and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO2 is null "
            'End If

            If txtMultDCS.arrValueMember IsNot Nothing AndAlso txtMultDCS.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE IN (" & clsCommon.GetMulcallString(txtMultDCS.arrValueMember) & ")"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim ii As Integer = 0
                For Each dr As DataRow In dt.Rows
                    clsCommon.ProgressBarPercentUpdate((ii + 1) * 100 / dt.Rows.Count, " Printing " & (ii + 1) & " Of " & dt.Rows.Count)
                    ProcessFile(dr) ' Process each file individually
                    ii = ii + 1
                Next
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, "Bill Print Successfully Done.", Me.Text)
                CalculateSendOrRemainingBill()
                txtMultDCS.arrValueMember = Nothing
                chkInactive.Checked = False
            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
        End Try
    End Sub

    Private Sub ProcessFile(dr As DataRow)
        Dim PDFPath As String = clsPaymentProcessHead.Load_Report_Paymnet_RCDF("'" + clsCommon.myCstr(dr("Doc_No")) + "'", clsCommon.myCDate(dr("From_Date")), clsCommon.myCDate(dr("To_Date")), "", "'" + clsCommon.myCstr(dr("VSP_CODE")) + "'", "", "", "", False, True, "", False)
        Dim qry1 As String = " Select FILE_INFO,FILE_INFO2 from TSPL_MILK_PURCHASE_INVOICE_HEAD where DOC_CODE= '" + clsCommon.myCstr(dr("Milk_Purchase_Invoice_No")) + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim fileInfo As String = clsCommon.myCstr(dt.Rows(0)("FILE_INFO"))
            Dim fileInfo2 As String = clsCommon.myCstr(dt.Rows(0)("FILE_INFO2"))
            If clsCommon.myLen(fileInfo) > 0 OrElse clsCommon.myLen(fileInfo) > 0 Then
                Exit Sub
            End If
            If PDFPath IsNot Nothing AndAlso clsCommon.myLen(PDFPath) > 0 Then
            Dim FileNo As Integer = clsAttachDocument.UploadWithHttpRequest(PDFPath, Path.GetFileName(PDFPath), clsUserMgtCode.frmMilkPurchaseInvoice, clsCommon.myCstr(dr("Milk_Purchase_Invoice_No")))
            If FileNo > 0 Then
                    If chkInactive.Checked = True Then
                        Dim qry As String = " UPDATE TSPL_MILK_PURCHASE_INVOICE_HEAD set FILE_INFO2=" + clsCommon.myCstr(FileNo) + ",Send_By = '" & objCommonVar.CurrentUserCode & "',Send_Date = '" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") & "' where DOC_CODE='" + clsCommon.myCstr(dr("Milk_Purchase_Invoice_No")) + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                    Else
                        Dim qry As String = " UPDATE TSPL_MILK_PURCHASE_INVOICE_HEAD set FILE_INFO=" + clsCommon.myCstr(FileNo) + ",Send_By = '" & objCommonVar.CurrentUserCode & "',Send_Date = '" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") & "' where DOC_CODE='" + clsCommon.myCstr(dr("Milk_Purchase_Invoice_No")) + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                    End If
            End If


        End If
        SaveFile(PDFPath, clsCommon.myCstr(dr("VSP_CODE")), clsCommon.myCstr(dr("Doc_No")), clsCommon.myCDate(dr("Doc_Date")), clsCommon.myCstr(dr("VSP_CODE")), clsCommon.myCstr(dr("VSP_NAME")), clsCommon.myCstr(dr("VLC_CODE_Uploader")), clsCommon.myCDate(dr("From_Date")), clsCommon.myCDate(dr("To_Date")))
        Else
            clsCommon.MyMessageBoxShow(Me, "Empty File Path", Me.Text)
        End If
    End Sub




    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub SaveFile(ByVal FilePath As String, ByVal Vendor_Code As String, ByVal Documnet_No As String, ByVal Document_Date As DateTime, ByVal VSP_Code As String, ByVal VSP_Name As String, ByVal VLC_Uploader_Code As String, ByVal FromDate As Date, ByVal ToDate As Date)
        Try
            If Vendor_Code IsNot Nothing AndAlso clsCommon.myLen(Vendor_Code) > 0 Then
                Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmSendBillToDCS + "'", Nothing)
                Dim Qry As String = "Select Email from TSPL_VENDOR_MASTER where Vendor_Code='" + Vendor_Code + "'"
                Dim arrMailID As List(Of String) = New List(Of String)()
                arrMailID.Add(clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry)))
                If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 AndAlso (arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Then
                    Dim objEmailH As New clsEMailHead()
                    'objEmailH.arrEMail = New List(Of String)()
                    objEmailH.arrEMail = arrMailID
                    objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                    objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_NO, Documnet_No)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(Document_Date, "dd/MMM/yyyy"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.VLCCode, VSP_Code)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.VLCName, VSP_Name)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.VLCUploaderCode, VLC_Uploader_Code)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy"))
                    objEmailH.Attachment_1_Path = FilePath
                    objEmailH.SaveData(clsUserMgtCode.frmSendBillToDCS, objEmailH, Nothing)
                    objEmailH = Nothing
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub fndMCCCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    Try
    '        Dim qry As String = ""
    '        Dim arrMCCRights As ArrayList = clsMCCCodes.GetUserHavingMCCRights()
    '        qry = "select * from ( select tspl_mcc_master.MCC_Code as [Code] ,tspl_mcc_master.MCC_Type as [Mcc Type] ,tspl_mcc_master.MCC_NAME as [Mcc Name] ,tspl_mcc_master.Chilling_Vendor as [Chilling Vendor] ,tspl_mcc_master.Add1 as [Address1] ,tspl_mcc_master.Add2 as [Address2] ,tspl_mcc_master.Tehsil as [Tehsil] ,tspl_mcc_master.City_code as [City Code] ,tspl_mcc_master.State_Code as [State Code] ,tspl_mcc_master.Country_code as [Country Code] ,tspl_mcc_master.Pin_code as [Pin Code],tspl_mcc_master.Pan_No as [Pan No] ,tspl_mcc_master.Telphone as [Telphone] ,tspl_mcc_master.Email as [Email] ,tspl_mcc_master.Fax as [Fax] ,tspl_mcc_master.MCC_Area as [Mcc Area] ,tspl_mcc_master.Area_Of_Store as [Area Of Store] ,tspl_mcc_master.Area_Of_Office as [Area Of Office] ,tspl_mcc_master.Open_Area_For_tanker as [Open Area For Tanker] ,tspl_mcc_master.Area_Of_LAB as [Area Of Lab] ,tspl_mcc_master.No_Of_SILO as [No Of Silo] ,tspl_mcc_master.Total_Storage_capacity as [Total Storage Capacity] ,tspl_mcc_master.Area_Of_Receiving_DOCK as [Area Of Receiving Dock] ,tspl_mcc_master.No_Of_Chiller as [No Of Chiller] ,tspl_mcc_master.Chiller_Brand_Name as [Chiller Brand Name] ,tspl_mcc_master.Chiller_Capacity as [Chiller Capacity] ,tspl_mcc_master.No_Of_MilkPump as [No Of Milkpump] ,tspl_mcc_master.MilkPump_Capacity as [Milkpump Capacity] ,tspl_mcc_master.DripSaver as [Drip Saver] ,tspl_mcc_master.CanWasher as [Can Washer] ,tspl_mcc_master.CanScrubber as [Can Scrubber] ,tspl_mcc_master.FSSAI_NO as [FSSAI No] ,tspl_mcc_master.ETP as [ETP] ,tspl_mcc_master.Earthing as [Earthing] ,tspl_mcc_master.Coil_Length as [Coil Length] ,tspl_mcc_master.Electricity_Connection as [Electricity Connection] ,tspl_mcc_master.Boiler as [Boiler] ,tspl_mcc_master.NoOfDG as [No. of DG] ,tspl_mcc_master.NoOfCompressor as [No. of Compressor] ,tspl_mcc_master.PayeeName as [Payee Name] ,tspl_mcc_master.BankName as [Bank Name] ,tspl_mcc_master.BankBranch as [Bank Branch] ,tspl_mcc_master.BankCityCode as [Bank City Code] ,tspl_mcc_master.BankStateCode as [Bank State Code] ,tspl_mcc_master.IFCICode as [IFCI Code] ,tspl_mcc_master.AccountNO as [Account No] ,tspl_mcc_master.Created_By as [Created By] ,tspl_mcc_master.Created_Date as [Created Date] ,tspl_mcc_master.Modified_By as [Modified By] ,tspl_mcc_master.Modified_Date as [Modified Date] ,tspl_mcc_master.Comp_Code as [Company Code],tspl_mcc_master.mcc_code_vlc_uploader as [MCC Code For VLC Uploder],tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER_PLANT.Location_Desc AS [Plant Name] from tspl_mcc_master LEFT JOIN TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_PLANT ON TSPL_LOCATION_MASTER_PLANT.Location_Code=tspl_mcc_master.Plant_Code  inner join tspl_location_master on tspl_location_master.location_Code= tspl_mcc_master.mcc_Code " _
    '        & " and (  tspl_mcc_master.mcc_Code in (" & clsCommon.GetMulcallString(arrMCCRights) & ")))xx "

    '        fndMCCCode.Value = clsCommon.ShowSelectForm("sensms@M", qry, "Code", "", fndMCCCode.Value, "Code", isButtonClicked)
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub

    'Private Sub RadButton3_Click(sender As Object, e As EventArgs)
    '    Try
    '        If clsCommon.myLen(cboShift.SelectedValue) <= 0 Then
    '            cboShift.Focus()
    '            Throw New Exception("Please select " + cboShift.MyLinkLable1.Text)
    '        End If
    '        If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
    '            fndMCCCode.Focus()
    '            Throw New Exception("Please select " + fndMCCCode.MyLinkLable1.Text)
    '        End If
    '        clsCommon.ProgressBarShow()
    '        Try
    '            clsMilkShiftEndMCC.CreateSMSContentVSP(fndMCCCode.Value, txtDate.Value, clsCommon.myCstr(cboShift.SelectedValue), Nothing)
    '            clsCommon.ProgressBarHide()
    '            clsCommon.MyMessageBoxShow(Me, "Task Completed", Me.Text)
    '        Catch ex As Exception
    '            clsCommon.ProgressBarHide()
    '            Throw New Exception(ex.Message)
    '        End Try
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub

    Private Sub txtMultDCS__My_Click(sender As Object, e As EventArgs) Handles txtMultDCS._My_Click
        Try
            If clsCommon.myLen(fndPaymentProcessDocNo.Value) > 0 Then
                Dim qry As String = "Select Doc_No As [Document Code], VSP_CODE As [DCS Code],	VSP_NAME As [DCS Name], VLC_CODE_Uploader As [DCS Uploader Code],Zone_Code As [Zone] from (" + ReturnDCSQry() + " and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is null and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO2 is null )xxx"
                txtMultDCS.arrValueMember = clsCommon.ShowMultipleSelectForm(True, "DCS@", qry, "DCS Code", "", txtMultDCS.arrValueMember, Nothing)
            Else
                clsCommon.MyMessageBoxShow(Me, "Fill Document Code.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub CalculateSendOrRemainingBill()
        Try
            'If chkInactive.Checked = True Then
            txtSendBill.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from (" & ReturnDCSQry() & " and (TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is not null or TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO2 is not null)  ) xxx"))
                txtRemainingBill.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from (" & ReturnDCSQry() & " and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is null and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO2 is null ) xxx"))
            'Else
            '    txtSendBill.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from (" & ReturnDCSQry() & " and (TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is not null or TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO2 is not null) ) xxx"))
            '    txtRemainingBill.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from (" & ReturnDCSQry() & " and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is null ) xxx"))
            'End If
            'txtSendBill.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from (" & ReturnDCSQry() & " and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is Not null ) xxx"))
            'txtRemainingBill.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from (" & ReturnDCSQry() & " and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is null ) xxx"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub txtREILBMC__My_Click(sender As Object, e As EventArgs) Handles txtREILBMC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME,Mcc_Code_VLC_Uploader as Uploader from TSPL_MCC_MASTER"
        txtREILBMC.arrValueMember = clsCommon.ShowMultipleSelectForm("MCC@ReilIn", qry, "MCC_Code", "MCC_NAME", txtREILBMC.arrValueMember, txtREILBMC.arrDispalyMember)
    End Sub

    Private Sub txtREILDCS__My_Click(sender As Object, e As EventArgs) Handles txtREILDCS._My_Click
        Dim qry As String = "select VLC_Code,VLC_Name,VLC_Code_VLC_Uploader from TSPL_VLC_MASTER_HEAD where 2=2 "
        qry += " and TSPL_VLC_MASTER_HEAD.REIL_Integrated=1 "
        If txtREILBMC.arrValueMember IsNot Nothing AndAlso txtREILBMC.arrValueMember.Count > 0 Then
            qry += " and MCC in (" + clsCommon.GetMulcallString(txtREILBMC.arrValueMember) + ")"
        End If
        txtREILDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("DCS@ReilIn", qry, "VLC_Code", "VLC_Name", txtREILDCS.arrValueMember, txtREILDCS.arrDispalyMember)
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        REILIntegration(True, True, True)
    End Sub

    Private Sub REILIntegration(isMilkcollection As Boolean, isLocalSale As Boolean, isFarmerSale As Boolean)
        Try
            Dim arrFilter As Dictionary(Of String, String) = Nothing
            Dim arrShift = New List(Of String)
            arrShift.Add("M")
            arrShift.Add("E")
            Dim ii As Integer = 1
            Dim strMsg As String = ""
            Dim qry As String = "select * from ExplodeDates('" & clsCommon.GetPrintDate(txtREILFromDate.Value, "dd/MMM/yyyy") & "','" + clsCommon.GetPrintDate(txtREILToDate.Value, "dd/MMM/yyyy") & "')"
            Dim dtDate As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim dtCollection As DataTable
            If dtDate Is Nothing OrElse dtDate.Rows.Count <= 0 Then
                Throw New Exception("Please select valid Date Range")
            End If
            qry = "select VLC_Code_VLC_Uploader,VLC_Code,VLC_Name,MCC from TSPL_VLC_MASTER_HEAD where 2=2 and TSPL_VLC_MASTER_HEAD.REIL_Integrated=1 "
            If txtREILBMC.arrValueMember IsNot Nothing AndAlso txtREILBMC.arrValueMember.Count > 0 Then
                qry += " And MCC in (" + clsCommon.GetMulcallString(txtREILBMC.arrValueMember) + ") "
            End If
            If txtREILDCS.arrValueMember IsNot Nothing AndAlso txtREILDCS.arrValueMember.Count > 0 Then
                qry += " And VLC_Code in (" + clsCommon.GetMulcallString(txtREILDCS.arrValueMember) + ") "
            End If
            Dim dtDCS As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtDCS Is Nothing OrElse dtDCS.Rows.Count <= 0 Then
                Throw New Exception("No DCS Found")
            End If
            Dim totalNo As Integer = dtDate.Rows.Count * dtDCS.Rows.Count
            clsCommon.ProgressBarPercentShow()
            Try
                For Each drDate As DataRow In dtDate.Rows
                    Dim dtStart As Date = clsCommon.myCDate(drDate("thedate"))
                    txtREILFromDate.Value = dtStart
                    For Each drDCS As DataRow In dtDCS.Rows
                        ii += 1
                        Dim strVLCUploader As String = clsCommon.myCstr(drDCS("VLC_Code_VLC_Uploader"))
                        Dim strMCC As String = clsCommon.myCstr(drDCS("MCC"))
                        For Each strShift As String In arrShift
                            If isMilkcollection Then
                                qry = "select top 1 Doc_No from TSPL_VLC_DATA_UPLOADER where MCC_Code ='" & strMCC & "' and VLC_CODE='" & clsCommon.myCstr(strVLCUploader) & "' and Doc_Date='" + clsCommon.GetPrintDate(dtStart, "dd/MMM/yyyy") & "' and shift='" & strShift & "' "
                                Dim docCode As String = clsDBFuncationality.getSingleValue(qry)
                                If clsCommon.myLen(docCode) <= 0 Then
                                    clsCommon.ProgressBarPercentUpdate(ii, totalNo, "MP Collection [" & clsCommon.GetPrintDate(dtStart, "dd/MM/yyyy") & "][" & strShift & "][" & strVLCUploader & "][" + clsCommon.myCstr(drDCS("VLC_Name")) & "]")
                                    arrFilter = New Dictionary(Of String, String)()
                                    arrFilter.Add("master", "01")
                                    arrFilter.Add("dcscode", clsCommon.myPadChar(strVLCUploader, 6, "0"))
                                    arrFilter.Add("date", clsCommon.GetPrintDate(dtStart, "dd/MM/yyyy"))
                                    arrFilter.Add("shift", strShift)
                                    dtCollection = Xtra.GetDataFromAPI(EnumAPI.REIL, "", arrFilter)
                                    If dtCollection IsNot Nothing AndAlso dtCollection.Rows.Count > 0 Then
                                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                                        Dim dtServDate As DateTime = clsCommon.GETSERVERDATE(trans)
                                        Try
                                            docCode = clsERPFuncationality.GetNextCode(trans, dtStart, clsDocType.vlcDataUploader, "", strMCC)
                                            If clsCommon.myLen(docCode) <= 0 Then
                                                Throw New Exception("Error in code generation [VLC Data Uploader]")
                                            End If
                                            For Each drCollection As DataRow In dtCollection.Rows
                                                Try
                                                    Dim strRouteNo As String = ""
                                                    Dim MPUploaderNo As String = clsCommon.myCstr(clsCommon.myCDecimal(clsCommon.myCstr(drCollection("MPCode")).Substring(clsCommon.myLen(drCollection("MPCode")) - 4, 4)))
                                                    Try
                                                        Dim dtMasterData As DataTable = GetMasterDataQryMPCode(clsCommon.myCstr(drCollection("MPCode")), MPUploaderNo, clsCommon.myCstr(drDCS("VLC_Code")), trans)
                                                        If dtMasterData Is Nothing OrElse dtMasterData.Rows.Count <= 0 Then
                                                            CreateNewMP(clsCommon.myCstr(drDCS("VLC_Code")), strVLCUploader, clsCommon.myCstr(drCollection("MPCode")), MPUploaderNo, trans)
                                                            dtMasterData = GetMasterDataQryMPCode(clsCommon.myCstr(drCollection("MPCode")), "", "", trans)
                                                        End If
                                                        If dtMasterData Is Nothing OrElse dtMasterData.Rows.Count <= 0 Then
                                                            Throw New Exception("Invalid MPCode [" & clsCommon.myCstr(drCollection("MPCode")) & "]")
                                                        ElseIf dtMasterData.Rows.Count > 1 Then
                                                            Throw New Exception("MPCode [" & clsCommon.myCstr(drCollection("MPCode")) & "] is not unique.")
                                                        End If
                                                        strRouteNo = clsCommon.myCstr(dtMasterData.Rows(0)("Route_Code"))
                                                        MPUploaderNo = clsCommon.myCstr(dtMasterData.Rows(0)("MP_Code_VLC_Uploader"))
                                                    Catch ex As Exception
                                                        strRouteNo = "REIL"
                                                        MPUploaderNo = clsCommon.myCstr(clsCommon.myCDecimal(clsCommon.myCstr(drCollection("MPCode")).Substring(clsCommon.myLen(drCollection("MPCode")) - 4, 4)))
                                                        strMsg += ex.Message + Environment.NewLine
                                                    End Try

                                                    Dim coll As Hashtable = New Hashtable()
                                                    clsCommon.AddColumnsForChange(coll, "Mcc_code", strMCC)
                                                    clsCommon.AddColumnsForChange(coll, "Doc_No", docCode)
                                                    clsCommon.AddColumnsForChange(coll, "doc_date", clsCommon.GetPrintDate(dtStart, "dd/MMM/yyyy hh:mm:ss tt"))
                                                    clsCommon.AddColumnsForChange(coll, "file_Date", clsCommon.GetPrintDate(dtStart, "dd/MMM/yyyy hh:mm:ss tt"))
                                                    clsCommon.AddColumnsForChange(coll, "Shift", strShift)
                                                    clsCommon.AddColumnsForChange(coll, "Mp_code", MPUploaderNo)
                                                    clsCommon.AddColumnsForChange(coll, "Vlc_code", clsCommon.myCstr(strVLCUploader))
                                                    clsCommon.AddColumnsForChange(coll, "Route_no", strRouteNo)
                                                    clsCommon.AddColumnsForChange(coll, "Milk_type", clsCommon.myCstr(drCollection("MilkType")))
                                                    clsCommon.AddColumnsForChange(coll, "Qty", clsCommon.myCdbl(drCollection("Qty")))
                                                    clsCommon.AddColumnsForChange(coll, "Fat", clsCommon.myCdbl(drCollection("FAT")))
                                                    clsCommon.AddColumnsForChange(coll, "snf", clsCommon.myCdbl(drCollection("SNF")))
                                                    clsCommon.AddColumnsForChange(coll, "Fat_Kg", ((clsCommon.myCdbl(drCollection("Qty")) * clsCommon.myCdbl(drCollection("FAT"))) / 100.0))
                                                    clsCommon.AddColumnsForChange(coll, "snf_Kg", ((clsCommon.myCdbl(drCollection("Qty")) * clsCommon.myCdbl(drCollection("SNF"))) / 100.0))
                                                    clsCommon.AddColumnsForChange(coll, "Uom_COde", "Ltr")
                                                    clsCommon.AddColumnsForChange(coll, "Rate", clsCommon.myCdbl(drCollection("Rate")))
                                                    clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCdbl(drCollection("Amount")))
                                                    clsCommon.AddColumnsForChange(coll, "water", 0)
                                                    clsCommon.AddColumnsForChange(coll, "Created_By", "REIL")
                                                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(dtServDate, "dd/MMM/yyyy hh:mm tt"))
                                                    clsCommon.AddColumnsForChange(coll, "Modified_By", "REIL")
                                                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(dtServDate, "dd/MMM/yyyy hh:mm tt"))
                                                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                                                    clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", 0)
                                                    clsCommon.AddColumnsForChange(coll, "Entry_Source", "REIL")
                                                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_DATA_UPLOADER", OMInsertOrUpdate.Insert, "", trans)
                                                Catch ex As Exception
                                                    strMsg += ex.Message + Environment.NewLine
                                                    Continue For
                                                End Try
                                            Next
                                            trans.Commit()
                                        Catch ex As Exception
                                            trans.Rollback()
                                            Throw New Exception(ex.Message)
                                        End Try
                                    End If
                                End If
                            End If

                            If isLocalSale Then
                                clsCommon.ProgressBarPercentUpdate(ii, totalNo, "Local Sale [" & clsCommon.GetPrintDate(dtStart, "dd/MM/yyyy") & "][" & strShift & "][" & strVLCUploader & "][" + clsCommon.myCstr(drDCS("VLC_Name")) & "]")
                                arrFilter = New Dictionary(Of String, String)()
                                arrFilter.Add("master", "03")
                                arrFilter.Add("dcscode", clsCommon.myPadChar(strVLCUploader, 6, "0"))
                                arrFilter.Add("date", clsCommon.GetPrintDate(dtStart, "dd/MM/yyyy"))
                                arrFilter.Add("shift", strShift)
                                dtCollection = Xtra.GetDataFromAPI(EnumAPI.REIL, "", arrFilter)
                                If dtCollection IsNot Nothing AndAlso dtCollection.Rows.Count > 0 Then
                                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                                    Try
                                        qry = "select PK_ID from TSPL_REIL_LOCAL_MILK_SALE where VLC_CODE='" & clsCommon.myCstr(drDCS("VLC_Code")) & "' and Doc_Date='" + clsCommon.GetPrintDate(dtStart, "dd/MMM/yyyy") & "' and shift='" & strShift & "' "
                                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                            Dim coll As Hashtable = New Hashtable()
                                            clsCommon.AddColumnsForChange(coll, "VLC_Code", clsCommon.myCstr(drDCS("VLC_Code")))
                                            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(dtStart, "dd/MMM/yyyy hh:mm:ss tt"))
                                            clsCommon.AddColumnsForChange(coll, "Shift", strShift)
                                            clsCommon.AddColumnsForChange(coll, "Created_By", "REIL")
                                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                                            clsCommon.AddColumnsForChange(coll, "Modify_By", "REIL")
                                            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REIL_LOCAL_MILK_SALE", OMInsertOrUpdate.Insert, "", trans)

                                            Dim intPKID As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select SCOPE_IDENTITY()", trans))
                                            For Each drCollection As DataRow In dtCollection.Rows
                                                coll = New Hashtable()
                                                clsCommon.AddColumnsForChange(coll, "REF_PK_ID", intPKID)
                                                clsCommon.AddColumnsForChange(coll, "Milk_Type", clsCommon.myCstr(drCollection("MilkType")))
                                                clsCommon.AddColumnsForChange(coll, "Qty", clsCommon.myCDecimal(drCollection("Qty")))
                                                clsCommon.AddColumnsForChange(coll, "Fat", clsCommon.myCDecimal(drCollection("FAT")))
                                                clsCommon.AddColumnsForChange(coll, "snf", clsCommon.myCDecimal(drCollection("SNF")))
                                                clsCommon.AddColumnsForChange(coll, "Rate", clsCommon.myCDecimal(drCollection("Rate")))
                                                clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCDecimal(drCollection("Amount")))
                                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REIL_LOCAL_MILK_SALE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                                            Next
                                        End If
                                        trans.Commit()
                                    Catch ex As Exception
                                        trans.Rollback()
                                        Throw New Exception(ex.Message)
                                    End Try
                                End If
                            End If
                        Next

                        If isFarmerSale Then
                            clsCommon.ProgressBarPercentUpdate(ii, totalNo, "Farmer Sale [" & clsCommon.GetPrintDate(dtStart, "dd/MM/yyyy") & "][" & strVLCUploader & "][" + clsCommon.myCstr(drDCS("VLC_Name")) & "]")
                            arrFilter = New Dictionary(Of String, String)()
                            arrFilter.Add("master", "04")
                            arrFilter.Add("dcscode", clsCommon.myPadChar(strVLCUploader, 6, "0"))
                            arrFilter.Add("date", clsCommon.GetPrintDate(dtStart, "dd/MM/yyyy"))
                            dtCollection = Xtra.GetDataFromAPI(EnumAPI.REIL, "", arrFilter)
                            If dtCollection IsNot Nothing AndAlso dtCollection.Rows.Count > 0 Then
                                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                                Try
                                    For Each drCollection As DataRow In dtCollection.Rows
                                        qry = "select Doc_No from TSPL_REIL_FARMER_SALE where Doc_No='" & clsCommon.myCstr(drCollection("DocNo")) & "' "
                                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                            Dim coll As Hashtable = New Hashtable()
                                            clsCommon.AddColumnsForChange(coll, "Doc_No", clsCommon.myCstr(drCollection("DocNo")))
                                            clsCommon.AddColumnsForChange(coll, "DocDate", clsCommon.myConvertDateFormat(clsCommon.myCstr(drCollection("DocDate"))))
                                            clsCommon.AddColumnsForChange(coll, "VLC_Code", clsCommon.myCstr(drDCS("VLC_Code")))
                                            clsCommon.AddColumnsForChange(coll, "MPUploaderCode", clsCommon.myCstr(clsCommon.myCDecimal((drCollection("MPCode")))))
                                            clsCommon.AddColumnsForChange(coll, "ItemCode", clsCommon.myCstr(drCollection("ItemCode")))
                                            clsCommon.AddColumnsForChange(coll, "ItemName", clsCommon.myCstr(drCollection("ItemName")))
                                            clsCommon.AddColumnsForChange(coll, "UOM", clsCommon.myCstr(drCollection("UOM")))
                                            clsCommon.AddColumnsForChange(coll, "Qty", clsCommon.myCDecimal(drCollection("Qty")))
                                            clsCommon.AddColumnsForChange(coll, "Rate", clsCommon.myCDecimal(drCollection("Rate")))
                                            clsCommon.AddColumnsForChange(coll, "Discount", clsCommon.myCDecimal(drCollection("Discount")))
                                            clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCDecimal(drCollection("Amount")))
                                            clsCommon.AddColumnsForChange(coll, "Created_By", "REIL")
                                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                                            clsCommon.AddColumnsForChange(coll, "Modify_By", "REIL")
                                            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REIL_FARMER_SALE", OMInsertOrUpdate.Insert, "", trans)
                                        End If
                                    Next
                                    trans.Commit()
                                Catch ex As Exception
                                    trans.Rollback()
                                    Throw New Exception(ex.Message)
                                End Try
                            End If
                        End If
                    Next
                Next
                clsCommon.ProgressBarPercentHide()

                If clsCommon.myLen(strMsg) Then
                    clsCommon.MyMessageBoxShow(Me, strMsg, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Info)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Task Completed", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Info)
                End If
            Catch ex As Exception
                clsCommon.ProgressBarPercentHide()
                Throw New Exception(ex.Message)
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub CreateNewMP(strVLCCode As String, strVLCUploader As String, strMPCode As String, MPUploaderNo As String, trans As SqlTransaction)
        Dim arrFilter As New Dictionary(Of String, String)()
        arrFilter.Add("master", "02")
        arrFilter.Add("dcscode", clsCommon.myPadChar(strVLCUploader, 6, "0"))
        arrFilter.Add("mpcode", strMPCode)
        Dim dtMPMaster As DataTable = Xtra.GetDataFromAPI(EnumAPI.REIL, "", arrFilter)
        If dtMPMaster Is Nothing OrElse dtMPMaster.Rows.Count <= 0 Then
            Throw New Exception("MP Details not found from service [" + strMPCode + "]")
        End If
        Dim obj = New clsMpMaster()
        obj.isNewEntry = True
        Dim dtServer As DateTime = clsCommon.GETSERVERDATE(trans)
        obj.MP_Code = clsCommon.myCstr(dtMPMaster.Rows(0)("MPCode"))
        obj.MP_Name = clsCommon.myCstr(dtMPMaster.Rows(0)("MPName"))
        obj.MP_CODE_VLC_UPLOADER = MPUploaderNo
        obj.MCC_Code = strVLCCode
        obj.Modified_By = "REIL"
        obj.Modified_Date = clsCommon.GetPrintDate(dtServer, "dd/MMM/yyyy")
        obj.Comp_Code = objCommonVar.CurrentCompanyCode
        obj.Created_By = "REIL"
        obj.Created_Date = clsCommon.GetPrintDate(dtServer, "dd/MMM/yyyy")
        obj.Form_Id = clsUserMgtCode.frmMPMaster
        obj.Father_Name = clsCommon.myCstr(dtMPMaster.Rows(0)("FatherHusbandName"))
        obj.Gender = clsCommon.myCstr(dtMPMaster.Rows(0)("Gender"))
        obj.Add1 = clsCommon.myCstr(dtMPMaster.Rows(0)("Address"))
        obj.Telphone = clsCommon.myCstr(dtMPMaster.Rows(0)("PhoneNo"))
        obj.Fax = clsCommon.myCstr(dtMPMaster.Rows(0)("AdhaarNo"))
        obj.BankName = clsCommon.myCstr(dtMPMaster.Rows(0)("BankName"))
        obj.IFCICode = clsCommon.myCstr(dtMPMaster.Rows(0)("IFSCCode"))
        obj.AccountNO = clsCommon.myCstr(dtMPMaster.Rows(0)("BankAccountNo"))
        obj.Jan_Aadhar_No = clsCommon.myCstr(dtMPMaster.Rows(0)("JanaadhaarNo"))
        clsMpMaster.SaveData(obj, trans)
    End Sub

    Private Function GetMasterDataQryMPCode(ByVal strMPCode As String, ByVal MPUploaderNo As String, ByVal VLCCode As String, trans As SqlTransaction) As DataTable
        Dim qry As String = "select TSPL_MP_MASTER.MP_Code_VLC_Uploader,TSPL_MP_MASTER.MP_Code,TSPL_MP_MASTER.MP_Name,TSPL_MP_MASTER.VLC_Code,TSPL_VLC_MASTER_HEAD.VLc_Code_vlc_uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code ,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME
from TSPL_MP_MASTER
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.MCC
where TSPL_MP_MASTER.MP_Code ='" & strMPCode & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            qry = "select TSPL_MP_MASTER.MP_Code_VLC_Uploader,TSPL_MP_MASTER.MP_Code,TSPL_MP_MASTER.MP_Name,TSPL_MP_MASTER.VLC_Code,TSPL_VLC_MASTER_HEAD.VLc_Code_vlc_uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code ,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME
from TSPL_MP_MASTER
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.MCC
where TSPL_MP_MASTER.VLC_Code ='" & VLCCode & "' and TSPL_MP_MASTER.MP_Code_VLC_Uploader='" + MPUploaderNo + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
        End If
        Return dt
    End Function

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        REILIntegration(True, False, False)
    End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        REILIntegration(False, True, False)
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        REILIntegration(False, False, True)
    End Sub
End Class


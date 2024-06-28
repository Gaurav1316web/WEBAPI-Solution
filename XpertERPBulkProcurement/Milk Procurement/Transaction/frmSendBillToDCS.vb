Imports System.Data.SqlClient
Imports common
Imports System.IO
Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Collections.Specialized
Public Class frmSendBillToDCS
#Region "Variables"
    Dim arrFilePath As List(Of String) = Nothing
#End Region

    Private Sub frmSendBillToDCS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDate.Value = clsCommon.GETSERVERDATE()
        LoadShift()
        txtSendBill.Text = 0
        txtRemainingBill.Text = 0
    End Sub
    Public Sub LoadShift()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)

        cboShift.DataSource = dt
        cboShift.ValueMember = "Code"
        cboShift.DisplayMember = "Name"
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


    'Private Sub btnPrintBillMobUser_Click(sender As Object, e As EventArgs) Handles btnPrintBillMobUser.Click
    '    Try
    '        Dim qry As String = ReturnDCSQry()
    '        qry += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is null "
    '        If txtMultDCS.arrValueMember IsNot Nothing AndAlso txtMultDCS.arrValueMember.Count > 0 Then
    '            qry += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE IN (" & clsCommon.GetMulcallString(txtMultDCS.arrValueMember) & ")"
    '        End If

    '        'Dim qry As String = "select top 2 TSPL_PAYMENT_PROCESS_DETAIL.Doc_No,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No from TSPL_PAYMENT_PROCESS_DETAIL 
    '        'left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No
    '        'left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
    '        'where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='PPR/2324/000032' and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is null"

    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            clsCommon.ProgressBarPercentShow()
    '            Dim ii As Integer = 0
    '            For Each dr As DataRow In dt.Rows
    '                clsCommon.ProgressBarPercentUpdate((ii + 1) * 100 / dt.Rows.Count, " " & " Printing " & (ii + 1) & " Of " & dt.Rows.Count)
    '                Dim PDFPath As String = clsPaymentProcessHead.Load_Report_Paymnet_RCDF("'" + clsCommon.myCstr(clsCommon.myCstr(dr("Doc_No"))) + "'", clsCommon.myCDate(clsCommon.myCstr(dr("From_Date"))), clsCommon.myCDate(clsCommon.myCstr(dr("To_Date"))), "", "'" + clsCommon.myCstr(dr("VSP_CODE")) + "'", "", "", "", False, True)
    '                Dim Str As String = clsAttachDocument.UploadWithHttpRequest(PDFPath, Path.GetFileName(PDFPath))
    '                Dim jObj As JObject = JObject.Parse(Str)
    '                Dim ArrJ As JArray = Nothing
    '                If clsCommon.CompairString(clsCommon.myCstr(jObj.SelectToken("result")), "true") = CompairStringResult.Equal Then
    '                    ArrJ = JArray.Parse(clsCommon.myCstr(jObj.SelectToken("data")))
    '                    If clsCommon.myCDecimal(ArrJ(0).SelectToken("Result")) > 0 Then
    '                        Dim FileNo As Integer = clsCommon.myCDecimal(ArrJ(0).SelectToken("Result"))
    '                        If FileNo > 0 Then
    '                            Str = " UPDATE TSPL_MILK_PURCHASE_INVOICE_HEAD set FILE_INFO=" + clsCommon.myCstr(FileNo) + " where DOC_CODE='" + clsCommon.myCstr(dr("Milk_Purchase_Invoice_No")) + "'"
    '                            clsDBFuncationality.ExecuteNonQuery(Str)
    '                        End If
    '                    Else
    '                        Throw New Exception(ArrJ(0).SelectToken("Message"))
    '                    End If
    '                Else
    '                    ArrJ = JArray.Parse(clsCommon.myCstr(jObj.SelectToken("data")))
    '                    Throw New Exception(ArrJ(0).SelectToken("Message"))
    '                End If
    '                SaveFile(PDFPath, clsCommon.myCstr(dr("VSP_CODE")), clsCommon.myCstr(dr("Doc_No")), clsCommon.myCDate(dr("Doc_Date")), clsCommon.myCstr(dr("VSP_CODE")), clsCommon.myCstr(dr("VSP_NAME")), clsCommon.myCstr(dr("VLC_CODE_Uploader")), clsCommon.myCDate(dr("From_Date")), clsCommon.myCDate(dr("To_Date")))
    '                ii = ii + 1
    '            Next
    '            clsCommon.ProgressBarPercentHide()
    '            clsCommon.MyMessageBoxShow(Me, "Bill Print Successfully Done.", Me.Text)
    '            CalculateSendOrRemainingBill()
    '            txtMultDCS.arrValueMember = Nothing
    '        End If
    '    Catch ex As Exception
    '        clsCommon.ProgressBarPercentHide()
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub
    Private Sub btnPrintBillMobUser_Click(sender As Object, e As EventArgs) Handles btnPrintBillMobUser.Click
        Try
            Dim qry As String = ReturnDCSQry()
            qry += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is null "
            If txtMultDCS.arrValueMember IsNot Nothing AndAlso txtMultDCS.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE IN (" & clsCommon.GetMulcallString(txtMultDCS.arrValueMember) & ")"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsCommon.ProgressBarPercentShow()
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
        Dim PDFPath As String = clsPaymentProcessHead.Load_Report_Paymnet_RCDF("'" + clsCommon.myCstr(clsCommon.myCstr(dr("Doc_No"))) + "'", clsCommon.myCDate(clsCommon.myCstr(dr("From_Date"))), clsCommon.myCDate(clsCommon.myCstr(dr("To_Date"))), "", "'" + clsCommon.myCstr(dr("VSP_CODE")) + "'", "", "", "", False, True, "", False)
        If PDFPath IsNot Nothing AndAlso clsCommon.myLen(PDFPath) > 0 Then
            Dim Str As String = clsAttachDocument.UploadWithHttpRequest(PDFPath, Path.GetFileName(PDFPath))
            Dim jObj As JObject = JObject.Parse(Str)
            Dim ArrJ As JArray = Nothing
            Try
                If clsCommon.CompairString(clsCommon.myCstr(jObj.SelectToken("result")), "true") = CompairStringResult.Equal Then
                    ArrJ = JArray.Parse(clsCommon.myCstr(jObj.SelectToken("data")))
                    If clsCommon.myCDecimal(ArrJ(0).SelectToken("Result")) > 0 Then
                        Dim FileNo As Integer = clsCommon.myCDecimal(ArrJ(0).SelectToken("Result"))
                        If FileNo > 0 Then
                            Str = " UPDATE TSPL_MILK_PURCHASE_INVOICE_HEAD set FILE_INFO=" + clsCommon.myCstr(FileNo) + " where DOC_CODE='" + clsCommon.myCstr(dr("Milk_Purchase_Invoice_No")) + "'"
                            clsDBFuncationality.ExecuteNonQuery(Str)
                        End If
                    Else
                        Throw New Exception(ArrJ(0).SelectToken("Message"))
                    End If
                Else
                    ArrJ = JArray.Parse(clsCommon.myCstr(jObj.SelectToken("data")))
                    Throw New Exception(ArrJ(0).SelectToken("Message"))
                End If
            Finally
                ' Dispose objects to release memory
                If jObj IsNot Nothing Then
                    jObj = Nothing
                End If
                If ArrJ IsNot Nothing Then
                    ArrJ = Nothing
                End If
            End Try

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

    Private Sub fndMCCCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndMCCCode._MYValidating
        Try
            Dim qry As String = ""
            Dim arrLoc As String = ""
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(True)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 1 Then
                arrLoc = "'" + obj.Default_LocCode + "'"
            Else
                arrLoc = obj.arrLocCodes
            End If
            qry = "select * from ( select tspl_mcc_master.MCC_Code as [Code] ,tspl_mcc_master.MCC_Type as [Mcc Type] ,tspl_mcc_master.MCC_NAME as [Mcc Name] ,tspl_mcc_master.Chilling_Vendor as [Chilling Vendor] ,tspl_mcc_master.Add1 as [Address1] ,tspl_mcc_master.Add2 as [Address2] ,tspl_mcc_master.Tehsil as [Tehsil] ,tspl_mcc_master.City_code as [City Code] ,tspl_mcc_master.State_Code as [State Code] ,tspl_mcc_master.Country_code as [Country Code] ,tspl_mcc_master.Pin_code as [Pin Code],tspl_mcc_master.Pan_No as [Pan No] ,tspl_mcc_master.Telphone as [Telphone] ,tspl_mcc_master.Email as [Email] ,tspl_mcc_master.Fax as [Fax] ,tspl_mcc_master.MCC_Area as [Mcc Area] ,tspl_mcc_master.Area_Of_Store as [Area Of Store] ,tspl_mcc_master.Area_Of_Office as [Area Of Office] ,tspl_mcc_master.Open_Area_For_tanker as [Open Area For Tanker] ,tspl_mcc_master.Area_Of_LAB as [Area Of Lab] ,tspl_mcc_master.No_Of_SILO as [No Of Silo] ,tspl_mcc_master.Total_Storage_capacity as [Total Storage Capacity] ,tspl_mcc_master.Area_Of_Receiving_DOCK as [Area Of Receiving Dock] ,tspl_mcc_master.No_Of_Chiller as [No Of Chiller] ,tspl_mcc_master.Chiller_Brand_Name as [Chiller Brand Name] ,tspl_mcc_master.Chiller_Capacity as [Chiller Capacity] ,tspl_mcc_master.No_Of_MilkPump as [No Of Milkpump] ,tspl_mcc_master.MilkPump_Capacity as [Milkpump Capacity] ,tspl_mcc_master.DripSaver as [Drip Saver] ,tspl_mcc_master.CanWasher as [Can Washer] ,tspl_mcc_master.CanScrubber as [Can Scrubber] ,tspl_mcc_master.FSSAI_NO as [FSSAI No] ,tspl_mcc_master.ETP as [ETP] ,tspl_mcc_master.Earthing as [Earthing] ,tspl_mcc_master.Coil_Length as [Coil Length] ,tspl_mcc_master.Electricity_Connection as [Electricity Connection] ,tspl_mcc_master.Boiler as [Boiler] ,tspl_mcc_master.NoOfDG as [No. of DG] ,tspl_mcc_master.NoOfCompressor as [No. of Compressor] ,tspl_mcc_master.PayeeName as [Payee Name] ,tspl_mcc_master.BankName as [Bank Name] ,tspl_mcc_master.BankBranch as [Bank Branch] ,tspl_mcc_master.BankCityCode as [Bank City Code] ,tspl_mcc_master.BankStateCode as [Bank State Code] ,tspl_mcc_master.IFCICode as [IFCI Code] ,tspl_mcc_master.AccountNO as [Account No] ,tspl_mcc_master.Created_By as [Created By] ,tspl_mcc_master.Created_Date as [Created Date] ,tspl_mcc_master.Modified_By as [Modified By] ,tspl_mcc_master.Modified_Date as [Modified Date] ,tspl_mcc_master.Comp_Code as [Company Code],tspl_mcc_master.mcc_code_vlc_uploader as [MCC Code For VLC Uploder],tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER_PLANT.Location_Desc AS [Plant Name] from tspl_mcc_master LEFT JOIN TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_PLANT ON TSPL_LOCATION_MASTER_PLANT.Location_Code=tspl_mcc_master.Plant_Code  inner join tspl_location_master on tspl_location_master.location_Code= tspl_mcc_master.mcc_Code " _
            & " and (tspl_location_master.loc_segment_Code in (" & arrLoc & ") or tspl_mcc_master.mcc_Code in (" & arrLoc & ")))xx "

            fndMCCCode.Value = clsCommon.ShowSelectForm("sensms@M", qry, "Code", "", fndMCCCode.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        Try
            If clsCommon.myLen(cboShift.SelectedValue) <= 0 Then
                cboShift.Focus()
                Throw New Exception("Please select " + cboShift.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
                fndMCCCode.Focus()
                Throw New Exception("Please select " + fndMCCCode.MyLinkLable1.Text)
            End If
            clsCommon.ProgressBarShow()
            Try
                clsMilkShiftEndMCC.CreateSMSContentVSP(fndMCCCode.Value, txtDate.Value, clsCommon.myCstr(cboShift.SelectedValue), Nothing)
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Task Completed", Me.Text)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                Throw New Exception(ex.Message)
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultDCS__My_Click(sender As Object, e As EventArgs) Handles txtMultDCS._My_Click
        Try
            If clsCommon.myLen(fndPaymentProcessDocNo.Value) > 0 Then
                Dim qry As String = "Select Doc_No As [Document Code], VSP_CODE As [DCS Code],	VSP_NAME As [DCS Name], VLC_CODE_Uploader As [DCS Uploader Code],Zone_Code As [Zone] from (" + ReturnDCSQry() + " and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is null )xxx"
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
            txtSendBill.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from (" & ReturnDCSQry() & " and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is Not null ) xxx"))
            txtRemainingBill.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from (" & ReturnDCSQry() & " and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is null ) xxx"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
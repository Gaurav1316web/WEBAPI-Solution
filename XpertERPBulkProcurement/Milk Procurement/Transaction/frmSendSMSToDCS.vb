Imports System.ComponentModel
Imports System.Data.SqlClient
Imports common
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class frmSendSMSToDCS
#Region "Variables"
    Dim arrFilePath As List(Of String) = Nothing
    Dim settFileUpload As Boolean = False
    Dim corrFactor As Double
#End Region

    Private Sub frmSendSMSToDCS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtDate.Value = clsCommon.GETSERVERDATE()
            fromDate.Value = clsCommon.GETSERVERDATE()
            ToDate.Value = clsCommon.GETSERVERDATE()
            txtQCDate.Value = clsCommon.GETSERVERDATE()
            txtTankerQCDate.Value = clsCommon.GETSERVERDATE()
            txtCrateEntryDate.Value = clsCommon.GETSERVERDATE()
            txtTankerQCDateException.Value = clsCommon.GETSERVERDATE()
            txtDateQC.Value = clsCommon.GETSERVERDATE()
            LoadShift()

            settFileUpload = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.FileUpload, clsUserMgtCode.frmSendSMSToDCS, Nothing)) = 1)
            'createAllTable()
            RadButton1.Visible = MyBase.isPostFlag
            RadButton3.Visible = MyBase.isPostFlag
            btnSendBMC.Visible = MyBase.isPostFlag
            btnTankerSMS.Visible = MyBase.isPostFlag
            btnSendSMSCrateEntry.Visible = MyBase.isPostFlag
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub createAllTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("DOC_CODE", "Varchar(30) not null Primary key")
        coll.Add("MCC_CODE", "Varchar(30) not null references TSPL_MCC_MASTER(MCC_CODE)")
        coll.Add("DOC_DATE", "datetime NOT NULL")
        coll.Add("SHIFT", "VARCHAR(10) NOT NULL")
        coll.Add("COMM_PORT", "VARCHAR(30) NULL")
        coll.Add("VLC_DOC_CODE", "VARCHAR(30) NOT NULL ")
        'coll.Add("MILK_SAMPLE_CODE", "VARCHAR(30) NULL references TSPL_MILK_SAMPLE_HEAD(Doc_CODE)")
        coll.Add("SAMPLE_NO", "INTEGER NOT NULL ")
        coll.Add("VLC_CODE", "VARCHAR(30) NOT NULL REFERENCES TSPL_VLC_MASTER_HEAD(VLC_CODE)")
        coll.Add("ROUTE_CODE", "VARCHAR(30) NOT NULL ")
        coll.Add("VSP_CODE", "varchar(12) not null REFERENCES TSPL_VENDOR_MASTER (Vendor_Code)")
        coll.Add("VEHICLE_CODE", "VARCHAR(30) NULL")
        coll.Add("Transporter", "varchar(12) not null REFERENCES TSPL_VENDOR_MASTER (Vendor_Code) ")
        coll.Add("Posted", "numeric(2) not null default 0")
        coll.Add("Posting_Date", "datetime null")
        coll.Add("Created_By", "varchar(12) NOT NULL")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        coll.Add("Comp_Code", "varchar(8) NULL REFERENCES TSPL_COMPANY_MASTER(COMP_CODE)")
        coll.Add("Is_Incentive_Created", "VARCHAR(1) NOT NULL Default 'N'")
        'coll.Add("Against_Reject_No", "Varchar(30) null references TSPL_MILK_REJECT_HEAD(DOC_CODE)")
        coll.Add("Dock_Collection_Milk_Type", "char(1) NOT NULL Default 'M'")
        coll.Add("SYNC_STATUS", "int Null")
        coll.Add("Failed_Sample_Status", "integer null")
        coll.Add("Failed_Sample_Approve_By", "varchar(12) null")
        coll.Add("Failed_Sample_Approve_Date", "datetime null")
        coll.Add("Purchase_Order_No", "Varchar(30) null")
        coll.Add("Capping_Apply", "integer null")
        coll.Add("Retesting", "integer null")
        coll.Add("Against_Send_SMS", "integer NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MILK_SRN_HEAD", coll, "", True, False, "", "DOC_CODE", "DOC_DATE")
        Dim qry = "select 1 from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='TSPL_MILK_SRN_HEAD' and COLUMN_NAME='Against_Send_SMS'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            clsDBFuncationality.ExecuteNonQuery("CREATE UNIQUE INDEX Unique_Against_Send_SMS ON TSPL_MILK_SRN_HEAD(Against_Send_SMS) WHERE Against_Send_SMS IS NOT NULL ")
        End If
        coll.Item("MILK_SAMPLE_CODE") = "VARCHAR(30) NULL "
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MILK_SRN_HEAD_SYNC", coll, "", False, False)
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

        cboShiftCrateEntry.DataSource = dt
        cboShiftCrateEntry.ValueMember = "Code"
        cboShiftCrateEntry.DisplayMember = "Name"
        cboShiftCrateEntry.SelectedValue = "E"
    End Sub
    'Private Sub fndPaymentProcessDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPaymentProcessDocNo._MYValidating
    '    fndPaymentProcessDocNo.Value = clsPaymentProcessHead.getFinder("FarmType='PP' And isPrePosted=1", fndPaymentProcessDocNo.Value, isButtonClicked)
    '    If clsCommon.myLen(fndPaymentProcessDocNo.Value) > 0 Then
    '        CalculateSendOrRemainingBill()
    '    End If
    'End Sub

    'Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
    '    fndPaymentProcessDocNo.Value = Nothing
    '    txtMultDCS.arrValueMember = Nothing
    '    txtSendBill.Text = 0
    '    txtRemainingBill.Text = 0
    'End Sub

    'Function ReturnDCSQry() As String
    '    Dim qry As String = "select TSPL_PAYMENT_PROCESS_DETAIL.Doc_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,
    '                             TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE,TSPL_PAYMENT_PROCESS_DETAIL.VSP_NAME,TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No,TSPL_Vendor_Master.Zone_Code 
    '                             from TSPL_PAYMENT_PROCESS_DETAIL 
    '                             left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No
    '                             left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
    '                             Left Outer Join TSPL_Vendor_Master On TSPL_Vendor_Master.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
    '                             where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + fndPaymentProcessDocNo.Value + "' "
    '    Return qry
    'End Function



    'Private Sub btnPrintBillMobUser_Click(sender As Object, e As EventArgs) Handles btnPrintBillMobUser.Click
    '    Try
    '        clsCommon.ProgressBarPercentShow()
    '        If Not settFileUpload Then
    '            Throw New Exception("This functionality is not for you.")
    '        End If
    '        Dim qry As String = ReturnDCSQry()
    '        qry += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is null "
    '        If txtMultDCS.arrValueMember IsNot Nothing AndAlso txtMultDCS.arrValueMember.Count > 0 Then
    '            qry += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE IN (" & clsCommon.GetMulcallString(txtMultDCS.arrValueMember) & ")"
    '        End If
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            Dim ii As Integer = 0
    '            For Each dr As DataRow In dt.Rows
    '                clsCommon.ProgressBarPercentUpdate((ii + 1) * 100 / dt.Rows.Count, " Printing " & (ii + 1) & " Of " & dt.Rows.Count)
    '                ProcessFile(dr) ' Process each file individually
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
    '    Finally
    '        GC.Collect()
    '        GC.WaitForPendingFinalizers()
    '        GC.Collect()
    '    End Try
    'End Sub

    'Private Sub ProcessFile(dr As DataRow)
    '    Dim PDFPath As String = clsPaymentProcessHead.Load_Report_Paymnet_RCDF("'" + clsCommon.myCstr(dr("Doc_No")) + "'", clsCommon.myCDate(dr("From_Date")), clsCommon.myCDate(dr("To_Date")), "", "'" + clsCommon.myCstr(dr("VSP_CODE")) + "'", "", "", "", False, True, "", False)
    '    If PDFPath IsNot Nothing AndAlso clsCommon.myLen(PDFPath) > 0 Then
    '        Dim FileNo As Integer = clsAttachDocument.UploadWithHttpRequest(PDFPath, Path.GetFileName(PDFPath), clsUserMgtCode.frmMilkPurchaseInvoice, clsCommon.myCstr(dr("Milk_Purchase_Invoice_No")))
    '        If FileNo > 0 Then
    '            Dim qry As String = " UPDATE TSPL_MILK_PURCHASE_INVOICE_HEAD set FILE_INFO=" + clsCommon.myCstr(FileNo) + " where DOC_CODE='" + clsCommon.myCstr(dr("Milk_Purchase_Invoice_No")) + "'"
    '            clsDBFuncationality.ExecuteNonQuery(qry)
    '        End If
    '        SaveFile(PDFPath, clsCommon.myCstr(dr("VSP_CODE")), clsCommon.myCstr(dr("Doc_No")), clsCommon.myCDate(dr("Doc_Date")), clsCommon.myCstr(dr("VSP_CODE")), clsCommon.myCstr(dr("VSP_NAME")), clsCommon.myCstr(dr("VLC_CODE_Uploader")), clsCommon.myCDate(dr("From_Date")), clsCommon.myCDate(dr("To_Date")))
    '    Else
    '        clsCommon.MyMessageBoxShow(Me, "Empty File Path", Me.Text)
    '    End If
    'End Sub




    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    'Private Sub SaveFile(ByVal FilePath As String, ByVal Vendor_Code As String, ByVal Documnet_No As String, ByVal Document_Date As DateTime, ByVal VSP_Code As String, ByVal VSP_Name As String, ByVal VLC_Uploader_Code As String, ByVal FromDate As Date, ByVal ToDate As Date)
    '    Try
    '        If Vendor_Code IsNot Nothing AndAlso clsCommon.myLen(Vendor_Code) > 0 Then
    '            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmSendSMSToDCS + "'", Nothing)
    '            Dim Qry As String = "Select Email from TSPL_VENDOR_MASTER where Vendor_Code='" + Vendor_Code + "'"
    '            Dim arrMailID As List(Of String) = New List(Of String)()
    '            arrMailID.Add(clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry)))
    '            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 AndAlso (arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Then
    '                Dim objEmailH As New clsEMailHead()
    '                'objEmailH.arrEMail = New List(Of String)()
    '                objEmailH.arrEMail = arrMailID
    '                objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
    '                objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
    '                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_NO, Documnet_No)
    '                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(Document_Date, "dd/MMM/yyyy"))
    '                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.VLCCode, VSP_Code)
    '                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.VLCName, VSP_Name)
    '                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.VLCUploaderCode, VLC_Uploader_Code)
    '                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy"))
    '                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy"))
    '                objEmailH.Attachment_1_Path = FilePath
    '                objEmailH.SaveData(clsUserMgtCode.frmSendSMSToDCS, objEmailH, Nothing)
    '                objEmailH = Nothing
    '            End If
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub fndMCCCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndMCCCode._MYValidating
        Try
            Dim qry As String = ""
            Dim arrMCCRights As ArrayList = clsMCCCodes.GetUserHavingMCCRights()
            qry = "select * from ( select tspl_mcc_master.MCC_Code as [Code] ,tspl_mcc_master.MCC_Type as [Mcc Type] ,tspl_mcc_master.MCC_NAME as [Mcc Name] ,tspl_mcc_master.Chilling_Vendor as [Chilling Vendor] ,tspl_mcc_master.Add1 as [Address1] ,tspl_mcc_master.Add2 as [Address2] ,tspl_mcc_master.Tehsil as [Tehsil] ,tspl_mcc_master.City_code as [City Code] ,tspl_mcc_master.State_Code as [State Code] ,tspl_mcc_master.Country_code as [Country Code] ,tspl_mcc_master.Pin_code as [Pin Code],tspl_mcc_master.Pan_No as [Pan No] ,tspl_mcc_master.Telphone as [Telphone] ,tspl_mcc_master.Email as [Email] ,tspl_mcc_master.Fax as [Fax] ,tspl_mcc_master.MCC_Area as [Mcc Area] ,tspl_mcc_master.Area_Of_Store as [Area Of Store] ,tspl_mcc_master.Area_Of_Office as [Area Of Office] ,tspl_mcc_master.Open_Area_For_tanker as [Open Area For Tanker] ,tspl_mcc_master.Area_Of_LAB as [Area Of Lab] ,tspl_mcc_master.No_Of_SILO as [No Of Silo] ,tspl_mcc_master.Total_Storage_capacity as [Total Storage Capacity] ,tspl_mcc_master.Area_Of_Receiving_DOCK as [Area Of Receiving Dock] ,tspl_mcc_master.No_Of_Chiller as [No Of Chiller] ,tspl_mcc_master.Chiller_Brand_Name as [Chiller Brand Name] ,tspl_mcc_master.Chiller_Capacity as [Chiller Capacity] ,tspl_mcc_master.No_Of_MilkPump as [No Of Milkpump] ,tspl_mcc_master.MilkPump_Capacity as [Milkpump Capacity] ,tspl_mcc_master.DripSaver as [Drip Saver] ,tspl_mcc_master.CanWasher as [Can Washer] ,tspl_mcc_master.CanScrubber as [Can Scrubber] ,tspl_mcc_master.FSSAI_NO as [FSSAI No] ,tspl_mcc_master.ETP as [ETP] ,tspl_mcc_master.Earthing as [Earthing] ,tspl_mcc_master.Coil_Length as [Coil Length] ,tspl_mcc_master.Electricity_Connection as [Electricity Connection] ,tspl_mcc_master.Boiler as [Boiler] ,tspl_mcc_master.NoOfDG as [No. of DG] ,tspl_mcc_master.NoOfCompressor as [No. of Compressor] ,tspl_mcc_master.PayeeName as [Payee Name] ,tspl_mcc_master.BankName as [Bank Name] ,tspl_mcc_master.BankBranch as [Bank Branch] ,tspl_mcc_master.BankCityCode as [Bank City Code] ,tspl_mcc_master.BankStateCode as [Bank State Code] ,tspl_mcc_master.IFCICode as [IFCI Code] ,tspl_mcc_master.AccountNO as [Account No] ,tspl_mcc_master.Created_By as [Created By] ,tspl_mcc_master.Created_Date as [Created Date] ,tspl_mcc_master.Modified_By as [Modified By] ,tspl_mcc_master.Modified_Date as [Modified Date] ,tspl_mcc_master.Comp_Code as [Company Code],tspl_mcc_master.mcc_code_vlc_uploader as [MCC Code For VLC Uploder],tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER_PLANT.Location_Desc AS [Plant Name] from tspl_mcc_master LEFT JOIN TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_PLANT ON TSPL_LOCATION_MASTER_PLANT.Location_Code=tspl_mcc_master.Plant_Code  inner join tspl_location_master on tspl_location_master.location_Code= tspl_mcc_master.mcc_Code " _
            & " and (  tspl_mcc_master.mcc_Code in (" & clsCommon.GetMulcallString(arrMCCRights) & ")))xx "

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
            Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                clsMilkShiftEndMCC.CreateSMSContentVSP(fndMCCCode.Value, txtDate.Value, clsCommon.myCstr(cboShift.SelectedValue), tran)
                tran.Commit()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Task Completed", Me.Text)
            Catch ex As Exception
                tran.Rollback()
                clsCommon.ProgressBarHide()
                Throw New Exception(ex.Message)
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub txtMultDCS__My_Click(sender As Object, e As EventArgs) Handles txtMultDCS._My_Click
    '    Try
    '        If clsCommon.myLen(fndPaymentProcessDocNo.Value) > 0 Then
    '            Dim qry As String = "Select Doc_No As [Document Code], VSP_CODE As [DCS Code],	VSP_NAME As [DCS Name], VLC_CODE_Uploader As [DCS Uploader Code],Zone_Code As [Zone] from (" + ReturnDCSQry() + " and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is null )xxx"
    '            txtMultDCS.arrValueMember = clsCommon.ShowMultipleSelectForm(True, "DCS@", qry, "DCS Code", "", txtMultDCS.arrValueMember, Nothing)
    '        Else
    '            clsCommon.MyMessageBoxShow(Me, "Fill Document Code.", Me.Text)
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub

    'Public Sub CalculateSendOrRemainingBill()
    '    Try
    '        txtSendBill.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from (" & ReturnDCSQry() & " and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is Not null ) xxx"))
    '        txtRemainingBill.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from (" & ReturnDCSQry() & " and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is null ) xxx"))
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            If clsCommon.myLen(cboShift.SelectedValue) <= 0 Then
                cboShift.Focus()
                Throw New Exception("Please select " + cboShift.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
                fndMCCCode.Focus()
                Throw New Exception("Please select " + fndMCCCode.MyLinkLable1.Text)
            End If
            Try
                Dim qry As String = "select '1' as T_UN,'" + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") + "' as T_DT,'1' as ROUTE,'" + IIf(clsCommon.CompairString(clsCommon.myCstr(cboShift.SelectedValue), "M") = CompairStringResult.Equal, "0", "1") + "' as T_SHFT,'' T_TIME1,'' T_TIME2,VLC_Code_VLC_Uploader as DCS
,'1' as T_SWCAN,Qty as T_SWQTY,(FAT_PER*10) AS T_SWFAT,'0' AS T_SWCLR,(SNF_Per*10) AS T_SWSNF
,'0' AS T_SOCAN,SOUR_Qty AS T_SOQTY,(SOUR_FAT_PER*10) AS T_SOFAT,'0' AS T_SOCLR,(SOUR_SNF_PER*10) AS T_SOSNF
,'0' AS T_CUCAN,CURD_Qty as T_CUQTY
,'1' AS T_EMPTCAN,'0' AS T_FILL,'' AS SMPLNO,'' AS MODE,'' AS ONLINE,'' AS E_MODE,'' AS EMT_COR,'' AS WGH_COR,'' AS SUBDCS 
from (" + clsMilkShiftEndMCC.GetSMSQry(fndMCCCode.Value, txtDate.Value, clsCommon.myCstr(cboShift.SelectedValue)) + ")xxxxx"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("No data found")
                End If
                clsCommon.MyGenerateDBFFile(dt)
                'clsCommon.MyMessageBoxShow(Me, "Task Completed", Me.Text)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                Throw New Exception(ex.Message)
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultBMC__My_Click(sender As Object, e As EventArgs) Handles txtMultBMC._My_Click
        Try
            Dim Qry As String = clsMilkCollectionMCC.GetQuery(txtQCDate.Value, 3, True)
            txtMultBMC.arrValueMember = clsCommon.ShowMultipleSelectForm(True, "BMC@", Qry, "MCC_Code", "", txtMultBMC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SendSMSandEmail(ByVal SMSType As String, ByVal Code As String, ByVal strNo As String, ByVal Route As String, ByVal Qty As String, ByVal FAT_Per As String, ByVal SNF_Per As String, ByVal FAT_KG As String, ByVal SNF_KG As String, ByVal Acidity As String, ByVal Flushing As String, ByVal FAT As String, ByVal SNF As String, ByVal Vehicle As String, ByVal Sample_or_Trip As String, ByVal trans As SqlTransaction)
        Try
            Dim strPhoneno As String = Nothing
            Dim dtContent As DataTable = Nothing

            If clsCommon.CompairString(SMSType, "BMCQCSMS") = CompairStringResult.Equal Then
                dtContent = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.MilkCollectionMCCSample + "'", trans)
            ElseIf clsCommon.CompairString(SMSType, "TANKERQCSMS") = CompairStringResult.Equal Then
                dtContent = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmSendSMSToDCS + "3'", trans)
            ElseIf clsCommon.CompairString(SMSType, "TANKERPLSMS") = CompairStringResult.Equal Then
                dtContent = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmSendSMSToDCS + "2'", trans)
            ElseIf clsCommon.CompairString(SMSType, "TANKERQCSMSException") = CompairStringResult.Equal Then
                dtContent = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmSendSMSToDCS + "1'", trans)
            End If

            If dtContent Is Nothing AndAlso dtContent.Rows.Count <= 0 Then
                Throw New Exception("SMS format content not found!")
            End If

            Dim DCSCode As String
            Dim Qry As String
            If clsCommon.CompairString(SMSType, "BMCQCSMS") = CompairStringResult.Equal Then
                Qry = "Select (Case When IsNull(TSPL_VENDOR_MASTER.Phone1,'')='' Then TSPL_VENDOR_MASTER.Phone2 Else TSPL_VENDOR_MASTER.Phone1 End) As DCS_Contact, 
                                TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader from TSPL_VENDOR_MASTER
                                Left Outer join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code
                                where TSPL_VENDOR_MASTER.Form_Type='VSP' And (Case When IsNull(TSPL_VENDOR_MASTER.Phone1,'')='' Then TSPL_VENDOR_MASTER.Phone2 Else TSPL_VENDOR_MASTER.Phone1 End) Not In ('Null','','(+__)__________') And TSPL_VLC_MASTER_HEAD.MCCOwnBMC='" + strNo + "'"
            Else 'If clsCommon.CompairString(SMSType, "TANKERPLSMS") = CompairStringResult.Equal Then
                Qry = "Select (Case When IsNull(TSPL_VENDOR_MASTER.Phone1,'')='' Then TSPL_VENDOR_MASTER.Phone2 Else TSPL_VENDOR_MASTER.Phone1 End) As DCS_Contact, 
TSPL_VENDOR_MASTER.Vendor_Name,TSPL_TANKER_MASTER.Tanker_No
from TSPL_VENDOR_MASTER
Left Outer join TSPL_TANKER_MASTER On TSPL_TANKER_MASTER.Tanker_Transporter_Code=TSPL_VENDOR_MASTER.Vendor_Code
where TSPL_VENDOR_MASTER.Form_Type='TTM' And (Case When IsNull(TSPL_VENDOR_MASTER.Phone1,'')='' Then TSPL_VENDOR_MASTER.Phone2 Else TSPL_VENDOR_MASTER.Phone1 End) Not In ('Null','','(+__)__________') And TSPL_TANKER_MASTER.Tanker_No='" + strNo + "'"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    strPhoneno = clsCommon.myCstr(dt.Rows(i)("DCS_Contact"))
                    If clsCommon.CompairString(SMSType, "BMCQCSMS") = CompairStringResult.Equal Then
                        DCSCode = clsCommon.myCstr(dt.Rows(i)("VLC_Code_VLC_Uploader"))
                    End If

                    Dim objEmailH As New clsEMailHead()
                    objEmailH.arrEMail = New List(Of String)()
                    Dim objSMSH As New clsSMSHead()
                    objSMSH.arrMobilNo = New List(Of String)()
                    If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then
                        If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                            objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))
                            If clsCommon.CompairString(SMSType, "BMCQCSMS") = CompairStringResult.Equal Then
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtQCDate.Value, "dd/MMM/yyyy"))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCName, clsCommon.myCstr(dt.Rows(0)("Vendor_Name")))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCUploaderCode, clsCommon.myCstr(DCSCode))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Route, clsCommon.myCstr(Route))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.vehicleNo, clsCommon.myCstr(Vehicle))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.SampleNo, clsCommon.myCstr(Sample_or_Trip))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderFat, FAT)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderSNF, SNF)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderCLR, clsCommon.myCstr(clsEkoPro.getClrOnCalculation(FAT, SNF, corrFactor)))
                            ElseIf clsCommon.CompairString(SMSType, "TANKERQCSMS") = CompairStringResult.Equal Then
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_No, Code)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtTankerQCDate.Value, "dd/MMM/yyyy"))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.TankerNo, strNo)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.vehicleNo, Vehicle)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Route, Route)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.TripNo, Sample_or_Trip)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Acidity, Acidity)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.EnteredQty, Qty)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.FAT_Per, FAT_Per)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.SNF_Per, SNF_Per)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.KgFAT, FAT_KG)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.KgSNF, SNF_KG)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Flushing, Flushing)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.FATPL, FAT)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.SNFPL, SNF)
                            ElseIf clsCommon.CompairString(SMSType, "TANKERPLSMS") = CompairStringResult.Equal Then
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_No, Code)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtTankerQCDate.Value, "dd/MMM/yyyy"))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.TankerNo, strNo)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.vehicleNo, Vehicle)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Route, Route)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.TripNo, Sample_or_Trip)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.EnteredQty, Qty)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Acidity, Acidity)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.FAT_Per, FAT_Per)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.SNF_Per, SNF_Per)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.KgFAT, FAT_KG)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.KgSNF, SNF_KG)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Flushing, Flushing)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.FATPL, FAT)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.SNFPL, SNF)
                            ElseIf clsCommon.CompairString(SMSType, "TANKERQCSMSException") = CompairStringResult.Equal Then
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_No, Code)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtTankerQCDate.Value, "dd/MMM/yyyy"))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.TankerNo, strNo)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.vehicleNo, Vehicle)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Route, Route)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.TripNo, Sample_or_Trip)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.EnteredQty, Qty)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Acidity, Acidity)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.FAT_Per, FAT_Per)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.SNF_Per, SNF_Per)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.KgFAT, FAT_KG)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.KgSNF, SNF_KG)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Flushing, Flushing)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.FATPL, FAT)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.SNFPL, SNF)
                            End If
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Form_Code, MyBase.Form_ID)

                            If clsCommon.myLen(strPhoneno) > 0 Then
                                strPhoneno = strPhoneno.Replace("(", "").Replace(")", "").Replace("+", "").Replace("_____", "").Replace("____", "").Replace("___", "").Replace("__", "").Replace("_", "")
                            End If

                            If clsCommon.myLen(strPhoneno) >= 10 Then
                                objSMSH.arrMobilNo.Add(clsCommon.myCstr(strPhoneno))
                            End If

                            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                                If clsCommon.CompairString(SMSType, "BMCQCSMS") = CompairStringResult.Equal Then
                                    objSMSH.SaveData(clsUserMgtCode.MilkCollectionMCCSample, objSMSH, trans)
                                ElseIf clsCommon.CompairString(SMSType, "TANKERQCSMS") = CompairStringResult.Equal Then
                                    objSMSH.SaveData(clsUserMgtCode.frmSendSMSToDCS + "3", objSMSH, trans)
                                ElseIf clsCommon.CompairString(SMSType, "TANKERPLSMS") = CompairStringResult.Equal Then
                                    objSMSH.SaveData(clsUserMgtCode.frmSendSMSToDCS + "2", objSMSH, trans)
                                ElseIf clsCommon.CompairString(SMSType, "TANKERQCSMSException") = CompairStringResult.Equal Then
                                    objSMSH.SaveData(clsUserMgtCode.frmSendSMSToDCS + "1", objSMSH, trans)
                                End If
                            End If
                        End If
                    End If
                    objSMSH = Nothing
                Next
            End If

            'If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
            '    clsCommon.MyMessageBoxShow(Me,"SMS Send Successfully", Me.Text)
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnSendBMC_Click(sender As Object, e As EventArgs) Handles btnSendBMC.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            corrFactor = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, trans)
            Dim Qry As String = clsMilkCollectionMCC.GetQuery(txtQCDate.Value, 3, True)
            If txtMultBMC.arrValueMember IsNot Nothing AndAlso txtMultBMC.arrValueMember.Count > 0 Then
                Qry += "  And TSPL_MCC_MASTER.MCC_Code In (" & clsCommon.GetMulcallString(txtMultBMC.arrValueMember) & ") "
            End If
            Dim dtt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
                clsCommon.ProgressBarPercentShow()
                For i As Integer = 0 To dtt.Rows.Count - 1
                    clsCommon.ProgressBarPercentUpdate((i + 1) * 100 / dtt.Rows.Count, " Sending " & (i + 1) & " Of " & dtt.Rows.Count)
                    SendSMSandEmail("BMCQCSMS", Nothing, clsCommon.myCstr(dtt.Rows(i)("MCC_Code")), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, clsCommon.myCstr(dtt.Rows(i)("Route_Code")), clsCommon.myCstr(dtt.Rows(i)("FAT")), clsCommon.myCstr(dtt.Rows(i)("SNF")), clsCommon.myCstr(dtt.Rows(i)("Vehicle_No")), clsCommon.myCstr(dtt.Rows(i)("Sample_No")), trans)
                Next
                clsCommon.ProgressBarPercentHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "SMS Send Successfully", Me.Text)
                txtMultBMC.arrValueMember = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found to send !", Me.Text)
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Function MultipleTanker(ByVal Status As Integer, ByVal strdate As DateTime) As String
        Dim Qry As String = "Select ID,Document_No,(Case When IsNull(TSPL_VENDOR_MASTER.Phone1,'')='' Then TSPL_VENDOR_MASTER.Phone2 Else TSPL_VENDOR_MASTER.Phone1 End) As Contact, 
TSPL_VENDOR_MASTER.Vendor_Name,tspl_Milk_collection_MCC.Route_Code,TSPL_TANKER_MASTER.Tanker_No,Vehicle_No,Trip_No,Entered_Qty,[FAT%],[SNF%],Flushing,[FAT P/L],[SNF P/L],TSPL_MILK_COLLECTION_MCC.Status
from TSPL_VENDOR_MASTER
Left Outer join TSPL_TANKER_MASTER On TSPL_TANKER_MASTER.Tanker_Transporter_Code=TSPL_VENDOR_MASTER.Vendor_Code
Inner Join (Select TSPL_MILK_COLLECTION_MCC.Document_No,max(TSPL_MILK_COLLECTION_MCC.Status)Status,Max(tspl_Milk_collection_MCC_Detail.PK_Id)ID,TSPL_MILK_COLLECTION_MCC.Route_Code,TSPL_MILK_COLLECTION_MCC.Tanker_No,TSPL_MILK_COLLECTION_MCC.Vehicle_No,tspl_Milk_collection_MCC.Trip_No,MAX(Convert(int,Entered_Qty))Entered_Qty,Max(Convert(decimal(18,2),(Entered_FATKg*100)/Entered_Qty)) [FAT%],Max(Convert(decimal(18,2),(Entered_SNFKg*100)/Entered_Qty)) [SNF%],(Max(Entered_Qty)-Sum(tspl_Milk_collection_MCC_Detail.Qty))Flushing,(Max(Entered_FATKg)-Sum(FATKG))[FAT P/L],(Max(Entered_SNFKg)-Sum(SNFKG))[SNF P/L] from TSPL_MILK_COLLECTION_MCC
left Outer Join tspl_Milk_collection_MCC_Detail on tspl_Milk_collection_MCC_Detail.Document_No=TSPL_MILK_COLLECTION_MCC.Document_No Where Convert(Date, tspl_Milk_collection_MCC.Document_Date,103) ='" + clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy") + "' Group By Route_Code,Tanker_No,Vehicle_No,Trip_No,TSPL_MILK_COLLECTION_MCC.Document_No )tspl_Milk_collection_MCC On tspl_Milk_collection_MCC.Tanker_No=TSPL_TANKER_MASTER.Tanker_No
where TSPL_VENDOR_MASTER.Form_Type='TTM' "
        If Status = 1 Then
            Qry += " and TSPL_MILK_COLLECTION_MCC.Status=1 "
        ElseIf Status = 0 Then
            Qry += " and TSPL_MILK_COLLECTION_MCC.Status=0 "
        End If
        Qry += " And (Case When IsNull(TSPL_VENDOR_MASTER.Phone1,'')='' Then TSPL_VENDOR_MASTER.Phone2 Else TSPL_VENDOR_MASTER.Phone1 End) Not In ('Null','','(+__)__________')"
        Return Qry
    End Function

    Private Sub txtMultTanker__My_Click(sender As Object, e As EventArgs) Handles txtMultTanker._My_Click
        Try
            Dim Qry As String = MultipleTanker(1, txtTankerQCDate.Value)
            txtMultTanker.arrValueMember = clsCommon.ShowMultipleSelectForm(True, "Tanker@", Qry, "ID", "", txtMultTanker.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnTankerSMS_Click(sender As Object, e As EventArgs) Handles btnTankerSMS.Click
        TankerPLorQCSMS("TANKERPLSMS", txtTankerQCDate.Value)
    End Sub

    Sub TankerPLorQCSMS(ByVal SMSType As String, ByVal strDate As DateTime)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "Select Document_No,Convert(varchar(10),Document_Date,103)Document_Date,Tanker_No,Vehicle_No,Route_Code,Entered_Qty,[FAT%],[SNF%],Entered_FATKg,Entered_SNFKg,Acidity,(Entered_Qty-Qty)Flushing,(Entered_FATKg-FATKG)FATPL,(Entered_SNFKg-SNFKG)SNFPL,Trip_No  from(
Select Max(TSPL_MILK_COLLECTION_MCC_DETAIL.PK_ID)PK_ID,TSPL_MILK_COLLECTION_MCC.Document_No,MAX(TSPL_MILK_COLLECTION_MCC.Document_Date)Document_Date,MAX(TSPL_MILK_COLLECTION_MCC.Tanker_No)Tanker_No,Max(TSPL_MILK_COLLECTION_MCC.Vehicle_No)Vehicle_No,Max(tspl_Milk_collection_MCC.Trip_No)Trip_No,MAX(TSPL_MILK_COLLECTION_MCC.Route_Code)Route_Code,Max(TSPL_MILK_COLLECTION_MCC.Entered_Qty)Entered_Qty,Max(Convert(decimal(18,2),(Entered_FATKg*100)/Entered_Qty)) [FAT%],Max(Convert(decimal(18,2),(Entered_SNFKg*100)/Entered_Qty)) [SNF%],Max(TSPL_MILK_COLLECTION_MCC.Entered_FATKg)Entered_FATKg,Max(TSPL_MILK_COLLECTION_MCC.Entered_SNFKg)Entered_SNFKg,Sum(TSPL_MILK_COLLECTION_MCC_DETAIL.Qty)Qty,Sum(TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG)FATKG,Sum(TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG)SNFKG,MAX(TSPL_MILK_COLLECTION_MCC.Acidity)Acidity from TSPL_MILK_COLLECTION_MCC_DETAIL
Left Outer Join TSPL_MILK_COLLECTION_MCC On TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
where Convert(Date, tspl_Milk_collection_MCC.Document_Date,103) ='" + clsCommon.GetPrintDate(strDate, "dd/MMM/yyyy") + "' "
            Qry += " Group By TSPL_MILK_COLLECTION_MCC.Document_No) xyz "

            If clsCommon.CompairString(SMSType, "TANKERPLSMS") = CompairStringResult.Equal Then
                If txtMultTanker.arrValueMember IsNot Nothing AndAlso txtMultTanker.arrValueMember.Count > 0 Then
                    Qry += "  Where  PK_ID IN (" & clsCommon.GetMulcallString(txtMultTanker.arrValueMember) & ") "
                End If
            End If

            If clsCommon.CompairString(SMSType, "TANKERQCSMS") = CompairStringResult.Equal Then
                If txtMultQCTanker.arrValueMember IsNot Nothing AndAlso txtMultQCTanker.arrValueMember.Count > 0 Then
                    Qry += "  Where  PK_ID IN (" & clsCommon.GetMulcallString(txtMultQCTanker.arrValueMember) & ") "
                End If
            End If

            If clsCommon.CompairString(SMSType, "TANKERQCSMSException") = CompairStringResult.Equal Then
                If txtMultTankerEx.arrValueMember IsNot Nothing AndAlso txtMultTankerEx.arrValueMember.Count > 0 Then
                    Qry += "  Where  PK_ID IN (" & clsCommon.GetMulcallString(txtMultTankerEx.arrValueMember) & ") "
                End If
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsCommon.ProgressBarPercentShow()
                For i As Integer = 0 To dt.Rows.Count - 1
                    clsCommon.ProgressBarPercentUpdate((i + 1) * 100 / dt.Rows.Count, " Sending " & (i + 1) & " Of " & dt.Rows.Count)
                    SendSMSandEmail(SMSType, clsCommon.myCstr(dt.Rows(i)("Document_No")), clsCommon.myCstr(dt.Rows(i)("Tanker_No")), clsCommon.myCstr(dt.Rows(i)("Route_Code")), clsCommon.myCstr(dt.Rows(i)("Entered_Qty")), clsCommon.myCstr(dt.Rows(i)("FAT%")), clsCommon.myCstr(dt.Rows(i)("SNF%")), clsCommon.myCstr(dt.Rows(i)("Entered_FATKg")), clsCommon.myCstr(dt.Rows(i)("Entered_SNFKg")), clsCommon.myCstr(dt.Rows(i)("Acidity")), clsCommon.myCstr(dt.Rows(i)("Flushing")), clsCommon.myCstr(dt.Rows(i)("FATPL")), clsCommon.myCstr(dt.Rows(i)("SNFPL")), clsCommon.myCstr(dt.Rows(i)("Vehicle_No")), clsCommon.myCstr(dt.Rows(i)("Trip_No")), trans)
                Next
                clsCommon.ProgressBarPercentHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "SMS Send Successfully", Me.Text)
                txtMultTanker.arrValueMember = Nothing
            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultRoute__My_Click(sender As Object, e As EventArgs) Handles txtMultRoute._My_Click
        Try
            Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No As [Route Code],TSPL_ROUTE_MASTER.Route_Desc AS Description From TSPL_ROUTE_MASTER
                                 left Outer Join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE On TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code=TSPL_ROUTE_MASTER.Route_No
                                 where Convert(Date, TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date,103) ='" + clsCommon.GetPrintDate(txtCrateEntryDate.Value, "dd/MMM/yyyy") + "' And TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.ShiftType='" + clsCommon.myCstr(cboShiftCrateEntry.SelectedValue) + "'"
            txtMultRoute.arrValueMember = clsCommon.ShowMultipleSelectForm(True, "Route@", qry, "Route Code", "", txtMultRoute.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSendSMSCrateEntry_Click(sender As Object, e As EventArgs) Handles btnSendSMSCrateEntry.Click
        Try
            SendSMSandEmail()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SendSMSandEmail()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strPhoneno As String = Nothing
            Dim dtContent As DataTable = Nothing
            dtContent = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmCrateReceviedDairySale + "'", trans)
            If dtContent Is Nothing AndAlso dtContent.Rows.Count <= 0 Then
                Throw New Exception("SMS format content not found!")
            End If

            Dim DCSCode As String
            Dim Qry As String = "select Convert(Varchar(10),TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date,103)Document_Date,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.ShiftType,
TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code,TSPL_ROUTE_MASTER.Route_Desc,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,
TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyRecd,
Case When IsNull(TSPL_CUSTOMER_MASTER.Phone1,'')<>'' Then Phone1 Else Phone2 End As DCS_Contact
from TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE
Left Outer Join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE On TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No=TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No
Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code
Left Outer Join TSPL_ROUTE_MASTER On TSPL_ROUTE_MASTER.Route_No=TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code
where Convert(Date, TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date,103) ='" + clsCommon.GetPrintDate(txtCrateEntryDate.Value, "dd/MMM/yyyy") + "' And TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.ShiftType='" + clsCommon.myCstr(cboShiftCrateEntry.SelectedValue) + "' "
            If txtMultRoute.arrValueMember IsNot Nothing AndAlso txtMultRoute.arrValueMember.Count > 0 Then
                Qry += " And TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code In (" + clsCommon.GetMulcallString(txtMultRoute.arrValueMember) + ") "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsCommon.ProgressBarPercentShow()
                For i As Integer = 0 To dt.Rows.Count - 1
                    strPhoneno = clsCommon.myCstr(dt.Rows(i)("DCS_Contact"))
                    'If clsCommon.CompairString(SMSType, "BMCQCSMS") = CompairStringResult.Equal Then
                    '    DCSCode = clsCommon.myCstr(dt.Rows(i)("VLC_Code_VLC_Uploader"))
                    'End If

                    Dim objEmailH As New clsEMailHead()
                    objEmailH.arrEMail = New List(Of String)()
                    Dim objSMSH As New clsSMSHead()
                    objSMSH.arrMobilNo = New List(Of String)()
                    If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then
                        clsCommon.ProgressBarPercentUpdate((i + 1) * 100 / dt.Rows.Count, " Sending " & (i + 1) & " Of " & dt.Rows.Count)
                        If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                            objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtCrateEntryDate.Value, "dd/MMM/yyyy"))
                            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i)("ShiftType")), "E") = CompairStringResult.Equal Then
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Shift, "Evening")
                            Else
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Shift, "Morning")
                            End If
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Route, clsCommon.myCstr(dt.Rows(i)("Route_code")))
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.RouteName, clsCommon.myCstr(dt.Rows(i)("Route_Desc")))
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Cust_Code, clsCommon.myCstr(dt.Rows(i)("Customer_Code")))
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Cust_Name, clsCommon.myCstr(dt.Rows(i)("Customer_Name")))
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.CRT, clsCommon.myCstr(dt.Rows(i)("CrateQtyRecd")))
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Form_Code, MyBase.Form_ID)

                            If clsCommon.myLen(strPhoneno) > 0 Then
                                strPhoneno = strPhoneno.Replace("(", "").Replace(")", "").Replace("+91", "").Replace("_____", "").Replace("____", "").Replace("___", "").Replace("__", "").Replace("_", "")
                            End If

                            If clsCommon.myLen(strPhoneno) >= 10 Then
                                objSMSH.arrMobilNo.Add(clsCommon.myCstr(strPhoneno))
                            End If

                            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                                objSMSH.SaveData(clsUserMgtCode.frmCrateReceviedDairySale, objSMSH, trans)
                            End If
                        End If
                    End If
                    objSMSH = Nothing
                Next
                clsCommon.ProgressBarPercentHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "SMS Send Successfully", Me.Text)
                txtMultRoute.arrValueMember = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found to send.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultTankerException__My_Click(sender As Object, e As EventArgs) Handles txtMultTankerEx._My_Click
        Try
            Dim Qry As String = MultipleTanker(0, txtTankerQCDateException.Value)
            txtMultTankerEx.arrValueMember = clsCommon.ShowMultipleSelectForm(True, "Tanker@", Qry, "ID", "", txtMultTankerEx.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnTankerSMSException_Click(sender As Object, e As EventArgs) Handles BtnTankerSMSException.Click
        TankerPLorQCSMS("TANKERQCSMSException", txtTankerQCDateException.Value)
    End Sub

    Private Sub SendSMSandEmailException(ByVal SMSType As String, ByVal Code As String, ByVal strNo As String, ByVal Route As String, ByVal Flushing As String, ByVal FAT As String, ByVal SNF As String, ByVal Vehicle As String, ByVal Sample_or_Trip As String, ByVal trans As SqlTransaction)
        Try
            Dim strPhoneno As String = Nothing
            Dim dtContent As DataTable = Nothing


            If clsCommon.CompairString(SMSType, "TANKERQCSMSException") = CompairStringResult.Equal Then
                dtContent = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmSendSMSToDCS + "1" + "'", trans)
            End If

            If dtContent Is Nothing AndAlso dtContent.Rows.Count <= 0 Then
                Throw New Exception("SMS format content not found!")
            End If

            Dim DCSCode As String
            Dim Qry As String
            If clsCommon.CompairString(SMSType, "TANKERQCSMSException") = CompairStringResult.Equal Then
                Qry = "Select (Case When IsNull(TSPL_VENDOR_MASTER.Phone1,'')='' Then TSPL_VENDOR_MASTER.Phone2 Else TSPL_VENDOR_MASTER.Phone1 End) As DCS_Contact, 
TSPL_VENDOR_MASTER.Vendor_Name,TSPL_TANKER_MASTER.Tanker_No
from TSPL_VENDOR_MASTER
Left Outer join TSPL_TANKER_MASTER On TSPL_TANKER_MASTER.Tanker_Transporter_Code=TSPL_VENDOR_MASTER.Vendor_Code
where TSPL_VENDOR_MASTER.Form_Type='TTM' And (Case When IsNull(TSPL_VENDOR_MASTER.Phone1,'')='' Then TSPL_VENDOR_MASTER.Phone2 Else TSPL_VENDOR_MASTER.Phone1 End) Not In ('Null','','(+__)__________') And TSPL_TANKER_MASTER.Tanker_No='" + Code + "'"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    strPhoneno = clsCommon.myCstr(dt.Rows(i)("DCS_Contact"))
                    If clsCommon.CompairString(SMSType, "BMCQCSMS") = CompairStringResult.Equal Then
                        DCSCode = clsCommon.myCstr(dt.Rows(i)("VLC_Code_VLC_Uploader"))
                    End If

                    Dim objEmailH As New clsEMailHead()
                    objEmailH.arrEMail = New List(Of String)()
                    Dim objSMSH As New clsSMSHead()
                    objSMSH.arrMobilNo = New List(Of String)()
                    If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then
                        If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                            objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))
                            If clsCommon.CompairString(SMSType, "TANKERQCSMSException") = CompairStringResult.Equal Then
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtTankerQCDate.Value, "dd/MMM/yyyy"))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.TankerNo, strNo)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.vehicleNo, Vehicle)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Route, Route)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.TripNo, Sample_or_Trip)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Flushing, Flushing)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.KgFAT, FAT)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.KgSNF, SNF)
                            End If
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Form_Code, MyBase.Form_ID + "1")

                            If clsCommon.myLen(strPhoneno) > 0 Then
                                strPhoneno = strPhoneno.Replace("(", "").Replace(")", "").Replace("+", "").Replace("_____", "").Replace("____", "").Replace("___", "").Replace("__", "").Replace("_", "")
                            End If

                            If clsCommon.myLen(strPhoneno) >= 10 Then
                                objSMSH.arrMobilNo.Add(clsCommon.myCstr(strPhoneno))
                            End If

                            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then

                                If clsCommon.CompairString(SMSType, "TANKERQCSMSException") = CompairStringResult.Equal Then
                                    objSMSH.SaveData(clsUserMgtCode.frmSendSMSToDCS + "1", objSMSH, trans)
                                End If

                            End If
                        End If
                    End If
                    objSMSH = Nothing
                Next
            End If

            'If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
            '    clsCommon.MyMessageBoxShow(Me,"SMS Send Successfully", Me.Text)
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtMultQCTanker__My_Click(sender As Object, e As EventArgs) Handles txtMultQCTanker._My_Click
        Try
            Dim Qry As String = MultipleTanker(2, txtDateQC.Value)
            txtMultQCTanker.arrValueMember = clsCommon.ShowMultipleSelectForm(True, "Tanker@", Qry, "ID", "", txtMultQCTanker.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnTankerQC_Click(sender As Object, e As EventArgs) Handles btnTankerQC.Click
        Try
            TankerPLorQCSMS("TANKERQCSMS", txtDateQC.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrintPdf_Click(sender As Object, e As EventArgs) Handles btnPrintPdf.Click
        Try
            If clsCommon.myLen(TxtDCS.Value) <= 0 Then
                TxtDCS.Focus()
                Throw New Exception("Please select " + TxtDCS.MyLinkLable1.Text)
                Exit Sub
            End If
            Dim qry As String = Nothing
            qry = "Select FILE_INFO from TSPL_MILK_PURCHASE_INVOICE_HEAD where 2=2 and  convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.VENDOR_INVOICE_DATE ,103)>=convert(date,'" + clsCommon.GetPrintDate(fromDate.Value) + "',103) 
                   and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.VENDOR_INVOICE_DATE ,103)<=convert(date,'" + clsCommon.GetPrintDate(ToDate.Value) + "',103)"
            If clsCommon.myLen(TxtDCS.Value) > 0 Then
                qry += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE = '" & TxtDCS.Value & "'"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim pdfPaths As New List(Of String)
            Dim fileInfoList As New List(Of String)

            For Each row As DataRow In dt.Rows
                Dim Path As String = row("FILE_INFO").ToString()
                If Not Path.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) Then
                    Path &= ".pdf"
                End If
                ' Use filePath here
                fileInfoList.Add(Path)
            Next

            Dim basePath As String = "C:\XpertServices\XpertFileUpload\Upload"
            For Each fileBase In fileInfoList
                Dim allFiles = Directory.GetFiles(basePath, "*.pdf", SearchOption.TopDirectoryOnly)
                For Each filePath In allFiles
                    Dim fileNameWithoutExtension As String = Path.GetFileNameWithoutExtension(filePath)

                    If Not pdfPaths.Contains(filePath) Then
                        pdfPaths.Add(filePath)
                    End If

                Next
            Next

            If pdfPaths.Count = 0 Then
                MessageBox.Show("No matching PDF files found.")
                Return
            End If

            Dim outputFolder As String = Path.Combine("C:\ERPTempFolder", "Merged_" & DateTime.Now.ToString("yyyyMMdd_HHmmss"))
            Directory.CreateDirectory(outputFolder)
            Dim outputPdfPath As String = Path.Combine(outputFolder, "merged.pdf")
            Dim outputFolders As String = Path.Combine("C:\ERPTempFolder", "Files_" & DateTime.Now.ToString("yyyyMMdd_HHmmss"))
            Directory.CreateDirectory(outputFolder)

            Dim SQry As String = " Select Count(*)FILE_INFO from TSPL_MILK_PURCHASE_INVOICE_HEAD where 2=2 and  convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.VENDOR_INVOICE_DATE ,103)>=convert(date,'" + clsCommon.GetPrintDate(fromDate.Value) + "',103) 
                   and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.VENDOR_INVOICE_DATE ,103)<=convert(date,'" + clsCommon.GetPrintDate(ToDate.Value) + "',103)  and FILE_INFO is  null"
            If clsCommon.myLen(TxtDCS.Value) > 0 Then
                SQry += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE = '" & TxtDCS.Value & "'"
            End If
            Dim NullCount As Decimal = clsDBFuncationality.getSingleValue(SQry)

            Dim SQuery As String = " Select Count(*)FILE_INFO from TSPL_MILK_PURCHASE_INVOICE_HEAD where 2=2 and  convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.VENDOR_INVOICE_DATE ,103)>=convert(date,'" + clsCommon.GetPrintDate(fromDate.Value) + "',103) 
                   and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.VENDOR_INVOICE_DATE ,103)<=convert(date,'" + clsCommon.GetPrintDate(ToDate.Value) + "',103)  and FILE_INFO is not null"
            If clsCommon.myLen(TxtDCS.Value) > 0 Then
                SQuery += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE = '" & TxtDCS.Value & "'"
            End If
            Dim NotNullCount As Decimal = clsDBFuncationality.getSingleValue(SQuery)

            If (common.clsCommon.MyMessageBoxShow(" Invoice Created:" & NotNullCount & "   Invoice Not Created: " & NullCount & "   Do you want to MergePDF", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                MergePdfFiles(pdfPaths, outputPdfPath)
                clsCommon.MyMessageBoxShow(Me, "Merged PDF saved at:  " & outputPdfPath, Me.Text)
            Else
                SaveFilesIndividually(pdfPaths, outputFolders)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub MergePdfFiles(pdfFiles As List(Of String), outputFilePath As String)
        Using stream As New FileStream(outputFilePath, FileMode.Create)
            Dim document As New Document()
            Dim pdfCopy As New PdfCopy(document, stream)
            document.Open()

            For Each file In pdfFiles
                Using reader As New PdfReader(file)
                    For pageNum As Integer = 1 To reader.NumberOfPages
                        pdfCopy.AddPage(pdfCopy.GetImportedPage(reader, pageNum))
                    Next
                    pdfCopy.FreeReader(reader)
                End Using
            Next

            document.Close()
        End Using
    End Sub
    Private Sub SaveFilesIndividually(pdfPaths As List(Of String), destinationBasePath As String)
        Dim outputFolder As String = Path.Combine(destinationBasePath, "Invoice")
        Directory.CreateDirectory(outputFolder)

        ' Copy each file
        For Each filePath In pdfPaths
            Dim fileName As String = Path.GetFileName(filePath)
            Dim destinationPath As String = Path.Combine(outputFolder, fileName)

            File.Copy(filePath, destinationPath, overwrite:=True)
        Next

        MessageBox.Show("All individual files copied to: " & outputFolder, "Done", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub TxtDCS__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtDCS._MYValidating
        Try
            Dim qry As String = " select Vendor_Code  as Code, Vendor_Name as Name,Zone_Code as Zone,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Uploader Code] from TSPL_VENDOR_MASTER  
                                  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_VENDOR_MASTER.Vendor_Code where Form_Type='VSP'"
            TxtDCS.Value = clsCommon.ShowSelectForm("sensms@M", qry, "Code", "", TxtDCS.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class


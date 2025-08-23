Imports System.Data.SqlClient
Imports common
Imports System.IO
Public Class frmSendBilltoDistributor

#Region "Variables"
    Dim arrFilePath As List(Of String) = Nothing
    Dim settFileUpload As Boolean = False
    Dim corrFactor As Double
    Dim strQry As String = Nothing
#End Region

    Private Sub frmSendBilltoDistributor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtDate1.Value = clsCommon.GETSERVERDATE()
            txtDate2.Value = txtDate1.Value
            txtDate3.Value = txtDate1.Value
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    '==================Start Demand=======================
    Private Sub txtMultDoc__My_Click(sender As Object, e As EventArgs) Handles txtMultDoc._My_Click
        Try
            strQry = Nothing
            strQry = "select TSPL_DEMAND_BOOKING_MASTER.Document_No as [Document No],convert(varchar(12),TSPL_DEMAND_BOOKING_MASTER.Document_date,103) as Document_Date,TSPL_DEMAND_BOOKING_MASTER.ShiftType As [Shift Type],TSPL_DEMAND_BOOKING_MASTER.Route_No as [Route No],TSPL_DEMAND_BOOKING_MASTER.Location_Code as [Location Code],TSPL_DEMAND_BOOKING_MASTER.City_Code as [City Code],TripNo AS [Trip No],TSPL_DEMAND_BOOKING_MASTER.IsIndividualCustomer as [Individual Cust] from TSPL_DEMAND_BOOKING_MASTER where Posted=1 "
            strQry += " And File_Info Is Null"
            If clsCommon.myCDate(txtDate1.Value) <= clsCommon.myCDate(clsCommon.GETSERVERDATE()) Then
                strQry += " And Convert(Date,Document_Date,103)=Convert(Date,'" & txtDate1.Value & "',103)"
            Else
                clsCommon.MyMessageBoxShow(Me, "Date can't be greater then current day date !", Me.Text)
#Disable Warning
                Exit Sub
#Enable Warning
            End If
            txtMultDoc.arrValueMember = clsCommon.ShowMultipleSelectForm(True, "Doc@", strQry, "Document No", "", txtMultDoc.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSendDemandBill_Click(sender As Object, e As EventArgs) Handles btnSendDemandBill.Click
        clsCommon.ProgressBarPercentShow()
        Try
            strQry = Nothing
            strQry = "select Document_No As [Document Code],CONVERT(VARCHAR(10), Document_Date, 103) As [Document Date],ShiftType As [Shift],Route_No As [Route No], ItemType As [Item Type],IsIndividualCustomer As [Is Individual Customer] from TSPL_DEMAND_BOOKING_MASTER Where Posted=1 "
            If txtMultDoc.arrValueMember IsNot Nothing AndAlso txtMultDoc.arrValueMember.Count > 0 Then
                strQry += " and Document_No In (" & clsCommon.GetMulcallString(txtMultDoc.arrValueMember) & ")"
            End If
            strQry += " And File_Info Is Null "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim ii As Integer = 0
                For Each dr As DataRow In dt.Rows
                    clsCommon.ProgressBarPercentUpdate((ii + 1) * 100 / dt.Rows.Count, " Sending " & (ii + 1) & " Of " & dt.Rows.Count)
                    ProcessFileDemamd(dr) ' Process each file individually
                    ii = ii + 1
                Next
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, "Bill Send Successfully Done.", Me.Text)
                txtMultDoc.arrValueMember = Nothing
            Else
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
#Disable Warning
                Exit Sub
#Enable Warning
            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ProcessFileDemamd(dr As DataRow)
        Dim arrRoute As New ArrayList
        arrRoute.Add(clsCommon.myCstr(dr("Route No")))
        Dim PDFPath As String = clsDemandBookingSale.PrintDOSData(arrRoute, clsCommon.myCstr(dr("Shift")), dr("Document Date"), IIf(clsCommon.myCstr(dr("Item Type")) = "Fresh", True, False), IIf(clsCommon.myCstr(dr("Item Type")) = "Ambient", True, False), IIf(clsCommon.myCDecimal(dr("Is Individual Customer")) > 0, True, False), 107, 48, DosPaperSize.A4, PageSetup.Landscap, False, False, True) ''

        Dim qry1 As String = " Select FILE_INFO from TSPL_DEMAND_BOOKING_MASTER where Document_No= '" & clsCommon.myCstr(dr("Document Code")) & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim fileInfo As String = clsCommon.myCstr(dt.Rows(0)("FILE_INFO"))
            If clsCommon.myLen(fileInfo) > 0 Then
#Disable Warning
                Exit Sub
#Enable Warning
            End If
            If clsCommon.myLen(PDFPath) > 0 Then
                Dim FileNo As Integer = clsAttachDocument.UploadWithHttpRequest(PDFPath, Path.GetFileName(PDFPath), "DMND-BLL", clsCommon.myCstr(dr("Document Code")))
                If FileNo > 0 Then
                    Dim qry As String = " UPDATE TSPL_DEMAND_BOOKING_MASTER set FILE_INFO=" & clsCommon.myCstr(FileNo) & ",Send_By = '" & objCommonVar.CurrentUserCode & "',Send_Date = '" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") & "' where Document_No='" & clsCommon.myCstr(dr("Document Code")) & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                End If
                'Else
                '    clsCommon.MyMessageBoxShow(Me, "Empty File Path", Me.Text)
                '    Exit Sub
            End If
            'SaveFile(PDFPath, clsCommon.myCstr(dr("Documnet_Code")), clsCommon.myCDate(dr("Document_Date")), clsCommon.myCstr(dr("Customer_Code")), clsCommon.myCstr(dr("Customer_Name")), clsCommon.myCDate(txtDate2.Value))
        Else
            clsCommon.MyMessageBoxShow(Me, "Empty File Path", Me.Text)
        End If
    End Sub

    Private Sub btnResetDemand_Click(sender As Object, e As EventArgs) Handles btnResetDemand.Click
        Try
            txtMultDoc.arrValueMember = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    '==================End Demand=======================


    '==================Start Invoice=======================

    Private Sub txtMultInvoice__My_Click(sender As Object, e As EventArgs) Handles txtMultInvoice._My_Click
        Try
            strQry = Nothing
            strQry = "select TSPL_SD_SHIPMENT_HEAD.Document_Code as Code,TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No as [Invoice No], 
TSPL_SD_SHIPMENT_HEAD.Route_No As [Route No],case when TSPL_SD_SHIPMENT_HEAD.Shift_Type='AM' then 'Morning' else 'Evening' end As [Shift Type], 
CONVERT(varchar(10), TSPL_SD_SHIPMENT_HEAD.supply_date, 103) AS [Supply Date], TSPL_SD_SHIPMENT_HEAD.Customer_Code as [Customer Code], Customer_Name as [Customer Name],TSPL_SD_SHIPMENT_HEAD.Bill_To_Location as [Location Code],TSPL_SD_SHIPMENT_HEAD.Total_Amt as Amount
from TSPL_SD_SHIPMENT_HEAD 
Left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code 
left outer join  TSPL_LOCATION_MASTER on TSPL_SD_SHIPMENT_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code where TSPL_SD_SHIPMENT_HEAD.Status=1 and  TSPL_SD_SHIPMENT_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SHIPMENT_HEAD.Screen_Type='DS' "
            If clsCommon.myCDate(txtDate1.Value) <= clsCommon.myCDate(clsCommon.GETSERVERDATE()) Then
                strQry += " And Convert(Date, TSPL_SD_SHIPMENT_HEAD.supply_date,103) = Convert(Date,'" & txtDate2.Value & "',103) "
            Else
                clsCommon.MyMessageBoxShow(Me, "Date can't be greater then current day date !", Me.Text)
#Disable Warning
                Exit Sub
#Enable Warning
            End If
            txtMultInvoice.arrValueMember = clsCommon.ShowMultipleSelectForm(True, "InvDoc@", strQry, "Invoice No", "", txtMultInvoice.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSendInvoice_Click(sender As Object, e As EventArgs) Handles btnSendInvoice.Click
        clsCommon.ProgressBarPercentShow()
        Try
            strQry = Nothing
            strQry = "select TSPL_SD_SHIPMENT_HEAD.Document_Code,TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,CONVERT(VARCHAR(10), TSPL_SD_SHIPMENT_HEAD.Document_Date, 103)Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Email,TSPL_CUSTOMER_MASTER.Phone1,TSPL_CUSTOMER_MASTER.Phone2 from TSPL_SD_SHIPMENT_HEAD 
Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
where TSPL_SD_SHIPMENT_HEAD.Status=1 And CONVERT(Date,Supply_Date,103)=CONVERT(Date,'" & txtDate2.Value & "',103)"

            If txtMultInvoice.arrValueMember IsNot Nothing AndAlso txtMultInvoice.arrValueMember.Count > 0 Then
                strQry += " and TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No in (" & clsCommon.GetMulcallString(txtMultInvoice.arrValueMember) & ")"
            End If
            strQry += " and TSPL_SD_SHIPMENT_HEAD.FILE_INFO is null " 'and TSPL_SD_SALE_INVOICE_HEAD.FILE_INFO2 is null "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim ii As Integer = 0
                For Each dr As DataRow In dt.Rows
                    clsCommon.ProgressBarPercentUpdate((ii + 1) * 100 / dt.Rows.Count, " Sending " & (ii + 1) & " Of " & dt.Rows.Count)
                    ProcessFileInvoice(dr) ' Process each file individually
                    ii = ii + 1
                Next
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, "Bill Send Successfully Done.", Me.Text)
                txtMultInvoice.arrValueMember = Nothing
            Else
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
#Disable Warning
                Exit Sub
#Enable Warning
            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ProcessFileInvoice(dr As DataRow)
        Dim objInvoicePath As New frmShipmentDairy
        Dim PDFPath As String = objInvoicePath.PrintInvoiveForAll(clsCommon.myCstr(dr("Document_Code")), txtDate2.Value, clsCommon.myCstr(dr("Sale_Invoice_No")), False, True, clsCommon.myCstr(dr("Customer_Code")))
        Dim qry1 As String = " Select FILE_INFO from TSPL_SD_SHIPMENT_HEAD where Document_Code= '" & clsCommon.myCstr(dr("Document_Code")) & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim fileInfo As String = clsCommon.myCstr(dt.Rows(0)("FILE_INFO"))
            If clsCommon.myLen(fileInfo) > 0 Then
#Disable Warning
                Exit Sub
#Enable Warning
            End If
            If clsCommon.myLen(PDFPath) > 0 Then
                Dim FileNo As Integer = clsAttachDocument.UploadWithHttpRequest(PDFPath, Path.GetFileName(PDFPath), "INV-BLL", clsCommon.myCstr(dr("Document_Code")))
                If FileNo > 0 Then
                    Dim qry As String = " UPDATE TSPL_SD_SHIPMENT_HEAD set FILE_INFO=" & clsCommon.myCstr(FileNo) & ",Send_By = '" & objCommonVar.CurrentUserCode & "',Send_Date = '" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") & "' where Document_Code='" & clsCommon.myCstr(dr("Document_Code")) & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                End If
                'Else
                '    clsCommon.MyMessageBoxShow(Me, "Empty File Path", Me.Text)
                '    Exit Sub
            End If
            'SaveFile(PDFPath, clsCommon.myCstr(dr("Documnet_Code")), clsCommon.myCDate(dr("Document_Date")), clsCommon.myCstr(dr("Customer_Code")), clsCommon.myCstr(dr("Customer_Name")), clsCommon.myCDate(txtDate2.Value))
        Else
            clsCommon.MyMessageBoxShow(Me, "Empty File Path", Me.Text)
        End If
    End Sub

    Private Sub btnResetInvoice_Click(sender As Object, e As EventArgs) Handles btnResetInvoice.Click
        Try
            txtMultInvoice.arrValueMember = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    '==================End Invoice=======================


    '==================Start GatePass====================
    Private Sub txtMultGatePass__My_Click(sender As Object, e As EventArgs) Handles txtMultGatePass._My_Click
        Try
            strQry = Nothing
            strQry = " SELECT  TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode As [GP Code],convert(varchar(10),TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103) As [GP Date],convert(varchar(10),TSPL_DAIRYSALE_GATEPASS_MASTER.Supply_Date,103) As [Supply Date],TSPL_DAIRYSALE_GATEPASS_MASTER.ShiftType As [Shift Type],TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Number AS [Vehicle Number],
TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No As [Route No],tspl_Route_Master.Route_Desc As [Route Desc],TSPL_DAIRYSALE_GATEPASS_MASTER.Item_Type as [Item Type],TSPL_DAIRYSALE_GATEPASS_MASTER.Location_Code As [Location Code],TSPL_DAIRYSALE_GATEPASS_MASTER.Loading_Slip As [Loading Slip],TSPL_DAIRYSALE_GATEPASS_MASTER.TotalCrate 
FROM  TSPL_DAIRYSALE_GATEPASS_MASTER 
left Outer join tspl_Route_Master on tspl_Route_Master.Route_No = TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No
Where TSPL_DAIRYSALE_GATEPASS_MASTER.Post='Y' "
            If clsCommon.myCDate(txtDate1.Value) <= clsCommon.myCDate(clsCommon.GETSERVERDATE()) Then
                strQry += " And CONVERT(Date,TSPL_DAIRYSALE_GATEPASS_MASTER.Supply_Date,103)=CONVERT(Date,'" & txtDate3.Value & "',103) "
            Else
                clsCommon.MyMessageBoxShow(Me, "Date can't be greater then current day date !", Me.Text)
#Disable Warning
                Exit Sub
#Enable Warning
            End If
            txtMultGatePass.arrValueMember = clsCommon.ShowMultipleSelectForm(True, "GateDoc@", strQry, "GP Code", "", txtMultGatePass.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSendGatePass_Click(sender As Object, e As EventArgs) Handles btnSendGatePass.Click
        clsCommon.ProgressBarPercentShow()
        Try
            strQry = Nothing
            strQry = "SELECT  TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode As [GP Code],convert(varchar(10),TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103) As [GP Date],convert(varchar(10),TSPL_DAIRYSALE_GATEPASS_MASTER.Supply_Date,103) As [Supply Date],TSPL_DAIRYSALE_GATEPASS_MASTER.ShiftType As [Shift Type],TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No As [Route No],tspl_Route_Master.Route_Desc As [Route Desc],TSPL_DAIRYSALE_GATEPASS_MASTER.Item_Type as [Item Type],TSPL_DAIRYSALE_GATEPASS_MASTER.Location_Code As [Location Code] 
FROM  TSPL_DAIRYSALE_GATEPASS_MASTER 
left Outer join tspl_Route_Master on tspl_Route_Master.Route_No = TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No
where TSPL_DAIRYSALE_GATEPASS_MASTER.Post='Y'   And CONVERT(Date,TSPL_DAIRYSALE_GATEPASS_MASTER.Supply_Date,103)=CONVERT(Date,'" & txtDate3.Value & "',103)"
            strQry += " And TSPL_DAIRYSALE_GATEPASS_MASTER.File_Info Is Null "
            If txtMultGatePass.arrValueMember IsNot Nothing AndAlso txtMultGatePass.arrValueMember.Count > 0 Then
                strQry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode in (" & clsCommon.GetMulcallString(txtMultGatePass.arrValueMember) & ")"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim ii As Integer = 0
                For Each dr As DataRow In dt.Rows
                    clsCommon.ProgressBarPercentUpdate((ii + 1) * 100 / dt.Rows.Count, " Sending " & (ii + 1) & " Of " & dt.Rows.Count)
                    ProcessFileGatePass(dr) ' Process each file individually
                    ii = ii + 1
                Next
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, "Bill Send Successfully Done.", Me.Text)
                txtMultGatePass.arrValueMember = Nothing
            Else
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
#Disable Warning
                Exit Sub
#Enable Warning
            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ProcessFileGatePass(dr As DataRow)
        Dim objGatePassPath As New frmDairyGatePass
        Dim PDFPath As String = objGatePassPath.GatepassWithFilePath(clsCommon.myCstr(dr("GP Code")), clsCommon.myCstr(dr("GP Date")), clsCommon.myCstr(dr("Shift Type")), Nothing, Nothing, clsCommon.myCstr(dr("Route No")), clsCommon.myCstr(dr("Location Code")), True)
        Dim qry1 As String = " Select FILE_INFO from TSPL_DAIRYSALE_GATEPASS_MASTER where GPCode= '" & clsCommon.myCstr(dr("GP Code")) & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim fileInfo As String = clsCommon.myCstr(dt.Rows(0)("FILE_INFO"))
            If clsCommon.myLen(fileInfo) > 0 Then
#Disable Warning
                Exit Sub
#Enable Warning
            End If
            If clsCommon.myLen(PDFPath) > 0 Then
                Dim FileNo As Integer = clsAttachDocument.UploadWithHttpRequest(PDFPath, Path.GetFileName(PDFPath), "GTP-BLL", clsCommon.myCstr(dr("GP Code")))
                If FileNo > 0 Then
                    Dim qry As String = " UPDATE TSPL_DAIRYSALE_GATEPASS_MASTER set FILE_INFO=" & clsCommon.myCstr(FileNo) & ",Send_By = '" & objCommonVar.CurrentUserCode & "',Send_Date = '" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") & "' where GPCode='" & clsCommon.myCstr(dr("GP Code")) & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                End If
                'Else
                '    clsCommon.MyMessageBoxShow(Me, "Empty File Path", Me.Text)
                '    Exit Sub
            End If
            'SaveFile(PDFPath, clsCommon.myCstr(dr("Documnet_Code")), clsCommon.myCDate(dr("Document_Date")), clsCommon.myCstr(dr("Customer_Code")), clsCommon.myCstr(dr("Customer_Name")), clsCommon.myCDate(txtDate2.Value))
        Else
            clsCommon.MyMessageBoxShow(Me, "Empty File Path", Me.Text)
        End If
    End Sub

    Private Sub btnResetGatePass_Click(sender As Object, e As EventArgs) Handles btnResetGatePass.Click
        Try
            txtMultGatePass.arrValueMember = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    '==================End GatePass=======================


    'Private Sub SaveFile(ByVal FilePath As String, ByVal Documnet_No As String, ByVal Document_Date As DateTime, ByVal Cust_Code As String, ByVal Cust_Name As String, ByVal txtDate As Date)
    '    Try
    '        If Cust_Code IsNot Nothing AndAlso clsCommon.myLen(Cust_Code) > 0 Then
    '            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" & clsUserMgtCode.frmSendBilltoDistributor & "Invoice" & "'", Nothing)
    '            Dim Qry As String = "Select Email from TSPL_CUSTOMER_MASTER where Cust_Code='" & Cust_Code & "'"
    '            Dim arrMailID As List(Of String) = New List(Of String)()
    '            arrMailID.Add(clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry)))
    '            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 AndAlso arrMailID IsNot Nothing AndAlso arrMailID.Count > 0 Then
    '                Dim objEmailH As New clsEMailHead()
    '                'objEmailH.arrEMail = New List(Of String)()
    '                objEmailH.arrEMail = arrMailID
    '                objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
    '                objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
    '                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_NO, Documnet_No)
    '                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(Document_Date, "dd/MMM/yyyy"))
    '                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.VLCCode, Cust_Code)
    '                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.VLCName, Cust_Name)
    '                'objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.VLCUploaderCode, VLC_Uploader_Code)
    '                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(txtDate, "dd/MMM/yyyy"))
    '                'objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy"))
    '                objEmailH.Attachment_1_Path = FilePath
    '                objEmailH.SaveData(clsUserMgtCode.frmSendBilltoDistributor & "Invoice", objEmailH, Nothing)
    '                objEmailH = Nothing
    '            End If
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub


End Class
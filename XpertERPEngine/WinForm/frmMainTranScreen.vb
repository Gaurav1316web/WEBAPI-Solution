'==BM00000007778,BM00000007011===========================
Imports common
Imports System.IO
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports System.Windows.Forms
Imports Telerik.WinControls.UI
Imports System.Drawing
Imports Microsoft.Office.Interop
Imports common.UserControls

Public Class FrmMainTranScreen
#Region "Variables"
    Public isReadFlag As Boolean = False
    Public isModifyFlag As Boolean = False
    Public isDeleteFlag As Boolean = False
    Public isPostFlag As Boolean = False
    Public isReverse As Boolean = False
    Public isExport As Boolean = False
    ''===Parteek===''
    Public isPrintFlag As Boolean = False
    Public isQuickExportFlag As Boolean = False
    ''====End===''
    '==Sanjeet======
    Public isModifyonPasswordFlag As Boolean = False
    '=========
    Public isCancel_Flag As Boolean = False
    Public isCancel_Flag_After_Posting As Boolean = False
    Public isAmendmentFlag As Boolean = False
    Public isUpdateFlag As Boolean = False
    Public customFieldTabProperty As ElementVisibility = ElementVisibility.Collapsed
    Public Form_ID As String = ""
    Public ArrDetailFields As List(Of clsCustomFieldMapping)
    Public Module_Code As String = ""
    'Public Shared LastWorkingTime As Date = DateTime.Now()
    Public SendMailSms As String = "N"
    Public OldMouseX As Integer = 0
    Public OldMouseY As Integer = 0
    Public strDocNoForOpen As String = ""
    Dim is_Cancel_Allowed As String = String.Empty
    Public objCD As ControlDesigner = Nothing
    Public bolDesignMode As Boolean = False

    Public Is_SMS_Applied As Boolean = False
    Public Is_EMAIL_Applied As Boolean = False
    Public Is_Notification_Applied As Boolean = False
    Public PageSetupReport_ID As String = ""
    Public TemplateGridview As MyRadGridView
    Public ListImpExpColumnsMandatory As List(Of String)
    Public ListImpExpColumnsSuperMandatory As List(Of String)


#End Region
    '''' <summary>

    '''' </summar---------Update By Preeti Guptay>
    '''' <param name="FormID"></param>
    '''' <remarks></remarks>

    Public Sub SetUserMgmt(ByVal FormID As String)
        Me.KeyPreview = True
        Dim qry As String = ""
        If clsCommon.myLen(FormID) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Form ID not found", Me.Text)
            Me.Close()
            Exit Sub
        End If
        Me.Form_ID = FormID


        qry = " select (select inn.Parent_Code  from TSPL_PROGRAM_MASTER as inn where inn.program_code=TSPL_PROGRAM_MASTER.Parent_Code) as ModuleCode,Is_SMS_Applied,Is_EMAIL_Applied,Is_Notification_Applied from TSPL_PROGRAM_MASTER where program_code='" + Form_ID + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Me.Module_Code = clsCommon.myCstr(dt.Rows(0)("ModuleCode"))
            Me.Is_EMAIL_Applied = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_EMAIL_Applied")) = 1, True, False)
            Me.Is_SMS_Applied = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_SMS_Applied")) = 1, True, False)
            Me.Is_Notification_Applied = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Notification_Applied")) = 1, True, False)
        End If

        If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
            isReadFlag = True
            isModifyFlag = True
            isDeleteFlag = True
            isPostFlag = True
            isReverse = True
            isExport = True
            isCancel_Flag = True
            isCancel_Flag_After_Posting = True
            isPrintFlag = True
            isQuickExportFlag = True
            isModifyonPasswordFlag = False
            isAmendmentFlag = True
            isUpdateFlag = True
        Else
            qry = "select Read_Flag,Modify_Flag,Delete_Flag,Authorized_Flag, Reverse_Flag, Export_Flag,Print_Flag,cancel_Flag,cancel_Flag_After_Posting,QucikExport_Flag,isModifyonPassword,isnull(TSPL_GROUP_PROGRAM_MAPPING.is_Amendment,0) as is_Amendment,update_flag from TSPL_GROUP_PROGRAM_MAPPING where Program_Code='" + FormID + "' and Group_Code in (select Group_Code  from TSPL_USER_GROUP_MAPPING where User_Code='" + objCommonVar.CurrentUserCode + "')"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If isReadFlag = False Then
                        isReadFlag = IIf(clsCommon.myCdbl(dr("Read_Flag")) = 1, True, False)
                    End If
                    If isModifyFlag = False Then
                        isModifyFlag = IIf(clsCommon.myCdbl(dr("Modify_Flag")) = 1, True, False)
                    End If
                    If isDeleteFlag = False Then
                        isDeleteFlag = IIf(clsCommon.myCdbl(dr("Delete_Flag")) = 1, True, False)
                    End If
                    If isPostFlag = False Then
                        isPostFlag = IIf(clsCommon.myCdbl(dr("Authorized_Flag")) = 1, True, False)
                    End If
                    If isReverse = False Then
                        isReverse = IIf(clsCommon.myCdbl(dr("Reverse_Flag")) = 1, True, False)
                    End If
                    If isExport = False Then
                        isExport = IIf(clsCommon.myCdbl(dr("Export_Flag")) = 1, True, False)
                    End If
                    If isPrintFlag = False Then
                        isPrintFlag = IIf(clsCommon.myCdbl(dr("Print_Flag")) = 1, True, False)
                    End If
                    If isQuickExportFlag = False Then
                        isQuickExportFlag = IIf(clsCommon.myCdbl(dr("QucikExport_Flag")) = 1, True, False)
                    End If
                    If isCancel_Flag = False Then
                        isCancel_Flag = IIf(clsCommon.myCdbl(dr("Cancel_Flag")) = 1, True, False)
                    End If
                    If isCancel_Flag_After_Posting = False Then
                        isCancel_Flag_After_Posting = IIf(clsCommon.myCdbl(dr("Cancel_Flag_After_Posting")) = 1, True, False)
                    End If
                    If isModifyonPasswordFlag = False Then
                        isModifyonPasswordFlag = IIf(clsCommon.myCdbl(dr("isModifyonPassword")) = 1, True, False)
                    End If
                    If isAmendmentFlag = False Then
                        isAmendmentFlag = IIf(clsCommon.myCdbl(dr("is_Amendment")) = 1, True, False)
                    End If
                    If isUpdateFlag = False Then
                        isUpdateFlag = IIf(clsCommon.myCdbl(dr("update_flag")) = 1, True, False)
                    End If
                Next
            End If
        End If

        qry = "select 1 from TSPL_CUSTOM_FIELD_MAPPING where Program_Code='" + FormID + "' and Is_For_Detail_Level='0' "
        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtNew IsNot Nothing AndAlso dtNew.Rows.Count Then
            customFieldTabProperty = ElementVisibility.Visible
        End If
        ArrDetailFields = clsCustomFieldMapping.GetData(FormID, "D")



    End Sub
    '---- Created By preeti gupta-----ticket no.BM00000003244 
    Public Shared Function bankPermission(Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = ""
        Dim strvalue As String = ""
        qry = "select distinct bank_code from TSPL_User_Bank_mapping where Item_Code ='" + objCommonVar.CurrentUserCode + "'"
        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
            For Each dr As DataRow In dtNew.Rows
                strvalue = strvalue + ",'" + clsCommon.myCstr(dr("bank_code")) + "'"
                If strvalue.Substring(0, 1) = "," Then

                    strvalue = strvalue.Substring(1, strvalue.Length - 1)
                End If

            Next
        End If
        Try

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return strvalue
    End Function
    '---- Created By Richa Agarwal-----Ticket no. BM00000003242 on 29/07/2014
    Public Shared Function CustomerPermission() As String
        Dim qry As String = ""
        Dim strvalue As String = ""
        qry = "select distinct Cust_Code from TSPL_CUSTOMER_MAPPING where User_Code ='" + objCommonVar.CurrentUserCode + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
            For Each dr As DataRow In dtNew.Rows
                strvalue = strvalue & "'" & clsCommon.myCstr(dr("Cust_Code")).Replace("'", "''").ToString() & "'" & ","
            Next

            If strvalue <> "" Then
                strvalue = strvalue.Substring(0, strvalue.Length - 1)
            End If

        End If
        Try

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return strvalue
    End Function

    '---- Created By Rohit-----16-Oct-2014=========
    Public Function Cust_CustomerVendorMapping() As String
        Dim qry As String = ""
        Dim strvalue As String = ""
        qry = "select distinct Cust_Code from TSPL_CUSTOMER_VENDOR_MAPPING"
        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
            For Each dr As DataRow In dtNew.Rows
                strvalue = strvalue & "'" & clsCommon.myCstr(dr("Cust_Code")).Replace("'", "''").ToString() & "'" & ","
            Next

            If strvalue <> "" Then
                strvalue = strvalue.Substring(0, strvalue.Length - 1)
            End If

        End If
        Try

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return strvalue
    End Function


    Private Sub FrmMainTranScreen_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated



    End Sub

    Private Sub FrmMainTranScreen_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'Try
        '    If clsCommon.CompairString(Me.Form_ID, clsUserMgtCode.frmMCCDispatch) = CompairStringResult.Equal Then
        '        If FrmMccDispatch.isPortOpened Then
        '            e.Cancel = True
        '            clsCommon.MyMessageBoxShow("Please stop the port before You close the MCC Dispatch Screen")
        '        End If
        '    End If
        '    If clsCommon.CompairString(Me.Form_ID, clsUserMgtCode.frmQualityCheck) = CompairStringResult.Equal Then
        '        If FrmQualityCheck.isPortOpened Then
        '            e.Cancel = True
        '            clsCommon.MyMessageBoxShow("Please stop the port before You close the Quality Check Screen")
        '        End If
        '    End If
        '    If clsCommon.CompairString(Me.Form_ID, clsUserMgtCode.frmMilkReceipt) = CompairStringResult.Equal Then
        '        If frmMilkReceiptMCC.isPortOpened Then
        '            e.Cancel = True
        '            clsCommon.MyMessageBoxShow("Please stop the port before You close the  Milk Receipt Screen")
        '        End If
        '    End If
        '    If clsCommon.CompairString(Me.Form_ID, clsUserMgtCode.frmMilkSample) = CompairStringResult.Equal Then
        '        If frmMilkSampleMCC.isPortOpened Then
        '            e.Cancel = True
        '            clsCommon.MyMessageBoxShow("Please stop the port before You close the  Milk Sample Screen")
        '        End If
        '    End If
        'Catch ex As Exception

        'End Try
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowDesignAtRunTime, clsFixedParameterCode.AllowDesignAtRunTime, Nothing)) = 1 Then
            If objCD IsNot Nothing Then
                objCD.Dispose()
                objCD = Nothing
            End If
            'Create manager object that contains the load an save methods
            Dim objCM As New ControlManager

            'Save all supported properties of current form's controls (except btnDesigner) to config file
            If bolDesignMode Then
                If clsCommon.MyMessageBoxShow(Me, "Save Design Mode Data ?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    objCM.SaveProperties(Me, New List(Of Control)({}))
                End If
            End If
            bolDesignMode = False
            'Close manager
            objCM.Dispose()
            objCM = Nothing
        End If
    End Sub

    Private Sub FrmMainTranScreen_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'Ticket No-TEC/24/07/19-000956,TEC/26/07/19-000965 
        'If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.UseControlMForHelp, clsFixedParameterCode.UseControlMForHelp, Nothing), "0") = CompairStringResult.Equal Then
        If e.KeyCode = Keys.F1 AndAlso objCommonVar.ControlMForHelp = False Then
            Dim strpath = Application.StartupPath
            Dim strHelpPath As String = ""
            strHelpPath = strpath + "\HTMLHELPERP\" & Form_ID & ".html"
            Dim IsExists As Boolean = System.IO.File.Exists(strHelpPath)

            If IsExists = True Then
                Help.ShowHelp(Me, Application.StartupPath & "\HTMLHELPERP\usermanual.chm", HelpNavigator.Topic, Form_ID + ".html")
            Else
                Exit Sub
            End If
        End If
        'Else
        If e.Control AndAlso e.KeyCode = Keys.M AndAlso objCommonVar.ControlMForHelp = True Then
            Dim strpath = Application.StartupPath
            Dim strHelpPath As String = ""
            strHelpPath = strpath + "\HTMLHELPERP\" & Form_ID & ".html"
            Dim IsExists As Boolean = System.IO.File.Exists(strHelpPath)

            If IsExists = True Then
                Help.ShowHelp(Me, Application.StartupPath & "\HTMLHELPERP\usermanual.chm", HelpNavigator.Topic, Form_ID + ".html")
            Else
                Exit Sub
            End If
        End If
        'End If
        If e.Control And e.Shift And e.KeyCode = Keys.L Then
            LoadChangeLabel()
        End If

        If e.Control And e.Shift And e.Alt And e.KeyCode = Keys.J Then
            'If clsCommon.CompairString(Form_ID, clsUserMgtCode.frmCSASaleInvoice) = CompairStringResult.Equal Then ''journal entry skip on Sale Patti[By Amit Sir 05/10/2015]
            '    Exit Sub
            'End If
            strRvalue = ""
            Dim strCode As String = getNavigatorValue(Me)
            'If clsCommon.myLen(strCode) <= 0 Then
            '    clsCommon.MyMessageBoxShow("No Document Found on Current Screen")
            '    Exit Sub
            'End If
            'If clsCommon.CompairString(Form_ID, clsUserMgtCode.frmPaymentProcessFarmer) = CompairStringResult.Equal Then
            '    Application.OpenForms("MDI").Controls("__txtDocNo").Text = strCode
            '    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmGLTransReport
            '    strRvalue = ""
            '    Exit Sub
            'End If

            'Dim qry As String = " select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" & strCode & "' "

            'If clsCommon.CompairString(Form_ID, clsUserMgtCode.ReceiptAdjustmentEntry) = CompairStringResult.Equal Then
            '    qry += " And TSPL_JOURNAL_MASTER.Source_Code ='AR-AD' "
            'ElseIf clsCommon.CompairString(Form_ID, clsUserMgtCode.PaymentAdjustmentEntry) = CompairStringResult.Equal Then
            '    qry += " And TSPL_JOURNAL_MASTER.Source_Code ='AP-AD' "
            'End If

            'strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            'If clsCommon.myLen(strCode) <= 0 Then
            '    clsCommon.MyMessageBoxShow("No Journal Entry Found For Current Document")
            '    Exit Sub
            'Else
            '    Application.OpenForms("MDI").Controls("__txtDocNo").Text = strCode
            '    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.journalEntry
            '    strRvalue = ""
            'End If
            ShowJE(Form_ID, strCode)
        End If

        If e.Control And e.Shift And e.Alt And e.KeyCode = Keys.A Then
            strRvalue = ""
            Dim strCode As String = getNavigatorValue(Me)
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Document Found on Current Screen", Me.Text)
                Exit Sub
            End If
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Document_No  from TSPL_VENDOR_INVOICE_HEAD where Description like'%" & strCode & "%' "))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No AP Invoice Entry Found For Current Document")
            Else

                Application.OpenForms("MDI").Controls("__txtDocNo").Text = strCode
                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnAPInvoiceEntry
                'objCommonVar.ScreenToOpen = clsUserMgtCode.mbtnAPInvoiceEntry
                'objCommonVar.ScreenToOpenDocNo = strCode
                strRvalue = ""
            End If
        End If

        '=================================Ticket No : TEC/03/10/18-000332=============================================
        If e.Control And e.Shift And e.Alt And e.KeyCode = Keys.B Then
            strRvalue = ""
            Dim strCode As String = getNavigatorValue(Me)
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Document Found on Current Screen", Me.Text)
                Exit Sub
            End If
            'Ticket No :TEC/10/09/19-001005
            If clsCommon.CompairString(Me.Form_ID, clsUserMgtCode.frmMCCMaterial) = CompairStringResult.Equal OrElse clsCommon.CompairString(Me.Form_ID, clsUserMgtCode.frmShipmentProductSale) = CompairStringResult.Equal OrElse clsCommon.CompairString(Me.Form_ID, clsUserMgtCode.FrmDispatchFreshSale) = CompairStringResult.Equal OrElse clsCommon.CompairString(Me.Form_ID, clsUserMgtCode.frmSaleDispatchDairy) = CompairStringResult.Equal Then
                strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Sale_Invoice_No from TSPL_SD_SHIPMENT_HEAD where Document_Code = '" + strCode + "'"))
            ElseIf clsCommon.CompairString(Me.Form_ID, clsUserMgtCode.ScrapSale) = CompairStringResult.Equal Then
                strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select invoice_No from TSPL_SCRAPINVOICE_HEAD where shipment_No = '" + strCode + "'"))
            ElseIf clsCommon.CompairString(Me.Form_ID, clsUserMgtCode.FrmCanSale) = CompairStringResult.Equal Then
                strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Document_No from TSPL_CANSALE_INVOICE_HEAD where CanSale_Doc_No= '" + strCode + "'"))

            End If
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Document_No  from TSPL_Customer_Invoice_Head where Description like'%" & strCode & "%' Or RefDocNo like'%" & strCode & "%' Or AgainstScrap like'%" & strCode & "%' Or Against_Sale_No like'%" & strCode & "%' Or Against_Sale_Return_No like'%" & strCode & "%' Or Against_MCC_Material_Sale_Return like'%" & strCode & "%' Or Against_VCGL like'%" & strCode & "%' Or Against_Service_Visit_Code like'%" & strCode & "%' Or Against_Asset_Disposal like'%" & strCode & "%' Or AgainstScrapReturn  like'%" & strCode & "%' Or Against_Security_Receipt_No like'%" & strCode & "%' Or Against_Subsidy_No like '%" & strCode & "%' "))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No AR Invoice Entry Found For Current Document")
            Else

                Application.OpenForms("MDI").Controls("__txtDocNo").Text = strCode
                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnARInvoiceEntry
                strRvalue = ""
            End If
            e.Handled = True
        End If

        If e.Control AndAlso e.Alt AndAlso e.Shift AndAlso e.KeyCode = Keys.F6 Then
            If Me.Is_EMAIL_Applied OrElse Me.Is_SMS_Applied OrElse Me.Is_Notification_Applied Then
                Dim frmPWD As New FrmPWD(Nothing)
                frmPWD.strType = clsFixedParameterType.SMSEMailPassword
                frmPWD.strCode = clsFixedParameterCode.SMSEMailPassword
                frmPWD.ShowDialog()
                If frmPWD.isPasswordCorrect Then
                    Dim qry As String = "select * from ( select Program_Code+'1' as Code,ES_Trans_Type_1 as Name  from TSPL_PROGRAM_MASTER where Program_Code='" + Me.Form_ID + "'" + Environment.NewLine + _
                     "union all" + Environment.NewLine + _
                     "select Program_Code+'2' as Code,ES_Trans_Type_2 as Name from TSPL_PROGRAM_MASTER where Program_Code='" + Me.Form_ID + "'" + Environment.NewLine + _
                     "union all" + Environment.NewLine + _
                     "select Program_Code+'3' as Code,ES_Trans_Type_3 as Name from TSPL_PROGRAM_MASTER where Program_Code='" + Me.Form_ID + "'" + Environment.NewLine + _
                     "union all" + Environment.NewLine + _
                     "select Program_Code+'4' as Code,ES_Trans_Type_4 as Name from TSPL_PROGRAM_MASTER where Program_Code='" + Me.Form_ID + "'" + Environment.NewLine + _
                     "union all" + Environment.NewLine + _
                     "select Program_Code+'5' as Code,ES_Trans_Type_5 as Name from TSPL_PROGRAM_MASTER where Program_Code='" + Me.Form_ID + "')xx where len(isnull(Name,''))>0"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim strFormID As String = Me.Form_ID
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim dr As DataRow = dt.NewRow()
                        dr("Code") = Me.Form_ID
                        dr("Name") = "Transaction"
                        dt.Rows.InsertAt(dr, 0)

                        Dim frmFC As New FrmFreeComboBox
                        frmFC.ComboSource = dt
                        frmFC.ComboValueMember = "Code"
                        frmFC.ComboDisplayMember = "Name"
                        frmFC.ShowDialog()
                        If clsCommon.myLen(frmFC.strRetValue) > 0 Then
                            strFormID = clsCommon.myCstr(frmFC.strRetValue)
                        Else
                            strFormID = ""
                        End If
                    End If
                    If clsCommon.myLen(strFormID) > 0 Then
                        Dim frm As New frmEMailAndSMSSetting
                        frm.isForSMS = Me.Is_SMS_Applied
                        frm.isForEMail = Me.Is_EMAIL_Applied
                        frm.isForNotification = Me.Is_Notification_Applied
                        frm.Form_ID = strFormID
                        frm.ShowDialog()
                    End If
                End If
            End If
        ElseIf e.Control AndAlso e.Alt AndAlso e.Shift AndAlso e.KeyCode = Keys.F7 Then
            Dim frmPWD As New FrmPWD(Nothing)
            frmPWD.strType = clsFixedParameterType.SettlementBankOnlyPWD
            frmPWD.strCode = clsFixedParameterCode.SettlementBankOnlyPWD
            frmPWD.ShowDialog()
            If frmPWD.isPasswordCorrect Then
                Dim frm As New frmSetting
                frm.strFormID = Me.Form_ID
                frm.ShowDialog()
                If frm.isDataSaved Then
                    clsCommon.MyMessageBoxShow(Me, "Setting saved successfully." + Environment.NewLine + Me.Text + " will close automatic For apply new settings")
                    clsERPFuncationality.closeForm(Me)
                End If
            End If
        End If
        If e.Control And e.Shift And e.KeyCode = Keys.R Then
            FindAndRestoreGridLayout(Me)
        End If
        If e.Control And e.Shift And e.Alt And e.KeyCode = Keys.X Then
            If objCommonVar.is_Cancel_Allowed = "1" Then
                Dim obj As New FrmMainTranScreen
                obj.SetUserMgmt(Me.Form_ID)
                If obj.isCancel_Flag = True Then
                    If clsCommon.MyMessageBoxShow(Me, "Do you want to cancel this Document.?", "cancel Docuement", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        Dim cancel_after_Posting_Date As Date = Nothing
                        If obj.isCancel_Flag_After_Posting Then
                            obj.isCancel_Flag_After_Posting = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Cancel_After_Posting_Tables_Details where form_id='" & Me.Form_ID & "'", trans)) > 0, True, False)
                            cancel_after_Posting_Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select convert(date,starting_date,103) from TSPL_Cancel_After_Posting_Tables_Details where form_id='" & Me.Form_ID & "'", trans))
                        End If
                        frmClientFormLableDetails.CancelDocument(Me, Me.Form_ID, trans, cancel_after_Posting_Date, Me, obj.isCancel_Flag_After_Posting, True)
                    End If
                End If
            End If

        End If
        If e.Alt And e.KeyCode = Keys.D Then
            If is_Cancel_Allowed = "1" Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_Cancel_Table_Details where Form_Id='" & Me.Form_ID & "'")
                If dt.Rows.Count > 0 Then
                    Me.isDeleteFlag = False
                End If
            End If
        End If


        If e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.X Then
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowDesignAtRunTime, clsFixedParameterCode.AllowDesignAtRunTime, Nothing)) = 1 Then
                Select Case bolDesignMode
                    Case False
                        If clsCommon.MyMessageBoxShow(Me, "Proceed To Design Mode ?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                            'Set some properties
                            bolDesignMode = True
                            'Activate design mode by creating designer class with current form but exclude btndesigner from beeing designed
                            objCD = New ControlDesigner(Me, New List(Of Control)({}), Color.LightYellow)
                        End If
                    Case Else
                        If clsCommon.MyMessageBoxShow(Me, "Save and Exit Design Mode ?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                            'Exit design mode by disposing the designer class
                            objCD.Dispose()
                            objCD = Nothing
                            'Reset some properties
                            bolDesignMode = False
                        End If
                End Select
            End If
        End If


        If e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F4 Then
            clsCreateAllTables.IsShowMenuOnRightClick = Not clsCreateAllTables.IsShowMenuOnRightClick
        End If

        If e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F5 Then
            Dim frm As New frmScreenControlMappingMultiple
            frm.formId = Me.Form_ID
            frm.WindowState = FormWindowState.Maximized
            frm.ShowDialog()
        End If

        'sanjay
        If e.Control And e.KeyCode = Keys.F Then
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim frm As New frmPDFPageSize
                frm.Text = "PDF Page Size [" + PageSetupReport_ID + "]"
                frm.PDFReportID = PageSetupReport_ID
                frm.strCurrentUserCode = objCommonVar.CurrentUserCode
                frm.ShowDialog()
            End If
        End If
        'Sanjay Ticket No- TEC/12/12/18-000379 Template
        If e.Alt And e.KeyCode = Keys.T Then
            If clsCommon.myLen(PageSetupReport_ID) > 0 AndAlso TemplateGridview IsNot Nothing Then
                If TemplateGridview.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data in Grid", Me.Text)
                    Exit Sub
                End If
                'Dim obj As New clsGridLayout()
                'TemplateGridview.MasterTemplate.FilterDescriptors.Clear()
                'obj = New clsGridLayout()
                'obj.GridLayout = New MemoryStream()
                'TemplateGridview.SaveLayout(obj.GridLayout)
                'obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                'Dim frm As New frmCreateTemplate()
                'Dim reader As StreamReader = New StreamReader(obj.GridLayout)
                'Dim strToSaveGridLayout As String = reader.ReadToEnd()
                'frm.ReportId = PageSetupReport_ID
                'frm.ColumnCount = TemplateGridview.ColumnCount
                'frm.StrGridLayout = strToSaveGridLayout
                'frm.ShowDialog()
                'obj.GridLayout.Close()
                'obj.GridLayout.Dispose()

                Dim frm As New frmExportTemplate(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, PageSetupReport_ID)
                frm.gvReport = TemplateGridview
                frm.dtReportType = Nothing
                frm.Report_Type = ""
                frm.ShowDialog()
            End If
        End If
        'Import/Export Template
        'If e.Control And e.Alt And e.KeyCode = Keys.T Then
        '    If clsCommon.myLen(PageSetupReport_ID) > 0 AndAlso ListImpExpColumns IsNot Nothing Then
        '        If ListImpExpColumns.Count <= 0 Then
        '            clsCommon.MyMessageBoxShow("No column to export")
        '            Exit Sub
        '        End If

        '        Dim frm As New frmTemplateExpImp(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, PageSetupReport_ID)
        '        frm.ListImpExpColumns1 = ListImpExpColumns
        '        frm.ListImpExpColumnsMandatory1 = ListImpExpColumnsMandatory
        '        frm.dtReportType = Nothing
        '        frm.Report_Type = ""
        '        frm.ShowDialog()
        '    End If
        'End If
    End Sub
    Dim isSavedGrid As Boolean = False
    Sub SaveLayout(ByRef gridName As Control)
        If clsCommon.myLen(Me.Form_ID) > 0 Then
            CType(gridName, RadGridView).MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = Me.Form_ID & gridName.Name.ToString() & clsCommon.myCstr(gridName.Tag)
            'obj.ReportID = obj.ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            CType(gridName, RadGridView).SaveLayout(obj.GridLayout)
            obj.GridColumns = CType(gridName, RadGridView).ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            isSavedGrid = obj.SaveData()

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Sub setUpDownFalse(ByRef gridName As Control)

        For i As Integer = 0 To CType(gridName, RadGridView).Columns.Count - 1
            If TypeOf CType(gridName, RadGridView).Columns(i) Is GridViewDecimalColumn Then
                CType(CType(gridName, RadGridView).Columns(i), GridViewDecimalColumn).ShowUpDownButtons = False
                CType(CType(gridName, RadGridView).Columns(i), GridViewDecimalColumn).Step = 0
            End If
        Next
        'CurGrid = gridName
        'AddHandler CType(gridName, RadGridView).CellEditorInitialized, AddressOf CommonGridCellEditorInitialized

    End Sub
    'Dim CurGrid As Control = Nothing
    'Private Sub CommonGridCellEditorInitialized(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
    '    If TypeOf CType(CurGrid, RadGridView).CurrentColumn Is GridViewDecimalColumn Then
    '        Dim gv As RadGridView = CType(CurGrid, RadGridView)

    '        Dim editor As Telerik.WinControls.UI.RadTextBoxEditor = TryCast(gv.ActiveEditor, RadTextBoxEditor)
    '        Dim oszlop As Telerik.WinControls.UI.GridViewDecimalColumn = TryCast(gv.CurrentColumn, Telerik.WinControls.UI.GridViewDecimalColumn)
    '        If editor IsNot Nothing And oszlop IsNot Nothing Then
    '            Dim editorElement As Telerik.WinControls.UI.RadTextBoxElement = TryCast(editor.EditorElement, RadTextBoxElement)

    '            Try
    '                RemoveHandler editorElement.KeyDown, AddressOf CommonGridKeyDown
    '            Catch ex As Exception
    '            End Try
    '            AddHandler editorElement.KeyDown, AddressOf CommonGridKeyDown
    '        End If
    '    End If

    'End Sub
    'Private Sub CommonGridKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
    '    If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
    '        e.Handled = True
    '    End If
    'End Sub
    Sub BestFitGridColumn(ByRef gridName As Control)
        For i As Integer = 0 To CType(gridName, RadGridView).Columns.Count - 1
            CType(gridName, RadGridView).Columns(i).BestFit()
        Next
    End Sub


    Sub ReStoreGridLayoutMain(ByRef gridName As Control)
        Try
            If clsCommon.myLen(Me.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Me.Form_ID & gridName.Name.ToString & clsCommon.myCstr(gridName.Tag), "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= CType(gridName, RadGridView).ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To CType(gridName, RadGridView).Columns.Count - 1 Step ii + 1
                        CType(gridName, RadGridView).Columns(ii).IsVisible = False
                        CType(gridName, RadGridView).Columns(ii).VisibleInColumnChooser = True
                    Next
                    CType(gridName, RadGridView).LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
                '        BestFitGridColumn(gridName)
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        GC.Collect()
    End Sub

    Dim strGridName As String = ""
    Public Sub FindAndSaveGridLayout(ByRef formname As XpertERPEngine.FrmMainTranScreen, Optional ByVal contrl As Control = Nothing)
        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If Not (TypeOf (ctrl) Is MyCheckBoxGrid) Then
                    If ctrl.HasChildren = True Then
                        FindAndSaveGridLayout(Me, ctrl)
                    End If
                    If TypeOf (ctrl) Is RadGridView Or TypeOf (ctrl) Is DataGridView Or TypeOf (ctrl) Is common.UserControls.MyRadGridView Then
                        Try
                            SaveLayout(ctrl)
                            CType(ctrl, RadGridView).AutoSizeRows = True
                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try
                    End If
                End If
            Next
        Else
            If TypeOf (contrl) Is RadGridView Or TypeOf (contrl) Is DataGridView Or TypeOf (contrl) Is common.UserControls.MyRadGridView Then
                Try
                    SaveLayout(contrl)
                    CType(contrl, RadGridView).AutoSizeRows = True
                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                End Try
            Else
                '' change in query by Panch Raj against ticket no-UDL/21/05/18-000168
                For Each ctrl As Control In contrl.Controls
                    If Not TypeOf (ctrl) Is MyCheckBoxGrid Then
                        If ctrl.HasChildren = True Then
                            FindAndSaveGridLayout(Me, ctrl)
                        End If
                        If TypeOf (ctrl) Is RadGridView Or TypeOf (ctrl) Is DataGridView Or TypeOf (ctrl) Is common.UserControls.MyRadGridView Then
                            Try
                                SaveLayout(ctrl)
                                CType(ctrl, RadGridView).AutoSizeRows = True
                            Catch ex As Exception
                                MessageBox.Show(ex.ToString)
                            End Try
                        End If
                    End If
                Next
            End If

        End If
    End Sub

    Public Sub FindAndSetTabStopFalse(ByRef formname As XpertERPEngine.FrmMainTranScreen, Optional ByVal contrl As Control = Nothing)
        'MyreadOnly
        'common.UserControls.txtNavigator
        'common.UserControls.txtFinder

        'Readonly
        'common.Controls.MyCheckBox
        'common.Controls.MyComboBox
        'common.Controls.MyDateTimePicker
        'common.Controls.MyRadioButton
        'common.Controls.MyTextBox
        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If ctrl.HasChildren = True Then
                    FindAndSetTabStopFalse(Me, ctrl)
                End If
                'If TypeOf (ctrl) Is common.UserControls.txtNavigator Then
                '    Try
                '        If CType(ctrl, common.UserControls.txtNavigator).MyReadOnly = True Then
                '            CType(ctrl, common.UserControls.txtNavigator).TabStop = False
                '            CType(ctrl, common.UserControls.txtNavigator).MendatroryField = False
                '            CType(ctrl, common.UserControls.txtNavigator).Enabled = False
                '        End If
                '    Catch ex As Exception
                '    End Try
                'End If

                If TypeOf (ctrl) Is common.UserControls.txtFinder Then
                    Try
                        If CType(ctrl, common.UserControls.txtFinder).MyReadOnly = True Then
                            CType(ctrl, common.UserControls.txtFinder).TabStop = False
                            CType(ctrl, common.UserControls.txtFinder).MendatroryField = False
                            'CType(ctrl, common.UserControls.txtFinder).Enabled = False
                        End If
                    Catch ex As Exception
                    End Try
                End If

                If TypeOf (ctrl) Is common.Controls.MyCheckBox Then
                    Try
                        If CType(ctrl, common.Controls.MyCheckBox).ReadOnly = True Then
                            CType(ctrl, common.Controls.MyCheckBox).TabStop = False
                            'CType(ctrl, common.Controls.MyCheckBox).Enabled = False
                        End If
                    Catch ex As Exception
                    End Try
                End If

                If TypeOf (ctrl) Is common.Controls.MyComboBox Then
                    Try
                        If CType(ctrl, common.Controls.MyComboBox).ReadOnly = True Then
                            CType(ctrl, common.Controls.MyComboBox).TabStop = False
                            CType(ctrl, common.Controls.MyComboBox).MendatroryField = False
                            ' CType(ctrl, common.Controls.MyComboBox).Enabled = False
                        End If
                    Catch ex As Exception
                    End Try
                End If

                If TypeOf (ctrl) Is common.Controls.MyDateTimePicker Then
                    Try
                        If CType(ctrl, common.Controls.MyDateTimePicker).ReadOnly = True Then
                            CType(ctrl, common.Controls.MyDateTimePicker).TabStop = False
                            CType(ctrl, common.Controls.MyDateTimePicker).MendatroryField = False
                            ' CType(ctrl, common.Controls.MyDateTimePicker).Enabled = False
                        End If
                    Catch ex As Exception
                    End Try
                End If

                If TypeOf (ctrl) Is common.Controls.MyRadioButton Then
                    Try
                        If CType(ctrl, common.Controls.MyRadioButton).ReadOnly = True Then
                            CType(ctrl, common.Controls.MyRadioButton).TabStop = False
                            'CType(ctrl, common.Controls.MyRadioButton).Enabled = False
                        End If
                    Catch ex As Exception
                    End Try
                End If

                If TypeOf (ctrl) Is common.Controls.MyTextBox Then
                    Try
                        If CType(ctrl, common.Controls.MyTextBox).ReadOnly = True Then
                            CType(ctrl, common.Controls.MyTextBox).TabStop = False
                            CType(ctrl, common.Controls.MyTextBox).MendatroryField = False
                            ' CType(ctrl, common.Controls.MyTextBox).Enabled = False
                        End If
                    Catch ex As Exception
                    End Try
                End If
                If TypeOf (ctrl) Is common.MyNumBox Then
                    Try
                        If CType(ctrl, common.MyNumBox).ReadOnly = True Then
                            CType(ctrl, common.MyNumBox).TabStop = False
                            CType(ctrl, common.MyNumBox).MendatroryField = False
                            ' CType(ctrl, common.MyNumBox).Enabled = False
                        End If
                    Catch ex As Exception
                    End Try
                End If
            Next
        Else
            For Each ctrl As Control In contrl.Controls
                If ctrl.HasChildren = True Then
                    FindAndSetTabStopFalse(Me, ctrl)
                End If
                'If TypeOf (ctrl) Is common.UserControls.txtNavigator Then
                '    Try
                '        If CType(ctrl, common.UserControls.txtNavigator).MyReadOnly = True Then
                '            CType(ctrl, common.UserControls.txtNavigator).TabStop = False
                '            CType(ctrl, common.UserControls.txtNavigator).MendatroryField = False
                '            CType(ctrl, common.UserControls.txtNavigator).Enabled = False
                '        End If
                '    Catch ex As Exception
                '    End Try
                'End If

                If TypeOf (ctrl) Is common.UserControls.txtFinder Then
                    Try
                        If CType(ctrl, common.UserControls.txtFinder).MyReadOnly = True Then
                            CType(ctrl, common.UserControls.txtFinder).TabStop = False
                            CType(ctrl, common.UserControls.txtFinder).MendatroryField = False
                            ' CType(ctrl, common.UserControls.txtFinder).Enabled = False
                        End If
                    Catch ex As Exception
                    End Try
                End If

                If TypeOf (ctrl) Is common.Controls.MyCheckBox Then
                    Try
                        If CType(ctrl, common.Controls.MyCheckBox).ReadOnly = True Then
                            CType(ctrl, common.Controls.MyCheckBox).TabStop = False
                            'CType(ctrl, common.Controls.MyCheckBox).Enabled = False
                        End If
                    Catch ex As Exception
                    End Try
                End If

                If TypeOf (ctrl) Is common.Controls.MyComboBox Then
                    Try
                        If CType(ctrl, common.Controls.MyComboBox).ReadOnly = True Then
                            CType(ctrl, common.Controls.MyComboBox).TabStop = False
                            CType(ctrl, common.Controls.MyComboBox).MendatroryField = False
                            'CType(ctrl, common.Controls.MyComboBox).Enabled = False
                        End If
                    Catch ex As Exception
                    End Try
                End If

                If TypeOf (ctrl) Is common.Controls.MyDateTimePicker Then
                    Try
                        If CType(ctrl, common.Controls.MyDateTimePicker).ReadOnly = True Then
                            CType(ctrl, common.Controls.MyDateTimePicker).TabStop = False
                            CType(ctrl, common.Controls.MyDateTimePicker).MendatroryField = False
                            'CType(ctrl, common.Controls.MyDateTimePicker).Enabled = False
                        End If
                    Catch ex As Exception
                    End Try
                End If

                If TypeOf (ctrl) Is common.Controls.MyRadioButton Then
                    Try
                        If CType(ctrl, common.Controls.MyRadioButton).ReadOnly = True Then
                            CType(ctrl, common.Controls.MyRadioButton).TabStop = False
                            ' CType(ctrl, common.Controls.MyRadioButton).Enabled = False
                        End If
                    Catch ex As Exception
                    End Try
                End If

                If TypeOf (ctrl) Is common.Controls.MyTextBox Then
                    Try
                        If CType(ctrl, common.Controls.MyTextBox).ReadOnly = True Then
                            CType(ctrl, common.Controls.MyTextBox).TabStop = False
                            CType(ctrl, common.Controls.MyTextBox).MendatroryField = False
                            'CType(ctrl, common.Controls.MyTextBox).Enabled = False
                        End If
                    Catch ex As Exception
                    End Try
                End If

                If TypeOf (ctrl) Is common.MyNumBox Then
                    Try
                        If CType(ctrl, common.MyNumBox).ReadOnly = True Then
                            CType(ctrl, common.MyNumBox).TabStop = False
                            CType(ctrl, common.MyNumBox).MendatroryField = False
                            'CType(ctrl, common.MyNumBox).Enabled = False
                        End If
                    Catch ex As Exception
                    End Try
                End If
            Next
        End If
    End Sub

    Public Sub FindAndSetgridUpDownFalse(ByRef formname As XpertERPEngine.FrmMainTranScreen, Optional ByVal contrl As Control = Nothing)

        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If ctrl.HasChildren = True Then
                    FindAndSetgridUpDownFalse(Me, ctrl)

                End If

                If TypeOf (ctrl) Is RadGridView Then
                    Try
                        setUpDownFalse(ctrl)

                    Catch ex As Exception
                    End Try
                End If
            Next

        Else
            For Each ctrl As Control In contrl.Controls
                If ctrl.HasChildren = True Then
                    FindAndSetgridUpDownFalse(Me, ctrl)
                End If


                If TypeOf (ctrl) Is RadGridView Then
                    Try
                        setUpDownFalse(ctrl)
                    Catch ex As Exception
                    End Try
                End If
            Next
        End If
    End Sub

    Public Sub AddMouseMove(ByRef formname As XpertERPEngine.FrmMainTranScreen, Optional ByVal contrl As Control = Nothing)
        Try
            If IsNothing(contrl) Then
                For Each ctrl As Control In formname.Controls
                    If ctrl.HasChildren = True Then
                        AddMouseMove(Me, ctrl)
                    End If
                    AddHandler ctrl.MouseMove, AddressOf FrmMainTranScreen_MouseMove
                Next
            Else
                For Each ctrl As Control In contrl.Controls
                    If ctrl.HasChildren = True Then
                        AddMouseMove(Me, ctrl)
                    End If
                    AddHandler ctrl.MouseMove, AddressOf FrmMainTranScreen_MouseMove
                Next
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub FindAndDeleteGridLayout(ByRef formname As XpertERPEngine.FrmMainTranScreen, Optional ByVal contrl As Control = Nothing)
        If IsNothing(contrl) Then


            For Each ctrl As Control In formname.Controls
                If ctrl.HasChildren = True Then
                    FindAndDeleteGridLayout(Me, ctrl)
                End If
                If TypeOf (ctrl) Is RadGridView Or TypeOf (ctrl) Is DataGridView Or TypeOf (ctrl) Is common.UserControls.MyRadGridView Then
                    Try
                        clsGridLayout.DeleteData(Me.Form_ID & ctrl.Name & clsCommon.myCstr(ctrl.Tag), objCommonVar.CurrentUserCode)
                        isSavedGrid = True
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            Next
        Else
            '' change in query by Panch Raj against ticket no-UDL/21/05/18-000168
            If TypeOf (contrl) Is RadGridView Or TypeOf (contrl) Is DataGridView Or TypeOf (contrl) Is common.UserControls.MyRadGridView Then
                Try
                    clsGridLayout.DeleteData(Me.Form_ID & contrl.Name & clsCommon.myCstr(contrl.Tag), objCommonVar.CurrentUserCode)
                    isSavedGrid = True
                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                End Try
            Else
                For Each ctrl As Control In contrl.Controls
                    If ctrl.HasChildren = True Then
                        FindAndDeleteGridLayout(Me, ctrl)
                    End If
                    If TypeOf (ctrl) Is RadGridView Or TypeOf (ctrl) Is DataGridView Or TypeOf (ctrl) Is common.UserControls.MyRadGridView Then
                        Try
                            clsGridLayout.DeleteData(Me.Form_ID & ctrl.Name & clsCommon.myCstr(ctrl.Tag), objCommonVar.CurrentUserCode)
                            isSavedGrid = True
                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try
                    End If
                Next
            End If

        End If
    End Sub
    Public Sub FindAndRestoreGridLayout(ByRef formname As XpertERPEngine.FrmMainTranScreen, Optional ByVal contrl As Control = Nothing)
        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If Not (TypeOf (ctrl) Is MyCheckBoxGrid) Then
                    If ctrl.HasChildren = True Then
                        FindAndRestoreGridLayout(Me, ctrl)
                    End If
                    If TypeOf (ctrl) Is RadGridView Or TypeOf (ctrl) Is DataGridView Or TypeOf (ctrl) Is common.UserControls.MyRadGridView Then
                        Try
                            ReStoreGridLayoutMain(ctrl)
                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try
                    End If
                End If
            Next
        Else
            '' change in query by Panch Raj against ticket no-UDL/21/05/18-000168
            If TypeOf (contrl) Is RadGridView Or TypeOf (contrl) Is DataGridView Or TypeOf (contrl) Is common.UserControls.MyRadGridView Then
                Try
                    ReStoreGridLayoutMain(contrl)
                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                End Try
            Else
                For Each ctrl As Control In contrl.Controls
                    If Not (TypeOf (ctrl) Is MyCheckBoxGrid) Then
                        If ctrl.HasChildren = True Then
                            FindAndRestoreGridLayout(Me, ctrl)
                        End If
                        If TypeOf (ctrl) Is RadGridView Or TypeOf (ctrl) Is DataGridView Or TypeOf (ctrl) Is common.UserControls.MyRadGridView Then
                            Try
                                ReStoreGridLayoutMain(ctrl)
                            Catch ex As Exception
                                MessageBox.Show(ex.ToString)
                            End Try
                        End If
                    End If
                Next
            End If

        End If
    End Sub
    Private Sub LoadChangeLabel()
        'To be Uncomment
        Try
            Dim obj As New frmClientFormLableDetails
            obj.formnam = Me
            obj.formcode = Form_ID
            ' obj.LoadLableData(Me)
            obj.ShowDialog()
            ' Dim ee As System.EventArgs 
            FrmMainTranScreen_Shown("", Nothing)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Dim aa As String = objCommonVar.CurrDatabase

    Private Sub FrmMainTranScreen_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        clsDBFuncationality._LastActiveTime = DateTime.Now()
    End Sub


    Private Sub FrmMainTranScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.DesignMode Then
            AddMouseUpEventHandlerToAllControl(Me)
            Dim isTabOrderOn As Boolean = objCommonVar.IsAutoTabOrdering
            If isTabOrderOn Then
                Dim tabOrderPattern As Integer = objCommonVar.CurrentTabOrderPattern
                Dim scheme As TabOrderManager.TabScheme
                If tabOrderPattern = 1 Then
                    scheme = TabOrderManager.TabScheme.AcrossFirst
                ElseIf tabOrderPattern = 2 Then
                    scheme = TabOrderManager.TabScheme.DownFirst
                Else
                    scheme = TabOrderManager.TabScheme.None
                End If
                Dim tom As New TabOrderManager(Me)
                tom.SetTabOrder(scheme)
            End If
            If objCommonVar.AutoSetTabStopForReadOnlyControls = 1 Then
                FindAndSetTabStopFalse(Me)
            End If
            clsDBFuncationality._LastActiveTime = DateTime.Now()
            'Try
            '    FrmMainTranScreen.LastWorkingTime = DateTime.Now()
            '    'Me.Capture = True
            '    'Me.Cursor = Cursors.Default

            '    If clsCommon.myLen(is_Cancel_Allowed) <= 0 Then
            '        is_Cancel_Allowed = objCommonVar.is_Cancel_Allowed
            '    End If

            'Catch ex As Exception

            'End Try
            If Not Me.DesignMode Then
                AddMouseMove(Me)
            End If

            'If Not Me.DesignMode Then
            '    If objCommonVar.AllowDesignAtRunTime Then
            '        Try
            '            Dim objCM As New ControlManager
            '            objCM.RestoreProperties(Me)
            '            objCM.Dispose()
            '            objCM = Nothing
            '        Catch ex As Exception
            '        End Try
            '    End If
            'End If
        End If

    End Sub

    Public strRvalue As String = ""
    Function getNavigatorValue(ByRef formname As XpertERPEngine.FrmMainTranScreen, Optional ByVal contrl As Control = Nothing) As String
        If clsCommon.myLen(strRvalue) > 0 Then
            Return strRvalue
            Exit Function
        End If

        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If ctrl.HasChildren = True Then
                    getNavigatorValue(Me, ctrl)
                End If
                If TypeOf (ctrl) Is common.UserControls.txtNavigator Then
                    Try
                        strRvalue = clsCommon.myCstr(CType(ctrl, common.UserControls.txtNavigator).Value)
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            Next
        Else
            For Each ctrl As Control In contrl.Controls
                If ctrl.HasChildren = True Then
                    getNavigatorValue(Me, ctrl)
                End If
                If TypeOf (ctrl) Is common.UserControls.txtNavigator Then
                    Try
                        strRvalue = clsCommon.myCstr(CType(ctrl, common.UserControls.txtNavigator).Value)
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            Next
        End If
        If clsCommon.myLen(strRvalue) > 0 Then
            Return strRvalue
            Exit Function
        End If
        Return ""
    End Function

    Private Sub FrmMainTranScreen_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown

    End Sub

    Private Sub FrmMainTranScreen_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseEnter
        clsDBFuncationality._LastActiveTime = DateTime.Now()
    End Sub

    Private Sub FrmMainTranScreen_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseMove
        If Not (OldMouseX = e.X AndAlso OldMouseY = e.Y) Then
            clsDBFuncationality._LastActiveTime = DateTime.Now()
        End If
        OldMouseX = e.X
        OldMouseY = e.Y
        'Me.Capture = True
    End Sub
    Public Sub AddSpecialAttributesToFormControl(ByRef formname As XpertERPEngine.FrmMainTranScreen, Optional ByVal contrl As Control = Nothing)

        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If ctrl.HasChildren = True AndAlso Not (TypeOf ctrl Is common.UserControls.txtFinder OrElse TypeOf ctrl Is common.UserControls.txtNavigator) Then
                    AddSpecialAttributesToFormControl(formname, ctrl)
                End If
                If Not (TypeOf ctrl Is RadGroupBox OrElse TypeOf ctrl Is SplitContainer OrElse TypeOf ctrl Is RadPanel OrElse TypeOf ctrl Is Panel OrElse TypeOf ctrl Is GroupBox OrElse TypeOf ctrl Is common.UserControls.MyRadGridView) AndAlso clsCommon.myLen(ctrl.Name) > 0 Then
                    Dim fieldName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from tspl_screen_control_master where programCode in (select Program_code from tspl_program_master where mainFormName='" & formname.Name & "') and controlName='" & ctrl.Name & "'"))
                    If TypeOf ctrl Is MyNumBox Then
                        TryCast(ctrl, MyNumBox).FieldName = fieldName
                    End If
                    If TypeOf ctrl Is common.Controls.MyTextBox Then
                        TryCast(ctrl, common.Controls.MyTextBox).FieldName = fieldName
                    End If
                    ctrl.Tag = fieldName
                End If
            Next
        Else
            For Each ctrl As Control In contrl.Controls
                If ctrl.HasChildren = True AndAlso Not (TypeOf ctrl Is common.UserControls.txtFinder OrElse TypeOf ctrl Is common.UserControls.txtNavigator) Then
                    AddSpecialAttributesToFormControl(formname, ctrl)
                End If
                If Not (TypeOf ctrl Is RadGroupBox OrElse TypeOf ctrl Is SplitContainer OrElse TypeOf ctrl Is RadPanel OrElse TypeOf ctrl Is Panel OrElse TypeOf ctrl Is GroupBox OrElse TypeOf ctrl Is common.UserControls.MyRadGridView) AndAlso clsCommon.myLen(ctrl.Name) > 0 Then
                    Dim fieldName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from tspl_screen_control_master where programCode in (select Program_code from tspl_program_master where mainFormName='" & formname.Name & "') and controlName='" & ctrl.Name & "'"))
                    If TypeOf ctrl Is MyNumBox Then
                        TryCast(ctrl, MyNumBox).FieldName = fieldName
                    End If
                    If TypeOf ctrl Is common.Controls.MyTextBox Then
                        TryCast(ctrl, common.Controls.MyTextBox).FieldName = fieldName
                    End If
                    ctrl.Tag = fieldName
                End If
            Next
        End If
    End Sub

    Public Sub AddMouseUpEventHandlerToAllControl(ByRef formname As XpertERPEngine.FrmMainTranScreen, Optional ByVal contrl As Control = Nothing)

        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If ctrl.HasChildren = True AndAlso Not (TypeOf ctrl Is common.UserControls.txtFinder OrElse TypeOf ctrl Is common.UserControls.txtNavigator) Then
                    AddMouseUpEventHandlerToAllControl(formname, ctrl)
                End If
                'If Not (TypeOf ctrl Is RadGroupBox OrElse TypeOf ctrl Is SplitContainer OrElse TypeOf ctrl Is RadPanel OrElse TypeOf ctrl Is Panel OrElse TypeOf ctrl Is GroupBox OrElse TypeOf ctrl Is common.UserControls.MyRadGridView) AndAlso clsCommon.myLen(ctrl.Name) > 0 Then
                '    AddHandler ctrl.MouseUp, AddressOf FrmMainTranScreen_MouseUp
                'End If
                If TypeOf ctrl Is common.UserControls.MyRadGridView AndAlso clsCommon.myLen(ctrl.Name) > 0 Then
                    AddHandler ctrl.MouseUp, AddressOf FrmMainTranScreen_MouseUp
                End If
            Next
        Else
            For Each ctrl As Control In contrl.Controls
                If ctrl.HasChildren = True AndAlso Not (TypeOf ctrl Is common.UserControls.txtFinder OrElse TypeOf ctrl Is common.UserControls.txtNavigator) Then
                    AddMouseUpEventHandlerToAllControl(formname, ctrl)
                End If
                'If Not (TypeOf ctrl Is RadGroupBox OrElse TypeOf ctrl Is SplitContainer OrElse TypeOf ctrl Is RadPanel OrElse TypeOf ctrl Is Panel OrElse TypeOf ctrl Is GroupBox OrElse TypeOf ctrl Is common.UserControls.MyRadGridView) AndAlso clsCommon.myLen(ctrl.Name) > 0 Then
                '    AddHandler ctrl.MouseUp, AddressOf FrmMainTranScreen_MouseUp
                'End If
                If TypeOf ctrl Is common.UserControls.MyRadGridView AndAlso clsCommon.myLen(ctrl.Name) > 0 Then
                    AddHandler ctrl.MouseUp, AddressOf FrmMainTranScreen_MouseUp
                End If
            Next
        End If
    End Sub
    Public ctrlRightClicked As New Control
    Private Sub FrmMainTranScreen_MouseUp(sender As Object, e As MouseEventArgs)

        If e.Button = Windows.Forms.MouseButtons.Right AndAlso clsCreateAllTables.IsShowMenuOnRightClick Then
            clsCommon.MyMessageBoxShow("Hi")
            Dim ctr As Control = sender 'clsCreateAllTables.FindControlAtCursor(Me)
            ctrlRightClicked = ctr
            If ctr IsNot Nothing Then
                ContextMenuStrip1.Show(ctr, e.Location)
            End If
        End If

    End Sub



    'Private Sub FrmMainTranScreen_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

    'End Sub
    Private Sub FrmMainTranScreen_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Not Me.DesignMode Then
            Try
                If clsCommon.myLen(Form_ID) > 0 Then
                    Dim obj As New frmClientFormLableDetails
                    obj.formcode = Form_ID
                    obj.formnam = Me
                    obj.LoadLableChanged(Me, , True)
                    'obj.Dispose()
                    'If objCommonVar.AutoRestoreGridLayout Then
                    '    FindAndRestoreGridLayout(Me)
                    'End If
                    FindAndSetgridUpDownFalse(Me)
                    'AddMouseMove(Me)

                End If
                If is_Cancel_Allowed = "1" Then
                    frmClientFormLableDetails.HideDeleteButon(Me, Me.Form_ID, Nothing)
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
    End Sub
    '' Anubhooti 09-Sep-2014 Check Transactions For Financial Year--------------------------------------------
    Public Shared Function ValidateTransactionAccToFinYear(ByVal Form_Name As String, ByVal DocDate As String) As Boolean
        'Try
        '    Dim Post_Previousyear As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Post_Previousyear From TSPL_GLSETTING"))
        '    If clsCommon.CompairString(Post_Previousyear, "Y") = CompairStringResult.Equal Then
        '        Dim QryCurrYear As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("  Select COUNT(Is_Current_Year) As Is_Current_Year  From TSPL_Fiscal_Year_Master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' and Is_Current_Year =1"))
        '        Dim Qry As String = "  Select COUNT(Fiscal_Code) As Fiscal_Code  From TSPL_Fiscal_Year_Master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' AND convert(Date, '" + DocDate + "', 103)>= CONVERT(date, Start_Date,103) AND convert(Date, '" + DocDate + "', 103) <= CONVERT(date, End_Date ,103) and Is_Current_Year =1"
        '        Dim DocDateWOTime As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select convert(Date, '" + DocDate + "', 103)"))
        '        Dim Fiscal_Code As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry))
        '        If Fiscal_Code = 0 Then
        '            If common.clsCommon.MyMessageBoxShow("Document date " + DocDateWOTime + " does not exists in current financial year.Do you still want to continue ", Form_Name, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
        '            Else
        '                Return False
        '            End If
        '        End If
        '    End If
        '    Return True
        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        'End Try

        Try
            Dim QryCurrYear As Integer
            QryCurrYear = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(Is_Current_Year) As Is_Current_Year  From TSPL_Fiscal_Year_Master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' and Is_Current_Year =1"))
            Dim Post_Previousyear As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Post_Previousyear From TSPL_GLSETTING"))
            If clsCommon.CompairString(Post_Previousyear, "Y") = CompairStringResult.Equal Then
                If QryCurrYear > 0 Then
                    Dim Qry As String = "  Select COUNT(Fiscal_Code) As Fiscal_Code  From TSPL_Fiscal_Year_Master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' AND convert(Date, '" + DocDate + "', 103)>= CONVERT(date, Start_Date,103) AND convert(Date, '" + DocDate + "', 103) <= CONVERT(date, End_Date ,103) and Is_Current_Year =1"
                    Dim DocDateWOTime As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select convert(Date, '" + DocDate + "', 103)"))
                    Dim Fiscal_Code As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry))
                    If Fiscal_Code = 0 Then
                        If common.clsCommon.MyMessageBoxShow("Document date " + DocDateWOTime + " does not exists in current financial year.Do you still want to continue ", Form_Name, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        Else
                            Return False
                        End If
                    End If
                End If
                'ElseIf clsCommon.CompairString(Post_Previousyear, "N") = CompairStringResult.Equal Then
                '    If QryCurrYear > 0 Then
                '        Dim QrySettOff As String = "  Select COUNT(Fiscal_Code) As Fiscal_Code  From TSPL_Fiscal_Year_Master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' AND convert(Date, '" + DocDate + "', 103)>= CONVERT(date, Start_Date,103) AND convert(Date, '" + DocDate + "', 103) <= CONVERT(date, End_Date ,103) and Is_Current_Year =1"
                '        Dim DocDateWOTimeSettOff As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select convert(Date, '" + DocDate + "', 103)"))
                '        Dim Fiscal_CodeSettOff As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue(QrySettOff))
                '        If Fiscal_CodeSettOff = 0 Then
                '            clsCommon.MyMessageBoxShow("You can not make this entry beacuse document date " + DocDateWOTimeSettOff + " does not lie in current financial year")
                '            Return False
                '        End If
                '    End If
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    '-------------------------------------------------------------------------------

    Private Sub GetControlToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GetControlToolStripMenuItem.Click
        Try
            clsCommon.MyMessageBoxShow("Hi")
            If ctrlRightClicked IsNot Nothing Then
                Dim frm As New frmScreenControlDescriptionMapping
                frm.ctrl = ctrlRightClicked
                frm.formId = Me.Form_ID
                frm.ShowDialog()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetDescriptionForAllControlToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetDescriptionForAllControlToolStripMenuItem.Click
        Dim frm As New frmScreenControlMappingMultiple
        frm.formId = Me.Form_ID
        frm.WindowState = FormWindowState.Maximized
        frm.ShowDialog()
    End Sub

    Private Sub AddForCustomFieldGridToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddForCustomFieldGridToolStripMenuItem.Click
        Try
            If ctrlRightClicked IsNot Nothing Then
                Dim qry As String = " delete from TSPL_SCREEN_Grid_CONTROL_MASTER where ProgramCode='" & Me.Form_ID & "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                Dim gv As common.UserControls.MyRadGridView = TryCast(ctrlRightClicked, common.UserControls.MyRadGridView)
                For i As Integer = 0 To gv.Columns.Count - 1
                    qry = "insert into TSPL_SCREEN_Grid_CONTROL_MASTER(ProgramCode,GridName,ColumnName,ColumnDescription) values('" & Me.Form_ID & "','" & gv.Name & "','" & gv.Columns(i).Name & "','" & gv.Columns(i).HeaderText & "')"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                Next
                clsCommon.MyMessageBoxShow(Me, "Saved Successfully", Me.Text)
                clsCreateAllTables.IsShowMenuOnRightClick = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function btnDelete() As Object
        Throw New NotImplementedException
    End Function

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub SetUserMgmtCommonForScreenwithTelerikButton(ByVal FormID As String, Optional ByVal btnSave As Telerik.WinControls.UI.RadButton = Nothing, Optional ByVal btnDelete As Telerik.WinControls.UI.RadButton = Nothing, Optional ByVal btnPost As Button = Nothing, Optional ByVal btnReverse As Telerik.WinControls.UI.RadButton = Nothing, Optional ByVal btnImport As Telerik.WinControls.UI.RadMenuItem = Nothing, Optional ByVal btnExport As Telerik.WinControls.UI.RadMenuItem = Nothing)
        Me.KeyPreview = True
        'Me.WindowState = FormWindowState.Maximized
        Dim qry As String = ""
        If clsCommon.myLen(FormID) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Form ID not found", Me.Text)
            Me.Close()
            Exit Sub
        End If
        Me.Form_ID = FormID
        qry = " select Parent_Code  from TSPL_PROGRAM_MASTER where program_code in (" & _
              " select Parent_Code from TSPL_PROGRAM_MASTER where program_code='" & Form_ID & "')"


        'strqModule = "WITH PROGRAM_HIERARCHY AS (" & _
        '            " SELECT TSPL_PROGRAM_MASTER.Program_Code, TSPL_PROGRAM_MASTER.Parent_Code ,1 AS PROGRAM_LEVEL,TSPL_PROGRAM_MASTER.Type" & _
        '            " FROM TSPL_PROGRAM_MASTER " & _
        '            " UNION ALL " & _
        '            " SELECT PROGRAM_HIERARCHY.Program_Code,TSPL_PROGRAM_MASTER.Parent_Code,(PROGRAM_LEVEL + 1) AS PROGRAM_LEVEL,TSPL_PROGRAM_MASTER.Type" & _
        '            " FROM PROGRAM_HIERARCHY JOIN TSPL_PROGRAM_MASTER ON PROGRAM_HIERARCHY.Parent_Code = TSPL_PROGRAM_MASTER.Program_Code" & _
        '            " )" & _
        '            " SELECT  DISTINCT PROGRAM_HIERARCHY.Parent_Code " & _
        '            " FROM PROGRAM_HIERARCHY " & _
        '            " WHERE PROGRAM_HIERARCHY.Program_Code='" & FormID & "' " & _
        '            " AND PROGRAM_HIERARCHY.Type='SM' AND PROGRAM_HIERARCHY.PROGRAM_LEVEL=2;"

        Me.Module_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
            isReadFlag = True
            isModifyFlag = True
            isDeleteFlag = True
            isPostFlag = True
            isReverse = True
            isExport = True
            isCancel_Flag = True
            isCancel_Flag_After_Posting = True
            btnSave.Visible = True
            btnDelete.Visible = True
            btnPost.Visible = True
            If btnSave.Visible Then
                btnReverse.Enabled = True
                btnImport.Enabled = True
                btnExport.Enabled = True
            End If
            isAmendmentFlag = True
        Else
            qry = "select Read_Flag,Modify_Flag,Delete_Flag,Authorized_Flag, Reverse_Flag, Export_Flag,cancel_Flag,cancel_Flag_After_Posting,isnull(TSPL_GROUP_PROGRAM_MAPPING.is_Amendment,0) as is_Amendment from TSPL_GROUP_PROGRAM_MAPPING where Program_Code='" + FormID + "' and Group_Code in (select Group_Code  from TSPL_USER_GROUP_MAPPING where User_Code='" + objCommonVar.CurrentUserCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If isReadFlag = False Then
                        Throw New Exception("Permission Denied")
                        isReadFlag = IIf(clsCommon.myCdbl(dr("Read_Flag")) = 1, True, False)
                    End If

                    If isModifyFlag = False Then
                        isModifyFlag = IIf(clsCommon.myCdbl(dr("Modify_Flag")) = 1, True, False)
                    End If
                    If isDeleteFlag = False Then
                        isDeleteFlag = IIf(clsCommon.myCdbl(dr("Delete_Flag")) = 1, True, False)
                    End If
                    If isPostFlag = False Then
                        isPostFlag = IIf(clsCommon.myCdbl(dr("Authorized_Flag")) = 1, True, False)
                    End If
                    If isReverse = False Then
                        isReverse = IIf(clsCommon.myCdbl(dr("Reverse_Flag")) = 1, True, False)
                    End If
                    If isExport = False Then
                        isExport = IIf(clsCommon.myCdbl(dr("Export_Flag")) = 1, True, False)
                    End If
                    If isCancel_Flag = False Then
                        isCancel_Flag = IIf(clsCommon.myCdbl(dr("Cancel_Flag")) = 1, True, False)
                    End If
                    If isCancel_Flag_After_Posting = False Then
                        isCancel_Flag_After_Posting = IIf(clsCommon.myCdbl(dr("Cancel_Flag_After_Posting")) = 1, True, False)
                    End If

                    btnSave.Visible = isReadFlag
                    btnDelete.Visible = isModifyFlag
                    btnPost.Visible = isPostFlag
                    If btnSave.Visible Then
                        btnReverse.Enabled = isReverse
                        btnImport.Enabled = isExport
                        btnExport.Enabled = isExport
                    End If
                    If isAmendmentFlag = False Then
                        isAmendmentFlag = IIf(clsCommon.myCdbl(dr("is_Amendment")) = 1, True, False)
                    End If
                Next
                'isReadFlag = IIf(clsCommon.myCdbl(dt.Rows(0)("Read_Flag")) = 1, True, False)
                'isModifyFlag = IIf(clsCommon.myCdbl(dt.Rows(0)("Modify_Flag")) = 1, True, False)
                'isDeleteFlag = IIf(clsCommon.myCdbl(dt.Rows(0)("Delete_Flag")) = 1, True, False)
                'isPostFlag = IIf(clsCommon.myCdbl(dt.Rows(0)("Authorized_Flag")) = 1, True, False)
                'isReverse = IIf(clsCommon.myCdbl(dt.Rows(0)("Reverse_Flag")) = 1, True, False)
                'isExport = IIf(clsCommon.myCdbl(dt.Rows(0)("Export_Flag")) = 1, True, False)

            End If
        End If

        qry = "select 1 from TSPL_CUSTOM_FIELD_MAPPING where Program_Code='" + FormID + "' and Is_For_Detail_Level='0' "
        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtNew IsNot Nothing AndAlso dtNew.Rows.Count Then
            customFieldTabProperty = ElementVisibility.Visible
        End If
        ArrDetailFields = clsCustomFieldMapping.GetData(FormID, "D")



    End Sub

    '== KUNAL > 5-SEP- 2016 ======
    Dim IsSettingOn As Boolean = False
    Public Function AllowFutureDateTransaction(ByVal docDate As Date, ByVal trans As SqlClient.SqlTransaction) As Boolean
        IsSettingOn = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowFutureDateTransaction, clsFixedParameterType.AllowFutureDateTransaction, trans)) = 1, True, False)
        If IsSettingOn = False Then
            If docDate > clsCommon.GETSERVERDATE(trans) Then
                clsCommon.MyMessageBoxShow(Me, "Cannot allow future date -  " & docDate)
                Return False
            End If
        End If
        '===================added By preeti Gupta [01/02/2017]=================
        If AllowBackDateEntry(docDate, trans) = False Then
            Return False
        End If
        '======================================================================
        Return True
    End Function

    'done by stuti on 17/10/2016 against ticket no - BM00000009608
    Dim BackDateEntry As Integer = 0
    Public Function AllowBackDateEntry(ByVal docDate As Date, ByVal trans As SqlClient.SqlTransaction) As Boolean
        BackDateEntry = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBackDateEntry, clsFixedParameterType.AllowBackDateEntry, trans))

        '================Added by preeti Gupta============================

        '=================================================================


        If clsCommon.GetPrintDate(docDate, "yyyy-MM-dd") < clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans).AddDays(-BackDateEntry), "yyyy-MM-dd") Then

            Dim frm As New FrmPWD(Nothing)
            frm.Text = "Please enter password for back date entry"
            frm.strType = "BackDateEntryPwd"
            frm.strCode = "BackDateEntryPwd"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                Return True
            Else
                clsCommon.MyMessageBoxShow(Me, "Back date entry allowed to authorised user only.")
                Return False
            End If

        End If
        Return True
    End Function

    Public Function AllowAmendmentWithPasssword(ByVal FormId As String, ByVal trans As SqlClient.SqlTransaction) As Boolean
        Dim qry As String = Nothing
        Dim val As String = Nothing
        Dim dt As DataTable = New DataTable()
        qry = "select is_Amendment from TSPL_GROUP_PROGRAM_MAPPING left outer join TSPL_USER_GROUP_MAPPING on TSPL_GROUP_PROGRAM_MAPPING.Group_Code=TSPL_USER_GROUP_MAPPING.Group_Code where User_Code='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "' and Program_Code='" + clsCommon.myCstr(FormId) + "' "
        val = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        If clsCommon.myLen(val) > 0 AndAlso clsCommon.CompairString(val, "1") = CompairStringResult.Equal Then
            Dim frm As New FrmPWD(trans)
            frm.Text = "Please enter password for amendment"
            frm.strType = ""
            frm.strCode = ""
            frm.FormId = FormId
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                Return True
            Else
                Return False
            End If
        End If
        Return False
    End Function


    Private Shared Function ColumnIndexToColumnLetter(ByVal colIndex As Integer) As String
        Dim div As Integer = colIndex
        Dim colLetter As String = [String].Empty
        Dim [mod] As Integer = 0
        While div > 0
            [mod] = (div - 1) Mod 26
            colLetter = (Convert.ToChar(65 + [mod])).ToString & colLetter
            div = CInt((div - [mod]) / 26)
        End While
        Return colLetter
    End Function


    Public Sub ShowJE(ByVal Form_ID As String, ByVal strCode As String)
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Document Found on Current Screen", Me.Text)
                Exit Sub
            End If
            ' clsCommon.CompairString(Form_ID, clsUserMgtCode.MilkMPPayment) = CompairStringResult.Equal OrElse
            If clsCommon.CompairString(Form_ID, clsUserMgtCode.frmPaymentProcessFarmer) = CompairStringResult.Equal OrElse
                clsCommon.CompairString(Form_ID, clsUserMgtCode.frmPaymentProcess) = CompairStringResult.Equal OrElse
                clsCommon.CompairString(Form_ID, clsUserMgtCode.MilkVSPPayment) = CompairStringResult.Equal OrElse
                clsCommon.CompairString(Form_ID, clsUserMgtCode.MPBillGeneration) = CompairStringResult.Equal OrElse
                clsCommon.CompairString(Form_ID, clsUserMgtCode.MilkVSPIssuePayment) = CompairStringResult.Equal Then
                Application.OpenForms("MDI").Controls("__txtDocNo").Text = Form_ID + "#$#" + strCode
                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmGLTransReport
                strRvalue = ""
                Exit Sub
            End If

            Dim qry As String = " select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" & strCode & "' "

            If clsCommon.CompairString(Form_ID, clsUserMgtCode.ReceiptAdjustmentEntry) = CompairStringResult.Equal Then
                qry += " And TSPL_JOURNAL_MASTER.Source_Code ='AR-AD' "
            ElseIf clsCommon.CompairString(Form_ID, clsUserMgtCode.PaymentAdjustmentEntry) = CompairStringResult.Equal Then
                qry += " And TSPL_JOURNAL_MASTER.Source_Code ='AP-AD' "
            End If

            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Journal Entry Found For Current Document", Me.Text)
                Exit Sub
            Else
                Application.OpenForms("MDI").Controls("__txtDocNo").Text = strCode
                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.journalEntry
                strRvalue = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

End Class

'--------------shivani tyagi000
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Public Class FrmMccRecurringPayablePendingInvoiceList
    Dim isInsideLoadData As Boolean = False
    Const COL_Type As String = "COL_VSP_Code"
    Const COL_Name As String = "COL_VSP_Name"
    Const COl_Mail_Id As String = "COl_Loc_Code"
    Const COl_Mobile_No As String = "COl_Loc_Name"
    Const ColIsSelect As String = "ColIsSelect"
    Const Col_Last_Invoice As String = "Col_Last_Invoice"
    'Const Col_Last_Invoice As String = "Col_Last_Invoice"
    Private isNewEntry As Boolean = False
    Dim is_Send_SMS As Boolean
    Dim Send_SMS_Time As String
    Dim qry As String


    Public Sub LoadBlankGrid()

        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim SelectCoL As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        SelectCoL.FormatString = ""
        SelectCoL.HeaderText = "Select"
        SelectCoL.Name = ColIsSelect
        SelectCoL.Width = 80
        SelectCoL.IsVisible = True
        ' SelectCoL.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(SelectCoL)

        Dim Type_COL As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        Type_COL.FormatString = ""
        Type_COL.HeaderText = "Type"
        Type_COL.Name = COL_Type
        Type_COL.Width = 150
        'Type_COL.DataSource =
        LoadType(Type_COL)
        Type_COL.IsVisible = True
        'Type_COL.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(Type_COL)

        Dim Name_COL As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Name_COL.FormatString = ""
        Name_COL.HeaderText = "Name"
        Name_COL.Name = COL_Name
        Name_COL.HeaderImage = Global.ERP.My.Resources.Resources.search4
        Name_COL.TextImageRelation = TextImageRelation.TextBeforeImage
        Name_COL.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Name_COL.Width = 300
        Name_COL.IsVisible = True
        Name_COL.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(Name_COL)

        Dim Mail_Id_COL As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Mail_Id_COL.FormatString = ""
        Mail_Id_COL.HeaderText = "Mail Id"
        Mail_Id_COL.Name = COl_Mail_Id
        Mail_Id_COL.Width = 300
        Mail_Id_COL.IsVisible = True
        Mail_Id_COL.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(Mail_Id_COL)

        Dim Mobile_No_COL As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Mobile_No_COL.FormatString = ""
        Mobile_No_COL.HeaderText = "Mobile No"
        Mobile_No_COL.Name = COl_Mobile_No
        Mobile_No_COL.Width = 300
        Mobile_No_COL.IsVisible = True
        Mobile_No_COL.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(Mobile_No_COL)


        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = True
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = True
        gv1.EnableFiltering = True
        gv1.EnableAlternatingRowColor = True
        gv1.AutoSizeRows = False
        gv1.AllowRowResize = True
        gv1.VerticalScrollState = ScrollState.AlwaysShow
        gv1.HorizontalScrollState = ScrollState.AlwaysShow
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.ShowFilteringRow = True

    End Sub

    Private Sub LoadType(ByVal cmb As GridViewComboBoxColumn)
        Dim Dt As New DataTable
        Dt.Columns.Add("Code")
        Dt.Columns.Add("Name")

        Dim dr As DataRow = Dt.NewRow
        dr("Code") = "O"
        dr("Name") = "Other"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow
        dr("Code") = "V"
        dr("Name") = "Vendor"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow
        dr("Code") = "C"
        dr("Name") = "Customer"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow
        dr("Code") = "E"
        dr("Name") = "Employee"
        Dt.Rows.Add(dr)

        cmb.DataSource = Dt
        cmb.ValueMember = "Code"
        cmb.DisplayMember = "Name"
    End Sub

    Sub AddNew()
        LoadBlankGrid()
        Me.gv1.Rows.AddNew()
        'cboModuleName.ReadOnly = True
    End Sub
    Private Sub FrmMccRecurringPayablePendingInvoiceList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub
    Sub SaveData()
        Try
            For Each row As GridViewRowInfo In gv1.Rows

            Next
            Dim Arr As New List(Of ClsMCCSMSSetting)
            Dim obj As New ClsMCCSMSSetting
            For Each grow As GridViewRowInfo In gv1.Rows
                obj = New ClsMCCSMSSetting
                'obj.Program_Code = clsCommon.myCstr(cboModuleName.SelectedValue)
                obj._MailId = clsCommon.myCstr(grow.Cells(COl_Mail_Id).Value)
                obj._MobileNo = clsCommon.myCstr(grow.Cells(COl_Mobile_No).Value)
                obj._Name = clsCommon.myCstr(grow.Cells(COL_Name).Value)
                obj._Type = clsCommon.myCstr(grow.Cells(COL_Type).Value)
                'obj.Loc_Code = clsCommon.myCstr(fndMCCCode.Value)
                Arr.Add(obj)
            Next

            'Dim qry As String = clsDBFuncationality.getSingleValue("select count(Program_Code)from TSPL_MCC_MAIL_SMS_Setting where Program_Code='" + obj.Program_Code + "'")
            'If qry = 0 Then
            '    isNewEntry = True
            'Else
            '    isNewEntry = False
            'End If

            If (ClsMCCSMSSetting.SaveData(Arr)) Then
                clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub DeleteData()
        For Each grow As GridViewRowInfo In gv1.Rows
            Dim qry As String = "delete from TSPL_MCC_MAIL_SMS_Setting where program_code='" + clsCommon.myCstr("") + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Next
        clsCommon.MyMessageBoxShow(Me, "delete successfully", Me.Text)

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        DeleteData()
    End Sub
    Sub OpenList(ByVal isButtonClick As Boolean, ByVal Name_type As String)
        Dim sQuery As String = String.Empty
        Dim dr As DataRow
        If Name_type = "V" Then
            sQuery = "select * from tspl_vendor_master"
            dr = clsCommon.ShowSelectFormForRow("V_Query", sQuery)
            If Not IsNothing(dr) Then
                gv1.CurrentRow.Cells(COL_Name).Value = clsCommon.myCstr(dr("Vendor_Name"))
                gv1.CurrentRow.Tag = clsCommon.myCstr(dr("Vendor_Code"))
                gv1.CurrentRow.Cells(COl_Mobile_No).Value = clsCommon.myCstr(dr("Phone1"))
                gv1.CurrentRow.Cells(COl_Mail_Id).Value = clsCommon.myCstr(dr("Contact_Person_Email"))
            End If
        ElseIf Name_type = "C" Then
            sQuery = "select * from tspl_customer_master" 'clsCustomerMaster.getFinder("", "", isButtonClick)
            dr = clsCommon.ShowSelectFormForRow("C_Query", sQuery)
            If Not IsNothing(dr) Then
                gv1.CurrentRow.Cells(COL_Name).Value = clsCommon.myCstr(dr("Customer_Name"))
                gv1.CurrentRow.Tag = clsCommon.myCstr(dr("Cust_Code"))
                gv1.CurrentRow.Cells(COl_Mobile_No).Value = clsCommon.myCstr(dr("Phone1"))
                gv1.CurrentRow.Cells(COl_Mail_Id).Value = clsCommon.myCstr(dr("Contact_Person_Email"))
            End If
        ElseIf Name_type = "E" Then
            sQuery = "select * from tspl_EMployee_master" 'clsEmployeeMaster.getFinder("", "", isButtonClick)
            dr = clsCommon.ShowSelectFormForRow("E_Query", sQuery)
            If Not IsNothing(dr) Then
                gv1.CurrentRow.Cells(COL_Name).Value = clsCommon.myCstr(dr("Emp_Name"))
                gv1.CurrentRow.Tag = clsCommon.myCstr(dr("EMP_Code"))
                gv1.CurrentRow.Cells(COl_Mobile_No).Value = clsCommon.myCstr(dr("Phone"))
                gv1.CurrentRow.Cells(COl_Mail_Id).Value = clsCommon.myCstr(dr("Email_Id"))
            End If
        End If
    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is gv1.Columns(COL_Name) Then
                    isInsideLoadData = True
                    gv1.CurrentRow.Cells(ColIsSelect).Value = True
                    OpenList(True, clsCommon.myCstr(gv1.CurrentRow.Cells(COL_Type).Value))
                    isInsideLoadData = False
                End If
                If e.Column Is gv1.Columns(COl_Mail_Id) Then
                    Dim check As Match = Regex.Match(clsCommon.myCstr(gv1.CurrentRow.Cells(COl_Mail_Id).Value), "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
                    If check.Success Then
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Please Enter the proper format of e-mail address", Me.Text)
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        AddNew()
    End Sub

    Private Sub cboModuleName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs)
        If isInsideLoadData = False Then
            Loaddata()
        End If
    End Sub

    Sub Loaddata()
        Try
            LoadBlankGrid()
            Dim obj As List(Of ClsMCCSMSSetting) = ClsMCCSMSSetting.GetData("")
            If obj.Count > 0 Then
                ' LoadBlankGrid()
                For Each objl As ClsMCCSMSSetting In obj
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COL_Name).Value = objl._Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COL_Type).Value = objl._Type
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COl_Mail_Id).Value = objl._MailId
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COl_Mobile_No).Value = objl._MobileNo
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub gv1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyUp
        'Try
        '    If Not IsNumeric(e.KeyCode) And Not gv1.CurrentColumn = gv1.Columns(COl_Mobile_No) Then
        '        e.Handled = True
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.ToString)
        'End Try
    End Sub

    '#Region "Mail SMS Setting"

    '    Public Sub SendMail(ByVal Doc_Code As String, ByVal Doc_Date As String, ByVal Mcc_Name As String, ByVal Frm_id As String, ByVal Shift As String)
    '        '=========Rohit 31 Jan,2015=======
    '        is_Send_SMS = IIf(clsFixedParameter.GetData(clsFixedParameterType.Is_Send_Sms, clsFixedParameterCode.MilkSetting, Nothing) = 0, False, True)
    '        If is_Send_SMS = True Then
    '            Send_SMS_Time = clsFixedParameter.GetData(clsFixedParameterType.Send_Sms_Time, clsFixedParameterCode.MilkSetting, Nothing)
    '            qry = "select Max(coalesce(is_sms_sended,'') from tspl_milk_receipt_Head where convert(date,doc_date,103)=convert(date,'" & Doc_Date & "',103)"
    '            Dim is_sms_sended As String = clsDBFuncationality.getSingleValue(qry)
    '            If is_sms_sended = "1" Then
    '                Exit Sub
    '            End If
    '        Else
    '            Exit Sub
    '        End If

    '        '=====================================
    '        Dim lstUsers As New List(Of String)
    '        Dim sQuery As String = "select * from TSPL_MCC_MAIL_SMS_Setting where program_code='" & Frm_id & "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
    '        For Each row As DataRow In dt.Rows
    '            lstUsers.Add(clsCommon.myCstr(row.Item("_Name")))
    '        Next
    '        If dt.Rows.Count > 0 Then
    '            SendSMSandEmail(Doc_Code, Doc_Date, Mcc_Name, Frm_id, lstUsers, False, shift)
    '        Else
    '            clsCommon.MyMessageBoxShow("No Person Found to Send Mail.")
    '        End If
    '    End Sub

    '    Private Sub SendSMSandEmail(ByVal Doc_Code As String, ByVal Doc_Date As String, ByVal Mcc_Name As String, ByVal Frm_id As String, ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean, ByVal Shift As String)
    '        Try
    '            Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(Frm_id)

    '            If obj Is Nothing Then
    '                clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '                Return
    '            End If
    '            If clsCommon.myLen(obj.mailsubjct) <= 0 Then
    '                clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '                Return
    '            End If

    '            Dim strContactPerson As String = ""
    '            Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.DOC_NO, Doc_Code)
    '            strSubject = strSubject.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy"))

    '            Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.DOC_NO, Doc_Code)
    '            strbody = strbody.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy"))
    '            'strbody = strbody.Replace(clsEmailSMSConstants.Mcc_Code, Mcc_code)
    '            strbody = strbody.Replace(clsEmailSMSConstants.Mcc_Name, Mcc_Name)
    '            strbody = strbody.Replace(clsEmailSMSConstants.Shift, shift)
    '            'strbody = strbody.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)
    '            'strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
    '            strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, Frm_id)

    '            ''------------------------code for attchament-------------------------------------
    '            'Dim strRptPath As String = ""
    '            'If obj.atchmnt = "Y" Then
    '            '    atchqry = GetMailPrintPreview(txtDocNo.Value)
    '            '    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(atchqry)
    '            '    If dt1.Rows.Count > 0 Then
    '            '        If clsCommon.CompairString(clsCommon.myCstr(dt1.Rows(0)("PurchaseOrder_Type")), "J") = CompairStringResult.Equal Then
    '            '            strRptPath = PurchaseOrderViewer.funreport1(dt1, "WO-G", "Work Order")
    '            '        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt1.Rows(0)("PurchaseOrder_Type")), "L") = CompairStringResult.Equal Then
    '            '            strRptPath = PurchaseOrderViewer.funreport1(dt1, "PO-G", "Purchase Order")
    '            '        Else
    '            '            Throw New Exception("Not a valid Po Type")
    '            '            Return
    '            '        End If
    '            '    End If
    '            'End If
    '            '---------------------------------------------------------------------------


    '            For Each strUser As String In lstUsers
    '                'lstUsers.Add(dr("User_Code").ToString())
    '                Dim lstReceiptents As New List(Of String)
    '                Dim qry As String = ""
    '                Dim emailId As String = ""
    '                If isSendForApproval Then
    '                    strContactPerson = strUser
    '                    qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
    '                    emailId = clsDBFuncationality.getSingleValue(qry)
    '                Else
    '                    strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_VENDOR_MASTER where Vendor_code ='" & strUser & "' ")
    '                    emailId = clsDBFuncationality.getSingleValue("select _Emailid from TSPL_MCC_MAIL_SMS_Setting where _name ='" & strUser & "' ")
    '                End If

    '                strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
    '                lstReceiptents.Add(emailId)

    '                Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

    '                clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, "")
    '            Next
    '            'If clsCommon.CompairString(MDI.IsMailSend, "NO") = CompairStringResult.Equal Then
    '            '    clsCommon.MyMessageBoxShow("E-Mail Not Send", Me.Text)
    '            'Else
    '            '    clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
    '            'End If


    '            If Not clsSMSAtPost_Purchase.SMSATPOST_PUR() Then
    '                SMSSENDONLY(Doc_Code, Doc_Date, Mcc_Name, lstUsers, Frm_id, Shift)
    '            End If
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub SMSSENDONLY(ByVal Doc_Code As String, ByVal Doc_Date As String, ByVal Mcc_Name As String, ByVal User_Code As List(Of String), ByVal frm_id As String, ByVal Shift As String)
    '        Try
    '            Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(frm_id)

    '            If obj Is Nothing Then
    '                clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '                Return
    '            End If


    '            If clsCommon.myLen(obj.smsbody) <= 0 Then
    '                Return
    '            End If
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select state_code,tspl_milk_srn_Head.mcc_code,Mcc_Name,convert(date,doc_date,103) as doc_date," _
    '                & " cast(round(sum(Qty),2,2) as float) as Qty,tspl_mcc_Uom_Detail.UOM_Code from tspl_milk_srn_Detail inner join tspl_milk_srn_Head on " _
    '                & " tspl_milk_srn_Detail.doc_code=tspl_milk_srn_Head.doc_code left join tspl_mcc_Uom_Detail on tspl_mcc_Uom_Detail.mcc_code=tspl_milk_srn_Head.mcc_code " _
    '                & " and stocking_unit='Y' left join tspl_mcc_Master on tspl_mcc_Master.mcc_cODE=tspl_milk_srn_Head.mcc_code where  convert(date,doc_date,103)=convert(date,'" & Doc_Date & "',103) group by tspl_milk_srn_Head.mcc_code," _
    '                & " convert(date,doc_date,103),Mcc_Name,tspl_mcc_Uom_Detail.Uom_Code,state_code order by tspl_milk_srn_Head.mcc_code,convert(date,doc_date,103)")
    '            Dim strMes As String = obj.smsbody
    '            If strMes.Contains(clsEmailSMSConstants.Doc_Code) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.Doc_Code, Doc_Code)
    '            End If
    '            If strMes.Contains(clsEmailSMSConstants.DOC_Date) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy"))
    '            End If
    '            If strMes.Contains(clsEmailSMSConstants.Total_collection) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.Total_collection, Math.Round(clsCommon.myCdbl(dt.Compute("Sum(Qty)", "")), 2))
    '            End If
    '            If strMes.Contains(clsEmailSMSConstants.UOM) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.UOM, "LTR")
    '            End If

    '            If strMes.Contains(clsEmailSMSConstants.State_Name) Then
    '                strMes = strMes & Environment.NewLine
    '                Dim dv As DataView = New DataView(dt)
    '                For Each row As DataRow In dv.ToTable(True, "State_Code").Rows
    '                    strMes = strMes & Environment.NewLine & row.Item("State_Code") & "   :  " & Math.Round(clsCommon.myCdbl(dt.Compute("Sum(Qty)", "State_Code='" & row.Item("State_Code") & "'")), 2)
    '                Next
    '                strMes = strMes.Replace(clsEmailSMSConstants.State_Name, "")
    '                ' strMes = strMes.Replace(clsEmailSMSConstants.Mcc_Name, Mcc_Name)
    '            End If
    '            If strMes.Contains(clsEmailSMSConstants.Mcc_Name) Then
    '                strMes = strMes & Environment.NewLine
    '                For Each row As DataRow In dt.Rows
    '                    strMes = strMes & Environment.NewLine & row.Item("Mcc_Name") & "   :  " & clsCommon.myCdbl(dt.Compute("Sum(Qty)", "Mcc_Code='" & row.Item("Mcc_Code") & "'"))
    '                Next
    '                strMes = strMes.Replace(clsEmailSMSConstants.Mcc_Name, "")
    '                ' strMes = strMes.Replace(clsEmailSMSConstants.Mcc_Name, Mcc_Name)
    '            End If
    '            If strMes.Contains(clsEmailSMSConstants.Shift) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.CustomerNo, Shift)
    '            End If

    '            For Each Str As String In User_Code
    '                Dim StrPhone_No As String = clsDBFuncationality.getSingleValue("select _MobileNo from TSPL_MCC_MAIL_SMS_Setting where _name='" & Str & "'")
    '                clsSMSSend.SendSMS(frm_id, strMes, StrPhone_No)
    '                qry = "update tspl_milk_receipt_Head set is_sms_sended='1' where convert(date,doc_date,103)=convert('" & Doc_Date & "',103)"
    '                clsDBFuncationality.ExecuteNonQuery(qry)
    '            Next
    '            clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    End Sub
    '#End Region
    '------------------------------------------------------------------------

    Private Sub fndMCCCode_Validating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
        'Dim sQuery As String = "select Location_Category from tspl_location_master where Location_Code='" & fndMCCCode.Value & "'"
        ''If clsDBFuncationality.getSingleValue(sQuery) = "HO" Then
        'Dim qry As String = "select Location_Code as Location,Location_Desc as Description  from TSPL_LOCATION_MASTER "
        ' fndMCCCode.Value = clsCommon.ShowSelectForm("LocatMast", qry, "Location", "  upper(location_category)='MCC' ", fndMCCCode.Value, "Location_Code", isButtonClicked)
        'fndMCCCode.Value = clsLocation.getFinder(" is_section='N' and is_Sub_location='N' and Rejected_Type='N' and GIT_Type<>'Y'", "Location_code", isButtonClicked)
        'Me.LblMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_desc from TSPL_Location_MASTER where Location_Code='" + fndMCCCode.Value + "' "))
    End Sub

End Class

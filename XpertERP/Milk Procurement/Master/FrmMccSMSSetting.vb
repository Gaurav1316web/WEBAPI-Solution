'--------------shivani tyagi
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
Imports XpertERPEngine
Public Class FrmMccSMSSetting
    Inherits FrmMainTranScreen
    Dim isInsideLoadData As Boolean = False
    Const COL_Type As String = "COL_Type"
    Const COL_Name As String = "COL_Name"
    Const COL_Name_VSP As String = "COL_Name_VSP"
    Const COl_Mail_Id As String = "COl_Mail_Id"
    Const COl_Mobile_No As String = "COl_Mobile_Id"
    Const ColIsSelect As String = "ColIsSelect"
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
        Name_COL.HeaderText = "Code"
        Name_COL.Name = COL_Name
        Name_COL.HeaderImage = Global.ERP.My.Resources.Resources.search4
        Name_COL.TextImageRelation = TextImageRelation.TextBeforeImage
        Name_COL.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Name_COL.Width = 300
        Name_COL.IsVisible = True
        Name_COL.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(Name_COL)

        Dim Name_VSP_COL As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Name_VSP_COL.FormatString = ""
        Name_VSP_COL.HeaderText = "Name"
        Name_VSP_COL.Name = COL_Name_VSP
        Name_VSP_COL.HeaderImage = Global.ERP.My.Resources.Resources.search4
        Name_VSP_COL.TextImageRelation = TextImageRelation.TextBeforeImage
        Name_VSP_COL.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Name_VSP_COL.Width = 300
        Name_VSP_COL.IsVisible = True
        Name_VSP_COL.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(Name_VSP_COL)

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

    Sub LoadProgramCode()
        LoadBlankGrid()
        ' Dim moduleName As String = cboModuleName.SelectedValue
        Dim qry As String = "select TSPL_PROGRAM_MASTER.Program_Code,Program_Name from TSPL_PROGRAM_MASTER   where Program_Code in ('M-PURINVOICE','M-Shift_End','M-SRN','M-PURINV-B','M-SRN-B','Pay-Pro','PYMT-NEW')"
        clsDBFuncationality.ExecuteNonQuery(qry)
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        isInsideLoadData = True
        cboModuleName.DataSource = dt1
        isInsideLoadData = False
        cboModuleName.ValueMember = "Program_Code"
        cboModuleName.DisplayMember = "Program_Name"
        'If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
        '    gv1.DataSource = Nothing
        '    gv1.Columns.Clear()
        '    gv1.Rows.Clear()
        '    gv1.DataSource = dt1
        'Else
        '    clsCommon.MyMessageBoxShow("No Data found to Display")
        'End If
        gv1.DataSource = ClsMCCSMSSetting.GetData(cboModuleName.SelectedValue)


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
        cboModuleName.SelectedIndex = -1
        fndMCCCode.Value = Nothing
        lblMCCCode.Text = Nothing
        'cboModuleName.ReadOnly = True
    End Sub
    Private Sub FrmMccSMSSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadProgramCode()
        'LoadBlankGrid()

        'Me.gv1.Rows.AddNew()
        cboModuleName.Text = ""
        'cboModuleName.ReadOnly = True
    End Sub
    Sub SaveData()
        Try
            For Each row As GridViewRowInfo In gv1.Rows

            Next
            Dim Arr As New List(Of ClsMCCSMSSetting)
            Dim obj As New ClsMCCSMSSetting
            For Each grow As GridViewRowInfo In gv1.Rows
                obj = New ClsMCCSMSSetting
                obj.Program_Code = clsCommon.myCstr(cboModuleName.SelectedValue)
                obj._MailId = clsCommon.myCstr(grow.Cells(COl_Mail_Id).Value)
                obj._MobileNo = clsCommon.myCstr(grow.Cells(COl_Mobile_No).Value)
                obj._Name = clsCommon.myCstr(grow.Cells(COL_Name).Value)
                obj._Type = clsCommon.myCstr(grow.Cells(COL_Type).Value)
                obj.Loc_Code = clsCommon.myCstr(fndMCCCode.Value)
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
            Dim qry As String = "delete from TSPL_MCC_MAIL_SMS_Setting where program_code='" + clsCommon.myCstr(cboModuleName.SelectedValue) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Next
        clsCommon.MyMessageBoxShow(Me, "delete successfully", Me.Text)

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub OpenList(ByVal isButtonClick As Boolean, ByVal Name_type As String)
        Dim sQuery As String = String.Empty
        Dim dr As DataRow
        If Name_type = "V" Then
            sQuery = "select * from tspl_vendor_master"
            dr = clsCommon.ShowSelectFormForRow("V_Query", sQuery)
            If Not IsNothing(dr) Then
                gv1.CurrentRow.Cells(COL_Name).Value = clsCommon.myCstr(dr("Vendor_Code"))
                gv1.CurrentRow.Cells(COL_Name_VSP).Value = clsCommon.myCstr(dr("Vendor_Name"))
                ' gv1.CurrentRow.Tag = clsCommon.myCstr(dr("Vendor_Code"))
                gv1.CurrentRow.Cells(COl_Mobile_No).Value = clsCommon.myCstr(dr("Phone1"))
                gv1.CurrentRow.Cells(COl_Mail_Id).Value = clsCommon.myCstr(dr("Contact_Person_Email"))
            End If
        ElseIf Name_type = "C" Then
            sQuery = "select * from tspl_customer_master" 'clsCustomerMaster.getFinder("", "", isButtonClick)
            dr = clsCommon.ShowSelectFormForRow("C_Query", sQuery)
            If Not IsNothing(dr) Then
                gv1.CurrentRow.Cells(COL_Name).Value = clsCommon.myCstr(dr("Cust_Code"))
                gv1.CurrentRow.Cells(COL_Name_VSP).Value = clsCommon.myCstr(dr("Customer_Name"))
                ' gv1.CurrentRow.Tag = clsCommon.myCstr(dr("Cust_Code"))
                gv1.CurrentRow.Cells(COl_Mobile_No).Value = clsCommon.myCstr(dr("Phone1"))
                gv1.CurrentRow.Cells(COl_Mail_Id).Value = clsCommon.myCstr(dr("Contact_Person_Email"))
            End If
        ElseIf Name_type = "E" Then
            sQuery = "select * from tspl_EMployee_master" 'clsEmployeeMaster.getFinder("", "", isButtonClick)
            dr = clsCommon.ShowSelectFormForRow("E_Query", sQuery)
            If Not IsNothing(dr) Then
                gv1.CurrentRow.Cells(COL_Name).Value = clsCommon.myCstr(dr("EMP_Code"))
                gv1.CurrentRow.Cells(COL_Name_VSP).Value = clsCommon.myCstr(dr("Emp_Name"))
                ' gv1.CurrentRow.Tag = clsCommon.myCstr(dr("EMP_Code"))
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

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub cboModuleName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboModuleName.SelectedIndexChanged
        If isInsideLoadData = False Then
            Loaddata()
        End If
    End Sub

    Sub Loaddata()
        Try
            LoadBlankGrid()
            Dim obj As List(Of ClsMCCSMSSetting) = ClsMCCSMSSetting.GetData(cboModuleName.SelectedValue)
            If obj.Count > 0 Then
                ' LoadBlankGrid()
                For Each objl As ClsMCCSMSSetting In obj
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COL_Name).Value = objl._Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COL_Type).Value = objl._Type
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COl_Mail_Id).Value = objl._MailId
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COl_Mobile_No).Value = objl._MobileNo
                    fndMCCCode.Value = objl.Loc_Code
                Next
                Me.LblMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_desc from TSPL_Location_MASTER where Location_Code='" + fndMCCCode.Value + "' "))
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

#Region "Mail SMS Setting"

    'Public Sub SendMail(ByVal Doc_Code As String, ByVal Doc_Date As String, ByVal Mcc_Name As String, ByVal Frm_id As String, ByVal Shift As String, ByVal Shift_Open_Time As String, ByVal Shift_Close_Time As String)
    '    '=========Rohit 31 Jan,2015=======
    '    is_Send_SMS = IIf(clsFixedParameter.GetData(clsFixedParameterType.Is_Send_Sms, clsFixedParameterCode.MilkSetting, Nothing) = 0, False, True)
    '    If is_Send_SMS = True Then
    '        Send_SMS_Time = clsFixedParameter.GetData(clsFixedParameterType.Send_Sms_Time, clsFixedParameterCode.MilkSetting, Nothing)
    '        qry = "select Max(coalesce(is_Send_sms,'')) from tspl_milk_receipt_Head where convert(date,doc_date,103)=convert(date,'" & Doc_Date & "',103)"
    '        Dim is_sms_sended As String = clsDBFuncationality.getSingleValue(qry)
    '        If is_sms_sended = "1" Then
    '            Exit Sub
    '        End If
    '    Else
    '        Exit Sub
    '    End If

    '    '=====================================
    '    Dim lstUsers As New List(Of String)
    '    If clsCommon.CompairString(clsUserMgtCode.frmMilkShiftEndMCC, Frm_id) = CompairStringResult.Equal Then

    '        Dim sQuery As String = "select * from TSPL_MCC_MAIL_SMS_Setting where program_code='" & Frm_id & "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
    '        For Each row As DataRow In dt.Rows
    '            lstUsers.Add(clsCommon.myCstr(row.Item("_Name")))
    '        Next
    '        If dt.Rows.Count > 0 Then
    '            If Frm_id = "M-Shift_End" Then
    '                Frm_id = "MCC-MST"
    '            End If

    '                SendSMSandEmail(Doc_Code, Doc_Date, Mcc_Name, Frm_id, lstUsers, False, Shift, Shift_Open_Time, Shift_Close_Time)
    '            ''----------------
    '        Else
    '            clsCommon.MyMessageBoxShow("No Person Found to Send Mail.")
    '        End If
    '    Else
    '        SendSMSandEmailForVSP(Doc_Code, Doc_Date, Mcc_Name, Frm_id + "VSP", lstUsers, False, Shift, Shift_Open_Time, Shift_Close_Time)
    '    End If

    'End Sub
    'Comment code as per Balwinder sir -----------
    'Public Sub SendMail(ByVal Doc_Code As String, ByVal Doc_Date As String, ByVal Mcc_Name As String, ByVal Frm_id As String, ByVal Shift As String, ByVal Shift_Open_Time As String, ByVal Shift_Close_Time As String)
    '    '=========Rohit 31 Jan,2015=======
    '    is_Send_SMS = IIf(clsFixedParameter.GetData(clsFixedParameterType.Is_Send_Sms, clsFixedParameterCode.MilkSetting, Nothing) = 0, False, True)
    '    If is_Send_SMS = True Then
    '        Send_SMS_Time = clsFixedParameter.GetData(clsFixedParameterType.Send_Sms_Time, clsFixedParameterCode.MilkSetting, Nothing)
    '        qry = "select Max(coalesce(is_Send_sms,'')) from tspl_milk_receipt_Head where convert(date,doc_date,103)=convert(date,'" & Doc_Date & "',103)"
    '        Dim is_sms_sended As String = clsDBFuncationality.getSingleValue(qry)
    '        If is_sms_sended = "1" Then
    '            Exit Sub
    '        End If
    '    Else
    '        Exit Sub
    '    End If

    '    '=====================================
    '    Dim lstUsers As New List(Of String)
    '    Dim sQuery As String = "select * from TSPL_MCC_MAIL_SMS_Setting where program_code='" & Frm_id & "'"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
    '    For Each row As DataRow In dt.Rows
    '        lstUsers.Add(clsCommon.myCstr(row.Item("_Name")))
    '    Next
    '    If dt.Rows.Count > 0 Then
    '        If Frm_id = "M-Shift_End" Then
    '            Frm_id = "MCC-MST"
    '        End If
    '        SendSMSandEmail(Doc_Code, Doc_Date, Mcc_Name, Frm_id, lstUsers, False, Shift, Shift_Open_Time, Shift_Close_Time)
    '    Else
    '        clsCommon.MyMessageBoxShow("No Person Found to Send Mail.")
    '    End If
    'End Sub
    'Public Sub SendMailForVSP(ByVal Doc_Code As String, ByVal Doc_Date As String, ByVal Mcc_Name As String, ByVal Frm_id As String, ByVal Shift As String, ByVal Shift_Open_Time As String, ByVal Shift_Close_Time As String)
    '    '=========Rohit 31 Jan,2015=======
    '    is_Send_SMS = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.Is_Send_Sms_ForVSP, clsFixedParameterCode.MilkSetting, Nothing)) = 0, False, True)
    '    If is_Send_SMS = True Then
    '        Send_SMS_Time = clsFixedParameter.GetData(clsFixedParameterType.Send_Sms_Time, clsFixedParameterCode.MilkSetting, Nothing)
    '        'qry = "select Max(coalesce(is_Send_sms,'')) from tspl_milk_receipt_Head where convert(date,doc_date,103)=convert(date,'" & Doc_Date & "',103)"
    '        'Dim is_sms_sended As String = clsDBFuncationality.getSingleValue(qry)
    '        'If is_sms_sended = "1" Then
    '        '    Exit Sub
    '        'End If
    '    Else
    '        Exit Sub
    '    End If

    '    '=====================================
    '    Dim lstUsers As New List(Of String)
    '    'Dim sQuery As String = "select * from TSPL_MCC_MAIL_SMS_Setting where program_code='" & Frm_id.Replace("VSP", "") & "' and _Type ='V'"
    '    'Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
    '    'For Each row As DataRow In dt.Rows
    '    '    lstUsers.Add(clsCommon.myCstr(row.Item("_Name")))
    '    'Next
    '    'If dt.Rows.Count > 0 Then
    '    'If Frm_id = "M-Shift_End" Then
    '    '    Frm_id = "MCC-MST"
    '    'End If
    '    SendSMSandEmailForVSP(Doc_Code, Doc_Date, Mcc_Name, Frm_id, lstUsers, False, Shift, Shift_Open_Time, Shift_Close_Time)
    '    'Else
    '    '    clsCommon.MyMessageBoxShow("No Person Found to Send Mail.")
    '    'End If
    'End Sub
    'Comment code as per Balwinder sir ------------------
    '    Private Sub SendSMSandEmailForVSP(ByVal Doc_Code As String, ByVal Doc_Date As String, ByVal Mcc_Name As String, ByVal Frm_id As String, ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean, ByVal Shift As String, Optional ByVal Shift_Open_Time As String = Nothing, Optional ByVal Shift_Close_Time As String = Nothing, Optional ByVal Total_Route As String = Nothing, Optional ByVal Total_VLC As String = Nothing)
    '        Try
    '            Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(Frm_id)

    '            If obj Is Nothing Then
    '                clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '                Return
    '            End If
    '            If clsCommon.myLen(obj.smsbody) <= 0 Then
    '                clsCommon.MyMessageBoxShow("First do sms setting", Me.Text)
    '                Return
    '            End If

    '            Dim strContactPerson As String = ""
    '            Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.CompanyName, objCommonVar.CurrentCompanyName)
    '            strSubject = strSubject.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy"))

    '            '' Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.DOC_NO, Doc_Code)
    '            ''strbody = strbody.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy"))
    '            ''strbody = strbody.Replace(clsEmailSMSConstants.Shift, Shift)

    '            'strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, Frm_id)

    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" Select max(OuterFinal.UOM_Code) as UOM_Code,max(OuterFinal.VSP_Code ) as VSP_Code,max (OuterFinal.[Milk Type]) as [Milk Type],max(OuterFinal.[MCC Name]) as [MCC Name],max(OuterFinal.Shift) as Shift,max(OuterFinal.Date) as date,max(OuterFinal.[Doc Date]) as [Doc Date], max(OuterFinal.[Vlc Uploader Code]) as [Vlc Uploader Code],max(OuterFinal.[VLC Name] ) as [VLC Name],convert(decimal(18,1),round(sum(OuterFinal.[Cow Milk Qty (KG)] ),2)) as [Cow Milk Qty (KG)], max(OuterFinal.[Cow FAT(%)]) as [Cow FAT(%)],max(OuterFinal.[Cow SNF(%)]) as [Cow SNF(%)], convert(decimal(18,2),sum(OuterFinal.[Cow Amount] )) as [Cow Amount],  max(OuterFinal.[Buffalo SNF(%)] ) as [Buffalo SNF(%)],convert(decimal(18,1),round(sum(OuterFinal.[Buffalo Milk Qty (KG)] ),2)) as [Buffalo Milk Qty (KG)], convert(decimal(18,2), sum (OuterFinal.buffaloamount ))  as buffaloamount,  max(OuterFinal.[Buffalo FAT(%)]  ) as [Buffalo FAT(%)], " & _
    '" sum(OuterFinal.[Buffalo FAT (KG)]) as [Buffalo FAT (KG)],sum(OuterFinal. [Buffalo SNF (KG)]) as   [Buffalo SNF (KG)], sum(OuterFinal. [Cow FAT (KG)]) as [Cow FAT (KG)], sum(OuterFinal.[Cow SNF (KG)] ) as [Cow SNF (KG)],case when sum(OuterFinal.[Buffalo Milk Qty (KG)])<>0 then convert(decimal(18,1), round( sum(OuterFinal.[Buffalo FAT (KG)])*100/sum(OuterFinal.[Buffalo Milk Qty (KG)]),2)) else 0 end  as BuffaloFatper," & _
    '" case when sum(OuterFinal.[Buffalo Milk Qty (KG)])<>0 then convert(decimal(18,1), round(sum(OuterFinal.[Buffalo SNF (KG)])*100/sum(OuterFinal.[Buffalo Milk Qty (KG)]),2)) else 0 end as Buffalosnfper," & _
    '" case when sum(OuterFinal.[Cow Milk Qty (KG)])<>0 then convert(decimal(18,1), round(sum(OuterFinal.[Cow SNF (KG)])*100/sum(OuterFinal.[Cow Milk Qty (KG)]),2)) else 0 end as Cowsnfper," & _
    '" case when sum(OuterFinal.[Cow Milk Qty (KG)])<>0 then convert(decimal(18,1), round(sum(OuterFinal.[Cow FAT (KG)])*100/sum(OuterFinal.[Cow Milk Qty (KG)]),2)) else 0 end as Cowfatper from (Select max(final.UOM_Code) as UOM_Code,max(final.[VSP Code]) as VSP_Code,max (final.[Milk Type]) as [Milk Type],max(final.[MCC Name]) as [MCC Name],max(final.Shift) as Shift,max(final.Date) as date,max(final.[Doc Date]) as [Doc Date]," & _
    '            " max(final.[Vlc Uploader Code]) as [Vlc Uploader Code],max(final.[VLC Name] ) as [VLC Name],sum(final.[Cow Milk Qty (KG)] ) as [Cow Milk Qty (KG)]," & _
    '            " max(final.[Cow FAT(%)]) as [Cow FAT(%)],max(final.[Cow SNF(%)]) as [Cow SNF(%)],case when sum(final.[Cow Milk Qty (KG)] )>0 then sum(final.[SRN Amount] ) else 0 end as [Cow Amount], " & _
    '            " max(final.[Buffalo SNF(%)] ) as [Buffalo SNF(%)],sum(final.[Buffalo Milk Qty (KG)] ) as [Buffalo Milk Qty (KG)], case when sum(final.[Buffalo Milk Qty (KG)] )> 0 then  sum (final.[SRN Amount]) else 0 end  as buffaloamount, " & _
    '            " max(final.[Buffalo FAT(%)]  ) as [Buffalo FAT(%)] ,Case When max(final.[FAT(%)]) > 5 Then sum(final.[FAT(KG)]) Else 0 End [Buffalo FAT (KG)], Case When max(final.[FAT(%)]) > 5 Then sum(final.[SNF(KG)]) Else 0 End [Buffalo SNF (KG)],Case When max(final.[FAT(%)]) <= 5 Then sum(final.[FAT(KG)]) Else 0 End [Cow FAT (KG)], Case When max(final.[FAT(%)])  <= 5 Then sum(final.[SNF(KG)]) Else 0 End [Cow SNF (KG)]  From (Select TSPL_MILK_RECEIPT_DETAIL.UOM_Code , Case When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)]," & _
    '            " Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT Else 0 End [Cow Milk Qty (KG)]," & _
    '            " Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT Else 0 End [Buffalo Milk Qty (KG)], Case When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], " & _
    '            " TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name], Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As Date, Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], " & _
    '            " Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift, TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE As [Route Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE As [Vehicle Code], " & _
    '            " TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], " & _
    '            " TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No], TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)]," & _
    '            " TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], Convert(decimal(18,2),TSPL_MILK_SAMPLE_DETAIL.FAT * TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT / 100) As [FAT(KG)], Convert(decimal(18,2),TSPL_MILK_SAMPLE_DETAIL.SNF * TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT / 100) As [SNF(KG)], " & _
    '            " Case When TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL = '' Then 'Auto' Else TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty]," & _
    '            " Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date     From TSPL_MILK_RECEIPT_DETAIL " & _
    '            " Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE " & _
    '            " Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE " & _
    '            " Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE " & _
    '            " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE " & _
    '            " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE " & _
    '            " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code" & _
    '            " Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)" & _
    '            " And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE " & _
    '            " And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code) As final where 2=2  and " & _
    '            " final.[Date] =convert(date,'" & Doc_Date & "',103) and MCC  IN (Select MCC_CODE from TSPL_MILK_Shift_End_Head where doc_code='" & Doc_Code & "') and  final.Shift ='" & Shift & "' " & _
    '            " group by [Vlc Uploader Code],[Milk Type] ) OuterFinal group by  [Vlc Uploader Code] ")

    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                For i As Integer = 0 To dt.Rows.Count - 1

    '                    Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.DOC_NO, Doc_Code)
    '                    strbody = strbody.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy"))
    '                    strbody = strbody.Replace(clsEmailSMSConstants.Shift, Shift)

    '                    strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, Frm_id)

    '                    If strbody.Contains(clsEmailSMSConstants.CompanyName) Then
    '                        strbody = strbody.Replace(clsEmailSMSConstants.CompanyName, objCommonVar.CurrentCompanyName)
    '                    End If

    '                    If strbody.Contains(clsEmailSMSConstants.VLCCode) Then
    '                        strbody = strbody.Replace(clsEmailSMSConstants.VLCCode, clsCommon.myCstr(dt.Rows(i)("MCC Name")))
    '                    End If
    '                    If strbody.Contains(clsEmailSMSConstants.VLCUploaderCode) Then
    '                        strbody = strbody.Replace(clsEmailSMSConstants.VLCUploaderCode, clsCommon.myCstr(dt.Rows(i)("Vlc Uploader Code")))
    '                    End If
    '                    If strbody.Contains(clsEmailSMSConstants.VLCName) Then
    '                        strbody = strbody.Replace(clsEmailSMSConstants.VLCName, clsCommon.myCstr(dt.Rows(i)("VLC Name")))
    '                    End If

    '                    If strbody.Contains(clsEmailSMSConstants.UOM) Then
    '                        strbody = strbody.Replace(clsEmailSMSConstants.UOM, clsCommon.myCstr(dt.Rows(i)("UOM_Code")))
    '                    End If

    '                    If strbody.Contains(clsEmailSMSConstants.CowQty) Then
    '                        strbody = strbody.Replace(clsEmailSMSConstants.CowQty, clsCommon.myCstr(dt.Rows(i)("Cow Milk Qty (KG)")))
    '                    End If
    '                    If strbody.Contains(clsEmailSMSConstants.CowFat) Then
    '                        strbody = strbody.Replace(clsEmailSMSConstants.CowFat, clsCommon.myCstr(dt.Rows(i)("Cowfatper")))
    '                    End If
    '                    If strbody.Contains(clsEmailSMSConstants.CowSNF) Then
    '                        strbody = strbody.Replace(clsEmailSMSConstants.CowSNF, clsCommon.myCstr(dt.Rows(i)("Cowsnfper")))
    '                    End If

    '                    If strbody.Contains(clsEmailSMSConstants.CowAmount) Then
    '                        strbody = strbody.Replace(clsEmailSMSConstants.CowAmount, clsCommon.myCstr(dt.Rows(i)("Cow Amount")))
    '                    End If
    '                    If strbody.Contains(clsEmailSMSConstants.BuffaloQty) Then
    '                        strbody = strbody.Replace(clsEmailSMSConstants.BuffaloQty, clsCommon.myCstr(dt.Rows(i)("Buffalo Milk Qty (KG)")))
    '                    End If
    '                    If strbody.Contains(clsEmailSMSConstants.BuffaloFat) Then
    '                        strbody = strbody.Replace(clsEmailSMSConstants.BuffaloFat, clsCommon.myCstr(dt.Rows(i).Item("BuffaloFatper")))
    '                    End If

    '                    If strbody.Contains(clsEmailSMSConstants.BuffaloSNF) Then
    '                        strbody = strbody.Replace(clsEmailSMSConstants.BuffaloSNF, clsCommon.myCstr(dt.Rows(i)("Buffalosnfper")))
    '                    End If
    '                    If strbody.Contains(clsEmailSMSConstants.BuffaloAmount) Then
    '                        strbody = strbody.Replace(clsEmailSMSConstants.BuffaloAmount, clsCommon.myCstr(dt.Rows(i).Item("buffaloamount")))
    '                    End If

    '                    If strbody.Contains(clsEmailSMSConstants.TotalAmount) Then
    '                        strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, clsCommon.myCdbl(dt.Rows(i)("Cow Amount")) + clsCommon.myCdbl(dt.Rows(i).Item("buffaloamount")))
    '                    End If
    '                    If strbody.Contains(clsEmailSMSConstants.Total_collection) Then
    '                        strbody = strbody.Replace(clsEmailSMSConstants.Total_collection, clsCommon.myCdbl(dt.Rows(i)("Buffalo Milk Qty (KG)")) + clsCommon.myCdbl(dt.Rows(i)("Cow Milk Qty (KG)")))
    '                    End If
    '                    '    Next

    '                    'End If
    '                    Dim lstReceiptents As New List(Of String)
    '                    Dim emailId As String = ""
    '                    strContactPerson = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_VENDOR_MASTER where Vendor_code ='" & clsCommon.myCstr(dt.Rows(i).Item("VSP_Code")) & "' "))
    '                    emailId = clsDBFuncationality.getSingleValue("select _Emailid from TSPL_MCC_MAIL_SMS_Setting where _name ='" & clsCommon.myCstr(dt.Rows(i).Item("VSP_Code")) & "' ")

    '                    strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
    '                    If clsCommon.myLen(clsCommon.myCstr(emailId)) > 0 Then
    '                        lstReceiptents.Add(emailId)
    '                    End If
    '                    Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, clsCommon.myCstr(dt.Rows(i).Item("VSP_Code")))

    '                    If lstReceiptents.Count > 0 Then
    '                        clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, "")
    '                    End If


    '                    'For Each strUser As String In lstUsers
    '                    '    Dim lstReceiptents As New List(Of String)
    '                    '    Dim qry As String = ""
    '                    '    Dim emailId As String = ""
    '                    '    If isSendForApproval Then
    '                    '        strContactPerson = strUser
    '                    '        qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
    '                    '        emailId = clsDBFuncationality.getSingleValue(qry)
    '                    '    Else
    '                    '        strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_VENDOR_MASTER where Vendor_code ='" & strUser & "' ")
    '                    '        emailId = clsDBFuncationality.getSingleValue("select _Emailid from TSPL_MCC_MAIL_SMS_Setting where _name ='" & strUser & "' ")
    '                    '    End If

    '                    '    strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
    '                    '    If clsCommon.myLen(clsCommon.myCstr(emailId)) > 0 Then
    '                    '        lstReceiptents.Add(emailId)
    '                    '    End If
    '                    '    Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

    '                    '    If lstReceiptents.Count > 0 Then
    '                    '        clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, "")
    '                    '    End If
    '                    'Next


    '                Next

    '            End If
    '            If Not clsSMSAtPost_Purchase.SMSATPOST_PUR() Then

    '                SMSSENDONLYForVSP(Doc_Code, Doc_Date, Mcc_Name, lstUsers, Frm_id, Shift, dt)

    '            End If
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    End Sub

    'Private Sub SendSMSandEmailForVSP(ByVal Doc_Code As String, ByVal Doc_Date As String, ByVal Mcc_Name As String, ByVal Frm_id As String, ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean, ByVal Shift As String, Optional ByVal Shift_Open_Time As String = Nothing, Optional ByVal Shift_Close_Time As String = Nothing, Optional ByVal Total_Route As String = Nothing, Optional ByVal Total_VLC As String = Nothing)
    '    Try
    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(Frm_id)

    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If
    '        If clsCommon.myLen(obj.smsbody) <= 0 Then
    '            clsCommon.MyMessageBoxShow("First do sms setting", Me.Text)
    '            Return
    '        End If

    '        Dim strContactPerson As String = ""
    '        Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.CompanyName, objCommonVar.CurrentCompanyName)
    '        strSubject = strSubject.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy"))

    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select max(final.UOM_Code) as UOM_Code,max(final.[VSP Code]) as VSP_Code,max (final.[Milk Type]) as [Milk Type],max(final.[MCC Name]) as [MCC Name],max(final.Shift) as Shift,max(final.Date) as date,max(final.[Doc Date]) as [Doc Date]," & _
    '        " max(final.[Vlc Uploader Code]) as [Vlc Uploader Code],max(final.[VLC Name] ) as [VLC Name],sum(final.[Cow Milk Qty (KG)] ) as [Cow Milk Qty (KG)]," & _
    '        " max(final.[Cow FAT(%)]) as [Cow FAT(%)],max(final.[Cow SNF(%)]) as [Cow SNF(%)], sum(final.[SRN Amount] * case when final.[Cow Milk Qty (KG)] > 0 then  1 else 0 end ) as [Cow Amount], " & _
    '        " max(final.[Buffalo SNF(%)] ) as [Buffalo SNF(%)],sum(final.[Buffalo Milk Qty (KG)] ) as [Buffalo Milk Qty (KG)], sum(final.[SRN Amount] * case when final.[Buffalo Milk Qty (KG)] > 0 then  1 else 0 end )  as buffaloamount, " & _
    '        " max(final.[Buffalo FAT(%)]  ) as [Buffalo FAT(%)] ,sum(final.[FAT(KG)] * Case When  final.[FAT(%)] > 5 Then 1 Else 0 End) as  [Buffalo FAT (KG)], sum(final.[SNF(KG)] * Case When  final.[FAT(%)] > 5 Then 1 Else 0 End) as [Buffalo SNF (KG)],sum(final.[FAT(KG)] * Case When  final.[FAT(%)] <= 5 Then 1 Else 0 End) as [Cow FAT (KG)], sum(final.[SNF(KG)] * Case When final.[FAT(%)]  <= 5 Then 1 Else 0 End) as [Cow SNF (KG)]  From (Select TSPL_MILK_RECEIPT_DETAIL.UOM_Code , Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)]," & _
    '        " Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT Else 0 End [Cow Milk Qty (KG)]," & _
    '        " Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT Else 0 End [Buffalo Milk Qty (KG)], Case When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], " & _
    '        " TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name], Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As Date, Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], " & _
    '        " Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift, TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE As [Route Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE As [Vehicle Code], " & _
    '        " TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], " & _
    '        " TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No], TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)]," & _
    '        " TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], Convert(decimal(18,2),TSPL_MILK_SAMPLE_DETAIL.FAT * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [FAT(KG)], Convert(decimal(18,2),TSPL_MILK_SAMPLE_DETAIL.SNF * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [SNF(KG)], " & _
    '        " Case When TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL = '' Then 'Auto' Else TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty]," & _
    '        " Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date     From TSPL_MILK_RECEIPT_DETAIL " & _
    '        " Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE " & _
    '        " Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE " & _
    '        " Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE " & _
    '        " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE " & _
    '        " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE " & _
    '        " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code" & _
    '        " Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)" & _
    '        " And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE " & _
    '        " And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code) As final where 2=2  and " & _
    '        " final.[Date] =convert(date,'" & Doc_Date & "',103) and MCC  IN (Select MCC_CODE from TSPL_MILK_Shift_End_Head where doc_code='" & Doc_Code & "') and  final.Shift ='" & Shift & "' " & _
    '        " group by [Vlc Uploader Code] ")

    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            For i As Integer = 0 To dt.Rows.Count - 1

    '                Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.DOC_NO, Doc_Code)
    '                strbody = strbody.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy"))
    '                strbody = strbody.Replace(clsEmailSMSConstants.Shift, Shift)

    '                strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, Frm_id)

    '                If strbody.Contains(clsEmailSMSConstants.CompanyName) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.CompanyName, objCommonVar.CurrentCompanyName)
    '                End If

    '                If strbody.Contains(clsEmailSMSConstants.VLCCode) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.VLCCode, clsCommon.myCstr(dt.Rows(i)("MCC Name")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.VLCUploaderCode) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.VLCUploaderCode, clsCommon.myCstr(dt.Rows(i)("Vlc Uploader Code")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.VLCName) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.VLCName, clsCommon.myCstr(dt.Rows(i)("VLC Name")))
    '                End If

    '                If strbody.Contains(clsEmailSMSConstants.UOM) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.UOM, clsCommon.myCstr(dt.Rows(i)("UOM_Code")))
    '                End If

    '                If strbody.Contains(clsEmailSMSConstants.CowQty) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.CowQty, clsCommon.myCstr(dt.Rows(i)("Cow Milk Qty (KG)")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.CowFat) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.CowFat, clsCommon.myCstr(dt.Rows(i)("Cow FAT(%)")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.CowSNF) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.CowSNF, clsCommon.myCstr(dt.Rows(i)("Cow SNF(%)")))
    '                End If

    '                If strbody.Contains(clsEmailSMSConstants.CowAmount) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.CowAmount, clsCommon.myCstr(dt.Rows(i)("Cow Amount")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.BuffaloQty) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.BuffaloQty, clsCommon.myCstr(dt.Rows(i)("Buffalo Milk Qty (KG)")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.BuffaloFat) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.BuffaloFat, clsCommon.myCstr(dt.Rows(i).Item("Buffalo FAT(%)")))
    '                End If

    '                If strbody.Contains(clsEmailSMSConstants.BuffaloSNF) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.BuffaloSNF, clsCommon.myCstr(dt.Rows(i)("Buffalo SNF(%)")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.BuffaloAmount) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.BuffaloAmount, clsCommon.myCstr(dt.Rows(i).Item("buffaloamount")))
    '                End If

    '                If strbody.Contains(clsEmailSMSConstants.TotalAmount) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, clsCommon.myCdbl(dt.Rows(i)("Cow Amount")) + clsCommon.myCdbl(dt.Rows(i).Item("buffaloamount")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.Total_collection) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.Total_collection, clsCommon.myCdbl(dt.Rows(i)("Buffalo Milk Qty (KG)")) + clsCommon.myCdbl(dt.Rows(i)("Cow Milk Qty (KG)")))
    '                End If
    '                Dim lstReceiptents As New List(Of String)
    '                Dim emailId As String = ""
    '                strContactPerson = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_VENDOR_MASTER where Vendor_code ='" & clsCommon.myCstr(dt.Rows(i).Item("VSP_Code")) & "' "))
    '                emailId = clsDBFuncationality.getSingleValue("select _Emailid from TSPL_MCC_MAIL_SMS_Setting where _name ='" & clsCommon.myCstr(dt.Rows(i).Item("VSP_Code")) & "' ")

    '                strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
    '                If clsCommon.myLen(clsCommon.myCstr(emailId)) > 0 Then
    '                    lstReceiptents.Add(emailId)
    '                End If
    '                Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, clsCommon.myCstr(dt.Rows(i).Item("VSP_Code")))

    '                If lstReceiptents.Count > 0 Then
    '                    clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, "")
    '                End If
    '            Next
    '        End If
    '        If Not clsSMSAtPost_Purchase.SMSATPOST_PUR() Then
    '            SMSSENDONLYForVSP(Doc_Code, Doc_Date, Mcc_Name, lstUsers, Frm_id, Shift, dt)
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    'Private Sub SendSMSandEmail(ByVal Doc_Code As String, ByVal Doc_Date As String, ByVal Mcc_Name As String, ByVal Frm_id As String, ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean, ByVal Shift As String, Optional ByVal Shift_Open_Time As String = Nothing, Optional ByVal Shift_Close_Time As String = Nothing, Optional ByVal Total_Route As String = Nothing, Optional ByVal Total_VLC As String = Nothing)
    '    Try
    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(Frm_id)

    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If
    '        If clsCommon.myLen(obj.mailsubjct) <= 0 Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If

    '        Dim strContactPerson As String = ""
    '        Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.DOC_NO, Doc_Code)
    '        strSubject = strSubject.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy"))

    '        Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.DOC_NO, Doc_Code)
    '        strbody = strbody.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy"))
    '        strbody = strbody.Replace(clsEmailSMSConstants.UOM, "LTR")
    '        'strbody = strbody.Replace(clsEmailSMSConstants.Mcc_Code, Mcc_code)
    '        ' strbody = strbody.Replace(clsEmailSMSConstants.Mcc_Name, Mcc_Name)
    '        strbody = strbody.Replace(clsEmailSMSConstants.Shift, Shift)
    '        strbody = strbody.Replace(clsEmailSMSConstants.Shift_Open_Time, Shift_Open_Time)
    '        strbody = strbody.Replace(clsEmailSMSConstants.Shift_Close_Time, Shift_Close_Time)
    '        '  strbody = strbody.Replace(clsEmailSMSConstants.Total_Route, Total_Route)
    '        ' strbody = strbody.Replace(clsEmailSMSConstants.Total_Vlc, Total_VLC)
    '        'strbody = strbody.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)
    '        'strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
    '        strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, Frm_id)
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select state_code,tspl_milk_srn_Head.mcc_code,Mcc_Name,convert(date,doc_date,103) as doc_date," _
    '            & " cast(round(sum(Qty),2,2) as float) as Qty,tspl_mcc_Uom_Detail.UOM_Code,count(distinct(tspl_milk_srn_Head.route_code)) as Route_Count,count(distinct(tspl_milk_srn_Head.vlc_code)) as VLC_Count,convert(decimal(18,2), case when sum(Qty)>0 then sum (FAT_KG)*100/sum(Qty) else 0 end) as FATPER,convert(decimal(18,2), case when sum(Qty)>0 then sum(SNF_KG)*100/sum(Qty) else 0 end) as SNFPER,convert(decimal(18,2), case when sum(Qty)>0 then sum(Amount)/sum(Qty) else 0 end) as Rate,convert(decimal(18,2), sum(Amount) ) as Amount from tspl_milk_srn_Detail inner join tspl_milk_srn_Head on " _
    '            & " tspl_milk_srn_Detail.doc_code=tspl_milk_srn_Head.doc_code left join tspl_mcc_Uom_Detail on tspl_mcc_Uom_Detail.mcc_code=tspl_milk_srn_Head.mcc_code " _
    '            & " and stocking_unit='Y' left join tspl_mcc_Master on tspl_mcc_Master.mcc_cODE=tspl_milk_srn_Head.mcc_code where  convert(date,doc_date,103)=convert(date,'" & Doc_Date & "',103) and shift='" & IIf(clsCommon.myCstr(Shift).Contains("M"), "M", "E") & "' group by tspl_milk_srn_Head.mcc_code," _
    '            & " convert(date,doc_date,103),Mcc_Name,tspl_mcc_Uom_Detail.Uom_Code,state_code order by tspl_milk_srn_Head.mcc_code,convert(date,doc_date,103)")

    '        If strbody.Contains(clsEmailSMSConstants.Total_Route) Then
    '            strbody = strbody.Replace(clsEmailSMSConstants.Total_Route, clsCommon.myCstr(dt.Rows(0)("Route_Count")))
    '        End If
    '        If strbody.Contains(clsEmailSMSConstants.Total_Vlc) Then
    '            strbody = strbody.Replace(clsEmailSMSConstants.Total_Vlc, clsCommon.myCstr(dt.Rows(0)("Vlc_Count")))
    '        End If
    '        If strbody.Contains(clsEmailSMSConstants.Mcc_Name) Then
    '            strbody = strbody.Replace(clsEmailSMSConstants.Mcc_Name, clsCommon.myCstr(dt.Rows(0).Item("Mcc_Name")))
    '        End If
    '        If strbody.Contains(clsEmailSMSConstants.Total_collection) Then
    '            strbody = strbody.Replace(clsEmailSMSConstants.Total_collection, Math.Round(clsCommon.myCdbl(dt.Compute("Sum(Qty)", "")), 2))
    '        End If

    '        If strbody.Contains(clsEmailSMSConstants.FAT) Then
    '            strbody = strbody.Replace(clsEmailSMSConstants.FAT, Math.Round(clsCommon.myCdbl(dt.Compute("FATPER", "")), 2))
    '        End If
    '        If strbody.Contains(clsEmailSMSConstants.SNF) Then
    '            strbody = strbody.Replace(clsEmailSMSConstants.SNF, Math.Round(clsCommon.myCdbl(dt.Compute("SNFPER", "")), 2))
    '        End If
    '        If strbody.Contains(clsEmailSMSConstants.Rate) Then
    '            strbody = strbody.Replace(clsEmailSMSConstants.Rate, Math.Round(clsCommon.myCdbl(dt.Compute("Rate", "")), 2))
    '        End If
    '        If strbody.Contains(clsEmailSMSConstants.Amount) Then
    '            strbody = strbody.Replace(clsEmailSMSConstants.Amount, Math.Round(clsCommon.myCdbl(dt.Compute("Amount", "")), 2))
    '        End If



    '        ''------------------------code for attchament-------------------------------------
    '        'Dim strRptPath As String = ""
    '        'If obj.atchmnt = "Y" Then
    '        '    atchqry = GetMailPrintPreview(txtDocNo.Value)
    '        '    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(atchqry)
    '        '    If dt1.Rows.Count > 0 Then
    '        '        If clsCommon.CompairString(clsCommon.myCstr(dt1.Rows(0)("PurchaseOrder_Type")), "J") = CompairStringResult.Equal Then
    '        '            strRptPath = PurchaseOrderViewer.funreport1(dt1, "WO-G", "Work Order")
    '        '        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt1.Rows(0)("PurchaseOrder_Type")), "L") = CompairStringResult.Equal Then
    '        '            strRptPath = PurchaseOrderViewer.funreport1(dt1, "PO-G", "Purchase Order")
    '        '        Else
    '        '            Throw New Exception("Not a valid Po Type")
    '        '            Return
    '        '        End If
    '        '    End If
    '        'End If
    '        '---------------------------------------------------------------------------


    '        For Each strUser As String In lstUsers
    '            'lstUsers.Add(dr("User_Code").ToString())
    '            Dim lstReceiptents As New List(Of String)
    '            Dim qry As String = ""
    '            Dim emailId As String = ""
    '            If isSendForApproval Then
    '                strContactPerson = strUser
    '                qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
    '                emailId = clsDBFuncationality.getSingleValue(qry)
    '            Else
    '                strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_VENDOR_MASTER where Vendor_code ='" & strUser & "' ")
    '                emailId = clsDBFuncationality.getSingleValue("select _Emailid from TSPL_MCC_MAIL_SMS_Setting where _name ='" & strUser & "' ")
    '            End If

    '            strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
    '            If clsCommon.myLen(clsCommon.myCstr(emailId)) > 0 Then
    '                lstReceiptents.Add(emailId)
    '            End If
    '            Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

    '            If lstReceiptents.Count > 0 Then
    '                clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, "")
    '            End If
    '        Next
    '        'If clsCommon.CompairString(MDI.IsMailSend, "NO") = CompairStringResult.Equal Then
    '        '    clsCommon.MyMessageBoxShow("E-Mail Not Send", Me.Text)
    '        'Else
    '        '    clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
    '        'End If


    '        If Not clsSMSAtPost_Purchase.SMSATPOST_PUR() Then
    '            If clsCommon.myLen(clsCommon.myCstr(Shift_Open_Time)) > 0 Then
    '                SMSSENDONLY(Doc_Code, Doc_Date, Mcc_Name, lstUsers, Frm_id, Shift, Shift_Open_Time, Shift_Close_Time)
    '            Else
    '                SMSSENDONLY(Doc_Code, Doc_Date, Mcc_Name, lstUsers, Frm_id, Shift)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    'Private Sub SMSSENDONLYForVSP(ByVal Doc_Code As String, ByVal Doc_Date As String, ByVal Mcc_Name As String, ByVal User_Code As List(Of String), ByVal frm_id As String, ByVal Shift As String, ByVal dt As DataTable)
    '    Try
    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(frm_id)

    '        If clsCommon.myLen(obj.smsbody) <= 0 Then
    '            Return
    '        End If
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            For i As Integer = 0 To dt.Rows.Count - 1
    '                Dim strbody As String = obj.smsbody
    '                If strbody.Contains(clsEmailSMSConstants.CompanyName) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.CompanyName, objCommonVar.CurrentCompanyName)
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.Shift) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.Shift, Shift)
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.DOC_Date) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy"))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.VLCCode) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.VLCCode, clsCommon.myCstr(dt.Rows(i)("MCC Name")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.UOM) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.UOM, clsCommon.myCstr(dt.Rows(i)("UOM_Code")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.VLCUploaderCode) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.VLCUploaderCode, clsCommon.myCstr(dt.Rows(i)("Vlc Uploader Code")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.VLCName) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.VLCName, clsCommon.myCstr(dt.Rows(i).Item("VLC Name")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.CowQty) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.CowQty, clsCommon.myCstr(dt.Rows(i)("Cow Milk Qty (KG)")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.CowFat) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.CowFat, clsCommon.myCstr(dt.Rows(i)("Cow FAT(%)")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.CowSNF) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.CowSNF, clsCommon.myCstr(dt.Rows(i).Item("Cow SNF(%)")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.CowAmount) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.CowAmount, clsCommon.myCstr(dt.Rows(i)("Cow Amount")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.BuffaloQty) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.BuffaloQty, clsCommon.myCstr(dt.Rows(i)("Buffalo Milk Qty (KG)")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.BuffaloFat) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.BuffaloFat, clsCommon.myCstr(dt.Rows(i).Item("Buffalo FAT(%)")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.BuffaloSNF) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.BuffaloSNF, clsCommon.myCstr(dt.Rows(i)("Buffalo SNF(%)")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.BuffaloAmount) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.BuffaloAmount, clsCommon.myCstr(dt.Rows(i).Item("buffaloamount")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.TotalAmount) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, clsCommon.myCdbl(dt.Rows(i)("Cow Amount")) + clsCommon.myCdbl(dt.Rows(i).Item("buffaloamount")))
    '                End If
    '                If strbody.Contains(clsEmailSMSConstants.Total_collection) Then
    '                    strbody = strbody.Replace(clsEmailSMSConstants.Total_collection, clsCommon.myCdbl(dt.Rows(i)("Buffalo Milk Qty (KG)")) + clsCommon.myCdbl(dt.Rows(i)("Cow Milk Qty (KG)")))
    '                End If
    '                Dim StrPhone_No As String
    '                If IIf(clsFixedParameter.GetData(clsFixedParameterType.Is_Pick_No_from_Mail_Setting, clsFixedParameterCode.MilkSetting, Nothing) = 0, False, True) = True Then
    '                    StrPhone_No = clsDBFuncationality.getSingleValue("select _MobileNo from TSPL_MCC_MAIL_SMS_Setting where _name='" & clsCommon.myCstr(dt.Rows(i).Item("VSP_Code")) & "'")
    '                Else
    '                    StrPhone_No = clsDBFuncationality.getSingleValue("Select substring (Phone1 ,6,10) from TSPL_VENDOR_MASTER where Vendor_Code='" & clsCommon.myCstr(dt.Rows(i).Item("VSP_Code")) & "'")
    '                End If
    '                If clsCommon.myLen(StrPhone_No) >= 10 Then
    '                    clsSMSSend.SendSMS(frm_id, strbody, StrPhone_No)
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub
    'Private Sub SMSSENDONLY(ByVal Doc_Code As String, ByVal Doc_Date As String, ByVal Mcc_Name As String, ByVal User_Code As List(Of String), ByVal frm_id As String, ByVal Shift As String, Optional ByVal Shift_Open_Time As String = Nothing, Optional ByVal Shift_Close_Time As String = Nothing)
    '    Try
    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(frm_id)

    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If


    '        If clsCommon.myLen(obj.smsbody) <= 0 Then
    '            Return
    '        End If
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select state_code,tspl_milk_srn_Head.mcc_code,Mcc_Name,convert(date,doc_date,103) as doc_date," _
    '            & " cast(round(sum(Qty),2,2) as float) as Qty,tspl_mcc_Uom_Detail.UOM_Code,count(distinct(tspl_milk_srn_Head.route_code)) as Route_Count,count(distinct(tspl_milk_srn_Head.vlc_code)) as VLC_Count from tspl_milk_srn_Detail inner join tspl_milk_srn_Head on " _
    '            & " tspl_milk_srn_Detail.doc_code=tspl_milk_srn_Head.doc_code left join tspl_mcc_Uom_Detail on tspl_mcc_Uom_Detail.mcc_code=tspl_milk_srn_Head.mcc_code " _
    '            & " and stocking_unit='Y' left join tspl_mcc_Master on tspl_mcc_Master.mcc_cODE=tspl_milk_srn_Head.mcc_code where  convert(date,doc_date,103)=convert(date,'" & Doc_Date & "',103) and shift='" & IIf(clsCommon.myCstr(Shift).Contains("M"), "M", "E") & "' group by tspl_milk_srn_Head.mcc_code," _
    '            & " convert(date,doc_date,103),Mcc_Name,tspl_mcc_Uom_Detail.Uom_Code,state_code order by tspl_milk_srn_Head.mcc_code,convert(date,doc_date,103)")
    '        Dim strMes As String = obj.smsbody
    '        If strMes.Contains(clsEmailSMSConstants.Doc_Code) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.Doc_Code, Doc_Code)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.DOC_Date) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy"))
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.Total_collection) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.Total_collection, Math.Round(clsCommon.myCdbl(dt.Compute("Sum(Qty)", "")), 2))
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.UOM) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.UOM, "LTR")
    '        End If

    '        If strMes.Contains(clsEmailSMSConstants.State_Name) Then
    '            strMes = strMes & Environment.NewLine
    '            Dim dv As DataView = New DataView(dt)
    '            For Each row As DataRow In dv.ToTable(True, "State_Code").Rows
    '                strMes = strMes & Environment.NewLine & row.Item("State_Code") & "   :  " & Math.Round(clsCommon.myCdbl(dt.Compute("Sum(Qty)", "State_Code='" & row.Item("State_Code") & "'")), 2)
    '            Next
    '            strMes = strMes.Replace(clsEmailSMSConstants.State_Name, "")
    '            ' strMes = strMes.Replace(clsEmailSMSConstants.Mcc_Name, Mcc_Name)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.Mcc_Name) Then
    '            ' strMes = strMes & Environment.NewLine
    '            ' For Each row As DataRow In dt.Rows
    '            'strMes = strMes & Environment.NewLine & row.Item("Mcc_Name") & "   :  " & clsCommon.myCdbl(dt.Compute("Sum(Qty)", "Mcc_Code='" & row.Item("Mcc_Code") & "'"))
    '            'Next
    '            'strMes = strMes.Replace(clsEmailSMSConstants.Mcc_Name, "")
    '            strMes = strMes.Replace(clsEmailSMSConstants.Mcc_Name, clsCommon.myCstr(dt.Rows(0).Item("Mcc_Name")))
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.Shift) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.CustomerNo, Shift)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.Shift) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.Shift, Shift)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.Shift_Open_Time) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.Shift_Open_Time, Shift_Open_Time)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.Shift_Close_Time) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.Shift_Close_Time, Shift_Close_Time)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.Total_Route) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.Total_Route, clsCommon.myCstr(dt.Rows(0)("Route_Count")))
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.Total_Vlc) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.Total_Vlc, clsCommon.myCstr(dt.Rows(0)("Vlc_Count")))
    '        End If

    '        For Each Str As String In User_Code
    '            Dim StrPhone_No As String = clsDBFuncationality.getSingleValue("select _MobileNo from TSPL_MCC_MAIL_SMS_Setting where _name='" & Str & "'")
    '            clsSMSSend.SendSMS(frm_id, strMes, StrPhone_No)
    '            qry = "update tspl_milk_receipt_Head set is_send_sms='1' where convert(date,doc_date,103)=convert(date,'" & Doc_Date & "',103)"
    '            clsDBFuncationality.ExecuteNonQuery(qry)
    '        Next
    '        ' clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub
    'Comment code as per Balwinder sir
#End Region
    '------------------------------------------------------------------------

    Private Sub fndMCCCode_Validating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndMCCCode._MYValidating
        Dim sQuery As String = "select Location_Category from tspl_location_master where Location_Code='" & fndMCCCode.Value & "'"
        'If clsDBFuncationality.getSingleValue(sQuery) = "HO" Then
        Dim qry As String = "select Location_Code as Location,Location_Desc as Description  from TSPL_LOCATION_MASTER "
        ' fndMCCCode.Value = clsCommon.ShowSelectForm("LocatMast", qry, "Location", "  upper(location_category)='MCC' ", fndMCCCode.Value, "Location_Code", isButtonClicked)
        fndMCCCode.Value = clsLocation.getFinder(" is_section='N' and is_Sub_location='N' and Rejected_Type='N' and GIT_Type<>'Y'", "Location_code", isButtonClicked)
        Me.LblMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_desc from TSPL_Location_MASTER where Location_Code='" + fndMCCCode.Value + "' "))
    End Sub

    Private Function strMes() As String
        Throw New NotImplementedException
    End Function

End Class

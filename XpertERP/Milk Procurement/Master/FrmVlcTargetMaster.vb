Imports Microsoft.VisualBasic
Imports System
Imports System.IO
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
Public Class FrmVlcTargetMaster
    Inherits FrmMainTranScreen


    Dim sQuery As String = ""

    Const colSno As String = "colSno"
    Const colVLCCode As String = "colVLCCode"
    Const colVLCUploaderCode As String = "colVLCUploaderCode"
    Const colVlcName As String = "colVlcName"
    Const colVSP_Code As String = "colVSP_Code"
    Const colVSP_Name As String = "colVSP_Name"
    Const colDay_Target As String = "colDay_Target"
    Const colMorning_Target As String = "colRoute_Name"
    Const colEvening_Target As String = "colEvening_Target"
    Const colRemarks As String = "colRemarks"
    Const colMPCode As String = "colMPCode"
    Const colMPName As String = "colMPName"
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False

    
    Sub AddNew()
        ' isNewEntry = True
        fndDocCode.Value = ""
        'fndDocCode.Tag = Nothing
        fndMccCode.Enabled = True
        btnsave.Text = "Save"
        'DtpDocDate.MinDate = Nothing
        'DtpFromDate.MinDate = Nothing
        'DtpTodate.MinDate = Nothing
        DtpDocDate.Value = clsCommon.GETSERVERDATE()
        DtpFromDate.Value = clsCommon.GETSERVERDATE()
        DtpTodate.Value = clsCommon.GETSERVERDATE()
        gv1.DataSource = Nothing
        BtnSelect.Text = "Select"
        btnsave.Enabled = True
        BtnPost.Enabled = True
        btnDelete.Enabled = True
        fndMccCode.Enabled = True
        fndRouteCode.Enabled = True
        gv1.Rows.Clear()
        isCellValueChangedOpen = False
        'gv1.Columns.Clear()
        chkMP.Checked = False
        fndVLCCode.Visible = False
        fndVLCName.Visible = False
        fndVSPCode.Visible = False
        fndVSPName.Visible = False
        lblVLC.Visible = False
        lblVSP.Visible = False
        fndVLCCode.Visible = Nothing
        fndVLCName.Text = Nothing
        fndVSPCode.Text = Nothing
        fndVSPName.Text = Nothing
        '  Me.dtpDocDate.Value = clsCommon.GETSERVERDATE()
        ' Me.cboShift.SelectedIndex = -1
        Me.fndRouteCode.Value = Nothing 'clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        lblRouteName.Text = Nothing
        UcAttachment1.BlankAllControls()
    End Sub

    Public Sub LoadData(ByVal strDoc As String, ByVal strRoute_Code As String, ByVal trans As SqlTransaction, ByVal navType As NavigatorType)
        Try
            'AddNew()
            BtnSave.Text = "Save"
            BtnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            fndMccCode.Enabled = False

            If clsCommon.myLen(strDoc) > 0 Then
                Dim Strqry As String = "SELECT count(MP_Code) FROM TSPL_Vlc_Target_Detail where Document_Code = '" + strDoc + "'"
                Dim checkMp As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If checkMp = 0 Then
                    chkMP.Checked = False
                Else
                    chkMP.Checked = True
                End If


            End If
          
            If chkMP.Checked Then
                LoadBlankGridforVLC()
            Else
                LoadBlankGridforRoute()
            End If

            Dim objList As List(Of ClsVlcTargetMaster)
            If chkMP.Checked Then
                If clsCommon.myLen(strDoc) > 0 Then
                    objList = ClsVlcTargetMaster.GetData(strDoc, strRoute_Code, navType, Nothing, fndVLCCode.Value)
                Else
                    objList = ClsVlcTargetMaster.GetData(strDoc, strRoute_Code, navType, trans, fndVLCCode.Value)
                End If
            Else
                If clsCommon.myLen(strDoc) > 0 Then
                    objList = ClsVlcTargetMaster.GetData(strDoc, strRoute_Code, navType, Nothing)
                Else
                    objList = ClsVlcTargetMaster.GetData(strDoc, strRoute_Code, navType, trans)
                End If
            End If


            ' If clsCommon.myLen(obj.DOC_DATE) > 0 Then
            'DtpDocDate.Value = obj.DOC_DATE
            'End If
            'fndMccCode.Value = obj.MCC_CODE
            '  cboShift.SelectedValue = obj.SHIFT
            gv1.Rows.Clear()
            For Each obj As ClsVlcTargetMaster In objList
                If Not IsNothing(obj) Then
                    UsLock1.Status = obj.POSTED
                    'If clsCommon.myLen(strRoute_Code) <= 0 Then

                    fndRouteCode.Value = obj.Route_Code
                    lblRouteName.Text = obj.Route_Name
                    fndMccCode.Value = obj.MCC_CODE
                    lblMccName.Text = obj.MCC_NAME

                    If clsCommon.myLen(obj.DOC_CODE) > 0 Then
                        DtpDocDate.Value = obj.DOC_DATE
                        DtpFromDate.Value = obj.From_DATE
                        DtpTodate.Value = obj.To_DATE
                        fndDocCode.Value = obj.DOC_CODE
                    Else
                        DtpDocDate.Value = clsCommon.GETSERVERDATE()
                        DtpFromDate.Value = clsCommon.GETSERVERDATE()
                        DtpTodate.Value = clsCommon.GETSERVERDATE()
                    End If
                    If clsCommon.myLen(obj.DOC_CODE) > 0 Then
                        BtnSave.Text = "Update"
                        fndMccCode.Enabled = False
                        fndRouteCode.Enabled = False
                    End If

                    If obj.POSTED = ERPTransactionStatus.Approved Then
                        BtnSave.Enabled = False
                        btnPost.Enabled = False
                        btnDelete.Enabled = False
                    End If
                    'End If
                    'LoadBlankGrid()

                    'For Each obj1 As ClsVlcTargetMaster In ClsVlcTargetMaster.ObjList
                    gv1.Rows.AddNew()
                    If chkMP.Checked Then
                        fndVLCCode.Value = obj.VLC_Code
                        fndVLCName.Text = obj.VLC_Name
                        fndVSPCode.Text = obj.VSP_Code
                        fndVSPName.Text = obj.VSP_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMPCode).Value = obj.MP_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMPName).Value = obj.MP_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDay_Target).Value = obj.Day_Target
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMorning_Target).Value = obj.Morning_target
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEvening_Target).Value = obj.Evening_target

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = obj.Remarks
                    Else
                        fndVLCCode.Value = Nothing
                        fndVLCName.Text = Nothing
                        fndVSPCode.Text = Nothing
                        fndVSPName.Text = Nothing
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCCode).Value = obj.VLC_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCUploaderCode).Value = obj.VLC_Uplader_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVlcName).Value = obj.VLC_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVSP_Code).Value = obj.VSP_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVSP_Name).Value = obj.VSP_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDay_Target).Value = obj.Day_Target
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMorning_Target).Value = obj.Morning_target
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEvening_Target).Value = obj.Evening_target

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = obj.Remarks
                    End If


                    If clsCommon.myLen(obj.DOC_CODE) > 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSno).Value = True
                    End If
                    'Next
                    UcAttachment1.LoadData(obj.DOC_CODE)
                End If
            Next

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmVlcTargetMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso BtnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmVLCMasterTarget)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")

            End If

        End If
        btnsave.Visible = MyBase.isModifyFlag
        BtnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmVlcTargetMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        If chkMP.Checked Then
            LoadBlankGridforVLC()
        Else
            LoadBlankGridforRoute()
        End If
        AddNew()
        Me.fndMccCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER inner join tspl_mcc_master on mcc_code=Default_Location where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        'If clsCommon.myCstr(fndMccCode.Value) <> "" Then
        '    Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(fndMccCode.Value)
        '    If DTShift.Rows.Count > 1 Then
        '        clsCommon.MyMessageBoxShow("There are more then one shifts are opened.Only one Shift can be Opened..")
        '        ' DtpDocDate.ReadOnly = True
        '        'Me.Close()
        '        'Exit Sub
        '        'Throw New Exception("There are more then one shifts are opened.Only one Shift can be Opened..")
        '    ElseIf DTShift.Rows.Count <= 0 Then
        '        clsCommon.MyMessageBoxShow("No shift is opened. one Shift Must be Opened..")
        '        '  DtpDocDate.ReadOnly = True
        '        'Me.Close()
        '        'Exit Sub
        '        'Throw New Exception("No shift is opened. one Shift Must be Opened..")
        '    Else
        '        DtpDocDate.Value = DTShift.Rows(0).Item("MCC_Shift_date")
        '        ' DtpDocDate.ReadOnly = True
        'Dim qry As String = "select Distinct Doc_Code as Milk_receipt_code,Mcc_name from TSPL_MCC_ROUTE_MASTER inner join " _
        '  & " TSPL_MILK_RECEIPT_DETAIL rh on rh.mcc_Code='" & clsCommon.myCstr(fndMccCode.Value) & "'  and" _
        '  & " convert(date,Doc_Date,103)=convert(date,'" & clsCommon.myCDate(DtpDocDate.Value) & "',103) and rh.route_code=TSPL_MCC_ROUTE_MASTER.route_code " _
        '  & " inner join tspl_mcc_master mm on mm.mcc_Code=rh.mcc_Code"
        Dim qry As String = "select Distinct Mcc_name from TSPL_MCC_MASTER where mcc_Code='" & clsCommon.myCstr(fndMccCode.Value) & "'"
        ' Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        ' fndMccCode.Tag = dt.Rows(0).Item("Milk_receipt_code")

        lblMccName.Text = clsDBFuncationality.getSingleValue(qry) 'dt.Rows(0).Item("Mcc_name")
        '     End If
        'End If
        DtpFromDate.Value = clsCommon.GETSERVERDATE()
        DtpTodate.Value = clsCommon.GETSERVERDATE()

    End Sub
    Public Sub LoadBlankGridforRoute()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoSNO As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSNO.FormatString = ""
        repoSNO.HeaderText = "Select"
        repoSNO.Name = colSno
        repoSNO.Width = 50
        repoSNO.IsVisible = True
        repoSNO.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoSNO)

        Dim repoVLCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVLCCode.FormatString = ""
        repoVLCCode.HeaderText = "VLC Code"
        repoVLCCode.Name = colVLCCode
        repoVLCCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoVLCCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoVLCCode.Width = 0
        repoVLCCode.IsVisible = False
        repoVLCCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVLCCode)

        Dim repoVLCUploaderCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVLCUploaderCode.FormatString = ""
        repoVLCUploaderCode.HeaderText = "DCS Code"
        repoVLCUploaderCode.Name = colVLCUploaderCode
        repoVLCUploaderCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoVLCUploaderCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoVLCUploaderCode.Width = 100
        repoVLCUploaderCode.IsVisible = True
        repoVLCUploaderCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVLCUploaderCode)



        Dim repoVLCName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVLCName.FormatString = ""
        repoVLCName.HeaderText = "DCS Name"
        repoVLCName.Name = colVlcName
        repoVLCName.Width = 200
        repoVLCName.IsVisible = True
        repoVLCName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVLCName)

        Dim repoCONS As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCONS.FormatString = ""
        repoCONS.HeaderText = "Secretary Code"
        repoCONS.Name = colVSP_Code
        repoCONS.Width = 100
        repoCONS.IsVisible = True
        repoCONS.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCONS)


        Dim repovlc_Code_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovlc_Code_Code.FormatString = ""
        repovlc_Code_Code.HeaderText = "Secretary Name"
        repovlc_Code_Code.Name = colVSP_Name
        repovlc_Code_Code.Width = 200
        repovlc_Code_Code.IsVisible = True
        repovlc_Code_Code.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repovlc_Code_Code)


        Dim repoqtyMcc As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoqtyMcc.FormatString = ""
        repoqtyMcc.HeaderText = "Day Target"
        repoqtyMcc.Name = colDay_Target
        repoqtyMcc.Width = 80
        repoqtyMcc.IsVisible = True
        repoqtyMcc.ReadOnly = False
        repoqtyMcc.Minimum = 0
        repoqtyMcc.ShowUpDownButtons = False
        gv1.MasterTemplate.Columns.Add(repoqtyMcc)

        Dim repoFATMCC As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFATMCC.FormatString = ""
        repoFATMCC.HeaderText = "Morning Target"
        repoFATMCC.Name = colMorning_Target
        repoFATMCC.Width = 80
        repoFATMCC.IsVisible = True
        repoFATMCC.ReadOnly = False
        repoFATMCC.Minimum = 0
        'repoFATMCC.Maximum = 100
        repoFATMCC.ShowUpDownButtons = False
        gv1.MasterTemplate.Columns.Add(repoFATMCC)

        Dim repoSNFMCC As GridViewDecimalColumn = New GridViewDecimalColumn()
        'Dim repoSNFMCC As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNFMCC.FormatString = ""
        repoSNFMCC.HeaderText = "Evening Target"
        repoSNFMCC.Name = colEvening_Target
        repoSNFMCC.Width = 80
        repoSNFMCC.IsVisible = True
        repoSNFMCC.ReadOnly = False
        repoSNFMCC.Minimum = 0
        ' repoSNFMCC.Maximum = 100
        repoSNFMCC.ShowUpDownButtons = False
        gv1.MasterTemplate.Columns.Add(repoSNFMCC)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 200
        repoRemarks.IsVisible = True
        repoRemarks.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        gv1.AllowDeleteRow = True

        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.AllowAddNewRow = False
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
        ReStoreGridLayout()
    End Sub

    Public Sub LoadBlankGridforVLC()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoSNO As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSNO.FormatString = ""
        repoSNO.HeaderText = "Select"
        repoSNO.Name = colSno
        repoSNO.Width = 50
        repoSNO.IsVisible = True
        repoSNO.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoSNO)

        Dim repoMPCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMPCode.FormatString = ""
        repoMPCode.HeaderText = "MP Code"
        repoMPCode.Name = colMPCode
        'repoMPCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoMPCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoMPCode.Width = 100
        repoMPCode.IsVisible = True
        repoMPCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoMPCode)

        Dim repoMpName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMpName.FormatString = ""
        repoMpName.HeaderText = "MP Name"
        repoMpName.Name = colMPName
        'repoMpName.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoMpName.TextImageRelation = TextImageRelation.TextBeforeImage
        repoMpName.Width = 100
        repoMpName.IsVisible = True
        repoMpName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoMpName)



       

        Dim repoqtyMcc As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoqtyMcc.FormatString = ""
        repoqtyMcc.HeaderText = "Day Target"
        repoqtyMcc.Name = colDay_Target
        repoqtyMcc.Width = 80
        repoqtyMcc.IsVisible = True
        repoqtyMcc.ReadOnly = False
        repoqtyMcc.Minimum = 0
        repoqtyMcc.ShowUpDownButtons = False
        gv1.MasterTemplate.Columns.Add(repoqtyMcc)

        Dim repoFATMCC As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFATMCC.FormatString = ""
        repoFATMCC.HeaderText = "Morning Target"
        repoFATMCC.Name = colMorning_Target
        repoFATMCC.Width = 80
        repoFATMCC.IsVisible = True
        repoFATMCC.ReadOnly = False
        repoFATMCC.Minimum = 0
        'repoFATMCC.Maximum = 100
        repoFATMCC.ShowUpDownButtons = False
        gv1.MasterTemplate.Columns.Add(repoFATMCC)

        Dim repoSNFMCC As GridViewDecimalColumn = New GridViewDecimalColumn()
        'Dim repoSNFMCC As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNFMCC.FormatString = ""
        repoSNFMCC.HeaderText = "Evening Target"
        repoSNFMCC.Name = colEvening_Target
        repoSNFMCC.Width = 80
        repoSNFMCC.IsVisible = True
        repoSNFMCC.ReadOnly = False
        repoSNFMCC.Minimum = 0
        ' repoSNFMCC.Maximum = 100
        repoSNFMCC.ShowUpDownButtons = False
        gv1.MasterTemplate.Columns.Add(repoSNFMCC)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 200
        repoRemarks.IsVisible = True
        repoRemarks.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        gv1.AllowDeleteRow = True

        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.AllowAddNewRow = False
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
        ReStoreGridLayout()
    End Sub


    Private Sub fndMccCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndMccCode._MYValidating
        Dim sQuery As String = "select Location_Category from tspl_location_master where Location_Code='" & fndMccCode.Value & "'"
        'If clsDBFuncationality.getSingleValue(sQuery) = "HO" Then
        Dim qry As String = "select Location_Code as Location,Location_Desc as Description  from TSPL_LOCATION_MASTER "
        fndMccCode.Value = clsCommon.ShowSelectForm("LocatMast", qry, "Location", "  upper(location_category)='MCC' ", fndMccCode.Value, "Location_Code", isButtonClicked)
        'Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(fndMccCode.Value)
        'If DTShift.Rows.Count > 1 Then
        '    clsCommon.MyMessageBoxShow("There are more then one shifts are opened.Only one Shift can be Opened..")
        '    Me.Close()
        'ElseIf DTShift.Rows.Count <= 0 Then
        '    clsCommon.MyMessageBoxShow("No shifts is opened.Atleats one Shift should be Opened..")
        '    BtnSave.Enabled = False
        '    '  Me.Close()
        'Else
        '    BtnSave.Enabled = True
        '    DtpDocDate.Value = DTShift.Rows(0).Item("MCC_Shift_date")

        'End If
        '' End If
        DtpFromDate.Value = clsCommon.GETSERVERDATE()
        DtpTodate.Value = clsCommon.GETSERVERDATE()
        qry = "select Distinct Mcc_name from TSPL_MCC_MASTER where mcc_Code='" & clsCommon.myCstr(fndMccCode.Value) & "'"
        ' Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        lblMccName.Text = clsDBFuncationality.getSingleValue(qry) 'dt.Rows(0).Item("Mcc_name")
    End Sub

    Private Sub fndRouteCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndRouteCode._MYValidating
        Try
            'and rh.route_code=TSPL_MCC_ROUTE_MASTER.route_code
            Dim qry As String = "select Distinct TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as [Route Description] from TSPL_MCC_ROUTE_MASTER where TSPL_MCC_ROUTE_MASTER.mcc_Code ='" & clsCommon.myCstr(fndMccCode.Value) & "' "
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ROTFND1", qry)

            If dr IsNot Nothing Then
                fndRouteCode.Value = clsCommon.myCstr(dr("code")) 'gv.CurrentRow.Cells(colRoutecode).Value
                lblRouteName.Text = clsCommon.myCstr(dr("Route Description")) 'gv.CurrentRow.Cells(colRoutename).Value
                If chkMP.Checked = False Then
                    LoadData(fndMccCode.Tag, fndRouteCode.Value, Nothing, NavigatorType.Current)
                End If

            Else
                fndRouteCode.Value = ""
                lblRouteName.Text = ""
                'gv.CurrentRow.Cells(colRoutecode).Value = ""
                'gv.CurrentRow.Cells(colRoutename).Value = ""
            End If
            ' fndVLCCode.Focus()
        Catch ex As Exception
        End Try
    End Sub

    Sub SaveData()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmVLCMasterTarget, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If

        Dim trans As SqlTransaction = Nothing
        Try
            If (AllowToSave()) Then

                trans = clsDBFuncationality.GetTransactin()
                Dim objList As New List(Of ClsVlcTargetMaster)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCdbl(grow.Cells(colSno).Value) = 1 Then
                        Dim objHead As ClsVlcTargetMaster
                        objHead = New ClsVlcTargetMaster
                        objHead.DOC_CODE = clsCommon.myCstr(fndDocCode.Value)
                        objHead.DOC_DATE = clsCommon.myCDate(DtpDocDate.Value)
                        objHead.From_DATE = clsCommon.myCDate(DtpFromDate.Value)
                        objHead.To_DATE = clsCommon.myCDate(DtpTodate.Value)

                        objHead.MCC_CODE = clsCommon.myCstr(fndMccCode.Value)
                        ' objHead.MACHINE_NO = clsCommon.myCstr(txtSerialNo.Text)
                        objHead.Route_Code = clsCommon.myCstr(fndRouteCode.Text)
                        objHead.Route_Code = clsCommon.myCstr(fndRouteCode.Value)
                        objHead.DOC_CODE = fndDocCode.Value
                        If chkMP.Checked Then
                            objHead.VLC_Code = clsCommon.myCstr(fndVLCCode.Value)
                            objHead.VSP_Code = clsCommon.myCstr(fndVSPCode.Text)
                            objHead.MP_Code = clsCommon.myCstr(grow.Cells(colMPCode).Value)
                        Else
                            objHead.VLC_Code = clsCommon.myCstr(grow.Cells(colVLCCode).Value)
                            objHead.VSP_Code = clsCommon.myCstr(grow.Cells(colVSP_Code).Value)
                        End If

                        objHead.Day_Target = clsCommon.myCdbl(grow.Cells(colDay_Target).Value)
                        objHead.Morning_target = clsCommon.myCdbl(grow.Cells(colMorning_Target).Value)
                        objHead.Evening_target = clsCommon.myCdbl(grow.Cells(colEvening_Target).Value)
                        objHead.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)



                        objList.Add(objHead)

                    End If

                Next
                If ClsVlcTargetMaster.SaveData(objList, trans) Then
                    trans.Commit()
                    UcAttachment1.SaveData(objList(0).DOC_CODE)
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(objList(0).DOC_CODE, objList(0).Route_Code, Nothing, NavigatorType.Current)
                    fndRouteCode.Focus()
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If BtnSave.Text = "Update" Then
                Dim strchk As String = "select Posted from TSPL_MILK_truck_Sheet_HEAD where DOC_COde='" + fndDocCode.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                    Return False
                End If
            End If
           

            If clsCommon.myLen(Me.fndMCCCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter MCC", Me.Text)
                fndMccCode.Focus()
                Return False
            End If
            'Dim objList As New List(Of ClsVlcTargetMaster)
            'For Each grow As GridViewRowInfo In gv1.Rows
            '    If clsCommon.myCdbl(grow.Cells(colSno).Value) = 1 Then
            '        Dim objHead As ClsVlcTargetMaster
            '        objHead = New ClsVlcTargetMaster
            '            objHead.VLC_Code = clsCommon.myCstr(grow.Cells(colVLCCode).Value)

            '        objList.Add(objHead)
            '    End If

            'Next

            'If chkMP.Checked Then
            '    Dim chkDateQry As String = "select count(*)  from TSPL_Vlc_Target_Detail where Route_Code ='" & fndRouteCode.Value & "' and vlc_code in('" + fndVLCCode.Value + "') and MCC_Code is not null"
            '    'chkDateQry += " and convert(date,Frm_Date,103) between (convert(date,'" & frmdate & "',103)) and (convert(date,'" & todate & "',103)) "
            '    chkDateQry += " and convert(date,Frm_Date,103)>=convert(date,'" + DtpFromDate.Value + "',103) and convert(date,To_Date,103)<=convert(date,'" + DtpTodate.Value + "',103)"
            '    Dim chkDate As Integer = clsDBFuncationality.getSingleValue(chkDateQry)
            '    If chkDate = 0 Then
            '        Return True
            '    Else
            '        clsCommon.MyMessageBoxShow("from date ")
            '        Return False
            '    End If


            'End If

            Dim grid_vlc_Count As Integer = 0
            For Each row As GridViewRowInfo In gv1.Rows
                If row.Cells(colSno).Value = True Then
                    grid_vlc_Count += 1
                    If clsCommon.myCdbl(row.Cells(colDay_Target).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Please Fill Day Target in Row No " & grid_vlc_Count & "", Me.Text)
                        gv1.Focus()
                        Return False
                    End If
                End If
            Next
            If grid_vlc_Count <= 0 Then
                Throw New Exception("Please select atleast one check box ")
            End If

            '============================================
            

            '============================================
            UcCustomFields1.AllowToSave()
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        SaveData()
    End Sub

    Private Sub fndDocCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocCode._MYNavigator
        LoadData(fndDocCode.Value, "", Nothing, NavType)
    End Sub

    Sub DeleteData()
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
                If (ClsVlcTargetMaster.DeleteData(fndDocCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndDocCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocCode._MYValidating
        Try
            Dim whrcls As String = ""
            Dim dr As DataRow = ClsVlcTargetMaster.getFinder(whrcls, fndDocCode.Value, isButtonClicked)
            If Not IsNothing(dr) Then
                fndDocCode.Value = dr("Code")
                ' fndDocCode.Tag = dr("Milk Receipt Code")
                fndRouteCode.Value = dr("Route Code")
            Else
                fndDocCode.Value = Nothing
                fndRouteCode.Value = Nothing
            End If

            If clsCommon.myLen(fndDocCode.Value) > 0 Then
                LoadData(fndDocCode.Value, fndRouteCode.Value, Nothing, NavigatorType.Current)
                BtnSave.Text = "&Update"
                ' btnDelete.Enabled = True
                fndMccCode.MyReadOnly = True
                UcAttachment1.LoadData(fndDocCode.Value)
            Else
                Reset()
                fndMccCode.MyReadOnly = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing

            If (myMessages.postConfirm()) Then
                'SaveData()
                '              Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If (ClsVlcTargetMaster.PostData(fndDocCode.Value, fndRouteCode.Value, True)) Then
                    '                   trans.Commit()
                    msg = "Successfully Posted"
                Else
                    'trans.Rollback()
                    qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + MyBase.Form_ID + "' "
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim level As String = dt.Rows(0)("LEVEL").ToString()
                        Dim NoOflevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))
                        If clsCommon.CompairString(level, "Level1") = CompairStringResult.Equal Then
                            msg = "Level 1 Approval done. "
                            If NoOflevel = 1 Then
                                msg += "Successfully Posted. "
                            Else
                                msg += "Level 2 Approval Required."
                            End If
                        ElseIf clsCommon.CompairString(level, "Level2") = CompairStringResult.Equal Then
                            msg = "Level 2 Approval done. "
                            If NoOflevel = 2 Then
                                msg += "Successfully Posted "
                            Else
                                msg += "Level 3 Approval Required."
                            End If
                        Else
                            msg = "Level 3 Approval done. Successfully Posted. "
                        End If
                    End If
                End If
                common.clsCommon.MyMessageBoxShow(msg)
                LoadData(fndDocCode.Value, fndRouteCode.Value, Nothing, NavigatorType.Current)
                'If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                '    PrintDataNew()
                'End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnsaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnsaveLayout.Click
        gv1.MasterTemplate.FilterDescriptors.Clear()
        Dim obj As New clsGridLayout()
        obj.ReportID = MyBase.Form_ID
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout = New MemoryStream()
        gv1.SaveLayout(obj.GridLayout)
        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        obj.GridColumns = gv1.ColumnCount
        If obj.SaveData() Then
            common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
        End If
        ''stuti regarding memory leakage
        obj.GridLayout.Close()
        obj.GridLayout.Dispose()
    End Sub

    Private Sub BtnDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            'If clsCommon.myLen("LoadinMainGrid") > 0 Then
            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                    gv1.Columns(ii).IsVisible = False
                    gv1.Columns(ii).VisibleInColumnChooser = True
                Next

                gv1.LoadLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
            'End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If isCellValueChangedOpen = False Then
                isCellValueChangedOpen = True
                If e.Column Is gv1.Columns(colMorning_Target) Then
                    If clsCommon.myCdbl(gv1.CurrentRow.Cells(colDay_Target).Value) > 0 Then
                        If clsCommon.myCdbl(gv1.CurrentRow.Cells(colDay_Target).Value) < clsCommon.myCdbl(gv1.CurrentRow.Cells(colMorning_Target).Value) Then
                            clsCommon.MyMessageBoxShow(Me, "Please fill Morning Target Less then Day Target..", Me.Text)
                            gv1.CurrentRow.Cells(colMorning_Target).Value = 0
                            isCellValueChangedOpen = False
                        End If
                        gv1.CurrentRow.Cells(colEvening_Target).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colDay_Target).Value) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colMorning_Target).Value)
                    Else
                        gv1.CurrentRow.Cells(colEvening_Target).Value = -clsCommon.myCdbl(gv1.CurrentRow.Cells(colMorning_Target).Value)
                    End If
                End If
                If e.Column Is gv1.Columns(colEvening_Target) Then
                    If clsCommon.myCdbl(gv1.CurrentRow.Cells(colDay_Target).Value) > 0 Then
                        If clsCommon.myCdbl(gv1.CurrentRow.Cells(colDay_Target).Value) < clsCommon.myCdbl(gv1.CurrentRow.Cells(colEvening_Target).Value) Then
                            clsCommon.MyMessageBoxShow(Me, "Please fill Morning Target Less then Day Target..", Me.Text)
                            gv1.CurrentRow.Cells(colEvening_Target).Value = 0
                            isCellValueChangedOpen = False
                        End If
                        gv1.CurrentRow.Cells(colMorning_Target).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colDay_Target).Value) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colEvening_Target).Value)
                    Else
                        gv1.CurrentRow.Cells(colMorning_Target).Value = -clsCommon.myCdbl(gv1.CurrentRow.Cells(colEvening_Target).Value)
                    End If
                End If
                If e.Column Is gv1.Columns(colDay_Target) Then
                    If clsCommon.myCdbl(gv1.CurrentRow.Cells(colDay_Target).Value) > 0 Then
                        gv1.CurrentRow.Cells(colMorning_Target).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colDay_Target).Value) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colEvening_Target).Value)
                    Else
                        gv1.CurrentRow.Cells(colMorning_Target).Value = -clsCommon.myCdbl(gv1.CurrentRow.Cells(colEvening_Target).Value)
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnSelect_Click(sender As Object, e As EventArgs) Handles BtnSelect.Click
        Try
            If BtnSelect.Text = "Unselect" Then
                For Each row As GridViewRowInfo In gv1.Rows
                    row.Cells(colSno).Value = False
                    BtnSelect.Text = "Select"
                Next
            Else
                For Each row As GridViewRowInfo In gv1.Rows
                    row.Cells(colSno).Value = True
                    BtnSelect.Text = "Unselect"
                Next
            End If
            
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub fndVLCCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndVLCCode._MYValidating
        If chkMP.Checked Then
            If clsCommon.myLen(Me.fndRouteCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select route", Me.Text)
                fndRouteCode.Focus()
                Exit Sub
            End If
        End If
        Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as [Name] from TSPL_VLC_MASTER_HEAD  "
        fndVLCCode.Value = clsCommon.ShowSelectForm("VLC", qry, "Code", "Route_Code='" + fndRouteCode.Value + "'", fndVLCCode.Value, "Code", isButtonClicked)
        If clsCommon.myLen(fndVLCCode.Value) > 0 Then
            fndVLCName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_VLC_MASTER_HEAD.VLC_Name  from TSPL_VLC_MASTER_HEAD Where VLC_Code='" + fndVLCCode.Value + "'"))
            fndVSPCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_VLC_MASTER_HEAD.VSP_Code from TSPL_VLC_MASTER_HEAD Where VLC_Code='" + fndVLCCode.Value + "'"))
            fndVSPName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_VENDOR_MASTER.Vendor_Name  from TSPL_VLC_MASTER_HEAD left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VLC_MASTER_HEAD.VSP_Code Where VLC_Code='" + fndVLCCode.Value + "'"))
            LoadData(fndMccCode.Tag, fndRouteCode.Value, Nothing, NavigatorType.Current)
        Else
            fndVLCName.Text = ""
            fndVSPCode.Text = ""
            fndVSPName.Text = ""
        End If
    End Sub

    Private Sub chkMP_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMP.ToggleStateChanged
        If chkMP.Checked Then
            fndVLCCode.Visible = True
            fndVLCName.Visible = True
            fndVSPCode.Visible = True
            fndVSPName.Visible = True
            lblVLC.Visible = True
            lblVSP.Visible = True
            fndVLCCode.Value = ""
            LoadBlankGridforVLC()
        Else
            fndVLCCode.Visible = False
            fndVLCName.Visible = False
            fndVSPCode.Visible = False
            fndVSPName.Visible = False
            lblVLC.Visible = False
            lblVSP.Visible = False
            LoadBlankGridforRoute()
        End If
    End Sub
End Class

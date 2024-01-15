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
Public Class FrmMilkTruckSheet
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim sQuery As String = ""

    Const colSno As String = "colSno"
    Const colVLCCode As String = "colVLCCode"
    Const colVlcName As String = "colVlcName"
    Const colCons As String = "colCons"
    Const colSampleNo As String = "colSampleNo"
    Const colVlcDocCode As String = "colVlcDocCode"
    Const colQtyVLC As String = "colQtyVLC"
    Const colFATVLC As String = "colFATVLC"
    Const colSNFVLC As String = "colSNFVLC"
    Const colQtyMCC As String = "colQtyMCC"
    Const colFATMCC As String = "colFATMCC"
    Const colSNFMCC As String = "colSNFMCC"
    Const colRemarks As String = "colRemarks"

    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False





    Public Sub GetshiftType()
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

    Sub AddNew()
        ' isNewEntry = True
        fndDocCode.Value = ""
        'fndDocCode.Tag = Nothing
        fndMccCode.Value = ""
        lblMccName.Text = ""
        fndMccCode.Enabled = True
        txtArrivalTime.Value = clsCommon.GETSERVERDATE()
        txtUnloadingTime.Value = clsCommon.GETSERVERDATE()
        btnsave.Text = "Save"
        DtpDocDate.MinDate = Nothing
        gv1.DataSource = Nothing

        btnsave.Enabled = True
        BtnPost.Enabled = True
        btnDelete.Enabled = True
        fndMccCode.Enabled = True
        fndRouteCode.Enabled = True
        gv1.Rows.Clear()
        'gv1.Columns.Clear()

        '  Me.dtpDocDate.Value = clsCommon.GETSERVERDATE()
        ' Me.cboShift.SelectedIndex = -1
        Me.fndRouteCode.Value = Nothing 'clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        lblRouteName.Text = Nothing
        txtArrivalTime.Value = clsCommon.GETSERVERDATE()
        txtSuperViserName.Text = Nothing
        txtUnloadingTime.Value = clsCommon.GETSERVERDATE()
        txtVehicleNo.Text = Nothing
        txtVehicleNo.Enabled = False
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
            LoadBlankGrid()
            Dim obj As clsMilk_Truck_Sheet
            If clsCommon.myLen(strRoute_Code) <= 0 Then
                obj = clsMilk_Truck_Sheet.GetData(strDoc, navType, trans)
            Else
                obj = clsMilk_Truck_Sheet.GetData(strDoc, strRoute_Code, navType, trans)
            End If

            ' If clsCommon.myLen(obj.DOC_DATE) > 0 Then
            'DtpDocDate.Value = obj.DOC_DATE
            'End If
            'fndMccCode.Value = obj.MCC_CODE
            '  cboShift.SelectedValue = obj.SHIFT
            If Not IsNothing(obj) Then
                UsLock1.Status = obj.POSTED
                If clsCommon.myLen(strRoute_Code) <= 0 Then
                    fndDocCode.Value = obj.DOC_CODE
                    fndRouteCode.Value = obj.Route_Code
                    lblRouteName.Text = obj.Route_Name
                    If clsCommon.myLen(obj.DOC_CODE) > 0 Then
                        BtnSave.Text = "Update"
                        fndMccCode.Enabled = False
                        fndRouteCode.Enabled = False
                    End If
                    fndMccCode.Value = obj.MCC_CODE
                    lblMccName.Text = obj.MCC_NAME
                    fndMccCode.Tag = obj.MILK_RECEIPT_CODE
                    txtSuperViserName.Text = obj.Superviser_Name
                    txtVehicleNo.Text = obj.Vehicle_No
                    If clsCommon.myLen(obj.Mcc_Arival_Time) > 0 Then
                        txtArrivalTime.Value = obj.Mcc_Arival_Time
                    End If
                    If clsCommon.myLen(obj.UnLoading_Time) > 0 Then
                        txtUnloadingTime.Value = obj.UnLoading_Time
                    End If
                    If obj.POSTED = ERPTransactionStatus.Approved Then
                        BtnSave.Enabled = False
                        btnPost.Enabled = False
                        btnDelete.Enabled = False
                    End If
                End If
                'LoadBlankGrid()
                gv1.Rows.Clear()
                If (clsMilk_Truck_Sheet.ObjList IsNot Nothing AndAlso clsMilk_Truck_Sheet.ObjList.Count > 0) Then
                    For Each obj1 As clsMilk_Truck_SheetDetail In clsMilk_Truck_Sheet.ObjList
                        gv1.Rows.AddNew()

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSampleNo).Value = obj1.SAMPLE_NO
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVlcDocCode).Value = obj1.VLC_DOC_CODE
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCCode).Value = obj1.Vlc_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVlcName).Value = obj1.Vlc_name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCons).Value = obj1.Cans
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQtyVLC).Value = obj1.MILK_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATVLC).Value = obj1.FAT
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFVLC).Value = obj1.SNF
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQtyMCC).Value = obj1.MCC_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATMCC).Value = obj1.MCC_FAT
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFMCC).Value = obj1.MCC_SNF
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = obj1.Remarks
                    Next
                    UcAttachment1.LoadData(obj.DOC_CODE)
                Else
                    gv1.Rows.AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmMilkTruckSheet_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(BtnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                      "========Table Name=========" + Environment.NewLine + _
                      "Tspl_Milk_Truck_Sheet_Head" + Environment.NewLine + _
                      "Tspl_Milk_Truck_Sheet_detail" + Environment.NewLine + _
                      "tspl_Milk_receipt_Detail")
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.MilkTruckSheet)
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

    Private Sub FrmMilkTruckSheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadBlankGrid()

        GetshiftType()
        Me.fndMccCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER inner join tspl_mcc_master on mcc_code=Default_Location where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myCstr(fndMccCode.Value) <> "" Then
            Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(fndMccCode.Value)
            If DTShift Is Nothing OrElse DTShift.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No shift is opened. one Shift Must be Opened..", Me.Text)
                DtpDocDate.ReadOnly = True
                cboShift.Enabled = False
            ElseIf DTShift.Rows.Count > 1 Then
                clsCommon.MyMessageBoxShow(Me, "There are more then one shifts are opened.Only one Shift can be Opened..", Me.Text)
                DtpDocDate.ReadOnly = True
                cboShift.Enabled = False
            Else
                DtpDocDate.Value = DTShift.Rows(0).Item("MCC_Shift_date")
                cboShift.SelectedValue = DTShift.Rows(0).Item("Shift")
                DtpDocDate.ReadOnly = True
                cboShift.Enabled = False
                Dim qry As String = "select Distinct Doc_Code as Milk_receipt_code,Mcc_name from TSPL_MCC_ROUTE_MASTER inner join " _
                  & " TSPL_MILK_RECEIPT_DETAIL rh on rh.mcc_Code='" & clsCommon.myCstr(fndMccCode.Value) & "' and Shift='" & clsCommon.myCstr(cboShift.SelectedValue) & "' and" _
                  & " convert(date,Doc_Date,103)=convert(date,'" & clsCommon.myCDate(DtpDocDate.Value) & "',103) and rh.route_code=TSPL_MCC_ROUTE_MASTER.route_code " _
                  & " inner join tspl_mcc_master mm on mm.mcc_Code=rh.mcc_Code"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    fndMccCode.Tag = dt.Rows(0).Item("Milk_receipt_code")
                    lblMccName.Text = dt.Rows(0).Item("Mcc_name")
                End If

            End If
        End If
        txtArrivalTime.Value = clsCommon.GETSERVERDATE()
        txtUnloadingTime.Value = clsCommon.GETSERVERDATE()
        txtVehicleNo.Enabled = False

    End Sub
    Public Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoSNO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNO.FormatString = ""
        repoSNO.HeaderText = "SNO"
        repoSNO.Name = colSno
        repoSNO.Width = 50
        repoSNO.IsVisible = False
        repoSNO.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSNO)

        Dim repoVLCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVLCCode.FormatString = ""
        repoVLCCode.HeaderText = "VLC Code"
        repoVLCCode.Name = colVLCCode
        repoVLCCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoVLCCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoVLCCode.Width = 100
        repoVLCCode.IsVisible = True
        repoVLCCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVLCCode)



        Dim repoVLCName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVLCName.FormatString = ""
        repoVLCName.HeaderText = "VLC Name"
        repoVLCName.Name = colVlcName
        repoVLCName.Width = 200
        repoVLCName.IsVisible = True
        repoVLCName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVLCName)

        Dim repoCONS As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCONS.FormatString = ""
        repoCONS.HeaderText = "Cans"
        repoCONS.Name = colCons
        repoCONS.Width = 50
        repoCONS.IsVisible = True
        repoCONS.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCONS)

        Dim repoSampleNO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSampleNO.FormatString = ""
        repoSampleNO.HeaderText = "Sample No"
        repoSampleNO.Name = colSampleNo
        repoSampleNO.Width = 50
        repoSampleNO.IsVisible = True
        repoSampleNO.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSampleNO)

        Dim repovlc_Code_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovlc_Code_Code.FormatString = ""
        repovlc_Code_Code.HeaderText = "Vlc Doc Code"
        repovlc_Code_Code.Name = colVlcDocCode
        repovlc_Code_Code.Width = 100
        repovlc_Code_Code.IsVisible = True
        repovlc_Code_Code.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repovlc_Code_Code)

        Dim repoQtyVLC As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQtyVLC.FormatString = ""
        repoQtyVLC.HeaderText = "Qty(MCC)"
        repoQtyVLC.Name = colQtyVLC
        repoQtyVLC.Width = 0
        repoQtyVLC.IsVisible = False
        repoQtyVLC.ReadOnly = True
        repoQtyVLC.Minimum = 0
        repoQtyVLC.ShowUpDownButtons = False
        gv1.MasterTemplate.Columns.Add(repoQtyVLC)

        Dim repoFATVLC As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFATVLC.FormatString = ""
        repoFATVLC.HeaderText = "FAT(%)(MCC)"
        repoFATVLC.Name = colFATVLC
        repoFATVLC.Width = 0
        repoFATVLC.IsVisible = False
        repoFATVLC.ReadOnly = True
        repoFATVLC.Minimum = 0
        repoFATVLC.Maximum = 100
        repoFATVLC.ShowUpDownButtons = False
        gv1.MasterTemplate.Columns.Add(repoFATVLC)

        Dim repoSNFVLC As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFVLC.FormatString = ""
        repoSNFVLC.HeaderText = "SNF(%)(MCC)"
        repoSNFVLC.Name = colSNFVLC
        repoSNFVLC.Width = 0
        repoSNFVLC.Minimum = 0
        repoSNFVLC.Maximum = 100
        repoSNFVLC.IsVisible = False
        repoSNFVLC.ReadOnly = True
        repoSNFVLC.ShowUpDownButtons = False
        gv1.MasterTemplate.Columns.Add(repoSNFVLC)

        Dim repoqtyMcc As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoqtyMcc.FormatString = ""
        repoqtyMcc.HeaderText = "Qty(VLC)"
        repoqtyMcc.Name = colQtyMCC
        repoqtyMcc.Width = 80
        repoqtyMcc.IsVisible = True
        repoqtyMcc.ReadOnly = False
        repoqtyMcc.Minimum = 0
        repoqtyMcc.ShowUpDownButtons = False
        gv1.MasterTemplate.Columns.Add(repoqtyMcc)

        Dim repoFATMCC As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFATMCC.FormatString = ""
        repoFATMCC.HeaderText = "FAT(%)(VLC)"
        repoFATMCC.Name = colFATMCC
        repoFATMCC.Width = 80
        repoFATMCC.IsVisible = True
        repoFATMCC.ReadOnly = False
        repoFATMCC.Minimum = 0
        repoFATMCC.Maximum = 100
        repoFATMCC.ShowUpDownButtons = False
        gv1.MasterTemplate.Columns.Add(repoFATMCC)

        Dim repoSNFMCC As GridViewDecimalColumn = New GridViewDecimalColumn()
        'Dim repoSNFMCC As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNFMCC.FormatString = ""
        repoSNFMCC.HeaderText = "SNF(%)(VLC)"
        repoSNFMCC.Name = colSNFMCC
        repoSNFMCC.Width = 80
        repoSNFMCC.IsVisible = True
        repoSNFMCC.ReadOnly = False
        repoSNFMCC.Minimum = 0
        repoSNFMCC.Maximum = 100
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
        Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(fndMccCode.Value)
        If DTShift Is Nothing OrElse DTShift.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No shifts is opened.Atleats one Shift should be Opened..", Me.Text)
            BtnSave.Enabled = False
        ElseIf DTShift.Rows.Count > 1 Then
            clsCommon.MyMessageBoxShow(Me, "There are more then one shifts are opened.Only one Shift can be Opened..", Me.Text)
            Me.Close()
        Else
            BtnSave.Enabled = True
            DtpDocDate.Value = DTShift.Rows(0).Item("MCC_Shift_date")
            cboShift.SelectedValue = DTShift.Rows(0).Item("Shift")
            qry = "select Distinct Doc_Code as Milk_receipt_code,Mcc_name from TSPL_MCC_ROUTE_MASTER inner join " _
                  & " TSPL_MILK_RECEIPT_DETAIL rh on rh.mcc_Code='" & clsCommon.myCstr(fndMccCode.Value) & "' and Shift='" & clsCommon.myCstr(cboShift.SelectedValue) & "' and" _
                  & " convert(date,Doc_Date,103)=convert(date,'" & clsCommon.myCDate(DtpDocDate.Value) & "',103) and rh.route_code=TSPL_MCC_ROUTE_MASTER.route_code " _
                  & " inner join tspl_mcc_master mm on mm.mcc_Code=rh.mcc_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            fndMccCode.Tag = dt.Rows(0).Item("Milk_receipt_code")
            lblMccName.Text = dt.Rows(0).Item("Mcc_name")
        End If
        ' End If
    End Sub

    Private Sub fndRouteCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndRouteCode._MYValidating
        Try
            'and rh.route_code=TSPL_MCC_ROUTE_MASTER.route_code
            Dim qry As String = "select Distinct TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as [Route Description],rh.Vehicle_Code from TSPL_MCC_ROUTE_MASTER inner join " _
            & " TSPL_MILK_RECEIPT_DETAIL rh on rh.mcc_Code='" & clsCommon.myCstr(fndMccCode.Value) & "' and Shift='" & clsCommon.myCstr(cboShift.SelectedValue) & "' and convert(date,Doc_Date,103)=convert(date,'" & clsCommon.myCDate(DtpDocDate.Value) & "',103)  and coalesce(is_truck_sheet_uploaded,'F')='F'  and TSPL_MCC_ROUTE_MASTER.Route_Code=rh.ROUTE_CODE"
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ROTFND1", qry)

            If dr IsNot Nothing Then
                fndRouteCode.Value = clsCommon.myCstr(dr("code")) 'gv.CurrentRow.Cells(colRoutecode).Value
                lblRouteName.Text = clsCommon.myCstr(dr("Route Description")) 'gv.CurrentRow.Cells(colRoutename).Value
                LoadData(fndMccCode.Tag, fndRouteCode.Value, Nothing, NavigatorType.Current)
                txtVehicleNo.Text = clsCommon.myCstr(dr("Vehicle_Code"))
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
        '  Dim trans As SqlTransaction = Nothing
        Try
            If (AllowToSave()) Then
                ' trans = clsDBFuncationality.GetTransactin()
                Dim objHead As clsMilk_Truck_Sheet
                objHead = New clsMilk_Truck_Sheet
                objHead.DOC_CODE = clsCommon.myCstr(fndDocCode.Value)
                objHead.DOC_DATE = clsCommon.myCDate(DtpDocDate.Value)
                objHead.SHIFT = clsCommon.myCstr(Me.cboShift.SelectedValue)
                objHead.UnLoading_Time = clsCommon.myCstr(txtUnloadingTime.Value)
                objHead.MCC_CODE = clsCommon.myCstr(fndMccCode.Value)
                ' objHead.MACHINE_NO = clsCommon.myCstr(txtSerialNo.Text)
                objHead.Superviser_Name = clsCommon.myCstr(txtSuperViserName.Text)
                objHead.Vehicle_No = clsCommon.myCstr(txtVehicleNo.Text)
                objHead.Route_Code = clsCommon.myCstr(fndRouteCode.Text)
                objHead.MILK_RECEIPT_CODE = clsCommon.myCstr(fndMccCode.Tag)
                objHead.Mcc_Arival_Time = clsCommon.myCstr(txtArrivalTime.Value)
                objHead.Route_Code = clsCommon.myCstr(fndRouteCode.Value)

                Dim objList As New List(Of clsMilk_Truck_SheetDetail)

                Dim obj1 As clsMilk_Truck_SheetDetail
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colSampleNo).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(ColVlcDocCode).Value)) > 0 Then
                        obj1 = New clsMilk_Truck_SheetDetail()

                        obj1.DOC_CODE = fndDocCode.Value
                        obj1.SAMPLE_NO = clsCommon.myCdbl(grow.Cells(colSampleNo).Value)
                        obj1.VLC_DOC_CODE = clsCommon.myCstr(grow.Cells(colVlcDocCode).Value)
                        obj1.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        obj1.Vlc_Code = clsCommon.myCstr(grow.Cells(colVLCCode).Value)
                        obj1.SNF = clsCommon.myCstr(grow.Cells(colSNFVLC).Value)
                        obj1.FAT = clsCommon.myCstr(grow.Cells(colFATVLC).Value)
                        obj1.MILK_Qty = clsCommon.myCdbl(grow.Cells(colQtyVLC).Value)

                        obj1.MCC_SNF = clsCommon.myCdbl(grow.Cells(colSNFMCC).Value)
                        obj1.MCC_FAT = clsCommon.myCdbl(grow.Cells(colFATMCC).Value)
                        obj1.MCC_Qty = clsCommon.myCdbl(grow.Cells(colQtyMCC).Value)

                        obj1.Mcc_Code = clsCommon.myCstr(fndMccCode.Value)
                        obj1.Doc_Date = clsCommon.myCstr(DtpDocDate.Value)
                        obj1.Cans = clsCommon.myCdbl(grow.Cells(colCons).Value)
                        obj1.Milk_Receipt_Code = clsCommon.myCstr(objHead.MILK_RECEIPT_CODE)
                        obj1.Shift = clsCommon.myCstr(objHead.SHIFT)
                        objList.Add(obj1)
                    End If
                Next
                If clsMilk_Truck_Sheet.SaveData(objHead, objList) Then
                    ' trans.Commit()
                    UcAttachment1.SaveData(objHead.DOC_CODE)
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(objHead.DOC_CODE, "", Nothing, NavigatorType.Current)
                    fndRouteCode.Focus()
                End If
            End If
        Catch ex As Exception
            ' trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            '= KUNAL > TICKET : BM00000009575 =====
            If AllowFutureDateTransaction(DtpDocDate.Value, Nothing) = False Then
                DtpDocDate.Focus()
                Return False
            End If

            If BtnSave.Text = "Update" Then
                Dim strchk As String = "select Posted from TSPL_MILK_truck_Sheet_HEAD where DOC_COde='" + fndDocCode.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                    Return False
                End If
            End If
            If clsCommon.myLen(Me.cboShift.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter Shift", Me.Text)
                Return False
            End If

            If clsCommon.myLen(Me.fndMccCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter MCC", Me.Text)
                fndMccCode.Focus()
                Return False
            End If

            If clsCommon.myLen(Me.txtSuperViserName.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter SuperVisor Name", Me.Text)
                txtSuperViserName.Focus()
                Return False
            End If

            Dim grid_vlc_Count As Integer = 0
            For Each row As GridViewRowInfo In gv1.Rows
                grid_vlc_Count += 1
                If clsCommon.myCdbl(row.Cells(colQtyMCC).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Fill Qty(MCC) in Row No " & grid_vlc_Count & "", Me.Text)
                    gv1.Focus()
                    Return False
                End If
                If clsCommon.myCdbl(row.Cells(colSNFMCC).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Fill SNF(%)(MCC) in Row No " & grid_vlc_Count & "", Me.Text)
                    gv1.Focus()
                    Return False
                End If
                If clsCommon.myCdbl(row.Cells(colFATMCC).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Fill FAT(%)(MCC) in Row No " & grid_vlc_Count & "", Me.Text)
                    gv1.Focus()
                    Return False
                End If
            Next

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
                If (clsMilk_Truck_Sheet.DeleteData(fndDocCode.Value)) Then
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
            fndDocCode.Value = clsMilk_Truck_Sheet.getFinder(whrcls, fndDocCode.Value, isButtonClicked)
            'If Not IsNothing(dr) Then
            '    fndDocCode.Value = dr("Code")
            '    fndDocCode.Tag = dr("Milk Receipt Code")
            'Else
            '    fndDocCode.Value = Nothing
            '    fndDocCode.Tag = Nothing
            'End If
           
            If clsCommon.myLen(fndDocCode.Value) > 0 Then
                LoadData(fndDocCode.Value, "", Nothing, NavigatorType.Current)
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
                If (clsMilk_Truck_Sheet.PostData(fndDocCode.Value, True)) Then
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
                common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                LoadData(fndDocCode.Value, "", Nothing, NavigatorType.Current)
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
            common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
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
            MessageBox.Show(Me, err.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles gv1.KeyDown
        Try
            Dim objrandom As New Random
            Dim iQtyindex As Integer = objrandom.Next(3, 5)
            Dim iFatindex As Integer = objrandom.Next(1, 5)
            Dim iSNfindex As Integer = objrandom.Next(1, 5)
            If e.Control And e.Alt And e.KeyCode = Keys.B Then
                For Each row As GridViewRowInfo In gv1.Rows
                    If row.Index Mod iQtyindex = 0 Then
                        row.Cells(colQtyMCC).Value = objrandom.Next(clsCommon.myCdbl(row.Cells(colQtyVLC).Value) - 1, clsCommon.myCdbl(row.Cells(colQtyVLC).Value) + 1)
                    Else
                        row.Cells(colQtyMCC).Value = clsCommon.myCdbl(row.Cells(colQtyVLC).Value)
                    End If
                    If row.Index Mod iFatindex = 0 Then
                        row.Cells(colFATMCC).Value = objrandom.Next(clsCommon.myCdbl(row.Cells(colFATVLC).Value) - 0.2, clsCommon.myCdbl(row.Cells(colFATVLC).Value) + 0.2)
                    Else
                        row.Cells(colFATMCC).Value = clsCommon.myCdbl(row.Cells(colFATVLC).Value)
                    End If
                    If row.Index Mod iSNfindex = 0 Then
                        row.Cells(colSNFMCC).Value = objrandom.Next(clsCommon.myCdbl(row.Cells(colSNFVLC).Value) - 0.2, clsCommon.myCdbl(row.Cells(colSNFVLC).Value) + 0.2)
                    Else
                        row.Cells(colSNFMCC).Value = clsCommon.myCdbl(row.Cells(colSNFVLC).Value)
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class

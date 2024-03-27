
'--------Created By Richa 16/07/2016 Against Ticket No 
Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class FrmGatePassTransfer
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim isFlag As Boolean = False
    Dim arrLoc As String = Nothing
    Public Shared aLoc As String = Nothing
    Public Const colSlNo As String = "SLNO"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colUnitCode As String = "colUnitCode"
    Public Const colQty As String = "Qty"
    Public Const colRemarks As String = "colRemarks"
    Dim Qry As String
    Public strDocumentCode As String = Nothing

#End Region
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmGatePassTransfer_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        End If
    End Sub
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try

            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                Dim LocationName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code from TSPL_LOCATION_MASTER where Type ='PLANT' AND Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' and Location_Code ='" & obj.Default_LocCode & "'"))
                If clsCommon.myLen(LocationName) > 0 Then
                    FndLocationCode.Value = obj.Default_LocCode
                    LblLocationName.Text = obj.Default_LocName

                Else
                    FndLocationCode.Value = ""
                    LblLocationName.Text = ""
                End If
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
                aLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub FrmGatePassTransfer_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N New Transaction")

    End Sub

    Private Sub fndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocationCode._MYValidating
        Dim whrcls As String = " Type ='PLANT' AND Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N'  "
        If clsCommon.myLen(arrLoc) > 0 Then
            whrcls += " and TSPL_LOCATION_MASTER.Location_Code in (" + arrLoc + ") "
        End If
        fndLocationCode.Value = clsLocation.getFinder(whrcls, fndLocationCode.Value, isButtonClicked)
        If clsCommon.myLen(fndLocationCode.Value) > 0 Then
            lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocationCode.Value & "'"))
        Else
            lblLocationName.Text = ""
        End If

    End Sub
    Private Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.GatePassTransfer)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        ' btnPost.Visible = MyBase.isPostFlag
    End Sub

    Sub loadBlankItemGrid()

        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing

        Dim lineNo As New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "SL. No."
        lineNo.Name = colSlNo
        lineNo.Width = 60
        lineNo.ReadOnly = True
        lineNo.WrapText = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(lineNo)


        Dim itemCode As New GridViewTextBoxColumn()
        itemCode.FormatString = ""
        itemCode.HeaderText = "Product Code"
        itemCode.Name = colItemCode
        itemCode.Width = 100
        itemCode.ReadOnly = True
        itemCode.WrapText = True
        itemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(itemCode)


        Dim itemDesc As New GridViewTextBoxColumn()
        itemDesc.FormatString = ""
        itemDesc.HeaderText = "Product Name"
        itemDesc.Name = colItemDesc
        itemDesc.Width = 300
        itemDesc.ReadOnly = True
        itemDesc.WrapText = True
        itemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(itemDesc)

        Dim strUnitCode As New GridViewTextBoxColumn()
        strUnitCode.FormatString = ""
        strUnitCode.HeaderText = "UOM"
        strUnitCode.Name = colUnitCode
        strUnitCode.Width = 120
        strUnitCode.ReadOnly = True
        strUnitCode.WrapText = True
        strUnitCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(strUnitCode)

        Dim Qty As New GridViewDecimalColumn
        Qty.FormatString = "{0:n3}"
        Qty.DecimalPlaces = 3
        Qty.HeaderText = "No."
        Qty.Name = colQty
        Qty.Width = 120
        Qty.ReadOnly = False
        Qty.WrapText = True
        Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Qty)

        Dim Remarks As New GridViewTextBoxColumn()
        Remarks.FormatString = ""
        Remarks.HeaderText = "Remarks"
        Remarks.Name = colRemarks
        Remarks.Width = 320
        Remarks.ReadOnly = False
        Remarks.WrapText = True
        Remarks.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(Remarks)

        gv1.Rows.AddNew()
        gv1.Rows(0).Cells(colSlNo).Value = "1"
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.AllowRowReorder = False
        ' gv1.ShowGroupPanel = False
        gv1.EnableFiltering = False
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        gv1.AllowColumnReorder = True
        ' gv1.AllowAddNewRow = False
        ' gv1.ShowGroupPanel = False
        ' gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        ' gv1.TableElement.TableHeaderHeight = 40

        lineNo = Nothing
        itemCode = Nothing
        itemDesc = Nothing
        strUnitCode = Nothing
        Remarks = Nothing

    End Sub
    Sub Reset()
        txtDocNo.Value = ""
        fndLocationCode.Value = ""
        lblLocationName.Text = ""
        FndBranch.Value = ""
        lblBranchName.Text = ""
        FndBooking.Value = ""
        FndVehicleCode.Value = ""
        LblVehicleName.Text = ""


        txtCrate.Text = 0
        txtJaali.Text = 0
        txtBox.Text = 0

        TxtTotalQty.Value = 0
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDate.Value = clsCommon.GETSERVERDATE()
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
        If DateTime = "1" Then
            txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            txtDate.CustomFormat = "dd/MM/yyyy"
        End If
        txtDocNo.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnPost.Enabled = True
        btnsave.Enabled = True
        loadBlankItemGrid()
        ' ReStoreGridLayout()
        isNewEntry = True

        LOCATIONRIGTHS()
    End Sub

    Private Sub FndBooking__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndBooking._MYValidating
        ' Qry = "sELECT Document_No as [Code] ,CONVERT(VARCHAR,Document_Date,103) AS Date  FROM TSPL_BOOKING_MATSER "

        '  FndBooking.Value = clsCommon.ShowSelectForm("GPTBooking", Qry, "Code", " Posted=1 and Document_No not in ( Select Booking_Code from Tspl_GatePass_Transfer_Head)", FndBooking.Value, "Code", isButtonClicked)

        Qry = " sELECT distinct TSPL_BOOKING_DETAIL.Document_No as [Code] ,CONVERT(VARCHAR,TSPL_BOOKING_MATSER.Document_Date,103) AS Date,tspl_location_master.location_desc as [Location],tspl_location_master.loc_short_name as [Location Short Name],tspl_vehicle_master.[description] as Vehicle  FROM TSPL_BOOKING_DETAIL" & _
        " Left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No=TSPL_BOOKING_MATSER.Document_No left outer join tspl_location_master on TSPL_BOOKING_DETAIL.Loc_Code =TSPL_LOCATION_MASTER.Location_Code " & _
        " Left Outer Join TSPL_GATEPASS_TRANSFER_HEAD on TSPL_GATEPASS_TRANSFER_HEAD.Booking_Code =TSPL_BOOKING_DETAIL.Document_No left outer join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_BOOKING_DETAIL.Vehicle_Code "

        FndBooking.Value = clsCommon.ShowSelectForm("GPTBooking", Qry, "Code", " TSPL_BOOKING_MATSER.Posted=1 and TSPL_BOOKING_DETAIL.Vehicle_Code not in (Select Vehicle_Code  from TSPL_GATEPASS_TRANSFER_HEAD where TSPL_GATEPASS_TRANSFER_HEAD.Booking_Code= TSPL_BOOKING_MATSER.Document_No) and TSPL_BOOKING_MATSER.Document_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'", FndBooking.Value, "Code", isButtonClicked)

        If clsCommon.myLen(FndBooking.Value) > 0 Then
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select TSPL_BOOKING_DETAIL.Loc_Code,TSPL_LOCATION_MASTER.Location_Desc  from TSPL_BOOKING_DETAIL left outer join TSPL_LOCATION_MASTER on TSPL_BOOKING_DETAIL.Loc_Code =TSPL_LOCATION_MASTER.Location_Code where TSPL_BOOKING_DETAIL.Document_No  ='" + FndBooking.Value + "' ")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FndBranch.Value = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
                lblBranchName.Text = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            End If

            'FndBranch.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("sELECT Loc_Code FROM TSPL_BOOKING_DETAIL where Document_No  ='" + FndBooking.Value + "' "))
            'lblBranchName.Text = clsLocation.GetName(FndBranch.Value, Nothing)
        Else
            FndBranch.Value = ""
            lblBranchName.Text = ""
        End If
    End Sub

    Private Sub FndVehicleCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndVehicleCode._MYValidating
        If clsCommon.myLen(FndBooking.Value) > 0 Then
            '  Qry = " sELECT distinct TSPL_BOOKING_DETAIL.Vehicle_Code as Code,TSPL_VEHICLE_MASTER.Description FROM TSPL_BOOKING_DETAIL  Left Outer Join TSPL_VEHICLE_MASTER on TSPL_BOOKING_DETAIL.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id  "
            Qry = "sELECT distinct TSPL_BOOKING_DETAIL.Vehicle_Code as Code,TSPL_VEHICLE_MASTER.Description FROM TSPL_BOOKING_DETAIL  Left Outer Join TSPL_VEHICLE_MASTER on TSPL_BOOKING_DETAIL.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id  "

            FndVehicleCode.Value = clsCommon.ShowSelectForm("GPTVehicle", Qry, "Code", " TSPL_BOOKING_DETAIL.Vehicle_Code not in (Select Vehicle_Code  from TSPL_GATEPASS_TRANSFER_HEAD where Booking_Code ='" & FndBooking.Value & "') and TSPL_BOOKING_DETAIL .Document_No ='" & FndBooking.Value & "' ", FndVehicleCode.Value, "Code", isButtonClicked)
            If clsCommon.myLen(FndVehicleCode.Value) > 0 Then
                loadbookingDetail(FndBooking.Value, FndVehicleCode.Value)
            Else
                LblVehicleName.Text = ""
                loadBlankItemGrid()
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "Please Select Booking No. First", Me.Text)
        End If


    End Sub
    Sub loadbookingDetail(ByVal BookingNo As String, ByVal vehicleno As String)
        loadBlankItemGrid()
        ' Qry = "sELECT TSPL_BOOKING_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc ,TSPL_BOOKING_DETAIL.Unit_code , TSPL_BOOKING_DETAIL.Booking_Qty,TSPL_VEHICLE_MASTER.Description as vehiclename   FROM TSPL_BOOKING_DETAIL Left Outer Join TSPL_ITEM_MASTER on TSPL_BOOKING_DETAIL .Item_Code =TSPL_ITEM_MASTER .Item_Code " & _
        '" left outer join TSPL_VEHICLE_MASTER on  TSPL_VEHICLE_MASTER .Vehicle_Id ='" & vehicleno & "' where Document_No ='" & BookingNo & "'  and Vehicle_Code ='" & vehicleno & "' "

        Qry = " Select z.Item_Code,max(z.Item_Desc ) as Item_Desc,z.Unit_code,sum(z.Booking_Qty) as Booking_Qty,max(z.vehiclename) as vehiclename  from " & _
        " (sELECT TSPL_BOOKING_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc ,TSPL_BOOKING_DETAIL.Unit_code , TSPL_BOOKING_DETAIL.Booking_Qty,TSPL_VEHICLE_MASTER.Description as vehiclename " & _
        " FROM TSPL_BOOKING_DETAIL Left Outer Join TSPL_ITEM_MASTER on TSPL_BOOKING_DETAIL .Item_Code =TSPL_ITEM_MASTER .Item_Code  " & _
        " left outer join TSPL_VEHICLE_MASTER on  TSPL_VEHICLE_MASTER .Vehicle_Id ='" & vehicleno & "' where Document_No ='" & BookingNo & "'  and Vehicle_Code ='" & vehicleno & "'  and Booking_Status=4  " & _
        "  )z group by Item_Code ,Unit_code "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            LblVehicleName.Text = clsCommon.myCstr(dt.Rows(0)("vehiclename"))
            For i As Integer = 0 To dt.Rows.Count - 1
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = gv1.Rows.Count
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dt.Rows(i)("Item_Desc"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = clsCommon.myCstr(dt.Rows(i)("Unit_code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dt.Rows(i)("Booking_Qty"))
                gv1.Rows.AddNew()
            Next
            CalculateTotalQuantity()
        End If
    End Sub
    Sub PostData()
        Try
            isFlag = True
            If (myMessages.postConfirm()) Then

                SaveData()
                If (ClsGatePassTransfer.PostData(MyBase.Form_ID, arrLoc, txtDocNo.Value)) Then
                    Throw New Exception("Successfully posted")
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
        End Try
    End Sub


    Private Function AllowToSave() As Boolean

        RadPageView1.SelectedPage = RadPageViewPage1
        If clsCommon.myLen(FndBooking.Value) <= 0 Then
            FndBooking.Focus()
            Throw New Exception("Booking No. cannot be left blank")
        End If
        If clsCommon.myLen(FndVehicleCode.Value) <= 0 Then
            FndVehicleCode.Focus()
            Throw New Exception("Vehicle No cannot be left blank")
        End If
        If clsCommon.myLen(fndLocationCode.Value) <= 0 Then
            fndLocationCode.Focus()
            Throw New Exception("Location cannot be left blank")
        End If

        Dim strCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(GatePassNo) from TSPL_TRANSFER_ORDER_DETAIL where isnull(GatePassNo ,'')='" & clsCommon.myCstr(txtDocNo.Value) & "'"))
        If strCount > 0 Then
            Throw New Exception("Gate Pass cannot be updated as it is used on transfer.")
        End If

        ''==================0 allowed
        If txtCrate.Text Is Nothing OrElse clsCommon.myLen(txtCrate.Text) < 0 Then
            txtCrate.Focus()
            txtCrate.Select()
            Throw New Exception("Please fill Crate value either 0 or any nnumber.")
        End If

        If txtBox.Text Is Nothing OrElse clsCommon.myLen(txtBox.Text) < 0 Then
            txtBox.Focus()
            txtBox.Select()
            Throw New Exception("Please fill Box value either 0 or any nnumber.")
        End If

        If txtJaali.Text Is Nothing OrElse clsCommon.myLen(txtJaali.Text) < 0 Then
            txtJaali.Focus()
            txtJaali.Select()
            Throw New Exception("Please fill Jaali value either 0 or any nnumber.")
        End If
        '===========================================

        'For i As Integer = 0 To gv1.Rows.Count - 1
        '    If clsCommon.myLen(gv1.Rows(i).Cells(colItemCode).Value) <= 0 Then
        '        Throw New Exception("Item Code cannot be left blank or zero")
        '    End If
        '    If clsCommon.myLen(gv1.Rows(i).Cells(colUnitCode).Value) <= 0 Then
        '        Throw New Exception("UOM cannot be left blank or zero")
        '    End If
        '    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells(colUnitCode).Value), "KG") <> CompairStringResult.Equal Then
        '        Throw New Exception("UOM should be in KG only")
        '    End If
        '    If clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value) < 0 Then
        '        Throw New Exception("Qty cannot be negative")
        '    End If
        '    If clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value) = 0 Then
        '        Throw New Exception("Qty cannot be left blank or zero")
        '    End If
        '    If clsCommon.myCdbl(gv1.Rows(i).Cells(colFatPer).Value) < 0 Then
        '        Throw New Exception("Fat% cannot be negative")
        '    End If
        '    If clsCommon.myCdbl(gv1.Rows(i).Cells(colFatPer).Value) = 0 Then
        '        Throw New Exception("Fat% cannot be left blank or zero")
        '    End If
        '    If clsCommon.myCdbl(gv1.Rows(i).Cells(colSNFPer).Value) < 0 Then
        '        Throw New Exception("SNF% cannot be negative")
        '    End If
        '    If clsCommon.myCdbl(gv1.Rows(i).Cells(colSNFPer).Value) = 0 Then
        '        Throw New Exception("SNF% cannot be left blank or zero")
        '    End If


        'Next

        Return True
    End Function

    Sub SaveData()
        Dim obj As New ClsGatePassTransfer
        Dim objTr As New ClsGatePassTransferDetail
        Try
            If AllowToSave() Then
                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Location_Code = fndLocationCode.Value
                obj.Booking_Code = FndBooking.Value
                obj.Branch_Code = FndBranch.Value
                obj.Vehicle_Code = FndVehicleCode.Value
                obj.TotalQty = clsCommon.myCdbl(TxtTotalQty.Value)

                obj.No_of_Jaali = clsCommon.myCdbl(txtJaali.Text)
                obj.No_of_Crate = clsCommon.myCdbl(txtCrate.Text)
                obj.No_of_Box = clsCommon.myCdbl(txtBox.Text)

                obj.arrGatePassTransferDetail = New List(Of ClsGatePassTransferDetail)

                For Each grow As GridViewRowInfo In gv1.Rows
                    objTr = New ClsGatePassTransferDetail()
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                        objTr.Document_No = clsCommon.myCstr(obj.Document_No)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                        objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        obj.arrGatePassTransferDetail.Add(objTr)
                    End If
                Next

                If (ClsGatePassTransfer.SaveData(obj, isNewEntry)) Then
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                        LoadData(obj.Document_No, NavigatorType.Current)
                    End If

                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
            objTr = Nothing
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsGatePassTransfer = Nothing
        Dim dt As DataTable = Nothing
        Reset()
        Try
            obj = ClsGatePassTransfer.GetData(strCode, arrLoc, NavTyep)

            isInsideLoadData = True
            If obj IsNot Nothing Then
                isNewEntry = False
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                fndLocationCode.Value = obj.Location_Code
                lblLocationName.Text = obj.Location_Name
                FndBooking.Value = obj.Booking_Code
                FndBranch.Value = obj.Branch_Code
                lblBranchName.Text = obj.Branch_Name
                TxtTotalQty.Value = obj.TotalQty

                txtJaali.Text = obj.No_of_Jaali
                txtCrate.Text = obj.No_of_Crate
                txtBox.Text = obj.No_of_Box

                FndVehicleCode.Value = obj.Vehicle_Code
                LblVehicleName.Text = obj.Vehicle_Name


                If obj.arrGatePassTransferDetail IsNot Nothing AndAlso obj.arrGatePassTransferDetail.Count > 0 Then
                    For Each objTr As ClsGatePassTransferDetail In obj.arrGatePassTransferDetail

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = gv1.Rows.Count
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        gv1.Rows.AddNew()
                    Next
                Else
                    gv1.DataSource = Nothing
                End If
                txtDocNo.MyReadOnly = True
                btnsave.Text = "Update"


                If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                    btnPost.Enabled = False
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                Else
                    btnPost.Enabled = True
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
            Else
                Reset()

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            obj = Nothing
            dt = Nothing
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Private Sub DeleteData()
        Try
            Dim strCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(GatePassNo) from TSPL_TRANSFER_ORDER_DETAIL where isnull(GatePassNo ,'')='" & clsCommon.myCstr(txtDocNo.Value) & "'"))
            If strCount > 0 Then
                Throw New Exception("Gate Pass cannot be deleted as it is used on transfer.")
            End If

            If (deleteConfirm()) Then
                If (ClsGatePassTransfer.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_GATEPASS_TRANSFER_HEAD where Document_No='" + txtDocNo.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocNo.MyReadOnly = False
            End If

            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "SELECT TSPL_GATEPASS_TRANSFER_HEAD.Document_No as Code,Convert(varchar,TSPL_GATEPASS_TRANSFER_HEAD.Document_Date,103) As Date,TSPL_GATEPASS_TRANSFER_HEAD.Booking_Code as [Booking Code],TSPL_GATEPASS_TRANSFER_HEAD.Vehicle_Code as [Vehicle Code],TSPL_VEHICLE_MASTER.Description as [Vehicle Name],TSPL_GATEPASS_TRANSFER_HEAD.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_GATEPASS_TRANSFER_HEAD.Branch_Code as [Branch Code],BRANCHcODE.Location_Desc AS [Branch Name],case when TSPL_GATEPASS_TRANSFER_HEAD.Posted=0 then 'Pending' else 'Approved' end as Status FROM TSPL_GATEPASS_TRANSFER_HEAD LEFT OUTER JOIN TSPL_VEHICLE_MASTER on TSPL_GATEPASS_TRANSFER_HEAD.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id lEFT oUTER JOIN TSPL_LOCATION_MASTER ON TSPL_GATEPASS_TRANSFER_HEAD.Location_Code=TSPL_LOCATION_MASTER.Location_Code lEFT oUTER JOIN TSPL_LOCATION_MASTER AS BRANCHcODE ON TSPL_GATEPASS_TRANSFER_HEAD.Branch_Code=BRANCHcODE.Location_Code"
        Dim whrcls As String = ""
        If clsCommon.myLen(arrLoc) > 0 Then
            whrcls = " TSPL_GATEPASS_TRANSFER_HEAD.Location_Code in (" & arrLoc & ")"
        End If
        txtDocNo.Value = clsCommon.ShowSelectForm("GatePassTransfer", qry, "Code", whrcls, txtDocNo.Value, "", isButtonClicked)
        LoadData(txtDocNo.Value, NavigatorType.Current)
        qry = Nothing
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Reset()
    End Sub
    Public Function GetQuery() As String
        Dim Qry As String = "select TSPL_GATEPASS_TRANSFER_HEAD.branch_code,branck_loc.location_desc as Branch_Name, TSPL_COMPANY_MASTER.logo_img, TSPL_COMPANY_MASTER.comp_code,TSPL_COMPANY_MASTER.comp_Name,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end " & _
        " + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ', TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end " & _
        " as Comp_Add,TSPL_COMPANY_MASTER.cinno as Comp_CinNo,TSPL_COMPANY_MASTER.ServiceTax_Reg_No +case when len(TSPL_COMPANY_MASTER.Ecc_No)>0 then '/ '+TSPL_COMPANY_MASTER.Ecc_No else '' end as Comp_ECC_No,TSPL_COMPANY_MASTER.CE_Range as Comp_CE_Range,TSPL_COMPANY_MASTER.CE_Division as GP_Division,TSPL_GATEPASS_TRANSFER_head.location_code,tspl_location_master.location_Desc,tspl_location_master.add1 +case when len(tspl_location_master.add2)>0 then ', '+tspl_location_master.add2 else '' end +case when LEN(isnull(tspl_location_master.Add3,''))>0 then ', '+isnull(tspl_location_master.Add3,'') else ' ' end " & _
        " + case when LEN(TSPL_STATE_MASTER.STATE_NAME)>0 then ', '+TSPL_STATE_MASTER.STATE_NAME else ' ' end " & _
        " as Loc_Add,tspl_location_master.tin_no as Loc_TinNo,TSPL_GATEPASS_TRANSFER_head.vehicle_code,TSPL_VEHICLE_MASTER.description as vehicle_Name,convert(varchar,TSPL_GATEPASS_TRANSFER_head.Document_Date,103)  as Document_Date,TSPL_GATEPASS_TRANSFER_head.Document_No ,TSPL_GATEPASS_TRANSFER_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc ,TSPL_GATEPASS_TRANSFER_DETAIL.Unit_code ,TSPL_GATEPASS_TRANSFER_DETAIL.Qty ,TSPL_GATEPASS_TRANSFER_head.Booking_Code ,TSPL_GATEPASS_TRANSFER_DETAIL.remarks " & _
        " from TSPL_GATEPASS_TRANSFER_DETAIL" & _
        " left join TSPL_GATEPASS_TRANSFER_head on TSPL_GATEPASS_TRANSFER_head.document_no=TSPL_GATEPASS_TRANSFER_DETAIL.document_no" & _
        " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.comp_code=TSPL_GATEPASS_TRANSFER_head.comp_code" & _
        " left join tspl_location_master on tspl_location_master.location_code=TSPL_GATEPASS_TRANSFER_head.location_code" & _
        " left join TSPL_STATE_MASTER on tspl_location_master.State =TSPL_STATE_MASTER.STATE_CODE " & _
        " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.vehicle_id=TSPL_GATEPASS_TRANSFER_head.vehicle_code" & _
        " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_GATEPASS_TRANSFER_DETAIL.Item_Code " & _
        " left join TSPL_LOCATION_MASTER  branck_loc on branck_loc.Location_code=TSPL_GATEPASS_TRANSFER_HEAD.branch_code"



        Return Qry
    End Function
    Public Sub funPrint(ByVal strDocNo As String)
        Try
            Dim Qry As String = GetQuery()
            Qry += " where TSPL_GATEPASS_TRANSFER_head.document_no ='" + strDocNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptTransferGatePass", "Gate Pass", "rptCompanyAddress.rpt")
                frmCRV = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            myMessages.blankValue(Me, "No data found to Print", Me.Text)
        Else
            funPrint(txtDocNo.Value)
        End If
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is gv1.Columns(colQty) Then
                    CalculateTotalQuantity()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isCellValueChangedOpen = False
        End Try
    End Sub
    Private Sub CalculateTotalQuantity()
        Try
            Dim Qty As Double = 0
            For i As Integer = 0 To gv1.Rows.Count - 1
                Qty = Qty + clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value)
            Next
            TxtTotalQty.Value = clsCommon.myCdbl(Qty)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
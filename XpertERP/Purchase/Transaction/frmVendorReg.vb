Imports common
Imports System
Imports System.IO
Public Class FrmVendorReg
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ColLineNo As String = "S.No"
    Const ColDescription As String = "DESCRIPTION"
    Const ColMakeModel As String = "MAKEMODEL"
    Const ColNoOfCS As String = "NOOFCS"
    Const ColYear As String = "YEAR"
    Dim ofd As New OpenFileDialog()
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.VendorRegistration)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadBlankGridForManufacturingEquipment()
        gvmanufacturing.Rows.Clear()
        gvmanufacturing.Columns.Clear()

        Dim repoRow As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRow = New GridViewTextBoxColumn()
        repoRow.FormatString = ""
        repoRow.HeaderText = "S.No"
        repoRow.Name = ColLineNo
        repoRow.Width = 150
        repoRow.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvmanufacturing.MasterTemplate.Columns.Add(repoRow)

        repoRow = New GridViewTextBoxColumn()
        repoRow.FormatString = ""
        repoRow.HeaderText = "DESCRIPTION"
        repoRow.Name = ColDescription
        repoRow.Width = 150
        repoRow.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvmanufacturing.MasterTemplate.Columns.Add(repoRow)

        repoRow = New GridViewTextBoxColumn()
        repoRow.FormatString = ""
        repoRow.HeaderText = "MAKE & MODEL"
        repoRow.Name = ColMakeModel
        repoRow.Width = 150
        repoRow.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvmanufacturing.MasterTemplate.Columns.Add(repoRow)

        repoRow = New GridViewTextBoxColumn()
        repoRow.FormatString = ""
        repoRow.HeaderText = "No. Of M/CS."
        repoRow.Name = ColNoOfCS
        repoRow.Width = 150
        repoRow.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvmanufacturing.MasterTemplate.Columns.Add(repoRow)

        repoRow = New GridViewTextBoxColumn()
        repoRow.FormatString = ""
        repoRow.HeaderText = "Year of Purchase"
        repoRow.Name = ColYear
        repoRow.Width = 150
        repoRow.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvmanufacturing.MasterTemplate.Columns.Add(repoRow)

        gvmanufacturing.AllowDeleteRow = True
        gvmanufacturing.AllowAddNewRow = True
        gvmanufacturing.AllowColumnReorder = False
        gvmanufacturing.AllowRowReorder = False
        gvmanufacturing.EnableSorting = False
        gvmanufacturing.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvmanufacturing.MasterTemplate.ShowRowHeaderColumn = False
        ReStoreGridLayout()
    End Sub

    Sub LoadBlankGridForTestingEquipment()
        gvtesting.Rows.Clear()
        gvtesting.Columns.Clear()

        Dim repoRow As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRow = New GridViewTextBoxColumn()
        repoRow.FormatString = ""
        repoRow.HeaderText = "S.No"
        repoRow.Name = ColLineNo
        repoRow.Width = 150
        repoRow.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvtesting.MasterTemplate.Columns.Add(repoRow)

        repoRow = New GridViewTextBoxColumn()
        repoRow.FormatString = ""
        repoRow.HeaderText = "DESCRIPTION"
        repoRow.Name = ColDescription
        repoRow.Width = 150
        repoRow.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvtesting.MasterTemplate.Columns.Add(repoRow)

        repoRow = New GridViewTextBoxColumn()
        repoRow.FormatString = ""
        repoRow.HeaderText = "MAKE & MODEL"
        repoRow.Name = ColMakeModel
        repoRow.Width = 150
        repoRow.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvtesting.MasterTemplate.Columns.Add(repoRow)

        repoRow = New GridViewTextBoxColumn()
        repoRow.FormatString = ""
        repoRow.HeaderText = "No. Of Inst."
        repoRow.Name = ColNoOfCS
        repoRow.Width = 150
        repoRow.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvtesting.MasterTemplate.Columns.Add(repoRow)

        repoRow = New GridViewTextBoxColumn()
        repoRow.FormatString = ""
        repoRow.HeaderText = "Year of Purchase"
        repoRow.Name = ColYear
        repoRow.Width = 150
        repoRow.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvtesting.MasterTemplate.Columns.Add(repoRow)

        gvtesting.AllowDeleteRow = True
        gvtesting.AllowAddNewRow = True
        gvtesting.AllowColumnReorder = False
        gvtesting.AllowRowReorder = False
        gvtesting.EnableSorting = False
        gvtesting.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvtesting.MasterTemplate.ShowRowHeaderColumn = False
        ReStoreGridLayout()
    End Sub

    Private Sub FrmVendorReg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadBlankGridForManufacturingEquipment()
        LoadBlankGridForTestingEquipment()
    End Sub

    

    Private Sub ReStoreGridLayout()
        'Try
        '    If clsCommon.myLen(ReportID) > 0 Then
        '        Dim obj As clsGridLayout = New clsGridLayout()
        '        obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
        '        If Not obj Is Nothing AndAlso obj.GridColumns >= gvmanufacturing.ColumnCount Then
        '            Dim ii As Integer
        '            For ii = 0 To gvmanufacturing.Columns.Count - 1 Step ii + 1
        '                gvmanufacturing.Columns(ii).IsVisible = False
        '                gvmanufacturing.Columns(ii).VisibleInColumnChooser = True
        '            Next

        '            gvmanufacturing.LoadLayout(obj.GridLayout)
        '            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        '        End If
        '    End If
        'Catch err As Exception
        '    MessageBox.Show(err.Message)
        'End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        'If clsCommon.myLen(ReportID) > 0 Then
        '    gvmanufacturing.MasterTemplate.FilterDescriptors.Clear()
        '    Dim obj As New clsGridLayout()
        '    obj.ReportID = ReportID
        '    obj.UserID = objCommonVar.CurrentUserCode
        '    obj.GridLayout = New MemoryStream()
        '    gvmanufacturing.SaveLayout(obj.GridLayout)
        '    obj.UserID = objCommonVar.CurrentUserCode
        '    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        '    obj.GridColumns = gvmanufacturing.ColumnCount
        '    If obj.SaveData() Then
        '        common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
        '    End If
        ''stuti regarding memory leakage
        'obj.GridLayout.Close()
        'obj.GridLayout.Dispose()
        'End If
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        'If clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode) AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
        '    common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
        'End If
    End Sub

    Private Sub btnselectsign_approved_Click(sender As Object, e As EventArgs) Handles btnselectsign_approved.Click, btnselectsign_assessor.Click, btnselectsign_vendor.Click
        ofd.Filter = " image |*.gif;  *.jpg;  *.bmp;  *.png;"
        ofd.ShowDialog()
        If clsCommon.CompairString(clsCommon.myCstr(sender.Name), "btnselectsign_approved") = CompairStringResult.Equal Then
            pb_approved.Image = Image.FromFile(ofd.FileName)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(sender.Name), "btnselectsign_assessor") = CompairStringResult.Equal Then
            pb_assessor.Image = Image.FromFile(ofd.FileName)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(sender.Name), "btnselectsign_vendor") = CompairStringResult.Equal Then
            pb_vendor.Image = Image.FromFile(ofd.FileName)
        End If
    End Sub

    Private Sub btnclear_approved_Click(sender As Object, e As EventArgs) Handles btnclear_approved.Click, btnclear_assessor.Click, btnclear_vendor.Click
        If clsCommon.CompairString(clsCommon.myCstr(sender.Name), "btnclear_approved") = CompairStringResult.Equal Then
            pb_approved.Image = Nothing
        ElseIf clsCommon.CompairString(clsCommon.myCstr(sender.Name), "btnclear_assessor") = CompairStringResult.Equal Then
            pb_assessor.Image = Nothing
        ElseIf clsCommon.CompairString(clsCommon.myCstr(sender.Name), "btnclear_vendor") = CompairStringResult.Equal Then
            pb_vendor.Image = Nothing
        End If
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            If txtcode.Value <> "" Then
                Dim qry As String = "SELECT vendor_code FROM TSPL_VENDOR_MASTER where TSPL_VENDOR_MASTER.Registration_No= '" & txtcode.Value & "'"
                Dim StrVendorCode As String = clsDBFuncationality.getSingleValue(qry)
                If clsCommon.myLen(StrVendorCode) > 0 Then
                    clsCommon.MyMessageBoxShow("Vendor Code (" & StrVendorCode & ") exist against this Registration.")
                    Exit Sub
                End If

                If myMessages.deleteConfirm() Then
                        If clsVendorReg.DeleteData(txtcode.Value) Then
                            clsCommon.MyMessageBoxShow("Record deleted successfully.")
                            btnNew.PerformClick()
                        End If
                    End If
                End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        closeform()
    End Sub

    Private Sub FrmVendorReg_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            closeform()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            btnNew.PerformClick()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                          "TSPL_VENDORREGISTRATION_MACHINERY_DETAILS" + Environment.NewLine + _
                          "TSPL_VENDORREGISTRATION_CUSTOMER_DETAILS" + Environment.NewLine + _
                          "TSPL_VENDORREGISTRATION_MASTER")
        End If
    End Sub

    Function AllowToSave() As Boolean
        Return True
    End Function

    Public Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsVendorReg

                obj.Code = txtcode.Value
                obj.Name = txtName.Text
                obj.Address1 = txtAddress1.Text
                obj.Address2 = txtAddress2.Text
                obj.Name_Owners = txtName_Owners.Text
                If rbtn_pvt.IsChecked = True Then
                    obj.Organization_Type = "P"
                ElseIf rbtn_govt.IsChecked = True Then
                    obj.Organization_Type = "G"
                ElseIf rbtn_ownership.IsChecked = True Then
                    obj.Organization_Type = "O"
                ElseIf rbtn_public.IsChecked = True Then
                    obj.Organization_Type = "B"
                End If

                obj.Sub_Contractor = IIf(rbtn_subcontractor.IsChecked, "1", "0")
                obj.Phone_No = txtPhoneNo.Text
                obj.Fax_No = txtFaxNo.Text
                obj.Turn_Over = txtTurnOver.Text
                obj.Contact_Person_Name = txtContactPName.Text
                obj.Contact_Person_Phone_No = txtContactPPhone.Text
                obj.Organization_Details = txtdetails_orgstructure.Text
                obj.Manufacturing_Facilities = txtManufacturing_facilities.Text
                obj.Captive_Power = IIf(chk_captive.Checked, "1", "0")
                obj.Captive_Power_Details = txt_captivedetails.Text
                obj.Is_SeparateSection_Responsible = IIf(chk_separatesection.Checked, "1", "0")
                obj.WhoisAuthorised = txt_whoisauthorised.Text
                obj.Is_full_doc_QS_Available = IIf(chk_fulldocqsavailable.Checked, "1", "0")
                obj.Is_Std_drawing_Available = IIf(chk_stddrawingsavailable.Checked, "1", "0")
                obj.Is_RM_Inspection_Available = IIf(chk_RMInsAvailable.Checked, "1", "0")
                obj.Is_Pro_Inspection_Available = IIf(chk_ProInsAvailable.Checked, "1", "0")
                obj.Is_Ins_despatch = IIf(chk_InsDespatch.Checked, "1", "0")
                obj.Is_Keep_Record = IIf(chk_KeepRecord.Checked, "1", "0")
                obj.Is_temp_perm_deviation_record = IIf(chk_tmppermdevRecord.Checked, "1", "0")
                obj.Is_equip_calibrated_periodically = IIf(chk_equipcalperiodically.Checked, "1", "0")
                obj.Is_defined_QP = IIf(chk_definedQP.Checked, "1", "0")
                obj.Defined_QP_Details = txt_definedQPdetails.Text
                obj.Is_Pro_Storage_Sys = IIf(chk_ProStorageSys.Checked, "1", "0")
                obj.Is_RM_Identified = IIf(chk_RMIdentified.Checked, "1", "0")
                obj.Is_Facilities_Available = IIf(chk_FacilitiesAvailable.Checked, "1", "0")
                obj.Is_Packing_defined = IIf(chk_PackingDefined.Checked, "1", "0")
                obj.Packing_Def_Details = txt_PackingDefinedDetails.Text
                obj.NameAndAddress_Banker = txt_nameofbanker.Text
                obj.Salestax_No = txt_salestaxregno.Text
                obj.Central_Excise_No = txt_centralexciseregno.Text
                obj.Payment_Terms = txt_paymentterms.Text

                obj.Vendor_Name = txt_vendorname.Text
                obj.Vendor_Desig = txt_vendordesig.Text
                If pb_vendor.Image IsNot Nothing Then
                    Dim ms As New MemoryStream()
                    pb_vendor.Image.Save(ms, pb_vendor.Image.RawFormat)
                    Dim data As Byte() = ms.GetBuffer()
                    obj.Vendor_Sign = data
                End If
                obj.Vendor_Date = dtp_vendorsigndate.Value

                obj.Assessor_Name = txt_assessorname.Text
                obj.Assessor_Desig = txt_assessordesig.Text
                If pb_assessor.Image IsNot Nothing Then
                    Dim ms As New MemoryStream()
                    pb_assessor.Image.Save(ms, pb_assessor.Image.RawFormat)
                    Dim data As Byte() = ms.GetBuffer()
                    obj.Assessor_Sign = data
                End If
                obj.Assessor_Date = dtp_assessorsigndate.Value

                obj.Visited_By = txt_visitorname.Text
                obj.Suitablefor = txt_suitablefor.Text
                obj.Suitablefor_Vendor = txt_suitableforvendor.Text
                If rb_approved.IsChecked Then
                    obj.Result_Approved = "1"
                Else
                    obj.Result_Approved = "0"
                End If

                obj.Approved_Name = txt_approvedname.Text
                obj.Approved_Desig = txt_approveddesig.Text
                If pb_approved.Image IsNot Nothing Then
                    Dim ms As New MemoryStream()
                    pb_approved.Image.Save(ms, pb_approved.Image.RawFormat)
                    Dim data As Byte() = ms.GetBuffer()
                    obj.Approved_Sign = data
                End If
                obj.Approved_Date = dtp_approveddate.Value


                obj.ArrMachinery = New List(Of clsVendorRegMachineryDetail)
                For Each grow As GridViewRowInfo In gvmanufacturing.Rows
                    Dim objmachinery As New clsVendorRegMachineryDetail()
                    objmachinery.Machine_Description = clsCommon.myCstr(grow.Cells(ColDescription).Value)
                    objmachinery.MakeandModel = clsCommon.myCstr(grow.Cells(ColMakeModel).Value)
                    objmachinery.NoofInst = clsCommon.myCstr(grow.Cells(ColNoOfCS).Value)
                    objmachinery.YearofPurchase = clsCommon.myCstr(grow.Cells(ColYear).Value)
                    objmachinery.Type = "0"
                    If objmachinery IsNot Nothing Then
                        obj.ArrMachinery.Add(objmachinery)
                    End If
                Next

                For Each grow As GridViewRowInfo In gvtesting.Rows
                    Dim objtesting As New clsVendorRegMachineryDetail()
                    objtesting.Machine_Description = clsCommon.myCstr(grow.Cells(ColDescription).Value)
                    objtesting.MakeandModel = clsCommon.myCstr(grow.Cells(ColMakeModel).Value)
                    objtesting.NoofInst = clsCommon.myCstr(grow.Cells(ColNoOfCS).Value)
                    objtesting.YearofPurchase = clsCommon.myCstr(grow.Cells(ColYear).Value)
                    objtesting.Type = "1"
                    If objtesting IsNot Nothing Then
                        obj.ArrMachinery.Add(objtesting)
                    End If
                Next

                obj.ArrCust = New List(Of clsVendorRegCustDetail)
                Dim objcust1 As New clsVendorRegCustDetail()
                If clsCommon.CompairString(txt_cust1.Text, "") <> CompairStringResult.Equal Then
                    objcust1.CustomerNameandAddress = txt_cust1.Text
                    If objcust1 IsNot Nothing Then
                        obj.ArrCust.Add(objcust1)
                    End If
                End If
                '-----------
                Dim objcust2 As New clsVendorRegCustDetail()
                If clsCommon.CompairString(txt_cust2.Text, "") <> CompairStringResult.Equal Then
                    objcust2.CustomerNameandAddress = txt_cust2.Text
                    If objcust2 IsNot Nothing Then
                        obj.ArrCust.Add(objcust2)
                    End If
                End If
                '---------------
                Dim objcust3 As New clsVendorRegCustDetail()
                If clsCommon.CompairString(txt_cust3.Text, "") <> CompairStringResult.Equal Then
                    objcust3.CustomerNameandAddress = txt_cust3.Text
                    If objcust3 IsNot Nothing Then
                        obj.ArrCust.Add(objcust3)
                    End If
                End If
                '------------------
                Dim objcust4 As New clsVendorRegCustDetail()
                If clsCommon.CompairString(txt_cust4.Text, "") <> CompairStringResult.Equal Then
                    objcust4.CustomerNameandAddress = txt_cust4.Text
                    If objcust4 IsNot Nothing Then
                        obj.ArrCust.Add(objcust4)
                    End If
                End If
                '-----------
                Dim objcust5 As New clsVendorRegCustDetail()
                If clsCommon.CompairString(txt_cust5.Text, "") <> CompairStringResult.Equal Then
                    objcust5.CustomerNameandAddress = txt_cust5.Text
                    If objcust5 IsNot Nothing Then
                        obj.ArrCust.Add(objcust5)
                    End If
                End If
                '----------------
                Dim objcust6 As New clsVendorRegCustDetail()
                If clsCommon.CompairString(txt_cust6.Text, "") <> CompairStringResult.Equal Then
                    objcust6.CustomerNameandAddress = txt_cust6.Text
                    If objcust6 IsNot Nothing Then
                        obj.ArrCust.Add(objcust6)
                    End If
                End If
                '------------
                Dim objcust7 As New clsVendorRegCustDetail()
                If clsCommon.CompairString(txt_cust7.Text, "") <> CompairStringResult.Equal Then
                    objcust7.CustomerNameandAddress = txt_cust7.Text
                    If objcust7 IsNot Nothing Then
                        obj.ArrCust.Add(objcust7)
                    End If
                End If
                '------------
                Dim objcust8 As New clsVendorRegCustDetail()
                If clsCommon.CompairString(txt_cust8.Text, "") <> CompairStringResult.Equal Then
                    objcust8.CustomerNameandAddress = txt_cust8.Text
                    If objcust8 IsNot Nothing Then
                        obj.ArrCust.Add(objcust8)
                    End If
                End If
                '--------
                Dim objcust9 As New clsVendorRegCustDetail()
                If clsCommon.CompairString(txt_cust9.Text, "") <> CompairStringResult.Equal Then
                    objcust9.CustomerNameandAddress = txt_cust9.Text
                    If objcust9 IsNot Nothing Then
                        obj.ArrCust.Add(objcust9)
                    End If
                End If
                '-----------
                Dim objcust10 As New clsVendorRegCustDetail()
                If clsCommon.CompairString(txt_cust10.Text, "") <> CompairStringResult.Equal Then
                    objcust10.CustomerNameandAddress = txt_cust10.Text
                    If objcust10 IsNot Nothing Then
                        obj.ArrCust.Add(objcust10)
                    End If
                End If


                If (clsVendorReg.SaveData(obj, clsVendorReg.CheckNewEntry(obj.Code, Nothing))) Then
                    LoadData(obj.Code, NavigatorType.Current)
                    clsCommon.MyMessageBoxShow("Data saved successfully.")
                End If

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub


    Public Sub closeform()
        Me.Close()
    End Sub

    Private Sub funReset()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtName.Text = ""
        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtName_Owners.Text = ""
        rbtn_pvt.IsChecked = True
        rbtn_subcontractor.IsChecked = False
        txtPhoneNo.Text = "(+__)__________"
        txtFaxNo.Text = ""
        txtContactPName.Text = ""
        txtContactPPhone.Text = "(+__)__________"
        txtdetails_orgstructure.Text = ""
        txtManufacturing_facilities.Text = ""

        LoadBlankGridForManufacturingEquipment()
        LoadBlankGridForTestingEquipment()
        chk_captive.IsChecked = False
        txt_captivedetails.Text = ""
        txt_captivedetails.Enabled = False

        '''''''''''''''''''''''''''''''''
        chk_separatesection.Checked = False
        txt_whoisauthorised.Text = ""
        chk_fulldocqsavailable.Checked = False
        chk_stddrawingsavailable.Checked = False
        chk_RMInsAvailable.Checked = False
        chk_ProInsAvailable.Checked = False
        chk_InsDespatch.Checked = False
        chk_KeepRecord.Checked = False
        chk_tmppermdevRecord.Checked = False
        chk_equipcalperiodically.Checked = False
        chk_definedQP.Checked = False
        txt_definedQPdetails.Text = ""
        chk_ProStorageSys.Checked = False
        chk_RMIdentified.Checked = False
        chk_FacilitiesAvailable.Checked = False
        chk_PackingDefined.Checked = False
        txt_PackingDefinedDetails.Text = ""
        txt_nameofbanker.Text = ""
        txt_salestaxregno.Text = ""
        txt_centralexciseregno.Text = ""
        txt_paymentterms.Text = ""

        txt_vendorname.Text = ""
        txt_vendordesig.Text = ""
        dtp_vendorsigndate.Value = clsCommon.GETSERVERDATE

        txt_assessorname.Text = ""
        txt_assessordesig.Text = ""
        dtp_assessorsigndate.Value = clsCommon.GETSERVERDATE
        txt_visitorname.Text = ""
        txt_suitablefor.Text = ""
        txt_suitableforvendor.Text = ""
        txt_approvedname.Text = ""
        txt_approveddesig.Text = ""
        dtp_approveddate.Value = clsCommon.GETSERVERDATE

        '''''''''''''''''''''''''''''''''
        txt_cust1.Text = ""
        txt_cust2.Text = ""
        txt_cust3.Text = ""
        txt_cust4.Text = ""
        txt_cust5.Text = ""
        txt_cust6.Text = ""
        txt_cust7.Text = ""
        txt_cust8.Text = ""
        txt_cust9.Text = ""
        txt_cust10.Text = ""

        btnsave.Enabled = True
        btnsave.Text = "Save"
        btndelete.Enabled = True
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        txtcode.Value = ""
        txtcode.MyReadOnly = False
        txtcode.Focus()
        funReset()
    End Sub

    Private Sub btn_print_Click(sender As Object, e As EventArgs) Handles btn_print.Click
        Try
            Dim dt As DataTable = New DataTable()
            Dim dt1 As DataTable = New DataTable()
            Dim dt2 As DataTable = New DataTable()

            Dim qry As String = Nothing
            Dim qry1 As String = Nothing
            Dim qry2 As String = Nothing
            Dim qry3 As String = Nothing
            Dim qry4 As String = Nothing
            qry = "select * ,stuff((select  ',' + TSPL_VENDORREGISTRATION_CUSTOMER_DETAILS.CustomerNameandAddress  from TSPL_VENDORREGISTRATION_CUSTOMER_DETAILS left outer  join TSPL_VENDORREGISTRATION_MASTER on TSPL_VENDORREGISTRATION_MASTER.Registration_No=TSPL_VENDORREGISTRATION_CUSTOMER_DETAILS.Registration_No for xml path('')  ),1,1,'') CustomerNameandAddress " & _
                 " from TSPL_VENDORREGISTRATION_MASTER left outer join TSPL_VENDORREGISTRATION_MACHINERY_DETAILS on TSPL_VENDORREGISTRATION_MACHINERY_DETAILS.Registration_No=TSPL_VENDORREGISTRATION_MASTER.Registration_No where type='0'"
            dt = clsDBFuncationality.GetDataTable(qry)

            qry1 = "select * from TSPL_VENDORREGISTRATION_MACHINERY_DETAILS left outer join TSPL_VENDORREGISTRATION_MASTER on TSPL_VENDORREGISTRATION_MACHINERY_DETAILS.Registration_No=TSPL_VENDORREGISTRATION_MASTER.Registration_No where type='1'"
            dt1 = clsDBFuncationality.GetDataTable(qry1)

            'frmCrystalReportViewer.funreport(CrystalReportFolder.Purchase, dt, "rptVendorReg", "Vendor Registration")
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreport(CrystalReportFolder.Purchase, qry, qry1, "rptVendorReg", "Vendor Registration", "rptVendorReg_Sub_Report")
            frmCRV = Nothing
            'qry2 = "select * from TSPL_VENDORREGISTRATION_MASTER"
            'qry3 = "select * from TSPL_VENDORREGISTRATION_MACHINERY_DETAILS"
            'qry4 = "select * from TSPL_VENDORREGISTRATION_CUSTOMER_DETAILS"

            'frmCrystalReportViewer.funsubreport(CrystalReportFolder.Purchase, qry2, qry3, qry4, "", "", "rptVendorReg", "Vendor Registration")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcode._MYValidating
        txtcode.Value = clsVendorReg.getFinder("", txtcode.Value, isButtonClicked)
        LoadData(txtcode.Value, NavigatorType.Current)
    End Sub

    Private Sub Txtcode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtcode._MYNavigator
        LoadData(txtcode.Value, NavType)
    End Sub

    Public Sub LoadData(ByVal strCode As String, ByVal NavType As common.NavigatorType)
        Try
            funReset()

            Dim obj As clsVendorReg = clsVendorReg.GetData(strCode, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                UsLock1.Status = obj.Is_VendorRegApproved
                btnsave.Text = "Update"
                txtcode.Value = obj.Code
                txtName.Text = obj.Name
                txtAddress1.Text = obj.Address1
                txtAddress2.Text = obj.Address2
                txtName_Owners.Text = obj.Name_Owners
                If obj.Organization_Type = "P" Then
                    rbtn_pvt.IsChecked = True
                ElseIf obj.Organization_Type = "G" Then
                    rbtn_govt.IsChecked = True
                ElseIf obj.Organization_Type = "O" Then
                    rbtn_ownership.IsChecked = True
                ElseIf obj.Organization_Type = "B" Then
                    rbtn_public.IsChecked = True
                End If

                rbtn_subcontractor.IsChecked = IIf(obj.Sub_Contractor = "1", True, False)
                txtPhoneNo.Text = obj.Phone_No
                txtFaxNo.Text = obj.Fax_No
                txtTurnOver.Text = obj.Turn_Over
                txtContactPName.Text = obj.Contact_Person_Name
                txtContactPPhone.Text = obj.Contact_Person_Phone_No
                txtdetails_orgstructure.Text = obj.Organization_Details
                txtManufacturing_facilities.Text = obj.Manufacturing_Facilities
                chk_captive.Checked = IIf(obj.Captive_Power = "1", True, False)
                txt_captivedetails.Text = obj.Captive_Power_Details
                chk_separatesection.Checked = IIf(obj.Is_SeparateSection_Responsible = "1", True, False)
                txt_whoisauthorised.Text = obj.WhoisAuthorised
                chk_fulldocqsavailable.Checked = IIf(obj.Is_full_doc_QS_Available = "1", True, False)
                chk_stddrawingsavailable.Checked = IIf(obj.Is_Std_drawing_Available = "1", True, False)
                chk_RMInsAvailable.Checked = IIf(obj.Is_RM_Inspection_Available = "1", True, False)
                chk_ProInsAvailable.Checked = IIf(obj.Is_Pro_Inspection_Available = "1", True, False)
                chk_InsDespatch.Checked = IIf(obj.Is_Ins_despatch = "1", True, False)
                chk_KeepRecord.Checked = IIf(obj.Is_Keep_Record = "1", True, False)
                chk_tmppermdevRecord.Checked = IIf(obj.Is_temp_perm_deviation_record = "1", True, False)
                chk_equipcalperiodically.Checked = IIf(obj.Is_equip_calibrated_periodically = "1", True, False)
                chk_definedQP.Checked = IIf(obj.Is_defined_QP = "1", True, False)
                txt_definedQPdetails.Text = obj.Defined_QP_Details
                chk_ProStorageSys.Checked = IIf(obj.Is_Pro_Storage_Sys = "1", True, False)
                chk_RMIdentified.Checked = IIf(obj.Is_RM_Identified = "1", True, False)
                chk_FacilitiesAvailable.Checked = IIf(obj.Is_Facilities_Available = "1", True, False)
                chk_PackingDefined.Checked = IIf(obj.Is_Packing_defined = "1", True, False)
                txt_PackingDefinedDetails.Text = obj.Packing_Def_Details
                txt_nameofbanker.Text = obj.NameAndAddress_Banker
                txt_salestaxregno.Text = obj.Salestax_No
                txt_centralexciseregno.Text = obj.Central_Excise_No
                txt_paymentterms.Text = obj.Payment_Terms

                txt_vendorname.Text = obj.Vendor_Name
                txt_vendordesig.Text = obj.Vendor_Desig
                'If pb_vendor.Image IsNot Nothing Then
                '    Dim ms As New MemoryStream()
                '    pb_vendor.Image.Save(ms, pb_vendor.Image.RawFormat)
                '    Dim data As Byte() = ms.GetBuffer()
                '    obj.Vendor_Sign = data
                'End If
                If obj.Vendor_Sign IsNot Nothing AndAlso obj.Vendor_Sign.Length > 0 Then
                    Dim data As Byte() = DirectCast(obj.Vendor_Sign, Byte())
                    Dim ms As New MemoryStream(data)
                    pb_vendor.Image = Image.FromStream(ms)
                    ms.Close()
                    ms.Dispose()
                End If
                dtp_vendorsigndate.Value = obj.Vendor_Date

                txt_assessorname.Text = obj.Assessor_Name
                txt_assessordesig.Text = obj.Assessor_Desig
                'If pb_assessor.Image IsNot Nothing Then
                '    Dim ms As New MemoryStream()
                '    pb_assessor.Image.Save(ms, pb_assessor.Image.RawFormat)
                '    Dim data As Byte() = ms.GetBuffer()
                '    obj.Assessor_Sign = data
                'End If
                If obj.Assessor_Sign IsNot Nothing AndAlso obj.Assessor_Sign.Length > 0 Then
                    Dim data As Byte() = DirectCast(obj.Assessor_Sign, Byte())
                    Dim ms As New MemoryStream(data)
                    pb_assessor.Image = Image.FromStream(ms)
                    ms.Close()
                    ms.Dispose()
                End If
                dtp_assessorsigndate.Value = obj.Assessor_Date

                txt_visitorname.Text = obj.Visited_By
                txt_suitablefor.Text = obj.Suitablefor
                txt_suitableforvendor.Text = obj.Suitablefor_Vendor
                If obj.Result_Approved = "1" Then
                    rb_approved.IsChecked = True
                Else
                    rb_approved.IsChecked = False
                End If

                txt_approvedname.Text = obj.Approved_Name
                txt_approveddesig.Text = obj.Approved_Desig
                'If pb_approved.Image IsNot Nothing Then
                '    Dim ms As New MemoryStream()
                '    pb_approved.Image.Save(ms, pb_approved.Image.RawFormat)
                '    Dim data As Byte() = ms.GetBuffer()
                '    obj.Approved_Sign = data
                'End If
                If obj.Approved_Sign IsNot Nothing AndAlso obj.Approved_Sign.Length > 0 Then
                    Dim data As Byte() = DirectCast(obj.Approved_Sign, Byte())
                    Dim ms As New MemoryStream(data)
                    pb_approved.Image = Image.FromStream(ms)
                    ms.Close()
                    ms.Dispose()
                End If
                dtp_approveddate.Value = obj.Approved_Date


                'obj.ArrMachinery = New List(Of clsVendorRegMachineryDetail)
                'For Each grow As GridViewRowInfo In gvmanufacturing.Rows
                '    Dim objmachinery As New clsVendorRegMachineryDetail()
                '    objmachinery.Machine_Description = clsCommon.myCstr(grow.Cells(ColDescription).Value)
                '    objmachinery.MakeandModel = clsCommon.myCstr(grow.Cells(ColMakeModel).Value)
                '    objmachinery.NoofInst = clsCommon.myCstr(grow.Cells(ColNoOfCS).Value)
                '    objmachinery.YearofPurchase = clsCommon.myCstr(grow.Cells(ColYear).Value)
                '    objmachinery.Type = "0"
                '    If objmachinery IsNot Nothing Then
                '        obj.ArrMachinery.Add(objmachinery)
                '    End If
                'Next

                If obj.ArrMachinery IsNot Nothing AndAlso obj.ArrMachinery.Count > 0 Then
                    For Each objtr As clsVendorRegMachineryDetail In obj.ArrMachinery
                        gvmanufacturing.Rows.AddNew()
                        gvmanufacturing.Rows(gvmanufacturing.RowCount - 1).Cells(ColDescription).Value = objtr.Machine_Description
                        gvmanufacturing.Rows(gvmanufacturing.RowCount - 1).Cells(ColMakeModel).Value = objtr.MakeandModel
                        gvmanufacturing.Rows(gvmanufacturing.RowCount - 1).Cells(ColNoOfCS).Value = objtr.NoofInst
                        gvmanufacturing.Rows(gvmanufacturing.RowCount - 1).Cells(ColYear).Value = objtr.YearofPurchase
                    Next
                End If

                'For Each grow As GridViewRowInfo In gvtesting.Rows
                '    Dim objtesting As New clsVendorRegMachineryDetail()
                '    objtesting.Machine_Description = clsCommon.myCstr(grow.Cells(ColDescription).Value)
                '    objtesting.MakeandModel = clsCommon.myCstr(grow.Cells(ColMakeModel).Value)
                '    objtesting.NoofInst = clsCommon.myCstr(grow.Cells(ColNoOfCS).Value)
                '    objtesting.YearofPurchase = clsCommon.myCstr(grow.Cells(ColYear).Value)
                '    objtesting.Type = "1"
                '    If objtesting IsNot Nothing Then
                '        obj.ArrMachinery.Add(objtesting)
                '    End If
                'Next

                If obj.ArrCust IsNot Nothing AndAlso obj.ArrCust.Count > 0 Then
                    If obj.ArrCust.Count > 0 AndAlso obj.ArrCust(0) IsNot Nothing Then
                        txt_cust1.Text = clsCommon.myCstr(obj.ArrCust(0).CustomerNameandAddress)
                    End If
                    If obj.ArrCust.Count > 1 AndAlso obj.ArrCust(1) IsNot Nothing Then
                        txt_cust2.Text = clsCommon.myCstr(obj.ArrCust(1).CustomerNameandAddress)
                    End If
                    If obj.ArrCust.Count > 2 AndAlso obj.ArrCust(2) IsNot Nothing Then
                        txt_cust3.Text = clsCommon.myCstr(obj.ArrCust(2).CustomerNameandAddress)
                    End If
                    If obj.ArrCust.Count > 3 AndAlso obj.ArrCust(3) IsNot Nothing Then
                        txt_cust4.Text = clsCommon.myCstr(obj.ArrCust(3).CustomerNameandAddress)
                    End If
                    If obj.ArrCust.Count > 4 AndAlso obj.ArrCust(4) IsNot Nothing Then
                        txt_cust5.Text = clsCommon.myCstr(obj.ArrCust(4).CustomerNameandAddress)
                    End If
                    If obj.ArrCust.Count > 5 AndAlso obj.ArrCust(5) IsNot Nothing Then
                        txt_cust6.Text = clsCommon.myCstr(obj.ArrCust(5).CustomerNameandAddress)
                    End If
                    If obj.ArrCust.Count > 6 AndAlso obj.ArrCust(6) IsNot Nothing Then
                        txt_cust7.Text = clsCommon.myCstr(obj.ArrCust(6).CustomerNameandAddress)
                    End If
                    If obj.ArrCust.Count > 7 AndAlso obj.ArrCust(7) IsNot Nothing Then
                        txt_cust8.Text = clsCommon.myCstr(obj.ArrCust(7).CustomerNameandAddress)
                    End If
                    If obj.ArrCust.Count > 8 AndAlso obj.ArrCust(8) IsNot Nothing Then
                        txt_cust9.Text = clsCommon.myCstr(obj.ArrCust(8).CustomerNameandAddress)
                    End If
                    If obj.ArrCust.Count > 9 AndAlso obj.ArrCust(9) IsNot Nothing Then
                        txt_cust10.Text = clsCommon.myCstr(obj.ArrCust(9).CustomerNameandAddress)
                    End If
                End If

                
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub Btn_VenRegApprove_Click(sender As Object, e As EventArgs) Handles btn_VenRegApprove.Click
        Try
            If txtcode.Value <> "" Then
                Dim qry As String = ""
                qry = "SELECT Is_VendorRegApproved FROM TSPL_VENDORREGISTRATION_MASTER where TSPL_VENDORREGISTRATION_MASTER.Registration_No= '" & txtcode.Value & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 1 Then
                    clsCommon.MyMessageBoxShow("Record Already Approved.")
                Else
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_VENDORREGISTRATION_MASTER set Is_VendorRegApproved=1 where TSPL_VENDORREGISTRATION_MASTER.Registration_No= '" & txtcode.Value & "'")
                    LoadData(txtcode.Value, NavigatorType.Current)
                    clsCommon.MyMessageBoxShow("Record Approved Successfully.")
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Imports System.IO
'Imports Outlook = Microsoft.Office.Interop.Outlook
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
'Imports XpertERPCommanServices

Public Class FrmComplaintDetailEntry
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim Obj As clscomplaintdetail
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim mailstatus As String
    Const colItemCode As String = "itemcode"
    Const colItemName As String = "itemname"
    Const colItemCost As String = "itemCost"
    Const colItemQty As String = "itemQty"
    Public colchkbox As Boolean
    Public sendmail As String
    Public copyclick As String = "N"
    Private ObjList As List(Of clscomplaintItemdetail)
    Dim IsinsideLoaded As Boolean = False
    Dim Isvaluechanged As Boolean = False
    Public strComplaint As String = ""
    Dim isNewEntry As Boolean = True
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmComplaintDetailEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Public Sub SetMailRight()
        If objCommonVar.IsMailSend Then
            btnsend.Enabled = True
        Else
            btnsend.Enabled = False
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Public Sub LoadDG()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        ' gv1.Rows.AddNew()
        'Dim qry As String = "select item_code as [Item Code],item_desc as [Item Description] from tspl_item_master where (item_type='R' or item_type='O') order by item_code"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        'dt.Columns.Add("Status", GetType(Boolean))
        'gv1.DataSource = dt
        'gv1.Columns("item code").Width = 100
        'gv1.Columns("Item Description").Width = 250

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colItemCode
        repoICode.HeaderImage = My.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colItemName
        repoIName.Width = 200
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoICost As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICost.FormatString = ""
        repoICost.HeaderText = "Item Cost"
        repoICost.Name = colItemCost
        repoICost.Width = 80
        repoICost.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICost)

        Dim repoIQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIQty.FormatString = ""
        repoIQty.HeaderText = "Item Quantity"
        repoIQty.Name = colItemQty
        repoIQty.Width = 80
        repoIQty.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoIQty)

        gv1.Rows.AddNew()
        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        'gv1.ReadOnly = True
    End Sub

    Private Sub FrmComplaintDetailEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        SetMailRight()

        Reset()
        If chkManual.Checked = False Then
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Hidden
            RadPageView1.SelectedPage = RadPageViewPage1
        Else
            RadPageView1.SelectedPage = RadPageViewPage2
        End If
        '' shivani=> against ticket[BM00000007996]
        'LoadDG()
        Try
            txtDate.Text = clsCommon.GETSERVERDATE().ToString("dd/MM/yyyy h:mm:ss tt")
        Catch ex As Exception
        End Try

        Try
            txtcmpldt.Text = clsCommon.GETSERVERDATE().ToString("dd/MM/yyyy h:mm:ss tt")
        Catch ex As Exception
        End Try
        Try
            txtCompleteDate.Text = clsCommon.GETSERVERDATE().ToString("dd/MM/yyyy h:mm:ss tt")
        Catch ex As Exception
        End Try
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        If clsCommon.myLen(strComplaint) > 0 Then
            LoadData(strComplaint, NavigatorType.Current)
        End If

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        txtcomid.Focus()
        txtcomid.Select()
        RadMenuItem1.Visibility = ElementVisibility.Collapsed
        btnsend.Visible = False
    End Sub

    Public Sub Reset()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv2.DataSource = Nothing
        gv2.Rows.Clear()
        LoadDG()
        LoadDGManual()
        txtsrvcdlr.MendatroryField = False
        txttdmcode.MendatroryField = False
        txtpendcode.MendatroryField = False
        chkManual.Enabled = True
        txtaddamt.Text = ""
        txtprimarycode.Value = ""
        txtprimarydesc.Text = ""
        txtseccode.Value = ""
        txtpendcode.Value = ""
        chksparepart.Checked = False

        'rdfranchise.IsChecked = False
        'rdsrvcexe.IsChecked = False
        txtworkorderno.Text = ""
        txtdesc.Text = ""
        txtcomid.Value = ""
        txtDate.Text = ""
        txtoutletcode.Value = ""
        txtoutletdesc.Text = ""
        txtdistributor.Text = ""
        txtcity.Text = ""
        txtstate.Text = ""
        txtcountry.Text = ""
        txtoutletadd.Text = ""
        txtphnno.Text = ""
        txtassetcode.Value = ""
        txtreptserialno.Text = ""
        txtassetdesc.Text = ""
        txtmake.Text = ""
        txtmodel.Text = ""
        txtSize.Text = ""
        txtserialno.Text = ""
        txtresponse.Text = ""
        txttagno.Text = ""
        txtapexno.Text = ""
        txtcomplntcode.Value = ""
        txtcmplntdesc.Text = ""
        txtcompgivenby.Text = ""
        txtcompgivento.Text = ""
        txtsrvcdlr.Value = ""
        txtsrvcdlrname.Text = ""
        txttdmcode.Value = ""
        txttdmdesc.Text = ""
        txtremarks.Text = ""

        rdnotcmplt.IsChecked = False
        rdpending.IsChecked = False

        rdcomplt.IsChecked = False
        txtcmpldt.Text = ""
        txtsecresn.Text = ""
        txtpendrsn.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnCopy.Enabled = True
        txtbillamt.Text = ""
        txtbillno.Text = ""
        chksparepart.Checked = False
        '=======
        chkManual.Checked = False
        txtOutletName.Text = ""
        txtType.Text = ""
        txtCity1.Text = ""
        txtState1.Text = ""
        txtCountry1.Text = ""
        txtLocation1.Text = ""
        txtPhn1.Text = ""
        txtAssetType1.Text = ""
        txtMake1.Text = ""
        txtModel1.Text = ""
        txtSize1.Text = ""
        txtSerialNo1.Text = ""
        txtTag1.Text = ""
        cboApex1.Text = ""
        txtprimary1.Text = ""
        txtComplaintType.Text = ""
        txtSecondary1.Text = ""
        txtComplaintGivenBy.Text = ""
        txtComplaintGivento.Text = ""
        txtfranchise1.Text = ""
        txtService1.Text = ""
        txtCompleteDate.Text = ""
        txtPending1.Text = ""
        txtResponseTime.Text = ""
        txtRemarks1.Text = ""
        txtWorkOrder1.Text = ""
        txtBillNo1.Text = ""
        txtBillAmt1.Text = ""
        txtAdditionalCharge.Text = ""
        rbtnComleted.IsChecked = False
        rbtnNotCompleted.IsChecked = False
        rbtnPending.IsChecked = False
        '====
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadApexData()
        LoadApexDataManual()
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub

    Public Sub LoadApexData()
        Try
            Dim dt As New DataTable()
            dt.Columns.Add("code", GetType(String))
            dt.Columns.Add("Name", GetType(String))
            Dim dr As DataRow = Nothing

            dr = dt.NewRow()
            dr("code") = ""
            dr("Name") = "Select"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("code") = "UW"
            dr("Name") = "Under Warranty"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("code") = "OW"
            dr("Name") = "Out Of Warranty"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("code") = "OF"
            dr("Name") = "On Your Freezer"
            dt.Rows.Add(dr)

            txtapexno.DataSource = dt
            txtapexno.DisplayMember = "name"
            txtapexno.ValueMember = "Code"
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Reset()
        Try
            txtDate.Text = clsCommon.GETSERVERDATE().ToString("dd/MM/yyyy h:mm:ss tt")
        Catch ex As Exception
        End Try

        Try
            txtcmpldt.Text = clsCommon.GETSERVERDATE().ToString("dd/MM/yyyy h:mm:ss tt")
        Catch ex As Exception
        End Try

        txtcomid.Focus()
        txtcomid.Select()
    End Sub

    Public Function AllowToSave() As Boolean
        Try
            If (clsCommon.myLen(txtoutletcode.Value) = 0) Then
                clsCommon.MyMessageBoxShow("Please Select OutLet For Complaint", Me.Text)
                txtoutletcode.Focus()
                txtoutletcode.Select()
                Return False
            End If

            If clsCommon.myLen(txtassetcode.Value) = 0 Then
                clsCommon.MyMessageBoxShow("Please Select Asset For Which Complaint Is Made", Me.Text)
                txtassetcode.Focus()
                txtassetcode.Select()
                Return False
            End If

            If (clsCommon.myLen(txtcomplntcode.Value) = 0) AndAlso (clsCommon.myLen(txtprimarycode.Value) > 0) Then
                clsCommon.MyMessageBoxShow("Please Select Complaint Type", Me.Text)
                txtcomplntcode.Focus()
                txtcomplntcode.Select()
                Return False
            End If

            If clsCommon.myLen(txtapexno.SelectedValue) = 0 Then
                clsCommon.MyMessageBoxShow("Please Select Apex Pending W/O No.", Me.Text)
                txtapexno.Select()
                Return False
            End If

            If txtapexno.SelectedValue <> "OF" AndAlso clsCommon.myLen(txtserialno.Text) = 0 AndAlso clsCommon.myLen(txttagno.Text) = 0 Then
                clsCommon.MyMessageBoxShow("Please Write Serial No. And Tag No. Of Asset", Me.Text)
                Return False
            End If

            'If rdcomplt.IsChecked = True Then 'AndAlso rdfranchise.IsChecked = False AndAlso rdsrvcexe.IsChecked = False
            '    clsCommon.MyMessageBoxShow("Please Checked Service Executive/Franchises By Which Complaint Is Completed", Me.Text)
            '    txtsrvcdlr.Focus()
            '    txtsrvcdlr.Select()
            '    Return False
            'End If

            If (rdcomplt.IsChecked = True) AndAlso (clsCommon.myLen(txtsrvcdlr.Value)) = 0 Then 'AndAlso rdsrvcexe.IsChecked
                clsCommon.MyMessageBoxShow("Please Select Service Executive", Me.Text)
                txtsrvcdlr.Focus()
                txtsrvcdlr.Select()
                Return False
            End If

            If (rdcomplt.IsChecked = True) AndAlso (clsCommon.myLen(txttdmcode.Value)) = 0 Then 'rdfranchise.IsChecked AndAlso
                clsCommon.MyMessageBoxShow("Please Select Franchise", Me.Text)
                txttdmcode.Focus()
                txttdmcode.Select()
                Return False
            End If



            If rdpending.IsChecked = False AndAlso rdcomplt.IsChecked = False Then
                rdnotcmplt.IsChecked = True

            End If

            If rdpending.IsChecked = True AndAlso clsCommon.myLen(txtpendcode.Value) = 0 Then
                clsCommon.MyMessageBoxShow("Please Enter Pending Reason For The Complaint", Me.Text)
                txtpendcode.Focus()
                txtpendcode.Select()
                Return False
            End If

            If rdcomplt.IsChecked = True AndAlso (clsCommon.myLen(txtworkorderno.Text) = 0 Or clsCommon.myLen(txtbillno.Text) = 0) Then
                clsCommon.MyMessageBoxShow("Please Enter Work Order No. and Bill No.", Me.Text)
                txtbillno.Focus()
                txtbillno.Select()
                Return False
            End If

            If rdcomplt.IsChecked = True AndAlso (clsCommon.myLen(txtbillamt.Text) = 0) Then
                clsCommon.MyMessageBoxShow("Please Enter Bill Amount", Me.Text)
                txtbillamt.Focus()
                txtbillamt.Select()
                Return False
            End If

            Dim icode As String = ""

            icode = clsCommon.myCstr(gv1.Rows(0).Cells(colItemCode).Value)

            If chksparepart.Checked = True AndAlso clsCommon.myLen(icode) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Mention Which Spare Part Is Used", Me.Text)
                Return False
            End If

            '-----------------check for complaint repeated or not-----------------------------------------------------------------
            Dim qry As String = "select count(*) from TSPL_COMPLAINT_DETAIL where cust_code='" + txtoutletcode.Value + "' and item_code='" + txtassetcode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If clsCommon.CompairString(btnSave.Text, "Update") = CompairStringResult.Equal Then
                check = check - 1
            End If

            qry = "select count(*) from TSPL_COMPLAINT_DETAIL where item_code='" + txtassetcode.Value + "' and cust_code<>'" + txtoutletcode.Value + "'"
            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry)


            If check > 0 Or check1 > 0 Then
                clsCommon.MyMessageBoxShow("Complaint Of Asset " + txtassetdesc.Text + " Of Same Outlet Name " + txtoutletdesc.Text + " Repeated " + clsCommon.myCstr(check) + " Times.,And On Other Outlets Repeation Is " + clsCommon.myCstr(check1) + "", Me.Text)
            End If
            '-------------------------------------------------------------------------------------------------------------------------

            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Function AllowToSaveManual() As Boolean

        Try
            If (clsCommon.myLen(txtOutletName.Text) = 0) Then
                clsCommon.MyMessageBoxShow("Please Fill OutLet For Complaint", Me.Text)
                txtOutletName.Focus()
                txtOutletName.Select()
                Return False
            End If

            If clsCommon.myLen(txtAssetType1.Text) = 0 Then
                clsCommon.MyMessageBoxShow("Please Fill Asset Type For Which Complaint Is Made", Me.Text)
                txtAssetType1.Focus()
                txtAssetType1.Select()
                Return False
            End If
            If clsCommon.myLen(cboApex1.SelectedValue) = 0 Then
                clsCommon.MyMessageBoxShow("Please Select Apex Pending W/O No.", Me.Text)
                cboApex1.Select()
                Return False
            End If

            If cboApex1.SelectedValue <> "OF" AndAlso clsCommon.myLen(txtSerialNo1.Text) = 0 AndAlso clsCommon.myLen(txtTag1.Text) = 0 Then
                clsCommon.MyMessageBoxShow("Please Write Serial No. And Tag No. Of Asset", Me.Text)
                Return False
            End If

            'If rdcomplt.IsChecked = True Then 'AndAlso rdfranchise.IsChecked = False AndAlso rdsrvcexe.IsChecked = False
            '    clsCommon.MyMessageBoxShow("Please Checked Service Executive/Franchises By Which Complaint Is Completed", Me.Text)
            '    txtsrvcdlr.Focus()
            '    txtsrvcdlr.Select()
            '    Return False
            'End If

            If (rbtnComleted.IsChecked = True) AndAlso (clsCommon.myLen(txtService1.Text)) = 0 Then 'AndAlso rdsrvcexe.IsChecked
                clsCommon.MyMessageBoxShow("Please Fill Service Executive", Me.Text)
                txtService1.Focus()
                txtService1.Select()
                Return False
            End If

            If (rbtnComleted.IsChecked = True) AndAlso (clsCommon.myLen(txtfranchise1.Text)) = 0 Then 'rdfranchise.IsChecked AndAlso
                clsCommon.MyMessageBoxShow("Please Fill Franchise", Me.Text)
                txtfranchise1.Focus()
                txtfranchise1.Select()
                Return False
            End If



            If rbtnPending.IsChecked = False AndAlso rbtnComleted.IsChecked = False Then
                rbtnNotCompleted.IsChecked = True

            End If

            If rbtnPending.IsChecked = True AndAlso clsCommon.myLen(txtPending1.Text) = 0 Then
                clsCommon.MyMessageBoxShow("Please Enter Pending Reason For The Complaint", Me.Text)
                txtPending1.Focus()
                txtPending1.Select()
                Return False
            End If

            If rbtnComleted.IsChecked = True AndAlso (clsCommon.myLen(txtWorkOrder1.Text) = 0 Or clsCommon.myLen(txtBillNo1.Text) = 0) Then
                clsCommon.MyMessageBoxShow("Please Enter Work Order No. and Bill No.", Me.Text)
                txtBillNo1.Focus()
                txtBillNo1.Select()
                Return False
            End If

            If rbtnComleted.IsChecked = True AndAlso (clsCommon.myLen(txtBillAmt1.Text) = 0) Then
                clsCommon.MyMessageBoxShow("Please Enter Bill Amount", Me.Text)
                txtBillAmt1.Focus()
                txtBillAmt1.Select()
                Return False
            End If

            Dim icode As String = ""

            icode = clsCommon.myCstr(gv1.Rows(0).Cells(colItemCode).Value)

            If ChkUserSparePart.Checked = True AndAlso clsCommon.myLen(icode) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Mention Which Spare Part Is Used", Me.Text)
                Return False
            End If
            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Sub SaveData()
        Obj = New clscomplaintdetail()
        ObjList = New List(Of clscomplaintItemdetail)
        Obj.comp_id = txtcomid.Value
        Obj.DocDate = clsCommon.myCDate(txtDate.Text).ToString("dd/MM/yyyy")
        'clsCommon.GetPrintDate(Obj.comp_date, "dd/MMM/yyyy hh:mm:ss tt")
        'Obj.comp_date = clsCommon.myCDate(txtDate.Text).ToString("MM/dd/yyyy h:mm:ss tt")
        Obj.comp_date = clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm:ss tt")
        Obj.description = txtdesc.Text.Replace("'", "`")
        If txtdesc.Text.Length > 200 Then
            Obj.description = txtdesc.Text.Replace("'", "`").Substring(0, 200)
        End If
        Obj.outltcode = txtoutletcode.Value
        Obj.phnno = txtphnno.Text
        Obj.itemcode = txtassetcode.Value
        Obj.itemmake = txtmake.Text
        Obj.modelno = txtmodel.Text
        Obj.size = txtSize.Text
        Obj.serialno = txtserialno.Text
        Obj.tagno = txttagno.Text
        Obj.apexno = txtapexno.SelectedValue.ToString()
        Obj.compltypecode = txtcomplntcode.Value
        Obj.complgivnby = txtcompgivenby.Text.Replace("'", "`")
        If txtcompgivenby.Text.Length > 150 Then
            Obj.complgivnby = txtcompgivenby.Text.Replace("'", "`").Substring(0, 150)
        End If
        Obj.complgivnto = txtcompgivento.Text.Replace("'", "`")
        If txtcompgivento.Text.Length > 150 Then
            Obj.complgivnto = txtcompgivento.Text.Substring(0, 150).Replace("'", "`")
        End If

        Obj.tdmcode = ""
        ' If rdfranchise.IsChecked Then
        Obj.tdmcode = txttdmcode.Value
        'End If

        Obj.remarks = txtremarks.Text.Replace("'", "`")
        If txtremarks.Text.Length > 250 Then
            Obj.remarks = txtremarks.Text.Replace("'", "`").Substring(0, 250)
        End If
        Obj.workno = txtworkorderno.Text

        If rdcomplt.IsChecked = True Then
            Obj.compl_status = "C"
        ElseIf rdnotcmplt.IsChecked = True Then
            Obj.compl_status = "N"
        ElseIf rdpending.IsChecked = True Then
            Obj.compl_status = "P"
        End If

        Obj.executivecode = ""
        ' If rdsrvcexe.IsChecked Then
        Obj.executivecode = txtsrvcdlr.Value
        'End If

        Obj.billno = txtbillno.Text
        Obj.billamt = txtbillamt.Text
        Obj.addcharge = txtaddamt.Text

        'Dim ii As Integer = 0
        'Obj.sparepart = ""
        'Obj.spare_Qty = 0
        'Try
        '    For Each grow As GridViewRowInfo In gv1.Rows
        '        'If gv1.Rows(ii).Cells(2).Value = True Then
        '        Obj.sparepart = Obj.sparepart + "," + gv1.Rows(ii).Cells(0).Value

        '        Obj.spare_Qty = clsCommon.myCstr(gv1.Rows(0).Cells(colItemQty).Value)
        '        'gv1.Rows(ii).Cells(0).Value
        '        'End If
        '        ii += 1
        '    Next
        'Catch ex As Exception
        'End Try


        'Try
        '    If Obj.sparepart.Substring(0, 1) = "," Then
        '        Obj.sparepart = Obj.sparepart.Substring(1, Obj.sparepart.Length - 1)
        '    End If
        'Catch ex As Exception
        'End Try
        'Obj.sparepart = Obj.sparepart.Replace("'", "")
        Obj.secresn = txtsecresn.Text.Replace("'", "`")
        If txtsecresn.Text.Length > 200 Then
            Obj.secresn = txtsecresn.Text.Replace("'", "`").Substring(0, 200)
        End If
        Obj.pendresn = txtpendrsn.Text.Replace("'", "`")
        If txtpendrsn.Text.Length > 200 Then
            Obj.pendresn = txtpendrsn.Text.Replace("'", "`").Substring(0, 200)
        End If
        Obj.status_date = clsCommon.GetPrintDate(txtcmpldt.Text, "dd/MMM/yyyy hh:mm:ss tt")
        'Obj.status_date = clsCommon.myCDate(txtcmpldt.Text).ToString("MM/dd/yyyy h:mm:ss tt")
        Obj.responsetym = txtresponse.Text

        ' If rdfranchise.IsChecked = True Then
        Obj.franchiseyn = "Y"
        'Else
        'Obj.franchiseyn = "N"
        'End If

        Obj.primarycode = txtprimarycode.Value
        Obj.primarydesc = txtprimarydesc.Text
        Obj.secdrycode = txtseccode.Value
        Obj.pendcode = txtpendcode.Value
        ObjList = New List(Of clscomplaintItemdetail)
        Dim objtr As clscomplaintItemdetail
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                objtr = New clscomplaintItemdetail()
                objtr.Comp_id = txtcomid.Value
                objtr.item_code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objtr.item_desc = clsCommon.myCstr(grow.Cells(colItemName).Value)
                objtr.Unit_Cost = clsCommon.myCstr(grow.Cells(colItemCost).Value)
                objtr.Qty = clsCommon.myCstr(grow.Cells(colItemQty).Value)
                ObjList.Add(objtr)
            End If
        Next
        Obj.ObjList = ObjList

        If btnSave.Text <> "Save" Then
            Dim qry As String = "select count(*) from tspl_complaint_detail where comp_id='" + txtcomid.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check = 0 Then
                clsCommon.MyMessageBoxShow("No Data Found")
                Reset()
                Return
            End If
        End If


        'If AllowToSave() Then
        Try
            Dim isSaved As Boolean = clscomplaintdetail.SaveData(Obj, IIf(btnSave.Text = "Save", True, False), Nothing)

            txtcomid.Value = Obj.comp_id
            If isSaved Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                btnSave.Text = "Update"
                btnCopy.Enabled = False
                btnPost.Enabled = True
                btnDelete.Enabled = True
            Else
                btnSave.Text = "Save"
                btnPost.Enabled = False
                btnDelete.Enabled = False
                btnCopy.Enabled = True
                common.clsCommon.MyMessageBoxShow("Data Could Not Saved", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        'End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If chkManual.Checked Then
            If AllowToSaveManual() Then SaveDataManual()
        Else
            If AllowToSave() Then SaveData()
        End If

    End Sub

    Private Sub txtcomid__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtcomid._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_COMPLAINT_DETAIL where comp_id='" + txtcomid.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtcomid.MyReadOnly = True
            ElseIf check <= 0 Then
                txtcomid.MyReadOnly = False
            End If
            LoadData(txtcomid.Value, NavType)


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'Reset()
        Obj = New clscomplaintdetail()
        txtcomid.Value = strCode
        Obj = clscomplaintdetail.GetData(txtcomid.Value, NavTyep)

        If Obj IsNot Nothing AndAlso clsCommon.myLen(Obj.comp_id) > 0 Then
            IsinsideLoaded = True
            If Obj.IsManual = 1 Then
                chkManual.Checked = True
                chkManual.Enabled = False
                RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Visible
                'RadPageViewPage2.Focus()
                'RadPageViewPage2.Select()
            ElseIf Obj.IsManual = 0 Then
                chkManual.Checked = False
                chkManual.Enabled = False
                RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Hidden
                'RadPageViewPage1.Focus()
                'RadPageViewPage1.Select()
            End If
            txtcomid.Value = Obj.comp_id
            txtDate.Text = clsCommon.myCDate(Obj.comp_date) '.ToString("MM/dd/yyyy h:mm:ss tt")
            txtdesc.Text = Obj.description
            txtoutletcode.Value = Obj.outltcode
            txtoutletdesc.Text = Obj.outltdesc
            txtdistributor.Text = Obj.outlttype
            txtoutletadd.Text = Obj.location
            txtcity.Text = Obj.city
            txtstate.Text = Obj.state
            txtcountry.Text = Obj.country
            txtphnno.Text = Obj.phnno
            txtassetcode.Value = Obj.itemcode
            txtassetdesc.Text = Obj.itemdesc
            txtmake.Text = Obj.itemmake
            txtmodel.Text = Obj.modelno
            txtSize.Text = Obj.size
            txtserialno.Text = Obj.serialno
            txttagno.Text = Obj.tagno
            txtapexno.SelectedValue = Obj.apexno

            txtcomplntcode.Value = Obj.compltypecode
            txtcmplntdesc.Text = Obj.compltypedesc
            txtprimarycode.Value = Obj.primarycode
            txtprimarydesc.Text = Obj.primarydesc
            txtseccode.Value = Obj.secdrycode
            txtcompgivenby.Text = Obj.complgivnby
            txtcompgivento.Text = Obj.complgivnto
            txttdmcode.Value = Obj.tdmcode
            txttdmdesc.Text = Obj.tdmdesc
            txtremarks.Text = Obj.remarks


            If Obj.compl_status = "C" Then
                rdcomplt.IsChecked = True
            ElseIf Obj.compl_status = "N" Then
                rdnotcmplt.IsChecked = True
            ElseIf Obj.compl_status = "P" Then
                rdpending.IsChecked = True
            End If

            txtsrvcdlr.Value = Obj.executivecode
            txtsrvcdlrname.Text = Obj.executivedesc
            txtbillno.Text = Obj.billno
            txtbillamt.Text = Obj.billamt
            txtaddamt.Text = Obj.addcharge

            'Dim ii As Integer = 0
            'Dim strspare() As String
            'strspare = Obj.sparepart.Split(",")



            'Dim j As Integer = strspare.Length
            'chksparepart.Checked = False
            'Dim jj As Integer = 0
            'While (j > 0)

            '    If Not clsCommon.CompairString(strspare(jj), "") = CompairStringResult.Equal Then
            '        Try
            '            gv1.Rows.AddNew()
            '            gv1.Rows(ii).Cells(colItemCode).Value = clsCommon.myCstr(strspare(jj))
            '            gv1.Rows(ii).Cells(colItemName).Value = clsCommon.myCstr(clsItemMaster.GetItemName(strspare(jj), Nothing))
            '            gv1.Rows(ii).Cells(colItemCost).Value = clsDBFuncationality.getSingleValue("select cost from TSPL_ITEM_MASTER where Item_Code='" & strspare(jj) & "'")
            '            gv1.Rows(ii).Cells(colItemQty).Value = Obj.spare_Qty
            '            chksparepart.Checked = True
            '        Catch ex As Exception
            '        End Try
            '    End If

            '    ii += 1
            '    j -= 1
            '    jj += 1
            'End While
            '==============
            LoadDG()
            gv1.DataSource = Nothing
            LoadDGManual()
            gv2.DataSource = Nothing

            If Obj.IsManual = 1 Then
                If (Obj.ObjList IsNot Nothing AndAlso Obj.ObjList.Count > 0) Then
                    For Each objtr As clscomplaintItemdetail In Obj.ObjList
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colItemCode).Value = objtr.item_code
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colItemName).Value = objtr.item_desc
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colItemCost).Value = objtr.Unit_Cost
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colItemQty).Value = objtr.Qty
                        gv2.Rows.AddNew()
                    Next
                    gv2.Rows.RemoveAt(gv2.Rows.Count - 1)
                Else
                    gv2.Rows.AddNew()
                End If
            Else
                If (Obj.ObjList IsNot Nothing AndAlso Obj.ObjList.Count > 0) Then
                    For Each objtr As clscomplaintItemdetail In Obj.ObjList
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objtr.item_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemName).Value = objtr.item_desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCost).Value = objtr.Unit_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemQty).Value = objtr.Qty
                        gv1.Rows.AddNew()
                    Next
                    gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                Else
                    gv1.Rows.AddNew()
                End If
            End If



            txtworkorderno.Text = Obj.workno
            txtresponse.Text = Obj.responsetym
            txtcmpldt.Text = clsCommon.myCDate(Obj.status_date) '.ToString("MM/dd/yyyy h:mm:ss tt")

            'If Obj.franchiseyn = "Y" Then
            '    rdfranchise.IsChecked = True
            'Else
            '    rdsrvcexe.IsChecked = True
            'End If


            txtsecresn.Text = Obj.secresn
            txtpendcode.Value = Obj.pendcode
            txtpendrsn.Text = Obj.pendresn
            '====for manual
            txtdesc.Text = Obj.description
            txtOutletName.Text = Obj.OutletNameManual
            txtType.Text = Obj.OutletTypeManual
            txtCity1.Text = Obj.CityManual
            txtState1.Text = Obj.StateManual
            txtCountry1.Text = Obj.CountryManual
            txtLocation1.Text = Obj.LocationManual
            txtPhn1.Text = Obj.phnno
            txtAssetType1.Text = Obj.Asset_Type_NameManual
            txtMake1.Text = Obj.itemmake
            txtModel1.Text = Obj.modelno
            txtSize1.Text = Obj.size



            txtSerialNo1.Text = Obj.serialno
            txtTag1.Text = Obj.tagno
            cboApex1.SelectedValue = Obj.apexno
            txtprimary1.Text = Obj.Primary_Reason_DescManual
            txtComplaintType.Text = Obj.ComplaintTypeManual
            txtSecondary1.Text = Obj.Secondary_Reason_DescManual
            txtComplaintGivenBy.Text = Obj.complgivnby
            txtComplaintGivento.Text = Obj.complgivnto
            txtfranchise1.Text = Obj.Franchise_DescManual
            txtService1.Text = Obj.Service_Executive_DescManual
            txtCompleteDate.Text = clsCommon.myCDate(Obj.status_date)
            txtPending1.Text = Obj.Pending_Reason_DescManual
            txtResponseTime.Text = Obj.responsetym
            txtRemarks1.Text = Obj.remarks
            'Obj.franchiseyn = "Y"

            txtWorkOrder1.Text = Obj.workno
            txtBillNo1.Text = Obj.billno
            txtBillAmt1.Text = Obj.billamt
            txtAdditionalCharge.Text = Obj.addcharge
            If Obj.compl_status = "C" Then
                rbtnComleted.IsChecked = True
            ElseIf Obj.compl_status = "N" Then
                rbtnNotCompleted.IsChecked = True
            ElseIf Obj.compl_status = "P" Then
                rbtnPending.IsChecked = True

            End If


            'Dim ii1 As Integer = 0
            'Dim strspare1() As String
            'strspare1 = Obj.sparepart.Split(",")



            'Dim j1 As Integer = strspare1.Length
            'ChkUserSparePart.Checked = False
            'Dim jj1 As Integer = 0
            'While (j1 > 0)

            '    If Not clsCommon.CompairString(strspare1(jj1), "") = CompairStringResult.Equal Then
            '        Try
            '            gv2.Rows.AddNew()
            '            gv2.Rows(ii1).Cells(colItemCode).Value = clsCommon.myCstr(strspare1(jj1))
            '            gv2.Rows(ii1).Cells(colItemName).Value = clsCommon.myCstr(clsItemMaster.GetItemName(strspare1(jj1), Nothing))
            '            gv2.Rows(ii1).Cells(colItemCost).Value = clsDBFuncationality.getSingleValue("select cost from TSPL_ITEM_MASTER where Item_Code='" & strspare1(jj1) & "'")
            '            gv2.Rows(ii1).Cells(colItemQty).Value = Obj.spare_Qty
            '            ChkUserSparePart.Checked = True
            '        Catch ex As Exception
            '        End Try
            '    End If

            '    ii1 += 1
            '    j1 -= 1
            '    jj1 += 1
            'End While

            '=========
            If copyclick = "N" Then
                btnSave.Text = "Update"
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                btnCopy.Enabled = False
                If Obj.post = "Y" Then
                    btnPost.Enabled = False
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnCopy.Enabled = False
                End If

                If Obj.post = "Y" Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                Else
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
            Else
                btnSave.Text = "Save"
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                btnCopy.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            '--------------------------------------------------------------
            Dim sDateFrom As DateTime = clsCommon.myCDate(txtDate.Text).ToString("dd/MM/yyyy h:mm:ss tt")
            Dim sDateTo As DateTime = clsCommon.myCDate(txtcmpldt.Text).ToString("dd/MM/yyyy h:mm:ss tt")
            'Dim dFrom As DateTime
            'Dim dTo As DateTime
            '' shivani ==>against ticket[BM00000007998]

            'If DateTime.TryParse(sDateFrom, dFrom) AndAlso DateTime.TryParse(sDateTo, dTo) Then
            Dim TS As TimeSpan = sDateTo - sDateFrom
            Dim days As Integer = IIf(TS.Days < 0, 0, TS.Days)
            Dim hour As Integer = IIf(TS.Hours < 0, 0, TS.Hours)
            Dim mins As Integer = IIf(TS.Minutes < 0, 0, TS.Minutes)
            Dim secs As Integer = IIf(TS.Seconds < 0, 0, TS.Seconds)
            hour = hour + (days * 24)
            Dim timeDiff As String = ((hour.ToString("00") & ":") + mins.ToString("00") & ":") + secs.ToString("00")
            txtresponse.Text = timeDiff
            'End If
            '----------------------------------------------------------------for manual
            Dim sDateFrom1 As DateTime = clsCommon.myCDate(txtDate.Text).ToString("dd/MM/yyyy h:mm:ss tt")
            Dim sDateTo1 As DateTime = clsCommon.myCDate(txtCompleteDate.Text).ToString("dd/MM/yyyy h:mm:ss tt")
            'Dim dFrom As DateTime
            'Dim dTo As DateTime

            'If DateTime.TryParse(sDateFrom, dFrom) AndAlso DateTime.TryParse(sDateTo, dTo) Then
            Dim TS1 As TimeSpan = sDateTo1 - sDateFrom1
            Dim days1 As Integer = IIf(TS1.Days < 0, 0, TS1.Days)
            Dim hour1 As Integer = IIf(TS1.Hours < 0, 0, TS1.Hours)
            Dim mins1 As Integer = IIf(TS1.Minutes < 0, 0, TS1.Minutes)
            Dim secs1 As Integer = IIf(TS1.Seconds < 0, 0, TS1.Seconds)
            hour1 = hour1 + (days * 24)
            Dim timeDiff1 As String = ((hour1.ToString("00") & ":") + mins1.ToString("00") & ":") + secs1.ToString("00")
            txtResponseTime.Text = timeDiff1

        End If

        IsinsideLoaded = False
    End Sub

    Private Sub txtcomid__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcomid._MYValidating
        Dim qry As String = "select distinct TSPL_COMPLAINT_DETAIL.comp_id as ComplaintID,TSPL_COMPLAINT_DETAIL.Description,TSPL_COMPLAINT_DETAIL.comp_date as" _
        & " [Complaint Date],TSPL_COMPLAINT_DETAIL.cust_code as [Outlet Code],TSPL_CUSTOMER_MASTER.customer_name as [Outlet Detail],TSPL_CUSTOMER_MASTER.Phone1 " _
        & " as [Phone No],(TSPL_CUSTOMER_MASTER.add1+' '+TSPL_CUSTOMER_MASTER.add2+' '+TSPL_CUSTOMER_MASTER.add3) as [Outlet Address],TSPL_COMPLAINT_DETAIL.compl_type_code " _
        & " as [Complaint Code],TSPL_COMPLAINT_GROUP_MASTER.description as [Complaint Desc],TSPL_COMPLAINT_DETAIL.item_code as [Asset Code],tspl_item_master.item_desc " _
        & " as [Asset Desc],TSPL_COMPLAINT_DETAIL.item_make as [Asset Make],TSPL_COMPLAINT_DETAIL.model_no as [Asset Model],TSPL_COMPLAINT_DETAIL.size " _
        & " as [Asset Size],TSPL_COMPLAINT_DETAIL.serial_no as [Asset Serial No],TSPL_COMPLAINT_DETAIL.tag_no as [Asset Tag No],case when post_status='N'" _
        & " then 'Pending' when post_status='Y' then 'Approved' end as [Status] from TSPL_COMPLAINT_DETAIL left outer join TSPL_CUSTOMER_MASTER on " _
        & " TSPL_CUSTOMER_MASTER.cust_code=tspl_complaint_detail.cust_code left outer join TSPL_COMPLAINT_GROUP_MASTER on TSPL_COMPLAINT_GROUP_MASTER.complaint_code" _
        & " =tspl_complaint_detail.compl_type_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_COMPLAINT_DETAIL.item_code"
        Dim whrClas As String = " TSPL_COMPLAINT_DETAIL.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        ' Reset()
        LoadData(clsCommon.ShowSelectForm("CMPDETFND", qry, "ComplaintID", whrClas, txtcomid.Value, "ComplaintID", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtoutletcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtoutletcode._MYValidating
        Try
            Dim qry As String = "select distinct TSPL_VISI_MASTER.Customer_id as OutletCode ,tspl_customer_master.Customer_Name as [Customer Name],(tspl_customer_master.add1+' '+tspl_customer_master.add2+' '+tspl_customer_master.add3) as Address,tspl_customer_master.phone1 as [Phone No],tspl_customer_master.Cust_Category_Code as [CategoryCode],Route_desc as [Route] ,(case when TSPL_CUSTOMER_MASTER .Credit_Customer ='N' then 'No' else 'Yes'end )as CreditParty,(case when tspl_customer_master.Status ='Y' then 'InActive'else 'Active' end)as Status,(select Count(*)from TSPL_VISI_MASTER where TSPL_VISI_MASTER .Customer_Id =TSPL_CUSTOMER_MASTER .Cust_Code )as NumOfVisi,TSPL_CUSTOMER_MASTER.closing_date as [Closing Date],TSPL_CUSTOMER_MASTER.cust_group_code as [Group Code],TSPL_CUSTOMER_MASTER.cust_type_code as [Customer Type],TSPL_CUSTOMER_MASTER.route_no as [Route No],TSPL_CUSTOMER_MASTER.route_desc as [Route],TSPL_CUSTOMER_MASTER.price_code as [Price Code],TSPL_CUSTOMER_MASTER.contact_person_name as [Contact Person],TSPL_CUSTOMER_MASTER.contact_person_phone as [Contact No],TSPL_CUSTOMER_MASTER.contact_person_email as [Contact Person E-Mail],TSPL_CUSTOMER_MASTER.terms_code as [Terms],TSPL_CUSTOMER_MASTER.cust_account as [Account Information],TSPL_CUSTOMER_MASTER.tax_group as [Tax Group],TSPL_CUSTOMER_MASTER.tin_no as [TIN No],TSPL_CUSTOMER_MASTER.lst_no as [CST/LST No],TSPL_CUSTOMER_MASTER.form_type as [Form Type],TSPL_CUSTOMER_MASTER.salesman_desc as [Salesman Name],TSPL_CUSTOMER_MASTER.created_by as [Created By],TSPL_CUSTOMER_MASTER.created_date as [Created On],TSPL_CUSTOMER_MASTER.modify_by as [Modify By],TSPL_CUSTOMER_MASTER.modify_date as [Modify On],TSPL_CUSTOMER_MASTER.PAN,TSPL_CUSTOMER_MASTER.parent_customer_no as [Parent Customer No] from tspl_customer_master left outer join TSPL_VISI_MASTER on TSPL_CUSTOMER_MASTER .cust_code =TSPL_VISI_MASTER.customer_id  "
            Dim whrcls As String = " TSPL_VISI_MASTER.Customer_id<>'' and TSPL_CUSTOMER_MASTER.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            txtoutletcode.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("OUTFNDID", qry, "OutletCode", whrcls, txtoutletcode.Value, "OutletCode", isButtonClicked))
            txtoutletdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" + txtoutletcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
            txtoutletadd.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (add1+' '+add2+' '+add3) as address from tspl_customer_master where cust_code='" + txtoutletcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
            txtcity.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select city_name from tspl_city_master where city_code in (select distinct city_code from tspl_customer_master where cust_code='" + txtoutletcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "')"))
            txtstate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct state from tspl_customer_master where cust_code='" + txtoutletcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
            txtcountry.Text = "India"
            txtphnno.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct phone1 from tspl_customer_master where cust_code='" + txtoutletcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
            txtdistributor.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_desc from TSPL_CUSTOMER_CATEGORY_MASTER where cust_category_code in (select distinct cust_category_code from tspl_customer_master where cust_code='" + txtoutletcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "')"))
            txtsrvcdlr.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Service_Dealer_Code from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtoutletcode.Value + "'"))
            txtsrvcdlrname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where Emp_Code='" + txtsrvcdlr.Value + "'"))
            txttdmcode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Franchise_Code from tspl_customer_master where cust_code='" + txtoutletcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
            txttdmdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Vendor_name from tspl_vendor_Master where Vendor_Code='" + txttdmcode.Value + "'"))
            If clsCommon.myLen(txttdmcode.Value) > 0 Then
                txttdmcode.Enabled = False
                txttdmdesc.Enabled = False
            End If
        Catch ex As Exception
            txtassetcode.Enabled = True
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtcomplntcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcomplntcode._MYValidating
        Try
            Dim qry As String
            If clsCommon.myLen(txtprimarycode.Value) <= 0 Then
                Return
            End If

            qry = "select distinct TSPL_COMPLAINT_GROUP_MASTER.complaint_code as GroupCode,TSPL_COMPLAINT_GROUP_MASTER.Description from TSPL_COMPLAINT_GROUP_MASTER"
            txtcomplntcode.Value = clsCommon.ShowSelectForm("CMPFNDID", qry, "GroupCode", "", txtcomplntcode.Value, "GroupCode", isButtonClicked)

            If clsCommon.myLen(txtcomplntcode.Value) > 0 Then
                txtcmplntdesc.Text = clsDBFuncationality.getSingleValue("select description from TSPL_COMPLAINT_GROUP_MASTER where complaint_code='" + txtcomplntcode.Value + "'")
            Else
                txtcmplntdesc.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txttdmcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txttdmcode._MYValidating
        Try
            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as [Vendor Name],(Add1+' '+Add2+' '+Add3) as Address,Closing_Date as [Closing Date],Vendor_Group_Code_Desc as [Vendor Group],City_Code_Desc as [City],State,(Phone1+' '+Phone2) as [Telephone No],Fax,Email,Contact_Person_Name as [Contact Person],Contact_Person_Phone as [Contact No],Terms_Code_Desc as [Terms],Vendor_Account_Desc as [Vendor Account],Payment_Code_Desc as [Payment Type],Bank_Code_Desc as [Bank],Tin_No as [TIN No],Lst_No as [LST No],PAN,Created_By as [Created By],Created_Date as [Created On],Modify_By as [Modified By],Modify_Date as [Modified On] from TSPL_VENDOR_MASTER "
            Dim whrcls As String = " franchise_yn='Y'"
            txttdmcode.Value = clsCommon.ShowSelectForm("TDMFND", qry, "Code", whrcls, txttdmcode.Value, "Code", isButtonClicked)
            txttdmdesc.Text = clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" + txttdmcode.Value + "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtsrvcdlr__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtsrvcdlr._MYValidating
        Try
            'If rdservcexe.IsChecked = True Then
            Dim qry As String = "select distinct emp_code as Code,emp_name as Name,emp_type as [Employee Type],emp_status as Status,birth_date as [DOB],joining_date as [DOJ] from TSPL_EMPLOYEE_MASTER "
            Dim whrcls As String = " emp_type='Service Dealer' and emp_code<>''"
            txtsrvcdlr.Value = clsCommon.ShowSelectForm("SRVFNDID", qry, "Code", whrcls, txtsrvcdlr.Value, "Code", isButtonClicked)
            txtsrvcdlrname.Text = clsDBFuncationality.getSingleValue("select emp_name from TSPL_employee_MASTER where emp_code='" + txtsrvcdlr.Value + "'")
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtassetcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtassetcode._MYValidating
        Try
            If txtapexno.SelectedValue <> "OF" Then
                Dim qry As String = "select distinct tspl_visi_master.asset_no as AssetCode,tspl_item_master.item_desc as [Asset Description],tspl_item_master.unit_code " _
                                    & " as unit,tspl_item_master.Rate,tspl_visi_master.visimake as [Asset Make Code],TSPL_ITEM_CATEGORY_LEVEL_VALUES.description " _
                                    & " as [Asset Make],tspl_visi_master.visi_size as [Size Code],a1.description as [Size],tspl_visi_master.model_no as " _
                                    & " [Asset Model Code],a2.description as [Model No.],tspl_visi_master.Serial_no as [Serial No],tspl_visi_master.tag_no as " _
                                    & " [Tag No] from tspl_visi_master left outer join tspl_item_master on tspl_item_master.item_code=tspl_visi_master.asset_no " _
                                    & " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.code=tspl_visi_master.visimake left outer " _
                                    & " join TSPL_ITEM_CATEGORY_LEVEL_VALUES as a1 on a1.code=tspl_visi_master.visi_size left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES " _
                                    & " as a2 on a2.code=tspl_visi_master.model_no"
                Dim whrcls As String = " tspl_visi_master.customer_id='" + txtoutletcode.Value + "' and tspl_visi_master.asset_no<>''"
                Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ASTFNDID", qry + " where  " + whrcls, "AssetCode", txtassetcode.Value)
                txtassetcode.Value = clsCommon.myCstr(dr("AssetCode")) 'clsCommon.ShowSelectForm("ASTFNDID", qry, "AssetCode", whrcls, txtassetcode.Value, "AssetCode", isButtonClicked)
                txtassetdesc.Text = clsCommon.myCstr(dr("Asset Description")) 'clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + txtassetcode.Value + "'")
                txtmake.Text = clsCommon.myCstr(dr("Asset Make")) 'clsDBFuncationality.getSingleValue("select TSPL_ITEM_CATEGORY_LEVEL_VALUES.description from TSPL_ITEM_CATEGORY_LEVEL_VALUES where code in (select visimake from tspl_visi_master where customer_id='" + txtoutletcode.Value + "' and asset_no='" + txtassetcode.Value + "' and visimake<>'')")
                txtmodel.Text = clsCommon.myCstr(dr("Model No.")) 'clsDBFuncationality.getSingleValue("select TSPL_ITEM_CATEGORY_LEVEL_VALUES.description from TSPL_ITEM_CATEGORY_LEVEL_VALUES where code in (select model_no from tspl_visi_master where customer_id='" + txtoutletcode.Value + "' and asset_no='" + txtassetcode.Value + "' and model_no<>'')")
                txtSize.Text = clsCommon.myCstr(dr("Size")) 'clsDBFuncationality.getSingleValue("select TSPL_ITEM_CATEGORY_LEVEL_VALUES.description from TSPL_ITEM_CATEGORY_LEVEL_VALUES where code in (select visi_size from tspl_visi_master where customer_id='" + txtoutletcode.Value + "' and asset_no='" + txtassetcode.Value + "' and visi_size<>'')")
                txtserialno.Text = clsCommon.myCstr(dr("Serial No")) 'clsDBFuncationality.getSingleValue("select serial_no from tspl_visi_master where customer_id='" + txtoutletcode.Value + "' and asset_no='" + txtassetcode.Value + "' and serial_no<>''")
                txttagno.Text = clsCommon.myCstr(dr("Tag No")) 'clsDBFuncationality.getSingleValue("select tag_no from tspl_visi_master where customer_id='" + txtoutletcode.Value + "' and asset_no='" + txtassetcode.Value + "' and tag_no<>''")
            End If

            If txtapexno.SelectedValue = "OF" Then
                Dim qry As String = "select distinct tspl_visi_master.asset_no as AssetCode,tspl_item_master.item_desc as [Asset Description],tspl_item_master.unit_code as unit " _
                                    & ",tspl_item_master.Rate,tspl_visi_master.visimake as [Asset Make Code],TSPL_ITEM_CATEGORY_LEVEL_VALUES.description as [Asset Make]," _
                                    & " tspl_visi_master.visi_size as [Size Code],a1.description as [Size],tspl_visi_master.model_no as [Asset Model Code],a2.description as" _
                                    & " [Model No.],tspl_visi_master.Serial_no as [Serial No],tspl_visi_master.tag_no as [Tag No] from tspl_visi_master left outer join " _
                                    & " tspl_item_master on tspl_item_master.item_code=tspl_visi_master.asset_no left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on " _
                                    & " TSPL_ITEM_CATEGORY_LEVEL_VALUES.code=tspl_visi_master.visimake left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES as a1 on " _
                                    & " a1.code=tspl_visi_master.visi_size left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES as a2 on a2.code=tspl_visi_master.model_no"
                Dim whrcls As String = " tspl_visi_master.asset_no<>''"
                Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ASTFNDIDD", qry + " where  " + whrcls, "AssetCode", txtassetcode.Value)
                txtassetcode.Value = clsCommon.myCstr(dr("AssetCode")) ' clsCommon.ShowSelectForm("ASTFNDID", qry, "AssetCode", whrcls, txtassetcode.Value, "AssetCode", isButtonClicked)
                txtassetdesc.Text = clsCommon.myCstr(dr("Asset Description")) 'clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + txtassetcode.Value + "'")
                txtmake.Text = clsCommon.myCstr(dr("Asset Make")) ' clsDBFuncationality.getSingleValue("select TSPL_ITEM_CATEGORY_LEVEL_VALUES.description from TSPL_ITEM_CATEGORY_LEVEL_VALUES where code in (select visimake from tspl_visi_master where asset_no='" + txtassetcode.Value + "' and visimake<>'')")
                txtmodel.Text = clsCommon.myCstr(dr("Model No.")) 'clsDBFuncationality.getSingleValue("select TSPL_ITEM_CATEGORY_LEVEL_VALUES.description from TSPL_ITEM_CATEGORY_LEVEL_VALUES where code in (select model_no from tspl_visi_master where asset_no='" + txtassetcode.Value + "' and model_no<>'')")
                txtSize.Text = clsCommon.myCstr(dr("Size")) 'clsDBFuncationality.getSingleValue("select TSPL_ITEM_CATEGORY_LEVEL_VALUES.description from TSPL_ITEM_CATEGORY_LEVEL_VALUES where code in (select visi_size from tspl_visi_master where asset_no='" + txtassetcode.Value + "' and visi_size<>'')")
                txtserialno.Text = clsCommon.myCstr(dr("Serial No")) 'clsDBFuncationality.getSingleValue("select serial_no from tspl_visi_master where asset_no='" + txtassetcode.Value + "' and serial_no<>''")
                txttagno.Text = clsCommon.myCstr(dr("Tag No")) 'clsDBFuncationality.getSingleValue("select tag_no from tspl_visi_master where asset_no='" + txtassetcode.Value + "' and tag_no<>''")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtapexno_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtapexno.SelectedValueChanged

    End Sub

    Private Sub chksparepart_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chksparepart.ToggleStateChanged

    End Sub

    Private Sub rdcomplt_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdcomplt.ToggleStateChanged
        RadGroupBox1.Visible = False

        If rdcomplt.IsChecked = True Then
            RadGroupBox1.Visible = True

        End If
    End Sub

    Private Sub txtserialno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtserialno.TextChanged
        Try
            If txtserialno.Text <> "" Then
                Dim qry As String = "select count(*) from tspl_complaint_detail where comp_code='" + objCommonVar.CurrentCompanyCode + "' and item_code='" + txtassetcode.Value + "' and serial_no='" + txtserialno.Text + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                txtreptserialno.Text = "Serial No. Repeated " + clsCommon.myCstr(check) + " Times"
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnDelete_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.myLen(txtcomid.Value) = 0 Then
                clsCommon.MyMessageBoxShow("No Document Found For Deletion,Please select complaint id", Me.Text)
                Return
            ElseIf clsCommon.myLen(txtcomid.Value) > 0 Then
                Dim qry As String = "select count(*) from tspl_complaint_detail where comp_code='" + objCommonVar.CurrentCompanyCode + "' and comp_id='" + txtcomid.Value + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    clsCommon.MyMessageBoxShow("No Document Found For Deletion,Please select complaint id", Me.Text)
                    Return
                ElseIf Not (common.clsCommon.MyMessageBoxShow("Delete the Complaint Detail Id " + txtcomid.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                    Return
                End If
            End If

            If clscomplaintdetail.DeleteDate(txtcomid.Value) Then
                common.clsCommon.MyMessageBoxShow("Data Deleted Sucessfully", Me.Text)
                Reset()

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtcomid.Value) = 0 Then
                clsCommon.MyMessageBoxShow("No Document Found For Posting,Please select complaint id", Me.Text)
                Return
            ElseIf clsCommon.myLen(txtcomid.Value) > 0 Then
                Dim qry As String = "select count(*) from tspl_complaint_detail where comp_code='" + objCommonVar.CurrentCompanyCode + "' and comp_id='" + txtcomid.Value + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    clsCommon.MyMessageBoxShow("No Document Found For Posting,Please select complaint id", Me.Text)
                    Return
                End If

            End If

            Dim qry1 As String = "update tspl_complaint_detail set post_status='Y' where comp_code='" + objCommonVar.CurrentCompanyCode + "' and comp_id='" + Obj.comp_id + "'"
            clsDBFuncationality.ExecuteNonQuery(qry1)
            common.clsCommon.MyMessageBoxShow("Complaint Detail Of Id " + txtcomid.Value + " Posted Sucessfully", Me.Text)
            btnSave.Enabled = False
            btnPost.Enabled = False
            btnDelete.Enabled = False
            btnCopy.Enabled = False
            'If sendmail = "1" Then
            '    'SendSMSandEmail()
            '    If rdcomplt.IsChecked = True Then
            '        mailstatus = "Closed"
            '    ElseIf rdpending.IsChecked = True Then
            '        mailstatus = "Pending"
            '    ElseIf rdnotcmplt.IsChecked = True Then
            '        mailstatus = "Not Closed"
            '    End If
            '    SendSMSandEmailToCustomer()
            'End If


            If Obj.post = "Y" Then
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtprimarycode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtprimarycode._MYValidating
        Try
            Dim qry As String = "select distinct TSPL_COMPLAINT_GROUP_MASTER.complaint_code as GroupCode,TSPL_COMPLAINT_GROUP_MASTER.Description,TSPL_PRIMARY_REASON_MASTER.reason_code as ComplaintCode,TSPL_PRIMARY_REASON_MASTER.description as [Complaint Description] from TSPL_PRIMARY_REASON_MASTER left outer join TSPL_COMPLAINT_GROUP_MASTER on TSPL_COMPLAINT_GROUP_MASTER.complaint_code=TSPL_PRIMARY_REASON_MASTER.complaint_code"
            Dim whrcls As String = ""
            txtprimarycode.Value = clsCommon.ShowSelectForm("PRIFNDID", qry, "ComplaintCode", whrcls, txtprimarycode.Value, "ComplaintCode", isButtonClicked)

            If clsCommon.myLen(txtprimarycode.Value) > 0 Then
                txtprimarydesc.Text = clsDBFuncationality.getSingleValue("select description from TSPL_PRIMARY_REASON_MASTER where reason_code='" + txtprimarycode.Value + "'")
                txtcomplntcode.Value = clsDBFuncationality.getSingleValue("select complaint_code from TSPL_PRIMARY_REASON_MASTER where reason_code='" + txtprimarycode.Value + "'")
                txtcmplntdesc.Text = clsDBFuncationality.getSingleValue("select description from TSPL_COMPLAINT_GROUP_MASTER where complaint_code='" + txtcomplntcode.Value + "'")
            Else
                txtprimarydesc.Text = ""
                txtcomplntcode.Value = ""
                txtcmplntdesc.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtseccode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtseccode._MYValidating
        Try
            Dim qry As String = "select distinct complaint_code as [SecondaryCode],Description as [Secondary Reason] from TSPL_COMPLAINT_MASTER"
            txtseccode.Value = clsCommon.ShowSelectForm("SECFNDID", qry, "SecondaryCode", "", txtseccode.Value, "SecondaryCode", isButtonClicked)
            txtsecresn.Text = clsDBFuncationality.getSingleValue("select description from TSPL_COMPLAINT_MASTER where complaint_code='" + txtseccode.Value + "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub txtpendcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtpendcode._MYValidating
        Try
            Dim qry As String = "select distinct pending_reason_code as [PendingCode],Description as [Pending Reason] from TSPL_PENDING_REASON_MASTER"
            txtpendcode.Value = clsCommon.ShowSelectForm("PENFNDID", qry, "PendingCode", "", txtpendcode.Value, "PendingCode", isButtonClicked)
            txtpendrsn.Text = clsDBFuncationality.getSingleValue("select description from TSPL_PENDING_REASON_MASTER where pending_reason_code='" + txtpendcode.Value + "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtcmpldt_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtcmpldt.Validating
        Try
            '--------------------------------------------------------------
            Dim sDateFrom As DateTime = clsCommon.myCDate(txtDate.Text).ToString("dd/MM/yyyy h:mm:ss tt")
            Dim sDateTo As DateTime = clsCommon.myCDate(txtcmpldt.Text).ToString("dd/MM/yyyy h:mm:ss tt")
            ' Dim dFrom As DateTime
            ' Dim dTo As DateTime

            'If DateTime.TryParse(sDateFrom, dFrom) AndAlso DateTime.TryParse(sDateTo, dTo) Then
            Dim TS As TimeSpan = sDateTo - sDateFrom
            Dim days As Integer = TS.Days
            Dim hour As Integer = TS.Hours
            Dim mins As Integer = TS.Minutes
            Dim secs As Integer = TS.Seconds
            hour = hour + (days * 24)
            Dim timeDiff As String = ((hour.ToString("00") & ":") + mins.ToString("00") & ":") + secs.ToString("00")
            txtresponse.Text = timeDiff
            'End If
            '----------------------------------------------------------------
        Catch ex As Exception
        End Try
    End Sub



    'Private Sub SendSMSandEmail()
    '    If Not objCommonVar.IsMailSend Then
    '        Exit Sub
    '    End If
    '    Dim strEmail, strphone, strMes As String
    '    ' Dim strCustomer, strContactperson, strVendorName As String
    '    'Dim decAmt As Decimal
    '    'Dim dt As DataTable

    '    '-------------------get dynamic data------------------------------
    '    Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmComplaintDetailEntry)
    '    If obj Is Nothing Then
    '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '        Return
    '    End If
    '    If clsCommon.myLen(obj.mailsubjct) <= 0 Then
    '        clsCommon.MyMessageBoxShow("First do setting of email and sms", Me.Text)
    '        Return
    '    End If

    '    Try

    '        If Process.GetProcessesByName("OutLook").Length < 1 Then
    '            'restarts the Process
    '            Process.Start("OutLook.exe")
    '        End If
    '        Dim oApp As New Outlook.Application()
    '        Dim oMsg As Outlook.MailItem


    '        oMsg = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
    '        strEmail = clsDBFuncationality.getSingleValue("select Email_id from tspl_employee_master where emp_code ='" & txtsrvcdlr.Value & "' ")



    '        oMsg.Body = obj.mailbody

    '        oMsg.Body = oMsg.Body.Replace("'", " ").Replace("`", "/")

    '        If oMsg.Body.Contains(clsEmailSMSConstants.Complnt_code) Then
    '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.Complnt_code, txtcomid.Value)
    '        End If
    '        If oMsg.Body.Contains(clsEmailSMSConstants.SerivceDealer) Then
    '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.SerivceDealer, txtsrvcdlrname.Text)
    '        End If
    '        If oMsg.Body.Contains(clsEmailSMSConstants.complnt_date) Then
    '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.complnt_date, txtDate.Text)
    '        End If
    '        If oMsg.Body.Contains(clsEmailSMSConstants.Assetcode) Then
    '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.Assetcode, txtassetdesc.Text + ",Make: " + txtmake.Text + ",Model No.: " + txtmodel.Text + ",Size: " + txtSize.Text + ",Serial No.: " + txtserialno.Text + " and Tag No.: " + txttagno.Text)
    '        End If
    '        If oMsg.Body.Contains(clsEmailSMSConstants.outlet) Then
    '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.outlet, txtoutletdesc.Text)
    '        End If
    '        '-------------------------------------------
    '        oMsg.Subject = obj.mailsubjct
    '        oMsg.Subject = oMsg.Subject.Replace("'", " ").Replace("`", "/")

    '        If oMsg.Subject.Contains(clsEmailSMSConstants.Complnt_code) Then
    '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.Complnt_code, txtcomid.Value)
    '        End If
    '        If oMsg.Subject.Contains(clsEmailSMSConstants.SerivceDealer) Then
    '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.SerivceDealer, txtsrvcdlrname.Text)
    '        End If
    '        If oMsg.Subject.Contains(clsEmailSMSConstants.complnt_date) Then
    '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.complnt_date, txtDate.Text)
    '        End If
    '        If oMsg.Subject.Contains(clsEmailSMSConstants.Assetcode) Then
    '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.Assetcode, txtassetdesc.Text + ",Make: " + txtmake.Text + ",Model No.: " + txtmodel.Text + ",Size: " + txtSize.Text + ",Serial No.: " + txtserialno.Text + " and Tag No.: " + txttagno.Text)
    '        End If
    '        If oMsg.Subject.Contains(clsEmailSMSConstants.outlet) Then
    '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.outlet, txtoutletdesc.Text)
    '        End If



    '        'strEmail = "monika@tecxpert.in"

    '        oMsg.Recipients.Add(strEmail)
    '        oMsg.CC = "ranjana.sinha@tecxpert.in;rakesh.sharma@tecxpert.in"
    '        oMsg.Send()
    '        oMsg = Nothing
    '        oApp = Nothing
    '        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Try


    '        Dim client As New System.Net.WebClient()
    '        'strMes = "Dear " & strContactperson & " (" & strCustomer & ")" & "Complaint Id " & txtcomid.Value & "  on dated  " & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy h:mm:ss tt") & "  has been registered with complaint type " + txtcmplntdesc.Text + " and primary reason " + txtprimarydesc.Text + ")"

    '        If clsCommon.myLen(obj.smsbody) <= 0 Then
    '            Return
    '        End If

    '        strMes = obj.smsbody

    '        strMes = strMes.Replace("'", " ").Replace("`", "/")

    '        If strMes.Contains(clsEmailSMSConstants.Complnt_code) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.Complnt_code, txtcomid.Value)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.SerivceDealer) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.SerivceDealer, txtsrvcdlrname.Text)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.complnt_date) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.complnt_date, txtDate.Text)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.Assetcode) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.Assetcode, txtassetdesc.Text + ",Make: " + txtmake.Text + ",Model No.: " + txtmodel.Text + ",Size: " + txtSize.Text + ",Serial No.: " + txtserialno.Text + " and Tag No.: " + txttagno.Text)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.outlet) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.outlet, txtoutletdesc.Text)
    '        End If

    '        strphone = "select phone from tspl_employee_master where emp_code='" + txtsrvcdlr.Value + "'"
    '        'Dim baseurl As String = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=tecxpert&password=1818948263&sendername=priti&mobileno=91" + strphone + "&message=" + strMes
    '        Dim baseurl As String = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=tecxpert&password=1818948263&sendername=vipin&mobileno=91" + strphone + "&message=" + strMes
    '        Dim data As Stream = client.OpenRead(baseurl)
    '        Dim reader As StreamReader = New StreamReader(data)
    '        Dim s As String = reader.ReadToEnd()
    '        data.Close()
    '        reader.Close()
    '        clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    'Private Sub SendSMSandEmailToCustomer()
    '    If Not objCommonVar.IsMailSend Then
    '        Exit Sub
    '    End If
    '    Dim strEmail, strphone, strMes As String
    '    Dim strCustomer, strContactperson As String
    '    'Dim decAmt As Decimal
    '    'Dim dt As DataTable
    '    Try


    '        If Process.GetProcessesByName("OutLook").Length < 1 Then
    '            'restarts the Process
    '            Process.Start("OutLook.exe")
    '        End If
    '        Dim oApp As New Outlook.Application()
    '        Dim oMsg As Outlook.MailItem

    '        strContactperson = clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" + txtoutletcode.Value + "'")
    '        strCustomer = txtcmplntdesc.Text
    '        oMsg = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
    '        strEmail = clsDBFuncationality.getSingleValue("select Email from tspl_customer_master where cust_code ='" & txtoutletcode.Value & "' ")
    '        oMsg.Body = "Dear " & strContactperson & " " & Environment.NewLine & "Complaint with Description " & strCustomer & "" & Environment.NewLine & " on dated: " & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy h:mm:ss tt") & "  has been registered." & Environment.NewLine & "Complaint Asset description is " + txtassetdesc.Text + ",Make: " + txtmake.Text + ",Model No.: " + txtmodel.Text + ",Size: " + txtSize.Text + ",Serial No.: " + txtserialno.Text + " and Tag No.: " + txttagno.Text + ""
    '        oMsg.Subject = "Customer Complaint Id  " & txtcomid.Value & "  has been " + mailstatus + ""
    '        oMsg.Recipients.Add(strEmail)
    '        oMsg.CC = "ranjana.sinha@tecxpert.in;rakesh.sharma@tecxpert.in"
    '        oMsg.Send()
    '        oMsg = Nothing
    '        oApp = Nothing
    '        clsCommon.MyMessageBoxShow("E-Mail Send To Outlet Successfully", Me.Text)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try

    '    Try
    '        Dim client As New System.Net.WebClient()
    '        strMes = "Dear " & strContactperson & " (" & strCustomer & ")" & "Complaint Id " & txtcomid.Value & "  on dated  " & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy h:mm:ss tt") & "  has been " + mailstatus + "."
    '        strphone = "select phone1 from tspl_customer_master where cust_code='" + txtoutletcode.Value + "'"
    '        'Dim baseurl As String = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=tecxpert&password=1818948263&sendername=priti&mobileno=91" + strphone + "&message=" + strMes
    '        Dim baseurl As String = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=tecxpert&password=1818948263&sendername=vipin&mobileno=91" + strphone + "&message=" + strMes
    '        Dim data As Stream = client.OpenRead(baseurl)
    '        Dim reader As StreamReader = New StreamReader(data)
    '        Dim s As String = reader.ReadToEnd()
    '        data.Close()
    '        reader.Close()
    '        clsCommon.MyMessageBoxShow("SMS Send To Outlet Successfully", Me.Text)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub
    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Try
            Dim qry As String = "select TSPL_COMPLAINT_DETAIL.comp_id as ComplaintId,TSPL_COMPLAINT_DETAIL.description as [Complaint Description],TSPL_COMPLAINT_DETAIL.comp_date as [Complaint Date],TSPL_COMPLAINT_DETAIL.cust_code as [Outlet No],TSPL_CUSTOMER_MASTER.customer_name as [Outlet Description],TSPL_COMPLAINT_DETAIL.item_code as [Asset Code],TSPL_ITEM_MASTER.item_desc as [Asset Description],TSPL_COMPLAINT_DETAIL.item_make as [Asset Make],TSPL_COMPLAINT_DETAIL.model_no as [Model No],TSPL_COMPLAINT_DETAIL.Size,TSPL_COMPLAINT_DETAIL.serial_no as [Serial No],TSPL_COMPLAINT_DETAIL.tag_no as [Tag No],(case when TSPL_COMPLAINT_DETAIL.apex_no='UW' then 'Under Warranty' else case when TSPL_COMPLAINT_DETAIL.apex_no='OW' then 'Out Of Warranty' else case when TSPL_COMPLAINT_DETAIL.apex_no<>'UW' and TSPL_COMPLAINT_DETAIL.apex_no<>'OW' then 'On Your Freezer' end end end) as [Apex Pending W/O No],TSPL_COMPLAINT_DETAIL.compl_given_by as [Complaint Given By],TSPL_COMPLAINT_DETAIL.compl_given_to as [Complaint Given To],(case when TSPL_COMPLAINT_DETAIL.compl_status='C' then 'Completed' else case when TSPL_COMPLAINT_DETAIL.compl_status='P' then 'Pending' else 'Not Completed' end end) as [Complaint Status],TSPL_COMPLAINT_DETAIL.bill_no as [Bill No],TSPL_COMPLAINT_DETAIL.bill_amount as [Bill Amount] from TSPL_COMPLAINT_DETAIL left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_COMPLAINT_DETAIL.cust_code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_COMPLAINT_DETAIL.item_code"
            Dim whrcls As String = " TSPL_COMPLAINT_DETAIL.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            Reset()
            txtcomid.Value = clsCommon.ShowSelectForm("COPFNDID", qry, "ComplaintId", whrcls, txtpendcode.Value, "ComplaintId", True)
            copyclick = "Y"
            LoadData(txtcomid.Value, NavigatorType.Current)
            txtcomid.Value = ""
            copyclick = "N"
            Try
                txtDate.Text = clsCommon.GETSERVERDATE().ToString("dd/MM/yyyy h:mm:ss tt")
            Catch ex As Exception
            End Try

            Try
                txtcmpldt.Text = clsCommon.GETSERVERDATE().ToString("dd/MM/yyyy h:mm:ss tt")
            Catch ex As Exception
            End Try
            btnSave.Enabled = True
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            btnPost.Enabled = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FrmComplaintDetailEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled = True Then
            btnSave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled = True Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled = True Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            btnAddNew.PerformClick()
        End If
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmComplaintDetailEntry
        frm.ShowDialog()
    End Sub

    Private Sub btnsend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsend.Click
        Try
            'If clsCommon.myLen(txtcomid.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Select Complaint Id For Sending Mail", Me.Text)
            '    txtcomid.Focus()
            '    txtcomid.Select()
            '    Return
            'End If

            'If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Complaint No. " + txtcomid.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
            '    Return
            'End If

            'LoadData(txtcomid.Value, NavigatorType.Current)
            'SendSMSandEmail()
            'If rdcomplt.IsChecked = True Then
            '    mailstatus = "Closed"
            'ElseIf rdpending.IsChecked = True Then
            '    mailstatus = "Pending"
            'ElseIf rdnotcmplt.IsChecked = True Then
            '    mailstatus = "Not Closed"
            'End If
            'SendSMSandEmailToCustomer()


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub



    Sub OpenSpareParts()
        Try
            Dim itemcode As String = ""
            Dim itemname As String = ""
            Dim ItemCost As String = "0"
            Dim XR As Integer = 0

            XR = gv1.CurrentCell.RowIndex
            itemcode = clsItemMaster.getFinder(" (item_type='R' or Item_type='O')", itemcode, True)
            itemname = clsItemMaster.GetItemName(itemcode, Nothing)
            ItemCost = clsDBFuncationality.getSingleValue("select cost from TSPL_ITEM_MASTER where Item_Code='" & itemcode & "'")
            gv1.Rows(XR).Cells(colItemCode).Value = itemcode
            gv1.Rows(XR).Cells(colItemName).Value = itemname
            gv1.Rows(XR).Cells(colItemCost).Value = ItemCost
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not IsinsideLoaded Then
                If Not Isvaluechanged Then
                    Isvaluechanged = True
                    If e.Column Is gv1.Columns(colItemCode) Then
                        OpenSpareParts()
                    End If
                    Isvaluechanged = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow

    End Sub

    Private Sub rdpending_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdpending.ToggleStateChanged
        txtpendcode.Enabled = False
        txtpendrsn.Enabled = False
        txtpendcode.MendatroryField = False
        If rdpending.IsChecked Then
            txtpendcode.Enabled = True
            txtpendrsn.Enabled = True
            txtpendcode.MendatroryField = True
        End If
    End Sub

    Private Sub txtprimarycode_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtprimarycode.Leave
        Try
            Dim qry As String = "select COUNT(*) from tspl_complaint_detail where cust_code='" & txtoutletcode.Value & "' and item_code='" & txtassetcode.Value & "' " _
            & " and serial_no='" & txtserialno.Text & "' and primary_reason_code='" & txtprimarycode.Value & "'"
            Dim Reason_Count As Integer = clsDBFuncationality.getSingleValue(qry)
            If Reason_Count > 1 Then
                Reason_Count = Reason_Count + 1
                clsCommon.MyMessageBoxShow("This Reason is Coming " & Reason_Count & " time for the same Serial and Asset for this Customer")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub


    Private Sub chkManual_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkManual.ToggleStateChanged
        If chkManual.Checked Then
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Visible
            RadPageView1.Pages("RadPageViewPage1").Item.Visibility = ElementVisibility.Hidden
            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            RadPageView1.Pages("RadPageViewPage1").Item.Visibility = ElementVisibility.Visible
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Hidden
            RadPageView1.SelectedPage = RadPageViewPage1
        End If
    End Sub
    ''for manual
    Public Sub SaveDataManual()
        Obj = New clscomplaintdetail()
        ObjList = New List(Of clscomplaintItemdetail)
        Obj.comp_id = txtcomid.Value
        Obj.DocDate = clsCommon.myCDate(txtDate.Text).ToString("dd/MM/yyyy")

        Obj.comp_date = clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm:ss tt")
        Obj.description = txtdesc.Text.Replace("'", "`")
        If txtdesc.Text.Length > 200 Then
            Obj.description = txtdesc.Text.Replace("'", "`").Substring(0, 200)
        End If

        If chkManual.Checked Then
            Obj.IsManual = 1
        Else
            Obj.IsManual = 0
        End If
        Obj.OutletNameManual = txtOutletName.Text
        Obj.OutletTypeManual = txtType.Text
        Obj.CityManual = txtCity1.Text
        Obj.StateManual = txtState1.Text
        Obj.CountryManual = txtCountry1.Text
        Obj.LocationManual = txtLocation1.Text
        Obj.phnno = txtPhn1.Text
        Obj.Asset_Type_NameManual = txtAssetType1.Text
        Obj.itemmake = txtMake1.Text
        Obj.modelno = txtModel1.Text
        Obj.size = txtSize1.Text
        Obj.serialno = txtSerialNo1.Text
        Obj.tagno = txtTag1.Text
        Obj.apexno = cboApex1.SelectedValue.ToString()
        Obj.Primary_Reason_DescManual = txtprimary1.Text
        Obj.ComplaintTypeManual = txtComplaintType.Text
        Obj.Secondary_Reason_DescManual = txtSecondary1.Text
        Obj.complgivnby = txtComplaintGivenBy.Text.Replace("'", "`")
        If txtcompgivenby.Text.Length > 150 Then
            Obj.complgivnby = txtComplaintGivenBy.Text.Replace("'", "`").Substring(0, 150)
        End If
        Obj.complgivnto = txtComplaintGivento.Text.Replace("'", "`")
        If txtcompgivento.Text.Length > 150 Then
            Obj.complgivnto = txtComplaintGivento.Text.Substring(0, 150).Replace("'", "`")
        End If

        Obj.Franchise_DescManual = txtfranchise1.Text
        Obj.Service_Executive_DescManual = txtService1.Text
        Obj.status_date = clsCommon.GetPrintDate(txtCompleteDate.Value, "dd/MMM/yyyy hh:mm:ss tt")
        Obj.Pending_Reason_DescManual = txtPending1.Text
        If txtResponseTime.Text IsNot Nothing AndAlso clsCommon.myLen(txtResponseTime.Text) > 0 Then
            Obj.responsetym = txtResponseTime.Text
        Else
            Obj.responsetym = Nothing
        End If
        'Obj.responsetym = txtResponseTime.Text
        Obj.remarks = txtRemarks1.Text.Replace("'", "`")
        If txtremarks.Text.Length > 250 Then
            Obj.remarks = txtRemarks1.Text.Replace("'", "`").Substring(0, 250)
        End If
        Obj.franchiseyn = "Y"

        Obj.workno = txtWorkOrder1.Text
        Obj.billno = txtBillNo1.Text
        Obj.billamt = txtBillAmt1.Text
        Obj.addcharge = txtAdditionalCharge.Text

        If rbtnComleted.IsChecked = True Then
            Obj.compl_status = "C"
        ElseIf rbtnNotCompleted.IsChecked = True Then
            Obj.compl_status = "N"
        ElseIf rbtnPending.IsChecked = True Then
            Obj.compl_status = "P"
        End If
        'Dim ii As Integer = 0
        'Obj.sparepart = ""
        'Obj.spare_Qty = 0
        'Dim tempqty As Double = 0
        'Try
        '    For Each grow As GridViewRowInfo In gv2.Rows
        '        Obj.sparepart = Obj.sparepart + "," + gv2.Rows(ii).Cells(0).Value
        '        Obj.spare_Qty = clsCommon.myCdbl(gv2.Rows(ii).Cells(colItemQty).Value)
        '        tempqty = clsCommon.myCdbl(gv2.Rows(ii).Cells(colItemQty).Value)
        '        ii += 1
        '    Next
        'Catch ex As Exception
        'End Try
        'Try
        '    If Obj.sparepart.Substring(0, 1) = "," Then
        '        Obj.sparepart = Obj.sparepart.Substring(1, Obj.sparepart.Length - 1)
        '    End If
        'Catch ex As Exception
        'End Try
        'Obj.sparepart = Obj.sparepart.Replace("'", "")
        ObjList = New List(Of clscomplaintItemdetail)
        Dim objtr As clscomplaintItemdetail
        For Each grow As GridViewRowInfo In gv2.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                objtr = New clscomplaintItemdetail()
                objtr.Comp_id = txtcomid.Value
                objtr.item_code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objtr.item_desc = clsCommon.myCstr(grow.Cells(colItemName).Value)
                objtr.Unit_Cost = clsCommon.myCstr(grow.Cells(colItemCost).Value)
                objtr.Qty = clsCommon.myCstr(grow.Cells(colItemQty).Value)
                ObjList.Add(objtr)
            End If
        Next
        Obj.ObjList = ObjList

        If btnSave.Text <> "Save" Then
            Dim qry As String = "select count(*) from tspl_complaint_detail where comp_id='" + txtcomid.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check = 0 Then
                clsCommon.MyMessageBoxShow("No Data Found")
                Reset()
                Return
            End If
        End If


        Try
            Dim isSaved As Boolean = clscomplaintdetail.SaveData(Obj, IIf(btnSave.Text = "Save", True, False), Nothing)

            txtcomid.Value = Obj.comp_id
            If isSaved Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                btnSave.Text = "Update"
                btnCopy.Enabled = False
                btnPost.Enabled = True
                btnDelete.Enabled = True
                chkManual.Enabled = False
            Else
                btnSave.Text = "Save"
                btnPost.Enabled = False
                btnDelete.Enabled = False
                btnCopy.Enabled = True
                common.clsCommon.MyMessageBoxShow("Data Could Not Saved", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        'End If
    End Sub

    Public Sub LoadDGManual()
        gv2.DataSource = Nothing
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        ' gv2.Rows.AddNew()
        'Dim qry As String = "select item_code as [Item Code],item_desc as [Item Description] from tspl_item_master where (item_type='R' or item_type='O') order by item_code"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        'dt.Columns.Add("Status", GetType(Boolean))
        'gv1.DataSource = dt
        'gv1.Columns("item code").Width = 100
        'gv1.Columns("Item Description").Width = 250

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colItemCode
        repoICode.HeaderImage = My.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv2.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colItemName
        repoIName.Width = 200
        repoIName.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoIName)

        Dim repoICost As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICost.FormatString = ""
        repoICost.HeaderText = "Item Cost"
        repoICost.Name = colItemCost
        repoICost.Width = 80
        repoICost.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoICost)

        Dim repoIQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIQty.FormatString = ""
        repoIQty.HeaderText = "Item Quantity"
        repoIQty.Name = colItemQty
        repoIQty.Width = 80
        repoIQty.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoIQty)

        gv2.Rows.AddNew()
        gv2.AllowDeleteRow = True
        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        'gv1.ReadOnly = True
    End Sub
    Public Sub LoadApexDataManual()
        Try
            Dim dt As New DataTable()
            dt.Columns.Add("code", GetType(String))
            dt.Columns.Add("Name", GetType(String))
            Dim dr As DataRow = Nothing

            dr = dt.NewRow()
            dr("code") = ""
            dr("Name") = "Select"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("code") = "UW"
            dr("Name") = "Under Warranty"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("code") = "OW"
            dr("Name") = "Out Of Warranty"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("code") = "OF"
            dr("Name") = "On Your Freezer"
            dt.Rows.Add(dr)

            cboApex1.DataSource = dt
            cboApex1.DisplayMember = "name"
            cboApex1.ValueMember = "Code"
        Catch ex As Exception
        End Try
    End Sub

    Sub OpenSparePartsManual()
        Try
            Dim itemcode As String = ""
            Dim itemname As String = ""
            Dim ItemCost As String = "0"
            Dim XR As Integer = 0

            XR = gv2.CurrentCell.RowIndex
            itemcode = clsItemMaster.getFinder(" (item_type='R' or Item_type='O')", itemcode, True)
            itemname = clsItemMaster.GetItemName(itemcode, Nothing)
            ItemCost = clsDBFuncationality.getSingleValue("select cost from TSPL_ITEM_MASTER where Item_Code='" & itemcode & "'")
            gv2.Rows(XR).Cells(colItemCode).Value = itemcode
            gv2.Rows(XR).Cells(colItemName).Value = itemname
            gv2.Rows(XR).Cells(colItemCost).Value = ItemCost
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Sub gv2_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv2.CellValueChanged
        Try
            If Not IsinsideLoaded Then
                If Not Isvaluechanged Then
                    Isvaluechanged = True
                    If e.Column Is gv2.Columns(colItemCode) Then
                        OpenSparePartsManual()
                    End If
                    Isvaluechanged = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv2_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv2.CurrentColumnChanged
        If gv2.RowCount > 0 Then
            Dim intCurrRow As Integer = gv2.CurrentRow.Index
            If intCurrRow = gv2.Rows.Count - 1 Then
                gv2.Rows.AddNew()
                gv2.CurrentRow = gv2.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub rbtnComleted_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnComleted.ToggleStateChanged
        RadGroupBox5.Visible = False

        If rbtnComleted.IsChecked = True Then
            RadGroupBox5.Visible = True

        End If
    End Sub

    Private Sub rbtnPending_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnPending.ToggleStateChanged

    End Sub


    Private Sub txtSerialNo1_TextChanged(sender As Object, e As EventArgs) Handles txtSerialNo1.TextChanged
        Try
            If txtSerialNo1.Text <> "" Then
                Dim qry As String = "select count(*) from tspl_complaint_detail where comp_code='" + objCommonVar.CurrentCompanyCode + "' and item_code='" + txtassetcode.Value + "' and serial_no='" + txtSerialNo1.Text + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                rptdSerialNo.Text = "Serial No. Repeated " + clsCommon.myCstr(check) + " Times"
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class

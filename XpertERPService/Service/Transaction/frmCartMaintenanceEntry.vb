Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Imports System.IO
'Imports Outlook = Microsoft.Office.Interop.Outlook
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls

Public Class FrmCartMaintenanceEntry
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
#End Region

    Sub Reset()
        Try
            btnSave.Text = "Save"
            btnSave.Enabled = True
            btnDelete.Enabled = False
            btnPost.Enabled = False
            UsLock1.Status = ERPTransactionStatus.Pending
            txtdocno.Value = ""
            txtDate.Text = ""
            txtdesc.Text = ""
            rdall.Enabled = True
            rdselect.Enabled = True


            btngo.Enabled = True
            rdall.IsChecked = True
            rdselect.IsChecked = False
            cbgCustomer.Enabled = False

            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Rows.AddNew()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCartMaintenanceEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadCustomer()
        Dim qry As String = "select distinct  TSPL_VISI_MASTER.customer_id as [Customer Code],tspl_customer_master.customer_name as [Customer Name]," _
        & " Phone1 as [Phone No],Add1 as [Address],city_code as [City],Contact_Person_Name as [Contact Person],Tin_No as [Tin No],Lst_No as [Lst No] from tspl_visi_master left outer join tspl_customer_master on tspl_customer_master.cust_code=tspl_visi_master.customer_id where TSPL_VISI_MASTER.Customer_Id<>''"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        dt.Columns.Add("Status", GetType(Boolean))

        cbgCustomer.DataSource = dt

        cbgCustomer.Columns("Customer Code").Width = 100
        cbgCustomer.Columns("Customer Code").ReadOnly = True

        cbgCustomer.Columns("Customer Name").Width = 200
        cbgCustomer.Columns("Customer Name").ReadOnly = True

        cbgCustomer.Columns("Address").Width = 200
        cbgCustomer.Columns("Address").ReadOnly = True

        cbgCustomer.Columns("City").Width = 100
        cbgCustomer.Columns("City").ReadOnly = True

        cbgCustomer.Columns("Phone No").Width = 80
        cbgCustomer.Columns("Phone No").ReadOnly = True

        cbgCustomer.Columns("Contact Person").Width = 100
        cbgCustomer.Columns("Contact Person").Width = 200

        cbgCustomer.Columns("Tin No").Width = 80
        cbgCustomer.Columns("Tin No").ReadOnly = True

        cbgCustomer.Columns("Lst No").Width = 80
        cbgCustomer.Columns("Lst No").ReadOnly = True

        cbgCustomer.Rows.AddNew()
        cbgCustomer.AllowDeleteRow = True
        cbgCustomer.AllowAddNewRow = False
        cbgCustomer.ShowGroupPanel = False
        cbgCustomer.AllowColumnReorder = False
        cbgCustomer.AllowRowReorder = False
        cbgCustomer.EnableSorting = False
        cbgCustomer.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        cbgCustomer.MasterTemplate.ShowRowHeaderColumn = False
        cbgCustomer.EnableFiltering = True
    End Sub

    Private Sub FrmCartMaintenanceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        txtDate.Text = clsCommon.myCDate(clsCommon.GETSERVERDATE()).ToString("dd/MM/yyyy")
        LoadCustomer()

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")

        txtdocno.Focus()
        txtdocno.Select()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub SaveData()
        Try
            If AllowToSave() Then

                Dim arr As New List(Of clsCartMaintenanceEntry)

                Dim i As Integer = 0
                Dim customercode As String = ""
                If rdselect.IsChecked = True Then
                    Try
                        For i = 0 To cbgCustomer.Rows.Count - 1
                            If cbgCustomer.Rows(i).Cells("Status").IsCurrent = True Then
                                customercode = customercode + "," + cbgCustomer.Rows(i).Cells(0).Value
                            End If

                        Next
                    Catch ex As Exception
                    End Try


                    Try
                        If customercode.Substring(0, 1) = "," Then
                            customercode = customercode.Substring(1, customercode.Length - 1)
                        End If
                    Catch ex As Exception
                    End Try
                    customercode = customercode.Replace("'", "")
                End If
                For ii As Integer = 4 To gv1.Columns.Count - 1

                    For Each grow As GridViewRowInfo In gv1.Rows
                        Dim obj As New clsCartMaintenanceEntry()
                        obj.docno = txtdocno.Value
                        obj.newdocdate = clsCommon.myCDate(txtDate.Text)
                        obj.docdate = clsCommon.myCDate(txtDate.Text).ToString("dd/MM/yyyy")
                        obj.description = txtdesc.Text.Replace("'", "`")

                        If clsCommon.myLen(obj.description) > 300 Then
                            obj.description = obj.description.Substring(0, 300)
                        End If

                        obj.custcode = customercode
                        obj.assetno = gv1.Columns(ii).Name.ToString()
                        obj.sparecode = clsCommon.myCstr(grow.Cells(0).Value)
                        obj.values = clsCommon.myCstr(grow.Cells(ii).Value)
                        obj.rate = clsCommon.myCdbl(grow.Cells(2).Value)
                        obj.amt = clsCommon.myCdbl(grow.Cells(3).Value)

                        If clsCommon.myLen(obj.sparecode) > 0 Then
                            arr.Add(obj)
                        End If
                    Next
                Next
                Dim rrcode As String = ""
                If (clsCartMaintenanceEntry.SaveData(txtdocno.Value, IIf(btnSave.Text = "Save", True, False), arr, Nothing, rrcode)) Then
                    txtdocno.Value = rrcode
                    clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                    'LoadData(txtdocno.Value, NavigatorType.Current)
                    rdall.Enabled = False
                    rdselect.Enabled = False
                    cbgCustomer.Enabled = False
                    btngo.Enabled = False
                Else
                    Reset()
                    btnSave.Text = "Save"
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Public Function AllowToSave() As Boolean
        Try
            Dim i As Integer = 0
            Dim customercode As String = ""

            If rdselect.IsChecked = True Then
                Try
                    For i = 0 To cbgCustomer.Rows.Count - 1
                        If cbgCustomer.Rows(i).Cells("Status").IsCurrent = True Then
                            customercode = customercode + "," + cbgCustomer.Rows(i).Cells(0).Value
                        End If

                    Next
                Catch ex As Exception
                End Try


                Try
                    If customercode.Substring(0, 1) = "," Then
                        customercode = customercode.Substring(1, customercode.Length - 1)
                    End If
                Catch ex As Exception
                End Try
                customercode = customercode.Replace("'", "")
            End If

            If rdselect.IsChecked = True AndAlso clsCommon.myLen(customercode) = 0 AndAlso clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("Please Select Atleast One Customer", Me.Text)
                cbgCustomer.Focus()
                cbgCustomer.Select()
                Return False
            End If

            Dim RowCount As Integer = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                For jj As Integer = 0 To gv1.Rows.Count - 1

                    If jj = ii Then
                        'Continue For
                    Else
                        If clsCommon.myLen(gv1.Rows(ii).Cells(0).Value) > 0 AndAlso clsCommon.myLen(gv1.Rows(jj).Cells(0).Value) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(0).Value), clsCommon.myCstr(gv1.Rows(jj).Cells(0).Value)) = CompairStringResult.Equal Then
                                Dim Msg As String = " Same Spare Part Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                                Msg = Msg + Environment.NewLine + "Spare Part: " + clsCommon.myCstr(gv1.Rows(ii).Cells(0).Value)
                                common.clsCommon.MyMessageBoxShow(Msg)
                                Return False
                            End If
                        End If
                    End If
                Next
                If clsCommon.myLen(gv1.Rows(ii).Cells(0).Value) > 0 Then
                    RowCount += 1
                End If
            Next
            If RowCount <= 0 Then
                gv1.Focus()
                Throw New Exception("Please select at least one spare part in grid.")
            End If

            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtdocno.Value) = 0 Then
                clsCommon.MyMessageBoxShow("No Document Found For Posting,Please select Cart Maintenance Document No.", Me.Text)
                Return
            ElseIf clsCommon.myLen(txtdocno.Value) > 0 Then
                Dim qry As String = "select count(*) from TSPL_CART_MAINTENANCE where comp_code='" + objCommonVar.CurrentCompanyCode + "' and docno='" + txtdocno.Value + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    clsCommon.MyMessageBoxShow("No Document Found For Posting,Please select cart maintenance document no.", Me.Text)
                    Return
                End If

            End If

            Dim qry1 As String = "update TSPL_CART_MAINTENANCE set status='Y' where comp_code='" + objCommonVar.CurrentCompanyCode + "' and docno='" + txtdocno.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry1)
            common.clsCommon.MyMessageBoxShow("Cart Maintenance Of Document No. " + txtdocno.Value + " Posted Sucessfully", Me.Text)
            btnSave.Enabled = False
            btnPost.Enabled = False
            btnDelete.Enabled = False
            UsLock1.Status = ERPTransactionStatus.Approved

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim qry As String = "select count(*) from TSPL_CART_MAINTENANCE where docno='" + txtdocno.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check = 0 Then
            clsCommon.MyMessageBoxShow("No Data Found For Deletion", Me.Text)
            Return
        ElseIf Not (common.clsCommon.MyMessageBoxShow("Delete the Cart Maintenance Document No. " + txtdocno.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
            Return
        End If

        qry = "delete from TSPL_CART_MAINTENANCE where docno='" + txtdocno.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        clsDBFuncationality.ExecuteNonQuery(qry)
        common.clsCommon.MyMessageBoxShow("Data Deleted Sucessfully", Me.Text)
        Reset()

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rdall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdall.ToggleStateChanged, rdselect.ToggleStateChanged
        cbgCustomer.Enabled = rdselect.IsChecked
    End Sub

    Private Sub btngo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngo.Click
        Try

            Dim i As Integer = 0
            Dim customercode As String = ""
            If rdselect.IsChecked = True Then
                Try
                    For i = 0 To cbgCustomer.Rows.Count - 1
                        If cbgCustomer.Rows(i).Cells("Status").IsCurrent = True Then
                            customercode = customercode + "," + cbgCustomer.Rows(i).Cells(0).Value
                        End If
                    Next
                Catch ex As Exception
                End Try
                Try
                    If customercode.Substring(0, 1) = "," Then
                        customercode = customercode.Substring(1, customercode.Length - 1)
                    End If
                Catch ex As Exception
                End Try
                customercode = customercode.Replace("'", "")
            End If

            If rdselect.IsChecked = True AndAlso clsCommon.myLen(customercode) = 0 Then
                clsCommon.MyMessageBoxShow("Please Select Atleast One Customer For Maintenance Entry", Me.Text)
                Return
            End If

            Dim custcode As String = ""

            If clsCommon.myLen(customercode) > 0 Then
                custcode = " and tspl_visi_master.customer_id in ('" + customercode + "')"
            End If

            Dim qry As String = "select distinct tspl_visi_master.asset_no+ '(' + tspl_item_master.item_desc + '-' + tspl_visi_master.serial_no + ')' as asset_no,tspl_item_master.item_desc from tspl_visi_master " _
            & " INNER JOIN TSPL_ITEM_MASTER_CATEGORY IMC ON tspl_visi_master.Asset_No=IMC.Item_code AND upper(Item_Cagetory_Values)in ('CYCLE','HAND') " _
            & " left outer join tspl_item_master on tspl_item_master.item_code=tspl_visi_master.asset_no where 1=1 " + custcode + ""
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim strtotal As String = ""

            For Each dr As DataRow In dt.Rows

                Dim itemcode As String = clsCommon.myCstr(dr("asset_no"))
                strtotal = strtotal + "," + "[" + itemcode + "]"
            Next
            If strtotal.Length <= 0 Then
                clsCommon.MyMessageBoxShow("There is No Items For Spare Parts..")
                Exit Sub
            End If
            Try
                If strtotal.Substring(0, 1) = "," Then
                    strtotal = strtotal.Substring(1, strtotal.Length - 1)
                End If
            Catch ex As Exception
            End Try

            qry = "select * from (select '' as [Spare Code],'' as [Spare Description],0.00 as Rate,0.00 as totalamt,asset_no, 0 as Qty from TSPL_VISI_MASTER) as t pivot(max(Qty) for asset_no in (" + strtotal + ")) as p"
            gv1.DataSource = clsDBFuncationality.GetDataTable(qry)

            gv1.Columns("Spare Code").HeaderImage = My.Resources.search4
            gv1.Columns("Spare Code").TextImageRelation = TextImageRelation.TextBeforeImage
            gv1.Columns("Spare Code").Width = 80

            gv1.Columns("Spare Description").Width = 150
            gv1.Columns("Spare Description").ReadOnly = True

            gv1.Columns("Rate").HeaderText = "Rate"
            gv1.Columns("Rate").Width = 80
            gv1.Columns("rate").ReadOnly = False

            gv1.Columns("totalamt").HeaderText = "Net Amount"
            gv1.Columns("totalamt").Width = 80
            gv1.Columns("totalamt").ReadOnly = True

            gv1.AutoSize = True
            gv1.AllowDeleteRow = True
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Reset()
        LoadCustomer()
        txtDate.Text = clsCommon.GETSERVERDATE()
    End Sub
    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)


        Dim obj As New clsCartMaintenanceEntry()

        obj = clsCartMaintenanceEntry.GetData(strCode, NavTyep)
        Dim customercode As String = ""

        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.docno) > 0 Then
            cbgCustomer.DataSource = Nothing
            LoadCustomer()

            txtdocno.Value = obj.docno
            Try
                txtDate.Text = clsCommon.myCDate(obj.docdate)
            Catch ex As Exception
                txtDate.Text = obj.docdate
            End Try

            txtdesc.Text = obj.description

            '-------------------------------------------customer------------------------------
            Dim ii As Integer = 0
            Dim strspare() As String
            customercode = obj.custcode
            strspare = obj.custcode.Split(",")

            Dim j As Integer = strspare.Length
            rdall.IsChecked = True
            rdselect.IsChecked = False
            Dim jj As Integer = 0
            While (j > 0)
                For Each grow As GridViewRowInfo In cbgCustomer.Rows
                    Try
                        If cbgCustomer.Rows(ii).Cells(0).Value = strspare(jj) Then
                            cbgCustomer.Rows(ii).Cells(2).Value = True
                            rdselect.IsChecked = True
                            rdall.IsChecked = False

                        End If
                    Catch ex As Exception
                    End Try

                    ii += 1
                Next
                ii = 0
                j -= 1
                jj += 1
            End While
            '---------------------------------------------------------------------

            If obj.status = "Y" Then
                btnPost.Enabled = False
                btnSave.Enabled = False
                btnDelete.Enabled = False
                btnSave.Text = "Update"
                rdall.Enabled = False
                rdselect.Enabled = False
                cbgCustomer.Enabled = False
                btngo.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnPost.Enabled = True
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnSave.Text = "Update"
                rdall.Enabled = False
                rdselect.Enabled = False
                cbgCustomer.Enabled = False
                btngo.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Pending
            End If


            '------------------------------------------------------------grid data----------------------------------


            Dim strvalue As String = ""
            If clsCommon.myLen(customercode) > 0 Then
                strvalue = " and tspl_visi_master.customer_id in ('" + customercode + "')"
            End If

            Dim qry As String = "select distinct tspl_visi_master.asset_no+ '(' + tspl_item_master.item_desc + '-' + tspl_visi_master.serial_no + ')' as asset_no,tspl_item_master.item_desc from tspl_visi_master " _
             & " left outer join tspl_item_master on tspl_item_master.item_code=tspl_visi_master.asset_no where 1=1 " + strvalue + ""
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim strtotal As String = ""

            For Each dr As DataRow In dt.Rows
                Dim itemcode As String = clsCommon.myCstr(dr("asset_no"))
                strtotal = strtotal + "," + "[" + itemcode + "]"
            Next

            Try
                If strtotal.Substring(0, 1) = "," Then
                    strtotal = strtotal.Substring(1, strtotal.Length - 1)
                End If
            Catch ex As Exception
            End Try
            Try
                If strtotal.Substring(0, 3) = "[]," Then
                    strtotal = strtotal.Substring(3, strtotal.Length - 3)
                End If
            Catch ex As Exception
            End Try

            qry = "select * from (select distinct TSPL_CART_MAINTENANCE.asset_no,TSPL_CART_MAINTENANCE.spare_code as [Spare Code],TSPL_ITEM_MASTER.Item_Desc as [Spare Description],TSPL_CART_MAINTENANCE.rate,TSPL_CART_MAINTENANCE.net_amt,TSPL_CART_MAINTENANCE.asset_value from TSPL_CART_MAINTENANCE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_CART_MAINTENANCE.spare_code where TSPL_CART_MAINTENANCE.comp_code='" + objCommonVar.CurrentCompanyCode + "' and TSPL_CART_MAINTENANCE.docno='" + txtdocno.Value + "' ) as p pivot(min(asset_value) for asset_no in (" + strtotal + ")) as t"
            gv1.DataSource = clsDBFuncationality.GetDataTable(qry)

            gv1.Columns("Spare Code").HeaderImage = My.Resources.search4
            gv1.Columns("Spare Code").TextImageRelation = TextImageRelation.TextBeforeImage
            gv1.Columns("Spare Code").Width = 80

            gv1.Columns("Spare Description").Width = 150
            gv1.Columns("Spare Description").ReadOnly = True

            gv1.Columns("rate").HeaderText = "Rate"
            gv1.Columns("rate").Width = 80
            gv1.Columns("rate").ReadOnly = False

            gv1.Columns("net_amt").HeaderText = "Net Amount"
            gv1.Columns("net_amt").Width = 80
            gv1.Columns("net_amt").ReadOnly = False

            gv1.AutoSize = True
            gv1.AllowDeleteRow = True
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            gv1.Rows.NewRow()
        End If
    End Sub

    Private Sub txtdocno__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtdocno._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_CART_MAINTENANCE where docno='" + txtdocno.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtdocno.MyReadOnly = True
            ElseIf check <= 0 Then
                txtdocno.MyReadOnly = False
            End If

            LoadData(txtdocno.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtdocno__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtdocno._MYValidating
        Dim qry As String = "select distinct TSPL_CART_MAINTENANCE.docno as DocumentNo,TSPL_CART_MAINTENANCE.Description,TSPL_CART_MAINTENANCE.Date from TSPL_CART_MAINTENANCE"
        Dim whrClas As String = " TSPL_CART_MAINTENANCE.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Reset()
        LoadData(clsCommon.ShowSelectForm("CRTMNTID", qry, "DocumentNo", whrClas, txtdocno.Value, "DocumentNo", isButtonClicked), NavigatorType.Current)
    End Sub

    Sub OpenSparePartFinder(ByVal isButtonClick As Boolean)
        Try
            Dim qry As String = "select item_Code as Code,item_Desc as Description,Cost,structure_desc as [Structure Desc],unit_code as Unit,item_type as [Type Of Items],Rate from TSPL_ITEM_MASTER"
            Dim whrCls As String = " (item_type='R' or item_type='O')"
            gv1.CurrentRow.Cells(0).Value = clsCommon.ShowSelectForm("CRTMNFND", qry, "code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(0).Value), "code", isButtonClick)

            If clsCommon.myLen(gv1.CurrentRow.Cells(0).Value) <= 0 Then
                Return
            End If
            gv1.CurrentRow.Cells(1).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_desc from tspl_item_master where item_code='" + gv1.CurrentRow.Cells(0).Value + "'"))
            gv1.CurrentRow.Cells(2).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select distinct cost from tspl_item_master where item_code='" + gv1.CurrentRow.Cells(0).Value + "'"))
        Catch ex As Exception
        End Try

    End Sub

    Sub Calculateamt()
        Try
            Dim XR As Integer = 0
            Dim XC As Integer = 0
            Dim ii As Integer = 0
            Dim totalamt As Decimal
            Dim qty As Decimal

            XR = gv1.CurrentCell.RowIndex
            XC = gv1.Columns.Count

            Dim rate As Decimal = clsCommon.myCdbl(gv1.CurrentRow.Cells(2).Value)

            If rate > 0 Then
                For ii = 4 To XC - 1
                    qty = clsCommon.myCdbl(gv1.CurrentRow.Cells(ii).Value)
                    totalamt = clsCommon.myCdbl(totalamt) + (clsCommon.myCdbl(rate) * clsCommon.myCdbl(qty))
                Next
                gv1.CurrentRow.Cells(3).Value = totalamt
            Else
                gv1.CurrentRow.Cells(3).Value = 0.0
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(0) Then
                        OpenSparePartFinder(False)
                    End If

                    If ((e.Column IsNot gv1.Columns(0)) AndAlso (e.Column IsNot gv1.Columns(1)) AndAlso (e.Column IsNot gv1.Columns(2)) AndAlso (e.Column IsNot gv1.Columns(3))) Then
                        Calculateamt()
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If clsCommon.myLen(txtdocno.Value) > 0 Then
            If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Exit Sub
            Else
                Dim qry As String = " Select count(*) from TSPL_CART_MAINTENANCE where spare_code= '" + gv1.CurrentRow.Cells(0).Value + "' and docno='" + txtdocno.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' "
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
                    clsCommon.MyMessageBoxShow("Current File is in Use.")
                    e.Cancel = True
                    Exit Sub
                Else
                    qry = " delete from TSPL_CART_MAINTENANCE where spare_code= '" + gv1.CurrentRow.Cells(0).Value + "' and docno='" + txtdocno.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                End If
            End If
        End If
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub FrmCartMaintenanceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
End Class

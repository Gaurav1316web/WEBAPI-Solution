
Imports common
Imports System.Data.SqlClient



Public Class FrmInvoiceCrateLinerDetail
    Inherits FrmMainTranScreen
    'Dim trnsLstCustomer As New List(Of String)
    'Dim strCustomerCode As String = Nothing
    Dim dt1 As DataTable = New DataTable()
    'Dim Isrefreshed As Boolean = False    '' Variable for Validate the btnPost(Enable/Disable) and GridView
    'Dim IsSelected As Boolean = False     '' Variable for Validate the btnSelectAll(ChangeText)
    Public isNewEntry As Boolean = False
    Dim qry As String
    Dim dt As DataTable
    Dim count As Integer = 0
    Dim strNoOfRecord As String
    Dim trnsLst As New List(Of String)
    Dim arrUser As New ArrayList()
    Dim arrSelectedUser As New ArrayList()
    Dim strDocNo As String = Nothing
    Dim countPostedDoc As Integer = 0
    Public IsPostBack As Boolean = False
    Dim DtError As DataTable
    Dim dr As DataRow
    Public fromdate As DateTime
    Public Todate As DateTime
    Public ModuleName As String = ""
    Public Transaction As String = ""
    Public IsOpenPsted As Boolean
    Dim ButtonToolTip As New ToolTip()
    Dim isInsideLoad As Boolean = False
    '' List for Storing The Data(Which Is Selected) For Bulk Posting
    '==Sanjeet==========================
    Dim dtAuthen As DataTable
    Dim StrQuery As String = Nothing
    Dim ChkAllowBulkPosting As Double
    Dim arrLoc As String = Nothing
    Dim IsInsideLoadData As Boolean = True
    Dim ShowDairySaleModuleOnBulkPosting As Integer

    Const colStatus As String = "COLSTATUS"
    Const colLineNo As String = "COLLINE_NO"
    Const colCustomer As String = "COLCUSTOMER"
    Const colLocation As String = "COLLOCATION"
    Const colINVOICE_NO As String = "COLINVOICE_NO"
    Const colINVOICE_DATE As String = "COLINVOICE_DATE"
    Const colCRATE As String = "COLCRATE"
    Const colLINER As String = "COLLINER"

    Private Sub FrmInvoiceCrateLinerDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for Closing The Window")
        SetUserMgmtNew()

        LoadBlankGrid()

        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        txtDate.Value = clsCommon.GETSERVERDATE()

    End Sub

    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnPendingApproval1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function

        End If
        btnPost.Visible = MyBase.isPostFlag
        'btnsave.Visible = MyBase.isModifyFlag
        'btndelete.Visible = MyBase.isDeleteFlag
    End Sub


    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.AllowAddNewRow = False

        Dim repoStatus As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        'repoLineNo = New GridViewTextBoxColumn()
        repoStatus.FormatString = ""
        repoStatus.HeaderText = "Status"
        repoStatus.Name = colStatus
        repoStatus.Width = 50
        repoStatus.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoStatus)

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = "{0:n2}" '""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        'sanjay
        Dim repoCustomer As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustomer.FormatString = ""
        repoCustomer.HeaderText = "Customer"
        repoCustomer.Name = colCustomer
        repoCustomer.Width = 200
        repoCustomer.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCustomer)

        Dim repoLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocation.FormatString = ""
        repoLocation.HeaderText = "Location"
        repoLocation.Name = colLocation
        repoLocation.Width = 150
        repoLocation.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLocation)
        'sanjay

        Dim repoINVOICE_NO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoINVOICE_NO.FormatString = ""
        repoINVOICE_NO.HeaderText = "Invoice No"
        repoINVOICE_NO.Name = colINVOICE_NO
        repoINVOICE_NO.Width = 200
        repoINVOICE_NO.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoINVOICE_NO)

        Dim repoINVOICE_DATE As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoINVOICE_DATE.FormatString = "" ' "{0:d}" '"{0:MM/dd/yyyy}"
        repoINVOICE_DATE.HeaderText = "Invoice Date"
        repoINVOICE_DATE.Name = colINVOICE_DATE
        'repoINVOICE_DATE.CustomFormat = "dd-mm-yyyy"
        repoINVOICE_DATE.Width = 100
        repoINVOICE_DATE.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoINVOICE_DATE)

        Dim repoCrateQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCrateQty = New GridViewDecimalColumn()
        repoCrateQty.FormatString = "{0:n2}" '""
        repoCrateQty.HeaderText = "Crate Qty"
        repoCrateQty.Name = colCRATE
        repoCrateQty.Width = 100
        repoCrateQty.Minimum = 0
        repoCrateQty.ReadOnly = False
        repoCrateQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCrateQty)

        Dim repoLinerQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLinerQty = New GridViewDecimalColumn()
        repoLinerQty.FormatString = "{0:n2}" '""
        repoLinerQty.HeaderText = "Liner Qty"
        repoLinerQty.Name = colLINER
        repoLinerQty.Width = 100
        repoLinerQty.Minimum = 0
        repoLinerQty.ReadOnly = False
        repoLinerQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLinerQty)


        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = True
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40


    End Sub




#Region "Showing Details on GRID"
    'done by stuti on 18/10/2016 against ticket no - BM00000010089
    ''-------------------------------------------------------------------
    '' Function For Filling --------Module(Purchase Order)---------------
    ''-------------------------------------------------------------------

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            Dim intLine As Integer = 0
            gv1.Rows.Clear()
            txtDocNo.Value = ""
            If dtpFromDate.Value > dtpToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "'From date' Cann't Be Greater Than 'To Date'", Me.Text)
            Else
                qry = Nothing
                'ShowData()
                ',ROW_NUMBER() OVER (ORDER BY Document_Code) as [Line No]
                qry = "select CAST((0)as BIT) as Status" & _
                    ",TSPL_CUSTOMER_MASTER.Customer_Name as [Customer]" & _
                    ",TSPL_LOCATION_MASTER.Location_Desc As [Location]" & _
                    ",convert(varchar,Document_Date,103) as [Invoice Date]"
                qry += ",Document_Code as [Invoice No],CrateQty as [Crate Qty],Liner as [Liner Qty] from TSPL_SD_SALE_INVOICE_HEAD left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & _
                    "  left join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location " & _
                    " where Trans_Type='FS' and screen_type='' "

                qry += "and convert(date,Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and " & _
                        "convert(date,Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "

                If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                    qry += " and Bill_To_Location in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
                End If

                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    qry += " and TSPL_CUSTOMER_MASTER.Cust_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") " + Environment.NewLine
                End If

                qry += " ORDER BY Document_Date, Document_Code "

                If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

                    dt = clsDBFuncationality.GetDataTable(qry)

                    For i As Int16 = 0 To dt.Rows.Count - 1
                        gv1.Rows.AddNew()
                        intLine += 1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colStatus).Value = clsCommon.myCBool(dt.Rows(i)("Status").ToString)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCdbl(intLine)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomer).Value = clsCommon.myCstr(dt.Rows(i)("Customer").ToString)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLocation).Value = clsCommon.myCstr(dt.Rows(i)("Location").ToString)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colINVOICE_NO).Value = clsCommon.myCstr(dt.Rows(i)("Invoice No").ToString)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colINVOICE_DATE).Value = clsCommon.GetPrintDate(dt.Rows(i)("Invoice Date").ToString, "dd/MMM/yyyy") 'clsCommon.myCDate(dt.Rows(i)("Invoice Date").ToString)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCRATE).Value = clsCommon.myCdbl(dt.Rows(i)("Crate Qty").ToString)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLINER).Value = clsCommon.myCdbl(dt.Rows(i)("Liner Qty").ToString)
                    Next


                    'gv1.DataSource = dt
                    'gv1.MasterTemplate.SummaryRowsBottom.Clear()

                    If dt.Rows.Count <= 0 Then
                        lblNoOfRecords.Text = "No Record Found"
                    Else
                        strNoOfRecord = clsCommon.myCstr(dt.Rows.Count)
                        lblNoOfRecords.Text = "" + strNoOfRecord + " Records Found"
                    End If



                    'gv1Format()

                End If
                btnSave.Enabled = True
                btnPost.Enabled = False
                btnDelete.Enabled = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    Sub gv1Format()
        Me.gv1.MasterTemplate.Columns("Status").Width = 50      ''First Column
        'Me.gv1.MasterTemplate.Columns("Document Id").Width = 150    ''Second Column
        Dim count As Integer = gv1.MasterTemplate.Columns.Count
        For i As Integer = 2 To count - 1
            Me.gv1.MasterTemplate.Columns(i).Width = 120
        Next i
        'Me.gv1.MasterTemplate.Columns("Description").Width = 200    ''Last Column
        For j As Integer = 1 To count - 3
            Me.gv1.MasterTemplate.Columns(j).ReadOnly = True
        Next

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = True
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

#End Region


#Region "Posting"

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Try


            'trnsLst = New List(Of String)
            'Dim trnsNo As Integer
            'countPostedDoc = 0

            'For trnsNo = 0 To gv1.Rows.Count - 1
            '    If gv1.Rows(trnsNo).Cells("Status").Value = True Then
            '        trnsLst.Add(gv1.Rows(trnsNo).Cells("Document Id").Value)  '' Insert The Document_Id in The StringList
            '    End If
            'Next


            'If trnsLst.Count <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Plz. Select Atleast One Document")
            'Else

            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Plz. Select Document", Me.Text)
                Exit Sub
            End If

            If myMessages.postConfirm Then
                Try
                    clsCommon.ProgressBarShow()
                    SaveData(True)
                    'sanjay
                    'For j As Integer = 0 To trnsLst.Count - 1
                    '    Try
                    'strDocNo = trnsLst.Item(j)
                    clsInvoiceCrateLinerHead.PostData(txtDocNo.Value)
                    'countPostedDoc = countPostedDoc + 1 '  Added By [Pankaj Kumar]-for Counting Posted Documents, And Creating Error Record-18/06/2012
                    'Catch ex As Exception
                    '    dr = DtError.NewRow()
                    '    dr("Code") = strDocNo
                    '    dr("Error") = ex.Message
                    '    DtError.Rows.Add(dr)
                    'End Try
                    'Next
                    'sanjay

                    clsCommon.ProgressBarHide()

                    common.clsCommon.MyMessageBoxShow(Me, "Document Posted Successfully", Me.Text)
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                Catch ex As Exception
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                Finally
                    clsCommon.ProgressBarHide()
                End Try
            End If
            'End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


#End Region

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm()
    End Sub


    Sub closeForm()
        Me.Close()
    End Sub




    Private Sub FrmInvoiceCrateLinerDetail_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            closeForm()
        End If
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim strQry As String = ""
        strQry = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub


    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        AddNew()
    End Sub

    Sub AddNew()
        btnSave.Text = "Save"
        txtDocNo.Value = ""
        btnReset.Enabled = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = False
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtCustomer.arrValueMember = Nothing
        TxtMultiLocation.arrValueMember = Nothing
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        strNoOfRecord = clsCommon.myCstr(0)
        BlankAllControls()
        lblNoOfRecords.Text = "" + strNoOfRecord + " Records Found"
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'AllowToSave()
        SaveData(False)

        If clsCommon.myLen(txtDocNo.Value) > 0 Then
            btnPost.Enabled = True
        Else
            btnPost.Enabled = False
        End If
    End Sub

    Function AllowToSave() As Boolean
        Try
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Select()
                Return False
            End If
            trnsLst = New List(Of String)
            Dim trnsNo As Integer

            For trnsNo = 0 To gv1.Rows.Count - 1
                If gv1.Rows(trnsNo).Cells(colStatus).Value = True Then
                    trnsLst.Add(gv1.Rows(trnsNo).Cells(colINVOICE_NO).Value)  '' Insert The Document_Id in The StringList
                End If
            Next

            If trnsLst.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Plz. Select Atleast One Document", Me.Text)
                Return False
            End If

            Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function


    Sub SaveData(ByVal ChekPostBtn As Boolean)
        Try
            'Dim strQuery As String
            Dim intLine As Integer = 0
            Dim blnSave As Boolean = False
            If (AllowToSave()) Then
                Dim obj As New clsInvoiceCrateLinerHead

                obj.DOCUMENT_NO = txtDocNo.Value
                obj.DOCUMENT_DATE = txtDate.Value

                obj.Arr = New List(Of clsInvoiceCrateLinerDetail)



                For Each grow As GridViewRowInfo In gv1.Rows
                    If (clsCommon.myCBool(grow.Cells(colStatus).Value) = True) Then
                        intLine += 1
                        Dim objTr As New clsInvoiceCrateLinerDetail()
                        'If (clsCommon.myCdbl(grow.Cells(colLineNo).Value)) > 0 Then
                        objTr.LINE_NO = intLine     'clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        objTr.DOCUMENT_NO = clsCommon.myCstr(obj.DOCUMENT_NO)
                        'objTr.INVOICE_DATE = clsCommon.myCDate(grow.Cells(2).Value)
                        'objTr.INVOICE_NO = clsCommon.myCstr(grow.Cells(2).Value)
                        'objTr.CRATE = clsCommon.myCdbl(grow.Cells(colCRATE).Value)
                        'objTr.LINER = clsCommon.myCdbl(grow.Cells(colLINER).Value)

                        objTr.INVOICE_NO = clsCommon.myCstr(grow.Cells(colINVOICE_NO).Value)
                        objTr.INVOICE_DATE = clsCommon.myCDate(grow.Cells(colINVOICE_DATE).Value)
                        objTr.CRATE = clsCommon.myCdbl(grow.Cells(colCRATE).Value)
                        objTr.LINER = clsCommon.myCdbl(grow.Cells(colLINER).Value)

                        obj.Arr.Add(objTr)
                    End If
                    'End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                    Return
                End If

                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    isNewEntry = False
                Else
                    isNewEntry = True
                End If

                If (obj.SaveData(obj, isNewEntry)) Then

                    If ChekPostBtn = False Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.DOCUMENT_NO, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            qry = "select count(*) from TSPL_INVOICE_CRATE_LINER_HEAD where Document_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            If count = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
            Else
                LoadData(txtDocNo.Value, NavType)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        qry = "SELECT Document_No as Code, CONVERT(varchar(10), Document_Date,103) as Date FROM TSPL_INVOICE_CRATE_LINER_HEAD "
        Dim whrClas As String = ""
        'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '    whrClas = " Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        'End If
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT count(*) FROM TSPL_INVOICE_CRATE_LINER_HEAD"))
        If count = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
        Else
            LoadData(clsCommon.ShowSelectForm("Docfnde", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
        End If
    End Sub

    Sub BlankAllControls()
        txtDocNo.Value = ""
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtCustomer.arrValueMember = Nothing
        TxtMultiLocation.arrValueMember = Nothing
        gv1.Rows.Clear()
        gv1.DataSource = Nothing
        'gv1.Rows.AddNew()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            BlankAllControls()
            LoadBlankGrid()
            Dim obj As New clsInvoiceCrateLinerHead
            obj = clsInvoiceCrateLinerHead.GetData(strCode, NavTyep)
            'If clsCommon.myLen(obj.DOCUMENT_NO) = 0 Then
            '    Exit Sub
            'End If
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.DOCUMENT_NO) > 0) Then
                txtDate.Value = obj.DOCUMENT_DATE
                txtDocNo.Value = obj.DOCUMENT_NO
                IsInsideLoadData = True
                isNewEntry = False
                btnSave.Text = "Update"
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsInvoiceCrateLinerDetail In obj.Arr
                        gv1.Rows.AddNew()
                        'CAST(TSPL_PURCHASE_ORDER_HEAD.Status as BIT ) as Status
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colStatus).Value = True
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.LINE_NO
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomer).Value = objTr.CUSTOMER
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLocation).Value = objTr.LOCATION
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colINVOICE_DATE).Value = clsCommon.GetPrintDate(objTr.INVOICE_DATE, "dd/MMM/yyyy") 'objTr.INVOICE_DATE
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colINVOICE_NO).Value = objTr.INVOICE_NO
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCRATE).Value = objTr.CRATE
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLINER).Value = objTr.LINER
                    Next
                End If
                If clsCommon.myCdbl(obj.POSTED) = 1 Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                End If

                strNoOfRecord = clsCommon.myCstr(gv1.Rows.Count)
                lblNoOfRecords.Text = "" + strNoOfRecord + " Records Found"
            End If
            'If clsCommon.myLen(txtDocNo.Value) > 0 Then
            '    btnPost.Enabled = True
            'Else
            '    btnPost.Enabled = False
            'End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtMultiLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER "
        TxtMultiLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Pro", qry, "Code", "Name", TxtMultiLocation.arrValueMember, TxtMultiLocation.arrDispalyMember)
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsInvoiceCrateLinerHead.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class





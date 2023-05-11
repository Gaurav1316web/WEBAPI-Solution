'--------Created By Richa 02/12/2019 Against Ticket No VIJ/09/12/19-000110

Imports System.Data.SqlClient
Imports common
Imports System.IO


Public Class FrmAcknowledgeOfGRN
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim isFlag As Boolean = False
    Public Const colSlNo As String = "SLNO"
    Public Const colSaleInvoice As String = "colSaleInvoice"
    Public Const colSaleInvoiceDate As String = "colSaleInvoiceDate"
    Public Const colremarks As String = "colremarks"
    Dim arrLoc As String = Nothing
    Public Shared Alocation As String = Nothing
    Dim Qry As String
#End Region
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmAcknowledgeOfGRN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        End If
    End Sub
    Private Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub FrmAcknowledgeOfGRN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N New Transaction")
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmDispatchBulkSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
     
    End Sub
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
                Alocation = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try
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
        itemCode.HeaderText = "Sale Invoice No"
        itemCode.Name = colSaleInvoice
        itemCode.Width = 100
        itemCode.ReadOnly = False
        itemCode.WrapText = True
        itemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(itemCode)

        Dim repoSaleInvoiceDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoSaleInvoiceDate.Format = DateTimePickerFormat.Custom
        repoSaleInvoiceDate.CustomFormat = "dd-MM-yyyy"
        repoSaleInvoiceDate.HeaderText = "Sale Invoice Date"
        repoSaleInvoiceDate.WrapText = True
        repoSaleInvoiceDate.FormatString = "{0:d}"
        repoSaleInvoiceDate.Name = colSaleInvoiceDate
        repoSaleInvoiceDate.ReadOnly = True
        repoSaleInvoiceDate.Width = 80
        repoSaleInvoiceDate.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoSaleInvoiceDate)

        Dim itemDesc As New GridViewTextBoxColumn()
        itemDesc.FormatString = ""
        itemDesc.HeaderText = "Remarks"
        itemDesc.Name = colremarks
        itemDesc.Width = 320
        itemDesc.ReadOnly = False
        itemDesc.WrapText = True
        itemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(itemDesc)

       
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.AllowRowReorder = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = False
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        gv1.AllowColumnReorder = True
        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

        lineNo = Nothing
        itemCode = Nothing
        itemDesc = Nothing
    End Sub

    Sub Reset()
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
        txtDocNo.Value = ""
        btndelete.Enabled = False
        btnPost.Enabled = True
        btnsave.Enabled = True
        loadBlankItemGrid()
        ReStoreGridLayout()
        isNewEntry = True
        LOCATIONRIGTHS()
        FndLocation.Value = ""
        lblLocationName.Text = ""
        fndCustomerNo.Value = ""
        lblCustomerName.Text = ""
        TxtRemarks.Text = ""
        gv1.Rows.AddNew()
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                txtDate.Select()
                Return False
            End If
            If clsCommon.myLen(fndCustomerNo.Value) <= 0 Then
                fndCustomerNo.Focus()
                Throw New Exception("Customer cannot be left blank")
            End If
            If clsCommon.myLen(FndLocation.Value) <= 0 Then
                FndLocation.Focus()
                Throw New Exception("Location cannot be left blank")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Sub SaveData()
        Dim totalqty As Decimal = 0
        Dim objApproval As New clsApply_Approval()
        Dim obj As New clsAcknowledgementOfGRN()
        Dim objTr As New clsAcknowledgementOfGRN_Dtail
        Try

            If AllowToSave() Then

                obj.ACKNOWLEDGEMENT_No = txtDocNo.Value
                obj.ACKNOWLEDGEMENT_Date = txtDate.Value
                obj.Customer_Code = clsCommon.myCstr(fndCustomerNo.Value)
                obj.Location_Code = clsCommon.myCstr(FndLocation.Value)
                obj.Remarks = clsCommon.myCstr(TxtRemarks.Text)
            
                obj.arrAcknowledgementOfGRN = New List(Of clsAcknowledgementOfGRN_Dtail)

                For Each grow As GridViewRowInfo In gv1.Rows
                    objTr = New clsAcknowledgementOfGRN_Dtail()
                    objTr.ACKNOWLEDGEMENT_No = clsCommon.myCstr(obj.ACKNOWLEDGEMENT_No)
                    objTr.SNo = clsCommon.myCdbl(grow.Cells(colSlNo).Value)
                    objTr.Sale_Invoice_No = clsCommon.myCstr(grow.Cells(colSaleInvoice).Value)
                    objTr.Sale_Invoice_Date = clsCommon.myCDate(grow.Cells(colSaleInvoiceDate).Value)
                    objTr.Remarks = clsCommon.myCstr(grow.Cells(colremarks).Value)
                    If clsCommon.myLen(objTr.Sale_Invoice_No) > 0 Then
                        obj.arrAcknowledgementOfGRN.Add(objTr)
                    End If
                Next
                If (clsAcknowledgementOfGRN.SaveData(obj, isNewEntry)) Then
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                        txtDocNo.Value = obj.ACKNOWLEDGEMENT_No
                        LoadData(obj.ACKNOWLEDGEMENT_No, NavigatorType.Current)
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
        Dim dt As DataTable = Nothing
        Dim obj As clsAcknowledgementOfGRN = Nothing
        Try
            obj = clsAcknowledgementOfGRN.GetData(strCode, NavTyep)

            isInsideLoadData = True
            If obj IsNot Nothing Then
                isNewEntry = False
                txtDocNo.Value = obj.ACKNOWLEDGEMENT_No
                txtDate.Value = obj.ACKNOWLEDGEMENT_Date
                fndCustomerNo.Value = obj.Customer_Code
                lblCustomerName.Text = obj.Customer_Name
                FndLocation.Value = obj.Location_Code
                lblLocationName.Text = obj.Location_Desc
                TxtRemarks.Text = obj.Remarks
                loadBlankItemGrid()
                gv1.Rows.AddNew()
                If obj.arrAcknowledgementOfGRN IsNot Nothing AndAlso obj.arrAcknowledgementOfGRN.Count > 0 Then
                    For Each objTr As clsAcknowledgementOfGRN_Dtail In obj.arrAcknowledgementOfGRN
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = clsCommon.myCdbl(objTr.SNo)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSaleInvoice).Value = clsCommon.myCstr(objTr.Sale_Invoice_No)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSaleInvoiceDate).Value = clsCommon.myCDate(objTr.Sale_Invoice_Date)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colremarks).Value = clsCommon.myCstr(objTr.Remarks)
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
            dt = Nothing
            obj = Nothing
        End Try
    End Sub
    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD where ACKNOWLEDGEMENT_No='" + txtDocNo.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocNo.MyReadOnly = False
            End If

            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = " select TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.ACKNOWLEDGEMENT_No,convert(varchar,TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.ACKNOWLEDGEMENT_Date,103) as Date,TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.Remarks,TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.Customer_Code,TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.Location_Code,tspl_location_master.Location_desc,TSPL_Customer_Master.Customer_Name,case when TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD left outer join tspl_location_master on tspl_location_master.Location_Code= TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.Location_Code left outer join TSPL_Customer_Master on TSPL_Customer_Master.Cust_Code= TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.Customer_Code "
        txtDocNo.Value = clsCommon.ShowSelectForm("ACKNOWLEDGEMENT_GRN", qry, "ACKNOWLEDGEMENT_No", " TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.Location_Code in (" + arrLoc + ")", txtDocNo.Value, "", isButtonClicked)
        LoadData(txtDocNo.Value, NavigatorType.Current)
        qry = Nothing
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Reset()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (clsAcknowledgementOfGRN.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Dim msg As String = Nothing
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing

        Dim desc As String = Nothing
        Try

         
            isFlag = True
            If (myMessages.postConfirm()) Then
                SaveData()
                If (clsAcknowledgementOfGRN.PostData(MyBase.Form_ID, arrLoc, txtDocNo.Value)) Then
                    msg = "Successfully posted"
                    common.clsCommon.MyMessageBoxShow(msg)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isFlag = False
            msg = Nothing
            qry = Nothing
            dt = Nothing
            desc = Nothing
        End Try
    End Sub
    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID & "gv1", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
                obj = Nothing
            End If

        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RDSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDSaveLayout.Click

        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID & "gv1"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                gv1.MasterTemplate.FilterDescriptors.Clear()

                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If

    End Sub

    Private Sub RDDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gv1", objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
    Private Sub fndCustomerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCustomerNo._MYValidating
        fndCustomerNo.Value = clsCustomerMaster.getFinder("", fndCustomerNo.Value, isButtonClicked)
        lblCustomerName.Text = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code  ='" + fndCustomerNo.Value + "' ")
    End Sub

    Private Sub FndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name,Loc_Short_Name as [Short Name] from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = "  Location_Type='Physical' and CSA_Type='N' and Is_Section='N' and Is_Sub_Location='N'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        FndLocation.Value = clsCommon.ShowSelectForm("AckOFGRN", qry, "Code", WhrCls, FndLocation.Value, "Code", isButtonClicked)
        lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + FndLocation.Value + "'"))

    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen And gv1.CurrentRow.Index >= 0 Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colSaleInvoice) Then
                        loadSaleInvoice()
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub loadSaleInvoice()
        Try
            Dim qry As String = "select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Code,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103 ) as Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as Customer,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location  as Location from  TSPL_SD_SALE_INVOICE_HEAD  " & Environment.NewLine
            Dim strwhrcls As String = "  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type in ('FS','PS')  " & Environment.NewLine & _
            " and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and TSPL_SD_SALE_INVOICE_HEAD.Status=1 " & Environment.NewLine & _
            " AND Document_Code NOT IN (SELECT DISTINCT INVOICE_CODE  fROM TSPL_SD_SALE_RETURN_DETAIL " & Environment.NewLine & _
            " Union All " & Environment.NewLine & _
            " SELECT DISTINCT Sale_Invoice_No  fROM TSPL_ACKNOWLEDGEMENT_OF_GRN_DETAIL where ACKNOWLEDGEMENT_No<>'" & clsCommon.myCstr(txtDocNo.Value) & "' " & Environment.NewLine & _
            "   ) AND TSPL_SD_SALE_INVOICE_HEAD.Status =1"

            If clsCommon.myLen(fndCustomerNo.Value) > 0 Then
                strwhrcls += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code='" + fndCustomerNo.Value + "'"
            End If
            If clsCommon.myLen(FndLocation.Value) > 0 Then
                strwhrcls += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location='" + FndLocation.Value + "'"
            End If

            gv1.CurrentRow.Cells(colSaleInvoice).Value = clsCommon.ShowSelectForm("InvoiceFinderForAcknowledgement", qry, "Code", strwhrcls, gv1.CurrentRow.Cells(colSaleInvoice).Value, "Date", True)
            gv1.CurrentRow.Cells(colSaleInvoiceDate).Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Document_Date from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colSaleInvoice).Value) & "' "))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            If gv1.CurrentRow.Index >= 0 Then
                Dim intCurrRow As Integer = gv1.CurrentRow.Index
                gv1.CurrentRow.Cells(colSlNo).Value = clsCommon.myCdbl(intCurrRow + 1)
                If intCurrRow = gv1.Rows.Count - 1 Then
                    gv1.Rows.AddNew()
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                End If
            End If
        End If
    End Sub

  
End Class

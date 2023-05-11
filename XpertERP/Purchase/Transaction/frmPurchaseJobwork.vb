Imports common
Imports System.Data.SqlClient


Public Class frmPurchaseJobwork
    Inherits FrmMainTranScreen
 
    Dim arrUser As New ArrayList()
 
    Dim ButtonToolTip As New ToolTip()
    Dim isInsideLoad As Boolean = False
    Dim FORMTYPE As String = Nothing
    Dim arrLoc As String = Nothing
    Const colDocCode As String = "COLDOC"
    Const colbillNo As String = "COLBILLNO"
    Const colbillDate As String = "COLBILLDATE"
    Const colPOType As String = "COLPOTYPE"
    Const colIName As String = "COLINAME"
    Const colStatus As String = "COLSTATUS"
    Public Const RowTypeYes As String = "Yes"
    Public Const RowTypeNo As String = "No"
    Const colSelect As String = "Select"
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub

    Private Sub frmPurchaseJobwork_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnPost, "TSPL_VENDOR_INVOICE_HEAD " + Environment.NewLine + _
                                                 "TSPL_VENDOR_INVOICE_DETAIL " + Environment.NewLine + _
                                                 "TSPL_REMITTANCE " + Environment.NewLine + _
                                                 "TSPL_AP_Invoice_Asset_EMI_Details " + Environment.NewLine + _
                                                 "TSPL_AP_Invoice_Advance_Interest " + Environment.NewLine + _
                                                 "TSPL_PROVISION_ENTRY_KNOCKOFF " + Environment.NewLine + _
                                                 "TSPL_JOURNAL_MASTER " + Environment.NewLine + _
                                                 "TSPL_JOURNAL_DETAILS " + Environment.NewLine + _
                                                 "TSPL_ADJUSTMENT_HEADER " + Environment.NewLine + _
                                                 "TSPL_ADJUSTMENT_DETAIL " + Environment.NewLine + _
                                                 "TSPL_PROVISION_ENTRY " + Environment.NewLine + _
                                                 "TSPL_ACQUISITION_DETAIL ")
        End If
    End Sub
    Private Sub FrmPendingAproval_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LOCATIONRIGTHS()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for Closing The Window")
        SetUserMgmtNew()
        btnPost.Enabled = True
        txtTotalDOc.ReadOnly = True

    End Sub

    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                Dim check As Integer = 0
                check = clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where type='Plant' and location_code='" + obj.Default_LocCode + "'")
                If check > 0 Then

                End If
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub
    
    Private Sub SetUserMgmtNew()

        MyBase.SetUserMgmt(clsUserMgtCode.mbtnPendingApproval1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPost.Visible = MyBase.isPostFlag
      
    End Sub


    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()
        gv.AllowAddNewRow = False

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        gv.MasterTemplate.Columns.Add(repoSelect)

        Dim repoDocCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocCode.FormatString = ""
        repoDocCode.HeaderText = "Doc Code"
        repoDocCode.Name = colDocCode
        repoDocCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoDocCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoDocCode.Width = 100
        gv.MasterTemplate.Columns.Add(repoDocCode)

        Dim repoPOType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPOType.FormatString = ""
        repoPOType.HeaderText = "PO Type"
        repoPOType.Name = colPOType
        repoPOType.Width = 80
        gv.MasterTemplate.Columns.Add(repoPOType)

        Dim repoBillNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBillNo.FormatString = ""
        repoBillNo.HeaderText = "Bill No"
        repoBillNo.Name = colbillNo
        repoBillNo.Width = 80
        gv.MasterTemplate.Columns.Add(repoBillNo)

        Dim repoBillDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoBillDate.FormatString = "{0:d}"
        repoBillDate.CustomFormat = "dd/MM/yyyy"
        repoBillDate.Format = DateTimePickerFormat.Custom
        repoBillDate.HeaderText = "Bill Date"
        repoBillDate.Name = colbillDate
        repoBillDate.Width = 80
        gv.MasterTemplate.Columns.Add(repoBillDate)

        Dim repoStatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoStatus.FormatString = ""
        repoStatus.HeaderText = "Status"
        repoStatus.Name = colStatus
        repoStatus.Width = 50
        repoStatus.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoStatus.DataSource = GetItemType()
        repoStatus.ValueMember = "Code"
        repoStatus.DisplayMember = "Code"
        gv.MasterTemplate.Columns.Add(repoStatus)

        gv.AllowDeleteRow = False
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = True
        gv.ShowFilteringRow = True
        gv.EnableFiltering = True
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False


    End Sub

    Private Function GetItemType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = RowTypeYes
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = RowTypeNo
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm()
    End Sub

    Sub closeForm()
        Me.Close()
    End Sub


    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If gv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data found", Me.Text)
            End If
            Dim isPosted As Boolean = False
            Dim DocCode As String = ""
            Dim ItemCode As String = ""
            For ii As Integer = 0 To gv.Rows.Count - 1

                If clsCommon.myCBool(gv.Rows(ii).Cells(colSelect).Value) Then
                    DocCode = clsCommon.myCstr(gv.Rows(ii).Cells(colDocCode).Value)
                    clsDBFuncationality.ExecuteNonQuery("Update TSPL_PURCHASE_ORDER_HEAD set Status=1, Modify_by='" & objCommonVar.CurrentUserCode & "' ,Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "' where PurchaseOrder_No='" & DocCode & "' ")
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, DocCode, "TSPL_PURCHASE_ORDER_HEAD", "PurchaseOrder_No", Nothing)
                    isPosted = True
                    If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colStatus).Value), "Yes") = CompairStringResult.Equal Then
                        clsPurchaseJobWork.CreateAPInvoice(DocCode, FORMTYPE)
                    End If

                End If
                If isPosted Then
                    clsCommon.MyMessageBoxShow("Successfully Post")
                End If
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnload_Click(sender As Object, e As EventArgs) Handles btnload.Click
        Try
            LoadBlankGrid()
            Dim dt As DataTable = Nothing
            Dim qry As String = " select TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,case when TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type='J' then 'Job Work' end as PurchaseOrder_Type from TSPL_PURCHASE_ORDER_HEAD where TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type='J'"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv.Rows.AddNew()
                    gv.CurrentRow.Cells(colDocCode).Value = clsCommon.myCstr(dr("PurchaseOrder_No"))
                    gv.CurrentRow.Cells(colPOType).Value = clsCommon.myCstr(dr("PurchaseOrder_Type"))
                    gv.ReadOnly = False
                    gv.Columns(colDocCode).ReadOnly = True
                    gv.Columns(colPOType).ReadOnly = True
                Next
                txtTotalDOc.Text = dt.Rows.Count.ToString()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        txtTotalDOc.Text = ""
    End Sub
  
End Class





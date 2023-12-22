
Imports common
Imports System.Data.SqlClient
Imports System.IO


Public Class rptCrateLinerReport
    Inherits FrmMainTranScreen
    Dim dt1 As DataTable = New DataTable()
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
    Dim arrLoc As String = Nothing
    Dim IsInsideLoadData As Boolean = True

    'Const colStatus As String = "COLSTATUS"
    'Const colLineNo As String = "COLLINE_NO"
    'Const colINVOICE_NO As String = "COLINVOICE_NO"
    'Const colINVOICE_DATE As String = "COLINVOICE_DATE"
    'Const colCRATE As String = "COLCRATE"
    'Const colLINER As String = "COLLINER"

    Private Sub rptCrateLinerReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for Closing The Window")
        SetUserMgmtNew()
        'LoadBlankGrid()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnPendingApproval1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function

        End If
    End Sub


    'Sub LoadBlankGrid()
    '    gv1.Rows.Clear()
    '    gv1.Columns.Clear()
    '    gv1.AllowAddNewRow = False

    '    Dim repoStatus As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
    '    'repoLineNo = New GridViewTextBoxColumn()
    '    repoStatus.FormatString = ""
    '    repoStatus.HeaderText = "Status"
    '    repoStatus.Name = colStatus
    '    repoStatus.Width = 50
    '    repoStatus.ReadOnly = False
    '    gv1.MasterTemplate.Columns.Add(repoStatus)

    '    Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    'repoLineNo = New GridViewTextBoxColumn()
    '    repoLineNo.FormatString = "{0:n2}" '""
    '    repoLineNo.HeaderText = "Line No"
    '    repoLineNo.Name = colLineNo
    '    repoLineNo.Width = 50
    '    repoLineNo.ReadOnly = True
    '    repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gv1.MasterTemplate.Columns.Add(repoLineNo)


    '    Dim repoINVOICE_NO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repoINVOICE_NO.FormatString = ""
    '    repoINVOICE_NO.HeaderText = "Invoice No"
    '    repoINVOICE_NO.Name = colINVOICE_NO
    '    repoINVOICE_NO.Width = 200
    '    repoINVOICE_NO.ReadOnly = True
    '    gv1.MasterTemplate.Columns.Add(repoINVOICE_NO)

    '    Dim repoINVOICE_DATE As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repoINVOICE_DATE.FormatString = "" ' "{0:d}" '"{0:MM/dd/yyyy}"
    '    repoINVOICE_DATE.HeaderText = "Invoice Date"
    '    repoINVOICE_DATE.Name = colINVOICE_DATE
    '    'repoINVOICE_DATE.CustomFormat = "dd-mm-yyyy"
    '    repoINVOICE_DATE.Width = 100
    '    repoINVOICE_DATE.ReadOnly = True
    '    gv1.MasterTemplate.Columns.Add(repoINVOICE_DATE)

    '    Dim repoCrateQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoCrateQty = New GridViewDecimalColumn()
    '    repoCrateQty.FormatString = "{0:n2}" '""
    '    repoCrateQty.HeaderText = "Crate Qty"
    '    repoCrateQty.Name = colCRATE
    '    repoCrateQty.Width = 100
    '    repoCrateQty.Minimum = 0
    '    repoCrateQty.ReadOnly = False
    '    repoCrateQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gv1.MasterTemplate.Columns.Add(repoCrateQty)

    '    Dim repoLinerQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoLinerQty = New GridViewDecimalColumn()
    '    repoLinerQty.FormatString = "{0:n2}" '""
    '    repoLinerQty.HeaderText = "Liner Qty"
    '    repoLinerQty.Name = colLINER
    '    repoLinerQty.Width = 100
    '    repoLinerQty.Minimum = 0
    '    repoLinerQty.ReadOnly = False
    '    repoLinerQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gv1.MasterTemplate.Columns.Add(repoLinerQty)


    '    gv1.AllowAddNewRow = False
    '    gv1.ShowGroupPanel = False
    '    gv1.AllowColumnReorder = True
    '    gv1.AllowRowReorder = False
    '    gv1.EnableSorting = False
    '    gv1.EnableFiltering = True
    '    gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
    '    gv1.MasterTemplate.ShowRowHeaderColumn = False
    '    gv1.TableElement.TableHeaderHeight = 40


    'End Sub




#Region "Showing Details on GRID"
    'done by stuti on 18/10/2016 against ticket no - BM00000010089
    ''-------------------------------------------------------------------
    '' Function For Filling --------Module(Purchase Order)---------------
    ''-------------------------------------------------------------------

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv1
            'Dim intLine As Integer = 0
            'gv1.Rows.Clear()
            gv1.DataSource = Nothing
            If dtpFromDate.Value > dtpToDate.Value Then
                common.clsCommon.MyMessageBoxShow("'From date' Cann't Be Greater Than 'To Date'", Me.Text)
            Else
                qry = Nothing
                'ShowData()
                ',ROW_NUMBER() OVER (ORDER BY Document_Code) as [Line No]
                'qry = "select CAST((0)as BIT) as Status,convert(varchar,Document_Date,103) as [Invoice Date]"
                'qry += ",Document_Code as [Invoice No],CrateQty as [Crate Qty],Liner as [Liner Qty] from TSPL_SD_SALE_INVOICE_HEAD left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code where Trans_Type='FS' and screen_type='' "

                qry = "select convert(varchar,TSPL_INVOICE_CRATE_LINER_HEAD.DOCUMENT_DATE,103) as [Document Date] " & _
                 ",TSPL_INVOICE_CRATE_LINER_HEAD.DOCUMENT_NO as [Document No] " & _
                ",TSPL_CUSTOMER_MASTER.Customer_Name as [Customer] " & _
                 ",TSPL_LOCATION_MASTER.Location_Desc As [Location] " & _
                 ",TSPL_INVOICE_CRATE_LINER_DETAIL.INVOICE_NO as [Invoice No] " & _
                 ",(select sum(TSPL_SD_SALE_INVOICE_DETAIL.Crate) from TSPL_SD_SALE_INVOICE_DETAIL where TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_INVOICE_CRATE_LINER_DETAIL.INVOICE_NO) as [Crate Qty] " & _
                 ",TSPL_INVOICE_CRATE_LINER_DETAIL.Crate as [Updated Crate Qty] " & _
                 ",TSPL_INVOICE_CRATE_LINER_DETAIL.Liner as [Liner Qty] " & _
                " from TSPL_INVOICE_CRATE_LINER_DETAIL " & _
                 " left join TSPL_INVOICE_CRATE_LINER_HEAD on TSPL_INVOICE_CRATE_LINER_DETAIL.DOCUMENT_NO=TSPL_INVOICE_CRATE_LINER_HEAD.DOCUMENT_NO " & _
                 " left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_INVOICE_CRATE_LINER_DETAIL.INVOICE_NO " & _
                 " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & _
                 " left join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location " & _
                 " where TSPL_INVOICE_CRATE_LINER_HEAD.posted=1  "

                qry += "and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and " & _
                        "convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "

                If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
                End If

                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") " + Environment.NewLine
                End If

                qry += " ORDER BY TSPL_INVOICE_CRATE_LINER_DETAIL.INVOICE_NO "

                If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

                    dt = clsDBFuncationality.GetDataTable(qry)

                    'For i As Int16 = 0 To dt.Rows.Count - 1
                    '    gv1.Rows.AddNew()
                    '    intLine += 1
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colStatus).Value = clsCommon.myCBool(dt.Rows(i)("Status").ToString)
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCdbl(intLine)
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colINVOICE_NO).Value = clsCommon.myCstr(dt.Rows(i)("Invoice No").ToString)
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colINVOICE_DATE).Value = clsCommon.GetPrintDate(dt.Rows(i)("Invoice Date").ToString, "dd/MMM/yyyy") 'clsCommon.myCDate(dt.Rows(i)("Invoice Date").ToString)
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colCRATE).Value = clsCommon.myCdbl(dt.Rows(i)("Crate Qty").ToString)
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colLINER).Value = clsCommon.myCdbl(dt.Rows(i)("Liner Qty").ToString)
                    'Next


                    gv1.DataSource = dt
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()

                    If dt.Rows.Count <= 0 Then
                        lblNoOfRecords.Text = "No Record Found"
                    Else
                        strNoOfRecord = clsCommon.myCstr(dt.Rows.Count)
                        lblNoOfRecords.Text = "" + strNoOfRecord + " Records Found"
                    End If



                    gv1Format()
                    ReStoreGridLayout()
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    Sub gv1Format()
        'Me.gv1.MasterTemplate.Columns("Status").Width = 50      ''First Column
        'Me.gv1.MasterTemplate.Columns("Document Id").Width = 150    ''Second Column
        Dim count As Integer = gv1.MasterTemplate.Columns.Count
        For i As Integer = 0 To count - 1
            Me.gv1.MasterTemplate.Columns(i).Width = 120
            Me.gv1.MasterTemplate.Columns(i).ReadOnly = True
        Next i
        'Me.gv1.MasterTemplate.Columns("Description").Width = 200    ''Last Column
        'For j As Integer = 1 To count - 3
        '    Me.gv1.MasterTemplate.Columns(j).ReadOnly = True
        'Next

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




    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm()
    End Sub


    Sub closeForm()
        Me.Close()
    End Sub




    Private Sub rptCrateLinerReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        txtCustomer.arrValueMember = Nothing
        TxtMultiLocation.arrValueMember = Nothing
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        strNoOfRecord = clsCommon.myCstr(0)
        BlankAllControls()
        lblNoOfRecords.Text = "" + strNoOfRecord + " Records Found"
    End Sub


    Sub BlankAllControls()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        txtCustomer.arrValueMember = Nothing
        TxtMultiLocation.arrValueMember = Nothing
        gv1.Rows.Clear()
        gv1.DataSource = Nothing
        'gv1.Rows.AddNew()
    End Sub


    Private Sub TxtMultiLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER "
        TxtMultiLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Pro", qry, "Code", "Name", TxtMultiLocation.arrValueMember, TxtMultiLocation.arrDispalyMember)
    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If (gv1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
                Exit Sub
            End If


             Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
            End If

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If (gv1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow("No Data To Export")
                Exit Sub
            End If


            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
            End If

          
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
          
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
End Class





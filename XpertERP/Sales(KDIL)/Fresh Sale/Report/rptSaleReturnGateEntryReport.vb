Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class RptSaleReturnGateEntryReport
    Inherits FrmMainTranScreen


    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim strQry As String = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub RptSaleReturnGateEntryReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MyBase.SetUserMgmt(clsUserMgtCode.rptSaleReturnGateEntryReport)
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        LoadDocType()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub LoadDocType()
        cboDocType.DataSource = DocumentType()
        cboDocType.ValueMember = "Code"
        cboDocType.DisplayMember = "Name"
        cboDocType.SelectedIndex = 0
    End Sub
    Public Function DocumentType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "All"
        dr("Name") = "All"
        dt.Rows.Add(dr)
        ' CSATRAN , EXPS, PS, MISS , FS , MCCS
        dr = dt.NewRow()
        dr("Code") = "EXP"
        dr("Name") = "Export Sale Return"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "PS"
        dr("Name") = "Product Sale Return"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "FS"
        dr("Name") = "Fresh Sale Return"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "MCCS"
        dr("Name") = "MCC Material Sale Return"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CSA"
        dr("Name") = "CSA Transfer Return"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "MISS"
        dr("Name") = "Material Sales Return"
        dt.Rows.Add(dr)
        Return dt
    End Function

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        EnabledisableControl(True)
        RadPageView1.SelectedPage = RadPageViewPage1
        'Gv1.DataSource = Nothing
        'Gv1.Rows.Clear()
        'Gv1.Columns.Clear()
        'Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        'txtCustomer.arrValueMember = Nothing
        'fromDate.Value = clsCommon.GETSERVERDATE()
        'ToDate.Value = clsCommon.GETSERVERDATE()
        'RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID + IIf(rbtnSummary.Checked = True, "S", "D")
            TemplateGridview = Gv1
            If fromdate.Value > todate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If
            Dim fromdates As String = clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")
            Dim todates As String = clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")
            Dim qry As String = Nothing
            Dim whrCustCode As String = ""
            Dim whrLocCode As String = ""

            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                whrCustCode += "  and TSPL_Sale_Return_Gate_Entry_head.Customer_Code in  (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") " + Environment.NewLine
            End If

            If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
                whrLocCode += "  and TSPL_Sale_Return_Gate_Entry_head.Location_Code in  (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") " + Environment.NewLine
            End If
            ' CSATRAN , EXPS, PS, MISS , FS , MCCS
            Dim strDocType As String = ""
            Dim strDocTypeMISS As String = ""
            If clsCommon.CompairString(cboDocType.SelectedValue, "All") = CompairStringResult.Equal Then
                strDocType = "  and GateEntryCTE.Doc_Type in ( 'PS','FS','MCCS','CSATRAN','EXPS')  "
            ElseIf clsCommon.CompairString(cboDocType.SelectedValue, "PS") = CompairStringResult.Equal Then
                strDocType = " and GateEntryCTE.Doc_Type in ( 'PS')  "
            ElseIf clsCommon.CompairString(cboDocType.SelectedValue, "FS") = CompairStringResult.Equal Then
                strDocType = " and GateEntryCTE.Doc_Type in ( 'FS')  "
            ElseIf clsCommon.CompairString(cboDocType.SelectedValue, "MCCS") = CompairStringResult.Equal Then
                strDocType = " and GateEntryCTE.Doc_Type in ( 'MCCS')  "
            ElseIf clsCommon.CompairString(cboDocType.SelectedValue, "CSA") = CompairStringResult.Equal Then
                strDocType = " and GateEntryCTE.Doc_Type in ( 'CSATRAN')  "
            ElseIf clsCommon.CompairString(cboDocType.SelectedValue, "EXP") = CompairStringResult.Equal Then
                strDocType = " and GateEntryCTE.Doc_Type in ( 'EXPS')  "
            End If
            Dim isAllPending As String = ""
            Dim isMIISPending As String = ""
            If rbtnPending.Checked = True Then
                isAllPending = " and len(isnull( TSPL_SD_SALE_RETURN_HEAD.Document_Code,'') ) < =0  "
                'TSPL_SCRAPSALE_HEAD_Return.Document_No
                isMIISPending = " and len(isnull( TSPL_SCRAPSALE_HEAD_Return.Document_No,'') ) < =0  "
            Else
                isAllPending = ""
                isMIISPending = ""
            End If
            ' Ticket No : KDI/03/05/18-000291 By Prabhakar Add all Header part column
            ' Ticket No : KDI/07/05/18-000299 By Prabhakar 
            ' Ticket No : KDI/11/05/18-000310 By Prabhakar Add Summary and Details 
            ' Ticket No : KDI/03/05/18-000291
            qry = " ;WITH GateEntryCTE AS " & _
                  " ( " & _
                  " Select TSPL_Sale_Return_Gate_Entry_head.Gate_Entry_No , convert (varchar,TSPL_Sale_Return_Gate_Entry_head.Gate_Entry_Date,103) as Gate_Entry_Date , TSPL_Sale_Return_Gate_Entry_head.Doc_Type,TSPL_Sale_Return_Gate_Entry_head.Customer_Code,'' as  Document_Code , '' as Document_Date, '' as Document_Status  , TSPL_Sale_Return_Gate_Entry_head.Vehicle_Code, TSPL_Sale_Return_Gate_Entry_head.Man_Vehicle_Code , TSPL_Sale_Return_Gate_Entry_head.Location_Code , TSPL_Sale_Return_Gate_Entry_head.Transport  , TSPL_Sale_Return_Gate_Entry_head.Man_Transport , TSPL_Sale_Return_Gate_Entry_head.Remarks, TSPL_Sale_Return_Gate_Entry_head.Comment ,case when  TSPL_Sale_Return_Gate_Entry_head.POSTED = 1 then 'Yes' else 'No' end  isPosted,  case when TSPL_Sale_Return_Gate_Entry_head.isCancel=1 then 'Yes' else 'No' end as isCancel , case when TSPL_Sale_Return_Gate_Entry_head.isCancel=1 then  convert(varchar,TSPL_Sale_Return_Gate_Entry_head.Cancel_Date,103)  else '' end Cancel_Date , TBL_Invocie_Details.Invoice_No  from  TSPL_Sale_Return_Gate_Entry_head " & _
                  " left outer join   ( SELECT     SS.Gate_Entry_No,    STUFF((SELECT ',' + US.Invoice_No            FROM TSPL_Sale_Return_Gate_Entry_Invoice_Wise US   WHERE US.Gate_Entry_No = SS.Gate_Entry_No   FOR XML PATH('')), 1, 1, '') [Invoice_No]  FROM TSPL_Sale_Return_Gate_Entry_Invoice_Wise SS  GROUP BY SS.Gate_Entry_No, SS.Gate_Entry_No ) TBL_Invocie_Details on  TBL_Invocie_Details.Gate_Entry_No = TSPL_Sale_Return_Gate_Entry_head.Gate_Entry_No " & _
                  "  where TSPL_Sale_Return_Gate_Entry_head.POSTED=1  and convert(date,TSPL_Sale_Return_Gate_Entry_head.Gate_Entry_Date,103) > = '" + fromdates + "' and convert(date,TSPL_Sale_Return_Gate_Entry_head.Gate_Entry_Date,103) <='" + todates + "'  " + whrCustCode + "  " + whrLocCode + " " & _
                  " )  " & _
                  " select Final.Gate_Entry_No as [Gate Entry No] , Final.Gate_Entry_Date as  [Gate Entry Date],  "
            If rbtnDetails.Checked = True Then
                qry = qry + " TSPL_Sale_Return_Gate_Entry_Detail.Item_Code as [Item Code],TSPL_ITEM_MASTER.Item_Desc as [Item Desc], TSPL_Sale_Return_Gate_Entry_Detail.UOM, TSPL_Sale_Return_Gate_Entry_Detail.Qty,   TSPL_Sale_Return_Gate_Entry_Detail.Remarks as [Item Remarks] , "
            End If
            qry = qry + "  case when Final.Doc_Type = 'PS' then 'Product Sale Return' when Final.Doc_Type = 'FS' then 'Fresh Sale Return' when Final.Doc_Type  = 'MCCS' then 'MCC Material Sale Return'  when Final.Doc_Type  ='CSATRAN' then 'CSA Transfer Return' when Final.Doc_Type  ='EXPS' then 'Export Sale Return' when Final.Doc_Type  =  'MISS' then 'Material Sales Return' else '' end as [Doc Type] , Final.Invoice_No as [Invoice No] , Final.Customer_Code as [Customer Code] , tspl_customer_master.Customer_Name as [Customer Name], Final.Document_Code as [Sale Return No], Final.Document_Date as [Sale Return Date],final.Against_Invoice_No as [Against Invoice No] ,Final.Vehicle_Code as [Vehicle Code],TSPL_VEHICLE_MASTER.Description as [Vehicle Desc], Final.Man_Vehicle_Code as [Manual Vehicle], Final.Location_Code as [Location Code] ,TSPL_Location_Master.Location_Desc as [Location Desc]  ,Final.Transport as [Transport Code],TSPL_TRANSPORT_MASTER.Transporter_Name as [Transporter Name], final.Man_Transport  as [Manual Transport] ,Final.Remarks, Final.Comment,Final.isPosted as [Posted],Final.isCancel as [Cancel],Final.Cancel_Date as [Cancel Date]  , case    when  len (Final.Document_Code)  >= 0  then 'Complete' when Final.isCancel ='Yes' then 'Cancel' else 'Pending' end as [Status]  from ( "
            If clsCommon.CompairString(cboDocType.SelectedValue, "MISS") <> CompairStringResult.Equal Then
                qry = qry + " select GateEntryCTE.Gate_Entry_No , GateEntryCTE.Gate_Entry_Date , GateEntryCTE.Doc_Type , GateEntryCTE.Invoice_No, GateEntryCTE.Customer_Code, TSPL_SD_SALE_RETURN_HEAD.Document_Code, convert(varchar,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) as Document_Date, TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No, TSPL_SD_SALE_RETURN_HEAD.Status , GateEntryCTE.Vehicle_Code , GateEntryCTE.Man_Vehicle_Code, GateEntryCTE.Location_Code,GateEntryCTE.Transport,GateEntryCTE.Man_Transport,GateEntryCTE.Remarks, GateEntryCTE.Comment,GateEntryCTE.isPosted,GateEntryCTE.isCancel,GateEntryCTE.Cancel_Date  from GateEntryCTE  left outer join TSPL_SD_SALE_RETURN_HEAD on GateEntryCTE.Gate_Entry_No= TSPL_SD_SALE_RETURN_HEAD.Gate_Entry_No  where 2 =2  " + strDocType + "  " + isAllPending + " "
            End If
            If clsCommon.CompairString(cboDocType.SelectedValue, "All") = CompairStringResult.Equal Then
                qry = qry + " Union "
            End If
            If clsCommon.CompairString(cboDocType.SelectedValue, "All") = CompairStringResult.Equal Or clsCommon.CompairString(cboDocType.SelectedValue, "MISS") = CompairStringResult.Equal Then
                qry = qry + "  select GateEntryCTE.Gate_Entry_No , GateEntryCTE.Gate_Entry_Date , GateEntryCTE.Doc_Type , GateEntryCTE.Invoice_No ,GateEntryCTE.Customer_Code, TSPL_SCRAPSALE_HEAD_Return.Document_No as Document_Code , convert(varchar,TSPL_SCRAPSALE_HEAD_Return.Created_Date,103) as Document_Date,TSPL_SCRAPSALE_HEAD_RETuRN.shipment_No as Against_Invoice_No ,TSPL_SCRAPSALE_HEAD_Return.Status  ,GateEntryCTE.Vehicle_Code, GateEntryCTE.Man_Vehicle_Code, GateEntryCTE.Location_Code,GateEntryCTE.Transport,GateEntryCTE.Man_Transport,GateEntryCTE.Remarks, GateEntryCTE.Comment,GateEntryCTE.isPosted,GateEntryCTE.isCancel,GateEntryCTE.Cancel_Date from GateEntryCTE  left outer join TSPL_SCRAPSALE_HEAD_Return on GateEntryCTE.Gate_Entry_No= TSPL_SCRAPSALE_HEAD_Return.Gate_Entry_No  where GateEntryCTE.Doc_Type = 'MISS' " + isMIISPending + " "
            End If
           
            qry = qry + " )   " & _
                        " Final left outer join tspl_customer_master on tspl_customer_master.Cust_Code= Final.Customer_Code  left outer join TSPL_Location_Master  on TSPL_Location_Master.Location_Code = Final.Location_Code left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id = Final.Transport  left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id = Final.Vehicle_Code  left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code = Final.Vehicle_Code     "
            If rbtnDetails.Checked = True Then
                qry = qry + "  left outer join TSPL_Sale_Return_Gate_Entry_Detail on TSPL_Sale_Return_Gate_Entry_Detail.Gate_Entry_No = Final.Gate_Entry_No "
                qry = qry + "  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_Sale_Return_Gate_Entry_Detail.Item_Code "
            End If
            qry = qry + " where 2= 2   order by convert (datetime, Final.Gate_Entry_Date,103) desc "

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing

            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv
            'SetGridFormationOFgv()
            Gv1.BestFitColumns()
            If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            RadPageView1.SelectedPage = RadPageViewPage2
            EnabledisableControl(False)
            ReStoreGridLayout()

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub EnabledisableControl(ByVal isEnable As Boolean)
        fromDate.Enabled = isEnable
        ToDate.Enabled = isEnable
        rbtnAll.Enabled = isEnable
        rbtnPending.Enabled = isEnable
        cboDocType.Enabled = isEnable
        txtCustomer.Enabled = isEnable
        btnGo.Enabled = isEnable
        fndLocation.Enabled = isEnable
        rbtnSummary.Enabled = isEnable
        rbtnDetails.Enabled = isEnable
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmExportToExcel_Click(sender As Object, e As EventArgs) Handles rmExportToExcel.Click
        Try

            If Gv1.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("From Date: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptSaleReturnGateEntryReport & "'"))

                If clsCommon.myLen(cboDocType.Text) > 0 Then
                    arrHeader.Add("Doc Type : " + cboDocType.Text)
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
                End If
                If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(fndLocation.arrDispalyMember))
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
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    'Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
    '    If Gv1.Rows.Count > 0 Then
    '        If e.Column.Name = "Gate Entry No" Then
    '            Dim strGateEntryNo As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Gate Entry No").Value)
    '            If clsCommon.myLen(strGateEntryNo) > 0 Then
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesInvoiceMT, strDocNo)
    '            End If

    '        End If
    '    End If
    'End Sub
    'Ticket No : KDI/03/05/18-000291 By Prabhakar Add Filter 
    Private Sub fndLocation__My_Click(sender As Object, e As EventArgs) Handles fndLocation._My_Click
        Dim strQry As String = " select TSPL_Location_Master.Location_Code as [code],TSPL_Location_Master.Location_Desc as [Name] from TSPL_Location_Master"
        fndLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransLoceMulSel", strQry, "Code", "Name", fndLocation.arrValueMember, fndLocation.arrDispalyMember)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        Try
            If Gv1.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("From Date: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptSaleReturnGateEntryReport & "'"))

                If clsCommon.myLen(cboDocType.Text) > 0 Then
                    arrHeader.Add("Doc Type : " + cboDocType.Text)
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
                End If
                If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(fndLocation.arrDispalyMember))
                End If

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Sale Return Gate Entry Report", Gv1, arrHeader, "Sale Return Gate Entry Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

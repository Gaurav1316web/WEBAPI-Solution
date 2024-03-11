Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class RptBulkMilkMultiplePurchaseInvoice
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False

    Public Shared ArrInvoice_Arr As New ArrayList()
    Dim arrLoc As String = Nothing
    Dim RunBulkProcOnAdjustFATCLR As Integer = 0

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptBulkMilkMultiplePurchaseInvoice)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        BtnPrint.Visible = MyBase.isPrintFlag
        btnPrindDocWise.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub chkSupplierAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkSupplierAll.ToggleStateChanged
        cbgSupplier.Enabled = Not chkSupplierAll.IsChecked
    End Sub

    Private Sub RptBulkMilkMultiplePurchaseInvoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ''====Adjusted Fat & SNF======
        RunBulkProcOnAdjustFATCLR = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBulkProcOnAdjustedFATCLR, clsFixedParameterCode.RunBulkProcOnAdjustedFATCLR, Nothing))
        ''=========================
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
    End Sub
    Sub LoadLocation()
        Dim qry As String = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER .Location_Desc as [Name] from TSPL_LOCATION_MASTER where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N'  and Location_Code in (" + arrLoc + ") "

        Else
            btnGo.Enabled = False
        End If
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"

    End Sub
    Sub LoadSupplier()
        Dim Qry As String = "Select Vendor_Code as [Code],Vendor_Name as [Name] from TSPL_VENDOR_MASTER"
        cbgSupplier.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgSupplier.ValueMember = "Code"
        cbgSupplier.DisplayMember = "Name"
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadLocation()
        chkLocationAll.CheckState = CheckState.Checked
        If chkLocationAll.IsChecked Then
            cbgLocation.CheckedAll()
        Else
            cbgLocation.UnCheckedAll()
        End If
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.DataSource = Nothing
        gv.Rows.Clear()
        LoadSupplier()
        chkSupplierAll.CheckState = CheckState.Checked
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        loadReport()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        loadData()
    End Sub

    Private Sub btnUnSelect_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(0).Value = False

            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(0).Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub
    Public Sub loadReport()
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single Location or select all.", Me.Text)
            Exit Sub
        End If
        If ChkSupplierSelect.IsChecked AndAlso cbgSupplier.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single Supplier or select all.", Me.Text)
            Exit Sub
        End If
        Dim sQuery As String = " select  Cast(1 as BIT) as 'Check',DOC_NO,convert(varchar,DOC_DATE,103)as DOC_DATE,tspl_Bulk_milk_purchase_Invoice_head.Loc_Code,Location_desc,tspl_Bulk_milk_purchase_Invoice_head.VENDOR_CODE as Ven_code,Vendor_Name,VENDOR_INVOICE_NO,convert(decimal(18,2),Total_Qty)as Total_Qty,convert(decimal(18,2),Total_FAT_KG)as Total_FAT_KG,convert(decimal(18,2),Total_SNF_KG)as Total_SNF_KG,convert(decimal(18,2),Total_AMT)as Total_AMT from tspl_Bulk_milk_purchase_Invoice_head left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_Bulk_milk_purchase_Invoice_head.Loc_Code left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=tspl_Bulk_milk_purchase_Invoice_head.VENDOR_CODE  where "

        sQuery += "  convert(date,tspl_bulk_milk_purchase_invoice_head.Doc_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,tspl_bulk_milk_purchase_invoice_head.Doc_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        If cbgLocation.CheckedValue.Count > 0 Then 'chkLocationSelect.IsChecked And
            sQuery += " and TSPL_LOCATION_MASTER. Location_Code   IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
        End If
        If ChkSupplierSelect.IsChecked And cbgSupplier.CheckedValue.Count > 0 Then
            sQuery += " and TSPL_VENDOR_MASTER. Vendor_Code   IN (" + clsCommon.GetMulcallString(cbgSupplier.CheckedValue) + ") "
        End If
        sQuery += " order by convert(date,DOC_DATE,103) "
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.text)
        End If
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("Check").IsVisible = True
        gv.Columns("Check").Width = 100
        gv.Columns("Check").HeaderText = " "
        gv.Columns("Check").ReadOnly = False

        gv.Columns("DOC_NO").IsVisible = True
        gv.Columns("DOC_NO").Width = 100
        gv.Columns("DOC_NO").HeaderText = "Document No. "
        gv.Columns("DOC_NO").ReadOnly = True

        gv.Columns("DOC_DATE").IsVisible = True
        gv.Columns("DOC_DATE").Width = 100
        gv.Columns("DOC_DATE").HeaderText = " Date"
        gv.Columns("DOC_DATE").FormatString = "{0:d}"

        gv.Columns("Vendor_Invoice_No").IsVisible = True
        gv.Columns("Vendor_Invoice_No").Width = 100
        gv.Columns("Vendor_Invoice_No").HeaderText = "Vendor Invoice No"

        gv.Columns("Ven_code").IsVisible = True
        gv.Columns("Ven_code").Width = 100
        gv.Columns("Ven_code").HeaderText = "Vendor Code"


        gv.Columns("Vendor_Name").IsVisible = True
        gv.Columns("Vendor_Name").Width = 100
        gv.Columns("Vendor_Name").HeaderText = "Vendor Name"

        gv.Columns("Loc_Code").IsVisible = True
        gv.Columns("Loc_Code").Width = 100
        gv.Columns("Loc_Code").HeaderText = "Location Code"

        gv.Columns("Location_Desc").IsVisible = True
        gv.Columns("Location_Desc").Width = 150
        gv.Columns("Location_Desc").HeaderText = "Location Name"

        gv.Columns("Total_Qty").IsVisible = True
        gv.Columns("Total_Qty").Width = 100
        gv.Columns("Total_Qty").HeaderText = " Qty"

        gv.Columns("Total_FAT_KG").IsVisible = True
        gv.Columns("Total_FAT_KG").Width = 100
        gv.Columns("Total_FAT_KG").HeaderText = "FAT KG"

     
        gv.Columns("Total_SNF_KG").IsVisible = True
        gv.Columns("Total_SNF_KG").Width = 100
        gv.Columns("Total_SNF_KG").HeaderText = "SNF KG"

        gv.Columns("Total_AMT").IsVisible = True
        gv.Columns("Total_AMT").Width = 100
        gv.Columns("Total_AMT").HeaderText = "Amount"

      
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Total_AMT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Total_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)



        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    '============update by preeti gupta Against Ticket no[BHA/03/07/18-000128]
    Sub loadData()


        ArrInvoice_Arr = New ArrayList


        Dim InvoiceNo As String = ""

        For Each grow As GridViewRowInfo In gv.Rows
            If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                InvoiceNo = InvoiceNo + "','" + clsCommon.myCstr(grow.Cells("DOC_NO").Value)
            End If
        Next

        If clsCommon.myLen(InvoiceNo) > 0 AndAlso clsCommon.myCstr(InvoiceNo).Substring(0, 3) = "','" Then
            InvoiceNo = InvoiceNo.Substring(3, InvoiceNo.Length - 3)

        End If
        Dim frmDate As String = txtFromDate.Text
        Dim ToDate As String = txtToDate.Text



        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single Location or select all.", Me.Text)
            Exit Sub
        End If
        If ChkSupplierSelect.IsChecked AndAlso cbgSupplier.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single Supplier or select all.", Me.Text)
            Exit Sub
        End If
        Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, Nothing))
        Dim qry As String = ""
        'Ticket No-ERO/21/11/19-001126,Erode,Add UOM and other column
        'Dim Qry As String = " select Tspl_Gate_Entry_Details.Gate_Entry_No,convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) as Date_And_Time,'" & frmDate & "' as frmDate,'" & ToDate & "' as ToDate, TSPL_LOCATION_MASTER.add1 as Loc_Add1,tspl_Bulk_milk_purchase_Invoice_head.Loc_Code ,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_VENDOR_MASTER.Vendor_Code as Ven_code,tspl_Bulk_milk_purchase_Invoice_head.Total_AMT ,tspl_Bulk_milk_purchase_Invoice_head.Total_QTY,tspl_Bulk_milk_purchase_Invoice_head.DOC_NO ,convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) as DOC_DATE ,tspl_Bulk_milk_purchase_Invoice_head.Vendor_Invoice_No,tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code ,TSPL_ITEM_MASTER.Item_Desc  ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  ,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add4,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add4,'') else ' ' end  as Loc_Add,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,TSPL_LOCATION_MASTER.Pin_Code  as Loc_PINCode,TSPL_LOCATION_MASTER.Email as Loc_Email,case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end   as Ven_Add,TSPL_VENDOR_MASTER.Tin_No as Ven_TINNo ,TSPL_VENDOR_MASTER.Email as Ven_Email ,case when ISNULL(TSPL_VENDOR_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_VENDOR_MASTER.Phone1 end +  Case When   ISNULL(TSPL_VENDOR_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_VENDOR_MASTER.Phone2 Else'' End as Ven_Phn,TSPL_Bulk_MILK_SRN.SRN_NO ,convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) as SRN_Date ,TSPL_Bulk_MILK_SRN.Tanker_No ,t_FAT.Param_Field_Value As FAT,t_SNF .Param_Field_Value As SNF,TSPL_Weighment_Detail.Net_Weight  as Milk_qty,'For  FAT' +convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) + ' % &  SNF' +  convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage)+ ' %' as 'MilkRate' , 'For ' +convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage ) + ' & ' +  convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage ) as 'Weightage',tspl_Bulk_milk_purchase_Invoice_Detail.NetRate,tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount,tspl_Bulk_milk_purchase_Invoice_head.RoundOffAmount ,(TSPL_Bulk_MILK_SRN.Incentive+TSPL_Bulk_MILK_SRN.Deduction-TSPL_Bulk_MILK_SRN.SpecialDeduction)as Ded_Inc,TSPL_Bulk_MILK_SRN.BasicRate,TSPL_Bulk_MILK_SRN.Fat_KG,TSPL_Bulk_MILK_SRN.SNF_KG from tspl_Bulk_milk_purchase_Invoice_Detail left outer join tspl_Bulk_milk_purchase_Invoice_head on tspl_Bulk_milk_purchase_Invoice_head.DOC_NO =tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code  left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code =tspl_Bulk_milk_purchase_Invoice_head.Comp_Code  left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =tspl_Bulk_milk_purchase_Invoice_head.loc_code  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =tspl_Bulk_milk_purchase_Invoice_head.vendor_code  left outer join TSPL_Bulk_MILK_SRN  on TSPL_Bulk_MILK_SRN.SRN_NO =tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO  left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No =TSPL_Bulk_MILK_SRN.Gate_Entry_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_Bulk_MILK_SRN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  =        TSPL_Bulk_MILK_SRN.QC_No  And        TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT      On t_FAT.QC_No   = TSPL_Bulk_MILK_SRN.QC_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_Bulk_MILK_SRN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  =        TSPL_Bulk_MILK_SRN.QC_No  And        TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF      On t_SNF .QC_No   = TSPL_Bulk_MILK_SRN.QC_No  left outer join TSPL_Bulk_Price_MASTER on TSPL_Bulk_Price_MASTER.Price_Code=tspl_Bulk_milk_purchase_Invoice_Detail.Price_code  left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Weighment_No =TSPL_Bulk_MILK_SRN.Weighment_No where tspl_bulk_milk_purchase_invoice_head.DOC_NO in ('" + InvoiceNo + "') order by Vendor_name,Date_And_Time"
        If TankerFromMaster = 0 Then
            qry = " ;with BulkMilkSRN as (select * from tspl_bulk_milk_srn where isnull(TSPL_Bulk_MILK_SRN.srn_return_no,'')='' "
            qry += " )select Tspl_Gate_Entry_Details.Gate_Entry_No,convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) as Date_And_Time,'" & frmDate & "' as frmDate,'" & ToDate & "' as ToDate, TSPL_LOCATION_MASTER.add1 as Loc_Add1,tspl_Bulk_milk_purchase_Invoice_head.Loc_Code ,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_VENDOR_MASTER.Vendor_Code as Ven_code,tspl_Bulk_milk_purchase_Invoice_head.Total_AMT ,tspl_Bulk_milk_purchase_Invoice_head.Total_QTY,tspl_Bulk_milk_purchase_Invoice_head.DOC_NO ,convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) as DOC_DATE ,tspl_Bulk_milk_purchase_Invoice_head.Vendor_Invoice_No,tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code ,TSPL_ITEM_MASTER.Item_Desc  ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  ,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add4,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add4,'') else ' ' end  as Loc_Add,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,TSPL_LOCATION_MASTER.Pin_Code  as Loc_PINCode,TSPL_LOCATION_MASTER.Email as Loc_Email,case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end   as Ven_Add,TSPL_VENDOR_MASTER.Tin_No as Ven_TINNo ,TSPL_VENDOR_MASTER.Email as Ven_Email ,case when ISNULL(TSPL_VENDOR_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_VENDOR_MASTER.Phone1 end +  Case When   ISNULL(TSPL_VENDOR_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_VENDOR_MASTER.Phone2 Else'' End as Ven_Phn,BulkMilkSrn.SRN_NO ,convert(varchar,BulkMilkSrn.SRN_Date,103) as SRN_Date ,BulkMilkSrn.Tanker_No ,t_FAT.Param_Field_Value As FAT,t_SNF .Param_Field_Value As SNF,TSPL_Weighment_Detail.Net_Weight  as Milk_qty,'For  FAT' +convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) + ' % &  SNF' +  convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage)+ ' %' as 'MilkRate' , 'For ' +convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage ) + ' & ' +  convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage ) as 'Weightage',tspl_Bulk_milk_purchase_Invoice_Detail.NetRate,tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount,tspl_Bulk_milk_purchase_Invoice_head.RoundOffAmount ,(BulkMilkSrn.Incentive+BulkMilkSrn.Deduction-BulkMilkSrn.SpecialDeduction)as Ded_Inc,BulkMilkSrn.BasicRate,BulkMilkSrn.Fat_KG,BulkMilkSrn.SNF_KG,TSPL_VENDOR_MASTER.Bank_Code,TSPL_VENDOR_BANK_MASTER.Bank_Name,(TSPL_VENDOR_BANK_MASTER.Add1+TSPL_VENDOR_BANK_MASTER.Add2+TSPL_VENDOR_BANK_MASTER.Add3)as Bank_Address,Account_No,convert(decimal(18,2),tspl_Bulk_milk_purchase_Invoice_head.Total_FAT_KG)as Total_FAT_KG,convert(decimal(18,2),tspl_Bulk_milk_purchase_Invoice_head.Total_SNF_KG)as Total_SNF_KG,tspl_Bulk_milk_purchase_Invoice_Detail.UOM,TSPL_COMPANY_MASTER.Pan_No,TSPL_COMPANY_MASTER.CINNo,TSPL_COMPANY_MASTER.GSTReg_No from tspl_Bulk_milk_purchase_Invoice_Detail "
            qry += " left outer join tspl_Bulk_milk_purchase_Invoice_head on tspl_Bulk_milk_purchase_Invoice_head.DOC_NO =tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code "
            qry += "  left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code =tspl_Bulk_milk_purchase_Invoice_head.Comp_Code  "
            qry += " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =tspl_Bulk_milk_purchase_Invoice_head.loc_code "
            qry += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =tspl_Bulk_milk_purchase_Invoice_head.vendor_code "
            qry += " left join TSPL_VENDOR_BANK_MASTER on TSPL_VENDOR_MASTER.BANK_CODE=TSPL_VENDOR_BANK_MASTER.BANK_CODE"
            qry += " left outer join BulkMilkSrn  on BulkMilkSrn.SRN_NO =tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO  "
            qry += " left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No =BulkMilkSrn.Gate_Entry_No "
            qry += " Left Outer Join (Select TSPL_QC_Parameter_Detail.* From BulkMilkSrn Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  =        BulkMilkSrn.QC_No  And        TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT      On t_FAT.QC_No   = BulkMilkSrn.QC_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From BulkMilkSrn Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  =        BulkMilkSrn.QC_No  And        TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF      On t_SNF .QC_No   = BulkMilkSrn.QC_No  left outer join TSPL_Bulk_Price_MASTER on TSPL_Bulk_Price_MASTER.Price_Code=tspl_Bulk_milk_purchase_Invoice_Detail.Price_code  left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Weighment_No =BulkMilkSrn.Weighment_No where tspl_bulk_milk_purchase_invoice_head.DOC_NO in ('" + InvoiceNo + "') order by Vendor_name,Date_And_Time"
        Else
            qry = GetQuery(frmDate, ToDate, InvoiceNo)
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHBA") = CompairStringResult.Equal Then
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptBulkMilkPurchaseInvoiceForBHBA", "Purchase Invoice")
            Else
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptBulkMilkPurchaseMultipleInvoice", "Bulk Invoice Statement")
            End If
            frmCRV = Nothing
        End If



    End Sub
    '=========update by preeti Gupta Against Ticket No[BHA/03/07/18-000127]
    Function GetQuery(ByVal frmDate As String, ByVal ToDate As String, ByVal InvoiceNo As String) As String
        Dim qry = " ;with BulkMilkSRN as (select * from tspl_bulk_milk_srn where isnull(TSPL_Bulk_MILK_SRN.srn_return_no,'')=''  ) " &
               "select " &
               " Case when dtax1.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX1_Rate when  dtax2.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX2_Rate when dtax3.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX3_Rate " &
                " when dtax4.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX4_Rate  when dtax5.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX5_Rate when dtax6.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX6_Rate  when dtax7.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX7_Rate when dtax8.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX8_Rate when dtax9.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX9_Rate when dtax10.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX10_Rate end as TCS_Rate " &
                " ,Case when dtax1.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX1_Amt when  dtax2.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX2_Amt when dtax3.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX3_Amt when dtax4.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX4_Amt  when dtax5.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX5_Amt when dtax6.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX6_Amt  when dtax7.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX7_Amt when dtax8.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX8_Amt when dtax9.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX9_Amt when dtax10.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX10_Amt end  as TCS_Amount" &
               " ,isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.Purchase_Tax_Invoice,'') as Purchase_Tax_Invoice,isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.Purchase_Tax_Invoice_Type,'') as Purchase_Tax_Invoice_Type ,  TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode,   Tspl_Gate_Entry_Details.Gate_Entry_No,convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) as Date_And_Time, " &
               "'" & frmDate & "' as frmDate,'" & ToDate & "' as ToDate, CONVERT(VARCHAR(15),tspl_Bulk_milk_purchase_Invoice_head.SRN_From_Date,103) AS From_Date,convert(varchar(15),tspl_Bulk_milk_purchase_Invoice_head.SRN_TO_Date,103) as To_Date, TSPL_LOCATION_MASTER.add1 as Loc_Add1,tspl_Bulk_milk_purchase_Invoice_head.Loc_Code , " &
               "TSPL_LOCATION_MASTER.Location_Desc ,TSPL_VENDOR_MASTER.Vendor_Code as Ven_code,tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount as Total_AMT ,tspl_Bulk_milk_purchase_Invoice_head.Total_AMT as Sum_of_ActualAmt, " &
               "tspl_Bulk_milk_purchase_Invoice_Detail.Invoice_Qty as Total_QTY,tspl_Bulk_milk_purchase_Invoice_head.DOC_NO , " &
               "convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) as DOC_DATE ,tspl_Bulk_milk_purchase_Invoice_head.Vendor_Invoice_No, " &
               "tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code ,TSPL_ITEM_MASTER.Item_Desc  ,TSPL_COMPANY_MASTER.Comp_Name , " &
               "TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  ,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add4,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add4,'') else ' ' end  as Loc_Add, " &
               "TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,TSPL_LOCATION_MASTER.Pin_Code  as Loc_PINCode,TSPL_LOCATION_MASTER.Email as Loc_Email,case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn, " &
               "TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end   as Ven_Add, " &
               "TSPL_VENDOR_MASTER.Tin_No as Ven_TINNo ,TSPL_VENDOR_MASTER.Email as Ven_Email , " &
               "case when ISNULL(TSPL_VENDOR_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_VENDOR_MASTER.Phone1 end +  Case When   ISNULL(TSPL_VENDOR_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_VENDOR_MASTER.Phone2 Else'' End as Ven_Phn, " &
               "BulkMilkSrn.SRN_NO ,convert(varchar,BulkMilkSrn.SRN_Date,103) as SRN_Date ,BulkMilkSrn.Tanker_No , "
        ' Ticket No :  BHA/12/07/18-000152 By Prabhakar Desc : Show data of invoice as per Bulk  Milk Purchase Invoice. Currently it is showing as per Bulk SRN 
        'If RunBulkProcOnAdjustFATCLR = 0 Then
        '    qry += " t_FAT.Param_Field_Value As FAT,t_SNF .Param_Field_Value As SNF,"
        'Else
        qry += "  tspl_Bulk_milk_purchase_Invoice_Detail.fat_per As FAT ,tspl_Bulk_milk_purchase_Invoice_Detail.snf_Per AS SNF,"
        'End If

        qry += " tspl_Bulk_milk_purchase_Invoice_detail.Net_Weight  as Milk_qty, " &
               "'For  FAT' +convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Percentage) + ' % &  SNF' +  convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Percentage)+ ' %' as 'MilkRate' , " &
               "'For ' +convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Weightage ) + ' & ' +  convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Weightage ) as 'Weightage', " &
               "tspl_Bulk_milk_purchase_Invoice_Detail.NetRate,tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount,tspl_Bulk_milk_purchase_Invoice_head.RoundOffAmount , " &
               "(TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Incentive+TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Deduction-TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SpecialDeduction)as Ded_Inc, " &
               "tspl_Bulk_milk_purchase_Invoice_detail.NetRate as BasicRate,tspl_Bulk_milk_purchase_Invoice_detail.Fat_KG, tspl_Bulk_milk_purchase_Invoice_detail.SNF_KG, " &
               "TSPL_VENDOR_MASTER.Bank_Code,TSPL_VENDOR_BANK_MASTER.Bank_Name,(TSPL_VENDOR_BANK_MASTER.Add1+TSPL_VENDOR_BANK_MASTER.Add2+TSPL_VENDOR_BANK_MASTER.Add3)as Bank_Address, " &
               "Account_No,TSPL_WEIGHMENT_CHEMBER_DETAILS.item_code  "
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHBA") = CompairStringResult.Equal Then
            qry += "  ,convert(decimal(18,2),t_CLR.Param_Field_Value) as CLR,
                        convert(decimal(18,2),t_BWRM.Param_Field_Value) as BWRM,
                        convert(decimal(18,2),t_AWRM.Param_Field_Value) as AWRM,
                        convert(decimal(18,2),t_DIFFRM.Param_Field_Value) as DIFFRM,
                        convert(decimal(18,2),t_PPM.Param_Field_Value) as PPM,
                        convert(decimal(18,3),t_PROTEIN.Param_Field_Value) as PROTEIN,
                        convert(decimal(18,2),t_BR.Param_Field_Value) as BR,
                        convert(decimal(18,3),t_FFA.Param_Field_Value) as FFA,
                        convert(decimal(18,2),t_CHANNA.Param_Field_Value) as CHANNA 
                        ,TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.NetRate as STD_Rate,
                        TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.NetRate+tspl_Bulk_milk_purchase_Invoice_Detail.Xtra_rate as Effective_Rate"
        End If
        qry +=   " from tspl_Bulk_milk_purchase_Invoice_Detail " &
               "left outer join tspl_Bulk_milk_purchase_Invoice_head on tspl_Bulk_milk_purchase_Invoice_head.DOC_NO =tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO " &
               "left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code  " &
                " left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =tspl_Bulk_milk_purchase_Invoice_Detail .tax1 " &
                " left outer join tspl_tax_master as dtax2 on dtax2.tax_code = tspl_Bulk_milk_purchase_Invoice_Detail.tax2 " &
                " left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=tspl_Bulk_milk_purchase_Invoice_Detail .TAX3 " &
                " left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= tspl_Bulk_milk_purchase_Invoice_Detail .tax4 " &
                " left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=tspl_Bulk_milk_purchase_Invoice_Detail .tax5 " &
                " left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =tspl_Bulk_milk_purchase_Invoice_Detail .TAX6 " &
                " left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =tspl_Bulk_milk_purchase_Invoice_Detail .TAX7 " &
                " left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =tspl_Bulk_milk_purchase_Invoice_Detail .TAX8 " &
                " left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =tspl_Bulk_milk_purchase_Invoice_Detail .TAX9 " &
                " left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =tspl_Bulk_milk_purchase_Invoice_Detail .TAX10 " &
               "left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code =tspl_Bulk_milk_purchase_Invoice_head.Comp_Code  " &
               "left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =tspl_Bulk_milk_purchase_Invoice_head.loc_code " &
               "left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =tspl_Bulk_milk_purchase_Invoice_head.vendor_code " &
               " left join TSPL_VENDOR_BANK_MASTER on TSPL_VENDOR_MASTER.BANK_CODE=TSPL_VENDOR_BANK_MASTER.BANK_CODE " &
               "left outer join BulkMilkSrn  on BulkMilkSrn.SRN_NO =tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO " &
               "left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS  on BulkMilkSrn.SRN_NO =TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO and " &
               "tspl_Bulk_milk_purchase_Invoice_Detail.CHAMBER_DESC=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Chamber_Desc " &
               "left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No =BulkMilkSrn.Gate_Entry_No  " &
               "Left Outer Join (Select TSPL_QC_Parameter_Detail.* From BulkMilkSrn Left Outer Join TSPL_QC_Parameter_Detail On " &
               "TSPL_QC_Parameter_Detail.QC_No  =  BulkMilkSrn.QC_No  And  TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT " &
               "On t_FAT.QC_No   = BulkMilkSrn.QC_No and t_FAT.LINE_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No " &
               "Left Outer Join (Select TSPL_QC_Parameter_Detail.* From BulkMilkSrn Left Outer Join TSPL_QC_Parameter_Detail On " &
               "TSPL_QC_Parameter_Detail.QC_No  = BulkMilkSrn.QC_No  And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On " &
               "t_SNF .QC_No   = BulkMilkSrn.QC_No   and t_SNF.LINE_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No " &
               "left outer join TSPL_Bulk_Price_MASTER on TSPL_Bulk_Price_MASTER.Price_Code=tspl_Bulk_milk_purchase_Invoice_Detail.Price_code  " &
               "left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_code  and " &
               "TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.MILK_GRADE_CODE " &
               "left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Weighment_No =BulkMilkSrn.Weighment_No " &
               "left outer join  TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_Weighment_Detail.Weighment_No=TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No and " &
               "TSPL_WEIGHMENT_CHEMBER_DETAILS.Line_No=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No " &
               " left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code = TSPL_STATE_MASTER.STATE_CODE " &
               " left outer join tspl_state_master as tspl_state_master_for_location_state on  " &
               " tspl_state_master_for_location_state.state_code=tspl_location_master.state  "

        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHBA") = CompairStringResult.Equal Then
            qry += " left outer join TSPL_QC_Parameter_Detail t_CLR on t_CLR.QC_No=BulkMilkSrn.QC_No  and t_CLR .Param_Type='CLR' and TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No=t_CLR.line_no 
                    left outer join TSPL_QC_Parameter_Detail t_BWRM on t_BWRM.QC_No=BulkMilkSrn.QC_No  and t_BWRM .Param_Type='BWRM' and TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No=t_BWRM.line_no 
                    left outer join TSPL_QC_Parameter_Detail t_AWRM on t_AWRM.QC_No=BulkMilkSrn.QC_No  and t_AWRM .Param_Type='AWRM' and TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No=t_AWRM.line_no 
                    left outer join TSPL_QC_Parameter_Detail t_DIFFRM on t_DIFFRM.QC_No=BulkMilkSrn.QC_No  and t_DIFFRM .Param_Type='DIFFRM' and TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No=t_DIFFRM.line_no 
                    left outer join TSPL_QC_Parameter_Detail t_PPM on t_PPM.QC_No=BulkMilkSrn.QC_No  and t_PPM .Param_field_code = 'PPM' and TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No=t_PPM.line_no 
                    left outer join TSPL_QC_Parameter_Detail t_PROTEIN on t_PROTEIN.QC_No=BulkMilkSrn.QC_No  and t_PROTEIN .Param_field_code = 'PROTEIN' and TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No=t_PROTEIN.line_no 
                    left outer join TSPL_QC_Parameter_Detail t_BR on t_BR.QC_No=BulkMilkSrn.QC_No  and t_BR .Param_field_code = 'BR' and TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No=t_BR.line_no 
                    left outer join TSPL_QC_Parameter_Detail t_FFA on t_FFA.QC_No=BulkMilkSrn.QC_No  and t_FFA .Param_field_code = 'FFA' and TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No=t_FFA.line_no 
                    left outer join TSPL_QC_Parameter_Detail t_CHANNA on t_CHANNA.QC_No=BulkMilkSrn.QC_No  and t_CHANNA .Param_Type = 'CHANNA' and TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No=t_CHANNA.line_no "
        End If
        qry += " where tspl_bulk_milk_purchase_invoice_head.DOC_NO in ('" + InvoiceNo + "') order by Vendor_name,Date_And_Time"
        Return qry
    End Function
    Private Sub chkLocationAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
        If chkLocationAll.IsChecked Then
            cbgLocation.CheckedAll()
        Else
            cbgLocation.UnCheckedAll()
        End If
    End Sub

    Private Sub RptBulkMilkMultiplePurchaseInvoice_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            loadReport()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            loadData()
        End If
    End Sub

    Sub printDetails(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If chkLocationSelect.IsChecked Then
                Dim strMCCName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strMCCName) > 0 Then
                        strMCCName += ", "
                    End If
                    strMCCName += StrName
                Next
                Dim strMCCCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strMCCCode) > 0 Then
                        strMCCCode += ", "
                    End If
                    strMCCCode += StrCode
                Next
                arrHeader.Add(("Location: " + strMCCName + " "))
            End If

            If ChkSupplierSelect.IsChecked Then
                Dim strMCCName As String = ""
                For Each StrName As String In cbgSupplier.CheckedDisplayMember
                    If clsCommon.myLen(strMCCName) > 0 Then
                        strMCCName += ", "
                    End If
                    strMCCName += StrName
                Next
                Dim strMCCCode As String = ""
                For Each StrCode As String In cbgSupplier.CheckedValue
                    If clsCommon.myLen(strMCCCode) > 0 Then
                        strMCCCode += ", "
                    End If
                    strMCCCode += StrCode
                Next
                arrHeader.Add(("Supplier: " + strMCCName + " "))
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid(Me.Text, gv, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptBulkMilkMultiplePurchaseInvoice & "'"))

            If chkLocationSelect.IsChecked Then
                Dim strMCCName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strMCCName) > 0 Then
                        strMCCName += ", "
                    End If
                    strMCCName += StrName
                Next
                Dim strMCCCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strMCCCode) > 0 Then
                        strMCCCode += ", "
                    End If
                    strMCCCode += StrCode
                Next
                arrHeader.Add((" Location: " + strMCCName + " "))
            End If

            If ChkSupplierSelect.IsChecked Then
                Dim strMCCName As String = ""
                For Each StrName As String In cbgSupplier.CheckedDisplayMember
                    If clsCommon.myLen(strMCCName) > 0 Then
                        strMCCName += ", "
                    End If
                    strMCCName += StrName
                Next
                Dim strMCCCode As String = ""
                For Each StrCode As String In cbgSupplier.CheckedValue
                    If clsCommon.myLen(strMCCCode) > 0 Then
                        strMCCCode += ", "
                    End If
                    strMCCCode += StrCode
                Next
                arrHeader.Add((" Supplier: " + strMCCName + " "))
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
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        ' printDetails(EnumExportTo.Excel)
    End Sub
    '====Sanjeet(05/01/2017)===================
    Sub Laad_CheckDoc()

        ArrInvoice_Arr = New ArrayList


        Dim Multi_InvoiceNo As String = ""

        For Each grow As GridViewRowInfo In gv.Rows
            If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                Multi_InvoiceNo = Multi_InvoiceNo + "','" + clsCommon.myCstr(grow.Cells("DOC_NO").Value)
            End If
        Next

        If clsCommon.myLen(Multi_InvoiceNo) > 0 AndAlso clsCommon.myCstr(Multi_InvoiceNo).Substring(0, 3) = "','" Then
            Multi_InvoiceNo = Multi_InvoiceNo.Substring(3, Multi_InvoiceNo.Length - 3)

        End If
        Dim frmDate As String = txtFromDate.Text
        Dim ToDate As String = txtToDate.Text

        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single Location or select all.", Me.Text)
            Exit Sub
        End If
        If ChkSupplierSelect.IsChecked AndAlso cbgSupplier.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single Supplier or select all.")
            Exit Sub
        End If

        Dim frm As New RptBulkMilkMultiplePurchaseInvoice
        Dim strQuery As String = frm.GetQuery(frmDate, ToDate, Multi_InvoiceNo)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
        If dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptBulkMilkPurchaseInvoice", "Purchase Invoice", clsCommon.myCDate(dt.Rows(0)("DOC_DATE")))
            frmCRV = Nothing
        End If

    End Sub

    Private Sub btnPrindDocWise_Click(sender As Object, e As EventArgs) Handles btnPrindDocWise.Click
        Try
            Laad_CheckDoc()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    '=======================================

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        printDetails(EnumExportTo.PDF)
    End Sub
End Class
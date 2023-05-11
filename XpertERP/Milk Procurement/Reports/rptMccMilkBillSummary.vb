Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'=========added tree and shift by shivani==========='
'' work against ticket no. MIL/30/01/19-000036
Public Class RptMccMilkBillSummary
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim arrLoc As String = Nothing
    Dim ItemStructureMandatoryOnWeightConversion As Boolean = False
    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptMCCShiftReportRouteWise)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub rmEXCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmEXCEL.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub


    Private Sub chkVSPAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVSPAll.ToggleStateChanged
        cbgVSP.Enabled = Not chkVSPAll.IsChecked
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

    Private Sub RptMccMilkBillSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ItemStructureMandatoryOnWeightConversion = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False))
        SetUserMgmtNew()
        LOCATIONRIGTHS()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+p For Print ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New ")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        'LoadMCC()
        'LoadVLC()
        'LoadRoute()
        LoadVSP()
        Reset()
    End Sub

    Private Sub rmSaveLayout_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
   
    Sub LoadVSP()
        Dim qry As String = "select Vendor_Code as [Code] ,Vendor_Name as [Name]  from TSPL_VENDOR_MASTER  where Form_Type ='VSP' "
        cbgVSP.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVSP.ValueMember = "Code"
        cbgVSP.DisplayMember = "Name"

    End Sub
    Public Sub Load_Report()
        Try
            Dim sQuery As String
            Dim companyADD, CompName, CompCode As String
            If txtFromDate.Value > txtToDate.Value Then
                common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
                txtFromDate.Focus()
                Exit Sub
            End If
            If chkVSPSelect.IsChecked AndAlso cbgVSP.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow("Please select atleast single VSP or select all.")
                Exit Sub
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.")
                Exit Sub
            End If
            Dim whrcls As String = " where 2=2 "

            'If rbtnMCCRouteVLCCSelect.IsChecked Then
            Dim arr As List(Of String) = Nothing
            If cbtMCCRouteVLCC.CheckedValue.Count > 0 Then
                arr = cbtMCCRouteVLCC.CheckedValue(1)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    whrcls += "and TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(arr) + ") "
                Else
                    Throw New Exception("Please select at least one MCC")
                End If
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
                arr = cbtMCCRouteVLCC.CheckedValue(2)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    whrcls += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one Route")
                End If
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count > 2 Then
                arr = cbtMCCRouteVLCC.CheckedValue(3)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    whrcls += " and TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_no in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one Route")
                End If
            End If

            If chkVSPSelect.IsChecked And cbgVSP.CheckedValue.Count > 0 Then
                whrcls += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ")"
            End If
            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                whrcls += " and 2=( case when TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.SHIFT='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                whrcls += " and 2=( case when TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.SHIFT='E' then 3 else 2 end  )"
            End If

            whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + txtToDate.Value + "'),103) "
            sQuery = ""
            sQuery += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            companyADD = dt1.Rows(0).Item("comp_address")

            sQuery = ""
            sQuery += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            CompName = dt2.Rows(0).Item("Comp_Name")
            ' Ticket No : BHA/21/11/18-000686 By Prabhakar - for Devided by Zero error

            sQuery = ""
            sQuery += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
            Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            CompCode = dt5.Rows(0).Item("Comp_Code")

            Dim fromDate As String = txtFromDate.Value
            Dim Todate As String = txtToDate.Value

            sQuery = ""

            ''richa agarwal 24 May,2019  TEC/28/03/19-000462 add item structure on setting based
            sQuery += "select '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate ,'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,''  as compLogo1 ,'' as compLogo2,*,convert(decimal(18,2),(FAT_Per *NewQty)/100 ) as T_FAT,convert(decimal(18,2),(SNF_Per *NewQty )/100) as T_SNF,convert(decimal(18,2),EMP_Fixed_Amount) as [EMP %] ,IncentiveEMP as [EMP Amount],convert(decimal(18,2),Rate_Head_Load) as [H.L Rate] ,Head_Load_AMOUNT as [H.L Amount]" & Environment.NewLine & _
            " ,Amount as [Total Amount],isnull(Head_Load_AMOUNT,0)+isnull(Amount,0) as [Grand Total] from(select VSP_CODE,max(Vendor_Name) as Vendor_Name,convert(decimal(18,2),SUM(NewQty )) as NewQty,convert(decimal(18,5),(SUM(FATQTY )/nullif(SUM(NewQty ),0))*100) as FAT_Per,convert(decimal(18,5),(SUM(SNFQTY )/nullif(SUM(NewQty ),0))*100 )as SNF_Per,convert(decimal(18,2),SUM(Amount )/nullif(SUM(NewQty ),0)) as Rate,convert(decimal(18,2),SUM(Amount )) as Amount,MCC_CODE,ROUTE_CODE  ,sum(IncentiveEMP) as IncentiveEMP,sum(Head_Load_AMOUNT) as Head_Load_AMOUNT,avg(EMP_Fixed_Amount) as EMP_Fixed_Amount ,avg(Rate_Head_Load) as Rate_Head_Load from (select DOC_DATE,UOM_Code,convert(decimal(18,2),FATQTY*CF,0) as FATQTY,convert(decimal(18,2),SNFQTY*CF,0) as SNFQTY,convert(decimal(18,2),Qty*CF,0) as NewQty, Qty,FromUOM,TOUOM,CF,xx.RATE ,cf*Net_AMOUNT as Amount ,MCC_CODE+' -'+MCC_NAME+'  QtyMode :'+TOUOM   as MCC_CODE ,VSP_CODE ,SHIFT,ROUTE_CODE+' -'+Route_Name as ROUTE_CODE  ,Vendor_Name ,MCC_NAME,IncentiveEMP " & Environment.NewLine & _
            " ,Head_Load_AMOUNT ,EMP_Fixed_Amount,Rate_Head_Load   from (select TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code ,TSPL_MILK_RECEIPT_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty  ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER ,(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as FATQTY,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER,(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as SNFQTY  ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.RATE as RATE,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Net_AMOUNT, TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,TSPL_MILK_SAMPLE_HEAD.SHIFT, TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME,TSPL_MILK_SRN_DETAIL.EMP_Amount as IncentiveEMP,TSPL_MILK_SRN_DETAIL.Head_Load_AMOUNT,TSPL_VENDOR_MASTER.Rate_Head_Load,TSPL_VENDOR_MASTER.Actual_charges as EMP_Fixed_Amount   from TSPL_MILK_PURCHASE_INVOICE_DETAIL  Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE LEFT OUTER JOIN TSPL_MILK_SRN_DETAIL ON TSPL_MILK_SRN_DETAIL.DOC_CODE=  TSPL_MILK_SRN_HEAD.DOC_CODE left outer join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE =TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE  left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE   left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE Left Outer Join TSPL_VENDOR_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  " & Environment.NewLine & _
            " Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_RECEIPT_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code " & Environment.NewLine & _
            " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code " & Environment.NewLine & _
            " " & whrcls & " ) " & Environment.NewLine & _
            " xx  " & Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code "

            If ItemStructureMandatoryOnWeightConversion = True Then
                sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION where Contained_UOM='KG' UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF,Structure_code from TSPL_WEIGHT_CONVERSION where Container_UOM='LTR' UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code ) "
            Else
                sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION where Contained_UOM='KG' UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION where Container_UOM='LTR' UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code and lower(zzz.TOUOM)='" + cboUnit.Text + "' ) "
            End If
            sQuery += " ttt group by MCC_CODE,ROUTE_CODE,VSP_CODE  ) FFF left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_name='" & CompName & "'"



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
                If btnReferesh = False Then
                    'MilkProcurementReportViewer.funreport(dtgv, "crptMccMilkBillSummary", "MCC Milk Bill Summary")
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtgv, clsERPFuncationality.CompanyAddresShowinHeader(), "crptMccMilkBillSummary", "MCC Milk Bill Summary", "Address.rpt")
                    frmCRV = Nothing
                End If
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        
    End Sub

    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 20
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        'gv.Columns("DOC_DATE").IsVisible = False
        'gv.Columns("DOC_DATE").Width = 100
        'gv.Columns("DOC_DATE").HeaderText = " Date"
        'gv.Columns("DOC_DATE").FormatString = "{0:d}"

        'gv.Columns("SHIFT").IsVisible = False
        'gv.Columns("SHIFT").Width = 30
        'gv.Columns("SHIFT").HeaderText = " Shift"

        gv.Columns("NewQty").IsVisible = True
        gv.Columns("NewQty").Width = 100
        gv.Columns("NewQty").HeaderText = " Quantity"

        gv.Columns("FAT_Per").IsVisible = True
        gv.Columns("FAT_Per").FormatString = "{0:N2}"
        gv.Columns("FAT_Per").Width = 100
        gv.Columns("FAT_Per").HeaderText = " Fat %"

        gv.Columns("SNF_Per").IsVisible = True
        gv.Columns("SNF_Per").Width = 100
        gv.Columns("SNF_Per").FormatString = "{0:N2}"
        gv.Columns("SNF_Per").HeaderText = "Snf %"

        gv.Columns("T_Fat").IsVisible = True
        gv.Columns("T_Fat").Width = 100
        gv.Columns("T_Fat").HeaderText = "TFat"

        gv.Columns("T_SNF").IsVisible = True
        gv.Columns("T_SNF").Width = 100
        gv.Columns("T_SNF").HeaderText = "TSnf"

        gv.Columns("Rate").IsVisible = True
        gv.Columns("Rate").Width = 100
        gv.Columns("Rate").HeaderText = "Rate"

        gv.Columns("Total Amount").IsVisible = True
        gv.Columns("Total Amount").Width = 100
        gv.Columns("Total Amount").HeaderText = "Amount"

        gv.Columns("MCC_CODE").IsVisible = False
        gv.Columns("MCC_CODE").Width = 100
        gv.Columns("MCC_CODE").HeaderText = "MCC Code"

        'gv.Columns("MCC_NAME").IsVisible = False
        'gv.Columns("MCC_NAME").Width = 100
        'gv.Columns("MCC_NAME").HeaderText = "MCC Name"

        gv.Columns("VSP_CODE").IsVisible = True
        gv.Columns("VSP_CODE").Width = 100
        gv.Columns("VSP_CODE").HeaderText = "VSP Code"

        gv.Columns("Vendor_Name").IsVisible = True
        gv.Columns("Vendor_Name").Width = 100
        gv.Columns("Vendor_Name").HeaderText = "VSP Name"


        gv.Columns("ROUTE_CODE").IsVisible = False
        gv.Columns("ROUTE_CODE").Width = 100
        gv.Columns("ROUTE_CODE").HeaderText = "Route Code"

        'gv.Columns("Route_Name").IsVisible = False
        'gv.Columns("Route_Name").Width = 100
        'gv.Columns("Route_Name").HeaderText = "Route Name"

        gv.Columns("EMP %").IsVisible = True
        gv.Columns("EMP %").Width = 100
        gv.Columns("EMP %").HeaderText = "EMP %"

        gv.Columns("EMP Amount").IsVisible = True
        gv.Columns("EMP Amount").Width = 100
        gv.Columns("EMP Amount").HeaderText = "EMP Amount"

        gv.Columns("H.L Rate").IsVisible = True
        gv.Columns("H.L Rate").Width = 100
        gv.Columns("H.L Rate").HeaderText = "H.L Rate"

        gv.Columns("H.L Amount").IsVisible = True
        gv.Columns("H.L Amount").Width = 100
        gv.Columns("H.L Amount").HeaderText = "H.L Amount"

        gv.Columns("Grand Total").IsVisible = True
        gv.Columns("Grand Total").Width = 100
        gv.Columns("Grand Total").HeaderText = "Grand Total"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("NewQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("FAT_Per", "{0:F2}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("SNF_Per", "{0:F2}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item3)
        
        Dim item7 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item4 As New GridViewSummaryItem("EMP Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("H.L Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)

        Dim item10 As New GridViewSummaryItem("T_FAT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item9 As New GridViewSummaryItem("T_SNF", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)

        Dim item8 As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)

        Dim item11 As New GridViewSummaryItem("Grand Total", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)

        gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_CODE as Item format ""{0}: {1}"" Group By MCC_CODE"))
        gv.GroupDescriptors.Add(New GridGroupByExpression("ROUTE_CODE as Item format ""{0}: {1}"" Group By ROUTE_CODE"))

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True
        gv.MasterTemplate.ShowTotals = True
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        'LoadMCC()
        'LoadVLC()
        'LoadRoute()
        cboUnit.Text = "Kg"
        'rbtnMCCRouteVLCCAll.IsChecked = True
        LoadMCCRouteVLCTree()
        LoadVSP()
        LoadShiftFrom()
        LoadShiftTo()
        ' chkMCCAll.CheckState = CheckState.Checked
        'chkVLCAll.CheckState = CheckState.Checked
        'chkRouteAll.CheckState = CheckState.Checked
        chkVSPAll.CheckState = CheckState.Checked
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            'If rbtnMCCRouteVLCCSelect.IsChecked Then
            Dim arr As List(Of String)
            If cbtMCCRouteVLCC.CheckedText.Count > 0 Then
                arr = cbtMCCRouteVLCC.CheckedText(1)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                End If
            End If
            If cbtMCCRouteVLCC.CheckedText.Count > 1 Then
                arr = cbtMCCRouteVLCC.CheckedText(2)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                End If
            End If
            If cbtMCCRouteVLCC.CheckedText.Count > 2 Then
                arr = cbtMCCRouteVLCC.CheckedText(3)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                End If
            End If
            'End If
            If chkVSPSelect.IsChecked Then
                Dim stVSPName As String = ""
                For Each StrName As String In cbgVSP.CheckedDisplayMember
                    If clsCommon.myLen(stVSPName) > 0 Then
                        stVSPName += ", "
                    End If
                    stVSPName += StrName
                Next
                Dim strVSPCode As String = ""
                For Each StrCode As String In cbgVSP.CheckedValue
                    If clsCommon.myLen(strVSPCode) > 0 Then
                        strVSPCode += ", "
                    End If
                    strVSPCode += StrCode
                Next
                arrHeader.Add(("VSP Name: " + stVSPName + " "))
            End If
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("MCC Milk Bill Summary", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("MCC Milk Bill Summary", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub RptMccMilkBillSummary_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            Load_Report()
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        btnReferesh = False
        Load_Report()
    End Sub

    Sub LoadMCCRouteVLCTree()
        'Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.Route_Code as ParentCode,3 as Lvl from TSPL_VLC_MASTER_HEAD where len(isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 union all   select TSPL_MCC_ROUTE_MASTER.Route_Code as Code,TSPL_MCC_ROUTE_MASTER.Route_Name as Name,TSPL_MCC_ROUTE_MASTER.MCC_Code as ParentCode,2 as Lvl from TSPL_MCC_ROUTE_MASTER where len(isnull(TSPL_MCC_ROUTE_MASTER.MCC_Code,''))>0  union all   select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name,null as ParentCode,1 as Lvl from TSPL_MCC_MASTER"
        'cbtMCCRouteVLCC.ValueMember = "Code"
        'cbtMCCRouteVLCC.DisplayMember = "Name"
        'cbtMCCRouteVLCC.ParentValue = "ParentCode"
        'cbtMCCRouteVLCC.DataSource = clsDBFuncationality.GetDataTable(qry)


        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.Route_Code as ParentCode,3 as Lvl from TSPL_VLC_MASTER_HEAD where len(isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 union all   select TSPL_MCC_ROUTE_MASTER.Route_Code as Code,TSPL_MCC_ROUTE_MASTER.Route_Name as Name,TSPL_MCC_ROUTE_MASTER.MCC_Code as ParentCode,2 as Lvl from TSPL_MCC_ROUTE_MASTER where len(isnull(TSPL_MCC_ROUTE_MASTER.MCC_Code,''))>0  union all   select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name,null as ParentCode,1 as Lvl from TSPL_MCC_MASTER   where TSPL_MCC_MASTER.MCC_Code in (" + arrLoc + ") "
            dt = clsDBFuncationality.GetDataTable(qry)
        End If

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            btnGo.Enabled = False
        Else
            cbtMCCRouteVLCC.DataSource = dt
            cbtMCCRouteVLCC.ValueMember = "Code"
            cbtMCCRouteVLCC.DisplayMember = "Name"
            cbtMCCRouteVLCC.ParentValue = "ParentCode"
        End If
    End Sub

    Private Sub rbtnMCCRouteVLCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        'cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked
    End Sub
    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"
        'cbgShift.DisplayMember = "Shift"
    End Sub
    Sub LoadShiftTo()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtToShift.DataSource = dt
        txtToShift.ValueMember = "Code"
        'cbgShift.DisplayMember = "Shift"
    End Sub



End Class

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'=====shivani Tyagi ticket no. [BM00000004835, preeti gupta-BM00000007971]
Public Class RptPendingMilkSRN
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptPendingMilkSRN)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Private Sub RptPendingMilkSRN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ItemStructureMandatoryOnWeightConversion = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False))
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+R Adding New")
        Reset()
    End Sub

    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(RbSummary.IsChecked = True, "S", "D")
        TemplateGridview = gv
        Load_Report()
    End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadLocation()
        LoadVendor()
        cboUnit.Text = "Kg"
        chkLocationAll.CheckState = CheckState.Checked
        If chkLocationAll.IsChecked Then
            cbgLocation.CheckedAll()
        Else
            cbgLocation.UnCheckedAll()
        End If
        chkVendorAll.CheckState = CheckState.Checked
        RbAll.CheckState = CheckState.Checked
        RbDetail.CheckState = CheckState.Checked
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                If chkLocationSelect.IsChecked Then
                    Dim strLocationName As String = ""
                    For Each StrName As String In cbgLocation.CheckedDisplayMember
                        If clsCommon.myLen(strLocationName) > 0 Then
                            strLocationName += ", "
                        End If
                        strLocationName += StrName
                    Next
                    Dim strLocationCode As String = ""
                    For Each StrCode As String In cbgLocation.CheckedValue
                        If clsCommon.myLen(strLocationCode) > 0 Then
                            strLocationCode += ", "
                        End If
                        strLocationCode += StrCode
                    Next
                    arrHeader.Add((" Location Name: " + strLocationName + " "))
                End If
                If chkVendorSelect.IsChecked Then
                    Dim strVendorName As String = ""
                    For Each StrName As String In cbgVendor.CheckedDisplayMember
                        If clsCommon.myLen(strVendorName) > 0 Then
                            strVendorName += ", "
                        End If
                        strVendorName += StrName
                    Next
                    Dim strVendorcode As String = ""
                    For Each StrCode As String In cbgVendor.CheckedValue
                        If clsCommon.myLen(strVendorcode) > 0 Then
                            strVendorcode += ", "
                        End If
                        strVendorcode += StrCode
                    Next
                    arrHeader.Add(("Vendor Name: " + strVendorName + " "))
                End If
                arrHeader.Add("Report Type: " + IIf(RbSummary.IsChecked = True, "Summary", "Detail"))
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("Pending Milk SRN Report", gv, arrHeader, Me.Text)
                Else
                    clsCommon.MyExportToPDF("Pending Milk SRN Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Public Sub Load_Report()
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Location or select all.", Me.Text)
            Exit Sub
        End If
        If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Vendor or select all.", Me.Text)
            Exit Sub
        End If
        Dim sQuery As String = ""

        If RbAll.IsChecked = True Then
            If RbDetail.IsChecked = True Then
                sQuery += "select xx.Location ,xx.Location_Desc as Location_Name,xx.VSP_CODE as Vendor_Code,xx.Vendor_Name ,xx.DOC_CODE as SRN_No,convert(varchar,xx.DOC_DATE,103) as SRN_Date,xx.DOC_DATE as DocDate,convert(decimal(18,2),CF*xx.AMOUNT) as Amount,xx.MILK_SAMPLE_CODE as Sample_Code  from (select t_detail.*  from  (select TSPL_MILK_SRN_DETAIL.Item_Code ,VSP_CODE ,Vendor_Name,Location_Desc,TSPL_MILK_SAMPLE_HEAD. MCC_Code +' -('+Location_Desc +')'  as Location ,UOM_Code,TSPL_MILK_SRN_DETAIL.AMOUNT ,TSPL_MILK_SRN_HEAD.DOC_CODE ,TSPL_MILK_SRN_HEAD.DOC_DATE ,TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE from TSPL_MILK_SRN_HEAD left join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE  =TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MILK_SAMPLE_HEAD.MCC_CODE left join TSPL_VENDOR_MASTER on  TSPL_VENDOR_MASTER.Vendor_Code =TSPL_MILK_SRN_HEAD.VSP_CODE  left join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE =TSPL_MILK_SRN_HEAD.DOC_CODE left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE =TSPL_MILK_SRN_HEAD.DOC_CODE  " & Environment.NewLine & _
                 " where 2 = 2 and  TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE is null and Form_Type ='VSP' and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)" & Environment.NewLine
               
                If cbgLocation.CheckedValue.Count > 0 Then
                    sQuery += " and TSPL_LOCATION_MASTER. Location_Code   IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                    sQuery += "and TSPL_VENDOR_MASTER.Vendor_Code   IN (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") "
                End If
                sQuery += "  )t_detail) xx " & Environment.NewLine & _
                    " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code "

                ''richa agarwal 28 May,2019  TEC/28/03/19-000462 add item structure on setting based
                If ItemStructureMandatoryOnWeightConversion = True Then
                    sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code  "
                Else
                    sQuery += "left outer join  (Select Distinct yyy.* From ( " & Environment.NewLine & _
                     " Select TOP 1 Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION WHERE Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
                    " UNION All Select  TOP 1 Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
                    " UNION All   Select  TOP 1 Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
                    " UNION All Select  TOP 1  Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
                     " ) yyy) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='" + cboUnit.Text + "'"
                End If
                sQuery += "   order by DocDate "

            ElseIf RbSummary.IsChecked = True Then
                sQuery += "select tt.Location ,tt.Location_Name ,tt.Vendor_Code ,tt.Vendor_Name ,sum (tt.Amount ) as Amount   from (select xx.Location as Location,xx.VSP_CODE as Vendor_Code,xx.Vendor_Name ,xx.DOC_CODE as SRN_No,xx.DOC_DATE as SRN_Date,convert(decimal(18,2),CF*xx.AMOUNT) as Amount ,xx.MILK_SAMPLE_CODE as Sample_Code,xx.Location_Desc as Location_Name  from (select t_detail.*  from  (select TSPL_MILK_SRN_DETAIL.Item_Code ,VSP_CODE ,Vendor_Name,TSPL_MILK_SAMPLE_HEAD. MCC_Code +' -('+Location_Desc +')'  as Location  ,UOM_Code,TSPL_MILK_SRN_DETAIL.AMOUNT ,TSPL_MILK_SRN_HEAD.DOC_CODE ,TSPL_MILK_SRN_HEAD.DOC_DATE ,TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE ,Location_Desc from TSPL_MILK_SRN_HEAD left join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE  =TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MILK_SAMPLE_HEAD.MCC_CODE left join TSPL_VENDOR_MASTER on  TSPL_VENDOR_MASTER.Vendor_Code =TSPL_MILK_SRN_HEAD.VSP_CODE  left join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE =TSPL_MILK_SRN_HEAD.DOC_CODE left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE =TSPL_MILK_SRN_HEAD.DOC_CODE " & Environment.NewLine & _
                 "   where 2 = 2 and  TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE is null and Form_Type ='VSP' and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)" & Environment.NewLine


                If cbgLocation.CheckedValue.Count > 0 Then '
                    sQuery += "and TSPL_LOCATION_MASTER. Location_Code  IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                    sQuery += "and TSPL_VENDOR_MASTER.Vendor_Code    IN (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") "
                End If
                sQuery += " )t_detail ) xx " & Environment.NewLine & _
                     " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code "

                ''richa agarwal 28 May,2019  TEC/28/03/19-000462 add item structure on setting based
                If ItemStructureMandatoryOnWeightConversion = True Then
                    sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code  "
                Else
                    sQuery += " left outer join  (Select Distinct yyy.* From ( " & Environment.NewLine & _
                    " Select TOP 1 Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION WHERE Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
                    " UNION All Select  TOP 1 Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
                    " UNION All   Select  TOP 1 Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
                    " UNION All Select  TOP 1  Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
                    "  ) yyy) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='" + cboUnit.Text + "'"
                End If
                sQuery += " )tt group by Location ,Vendor_Name,Location_Name ,Vendor_Code "
            End If

        Else
            '=============================================
            If RbDetail.IsChecked = True Then
                sQuery += "select xx.Location ,xx.Location_Desc as Location_Name,xx.VSP_CODE as Vendor_Code,xx.Vendor_Name ,xx.DOC_CODE as SRN_No,xx.DOC_DATE as DocDate,convert(varchar,xx.DOC_DATE,103) as SRN_Date,convert(decimal(18,2),CF*xx.AMOUNT) as Amount ,xx.MILK_SAMPLE_CODE as Sample_Code  from (select t_detail.*  from  (select TSPL_MILK_SRN_DETAIL.Item_Code ,VSP_CODE,Location_Desc ,Vendor_Name,TSPL_MILK_SAMPLE_HEAD. MCC_Code +' -('+Location_Desc +')'  as Location  ,UOM_Code,TSPL_MILK_SRN_DETAIL.AMOUNT ,TSPL_MILK_SRN_HEAD.DOC_CODE ,TSPL_MILK_SRN_HEAD.DOC_DATE ,TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE from TSPL_MILK_SRN_HEAD left join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE  =TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MILK_SAMPLE_HEAD.MCC_CODE left join TSPL_VENDOR_MASTER on  TSPL_VENDOR_MASTER.Vendor_Code =TSPL_MILK_SRN_HEAD.VSP_CODE  left join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE =TSPL_MILK_SRN_HEAD.DOC_CODE left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE =TSPL_MILK_SRN_HEAD.DOC_CODE  " & Environment.NewLine & _
                " where 2 = 2 and  TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE is null and Form_Type ='VSP' and  TSPL_MILK_SAMPLE_HEAD.Posted ='0'   and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)" & Environment.NewLine

                If cbgLocation.CheckedValue.Count > 0 Then
                    sQuery += "and TSPL_LOCATION_MASTER. Location_Code  IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                    sQuery += "and TSPL_VENDOR_MASTER.Vendor_Code  IN (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") "
                End If
                sQuery += " )t_detail ) xx " & Environment.NewLine & _
                 " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code "

                ''richa agarwal 28 May,2019  TEC/28/03/19-000462 add item structure on setting based
                If ItemStructureMandatoryOnWeightConversion = True Then
                    sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code  "
                Else
                    sQuery += "left outer join  (Select Distinct yyy.* From ( " & Environment.NewLine & _
                    " Select TOP 1 Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION WHERE Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
                    " UNION All Select  TOP 1 Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
                    " UNION All   Select  TOP 1 Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
                    " UNION All Select  TOP 1  Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
                    ") yyy) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='" + cboUnit.Text + "'"
                End If
                sQuery += " order by SRN_Date "
            ElseIf RbSummary.IsChecked = True Then
                sQuery += "select tt.Location ,tt.Location_Name ,tt.Vendor_Code ,tt.Vendor_Name ,sum (tt.Amount ) as Amount   from (select xx.Location as Location,xx.VSP_CODE as Vendor_Code,xx.Vendor_Name ,xx.DOC_CODE as SRN_No,xx.DOC_DATE as SRN_Date,convert(decimal(18,2),CF*xx.AMOUNT) as Amount ,xx.MILK_SAMPLE_CODE as Sample_Code,xx.Location_Desc as Location_Name  from (select t_detail.*  from  (select TSPL_MILK_SRN_DETAIL.Item_Code ,VSP_CODE ,Vendor_Name,TSPL_MILK_SAMPLE_HEAD. MCC_Code +' -('+Location_Desc +')'  as Location  ,UOM_Code,TSPL_MILK_SRN_DETAIL.AMOUNT ,TSPL_MILK_SRN_HEAD.DOC_CODE ,TSPL_MILK_SRN_HEAD.DOC_DATE ,TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE ,Location_Desc from TSPL_MILK_SRN_HEAD left join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE  =TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MILK_SAMPLE_HEAD.MCC_CODE left join TSPL_VENDOR_MASTER on  TSPL_VENDOR_MASTER.Vendor_Code =TSPL_MILK_SRN_HEAD.VSP_CODE  left join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE =TSPL_MILK_SRN_HEAD.DOC_CODE left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE =TSPL_MILK_SRN_HEAD.DOC_CODE" & Environment.NewLine & _
                "   where 2 = 2 and  TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE is null and Form_Type ='VSP' and  TSPL_MILK_SAMPLE_HEAD.Posted ='0'  and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)" & Environment.NewLine

                If cbgLocation.CheckedValue.Count > 0 Then
                    sQuery += " and TSPL_LOCATION_MASTER. Location_Code   IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                    sQuery += "and TSPL_VENDOR_MASTER.Vendor_Code    IN (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") "
                End If
                sQuery += "   )t_detail) xx " & Environment.NewLine & _
                  " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code "

                ''richa agarwal 28 May,2019  TEC/28/03/19-000462 add item structure on setting based
                If ItemStructureMandatoryOnWeightConversion = True Then
                    sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code  "
                Else
                    sQuery += " left outer join  (Select Distinct yyy.* From ( " & Environment.NewLine & _
                    " Select TOP 1 Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION WHERE Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
                    " UNION All Select  TOP 1 Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
                    " UNION All   Select  TOP 1 Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
                    " UNION All Select  TOP 1  Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
                    " ) yyy) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='" + cboUnit.Text + "'"
                End If

                sQuery += " )tt group by Location ,Vendor_Name,Location_Name ,Vendor_Code "

            End If
        End If
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
            clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
    End Sub
   

    Private Sub RptPendingMilkSRN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
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
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code as [Code] ,Vendor_Name as [Name] from TSPL_VENDOR_MASTER where Form_Type ='VSP' and Status='N' "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Code"
        cbgVendor.DisplayMember = "Name"
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
   
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
        If chkLocationAll.IsChecked Then
            cbgLocation.CheckedAll()
        Else
            cbgLocation.UnCheckedAll()
        End If
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub

    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next
        If (RbPending.IsChecked And RbSummary.IsChecked) Or (RbAll.IsChecked And RbSummary.IsChecked) = True Then
            gv.Columns("Location").IsVisible = True
            gv.Columns("Location").Width = 100
            gv.Columns("Location").HeaderText = "Location"

            gv.Columns("Location_Name").IsVisible = True
            gv.Columns("Location_Name").Width = 100
            gv.Columns("Location_Name").HeaderText = "Location Name"

            gv.Columns("Vendor_Code").IsVisible = False
            gv.Columns("Vendor_Code").Width = 100
            gv.Columns("Vendor_Code").HeaderText = "Vendor Code"

            gv.Columns("Vendor_Name").IsVisible = True
            gv.Columns("Vendor_Name").Width = 100
            gv.Columns("Vendor_Name").HeaderText = "Vendor Name"

            gv.Columns("Amount").IsVisible = True
            gv.Columns("Amount").Width = 100
            gv.Columns("Amount").HeaderText = "Amount"

        ElseIf (RbPending.IsChecked And RbDetail.IsChecked) Or (RbAll.IsChecked And RbDetail.IsChecked) = True Then
            gv.Columns("Location").IsVisible = True
            gv.Columns("Location").Width = 100
            gv.Columns("Location").HeaderText = "Location"

            gv.Columns("Location_Name").IsVisible = True
            gv.Columns("Location_Name").Width = 100
            gv.Columns("Location_Name").HeaderText = "Location Name"

            gv.Columns("Vendor_Code").IsVisible = True
            gv.Columns("Vendor_Code").Width = 100
            gv.Columns("Vendor_Code").HeaderText = "Vendor Code"

            gv.Columns("Vendor_Name").IsVisible = True
            gv.Columns("Vendor_Name").Width = 100
            gv.Columns("Vendor_Name").HeaderText = "Vendor Name"

            gv.Columns("SRN_No").IsVisible = True
            gv.Columns("SRN_No").Width = 100
            gv.Columns("SRN_No").HeaderText = " SRN No."

            gv.Columns("SRN_Date").IsVisible = True
            gv.Columns("SRN_Date").Width = 100
            gv.Columns("SRN_Date").HeaderText = " SRN Date"
            gv.Columns("SRN_Date").FormatString = "{0:d}"

            gv.Columns("Amount").IsVisible = True
            gv.Columns("Amount").Width = 100
            gv.Columns("Amount").HeaderText = "Amount"

            gv.Columns("Sample_Code").IsVisible = True
            gv.Columns("Sample_Code").Width = 100
            gv.Columns("Sample_Code").HeaderText = " Milk Sample Code"
        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        gv.GroupDescriptors.Add(New GridGroupByExpression("Location as Item format ""{0}: {1}"" Group By Location"))
        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
           print(EnumExportTo.PDF)
    End Sub
End Class

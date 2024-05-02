Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'--preeti gupta ticket no.[BM00000004213,BM00000004474]
Public Class RptMonthlyVLCProcurement1
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    '=========added tree and shift by shivani==========='
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptMonthlyVLCProcurement)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
            End If
        End If
        RadSplitButton4.Visible = MyBase.isExport
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

    Public Sub Load_Report()
        Try

            If txtFromDate.Value > txtToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can't be greater than to Date", Me.Text)
                txtFromDate.Focus()
                Exit Sub
            End If

            If cboUnit.Text = "" Then
                clsCommon.MyMessageBoxShow(Me, "Please select Unit", Me.Text)
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select atleast single MCC or select all.", Me.Text)
                Exit Sub
            End If
            'Sanjay Ticket No-TEC/04/07/19-000928, case when sum(NewQty)>0
            'Dim squery As String = "select RANK() over(partition by DOC_DATE order by DOC_DATE,Shift,VLC) as [SamNO], convert(date,DOC_DATE,103) as DOC_DATE,DOC_DATE as Document_date,Shift,VLC,VLC_Name,convert(decimal(18,2),QTY)as QTY ,convert(decimal(18,1),[FAT %])as [FAT %] ,convert(decimal(18,1),[SNF %])as [SNF %]  ,Convert(Decimal(18,2),([FAT %] *QTY /100)) as [FAT in kg],Convert(DECIMAL(18,2),([SNF %] *QTY /100)) as [SNF in kg] from (select convert(varchar,DOC_DATE,103) as DOC_DATE,MAX(TOUOM ) as UOM,max(shift) as shift,VLC_CODE as VLC,max(VLC_Name) as VLC_Name,convert(Decimal(18,2),sum(FATQTY)/sum(NewQty)*100) as [FAT %], convert(DECIMAL(18,2),sum(SNFQTY )/sum(NewQty)*100) as [SNF %],SUM(isnull(NewQty,0) ) as QTY from(" & Environment.NewLine & _
            Dim squery As String = "select RANK() over(partition by DOC_DATE order by DOC_DATE,Shift,VLC) as [SamNO], convert(date,DOC_DATE,103) as DOC_DATE,DOC_DATE as Document_date,Shift,VLC,VLC_Name,convert(decimal(18,2),QTY)as QTY ,convert(decimal(18,1),[FAT %])as [FAT %] ,convert(decimal(18,1),[SNF %])as [SNF %]  ,Convert(Decimal(18,2),([FAT %] *QTY /100)) as [FAT in kg],Convert(DECIMAL(18,2),([SNF %] *QTY /100)) as [SNF in kg] from (select convert(varchar,DOC_DATE,103) as DOC_DATE,MAX(TOUOM ) as UOM,max(shift) as shift,VLC_CODE as VLC,max(VLC_Name) as VLC_Name,case when sum(NewQty)>0 then convert(Decimal(18,2),sum(FATQTY)/sum(NewQty)*100) else 0 end as [FAT %],case when sum(NewQty)>0 then convert(DECIMAL(18,2),sum(SNFQTY )/sum(NewQty)*100) else 0 end as [SNF %],SUM(isnull(NewQty,0) ) as QTY from(" & Environment.NewLine &
            " select DOC_DATE,UOM_Code,shift,VLC_CODE,VLC_Name,FATQTY*CF as FATQTY,SNFQTY*CF as SNFQTY,Qty*CF as NewQty, Qty,FromUOM,TOUOM,CF   from(" & Environment.NewLine &
            " select TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_MILK_SRN_HEAD.DOC_DATE,TSPL_MILK_SRN_HEAD.shift,TSPL_MILK_SRN_DETAIL.UOM_Code ,TSPL_MILK_SRN_HEAD.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name ,TSPL_MILK_SRN_DETAIL.FAT_PER,(TSPL_MILK_SRN_DETAIL.FAT_PER*TSPL_MILK_SRN_DETAIL.Qty/100) as FATQTY ,TSPL_MILK_SRN_DETAIL.SNF_PER,(TSPL_MILK_SRN_DETAIL.SNF_PER*TSPL_MILK_SRN_DETAIL.Qty /100) as SNFQTY ,TSPL_MILK_SRN_DETAIL.Qty  from TSPL_MILK_SRN_DETAIL left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE =TSPL_MILK_SRN_DETAIL.DOC_CODE" & Environment.NewLine &
            " left outer join TSPL_MCC_MASTER  on TSPL_MCC_MASTER .MCC_Code =TSPL_MILK_SRN_HEAD.MCC_CODE   " & Environment.NewLine &
            " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_SRN_HEAD.VLC_CODE" & Environment.NewLine &
            " where 2 = 2" & Environment.NewLine &
            " and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103) "

            Dim arr As List(Of String) = Nothing
            If cbtMCCRouteVLCC.CheckedValue.Count > 0 Then
                arr = cbtMCCRouteVLCC.CheckedValue(1)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    squery += "and TSPL_MILK_SRN_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(arr) + ") "
                Else
                    Throw New Exception("Please select at least one MCC")
                End If
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
                arr = cbtMCCRouteVLCC.CheckedValue(2)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    squery += " and TSPL_MILK_SRN_HEAD.ROUTE_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one Route")
                End If
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count > 2 Then
                arr = cbtMCCRouteVLCC.CheckedValue(3)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    squery += " and TSPL_MILK_SRN_HEAD.VLC_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one Route")
                End If
            End If
            'End If
            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                squery += " and 2=( case when TSPL_MILK_SRN_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.SHIFT='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                squery += " and 2=( case when TSPL_MILK_SRN_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.SHIFT='E' then 3 else 2 end  )"
            End If

            squery += " ) xx  left outer join tspl_item_master on tspl_item_master.item_code= xx.Item_Code "

            ''richa agarwal 24 May,2019  TEC/28/03/19-000462 add item structure on setting based
            If ItemStructureMandatoryOnWeightConversion = True Then
                squery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =UOM_Code   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code "
            Else
                squery += "  left outer join (Select Distinct yyy.* From ( " & Environment.NewLine &
                " Select TOP 1 Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION WHERE Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine &
                " UNION All Select  TOP 1 Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine &
                " UNION All   Select  TOP 1 Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine &
                " UNION All Select  TOP 1  Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine &
                " ) yyy) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='" + cboUnit.Text + "' "
            End If


            squery += "  ) ttt group by DOC_DATE,VLC_CODE ) ff order by convert(date,ff.DOC_DATE,103)"




            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(squery)
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
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 20
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("VLC").IsVisible = False
        gv.Columns("VLC").Width = 20
        gv.Columns("VLC").HeaderText = "VLC"

        gv.Columns("Document_date").IsVisible = True
        gv.Columns("Document_date").Width = 100
        gv.Columns("Document_date").HeaderText = " Date"
        gv.Columns("Document_date").FormatString = "{0:d}"

        gv.Columns("SamNO").IsVisible = True
        gv.Columns("SamNO").Width = 40
        gv.Columns("SamNO").HeaderText = "SNo."

        gv.Columns("VLC_Name").IsVisible = True
        gv.Columns("VLC_Name").Width = 100
        gv.Columns("VLC_Name").HeaderText = "VLC/VILLAGE"


        gv.Columns("DOC_DATE").IsVisible = True
        gv.Columns("DOC_DATE").Width = 100
        gv.Columns("DOC_DATE").HeaderText = " Date"
        gv.Columns("DOC_DATE").FormatString = "dd-MMM-yyyy"

        gv.Columns("Shift").IsVisible = True
        gv.Columns("Shift").Width = 100
        gv.Columns("Shift").HeaderText = "Shift"

        gv.Columns("Qty").IsVisible = True
        gv.Columns("Qty").Width = 100
        gv.Columns("Qty").HeaderText = "Qty"


        gv.Columns("FAT %").IsVisible = True
        gv.Columns("FAT %").Width = 100
        gv.Columns("FAT %").HeaderText = "FAT%"


        gv.Columns("SNF %").IsVisible = True
        gv.Columns("SNF %").Width = 100
        gv.Columns("SNF %").HeaderText = "SNF %"


        gv.Columns("FAT in kg").IsVisible = True
        gv.Columns("FAT in kg").Width = 100
        gv.Columns("FAT in kg").HeaderText = "FAT Kg"

        gv.Columns("SNF in kg").IsVisible = True
        gv.Columns("SNF in kg").Width = 100
        gv.Columns("SNF in kg").HeaderText = "SNF Kg"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("QTY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        gv.GroupDescriptors.Add(New GridGroupByExpression("DOC_DATE as Item format ""{0}: {1}"" Group By DOC_DATE"))
        gv.GroupDescriptors.Add(New GridGroupByExpression("VLC_CODE as Item format ""{0}: {1}"" Group By VLC_CODE"))
        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
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
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
    End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        cboUnit.Text = "Kg"
        LoadMCCRouteVLCTree()
        LoadShiftFrom()
        LoadShiftTo()
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
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
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("Monthly VLC Procurement", gv, arrHeader, Me.Text)
                Else
                    clsCommon.MyExportToPDF("Monthly VLC Procurement", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub


    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub


    Private Sub butnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butnClose.Click
        Me.Close()
    End Sub

    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub RptMonthlyVLCProcurement1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ItemStructureMandatoryOnWeightConversion = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False))
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+R ")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        Reset()
    End Sub

    Private Sub RptMonthlyVLCProcurement1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub
    Sub LoadMCCRouteVLCTree()
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
    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class

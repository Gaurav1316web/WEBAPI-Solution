Imports System.IO
Imports common
Public Class frmCompareVoucherReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub rptMccCollectionDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value

        cboTransType.DataSource = LoadTranType()
        cboTransType.ValueMember = "Code"
        cboTransType.DisplayMember = "Name"
        cboTransType.SelectedIndex = 0
        Reset()
    End Sub
    Public Function LoadTranType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "All"
        dr("Name") = "ALL"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Posted"
        dr("Name") = "Posted"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Unposted"
        dr("Name") = "Unposted"
        dt.Rows.Add(dr)
        Return dt
    End Function
    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()

        Dim viewBlank As New TableViewDefinition()
        Gv1.ViewDefinition = viewBlank
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        ControlEnableDisable(True)
    End Sub
    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Dim qry As String = " select ROUTE_NO,ROUTE_NAME from TSPL_BULK_ROUTE_MASTER "
        txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSelRoute@cvr", qry, "ROUTE_NO", "ROUTE_NO", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
    End Sub
    Sub ControlEnableDisable(ByVal isEnable As Boolean)
        txtRoute.Enabled = isEnable
        txtFromDate.Enabled = isEnable
        txtToDate.Enabled = isEnable
        cboTransType.Enabled = isEnable

    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim strFromDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt")
            Dim strToDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt")
            Dim qry As String = "select Route_Code
,sum(Qty * case when ri=1 then 1 else 0 end) as TankerQty 
,sum(FATKg * case when ri=1 then 1 else 0 end) as TankerFATKg
,sum(SNFKg * case when ri=1 then 1 else 0 end) as TankerSNFKg
,sum(Qty * case when ri=2 then 1 else 0 end) as BMCQty 
,sum(FATKg * case when ri=2 then 1 else 0 end) as BMCFATKg
,sum(SNFKg * case when ri=2 then 1 else 0 end) as BMCSNFKg
,sum(Qty * case when ri=3 then 1 else 0 end) as DCSQty 
,sum(FATKg * case when ri=3 then 1 else 0 end) as DCSFATKg
,sum(SNFKg * case when ri=3 then 1 else 0 end) as DCSSNFKg
,sum(Qty * case when ri=1 then 1 else case when ri=2 then -1 else 0 end end) as TankerBMCQty 
,sum(FATKg * case when ri=1 then 1 else case when ri=2 then -1 else 0 end end) as TankerBMCFATKg
,sum(SNFKg * case when ri=1 then 1 else case when ri=2 then -1 else 0 end end) as TankerBMCSNFKg
,sum(Qty * case when ri=2 then 1 else case when ri=3 then -1 else 0 end end) as BMCDCSQty 
,sum(FATKg * case when ri=2 then 1 else case when ri=3 then -1 else 0 end end) as BMCDCSFATKg
,sum(SNFKg * case when ri=2 then 1 else case when ri=3 then -1 else 0 end end) as BMCDCSSNFKg
,sum(Qty * case when ri=1 then 1 else case when ri=3 then -1 else 0 end end) as TankerDCSQty 
,sum(FATKg * case when ri=1 then 1 else case when ri=3 then -1 else 0 end end) as TankerDCSFATKg
,sum(SNFKg * case when ri=1 then 1 else case when ri=3 then -1 else 0 end end) as TankerDCSSNFKg
from (
select TSPL_MILK_COLLECTION_MCC.Document_No,TSPL_MILK_COLLECTION_MCC.Route_Code,TSPL_MILK_COLLECTION_MCC.Entered_Qty as Qty,TSPL_MILK_COLLECTION_MCC.Entered_FATKg as FATKg,TSPL_MILK_COLLECTION_MCC.Entered_SNFKg as SNFKg,1 as RI ,1 as Chk
from TSPL_MILK_COLLECTION_MCC 
where Document_Date>='" + strFromDate + "' and Document_Date<='" + strToDate + "'"
            If clsCommon.CompairString(cboTransType.SelectedValue, "Posted") = CompairStringResult.Equal Then
                qry += " and isnull(TSPL_MILK_COLLECTION_MCC.Status,0)=1"
            ElseIf clsCommon.CompairString(cboTransType.SelectedValue, "Unposted") = CompairStringResult.Equal Then
                qry += " and isnull(TSPL_MILK_COLLECTION_MCC.Status,0)=0"
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_COLLECTION_MCC.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
            End If


            qry += " union all
select TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No,TSPL_MILK_COLLECTION_MCC.Route_Code,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty,TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG,TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG,2 as RI ,0 as Chk
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
where  TSPL_MILK_COLLECTION_MCC.Document_Date>='" + strFromDate + "' and TSPL_MILK_COLLECTION_MCC.Document_Date<='" + strToDate + "' "
            If clsCommon.CompairString(cboTransType.SelectedValue, "Posted") = CompairStringResult.Equal Then
                qry += " and isnull(TSPL_MILK_COLLECTION_MCC.Status,0)=1"
            ElseIf clsCommon.CompairString(cboTransType.SelectedValue, "Unposted") = CompairStringResult.Equal Then
                qry += " and isnull(TSPL_MILK_COLLECTION_MCC.Status,0)=0"
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_COLLECTION_MCC.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
            End If


            qry += " union all
select TSPL_MILK_COLLECTION_MCC.Document_No,TSPL_MILK_COLLECTION_MCC.Route_Code,TSPL_MILK_COLLECTION_DCS_DETAIL.Qty,TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG,TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG,3 as RI ,0 as Chk
from TSPL_MILK_COLLECTION_DCS_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
left outer join (select TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No as DCSDocNo,TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No as MCCDocNo 
from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL 
left outer join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail 
where  TSPL_MILK_COLLECTION_MCC_DETAIL.Sample_No=1 and TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id is not null) as TabBMCDCS on TabBMCDCS.DCSDocNo=TSPL_MILK_COLLECTION_DCS.Document_No
left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TabBMCDCS.MCCDocNo
where TSPL_MILK_COLLECTION_MCC.Document_Date>='" + strFromDate + "' and TSPL_MILK_COLLECTION_MCC.Document_Date<='" + strToDate + "'"

            If clsCommon.CompairString(cboTransType.SelectedValue, "Posted") = CompairStringResult.Equal Then
                qry += " and isnull(TSPL_MILK_COLLECTION_DCS.Status,0)=1"
            ElseIf clsCommon.CompairString(cboTransType.SelectedValue, "Unposted") = CompairStringResult.Equal Then
                qry += " and isnull(TSPL_MILK_COLLECTION_DCS.Status,0)=0"
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_COLLECTION_MCC.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
            End If
            qry += "  )xx group by Route_Code having sum(chk)>0"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()

            Dim viewBlank As New TableViewDefinition()
            Gv1.ViewDefinition = viewBlank
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                SetGridFormationOFGV1()


                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
                Dim summaryRowItem As New GridViewSummaryRowItem()



                Dim MCCQty As New GridViewSummaryItem("TankerQty", "{0:n2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(MCCQty)
                Dim MCCFatKg As New GridViewSummaryItem("TankerFATKg", "{0:n3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(MCCFatKg)
                Dim MCCSnfKg As New GridViewSummaryItem("TankerSNFKg", "{0:n3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(MCCSnfKg)
                Dim DCSQty As New GridViewSummaryItem("BMCQty", "{0:n2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(DCSQty)
                Dim DCSFatKg As New GridViewSummaryItem("BMCFATKg", "{0:n3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(DCSFatKg)
                Dim DCSSnfKg As New GridViewSummaryItem("BMCSNFKg", "{0:n3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(DCSSnfKg)
                Dim DiffQty As New GridViewSummaryItem("DCSQty", "{0:n2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(DiffQty)
                Dim DiffFatKg As New GridViewSummaryItem("DCSFATKg", "{0:n3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(DiffFatKg)
                Dim DiffSnfKg As New GridViewSummaryItem("DCSSNFKg", "{0:n3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(DiffSnfKg)

                Dim TANBMCQty As New GridViewSummaryItem("TankerBMCQty", "{0:n2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(TANBMCQty)
                Dim TANBMCFATKg As New GridViewSummaryItem("TankerBMCFATKg", "{0:n3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(TANBMCFATKg)
                Dim TANBMCSNFKg As New GridViewSummaryItem("TankerBMCSNFKg", "{0:n3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(TANBMCSNFKg)

                Dim BMCDCSQty As New GridViewSummaryItem("BMCDCSQty", "{0:n2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(BMCDCSQty)
                Dim BMCDCSFATKg As New GridViewSummaryItem("BMCDCSFATKg", "{0:n3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(BMCDCSFATKg)
                Dim BMCDCSSNFKg As New GridViewSummaryItem("BMCDCSSNFKg", "{0:n3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(BMCDCSSNFKg)

                Dim TankerDCSQty As New GridViewSummaryItem("TankerDCSQty", "{0:n2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(TankerDCSQty)
                Dim TankerDCSFATKg As New GridViewSummaryItem("TankerDCSFATKg", "{0:n3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(TankerDCSFATKg)
                Dim TankerDCSSNFKg As New GridViewSummaryItem("TankerDCSSNFKg", "{0:n3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(TankerDCSSNFKg)
                Gv1.SummaryRowsBottom.Add(summaryRowItem)
                ControlEnableDisable(False)
                ReStoreGridLayout()
                View()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub GetReportID()
        Gv1.VarID = MyBase.Form_ID
    End Sub
    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
        Next

        Gv1.Columns("Route_Code").HeaderText = "Route"

        Gv1.Columns("TankerQty").HeaderText = "Qty"
        Gv1.Columns("TankerQty").FormatString = "{0:n2}"
        Gv1.Columns("TankerFATKg").HeaderText = "FAT KG"
        Gv1.Columns("TankerFATKg").FormatString = "{0:n3}"
        Gv1.Columns("TankerSNFKg").HeaderText = "SNF KG"
        Gv1.Columns("TankerSNFKg").FormatString = "{0:n3}"

        Gv1.Columns("BMCQty").HeaderText = "Qty"
        Gv1.Columns("BMCQty").FormatString = "{0:n2}"
        Gv1.Columns("BMCFATKg").HeaderText = "FAT KG"
        Gv1.Columns("BMCFATKg").FormatString = "{0:n3}"
        Gv1.Columns("BMCSNFKg").HeaderText = "SNF KG"
        Gv1.Columns("BMCSNFKg").FormatString = "{0:n3}"


        Gv1.Columns("DCSQty").HeaderText = "Qty"
        Gv1.Columns("DCSQty").FormatString = "{0:n2}"
        Gv1.Columns("DCSFATKg").HeaderText = "FAT KG"
        Gv1.Columns("DCSFATKg").FormatString = "{0:n3}"
        Gv1.Columns("DCSSNFKg").HeaderText = "SNF KG"
        Gv1.Columns("DCSSNFKg").FormatString = "{0:n3}"


        Gv1.Columns("TankerBMCQty").HeaderText = "Qty"
        Gv1.Columns("TankerBMCQty").FormatString = "{0:n2}"
        Gv1.Columns("TankerBMCFATKg").HeaderText = "FAT KG"
        Gv1.Columns("TankerBMCFATKg").FormatString = "{0:n3}"
        Gv1.Columns("TankerBMCSNFKg").HeaderText = "SNF KG"
        Gv1.Columns("TankerBMCSNFKg").FormatString = "{0:n3}"


        Gv1.Columns("BMCDCSQty").HeaderText = "Qty"
        Gv1.Columns("BMCDCSQty").FormatString = "{0:n2}"
        Gv1.Columns("BMCDCSFATKg").HeaderText = "FAT KG"
        Gv1.Columns("BMCDCSFATKg").FormatString = "{0:n3}"
        Gv1.Columns("BMCDCSSNFKg").HeaderText = "SNF KG"
        Gv1.Columns("BMCDCSSNFKg").FormatString = "{0:n3}"


        Gv1.Columns("TankerDCSQty").HeaderText = "Qty"
        Gv1.Columns("TankerDCSQty").FormatString = "{0:n2}"
        Gv1.Columns("TankerDCSFATKg").HeaderText = "FAT KG"
        Gv1.Columns("TankerDCSFATKg").FormatString = "{0:n3}"
        Gv1.Columns("TankerDCSSNFKg").HeaderText = "SNF KG"
        Gv1.Columns("TankerDCSSNFKg").FormatString = "{0:n3}"
    End Sub
    Sub View()
        If Gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup(" "))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Route_Code").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Tanker Data(A)"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("TankerQty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("TankerFATKg").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("TankerSNFKg").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("BMC Data(B)"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("BMCQty").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("BMCFATKg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("BMCSNFKg").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("DCS Data(C)"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCSQty").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCSFATKg").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCSSNFKg").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Tanker-BMC(A-B)"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("TankerBMCQty").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("TankerBMCFATKg").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("TankerBMCSNFKg").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("BMC-DCS(B-C)"))
            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("BMCDCSQty").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("BMCDCSFATKg").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("BMCDCSSNFKg").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Tanker-DCS(A-C)"))
            view.ColumnGroups(6).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(Gv1.Columns("TankerDCSQty").Name)
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(Gv1.Columns("TankerDCSFATKg").Name)
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(Gv1.Columns("TankerDCSSNFKg").Name)


            Gv1.ViewDefinition = view
        End If
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
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
            common.clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(objCommonVar.CurrentCompanyName)
                arrHeader.Add("BMC SECTION")
                arrHeader.Add("COMPARE VOUCHER REPORT")

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.exportdata(Gv1, "", Me.Text, False, arrHeader, False, False, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Date : " & clsCommon.GetPrintDate(txtFromDate.Text, "dd/MM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Text, "dd/MM/yyyy"))
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Daily Quantity Report", Gv1, arrHeader, "", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
    End Sub
End Class

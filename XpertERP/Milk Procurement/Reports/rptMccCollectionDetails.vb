Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
' Ticket no : BHA/15/02/19-000816 By Prabhakar
' Ticket No : MIL/26/02/19-000048 By Prabhakar 
Public Class rptMccCollectionDetails
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        LoadShiftName()
        txtMCC.arrValueMember = Nothing
        ControlEnableDisable(True)
    End Sub
    Sub ControlEnableDisable(ByVal isEnable As Boolean)
        txtMCC.Enabled = isEnable
        fromDate.Enabled = isEnable
        ToDate.Enabled = isEnable
        cboShift.Enabled = isEnable
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try

            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim BaseQry As String = ""
            Dim qry As String = ""
            Dim dt As New DataTable
            Dim whre As String = ""

            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                whre = " and TSPL_MILK_SRN_HEAD.MCC_CODE in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
            End If
            If clsCommon.CompairString(cboShift.SelectedValue, "M") = CompairStringResult.Equal Then
                whre += " and shift in ('M')"
            End If
            If clsCommon.CompairString(cboShift.SelectedValue, "E") = CompairStringResult.Equal Then
                whre += " and shift in ('E')"
            End If
            qry = " select final.MCC_CODE as [MCC CODE] , final.MCC_NAME as [MCC NAME],final.DOC_DATE as [DOC DATE],final.SHIFT, final.FATPER as [FAT(%)],final.SNFPER as [SNF(%)],final .Amount,final.VSP_Count as [No of VSP]  from (  " & _
                      " select ROW_NUMBER () over (order by tspl_milk_srn_Head.mcc_code,convert(varchar,DOC_DATE,103),shift desc ) as SNO, shift, max(State_Code) as State_Code,max(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader) as Mcc_Code_VLC_Uploader,TSPL_MILK_SRN_HEAD.MCC_CODE,max(MCC_NAME) as MCC_NAME, convert(varchar,DOC_DATE,103) as DOC_DATE,cast(round(sum(Qty),2,2) as float) as Qty,max(TSPL_Mcc_UOM_DETAIL.UOM_Code) as UOM_Code,count(distinct(TSPL_MILK_SRN_HEAD.ROUTE_CODE)) as Route_Count,count(distinct(TSPL_MILK_SRN_HEAD.VLC_CODE)) as VLC_Count,count(distinct(TSPL_MILK_SRN_HEAD.VSP_CODE)) as VSP_Count,convert(decimal(18,2), case when sum(Qty)>0 then sum (FAT_KG)*100/sum(Qty) else 0 end) as FATPER,convert(decimal(18,2), case when sum(Qty)>0 then sum(SNF_KG)*100/sum(Qty) else 0 end) as SNFPER,convert(decimal(18,2), case when sum(Qty)>0 then sum(Amount)/sum(Qty) else 0 end) as Rate,convert(decimal(18,2), sum(Amount) ) as Amount from TSPL_MILK_SRN_DETAIL " & _
                      " inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE " & _
                      " left join TSPL_Mcc_UOM_DETAIL on TSPL_Mcc_UOM_DETAIL.MCC_CODE=TSPL_MILK_SRN_HEAD.MCC_CODE and Stocking_Unit='Y' " & _
                      " left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_SRN_HEAD.MCC_CODE " & _
                      " where  2=2  " + whre + " " & _
                      " and  convert(date,DOC_DATE,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' AND convert(date,DOC_DATE,103) <='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "'  " & _
                      " group by tspl_milk_srn_Head.mcc_code,convert(varchar,doc_date,103),shift " & _
                      " ) final order by SNO  asc "

            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim Amount As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(Amount)
                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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
            common.clsCommon.MyMessageBoxShow(err.Message)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMccCollectionDetails & "'"))
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMccCollectionDetails & "'"))

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub rptMccCollectionDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        Reset()
    End Sub
    Sub LoadShiftName()
        cboShift.DataSource = LoadShift()
        cboShift.ValueMember = "Code"
        cboShift.DisplayMember = "Name"
        cboShift.SelectedIndex = 0
    End Sub
    Public Function LoadShift() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "All"
        dr("Name") = "ALL"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)
        Return dt
    End Function

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = " select TSPL_MCC_MASTER.MCC_Code as Code , TSPL_MCC_MASTER.MCC_NAME as Name from TSPL_MCC_MASTER "
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSelMCC@MCCCollectionRPT", qry, "Code", "Code", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub
End Class

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'created by preeti gupta ticket no.[BM00000004212,BM00000004458]

Public Class RptDailyProgressReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Dim arrLoc As String = Nothing

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
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptDailyProgressReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvReport.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvReport.Columns.Count - 1 Step ii + 1
                        gvReport.Columns(ii).IsVisible = False
                        gvReport.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvReport.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Load_Report()
        Load_Report()
    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub butnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butnClose.Click
        Me.Close()
    End Sub



    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvReport.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvReport.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvReport.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RptDailyProgressReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(butnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")

        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+R ")
        RadPageView1.SelectedPage = RadPageViewPage3
        txttoDatee.Value = clsCommon.GETSERVERDATE()
        txtfromDatee.Value = txttoDatee.Value.AddMonths(-1)

        Reset()
    End Sub
    Sub LoadMCC()
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = "select MCC_Code as [Code] ,MCC_NAME as [Name] from TSPL_MCC_MASTER where MCC_Code in (" + arrLoc + ") "
            dt = clsDBFuncationality.GetDataTable(qry)
        End If

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            btnGo.Enabled = False
        Else
            cbgMCC.DataSource = dt
            cbgMCC.ValueMember = "Code"
            cbgMCC.DisplayMember = "Name"
        End If

    End Sub

    Sub LoadState()
        Dim qry As String = " select STATE_CODE as 'Code' ,STATE_NAME as 'Name' from TSPL_STATE_MASTER  "
        cbgState.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgState.ValueMember = "Code"
        cbgState.DisplayMember = "Name"

    End Sub

    Private Sub chkstateall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkstateall.ToggleStateChanged
        cbgState.Enabled = Not chkstateall.IsChecked
    End Sub

    Private Sub chkMccAlll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMccAlll.ToggleStateChanged
        cbgMCCC.Enabled = Not chkMccAlll.IsChecked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
    End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        txttoDatee.Value = clsCommon.GETSERVERDATE()
        txtfromDatee.Value = txttoDatee.Value.AddMonths(-1)
        LoadMCC()
        LoadState()
        cboUnitt.Text = "Kg"
        chkMccAlll.CheckState = CheckState.Checked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
        chkstateall.CheckState = CheckState.Checked

        gvReport.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage3
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtfromDatee.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txttoDatee.Value, "dd/MM/yyyy")) + " ")
            If chkMCCSelect.IsChecked Then
                Dim strMCCName As String = ""
                For Each StrName As String In cbgMCC.CheckedDisplayMember
                    If clsCommon.myLen(strMCCName) > 0 Then
                        strMCCName += ", "
                    End If
                    strMCCName += StrName
                Next
                Dim strMCCCode As String = ""
                For Each StrCode As String In cbgMCC.CheckedValue
                    If clsCommon.myLen(strMCCCode) > 0 Then
                        strMCCCode += ", "
                    End If
                    strMCCCode += StrCode
                Next
                arrHeader.Add((" MCC Name: " + strMCCName + " "))
            End If
            If chkStateSelect.IsChecked Then
                Dim strStateName As String = ""
                For Each StrName As String In cbgState.CheckedDisplayMember
                    If clsCommon.myLen(strStateName) > 0 Then
                        strStateName += ", "
                    End If
                    strStateName += StrName
                Next
                Dim strStateCode As String = ""
                For Each StrCode As String In cbgState.CheckedValue
                    If clsCommon.myLen(strStateCode) > 0 Then
                        strStateCode += ", "
                    End If
                    strStateCode += StrCode
                Next
                arrHeader.Add(("State Name: " + strStateName + " "))
            End If
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Daily Progress Report", gvReport, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Daily Progress Report", gvReport, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        print(EnumExportTo.Excel)
    End Sub
    Public Sub Load_Report()
        If txtfromDatee.Value > txttoDatee.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtfromDatee.Focus()
            Exit Sub
        End If

        If cboUnitt.Text = "" Then
            clsCommon.MyMessageBoxShow(Me, "Please select Unit", Me.Text)
        End If
        If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single MCC or select all.", Me.Text)
            Exit Sub
        End If
        If chkVlCSelect.IsChecked AndAlso cbgVLC.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single VLC or select all.", Me.Text)
            Exit Sub
        End If

        'Dim squery As String = "select DOC_DATE,VLC,QTY,[FAT %] ,[SNF %] ,([FAT %] *QTY /100) as [FAT in kg],([SNF %] *QTY /100) as [SNF in kg] from (select DOC_DATE,MAX(TOUOM ) as UOM,VLC_CODE as VLC,sum(FATQTY)/sum(NewQty)*100 as [FAT %], sum(SNFQTY )/sum(NewQty)*100 as [SNF %],SUM(isnull(NewQty,0) ) as QTY from("
        'squery += " select DOC_DATE,UOM_Code,VLC_CODE,FATQTY*CF as FATQTY,SNF*CF as SNFQTY,Qty*CF as NewQty, Qty,FromUOM,TOUOM,CF   from("

        'squery += " select TSPL_MILK_RECEIPT_HEAD.DOC_DATE,TSPL_MILK_RECEIPT_DETAIL.UOM_Code ,TSPL_MILK_RECEIPT_DETAIL.VLC_CODE ,TSPL_MILK_SAMPLE_DETAIL.FAT,(TSPL_MILK_SAMPLE_DETAIL.FAT*TSPL_MILK_SAMPLE_DETAIL.Qty/100) as FATQTY ,TSPL_MILK_SAMPLE_DETAIL.SNF,(TSPL_MILK_SAMPLE_DETAIL.SNF*TSPL_MILK_SAMPLE_DETAIL.Qty /100) as SNFQTY ,TSPL_MILK_SAMPLE_DETAIL.Qty  from TSPL_MILK_RECEIPT_DETAIL left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_RECEIPT_DETAIL.DOC_CODE"
        'squery += " left outer join TSPL_MCC_MASTER  on TSPL_MCC_MASTER .MCC_Code =TSPL_MILK_RECEIPT_HEAD .MCC_CODE  left outer join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE =TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE "
        'squery += "    where 2 = 2"
        'squery += " and convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)"

        'If chkMCCSelect.IsChecked And cbgMCC.CheckedValue.Count > 0 Then
        '    squery += "and TSPL_MILK_RECEIPT_DETAIL.MCC_CODE  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        'End If
        'If chkVlCSelect.IsChecked And cbgvreportLC.CheckedValue.Count > 0 Then
        '    squery += "and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE  IN (" + clsCommon.GetMulcallString(cbgvreportLC.CheckedValue) + ") "
        'End If

        'squery += " ) xx "
        'squery += "  left outer join (Select Distinct yyy.* From ( "
        'squery += " Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION"
        'squery += " UNION All"
        'squery += " Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION"
        'squery += " UNION All "
        'squery += "  Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION"
        'squery += " UNION All"
        'squery += " Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION"
        'squery += "  ) yyy) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='" + cboUnit.Text + "'  ) ttt group by DOC_DATE,VLC_CODE ) ff"


        Dim squery As String = " select max(MCC_Code) as MCC_Code, DOC_DATE,max(STATE_NAME ) as STATE_NAME,max(City_Name) as City_Name,MAX(TOUOM ) as UOM,convert(decimal(18,1),sum(FATQTY)/sum(NewQty)*100) as [FAT%], convert(decimal(18,1),sum(SNFQTY )/sum(NewQty)*100) as [SNF %],convert(Decimal(18,2),SUM(isnull(NewQty,0) )) as QTY from (select MCC_Code +' -'+MCC_NAME  as MCC_Code, DOC_DATE,STATE_NAME ,City_Name  ,UOM_Code,FATQTY*CF as FATQTY,SNFQTY*CF as SNFQTY,Qty*CF as NewQty, Qty,FromUOM,TOUOM,CF from (select TSPL_MCC_MASTER.MCC_CODE,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_RECEIPT_HEAD.DOC_DATE ,TSPL_STATE_MASTER.STATE_NAME,TSPL_STATE_MASTER .STATE_CODE   ,TSPL_CITY_MASTER.City_Name,TSPL_MILK_RECEIPT_DETAIL.UOM_Code ,TSPL_MILK_RECEIPT_DETAIL.VLC_CODE ,TSPL_MILK_SAMPLE_DETAIL.FAT,(TSPL_MILK_SAMPLE_DETAIL.FAT*TSPL_MILK_SAMPLE_DETAIL.Qty/100) as FATQTY ,TSPL_MILK_SAMPLE_DETAIL.SNF,(TSPL_MILK_SAMPLE_DETAIL.SNF*TSPL_MILK_SAMPLE_DETAIL.Qty /100) as SNFQTY ,TSPL_MILK_SAMPLE_DETAIL.Qty   from TSPL_MILK_SAMPLE_DETAIL "
        squery += "  left outer join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_DETAIL .DOC_CODE =TSPL_MILK_SAMPLE_HEAD .DOC_CODE"
        squery += " left outer join TSPL_MILK_RECEIPT_HEAD  on TSPL_MILK_RECEIPT_HEAD.DOC_CODE  =TSPL_MILK_SAMPLE_HEAD .MILK_RECEIPT_CODE"
        squery += " left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE   =TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE "
        squery += " left outer join TSPL_MCC_MASTER  on TSPL_MCC_MASTER .MCC_Code =TSPL_MILK_SAMPLE_HEAD.MCC_CODE  "
        squery += " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code =TSPL_MCC_MASTER .State_Code "
        squery += " left outer join TSPL_CITY_MASTER  on TSPL_CITY_MASTER.City_Code =TSPL_MCC_MASTER.City_code where 2=2"
        squery += " and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE,103)>=convert(date,'" + txtfromDatee.Value + "',103) and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE,103) <=convert(date,'" + txttoDatee.Value + "' ,103) "

        If cbgMCCC.CheckedValue.Count > 0 Then 'chkMccSelectt.IsChecked And
            squery += "and TSPL_MILK_RECEIPT_DETAIL.MCC_CODE  IN (" + clsCommon.GetMulcallString(cbgMCCC.CheckedValue) + ") "
        End If
        If chkStateSelect.IsChecked And cbgState.CheckedValue.Count > 0 Then
            squery += "and TSPL_STATE_MASTER.State_Code  IN (" + clsCommon.GetMulcallString(cbgState.CheckedValue) + ") "
        End If

        squery += " )xx"
        squery += " left outer join (Select Distinct yyy.* From ( "
        squery += " Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION"
        squery += " UNION All"
        squery += " Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION"
        squery += " UNION All"
        squery += " Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION"
        squery += " UNION All"
        squery += " Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION"
        squery += " ) yyy) zzz on zzz.FromUOM =UOM_Code  and zzz.TOUOM='" + cboUnitt.Text + "'  ) ttt group by DOC_DATE"

        Dim dtgvreport As New DataTable
        dtgvreport = clsDBFuncationality.GetDataTable(squery)
        If dtgvreport IsNot Nothing And dtgvreport.Rows.Count > 0 Then
            gvReport.DataSource = Nothing
            gvReport.Rows.Clear()
            gvReport.Columns.Clear()
            gvReport.DataSource = dtgvreport
            'For i As Integer = 0 To gvreport.Rows.Count - 1
            '    gvreport.Rows(i).Cells(0).Value = i + 1
            'Next
            gvReport.GroupDescriptors.Clear()
            gvReport.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            RadPageView1.SelectedPage = RadPageViewPage4
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If

        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()
        '  Dim strItemCode, head2 As String

        gvReport.TableElement.TableHeaderHeight = 20
        gvReport.MasterTemplate.ShowRowHeaderColumn = False
        'For ii As Integer = 0 To gvReport.Columns.Count - 1
        '    gvReport.Columns(ii).ReadOnly = True
        '    gvReport.Columns(ii).IsVisible = False
        'Next

        gvReport.Columns("DOC_DATE").IsVisible = True
        gvReport.Columns("DOC_DATE").Width = 100
        gvReport.Columns("DOC_DATE").HeaderText = " Date"
        gvReport.Columns("DOC_DATE").FormatString = "{0:d}"

        gvReport.Columns("Qty").IsVisible = True
        gvReport.Columns("Qty").Width = 100
        gvReport.Columns("Qty").HeaderText = "Qty"


        gvReport.Columns("FAT%").IsVisible = True
        gvReport.Columns("FAT%").Width = 100
        gvReport.Columns("FAT%").HeaderText = "FAT%"


        gvReport.Columns("SNF %").IsVisible = True
        gvReport.Columns("SNF %").Width = 100
        gvReport.Columns("SNF %").HeaderText = "SNF %"


        gvReport.Columns("STATE_NAME").IsVisible = False
        gvReport.Columns("STATE_NAME").Width = 100
        gvReport.Columns("STATE_NAME").HeaderText = "State Name"

        gvReport.Columns("City_Name").IsVisible = False
        gvReport.Columns("City_Name").Width = 100
        gvReport.Columns("City_Name").HeaderText = "City Name"

        gvReport.Columns("MCC_Code").IsVisible = False
        gvReport.Columns("MCC_Code").Width = 100
        gvReport.Columns("MCC_Code").HeaderText = "MCC Code"


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("QTY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        'Dim item2 As New GridViewSummaryItem("Av.Per Shift", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)
        'Dim item3 As New GridViewSummaryItem("Av.Per Day", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)

        gvReport.GroupDescriptors.Add(New GridGroupByExpression("STATE_NAME as Item format ""{0}: {1}"" Group By STATE_NAME"))
        gvReport.GroupDescriptors.Add(New GridGroupByExpression("City_Name as Item format ""{0}: {1}"" Group By City_Name"))
        gvReport.GroupDescriptors.Add(New GridGroupByExpression("MCC_Code as Item format ""{0}: {1}"" Group By MCC_Code"))

        gvReport.ShowGroupPanel = False
        gvReport.MasterTemplate.AutoExpandGroups = True

        gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)



    End Sub

    Private Sub RptDailyProgressReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub RadSplitButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadSplitButton4.Click

    End Sub
End Class

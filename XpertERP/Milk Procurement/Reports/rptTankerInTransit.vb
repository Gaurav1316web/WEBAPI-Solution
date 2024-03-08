Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'===============create new report ==against Ticket No[KDI/03/05/18-000293]

Public Class RptTankerInTransit
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
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
        MyBase.SetUserMgmt(clsUserMgtCode.RptTankerInTransit)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            End If
        End If
        btnExp.Visible = MyBase.isExport
    End Sub

    Private Sub LoadData(Optional ByVal BulkExport As Integer = 0)
        Try
            If txtFromDate.Value > txtToDate.Value Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If
            Dim qry As String = Nothing
            'Ticket No-ERO/20/08/19-000996,Sanjay , add Status
            '' done by richa agarwal ERO/25/04/18-000104
            '==========update by preeti gupta [ERO/17/05/18-000314]==========
            'If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, Nothing)) = 1, True, False)) = True Then
            qry = "Select TSPL_MCC_MASTER.MCC_NAME As [Issued By], TSPL_LOCATION_MASTER.Location_Desc As [Dispatch To Plant], " & _
            " XX.Dispatch_Date As Date, Convert(varchar,XX.Dispatch_Date,103) As [Dispatch Date], Convert(varchar,XX.Dispatch_Date,108) As [Dispatch Time], " & _
            "  XX.Chalan_NO As [Doc No.], XX.Tanker_No As [Tranker No.], TSPL_VENDOR_MASTER.Vendor_Name As [Transporter Name], XX.ChamberQty As [Dispatch Qty]," & _
            " XX.FAT As [FAT %], XX.SNF As [SNF %], XX.CLR, TSPL_MCC_MASTER.MCC_NAME As MCC, TSPL_LOCATION_MASTER.Location_Desc As [Dispatch To], " & _
            " XX.MCC_Code As [MCC Code], XX.Mcc_Or_Plant_Code As Dispatch ,case when isnull(XX.isPosted,0)=0 then 'Pending' else 'Approved' end as [Status] From (Select TSPL_MCC_Dispatch_Challan.*,case when len(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Qty_KG)>0 then TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Qty_KG else TSPL_MCC_Dispatch_Challan.Net_Qty end as ChamberQty, t_FAT.Param_Field_Value As FAT," & _
            "  t_SNf.Param_Field_Value As SNF, t_Clr.Param_Field_Value As CLR    From TSPL_MCC_Dispatch_Challan " & _
            " LEFT OUTER JOIN TSPL_MCC_DISPATCH_CHALLAN_DETAIL ON TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No =TSPL_MCC_Dispatch_Challan.Chalan_NO  " & _
            " Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail t_FAT On t_FAT.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO and t_FAT.Param_Type = 'FAT' AND isnull(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,0) =isnull(t_FAT .SNO,0)  " & _
            " Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail t_SNf On t_SNf.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO and t_SNf.Param_Type = 'SNF' AND isnull(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,0) =isnull(t_SNf .SNO,0)  " & _
            " Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail t_Clr On t_Clr.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO and t_Clr.Param_Type = 'CLR' AND isnull(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,0) =isnull(t_Clr .SNO,0) " & _
            " Left Outer Join Tspl_Gate_Entry_Details On Tspl_Gate_Entry_Details.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO    Where IsNull(Tspl_Gate_Entry_Details.Challan_No, '') = '') XX Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = XX.Mcc_Or_Plant_Code Left Outer Join TSPL_TANKER_MASTER On TSPL_TANKER_MASTER.Tanker_No = XX.Tanker_No Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = XX.MCC_Code Left Outer Join TSPL_VENDOR_MASTER On TSPL_TANKER_MASTER.Tanker_Transporter_Code = TSPL_VENDOR_MASTER.Vendor_Code" & _
            " where xx.Chalan_NO not in (select Challan_No from TSPL_MCC_DISPATCH_CHALLAN_RETURN) and xx.Chalan_NO " & _
            " not in (select Chalan_No from TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD)"
            'Else
            'qry = "Select TSPL_MCC_MASTER.MCC_NAME As [Issued By], TSPL_LOCATION_MASTER.Location_Desc As [Dispatch To Plant], " & _
            '" XX.Dispatch_Date As Date, Convert(varchar,XX.Dispatch_Date,103) As [Dispatch Date], Convert(varchar,XX.Dispatch_Date,108) As [Dispatch Time], " & _
            '"  XX.Chalan_NO As [Doc No.], XX.Tanker_No As [Tranker No.], TSPL_VENDOR_MASTER.Vendor_Name As [Transporter Name], XX.Net_Qty As [Dispatch Qty]," & _
            '" XX.FAT As [FAT %], XX.SNF As [SNF %], XX.CLR, TSPL_MCC_MASTER.MCC_NAME As MCC, TSPL_LOCATION_MASTER.Location_Desc As [Dispatch To], " & _
            '" XX.MCC_Code As [MCC Code], XX.Mcc_Or_Plant_Code As Dispatch From (Select TSPL_MCC_Dispatch_Challan.*, t_FAT.Param_Field_Value As FAT," & _
            '"  t_SNf.Param_Field_Value As SNF, t_Clr.Param_Field_Value As CLR    From TSPL_MCC_Dispatch_Challan" & _
            '"   Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*      From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO And TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO " & _
            '" Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*, TSPL_MCC_Dispatch_Challan.MCC_Code      From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO And TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'SNF') t_SNf On t_SNf.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO " & _
            '" Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*      From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO And TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'CLR') t_Clr On t_Clr.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO " & _
            '" Left Outer Join Tspl_Gate_Entry_Details On Tspl_Gate_Entry_Details.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO    Where IsNull(Tspl_Gate_Entry_Details.Challan_No, '') = '') XX Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = XX.Mcc_Or_Plant_Code Left Outer Join TSPL_TANKER_MASTER On TSPL_TANKER_MASTER.Tanker_No = XX.Tanker_No Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = XX.MCC_Code Left Outer Join TSPL_VENDOR_MASTER On TSPL_TANKER_MASTER.Tanker_Transporter_Code = TSPL_VENDOR_MASTER.Vendor_Code" & _
            '"   where xx.Chalan_NO not in (select Challan_No from TSPL_MCC_DISPATCH_CHALLAN_RETURN) and xx.Chalan_NO " & _
            '" not in (select Chalan_No from TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD)"
            'End If

            qry += " AND convert(date,XX.Dispatch_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,XX.Dispatch_Date,103)<=convert(date,'" + txtToDate.Value + "',103)"

            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += " and   XX.MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            End If
            If txtDispatch.arrValueMember IsNot Nothing AndAlso txtDispatch.arrValueMember.Count > 0 Then
                qry += " and XX.Mcc_Or_Plant_Code in (" + clsCommon.GetMulcallString(txtDispatch.arrValueMember) + ")"
            End If


            qry += "  ORDER BY XX.Dispatch_Date "
            dt = clsDBFuncationality.GetDataTable(qry)
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            RadPageView1.SelectedPage = RadPageViewPage2
            gv.BestFitColumns()
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Function GetReportID() As String
        Dim ReportID As String = "TANKINTRANSIT"
        Return ReportID
    End Function
    Private Sub ReStoreGridLayout()
        Try
            Dim ReportID As String = GetReportID()
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add(CompName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptTankerInTransit & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")


                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember) + " "))
                End If
                If txtDispatch.arrValueMember IsNot Nothing AndAlso txtDispatch.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Dispatch : " + clsCommon.GetMulcallStringWithComma(txtDispatch.arrDispalyMember) + " "))
                End If


                If exporter = EnumExportTo.Excel Then
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
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Tanker In Transit", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Sub FormatGrid()
        ' Dim strItemCode, head2 As String
        Dim summaryItem As New GridViewSummaryItem()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True
            'gv.Columns(ii).FormatString = "{0:n2}"
        Next

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("Dispatch Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub Reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        gv.DataSource = Nothing
        txtMCC.arrValueMember = Nothing
        txtDispatch.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub


    Private Sub txtDispatch__My_Click(sender As Object, e As EventArgs) Handles txtDispatch._My_Click
        If clsCommon.myLen(arrLoc) > 0 Then
            Dim qry As String = "Select TSPL_LOCATION_MASTER.Location_Code As Code,  TSPL_LOCATION_MASTER.Location_Desc As Name From TSPL_LOCATION_MASTER"
            txtDispatch.arrValueMember = clsCommon.ShowMultipleSelectForm("MCCDISP", qry, "Code", "Name", txtDispatch.arrValueMember, txtDispatch.arrDispalyMember)
        End If
      
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME from TSPL_MCC_MASTER where TSPL_MCC_MASTER.MCC_Code in (" + arrLoc + ")"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub

    Private Sub RptTankerInTransit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        LOCATIONRIGTHS()
        SetUserMgmtNew()
        Reset()
    End Sub

    Private Sub RptTankerInTransit_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = GetReportID()
        TemplateGridview = gv
        LoadData()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(GetReportID()) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = GetReportID()
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

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(GetReportID(), objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub
End Class

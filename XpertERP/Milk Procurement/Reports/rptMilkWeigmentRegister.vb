Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms

Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common

Public Class RptMilkWeigmentRegister
    Inherits FrmMainTranScreen
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Private Sub RptMilkWeigmentRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
    End Sub

    Sub Reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtParty.arrValueMember = Nothing
        txtSubSupplier.arrValueMember = Nothing
        txtGateEntryType.arrValueMember = Nothing
        txtMilkType.arrValueMember = Nothing
        gv.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        LoadData()
    End Sub
    Sub LoadData()
        Try
            If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithEndTime(txtToDate.Value) Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            Dim qry As String = ""
            Dim fromdate As String = ""
            Dim Todate As String = ""
            Dim whr As String = ""

            fromdate = clsCommon.myCDate(txtFromDate.Value, "dd/MM/yyyy")
            Todate = clsCommon.myCDate(txtToDate.Value, "dd/MM/yyyy")
            If txtParty.arrValueMember IsNot Nothing AndAlso txtParty.arrValueMember.Count > 0 Then
                whr = " and tspl_gate_entry_details.Vendor_Code in ( " + clsCommon.GetMulcallString(txtParty.arrValueMember) + ")"
            End If

            If txtSubSupplier.arrValueMember IsNot Nothing AndAlso txtSubSupplier.arrValueMember.Count > 0 Then
                whr = whr + " and tspl_gate_entry_details.Supplier_Code in ( " + clsCommon.GetMulcallString(txtSubSupplier.arrValueMember) + ")"
            End If

            If txtGateEntryType.arrValueMember IsNot Nothing AndAlso txtGateEntryType.arrValueMember.Count > 0 Then
                whr = whr + " and tspl_gate_entry_details.Gate_Entry_Type in ( " + clsCommon.GetMulcallString(txtGateEntryType.arrValueMember) + ")"
            End If

            If txtMilkType.arrValueMember IsNot Nothing AndAlso txtMilkType.arrValueMember.Count > 0 Then
                whr = whr + " and tspl_gate_entry_details.MIKL_TYPE_CODE in ( " + clsCommon.GetMulcallString(txtMilkType.arrValueMember) + ")"
            End If
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                whr = whr + " and tspl_gate_entry_details.Dispatched_From_Mcc in ( " + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            End If


            'qry = "  select tspl_gate_entry_details.Doc_Type as [Document Type], case when tspl_gate_entry_details.Gate_Entry_Type='P' then 'Purchase' when tspl_gate_entry_details.Gate_Entry_Type='J' then 'Job Work' end as [Gate Entry Type] ,tspl_gate_entry_details.Gate_Entry_No as [Gate Entry No], convert (varchar, FORMAT (tspl_gate_entry_details.date_and_time,'dd/MM/yyyy HH:mm:ss' )) as [Gate Entry Date and time],tspl_gate_entry_details.Vendor_Code as [Party Code], tspl_gate_entry_details.Vendor_Desc as [Party Name],tspl_gate_entry_details.Supplier_Code  as [Sub Supplier Code],TSPL_SUPPLIER_MASTER.DESCRIPTION as [Sub Supplier Name],tspl_gate_entry_details.Dispatched_From_Mcc as MCC_Code, TSPL_Location_MASTER.Location_Desc as MCC_Name, tspl_gate_entry_details.MIKL_TYPE_CODE as [Milk Type Code],tspl_weighment_detail_Temp.Weighment_No as [Weiggment Entry Number],convert (varchar, FORMAT (tspl_weighment_detail_Temp.Weighment_date,'dd/MM/yyyy HH:mm:ss' )) as [Weighment Date and Time],tspl_weighment_detail_Temp.Gross_Weight as [Gross Weigment],tspl_weighment_detail_Temp.Tare_Weight as [Tare Weight],tspl_weighment_detail_Temp.Net_Weight as [Net Weight]  from tspl_gate_entry_details " & _
            '      " left outer join (select * from tspl_weighment_detail where isPosted=1) as tspl_weighment_detail_Temp on tspl_gate_entry_details.Gate_Entry_No = tspl_weighment_detail_Temp.Gate_Entry_No " & _
            '      " left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.Gate_Entry_No= tspl_gate_entry_details.Gate_Entry_No " & _
            '      " left outer join TSPL_SUPPLIER_MASTER on TSPL_SUPPLIER_MASTER.SUPPLIER_CODE =tspl_gate_entry_details.Supplier_Code " & _
            '      " left outer join TSPL_Location_MASTER  on TSPL_Location_MASTER.Location_Code = tspl_gate_entry_details.Dispatched_From_Mcc " & _
            '      " where TSPL_QUALITY_CHECK.is_Param_Accepted='1'  and tspl_gate_entry_details.isPosted=1 " & _
            '      " and  Convert(date, tspl_gate_entry_details.date_and_time,103) >=convert (date, '" + fromdate + "',103) and    convert(date, tspl_gate_entry_details.date_and_time,103) <=convert (date, '" + Todate + "',103)  " + whr + " order by tspl_gate_entry_details.Gate_Entry_No"

            qry = "  select tspl_gate_entry_details.Doc_Type as [Document Type], case when tspl_gate_entry_details.Gate_Entry_Type='P' then 'Purchase' when tspl_gate_entry_details.Gate_Entry_Type='J' then 'Job Work' end as [Gate Entry Type] ,tspl_gate_entry_details.Gate_Entry_No as [Gate Entry No], convert (varchar, FORMAT (tspl_gate_entry_details.date_and_time,'dd/MM/yyyy HH:mm:ss' )) as [Gate Entry Date and time],tspl_gate_entry_details.Vendor_Code as [Party Code], tspl_gate_entry_details.Vendor_Desc as [Party Name],tspl_gate_entry_details.Supplier_Code  as [Sub Supplier Code],TSPL_SUPPLIER_MASTER.DESCRIPTION as [Sub Supplier Name],tspl_gate_entry_details.Dispatched_From_Mcc as MCC_Code, TSPL_Location_MASTER.Location_Desc as MCC_Name, tspl_gate_entry_details.MIKL_TYPE_CODE as [Milk Type Code],tspl_weighment_detail_Temp.Weighment_No as [Weiggment Entry Number],convert (varchar, FORMAT (tspl_weighment_detail_Temp.Weighment_date,'dd/MM/yyyy HH:mm:ss' )) as [Weighment Date and Time],tspl_weighment_detail_Temp.Gross_Weight as [Gross Weigment],tspl_weighment_detail_Temp.Tare_Weight as [Tare Weight],tspl_weighment_detail_Temp.Net_Weight as [Net Weight]  from tspl_gate_entry_details " & _
                 " left outer join (select tspl_weighment_detail.Gate_Entry_No,tspl_weighment_detail.Weighment_No,tspl_weighment_detail.Weighment_date,coalesce(TSPL_Weighment_Chember_Details.Gross_Weight,tspl_weighment_detail.Gross_Weight) as Gross_Weight,coalesce(TSPL_Weighment_Chember_Details.Tare_Weight,tspl_weighment_detail.Tare_Weight) as Tare_Weight,coalesce(TSPL_Weighment_Chember_Details.Net_Weight,tspl_weighment_detail.Net_Weight) as Net_Weight from tspl_weighment_detail left join TSPL_Weighment_Chember_Details on TSPL_Weighment_Chember_Details.Weighment_No=tspl_weighment_detail.Weighment_No where isPosted=1) " & _
                 " as tspl_weighment_detail_Temp on tspl_gate_entry_details.Gate_Entry_No = tspl_weighment_detail_Temp.Gate_Entry_No " & _
                 " left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.Gate_Entry_No= tspl_gate_entry_details.Gate_Entry_No " & _
                 " left outer join TSPL_SUPPLIER_MASTER on TSPL_SUPPLIER_MASTER.SUPPLIER_CODE =tspl_gate_entry_details.Supplier_Code " & _
                 " left outer join TSPL_Location_MASTER  on TSPL_Location_MASTER.Location_Code = tspl_gate_entry_details.Dispatched_From_Mcc " & _
                 " where TSPL_QUALITY_CHECK.is_Param_Accepted='1'  and tspl_gate_entry_details.isPosted=1 " & _
                 " and  Convert(date, tspl_gate_entry_details.date_and_time,103) >=convert (date, '" + fromdate + "',103) and    convert(date, tspl_gate_entry_details.date_and_time,103) <=convert (date, '" + Todate + "',103)  " + whr + " order by tspl_gate_entry_details.Gate_Entry_No"

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.BestFitColumns()
                RadPageView1.SelectedPage = RadPageViewPage2
                ReStoreGridLayout()
            End If
            If dtgv.Rows.Count <= 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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


    Private Sub txtParty__My_Click(sender As Object, e As EventArgs) Handles txtParty._My_Click
        Dim qry As String = " select Vendor_Code as Code, Vendor_Name as Name from TSPL_VENDOR_MASTER "
        txtParty.arrValueMember = clsCommon.ShowMultipleSelectForm("PARTY", qry, "Code", "Name", txtParty.arrValueMember, txtParty.arrDispalyMember)
    End Sub

    Private Sub txtSubSupplier__My_Click(sender As Object, e As EventArgs) Handles txtSubSupplier._My_Click
        Dim qry As String = " select SUPPLIER_CODE as Code, DESCRIPTION as Name from TSPL_SUPPLIER_MASTER "
        txtSubSupplier.arrValueMember = clsCommon.ShowMultipleSelectForm("SUPPL", qry, "Code", "Name", txtSubSupplier.arrValueMember, txtSubSupplier.arrDispalyMember)
    End Sub

    Private Sub txtGateEntryType__My_Click(sender As Object, e As EventArgs) Handles txtGateEntryType._My_Click
        Dim qry As String = " select distinct   Gate_Entry_Type as Code,case when tspl_gate_entry_details.Gate_Entry_Type='P' then 'Purchase' when tspl_gate_entry_details.Gate_Entry_Type='J' then 'Job Work' end as Name   from tspl_gate_entry_details where len( Gate_Entry_Type) >0 "
        txtGateEntryType.arrValueMember = clsCommon.ShowMultipleSelectForm("GATE_ENTRY_TYPE", qry, "Code", "Name", txtGateEntryType.arrValueMember, txtGateEntryType.arrDispalyMember)
    End Sub

    Private Sub txtMilkType__My_Click(sender As Object, e As EventArgs) Handles txtMilkType._My_Click
        Dim qry As String = " select TSPL_MILK_TYPE_MASTER.MILK_TYPE_CODE as Code ,TSPL_MILK_TYPE_MASTER.DESCRIPTION  as Name   From TSPL_MILK_TYPE_MASTER  "
        txtMilkType.arrValueMember = clsCommon.ShowMultipleSelectForm("GATE_ENTRY_TYPE", qry, "Code", "Name", txtMilkType.arrValueMember, txtMilkType.arrDispalyMember)
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try
            If gv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptMilkWeigmentRegister & "'"))

            If txtParty.arrValueMember IsNot Nothing AndAlso txtParty.arrValueMember.Count > 0 Then
                arrHeader.Add("Party : " + clsCommon.GetMulcallString(txtParty.arrDispalyMember))
            End If
            If txtSubSupplier.arrValueMember IsNot Nothing AndAlso txtSubSupplier.arrValueMember.Count > 0 Then
                arrHeader.Add("Sub Supplier : " + clsCommon.GetMulcallString(txtSubSupplier.arrDispalyMember))
            End If
            If txtGateEntryType.arrValueMember IsNot Nothing AndAlso txtGateEntryType.arrValueMember.Count > 0 Then
                arrHeader.Add("Gate Entry Type : " + clsCommon.GetMulcallString(txtGateEntryType.arrDispalyMember))
            End If
            If txtMilkType.arrValueMember IsNot Nothing AndAlso txtMilkType.arrValueMember.Count > 0 Then
                arrHeader.Add("Milk Type : " + clsCommon.GetMulcallString(txtMilkType.arrDispalyMember))
            End If
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                arrHeader.Add("MCC : " + clsCommon.GetMulcallString(txtMCC.arrDispalyMember))
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
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = " select * from (select Location_Code as Code,Location_Desc as Name from TSPL_Location_MASTER where Location_category='MCC') as  TSPL_Location_MASTER_Temp  "
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MCC", qry, "Code", "Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
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
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Try
            If gv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim CompName As String = "Company : " & objCommonVar.CurrentCompanyName
            arrHeader.Add(CompName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptMilkWeigmentRegister & "'"))

            If txtParty.arrValueMember IsNot Nothing AndAlso txtParty.arrValueMember.Count > 0 Then
                arrHeader.Add("Party : " + clsCommon.GetMulcallString(txtParty.arrDispalyMember))
            End If
            If txtSubSupplier.arrValueMember IsNot Nothing AndAlso txtSubSupplier.arrValueMember.Count > 0 Then
                arrHeader.Add("Sub Supplier : " + clsCommon.GetMulcallString(txtSubSupplier.arrDispalyMember))
            End If
            If txtGateEntryType.arrValueMember IsNot Nothing AndAlso txtGateEntryType.arrValueMember.Count > 0 Then
                arrHeader.Add("Gate Entry Type : " + clsCommon.GetMulcallString(txtGateEntryType.arrDispalyMember))
            End If
            If txtMilkType.arrValueMember IsNot Nothing AndAlso txtMilkType.arrValueMember.Count > 0 Then
                arrHeader.Add("Milk Type : " + clsCommon.GetMulcallString(txtMilkType.arrDispalyMember))
            End If
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                arrHeader.Add("MCC : " + clsCommon.GetMulcallString(txtMCC.arrDispalyMember))
            End If


            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            clsCommon.MyExportToPDF(Me.Text, gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class

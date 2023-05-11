Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmMCCWeightDiifferenceRpt
    Inherits FrmMainTranScreen
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmMccWeightDifferenceRpt)
        btnExp.Visible = MyBase.isExport
    End Sub

    Public Sub GetshiftType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)

        cboShift.DataSource = dt
        cboShift.ValueMember = "Code"
        ' cboShift.DisplayMember = "Name"
    End Sub
    Public Sub GetFromshiftType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)

        FromShift.DataSource = dt
        FromShift.ValueMember = "Code"
        ' cboShift.DisplayMember = "Name"
    End Sub


    Private Sub FrmMCCWeightDiifferenceRpt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rbtnDateWise_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnDateWise.CheckedChanged
        If rbtnDateWise.Checked = True Then
            lbltodate.Enabled = False
            dtDoctToDate.Enabled = False
            lbldifamt.Enabled = False
            txtDiffAmt.Enabled = False
            DtpDocfDate.Text = clsCommon.GETSERVERDATE
            lblShift.Enabled = False
            FromShift.Enabled = True
            cboShift.Enabled = False
        Else
            lblShift.Enabled = True
            cboShift.Enabled = True
            lbldifamt.Enabled = True
            txtDiffAmt.Enabled = True
            lbltodate.Enabled = True
            dtDoctToDate.Enabled = True
            DtpDocfDate.Text = clsCommon.GETSERVERDATE().AddMonths(-1)

        End If

    End Sub

    Private Sub txtMccCode__My_Click(sender As Object, e As EventArgs) Handles txtMccCode._My_Click
        Try
            Dim qry As String = "select Location_Code as Location,Location_Desc as Description  from TSPL_LOCATION_MASTER where Location_Category='MCC'"
            txtMccCode.arrValueMember = clsCommon.ShowMultipleSelectForm("LocatMast", qry, "Location", "Description", txtMccCode.arrValueMember, txtMccCode.arrDispalyMember)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtRouteCode__My_Click(sender As Object, e As EventArgs) Handles txtRouteCode._My_Click
        Try
             Dim qry As String=Nothing
            If txtMccCode.arrValueMember IsNot Nothing AndAlso txtMccCode.arrValueMember.Count > 0 Then
                qry = "select Distinct TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as Description from TSPL_MCC_ROUTE_MASTER  WHERE MCC_Code IN (" + clsCommon.GetMulcallString(txtMccCode.arrValueMember) + ")"
            Else
                qry = "select Distinct TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as Description from TSPL_MCC_ROUTE_MASTER "
            End If

            txtRouteCode.arrValueMember = clsCommon.ShowMultipleSelectForm("Routed", qry, "Code", "Description", txtRouteCode.arrValueMember, txtRouteCode.arrDispalyMember)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnok_Click(sender As Object, e As EventArgs) Handles btnok.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID + IIf(rbtnDateWise.Checked = True, "D", "DR")
            TemplateGridview = gv1
            Dim strQuery As String = Nothing
            Dim str11 As String = Nothing
            strQuery = "select max(Weighment_Code) as [Weighment Code],max(DOC_CODE) as [Milk Receipt Code],MAX(Vehicle_No) AS [Vehicle Code], MAX(Route_Code) AS [Route No] ,MAX(Entry_Shift) AS Shift,max(ISNULL(Net_Weight,0)) AS [Bulk Weight],SUM(ISNULL(MILK_WEIGHT,0)) AS [Actual Weight], ISNULL(max(Net_Weight),0)-SUM(ISNULL(MILK_WEIGHT,0)) AS [Difference in Ltrs] from ( " & _
                " select TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Weighment_Code,TSPL_MILK_GATE_ENTRY_IN.Route_Code  ,TSPL_MILK_GATE_ENTRY_IN.Entry_Shift ,TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Net_Weight ,TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT, TSPL_MILK_GATE_ENTRY_IN.MCC_CODE  ,TSPL_MILK_RECEIPT_DETAIL.DOC_DATE,TSPL_MILK_GATE_ENTRY_IN.Vehicle_No,TSPL_MILK_RECEIPT_DETAIL.DOC_CODE " & _
            " from TSPL_MILK_GATE_ENTRY_IN " & _
            " left outer join TSPL_MILK_GATE_ENTRY_WEIGHTMENT  ON TSPL_MILK_GATE_ENTRY_IN.Entry_Code=TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Against_Gate_Entry_No " & _
            " left outer JOIN TSPL_MILK_RECEIPT_DETAIL  ON   TSPL_MILK_RECEIPT_DETAIL.MCC_CODE =TSPL_MILK_GATE_ENTRY_IN.MCC_CODE and TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE =TSPL_MILK_GATE_ENTRY_IN.Route_Code AND  TSPL_MILK_RECEIPT_DETAIL.SHIFT =TSPL_MILK_GATE_ENTRY_IN.ENTRY_SHIFT " & _
             " and convert(date,TSPL_MILK_RECEIPT_DETAIL.DOC_DATE ,103)= convert(date,TSPL_MILK_GATE_ENTRY_IN.Entry_Date,103) "
            strQuery += " where 1=1 "
            If txtMccCode.arrValueMember IsNot Nothing AndAlso txtMccCode.arrValueMember.Count > 0 Then
                strQuery += " and TSPL_MILK_GATE_ENTRY_IN.MCC_CODE IN (" + clsCommon.GetMulcallString(txtMccCode.arrValueMember) + ")"
            End If
            If txtRouteCode.arrValueMember IsNot Nothing AndAlso txtRouteCode.arrValueMember.Count > 0 Then
                strQuery += " AND TSPL_MILK_GATE_ENTRY_IN.Route_Code IN(" + clsCommon.GetMulcallString(txtRouteCode.arrValueMember) + ") "
            End If
            'If clsCommon.myLen(cboShift.SelectedValue) > 0 Then
            '     strQuery += " and MGEI.Entry_Shift='" + clsCommon.myCstr(cboShift.SelectedValue) + "'"
            '    If clsCommon.CompairString(FromShift.Text, "E") = CompairStringResult.Equal Then
            '        strQuery += " and 2=( case when MRH.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(DtpDocfDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and MRH.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(DtpDocfDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and MRD.SHIFT='M' then 3 else 2 end  )"
            '    End If
            '    If clsCommon.CompairString(cboShift.Text, "M") = CompairStringResult.Equal Then
            '        strQuery += " and 2=( case when MRH.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtDoctToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and MRH.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDoctToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and MRD.SHIFT='E' then 3 else 2 end  )"
            '    End If
            'Else
            '    strQuery += " and MGEI.Entry_Shift=MGEI.Entry_Shift "
            'End If

            If rbtnDateWise.Checked = True Then

                strQuery += " and convert(date,TSPL_MILK_GATE_ENTRY_IN.Shift_Date,103)=convert(date,'" & clsCommon.myCDate(DtpDocfDate.Value) & "',103) and TSPL_MILK_GATE_ENTRY_IN.Entry_Shift='" + clsCommon.myCstr(FromShift.SelectedValue) + "' ) AS Final GROUP BY DOC_CODE,MCC_CODE,Route_Code,Vehicle_No "
            Else
                If clsCommon.CompairString(FromShift.Text, "E") = CompairStringResult.Equal Then
                    strQuery += " and 2=( case when TSPL_MILK_GATE_ENTRY_IN.Shift_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(DtpDocfDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_GATE_ENTRY_IN.Shift_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(DtpDocfDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_GATE_ENTRY_IN.Entry_Shift='M' then 3 else 2 end  )"
                End If
                If clsCommon.CompairString(cboShift.Text, "M") = CompairStringResult.Equal Then
                    strQuery += " and 2=( case when TSPL_MILK_GATE_ENTRY_IN.Shift_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtDoctToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_GATE_ENTRY_IN.Shift_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDoctToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_GATE_ENTRY_IN.Entry_Shift='E' then 3 else 2 end  )"
                End If
                'strQuery += " and convert(date,MGEI.Entry_Date,103)>=convert(date,'" & clsCommon.myCDate(DtpDocfDate.Value) & "',103) and convert(date,MGEI.Entry_Date,103)<=convert(date,'" & clsCommon.myCDate(dtDoctToDate.Value) & "',103) " & _
                strQuery += " and convert(date,TSPL_MILK_GATE_ENTRY_IN.Shift_Date,103)>=convert(date,'" & clsCommon.myCDate(DtpDocfDate.Value) & "',103) and convert(date,TSPL_MILK_GATE_ENTRY_IN.Shift_Date,103)<=convert(date,'" & clsCommon.myCDate(dtDoctToDate.Value) & "',103) " & _
                    " ) AS Final GROUP BY DOC_CODE,MCC_CODE,Route_Code,Vehicle_No "
                If clsCommon.myCdbl(txtDiffAmt.Value) > 0 Then
                    str11 = " having (max(ISNULL(Net_Weight,0))-SUM(MILK_WEIGHT)>= " + clsCommon.myCstr(txtDiffAmt.Value) + " OR max(ISNULL(Net_Weight,0))-SUM(MILK_WEIGHT) <= (0 - " + clsCommon.myCstr(txtDiffAmt.Value) + ")) "
                End If

            End If
            strQuery += str11
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()

            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.AllowAddNewRow = False
                gv1.ShowGroupPanel = False
                gv1.EnableFiltering = True
                gv1.ShowFilteringRow = True
                gv1.AllowColumnReorder = True
                gv1.AllowRowReorder = False
                gv1.EnableSorting = False
                gv1.ReadOnly = True
                gv1.BestFitColumns()
                gv1.MasterTemplate.ShowRowHeaderColumn = False
                gv1.TableElement.TableHeaderHeight = 40
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No Data Found..")
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmMccWeightDifferenceRpt & "'"))
                If rbtnDateWise.Checked = True Then
                    arrHeader.Add(("Date: " + clsCommon.GetPrintDate(DtpDocfDate.Value, "dd/MM/yyyy")))
                    arrHeader.Add("Name : Weight Difference Report")
                Else
                    arrHeader.Add(("Date: " + clsCommon.GetPrintDate(DtpDocfDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtDoctToDate.Value, "dd/MM/yyyy")) + " ")
                    arrHeader.Add("Name :Weight Difference Report +/- " + clsCommon.myCstr(txtDiffAmt.Value) + " Ltrs ")
                End If
                If Exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Sub Reset()
        txtMccCode.arrValueMember = Nothing
        txtRouteCode.arrValueMember = Nothing
        gv1.DataSource = Nothing
        txtDiffAmt.Text = 0
        DtpDocfDate.Text = clsCommon.GETSERVERDATE
        dtDoctToDate.Text = clsCommon.GETSERVERDATE
        rbtnDateWise.Checked = True
        lbltodate.Enabled = False
        dtDoctToDate.Enabled = False
        GetshiftType()
        GetFromshiftType()
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage2.Text = "Report"
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
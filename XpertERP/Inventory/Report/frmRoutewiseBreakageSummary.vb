Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Collections
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Microsoft.Office.Interop
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports common

Public Class FrmRoutewiseBreakageSummary
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()


    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.RoutewiseBreakageReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnprint.Visible = MyBase.isPrintFlag
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmRoutewiseBreakageSummary_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.Control And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub
    Private Sub FrmRoutewiseBreakageSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpFromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        rdodetail.IsChecked = True
        LoadVendor()
        LoadLocation()
        SetUserMgmtNew()
        chkLocAll.IsChecked = True
        chkAll.IsChecked = True
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnprint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "RT-BRK-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function

    Sub LoadVendor()
        Dim qry As String = "select route_no,route_desc from TSPL_ROUTE_MASTER order by route_no"
        cbgroute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgroute.ValueMember = "route_no"
        cbgroute.DisplayMember = "route_desc"

    End Sub

    Sub LoadLocation()
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub



    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click

        PrintData()
    End Sub
    Sub PrintData()

        If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Location")
            Return
        End If
        If chkSelect.IsChecked AndAlso cbgroute.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Route")
            Return
        End If

        Try
            Dim Str As String
            If rdodetail.IsChecked = True Then
                Str = " SELECT  '" + dtpFromdate.Value + "' as  fromdate, '" + dtptodate.Value + "' as todate , TSPL_TRANSFER_HEAD.Load_Out_No, TSPL_TRANSFER_HEAD.Route_No,TSPL_TRANSFER_HEAD.Transfer_No, TSPL_TRANSFER_HEAD.Date_Time_Removal, TSPL_TRANSFER_HEAD.From_Location,TSPL_TRANSFER_HEAD.Comp_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_TRANSFER_DETAIL.Item_Code ,TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_TRANSFER_DETAIL.Uom,ISNULL(    TSPL_TRANSFER_DETAIL.Leak ,0) as Leakage , ISNULL(TSPL_TRANSFER_DETAIL.Burst,0) as Brust,(TSPL_ROUTE_MASTER.Route_Desc ) as RouteDesc ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2 " & _
                                " FROM TSPL_TRANSFER_DETAIL " & _
                  " Left Outer JOIN  TSPL_TRANSFER_HEAD ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No " & _
                  "  Left Outer JOIN TSPL_ROUTE_MASTER on TSPL_TRANSFER_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No " & _
                  " Left Outer JOIN TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code " & _
                   " left Outer Join TSPL_COMPANY_MASTER on TSPL_TRANSFER_HEAD.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " & _
                   " where TSPL_TRANSFER_HEAD.Transfer_Type='LI' and TSPL_TRANSFER_HEAD.Item_Type='Full' and TSPL_LOCATION_MASTER.Location_Type ='Logical' and   CONVERT(date,TSPL_TRANSFER_HEAD.Transfer_Date ,103) >=CONVERT(date,'" + dtpFromdate.Value + "',103)  and CONVERT(date,TSPL_TRANSFER_HEAD.Transfer_Date ,103)  <=CONVERT(date,'" + dtptodate.Value + "',103)  "

                If chkAll.IsChecked = True Then
                    Str += " "
                Else
                    Str += " and TSPL_TRANSFER_HEAD.Route_No in (" + (clsCommon.GetMulcallString(cbgroute.CheckedValue)) + ")"
                End If

                If chkLocSelect.IsChecked Then
                    Str += " and  TSPL_TRANSFER_HEAD.To_Location in  (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") ) "
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Str)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "BreakageReportDetail", "Breakage Detail Report")
                frmCRV = Nothing
            ElseIf rdodetailcolumn.IsChecked = True Then
                FunExel()

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Sub FunExel()

        'Dim Str As String = " SELECT  '" + dtpFromdate.Value + "' as  fromdate, '" + dtptodate.Value + "' as todate , TSPL_TRANSFER_HEAD.Load_Out_No, TSPL_TRANSFER_HEAD.Route_No,TSPL_TRANSFER_HEAD.Transfer_No, TSPL_TRANSFER_HEAD.Date_Time_Removal, TSPL_TRANSFER_HEAD.From_Location,TSPL_TRANSFER_HEAD.Comp_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_TRANSFER_DETAIL.Item_Code ,TSPL_TRANSFER_DETAIL.Item_Desc,ISNULL(    TSPL_TRANSFER_DETAIL.Leak ,0) as Leakage , ISNULL(TSPL_TRANSFER_DETAIL.Burst,0) as Brust,(TSPL_ROUTE_MASTER.Route_Desc ) as RouteDesc ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2 " & _
        '                       " FROM TSPL_TRANSFER_DETAIL " & _
        '         " Left Outer JOIN  TSPL_TRANSFER_HEAD ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No " & _
        '         "  Left Outer JOIN TSPL_ROUTE_MASTER on TSPL_TRANSFER_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No " & _
        '         " Left Outer JOIN TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code " & _
        '          " left Outer Join TSPL_COMPANY_MASTER on TSPL_TRANSFER_HEAD.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " & _
        '          " where TSPL_TRANSFER_HEAD.Transfer_Type='LI' and TSPL_TRANSFER_HEAD.Item_Type='Full' and TSPL_LOCATION_MASTER.Location_Type ='Logical' and   CONVERT(date,TSPL_TRANSFER_HEAD.Transfer_Date ,103) >=CONVERT(date,'" + dtpFromdate.Value + "',103)  and CONVERT(date,TSPL_TRANSFER_HEAD.Transfer_Date ,103)  <=CONVERT(date,'" + dtptodate.Value + "',103)  "



        Dim arrlist As ArrayList
        arrlist = cbgroute.CheckedValue
        Dim Str As String
        If arrlist.Count = 1 Then

            Str = " select '" + dtpFromdate.Value + "' as  fromdate, '" + dtptodate.Value + "' as todate ,compname,loadout,routecode,RouteDesc,item_code,Leakage,Brust " & _
                 "from ( SELECT    max(TSPL_TRANSFER_HEAD.Load_Out_No) as loadout,max(TSPL_TRANSFER_HEAD.Transfer_No) as transferNo, max(TSPL_TRANSFER_HEAD.Route_No) as routecode, max(TSPL_TRANSFER_HEAD.Comp_Code) as compcode,TSPL_TRANSFER_DETAIL.Item_Code ,sum(ISNULL(    TSPL_TRANSFER_DETAIL.Leak ,0)) as Leakage , sum(ISNULL(TSPL_TRANSFER_DETAIL.Burst,0)) as Brust,max(TSPL_ROUTE_MASTER.Route_Desc ) as RouteDesc ,max(TSPL_COMPANY_MASTER.Comp_Name) as compname  FROM TSPL_TRANSFER_DETAIL  Left Outer JOIN  TSPL_TRANSFER_HEAD ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No   Left Outer JOIN TSPL_ROUTE_MASTER on TSPL_TRANSFER_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No  Left Outer JOIN TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code  left Outer Join TSPL_COMPANY_MASTER on TSPL_TRANSFER_HEAD.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " & _
                 " where TSPL_TRANSFER_HEAD.Transfer_Type='LI' and TSPL_TRANSFER_HEAD.Item_Type='Full' and TSPL_LOCATION_MASTER.Location_Type ='Logical' and    CONVERT(date,TSPL_TRANSFER_HEAD.Transfer_Date ,103) >=CONVERT(date,'" + dtpFromdate.Value + "',103)  and CONVERT(date,TSPL_TRANSFER_HEAD.Transfer_Date ,103)  <=CONVERT(date,'" + dtptodate.Value + "',103)    and TSPL_TRANSFER_HEAD.Route_No in ((" + (clsCommon.GetMulcallString(cbgroute.CheckedValue)) + ")) "

            If chkLocSelect.IsChecked Then
                Str += " and  TSPL_TRANSFER_HEAD.To_Location in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")  "
            End If


            Str += " group by TSPL_TRANSFER_DETAIL.Item_Code  )   xxx " & _
                  "   left outer join TSPL_TRANSFER_HEAD on xxx.transferNo=TSPL_TRANSFER_HEAD.Transfer_No "

            Dim ds As DataSet
            ds = connectSql.RunSQLReturnDS(Str)
            If ds.Tables(0).Rows.Count <> 0 Then
            Else
                common.clsCommon.MyMessageBoxShow("No data Found")
                Exit Sub
            End If


        Else
            common.clsCommon.MyMessageBoxShow("Select only one Route ")
            Exit Sub
        End If



        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Str)


        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value
        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add(misValue)
        xlWorkSheet = xlWorkBook.Sheets("sheet1")

        Dim style As Excel.Style = xlWorkSheet.Application.ActiveWorkbook.Styles.Add("NewStyle")
        style.Font.Bold = True


        '  style.Font.Size = 14
        style.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Transparent)




        If dt.Rows.Count > 0 Then
            Dim item As String
            Dim leak As String
            Dim brust As String
            Dim dtp As String = DateTime.Now()

            xlWorkSheet.Cells(1, 1) = dt.Rows(1)("compname").ToString
            xlWorkSheet.Cells(1, 1).Style = "NewStyle"

            xlWorkSheet.Cells(3, 1) = "Leakage/Burst Report(Route Wise)"
            xlWorkSheet.Cells(3, 1).Style = "NewStyle"

            xlWorkSheet.Cells(4, 1) = "From : " + dtpFromdate.Value + " "
            'xlWorkSheet.Cells(4, 1).Style = "NewStyle"

            xlWorkSheet.Cells(5, 1) = "To    : " + CDate(dtptodate.Value).ToString("dd/MM/yyyy") + " "
            'xlWorkSheet.Cells(5, 1).Style = "NewStyle"

            xlWorkSheet.Cells(7, 1) = "         Route No"
            xlWorkSheet.Cells(7, 2) = " Route Description"
            xlWorkSheet.Cells(7, 1).Style = "NewStyle"
            xlWorkSheet.Cells(7, 2).Style = "NewStyle"
            xlWorkSheet.Cells(9, 1) = "" + dt.Rows(1)("routecode").ToString + "     "
            xlWorkSheet.Cells(9, 2) = "    " + dt.Rows(1)("RouteDesc").ToString + " "

            'xlWorkSheet.Cells(9, 1).Style = "NewStyle"

            For no As Integer = 0 To dt.Rows.Count - 1
                Dim no1 As Integer = no + 3

                item = dt.Rows(no)("item_code").ToString
                leak = dt.Rows(no)("Leakage").ToString
                brust = dt.Rows(no)("Brust").ToString

                Dim value As String = " " + leak + "          " + brust + " "

                xlWorkSheet.Cells(7, no1) = "      " + item + "      "
                ' xlWorkSheet.Cells(no1).AutoFit = True
                xlWorkSheet.Cells(7, no1).Style = "NewStyle"

                xlWorkSheet.Cells(8, no1) = " Half            Bkg. "
                xlWorkSheet.Cells(8, 1).Style = "NewStyle"

                xlWorkSheet.Cells(9, no1) = value

            Next




        Else
            Exit Sub
        End If
        'xlWorkSheet.Cells(1, 1) = dataGrd.Item(1, 0).Value 

        Me.SaveFileDialog1.AddExtension = True
        Me.SaveFileDialog1.Filter = "Excel (*.xls;*.xlsx)|*.xls;*.xlsx"
        If Me.SaveFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then

            Dim strFileName As String = Me.SaveFileDialog1.FileName
            xlWorkSheet.SaveAs("" + strFileName + "")

            xlWorkBook.Close()
            xlApp.Quit()
            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)

            common.clsCommon.MyMessageBoxShow("Data Transferred ...")
        End If
    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Sub AutoFitAll()

        'Application.ScreenUpdating = False
        'Dim wkSt As String
        'Dim wkBk As Worksheet
        'wkSt = ActiveSheet.Name
        'For Each wkBk In ActiveWorkbook.Worksheets
        '    On Error Resume Next
        '    wkBk.Activate()
        '    Cells.EntireColumn.AutoFit()
        'Next wkBk
        'Sheets(wkSt).Select()
        'Application.ScreenUpdating = True

    End Sub



    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Sub Reset()

        chkAll.IsChecked = True
        dtpFromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        LoadVendor()
        LoadLocation()
        chkLocAll.IsChecked = True
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub chkAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAll.ToggleStateChanged
        cbgroute.Enabled = False
    End Sub

    Private Sub chkSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSelect.ToggleStateChanged
        cbgroute.Enabled = True
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub
End Class

Imports Microsoft.Office.Interop
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Collections
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports common
Public Class FrmBreakageReportSummary
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.BreakageReportSummary)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnprint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
        btnprint.Visible = MyBase.isPrintFlag
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmBreakageReportSummary_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            funprint()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub
    Private Sub FrmBreakageReportSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpFromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        rdosummary.IsChecked = True
        SetUserMgmtNew()
        LoadLocation()
        chkLocAll.IsChecked = True

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "BRK-RPT-SUM"
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

    Sub LoadLocation()
        ' qry As String = " select Location_Code ,Location_Desc  from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub



    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        funprint()
    End Sub
    Sub funprint()
        Try
            Dim str As String
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location")
                Return
            End If

            If rdosummary.IsChecked = True Then
                str = "select '" + dtpFromdate.Value + "' as  fromdate, '" + dtptodate.Value + "' as todate ,TSPL_TRANSFER_HEAD.To_Location, TSPL_TRANSFER_HEAD.Transfer_No,TSPL_TRANSFER_HEAD.Transfer_Date,TSPL_TRANSFER_HEAD.Date_Time_Removal ,ItemDesc,Leakage,Brust,RouteDesc,  Comp_Name, Item_Code ,TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1 , TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2  from " & _
                                  "(SELECT   max(TSPL_TRANSFER_HEAD.Route_No) as RouteCode,max(TSPL_TRANSFER_HEAD.Transfer_No) as transferNo, max(TSPL_TRANSFER_HEAD.Date_Time_Removal) as Time, max(TSPL_TRANSFER_HEAD.From_Location) as Loc,MAX(TSPL_TRANSFER_HEAD.Comp_Code) as Comp,max(TSPL_LOCATION_MASTER.Location_Desc) as Location,TSPL_TRANSFER_DETAIL.Item_Code ,Max(TSPL_TRANSFER_DETAIL.Item_Desc) as  ItemDesc,sum(ISNULL(    TSPL_TRANSFER_DETAIL.Leak ,0)) as Leakage , sum(ISNULL(TSPL_TRANSFER_DETAIL.Burst,0)) as Brust,max(TSPL_ROUTE_MASTER.Route_Desc ) as RouteDesc  " & _
                            " FROM TSPL_TRANSFER_DETAIL  " & _
         " Left Outer JOIN  TSPL_TRANSFER_HEAD ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No " & _
         " Left Outer JOIN TSPL_ROUTE_MASTER on TSPL_TRANSFER_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No" & _
         " Left Outer JOIN TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code " & _
         " where TSPL_TRANSFER_HEAD.Transfer_Type='LI' and TSPL_TRANSFER_HEAD.Item_Type='Full' and TSPL_LOCATION_MASTER.Location_Type ='Logical' group by " & _
         " Item_Code)xxx " & _
         " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON xxx.Comp = TSPL_COMPANY_MASTER.Comp_Code " & _
         " left outer join TSPL_TRANSFER_HEAD on xxx.transferNo=TSPL_TRANSFER_HEAD.Transfer_No  where    CONVERT(date,TSPL_TRANSFER_HEAD.Transfer_Date ,103) >=CONVERT(date,'" + dtpFromdate.Value + "',103)  and CONVERT(date,TSPL_TRANSFER_HEAD.Transfer_Date ,103)  <=CONVERT(date,'" + dtptodate.Value + "',103) "
                If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    str += " and  To_Location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(str)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "BreakageReportSummary", "Breakage Summary Report")
                frmCRV = Nothing
                'ElseIf rdodetail.IsChecked = True Then

                '    str = " SELECT  '" + dtpFromdate.Value + "' as  fromdate, '" + dtptodate.Value + "' as todate , TSPL_TRANSFER_HEAD.Load_Out_No, TSPL_TRANSFER_HEAD.Route_No,TSPL_TRANSFER_HEAD.Transfer_No, TSPL_TRANSFER_HEAD.Date_Time_Removal, TSPL_TRANSFER_HEAD.From_Location,TSPL_TRANSFER_HEAD.Comp_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_TRANSFER_DETAIL.Item_Code ,TSPL_TRANSFER_DETAIL.Item_Desc,ISNULL(    TSPL_TRANSFER_DETAIL.Leak ,0) as Leakage , ISNULL(TSPL_TRANSFER_DETAIL.Burst,0) as Brust,(TSPL_ROUTE_MASTER.Route_Desc ) as RouteDesc ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2 " & _
                '                    " FROM TSPL_TRANSFER_DETAIL " & _
                '      " Left Outer JOIN  TSPL_TRANSFER_HEAD ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No " & _
                '      "  Left Outer JOIN TSPL_ROUTE_MASTER on TSPL_TRANSFER_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No " & _
                '      " Left Outer JOIN TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code " & _
                '       " left Outer Join TSPL_COMPANY_MASTER on TSPL_TRANSFER_HEAD.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " & _
                '       " where TSPL_TRANSFER_HEAD.Transfer_Type='LI' and TSPL_TRANSFER_HEAD.Item_Type='Full' and TSPL_LOCATION_MASTER.Location_Type ='Logical' and   CONVERT(date,TSPL_TRANSFER_HEAD.Transfer_Date ,103) >=CONVERT(date,'" + dtpFromdate.Value + "',103)  and CONVERT(date,TSPL_TRANSFER_HEAD.Transfer_Date ,103)  <=CONVERT(date,'" + dtptodate.Value + "',103)  "

                '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(str)
                '    InventryViewer.funreport(dt, "BreakageReportDetail", "Breakage Detail Report")

            ElseIf rdosummaycolumn.IsChecked = True Then
                FunExel()
            End If
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(Str)
            'InventryViewer.funreport(dt, "BreakageReportSummary", "Breakage Summary Report")

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Sub Reset()

        dtpFromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        rdosummary.IsChecked = True
        LoadLocation()
        chkLocAll.IsChecked = True
    End Sub


    Private Sub FunExel()

        Dim Str As String = "select '" + dtpFromdate.Value + "' as  fromdate, '" + dtptodate.Value + "' as todate  ,TSPL_TRANSFER_HEAD.To_Location, TSPL_TRANSFER_HEAD.Transfer_No,TSPL_TRANSFER_HEAD.Transfer_Date,TSPL_TRANSFER_HEAD.Date_Time_Removal ,ItemDesc,Leakage,Brust,RouteDesc,  Comp_Name, Item_Code ,TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1 , TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2  from " & _
                                 "(SELECT   max(TSPL_TRANSFER_HEAD.Route_No) as RouteCode,max(TSPL_TRANSFER_HEAD.Transfer_No) as transferNo, max(TSPL_TRANSFER_HEAD.Date_Time_Removal) as Time, max(TSPL_TRANSFER_HEAD.From_Location) as Loc,MAX(TSPL_TRANSFER_HEAD.Comp_Code) as Comp,max(TSPL_LOCATION_MASTER.Location_Desc) as Location,TSPL_TRANSFER_DETAIL.Item_Code ,Max(TSPL_TRANSFER_DETAIL.Item_Desc) as  ItemDesc,sum(ISNULL(    TSPL_TRANSFER_DETAIL.Leak ,0)) as Leakage , sum(ISNULL(TSPL_TRANSFER_DETAIL.Burst,0)) as Brust,max(TSPL_ROUTE_MASTER.Route_Desc ) as RouteDesc  " & _
                           " FROM TSPL_TRANSFER_DETAIL  " & _
        " Left Outer JOIN  TSPL_TRANSFER_HEAD ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No " & _
        " Left Outer JOIN TSPL_ROUTE_MASTER on TSPL_TRANSFER_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No" & _
        " Left Outer JOIN TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.From_Location=TSPL_LOCATION_MASTER.Location_Code " & _
        " where TSPL_TRANSFER_HEAD.Transfer_Type='LI' and TSPL_TRANSFER_HEAD.Item_Type='Full' and TSPL_LOCATION_MASTER.Location_Type ='Logical' group by " & _
        " Item_Code)xxx " & _
        " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON xxx.Comp = TSPL_COMPANY_MASTER.Comp_Code " & _
        " left outer join TSPL_TRANSFER_HEAD on xxx.transferNo=TSPL_TRANSFER_HEAD.Transfer_No  where   CONVERT(date,TSPL_TRANSFER_HEAD.Transfer_Date ,103) >=CONVERT(date,'" + dtpFromdate.Value + "',103)  and CONVERT(date,TSPL_TRANSFER_HEAD.Transfer_Date ,103)  <=CONVERT(date,'" + dtptodate.Value + "',103) "

        If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
            Str += " and  To_Location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
        End If



        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Str)

        If dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No data found")
            Exit Sub
        End If


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

            xlWorkSheet.Cells(1, 1) = dt.Rows(1)("comp_name").ToString
            xlWorkSheet.Cells(1, 1).Style = "NewStyle"

            xlWorkSheet.Cells(3, 1) = "Leakage/Brust Report (Summary)"
            xlWorkSheet.Cells(3, 1).Style = "NewStyle"

            xlWorkSheet.Cells(4, 1) = "From : " + dtpFromdate.Value + " "
            'xlWorkSheet.Cells(4, 1).Style = "NewStyle"

            xlWorkSheet.Cells(5, 1) = "To    : " + CDate(dtptodate.Value).ToString("dd/MM/yyyy") + " "
            'xlWorkSheet.Cells(5, 1).Style = "NewStyle"

            xlWorkSheet.Cells(7, 1) = "            Date"
            xlWorkSheet.Cells(7, 1).Style = "NewStyle"

            xlWorkSheet.Cells(9, 1) = "    " + CDate(dtp).ToString("dd/MM/yyyy") + " "
            'xlWorkSheet.Cells(9, 1).Style = "NewStyle"

            For no As Integer = 0 To dt.Rows.Count - 1
                Dim no1 As Integer = no + 2

                item = dt.Rows(no)("item_code").ToString
                leak = dt.Rows(no)("Leakage").ToString
                brust = dt.Rows(no)("Brust").ToString

                Dim value As String = " " + leak + "          " + brust + " "

                xlWorkSheet.Cells(7, no1) = "      " + item + "      "
                xlWorkSheet.Cells(7, no1).Style = "NewStyle"

                xlWorkSheet.Cells(8, no1) = " Half            Bkg. "
                xlWorkSheet.Cells(8, 1).Style = "NewStyle"

                xlWorkSheet.Cells(9, no1) = value

            Next


            'Dim x As Integer
            '' x = dataGrd.Rows.Count - 1
            'Dim y As Integer
            '' y = dataGrd.Columns.Count
            'Dim i As Integer
            'Dim j As Integer
            'x = 2
            'y = 2
            'i = 0
            'j = 0
            'Dim intRow As Integer
            'Dim intColumn As Integer
            'For intRow = 0 To dt.Rows.Count - 1
            '    x = x + 1
            '    j = 0
            '    y = 0
            '    For intColumn = 0 To dt.Columns.Count - 1
            '        y = y + 1
            '        xlWorkSheet.Cells(x, y) = dt.Item(j, i).Value
            '        j += 1
            '    Next
            '    i = i + 1
            'Next
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
            MsgBox("Data Transferred ...", MsgBoxStyle.OkOnly)
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

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub
End Class

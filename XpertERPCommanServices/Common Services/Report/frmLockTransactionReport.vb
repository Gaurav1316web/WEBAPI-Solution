Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared

Public Class FrmLockTransactionReport
    Inherits FrmMainTranScreen
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Private Sub FrmLockTransactionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpFromLockDate.Value = clsCommon.GETSERVERDATE()
        dtpToLockDate.Value = clsCommon.GETSERVERDATE()
        Reset()
    End Sub

    Sub Reset()
        txtLocationMult.arrValueMember = Nothing
        txtLocationMult.Enabled = True
        gv.DataSource = Nothing
        chkLocationCode.IsChecked = True
        chkLocationSegment.IsChecked = False
        chkUser.Checked = False
        RadPageView1.SelectedPage = RadPageViewPage1
        dtpFromLockDate.Checked = False
        dtpToLockDate.Checked = False
        '***************************************************
        Panel1.Enabled = True
        dtpFromLockDate.Enabled = True
        dtpToLockDate.Enabled = True
        txtLocationMult.Enabled = True
        btnRefresh.Enabled = True
        btnExport.Enabled = False
        '***************************************************
    End Sub

    Private Function GetReport_Id()
        Dim rptid As String = Me.Form_ID
        If chkLocationCode.IsChecked = True Then
            rptid = rptid + "LC"
        ElseIf chkLocationSegment.IsChecked = True Then
            rptid = rptid + "LS"
        End If
        If chkUser.Checked = True Then
            rptid = rptid + "U"
        End If
        Return rptid
    End Function
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = GetReport_Id()
        TemplateGridview = gv
        LoadData(Exporter.Refresh)
    End Sub
    Sub LoadData(ByVal IsPrint As Exporter)
        Try
            'gv.DataSource = Nothing
            Dim From_Date As String = ""
            If dtpFromLockDate.Checked = True Then
                From_Date = clsCommon.GetPrintDate(dtpFromLockDate.Value, "dd-MMM-yyyy")
            End If
            Dim To_Date As String = ""
            If dtpToLockDate.Checked = True Then
                To_Date = clsCommon.GetPrintDate(dtpToLockDate.Value, "dd-MMM-yyyy")
            End If
            Dim strLoacation As String = String.Empty
            Dim mainQry As String = String.Empty
            Dim Qry As String = String.Empty
            Dim wher As String = " Where 2=2 "

            If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                strLoacation = clsCommon.GetMulcallString(txtLocationMult.arrValueMember)
                If chkLocationCode.IsChecked = True Then
                    If chkUser.Checked = True Then
                        wher = wher + "  and TSPL_LOCK_LOCATION_USER.Location_Code in  (" & strLoacation & ") "
                    Else
                        wher = wher + "  and TSPL_LOCK_LOCATION.Location_Code in  (" & strLoacation & ") "
                    End If

                Else
                    If chkUser.Checked = True Then
                        wher = wher + "  and TSPL_LOCK_LOCATION_SEGMENT_USER.Location_Segment_Code in  (" & strLoacation & ") "
                    Else
                        wher = wher + "  and TSPL_LOCK_LOCATION_SEGMENT.Location_Segment_Code in  (" & strLoacation & ") "
                    End If
                End If

            End If
            If dtpFromLockDate.Checked = True Then
                If chkLocationCode.IsChecked = True Then
                    If chkUser.Checked = True Then
                        wher = wher + " and TSPL_LOCK_LOCATION_USER.FromDate >= '" + From_Date + "' "
                    Else
                        wher = wher + " and TSPL_LOCK_LOCATION.Start_Date >= '" + From_Date + "' "
                    End If

                Else
                    If chkUser.Checked = True Then
                        wher = wher + " and TSPL_LOCK_LOCATION_SEGMENT_USER.FromDate >= '" + From_Date + "' "
                    Else
                        wher = wher + " and TSPL_LOCK_LOCATION_SEGMENT.Start_Date >= '" + From_Date + "' "
                    End If
                End If
            End If

            If dtpToLockDate.Checked = True Then
                If chkLocationCode.IsChecked = True Then
                    If chkUser.Checked = True Then
                        wher = wher + " and TSPL_LOCK_LOCATION_USER.ToDate <= '" + To_Date + "' "
                    Else
                        wher = wher + " and TSPL_LOCK_LOCATION.End_Date <= '" + To_Date + "' "
                    End If

                Else
                    If chkUser.Checked = True Then
                        wher = wher + " and TSPL_LOCK_LOCATION_SEGMENT_USER.ToDate <= '" + To_Date + "' "
                    Else
                        wher = wher + " and TSPL_LOCK_LOCATION_SEGMENT.End_Date <= '" + To_Date + "' "
                    End If
                End If

            End If
            If IsPrint = Exporter.Refresh Then

                If chkLocationCode.IsChecked = True Then
                    If chkUser.Checked = True Then
                        Qry = " select  TSPL_LOCK_LOCATION_USER.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Desc] , TSPL_LOCK_LOCATION_USER.Module_Name  as [Module Name] , TSPL_LOCK_LOCATION_USER.Trans_Name as [Screen Name] ,case when TSPL_LOCK_LOCATION_USER.FromDate is null then '' else convert (varchar, TSPL_LOCK_LOCATION_USER.FromDate,103) end as [From Lock Date] , case when TSPL_LOCK_LOCATION_USER.ToDate is null then '' else   convert (varchar,TSPL_LOCK_LOCATION_USER.ToDate,103) end as [To Locked Date]  ,TSPL_LOCK_LOCATION_USER.User_Code as [User Code] , TSPL_USER_MASTER.User_Name as [User Name] from TSPL_LOCK_LOCATION_USER left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code = TSPL_LOCK_LOCATION_USER.User_Code inner  join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_LOCK_LOCATION_USER.Location_Code  " + wher + " "
                    Else
                        Qry = " select  TSPL_LOCK_LOCATION.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Desc], TSPL_LOCK_LOCATION.Module_Name as [Module Name] , TSPL_LOCK_LOCATION .Trans_Name  as [Screen Name], case when TSPL_LOCK_LOCATION.Start_Date is null then '' else convert (varchar,TSPL_LOCK_LOCATION.Start_Date,103) end as [From Lock Date] , case when TSPL_LOCK_LOCATION.End_Date is null then '' else  convert (varchar,TSPL_LOCK_LOCATION.End_Date,103) end as [To Locked Date]  from TSPL_LOCK_LOCATION  inner join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_LOCK_LOCATION.Location_Code   " + wher + " and TSPL_LOCK_LOCATION.Is_Locked =1 "
                    End If

                Else
                    If chkUser.Checked = True Then
                        Qry = " select  TSPL_LOCK_LOCATION_SEGMENT_USER .Location_Segment_Code  as [Location Segment Code ],TSPL_GL_SEGMENT_CODE.Description as [Location Segment Desc] , TSPL_LOCK_LOCATION_SEGMENT_USER.Module_Name as [Module Name] , TSPL_LOCK_LOCATION_SEGMENT_USER .Trans_Name as [Screen Name] , case when TSPL_LOCK_LOCATION_SEGMENT_USER .FromDate is null then '' else  convert (varchar,TSPL_LOCK_LOCATION_SEGMENT_USER .FromDate,103) end [From Lock Date] , case when TSPL_LOCK_LOCATION_SEGMENT_USER.ToDate is null then '' else convert (varchar, TSPL_LOCK_LOCATION_SEGMENT_USER.ToDate,103) end as [To Locked Date] , TSPL_LOCK_LOCATION_SEGMENT_USER.User_Code as [User Code] , TSPL_USER_MASTER.User_Name as [User Name] from TSPL_LOCK_LOCATION_SEGMENT_USER left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code = TSPL_LOCK_LOCATION_SEGMENT_USER.User_Code  inner  join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code = TSPL_LOCK_LOCATION_SEGMENT_USER .Location_Segment_Code " + wher + ""
                    Else
                        Qry = " select  TSPL_LOCK_LOCATION_SEGMENT.Location_Segment_Code as [Location Segment Code ] ,TSPL_GL_SEGMENT_CODE.Description as [Location Segment Desc] , TSPL_LOCK_LOCATION_SEGMENT.Module_Name as [Module Name] , TSPL_LOCK_LOCATION_SEGMENT.Trans_Name as [Screen Name] , case when TSPL_LOCK_LOCATION_SEGMENT.Start_Date is null then '' else   convert (varchar,TSPL_LOCK_LOCATION_SEGMENT.Start_Date,103) end as [From Lock Date], case when TSPL_LOCK_LOCATION_SEGMENT.End_Date is null then '' else convert (varchar,TSPL_LOCK_LOCATION_SEGMENT.End_Date,103) end as [To Locked Date] from  TSPL_LOCK_LOCATION_SEGMENT  inner  join   TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code = TSPL_LOCK_LOCATION_SEGMENT.Location_Segment_Code    " + wher + " and TSPL_LOCK_LOCATION_SEGMENT.Is_Locked =1 "
                    End If
                End If

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
                Else
                    gv.MasterTemplate.SummaryRowsBottom.Clear()
                    gv.DataSource = Nothing
                    gv.DataSource = dt
                    SetGridFormation()
                    ReStoreGridLayout()
                    RadPageView1.SelectedPage = RadPageViewPage2
                    '***************************************************
                    Panel1.Enabled = False
                    dtpFromLockDate.Enabled = False
                    dtpToLockDate.Enabled = False
                    txtLocationMult.Enabled = False
                    btnRefresh.Enabled = False
                    btnExport.Enabled = True
                    '***************************************************
                End If
                
            ElseIf IsPrint = Exporter.Excel Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim strTemp As String = ""
                arrHeader.Add("From Lock Date : " + From_Date + " ")
                arrHeader.Add("To Lock Date : " + To_Date + " ")
                If clsCommon.myLen(strLoacation) > 0 Then
                    arrHeader.Add("Location : " + strLoacation)
                Else
                    arrHeader.Add("Location : " + "All")
                End If
                arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
                clsCommon.MyExportToExcelGrid(" Lock Transaction Report", gv, arrHeader, Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormation()
        For Each col As GridViewColumn In gv.Columns
            col.BestFit()
        Next
    End Sub

    Private Sub txtLocationMult__My_Click(sender As Object, e As EventArgs) Handles txtLocationMult._My_Click
        Try
            Dim qry As String
            If chkLocationSegment.IsChecked = True Then
                qry = " Select Segment_code as Code, Description as Name  from TSPL_GL_SEGMENT_CODE where Seg_No=7 "
                txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("MulLoc", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
            Else
                qry = " Select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical'"
                txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("MulLoc", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        If (gv.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        'LoadData(Exporter.Excel)
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim From_Date As String = ""
            If dtpFromLockDate.Checked = True Then
                From_Date = clsCommon.GetPrintDate(dtpFromLockDate.Value, "dd-MMM-yyyy")
                arrHeader.Add("From Lock Date : " + From_Date + " ")
            End If
            Dim To_Date As String = ""
            If dtpToLockDate.Checked = True Then
                To_Date = clsCommon.GetPrintDate(dtpToLockDate.Value, "dd-MMM-yyyy")
                arrHeader.Add("To Lock Date :" + To_Date + " ")
            End If
            'Dim strLoacation As String = String.Empty
            'If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            '    strLoacation = clsCommon.GetMulcallString(txtLocationMult.arrValueMember)
            'End If
            'If clsCommon.myLen(strLoacation) > 0 Then
            '    arrHeader.Add("Location : " + strLoacation)
            'Else
            '    arrHeader.Add("Location : " + "All")
            'End If

            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmLockTransactionReport & "'"))
            If txtLocationMult.arrDispalyMember IsNot Nothing AndAlso txtLocationMult.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMult.arrDispalyMember))
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
                clsCommon.MyExportToPDF("Lock Transaction Report", gv, arrHeader, "Lock Transaction Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub gv_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv.CellFormatting
    '    If e.CellElement.ColumnInfo.Name = "SNo" AndAlso (IsNothing(e.CellElement.Value) = True Or clsCommon.myLen(e.CellElement.Value) <= 0) Then
    '        e.CellElement.Value = e.CellElement.RowIndex + 1
    '    End If
    'End Sub

    Private Sub btnExportPDF_Click(sender As Object, e As EventArgs) Handles btnExportPDF.Click
        If (gv.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        Export(EnumExportTo.PDF)
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

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub
End Class

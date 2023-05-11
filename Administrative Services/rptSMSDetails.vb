Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

' Ticket No :TEC/22/11/18-000372   by prabhakar - Create new report 
Public Class rptSMSDetails
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub


    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID + IIf(rdbSms.Checked = True, "SMS", "EMAIL")
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim dt As New DataTable

            If rdbSms.Checked = True Then
                qry = " select TSPL_SMS_DETAIL.Code as [SMS Code] ,tspl_sms_head.SMS_Text as [SMS Text], tspl_sms_head.Created_By as [Created By] ,  " & _
                      " convert(varchar,tspl_sms_head.Created_Date,103) as [Created Date], " & _
                      " TSPL_SMS_DETAIL.Mobile_No as [Mobile No] , convert (varchar, TSPL_SMS_DETAIL.Send_On ,103)  as [Send On]  from TSPL_SMS_DETAIL " & _
                      " left outer join tspl_sms_head on tspl_sms_head.code =TSPL_SMS_DETAIL.Code " & _
                      " where  convert(date,tspl_sms_head.Created_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,tspl_sms_head.Created_Date,103)<=convert(date,'" + ToDate.Value + "',103) "

                If rdbSend.Checked = True Then
                    qry += " and Send_On is Not Null "
                End If
                If rdbNotSend.Checked = True Then
                    qry += " and send_On is Null "
                End If
                qry = qry + " order by convert(date,tspl_sms_head.Created_Date,103) , TSPL_SMS_DETAIL.Code asc   "
            Else
                qry = " select tspl_email_Head .Code  as  [Email Code], tspl_email_Head .Email_subject as [Email Subject]  " & _
                      " , tspl_email_Head.Email_Text as [Email Text] ,Attachment_1_File_Name as [Attachment1] , Attachment_2_File_Name as [Attachment2], tspl_email_Head.Email_ID as [Email ID] ,Created_By as [Created By] " & _
                      " , convert (varchar,Created_Date,103)  as [Created Date], convert (varchar, Send_On,103) as [Send On] from tspl_email_Head " & _
                      " where  convert(date,Created_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,Created_Date,103)<=convert(date,'" + ToDate.Value + "',103) "

                If rdbSend.Checked = True Then
                    qry += " and Send_On is Not Null "
                End If
                If rdbNotSend.Checked = True Then
                    qry += " and send_On is Null "
                End If
                qry = qry + " order by convert(date,Created_Date,103) , Code asc   "
            End If

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
                ' Gv1.Columns("Trans Type").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.AutoSizeRows = True
                Gv1.EnableFiltering = True
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
            MessageBox.Show(err.Message)
        End Try
    End Sub
   
   
   
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = PageSetupReport_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
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
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub


    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptSMSDetailsReport & "'"))
            arrHeader.Add("Report Type : " & IIf(rdbSms.Checked = True, "SMS", "Email"))
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("SMS/Email Details Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("SMS/Email Details Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
End Class

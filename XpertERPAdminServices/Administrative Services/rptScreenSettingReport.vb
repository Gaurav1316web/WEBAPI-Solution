Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI


' Ticket No : TEC/22/11/18-000371  By Prabhakar - Create New Report
Public Class rptScreenSettingReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Dim strSettingType As String = Nothing
    Dim strSettingCode As String = Nothing
    Dim isBackOn As Boolean = False

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub


    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim frm As New FrmPWD(Nothing)
        frm.strType = "SIRC"
        frm.strCode = "SIReversAndCreate"
        frm.ShowDialog()
        If frm.isPasswordCorrect = True Then
            Reset()
        Else
            Me.Close()
        End If

    End Sub
    Sub Reset()
        rdbSummary.Checked = True
        txtType.arrValueMember = Nothing
        txtCode.arrValueMember = Nothing
        txtScreen.arrValueMember = Nothing
        txtModule.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        strSettingType = ""
        strSettingCode = ""
        isBackOn = False
        btnGo.Enabled = True
        btnExp.Enabled = True
        btnBack.Enabled = False
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID + IIf(rdbSummary.Checked = True, "S", "D")
            TemplateGridview = Gv1
            Dim BaseQry As String = ""
            Dim qry As String = ""
            Dim dt As New DataTable
            If rdbDetails.Checked = True Then
                qry = "  select '' as SNO, TBL_MODULE.Program_Name as Module_Name, TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.Program_Code as [Screen Code],  " & _
               "  case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else TSPL_PROGRAM_MASTER.Program_Name end [Screen Name], " & _
               "  TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Type as [Type], TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Code as [Code] , TSPL_FIXED_PARAMETER.Description ,TSPL_FIXED_PARAMETER.Specification , case when TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.Control_Type = 0 then 'CheckBox' when TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.Control_Type = 1 then 'TextBox' when TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.Control_Type = 2 then 'NumericBox' end as 'Control Type'  " & _
               "  from TSPL_FIXED_PARAMETER_PROGRAM_MAPPING " & _
               "  left outer join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code = TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.Program_Code " & _
               "  left outer join TSPL_FIXED_PARAMETER on TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Type = TSPL_FIXED_PARAMETER.Type and TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Code = TSPL_FIXED_PARAMETER.Code " & _
               "  left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('SM')) as TBL_SMODULE on TBL_SMODULE.Program_Code = TSPL_PROGRAM_MASTER.Parent_Code  " & _
               "  left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('M')) as TBL_MODULE on TBL_MODULE.Program_Code = TBL_SMODULE.Parent_Code  "
                '"  where TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.Control_Type = 0   "

            Else
                qry = "  select '' as SNO , TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Type as [Type] , TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Code as [Code] , " & _
                      "  (SELECT STUFF((SELECT ';' +case when len (isnull(P2.Re_Name,'')) > 0 then P2.Re_Name else P2.Program_Name end  " & _
                      "  FROM TSPL_FIXED_PARAMETER_PROGRAM_MAPPING tn2 left outer join tspl_Program_Master P2 on tn2.Program_code = P2 .program_code WHERE tn2.FP_Type = TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Type  " & _
                      "  and tn2.FP_Code = TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Code  and tn2.Program_Code = P2.Program_Code " & _
                      "  FOR XML PATH('')) ,1,1,'')) as [Screen Name] " & _
                      "  from TSPL_FIXED_PARAMETER_PROGRAM_MAPPING  " & _
                      "  left outer join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code = TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.Program_Code " & _
                      "  left outer join TSPL_FIXED_PARAMETER on TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Type = TSPL_FIXED_PARAMETER.Type and TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Code =  TSPL_FIXED_PARAMETER.Code  " & _
                      "    left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('SM')) as TBL_SMODULE on TBL_SMODULE.Program_Code = TSPL_PROGRAM_MASTER.Parent_Code   " & _
                      "  left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('M')) as TBL_MODULE on TBL_MODULE.Program_Code = TBL_SMODULE.Parent_Code  "
                ' "  where TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.Control_Type = 0 "

            End If

            If txtType.arrValueMember IsNot Nothing AndAlso txtType.arrValueMember.Count > 0 And isBackOn = False Then
                qry += " and TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Type in (" + clsCommon.GetMulcallString(txtType.arrValueMember) + ")"
            End If
            If clsCommon.myLen(strSettingType) > 0 AndAlso isBackOn = True Then
                qry += " and TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Type in ('" + strSettingType + "')  "
            End If
            If txtCode.arrValueMember IsNot Nothing AndAlso txtCode.arrValueMember.Count > 0 And isBackOn = False Then
                qry += " and TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Code in (" + clsCommon.GetMulcallString(txtCode.arrValueMember) + ")  "
            End If
            If clsCommon.myLen(strSettingCode) > 0 AndAlso isBackOn = True Then
                qry += " and TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Code in ('" + strSettingCode + "')  "
            End If
            If txtModule.arrValueMember IsNot Nothing AndAlso txtModule.arrValueMember.Count > 0 And isBackOn = False Then
                qry += " and TBL_MODULE.Program_Code in (" + clsCommon.GetMulcallString(txtModule.arrValueMember) + ")"
            End If
            If txtScreen.arrValueMember IsNot Nothing AndAlso txtScreen.arrValueMember.Count > 0 Then ' And isBackOn = False
                qry += " and TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.Program_Code in (" + clsCommon.GetMulcallString(txtScreen.arrValueMember) + ")"
            End If
            If rdbDetails.Checked = True Then
                If isBackOn = True Then
                    If clsCommon.myLen(strSettingType) > 0 Or clsCommon.myLen(strSettingCode) > 0 Then
                        qry += " where TBL_MODULE.Program_Name is not null "
                    End If
                End If
                qry += "  order by TBL_MODULE.Program_Name, TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.Program_Code "
            Else
                qry += "  group by  TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Type , TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Code  " & _
                       "  order by TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Type "
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
                Gv1.EnableFiltering = True
                '=========================================================================
                For ii As Integer = 0 To Gv1.Rows.Count - 1
                    Gv1.Rows(ii).Cells("SNO").Value = ii + 1
                    'Dim intCurrRow As Integer = Gv1.CurrentRow.Index
                    'Gv1.CurrentRow.Cells("SNO").Value = clsCommon.myCdbl(intCurrRow + 1)
                Next
                '=========================================================================

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
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptScreenSettingReport & "'"))
            arrHeader.Add("Report Type : " & IIf(rdbSummary.Checked = True, "Summary", "Details"))
            If txtType.arrValueMember IsNot Nothing AndAlso txtType.arrValueMember.Count > 0 Then
                arrHeader.Add("Type : " + clsCommon.GetMulcallStringWithComma(txtType.arrDispalyMember))
            End If
            If txtCode.arrValueMember IsNot Nothing AndAlso txtCode.arrValueMember.Count > 0 Then
                arrHeader.Add("Code : " + clsCommon.GetMulcallStringWithComma(txtCode.arrDispalyMember))
            End If
            If txtModule.arrValueMember IsNot Nothing AndAlso txtModule.arrValueMember.Count > 0 Then
                arrHeader.Add("Module : " + clsCommon.GetMulcallStringWithComma(txtModule.arrDispalyMember))
            End If
            If txtScreen.arrValueMember IsNot Nothing AndAlso txtScreen.arrValueMember.Count > 0 Then
                arrHeader.Add("Screen : " + clsCommon.GetMulcallStringWithComma(txtScreen.arrDispalyMember))
            End If

            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Screen Setting Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Screen Setting Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub rdbDetails_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDetails.CheckedChanged

    End Sub

    Private Sub rdbSummary_CheckedChanged(sender As Object, e As EventArgs) Handles rdbSummary.CheckedChanged

    End Sub



    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick

        If rdbSummary.Checked = True AndAlso e.Column Is Gv1.Columns("Type") Then
            strSettingType = Gv1.CurrentRow.Cells("Type").Value
            isBackOn = True
            rdbDetails.Checked = True
            rdbSummary.Checked = False
            btnGo.PerformClick()
            btnGo.Enabled = False
            btnExp.Enabled = False
            btnBack.Enabled = True
        ElseIf rdbSummary.Checked = True AndAlso e.Column Is Gv1.Columns("Code") Then
            strSettingCode = Gv1.CurrentRow.Cells("Code").Value
            isBackOn = True
            rdbDetails.Checked = True
            rdbSummary.Checked = False
            btnGo.PerformClick()
            btnGo.Enabled = False
            btnExp.Enabled = False
            btnBack.Enabled = True
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        rdbSummary.Checked = True
        isBackOn = False
        btnGo.Enabled = True
        btnExp.Enabled = True
        btnBack.Enabled = False
        strSettingType = ""
        strSettingCode = ""
        btnGo.PerformClick()
    End Sub

    Private Sub txtType__My_Click(sender As Object, e As EventArgs) Handles txtType._My_Click
        Dim qry As String = " select distinct TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Type  as [Type]  from TSPL_FIXED_PARAMETER_PROGRAM_MAPPING where Control_Type = 0 "
        txtType.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@SettingType", qry, "Type", "Type", txtType.arrValueMember, txtType.arrDispalyMember)
    End Sub

    Private Sub txtCode__My_Click(sender As Object, e As EventArgs) Handles txtCode._My_Click
        Dim qry As String = " select distinct TSPL_FIXED_PARAMETER_PROGRAM_MAPPING.FP_Code  as [Code]  from TSPL_FIXED_PARAMETER_PROGRAM_MAPPING where Control_Type = 0 "
        txtCode.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@SettingCode", qry, "Code", "Code", txtCode.arrValueMember, txtCode.arrDispalyMember)
    End Sub

    Private Sub txtModule__My_Click(sender As Object, e As EventArgs) Handles txtModule._My_Click
        Dim qry As String = " select Program_Code as [Code], Program_Name as [Name]  from TSPL_PROGRAM_MASTER where Type in ('M') "
        txtModule.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@ModuleCode", qry, "Code", "Code", txtModule.arrValueMember, txtModule.arrDispalyMember)
    End Sub

    Private Sub txtScreen__My_Click(sender As Object, e As EventArgs) Handles txtScreen._My_Click
        Dim qry As String = " select TSPL_PROGRAM_MASTER.Program_Code as Code,case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end  as Name , TBL_MODULE.Program_Name as [Module Name] from TSPL_PROGRAM_MASTER " & _
                           " left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('SM')) as TBL_SMODULE on TBL_SMODULE.Program_Code = TSPL_PROGRAM_MASTER.Parent_Code  " & _
                           " left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('M')) as TBL_MODULE on TBL_MODULE.Program_Code = TBL_SMODULE.Parent_Code " & _
                           " where  TBL_MODULE.Program_Code in (select  distinct Module_Name from TSPL_MODULE_PERMISSION ) and  not TSPL_PROGRAM_MASTER.Type in ('M','SM') order by SNo "
        txtScreen.arrValueMember = clsCommon.ShowMultipleSelectForm("Screen@MulitSection", qry, "Code", "Name", txtScreen.arrValueMember, txtScreen.arrDispalyMember)
    End Sub

    Private Sub Gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles Gv1.CellFormatting
        'Dim intCurrRow As Integer = Gv1.CurrentRow.Index
        'Gv1.CurrentRow.Cells("SNO").Value = clsCommon.myCdbl(intCurrRow + 1)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
End Class

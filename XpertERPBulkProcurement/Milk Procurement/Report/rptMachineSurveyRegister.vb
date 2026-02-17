Imports common
Public Class rptMachineSurveyRegister

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub rptMachineSurveyRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            If objCommonVar.RCDFCFP Then
                HideAndShowFields(True)
            Else
                HideAndShowFields(False)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub BlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.GroupDescriptors.Clear()
        Dim viewBlank As New TableViewDefinition()
        gv1.ViewDefinition = viewBlank
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.MasterView.Refresh()
    End Sub

    Sub EnableDisableFields(ByVal isBool As Boolean)
        RadGroupBox1.Enabled = isBool
        RadGroupBox2.Enabled = isBool
    End Sub

    Sub HideAndShowFields(ByVal isBool As Boolean)
        rbtnUnionWise.Visible = isBool
        lblMilkAnalyzer.Visible = Not isBool
        txtMultMilkAnalyzer.Visible = Not isBool
        lblWeighmentMachine.Visible = Not isBool
        txtMultWeighingMachine.Visible = Not isBool
    End Sub

    Sub Reset()
        BlankGrid()
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Try
            EnableDisableFields(True)
            Reset()
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found !", Me.Text)
                Exit Sub
            End If
            Dim Qry As String = ReturnFinalQry(False)
            dt = Nothing
            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                BlankGrid()
                gv1.DataSource = dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                View()
                SetGridFormat()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                EnableDisableFields(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function ReturnFinalQry(ByVal isPrint As Boolean) As String
        Dim Qry As String = Nothing
        If rbtnDetail.Checked Then
            Qry = "Select [S.No.],[Union],MCC_Code As [BMC Code],MCC_NAME As [BMC Name],Zone_Code As [Zone Code],Description As [Zone Name],VLC_Code_VLC_Uploader As [DCS Code],Vendor_Name As [DCS Name],Case When IsAMCU=1 Then 'Yes' Else 'No' End As [Analyzer Yes/No],BrandName As [Analyzer Make],Case When IsWeighing=1 Then 'Yes' Else 'No' End As [Weighing Machine Yes/No],Weighing_BrandName As [Weighing Machine Make] from(" & ReturnBaseQry() & ")finalQry"
        ElseIf rbtnMachineWise.Checked Then
            Qry = "Select Row_Number() Over (Order By (Select 1)) As [S.No.],[Union],COUNT(VLC_Code_VLC_Uploader) As [Total DCS],Sum(Case When isnull(IsAMCU,0)=1 Then 1 Else 0 End) As [AnalyzerYes],Sum(Case When isnull(IsAMCU,0)=0 Then 1 Else 0 End) As [AnalyzerNo],Sum(Case When isnull(IsWeighing,0)=1 Then 1 Else 0 End) As [WeighingYes],Sum(Case When isnull(IsWeighing,0)=0 Then 1 Else 0 End) As [WeighingNo]  from(" & ReturnBaseQry() & ")finalQry Group By [Union]"
        Else
            Qry = "; With BaseQry As (" & ReturnBaseQry() & ")"
            If isPrint AndAlso rbtnUnionWise.Checked Then
                Qry &= "Select ROW_NUMBER() Over (Order By (Select 1)) As [S.No.],printQry.*,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_STATE_MASTER.STATE_NAME,'" & objCommonVar.CurrentUser & "' As PrintBy from ("
            End If
            Qry &= "Select "
            If Not isPrint Then
                Qry &= " ROW_NUMBER() Over (Order By (Select 1)) As [S.No.],"
            End If
            Qry &= " [Union],IsNull(MachineName,'') As [Machine Name],IsNull(MachineType,'') As [Machine Type],SUM(MachineCount) As [No of Machine] from (
Select [Union],BrandName As MachineName,Case When IsAMCU=1 Then 'Analyzer' Else Null End As MachineType,Case When IsAMCU=1 Then 1 Else 0 End As MachineCount from BaseQry
Union All
Select [Union],Weighing_BrandName As MachineName,Case When IsWeighing=1 Then 'Weighing Scale' Else Null End As MachineType,Case When IsWeighing=1 Then 1 Else 0 End As MachineCount from BaseQry
)finalQry
Group By [Union],MachineName,MachineType"
            If isPrint AndAlso rbtnUnionWise.Checked Then
                Qry &= ")printQry Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" & objCommonVar.CurrComp_Code1 & "'
Left Outer Join TSPL_STATE_MASTER On TSPL_STATE_MASTER.STATE_CODE=TSPL_COMPANY_MASTER.State Order By [Union]"
            End If
        End If

        If isPrint AndAlso Not rbtnUnionWise.Checked Then
            Qry = "Select printQry.*,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_STATE_MASTER.STATE_NAME,'" & objCommonVar.CurrentUser & "' As PrintBy from(" & Qry & ")printQry
Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" & objCommonVar.CurrComp_Code1 & "'
Left Outer Join TSPL_STATE_MASTER On TSPL_STATE_MASTER.STATE_CODE=TSPL_COMPANY_MASTER.State"
        End If
        Return Qry
    End Function

    Function ReturnBaseQry() As String
        Dim dt As DataTable = Nothing
        If txtMultUnion.arrValueMember IsNot Nothing AndAlso txtMultUnion.arrValueMember.Count > 0 Then
            dt = clsMilkUnion.UnionDBName1(txtMultUnion.arrValueMember)
        Else
            If objCommonVar.RCDFCFP Then
                dt = clsMilkUnion.UnionDBName()
            Else
                Dim arrUnion As New ArrayList()
                arrUnion.Add(objCommonVar.CurrDatabase)
                dt = clsMilkUnion.UnionDBName1(arrUnion)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    dt = clsMilkUnion.GetUnionDBandLocName(objCommonVar.CurrDatabase)
                End If
            End If
        End If

        Dim Qry As String = ""
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim i As Integer = 0
            For Each strUnion In dt.Rows
                If i <> 0 Then
                    Qry &= Environment.NewLine & " Union All " & Environment.NewLine
                End If
                Qry &= "Select Row_Number() Over (Order By (Select 1)) As [S.No.],'" & clsCommon.myCstr(strUnion("Location_Name")) & "' As [Union],TSPL_MCC_MASTER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME ,TSPL_ZONE_MASTER.Zone_Code ,TSPL_ZONE_MASTER.Description,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.IsAMCU,TSPL_AMCU_MASTER.Name As BrandName,TSPL_VENDOR_MASTER.IsWeighing,TSPL_WEIGHING_MASTER.Name As Weighing_BrandName
from " & clsCommon.myCstr(strUnion("DataBase_Name")) & ".dbo.TSPL_VLC_MASTER_HEAD
Left Outer Join " & clsCommon.myCstr(strUnion("DataBase_Name")) & ".dbo.TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code And TSPL_VLC_MASTER_HEAD.Active=1
Left Outer Join " & clsCommon.myCstr(strUnion("DataBase_Name")) & ".dbo.TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
Left Outer Join " & clsCommon.myCstr(strUnion("DataBase_Name")) & ".dbo.TSPL_ZONE_MASTER On TSPL_ZONE_MASTER.Zone_Code=TSPL_VENDOR_MASTER.Zone_Code
Left Outer Join " & clsCommon.myCstr(strUnion("DataBase_Name")) & ".dbo.TSPL_AMCU_MASTER On TSPL_AMCU_MASTER.Code=TSPL_VENDOR_MASTER.BrandName
Left Outer Join " & clsCommon.myCstr(strUnion("DataBase_Name")) & ".dbo.TSPL_WEIGHING_MASTER On TSPL_WEIGHING_MASTER.Code=TSPL_VENDOR_MASTER.Weighing_BrandName"
                Qry &= " Where 1=1 "
                If txtMultZone.arrValueMember IsNot Nothing AndAlso txtMultZone.arrValueMember.Count > 0 Then
                    Qry &= " And TSPL_ZONE_MASTER.Zone_Code In (" & clsCommon.GetMulcallString(txtMultZone.arrValueMember) & ")"
                End If
                If txtMultBMC.arrValueMember IsNot Nothing AndAlso txtMultBMC.arrValueMember.Count > 0 Then
                    Qry &= " And TSPL_MCC_MASTER.MCC_Code In (" & clsCommon.GetMulcallString(txtMultBMC.arrValueMember) & ") "
                End If
                If txtMultDCS.arrValueMember IsNot Nothing AndAlso txtMultDCS.arrValueMember.Count > 0 Then
                    Qry &= " And TSPL_VENDOR_MASTER.Vendor_Code In (" & clsCommon.GetMulcallString(txtMultDCS.arrValueMember) & ") "
                End If
                If txtMultMilkAnalyzer.arrValueMember IsNot Nothing AndAlso txtMultMilkAnalyzer.arrValueMember.Count > 0 Then
                    Qry &= " And TSPL_AMCU_MASTER.Code In (" & clsCommon.GetMulcallString(txtMultMilkAnalyzer.arrValueMember) & ")"
                End If
                If txtMultWeighingMachine.arrValueMember IsNot Nothing AndAlso txtMultWeighingMachine.arrValueMember.Count > 0 Then
                    Qry &= " And TSPL_WEIGHING_MASTER.Code In (" & clsCommon.GetMulcallString(txtMultWeighingMachine.arrValueMember) & ")"
                End If
                i += 1
            Next
        Else
            Throw New Exception("Database name not found !")
        End If
        Return Qry
    End Function

    Sub SetGridFormat()
        Try
            gv1.AutoExpandGroups = False
            gv1.ShowGroupPanel = False
            gv1.ShowRowHeaderColumn = False
            gv1.AllowAddNewRow = False
            gv1.AllowDeleteRow = False
            gv1.EnableFiltering = True
            gv1.ShowFilteringRow = True
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).BestFit()
            Next
            If rbtnMachineWise.Checked Then
                gv1.Columns("AnalyzerYes").HeaderText = "Yes"
                gv1.Columns("AnalyzerNo").HeaderText = "No"
                gv1.Columns("WeighingYes").HeaderText = "Yes"
                gv1.Columns("WeighingNo").HeaderText = "No"
            End If
            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.AutoExpandGroups = False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub View()
        Try
            If rbtnMachineWise.Checked Then
                If gv1.Rows.Count > 0 Then
                    Dim view As New ColumnGroupsViewDefinition()
                    view.ColumnGroups.Add(New GridViewColumnGroup(""))
                    view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("S.No.").Name)
                    view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Union").Name)
                    view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Total DCS").Name)

                    view.ColumnGroups.Add(New GridViewColumnGroup("Milk Analyzer"))
                    view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("AnalyzerYes").Name)
                    view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("AnalyzerNo").Name)

                    view.ColumnGroups.Add(New GridViewColumnGroup("Weighing Scale"))
                    view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("WeighingYes").Name)
                    view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("WeighingNo").Name)
                    gv1.ViewDefinition = view
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMachineSurveyRegister & "'"))

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Report Name : " & strHeading)
            'transportSql.exportdata(gv1, "", Me.Text, False, arrHeader, False, False, True)
            clsCommon.MyExportToExcel(Nothing, gv1, arrHeader, strHeading)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                'Dim arrHeader As List(Of String) = New List(Of String)()
                clsCommon.MyExportToPDF(Me.Text, gv1, Nothing, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultZone__My_Click(sender As Object, e As EventArgs) Handles txtMultZone._My_Click
        Try
            Dim strQry As String = "select Zone_Code As Code,Description from TSPL_Zone_MASTER "
            txtMultZone.arrValueMember = clsCommon.ShowMultipleSelectForm("@Deduction", strQry, "Code", "Description", txtMultZone.arrValueMember, txtMultZone.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultBMC__My_Click(sender As Object, e As EventArgs) Handles txtMultBMC._My_Click
        Try
            Dim strQry As String = "select MCC_Code As Code,MCC_NAME As Name,Area_Location_Code As [Area Location Code] From tspl_mcc_master where 1=1 "
            txtMultBMC.arrValueMember = clsCommon.ShowMultipleSelectForm("@BMC", strQry, "Code", "Name", txtMultBMC.arrValueMember, txtMultBMC.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultDCS__My_Click(sender As Object, e As EventArgs) Handles txtMultDCS._My_Click
        Try
            Dim strQry As String = "Select TSPL_VLC_MASTER_HEAD.VSP_Code As Code,TSPL_VLC_MASTER_HEAD.VLC_Name As Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [DCS Uploader Code] from TSPL_VLC_MASTER_HEAD Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code where TSPL_VENDOR_MASTER.Vendor_Group_Code='DCS'"
            txtMultDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("@DCS", strQry, "Code", "Name", txtMultDCS.arrValueMember, txtMultDCS.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultUnion__My_Click(sender As Object, e As EventArgs) Handles txtMultUnion._My_Click
        Try
            Dim arrUnion As New ArrayList()
            arrUnion.Add(objCommonVar.CurrComp_Code1)
            Dim dt As DataTable = Nothing
            If objCommonVar.RCDFCFP Then
                dt = clsMilkUnion.UnionDBName()
            Else
                dt = clsMilkUnion.UnionDBName1(arrUnion)
            End If
            arrUnion = Nothing
            Dim lstUnion As New List(Of String)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each strUinon In dt.Rows
                    lstUnion.Add(clsCommon.myCstr(strUinon("DataBase_Name")))
                Next
            End If
            Dim strQry As String = "SELECT [TSPL_APP_LOCATION].DataBase_Name As Code,[TSPL_APP_LOCATION].Location_Name As Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 "
            If lstUnion IsNot Nothing AndAlso lstUnion.Count > 0 Then
                strQry &= " And [TSPL_APP_LOCATION].DataBase_Name In (" & clsCommon.GetMulcallString(lstUnion) & ") "
            End If
            strQry &= " ORDER BY [TSPL_APP_LOCATION].Location_Name"
            txtMultUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("@Union", strQry, "Code", "Name", txtMultDCS.arrValueMember, txtMultDCS.arrDispalyMember)
            lstUnion = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultMilkAnalyzer__My_Click(sender As Object, e As EventArgs) Handles txtMultMilkAnalyzer._My_Click
        Try
            Dim strQry As String = "select Code,Name from TSPL_AMCU_MASTER"
            txtMultMilkAnalyzer.arrValueMember = clsCommon.ShowMultipleSelectForm("@AMCU", strQry, "Code", "Name", txtMultMilkAnalyzer.arrValueMember, txtMultMilkAnalyzer.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultWeighingMachine__My_Click(sender As Object, e As EventArgs) Handles txtMultWeighingMachine._My_Click
        Try
            Dim strQry As String = "select Code,Name from TSPL_weighing_MASTER"
            txtMultWeighingMachine.arrValueMember = clsCommon.ShowMultipleSelectForm("@Weighning", strQry, "Code", "Name", txtMultWeighingMachine.arrValueMember, txtMultWeighingMachine.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim strQry As String = ReturnFinalQry(True)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frm As New frmCrystalReportViewer()
                If rbtnDetail.Checked Then
                    frm.funreport(Form_ID, CrystalReportFolder.CommonForUnionAndCattlefeed, dt, "crptUnionWiseMachineDetail", "Union Wise Machine Details")
                ElseIf rbtnMachineWise.Checked Then
                    frm.funreport(Form_ID, CrystalReportFolder.CommonForUnionAndCattlefeed, dt, "crptUnionWiseDCSMachineSummary", "Milk Union Wise Analyzer & Weighing Scal Count")
                ElseIf rbtnUnionWise.Checked Then
                    frm.funreport(Form_ID, CrystalReportFolder.CommonForUnionAndCattlefeed, dt, "crptAnalyzerandWeighingScalWise", "Analyzer And Weighing Scal Wise")
                End If
                frm = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to print !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
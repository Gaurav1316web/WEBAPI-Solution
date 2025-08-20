Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports common.UserControls
Public Class Can_Summary_Report
    Inherits FrmMainTranScreen
    Dim AreaWiseBilling As Boolean = False

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Can_Summary_Report_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = clsCommon.GETSERVERDATE()
        Reset()
        AreaWiseBilling = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AreaWiseBilling, clsFixedParameterCode.AreaWiseBilling, Nothing)) = 1)
        LblTanker.Visible = AreaWiseBilling
        fndArea.Visible = AreaWiseBilling
    End Sub


    Public Sub LoadData()
        Try
            Dim ConversionFactor As String = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
            Dim dt As New DataTable
            Dim strQry As String = ""
            If txtBMC.arrValueMember IsNot Nothing AndAlso txtBMC.arrValueMember.Count > 0 Then
                strQry += " and TSPL_MCC_MASTER.MCC_Code in (" + clsCommon.GetMulcallString(txtBMC.arrValueMember) + ")"
            End If

            If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                strQry += " and TSPL_MCC_MASTER.Area_Location_Code in (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ")"
            End If

            'If TxtArea.arrValueMember IsNot Nothing AndAlso TxtArea.arrValueMember.Count > 0 Then
            '    strQry += " and TSPL_VLC_MASTER_HEAD.VLC_Code in (" + clsCommon.GetMulcallString(TxtArea.arrValueMember) + ")"
            'End If
            strQry = "Select 
                        CONVERT(varchar(10), final.Shift_Date, 103) as [From Date],
                        CONVERT(varchar(10), final.Shift_Date, 103) as [To Date],
                        CONVERT(varchar(10), final.Shift_Date, 103) as Shift_Date,
                        SUM(CASE WHEN final.Shift = 'Morning' THEN final.[No Of Cans] ELSE 0 END) as Morning_Cans,
                        SUM(CASE WHEN final.Shift = 'Evening' THEN final.[No Of Cans] ELSE 0 END) as Evening_Cans,
                        SUM(final.[No Of Cans]) as Total_Cans
                    from (
                            Select 
                                Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as date) as Shift_Date,
                                case when isnull(TSPL_MILK_SRN_HEAD.Capping_Apply,0)=1 
                                     then TSPL_MILK_SRN_DETAIL.Capping_FAT else null end as Capping_FAT,
                                Case When TSPL_MILK_SRN_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,
                                Case 
                                    When TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No IS Null 
                                        Then TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.No_Of_Cans 
                                    Else Case 
                                             When TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No Is Null 
                                                  Then TSPL_MILK_SHIFT_UPLOADER_DETAIL.No_Of_Cans 
                                             Else 0 
                                         End 
                                End As [No Of Cans]
                            From TSPL_MILK_SRN_DETAIL 
                            Left Outer Join TSPL_MILK_SRN_HEAD 
                                On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE  
                            Left Outer Join TSPL_MILK_SHIFT_UPLOADER_DETAIL 
                                ON TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No = TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No 
                            Left Outer Join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL 
                                ON TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_NO = TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No 
                            Left Outer Join TSPL_MCC_MASTER 
                                On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE
                            Left Outer Join TSPL_VLC_MASTER_HEAD 
                                on TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code
                            Left Outer Join TSPL_COMPANY_MASTER 
                                on 2 = 2
                    where Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) <='" + clsCommon.GetPrintDate(dtpToDate.Value) + "' " + strQry + " ) As final "
            strQry += "Group by final.Shift_Date"
            dt = clsDBFuncationality.GetDataTable(strQry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt

                RadPageView1.SelectedPage = RadPageViewPage2

                Gv1.EnableFiltering = True
                Gv1.ReadOnly = True
                'FormatGrid()
                'ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If
            'SetGridFormationOFGV1()
            Gv1.BestFitColumns()
            'ReStoreGridLayout()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Reset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = Gv1
        LoadData()
    End Sub

    Private Sub txtBMC__My_Click(sender As Object, e As EventArgs) Handles txtBMC._My_Click
        Try
            Dim qry As String = "select MCC_Code as Code,Mcc_Code_VLC_Uploader as [Mcc Uploader Code],MCC_NAME as Name from TSPL_MCC_MASTER where 2=2 "
            txtBMC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "Code", "Name", txtBMC.arrValueMember, txtBMC.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub TxtArea__My_Click(sender As Object, e As EventArgs)
    '    Try
    '        Dim qry As String = "select Area_Location_Code from TSPL_MCC_MASTER where 2=2 "
    '        TxtArea.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUArea", qry, "Area_Location_Code", " ", TxtArea.arrValueMember, TxtArea.arrDispalyMember)
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub

    Private Sub txtDCS__My_Click(sender As Object, e As EventArgs) Handles txtDCS._My_Click
        Try
            Dim qry As String = "select VLC_Code as Dcs,VLC_Name as DcsName from TSPL_VLC_MASTER_HEAD where 2=2 "
            txtDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUDcs", qry, "VLC_Code", "DcsName", txtDCS.arrValueMember, txtDCS.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Reset()
    End Sub

    Sub Reset()
        txtDCS.arrValueMember = Nothing
        'TxtArea.arrValueMember = Nothing
        txtBMC.arrValueMember = Nothing
        fndArea.Value = ""
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        Gv1.DataSource = Nothing
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = clsCommon.GETSERVERDATE()
        'ControlEnableDisable(True)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click

        Dim strQryy As String = ""
        If txtBMC.arrValueMember IsNot Nothing AndAlso txtBMC.arrValueMember.Count > 0 Then
            strQryy += " and TSPL_MCC_MASTER.MCC_Code in (" + clsCommon.GetMulcallString(txtBMC.arrValueMember) + ")"
        End If

        If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
            strQryy += " and TSPL_MCC_MASTER.Area_Location_Code in (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ")"
        End If

        Try
            Dim strQry As String = ""
            Dim qry As String = "SELECT '" + fromDate.Value + "' as FromDate,'" + dtpToDate.Value + "' as Todate, 
                        CONVERT(varchar(10), final.Shift_Date, 103) as [From Date],
                        CONVERT(varchar(10), final.Shift_Date, 103) as [To Date],
                        CONVERT(varchar(10), final.Shift_Date, 103) as Shift_Date,
                        SUM(CASE WHEN final.Shift = 'Morning' THEN final.[No Of Cans] ELSE 0 END) as Morning_Cans,
                        SUM(CASE WHEN final.Shift = 'Evening' THEN final.[No Of Cans] ELSE 0 END) as Evening_Cans,
                        SUM(final.[No Of Cans]) as Total_Cans,
                        MAX(final.Add1) as Add1,
                        MAX(final.Comp_Name) as Comp_Name,
                        MAX(final.mcc_name) as mcc
                    from (
                            Select 
                                Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as date) as Shift_Date,
                                case when isnull(TSPL_MILK_SRN_HEAD.Capping_Apply,0)=1 
                                     then TSPL_MILK_SRN_DETAIL.Capping_FAT else null end as Capping_FAT,
                                Case When TSPL_MILK_SRN_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,
                                Case 
                                    When TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No IS Null 
                                        Then TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.No_Of_Cans 
                                    Else Case 
                                             When TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No Is Null 
                                                  Then TSPL_MILK_SHIFT_UPLOADER_DETAIL.No_Of_Cans 
                                             Else 0 
                                         End 
                                End As [No Of Cans],
                                TSPL_COMPANY_MASTER.Add1,
                                TSPL_COMPANY_MASTER.Comp_Name,
                                TSPL_MCC_MASTER.mcc_name
                            From TSPL_MILK_SRN_DETAIL 
                            Left Outer Join TSPL_MILK_SRN_HEAD 
                                On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE  
                            Left Outer Join TSPL_MILK_SHIFT_UPLOADER_DETAIL 
                                ON TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No = TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No 
                            Left Outer Join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL 
                                ON TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_NO = TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No 
                            Left Outer Join TSPL_MCC_MASTER 
                                On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE
                            Left Outer Join TSPL_VLC_MASTER_HEAD 
                                on TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code
                            Left Outer Join TSPL_COMPANY_MASTER 
                                on 2 = 2
                     where Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) <='" + clsCommon.GetPrintDate(dtpToDate.Value) + "' " + strQryy + " ) As final "
            qry += "Group by final.Shift_Date"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.MilkProcurement, dt, "rptCanSummaryReport", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndArea__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndArea._MYValidating
        Try
            Dim sQuery As String = " Select TSPL_LOCATION_MASTER.Location_Code as Code ,  TSPL_LOCATION_MASTER.Location_Desc, Type from TSPL_LOCATION_MASTER
     "
            fndArea.Value = clsCommon.ShowSelectForm("Location@Plant@Master", sQuery, "Code", "TSPL_LOCATION_MASTER.Type <> 'PLANT' OR TSPL_LOCATION_MASTER.Location_Category <> 'Mcc'", fndArea.Value, "Code", isButtonClicked)
            Dim arrMCCMapped As New ArrayList
            Dim arrMCCName As New ArrayList
            Dim dt As New DataTable
            Dim query As String = "select MCC_Code,MCC_NAME from TSPL_MCC_MASTER  WHERE Area_Location_Code='" + fndArea.Value + "'"
            dt = Nothing

            dt = clsDBFuncationality.GetDataTable(query)

            For i As Integer = 0 To dt.Rows.Count - 1
                arrMCCMapped.Add(dt.Rows(i)("MCC_Code"))
                arrMCCName.Add(dt.Rows(i)("MCC_NAME"))
            Next
            txtBMC.arrValueMember = arrMCCMapped
            'If fndLocation.Value <> "" Then
            '    lblLocation.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndArea.Value & "'")
            'Else
            '    lblLocation.Text = ""
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub
End Class
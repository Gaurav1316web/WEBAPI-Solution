Imports common
Public Class FrmDcscollectionHistory
    Private Sub lblDate_Click(sender As Object, e As EventArgs) Handles lblDate.Click

    End Sub

    Private Sub frmDemandHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDate.Value = clsCommon.GETSERVERDATE()
        txttodate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
    Private Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmDcscollectionHistory & "'"))
            arrHeader.Add("Date : " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy"))
            If clsCommon.CompairString(cmbShift.Text, "Morning") = CompairStringResult.Equal Then
                arrHeader.Add("Shift Type : Morning")
            ElseIf clsCommon.CompairString(cmbShift.Text, "Evening") = CompairStringResult.Equal Then
                arrHeader.Add("Shift Type : Evening")
            End If
            arrHeader.Add("DCS : " + txtBoothDesc.Text)
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub Reset()
        txtDate.Value = clsCommon.GETSERVERDATE()
        txttodate.Value = clsCommon.GETSERVERDATE()
        txtBooth.Value = ""
        cmbShift.SelectedIndex = 0
        txtBoothDesc.Text = ""
        gv1.DataSource = Nothing
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Reset()
    End Sub

    Private Sub txtBooth__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBooth._MYValidating
        Try
            Dim StrQry As String = "select distinct VLC_Code_VLC_Uploader as [Code],VLC_Name as [Dcs Name] from TSPL_VLC_MASTER_HEAD "
            Dim WhrCls As String = Nothing
            txtBooth.Value = clsCommon.ShowSelectForm("BoothDetails", StrQry, "Code", WhrCls, txtBooth.Value, "Code", isButtonClicked)
            txtBoothDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Name  from TSPL_VLC_MASTER_HEAD  where VLC_Code_VLC_Uploader ='" + txtBooth.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim strqry As String = " Select ROW_NUMBER() OVER (ORDER BY final.[Doc Date], final.Shift) AS [SR No],final.[Doc Date],final.Shift ,final.[Vlc Uploader Code] as Dcs,final.[VLC Name] as Dcs_Name,final.[Milk Weight(KG)],final.[FAT(%)],final.[SNF(%)],
                                     final.[FAT(KG)],final.[SNF(KG)],final.[SRN Rate] as Rate,final.NET_AMOUNT From (Select case when isnull(TSPL_MILK_SRN_HEAD.Capping_Apply,0)=1 then TSPL_MILK_SRN_DETAIL.Capping_FAT else null end as Capping_FAT, 
                                     Convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As Date,Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_SRN_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift, 
                                     TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name],TSPL_MILK_SRN_DETAIL.ACC_QTY As [Milk Weight(KG)],TSPL_MILK_SRN_DETAIL.FAT_PER As [FAT(%)], 
                                     TSPL_MILK_SRN_DETAIL.SNF_PER As [SNF(%)],Convert(decimal(18,3), TSPL_MILK_SRN_DETAIL.FAT_KG) As [FAT(KG)], Convert(decimal(18,3),TSPL_MILK_SRN_DETAIL.SNF_KG) As [SNF(KG)],TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate],isnull(TSPL_MILK_SRN_DETAIL.NET_AMOUNT,0) as NET_AMOUNT  
                                     From TSPL_MILK_SRN_DETAIL 
                                     Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE  
                                     left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code 
                                     Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE  
                                     where 2 = 2 and  Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(txtDate.Value) + "' and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) <='" + clsCommon.GetPrintDate(txttodate.Value) + "'And TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader in (" + txtBooth.Value + ")  ) As final"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.MasterView.Refresh()

                gv1.DataSource = dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next

                'RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
                gv1.EnableFiltering = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class

Imports System.Data.SqlClient
Imports common
Imports Telerik

Public Class SrnCorrection
    Inherits FrmMainTranScreen

    Public strDocNo As String = ""
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub
    Private Sub SrnCorrection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim docno = strDocNo
            'If clsCommon.myLen(strDocNo) > 0 Then
            '    Throw New Exception("NO Data Found")
            'End If
            Dim qry As String = "select TSPL_MILK_SRN_DETAIL.DOC_CODE AS SRNNO,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_MILK_SRN_DETAIL.Qty,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER,
                                 TSPL_MILK_SRN_DETAIL.RATE,TSPL_MILK_SRN_DETAIL.AMOUNT,TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.DOC_CODE AS [SRN AFTER CORRECTION],
                                 TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.Qty AS  [Qty AFTER CORRECTION],TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.FAT_PER as[FAT% AFTER CORRECTION] ,
                                 TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.SNF_PER as [SNF% AFTER CORRECTION],TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.RATE as [RATE AFTER CORRECTION],
                                 TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.AMOUNT as [AMOUNT AFTER CORRECTION],TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.Remarks as [Remarks],Case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then'Credit' else '' end as Document_Type,
                                 TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Total
                            from 
                            TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS
                            left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.DOC_CODE
                            left outer join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
                            left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No
                            left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
                            left outer join TSPL_MILK_COLLECTION_DCS_DETAIL as TabDCS1 on TabDCS1.PK_Id=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail   
                            left outer join TSPL_MILK_COLLECTION_DCS_DETAIL as TabDCS2 on  TabDCS2.PK_Id= TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail
                            inner join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
                            left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_MILK_SRN_HEAD.VSP_CODE
							left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.RefDocNo=TSPL_MILK_SRN_DETAIL.DOC_CODE
                            where TabDCS1.Document_No='" + docno + "' or TabDCS2.Document_No='" + docno + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            Dim viewBlank As New TableViewDefinition()
            gv1.ViewDefinition = viewBlank
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                gv1.DataSource = dt
            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                SetGridFormationOFGV1Multiple()
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
            View()
            gv1.BestFitColumns()
        Catch ex As Exception
            'clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub View()
        Try
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup("SRN"))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("SRNNO").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("VLC_Code_VLC_Uploader").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Qty").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("FAT_PER").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("SNF_PER").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("RATE").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("AMOUNT").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("CORRECTION AFTER PROCESS"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("SRN AFTER CORRECTION").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Qty AFTER CORRECTION").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("FAT% AFTER CORRECTION").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("SNF% AFTER CORRECTION").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("RATE AFTER CORRECTION").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("AMOUNT AFTER CORRECTION").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("AP DETAIL"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Document_No").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Document_Type").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Document_Total").Name)

            gv1.ViewDefinition = view
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub SetGridFormationOFGV1Multiple()
        gv1.TableElement.TableHeaderHeight = 70
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns("SRNNO").HeaderText = "SRN NO"
            gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS Uploader NO"
            gv1.Columns("Qty").HeaderText = "Qty"
            gv1.Columns("FAT_PER").HeaderText = "FAT %"
            gv1.Columns("SNF_PER").HeaderText = "SNF %"
            gv1.Columns("RATE").HeaderText = "RATE"
            gv1.Columns("AMOUNT").HeaderText = "AMOUNT"
            gv1.Columns("SRN AFTER CORRECTION").HeaderText = "SRN AFTER CORRECTION"
            gv1.Columns("SRN AFTER CORRECTION").IsVisible = False
            gv1.Columns("Qty AFTER CORRECTION").HeaderText = "Qty AFTER CORRECTION"
            gv1.Columns("FAT% AFTER CORRECTION").HeaderText = "FAT% AFTER CORRECTION"
            gv1.Columns("SNF% AFTER CORRECTION").HeaderText = "SNF% AFTER CORRECTION"
            gv1.Columns("RATE AFTER CORRECTION").HeaderText = "RATE AFTER CORRECTION"
            gv1.Columns("AMOUNT AFTER CORRECTION").HeaderText = "AMOUNT AFTER CORRECTION"
            gv1.Columns("Document_No").HeaderText = "AP Invoice"
            gv1.Columns("Document_Type").HeaderText = "Type"
            gv1.Columns("Document_Total").HeaderText = "Total"
        Next
    End Sub
End Class
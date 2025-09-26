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
            Dim qry As String = "WITH CTE AS
            (SELECT OwnDCS_DOC_CODE,OwnDCS_Qty,OwnDCS_FAT_PER,OwnDCS_FAT_KG,OwnDCS_SNF_PER,OwnDCS_SNF_KG,OwnDCS_CLR,OwnDCS_Price_Code,OwnDCS_RATE,OwnDCS_AMOUNT,
            OwnDCS_ACC_Qty,OwnDCS_ACC_Qty_LTR,OwnDCS_DRCR_Amt,Created_Date
            FROM TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS WHERE OwnDCS_DOC_CODE IS NOT NULL),XX AS(
            SELECT * FROM ((SELECT *,ROW_NUMBER() OVER (PARTITION BY DOC_CODE ORDER BY Created_Date DESC) AS rn
            FROM (  SELECT t.DOC_CODE,t.Qty,t.FAT_PER,t.FAT_KG,t.SNF_PER,t.SNF_KG,t.CLR,t.Price_Code,t.RATE,t.AMOUNT,t.ACC_Qty,t.ACC_Qty_LTR,t.DRCR_Amt,
            t.Created_Date FROM TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS t
            INNER JOIN CTE c ON c.OwnDCS_DOC_CODE = t.DOC_CODE

        UNION ALL

        SELECT OwnDCS_DOC_CODE,OwnDCS_Qty,OwnDCS_FAT_PER,OwnDCS_FAT_KG,OwnDCS_SNF_PER,OwnDCS_SNF_KG,OwnDCS_CLR,OwnDCS_Price_Code,OwnDCS_RATE,
            OwnDCS_AMOUNT,OwnDCS_ACC_Qty,OwnDCS_ACC_Qty_LTR,OwnDCS_DRCR_Amt, Created_Date
        FROM CTE 
		 union all
        select DOC_CODE,	Qty,	FAT_PER,	FAT_KG,	SNF_PER,	SNF_KG,	CLR,	Price_Code,	RATE,	AMOUNT,	ACC_Qty,	ACC_Qty_LTR,	DRCR_Amt,TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.Created_Date from TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS 
				) vv )  ) xxt where XXT.rn=1)
        SELECT TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.DOC_CODE AS SRNNO,CONVERT(VARCHAR, TSPL_MILK_SRN_HEAD.DOC_DATE,103) AS DOC_DATE,coalesce(TabDCS1.Document_No,TabDCS2.Document_No)as Truck_SheetNo,
        TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_MILK_SRN_DETAIL.Qty,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER,TSPL_MILK_SRN_DETAIL.RATE,
        TSPL_MILK_SRN_DETAIL.AMOUNT,XX.DOC_CODE AS [SRN AFTER CORRECTION],XX.Qty AS [Qty AFTER CORRECTION],XX.FAT_PER AS [FAT% AFTER CORRECTION],XX.SNF_PER AS [SNF% AFTER CORRECTION],
        XX.RATE AS [RATE AFTER CORRECTION],XX.AMOUNT AS [AMOUNT AFTER CORRECTION],TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.Remarks AS [Remarks],
        CASE WHEN TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' THEN 'Debit' WHEN TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' THEN 'Credit' ELSE '' END AS Document_Type,
        TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Total
        FROM TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS
        LEFT JOIN TSPL_MILK_SRN_HEAD ON TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.DOC_CODE
        LEFT JOIN TSPL_MILK_SRN_DETAIL ON TSPL_MILK_SRN_DETAIL.DOC_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE
        LEFT JOIN TSPL_MILK_SHIFT_UPLOADER_DETAIL ON TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No = TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No
        LEFT JOIN TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL ON TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No = TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
        LEFT JOIN TSPL_MILK_COLLECTION_DCS_DETAIL AS TabDCS1 ON TabDCS1.PK_Id = TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail
        LEFT JOIN TSPL_MILK_COLLECTION_DCS_DETAIL AS TabDCS2 ON TabDCS2.PK_Id = TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail
        INNER JOIN TSPL_MILK_PURCHASE_INVOICE_DETAIL ON TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE
        LEFT JOIN TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MILK_SRN_HEAD.VSP_CODE
        LEFT JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.RefDocNo = TSPL_MILK_SRN_DETAIL.DOC_CODE AND RefDocType In('CAP-MSN','CAP-MSN-CDCS')
        LEFT JOIN XX ON XX.DOC_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE
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
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Truck_SheetNo").Name)
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
            gv1.Columns("Truck_SheetNo").HeaderText = "Truck Sheet No"
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
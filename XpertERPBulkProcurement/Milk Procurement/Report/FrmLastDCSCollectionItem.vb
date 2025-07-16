Imports common
Imports XpertERPEngine
Imports System.Data.SqlClient
Public Class FrmLastDCSCollectionItem
    Inherits FrmMainTranScreen



    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData
    End Sub
    Public Sub LoadData()
        Try
            txtMultBmc.Enabled = False
            txtMultDCS.Enabled = False
            txtDate.Enabled = False
            Dim dt As New DataTable
            Dim strQry As String = Nothing
            Dim whrcls As String = ""
            Dim whrclsDCSSALE As String = ""
            whrcls = "  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <=convert(date,'" + txtDate.Value + "' ,103) "
            whrclsDCSSALE = "convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_DATE,103) <=convert(date,'" + txtDate.Value + "' ,103) "
            If txtMultBmc.arrValueMember IsNot Nothing AndAlso txtMultBmc.arrValueMember.Count > 0 Then
                whrcls += "  and TSPL_MCC_MASTER.MCC_Code in (" + clsCommon.GetMulcallString(txtMultBmc.arrValueMember) + ")  "
                whrclsDCSSALE += "  and TSPL_MCC_MASTER.MCC_Code in (" + clsCommon.GetMulcallString(txtMultBmc.arrValueMember) + ")  "
            End If
            If txtMultDCS.arrValueMember IsNot Nothing AndAlso txtMultDCS.arrValueMember.Count > 0 Then
                whrcls += "  and TSPL_VLC_MASTER_HEAD.VLC_Code in (" + clsCommon.GetMulcallString(txtMultDCS.arrValueMember) + ")  "
                whrclsDCSSALE += "  and TSPL_VLC_MASTER_HEAD.VLC_Code in (" + clsCommon.GetMulcallString(txtMultDCS.arrValueMember) + ")  "
            End If

            If rbtnMilkSrn.Checked Then
                strQry = " select * from (
 select row_number() over (Partition by [DCS Code] order by [Date] desc,[Shift] desc) as sno ,xx.* from (
 select  
 TSPL_MCC_MASTER.MCC_Code as [BMC Code],
 (MCC_NAME) as[BMC NAME],
 (TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader) as Mcc_Code_VLC_Uploader,
(TSPL_VLC_MASTER_HEAD.VLC_Code) as [DCS Code],	
(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader ,
(TSPL_VLC_MASTER_HEAD.VLC_Name) as [DCS Name],
cast(TSPL_MILK_SRN_HEAD.DOC_DATE as date) as [Date],
SHIFT as [Shift],
(TSPL_MILK_SRN_detail.ACC_Qty) as qty,
(TSPL_MILK_SRN_detail.FAT_PER) as [Fat Per],
(TSPL_MILK_SRN_detail.SNF_PER) as [SNF Per],
(TSPL_MILK_SRN_detail.FAT_KG) as [Fat Kg],
(TSPL_MILK_SRN_detail.SNF_KG) as [SNF KG] 
from  TSPL_MILK_SRN_detail 
LEFT OUTER JOIN TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_detail.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
LEFT OUTER JOIN TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SRN_HEAD.VLC_CODE
LEFT OUTER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_SRN_HEAD.MCC_CODE 

where " & whrcls & " )xx
)xxx where sno=1"
            Else
                strQry = "select * from (
 select row_number() over (Partition by [DCS Code] order by [Date] desc) as sno ,xx.* from 
 (
 SELECT TSPL_MCC_MASTER.MCC_Code as [BMC Code],
(MCC_NAME) as[BMC NAME],
(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader) as Mcc_Code_VLC_Uploader,
TSPL_VLC_MASTER_HEAD.VLC_Code as [DCS Code],
(TSPL_VLC_MASTER_HEAD.VLC_Name) as [DCS Name],
(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader ,
(Item_Code) as [Item Code],
(qty) as Qty,
FORMAT(TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 'dd/MM/yyyy') as [Date] FROM TSPL_SD_SALE_INVOICE_DETAIL

LEFT OUTER JOIN  TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
 left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code 
 left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code
left join TSPL_VLC_MASTER_HEAD on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code 
  Left Outer Join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC

where  " & whrclsDCSSALE & "   )xx
)xxx where sno=1  AND ISNULL(LTRIM(RTRIM([DCS Code])), '') <> ''"
            End If
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
                FormatGrid()
                'ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If
            Gv1.BestFitColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub FormatGrid()

        Gv1.AutoExpandGroups = False
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True


        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        If rbtnMilkSrn.Checked OrElse rbtnDCSSaleReturn.Checked Then
            Gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = "BMC UploaderNo"
            Gv1.Columns("Mcc_Code_VLC_Uploader").IsVisible = False
            Gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS Uploader Code"
            Gv1.Columns("VLC_Code_VLC_Uploader").IsVisible = False
            Gv1.Columns("sno").IsVisible = False

        End If
        Gv1.Columns("Qty").HeaderText = "Qty"



        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim qty As New GridViewSummaryItem("Qty", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(qty)

        'Dim FATKG As New GridViewSummaryItem("FAT KG", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(FATKG)
        'Dim SNFKG As New GridViewSummaryItem("SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(SNFKG)
        Gv1.ShowGroupPanel = True
        Gv1.MasterTemplate.AutoExpandGroups = True
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        'Dim summaryItem As New GridViewSummaryItem()
        'Gv1.TableElement.TableHeaderHeight = 25
        'Gv1.MasterTemplate.ShowRowHeaderColumn = True
        'For ii As Integer = 0 To Gv1.Columns.Count - 1
        '    Gv1.Columns(ii).ReadOnly = True
        '    Gv1.Columns(ii).IsVisible = True
        '    If rbtnMilkSrn.Checked OrElse rbtnDCSSaleReturn.Checked Then
        '        Gv1.Columns("BMC UploaderNo").IsVisible = False
        '        Gv1.Columns("DCS Uploader Code").IsVisible = False
        '    End If
        'Next
    End Sub


    Private Sub FrmLastDCSCollectionItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub txtMultBmc__My_Click(sender As Object, e As EventArgs) Handles txtMultBmc._My_Click
        Dim qry As String = " select  TSPL_MCC_MASTER.MCC_Code as [Code],MCC_NAME as[BMC NAME],TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as [ BMC UploaderNo] from TSPL_MCC_MASTER where 2=2  "
        txtMultBmc.arrValueMember = clsCommon.ShowMultipleSelectForm("VSPMulSelect", qry, "Code", "", txtMultBmc.arrValueMember, txtMultBmc.arrDispalyMember)
    End Sub

    Private Sub txtMultDCS__My_Click(sender As Object, e As EventArgs) Handles txtMultDCS._My_Click
        Dim qry As String = " select TSPL_VLC_MASTER_HEAD.VLC_Code as [Code],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Uploader Code] ,TSPL_VLC_MASTER_HEAD.VLC_Name as [DCS Name] from TSPL_VLC_MASTER_HEAD
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
where  TSPL_MCC_MASTER.MCC_Code in (" + clsCommon.GetMulcallString(txtMultBmc.arrValueMember) + ") "
        txtMultDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("VSPMulSelect", qry, "Code", "", txtMultDCS.arrValueMember, txtMultDCS.arrDispalyMember)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub Reset()
        'txtMultBmc.Enabled=True
        txtMultBmc.Enabled = True
        txtMultDCS.Enabled = True
        txtDate.Enabled = True
        'txtMultBmc.arrValueMember = Nothing
        'txtMultDCS.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date: " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmLastDCSCollectionItem & "'"))
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid(Me.Text, Gv1, arrHeader, Me.Text)
            common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
End Class
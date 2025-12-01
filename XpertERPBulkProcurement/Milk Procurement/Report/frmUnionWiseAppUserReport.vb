Imports common
Imports System
Imports System.Text
Public Class frmUnionWiseAppUserReport
    Private Sub frmUnionWiseAppUserReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        BlankGrid()
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisable(True)
    End Sub

    Sub BlankGrid()
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        gv.GroupDescriptors.Clear()
        gv.MasterView.Refresh()
        gv.GroupDescriptors.Clear()
        gv.EnableFiltering = False
        gv.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Sub EnableDisable(ByVal isEnable As Boolean)
        RadGroupBox1.Enabled = isEnable
        gv.ReadOnly = Not isEnable
    End Sub

    Private Sub txtUnion__My_Click(sender As Object, e As EventArgs) Handles txtUnion._My_Click
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim qry As String = ReturnForAllUnion()
            txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("UnionAppUser", qry, "DataBase Name", "", txtUnion.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function ReturnForAllUnion()
        Dim strQry As String = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] Where Union_Report=1 "
        If Not objCommonVar.RCDFCFP Then
            strQry += " And [TSPL_APP_LOCATION].DataBase_Name='" & objCommonVar.CurrDatabase & "'"
        End If
        Return strQry
    End Function

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(ReturnQry())
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                BlankGrid()
                gv.DataSource = dt
                gv.BestFitColumns()
                EnableDisable(False)
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function ReturnQry() As String
        Dim finalBaseQry As String = Nothing
        Try
            Dim sbQry As New StringBuilder()
            Dim strQry As String = ReturnForAllUnion()
            If txtUnion.arrValueMember IsNot Nothing Then
                strQry += " And DataBase_Name In (" & clsCommon.GetMulcallString(txtUnion.arrValueMember) & ") "
            End If
            strQry += " Order By [TSPL_APP_LOCATION].Location_Name"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each strUnion As DataRow In dt.Rows
                    If clsCommon.myLen(sbQry) <> 0 Then
                        sbQry.Append(Environment.NewLine & " Union All " & Environment.NewLine)
                    End If
                    sbQry.Append(" Select [Union Name],COUNT(Distinct Cust_Code) As [User Count],Sum(TotalLtr_ItemWise) As [Qty(Ltr)],SUM([Qty(KG)]) As [Qty(KG)]   " &
                         " from (Select '" & strUnion("Location") & "' As [Union Name],TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_DEMAND_BOOKING_MASTER.ItemType,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Is_FreshItem,TSPL_ITEM_MASTER.Is_Ambient,TSPL_DEMAND_BOOKING_DETAIL.Qty,TSPL_DEMAND_BOOKING_DETAIL.Unit_code," &
" Case When TSPL_ITEM_MASTER.Is_FreshItem=1 And TSPL_ITEM_MASTER.IsTaxable=0 Then TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise Else 0 End As TotalLtr_ItemWise,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL_LTR.Conversion_Factor As CFinLTR,TSPL_ITEM_UOM_DETAIL_KG.Conversion_Factor As CFinKG,case when TSPL_ITEM_MASTER.Is_Ambient=1 OR (TSPL_ITEM_MASTER.Is_FreshItem=1 And TSPL_ITEM_MASTER.IsTaxable=1) then CONVERT(decimal(18,2),(Qty * TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/ TSPL_ITEM_UOM_DETAIL_KG.Conversion_Factor ) else 0 end As [Qty(KG)] " &
" from [" & strUnion("DataBase Name") & "].dbo.TSPL_DEMAND_BOOKING_DETAIL " &
" Left Outer Join [" & strUnion("DataBase Name") & "].dbo.TSPL_DEMAND_BOOKING_MASTER On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No " &
" Left Outer Join [" & strUnion("DataBase Name") & "].dbo.TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code " &
" Left Outer Join [" & strUnion("DataBase Name") & "].dbo.TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code And TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code " &
" Left Outer Join [" & strUnion("DataBase Name") & "].dbo.TSPL_ITEM_UOM_DETAIL As TSPL_ITEM_UOM_DETAIL_LTR On TSPL_ITEM_UOM_DETAIL_LTR.Item_Code=TSPL_ITEM_MASTER.Item_Code And TSPL_ITEM_UOM_DETAIL_LTR.UOM_Code='LTR' " &
" Left Outer Join [" & strUnion("DataBase Name") & "].dbo.TSPL_ITEM_UOM_DETAIL As TSPL_ITEM_UOM_DETAIL_KG On TSPL_ITEM_UOM_DETAIL_KG.Item_Code=TSPL_ITEM_MASTER.Item_Code And TSPL_ITEM_UOM_DETAIL_KG.UOM_Code='KG' " &
" where  IsNull(TSPL_DEMAND_BOOKING_DETAIL.created_by,'') <>'' And Convert(Date,Document_Date,103)=Convert(date,'" & txtFromDate.Value & "',103) " &
" Union All " &
" Select '" & strUnion("Location") & "' As [Union Name],TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_PRODUCT_DEMAND_BOOKING_MASTER.ItemType,TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Is_FreshItem,TSPL_ITEM_MASTER.Is_Ambient,TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Qty,TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Unit_code,
Case When TSPL_ITEM_MASTER.Is_FreshItem=1 And TSPL_ITEM_MASTER.IsTaxable=0 Then TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise Else 0 End As TotalLtr_ItemWise,
TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL_LTR.Conversion_Factor As CFinLTR,TSPL_ITEM_UOM_DETAIL_KG.Conversion_Factor As CFinKG,
case when TSPL_ITEM_MASTER.Is_Ambient=1 OR (TSPL_ITEM_MASTER.Is_FreshItem=1 And TSPL_ITEM_MASTER.IsTaxable=1) then CONVERT(decimal(18,2),(Qty * TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/ TSPL_ITEM_UOM_DETAIL_KG.Conversion_Factor ) else 0 end As [Qty(KG)]  
from [JPRTEST].dbo.TSPL_PRODUCT_DEMAND_BOOKING_DETAIL   
Left Outer Join [" & strUnion("DataBase Name") & "].dbo.TSPL_PRODUCT_DEMAND_BOOKING_MASTER On TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_No=TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Document_No  
Left Outer Join [" & strUnion("DataBase Name") & "].dbo.TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code=TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code  
Left Outer Join [" & strUnion("DataBase Name") & "].dbo.TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code And TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Unit_code  
Left Outer Join [" & strUnion("DataBase Name") & "].dbo.TSPL_ITEM_UOM_DETAIL As TSPL_ITEM_UOM_DETAIL_LTR On TSPL_ITEM_UOM_DETAIL_LTR.Item_Code=TSPL_ITEM_MASTER.Item_Code And TSPL_ITEM_UOM_DETAIL_LTR.UOM_Code='LTR'  
Left Outer Join [" & strUnion("DataBase Name") & "].dbo.TSPL_ITEM_UOM_DETAIL As TSPL_ITEM_UOM_DETAIL_KG On TSPL_ITEM_UOM_DETAIL_KG.Item_Code=TSPL_ITEM_MASTER.Item_Code And TSPL_ITEM_UOM_DETAIL_KG.UOM_Code='KG'  
where  IsNull(TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.created_by,'') <>'' And Convert(Date,Document_Date,103)=Convert(date,'" & txtFromDate.Value & "',103) " &
" Union All " &
" Select '" & strUnion("Location") & "' As [Union Name],Null As Cust_Code,Null As ItemType,Null As Item_Code,Null As Item_Desc,0 As Is_FreshItem,0 As Is_Ambient,0 As Qty,Null As Unit_code,0 As TotalLtr_ItemWise,0 As Conversion_Factor,0 As CFinLTR,0 As CFinKG,0 As [Qty(KG)] " &
" )BaseQry " &
" Group By [Union Name] ")
                Next
                finalBaseQry = "Select ROW_NUMBER() Over(Order By (Select 1)) As [S.No.],finalBase.* from (" & clsCommon.myCstr(sbQry) & ")finalBase"
            Else
                Throw New Exception("Data not found !")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return finalBaseQry
    End Function

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        ExportExcelorPDF(True)
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        ExportExcelorPDF(False)
    End Sub

    Sub ExportExcelorPDF(ByVal isExcelPDF As Boolean)
        Try
            If isExcelPDF Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Doc Date : " & clsCommon.myCstr(clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy")))
                clsCommon.MyExportToExcelGrid("Union Wise App User Report", gv, arrHeader, "Union Wise App User Report")
                arrHeader = Nothing
            Else
                Dim doc As New XpertERPEngine.clsMyPrintDocument()
                doc.Margins.Top = 50
                doc.Margins.Bottom = 50
                doc.Margins.Left = 50
                doc.Margins.Right = 50
                doc.HeaderHeight = 90
                doc.Landscape = True
                doc.AssociatedObject = gv

                Dim strHeader As String = "Union Wise App User Report"
                Dim strHeader2 As String = "Date : " & clsCommon.myCstr(clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy"))
                doc.LeftUpperText = strHeader
                doc.LeftHeader = strHeader2
                doc.LeftUpperFont = New Font("Arial", 14, FontStyle.Bold)
                doc.HeaderFont = New Font("Arial", 14, FontStyle.Bold)
                doc.AssociatedObject = gv
                doc.Print()
                doc = Nothing
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim strQry As String = "Select finalQry.*,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_STATE_MASTER.STATE_NAME from (" & ReturnQry() & ")finalQry Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" & objCommonVar.CurrComp_Code1 & "' Left Outer Join TSPL_STATE_MASTER On TSPL_STATE_MASTER.STATE_CODE=TSPL_COMPANY_MASTER.State "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frm As New frmCrystalReportViewer()
                frm.funreport(Form_ID, CrystalReportFolder.UnionReports, dt, "crptUnionWiseAppUserReport", "Union Wise Saras Mobile App User Report", Nothing)
                frm = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
End Class
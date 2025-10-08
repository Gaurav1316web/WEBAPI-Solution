Imports System.Text
Imports common
Public Class frmRouteWiseSaleTargetReport
    Private Sub frmRouteWiseSaleTargetReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtToDate.Value = clsCommon.GETSERVERDATE()
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
            EnableDisableFields(True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.GroupDescriptors.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.MasterView.Refresh()
    End Sub

    Sub EnableDisableFields(ByVal isEnable As Boolean)
        txtToDate.Enabled = isEnable
        If isEnable Then
            RadPageView1.SelectedPage = RadPageViewPage1
        Else
            RadPageView1.SelectedPage = RadPageViewPage2
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim strQry As String = ReturnQry(False)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.MasterView.Refresh()
                Gv1.DataSource = dt
                Gv1.EnableFiltering = True
                Gv1.AllowAddNewRow = False
                Gv1.AllowDragToGroup = False
                Gv1.ReadOnly = True
                EnableDisableFields(False)

                For rowI As Integer = 0 To Gv1.Rows.Count - 1
                    For colI As Integer = 2 To Gv1.Columns.Count - 1
                        Dim cellValue As Object = Gv1.Rows(rowI).Cells(Gv1.Columns(colI).Name).Value
                        If cellValue IsNot Nothing AndAlso IsNumeric(cellValue) Then
                            Dim col As GridViewDecimalColumn = TryCast(Gv1.Columns(Gv1.Columns(colI).Name), GridViewDecimalColumn)
                            If col IsNot Nothing Then
                                col.FormatString = "{0:N0}"
                            End If
                            col = Nothing
                        End If
                    Next
                Next
                Gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Function ReturnQry(ByVal isPrint As Boolean) As String
        Dim strQry As String
        Try
            strQry = "Select Chapter_Head_Code,MAX(Description)Description,UOM_Code from(
select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,tspl_chapter_head.Chapter_Head_Code,tspl_chapter_head.Description,TSPL_ITEM_UOM_DETAIL.UOM_Code from tspl_chapter_head
Left Outer Join TSPL_ROUTE_WISE_SALE_TARGET On TSPL_ROUTE_WISE_SALE_TARGET.Item_Sub_Category=tspl_chapter_head.Chapter_Head_Code
Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Sub_Group_Type=TSPL_ROUTE_WISE_SALE_TARGET.Item_Sub_Category
Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code
Where TSPL_ITEM_UOM_DETAIL.Default_UOM=1)ItemUOMDetails Group By Chapter_Head_Code,UOM_Code"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim sbLTR As New StringBuilder()
                Dim sbKG As New StringBuilder()
                Dim strItemType As New StringBuilder()
                Dim i As Integer = 0
                For Each UOM In dt.Rows
                    If clsCommon.myLen(UOM("Chapter_Head_Code")) <> 0 AndAlso clsCommon.CompairString(UOM("UOM_Code"), "LTR") = CompairStringResult.Equal Then
                        If clsCommon.myLen(sbLTR) > 0 Then
                            sbLTR.Append(",")
                        End If
                        sbLTR.Append("'" & clsCommon.myCstr(UOM("Chapter_Head_Code")) & "'")
                    End If
                    If clsCommon.myLen(UOM("Chapter_Head_Code")) <> 0 AndAlso clsCommon.CompairString(UOM("UOM_Code"), "KG") = CompairStringResult.Equal Then
                        If clsCommon.myLen(sbKG) > 0 Then
                            sbKG.Append(",")
                        End If
                        sbKG.Append("'" & clsCommon.myCstr(UOM("Chapter_Head_Code")) & "'")
                    End If
                    If isPrint Then
                        If clsCommon.myLen(UOM("Description")) > 0 Then
                            strItemType.Append(",SUM([" & clsCommon.myCstr(UOM("Description")) & "]) As [Group" & clsCommon.myCstr(i + 1) & "]")
                        End If
                    Else
                        If clsCommon.myLen(UOM("Description")) > 0 Then
                            strItemType.Append(",SUM([" & clsCommon.myCstr(UOM("Description")) & "]) As [" & clsCommon.myCstr(UOM("Description")) & "]")
                        End If
                    End If
                Next


                strQry = Nothing
                strQry = ";WITH DetailData AS
(SELECT TSPL_DEMAND_BOOKING_MASTER.Document_No,TSPL_DEMAND_BOOKING_MASTER.Document_Date,TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,
TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc,
TSPL_CUSTOMER_GROUP_MASTER.IsGoverment,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,
TSPL_DEMAND_BOOKING_DETAIL.Qty,TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_ITEM_UOM_DETAIL.Default_UOM,
TSPL_ITEM_UOM_DETAIL.UOM_Code As [Default_UOM_Code],TSPL_ITEM_MASTER.Item_Sub_Group_Type,tspl_chapter_head.Description,
TSPL_ITEM_UOM_DETAIL.Conversion_Factor,
TSPL_ITEM_UOM_DETAIL_IN_LTR.Conversion_Factor As [CFinLTR],TSPL_ITEM_UOM_DETAIL_IN_KG.Conversion_Factor As [CFinKG],"

                If clsCommon.myLen(sbLTR) > 0 Then
                    strQry += " Case When TSPL_ITEM_MASTER.Item_Sub_Group_Type IN (" & clsCommon.myCstr(sbLTR) & ") Then (TSPL_DEMAND_BOOKING_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor/TSPL_ITEM_UOM_DETAIL_IN_LTR.Conversion_Factor) Else 0 End As QtyInLTR "
                End If
                If clsCommon.myLen(sbKG) > 0 Then
                    strQry += " ,Case When TSPL_ITEM_MASTER.Item_Sub_Group_Type IN (" & clsCommon.myCstr(sbKG) & ") Then (TSPL_DEMAND_BOOKING_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor/TSPL_ITEM_UOM_DETAIL_IN_KG.Conversion_Factor) Else 0 End As QtyInKG "
                End If

                strQry += ",TSPL_ROUTE_WISE_SALE_TARGET.Months,IsNull(TSPL_ROUTE_WISE_SALE_TARGET.Target_Qty,0)Target_Qty FROM TSPL_DEMAND_BOOKING_DETAIL
Left Outer Join TSPL_DEMAND_BOOKING_MASTER On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
Left Outer Join TSPL_CUSTOMER_GROUP_MASTER On TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code

LEFT Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code

Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code 
				And TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code
				--And TSPL_ITEM_UOM_DETAIL.Default_UOM=1

Left Outer Join TSPL_ITEM_UOM_DETAIL As TSPL_ITEM_UOM_DETAIL_IN_LTR On TSPL_ITEM_UOM_DETAIL_IN_LTR.Item_Code=TSPL_ITEM_MASTER.Item_Code 				
				And TSPL_ITEM_UOM_DETAIL_IN_LTR.UOM_Code='LTR'
Left Outer Join TSPL_ITEM_UOM_DETAIL As TSPL_ITEM_UOM_DETAIL_IN_KG On TSPL_ITEM_UOM_DETAIL_IN_KG.Item_Code=TSPL_ITEM_MASTER.Item_Code 
				And TSPL_ITEM_UOM_DETAIL_IN_KG.UOM_Code='KG'

Left Outer Join tspl_chapter_head On tspl_chapter_head.Chapter_Head_Code=TSPL_ITEM_MASTER.Item_Sub_Group_Type
Left Outer Join TSPL_ROUTE_MASTER On TSPL_ROUTE_MASTER.Route_No=TSPL_DEMAND_BOOKING_MASTER.Route_No

Left Outer Join (Select TSPL_ROUTE_WISE_SALE_TARGET.Months,TSPL_ROUTE_WISE_SALE_TARGET.Item_Sub_Category,TSPL_ROUTE_WISE_SALE_TARGET_DETAIL.Route_Code,
TSPL_ROUTE_WISE_SALE_TARGET_DETAIL.Cust_Group_Code,IsNull(TSPL_ROUTE_WISE_SALE_TARGET_DETAIL.Target_Qty,0) AS Target_Qty 
from TSPL_ROUTE_WISE_SALE_TARGET_DETAIL
Left Outer Join TSPL_ROUTE_WISE_SALE_TARGET  On TSPL_ROUTE_WISE_SALE_TARGET.Document_Code=TSPL_ROUTE_WISE_SALE_TARGET_DETAIL.Document_Code 
Where ISNULL(TSPL_ROUTE_WISE_SALE_TARGET_DETAIL.Route_Code, '') <> '' And TSPL_ROUTE_WISE_SALE_TARGET.Status=1
Union All
Select TSPL_ROUTE_WISE_SALE_TARGET.Months,TSPL_ROUTE_WISE_SALE_TARGET.Item_Sub_Category,TSPL_ROUTE_WISE_SALE_TARGET_DETAIL.Route_Code,
TSPL_ROUTE_WISE_SALE_TARGET_DETAIL.Cust_Group_Code,IsNull(TSPL_ROUTE_WISE_SALE_TARGET_DETAIL.Target_Qty,0) AS Target_Qty 
from TSPL_ROUTE_WISE_SALE_TARGET_DETAIL
Left Outer Join TSPL_ROUTE_WISE_SALE_TARGET  On TSPL_ROUTE_WISE_SALE_TARGET.Document_Code=TSPL_ROUTE_WISE_SALE_TARGET_DETAIL.Document_Code 
Where ISNULL(TSPL_ROUTE_WISE_SALE_TARGET_DETAIL.Cust_Group_Code, '') <> '' And TSPL_ROUTE_WISE_SALE_TARGET.Status=1

)TSPL_ROUTE_WISE_SALE_TARGET On TSPL_ROUTE_WISE_SALE_TARGET.Item_Sub_Category=TSPL_ITEM_MASTER.Item_Sub_Group_Type 
And (
        (ISNULL(TSPL_ROUTE_WISE_SALE_TARGET.Route_Code, '') <> '' 
            AND TSPL_ROUTE_WISE_SALE_TARGET.Route_Code = TSPL_ROUTE_MASTER.Route_No)
        OR
        (ISNULL(TSPL_ROUTE_WISE_SALE_TARGET.Route_Code, '') = '' 
            AND TSPL_ROUTE_WISE_SALE_TARGET.Cust_Group_Code = TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code)
    )

where 2=2  and TSPL_DEMAND_BOOKING_MASTER.Document_Date >= '01/" & clsCommon.GetPrintDate(txtToDate.Value, "MMM/yyyy") & "' And TSPL_DEMAND_BOOKING_MASTER.Document_Date <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'
--and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code='ARMY U'
And IsNull(TSPL_ROUTE_WISE_SALE_TARGET.Target_Qty,0)<>0
)

Select Code,MAX(Description)Description " & clsCommon.myCstr(strItemType) & ",SUM(Target_Qty)[TGT MILK],SUM([ACHV MILK])[ACHV MILK],Case When Sum(Target_Qty)<>0 Then Round(((SUM([ACHV MILK])/SUM(Target_Qty))*100),0) Else 0 End As 'PROGRESS' from (
Select 
DetailData.Route_No As Code,MAX(Route_Desc) As Description, "

                For Each strUOM In dt.Rows
                    strQry += " Sum(QtyIn" & clsCommon.myCstr(strUOM("UOM_Code")) & ")*Case When Max(DetailData.Document_Date)='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' And Item_Sub_Group_Type='" & clsCommon.myCstr(strUOM("Chapter_Head_Code")) & "' Then 1 Else 0 End As '" & clsCommon.myCstr(strUOM("Description")) & "',"
                Next
                strQry += " MAX(DetailData.Months)Months,
IsNull(Max(DetailData.Target_Qty),0)Target_Qty,
(Sum(QtyInLTR)*Case When Max(DetailData.Document_Date)<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' And Item_Sub_Group_Type='MILK' Then 1 Else 0 End)/DAY('" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "') As 'ACHV MILK'
from DetailData 
Where DetailData.IsGoverment<>1 
Group BY DetailData.Route_No,Item_Sub_Group_Type

Union All

Select 
DetailData.Cust_Group_Code,MAX(Cust_Group_Desc)As Description,"

                For Each strUOM In dt.Rows
                    strQry += " Sum(QtyIn" & clsCommon.myCstr(strUOM("UOM_Code")) & ")*Case When Max(DetailData.Document_Date)='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' And Item_Sub_Group_Type='" & clsCommon.myCstr(strUOM("Chapter_Head_Code")) & "' Then 1 Else 0 End As '" & clsCommon.myCstr(strUOM("Description")) & "',"
                Next
                strQry += " MAX(DetailData.Months)Months,
IsNull(Max(DetailData.Target_Qty),0)Target_Qty,
(Sum(QtyInLTR)*Case When Max(DetailData.Document_Date)<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' And Item_Sub_Group_Type='MILK' Then 1 Else 0 End)/DAY('" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "') As 'ACHV MILK'
from DetailData 
Where IsGoverment=1
Group BY DetailData.Cust_Group_Code,Item_Sub_Group_Type
)xyz Group By Code

Union All

Select '' As Code,Max(Description)Description ,SUM([MILK]) As [MILK],SUM(Target_Qty)Target_Qty,SUM([ACHV MILK])[ACHV MILK],
Case When Sum(Target_Qty)<>0 Then Round(((SUM([ACHV MILK])/SUM(Target_Qty))*100),0) Else 0 End As 'PROGRESS' 
from (
Select 
DetailData.Route_No As Code,'CITY (LTR)' As Description, "
                For Each strUOM In dt.Rows
                    strQry += " Sum(QtyIn" & clsCommon.myCstr(strUOM("UOM_Code")) & ")*Case When Max(DetailData.Document_Date)='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' And Max(Item_Sub_Group_Type)='" & clsCommon.myCstr(strUOM("Chapter_Head_Code")) & "' Then 1 Else 0 End As '" & clsCommon.myCstr(strUOM("Description")) & "',"
                Next
                strQry += "MAX(DetailData.Months)Months,
IsNull(Max(DetailData.Target_Qty),0)Target_Qty,
(Sum(QtyInLTR)*Case When Max(DetailData.Document_Date)<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' And Item_Sub_Group_Type='MILK' Then 1 Else 0 End)/DAY('" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "') As 'ACHV MILK'

from DetailData 
Where DetailData.IsGoverment<>1 
Group BY DetailData.Route_No,Item_Sub_Group_Type --Having Sum(Target_Qty)<>0

Union All

Select '' As Code,Description,SUM(MILK)MILK,MAX(Months)Months,SUM(Target_Qty)Target_Qty,SUm([ACHV MILK])[ACHV MILK]  from (
Select 
MAx(DetailData.Route_No) As Code,'TOTAL (LTR)' As Description,  "
                For Each strUOM In dt.Rows
                    strQry += " Sum(QtyIn" & clsCommon.myCstr(strUOM("UOM_Code")) & ")*Case When Max(DetailData.Document_Date)='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' And Max(Item_Sub_Group_Type)='" & clsCommon.myCstr(strUOM("Chapter_Head_Code")) & "' Then 1 Else 0 End As '" & clsCommon.myCstr(strUOM("Description")) & "',"
                Next
                strQry += "MAX(DetailData.Months)Months,
IsNull(Max(DetailData.Target_Qty),0)Target_Qty,
(Sum(QtyInLTR)*Case When Max(DetailData.Document_Date)<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' And Max(Item_Sub_Group_Type)='MILK' Then 1 Else 0 End)/DAY('" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "') As 'ACHV MILK'

from DetailData 
Group BY DetailData.Target_Qty 
)AllTotal Group By Description
)TotalWise Group By Description "
            Else
                clsCommon.MyMessageBoxShow(Me, "Item sub group type not found !", Me.Text)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return strQry
    End Function

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim strQry As String = ReturnQry(True)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frm As New frmCrystalReportViewer()
                frm.funreport(Form_ID, CrystalReportFolder.SalesReport, dt, "crptRouteWiseSaleTargetReport", "Route Wise Sale Target Report")
                frm = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
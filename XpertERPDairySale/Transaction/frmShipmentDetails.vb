Imports common

Public Class frmShipmentDetails
#Region "Variables"
    Public strFromDate As DateTime? = Nothing
    Public strToDate As DateTime? = Nothing
    Public strFromShift As String = ""
    Public strToshift As String = ""
    Public strCustCode As String = ""
    Public strItemType As String = ""
    Public strRouteNo As String = ""
    Public strLocation As String = ""

    Private Sub frmShipmentDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        LoadData()
    End Sub
#End Region
    Private Sub LoadData()
        Try
            Dim WhrCls As String = ""
            Dim FromShift As String = ""
            Dim ToShift As String = ""
            If clsCommon.CompairString(strFromShift, "E") = CompairStringResult.Equal Then
                FromShift = "PM"
            ElseIf clsCommon.CompairString(strFromShift, "M") = CompairStringResult.Equal Then
                FromShift = "AM"
            End If
            If clsCommon.CompairString(strToshift, "E") = CompairStringResult.Equal Then
                ToShift = "PM"
            ElseIf clsCommon.CompairString(strToshift, "M") = CompairStringResult.Equal Then
                ToShift = "AM"
            End If
            Dim Qry As String = "select TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE as [Shipment No],max(TSPL_SD_SHIPMENT_HEAD.Supply_Date) as [Supply Date],max(isnull(TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode,'')) as [GP Code],max(TSPL_SD_SHIPMENT_HEAD.Total_Amt) as [Total Amt]
from TSPL_SD_SHIPMENT_HEAD
left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE=TSPL_SD_SHIPMENT_HEAD.Document_Code
left join TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL on TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID=TSPL_SD_SHIPMENT_DETAIL.PK_ID where 2=2 "
            Qry += "  and TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No='' "
            Qry += " and CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(strFromDate, "dd/MMM/yyyy") + "' AND '" + clsCommon.GetPrintDate(strToDate, "dd/MMM/yyyy") + "' "
            If clsCommon.CompairString(strFromShift, "E") = CompairStringResult.Equal AndAlso clsCommon.CompairString(strToshift, "E") = CompairStringResult.Equal Then
                Qry += " AND ( (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) = '" + clsCommon.GetPrintDate(strFromDate, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_HEAD.Shift_Type = '" + FromShift + "') OR
        (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) > '" + clsCommon.GetPrintDate(strFromDate, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) <= '" + clsCommon.GetPrintDate(strToDate, "dd/MMM/yyyy") + "')) "

            ElseIf clsCommon.CompairString(strFromShift, "M") = CompairStringResult.Equal AndAlso clsCommon.CompairString(strToshift, "M") = CompairStringResult.Equal Then
                Qry += " AND ( 
        (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) > '" + clsCommon.GetPrintDate(strFromDate, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) < '" + clsCommon.GetPrintDate(strToDate, "dd/MMM/yyyy") + "')  
            or (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) = '" + clsCommon.GetPrintDate(strToDate, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_HEAD.Shift_Type = '" + ToShift + "') ) "

            ElseIf clsCommon.CompairString(strFromShift, "E") = CompairStringResult.Equal AndAlso clsCommon.CompairString(strToshift, "M") = CompairStringResult.Equal Then
                Qry += " AND ( (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) = '" + clsCommon.GetPrintDate(strFromDate, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_HEAD.Shift_Type = '" + FromShift + "') OR
        (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) > '" + clsCommon.GetPrintDate(strFromDate, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) < '" + clsCommon.GetPrintDate(strToDate, "dd/MMM/yyyy") + "')  
            or (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) = '" + clsCommon.GetPrintDate(strToDate, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_HEAD.Shift_Type = '" + ToShift + "') ) "
            End If

            Qry += " and TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='" + strItemType + "' and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" + strLocation + "' "

            Qry += " and TSPL_SD_SHIPMENT_HEAD.Customer_Code ='" + clsCommon.myCstr(strCustCode) + "' and TSPL_SD_SHIPMENT_HEAD.Route_No='" + strRouteNo + "'"
            Qry += "  group by TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.GroupDescriptors.Clear()
                gv1.ShowGroupPanel = False
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.EnableFiltering = True
                gv1.AllowAddNewRow = False
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                    gv1.Columns(ii).IsVisible = True
                Next
                gv1.BestFitColumns()

            Else
                Throw New Exception("Data Not Found!")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
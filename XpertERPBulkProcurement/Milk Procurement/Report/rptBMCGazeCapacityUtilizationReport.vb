Imports common
Imports System.IO
Imports System.Net.Mail
Imports System.Net.Mime
Public Class rptBMCGazeCapacityUtilizationReport
    Inherits FrmMainTranScreen

    Private Sub rptBMCGazeCapacityUtilizationReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub txtFromDate_ValueChanged(sender As Object, e As EventArgs) Handles txtFromDate.ValueChanged

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print(False)
    End Sub
    Sub Print(ByVal isPrint As Boolean)
        Try
            Dim qry As String = " Select VLC_Code_VLC_Uploader,MAX(XX.VLC_Name)VLC_Name,Max(TSPL_Silo_Detail.Silo_Area)Capacity,
   sum(GazeQty)GazeQty
  
  ,max(xx.UTILISATION)UTILISATION,max(xx.Zone_Code)Zone_Code,max(xx.Contained_Qty)Contained_Qty 
  
  from (SELECT MCC_Code,VLC_Name, VLC_Code_VLC_Uploader, 
TSPL_MILK_COLLECTION_MCC_DETAIL.Gaze_Qty as GazeQty,

Format(ROUND(( (TSPL_MILK_COLLECTION_MCC_DETAIL.Gaze_Qty/TSPL_WEIGHT_CONVERSION.Contained_Qty) /Silo_Capacity ) * 100, 2),'0.00') as  UTILISATION,
TSPL_VENDOR_MASTER.Zone_Code,TSPL_WEIGHT_CONVERSION.Contained_Qty
FROM TSPL_MILK_COLLECTION_MCC_DETAIL 
left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.MCC=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code
left outer join TSPL_VENDOR_MASTER on  TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code
left outer join TSPL_ZONE_MASTER on  TSPL_ZONE_MASTER.zone_code = TSPL_VENDOR_MASTER.Zone_Code 
left Outer Join  TSPL_WEIGHT_CONVERSION On  TSPL_WEIGHT_CONVERSION.Contained_UOM='KG'

where isOwnBMC ='1' and  convert(date,TSPL_MILK_COLLECTION_MCC.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103) and convert(date,TSPL_MILK_COLLECTION_MCC.Document_Date,103) <=convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' ,103)
 "

            If clsCommon.myLen(fndMcc.Value) > 0 Then
                qry += " And MCC_Code = '" + fndMcc.Value + "' "
            End If
            If clsCommon.myLen(fndZone.Value) > 0 Then
                qry += " And TSPL_VENDOR_MASTER.Zone_Code = '" + fndZone.Value + "' "
            End If
            qry += " )xx  left outer join (Select Trans_Code,Sum(Silo_Area)Silo_Area from TSPL_Silo_Detail "
            If clsCommon.myLen(fndMcc.Value) > 0 Then
                qry += " Where MCC_Code = '" + fndMcc.Value + "' "
            End If
            qry += " Group By Trans_Code )TSPL_Silo_Detail on TSPL_Silo_Detail.Trans_Code=MCC_Code group by VLC_Code_VLC_Uploader,MCC_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            'Dt  clsDBFuncationality.GetDataTable(qry)
            If (Dt IsNot Nothing AndAlso Dt.Rows.Count > 0) Then
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.MasterView.Refresh()
                Gv1.DataSource = Dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                    'Gv.Rows.Add()
                Next

                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
                SetGridFormat()
                Gv1.BestFitColumns()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        
    End Sub

    Sub SetGridFormat()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            'gv1.Columns("Document_No").HeaderText = "Document No."
            Gv1.Columns("VLC_Name").IsVisible = True
            Gv1.Columns("VLC_Name").HeaderText = "DCS Name"
            Gv1.Columns("VLC_Code_VLC_Uploader").IsVisible = True
            Gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS Code"
            Gv1.Columns("Capacity").IsVisible = True
            Gv1.Columns("Capacity").HeaderText = "Total Capacity (Litre)"
            'Gv1.Columns("QtyKG").IsVisible = False
            'Gv1.Columns("QtyKG").HeaderText = "Qty (KG)"
            'Gv1.Columns("QtyLTR").IsVisible = True
            'Gv1.Columns("QtyLTR").HeaderText = "Qty (LTR)"
            Gv1.Columns("GazeQty").IsVisible = True
            Gv1.Columns("GazeQty").HeaderText = "Gaze Qty"
            Gv1.Columns("UTILISATION").IsVisible = True
            Gv1.Columns("UTILISATION").HeaderText = "% UTILISATION"
            Gv1.Columns("Zone_Code").IsVisible = True
            Gv1.Columns("Zone_Code").HeaderText = "Zone"
            Gv1.Columns("Contained_Qty").IsVisible = False
            Gv1.Columns("Contained_Qty").HeaderText = "Contained_Qty"
        Next
        Dim summaryRowItemB As New GridViewSummaryRowItem()
        'Dim MilkTypeB As New GridViewSummaryItem("Payable_Amount", "{0:n0}", GridAggregateFunction.Sum)

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub

    Private Sub fndZone__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndZone._MYValidating


        Dim qry As String = " select TSPL_VENDOR_MASTER.Zone_Code as Code ,TSPL_VLC_MASTER_HEAD.mcc as MCC from TSPL_ZONE_MASTER
 left outer join TSPL_VENDOR_MASTER on  TSPL_VENDOR_MASTER.zone_code = TSPL_ZONE_MASTER.Zone_Code 
 Left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code"
        Dim WhrCls As String = " mcc ='" + fndMcc.Value + "'"

        fndZone.Value = clsCommon.ShowSelectForm("Zone", qry, "Code", WhrCls, fndZone.Value, "Code", isButtonClicked)

    End Sub

    Private Sub fndMcc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndMcc._MYValidating
        ''Dim whrClas As String = ""

        Dim qry As String = "select MCC_Code as Code,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"
        Dim whrClas As String = ""

        fndMcc.Value = clsCommon.ShowSelectForm("VMasterFND", qry, "Code", whrClas, fndMcc.Value, "Code", isButtonClicked)


    End Sub
End Class
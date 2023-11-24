Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class rptDCSSaleRegister
    Inherits FrmMainTranScreen


    Private Sub rptDCSSaleRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtBilltoLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtBilltoLocation.Value) > 0 Then
            lblBilltoLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBilltoLocation.Value + "' "))
        End If

    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptItemConsumptionReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'rmExportToExcel.Visible = MyBase.isExport
        btnSplitExport.Visible = MyBase.isExport
    End Sub


    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        txtBilltoLocation.Value = Nothing
        lblBilltoLocation.Text = ""
        txtDCS.Value = ""
        txtDocumentNo.Value = ""
        lblDCS.Text = ""
        lblDocumentNo.Text = ""
        Gv1.DataSource = Nothing
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub FormatGrid()

        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next
        Gv1.Columns("SNo").Width = 50
        Gv1.Columns("SNo").IsVisible = True
        Gv1.Columns("SNo").HeaderText = "S.No"

        Gv1.Columns("Bill_Date").Width = 100
        Gv1.Columns("Bill_Date").IsVisible = True
        Gv1.Columns("Bill_Date").HeaderText = "Bill Date"

        Gv1.Columns("Bill").Width = 100
        Gv1.Columns("Bill").IsVisible = True
        Gv1.Columns("Bill").HeaderText = "Bill No"

        Gv1.Columns("Dispatch_Date").Width = 100
        Gv1.Columns("Dispatch_Date").IsVisible = True
        Gv1.Columns("Dispatch_Date").HeaderText = "Dispatch Date"

        Gv1.Columns("DCS_Name").Width = 100
        Gv1.Columns("DCS_Name").IsVisible = True
        Gv1.Columns("DCS_Name").HeaderText = "DCS Name"

        Gv1.Columns("C_No").Width = 100
        Gv1.Columns("C_No").IsVisible = True
        Gv1.Columns("C_No").HeaderText = "C No"

        Gv1.Columns("GRNO").Width = 100
        Gv1.Columns("GRNO").IsVisible = True
        Gv1.Columns("GRNO").HeaderText = "GRNO"

        Gv1.Columns("Zone").Width = 100
        Gv1.Columns("Zone").IsVisible = True
        Gv1.Columns("Zone").HeaderText = "Zone"

        Gv1.Columns("SADA").Width = 80
        Gv1.Columns("SADA").IsVisible = True
        Gv1.Columns("SADA").HeaderText = "SADA"

        Gv1.Columns("GOLD").Width = 80
        Gv1.Columns("GOLD").IsVisible = True
        Gv1.Columns("GOLD").HeaderText = "GOLD"

        Gv1.Columns("BPP").Width = 80
        Gv1.Columns("BPP").IsVisible = True
        Gv1.Columns("BPP").HeaderText = "BPP"

        Gv1.Columns("TruckNo").Width = 120
        Gv1.Columns("TruckNo").IsVisible = True
        Gv1.Columns("TruckNo").HeaderText = "Truck No"

        Gv1.Columns("ChallanNo").Width = 100
        Gv1.Columns("ChallanNo").IsVisible = True
        Gv1.Columns("ChallanNo").HeaderText = "Challan No"

        Gv1.Columns("Bags").Width = 100
        Gv1.Columns("Bags").IsVisible = True
        Gv1.Columns("Bags").HeaderText = "Bags"

        Gv1.Columns("MT").Width = 100
        Gv1.Columns("MT").IsVisible = True
        Gv1.Columns("MT").HeaderText = "MT"

        Gv1.Columns("CF_AMT").Width = 150
        Gv1.Columns("CF_AMT").IsVisible = True
        Gv1.Columns("CF_AMT").HeaderText = "CF Amount"

        Gv1.Columns("Frieght").Width = 100
        Gv1.Columns("Frieght").IsVisible = True
        Gv1.Columns("Frieght").HeaderText = "Frieght"

        Gv1.Columns("Frieght_Amt").Width = 150
        Gv1.Columns("Frieght_Amt").IsVisible = True
        Gv1.Columns("Frieght_Amt").HeaderText = "Frieght Amt"

        Gv1.Columns("Total_Amt").Width = 150
        Gv1.Columns("Total_Amt").IsVisible = True
        Gv1.Columns("Total_Amt").HeaderText = "Total Amt"



        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item1 As New GridViewSummaryItem("SADA", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("GOLD", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("BPP", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("Bags", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Dim item5 As New GridViewSummaryItem("MT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)

        Dim item6 As New GridViewSummaryItem("CF_AMT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        Dim item7 As New GridViewSummaryItem("Frieght_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        Dim item8 As New GridViewSummaryItem("Total_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Load_DCS_Sale_Report()
    End Sub
    Private Sub Load_DCS_Sale_Report()
        Dim qry As String = ""
        Dim dt As New DataTable()
        Try
            Dim whr As String = ""
            If txtBilltoLocation.Value IsNot Nothing AndAlso txtBilltoLocation.Value.Count > 0 Then
                whr += " and TSPL_SD_SALE_INVOICE_DETAIL.Location In  ('" + clsCommon.myCstr(txtBilltoLocation.Value) + "') "
            End If
            If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
                whr += " and TSPL_SD_SALE_INVOICE_DETAIL.Document_Code='" + txtDocumentNo.Value + "'"
            End If
            If clsCommon.myLen(txtDCS.Value) > 0 Then
                whr += "and TSPL_DCS_FOR_SALE.Name='" + txtDCS.Value + "'"
            End If

            qry = " Select ROW_NUMBER() OVER(ORDER BY TSPL_LOCATION_MASTER.Location_code ASC) as SNo, XFinal.*,cast((XFinal.CF_AMT+XFinal.Frieght_Amt)as decimal(18,2)) as Total_Amt,TSPL_LOCATION_MASTER.Location_Desc as Location_Desc,TSPL_LOCATION_MASTER.Add1 as Add1
                    from(
                    select Convert(Varchar(10),XX.Document_Date,105)as Bill_Date,XX.Bill,Convert(Varchar(10),XX.Dispatch_Date,105) as Dispatch_Date,XX.DCS_Name,XX.C_No,XX.GRNO,xx.Zone,XX.SADA,XX.SADA_Cost,XX.GOLD,XX.GOLD_Cost,XX.BPP,XX.BPP_Cost,XX.TruckNo,XX.ChallanNo,
                    cast((XX.SADA+XX.GOLD+XX.BPP) as decimal(18,2))as Bags,cast((((XX.SADA*XX.CF_Bag)+(XX.GOLD*XX.CF_Bag)+(XX.BPP*XX.CF_Bag))/XX.CF_MT) as decimal(18,2))as MT,
                    cast(((XX.SADA_Cost*XX.SADA)+(XX.GOLD_Cost*XX.GOLD)+(XX.BPP_Cost*XX.BPP))as decimal(18,2)) as CF_AMT,Cast(XX.Frieght_Rate as decimal(18,2)) as Frieght,
                    cast(((((XX.SADA*XX.CF_Bag)+(XX.GOLD*XX.CF_Bag)+(XX.BPP*XX.CF_Bag))/XX.CF_QTL)*XX.Frieght_Rate)as decimal(18,2))as Frieght_Amt,
                    XX.Location_code,XX.TranspoterName
                from(
                select 
                min(TSPL_SD_SHIPMENT_HEAD.Document_Date)as Dispatch_Date,
                TSPL_DCS_FOR_SALE.Name as DCS_Name,
                max(TSPL_DCS_FOR_SALE.Uploader_No) as C_No,
                max(TSPL_SD_SHIPMENT_HEAD.LR_GR_NO) as GRNO,
                max(TSPL_DCS_FOR_SALE.Zone) as Zone,
                sum(case when TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.ICode='FG0002' then TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.Qty else '0' end) as SADA,
                sum(case when TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.ICode='FG0003' then TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.Qty else '0' end) as GOLD,
                sum(case when TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.ICode='FG0001' then TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.Qty else '0' end) as BPP,
                max(TSPL_SD_SHIPMENT_HEAD.Form_38_No) as TruckNo,
                max(TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.PK_ID) as ChallanNo,
                max(case when TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.ICode='FG0002' then TSPL_SD_SHIPMENT_DETAIL.Item_Cost else '1' end ) as SADA_Cost,
                max(case when TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.ICode='FG0003' then TSPL_SD_SHIPMENT_DETAIL.Item_Cost else '1' end ) as GOLD_Cost,
                max(case when TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.ICode='FG0001' then TSPL_SD_SHIPMENT_DETAIL.Item_Cost else '1' end ) as BPP_Cost,
                max(TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.Frieght_Rate) as Frieght_Rate,
                max(TSPL_ITEM_UOM_DETAIL.Conversion_Factor)as CF_MT,
                max(TSPL_ITEM_UOM_DETAIL_BAG.Conversion_Factor) as CF_Bag,
                max(TSPL_ITEM_UOM_DETAIL_QTL.Conversion_Factor) as CF_QTL,
                TSPL_SD_SALE_INVOICE_HEAD.Document_Date,
                max(Right(TSPL_SD_SALE_INVOICE_HEAD.Document_Code,6))as Bill,
                max(TSPL_SD_SALE_INVOICE_DETAIL.Document_Code) as Document_Code,
                max(TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code) as Shipment_Code,
                max(TSPL_SD_SHIPMENT_HEAD.Bill_To_Location) as Location_code,
                max(TSPL_SD_SHIPMENT_HEAD.Carrier)as TranspoterName
                from TSPL_SD_SHIPMENT_HEAD
                left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE=TSPL_SD_SHIPMENT_HEAD.Document_Code
                left join TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL on TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.DOCUMENT_CODE=TSPL_SD_SHIPMENT_DETAIL.Document_Code AND
                TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.ICode=TSPL_SD_SHIPMENT_DETAIL.Item_Code
                --left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                --left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                left join TSPL_DCS_FOR_SALE on TSPL_DCS_FOR_SALE.Code=TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.DCS_Code
                --left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_HEAD.Bill_To_Location
                left join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code=TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.Document_Code  and
                TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.ICode
                left join TSPL_SD_SALE_INVOICE_HEAD	on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
                left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and
                TSPL_ITEM_UOM_DETAIL.UOM_Code='MT' 
                left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAIL_BAG on TSPL_ITEM_UOM_DETAIL_BAG.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and
                TSPL_ITEM_UOM_DETAIL_BAG.UOM_Code='BAG'
                left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAIL_QTL on TSPL_ITEM_UOM_DETAIL_QTL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and
                TSPL_ITEM_UOM_DETAIL_QTL.UOM_Code='QTL' WHERE  TSPL_SD_SALE_INVOICE_HEAD.Document_Code >= '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'  and  TSPL_SD_SALE_INVOICE_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(txtToDate.Value) + "'" + whr + "
                    group by TSPL_DCS_FOR_SALE.Name,TSPL_SD_SALE_INVOICE_HEAD.Document_Date
                    )XX
                    )XFinal 
                    left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=XFinal.Location_code"

            If clsCommon.myLen(qry) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry)
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.GroupDescriptors.Clear()
                Gv1.SummaryRowsBottom.Clear()
                Gv1.DataSource = dt
                'gv1.Columns("TransType").IsVisible = False
                'gv1.Columns("PROD_ENTRY_CODE").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                FormatGrid()
                'ReStoreGridLayout()
            Else
                clsCommon.MyMessageBoxShow("No data found to display.", "Sales Report")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtDocumentNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocumentNo._MYValidating
        Dim qry As String = "SELECT DISTINCT TSPL_SD_SALE_INVOICE_HEAD.Document_Code as code,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,tspl_customer_master.Customer_Name  FROM TSPL_SD_SALE_INVOICE_HEAD 
                            inner join  tspl_customer_master on tspl_customer_master.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
							inner join TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL on TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No  "
        Dim whrcls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls += " TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtDocumentNo.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "code", whrcls, txtDocumentNo.Value, "code", isButtonClicked)
        lblDocumentNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Document_Date FROM TSPL_SD_SALE_INVOICE_HEAD where TSPL_SD_SALE_INVOICE_HEAD.Document_Code= '" + txtDocumentNo.Value + "'"))
    End Sub
    Private Sub txtBilltoLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBilltoLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtBilltoLocation.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", WhrCls, txtBilltoLocation.Value, "Code", isButtonClicked)
        lblBilltoLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBilltoLocation.Value + "'"))

    End Sub

    Private Sub txtDCS__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDCS._MYValidating
        Dim qry As String = "SELECT  TSPL_DCS_FOR_SALE.CODE as DCSCode,TSPL_DCS_FOR_SALE.Name as [DCS Name] FROM TSPL_DCS_FOR_SALE 
                            INNER JOIN TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL  ON TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.DCS_Code=TSPL_DCS_FOR_SALE.Code "
        Dim whrcls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls += "   TSPL_DCS_FOR_SALE.Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtDCS.Value = clsCommon.ShowSelectForm("dcscode", qry, "DCSCode", whrcls, txtDCS.Value, "DCSCode", isButtonClicked)
        lblDCS.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TSPL_DCS_FOR_SALE.Name FROM TSPL_DCS_FOR_SALE INNER JOIN TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL  ON TSPL_SD_SHIPMENT_DCS_ITEM_DETAIL.DCS_Code=TSPL_DCS_FOR_SALE.Code where TSPL_DCS_FOR_SALE.CODE ='" + txtDCS.Value + "'"))

    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If

                If txtBilltoLocation.Value IsNot Nothing AndAlso txtBilltoLocation.Value.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.myCstr(lblBilltoLocation.Text))
                End If
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("DCS Sale Register Report", Gv1, arrHeader, Me.Text)
                ' transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                common.clsCommon.MyMessageBoxShow("Exported Successfully.", Me.Text)
                'Process.Start(filePath)

            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            If Gv1.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If
                '
                If txtBilltoLocation.Value IsNot Nothing AndAlso txtBilltoLocation.Value.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.myCstr(lblBilltoLocation.Text))
                End If
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                Dim doc As New RadPrintDocument()
                doc.Landscape = True
                clsCommon.MyExportToPDF("DCS Sale Register Report", Gv1, arrHeader, "DCS Sale Register Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class
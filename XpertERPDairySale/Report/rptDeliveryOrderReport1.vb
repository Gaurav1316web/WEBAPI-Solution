Imports common
Imports Telerik
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports System.IO
Public Class RptDeliveryOrderReport1
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()
    ' Ticket No : BHA/04/01/19-000774
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptCustomerEffective_ItemRate)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnGo.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub txtLocation1Mult__My_Click(sender As Object, e As EventArgs) Handles txtLocation1Mult._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER "
        txtLocation1Mult.arrValueMember = clsCommon.ShowMultipleSelectForm("bank", qry, "Code", "Name", txtLocation1Mult.arrValueMember, txtLocation1Mult.arrDispalyMember)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Dim qry As String
        Dim dt As DataTable
        Dim dtExtraColumn As DataTable = Nothing
        Dim StrToatlQuery As String = Nothing
        Dim StrToatl As String = Nothing
        Dim pivtvar As String = Nothing
        Dim strq As String = Nothing

        ' By Customer 


        Try
            If clsCommon.CompairString(clsCommon.myCstr(txtLocation1Mult.arrValueMember), "") = CompairStringResult.Equal Then
                'dtExtraColumn = clsDBFuncationality.GetDataTable("SELECT distinct Description" & _
                '                  " from TSPL_BOOKING_MATSER as a inner join TSPL_BOOKING_DETAIL as b on a.Document_No=b.Document_No" & _
                '                  " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on b.Vehicle_Code=d.Vehicle_Id" & _
                '                  " where  a.Document_Date='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Vehicle_Code is not null and Vehicle_Code!='' ")

                'StrToatlQuery = "select +','+STUFF(a.strr,1,1,'') from (select(select distinct + '+(isnull(['+ Description+'],0))' " & _
                '                 " from TSPL_BOOKING_MATSER as a inner join TSPL_BOOKING_DETAIL as b on a.Document_No=b.Document_No" & _
                '                  " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on b.Vehicle_Code=d.Vehicle_Id" & _
                '                  " where  a.Document_Date='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Vehicle_Code is not null and Vehicle_Code!='' for xml path ('')) as strr)as a"

                'StrToatl = clsCommon.myCstr(clsDBFuncationality.getSingleValue(StrToatlQuery))

                'strq = "select STUFF((SELECT distinct ',' + QUOTENAME(Description)" & _
                '                       " from TSPL_BOOKING_MATSER as a inner join TSPL_BOOKING_DETAIL as b on a.Document_No=b.Document_No" & _
                '                       " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on b.Vehicle_Code=d.Vehicle_Id" & _
                '                       " where a.Document_Date='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Vehicle_Code is not null and Vehicle_Code!='' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"

                '' qry = "USP_Get_Delivery_Order_Report_All  @Document_Date='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'"
                'pivtvar = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strq))

                dtExtraColumn = clsDBFuncationality.GetDataTable("SELECT distinct d.Description" & _
                                  " from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE as a inner join TSPL_DELIVERY_NOTE_Detail_FRESHSALE as b on a.Document_No=b.Document_No" & _
                                  " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on a.Lorry_No=d.Vehicle_Id" & _
                                  " where  convert (date,a.Document_Date,103)=convert (date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103) and a.Lorry_No is not null and a.Lorry_No!='' ")

                StrToatlQuery = "select +','+STUFF(a.strr,1,1,'') from (select(select distinct + '+(isnull(['+ Description+'],0))' " & _
                                 " from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE as a inner join TSPL_DELIVERY_NOTE_Detail_FRESHSALE as b on a.Document_No=b.Document_No" & _
                                  " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on a.Lorry_No=d.Vehicle_Id" & _
                                  " where  convert (date,a.Document_Date,103)=convert (date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103) and a.Lorry_No is not null and a.Lorry_No!='' for xml path ('')) as strr)as a"

                StrToatl = clsCommon.myCstr(clsDBFuncationality.getSingleValue(StrToatlQuery))

                strq = "select STUFF((SELECT distinct ',' + QUOTENAME(Description)" & _
                                       " from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE as a inner join TSPL_DELIVERY_NOTE_Detail_FRESHSALE as b on a.Document_No=b.Document_No" & _
                                       " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on a.Lorry_No=d.Vehicle_Id" & _
                                       " where convert (date,a.Document_Date,103)=convert (date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103) and a.Lorry_No is not null and a.Lorry_No!='' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"

                ' qry = "USP_Get_Delivery_Order_Report_All  @Document_Date='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'"
                pivtvar = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strq))
                If clsCommon.CompairString(clsCommon.myCstr(pivtvar), "") = CompairStringResult.Equal Then
                    gv.DataSource = Nothing
                    clsCommon.MyMessageBoxShow(Me, "No record found", Me.Text)
                    Return
                End If

                'qry = "select * " & StrToatl & " as 'Total'" & _
                '   " from " & _
                '   " (select max(e.Location_Desc) as 'Branch',c.Item_Code as 'Product Code'" & _
                '   " ,max(C.Item_Desc) as 'Product Name',isnull(sum(b.Booking_Qty),0) as 'BookingQty' ,d.Description" & _
                '   " from TSPL_BOOKING_MATSER as a inner join TSPL_BOOKING_DETAIL as b on a.Document_No=b.Document_No" & _
                '   " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on b.Vehicle_Code=d.Vehicle_Id" & _
                '   "  inner Join TSPL_LOCATION_MASTER as e on e.Location_Code=a.Location_Code" & _
                '   " where a.Document_Date='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'  group  by a.location_code,b.Vehicle_Code,c.Item_Code,d.Description) as p" & _
                '    "               Pivot" & _
                '   " (" & _
                '                   "sum(BookingQty)" & _
                '    " for Description in (" + pivtvar + ")" & _
                '   " )piv "
                If chkByCustomer.Checked = True Then
                    qry = "select * " & StrToatl & " as 'Total'" & _
                   " from " & _
                   " (select  max(e.Location_Desc) as 'Branch',c.Item_Code as 'Product Code'" & _
                   " ,max(C.Item_Desc) as 'Product Name',max(isnull(b.Unit_code,'')) as Unit,max(case isnull(b.Scheme_Item,'N') when 'N' then 'No' else 'Yes'  end )as 'Scheme Item',isnull(sum(b.Qty),0) as 'BookingQty' ,d.Description" & _
                   "  ,a.Customer_Code as [Customer Code],max(TSPL_CUSTOMER_MASTER.Customer_Name ) as [Customer Name] " & _
                   " from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE as a inner join TSPL_DELIVERY_NOTE_Detail_FRESHSALE as b on a.Document_No=b.Document_No" & _
                   " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on a.Lorry_No=d.Vehicle_Id" & _
                   "  inner Join TSPL_LOCATION_MASTER as e on e.Location_Code=a.Location_Code" & _
                   " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code= a.Customer_Code " & _
                   " where convert (date,a.Document_Date,103)=convert (date, '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103)  group  by a.location_code,a.Lorry_No,c.Item_Code,d.Description,a.Customer_Code) as p" & _
                    "               Pivot" & _
                   " (" & _
                                   "sum(BookingQty)" & _
                    " for Description in (" + pivtvar + ")" & _
                   " )piv "
                Else
                    qry = "select * " & StrToatl & " as 'Total'" & _
                   " from " & _
                   " (select  max(e.Location_Desc) as 'Branch',c.Item_Code as 'Product Code'" & _
                   " ,max(C.Item_Desc) as 'Product Name',max(isnull(b.Unit_code,'')) as Unit,max(case isnull(b.Scheme_Item,'N') when 'N' then 'No' else 'Yes'  end )as 'Scheme Item',isnull(sum(b.Qty),0) as 'BookingQty' ,d.Description" & _
                   " from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE as a inner join TSPL_DELIVERY_NOTE_Detail_FRESHSALE as b on a.Document_No=b.Document_No" & _
                   " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on a.Lorry_No=d.Vehicle_Id" & _
                   "  inner Join TSPL_LOCATION_MASTER as e on e.Location_Code=a.Location_Code" & _
                   " where convert (date, a.Document_Date,103)=convert (date, '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103)  group  by a.location_code,a.Lorry_No,c.Item_Code,d.Description) as p" & _
                    "               Pivot" & _
                   " (" & _
                                   "sum(BookingQty)" & _
                    " for Description in (" + pivtvar + ")" & _
                   " )piv "

                End If


            Else

                'dtExtraColumn = clsDBFuncationality.GetDataTable("SELECT distinct Description" & _
                '                  " from TSPL_BOOKING_MATSER as a inner join TSPL_BOOKING_DETAIL as b on a.Document_No=b.Document_No" & _
                '                  " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on b.Vehicle_Code=d.Vehicle_Id" & _
                '                  " where a.location_code in(" + clsCommon.GetMulcallString(txtLocation1Mult.arrValueMember) + ") and a.Document_Date='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Vehicle_Code is not null and Vehicle_Code!='' ")

                'StrToatlQuery = "select +','+STUFF(a.strr,1,1,'') from (select(select distinct + '+(isnull(['+ Description+'],0))' " & _
                '              " from TSPL_BOOKING_MATSER as a inner join TSPL_BOOKING_DETAIL as b on a.Document_No=b.Document_No" & _
                '               " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on b.Vehicle_Code=d.Vehicle_Id" & _
                '               " where  a.location_code in(" + clsCommon.GetMulcallString(txtLocation1Mult.arrValueMember) + ") and a.Document_Date='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Vehicle_Code is not null and Vehicle_Code!='' for xml path ('')) as strr)as a"

                'StrToatl = clsCommon.myCstr(clsDBFuncationality.getSingleValue(StrToatlQuery))
                'strq = "select STUFF((SELECT distinct ',' + QUOTENAME(Description)" & _
                '                        " from TSPL_BOOKING_MATSER as a inner join TSPL_BOOKING_DETAIL as b on a.Document_No=b.Document_No" & _
                '                        " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on b.Vehicle_Code=d.Vehicle_Id" & _
                '                        " where a.location_code in(" + clsCommon.GetMulcallString(txtLocation1Mult.arrValueMember) + ") and a.Document_Date='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Vehicle_Code is not null and Vehicle_Code!='' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"

                dtExtraColumn = clsDBFuncationality.GetDataTable("SELECT distinct d.Description" & _
                               " from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE as a inner join TSPL_DELIVERY_NOTE_Detail_FRESHSALE as b on a.Document_No=b.Document_No" & _
                               " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on a.Lorry_No=d.Vehicle_Id" & _
                               " where a.location_code in(" + clsCommon.GetMulcallString(txtLocation1Mult.arrValueMember) + ") and convert (date, a.Document_Date,1)=convert (date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103) and a.Lorry_No is not null and a.Lorry_No!='' ")

                StrToatlQuery = "select +','+STUFF(a.strr,1,1,'') from (select(select distinct + '+(isnull(['+ Description+'],0))' " & _
                                 " from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE as a inner join TSPL_DELIVERY_NOTE_Detail_FRESHSALE as b on a.Document_No=b.Document_No" & _
                                  " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on a.Lorry_No=d.Vehicle_Id" & _
                                  " where a.location_code in(" + clsCommon.GetMulcallString(txtLocation1Mult.arrValueMember) + ") and convert (date, a.Document_Date,103)=convert (date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103) and a.Lorry_No is not null and a.Lorry_No!='' for xml path ('')) as strr)as a"

                StrToatl = clsCommon.myCstr(clsDBFuncationality.getSingleValue(StrToatlQuery))

                strq = "select STUFF((SELECT distinct ',' + QUOTENAME(Description)" & _
                                       " from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE as a inner join TSPL_DELIVERY_NOTE_Detail_FRESHSALE as b on a.Document_No=b.Document_No" & _
                                       " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on a.Lorry_No=d.Vehicle_Id" & _
                                       " where a.location_code in(" + clsCommon.GetMulcallString(txtLocation1Mult.arrValueMember) + ") and convert (date, a.Document_Date,103)=convert (date, '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103) and a.Lorry_No is not null and a.Lorry_No!='' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"

                pivtvar = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strq))
                If clsCommon.CompairString(clsCommon.myCstr(pivtvar), "") = CompairStringResult.Equal Then
                    gv.DataSource = Nothing
                    clsCommon.MyMessageBoxShow(Me, "No record found", Me.Text)
                    Return
                End If
                'qry = "select * " & StrToatl & " as 'Total'" & _
                '    " from " & _
                '    " (select max(e.Location_Desc) as 'Branch',c.Item_Code as 'Product Code'" & _
                '    " ,max(C.Item_Desc) as 'Product Name',isnull(Unit_Code),isnull(sum(b.Qty),0) as 'BookingQty' ,d.Description" & _
                '    " from TSPL_BOOKING_MATSER as a inner join TSPL_BOOKING_DETAIL as b on a.Document_No=b.Document_No" & _
                '    " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on b.Vehicle_Code=d.Vehicle_Id" & _
                '    "  inner Join TSPL_LOCATION_MASTER as e on e.Location_Code=a.Location_Code" & _
                '    " where a.location_code in(" + clsCommon.GetMulcallString(txtLocation1Mult.arrValueMember) + ") and a.Document_Date='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'  group  by a.location_code,b.Vehicle_Code,c.Item_Code,d.Description) as p" & _
                '     "               Pivot" & _
                '    " (" & _
                '                    "sum(BookingQty)" & _
                '     " for Description in (" + pivtvar + ")" & _
                '    " )piv "

                If chkByCustomer.Checked = True Then
                    qry = "select * " & StrToatl & " as 'Total'" & _
                " from " & _
                " (select  max(e.Location_Desc) as 'Branch',c.Item_Code as 'Product Code'" & _
                " ,max(C.Item_Desc) as 'Product Name',max(isnull(b.Unit_code,'')) as Unit,max(case isnull(b.Scheme_Item,'N') when 'N' then 'No' else 'Yes' end )as 'Scheme Item',isnull(sum(b.Qty),0) as 'BookingQty' ,d.Description" & _
                " ,a.Customer_Code as [Customer Code],max(TSPL_CUSTOMER_MASTER.Customer_Name ) as [Customer Name] " & _
                " from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE as a inner join TSPL_DELIVERY_NOTE_Detail_FRESHSALE as b on a.Document_No=b.Document_No" & _
                " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on a.Lorry_No=d.Vehicle_Id" & _
                "  inner Join TSPL_LOCATION_MASTER as e on e.Location_Code=a.Location_Code" & _
                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code= a.Customer_Code " & _
                " where a.location_code in(" + clsCommon.GetMulcallString(txtLocation1Mult.arrValueMember) + ") and convert (date,a.Document_Date,103)=convert (date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103)  group  by isnull(b.Scheme_Item,'N'),a.location_code,a.Lorry_No,c.Item_Code,d.Description,a.Customer_Code) as p" & _
                 "               Pivot" & _
                " (" & _
                                "sum(BookingQty)" & _
                 " for Description in (" + pivtvar + ")" & _
                " )piv "

                Else
                    qry = "select * " & StrToatl & " as 'Total'" & _
               " from " & _
               " (select  max(e.Location_Desc) as 'Branch',c.Item_Code as 'Product Code'" & _
               " ,max(C.Item_Desc) as 'Product Name',max(isnull(b.Unit_code,'')) as Unit,max(case isnull(b.Scheme_Item,'N') when 'N' then 'No' else 'Yes' end )as 'Scheme Item',isnull(sum(b.Qty),0) as 'BookingQty' ,d.Description" & _
               " from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE as a inner join TSPL_DELIVERY_NOTE_Detail_FRESHSALE as b on a.Document_No=b.Document_No" & _
               " inner join TSPL_ITEM_MASTER as c on b.item_code=c.Item_Code inner join TSPL_VEHICLE_MASTER as d on a.Lorry_No=d.Vehicle_Id" & _
               "  inner Join TSPL_LOCATION_MASTER as e on e.Location_Code=a.Location_Code" & _
               " where a.location_code in(" + clsCommon.GetMulcallString(txtLocation1Mult.arrValueMember) + ") and convert (date,a.Document_Date,103)=convert (date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103)  group  by isnull(b.Scheme_Item,'N'),a.location_code,a.Lorry_No,c.Item_Code,d.Description) as p" & _
                "               Pivot" & _
               " (" & _
                               "sum(BookingQty)" & _
                " for Description in (" + pivtvar + ")" & _
               " )piv "
                End If
                




            End If


            dt = clsDBFuncationality.GetDataTable(qry)
            If (dt Is Nothing OrElse dt.Rows.Count > 0) Then
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                gv.DataSource = Nothing
                gv.DataSource = dt
                gv.BestFitColumns()
                FormatGrid(dtExtraColumn)
                gv.ReadOnly = True
            Else
                gv.DataSource = Nothing
                clsCommon.MyMessageBoxShow(Me, "No record found", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub rptSaveLayout_Click(sender As Object, e As EventArgs) Handles rptSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()

            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------

        End If

    End Sub

    Private Sub rptDeleteLayout_Click(sender As Object, e As EventArgs) Handles rptDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub RptDeliveryOrderReport1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtFromDate.Text = clsCommon.GETSERVERDATE()
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+G for Search")
        ButtonToolTip.SetToolTip(btnExport, "Press Alt+E for Export")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for Close")

    End Sub



    Private Sub SplitContainer1_Panel2_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel2.Paint

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub FormatGrid(ByVal dtExtraColumn As DataTable)

        'For ii As Integer = 0 To gv.Columns.Count - 1
        '    gv.Columns(ii).ReadOnly = True
        '    gv.Columns(ii).BestFit()
        'Next


        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim intCount As Integer = 0
        If dtExtraColumn IsNot Nothing AndAlso dtExtraColumn.Rows.Count > 0 Then
            For Each dr As DataRow In dtExtraColumn.Rows
                Dim item1 As New GridViewSummaryItem(clsCommon.myCstr(dr(0)), "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            Next
        End If

        Dim item3 As New GridViewSummaryItem("Total", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)


        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub Excel_Click(sender As Object, e As EventArgs) Handles Excel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Delivery Order Report ")
            arrHeader.Add("Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Delivery Order Report ")
            arrHeader.Add("Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Delivery Order Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

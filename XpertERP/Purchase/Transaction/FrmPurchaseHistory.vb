
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System
Imports System.IO

'-------------------Done by Meenesh-------------dated on 28/05/2014---------------------------------------
Public Class FrmPurchaseHistory
    Inherits FrmMainTranScreen

#Region "Variables"
    Public Const ColPI_NO As String = "PI_NO"
    Const ColVendorCode As String = "vendor Code"
    Const ColVendorName As String = "Vendor name"
    Const ColVendorDesc As String = "Vendor Desc"
    Const ColVendorInvoiceNo As String = "Vendor invoice no"
    Const ColSRNNO As String = "SRN_ID"
    Const ColItemCode As String = "Item Code"
    Const ColItemDesc As String = "Item Desc"
    Const ColPRcode As String = "PR Code"
    Dim buttontooltip As ToolTip = New ToolTip()

    Public strFormId As String
    Public strVendorCode As String = ""
    Public strVendorName As String = ""



#End Region


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPurchaseHistory)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

    End Sub

    Private Sub FrmPurchaseHistory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R AndAlso btnReport.Enabled Then
            fillGridReport(dtpfromdate.Value, dtptodate.Value, fndSrcCode.Value)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.E AndAlso btnreset.Enabled Then
            reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()

        End If

    End Sub
    Private Sub FrmPurchaseHistory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        buttontooltip.SetToolTip(btnReport, "Press Alt+R for Summary ")
        buttontooltip.SetToolTip(btnreset, "Press Alt+E for Reset ")
        buttontooltip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        dtptodate.Value = clsCommon.GETSERVERDATE()
        dtpfromdate.Value = dtptodate.Value.AddMonths(-1)

        cmbType.SelectedIndex = 0
        lbltype.Text = "Vendor"

        cmbType.SelectedIndex = 1
        fndSrcCode.Value = strVendorCode
        txtSrcDesc.Text = strVendorName

        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.Visible = False





    End Sub
    Private Sub cmbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cmbType.SelectedIndexChanged
        newgv1.Visible = False
        fndSrcCode.Value = ""
        txtSrcDesc.Text = ""
        If cmbType.SelectedIndex = 1 Then
            lbltype.Text = "Item"
            gv1.Visible = False
        ElseIf cmbType.SelectedIndex = 2 Then
            lbltype.Text = "Vendor"
        Else
            lbltype.Text = "Vendor"
            gv1.Visible = False


        End If
    End Sub


    Private Sub fndSrcCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSrcCode._MYValidating
        Try
            If cmbType.SelectedIndex = 1 Then

                Dim qry As String = "select Item_Code as Code ,Item_Desc  from TSPL_ITEM_MASTER "
                fndSrcCode.Value = clsCommon.ShowSelectForm("ItemDetails", qry, "Code", "", fndSrcCode.Value, "Code", True)
                txtSrcDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc  from TSPL_ITEM_MASTER   where Item_Code ='" + fndSrcCode.Value + "'"))
               

            ElseIf cmbType.SelectedIndex = 2 Then
                Dim qry As String = "select Vendor_Code as code,Vendor_Name  from TSPL_VENDOR_MASTER  "
                Dim Whr = " Status='N' "
                fndSrcCode.Value = clsCommon.ShowSelectForm("VendorDetails", qry, "Code", Whr, fndSrcCode.Value, "Code", True)
                txtSrcDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name  from TSPL_VENDOR_MASTER  where Vendor_Code ='" + fndSrcCode.Value + "'"))
            
            ElseIf (fndSrcCode.Value = "" AndAlso isButtonClicked = True) Then
                newgv1.Visible = False
            Else
                common.clsCommon.MyMessageBoxShow(Me, "choose Select By: first", Me.Text)


            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)

        End Try
    End Sub


    'Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    'End Sub
    Public Sub fillGridReport(ByVal frmdate As Date, ByVal todate As Date, ByVal fndsrc As String)
        Try
            Dim query As String
            Dim tdate As Date = clsCommon.GetPrintDate(dtptodate.Value, "dd/MMM/yyyy")
            Dim fdate As Date = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MMM/yyyy")

            newgv1.DataSource = Nothing

            If (validate1() = True) Then
                If cmbType.SelectedIndex = 2 Then
                    query = "select [Item Code] ,  Bill_To_Location,max(Location_Desc)  as Location_Desc ,max([Item Description])as [Item Desc],sum([Qty Received]) as[Qty Received],sum([Receipt Amount])as [Receipt Amount],sum([Invoice Amount]) as [Invoice Amount],sum([Qty Returned])as [Quantity Returned], sum([Return Amount]) as [Return Amount],sum(([Invoice Amount])-([Return Amount]))as [Net Amount]  "
                    query += " from (select TSPL_SRN_DETAIL .Item_Code as [Item Code] , TSPL_PI_HEAD.Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc , TSPL_SRN_DETAIL.Item_Desc as [Item Description],TSPL_SRN_DETAIL .SRN_Qty as 'Qty Received',TSPL_SRN_DETAIL .Item_Net_Amt as 'Receipt Amount' ,TSPL_PI_DETAIL .Item_Net_Amt as 'Invoice Amount',"
                    query += " (case when TSPL_PR_DETAIL .PR_Qty is not null then  TSPL_PR_DETAIL .PR_Qty else '0.0' end)as 'Qty Returned',"
                    query += " (case when TSPL_PR_DETAIL .Item_Net_Amt is not null then TSPL_PR_DETAIL .Item_Net_Amt else '0.0' end )as 'Return Amount'   "
                    query += " from TSPL_SRN_HEAD  inner join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No inner join TSPL_PI_HEAD on TSPL_PI_HEAD.Against_SRN=TSPL_SRN_DETAIL.SRN_No "
                    query += " inner join TSPL_PI_DETAIL on TSPL_PI_DETAIL.SRN_Id=TSPL_PI_HEAD.Against_SRN   AND   TSPL_PI_DETAIL.PI_No =TSPL_PI_HEAD .PI_No   AND   TSPL_SRN_DETAIL .Item_Code =TSPL_PI_DETAIL .Item_Code   left outer join TSPL_PR_HEAD on  TSPL_PR_HEAD.Against_PI=TSPL_PI_HEAD .PI_No   AND   TSPL_PR_HEAD.Against_SRN=TSPL_PI_HEAD.Against_SRN  "
                    query += " left outer join TSPL_PR_DETAIL on TSPL_PR_DETAIL.PI_Id = TSPL_PR_HEAD.Against_PI   AND   TSPL_PR_DETAIL.PR_No=TSPL_PR_HEAD.PR_No   AND   TSPL_PI_DETAIL.Item_Code=TSPL_PR_DETAIL.Item_Code LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_code = TSPL_PI_HEAD.Bill_To_Location "
                    query += " where Convert(date,TSPL_SRN_HEAD.SRN_Date,103) >=Convert(date,'" + fdate + "',103)   AND   Convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=convert(date,'" + tdate + "',103) "
                    query += "   AND   TSPL_SRN_HEAD.Vendor_Code ='" + fndsrc + "'   AND   TSPL_PI_head.Vendor_Code ='" + fndsrc + "'"

                    If multiLocationFinder.arrValueMember IsNot Nothing AndAlso multiLocationFinder.arrValueMember.Count > 0 Then
                        query += "  AND   TSPL_SRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ")   AND   TSPL_PI_head.Bill_To_Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ")"
                    Else
                        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                            query += "  AND   TSPL_SRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")   AND   TSPL_PI_head.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                        End If
                    End If

                    query += "union all"

                    query += " select TSPL_SRN_DETAIL .Item_Code as [Item Code] , TSPL_PI_HEAD.Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc , TSPL_SRN_DETAIL.Item_Desc as [Item Description],TSPL_SRN_DETAIL .SRN_Qty as 'Qty Received',TSPL_SRN_DETAIL .Item_Net_Amt as 'Receipt Amount' ,TSPL_PI_DETAIL .Item_Net_Amt as 'Invoice Amount',"
                    query += " (case when TSPL_PR_DETAIL .PR_Qty is not null then  TSPL_PR_DETAIL .PR_Qty else '0.0' end)as 'Qty Returned',"
                    query += " (case when TSPL_PR_DETAIL .Item_Net_Amt is not null then TSPL_PR_DETAIL .Item_Net_Amt else '0.0' end )as 'Return Amount'   "
                    query += " from TSPL_SRN_HEAD  inner join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No inner join TSPL_PI_HEAD on TSPL_PI_HEAD.Against_SRN=TSPL_SRN_DETAIL.SRN_No "
                    query += " inner join TSPL_PI_DETAIL on TSPL_PI_DETAIL.SRN_Id=TSPL_PI_HEAD.Against_SRN   AND   TSPL_PI_DETAIL.PI_No =TSPL_PI_HEAD .PI_No   AND   TSPL_SRN_DETAIL .Item_Code =TSPL_PI_DETAIL .Item_Code   left outer join TSPL_PR_HEAD on  TSPL_PR_HEAD.Against_PI=TSPL_PI_HEAD .PI_No   AND   TSPL_PR_HEAD.Against_SRN=TSPL_PI_HEAD.Against_SRN  "
                    query += " left outer join TSPL_PR_DETAIL on TSPL_PR_DETAIL.PI_Id = TSPL_PR_HEAD.Against_PI   AND   TSPL_PR_DETAIL.PR_No=TSPL_PR_HEAD.PR_No   AND   TSPL_PI_DETAIL.Item_Code=TSPL_PR_DETAIL.Item_Code LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_code = TSPL_PI_HEAD.Bill_To_Location "
                    query += " where Convert(date,TSPL_SRN_HEAD.SRN_Date,103) >=Convert(date,'" + fdate + "',103)   AND   Convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=convert(date,'" + tdate + "',103) "
                    query += " AND   TSPL_SRN_HEAD.Vendor_Code ='" + fndsrc + "'   AND   TSPL_PI_head.Vendor_Code ='" + fndsrc + "'"
                    If multiLocationFinder.arrValueMember IsNot Nothing AndAlso multiLocationFinder.arrValueMember.Count > 0 Then
                        query += "  AND   TSPL_SRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ")   AND   TSPL_PI_head.Bill_To_Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ")"
                    Else
                        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                            query += "  AND   TSPL_SRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")   AND   TSPL_PI_head.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                        End If
                    End If
                    query += "   AND   TSPL_PR_HEAD.PR_No <> ''"
                    query += " ) XYZ  GROUP BY [Item code] , Bill_To_Location"

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        newgv1.Visible = True
                        newgv1.DataSource = dt
                        fillgridcolumnvendor()
                        newgv1.ReadOnly = True
                        gv1.Visible = False
                        ReStoreGridLayout()
                    Else
                        common.clsCommon.MyMessageBoxShow("No Data Found for this Vendor  ", Me.Text)
                        gv1.DataSource = Nothing

                    End If

                ElseIf cmbType.SelectedIndex = 1 Then
                    query = "select [Vendor Code]  ,max([Vendor Name])as [Vendor Desc],sum([Qty Received]) as[Qty Received],sum([Receipt Amount])as [Receipt Amount],sum([Invoice Amount]) as [Invoice Amount],sum([Qty Returned])as [Quantity Returned], sum([Return Amount]) as [Return Amount],sum(([Invoice Amount])-([Return Amount]))as [Net Amount] ,  [Location], MAX([Location Name]) [Location Desc] "
                    query += " from (select  TSPL_SRN_HEAD.Vendor_Code as [Vendor Code] ,TSPL_SRN_HEAD.Vendor_Name as [Vendor Name],TSPL_SRN_DETAIL.SRN_Qty as [Qty Received],TSPL_SRN_DETAIL .Item_Net_Amt as 'Receipt Amount' ,TSPL_PI_DETAIL .Item_Net_Amt as 'Invoice Amount',  TSPL_PI_HEAD.Bill_To_Location AS [Location], Location_Desc AS [Location Name],"
                    query += " (case when TSPL_PR_DETAIL .PR_Qty is not null then  TSPL_PR_DETAIL .PR_Qty else '0.0' end)as 'Qty Returned',"
                    query += " (case when TSPL_PR_DETAIL .Item_Net_Amt is not null then TSPL_PR_DETAIL .Item_Net_Amt else '0.0' end )as 'Return Amount'   "
                    query += "  from TSPL_SRN_HEAD inner join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No inner join TSPL_PI_HEAD on TSPL_PI_HEAD.Against_SRN=TSPL_SRN_DETAIL.SRN_No "
                    query += " inner join TSPL_PI_DETAIL on TSPL_PI_DETAIL.SRN_Id=TSPL_PI_HEAD.Against_SRN and TSPL_PI_DETAIL.PI_No =TSPL_PI_HEAD .PI_No and TSPL_SRN_DETAIL .Item_Code =TSPL_PI_DETAIL .Item_Code  left outer join TSPL_PR_HEAD on  TSPL_PR_HEAD.Against_PI=TSPL_PI_HEAD .PI_No and TSPL_PR_HEAD.Against_SRN=TSPL_PI_HEAD.Against_SRN  "
                    query += " left outer join TSPL_PR_DETAIL on TSPL_PR_DETAIL.PI_Id = TSPL_PR_HEAD.Against_PI and TSPL_PR_DETAIL.PR_No=TSPL_PR_HEAD.PR_No AND TSPL_PI_DETAIL.Item_Code=TSPL_PR_DETAIL.Item_Code LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_code = TSPL_PI_HEAD.Bill_To_Location"
                    query += " where Convert(date,TSPL_SRN_HEAD.SRN_Date,103) >=Convert(date,'" + fdate + "',103) and Convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=Convert(date,'" + tdate + "',103) "
                    query += " and TSPL_SRN_DETAIL .Item_Code ='" + fndsrc + "' and TSPL_PI_DETAIL.Item_Code='" + fndsrc + "'"
                    If multiLocationFinder.arrValueMember IsNot Nothing AndAlso multiLocationFinder.arrValueMember.Count > 0 Then
                        query += "  AND  TSPL_SRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ")  AND  TSPL_PI_head.Bill_To_Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ")"
                    Else
                        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                            query += "  AND   TSPL_SRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")   AND   TSPL_PI_head.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                        End If
                    End If

                    query += " union all "

                    query += " select  TSPL_SRN_HEAD.Vendor_Code as [Vendor Code] ,TSPL_SRN_HEAD.Vendor_Name as [Vendor Name],TSPL_SRN_DETAIL.SRN_Qty as [Qty Received],TSPL_SRN_DETAIL .Item_Net_Amt as 'Receipt Amount' ,TSPL_PI_DETAIL .Item_Net_Amt as 'Invoice Amount',  TSPL_PI_HEAD.Bill_To_Location AS [Location], Location_Desc AS [Location Name],"
                    query += " (case when TSPL_PR_DETAIL .PR_Qty is not null then  TSPL_PR_DETAIL .PR_Qty else '0.0' end)as 'Qty Returned',"
                    query += " (case when TSPL_PR_DETAIL .Item_Net_Amt is not null then TSPL_PR_DETAIL .Item_Net_Amt else '0.0' end )as 'Return Amount'   "
                    query += "  from TSPL_SRN_HEAD inner join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No inner join TSPL_PI_HEAD on TSPL_PI_HEAD.Against_SRN=TSPL_SRN_DETAIL.SRN_No "
                    query += " inner join TSPL_PI_DETAIL on TSPL_PI_DETAIL.SRN_Id=TSPL_PI_HEAD.Against_SRN and TSPL_PI_DETAIL.PI_No =TSPL_PI_HEAD .PI_No and TSPL_SRN_DETAIL .Item_Code =TSPL_PI_DETAIL .Item_Code  left outer join TSPL_PR_HEAD on  TSPL_PR_HEAD.Against_PI=TSPL_PI_HEAD .PI_No and TSPL_PR_HEAD.Against_SRN=TSPL_PI_HEAD.Against_SRN  "
                    query += " left outer join TSPL_PR_DETAIL on TSPL_PR_DETAIL.PI_Id = TSPL_PR_HEAD.Against_PI and TSPL_PR_DETAIL.PR_No=TSPL_PR_HEAD.PR_No AND TSPL_PI_DETAIL.Item_Code=TSPL_PR_DETAIL.Item_Code LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_code = TSPL_PI_HEAD.Bill_To_Location"
                    query += " where Convert(date,TSPL_SRN_HEAD.SRN_Date,103) >=Convert(date,'" + fdate + "',103) and Convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=Convert(date,'" + tdate + "',103) "
                    query += " and TSPL_SRN_DETAIL .Item_Code ='" + fndsrc + "' and TSPL_PI_DETAIL.Item_Code='" + fndsrc + "'"
                    If multiLocationFinder.arrValueMember IsNot Nothing AndAlso multiLocationFinder.arrValueMember.Count > 0 Then
                        query += "  AND  TSPL_SRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ")   AND   TSPL_PI_head.Bill_To_Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ")"
                    Else
                        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                            query += "  AND   TSPL_SRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")   AND   TSPL_PI_head.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                        End If
                    End If
                    query += "   AND   TSPL_PR_HEAD.PR_No <> ''"
                    query += " ) XYZ  GROUP BY [Vendor Code] , [Location]"

                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
                    If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                        newgv1.Visible = True
                        newgv1.DataSource = dt2
                        fillgridcolumnsItem()
                        newgv1.ReadOnly = True
                        gv1.Visible = False
                        ReStoreGridLayout()

                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "No Data Found for this Item ", Me.Text)
                        gv1.DataSource = Nothing
                    End If
                End If

            Else
                common.clsCommon.MyMessageBoxShow("First fill the fields ", Me.Text)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)


        End Try

    End Sub
    Private Function validate1() As Boolean
        If cmbType.SelectedIndex = 0 AndAlso fndSrcCode.Value = Nothing Then
            Return False

        ElseIf fndSrcCode.Value = "" Then
            Return False
        ElseIf cmbType.SelectedIndex = 0 Then
            Return False
        Else

            Return True
        End If

    End Function

    Sub fillgridcolumnvendor()

        Try
            newgv1.Columns("Item Code").Width = 100
            newgv1.Columns("Item Desc").Width = 150
            newgv1.Columns("Qty Received").Width = 150
            newgv1.Columns("Receipt Amount").Width = 150
            newgv1.Columns("Invoice Amount").Width = 150
            newgv1.Columns("Quantity Returned").Width = 100
            newgv1.Columns("Return Amount").Width = 100
            newgv1.Columns("Net Amount").Width = 100

            newgv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim summaryRowItem As New GridViewSummaryRowItem()

            newgv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Dim item1 As New GridViewSummaryItem("Qty Received", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Net Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    Sub fillgridcolumnsItem()
        Try
            newgv1.Columns("Vendor Code").Width = 100
            newgv1.Columns("Vendor Desc").Width = 100
            newgv1.Columns("Qty Received").Width = 150
            newgv1.Columns("Receipt Amount").Width = 150
            newgv1.Columns("Invoice Amount").Width = 150
            newgv1.Columns("Quantity Returned").Width = 100
            newgv1.Columns("Return Amount").Width = 100
            newgv1.Columns("Net Amount").Width = 100
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    Private Sub newgv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles newgv1.DoubleClick

        Try
            Dim query As String
            Dim tdate As Date = clsCommon.GetPrintDate(dtptodate.Value, "dd/MMM/yyyy")
            Dim fdate As Date = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MMM/yyyy")
            Dim code As String = fndSrcCode.Value
            gv1.DataSource = Nothing
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim summaryRowItem As New GridViewSummaryRowItem()

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Dim item1 As New GridViewSummaryItem("Quantity", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)

            Dim item3 As New GridViewSummaryItem("Reciept Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)

            If (cmbType.SelectedIndex = 2) Then
                Dim VenCode As String = clsCommon.myCstr(newgv1.CurrentRow.Cells("Item Code").Value)
                query = " select tspl_srn_head.SRN_No as[SRN NO], convert(varchar,tspl_srn_head.SRN_Date,103) as [SRN DATE],'Purchase Invoice' as [Type],TSPL_PI_HEAD.PI_No as [Doc NO] , convert(varchar,TSPL_PI_Head.PI_Date,103) as [Date], TSPL_PI_HEAD.Bill_To_Location as [Location] ,Location_Desc as [Location Name],TSPL_PI_head.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name],TSPL_PI_DETAIL .PI_Qty  as [Quantity] ,TSPL_PI_DETAIL.Item_Net_Amt as[Amount],TSPL_SRN_DETAIL.Item_Net_Amt as [Reciept Amount] "
                query += " from TSPL_SRN_HEAD  inner join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No inner join TSPL_PI_HEAD on TSPL_PI_HEAD.Against_SRN=TSPL_SRN_DETAIL.SRN_No "
                query += " inner join TSPL_PI_DETAIL on TSPL_PI_DETAIL.SRN_Id=TSPL_PI_HEAD.Against_SRN and TSPL_PI_DETAIL.PI_No =TSPL_PI_HEAD .PI_No and TSPL_SRN_DETAIL .Item_Code =TSPL_PI_DETAIL .Item_Code   left outer join TSPL_PR_HEAD on  TSPL_PR_HEAD.Against_PI=TSPL_PI_HEAD .PI_No and TSPL_PR_HEAD.Against_SRN=TSPL_PI_HEAD.Against_SRN  "
                query += " left outer join TSPL_PR_DETAIL on TSPL_PR_DETAIL.PI_Id = TSPL_PR_HEAD.Against_PI and TSPL_PR_DETAIL.PR_No=TSPL_PR_HEAD.PR_No AND TSPL_PI_DETAIL.Item_Code=TSPL_PR_DETAIL.Item_Code "
                query += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_code = TSPL_PI_HEAD.Bill_To_Location left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PI_head.Vendor_Code"
                query += " where Convert(date,TSPL_SRN_HEAD.SRN_Date,103) >=Convert(date,'" + fdate + "',103) and Convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=convert(date,'" + tdate + "',103) "
                query += " and TSPL_SRN_HEAD.Vendor_Code ='" + code + "' and TSPL_PI_head.Vendor_Code ='" + code + "' and TSPL_SRN_DETAIL .Item_Code ='" + VenCode + "' and TSPL_PI_DETAIL.Item_Code='" + VenCode + "' "
                If multiLocationFinder.arrValueMember IsNot Nothing AndAlso multiLocationFinder.arrValueMember.Count > 0 Then
                    query += "  AND  TSPL_SRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ")   AND   TSPL_PI_head.Bill_To_Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ")"
                Else
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        query += "  AND   TSPL_SRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")   AND   TSPL_PI_head.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                End If
                query += " union "
                query += " select tspl_srn_head.SRN_No as[SRN NO], convert(varchar,tspl_srn_head.SRN_Date,103) as [SRN DATE] ,'Purchase Return' as [Type] ,TSPL_PR_HEAD.PR_No  as [Doc NO] , convert(varchar,TSPL_PR_HEAD.PR_Date,103) as [Date] , TSPL_PR_DETAIL.Location as [Location] ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name] ,TSPL_PR_HEAD.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name] ,TSPL_PR_DETAIL .PR_Qty  *-1 as [Quantity],TSPL_PR_DETAIL.Item_Net_Amt *-1 as [Amount],TSPL_SRN_DETAIL.Item_Net_Amt*-1 as [Reciept Amount]    "
                query += " from TSPL_SRN_HEAD  inner join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No inner join TSPL_PI_HEAD on TSPL_PI_HEAD.Against_SRN=TSPL_SRN_DETAIL.SRN_No "
                query += " inner join TSPL_PI_DETAIL on TSPL_PI_DETAIL.SRN_Id=TSPL_PI_HEAD.Against_SRN and TSPL_PI_DETAIL.PI_No =TSPL_PI_HEAD .PI_No and TSPL_SRN_DETAIL .Item_Code =TSPL_PI_DETAIL .Item_Code   left outer join TSPL_PR_HEAD on  TSPL_PR_HEAD.Against_PI=TSPL_PI_HEAD .PI_No and TSPL_PR_HEAD.Against_SRN=TSPL_PI_HEAD.Against_SRN  "
                query += " left outer join TSPL_PR_DETAIL on TSPL_PR_DETAIL.PI_Id = TSPL_PR_HEAD.Against_PI and TSPL_PR_DETAIL.PR_No=TSPL_PR_HEAD.PR_No AND TSPL_PI_DETAIL.Item_Code=TSPL_PR_DETAIL.Item_Code "
                query += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_code = TSPL_PR_DETAIL.Location  left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PR_HEAD.Vendor_Code	"
                query += " where Convert(date,TSPL_SRN_HEAD.SRN_Date,103) >=Convert(date,'" + fdate + "',103) and Convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=convert(date,'" + tdate + "',103) "
                query += " and TSPL_SRN_HEAD.Vendor_Code ='" + code + "' and TSPL_PI_head.Vendor_Code ='" + code + "' and      TSPL_PR_HEAD.PR_No<>'' and TSPL_SRN_DETAIL .Item_Code ='" + VenCode + "' and TSPL_PI_DETAIL.Item_Code='" + VenCode + "'    "
                If multiLocationFinder.arrValueMember IsNot Nothing AndAlso multiLocationFinder.arrValueMember.Count > 0 Then
                    query += "  AND  TSPL_SRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ")   AND   TSPL_PI_head.Bill_To_Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ")"
                Else
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        query += "  AND   TSPL_SRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")   AND   TSPL_PI_head.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    gv1.Visible = True

                    gv1.DataSource = dt

                    fillgridcolumnvendorDetails()

                    gv1.ReadOnly = True

                    RadPageView1.Visible = True
                    ReStoreGridLayoutDetails()
                    RadPageView1.SelectedPage = RadPageViewPage2


                End If
            ElseIf (cmbType.SelectedIndex = 1) Then

                Dim ItemCode As String = clsCommon.myCstr(newgv1.CurrentRow.Cells("Vendor Code").Value)

                query = " select tspl_srn_head.SRN_No as[SRN NO], convert(varchar,tspl_srn_head.SRN_Date,103) as [SRN DATE] ,'Purchase Invoice' as [Type],TSPL_PI_HEAD.PI_No as [Doc NO] , convert(varchar,TSPL_PI_Head.PI_Date,103) as [Date], TSPL_PI_DETAIL.Location as [Location],Location_Desc as [Location Name],TSPL_PI_head.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name],TSPL_PI_DETAIL .PI_Qty  as [Quantity] ,TSPL_PI_DETAIL.Item_Net_Amt as[Amount],TSPL_SRN_DETAIL.Item_Net_Amt as [Reciept Amount] "
                query += " from TSPL_SRN_HEAD  inner join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No inner join TSPL_PI_HEAD on TSPL_PI_HEAD.Against_SRN=TSPL_SRN_DETAIL.SRN_No "
                query += " inner join TSPL_PI_DETAIL on TSPL_PI_DETAIL.SRN_Id=TSPL_PI_HEAD.Against_SRN and TSPL_PI_DETAIL.PI_No =TSPL_PI_HEAD .PI_No and TSPL_SRN_DETAIL .Item_Code =TSPL_PI_DETAIL .Item_Code   left outer join TSPL_PR_HEAD on  TSPL_PR_HEAD.Against_PI=TSPL_PI_HEAD .PI_No and TSPL_PR_HEAD.Against_SRN=TSPL_PI_HEAD.Against_SRN  "
                query += " left outer join TSPL_PR_DETAIL on TSPL_PR_DETAIL.PI_Id = TSPL_PR_HEAD.Against_PI and TSPL_PR_DETAIL.PR_No=TSPL_PR_HEAD.PR_No AND TSPL_PI_DETAIL.Item_Code=TSPL_PR_DETAIL.Item_Code "
                query += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_code = TSPL_PI_DETAIL.Location left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PI_head.Vendor_Code"
                query += " where TSPL_SRN_HEAD.SRN_Date >=Convert(date,'" + fdate + "',103) and TSPL_SRN_HEAD.SRN_Date <=convert(date,'" + tdate + "',103) "
                query += " and TSPL_SRN_DETAIL .Item_Code ='" + code + "' and TSPL_PI_DETAIL.Item_Code='" + code + "' and TSPL_SRN_HEAD.Vendor_Code ='" + ItemCode + "' and TSPL_PI_head.Vendor_Code ='" + ItemCode + "'"
                If multiLocationFinder.arrValueMember IsNot Nothing AndAlso multiLocationFinder.arrValueMember.Count > 0 Then
                    query += "  AND  TSPL_SRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ")   AND   TSPL_PI_head.Bill_To_Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ")"
                Else
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        query += "  AND   TSPL_SRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")   AND   TSPL_PI_head.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                End If
                query += " union"
                query += " select tspl_srn_head.SRN_No as[SRN NO], convert(varchar,tspl_srn_head.SRN_Date,103) as [SRN DATE],'Purchase Return' as [Type],TSPL_PR_HEAD.PR_No  as [Doc NO] , convert(varchar,TSPL_PR_HEAD.PR_Date,103) as [Date] , TSPL_PR_DETAIL.Location as [Location] ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name] ,TSPL_PR_HEAD.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name] ,TSPL_PR_DETAIL .PR_Qty  *-1 as [Quantity],TSPL_PR_DETAIL.Item_Net_Amt *-1 as [Amount] ,TSPL_SRN_DETAIL.Item_Net_Amt*-1 as [Reciept Amount]   "
                query += " from TSPL_SRN_HEAD  inner join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No inner join TSPL_PI_HEAD on TSPL_PI_HEAD.Against_SRN=TSPL_SRN_DETAIL.SRN_No "
                query += " inner join TSPL_PI_DETAIL on TSPL_PI_DETAIL.SRN_Id=TSPL_PI_HEAD.Against_SRN and TSPL_PI_DETAIL.PI_No =TSPL_PI_HEAD .PI_No and TSPL_SRN_DETAIL .Item_Code =TSPL_PI_DETAIL .Item_Code   left outer join TSPL_PR_HEAD on  TSPL_PR_HEAD.Against_PI=TSPL_PI_HEAD .PI_No and TSPL_PR_HEAD.Against_SRN=TSPL_PI_HEAD.Against_SRN  "
                query += " left outer join TSPL_PR_DETAIL on TSPL_PR_DETAIL.PI_Id = TSPL_PR_HEAD.Against_PI and TSPL_PR_DETAIL.PR_No=TSPL_PR_HEAD.PR_No AND TSPL_PI_DETAIL.Item_Code=TSPL_PR_DETAIL.Item_Code "
                query += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_code = TSPL_PR_DETAIL.Location  left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PR_HEAD.Vendor_Code	"
                query += " where TSPL_SRN_HEAD.SRN_Date >=Convert(date,'" + fdate + "',103) and TSPL_SRN_HEAD.SRN_Date <=convert(date,'" + tdate + "',103) "
                query += " and TSPL_SRN_DETAIL .Item_Code ='" + code + "' and TSPL_PI_DETAIL.Item_Code='" + code + "' and TSPL_SRN_HEAD.Vendor_Code ='" + ItemCode + "' and TSPL_PI_head.Vendor_Code ='" + ItemCode + "' and TSPL_PR_HEAD.PR_No <>''"
                If multiLocationFinder.arrValueMember IsNot Nothing AndAlso multiLocationFinder.arrValueMember.Count > 0 Then
                    query += "  AND  TSPL_SRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ")   AND   TSPL_PI_head.Bill_To_Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ")"
                Else
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        query += "  AND   TSPL_SRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")   AND   TSPL_PI_head.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                End If
                Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
                If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                    gv1.Visible = True

                    gv1.DataSource = dt2
                    fillgridcolumnvendorDetails()

                    gv1.ReadOnly = True

                    RadPageView1.Visible = True
                    RadPageView1.SelectedPage = RadPageViewPage2
                    ReStoreGridLayoutDetails()

                End If
            End If




        Catch ex As Exception
            Throw New Exception(ex.Message)


        End Try

    End Sub
    Sub fillgridcolumnvendorDetails()
        gv1.Columns("SRN NO").Width = 100
        gv1.Columns("SRN DATE").Width = 150
        gv1.Columns("Type").Width = 150
        gv1.Columns("Doc NO").Width = 100
        gv1.Columns("Date").Width = 150

        gv1.Columns("Location").Width = 120
        gv1.Columns("Location Name").Width = 120
        gv1.Columns("Vendor Code").Width = 120
        gv1.Columns("Vendor Name").Width = 120
        gv1.Columns("Quantity").Width = 120
        gv1.Columns("Amount").Width = 120
        gv1.Columns("Reciept Amount").Width = 120


    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub
    Sub reset()

        dtptodate.Value = clsCommon.GETSERVERDATE()

        dtpfromdate.Value = dtptodate.Value.AddMonths(-1)
        fndSrcCode.Value = ""
        cmbType.SelectedIndex = 0
        txtSrcDesc.Text = ""
        lbltype.Text = "Vendor"
        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.Visible = False

    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        'Dim ss As String = gv1.CurrentCell.RowIndex
        'Dim sss As String = e.RowIndex.ToString
        If e.RowIndex >= 0 Then
            If clsCommon.CompairString(gv1.CurrentRow.Cells("Type").Value, "Purchase Invoice") = CompairStringResult.Equal Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseInvoice, gv1.CurrentRow.Cells("Doc No").Value)
            ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells("Type").Value, "Purchase Return") = CompairStringResult.Equal Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, gv1.CurrentRow.Cells("Doc No").Value)
            End If
        End If
        


    End Sub

    Private Sub SplitContainer2_Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer2.Panel1.Paint

    End Sub

    Private Sub btnReport_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Try
            PageSetupReport_ID = Me.Form_ID & newgv1.Name & Microsoft.VisualBasic.Left(cmbType.SelectedIndex, 3)
            TemplateGridview = newgv1
            fillGridReport(dtpfromdate.Value, dtptodate.Value, fndSrcCode.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Dim success As Boolean = True
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then

            newgv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID & newgv1.Name & Microsoft.VisualBasic.Left(cmbType.SelectedIndex, 3)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            newgv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = newgv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                success = True
            End If

            gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            success = success And obj.SaveData()
            If success Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & newgv1.Name & Microsoft.VisualBasic.Left(cmbType.SelectedIndex, 3), objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        'clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        'clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        'clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)

        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then

                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID & newgv1.Name & Microsoft.VisualBasic.Left(cmbType.SelectedIndex, 3), "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= newgv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To newgv1.Columns.Count - 1
                        newgv1.Columns(ii).IsVisible = False
                        newgv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    newgv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)

                End If

            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub ReStoreGridLayoutDetails()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then

                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)

                End If

            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub



    Private Sub print(ByVal exporter As EnumExportTo)


        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmPurchaseHistory & "'"))
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy"))

            lbltype.Text = "Item"
            If clsCommon.CompairString(cmbType.Text, "Item") = CompairStringResult.Equal Then
                arrHeader.Add("Item : " + txtSrcDesc.Text)
            ElseIf clsCommon.CompairString(cmbType.Text, "Vendor") = CompairStringResult.Equal Then
                arrHeader.Add("Vendor : " + txtSrcDesc.Text)
            End If

            If multiLocationFinder.arrDispalyMember IsNot Nothing AndAlso multiLocationFinder.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(multiLocationFinder.arrDispalyMember))
            End If

            If exporter = EnumExportTo.Excel Then

                If RadPageView1.Pages("RadPageViewPage1").Item.IsSelected = True Then
                  
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(newgv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(newgv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdata(newgv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                End If
                If RadPageView1.Pages("RadPageViewPage2").Item.IsSelected = True Then
                   
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdata(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                End If
            Else
                If RadPageView1.Pages("RadPageViewPage1").Item.IsSelected = True Then
                    transportSql.applyExportTemplate(newgv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(Me.Text, newgv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If

                If RadPageView1.Pages("RadPageViewPage2").Item.IsSelected = True Then
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    'KUNAL > TICKET : 
    Private Sub multiLocationFinder__My_Click(sender As Object, e As EventArgs) Handles multiLocationFinder._My_Click
        Dim qry As String = "SELECT LOCATION_CODE , Location_Desc  FROM TSPL_LOCATION_MASTER where 1=1"
        Try
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            multiLocationFinder.arrValueMember = clsCommon.ShowMultipleSelectForm("purchaseHistoryLocsFinder", qry, "LOCATION_CODE", "Location_Desc", multiLocationFinder.arrValueMember, multiLocationFinder.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub RadPageView1_SelectedPageChanged(sender As Object, e As EventArgs) Handles RadPageView1.SelectedPageChanged
        If clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage1.Name) = CompairStringResult.Equal Then
            PageSetupReport_ID = Me.Form_ID & newgv1.Name & Microsoft.VisualBasic.Left(cmbType.SelectedIndex, 3)
            TemplateGridview = newgv1
        End If
        If clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage2.Name) = CompairStringResult.Equal Then
            PageSetupReport_ID = Me.Form_ID
            TemplateGridview = gv1
        End If
    End Sub
End Class

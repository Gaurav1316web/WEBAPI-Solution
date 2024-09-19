Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Imports System.Globalization
Imports System.Data.SqlClient

Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net

Public Class rptBoothTruckSheet
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Dim isSchemeItem As Boolean = False
    Dim strItemNameOnly As String = ""
    Private Sub rptBoothTruckSheet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        isSchemeItem = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowSchemeItemQty, clsFixedParameterCode.AllowSchemeItemQty, Nothing)) = 1, True, False)

        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        'cboDocumentType.SelectedIndex = 0
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PrintView(False)
    End Sub
    Sub PrintView(ByVal isPrint As Boolean)
        Try


            Dim Shift1 As String = ""
            Dim qry As String = ""
            Dim whrcls As String = ""
            Dim strShift As String = ""
            Dim whrclsShift As String = ""


            If rbtnMorning.IsChecked Then
                'whrclsShift = " and TSPL_BOOKING_MATSER.GatePass_Type  = 'AM' "
                whrclsShift = " and TSPL_DEMAND_BOOKING_MASTER.ShiftType  = 'MORNING' "
            ElseIf rbtnEvening.IsChecked Then
                ' whrclsShift = " and TSPL_BOOKING_MATSER.GatePass_Type  = 'PM' "
                whrclsShift = " and TSPL_DEMAND_BOOKING_MASTER.ShiftType  = 'Evening' "
            End If

            If rbtnMilkType.IsChecked Then
                whrcls += " and TSPL_ITEM_MASTER.Is_FreshItem = 1  "
            ElseIf rbtnProductType.IsChecked Then
                whrcls += " and TSPL_ITEM_MASTER.Is_Ambient = 1 "
            End If

            If txtRouteCode.Value IsNot Nothing Then
                'whrcls += "  And TSPL_BOOKING_DETAIL.Route_No In ('" + clsCommon.myCstr(txtRouteCode.Value) + "')"
                whrcls += " and TSPL_DEMAND_BOOKING_master.Route_No in (" + clsCommon.myCstr(txtRouteCode.Value) + ")"

            End If
            qry = "  SELECT max(TSPL_ITEM_MASTER.Short_Description)Short_Description,max(TSPL_ITEM_MASTER.Short_Description) + 'Amt' as Item_Description,max(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq
            FROM TSPL_DEMAND_BOOKING_DETAIL 
			left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
			left outer join TSPL_DEMAND_BOOKING_master on TSPL_DEMAND_BOOKING_master.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DEMAND_BOOKING_DETAIL.Cust_Code

            where  TSPL_DEMAND_BOOKING_master.Posted = 1 AND convert(date,TSPL_DEMAND_BOOKING_master.Document_Date,103) >=Convert(date,'" & txtFromDate.Value & "',103) 
            and convert(date,TSPL_DEMAND_BOOKING_master.Document_Date,103) <= Convert(date,'" & txtToDate.Value & "',103) " & whrcls & " " & whrclsShift & ""

            'qry = " SELECT  max(TSPL_ITEM_MASTER.Short_Description)Short_Description,max(TSPL_ITEM_MASTER.Short_Description) + 'Amt' as Item_Description,max(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq
            'FROM TSPL_BOOKING_DETAIL  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code
            'where  TSPL_BOOKING_MATSER.Posted = 1 and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) >= Convert(date,'" & txtFromDate.Value & "',103)   and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) <= Convert(date,'" & txtToDate.Value & "',103)  " & whrcls & " " & whrclsShift & ""
            'End If
            qry += " group by TSPL_ITEM_MASTER.Item_Code ORDER BY Sku_Seq "

            Dim itemName2 As String = Nothing
            Dim itemName1 As String = Nothing
            Dim itemNames1 As String = Nothing
            Dim itemNames2 As String = Nothing
            Dim itemNamesQty As String = Nothing
            Dim itemNamesAmt As String = Nothing
            Dim FinalItemNamesQty As String = Nothing
            Dim FinalItemNamesAmt As String = Nothing
            Dim dtitemName As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    itemName1 += "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]" + ","
                    itemName2 += "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]" + ","
                    FinalItemNamesQty += "SUM(XXFINAL.[" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]" + ","
                    FinalItemNamesAmt += "SUM(XXFINAL.[" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]" + ","

                    If i = 0 Then
                        itemNamesQty += "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "],0)"
                        itemNamesAmt += "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "],0)"
                        itemNames1 += "[" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "] "
                        itemNames2 += "[" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "] "
                    Else
                        itemNamesQty += "+" + "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "],0)"
                        itemNamesAmt += "+" + "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "],0)"
                        itemNames1 += ", [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "] "
                        itemNames2 += ", [" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "] "

                    End If
                Next
            Else
                Gv1.DataSource = Nothing
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
                Exit Sub
            End If

            Dim BaseQry As String = ""
            Dim FinalQuery As String = ""
            Dim groupBy As String = ""

            BaseQry += "SELECT (Cust_Code)Boothcode,"
            If rdbEnglish.IsChecked = True Then
                BaseQry += "max(Boothname)Boothname "
            Else
                BaseQry += "max(Boothname)Boothname "
            End If
            BaseQry += " ,max(Route_No)Route_No,max(Route_Desc)Route_Desc,max(Document_Date)Document_Date,max(ShiftType)Shift_Type "


            BaseQry += " ," & itemName1 & " SUM(" & itemNamesQty & ") AS [Total Qty], " & itemName2 & "SUM(" & itemNamesAmt & ") AS [Total Amt] FROM ( SELECT TSPL_CUSTOMER_MASTER.Cust_Code ,"
            If rdbEnglish.IsChecked = True Then
                BaseQry += "TSPL_CUSTOMER_MASTER.Customer_Name as [Boothname],"
            Else
                BaseQry += "TSPL_CUSTOMER_MASTER.customer_name_hindi as [Boothname],"

            End If

            '   BaseQry += "  TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc,
            '   Case When isnull(TSPL_BOOKING_MATSER.GatePass_Type,'') = 'AM' THEN 'AM' else 'PM'   END AS Shift_Type,
            '   TSPL_BOOKING_MATSER.Document_Date, TSPL_ITEM_MASTER.Item_Desc,TSPL_BOOKING_DETAIL.Amount_with_Tax as Amount,
            '   TSPL_ITEM_MASTER.Short_Description,TSPL_ITEM_MASTER.Short_Description + 'Amt' AS Item_Description,
            'TSPL_BOOKING_DETAIL.Unit_code,TSPL_BOOKING_DETAIL.Booking_Qty as CRATE,0 AS Receipt_Amount
            'FROM TSPL_BOOKING_DETAIL LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code
            'LEFT OUTER JOIN TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No
            'LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code
            'left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.zone_code = TSPL_CUSTOMER_MASTER.zone_code
            'left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_BOOKING_DETAIL.Route_No
            'where 2 = 2   and TSPL_BOOKING_MATSER.Posted = 1 " & whrcls & " " & whrclsShift & ""

            BaseQry += " TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc, TSPL_DEMAND_BOOKING_MASTER.Document_Date,
TSPL_DEMAND_BOOKING_MASTER.ShiftType,
TSPL_ITEM_MASTER.Short_Description,TSPL_ITEM_MASTER.Short_Description + 'Amt' AS Item_Description,
TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_DEMAND_BOOKING_DETAIL.Qty as CRATE ,TSPL_SD_SHIPMENT_BOOKING_DETAIL.Unit_code,0 AS Receipt_Amount,TSPL_SD_SHIPMENT_BOOKING_DETAIL.Qty,TSPL_SD_SHIPMENT_BOOKING_DETAIL.Trip_No,TSPL_SD_SHIPMENT_BOOKING_DETAIL.Commission_Amt,TSPL_SD_SHIPMENT_BOOKING_DETAIL.Security_Amt ,ItemNetAmount as Amount
from  TSPL_SD_SHIPMENT_BOOKING_DETAIL
left outer join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_DETAIL.TR_Code=TSPL_SD_SHIPMENT_BOOKING_DETAIL.Booking_TR_Code
left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
Left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
where 2 = 2  
And TSPL_DEMAND_BOOKING_MASTER.Posted = 1 " & whrcls & " " & whrclsShift & "  "
            BaseQry += "And  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) >= CONVERT(DATE, '" & txtFromDate.Value & "', 103)  and   convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) <= CONVERT(DATE, '" & txtToDate.Value & "', 103) "
            If rbtnMorning.IsChecked Then
                FinalQuery += " and ShiftType = 'MORNING' "
            ElseIf rbtnEvening.IsChecked Then
                FinalQuery += " and ShiftType = 'Evening'"
            End If

            BaseQry += ") AS xx PIVOT (SUM(CRATE)  FOR Short_Description IN (" & itemNames1 & ") ) AS pivot_crate PIVOT (SUM(Amount)  FOR Item_Description IN (" & itemNames2 & ") ) AS pivot_net_amt  "
            If rbtnMorning.IsChecked Then
                FinalQuery += " and XXX.ShiftType = 'MORNING' "
            ElseIf rbtnEvening.IsChecked Then
                FinalQuery += " and XXX.ShiftType = 'Evening'"
            End If

            FinalQuery = "" & BaseQry & ""

            FinalQuery += "Group BY  Cust_Code order by Cust_Code"
            'End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(FinalQuery)

            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterView.Refresh()
            Gv1.GroupDescriptors.Clear()
            Gv1.EnableFiltering = True
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                Gv1.BestFitColumns()
                'View()
                'SetGridFormation()
                ReStoreGridLayout()
                Gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    'Sub View()

    '    If Gv1.Rows.Count > 0 Then
    '        Dim view As New ColumnGroupsViewDefinition()

    '        view.ColumnGroups.Add(New GridViewColumnGroup(" "))
    '        view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())


    '        'view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Distributor Code").Name)
    '        '    view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Distributor Name").Name)
    '        '    If rdbHindi.IsChecked = True Then
    '        '        Gv1.Columns("Distributor Code").HeaderText = "वितरणकर्ता का कोड"
    '        '        Gv1.Columns("Distributor Name").HeaderText = "वितरणकर्ता का नाम"
    '        '    End If

    '        view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Booth Code").Name)
    '        view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Booth Name").Name)
    '        If rdbHindi.IsChecked = True Then
    '            Gv1.Columns("Booth Code").HeaderText = "ग्राहक का कोड"
    '            Gv1.Columns("Booth Name").HeaderText = "ग्राहक का नाम"
    '        End If



    '        Dim dblGroupNo As Integer = 0
    '        Dim str As String
    '        Dim strArr() As String
    '        Dim count As Integer
    '        str = strItemNameOnly
    '        strArr = str.Split(",")
    '        For count = 0 To strArr.Length - 1
    '            'MsgBox(strArr(count))
    '            Dim strName As String = strArr(count).Replace("[", " ").Trim()
    '            strName = strName.Replace("]", " ").Trim()
    '            dblGroupNo = dblGroupNo + 1
    '            view.ColumnGroups.Add(New GridViewColumnGroup(strName))
    '            view.ColumnGroups(dblGroupNo).Rows.Add(New GridViewColumnGroupRow())
    '            view.ColumnGroups(dblGroupNo).Rows(0).ColumnNames.Add(Gv1.Columns(strName + "_Crate").Name)
    '            If rdbEnglish.IsChecked = True Then
    '                Gv1.Columns(strName + "_Crate").HeaderText = "Crate"
    '            Else
    '                Gv1.Columns(strName + "_Crate").HeaderText = "कैरेट"
    '            End If

    '            view.ColumnGroups(dblGroupNo).Rows(0).ColumnNames.Add(Gv1.Columns(strName + "_Pouch").Name)
    '            If rdbEnglish.IsChecked = True Then
    '                Gv1.Columns(strName + "_Pouch").HeaderText = "Pouch"
    '            Else
    '                Gv1.Columns(strName + "_Pouch").HeaderText = "थैली"
    '            End If

    '            view.ColumnGroups(dblGroupNo).Rows(0).ColumnNames.Add(Gv1.Columns(strName + "_Ltr").Name)
    '            Gv1.Columns(strName + "_Ltr").IsVisible = False
    '            view.ColumnGroups(dblGroupNo).Rows(0).ColumnNames.Add(Gv1.Columns(strName + "_Amount").Name)
    '            Gv1.Columns(strName + "_Amount").IsVisible = False
    '        Next


    '        'For i As Integer = 0 To Gv1.ColumnNames.Count - 1 Step 4

    '        '    Dim aa As String = Gv1.Columns(i + 1).HeaderText()
    '        '    Dim aaa As String = aa
    '        '    Dim strItemName As String = ""
    '        '    If aa.Contains("_Crate") = True Then
    '        '        strItemName = clsCommon.myCstr(Replace(aaa, "_Crate", " "))
    '        '    ElseIf aa.Contains("_Pouch") = True Then
    '        '        strItemName = clsCommon.myCstr(Replace(aaa, "_Pouch", " "))
    '        '    ElseIf aa.Contains("_Ltr") = True Then
    '        '        strItemName = clsCommon.myCstr(Replace(aaa, "_Ltr", " "))
    '        '    ElseIf aa.Contains("_Amount") = True Then
    '        '        strItemName = clsCommon.myCstr(Replace(aaa, "_Amount", " "))
    '        '    End If
    '        '    If clsCommon.myLen(strItemName) > 0 Then
    '        '        dblGroupNo = dblGroupNo + 1
    '        '        view.ColumnGroups.Add(New GridViewColumnGroup(strItemName))
    '        '        view.ColumnGroups(dblGroupNo).Rows.Add(New GridViewColumnGroupRow())
    '        '        view.ColumnGroups(dblGroupNo).Rows(0).ColumnNames.Add(Gv1.Columns(strItemName + "_Crate"))
    '        '        view.ColumnGroups(dblGroupNo).Rows(0).ColumnNames.Add(Gv1.Columns(strItemName + "_Pouch"))
    '        '        view.ColumnGroups(dblGroupNo).Rows(0).ColumnNames.Add(Gv1.Columns(strItemName + "_Ltr"))
    '        '        view.ColumnGroups(dblGroupNo).Rows(0).ColumnNames.Add(Gv1.Columns(strItemName + "_Amount"))
    '        '    End If
    '        'Next
    '        If rdbEnglish.IsChecked = True Then
    '            view.ColumnGroups.Add(New GridViewColumnGroup("Total"))
    '        Else
    '            view.ColumnGroups.Add(New GridViewColumnGroup("कुल"))
    '        End If

    '        view.ColumnGroups(dblGroupNo + 1).Rows.Add(New GridViewColumnGroupRow())
    '        view.ColumnGroups(dblGroupNo + 1).Rows(0).ColumnNames.Add(Gv1.Columns("Grand Total").Name)
    '        If rdbEnglish.IsChecked = True Then
    '            Gv1.Columns("Grand Total").HeaderText = "Grand Total"
    '        Else
    '            Gv1.Columns("Grand Total").HeaderText = "कुल योग"
    '        End If
    '        view.ColumnGroups(dblGroupNo + 1).Rows(0).ColumnNames.Add(Gv1.Columns("Total In Ltr").Name)
    '        If rdbEnglish.IsChecked = True Then
    '            Gv1.Columns("Total In Ltr").HeaderText = "Total In Ltr"
    '        Else
    '            Gv1.Columns("Total In Ltr").HeaderText = "कुल लीटर में"
    '        End If
    '        Gv1.ViewDefinition = view
    '    End If
    'End Sub

    Private Sub txtRouteCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRouteCode._MYValidating
        Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No AS Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER"
        txtRouteCode.Value = clsCommon.ShowSelectForm("Demand@Print@DayshiftWise", qry, "Code", "", txtRouteCode.Value, "Code", isButtonClicked)
        lblRouteCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Desc as Name from TSPL_ROUTE_MASTER where Route_No ='" + txtRouteCode.Value + "' "))
    End Sub
    Sub Reset()
        ' cboDocumentType.SelectedIndex = 0
        txtRouteCode.Value = ""
        lblRouteCode.Text = ""

        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        Dim whrcls As String = ""
        If rbtnMilkType.IsChecked Then
            whrcls += " and TSPL_ITEM_MASTER.Is_FreshItem = 1  "
        ElseIf rbtnProductType.IsChecked Then
            whrcls += " and TSPL_ITEM_MASTER.Is_Ambient = 1 "
        End If
        Dim whrclsShift As String = ""
        Dim Shift As String = ""
        If rbtnMorning.IsChecked Then
            ' whrclsShift = " and TSPL_BOOKING_MATSER.GatePass_Type  = 'AM' "
            whrclsShift = " and TSPL_DEMAND_BOOKING_MASTER.ShiftType  = 'Morning' "

            Shift = "Morning"
        ElseIf rbtnEvening.IsChecked Then
            'whrclsShift = " and TSPL_BOOKING_MATSER.GatePass_Type  = 'PM' "
            whrclsShift = " and TSPL_DEMAND_BOOKING_MASTER.ShiftType  = 'Evening' "

            Shift = "Evening"
        Else
            Shift = "Both"

        End If
        Dim qry As String = "( SELECT TSPL_COMPANY_MASTER.Comp_Name ,tspl_transport_master.Transporter_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.City_Code,TSPL_COMPANY_MASTER.Pincode,TSPL_COMPANY_MASTER.State,TSPL_COMPANY_MASTER.Phone1 ,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code  ,"
        If rdbEnglish.IsChecked = True Then
            qry += "(TSPL_ITEM_MASTER.Alies_Name)Short_Description,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code + ' ' + TSPL_CUSTOMER_MASTER.Customer_Name  as [BoothName], "
        ElseIf rdbHindi.IsChecked = True Then
            ' qry += " (TSPL_ITEM_MASTER.Alies_Name)Short_Description, (TSPL_ITEM_MASTER.Alies_Name_Hindi)  as Item_Description,  "
            qry += " (TSPL_ITEM_MASTER.Alies_Name_Hindi)Short_Description,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code  + ' ' + TSPL_CUSTOMER_MASTER.Customer_Name_Hindi  as [BoothName], "
        End If
        qry += "TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,'" + Shift + "' AS Shift_Type,TSPL_DEMAND_BOOKING_MASTER.Document_Date, TSPL_ITEM_MASTER.Item_Desc,
TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount as Amount,TSPL_ITEM_MASTER.Short_Description + 'Amt' AS Item_Description,
         TSPL_SD_SHIPMENT_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.Qty as CRATE,
		    		Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_code='Pouch' Then TSPL_DEMAND_BOOKING_DETAIL.Qty Else 0 End Pouch,
		 0 AS Receipt_Amount 
FROM TSPL_SD_SHIPMENT_BOOKING_DETAIL 
left outer join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_DETAIL.TR_Code=TSPL_SD_SHIPMENT_BOOKING_DETAIL.Booking_TR_Code
left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
Left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.zone_code = TSPL_CUSTOMER_MASTER.zone_code
		 left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_DEMAND_BOOKING_master.Comp_Code
		  left outer join tspl_vehicle_master on tspl_vehicle_master.vehicle_id =TSPL_DEMAND_BOOKING_DETAIL.vehicle_code
		 left outer join tspl_transport_master on tspl_transport_master.Transport_Id=tspl_vehicle_master.Transport_Id
         where 2 = 2   and TSPL_DEMAND_BOOKING_MASTER.Posted = 1 " & whrcls & "    And TSPL_DEMAND_BOOKING_master.Route_No In ('" + clsCommon.myCstr(txtRouteCode.Value) + "')   " & whrclsShift & "  And
 convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) >= CONVERT(DATE, '" & txtFromDate.Value & "', 103)  and   convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) <= CONVERT(DATE, '" & txtToDate.Value & "', 103) )
"
        Dim dtPrint As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtPrint IsNot Nothing And dtPrint.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            If rdbEnglish.IsChecked = True Then
                If chkPouch.Checked = True Then
                    frmCRV.funreport(False, CrystalReportFolder.SalesReport, dtPrint, "rptDairySaleBoothTruckSheetPouch", "Dairy Sale Booth Truck Sheetr")

                Else
                    frmCRV.funreport(False, CrystalReportFolder.SalesReport, dtPrint, "rptDairySaleBoothTruckSheet", "Dairy Sale Booth Truck Sheetr")

                End If
            Else
                If chkPouch.Checked = True Then
                    frmCRV.funreport(False, CrystalReportFolder.SalesReport, dtPrint, "rptDairySaleBoothTruckSheetInHindiPouch", "Dairy Sale Booth Truck Sheetr")

                Else
                    frmCRV.funreport(False, CrystalReportFolder.SalesReport, dtPrint, "rptDairySaleBoothTruckSheetInHindi", "Dairy Sale Booth Truck Sheet In Hindi")
                End If
            End If
            frmCRV = Nothing
            Return
            'ElseIf
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub

End Class


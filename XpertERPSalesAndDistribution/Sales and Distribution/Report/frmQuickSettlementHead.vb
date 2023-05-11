'                                            created by = Priit (30/11/2012   04:55 PM)
'                                            Modified by = Priit (24/12/2012   12:55 PM)

'                                            Modified by = Priit (08/01/2013   11:05 AM)
'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 31/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------
'by vipin for pdf on 08/02/2013
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Telerik.Collections.Generic
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common

Public Class FrmQuickSettlementHead
    Inherits FrmMainTranScreen


    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
    End Sub
    Sub LoadLocation()
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Sub LoadRoute()
        Dim qry As String = "select Route_No as [Route],Route_Desc as [Route Description] from TSPL_ROUTE_MASTER"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Route"
        cbgRoute.DisplayMember = "Route Description"
    End Sub

    Sub LoadSalesPerson()
        Dim qry As String = "Select EMP_CODE as [SalesPerson Code],Emp_Name as [SalesPerson Name] from TSPL_EMPLOYEE_MASTER"
        cbgSalesPerson.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSalesPerson.ValueMember = "SalesPerson Code"
        cbgSalesPerson.DisplayMember = "SalesPerson Name"
    End Sub
    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged, chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub
    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked
    End Sub

    Private Sub chkLocatioAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocatioAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocatioAll.IsChecked
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ChkSalesAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkSalesAll.ToggleStateChanged
        cbgSalesPerson.Enabled = Not ChkSalesAll.IsChecked
    End Sub

    Private Sub Print()
        Try
            Dim strLocAll, strRouteAll, strSalesAll, strPost As String
            strPost = ""
            If chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one route Category or select ALL ")
                Return
            ElseIf chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one location or select ALL ")
                Return
            ElseIf chlSalesSelect.IsChecked = True AndAlso cbgSalesPerson.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Salesperson or select ALL ")
                Return
            ElseIf rbtnCompanySelect.IsChecked = True AndAlso cbgCompany.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one company or select ALL ")
                Return

            End If
            Dim ArrDBName As ArrayList = Nothing
            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If


            If ArrDBName Is Nothing OrElse ArrDBName.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one company or select ALL")
                Return
            End If


            If rbtnAll.IsChecked Then
                strPost = ""
            ElseIf rbtnPost.IsChecked Then
                strPost = " and tspl_QuickSettleMent.Post='Y'"
            End If
            If chkLocatioAll.IsChecked = True Then
                strLocAll = "Y"
            Else
                strLocAll = "N"
            End If
            If chkRouteAll.IsChecked = True Then
                strRouteAll = "Y"
            Else
                strRouteAll = "N"
            End If
            If ChkSalesAll.IsChecked = True Then
                strSalesAll = "Y"
            Else
                strSalesAll = "N"
            End If
            Dim strItemCodestring, strItemCode, strMainItemCode, strmainItemCodeString, strsum As String
            strItemCode = ""
            strsum = ""
            strItemCodestring = ""
            strmainItemCodeString = ""
            Dim dt As DataTable
            Dim str1 As String
            Dim qry As String = "select distinct " + clsCommon.ReplicateDBString + "TSPL_SettleMent_Master.Description from   " + clsCommon.ReplicateDBString + "TSPL_QuickSettleMent_Detail LEFT OUTER JOIN " & _
                 "" + clsCommon.ReplicateDBString + "TSPL_QuickSettleMent ON " + clsCommon.ReplicateDBString + "TSPL_QuickSettleMent_Detail.Quick_SettleMent_Id = " + clsCommon.ReplicateDBString + "TSPL_QuickSettleMent.Quick_SettleMent_Id LEFT OUTER JOIN " & _
                 "" + clsCommon.ReplicateDBString + "TSPL_SettleMent_Master ON " + clsCommon.ReplicateDBString + "TSPL_QuickSettleMent_Detail.SettleMent_Code = " + clsCommon.ReplicateDBString + "TSPL_SettleMent_Master.SettleMentCode " & _
                 "WHERE (" + clsCommon.ReplicateDBString + "TSPL_QuickSettleMent_Detail.Amount <> 0) and   " + clsCommon.ReplicateDBString + "TSPL_QuickSettleMent.Transfer_Date >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND  " & _
                "  " + clsCommon.ReplicateDBString + "TSPL_QuickSettleMent.Transfer_Date <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'   "

            qry = clsCommon.GetQueryWithAllSelectedDataBase(qry, ArrDBName, False)

            qry = "select distinct Description from ( " & qry & " ) a "
            dt = clsDBFuncationality.GetDataTable(qry)


            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    'While dr.Read
                    strItemCode = CStr(dr(0).ToString())
                    strItemCodestring = strItemCodestring & "[" & strItemCode & "]" & ","

                    strMainItemCode = CStr(dr(0).ToString())
                    strmainItemCodeString = strmainItemCodeString & "  isnull(" & "(" & "[" & strItemCode & "]" & " ) " & ",0)  " & "as  " & "[" & strItemCode & "]" & ","
                    strsum = strsum & " isnull(" & "Sum(" & "[" & strItemCode & "]" & " ) " & ",0)" & "+"
                Next
            End If
            'End While
            If strItemCode <> "" Then

                strItemCodestring = strItemCodestring.Substring(0, strItemCodestring.Length - 1)
                strmainItemCodeString = strmainItemCodeString.Substring(0, strmainItemCodeString.Length - 1)
                strsum = strsum.Substring(0, strsum.Length - 1)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If

            str1 = "select From_Location as Loc,FromLoc_Desc as LocDesc,Quick_SettleMent_Id, " & _
            "Quick_Settlement_Date,Transfer_Number,Route_No,Salesman_code,Salesman, " & _
            "" & strmainItemCodeString & " from " & _
            "(select From_Location,FromLoc_Desc," + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_SettleMent_Id, " & _
            "Quick_Settlement_Date,Transfer_Number,Route_No,Salesman_code,Salesman,Amount, " & _
            "" + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.Description as SettDesc from  " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent left outer join  " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail on " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_SettleMent_Id=" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.Quick_SettleMent_Id left outer join " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD on " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No left outer join " & _
            "" + clsCommon.ReplicateDBString + "tspl_SettleMent_Master on " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.SettleMent_Code=" + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMentCode " & _
            "WHERE   " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            " " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  " & strPost & " "

            If strLocAll = "N" Then
                str1 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                str1 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                str1 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            str1 += ") a pivot ( sum(amount) for SettDesc in (" & strItemCodestring & ")) as  Pvt1"


            qry = clsCommon.GetQueryWithAllSelectedDataBase(str1, ArrDBName, False)

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.EnableFiltering = True

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                gv1.DataSource = dt
                SetGridFormationOFGV1()
            End If

            gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormationOFGV1()
        gv1.ShowGroupPanel = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
       
        gv1.Columns("LOc").IsVisible = True
        gv1.Columns("LOc").Width = 50
        gv1.Columns("LOc").HeaderText = "Location"

        gv1.Columns("LocDesc").IsVisible = True
        gv1.Columns("LocDesc").Width = 100
        gv1.Columns("LocDesc").HeaderText = "Location Desc"

        gv1.Columns("Quick_SettleMent_Id").IsVisible = True
        gv1.Columns("Quick_SettleMent_Id").Width = 100
        gv1.Columns("Quick_SettleMent_Id").HeaderText = "Settlement No"

        gv1.Columns("Quick_Settlement_Date").IsVisible = True
        gv1.Columns("Quick_Settlement_Date").Width = 60
        gv1.Columns("Quick_Settlement_Date").HeaderText = "Settlement Date"

        gv1.Columns("Transfer_Number").IsVisible = True
        gv1.Columns("Transfer_Number").Width = 100
        gv1.Columns("Transfer_Number").HeaderText = "Loadout No"


        gv1.Columns("Route_No").IsVisible = True
        gv1.Columns("Route_No").Width = 100
        gv1.Columns("Route_No").HeaderText = "Route No"

        gv1.Columns("Salesman_code").IsVisible = True
        gv1.Columns("Salesman_code").Width = 100
        gv1.Columns("Salesman_code").HeaderText = "Salesman Code"

        gv1.Columns("Salesman").IsVisible = True
        gv1.Columns("Salesman").Width = 100
        gv1.Columns("Salesman").HeaderText = "Salesman Name"

        Dim strItemCode As String
        Dim intCount As Integer

        For ii As Integer = 8 To gv1.Columns.Count - 1
            strItemCode = gv1.Columns(ii).FieldName
            gv1.Columns("" & strItemCode & "").IsVisible = True
            gv1.Columns("" & strItemCode & "").Width = 80
            gv1.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
        Next

        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 7 To gv1.Columns.Count - 1
            intCount = intCount + 1
            strItemCode = gv1.Columns(ii).FieldName
            Dim item1 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
        Next
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)



    End Sub
    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        Print()

    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.DailySettlement)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmQuickSettlementHead_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadLocation()
        chkLocatioAll.IsChecked = True
        LoadRoute()
       
        LoadSalesPerson()
        ChkSalesAll.IsChecked = True
        chkRouteAll.IsChecked = True
        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        LoadCompany()
        'rbtnCompanyAll.IsChecked = True
        rbtnCompanySelect.IsChecked = True
        Dim arrDBName As New ArrayList()
        arrDBName.Add(objCommonVar.CurrDatabase)
        cbgCompany.CheckedValue = arrDBName


    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        'Try
        '    Print()
        '    Dim arrHeader As List(Of String) = New List(Of String)()
        '    Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
        '    arrHeader.Add(strtemp)

        '    If chkRouteSelect.IsChecked Then
        '        strtemp = ""
        '        For Each Str As String In cbgRoute.CheckedDisplayMember
        '            If clsCommon.myLen(strtemp) > 0 Then
        '                strtemp += ", "
        '            End If
        '            strtemp += Str
        '        Next
        '        arrHeader.Add("Route : " + strtemp)
        '    End If
        '    If chlSalesSelect.IsChecked Then
        '        strtemp = ""
        '        For Each Str As String In cbgSalesPerson.CheckedDisplayMember
        '            If clsCommon.myLen(strtemp) > 0 Then
        '                strtemp += ", "
        '            End If
        '            strtemp += Str
        '        Next
        '        arrHeader.Add("Salesname : " + strtemp)
        '    End If

        '    If chkLocationSelect.IsChecked Then
        '        strtemp = ""
        '        For Each Str As String In cbgLocation.CheckedDisplayMember
        '            If clsCommon.myLen(strtemp) > 0 Then
        '                strtemp += ", "
        '            End If
        '            strtemp += Str
        '        Next
        '        arrHeader.Add("Location Segment : " + strtemp)
        '    End If



        '    clsCommon.MyExportToExcel("Settlement Heads Report", gv1, arrHeader, Me.Text)
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'End Try
    End Sub

    Sub printdata(ByVal exporter As EnumExportTo)
        Try
            Print()
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)

            If chkRouteSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgRoute.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Route : " + strtemp)
            End If
            If chlSalesSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgSalesPerson.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Salesname : " + strtemp)
            End If

            If chkLocationSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location Segment : " + strtemp)
            End If



            ' clsCommon.MyExportToExcel("Settlement Heads Report", gv1, arrHeader, Me.Text)


            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Settlement Heads Report", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Settlement Heads Report", gv1, arrHeader, "Settlement Heads Report", True)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        printdata(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        printdata(EnumExportTo.PDF)
    End Sub
End Class

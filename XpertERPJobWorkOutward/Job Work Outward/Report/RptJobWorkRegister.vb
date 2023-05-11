'================Create by Sanjeet (07/09/2017)========
Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class RptJobWorkRegister
    Inherits FrmMainTranScreen
    Dim whrLocaion_for_vendor_filter As String = ""
    Dim OnlyLocaion_for_vendor_filter_Check As String = ""

    Private Sub RptJobWorkRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
        LoadLocation()
        rbtnLocationAll.IsChecked = True
    End Sub

    Sub LoadLocation()
        gvLocation.DataSource = Nothing
        ' Dim whrCls As String = " and ((Is_Section='N' and Is_Sub_Location='N' and Location_Type IN ('Physical','Logical','Virtual') ) or (CSA_Type='Y') )"
        Dim whrCls As String = " and Location_Code in (select Main_Location_Code from TSPL_LOCATION_MASTER where Is_Jobwork=1 and TSPL_LOCATION_MASTER.Location_Type='Physical')"
        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME from TSPL_LOCATION_MASTER where 1=1 " + whrCls + ""
        'If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.stockRecoNewJR) = CompairStringResult.Equal Then
        '    qry = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME,Jobwork_Vendor,Is_Sub_Location from TSPL_LOCATION_MASTER where len(coalesce(Jobwork_Vendor,''))>0 and Is_Sub_Location='Y'"
        'End If
        qry += " order by Location_Code"
        gvLocation.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvLocation.Columns("SEL").ReadOnly = False
        gvLocation.Columns("SEL").Width = 30
        gvLocation.Columns("SEL").HeaderText = " "

        gvLocation.Columns("CODE").ReadOnly = True
        gvLocation.Columns("CODE").Width = 100
        gvLocation.Columns("CODE").HeaderText = "Code"

        gvLocation.Columns("NAME").ReadOnly = True
        gvLocation.Columns("NAME").Width = 200
        gvLocation.Columns("NAME").HeaderText = "Description"

        gvLocation.ShowGroupPanel = False
        gvLocation.AllowAddNewRow = False
        gvLocation.AllowColumnReorder = False
        gvLocation.AllowRowReorder = False
        gvLocation.EnableSorting = False
        gvLocation.ShowFilteringRow = True
        gvLocation.EnableFiltering = True
        gvLocation.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvLocation.MasterTemplate.ShowRowHeaderColumn = True
    End Sub
    Private Sub RptJobWorkRegister_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        txtVendor.arrValueMember = Nothing
        ' txtLocation.arrValueMember = Nothing

        'txtLocation.Enabled = True
        txtVendor.Enabled = True
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = Gv1
        GetData()
    End Sub
    Public Sub GetLocationSublocation()

        Dim strLocCond As String = ""
        ' Dim strWhrCatg As String = ""
        Dim LocationFirstTime As Integer = 0
        Dim wherLocation As String = ""
        whrLocaion_for_vendor_filter = ""
        OnlyLocaion_for_vendor_filter_Check = ""
        If rbtnLocationSelect.IsChecked Then
            Dim IsApplicable As Boolean = False
            For ii As Integer = 0 To gvLocation.RowCount - 1
                If clsCommon.myCBool(gvLocation.Rows(ii).Cells("SEL").Value) Then
                    LocationFirstTime += 1

                    If IsApplicable Then
                        whrLocaion_for_vendor_filter += " Or "
                    End If
                    whrLocaion_for_vendor_filter += "  ((tspl_location_master.Main_Location_Code) = '" + clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) + "') "
                    OnlyLocaion_for_vendor_filter_Check += "  '" + clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) + "'  "
                    IsApplicable = True
                    Dim arr As Dictionary(Of String, Object) = gvLocation.Rows(ii).Tag
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        whrLocaion_for_vendor_filter += " and tspl_location_master.Location_Code in ("
                        Dim isFirstTime As Boolean = True
                        For Each strInn As String In arr.Keys
                            If Not isFirstTime Then
                                whrLocaion_for_vendor_filter += ","
                            End If
                            whrLocaion_for_vendor_filter += "'" + strInn + "'"
                            isFirstTime = False
                        Next
                        whrLocaion_for_vendor_filter += ")"
                    End If
                End If
            Next
            If Not IsApplicable Then
                Throw New Exception("Please select at least one location")
            End If
            whrLocaion_for_vendor_filter = " and (" + whrLocaion_for_vendor_filter + ")"

        End If
    End Sub
    Sub GetData()
        Try
            If clsCommon.myCDate(fromDate.Value) > clsCommon.myCDate(ToDate.Value) Then
                Throw New Exception("From date shoud not be greater then To Date.")
            End If
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.ShowGroupPanel = False
            Gv1.EnableFiltering = True
            txtVendor.Enabled = False
            Dim dt As DataTable = Nothing
            Dim strRunQuery As String = GetBaseQuery()
            dt = clsDBFuncationality.GetDataTable(strRunQuery)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.DataSource = dt
                Gv1.GroupDescriptors.Clear()
                Gv1.AllowAddNewRow = False
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                FormatGrid()
                ReStoreGridLayout()
                ViewGrid()
                RadPageView1.SelectedPage = RadPageViewPage2
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    
    Sub FormatGrid()
        ' Dim strItemCode, head2 As String

        'Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next
        Gv1.Columns("Trans_Date").IsVisible = True
        Gv1.Columns("Trans_Date").Width = 100
        Gv1.Columns("Trans_Date").HeaderText = " Date"
        Gv1.Columns("Trans_Date").FormatString = "{0:d}"


        Gv1.Columns("vendor_code").IsVisible = True
        Gv1.Columns("vendor_code").Width = 100
        Gv1.Columns("vendor_code").HeaderText = "Vendor Code"

        Gv1.Columns("Vendor_Name").IsVisible = True
        Gv1.Columns("Vendor_Name").Width = 250
        Gv1.Columns("Vendor_Name").HeaderText = "Vendor Name"


        Gv1.Columns("Opening_Fat_KG").IsVisible = True
        Gv1.Columns("Opening_Fat_KG").Width = 80
        Gv1.Columns("Opening_Fat_KG").HeaderText = "Opening FAT KG"

        Gv1.Columns("Opening_SNF_KG").IsVisible = True
        Gv1.Columns("Opening_SNF_KG").Width = 80
        Gv1.Columns("Opening_SNF_KG").HeaderText = "Opening SNF KG"

        Gv1.Columns("Issue_Fat_KG").IsVisible = True
        Gv1.Columns("Issue_Fat_KG").Width = 80
        Gv1.Columns("Issue_Fat_KG").HeaderText = "Issue FAT KG"

        Gv1.Columns("Issue_SNFKG").IsVisible = True
        Gv1.Columns("Issue_SNFKG").Width = 80
        Gv1.Columns("Issue_SNFKG").HeaderText = "Issue SNF KG"

        Gv1.Columns("Received_Fat_KG").IsVisible = True
        Gv1.Columns("Received_Fat_KG").Width = 80
        Gv1.Columns("Received_Fat_KG").HeaderText = "Received Fat KG"

        Gv1.Columns("Received_SNFKG").IsVisible = True
        Gv1.Columns("Received_SNFKG").Width = 80
        Gv1.Columns("Received_SNFKG").HeaderText = "Received SNF KG"

        Gv1.Columns("Return_FatKG").IsVisible = True
        Gv1.Columns("Return_FatKG").Width = 80
        Gv1.Columns("Return_FatKG").HeaderText = "Return Fat KG"

        Gv1.Columns("Return_SNFKG").IsVisible = True
        Gv1.Columns("Return_SNFKG").Width = 80
        Gv1.Columns("Return_SNFKG").HeaderText = "Return SNF KG"

        Gv1.Columns("Short_FatKG").IsVisible = True
        Gv1.Columns("Short_FatKG").Width = 80
        Gv1.Columns("Short_FatKG").HeaderText = "Short FAT Kg"

        Gv1.Columns("Short_SNFKG").IsVisible = True
        Gv1.Columns("Short_SNFKG").Width = 80
        Gv1.Columns("Short_SNFKG").HeaderText = "Short SNF Kg"

        Gv1.Columns("Excess_fatKG").IsVisible = True
        Gv1.Columns("Excess_fatKG").Width = 80
        Gv1.Columns("Excess_fatKG").HeaderText = "Excess FAT Kg"

        Gv1.Columns("Excess_SNFKG").IsVisible = True
        Gv1.Columns("Excess_SNFKG").Width = 80
        Gv1.Columns("Excess_SNFKG").HeaderText = "Excess SNF Kg"

        Gv1.Columns("CL_Fat_KG").IsVisible = True
        Gv1.Columns("CL_Fat_KG").Width = 80
        Gv1.Columns("CL_Fat_KG").HeaderText = "Closing FAT Kg"

        Gv1.Columns("CL_SNF_KG").IsVisible = True
        Gv1.Columns("CL_SNF_KG").Width = 80
        Gv1.Columns("CL_SNF_KG").HeaderText = "Closing SNF Kg"


        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item1 As New GridViewSummaryItem("Opening_Fat_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Opening_SNF_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Issue_Fat_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Issue_SNFKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Received_Fat_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("Received_SNFKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Short_FatKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("Short_SNFKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("CL_Fat_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("CL_SNF_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Sub ViewGrid()
        If Gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Trans_Date").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Vendor_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Vendor_Name").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Opening Stock"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Opening_Fat_KG").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Opening_SNF_KG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Issued to Job Worker"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Issue_Fat_KG").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Issue_SNFKG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Received from Job Worker"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Received_Fat_KG").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Received_SNFKG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Return from Job Worker"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Return_FatKG").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Return_SNFKG").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Short/Excess(+/-)"))
            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("Short_FatKG").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("Short_SNFKG").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("Excess_fatKG").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("Excess_SNFKG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Closing Stock"))
            view.ColumnGroups(6).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(Gv1.Columns("CL_Fat_KG").Name)
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(Gv1.Columns("CL_SNF_KG").Name)


            Gv1.ViewDefinition = view
        End If

    End Sub

    'Private Sub txtLocation__My_Click(sender As Object, e As EventArgs)

    '    ' Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Is_Jobwork=1 and TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER.Is_Sub_Location='Y' "
    '    Dim qry As String = " select distinct xyz.Code , xyz.Name  from (select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  where TSPL_LOCATION_MASTER.Is_Jobwork=1 and TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER.Is_Sub_Location='Y' union  select Main_Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  where Location_Code in (select Location_Code  from TSPL_LOCATION_MASTER  where TSPL_LOCATION_MASTER.Is_Jobwork=1 and TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER.Is_Sub_Location='Y'))xyz "
    '    txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    '    ' txtVendor.Enabled = True
    'End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        GetLocationSublocation()
        If clsCommon.myLen(OnlyLocaion_for_vendor_filter_Check) > 0 Then
            Dim qry As String = " SELECT Vendor_Code as Code,Vendor_Name as Name FROM TSPL_VENDOR_MASTER WHERE Vendor_Code IN(select Jobwork_Vendor FROM TSPL_LOCATION_MASTER WHERE 2= 2  " + whrLocaion_for_vendor_filter + ")"
            txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
        Else
            Dim qry As String = " SELECT Vendor_Code as Code,Vendor_Name as Name FROM TSPL_VENDOR_MASTER WHERE Vendor_Code IN(select Jobwork_Vendor from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Is_Jobwork=1 and TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER.Is_Sub_Location='Y')"
            txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
        End If
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try

            If Gv1.DataSource Is Nothing OrElse Gv1.Rows.Count <= 0 Then
                Throw New Exception("No data found in grid")
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptJobWorkRegister & "'"))
            If txtVendor.arrDispalyMember IsNot Nothing AndAlso txtVendor.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If

            If rbtnLocationSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each grow As GridViewRowInfo In gvLocation.Rows
                    If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
                        If clsCommon.CompairString(strLoca, "") = CompairStringResult.Equal Then
                            strLoca += clsCommon.myCstr(grow.Cells("NAME").Value)
                        Else
                            strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
                        End If
                    End If
                Next
                arrHeader.Add("Location : " + strLoca)
            End If

            If exporter = EnumExportTo.Excel Then
                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Public Function GetBaseQuery() As String
        Dim From_Date As String = clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy")
        Dim To_Date As String = clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy")
        Dim strLoacation1 As String = ""
        Dim strLoacation2 As String = ""
        Dim strVendor As String = ""

        '*********************************************************************************************************************************************
        Dim strLocCond As String = ""
        Dim strWhrCatg As String = ""
        Dim LocationFirstTime As Integer = 0
        Dim LocationAddress As String = String.Empty
        Dim wherLocation As String = ""

        If rbtnLocationSelect.IsChecked Then
            Dim IsApplicable As Boolean = False
            For ii As Integer = 0 To gvLocation.RowCount - 1
                If clsCommon.myCBool(gvLocation.Rows(ii).Cells("SEL").Value) Then
                    LocationFirstTime += 1
                    If LocationFirstTime = 1 Then
                        LocationAddress = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress] from TSPL_LOCATION_MASTER where Location_Code= '" & clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) & "'")
                    End If
                    If IsApplicable Then
                        strWhrCatg += " Or "
                    End If
                    'strWhrCatg += " ((case when tspl_location_master.Is_Section='N' and tspl_location_master.Is_Sub_Location='N' then tspl_location_master.Location_Code else tspl_location_master.Main_Location_Code end) = '" + clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) + "') "
                    strWhrCatg += " ((tspl_location_master.Main_Location_Code) = '" + clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) + "') "
                    IsApplicable = True
                    Dim arr As Dictionary(Of String, Object) = gvLocation.Rows(ii).Tag
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        strWhrCatg += " and tspl_location_master.Location_Code in ("
                        Dim isFirstTime As Boolean = True
                        For Each strInn As String In arr.Keys
                            If Not isFirstTime Then
                                strWhrCatg += ","
                            End If
                            strWhrCatg += "'" + strInn + "'"
                            isFirstTime = False
                        Next
                        strWhrCatg += ")"
                    End If
                End If
            Next
            If Not IsApplicable Then
                Throw New Exception("Please select at least one location")
            End If
            wherLocation += " and (" + strWhrCatg + ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            strVendor = clsCommon.GetMulcallString(txtVendor.arrValueMember)
            strVendor = "and TSPL_LOCATION_MASTER.Jobwork_Vendor in (" & strVendor & ")"
        End If

        Dim qry As String = ""
        qry = "  select * from ( " & _
              " select Trans_Date,Final.Location_Code,TSPL_LOCATION_MASTER.Jobwork_Vendor as Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name," & _
              " round((CL_Fat_KG-(Issue_Fat_KG-Received_Fat_KG+Return_FatKG+Short_FatKG-Excess_fatKG)),3) as  Opening_Fat_KG," & _
              " round((CL_SNF_KG-(Issue_SNFKG-Received_SNFKG+Return_SNFKG+Short_SNFKG-Excess_SNFKG)),3) as Opening_SNF_KG, " & _
              " round(Issue_Fat_KG,3) as Issue_Fat_KG,round(Issue_SNFKG,3) as Issue_SNFKG,round(Received_Fat_KG,3) as Received_Fat_KG,round(Received_SNFKG,3) as Received_SNFKG,round(Return_FatKG,3) as Return_FatKG,round(Return_SNFKG,3) as Return_SNFKG,round(Short_FatKG,3) as Short_FatKG,round(Short_SNFKG,3) as Short_SNFKG, " & _
              " round(Excess_fatKG,3) as Excess_FatKG,round(Excess_SNFKG,3) as Excess_SNFKG ,round(CL_Fat_KG,3) as CL_Fat_KG,round(CL_SNF_KG,3) as CL_SNF_KG from ( " & _
              " select Trans_Date,Location_Code,sum(Opening_Fat_KG) as Opening_Fat_KG,sum(Opening_SNF_KG) as Opening_SNF_KG,sum(Issue_Fat_KG) as Issue_Fat_KG,sum(Issue_SNFKG) as Issue_SNFKG,sum(Received_Fat_KG) as Received_Fat_KG,sum(Received_SNFKG) as Received_SNFKG,sum(Return_FatKG) as Return_FatKG,sum(Return_SNFKG) as Return_SNFKG ,sum(Short_FatKG) as Short_FatKG,sum(Short_SNFKG) as Short_SNFKG, " & _
              " sum(Excess_fatKG) as Excess_fatKG,sum(Excess_SNFKG) as Excess_SNFKG,round(sum(sum(Opening_Fat_KG)+sum(Issue_Fat_KG)-sum(Received_Fat_KG)+sum(Return_FatKG)+sum(Short_FatKG)-sum(Excess_fatKG) ) over(partition by Location_Code order by Trans_Date),3) as CL_Fat_KG, " & _
              " round(sum(sum(Opening_SNF_KG)+sum(Issue_SNFKG)-sum(Received_SNFKG)+sum(Return_SNFKG)+sum(Short_SNFKG)-sum(Excess_SNFKG) ) over(partition by Location_Code order by Trans_Date),3) as CL_SNF_KG " & _
              " from ( " & _
              " select '" & From_Date & "' as Trans_Date,Location_Code,sum(CL_FAT_KG) as Opening_Fat_KG,sum(CL_SNF_KG) as Opening_SNF_KG,sum(Issue_FatKG) as Issue_Fat_KG, " & _
              " sum(Issue_SNFKG) as Issue_SNFKG,sum(Received_FatKG) as Received_Fat_KG, " & _
              " sum(Received_SNFKG) as Received_SNFKG,sum(Short_FatKG) as Short_FatKG,sum(Short_SNFKG) as Short_SNFKG,sum(Excess_fatKG) as Excess_fatKG,sum(Excess_SNFKG) as Excess_SNFKG,0 as  Return_FatKG ,0 as  Return_SNFKG from ( " & _
              " select TSPL_INV_MOVE_DL.TRANS_DATE,TSPL_INV_MOVE_DL.Location_Code,TSPL_INV_MOVE_DL.CL_FAT_KG,TSPL_INV_MOVE_DL.CL_SNF_KG,0 as Issue_FatKG,0 as Issue_SNFKG,0 as Received_FatKG,0 as Received_SNFKG,0 as Short_FatKG,0 as Short_SNFKG, " & _
              " 0 as Excess_fatKG,0 as Excess_SNFKG from TSPL_INV_MOVE_DL " & _
              " inner join (select Location_Code,max(TRANS_DATE) as Opening_Date  from TSPL_INV_MOVE_DL where TRANS_DATE<'" & From_Date & "' " & strLoacation1 & "" & _
              " group by Location_Code) as OPDate on TSPL_INV_MOVE_DL.Location_Code=OPDate.Location_Code and TSPL_INV_MOVE_DL.TRANS_DATE=OPDate.Opening_Date " & _
              " where 2=2  " & strLoacation1 & " and TSPL_INV_MOVE_DL.TRANS_DATE<'" & From_Date & "' " & _
              " union all " & _
              " -- git table " & Environment.NewLine & _
              " select cast(Punching_Date as Date) as Trans_Date,Location_Code,sum((case when View_STOCK_DATA_GIT.Trans_Type='MilkTransferJobWork' and InOut='I' then Fat_KG else 0 end)-(case when View_STOCK_DATA_GIT.Trans_Type IN('MilkTransJWOReturn','JWO-SRN') then Fat_KG else 0 end)+(case when View_STOCK_DATA_GIT.Trans_Type='JWO-SRN-RET' then Fat_KG else 0 end)+(case when len(TSPL_ADJUSTMENT_HEADER.Reference_Document )<=0 AND View_STOCK_DATA_GIT.Trans_Type NOT IN ('MilkTransferJobWork','JWO-SRN-JLO','MilkTransJWOReturn') then (case when View_STOCK_DATA_GIT.InOut='I' then Fat_KG else 0 end) else 0 end)-(case when len(TSPL_ADJUSTMENT_HEADER.Reference_Document )<=0 AND View_STOCK_DATA_GIT.Trans_Type NOT IN ('MilkTransferJobWork','JWO-SRN-JLO','MilkTransJWOReturn') then (case when View_STOCK_DATA_GIT.InOut='O' then FAT_KG else 0 end) else 0 end)) as Opening_FatKg,sum((case when View_STOCK_DATA_GIT.Trans_Type='MilkTransferJobWork' and InOut='I' then SNF_KG else 0 end )-(case when View_STOCK_DATA_GIT.Trans_Type IN('MilkTransJWOReturn','JWO-SRN') then SNF_KG else 0 end)+(case when View_STOCK_DATA_GIT.Trans_Type='JWO-SRN-RET' then SNF_KG else 0 end)+(case when len(TSPL_ADJUSTMENT_HEADER.Reference_Document )<=0 AND View_STOCK_DATA_GIT.Trans_Type NOT IN ('MilkTransferJobWork','JWO-SRN-JLO','MilkTransJWOReturn') then (case when View_STOCK_DATA_GIT.InOut='I' then SNF_KG else 0 end) else 0 end)-(case when len(TSPL_ADJUSTMENT_HEADER.Reference_Document )<=0 AND View_STOCK_DATA_GIT.Trans_Type NOT IN ('MilkTransferJobWork','JWO-SRN-JLO','MilkTransJWOReturn') then (case when View_STOCK_DATA_GIT.InOut='O' then SNF_KG else 0 end) else 0 end)) as SNF_Kg, " & _
              " 0 as Issue_FatKG, " & _
              " 0 as Issue_SNFKG, " & _
              " 0 as  Received_FatKG, " & _
              " 0 as  Received_SNFKG, " & _
              " 0 as Short_FatKG, " & _
              " 0 as Short_SNFKG, " & _
              " 0 as Excess_fatKG, " & _
              " 0 as Excess_SNFKG " & _
              " from View_STOCK_DATA_GIT " & _
              " left join TSPL_ADJUSTMENT_HEADER on View_STOCK_DATA_GIT.Source_Doc_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No " & _
              " where 2=2 " & strLoacation2 & " and Punching_Date<'" & From_Date & "' " & _
              " group by cast(Punching_Date as Date),Location_Code ) as Opening_Stock group by TRANS_DATE,Location_Code " & _
              " union all " & _
              " select cast(Punching_Date as Date) as Trans_Date,Location_Code,0 as Opening_FatKg,0 as Opening_SNF_KG," & _
              " sum(case when TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='MilkTransferJobWork' and InOut='I' then Fat_KG else 0 end ) as Issue_FatKG," & _
              " sum(case when TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='MilkTransferJobWork' and InOut='I' then SNF_KG else 0 end ) as Issue_SNFKG, " & _
              " sum(case when TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type in('MilkTransJWOReturn','JWO-SRN') then Fat_KG else 0 end) as  Received_FatKG," & _
              " sum(case when TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type in('MilkTransJWOReturn','JWO-SRN') then SNF_KG else 0 end) as  Received_SNFKG, " & _
              " sum(case when len(coalesce(TSPL_ADJUSTMENT_HEADER.Reference_Document,''))<=0 AND TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type NOT IN ('MilkTransferJobWork','JWO-SRN-JLO','MilkTransJWOReturn') then (case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' then Fat_KG else 0 end) else 0 end) as Short_FatKG," & _
              " sum(case when len(coalesce(TSPL_ADJUSTMENT_HEADER.Reference_Document,''))<=0 AND TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type NOT IN ('MilkTransferJobWork','JWO-SRN-JLO','MilkTransJWOReturn') then (case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' then SNF_KG else 0 end) else 0 end) as Short_SNFKG," & _
              " sum(case when len(coalesce(TSPL_ADJUSTMENT_HEADER.Reference_Document,''))<=0 AND TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type NOT IN ('MilkTransferJobWork','JWO-SRN-JLO','MilkTransJWOReturn') then (case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='O' then Fat_KG else 0 end) else 0 end) as Excess_fatKG," & _
              " sum(case when len(coalesce(TSPL_ADJUSTMENT_HEADER.Reference_Document,''))<=0 AND TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type NOT IN ('MilkTransferJobWork','JWO-SRN-JLO','MilkTransJWOReturn') then (case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='O' then SNF_KG else 0 end) else 0 end) as Excess_SNFKG " & _
              " ,sum(case when TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='JWO-SRN-RET' then Fat_KG else 0 end) as  Return_FatKG " & _
              " ,sum(case when TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='JWO-SRN-RET' then SNF_KG else 0 end) as  Return_SNFKG" & _
              " from TSPL_INVENTORY_MOVEMENT_NEW " & _
              " left join TSPL_ADJUSTMENT_HEADER on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No " & _
              " where 2=2  " & strLoacation2 & " and cast(Punching_Date as date)>='" & From_Date & "' and cast(Punching_Date as date)<='" & To_Date & "' " & _
              " group by cast(Punching_Date as Date),Location_Code" & _
              " union all " & _
              " select cast(TSPL_JWO_SRN_HEAD.Document_Date as Date) as Trans_Date,TSPL_JWO_SRN_HEAD.Job_Loc_Code AS Location_Code,0 as Opening_FatKg,0 as Opening_SNF_KG," & _
              " 0 as Issue_FatKG,0 as Issue_SNFKG,sum(TSPL_JWO_SRN_DETAIL.FAT_KG) as  Received_FatKG,sum(TSPL_JWO_SRN_DETAIL.SNF_KG) as  Received_SNFKG,0 as Short_FatKG,0 as Short_SNFKG,0 as Excess_fatKG,0 as Excess_SNFKG,0 as  Return_FatKG ,0 as  Return_SNFKG  " & _
              " from TSPL_JWO_SRN_DETAIL INNER join TSPL_JWO_SRN_HEAD on TSPL_JWO_SRN_DETAIL.Document_No=TSPL_JWO_SRN_HEAD.Document_No " & _
              " where 2=2  " & strLoacation2 & "  and cast(TSPL_JWO_SRN_HEAD.Document_Date as date)>='" & From_Date & "' and cast(TSPL_JWO_SRN_HEAD.Document_Date as date)<='" & To_Date & "' " & _
              " group by cast(TSPL_JWO_SRN_HEAD.Document_Date as Date),TSPL_JWO_SRN_HEAD.Job_Loc_Code " & _
              "  union all  " + Environment.NewLine + _
              " select cast(TSPL_JWO_SRN_RETURN.Document_Date as Date) as Trans_Date,TSPL_JWO_SRN_HEAD.Job_Loc_Code AS Location_Code,0 as Opening_FatKg,0 as Opening_SNF_KG, 0 as Issue_FatKG,0 as Issue_SNFKG,0 as  Received_FatKG,0 as  Received_SNFKG,0 as Short_FatKG,0 as Short_SNFKG,0 as Excess_fatKG,0 as Excess_SNFKG" + _
              " ,sum(TSPL_JWO_SRN_DETAIL.FAT_KG) as  Return_FatKG " + _
              " ,sum(TSPL_JWO_SRN_DETAIL.SNF_KG) as  Return_SNFKG " + _
              " from TSPL_JWO_SRN_DETAIL " + _
              " INNER join TSPL_JWO_SRN_HEAD on TSPL_JWO_SRN_DETAIL.Document_No=TSPL_JWO_SRN_HEAD.Document_No  " +
              " inner join TSPL_JWO_SRN_RETURN on TSPL_JWO_SRN_RETURN.JWO_SRN_No=TSPL_JWO_SRN_HEAD.Document_No" + _
              " where 2=2 " & strLoacation2 & " and cast(TSPL_JWO_SRN_RETURN.Document_Date as date)>='" & From_Date & "' and cast(TSPL_JWO_SRN_RETURN.Document_Date as date)<='" & To_Date & "'  " + _
              " group by cast(TSPL_JWO_SRN_RETURN.Document_Date as Date),TSPL_JWO_SRN_HEAD.Job_Loc_Code  " + Environment.NewLine + _
              " ) as Prev_Final group by Trans_Date,Location_Code ) as Final  " & _
              " left join TSPL_LOCATION_MASTER on Final.Location_Code=TSPL_LOCATION_MASTER.Location_Code " & _
              " LEFT JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_LOCATION_MASTER.Jobwork_Vendor where len(coalesce(TSPL_LOCATION_MASTER.Jobwork_Vendor,''))>0 " & strVendor & "" & _
              " ) xyz  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = xyz.Location_Code  where 2 =2  " + wherLocation + " order by xyz.Location_Code,xyz.Trans_Date "

        Return qry
    End Function
    Function GetDetailQry(ByVal Report_Type As String, ByVal Location_Code As String, ByVal Trans_Date As Date) As String
        Dim ReportTypeCol As String = ""
        Dim Punching_DateCol As String = ""
        ReportTypeCol = "(case when TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='MilkTransferJobWork' and InOut='I' then 'Issue_FatKG' when TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type in ('MilkTransJWOReturn','JWO-SRN') then 'Received_FatKG' when len(coalesce(TSPL_ADJUSTMENT_HEADER.Reference_Document,''))<=0 AND TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type NOT IN ('MilkTransferJobWork','JWO-SRN-JLO') then (case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' then 'Short_FatKG' else 'Excess_fatKG' end) else TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type end) "
        Punching_DateCol = "Punching_Date "

        Dim Qry As String = ""
        Qry = " select Item.Product_Type,TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type," & ReportTypeCol & " as Report_Type, " & Environment.NewLine &
              " TSPL_INVENTORY_MOVEMENT_NEW.InOut,TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No,TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,Item.Item_Desc, " & Environment.NewLine &
              " TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM,TSPL_INVENTORY_MOVEMENT_NEW.Net_Cost,TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost, " & Environment.NewLine &
              " TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per, " & Environment.NewLine &
              " TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per, " & Environment.NewLine &
              " TSPL_INVENTORY_MOVEMENT_NEW.FAT_Kg," & Environment.NewLine &
              " TSPL_INVENTORY_MOVEMENT_NEW.SNF_Kg,TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date " & Environment.NewLine &
              " from TSPL_INVENTORY_MOVEMENT_NEW  " & Environment.NewLine &
              " left join TSPL_ADJUSTMENT_HEADER on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No " & _
              " left join TSPL_ITEM_MASTER Item on TSPL_INVENTORY_MOVEMENT_NEW.Item_Code=Item.Item_Code " & Environment.NewLine &
              " left join TSPL_LOCATION_MASTER Loc on TSPL_INVENTORY_MOVEMENT_NEW.Location_Code=Loc.Location_Code where 2=2 and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" & Location_Code & "' and cast(Punching_Date as date)='" & clsCommon.GetPrintDate(Trans_Date, "dd-MMM-yyyy") & " '  "
        Qry = Qry & " Union all  select Item.Product_Type,'JWO-SRN' as Trans_Type,'Received from Jobworker' as Report_Type, " & _
                    " 'I' as InOut,TSPL_JWO_SRN_HEAD.Job_Loc_Code as Location_Code,TSPL_JWO_SRN_DETAIL.Document_No as Source_Doc_No,TSPL_JWO_SRN_DETAIL.Item_Code,Item.Item_Desc, " & Environment.NewLine &
                    " TSPL_JWO_SRN_DETAIL.Qty,TSPL_JWO_SRN_DETAIL.UOM as Stock_UOM,TSPL_JWO_SRN_DETAIL.Job_Amount as Net_Cost,TSPL_JWO_SRN_DETAIL.Amount as Avg_Cost, " & Environment.NewLine &
                    " TSPL_JWO_SRN_DETAIL.Fat_Per,TSPL_JWO_SRN_DETAIL.SNF_Per,TSPL_JWO_SRN_DETAIL.FAT_Kg,TSPL_JWO_SRN_DETAIL.SNF_Kg,TSPL_JWO_SRN_HEAD.Document_Date " & Environment.NewLine &
                    " from TSPL_JWO_SRN_DETAIL " & Environment.NewLine &
                    " inner join TSPL_JWO_SRN_HEAD on TSPL_JWO_SRN_DETAIL.Document_No=TSPL_JWO_SRN_HEAD.Document_No " & Environment.NewLine &
                    " left join TSPL_ITEM_MASTER Item on TSPL_JWO_SRN_DETAIL.Item_Code=Item.Item_Code " & Environment.NewLine &
                    " left join TSPL_LOCATION_MASTER Loc on TSPL_JWO_SRN_HEAD.Job_Loc_Code=Loc.Location_Code where 2=2 and TSPL_JWO_SRN_HEAD.Job_Loc_Code='" & Location_Code & "' " & Environment.NewLine &
                    " and cast(TSPL_JWO_SRN_HEAD.Document_Date as date)='" & clsCommon.GetPrintDate(Trans_Date, "dd-MMM-yyyy") & "'"
        Qry = Qry & " Union all  select Item.Product_Type,'JWO-SRN-RET' as Trans_Type,'Return from Jobworker' as Report_Type, " & _
                   " 'O' as InOut,TSPL_JWO_SRN_HEAD.Job_Loc_Code as Location_Code,TSPL_JWO_SRN_RETURN.Document_No as Source_Doc_No,TSPL_JWO_SRN_DETAIL.Item_Code,Item.Item_Desc, " & Environment.NewLine &
                   " TSPL_JWO_SRN_DETAIL.Qty,TSPL_JWO_SRN_DETAIL.UOM as Stock_UOM,TSPL_JWO_SRN_DETAIL.Job_Amount as Net_Cost,TSPL_JWO_SRN_DETAIL.Amount as Avg_Cost, " & Environment.NewLine &
                   " TSPL_JWO_SRN_DETAIL.Fat_Per,TSPL_JWO_SRN_DETAIL.SNF_Per,TSPL_JWO_SRN_DETAIL.FAT_Kg,TSPL_JWO_SRN_DETAIL.SNF_Kg,TSPL_JWO_SRN_RETURN.Document_Date " & Environment.NewLine &
                   " from TSPL_JWO_SRN_DETAIL " & Environment.NewLine &
                   " inner join TSPL_JWO_SRN_HEAD on TSPL_JWO_SRN_DETAIL.Document_No=TSPL_JWO_SRN_HEAD.Document_No " & Environment.NewLine &
                   " inner join TSPL_JWO_SRN_RETURN on TSPL_JWO_SRN_RETURN.JWO_SRN_No=TSPL_JWO_SRN_HEAD.Document_No " + Environment.NewLine + _
                   " left join TSPL_ITEM_MASTER Item on TSPL_JWO_SRN_DETAIL.Item_Code=Item.Item_Code " & Environment.NewLine &
                   " left join TSPL_LOCATION_MASTER Loc on TSPL_JWO_SRN_HEAD.Job_Loc_Code=Loc.Location_Code where 2=2 and TSPL_JWO_SRN_HEAD.Job_Loc_Code='" & Location_Code & "' " & Environment.NewLine &
                   " and cast(TSPL_JWO_SRN_RETURN.Document_Date as date)='" & clsCommon.GetPrintDate(Trans_Date, "dd-MMM-yyyy") & "'"
        Qry = " select * from (" & Qry & ") as Outermost where Report_Type like '%" & Report_Type & "%'"
        Return Qry
        '  " from ( " & Environment.NewLine &
        '  " select  'MI' as Product_Type,Trans_Type,InOut,Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,Net_Cost,Avg_Cost, " & Environment.NewLine &
        '  " Fat_Per,SNF_Per,FAT_Kg,SNF_Kg,cast(Punching_Date as date) as Punching_Date " & Environment.NewLine &
    End Function

    Private Sub rbtnLocationAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub


    Private Sub rbtnLocationSelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnLocationSelect.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        CheckedAll(gvLocation)
    End Sub
    Private Sub CheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
        For ii As Integer = 0 To gv.ChildRows.Count - 1
            gv.ChildRows(ii).Cells("SEL").Value = True
        Next
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        UnCheckedAll(gvLocation)
    End Sub
    Private Sub UnCheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
    End Sub

    Private Sub gvLocation_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvLocation.CellDoubleClick
        If clsCommon.myCBool(gvLocation.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 4
            frm.strCode = clsCommon.myCstr(gvLocation.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvLocation.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvLocation.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub
    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try
            Dim qry As String = ""
            If clsCommon.myCdbl(e.Value) = 0 Then
                Exit Sub
            End If

            Dim Loc_Code As String = Gv1.Rows(e.RowIndex).Cells("Location_Code").Value
            'arrLoc = New ArrayList
            'arrLoc.Add(Loc_Code)
            Dim Trans_Date As Date = Gv1.Rows(e.RowIndex).Cells("Trans_Date").Value
            Dim Report_Type As String = Gv1.Columns(e.ColumnIndex).Name.ToString.Replace("_", "").Replace("FatKG", "").Replace("fatKG", "").Replace("FATKG", "").Replace("SNFKG", "")
            If Report_Type.Contains("Opening") = True OrElse Report_Type.Contains("Closing") = True OrElse Report_Type.Contains("CL") = True Then
                Exit Sub
            End If
            clsCommon.ProgressBarShow()
            qry = GetDetailQry(Report_Type, Loc_Code, Trans_Date)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv2.DataSource = Nothing
            gv2.DataSource = dt
            gv2.ReadOnly = True
            gv2.SummaryRowsBottom.Clear()
            Dim summaryRowItem As New GridViewSummaryRowItem()
            For Each col As GridViewColumn In gv2.Columns
                If (col.Name.Contains("FAT") = True OrElse col.Name.Contains("Fat") = True OrElse col.Name.Contains("SNF") = True OrElse col.Name.Contains("Cost") = True OrElse col.Name.Contains("Qty") = True OrElse col.Name.Contains("Amount") = True) Then
                    If col.Name.Contains("_Per") = True Then
                        Continue For
                    End If
                    Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item)
                End If
            Next

            gv2.SummaryRowsBottom.Add(summaryRowItem)
            gv2.BestFitColumns()
            gv2.EnableFiltering = True
            RadPageView1.SelectedPage = RadPageViewPage3
            clsCommon.ProgressBarHide()

        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
End Class

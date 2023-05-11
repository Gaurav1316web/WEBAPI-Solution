Imports common
Imports XpertERPEngine
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

'''' Create BY abhishek as on 3 Nov 2012 ---
'''' Updated BY abhishek as on 3 Nov 2012 10:00pm

'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 31/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------
''''By vipin for pdf on 08/02/2013 

Public Class FrmPendingLoadIn_Transfer_Type
    Inherits FrmMainTranScreen
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"

    Private Sub FrmPendingLoadIn_Transfer_Type_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadLocation()
        SetDataBaseGrid()
        Reset()
    End Sub
    Sub SetDataBaseGrid()
        gvDB.Rows.Clear()
        gvDB.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDB.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCompCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompCode.FormatString = ""
        repoCompCode.HeaderText = "Company Code"
        repoCompCode.Name = colCompCode
        repoCompCode.Width = 150
        repoCompCode.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompCode)

        Dim repoCompName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompName.FormatString = ""
        repoCompName.HeaderText = "Company Name"
        repoCompName.Name = colCompName
        repoCompName.Width = 150
        repoCompName.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompName)

        Dim repoDB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDB.FormatString = ""
        repoDB.HeaderText = "Database Name"
        repoDB.Name = colDataBaseName
        repoDB.IsVisible = False
        repoDB.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvDB.Rows.AddNew()
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colSelect).Value = False
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompCode).Value = clsCommon.myCstr(dr("Comp_Code"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompName).Value = clsCommon.myCstr(dr("Comp_Name"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colDataBaseName).Value = clsCommon.myCstr(dr("DataBase_Name"))
            Next
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    End Sub
    Private Function GetSelectedDatabase() As List(Of String)
        Dim arrDBName As New List(Of String)
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If ((clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) OrElse rbtnAllCompany.IsChecked) Then
                arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next
        Return arrDBName
    End Function


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmpendingLoadin)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadLocation()
        Dim qry As String = " select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location_Code"
        cbgLocation.DisplayMember = "Location_Desc"
    End Sub

    Sub Reset()
        dtpFrmDate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        chkLocAll.IsChecked = True
        'rbtnAllCompany.IsChecked = True

        rbtnSelectCompany.IsChecked = True
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(gvDB.Rows(ii).Cells(colCompCode).Value), objCommonVar.CurrentCompanyCode) = CompairStringResult.Equal Then
                gvDB.Rows(ii).Cells(colSelect).Value = 1
            End If
        Next
        btnPrint.Enabled = False
        'RadPageView1.SelectedPage = RadPageViewPage1

    End Sub

    Private Sub rbtnAllCompany_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnAllCompany.ToggleStateChanged
        gvDB.Enabled = Not rbtnAllCompany.IsChecked
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Gv1.DataSource = Nothing
        Gv1.Columns.Clear()
        Gv1.Rows.Clear()
        Reset()
    End Sub
    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'Printdata()
    End Sub

    Public Sub Printdata(ByVal exporter As EnumExportTo)
        Dim location As String
        Dim strlocation As String = ""
        If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
            location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
            strlocation = location.Replace("'", "")
        End If

        Dim str As String = "Pending Load In"
        Dim head1 = "Pending Load In "

        Dim arr As New List(Of String)()
        arr.Add(head1)
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("  From:  " + dtpFrmDate.Value + "  To: " + dtpToDate.Value + "")
        arr.Add("Location:  " + strlocation + "")

        ' clsCommon.MyExportToExcel(str, Gv1, arr, "Pending Load In")

        If exporter = EnumExportTo.Excel Then
            clsCommon.MyExportToExcel(str, Gv1, arr, "Pending Load In")


        Else
            clsCommon.MyExportToPDF(str, Gv1, arr, "Pending Load In", True)
        End If


    End Sub

    Public Sub gridformat()

        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.ShowGroupPanel = False

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Gv1.AllowAddNewRow = False


        Gv1.Columns("Transfer_No").IsVisible = True
        Gv1.Columns("Transfer_No").Width = 125
        Gv1.Columns("Transfer_No").HeaderText = "Transfer Number"

        Gv1.Columns("Load_Out_No").IsVisible = False
        Gv1.Columns("Load_Out_No").Width = 125
        Gv1.Columns("Load_Out_No").HeaderText = "Load_Out_No"

        Gv1.Columns("Transfer_Date").IsVisible = True
        Gv1.Columns("Transfer_Date").Width = 150
        Gv1.Columns("Transfer_Date").HeaderText = "Transfer Date"

        Gv1.Columns("Vehicle_No").IsVisible = True
        Gv1.Columns("Vehicle_No").Width = 130
        Gv1.Columns("Vehicle_No").HeaderText = "Vehicle_No"

        Gv1.Columns("Posting_Date").IsVisible = False
        Gv1.Columns("Posting_Date").Width = 120
        Gv1.Columns("Posting_Date").HeaderText = "Posting_Date"

        Gv1.Columns("Transfer_Type").IsVisible = False
        Gv1.Columns("Transfer_Type").Width = 120
        Gv1.Columns("Transfer_Type").HeaderText = "Transfer_Type"

        Gv1.Columns("From_Location").IsVisible = False
        Gv1.Columns("From_Location").Width = 100
        Gv1.Columns("From_Location").HeaderText = "From_Location"


        Gv1.Columns("To_Location").IsVisible = False
        Gv1.Columns("To_Location").Width = 100
        Gv1.Columns("To_Location").HeaderText = "To_Location"

        Gv1.Columns("Price_Date").IsVisible = False
        Gv1.Columns("Price_Date").Width = 100
        Gv1.Columns("Price_Date").HeaderText = "Price_Date"

        Gv1.Columns("Tax_Group").IsVisible = False
        Gv1.Columns("Tax_Group").Width = 100
        Gv1.Columns("Tax_Group").HeaderText = "Tax_Group"

        Gv1.Columns("Reference").IsVisible = False
        Gv1.Columns("Reference").Width = 100
        Gv1.Columns("Reference").HeaderText = "Reference"

        Gv1.Columns("Description").IsVisible = False
        Gv1.Columns("Description").Width = 100
        Gv1.Columns("Description").HeaderText = "Description"

        Gv1.Columns("Route_No").IsVisible = False
        Gv1.Columns("Route_No").Width = 100
        Gv1.Columns("Route_No").HeaderText = "Route_No"


        Gv1.Columns("Salesmancode").IsVisible = False
        Gv1.Columns("Salesmancode").Width = 100
        Gv1.Columns("Salesmancode").HeaderText = "Salesmancode"

        Gv1.Columns("Price_Code").IsVisible = False
        Gv1.Columns("Price_Code").Width = 100
        Gv1.Columns("Price_Code").HeaderText = "Price_Code"

        Gv1.Columns("Mode_Of_Transport").IsVisible = False
        Gv1.Columns("Mode_Of_Transport").Width = 100
        Gv1.Columns("Mode_Of_Transport").HeaderText = "Mode_Of_Transport"

        Gv1.Columns("Km_Reading").IsVisible = False
        Gv1.Columns("Km_Reading").Width = 100
        Gv1.Columns("Km_Reading").HeaderText = "Km_Reading"

        Gv1.Columns("Tax1").IsVisible = False
        Gv1.Columns("Tax1").Width = 100
        Gv1.Columns("Tax1").HeaderText = "Tax1"

        Gv1.Columns("Tax1_Amt").IsVisible = False
        Gv1.Columns("Tax1_Amt").Width = 100
        Gv1.Columns("Tax1_Amt").HeaderText = "Tax1_Amt"


        Gv1.Columns("Tax1_Rate").IsVisible = False
        Gv1.Columns("Tax1_Rate").Width = 100
        Gv1.Columns("Tax1_Rate").HeaderText = "Tax1_Rate"

        Gv1.Columns("Tax1_Assessable_Amt").IsVisible = False
        Gv1.Columns("Tax1_Assessable_Amt").Width = 100
        Gv1.Columns("Tax1_Assessable_Amt").HeaderText = "Tax1_Assessable_Amt"

        Gv1.Columns("Tax2").IsVisible = False
        Gv1.Columns("Tax2").Width = 100
        Gv1.Columns("Tax2").HeaderText = "Tax2"

        Gv1.Columns("Tax2_Amt").IsVisible = False
        Gv1.Columns("Tax2_Amt").Width = 100
        Gv1.Columns("Tax2_Amt").HeaderText = "Tax2_Amt"


        Gv1.Columns("Tax2_Rate").IsVisible = False
        Gv1.Columns("Tax2_Rate").Width = 100
        Gv1.Columns("Tax2_Rate").HeaderText = "Tax2_Rate"

        Gv1.Columns("Tax2_Assessable_Amt").IsVisible = False
        Gv1.Columns("Tax2_Assessable_Amt").Width = 100
        Gv1.Columns("Tax2_Assessable_Amt").HeaderText = "Tax2_Assessable_Amt"

        Gv1.Columns("Tax3").IsVisible = False
        Gv1.Columns("Tax3").Width = 100
        Gv1.Columns("Tax3").HeaderText = "Tax3"

        Gv1.Columns("Tax3_Amt").IsVisible = False
        Gv1.Columns("Tax3_Amt").Width = 100
        Gv1.Columns("Tax3_Amt").HeaderText = "Tax3_Amt"


        Gv1.Columns("Tax3_Rate").IsVisible = False
        Gv1.Columns("Tax3_Rate").Width = 100
        Gv1.Columns("Tax3_Rate").HeaderText = "Tax3_Rate"

        Gv1.Columns("Tax3_Assessable_Amt").IsVisible = False
        Gv1.Columns("Tax3_Assessable_Amt").Width = 100
        Gv1.Columns("Tax3_Assessable_Amt").HeaderText = "Tax3_Assessable_Amt"



        Gv1.Columns("Tax4").IsVisible = False
        Gv1.Columns("Tax4").Width = 100
        Gv1.Columns("Tax4").HeaderText = "Tax1"

        Gv1.Columns("Tax4_Amt").IsVisible = False
        Gv1.Columns("Tax4_Amt").Width = 100
        Gv1.Columns("Tax4_Amt").HeaderText = "Tax4_Amt"


        Gv1.Columns("Tax4_Rate").IsVisible = False
        Gv1.Columns("Tax4_Rate").Width = 100
        Gv1.Columns("Tax4_Rate").HeaderText = "Tax4_Rate"

        Gv1.Columns("Tax4_Assessable_Amt").IsVisible = False
        Gv1.Columns("Tax4_Assessable_Amt").Width = 100
        Gv1.Columns("Tax4_Assessable_Amt").HeaderText = "Tax4_Assessable_Amt"

        Gv1.Columns("Tax5").IsVisible = False
        Gv1.Columns("Tax5").Width = 100
        Gv1.Columns("Tax5").HeaderText = "Tax5"

        Gv1.Columns("Tax5_Amt").IsVisible = False
        Gv1.Columns("Tax5_Amt").Width = 100
        Gv1.Columns("Tax5_Amt").HeaderText = "Tax5_Amt"


        Gv1.Columns("Tax5_Rate").IsVisible = False
        Gv1.Columns("Tax5_Rate").Width = 100
        Gv1.Columns("Tax5_Rate").HeaderText = "Tax5_Rate"


        Gv1.Columns("Tax5_Assessable_Amt").IsVisible = False
        Gv1.Columns("Tax5_Assessable_Amt").Width = 100
        Gv1.Columns("Tax5_Assessable_Amt").HeaderText = "Tax5_Assessable_Amt"

        Gv1.Columns("Tax6").IsVisible = False
        Gv1.Columns("Tax6").Width = 100
        Gv1.Columns("Tax6").HeaderText = "Tax6"

        Gv1.Columns("Tax6_Amt").IsVisible = False
        Gv1.Columns("Tax6_Amt").Width = 100
        Gv1.Columns("Tax6_Amt").HeaderText = "Tax6_Amt"


        Gv1.Columns("Tax6_Rate").IsVisible = False
        Gv1.Columns("Tax6_Rate").Width = 100
        Gv1.Columns("Tax6_Rate").HeaderText = "Tax6_Rate"

        Gv1.Columns("Tax6_Assessable_Amt").IsVisible = False
        Gv1.Columns("Tax6_Assessable_Amt").Width = 100
        Gv1.Columns("Tax6_Assessable_Amt").HeaderText = "Tax6_Assessable_Amt"

        Gv1.Columns("Tax7").IsVisible = False
        Gv1.Columns("Tax7").Width = 100
        Gv1.Columns("Tax7").HeaderText = "Tax7"

        Gv1.Columns("Tax7_Amt").IsVisible = False
        Gv1.Columns("Tax7_Amt").Width = 100
        Gv1.Columns("Tax7_Amt").HeaderText = "Tax7_Amt"


        Gv1.Columns("Tax7_Rate").IsVisible = False
        Gv1.Columns("Tax7_Rate").Width = 100
        Gv1.Columns("Tax7_Rate").HeaderText = "Tax7_Rate"

        Gv1.Columns("Tax7_Assessable_Amt").IsVisible = False
        Gv1.Columns("Tax7_Assessable_Amt").Width = 100
        Gv1.Columns("Tax7_Assessable_Amt").HeaderText = "Tax7_Assessable_Amt"

        Gv1.Columns("Tax8").IsVisible = False
        Gv1.Columns("Tax8").Width = 100
        Gv1.Columns("Tax8").HeaderText = "Tax8"

        Gv1.Columns("Tax8_Amt").IsVisible = False
        Gv1.Columns("Tax8_Amt").Width = 100
        Gv1.Columns("Tax8_Amt").HeaderText = "Tax8_Amt"


        Gv1.Columns("Tax8_Rate").IsVisible = False
        Gv1.Columns("Tax8_Rate").Width = 100
        Gv1.Columns("Tax8_Rate").HeaderText = "Tax8_Rate"

        Gv1.Columns("Tax8_Assessable_Amt").IsVisible = False
        Gv1.Columns("Tax8_Assessable_Amt").Width = 100
        Gv1.Columns("Tax8_Assessable_Amt").HeaderText = "Tax8_Assessable_Amt"

        Gv1.Columns("Tax9").IsVisible = False
        Gv1.Columns("Tax9").Width = 100
        Gv1.Columns("Tax9").HeaderText = "Tax1"

        Gv1.Columns("Tax9_Amt").IsVisible = False
        Gv1.Columns("Tax9_Amt").Width = 100
        Gv1.Columns("Tax9_Amt").HeaderText = "Tax9_Amt"


        Gv1.Columns("Tax9_Rate").IsVisible = False
        Gv1.Columns("Tax9_Rate").Width = 100
        Gv1.Columns("Tax9_Rate").HeaderText = "Tax9_Rate"

        Gv1.Columns("Tax9_Assessable_Amt").IsVisible = False
        Gv1.Columns("Tax9_Assessable_Amt").Width = 100
        Gv1.Columns("Tax9_Assessable_Amt").HeaderText = "Tax9_Assessable_Amt"

        Gv1.Columns("Tax10").IsVisible = False
        Gv1.Columns("Tax10").Width = 100
        Gv1.Columns("Tax10").HeaderText = "Tax10"

        Gv1.Columns("Tax10_Amt").IsVisible = False
        Gv1.Columns("Tax10_Amt").Width = 100
        Gv1.Columns("Tax10_Amt").HeaderText = "Tax10_Amt"


        Gv1.Columns("Tax10_Rate").IsVisible = False
        Gv1.Columns("Tax10_Rate").Width = 100
        Gv1.Columns("Tax10_Rate").HeaderText = "Tax10_Rate"

        Gv1.Columns("Tax10_Assessable_Amt").IsVisible = False
        Gv1.Columns("Tax10_Assessable_Amt").Width = 100
        Gv1.Columns("Tax10_Assessable_Amt").HeaderText = "Tax10_Assessable_Amt"


        Gv1.Columns("Item_Amount").IsVisible = False
        Gv1.Columns("Item_Amount").Width = 100
        Gv1.Columns("Item_Amount").HeaderText = "Item_Amount"


        Gv1.Columns("Total_Tax_Amount").IsVisible = False
        Gv1.Columns("Total_Tax_Amount").Width = 100
        Gv1.Columns("Total_Tax_Amount").HeaderText = "Total_Tax_Amount"

        Gv1.Columns("Post").IsVisible = False
        Gv1.Columns("Post").Width = 100
        Gv1.Columns("Post").HeaderText = "Post"


        Gv1.Columns("Created_By").IsVisible = False
        Gv1.Columns("Created_By").Width = 100
        Gv1.Columns("Created_By").HeaderText = "Created_By"


        Gv1.Columns("Created_Date").IsVisible = False
        Gv1.Columns("Created_Date").Width = 100
        Gv1.Columns("Created_Date").HeaderText = "Created_Date"

        Gv1.Columns("Modify_By").IsVisible = False
        Gv1.Columns("Modify_By").Width = 100
        Gv1.Columns("Modify_By").HeaderText = "Modify_By"


        Gv1.Columns("Modify_Date").IsVisible = False
        Gv1.Columns("Modify_Date").Width = 100
        Gv1.Columns("Modify_Date").HeaderText = "Modify_Date"

        Gv1.Columns("Level1_User_Code").IsVisible = False
        Gv1.Columns("Level1_User_Code").Width = 100
        Gv1.Columns("Level1_User_Code").HeaderText = "Level1_User_Code"


        Gv1.Columns("Level2_User_Code").IsVisible = False
        Gv1.Columns("Level2_User_Code").Width = 100
        Gv1.Columns("Level2_User_Code").HeaderText = "Level2_User_Code"

        Gv1.Columns("Level3_User_Code").IsVisible = False
        Gv1.Columns("Level3_User_Code").Width = 100
        Gv1.Columns("Level3_User_Code").HeaderText = "Level3_User_Code"

        Gv1.Columns("Level4_User_Code").IsVisible = False
        Gv1.Columns("Level4_User_Code").Width = 100
        Gv1.Columns("Level4_User_Code").HeaderText = "Level4_User_Code"

        Gv1.Columns("Level5_User_Code").IsVisible = False
        Gv1.Columns("Level5_User_Code").Width = 100
        Gv1.Columns("Level5_User_Code").HeaderText = "Level5_User_Code"

        Gv1.Columns("Comp_Code").IsVisible = False
        Gv1.Columns("Comp_Code").Width = 100
        Gv1.Columns("Comp_Code").HeaderText = "Comp_Code"

        Gv1.Columns("Load_Out_Date").IsVisible = False
        Gv1.Columns("Load_Out_Date").Width = 100
        Gv1.Columns("Load_Out_Date").HeaderText = "Load_Out_Date"

        Gv1.Columns("Is_Shipped").IsVisible = False
        Gv1.Columns("Is_Shipped").Width = 100
        Gv1.Columns("Is_Shipped").HeaderText = "Is_Shipped"

        Gv1.Columns("Trip_No").IsVisible = False
        Gv1.Columns("Trip_No").Width = 100
        Gv1.Columns("Trip_No").HeaderText = "Trip_No"

        Gv1.Columns("Date_Time_Removal").IsVisible = False
        Gv1.Columns("Date_Time_Removal").Width = 100
        Gv1.Columns("Date_Time_Removal").HeaderText = "Date_Time_Removal"


        Gv1.Columns("Is_Complete").IsVisible = False
        Gv1.Columns("Is_Complete").Width = 100
        Gv1.Columns("Is_Complete").HeaderText = "Is_Complete"


        Gv1.Columns("HOS").IsVisible = False
        Gv1.Columns("HOS").Width = 100
        Gv1.Columns("HOS").HeaderText = "HOS"


        Gv1.Columns("TDM").IsVisible = False
        Gv1.Columns("TDM").Width = 100
        Gv1.Columns("TDM").HeaderText = "TDM"

        Gv1.Columns("CE").IsVisible = False
        Gv1.Columns("CE").Width = 100
        Gv1.Columns("CE").HeaderText = "CE"


        Gv1.Columns("ADC").IsVisible = False
        Gv1.Columns("ADC").Width = 100
        Gv1.Columns("ADC").HeaderText = "ADC"

        Gv1.Columns("EntryDateTime").IsVisible = False
        Gv1.Columns("EntryDateTime").Width = 100
        Gv1.Columns("EntryDateTime").HeaderText = "EntryDateTime"

        Gv1.Columns("Route_Desc").IsVisible = False
        Gv1.Columns("Route_Desc").Width = 100
        Gv1.Columns("Route_Desc").HeaderText = "Route_Desc"

        Gv1.Columns("Price_Desc").IsVisible = False
        Gv1.Columns("Price_Desc").Width = 100
        Gv1.Columns("Price_Desc").HeaderText = "Price_Desc"

        Gv1.Columns("Vehicle_Desc").IsVisible = False
        Gv1.Columns("Vehicle_Desc").Width = 100
        Gv1.Columns("Vehicle_Desc").HeaderText = "Vehicle_Desc"


        Gv1.Columns("Printed").IsVisible = False
        Gv1.Columns("Printed").Width = 100
        Gv1.Columns("Printed").HeaderText = "Printed"


        Gv1.Columns("Quick_Settlement").IsVisible = False
        Gv1.Columns("Quick_Settlement").Width = 100
        Gv1.Columns("Quick_Settlement").HeaderText = "Quick_Settlement"

        Gv1.Columns("Sale_Invoice_Completed").IsVisible = False
        Gv1.Columns("Sale_Invoice_Completed").Width = 100
        Gv1.Columns("Sale_Invoice_Completed").HeaderText = "Sale_Invoice_Completed"

        Gv1.Columns("Is_AgainstFormF").IsVisible = False
        Gv1.Columns("Is_AgainstFormF").Width = 100
        Gv1.Columns("Is_AgainstFormF").HeaderText = "Is_AgainstFormF"

        Gv1.Columns("Location_Type").IsVisible = False
        Gv1.Columns("Location_Type").Width = 100
        Gv1.Columns("Location_Type").HeaderText = "Location_Type"

        Gv1.Columns("Route_Type_Id").IsVisible = False
        Gv1.Columns("Route_Type_Id").Width = 100
        Gv1.Columns("Route_Type_Id").HeaderText = "Route_Type_Id"

        Gv1.Columns("Tax_Group_Type").IsVisible = False
        Gv1.Columns("Tax_Group_Type").Width = 100
        Gv1.Columns("Tax_Group_Type").HeaderText = "Tax_Group_Type"

        Gv1.Columns("Total_Item_Amount").IsVisible = False
        Gv1.Columns("Total_Item_Amount").Width = 100
        Gv1.Columns("Total_Item_Amount").HeaderText = "Total_Item_Amount"

        Gv1.Columns("FromLoc_Desc").IsVisible = True
        Gv1.Columns("FromLoc_Desc").Width = 120
        Gv1.Columns("FromLoc_Desc").HeaderText = "From Location Desc"

        Gv1.Columns("ToLoc_Desc").IsVisible = True
        Gv1.Columns("ToLoc_Desc").Width = 130
        Gv1.Columns("ToLoc_Desc").HeaderText = "To Location Desc"

        Gv1.Columns("Vehicle_Code").IsVisible = False
        Gv1.Columns("Vehicle_Code").Width = 100
        Gv1.Columns("Vehicle_Code").HeaderText = "Vehicle Code"

        Gv1.Columns("Item_Type").IsVisible = True
        Gv1.Columns("Item_Type").Width = 100
        Gv1.Columns("Item_Type").HeaderText = "Item Type"


        Gv1.Columns("Total_Transfer_Amount").IsVisible = True
        Gv1.Columns("Total_Transfer_Amount").Width = 130
        Gv1.Columns("Total_Transfer_Amount").HeaderText = "Total Amount"


        Gv1.Columns("Total_Transfer_QtyInCase").IsVisible = True
        Gv1.Columns("Total_Transfer_QtyInCase").Width = 130
        Gv1.Columns("Total_Transfer_QtyInCase").HeaderText = "Total Qty "



        Dim SumTotalAmount As New GridViewSummaryItem("Total_Transfer_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SumTotalAmount)
        Dim sumTotalQty As New GridViewSummaryItem("Total_Transfer_QtyInCase", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(sumTotalQty)

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefreshe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshe.Click
        Try

            Dim arrSelDB As List(Of String) = GetSelectedDatabase()
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select AtLeast Single Location Or Select All")
                Return
            End If
            Dim qry As String = "select * from " + clsCommon.ReplicateDBString + " TSPL_TRANSFER_HEAD where Transfer_Type='LO'  and Location_Type='Physical' and Post ='Y' "
            qry += " and " + clsCommon.ReplicateDBString + " TSPL_TRANSFER_HEAD .Transfer_Date >='" + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "' and " + clsCommon.ReplicateDBString + " TSPL_TRANSFER_HEAD .Transfer_Date <= '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "'  "
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")  "
            End If
            qry += " and  Transfer_No not in (select Load_Out_No from " + clsCommon.ReplicateDBString + " TSPL_TRANSFER_HEAD as inn where  inn.Transfer_Type='LI' and inn.Location_Type='Physical')"

            Dim finalqry As String = clsCommon.GetQueryWithAllSelectedDataBase(qry, arrSelDB, True)
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(finalqry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then

                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.DataSource = dtgv
                gridformat()
                RadPageView1.SelectedPage = RadPageViewPage2


                btnPrint.Enabled = True
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Printdata(EnumExportTo.Excel)
    End Sub


    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        Printdata(EnumExportTo.PDF)
    End Sub
End Class

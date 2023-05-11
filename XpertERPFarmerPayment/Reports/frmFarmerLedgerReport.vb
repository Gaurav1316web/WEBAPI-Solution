
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class frmFarmerLedgerReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    'Const ReportID As String = "FarmerLedgerReport"
    Dim arrBack As New List(Of String)
    Dim arrVSP As New ArrayList()
    Dim arrFarmer As New ArrayList()

#Region "Variable"
    Private isInsideLoadData As Boolean = False
#End Region

    Sub LoadData()
        Try
            If rbtnDetail.IsChecked = True Then
                Dim dt As DataTable = clsMpMaster.GetLedgerDetailDt(txtFromDate.Value, txtToDate.Value, TxtCustCode.arrValueMember, txtMultDistr.arrValueMember, Nothing, txtLocation.arrValueMember)
                gv3.DataSource = Nothing
                gv3.Rows.Clear()
                gv3.Columns.Clear()
                gv3.DataSource = dt
                gv3.GroupDescriptors.Clear()
                gv3.MasterTemplate.BestFitColumns()
                gv3.EnableFiltering = True
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                Dim dtgv As DataTable = clsMpMaster.GetLedgerSummaryDt(txtFromDate.Value, txtToDate.Value, TxtCustCode.arrValueMember, txtMultDistr.arrValueMember, Nothing, txtLocation.arrValueMember)
                gv3.DataSource = Nothing
                gv3.Rows.Clear()
                gv3.Columns.Clear()
                gv3.DataSource = dtgv
                gv3.GroupDescriptors.Clear()
                gv3.MasterTemplate.BestFitColumns()
                gv3.EnableFiltering = True
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
            
            gv3.ReadOnly = True
            btnGenrate.Enabled = True
            SetGridLayout()
            PageSetupReport_ID = MyBase.Form_ID + IIf(rbtnSummary.IsChecked = True, "S", "D")
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            btnGenrate.Enabled = True
        End Try
    End Sub
    Sub SetGridLayout()
        gv3.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()

        If rbtnSummary.IsChecked Then
            gv3.Columns("FARMER_CODE").IsVisible = True
            gv3.Columns("FARMER_NAME").IsVisible = True
            gv3.Columns("VSP_CODE").IsVisible = True
            gv3.Columns("VSP_NAME").IsVisible = True

            gv3.Columns("FARMER_CODE").Width = 100
            gv3.Columns("FARMER_CODE").HeaderText = "Farmer Code"

            gv3.Columns("FARMER_NAME").Width = 100
            gv3.Columns("FARMER_NAME").HeaderText = "Farmer Name"

            gv3.Columns("VSP_CODE").Width = 100
            gv3.Columns("VSP_CODE").HeaderText = "VSP CODE"

            gv3.Columns("VSP_NAME").Width = 100
            gv3.Columns("VSP_NAME").HeaderText = "VSP Name"

            Dim item7 As New GridViewSummaryItem("Opening", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)

            Dim item5 As New GridViewSummaryItem("Closing", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
        Else
            gv3.Columns("FARMER_CODE").IsVisible = True
            gv3.Columns("FARMER_NAME").IsVisible = True
            gv3.Columns("VSP_CODE").IsVisible = True
            gv3.Columns("VSP_NAME").IsVisible = True

            gv3.Columns("FARMER_CODE").Width = 100
            gv3.Columns("FARMER_CODE").HeaderText = "Farmer Code"

            gv3.Columns("FARMER_NAME").Width = 100
            gv3.Columns("FARMER_NAME").HeaderText = "Farmer Name"

            gv3.Columns("VSP_CODE").Width = 100
            gv3.Columns("VSP_CODE").HeaderText = "VSP CODE"

            gv3.Columns("VSP_NAME").Width = 100
            gv3.Columns("VSP_NAME").HeaderText = "VSP Name"

            gv3.Columns("Trans_Type").Width = 100
            gv3.Columns("Trans_Type").HeaderText = "Trans Type"

            gv3.Columns("Doc_No").Width = 100
            gv3.Columns("Doc_No").HeaderText = "Document No"

            gv3.Columns("Doc_Date").Width = 100
            gv3.Columns("Doc_Date").HeaderText = "Document Date"

            gv3.Columns("APAdjustmentNo").Width = 150
            gv3.Columns("APAdjustmentNo").HeaderText = "Ref Doc No"

            gv3.Columns("Balance").IsVisible = False


        End If

        Dim item1 As New GridViewSummaryItem("Debit", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("Credit", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("Balance", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        gv3.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)



    End Sub
    Private Sub frmFarmerLedgerReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = txtFromDate.Value
        ButtonToolTip.SetToolTip(btnGenrate, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmFarmerLedgerReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnExport.Visible = MyBase.isExport
        btnGenrate.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funReset()
    End Sub

    Sub funReset()
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = txtFromDate.Value
        btnGenrate.Enabled = True
        rbtnSummary.IsChecked = True
        gv3.DataSource = Nothing

        txtLocation.arrValueMember = Nothing

        TxtCustCode.arrValueMember = Nothing
        txtMultDistr.arrValueMember = Nothing
        gv3.Rows.Clear()
        gv3.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub
    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmFarmerLedgerReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub btnGenrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenrate.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(rbtnSummary.IsChecked = True, "S", "D")
        TemplateGridview = gv3
        LoadData()
    End Sub
    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv3.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv3.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv3.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv3.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv3.Columns.Count - 1 Step ii + 1
                        gv3.Columns(ii).IsVisible = False
                        gv3.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv3.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemDelete.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout delete successfully", "Information")
    End Sub

    Private Sub btnExpoExl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Distributer Ledger Report (Detail)")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToExcel("Distributer Ledger Report", gv3, arr, "Salary Register")
        clsCommon.MyExportToExcelGrid("Distributer Ledger Report", gv3, arr, "Distributer Ledger Report", False)
    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Distributer Ledger Report (Detail)")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Distributer Ledger Report", gv3, arr, "Distributer Ledger Report", False)
    End Sub

#Region "grid operations"


#End Region

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)

        'arr.Add("Distributer Ledger Report (Detail)")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'If TxtCustCode.arrValueMember IsNot Nothing AndAlso TxtCustCode.arrValueMember.Count > 0 Then
        '    arr.Add(" Employee : " + clsCommon.GetMulcallStringWithComma(TxtCustCode.arrDispalyMember))
        'End If
        ''clsCommon.MyExportToExcel("Distributer Ledger Report", gv3, arr, "Salary Register")
        'If gv3.Rows.Count <= 0 Then
        '    gv3.Focus()
        '    clsCommon.MyMessageBoxShow("Data not found.")
        'Else
        '    clsCommon.MyExportToExcelGrid("Distributer Ledger Report", gv3, arr, "Distributer Ledger Report", False)
        'End If
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)

        'arr.Add("Distributer Ledger Report (Detail)")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'If TxtCustCode.arrValueMember IsNot Nothing AndAlso TxtCustCode.arrValueMember.Count > 0 Then
        '    arr.Add(" Employee : " + clsCommon.GetMulcallStringWithComma(TxtCustCode.arrDispalyMember))
        'End If
        'clsCommon.MyExportToPDF("Distributer Ledger Report", gv3, arr, "Distributer Ledger Report", False)
        ExportGrid(EnumExportTo.PDF)
    End Sub
    ' ============= Addded by Preeti gupta============
    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv3.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Date :" + clsCommon.GETSERVERDATE() + " ")

                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where PROGRAM_CODE='" & clsUserMgtCode.frmFarmerLedgerReport & "'"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                End If
                If TxtCustCode.arrValueMember IsNot Nothing AndAlso TxtCustCode.arrValueMember.Count > 0 Then
                    arrHeader.Add("VSP : " + clsCommon.GetMulcallStringWithComma(TxtCustCode.arrDispalyMember))
                End If
                If txtMultDistr.arrValueMember IsNot Nothing AndAlso txtMultDistr.arrValueMember.Count > 0 Then
                    arrHeader.Add("Farmer : " + clsCommon.GetMulcallStringWithComma(txtMultDistr.arrDispalyMember))
                End If
                'If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                '    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMult.arrDispalyMember))
                'End If
                'arrHeader.Add("Pay Period: " + txtFromPP.Value)
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
                    transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv3, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv3, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(Me.Text, gv3, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv3_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv3.CellDoubleClick

        If rbtnSummary.IsChecked Then
            If Not arrBack.Contains("Summary") Then
                arrBack.Add("Summary")
            End If
            rbtnDetail.IsChecked = True

            arrVSP = TxtCustCode.arrValueMember
            Dim tmp As New ArrayList()
            tmp.Add(clsCommon.myCstr(gv3.CurrentRow.Cells("Farmer_Code").Value))
            txtMultDistr.arrValueMember = tmp
            arrFarmer = txtMultDistr.arrValueMember
            tmp = New ArrayList
            tmp.Add(clsCommon.myCstr(gv3.CurrentRow.Cells("VSP_Code").Value))
            TxtCustCode.arrValueMember = tmp
            LoadData()
            PageSetupReport_ID = MyBase.Form_ID + IIf(rbtnSummary.IsChecked = True, "S", "D")
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If rbtnDetail.IsChecked Then
                arrBack.Remove("Summary")
                ' MultVendor.arrValueMember = arrVendor
                rbtnSummary.IsChecked = True
                TxtCustCode.arrValueMember = arrVSP
                txtMultDistr.arrValueMember = arrFarmer
                LoadData()
                PageSetupReport_ID = MyBase.Form_ID + IIf(rbtnSummary.IsChecked = True, "S", "D")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    
    Private Sub TxtMultiEmployee__My_Click(sender As Object, e As EventArgs) Handles TxtCustCode._My_Click
        Dim qry As String = " select TSPL_VENDOR_MASTER.Vendor_Code as [Code],Vendor_Name as [Name],VLCH.VLC_Code AS [VLC Code],VLCH.VLC_Name as [VLC Name],VLCH.MCC as [MCC Code]," & _
                            " ISNULL(TSPL_VENDOR_MASTER.Alies_Name,'') As [Alies Name],TSPL_VENDOR_MASTER.Add1,TSPL_VENDOR_MASTER.Add2,Add3,TSPL_VENDOR_MASTER.Closing_Date as [Closing Date], " & _
                            " TSPL_VENDOR_MASTER.Vendor_Group_Code as [Vendor Group Code],TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc as [Vendor Group Description], " & _
                            " TSPL_VENDOR_MASTER.City_Code as [City Code],TSPL_VENDOR_MASTER.City_Code_Desc as [City Description],TSPL_VENDOR_MASTER.State,TSPL_VENDOR_MASTER.Phone1,TSPL_VENDOR_MASTER.Phone2,TSPL_VENDOR_MASTER.Fax,TSPL_VENDOR_MASTER.Email,TSPL_VENDOR_MASTER.WebSite, " & _
                            " TSPL_VENDOR_MASTER.Contact_Person_Name as [Contact Person Name],TSPL_VENDOR_MASTER.Contact_Person_Phone as [Contact Person Phone] from TSPL_VENDOR_MASTER " & _
                            " inner join TSPL_VLC_MASTER_HEAD VLCH ON TSPL_VENDOR_MASTER.Vendor_Code=VLCH.VSP_Code where 2=2 "
        If Not txtLocation.arrValueMember Is Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry = qry & " and VLCH.MCC in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & " )"
        End If

        TxtCustCode.arrValueMember = clsCommon.ShowMultipleSelectForm("DivVendMulSel", qry, "Code", "Name", TxtCustCode.arrValueMember, TxtCustCode.arrDispalyMember)
    End Sub

    Private Sub txtMultDistr__My_Click(sender As Object, e As EventArgs) Handles txtMultDistr._My_Click
        Dim qry As String = " select tspl_mp_master.MP_Code as [Code] ,tspl_mp_master.MP_Name as [Name] ,tspl_mp_master.VLC_Code as [VLC Code] ,tspl_mp_master.Village_Code as [Village Code] ,tspl_mp_master.Father_Name as [Father Name] ,tspl_mp_master.Add1 as [Address1] ,tspl_mp_master.Add2 as [Address2] ,tspl_mp_master.Zila as [Zila] ,tspl_mp_master.Tehsil as [Tehsil] ,tspl_mp_master.City_code as [City Code] ,tspl_mp_master.State_Code as [State Code] ,tspl_mp_master.Country_code as [Country Code] ,tspl_mp_master.Pin_code as [Pin Code] ,tspl_mp_master.Telphone as [Telphone] ,tspl_mp_master.Email as [Email] ,tspl_mp_master.Fax as [Fax] ,tspl_mp_master.DOB as [Date Of Birth] ,tspl_mp_master.Education as [Education] ,tspl_mp_master.Land_Holding as [Land Holding] ,tspl_mp_master.No_Of_Buffaloes as [No Of Buffaloes] ,tspl_mp_master.No_Of_Cows as [No Of Cows] ,tspl_mp_master.No_Of_breedable_milk_animal as [No Of Breedable Milk Animal] ,tspl_mp_master.Milk_production as [Total Milk Production] ,tspl_mp_master.Milk_Home_consumption as [Total Milk Home Consumption] ,tspl_mp_master.Milk_For_sale as [Remaining Milk For Sale] ,tspl_mp_master.PayeeName as [Payee Name] ,tspl_mp_master.BankName as [Bank Name] ,tspl_mp_master.BankBranch as [Bank Branch] ,tspl_mp_master.BankCityCode as [Bank City Code] ,tspl_mp_master.BankStateCode as [Bank State Code] ,tspl_mp_master.IFCICode as [IFCI Code] ,tspl_mp_master.AccountNO as [Account No] ,tspl_mp_master.Created_By as [Created By] ,tspl_mp_master.Created_Date as [Created Date] ,tspl_mp_master.Modified_By as [Modified By] ,tspl_mp_master.Modified_Date as [Modified Date] ,tspl_mp_master.Comp_Code as [Company Code],tspl_mp_master.Mp_code_Vlc_uploader as [MP Code VLC Uploder],VLCH.MCC as [MCC Code],VLCH.VSP_Code as [VSP Code]  From tspl_mp_master left join TSPL_VLC_MASTER_HEAD VLCH on tspl_mp_master.VLC_Code=VLCH.VLC_Code where 2=2 "
        If Not TxtCustCode.arrValueMember Is Nothing AndAlso TxtCustCode.arrValueMember.Count > 0 Then
            qry = qry & " and VLCH.VSP_Code IN (" & clsCommon.GetMulcallString(TxtCustCode.arrValueMember) & " )"
        End If
        txtMultDistr.arrValueMember = clsCommon.ShowMultipleSelectForm("DistrMulSel", qry, "Code", "Name", txtMultDistr.arrValueMember, txtMultDistr.arrDispalyMember)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funReset()
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " SELECT MCC_Code AS Code,MCC_NAME as Name,MCC_Type as [MCC Type],Add1 as Address1,Add2 as Address2,City_code as [City Code],Pin_code as [Pin Code],Telphone,Email FROM TSPL_MCC_MASTER"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("MCCMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
End Class

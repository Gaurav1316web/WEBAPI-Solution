'--Created By --[Panch Raj]--Aaagainst Ticket No-[Demo]

Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class frmProspectDetailReport
    Inherits FrmMainTranScreen
    'Dim Qry As String
    'Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    'Dim ArrDb As New List(Of String)
    Const colInvoiceNo As String = "colInvoiceNo"
    Const colInvoiceDate As String = "colInvoiceDate"
    Const colBillToLocation As String = "colBillToLocation"
    Const colBillToLocationDesc As String = "colBillToLocationDesc"
    Const colInvoiceAmount As String = "colInvoiceAmount"
    Const colCustCode As String = "colCustCode"
    Const colCustName As String = "colCustName"
    Const colSalesmanCode As String = "colSalesmanCode"
    Const colSalesmanName As String = "colSalesmanName"

    Const colVehicleIn As String = "colVehicleIn"
    Const colReceiptIn As String = "colReceiptIn"
    Const colRemarks As String = "colRemarks"
    Const colComments As String = "colComments"
    Const colTransferHO As String = "colTransferHO"
    Dim ReportID As String = "Daily Receipt"
    Dim IsInsideLoadData As Boolean = True
    'New Grid '
    Const colProspectLineNo As String = "colProspectLineNo"
    Const colProspectDocument As String = "colProspectDocNo"
    Const colProspectComm_No As String = "colProspectComm_No"
    Const colProspectCustName As String = "colProspectCustname"
    Const colProspectMode_of_transport As String = "colProspectMode_of_transport"
    Const colProspectQueryRecBy As String = "colProspectQueryRecBy"
    Const colProspectQueryvalue As String = "colProspectQueryValue"
    Const colProspectcategory As String = "colProspectCategory"
    Const colProspectcategoryDesc As String = "colProspectCategoryDesc"
    Const colProspectInternalCheck As String = "colProspectInternalcheck"
    Const colProspectInternalDesc As String = "colProspectInternalDesc"
    Const colProspectEmail As String = "colProspectEmail"
    Const colProspectPhoneNo As String = "colProspectPhoneNo"
    Const colProspectItemCode As String = "colProspectItemCode"
    Const colProspectItemDesc As String = "colProspectItemDesc"
    Const colProspectCustomerSource As String = "colProspectCustomerSource"
    Const colProspectCustomerDesc As String = "colProspectCustomerDesc"
    Const colProspectCloseStatus As String = "colProspectCloseStatus"
    Const colProspectCloseQuery As String = "colProspectCloseQuery"
    Const colProspectCloseValue As String = "colProspectCloseValue"
    Const colProspectForwardStatus As String = "colProspectForwardStatus"
    Const colProspectForward_Query_Active As String = "colProspectForwardQueryActive"
    Const colProspectForwardUser As String = "colProspectForwarduser"
    Const colProspectForwardVendor As String = "colProspectForwardVendor"


    Dim dt1 As DataTable

    Private Sub frmReceiptChallan_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
        '    SaveData()
        'Else
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso MyBase.isPostFlag Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        End If
    End Sub


    Private Sub frmReceiptChallan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadProspect()

        'cbgCustomer.CheckedAll()
        'LoadLocation()
        'cbgLocation.CheckedAll()
        rbtncustall.IsChecked = True
        RbtnAll.IsChecked = True
        'rbtnlocall.IsChecked = True
        ' ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Adding New Trasnaction")
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Reset Trasnaction")
        'btnSave.Visible = False
        MyCheckBoxGrid1.DataSource = clsProspectHead.GetProspectCategoryTable()
        MyCheckBoxGrid1.ValueMember = "Code"
        MyCheckBoxGrid1.DisplayMember = "Name"

    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmProspectDetailReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

        btnExport.Visible = MyBase.isExport

        'btnSave.Visible = MyBase.isModifyFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub


    Sub LoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.DataSource = dt1
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).Width = 100
            gv1.Columns(ii).IsVisible = True
        Next

        gv1.EnableFiltering = True
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = True
        gv1.AllowAddNewRow = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub Reset()
        Dim serverdate As Date = clsCommon.GETSERVERDATE
        Me.dtpFrom.Value = serverdate
        Me.dtpTo.Value = serverdate
        'Me.fndVehicleCode.Value = Nothing
        ' Me.fndCustCode.Value = Nothing

        'LoadBlankGrid()
    End Sub

    ''Anubhooti 30-June-2014
    Sub LoadProspect()

        cbgProspectType.DataSource = clsProspectHead.GetCommTypeTable()
        cbgProspectType.ValueMember = "Code"
        cbgProspectType.DisplayMember = "Name"
    End Sub
    'Sub LoadLocation()
    '    Dim qry As String = " Select Location_Code As Code ,Location_Desc as Name From TSPL_Location_Master "
    '    cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    cbgLocation.ValueMember = "Code"
    '    cbgLocation.DisplayMember = "Name"
    'End Sub
    ''

    'Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
    '    Try
    '        If SaveData() Then
    '            RadMessageBox.Show("Data Saved Successfully")
    '            Reset()
    '        End If
    '    Catch ex As Exception
    '        RadMessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    'Private Function SaveData() As Boolean
    '    Try
    '        If AllowToSave() Then
    '            Dim Arr As New List(Of clsReceiptChallan)
    '            For Each grow As GridViewRowInfo In gv1.Rows
    '                Dim objTr As New clsReceiptChallan()
    '                objTr.SALE_INVOICE_NO = clsCommon.myCstr(grow.Cells(colInvoiceNo).Value)
    '                objTr.VEHICLE_IN = IIf(clsCommon.myCBool(grow.Cells(colVehicleIn).Value) = True, "Y", "N")
    '                objTr.RECEIPT_IN = IIf(clsCommon.myCBool(grow.Cells(colReceiptIn).Value) = True, "Y", "N")
    '                objTr.TRANSFER_HO = IIf(clsCommon.myCBool(grow.Cells(colTransferHO).Value) = True, "Y", "N")
    '                objTr.REMARKS = clsCommon.myCstr(grow.Cells(colRemarks).Value)
    '                objTr.COMMENTS = clsCommon.myCstr(grow.Cells(colComments).Value)
    '                Arr.Add(objTr)
    '            Next
    '            If clsReceiptChallan.SaveData(Arr) Then
    '                Return True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function

    'Private Function AllowToSave() As Boolean
    '    Return True
    'End Function


    Private Sub LoadDetails()
        Try
            IsInsideLoadData = True

            If rbtncustslct.IsChecked AndAlso cbgProspectType.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow("Please Select Atleast One Prospect", Me.Text)
                Return
            End If
            Dim Type As String = ""
            If rbtncustall.IsChecked Then
                Type = "All"
            Else
                Type = clsCommon.GetMulcallString(cbgProspectType.CheckedValue)
            End If
            Dim qry As String
            ' qry = "select " & Type & " as Prospect_Type,'" & clsCommon.GetPrintDate(dtpFrom.Value, "dd/MMM/yyyy") & "' as From_Date,'" & clsCommon.GetPrintDate(dtpTo.Value, "dd/MMM/yyyy") & "' as To_Date,TSPL_PROSPECT_HEAD.Document_Code,TSPL_PROSPECT_HEAD.Customer_Name,TSPL_PROSPECT_HEAD.Document_Date, TSPL_PROSPECT_COMMUNICATION.Comm_Type,TSPL_PROSPECT_COMMUNICATION.Line_No,convert(varchar,TSPL_PROSPECT_COMMUNICATION.Comm_Date,105) as Comm_Date,  TSPL_PROSPECT_COMMUNICATION.Comm_Remarks ,TSPL_PROSPECT_HEAD.Modify_By As 'Updated By' " & _
            '" from TSPL_PROSPECT_HEAD " & _
            '" inner join TSPL_PROSPECT_COMMUNICATION  on TSPL_PROSPECT_HEAD.Document_Code=TSPL_PROSPECT_COMMUNICATION.Document_Code  " & _

            'qry = "select e1.Document_Code as Document,e1.Item_Category as ItemCode,e3.DESCRIPTION as ItemName,e1.Customer_Name as Customer,e1.comm_type as Communication,e1.Cust_Source as CustomerSource,e1.Mode_Of_Transport as Transport,Prospect_category as Category,e1.QueryRecBy,e1.Query_Value as Description,e1.Close_Status as Closed,e1.Close_Query as ClosedCategory,e1.Close_Query_Value as Description,e1.Forward_Query_Active as ForwardActivate,e1.Forward_Status as ForwardStatus,Internal_Check as InternalStatus,Internal_Desc as InternalDescription, " & _
            '"e1.Forward_User_Query as ForwardUser,e2.Vendor_Name as Vendor from TSPL_PROSPECT_HEAD as e1 " & _
            '"left outer join TSPL_VENDOR_MASTER as e2 on e1.Forward_Vendor=e2.Vendor_Code " & _
            '"left outer join TSPL_ITEM_CATEGORY_STRUCTURE as e3 on e1.Item_Category=e3.ITEM_CATEGORY_STRUCT_CODE " & _

            qry = "select ROW_NUMBER() OVER(ORDER BY TSPL_PROSPECT_HEAD.Document_Code DESC) AS SrNo, TSPL_PROSPECT_HEAD.Document_Code as DocumentNo,TSPL_PROSPECT_HEAD.Document_Date as Doc_Date,TSPL_PROSPECT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc as ItemDesc,TSPL_PROSPECT_DETAIL.Qty,TSPL_PROSPECT_DETAIL.Cost as Rate,TSPL_PROSPECT_HEAD.QueryRecBy as 'Mode of Query',TSPL_PROSPECT_HEAD.Query_Value as 'Detail',TSPL_PROSPECT_HEAD.Mode_Of_Transport,TSPL_PROSPECT_HEAD.Forward_User_Query as 'Query Forwarded',TSPL_PROSPECT_HEAD.Customer_Name as 'Customer Name',TSPL_PROSPECT_HEAD.PhoneNo as 'Contact No',TSPL_PROSPECT_HEAD.Email,TSPL_PROSPECT_HEAD.Close_Query,TSPL_PROSPECT_HEAD.Close_Query_Value,case TSPL_PROSPECT_HEAD.Close_Status when '1' then 'Close' when '0' then 'Open' end as Close_Status,TSPL_PROSPECT_HEAD.Bill_To_Location,TSPL_PROSPECT_HEAD.Item_Category as 'Itam Category',TSPL_PROSPECT_HEAD.Comp_Code as Company,TSPL_PROSPECT_LOCATION.State_Code as State,TSPL_PROSPECT_LOCATION.Zone_Code as Zone, TSPL_PROSPECT_LOCATION.Address as Address ,TSPL_PROSPECT_LOCATION.Location,TSPL_PROSPECT_HEAD.Comm_Type as Type,TSPL_PROSPECT_HEAD.prospect_category as 'Category',TSPL_PROSPECT_DETAIL.Specification,TSPL_PROSPECT_DETAIL.Remarks,TSPL_PROSPECT_HEAD.Modify_By as 'Created By' from TSPL_PROSPECT_HEAD"
            qry += " left outer join TSPL_PROSPECT_DETAIL on TSPL_PROSPECT_DETAIL.Document_Code=TSPL_PROSPECT_HEAD.Document_Code"
            qry += " left outer join TSPL_PROSPECT_LOCATION on TSPL_PROSPECT_LOCATION.Document_Code=TSPL_PROSPECT_HEAD.Document_Code"
            qry += " left outer join TSPL_ITEM_MASTER on tspl_item_master.Item_Code=TSPL_PROSPECT_DETAIL.Item_Code "
            qry += " where 2=2 and cast(TSPL_PROSPECT_HEAD.Document_Date as date)  between '" & clsCommon.GetPrintDate(Me.dtpFrom.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(Me.dtpTo.Value, "dd/MMM/yyyy") & "'"

            If rbtncustslct.IsChecked AndAlso cbgProspectType.CheckedValue.Count > 0 Then
                qry = qry & "  and TSPL_PROSPECT_HEAD.Comm_Type in (" & clsCommon.GetMulcallString(cbgProspectType.CheckedValue) & ")"
            End If
            If rbtnSelect.IsChecked AndAlso MyCheckBoxGrid1.CheckedValue.Count > 0 Then
                qry = qry & "  and TSPL_PROSPECT_HEAD.prospect_category in (" & clsCommon.GetMulcallString(MyCheckBoxGrid1.CheckedValue) & ")"
            End If
            If chkClosed.Checked = True Then
                qry = qry & "  and TSPL_PROSPECT_HEAD.Close_Status='True'"
            End If
            If ChkForward.Checked = True Then
                qry = qry & "  and TSPL_PROSPECT_HEAD.Forward_Status='True'"
            End If
            If chkInternalRef.Checked = True Then
                qry = qry & "  and TSPL_PROSPECT_HEAD.Internal_Check='True'"
            End If
            If FndEmployeeMult.arrValueMember IsNot Nothing AndAlso FndEmployeeMult.arrValueMember.Count > 0 Then
                qry += " and TSPL_PROSPECT_HEAD.Forward_User_Query  in (" + clsCommon.GetMulcallString(FndEmployeeMult.arrValueMember) + ") "
            End If
            If (clsCommon.myLen(txtItemCode.Value) > 0) Then
                qry += " and TSPL_PROSPECT_DETAIL.Item_Code='" + txtItemCode.Value + "'"
            End If
            If (clsCommon.myLen(txtItemCategory.Value) > 0) Then
                qry += " and TSPL_PROSPECT_HEAD.Item_Category='" + txtItemCategory.Value + "'"
            End If
            If (clsCommon.myLen(txtStateFinder.Value) > 0) Then
                qry += " and TSPL_PROSPECT_LOCATION.State_Code='" + txtStateFinder.Value + "'"
            End If



            dt1 = clsDBFuncationality.GetDataTable(qry)
            'gv1.DataSource = Nothing
            gv1.DataSource = dt1

            RadPageView1.SelectedPage = RadPageViewPage2

            'ReStoreGridLayout()

            'Dim qry1 As String = " select  max(TSPL_PROSPECT_HEAD.Document_Code) as Document_Code,TSPL_PROSPECT_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc, max(TSPL_PROSPECT_DETAIL.Qty) as Qty  " & _
            '" from TSPL_PROSPECT_HEAD  inner join TSPL_PROSPECT_DETAIL on TSPL_PROSPECT_HEAD.Document_Code=TSPL_PROSPECT_DETAIL.Document_Code  " & _
            '" left join TSPL_ITEM_MASTER on TSPL_PROSPECT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
            '" left join TSPL_PROSPECT_COMMUNICATION  on TSPL_PROSPECT_HEAD.Document_Code=TSPL_PROSPECT_COMMUNICATION.Document_Code " & _
            '" where 2=2 and cast(TSPL_PROSPECT_HEAD.Document_Date as date)  between '" & clsCommon.GetPrintDate(Me.dtpFrom.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(Me.dtpTo.Value, "dd/MMM/yyyy") & "'"

            'If rbtncustslct.IsChecked AndAlso cbgProspectType.CheckedValue.Count > 0 Then
            '    qry1 = qry1 & "  and TSPL_PROSPECT_head.Comm_Type in (" & clsCommon.GetMulcallString(cbgProspectType.CheckedValue) & ")"
            'End If
            'qry1 = qry1 & " group by TSPL_PROSPECT_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc"
            'frmCrystalReportViewer.funsubreport(CrystalReportFolder.NewSalesReports, qry, qry1, "crptProspectReport", Me.Text, "crptProspectReportSub.rpt")

            '  frmCrystalReportViewer.funsubreport(CrystalReportFolder.NewSalesReports, qry, "crptProspectReport", Me.Text, "crptProspectReportSub.rpt")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            IsInsideLoadData = False
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
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
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        'Reset()
        LoadDetails()


    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reseted()
    End Sub
    Private Sub Reseted()
        chkClosed.Checked = False
        ChkForward.Checked = False
        gv1.Rows.Clear()

    End Sub



    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    'Private Sub fndCustCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
    '    Dim qry As String = "select Cust_Code as Code,Customer_Name as Name,TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name)>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Address,TSPL_CUSTOMER_MASTER.Terms_Code as [Term Code] , TSPL_TERMS_MASTER.Terms_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Salesman_Code as [Salesman Code],Salesman_Desc as Salesman "
    '    qry += " from TSPL_CUSTOMER_MASTER "
    '    qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
    '    qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
    '    qry += " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code"
    '    qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'"
    '    fndCustCode.Value = clsCommon.ShowSelectForm("SNSOVendorFndr", qry, "Code", "", fndCustCode.Value, "Code", isButtonClicked)
    '    If clsCommon.myLen(fndCustCode.Value) > 0 Then
    '        lblCustName.Text = clsCustomerMasterNew.GetData(fndCustCode.Value, Nothing).Customer_Name
    '    End If

    'End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        If (gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        ExportToExcel(Exporter.Excel)
    End Sub

    Private Sub PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PDF.Click
        If (gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        ExportToExcel(Exporter.PDF)
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Sub ExportToExcel(ByVal IsPrint As Exporter)
        Dim arrHeader As List(Of String) = New List(Of String)()
        Dim strTemp As String = ""
        arrHeader.Add("From Date : " + clsCommon.GetPrintDate(dtpFrom.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpTo.Value, "dd/MM/yyyy") + " ")
        'arrHeader.Add("Customer : " + fndCustCode.Value)


        If IsPrint = Exporter.Excel Then
            clsCommon.MyExportToExcelGrid("Daily Receipt", gv1, arrHeader, Me.Text)
        ElseIf IsPrint = Exporter.PDF Then
            clsCommon.MyExportToPDF("Daily Receipt", gv1, arrHeader, Me.Text, True)
        End If
    End Sub


    Private Sub rbtncustslct_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtncustslct.ToggleStateChanged
        cbgProspectType.Enabled = rbtncustslct.IsChecked
    End Sub
    Private Sub rbtncustall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtncustall.ToggleStateChanged
        cbgProspectType.Enabled = rbtncustslct.IsChecked
    End Sub
    Private Sub RbtnAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RbtnAll.ToggleStateChanged
        MyCheckBoxGrid1.Enabled = rbtnSelect.IsChecked
    End Sub
    Private Sub rbtnSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnSelect.ToggleStateChanged
        MyCheckBoxGrid1.Enabled = rbtnSelect.IsChecked
    End Sub
    Private Sub FndEmployeeMult__My_Click(sender As Object, e As EventArgs) Handles FndEmployeeMult._My_Click

        Dim qry As String = "select User_Code , User_Name as Name from TSPL_USER_MASTER  "

        FndEmployeeMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "User_Code", "Name", FndEmployeeMult.arrValueMember, FndEmployeeMult.arrDispalyMember)
    End Sub
    Private Sub txtStateFinder__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtStateFinder._MYValidating
        txtStateFinder.Value = clsStateMaster.getFinder("", txtStateFinder.Value, isButtonClicked)

    End Sub
    Private Sub txtItemCategory__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtItemCategory._MYValidating
        Dim qry As String = "select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_STRUCT_CODE as Code,TSPL_ITEM_CATEGORY_STRUCTURE.DESCRIPTION as Description,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION AS ItemValue,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as LevelGroup from TSPL_ITEM_CATEGORY_STRUCT_DETAIL LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE left outer join TSPL_ITEM_CATEGORY_LEVEL on  TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE and TSPL_ITEM_CATEGORY_LEVEL.form_type=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type LEFT OUTER JOIN TSPL_ITEM_CATEGORY_STRUCTURE ON TSPL_ITEM_CATEGORY_STRUCTURE.ITEM_CATEGORY_STRUCT_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_STRUCT_CODE "
        txtItemCategory.Value = clsCommon.ShowSelectForm("SNOITEMCATEGORY", qry, "ItemValue", "", txtItemCategory.Value, "ItemValue", isButtonClicked)
        lblCategory.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_ITEM_CATEGORY_STRUCTURE.DESCRIPTION as Description from TSPL_ITEM_CATEGORY_STRUCT_DETAIL LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE left outer join TSPL_ITEM_CATEGORY_LEVEL on  TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE and TSPL_ITEM_CATEGORY_LEVEL.form_type=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type LEFT OUTER JOIN TSPL_ITEM_CATEGORY_STRUCTURE ON TSPL_ITEM_CATEGORY_STRUCTURE.ITEM_CATEGORY_STRUCT_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_STRUCT_CODE where TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION='" + txtItemCategory.Value + "'"))
    End Sub
    Private Sub txtItemCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtItemCode._MYValidating
        txtItemCode.Value = clsItemMaster.getFinder("", txtItemCode.Value, isButtonClicked)
        'lblItemName.Text = clsItemMaster.GetName(txtItemCode.Value)
    End Sub
End Class

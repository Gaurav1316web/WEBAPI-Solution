Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
' Update BY abhishek as on 29 oct 2012 4:45 pm For Excel
' by vipin for pdf work  (31/01/2013)
'--Ticket No-[BM00000000604]-
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
''changes by richa agarwal againt ticket no (BM00000005627),BM00000005684

Public Class RptIssueReturnHirerachyWise
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim isprint As Boolean
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub RptIssueReturnHirerachyWise_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.Alt And e.KeyCode = Keys.P Then
            'PrintData1()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()

        End If


    End Sub





    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptIssueReturnHirerachyWise)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
    End Sub


    Private Sub RptIssueReturnHirerachyWise_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ddlRptType.SelectedIndex = 0
        chkDeapartmentAll.IsChecked = True
        'chkItemAll.IsChecked = True
        chkCategoryAll.IsChecked = True
        chkSubCategoryAll.IsChecked = True
        'chkLocationAll.IsChecked = True
        chkVehicleNoAll.IsChecked = True
        chkmachineAll.IsChecked = True
        ''richa 20/02/2015
        'chkCostCenterAll.IsChecked = True
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        DepartmentLoad()
        'ItemLoad()
        CategoryLoad()
        SubCategoryLoad()
        'LoadLocation()
        LoadVehicle()
        'LoadCostCenter()
        LoadMachine()
        grpbxDepartment.Enabled = False
        RadGroupBox2.Enabled = False
        RadGroupBox1.Enabled = False
        isprint = False
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ' ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "NET-ISS-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function


    Public Sub DepartmentLoad()
        qry = "select Segment_code as Code,Description as Name from TSPL_GL_SEGMENT_CODE where Seg_No  ='3' "
        cbgDepartment.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDepartment.ValueMember = "Code"
    End Sub

    Public Sub CategoryLoad()
        qry = "select Category_Code as Code,Category_Name  as Name from TSPL_Item_Category  "
        cbgCategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCategory.ValueMember = "Code"
    End Sub
    Public Sub SubCategoryLoad()
        qry = "select sub_Category_Code as Code,Description as Name  from TSPL_ITEM_SUB_CATEGORY  "
        cbgSubCategroy.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSubCategroy.ValueMember = "Code"
    End Sub


    Public Sub LoadVehicle()
        Dim qry As String = "select Segment_code as Code , Description as Description From TSPL_GL_SEGMENT_CODE where Seg_No  ='2'"
        cbgVehicle.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVehicle.ValueMember = "Code"
    End Sub
    Public Sub LoadMachine()
        Dim qry As String = "select Segment_code as Code , Description as Description From TSPL_GL_SEGMENT_CODE where Seg_No  ='5'"
        cbgMachine.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgMachine.ValueMember = "Code"
    End Sub
    'kunal - > UDL Ticket ( "Overloaded Function so that existing function not get affect " )
    Sub PrintData(ByVal companyCode As String, ByVal reportFilterType As String)
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "chkDocWise") Then
            Try
                Dim qry As String = Nothing
                Dim fromdate As String = clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy")
                Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
                Dim DepartmentArr As ArrayList = cbgDepartment.CheckedValue
                Dim ItemArr As ArrayList = cbgItem.CheckedValue
                Dim CategoryArr As ArrayList = cbgCategory.CheckedValue
                Dim SubCategoryArr As ArrayList = cbgSubCategroy.CheckedValue
                Dim Location As ArrayList = cbgLocation.CheckedValue
                Dim vehicle As ArrayList = cbgVehicle.CheckedValue
                Dim machine As ArrayList = cbgMachine.CheckedValue
                Dim CostCenter As ArrayList = cbgCostCenter.CheckedValue
                Dim Dept As String = ""
                Dim MachNo As String = ""
                Dim vehicleNo As String = ""
                Dim item As String = ""
                Dim location1 As String = ""
                Dim itemcategory As String = ""
                Dim costcenter1 As String = String.Empty
                Dim StrDept As String = ""
                Dim StrMachNo As String = ""
                Dim StrvehicleNo As String = ""
                Dim Stritem As String = ""
                Dim Strlocation As String = ""
                Dim Stritemcategory As String = ""
                Dim Stritemsubcategory As String = ""
                Dim strcostcenter As String = String.Empty
                If chkCategorySelect.IsChecked = True AndAlso cbgCategory.CheckedValue.Count > 0 Then
                    itemcategory = "'" + clsCommon.GetMulcallString(cbgCategory.CheckedValue) + "'"
                    Stritemcategory = itemcategory.Replace("'", "")
                End If
                If cbgDepartment.CheckedValue.Count > 0 Then
                    Dept = "'" + clsCommon.GetMulcallString(DepartmentArr) + "'"
                    StrDept = Dept.Replace("'", "")
                End If
                If cbgSubCategroy.CheckedValue.Count > 0 Then
                    itemcategory = "'" + clsCommon.GetMulcallString(cbgSubCategroy.CheckedValue) + "'"
                    Stritemcategory = itemcategory.Replace("'", "")
                End If
                If cbgMachine.CheckedValue.Count > 0 Then
                    MachNo = "'" + clsCommon.GetMulcallString(machine) + "'"
                    StrMachNo = MachNo.Replace("'", "")
                End If
                If cbgVehicle.CheckedValue.Count > 0 Then
                    vehicleNo = "'" + clsCommon.GetMulcallString(vehicle) + "'"
                    StrvehicleNo = vehicleNo.Replace("'", "")
                End If

                Dim UpperQry1 As String = ""
                Dim UpperQry2 As String = ""
                Dim strInnerQry As String = ""
                Dim LowerQry1 As String = ""
                Dim LowerQry2 As String = ""
                If chkDocWise.Checked Then

                    ' UpperQry1 = " SELECT * FROM (SELECT '" + fromdate + "' AS FromDate, '" + Todate + "' AS ToDate, issuertn.docNo, CONVERT(varchar, issuertn.doc_date, 103) AS doc_date, issuertn.COMMENT, TSPL_ITEM_MASTER_CATEGORY.item_category_code AS [Item Category Code], TSPL_ITEM_CATEGORY_LEVEL.description AS [Item Category Name], TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values AS [item Cagetory Values], TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION AS [item Cagetory Values Name], coalesce(TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code,'') as Sub_Category_Code , Location, tspl_location_master.Location_Desc, issuertn.item_code, issuertn.Item_Desc, issuertn.issued_qty as [issued_qty], issuertn.ReturnQty as [Return_qty], issuertn.NetQty as [Net_qty], issuertn.IssueAMt as [issued_Value], issuertn.ReturnAmt as [Return_Value], issuertn.[Net Amt] as [Net_Value], issuertn.comp_code, Vehicle_Id, COALESCE(TSPL_GL_SEGMENT_CODE.Description, '') AS [Vehicle Name], issuertn.Cost_Centre_Code AS [Cost Center Code], TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name AS [Cost Center Desc], TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code AS [Hirerachy Code], TSPL_HIRERACHY_LEVEL_MASTER.Description AS [Hirerachy Name], issuertn.GL_account, TSPL_GL_ACCOUNTS.Description AS [GL Account Desc], TSPL_COMPANY_MASTER.Comp_Name AS compname, TSPL_COMPANY_MASTER.Logo_Img AS Image1, TSPL_COMPANY_MASTER.Logo_Img2 AS Image2, (TSPL_COMPANY_MASTER.Add1 + CASE WHEN TSPL_COMPANY_MASTER.Add2 = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.Add2, 103) END + CASE WHEN TSPL_COMPANY_MASTER.Add3 = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.Add3, 103) END + CASE WHEN TSPL_COMPANY_MASTER.City_Code = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.City_Code, 103) END + CASE WHEN TSPL_COMPANY_MASTER.State = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.State) END + CASE WHEN TSPL_COMPANY_MASTER.Pincode = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.Pincode, 103) END) AS address , Dept , Dept_Desc  "

                    UpperQry1 = " SELECT * FROM (SELECT '" + fromdate + "' AS FromDate, '" + Todate + "' AS ToDate, issuertn.docNo AS Doc_No, CONVERT(varchar, issuertn.doc_date, 103) AS doc_date, issuertn.COMMENT AS Comment, TSPL_ITEM_MASTER_CATEGORY.item_category_code AS [Item Category Code], TSPL_ITEM_CATEGORY_LEVEL.description AS [Item Category Name], TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values AS [item Cagetory Values], TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION AS [item Cagetory Values Name], COALESCE(TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code, '') AS Sub_Category_Code, Location, tspl_location_master.Location_Desc, issuertn.item_code, issuertn.Item_Desc, issuertn.unit_code, issuertn.issued_qty AS [issued_qty], issuertn.ReturnQty AS [Return_qty], issuertn.NetQty AS [Net_qty], issuertn.IssueAMt AS [issued_Value], issuertn.ReturnAmt AS [Return_Value], issuertn.[Net Amt] AS [Net_Value], issuertn.comp_code, TSPL_COMPANY_MASTER.Comp_Name AS compname, issuertn.Vehicle_Id AS VehicleId, COALESCE(TSPL_GL_SEGMENT_CODE.Description, '') AS [Vehiclename], issuertn.Cost_Centre_Code AS [Cost Center Code], TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name AS [Cost Center Desc], TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code AS [Hirerachy Code], TSPL_HIRERACHY_LEVEL_MASTER.Description AS [Hirerachy Name], issuertn.GL_account, TSPL_GL_ACCOUNTS.Description AS [GL Account Desc], TSPL_COMPANY_MASTER.Logo_Img AS Image1, TSPL_COMPANY_MASTER.Logo_Img2 AS Image2, (TSPL_COMPANY_MASTER.Add1 + CASE WHEN TSPL_COMPANY_MASTER.Add2 = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.Add2, 103) END + CASE WHEN TSPL_COMPANY_MASTER.Add3 = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.Add3, 103) END + CASE WHEN TSPL_COMPANY_MASTER.City_Code = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.City_Code, 103) END + CASE WHEN TSPL_COMPANY_MASTER.State = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.State) END + CASE WHEN TSPL_COMPANY_MASTER.Pincode = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.Pincode, 103) END) AS address, Dept, Dept_Desc "

                    UpperQry2 = " FROM (SELECT docNo, MAX(Comment) AS Comment, item_code, MAX(Item_Desc) AS Item_Desc, MAX(Unit_code) AS Unit_code, SUM(issued_qty) AS issued_qty, SUM(ReturnQty) AS ReturnQty, (SUM(issued_qty) - SUM(ReturnQty)) AS NetQty, SUM(IssueAMt) AS IssueAMt, SUM(ReturnAmt) AS ReturnAmt, (SUM(IssueAMt) - SUM(ReturnAmt)) AS [Net Amt], MAX(Vehicle_Id) AS Vehicle_Id "



                    UpperQry2 += " , MAX(Cost_Centre_Code) AS Cost_Centre_Code, MAX(Hirerachy_Code) AS Hirerachy_Code, MAX(GL_account) AS GL_account, MAX(doc_date) AS doc_date, MAX(comp_code) AS comp_code, MAX(From_Location) AS [Location], MAX(Unit_Cost) AS Unit_Cost , MAX(Dept) AS Dept , MAX(Dept_Desc) AS Dept_Desc "

                    UpperQry2 += " FROM (SELECT h.doc_no AS docNo, h.Comment, d.item_code, d.Item_Desc, d.Unit_code, issued_qty, 0 AS ReturnQty, Amount AS IssueAMt, 0 AS ReturnAmt, h.Vehicle_Id, d.Cost_Centre_Code, d.Hirerachy_Code, d.GL_account, h.doc_date, h.comp_code, h.From_Location, d.Unit_Cost , h.Dept , H.Dept_Desc  FROM tspl_issuereturn_head h LEFT OUTER JOIN TSPL_IssueReturn_DETAIL d ON h.doc_no = d.doc_no WHERE h.doc_type = 'Issue' "

                    UpperQry2 += " UNION ALL "

                    UpperQry2 += " SELECT h.req_issueno AS docNo, h.Comment, d.item_code, d.Item_Desc, d.Unit_code, 0 AS issued_qty, issued_qty AS ReturnQty, 0 AS IssueAMt, Amount AS ReturnAmt, h.Vehicle_Id, d.Cost_Centre_Code, d.Hirerachy_Code, d.GL_account, h.doc_date, h.comp_code, h.From_Location, d.Unit_Cost , h.Dept , H.Dept_Desc FROM tspl_issuereturn_head h LEFT OUTER JOIN TSPL_IssueReturn_DETAIL d ON h.doc_no = d.doc_no WHERE h.doc_type = 'Return') coreSubQry "

                    UpperQry2 += " GROUP BY docNo, item_code) AS issuertn "

                    UpperQry2 += " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = issuertn.Item_Code LEFT OUTER JOIN TSPL_ITEM_SUB_CATEGORY ON TSPL_ITEM_SUB_CATEGORY.Category_Code = TSPL_ITEM_MASTER.item_category AND tspl_item_sub_category.Sub_Category_Code = TSPL_ITEM_MASTER.sub_item_category LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE = TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code AND TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE = TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values LEFT JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.Item_Category_Code = TSPL_ITEM_CATEGORY_LEVEL_VALUES.Item_Category_Code LEFT OUTER JOIN TSPL_Item_Category ON TSPL_Item_Category.Category_Code = TSPL_ITEM_MASTER.item_category LEFT OUTER JOIN TSPL_GL_SEGMENT_CODE ON TSPL_GL_SEGMENT_CODE.Segment_code = issuertn.Vehicle_Id AND TSPL_GL_SEGMENT_CODE.Seg_No = 2 LEFT OUTER JOIN TSPL_COST_CENTRE_FINANCIAL ON TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code = issuertn.Cost_Centre_Code LEFT OUTER JOIN TSPL_HIRERACHY_LEVEL_MASTER ON TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code = issuertn.Hirerachy_Code LEFT OUTER JOIN TSPL_GL_ACCOUNTS ON TSPL_GL_ACCOUNTS.Account_Code = issuertn.GL_account INNER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = issuertn.comp_code LEFT OUTER JOIN tspl_location_master ON issuertn.Location = tspl_location_master.Location_Code WHERE 2 = 2) xxx "

                    UpperQry2 += " WHERE 1 = 1 "

                End If

                UpperQry2 += " AND CONVERT(date, Doc_Date, 103) >= Convert(Date,'" & dtpFromdate1.Value.Date & "',103) "
                UpperQry2 += " AND CONVERT(date, Doc_Date, 103) <= Convert(Date,'" & dtpToDate.Value.Date & "',103)  "

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    UpperQry2 += " AND Location IN (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
                End If

                If txtHirerachyCode.arrValueMember IsNot Nothing AndAlso txtHirerachyCode.arrValueMember.Count > 0 Then
                    UpperQry2 += " AND [Hirerachy Code] IN (" + clsCommon.GetMulcallString(txtHirerachyCode.arrValueMember) + ") "
                End If

                If chkDepartmentSelect.IsChecked = True AndAlso cbgDepartment.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Department", Me.Text)
                    Return
                ElseIf cbgDepartment.CheckedValue.Count > 0 Then
                    UpperQry2 += " AND Dept IN (" + clsCommon.GetMulcallString(DepartmentArr) + ")"
                End If

                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    UpperQry2 += " AND Item_Code IN   (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") "
                End If
                If chkCategorySelect.IsChecked = True AndAlso cbgCategory.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Item Code", Me.Text)
                    Return
                ElseIf cbgCategory.CheckedValue.Count > 0 Then
                    UpperQry2 += " AND [Item Category Code] in  (" + clsCommon.GetMulcallString(CategoryArr) + ")"
                End If

                If chkSubCategroySelect.IsChecked = True AndAlso cbgSubCategroy.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Sub Category", Me.Text)
                    Return
                ElseIf cbgSubCategroy.CheckedValue.Count > 0 Then
                    UpperQry2 += " AND [Sub_Category_Code] in  (" + clsCommon.GetMulcallString(SubCategoryArr) + ")"
                End If

                qry = UpperQry1 + UpperQry2
                Dim dt As New DataTable
                If qry IsNot Nothing AndAlso clsCommon.myLen(qry) > 0 Then
                    dt = clsDBFuncationality.GetDataTable(qry)
                End If
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "No Record Found to Display", Me.Text)
                Else
                    If dt IsNot Nothing And dt.Rows.Count > 0 Then
                        gv.DataSource = Nothing
                        gv.Rows.Clear()
                        gv.Columns.Clear()
                        gv.DataSource = dt
                        For Each columns As GridViewColumn In gv.Columns
                            columns.Width = 150
                            columns.ReadOnly = True
                            If columns.Name = "Item Category Name" Or columns.Name = "Image1" Or columns.Name = "Image2" Or columns.Name = "address" Then
                                columns.IsVisible = False
                            End If
                        Next
                        RadPageView1.SelectedPage = RadPageViewPage2
                    End If

                    If isprint Then
                        Dim frmCRV As New frmCrystalReportViewer()
                        If chkDocWise.Checked Then
                            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptIssReturn_DocWiseSummary", "Issue/Return Document Wise Summary")
                        ElseIf ddlRptType.SelectedIndex = 0 AndAlso chkVehicleWise.Checked = False Then
                            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptNetIssue", "Issue Item Wise Summary")
                        ElseIf ddlRptType.SelectedIndex = 1 AndAlso chkVehicleWise.Checked = False Then
                            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "IssueOrReturnItemWiseSummary", "Issue Or Return Item Wise Summary")
                        ElseIf chkVehicleWise.Checked = True Then
                            If ddlRptType.SelectedIndex = 1 Then
                                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "IssueOrReturnVehicleWiseItemSummary", " Issue Or Retrun Vehicle Wise Item Summery ")
                            ElseIf ddlRptType.SelectedIndex = 0 Then
                                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptVehicleWiseIssue", " Issue Or Retrun Vehicle Wise Issue ")
                            End If
                        End If
                        frmCRV = Nothing
                    End If
                End If

            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Sub PrintData()
        Try
            Dim qry As String
            Dim fromdate As String = clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy")
            Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
            Dim DepartmentArr As ArrayList = cbgDepartment.CheckedValue
            Dim ItemArr As ArrayList = cbgItem.CheckedValue
            Dim CategoryArr As ArrayList = cbgCategory.CheckedValue
            Dim SubCategoryArr As ArrayList = cbgSubCategroy.CheckedValue
            Dim Location As ArrayList = cbgLocation.CheckedValue
            Dim vehicle As ArrayList = cbgVehicle.CheckedValue
            Dim machine As ArrayList = cbgMachine.CheckedValue
            ''richa 20/02/2015
            Dim CostCenter As ArrayList = cbgCostCenter.CheckedValue

            Dim Dept As String = ""
            Dim MachNo As String = ""
            Dim vehicleNo As String = ""
            Dim item As String = ""
            Dim location1 As String = ""
            Dim itemcategory As String = ""
            ''richa 20/02/2015
            Dim costcenter1 As String = String.Empty
            ' Dim itemsubcategory As String

            Dim StrDept As String = ""
            Dim StrMachNo As String = ""
            Dim StrvehicleNo As String = ""
            Dim Stritem As String = ""
            Dim Strlocation As String = ""
            Dim Stritemcategory As String = ""
            Dim Stritemsubcategory As String = ""
            ''richa 20/02/2015
            Dim strcostcenter As String = String.Empty
            If chkCategorySelect.IsChecked = True AndAlso cbgCategory.CheckedValue.Count > 0 Then
                itemcategory = "'" + clsCommon.GetMulcallString(cbgCategory.CheckedValue) + "'"
                Stritemcategory = itemcategory.Replace("'", "")
            End If

            If cbgDepartment.CheckedValue.Count > 0 Then
                Dept = "'" + clsCommon.GetMulcallString(DepartmentArr) + "'"
                StrDept = Dept.Replace("'", "")
            End If

            'If cbgItem.CheckedValue.Count > 0 Then
            '    item = "'" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + "'"
            '    Stritem = item.Replace("'", "")
            'End If

            If cbgSubCategroy.CheckedValue.Count > 0 Then
                itemcategory = "'" + clsCommon.GetMulcallString(cbgSubCategroy.CheckedValue) + "'"
                Stritemcategory = itemcategory.Replace("'", "")
            End If

            If cbgMachine.CheckedValue.Count > 0 Then
                MachNo = "'" + clsCommon.GetMulcallString(machine) + "'"
                StrMachNo = MachNo.Replace("'", "")
            End If

            If cbgVehicle.CheckedValue.Count > 0 Then
                vehicleNo = "'" + clsCommon.GetMulcallString(vehicle) + "'"
                StrvehicleNo = vehicleNo.Replace("'", "")
            End If
            'If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
            '    location1 = "'" + clsCommon.GetMulcallString(Location) + "'"
            '    Strlocation = location1.Replace("'", "")

            'End If
            ' ''richa 20/02/2015
            'If chkCostCenterSelect.IsChecked = True AndAlso cbgCostCenter.CheckedValue.Count > 0 Then
            '    costcenter1 = "'" + clsCommon.GetMulcallString(CostCenter) + "'"
            '    strcostcenter = costcenter1.Replace("'", "")

            'End If

            Dim Address As String
            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 1 Then
                Address = "(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER .State_Name)> 0 then ',' else '' end  +(TSPL_TDS_STATE_MASTER .State_Name )) from tspl_location_master LEFT OUTER JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER .State =TSPL_TDS_STATE_MASTER .State_Code where Location_Code =xxx1 .From_Location )"
            Else
                Address = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "
            End If
            Dim UpperQry1 As String = ""
            Dim UpperQry2 As String = ""
            Dim strInnerQry As String = ""
            Dim LowerQry1 As String = ""
            Dim LowerQry2 As String = ""
            If chkDocWise.Checked Then
                UpperQry1 = "select '" + fromdate + "'as FromDate,'" + Todate + "'as ToDate, Doc_No, convert(varchar,DocumentDate,103) as DocumentDate , Comment,xxx1.[Item Category Code],xxx1. [Item Category Name],xxx1. [item Cagetory Values], xxx1. [item Cagetory Values Name], xxx1.Item_Code, " & _
                       " xxx1 .item_desc,xxx1.From_Location as Location,(Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code=xxx1.From_Location) as [Location Desc],xxx1.issued_qty,xxx1.CostCode as [Cost Center Code] ,xxx1.CostCenName as [Cost Center Desc] ,xxx1.[Hirerachy Code],xxx1.[Hirerachy Name],GL_account,[GL Account Desc],[Consumption Account],[Consumption Ac Desc],[Inventory Account],[Inventory AC Desc],xxx1.issued_Value ,xxx1.Return_qty ,xxx1.Return_Value ,xxx1.Net_qty," & _
                       " xxx1.Net_Value ,xxx1 .unit_code,xxx1.VehicleId ,xxx1 .Vehiclename ,TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img as Image1,TSPL_COMPANY_MASTER.Logo_Img2 as Image2," & _
                       " " + Address + " as address,Unit_Cost " & _
                       " from( "
                UpperQry2 = "select (xxx.Doc_No) as Doc_No, (xxx.Comment) AS Comment, xxx.item_code,(xxx.item_desc)as item_desc," & _
                  "  xxx.[Item Category Code],xxx. [Item Category Name],xxx. [item Cagetory Values], xxx. [item Cagetory Values Name], " & _
                    " (xxx.issued_qty * case when XXX.Doc_Type ='Issue' then 1 else 0 end)as issued_qty, "

                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                    UpperQry2 += " case when (XXX.Doc_Type) ='Issue' then (xxx.Value) else 0 end as issued_Value, "
                Else
                    UpperQry2 += " case when (XXX.Doc_Type) ='Issue' then (xxx.Value) else 0 end as issued_Value, "
                End If

                UpperQry2 += " (xxx.issued_qty * case when XXX.Doc_Type ='Return' then 1 else 0 end)as Return_qty," & _
                    " case when (XXX.Doc_Type) ='Return' then (xxx.Value) else 0 end as Return_Value," & _
                    " (xxx.issued_qty * case when XXX.Doc_Type ='Issue' then 1 else -1 end)as Net_qty," & _
                    "  (xxx.Value) * case when (XXX.Doc_Type) ='Issue' then 1 else -1 end as Net_Value," & _
                    " (xxx.unit_code)as unit_code,(xxx.Doc_type)as DocType,(xxx.comp_code )as Comp_code ," & _
                    " (xxx.Doc_Date)as DocumentDate,(xxx.dept)as DeptCode,(xxx.VehicleId )as VehicleId,(xxx.Vehiclename) as Vehiclename,(xxx.machineID )as machineID,(xxx.From_Location)as From_Location"

                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                    UpperQry2 += " ,(xxx.Cost_Code) as CostCode,(xxx.Cost_name ) as CostCenName,(Unit_Cost) as Unit_Cost,(xxx.[Hirerachy Code]) as [Hirerachy Code],(xxx.[Hirerachy Name]) as [Hirerachy Name] ,(XXX.GL_account) AS GL_account,([GL Account Desc]) as [GL Account Desc],[Consumption Account],[Consumption Ac Desc],[Inventory Account],[Inventory AC Desc] from ( "
                Else
                    UpperQry2 += " ,(xxx.Cost_Code) as CostCode,(xxx.Cost_name ) as CostCenName,(Unit_Cost) as Unit_Cost,(xxx.[Hirerachy Code]) as [Hirerachy Code],(xxx.[Hirerachy Name]) as [Hirerachy Name] ,(XXX.GL_account) AS GL_account,([GL Account Desc]) as [GL Account Desc],[Consumption Account],[Consumption Ac Desc],[Inventory Account],[Inventory AC Desc] from ( "
                    'UpperQry2 += " ,Max(xxx.Cost_Code) as CostCode,Max(xxx.Cost_name ) as CostCenName,max(Unit_Cost) as Unit_Cost,max(xxx.[Hirerachy Code]) as [Hirerachy Code],max(xxx.[Hirerachy Name]) as [Hirerachy Name] ,MAX(XXX.GL_account) AS GL_account,max([GL Account Desc]) as [GL Account Desc] from ( "
                End If

            Else
                UpperQry1 = "select '" + fromdate + "'as FromDate,'" + Todate + "'as ToDate,'" + StrDept + "'as StrDept,'" + StrMachNo + "'as StrMachNo,'" + StrvehicleNo + "'as StrvehicleNo,'" + Stritem + "'as Stritem,'" + Strlocation + "'as Strlocation,'" + Stritemcategory + "'as Stritemcategory,'" + Stritemsubcategory + "'as Stritemsubcategory,xxx1.item_category ,xxx1.Category_Name ,xxx1.Sub_Category_Code ,xxx1.Description,xxx1.[Item Category Code],xxx1. [Item Category Name],xxx1. [item Cagetory Values], xxx1. [item Cagetory Values Name] ,xxx1.Item_Code, " & _
                       " xxx1 .item_desc,xxx1.From_Location as Location,(Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code=xxx1.From_Location) as [Location Desc],xxx1.CostCode as [Cost Center Code] ,xxx1.CostCenName as [Cost Center Desc], xxx1.[Hirerachy Code],xxx1.[Hirerachy Name],GL_account,[GL Account Desc],[Consumption Account],[Consumption Ac Desc],[Inventory Account],[Inventory AC Desc],xxx1.issued_qty ,xxx1.issued_Value ,xxx1.Return_qty ,xxx1.Return_Value ,xxx1.Net_qty," & _
                       " xxx1.Net_Value ,xxx1 .unit_code,xxx1.VehicleId ,xxx1 .Vehiclename ,TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img as Image1,TSPL_COMPANY_MASTER.Logo_Img2 as Image2," & _
                       " " + Address + " as address,Unit_Cost " & _
                       " from( "
                UpperQry2 = "select xxx.item_category ,max(xxx.Category_Name)as Category_Name ,xxx.Sub_Category_Code," & _
                  "  xxx.[Item Category Code],xxx. [Item Category Name],xxx. [item Cagetory Values], xxx. [item Cagetory Values Name] ," & _
                   " max(xxx.Description)as Description, xxx.item_code,max(xxx.item_desc)as item_desc," & _
                    " sum(xxx.issued_qty) * case when XXX.Doc_Type ='Issue' then 1 else 0 end as issued_qty, "

                UpperQry2 += "case when (XXX.Doc_Type) ='Issue' then sum(xxx.Value) else 0 end as issued_Value, "



                UpperQry2 += " sum(xxx.issued_qty) * case when XXX.Doc_Type ='Return' then 1 else 0 end as Return_qty," & _
                    "case when (XXX.Doc_Type) ='Return' then sum(xxx.Value) else 0 end as Return_Value," & _
                    " sum(xxx.issued_qty) * case when XXX.Doc_Type ='Issue' then 1 else -1 end as Net_qty," & _
                    "  sum(xxx.Value) * case when XXX.Doc_Type ='Issue' then 1 else -1 end as Net_Value," & _
                    " max(xxx.unit_code)as unit_code,max(xxx.Doc_type)as DocType,max(xxx.comp_code )as Comp_code ," & _
                    " max(xxx.Doc_Date)as DocumentDate,MAX(xxx.dept)as DeptCode,MAX(xxx.VehicleId )as VehicleId,MAX(xxx.Vehiclename) as Vehiclename,MAX(xxx.machineID )as machineID,Max(xxx.From_Location)as From_Location "

                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                    UpperQry2 += " ,(xxx.Cost_Code) as CostCode,Max(xxx.Cost_name ) as CostCenName,max(Unit_Cost) as Unit_Cost,max(xxx.[Hirerachy Code]) as [Hirerachy Code],max(xxx.[Hirerachy Name]) as [Hirerachy Name] ,MAX(XXX.GL_account) AS GL_account,max([GL Account Desc]) as [GL Account Desc],max([Consumption Account]) as [Consumption Account],max([Inventory Account]) as [Inventory Account],max([Inventory AC Desc]) as [Inventory AC Desc],max([Consumption Ac Desc]) as [Consumption Ac Desc]  from ( "
                Else
                    UpperQry2 += ",Max(xxx.Cost_Code) as CostCode,Max(xxx.Cost_name ) as CostCenName,max(Unit_Cost) as Unit_Cost,max(xxx.[Hirerachy Code]) as [Hirerachy Code],max(xxx.[Hirerachy Name]) as [Hirerachy Name] ,MAX(XXX.GL_account) AS GL_account,max([GL Account Desc]) as [GL Account Desc],max([Consumption Account]) as [Consumption Account],max([Inventory Account]) as [Inventory Account],max([Inventory AC Desc]) as [Inventory AC Desc],max([Consumption Ac Desc]) as [Consumption Ac Desc]   from ( "
                End If

            End If
            ''richa 20/02/2015
            Dim strDocType As String = String.Empty
            If clsCommon.CompairString(ddlRptType.SelectedIndex, "1") = CompairStringResult.Equal Then
                strDocType = "'Issue','Return'"
            Else
                strDocType = "'Issue'"
            End If

            ''--------------
            strInnerQry = " select issuehd.Doc_No, issuehd.Comment, TSPL_ITEM_MASTER .item_category,TSPL_Item_Category.Category_Name," & _
                    " TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code, TSPL_ITEM_SUB_CATEGORY.Description," & _
                    " TSPL_ITEM_MASTER_CATEGORY.item_category_code as [Item Category Code],TSPL_ITEM_CATEGORY_LEVEL.description as [Item Category Name],TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values as [item Cagetory Values], TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as [item Cagetory Values Name], " & _
                    " issuertn.Item_Code, issuertn.Item_Desc, issuertn.Issued_Qty, issuertn.Unit_code, " & _
                    " issuehd.Doc_Type, issuehd.Comp_code, issuehd.Doc_Date, issuehd.dept, " & _
                    " issuertn.Amount  as Value,issuertn.Unit_Cost, " & _
                    " issuehd .Vehicle_Id as VehicleId ,(TSPL_GL_SEGMENT_CODE .Description )as Vehiclename,issuehd .Machine_Id as machineID,case when issuehd.Doc_Type='Issue' then isnull(issuehd.From_Location,'') else isnull(issuehd.To_Location,'') end as From_Location,issuertn.Cost_Centre_Code AS Cost_Code ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name  AS Cost_name " & _
                    " ,TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code as [Hirerachy Code],TSPL_HIRERACHY_LEVEL_MASTER.Description as [Hirerachy Name],issuertn.GL_account,TSPL_GL_ACCOUNTS.Description as [GL Account Desc],issuertn.Consumption_Ac as [Consumption Account],issuertn.inventory_Ac as [Inventory Account],GL_inventory.Description as [Inventory AC Desc],GL_Consumption.Description as [Consumption Ac Desc]  " & _
                    " from TSPL_IssueReturn_DETAIL as issuertn  " & _
                    " left outer join TSPL_IssueReturn_HEAD as issuehd on issuertn .Doc_No = issuehd .Doc_No " & _
                    " left outer  join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code = issuertn .Item_Code " & _
                    " left outer join  TSPL_ITEM_SUB_CATEGORY   on TSPL_ITEM_SUB_CATEGORY .Category_Code = TSPL_ITEM_MASTER .item_category and tspl_item_sub_category.Sub_Category_Code = TSPL_ITEM_MASTER .sub_item_category " & _
                   " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " & _
                    " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" & _
                    " left join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.Item_Category_Code=TSPL_ITEM_CATEGORY_LEVEL_VALUES.Item_Category_Code" & _
                    " LEFT outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code=TSPL_ITEM_MASTER .item_category left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE .Segment_code =issuehd  .Vehicle_Id and TSPL_GL_SEGMENT_CODE.Seg_No  =2 " & _
                    " Left outer join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL .Cost_Center_Fin_Code=issuertn.Cost_Centre_Code  " & _
                    " left outer join TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code=issuertn.Hirerachy_Code " & _
                    "   Left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=issuertn.GL_account " & _
                    "   Left outer join TSPL_GL_ACCOUNTS as GL_Consumption on GL_Consumption.Account_Code=issuertn.Consumption_Ac " & _
                    "   Left outer join TSPL_GL_ACCOUNTS  as GL_inventory on GL_inventory.Account_Code=issuertn.inventory_Ac " & _
                    " where Doc_Type in (" & strDocType & ") and issuehd.Status =1  and Convert(Date,issuehd .Doc_Date,103) >=Convert(Date,'" & dtpFromdate1.Value.Date & "',103) and Convert(Date,issuehd .Doc_Date,103) <=Convert(Date,'" & dtpToDate.Value.Date & "',103)  "
            'If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
            '    Return
            'ElseIf cbgLocation.CheckedValue.Count > 0 Then
            '    '' Removed location_segmentCode by abhishek as on 19june 2012 said by amit sir
            '    strInnerQry += " and issuehd. From_Location  in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
            'End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strInnerQry += " and issuehd. From_Location  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
            End If
            ''richa 20/02/2015
            'If chkCostCenterSelect.IsChecked = True AndAlso cbgCostCenter.CheckedValue.Count = 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select atleast one Cost Center")
            '    Return
            'ElseIf cbgCostCenter.CheckedValue.Count > 0 Then
            '    strInnerQry += " and issuertn.Cost_Centre_Code   in  (" + clsCommon.GetMulcallString(cbgCostCenter.CheckedValue) + ")"
            'End If
            If txtHirerachyCode.arrValueMember IsNot Nothing AndAlso txtHirerachyCode.arrValueMember.Count > 0 Then
                strInnerQry += " and issuertn.Hirerachy_Code  in  (" + clsCommon.GetMulcallString(txtHirerachyCode.arrValueMember) + ") "
            End If
            ''----------
            If chkDepartmentSelect.IsChecked = True AndAlso cbgDepartment.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Department", Me.Text)
                Return
            ElseIf cbgDepartment.CheckedValue.Count > 0 Then
                strInnerQry += " and issuehd .Dept in (" + clsCommon.GetMulcallString(DepartmentArr) + ")"
            End If
            'If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count = 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select atleast one ItemCode")
            '    Return
            'ElseIf cbgItem.CheckedValue.Count > 0 Then
            '    strInnerQry += " and issuertn.Item_Code in (" + clsCommon.GetMulcallString(ItemArr) + ")"
            'End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                strInnerQry += " and issuertn.Item_Code in  (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") "
            End If
            If chkCategorySelect.IsChecked = True AndAlso cbgCategory.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one ItemCode", Me.Text)
                Return
            ElseIf cbgCategory.CheckedValue.Count > 0 Then
                strInnerQry += " and TSPL_ITEM_MASTER .item_category in (" + clsCommon.GetMulcallString(CategoryArr) + ")"
            End If

            If chkSubCategroySelect.IsChecked = True AndAlso cbgSubCategroy.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one SubCategory", Me.Text)
                Return
            ElseIf cbgSubCategroy.CheckedValue.Count > 0 Then
                strInnerQry += " and TSPL_ITEM_SUB_CATEGORY .Sub_Category_Code in (" + clsCommon.GetMulcallString(SubCategoryArr) + ")"
            End If

            If chkMachineSelect.IsChecked = True AndAlso cbgMachine.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Machine", Me.Text)
                Return
            ElseIf cbgMachine.CheckedValue.Count > 0 Then
                strInnerQry += " and issuehd.Machine_Id in (" + clsCommon.GetMulcallString(machine) + ")"
            End If

            If chkDocWise.Checked Then
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                    LowerQry1 += ")xxx "
                    'LowerQry1 += ")xxx group by xxx.Item_Code,xxx.Unit_code,xxx.[Item Category Code],xxx. [Item Category Name],xxx. [item Cagetory Values], xxx. [item Cagetory Values Name] ,xxx.Cost_Code "
                Else
                    LowerQry1 += ")xxx "
                    ' LowerQry1 += ")xxx group by xxx.Item_Code,xxx.Unit_code,xxx.[Item Category Code],xxx. [Item Category Name],xxx. [item Cagetory Values], xxx. [item Cagetory Values Name]  "
                End If

            Else
                LowerQry1 += ")xxx group by xxx.Doc_Type,xxx.item_category,xxx.Sub_Category_Code  ,xxx .Item_Code,xxx.Unit_code,xxx.[Item Category Code],xxx. [Item Category Name],xxx. [item Cagetory Values], xxx. [item Cagetory Values Name]  "

                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                    LowerQry1 += ",xxx.Cost_Code "
                End If

            End If

            LowerQry2 = " )xxx1 inner join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code = xxx1.Comp_code "
            If chkVehicleWise.Checked = True AndAlso chkVehicleSelect.IsChecked = True AndAlso cbgVehicle.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Vehicle", Me.Text)
                Return
            ElseIf chkVehicleWise.Checked = True AndAlso cbgVehicle.CheckedValue.Count > 0 Then
                LowerQry2 += " where xxx1.VehicleId in (" + clsCommon.GetMulcallString(vehicle) + ")"
            End If
            qry = UpperQry1 + UpperQry2 + strInnerQry + LowerQry1 + LowerQry2
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
            Else
                If dt IsNot Nothing And dt.Rows.Count > 0 Then
                    gv.DataSource = Nothing
                    gv.Rows.Clear()
                    gv.Columns.Clear()
                    gv.DataSource = dt
                    gridformat()
                    ReStoreGridLayout()
                    RadPageView1.SelectedPage = RadPageViewPage2
                End If
                'dt = clsDBFuncationality.GetDataTable(qry)
                If isprint Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    If chkDocWise.Checked Then
                        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptIssReturn_DocWiseSummary", "Issue/Return Document Wise Summary")
                    ElseIf ddlRptType.SelectedIndex = 0 AndAlso chkVehicleWise.Checked = False Then
                        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptNetIssue", "Issue Item Wise Summary")
                    ElseIf ddlRptType.SelectedIndex = 1 AndAlso chkVehicleWise.Checked = False Then
                        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "IssueOrReturnItemWiseSummary", "Issue Or Return Item Wise Summary")
                    ElseIf chkVehicleWise.Checked = True Then
                        If ddlRptType.SelectedIndex = 1 Then
                            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "IssueOrReturnVehicleWiseItemSummary", " Issue Or Retrun Vehicle Wise Item Summery ")
                        ElseIf ddlRptType.SelectedIndex = 0 Then
                            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptVehicleWiseIssue", " Issue Or Retrun Vehicle Wise Issue ")
                        End If
                    End If
                    frmCRV = Nothing
                End If
            End If

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Public Sub gridformat()
        Try
            gv.AllowAddNewRow = False
            If chkDocWise.Checked Then
                For ii As Integer = 0 To gv.Columns.Count - 1
                    gv.Columns(ii).ReadOnly = True
                    gv.Columns(ii).IsVisible = False

                Next

                gv.Columns("Doc_No").IsVisible = True
                gv.Columns("Doc_No").HeaderText = "Document No"
                gv.Columns("Doc_No").Width = 100

                gv.Columns("DocumentDate").IsVisible = True
                gv.Columns("DocumentDate").HeaderText = "Document Date"
                gv.Columns("DocumentDate").Width = 100

                gv.Columns("Comment").IsVisible = True
                gv.Columns("Comment").Width = 100

                gv.Columns("Item_Code").IsVisible = True
                gv.Columns("Item_Code").HeaderText = "Item Code"
                gv.Columns("Item_Code").Width = 100

                gv.Columns("item_desc").IsVisible = True
                gv.Columns("item_desc").HeaderText = "Description"
                gv.Columns("item_desc").Width = 100
                ''richa 20/02/2014
                gv.Columns("Location").IsVisible = True
                gv.Columns("Location").HeaderText = "Location"
                gv.Columns("Location").Width = 100

                gv.Columns("Location Desc").IsVisible = True
                gv.Columns("Location Desc").HeaderText = "Location Desc"
                gv.Columns("Location Desc").Width = 100

                gv.Columns("Cost Center Code").IsVisible = True
                gv.Columns("Cost Center Code").HeaderText = "Cost Center Code"
                gv.Columns("Cost Center Code").Width = 100

                gv.Columns("Cost Center Desc").IsVisible = True
                gv.Columns("Cost Center Desc").HeaderText = "Cost Center Desc"
                gv.Columns("Cost Center Desc").Width = 100

                gv.Columns("Hirerachy Code").IsVisible = True
                gv.Columns("Hirerachy Code").HeaderText = "Hirerachy Level Desc"
                gv.Columns("Hirerachy Code").Width = 100

                gv.Columns("Hirerachy Name").IsVisible = True
                gv.Columns("Hirerachy Name").HeaderText = "Hirerachy Level Desc"
                gv.Columns("Hirerachy Name").Width = 100

                gv.Columns("GL_account").IsVisible = True
                gv.Columns("GL_account").HeaderText = "Gl Account"
                gv.Columns("GL_account").Width = 100

                gv.Columns("GL Account Desc").IsVisible = True
                gv.Columns("GL Account Desc").HeaderText = "Gl Account Desc"
                gv.Columns("GL Account Desc").Width = 100

                gv.Columns("unit_code").IsVisible = True
                gv.Columns("unit_code").HeaderText = "Unit Code"
                gv.Columns("unit_code").Width = 50

                gv.Columns("issued_qty").IsVisible = True
                gv.Columns("issued_qty").HeaderText = "Issue Qty"
                gv.Columns("issued_qty").Width = 80

                gv.Columns("issued_Value").IsVisible = True
                gv.Columns("issued_Value").HeaderText = "Issue Value"
                gv.Columns("issued_Value").Width = 100

                gv.Columns("Return_qty").IsVisible = True
                gv.Columns("Return_qty").HeaderText = "Return Qty"
                gv.Columns("Return_qty").Width = 100

                gv.Columns("Return_Value").IsVisible = True
                gv.Columns("Return_Value").HeaderText = "Return value"
                gv.Columns("Return_Value").Width = 100

                gv.Columns("Consumption Account").IsVisible = True
                gv.Columns("Consumption Account").HeaderText = "Consumption Account"
                gv.Columns("Consumption Account").Width = 100

                gv.Columns("Consumption Ac Desc").IsVisible = True
                gv.Columns("Consumption Ac Desc").HeaderText = "Consumption Account Desc"
                gv.Columns("Consumption Ac Desc").Width = 100

                gv.Columns("Inventory Account").IsVisible = True
                gv.Columns("Inventory Account").HeaderText = "Inventory Account"
                gv.Columns("Inventory Account").Width = 100

                gv.Columns("Inventory AC Desc").IsVisible = True
                gv.Columns("Inventory AC Desc").HeaderText = "Inventory Account Desc"
                gv.Columns("Inventory AC Desc").Width = 100


                If clsCommon.CompairString(ddlRptType.Text, "Net Issue") = CompairStringResult.Equal AndAlso chkDocWise.Checked = True Then
                Else
                    gv.Columns("Net_qty").IsVisible = True
                    gv.Columns("Net_qty").HeaderText = "Net Qty"

                    gv.Columns("Net_Value").IsVisible = True
                    gv.Columns("Net_Value").HeaderText = "Net value"
                End If

            Else

                For i As Integer = 0 To gv.Columns.Count - 1
                    gv.Columns(i).ReadOnly = True
                    gv.Columns(i).BestFit()
                    gv.Columns(i).IsVisible = True
                    If clsCommon.CompairString("ITF_CODE", gv.Columns(i).HeaderText) = CompairStringResult.Equal Then
                        gv.Columns(i).HeaderText = "ITF Code"
                    Else
                        gv.Columns(i).HeaderText = Replace(gv.Columns(i).HeaderText, "_", " ")
                        'Gv1.Columns(i).HeaderText = StrConv(Gv1.Columns(i).HeaderText, VbStrConv.ProperCase)
                    End If
                Next
                gv.Columns("StrDept").IsVisible = False
                gv.Columns("StrDept").Width = 100
                gv.Columns("StrDept").HeaderText = "StrDept"

                gv.Columns("StrMachNo").IsVisible = False
                gv.Columns("StrMachNo").Width = 100
                gv.Columns("StrMachNo").HeaderText = "StrMachNo"

                gv.Columns("StrvehicleNo").IsVisible = False
                gv.Columns("StrvehicleNo").Width = 200
                gv.Columns("StrvehicleNo").HeaderText = "StrvehicleNo"

                gv.Columns("Stritem").IsVisible = False
                gv.Columns("Stritem").Width = 100
                gv.Columns("Stritem").HeaderText = "Stritem"

                gv.Columns("Strlocation").IsVisible = False
                gv.Columns("Strlocation").Width = 100
                gv.Columns("Strlocation").HeaderText = "Strlocation"

                gv.Columns("StrItemCategory").IsVisible = False
                gv.Columns("StrItemCategory").Width = 200
                gv.Columns("StrItemCategory").HeaderText = "StrItemCategory"

                gv.Columns("StrItemSubcategory").IsVisible = False
                gv.Columns("StrItemSubcategory").Width = 100
                gv.Columns("StrItemSubcategory").HeaderText = "StrItemSubcategory"
                ''richa 02/03/2015
                gv.Columns("Category_Name").IsVisible = False
                gv.Columns("item_category").IsVisible = False
                gv.Columns("Sub_Category_Code").IsVisible = False
                gv.Columns("Description").IsVisible = False
                gv.Columns("VehicleId").IsVisible = False
                gv.Columns("Vehiclename").IsVisible = False

                '============Preeti Gupta 20/04/2015==========
                gv.Columns("compname").IsVisible = False
                gv.Columns("Image1").IsVisible = False
                gv.Columns("Image2").IsVisible = False
                gv.Columns("address").IsVisible = False
                gv.Columns("fromdate").IsVisible = False
                gv.Columns("Todate").IsVisible = False

                ''----------------------
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'PrintData1()
        isprint = True
        If ddlRptType.SelectedIndex = 1 AndAlso chkDocWise.Checked = True Then
            PrintDatafornetissue()
        Else
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                PrintData(objCommonVar.CurrentCompanyCode, "chkDocWise")
            Else
                PrintData()
            End If
        End If
        isprint = False
    End Sub

    '=============================Preeti Gupta=========================
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptIssueReturnHirerachyWise & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpFromdate1.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
                If txtHirerachyCode.arrDispalyMember IsNot Nothing AndAlso txtHirerachyCode.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Cost Center : " + clsCommon.GetMulcallStringWithComma(txtHirerachyCode.arrDispalyMember))
                End If

                If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                End If
                If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                    arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
                End If
         
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToExcelGrid("Issue Return Report", gv, arrHeader, Me.Text)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Issue Return Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Sub PrintData1(ByVal exporter As EnumExportTo)
        Dim Dept As String = ""
        Dim MachNo As String = ""
        Dim vehicleNo As String = ""
        Dim item As String = ""
        ' Dim location1 As String
        Dim itemcategory As String = ""
        'Dim itemsubcategory As String

        Dim StrDept As String = ""
        Dim StrMachNo As String = ""
        Dim StrvehicleNo As String = ""
        Dim Stritem As String = ""
        Dim Strlocation As String = ""
        Dim Stritemcategory As String = ""
        Dim Stritemsubcategory As String = ""

        ' If chkCostCentreReport.Checked = True Then
        Dim fromdate As String = clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy")
        Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
        Dim DepartmentArr As ArrayList = cbgDepartment.CheckedValue
        Dim vehicle As ArrayList = cbgVehicle.CheckedValue
        Dim machine As ArrayList = cbgMachine.CheckedValue
        Dim ItemArr As ArrayList = cbgItem.CheckedValue
        Dim SubCategoryArr As ArrayList = cbgSubCategroy.CheckedValue
        Dim qry As String = ""
        If (ddlRptType.Text = "Net Issue Return") Then

            qry = "select xxx1.item_category ,xxx1.Category_Name ,xxx1.Sub_Category_Code ,xxx1.Description ,xxx1.Item_Code, " & _
               " xxx1 .item_desc,xxx1.issued_qty ,xxx1.issued_Value ,xxx1.Return_qty ,xxx1.Return_Value ,xxx1.Net_qty," & _
               " xxx1.Net_Value ,xxx1 .unit_code,xxx1.VehicleId ,xxx1 .Vehiclename ,TSPL_COMPANY_MASTER.Comp_Name as compname" & _
" from(select xxx.item_category ,max(xxx.Category_Name)as Category_Name ,xxx.Sub_Category_Code," & _
               " max(xxx.Description)as Description,xxx.item_code,max(xxx.item_desc)as item_desc," & _
                " sum(xxx.issued_qty * case when XXX.Doc_Type ='Issue' then 1 else 0 end)as issued_qty," & _
                " sum(xxx.Value*xxx.issued_qty * case when XXX.Doc_Type ='Issue' then 1 else 0 end)as issued_Value," & _
                " sum(xxx.issued_qty * case when XXX.Doc_Type ='Return' then 1 else 0 end)as Return_qty," & _
                " sum(xxx.Value*xxx.issued_qty * case when XXX.Doc_Type ='Return' then 1 else 0 end)as Return_Value," & _
                " sum(xxx.issued_qty * case when XXX.Doc_Type ='Issue' then 1 else -1 end)as Net_qty," & _
                " sum(xxx.Value*xxx.issued_qty * case when XXX.Doc_Type ='Issue' then 1 else -1 end)as Net_Value," & _
                " max(xxx.unit_code)as unit_code,max(xxx.Doc_type)as DocType,max(xxx.comp_code )as Comp_code ," & _
                " max(xxx.Doc_Date)as DocumentDate,MAX(xxx.dept)as DeptCode,MAX(xxx.VehicleId )as VehicleId,MAX(xxx.Vehiclename) as Vehiclename,MAX(xxx.machineID )as machineID,Max(xxx.From_Location)as From_Location  from " & _
                " (select TSPL_ITEM_MASTER .item_category,TSPL_Item_Category.Category_Name," & _
                " TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code, TSPL_ITEM_SUB_CATEGORY.Description," & _
                " issuertn.Item_Code, issuertn.Item_Desc, issuertn.Issued_Qty, issuertn.Unit_code, " & _
                " issuehd.Doc_Type, issuehd.Comp_code, issuehd.Doc_Date, issuehd.dept, " & _
                " isnull((select top(1) case when isnull(Item_Qty,0)<>0 then isnull(Amount,0)/isnull(Item_Qty,0) else 0 end   from TSPL_ITEM_LOCATION_DETAILS where Item_Code=issuertn.Item_Code and Location_Code =issuehd .From_Location ),0)as Value,issuehd .Vehicle_Id as VehicleId ,(TSPL_GL_SEGMENT_CODE .Description )as Vehiclename,issuehd .Machine_Id as machineID,issuehd.From_Location " & _
                " from TSPL_IssueReturn_DETAIL as issuertn  " & _
                " left outer join TSPL_IssueReturn_HEAD as issuehd on issuertn .Doc_No = issuehd .Doc_No " & _
                " left outer  join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code = issuertn .Item_Code " & _
                " left outer join  TSPL_ITEM_SUB_CATEGORY   on TSPL_ITEM_SUB_CATEGORY .Category_Code = TSPL_ITEM_MASTER .item_category and tspl_item_sub_category.Sub_Category_Code = TSPL_ITEM_MASTER .sub_item_category " & _
                " LEFT outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code=TSPL_ITEM_MASTER .item_category left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE .Segment_code =issuehd  .Vehicle_Id and TSPL_GL_SEGMENT_CODE.Seg_No  =2 " & _
                " where Doc_Type in ('Issue','Return') and Convert(Date,issuehd .Doc_Date,103) >=Convert(Date,'" & dtpFromdate1.Value.Date & "',103) and Convert(Date,issuehd .Doc_Date,103) <=Convert(Date,'" & dtpToDate.Value.Date & "',103)  "




            'qry = " SELECT     issuehd.Doc_No, issuehd.Doc_Date,issuehd.Doc_Type, issuehd.Remarks, issuehd.Comment,  case when  issuehd.Status=0 then 'Pending' else 'Approved' end as Status, issuehd.Posting_Date, issuertn.Item_Code,   issuertn.Item_Desc, issuertn.Required_Qty,(select xxxx.Issued_Qty  from TSPL_IssueReturn_DETAIL  xxxx where xxxx.Doc_No=issuehd.Req_IssueNo and xxxx.Item_Code=issuertn .Item_Code  )as [Issued_Qty], issuertn.Issued_Qty as Returnqty,((select xxxx.Issued_Qty  from TSPL_IssueReturn_DETAIL  xxxx where xxxx.Doc_No=issuehd.Req_IssueNo and xxxx.Item_Code=issuertn .Item_Code  )- issuertn.Issued_Qty )as NetIssue,   issuertn.Unit_code, TSPL_COMPANY_MASTER.Comp_Name, (TSPL_COMPANY_MASTER.Add1+','+ TSPL_COMPANY_MASTER.Add2+ ','+  TSPL_COMPANY_MASTER.Add3) as Address,   loc1.Location_Desc as Fromlocation,loc2.Location_Desc as Tolocation, emp1.Emp_Name as IssuesTo,emp2.Emp_Name as RequestBy     FROM  TSPL_IssueReturn_HEAD as issuehd"
            'qry += " INNER JOIN TSPL_IssueReturn_DETAIL as issuertn  ON issuehd.Doc_No = issuertn.Doc_No"
            'qry += " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON issuehd.comp_code = TSPL_COMPANY_MASTER.Comp_Code"
            'qry += " LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp1 ON issuehd .Issue_To = emp1.EMP_CODE  "
            'qry += " LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp2 ON issuehd.Request_By = emp2.EMP_CODE    "
            'qry += " LEFT OUTER JOIN  TSPL_LOCATION_MASTER as loc1 ON issuehd.From_Location = loc1.Location_Code"
            'qry += " LEFT OUTER JOIN  TSPL_LOCATION_MASTER  as loc2 ON issuehd.To_Location = loc2.Location_Code where 2=2   "
            'qry += " and Convert(Date,issuehd .Doc_Date,103) >=Convert(Date,'" + dtpFromdate1.Value.Date + "',103) and Convert(Date,issuehd .Doc_Date,103) <=Convert(Date,'" + dtpToDate.Value.Date + "',103)"

        End If

        If (ddlRptType.Text = "Net Issue") Then

            qry = "select (case when len(xxx.DeptId )>0 then xxx.DeptId  when LEN(xxx.VehicleId )>0 then xxx.VehicleId when LEN(xxx.machineID )>0 then xxx.machineID end)as [Group]," & _
                                "  (case when len(xxx.DeptDesc  )>0 then xxx.DeptDesc   when LEN(xxx.Vehiclename  )>0 then xxx.Vehiclename  when LEN(xxx.MachineDesc  )>0 then xxx.MachineDesc  end)as [Name Of Gruop]," & _
                                " (xxx.Sub_Category_Code)as [Item Group] , (xxx.Description)as [Item Group Name],xxx.item_code as [Item] ,(xxx.item_desc)as [Item Name],(xxx.unit_code)as Unit, (xxx.issued_qty * case when XXX.Doc_Type ='Issue' then 1 else 0 end)as [Issue Qty]," & _
                                 " (xxx.Value*xxx.issued_qty * case when XXX.Doc_Type ='Issue' then 1 else 0 end)as [Issue Value], (xxx.issued_qty * case when XXX.Doc_Type ='Return' then 1 else 0 end)as [Return Qty]," & _
                                "  (xxx.Value*xxx.issued_qty * case when XXX.Doc_Type ='Return' then 1 else 0 end)as [Return Value], (xxx.issued_qty * case when XXX.Doc_Type ='Issue' then 1 else -1 end)as [Net Qty], " & _
                                 " (xxx.Value*xxx.issued_qty * case when XXX.Doc_Type ='Issue' then 1 else -1 end)as [Net Value] from  " & _
                                 "  (select  TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code, TSPL_ITEM_SUB_CATEGORY.Description, issuertn.Item_Code, issuertn.Item_Desc, issuertn.Issued_Qty, issuertn.Unit_code,issuehd.Doc_Type,isnull(issuertn .Item_Net_Amt ,0)as Value ,issuehd .Vehicle_Id as VehicleId ,(TSPL_GL_SEGMENT_CODE .Description )as Vehiclename,issuehd .Machine_Id as machineID,GlsegMach.Description as MachineDesc ,issuehd .Dept as DeptId,GlsegDepartId .Description as DeptDesc  from TSPL_IssueReturn_DETAIL as issuertn   left outer join TSPL_IssueReturn_HEAD as issuehd on issuertn .Doc_No = issuehd .Doc_No  left outer  join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code = issuertn .Item_Code  left outer join  TSPL_ITEM_SUB_CATEGORY   on TSPL_ITEM_SUB_CATEGORY .Category_Code = TSPL_ITEM_MASTER .item_category and tspl_item_sub_category.Sub_Category_Code = TSPL_ITEM_MASTER .sub_item_category  LEFT outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code=TSPL_ITEM_MASTER .item_category left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE .Segment_code =issuehd  .Vehicle_Id and TSPL_GL_SEGMENT_CODE.Seg_No  = 2 left outer join TSPL_GL_SEGMENT_CODE as GlsegMach on GlsegMach  .Segment_code =issuehd  .Machine_Id  and GlsegMach.Seg_No  = 5 left outer join TSPL_GL_SEGMENT_CODE as GlsegDepartId on GlsegDepartId.Segment_code =issuehd .Dept  and GlsegDepartId.Seg_No  =3 where Doc_Type in ('Issue','Return') and Convert(Date,issuehd .Doc_Date,103) >=Convert(Date,'" + dtpFromdate1.Value.Date + "',103) and Convert(Date,issuehd .Doc_Date,103) <=Convert(Date,'" + dtpToDate.Value.Date + "',103) "
        End If
        If chkDepartmentSelect.IsChecked = True AndAlso cbgDepartment.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Department", Me.Text)
            Return
        ElseIf cbgDepartment.CheckedValue.Count > 0 Then
            qry += " and issuehd .Dept in (" + clsCommon.GetMulcallString(DepartmentArr) + ")"
        End If
        'If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count = 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select atleast one ItemCode")
        '    Return
        'ElseIf cbgItem.CheckedValue.Count > 0 Then
        '    qry += " and issuertn.Item_Code in (" + clsCommon.GetMulcallString(ItemArr) + ")"
        'End If
        '====added by shivani
        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            qry += " and issuertn.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") "
        End If
        '========================
        If chkSubCategroySelect.IsChecked = True AndAlso cbgSubCategroy.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one SubCategory", Me.Text)
            Return
        ElseIf cbgSubCategroy.CheckedValue.Count > 0 Then

            qry += " and TSPL_ITEM_SUB_CATEGORY .Sub_Category_Code in (" + clsCommon.GetMulcallString(SubCategoryArr) + ")"
        End If
        If chkMachineSelect.IsChecked = True AndAlso cbgMachine.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Machine", Me.Text)
            Return
        ElseIf cbgMachine.CheckedValue.Count > 0 Then
            qry += " and issuehd.Machine_Id in (" + clsCommon.GetMulcallString(machine) + ")"
        End If
        If chkVehicleSelect.IsChecked = True AndAlso cbgVehicle.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Vahicle", Me.Text)
            Return
        ElseIf cbgVehicle.CheckedValue.Count > 0 Then
            qry += " and issuehd.Vehicle_Id in (" + clsCommon.GetMulcallString(vehicle) + ")"
        End If



        If cbgDepartment.CheckedValue.Count > 0 Then
            Dept = "'" + clsCommon.GetMulcallString(DepartmentArr) + "'"
            StrDept = Dept.Replace("'", "")
        End If

        'If cbgItem.CheckedValue.Count > 0 Then
        '    item = "'" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + "'"
        '    Stritem = item.Replace("'", "")
        'End If

        If cbgSubCategroy.CheckedValue.Count > 0 Then
            itemcategory = "'" + clsCommon.GetMulcallString(cbgSubCategroy.CheckedValue) + "'"
            Stritemcategory = itemcategory.Replace("'", "")
        End If

        If cbgMachine.CheckedValue.Count > 0 Then
            MachNo = "'" + clsCommon.GetMulcallString(machine) + "'"
            StrMachNo = MachNo.Replace("'", "")
        End If

        If cbgVehicle.CheckedValue.Count > 0 Then
            vehicleNo = "'" + clsCommon.GetMulcallString(vehicle) + "'"
            StrvehicleNo = vehicleNo.Replace("'", "")
        End If

        If (ddlRptType.Text = "Net Issue") Then

            qry += " ) xxx"
        End If
        If (ddlRptType.Text = "Net Issue Return") Then
            qry += ")xxx group by xxx.item_category,xxx.Sub_Category_Code  ,xxx .Item_Code,xxx.Unit_code )xxx1 inner join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code = xxx1  .Comp_code "

        End If
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(qry)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then

            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv


        End If
        Dim str As String = "IssurOrReturnItemWiseSummary Report"

        Dim arr As New List(Of String)()
        arr.Add("IssurOrReturnItemWiseSummary Report")
        arr.Add("  From Date:  " + fromdate + "  To Date: " + Todate + "   ")
        If Strlocation <> "" Then
            arr.Add(" Location:   " + Strlocation + "")
        End If
        If Stritem <> "" Then
            arr.Add(" Item:  " + Stritem + "")
        End If
        If Stritemcategory <> "" Then
            arr.Add(" Category:  " + Stritemcategory + "")
        End If
        If Stritemsubcategory <> "" Then
            arr.Add(" SubCategory:   " + Stritemsubcategory + "")
        End If
        If StrMachNo <> "" Then
            arr.Add(" MachineNo:  " + StrMachNo + "")
        End If

        If StrvehicleNo <> "" Then
            arr.Add(" VehicleNo:  " + StrvehicleNo + "")
        End If
        If StrDept <> "" Then
            arr.Add(" Department:   " + StrDept + "")
        End If
        ' clsCommon.MyExportToExcel(str, gv, arr, "IssurOrReturnItemWiseSummary Report")
        If exporter = EnumExportTo.Excel Then
            'clsCommon.MyExportToExcel(str, gv, arr, Me.Text)
            clsCommon.MyExportToExcel(str, gv, arr, "IssurOrReturnItemWiseSummary Report")
        Else
            clsCommon.MyExportToPDF(str, gv, arr, "IssurOrReturnItemWiseSummary", True)
        End If
        ' ExporttoMyExcel(qry, Me)

        'Else
        '    PrintData()
        'End If

    End Sub
    Public Sub FillGridView(ByVal sql As String, ByVal gv As RadGridView)
        Dim ds As New DataSet
        Dim bs As New BindingSource()
        ds = connectSql.RunSQLReturnDS(sql)
        bs.DataSource = ds.Tables(0)
        gv.DataSource = bs
    End Sub
    Public Function ExporttoMyExcel(ByVal sql As String, ByVal frm As RadForm) As Boolean
        Dim sfd As SaveFileDialog = New SaveFileDialog()
        Dim Fullpath As String
        Dim path As String
        sfd.FileName = "COST CENTER WISE ISSUE FROM STORES"
        sfd.Filter = "Excel Workbooks (*.xls;*.xlsx)|*.xls;*.xlsx"
        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            path = sfd.FileName
            Fullpath = path
        Else
            Return False
        End If


        If Not path.Equals(String.Empty) Then
            Dim gv As New RadGridView()
            Try
                gv.Name = "gTax"
                frm.Controls.Add(gv)
                FillGridView(sql, gv)
                If gv.Rows.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "There is no data for Show Excel Report.", Me.Text)
                    Return False
                End If
                Dim i As Integer = 0
                For i = 0 To gv.ColumnCount - 1
                    Dim grow As GridViewRowInfo = TryCast(gv.Rows(0), GridViewRowInfo)
                    If TypeOf grow.Cells(i).Value Is DateTime Then
                        Dim datecol As GridViewDateTimeColumn = TryCast(gv.Columns(i), GridViewDateTimeColumn)
                        datecol.ExcelExportType = DisplayFormatType.ShortDate
                    End If
                Next i
                Dim exporter As New ExportToExcelML(gv)
                AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
                exporter.ExportHierarchy = True
                exporter.SheetMaxRows = ExcelMaxRows._65536
                exporter.SheetName = frm.Text
                exporter.RunExport(Fullpath)

                frm.Controls.Remove(gv)
                '' Added By Abhishek For Show Excel Without save.
                Dim xlsApp As Microsoft.Office.Interop.Excel.Application
                Dim xlsWB As Microsoft.Office.Interop.Excel.Workbook
                xlsApp = New Microsoft.Office.Interop.Excel.Application
                xlsApp.Visible = True
                xlsWB = xlsApp.Workbooks.Open(Fullpath)
                Return True
            Catch ex As Exception
                frm.Controls.Remove(gv)
                common.clsCommon.MyMessageBoxShow("No Report Created.", "Export Error", MessageBoxButtons.OK)
                Return False
            End Try
        End If
    End Function
    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelML.ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewTableHeaderRowInfo) Then
            e.ExcelStyleElement.FontStyle.Bold = False
            e.ExcelStyleElement.FontStyle.Size = 8
            e.ExcelStyleElement.FontStyle.FontName = "Verdana"
        End If

        e.ExcelStyleElement.FontStyle.Bold = False
        e.ExcelStyleElement.FontStyle.Size = 8
        e.ExcelStyleElement.FontStyle.FontName = "Verdana"

    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkDeapartmentAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDeapartmentAll.ToggleStateChanged
        cbgDepartment.Enabled = Not chkDeapartmentAll.IsChecked
    End Sub
    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub
    Private Sub chkCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCategoryAll.ToggleStateChanged
        cbgCategory.Enabled = Not chkCategoryAll.IsChecked
    End Sub
    Private Sub chkSubCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSubCategoryAll.ToggleStateChanged
        cbgSubCategroy.Enabled = Not chkSubCategoryAll.IsChecked
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub
    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub
    Private Sub chkVehicleNoAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVehicleNoAll.ToggleStateChanged
        cbgVehicle.Enabled = False
    End Sub

    Private Sub chkVehicleSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVehicleSelect.ToggleStateChanged
        cbgVehicle.Enabled = True
    End Sub

    Private Sub chkmachineAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkmachineAll.ToggleStateChanged
        cbgMachine.Enabled = False
    End Sub

    Private Sub chkMachineSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMachineSelect.ToggleStateChanged
        cbgMachine.Enabled = True
    End Sub


    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Sub Reset()
        ddlRptType.SelectedIndex = 0
        chkDeapartmentAll.IsChecked = True
        'chkItemAll.IsChecked = True
        chkCategoryAll.IsChecked = True
        chkSubCategoryAll.IsChecked = True
        'chkLocationAll.IsChecked = True
        chkVehicleNoAll.IsChecked = True
        chkmachineAll.IsChecked = True
        ''richa 20/02/2015
        'chkCostCenterAll.IsChecked = True
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        DepartmentLoad()
        'ItemLoad()
        CategoryLoad()
        SubCategoryLoad()
        'LoadLocation()
        'LoadCostCenter()
        LoadVehicle()
        LoadMachine()
        grpbxDepartment.Enabled = False
        RadGroupBox2.Enabled = False
        chkVehicleWise.Checked = False
        RadGroupBox1.Enabled = False
        chkCostCentreReport.Checked = False
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.DataSource = Nothing

    End Sub
    Private Sub chkVehicleWise_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVehicleWise.ToggleStateChanged
        If chkVehicleWise.Checked = True Then
            RadGroupBox1.Enabled = True
            chkCostCentreReport.Enabled = False
            chkCostCentreReport.Checked = False
            chkVehicleSelect.Enabled = True
            chkDocWise.Checked = False
        Else
            RadGroupBox1.Enabled = False
            grpbxDepartment.Enabled = False
            RadGroupBox2.Enabled = False
            chkCostCentreReport.Enabled = True
        End If
    End Sub
    Private Sub chkCostCentreReport_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCostCentreReport.ToggleStateChanged
        If chkCostCentreReport.Checked = True Then
            grpbxDepartment.Enabled = True
            RadGroupBox2.Enabled = True
            RadGroupBox1.Enabled = True
            grpbxItemCategory.Enabled = False
            RadGroupBox13.Enabled = False
            chkVehicleWise.Enabled = False
        Else
            grpbxDepartment.Enabled = False
            RadGroupBox2.Enabled = False
            grpbxItemCategory.Enabled = True
            RadGroupBox13.Enabled = True
            chkVehicleWise.Enabled = True
            chkVehicleWise.Checked = False
            RadGroupBox1.Enabled = False
        End If
    End Sub

    'Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
    '    PrintData1(EnumExportTo.Excel)

    'End Sub

    'Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
    '    PrintData1(EnumExportTo.PDF)
    'End Sub

    Private Sub btnExcel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub chkDocWise_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDocWise.ToggleStateChanged
        If chkDocWise.Checked Then
            chkVehicleWise.Checked = False
        End If
    End Sub

    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        'If clsCommon.CompairString(e.Column.Name, "Doc_No") = CompairStringResult.Equal Then
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            If gv.Rows.Count > 0 Then
                Dim strDoc
                If chkDocWise.Checked = True Then
                    strDoc = gv.CurrentRow.Cells("Doc_No").Value
                    If strDoc IsNot Nothing AndAlso clsCommon.myLen(strDoc) > 0 Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnIssueReturn, strDoc)
                    End If
                End If
            End If
        End If
    End Sub
    ''richa 20/02/2015
    Private Sub chkCostCenterAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCostCenterAll.ToggleStateChanged
        cbgCostCenter.Enabled = False
    End Sub

    Private Sub chkCostCenterSelect_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCostCenterSelect.ToggleStateChanged
        cbgCostCenter.Enabled = True
    End Sub

    Private Sub BtnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGo.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(chkDocWise.Checked = True, "DOC", "")
        TemplateGridview = gv
        If clsCommon.CompairString(ddlRptType.Text, "Net Issue") = CompairStringResult.Equal AndAlso chkDocWise.Checked = True Then
            PrintDatafornetissue()
        Else
            PrintData()
        End If

    End Sub


    Sub PrintDatafornetissue()
        Try
            Dim qry As String
            Dim fromdate As String = clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy")
            Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
            Dim ItemArr As ArrayList = cbgItem.CheckedValue
            Dim Location As ArrayList = cbgLocation.CheckedValue
            Dim CostCenter As ArrayList = cbgCostCenter.CheckedValue


            Dim item As String = ""
            Dim location1 As String = ""

            Dim costcenter1 As String = String.Empty


            Dim StrDept As String = ""
            Dim StrMachNo As String = ""
            Dim StrvehicleNo As String = ""
            Dim Stritem As String = ""
            Dim Strlocation As String = ""
            Dim Stritemcategory As String = ""
            Dim Stritemsubcategory As String = ""
            ''richa 20/02/2015
            Dim strcostcenter As String = String.Empty


            'If cbgItem.CheckedValue.Count > 0 Then
            '    item = "'" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + "'"
            '    Stritem = item.Replace("'", "")
            'End If


            'If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
            '    location1 = "'" + clsCommon.GetMulcallString(Location) + "'"
            '    Strlocation = location1.Replace("'", "")

            'End If
            ' ''richa 20/02/2015
            'If chkCostCenterSelect.IsChecked = True AndAlso cbgCostCenter.CheckedValue.Count > 0 Then
            '    costcenter1 = "'" + clsCommon.GetMulcallString(CostCenter) + "'"
            '    strcostcenter = costcenter1.Replace("'", "")

            'End If

            'If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 1 Then
            '    Address = "(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER .State_Name)> 0 then ',' else '' end  +(TSPL_TDS_STATE_MASTER .State_Name )) from tspl_location_master LEFT OUTER JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER .State =TSPL_TDS_STATE_MASTER .State_Code where Location_Code =xxx1 .From_Location )"
            'Else
            '    Address = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "
            'End If

            Dim strInnerQry As String = ""


            'If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
            '    Return
            'ElseIf cbgLocation.CheckedValue.Count > 0 Then
            '    strInnerQry += " and issuehd. From_Location  in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
            'End If
            'If chkCostCenterSelect.IsChecked = True AndAlso cbgCostCenter.CheckedValue.Count = 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select atleast one Cost Center")
            '    Return
            'ElseIf cbgCostCenter.CheckedValue.Count > 0 Then
            '    strInnerQry += " and TSPL_HIRERACHY_LEVEL_MASTER.Description as [Hirerachy Name],issuertn.GL_account   AS Cost_Code  in  (" + clsCommon.GetMulcallString(cbgCostCenter.CheckedValue) + ")"
            'End If
            ' '''----------

            'If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count = 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select atleast one ItemCode")
            '    Return
            'ElseIf cbgItem.CheckedValue.Count > 0 Then
            '    strInnerQry += " and issuertn.Item_Code in (" + clsCommon.GetMulcallString(ItemArr) + ")"
            'End If
            '====added by shivani
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strInnerQry += "  and issuehd. From_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                strInnerQry += " and issuertn.Item_Code in(" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") "
            End If
            If txtHirerachyCode.arrValueMember IsNot Nothing AndAlso txtHirerachyCode.arrValueMember.Count > 0 Then
                strInnerQry += "  and issuertn.Hirerachy_Code  in (" + clsCommon.GetMulcallString(txtHirerachyCode.arrValueMember) + ") "
            End If
            '========================
            qry = "Select Final1.*, TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img as Image1,TSPL_COMPANY_MASTER.Logo_Img2 as Image2, (TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end)  as address  from (" & _
                " Select '" + fromdate + "'as FromDate,'" + Todate + "'as ToDate, (Doc_No) as Doc_No,max(Comment) as Comment , (Item_Code) as Item_Code,max(Item_Desc) as Item_Desc ," & _
                " sum(Issued_Qty)* case when max(Doc_Type) ='Issue' then 1 else 0 end as Issued_Qty,  " & _
" case when max(Doc_Type) ='Issue' then sum(Value) else 0 end as issued_Value," & _
" sum(ReturnQty)* case when max(Doc_Type) ='Return' then 1 else 0 end  as Return_qty," & _
 " case when max(Doc_Type) ='Return' then sum(Value) else 0 end as Return_Value," & _
" max(Unit_code) as Unit_code,max(Doc_Type) as Doc_Type,max(Final.Comp_code) as Comp_code," & _
" max(Doc_Date) as DocumentDate, max(dept) as dept,  Max(Value) as Value,Max(VehicleId) as VehicleId ,max(Vehiclename) as Vehiclename,Max(machineID) as machineID,Max(From_Location) as Location,Max(TSPL_LOCATION_MASTER.Location_Desc)  as  [Location Desc] "

            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                qry += " ,(Cost_Code) as [Cost Center Code]"
            Else
                qry += " ,MAx(Cost_Code) as [Cost Center Code]"
            End If

            qry += " ,MAx(Cost_name) as [Cost Center Desc] ,max(Unit_cost)as Unit_cost,max([Hirerachy Code]) as [Hirerachy Code],max([Hirerachy Name]) as [Hirerachy Name],MAX(GL_account) AS GL_account,max([GL Account Desc]) as [GL Account Desc] from " & _
" (select issuehd.Doc_No, issuehd.Comment, TSPL_ITEM_MASTER .item_category,TSPL_Item_Category.Category_Name, TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code, TSPL_ITEM_SUB_CATEGORY.Description, issuertn.Item_Code, issuertn.Item_Desc, issuertn.Issued_Qty,0 as ReturnQty," & _
" issuertn .Unit_code,  issuehd.Doc_Type, issuehd.Comp_code, issuehd.Doc_Date, issuehd.dept,  issuertn.Amount Value,issuertn.Unit_Cost,issuehd .Vehicle_Id as VehicleId ,(TSPL_GL_SEGMENT_CODE .Description )as Vehiclename,issuehd .Machine_Id as machineID,case when issuehd.Doc_Type='Issue' then isnull(issuehd.From_Location,'') else isnull(issuehd.To_Location,'') end as From_Location,issuertn.Cost_Centre_Code AS Cost_Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name  AS Cost_name,  TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code as [Hirerachy Code],TSPL_HIRERACHY_LEVEL_MASTER.Description as [Hirerachy Name],issuertn.GL_account,TSPL_GL_ACCOUNTS.Description as [GL Account Desc] " & _
"  from TSPL_IssueReturn_DETAIL as issuertn   left outer join TSPL_IssueReturn_HEAD as issuehd on issuertn .Doc_No = issuehd .Doc_No  left outer  join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code = issuertn .Item_Code  left outer join  TSPL_ITEM_SUB_CATEGORY   on TSPL_ITEM_SUB_CATEGORY .Category_Code = TSPL_ITEM_MASTER .item_category and tspl_item_sub_category.Sub_Category_Code = TSPL_ITEM_MASTER .sub_item_category  LEFT outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code=TSPL_ITEM_MASTER .item_category left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE .Segment_code =issuehd  .Vehicle_Id and TSPL_GL_SEGMENT_CODE.Seg_No  =2  Left outer join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL .Cost_Center_Fin_Code=issuertn.Cost_Centre_Code  left outer join TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code=issuertn.Hirerachy_Code " & _
" Left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=issuertn.GL_account where Doc_Type ='Issue' and issuehd.Status =1  and Convert(Date,issuehd .Doc_Date,103) >=Convert(Date,'" & dtpFromdate1.Value.Date & "',103) and Convert(Date,issuehd .Doc_Date,103) <=Convert(Date,'" & dtpToDate.Value.Date & "',103) " & strInnerQry & " "

            '"  Union All " 
            '" select issuehd.Doc_No , issuehd.Comment, TSPL_ITEM_MASTER .item_category,TSPL_Item_Category.Category_Name, TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code, TSPL_ITEM_SUB_CATEGORY.Description, issuertn.Item_Code, issuertn.Item_Desc,0 as Issued_Qty, issuertn.Issued_Qty  as ReturnQty, issuertn.Unit_code,  issuehd.Doc_Type, issuehd.Comp_code, issuehd.Doc_Date, issuehd.dept, issuertn.Amount Value,issuertn.Unit_Cost,issuehd .Vehicle_Id as VehicleId ,(TSPL_GL_SEGMENT_CODE .Description )as Vehiclename,issuehd .Machine_Id as machineID,case when issuehd.Doc_Type='Issue' then isnull(issuehd.From_Location,'') else isnull(issuehd.To_Location,'') end as From_Location,issuertn.Cost_Centre_Code AS Cost_Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name  AS Cost_name  , TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code as [Hirerachy Code],TSPL_HIRERACHY_LEVEL_MASTER.Description as [Hirerachy Name],issuertn.GL_account,TSPL_GL_ACCOUNTS.Description as [GL Account Desc]  " & _
            '" from TSPL_IssueReturn_DETAIL as issuertn   left outer join TSPL_IssueReturn_HEAD as issuehd on issuertn .Doc_No = issuehd .Doc_No  left outer  join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code = issuertn .Item_Code  left outer join  TSPL_ITEM_SUB_CATEGORY   on TSPL_ITEM_SUB_CATEGORY .Category_Code = TSPL_ITEM_MASTER .item_category and tspl_item_sub_category.Sub_Category_Code = TSPL_ITEM_MASTER .sub_item_category  LEFT outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code=TSPL_ITEM_MASTER .item_category left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE .Segment_code =issuehd  .Vehicle_Id and TSPL_GL_SEGMENT_CODE.Seg_No  =2  Left outer join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL .Cost_Center_Fin_Code=issuertn.Cost_Centre_Code  left outer join TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code=issuertn.Hirerachy_Code " & _
            '" Left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=issuertn.GL_account  where Doc_Type ='Return' and issuehd.Status =1  and Convert(Date,issuehd .Doc_Date,103) >=Convert(Date,'" & dtpFromdate1.Value.Date & "',103) and Convert(Date,issuehd .Doc_Date,103) <=Convert(Date,'" & dtpToDate.Value.Date & "',103) " & strInnerQry & " " & _
            qry += " ) Final Left Outer Join TSpl_Location_Master on Tspl_location_master.Location_Code =Final.From_Location  " & _
          " group by Final.Doc_No ,Final.Item_Code "

            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                qry += " , final.Cost_Code "
            Else
                qry += "  Having Sum(ReturnQty )>0  "
            End If

            qry += " ) Final1 Left Outer Join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code  =Final1.comp_code  "


            ''-----------------------


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found to Display", Me.Text)
            Else
                If dt IsNot Nothing And dt.Rows.Count > 0 Then
                    gv.DataSource = Nothing
                    gv.Rows.Clear()
                    gv.Columns.Clear()
                    gv.DataSource = dt
                    gridformat()
                    ReStoreGridLayout()
                    RadPageView1.SelectedPage = RadPageViewPage2
                End If
                If isprint Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    If chkDocWise.Checked Then
                        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptIssReturn_DocWiseSummary", "Issue/Return Document Wise Summary")
                    ElseIf ddlRptType.SelectedIndex = 0 AndAlso chkVehicleWise.Checked = False Then
                        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptNetIssue", "Issue Item Wise Summary")
                    ElseIf ddlRptType.SelectedIndex = 1 AndAlso chkVehicleWise.Checked = False Then
                        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "IssueOrReturnItemWiseSummary", "Issue Or Return Item Wise Summary")
                    ElseIf chkVehicleWise.Checked = True Then
                        If ddlRptType.SelectedIndex = 1 Then
                            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "IssueOrReturnVehicleWiseItemSummary", " Issue Or Retrun Vehicle Wise Item Summery ")
                        ElseIf ddlRptType.SelectedIndex = 0 Then
                            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptVehicleWiseIssue", " Issue Or Retrun Vehicle Wise Issue ")
                        End If
                    End If
                    frmCRV = Nothing
                End If
            End If

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Public Sub ItemLoad()
        qry = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER  "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Code"
    End Sub
    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "

        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
    End Sub
    ''richa 20/02/2015
    Public Sub LoadCostCenter()
        qry = "select Cost_Code as Code,Cost_name as Name from  TSPL_COST_CENTRE_FINANCIAL"
        cbgCostCenter.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCostCenter.ValueMember = "Code"
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = "select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical'   "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = "select Item_Code as Code,Item_Desc as Name  from TSPL_ITEM_Master"
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub

    Private Sub txtHirerachyCode__My_Click(sender As Object, e As EventArgs) Handles txtHirerachyCode._My_Click
        Try
            'Dim qry As String = "select TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE as Code ,TSPL_HIRERACHY_LEVEL_MASTER.Description as Name,TSPL_HIRERACHY_LEVEL_MASTER.Level as [Level] ,TSPL_HIRERACHY_LEVEL_MASTER.Created_By as [Created By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Created_Date,103) as [Created Date] ,TSPL_HIRERACHY_LEVEL_MASTER.Modified_By as [Modified By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Modified_Date,103) as [Modified Date]  From TSPL_HIRERACHY_LEVEL_MASTER  "
            Dim qry As String = " select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name as Name,ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level_Code,'')  as Code,ISNULL(TSPL_COST_CENTRE_FINANCIAL.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level,'') AS [Hirerachy Level] ,TSPL_COST_CENTRE_FINANCIAL.Created_By as [Created By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Created_Date,103) as [Created Date] ,TSPL_COST_CENTRE_FINANCIAL.Modified_By as [Modified By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Modified_Date,103) as [Modified Date]  From TSPL_COST_CENTRE_FINANCIAL  Where 2=2"
            txtHirerachyCode.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtHirerachyCode.arrValueMember, txtHirerachyCode.arrDispalyMember)
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
End Class

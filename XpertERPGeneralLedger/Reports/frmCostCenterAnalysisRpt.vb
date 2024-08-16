'---------------------------------------------------------------
'--01/08/2012--Created By--[Pankaj Kumar]----By_Amit Sir
'---------------------------------------------------------------
'--Updation By-[Pankaj Kumar]--added a list containing items (Dept+Employee, Vehicle, Machine, Visi, Acc+Location) and Printed Report for each Item Separately--by--Ranjana Mam
'--Updation By-[Pankaj Kumar]--added a Filter (Accounts) --by--Amit Sir
'--Updation By-[Pankaj Kumar]--against Ticket No--[BM00000001127]
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
Imports common

Public Class FrmCostCenterAnalysisRpt
    Inherits FrmMainTranScreen
    Dim DtMain As DataTable
    Dim qry As String
    Dim reporttype As String
    Dim vechile As String
    Dim department As String
    Dim employee As String
    Dim machine As String
    Dim visi As String
    ' Dim location As String
    Dim account As String
    Dim IsFormLoad As Boolean = False
    Private Sub FrmCostCenterAnalysisRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadVehicle()
        LoadDepartment()
        LoadEmployee()
        LoadMachine()
        LoadVisi()
        LoadLocation()
        LoadAccounts()
        LoadVendors()
        Reset()
        SetUserMgmtNew()
        IsFormLoad = True
        LoadType()
        IsFormLoad = False
        chkSummary.Visible = False
        listType.SelectedIndex = 4
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmCostCenterAnalysisRpt)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        'btnExportToExcel.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
        btnExport.Visible = MyBase.isExport
    End Sub

    Public Sub Reset()

        If System.DateTime.Now.Date.Month >= 1 AndAlso System.DateTime.Now.Date.Month <= 3 Then
            txtFromDate.Value = clsCommon.myCDate("01/04/" + clsCommon.myCstr(System.DateTime.Now.Date.Year - 1))
        Else
            txtFromDate.Value = clsCommon.myCDate("01/04/" + clsCommon.myCstr(System.DateTime.Now.Date.Year))
        End If

        txtToDate.Value = clsCommon.GETSERVERDATE
        chkVehicleAll.IsChecked = True
        chkdeptAll.IsChecked = True
        chkEmpAll.IsChecked = True
        chkMachineAll.IsChecked = True
        chkVisiAll.IsChecked = True
        chkLocAll.IsChecked = True
        chkAccAll.IsChecked = True
        chkVndrAll.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        listType.SelectedIndex = 4
    End Sub

    Sub LoadVehicle()
        Dim qry1 As String = " Select Segment_code as [Code], Description  from TSPL_GL_SEGMENT_CODE Where Seg_No='2'"
        cbgVehicle.DataSource = clsDBFuncationality.GetDataTable(qry1)
        cbgVehicle.ValueMember = "Code"
        cbgVehicle.DisplayMember = "Description"
    End Sub
    Sub LoadDepartment()
        Dim qry1 As String = " Select Segment_code as [Code], Description  from TSPL_GL_SEGMENT_CODE Where Seg_No='3'"
        dgvDept.DataSource = clsDBFuncationality.GetDataTable(qry1)
        dgvDept.ValueMember = "Code"
        dgvDept.DisplayMember = "Description"
    End Sub

    Sub LoadEmployee()
        Dim qry1 As String = " Select Segment_code as [Code], Description  from TSPL_GL_SEGMENT_CODE Where Seg_No='4'"
        dgvEmp.DataSource = clsDBFuncationality.GetDataTable(qry1)
        dgvEmp.ValueMember = "Code"
        dgvEmp.DisplayMember = "Description"
    End Sub

    Sub LoadMachine()
        Dim qry1 As String = " Select Segment_code as [Code], Description  from TSPL_GL_SEGMENT_CODE Where Seg_No='5'"
        cbgMachine.DataSource = clsDBFuncationality.GetDataTable(qry1)
        cbgMachine.ValueMember = "Code"
        cbgMachine.DisplayMember = "Description"
    End Sub

    Sub LoadVisi()
        Dim qry1 As String = " Select Segment_code as [Code], Description  from TSPL_GL_SEGMENT_CODE Where Seg_No='6'"
        cbgVisi.DataSource = clsDBFuncationality.GetDataTable(qry1)
        cbgVisi.ValueMember = "Code"
        cbgVisi.DisplayMember = "Description"
    End Sub

    Sub LoadAccounts()
        Dim qry1 As String = " Select Distinct Account_Seg_Code1 as Code , Account_Seg_Desc1 as Description from TSPL_GL_ACCOUNTS"
        cbgAccounts.DataSource = clsDBFuncationality.GetDataTable(qry1)
        cbgAccounts.ValueMember = "Code"
        cbgAccounts.DisplayMember = "Description"
    End Sub

    Sub LoadLocation()
        ' Dim qry1 As String = " Select Segment_code as [Code], Description  from TSPL_GL_SEGMENT_CODE Where Seg_No='7'"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Sub LoadVendors()
        Dim qry As String = " select Vendor_Code , Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code in ( select CustVend_Code from tspl_journal_master) And Status='N'  "
        Try
            cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbgVendor.ValueMember = "Vendor_Code"
            cbgVendor.DisplayMember = "Vendor_Name"

        Catch ex As Exception
        End Try
    End Sub
    Public Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Dept+Employee"
        dr("Name") = "Dept+Employee"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Vehicle"
        dr("Name") = "Vehicle"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Visi"
        dr("Name") = "Visi"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Machine"
        dr("Name") = "Machine"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Account+Location"
        dr("Name") = "Account+Location"
        dt.Rows.Add(dr)

        listType.DataSource = dt
        listType.DisplayMember = "Name"
        listType.ValueMember = "Code"

    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        GetReportID()
        PageSetupReport_ID = MyBase.Form_ID
        RefreshData()
    End Sub
    Sub GetReportID()
        Dim VarID As String = ""

        If clsCommon.CompairString(listType.Text, "Vehicle") = CompairStringResult.Equal Then
            VarID += "_V"
        End If
        If chkSummary.Checked = True Then
            VarID += "_S"
        End If
        gv1.VarID = VarID

    End Sub

    'Public Function RefreshData()
    '    gv1.EnableFiltering = True
    '    gv1.DataSource = Nothing
    '    gv1.Columns.Clear()
    '    gv1.Rows.Clear()
    '    gv1.GroupDescriptors.Clear()
    '    gv1.MasterTemplate.SummaryRowsBottom.Clear()

    '    If chkVehicleSelect.IsChecked AndAlso cbgVehicle.CheckedValue.Count <= 0 Then
    '        common.clsCommon.MyMessageBoxShow("Please Select atleast one Vehicle Or Select All", Me.Text)
    '        Return False
    '        Exit Function
    '    End If
    '    If chkDeptSelect.IsChecked AndAlso dgvDept.CheckedValue.Count <= 0 Then
    '        common.clsCommon.MyMessageBoxShow("Please Select atleast one Department Or Select All", Me.Text)
    '        Return False
    '        Exit Function
    '    End If
    '    If chkEmpSelect.IsChecked AndAlso dgvEmp.CheckedValue.Count <= 0 Then
    '        common.clsCommon.MyMessageBoxShow("Please Select atleast one Employee Or Select All", Me.Text)
    '        Return False
    '        Exit Function
    '    End If
    '    If chkMachineSelect.IsChecked AndAlso cbgMachine.CheckedValue.Count <= 0 Then
    '        common.clsCommon.MyMessageBoxShow("Please Select atleast one Machine Or Select All", Me.Text)
    '        Return False
    '        Exit Function
    '    End If
    '    If chkVisiSelect.IsChecked AndAlso cbgVisi.CheckedValue.Count <= 0 Then
    '        common.clsCommon.MyMessageBoxShow("Please Select atleast one Visi Or Select All", Me.Text)
    '        Return False
    '        Exit Function
    '    End If
    '    If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
    '        common.clsCommon.MyMessageBoxShow("Please Select atleast one Employee Or Select All", Me.Text)
    '        Return False
    '        Exit Function
    '    End If
    '    If chkAccSelect.IsChecked AndAlso cbgAccounts.CheckedValue.Count <= 0 Then
    '        common.clsCommon.MyMessageBoxShow("Please Select atleast one Account Or Select All", Me.Text)
    '        Return False
    '        Exit Function
    '    End If



    '    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '    Dim reporttype As String = ""
    '    Dim vechile As String = ""
    '    Dim department As String = ""
    '    Dim employee As String = ""
    '    Dim machine As String = ""
    '    Dim visi As String = ""
    '    Dim location As String = ""
    '    Dim account As String = ""


    '    reporttype = listType.Text

    '    If chkAccSelect.IsChecked AndAlso cbgAccounts.CheckedValue.Count > 0 Then
    '        account = ("'" + clsCommon.GetMulcallString(cbgAccounts.CheckedValue) + "' ")
    '        account = account.Replace("'", "")
    '    End If
    '    If chkVehicleSelect.IsChecked AndAlso cbgVehicle.CheckedValue.Count > 0 Then
    '        vechile = ("'" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + "'")
    '        vechile = vechile.Replace("'", "")
    '    End If
    '    If chkDeptSelect.IsChecked AndAlso dgvDept.CheckedValue.Count > 0 Then
    '        department = ("'" + clsCommon.GetMulcallString(dgvDept.CheckedValue) + "'")
    '        department = department.Replace("'", "")
    '    End If

    '    If chkEmpSelect.IsChecked AndAlso dgvEmp.CheckedValue.Count > 0 Then
    '        employee = ("'" + clsCommon.GetMulcallString(dgvEmp.CheckedValue) + "'")
    '        employee = employee.Replace("'", "")
    '    End If

    '    If chkMachineSelect.IsChecked AndAlso cbgMachine.CheckedValue.Count > 0 Then
    '        machine = ("'" + clsCommon.GetMulcallString(cbgMachine.CheckedValue) + "'")
    '        machine = machine.Replace("'", "")
    '    End If

    '    If chkVisiSelect.IsChecked AndAlso cbgVisi.CheckedValue.Count > 0 Then
    '        visi = ("'" + clsCommon.GetMulcallString(cbgVisi.CheckedValue) + "'")
    '        visi = visi.Replace("'", "")
    '    End If

    '    If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
    '        location = ("'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ",")
    '        location = location.Replace("'", "")
    '    End If

    '    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    '    Dim RptType As String = ""
    '    If listType.Text = "Dept+Employee" Then
    '        RptType = "  TSPL_JOURNAL_DETAILS.Account_Seg_Code3 as [Dept], TSPL_JOURNAL_DETAILS.Account_Seg_Desc3 as [Dept Name], TSPL_JOURNAL_DETAILS.Account_Seg_Code4 as [Cost Centre], TSPL_JOURNAL_DETAILS.Account_Seg_Desc4 as [Name]"
    '    ElseIf listType.Text = "Vehicle" Then
    '        RptType = " TSPL_JOURNAL_DETAILS.Account_Seg_Code2 as [Cost Centre], TSPL_JOURNAL_DETAILS.Account_Seg_Desc2 as [Name]"
    '    ElseIf listType.Text = "Visi" Then
    '        RptType = " TSPL_JOURNAL_DETAILS.Account_Seg_Code6 as [Cost Centre], TSPL_JOURNAL_DETAILS.Account_Seg_Desc6 as [Name]"
    '    ElseIf listType.Text = "Machine" Then
    '        RptType = " TSPL_JOURNAL_DETAILS.Account_Seg_Code5 as [Cost Centre], TSPL_JOURNAL_DETAILS.Account_Seg_Desc5 as [Name]"
    '    ElseIf listType.Text = "Account+Location" Then
    '        RptType = " TSPL_JOURNAL_DETAILS.Account_Seg_Code7 as [Cost Centre], TSPL_JOURNAL_DETAILS.Account_Seg_Desc7 as [Name]"
    '    End If



    '    qry = "Select '" + reporttype + "' as ReportType,'" + account + "' as Accounts,'" + vechile + "' as Vehicle,'" + location + "' as Location,'" + visi + "' as Visi,'" + machine + "' as Machine,'" + employee + "' as Employee,'" + department + "' as Department,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") + "' as [StartDate], '" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "' as [EndDate], '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd-MMM-yyyy") + "' as [RunDate], "
    '    qry += " " + RptType + " ,convert( varchar,TSPL_JOURNAL_MASTER.Voucher_Date,103) as [Date], ISNULL(TSPL_BANK_MASTER.Bank_type, '') as [Type], TSPL_JOURNAL_MASTER.Voucher_No as [Vr. No], TSPL_JOURNAL_MASTER.Source_Doc_No, TSPL_JOURNAL_DETAILS.Account_Seg_Code1 as [A/c Code], TSPL_JOURNAL_DETAILS.Account_Seg_Desc1 as "
    '    qry += " [A/c Name], ISNULL(Case When ISNULL(TSPL_PAYMENT_HEADER.Cheque_No, '')<>'' Then TSPL_PAYMENT_HEADER.Cheque_No Else case When ISNULL(TSPL_RECEIPT_HEADER.Cheque_No, '')<>'' Then TSPL_RECEIPT_HEADER.Cheque_No END END, '') as [Chq.No], TSPL_JOURNAL_DETAILS.Description as [Description], Case When TSPL_JOURNAL_DETAILS.Amount>0 Then TSPL_JOURNAL_DETAILS.Amount Else 0 End as [Debit], "
    '    qry += " Case When TSPL_JOURNAL_DETAILS.Amount<0 Then (TSPL_JOURNAL_DETAILS.Amount*-1) Else 0 End as [Credit],TSPL_COMPANY_MASTER.Comp_Name,  (TSPL_COMPANY_MASTER.Add1+ Case When ISNULL(TSPL_COMPANY_MASTER.Add2,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.Add2 End+ "
    '    qry += " Case When ISNULL(TSPL_COMPANY_MASTER.Add3, '')='' then '' else ', '+TSPL_COMPANY_MASTER.Add3 End+ "
    '    qry += " Case When ISNULL(TSPL_CITY_MASTER.City_Name,'')='' then '' else ', '+TSPL_CITY_MASTER.City_Name  End+ Case When ISNULL(TSPL_COMPANY_MASTER.State,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.State End+ case When ISNULL(TSPL_COMPANY_MASTER.Pincode,'')='' Then '' else '- '+TSPL_COMPANY_MASTER.Pincode End ) as Address  from TSPL_JOURNAL_DETAILS "
    '    qry += " Left Outer Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Journal_No=TSPL_JOURNAL_DETAILS.Journal_No "
    '    qry += " Left Outer Join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_JOURNAL_MASTER.Source_Doc_No "
    '    qry += " Left Outer Join TSPL_RECEIPT_HEADER  on TSPL_RECEIPT_HEADER.Receipt_No =TSPL_JOURNAL_MASTER.Source_Doc_No "
    '    qry += " Left OUTER JOIN TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code OR TSPL_BANK_MASTER.BANK_CODE=TSPL_RECEIPT_HEADER.Bank_Code "
    '    qry += " Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_JOURNAL_MASTER.Comp_Code "
    '    qry += " Left Outer Join TSPL_CITY_MASTER on TSPL_COMPANY_MASTER.City_Code=TSPL_CITY_MASTER.City_Code   "
    '    qry += " Where  CONVERT(Date, TSPL_JOURNAL_MASTER.Voucher_Date, 103)>=CONVERT(Date, '" + txtFromDate.Value + "', 103) AND  CONVERT(Date, TSPL_JOURNAL_MASTER.Voucher_Date, 103)<=CONVERT(Date, '" + txtToDate.Value + "', 103) "



    '    If listType.Text = "Dept+Employee" Then
    '        qry += " AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code4, '')<>''"
    '    ElseIf listType.Text = "Vehicle" Then
    '        qry += " AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code2, '')<>''"
    '    ElseIf listType.Text = "Machine" Then
    '        qry += " AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code5, '')<>''"
    '    ElseIf listType.Text = "Visi" Then
    '        qry += " AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code6, '')<>''"
    '    ElseIf listType.Text = "Account+Location" Then
    '        qry += " AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code1, '')<>''AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code7, '')<>''"
    '    End If

    '    If chkAccSelect.IsChecked AndAlso cbgAccounts.CheckedValue.Count > 0 Then
    '        qry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code1 IN (" + clsCommon.GetMulcallString(cbgAccounts.CheckedValue) + ")"
    '    End If
    '    If chkVehicleSelect.IsChecked AndAlso cbgVehicle.CheckedValue.Count > 0 Then
    '        qry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code2 IN (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ")"
    '    End If
    '    If chkDeptSelect.IsChecked AndAlso dgvDept.CheckedValue.Count > 0 Then
    '        qry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code3 IN (" + clsCommon.GetMulcallString(dgvDept.CheckedValue) + ")"
    '    End If
    '    If chkEmpSelect.IsChecked AndAlso dgvEmp.CheckedValue.Count > 0 Then
    '        qry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code4 IN (" + clsCommon.GetMulcallString(dgvEmp.CheckedValue) + ")"
    '    End If
    '    If chkMachineSelect.IsChecked AndAlso cbgMachine.CheckedValue.Count > 0 Then
    '        qry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code5 IN (" + clsCommon.GetMulcallString(cbgMachine.CheckedValue) + ")"
    '    End If
    '    If chkVisiSelect.IsChecked AndAlso cbgVisi.CheckedValue.Count > 0 Then
    '        qry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code6 IN (" + clsCommon.GetMulcallString(cbgVisi.CheckedValue) + ")"
    '    End If
    '    If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
    '        qry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code7 IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
    '    End If
    '    Dim qryAc As String = "Select Distinct '['+Account_Seg_Desc1+']' as Account from TSPL_JOURNAL_DETAILS "

    '    If clsCommon.CompairString(listType.SelectedValue, "Vehicle") = CompairStringResult.Equal And chkSummary.Checked Then
    '        qryAc += " Where ISNULL(Account_Seg_Code2,'')<>''"
    '    ElseIf clsCommon.CompairString(listType.SelectedValue, "Dept+Employee") = CompairStringResult.Equal And chkSummary.Checked Then
    '        qryAc += " Where ISNULL(Account_Seg_Code4,'')<>''"
    '    End If
    '    If (clsCommon.CompairString(listType.SelectedValue, "Vehicle") = CompairStringResult.Equal) Or (clsCommon.CompairString(listType.SelectedValue, "Dept+Employee") = CompairStringResult.Equal) And chkSummary.Checked Then
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryAc)
    '        Dim ItemString1 As String = ""
    '        Dim ItemString2 As String = ""
    '        Dim strTotal As String = ""
    '        Dim Count As Integer = 0
    '        For Each dr As DataRow In dt.Rows
    '            Dim Acc As String = clsCommon.myCstr(dr("Account"))
    '            If Count > 0 Then
    '                ItemString1 += "," + Acc
    '                ItemString2 += "," + "ISNULL(" + Acc + ",0) AS " + Acc
    '                strTotal += "+" + "ISNULL(" + Acc + ",0)"
    '            Else
    '                ItemString1 += Acc
    '                ItemString2 += "ISNULL(" + Acc + ",0) AS " + Acc
    '                strTotal += "ISNULL(" + Acc + ",0)"
    '            End If
    '            Count += 1
    '        Next
    '        If Count <= 0 Then
    '            clsCommon.MyMessageBoxShow("No data found.")
    '            Return False
    '        Else
    '            qry = "Select [Cost Centre], Name, " + ItemString2 + ", (" + strTotal + ") as Total from ( Select [Cost Centre], MAX(Name) as Name, (SUM(Debit) - SUM(Credit)) as charge, MAX([A/c Name]) as [A/c Name]   from ( " + qry + " ) XXX Group by [Cost Centre], [A/c Code] ) Final Pivot (SUM(charge) FOR [A/c Name] IN (" + ItemString1 + ")) AS pvt Order by [Cost Centre]"
    '            dt = clsDBFuncationality.GetDataTable(qry)
    '            gv1.DataSource = dt
    '            FormatGV1()
    '        End If
    '        Return True
    '    Else
    '        qry += " ORDER BY CONVERT(Date, TSPL_JOURNAL_MASTER.Voucher_Date, 103), Debit DESC, Credit DESC"
    '    End If

    '    Try
    '        DtMain = clsDBFuncationality.GetDataTable(qry)
    '        If DtMain.Rows.Count > 0 Then
    '            gv1.DataSource = DtMain
    '            FormatGV1()
    '            RadPageView1.SelectedPage = RadPageViewPage2
    '            Return True
    '        Else
    '            common.clsCommon.MyMessageBoxShow("No Data Found")
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
    '        Return False
    '    End Try
    'End Function




    '- UPDATED DATE : 15 NOV - 2016 - BECAUSE WHOLE SOL WAS CORRUPED ON SERVER SOURCE SAFE ---
    Public Function RefreshData()
        'kunal > kdil > date 11-nov-16 > Request No > KLREQ000663 > Ticket : BM00000009584
        gv1.EnableFiltering = True
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()

        If chkVehicleSelect.IsChecked AndAlso cbgVehicle.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select atleast one Vehicle Or Select All", Me.Text)
            Return False
            Exit Function
        End If
        If chkDeptSelect.IsChecked AndAlso dgvDept.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select atleast one Department Or Select All", Me.Text)
            Return False
            Exit Function
        End If
        If chkEmpSelect.IsChecked AndAlso dgvEmp.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select atleast one Employee Or Select All", Me.Text)
            Return False
            Exit Function
        End If
        If chkMachineSelect.IsChecked AndAlso cbgMachine.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select atleast one Machine Or Select All", Me.Text)
            Return False
            Exit Function
        End If
        If chkVisiSelect.IsChecked AndAlso cbgVisi.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select atleast one Visi Or Select All", Me.Text)
            Return False
            Exit Function
        End If
        If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select atleast one Employee Or Select All", Me.Text)
            Return False
            Exit Function
        End If
        If chkAccSelect.IsChecked AndAlso cbgAccounts.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select atleast one Account Or Select All", Me.Text)
            Return False
            Exit Function
        End If

        If chkVndrSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select atleast one Vendor Or Select All", Me.Text)
            Return False
            Exit Function
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim reporttype As String = ""
        Dim vechile As String = ""
        Dim department As String = ""
        Dim employee As String = ""
        Dim machine As String = ""
        Dim visi As String = ""
        Dim location As String = ""
        Dim account As String = ""
        reporttype = listType.Text

        If chkAccSelect.IsChecked AndAlso cbgAccounts.CheckedValue.Count > 0 Then
            account = ("'" + clsCommon.GetMulcallString(cbgAccounts.CheckedValue) + "' ")
            account = account.Replace("'", "")
        End If
        If chkVehicleSelect.IsChecked AndAlso cbgVehicle.CheckedValue.Count > 0 Then
            vechile = ("'" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + "'")
            vechile = vechile.Replace("'", "")
        End If
        If chkDeptSelect.IsChecked AndAlso dgvDept.CheckedValue.Count > 0 Then
            department = ("'" + clsCommon.GetMulcallString(dgvDept.CheckedValue) + "'")
            department = department.Replace("'", "")
        End If

        If chkEmpSelect.IsChecked AndAlso dgvEmp.CheckedValue.Count > 0 Then
            employee = ("'" + clsCommon.GetMulcallString(dgvEmp.CheckedValue) + "'")
            employee = employee.Replace("'", "")
        End If

        If chkMachineSelect.IsChecked AndAlso cbgMachine.CheckedValue.Count > 0 Then
            machine = ("'" + clsCommon.GetMulcallString(cbgMachine.CheckedValue) + "'")
            machine = machine.Replace("'", "")
        End If

        If chkVisiSelect.IsChecked AndAlso cbgVisi.CheckedValue.Count > 0 Then
            visi = ("'" + clsCommon.GetMulcallString(cbgVisi.CheckedValue) + "'")
            visi = visi.Replace("'", "")
        End If

        If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
            location = ("'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ",")
            location = location.Replace("'", "")
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim RptType As String = ""
        If listType.Text = "Dept+Employee" Then
            RptType = "  TSPL_JOURNAL_DETAILS.Account_Seg_Code3 as [Dept], TSPL_JOURNAL_DETAILS.Account_Seg_Desc3 as [Dept Name], TSPL_JOURNAL_DETAILS.Account_Seg_Code4 as [Cost Centre], TSPL_JOURNAL_DETAILS.Account_Seg_Desc4 as [Name]"
        ElseIf listType.Text = "Vehicle" Then
            RptType = " TSPL_JOURNAL_DETAILS.Account_Seg_Code2 as [Cost Centre], TSPL_JOURNAL_DETAILS.Account_Seg_Desc2 as [Name]"
        ElseIf listType.Text = "Visi" Then
            RptType = " TSPL_JOURNAL_DETAILS.Account_Seg_Code6 as [Cost Centre], TSPL_JOURNAL_DETAILS.Account_Seg_Desc6 as [Name]"
        ElseIf listType.Text = "Machine" Then
            RptType = " TSPL_JOURNAL_DETAILS.Account_Seg_Code5 as [Cost Centre], TSPL_JOURNAL_DETAILS.Account_Seg_Desc5 as [Name]"
        ElseIf listType.Text = "Account+Location" Then
            RptType = " TSPL_JOURNAL_DETAILS.Account_Seg_Code7 as [Cost Centre], TSPL_JOURNAL_DETAILS.Account_Seg_Desc7 as [Name]"
        End If
        ''richa agarwal 21 May,2019 do correction on join of TSPL_JOURNAL_MASTER with TSPL_JOURNAL_DETAILS KDI/21/05/19-000454
        qry = "Select '" + reporttype + "' as ReportType,'" + account + "' as Accounts,'" + vechile + "' as Vehicle,'" + location + "' as Location,'" + visi + "' as Visi,'" + machine + "' as Machine,'" + employee + "' as Employee,'" + department + "' as Department,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") + "' as [StartDate], '" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "' as [EndDate], '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd-MMM-yyyy") + "' as [RunDate], "
        qry += " " + RptType + " ,convert( varchar,TSPL_JOURNAL_MASTER.Voucher_Date,103) as [Date], ISNULL(TSPL_BANK_MASTER.Bank_type, '') as [Type], TSPL_JOURNAL_MASTER.Voucher_No as [Vr. No], TSPL_JOURNAL_MASTER.Source_Doc_No, tspl_journal_master.CustVend_Code  As [CustVend_Code], tspl_journal_master.CustVend_Name As [CustVend_Name],  TSPL_JOURNAL_DETAILS.Account_Seg_Code1 as [A/c Code], TSPL_JOURNAL_DETAILS.Account_Seg_Desc1 as "
        qry += " [A/c Name], ISNULL(Case When ISNULL(TSPL_PAYMENT_HEADER.Cheque_No, '')<>'' Then TSPL_PAYMENT_HEADER.Cheque_No Else case When ISNULL(TSPL_RECEIPT_HEADER.Cheque_No, '')<>'' Then TSPL_RECEIPT_HEADER.Cheque_No END END, '') as [Chq.No], TSPL_JOURNAL_DETAILS.Description as [Description], Case When TSPL_JOURNAL_DETAILS.Amount>0 Then TSPL_JOURNAL_DETAILS.Amount Else 0 End as [Debit], "
        qry += " Case When TSPL_JOURNAL_DETAILS.Amount<0 Then (TSPL_JOURNAL_DETAILS.Amount*-1) Else 0 End as [Credit],TSPL_COMPANY_MASTER.Comp_Name,  (TSPL_COMPANY_MASTER.Add1+ Case When ISNULL(TSPL_COMPANY_MASTER.Add2,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.Add2 End+ "
        qry += " Case When ISNULL(TSPL_COMPANY_MASTER.Add3, '')='' then '' else ', '+TSPL_COMPANY_MASTER.Add3 End+ "
        qry += " Case When ISNULL(TSPL_CITY_MASTER.City_Name,'')='' then '' else ', '+TSPL_CITY_MASTER.City_Name  End+ Case When ISNULL(TSPL_COMPANY_MASTER.State,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.State End+ case When ISNULL(TSPL_COMPANY_MASTER.Pincode,'')='' Then '' else '- '+TSPL_COMPANY_MASTER.Pincode End ) as Address  from TSPL_JOURNAL_DETAILS "
        qry += " Left Outer Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No "
        qry += " Left Outer Join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_JOURNAL_MASTER.Source_Doc_No "
        qry += " Left Outer Join TSPL_RECEIPT_HEADER  on TSPL_RECEIPT_HEADER.Receipt_No =TSPL_JOURNAL_MASTER.Source_Doc_No "
        qry += " Left OUTER JOIN TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code OR TSPL_BANK_MASTER.BANK_CODE=TSPL_RECEIPT_HEADER.Bank_Code "
        qry += " Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_JOURNAL_MASTER.Comp_Code "
        qry += " Left Outer Join TSPL_CITY_MASTER on TSPL_COMPANY_MASTER.City_Code=TSPL_CITY_MASTER.City_Code   "
        qry += " Where  CONVERT(Date, TSPL_JOURNAL_MASTER.Voucher_Date, 103)>=CONVERT(Date, '" + txtFromDate.Value + "', 103) AND  CONVERT(Date, TSPL_JOURNAL_MASTER.Voucher_Date, 103)<=CONVERT(Date, '" + txtToDate.Value + "', 103) "

        If listType.Text = "Dept+Employee" Then
            qry += " AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code4, '')<>''"
        ElseIf listType.Text = "Vehicle" Then
            qry += " AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code2, '')<>''"
        ElseIf listType.Text = "Machine" Then
            qry += " AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code5, '')<>''"
        ElseIf listType.Text = "Visi" Then
            qry += " AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code6, '')<>''"
        ElseIf listType.Text = "Account+Location" Then
            qry += " AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code1, '')<>''AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code7, '')<>''"
        End If

        If chkAccSelect.IsChecked AndAlso cbgAccounts.CheckedValue.Count > 0 Then
            qry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code1 IN (" + clsCommon.GetMulcallString(cbgAccounts.CheckedValue) + ")"
        End If
        If chkVehicleSelect.IsChecked AndAlso cbgVehicle.CheckedValue.Count > 0 Then
            qry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code2 IN (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ")"
        End If
        If chkDeptSelect.IsChecked AndAlso dgvDept.CheckedValue.Count > 0 Then
            qry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code3 IN (" + clsCommon.GetMulcallString(dgvDept.CheckedValue) + ")"
        End If
        If chkEmpSelect.IsChecked AndAlso dgvEmp.CheckedValue.Count > 0 Then
            qry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code4 IN (" + clsCommon.GetMulcallString(dgvEmp.CheckedValue) + ")"
        End If
        If chkMachineSelect.IsChecked AndAlso cbgMachine.CheckedValue.Count > 0 Then
            qry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code5 IN (" + clsCommon.GetMulcallString(cbgMachine.CheckedValue) + ")"
        End If
        If chkVisiSelect.IsChecked AndAlso cbgVisi.CheckedValue.Count > 0 Then
            qry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code6 IN (" + clsCommon.GetMulcallString(cbgVisi.CheckedValue) + ")"
        End If
        If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
            qry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code7 IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        End If

        If chkVndrSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count > 0 Then
            qry += " AND tspl_journal_master.CustVend_Code IN (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")"
        End If


        Dim qryAc As String = "Select Distinct '['+Account_Seg_Desc1+']' as Account from TSPL_JOURNAL_DETAILS "

        If clsCommon.CompairString(listType.SelectedValue, "Vehicle") = CompairStringResult.Equal And chkSummary.Checked Then
            qryAc += " Where ISNULL(Account_Seg_Code2,'')<>''"
        ElseIf clsCommon.CompairString(listType.SelectedValue, "Dept+Employee") = CompairStringResult.Equal And chkSummary.Checked Then
            qryAc += " Where ISNULL(Account_Seg_Code4,'')<>''"
        End If
        If (clsCommon.CompairString(listType.SelectedValue, "Vehicle") = CompairStringResult.Equal) Or (clsCommon.CompairString(listType.SelectedValue, "Dept+Employee") = CompairStringResult.Equal) And chkSummary.Checked Then
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryAc)
            Dim ItemString1 As String = ""
            Dim ItemString2 As String = ""
            Dim strTotal As String = ""
            Dim Count As Integer = 0
            For Each dr As DataRow In dt.Rows
                Dim Acc As String = clsCommon.myCstr(dr("Account"))
                If Count > 0 Then
                    ItemString1 += "," + Acc
                    ItemString2 += "," + "ISNULL(" + Acc + ",0) AS " + Acc
                    strTotal += "+" + "ISNULL(" + Acc + ",0)"
                Else
                    ItemString1 += Acc
                    ItemString2 += "ISNULL(" + Acc + ",0) AS " + Acc
                    strTotal += "ISNULL(" + Acc + ",0)"
                End If
                Count += 1
            Next
            If Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No data found.", Me.Text)
                Return False
            Else
                qry = "Select [Cost Centre], Name, " + ItemString2 + ", (" + strTotal + ") as Total from ( Select [Cost Centre], MAX(Name) as Name, (SUM(Debit) - SUM(Credit)) as charge, MAX([A/c Name]) as [A/c Name]   from ( " + qry + " ) XXX Group by [Cost Centre], [A/c Code] ) Final Pivot (SUM(charge) FOR [A/c Name] IN (" + ItemString1 + ")) AS pvt Order by [Cost Centre]"
                dt = clsDBFuncationality.GetDataTable(qry)
                gv1.DataSource = dt
                FormatGV1()
            End If
            Return True
        Else
            qry += " ORDER BY CONVERT(Date, TSPL_JOURNAL_MASTER.Voucher_Date, 103), Debit DESC, Credit DESC"
        End If

        Try
            DtMain = clsDBFuncationality.GetDataTable(qry)
            If DtMain.Rows.Count > 0 Then
                gv1.DataSource = DtMain
                FormatGV1()
                RadPageView1.SelectedPage = RadPageViewPage2
                Return True
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Return False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Sub FormatGV1()
        If (clsCommon.CompairString(listType.SelectedValue, "Vehicle") = CompairStringResult.Equal) Or (clsCommon.CompairString(listType.SelectedValue, "Dept+Employee") = CompairStringResult.Equal) And chkSummary.Checked Then
            For Each col As GridViewColumn In gv1.Columns
                col.Width = 100
            Next
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Total", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Else
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = False
            Next
            gv1.Columns("StartDate").IsVisible = False
            gv1.Columns("EndDate").IsVisible = False
            gv1.Columns("RunDate").IsVisible = False

            If listType.Text = "Dept+Employee" Then
                gv1.Columns("Dept").IsVisible = True
                gv1.Columns("Dept").Width = 50

                gv1.Columns("Dept Name").IsVisible = True
                gv1.Columns("Dept Name").Width = 100
            End If

            gv1.Columns("Cost Centre").IsVisible = True
            gv1.Columns("Cost Centre").Width = 70

            gv1.Columns("Name").IsVisible = True
            gv1.Columns("Name").Width = 150

            gv1.Columns("Date").IsVisible = True
            gv1.Columns("Date").Width = 80

            gv1.Columns("Type").IsVisible = True
            gv1.Columns("Type").Width = 50

            gv1.Columns("Vr. No").IsVisible = True
            gv1.Columns("Vr. No").Width = 100

            gv1.Columns("Source_Doc_No").IsVisible = True
            gv1.Columns("Source_Doc_No").Width = 100
            gv1.Columns("Source_Doc_No").HeaderText = "Source Document"

            'kunal > kdil > date 11-nov-16 > Request No > KLREQ000663 > Ticket : BM00000009584
            gv1.Columns("CustVend_Code").IsVisible = True
            gv1.Columns("CustVend_Code").Width = 100
            gv1.Columns("CustVend_Code").HeaderText = "Customer/Vendor Code"

            'kunal > kdil > date 11-nov-16 > Request No > KLREQ000663 > Ticket : BM00000009584
            gv1.Columns("CustVend_Name").IsVisible = True
            gv1.Columns("CustVend_Name").Width = 100
            gv1.Columns("CustVend_Name").HeaderText = "Customer/Vendor Name"

            gv1.Columns("A/c Code").IsVisible = True
            gv1.Columns("A/c Code").Width = 80

            gv1.Columns("A/c Name").IsVisible = True
            gv1.Columns("A/c Name").Width = 100

            gv1.Columns("Chq.No").IsVisible = True
            gv1.Columns("Chq.No").Width = 100

            gv1.Columns("Description").IsVisible = True
            gv1.Columns("Description").Width = 100

            gv1.Columns("Debit").IsVisible = True
            gv1.Columns("Debit").Width = 80

            gv1.Columns("Credit").IsVisible = True
            gv1.Columns("Credit").Width = 80

            gv1.Columns("Comp_Name").IsVisible = False
            gv1.Columns("Address").IsVisible = False

            gv1.Columns("ReportType").IsVisible = False
            gv1.Columns("Accounts").IsVisible = False
            gv1.Columns("Vehicle").IsVisible = False
            gv1.Columns("Location").IsVisible = False
            gv1.Columns("Visi").IsVisible = False
            gv1.Columns("Machine").IsVisible = False
            gv1.Columns("Employee").IsVisible = False
            gv1.Columns("Department").IsVisible = False






            'gv1.GroupDescriptors.Add(New GridGroupByExpression("Transfer_No as Transfer_No format ""{0}: {1}"" Group By Transfer_No"))
            'gv1.MasterTemplate.ExpandAllGroups()
            gv1.ShowGroupPanel = False
            'gv1.MasterTemplate.AutoExpandGroups = True
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Debit", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Credit", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Printdata()
    End Sub

    Public Sub Printdata()
        If RefreshData() Then
            Dim frmCRV As New frmCrystalReportViewer()
            If listType.Text = "Dept+Employee" Then
                frmCRV.funreport(CrystalReportFolder.GeneralLedger, DtMain, "crptCostCentreAnalysis", "Cost Centre Analysis Report @ Dept+Employee")
            ElseIf listType.Text = "Vehicle" Or listType.Text = "Visi" Or listType.Text = "Machine" Then
                frmCRV.funreport(CrystalReportFolder.GeneralLedger, DtMain, "crptCostCentreAnalysis@Vehicle", "Cost Centre Analysis Report @ Vehicle")
            ElseIf listType.Text = "Account+Location" Then
                frmCRV.funreport(CrystalReportFolder.GeneralLedger, DtMain, "crptCostCentreAnalysis@Ac&Loc", "Cost Centre Analysis Report @ Account+Location")
            End If
            frmCRV = Nothing
        End If
    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        Try
            If RefreshData() Then
                If (clsCommon.CompairString(listType.SelectedValue, "Vehicle") = CompairStringResult.Equal) Or (clsCommon.CompairString(listType.SelectedValue, "Dept+Employee") = CompairStringResult.Equal) And chkSummary.Checked Then
                    ExportToExcelGV(EnumExportTo.Excel)
                Else
                    ExportToExcel()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub ExportToExcel()

        Dim RptType As String = ""
        If listType.Text = "Dept+Employee" Then
            RptType = " TSPL_JOURNAL_DETAILS.Account_Seg_Code3 as [Dept], TSPL_JOURNAL_DETAILS.Account_Seg_Desc3 as [Dept Name], TSPL_JOURNAL_DETAILS.Account_Seg_Code4 as [Cost Centre], TSPL_JOURNAL_DETAILS.Account_Seg_Desc4 as [Name]"
        ElseIf listType.Text = "Vehicle" Then
            RptType = " TSPL_JOURNAL_DETAILS.Account_Seg_Code2 as [Cost Centre], TSPL_JOURNAL_DETAILS.Account_Seg_Desc2 as [Name]"
        ElseIf listType.Text = "Visi" Then
            RptType = " TSPL_JOURNAL_DETAILS.Account_Seg_Code6 as [Cost Centre], TSPL_JOURNAL_DETAILS.Account_Seg_Desc6 as [Name]"
        ElseIf listType.Text = "Machine" Then
            RptType = " TSPL_JOURNAL_DETAILS.Account_Seg_Code5 as [Cost Centre], TSPL_JOURNAL_DETAILS.Account_Seg_Desc5 as [Name]"
        ElseIf listType.Text = "Account+Location" Then
            RptType = " TSPL_JOURNAL_DETAILS.Account_Seg_Code7 as [Cost Centre], TSPL_JOURNAL_DETAILS.Account_Seg_Desc7 as [Name]"
        End If

        Dim qryExcel As String = "Select"
        qryExcel += "  " + RptType + ","
        qryExcel += "  TSPL_JOURNAL_MASTER.Voucher_Date as [Date], ISNULL(TSPL_BANK_MASTER.Bank_type, '') as [Type], TSPL_JOURNAL_MASTER.Voucher_No as [Vr. No], TSPL_JOURNAL_DETAILS.Account_Seg_Code1 as [A/c Code], TSPL_JOURNAL_DETAILS.Account_Seg_Desc1 as "
        qryExcel += " [A/c Name], ISNULL(Case When ISNULL(TSPL_PAYMENT_HEADER.Cheque_No, '')<>'' Then TSPL_PAYMENT_HEADER.Cheque_No Else case When ISNULL(TSPL_RECEIPT_HEADER.Cheque_No, '')<>'' Then TSPL_RECEIPT_HEADER.Cheque_No END END, '') as [Chq.No], TSPL_JOURNAL_DETAILS.Description as [Description], Case When TSPL_JOURNAL_DETAILS.Amount>0 Then TSPL_JOURNAL_DETAILS.Amount Else 0 End as [Debit], "
        qryExcel += " Case When TSPL_JOURNAL_DETAILS.Amount<0 Then (TSPL_JOURNAL_DETAILS.Amount*-1) Else 0 End as [Credit]  from TSPL_JOURNAL_DETAILS "
        qryExcel += " Left Outer Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Journal_No=TSPL_JOURNAL_DETAILS.Journal_No "
        qryExcel += " Left Outer Join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_JOURNAL_MASTER.Source_Doc_No "
        qryExcel += " Left Outer Join TSPL_RECEIPT_HEADER  on TSPL_RECEIPT_HEADER.Receipt_No =TSPL_JOURNAL_MASTER.Source_Doc_No "
        qryExcel += " Left OUTER JOIN TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code OR TSPL_BANK_MASTER.BANK_CODE=TSPL_RECEIPT_HEADER.Bank_Code "
        qryExcel += " Where  CONVERT(Date, TSPL_JOURNAL_MASTER.Voucher_Date, 103)>=CONVERT(Date, '" + txtFromDate.Value + "', 103) AND  CONVERT(Date, TSPL_JOURNAL_MASTER.Voucher_Date, 103)<=CONVERT(Date, '" + txtToDate.Value + "', 103) "

        If listType.Text = "Dept+Employee" Then
            qryExcel += " AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code4, '')<>''"
        ElseIf listType.Text = "Vehicle" Then
            qryExcel += " AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code2, '')<>''"
        ElseIf listType.Text = "Machine" Then
            qryExcel += " AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code5, '')<>''"
        ElseIf listType.Text = "Visi" Then
            qryExcel += " AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code6, '')<>''"
        ElseIf listType.Text = "Account+Location" Then
            qryExcel += " AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code1, '')<>''AND ISNULL(TSPL_JOURNAL_DETAILS.Account_Seg_Code7, '')<>''"
        End If

        If chkAccSelect.IsChecked AndAlso cbgAccounts.CheckedValue.Count > 0 Then
            qryExcel += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code1 IN (" + clsCommon.GetMulcallString(cbgAccounts.CheckedValue) + ")"
        End If
        If chkVehicleSelect.IsChecked AndAlso cbgVehicle.CheckedValue.Count > 0 Then
            qryExcel += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code2 IN (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ")"
        End If
        If chkDeptSelect.IsChecked AndAlso dgvDept.CheckedValue.Count > 0 Then
            qryExcel += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code3 IN (" + clsCommon.GetMulcallString(dgvDept.CheckedValue) + ")"
        End If
        If chkEmpSelect.IsChecked AndAlso dgvEmp.CheckedValue.Count > 0 Then
            qryExcel += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code4 IN (" + clsCommon.GetMulcallString(dgvEmp.CheckedValue) + ")"
        End If
        If chkMachineSelect.IsChecked AndAlso cbgMachine.CheckedValue.Count > 0 Then
            qryExcel += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code5 IN (" + clsCommon.GetMulcallString(cbgMachine.CheckedValue) + ")"
        End If
        If chkVisiSelect.IsChecked AndAlso cbgVisi.CheckedValue.Count > 0 Then
            qryExcel += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code6 IN (" + clsCommon.GetMulcallString(cbgVisi.CheckedValue) + ")"
        End If
        If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
            qryExcel += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code7 IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        End If

        Try
            ExporttoMyExcel(qryExcel, Me)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Function ExporttoMyExcel(ByVal sql As String, ByVal frm As RadForm) As Boolean
        Dim sfd As SaveFileDialog = New SaveFileDialog()
        Dim Fullpath As String
        sfd.FileName = frm.Text

        sfd.Filter = "Excel Workbooks (*.xls;*.xlsx)|*.xls;*.xlsx"
        'If sfd.ShowDialog() = System.System.Windows.Forms.DialogResult.OK Then
        '    path = sfd.FileName
        'Else
        '    Return False
        'End If
        Dim path = Application.StartupPath
        Fullpath = path + "\" + sfd.FileName

        If Not path.Equals(String.Empty) Then
            Dim gv As New RadGridView()
            Try
                ''''' Dim exporter As New RadGridViewExcelExporter()
                gv.Name = "StoreLedger"
                frm.Controls.Add(gv)
                FillGridView(sql, gv)
                If gv.Rows.Count = 0 Then
                    Throw New Exception("There is no data for Show Excel Report.")
                End If
                Dim i As Integer = 0
                For i = 0 To gv.ColumnCount - 1
                    Dim grow As GridViewRowInfo = TryCast(gv.Rows(0), GridViewRowInfo)
                    If TypeOf grow.Cells(i).Value Is DateTime Then
                        Dim datecol As GridViewDateTimeColumn = TryCast(gv.Columns(i), GridViewDateTimeColumn)
                        datecol.ExcelExportType = DisplayFormatType.ShortDate
                    End If
                    If TypeOf grow.Cells(i).Value Is Decimal Then
                        Dim datecol As GridViewDecimalColumn = TryCast(gv.Columns(i), GridViewDecimalColumn)
                        datecol.ExcelExportType = DisplayFormatType.Standard
                    End If
                Next i
                '    exporter.Export(gv, path, frm.Text)

                Dim exporter As New ExportToExcelML(gv)
                AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
                exporter.ExportHierarchy = True
                ' exporter.ExportVisualSettings = True
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
                'common.clsCommon.MyMessageBoxShow("Excel Report Created!", "Export", MessageBoxButtons.OK)
                Return True
            Catch ex As Exception
                frm.Controls.Remove(gv)
                Throw New Exception(ex.Message)
                Return False
            End Try
        End If
        Return True
    End Function

    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelML.ExcelCellFormattingEventArgs)
        e.ExcelStyleElement.FontStyle.Bold = False
        e.ExcelStyleElement.FontStyle.Size = 8
        e.ExcelStyleElement.FontStyle.FontName = "Verdana"
    End Sub

    Private Sub chkdeptAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkdeptAll.ToggleStateChanged
        dgvDept.Enabled = False
    End Sub

    Private Sub chkDeptSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDeptSelect.ToggleStateChanged
        dgvDept.Enabled = True
    End Sub

    Private Sub chkEmpAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkEmpAll.ToggleStateChanged
        dgvEmp.Enabled = False
    End Sub

    Private Sub chkEmpSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkEmpSelect.ToggleStateChanged
        dgvEmp.Enabled = True
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged

        cbgLocation.Enabled = True
    End Sub

    Private Sub chkVehicleAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVehicleAll.ToggleStateChanged
        cbgVehicle.Enabled = False
    End Sub

    Private Sub chkVehicleSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVehicleSelect.ToggleStateChanged
        cbgVehicle.Enabled = True
    End Sub

    Private Sub chkMachineAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMachineAll.ToggleStateChanged
        cbgMachine.Enabled = False
    End Sub

    Private Sub chkMachineSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMachineSelect.ToggleStateChanged
        cbgMachine.Enabled = True
    End Sub

    Private Sub chkVisiAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVisiAll.ToggleStateChanged
        cbgVisi.Enabled = False
    End Sub

    Private Sub chkVisiSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVisiSelect.ToggleStateChanged
        cbgVisi.Enabled = True
    End Sub

    Private Sub chkAccAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAccAll.ToggleStateChanged
        cbgAccounts.Enabled = False
    End Sub

    Private Sub chkAccSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAccSelect.ToggleStateChanged
        cbgAccounts.Enabled = True
    End Sub

    Private Sub listType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles listType.SelectedIndexChanged
        If Not IsFormLoad Then
            chkSummary.Visible = False
            chkSummary.Checked = False
            If (clsCommon.CompairString(listType.SelectedValue, "Vehicle") = CompairStringResult.Equal) Or (clsCommon.CompairString(listType.SelectedValue, "Dept+Employee") = CompairStringResult.Equal) Then
                chkSummary.Visible = True
            End If
        End If
    End Sub

    Private Sub ExportToExcelGV(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            arrHeader.Add(CompName)
            arrHeader.Add("Cost Centre Analysis Report @ Vehicle   " + clsCommon.GETSERVERDATE() + "")
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + " ")
            clsCommon.MyExportToExcel("Cost Centre Analysis Report", gv1, arrHeader, Me.Text)

        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Private Sub chkSummary_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSummary.ToggleStateChanged
        If chkSummary.Checked Then
            btnPrint.Visible = False
        Else
            btnPrint.Visible = True
        End If
    End Sub
    ' KUNAL > DATE 15-12-2016 > KDIL > REQ : KLREQ000663 > TICKET : BM00000009584 > REMARK : EXTRA BUG WAS FOUND BY ME DURING FIXING THE TICKET AND IT WAS DONE BY RANJANA MADAM'S PERMISSION
    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Dim qry As String = " SELECT  DISTINCT  source_code,  Source_Desc  FROM TSPL_JOURNAL_MASTER WHERE 1=1  "
        Try
            If e.Column.Name = "Source_Doc_No" Then
                If clsCommon.myLen(gv1.CurrentRow.Cells("Source_Doc_No").Value) > 0 Then
                    qry += " and Source_Doc_No in ('" + gv1.CurrentRow.Cells("Source_Doc_No").Value + "')"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim result As String = Nothing
                    For Each val As DataRow In dt.Rows
                        result = val("source_code").ToString()

                        ' LINK SCREENS BASED ON CODES ------------------------------------------------------------------------------------
                        If result.Contains("AP-DN") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, gv1.CurrentRow.Cells("Source_Doc_No").Value)

                        ElseIf result.Contains("AP-MI") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, gv1.CurrentRow.Cells("Source_Doc_No").Value)

                        ElseIf result.Contains("AR-IN") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, gv1.CurrentRow.Cells("Source_Doc_No").Value)

                        ElseIf result.Contains("BK-TF") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.bankTransfer, gv1.CurrentRow.Cells("Source_Doc_No").Value)

                        ElseIf result.Contains("MM-TF") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, gv1.CurrentRow.Cells("Source_Doc_No").Value)


                        ElseIf result.Contains("PL-JE") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalaryGeneration, gv1.CurrentRow.Cells("Source_Doc_No").Value)

                        ElseIf result.Contains("AP-PY") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, gv1.CurrentRow.Cells("Source_Doc_No").Value)


                        ElseIf result.Contains("AR-CR") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, gv1.CurrentRow.Cells("Source_Doc_No").Value)

                        ElseIf result.Contains("AR-DC") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, gv1.CurrentRow.Cells("Source_Doc_No").Value)

                        ElseIf result.Contains("AR-MI") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, gv1.CurrentRow.Cells("Source_Doc_No").Value)


                        ElseIf result.Contains("CS-TR") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, gv1.CurrentRow.Cells("Source_Doc_No").Value)


                        ElseIf result.Contains("DI-CH") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, gv1.CurrentRow.Cells("Source_Doc_No").Value)


                        ElseIf result.Contains("AP-AD") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, gv1.CurrentRow.Cells("Source_Doc_No").Value)

                        ElseIf result.Contains("AP-CN") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, gv1.CurrentRow.Cells("Source_Doc_No").Value)

                        ElseIf result.Contains("AR-IN") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, gv1.CurrentRow.Cells("Source_Doc_No").Value)

                        ElseIf result.Contains("AR-PY") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, gv1.CurrentRow.Cells("Source_Doc_No").Value)


                        ElseIf result.Contains("AR-RF") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, gv1.CurrentRow.Cells("Source_Doc_No").Value)


                        ElseIf result.Contains("CS-RC") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, gv1.CurrentRow.Cells("Source_Doc_No").Value)


                        ElseIf result.Contains("GL-JE") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, gv1.CurrentRow.Cells("Source_Doc_No").Value)


                        ElseIf result.Contains("MT-IN") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, gv1.CurrentRow.Cells("Source_Doc_No").Value)


                        ElseIf result.Contains("PU-RE") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, gv1.CurrentRow.Cells("Source_Doc_No").Value)
                            'NA

                        ElseIf result.Contains("SN-RT") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.TransferReturn, gv1.CurrentRow.Cells("Source_Doc_No").Value)

                        ElseIf result.Contains("AP-IN") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, gv1.CurrentRow.Cells("Source_Doc_No").Value)

                        ElseIf result.Contains("AR-AD") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptAdjustmentEntry, gv1.CurrentRow.Cells("Source_Doc_No").Value)


                        ElseIf result.Contains("AR-DN") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, gv1.CurrentRow.Cells("Source_Doc_No").Value)

                        ElseIf result.Contains("AR-OA") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, gv1.CurrentRow.Cells("Source_Doc_No").Value)

                        ElseIf result.Contains("AR-PI") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, gv1.CurrentRow.Cells("Source_Doc_No").Value)
                          
                        ElseIf result.Contains("RV-TA") Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.reverseTransaction, gv1.CurrentRow.Cells("Source_Doc_No").Value)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmCostCenterAnalysisRpt & "'"))

            If chkLocSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location : " + strtemp)
            End If
            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkVndrAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkVndrAll.ToggleStateChanged
        cbgVendor.Enabled = False
    End Sub

    Private Sub chkVndrSelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkVndrSelect.ToggleStateChanged
        cbgVendor.Enabled = True
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub Export(ByVal IsPrint As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
                arrHeader.Add(strtemp)
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmCostCenterAnalysisRpt & "'"))

                If chkLocSelect.IsChecked Then
                    strtemp = ""
                    For Each Str As String In cbgLocation.CheckedDisplayMember
                        If clsCommon.myLen(strtemp) > 0 Then
                            strtemp += ", "
                        End If
                        strtemp += Str
                    Next
                    arrHeader.Add("Location : " + strtemp)
                End If
                If IsPrint = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    ''richa ERO/24/12/19-001164
                    'clsCommon.MyExportToExcel("Cost Centre Analysis Report", gv1, arrHeader, Me.Text)
                    clsCommon.MyExportToExcelGrid("Cost Centre Analysis Report", gv1, arrHeader, Me.Text)
                Else
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

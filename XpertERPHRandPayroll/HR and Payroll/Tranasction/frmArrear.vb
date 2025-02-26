Imports common
Imports System.Data.SqlClient
Imports System
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Public Class frmArrear
    Inherits FrmMainTranScreen
    Const colApply As String = "Apply"
    Const colEMPCode As String = "empcode"
    Const colempname As String = "empname"
    Const colBasic As String = "Basic"
    Const colDA As String = "DA"
    Const colDAArrearPer As String = "Arrear"
    Const colEPF As String = "EPF"
    Dim isNewEntry As Boolean = True
    Dim isInsideLoadData As Boolean = False
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim apply As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        apply.FormatString = ""
        apply.HeaderText = colApply
        apply.Name = colApply
        apply.Width = 80
        apply.ReadOnly = True
        apply.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(apply)

        Dim Empcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Empcode.FormatString = ""
        Empcode.HeaderText = "Employee Code"
        Empcode.Name = colEMPCode
        Empcode.Width = 100
        Empcode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(Empcode)

        Dim EmpName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        EmpName.FormatString = ""
        EmpName.HeaderText = "Employee Name"
        EmpName.Name = colempname
        EmpName.Width = 150
        EmpName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(EmpName)

        Dim Basic As GridViewDecimalColumn = New GridViewDecimalColumn()
        Basic.FormatString = ""
        Basic.HeaderText = "Basic"
        Basic.Name = colBasic
        Basic.Width = 150
        Basic.ReadOnly = True
        Basic.IsVisible = True
        gv1.MasterTemplate.Columns.Add(Basic)

        Dim DA As GridViewDecimalColumn = New GridViewDecimalColumn()
        DA.FormatString = ""
        DA.HeaderText = "DA"
        DA.Name = colDA
        DA.Width = 150
        DA.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(DA)

        Dim DAArrear As GridViewDecimalColumn = New GridViewDecimalColumn()
        DAArrear.FormatString = ""
        DAArrear.HeaderText = "DA Arrear %"
        DAArrear.Name = colDAArrearPer
        DAArrear.Width = 150
        DAArrear.ReadOnly = True
        DAArrear.IsVisible = True
        gv1.MasterTemplate.Columns.Add(DAArrear)

        Dim EPF As GridViewDecimalColumn = New GridViewDecimalColumn()
        EPF.FormatString = ""
        EPF.HeaderText = "PF"
        EPF.Name = colEPF
        EPF.Width = 150
        EPF.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(EPF)
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.EnableFiltering = True
        gv1.AllowAddNewRow = False
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub frmArrear_Load(sender As Object, e As EventArgs)
        Addnew()
    End Sub
    Sub Addnew()
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtApplicabbleDate.Value = clsCommon.GETSERVERDATE()
        txtDAArrear.Text = ""
        gv1.DataSource = Nothing
        LoadBlankGrid()
        BtnGo.Enabled = True
        gv2.DataSource = Nothing
        txtmultPayperiod.arrValueMember = Nothing
        txtmulLocation.arrValueMember = Nothing
        isNewEntry = True
        UsLock1.Status = ERPTransactionStatus.Pending
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = False
        btnDelete.Enabled = False
    End Sub
    Sub Godata()
        Try
            If clsCommon.myLen(txtDAArrear.Text) <= 0 Then
                Throw New Exception("Please Fill DA Arrear %")
            End If
            LoadBlankGrid()
            Dim PMCond As String = "''"
            Dim str As String = clsSalaryGeneration.GetArrearData(txtmultPayperiod.arrValueMember, txtmulLocation.arrValueMember, Nothing, PMCond, False)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(str)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colApply).Value = "Yes"
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEMPCode).Value = clsCommon.myCstr(dr("Emp_code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colempname).Value = clsCommon.myCstr(dr("EMPLOYEE_NAME"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBasic).Value = clsCommon.myCstr(dr("basic"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDA).Value = clsCommon.myCstr(dr("da"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEPF).Value = clsCommon.myCstr(dr("vepf"))
                    Dim Basic As Decimal = 0
                    Basic = clsCommon.myCstr(dr("basic"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDAArrearPer).Value = (Basic * txtDAArrear.Text) / 100
                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display ", Me.Text)
            End If
            Dim payPeriod As String = clsCommon.GetMulcallString(txtmultPayperiod.arrValueMember)
            Dim stringArray As String() = payPeriod.Replace("'", "").Split(","c)
            Dim stringList As List(Of String) = New List(Of String)(stringArray)
            If stringList IsNot Nothing AndAlso stringList.Count > 0 Then
                Dim strPeriod As String = Nothing
                Dim PayPeriodName As String = Nothing
                For Each StrP As String In stringList
                    If clsCommon.myLen(strPeriod) > 0 Then
                        strPeriod += ",[" + StrP + "] As [" + StrP + "]"
                        PayPeriodName += ",[" + StrP + "] "
                    Else
                        strPeriod = "[" + StrP + "] As [" + StrP + "]"
                        PayPeriodName = "[" + StrP + "] "
                    End If
                Next
                Dim DetailData As String = clsSalaryGeneration.GetArrearDetailData(txtmultPayperiod.arrValueMember, txtmulLocation.arrValueMember, Nothing, PMCond, False)

                Dim Qry As String = "SELECT EMP_CODE, EMPLOYEE_NAME, 
                       " + strPeriod + "
                FROM (
                    SELECT xy.EMP_CODE, 
                           xy.EMPLOYEE_NAME, 
                           xy.PAY_PERIOD_CODE, 
                           SUM(xy.basic) AS basic
                    FROM (" + DetailData + ")AS xy
                    GROUP BY xy.EMP_CODE, xy.EMPLOYEE_NAME, xy.PAY_PERIOD_CODE
                ) AS SourceTable
                PIVOT (
                    SUM(basic) 
                    FOR PAY_PERIOD_CODE IN (" + PayPeriodName + ") 
                ) AS PivotTable
                ORDER BY CAST(EMP_CODE AS INT);
                "
                Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(Qry)
                gv2.DataSource = Nothing
                gv2.Rows.Clear()
                gv2.Columns.Clear()
                gv2.GroupDescriptors.Clear()
                gv2.MasterTemplate.SummaryRowsBottom.Clear()
                gv2.DataSource = dt2
                gv2.BestFitColumns()
                gv2.EnableFiltering = True
                gv2.ShowGroupPanel = False
                If dt2 Is Nothing OrElse dt2.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        Godata()
    End Sub

    Private Sub txtmultPayperiod__My_Click(sender As Object, e As EventArgs) Handles txtmultPayperiod._My_Click
        Dim qry As String = " select TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE as [Code] ,TSPL_PAYPERIOD_MASTER.PAY_PERIOD_NAME as [Pay Period Name] ,TSPL_PAYPERIOD_MASTER.DATE_FROM as [Date From] ,TSPL_PAYPERIOD_MASTER.DATE_TO as [Date To] ,TSPL_PAYPERIOD_MASTER.DESCRIPTION as [Description] ,TSPL_PAYPERIOD_MASTER.POSTED as [Posted] ,TSPL_PAYPERIOD_MASTER.FREEZED as [Freezed] ,TSPL_PAYPERIOD_MASTER.Posting_Date as [Posting Date] ,TSPL_PAYPERIOD_MASTER.Created_By as [Created By] ,TSPL_PAYPERIOD_MASTER.Created_Date as [Created Date] ,TSPL_PAYPERIOD_MASTER.Modified_By as [Modified By] ,TSPL_PAYPERIOD_MASTER.Modified_Date as [Modified Date]  From TSPL_PAYPERIOD_MASTER  "
        txtmultPayperiod.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Code", txtmultPayperiod.arrValueMember, txtmultPayperiod.arrDispalyMember)

    End Sub
    Private Sub txtmulLocation__My_Click(sender As Object, e As EventArgs) Handles txtmulLocation._My_Click
        Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],TSPL_Location_MASTER.Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Hoadd1 ,Hoadd2,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Seg.Description as [Location Segment Description],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],tspl_location_master.Created_By as [Created By],tspl_location_master.Created_Date as [Created Date],tspl_location_master.Modify_By as [Modify By],tspl_location_master.Modify_Date as [Modify Date],tspl_location_master.Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],CST_No as [CST No],Phone1,Phone2,stock_transfer_ac as [Stock Tranfer A/C],Loss_Ac as [Loss A/C] ,Is_Consumption_Location as [Is Consumption Location],Is_Section as [Is Section],Section_Code as [Section Code],Is_Sub_Location as [Is Sub Location],Main_Location_Code as [Main Location Code],IsSubLocationWise as [Is Sub Location Wise] from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code   where Location_Type='Physical' "
        txtmulLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Code", txtmulLocation.arrValueMember, txtmulLocation.arrDispalyMember)
    End Sub
    Private Sub frmArrear_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        Addnew()
        LoadBlankGrid()
        ' Dim coll As Dictionary(Of String, String)

    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If (e.Column Is gv1.Columns(colApply)) Then
            If gv1.CurrentRow.Cells(colApply).Value = "No" Then
                gv1.CurrentRow.Cells(colApply).Value = "Yes"
            Else
                gv1.CurrentRow.Cells(colApply).Value = "No"
            End If
        End If
    End Sub
    Private Function AllowToSave() As Boolean 'ByVal isPost As Boolean
        Try
            Dim obj As New clsRMProcessLoss()
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If

            If clsCommon.myLen(txtmulLocation.arrValueMember) <= 0 Then
                txtmulLocation.Focus()
                txtmulLocation.Select()
                clsCommon.MyMessageBoxShow(Me, "Select Location", Me.Text)
                Return False
            End If
            If clsCommon.myLen(txtmultPayperiod.arrValueMember) <= 0 Then
                txtmultPayperiod.Focus()
                txtmultPayperiod.Select()
                clsCommon.MyMessageBoxShow(Me, "Select Pay Period", Me.Text)
                Return False
            End If

            If clsCommon.myLen(fndFromPeriod.Value) <= 0 Then
                fndFromPeriod.Focus()
                fndFromPeriod.Select()
                clsCommon.MyMessageBoxShow(Me, "Select Applicable Pay Period", Me.Text)
                Return False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Private Sub SaveData(ByVal isPost As Boolean)
        Dim obj As New clsDAArrear()
        Dim objpd As New ClsDAArrearDetail
        Try
            If AllowToSave() Then
                obj.document_code = clsCommon.myCstr(txtDocNo.Value)
                obj.document_date = clsCommon.myCDate(txtDate.Value)
                obj.Pay_Period = clsCommon.myCstr(fndFromPeriod.Value)
                obj.DA_Arrear = clsCommon.myCdbl(txtDAArrear.Text)
                obj.Applicable_date = clsCommon.myCDate(txtApplicabbleDate.Value)
                obj.ArrD = New List(Of ClsDAArrearDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    objpd = New ClsDAArrearDetail()
                    If clsCommon.myCstr(grow.Cells(colApply).Value) = "Yes" Then
                        Dim objTr As New ClsDAArrearDetail()
                        objTr.Apply = "1"
                        objTr.Emp_Code = clsCommon.myCdbl(grow.Cells(colEMPCode).Value)
                        objTr.Basic = clsCommon.myCdbl(grow.Cells(colBasic).Value)
                        objTr.DA = clsCommon.myCdbl(grow.Cells(colDA).Value)
                        objTr.DA_Arrear = clsCommon.myCdbl(grow.Cells(colDAArrearPer).Value)
                        objTr.PF = clsCommon.myCdbl(grow.Cells(colEPF).Value)
                        objTr.GPF = clsCommon.myCdbl(grow.Cells(colBasic).Value)
                        obj.ArrD.Add(objTr)
                    End If
                Next
                Dim arrUserType As New List(Of String)

                If txtmultPayperiod.arrValueMember IsNot Nothing Then
                    For i As Integer = 0 To txtmultPayperiod.arrValueMember.Count - 1
                        arrUserType.Add(txtmultPayperiod.arrValueMember(i))
                    Next
                Else
                    clsCommon.MyMessageBoxShow(Me, "Please select at least one Pay Period", Me.Text)
                    Exit Sub
                End If
                obj.Arr_PayPeriod = New List(Of clsPayPeriod_detail)
                For i As Integer = 0 To arrUserType.Count - 1
                    Dim objtr As New clsPayPeriod_detail
                    objtr.PAY_PERIOD_Code = arrUserType(i)
                    obj.Arr_PayPeriod.Add(objtr)
                Next

                Dim arrUserTypeLocation As New List(Of String)

                If txtmulLocation.arrValueMember IsNot Nothing Then
                    For i As Integer = 0 To txtmulLocation.arrValueMember.Count - 1
                        arrUserTypeLocation.Add(txtmulLocation.arrValueMember(i))
                    Next
                Else
                    clsCommon.MyMessageBoxShow(Me, "Please select at least one Location", Me.Text)
                    Exit Sub
                End If
                obj.Arr_Location = New List(Of clsDALocation_detail)
                For i As Integer = 0 To arrUserTypeLocation.Count - 1
                    Dim objtr As New clsDALocation_detail
                    objtr.Location = arrUserTypeLocation(i)
                    obj.Arr_Location.Add(objtr)
                Next
                If clsDAArrear.SaveData(obj, isNewEntry) Then
                    If Not isPost Then
                        clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
                    End If
                    LoadData(obj.document_code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            objpd = Nothing
            obj = Nothing
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub
    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim obj As New clsDAArrear()
        Try
            Addnew()
            obj = clsDAArrear.GetData(strCode, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.document_code) > 0 Then
                isInsideLoadData = True
                isNewEntry = False
                txtDocNo.Value = obj.document_code
                txtDate.Value = obj.document_date
                txtDAArrear.Text = obj.DA_Arrear
                fndFromPeriod.Value = obj.Pay_Period
                txtApplicabbleDate.Value = obj.Applicable_date
                UsLock1.Status = ERPTransactionStatus.Pending
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                btnPost.Enabled = True
                BtnGo.Enabled = False
                If obj.Status = 1 Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If
                If obj.ArrD IsNot Nothing AndAlso obj.ArrD.Count > 0 Then
                    For Each objtr As ClsDAArrearDetail In obj.ArrD
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colApply).Value = "Yes"
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEMPCode).Value = objtr.Emp_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colempname).Value = objtr.Emp_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBasic).Value = objtr.Basic
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDA).Value = objtr.DA
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDAArrearPer).Value = objtr.DA_Arrear
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEPF).Value = objtr.PF
                    Next
                End If
                Dim dtPayPeriod As DataTable = clsDBFuncationality.GetDataTable("select Pay_Period from TSPL_DAAREAR_PAYPERIOD_DETAIL  where Document_Code='" + obj.document_code + "'")
                Dim Arr_Prod As New ArrayList
                For Each dr As DataRow In dtPayPeriod.Rows
                    Arr_Prod.Add(dr("Pay_Period"))
                Next
                txtmultPayperiod.arrValueMember = Arr_Prod

                Dim dtLocation As DataTable = clsDBFuncationality.GetDataTable("select Location from TSPL_DAAREAR_Location_DETAIL  where Document_Code='" + obj.document_code + "'")
                Dim Arr_Loc As New ArrayList
                For Each dr As DataRow In dtLocation.Rows
                    Arr_Loc.Add(dr("Location"))
                Next
                txtmulLocation.arrValueMember = Arr_Loc
                Dim PMCond As String = "''"
                Dim payPeriod As String = clsCommon.GetMulcallString(txtmultPayperiod.arrValueMember)
                Dim stringArray As String() = payPeriod.Replace("'", "").Split(","c)
                Dim stringList As List(Of String) = New List(Of String)(stringArray)
                If stringList IsNot Nothing AndAlso stringList.Count > 0 Then
                    Dim strPeriod As String = Nothing
                    Dim PayPeriodName As String = Nothing
                    For Each StrP As String In stringList
                        If clsCommon.myLen(strPeriod) > 0 Then
                            strPeriod += ",[" + StrP + "] As [" + StrP + "]"
                            PayPeriodName += ",[" + StrP + "] "
                        Else
                            strPeriod = "[" + StrP + "] As [" + StrP + "]"
                            PayPeriodName = "[" + StrP + "] "
                        End If
                    Next
                    Dim DetailData As String = clsSalaryGeneration.GetArrearDetailData(txtmultPayperiod.arrValueMember, txtmulLocation.arrValueMember, Nothing, PMCond, False)

                    Dim Qry As String = "SELECT EMP_CODE, EMPLOYEE_NAME, 
                       " + strPeriod + "
                FROM (
                    SELECT xy.EMP_CODE, 
                           xy.EMPLOYEE_NAME, 
                           xy.PAY_PERIOD_CODE, 
                           SUM(xy.basic) AS basic
                    FROM (" + DetailData + ")AS xy
                    GROUP BY xy.EMP_CODE, xy.EMPLOYEE_NAME, xy.PAY_PERIOD_CODE
                ) AS SourceTable
                PIVOT (
                    SUM(basic) 
                    FOR PAY_PERIOD_CODE IN (" + PayPeriodName + ") 
                ) AS PivotTable
                ORDER BY CAST(EMP_CODE AS INT);
                "
                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(Qry)
                    gv2.DataSource = Nothing
                    gv2.Rows.Clear()
                    gv2.Columns.Clear()
                    gv2.GroupDescriptors.Clear()
                    gv2.MasterTemplate.SummaryRowsBottom.Clear()
                    gv2.DataSource = dt2
                    gv2.BestFitColumns()
                    gv2.EnableFiltering = True
                    gv2.ShowGroupPanel = False
                    If dt2 Is Nothing OrElse dt2.Rows.Count <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Dim whrClas As String = ""
            'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            '    whrClas = " Location in (" + objCommonVar.strCurrUserLocations + ") "
            'End If
            Dim qry As String = " select Document_Code as Code,convert(varchar,Document_Date,103)DocumentDate,Case when status=0 then 'Pending' else 'Approved' end as 'Status' from TSPL_DA_Arrear_Header "
            LoadData(clsCommon.ShowSelectForm("OutgoingQC", qry, "Code", "", txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_DA_Arrear_Header where Document_Code ='" + txtDocNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If myMessages.postConfirm() Then
                If (clsDAArrear.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub funDelete()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (clsDAArrear.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    Addnew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsDAArrear.ReverseAndUnpost(txtDocNo.Value)
                    clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub frmArrear_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "sirc"
                frm.strCode = "sireversandcreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
            End If
        End If
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Document not found")
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date: " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")))
            ' arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If txtmulLocation.arrValueMember IsNot Nothing AndAlso txtmulLocation.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtmulLocation.arrValueMember)
                arrHeader.Add((" Location : " + strLocationName + " "))
            Else
                arrHeader.Add((" Location : All"))
            End If
            If txtmultPayperiod.arrValueMember IsNot Nothing AndAlso txtmultPayperiod.arrValueMember.Count > 0 Then
                Dim strCustomerCat As String = clsCommon.GetMulcallStringWithComma(txtmultPayperiod.arrValueMember)
                arrHeader.Add((" Pay Period : " + strCustomerCat + " "))
            End If

            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Arrear Summary ", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        print(EnumExportTo.PDF)
    End Sub
    Sub printDetail(ByVal exporter As EnumExportTo)
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Document not found")
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date: " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")))
            'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If txtmulLocation.arrValueMember IsNot Nothing AndAlso txtmulLocation.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtmulLocation.arrValueMember)
                arrHeader.Add((" Location : " + strLocationName + " "))
            Else
                arrHeader.Add((" Location : All"))
            End If
            If txtmultPayperiod.arrValueMember IsNot Nothing AndAlso txtmultPayperiod.arrValueMember.Count > 0 Then
                Dim strCustomerCat As String = clsCommon.GetMulcallStringWithComma(txtmultPayperiod.arrValueMember)
                arrHeader.Add((" Pay Period : " + strCustomerCat + " "))
            End If

            transportSql.applyExportTemplate(gv2, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Arrear Detail ", gv2, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        printDetail(EnumExportTo.PDF)
    End Sub

    Private Sub fndFromPeriod__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndFromPeriod._MYValidating
        Try
            Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
           & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
            fndFromPeriod.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", fndFromPeriod.Value, "", isButtonClicked)
            ' lblFromPeriodName.Text = clsPayPeriodMaster.GetName(fndFromPeriod.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Document No")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtDocNo.Value, " Document_Code", "TSPL_DA_Arrear_Header", "TSPL_DA_Arrear_Detail")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
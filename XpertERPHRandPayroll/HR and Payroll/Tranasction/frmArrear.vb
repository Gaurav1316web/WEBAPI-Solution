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
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtTodate.Value = clsCommon.GETSERVERDATE()
        gv1.DataSource = Nothing
        LoadBlankGrid()
        gv2.DataSource = Nothing
        txtmultPayperiod.arrValueMember = Nothing
        txtmultPayperiod.arrValueMember = Nothing
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
            Dim fromDate As Date = Date.Parse(txtFromDate.Value)
            Dim toDate As Date = Date.Parse(txtTodate.Value)
            Dim str As String = clsSalaryGeneration.GetArrearData(txtmultPayperiod.arrValueMember, txtmulLocation.arrValueMember, Nothing, PMCond, False, fromDate, toDate)
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
        Dim payPeriod As String = clsCommon.GetMulcallString(txtmultPayperiod.arrValueMember)
        Dim stringArray As String() = payPeriod.Replace("'", "").Split(","c)
        Dim stringList As List(Of String) = New List(Of String)(stringArray)
        If stringList IsNot Nothing AndAlso stringList.Count > 0 Then
            Dim strPeriod As String = Nothing
            For Each Str As String In stringList
                If clsCommon.myLen(strPeriod) > 0 Then
                    strPeriod += ",'" + Str + "' As [" + Str + "]"
                Else
                    strPeriod = "'" + Str + "' As [" + Str + "]"
                End If
            Next

        End If
    End Sub
    Private Sub txtmulLocation__My_Click(sender As Object, e As EventArgs) Handles txtmulLocation._My_Click
        Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],TSPL_Location_MASTER.Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Hoadd1 ,Hoadd2,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Seg.Description as [Location Segment Description],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],tspl_location_master.Created_By as [Created By],tspl_location_master.Created_Date as [Created Date],tspl_location_master.Modify_By as [Modify By],tspl_location_master.Modify_Date as [Modify Date],tspl_location_master.Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],CST_No as [CST No],Phone1,Phone2,stock_transfer_ac as [Stock Tranfer A/C],Loss_Ac as [Loss A/C] ,Is_Consumption_Location as [Is Consumption Location],Is_Section as [Is Section],Section_Code as [Section Code],Is_Sub_Location as [Is Sub Location],Main_Location_Code as [Main Location Code],IsSubLocationWise as [Is Sub Location Wise] from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code   where Location_Type='Physical' "
        txtmulLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Code", txtmulLocation.arrValueMember, txtmulLocation.arrDispalyMember)
    End Sub
    Private Sub frmArrear_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        Addnew()
        LoadBlankGrid()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)
        coll.Add("Document_Code", "varchar(30) NOT NULL Primary Key")
        coll.Add("Document_Date", "datetime  NULL")
        coll.Add("DA_Arrear", "NUMERIC(18,2)  NULL")
        coll.Add("Pay_Period", "varchar(12) null")
        coll.Add("Location", "varchar(12) null")
        coll.Add("From_Date", "datetime  NULL")
        coll.Add("To_Date", "datetime  NULL")
        coll.Add("Created_By", "varchar(12)   NULL")
        coll.Add("Created_Date", "datetime   NULL")
        coll.Add("Modify_By", "varchar(12)   NULL")
        coll.Add("Modify_Date", "datetime   NULL")
        coll.Add("Posted_By", "varchar(12)   NULL")
        coll.Add("Posted_Date", "datetime   NULL")
        coll.Add("Status", "integer not null default 0")
        clsCommonFunctionality.CreateOrAlterTable(False, "TSPL_DA_Arrear_Header", coll, "", True)

        coll = New Dictionary(Of String, String)
        coll.Add("Document_Code", "varchar(30) null References TSPL_DA_Arrear_Header(Document_Code)")
        coll.Add("Apply", "char(1) NULL")
        coll.Add("Emp_Code", "varchar(12) null")
        coll.Add("Basic", "decimal (18,2)  NULL")
        coll.Add("DA", "decimal (18,2)  NULL")
        coll.Add("DA_Arrear", "decimal (18,2) NULL")
        coll.Add("PF", "decimal (18,2) NULL")
        clsCommonFunctionality.CreateOrAlterTable(False, "TSPL_DA_Arrear_Detail", coll, "", True)
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
            If AllowFutureDateTransaction(txtFromDate.Value, Nothing) = False Then
                txtFromDate.Focus()
                Return False
            End If
            If AllowFutureDateTransaction(txtTodate.Value, Nothing) = False Then
                txtTodate.Focus()
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
                obj.Location = clsCommon.myCstr(txtmulLocation.arrValueMember)
                obj.Pay_Period = clsCommon.myCstr(txtmultPayperiod.arrValueMember)
                obj.Fromdate = clsCommon.myCDate(txtFromDate.Value)
                obj.Todate = clsCommon.myCDate(txtTodate.Value)
                obj.DA_Arrear = clsCommon.myCdbl(txtDAArrear.Text)
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
                'txtmulLocation.arrValueMember = obj.Location
                'txtmultPayperiod.arrValueMember = obj.Pay_Period
                txtFromDate.Value = obj.Fromdate
                txtTodate.Value = obj.Todate
                UsLock1.Status = ERPTransactionStatus.Pending
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                btnPost.Enabled = True
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
            Dim qry As String = "   	select Document_Code as Code,convert(varchar,Document_Date,103)DocumentDate,Case when status=0 then 'Pending' else 'Approved' end as 'Status',convert(varchar,from_date,103)FromDate,convert(varchar,to_date,103)ToDate from TSPL_DA_Arrear_Header "
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
End Class
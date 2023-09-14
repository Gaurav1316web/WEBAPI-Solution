Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class FrmEmpIncrement
    Inherits FrmMainTranScreen
    Const colLineNo As String = "LineNo"
    Const colpayHeadCode As String = "PayHeadCode"
    Const colpayHeadName As String = "PayHeadName"
    Const colPayHeadFormula As String = "Formula"
    Const colRateAmount As String = "RateAmount"
    Const colHiddenComponent As String = "HiddenComponent"
    Const colMax_Amount As String = "colMax_Amount"
    Const colPAYPERIOD_Amount As String = "colPAYPERIOD_Amount"
    Const colIncrementRate_Amt As String = "colIncrementRate_Amt"
    Const colIncrementAmt As String = "colIncrementAmt"
    Const colIncrementedRate_Amt As String = "colIncrementedRate_Amt"
    Const colTotalExperience As String = "colTotalExperience"
    Const colPayHeadType As String = "colPayHeadType"
    Const colIncrementType As String = "colIncrementType"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    ''====created by Shivani Tyagi
    ''===changesby shivani [BM00000007655]
    Dim obj As New XpertERPEngine.ClsEmpIncrement
    Private ObjList As New List(Of XpertERPEngine.clsEmpIncrementDetail)
    Dim obj2 As New XpertERPEngine.clsEmployeeSalary
    Private ObjList1 As New List(Of XpertERPEngine.clsEmpSalaryPayHeadDetails)
    Dim isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Public LastApplicableFrom As Date? = Nothing
    Public LastArrearFrom As Date? = Nothing

    Sub LoadGridColumns()
        gvSalary.Rows.Clear()
        gvSalary.Columns.Clear()

        Dim LineNo As New GridViewTextBoxColumn
        Dim payHeadCode As New GridViewTextBoxColumn
        Dim PayHeadName As New GridViewTextBoxColumn
        Dim Formula As New GridViewTextBoxColumn
        Dim RateAmount As New GridViewDecimalColumn
        Dim IsHiddenComponent As New GridViewCheckBoxColumn
        Dim Max_Amount As New GridViewDecimalColumn
        Dim PAYPERIOD_Amount As New GridViewDecimalColumn
        Dim IncrementRate_Amt As New GridViewDecimalColumn
        Dim IncrementAmt As New GridViewDecimalColumn
        Dim IncrementedRate_Amt As New GridViewDecimalColumn
        Dim TotalExperience As New GridViewDecimalColumn
        Dim payHeadType As New GridViewTextBoxColumn
        Dim IncrementType As New GridViewTextBoxColumn



        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 100
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSalary.Columns.Add(LineNo)

        payHeadCode.FormatString = ""
        payHeadCode.HeaderText = "Pay Head Code"
        payHeadCode.Name = colpayHeadCode
        payHeadCode.Width = 100
        payHeadCode.ReadOnly = True
        payHeadCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSalary.Columns.Add(payHeadCode)

        PayHeadName.FormatString = ""
        PayHeadName.HeaderText = "Pay Head Name"
        PayHeadName.Name = colpayHeadName
        PayHeadName.Width = 100
        PayHeadName.ReadOnly = True
        PayHeadName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSalary.Columns.Add(PayHeadName)

        Formula.FormatString = ""
        Formula.HeaderText = "Formula"
        Formula.Name = colPayHeadFormula
        Formula.Width = 100
        Formula.ReadOnly = True
        Formula.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSalary.Columns.Add(Formula)

        RateAmount.FormatString = ""
        RateAmount.HeaderText = "Rate/Amount"
        RateAmount.Name = colRateAmount
        RateAmount.Width = 100
        RateAmount.ReadOnly = True
        RateAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSalary.Columns.Add(RateAmount)

        IsHiddenComponent = New GridViewCheckBoxColumn()
        IsHiddenComponent.HeaderText = "Is Hidden Component"
        IsHiddenComponent.Name = colHiddenComponent
        IsHiddenComponent.Width = 50
        IsHiddenComponent.ReadOnly = True
        gvSalary.Columns.Add(IsHiddenComponent)

        Max_Amount.FormatString = ""
        Max_Amount.HeaderText = "Maximum Amount Limit"
        Max_Amount.Name = colMax_Amount
        Max_Amount.Width = 100
        Max_Amount.ReadOnly = True
        Max_Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSalary.Columns.Add(Max_Amount)

        PAYPERIOD_Amount.FormatString = ""
        PAYPERIOD_Amount.HeaderText = "Last Amount"
        PAYPERIOD_Amount.Name = colPAYPERIOD_Amount
        PAYPERIOD_Amount.Width = 100
        PAYPERIOD_Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        PAYPERIOD_Amount.ReadOnly = True
        gvSalary.Columns.Add(PAYPERIOD_Amount)


        IncrementType.FormatString = ""
        IncrementType.HeaderText = "Increment Type"
        IncrementType.Name = colIncrementType
        IncrementType.Width = 100
        IncrementType.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        IncrementType.TextImageRelation = TextImageRelation.TextBeforeImage
        IncrementType.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        IncrementType.ReadOnly = False
        IncrementType.IsVisible = False
        gvSalary.Columns.Add(IncrementType)

        IncrementRate_Amt.FormatString = ""
        IncrementRate_Amt.HeaderText = "Increment Rate"
        IncrementRate_Amt.Name = colIncrementRate_Amt
        IncrementRate_Amt.Width = 100
        IncrementRate_Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        IncrementRate_Amt.ReadOnly = False
        gvSalary.Columns.Add(IncrementRate_Amt)

        IncrementAmt.FormatString = ""
        IncrementAmt.HeaderText = "Increment Amount"
        IncrementAmt.Name = colIncrementAmt
        IncrementAmt.Width = 100
        IncrementAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        IncrementAmt.ReadOnly = False
        gvSalary.Columns.Add(IncrementAmt)

        IncrementedRate_Amt.FormatString = ""
        IncrementedRate_Amt.HeaderText = "Incremented Rate/Amount"
        IncrementedRate_Amt.Name = colIncrementedRate_Amt
        IncrementedRate_Amt.Width = 100
        IncrementedRate_Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        IncrementedRate_Amt.ReadOnly = False
        gvSalary.Columns.Add(IncrementedRate_Amt)

        TotalExperience.FormatString = ""
        TotalExperience.HeaderText = "Total Experience"
        TotalExperience.Name = colTotalExperience
        TotalExperience.Width = 100
        TotalExperience.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        TotalExperience.ReadOnly = False
        gvSalary.Columns.Add(TotalExperience)

        gvSalary.EnableSorting = False


    End Sub
    Public Function importExcelSalary(ByVal gv As RadGridView, ByVal ParamArray fieldNames As String()) As Boolean
        Try
            If Not transportSql.LoadDocument(gv, "", fieldNames) Then
                Return False
            End If
            Dim fieldCount As Integer = fieldNames.Length
            Dim strfields As String = ""
            For Each field As String In fieldNames
                strfields = strfields + field + ","
            Next
            Dim qry As String = "select EMP_CODE,MAX(INCREMENT_CODE) AS INCREMENT_CODE,MAX(REVISION_NO) AS REVISION_NO,max(EMP_SAL_CODE) as EMP_SAL_CODE ,Location_Code,max(Location_Desc) as Location_Desc,Devision_Code,max(Devision_Name) as Devision_Name,max(convert(varchar,APPLICABLE_FROM,103 )) as APPLICABLE_FROM , max(SALARY_STRUCTURE_CODE ) as SALARY_STRUCTURE_CODE"
            qry += " from TSPL_EMPLOYEE_INCREMENT_HEAD GROUP BY EMP_CODE,Location_Code,Devision_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim strSelect As String = clsEmployeeSalary.GetPayHeadCodeString(Me.lblSalaryStructCode.Text)
            strSelect = strSelect.Replace("[", "")
            strSelect = strSelect.Replace("]", "")
            Dim arrs() As String
            arrs = strSelect.Split(",")
            For Each strss As String In arrs
                If Not gv.Columns.Contains(strss) Then
                    gv.Columns.Add(strss)
                End If
            Next
            If gv.ColumnCount > 1 Then
                Dim i As Integer = 0
                Dim arr As ArrayList = New ArrayList()
                Dim arrExtraPayHead As ArrayList = New ArrayList()
                For Each GC As GridViewColumn In gv.Columns
                    arr.Add(GC.HeaderText.ToString.Replace("_", "."))
                Next
                For Each field As String In fieldNames
                    If Array.IndexOf(fieldNames, field) > 1 Then
                        arrExtraPayHead.Add(field)
                    End If
                    i = i + 1
                Next
                ' adding extra columns 
                For Each payhead As String In arrExtraPayHead
                    If Not gv.Columns.Contains(payhead.Replace(".", "_")) Then
                        gv.Columns.Add(payhead.Replace(".", "_"))
                        '' update amount in extra payheads
                        For Each item As GridViewRowInfo In gv.Rows
                            Dim dr() As DataRow = dt.Select("Emp_Code='" & item.Cells("Emp Code").Value & "'")
                            If Not IsNothing(dr) AndAlso dr.Length > 0 Then
                                item.Cells(payhead.Replace(".", "_")).Value = clsEmployeeSalary.getPayHeadAmount(item.Cells("Emp Code").Value, dr(0)("Salary_Structure_Code"), payhead, item.Cells("APPLICABLE FROM").Value, "")
                            End If
                        Next
                    End If
                Next
            Else
                Throw New Exception("Excel Sheet is not in expected format. It should have the columns named - " + strfields)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Private Sub rmClose_Click(sender As Object, e As EventArgs) Handles rmClose.Click
        Me.Close()
    End Sub


    Private Sub fndEmpCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndEmpCode._MYValidating
        Try
            Dim whrcls As String = Nothing
            Dim LocCode As String = Nothing
            If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
                LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
                If clsCommon.myLen(LocCode) > 0 Then
                    whrcls = " LOCATION_CODE='" + LocCode + "'"
                End If
            End If
            Dim qry As String = "SELECT EMP_CODE as Code,EMP_Name as Name,Designation,LOCATION_CODE ,DEPARTMENT_CODE ,DEVISION_CODE  FROM TSPL_EMPLOYEE_MASTER "
            fndEmpCode.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", whrcls, fndEmpCode.Value, "", isButtonClicked)
            Dim clsemp As clsEmployeeMaster
            clsemp = clsEmployeeMaster.FinderForEmployee(fndEmpCode.Value, Nothing)
            If Not clsemp Is Nothing Then
                lblEmpName.Text = clsemp.Emp_Name
            End If
            If isNewEntry = True Then
                Try
                    Dim Current_Rev_No As Integer = 0
                    qry = "select (coalesce(max(revision_no),0)) AS revision_no from TSPL_EMPLOYEE_SALARY where EMP_CODE='" & Me.fndEmpCode.Value & "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt.Rows.Count > 0 AndAlso clsCommon.myCdbl(dt.Rows(0).Item("revision_no")) > 0 Then
                        Current_Rev_No = clsCommon.myCdbl(dt.Rows(0).Item("revision_no"))
                    Else
                        clsCommon.MyMessageBoxShow("Salary not define for selected employee.")
                        Exit Sub
                    End If
                    Me.txtRevisionNo.Text = Current_Rev_No 'clsDBFuncationality.GetDataTable("").Rows(0).Item("revision_no")
                    lblSalaryStructCode.Text = clsDBFuncationality.GetDataTable("select (coalesce(max(SALARY_STRUCTURE_CODE),'')) AS SALARY_STRUCTURE_CODE from TSPL_EMPLOYEE_SALARY where EMP_CODE='" & Me.fndEmpCode.Value & "' and REVISION_NO='" & Current_Rev_No & "'").Rows(0).Item("SALARY_STRUCTURE_CODE")
                    lblSalStructName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select SALARY_STRUCTURE_NAME  from TSPL_SALARY_STRUCTURE where SALARY_STRUCTURE_CODE='" & Me.lblSalaryStructCode.Text & "'"))
                    lblSalaryCode.Text = clsDBFuncationality.GetDataTable("select (coalesce(max(EMP_SAL_CODE),'')) AS EMP_SAL_CODE from TSPL_EMPLOYEE_SALARY where EMP_CODE='" & Me.fndEmpCode.Value & "' and REVISION_NO='" & Current_Rev_No & "'").Rows(0).Item("EMP_SAL_CODE")
                    LocationCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select LOCATION_CODE  from TSPL_EMPLOYEE_MASTER where EMP_CODE='" & Me.fndEmpCode.Value & "'"))
                    lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc  from TSPL_LOCATION_MASTER  where  Location_Code='" & Me.LocationCode.Value & "'"))
                    lblDivisionCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DEVISION_CODE  from TSPL_EMPLOYEE_MASTER  where Emp_Code='" & Me.fndEmpCode.Value & "'"))
                    lblDivisionName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DEVISION_NAME  from TSPL_DEVISION_MASTER  where DEVISION_CODE='" & Me.lblDivisionCode.Text & "'"))
                    Show_salary_struct(lblSalaryCode.Text, NavigatorType.Current)
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception
        End Try
    End Sub
    Sub Show_salary_struct(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Dim obj1 As clsEmployeeSalary
        obj1 = clsEmployeeSalary.GetData(strCode, NavTyep)
        If (obj1 IsNot Nothing AndAlso clsCommon.myLen(obj1.EMP_SAL_CODE) > 0) Then

            Dim ii As Int16 = 0
            LoadGridColumns()

            lblSalaryCode.Text = obj1.EMP_SAL_CODE
            LastApplicableFrom = Nothing
            LastArrearFrom = Nothing
            ValidateDate(lblSalaryCode.Text)
            If (clsEmployeeSalary.ObjList IsNot Nothing AndAlso clsEmployeeSalary.ObjList.Count > 0) Then
                For Each obj As clsEmpSalaryPayHeadDetails In clsEmployeeSalary.ObjList
                    gvSalary.Rows.AddNew()

                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colLineNo).Value = obj.Line_No
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colpayHeadCode).Value = obj.PayHeadCode
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colpayHeadName).Value = obj.PayHeadName
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colPayHeadFormula).Value = obj.Formula
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colRateAmount).Value = obj.Rate_Amount
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colHiddenComponent).Value = obj.IsHiddenComponent
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colMax_Amount).Value = obj.MAX_AMOUNT
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colPAYPERIOD_Amount).Value = obj.Rate_Amount
                    If clsPayHeadDefinitions.CheckIncrementPayhead(obj.PayHeadCode) = True Then
                        gvSalary.Rows(gvSalary.Rows.Count - 1).IsVisible = True
                    Else
                        gvSalary.Rows(gvSalary.Rows.Count - 1).IsVisible = False
                    End If
                Next
            Else
                gvSalary.Rows.AddNew()
            End If

        End If
    End Sub
    Public Sub ValidateDate(ByVal Emp_Sal_Code As String)
        Dim qry As String = ""
        qry = "select Applicable_From,Arrear_From from TSPL_EMPLOYEE_INCREMENT_HEAD where EMP_SAL_CODE_NEW='" & Emp_Sal_Code & "' AND INCREMENT_CODE NOT IN ('" & fndIncrementCode.Value & "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            LastApplicableFrom = dt.Rows(0).Item("Applicable_From")
            LastArrearFrom = dt.Rows(0).Item("Arrear_From")
        Else
            LastApplicableFrom = Nothing
            LastArrearFrom = Nothing
        End If
    End Sub
    Public Sub validatedateimport(ByVal EMPCODE As String)
        Dim Current_Rev_No As Integer = 0
        Dim qry As String = "select (coalesce(max(revision_no),0)) AS revision_no from TSPL_EMPLOYEE_SALARY where EMP_CODE='" & EMPCODE & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 AndAlso clsCommon.myCdbl(dt.Rows(0).Item("revision_no")) > 0 Then
            Current_Rev_No = clsCommon.myCdbl(dt.Rows(0).Item("revision_no"))
        Else
            clsCommon.MyMessageBoxShow("Salary not define for selected employee.")
            Exit Sub
        End If
        Dim Emp_Sal_Code As String = clsCommon.myCstr(clsDBFuncationality.GetDataTable("select (coalesce(max(EMP_SAL_CODE),'')) AS EMP_SAL_CODE from TSPL_EMPLOYEE_SALARY where EMP_CODE='" & EMPCODE & "' and REVISION_NO='" & Current_Rev_No & "'").Rows(0).Item("EMP_SAL_CODE"))
        qry = "select Applicable_From,Arrear_From from TSPL_EMPLOYEE_INCREMENT_HEAD where EMP_SAL_CODE_NEW='" & Emp_Sal_Code & "'"
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            LastApplicableFrom = dt.Rows(0).Item("Applicable_From")
            LastArrearFrom = dt.Rows(0).Item("Arrear_From")
        Else
            LastApplicableFrom = Nothing
            LastArrearFrom = Nothing
        End If
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        funReset()
    End Sub
    Public Function Save() As Boolean
        '' Ticket No : BM00000007939,BM00000007927,BM00000007928 by Panch Raj
        Try
            If AllowToSave() = False Then
                Return False
            End If
            Dim obj As New ClsEmpIncrement
            obj.INCREMENT_CODE = fndIncrementCode.Value
            obj.INCREMENT_DATE = txtIncrementDate.Text
            obj.EMP_SAL_CODE = lblSalaryCode.Text
            obj.EMP_CODE = fndEmpCode.Value
            obj.REVISION_NO = clsCommon.myCdbl(Me.txtRevisionNo.Text)
            obj.APPLICABLE_FROM = clsCommon.GetPrintDate(dtpApplicableFrom.Value, "dd/MMM/yyyy")
            obj.SALARY_STRUCTURE_CODE = lblSalaryStructCode.Text
            obj.location_Code = LocationCode.Value
            obj.Location_Desc = lblLocationName.Text
            obj.DEVISION_CODE = lblDivisionCode.Text
            obj.Devision_Name = lblDivisionName.Text

            obj.ARREAR_FROM = clsCommon.GetPrintDate(dtpArrearFrom.Value, "dd/MMM/yyyy")

            Dim obj1 As clsEmpIncrementDetail
            ObjList = New List(Of clsEmpIncrementDetail)
            For Each grow As GridViewRowInfo In gvSalary.Rows
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)) > 0 Then
                    obj1 = New clsEmpIncrementDetail()

                    obj1.PayHeadCode = clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)
                    obj1.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    obj1.PayHeadName = clsCommon.myCstr(grow.Cells(colpayHeadName).Value)
                    obj1.Formula = clsCommon.myCstr(grow.Cells(colPayHeadFormula).Value)
                    obj1.Rate_Amount = clsCommon.myCdbl(grow.Cells(colRateAmount).Value)
                    obj1.IsHiddenComponent = clsCommon.myCdbl(grow.Cells(colHiddenComponent).Value)
                    obj1.MAX_AMOUNT = clsCommon.myCdbl(grow.Cells(colMax_Amount).Value)
                    obj1.PAYPERIOD_AMOUNT = clsCommon.myCdbl(grow.Cells(colPAYPERIOD_Amount).Value)
                    obj1.IncrementRate_Amt = clsCommon.myCdbl(grow.Cells(colIncrementRate_Amt).Value)
                    obj1.IncrementAmt = clsCommon.myCdbl(grow.Cells(colIncrementAmt).Value)
                    obj1.IncrementedRate_Amt = clsCommon.myCdbl(grow.Cells(colIncrementedRate_Amt).Value)
                    obj1.TotalExperience = clsCommon.myCstr(grow.Cells(colTotalExperience).Value)
                    obj1.Increment_Type = clsCommon.myCstr(grow.Cells(colIncrementType).Value)
                    ObjList.Add(obj1)
                End If
            Next

            If (obj.SaveData(obj, ObjList, isNewEntry, clsCommon.myCstr(fndIncrementCode.Value))) Then
                LoadData(obj.INCREMENT_CODE, NavigatorType.Current)
                Return True
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            End If
            Return False

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return False
    End Function
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub
    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (ClsEmpIncrement.PostData(fndIncrementCode.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Posted")
                    LoadData(fndIncrementCode.Value, NavigatorType.Current)
                    'Save1()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        '' Ticket No:BM00000007692 by Panch raj
        If clsCommon.myLen(fndEmpCode.Value) <= 0 Then
            RadMessageBox.Show("Please select Employee Code")
            fndEmpCode.Focus()
            Return False
        End If
        If clsCommon.myLen(lblSalaryStructCode.Text) <= 0 Then
            RadMessageBox.Show("Salary Structure is blank")
            fndEmpCode.Focus()
            Return False
        End If
        If dtpArrearFrom.Value > dtpApplicableFrom.Value Then
            RadMessageBox.Show("Arrear from date must be less or equal to applicable from date!")
            dtpArrearFrom.Focus()
            Return False
        End If
        ValidateDate(lblSalaryCode.Text)
        If Not LastApplicableFrom Is Nothing Then
            If CDate(clsCommon.GetPrintDate(dtpApplicableFrom.Value, "dd/MMM/yyyy")) <= CDate(clsCommon.GetPrintDate(LastApplicableFrom, "dd/MMM/yyyy")) Then
                clsCommon.MyMessageBoxShow("Applicable from date must be greater than last increment applicable from date (" & LastApplicableFrom & ")")
                Return False
            End If
        End If
        If Not LastArrearFrom Is Nothing Then
            If CDate(clsCommon.GetPrintDate(dtpArrearFrom.Value, "dd/MMM/yyyy")) <= CDate(clsCommon.GetPrintDate(LastApplicableFrom, "dd/MMM/yyyy")) Then
                clsCommon.MyMessageBoxShow("Arrear from date must be greater than last increment applicable from date (" & LastApplicableFrom & ")")
                Return False
            End If
        End If

        Return True
    End Function
    Sub Save1()
        Dim isnewentry As Boolean = True
        Dim obj2 As New clsEmployeeSalary
        obj2.EMP_SAL_CODE = Nothing
        obj2.EMP_CODE = fndEmpCode.Value
        obj2.REVISION_NO = clsCommon.myCdbl(Me.txtRevisionNo.Text) + 1
        obj2.APPLICABLE_FROM = clsCommon.GetPrintDate(dtpApplicableFrom.Value, "dd/MMM/yyyy")
        obj2.SALARY_STRUCT_CODE = lblSalaryStructCode.Text

        Dim objt As clsEmpSalaryPayHeadDetails
        ObjList1 = New List(Of clsEmpSalaryPayHeadDetails)
        For Each grow As GridViewRowInfo In gvSalary.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)) > 0 Then
                objt = New clsEmpSalaryPayHeadDetails()

                objt.PayHeadCode = clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)
                objt.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                objt.PayHeadName = clsCommon.myCstr(grow.Cells(colpayHeadName).Value)
                objt.Formula = clsCommon.myCstr(grow.Cells(colPayHeadFormula).Value)
                objt.Rate_Amount = IIf(clsCommon.myCdbl(grow.Cells(colIncrementedRate_Amt).Value) > 0, clsCommon.myCdbl(grow.Cells(colIncrementedRate_Amt).Value), clsCommon.myCdbl(grow.Cells(colRateAmount).Value))
                objt.IsHiddenComponent = clsCommon.myCdbl(grow.Cells(colHiddenComponent).Value)
                objt.MAX_AMOUNT = clsCommon.myCdbl(grow.Cells(colMax_Amount).Value)
                'objt.PAYPERIOD_AMOUNT = clsCommon.myCdbl(grow.Cells(colIncrementedRate_Amt).Value)

                ObjList1.Add(objt)
            End If
        Next
        If (obj2.SaveData(obj2, ObjList1, isnewentry, obj2.EMP_SAL_CODE)) Then
            LoadData(obj.EMP_SAL_CODE, NavigatorType.Current)
            If (clsEmployeeSalary.PostData(obj2.EMP_SAL_CODE, True)) Then
                'common.clsCommon.MyMessageBoxShow("Successfully Posted")
                LoadData(obj.EMP_SAL_CODE, NavigatorType.Current)
            End If
        End If


    End Sub
    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            End If
        End If
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.fndIncrementCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
    Sub funDelete()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (ClsEmpIncrement.DeleteData(fndIncrementCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Sub funReset()
        isNewEntry = True
        fndIncrementCode.MyReadOnly = False
        fndIncrementCode.Value = Nothing
        fndIncrementCode.Focus()
        fndEmpCode.Value = ""
        lblEmpName.Text = ""
        dtpApplicableFrom.Text = ""
        txtRevisionNo.Text = ""
        lblSalaryCode.Text = ""
        lblSalaryStructCode.Text = ""
        lblSalStructName.Text = ""
        txtIncrementDate.Text = ""
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            LocationCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocation.Text = clsLocation.GetName(LocationCode.Value, Nothing)
        Else
            LocationCode.Value = ""
            lblLocation.Text = ""
        End If
        lblDivisionCode.Text = ""
        lblDivisionName.Text = ""
        btnsave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        dtpApplicableFrom.Value = clsCommon.GETSERVERDATE
        dtpArrearFrom.Value = dtpApplicableFrom.Value
        Me.gvSalary.Rows.Clear()
        Me.gvSalary.Rows.AddNew()
    End Sub
    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        funDelete()
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'funReset()
        obj = ClsEmpIncrement.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.INCREMENT_CODE) > 0) Then
            isInsideLoadData = True
            isNewEntry = False
            btnsave.Text = "Update"
            If obj.POSTED = True Then
                btnsave.Enabled = False
                btnPost.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            Dim ii As Int16 = 0
            LoadGridColumns()

            fndIncrementCode.Value = obj.INCREMENT_CODE
            txtIncrementDate.Text = obj.INCREMENT_DATE
            fndEmpCode.Value = clsCommon.myCstr(obj.EMP_CODE)
            lblEmpName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where Emp_code='" + fndEmpCode.Value + "'"))
            lblSalaryStructCode.Text = obj.SALARY_STRUCTURE_CODE
            lblSalStructName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select SALARY_STRUCTURE_NAME  from TSPL_SALARY_STRUCTURE where SALARY_STRUCTURE_CODE='" & Me.lblSalaryStructCode.Text & "'"))
            lblSalaryCode.Text = obj.EMP_SAL_CODE
            txtRevisionNo.Text = clsCommon.myCdbl(obj.REVISION_NO)
            dtpApplicableFrom.Value = obj.APPLICABLE_FROM
            LocationCode.Value = obj.location_Code
            lblLocationName.Text = obj.Location_Desc
            lblDivisionCode.Text = obj.DEVISION_CODE
            lblDivisionName.Text = obj.Devision_Name
            If Not obj.ARREAR_FROM Is Nothing Then
                dtpArrearFrom.Value = obj.ARREAR_FROM
            End If
            '' assign last applicable from date
            LastApplicableFrom = Nothing
            LastArrearFrom = Nothing
            ValidateDate(lblSalaryCode.Text)

            If (ClsEmpIncrement.ObjList IsNot Nothing AndAlso ClsEmpIncrement.ObjList.Count > 0) Then
                For Each obj As clsEmpIncrementDetail In ClsEmpIncrement.ObjList
                    gvSalary.Rows.AddNew()

                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colLineNo).Value = obj.Line_No
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colpayHeadCode).Value = obj.PayHeadCode
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colpayHeadName).Value = obj.PayHeadName
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colPayHeadFormula).Value = obj.Formula
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colRateAmount).Value = obj.Rate_Amount
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colHiddenComponent).Value = obj.IsHiddenComponent
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colMax_Amount).Value = obj.MAX_AMOUNT
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colPAYPERIOD_Amount).Value = obj.PAYPERIOD_AMOUNT
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colIncrementType).Value = obj.Increment_Type
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colIncrementRate_Amt).Value = obj.IncrementRate_Amt
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colIncrementAmt).Value = obj.IncrementAmt
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colIncrementedRate_Amt).Value = obj.IncrementedRate_Amt  'IIf(clsCommon.myCdbl(obj.IncrementedRate_Amt) > 0, clsCommon.myCdbl(obj.IncrementedRate_Amt), clsCommon.myCdbl(obj.Rate_Amount))
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colTotalExperience).Value = obj.TotalExperience
                    If clsPayHeadDefinitions.CheckIncrementPayhead(obj.PayHeadCode) = True Then
                        gvSalary.Rows(gvSalary.Rows.Count - 1).IsVisible = True
                    Else
                        gvSalary.Rows(gvSalary.Rows.Count - 1).IsVisible = False
                    End If
                Next
                isInsideLoadData = False
            Else
                gvSalary.Rows.AddNew()
            End If
            If obj.POSTED = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btnPost.Enabled = False
                'btnPrint.Enabled = True
            End If
            UsLock1.Status = obj.POSTED
        End If


    End Sub


    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub gvSalary_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvSalary.CellValueChanged
        Try
            If Not isInsideLoadData Then
                isInsideLoadData = True

                'If e.Column Is gvSalary.Columns(colIncrementRate_Amt) Then
                '    gvSalary.CurrentRow.Cells(colIncrementedRate_Amt).Value = ((clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value) * clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colIncrementRate_Amt).Value)) / 100) + clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value)
                '    gvSalary.CurrentRow.Cells(colIncrementAmt).Value = ((clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value) * clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colIncrementRate_Amt).Value)) / 100)
                'ElseIf e.Column Is gvSalary.Columns(colIncrementAmt) Then
                '    gvSalary.CurrentRow.Cells(colIncrementedRate_Amt).Value = clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value) + clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colIncrementAmt).Value)
                '    gvSalary.CurrentRow.Cells(colIncrementRate_Amt).Value = (clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colIncrementAmt).Value) * 100) / clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value)
                'ElseIf e.Column Is gvSalary.Columns(colIncrementedRate_Amt) Then
                '    gvSalary.CurrentRow.Cells(colIncrementAmt).Value = (clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colIncrementedRate_Amt).Value) - clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value))
                '    gvSalary.CurrentRow.Cells(colIncrementRate_Amt).Value = (clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colIncrementAmt).Value) * 100) / clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value)
                'End If

                If e.Column Is gvSalary.Columns(colIncrementRate_Amt) Then

                    gvSalary.CurrentRow.Cells(colIncrementedRate_Amt).Value = ((clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value) * clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colIncrementRate_Amt).Value)) / 100) + clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value)
                    gvSalary.CurrentRow.Cells(colIncrementAmt).Value = ((clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value) * clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colIncrementRate_Amt).Value)) / 100)
                ElseIf e.Column Is gvSalary.Columns(colIncrementAmt) Then
                    If clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value) = 0 Then
                        gvSalary.CurrentRow.Cells(colIncrementedRate_Amt).Value = clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value) + clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colIncrementAmt).Value)
                        gvSalary.CurrentRow.Cells(colIncrementRate_Amt).Value = 0
                    Else
                        gvSalary.CurrentRow.Cells(colIncrementedRate_Amt).Value = clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value) + clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colIncrementAmt).Value)
                        gvSalary.CurrentRow.Cells(colIncrementRate_Amt).Value = (clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colIncrementAmt).Value) * 100) / clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value)

                    End If
                ElseIf e.Column Is gvSalary.Columns(colIncrementedRate_Amt) Then
                    If clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value) = 0 Then
                        gvSalary.CurrentRow.Cells(colIncrementAmt).Value = (clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colIncrementedRate_Amt).Value) - clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value))
                        gvSalary.CurrentRow.Cells(colIncrementRate_Amt).Value = 0
                    End If
                    gvSalary.CurrentRow.Cells(colIncrementAmt).Value = (clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colIncrementedRate_Amt).Value) - clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value))
                    gvSalary.CurrentRow.Cells(colIncrementRate_Amt).Value = (clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colIncrementAmt).Value) * 100) / clsCommon.myCdbl(gvSalary.CurrentRow.Cells(colPAYPERIOD_Amount).Value)

                End If




                isInsideLoadData = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    'Sub OpenRoundCodeList(ByVal isButtonClick As Boolean)

    '    Dim qry As String = "select 'Fixed' as Code union select 'Percent' as Code "
    '    gvSalary.CurrentRow.Cells(colIncrementType).Value = clsCommon.ShowSelectForm("salary444", qry, "Code", "", clsCommon.myCstr(gvSalary.CurrentRow.Cells(colIncrementType).Value), "Code", isButtonClick)
    'End Sub
    Private Sub fndIncrementCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndIncrementCode._MYNavigator
        Try
            LoadData(fndIncrementCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndIncrementCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndIncrementCode._MYValidating
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " LOCATION_CODE='" + LocCode + "'"
            End If
        End If
        Dim str As String = "select count(*) from TSPL_EMPLOYEE_INCREMENT_HEAD where INCREMENT_CODE ='" + fndIncrementCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            fndIncrementCode.MyReadOnly = False
        Else
            fndIncrementCode.MyReadOnly = True
        End If
        If fndIncrementCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select INCREMENT_CODE as Code,INCREMENT_DATE as [Date] ,APPLICABLE_FROM as [Applicable From ],EMP_SAL_CODE as [Employee Salary] ,EMP_CODE as [Emp Code],SALARY_STRUCTURE_CODE as [Salary Structure Code] ,location_Code as [Location] ,DEVISION_CODE as [Division] from TSPL_EMPLOYEE_INCREMENT_HEAD "
            fndIncrementCode.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_INCREMENT_HEAD", qry, "Code", whrcls, fndIncrementCode.Value, "INCREMENT_CODE", isButtonClicked)
            If fndIncrementCode.Value <> "" Then
                LoadData(fndIncrementCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmEmpIncrement)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmEmpIncrement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        isNewEntry = True
        'isInsideLoadData = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        'If clsCommon.myLen(Me.Tag) > 0 Then
        '    LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        'End If
        funReset()
        Try
            If clsCommon.myLen(Me.Tag) > 0 Then
                LoadData(Me.Tag, NavigatorType.Current)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FrmEmpIncrement_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            funDelete()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            PostData()
        End If
    End Sub

    Sub ImportSalary(ByVal _Type As String)
        Try
            '' Options for _Type 1. Increment Amount(IA) 2. New Salary(NS)
            If clsCommon.myLen(_Type) <= 0 Or (clsCommon.CompairString(_Type, "NS") <> CompairStringResult.Equal And clsCommon.CompairString(_Type, "IA") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Invalid Import Type")
                Exit Sub
            End If
            '' shivani,,,against[BM00000007827]
            If clsCommon.myLen(Me.fndEmpCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select any Employee Code")
                Exit Sub
            End If
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim currentdate As Date = Date.Today
            Dim strSelect As String
            strSelect = clsEmployeeSalary.GetPayHeadCodeString(Me.lblSalaryStructCode.Text) 'ClsEmpIncrement.GetPayHeadCodeString(Me.lblSalaryStructCode.Text)
            strSelect = strSelect.Replace("[", "")
            strSelect = strSelect.Replace("]", "")
            If clsCommon.myLen(strSelect) <= 0 Then
                clsCommon.MyMessageBoxShow("Pay Head not Found for the Selected Salary Structure " & Me.lblSalaryStructCode.Text & "")
                Exit Sub
            End If
            Dim arr() As String
            Dim arrParam() As String = {"Emp Code", "APPLICABLE FROM", "Arrear From"}
            arr = strSelect.Split(",")
            For Each strarr As String In arr
                ReDim Preserve arrParam(arrParam.Length)
                arrParam(arrParam.Length - 1) = strarr
            Next

            ''Ticket No:BM00000007880 by Panch Raj
            If importExcelSalary(gv, arrParam) Then
                Try
                    clsCommon.ProgressBarShow()

                    Dim obj As ClsEmpIncrement
                    Dim obj1 As clsEmpIncrementDetail
                    Dim Increment_Code As New List(Of String)

                    For Each grow As GridViewRowInfo In gv.Rows
                        obj = New ClsEmpIncrement
                        ObjList = New List(Of clsEmpIncrementDetail)

                        Dim strCode As String = clsCommon.myCstr(grow.Cells("Emp Code").Value)
                        If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                            Throw New Exception("Emp Code not be blank or incorrect.")
                        End If
                        obj.EMP_CODE = strCode

                        Dim strDate As Date = clsCommon.myCDate(grow.Cells("APPLICABLE FROM").Value)
                        If strDate.Year < 2000 Or (String.IsNullOrEmpty(strDate)) Then
                            Throw New Exception("Applicable From can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp Code").Value) + "")
                        End If
                        obj.APPLICABLE_FROM = clsCommon.GetPrintDate(strDate, "dd/MMM/yyyy")
                        obj.INCREMENT_DATE = obj.APPLICABLE_FROM
                        Dim strDateArrear As Date = clsCommon.myCDate(grow.Cells("Arrear From").Value)
                        If clsCommon.myLen(strDateArrear) > 0 Then
                            If strDateArrear > obj.APPLICABLE_FROM Then
                                Throw New Exception("Arrear From must be less or equal to Applicable From date for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp Code").Value) + "")
                            Else
                                obj.ARREAR_FROM = clsCommon.GetPrintDate(strDateArrear, "dd/MMM/yyyy")
                            End If
                        End If

                        txtRevisionNo.Text = clsDBFuncationality.GetDataTable("select (coalesce(max(revision_no ),0))  AS revision_no from TSPL_EMPLOYEE_SALARY where EMP_CODE='" & obj.EMP_CODE & "'").Rows(0).Item("revision_no")
                        obj.REVISION_NO = (txtRevisionNo.Text)
                        obj.SALARY_STRUCTURE_CODE = clsDBFuncationality.GetDataTable("select (coalesce(max(SALARY_STRUCTURE_CODE),'')) AS SALARY_STRUCTURE_CODE from TSPL_EMPLOYEE_SALARY where EMP_CODE='" & obj.EMP_CODE & "'").Rows(0).Item("SALARY_STRUCTURE_CODE")
                        lblSalStructName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select SALARY_STRUCTURE_NAME  from TSPL_SALARY_STRUCTURE where SALARY_STRUCTURE_CODE='" & obj.SALARY_STRUCTURE_CODE & "'"))
                        obj.EMP_SAL_CODE = clsDBFuncationality.GetDataTable("select (coalesce(max(EMP_SAL_CODE),'')) AS EMP_SAL_CODE from TSPL_EMPLOYEE_SALARY where EMP_CODE='" & obj.EMP_CODE & "'").Rows(0).Item("EMP_SAL_CODE")
                        obj.location_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select LOCATION_CODE  from TSPL_EMPLOYEE_MASTER where EMP_CODE='" & obj.EMP_CODE & "'"))
                        obj.Location_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc  from TSPL_LOCATION_MASTER  where  Location_Code='" & obj.location_Code & "'"))
                        obj.DEVISION_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DEVISION_CODE  from TSPL_EMPLOYEE_MASTER  where Emp_Code='" & obj.EMP_CODE & "'"))
                        obj.Devision_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DEVISION_NAME  from TSPL_DEVISION_MASTER  where DEVISION_CODE='" & obj.DEVISION_CODE & "'"))



                        ''''''''''''''''''''''''''Detail part''''''''''''''''''''''''''''''''''
                        Dim intLoop As Integer = 1
                        For Each strarr As String In arr
                            'strarr = strarr.Replace("_", ".")
                            obj1 = New clsEmpIncrementDetail
                            'obj1.EMP_CODE = strCode
                            obj1.PayHeadCode = strarr.Replace("_", ".")
                            obj1.Line_No = intLoop
                            obj1.Formula = clsMapPayHeadsToSalaStructure.GetFormula(Me.lblSalaryStructCode.Text, strarr.Replace("_", "."))
                            obj1.Rate_Amount = clsEmployeeSalary.getPayHeadAmount(obj.EMP_CODE, obj.SALARY_STRUCTURE_CODE, obj1.PayHeadCode, obj.APPLICABLE_FROM, "") 'clsCommon.myCdbl(grow.Cells(strarr.Replace(".", "_")).Value)
                            obj1.PAYPERIOD_AMOUNT = obj1.Rate_Amount
                            If clsCommon.CompairString(_Type, "IA") = CompairStringResult.Equal Then
                                obj1.IncrementAmt = clsCommon.myCdbl(grow.Cells(strarr.Replace(".", "_")).Value) 'ClsEmpIncrement.getPayHeadAmount(obj.EMP_CODE, obj.SALARY_STRUCTURE_CODE, obj1.PayHeadCode, obj.APPLICABLE_FROM)
                                obj1.IncrementedRate_Amt = obj1.Rate_Amount + obj1.IncrementAmt 'clsCommon.myCdbl(grow.Cells(strarr.Replace(".", "_")).Value)
                                obj1.IncrementRate_Amt = (obj1.IncrementAmt * 100) / IIf(clsCommon.myCdbl(obj1.PAYPERIOD_AMOUNT) > 0, clsCommon.myCdbl(obj1.PAYPERIOD_AMOUNT), 1)
                            ElseIf clsCommon.CompairString(_Type, "NS") = CompairStringResult.Equal Then
                                obj1.IncrementedRate_Amt = clsCommon.myCdbl(grow.Cells(strarr.Replace(".", "_")).Value)
                                obj1.IncrementAmt = obj1.IncrementedRate_Amt - obj1.Rate_Amount
                                obj1.IncrementRate_Amt = (obj1.IncrementAmt * 100) / IIf(clsCommon.myCdbl(obj1.PAYPERIOD_AMOUNT) > 0, clsCommon.myCdbl(obj1.PAYPERIOD_AMOUNT), 1)
                            End If

                            ObjList.Add(obj1)
                            intLoop = intLoop + 1
                        Next
                        LastApplicableFrom = Nothing
                        LastArrearFrom = Nothing
                        validatedateimport(obj.EMP_CODE)
                        If Not LastApplicableFrom Is Nothing Then
                            If CDate(clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd/MMM/yyyy")) <= CDate(clsCommon.GetPrintDate(LastApplicableFrom, "dd/MMM/yyyy")) Then
                                clsCommon.ProgressBarHide()
                                clsCommon.MyMessageBoxShow("Applicable from date must be greater than last increment applicable from date (" & LastApplicableFrom & ")")

                                Exit Sub
                            End If
                        End If
                        If Not LastArrearFrom Is Nothing Then
                            If CDate(clsCommon.GetPrintDate(obj.ARREAR_FROM, "dd/MMM/yyyy")) <= CDate(clsCommon.GetPrintDate(LastApplicableFrom, "dd/MMM/yyyy")) Then
                                clsCommon.ProgressBarHide()
                                clsCommon.MyMessageBoxShow("Arrear from date must be greater than last increment applicable from date (" & LastApplicableFrom & ")")

                                Exit Sub
                            End If
                        End If
                        obj.SaveData(obj, ObjList, True, "")
                        Increment_Code.Add(obj.INCREMENT_CODE)

                    Next

                    Dim UP_QRY As String = ""

                    UP_QRY = " UPDATE TSPL_EMPLOYEE_INCREMENT_DETAIL SET ISHIDDENCOMPONENT=T1.ISHIDDENCOMPONENT FROM TSPL_PAYHEAD_MASTER T1  "
                    UP_QRY += " WHERE T1.PAY_HEAD_CODE=TSPL_EMPLOYEE_INCREMENT_DETAIL.PAY_HEAD_CODE and TSPL_EMPLOYEE_INCREMENT_DETAIL.INCREMENT_CODE in (" & clsCommon.GetMulcallString(Increment_Code) & ") "

                    clsDBFuncationality.ExecuteNonQuery(UP_QRY)

                    UP_QRY = ""
                    UP_QRY = " UPDATE TSPL_EMPLOYEE_INCREMENT_DETAIL SET TSPL_EMPLOYEE_INCREMENT_DETAIL.LINE_NO=TSPL_SALSTRUCT_PAYHEADS.LINE_NO FROM  " &
                             " TSPL_SALSTRUCT_PAYHEADS INNER JOIN TSPL_EMPLOYEE_INCREMENT_HEAD " &
                             "  ON TSPL_SALSTRUCT_PAYHEADS.SALARY_STRUCTURE_CODE=TSPL_EMPLOYEE_INCREMENT_HEAD.SALARY_STRUCTURE_CODE  " &
                             " WHERE(TSPL_EMPLOYEE_INCREMENT_HEAD.SALARY_STRUCTURE_CODE = TSPL_SALSTRUCT_PAYHEADS.SALARY_STRUCTURE_CODE)  " &
                             " AND TSPL_SALSTRUCT_PAYHEADS.PAY_HEAD_CODE=TSPL_EMPLOYEE_INCREMENT_DETAIL.PAY_HEAD_CODE AND TSPL_EMPLOYEE_INCREMENT_HEAD.SALARY_STRUCTURE_CODE='" & lblSalaryStructCode.Text & "' and  TSPL_EMPLOYEE_INCREMENT_DETAIL.INCREMENT_CODE in (" & clsCommon.GetMulcallString(Increment_Code) & ")"
                    clsDBFuncationality.ExecuteNonQuery(UP_QRY)
                    UP_QRY = String.Empty

                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    clsCommon.ProgressBarHide()
                    myMessages.myExceptions(ex)
                End Try

            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub mnuExportWithIncrementAmt_Click(sender As Object, e As EventArgs) Handles mnuExportWithIncrementAmt.Click
        If clsCommon.myLen(Me.fndEmpCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select any Employee Code")
            Exit Sub
        End If
        Dim str As String
        str = ClsEmpIncrement.ExportEmployeeSalary(Me.lblSalaryStructCode.Text, Me)

    End Sub

    Private Sub mnuExportWithNewSalary_Click(sender As Object, e As EventArgs) Handles mnuExportWithNewSalary.Click
        If clsCommon.myLen(Me.fndEmpCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select any Employee Code")
            Exit Sub
        End If
        Dim str As String
        str = ClsEmpIncrement.ExportEmployeeIncrementedSalary(Me.lblSalaryStructCode.Text, Me)

    End Sub

    Private Sub mnuImportWithIncrementAmt_Click(sender As Object, e As EventArgs) Handles mnuImportWithIncrementAmt.Click
        ImportSalary("IA")
    End Sub

    Private Sub mnuImportWithFinalSalary_Click(sender As Object, e As EventArgs) Handles mnuImportWithFinalSalary.Click
        ImportSalary("NS")
    End Sub
    '' changes by shivani against[BM00000007880]

    Private Sub gvSalary_ViewRowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvSalary.ViewRowFormatting
        Try
            Dim TotalInc As Double = 0
            Dim TotalLastSalary As Double = 0
            Dim TotalNewSalary As Double = 0

            For Each grow As GridViewRowInfo In gvSalary.ChildRows
                TotalInc += clsCommon.myCdbl(grow.Cells(colIncrementAmt).Value)
                lblTotalInc1.Text = TotalInc
                TotalLastSalary += clsCommon.myCdbl(grow.Cells(colPAYPERIOD_Amount).Value)
                lblTotalSalary1.Text = TotalLastSalary
                TotalNewSalary += clsCommon.myCdbl(grow.Cells(colIncrementedRate_Amt).Value)
                lblNewSalary.Text = TotalNewSalary
            Next
            'End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub LocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles LocationCode._MYValidating
        Try
            Dim whrcls As String = Nothing
            Dim LocCode As String = Nothing
            If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
                LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
                If clsCommon.myLen(LocCode) > 0 Then
                    whrcls = " TSPL_Location_MASTER.LOCATION_CODE='" + LocCode + "'"
                End If
            End If
            LocationCode.Value = clsLocation.getFinder(whrcls, Me.LocationCode.Value, isButtonClicked)
            lblLocationName.Text = clsLocation.GetName(LocationCode.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex, Me.Text)
        End Try
    End Sub
End Class

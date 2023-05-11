Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

' Ticket No : ERO/04/12/18-000426 By Prabhakar Create New Screen 
Public Class frmSaleIncentiveMaster
    Inherits FrmMainTranScreen
    Const colLineNo As String = "LineNo"
    Const colFromRange As String = "FromRange"
    Const colToRange As String = "ToRange"
    Const colRangeUom As String = "RangeUom"
    Const colIncentiveUom As String = "IncentiveUom"
    Const colIncentiveValue As String = "IncentiveValue"
    Private isInsideLoadData As Boolean = False
    Dim isNewEntry As Boolean = True
    Dim isCellValueChangedOpen As Boolean = False
    Dim dt As DataTable
    Dim qry As String
    Dim CurrentDate As DateTime = clsCommon.GETSERVERDATE()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim SettNoOFCustomerForImportExport As Integer
    Dim SettNoOFSlabForImportExport As Integer
    Dim SettNoOFItemStructureForImportExport As Integer
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        If btnSave.Visible = True Then
            rmiImport.Enabled = True
            rmiExport.Enabled = True
        Else
            rmiImport.Enabled = False
            rmiExport.Enabled = False
        End If
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub

    Private Sub frmSaleIncentiveMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            Reset()
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
            ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
            ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
            ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
            ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
            ValidateLength()
            SettNoOFCustomerForImportExport = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.NoOFCustomerForImportExport, clsFixedParameterCode.NoOFCustomerForImportExport, Nothing))
            SettNoOFSlabForImportExport = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.NoOFSlabForImportExport, clsFixedParameterCode.NoOFSlabForImportExport, Nothing))
            SettNoOFItemStructureForImportExport = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.NoOFItemStructureForImportExport, clsFixedParameterCode.NoOFItemStructureForImportExport, Nothing))
            '=====================================================
            
            'txtFromDate.CustomFormat = "MMM/yyyy"
            'txtFromDate.Format = DateTimePickerFormat.Custom
            'txtFromDate.Culture.Calendar.ge(clsCommon.GETSERVERDATE())
            'Dim strToDate As String = Nothing
            'Dim To_Date As Date
            'strToDate = "31/" + "03" + "/" + clsCommon.myCstr(fromDate.Value.AddYears(+1).Year)
            'To_Date = clsCommon.myCDate(strToDate)
            'Todate.Value = To_Date


            'Dim strFromDate As String = Nothing
            'strFromDate = "01/" + clsCommon.myCstr(txtFromDate.Value.Month) + "/" + "" + clsCommon.myCstr(txtFromDate.Value.Year) + """"
            'Dim strToDate As String = Nothing
            'Dim intMonth As Integer = txtToDate.Value.AddMonths(+1).Month

            'strToDate = "01/" + clsCommon.myCstr(txtToDate.Value.Month) + "/" + "" + clsCommon.myCstr(txtToDate.Value.Year) + """"
            'Dim toDateNoOfDays As DateTime = DateSerial(txtToDate.Value.Year, txtToDate.Value.Month, 0)
            'Dim strFromDate As String = Nothing
            'strFromDate = "01/" + clsCommon.myCstr(txtFromDate.Value.Month) + "/" + "" + clsCommon.myCstr(txtFromDate.Value.Year) + """"
            '=======================================================
        Catch ex As Exception
            Throw New Exception(ex.Message) ''clsCommon.MyMessageBoxShow(ex.Message) comment it because after getting permission denied exception, it still opened the screen due to showmessage propety,so chenage it into throw exception
        End Try
    End Sub
    Private Sub ValidateLength()
        txtDesc.MaxLength = 200
    End Sub

    Private Sub Reset()
        BlankAllControl()
        txtIncentive.MyReadOnly = False
        btnSave.Text = "Save"
        lblPending.Status = ERPTransactionStatus.Pending
        isCellValueChangedOpen = False
        isNewEntry = True
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        chkInactive.Enabled = False
        chkInactive.Checked = False
        btnUpdates.Enabled = False
    End Sub
    Sub BlankAllControl()
        txtIncentive.Value = ""
        txtIncentiveDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtIncentiveDate.ReadOnly = True
        txtRangeUnit.Value = ""
        txtIncentiveUnit.Value = ""
        txtDesc.Text = ""
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtCustomer.arrValueMember = Nothing
        txtItemSturcture.arrValueMember = Nothing
        txtGLAccount.Value = ""
        lblGLAccount.Text = ""
        LoadBlankIncentiveGrid()
    End Sub

    Sub LoadBlankIncentiveGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        gv.Columns.Add(colLineNo, "SNo")
        gv.Columns(colLineNo).Width = 50
        gv.Columns(colLineNo).ReadOnly = True

        'Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoLineNo.FormatString = ""
        'repoLineNo.HeaderText = "Line No"
        'repoLineNo.Name = colLineNo
        'repoLineNo.Width = 50
        'repoLineNo.ReadOnly = True
        'repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoFromRange As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFromRange.FormatString = "{0:n2}"
        repoFromRange.HeaderText = "From Range"
        repoFromRange.Name = colFromRange
        repoFromRange.Width = 80
        repoFromRange.Minimum = 0
        repoFromRange.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoFromRange)

        Dim repoToRange As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoToRange.FormatString = "{0:n2}"
        repoToRange.HeaderText = "To Range"
        repoToRange.Name = colToRange
        repoToRange.Width = 80
        repoToRange.Minimum = 0
        repoToRange.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoToRange)

        Dim repoRangeUOM As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRangeUOM.HeaderText = "Unit Code"
        repoRangeUOM.Name = colRangeUom
        repoRangeUOM.Width = 80
        repoRangeUOM.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoRangeUOM)

        Dim repIncentive As GridViewDecimalColumn = New GridViewDecimalColumn()
        repIncentive.FormatString = "{0:n2}"
        repIncentive.HeaderText = "Incentive"
        repIncentive.Name = colIncentiveValue
        repIncentive.Width = 80
        repIncentive.Minimum = 0
        repIncentive.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repIncentive)

        Dim repoIncentiveUOM As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoIncentiveUOM.HeaderText = "Per"
        repoIncentiveUOM.Name = colIncentiveUom
        repoIncentiveUOM.Width = 80
        repoIncentiveUOM.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIncentiveUOM)


        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = True
        gv.AllowRowReorder = False
        gv.ShowGroupPanel = False
        gv.EnableFiltering = False
        gv.EnableSorting = False
        gv.EnableGrouping = False
        gv.AllowColumnChooser = True
        gv.AllowColumnReorder = True
        gv.Rows.AddNew()
    End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Try
            If (AllowToSave()) Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.SaleIncentiveMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim obj As New clsSaleIncentiveHeader()
                obj.INCENTIVE_CODE = txtIncentive.Value
                obj.INCENTIVE_DATE = txtIncentiveDate.Value
                obj.DESCRIPTION = txtDesc.Text
                Dim strFromDate As String = Nothing
                strFromDate = "01/" + clsCommon.myCstr(txtFromDate.Value.Month) + "/" + clsCommon.myCstr(txtFromDate.Value.Year)
                obj.FROM_DATE = clsCommon.myCDate(strFromDate)
                Dim strToDAteValue As String = clsCommon.myCDate(DateSerial(txtToDate.Value.Year, txtToDate.Value.Month + 1, 0))
                obj.TO_DATE = clsCommon.myCDate(DateSerial(txtToDate.Value.Year, txtToDate.Value.Month + 1, 0)) 'clsCommon.myCDate(txtToDate.Value)
                obj.RANGE_UOM = txtRangeUnit.Value
                obj.INCENTIVE_UOM = txtIncentiveUnit.Value
                obj.GL_Code = txtGLAccount.Value
                obj.ArrIncentiveDetails = New List(Of clsSaleIncentiveDetails)
                Dim Count As Integer = 0
                For Each grow As GridViewRowInfo In gv.Rows
                    If clsCommon.myLen(grow.Cells(colFromRange).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colToRange).Value) > 0 Then
                        Dim objTr As New clsSaleIncentiveDetails()
                        objTr.SNO = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        objTr.INCENTIVE_CODE = obj.INCENTIVE_CODE
                        objTr.FROM_RANGE = clsCommon.myCdbl(grow.Cells(colFromRange).Value)
                        objTr.TO_RANGE = clsCommon.myCdbl(grow.Cells(colToRange).Value)
                        objTr.INCENTIVE = clsCommon.myCdbl(grow.Cells(colIncentiveValue).Value)
                        Count += 1
                        'If clsCommon.myLen(objTr.INCENTIVE_CODE) > 0 Then
                        obj.ArrIncentiveDetails.Add(objTr)
                        'End If
                    End If
                Next
                obj.ArrIncentiveCustomerMapping = txtCustomer.arrValueMember
                'obj.ArrIncentiveCustomerMapping = New List(Of clsSaleIncentiveCustomerMapping)
                'If txtCustomer.arrValueMember Is Nothing OrElse txtCustomer.arrValueMember.Count > 0 Then
                '    Dim i As Integer = 0
                '    For i = 0 To txtCustomer.arrValueMember.Count - 1
                '        Dim objCust As New clsSaleIncentiveCustomerMapping()
                '        objCust.INCENTIVE_CODE = obj.INCENTIVE_CODE
                '        objCust.CUSTOMER_CODE = clsCommon.myCstr(txtCustomer.arrValueMember(i))
                '        obj.ArrIncentiveCustomerMapping.Add(objCust)
                '    Next
                'End If

                obj.arrIncentiveStructureMapping = txtItemSturcture.arrValueMember
                'obj.arrIncentiveStructureMapping = New List(Of clsSaleIncentiveSturctureMapping)
                'If txtItemSturcture.arrValueMember Is Nothing OrElse txtItemSturcture.arrValueMember.Count > 0 Then
                '    Dim i As Integer = 0
                '    For i = 0 To txtItemSturcture.arrValueMember.Count - 1
                '        Dim objItemStr As New clsSaleIncentiveSturctureMapping()
                '        objItemStr.INCENTIVE_CODE = obj.INCENTIVE_CODE
                '        objItemStr.STRUCTURE_CODE = clsCommon.myCstr(txtItemSturcture.arrValueMember(i))
                '        obj.arrIncentiveStructureMapping.Add(objItemStr)
                '    Next
                'End If
                If obj.SaveData(obj, isNewEntry) Then
                    If isPost = False Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully")
                        LoadData(obj.INCENTIVE_CODE, NavigatorType.Current)
                        Exit Sub
                    End If
                    
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Function AllowToSave() As Boolean

        Dim linno As Integer = 0

        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please enter Incentive Description")
            txtDesc.Focus()
            Return False
        End If
        If clsCommon.myLen(txtToDate.Value) > 0 AndAlso clsCommon.myLen(txtFromDate.Value) > 0 Then
            Dim strFromDate As String = Nothing
            strFromDate = "01/" + clsCommon.myCstr(txtFromDate.Value.Month) + "/" + clsCommon.myCstr(txtFromDate.Value.Year)
            Dim From_Date As Date = clsCommon.myCDate(strFromDate)
            Dim strToDAteValue As String = clsCommon.myCDate(DateSerial(txtToDate.Value.Year, txtToDate.Value.Month + 1, 0))
            Dim To_Date As Date = clsCommon.myCDate(DateSerial(txtToDate.Value.Year, txtToDate.Value.Month + 1, 0))
            If From_Date > To_Date Then
                clsCommon.MyMessageBoxShow("From Date can not be greater then To Date.")
                Return False
            End If
        End If
        If clsCommon.myLen(txtRangeUnit.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Range UOM")
            txtRangeUnit.Focus()
            Return False
        End If

        If clsCommon.myLen(txtGLAccount.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select GL Account")
            txtGLAccount.Focus()
            Return False
        End If
        If clsCommon.myLen(txtIncentiveUnit.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Incentive UOM")
            txtIncentiveUnit.Focus()
            Return False
        End If
        If txtCustomer.arrValueMember Is Nothing OrElse txtCustomer.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select at least one customer")
            txtCustomer.Focus()
            Return False
        End If
        If txtItemSturcture.arrValueMember Is Nothing OrElse txtItemSturcture.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select at least one Item Structure")
            txtItemSturcture.Focus()
            Return False
        End If
        If clsCommon.myLen(gv.Rows(0).Cells(colToRange).Value) <= 0 AndAlso clsCommon.myLen(gv.Rows(0).Cells(colFromRange).Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Enter Atleast one SLAB in grid.")
            Return False
        End If
        If gv.Rows.Count > 0 Then
            For ii As Integer = 0 To gv.RowCount - 1
                If clsCommon.myCdbl(gv.Rows(ii).Cells(colToRange).Value) > 0 AndAlso clsCommon.myCdbl(gv.Rows(ii).Cells(colFromRange).Value) > 0 Then
                    If clsCommon.myCdbl(gv.Rows(ii).Cells(colToRange).Value) <= clsCommon.myCdbl(gv.Rows(ii).Cells(colFromRange).Value) Then
                        clsCommon.MyMessageBoxShow("To Range should be greater  then From Range . At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ", Me.Text)
                        Return False
                    End If
                    If clsCommon.myLen(gv.Rows(ii).Cells(colIncentiveValue).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Incentive Value is Mandatory. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ", Me.Text)
                        Return False
                    End If
                End If
            Next
        End If
        Return True
    End Function

   

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub

    

    Sub LoadData(ByVal strIncentiveCode As String, ByVal NavType As NavigatorType)
        Try
            Reset()
            isInsideLoadData = True
            Dim obj As New clsSaleIncentiveHeader
            obj = clsSaleIncentiveHeader.GetData(strIncentiveCode, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.INCENTIVE_CODE) > 0 Then
                isNewEntry = False
                btnSave.Text = "Update"
                txtFromDate.Value = obj.FROM_DATE
                txtToDate.Value = obj.TO_DATE
                txtIncentive.Value = obj.INCENTIVE_CODE
                txtDesc.Text = obj.DESCRIPTION
                txtRangeUnit.Value = obj.RANGE_UOM
                txtIncentiveUnit.Value = obj.INCENTIVE_UOM
                txtGLAccount.Value = obj.GL_Code
                lblGLAccount.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + txtGLAccount.Value + "' ")
                Dim LineNo As Integer = 0
                'LoadBlankIncentiveGrid()
                For Each objIncentiveDetail As clsSaleIncentiveDetails In obj.ArrIncentiveDetails
                    LineNo += 1

                    gv.CurrentRow.Cells(colLineNo).Value = LineNo
                    gv.CurrentRow.Cells(colFromRange).Value = objIncentiveDetail.FROM_RANGE
                    gv.CurrentRow.Cells(colToRange).Value = objIncentiveDetail.TO_RANGE
                    gv.CurrentRow.Cells(colRangeUom).Value = obj.RANGE_UOM
                    gv.CurrentRow.Cells(colIncentiveValue).Value = objIncentiveDetail.INCENTIVE
                    gv.CurrentRow.Cells(colIncentiveUom).Value = obj.INCENTIVE_UOM
                    gv.Rows.AddNew()
                Next



                'Dim arrStructureCode As New ArrayList()
                'Dim arrStructureName As New ArrayList()
                'For Each objItemStructure As clsSaleIncentiveSturctureMapping In obj.arrIncentiveStructureMapping
                '    arrStructureCode.Add(objItemStructure.STRUCTURE_CODE)
                'Next
                txtItemSturcture.arrValueMember = obj.arrIncentiveStructureMapping

                'Dim arrCustomerCode As New ArrayList()
                'For Each objCustomerCode As clsSaleIncentiveCustomerMapping In obj.ArrIncentiveCustomerMapping
                '    arrCustomerCode.Add(objCustomerCode.CUSTOMER_CODE)
                'Next
                txtCustomer.arrValueMember = obj.ArrIncentiveCustomerMapping
                lblPending.Status = obj.Status
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    chkInactive.Enabled = True
                    btnUpdates.Enabled = True
                Else
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                    chkInactive.Enabled = False
                End If
                chkInactive.Checked = obj.In_Active
                If obj.In_Active Then
                    chkInactive.Enabled = False
                End If
                If Not MyBase.isModifyFlag Then
                    chkInactive.Enabled = False
                End If
            Else
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub


   

    
    

   

    Private Sub DeleteData(ByVal strIcentiveCode As String)
        If clsCommon.myLen(strIcentiveCode) = 0 Then
            clsCommon.MyMessageBoxShow("No Incentive Code found to delete.")
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strIcentiveCode) > 0 Then
                If clsSaleIncentiveHeader.fundelete(strIcentiveCode, trans) Then
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data deleted successfully.")
                    Reset()
                End If
            Else
                clsCommon.MyMessageBoxShow("No Incentive Code found to delete.")
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

   

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

   

    Private Sub rmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExport.Click
        Dim str As String
        str = "select '' as [From Date(dd/MMM/yyyy)],'' as [To Date(dd/MMM/yyyy)],'' as [Description],'' AS [Range Unit] , '' as [Incentive Unit],'' as [GL Account],'' as [From Range] "
        For ii As Integer = 1 To SettNoOFSlabForImportExport
            ' str += ",'' as [From Range " + clsCommon.myCstr(ii) + "]"
            str += ",'' as [To Range " + clsCommon.myCstr(ii) + "]"
            str += ",'' as [Incentive " + clsCommon.myCstr(ii) + "]"
        Next
        For ii As Integer = 1 To SettNoOFCustomerForImportExport
            str += ",'' as [Customer " + clsCommon.myCstr(ii) + "]"
        Next
        For ii As Integer = 1 To SettNoOFItemStructureForImportExport
            str += ",'' as [Item Structure " + clsCommon.myCstr(ii) + "]"
        Next

        ListImpExpColumnsMandatory = New List(Of String)({"From Date(dd/MMM/yyyy)", "To Date(dd/MMM/yyyy)", "Range Unit", "Incentive Unit", "GL Account"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory)
    End Sub
    Private Sub rmiImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim currentdate As Date = Date.Today
            Dim linno As Integer = 0

            Dim Strs As List(Of String) = New List(Of String)
            Strs.Add("From Date(dd/MMM/yyyy)")
            Strs.Add("To Date(dd/MMM/yyyy)")
            Strs.Add("Description")
            Strs.Add("Range Unit")
            Strs.Add("Incentive Unit")
            Strs.Add("GL Account")
            Strs.Add("From Range")
            For ii As Integer = 1 To SettNoOFSlabForImportExport
                ' Strs.Add("From Range " + clsCommon.myCstr(ii))
                Strs.Add("To Range " + clsCommon.myCstr(ii))
                Strs.Add("Incentive " + clsCommon.myCstr(ii))
            Next
            For ii As Integer = 1 To SettNoOFCustomerForImportExport
                Strs.Add("Customer " + clsCommon.myCstr(ii))
            Next
            For ii As Integer = 1 To SettNoOFItemStructureForImportExport
                Strs.Add("Item Structure " + clsCommon.myCstr(ii))
            Next
            If transportSql.importExcel(gv, Strs.ToArray()) Then
                Dim trans As SqlTransaction = Nothing
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        Dim obj As New clsSaleIncentiveHeader()
                        linno += 1
                        obj.DESCRIPTION = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(obj.DESCRIPTION) > 0 Then
                            If obj.DESCRIPTION.Length > 200 Then
                                Throw New Exception("Description length can not be more than 200 at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            'obj.FROM_DATE = clsCommon.myCdbl(grow.Cells("Amount").Value)
                            'If obj.Deduction_Amount < 0 Then
                            '    Throw New Exception("Deduction amount cannot be less than or equal to zero. " + clsCommon.myCstr(linno) + ".")
                            'End If
                            obj.FROM_DATE = clsCommon.myCDate(grow.Cells("From Date(dd/MMM/yyyy)").Value)
                            obj.TO_DATE = clsCommon.myCDate(grow.Cells("To Date(dd/MMM/yyyy)").Value)
                            If clsCommon.myLen(obj.FROM_DATE) <= 0 Then
                                Throw New Exception("From Date can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            If clsCommon.myLen(obj.TO_DATE) <= 0 Then
                                Throw New Exception("To Date can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            If obj.FROM_DATE > obj.TO_DATE Then
                                Throw New Exception("From Date can not be greater then To Date at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.RANGE_UOM = clsCommon.myCstr(grow.Cells("Range Unit").Value)
                            If clsCommon.myLen(obj.RANGE_UOM) <= 0 Then
                                Throw New Exception("Range UOM can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry As String = Nothing
                            Dim isValid As Boolean = Nothing
                            If clsCommon.myLen(obj.RANGE_UOM) > 0 Then
                                qry = " select count (*) from TSPL_UNIT_MASTER where Unit_Code = '" + obj.RANGE_UOM + "' "
                                isValid = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
                                If isValid = False Then
                                    Throw New Exception("Invalid Range UOM at line no. " + clsCommon.myCstr(linno) + ".")
                                End If
                            End If
                            obj.INCENTIVE_UOM = clsCommon.myCstr(grow.Cells("Incentive Unit").Value)
                            If clsCommon.myLen(obj.INCENTIVE_UOM) <= 0 Then
                                Throw New Exception("Incentive UOM can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            If clsCommon.myLen(obj.INCENTIVE_UOM) > 0 Then
                                qry = " select count (*) from TSPL_UNIT_MASTER where Unit_Code = '" + obj.INCENTIVE_UOM + "' "
                                isValid = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
                                If isValid = False Then
                                    Throw New Exception("Invalid Incentive UOM at line no. " + clsCommon.myCstr(linno) + ".")
                                End If
                                qry = " select count (*) from TSPL_UNIT_MASTER where Unit_Code = '" + obj.INCENTIVE_UOM + "' and Ltr_Type = 'Y'"
                                isValid = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
                                If isValid = False Then
                                    Throw New Exception("Incentive UOM should by Ltr Type at line no. " + clsCommon.myCstr(linno) + ".")
                                End If
                            End If
                            obj.GL_Code = clsCommon.myCstr(grow.Cells("GL Account").Value)
                            If clsCommon.myLen(obj.GL_Code) <= 0 Then
                                Throw New Exception("GL Account can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            If clsCommon.myLen(obj.GL_Code) > 0 Then
                                qry = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + obj.GL_Code + "'"
                                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                                If check <= 0 Then
                                    Throw New Exception("Filled GL Account(" & obj.GL_Code & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                                End If
                                'Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + obj.GL_Code + "' AND ControlAccount ='Y'"
                                'Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                                'If check1 <= 0 Then
                                '    Throw New Exception("Filled GL Account (" & obj.GL_Code & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                                'End If
                            End If
                            obj.INCENTIVE_DATE = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy  hh:mm:ss tt ")
                            Dim strFromRange As String = clsCommon.myCstr(grow.Cells("From Range").Value)

                            '=======================Slab===========================================
                            obj.ArrIncentiveDetails = New List(Of clsSaleIncentiveDetails)
                            Dim sno As Integer = 1
                            Dim strMakeFromRangeByToRange As String = Nothing
                            For i As Integer = 1 To SettNoOFSlabForImportExport

                                Dim objSlab As New clsSaleIncentiveDetails()
                                objSlab.INCENTIVE_CODE = obj.INCENTIVE_CODE
                                If i = 1 Then
                                    strFromRange = clsCommon.myCstr(strFromRange)
                                Else
                                    strFromRange = clsCommon.myCstr((clsCommon.myCdbl(strMakeFromRangeByToRange) + 0.01))
                                End If
                                ' objSlab.FROM_RANGE
                                'strFromRange = clsCommon.myCstr((clsCommon.myCdbl(strMakeFromRangeByToRange) + 0.1))
                                Dim strToRange As String = clsCommon.myCstr(grow.Cells("To Range " + clsCommon.myCstr(i)).Value)     ' objSlab.TO_RANGE

                                Dim strIncentive As String = clsCommon.myCstr(grow.Cells("Incentive " + clsCommon.myCstr(i)).Value)   ' objSlab.INCENTIVE

                                objSlab.SNO = sno
                                If clsCommon.myLen(strFromRange) > 0 Then
                                    If IsNumeric(strFromRange) = False Then
                                        Throw New Exception("" + clsCommon.myCstr(grow.Cells("" + clsCommon.myCstr(i)).Value) + " should be Numeric at line no. " + clsCommon.myCstr(linno) + ".")
                                    End If
                                End If
                                If clsCommon.myLen(strToRange) > 0 Then
                                    If IsNumeric(strToRange) = False Then
                                        Throw New Exception("" + clsCommon.myCstr(grow.Cells("" + clsCommon.myCstr(i)).Value) + " should be Numeric at line no. " + clsCommon.myCstr(linno) + ".")
                                    End If
                                End If

                                If clsCommon.myCdbl(strFromRange) > 0 AndAlso clsCommon.myCdbl(strToRange) > 0 Then
                                    If clsCommon.myCdbl(strFromRange) > clsCommon.myCdbl(strToRange) AndAlso i > 1 Then
                                        Throw New Exception("[To Range] should be accending order. [To Range " + clsCommon.myCstr(i - 1) + "] can not be greater then [To Range " + clsCommon.myCstr(i) + "]  at line no. " + clsCommon.myCstr(linno) + ".")
                                    End If
                                    If clsCommon.myLen(strIncentive) <= 0 Then
                                        Throw New Exception("Incentive of " + clsCommon.myCstr(grow.Cells("Incentive " + clsCommon.myCstr(i)).Value) + " can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                                    Else
                                        If IsNumeric(strIncentive) = False Then
                                            Throw New Exception("" + clsCommon.myCstr(grow.Cells("Incentive " + clsCommon.myCstr(i)).Value) + " should be Numeric at line no. " + clsCommon.myCstr(linno) + ".")
                                        End If
                                    End If
                                End If
                                If strFromRange <> "" Then
                                    objSlab.FROM_RANGE = strFromRange
                                End If
                                If strToRange <> "" Then
                                    objSlab.TO_RANGE = strToRange
                                End If
                                If strIncentive <> "" Then
                                    objSlab.INCENTIVE = strIncentive
                                End If
                                If strFromRange <> "" AndAlso strToRange <> "" AndAlso strIncentive <> "" Then
                                    obj.ArrIncentiveDetails.Add(objSlab)
                                    sno = sno + 1
                                    strMakeFromRangeByToRange = strToRange
                                End If
                            Next
                            If obj.ArrIncentiveDetails.Count <= 0 Then
                                Throw New Exception("Atleast One Slab Enter in at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            '======================= Customer =====================================
                            obj.ArrIncentiveCustomerMapping = New ArrayList()
                            For i As Integer = 1 To SettNoOFCustomerForImportExport
                                Dim objCust As New clsSaleIncentiveCustomerMapping()
                                objCust.INCENTIVE_CODE = obj.INCENTIVE_CODE
                                objCust.CUSTOMER_CODE = clsCommon.myCstr(grow.Cells("Customer " + clsCommon.myCstr(i)).Value)
                                If clsCommon.myLen(objCust.CUSTOMER_CODE) > 0 Then
                                    qry = "Select count (*) from tspl_Customer_Master where Cust_Code = '" + objCust.CUSTOMER_CODE + "'"
                                    isValid = clsDBFuncationality.getSingleValue(qry, trans)
                                    If isValid = False Then
                                        Throw New Exception("Invalid Customer Code of " + clsCommon.myCstr(grow.Cells("Customer " + clsCommon.myCstr(i)).Value) + " at line no. " + clsCommon.myCstr(linno) + ".")
                                    End If
                                End If
                                If clsCommon.myLen(objCust.CUSTOMER_CODE) > 0 Then
                                    obj.ArrIncentiveCustomerMapping.Add(objCust)
                                End If
                            Next
                            If obj.ArrIncentiveCustomerMapping.Count <= 0 Then
                                Throw New Exception("Atleast One Customer Enter in at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            '======================== Item Structure ===============================
                            obj.arrIncentiveStructureMapping = New ArrayList()
                            For i As Integer = 1 To SettNoOFItemStructureForImportExport
                                Dim objItemStruct As New clsSaleIncentiveSturctureMapping()
                                objItemStruct.INCENTIVE_CODE = obj.INCENTIVE_CODE
                                objItemStruct.STRUCTURE_CODE = clsCommon.myCstr(grow.Cells("Item Structure " + clsCommon.myCstr(i)).Value)
                                If clsCommon.myLen(objItemStruct.STRUCTURE_CODE) > 0 Then
                                    qry = "select count (*) from tspl_Structure_Master where  Structure_Code = '" + objItemStruct.STRUCTURE_CODE + "'"
                                    isValid = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
                                    If isValid = False Then
                                        Throw New Exception("Invalid Structure Code of " + clsCommon.myCstr(grow.Cells("Item Structure " + clsCommon.myCstr(i)).Value) + " at line no. " + clsCommon.myCstr(linno) + ".")
                                    End If
                                End If
                                If clsCommon.myLen(objItemStruct.STRUCTURE_CODE) > 0 Then
                                    obj.arrIncentiveStructureMapping.Add(objItemStruct)
                                End If
                            Next
                            If obj.arrIncentiveStructureMapping.Count <= 0 Then
                                Throw New Exception("Atleast One Structure code Enter in at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim isSaved As Boolean = False
                            '======================================================================
                            If obj.ArrIncentiveDetails IsNot Nothing AndAlso obj.ArrIncentiveDetails.Count > 0 AndAlso obj.ArrIncentiveCustomerMapping IsNot Nothing AndAlso obj.ArrIncentiveCustomerMapping.Count > 0 AndAlso obj.arrIncentiveStructureMapping IsNot Nothing AndAlso obj.arrIncentiveStructureMapping.Count > 0 Then
                                isSaved = obj.SaveData(obj, True, trans)
                                If isSaved = False Then
                                    'trans.Rollback()
                                    clsCommon.ProgressBarHide()
                                    Throw New Exception("")
                                End If
                            End If
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    Throw New Exception("Error at Line No" + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub


    Private Sub rmiClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiClose.Click
        Me.Close()
    End Sub
   
    Private Sub frmSaleIncentiveMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled Then
            DeleteData(txtIncentive.Value)
        End If
    End Sub

 

    Private Sub txtRangeUnit__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRangeUnit._MYValidating
        qry = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER "
        Dim whrCls As String = " "
        txtRangeUnit.Value = clsCommon.ShowSelectForm("RangeUOMFinder@SCHMMD", qry, "Code", whrCls, txtRangeUnit.Value, "", isButtonClicked)
        If gv.RowCount > 0 Then
            For ii As Integer = 0 To gv.RowCount - 1
                If clsCommon.myCdbl(gv.Rows(ii).Cells(colToRange).Value) > 0 Then
                    gv.Rows(ii).Cells(colRangeUom).Value = txtRangeUnit.Value
                End If
            Next
        End If
    End Sub

    Private Sub txtIncentiveUnit__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtIncentiveUnit._MYValidating
        qry = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER "
        Dim whrCls As String = " Ltr_Type = 'Y' "
        txtIncentiveUnit.Value = clsCommon.ShowSelectForm("IncentiveUOMFinder@SCHMMD", qry, "Code", whrCls, txtIncentiveUnit.Value, "", isButtonClicked)
        If gv.RowCount > 0 Then
            For ii As Integer = 0 To gv.RowCount - 1
                If clsCommon.myCdbl(gv.Rows(ii).Cells(colToRange).Value) > 0 Then
                    gv.Rows(ii).Cells(colIncentiveUom).Value = txtIncentiveUnit.Value
                End If
            Next
        End If
    End Sub

    Private Sub txtIncentive__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtIncentive._MYValidating
        txtIncentive.Value = clsSaleIncentiveHeader.getFinder("", txtIncentive.Value, isButtonClicked)
        LoadData(txtIncentive.Value, NavigatorType.Current)
    End Sub

    Private Sub txtIncentive__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtIncentive._MYNavigator
        Try
            qry = "select count(*) from TSPL_SALES_INCENTIVE_HEADER where INCENTIVE_CODE='" + txtIncentive.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtIncentive.MyReadOnly = False
            Else
                txtIncentive.MyReadOnly = True
            End If
            LoadData(txtIncentive.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

   

    'Private Sub gv_CurrentCellChanged(sender As Object, e As CurrentCellChangedEventArgs)
    '    If gv.RowCount > 0 Then
    '        Dim intCurrRow As Integer = gv.CurrentRow.Index
    '        If intCurrRow > 0 Then
    '            gv.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
    '            If intCurrRow = gv.Rows.Count - 1 Then
    '                gv.Rows.AddNew()
    '                gv.CurrentRow = gv.Rows(intCurrRow)
    '            End If
    '        Else
    '            gv.CurrentRow.Cells(colLineNo).Value = intCurrRow + 1
    '            gv.CurrentRow.Cells(colLineNo).ReadOnly = False
    '        End If
    '    End If
    'End Sub

    'Private Sub gv_CellValueChanged(sender As Object, e As GridViewCellEventArgs)

    '    If (Not isInsideLoadData) AndAlso isFromLoad = False Then
    '        If clsCommon.myLen(txtRangeUnit.Value) <= 0 Then
    '            clsCommon.MyMessageBoxShow("Pleasse Select Range UOM First", Me.Text)
    '            Return
    '        ElseIf clsCommon.myLen(txtIncentiveUnit.Value) <= 0 Then
    '            clsCommon.MyMessageBoxShow("Pleasse Select Incentive UOM First", Me.Text)
    '            Return
    '        End If
    '        If e.Column Is gv.Columns(colToRange) Then
    '            If clsCommon.myCdbl(gv.Rows(gv.CurrentRow.Index).Cells(colToRange).Value) > 0 Then
    '                If clsCommon.CompairString(gv.Rows.Count, gv.CurrentRow.Index + 1) = CompairStringResult.Equal Then
    '                    gv.Rows.AddNew()
    '                End If
    '                gv.Rows(gv.CurrentRow.Index + 1).Cells(colFromRange).Value = clsCommon.myCdbl(gv.Rows(gv.CurrentRow.Index + 1).Cells(colToRange).Value + 1)
    '                gv.Rows(gv.CurrentRow.Index + 1).Cells(colRangeUom).Value = clsCommon.myCstr(txtRangeUnit.Value)
    '                gv.Rows(gv.CurrentRow.Index + 1).Cells(colIncentiveUom).Value = clsCommon.myCstr(txtIncentiveUnit.Value)
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub txtItemSturcture__My_Click(sender As Object, e As EventArgs) Handles txtItemSturcture._My_Click
        Try
            Dim qry = "select distinct Structure_Code as Code, Structure_Descq as Name from tspl_Structure_Master "
            txtItemSturcture.arrValueMember = clsCommon.ShowMultipleSelectForm("FND@Structure", qry, "Code", "Name", txtItemSturcture.arrValueMember, txtItemSturcture.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Try
            Dim qry = "select distinct TSPL_CUSTOMER_MASTER.Cust_Code  as Code ,TSPL_CUSTOMER_MASTER.CUSTOMER_NAME as Name,TSPL_CUSTOMER_MASTER.add1 as Address,TSPL_CUSTOMER_MASTER.Zone_Code as [Zone Code],TSPL_ZONE_MASTER.Description as Zone from TSPL_CUSTOMER_MASTER left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code"
            txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("FND@Customer", qry, "Code", "NAME", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
 
    'Private Sub gv_CellValueChanged_1(sender As Object, e As GridViewCellEventArgs)
    '    Try
    '        If (Not isInsideLoadData) Then
    '            If Not isCellValueChangedOpen Then
    '                isCellValueChangedOpen = True
    '                If e.Column Is gv.Columns(colFromRange) Then
    '                    FillUom(False)
    '                ElseIf e.Column Is gv.Columns(colToRange) Then
    '                    FillUom(False)
    '                    gv.Rows(gv.CurrentRow.Index + 1).Cells(colFromRange).Value = clsCommon.myCdbl(gv.Rows(gv.CurrentRow.Index).Cells(colToRange).Value + 1)
    '                End If
    '                isCellValueChangedOpen = False
    '            End If
    '        End If
    '    Catch ex As Exception
    '        isCellValueChangedOpen = False
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Sub FillUom(ByVal isButtonClick As Boolean)
        gv.CurrentRow.Cells(colRangeUom).Value = txtRangeUnit.Value
        gv.CurrentRow.Cells(colIncentiveUom).Value = txtIncentiveUnit.Value
        If gv.CurrentRow.Index = 0 Then
            gv.CurrentRow.Cells(colFromRange).ReadOnly = False
            If gv.Rows.Count > 1 Then
                ' If String.IsNullOrEmpty(clsCommon.myCstr(gv.Rows(gv.CurrentRow.Index + 1).Cells(colFromRange).Value)) = False Then
                If String.IsNullOrEmpty(gv.Rows(gv.CurrentRow.Index).Cells(colToRange).Value) = True Then
                    gv.Rows(gv.CurrentRow.Index).Cells(colToRange).Value = 999999.0
                End If

                'End If

            End If
        Else
            gv.CurrentRow.Cells(colFromRange).ReadOnly = True
        End If
    End Sub

    Private Sub gv_UserDeletedRow(sender As Object, e As GridViewRowEventArgs)
        For ii As Integer = 1 To gv.Rows.Count
            gv.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

   

    Private Sub gv_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs)
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    'Private Sub gv_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs)
    '    If gv.RowCount > 0 Then
    '        Dim intCurrRow As Integer = gv.CurrentRow.Index
    '        gv.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
    '        If intCurrRow = gv.Rows.Count - 1 Then
    '            gv.Rows.AddNew()
    '            gv.CurrentRow = gv.Rows(intCurrRow)
    '        End If
    '    End If
    'End Sub

   
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            SaveData(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData(txtIncentive.Value)
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        postData()
    End Sub
    Sub postData()
        Try
            If (myMessages.postConfirm()) Then
                SaveData(True)
                clsSaleIncentiveHeader.postData(txtIncentive.Value)
                clsCommon.MyMessageBoxShow("Successfully Posted")
                LoadData(txtIncentive.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv.Columns(colFromRange) Then
                        FillUom(False)
                    ElseIf e.Column Is gv.Columns(colToRange) Then
                        FillUom(False)
                        gv.Rows(gv.CurrentRow.Index + 1).Cells(colFromRange).Value = clsCommon.myCdbl(gv.Rows(gv.CurrentRow.Index).Cells(colToRange).Value + 0.01)
                        If String.IsNullOrEmpty(clsCommon.myCstr(gv.Rows(gv.CurrentRow.Index + 1).Cells(colToRange).Value)) = True Then
                            gv.Rows.AddNew()
                            gv.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(gv.CurrentRow.Index + 1)
                            gv.Rows(gv.CurrentRow.Index + 1).Cells(colToRange).Value = 999999.0
                            gv.CurrentRow.Cells(colRangeUom).Value = txtRangeUnit.Value
                            gv.CurrentRow.Cells(colIncentiveUom).Value = txtIncentiveUnit.Value
                        End If

                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv_CurrentCellChanged(sender As Object, e As CurrentCellChangedEventArgs) Handles gv.CurrentCellChanged
        'If gv.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gv.CurrentRow.Index
        '    If intCurrRow > 0 Then
        '        gv.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        '        If intCurrRow = gv.Rows.Count - 1 Then
        '            gv.Rows.AddNew()
        '            gv.CurrentRow = gv.Rows(intCurrRow)
        '        End If
        '    Else
        '        gv.CurrentRow.Cells(colLineNo).Value = intCurrRow + 1
        '        gv.CurrentRow.Cells(colLineNo).ReadOnly = False
        '    End If
        'End If
    End Sub

    Private Sub gv_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv.CellFormatting
        If IsLoaded = True Then
            If e.Column Is gv.Columns(colFromRange) OrElse e.Column Is gv.Columns(colToRange) OrElse e.Column Is gv.Columns(colIncentiveValue) Then
                If clsCommon.CompairString(gv.CurrentRow.Index, 0) = CompairStringResult.Equal Then
                    gv.CurrentRow.Cells(colFromRange).ReadOnly = False
                Else
                    gv.CurrentRow.Cells(colFromRange).ReadOnly = True
                End If
                If String.IsNullOrEmpty(clsCommon.myCstr(gv.CurrentRow.Cells(colFromRange).Value)) = True Then
                    gv.CurrentRow.Cells(colToRange).ReadOnly = True
                Else
                    gv.CurrentRow.Cells(colToRange).ReadOnly = False

                End If
            End If
        End If
    End Sub

    Private Sub gv_CurrentRowChanged(sender As Object, e As CurrentRowChangedEventArgs) Handles gv.CurrentRowChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv.Rows.Count - 1 Then
                'gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub txtGLAccount__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtGLAccount._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        txtGLAccount.Value = clsCommon.ShowSelectForm("fndIGLAccount", Qry, "Account_Code", "", txtGLAccount.Value, "Account_Code", isButtonClicked)
        lblGLAccount.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + txtGLAccount.Value + "' ")
    End Sub

    Private Sub chkInactive_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkInactive.ToggleStateChanged
        Try
            If Not isInsideLoadData Then
                If chkInactive.Checked Then
                    If clsCommon.myLen(txtIncentive.Value) > 0 Then
                        If clsCommon.MyMessageBoxShow("Current Incentive will be getting in active" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, Telerik.WinControls.RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            clsSaleIncentiveHeader.InActiveData(txtIncentive.Value)
                            clsCommon.MyMessageBoxShow("Successfully Incentive the Incentive", Me.Text)
                            LoadData(txtIncentive.Value, NavigatorType.Current)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnUpdates_Click(sender As Object, e As EventArgs) Handles btnUpdates.Click
        Try
            If clsCommon.myLen(txtIncentive.Value) <= 0 Then ''ERO/11/07/19-000683 by balwinder On 12/07/2019
                Throw New Exception("Please select Incentive code")
            End If
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "PWD"
            frm.strCode = "UserPWD"
            frm.ShowDialog()
            If Not frm.isPasswordCorrect Then
                Exit Sub
            End If
            Dim qry = "select TSPL_CUSTOMER_MASTER.Cust_Code  as Code ,TSPL_CUSTOMER_MASTER.CUSTOMER_NAME as Name from TSPL_CUSTOMER_MASTER Where not exists (select 1 from TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING where INCENTIVE_CODE='" + txtIncentive.Value + "' and TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING.CUSTOMER_CODE)"
            Dim arr As ArrayList = clsCommon.ShowMultipleSelectForm(False, "FND@Customer", qry, "Code", "NAME", Nothing, Nothing)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                If clsCommon.MyMessageBoxShow("Add " + clsCommon.myCstr(arr.Count) + " Customer in current Incentive [" + txtIncentive.Value + "]." + Environment.NewLine + " Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Try
                        Dim dt As DateTime = clsCommon.GETSERVERDATE(trans)
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            For Each strvalue As String In arr
                                Dim coll As New Hashtable()
                                Dim strTRCode As String = clsERPFuncationality.GetNextCode(trans, txtIncentiveDate.Value, clsDocType.Detail, clsDocTransactionType.Detail, "")
                                clsCommon.AddColumnsForChange(coll, "TR_CODE", strTRCode)
                                clsCommon.AddColumnsForChange(coll, "INCENTIVE_CODE", txtIncentive.Value)
                                clsCommon.AddColumnsForChange(coll, "CUSTOMER_CODE", strvalue)
                                clsCommon.AddColumnsForChange(coll, "Added_By", objCommonVar.CurrentUserCode)
                                clsCommon.AddColumnsForChange(coll, "Added_On", clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm tt"))
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                            Next
                        End If
                        trans.Commit()
                        clsCommon.MyMessageBoxShow("Customer Added Surressfully", Me.Text)
                        LoadData(txtIncentive.Value, NavigatorType.Current)
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception(ex.Message)
                    End Try
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class


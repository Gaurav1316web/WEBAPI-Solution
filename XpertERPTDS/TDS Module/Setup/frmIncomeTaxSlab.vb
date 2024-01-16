' ------------------------- Created By Preeti Gupta  --------------------'
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Imports XpertERPEngine

Public Class frmIncomeTaxSlab
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode As String
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Const colIT_CODE As String = "IT CODE"
    Const colSlab_From As String = "Slab From"
    Const colSlab_To As String = "Slab To"
    Const colTaxRate As String = "Tax Rate"
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
#End Region

#Region "Functions"
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmIncomeTaxSlab)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        RadMenuItem3.Enabled = MyBase.isModifyFlag
    End Sub
    Function AllowToSave() As Boolean
        btnsave.Focus()
        If clsCommon.myLen(txtcode.Value) <= 0 Then
            myMessages.blankValue("Code ")
            txtcode.Focus()
            txtcode.Select()
            Errorcontrol.SetError(txtcode, "Code")
            Return False
        Else
            Errorcontrol.ResetError(txtcode)
        End If
       
        Dim totalSlab As Integer = 0
        Dim linno As Integer = 0
        'Dim NegativeSlab As Integer = 0
        For Each grow As GridViewRowInfo In gv.Rows
            If clsCommon.myLen(grow.Cells(colSlab_From).Value) > 0 Then
                linno += 1
                If clsCommon.myCdbl(grow.Cells(colSlab_From).Value) >= 0 Then
                    If clsCommon.myLen(grow.Cells(colSlab_To).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Slab to value can not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                        Return False
                    End If
                    If clsCommon.myCdbl(grow.Cells(colSlab_From).Value) >= clsCommon.myCdbl(grow.Cells(colSlab_To).Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Slab to value must be greater than slab from at line no. " + clsCommon.myCstr(linno) + ".")
                        Return False
                    End If
                    If clsCommon.myCdbl(grow.Cells(colSlab_From).Value) = clsCommon.myCdbl(grow.Cells(colSlab_To).Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Slab to value must be greater than slab from at line no. " + clsCommon.myCstr(linno) + ".")
                        Return False
                    End If
                    If clsCommon.myCdbl(grow.Cells(colSlab_To).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Slab to value can not be negative at line no. " + clsCommon.myCstr(linno) + ".")
                        Return False
                    End If
                    If clsCommon.myLen(grow.Cells(colTaxRate).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Tax rate can not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                        Return False
                    End If
                    If clsCommon.myCdbl(grow.Cells(colTaxRate).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Tax rate can not be negative or incorrect at line no. " + clsCommon.myCstr(linno) + ".")
                        Return False
                    End If
                    If clsCommon.myLen(cmbappliedfor.Text) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Applied for can not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                        Return False
                    End If
                    totalSlab = totalSlab + 1
                Else
                    clsCommon.MyMessageBoxShow(Me, "Slab from value can not be negative at line no. " + clsCommon.myCstr(linno) + ".")
                    Return False
                End If
            End If
        Next
        If totalSlab <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Slab from value can not be left blank.", Me.Text)
            Return False
        End If
        Return True
    End Function
    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        TxtDesp.Text = ""
        Me.gv.Rows.Clear()
        Me.gv.Rows.AddNew()

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
    End Sub
    Sub LoadGridColumns()
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()

        gv.ReadOnly = False
        'Dim IT_CODE As New GridViewTextBoxColumn
        Dim Slab_From As New GridViewDecimalColumn
        Dim Slab_To As New GridViewDecimalColumn
        Dim Tax_Rate As New GridViewTextBoxColumn

        Slab_From.FormatString = ""
        Slab_From.HeaderText = "Slab From"
        Slab_From.Name = colSlab_From
        Slab_From.Width = 100
        Slab_From.ReadOnly = False
        Slab_From.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.Columns.Add(Slab_From)

        Slab_To.FormatString = ""
        Slab_To.HeaderText = "Slab To"
        Slab_To.Name = colSlab_To
        Slab_To.Width = 100
        Slab_To.ReadOnly = False
        Slab_To.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.Columns.Add(Slab_To)

        Tax_Rate.FormatString = ""
        Tax_Rate.HeaderText = "Income Tax Rate"
        Tax_Rate.Name = colTaxRate
        Tax_Rate.Width = 100
        Tax_Rate.ReadOnly = False
        Tax_Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.Columns.Add(Tax_Rate)

    End Sub
    Sub LoadAppliedFor()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "G"
        dr("Name") = "General"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "L"
        dr("Name") = "Ladies"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "SC"
        dr("Name") = "Senior Citizen"
        dt.Rows.Add(dr)

        cmbappliedfor.DataSource = dt
        cmbappliedfor.ValueMember = "Code"
        cmbappliedfor.DisplayMember = "Name"
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'txtCode.MyReadOnly = True
        btnsave.Enabled = True

        Dim obj As New ClsIncomeTaxSlab()
        obj = ClsIncomeTaxSlab.GetData(strCode, NavTyep)


        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.IT_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            btndelete.Enabled = True
            btndelete.Enabled = True
            txtCode.Value = obj.IT_CODE
            TxtDesp.Text = obj.Description
            cmbappliedfor.SelectedValue = obj.Applied_For
            txtCode.MyReadOnly = True
            Dim ii As Int16 = 0
            If obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0 Then
                LoadGridColumns()
                For Each objTr As ClsIncomeTaxSlabDetail In obj.ObjList
                    gv.Rows.AddNew()
                    ii = ii + 1
                    gv.Rows(gv.Rows.Count - 1).Cells(colSlab_From).Value = objTr.Slab_From
                    gv.Rows(gv.Rows.Count - 1).Cells(colSlab_To).Value = objTr.Slab_To
                    gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate).Value = objTr.Tax_Rate
                Next
            End If
        Else
            isNewEntry = True
            funReset()
            Me.gv.Rows.Clear()
            Me.gv.Rows.AddNew()
        End If
    End Sub
    Public Function Save()
        If AllowToSave() Then
            Dim obj As New ClsIncomeTaxSlab()
            obj.IT_CODE = txtCode.Value
            obj.Description = TxtDesp.Text
            obj.Applied_For = cmbappliedfor.SelectedValue
            obj.ObjList = New List(Of ClsIncomeTaxSlabDetail)
            For Each grow As GridViewRowInfo In gv.Rows
                Dim objTr As New ClsIncomeTaxSlabDetail()
                objTr.IT_CODE = clsCommon.myCstr(Me.txtCode.Value)
                objTr.Slab_From = clsCommon.myCstr(grow.Cells(colSlab_From).Value)
                objTr.Slab_To = clsCommon.myCdbl(grow.Cells(colSlab_To).Value)
                objTr.Tax_Rate = clsCommon.myCstr(grow.Cells(colTaxRate).Value)
                obj.ObjList.Add(objTr)
            Next
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If (obj.SaveData(obj, isNewEntry, trans)) Then
                trans.Commit()
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadData(obj.IT_CODE, NavigatorType.Current)
                btnsave.Text = "Update"
                btndelete.Enabled = True
            Else
                btnsave.Text = "Save"
                btndelete.Enabled = False
                trans.Rollback()
            End If
        End If
        Return True
    End Function
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Code could not found to delete", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsIncomeTaxSlab.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
#End Region
#Region "Events"



    Private Sub frmIncomeTaxSlab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        isNewEntry = True
        LoadAppliedFor()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        funReset()
    End Sub

    Private Sub frmIncomeTaxSlab_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click

    End Sub

    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click

    End Sub

    Private Sub fndcode__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType) Handles txtcode._MYNavigator
        Try
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcode._MYValidating
        Dim str As String = "select count(*) from TSPL_INCOME_TAX_SLAB where IT_CODE ='" + txtcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtcode.MyReadOnly = False
        Else
            txtcode.MyReadOnly = True
        End If
        If txtcode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select IT_Code As [Code],Description As [Description] from TSPL_INCOME_TAX_SLAB"
            txtcode.Value = clsCommon.ShowSelectForm("TSPL_INCOME_TAX_SLAB", qry, "Code", "", txtcode.Value, "TSPL_INCOME_TAX_SLAB.IT_Code", isButtonClicked)
            If clsCommon.myLen(txtcode.Value) > 0 Then
                Dim objOT As ClsIncomeTaxSlab
                objOT = ClsIncomeTaxSlab.GetData(txtcode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtcode.Value, NavigatorType.Current)
                End If
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
#End Region

    Private Sub gv_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

   
    Private Sub rmExportHead_Click(sender As Object, e As EventArgs) Handles rmExportHead.Click
        Dim str As String

        str = "Select IT_CODE As [IT CODE],DESCRIPTION  As [DESCRIPTION],Applied_For As [Applied For] From TSPL_INCOME_TAX_SLAB"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub rmExportDetail_Click(sender As Object, e As EventArgs) Handles rmExportDetail.Click
        Dim strDetail As String

        strDetail = "select IT_CODE As [IT CODE],SLAB_FROM As [SLAB FROM],SLAB_TO As [SLAB TO],TAX_RATE As [TAX RATE] from TSPL_INCOME_TAX_SLAB_DETAIL"
        transportSql.ExporttoExcel(strDetail, Me)
    End Sub

    Private Sub rmImportHead_Click(sender As Object, e As EventArgs) Handles rmImportHead.Click
        Dim gv As New RadGridView()
        Dim isSaved As Boolean = True
        Dim obj As ClsIncomeTaxSlab
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "IT CODE", "DESCRIPTION", "Applied For") Then
            Dim linno As Integer = 0
            Dim trans As SqlTransaction
            trans = clsDBFuncationality.GetTransactin()
            Try

                clsCommon.ProgressBarShow()
                '''' Master ''''
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New ClsIncomeTaxSlab
                    linno += 1

                    Dim strcode As String = clsCommon.myCstr(grow.Cells("IT CODE").Value)
                    If clsCommon.myLen(strcode) > 0 Then


                        If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 30 Then
                            Throw New Exception("Length of IT Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.IT_CODE = strcode

                        'If clsCommon.myLen(strcode) > 0 Then
                        '    Dim qry As String = "select IT_CODE from TSPL_INCOME_TAX_SLAB where IT_CODE='" + strcode + "'"
                        '    Dim IT_CODE As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        '    If clsCommon.myLen(IT_CODE) <= 0 Then
                        '        Throw New Exception("Please Fill IT Code For Income Tax Slab [" + strcode + "] Or Make Income Tax Slab Entry First")
                        '    End If
                        'End If

                        Dim strType As String = clsCommon.myCstr(grow.Cells("DESCRIPTION").Value)
                        If Not String.IsNullOrEmpty(strType) Then
                            If clsCommon.myLen(clsCommon.myCstr(strType)) > 100 Then
                                Throw New Exception("Length of description should be max. 100 character at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                        obj.Description = strType

                        Dim strAppFor As String = clsCommon.myCstr(grow.Cells("Applied For").Value)
                        If (String.IsNullOrEmpty(strAppFor)) Or clsCommon.myLen(strAppFor) > 50 Then
                            Throw New Exception("Length of applied for should be max. 50 character at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If clsCommon.myLen(strAppFor) > 0 Then
                            If clsCommon.CompairString(strAppFor.ToUpper().Trim(), "G") = CompairStringResult.Equal Or clsCommon.CompairString(strAppFor.ToUpper().Trim(), "L") = CompairStringResult.Equal Or clsCommon.CompairString(strAppFor.ToUpper().Trim(), "SC") = CompairStringResult.Equal Then
                            Else
                                Throw New Exception("Applied for should be amoung 'G','L','SC'.")
                            End If
                        End If
                        obj.Applied_For = strAppFor.ToUpper().Trim()
                        Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_INCOME_TAX_SLAB WHERE IT_CODE ='" + strcode + "'", trans)

                        Dim coll As New Hashtable()
                        Try
                            clsCommon.AddColumnsForChange(coll, "IT_CODE", strcode)
                            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                            clsCommon.AddColumnsForChange(coll, "Applied_For", obj.Applied_For)
                            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                            clsCommon.AddColumnsForChange(coll, "Modified_By", userCode)
                            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans)))

                            If check <= 0 Then
                                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INCOME_TAX_SLAB", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INCOME_TAX_SLAB", OMInsertOrUpdate.Update, " TSPL_INCOME_TAX_SLAB.IT_CODE='" + strcode + "'", trans)
                            End If

                        Catch ex As Exception
                            myMessages.myExceptions(ex)
                        End Try
                    End If
                Next
                trans.Commit()

                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)

            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)

    End Sub

    Private Sub rmImportDetail_Click(sender As Object, e As EventArgs) Handles rmImportDetail.Click
        Dim gv As New RadGridView()
        Dim isSaved As Boolean = True
        Dim obj As ClsIncomeTaxSlabDetail
        Dim ITCode As String
        'Dim Ratetype As String
        'Dim CriteriaType As String
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "IT CODE", "SLAB FROM", "SLAB TO", "TAX RATE") Then
            Dim linno As Integer = 0
            Dim trans As SqlTransaction
            trans = clsDBFuncationality.GetTransactin()
            Try

                clsCommon.ProgressBarShow()
                For i As Integer = 0 To gv.Rows.Count - 1
                    clsDBFuncationality.ExecuteNonQuery("DELETE FROM TSPL_INCOME_TAX_SLAB_DETAIL where IT_CODE = '" & clsCommon.myCstr(gv.Rows(i).Cells("IT CODE").Value) & "'", trans)
                Next
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New ClsIncomeTaxSlabDetail
                    linno += 1
                    ITCode = clsCommon.myCstr(grow.Cells("IT CODE").Value)
                    If clsCommon.myLen(ITCode) > 0 Then
                        'Throw New Exception("Please Fill IT Code.")

                        If clsCommon.myLen(ITCode) > 0 Then
                            Dim qry As String = "select IT_CODE from TSPL_INCOME_TAX_SLAB where IT_CODE='" + ITCode + "'"
                            Dim IT_CODE As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(IT_CODE) <= 0 Then
                                Throw New Exception("Please check ! IT code[" + ITCode + "] does not exists.Please make it entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If

                        Dim strcode As String = clsCommon.myCstr(grow.Cells("IT CODE").Value)
                        If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 30 Then
                            Throw New Exception("Length of IT code should be max. 30 character at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.IT_CODE = strcode

                        Dim DblFrom As Double = clsCommon.myCdbl(grow.Cells("SLAB FROM").Value)
                        If Not IsNumeric(grow.Cells("SLAB FROM").Value) Or DblFrom < 0 Then
                            Throw New Exception("Please check ! slab from can not be left zero or incorrect at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Slab_From = DblFrom

                        Dim DblTo As Double = clsCommon.myCdbl(grow.Cells("SLAB TO").Value)
                        If Not IsNumeric(grow.Cells("SLAB TO").Value) Or DblTo < 0 Then
                            Throw New Exception("Please check ! slab to can not be left zero or incorrect at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Slab_To = DblTo

                        If clsCommon.myCdbl(DblFrom) >= clsCommon.myCdbl(DblTo) Then
                            Throw New Exception("Slab to value must be greater than slab from at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        If clsCommon.myCdbl(DblFrom) = clsCommon.myCdbl(DblTo) Then
                            Throw New Exception("Slab to value must be greater than slab from at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim DblTaxRate As Double = clsCommon.myCdbl(grow.Cells("TAX RATE").Value)
                        If Not IsNumeric(grow.Cells("TAX RATE").Value) Or DblTaxRate < 0 Then
                            Throw New Exception("Please check ! tax rate can not be left zero or incorrect at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Tax_Rate = DblTaxRate

                        Dim coll As New Hashtable()
                        Try
                            clsCommon.AddColumnsForChange(coll, "IT_CODE", obj.IT_CODE)
                            clsCommon.AddColumnsForChange(coll, "SLAB_FROM", obj.Slab_From)
                            clsCommon.AddColumnsForChange(coll, "SLAB_TO", obj.Slab_To)
                            clsCommon.AddColumnsForChange(coll, "TAX_RATE", obj.Tax_Rate)
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INCOME_TAX_SLAB_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                        Catch ex As Exception
                            myMessages.myExceptions(ex)
                        End Try
                    End If
                Next
                trans.Commit()

                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)

            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)

    End Sub
End Class
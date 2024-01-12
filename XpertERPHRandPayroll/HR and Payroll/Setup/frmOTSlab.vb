'--06/03/2014--form Add By- Panch Raj ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmOTSlab
    Inherits FrmMainTranScreen
   
#Region "Variable"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode As String
    Dim DtDetail As New DataTable

    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    'Const colLineNo As String = "LineNo"
    Const colOT_CODE As String = "colOT_CODE"
    Const colasperActualCalc As String = "colasperActualCalc"
    Const colCRITERIA_TYPE As String = "colCRITERIA_TYPE"
    Const col_FROM As String = "col_FROM"
    Const col_TO As String = "col_TO"
    Const colOT_RATE As String = "colOT_RATE"
    Const colRATE_TYPE As String = "colRATE_TYPE"

#End Region

    Private Sub frmOTSlab_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        funReset()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Private Sub Save()

        If AllowToSave() Then

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmOTSlab, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            Dim arr As New List(Of clsOTSlab)
            Dim obj As New clsOTSlab()
            obj.OT_CODE = txtCode.Value
            obj.SLAB_DESCRIPTION = Me.txtDescription.Text

            obj.ObjList = New List(Of clsOTSlabDetails)
            For Each grow As GridViewRowInfo In gvOTSlab.Rows
                If clsCommon.myLen(grow.Cells(colCRITERIA_TYPE).Value) <= 0 Then
                    Continue For
                End If
                Dim objTr As New clsOTSlabDetails()
                objTr.OT_CODE = clsCommon.myCstr(Me.txtCode.Value)
                objTr.CRITERIA_TYPE = clsCommon.myCstr(grow.Cells(colCRITERIA_TYPE).Value)
                objTr._FROM = clsCommon.myCdbl(grow.Cells(col_FROM).Value)
                objTr._TO = clsCommon.myCdbl(grow.Cells(col_TO).Value)
                objTr.OT_RATE = clsCommon.myCdbl(grow.Cells(colOT_RATE).Value)
                objTr.RATE_TYPE = clsCommon.myCstr(grow.Cells(colRATE_TYPE).Value)
                obj.ObjList.Add(objTr)
            Next
            arr.Add(obj)
            'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If (clsOTSlab.SaveData(arr)) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadData(obj.OT_CODE, NavigatorType.Current)
            End If
        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        Dim obj As New clsOTSlab()
        obj = clsOTSlab.GetData(strCode, NavTyep)


        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.OT_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            txtCode.Value = obj.OT_CODE
            txtDescription.Text = obj.SLAB_DESCRIPTION
            chkIS_ASPER_ACTUAL_CALC.Checked = IIf(obj.IS_ASPER_ACTUAL_CALC = 1, True, False)
            txtCode.MyReadOnly = True
            Dim ii As Int16 = 0
            If obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0 Then
                LoadGridColumns()
                For Each objTr As clsOTSlabDetails In obj.ObjList
                    gvOTSlab.Rows.AddNew()
                    ii = ii + 1
                    gvOTSlab.Rows(gvOTSlab.Rows.Count - 1).Cells(colCRITERIA_TYPE).Value = objTr.CRITERIA_TYPE
                    gvOTSlab.Rows(gvOTSlab.Rows.Count - 1).Cells(col_FROM).Value = objTr._FROM
                    gvOTSlab.Rows(gvOTSlab.Rows.Count - 1).Cells(col_TO).Value = objTr._TO
                    gvOTSlab.Rows(gvOTSlab.Rows.Count - 1).Cells(colOT_RATE).Value = objTr.OT_RATE
                    gvOTSlab.Rows(gvOTSlab.Rows.Count - 1).Cells(colRATE_TYPE).Value = objTr.RATE_TYPE
                Next
            End If
        Else
            isNewEntry = True
            Me.gvOTSlab.Rows.Clear()
            Me.gvOTSlab.Rows.AddNew()
        End If
    End Sub

    Function AllowToSave() As Boolean

        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue("Code")
            txtCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtDescription.Text) <= 0 Then
            myMessages.blankValue("Description")
            txtDescription.Focus()
            Return False

        End If
        Dim totalSlab As Integer = 0
        For Each grow As GridViewRowInfo In gvOTSlab.Rows
            If clsCommon.myLen(grow.Cells(colCRITERIA_TYPE).Value) <= 0 Then
                Continue For
            End If

            If clsCommon.myCdbl(grow.Cells(col_TO).Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "To Value must be greater than Zero", Me.Text)
                Return False
            End If
            If clsCommon.myCdbl(grow.Cells(col_TO).Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "To Value must be greater than Zero", Me.Text)
                Return False
            End If
            If clsCommon.myCdbl(grow.Cells(colOT_RATE).Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "OT Rate must be greater than Zero", Me.Text)
                Return False
            End If
            If clsCommon.myLen(grow.Cells(colRATE_TYPE).Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Rate Type", Me.Text)
                Return False
            End If
            totalSlab = totalSlab + 1
        Next
        If totalSlab <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Enter at least one OT Slab", Me.Text)
            Return False
        End If
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If

        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsOTSlab.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmOTSlab)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            rmImport.Enabled = True
            rmExport.Enabled = True
        Else
            rmImport.Enabled = False
            rmExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = True
        txtCode.Value = Nothing
        txtCode.Focus()
        txtDescription.Text = ""
        'txtCategoryLevel.Text = ""
        Me.gvOTSlab.Rows.Clear()
        Me.gvOTSlab.Rows.AddNew()

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        'Dim str As String = "select count(*) from TSPL_OT_SLAB where OT_CODE ='" + txtCode.Value + "' "
        'Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        'If no = 0 AndAlso isButtonClicked = False Then
        '    txtCode.MyReadOnly = True
        '    'txtCode.Value = ""
        '    '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        'Else
        '    txtCode.MyReadOnly = True
        'End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = " select TSPL_OT_MASTER.OT_CODE as Code,  COALESCE(TSPL_OT_SLAB.SLAB_DESCRIPTION,TSPL_OT_MASTER.DESCRIPTION) AS SLAB_DESCRIPTION  from TSPL_OT_MASTER LEFT JOIN TSPL_OT_SLAB ON TSPL_OT_MASTER.OT_CODE=TSPL_OT_SLAB.OT_CODE "
            'txtCode.Value = clsCommon.ShowSelectForm("TSPL_OT_SLAB", qry, "Code", "", txtCode.Value, "TSPL_OT_MASTER.OT_CODE", isButtonClicked)
            txtCode.Value = clsOTSlab.getFinder("", txtCode.Value, isButtonClicked)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                Dim objOT As clsOTMaster
                objOT = clsOTMaster.GetData(txtCode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    txtDescription.Text = objOT.Description
                    chkIS_ASPER_ACTUAL_CALC.Checked = IIf(objOT.IS_ASPER_ACTUAL_CALC = 1, True, False)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If

            Else
                funReset()

            End If
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmOTSlab_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Sub LoadGridColumns()
        gvOTSlab.DataSource = Nothing
        gvOTSlab.Rows.Clear()
        gvOTSlab.Columns.Clear()

        gvOTSlab.ReadOnly = False
        'Dim OT_CODE As New GridViewTextBoxColumn
        Dim CRITERIA_TYPE As New GridViewComboBoxColumn
        Dim _FROM As New GridViewDecimalColumn
        Dim _TO As New GridViewDecimalColumn
        Dim OT_RATE As New GridViewDecimalColumn
        Dim RATE_TYPE As New GridViewComboBoxColumn

        CRITERIA_TYPE.FormatString = ""
        CRITERIA_TYPE.HeaderText = "Criteria Type"
        CRITERIA_TYPE.Name = colCRITERIA_TYPE
        CRITERIA_TYPE.Width = 100
        CRITERIA_TYPE.ReadOnly = False
        CRITERIA_TYPE.DataSource = clsOTSlabDetails.GetOTCriteriaType()
        CRITERIA_TYPE.DisplayMember = "Name"
        CRITERIA_TYPE.ValueMember = "Code"
        gvOTSlab.Columns.Add(CRITERIA_TYPE)

        _FROM.FormatString = ""
        _FROM.HeaderText = "From"
        _FROM.Name = col_FROM
        _FROM.Width = 100
        _FROM.ReadOnly = False
        _FROM.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvOTSlab.Columns.Add(_FROM)

        _TO.FormatString = ""
        _TO.HeaderText = "To"
        _TO.Name = col_TO
        _TO.Width = 100
        _TO.ReadOnly = False
        _TO.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvOTSlab.Columns.Add(_TO)

        OT_RATE.FormatString = ""
        OT_RATE.HeaderText = IIf(Me.chkIS_ASPER_ACTUAL_CALC.Checked, "Rate Multiplier", "OT Rate")
        OT_RATE.Name = colOT_RATE
        OT_RATE.Width = 100
        OT_RATE.ReadOnly = False
        OT_RATE.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvOTSlab.Columns.Add(OT_RATE)

        RATE_TYPE.FormatString = ""
        RATE_TYPE.HeaderText = "Rate Type"
        RATE_TYPE.Name = colRATE_TYPE
        RATE_TYPE.Width = 100
        RATE_TYPE.ReadOnly = False
        RATE_TYPE.DataSource = clsOTSlabDetails.GetOTRateType()
        RATE_TYPE.DisplayMember = "Name"
        RATE_TYPE.ValueMember = "Code"
        gvOTSlab.Columns.Add(RATE_TYPE)
    End Sub

    Private Sub gvCategoryValues_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvOTSlab.CurrentColumnChanged
        If gvOTSlab.RowCount > 0 Then
            Dim intCurrRow As Integer = gvOTSlab.CurrentRow.Index
            'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvOTSlab.Rows.Count - 1 Then
                gvOTSlab.Rows.AddNew()
                'gvCategoryValues.Rows(gvCategoryValues.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                gvOTSlab.CurrentRow = gvOTSlab.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub rmHeadExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmHeadExport.Click
        Dim str As String

        str = "Select OT_CODE As [OT CODE],SLAB_DESCRIPTION  As [SLAB DESCRIPTION] From TSPL_OT_SLAB"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub rmDetailExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDetailExport.Click
        Dim strDetail As String
        strDetail = "select OT_CODE As [OT CODE],CRITERIA_TYPE As [CRITERIA TYPE],_FROM As [FROM],_TO As [TO],OT_RATE As [OT RATE],RATE_TYPE As [RATE TYPE] from TSPL_OT_SLAB_DETAIL"
        transportSql.ExporttoExcel(strDetail, Me)
    End Sub

    Private Sub rmHeadImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmHeadImport.Click
        Dim gv As New RadGridView()
        Dim isSaved As Boolean = True
        Dim obj As clsOTSlab
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "OT CODE", "SLAB DESCRIPTION") Then
            Dim linno As Integer = 0
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsOTSlab
                    linno += 1

                    Dim strcode As String = clsCommon.myCstr(grow.Cells("OT CODE").Value)
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 30 Then
                        Throw New Exception("Length of OT Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.OT_CODE = strcode

                    If clsCommon.myLen(strcode) > 0 Then
                        Dim qry As String = "select OT_CODE from TSPL_OT_MASTER where OT_CODE='" + strcode + "'"
                        Dim OT_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.myLen(OT_Code) <= 0 Then
                            Throw New Exception("Please Fill OT Code For OT Master [" + strcode + "] Or Make OT Master Entry First")
                        End If
                    End If

                    Dim strType As String = clsCommon.myCstr(grow.Cells("SLAB DESCRIPTION").Value)
                    If (String.IsNullOrEmpty(strType)) Or clsCommon.myLen(strType) > 100 Then
                        Throw New Exception("Length of Slab Description should be max. 100 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    obj.SLAB_DESCRIPTION = strType

                    Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_OT_SLAB WHERE OT_CODE ='" + strcode + "'", trans)

                    Dim coll As New Hashtable()
                    Try
                        clsCommon.AddColumnsForChange(coll, "OT_CODE", strcode)
                        clsCommon.AddColumnsForChange(coll, "SLAB_DESCRIPTION", strType)
                        clsCommon.AddColumnsForChange(coll, "Modified_By", userCode)
                        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans)))

                        If check <= 0 Then
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OT_SLAB", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OT_SLAB", OMInsertOrUpdate.Update, " TSPL_OT_SLAB.OT_CODE='" + strcode + "'", trans)
                        End If
                    Catch ex As Exception

                    End Try
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

    Private Sub rmDetailImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDetailImport.Click
        Dim gv As New RadGridView()
        Dim isSaved As Boolean = True
        Dim obj As clsOTSlabDetails
        Dim OTCode As String
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "OT CODE", "CRITERIA TYPE", "FROM", "TO", "OT RATE", "RATE TYPE") Then
            Dim linno As Integer = 0
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For i As Integer = 0 To gv.Rows.Count - 1
                    clsDBFuncationality.ExecuteNonQuery("DELETE FROM TSPL_OT_SLAB_DETAIL where OT_CODE = '" & clsCommon.myCstr(gv.Rows(i).Cells("OT CODE").Value) & "'", trans)
                Next
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsOTSlabDetails
                    linno += 1
                    OTCode = clsCommon.myCstr(grow.Cells("OT CODE").Value)
                    If clsCommon.myLen(OTCode) <= 0 Then
                        Throw New Exception("Please Fill Scheme Code/Scheme Description")
                    End If

                    If clsCommon.myLen(OTCode) > 0 Then
                        Dim qry As String = "select OT_CODE from TSPL_OT_SLAB where OT_CODE='" + OTCode + "'"
                        Dim OT_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.myLen(OT_Code) <= 0 Then
                            Throw New Exception("Please Fill OT Code For OT Slab [" + OTCode + "] Or Make OT Head Entry First")
                        End If
                    End If

                    Dim strcode As String = clsCommon.myCstr(grow.Cells("OT CODE").Value)
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 30 Then
                        Throw New Exception("Length of OT Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.OT_CODE = strcode

                    Dim strType As String = clsCommon.myCstr(grow.Cells("CRITERIA TYPE").Value)
                    If (String.IsNullOrEmpty(strType)) Or clsCommon.myLen(strType) > 30 Then
                        Throw New Exception("Length of Criteria Type should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.CRITERIA_TYPE = strType

                    If clsCommon.myLen(strType) > 0 Then
                        If clsCommon.CompairString(strType, "OTH") = CompairStringResult.Equal Or clsCommon.CompairString(strType, "Basic") = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("Criteria Type should be amoung 'OTH','Basic'.")
                        End If
                    End If

                    If Not IsNumeric(grow.Cells("FROM").Value) Then
                        Throw New Exception(" Field From Should be Numeric ")
                    End If
                    Dim DblFrom As Double = clsCommon.myCdbl(grow.Cells("FROM").Value)
                    If (String.IsNullOrEmpty(DblFrom)) Or clsCommon.myLen(DblFrom) > 9 Then
                        Throw New Exception("Length of From should be max. 9 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj._FROM = DblFrom

                    If Not IsNumeric(grow.Cells("TO").Value) Then
                        Throw New Exception(" Field To Should be Numeric ")
                    End If
                    Dim DblTo As Double = clsCommon.myCdbl(grow.Cells("TO").Value)
                    If (String.IsNullOrEmpty(DblTo)) Or clsCommon.myLen(DblTo) > 9 Then
                        Throw New Exception("Length of To should be max. 9 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj._TO = DblTo

                    If Not IsNumeric(grow.Cells("OT RATE").Value) Then
                        Throw New Exception(" Field OT Rate Should be Numeric ")
                    End If
                    Dim DblOTRate As Double = clsCommon.myCdbl(grow.Cells("OT RATE").Value)
                    If (String.IsNullOrEmpty(DblOTRate)) Or clsCommon.myLen(DblOTRate) > 9 Then
                        Throw New Exception("Length of OT Rate should be max. 9 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.OT_RATE = DblOTRate


                    Dim strRateType As String = clsCommon.myCstr(grow.Cells("RATE TYPE").Value)
                    If (String.IsNullOrEmpty(strRateType)) Or clsCommon.myLen(strRateType) > 30 Then
                        Throw New Exception("Length of Rate Type should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.RATE_TYPE = strRateType

                    If clsCommon.myLen(strRateType) > 0 Then
                        If clsCommon.CompairString(strRateType, "PBS") = CompairStringResult.Equal Or clsCommon.CompairString(strRateType, "PGS") = CompairStringResult.Equal Or clsCommon.CompairString(strRateType, "POTH") = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("Rate Type should be amoung 'PBS','PGS' OR 'POTH'.")
                        End If
                    End If

                    Dim coll As New Hashtable()
                    Try
                        clsCommon.AddColumnsForChange(coll, "OT_CODE", strcode)
                        clsCommon.AddColumnsForChange(coll, "CRITERIA_TYPE", strType)
                        clsCommon.AddColumnsForChange(coll, "_FROM", DblFrom)
                        clsCommon.AddColumnsForChange(coll, "_TO", DblTo)
                        clsCommon.AddColumnsForChange(coll, "OT_RATE", DblOTRate)
                        clsCommon.AddColumnsForChange(coll, "RATE_TYPE", strRateType)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OT_SLAB_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    Catch ex As Exception
                    End Try
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

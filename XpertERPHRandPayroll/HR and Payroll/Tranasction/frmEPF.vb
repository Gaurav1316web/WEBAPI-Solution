Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Public Class frmEPF
    Inherits FrmMainTranScreen
#Region "Variables"
    Const colLineNo As String = "colLineNo"
    Const colEMP_Code As String = "colEMP_Code"
    Const colEMP_Desc As String = "colEMP_Desc"
    Const colMEDLI As String = "colMEDLI"
    Const COLMNACEDLI As String = "COLMNACEDLI"
    Const COLMXACEDLI As String = "COLMXACEDLI"
    Const COLMACEPF As String = "MACEPF"
    Const colTAX_GROUP_Desc As String = "colTAX_GROUP_Desc"
    Const colCEPFAC01 As String = "colCEPFAC01"
    Const colActualAmt As String = "colactualamt"
    Const colheadValue As String = "coheadValue"
    Const colCEPSAC10 As String = "colTAX1_Rate"
    Const colCEPpacepf As String = "colCEPpacepf"
    Const colAC As String = "colAC"
    Const colEDLICAC22 As String = "colEDLICAC22"
    Const colEMPEPF As String = "colEMPEPF"
    Const colEMPEPFMAX As String = "colEMPEPFMAX"
    Const colOTHERCHARGES As String = "coloTHERCHARGES"
    Const colOCM As String = "colOCM"
    Const colMEPSAMT As String = "colMEPSAMT"
    Dim userCode, companyCode As String
    Private isCellValueChanged As Boolean = False
    Dim isImport As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ErrorControl As clsErrorControl = New clsErrorControl()
    Dim isNewEntry As Boolean = True
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim chkPostClick As Boolean = False
    Dim dtpFrom As Date
    Dim dtpTo As Date
#End Region
    Private Sub frmEPF_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
        gv1.Rows.AddNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        gv1.Enabled = True
        SetUserMgmtNew()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Public Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnreverse.Visible = False
    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " Location_Type='Physical' And LOCATION_CODE='" + LocCode + "'"
            Else
                whrcls = " Location_Type='Physical' "
            End If
        End If
        txtLocation.Value = clsLocation.getFinder(whrcls, Me.txtLocation.Value, isButtonClicked)
        lblLocationDesc.Text = clsLocation.GetName(txtLocation.Value, Nothing)
    End Sub

    Private Sub fndPayperiod__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPayperiod._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS Code,(DATEDIFF(DAY,date_from,date_to)+1) as Totaldays, " _
    & " PAY_PERIOD_NAME as Name FROM TSPL_PAYPERIOD_MASTER"
        'Dim qry As String = "select PAY_PERIOD_CODE as Code , PAY_PERIOD_NAME as Name, DATE_FROM as 'From Date', DATE_TO AS 'To Date', DESCRIPTION as Description  from TSPL_PAYPERIOD_MASTER"
        fndPayperiod.Value = clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1 and FREEZED=0 and convert(date, date_from,103) <= Convert (date,SYSDATETIME(),103)", fndPayperiod.Value, "PAY_PERIOD_CODE", isButtonClicked)
        If clsCommon.myLen(fndPayperiod.Value) > 0 Then
            Dim clspp As clsPayPeriodMaster
            clspp = clsPayPeriodMaster.GetData(fndPayperiod.Value, NavigatorType.Current)
            lblPayPeriodName.Text = clspp.Name
            dtpFrom = clspp.DATE_FROM
            dtpTo = clspp.DATE_TO
        Else
            lblPayPeriodName.Text = ""
        End If
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Reset()
        gv1.Rows.AddNew()
    End Sub
    Sub Reset()
        txtCode.Value = ""
        fndPayperiod.Value = ""
        txtLocation.Value = ""
        lblLocationDesc.Text = ""
        btndelete.Enabled = False
        btnPost.Enabled = False
        btnsave.Enabled = True
        LoadBlankGrid()
        isNewEntry = True
        btnsave.Text = "Save"
        fndPayperiod.Value = ""
        txtdocdate.Value = clsCommon.GETSERVERDATE()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtRemaks.Text = ""
        lblPayPeriodName.Text = ""
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.IsVisible = False
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoEMPCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEMPCode.FormatString = ""
        repoEMPCode.HeaderText = "EMP Code"
        repoEMPCode.Name = colEMP_Code
        repoEMPCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoEMPCode.Width = 90
        gv1.MasterTemplate.Columns.Add(repoEMPCode)

        Dim repoEMPDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEMPDesc.FormatString = ""
        repoEMPDesc.HeaderText = "EMP Name"
        repoEMPDesc.Name = colEMP_Desc
        repoEMPDesc.Width = 120
        repoEMPDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoEMPDesc)

        Dim repoActual As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoActual.FormatString = ""
        repoActual.HeaderText = "Actual Amount"
        repoActual.Name = colActualAmt
        repoActual.Width = 120
        repoActual.ReadOnly = False
        repoActual.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoActual)

        Dim repoheadVAlue As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoheadVAlue.FormatString = ""
        repoheadVAlue.HeaderText = "Head Value"
        repoheadVAlue.Name = colheadValue
        repoheadVAlue.Width = 120
        repoheadVAlue.ReadOnly = False
        repoheadVAlue.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoheadVAlue)

        Dim repoCEPFAC1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCEPFAC1.FormatString = ""
        repoCEPFAC1.HeaderText = "C-EPF A/C 01"
        repoCEPFAC1.Name = colCEPFAC01
        repoCEPFAC1.Width = 120
        repoCEPFAC1.ReadOnly = False
        repoCEPFAC1.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoCEPFAC1)

        Dim repoCEPSAC110 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCEPSAC110.FormatString = ""
        repoCEPSAC110.HeaderText = "C-EPS A/C 10"
        repoCEPSAC110.Name = colCEPSAC10
        repoCEPSAC110.Width = 120
        repoCEPSAC110.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCEPSAC110.ReadOnly = False
        repoCEPSAC110.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoCEPSAC110)

        Dim repoADCEPFAC02 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoADCEPFAC02.FormatString = ""
        repoADCEPFAC02.HeaderText = " EPS(ACEPF)- A/C 02"
        repoADCEPFAC02.Name = colCEPpacepf
        repoADCEPFAC02.Width = 120
        repoADCEPFAC02.TextImageRelation = TextImageRelation.TextBeforeImage
        repoADCEPFAC02.ReadOnly = False
        repoADCEPFAC02.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoADCEPFAC02)

        Dim repoAC As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAC.FormatString = ""
        repoAC.HeaderText = "EDLI-C A/C 21"
        repoAC.Name = colAC
        repoAC.Width = 120
        repoAC.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAC.ReadOnly = False
        repoAC.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoAC)

        Dim repoACEDLI As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACEDLI.FormatString = ""
        repoACEDLI.HeaderText = "EDLI(ACEDLI) A/C 22"
        repoACEDLI.Name = colEDLICAC22
        repoACEDLI.Width = 120
        repoACEDLI.TextImageRelation = TextImageRelation.TextBeforeImage
        repoACEDLI.ReadOnly = False
        repoACEDLI.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoACEDLI)


        Dim repoEMPEPDCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEMPEPDCode.FormatString = ""
        repoEMPEPDCode.HeaderText = "EMP-EPF A/C 01"
        repoEMPEPDCode.Name = colEMPEPF
        repoEMPEPDCode.Width = 120
        repoEMPEPDCode.ReadOnly = False
        repoEMPEPDCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoEMPEPDCode)

        Dim repoMAXEPF As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMAXEPF.FormatString = ""
        repoMAXEPF.HeaderText = "Max EPF Amt"
        repoMAXEPF.Name = colEMPEPFMAX
        repoMAXEPF.Width = 120
        repoMAXEPF.TextImageRelation = TextImageRelation.TextBeforeImage
        repoMAXEPF.ReadOnly = False
        repoMAXEPF.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoMAXEPF) '27


        Dim repoOC As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOC.FormatString = ""
        repoOC.HeaderText = "Other Charges(OC)"
        repoOC.Name = colOTHERCHARGES
        repoOC.Width = 120
        repoOC.ReadOnly = False
        repoOC.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoOC) '26

        Dim repoMOC As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMOC.FormatString = ""
        repoMOC.HeaderText = "Max of (OC)"
        repoMOC.Name = colOCM
        repoMOC.Width = 120
        repoMOC.TextImageRelation = TextImageRelation.TextBeforeImage
        repoMOC.ReadOnly = False
        repoMOC.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoMOC)


        Dim repoMEPS As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMEPS.FormatString = ""
        repoMEPS.HeaderText = "Max EPS Amt"
        repoMEPS.Name = colMEPSAMT
        repoMEPS.Width = 120
        repoMEPS.ReadOnly = False
        repoMEPS.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoMEPS)

        Dim repoMACEPF As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMACEPF.FormatString = ""
        repoMACEPF.HeaderText = "Max ACEPF Amt"
        repoMACEPF.Name = COLMACEPF
        repoMACEPF.Width = 120
        repoMACEPF.TextImageRelation = TextImageRelation.TextBeforeImage
        repoMACEPF.ReadOnly = False
        repoMACEPF.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoMACEPF)

        Dim MaxEDLI As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        MaxEDLI.FormatString = ""
        MaxEDLI.HeaderText = "Max EDLI"
        MaxEDLI.Name = colMEDLI
        MaxEDLI.Width = 120
        MaxEDLI.TextImageRelation = TextImageRelation.TextBeforeImage
        MaxEDLI.ReadOnly = False
        MaxEDLI.IsVisible = True
        gv1.MasterTemplate.Columns.Add(MaxEDLI)

        Dim Max_ACEDLI As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Max_ACEDLI.FormatString = ""
        Max_ACEDLI.HeaderText = "Max  ACEDLI"
        Max_ACEDLI.Name = COLMXACEDLI
        Max_ACEDLI.Width = 120
        Max_ACEDLI.TextImageRelation = TextImageRelation.TextBeforeImage
        Max_ACEDLI.ReadOnly = False
        Max_ACEDLI.IsVisible = True
        gv1.MasterTemplate.Columns.Add(Max_ACEDLI)

        Dim min_ACEDLI As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        min_ACEDLI.FormatString = ""
        min_ACEDLI.HeaderText = "Min  ACEDLI"
        min_ACEDLI.Name = COLMNACEDLI
        min_ACEDLI.Width = 120
        min_ACEDLI.TextImageRelation = TextImageRelation.TextBeforeImage
        min_ACEDLI.ReadOnly = False
        min_ACEDLI.IsVisible = True
        gv1.MasterTemplate.Columns.Add(min_ACEDLI)

        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = True
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        ReStoreGridLayout()
    End Sub
    Sub ClearAllCurrentRowFinder()
        gv1.CurrentRow.Cells(colEMP_Code).Value = ""
        gv1.CurrentRow.Cells(colEMP_Desc).Value = ""
        gv1.CurrentRow.Cells(colAC).Value = ""
        gv1.CurrentRow.Cells(colCEPFAC01).Value = ""
        gv1.CurrentRow.Cells(colCEPpacepf).Value = ""
        gv1.CurrentRow.Cells(colCEPSAC10).Value = ""
        gv1.CurrentRow.Cells(colEDLICAC22).Value = ""
        gv1.CurrentRow.Cells(colEMPEPF).Value = ""
        gv1.CurrentRow.Cells(colEMPEPFMAX).Value = ""
        gv1.CurrentRow.Cells(colMEDLI).Value = ""
        gv1.CurrentRow.Cells(colMEPSAMT).Value = ""
        gv1.CurrentRow.Cells(colOCM).Value = ""
        gv1.CurrentRow.Cells(colOTHERCHARGES).Value = ""
        gv1.CurrentRow.Cells(COLMACEPF).Value = ""
        gv1.CurrentRow.Cells(COLMNACEDLI).Value = ""
        gv1.CurrentRow.Cells(COLMXACEDLI).Value = ""
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        If (Not isInsideLoadData) Then
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is gv1.Columns(colEMP_Code) Then
                    OpenEmpList(False)
                End If
            End If
            isCellValueChangedOpen = False
        End If
    End Sub
    Sub OpenEmpList(ByVal isButtonClick As Boolean)
        Try
            'ClearAllCurrentRowFinder()
            Dim qry As String = "SELECT EMP_CODE as code,Emp_Name,Designation,PF_NO FROM TSPL_EMPLOYEE_MASTER"
            gv1.CurrentRow.Cells(colEMP_Code).Value = clsCommon.ShowSelectForm("fndnder21", qry, "code", " Emp_Status<>'Inactive'", clsCommon.myCstr(gv1.CurrentRow.Cells(colEMP_Code).Value), "Code", isButtonClick)
            If clsCommon.myLen(gv1.CurrentRow.Cells(colEMP_Code).Value) > 0 Then
                ADDNewRows()
            End If
            gv1.CurrentRow.Cells(colEMP_Desc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where emp_code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colEMP_Code).Value) & "'"))

        Catch ex As Exception
            gv1.CurrentRow.Cells(colEMP_Code).Value = ""
            gv1.CurrentRow.Cells(colEMP_Desc).Value = ""
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub ADDNewRows()
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData(False)
    End Sub

    Private Sub SaveData(ByVal isPost As Boolean)
        Dim obj As New clsEPF()
        Dim objpd As New clsEPFEntry
        isInsideLoadData = True
        Try
            If AllowToSave() Then
                obj.Doc_Code = clsCommon.myCstr(txtCode.Value)
                obj.DOC_DATE = clsCommon.myCDate(txtdocdate.Text)
                obj.Location_Code = clsCommon.myCstr(txtLocation.Value)
                obj.Pay_period_code = clsCommon.myCstr(fndPayperiod.Value)
                obj.Remarks = clsCommon.myCstr(txtRemaks.Text)
                obj.arr_epfentry = New List(Of clsEPFEntry)
                For Each grow As GridViewRowInfo In gv1.Rows
                    objpd = New clsEPFEntry()
                    objpd.EmployeeCode = (clsCommon.myCstr(grow.Cells(colEMP_Code).Value))
                    objpd.COEPF_A01 = clsCommon.myCDecimal(grow.Cells(colCEPFAC01).Value)
                    objpd.COEPS_AC10 = clsCommon.myCDecimal(grow.Cells(colCEPSAC10).Value)
                    objpd.Adm_EPFACEPF_AC02 = clsCommon.myCDecimal(grow.Cells(colCEPpacepf).Value)
                    objpd.EDLI_COM_AC21 = clsCommon.myCDecimal(grow.Cells(colAC).Value)
                    objpd.Adm_EDLIACEDLI_AC22 = clsCommon.myCDecimal(grow.Cells(colEDLICAC22).Value)
                    objpd.EMP_EPF_AC01 = clsCommon.myCDecimal(grow.Cells(colEMPEPF).Value)
                    objpd.MAX_EPF_AMT = clsCommon.myCDecimal(grow.Cells(colEMPEPFMAX).Value)
                    objpd.OTHER_CHARGES = clsCommon.myCDecimal(grow.Cells(colOTHERCHARGES).Value)
                    objpd.MAX_OTHER_CHARGES = clsCommon.myCDecimal(grow.Cells(colOCM).Value)
                    objpd.MAX_EPS_AMT = clsCommon.myCDecimal(grow.Cells(colMEPSAMT).Value)
                    objpd.MAX_ACEPF_AMT = clsCommon.myCDecimal(grow.Cells(COLMACEPF).Value)
                    objpd.MAX_EDLI = clsCommon.myCDecimal(grow.Cells(colMEDLI).Value)
                    objpd.MIN_ACEDLI = clsCommon.myCDecimal(grow.Cells(COLMXACEDLI).Value)
                    objpd.MAX_ACEDLI = clsCommon.myCDecimal(grow.Cells(COLMNACEDLI).Value)
                    objpd.ActualAmt = clsCommon.myCDecimal(grow.Cells(colActualAmt).Value)
                    objpd.headvalue = clsCommon.myCDecimal(grow.Cells(colheadValue).Value)
                    If clsCommon.myLen(objpd.EmployeeCode) > 0 Then
                        obj.arr_epfentry.Add(objpd)
                    End If
                Next

                If clsEPF.SaveData(obj, isNewEntry, Nothing) Then
                    If Not isPost Then
                        clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
                    End If
                    LoadData(obj.Doc_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            'objpd = Nothing
            obj = Nothing
        End Try
    End Sub
    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (clsEPF.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Private Function AllowToSave() As Boolean 'ByVal isPost As Boolean
        Try
            Dim obj As New clsEPF()
            If AllowFutureDateTransaction(txtdocdate.Value, Nothing) = False Then
                txtdocdate.Focus()
                Return False
            End If
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                txtLocation.Select()
                clsCommon.MyMessageBoxShow(Me, "Select Location", Me.Text)
                Return False
            End If
            If clsCommon.myLen(fndPayperiod.Value) <= 0 Then
                fndPayperiod.Focus()
                fndPayperiod.Select()
                clsCommon.MyMessageBoxShow(Me, "Select Pay Period Code", Me.Text)
                Return False
            End If

            Dim arrpd As New List(Of String)
            Dim Ecode As String = ""
            Dim status As Integer = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Ecode = clsCommon.myCstr(gv1.Rows(ii).Cells(colEMP_Code).Value)
                If Ecode Is Nothing Then
                    clsCommon.MyMessageBoxShow(Me, "Please fill at least one row ", Me.Text)
                    Return False
                End If
                arrpd.Add(Ecode)
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_EPF_ENTRY where Doc_Code ='" + txtCode.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If myMessages.postConfirm() Then
                If (clsEPF.PostData(MyBase.Form_ID, txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnreverse_Click(sender As Object, e As EventArgs) Handles btnreverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' REASON FOR Reverse 
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If

                If clsEPF.ReverseAndUnpost(txtCode.Value) Then
                    saveCancelLog(Reason, "Reverse And Recreate")
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception

            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, Nothing)
    End Function

    Private Sub frmEPF_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "sirc"
                frm.strCode = "sireversandcreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnreverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
            End If
        End If
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = " select doc_code as Code ,doc_date as Date,Case when status=0 then 'Pending' else 'Approved' end as 'Status' from TSPL_EPF_ENTRY"
        LoadData(clsCommon.ShowSelectForm("fndrcode", qry, "Code", "", txtCode.Value, "Code", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim obj As New clsEPF()
        Try
            Reset()
            obj = clsEPF.GetData(strCode, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0 Then
                isInsideLoadData = True
                isNewEntry = False
                txtCode.Value = obj.Doc_Code
                txtdocdate.Value = obj.DOC_DATE
                txtLocation.Value = obj.Location_Code
                lblLocationDesc.Text = obj.Location_desc
                fndPayperiod.Value = obj.Pay_period_code
                lblPayPeriodName.Text = obj.Pay_period_Name
                txtRemaks.Text = obj.Remarks
                UsLock1.Status = obj.Status
                btnsave.Text = "Update"
                If obj.Status = 1 Then
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnPost.Enabled = False
                Else
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                    btnPost.Enabled = True
                End If
            End If
            If obj.arr_epfentry IsNot Nothing AndAlso obj.arr_epfentry.Count > 0 Then
                For Each objtr As clsEPFEntry In obj.arr_epfentry
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEMP_Code).Value = objtr.EmployeeCode
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEMP_Desc).Value = objtr.EmployeeName
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCEPFAC01).Value = objtr.COEPF_A01
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCEPSAC10).Value = objtr.COEPS_AC10
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCEPpacepf).Value = objtr.Adm_EPFACEPF_AC02
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAC).Value = objtr.EDLI_COM_AC21
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEDLICAC22).Value = objtr.Adm_EDLIACEDLI_AC22
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEMPEPF).Value = objtr.EMP_EPF_AC01
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEMPEPFMAX).Value = objtr.MAX_EPF_AMT
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOTHERCHARGES).Value = objtr.OTHER_CHARGES
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOCM).Value = objtr.MAX_OTHER_CHARGES
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMEPSAMT).Value = objtr.MAX_EPS_AMT
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COLMACEPF).Value = objtr.MAX_ACEPF_AMT
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMEDLI).Value = objtr.MAX_EDLI
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COLMXACEDLI).Value = objtr.MIN_ACEDLI
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COLMNACEDLI).Value = objtr.MAX_ACEDLI
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colActualAmt).Value = objtr.ActualAmt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colheadValue).Value = objtr.headvalue
                Next
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            isInsideLoadData = False
        End Try
    End Sub
End Class
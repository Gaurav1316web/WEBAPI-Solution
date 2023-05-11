Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions


Public Class frmContractTanker
    Inherits FrmMainTranScreen
    Public Const colSlNo As String = "SLNO"
    Public Const colValue As String = "Value"
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim QrySheet As String
    Const colVendorCode As String = "colVendorCode"
    Const colVendorDesc As String = "colVendorDesc"

    Sub SetMaxLength()
        txtTankerCode.MyMaxLength = 30
        txtTankerNo.MaxLength = 30
        txtChamborNo.MaxLength = 2
    End Sub
    Sub loadBlankGv()
        Try
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.Columns.Add(colSlNo, "SL.NO")
            gv.Columns.Add(colValue, "Description")
            gv.Columns(colSlNo).Width = 100
            gv.Columns(colSlNo).ReadOnly = True
            gv.Columns(colValue).Width = 250
            gv.AllowAddNewRow = True
            gv.AddNewRowPosition = SystemRowPosition.Bottom
            gv.AllowEditRow = True
            gv.AllowDeleteRow = True
            gv.AllowRowResize = False
            gv.AllowRowReorder = False
            gv.AllowColumnResize = False
            gv.AllowColumnChooser = False
            gv.AllowAutoSizeColumns = False
            gv.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub reset()
        isNewEntry = True
        txtTankerCode.Value = ""
        txtTankerNo.Text = ""
        txtChamborNo.Text = ""
        fndVendorNo.Value = ""
        lblVendorName.Text = ""
        loadBlankGv()
        btnSave.Text = "Save"
        btnDelete.Enabled = True
        txtTankerCode.MyReadOnly = False
        LoadVendor("")
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmContractTanker)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmParameterValueMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            btnSave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmParameterValueMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadVendor("")
        SetUserMgmtNew()
        reset()
        SetMaxLength()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub
    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repocode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocode.Name = colVendorCode
        repocode.Width = 150
        repocode.HeaderText = "Vendor Code"
        repocode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repocode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repocode)


        repocode = New GridViewTextBoxColumn()
        repocode.Name = colVendorDesc
        repocode.Width = 300
        repocode.HeaderText = "Vendor Desc"
        repocode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repocode)


        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = True
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = True
        gv.EnableSorting = True
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False

    End Sub
    Sub LoadVendor(ByVal tankerCode As String)
        GvVendor.DataSource = Nothing
        Dim qry As String = Nothing

        qry = "Select Final.* from (sELECT cast(1 as bit) as Sel,TSPL_CONTRACT_TANKER_VENDOR_DETAIL.Vendor_Code As [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as Description FROM TSPL_CONTRACT_TANKER_VENDOR_DETAIL  left outer join TSPL_VENDOR_MASTER on TSPL_CONTRACT_TANKER_VENDOR_DETAIL.Vendor_Code=TSPL_VENDOR_MASTER.vendor_code  where TSPL_VENDOR_MASTER.Status='N' and TSPL_CONTRACT_TANKER_VENDOR_DETAIL.TANKER_CODE ='" & tankerCode & "' " & _
      " union all " & _
      "  SELECT cast(0 as bit) as Sel,TSPL_VENDOR_MASTER.vendor_code As [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as Description FROM TSPL_VENDOR_MASTER  where  TSPL_VENDOR_MASTER.Status='N' and  TSPL_VENDOR_MASTER.vendor_code not in (sELECT TSPL_CONTRACT_TANKER_VENDOR_DETAIL.Vendor_Code FROM TSPL_CONTRACT_TANKER_VENDOR_DETAIL where TSPL_CONTRACT_TANKER_VENDOR_DETAIL.TANKER_CODE= '" & tankerCode & "' )) Final ORDER BY fINAL.[Vendor Code]  "

        GvVendor.DataSource = clsDBFuncationality.GetDataTable(qry)

        GvVendor.Columns("Sel").HeaderText = " "
        GvVendor.Columns("Sel").Width = 50
        GvVendor.Columns("Sel").ReadOnly = False

        GvVendor.Columns("Vendor Code").HeaderText = "Vendor Code"
        GvVendor.Columns("Vendor Code").Width = 100
        GvVendor.Columns("Vendor Code").ReadOnly = True

        GvVendor.Columns("Description").HeaderText = "Vendor Name"
        GvVendor.Columns("Description").Width = 200
        GvVendor.Columns("Description").ReadOnly = True

        GvVendor.AllowAddNewRow = False
        GvVendor.ShowGroupPanel = False
        GvVendor.AllowColumnReorder = False
        GvVendor.AllowRowReorder = False
        GvVendor.EnableSorting = False
        GvVendor.Enabled = True
        GvVendor.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GvVendor.MasterTemplate.ShowRowHeaderColumn = False
    End Sub
    Function allowToSave() As Boolean
        Try
            If clsCommon.myLen(txtTankerCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please enter Tanker Code")
                txtTankerCode.Focus()
                Return False
            ElseIf clsCommon.myLen(txtTankerNo.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please enter Tanker No")
                txtTankerNo.Focus()
                Return False
            ElseIf clsCommon.myCdbl(txtChamborNo.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please enter Chamber No")
                txtChamborNo.Focus()
                Return False
            End If
            Dim rowno As Integer = -1
            rowno = chkDuplicateValue()
            If rowno > -1 Then
                clsCommon.MyMessageBoxShow("Duplicate value at Row no. " & (rowno + 1))
                Return False
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function


    Private Function SaveData() As Boolean
        Try
            If (allowToSave()) Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.FrmContractTanker, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return False
                    End If
                End If

                Dim obj As New clsContractTankerHead()
                obj.TANKER_CODE = clsCommon.myCstr(txtTankerCode.Value)
                obj.TANKER_NO = clsCommon.myCstr(txtTankerNo.Text)
                obj.NO_OF_CHAMBER = clsCommon.myCdbl(txtChamborNo.Value)
                obj.Vendor_Code = clsCommon.myCstr(fndVendorNo.Value)
                obj.Arr = New List(Of clsContractTankerDetail)
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim objTr As New clsContractTankerDetail()
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSlNo).Value)
                    objTr.CHAMBER_DESC = clsCommon.myCstr(grow.Cells(colValue).Value)
                    If (clsCommon.myLen(objTr.CHAMBER_DESC) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                obj.Arrvendor = New List(Of clsContractTankerVendorDetail)
                For ii As Integer = 0 To GvVendor.Rows.Count - 1
                    If clsCommon.myCBool(GvVendor.Rows(ii).Cells("Sel").Value) Then
                        Dim objVen As New clsContractTankerVendorDetail
                        objVen.Vendor_Code = clsCommon.myCstr(GvVendor.Rows(ii).Cells("Vendor Code").Value)
                        If clsCommon.myLen(objVen.Vendor_Code) > 0 Then
                            obj.Arrvendor.Add(objVen)
                        End If
                    End If
                Next
                obj.isNewEntry = isNewEntry
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Chamber Desc")
                    Return False
                End If
                Dim isSaved As Boolean = obj.SaveData(obj)
                If isSaved Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.TANKER_CODE, NavigatorType.Current)
                End If

                Return isSaved
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function
 Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (clsContractTankerHead.DeleteData(txtTankerCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Function chkDuplicateValue() As Integer
        Dim strValue As String = String.Empty
        For i As Integer = 0 To gv.Rows.Count - 2
            strValue = gv.Rows(i).Cells(colValue).Value.ToString
            For j As Integer = i + 1 To gv.Rows.Count - 1
                If clsCommon.CompairString(gv.Rows(j).Cells(colValue).Value, strValue) = CompairStringResult.Equal Then
                    Return j
                End If
            Next
        Next
        Return -1
    End Function
    Sub SetSerialNo()
        For i As Integer = 0 To gv.Rows.Count - 1
            gv.Rows(i).Cells(colSlNo).Value = (i + 1)
        Next
    End Sub

    Private Sub gv_UserAddedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv.UserAddedRow
        SetSerialNo()
    End Sub

    Private Sub gv_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv.UserDeletedRow
        SetSerialNo()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If allowToSave() Then SaveData()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        deleteData()
    End Sub

    'Sub LoadParameterValues()
    '    loadBlankGv()
    '    If clsCommon.myLen(ddlParamCode.Text) > 0 Then
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Value from tspl_Parameter_value_master where parameter_code='" & ddlParamCode.Text & "'")
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            loadBlankGv()
    '            For i As Integer = 0 To dt.Rows.Count - 1
    '                gv.Rows.AddNew()
    '                gv.Rows(i).Cells(colSlNo).Value = (i + 1)
    '                gv.Rows(i).Cells(colValue).Value = dt.Rows(i)("Value").ToString
    '            Next
    '        End If
    '    End If
    'End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try

            Dim obj As New clsContractTankerHead()
            obj = clsContractTankerHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.TANKER_CODE) > 0) Then
                reset()
                isNewEntry = False
                btnSave.Text = "Update"
                txtTankerCode.MyReadOnly = True
                txtTankerCode.Value = obj.TANKER_CODE
                txtTankerNo.Text = obj.TANKER_NO
                txtChamborNo.Value = obj.NO_OF_CHAMBER
                fndVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsContractTankerDetail In obj.Arr
                        gv.Rows.AddNew()
                        gv.Rows(gv.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colValue).Value = objTr.CHAMBER_DESC
                    Next
                End If
                LoadVendor(txtTankerCode.Value)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            'Finally
            '    isInsideLoadData = False
        End Try
    End Sub
    Private Sub rbtnReset_Click(sender As Object, e As EventArgs) Handles rbtnReset.Click
        reset()
    End Sub

    Private Sub btnGO_Click(sender As Object, e As EventArgs) Handles btnGO.Click
        Try
            If clsCommon.myCdbl(txtChamborNo.Text) = 0 Then
                clsCommon.MyMessageBoxShow("Value of No of Chamber must be >0")
                txtChamborNo.Focus()
                Exit Sub
            End If
            Dim i As Integer = 0
            If clsCommon.myCdbl(txtChamborNo.Value) > gv.Rows.Count Then
                For i = gv.Rows.Count + 1 To clsCommon.myCdbl(txtChamborNo.Value)
                    gv.Rows.AddNew()
                    gv.Rows(i - 1).Cells(colSlNo).Value = i
                    'gv.Rows(i - 1).Cells(colValue).ReadOnly = True
                Next
            ElseIf clsCommon.myCdbl(txtChamborNo.Value) < gv.Rows.Count Then
                For i = gv.Rows.Count - 1 To clsCommon.myCdbl(txtChamborNo.Value) Step -1
                    gv.Rows.RemoveAt(i)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtTankerCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtTankerCode._MYNavigator
        Try
            Dim strwherecls As String = ""
            Dim qst As String = ""
            strwherecls = ""

            qst = "select count(*) from TSPL_CONTRACT_TANKER_MASTER where TANKER_CODE='" + txtTankerCode.Value + "'"

            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtTankerCode.MyReadOnly = False
            Else
                txtTankerCode.MyReadOnly = True
            End If
            LoadData(txtTankerCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtTankerCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTankerCode._MYValidating
        Dim qry = "select count(*) from TSPL_CONTRACT_TANKER_MASTER where TANKER_CODE='" + txtTankerCode.Value + "'"
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If no = 0 Then
            txtTankerCode.MyReadOnly = False
        Else
            txtTankerCode.MyReadOnly = True
        End If
        Dim whrClas As String = ""
        If txtTankerCode.MyReadOnly OrElse isButtonClicked Then
            txtTankerCode.Value = clsContractTankerHead.getFinder("", txtTankerCode.Value, isButtonClicked)
            LoadData(txtTankerCode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub txtTankerNo_Leave(sender As Object, e As EventArgs) Handles txtTankerNo.Leave
        If clsCommon.myLen(txtTankerNo.Text) > 0 Then
            If Not Regex.Match(txtTankerNo.Text, "^[a-zA-Z0-9_]*$", RegexOptions.IgnoreCase).Success Then
                MessageBox.Show("Space Not Allowed in Tanker No!") 'Inform User
                txtTankerNo.Text = ""
                txtTankerNo.Focus()
            End If
        End If
    End Sub

    Private Sub mnuExport_Click(sender As Object, e As EventArgs) Handles mnuExport.Click
        Try
            '----------------------for n-level category-----------------------------------
            Dim code As String = ""
            Dim whrcls As String = ""
 

            QrySheet = " Select TSPL_CONTRACT_TANKER_MASTER.TANKER_CODE as [TANKER CODE],TSPL_CONTRACT_TANKER_MASTER.TANKER_NO as [TANKER NO],TSPL_CONTRACT_TANKER_MASTER.NO_OF_CHAMBER as [NO OF CHAMBER],TSPL_CONTRACT_TANKER_MASTER.Vendor_Code as [Vendor Code] "
            
        
            '' Chamber Details Section
            Dim UOMTotal As String = ""
            Dim UOMConTotal As String = ""
            Dim UOMDefTotal As String = ""
            Dim UOMStockUnitTotal As String = ""

            Dim UOMConversion As String = ""
            Dim UOMDefault As String = ""
            Dim UOMDetail As String = ""
            Dim UOMStockUnit As String = ""
            ''richA AGARWAL  
            Dim UOMWeight As String = ""
            Dim TotalUOMWeight As String = ""
            ''------------

            For j As Integer = 1 To 10
                UOMDetail = "(Select CHAMBER_DESC From (Select ROW_NUMBER () over (order by TSPL_CONTRACT_TANKER_DETAIL.TANKER_CODE,TSPL_CONTRACT_TANKER_DETAIL.CHAMBER_DESC ) As SNo,TSPL_CONTRACT_TANKER_DETAIL.CHAMBER_DESC  From TSPL_CONTRACT_TANKER_DETAIL where TSPL_CONTRACT_TANKER_DETAIL.TANKER_CODE =TSPL_CONTRACT_TANKER_MASTER.TANKER_CODE) xxx where xxx.SNo =" & j & " )  AS ChamberDesc" & j & ""
                UOMTotal = UOMTotal + "," + "" + UOMDetail + ""
            Next

          
            QrySheet += UOMTotal + " from TSPL_CONTRACT_TANKER_MASTER  " + code + ""
            transportSql.ExporttoExcel(QrySheet, whrcls, Me)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub mnuImport_Click(sender As Object, e As EventArgs) Handles mnuImport.Click
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        If transportSql.importExcel(gv1, "TANKER CODE", "TANKER NO", "Vendor Code", "NO OF CHAMBER", "ChamberDesc1", "ChamberDesc2", "ChamberDesc3", "ChamberDesc4", "ChamberDesc5", "ChamberDesc6", "ChamberDesc7", "ChamberDesc8", "ChamberDesc9", "ChamberDesc10") Then
            Dim isSaved As Boolean = True
            Dim currentdate As Date = Date.Today
            Dim trans As SqlTransaction
            Dim gv2 As New RadGridView
            Dim dtt As DataTable = TryCast(gv1.DataSource, DataTable)
            dtt.Columns.Add("ErrorDesc", "".GetType())
            clsCommon.ProgressBarPercentShow()
            Try
                Dim TANKER_CODE As String = ""
                Dim TANKER_NO As String = ""
                Dim Vendor_Code As String = String.Empty
                Dim NO_OF_CHAMBER As Double = 0
                trans = clsDBFuncationality.GetTransactin()
                Try

                    Dim jj As Integer = -1
                    Dim ErrCount As Integer = 0
                    Dim Datee As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
                    For Each grow As GridViewRowInfo In gv1.Rows
                        If (clsCommon.myLen(grow.Cells("TANKER CODE").Value) > 0 And clsCommon.myLen(grow.Cells("TANKER CODE").Value) <= 30) Then
                            Dim LineNo As String = clsCommon.myCstr(grow.Index + 2)
                            jj = jj + 1
                            clsCommon.ProgressBarPercentUpdate(((jj + 1) * 100) / dtt.Rows.Count, " Inserting/Updating Records " & (jj + 1) & " Of Total " & dtt.Rows.Count & " Records , Error Found :" & ErrCount)

                            Dim coll As New Hashtable()
                            TANKER_CODE = clsCommon.myCstr(grow.Cells("TANKER CODE").Value)
                            TANKER_NO = clsCommon.myCstr(grow.Cells("TANKER NO").Value)
                            If clsCommon.myLen(TANKER_NO) > 50 Then
                                'Throw New Exception("Length of item description on line '" + LineNo + "' is greater than 100.")
                                dtt.Rows(jj)("ErrorDesc") = "Length of Tanker No  is greater than 50."

                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            If Not Regex.Match(TANKER_NO, "^[a-zA-Z0-9_]*$", RegexOptions.IgnoreCase).Success Then
                                'Throw New Exception("Length of item description on line '" + LineNo + "' is greater than 100.")
                                dtt.Rows(jj)("ErrorDesc") = "Space Not Allowed in Tanker No!"

                                ErrCount = ErrCount + 1 : GoTo ExitLOOP

                            End If
                            NO_OF_CHAMBER = clsCommon.myCdbl(grow.Cells("NO OF CHAMBER").Value)

                            Vendor_Code = clsCommon.myCstr(grow.Cells("Vendor Code").Value)
                            If clsCommon.myLen(Vendor_Code) > 12 Then
                                dtt.Rows(jj)("ErrorDesc") = "Length of Vendor Code is greater than 12."
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            If clsCommon.myLen(Vendor_Code) > 0 Then
                                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select count(*) from TSPL_VENDOR_MASTER where Vendor_Code ='" & clsCommon.myCstr(Vendor_Code) & "' and TSPL_VENDOR_MASTER.Vendor_Type in ('A','B')  ", trans)) <= 0 Then
                                    dtt.Rows(jj)("ErrorDesc") = "Vendor Code is not exist in vendor master."
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            End If

                            clsCommon.AddColumnsForChange(coll, "TANKER_CODE", TANKER_CODE)
                            clsCommon.AddColumnsForChange(coll, "TANKER_NO", TANKER_NO)
                            clsCommon.AddColumnsForChange(coll, "Vendor_Code", Vendor_Code, True)
                            clsCommon.AddColumnsForChange(coll, "NO_OF_CHAMBER", NO_OF_CHAMBER)
                            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Modified_Date", Datee)

                            Dim Qry = "Select COUNT(*) From TSPL_CONTRACT_TANKER_MASTER Where TANKER_CODE='" + TANKER_CODE + "'"
                            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 0 Then
                                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                                clsCommon.AddColumnsForChange(coll, "Created_Date", Datee)
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CONTRACT_TANKER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CONTRACT_TANKER_MASTER", OMInsertOrUpdate.Update, "TSPL_CONTRACT_TANKER_MASTER.TANKER_CODE='" + TANKER_CODE + "'", trans)
                            End If

                            Dim ChamberDescCount As Integer = 0

                            Qry = "delete from TSPL_CONTRACT_TANKER_DETAIL where TANKER_CODE='" + TANKER_CODE + "'"
                            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                            For j As Integer = 1 To 10
                                Dim colChamberDesc As New Hashtable()
                                Dim CHAMBER_DESC As String = clsCommon.myCstr(grow.Cells("CHAMBERDESC" & clsCommon.myCstr(j) & "").Value)
                                If clsCommon.myLen(CHAMBER_DESC) > 0 Then
                                    ChamberDescCount += 1
                                    clsCommon.AddColumnsForChange(colChamberDesc, "TANKER_CODE", TANKER_CODE)
                                    clsCommon.AddColumnsForChange(colChamberDesc, "Line_No", ChamberDescCount)
                                    clsCommon.AddColumnsForChange(colChamberDesc, "CHAMBER_DESC", CHAMBER_DESC)
                                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(colChamberDesc, "TSPL_CONTRACT_TANKER_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                                End If
                            Next
                            If ChamberDescCount <> NO_OF_CHAMBER Then
                                dtt.Rows(jj)("ErrorDesc") = "Please insert Chamber Desc equal to no of chamber "
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                        End If

ExitLOOP:
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    dtt.DefaultView.RowFilter = "ErrorDesc<>''"
                    dtt = dtt.DefaultView.ToTable
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!" & IIf(dtt.Rows.Count > 0, " Except of  " & dtt.Rows.Count & " Records", ""), Me.Text, MessageBoxButtons.OK)
                    If dtt.Rows.Count > 0 Then
                        Dim ff As New FrmFreeGrid
                        ff.ReportID = "UnImportedItemList"
                        ff.Text = "Record Could not Saved"
                        ff.dt = dtt
                        ff.ShowDialog()
                    End If

                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try
            Catch ex As Exception

                'clsCommon.ProgressBarHide()
                clsCommon.ProgressBarPercentHide()
                'trans.Rollback()
                RadMessageBox.Show(ex.Message)
            Finally
                Me.Controls.Remove(gv1)
            End Try
        End If
    End Sub

    
    Private Sub fndVendorNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndVendorNo._MYValidating
        fndVendorNo.Value = clsVendorMaster.getFinder(" TSPL_VENDOR_MASTER.Vendor_Type in ('A','B') ", fndVendorNo.Value, isButtonClicked)
        If clsCommon.myLen(fndVendorNo.Value) > 0 Then
            lblVendorName.Text = clsVendorMaster.GetName(fndVendorNo.Value, Nothing)
        Else
            lblVendorName.Text = ""
        End If
    End Sub

    Private Sub mnuExportVendor_Click(sender As Object, e As EventArgs) Handles mnuExportVendor.Click
        Try
            Dim qry = "select count(*) from TSPL_CONTRACT_TANKER_VENDOR_DETAIL"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                qry = "select TANKER_CODE as [Tanker Code],Vendor_Code as [Vendor Code] from TSPL_CONTRACT_TANKER_VENDOR_DETAIL"
            Else
                qry = "select '' as [Tanker Code],'' as [Vendor Code] from TSPL_CONTRACT_TANKER_VENDOR_DETAIL"
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub mnuImportVendor_Click(sender As Object, e As EventArgs) Handles mnuImportVendor.Click
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim issaved As Boolean = True

            If transportSql.importExcel(gv, "Tanker Code", "Vendor Code") Then
                clsCommon.ProgressBarShow()
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try

                    Dim TANKER_CODE As String = Nothing
                    Dim Vendor_Code As String = Nothing
                    Dim counter As Integer = 0
                    Dim qry As String = Nothing
                    For Each grow As GridViewRowInfo In gv.Rows
                        TANKER_CODE = clsCommon.myCstr(grow.Cells("Tanker Code").Value)
                        If clsCommon.myLen(TANKER_CODE) > 0 Then
                            qry = "select TANKER_CODE from TSPL_CONTRACT_TANKER_MASTER where TANKER_CODE='" + TANKER_CODE + "'"
                            TANKER_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        Else
                            If clsCommon.myLen(TANKER_CODE) <= 0 Then
                                Throw New Exception("Please Fill tanker Code in header part")
                            End If
                        End If
                        Vendor_Code = clsCommon.myCstr(grow.Cells("Vendor Code").Value)
                        If clsCommon.myLen(Vendor_Code) > 0 Then
                            qry = "select Vendor_Code from TSPL_VENDOR_MASTER where Vendor_Code='" + Vendor_Code + "'"
                            Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        Else
                            Throw New Exception("Please Fill Vendor Code")
                        End If

                        'If counter = 0 Then
                        Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_CONTRACT_TANKER_VENDOR_DETAIL WHERE TANKER_CODE ='" + TANKER_CODE + "' and Vendor_Code='" + Vendor_Code + "'", trans)
                        'End If

                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "TANKER_CODE", TANKER_CODE)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Code", Vendor_Code)
                      
                        If check <= 0 Then
                            issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CONTRACT_TANKER_VENDOR_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CONTRACT_TANKER_VENDOR_DETAIL", OMInsertOrUpdate.Update, " TSPL_CONTRACT_TANKER_VENDOR_DETAIL.TANKER_CODE='" + TANKER_CODE + "' and TSPL_CONTRACT_TANKER_VENDOR_DETAIL.Vendor_Code='" + Vendor_Code + "'", trans)
                        End If

                        counter += 1
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    Throw New Exception(ex.Message)
                End Try
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        If chkVendorAll.IsChecked = True Then
            For ii As Integer = 0 To GvVendor.RowCount - 1
                GvVendor.Rows(ii).Cells("SEL").Value = True
            Next
        Else
            For ii As Integer = 0 To GvVendor.RowCount - 1
                GvVendor.Rows(ii).Cells("SEL").Value = False
            Next
        End If
    End Sub
End Class

Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions


Public Class frmTankerMasterSale
    Inherits FrmMainTranScreen

#Region "Variables"
    Public Const colSlNo As String = "SLNO"
    Public Const colValue As String = "Value"
    Public Const colCapacity As String = "Capacity"
    Public Const colUOM As String = "UOM"
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim QrySheet As String
    Const colDriverCode As String = "colDriverCode"
    Const colDriverDesc As String = "colDriverDesc"
    Public isCellValueChangedOpen As Boolean = False
    Public isInsideLoadData As Boolean = False
#End Region

    Sub SetMaxLength()
        txtTankerCode.MyMaxLength = 30
        txtTankerNo.MaxLength = 30
        txtTareWeight.MaxLength = 30
        txtChamborNo.MaxLength = 2
    End Sub

    Sub loadBlankGv()
        Try
            gv.Rows.Clear()
            gv.Columns.Clear()

            Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoLineNo = New GridViewDecimalColumn()
            repoLineNo.FormatString = ""
            repoLineNo.HeaderText = "Line No"
            repoLineNo.Name = colSlNo
            repoLineNo.Width = 50
            repoLineNo.ReadOnly = True
            repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gv.MasterTemplate.Columns.Add(repoLineNo) '0

            Dim repoDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoDesc.FormatString = ""
            repoDesc.HeaderText = "Description"
            repoDesc.Name = colValue
            repoDesc.Width = 150
            repoDesc.ReadOnly = False
            gv.MasterTemplate.Columns.Add(repoDesc)

            Dim repoCapacity As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoCapacity.FormatString = ""
            repoCapacity.HeaderText = "Capacity"
            repoCapacity.Name = colCapacity
            repoCapacity.Width = 80
            repoCapacity.Minimum = 0
            repoCapacity.ShowUpDownButtons = False
            repoCapacity.Step = 0
            repoCapacity.DecimalPlaces = 3
            repoCapacity.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gv.MasterTemplate.Columns.Add(repoCapacity)

            Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoUnit.FormatString = ""
            repoUnit.HeaderText = "UOM"
            repoUnit.Name = colUOM
            repoUnit.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoUnit.TextImageRelation = TextImageRelation.TextBeforeImage
            repoUnit.Width = 80
            repoUnit.ReadOnly = False
            gv.MasterTemplate.Columns.Add(repoUnit)

            gv.AllowAddNewRow = False
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub reset()
        isNewEntry = True
        txtTankerCode.Value = ""
        txtTankerNo.Text = ""
        txtChamborNo.Text = ""
        txtTareWeight.Text = 0
        loadBlankGv()
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        txtTankerCode.MyReadOnly = False
        LoadDriver("")
        chkVendorAll.IsChecked = True
        GvDriver.Enabled = False
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.TankerMasterSale)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmTankerMasterSale_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            btnSave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub frmTankerMasterSale_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadDriver("")
        SetUserMgmtNew()
        reset()
        SetMaxLength()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub
    
    Sub LoadDriver(ByVal strTankerCode As String)
        GvDriver.DataSource = Nothing
        Dim qry As String = Nothing

        qry = "Select Final.* from (Select cast(1 as bit) as Sel,TSPL_TANKER_MASTER_SALE_DRIVER_DETAIL.Driver_Code As [Driver Code],TSPL_EMPLOYEE_MASTER.Emp_Name as Description FROM TSPL_TANKER_MASTER_SALE_DRIVER_DETAIL  left outer join TSPL_EMPLOYEE_MASTER on TSPL_TANKER_MASTER_SALE_DRIVER_DETAIL.Driver_Code=TSPL_EMPLOYEE_MASTER.EMP_CODE  where TSPL_TANKER_MASTER_SALE_DRIVER_DETAIL.TANKER_CODE ='" & strTankerCode & "'" & _
        " union all" & _
        " SELECT cast(0 as bit) as Sel,TSPL_EMPLOYEE_MASTER.EMP_CODE As [Driver Code],TSPL_EMPLOYEE_MASTER.Emp_Name as Description FROM TSPL_EMPLOYEE_MASTER  where TSPL_EMPLOYEE_MASTER.Emp_type='Driver' and  TSPL_EMPLOYEE_MASTER.EMP_CODE not in (Select TSPL_TANKER_MASTER_SALE_DRIVER_DETAIL.Driver_Code FROM TSPL_TANKER_MASTER_SALE_DRIVER_DETAIL where TSPL_TANKER_MASTER_SALE_DRIVER_DETAIL.TANKER_CODE= '" & strTankerCode & "' )) Final ORDER BY Final.[Driver Code]"

        GvDriver.DataSource = clsDBFuncationality.GetDataTable(qry)

        GvDriver.Columns("Sel").HeaderText = " "
        GvDriver.Columns("Sel").Width = 50
        GvDriver.Columns("Sel").ReadOnly = False

        GvDriver.Columns("Driver Code").HeaderText = "Driver Code"
        GvDriver.Columns("Driver Code").Width = 100
        GvDriver.Columns("Driver Code").ReadOnly = True

        GvDriver.Columns("Description").HeaderText = "Driver Name"
        GvDriver.Columns("Description").Width = 200
        GvDriver.Columns("Description").ReadOnly = True

        GvDriver.AllowAddNewRow = False
        GvDriver.ShowGroupPanel = False
        GvDriver.AllowColumnReorder = False
        GvDriver.AllowRowReorder = False
        GvDriver.EnableSorting = False
        'GvDriver.Enabled = True
        GvDriver.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GvDriver.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Function allowToSave() As Boolean
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso clsCommon.myLen(txtTankerCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please enter Tanker Code", Me.Text)
                txtTankerCode.Focus()
                Return False
            ElseIf clsCommon.myLen(txtTankerNo.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please enter Tanker No", Me.Text)
                txtTankerNo.Focus()
                Return False
            ElseIf clsCommon.myCdbl(txtChamborNo.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please enter Chamber No", Me.Text)
                txtChamborNo.Focus()
                Return False
            ElseIf clsCommon.myCdbl(txtTareWeight.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please enter Tare Weight", Me.Text)
                txtTareWeight.Focus()
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

                Dim obj As New clsTankerMasterSaleHead()
                obj.Tanker_Code = clsCommon.myCstr(txtTankerCode.Value)
                obj.Tanker_No = clsCommon.myCstr(txtTankerNo.Text)
                obj.No_Of_Chamber = clsCommon.myCdbl(txtChamborNo.Value)
                obj.Tare_Weight = clsCommon.myCdbl(txtTareWeight.Value)
                obj.Arr = New List(Of clsTankerMasterSaleDetail)

                For Each grow As GridViewRowInfo In gv.Rows
                    Dim objTr As New clsTankerMasterSaleDetail()
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSlNo).Value)
                    objTr.Chamber_Desc = clsCommon.myCstr(grow.Cells(colValue).Value)
                    objTr.Chamber_Capacity = clsCommon.myCdbl(grow.Cells(colCapacity).Value)
                    objTr.Capacity_Uom = clsCommon.myCstr(grow.Cells(colUOM).Value)
                    If (clsCommon.myLen(objTr.Chamber_Desc) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next

                obj.ArrDriver = New List(Of clsTankerMasterSaleDriverDetail)
                For ii As Integer = 0 To GvDriver.Rows.Count - 1
                    If clsCommon.myCBool(GvDriver.Rows(ii).Cells("Sel").Value) OrElse chkVendorAll.IsChecked Then
                        Dim objDriver As New clsTankerMasterSaleDriverDetail
                        objDriver.Driver_Code = clsCommon.myCstr(GvDriver.Rows(ii).Cells("Driver Code").Value)
                        If clsCommon.myLen(objDriver.Driver_Code) > 0 Then
                            obj.ArrDriver.Add(objDriver)
                        End If
                    End If
                Next

                obj.isNewEntry = isNewEntry
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill atleast one Chamber Desc", Me.Text)
                    Return False
                End If

                If (obj.ArrDriver Is Nothing OrElse obj.ArrDriver.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one driver", Me.Text)
                    Return False
                End If

                Dim isSaved As Boolean = obj.SaveData(obj)
                If isSaved Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Tanker_Code, NavigatorType.Current)
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
                If (clsTankerMasterSaleHead.DeleteData(txtTankerCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
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

    Private Sub gv_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv.Columns(colUOM) Then
                        OpenUOMList(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as [Description] from TSPL_UNIT_MASTER"
        Dim whrCls As String = "Weight_Type='Y'"
        gv.CurrentRow.Cells(colUOM).Value = clsCommon.ShowSelectForm("SRNUnitfndnder", qry, "Code", whrCls, clsCommon.myCstr(gv.CurrentRow.Cells(colUOM).Value), "Code", isButtonClick)
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

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            Dim obj As New clsTankerMasterSaleHead()
            obj = clsTankerMasterSaleHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Tanker_Code) > 0) Then
                reset()
                isNewEntry = False
                btnSave.Text = "Update"
                txtTankerCode.MyReadOnly = True
                txtTankerCode.Value = obj.Tanker_Code
                txtTankerNo.Text = obj.Tanker_No
                txtChamborNo.Value = obj.No_Of_Chamber
                txtTareWeight.Value = obj.Tare_Weight
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsTankerMasterSaleDetail In obj.Arr
                        gv.Rows.AddNew()
                        gv.Rows(gv.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colValue).Value = objTr.Chamber_Desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colCapacity).Value = objTr.Chamber_Capacity
                        gv.Rows(gv.Rows.Count - 1).Cells(colUOM).Value = objTr.Capacity_Uom
                    Next
                End If
                LoadDriver(obj.Tanker_Code)
                btnDelete.Enabled = True
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtTankerCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtTankerCode._MYNavigator
        Try
            Dim strwherecls As String = ""
            Dim qst As String = ""
            strwherecls = ""

            qst = "select count(*) from TSPL_TANKER_MASTER_SALE where TANKER_CODE='" + txtTankerCode.Value + "'"

            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtTankerCode.MyReadOnly = False
            Else
                txtTankerCode.MyReadOnly = True
            End If
            LoadData(txtTankerCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtTankerCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTankerCode._MYValidating
        Dim qry = "select count(*) from TSPL_TANKER_MASTER_SALE where TANKER_CODE='" + txtTankerCode.Value + "'"
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If no = 0 Then
            txtTankerCode.MyReadOnly = False
        Else
            txtTankerCode.MyReadOnly = True
        End If
        Dim whrClas As String = ""
        If txtTankerCode.MyReadOnly OrElse isButtonClicked Then
            txtTankerCode.Value = clsTankerMasterSaleHead.getFinder("", txtTankerCode.Value, isButtonClicked)
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
            Dim qry As String = Nothing
            Dim whrcls As String = Nothing
            qry = "select TSPL_TANKER_MASTER_SALE.Tanker_Code as [Tanker Code],TSPL_TANKER_MASTER_SALE.Tanker_No as [Tanker No],TSPL_TANKER_MASTER_SALE.Tare_Weight as [Tare Weight],TSPL_TANKER_MASTER_SALE.No_Of_Chamber as [No Of Chamber],TSPL_TANKER_MASTER_SALE_DRIVER_DETAIL.Driver_Code as [Driver Code],TSPL_TANKER_MASTER_SALE_DETAIL.Chamber_Desc as [Chamber Description],TSPL_TANKER_MASTER_SALE_DETAIL.Chamber_Capacity as [Chamber Capacity],TSPL_TANKER_MASTER_SALE_DETAIL.Capacity_Uom as [Capacity UOM] from TSPL_TANKER_MASTER_SALE " & _
                    " left outer join TSPL_TANKER_MASTER_SALE_DETAIL on TSPL_TANKER_MASTER_SALE_DETAIL.Tanker_Code=TSPL_TANKER_MASTER_SALE.Tanker_Code" & _
                    " left outer join TSPL_TANKER_MASTER_SALE_DRIVER_DETAIL on TSPL_TANKER_MASTER_SALE_DRIVER_DETAIL.Tanker_Code=TSPL_TANKER_MASTER_SALE.Tanker_Code"
            transportSql.ExporttoExcel(qry, whrcls, Me)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub mnuImport_Click(sender As Object, e As EventArgs) Handles mnuImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Tanker Code", "Tanker No", "Tare Weight", "No Of Chamber", "Driver Code", "Chamber Description", "Chamber Capacity", "Capacity UOM") Then
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New clsTankerMasterSaleHead()
                    Dim objtr As New clsTankerMasterSaleDetail()
                    Dim objtrDriver As New clsTankerMasterSaleDriverDetail()

                    Dim strTankerCode As String = clsCommon.myCstr(grow.Cells("Tanker Code").Value)
                    If (String.IsNullOrEmpty(strTankerCode)) Then
                        Throw New Exception("Tanker Code can not be blank or incorrect.")
                    End If
                    obj.Tanker_Code = strTankerCode

                    Dim strTankerNo As String = clsCommon.myCstr(grow.Cells("Tanker No").Value)
                    If (String.IsNullOrEmpty(strTankerNo)) Then
                        Throw New Exception("Tanker No can not be blank or incorrect.")
                    End If
                    obj.Tanker_No = strTankerNo

                    Dim TareWeight As Double = clsCommon.myCdbl(grow.Cells("Tare Weight").Value)
                    If TareWeight <= 0 Then
                        Throw New Exception("Tare Weight must be greater than zero.")
                    End If
                    obj.Tare_Weight = TareWeight

                    Dim NoOfChamber As Double = clsCommon.myCdbl(grow.Cells("No Of Chamber").Value)
                    If NoOfChamber <= 0 Then
                        Throw New Exception("No of chamber must be greater than zero.")
                    End If
                    obj.No_Of_Chamber = NoOfChamber

                    Dim strDriverCode As String = clsCommon.myCstr(grow.Cells("Driver Code").Value)
                    If (String.IsNullOrEmpty(strDriverCode)) Then
                        Throw New Exception("Driver Code can not be blank or incorrect.")
                    Else
                        Dim query As String = Nothing
                        Dim chk As Integer = Nothing
                        query = "select count(*) from TSPL_EMPLOYEE_MASTER where Emp_type='DRIVER' and EMP_CODE='" + strDriverCode + "' "
                        chk = CInt(clsDBFuncationality.getSingleValue(query))
                        If chk < 1 Then
                            Throw New Exception("Driver Code not valid.")
                        End If
                    End If
                    objtrDriver.Driver_Code = strDriverCode

                    Dim strChamberDescription As String = clsCommon.myCstr(grow.Cells("Chamber Description").Value)
                    If (String.IsNullOrEmpty(strChamberDescription)) Then
                        Throw New Exception("Chamber Description can not be blank.")
                    End If
                    objtr.Chamber_Desc = strChamberDescription

                    Dim ChamberCapacity As Double = clsCommon.myCdbl(grow.Cells("Chamber Capacity").Value)
                    If ChamberCapacity <= 0 Then
                        Throw New Exception("Chamber Capacity must be greater than zero.")
                    End If
                    objtr.Chamber_Capacity = ChamberCapacity

                    Dim strCapacityUOM As String = clsCommon.myCstr(grow.Cells("Capacity UOM").Value)
                    If (String.IsNullOrEmpty(strCapacityUOM)) Then
                        Throw New Exception("Capacity UOM can not be blank.")
                    Else
                        Dim query As String = Nothing
                        Dim chk As Integer = Nothing
                        query = "select count(*) from TSPL_UNIT_MASTER where Weight_Type='Y' AND Unit_Code='" + strCapacityUOM + "'"
                        chk = CInt(clsDBFuncationality.getSingleValue(query))
                        If chk < 1 Then
                            Throw New Exception("Capacity UOM not valid.")
                        End If
                    End If
                    objtr.Capacity_Uom = strCapacityUOM


                    '------------------------------------------------------------------------------------------------------------//
                    Dim qury As String = Nothing
                    Dim chek As Integer = Nothing
                    qury = "select count(*) from TSPL_TANKER_MASTER_SALE where Tanker_Code='" + strTankerCode + "'"
                    chek = CInt(clsDBFuncationality.getSingleValue(qury))
                    If chek < 1 Then
                        obj.isNewEntry = True
                    Else
                        obj.isNewEntry = False
                    End If
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Tanker_Code", obj.Tanker_Code)
                    clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)
                    clsCommon.AddColumnsForChange(coll, "No_Of_Chamber", obj.No_Of_Chamber)
                    clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)
                    If obj.isNewEntry Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TANKER_MASTER_SALE", OMInsertOrUpdate.Insert, "")
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TANKER_MASTER_SALE", OMInsertOrUpdate.Update, "TSPL_TANKER_MASTER_SALE.Tanker_Code='" + obj.Tanker_Code + "'")
                    End If
                    '-----------------------------------------------------------------------------------------------------------------//

                    qury = "select line_no from TSPL_TANKER_MASTER_SALE_DETAIL where Tanker_Code='" + obj.Tanker_Code + "' and Chamber_Desc='" + objtr.Chamber_Desc + "'"
                    Dim lineno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qury))
                    objtr.Line_No = clsCommon.myCdbl(lineno)
                    If clsCommon.myLen(lineno) <= 0 OrElse clsCommon.myCdbl(lineno) <= 0 Then
                        qury = "select max(line_no) from TSPL_TANKER_MASTER_SALE_DETAIL where Tanker_Code='" + obj.Tanker_Code + "' group by tanker_code"
                        lineno = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qury))
                        objtr.Line_No = clsCommon.myCdbl(lineno) + 1
                    End If

                    qury = "delete from TSPL_TANKER_MASTER_SALE_DETAIL where Tanker_Code='" + obj.Tanker_Code + "' and Chamber_Desc='" + objtr.Chamber_Desc + "'"
                    clsDBFuncationality.ExecuteNonQuery(qury)

                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Tanker_Code", strTankerCode)
                    clsCommon.AddColumnsForChange(coll, "Line_No", objtr.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Chamber_Desc", objtr.Chamber_Desc)
                    clsCommon.AddColumnsForChange(coll, "Chamber_Capacity", objtr.Chamber_Capacity)
                    clsCommon.AddColumnsForChange(coll, "Capacity_Uom", objtr.Capacity_Uom, True)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TANKER_MASTER_SALE_DETAIL", OMInsertOrUpdate.Insert, "")
                    '------------------------------------------------------------------------------------------------------------------//
                    qury = "delete from TSPL_TANKER_MASTER_SALE_DRIVER_DETAIL where Tanker_Code='" + obj.Tanker_Code + "' and Driver_Code='" + objtrDriver.Driver_Code + "'"
                    clsDBFuncationality.ExecuteNonQuery(qury)

                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "TANKER_CODE", strTankerCode)
                    clsCommon.AddColumnsForChange(coll, "Driver_Code", objtrDriver.Driver_Code, True)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TANKER_MASTER_SALE_DRIVER_DETAIL", OMInsertOrUpdate.Insert, "")

                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
        reset()
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        If chkVendorAll.IsChecked = True Then
            For ii As Integer = 0 To GvDriver.RowCount - 1
                GvDriver.Rows(ii).Cells("SEL").Value = True
            Next
            GvDriver.Enabled = False
        Else
            For ii As Integer = 0 To GvDriver.RowCount - 1
                GvDriver.Rows(ii).Cells("SEL").Value = False
            Next
            GvDriver.Enabled = True
        End If
    End Sub
End Class

Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class FrmOverheadCostGroup
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Const colLineNo As String = "colLineNo"
    Const colCost_Code As String = "colCost_Code"
    Const colCost_Desc As String = "colCost_Desc"
    Const colRatePerHour As String = "colRatePerHour"
    Const colHours As String = "colHours"
    Const colNO As String = "colNO"
    Const colCost As String = "colCost"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim isImport As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim Qry As String
    Dim isIncludeRatePerHoursIn As Boolean = False
#End Region
    Private Sub FrmOverheadCostGroup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        isIncludeRatePerHoursIn = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.IncludeRatePerHoursIn & "'")) = 0, False, True)
        funReset()
        gv1.Rows.AddNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")

    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtDesc.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        txtCode.MyReadOnly = False
        LoadBlankGrid()
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
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoGroupCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroupCode.FormatString = ""
        repoGroupCode.HeaderText = "Cost Code"
        repoGroupCode.Name = colCost_Code
        repoGroupCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoGroupCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoGroupCode.Width = 120
        gv1.MasterTemplate.Columns.Add(repoGroupCode)

        Dim repoGroupDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroupDesc.FormatString = ""
        repoGroupDesc.HeaderText = "Cost Description"
        repoGroupDesc.Name = colCost_Desc
        repoGroupDesc.Width = 180
        repoGroupDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoGroupDesc)


        Dim repoGroupRatePerHour As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoGroupRatePerHour.FormatString = "{0:N2}"
        repoGroupRatePerHour.HeaderText = "Rate/Hour"
        repoGroupRatePerHour.Name = colRatePerHour
        repoGroupRatePerHour.Width = 80
        repoGroupRatePerHour.ReadOnly = False
        If isIncludeRatePerHoursIn = True Then
            repoGroupRatePerHour.IsVisible = True
        Else
            repoGroupRatePerHour.IsVisible = False
        End If
        repoGroupRatePerHour.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoGroupRatePerHour)

        Dim repoGroupHour As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoGroupHour.FormatString = "{0:N2}"
        repoGroupHour.HeaderText = "Hour"
        repoGroupHour.Name = colHours
        repoGroupHour.Width = 80
        repoGroupHour.ReadOnly = False
        If isIncludeRatePerHoursIn = True Then
            repoGroupHour.IsVisible = True
        Else
            repoGroupHour.IsVisible = False
        End If
        repoGroupHour.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoGroupHour)

        Dim repoGroupNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoGroupNo.FormatString = "{0:N2}"
        repoGroupNo.HeaderText = "NO"
        repoGroupNo.Name = colNO
        repoGroupNo.Width = 80
        repoGroupNo.ReadOnly = False
        If isIncludeRatePerHoursIn = True Then
            repoGroupNo.IsVisible = True
        Else
            repoGroupNo.IsVisible = False
        End If
        repoGroupNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoGroupNo)

        Dim repoGroupCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoGroupCost.FormatString = "{0:N2}"
        repoGroupCost.HeaderText = "Cost"
        repoGroupCost.Name = colCost
        repoGroupCost.Width = 150
        repoGroupCost.ReadOnly = True
        If isIncludeRatePerHoursIn = True Then
            repoGroupCost.IsVisible = True
        Else
            repoGroupCost.IsVisible = False
        End If
        repoGroupCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoGroupCost)



        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AutoSizeRows = False
        gv1.AllowRowReorder = True
        'ReStoreGridLayout()
        'gv1.Rows.AddNew()
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

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        funReset()
        gv1.Rows.AddNew()
    End Sub
    Sub funClose()
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Public Sub Save()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowToSave() Then

                Dim obj As New clsOverheadCostGroupHead
                obj.GROUP_CODE = txtCode.Value
                obj.Description = txtDesc.Text
                obj.Arr = New List(Of clsOverheadCostGroupDetails)
                For ii As Integer = 0 To gv1.RowCount - 1
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colCost_Code).Value) > 0 Then
                        Dim objtr As New clsOverheadCostGroupDetails
                        objtr.GROUP_CODE = obj.GROUP_CODE
                        objtr.COST_CODE = clsCommon.myCstr(gv1.Rows(ii).Cells(colCost_Code).Value)
                        objtr.COST_DESC = clsCommon.myCstr(gv1.Rows(ii).Cells(colCost_Desc).Value)
                        ' Ticket No : BHA/03/08/18-000387 By prabhakar for include Rate Per Hours
                        If isIncludeRatePerHoursIn = True Then
                            objtr.RatePerHour = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRatePerHour).Value)
                            objtr.Hours = clsCommon.myCdbl(gv1.Rows(ii).Cells(colHours).Value)
                            objtr.NO = clsCommon.myCdbl(gv1.Rows(ii).Cells(colNO).Value)
                            objtr.Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCost).Value)
                        End If
                        obj.Arr.Add(objtr)
                    End If

                Next
                If (clsOverheadCostGroupHead.SaveData(obj, isNewEntry, trans)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully")

                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                Else
                    btnSave.Text = "Save"
                    btnDelete.Enabled = False
                End If
                trans.Commit()
                LoadData(obj.GROUP_CODE, NavigatorType.Current, Nothing)
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue("Code")
            txtCode.Focus()
            Return False

        ElseIf clsCommon.myLen(txtDesc.Text) <= 0 Then
            myMessages.blankValue("Description")
            txtDesc.Focus()
            Return False
        ElseIf gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strCostCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colCost_Code).Value)

                For jj As Integer = 0 To gv1.Rows.Count - 1
                    If jj = ii Then
                        Continue For
                    End If
                    Dim strInnerCostCode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colCost_Code).Value)
                    If clsCommon.CompairString(strCostCode, strInnerCostCode) = CompairStringResult.Equal Then
                        Dim Msg As String = "Same Cost Code  Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                        common.clsCommon.MyMessageBoxShow(Me, Msg)
                        Return False
                    End If
                Next
                If isIncludeRatePerHoursIn = True AndAlso clsCommon.myLen(strCostCode) > 0 Then
                    Dim strRatePerHours As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRatePerHour).Value)
                    If String.IsNullOrEmpty(strRatePerHours) = True Then
                        common.clsCommon.MyMessageBoxShow(Me, "Rate/Hour can not be blank for cost code " + strCostCode + " ")
                        Return False
                    End If
                    Dim strHours As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colHours).Value)
                    If String.IsNullOrEmpty(strHours) = True Then
                        common.clsCommon.MyMessageBoxShow(Me, "Hour can not be blank for cost code " + strCostCode + " ")
                        Return False
                    End If
                    Dim strNO As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colNO).Value)
                    If String.IsNullOrEmpty(strHours) = True Then
                        common.clsCommon.MyMessageBoxShow(Me, "No can not be blank for cost code " + strCostCode + " ")
                        Return False
                    End If
                End If
            Next
        End If
        Return True
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType, ByVal tran As SqlTransaction)
        Try
            isInsideLoadData = True
            txtCode.MyReadOnly = True
            btnSave.Enabled = True
            btnDelete.Enabled = True
            isNewEntry = False
            Dim obj As New clsOverheadCostGroupHead()
            obj = clsOverheadCostGroupHead.GetData(strCode, NavTyep, tran)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.GROUP_CODE) > 0) Then
                funReset()
                isNewEntry = False

                btnSave.Text = "Update"
                btnDelete.Enabled = True
                txtCode.Value = obj.GROUP_CODE
                txtDesc.Text = obj.Description
                LoadDetailData(obj.Arr, False)
                txtCode.MyReadOnly = True
                gv1.Rows.AddNew()
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        Finally
            isInsideLoadData = False
        End Try
        
    End Sub

    Sub LoadDetailData(ByVal Arr As List(Of clsOverheadCostGroupDetails), ByVal isAddMasterCode As Boolean)
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            For Each objtr As clsOverheadCostGroupDetails In Arr
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 1)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objtr.SNO
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCost_Code).Value = objtr.COST_CODE
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCost_Desc).Value = objtr.COST_DESC
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRatePerHour).Value = objtr.RatePerHour
                gv1.Rows(gv1.Rows.Count - 1).Cells(colHours).Value = objtr.Hours
                gv1.Rows(gv1.Rows.Count - 1).Cells(colNO).Value = objtr.NO
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCost).Value = objtr.Cost

            Next
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsOverheadCostGroupHead.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_OVERHEAD_COST_GROUP_HEAD where GROUP_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsOverheadCostGroupHead.getFinder("", txtCode.Value, isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current, Nothing)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType, Nothing)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If isImport = True Then
                Return
            End If
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colCost_Code) Then
                        OpenGroupCodeList(False)
                    End If
                    If e.Column Is gv1.Columns(colRatePerHour) Then
                        gv1.CurrentRow.Cells(colCost).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRatePerHour).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colHours).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colNO).Value)
                    End If
                    If e.Column Is gv1.Columns(colHours) Then
                        gv1.CurrentRow.Cells(colCost).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRatePerHour).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colHours).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colNO).Value)
                    End If
                    If e.Column Is gv1.Columns(colNO) Then
                        gv1.CurrentRow.Cells(colCost).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRatePerHour).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colHours).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colNO).Value)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub
    Sub OpenGroupCodeList(ByVal isButtonClick As Boolean)
        Try
            ClearAllCurrentRowFinder()
            Dim aaa As String = ""
            Dim qry As String = "Select Cost_Code as Code ,Description from TSPL_OVERHEAD_COST"
            gv1.CurrentRow.Cells(colCost_Code).Value = clsCommon.ShowSelectForm("Fnd", qry, "Code", "", gv1.CurrentRow.Cells(colCost_Code).Value, "", True)
            ' gv1.CurrentRow.Cells(colCost_Code).Value = aaa
            'gv1.CurrentRow.Cells(colCost_Code).Value = clsCommon.ShowSelectForm("Fnder@GroupCode", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colCost_Code).Value), "", isButtonClick)
            'gv1.CurrentRow.Cells(colCost_Code).Value = clsCommon.ShowSelectForm("CostCode@finder", qry, "Code", " 2=2 ", clsCommon.myCstr(gv1.CurrentRow.Cells(colCost_Code).Value), "Code", isButtonClick)
            If clsCommon.myLen(gv1.CurrentRow.Cells(colCost_Code).Value) > 0 Then
                ADDNewRows()
                gv1.CurrentRow.Cells(colCost_Desc).Value = clsDBFuncationality.getSingleValue(" Select Description from TSPL_OVERHEAD_COST where Cost_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCost_Code).Value) + "' ")
                gv1.CurrentRow.Cells(colRatePerHour).Value = clsDBFuncationality.getSingleValue(" Select RatePerHour from TSPL_OVERHEAD_COST where Cost_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCost_Code).Value) + "' ")
            End If

        Catch ex As Exception
            gv1.CurrentRow.Cells(colCost_Code).Value = ""
            gv1.CurrentRow.Cells(colCost_Desc).Value = ""
            gv1.CurrentRow.Cells(colRatePerHour).Value = 0
            gv1.CurrentRow.Cells(colHours).Value = 0
            gv1.CurrentRow.Cells(colNO).Value = 0
            gv1.CurrentRow.Cells(colCost).Value = 0
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub ClearAllCurrentRowFinder()
        gv1.CurrentRow.Cells(colCost_Code).Value = ""
        gv1.CurrentRow.Cells(colCost_Desc).Value = ""
        gv1.CurrentRow.Cells(colRatePerHour).Value = 0
        gv1.CurrentRow.Cells(colHours).Value = 0
        gv1.CurrentRow.Cells(colNO).Value = 0
        gv1.CurrentRow.Cells(colCost).Value = 0
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

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try
            Dim query As String = " select * from (select TSPL_OVERHEAD_COST_GROUP_HEAD.GROUP_CODE,TSPL_OVERHEAD_COST_GROUP_HEAD.Description as Group_Desc ,TSPL_OVERHEAD_COST_GROUP_DETAILS.COST_CODE,TSPL_OVERHEAD_COST.Description as COST_DESC from TSPL_OVERHEAD_COST_GROUP_DETAILS left outer join TSPL_OVERHEAD_COST_GROUP_HEAD on TSPL_OVERHEAD_COST_GROUP_DETAILS.GROUP_CODE = TSPL_OVERHEAD_COST_GROUP_HEAD.GROUP_CODE left outer join TSPL_OVERHEAD_COST on TSPL_OVERHEAD_COST.COST_CODE = TSPL_OVERHEAD_COST_GROUP_DETAILS.COST_CODE) xx "
            transportSql.ExporttoExcel(query, "", " xx.GROUP_CODE", Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, "Overhead Cost Group")
        End Try

    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Try
            If transportSql.importExcel(dgv, "GROUP_CODE", "Group_Desc", "COST_CODE", "COST_DESC") Then






                Dim lineNo As Integer = 0
                'Dim GROUP_CODE As String = ""
                'Dim Group_Desc As String = ""
                'Dim COST_CODE As String = ""
                'Dim COST_DESC As String = ""
                For Each grow As GridViewRowInfo In dgv.Rows
                    lineNo = lineNo + 1
                    If clsCommon.myLen(grow.Cells("GROUP_CODE").Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "CROUP CODE cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "'")
                        Exit Sub
                    End If
                    If clsCommon.myLen(grow.Cells("Group_Desc").Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "CROUP DESC cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "'")
                        Exit Sub
                    End If
                    If clsCommon.myLen(grow.Cells("COST_CODE").Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "COST CODE cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "'")
                        Exit Sub
                    End If
                    If clsCommon.myLen(grow.Cells("COST_CODE").Value) > 0 Then
                        Dim chkCostCode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count (*) from TSPL_OVERHEAD_COST where COST_CODE = '" + clsCommon.myCstr(grow.Cells("COST_CODE").Value) + "'", trans))
                        If chkCostCode <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, " Invalid [Cost Code]  at line :'" + clsCommon.myCstr(lineNo) + "'")
                            Exit Sub
                        End If
                    End If

                    Dim chkGroupCodeExist As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count (*) from TSPL_OVERHEAD_COST_GROUP_HEAD where GROUP_CODE =  '" + clsCommon.myCstr(grow.Cells("GROUP_CODE").Value) + "'", trans))

                    If chkGroupCodeExist > 0 Then
                        clsDBFuncationality.ExecuteNonQuery("update TSPL_OVERHEAD_COST_GROUP_HEAD set   Description = '" + clsCommon.myCstr(grow.Cells("Group_Desc").Value) + "' , Modify_By = '" + objCommonVar.CurrentUserCode + "' , Modify_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' where GROUP_CODE = '" + clsCommon.myCstr(grow.Cells("GROUP_CODE").Value) + "'", trans)
                        Dim chkCostCodeExist As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count (*) from TSPL_OVERHEAD_COST_GROUP_DETAILS where GROUP_CODE =  '" + clsCommon.myCstr(grow.Cells("GROUP_CODE").Value) + "' and COST_CODE ='" + clsCommon.myCstr(grow.Cells("COST_CODE").Value) + "'", trans))
                        If chkCostCodeExist <= 0 Then
                            Dim sno As Integer = clsDBFuncationality.getSingleValue("select count(*) +1 from TSPL_OVERHEAD_COST_GROUP_DETAILS where GROUP_CODE ='" + clsCommon.myCstr(grow.Cells("GROUP_CODE").Value) + "'", trans)
                            'Dim aa As String = clsCommon.myCstr(grow.Cells("GROUP_CODE").Value)
                            'Dim bb As String = clsCommon.myCstr(grow.Cells("COST_CODE").Value)
                            'Dim cc As String = sno
                            'Dim ddddddd As String = "Insert into TSPL_OVERHEAD_COST_GROUP_DETAILS ( '" + aa + "'," + cc + ""
                            'Dim ddd As String = "Insert into TSPL_OVERHEAD_COST_GROUP_DETAILS ( GROUP_CODE,SNO,COST_CODE) values ('" + aa + "'," + cc + ",'" + bb + "')"
                            clsDBFuncationality.ExecuteNonQuery("Insert into TSPL_OVERHEAD_COST_GROUP_DETAILS ( GROUP_CODE,SNO,COST_CODE) values ('" + clsCommon.myCstr(grow.Cells("GROUP_CODE").Value) + "'," + clsCommon.myCstr(sno) + ",'" + clsCommon.myCstr(grow.Cells("COST_CODE").Value) + "')", trans)
                        End If

                    Else
                        clsDBFuncationality.ExecuteNonQuery("insert into TSPL_OVERHEAD_COST_GROUP_HEAD (GROUP_CODE,GROUP_DATE,Description,Created_By,Created_Date,Modify_By,Modify_Date,Comp_code ) values ('" + clsCommon.myCstr(grow.Cells("GROUP_CODE").Value) + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + clsCommon.myCstr(grow.Cells("Group_Desc").Value) + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentCompanyCode + "')", trans)
                        Dim sno As Integer = clsDBFuncationality.getSingleValue("select count(*) +1 from TSPL_OVERHEAD_COST_GROUP_DETAILS where GROUP_CODE ='" + clsCommon.myCstr(grow.Cells("GROUP_CODE").Value) + "'", trans)
                        clsDBFuncationality.ExecuteNonQuery("Insert into TSPL_OVERHEAD_COST_GROUP_DETAILS ( GROUP_CODE,SNO,COST_CODE) values ('" + clsCommon.myCstr(grow.Cells("GROUP_CODE").Value) + "'," + clsCommon.myCstr(sno) + ",'" + clsCommon.myCstr(grow.Cells("COST_CODE").Value) + "')", trans)
                    End If
                Next
            End If
            trans.Commit()
            common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

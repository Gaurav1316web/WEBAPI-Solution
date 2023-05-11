'==============BM00000003063,Updated By Rohit===========
Imports common
Imports System.IO
Imports System.Windows.Forms
Imports Telerik.WinControls.UI

Public Class FrmSerializeItemIn
#Region "Variables"
    Public strItemCode As String = ""
    Public strItemName As String = ""
    Public dblqty As Double = 0
    Const ColSNo As String = "COLSNO"
    Const ColAutoSNo As String = "COLAUTOSNO"
    Const ColTAGNo As String = "COLTAGNO"
    Const COLAllow_QC As String = "COLAllow_QC"
    Const COLBINNO As String = "COLBINNO"
    Public arr As List(Of clsSerializeInvenotry) = Nothing
    Const ReportID As String = "SerialInvIn"
    Public isCencelButtonClicked As Boolean = False
    Public strItemType As String = ""
    Public AcceptedQty As Integer
    Public RejectedQty As Integer
    Public strBinNo As String = ""
    Dim isInsideLoadData As Boolean = False
#End Region

    Private Sub FrmSerializeItemIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isInsideLoadData = True
        lblItemCode.Text = strItemCode
        lblItemName.Text = strItemName
        lblQty.Text = clsCommon.myFormat(dblqty)
        LoadBlankGrid()

        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            Dim counter As Integer = 0
            For Each obj As clsSerializeInvenotry In arr
                If counter > gv1.RowCount - 1 Then
                    gv1.Rows.AddNew()
                End If
                gv1.Rows(counter).Cells(ColAutoSNo).Value = obj.Auto_Sr_No
                ''richa agarwal
                'gv1.Rows(counter).Cells(COLAllow_QC).Value = obj.Allow_QC
                gv1.Rows(counter).Cells(COLAllow_QC).Value = 1
                gv1.Rows(counter).Cells(COLBINNO).Value = obj.Auto_BIN_No
                ''-------------
                If gv1.Columns.Contains(ColTAGNo) Then
                    gv1.Rows(counter).Cells(ColTAGNo).Value = obj.Tag_No
                End If
                counter += 1
            Next
        End If
        RefeshSNO()
        If gv1.RowCount > 0 Then
            gv1.CurrentRow = gv1.Rows(0)
            gv1.CurrentColumn = gv1.Columns(ColAutoSNo)
        End If
        txtBarCode.Focus()
        isInsideLoadData = False
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = ColSNo
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoLocationName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocationName.FormatString = ""
        repoLocationName.HeaderText = "Serial No"
        repoLocationName.Name = ColAutoSNo
        repoLocationName.ReadOnly = False
        repoLocationName.IsVisible = True
        repoLocationName.Width = 250
        gv1.MasterTemplate.Columns.Add(repoLocationName)

        If strItemType = "A" Then
            Dim repoTagNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTagNo.FormatString = ""
            repoTagNo.HeaderText = "Tag No"
            repoTagNo.Name = ColTAGNo
            repoTagNo.ReadOnly = False
            repoTagNo.IsVisible = True
            repoTagNo.Width = 200
            repoLocationName.Width = 200
            gv1.MasterTemplate.Columns.Add(repoTagNo)
        End If

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "QC Complete"
        repoRowType.Name = COLAllow_QC
        repoRowType.Width = 150
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetAllowQC()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Name"
        gv1.MasterTemplate.Columns.Add(repoRowType)



        Dim repoBINNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBINNo.FormatString = ""
        repoBINNo.HeaderText = "Bin No"
        repoBINNo.Name = COLBINNO
        'repoBINNo.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoBINNo.TextImageRelation = TextImageRelation.TextBeforeImage
        repoBINNo.Width = 100
        repoBINNo.IsVisible = False
        repoBINNo.VisibleInColumnChooser = False
        Dim ShowBinMapping As Boolean = False
        ShowBinMapping = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.ShowBinMapping, clsFixedParameterCode.ShowBinMapping, Nothing) = "1", True, False))
        If ShowBinMapping = True Then
            repoBINNo.IsVisible = True
            repoBINNo.VisibleInColumnChooser = True
        End If
        gv1.MasterTemplate.Columns.Add(repoBINNo)

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

        gv1.AllowDeleteRow = True
        ReStoreGridLayout()
        For ii As Integer = 1 To dblqty
            gv1.Rows.AddNew()
            gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
        Next
    End Sub

    Public Shared Function GetAllowQC() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "Yes"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "0"
        dr("Name") = "No"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        OKPressed()
    End Sub

    Sub OKPressed()
        If AllowToSave() Then
            AcceptedQty = 0
            RejectedQty = 0
            arr = New List(Of clsSerializeInvenotry)
            For ii As Integer = 0 To gv1.RowCount - 1
                Dim obj As clsSerializeInvenotry = New clsSerializeInvenotry()
                obj.Auto_Sr_No = clsCommon.myCstr(gv1.Rows(ii).Cells(ColAutoSNo).Value)
                obj.Allow_QC = clsCommon.myCstr(gv1.Rows(ii).Cells(COLAllow_QC).Value)
                obj.Auto_BIN_No = clsCommon.myCstr(gv1.Rows(ii).Cells(COLBINNO).Value)
                If clsCommon.myCstr(gv1.Rows(ii).Cells(COLAllow_QC).Value) = "1" Then
                    AcceptedQty += 1
                Else
                    RejectedQty += 1
                End If
                If gv1.Columns.Contains(ColTAGNo) Then
                    obj.Tag_No = clsCommon.myCstr(gv1.Rows(ii).Cells(ColTAGNo).Value)
                End If
                arr.Add(obj)
            Next
            Me.Close()
        End If
    End Sub

    Private Function AllowToSave() As Boolean
        If gv1.RowCount <> clsCommon.myCdbl(lblQty.Text) Then
            clsCommon.MyMessageBoxShow("Entered Quantity" + lblQty.Text + " Entered Serail Numbers" + clsCommon.myCstr(gv1.RowCount))
            Return False
        End If
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(ColAutoSNo).Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please enter Serial Number at line number" + clsCommon.myCstr(ii + 1))
                Return False
            End If

            Dim ShowBinMapping As Boolean = False
            ShowBinMapping = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.ShowBinMapping, clsFixedParameterCode.ShowBinMapping, Nothing) = "1", True, False))
            If ShowBinMapping = True Then
                If clsCommon.myLen(gv1.Rows(ii).Cells(COLBINNO).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Please enter Bin Number at line number" + clsCommon.myCstr(ii + 1))
                    Return False
                End If
            End If

        Next
        Return True
    End Function

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub FrmSerializeItemIn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            OKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            CancelPressed()
        ElseIf e.Control And e.Alt And e.KeyCode = Keys.Y Then
            For Each grow As GridViewRowInfo In gv1.Rows
                grow.Cells(COLAllow_QC).Value = "1"
            Next
        ElseIf e.Control And e.Alt And e.KeyCode = Keys.N Then
            For Each grow As GridViewRowInfo In gv1.Rows
                grow.Cells(COLAllow_QC).Value = "0"
            Next
        End If
    End Sub

    Sub CancelPressed()
        isCencelButtonClicked = True
        Me.Close()
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        RefeshSNO()
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If gv1.RowCount <= clsCommon.myCdbl(lblQty.Text) Then
            e.Cancel = True
            Exit Sub
        End If

        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Sub RefeshSNO()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
        Next
    End Sub

    Private Sub txtBarCode_Validating_1(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBarCode.Validating
        If clsCommon.myLen(txtBarCode.Text) > 0 Then
            Dim CurrentRow As Integer = 1
            gv1.CurrentRow.Cells(ColAutoSNo).Value = txtBarCode.Text
            If gv1.CurrentRow.Index < gv1.RowCount - 1 Then
                gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index + 1)
            End If
            lblQty.Focus()
            txtBarCode.Text = ""
            txtBarCode.Focus()
        End If
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        isInsideLoadData = True
        For ii As Integer = 0 To gv1.RowCount - 1
            If ii < dblqty Then
                If clsCommon.myLen(gv1.Rows(ii).Cells(ColAutoSNo).Value) <= 0 Then
                    gv1.Rows(ii).Cells(ColAutoSNo).Value = clsItemMaster.GetItemSerialCounter(strItemCode, Nothing)
                    '===========preeti===================
                    gv1.Rows(ii).Cells(COLBINNO).Value = strBinNo
                    '======================
                    ''richa agarwal
                    gv1.Rows(ii).Cells(COLAllow_QC).Value = 1
                    ''-------------
                End If
            End If
        Next
        isInsideLoadData = False
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        CancelPressed()
    End Sub
    Sub OpenCatValueList(ByVal isButtonClick As Boolean)
        'Dim qry As String = " select CODE,DESCRIPTION,Bin_No from TSPL_ITEM_CATEGORY_LEVEL_VALUES"

        Dim qry As String = " select TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.Bin_No  from TSPL_ITEM_MASTER_CATEGORY "
        qry += " left join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE =TSPL_ITEM_MASTER_CATEGORY.ITEM_CATEGORY_CODE "
        'where Item_code ='FG000018'
        gv1.CurrentRow.Cells(COLBINNO).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("BINNo", qry, "Bin_No", " isnull(form_type,'Item')='Item' and Item_code ='" + strItemCode + "' ", clsCommon.myCstr(gv1.CurrentRow.Cells(COLBINNO).Value), "CODE", isButtonClick))
        'qry = "select DESCRIPTION from TSPL_ITEM_CATEGORY_LEVEL_VALUES where ITEM_CATEGORY_CODE='" + clsCommon.myCstr(gvCategory.CurrentRow.Cells(CatcolCode).Value) + "' and CODE='" + clsCommon.myCstr(gvCategory.CurrentRow.Cells(CatcolValue).Value) + "' and isnull(form_type,'Item')='Item'"
        'gv1.CurrentRow.Cells(CatcolValueDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        'qry = "select Bin_No from TSPL_ITEM_CATEGORY_LEVEL_VALUES where ITEM_CATEGORY_CODE='" + clsCommon.myCstr(gvCategory.CurrentRow.Cells(CatcolCode).Value) + "' and CODE='" + clsCommon.myCstr(gvCategory.CurrentRow.Cells(CatcolValue).Value) + "' and isnull(form_type,'Item')='Item'"
        'gv1.CurrentRow.Cells(CatBinNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Sub
    Dim isCellValueChangedOpenCat As Boolean = False
    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpenCat Then
                    isCellValueChangedOpenCat = True
                    If e.Column Is gv1.Columns(COLBINNO) Then
                        OpenCatValueList(False)
                    ElseIf e.Column Is gv1.Columns(ColAutoSNo) Then
                        If chkAutoIncrement.Checked Then
                            Dim strVal As String = clsCommon.myCstr(gv1.CurrentRow.Cells(ColAutoSNo).Value)
                            For ii As Integer = gv1.CurrentRow.Index + 1 To gv1.RowCount - 1
                                Dim strNewVal As String = clsCommon.incval(strVal)
                                If clsCommon.CompairString(strNewVal, strVal) = CompairStringResult.Equal Then
                                    Exit For
                                Else
                                    gv1.Rows(ii).Cells(ColAutoSNo).Value = strNewVal
                                    strVal = strNewVal
                                End If
                            Next
                        End If
                    End If
                    isCellValueChangedOpenCat = False
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class

'==============Created By Panch Raj===========
Imports common
Imports System.IO
Imports Telerik.WinControls.UI
Imports XpertERPEngine

Public Class frmSerializeItemOutCheckBox
#Region "Variables"
    Public strItemCode As String = ""
    Public strItemName As String = ""
    Public strLocationCode As String = ""
    Public strCurrDocNo As String = ""
    Public strCurrDocType As String = ""
    Public dblqty As Double = 0

    Public strAgaintsDocNo As String = ""
    Public isCencelButtonClicked As Boolean = False
    Const ColSNo As String = "COLSNO"
    Const ColSelect As String = "ColSelect"
    Const ColAutoSNo As String = "COLAUTOSNO"
    Const ColTagNo As String = "COLTAGNO"

    Public arr As List(Of clsSerializeInvenotry) = Nothing
    Const ReportID As String = "SerialInvIn"

    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Public strItemType As String = ""
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
                If gv1.Columns.Contains(ColTagNo) Then
                    gv1.Rows(counter).Cells(ColTagNo).Value = obj.Tag_No
                End If
                'gv1.Rows(counter).Cells(ColManualSNo).Value = obj.Manual_Sr_No
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
        repoLocationName.HeaderText = "Serial Number"
        repoLocationName.Name = ColAutoSNo
        repoLocationName.ReadOnly = True
        repoLocationName.IsVisible = True
        repoLocationName.Width = 400
        gv1.MasterTemplate.Columns.Add(repoLocationName)

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = ColSelect
        repoSelect.Width = 80
        repoSelect.ReadOnly = False
        'repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSelect)

        'If strItemType = "A" Then
        '    Dim repotagNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        '    repotagNo.FormatString = ""
        '    repotagNo.HeaderText = "Tag No"
        '    repotagNo.Name = ColTagNo
        '    repotagNo.ReadOnly = False
        '    repotagNo.IsVisible = True
        '    repotagNo.Width = 200
        '    repoLocationName.Width = 200
        '    gv1.MasterTemplate.Columns.Add(repotagNo)
        'End If


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
        'For ii As Integer = 1 To dblqty
        '    gv1.Rows.AddNew()
        '    gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
        'Next
        FillAuto()
    End Sub

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
            arr = New List(Of clsSerializeInvenotry)
            For ii As Integer = 0 To gv1.RowCount - 1
                If gv1.Rows(ii).Cells(ColSelect).Value = True Then
                    Dim obj As clsSerializeInvenotry = New clsSerializeInvenotry()
                    obj.Auto_Sr_No = clsCommon.myCstr(gv1.Rows(ii).Cells(ColAutoSNo).Value)
                    If gv1.Columns.Contains(ColTagNo) Then
                        obj.Tag_No = clsCommon.myCstr(gv1.Rows(ii).Cells(ColTagNo).Value)
                    End If
                    arr.Add(obj)
                End If
            Next
            'If clsCommon.myCdbl(lblQty.Text) < arr.Count Then
            '    clsCommon.MyMessageBoxShow("Please select maximum " & lblQty.Text & " Serial.")
            '    Exit Sub
            'End If
            Me.Close()
        End If
    End Sub

    Private Function AllowToSave() As Boolean
        Dim TotalSelected As Integer = 0
        For Each dr As GridViewRowInfo In gv1.Rows
            If dr.Cells(ColSelect).Value = True Then
                TotalSelected = TotalSelected + 1
            End If
        Next
        If TotalSelected <> clsCommon.myCdbl(lblQty.Text) Then
            clsCommon.MyMessageBoxShow("Entered Quantity" + lblQty.Text + " Entered Serail Numbers" + clsCommon.myCstr(TotalSelected))
            Return False
        End If
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(ColAutoSNo).Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please enter Serial Number at line number" + clsCommon.myCstr(ii + 1))
                Return False
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
        End If
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
        isInsideLoadData = True
        If clsCommon.myLen(txtBarCode.Text) > 0 Then
            OpenSerialList(gv1.CurrentRow.Index, txtBarCode.Text)
            If gv1.CurrentRow.Index < gv1.RowCount - 1 Then
                gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index + 1)
            End If
            lblQty.Focus()
            txtBarCode.Text = ""
            txtBarCode.Focus()
        End If
        isInsideLoadData = False
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(ColAutoSNo) Then
                        OpenSerialList(gv1.CurrentRow.Index, clsCommon.myCstr(gv1.CurrentRow.Cells(ColAutoSNo).Value))
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenSerialList(ByVal intRowNo As Integer, ByVal currCode As String)
        Dim qry As String = getQry()
        gv1.Rows(intRowNo).Cells(ColAutoSNo).Value = clsCommon.ShowSelectForm("SerialManNo1", qry, "AutoSerialNo", "", currCode)

    End Sub

    Private Sub btnFillAuto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFillAuto.Click
        FillAuto()
    End Sub
    Sub FillAuto()
        isInsideLoadData = True
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(getQry() + " order by Document_Date ")
        Dim qry As String = getQry()
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Items serial number not found to fill", Me.Text)
            isInsideLoadData = False
            Exit Sub
        End If
        Dim ii As Integer = 0
        gv1.Rows.Clear()
        For Each dr As DataRow In dt.Rows
            'If ii > gv1.RowCount - 1 Then
            '    Exit For
            'End If
            gv1.Rows.AddNew()
            gv1.Rows(ii).Cells(ColSNo).Value = gv1.Rows.Count
            gv1.Rows(ii).Cells(ColAutoSNo).Value = clsCommon.myCstr(dr("AutoSerialNo"))
            If gv1.Columns.Contains(ColTagNo) Then
                gv1.Rows(ii).Cells(ColTagNo).Value = clsCommon.myCstr(dr("Tag_no"))
            End If
            ii += 1
        Next
        If Not arr Is Nothing AndAlso arr.Count > 0 Then
            For Each Srdr As clsSerializeInvenotry In arr
                For Each drGrid As GridViewRowInfo In gv1.Rows
                    If clsCommon.CompairString(Srdr.Auto_Sr_No, drGrid.Cells(ColAutoSNo).Value) = CompairStringResult.Equal Then
                        drGrid.Cells(ColSelect).Value = True
                        Exit For
                    End If
                Next
            Next
        End If
        isInsideLoadData = False
    End Sub


    Function getQry() As String
        Dim qry As String = ""
        If clsCommon.CompairString(strCurrDocType, "Purchase Return") = CompairStringResult.Equal Then
            qry = "select Auto_Sr_No as AutoSerialNo from ( select Auto_Sr_No,max(Document_Date) as Document_Date "
            qry += " from TSPL_SERIAL_ITEM "
            qry += " where Item_Code='" + strItemCode + "' and Location_Code='" + strLocationCode + "' and not( Document_Code = '" + strCurrDocNo + "' and Document_Type = '" + strCurrDocType + "')"
            qry += " and 2=(case when In_Out_Type='O' then 2 else case when  exists (select 1 from TSPL_PI_DETAIL where TSPL_PI_DETAIL.Item_Code=TSPL_SERIAL_ITEM.Item_Code and PI_No='" + strAgaintsDocNo + "' and SRN_Id=TSPL_SERIAL_ITEM.Document_Code ) then 2 else 0 end end)"
            qry += " group by Auto_Sr_No having sum(case when In_Out_Type='I' then 1 else case when In_Out_Type='O' then -1 else 0 end end)>0 "
            qry += "  )xxx  "
        ElseIf clsCommon.CompairString(strCurrDocType, "Sale Return") = CompairStringResult.Equal Then
            qry = "select Auto_Sr_No as AutoSerialNo from ( "
            qry += " select Auto_Sr_No,max(Document_Date) as Document_Date"
            qry += " from TSPL_SERIAL_ITEM "
            qry += " where Item_Code='" + strItemCode + "' and Location_Code='" + strLocationCode + "' "
            qry += " and not( Document_Code = '" + strCurrDocNo + "' and Document_Type = '" + strCurrDocType + "') "
            qry += " and 2=(case when In_Out_Type='I' then 2 else case when  exists (select 1 from TSPL_SD_SALE_INVOICE_DETAIL where TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TSPL_SERIAL_ITEM.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE ='" + strAgaintsDocNo + "' and TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code =TSPL_SERIAL_ITEM.Document_Code ) then 2 else 0 end end) group by Auto_Sr_No "
            qry += " having sum(case when In_Out_Type='I' then 1 else case when In_Out_Type='O' then -1 else 0 end end)<=0   "
            qry += " )xxx "
        ElseIf clsCommon.CompairString(strCurrDocType, "PROD_RN") = CompairStringResult.Equal Then
            qry = "select Auto_Sr_No as AutoSerialNo from ( "
            qry += " select Auto_Sr_No,max(Document_Date) as Document_Date"
            qry += " from TSPL_SERIAL_ITEM "
            qry += " where Item_Code='" + strItemCode + "' and Location_Code='" + strLocationCode + "' "
            qry += " and not( Document_Code = '" + strCurrDocNo + "' and Document_Type = '" + strCurrDocType + "') "
            qry += " and 2=(case when In_Out_Type='I' then 2 else case when  exists (select 1 from TSPL_MF_ISSUE_DETAIL where TSPL_MF_ISSUE_DETAIL.Item_Code=TSPL_SERIAL_ITEM.Item_Code and TSPL_MF_ISSUE_DETAIL.issue_code ='" + strAgaintsDocNo + "' ) then 2 else 0 end end) group by Auto_Sr_No "
            ' qry += " having sum(case when In_Out_Type='I' then 1 else case when In_Out_Type='O' then -1 else 0 end end)<=0   "
            qry += " )xxx "
        ElseIf clsCommon.CompairString(strCurrDocType, "ISSTRAN") = CompairStringResult.Equal Then
            qry = "select Auto_Sr_No as AutoSerialNo from ( "
            qry += " select Auto_Sr_No,max(Document_Date) as Document_Date"
            qry += " from TSPL_SERIAL_ITEM "
            qry += " where Item_Code='" + strItemCode + "' and Location_Code='" + strLocationCode + "' "
            qry += " and not( Document_Code = '" + strCurrDocNo + "' and Document_Type = '" + strCurrDocType + "') "
            qry += " and 2=(case when In_Out_Type='I' then 2 else case when  exists (select 1 from TSPL_IssueItemToAssembledAsset_Detail where TSPL_IssueItemToAssembledAsset_Detail.Item_Code=TSPL_SERIAL_ITEM.Item_Code and TSPL_IssueItemToAssembledAsset_Detail.Doc_No ='" + strAgaintsDocNo + "' ) then 2 else 0 end end) group by Auto_Sr_No "
            ' qry += " having sum(case when In_Out_Type='I' then 1 else case when In_Out_Type='O' then -1 else 0 end end)<=0   "
            qry += " )xxx "
        Else
            qry = "select Auto_Sr_No as AutoSerialNo,Tag_no from ( select Auto_Sr_No,Tag_no ,max(Document_Date) as Document_Date "
            qry += " from TSPL_SERIAL_ITEM LEFT JOIN TSPL_VISI_MASTER ON Serial_No=Auto_Sr_No "
            qry += " where Item_Code='" + strItemCode + "' and Location_Code='" + strLocationCode + "' and not( Document_Code = '" + strCurrDocNo + "' and Document_Type = '" + strCurrDocType + "')"
            qry += " group by Auto_Sr_No,Tag_no having sum(case when In_Out_Type='I' and Against_Inv_Movement_Trans_Id is not null then 1 else case when In_Out_Type='O' then -1 else 0 end end)>0 "
            qry += "  )xxx "
        End If
        Return qry
    End Function

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        CancelPressed()
    End Sub

    Sub CancelPressed()
        isCencelButtonClicked = True
        Me.Close()
    End Sub
End Class

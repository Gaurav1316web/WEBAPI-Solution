Imports common
Imports System.IO
''RICHA UDL/14/05/18-000161
Public Class frmBatchItemOutNew
#Region "Variables"
    Public strItemCode As String = ""
    Public strItemName As String = ""
    Public strLocationCode As String = ""
    Public strCurrDocNo As String = ""
    Public strCurrDocType As String = ""
    Public dblqty As Double = 0
    Public strUOM As String = ""
    Public strAgaintsDocNo As String = ""
    Public isCencelButtonClicked As Boolean = False
    Public strLoadoutNo As String = ""
    Public strShipmentNo As String = ""
    Public ArrTransferNo As New ArrayList()
    Public strSplTransaction As String = ""
    Const ColSNo As String = "COLSNO"
    Const colBatchNo As String = "COLBATCHNO"
    Const colBalance As String = "COLBALANCE"
    Const colQty As String = "COLQTY"

    Public arr As List(Of clsBatchInventoryNew) = Nothing
    Const ReportID As String = "BatchInvOutNew"

    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim RunBatchFifowise As Boolean = False
    Dim AllowBatchQtyin3DecimalPlaces As Boolean = False
#End Region

    Private Sub FrmSerializeItemIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RunBatchFifowise = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.RunBatchFifowise & "'")) = 0, False, True)
        AllowBatchQtyin3DecimalPlaces = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowBatchQtyin3DecimalPlaces, clsFixedParameterCode.AllowBatchQtyin3DecimalPlaces, Nothing)) = 1, True, False))
        isInsideLoadData = True
        lblItemCode.Text = strItemCode
        lblItemName.Text = strItemName
        ''richa 9 Apr,2019 
        If AllowBatchQtyin3DecimalPlaces = True Then
            lblQty.Text = dblqty
        Else
            lblQty.Text = clsCommon.myFormat(dblqty)
        End If
        txtUOM.Text = strUOM
        LoadBlankGrid()

        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            Dim counter As Integer = 0
            For Each obj As clsBatchInventoryNew In arr
                gv1.Rows.AddNew()
                gv1.Rows(counter).Cells(colBatchNo).Value = obj.Batch_No
                gv1.Rows(counter).Cells(colBalance).Value = GetBatchBalance(obj.Batch_No)
                gv1.Rows(counter).Cells(colQty).Value = obj.Qty
                counter += 1
            Next
        End If
        gv1.Rows.AddNew()

        RefeshSNO()
        If gv1.RowCount > 0 Then
            gv1.CurrentRow = gv1.Rows(0)
            gv1.CurrentColumn = gv1.Columns(colBatchNo)
        End If
        gv1.Focus()
        isInsideLoadData = False
    End Sub

    Private Function GetBatchBalance(ByVal strBatchNo As String) As Double
        Dim qry As String = "select Qty from ( " + getQry(False, "") + ")xxxxx where BatchNo='" + strBatchNo + "' "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SrNo"
        repoLineNo.Name = ColSNo
        repoLineNo.Width = 20
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoBatchNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBatchNo.FormatString = ""
        repoBatchNo.HeaderText = "Batch No"
        repoBatchNo.Name = colBatchNo
        repoBatchNo.ReadOnly = False
        repoBatchNo.IsVisible = True
        repoBatchNo.HeaderImage = My.Resources.search4
        repoBatchNo.TextImageRelation = TextImageRelation.TextBeforeImage
        repoBatchNo.Width = 150
        gv1.MasterTemplate.Columns.Add(repoBatchNo)

        'Dim repoManualbatch As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoManualbatch.FormatString = ""
        'repoManualbatch.HeaderText = "Manual Batch No"
        'repoManualbatch.Name = colManualBatch
        'repoManualbatch.ReadOnly = False
        'repoManualbatch.IsVisible = True
        'repoManualbatch.Width = 150
        'gv1.MasterTemplate.Columns.Add(repoManualbatch)

        'Dim repoManDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        'repoManDate.Format = DateTimePickerFormat.Custom
        'repoManDate.CustomFormat = "dd/MM/yyyy"
        'repoManDate.FormatString = "{0:dd/MM/yyyy}"
        'repoManDate.HeaderText = "Mfg.Date"
        'repoManDate.Name = colMfgDate
        'repoManDate.ReadOnly = True
        'repoManDate.IsVisible = True
        'repoManDate.Width = 80
        'gv1.MasterTemplate.Columns.Add(repoManDate)

        'Dim repoExpDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        'repoExpDate.Format = DateTimePickerFormat.Custom
        'repoExpDate.CustomFormat = "dd/MM/yyyy"
        'repoExpDate.FormatString = "{0:dd/MM/yyyy}"
        'repoExpDate.HeaderText = "Expiry Date"
        'repoExpDate.Name = colExpDate
        'repoExpDate.ReadOnly = True
        'repoExpDate.IsVisible = True
        'repoExpDate.Width = 80
        'gv1.MasterTemplate.Columns.Add(repoExpDate)

        Dim repoBalanceQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBalanceQty.FormatString = "{0:n2}"
        repoBalanceQty.HeaderText = "Balance Qty"
        repoBalanceQty.Name = colBalance
        repoBalanceQty.ReadOnly = True
        repoBalanceQty.IsVisible = True
        repoBalanceQty.Width = 80
        gv1.MasterTemplate.Columns.Add(repoBalanceQty)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.HeaderText = "Qty"
        repoQty.Name = colQty
        repoQty.ReadOnly = False
        repoQty.IsVisible = True
        repoQty.Width = 80
        ''richa 9 Apr,2019 
        If AllowBatchQtyin3DecimalPlaces = True Then
            repoQty.FormatString = "{0:n3}"
            repoQty.DecimalPlaces = 3
        Else
            repoQty.FormatString = "{0:n2}"
        End If

        gv1.MasterTemplate.Columns.Add(repoQty)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        ReStoreGridLayout()

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
        Try
            If AllowToSave() Then
                arr = New List(Of clsBatchInventoryNew)
                For ii As Integer = 0 To gv1.RowCount - 1
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colBatchNo).Value) > 0 Then
                        Dim obj As clsBatchInventoryNew = New clsBatchInventoryNew()
                        obj.Batch_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colBatchNo).Value)
                        'obj.Manual_BatchNo = clsCommon.myCstr(gv1.Rows(ii).Cells(colManualBatch).Value)
                        'obj.Expiry_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colExpDate).Value)
                        'obj.Manufacture_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colMfgDate).Value)
                        obj.Qty = clsCommon.myCstr(gv1.Rows(ii).Cells(colQty).Value)
                        If obj.Qty > 0 Then
                            arr.Add(obj)
                        End If
                    End If
                Next
                Me.Close()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Dim dblEnteredQty As Decimal = 0
        Try
            For ii As Integer = 0 To gv1.RowCount - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colBatchNo).Value)
                If clsCommon.myLen(strICode) > 0 Then
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        If jj = ii Then
                            Continue For
                        End If
                        Dim strInnerICode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colBatchNo).Value)

                        If clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal Then
                            Dim Msg As String = "Same Batch Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                            Msg = Msg + Environment.NewLine + "Batch: " + strICode + ""
                            Throw New Exception(Msg)
                        End If
                    Next
                End If

                If clsCommon.myLen(gv1.Rows(ii).Cells(colBatchNo).Value) > 0 Then
                    If clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) > clsCommon.myCdbl(gv1.Rows(ii).Cells(colBalance).Value) Then
                        Throw New Exception("Error at row No " + clsCommon.myCstr(ii + 1) + Environment.NewLine + "Entered Qty " + clsCommon.myFormat(clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)) + " And Balance Qty " + clsCommon.myFormat(clsCommon.myCdbl(gv1.Rows(ii).Cells(colBalance).Value)))
                    End If
                    dblEnteredQty += clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                End If
            Next

            If AllowBatchQtyin3DecimalPlaces = True Then
                If Math.Round(dblEnteredQty, 3) <> Math.Round(dblqty, 3) Then
                    Throw New Exception("Item Qty " & Math.Round(dblqty, 3) & " And Batch Qty " & Math.Round(clsCommon.myCdbl(dblEnteredQty), 3))
                End If
            Else
                If Math.Round(dblEnteredQty, 2) <> Math.Round(dblqty, 2) Then
                    Throw New Exception("Item Qty " + clsCommon.myFormat(dblqty) + " And Batch Qty " + clsCommon.myFormat(dblEnteredQty))
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Sub RefeshSNO()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
        Next
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colBatchNo) Then
                        OpenSerialList(gv1.CurrentRow.Index, clsCommon.myCstr(gv1.CurrentRow.Cells(colBatchNo).Value), "")
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Function OpenSerialList(ByVal intRowNo As Integer, ByVal currCode As String)
        Return OpenSerialList(intRowNo, currCode, "")
    End Function

    Function OpenSerialList(ByVal intRowNo As Integer, ByVal currCode As String, ByVal strUnion As String)
        Dim qry As String = getQry(True, strUnion)
        Dim dblTotalQty As Double = 0
        Dim blnAvailable As Boolean = False
        RunBatchFifowise = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.RunBatchFifowise & "'")) = 0, False, True)
        If RunBatchFifowise = False Then
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("BatchNoFinderNewForMilk", qry)
            If dr IsNot Nothing Then
                gv1.Rows(intRowNo).Cells(colBatchNo).Value = clsCommon.myCstr(dr("BatchNo"))
                ' gv1.Rows(intRowNo).Cells(colManualBatch).Value = clsCommon.myCstr(dr("Manual_BatchNo"))
                'gv1.Rows(intRowNo).Cells(colMfgDate).Value = clsCommon.myCDate(dr("ManufactureDate"))
                'gv1.Rows(intRowNo).Cells(colExpDate).Value = clsCommon.myCDate(dr("ExpiryDate"))
                gv1.Rows(intRowNo).Cells(colBalance).Value = clsCommon.myCdbl(dr("Qty"))
            End If
            blnAvailable = True
        Else
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of clsBatchInventoryNew)
                dblTotalQty = 0

                For Each dr As DataRow In dt.Rows
                    If dblTotalQty < dblqty Then
                        Dim obj As clsBatchInventoryNew = New clsBatchInventoryNew()
                        obj.Batch_No = clsCommon.myCstr(dr("BatchNo"))
                        'obj.Manual_BatchNo = clsCommon.myCstr(dr("Manual_BatchNo"))
                        ' obj.Manufacture_Date = clsCommon.myCDate(dr("ManufactureDate"))
                        'obj.Expiry_Date = clsCommon.myCDate(dr("ExpiryDate"))
                        If dblqty - dblTotalQty >= clsCommon.myCdbl(dr("Qty")) Then
                            obj.Qty = clsCommon.myCdbl(dr("Qty"))
                        Else
                            obj.Qty = dblqty - dblTotalQty
                        End If
                        obj.UOM = strUOM
                        If obj.Qty > 0 Then
                            arr.Add(obj)
                        End If

                        dblTotalQty += clsCommon.myCdbl(dr("Qty"))

                    End If
                Next
                If dblTotalQty < dblqty Then
                    blnAvailable = False
                Else
                    blnAvailable = True
                End If
            End If

        End If
        Return blnAvailable
    End Function

    Function getQry() As String
        Return getQry(True)
    End Function

    Public Function getQry(ByVal isOrderBy As Boolean) As String
        Return getQry(isOrderBy, "")
    End Function

    Public Function getQry(ByVal isOrderBy As Boolean, ByVal strUnion As String) As String
        Dim qry As String =  "select Batch_No as BatchNo,sum(Qty * (case when In_Out_Type='I' then 1 else case when In_Out_Type='O' then -1 else 0 end end )) as Qty from (" + Environment.NewLine
        qry += " select * from (" + Environment.NewLine
        qry += " select TSPL_BATCH_ITEM_NEW.Batch_No,TSPL_BATCH_ITEM_NEW.In_Out_Type,TSPL_BATCH_ITEM_NEW.UOM as OrgUOM,TSPL_BATCH_ITEM_NEW.Qty as OrgQty, convert(decimal(18,2),(TSPL_BATCH_ITEM_NEW.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/ConvertedUOM.Conversion_Factor) as Qty" + Environment.NewLine
        qry += " from TSPL_BATCH_ITEM_NEW " + Environment.NewLine
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_BATCH_ITEM_NEW.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_BATCH_ITEM_NEW.UOM" + Environment.NewLine
        qry += " left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUOM on ConvertedUOM.Item_Code=TSPL_BATCH_ITEM_NEW.Item_Code and ConvertedUOM.UOM_Code='" + strUOM + "'" + Environment.NewLine
        qry += " where TSPL_BATCH_ITEM_NEW.Item_Code='" + strItemCode + "' and TSPL_BATCH_ITEM_NEW.Location_Code='" + strLocationCode + "' " + Environment.NewLine
        qry += " and not( TSPL_BATCH_ITEM_NEW.Document_Code = '" + strCurrDocNo + "' and TSPL_BATCH_ITEM_NEW.Document_Type = '" + strCurrDocType + "') " + Environment.NewLine
        qry += " and 1=(case when TSPL_BATCH_ITEM_NEW.In_Out_Type='I' and TSPL_BATCH_ITEM_NEW.Against_Inv_Movement_New_Trans_Id is not null then 1 else case when TSPL_BATCH_ITEM_NEW.In_Out_Type='O' then 1 else 0 end end)"
        qry += " " & strUnion & " ) xx where 2=2 "
        qry += " )xxx" + Environment.NewLine
        qry += " group by Batch_No having sum(Qty * (case when In_Out_Type='I' then 1 else case when In_Out_Type='O' then -1 else 0 end end ))>0 " + Environment.NewLine
        Return qry
    End Function

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        CancelPressed()
    End Sub

    Sub CancelPressed()
        isCencelButtonClicked = True
        Me.Close()
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub
End Class

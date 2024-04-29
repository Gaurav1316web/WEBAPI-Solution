Imports common
Public Class frmCopyBOM

#Region "Variables"
    Public strFirstPO As String = Nothing
    Public strCurrCode As String = Nothing
    Public ArrReturn As List(Of clsBillOfMaterial) = Nothing
    Dim IsInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Const colDICode As String = "ICODE"
    Const colDIName As String = "INAME"
    Const colRowType As String = "COLTYPE"
    Const colDUnit As String = "UNIT"
    Const colDItemType As String = "ITEMTYPE"
    Const colDQuantity As String = "QUANTITY"
    Const colDScrap As String = "SCRAP"
    Const colDWastage As String = "WASTAGE"
    Const colDDrawingNo As String = "DRAWINGNO"
    Const colDRemarks As String = "REMARKS"
    Const colDItemCategory As String = "ITEMCATEGORY"
    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"

    Public arrPONo As New ArrayList()
#End Region

    Private Sub FrmPendingRequistion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Sub LoadHeadData(ByVal dtAllData As DataTable)
        IsInsideLoadData = True
        LoadBlankHeadGrid()
        Dim arr As New List(Of String)
        For Each dr As DataRow In dtAllData.Rows
            Dim strCode As String = clsCommon.myCstr(dr("Code"))
            If Not arr.Contains(strCode) Then
                arr.Add(strCode)
                gvHead.Rows.AddNew()
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHSelect).Value = False
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHCode).Value = strCode
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("BOM_DATE"))
            End If
        Next
        IsInsideLoadData = False
    End Sub

    Sub LoadBlankHeadGrid()
        gvHead.Rows.Clear()
        gvHead.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colHSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvHead.MasterTemplate.Columns.Add(repoSelect)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "BOM Code"
        repoCode.Name = colHCode
        repoCode.Width = 150
        repoCode.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoCode)

        Dim repoDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Date"
        repoDate.Name = colHDate
        repoDate.Width = 100
        repoDate.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoDate)

        gvHead.ShowFilteringRow = True
        gvHead.EnableFiltering = True
        gvHead.AllowDeleteRow = False
        gvHead.AllowAddNewRow = False
        gvHead.ShowGroupPanel = False
        gvHead.AllowColumnReorder = False
        gvHead.AllowRowReorder = False
        gvHead.EnableSorting = False
        gvHead.EnableAlternatingRowColor = True
        gvHead.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvHead.MasterTemplate.ShowRowHeaderColumn = False
        gvHead.TableElement.TableHeaderHeight = 40
    End Sub

    Sub LoadBlankGridDetail()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoICategory As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICategory.FormatString = ""
        repoICategory.HeaderText = "Item Category"
        repoICategory.Name = colDItemCategory
        repoICategory.Width = 100
        repoICategory.ReadOnly = True
        repoICategory.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoICategory)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colDICode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Name"
        repoIName.Name = colDIName
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoIType As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIType.FormatString = ""
        repoIType.HeaderText = "Item Type"
        repoIType.Name = colDItemType
        repoIType.ReadOnly = True
        repoIType.IsVisible = False
        repoIType.WrapText = True
        repoIType.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoIType)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Unit"
        repoUnit.Name = colDUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = True
        repoUnit.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoDrawing As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDrawing.FormatString = ""
        repoDrawing.HeaderText = "Drawing No"
        repoDrawing.Name = colDDrawingNo
        repoDrawing.Width = 100
        repoDrawing.ReadOnly = True
        repoDrawing.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoDrawing)

        Dim repoQuantity As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQuantity.FormatString = ""
        repoQuantity.HeaderText = "Quantity"
        repoQuantity.Name = colDQuantity
        repoQuantity.ReadOnly = True
        repoQuantity.IsVisible = True
        repoQuantity.WrapText = True
        repoQuantity.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQuantity)

        Dim Scrape As GridViewDecimalColumn = New GridViewDecimalColumn()
        Scrape = New GridViewDecimalColumn()
        Scrape.FormatString = ""
        Scrape.HeaderText = "Scrap(%)"
        Scrape.Name = colDScrap
        Scrape.ReadOnly = True
        Scrape.IsVisible = True
        Scrape.WrapText = True
        Scrape.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(Scrape)

        Dim repoWastage As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoWastage.FormatString = ""
        repoWastage.HeaderText = "Wastage(%)"
        repoWastage.Name = colDWastage
        repoWastage.ReadOnly = True
        repoWastage.IsVisible = True
        repoWastage.WrapText = True
        repoWastage.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoWastage)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colDRemarks
        repoRemarks.ReadOnly = True
        repoRemarks.IsVisible = True
        repoRemarks.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        gv1.BestFitColumns()
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.ShowColumnHeaders = True
        gv1.EnableAlternatingRowColor = True
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnCancelPressed()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        btnOKPressed()
    End Sub

    Sub btnCancelPressed()
        Me.Close()
    End Sub

    Sub btnOKPressed()
        ArrReturn = New List(Of clsBillOfMaterial)
        Dim obj As clsBillOfMaterial = Nothing
        For ii As Integer = 0 To gv1.RowCount - 1
            obj = New clsBillOfMaterial()
            obj.CONSM_ITEM_CODE = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
            obj.ITEM_DESCRIPTION = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
            obj.CONSM_ITEM_CATEGORY_CODE = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDItemCategory).Value)
            obj.CONSM_ITEM_UNIT_CODE = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
            obj.CONSM_QUANTITY = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDQuantity).Value)
            obj.PROD_Drawing_No = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDDrawingNo).Value)
            obj.SCRAP_PERCENT = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDScrap).Value)
            obj.WASTAGE_PERCENT = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDWastage).Value)
            obj.REMARKS = clsCommon.myCstr(gv1.Rows(ii).Cells(colDRemarks).Value)
            If clsCommon.myLen(obj.CONSM_ITEM_CODE) > 0 Then
                ArrReturn.Add(obj)
            End If
        Next

        If ArrReturn.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one item", Me.Text)
        Else
            Me.Close()
        End If
    End Sub

    'Private Sub FrmPendingRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Keys.F5 Then
    '        btnOKPressed()
    '    ElseIf e.KeyCode = Keys.Escape Then
    '        btnCancelPressed()
    '    End If
    'End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick

    End Sub

    Private Sub gvHead_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
        If e.Column Is gvHead.Columns(colHSelect) Then
            gvHead.CurrentColumn = gvHead.Columns(colHCode)
            gvHead.CurrentColumn = gvHead.Columns(colHSelect)
        End If

        LoadDetailData()
    End Sub



    Sub LoadDetailData()
        LoadBlankGridDetail()
        arrPONo = New ArrayList()
        For ii As Integer = 0 To gvHead.RowCount - 1
            If clsCommon.myCBool(gvHead.Rows(ii).Cells(colHSelect).Value) Then
                arrPONo.Add(clsCommon.myCstr(gvHead.Rows(ii).Cells(colHCode).Value))
            End If
        Next
        If arrPONo Is Nothing OrElse arrPONo.Count <= 0 Then
            Exit Sub
        Else
            strFirstPO = arrPONo(0)
        End If
        Dim qry As String = " select max(xxx.CONSM_ITEM_CATEGORY_CODE)  as  CONSM_ITEM_CATEGORY_CODE,CONSM_ITEM_CODE ,max(ITEM_DESCRIPTION ) as ITEM_DESCRIPTION,max(CONSM_Drawing_No ) as CONSM_Drawing_No,sum(CONSM_QUANTITY) as CONSM_QUANTITY ,max(CONSM_ITEM_UNIT_CODE) as CONSM_ITEM_UNIT_CODE,avg(SCRAP_PERCENT) as SCRAP_PERCENT ,avg(WASTAGE_PERCENT) as WASTAGE_PERCENT ,max(REMARKS) as  REMARKS,max(DESCRIPTION) as DESCRIPTION,max(PROD_Drawing_No) as PROD_Drawing_No,max(rev_no) as rev_no  from(select TSPL_MF_BOM_HEAD.BOM_CODE ,BOM_DATE ,CONSM_ITEM_CODE ,ITEM_DESCRIPTION ,TSPL_MF_BOM_DETAIL.CONSM_ITEM_CATEGORY_CODE ,CONSM_QUANTITY ,CONSM_ITEM_UNIT_CODE ,SCRAP_PERCENT ,WASTAGE_PERCENT , TSPL_MF_BOM_DETAIL.REMARKS ,TSPL_MF_BOM_DETAIL.REVISION_NO ,CONSM_Drawing_No,TSPL_MF_BOM_HEAD.DESCRIPTION ,TSPL_MF_BOM_HEAD.PROD_Drawing_No,TSPL_MF_BOM_HEAD.REVISION_NO as rev_no,PROD_QUANTITY   from TSPL_MF_BOM_DETAIL left join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE = TSPL_MF_BOM_DETAIL.BOM_CODE " & _
         " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE  and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE " & _
         " where  TSPL_MF_BOM_HEAD.BOM_CODE  in (" + clsCommon.GetMulcallString(arrPONo) + "))xxx group by   CONSM_ITEM_CODE"
        Dim dtAllData As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtAllData IsNot Nothing AndAlso dtAllData.Rows.Count > 0 Then
            For Each dr As DataRow In dtAllData.Rows
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("CONSM_ITEM_CODE"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("ITEM_DESCRIPTION"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("CONSM_ITEM_UNIT_CODE"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDItemCategory).Value = clsCommon.myCdbl(dr("CONSM_ITEM_CATEGORY_CODE"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDQuantity).Value = clsCommon.myCdbl(dr("CONSM_QUANTITY"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDDrawingNo).Value = clsCommon.myCdbl(dr("CONSM_Drawing_No"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDScrap).Value = clsCommon.myCdbl(dr("SCRAP_PERCENT"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDWastage).Value = clsCommon.myCdbl(dr("WASTAGE_PERCENT"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDRemarks).Value = clsCommon.myCstr(dr("REMARKS"))
            Next
        End If

    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        If clsCommon.myLen(txtMainItem.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Item", Me.Text)
            Exit Sub
        End If

        Dim qry As String = " Select CAST(0 as bit) as Sel,TSPL_MF_BOM_DETAIL.BOM_CODE  as Code,TSPL_MF_BOM_HEAD.BOM_DATE from TSPL_MF_BOM_DETAIL " & _
       " left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE =TSPL_MF_BOM_DETAIL.BOM_CODE  " & _
        " where  TSPL_MF_BOM_HEAD.PROD_ITEM_CODE ='" + txtMainItem.Value + "' and TSPL_MF_BOM_HEAD.BOM_DATE  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MF_BOM_HEAD.BOM_DATE  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' order by TSPL_MF_BOM_HEAD.BOM_DATE "


        LoadHeadData(clsDBFuncationality.GetDataTable(qry))
        LoadBlankGridDetail()
    End Sub

   

    Private Sub gvHead_ValueChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvHead.ValueChanging
        Try
            If Not IsInsideLoadData Then
                If Not isCellValueChanged Then
                    isCellValueChanged = True
                    If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
                        gvHead.CurrentRow.Cells(colHSelect).Value = e.NewValue
                    End If
                    LoadDetailData()
                    isCellValueChanged = False
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtMainItem__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMainItem._MYValidating
        Try
            Dim whrcls As String = " ITEM_TYPE IN ('F','S') "
            Dim qry As String = "SELECT ITEM_CODE AS CODE,ITEM_DESC AS ITEM_NAME,ITEM_TYPE AS TYPE FROM TSPL_ITEM_MASTER "
            txtMainItem.Value = clsItemMaster.getFinder(whrcls, txtMainItem.Value, isButtonClicked)

            Dim objItm As New clsItemMaster
            '' NO CLASS  FOR ITEM MASTER(FINISHED)
            Dim DT_ITEM As DataTable
            Dim STRQ As String
            STRQ = "SELECT ITEM_DESC,ITEM_TYPE,UNIT_CODE FROM TSPL_ITEM_MASTER WHERE ITEM_CODE='" & txtMainItem.Value & "'"

            DT_ITEM = clsDBFuncationality.GetDataTable(STRQ)
            If DT_ITEM.Rows.Count > 0 Then
                Me.lblMainItemName.Text = DT_ITEM.Rows(0).Item("ITEM_DESC")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmCopyBOM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
    End Sub

    Private Sub frmCopyBOM_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancelPressed()
        End If
    End Sub
End Class


'Monika----------------BM00000003196-----------BM00000003794----29/07/2014----BM00000003898
Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmChildBOScreen
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim DecimalPoint As Integer = 3
    Public ManualBOMSelect As Integer = 0
    Const colCSno As String = "CSNo"
    Const colCitemCode As String = "CItemcode"
    Const colCItemdesc As String = "CItemdesc"
    Const colBomCOde As String = "colBomCOde"
    Const colCIsChild As String = "IsChild"
    Const colCGowithCalulation As String = "GoWithCalculation"
    Const colCStockQty As String = "CStockQty"
    Const colCAvailQty As String = "CAvailQty"
    Const colCDiffStock As String = "DiffStock"
    Const colCBOQty As String = "CBOQty"
    Const colCItemUOM As String = "CItemUOM"
    Const colCParentSNO As String = "CParentSNO"
    Const colCParentItemCode As String = "ParentIcode"
    Const colCParentItemName As String = "ParentName"

    Public SFGCodes As String = ""
    Public SFGQty As String = ""
    Public SFGREFNO As String = ""
    Public SFGUOM As String = ""
    Public BO_Code As String = ""
    Public Error_Status As String = ""
    Public dtpDate As Date = Nothing
    Public Location_Code As String = Nothing

    Dim ReportID As String = "PP_CHILD_BO"
    Public isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmBatchOrderDairy)
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If
        'btn_Child_Close.Visible = MyBase.isModifyFlag
    End Sub

    Public Sub LoadBlankChildGrid()
        gv_Child_BO.Rows.Clear()
        gv_Child_BO.Columns.Clear()

        Dim repoischildSNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoischildSNo.FormatString = ""
        repoischildSNo.HeaderText = "S.No."
        repoischildSNo.Name = colCSno
        repoischildSNo.Width = 60
        repoischildSNo.ReadOnly = True
        gv_Child_BO.MasterTemplate.Columns.Add(repoischildSNo)

        Dim repoischild As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoischild.FormatString = ""
        repoischild.HeaderText = "Is Child-BO"
        repoischild.Name = colCIsChild
        repoischild.Width = 60
        gv_Child_BO.MasterTemplate.Columns.Add(repoischild)

        Dim repoischild1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoischild1.FormatString = ""
        repoischild1.HeaderText = "Go With Difference Qty"
        repoischild1.Name = colCGowithCalulation
        repoischild1.Width = 60
        gv_Child_BO.MasterTemplate.Columns.Add(repoischild1)

        Dim repoischild21S As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoischild21S.FormatString = ""
        repoischild21S.HeaderText = "Parent SNO"
        repoischild21S.Name = colCParentSNO
        repoischild21S.Width = 80
        repoischild21S.ReadOnly = True
        repoischild21S.IsVisible = False
        gv_Child_BO.MasterTemplate.Columns.Add(repoischild21S)

        Dim repoischild21 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoischild21.FormatString = ""
        repoischild21.HeaderText = "Item Code"
        repoischild21.Name = colCitemCode
        repoischild21.Width = 100
        repoischild21.ReadOnly = True
        gv_Child_BO.MasterTemplate.Columns.Add(repoischild21)

        Dim repoischild22 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoischild22.FormatString = ""
        repoischild22.HeaderText = "Description"
        repoischild22.Name = colCItemdesc
        repoischild22.Width = 130
        repoischild22.ReadOnly = True
        gv_Child_BO.MasterTemplate.Columns.Add(repoischild22)

        Dim repoischild21_1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoischild21_1.FormatString = ""
        repoischild21_1.HeaderText = "BOM Code"
        repoischild21_1.Name = colBomCOde
        repoischild21_1.Width = 100
        repoischild21_1.ReadOnly = If(ManualBOMSelect = 0, True, False)
        gv_Child_BO.MasterTemplate.Columns.Add(repoischild21_1)

        Dim repoischild22U As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoischild22U.FormatString = ""
        repoischild22U.HeaderText = "Unit"
        repoischild22U.Name = colCItemUOM
        repoischild22U.Width = 80
        repoischild22U.ReadOnly = True
        gv_Child_BO.MasterTemplate.Columns.Add(repoischild22U)

        Dim repoischild2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoischild2.FormatString = ""
        repoischild2.HeaderText = "Stock Qty"
        repoischild2.Name = colCStockQty
        repoischild2.Width = 70
        repoischild2.DecimalPlaces = DecimalPoint
        repoischild2.ReadOnly = True
        gv_Child_BO.MasterTemplate.Columns.Add(repoischild2)

        Dim repoischild3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoischild3.FormatString = ""
        repoischild3.HeaderText = "Allocated Qty"
        repoischild3.Name = colCAvailQty
        repoischild3.Width = 70
        repoischild3.DecimalPlaces = DecimalPoint
        repoischild3.ReadOnly = True
        gv_Child_BO.MasterTemplate.Columns.Add(repoischild3)

        Dim repoischild5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoischild5.FormatString = ""
        repoischild5.HeaderText = "Required Qty"
        repoischild5.Name = colCBOQty
        repoischild5.Width = 70
        repoischild5.DecimalPlaces = DecimalPoint
        repoischild5.ReadOnly = True
        gv_Child_BO.MasterTemplate.Columns.Add(repoischild5)

        Dim repoischild4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoischild4.FormatString = ""
        repoischild4.HeaderText = "Difference Qty"
        repoischild4.Name = colCDiffStock
        repoischild4.Width = 70
        repoischild4.DecimalPlaces = DecimalPoint
        repoischild4.ReadOnly = True
        gv_Child_BO.MasterTemplate.Columns.Add(repoischild4)

        Dim repoischild4PI As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoischild4PI.FormatString = ""
        repoischild4PI.HeaderText = "Parent Code"
        repoischild4PI.Name = colCParentItemCode
        repoischild4PI.Width = 70
        repoischild4PI.ReadOnly = True
        gv_Child_BO.MasterTemplate.Columns.Add(repoischild4PI)

        Dim repoischild4PID As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoischild4PID.FormatString = ""
        repoischild4PID.HeaderText = "Parent Name"
        repoischild4PID.Name = colCParentItemName
        repoischild4PID.Width = 170
        repoischild4PID.ReadOnly = True
        gv_Child_BO.MasterTemplate.Columns.Add(repoischild4PID)

        gv_Child_BO.AllowDeleteRow = False
        gv_Child_BO.AllowAddNewRow = False
        gv_Child_BO.ShowGroupPanel = False
        gv_Child_BO.AllowColumnReorder = True
        gv_Child_BO.AllowRowReorder = False
        gv_Child_BO.EnableSorting = False
        gv_Child_BO.EnableFiltering = False
        gv_Child_BO.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_Child_BO.MasterTemplate.ShowRowHeaderColumn = False
        'gv_Child_BO.Rows.AddNew()

        ReStoreGridLayout()
    End Sub

    Private Sub FrmChildBOScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DecimalPoint = CInt(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionQtyDecimalPoint, clsFixedParameterCode.ProductionQtyDecimalPoint, Nothing)))
        If DecimalPoint <= 0 Then
            DecimalPoint = 3
        End If
    End Sub

    Public Sub UpdateChildBO_Grid(ByVal btnsave As String, ByVal Icode As String, ByVal IQty As String, ByVal Item_Unit As String, ByVal ChildBO_Code As String)
        Try
            '=================at time of load if already child bo exist then update status on that columns.
            If clsCommon.CompairString(btnsave, "Update") = CompairStringResult.Equal Then
                Dim xSplitCodes() As String
                Dim xSplitQty() As String
                Dim xSplitBO() As String
                Dim xsplitUOM() As String

                xSplitCodes = Icode.Replace("'", "").Split(",")
                xSplitQty = IQty.Split(",")
                xSplitBO = ChildBO_Code.Split(",")
                xsplitUOM = Item_Unit.Split(",")
                Dim jj1 As Integer = 0
                isInsideLoadData = True

a:
                For jj As Integer = jj1 To gv_Child_BO.Rows.Count - 1
                    For ii As Integer = 0 To xSplitCodes.Length - 1
                        If clsCommon.myLen(xSplitCodes(ii)) > 0 AndAlso clsCommon.myLen(xSplitBO(ii)) > 0 AndAlso clsCommon.CompairString(xSplitCodes(ii), clsCommon.myCstr(gv_Child_BO.Rows(jj).Cells(colCitemCode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(xsplitUOM(ii), clsCommon.myCstr(gv_Child_BO.Rows(jj).Cells(colCItemUOM).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(xSplitQty(ii), clsCommon.myCstr(gv_Child_BO.Rows(jj).Cells(colCBOQty).Value)) = CompairStringResult.Equal Then
                            gv_Child_BO.Rows(jj).Cells(colCIsChild).Value = True
                            gv_Child_BO.Rows(jj).Cells(colCGowithCalulation).Value = False
                            jj1 += 1
                            If jj1 <= gv_Child_BO.Rows.Count - 1 Then
                                GoTo a
                            End If
                        ElseIf clsCommon.myLen(xSplitCodes(ii)) > 0 AndAlso clsCommon.myLen(xSplitBO(ii)) > 0 AndAlso clsCommon.CompairString(xSplitCodes(ii), clsCommon.myCstr(gv_Child_BO.Rows(jj).Cells(colCitemCode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(xsplitUOM(ii), clsCommon.myCstr(gv_Child_BO.Rows(jj).Cells(colCItemUOM).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(xSplitQty(ii), clsCommon.myCstr(gv_Child_BO.Rows(jj).Cells(colCBOQty).Value)) <> CompairStringResult.Equal Then
                            gv_Child_BO.Rows(jj).Cells(colCIsChild).Value = True
                            gv_Child_BO.Rows(jj).Cells(colCGowithCalulation).Value = True
                            jj1 += 1
                            If jj1 <= gv_Child_BO.Rows.Count - 1 Then
                                GoTo a
                            End If
                        ElseIf clsCommon.myLen(xSplitCodes(ii)) > 0 AndAlso clsCommon.CompairString(xSplitCodes(ii), clsCommon.myCstr(gv_Child_BO.Rows(jj).Cells(colCitemCode).Value)) <> CompairStringResult.Equal Then
                            gv_Child_BO.Rows(jj).Cells(colCIsChild).Value = False
                            gv_Child_BO.Rows(jj).Cells(colCGowithCalulation).Value = False
                            'Continue For
                        End If
                    Next
                    jj1 += 1
                Next
                isInsideLoadData = False
            End If
        Catch ex As Exception
            isInsideLoadData = False
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btn_Child_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Child_Close.Click
        Try
            gv_Child_BO.Rows.AddNew()
            gv_Child_BO.CurrentRow = gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1)
            SFGCodes = ""
            SFGQty = ""
            SFGREFNO = ""
            SFGUOM = ""
            Error_Status = ""
            Dim qry As String = ""

            For Each grow As GridViewRowInfo In gv_Child_BO.Rows
                If clsCommon.myCBool(grow.Cells(colCIsChild).Value) = True Then
                    qry = "select count(*) from tspl_pp_bom_head where isnull(TSPL_PP_BOM_HEAD.is_osp,0)<>1 and prod_item_code='" + clsCommon.myCstr(grow.Cells(colCitemCode).Value) + "' and isnull(is_post,'0')='1' and ('" + clsCommon.GetPrintDate(dtpDate, "dd/MMM/yyyy") + "' between valid_from_date and valid_upto_date)"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                    If check <= 0 Then
                        clsCommon.MyMessageBoxShow("Create BOM for [" + clsCommon.myCstr(grow.Cells(colCitemCode).Value) + "] item first.")
                        Error_Status = "E"
                        Me.Close()
                        Exit Sub
                    End If
                End If
            Next


            '====================get SFG Item from grid=====================
            '==========================insert into temp table
            
            qry = "delete from TEMP_CHILDBO_REF_NO where main_bo_code='" + BO_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)


            For Each grow As GridViewRowInfo In gv_Child_BO.Rows
                qry = "select Structure_Code from TSPL_ITEM_MASTER where item_code='" + clsCommon.myCstr(grow.Cells(colCitemCode).Value) + "'"
                Dim structcode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                If clsCommon.myCBool(grow.Cells(colCIsChild).Value) = True AndAlso clsCommon.myCBool(grow.Cells(colCGowithCalulation).Value) = True Then
                    SFGCodes = SFGCodes + "," + clsCommon.myCstr(grow.Cells(colCitemCode).Value)
                    SFGQty = SFGQty + "," + clsCommon.myCstr(grow.Cells(colCDiffStock).Value)
                    SFGREFNO = SFGREFNO + "," + clsCommon.myCstr(grow.Cells(colCSno).Value)
                    SFGUOM = SFGUOM + "," + clsCommon.myCstr(grow.Cells(colCItemUOM).Value)

                    Dim level As Integer = 1
                    For Each c As Char In clsCommon.myCstr(grow.Cells(colCSno).Value)
                        If clsCommon.CompairString(c, ".") = CompairStringResult.Equal Then
                            level += 1
                        End If
                    Next
                    clsDBFuncationality.ExecuteNonQuery("insert into TEMP_CHILDBO_REF_NO select '" + BO_Code + "','" + clsCommon.myCstr(grow.Cells(colCSno).Value) + "','" + clsCommon.myCstr(grow.Cells(colCParentSNO).Value) + "','','" + clsCommon.myCstr(level) + "','" + clsCommon.myCstr(grow.Cells(colCitemCode).Value) + "','" + clsCommon.myCstr(grow.Cells(colCItemUOM).Value) + "','" + clsCommon.myCstr(grow.Cells(colCDiffStock).Value) + "','" + structcode + "','1','1','" + clsCommon.myCstr(grow.Cells(colBomCOde).Value) + "'")

                ElseIf clsCommon.myCBool(grow.Cells(colCIsChild).Value) = True AndAlso clsCommon.myCBool(grow.Cells(colCGowithCalulation).Value) = False Then
                    SFGCodes = SFGCodes + "," + clsCommon.myCstr(grow.Cells(colCitemCode).Value)
                    SFGQty = SFGQty + "," + clsCommon.myCstr(grow.Cells(colCBOQty).Value)
                    SFGREFNO = SFGREFNO + "," + clsCommon.myCstr(grow.Cells(colCSno).Value)
                    SFGUOM = SFGUOM + "," + clsCommon.myCstr(grow.Cells(colCItemUOM).Value)

                    Dim level As Integer = 1
                    For Each c As Char In clsCommon.myCstr(grow.Cells(colCSno).Value)
                        If clsCommon.CompairString(c, ".") = CompairStringResult.Equal Then
                            level += 1
                        End If
                    Next
                    clsDBFuncationality.ExecuteNonQuery("insert into TEMP_CHILDBO_REF_NO select '" + BO_Code + "','" + clsCommon.myCstr(grow.Cells(colCSno).Value) + "','" + clsCommon.myCstr(grow.Cells(colCParentSNO).Value) + "','','" + clsCommon.myCstr(level) + "','" + clsCommon.myCstr(grow.Cells(colCitemCode).Value) + "','" + clsCommon.myCstr(grow.Cells(colCItemUOM).Value) + "','" + clsCommon.myCstr(grow.Cells(colCBOQty).Value) + "','" + structcode + "','1','0','" + clsCommon.myCstr(grow.Cells(colBomCOde).Value) + "'")
                ElseIf clsCommon.myLen(grow.Cells(colCSno).Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colCSno).Value), "0") <> CompairStringResult.Equal Then
                    clsDBFuncationality.ExecuteNonQuery("insert into TEMP_CHILDBO_REF_NO select '" + BO_Code + "','" + clsCommon.myCstr(grow.Cells(colCSno).Value) + "','" + clsCommon.myCstr(grow.Cells(colCParentSNO).Value) + "','','0','" + clsCommon.myCstr(grow.Cells(colCitemCode).Value) + "','" + clsCommon.myCstr(grow.Cells(colCItemUOM).Value) + "','0','" + structcode + "','0','0','" + clsCommon.myCstr(grow.Cells(colBomCOde).Value) + "'")
                End If
            Next

            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub gv_Child_BO_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_Child_BO.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                If e.Column Is gv_Child_BO.Columns(colCIsChild) OrElse e.Column Is gv_Child_BO.Columns(colCGowithCalulation) OrElse e.Column Is gv_Child_BO.Columns(colBomCOde) Then
                    isCellValueChanged = True
                    '=================if not want to create child BO and click on Go calculation button then it should be unchecked
                    If clsCommon.myCBool(gv_Child_BO.CurrentRow.Cells(colCIsChild).Value) = False AndAlso clsCommon.myCBool(gv_Child_BO.CurrentRow.Cells(colCGowithCalulation).Value) = True Then
                        gv_Child_BO.CurrentRow.Cells(colCGowithCalulation).Value = False
                    End If

                    If e.Column Is gv_Child_BO.Columns(colCIsChild) Then
                        FillChild_SubChildDetailInGrid(clsCommon.myCBool(gv_Child_BO.CurrentRow.Cells(colCIsChild).Value), clsCommon.myCstr(gv_Child_BO.CurrentRow.Cells(colCitemCode).Value), CInt(clsCommon.myCdbl(gv_Child_BO.CurrentRow.Index)))
                    End If
                    If e.Column Is gv_Child_BO.Columns(colBomCOde) Then
                        Dim qry As String = "select BOM_CODE as [Code],DESCRIPTION as [Name],PROD_QUANTITY as [Build Qty] from TSPL_PP_BOM_HEAD  "
                        Dim WhrClause As String = ""
                        WhrClause = " PROD_ITEM_CODE = '" & gv_Child_BO.CurrentRow.Cells(colCitemCode).Value & "' AND Is_POST=1"
                        gv_Child_BO.CurrentRow.Cells(colBomCOde).Value = clsCommon.ShowSelectForm("TSPL_PP_BOM_HEAD", qry, "Code", WhrClause, gv_Child_BO.CurrentRow.Cells(colCitemCode).Value, "BOM_CODE", False)
                    End If
                    isCellValueChanged = False
                End If
            End If
        End If
    End Sub

    Private Sub FillChild_SubChildDetailInGrid(ByVal NewValue As Boolean, ByVal sfgcode As String, ByVal IntRow As Integer)
        Try
            isInsideLoadData = True

            If clsCommon.myCBool(gv_Child_BO.CurrentRow.Cells(colCIsChild).Value) = False Then
                For ii As Integer = gv_Child_BO.Rows.Count - 1 To 0 Step -1
                    If clsCommon.myLen(gv_Child_BO.Rows(ii).Cells(colCitemCode).Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Child_BO.Rows(IntRow).Cells(colCSno).Value), clsCommon.myCstr(gv_Child_BO.Rows(ii).Cells(colCParentSNO).Value)) = CompairStringResult.Equal Then
                        gv_Child_BO.Rows.RemoveAt(ii)
                    End If
                Next
            ElseIf clsCommon.myCBool(gv_Child_BO.CurrentRow.Cells(colCIsChild).Value) = True Then
                Dim UOM As String = clsCommon.myCstr(gv_Child_BO.Rows(IntRow).Cells(colCItemUOM).Value)
                Dim counter As Integer = 1

                Dim qry As String = "select top 1 TSPL_PP_BOM_HEAD.BOM_CODE from TSPL_PP_BOM_HEAD where isnull(TSPL_PP_BOM_HEAD.is_osp,0)<>1 and isnull(TSPL_PP_BOM_HEAD.Is_Post,'0')='1' and ('" + clsCommon.GetPrintDate(dtpDate, "dd/MMM/yyyy") + "' between TSPL_PP_BOM_HEAD.Valid_FROM_DATE and TSPL_PP_BOM_HEAD.Valid_UPTO_DATE) and TSPL_PP_BOM_head.prod_ITEM_CODE in ('" + sfgcode + "') and TSPL_PP_BOM_head.PROD_ITEM_UNIT_CODE in ('" + UOM + "') "
                qry += " order by TSPL_PP_BOM_HEAD.bom_date desc"
                Dim bom_code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                If bom_code IsNot Nothing AndAlso clsCommon.myLen(bom_code) > 0 Then
                    qry = "select row_number() over (order by TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE) as sno,TSPL_PP_BOM_HEAD.prod_quantity,TSPL_PP_BOM_HEAD.bom_code,TSPL_PP_BOM_HEAD.prod_item_code,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,TSPL_PP_BOM_ITEM_DETAIL.FAT,TSPL_PP_BOM_ITEM_DETAIL.SNF,TSPL_PP_BOM_ITEM_DETAIL.remarks from TSPL_PP_BOM_ITEM_DETAIL left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.bom_code=TSPL_PP_BOM_ITEM_DETAIL.bom_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PP_BOM_ITEM_DETAIL.item_code where TSPL_PP_BOM_ITEM_DETAIL.bom_code in ('" + bom_code + "') and tspl_item_master.item_type in ('F','S')"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    counter = dt.Rows.Count

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            If clsCommon.CompairString(clsItemMaster.GetItemType(clsCommon.myCstr(dr("ITEM_CODE")), Nothing), "F") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsItemMaster.GetItemType(clsCommon.myCstr(dr("ITEM_CODE")), Nothing), "S") <> CompairStringResult.Equal Then
                                Continue For
                            End If
                            gv_Child_BO.Rows.AddNew()

                            Dim rawqty As Decimal = 0
                            If clsCommon.myCBool(gv_Child_BO.Rows(IntRow).Cells(colCGowithCalulation).Value) = True Then
                                rawqty = Math.Round((clsCommon.myCdbl(dr("QUANTITY")) / clsCommon.myCdbl(dr("prod_quantity"))) * clsCommon.myCdbl(gv_Child_BO.Rows(IntRow).Cells(colCDiffStock).Value), DecimalPoint)
                            Else
                                rawqty = Math.Round((clsCommon.myCdbl(dr("QUANTITY")) / clsCommon.myCdbl(dr("prod_quantity"))) * clsCommon.myCdbl(gv_Child_BO.Rows(IntRow).Cells(colCBOQty).Value), DecimalPoint)
                            End If

                            gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells(colCSno).Value = clsCommon.myCstr(gv_Child_BO.Rows(IntRow).Cells(colCSno).Value) + "." + clsCommon.myCstr(counter)
                            gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("IsChild").Value = False 'colCIsChild
                            gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("GoWithCalculation").Value = False 'colCGowithCalulation
                            gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("CStockQty").Value = Math.Round(clsProcessBatchOrder.GetBalance(clsCommon.myCstr(dr("ITEM_CODE")), clsItemMaster.GetItemProductType(clsCommon.myCstr(dr("ITEM_CODE")), Nothing), clsCommon.myCstr(dr("UNIT_CODE")), Location_Code, BO_Code, dtpDate, Nothing), DecimalPoint)
                            gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("CAvailQty").Value = Math.Round(clsProcessBatchOrder.GetBOAvailQty(BO_Code, clsCommon.myCstr(dr("ITEM_CODE")), clsCommon.myCstr(dr("UNIT_CODE")), Location_Code), DecimalPoint)

                            If clsCommon.myCdbl(gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("CAvailQty").Value) <= 0 Then
                                gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("CStockQty").Value = 0
                                gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("CAvailQty").Value = 0
                            End If

                            gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("DiffStock").Value = Math.Round(clsCommon.myCdbl(gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("CStockQty").Value) - clsCommon.myCdbl(gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("CAvailQty").Value) - clsCommon.myCdbl(rawqty), DecimalPoint)
                            gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("CItemcode").Value = clsCommon.myCstr(dr("ITEM_CODE"))
                            gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("CItemdesc").Value = clsItemMaster.GetItemName(clsCommon.myCstr(dr("ITEM_CODE")), Nothing)
                            gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("CItemUOM").Value = clsCommon.myCstr(dr("UNIT_CODE"))
                            gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("CBOQty").Value = clsCommon.myCdbl(rawqty) 'colCBOQty
                            gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("CParentSNO").Value = clsCommon.myCstr(gv_Child_BO.Rows(IntRow).Cells(colCSno).Value) 'use for taking reference of child,sub-child
                            gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells(colCParentItemCode).Value = clsCommon.myCstr(gv_Child_BO.Rows(IntRow).Cells(colCitemCode).Value) 'use for taking reference of child,sub-child
                            gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells(colCParentItemName).Value = clsCommon.myCstr(gv_Child_BO.Rows(IntRow).Cells(colCItemdesc).Value) 'use for taking reference of child,sub-child

                            If clsCommon.myCdbl(gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("DiffStock").Value) <= 0 Then
                                gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("GoWithCalculation").ReadOnly = True
                            Else
                                gv_Child_BO.Rows(gv_Child_BO.Rows.Count - 1).Cells("GoWithCalculation").ReadOnly = False
                            End If

                            gv_Child_BO.Rows.Move(gv_Child_BO.Rows.Count - 1, IntRow + 1)
                            counter -= 1
                        Next
                    Else
                        'clsCommon.MyMessageBoxShow("No Finished and Semi-FInished items exist for selected item.")
                    End If
                Else
                    gv_Child_BO.Rows(IntRow).Cells(colCIsChild).Value = False
                    gv_Child_BO.Rows(IntRow).Cells(colCGowithCalulation).Value = False
                    gv_Child_BO.Rows(IntRow).Cells(colCIsChild).ReadOnly = True
                    gv_Child_BO.Rows(IntRow).Cells(colCGowithCalulation).ReadOnly = True
                    Throw New Exception("No Bom detail exist for selected item.")
                End If
            End If

            isInsideLoadData = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
       
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        'save
        If clsCommon.myLen(ReportID) > 0 Then
            gv_Child_BO.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv_Child_BO.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv_Child_BO.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv_Child_BO.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv_Child_BO.Columns.Count - 1 Step ii + 1
                        gv_Child_BO.Columns(ii).IsVisible = False
                        gv_Child_BO.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv_Child_BO.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        'delete
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Delete layout successfully", "Information")
    End Sub

    
   
End Class

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common

Public Class frmItemReorder
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

#Region "Variables"
    Dim dt As DataTable
    Dim isInsideLoadData As Boolean = False
    Const colICode As String = "colICode"
    Const colIDesc As String = "colIDesc"
    Const colItype As String = "colItype"
    Const colSelect As String = "colSelect"
    Const colUnit As String = "colUnit"
    Const colqty As String = "colqty"
    Public strLocation As String = Nothing
    Public strReqQty As Decimal = 0
    Public strBalQty As Decimal = 0
    Public UnitCode As String = Nothing
    Private isNewEntry As Boolean = False

#End Region

    Private Sub SetUserMgmtNew()

    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoCheck As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoCheck.FormatString = ""
        repoCheck.HeaderText = "Select"
        repoCheck.Name = colSelect
        repoCheck.Width = 80
        gv1.MasterTemplate.Columns.Add(repoCheck)

        Dim repoACCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACCode.FormatString = ""
        repoACCode.HeaderText = "Item Code"
        repoACCode.Name = colICode
        repoACCode.Width = 100
        repoACCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoACCode)

        Dim repoIDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIDesc.FormatString = ""
        repoIDesc.HeaderText = "Item Description"
        repoIDesc.Name = colIDesc
        repoIDesc.Width = 150
        repoIDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIDesc)

        Dim repoTYpe As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTYpe.FormatString = ""
        repoTYpe.HeaderText = "Item Type"
        repoTYpe.Name = colItype
        repoTYpe.Width = 100
        repoTYpe.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTYpe)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Unit Code"
        repoUnit.Name = colUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoqty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Qty"
        repoqty.Name = colqty
        repoqty.Width = 100
        repoqty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoqty)



        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim obj As New clsRequistionHead()
            obj.ArrTr = New List(Of clsRequistionDetail)
            Dim intLineNo As Integer = 1
            Dim strcheck As String = ""
            Dim line As Integer = 0
            obj.Requisition_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
            obj.On_Hold = 0
            obj.Location = strLocation
            obj.RQ_Detail_Total_Amt = 0
            obj.Total_RQ_Amt = 0
            obj.Mode_Of_Transport = "By Road"
            obj.Is_Internal = "N"
            obj.Requisition_Type = "L"
            obj.Category = "Regular"
            obj.close_yn = "N"
            obj.Approvel_Level_Required = 2
            obj.Description = "Auto Indent"
            obj.Dept = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Segment_code from TSPL_USER_MASTER where User_Code ='" + objCommonVar.CurrentUserCode + "'", trans))
            obj.Dept_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Segment_name from TSPL_GL_SEGMENT_CODE where Seg_No=3 and Segment_code='" + obj.Dept + "'", trans))
            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objTr As New clsRequistionDetail()
                If clsCommon.CompairString(grow.Cells(colSelect).Value, True) = CompairStringResult.Equal Then
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIDesc).Value)
                    obj.Item_Type = clsCommon.myCstr(grow.Cells(colItype).Value)
                    objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.Requisition_Qty = clsCommon.myCstr(grow.Cells(colqty).Value)
                    objTr.Balance_Qty = clsCommon.myCstr(grow.Cells(colqty).Value)
                    objTr.Requisition_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                    objTr.ItemReorder = "Yes"
                    obj.ItemReorder = "Yes"
                    '' objTr.Unit_Code = UnitCode
                    Dim qryy As String = " select TOP 1 ISNULL(TSPL_SRN_DETAIL.Landed_Cost_Rate,0) from TSPL_SRN_HEAD LEFT OUTER JOIN TSPL_SRN_DETAIL ON TSPL_SRN_DETAIL.SRN_No=TSPL_SRN_HEAD.SRN_No where Item_Code='" + clsCommon.myCstr(grow.Cells(colICode).Value) + "' ORDER BY TSPL_SRN_HEAD.SRN_Date DESC "
                    objTr.Item_Cost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qryy, trans))
                    objTr.Item_Net_Amt = 0
                    objTr.Status = "N"


                    '' Head Part Save
                    If clsCommon.CompairString(obj.ItemReorder, "Yes") = CompairStringResult.Equal Then
                        strcheck = clsDBFuncationality.getSingleValue("select Requisition_Id from TSPL_REQUISITION_HEAD where Item_Type='" & obj.Item_Type & "' and convert(varchar,Requisition_Date,103)=convert(varchar,'" & obj.Requisition_Date & "',103) and Status=0 and Description='Auto Indent'", trans)
                        If clsCommon.myLen(strcheck) > 0 Then
                            isNewEntry = False
                            obj.Requisition_Id = strcheck
                            line = clsDBFuncationality.getSingleValue("select top 1 Line_No from TSPL_REQUISITION_DETAIL where Requisition_Id='" & obj.Requisition_Id & "' order by line_no desc ", trans)
                        Else
                            isNewEntry = True
                        End If

                    End If
                    If (isNewEntry) Then
                        If clsCommon.CompairString(obj.Item_Type, "F") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Is_Internal, "Y") = CompairStringResult.Equal Then
                            obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, obj.Requisition_Date, clsDocType.frmStoreRequistion, clsDocTransactionType.FinishedGoodInternal, obj.Location)
                        ElseIf clsCommon.CompairString(obj.Item_Type, "F") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Is_Internal, "N") = CompairStringResult.Equal Then
                            obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, obj.Requisition_Date, clsDocType.PurchaserRegusitsion, clsDocTransactionType.FinishedGoodExternal, obj.Location)
                        ElseIf clsCommon.CompairString(obj.Item_Type, "S") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Is_Internal, "Y") = CompairStringResult.Equal Then
                            obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, obj.Requisition_Date, clsDocType.frmStoreRequistion, clsDocTransactionType.SemiFinishedGoodInternal, obj.Location)
                        ElseIf clsCommon.CompairString(obj.Item_Type, "S") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Is_Internal, "N") = CompairStringResult.Equal Then
                            obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, obj.Requisition_Date, clsDocType.PurchaserRegusitsion, clsDocTransactionType.SemiFinishedGoodExternal, obj.Location)
                        ElseIf (clsCommon.CompairString(obj.Item_Type, "O") Or clsCommon.CompairString(obj.Item_Type, "R") Or clsCommon.CompairString(obj.Item_Type, "P") = CompairStringResult.Equal) And clsCommon.CompairString(obj.Is_Internal, "Y") = CompairStringResult.Equal Then
                            obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, obj.Requisition_Date, clsDocType.frmStoreRequistion, clsDocTransactionType.OtherInternal, obj.Location)
                        ElseIf (clsCommon.CompairString(obj.Item_Type, "O") Or clsCommon.CompairString(obj.Item_Type, "R") Or clsCommon.CompairString(obj.Item_Type, "P") = CompairStringResult.Equal) And clsCommon.CompairString(obj.Is_Internal, "N") = CompairStringResult.Equal Then
                            obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, obj.Requisition_Date, clsDocType.PurchaserRegusitsion, clsDocTransactionType.OtherExternal, obj.Location)
                        Else
                            Throw New Exception("Item Type is Not Correct To Generate the Transaction Code")
                        End If

                        If (clsCommon.myLen(obj.Requisition_Id) <= 0) Then
                            Throw New Exception("Error in Document Code Generation")
                        End If
                    End If


                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Requisition_Date", clsCommon.GetPrintDate(obj.Requisition_Date, "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Cust_OrderNo", obj.Cust_OrderNo)

                    clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
                    clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                    clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                    clsCommon.AddColumnsForChange(coll, "On_Hold", obj.On_Hold)
                    clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                    clsCommon.AddColumnsForChange(coll, "RQ_Detail_Total_Amt", obj.RQ_Detail_Total_Amt)
                    clsCommon.AddColumnsForChange(coll, "Total_RQ_Amt", obj.Total_RQ_Amt)
                    clsCommon.AddColumnsForChange(coll, "Mode_Of_Transport", obj.Mode_Of_Transport)
                    clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
                    clsCommon.AddColumnsForChange(coll, "Is_Internal", obj.Is_Internal)
                    clsCommon.AddColumnsForChange(coll, "Requisition_Type", obj.Requisition_Type)
                    clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept, True)
                    clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)
                    clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
                    clsCommon.AddColumnsForChange(coll, "Request_By", obj.Request_By)
                    clsCommon.AddColumnsForChange(coll, "PROJECT_ID", obj.PROJECT_ID, True)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                    clsCommon.AddColumnsForChange(coll, "close_yn", obj.close_yn)
                    clsCommon.AddColumnsForChange(coll, "Category", clsCommon.myCstr(obj.Category))
                    clsCommon.AddColumnsForChange(coll, "Capex_Code", clsCommon.myCstr(obj.Capex_Code), True)
                    clsCommon.AddColumnsForChange(coll, "Capex_SubCode", clsCommon.myCstr(obj.Capex_SubCode), True)
                    clsCommon.AddColumnsForChange(coll, "Emergency", CInt(obj.Emergency))
                    If objCommonVar.IsDemoERP Then
                        clsCommon.AddColumnsForChange(coll, "Approvel_Level_Required", 2 + obj.Approvel_Level_Required) ''2 is Added for Budgetry and Function Approval
                    End If

                    Dim s As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                    If isNewEntry Then
                        clsCommon.AddColumnsForChange(coll, "Requisition_Id", obj.Requisition_Id)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REQUISITION_HEAD", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REQUISITION_HEAD", OMInsertOrUpdate.Update, "Requisition_Id='" + obj.Requisition_Id + "'", trans)
                    End If

                    If clsCommon.myLen(strcheck) > 0 Then
                        intLineNo = line + 1
                    Else
                        intLineNo = 1
                    End If

                    '' Detail Part Save
                    Dim coll1 As New Hashtable()
                    clsCommon.AddColumnsForChange(coll1, "Requisition_Id", obj.Requisition_Id)
                    clsCommon.AddColumnsForChange(coll1, "Line_No", intLineNo)

                    clsCommon.AddColumnsForChange(coll1, "Item_Code", objTr.Item_Code)
                    clsCommon.AddColumnsForChange(coll1, "Item_Desc", objTr.Item_Desc)

                    clsCommon.AddColumnsForChange(coll1, "Requisition_Qty", objTr.Requisition_Qty)
                    clsCommon.AddColumnsForChange(coll1, "Balance_Qty", objTr.Balance_Qty)
                    clsCommon.AddColumnsForChange(coll1, "Location", obj.Location)
                    clsCommon.AddColumnsForChange(coll1, "Item_Cost", objTr.Item_Cost)
                    clsCommon.AddColumnsForChange(coll1, "Status", objTr.Status)

                    clsCommon.AddColumnsForChange(coll1, "Item_Net_Amt", objTr.Item_Net_Amt)
                    clsCommon.AddColumnsForChange(coll1, "Unit_Code", objTr.Unit_Code)



                    If clsCommon.CompairString(obj.ItemReorder, "Yes") = CompairStringResult.Equal Then
                        If clsCommon.myLen(strcheck) > 0 Then
                            clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_REQUISITION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_REQUISITION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                        End If
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REQUISITION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    End If



                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.ArrTr.Add(objTr)
                    End If



                End If

            Next

            If (obj.ArrTr Is Nothing OrElse obj.ArrTr.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at list one Item")
                Exit Sub
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully")
                ''===Sanjeet(03/01/2017) for notifiaction====
                'Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnPurchaseRequistion + "'", trans))
                'If clsCommon.CompairString(strNotificationOn, "S") = CompairStringResult.Equal Then
                '    If clsCommon.CompairString(clsCommon.myCstr(obj.Description), "Auto Indent") = CompairStringResult.Equal Then
                '        Dim objnotify As New clsNotificationReplace
                '        objnotify.DocNo = obj.Requisition_Id
                '        objnotify.DocDate = obj.Requisition_Date
                '        objnotify.DocAmt = obj.Total_RQ_Amt
                '        clsNotificationHead.SendNotification(clsUserMgtCode.mbtnPurchaseRequistion, objnotify, "P", trans)
                '        'trans.Commit()
                '    End If
                'End If
            End If
            trans.Commit()
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            loadReorderLevel()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub


    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Reset()
    End Sub

    Sub Reset()
        gv1.DataSource = Nothing
    End Sub

    Private Sub frmItemReorderLevel1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub frmItemReorderLevel1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save")
        LoadBlankGrid()
        loadReorderLevel()

    End Sub
    Sub loadReorderLevel()
        LoadBlankGrid()
        Dim qry As String = ""
        qry = "select z.ItemCode,z.ItemType,z.ItemDesc ,z.Qty,z.Unit  from (select coalesce(xx.ItemCode,TSPL_ITEM_REORDER_LEVEL_NEW.item_Code) as ItemCode,(((TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Qty)*uom1.Conversion_Factor)/TSPL_ITEM_UOM_DETAIL.Conversion_Factor-isnull(xxx.Qty,0)) as Qty,TSPL_ITEM_MASTER.Item_Type as ItemType," & _
                       " TSPL_ITEM_MASTER.Item_Desc as ItemDesc ,TSPL_ITEM_UOM_DETAIL.UOM_Code as Unit from TSPL_ITEM_REORDER_LEVEL_NEW left outer join (select TSPL_INVENTORY_MOVEMENT.Item_Code as ItemCode," & _
                       " (SUM(TSPL_INVENTORY_MOVEMENT.Stock_Qty * case when TSPL_INVENTORY_MOVEMENT.InOut ='I' then 1 else -1 end)) as Balance from TSPL_INVENTORY_MOVEMENT where TSPL_INVENTORY_MOVEMENT.Location_Code='" + strLocation + "'  group by TSPL_INVENTORY_MOVEMENT.Item_Code)xx on xx.ItemCode=TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code" & _
                       " left outer join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code" & _
                       " left outer join (select TSPL_REQUISITION_DETAIL.Item_Code,SUM(TSPL_REQUISITION_DETAIL.Requisition_Qty) AS Qty from TSPL_REQUISITION_DETAIL " & _
                       " left outer join TSPL_REQUISITION_HEAD ON TSPL_REQUISITION_HEAD.Requisition_Id=TSPL_REQUISITION_DETAIL.Requisition_Id " & _
                       " WHERE TSPL_REQUISITION_HEAD.Status=0  group by TSPL_REQUISITION_DETAIL.Item_Code)xxx on xxx.Item_Code = TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code" & _
                       " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code left outer join TSPL_ITEM_UOM_DETAIL uom1 on uom1.Item_Code = TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code and uom1.UOM_Code=(case when isnull(TSPL_ITEM_REORDER_LEVEL_NEW.UOM_Code,'')='' then uom1.UOM_Code else TSPL_ITEM_REORDER_LEVEL_NEW.Uom_Code end)" & _
                       " where TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code='" + strLocation + "' and TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level>coalesce(xx.Balance,0)" & _
                       " and TSPL_ITEM_UOM_DETAIL.Stocking_Unit='Y' and TSPL_ITEM_REORDER_LEVEL_NEW.Apply='Y')z where z.Qty>0"

        dt = clsDBFuncationality.GetDataTable(qry, Nothing)
        For Each dtrow As DataRow In dt.Rows
            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dtrow("ItemCode"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colIDesc).Value = clsCommon.myCstr(dtrow("ItemDesc"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colItype).Value = clsCommon.myCstr(dtrow("ItemType"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colqty).Value = clsCommon.myCdbl(dtrow("Qty"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dtrow("Unit"))
        Next
    End Sub
    Private Sub CheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells(colSelect).Value = False
        Next
        For ii As Integer = 0 To gv.ChildRows.Count - 1
            gv.ChildRows(ii).Cells(colSelect).Value = True
        Next
    End Sub

    Private Sub UnCheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells(colSelect).Value = False
        Next
    End Sub
    Private Sub chk_applyall_CheckedChanged(sender As Object, e As EventArgs) Handles chk_applyall.CheckedChanged
        If chk_applyall.Checked = True Then
            For ii As Integer = 0 To gv1.RowCount - 1
                CheckedAll(gv1)
            Next
        Else
            For ii As Integer = 0 To gv1.RowCount - 1
                UnCheckedAll(gv1)
            Next
        End If
    End Sub


End Class

Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
'''' <summary>
'''' ''''''''''''''''''''''''TicketNo='BM00000001531''''''''''''''''''''''''''''''''''''''''
'''' </summary>
'''' <remarks></remarks>

Public Class frmAssemblies
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim icodestatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
    Private isCellValueChangedOpen As Boolean = False
    Dim isLoadTime As Boolean = False
    Public Document_No As String = Nothing
    '' component grid columns
    Const colIcodeStatus As String = "IcodeStatus"
    Const colLineNo As String = "LineNo"
    Const colItemCode As String = "ItemCode"
    Const colitemDesc As String = "ItemDesc"
    Const colqty As String = "Qty"
    Const colUnitCode As String = "UnitCode"
    Const colItemstatus As String = "ItemStatus"
    Const colLocationCode As String = "Location"
    Const colSerialNo As String = "colSerialNo"

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmAssemblies)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        '--preeti gupta-ticket no[BM00000003177]
        If btnsave.Visible = True Then
            RadMenuItem1.Enabled = True
            RadMenuItem2.Enabled = True
        Else
            RadMenuItem1.Enabled = False
            RadMenuItem2.Enabled = False
        End If
    End Sub

    Function fillgridcombobox() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "OK"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "SCRAP"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Sub LoadGridColumns()
        gvBOM.Rows.Clear()
        gvBOM.Columns.Clear()
        Dim LineNo As New GridViewTextBoxColumn
        Dim ItemCategoryCode As New GridViewTextBoxColumn
        Dim ItemCode As New GridViewTextBoxColumn
        Dim itemDesc As New GridViewTextBoxColumn
        Dim qty As New GridViewDecimalColumn
        Dim UnitCode As New GridViewTextBoxColumn        
        Dim LocationCode As New GridViewTextBoxColumn
        Dim SerialNo As New GridViewTextBoxColumn

        Dim itemstatus As New GridViewComboBoxColumn
        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 70
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(LineNo)


        icodestatus.FormatString = ""
        icodestatus.HeaderText = "Item Status"
        icodestatus.Width = 80
        icodestatus.Name = colIcodeStatus
        icodestatus.DataSource = fillgridcombobox()
        icodestatus.IsVisible = False
        If clsCommon.CompairString(cboTransactionType.Text, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlDisassemblyType.Text, "Other") = CompairStringResult.Equal Then
            icodestatus.IsVisible = True
        End If
        icodestatus.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        icodestatus.DisplayMember = "Code"
        icodestatus.ValueMember = "Code"
        gvBOM.Columns.Add(icodestatus)


        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        If clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Other", False) = CompairStringResult.Equal Then
            ItemCode.ReadOnly = False
        Else
            ItemCode.ReadOnly = True
        End If

        ItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(ItemCode)

        itemDesc.FormatString = ""
        itemDesc.HeaderText = "Item Description"
        itemDesc.Name = colitemDesc
        itemDesc.Width = 200
        itemDesc.ReadOnly = True
        itemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(itemDesc)

        qty.FormatString = ""
        qty.HeaderText = "Quantity"
        qty.Name = colqty
        qty.Width = 100
        If clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Other", False) = CompairStringResult.Equal Then
            qty.ReadOnly = False
        Else
            qty.ReadOnly = True
        End If
        qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(qty)

        UnitCode.FormatString = ""
        UnitCode.HeaderText = "UOM"

        UnitCode.Name = colUnitCode
        UnitCode.Width = 100
        UnitCode.ReadOnly = True
        UnitCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(UnitCode)


        LocationCode.FormatString = ""
        LocationCode.HeaderText = "Location"
        LocationCode.Name = colLocationCode
        LocationCode.Width = 100
        'LocationCode.ReadOnly = True
        LocationCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(LocationCode)

        SerialNo.FormatString = ""
        SerialNo.HeaderText = "Serial No"
        SerialNo.Name = colSerialNo
        SerialNo.Width = 100
        'LocationCode.ReadOnly = True
        SerialNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(SerialNo)


    End Sub
   

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As clsAssemblies = clsAssemblies.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            If obj.POSTED Then
                btnsave.Enabled = False
                btnPost.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            isNewEntry = False
            txtCode.Value = obj.CODE
            dtp_AssembleDate.Value = obj.ASSEMBLY_DATE
            cboTransactionType.SelectedValue = obj.TRANSACTION_TYPE
            ddlDisassemblyType.SelectedValue = obj.DISASSEMBLY_TYPE
            fndAssemblyCode.Value = obj.ASSEMBLY_CODE
            lblAssemblyDesc.Text = obj.ASSEMBLY_DESC
            Me.txtDesc.Text = obj.DESCRIPTION
            txtComment.Text = obj.COMMENTS
            fndMainItem.Value = obj.Main_Item_Code
            fndMainItem.Tag = obj.Main_Item_Code
            lblMainItemDesc.Text = obj.Main_Item_Desc
            fndBomCode.Value = obj.BOM_CODE
            lblBomDesc.Text = obj.BOM_DESC
            cboCompoAssMethod.SelectedValue = obj.COMP_ASSEMBL_METHOD
            Me.fndLocation.Value = obj.LOCATION_CODE
            lblLocationDesc.Text = obj.LOCATION_DESC
            txtBuildQty.Text = obj.BUILD_QUANTITY

            txtDisassCost.Text = obj.DISASSEMBLY_COST
            txtQuantity.Text = obj.QUANTITY
            lblUnitName.Text = obj.BUILD_ITEM_UNIT_CODE
            Me.txtSerialNo.Text = obj.Serial_No
            txtCode.MyReadOnly = True
            '' load item details
            Dim objList1 As New List(Of clsAssembliesItemDetail)
            objList1 = obj.objList
            Me.gvBOM.Rows.Clear()
            LoadGridColumns()

            If objList1 IsNot Nothing AndAlso objList1.Count > 0 Then
                For Each obj1 As clsAssembliesItemDetail In objList1
                    Me.gvBOM.Rows.AddNew()
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLineNo).Value = obj1.LINE_NO

                    If clsCommon.CompairString(cboTransactionType.Text, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlDisassemblyType.Text, "Other") = CompairStringResult.Equal Then
                        Try
                            If clsCommon.CompairString(obj1.ItemType, "OK") = CompairStringResult.Equal Then
                                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colIcodeStatus).Value = "OK"
                            ElseIf clsCommon.CompairString(obj1.ItemType, "SCRAP") = CompairStringResult.Equal Then
                                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colIcodeStatus).Value = "SCRAP"
                            End If
                        Catch exx As Exception
                        End Try
                    End If
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value = obj1.CONSM_ITEM_CODE
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colUnitCode).Value = obj1.CONSM_ITEM_UNIT_CODE
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Value = obj1.CONSM_QUANTITY
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Tag = (obj1.CONSM_QUANTITY / Val(Me.txtQuantity.Text)) * Val(txtBuildQty.Text)
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colitemDesc).Value = obj1.ITEM_DESCRIPTION
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLocationCode).Value = obj1.LOCATION_CODE
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSerialNo).Value = obj1.Serial_No
                Next
            End If

            ''For Custom Fields
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.LoadData(obj.CODE)
            End If
            ''End of For Custom Fields


        End If
    End Sub
    Function SaveData(Optional ByVal isPost As Boolean = False) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            If AllowToSave() Then
                trans = clsDBFuncationality.GetTransactin()
                Dim obj As New clsAssemblies()
                obj.CODE = txtCode.Value
                obj.ASSEMBLY_DATE = Me.dtp_AssembleDate.Value
                obj.TRANSACTION_TYPE = Me.cboTransactionType.SelectedValue.ToString
                If Not Me.ddlDisassemblyType.SelectedValue Is Nothing Then
                    obj.DISASSEMBLY_TYPE = Me.ddlDisassemblyType.SelectedValue.ToString
                Else
                    obj.DISASSEMBLY_TYPE = ""
                End If

                obj.ASSEMBLY_CODE = Me.fndAssemblyCode.Value.ToString
                obj.DESCRIPTION = Me.txtDesc.Text
                obj.COMMENTS = Me.txtComment.Text
                obj.Main_Item_Code = Me.fndMainItem.Tag.ToString
                obj.BOM_CODE = Me.fndBomCode.Value.ToString
                obj.COMP_ASSEMBL_METHOD = Me.cboCompoAssMethod.SelectedValue.ToString
                obj.LOCATION_CODE = Me.fndLocation.Value.ToString
                obj.BUILD_QUANTITY = clsCommon.myCdbl(Me.txtBuildQty.Text)
                obj.QUANTITY = clsCommon.myCdbl(Me.txtQuantity.Text)
                obj.DISASSEMBLY_COST = clsCommon.myCdbl(Me.txtDisassCost.Text)
                obj.BUILD_ITEM_UNIT_CODE = lblUnitName.Text
                obj.Comp_Code = objCommonVar.CurrentCompanyCode
                obj.Serial_No = txtSerialNo.Text

                obj.MainItemStatus = ""
                If clsCommon.CompairString(cboTransactionType.Text, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlDisassemblyType.Text, "Other") = CompairStringResult.Equal Then
                    obj.MainItemStatus = "SCRAP"
                End If

                'Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(CODE) from TSPL_PJC_ASSEMBLIES where CODE='" + obj.CODE + "'", trans)
                'If (qry = 0) Then
                '    isNewEntry = True
                'Else
                '    isNewEntry = False
                'End If

                '' saving item details 
                Dim objTr As clsAssembliesItemDetail
                Dim objListItem As New List(Of clsAssembliesItemDetail)

                For Each row As GridViewRowInfo In gvBOM.Rows
                    If clsCommon.myLen(row.Cells(colItemCode).Value) <= 0 Then
                        Continue For
                    End If
                    objTr = New clsAssembliesItemDetail
                    objTr.ASSEMBLY_CODE = Me.txtCode.Value
                    objTr.BOM_CODE = Me.fndBomCode.Value
                    objTr.CONSM_ITEM_CODE = row.Cells(colItemCode).Value
                    objTr.CONSM_ITEM_UNIT_CODE = row.Cells(colUnitCode).Value
                    objTr.CONSM_QUANTITY = row.Cells(colqty).Value
                    objTr.ITEM_DESCRIPTION = row.Cells(colitemDesc).Value
                    objTr.LINE_NO = row.Cells(colLineNo).Value
                    objTr.LOCATION_CODE = row.Cells(colLocationCode).Value

                    objTr.ItemType = ""
                    objTr.ItemStatus = ""
                    If clsCommon.CompairString(cboTransactionType.Text, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlDisassemblyType.Text, "Other") = CompairStringResult.Equal Then
                        objTr.ItemType = row.Cells(colIcodeStatus).Value
                        objTr.ItemStatus = "OLD"
                    End If
                    objTr.Serial_No = row.Cells(colSerialNo).Value
                    objListItem.Add(objTr)

                Next
                obj.objList = objListItem
                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                ''End of For Custom Fields


                If (clsAssemblies.SaveData(obj, isNewEntry, trans)) Then
                    trans.Commit()
                    If isPost = False Then
                        clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                        LoadData(obj.CODE, NavigatorType.Current)

                    End If

                End If
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Private Function AllowToSave() As Boolean
        Try
            'If clsCommon.myLen(clsCommon.myCstr(txtCode.Value)) <= 0 Then
            '    txtCode.Focus()
            '    Throw New Exception("Please Fill  Code")
            'End If
            If AllowFutureDateTransaction(dtp_AssembleDate.Value, Nothing) = False Then
                Return False
            End If
            If clsCommon.myLen(clsCommon.myCstr(cboTransactionType.SelectedValue)) <= 0 Then
                cboTransactionType.Focus()
                Throw New Exception("Please select Transaction Type")
            Else
                If Me.cboTransactionType.SelectedValue = "Disassembly" Then
                    If clsCommon.myLen(clsCommon.myCstr(ddlDisassemblyType.SelectedValue)) <= 0 Then
                        ddlDisassemblyType.Focus()
                        Throw New Exception("Please Select Disassembly Type")
                    Else
                        If ddlDisassemblyType.SelectedValue = "Assembly" Then
                            If clsCommon.myLen(Me.fndAssemblyCode.Value) <= 0 Then
                                fndAssemblyCode.Focus()
                                Throw New Exception("Please Fill Assembly Code")
                            End If

                        End If
                    End If
                End If
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtDesc.Text)) <= 0 Then
                txtDesc.Focus()
                Throw New Exception("Please Fill  Description")
            End If

            If clsCommon.myLen(clsCommon.myCstr(fndMainItem.Value)) <= 0 Then
                fndMainItem.Focus()
                Throw New Exception("Please select Item ")
            End If

            If clsCommon.myLen(clsCommon.myCstr(fndMainItem.Tag.ToString)) <= 0 Then
                fndMainItem.Focus()
                Throw New Exception("Please select Main Item ")
            End If
            If clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Other", False) = CompairStringResult.Equal Then
            Else

                If clsCommon.myLen(clsCommon.myCstr(fndBomCode.Value)) <= 0 Then
                    fndBomCode.Focus()
                    Throw New Exception("Please select Bom ")
                End If
            End If

            If txtSerialNo.Enabled = True And clsCommon.myLen(clsCommon.myCstr(txtSerialNo.Text)) <= 0 Then
                txtSerialNo.Focus()
                Throw New Exception("Please Enter  Serial no for Main Item.")
            End If
            If clsCommon.myLen(clsCommon.myCstr(cboCompoAssMethod.SelectedValue)) <= 0 Then
                cboCompoAssMethod.Focus()
                Throw New Exception("Please select Component Assembly Method ")
            End If

            If clsCommon.myLen(clsCommon.myCstr(fndLocation.Value)) <= 0 Then
                fndLocation.Focus()
                Throw New Exception("Please select Location ")
            Else
                '' check for itemwise location
                For Each dr As GridViewRowInfo In gvBOM.Rows
                    If clsCommon.myLen(dr.Cells(colItemCode).Value) > 0 And clsCommon.myLen(dr.Cells(colLocationCode).Value) <= 0 Then
                        Throw New Exception("Select Loacation for Item Code: " & dr.Cells(colItemCode).Value & "")
                    End If
                    If clsCommon.myLen(dr.Cells(colSerialNo).Value) < 0 Then
                        Continue For
                    End If
                    For rw As Integer = 0 To gvBOM.Rows.IndexOf(dr) - 1
                        If gvBOM.Rows(rw).Cells(colSerialNo).Value = dr.Cells(colSerialNo).Value And clsCommon.myLen(dr.Cells(colSerialNo).Value) > 0 Then
                            Throw New Exception("Serial No : " & dr.Cells(colSerialNo).Value & " duplicated at row no-" & rw + 1 & "")
                        End If
                    Next
                Next

                '' check for available quantity on selected location
                If Me.cboTransactionType.SelectedValue = "Disassembly" Then
                    Dim AvailQty As Double = 0
                    AvailQty = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(Me.fndMainItem.Tag, Me.fndLocation.Value, Me.txtCode.Value, dtp_AssembleDate.Value, Nothing, Me.lblUnitName.Text)
                    If AvailQty < clsCommon.myCdbl(Me.txtQuantity.Text) Then
                        Throw New Exception("Item Code: " & Me.fndMainItem.Tag & "; Required Qty : " & Me.txtQuantity.Text & " ; Available Qty : " & AvailQty & "")
                    End If
                ElseIf Me.cboTransactionType.SelectedValue = "Assembly" Then
                    '' check for available quantites for raw material
                    'Dim dtRM As DataTable
                    'Dim strq As String
                    'strq = "select TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE,TSPL_MF_BOM_DETAIL.CONSM_QUANTITY,TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE " & _
                    '       " from TSPL_MF_BOM_DETAIL inner join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE " & _
                    '       " where TSPL_MF_BOM_DETAIL.BOM_CODE='" & Me.fndBomCode.Value & "' and TSPL_MF_BOM_HEAD.PROD_ITEM_CODE='" & fndMainItem.Value & "' " & _
                    '       " order by TSPL_MF_BOM_DETAIL.LINE_NO"
                    'dtRM = clsDBFuncationality.GetDataTable(strq)
                    For Each dr As GridViewRowInfo In gvBOM.Rows
                        Dim availQty As Double = 0
                        Dim reqQty As Double = 0

                        availQty = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(dr.Cells(colItemCode).Value, dr.Cells(colLocationCode).Value, Me.txtCode.Value, dtp_AssembleDate.Value, Nothing, Me.lblUnitName.Text)
                        reqQty = dr.Cells(colqty).Value ''clsCommon.myCdbl(dr.Cells(colItemCode)) * (clsCommon.myCdbl(Me.txtQuantity.Text) / clsCommon.myCdbl(Me.txtBuildQty.Text))
                        If availQty < reqQty Then
                            Throw New Exception("Item Code: " & dr.Cells(colItemCode).Value & " ; Required Qty : " & reqQty & " ; Available Qty : " & availQty & "")
                        End If
                    Next
                End If

            End If
            If clsCommon.myLen(clsCommon.myCdbl(txtQuantity.Text)) <= 0 Then
                txtQuantity.Focus()
                Throw New Exception("Please fill Quantity ")
            Else
                If clsCommon.myCdbl(txtQuantity.Text) Mod clsCommon.myCdbl(Me.txtBuildQty.Text) > 0 Then
                    txtQuantity.Focus()
                    Throw New Exception("Quantity must be multiple of Build quantity")
                End If
            End If


            '-----------------check for status------------------------------------------------------------------------------
            If clsCommon.CompairString(cboTransactionType.Text, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlDisassemblyType.Text, "Other") = CompairStringResult.Equal Then
                For ii As Integer = 0 To gvBOM.Rows.Count - 1
                    Dim icode As String = ""
                    Dim istatus As String = ""

                    icode = clsCommon.myCstr(gvBOM.Rows(ii).Cells(colItemCode).Value)
                    istatus = clsCommon.myCstr(gvBOM.Rows(ii).Cells(colIcodeStatus).Value)

                    If clsCommon.myLen(icode) > 0 AndAlso clsCommon.myLen(istatus) <= 0 Then
                        Throw New Exception("Please Select Item Status(OK/SCRAP) For Item At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                        Return False
                    End If
                Next
            End If
            '-----------------------------------------------------------------------------------------------------------------

            UcCustomFields1.AllowToSave()

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Private Sub DeleteData()
        Dim trans As SqlTransaction = Nothing
        Try


            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("  Code not found to delete")

            End If
            Dim Reason As String = ""
            If clsCommon.MyMessageBoxShow("Do you want to delete  Code '" + txtCode.Value + "'", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                trans = clsDBFuncationality.GetTransactin
                clsAssemblies.DeleteData(Me.txtCode.Value, trans)
                saveCancelLog(Reason, "Delete", trans)
                '' custom fields
                clsCustomFieldValues.DeleteData(Me.Form_ID, txtCode.Value, trans)
                trans.Commit()
                clsCommon.MyMessageBoxShow("Successfully Deleted", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Code is in use")

            Else
                clsCommon.MyMessageBoxShow(ex.Message)
            End If
            trans.Rollback()
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
    Sub AddNew()
        txtCode.Value = ""
        isNewEntry = True
        gvBOM.Rows.Clear()
        Me.dtp_AssembleDate.Value = clsCommon.GETSERVERDATE()
        txtCode.MyReadOnly = False

        txtDesc.Text = ""
        Me.cboTransactionType.SelectedValue = "Assembly"
        Me.cboCompoAssMethod.SelectedIndex = -1
        Me.ddlDisassemblyType.SelectedIndex = -1
        Me.cboCompoAssMethod.SelectedValue = "0"

        Me.txtDesc.Text = ""
        Me.txtComment.Text = ""
        fndMainItem.Value = Nothing
        Me.fndMainItem.Tag = ""
        lblMainItemDesc.Text = ""
        fndBomCode.Value = Nothing
        lblBomDesc.Text = ""

        Me.fndLocation.Value = Nothing
        Me.lblLocationDesc.Text = ""
        Me.txtBuildQty.Text = ""
        Me.txtDisassCost.Text = ""
        Me.txtQuantity.Text = ""
        Me.lblUnitName.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields

    End Sub

#End Region
#Region "Events"

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click

        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_PJC_ASSEMBLIES where CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select CODE as Code,Description as Name,ASSEMBLY_DATE AS [Date],TRANSACTION_TYPE as [Type],Main_Item_Code as [Main Item Code],BOM_CODE as [BOM Code],LOCATION_CODE as [Location Code] from  TSPL_PJC_ASSEMBLIES"
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_PJC_ASSEMBLIES", qry, "Code", "", txtCode.Value, "", isButtonClicked)
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub
    Private Sub frmAssemblies_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ' globalFunc.mandatoryText(fnddesig.txtValue, txtdes)

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields
        isNewEntry = True
        isLoadTime = True
        LoadGridColumns()
        '' fill transaction type
        cboTransactionType.DataSource = clsAssemblies.GetTransactionTypeTable()
        cboTransactionType.DisplayMember = "Name"
        cboTransactionType.ValueMember = "Code"

        '' fill disassembly type
        ddlDisassemblyType.DataSource = clsAssemblies.GetDisassemblyTypeTable()
        ddlDisassemblyType.DisplayMember = "Name"
        ddlDisassemblyType.ValueMember = "Code"

        '' fill disassembly type
        cboCompoAssMethod.DataSource = clsAssemblies.GetComponentAssemblyMethodTable()
        cboCompoAssMethod.DisplayMember = "Name"
        cboCompoAssMethod.ValueMember = "Code"


        'Me.ddlDisassemblyType.Enabled = False
        Me.fndAssemblyCode.Enabled = False

        isLoadTime = False
        If clsCommon.myLen(Document_No) > 0 Then
            LoadData(Document_No, NavigatorType.Current)
        Else
            AddNew()
        End If




    End Sub
    Sub LoadAssemblyDisassemblyType()
        If clsCommon.CompairString(Me.cboTransactionType.SelectedValue, "Assembly", False) = CompairStringResult.Equal Then
            '' fill assembly type
            ddlDisassemblyType.DataSource = clsAssemblies.GetAssemblyTypeTable()
            ddlDisassemblyType.DisplayMember = "Name"
            ddlDisassemblyType.ValueMember = "Code"
        Else
            '' fill disassembly type
            ddlDisassemblyType.DataSource = clsAssemblies.GetDisassemblyTypeTable()
            ddlDisassemblyType.DisplayMember = "Name"
            ddlDisassemblyType.ValueMember = "Code"
        End If

    End Sub

    Private Sub frmAssemblies_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub
#End Region



    Private Sub cboTransactionType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboTransactionType.SelectedIndexChanged
        Try
            icodestatus.IsVisible = False
            If isLoadTime = False Then
                If Me.cboTransactionType.SelectedValue = "Assembly" Then
                    LoadAssemblyDisassemblyType()
                    'Me.ddlDisassemblyType.SelectedValue = "BOM"
                    'Me.ddlDisassemblyType.Enabled = False
                    'Me.fndAssemblyCode.Enabled = False
                Else
                    LoadAssemblyDisassemblyType()
                    'Me.ddlDisassemblyType.SelectedValue = "BOM"
                    'Me.ddlDisassemblyType.Enabled = True
                    'Me.fndAssemblyCode.Enabled = False

                    If clsCommon.CompairString(ddlDisassemblyType.SelectedValue, "Other") = CompairStringResult.Equal Then
                        icodestatus.IsVisible = True
                    End If
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub ddlDisassemblyType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlDisassemblyType.SelectedIndexChanged
        Try
            icodestatus.IsVisible = False
            If isLoadTime = False Then
                If clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Assembly", False) = CompairStringResult.Equal Then
                    Me.fndAssemblyCode.Enabled = True
                    Me.fndBomCode.Enabled = True
                ElseIf clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Other", False) = CompairStringResult.Equal Then
                    Me.fndAssemblyCode.Enabled = False
                    Me.fndBomCode.Enabled = False
                    Me.fndBomCode.Value = Nothing

                    If clsCommon.CompairString(cboTransactionType.Text, "DisAssembly") = CompairStringResult.Equal Then
                        icodestatus.IsVisible = True
                    End If
                Else
                    Me.fndAssemblyCode.Enabled = False
                    Me.fndBomCode.Enabled = True
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub fndAssemblyCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndAssemblyCode._MYValidating
        Dim qry As String = "select code as [Code],description as [Name] from TSPL_PJC_ASSEMBLIES "
        fndAssemblyCode.Value = clsCommon.ShowSelectForm("TSPL_PJC_ASSEMBLIES", qry, "Code", "", fndAssemblyCode.Value.ToString, "CODE", isButtonClicked)
        lblAssemblyDesc.Text = clsDBFuncationality.getSingleValue("select DESCRIPTION from TSPL_PJC_ASSEMBLIES where CODE='" + fndAssemblyCode.Value + "' ")
    End Sub



    Private Sub fndLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            fndLocation.Value = clsCommon.ShowSelectForm("VendorLocFND", qry, "Code", WhrCls, fndLocation.Value, "Code", isButtonClicked)
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"))
            For Each row As GridViewRowInfo In gvBOM.Rows
                row.Cells(colLocationCode).Value = fndLocation.Value
            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub fndMainItem__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndMainItem._MYValidating
        Try
            'ITEM_TYPE IN ('F','O')
            Dim qry As String = "SELECT ITEM_CODE AS CODE,ITEM_DESC AS [Item Description],ITEM_TYPE AS Type,Is_Serial_Item as [Is Serail Item] FROM TSPL_ITEM_MASTER "
            fndMainItem.Value = clsCommon.ShowSelectForm("TSPL_MF_BOM_HEAD", qry, "Code", "", fndMainItem.Value, "", isButtonClicked)
            Dim objItm As New clsItemMaster
            '' NO CLASS  FOR ITEM MASTER(FINISHED)
            Dim DT_ITEM As DataTable
            Dim STRQ As String
            STRQ = "SELECT ITEM_DESC,ITEM_TYPE,UNIT_CODE,coalesce(Is_Serial_Item,0) as Is_Serial_Item FROM TSPL_ITEM_MASTER WHERE ITEM_CODE='" & fndMainItem.Value & "'"

            DT_ITEM = clsDBFuncationality.GetDataTable(STRQ)
            If DT_ITEM.Rows.Count > 0 Then
                Me.lblMainItemDesc.Text = DT_ITEM.Rows(0).Item("ITEM_DESC")
                Me.lblUnitName.Text = DT_ITEM.Rows(0).Item("UNIT_CODE")
                If DT_ITEM.Rows(0).Item("Is_Serial_Item") = 1 Then
                    Me.txtSerialNo.Enabled = True
                Else
                    Me.txtSerialNo.Enabled = False
                End If
                If clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Other", False) = CompairStringResult.Equal Then
                    LoadGridColumns()
                    gvBOM.Rows.AddNew()
                    Me.fndMainItem.Tag = fndMainItem.Value
                Else
                    DisplayBOM()
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fndBomCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndBomCode._MYValidating
        If clsCommon.myLen(Me.fndMainItem.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Item first.")
            Exit Sub
        End If
        Dim qry As String = "select BOM_CODE as [Code],DESCRIPTION as [Name],PROD_QUANTITY as [Build Qty] from TSPL_MF_BOM_HEAD  "
        Dim WhrClause As String = ""

        If Me.cboTransactionType.SelectedValue = "Assembly" Then
            WhrClause = " PROD_ITEM_CODE IN ('" & fndMainItem.Value & "') AND POSTED=1"
        Else
            WhrClause = " (BOM_CODE in (select BOM_CODE from TSPL_MF_BOM_DETAIL WHERE CONSM_ITEM_CODE='" & Me.fndMainItem.Value & "') or PROD_ITEM_CODE IN ('" & fndMainItem.Value & "')) AND POSTED=1"
        End If
        fndBomCode.Value = clsCommon.ShowSelectForm("TSPL_MF_BOM_HEAD", qry, "Code", WhrClause, fndBomCode.Value.ToString, "BOM_CODE", isButtonClicked)
        Dim strq As String = "select PROD_ITEM_CODE,PROD_QUANTITY,DESCRIPTION from TSPL_MF_BOM_HEAD where BOM_CODE='" + fndBomCode.Value + "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)
        If dt.Rows.Count > 0 Then
            lblBomDesc.Text = dt.Rows(0).Item("DESCRIPTION")
            txtBuildQty.Text = dt.Rows(0).Item("PROD_QUANTITY")
            txtQuantity.Text = dt.Rows(0).Item("PROD_QUANTITY")
            Me.fndMainItem.Tag = dt.Rows(0).Item("PROD_ITEM_CODE")
            LoadBomDetail(fndBomCode.Value, NavigatorType.Current)
        Else
            lblBomDesc.Text = ""
            txtBuildQty.Text = ""
            Me.fndMainItem.Tag = ""
            Me.gvBOM.Rows.Clear()
        End If
    End Sub
    Sub DisplayBOM()
        Dim qry As String = "select BOM_CODE as [Code],DESCRIPTION as [Name],PROD_ITEM_CODE as [Build Item Code],PROD_QUANTITY as [Build Qty] from TSPL_MF_BOM_HEAD  "
        Dim WhrClause As String = ""

        If Me.cboTransactionType.SelectedValue = "Assembly" Then
            WhrClause = " PROD_ITEM_CODE IN ('" & fndMainItem.Value & "') AND POSTED=1"
        Else
            WhrClause = " (BOM_CODE in (select BOM_CODE from TSPL_MF_BOM_DETAIL WHERE CONSM_ITEM_CODE='" & Me.fndMainItem.Value & "') or PROD_ITEM_CODE IN ('" & fndMainItem.Value & "'))AND POSTED=1"
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry & " where " & WhrClause)
        If dt.Rows.Count = 0 Then
            Me.fndBomCode.Value = Nothing
            Me.lblBomDesc.Text = ""
            clsCommon.MyMessageBoxShow("BOM not found for Item '" & Me.fndMainItem.Value & "'")
            Exit Sub
        ElseIf dt.Rows.Count = 1 Then
            Me.fndBomCode.Value = dt.Rows(0).Item("Code")
            Me.lblBomDesc.Text = dt.Rows(0).Item("Name")
            txtBuildQty.Text = dt.Rows(0).Item("Build Qty")
            txtQuantity.Text = dt.Rows(0).Item("Build Qty")
            fndMainItem.Tag = dt.Rows(0).Item("Build Item Code")
            LoadBomDetail(fndBomCode.Value, NavigatorType.Current)
        Else
            fndBomCode.Value = clsCommon.ShowSelectForm("TSPL_MF_BOM_HEAD", qry, "Code", WhrClause, fndBomCode.Value.ToString, "BOM_CODE", False)
            Dim strq As String = "select PROD_QUANTITY,DESCRIPTION,PROD_ITEM_CODE as [Build Item Code] from TSPL_MF_BOM_HEAD where BOM_CODE='" + fndBomCode.Value + "'"

            dt = clsDBFuncationality.GetDataTable(strq)

            If dt.Rows.Count > 0 Then
                lblBomDesc.Text = dt.Rows(0).Item("DESCRIPTION")
                txtBuildQty.Text = dt.Rows(0).Item("PROD_QUANTITY")
                txtQuantity.Text = dt.Rows(0).Item("PROD_QUANTITY")
                fndMainItem.Tag = dt.Rows(0).Item("Build Item Code")
                LoadBomDetail(fndBomCode.Value, NavigatorType.Current)
            Else
                lblBomDesc.Text = ""
                txtBuildQty.Text = ""
                txtQuantity.Text = 0
                fndMainItem.Tag = ""
                Me.gvBOM.Rows.Clear()
            End If
        End If


    End Sub
    Sub LoadBomDetail(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        gvBOM.Rows.Clear()
        Dim obj As clsBillOfMaterial
        obj = clsBillOfMaterial.GetData(strCode, NavTyep)
        If (clsBillOfMaterial.ObjList IsNot Nothing AndAlso clsBillOfMaterial.ObjList.Count > 0) Then
            For Each obj1 As clsBillOfMaterial In clsBillOfMaterial.ObjList
                If clsItemMaster.IsSerializeItem(obj1.CONSM_ITEM_CODE) = True Then
                    For intloop As Integer = 0 To (obj1.CONSM_QUANTITY * Val(Me.txtQuantity.Text)) / Val(Me.txtBuildQty.Text) - 1
                        gvBOM.Rows.AddNew()
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLineNo).Value = gvBOM.CurrentRow.Index + 1
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value = obj1.CONSM_ITEM_CODE
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colitemDesc).Value = obj1.ITEM_DESCRIPTION
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Tag = obj1.CONSM_QUANTITY
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Value = 1
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colUnitCode).Value = obj1.CONSM_ITEM_UNIT_CODE
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLocationCode).Value = Me.fndLocation.Value
                    Next
                Else
                    gvBOM.Rows.AddNew()
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLineNo).Value = gvBOM.CurrentRow.Index + 1
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value = obj1.CONSM_ITEM_CODE
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colitemDesc).Value = obj1.ITEM_DESCRIPTION
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Tag = obj1.CONSM_QUANTITY
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Value = (obj1.CONSM_QUANTITY * Val(Me.txtQuantity.Text)) / Val(Me.txtBuildQty.Text)
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colUnitCode).Value = obj1.CONSM_ITEM_UNIT_CODE
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLocationCode).Value = Me.fndLocation.Value
                End If

            Next
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Dim trans As SqlClient.SqlTransaction = Nothing
        Try

            If (myMessages.postConfirm()) Then
                If (SaveData(True)) Then
                    trans = clsDBFuncationality.GetTransactin

                    clsAssemblies.PostData(txtCode.Value, True, trans)
                    UpdateInventoryMovement(trans)

                    trans.Commit()
                    common.clsCommon.MyMessageBoxShow("Successfully Posted")
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If

            End If

        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Function UpdateInventoryMovement(Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim obj As New clsInventoryMovement
        '' get data
        Dim objData As clsAssemblies = clsAssemblies.GetData(Me.txtCode.Value, NavigatorType.Current, trans)
        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim strq As String = ""
        Dim strItemTypeToSave As String = ""
        Dim strItemType As String
        If objData.TRANSACTION_TYPE = "Assembly" Then
            '' in produced item
            obj = New clsInventoryMovement
            obj.Trans_Type = "Assembly"
            obj.InOut = "I"
            obj.Location_Code = Me.fndLocation.Value
            obj.Item_Code = Me.fndMainItem.Value
            obj.Item_Desc = lblMainItemDesc.Text
            obj.Qty = Me.txtQuantity.Text
            obj.UOM = Me.lblUnitName.Text
            obj.Source_Doc_No = Me.txtCode.Value
            obj.Source_Doc_Date = Me.dtp_AssembleDate.Value

            strItemType = clsItemMaster.GetItemType(Me.fndMainItem.Value, trans)
            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                strItemTypeToSave = "RM"
            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                strItemTypeToSave = "OT"
            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                strItemTypeToSave = "FT"
            ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                strItemTypeToSave = "AT"
            Else
                strItemTypeToSave = strItemType
                'Throw New Exception("Item Type not found: " + strItemType)
            End If
            obj.ItemType = strItemTypeToSave
            obj.Basic_Cost = Me.txtDisassCost.Text
            obj.MRP = Me.txtDisassCost.Text
            obj.Add_Cost = Me.txtDisassCost.Text
            obj.Net_Cost = Me.txtDisassCost.Text
            obj.MFG_Date = dtp_AssembleDate.Value

            ArrInventoryMovement.Add(obj)

            '' out consumed data
            'strq = "select TSPL_PJC_ASSEMBLIES.BOM_CODE,TSPL_PJC_ASSEMBLIES.BUILD_QUANTITY,TSPL_PJC_ASSEMBLIES.QUANTITY,TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE,TSPL_MF_BOM_DETAIL.ITEM_DESCRIPTION," & _
            '       " TSPL_MF_BOM_DETAIL.CONSM_QUANTITY*(TSPL_PJC_ASSEMBLIES.QUANTITY/TSPL_PJC_ASSEMBLIES.BUILD_QUANTITY) AS CONSM_PROD_QUANTITY , " & _
            '       " TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE from TSPL_MF_BOM_DETAIL inner join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE " & _
            '       " LEFT JOIN TSPL_PJC_ASSEMBLIES ON TSPL_PJC_ASSEMBLIES.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE  where TSPL_MF_BOM_DETAIL.BOM_CODE='" & Me.fndBomCode.Value & "' " & _
            '       " and TSPL_MF_BOM_HEAD.PROD_ITEM_CODE='" & Me.fndMainItem.Value & "' and TSPL_PJC_ASSEMBLIES.CODE='" & Me.txtCode.Value & "' order by TSPL_MF_BOM_DETAIL.LINE_NO"
            'Dim dtConsm As DataTable
            'dtConsm = clsDBFuncationality.GetDataTable(strq, trans)
            For Each dr As GridViewRowInfo In gvBOM.Rows
                obj = New clsInventoryMovement
                obj.Trans_Type = "Assembly"
                obj.InOut = "O"
                obj.Location_Code = dr.Cells(colLocationCode).Value
                obj.Item_Code = dr.Cells(colItemCode).Value
                obj.Item_Desc = dr.Cells(colitemDesc).Value.ToString
                obj.Qty = dr.Cells(colqty).Value
                obj.UOM = dr.Cells(colUnitCode).Value
                obj.Source_Doc_No = Me.txtCode.Value
                obj.Source_Doc_Date = Me.dtp_AssembleDate.Value

                obj.itemstatus = ""
                obj.itemtypeinventry = ""


                'strItemType = clsItemMaster.GetItemType(Me.fndMainItem.Value, trans)
                strItemType = clsItemMaster.GetItemType(obj.Item_Code, trans)
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                    strItemTypeToSave = "AT"
                Else
                    strItemTypeToSave = strItemType
                    'Throw New Exception("Item Type not found: " + strItemType)
                End If
                obj.ItemType = strItemTypeToSave
                obj.Basic_Cost = 0
                obj.MRP = 0
                obj.Add_Cost = 0
                obj.Net_Cost = 0
                'obj.MFG_Date = dtp_AssembleDate.Value

                ArrInventoryMovement.Add(obj)

            Next
            clsInventoryMovement.SaveData("Assembly", Me.txtCode.Value, clsCommon.GetPrintDate(Me.dtp_AssembleDate.Value, "dd/MMM/yyyy"), clsCommon.GetPrintDate(Me.dtp_AssembleDate.Value, "dd/MM/yyyy"), ArrInventoryMovement, trans)

            If clsCommon.myLen(txtSerialNo.Text) > 0 Then
                Dim objList As New List(Of clsSerializeInvenotry)
                Dim objsri As New clsSerializeInvenotry
                objsri.Auto_Sr_No = txtSerialNo.Text
                objsri.Document_Code = txtCode.Value
                objsri.Document_Date = dtp_AssembleDate.Value
                objsri.Document_Type = "Assembly"
                objsri.In_Out_Type = "I"
                objsri.Item_Code = fndMainItem.Value
                objsri.Line_No = 1
                objsri.Location_Code = fndLocation.Value
                objsri.Parent_Line_No = 1
                objsri.Tag_No = ""
                objList.Add(objsri)
                clsSerializeInvenotry.SaveData("Assembly", txtCode.Value, dtp_AssembleDate.Value, "I", fndMainItem.Value, fndLocation.Value, 1, objList, trans)
            End If
        Else
            '' in consumed item
            obj = New clsInventoryMovement
            obj.Trans_Type = "Disassembly"
            obj.InOut = "O"
            obj.Location_Code = Me.fndLocation.Value
            obj.Item_Code = Me.fndMainItem.Value
            obj.Item_Desc = lblMainItemDesc.Text
            obj.Qty = Me.txtQuantity.Text
            obj.UOM = Me.lblUnitName.Text
            obj.Source_Doc_No = Me.txtCode.Value
            obj.Source_Doc_Date = Me.dtp_AssembleDate.Value

            strItemType = clsItemMaster.GetItemType(Me.fndMainItem.Value, trans)
            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                strItemTypeToSave = "RM"
            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                strItemTypeToSave = "OT"
            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                strItemTypeToSave = "FT"
            ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                strItemTypeToSave = "AT"
            Else
                strItemTypeToSave = strItemType
                'Throw New Exception("Item Type not found: " + strItemType)
            End If
            obj.ItemType = strItemTypeToSave
            obj.Basic_Cost = 0 'Me.txtDisassCost.Text
            obj.MRP = 0 'Me.txtDisassCost.Text
            obj.Add_Cost = 0 'Me.txtDisassCost.Text
            obj.Net_Cost = 0 'Me.txtDisassCost.Text
            'obj.MFG_Date = dtp_AssembleDate.Value



            ArrInventoryMovement.Add(obj)

            '' in produced data
            'strq = "select TSPL_PJC_ASSEMBLIES.BOM_CODE,TSPL_PJC_ASSEMBLIES.BUILD_QUANTITY,TSPL_PJC_ASSEMBLIES.QUANTITY,TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE,TSPL_MF_BOM_DETAIL.ITEM_DESCRIPTION," & _
            '       " TSPL_MF_BOM_DETAIL.CONSM_QUANTITY*(TSPL_PJC_ASSEMBLIES.QUANTITY/TSPL_PJC_ASSEMBLIES.BUILD_QUANTITY) AS CONSM_PROD_QUANTITY , " & _
            '       " TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE from TSPL_MF_BOM_DETAIL inner join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE " & _
            '       " LEFT JOIN TSPL_PJC_ASSEMBLIES ON TSPL_PJC_ASSEMBLIES.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE  where TSPL_MF_BOM_DETAIL.BOM_CODE='" & Me.fndBomCode.Value & "' " & _
            '       " and TSPL_MF_BOM_HEAD.PROD_ITEM_CODE='" & Me.fndMainItem.Value & "'  and TSPL_PJC_ASSEMBLIES.CODE='" & Me.txtCode.Value & "' order by TSPL_MF_BOM_DETAIL.LINE_NO"
            'Dim dtConsm As DataTable
            'dtConsm = clsDBFuncationality.GetDataTable(strq, trans)
            For Each dr As GridViewRowInfo In gvBOM.Rows
                obj = New clsInventoryMovement
                obj.Trans_Type = "Disassembly"
                obj.InOut = "I"
                obj.Location_Code = dr.Cells(colLocationCode).Value
                obj.Item_Code = dr.Cells(colItemCode).Value
                obj.Item_Desc = dr.Cells(colitemDesc).Value.ToString
                obj.Qty = dr.Cells(colqty).Value
                obj.UOM = dr.Cells(colUnitCode).Value
                obj.Source_Doc_No = Me.txtCode.Value
                obj.Source_Doc_Date = Me.dtp_AssembleDate.Value

                obj.itemstatus = ""
                obj.itemtypeinventry = ""
                If clsCommon.CompairString(cboTransactionType.Text, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlDisassemblyType.Text, "Other") = CompairStringResult.Equal Then
                    obj.itemtypeinventry = clsCommon.myCstr(dr.Cells(colIcodeStatus).Value)
                    obj.itemstatus = "OLD"
                End If

                strItemType = clsItemMaster.GetItemType(obj.Item_Code, trans)
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                    strItemTypeToSave = "AT"
                Else
                    strItemTypeToSave = strItemType
                    'Throw New Exception("Item Type not found: " + strItemType)
                End If
                obj.ItemType = strItemTypeToSave
                obj.Basic_Cost = Me.txtDisassCost.Text
                obj.MRP = Me.txtDisassCost.Text
                obj.Add_Cost = Me.txtDisassCost.Text
                obj.Net_Cost = Me.txtDisassCost.Text
                obj.MFG_Date = dtp_AssembleDate.Value

                If clsCommon.CompairString(obj.itemtypeinventry, "SCRAP") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cboTransactionType.Text, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlDisassemblyType.Text, "Other") = CompairStringResult.Equal Then
                    'ArrInventoryMovement.Add(obj)
                Else
                    ArrInventoryMovement.Add(obj)
                End If

                'ArrInventoryMovement.Add(obj)

            Next
            clsInventoryMovement.SaveData("Disassembly", Me.txtCode.Value, clsCommon.GetPrintDate(Me.dtp_AssembleDate.Value, "dd/MMM/yyyy"), clsCommon.GetPrintDate(Me.dtp_AssembleDate.Value, "dd/MM/yyyy"), ArrInventoryMovement, trans)

        End If

        Return True
    End Function

    Private Sub txtQuantity_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQuantity.TextChanged
        If clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Other", False) = CompairStringResult.Equal Then
        Else
            For Each row As GridViewRowInfo In gvBOM.Rows
                If row.Cells(colqty).Tag IsNot Nothing Then
                    If clsItemMaster.IsSerializeItem(row.Cells(colItemCode).Value) = True Then
                        row.Cells(colqty).Value = 1
                    Else
                        row.Cells(colqty).Value = (row.Cells(colqty).Tag * Val(Me.txtQuantity.Text)) / IIf(Val(Me.txtBuildQty.Text) = 0, 1, Val(Me.txtBuildQty.Text))
                    End If

                End If

            Next
        End If
       
    End Sub

    Private Sub gvBOM_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvBOM.CellEndEdit
        If gvBOM.CurrentRow Is Nothing Then
            Exit Sub
        End If

        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            
            If e.Column Is gvBOM.Columns(colLocationCode) Then
                Dim obj As clsLocation = clsLocation.FinderForPhysicalLoaction(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colLocationCode).Value), False)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                    gvBOM.CurrentRow.Cells(colLocationCode).Value = obj.Code
                    'gvBOM.CurrentRow.Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION
                Else
                    gvBOM.CurrentRow.Cells(colLocationCode).Value = ""
                End If
            End If
            If clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Other", False) = CompairStringResult.Equal Then
                If e.Column Is gvBOM.Columns(colItemCode) Then
                    Dim obj As clsBillOfMaterial = clsBillOfMaterial.FinderForItem(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value), "", False)
                    ''and prod_item_category_code='" & gvBOM.CurrentRow.Cells(colItemCategoryCode).Value & "'
                    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PROD_ITEM_CODE) > 0 Then
                        gvBOM.CurrentRow.Cells(colItemCode).Value = obj.PROD_ITEM_CODE
                        gvBOM.CurrentRow.Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION
                        gvBOM.CurrentRow.Cells(colUnitCode).Value = obj.PROD_ITEM_UNIT_CODE

                    End If
                End If
            End If

            If e.Column Is gvBOM.Columns(colSerialNo) Then
                Dim qry As String = "SELECT Auto_Sr_No as Code,Item_Code as [Item Code],Document_Code as [Document Code], " & _
                " Document_Date as [Document Date],Document_Type as [Document Type]  FROM TSPL_SERIAL_ITEM"

                Dim whrcls As String = " Item_Code='" & gvBOM.CurrentRow.Cells(colItemCode).Value & "' and Auto_Sr_No not in (select Serial_No from TSPL_PJC_ASSEMBLIES_ITEM_DETAIL where ASSEMBLY_CODE<>'" & txtCode.Value & "')"
                gvBOM.CurrentRow.Cells(colSerialNo).Value = clsCommon.ShowSelectForm("SerialNo", qry, "Code", whrcls, gvBOM.CurrentRow.Cells(colSerialNo).Value, "Code", False)

            End If
            isCellValueChangedOpen = False
        End If
    End Sub

    Private Sub gvBOM_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvBOM.CurrentColumnChanged
        If gvBOM.RowCount > 0 Then
            Dim intCurrRow As Integer = gvBOM.CurrentRow.Index
            gvBOM.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Other", False) = CompairStringResult.Equal Then
                If intCurrRow = gvBOM.Rows.Count - 1 Then
                    gvBOM.Rows.AddNew()
                    gvBOM.CurrentRow = gvBOM.Rows(intCurrRow)
                End If
            End If
           
        End If
    End Sub

    Private Sub rdmenufile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenufile.Click

    End Sub

    Private Sub pageGeneral_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pageGeneral.Paint

    End Sub
    '' Created By richa Ticket no BM00000003571 on 22/08/2014  
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtCode.Value = "" Then
            myMessages.blankValue(Me, "Code", Me.Text)
        Else
            funPrint(txtCode.Value)
        End If
    End Sub
    Public Sub funPrint(ByVal StrCode As String)
        Try
            Dim qry As String = ""
            qry = " Select  '" & objCommonVar.CurrentCompanyName & "' as Company_Name , TSPL_PJC_ASSEMBLIES.CODE as AssemblyCode,Convert(varchar,TSPL_PJC_ASSEMBLIES.ASSEMBLY_DATE,103) as Assemblydate ,TSPL_PJC_ASSEMBLIES.Main_Item_Code,"
            qry += " TSPL_PJC_ASSEMBLIES.Serial_No as MainSerialNo,TSPL_PJC_ASSEMBLIES_ITEM_DETAIL.LINE_NO as SL_No,TSPL_PJC_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE as ItemCode, "
            qry += " TSPL_ITEM_MASTER.Item_Desc as ItemDesc ,TSPL_PJC_ASSEMBLIES_ITEM_DETAIL.Serial_No as SerialNo,TSPL_PJC_ASSEMBLIES.Modified_By as ModifiedBy,TSPL_PJC_ASSEMBLIES.Created_By as CreatedBy from TSPL_PJC_ASSEMBLIES_ITEM_DETAIL  Left Outer Join TSPL_PJC_ASSEMBLIES on TSPL_PJC_ASSEMBLIES.CODE=TSPL_PJC_ASSEMBLIES_ITEM_DETAIL.ASSEMBLY_CODE Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PJC_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE"
            qry += " where 2=2"

            If StrCode <> "" Then
                qry += " and  TSPL_PJC_ASSEMBLIES.CODE='" & StrCode & "' "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "rptAssembliesDeassembliesReport", "Assembly Report")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    ''---------Richa code Ends Here----------------
End Class

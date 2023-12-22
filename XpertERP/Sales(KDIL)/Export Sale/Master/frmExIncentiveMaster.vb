Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports System.Text.RegularExpressions
Public Class FrmExIncentiveMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As New ToolTip()
    Dim isNewEntry As Boolean = True
    Dim Errorcontrol As New clsErrorControl()
    Const colILineNo As String = "colILineNo"
    Const colICode As String = "colICode"
    Const colIName As String = "colIName"
    Const colIType As String = "colIType"
    Const colIUnit As String = "colIUnit"
    Const colIPercnt As String = "colIPercnt"
    Const colIRupees As String = "colIRupees"
    Dim isInsideLoadData As Boolean = False
    Dim obj As New clsExIncentiveMasterHead
    Private ObjList As New List(Of clsExIncentiveDetail)
    Private isCellValueChangedOpen As Boolean = False
    Dim OpenFileDialog1 As New OpenFileDialog
#End Region
    
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmExIncentiveMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadIncentiveGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim LineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No."
        LineNo.Name = colILineNo
        LineNo.Width = 100
        LineNo.IsVisible = True
        LineNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(LineNo)

        Dim ItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colICode
        ItemCode.Width = 100
        ItemCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        ItemCode.TextImageRelation = TextImageRelation.TextBeforeImage
        ItemCode.IsVisible = True
        ItemCode.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(ItemCode)

        Dim ItemDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemDesc.FormatString = ""
        ItemDesc.HeaderText = "Item Name"
        ItemDesc.Name = colIName
        ItemDesc.Width = 100
        ItemDesc.IsVisible = True
        ItemDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(ItemDesc)

        Dim ItemType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemType.FormatString = ""
        ItemType.HeaderText = "Item Type"
        ItemType.Name = colIType
        ItemType.Width = 100
        ItemType.IsVisible = True
        ItemType.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(ItemType)

        Dim ItemUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemUnit.FormatString = ""
        ItemUnit.HeaderText = "Unit"
        ItemUnit.Name = colIUnit
        ItemUnit.Width = 100
        ItemUnit.IsVisible = True
        ItemUnit.HeaderImage = Global.ERP.My.Resources.Resources.search4
        ItemUnit.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(ItemUnit)

        Dim Itempercnt As GridViewDecimalColumn = New GridViewDecimalColumn()
        Itempercnt.FormatString = ""
        Itempercnt.HeaderText = "Percent%"
        Itempercnt.Name = colIPercnt
        Itempercnt.Width = 100
        Itempercnt.IsVisible = True
        Itempercnt.ReadOnly = False
        Itempercnt.FormatString = "{0:n4}"
        Itempercnt.DecimalPlaces = 2
        Itempercnt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(Itempercnt)

        Dim ItemRupees As GridViewDecimalColumn = New GridViewDecimalColumn()
        ItemRupees = New GridViewDecimalColumn()
        ItemRupees.FormatString = ""
        ItemRupees.HeaderText = "Amount"
        ItemRupees.Name = colIRupees
        ItemRupees.Width = 100
        ItemRupees.FormatString = "{0:n4}"
        ItemRupees.DecimalPlaces = 2
        Itempercnt.IsVisible = True
        Itempercnt.ReadOnly = False
        ItemRupees.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(ItemRupees)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.Rows.AddNew()
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = False
        gv1.EnableAlternatingRowColor = True
        gv1.AutoSizeRows = False
        gv1.AllowRowResize = True
        gv1.VerticalScrollState = ScrollState.AlwaysShow
        gv1.HorizontalScrollState = ScrollState.AlwaysShow
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.ShowFilteringRow = True
    End Sub

    Private Sub LoadComboBox()
        'frm.qry = "select '' as Code,'None' as Name union all select 'FOB' as Code,'Duty Draw Back-1% on FOB' as Name union all select 'BOF' as Code,'Basic Ocean Freight-25%' as Name union all select 'BOK' as Code,'Basic Ocean Freight per KG' as Name union all select 'VKGUY' as Code,'VisheshKrishi Gram udyogYojana-5% on FOB' as Name"
        Dim xvlue As String = ""

        Dim frm As New FrmCheckBoxGrid()
        frm.qry = "select 'Duty Draw Back-1% on FOB' as Value union all select 'Basic Ocean Freight-25%' as Value union all select 'Basic Ocean Freight per KG' as Value union all select 'VisheshKrishi Gram udyogYojana-5% on FOB' as Value"
        frm.arrValue = New List(Of String)
        While (clsCommon.myLen(cmbType.Text) > 0)
            If Not cmbType.Text.Contains(",") Then
                xvlue = cmbType.Text
                frm.arrValue.Add(xvlue)
                cmbType.Text = ""
            Else
                xvlue = cmbType.Text.Substring(0, cmbType.Text.IndexOf(","))
                frm.arrValue.Add(xvlue)
                If clsCommon.myLen(cmbType.Text) > 0 Then
                    cmbType.Text = cmbType.Text.Replace(xvlue + ",", "")
                End If
            End If

        End While
        frm.ShowDialog()

        txtTypeCode.Text = ""
        cmbType.Text = ""

        If frm.arrValue IsNot Nothing AndAlso frm.arrValue.Count > 0 Then
            For Each Str As String In frm.arrValue
                cmbType.Text = cmbType.Text + "," + Str
                If clsCommon.CompairString(Str, "Duty Draw Back-1% on FOB") = CompairStringResult.Equal Then
                    txtTypeCode.Text = txtTypeCode.Text + ",FOB"
                ElseIf clsCommon.CompairString(Str, "Basic Ocean Freight-25%") = CompairStringResult.Equal Then
                    txtTypeCode.Text = txtTypeCode.Text + ",BOF"
                ElseIf clsCommon.CompairString(Str, "Basic Ocean Freight per KG") = CompairStringResult.Equal Then
                    txtTypeCode.Text = txtTypeCode.Text + ",BOK"
                ElseIf clsCommon.CompairString(Str, "VisheshKrishi Gram udyogYojana-5% on FOB") = CompairStringResult.Equal Then
                    txtTypeCode.Text = txtTypeCode.Text + ",VKGUY"
                End If
            Next
        End If

        Disable_EnableRestFiled()
        If clsCommon.myLen(cmbType.Text) > 0 AndAlso cmbType.Text.Substring(0, 1) = "," Then
            cmbType.Text = cmbType.Text.Substring(1, clsCommon.myLen(cmbType.Text) - 1)
        End If
        If clsCommon.myLen(txtTypeCode.Text) > 0 AndAlso txtTypeCode.Text.Substring(0, 1) = "," Then
            txtTypeCode.Text = txtTypeCode.Text.Substring(1, clsCommon.myLen(txtTypeCode.Text) - 1)
        End If
    End Sub

    Private Sub Disable_EnableRestFiled()
        If Not clsCommon.myCstr(txtTypeCode.Text).Contains("FOB") Then
            txtFOBPers.Enabled = False
            txtFOBPers.Text = Nothing
        ElseIf clsCommon.myCstr(txtTypeCode.Text).Contains("FOB") Then
            txtFOBPers.Enabled = True
        End If
        If Not clsCommon.myCstr(txtTypeCode.Text).Contains("BOF") Then
            txtBOFPers.Enabled = False
            txtBOFPers.Text = Nothing
        ElseIf clsCommon.myCstr(txtTypeCode.Text).Contains("BOF") Then
            txtBOFPers.Enabled = True
        End If
        If Not clsCommon.myCstr(txtTypeCode.Text).Contains("BOK") Then
            gv1.Enabled = False
            gv1.Rows.Clear()
            gv1.Rows.AddNew()
        ElseIf clsCommon.myCstr(txtTypeCode.Text).Contains("BOK") Then
            gv1.Enabled = True
        End If
        If Not clsCommon.myCstr(txtTypeCode.Text).Contains("VKGUY") Then
            txtVKGUPers.Enabled = False
            txtVKGUPers.Text = Nothing
        ElseIf clsCommon.myCstr(txtTypeCode.Text).Contains("VKGUY") Then
            txtVKGUPers.Enabled = True
        End If
    End Sub

    Function AllowToSave() As Boolean
        Try

            If clsCommon.myLen(cmbType.Text) <= 0 Then
                cmbType.Select()
                cmbType.Focus()
                Errorcontrol.SetError(cmbType, "Fill Type")
                Throw New Exception("Select incentive type")
            Else
                Errorcontrol.ResetError(cmbType)
            End If

            If clsCommon.myCstr(txtTypeCode.Text).Contains("FOB") AndAlso clsCommon.myCdbl(txtFOBPers.Text) <= 0 Then
                txtFOBPers.Focus()
                txtFOBPers.Select()
                Errorcontrol.SetError(txtFOBPers, "Fill FOB percentage")
                Throw New Exception("Fill FOB incentive percentage")
            Else
                Errorcontrol.ResetError(txtFOBPers)
            End If

            If clsCommon.myCstr(txtTypeCode.Text).Contains("BOF") AndAlso clsCommon.myCdbl(txtBOFPers.Text) <= 0 Then
                txtBOFPers.Focus()
                txtBOFPers.Select()
                Errorcontrol.SetError(txtBOFPers, "Fill Basic Ocean Freight percentage")
                Throw New Exception("Fill Basic Ocean Freight percentage")
            Else
                Errorcontrol.ResetError(txtBOFPers)
            End If

            If clsCommon.myCstr(txtTypeCode.Text).Contains("VKGUY") AndAlso clsCommon.myCdbl(txtVKGUPers.Text) <= 0 Then
                txtVKGUPers.Focus()
                txtVKGUPers.Select()
                Errorcontrol.SetError(txtVKGUPers, "Fill VisheshKrishi Gram udyogYojana percentage")
                Throw New Exception("Fill VisheshKrishi Gram udyogYojana percentage")
            Else
                Errorcontrol.ResetError(txtVKGUPers)
            End If

            Dim Icode As String = ""
            Dim oldicode As String = ""
            Dim Percent As String = ""
            Dim Amount As String = ""
            Dim arrIcode As New List(Of String)

            arrIcode = New List(Of String)

            If clsCommon.myCstr(txtTypeCode.Text).Contains("BOK") Then
                gv1.Focus()
                gv1.Select()

                For ii As Integer = 0 To gv1.Rows.Count - 1
                    Icode = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                    Percent = clsCommon.myCdbl(gv1.Rows(ii).Cells(colIPercnt).Value)
                    Amount = clsCommon.myCdbl(gv1.Rows(ii).Cells(colIRupees).Value)

                    If clsCommon.myLen(Icode) > 0 AndAlso Not arrIcode.Contains(Icode) Then
                        arrIcode.Add(Icode)
                    End If

                    If clsCommon.myLen(Icode) > 0 Then

                        If Percent <= 0 AndAlso Amount <= 0 Then
                            gv1.CurrentRow = gv1.Rows(ii)
                            gv1.CurrentColumn = gv1.Columns(colIPercnt)
                            Throw New Exception("Fill either percent% or amount at row no. " + clsCommon.myCstr(ii + 1) + "")
                        End If

                        If Percent > 0 AndAlso Amount > 0 Then
                            gv1.CurrentRow = gv1.Rows(ii)
                            gv1.CurrentColumn = gv1.Columns(colIPercnt)
                            Throw New Exception("Fill either percent% or amount at row no. " + clsCommon.myCstr(ii + 1) + "")
                        End If

                        For jj As Integer = ii + 1 To gv1.Rows.Count - 1
                            oldicode = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)

                            If clsCommon.CompairString(Icode, oldicode) = CompairStringResult.Equal Then
                                gv1.CurrentRow = gv1.Rows(jj)
                                gv1.CurrentColumn = gv1.Columns(colICode)
                                Throw New Exception("Duplicate item at row no. " + clsCommon.myCstr(jj + 1) + "")
                            End If
                        Next
                    End If
                Next

                If arrIcode Is Nothing OrElse arrIcode.Count <= 0 Then
                    gv1.Focus()
                    gv1.Select()
                    gv1.CurrentRow = gv1.Rows(0)
                    Throw New Exception("Fill atleast one row in grid.")
                End If
            End If

            

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Public Function SaveData() As Boolean
        Dim obj As New clsExIncentiveMasterHead()
        Dim obj1 As New clsExIncentiveDetail()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmExIncentiveMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return False
                    End If
                End If

                obj = New clsExIncentiveMasterHead()

                obj.Doc_Code = clsCommon.myCstr(fndCode.Value)
                obj.Description = clsCommon.myCstr(txtDescription.Text)
                obj.Doc_Date = clsCommon.myCDate(txtDate.Value)
                obj.Pers = clsCommon.myCdbl(txtFOBPers.Text)
                If obj.Pers > 0 Then
                    obj.Type = "FOB"
                End If


                obj.ObjList = New List(Of clsExIncentiveDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colICode).Value)) > 0 Then
                        obj1 = New clsExIncentiveDetail()

                        obj1.Doc_Code = fndCode.Value
                        obj1.LINE_NO = clsCommon.myCdbl(grow.Cells(colILineNo).Value)
                        obj1.Item_code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        obj1.Unit_Code = clsCommon.myCstr(grow.Cells(colIUnit).Value)
                        obj1.incentive_Per = clsCommon.myCdbl(grow.Cells(colIPercnt).Value)
                        obj1.Incentive_Amount = clsCommon.myCdbl(grow.Cells(colIRupees).Value)
                        obj1.Type = Nothing

                        If clsCommon.myLen(obj1.Item_code) > 0 Then
                            obj.ObjList.Add(obj1)
                        End If
                    End If
                Next

                '=================types===========================
                Dim xSplit() As String = Nothing
                xSplit = txtTypeCode.Text.Split(",")

                If xSplit IsNot Nothing AndAlso xSplit.Count > 0 Then
                    For Each Str As String In xSplit
                        obj1 = New clsExIncentiveDetail()

                        obj1.Doc_Code = fndCode.Value
                        obj1.LINE_NO = 0
                        obj1.Item_code = Nothing
                        obj1.Unit_Code = Nothing
                        obj1.Incentive_Amount = Nothing
                        obj1.Type = Str
                        If clsCommon.CompairString(Str, "FOB") = CompairStringResult.Equal Then
                            obj1.incentive_Per = clsCommon.myCdbl(txtFOBPers.Text)
                        ElseIf clsCommon.CompairString(Str, "BOF") = CompairStringResult.Equal Then
                            obj1.incentive_Per = clsCommon.myCdbl(txtBOFPers.Text)
                        ElseIf clsCommon.CompairString(Str, "VKGUY") = CompairStringResult.Equal Then
                            obj1.incentive_Per = clsCommon.myCdbl(txtVKGUPers.Text)
                        End If

                        If clsCommon.myLen(obj1.Type) > 0 Then
                            obj.ObjList.Add(obj1)
                        End If
                    Next
                End If
                '===============end here=====================================

                If clsExIncentiveMasterHead.SaveData(obj, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)

                    fndCode.Value = obj.Doc_Code
                    fndCode.MyReadOnly = True

                    LoadData(fndCode.Value, NavigatorType.Current)
                End If

                Return False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
            obj1 = Nothing
        End Try

    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As New clsExIncentiveMasterHead()
        Try
            funReset()
            isInsideLoadData = False
            obj = clsExIncentiveMasterHead.GetData(strCode, NavTyep)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
                isInsideLoadData = True
                isNewEntry = False
                btnSave.Text = "Update"
                btnDelete.Enabled = True

                fndCode.Value = obj.Doc_Code
                Me.txtDescription.Text = clsCommon.myCstr(obj.Description)
                Me.txtDate.Value = obj.Doc_Date

                gv1.Rows.Clear()
                If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
                    For Each objTr As clsExIncentiveDetail In obj.ObjList
                        If clsCommon.myLen(objTr.Type) > 0 Then
                            If clsCommon.CompairString(objTr.Type, "FOB") = CompairStringResult.Equal Then
                                txtTypeCode.Text = txtTypeCode.Text + "," + objTr.Type
                                cmbType.Text = cmbType.Text + ",Duty Draw Back-1% on FOB"
                                txtFOBPers.Text = objTr.incentive_Per
                            ElseIf clsCommon.CompairString(objTr.Type, "BOF") = CompairStringResult.Equal Then
                                txtTypeCode.Text = txtTypeCode.Text + "," + objTr.Type
                                cmbType.Text = cmbType.Text + ",Basic Ocean Freight-25%"
                                txtBOFPers.Text = objTr.incentive_Per
                            ElseIf clsCommon.CompairString(objTr.Type, "BOK") = CompairStringResult.Equal Then
                                txtTypeCode.Text = txtTypeCode.Text + "," + objTr.Type
                                cmbType.Text = cmbType.Text + ",Basic Ocean Freight per KG"
                            ElseIf clsCommon.CompairString(objTr.Type, "VKGUY") = CompairStringResult.Equal Then
                                txtTypeCode.Text = txtTypeCode.Text + "," + objTr.Type
                                cmbType.Text = cmbType.Text + ",VisheshKrishi Gram udyogYojana-5% on FOB"
                                txtVKGUPers.Text = objTr.incentive_Per
                            End If
                        Else
                            gv1.Rows.AddNew()

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colILineNo).Value = objTr.LINE_NO
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Des
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIType).Value = objTr.Item_Type
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIUnit).Value = objTr.Unit_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIPercnt).Value = objTr.incentive_Per
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIRupees).Value = objTr.Incentive_Amount
                        End If
                    Next
                End If


                If clsCommon.myLen(cmbType.Text) > 0 AndAlso cmbType.Text.Substring(0, 1) = "," Then
                    cmbType.Text = cmbType.Text.Substring(1, clsCommon.myLen(cmbType.Text) - 1)
                End If
                If clsCommon.myLen(txtTypeCode.Text) > 0 AndAlso txtTypeCode.Text.Substring(0, 1) = "," Then
                    txtTypeCode.Text = txtTypeCode.Text.Substring(1, clsCommon.myLen(txtTypeCode.Text) - 1)
                End If

                Disable_EnableRestFiled()
            Else
                funReset()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            isInsideLoadData = False
        End Try
    End Sub

    Sub OpenitemCodeList(ByVal isButtonClick As Boolean)
        Dim strFCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)

        gv1.CurrentRow.Cells(colICode).Value = clsItemMaster.getFinder(" tspl_item_master.active=1 and isnull(Is_FreshItem,0)<>1", strFCode, isButtonClick)
        gv1.CurrentRow.Cells(colIName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing)
        gv1.CurrentRow.Cells(colIUnit).Value = clsDBFuncationality.getSingleValue("Select Unit_Code from TSPL_ITEM_MASTER where Item_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "' ")
        gv1.CurrentRow.Cells(colIType).Value = clsItemMaster.GetItemType(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing)
    End Sub

    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv_Import As New RadGridView()
        Me.Controls.Add(gv_Import)
        Dim oldNewentry As Boolean = isNewEntry
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        clsCommon.ProgressBarShow()
        If transportSql.importExcel(gv_Import, "Code", "Description", "Date", "Type", "Line_No", "Item_Code", "Unit_Code", "Incentive_Per", "Incentive_Amount") Then
            Try
                Dim qry As String = ""
                Dim check As Integer = 0


                For Each grow As GridViewRowInfo In gv_Import.Rows

                    Dim obj As New clsExIncentiveMasterHead()
                    obj.ObjList = New List(Of clsExIncentiveDetail)

                    obj.Doc_Code = clsCommon.myCstr(grow.Cells("Code").Value)
                    obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                    If clsCommon.myLen(obj.Description) <= 0 Then
                        obj.Description = clsCommon.myCstr(grow.Cells("Type").Value)
                    End If
                    If clsCommon.myLen(obj.Description) > 150 Then
                        obj.Description = obj.Description.Substring(0, 150)
                    End If

                    If grow.Cells("Date").Value Is Nothing OrElse grow.Cells("Date").Value Is DBNull.Value Then
                        obj.Doc_Date = clsCommon.GETSERVERDATE(trans)
                    Else
                        obj.Doc_Date = clsCommon.myCDate(grow.Cells("Date").Value)
                    End If

                    '=========================fill into array list=========================================
                    obj.Type = clsCommon.myCstr(grow.Cells("Type").Value)
                    

                    Dim objtr As New clsExIncentiveDetail()

                    objtr.Item_code = clsCommon.myCstr(grow.Cells("Item_Code").Value)

                    If clsCommon.myLen(objtr.Item_code) <= 0 AndAlso clsCommon.myLen(obj.Type) <= 0 Then
                        Throw New Exception("Fill Type at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If clsCommon.myLen(obj.Type) <= 0 AndAlso clsCommon.myLen(objtr.Item_code) <= 0 Then
                        Throw New Exception("Fill Item Code at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    ElseIf clsCommon.myLen(objtr.Item_code) > 0 Then
                        qry = "select count(*) from tspl_item_master where item_code='" + objtr.Item_code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled Item Code does not exist at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    
                    If clsCommon.myLen(objtr.Item_code) <= 0 AndAlso clsCommon.CompairString(obj.Type, "FOB") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Type, "BOF") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Type, "BOK") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Type, "VKGUY") <> CompairStringResult.Equal Then
                        Throw New Exception("Fill Type [FOB or BOF or BOK or VKGUY] at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    objtr.Type = obj.Type

                    objtr.LINE_NO = CInt(clsCommon.myCdbl(grow.Cells("Line_No").Value))
                    If clsCommon.myLen(objtr.Item_code) > 0 AndAlso objtr.LINE_NO <= 0 Then
                        Throw New Exception("Fill Line No. at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    objtr.Unit_Code = clsCommon.myCstr(grow.Cells("Unit_Code").Value)
                    If clsCommon.myLen(objtr.Item_code) > 0 AndAlso clsCommon.myLen(objtr.Unit_Code) <= 0 Then
                        Throw New Exception("Fill Unit Code at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    ElseIf clsCommon.myLen(objtr.Unit_Code) > 0 Then
                        qry = "select count(*) from tspl_unit_master where unit_code='" + objtr.Unit_Code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled Unit Code does not exist at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If

                        qry = "select count(*) from tspl_item_uom_detail where uom_code='" + objtr.Unit_Code + "' and item_code='" + objtr.Item_code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled Unit Code does not mapped with Item at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    objtr.incentive_Per = clsCommon.myCdbl(grow.Cells("Incentive_Per").Value)
                    objtr.Incentive_Amount = clsCommon.myCdbl(grow.Cells("Incentive_Amount").Value)
                    If clsCommon.myLen(objtr.Item_code) > 0 AndAlso objtr.Incentive_Amount <= 0 AndAlso objtr.incentive_Per <= 0 Then
                        Throw New Exception("Fill either incentive% or incentive amount at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    If clsCommon.myLen(objtr.Item_code) > 0 AndAlso objtr.Incentive_Amount > 0 AndAlso objtr.incentive_Per > 0 Then
                        Throw New Exception("Fill either incentive% or incentive amount at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If clsCommon.myLen(objtr.Type) > 0 AndAlso clsCommon.CompairString(obj.Type, "BOK") <> CompairStringResult.Equal AndAlso objtr.incentive_Per <= 0 Then
                        Throw New Exception("Fill incentive% at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If clsCommon.myLen(objtr.Item_code) > 0 OrElse clsCommon.myLen(objtr.Type) > 0 Then
                        obj.ObjList.Add(objtr)
                    End If

                    qry = "select count(*) from TSPL_Ex_Incentive_Head where doc_code='" + obj.Doc_Code + "'"
                    check = clsDBFuncationality.getSingleValue(qry, trans)
                    isNewEntry = True
                    If check > 0 Then
                        isNewEntry = False
                    End If

                    If clsExIncentiveMasterHead.SaveIMPORTData(obj, isNewEntry, trans) Then

                    End If
                Next

                trans.Commit()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data transfer successfully", Me.Text)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Finally
                clsCommon.ProgressBarHide()
            End Try
        End If

        isNewEntry = oldNewentry
        Me.Controls.Remove(gv_Import)
    End Sub

    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim qry As String = "select count(*) from TSPL_Ex_Incentive_Head"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select TSPL_Ex_Incentive_Head.Doc_Code as [Code],TSPL_Ex_Incentive_Head.Description  as [Description],TSPL_Ex_Incentive_Head.Doc_Date as [Date],TSPL_Ex_Incentive_Detail.Type,TSPL_Ex_Incentive_Detail.Line_No,TSPL_Ex_Incentive_Detail.Item_Code,TSPL_Ex_Incentive_Detail.Unit_Code,TSPL_Ex_Incentive_Detail.Incentive_Per,TSPL_Ex_Incentive_Detail.Incentive_Amount "
            qry += " from TSPL_Ex_Incentive_Head left outer join TSPL_Ex_Incentive_Detail on TSPL_Ex_Incentive_Detail.doc_code=TSPL_Ex_Incentive_Head.doc_code"
        Else
            qry = "select '' as [Code],'' as [Description],'' as [Date],(FOB or BOF or BOK or VKGUY) as Type,1 as Line_No,'' as Item_Code,'' as Unit_Code,0 as Incentive_Per,0 as Incentive_Amount "
        End If

        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub fndCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCode._MYNavigator
        Try
            LoadData(fndCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Shared Function CheckValidCode(ByVal Doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = "select count(*) from TSPL_Ex_Incentive_Head where Doc_Code='" + Doc_No + "'"
        Dim count As Integer = clsDBFuncationality.getSingleValue(qry, trans)
        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub fndCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCode._MYValidating
        Dim check As Boolean = False
        check = clsExIncentiveMasterHead.CheckValidCode(Me.fndCode.Value)

        If check Then
            fndCode.MyReadOnly = True
        Else
            fndCode.MyReadOnly = False
        End If

        If fndCode.MyReadOnly OrElse isButtonClicked Then
            fndCode.Value = clsCommon.myCstr(clsExIncentiveMasterHead.GetFinder("", fndCode.Value, isButtonClicked))
            LoadData(fndCode.Value, NavigatorType.Current)
        Else
            funReset()
        End If
    End Sub

    Sub funReset()
        isNewEntry = True
        txtDate.Text = clsCommon.GETSERVERDATE(Nothing)
        fndCode.Value = ""
        txtDescription.Text = ""
        fndCode.MyReadOnly = False
        cmbType.Text = ""
        txtTypeCode.Text = ""
        txtFOBPers.Text = Nothing
        txtFOBPers.Enabled = False
        txtBOFPers.Text = Nothing
        txtBOFPers.Enabled = False
        txtVKGUPers.Text = Nothing
        txtVKGUPers.Enabled = False

        gv1.Enabled = False
        gv1.Rows.Clear()
        gv1.Rows.AddNew()
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        btnSave.Enabled = True

        txtDescription.Focus()
        txtDescription.Select()
    End Sub

    Sub funDelete()
        Try
            If clsCommon.myLen(fndCode.Value) <= 0 Then
                fndCode.Focus()
                fndCode.Select()
                Errorcontrol.SetError(fndCode, "Code not found to delete.")
                Throw New Exception("Code not found to delete.")
            Else
                Errorcontrol.ResetError(fndCode)
            End If

            If myMessages.deleteConfirm() Then
                If (clsExIncentiveMasterHead.DeleteData(fndCode.Value)) Then
                    myMessages.delete()
                    funReset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        funDelete()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub FrmExIncentiveMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            funDelete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If

        If e.KeyData = Keys.F2 AndAlso gv1.CurrentColumn IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colICode) Then
            isCellValueChangedOpen = True
            OpenitemCodeList(True)
            isCellValueChangedOpen = False
        End If

        If e.KeyData = Keys.F2 AndAlso gv1.CurrentColumn IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colIUnit) Then
            isCellValueChangedOpen = True
            OpenIUOM(True)
            isCellValueChangedOpen = False
        End If
    End Sub

    Private Sub FrmExIncentiveMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadIncentiveGrid()
        funReset()

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for save/update record.")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for close window.")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N for refresh window.")
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    If e.Column Is gv1.Columns(colICode) Then
                        isCellValueChangedOpen = True
                        OpenitemCodeList(False)
                        isCellValueChangedOpen = False
                    End If

                    If e.Column Is gv1.Columns(colIUnit) Then
                        isCellValueChangedOpen = True
                        OpenIUOM(False)
                        isCellValueChangedOpen = False
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub OpenIUOM(ByVal isButtonClicked As Boolean)
        Dim qry As String = "select tspl_item_uom_detail.uom_code as Code,tspl_unit_master.Unit_Desc as Unit from tspl_item_uom_detail left outer join tspl_unit_master on tspl_unit_master.unit_code=tspl_item_uom_detail.uom_code "
        gv1.CurrentRow.Cells(colIUnit).Value = clsCommon.ShowSelectForm("INCFND", qry, "Code", " tspl_item_uom_detail.item_code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colIUnit).Value), "Code", isButtonClicked)
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colILineNo).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub btnNew_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Private Sub cmbType_DoubleClick(sender As Object, e As EventArgs) Handles cmbType.DoubleClick
        LoadComboBox()
    End Sub
End Class

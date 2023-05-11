'--Created By---[Pankaj Kumar Chaudhary]---Against Ticket No--[BM00000000908]-BM00000004877
Imports common
Imports System.Data.SqlClient

Public Class FrmWeightCoversion
    Inherits FrmMainTranScreen

#Region "Variables"
    Const colContainerQty As String = "colQuantity"
    Const colCOntainerUOM As String = "COntainerUOM"
    Const colDesc1 As String = "Description1"
    Const coltemp As String = "TEMP"
    Const colContainedQty As String = "colContainedQty"
    Const colContainedUOM As String = "COntainedUOM"
    Const colDesc2 As String = "Description2"
    Const colProducttype As String = "ProductType"
    Const colItemStructure As String = "colItemStructure"
    Dim dt As DataTable
    Dim qry As String
    Dim IsLoadData As Boolean = False
    Dim ItemStructureMandatory As Boolean = False
#End Region

    Private Sub FrmWeightCoversion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyData = Keys.S AndAlso MyBase.isModifyFlag Then
            btnSave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyData = Keys.C Then
            Me.Close()
        End If

        If e.KeyData = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colCOntainerUOM) Then
            OpenCOntainerUOM(True)
            gv.CurrentRow.Cells(colContainerQty).Value = 1
            gv.CurrentRow.Cells(coltemp).Value = "="
        ElseIf e.KeyData = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colContainedUOM) Then
            OpenCOntainedUOM(True)
        End If
    End Sub

    Private Sub FrmWeightCoversion_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ItemStructureMandatory = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion & "'")) = 0, False, True)
        gv.AllowAddNewRow = False
        LoadData()
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmWeightConversion)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = ""
        repoQty.Name = colContainerQty
        repoQty.ReadOnly = True
        repoQty.Width = 100
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoQty)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "UOM"
        repoICode.Name = colCOntainerUOM
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 130
        gv.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Container UOM Description"
        repoIName.Name = colDesc1
        repoIName.Width = 200
        repoIName.IsVisible = False
        repoIName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIName)

        Dim repoTemp As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTemp.FormatString = ""
        repoTemp.HeaderText = "Equal To"
        repoTemp.Name = coltemp
        repoTemp.Width = 70
        repoTemp.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTemp)

        Dim repoContainedQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoContainedQty.FormatString = "{0:n4}" '"{0:F4}"
        repoContainedQty.DecimalPlaces = 4
        repoContainedQty.HeaderText = ""
        repoContainedQty.Name = colContainedQty
        repoContainedQty.ReadOnly = False
        repoContainedQty.Width = 100
        repoContainedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoContainedQty)


        Dim repoUOM2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOM2.FormatString = ""
        repoUOM2.HeaderText = "Converted UOM"
        repoUOM2.Name = colContainedUOM
        repoUOM2.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoUOM2.TextImageRelation = TextImageRelation.TextBeforeImage
        repoUOM2.Width = 130
        gv.MasterTemplate.Columns.Add(repoUOM2)

        Dim repoDesc2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDesc2.FormatString = ""
        repoDesc2.HeaderText = "Contained UOM Description"
        repoDesc2.Name = colDesc2
        repoDesc2.Width = 200
        repoDesc2.ReadOnly = True
        repoDesc2.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoDesc2)

        Dim repoProductType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoProductType.FormatString = ""
        repoProductType.HeaderText = "Product Type"
        repoProductType.Name = colProducttype
        repoProductType.Width = 100
        repoProductType.DataSource = clsWeightConversionInfo.LoadProductTypeWithALL()
        repoProductType.DisplayMember = "Name"
        repoProductType.ValueMember = "Code"
        repoProductType.ReadOnly = False
        repoProductType.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoProductType)

        Dim repoItemStructure As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemStructure.FormatString = ""
        repoItemStructure.HeaderText = "Item Structure"
        repoItemStructure.Name = colItemStructure
        repoItemStructure.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoItemStructure.TextImageRelation = TextImageRelation.TextBeforeImage
        repoItemStructure.Width = 130
        gv.MasterTemplate.Columns.Add(repoItemStructure)

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40
    End Sub

    Public Sub LoadData()
        Try
            LoadBlankGrid()
            IsLoadData = True
            Dim arr As New List(Of clsWeightConversionInfo)
            arr = clsWeightConversionInfo.GetData(Nothing)
            gv.Rows.AddNew()
            For Each obj As clsWeightConversionInfo In arr
                gv.CurrentRow.Cells(colContainerQty).Value = clsCommon.myCdbl(obj.Container_Qty)
                gv.CurrentRow.Cells(colCOntainerUOM).Value = clsCommon.myCstr(obj.Container_UOM)
                gv.CurrentRow.Cells(colDesc1).Value = clsCommon.myCstr(obj.Container_UOM_Dsec)
                gv.CurrentRow.Cells(coltemp).Value = "="
                gv.CurrentRow.Cells(colContainedQty).Value = clsCommon.myCdbl(obj.Contained_Qty)
                gv.CurrentRow.Cells(colContainedUOM).Value = clsCommon.myCstr(obj.Contained_UOM)
                gv.CurrentRow.Cells(colDesc2).Value = clsCommon.myCstr(obj.Contained_UOM_Desc)
                gv.CurrentRow.Cells(colProducttype).Value = obj.Product_Type
                gv.CurrentRow.Cells(colItemStructure).Value = clsCommon.myCstr(obj.structure_code)
                gv.Rows.AddNew()
            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            IsLoadData = False
        End Try
    End Sub

    Private Sub btnChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Not AllowToSave() Then
                Exit Sub
            End If

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmWeightConversion, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            Dim arr As New List(Of clsWeightConversionInfo)
            Dim obj As clsWeightConversionInfo
            For Each grow As GridViewRowInfo In gv.Rows
                If clsCommon.myLen(grow.Cells(colCOntainerUOM).Value) > 0 Then
                    obj = New clsWeightConversionInfo()
                    obj.Container_Qty = clsCommon.myCdbl(grow.Cells(colContainerQty).Value)
                    obj.Container_UOM = clsCommon.myCstr(grow.Cells(colCOntainerUOM).Value)
                    obj.Contained_Qty = clsCommon.myCdbl(grow.Cells(colContainedQty).Value)
                    obj.Contained_UOM = clsCommon.myCstr(grow.Cells(colContainedUOM).Value)
                    obj.Product_Type = clsCommon.myCstr(grow.Cells(colProducttype).Value)
                    obj.structure_code = clsCommon.myCstr(grow.Cells(colItemStructure).Value)
                    arr.Add(obj)
                End If
            Next
            If clsWeightConversionInfo.SaveData(arr) Then
                clsCommon.MyMessageBoxShow("Data saved successfully.")
            End If
            LoadData()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        For ii As Integer = 0 To gv.Rows.Count - 1
            Dim Containeduom As String = clsCommon.myCstr(gv.Rows(ii).Cells(colContainedUOM).Value)
            Dim Containeruom As String = clsCommon.myCstr(gv.Rows(ii).Cells(colCOntainerUOM).Value)
            Dim product_type As String = clsCommon.myCstr(gv.Rows(ii).Cells(colProducttype).Value)
            Dim Itemstruct As String = clsCommon.myCstr(gv.Rows(ii).Cells(colItemStructure).Value)

            For jj As Integer = ii + 1 To gv.Rows.Count - 1
                Dim oldContaineduom As String = clsCommon.myCstr(gv.Rows(jj).Cells(colContainedUOM).Value)
                Dim oldContaineruom As String = clsCommon.myCstr(gv.Rows(jj).Cells(colCOntainerUOM).Value)
                Dim oldproduct_type As String = clsCommon.myCstr(gv.Rows(jj).Cells(colProducttype).Value)
                Dim oldItemstruct As String = clsCommon.myCstr(gv.Rows(jj).Cells(colItemStructure).Value)
                If ItemStructureMandatory = 0 Then
                    If clsCommon.myLen(Containeduom) > 0 AndAlso clsCommon.myLen(Containeruom) > 0 AndAlso clsCommon.CompairString(Containeduom, oldContaineduom) = CompairStringResult.Equal AndAlso clsCommon.CompairString(Containeruom, oldContaineruom) = CompairStringResult.Equal AndAlso clsCommon.CompairString(product_type, oldproduct_type) = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Duplicate value at row no. " + clsCommon.myCstr(jj + 1) + "", Me.Text)
                        Return False
                    End If
                Else
                    If clsCommon.myLen(Containeduom) > 0 AndAlso clsCommon.myLen(Containeruom) > 0 AndAlso clsCommon.CompairString(Containeduom, oldContaineduom) = CompairStringResult.Equal AndAlso clsCommon.CompairString(Containeruom, oldContaineruom) = CompairStringResult.Equal AndAlso clsCommon.CompairString(Itemstruct, oldItemstruct) = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Duplicate value at row no. " + clsCommon.myCstr(jj + 1) + "", Me.Text)
                        Return False
                    End If
                End If
            Next

            'Sanjay Ticket No  BHA/27/08/18-000483
            If ItemStructureMandatory = True Then
                If clsCommon.myLen(gv.Rows(ii).Cells(colCOntainerUOM).Value) > 0 AndAlso clsCommon.myLen(gv.Rows(ii).Cells(colItemStructure).Value) = 0 Then
                    clsCommon.MyMessageBoxShow("Select Item Structure at row no. " + clsCommon.myCstr(ii + 1) + "", Me.Text)
                    Return False
                End If
            End If
            Dim ItemStructure As String = clsCommon.myCstr(gv.Rows(ii).Cells(colItemStructure).Value)
            For jj As Integer = ii + 1 To gv.Rows.Count - 1
                Dim oldContaineduom As String = clsCommon.myCstr(gv.Rows(jj).Cells(colContainedUOM).Value)
                Dim oldContaineruom As String = clsCommon.myCstr(gv.Rows(jj).Cells(colCOntainerUOM).Value)
                Dim oldItemStructure As String = clsCommon.myCstr(gv.Rows(jj).Cells(colItemStructure).Value)

                If clsCommon.myLen(Containeduom) > 0 AndAlso clsCommon.myLen(Containeruom) > 0 AndAlso clsCommon.CompairString(Containeduom, oldContaineduom) = CompairStringResult.Equal AndAlso clsCommon.CompairString(Containeruom, oldContaineruom) = CompairStringResult.Equal AndAlso clsCommon.CompairString(ItemStructure, oldItemStructure) = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Duplicate value at row no. " + clsCommon.myCstr(jj + 1) + "", Me.Text)
                    Return False
                End If
            Next
            'Sanjay Ticket No  BHA/27/08/18-000483
        Next

        Return True
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not IsLoadData Then
            If e.Column Is gv.Columns(colCOntainerUOM) Then
                OpenCOntainerUOM(False)
                gv.CurrentRow.Cells(colContainerQty).Value = 1
                gv.CurrentRow.Cells(coltemp).Value = "="
            ElseIf e.Column Is gv.Columns(colContainedUOM) Then
                OpenCOntainedUOM(False)
            ElseIf e.Column Is gv.Columns(colItemStructure) Then
                OpenItemStructure(False)
            End If
        End If
    End Sub

    Sub OpenCOntainerUOM(ByVal isButtonClick As Boolean)
        qry = "Select Unit_Code as Code, Unit_Desc as Description from TSPL_UNIT_MASTER"
        gv.CurrentRow.Cells(colCOntainerUOM).Value = clsCommon.ShowSelectForm("SRNItefndnder", qry, "Code", "Weight_Type='Y'", clsCommon.myCstr(gv.CurrentRow.Cells(colCOntainerUOM).Value), "Code", isButtonClick)
        gv.CurrentRow.Cells(colDesc1).Value = clsUOMInfo.GetUnitDesc(gv.CurrentRow.Cells(colCOntainerUOM).Value, Nothing)
    End Sub

    Sub OpenCOntainedUOM(ByVal isButtonClick As Boolean)
        qry = "Select Unit_Code as Code, Unit_Desc as Description from TSPL_UNIT_MASTER"
        gv.CurrentRow.Cells(colContainedUOM).Value = clsCommon.ShowSelectForm("SRNItefndnder", qry, "Code", "Weight_Type='Y' AND Unit_Code<>'" + gv.CurrentRow.Cells(colCOntainerUOM).Value + "'", clsCommon.myCstr(gv.CurrentRow.Cells(colContainedUOM).Value), "Code", isButtonClick)
        gv.CurrentRow.Cells(colDesc2).Value = clsUOMInfo.GetUnitDesc(gv.CurrentRow.Cells(colContainedUOM).Value, Nothing)
    End Sub

    Sub OpenItemStructure(ByVal isButtonClick As Boolean)
        qry = "select 'ALL' as Code,'ALL' as Description"
        qry += " union all select structure_code as Code,Structure_Descq as Description from TSPL_STRUCTURE_MASTER"
        gv.CurrentRow.Cells(colItemStructure).Value = clsCommon.ShowSelectForm("Strufnd", qry, "Code", "", clsCommon.myCstr(gv.CurrentRow.Cells(colItemStructure).Value), "Code", isButtonClick)
    End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.CurrentRow.Cells(colContainerQty).Value = 1
            gv.CurrentRow.Cells(coltemp).Value = "="
            If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colProducttype).Value), "") = CompairStringResult.Equal Then
                gv.CurrentRow.Cells(colProducttype).Value = "ALL"
            End If
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Try
            Dim qry As String
            qry = " select Container_Qty as [Container Qty] ,Container_UOM  as [Container UOM],Convert(varchar, Contained_Qty)as [Contained Qty] , Contained_UOM as [Contained UOM],Structure_Code as [Structure Code] from TSPL_WEIGHT_CONVERSION "
            ListImpExpColumnsMandatory = New List(Of String)({"Container Qty", "Container UOM", "Contained Qty", "Contained UOM"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"Container Qty", "Container UOM", "Contained Qty", "Contained UOM"})
            transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try
       
    End Sub

    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        ' Ticket No : BHA/14/01/19-000778 By Prabhakar 
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim obj As clsWeightConversionInfo = Nothing
        Dim currentdate As Date = Date.Today
        Dim trans As SqlTransaction = Nothing
        If transportSql.importExcel(gv, "Container Qty", "Container UOM", "Contained Qty", "Contained UOM", "Structure Code") Then
            Dim linno As Integer = 1
            Try
                'Dim arr As New List(Of clsWeightConversionInfo)
                clsCommon.ProgressBarShow()
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsWeightConversionInfo
                    linno += 1
                    Dim strContainerQty As String = clsCommon.myCstr(grow.Cells("Container Qty").Value)
                    If clsCommon.CompairString("1", strContainerQty) <> CompairStringResult.Equal Then
                        Throw New Exception("[Container Qty] Should be 1 At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Container_Qty = strContainerQty

                    Dim strContainerUOM As String = clsCommon.myCstr(grow.Cells("Container UOM").Value)
                    If (String.IsNullOrEmpty(strContainerUOM)) = True Then
                        Throw New Exception("[Container UOM] Can't be blank At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    qry = "select Count(*) from TSPL_UNIT_MASTER where Unit_Code = '" + strContainerUOM + "' "
                    Dim isValidContainerUOM As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
                    If isValidContainerUOM = False Then
                        Throw New Exception("Invalid [Container UOM] Code At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    qry = "select Count(*) from TSPL_UNIT_MASTER where Weight_Type='Y' and Unit_Code = '" + strContainerUOM + "' "
                    Dim isWeightTypeUOM As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
                    If isWeightTypeUOM = False Then
                        Throw New Exception("[Container UOM] should be weight Type At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    obj.Container_UOM = strContainerUOM
                    Dim strContainerUOMCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Category from TSPL_UNIT_MASTER where unit_Code = '" + strContainerUOM + "'", trans))
                    Dim strContainedQty As String = clsCommon.myCstr(grow.Cells("Contained Qty").Value)
                    If (String.IsNullOrEmpty(strContainedQty)) = True Then
                        Throw New Exception("[Contained Qty] Can't be blank At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If IsNumeric(strContainedQty) = False Then
                        Throw New Exception("[Contained Qty] Should be numeric At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Contained_Qty = strContainedQty

                    Dim strContainedUOM As String = clsCommon.myCstr(grow.Cells("Contained UOM").Value)
                    If (String.IsNullOrEmpty(strContainedUOM)) = True Then
                        Throw New Exception("[Contained UOM] Can't be blank At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    qry = "select Count(*) from TSPL_UNIT_MASTER where Unit_Code = '" + strContainedUOM + "' "
                    Dim isValidContainedUOM As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
                    If isValidContainedUOM = False Then
                        Throw New Exception("Invalid [Contained UOM] Code At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    qry = "select Count(*) from TSPL_UNIT_MASTER where Weight_Type='Y' and Unit_Code = '" + strContainedUOM + "' "
                    Dim isWeightTypeContainedUOM As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
                    If isWeightTypeContainedUOM = False Then
                        Throw New Exception("[Contained UOM] should be weight Type At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.CompairString(strContainerUOM, strContainedUOM) = CompairStringResult.Equal Then
                        Throw New Exception("[Container UOM] and [Contained UOM] can not be same At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    '=============

                    '"Weight_Type='Y' AND Unit_Code<>
                    '=============
                    obj.Contained_UOM = strContainedUOM
                    Dim strContainedUOMCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Category from TSPL_UNIT_MASTER where unit_Code = '" + strContainedUOM + "'", trans))
                    Dim strStructureCode As String = clsCommon.myCstr(grow.Cells("Structure Code").Value)
                    If clsCommon.CompairString(strContainerUOMCategory, strContainedUOMCategory) = CompairStringResult.Equal Then
                        strStructureCode = "All"
                    End If
                    If clsCommon.CompairString(strStructureCode, "All") <> CompairStringResult.Equal Then


                        If String.IsNullOrEmpty(strStructureCode) = False Then
                            qry = "select Count (*) from TSPL_STRUCTURE_MASTER where Structure_Code = '" + strStructureCode + "'"
                            Dim isValidStructureCode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
                            If isValidStructureCode = False Then
                                Throw New Exception("Invalid Structure Code At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                        Else
                            ' If ItemStructureMandatory = True Then
                            Throw New Exception("Structure Code can't be blank At Line No. " + clsCommon.myCstr(linno) + ".")
                            'End If
                        End If
                    End If
                    obj.structure_code = strStructureCode

                    qry = " select Count (*) from TSPL_WEIGHT_CONVERSION where Container_UOM = '" + obj.Container_UOM + "' and Contained_UOM = '" + obj.Contained_UOM + "'   "
                    qry += " and Structure_Code = '" + obj.structure_code + "' "
                    Dim isContainercontaintdUOMExist As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
                    'If ItemStructureMandatory = True Or clsCommon.myLen(obj.structure_code) > 0 Then

                    'End If
                    Dim isUpdateEntry As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
                    'If isDuplicateEntery = True Then
                    '    Throw New Exception("Container UOM (" + obj.Container_UOM + ") with Contained UOM  (" + obj.Contained_UOM + ") Record Already Exist in Database Or duplicate row exist in Excel sheet  At Line No. " + clsCommon.myCstr(linno) + ".")
                    'End If
                    '===============================================================================
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Container_Qty", obj.Container_Qty)
                    clsCommon.AddColumnsForChange(coll, "Container_UOM", obj.Container_UOM)
                    clsCommon.AddColumnsForChange(coll, "Contained_Qty", obj.Contained_Qty)
                    clsCommon.AddColumnsForChange(coll, "Contained_UOM", obj.Contained_UOM)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "product_type", obj.Product_Type)
                    clsCommon.AddColumnsForChange(coll, "structure_code", obj.structure_code)
                    'If isUpdateEntry = False AndAlso isContainercontaintdUOMExist = True Then
                    '    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WEIGHT_CONVERSION", OMInsertOrUpdate.Update, "TSPL_WEIGHT_CONVERSION.Container_UOM='" + obj.Container_UOM + "' and Contained_UOM = '" + obj.Contained_UOM + "'  ", trans)
                    'ElseIf isUpdateEntry = True AndAlso isContainercontaintdUOMExist = True Then
                    If isContainercontaintdUOMExist = True Then
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WEIGHT_CONVERSION", OMInsertOrUpdate.Update, "TSPL_WEIGHT_CONVERSION.Container_UOM='" + obj.Container_UOM + "' and Contained_UOM = '" + obj.Contained_UOM + "' and structure_code ='" + obj.structure_code + "' ", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WEIGHT_CONVERSION", OMInsertOrUpdate.Insert, "", trans)
                    End If

                    '===============================================================================

                    'If clsCommon.myLen(strcode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_JW_FORMULA  where Code='" + strcode + "' ", trans) > 0 Then
                    '    isNewEntry = False
                    'Else
                    '    isNewEntry = True
                    'End If
                    'arr.Add(obj)

                    'obj.SaveData(arr, Arr2, isNewEntry, trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
End Class


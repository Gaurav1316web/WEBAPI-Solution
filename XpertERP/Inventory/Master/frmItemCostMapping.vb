Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports common
Public Class FrmItemCostMapping
    Inherits FrmMainTranScreen

#Region "Variables"
    Const colLineNo As String = "colLineNo"
    Const colCost_Code As String = "colCost_Code"
    Const colCost_Desc As String = "colCost_Desc"
    Const colCost As String = "colCost"
    Const colUOM As String = "colUOM"

    Const colRatePerHour As String = "colRatePerHour"
    Const colHours As String = "colHours"
    Const colNO As String = "colNO"

    Dim userCode, companyCode As String
    Private isCellValueChanged As Boolean = False
    Dim isImport As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ErrorControl As clsErrorControl = New clsErrorControl()
    Dim isNewEntry As Boolean = True
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim chkPostClick As Boolean = False
    Dim TotalAmount As Double = 0.0
    Dim isIncludeRatePerHoursIn As Boolean = False
#End Region

   
    Private Sub FrmItemCostMapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Ticket No : BHA/03/08/18-000387 By prabhakar for include Rate Per Hours
        isIncludeRatePerHoursIn = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.IncludeRatePerHoursIn & "'")) = 0, False, True)
        Reset()
        ButtonToolTip.SetToolTip(btnAdd, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        gv1.Enabled = True
        gv1.Enabled = True
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
        End If
        btnAdd.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub Reset()
        fndCode.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtStartDate.Value = clsCommon.GETSERVERDATE()
        txtEndDate.Value = clsCommon.GETSERVERDATE()
        txtItemCode.Value = String.Empty
        lblItemCode.Text = String.Empty
        txtUOM.Value = String.Empty
        lblUOM.Text = String.Empty
        txtCostGroup.Value = String.Empty
        lblCostGroup.Text = String.Empty
        fndCode.MyReadOnly = False
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnAdd.Enabled = True
        LoadBlankGrid1()
        LoadBlankGrid2()
        isNewEntry = True
        btnAdd.Text = "Save"
        txtDescription.Text = ""
        lblPending.Status = ERPTransactionStatus.Pending
        txtEndDate.Checked = False
        lblTotalCost.Text = "0.0"
        txtItemCode.MyReadOnly = False
    End Sub
    Sub LoadBlankGrid1()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoGroupCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroupCode.FormatString = ""
        repoGroupCode.HeaderText = "Cost Code"
        repoGroupCode.Name = colCost_Code
        repoGroupCode.ReadOnly = True
        repoGroupCode.Width = 120
        gv1.MasterTemplate.Columns.Add(repoGroupCode)

        Dim repoGroupDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroupDesc.FormatString = ""
        repoGroupDesc.HeaderText = "Cost Description"
        repoGroupDesc.Name = colCost_Desc
        repoGroupDesc.Width = 180
        repoGroupDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoGroupDesc)
        'If isIncludeRatePerHoursIn = True Then
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
            repoGroupCost.ReadOnly = True
        Else
            repoGroupCost.ReadOnly = False
        End If
        repoGroupCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoGroupCost)
        'Else
        '    Dim repoCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        '    repoCost.FormatString = ""
        '    repoCost.HeaderText = "Cost"
        '    repoCost.Name = colCost
        '    repoCost.Width = 100
        '    repoCost.ReadOnly = False
        '    gv1.MasterTemplate.Columns.Add(repoCost)
        'End If


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
        ReStoreGridLayout()
    End Sub
    Sub LoadBlankGrid2()
        gv2.Rows.Clear()
        gv2.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoGroupCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroupCode.FormatString = ""
        repoGroupCode.HeaderText = "Cost Code"
        repoGroupCode.Name = colCost_Code
        repoGroupCode.ReadOnly = True
        repoGroupCode.Width = 120
        gv2.MasterTemplate.Columns.Add(repoGroupCode)

        Dim repoGroupDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroupDesc.FormatString = ""
        repoGroupDesc.HeaderText = "Cost Description"
        repoGroupDesc.Name = colCost_Desc
        repoGroupDesc.Width = 180
        repoGroupDesc.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoGroupDesc)

        Dim repoUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOM.FormatString = ""
        repoUOM.HeaderText = "UOM"
        repoUOM.Name = colUOM
        repoUOM.Width = 100
        repoUOM.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoUOM)

       
        '===========================================================
        Dim repoCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCost.FormatString = ""
        repoCost.HeaderText = "Cost"
        repoCost.Name = colCost
        repoCost.Width = 100
        repoCost.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoCost)
        '=========================================================

        clsCustomFieldGrid.LoadBlankGrid(gv2, MyBase.ArrDetailFields)

        gv2.AllowDeleteRow = True
        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        gv2.TableElement.TableHeaderHeight = 40
        gv2.AutoSizeRows = False
        gv2.AllowRowReorder = True
        ReStoreGridLayout()
    End Sub
    Function AllowToSave() As Boolean
        Try
            'If clsCommon.myLen(fndCode.Value) <= 0 Then
            '    myMessages.blankValue("Document Code")
            '    txtItemCode.Focus()
            '    Return False
            If clsCommon.myLen(txtItemCode.Value) <= 0 Then
                myMessages.blankValue("Item Code")
                txtItemCode.Focus()
                Return False
            
           
            ElseIf clsCommon.myLen(txtUOM.Value) <= 0 Then
                myMessages.blankValue("UOM")
                txtUOM.Focus()
                Return False
            
            ElseIf clsCommon.myLen(txtCostGroup.Value) <= 0 Then
                myMessages.blankValue("Cost Group")
                txtCostGroup.Focus()
                Return False
            End If
            If clsCommon.myLen(txtItemCode.Value) > 0 Then
                Dim chkItem As Double = clsDBFuncationality.getSingleValue("  select Count(*) from TSPL_ITEM_MASTER where item_code = '" + txtItemCode.Value + "'")
                If chkItem <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Invalid Item Code", Me.Text)
                    txtItemCode.Focus()
                    Return False
                End If
            End If
            If isNewEntry = True Then
                If clsCommon.myLen(txtItemCode.Value) > 0 Then
                    Dim chkItem As Double = clsDBFuncationality.getSingleValue("  select Count(*) from TSPL_ITEM_COST_MAPPING_HEADS where item_code = '" + txtItemCode.Value + "'")
                    If chkItem > 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Record Exist against Item Code : '" + txtItemCode.Value + "' ", Me.Text)
                        txtItemCode.Focus()
                        Return False
                    End If
                End If
            End If
           
            If clsCommon.myLen(txtUOM.Value) > 0 Then
                Dim chkItem As Double = clsDBFuncationality.getSingleValue("    select count(*) from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + txtItemCode.Value + "' and UOM_Code = '" + txtUOM.Value + "' ")
                If chkItem <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Invalid UOM", Me.Text)
                    txtItemCode.Focus()
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsItemCostMapping()
                Dim objDetails As New clsItemCostMappingDetails()
                If isNewEntry = True Then
                    obj.HCODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GETSERVERDATE(), clsDocType.ItemCostMapping, "", "") 'clsERPFuncationality.GetNextCode(Nothing, clsCommon.GETSERVERDATE(), clsDocType.ItemWiseTax, clsDocTransactionType.ItemPurchaseTax, "")
                    If clsCommon.myLen(obj.HCODE) <= 0 Then
                        Throw New Exception("Error in code generation")
                    End If
                Else
                    obj.HCODE = fndCode.Value
                End If

                obj.DOC_DATE = txtDate.Value
                obj.Item_Code = txtItemCode.Value
                obj.UOM = txtUOM.Value
                obj.GROUP_CODE = txtCostGroup.Value
                obj.Description = txtDescription.Text
                obj.Start_Date = txtStartDate.Value
                If txtEndDate.Checked = True Then
                    obj.End_Date = txtEndDate.Value
                Else
                    obj.End_Date = Nothing
                End If
                obj.TOTAL_COST = lblTotalCost.Text
                obj.ArrDetails = New List(Of clsItemCostMappingDetails)
                obj.ArrDetailsAll = New List(Of clsItemCostMappingDetailsAll)
                Dim i As Integer = 1
                For ii As Integer = 0 To gv1.RowCount - 1
                    If gv1.Rows.Count > 0 Then
                        Dim objtr As New clsItemCostMappingDetails
                        objtr.HCODE = obj.HCODE
                        objtr.Item_Code = obj.Item_Code
                        objtr.UOM = obj.UOM
                        objtr.SNO = ii + 1
                        objtr.DCODE = clsCommon.myCstr(objtr.HCODE) + "-" + clsCommon.myCstr(objtr.SNO)
                        objtr.COST_CODE = clsCommon.myCstr(gv1.Rows(ii).Cells(colCost_Code).Value)
                        objtr.COST = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCost).Value)
                        objtr.RatePerHour = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRatePerHour).Value)
                        objtr.Hours = clsCommon.myCdbl(gv1.Rows(ii).Cells(colHours).Value)
                        objtr.NO = clsCommon.myCdbl(gv1.Rows(ii).Cells(colNO).Value)
                        ' POST CODE START 
                        If chkPostClick = True Then


                            Dim defaltUOM As String = clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + obj.Item_Code + "' and Stocking_Unit ='Y'")
                            Dim qry = "select UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + obj.Item_Code + "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                            Dim defaltCostAmount As Double = 0
                            If obj.UOM = defaltUOM Then
                                defaltCostAmount = objtr.COST
                            Else
                                Dim defaultconvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + obj.Item_Code + "' and Stocking_Unit ='Y'"))
                                Dim enterConverFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + obj.Item_Code + "' and UOM_Code ='" + obj.UOM + "'"))
                                defaltCostAmount = (objtr.COST * defaultconvFactor) / enterConverFactor
                            End If
                            Dim countAllUom As Integer = dt.Rows.Count
                            'Dim i As Integer = 1
                            For Each dr As DataRow In dt.Rows

                                Dim objDetailsAll As New clsItemCostMappingDetailsAll
                                objDetailsAll.DDCODE = clsCommon.myCstr(objtr.DCODE) + "-" + clsCommon.myCstr(i)
                                objDetailsAll.HCODE = obj.HCODE
                                objDetailsAll.Item_Code = objtr.Item_Code
                                objDetailsAll.UOM = clsCommon.myCstr(dr("UOM_Code"))
                                objDetailsAll.COST_CODE = objtr.COST_CODE
                                objDetailsAll.COST = defaltCostAmount * clsCommon.myCdbl(dr("Conversion_Factor"))
                                objDetailsAll.SNO = i


                                obj.ArrDetailsAll.Add(objDetailsAll)
                                i = i + 1
                            Next
                            ' End Post
                        End If
                        obj.ArrDetails.Add(objtr)

                    End If
                Next
                If obj.SaveData(obj, isNewEntry) Then
                    If chkPostClick = False Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.HCODE, NavigatorType.Current)
                    btnAdd.Text = "Update"
                End If
            Else
                fndCode.MyReadOnly = False
                btnDelete.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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


    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        SaveData()
    End Sub

    Private Sub FrmItemCostMapping_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            btnReset.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnAdd.Enabled Then
            btnAdd.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub



    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.myLen(fndCode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Delete the current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsItemCostMapping.DeleteData(fndCode.Value)
                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            isInsideLoadData = True
            Reset()
            Dim obj As clsItemCostMapping = clsItemCostMapping.GetData(strCode, NavType, Nothing)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.HCODE) > 0 Then
                isNewEntry = False
                btnDelete.Enabled = True
                btnAdd.Enabled = True
                fndCode.Value = obj.HCODE
                txtDate.Value = obj.DOC_DATE
                txtItemCode.Value = obj.Item_Code
                lblItemCode.Text = clsItemMaster.GetItemName(obj.Item_Code, Nothing)
                txtUOM.Value = obj.UOM
                lblUOM.Text = clsDBFuncationality.getSingleValue("select UOM_Description from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + obj.Item_Code + "' and UOM_Code = '" + obj.UOM + "' ")
                txtCostGroup.Value = obj.GROUP_CODE
                lblCostGroup.Text = clsDBFuncationality.getSingleValue(" select Description from TSPL_OVERHEAD_COST_GROUP_HEAD where GROUP_CODE = '" + obj.GROUP_CODE + "'  ")
                lblTotalCost.Text = obj.TOTAL_COST
                txtDescription.Text = obj.Description
                txtStartDate.Value = clsCommon.GetPrintDate(obj.Start_Date, "dd/MM/yyyy")
                If obj.End_Date IsNot Nothing Then
                    txtEndDate.Value = clsCommon.GetPrintDate(obj.End_Date, "dd/MM/yyyy")
                    txtEndDate.Checked = True
                End If

                LoadDetailData(obj.ArrDetails, False)
                'LoadDetailDataAll(obj.ArrDetailsAll, False)
                fndCode.MyReadOnly = True
                txtItemCode.MyReadOnly = True
                btnAdd.Text = "Update"
                lblPending.Status = obj.Status
                If obj.Status = 1 Then
                    btnAdd.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    LoadDetailDataAll(obj.ArrDetailsAll, False)
                Else
                    btnAdd.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                End If
                RadPageView1.SelectedPage = RadPageViewPage1
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub


    Sub LoadDetailData(ByVal Arr As List(Of clsItemCostMappingDetails), ByVal isAddMasterCode As Boolean)
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            For Each objtr As clsItemCostMappingDetails In Arr
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 1)

                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objtr.SNO
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCost_Code).Value = objtr.COST_CODE
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCost_Desc).Value = clsDBFuncationality.getSingleValue("select Description from TSPL_OVERHEAD_COST where COST_CODE ='" + objtr.COST_CODE + "'")
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCost).Value = objtr.COST
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRatePerHour).Value = objtr.RatePerHour
                gv1.Rows(gv1.Rows.Count - 1).Cells(colHours).Value = objtr.Hours
                gv1.Rows(gv1.Rows.Count - 1).Cells(colNO).Value = objtr.NO
            Next
        End If
    End Sub

    Sub LoadGroupCostData(ByVal strGroupCode As String, ByVal tran As SqlTransaction)
        Try

            Dim obj As New clsItemCostMapping()
            obj = clsItemCostMapping.GetGroupData(strGroupCode, Nothing)
            LoadDetailData(obj.ArrDetails, False)
        Catch ex As Exception

            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try

    End Sub

    Sub LoadDetailDataAll(ByVal Arr As List(Of clsItemCostMappingDetailsAll), ByVal isAddMasterCode As Boolean)
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            For Each objtr As clsItemCostMappingDetailsAll In Arr
                gv2.Rows.AddNew()
                gv2.CurrentRow = gv1.Rows(gv1.Rows.Count - 1)
                gv2.Rows(gv2.Rows.Count - 1).Cells(colLineNo).Value = objtr.SNO
                gv2.Rows(gv2.Rows.Count - 1).Cells(colCost_Code).Value = objtr.COST_CODE
                gv2.Rows(gv2.Rows.Count - 1).Cells(colCost_Desc).Value = clsDBFuncationality.getSingleValue("select Description from TSPL_OVERHEAD_COST where COST_CODE ='" + objtr.COST_CODE + "'")
                gv2.Rows(gv2.Rows.Count - 1).Cells(colUOM).Value = objtr.UOM
                gv2.Rows(gv2.Rows.Count - 1).Cells(colCost).Value = objtr.COST

            Next
        End If
    End Sub

    Private Sub fndCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCode._MYValidating
        Dim qry As String = "  select HCODE as Code , DOC_DATE  , Item_Code , UOM , GROUP_CODE , Description , Start_Date , End_Date  , Case when  Status=0 then 'Pending' else 'Approved' end as Status from TSPL_ITEM_COST_MAPPING_HEADS  "
        Dim whrcls As String = ""
        fndCode.Value = clsCommon.ShowSelectForm("FNDDoc", qry, "Code", whrcls, fndCode.Value, "Code", isButtonClicked)
        If clsCommon.myLen(fndCode.Value) > 0 Then
            LoadData(fndCode.Value, NavigatorType.Current)
        Else
            Reset()
        End If
    End Sub

    Private Sub fndCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndCode._MYNavigator
        LoadData(fndCode.Value, NavType)
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            chkPostClick = True
            If clsCommon.myLen(fndCode.Value) <= 0 Then
                Throw New Exception("Code not found to post")
            End If
            If (myMessages.postConfirm()) Then
                SaveData()
                If clsItemCostMapping.PostData(fndCode.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(fndCode.Value, NavigatorType.Current)
                End If

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Info)
        Finally
            chkPostClick = False
        End Try
    End Sub

    Private Sub txtItemCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtItemCode._MYValidating
        If isButtonClicked Then
            txtItemCode.Value = clsItemCostMapping.getItemFinder("", txtItemCode.Value, isButtonClicked)
            lblItemCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc from tspl_item_master where item_code= '" + txtItemCode.Value + "'"))
            txtUOM.Value = ""
            lblUOM.Text = ""
            txtCostGroup.Value = ""
            lblCostGroup.Text = ""
            LoadBlankGrid1()
            LoadBlankGrid2()
            btnAdd.Text = "Save"
            Dim strHCODE As String = clsDBFuncationality.getSingleValue("select * from TSPL_ITEM_COST_MAPPING_HEADS where Item_Code= '" + txtItemCode.Value + "' ")
            If clsCommon.myLen(strHCODE) > 0 Then
                LoadData(strHCODE, NavigatorType.Current)
            End If
        End If
    End Sub


    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Try
            If isButtonClicked Then
                If clsCommon.myLen(txtItemCode.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please select Item Code First.", Me.Text)
                    Return
                End If
                txtUOM.Value = clsItemCostMapping.getUOMFinder("item_code='" + txtItemCode.Value + "'", txtUOM.Value, isButtonClicked)
                lblUOM.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Description from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + txtItemCode.Value + "' and UOM_Code = '" + txtUOM.Value + "'"))
                txtCostGroup.Value = ""
                lblCostGroup.Text = ""
                LoadBlankGrid1()
                LoadBlankGrid2()
            End If
        Catch ex As Exception
            LoadBlankGrid1()
        End Try
    End Sub

    Private Sub txtCostGroup__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCostGroup._MYValidating
        Try
            If isButtonClicked Then
                LoadBlankGrid1()
                LoadBlankGrid2()
                If clsCommon.myLen(txtUOM.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please select UOM First.", Me.Text)
                    Return
                End If
                txtCostGroup.Value = clsItemCostMapping.getGroupFinder("", txtCostGroup.Value, isButtonClicked)
                lblCostGroup.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_OVERHEAD_COST_GROUP_HEAD where GROUP_CODE ='" + txtCostGroup.Value + "'"))
                If clsCommon.myLen(txtCostGroup.Value) > 0 Then
                    LoadGroupCostData(txtCostGroup.Value, Nothing)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        '===========================================================
        If isIncludeRatePerHoursIn = True Then
            If e.Column Is gv1.Columns(colRatePerHour) Then
                gv1.CurrentRow.Cells(colCost).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRatePerHour).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colHours).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colNO).Value)

            End If
            If e.Column Is gv1.Columns(colHours) Then
                gv1.CurrentRow.Cells(colCost).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRatePerHour).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colHours).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colNO).Value)
            End If
            If e.Column Is gv1.Columns(colNO) Then
                gv1.CurrentRow.Cells(colCost).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRatePerHour).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colHours).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colNO).Value)
            End If
        End If
        '=================================================================
        If e.Column Is gv1.Columns(colCost) Or e.Column Is gv1.Columns(colRatePerHour) Or e.Column Is gv1.Columns(colHours) Or e.Column Is gv1.Columns(colNO) Then
            If gv1.RowCount > 0 Then
                TotalAmount = 0
                For ii As Integer = 0 To gv1.RowCount - 1
                    TotalAmount = TotalAmount + clsCommon.myCdbl(gv1.Rows(ii).Cells(colCost).Value)
                Next
                lblTotalCost.Text = TotalAmount
            End If
        End If
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        'Try
        '    Dim query As String = "select TSPL_ITEM_COST_MAPPING_HEADS.HCODE,TSPL_ITEM_COST_MAPPING_HEADS.DOC_DATE ,TSPL_ITEM_COST_MAPPING_HEADS.Item_Code,TSPL_ITEM_COST_MAPPING_HEADS.UOM,TSPL_ITEM_COST_MAPPING_HEADS.GROUP_CODE, TSPL_OVERHEAD_COST_GROUP_HEAD.Description as GROUP_DESC, convert (varchar,TSPL_ITEM_COST_MAPPING_HEADS.Start_Date,103) as Start_Date,convert (varchar,TSPL_ITEM_COST_MAPPING_HEADS.End_Date,103) as End_Date , TSPL_ITEM_COST_MAPPING_DETAIL .COST_CODE ,TSPL_OVERHEAD_COST.Description as COST_DESC, TSPL_ITEM_COST_MAPPING_DETAIL.COST from TSPL_ITEM_COST_MAPPING_DETAIL left outer join TSPL_ITEM_COST_MAPPING_HEADS on TSPL_ITEM_COST_MAPPING_DETAIL.HCODE =TSPL_ITEM_COST_MAPPING_HEADS.HCODE left outer join TSPL_OVERHEAD_COST_GROUP_HEAD on TSPL_OVERHEAD_COST_GROUP_HEAD.GROUP_CODE = TSPL_ITEM_COST_MAPPING_HEADS.GROUP_CODE  left outer join TSPL_OVERHEAD_COST on TSPL_OVERHEAD_COST.COST_CODE =TSPL_ITEM_COST_MAPPING_DETAIL.COST_CODE "
        '    Dim wher As String = " "
        '    transportSql.ExporttoExcel(query, wher, Me)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message, "Item Cost Mapping")
        'End Try

        Try
            Dim query As String = " select *  from ( select TSPL_ITEM_COST_MAPPING_DETAIL.HCODE as DOC_CODE,TSPL_ITEM_COST_MAPPING_HEADS.DOC_DATE,TSPL_ITEM_COST_MAPPING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_COST_MAPPING_HEADS.UOM,TSPL_ITEM_UOM_DETAIL.UOM_Description,TSPL_ITEM_COST_MAPPING_HEADS.GROUP_CODE,TSPL_ITEM_COST_MAPPING_HEADS.Description as GROUP_DESC,convert (varchar,TSPL_ITEM_COST_MAPPING_HEADS.Start_Date,103) as Start_Date, case when TSPL_ITEM_COST_MAPPING_HEADS.End_Date is null then '' else convert (varchar,TSPL_ITEM_COST_MAPPING_HEADS.End_Date ,103) end as End_Date,TSPL_ITEM_COST_MAPPING_HEADS.Description,TSPL_ITEM_COST_MAPPING_DETAIL.COST_CODE,TSPL_OVERHEAD_COST.Description as COST_DESC,TSPL_ITEM_COST_MAPPING_DETAIL.COST, case when  TSPL_ITEM_COST_MAPPING_HEADS.Status =1 then 'Y' else 'N' end as Approved from TSPL_ITEM_COST_MAPPING_DETAIL left outer join TSPL_ITEM_COST_MAPPING_HEADS on TSPL_ITEM_COST_MAPPING_HEADS.HCODE =TSPL_ITEM_COST_MAPPING_DETAIL.HCODE " & _
                                  " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_COST_MAPPING_DETAIL.Item_Code  " & _
                                  " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.UOM_Description = TSPL_ITEM_COST_MAPPING_HEADS.UOM and TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_ITEM_COST_MAPPING_DETAIL.Item_Code " & _
                                  " left outer join TSPL_OVERHEAD_COST_GROUP_HEAD on TSPL_OVERHEAD_COST_GROUP_HEAD.GROUP_CODE = TSPL_ITEM_COST_MAPPING_HEADS.GROUP_CODE " & _
                                  " left outer join TSPL_OVERHEAD_COST on TSPL_OVERHEAD_COST.COST_CODE =TSPL_ITEM_COST_MAPPING_DETAIL.COST_CODE ) as xx  "
            Dim wher As String = " "
            transportSql.ExporttoExcel(query, wher, "xx.DOC_CODE", Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, "Item Cost Mapping")
        End Try

    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Try
            If transportSql.importExcel(dgv, "DOC_CODE", "DOC_DATE", "Item_Code", "Item_Desc", "UOM", "UOM_Description", "GROUP_CODE", "GROUP_DESC", "Start_Date", "End_Date", "Description", "COST_CODE", "COST_DESC", "COST", "Approved") Then
                Dim lineNo As Integer = 0
                Dim DOC_CODE As String = ""
                Dim DOC_DATE As String = ""
                Dim Item_Code As String = ""
                Dim Item_Desc As String = ""
                Dim UOM As String = ""
                Dim UOM_Description As String = ""
                Dim GROUP_CODE As String = ""
                Dim GROUP_DESC As String = ""
                Dim Start_Date As String = ""
                Dim End_Date As String = ""
                Dim Description As String = ""
                Dim COST_CODE As String = ""
                Dim COST_DESC As String = ""
                Dim COST As Double = 0
                Dim Approved As String = ""
                Dim DCODE As String = ""
                Dim DDCODE As String = ""
                For Each grow As GridViewRowInfo In dgv.Rows
                    lineNo = lineNo + 1
                    If clsCommon.myLen(grow.Cells("DOC_CODE").Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Document Code cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "'")
                        trans.Rollback()
                        Exit Sub
                    End If

                    If clsCommon.myLen(grow.Cells("DOC_CODE").Value) > 30 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Document Code Length cannot be greater than 30 at line :'" + clsCommon.myCstr(lineNo) + "'")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If clsCommon.myLen(grow.Cells("DOC_CODE").Value) > 0 Then
                        Dim chkPostDocCode As Double = clsDBFuncationality.getSingleValue("select Count(*) from TSPL_ITEM_COST_MAPPING_HEADS where HCODE='" + clsCommon.myCstr(grow.Cells("DOC_CODE").Value) + "' and status =1 ", trans)
                        If chkPostDocCode > 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Document already Poasted,You Can't update Posted document at line :'" + clsCommon.myCstr(lineNo) + "'")
                            trans.Rollback()
                            Exit Sub
                        End If
                    End If

                Next
                For Each grow As GridViewRowInfo In dgv.Rows
                    lineNo = lineNo + 1
                    If clsCommon.myLen(grow.Cells("DOC_CODE").Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Document Code cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "'")
                        trans.Rollback()
                        Exit Sub
                    End If

                    If clsCommon.myLen(grow.Cells("DOC_CODE").Value) > 30 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Document Code Length cannot be greater than 30 at line :'" + clsCommon.myCstr(lineNo) + "'")
                        trans.Rollback()
                        Exit Sub
                    End If
                    DOC_CODE = clsCommon.myCstr(grow.Cells("DOC_CODE").Value)
                    If clsCommon.myLen(grow.Cells("Item_Code").Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Item Code cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "'")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If clsCommon.myLen(grow.Cells("Item_Code").Value) > 0 Then
                        Dim chkItemCode As Double = clsDBFuncationality.getSingleValue("select count(*) from tspl_item_master where item_code= '" + clsCommon.myCstr(grow.Cells("Item_Code").Value) + "'", trans)
                        If chkItemCode <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, " Invalid Item Code not at line :'" + clsCommon.myCstr(lineNo) + "'")
                            trans.Rollback()
                            Exit Sub
                        End If
                    End If
                    'If clsCommon.myLen(grow.Cells("Item_Code").Value) > 0 Then
                    '    Dim chkItemCode As Double = clsDBFuncationality.getSingleValue("select count (*) from TSPL_ITEM_COST_MAPPING_HEADS where Item_Code = '" + clsCommon.myCstr(grow.Cells("Item_Code").Value) + "'", trans)
                    '    common.clsCommon.MyMessageBoxShow("Duplicate entry not possible, Already Item Code Exist another document  at line :'" + clsCommon.myCstr(lineNo) + "'")
                    '    Exit Sub
                    'End If
                    Item_Code = clsCommon.myCstr(grow.Cells("Item_Code").Value)


                    If clsCommon.myLen(grow.Cells("UOM").Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Item Code cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "'")
                        trans.Rollback()
                        Exit Sub
                    End If

                    If clsCommon.myLen(grow.Cells("UOM").Value) > 0 Then
                        Dim chkUOMCode As Double = clsDBFuncationality.getSingleValue("select count( *)  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + clsCommon.myCstr(grow.Cells("Item_Code").Value) + "' and UOM_Code = '" + clsCommon.myCstr(grow.Cells("UOM").Value) + "' ", trans)
                        If chkUOMCode <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Invalid UOM Code at line :'" + clsCommon.myCstr(lineNo) + "'")
                            trans.Rollback()
                            Exit Sub
                        End If
                    End If
                    UOM = clsCommon.myCstr(grow.Cells("UOM").Value)
                    If clsCommon.myLen(grow.Cells("GROUP_CODE").Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Group Code cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "'")
                        trans.Rollback()
                        Exit Sub
                    End If

                    If clsCommon.myLen(grow.Cells("GROUP_CODE").Value) <= 0 Then
                        Dim chkGroupCode As Double = clsDBFuncationality.getSingleValue("select count (*) from TSPL_OVERHEAD_COST_GROUP_HEAD where GROUP_CODE = '" + clsCommon.myCstr(grow.Cells("GROUP_CODE").Value) + "' ", trans)
                        If chkGroupCode <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Invalid Group Code at line :'" + clsCommon.myCstr(lineNo) + "'")
                            trans.Rollback()
                            Exit Sub
                        End If
                    End If
                    GROUP_CODE = clsCommon.myCstr(grow.Cells("GROUP_CODE").Value)

                    If clsCommon.myLen(grow.Cells("Start_Date").Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Start Date cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "'")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Start_Date = clsCommon.GetPrintDate(grow.Cells("Start_Date").Value, "dd/MMM/yyyy hh:mm tt")

                    If clsCommon.myLen(grow.Cells("End_Date").Value) > 0 Then
                        End_Date = clsCommon.GetPrintDate(grow.Cells("End_Date").Value, "dd/MMM/yyyy hh:mm tt")
                    Else
                        End_Date = Nothing
                    End If


                    If clsCommon.myLen(grow.Cells("COST_CODE").Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Cost Code cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "'")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If clsCommon.myLen(grow.Cells("COST_CODE").Value) > 0 Then
                        Dim chkCostCode As Double = clsDBFuncationality.getSingleValue("select count(*) from TSPL_OVERHEAD_COST where COST_CODE = '" + clsCommon.myCstr(grow.Cells("COST_CODE").Value) + "' ", trans)
                        If chkCostCode <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Invalid Cost Code at line :'" + clsCommon.myCstr(lineNo) + "'")
                            trans.Rollback()
                            Exit Sub
                        End If
                    End If
                    COST_CODE = clsCommon.myCstr(grow.Cells("COST_CODE").Value)
                    If clsCommon.myLen(grow.Cells("COST").Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Cost  cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "'")
                        trans.Rollback()
                        Exit Sub
                    End If
                    COST = clsCommon.myCdbl(grow.Cells("COST").Value)
                    '
                    If clsCommon.myLen(GROUP_CODE) > 0 AndAlso clsCommon.myLen(COST_CODE) > 0 Then
                        Dim chkGroupCodeCostCode As Double = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_OVERHEAD_COST_GROUP_DETAILS where Group_Code = '" + GROUP_CODE + "' and COST_CODE = '" + COST_CODE + "'", trans)
                        If chkGroupCodeCostCode <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Cost Code not mapped under Group Code  at line :'" + clsCommon.myCstr(lineNo) + "'")
                            trans.Rollback()
                            Exit Sub
                        End If
                    End If
                    If clsCommon.myLen(grow.Cells("Approved").Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Approved  cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "'")
                        Exit Sub
                    End If
                    Approved = clsCommon.myCstr(grow.Cells("Approved").Value)
                    If (Approved = "Y" Or Approved = "N") Then
                    Else
                        Throw New Exception("You can use only  'Y' OR 'N'  for Status at line :'" + clsCommon.myCstr(lineNo) + "'")
                    End If
                    If Approved = "Y" Then
                        Approved = 1
                    Else
                        Approved = 0
                    End If
                    Dim CheckDocExist As Double = clsDBFuncationality.getSingleValue("select count(*) from TSPL_ITEM_COST_MAPPING_HEADS  where HCODE = '" + clsCommon.myCstr(grow.Cells("DOC_CODE").Value) + "'", trans)
                    If CheckDocExist > 0 Then
                        Dim chkUOMCodeSameOrDiff As Double = clsDBFuncationality.getSingleValue("select count(*) from TSPL_ITEM_COST_MAPPING_HEADS where HCODE = '" + DOC_CODE + "' and UOM = '" + UOM + "' ", trans)
                        If chkUOMCodeSameOrDiff <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "More then one UOM Code can't use same document at line :'" + clsCommon.myCstr(lineNo) + "'")
                            trans.Rollback()
                            Exit Sub
                        End If

                        Dim chkGroupCodeSameOrDiff As Double = clsDBFuncationality.getSingleValue("select count (*)from TSPL_ITEM_COST_MAPPING_HEADS where HCODE = '" + DOC_CODE + "'  and GROUP_CODE = '" + GROUP_CODE + "' ", trans)
                        If chkGroupCodeSameOrDiff <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "More then one Group Code can't use same document at line :'" + clsCommon.myCstr(lineNo) + "'")
                            trans.Rollback()
                            Exit Sub
                        End If

                        'Dim chkStatusSameOrDiff As Double = clsDBFuncationality.getSingleValue("select count (*)from TSPL_ITEM_COST_MAPPING_HEADS where HCODE = '" + DOC_CODE + "'  and Status = '" + Approved + "' ", trans)
                        'If chkStatusSameOrDiff <= 0 Then
                        '    common.clsCommon.MyMessageBoxShow("Status should be same for For every [Cost Code] in Same Document at line :'" + clsCommon.myCstr(lineNo) + "'")
                        '    trans.Rollback()
                        '    Exit Sub
                        'End If

                        Dim chkItemCodeSameOrDiff As Double = clsDBFuncationality.getSingleValue("select count (*)from TSPL_ITEM_COST_MAPPING_HEADS where HCODE = '" + DOC_CODE + "'  and Item_Code = '" + Item_Code + "' ", trans)
                        If chkItemCodeSameOrDiff <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Item Code should be same for For every [Cost Code] in Same Document at line :'" + clsCommon.myCstr(lineNo) + "'")
                            trans.Rollback()
                            Exit Sub
                        End If
                        Dim qry As String = ""
                        qry = " update TSPL_ITEM_COST_MAPPING_HEADS set Start_Date = '" + Start_Date + "' , End_Date = '" + End_Date + "' , GROUP_CODE = '" + GROUP_CODE + "' , Status='" + Approved + "', Modify_By = '" + objCommonVar.CurrentUserCode + "' , Modify_Date ='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' where HCODE = '" + DOC_CODE + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        'check Mapping details table record exist or not 
                        Dim chkMappingDetailCodeExist As Double = clsDBFuncationality.getSingleValue("select count(*) from TSPL_ITEM_COST_MAPPING_DETAIL  where HCODE = '" + DOC_CODE + "' and Item_Code ='" + Item_Code + "' and UOM = '" + UOM + "'  and Cost_Code = '" + COST_CODE + "' ", trans)
                        If chkMappingDetailCodeExist <= 0 Then
                            'insert  details
                            Dim strSNO As String = clsDBFuncationality.getSingleValue(" select count (*) + 1 from TSPL_ITEM_COST_MAPPING_DETAIL where HCODE = '" + DOC_CODE + "' ", trans)
                            DCODE = DOC_CODE + "-" + strSNO
                            qry = "insert into TSPL_ITEM_COST_MAPPING_DETAIL (DCODE,HCODE,Item_Code,UOM,SNO,COST_CODE,COST) values ('" + DCODE + "','" + DOC_CODE + "','" + Item_Code + "','" + UOM + "','" + strSNO + "','" + COST_CODE + "','" + clsCommon.myCstr(COST) + "')"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            '========================== For Post Start ===========================================================================================================
                            If Approved = 1 Then
                                qry = "select UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + Item_Code + "'"
                                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                For Each dr As DataRow In dt.Rows
                                    Dim strSNONew As String = clsDBFuncationality.getSingleValue(" select count (*) + 1 from TSPL_ITEM_COST_MAPPING_DETAILS_ALL where HCODE = '" + DOC_CODE + "' ", trans)
                                    DDCODE = DOC_CODE + "-" + strSNONew
                                    Dim UOM_Defalt_Cost As Double = UOMConveter(Item_Code, UOM, COST, trans)
                                    Dim UOM_COST As Double = UOM_Defalt_Cost * clsCommon.myCdbl(dr("Conversion_Factor"))
                                    qry = "insert into TSPL_ITEM_COST_MAPPING_DETAILS_ALL (DDCODE,HCODE,Item_Code,UOM,SNO,COST_CODE,COST) values ('" + DDCODE + "','" + DOC_CODE + "','" + Item_Code + "','" + clsCommon.myCstr(dr("UOM_Code")) + "','" + strSNONew + "','" + COST_CODE + "','" + clsCommon.myCstr(UOM_COST) + "')"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                Next
                            End If
                            '=========================== For Post End===========================================================================================================

                        Else
                            ' update
                            qry = "update TSPL_ITEM_COST_MAPPING_DETAIL set COST= '" + clsCommon.myCstr(COST) + "' where  HCODE = '" + DOC_CODE + "' and Item_Code = '" + Item_Code + "' and UOM = '" + UOM + "' and COST_CODE = '" + COST_CODE + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            '========================== For Post Start ===========================================================================================================
                            If Approved = 1 Then
                                qry = "select UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + Item_Code + "'"
                                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                For Each dr As DataRow In dt.Rows
                                    Dim strSNONew As String = clsDBFuncationality.getSingleValue(" select count (*) + 1 from TSPL_ITEM_COST_MAPPING_DETAILS_ALL where HCODE = '" + DOC_CODE + "' ", trans)
                                    DDCODE = DOC_CODE + "-" + strSNONew
                                    Dim UOM_Defalt_Cost As Double = UOMConveter(Item_Code, UOM, COST, trans)
                                    Dim UOM_COST As Double = UOM_Defalt_Cost * clsCommon.myCdbl(dr("Conversion_Factor"))
                                    qry = "insert into TSPL_ITEM_COST_MAPPING_DETAILS_ALL (DDCODE,HCODE,Item_Code,UOM,SNO,COST_CODE,COST) values ('" + DDCODE + "','" + DOC_CODE + "','" + Item_Code + "','" + clsCommon.myCstr(dr("UOM_Code")) + "','" + strSNONew + "','" + COST_CODE + "','" + clsCommon.myCstr(UOM_COST) + "')"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                Next
                            End If
                            '=========================== For Post End===========================================================================================================

                        End If
                        'Update 
                    Else

                        'DOC_CODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GETSERVERDATE(), clsDocType.ItemCostMapping, "", "")
                        'If clsCommon.myLen(DOC_CODE) <= 0 Then
                        '    clsCommon.MyMessageBoxShow("Error in code generation", Me.Text)
                        'End If

                        Dim chkItemCode As Double = clsDBFuncationality.getSingleValue("select count (*) from TSPL_ITEM_COST_MAPPING_HEADS where Item_Code = '" + clsCommon.myCstr(grow.Cells("Item_Code").Value) + "'", trans)
                        If chkItemCode > 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Duplicate entry not possible, Already Item Code Exist another document  at line :'" + clsCommon.myCstr(lineNo) + "'")
                            trans.Rollback()
                            Exit Sub
                        End If

                        Dim qry As String = ""
                        qry = "insert into TSPL_ITEM_COST_MAPPING_HEADS (HCODE,DOC_DATE,Item_Code,UOM,GROUP_CODE,Description,Start_Date,End_Date,Status,Created_By,Created_Date,Modify_By,Modify_Date,Comp_code) values ('" + DOC_CODE + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + Item_Code + "','" + UOM + "','" + GROUP_CODE + "','" + Description + "','" + Start_Date + "','" + End_Date + "','" + Approved + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentCompanyCode + "')"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        Dim strSNO As String = clsDBFuncationality.getSingleValue(" select count (*) + 1 from TSPL_ITEM_COST_MAPPING_DETAIL where HCODE = '" + DOC_CODE + "' ", trans)
                        DCODE = DOC_CODE + "-" + strSNO
                        qry = "insert into TSPL_ITEM_COST_MAPPING_DETAIL (DCODE,HCODE,Item_Code,UOM,SNO,COST_CODE,COST) values ('" + DCODE + "','" + DOC_CODE + "','" + Item_Code + "','" + UOM + "','" + strSNO + "','" + COST_CODE + "','" + clsCommon.myCstr(COST) + "')"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        ' ========================= For Post Start ===========================================================================================================
                        If Approved = 1 Then
                            qry = "select UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + Item_Code + "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            For Each dr As DataRow In dt.Rows
                                Dim strSNONew As String = clsDBFuncationality.getSingleValue(" select count (*) + 1 from TSPL_ITEM_COST_MAPPING_DETAILS_ALL where HCODE = '" + DOC_CODE + "' ", trans)
                                DDCODE = DOC_CODE + "-" + strSNONew
                                Dim UOM_Defalt_Cost As Double = UOMConveter(Item_Code, UOM, COST, trans)
                                Dim UOM_COST As Double = UOM_Defalt_Cost * clsCommon.myCdbl(dr("Conversion_Factor"))
                                qry = "insert into TSPL_ITEM_COST_MAPPING_DETAILS_ALL (DDCODE,HCODE,Item_Code,UOM,SNO,COST_CODE,COST) values ('" + DDCODE + "','" + DOC_CODE + "','" + Item_Code + "','" + clsCommon.myCstr(dr("UOM_Code")) + "','" + strSNONew + "','" + COST_CODE + "','" + clsCommon.myCstr(UOM_COST) + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            Next
                        End If
                        ' ==========================For Post End ===========================================================================================================
                        ' Insert                      


                    End If
                Next
            End If
            trans.Commit()
            common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Function UOMConveter(ByVal strItem As String, ByVal strUOM As String, ByVal strCost As Double, ByVal trans As SqlTransaction) As Double
        Dim defaltUOM As String = clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + strItem + "' and Stocking_Unit ='Y'", trans)
        'Dim qry = "select UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + strItem + "'"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim defaltCostAmount As Double = 0
        If strUOM = defaltUOM Then
            defaltCostAmount = strCost
        Else
            Dim defaultconvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + strItem + "' and Stocking_Unit ='Y'", trans))
            Dim enterConverFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + strItem + "' and UOM_Code ='" + strUOM + "'", trans))
            defaltCostAmount = (strCost * defaultconvFactor) / enterConverFactor
        End If
        Return defaltCostAmount
    End Function

    'Public Function IsPOSTDocument(ByVal strICode As String, ByVal trans As SqlTransaction) As Boolean
    '    Dim qry As String = "select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + strICode + "' "
    '    Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    'End Function

End Class

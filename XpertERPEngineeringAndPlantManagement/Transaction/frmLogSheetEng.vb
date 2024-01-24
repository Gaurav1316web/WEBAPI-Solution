Imports common
Imports System.Data.SqlClient

Public Class frmLogSheetEng
    Inherits FrmMainTranScreen
    'Ticket No-  VIJ/21/10/19-000041 ,Created by- Sanjay
#Region "Variables"
    Public activateSFGProduction As Boolean = False
    Public ShowOnlyProdItemsOnAddRemove As Boolean = False
    Public AutoCalcQtyAddRem As Boolean = False

    Dim OpenAvailorEmptyStckLocationOn_Standardization As Boolean = False
    Public strDocumentCode As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim qry As String = ""
    Dim check As Integer = 0
    Dim isNewEntry As Boolean = Nothing
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim DecimalPointQty As Integer = 3
    Dim DecimalPointFatSNFPer As Integer = 3
    '' Batch Production Columns

    Const colSno As String = "SNO"
    Const colBOM_Code As String = "colBOM_Code"
    Const colBOM_Desc As String = "colBOM_Desc"
    Const colitemcode As String = "colitemcode"
    Const colitemname As String = "itemname"
    Const colitemtype As String = "itemtype"
    Const colitemproducttype As String = "producttype"
    Const coluom As String = "UOM"
    Const colUOMDesc As String = "UOM_Desc"
    Const colQuantity As String = "colQuantity"
    Const colShift_Code As String = "colShift_Code"
    Const colShift_Desc As String = "colShift_Desc"
    Const colSection_Code As String = "colSection_Code"
    Const colSection_Desc As String = "colSection_Desc"

    Const colRequir_FAT_per As String = "colRequir_FAT_per"
    Const colRequir_FAT_KG As String = "colRequir_FAT_KG"
    Const colRequir_SNF_per As String = "colRequir_SNF_per"
    Const colRequir_SNF_KG As String = "colRequir_SNF_KG"
    Const colProduced_FAT_per As String = "colProduced_FAT_per"
    Const colProduced_SNF_per As String = "colProduced_SNF_per"
    Const colProduced_Qty As String = "colProduced_Qty"
    Const colProduced_FAT_KG As String = "colProduced_FAT_KG"
    Const colProduced_SNF_KG As String = "colProduced_SNF_KG"
    Const colSTD_Loaction_Code As String = "colSTD_Loaction_Code"
    Const colSTD_Loaction_Desc As String = "colSTD_Loaction_Desc"

    'Const colNO_SAMPLE_QC As String = "colNO_SAMPLE_QC"
    'Const colDAMAGE_Qty As String = "colDAMAGE_Qty"
    'Const colFINAL_PROD_Qty As String = "colFINAL_PROD_Qty"
    '' Issue detail columns

    Const colIssueSno As String = "colIssueSno"
    Const colIssueItemCode As String = "colIssueItemCode"
    Const colIssueItemName As String = "colIssueItemName"
    Const colIssueItemType As String = "colIssueItemType"
    Const colIssueItemProductType As String = "colIssueItemProductType"
    Const colIssueUom As String = "colIssueUom"
    Const colIssueUOMDesc As String = "colIssueUOMDesc"
    Const colIssued_Qty As String = "colIssued_Qty"

    Const colIssued_FAT_Per As String = "colIssued_FAT_Per"
    Const colIssued_FAT_KG As String = "colIssued_FAT_KG"
    Const colIssued_SNF_Per As String = "colIssued_SNF_Per"
    Const colIssued_SNF_KG As String = "colIssued_SNF_KG"

    'Const colDiff_Qty As String = "colDiff_Qty"
    Const colDiff_FAT_Per As String = "colDiff_FAT_Per"
    Const colDiff_SNF_Per As String = "colDiff_SNF_Per"

    Const colIssueRequir_FAT_Per As String = "colIssueRequir_FAT_Per"
    Const colIssueRequir_SNF_Per As String = "colIssueRequir_SNF_Per"

    Const colDiff_FAT_KG As String = "colDiff_FAT_KG"
    Const colDiff_SNF_KG As String = "colDiff_SNF_KG"
    Const colIssueRemarks As String = "colIssueRemarks"
    Const colProducedItem As String = "colProducedItem"
    Const colTO_LOC_CODE As String = "colTO_LOC_CODE"
    Const colTO_LOC_DESC As String = "colTO_LOC_DESC"
    Const colIssueStatus As String = "colIssueStatus"

    Const colIssue_Fat_Rate As String = "colIssue_Fat_Rate"
    Const colIssue_SNF_Rate As String = "colIssue_SNF_Rate"
    Const colIssue_Fat_Amt As String = "colIssue_Fat_Amt"
    Const colIssue_SNF_Amt As String = "colIssue_SNF_Amt"

    '' add/remove tab columns

    Const colARSno As String = "colARSno"
    Const colARItemCode As String = "colARItemCode"
    Const colARItemName As String = "colARItemName"
    Const colARItemType As String = "colARItemType"
    Const colARItemProductType As String = "colARItemProductType"
    Const colARUom As String = "colARUom"
    Const colARUOMDesc As String = "colARUOMDesc"
    Const colARAvailQty As String = "colARAvailQty"
    Const colARQty As String = "colARQty"
    Const colARType As String = "colARType"
    Const colLoaction_Code As String = "colLoaction_Code"
    Const colLoaction_Desc As String = "colLoaction_Desc"
    Const colARRemarks As String = "colARRemarks"

    Const colAR_FAT_Per As String = "colAR_FAT_Per"
    Const colAR_FAT_KG As String = "colAR_FAT_KG"
    Const colAR_SNF_Per As String = "colAR_SNF_Per"
    Const colAR_SNF_KG As String = "colAR_SNF_KG"
    '' QC tab columns
    Const colQCSno As String = "colQCSno"
    Const colQCType As String = "colQCType" 'Batch or Add/Remove
    Const colQCItemCode As String = "colQCItemCode"
    Const colQCItemName As String = "colQCItemName"
    Const colQCparamcode As String = "paramcode"
    Const colQCparam_desc As String = "param_desc"
    Const colQCparam_type As String = "paramtype"
    Const colQCparam_nature As String = "paramnature"
    Const colQCrange1 As String = "range1"
    Const colQCParentLineNo As String = "colQCParentLineNo"
    'Const colQCrange2 As String = "range2"
    Const colQCBooleanStatus As String = "colQCBooleanStatus"
    Const colQCAlphaValue As String = "colQCAlphaValue"
    Const colActual_Range As String = "colActual_Range"
    Const colActual_Status As String = "colActual_Status"
    Const colActual_Value As String = "colActual_Value"
    Const colQc_Status As String = "colQc_Status"
    Const colQCremarks As String = "colQCremarks"

    '' stage detail tab
    Const colStageSno As String = "colStageSno"
    Const colStage_Code As String = "colStage_Code"
    Const colStage_Desc As String = "colStage_Desc"
    Const colReceived_Qty As String = "colReceived_Qty"
    Const colUnit_Code As String = "colUnit_Code"
    Const colSPUnit_Desc As String = "colSPUnit_Desc"
    Const colLog_Sheet_No As String = "colLog_Sheet_No"
    Const colStatus As String = "colStatus"
    Const colSPRemarks As String = "colSPRemarks"
    Const colSPProdCategory As String = "colSPProdCategory"
    Const colSPSection As String = "colSPSection"
    Const colStageBatch_Code As String = "colStageBatch_Code"
    Public CheckStockServerDate As Boolean = True

    'Public objList As List(Of clsPPStageProcessLogSheetDetail) = New List(Of clsPPStageProcessLogSheetDetail)
    Dim arrLoc As String = Nothing
    Private settAllowNegativeStockInDairyProduction As Boolean = False
    Private SettUseProductFATSNFKgForEstimationCost As Boolean = False
    Dim FATColName As String = ""
    Dim SNFColName As String = ""

#End Region

    Private Sub frmLogSheetEng_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage2
        FunReset()

        ButtonToolTip.SetToolTip(btnsave, "Alt+S for save/update data")
        ButtonToolTip.SetToolTip(btndelete, "Alt+D for deleting data")
        ButtonToolTip.SetToolTip(btnclose, "Alt+C for window close")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub



    Private Sub FunReset()
        txtCode.Value = ""
        dtpDate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtLocation.Value = Nothing
        lblLocation.Text = ""
        txtSection.Value = Nothing
        lblSection.Text = ""
        txtConsumType.Value = Nothing
        lblConsumTypedes.Text = ""
        txtOpEarlier.Text = ""
        txtOp.Text = ""
        txtopTotal.Text = ""
        txtOilChange.Text = ""
        txtRepair.Text = ""
        gvParam.Rows.Clear()
        gvParam.Columns.Clear()
        gvParam.DataSource = Nothing
        'loadBlankParameterGrid()
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnsave.Text = "Save"
        'btnUpdateParameters.Enabled = False
        txtCode.MyReadOnly = False
        txtCode.Focus()
        txtCode.Select()
        isNewEntry = True
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmLogSheetEng)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPrint.Enabled = MyBase.isPrintFlag
    End Sub

    Private Sub frmLogSheetEng_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            If AllowToSave() Then SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            FunReset()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                     "TSPL_ENG_LOG_SHEET_HEAD " + Environment.NewLine + _
                                     "TSPL_ENG_LOG_SHEET_DETAIL ")
        End If

       
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            If AllowToSave() Then
                SaveData(False)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Function AllowToSave(Optional ByVal IsPost As Boolean = False) As Boolean
        Try
            If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
                Return False
            End If

            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                Throw New Exception("Please select location code.")
            End If
            If clsCommon.myLen(txtSection.Value) <= 0 Then
                txtSection.Focus()
                Throw New Exception("Please select Section code.")
            End If
            If clsCommon.myLen(txtConsumType.Value) <= 0 Then
                txtConsumType.Focus()
                Throw New Exception("Please select consumption type Code.")
            End If

            Dim TempParameterExist As Boolean = False
            For i As Integer = 0 To gvParam.Rows.Count - 1
                TempParameterExist = False
                For j As Integer = 1 To gvParam.Columns.Count - 1
                    If clsCommon.myLen(clsCommon.myCstr(gvParam.Rows(i).Cells(j).Value)) > 0 Then
                        TempParameterExist = True
                        Exit For
                    End If
                Next
                If TempParameterExist = False Then
                    Throw New Exception("Please enter at least one parameter value at line no : " & i & " ")
                    Return False
                End If
            Next

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function



    Private Function SaveData(ByVal isPost As Boolean) As Boolean
        Try
            '        updateBatchGridParameter()
            Dim obj As New clsEngLogSheetMaster()
            obj.Doc_No = clsCommon.myCstr(txtCode.Value)
            obj.Doc_Date = clsCommon.myCDate(dtpDate.Text)
            obj.Section_Code = txtSection.Value
            obj.Consumption_Code = clsCommon.myCstr(txtConsumType.Value)
            obj.Location_Code = clsCommon.myCstr(txtLocation.Value)
            obj.OperatedEarlierHrs = clsCommon.myCstr(txtOpEarlier.Text)
            obj.OperatedHrs = clsCommon.myCstr(txtOp.Text)
            obj.OperatedTotalHrs = clsCommon.myCstr(txtopTotal.Text)
            obj.OilChange = clsCommon.myCstr(txtOilChange.Text)
            obj.Repair = clsCommon.myCstr(txtRepair.Text)

            obj.ArrPM = New List(Of clsEngLogSheetDeatil)

            obj.ArrPM = GetParamCollection()


            If clsEngLogSheetMaster.SaveData(obj, isNewEntry) Then
                If isPost = False Then
                    If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                    End If
                End If

                txtCode.Value = obj.Doc_No

                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                Return False
            End If
            obj = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Private Function GetParamCollection() As List(Of clsEngLogSheetDeatil)
        Try
            Dim ArrParamDetail = New List(Of clsEngLogSheetDeatil)
            Dim objParam As clsEngLogSheetDeatil = Nothing

            For i As Integer = 0 To gvParam.Columns.Count - 1
                If clsCommon.CompairString(gvParam.Columns(i).Name, colParamSNO) = CompairStringResult.Equal Then
                Else
                    For jj As Integer = 0 To gvParam.Rows.Count - 1
                        objParam = New clsEngLogSheetDeatil
                        objParam.parameter_code = clsCommon.myCstr(gvParam.Columns(i).Name)
                        'objParam.Param_Field_Desc = clsCommon.myCstr(gvParam.Columns(i).HeaderText)
                        objParam.parameter_Value = clsCommon.myCstr(gvParam.Rows(jj).Cells(i).Value)
                        'objParam.Param_Type = clsCommon.myCstr(gvParam.Columns(i).Tag)
                        objParam.sno = jj + 1
                        ArrParamDetail.Add(objParam)
                    Next
                End If
            Next

            Return ArrParamDetail

        Catch ex As Exception
            myMessages.myExceptions(ex)
            Return New List(Of clsEngLogSheetDeatil)
        End Try

    End Function

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub DeleteData()
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Select()
                txtCode.Focus()
                Throw New Exception("Select Document Code to delete.")
            End If

            qry = "select count(*) from TSPL_ENG_LOG_SHEET_HEAD where Doc_No='" + txtCode.Value + "'"
            check = clsDBFuncationality.getSingleValue(qry, Nothing)

            If check <= 0 Then
                txtCode.Select()
                txtCode.Focus()
                Throw New Exception("Code not found.")
            End If

            If clsEngLogSheetMaster.DeleteData(txtCode.Value) Then
                clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
                FunReset()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub



    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Try
            qry = "select count(*) from TSPL_ENG_LOG_SHEET_HEAD where Doc_No='" + txtCode.Value + "'"
            check = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                txtCode.MyReadOnly = True
            Else
                txtCode.MyReadOnly = False
            End If

            If txtCode.MyReadOnly Or isButtonClicked Then
                Dim qry As String = "select Doc_No,Doc_Date from  TSPL_ENG_LOG_SHEET_HEAD"
                txtCode.Value = clsCommon.ShowSelectForm("TSPL_ENG_LOG_SHEET_HEAD", qry, "Doc_No", "", txtCode.Value, "", isButtonClicked)
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                FunReset()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As New clsEngLogSheetMaster()
            isNewEntry = True
            obj = clsEngLogSheetMaster.GetData(strCode, NavType, Nothing)
            isInsideLoadData = True

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_No) > 0 Then
                isNewEntry = False

                txtCode.Value = obj.Doc_No
                dtpDate.Text = obj.Doc_Date

                txtLocation.Value = obj.Location_Code
                lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
                txtSection.Value = obj.Section_Code
                lblSection.Text = clsEngSectionMaster.GetName(txtSection.Value, Nothing)
                txtConsumType.Value = obj.Consumption_Code
                lblConsumTypedes.Text = clsEngConsumptionTypeMaster.GetName(txtConsumType.Value, Nothing)
                txtOpEarlier.Text = obj.OperatedEarlierHrs
                txtOp.Text = obj.OperatedHrs
                txtopTotal.Text = obj.OperatedTotalHrs
                txtOilChange.Text = obj.OilChange
                txtRepair.Text = obj.Repair

                txtOpEarlier.Text = obj.OperatedEarlierHrs
                txtOp.Text = obj.OperatedHrs
                txtopTotal.Text = obj.OperatedTotalHrs
                txtOilChange.Text = obj.OilChange
                txtRepair.Text = obj.Repair

                '            If obj.Posted = "1" Then
                '                UsLock1.Status = ERPTransactionStatus.Approved
                '                btnCancel.Enabled = True
                '            Else
                '                UsLock1.Status = ERPTransactionStatus.Pending
                '                btnCancel.Enabled = False
                '            End If



                loadParameterGrid()
                '            For Each objTr As clsPPStdFinalQCDetail In obj.ArrDetail
                '                gvParam.Rows.AddNew()
                '                gvParam.Rows(gvParam.Rows.Count - 1).Cells(colParamSNO).Value = objTr.Line_No
                '                gvParam.Rows(gvParam.Rows.Count - 1).Cells(colParamItemCode).Value = objTr.Item_Code
                '                gvParam.Rows(gvParam.Rows.Count - 1).Cells(colParamItemName).Value = objTr.Item_Name
                '                gvParam.Rows(gvParam.Rows.Count - 1).Cells(colParamFrom).Value = objTr.QC_From
                '                gvParam.Rows(gvParam.Rows.Count - 1).Cells(colParamParentID).Value = objTr.Parent_ID
                '            Next
                'Add rows in grid
                Dim qry As String = "select count(distinct SNO) FROM TSPL_ENG_LOG_SHEET_DETAIL where Doc_No='" + txtCode.Value + "'"
                Dim IntRowCount As Integer = clsDBFuncationality.getSingleValue(qry)
                For i As Integer = 0 To IntRowCount - 1
                    gvParam.Rows.AddNew()
                    gvParam.Rows(i).Cells(0).Value = i + 1
                Next

                For Each objTr As clsEngLogSheetDeatil In obj.ArrPM
                    Try
                        gvParam.Rows(objTr.sno - 1).Cells(objTr.parameter_code).Value = objTr.parameter_Value
                    Catch ex As Exception
                    End Try
                Next
                

                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                '            btnPost.Enabled = True
                txtCode.MyReadOnly = True
              
                '            If obj.Posted = "1" Then
                '                UsLock1.Status = ERPTransactionStatus.Approved
                '                btnsave.Enabled = False
                '                btndelete.Enabled = False
                '                btnPost.Enabled = False
                '                SetReadOnlyParameters()
                '                btnUpdateParameters.Enabled = True
                '            End If
            Else
                FunReset()
            End If
            '        FillSection()
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        isInsideLoadData = False
    End Sub

    Const colParamSNO As String = "colParamSNO"
    'Const colParamFrom As String = "colParamFrom"
    'Const colParamItemCode As String = "colParamItemCode"
    'Const colParamItemName As String = "colParamItemName"
    'Const colParamParentID As String = "colParamParentID"
    Sub loadParameterGrid()
        Try
            Dim whrCls As String = String.Empty
            whrCls = ""

            Dim pFields As Boolean = True
            Dim gridWidth As Integer = 60

            Dim qry As String = "select TSPL_ENG_SECTION_PARAMETER_MAPPING.Parameter_Code as Code,TSPL_ENG_PARAMETER_MASTER.Description from TSPL_ENG_SECTION_PARAMETER_MAPPING left join TSPL_ENG_PARAMETER_MASTER on TSPL_ENG_PARAMETER_MASTER.Code=TSPL_ENG_SECTION_PARAMETER_MAPPING.Parameter_Code where TSPL_ENG_SECTION_PARAMETER_MAPPING.Section_Code='" + txtSection.Value + "'  and TSPL_ENG_SECTION_PARAMETER_MAPPING.Consumption_Code='" + txtConsumType.Value + "' order by TSPL_ENG_SECTION_PARAMETER_MAPPING.Parameter_Seq"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
                pFields = True
            Else
                pFields = False
                clsCommon.MyMessageBoxShow(Me, "Please map parameter first.", Me.Text)
                Exit Sub
            End If
            gvParam.Rows.Clear()
            gvParam.Columns.Clear()
            gvParam.DataSource = Nothing
            'Dim repoComboColumn As GridViewComboBoxColumn
            Dim repoTextColumn As GridViewTextBoxColumn
            Dim repoDecimalColumn As GridViewDecimalColumn = Nothing

            repoDecimalColumn = New GridViewDecimalColumn()
            repoDecimalColumn.Name = colParamSNO
            repoDecimalColumn.Width = 50
            repoDecimalColumn.FormatString = "{0:n0}"
            repoDecimalColumn.DecimalPlaces = 0
            repoDecimalColumn.HeaderText = "SNo"
            repoDecimalColumn.Tag = colParamSNO
            repoDecimalColumn.ReadOnly = True
            gvParam.MasterTemplate.Columns.Add(repoDecimalColumn)


            If pFields Then
                For i As Integer = 0 To dt.Rows.Count() - 1
                    repoTextColumn = New GridViewTextBoxColumn()
                    repoTextColumn.Name = dt.Rows(i)("Code")
                    repoTextColumn.Width = 120
                    repoTextColumn.HeaderText = dt.Rows(i)("Description")
                    repoTextColumn.ReadOnly = False
                    gvParam.MasterTemplate.Columns.Add(repoTextColumn)
                Next
            End If
            'Dim blnExit As Boolean = False
            gvParam.AllowAddNewRow = True
            gvParam.AddNewRowPosition = UI.SystemRowPosition.Bottom
            gvParam.AllowDeleteRow = True
            gvParam.AllowRowReorder = False
            gvParam.ShowGroupPanel = False
            gvParam.EnableFiltering = False
            gvParam.EnableSorting = False
            gvParam.EnableGrouping = False
            gvParam.AllowColumnChooser = True
            gvParam.AllowColumnReorder = True
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub


    Private Sub txtConsumType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtConsumType._MYValidating
        Try
            If clsCommon.myLen(txtSection.Value) = 0 Then
                Throw New Exception("Please select Section code first.")
                txtSection.Focus()
                Exit Sub
            End If

            Dim qry As String = ""
            Dim strWhrClause As String = ""
            qry = "select TSPL_ENG_CONSUMPTION_TYPE_MASTER.Code,TSPL_ENG_CONSUMPTION_TYPE_MASTER.Description from TSPL_ENG_CONSUMPTION_TYPE_MASTER " & _
                " left join TSPL_ENG_SECTION_CONSUMPTION_MAPPING on TSPL_ENG_SECTION_CONSUMPTION_MAPPING.Consumption_Code=TSPL_ENG_CONSUMPTION_TYPE_MASTER.Code " & _
                " left join TSPL_ENG_SECTION_MASTER on TSPL_ENG_SECTION_MASTER.Code=TSPL_ENG_SECTION_CONSUMPTION_MAPPING.Section_Code "
            strWhrClause = "TSPL_ENG_SECTION_MASTER.Code in ('" + txtSection.Value + "')"
            txtConsumType.Value = clsCommon.ShowSelectForm("ConsumTypeFinder", qry, "Code", strWhrClause, txtConsumType.Value, "Code", isButtonClicked)
            lblConsumTypedes.Text = clsEngConsumptionTypeMaster.GetName(txtConsumType.Value, Nothing)
            If clsCommon.myLen(txtConsumType.Value) > 0 AndAlso clsCommon.myLen(txtSection.Value) > 0 Then
                loadParameterGrid()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub txtSection__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSection._MYValidating
        Try
            Dim qry As String = " select Code as [Code],Description as [Description] from TSPL_ENG_SECTION_MASTER  "
            txtSection.Value = clsCommon.ShowSelectForm("TSPL_ENG_SECTION_MASTER22", qry, "Code", "", txtSection.Value, "Code", isButtonClicked)
            lblSection.Text = clsEngSectionMaster.GetName(txtSection.Value, Nothing)
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name,Loc_Short_Name as [Short Name] from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("ENG-LocFinder", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
    End Sub

    Private Sub gvParam_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvParam.CurrentColumnChanged
        If gvParam.RowCount > 0 Then
            Dim intCurrRow As Integer = gvParam.CurrentRow.Index
            gvParam.CurrentRow.Cells(0).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvParam.Rows.Count - 1 Then
                gvParam.Rows.AddNew()
                gvParam.CurrentRow = gvParam.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvParam_UserAddedRow(sender As Object, e As GridViewRowEventArgs) Handles gvParam.UserAddedRow
        Try
            For i As Integer = 0 To gvParam.Rows.Count - 1
                gvParam.Rows(0).Cells(0).Value = 1
                If i <> 0 Then
                    gvParam.Rows(i).Cells(0).Value = i + 1
                End If
            Next
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub gvParam_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gvParam.UserDeletedRow
        For ii As Integer = 1 To gvParam.Rows.Count
            gvParam.Rows(ii - 1).Cells(0).Value = ii
        Next
    End Sub

    Private Sub gvParam_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gvParam.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                myMessages.blankValue("No data found to Print")
            Else
                Dim Qry2 As String = ""
                Qry2 = "select TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2, TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin ,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.ADD3,TSPL_LOCATION_MASTER.Pin_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_ENG_SECTION_MASTER.Description as Section_Desc,TSPL_ENG_CONSUMPTION_TYPE_MASTER.Description as Consumption_Desc, TSPL_ENG_LOG_SHEET_HEAD.Doc_No as Doc_No, convert(varchar(15),TSPL_ENG_LOG_SHEET_HEAD.Doc_Date,103) as Doc_Date " & _
                    ",TSPL_ENG_LOG_SHEET_HEAD.OperatedEarlierHrs,TSPL_ENG_LOG_SHEET_HEAD.OperatedHrs,TSPL_ENG_LOG_SHEET_HEAD.OperatedTotalHrs " & _
                    ",TSPL_ENG_LOG_SHEET_HEAD.OilChange,TSPL_ENG_LOG_SHEET_HEAD.Repair" & _
                     ",TSPL_ENG_LOG_SHEET_DETAIL.SNO,TSPL_ENG_PARAMETER_MASTER.Description,TSPL_ENG_LOG_SHEET_DETAIL.Parameter_Value,TSPL_ENG_SECTION_PARAMETER_MAPPING.Parameter_Seq " & _
                     " from TSPL_ENG_LOG_SHEET_HEAD  Left outer join TSPL_ENG_LOG_SHEET_DETAIL on TSPL_ENG_LOG_SHEET_HEAD.Doc_No = TSPL_ENG_LOG_SHEET_DETAIL.Doc_No " & _
                    " left outer join TSPL_ENG_PARAMETER_MASTER on TSPL_ENG_PARAMETER_MASTER.Code =TSPL_ENG_LOG_SHEET_DETAIL.Parameter_Code " & _
                    " left join TSPL_ENG_SECTION_MASTER on TSPL_ENG_SECTION_MASTER.code=TSPL_ENG_LOG_SHEET_HEAD.Section_Code " & _
                    " left join TSPL_ENG_CONSUMPTION_TYPE_MASTER on TSPL_ENG_CONSUMPTION_TYPE_MASTER.code=TSPL_ENG_LOG_SHEET_HEAD.Consumption_Code " & _
                    " LEFT OUTER JOIN TSPL_ENG_SECTION_PARAMETER_MAPPING on TSPL_ENG_LOG_SHEET_DETAIL.Parameter_Code=TSPL_ENG_SECTION_PARAMETER_MAPPING.Parameter_Code" & _
                    " and TSPL_ENG_LOG_SHEET_HEAD.Section_Code=TSPL_ENG_SECTION_PARAMETER_MAPPING.Section_Code" & _
                    " and TSPL_ENG_LOG_SHEET_HEAD.Consumption_Code=TSPL_ENG_SECTION_PARAMETER_MAPPING.Consumption_Code" & _
                    " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_ENG_LOG_SHEET_HEAD.Comp_Code " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_ENG_LOG_SHEET_HEAD.location_code " & _
                    " WHERE  TSPL_ENG_LOG_SHEET_HEAD.Doc_No in  ('" & txtCode.Value & "')"

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry2)
                If dt.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreportWithdt(CrystalReportFolder.Engineering, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptEngLogSheet", "Log Sheet", clsCommon.myCDate(dt.Rows(0)("Doc_Date")), "rptCompanyAddress.rpt")
                    frmCRV = Nothing
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

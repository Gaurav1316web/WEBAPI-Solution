'---------------------'---------------------BM00000003305
'-BM00000003441
''updation by Richa Agarwal Against Ticket No. BM00000003766

Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports common
'' CREATED BY : SURAJ
''Start Date: 10-05-2011
'' End Date:10-05-2011
Public Class frmInventorySetting

    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
#Region "Variables"
    Dim sql As String
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Dim tableName As String = "TSPL_TAX_MASTER"
    Dim tableCode As String = "Tax_Code"
    Dim codePrefix As String = "TAX"
#End Region

    Private Sub frmInventorySetting_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'funDelete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If

    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.inventorySetting)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            mnimport.Enabled = True
            mnexport.Enabled = True
        Else
            mnimport.Enabled = False
            mnexport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        'btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub frmInventorySetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GroupBox1.Enabled = False
        chkNegativeStockInDairyProduction.Visible = True
        SetUserMgmtNew()
        Funfill()
        grpItemType.Visible = chkauto_item_nlevel.Checked
        If chkauto_item_nlevel.Checked Then
            GetItemTypeForPrefix()
        End If
        Dim dt As GridViewComboBoxColumn = TryCast(dgvclasss.Columns(3), GridViewComboBoxColumn)
        dt.DataSource = New String() {"Category Type", "Flavour Type", "Size Type", "Pack Type"}
        Dim str As String = "select Inv_Class_No from tspl_inv_class"
        Dim str1 As String = clsDBFuncationality.getSingleValue(str)
        If str1 <> "" Then
            btnsave.Text = "Update"
        End If
        chkAllowcostZero.Visible = True
        GroupBox4.Visible = False
        Dim qry As String = "select Description from TSPL_FIXED_PARAMETER where Type='IsConsiderOutTypeDocForBalance' and Code='IsConsiderOutTypeDocForBalance'"
        Dim dblStockBalanceType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If dblStockBalanceType = 1 Then
            rbtnIsConsiderOutTypeDoc.IsChecked = True
        ElseIf dblStockBalanceType = 0 Then
            rbntBalanceOnDocDate.IsChecked = True
        ElseIf dblStockBalanceType = 2 Then
            rbtnStockAvailable.IsChecked = True
        End If
        txtRawMaterial.MaxLength = 50
        txtFinishedGoods.MaxLength = 50
        txtSemiFinishGoods.MaxLength = 50
        txtAsset.MaxLength = 50
        txtOther.MaxLength = 50
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        If clsCommon.CompairString(objCommonVar.CurrentIndustryType, "D") <> CompairStringResult.Equal Then
            chkAllowtoshowMilkTypeonAdjustmentEntry.Visible = False
        End If
    End Sub

    Sub LoadLocation_StockTransfer()
        Dim qry As String = "select CAST(Case When ISNULL(TSPL_FIXED_PARAMETER.Description, 0)=10 Then 1 Else 0 End as Bit) AS [Select], CAST(Case When ISNULL(TSPL_FIXED_PARAMETER.Description, 0)=1 Then 1 Else 0 End as Bit) AS [Select1], TSPL_LOCATION_MASTER.Location_Code as Code, TSPL_LOCATION_MASTER.Location_Desc as Name, Case When TSPL_LOCATION_MASTER.Excisable='T' Then 'Yes' else 'No' End as [Excisable], TSPL_STATE_MASTER.STATE_NAME as State from TSPL_LOCATION_MASTER Left Outer Join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_LOCATION_MASTER.State LEFT OUTER JOIN (Select Type, Code, Description, Specification from TSPL_FIXED_PARAMETER Where Type='CreateTransferWithProductionSale_Retail_Series') TSPL_FIXED_PARAMETER on TSPL_FIXED_PARAMETER.Code=TSPL_LOCATION_MASTER.Location_Code WHERE Location_Type='Physical' ORDER BY TSPL_LOCATION_MASTER.Location_Code"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.Columns("Code").ReadOnly = True
        cbgLocation.Columns("Name").ReadOnly = True
        cbgLocation.Columns("State").ReadOnly = True
        If cbgLocation.Rows.Count > 0 Then
            cbgLocation.Columns("Select").Width = 50
            cbgLocation.Columns("Select1").Width = 50
            cbgLocation.Columns("Code").Width = 75
            cbgLocation.Columns("Name").Width = 180
            cbgLocation.Columns("State").Width = 180
        End If
    End Sub

    Sub LoadLocation_CSATransfer()
        'Dim qry As String = "select CAST(Case When ISNULL(TSPL_FIXED_PARAMETER.Description, 0)=1 Then 1 Else 0 End as Bit) AS [Select], TSPL_LOCATION_MASTER.Location_Code as Code,TSPL_LOCATION_MASTER.Location_Desc as Name, TSPL_STATE_MASTER.STATE_NAME as State from TSPL_LOCATION_MASTER Left Outer Join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_LOCATION_MASTER.State LEFT OUTER JOIN (Select Type, Code, Description, Specification from TSPL_FIXED_PARAMETER Where Type='CreateCSATransferWithProductionSale_Retail_Series') TSPL_FIXED_PARAMETER on TSPL_FIXED_PARAMETER.Code=TSPL_LOCATION_MASTER.Location_Code WHERE Location_Type='Physical' AND Excisable<>'T' ORDER BY TSPL_LOCATION_MASTER.Location_Code"
        'gvLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'gvLocation.Columns("Code").ReadOnly = True
        'gvLocation.Columns("Name").ReadOnly = True
        'gvLocation.Columns("State").ReadOnly = True
        'If gvLocation.Rows.Count > 0 Then
        '    gvLocation.Columns("Select").Width = 50
        '    gvLocation.Columns("Code").Width = 75
        '    gvLocation.Columns("Name").Width = 180
        '    gvLocation.Columns("State").Width = 180
        'End If
    End Sub
    ''To Authorised the user 
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "INV-SETT-M"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            'rdbtnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            'rdbtndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
    ''insert function 
    Public Sub funinsert()
        Dim strallownegative As String
        Dim strallownonstock As String


        If chkallownegativeinventory.Checked = True Then
            strallownegative = "Y"
        Else
            strallownegative = "N"
        End If
        If chkallowreceipts.Checked = True Then
            strallownonstock = "Y"
        Else
            strallownonstock = "N"
        End If
        Dim currentdate As Date = Date.Today
        Dim modify_by As String = objCommonVar.CurrentUserCode
        Dim modify_date As Date = Date.Today
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from tspl_inv_class"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_INV_PARAMETERS"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            For i As Integer = 0 To dgvclasss.Rows.Count - 1
                connectSql.RunSpTransaction(trans, "sp_tspl_inv_class_insert", New SqlParameter("@name", Me.dgvclasss.Rows(i).Cells(1).Value), New SqlParameter("@length", Me.dgvclasss.Rows(i).Cells(2).Value), New SqlParameter("@classtype", dgvclasss.Rows(i).Cells(3).Value), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", connectSql.serverDate(trans)), New SqlParameter("Modify_By", modify_by), New SqlParameter("Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
            Next
            connectSql.RunSpTransaction(trans, "sp_TSPL_inv_parameters_insert", New SqlParameter("@allownegativestock", strallownegative), New SqlParameter("@defaultitemstructure", "STR01"), New SqlParameter("@allownonstock", strallownonstock), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", connectSql.serverDate(trans)), New SqlParameter("Modify_By", userCode), New SqlParameter("Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode), New SqlParameter("@IsMRNQtyEdiatableOnSRN", IIf(chkIsEnterQtyOnSRN.Checked, 1, 0)))

            qry = "update TSPL_INV_PARAMETERS  set   Auto_Scheme='" + clsCommon.myCstr(IIf(chkAutoScheme.Checked, 1, 0)) + "', " & _
            "IsAutoCreateGRNAndMRN='" + clsCommon.myCstr(IIf(chkAutoCreateSRNMRNOnPOPost.Checked, 1, 0)) + "', " & _
            "isSetAmountZero_InItemLocDetail='" + clsCommon.myCstr(IIf(chkAllowcostZero.Checked, 1, 0)) + "', " & _
            "IsNLevelCatForItem='" + clsCommon.myCstr(IIf(chknLevelCategory.Checked, 1, 0)) + "', " & _
            "IsRateBackCalculation='" + clsCommon.myCstr(IIf(chkBackCalculation.Checked, 1, 0)) + "', " & _
            "Allow_Change_InvoiceType='" + clsCommon.myCstr(IIf(chkAllowchangeInvoiceType.Checked, 1, 0)) + "', " & _
            "IsBatchNo_MFD_EXD_Mandatory='" + clsCommon.myCstr(IIf(chkBatchMandatory.Checked, 1, 0)) + "', " & _
            "IsMRPwithAbatement='" + clsCommon.myCstr(IIf(chkMRPwithAbatement.Checked, 1, 0)) + "'," & _
            "IsTermsEditableOnPurchase='" + clsCommon.myCstr(IIf(chkAllowTermEditPurchase.Checked, 1, 0)) + "', " & _
            "IsTermsEditableOnSales='" + clsCommon.myCstr(IIf(chkAllowTermsEditSales.Checked, 1, 0)) + "'," & _
            "IsPriceGrpCodeOnCstMst='" + clsCommon.myCstr(IIf(chkalwPGMCusMst.Checked, 1, 0)) + "'," & _
            "IsTermsEditableOnInv='" + clsCommon.myCstr(IIf(chkAllowTermsEditMM.Checked, 1, 0)) + "'," & _
            "IsThirdPartyLocationOnERP='" + clsCommon.myCstr(IIf(chkthirdparty.Checked, 1, 0)) + "'," & _
            "IsBOMFromProcessProduction='" + clsCommon.myCstr(IIf(chkBomProductionProcess.Checked, 1, 0)) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim dblStockBalanceType As String = "0"
            If rbtnIsConsiderOutTypeDoc.IsChecked Then
                dblStockBalanceType = "1"
            ElseIf rbntBalanceOnDocDate.IsChecked Then
                dblStockBalanceType = "0"
            ElseIf rbtnStockAvailable.IsChecked Then
                dblStockBalanceType = "2"
            End If
            qry = "update TSPL_FIXED_PARAMETER set Description='" + dblStockBalanceType + "' where Type='" + clsFixedParameterType.IsConsiderOutTypeDocForBalance + " ' and Code='" + clsFixedParameterCode.IsConsiderOutTypeDocForBalance + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)




            If chkNlevel_Location.Checked Then
                qry = "update TSPL_FIXED_PARAMETER set Description='1' where Type='" + clsFixedParameterType.NLevelAtLocation + " ' and Code='" + clsFixedParameterCode.NLevelAtLocation + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                MDI.IsLoaction_NLevel = "YES"
            ElseIf Not chkNlevel_Location.Checked Then
                qry = "update TSPL_FIXED_PARAMETER set Description='0' where Type='" + clsFixedParameterType.NLevelAtLocation + " ' and Code='" + clsFixedParameterCode.NLevelAtLocation + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                MDI.IsLoaction_NLevel = "NO"
            End If

            ''For item counter
            qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(chkauto_item_nlevel.Checked, "1", "0") + "' where Type='" + clsFixedParameterType.AutoItemNLevel + " ' and Code='" + clsFixedParameterCode.AutoItemNLevel + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(chkauto_item_nlevel.Checked, txtRawMaterial.Text, "") + "' where Type='" + clsFixedParameterType.AutoItemNLevel + " ' and Code='" + clsFixedParameterCode.CounterRawMaterial + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(chkauto_item_nlevel.Checked, txtFinishedGoods.Text, "") + "' where Type='" + clsFixedParameterType.AutoItemNLevel + " ' and Code='" + clsFixedParameterCode.CounterFinishGood + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(chkauto_item_nlevel.Checked, txtSemiFinishGoods.Text, "") + "' where Type='" + clsFixedParameterType.AutoItemNLevel + " ' and Code='" + clsFixedParameterCode.CounterSemiFinishGood + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(chkauto_item_nlevel.Checked, txtAsset.Text, "") + "' where Type='" + clsFixedParameterType.AutoItemNLevel + " ' and Code='" + clsFixedParameterCode.CounterAsset + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(chkauto_item_nlevel.Checked, txtOther.Text, "") + "' where Type='" + clsFixedParameterType.AutoItemNLevel + " ' and Code='" + clsFixedParameterCode.CounterOther + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(chkauto_item_nlevel.Checked, txttradingGoods.Text, "") + "' where Type='" + clsFixedParameterType.AutoItemNLevel + " ' and Code='" + clsFixedParameterCode.CounterTradingGood + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" + clsCommon.myCstr(fndVehicle_Unit.Value) + "' where Type='" + clsFixedParameterType.VehicleCapacityUnit + " ' and Code='" + clsFixedParameterCode.VehicleCapacityUnit + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" + clsCommon.myCstr(fndProd_FatSnf_Base_Unit.Value) + "' where Type='" + clsFixedParameterType.ProductionFATSNF_KG_Unit + " ' and Code='" + clsFixedParameterCode.ProductionFATSNF_KG_Unit + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            ''End of For item counter

            qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(chkPrncpl_BOM.Checked, "1", "0") + "' where Type='" + clsFixedParameterType.Princi_Bom + " ' and Code='" + clsFixedParameterCode.Princi_Bom + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(chkCSACommision_Inv.Checked, "1", "0") + "' where Type='" + clsFixedParameterType.AP_INV_COMMSN + " ' and Code='" + clsFixedParameterCode.AP_INV_COMMSN + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            ''richa agarwal
            qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(chkAllowtoshowMilkTypeonAdjustmentEntry.Checked, "1", "0") + "' where Type='" + clsFixedParameterType.AllowToShowMilkTypeinAdjustmentEntry + " ' and Code='" + clsFixedParameterCode.AllowToShowMilkTypeinAdjustmentEntry + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(chkGPAfterTransfer.Checked, "1", "0") + "' where Type='" + clsFixedParameterType.GatePassAfterTransfer + " ' and Code='" + clsFixedParameterCode.GatePassAfterTransfer + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(chkCreateTransferFromBooking.Checked, "1", "0") + "' where Type='" + clsFixedParameterType.CreateTransferFromBooking + " ' and Code='" + clsFixedParameterCode.CreateTransferFromBooking + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(ChkAllowtoeditCategorycodeinitemmaster.Checked, "1", "0") + "' where Type='" + clsFixedParameterType.AllowToEditCategoryCodeinItemMaster + " ' and Code='" + clsFixedParameterCode.AllowToEditCategoryCodeinItemMaster + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(chkItemWithDifferntUnitConsiderAsOtherItem.Checked, "1", "0") + "' where Type='" + clsFixedParameterType.IsItemWithDifferntUnitConsiderAsOtherItem + " ' and Code='" + clsFixedParameterCode.IsItemWithDifferntUnitConsiderAsOtherItem + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''================
            qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(chkIsMRPWiseBalance.Checked, "1", "0") + "' where Type='" + clsFixedParameterType.IsMRPWiseBalance + " ' and Code='" + clsFixedParameterCode.IsMRPWiseBalance + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(chkTransferJEForLocationMapping.Checked, "1", "0") + "' where Type='" + clsFixedParameterType.TransferJEForLocationMapping + " ' and Code='" + clsFixedParameterCode.TransferJEForLocationMapping + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '' Anubhooti 23-Jan-2015 (Setting For Creation of GL Acc To Item GL Account(Issue/Return/Transfer))
            'qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(ChkGLAccToItem.Checked, "1", "0") + "' where Type='" + clsFixedParameterType.CreateGLAccToItem + " ' and Code='" + clsFixedParameterCode.CreateGLAccToItem + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '----------------------Setting for Stock Transfer Series----------------------
            qry = "Delete from TSPL_FIXED_PARAMETER Where Type in ('" & clsFixedParameterType.TransferWithProductionSale_Retail_Series & "','" & clsFixedParameterType.TransferLocalInterState & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each grow As GridViewRowInfo In cbgLocation.Rows
                If grow.Cells("Select").Value = True Then
                    qry = "10"
                ElseIf grow.Cells("Select1").Value = True Then
                    qry = "01"
                Else
                    qry = "00"
                End If
                clsDBFuncationality.ExecuteNonQuery("Insert Into TSPL_FIXED_PARAMETER(Type, Code, Description, Specification) Values ('" + clsFixedParameterType.TransferWithProductionSale_Retail_Series + "', '" & grow.Cells("Code").Value & "', '" & qry & "', '')", trans)
            Next
            clsDBFuncationality.ExecuteNonQuery("Insert Into TSPL_FIXED_PARAMETER(Type, Code, Description, Specification) Values ('" + clsFixedParameterType.TransferLocalInterState + "', '" & clsFixedParameterType.TransferLocalInterState & "', '" + clsCommon.myCstr(IIf(chkLocal_InterStateTransfer.Checked, 1, 0)) + "', '1: Different Local/InterState Transfer, 2:Same')", trans)
            '-----------------------------------------------------------------------

            '----------------------Setting for CSA Transfer Series------------------------
            'qry = "Delete from TSPL_FIXED_PARAMETER Where Type='" + clsFixedParameterType.CSATransferWithProductionSale_Retail_Series + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'For Each grow1 As GridViewRowInfo In gvLocation.Rows
            '    clsDBFuncationality.ExecuteNonQuery("Insert Into TSPL_FIXED_PARAMETER(Type, Code, Description, Specification) Values ('" + clsFixedParameterType.CSATransferWithProductionSale_Retail_Series + "', '" + grow1.Cells("Code").Value + "', '" + clsCommon.myCstr(IIf(grow1.Cells("Select").Value = True, 1, 0)) + "', '')", trans)
            'Next
            '-----------------------------------------------------------------------
            qry = "update TSPL_FIXED_PARAMETER set Description='" + clsCommon.myCstr(txtProdQty_Decimal.Text) + "' where type='" + clsFixedParameterType.ProductionQtyDecimalPoint + "' and code='" + clsFixedParameterCode.ProductionQtyDecimalPoint + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(chkAllowNegativeStock.Checked, "1", "0") + "',Specification='" + clsCommon.myCstr(txtNegativeStock.Value) + "' where Type='" + clsFixedParameterType.AllowNegativeStock + " ' and Code='" + clsFixedParameterCode.AllowNegativeStock + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'stuti==07/09/2016
            qry = "update TSPL_FIXED_PARAMETER set Description='" + clsCommon.myCstr(gv_itemsettings.Rows(0).Cells("Wt").Value) + "' where Type='" + clsFixedParameterType.ItemCrateWtinKg + " ' and Code='" + clsFixedParameterCode.ItemCrateWtinKg + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" + clsCommon.myCstr(gv_itemsettings.Rows(0).Cells("Rate").Value) + "' where Type='" + clsFixedParameterType.ItemCrateRate + " ' and Code='" + clsFixedParameterCode.ItemCrateRate + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" + clsCommon.myCstr(gv_itemsettings.Rows(1).Cells("Wt").Value) + "' where Type='" + clsFixedParameterType.ItemJaaliWtinKg + " ' and Code='" + clsFixedParameterCode.ItemJaaliWtinKg + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" + clsCommon.myCstr(gv_itemsettings.Rows(1).Cells("Rate").Value) + "' where Type='" + clsFixedParameterType.ItemJaaliRate + " ' and Code='" + clsFixedParameterCode.ItemJaaliRate + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" + clsCommon.myCstr(gv_itemsettings.Rows(2).Cells("Wt").Value) + "' where Type='" + clsFixedParameterType.ItemBoxWtinKg + " ' and Code='" + clsFixedParameterCode.ItemBoxWtinKg + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" + clsCommon.myCstr(gv_itemsettings.Rows(2).Cells("Rate").Value) + "' where Type='" + clsFixedParameterType.ItemBoxRate + " ' and Code='" + clsFixedParameterCode.ItemBoxRate + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If chkauto_item_nlevel.Checked Then
                For Each grow As GridViewRowInfo In gvItemType.Rows
                    qry = "UPDATE TSPL_ITEM_TYPE_MASTER SET PREFIX='" + clsCommon.myCstr(grow.Cells("ColPrefix").Value) + "' where ITEM_TYPE_CODE='" + clsCommon.myCstr(grow.Cells("ColItemTypeCode").Value) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Next
            End If

            '===================================end here================================


            qry = "update TSPL_FIXED_PARAMETER set Description='" + IIf(chkNegativeStockInDairyProduction.Checked, "1", "0") + "' where Type='" + clsFixedParameterType.AllowNegativeStockInDairyProduction + " ' and Code='" + clsFixedParameterCode.AllowNegativeStockInDairyProduction + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            myMessages.insert()

            Try
                MDI.IsLoc_Third_Party = clsCommon.myCstr(IIf(chkthirdparty.Checked, "YES", "NO"))
            Catch ex1 As Exception
            End Try

        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)

        End Try
    End Sub
    ''To Close the inventory setting screen
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    ''To Call The Insert function
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click

        SaveData()

    End Sub
    Sub SaveData()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.inventorySetting, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        Dim total As Decimal
        Dim check As String = ""

        For i As Integer = 0 To dgvclasss.Rows.Count - 1
            If Not String.IsNullOrEmpty(dgvclasss.Rows(i).Cells(2).Value.ToString()) Then
                total = total + dgvclasss.Rows(i).Cells(2).Value
            End If
        Next
        Dim total2 As Decimal = total
        If total2 > 50 Then
            common.clsCommon.MyMessageBoxShow("Not More Than 50")
        Else
            For i As Integer = 0 To dgvclasss.Rows.Count - 1
                If String.IsNullOrEmpty(dgvclasss.Rows(i).Cells(2).Value.ToString()) Or String.IsNullOrEmpty(dgvclasss.Rows(i).Cells(3).Value.ToString()) Then
                    check = i
                End If
            Next
            If Not String.IsNullOrEmpty(check) Then
                common.clsCommon.MyMessageBoxShow("Please enter the class length and Class Type")
            Else
                funinsert()
                btnsave.Text = "Update"
                Funfill()
            End If

            'Else : btnsave.Text = "Update"
            '    For i As Integer = 0 To dgvclasss.Rows.Count - 1
            '        If String.IsNullOrEmpty(dgvclasss.Rows(i).Cells(2).Value.ToString()) Or String.IsNullOrEmpty(dgvclasss.Rows(i).Cells(3).Value.ToString()) Then
            '            check = i
            '        End If
            '    Next
            '    If Not String.IsNullOrEmpty(check) Then
            '        common.clsCommon.MyMessageBoxShow("Please enter the class  length and Class Type")
            '    Else
            '        funupdate()
            '        Funfill()
            '    End If
            'End If
        End If
    End Sub
    ''Apply character validation on radgridview cell 1 and cell 2(numeric validation)
    Private Sub dgvclasss_CellValidating(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs) Handles dgvclasss.CellValidating
        If Me.dgvclasss.MasterTemplate.CurrentColumn.Index = 1 Then
            Dim strclassname As String = e.Value
            If strclassname <> "" Then
                Dim check As Match = Regex.Match(strclassname, "^[a-zA-Z ]*$")
                If check.Success Then
                Else
                    common.clsCommon.MyMessageBoxShow("Enter only Alphabetics")
                    e.Cancel = True
                End If
            End If
        End If
        'Dim column As GridViewDataColumn = e.Column
        'If TypeOf e.Row Is GridViewRowInfo Then
        '    If column.HeaderText = "Class Type" Then
        '        For Each grow As GridViewDataRowInfo In dgvclasss.Rows
        '            If e.Value = grow.Cells(3).Value Then
        '                e.Cancel = True
        '            End If
        '        Next
        '    End If
        'End If
    End Sub
    '' To Update data on the table(tspl_inv_class)
    'Public Sub funupdate()
    '    Dim trans As SqlTransaction
    '    Try
    '        Dim strdelete As String = "truncate table tspl_inv_class"
    '        connectSql.RunSql(strdelete)

    '        strdelete = "delete from TSPL_INV_PARAMETERS"
    '        connectSql.RunSql(strdelete)


    '        Dim strallownegative As String
    '        Dim strallownonstock As String
    '        If chkallownegativeinventory.Checked = True Then
    '            strallownegative = "Y"
    '        Else
    '            strallownegative = "N"
    '        End If
    '        If chkallowreceipts.Checked = True Then
    '            strallownonstock = "Y"
    '        Else
    '            strallownonstock = "N"
    '        End If
    '        Dim currentdate As Date = Date.Today
    '        Dim modify_by As String = "suraj"
    '        Dim modify_date As Date = Date.Today
    '        connectSql.OpenConnection()
    '        trans = clsDBFuncationality.GetTransactin()
    '        For i As Integer = 0 To dgvclasss.Rows.Count - 1
    '            If Not String.IsNullOrEmpty(dgvclasss.Rows(i).Cells(1).Value) Then
    '                connectSql.RunSpTransaction(trans, "sp_tspl_inv_class_insert", New SqlParameter("@name", Me.dgvclasss.Rows(i).Cells(1).Value), New SqlParameter("@length", Me.dgvclasss.Rows(i).Cells(2).Value.ToString()), New SqlParameter("@classtype", dgvclasss.Rows(i).Cells(3).Value), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", connectSql.serverDate(trans)), New SqlParameter("Modify_By", modify_by), New SqlParameter("Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
    '            End If
    '        Next
    '        connectSql.RunSpTransaction(trans, "sp_TSPL_inv_parameters_update", New SqlParameter("@allownegativestock", strallownegative), New SqlParameter("@allownonstock", strallownonstock), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", connectSql.serverDate(trans)), New SqlParameter("Modify_By", userCode), New SqlParameter("Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@IsMRNQtyEdiatableOnSRN", IIf(chkIsEnterQtyOnSRN.Checked, 1, 0)))
    '        trans.Commit()
    '        myMessages.update()
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
    '        trans.Rollback()
    '        myMessages.myExceptions(ex)

    '    End Try
    'End Sub
    ''To Retrieve data from the table(tspl_inv_class) and (TSPL_inv_parameters)
    Private Sub Funfill()
        loadBlankItemSettingsGrid()
        dgvclasss.DataSource = Nothing

        Dim str12 As String = "select Inv_Class_No, Inv_Class_Name, Inv_Class_Length, Class_Type  from tspl_inv_class  order by inv_class_no "
        dgvclasss.AutoGenerateColumns = False
        transportSql.FillGridView(str12, dgvclasss)
        dgvclasss.Columns(0).FieldName = "Inv_Class_No"
        dgvclasss.Columns(1).FieldName = "Inv_Class_Name"
        dgvclasss.Columns(2).FieldName = "Inv_Class_Length"
        dgvclasss.Columns(3).FieldName = "Class_Type"
        For i As Integer = 0 To dgvclasss.Rows.Count - 1
            Dim strcheck As String = connectSql.RunScalar("select top 1 Inv_Class_Code  from TSPL_INV_CLASS_DETAILS where Inv_Class_Name  ='" + dgvclasss.Rows(i).Cells(1).Value + "'")
            If strcheck <> "" Then
                dgvclasss.Rows(i).Cells(1).ReadOnly = True
                dgvclasss.Rows(i).Cells(2).ReadOnly = True
                dgvclasss.Rows(i).Cells(3).ReadOnly = True
            End If
        Next

        Dim da2 As New SqlDataAdapter("select IsBatchNo_MFD_EXD_Mandatory,Allow_Change_InvoiceType,IsMRPwithAbatement,IsRateBackCalculation,Allow_Negative_Inv, Allow_Non_Stock,IsMRNQtyEdiatableOnSRN,IsCreateJEForProductionEntry,IsCreateJEForStoreAdj,IsAutoCreateGRNAndMRN,isSetAmountZero_InItemLocDetail, IsCreateJEForSRN,IsNLevelCatForItem,Auto_Scheme,IsTermsEditableOnPurchase,IsTermsEditableOnSales,IsTermsEditableOnInv,IsPriceGrpCodeOnCstMst,IsThirdPartyLocationOnERP,IsBOMFromProcessProduction from TSPL_inv_parameters", connectSql.SqlCon())
        Dim dt2 As New DataTable()
        da2.Fill(dt2)
        If dt2.Rows.Count > 0 Then
            Dim row2 As DataRow = dt2.Rows(0)
            For Each r As Object In row2.Table.Rows
                Dim strallownegative As String = r(0).ToString().Trim()
                Dim strallownonreceipt As String = r(1).ToString().Trim()
                If strallownegative = "Y" Then
                    chkallownegativeinventory.Checked = True
                Else
                    chkallownegativeinventory.Checked = False
                End If
                If strallownonreceipt = "Y" Then
                    chkallowreceipts.Checked = True
                Else
                    chkallowreceipts.Checked = False
                End If
                'chkJEProductionEntry.Checked = IIf(clsCommon.myCdbl(r("IsCreateJEForProductionEntry")) = 0, False, True)
                chkAutoScheme.Checked = IIf(clsCommon.myCdbl(r("Auto_Scheme")) = 0, False, True)
                'chkJEStoreAdjustment.Checked = IIf(clsCommon.myCdbl(r("IsCreateJEForStoreAdj")) = 0, False, True)
                chkAutoCreateSRNMRNOnPOPost.Checked = IIf(clsCommon.myCdbl(r("IsAutoCreateGRNAndMRN")) = 0, False, True)
                chkAllowcostZero.Checked = IIf(clsCommon.myCdbl(r("isSetAmountZero_InItemLocDetail")) = 0, False, True)
                'chkJE4SRN.Checked = IIf(clsCommon.myCdbl(r("IsCreateJEForSRN")) = 0, False, True)
                chknLevelCategory.Checked = IIf(clsCommon.myCdbl(r("IsNLevelCatForItem")) = 0, False, True)
                chkBackCalculation.Checked = IIf(clsCommon.myCdbl(r("IsRateBackCalculation")) = 0, False, True)
                chkMRPwithAbatement.Checked = IIf(clsCommon.myCdbl(r("IsMRPwithAbatement")) = 0, False, True)
                chkAllowchangeInvoiceType.Checked = IIf(clsCommon.myCdbl(r("Allow_Change_InvoiceType")) = 0, False, True)
                chkBatchMandatory.Checked = IIf(clsCommon.myCdbl(r("IsBatchNo_MFD_EXD_Mandatory")) = 0, False, True)
                chkAllowTermEditPurchase.Checked = IIf(clsCommon.myCdbl(r("IsTermsEditableOnPurchase")) = 0, False, True)
                chkAllowTermsEditSales.Checked = IIf(clsCommon.myCdbl(r("IsTermsEditableOnSales")) = 0, False, True)
                chkAllowTermsEditMM.Checked = IIf(clsCommon.myCdbl(r("IsTermsEditableOnInv")) = 0, False, True)
                chkalwPGMCusMst.Checked = IIf(clsCommon.myCdbl(r("IsPriceGrpCodeOnCstMst")) = 0, False, True)
                chkthirdparty.Checked = IIf(clsCommon.myCdbl(r("IsThirdPartyLocationOnERP")) = 0, False, True)
                chkBomProductionProcess.Checked = IIf(clsCommon.myCdbl(r("IsBOMFromProcessProduction")) = 0, False, True)
            Next
        End If

        'txtdesc_seprtr.Text = ""
        'txtseperator.Text = ""
        chkNlevel_Location.Checked = IIf(clsFixedParameter.GetData(clsFixedParameterType.NLevelAtLocation, clsFixedParameterCode.NLevelAtLocation, Nothing) = "1", True, False)

        chkPrncpl_BOM.Checked = IIf(clsFixedParameter.GetData(clsFixedParameterType.Princi_Bom, clsFixedParameterCode.Princi_Bom, Nothing) = "0", False, True)
        chkCSACommision_Inv.Checked = IIf(clsFixedParameter.GetData(clsFixedParameterType.AP_INV_COMMSN, clsFixedParameterCode.AP_INV_COMMSN, Nothing) = "0", False, True)
        ''richa agarwal
        chkAllowtoshowMilkTypeonAdjustmentEntry.Checked = IIf(clsFixedParameter.GetData(clsFixedParameterType.AllowToShowMilkTypeinAdjustmentEntry, clsFixedParameterCode.AllowToShowMilkTypeinAdjustmentEntry, Nothing) = "0", False, True)
        chkGPAfterTransfer.Checked = IIf(clsFixedParameter.GetData(clsFixedParameterType.GatePassAfterTransfer, clsFixedParameterCode.GatePassAfterTransfer, Nothing) = "1", True, False)
        chkCreateTransferFromBooking.Checked = IIf(clsFixedParameter.GetData(clsFixedParameterType.CreateTransferFromBooking, clsFixedParameterCode.CreateTransferFromBooking, Nothing) = "1", True, False)
        ChkAllowtoeditCategorycodeinitemmaster.Checked = IIf(clsFixedParameter.GetData(clsFixedParameterType.AllowToEditCategoryCodeinItemMaster, clsFixedParameterCode.AllowToEditCategoryCodeinItemMaster, Nothing) = "0", False, True)
        ''====================
        '' Anubhooti 23-Jan-2015 (Setting For Creation of GL Acc To Item GL Account(Issue/Return/Transfer))
        'ChkGLAccToItem.Checked = IIf(clsFixedParameter.GetData(clsFixedParameterType.CreateGLAccToItem, clsFixedParameterCode.CreateGLAccToItem, Nothing) = "0", False, True)
        ''
        ''For item counter
        chkauto_item_nlevel.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoItemNLevel, clsFixedParameterCode.AutoItemNLevel, Nothing)) > 0, True, False)
        'txtRawMaterial.Text = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoItemNLevel, clsFixedParameterCode.CounterRawMaterial, Nothing))
        'txtFinishedGoods.Text = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoItemNLevel, clsFixedParameterCode.CounterFinishGood, Nothing))
        'txtSemiFinishGoods.Text = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoItemNLevel, clsFixedParameterCode.CounterSemiFinishGood, Nothing))
        'txtAsset.Text = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoItemNLevel, clsFixedParameterCode.CounterAsset, Nothing))
        'txtOther.Text = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoItemNLevel, clsFixedParameterCode.CounterOther, Nothing))
        'txttradingGoods.Text = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoItemNLevel, clsFixedParameterCode.CounterTradingGood, Nothing))

        'GroupBox2.Visible = chkauto_item_nlevel.Checked
        grpItemType.Visible = chkauto_item_nlevel.Checked
        chkItemWithDifferntUnitConsiderAsOtherItem.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsItemWithDifferntUnitConsiderAsOtherItem, clsFixedParameterCode.IsItemWithDifferntUnitConsiderAsOtherItem, Nothing)) > 0, True, False)
        chkIsMRPWiseBalance.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsMRPWiseBalance, clsFixedParameterCode.IsMRPWiseBalance, Nothing)) > 0, True, False)
        'If chkauto_item_nlevel.Checked Then
        '    txtseperator.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.AutoItemNLevel + "' and code='" + clsFixedParameterType.AutoItemNLevel + "'"))
        '    If clsCommon.myLen(txtseperator.Text) > 1 Then
        '        txtdesc_seprtr.Text = txtseperator.Text.Substring(1, txtseperator.Text.Length - 1)
        '        txtseperator.Text = txtseperator.Text.Substring(0, 1)
        '    End If
        'End If

        fndVehicle_Unit.Value = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.VehicleCapacityUnit, clsFixedParameterCode.VehicleCapacityUnit, Nothing))
        fndProd_FatSnf_Base_Unit.Value = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ProductionFATSNF_KG_Unit, clsFixedParameterCode.ProductionFATSNF_KG_Unit, Nothing))
        chkTransferJEForLocationMapping.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TransferJEForLocationMapping, clsFixedParameterCode.TransferJEForLocationMapping, Nothing)) > 0, True, False)
        chkTransferWithProductionSaleSeries.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TransferWithProductionSale_Retail_Series, clsFixedParameterCode.TransferWithProductionSale_Retail_Series, Nothing)) > 0, True, False)
        txtProdQty_Decimal.Text = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionQtyDecimalPoint, clsFixedParameterCode.ProductionQtyDecimalPoint, Nothing))
        LoadLocation_StockTransfer()
        chkLocal_InterStateTransfer.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TransferLocalInterState, clsFixedParameterType.TransferLocalInterState, Nothing)) > 0, True, False)
        chkAllowNegativeStock.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStock, clsFixedParameterType.AllowNegativeStock, Nothing)) > 0, True, False)
        txtNegativeStock.Value = clsCommon.myCdbl(clsFixedParameter.GetSpecification(clsFixedParameterType.AllowNegativeStock, clsFixedParameterType.AllowNegativeStock, Nothing))
        chkNegativeStockInDairyProduction.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStockInDairyProduction, clsFixedParameterType.AllowNegativeStockInDairyProduction, Nothing)) > 0, True, False)
        LoadLocation_CSATransfer()
    End Sub
    ''export function
    Private Sub mnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnexport.Click
        Dim str As String = "select distinct m.Inv_Class_No as[Invoice Class No], m.Inv_Class_Name as [Invoice Class Name], m.Inv_Class_Length AS [Invoice Class Length], m.Class_Type as [Class Type], d.Allow_Negative_Inv as [Allow Negative Inventory], d.Allow_Non_Stock as [Allow Non Stock], m.Created_By as [Created By], m.Created_Date  as [Created Date], m.Modify_By as [Modify By], m.Modify_Date as [Modify Date], m.Comp_Code as [Company Code] from TSPL_inv_parameters d join TSPL_INV_CLASS m on m.Comp_Code = d.Comp_Code "
        Dim orderByClause = "[Invoice Class No] "
        ListImpExpColumnsMandatory = New List(Of String)({"Invoice Class Name", "Invoice Class Length"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Invoice Class Name"})
        transportSql.ExporttoExcel(str, "", orderByClause, Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub
    ''To close the form
    Private Sub mnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnclose.Click
        Me.Close()
    End Sub
    ''import function
    Private Sub mnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Invoice Class Name", "Invoice Class Length", "Class Type", "Allow Negative Inventory", "Allow Non Stock") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strinvoiceclass As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim strinvoiceclasslength As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim strclasstype As String = clsCommon.myCstr(grow.Cells(2).Value)
                    Dim strallownegative As String = clsCommon.myCstr(grow.Cells(3).Value)
                    Dim strallownonstock As String = clsCommon.myCstr(grow.Cells(4).Value)
                    ' Dim str5 As String = grow.Cells(5).Value
                    If String.IsNullOrEmpty(strinvoiceclass) Then
                        Throw New Exception("Invoice Class Name has some error")

                    End If
                    If String.IsNullOrEmpty(strinvoiceclasslength) Then
                        Throw New Exception("Invoice Class Name has some error")

                    End If
                    Dim sql1 As String = "select count(*) from TSPL_INV_CLASS where Inv_Class_Name='" + strinvoiceclass + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        Dim Sql As String = "insert into TSPL_INV_CLASS(Inv_Class_Name, Inv_Class_Length, Class_Type,Created_By, Created_Date, Modify_By, Modify_Date, Comp_Code) values ('" + strinvoiceclass + "', '" + strinvoiceclasslength + "', '" + strclasstype + "','" + userCode + "', '" + connectSql.serverDate() + "', '" + userCode + "', '" + connectSql.serverDate() + "', '" + companyCode + "' )"
                        connectSql.RunSqlTransaction(trans, Sql)
                        Dim strupdate As String = "update TSPL_INV_PARAMETERS set Allow_Negative_Inv = '" + strallownegative + "', Allow_Non_Stock = '" + strallownonstock + "', Created_By = '" + userCode + "', Create_Date = '" + connectSql.serverDate(trans) + "', Modify_By = '" + userCode + "', Modify_Date = '" + connectSql.serverDate(trans) + "', Comp_Code = '" + companyCode + "'"
                        connectSql.RunSqlTransaction(trans, strupdate)

                    Else
                        Dim sql As String = "update TSPL_INV_CLASS set Inv_Class_Length = '" + strinvoiceclasslength + "', Class_Type = '" + strclasstype + "' where Inv_Class_Name = '" + strinvoiceclass + "' "
                        connectSql.RunSqlTransaction(trans, sql)
                        Dim strupdate As String = "update TSPL_INV_PARAMETERS set Allow_Negative_Inv = '" + strallownegative + "', Allow_Non_Stock = '" + strallownonstock + "', Created_By = '" + userCode + "', Create_Date = '" + connectSql.serverDate(trans) + "', Modify_By = '" + userCode + "', Modify_Date = '" + connectSql.serverDate(trans) + "', Comp_Code = '" + companyCode + "'"
                        connectSql.RunSqlTransaction(trans, strupdate)
                    End If

                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                'trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)

    End Sub
    ''To check segment is used or not 
    Private Sub funchecksegment()

    End Sub

    Private Sub dgvclasss_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles dgvclasss.EditorRequired
        '' {"Category Type", "Flavour Type", "Size Type", "Pack Type"}
        For j As Integer = 0 To dgvclasss.Rows.Count - 1
            If dgvclasss.Rows(j).Cells(3).Value = "Pack Type" Then
                Dim dt As GridViewComboBoxColumn = TryCast(dgvclasss.Columns(3), GridViewComboBoxColumn)
                dt.DataSource = Nothing
                dt.DataSource = New String() {"Category Type", "Flavour Type", "Size Type"}
            End If
        Next
    End Sub

    Private Sub chkauto_item_nlevel_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkauto_item_nlevel.ToggleStateChanged
        'GroupBox2.Visible = chkauto_item_nlevel.Checked
        grpItemType.Visible = chkauto_item_nlevel.Checked
    End Sub


    Private Sub fndVehicle_Unit__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndVehicle_Unit._MYValidating
        fndVehicle_Unit.Value = clsCommon.ShowSelectForm("FIXUNITFND", "select unit_code as Code,unit_desc as Description from tspl_unit_master", "Code", "", fndVehicle_Unit.Value, "Code", isButtonClicked)
    End Sub


    Private Sub cbgLocation_ValueChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles cbgLocation.ValueChanging
        Try
            If cbgLocation.CurrentColumn Is cbgLocation.Columns("Select") Then
                If e.NewValue Then
                    cbgLocation.CurrentRow.Cells("Select1").Value = False
                End If
            ElseIf cbgLocation.CurrentColumn Is cbgLocation.Columns("Select1") Then
                If e.NewValue Then
                    cbgLocation.CurrentRow.Cells("Select").Value = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub fndProd_FatSnf_Base_Unit__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndProd_FatSnf_Base_Unit._MYValidating
        fndProd_FatSnf_Base_Unit.Value = clsCommon.ShowSelectForm("FIXUNITProdFND", "select unit_code as Code,unit_desc as Description from tspl_unit_master", "Code", "", fndProd_FatSnf_Base_Unit.Value, "Code", isButtonClicked)
    End Sub

    Private Sub loadBlankItemSettingsGrid()
        gv_itemsettings.Rows.Clear()
        gv_itemsettings.Columns.Clear()

        Dim itemname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        itemname.FormatString = ""
        itemname.HeaderText = "Item Name"
        itemname.Name = "ItemName"
        itemname.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        itemname.ReadOnly = True
        itemname.Width = 50
        gv_itemsettings.MasterTemplate.Columns.Add(itemname)

        Dim itemwt As GridViewDecimalColumn = New GridViewDecimalColumn()
        itemwt.FormatString = ""
        itemwt.HeaderText = "Wt. (Kg.)"
        itemwt.Name = "Wt"
        itemwt.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        itemwt.ReadOnly = False
        itemwt.Width = 50
        gv_itemsettings.MasterTemplate.Columns.Add(itemwt)

        Dim itemrate As GridViewDecimalColumn = New GridViewDecimalColumn()
        itemrate.FormatString = ""
        itemrate.HeaderText = "Rate"
        itemrate.Name = "Rate"
        itemrate.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        itemrate.ReadOnly = False
        itemrate.Width = 50
        gv_itemsettings.MasterTemplate.Columns.Add(itemrate)


        gv_itemsettings.Rows.Add("Crate", 0, 0)
        gv_itemsettings.Rows.Add("Jaali", 0, 0)
        gv_itemsettings.Rows.Add("Box", 0, 0)


        gv_itemsettings.AllowAddNewRow = False
        gv_itemsettings.AllowDeleteRow = False
        '-----------------------------------------'
        gv_itemsettings.Rows(0).Cells(1).Value = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemCrateWtinKg, clsFixedParameterCode.ItemCrateWtinKg, Nothing))

        gv_itemsettings.Rows(0).Cells(2).Value = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemCrateRate, clsFixedParameterCode.ItemCrateRate, Nothing))

        gv_itemsettings.Rows(1).Cells(1).Value = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemJaaliWtinKg, clsFixedParameterCode.ItemJaaliWtinKg, Nothing))

        gv_itemsettings.Rows(1).Cells(2).Value = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemJaaliRate, clsFixedParameterCode.ItemJaaliRate, Nothing))

        gv_itemsettings.Rows(2).Cells(1).Value = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemBoxWtinKg, clsFixedParameterCode.ItemBoxWtinKg, Nothing))

        gv_itemsettings.Rows(2).Cells(2).Value = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemBoxRate, clsFixedParameterCode.ItemBoxRate, Nothing))

    End Sub
    Public Sub GetItemTypeForPrefix()
        Dim Qry = "WITH  A AS (select * from TSPL_FIXED_PARAMETER where TYPE='NLevel_ItemCode') "
        Qry += " SELECT ITEM_TYPE_CODE,ITEM_TYPE_NAME,Description=(CASE WHEN ISNULL(PREFIX,'')!='' THEN PREFIX ELSE Description END) FROM TSPL_ITEM_TYPE_MASTER I LEFT JOIN A ON A.CODE=I.ITEM_TYPE_CODE"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        gvItemType.DataSource = dt
        gvItemType.AllowAddNewRow = False
        gvItemType.AllowDeleteRow = False
    End Sub

    Private Sub gvItemType_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvItemType.CellValueChanged

    End Sub
End Class

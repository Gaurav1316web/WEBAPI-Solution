Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Public Class FrmsaleReturnGateEntry
    Dim FORMTYPE As String = Nothing
    Public strParentCust As String = ""
    Dim AllowPlandDeptMCCLocation As Boolean = False
    Dim arrLoc As String = Nothing
    Const colLineNo As String = "COLLNO"
    Const colItemCode As String = "colItemCode"
    Const colItemDesc As String = "colItemDesc"
    Const colHSNNo As String = "COLHSNNo"
    Const colUnit As String = "COLUNIT"
    Const colQty As String = "COLQTY"
    Const colRemarks As String = "colRemarks"
    Public isInsideLoadData As Boolean = False
    Dim WhrCls As String = Nothing
    Public isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub

#Region "Functions"
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag


    End Sub
#End Region
#End Region
    Sub LoadDocType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "FS"
        dr("Name") = "Fresh Sale"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "PS"
        dr("Name") = "Product Sale"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "MISS"
        dr("Name") = "Misc Sale"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "MCCS"
        dr("Name") = "MCC Sale"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "BS"
        'dr("Name") = "Bulk Sale"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "EXPS"
        dr("Name") = "Export Sale"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CSATRAN"
        dr("Name") = "CSA Transfer"
        dt.Rows.Add(dr)

        ddlDocType.DataSource = dt
        ddlDocType.ValueMember = "Code"
        ddlDocType.DisplayMember = "Name"
    End Sub
    Sub DisableTpe()
        If clsCommon.myLen(ddlDocType.SelectedValue) > 0 Then
            ddlDocType.Enabled = False
        Else
            ddlDocType.Enabled = True
        End If

    End Sub
    Sub EnableDisableControl()
        If clsCommon.CompairString(ddlDocType.Text, "Fresh Sale") = CompairStringResult.Equal Then
            txtVehicleCode.Enabled = True
            txtManualVehicle.Enabled = True
            txtTransporterCode.Enabled = False
            'txtManualTransport.Enabled = True
        ElseIf clsCommon.CompairString(ddlDocType.Text, "Product Sale") = CompairStringResult.Equal Then
            txtVehicleCode.Enabled = True
            txtManualVehicle.Enabled = True
            txtTransporterCode.Enabled = True
            'txtManualTransport.Enabled = False
        ElseIf clsCommon.CompairString(ddlDocType.Text, "Misc Sale") = CompairStringResult.Equal Then
            txtVehicleCode.Enabled = True
            txtManualVehicle.Enabled = True
            txtTransporterCode.Enabled = True
            'txtManualTransport.Enabled = False

        ElseIf clsCommon.CompairString(ddlDocType.Text, "MCC Sale") = CompairStringResult.Equal Then
            txtVehicleCode.Enabled = False
            txtManualVehicle.Enabled = True
            txtTransporterCode.Enabled = False
            'txtManualTransport.Enabled = True
        ElseIf clsCommon.CompairString(ddlDocType.Text, "Bulk Sale") = CompairStringResult.Equal Then
            txtVehicleCode.Enabled = False
            txtManualVehicle.Enabled = True
            txtTransporterCode.Enabled = False
            'txtManualTransport.Enabled = True
        ElseIf clsCommon.CompairString(ddlDocType.Text, "Export Sale") = CompairStringResult.Equal Then
            txtVehicleCode.Enabled = False
            txtManualVehicle.Enabled = True
            txtTransporterCode.Enabled = False
            'txtManualTransport.Enabled = True
        ElseIf clsCommon.CompairString(ddlDocType.Text, "CSA Transfer") = CompairStringResult.Equal Then
            txtVehicleCode.Enabled = False
            txtManualVehicle.Enabled = True
            txtTransporterCode.Enabled = True
            'txtManualTransport.Enabled = False
        Else
            txtVehicleCode.Enabled = True
            txtManualVehicle.Enabled = True
            txtTransporterCode.Enabled = True
            txtManualTransport.Enabled = True
        End If

    End Sub
    Private Function MCCLOCATIONFINDER()
        Dim arrloc As String = ""
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(True)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                arrloc = obj.arrLocCodes
            Else
               
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arrloc
    End Function
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.IsVisible = False
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo) '0

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Width = 100
        repoICode.Name = colItemCode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoICode) '1

        Dim repoICodeDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICodeDesc.FormatString = ""
        repoICodeDesc.HeaderText = "Item Desc"
        repoICodeDesc.Name = colItemDesc
        repoICodeDesc.Width = 150
        repoICodeDesc.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoICodeDesc) '2


        Dim repoICodeHSN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICodeHSN.FormatString = ""
        repoICodeHSN.HeaderText = "HSN Code"
        repoICodeHSN.Name = colHSNNo
        repoICodeHSN.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICodeHSN.Width = 100
        repoICodeHSN.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICodeHSN) '3

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 80
        repoUnit.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoUnit) '4

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Minimum = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty) '5

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 150
        repoRemarks.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoRemarks)

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

    End Sub

    Sub reset()
        blankAllControl()
        LoadBlankGrid()
        EnableDisableControl()
        gv1.Rows.AddNew()
        LoadDocType()
        MCCLOCATIONFINDER()
    End Sub
    Sub blankAllControl()

        isNewEntry = True
        isCellValueChangedOpen = False
        txtDocNo.Value = ""
        txtDate.Text = clsCommon.GETSERVERDATE
        txtVehicleCode.Value = ""
        lblVehicleCode.Text = ""
        lblVendorName.Text = ""

        txtManualVehicle.Text = ""
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        txtBillToLocation.Value = ""
        lblBillToLocation.Text = ""
        txtComment.Text = ""
        txtRemarks.Text = ""
        txtTransporterCode.Value = ""
        lblTransportName.Text = ""
        txtManualTransport.Text = ""
        txtBillToLocation.Enabled = True
        txtVendorNo.Enabled = True
        ddlDocType.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtmulticapex.arrValueMember = Nothing
        txtmulticapex.arrDispalyMember = Nothing
        chkCancel.Checked = False
        chkCancel.Enabled = False
        btnCancel.Enabled = False
        btnSave.Text = "Save"

    End Sub
    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colItemCode).Value = ""
        gv1.CurrentRow.Cells(colItemDesc).Value = ""
        gv1.CurrentRow.Cells(colHSNNo).Value = ""
        gv1.CurrentRow.Cells(colUnit).Value = ""
        gv1.CurrentRow.Cells(colQty).Value = 0
    End Sub
    
    Sub OpenItemForFreshSale(ByVal strCode As String, ByVal isButtonClicked As Boolean)
        WhrCls = Nothing
        WhrCls = " is_freshitem=1 "
        strCode = clsItemMaster.getFinder(WhrCls, strCode, isButtonClicked)
        gv1.CurrentRow.Cells(colItemCode).Value = strCode
    End Sub
    Sub OpenItemForProductSale(ByVal strCode As String, ByVal isButtonClicked As Boolean)
        WhrCls = Nothing
        WhrCls = "Is_FreshItem=0 and Product_Type not in ('MI')  and Item_Type in ('F','T','S') and Is_Serial_Item=0 "
        gv1.CurrentRow.Cells(colItemCode).Value = clsItemMaster.getFinder(WhrCls, strCode, isButtonClicked)
    End Sub
    Sub OpenItemForMISSale(ByVal strCode As String, ByVal isButtonClicked As Boolean)
        Dim qry As String = "select TSPL_ITEM_MASTER.Item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as Name from  TSPL_ITEM_MASTER"
        qry += " left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code =TSPL_ITEM_MASTER.item_category "
        qry += "  left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code  =TSPL_ITEM_MASTER.Sub_item_category "
        gv1.CurrentRow.Cells(colItemCode).Value = clsCommon.ShowSelectForm("ItemFinderForMISSale", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)

    End Sub
    Sub OpenItemForMCCSale(ByVal strCode As String, ByVal isButtonClicked As Boolean)
        Dim qry As String = Nothing
        WhrCls = Nothing
        qry = " select a.DESCRIPTION,a.cat_value, TSPL_ITEM_MASTER.item_code as Item,TSPL_ITEM_MASTER.item_desc as [ItemDesc]," _
            & " TSPL_ITEM_MASTER.Unit_Code as Unit , TSPL_ITEM_MASTER.Rate as BasicRate,TSPL_ITEM_MASTER.rate as MRP, Weight_Value as [Weight Value] from " _
            & " TSPL_ITEM_MASTER    left outer join (select TSPL_ITEM_MASTER_CATEGORY.Item_code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code," _
            & " TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as cat_value " _
            & " from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=" _
            & " TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and ISNULL(TSPL_ITEM_CATEGORY_LEVEL.Form_Type,'item')='item' left outer join " _
            & " TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code" _
            & " and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  and ISNULL(TSPL_ITEM_CATEGORY_LEVEL_VALUES.Form_Type,'item')='item')a" _
            & " on a.Item_code=TSPL_ITEM_MASTER.Item_Code and TSPL_ITEM_MASTER.item_code=a.item_code "
            

        WhrCls = "TSPL_ITEM_MASTER.Active=1 and Is_FreshItem=0 and coalesce(Product_Type,'') not in ('MI') and Item_Type not in ('A') and coalesce(Item_used_as,'')='S' "
        gv1.CurrentRow.Cells(colItemCode).Value = clsCommon.ShowSelectForm("ItemFinderForMCCSale", qry, "Item", WhrCls, strCode, "Item", isButtonClicked)
    End Sub
    Sub OpenItemForBulkSale(ByVal strCode As String, ByVal isButtonClicked As Boolean)
        WhrCls = Nothing
        Dim qry As String = "Select Item_Code as Code,Item_Desc as Description from TSPL_ITEM_MASTER"
        WhrCls = "Product_Type ='MI' and Active=1"
        gv1.CurrentRow.Cells(colItemCode).Value = clsCommon.ShowSelectForm("SalesOrderItemFinderbs", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
    End Sub
    Sub OpenItemForExportSale(ByVal strCode As String, ByVal isButtonClicked As Boolean)
        WhrCls = Nothing
        WhrCls = " isnull(Is_FreshItem,0)<>1 and isnull(active,0)=1  "
        gv1.CurrentRow.Cells(colItemCode).Value = clsItemMaster.getFinder(WhrCls, strCode, isButtonClicked)
    End Sub
    Sub OpenItemForCSATransfer(ByVal strCode As String, ByVal isButtonClicked As Boolean)
        gv1.CurrentRow.Cells(colItemCode).Value = clsItemMaster.getFinder("active=1 and tspl_item_master.item_type in ('F','T') and isnull(Is_FreshItem,0)<>1", strCode, False)
    End Sub
    Sub disableControl()
        ddlDocType.Enabled = False
        txtVendorNo.Enabled = False
        txtBillToLocation.Enabled = False
    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        If clsCommon.CompairString(ddlDocType.SelectedValue, "") = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow("Please select Doc Type")
            ddlDocType.Focus()
            Exit Sub
        ElseIf clsCommon.myLen(txtVendorNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Customer")
            txtVendorNo.Focus()
            Exit Sub
        ElseIf clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Location")
            txtBillToLocation.Focus()
            Exit Sub
        End If

        If clsCommon.CompairString(ddlDocType.SelectedValue, "FS") = CompairStringResult.Equal Then
            OpenItemForFreshSale(clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), isButtonClick)
        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "PS") = CompairStringResult.Equal Then
            OpenItemForProductSale(clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), isButtonClick)
        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "MISS") = CompairStringResult.Equal Then
            OpenItemForMISSale(clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), isButtonClick)
        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "MCCS") = CompairStringResult.Equal Then
            OpenItemForMCCSale(clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), isButtonClick)
        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "BS") = CompairStringResult.Equal Then
            OpenItemForBulkSale(clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), isButtonClick)
        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "EXPS") = CompairStringResult.Equal Then
            OpenItemForExportSale(clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), isButtonClick)
        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "CSATRAN") = CompairStringResult.Equal Then
            OpenItemForCSATransfer(clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), isButtonClick)
        End If
        If clsCommon.myLen(gv1.CurrentRow.Cells(colItemCode).Value) > 0 Then
            gv1.CurrentRow.Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" & gv1.CurrentRow.Cells(colItemCode).Value & "' ")
            gv1.CurrentRow.Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(gv1.CurrentRow.Cells(colItemCode).Value, Nothing)
            gv1.CurrentRow.Cells(colUnit).Value = clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Default_UOM=1 and Item_Code='" & gv1.CurrentRow.Cells(colItemCode).Value & "' ")
        Else
            SetBlankOfItemColumns()
        End If
        disableControl()
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("FinderUnit", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)

        End If
    End Sub

    Private Sub txtVehicleCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVehicleCode._MYValidating
        Dim qry As String = Nothing
        Dim WhrCls As String = Nothing
        If clsCommon.CompairString(ddlDocType.SelectedValue, "") = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow("Please select Doc Type")
            ddlDocType.Focus()
            Exit Sub
        End If
        If clsCommon.CompairString(ddlDocType.SelectedValue, "FS") = CompairStringResult.Equal Then
            qry = "Select distinct  vehicle_id ,Description from TSPL_VEHICLE_MASTER"
            txtVehicleCode.Value = clsCommon.ShowSelectForm("SRBSVehicleNoFS", qry, "vehicle_id", "", txtVehicleCode.Value, "vehicle_id", isButtonClicked)
            If clsCommon.myLen(txtVehicleCode.Value) > 0 Then
                lblVehicleCode.Text = connectSql.RunScalar("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(txtVehicleCode.Value) + "'")
            Else
                lblVehicleCode.Text = ""
            End If


        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "PS") = CompairStringResult.Equal Then
            qry = "select Vehicle_Id as[Code],Description as [Description] from TSPL_VEHICLE_MASTER"
            txtVehicleCode.Value = clsCommon.ShowSelectForm("SRBSvehiclePS", qry, "Code", "", txtVehicleCode.Value, "", isButtonClicked)
            If clsCommon.myLen(txtVehicleCode.Value) > 0 Then
                lblVehicleCode.Text = connectSql.RunScalar("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(txtVehicleCode.Value) + "'")
            Else
                lblVehicleCode.Text = ""
            End If

        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "MISS") = CompairStringResult.Equal Then
            qry = "Select Segment_code as Code, Description from TSPL_GL_SEGMENT_CODE "
            WhrCls = "Seg_No=2"
            txtVehicleCode.Value = clsCommon.ShowSelectForm("SRBSVehicleFNDMISS", qry, "Code", WhrCls, txtVehicleCode.Value, "Code", isButtonClicked)
            If clsCommon.myLen(txtVehicleCode.Value) > 0 Then
                lblVehicleCode.Text = ClsScrapSaleHead.GetVehicleDesc(txtVehicleCode.Value, Nothing)
            Else
                lblVehicleCode.Text = ""
            End If

        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "MCCS") = CompairStringResult.Equal Then


        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "BS") = CompairStringResult.Equal Then

        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "EXPS") = CompairStringResult.Equal Then

        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "CSATRAN") = CompairStringResult.Equal Then

        End If
        DisableTpe()

    End Sub

    Private Sub txtBillToLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBillToLocation._MYValidating
        Dim qry As String = Nothing
        Dim WhrCls As String = Nothing
        'txtBillToLocation.Value = ""
        'lblBillToLocation.Text = ""
        If clsCommon.CompairString(ddlDocType.SelectedValue, "") = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow("Please select Doc Type")
            ddlDocType.Focus()
            Exit Sub
        End If
        If clsCommon.CompairString(ddlDocType.SelectedValue, "FS") = CompairStringResult.Equal Then
            qry = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            WhrCls = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtBillToLocation.Value = clsCommon.ShowSelectForm("SRGEFSFINDERFS", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)



        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "PS") = CompairStringResult.Equal Then
            qry = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            WhrCls = "  Location_Type='Physical' and CSA_Type='N' and Is_Section='N' and Is_Sub_Location='N' and (GIT_Type='' or GIT_Type='N') and MCC_Type='N' "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtBillToLocation.Value = clsCommon.ShowSelectForm("PS-SRGEFSFINDERPS", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)


        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "MISS") = CompairStringResult.Equal Then
            qry = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            WhrCls = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtBillToLocation.Value = clsCommon.ShowSelectForm("SRGEFSFINDERMISS", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)


        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "MCCS") = CompairStringResult.Equal Then
            qry = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            WhrCls = "  Location_Type='Physical' and CSA_Type='N' and Is_Section='N' and Is_Sub_Location='N' "
            If AllowPlandDeptMCCLocation Then
                WhrCls += " and (GIT_Type='' or GIT_Type='N') "
            End If
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ") "
            End If
            If AllowPlandDeptMCCLocation = False Then
                WhrCls += "  and location_category='MCC' and  Location_Code in (" + MCCLOCATIONFINDER() + ")"
            End If

            txtBillToLocation.Value = clsCommon.ShowSelectForm("SRGEFSFINDERMCCS", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)


        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "BS") = CompairStringResult.Equal Then

            txtBillToLocation.Value = clsLocation.getFinder(" (Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' and TSPL_LOCATION_MASTER.Location_Code in (" + MCCLOCATIONFINDER() + ")) or Location_Type='Virtual' ", txtBillToLocation.Value, isButtonClicked)


        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "EXPS") = CompairStringResult.Equal Then
            If clsCommon.myLen(MCCLOCATIONFINDER) <= 0 Then
                clsCommon.MyMessageBoxShow("No location rights.")
                Exit Sub
            End If

            qry = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            WhrCls = "  Location_Type='Physical'  "

            If clsCommon.myLen(MCCLOCATIONFINDER) > 0 Then
                WhrCls += "  and  Location_Code in (" + MCCLOCATIONFINDER() + ")"
            End If


            txtBillToLocation.Value = clsCommon.ShowSelectForm("EXCOMMLOCFND", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)

        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "CSATRAN") = CompairStringResult.Equal Then
            WhrCls = " coalesce(ltrim(Rejected_Type),'') in ('N','') and coalesce(ltrim(GIT_Type),'') in ('N','') and Is_Section='N' and Is_Sub_Location='N'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            WhrCls += " and ( (Excisable ='T') or ( Excisable <> 'T') )"
            txtBillToLocation.Value = clsLocation.getFinder(WhrCls, txtBillToLocation.Value, isButtonClicked)

        End If
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))
        Else
            lblBillToLocation.Text = ""
        End If
        DisableTpe()
    End Sub

    Private Sub txtVendorNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVendorNo._MYValidating
        Dim qry As String = ""
        Dim whrcls As String = ""
        Dim strwherecls As String = ""
        
        If clsCommon.CompairString(ddlDocType.SelectedValue, "") = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow("Please select Doc Type")
            ddlDocType.Focus()
            Exit Sub
        End If
        ' Ticket No : KDI/11/05/18-000309 By Prabhakar

        qry = " select Cust_Code as Code,Customer_Name as Name  from TSPL_CUSTOMER_MASTER "
        txtVendorNo.Value = clsCommon.ShowSelectForm("SRGECust", qry, "Code", "", txtVendorNo.Value, "Code", isButtonClicked)

        'If clsCommon.CompairString(ddlDocType.SelectedValue, "FS") = CompairStringResult.Equal Then

        'strwherecls = Xtra.CustomerPermission()

        '    qry = " select Cust_Code as Code,Customer_Name as Name "
        '    qry += " from TSPL_CUSTOMER_MASTER "
        '    qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
        '    qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
        '    qry += " left outer join TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER on TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code"
        '    qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'"

        '    If clsCommon.myLen(strwherecls) = 0 Then
        '        txtVendorNo.Value = clsCommon.ShowSelectForm("SRGECust", qry, "Code", "", txtVendorNo.Value, "Code", isButtonClicked)
        '    Else
        '        txtVendorNo.Value = clsCommon.ShowSelectForm("SRGECust", qry, "Code", " TSPL_CUSTOMER_MASTER.Cust_Code in (" + strwherecls + ")", txtVendorNo.Value, "Code", isButtonClicked)
        '    End If

        'ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "PS") = CompairStringResult.Equal Then

        '    strwherecls = Xtra.CustomerPermission()
        '    '-----------------------------------------------------
        '    qry = "select Cust_Code as Code,Customer_Name as Name "
        '    qry += " from TSPL_CUSTOMER_MASTER "
        '    qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
        '    qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
        '    qry += " left outer join TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER on TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code"
        '    qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' " & _
        '    "left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No  " & _
        '    "left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id "

        '    If clsCommon.myLen(strwherecls) = 0 Then
        '        txtVendorNo.Value = clsCommon.ShowSelectForm("PS-DOCustFndr", qry, "Code", "TSPL_CUSTOMER_MASTER.Status='N' and (Parent_Customer_No='" & strParentCust & "'  or  Cust_Code='" & strParentCust & "' )", txtVendorNo.Value, "Code", isButtonClicked)
        '    Else
        '        txtVendorNo.Value = clsCommon.ShowSelectForm("PS-DOCustFndr", qry, "Code", " TSPL_CUSTOMER_MASTER.Status='N' and TSPL_CUSTOMER_MASTER.Cust_Code in (" + strwherecls + ") and (Parent_Customer_No='" & strParentCust & "'  or  Cust_Code='" & strParentCust & "' )  ", txtVendorNo.Value, "Code", isButtonClicked)

        '    End If

        'ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "MISS") = CompairStringResult.Equal Then
        '    qry = "  select TSPL_customer_MASTER.cust_code as Code,TSPL_customer_MASTER.Customer_Name as Name from TSPL_customer_MASTER  left outer join  TSPL_TERMS_MASTER on TSPL_customer_MASTER.Terms_Code=TSPL_TERMS_MASTER.Terms_Code  left outer join  TSPL_TAX_GROUP_MASTER on TSPL_customer_MASTER.Tax_Group=TSPL_TAX_GROUP_MASTER.Tax_Group_Code "
        '    whrcls = "TSPL_TAX_GROUP_MASTER.Tax_Group_Type='s' and  TSPL_customer_MASTER.Status ='N'"
        '    txtVendorNo.Value = clsCommon.ShowSelectForm("CustmrMstrIFND", qry, "Code", whrcls, txtVendorNo.Value, "Code", isButtonClicked)


        'ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "MCCS") = CompairStringResult.Equal Then
        '    strwherecls = MyBase.Cust_CustomerVendorMapping()
        '    If clsCommon.myLen(strwherecls) <= 0 Then
        '        clsCommon.MyMessageBoxShow("No Customer Found")
        '        Exit Sub
        '    End If
        '    qry = " select TSPL_CUSTOMER_MASTER.Cust_Code as Code,Customer_Name as Name "
        '    qry += " from TSPL_CUSTOMER_MASTER "
        '    qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
        '    qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
        '    qry += " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code"
        '    qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'" & _
        '    "left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No  " & _
        '    "left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id  " & _
        '    " left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
        '    " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code " & _
        '    " left join TSPL_VLC_MASTER_HEAD on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code "

        '    whrcls = " (TSPL_CUSTOMER_MASTER.Status = 'N') or ( TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code is null and TSPL_VENDOR_MASTER.Is_Inactive_In_Milk_Procurement=0) or (TSPL_CUSTOMER_MASTER.Cust_Code in (" + strwherecls + ") and TSPL_VENDOR_MASTER.Is_Inactive_In_Milk_Procurement=0)"

        '    txtVendorNo.Value = clsCommon.ShowSelectForm("MCC Customer Lists", qry, "Code", " TSPL_CUSTOMER_MASTER.Status = 'N' ", txtVendorNo.Value, "Code", isButtonClicked)


        'ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "BS") = CompairStringResult.Equal Then
        '    txtVendorNo.Value = clsCustomerMaster.getFinder("", txtVendorNo.Value, isButtonClicked)

        'ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "EXPS") = CompairStringResult.Equal Then
        '    strwherecls = Xtra.CustomerPermission()

        '    qry = "select Cust_Code as Code,Customer_Name as Name"
        '    qry += " from TSPL_CUSTOMER_MASTER "
        '    qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
        '    qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
        '    qry += " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code"
        '    qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'"


        'If clsCommon.myLen(strwherecls) = 0 Then
        '    txtVendorNo.Value = clsCommon.ShowSelectForm("EXCOMVDNFND", qry, "Code", " ISNULL(TSPL_CUSTOMER_MASTER.currency_code,'') not in ('','" + objCommonVar.BaseCurrencyCode + "')", txtVendorNo.Value, "Code", isButtonClicked)
        'Else
        '    txtVendorNo.Value = clsCommon.ShowSelectForm("EXCOMVNDFND", qry, "Code", " ISNULL(TSPL_CUSTOMER_MASTER.currency_code,'') not in ('','" + objCommonVar.BaseCurrencyCode + "') and TSPL_CUSTOMER_MASTER.Cust_Code in (" + strwherecls + ")", txtVendorNo.Value, "Code", isButtonClicked)

        'End If
        'ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "CSATRAN") = CompairStringResult.Equal Then
        'txtVendorNo.Value = clsCustomerMaster.getFinder(" isnull(tspl_customer_master.csa_type,'N')='Y' ", txtVendorNo.Value, isButtonClicked)
        'End If
        If clsCommon.myLen(txtVendorNo.Value) > 0 Then
            lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" + txtVendorNo.Value + "'"))
        Else
            lblVendorName.Text = ""
        End If
        DisableTpe()


    End Sub
    Function AllowToSave() As Boolean
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Focus()
            Return False
        End If
        If clsCommon.CompairString(ddlDocType.SelectedValue, "") = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow("Please select Doc Type")
            ddlDocType.Focus()
            Return False
        ElseIf clsCommon.myLen(txtVendorNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Customer")
            txtVendorNo.Focus()
            Return False
        ElseIf clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Location")
            txtBillToLocation.Focus()
            Return False

        End If
        If clsCommon.CompairString(ddlDocType.SelectedValue, "FS") = CompairStringResult.Equal Then
            If clsCommon.myLen(txtVehicleCode.Value) <= 0 AndAlso clsCommon.myLen(txtManualVehicle.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Vehicle")
                txtVehicleCode.Focus()
                txtManualVehicle.Focus()
                Return False
            End If

        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "PS") = CompairStringResult.Equal Then
            If clsCommon.myLen(txtVehicleCode.Value) <= 0 AndAlso clsCommon.myLen(txtManualVehicle.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Vehicle")
                txtVehicleCode.Focus()
                txtManualVehicle.Focus()
                Return False
            End If

        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "MISS") = CompairStringResult.Equal Then
            If clsCommon.myLen(txtVehicleCode.Value) <= 0 AndAlso clsCommon.myLen(txtManualVehicle.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Vehicle")
                txtVehicleCode.Focus()
                txtManualVehicle.Focus()
                Return False
            End If
        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "MCCS") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlDocType.SelectedValue, "BS") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlDocType.SelectedValue, "EXPS") = CompairStringResult.Equal Then
            If clsCommon.myLen(txtManualVehicle.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Vehicle")
                txtManualVehicle.Focus()
                Return False
            End If

        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "CSATRAN") = CompairStringResult.Equal Then
            If clsCommon.myLen(txtManualVehicle.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Vehicle")
                txtManualVehicle.Focus()
                Return False
            End If
        End If
        '==================update by preeti Gupta against Ticket No[KDI/03/05/18-000290]'================
        If clsCommon.myLen(txtManualTransport.Text) <= 0 AndAlso clsCommon.myLen(txtTransporterCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Transport")
            txtTransporterCode.Focus()
            Return False
        End If
        '=======================================Detail level=========================
        Dim arrICode As New List(Of String)()
        For ii As Integer = 0 To gv1.Rows.Count - 1

            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value)
            Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemDesc).Value)
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim HSNCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colHSNNo).Value)
            If clsCommon.myLen(gv1.Rows(ii).Cells(colItemCode).Value) > 0 Then
                If clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Fill Quantity at row no. " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + "")
                    Return False
                End If
                If clsCommon.myLen(gv1.Rows(ii).Cells(colUnit).Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please enter UOM for Item : " + strIName + " . At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                    Return False
                End If
                'If clsCommon.myLen(HSNCode) <= 0 Then
                '    clsCommon.MyMessageBoxShow("HSN Code is Mandatory. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                '    Return False
                'End If
            End If
            '===============
            For jj As Integer = 0 To gv1.Rows.Count - 1
                If jj = ii Then
                    Continue For
                End If
                Dim strInnerICode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colItemCode).Value)
                Dim strInnerUOM As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)
                If clsCommon.myLen(gv1.Rows(ii).Cells(colItemCode).Value) > 0 Then
                    If clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, strInnerUOM) = CompairStringResult.Equal Then
                        Dim Msg As String = "Same Item Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                        Msg = Msg + Environment.NewLine + "Item: " + strICode + "(" + strIName + ")"
                        Msg = Msg + Environment.NewLine + "UOM: " + strUOM
                        common.clsCommon.MyMessageBoxShow(Msg)
                        Return False
                    End If
                End If
                
            Next
            '===============
        Next


        Return True
    End Function
    Sub SaveData(ByVal ChekPostBtn As Boolean)
        Try

            'If ChekPostBtn = False Then
            '    If FrmMainTranScreen.ValidateTransactionAccToFinYear("Sale Return", txtDate.Value) = False Then
            '        Exit Sub
            '    End If
            'End If
            ''
            If (AllowToSave()) Then


                Dim obj As New clssaleReturnGateEntryHead()
                obj.Gate_Entry_No = txtDocNo.Value
                obj.Gate_Entry_Date = txtDate.Text
                obj.Vehicle_Code = txtVehicleCode.Value
                obj.Vehicle_Desc = lblVendorName.Text
                obj.Man_Vehicle_Code = txtManualVehicle.Text
                obj.Location_Code = txtBillToLocation.Value
                obj.Location_Desc = lblBillToLocation.Text
                obj.Transport = txtTransporterCode.Value
                obj.Transporter_Name = lblTransportName.Text
                obj.Man_Transport = txtManualTransport.Text
                obj.Doc_Type = ddlDocType.SelectedValue
                obj.Customer_Code = txtVendorNo.Value
                obj.Customer_Name = lblVendorName.Text
                obj.Remarks = txtRemarks.Text
                obj.Comment = txtComment.Text
                If chkCancel.Checked = True Then
                    obj.isCancel = 1
                Else
                    obj.isCancel = 0
                End If
                If obj.POSTED = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If

                obj.Arr = New List(Of clssaleReturnGateEntryDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clssaleReturnGateEntryDetail()
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                        objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                        objTr.HSN = clsCommon.myCstr(grow.Cells(colHSNNo).Value)
                        objTr.UOM = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            obj.Arr.Add(objTr)
                        End If
                    End If

                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                    Return
                End If

                obj.ArrInvoice = New List(Of clssaleReturnGateEntryInvoice)
                If clsCommon.myLen(txtmulticapex.arrValueMember) > 0 Then
                    For Each item As String In txtmulticapex.arrValueMember
                        Dim objTrTr As New clssaleReturnGateEntryInvoice()
                        objTrTr.Gate_Entry_No = clsCommon.myCstr(txtDocNo.Value)
                        objTrTr.Invoice_No = item
                        If (clsCommon.myLen(objTrTr.Invoice_No) > 0) Then
                            obj.ArrInvoice.Add(objTrTr)
                        End If
                    Next


                End If
                
                If (obj.SaveData(obj, isNewEntry)) Then

                    If ChekPostBtn = False Then
                        common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    End If
                    LoadData(obj.Gate_Entry_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try

            Dim obj As New clssaleReturnGateEntryHead()
            obj = clssaleReturnGateEntryHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Gate_Entry_No) > 0) Then
                blankAllControl()
                isNewEntry = False
                LoadBlankGrid()
                isInsideLoadData = True
                btnSave.Text = "Update"

                txtDocNo.Value = obj.Gate_Entry_No
                txtDate.Text = obj.Gate_Entry_Date
                txtVehicleCode.Value = obj.Vehicle_Code
                lblVendorName.Text = obj.Vehicle_Desc
                txtManualVehicle.Text = obj.Man_Vehicle_Code
                txtBillToLocation.Value = obj.Location_Code
                lblBillToLocation.Text = obj.Location_Desc
                txtTransporterCode.Value = obj.Transport
                lblTransportName.Text = obj.Transporter_Name
                txtManualTransport.Text = obj.Man_Transport
                ddlDocType.SelectedValue = obj.Doc_Type
                txtVendorNo.Value = obj.Customer_Code
                lblVendorName.Text = obj.Customer_Name
                txtRemarks.Text = obj.Remarks
                txtComment.Text = obj.Comment
                If obj.POSTED = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If
                UsLock1.Status = obj.POSTED
                If obj.POSTED = 1 Then
                    chkCancel.Enabled = True
                    btnCancel.Enabled = True
                Else
                    chkCancel.Enabled = False
                    btnCancel.Enabled = False
                End If
                If obj.isCancel = 1 Then
                    chkCancel.Checked = 1
                    chkCancel.Enabled = False
                    btnCancel.Enabled = False
                End If
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clssaleReturnGateEntryDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = objTr.HSN
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                    Next

                End If
                Dim DocCode As New ArrayList
                If obj.ArrInvoice IsNot Nothing AndAlso obj.ArrInvoice.Count > 0 Then
                    For Each ob As clssaleReturnGateEntryInvoice In obj.ArrInvoice
                        DocCode.Add(ob.Invoice_No)
                    Next
                    txtmulticapex.arrValueMember = DocCode
                End If
                
               

                disableControl()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Sub CloseForm()
        Me.Close()

    End Sub


    Private Sub FrmsaleReturnGateEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        AllowPlandDeptMCCLocation = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.Allow_Plant_Depot_MCC_typeLocation, clsFixedParameterCode.Allow_Plant_Depot_MCC_typeLocation, Nothing)) = "1", True, False))
        reset()
    End Sub

    Private Sub FrmsaleReturnGateEntry_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                              "TSPL_Sale_Return_Gate_Entry_Head " + Environment.NewLine + _
                                              "TSPL_Sale_Return_Gate_Entry_Detail " + Environment.NewLine + _
                                              "TSPL_Sale_Return_Gate_Entry_Invoice_Wise ")
        End If
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            SaveData(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            If (myMessages.postConfirm()) Then
                SaveData(True)
                If (clssaleReturnGateEntryHead.PostData(txtDocNo.Value)) Then ''pass schedule value for creating auto schedule
                    msg = "Successfully Posted"

                End If
                If clsCommon.myLen(msg) > 0 Then
                    common.clsCommon.MyMessageBoxShow(msg)
                End If
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then

                If (clssaleReturnGateEntryHead.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Gate Entry No not found to Print", Me.Text)
                Return
            End If
            Dim qry As String = "  select TSPL_Sale_Return_Gate_Entry_Detail.Gate_Entry_No , convert(varchar, TSPL_Sale_Return_Gate_Entry_Head.Gate_Entry_Date, 103) as Gate_Entry_Date , case when len(TSPL_Sale_Return_Gate_Entry_Head.Vehicle_Code) > 0 then  TSPL_Sale_Return_Gate_Entry_Head.Vehicle_Code else  TSPL_Sale_Return_Gate_Entry_Head. Man_Vehicle_code end as Vehicle_Code , " & _
                                "  TSPL_Sale_Return_Gate_Entry_Head.Location_Code,case when len ( TSPL_Sale_Return_Gate_Entry_Head.Transport) > 0 then TSPL_Sale_Return_Gate_Entry_Head.Transport else TSPL_Sale_Return_Gate_Entry_Head.Man_Transport end as  Man_Transport, case when TSPL_Sale_Return_Gate_Entry_Head.Doc_Type = 'PS' then 'Product Sale Return' when TSPL_Sale_Return_Gate_Entry_Head.Doc_Type = 'FS' then 'Fresh Sale Return' when TSPL_Sale_Return_Gate_Entry_Head.Doc_Type  = 'MCCS' then 'MCC Material Sale Return'  when TSPL_Sale_Return_Gate_Entry_Head.Doc_Type  ='CSA' then 'CSA Transfer Return' when TSPL_Sale_Return_Gate_Entry_Head.Doc_Type  ='EXP' then 'Export Sale Return' when TSPL_Sale_Return_Gate_Entry_Head.Doc_Type  =  'MISS' then 'Material Sales Return' else '' end as Doc_Type, TSPL_Sale_Return_Gate_Entry_Head.Doc_Type as Doc_Type2 , TSPL_Sale_Return_Gate_Entry_Head.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_Sale_Return_Gate_Entry_Head.Remarks , TSPL_Sale_Return_Gate_Entry_Head.Comment , TSPL_Sale_Return_Gate_Entry_Head.Created_By, convert(varchar,TSPL_Sale_Return_Gate_Entry_Head.Created_Date,103) as Created_Date , TSPL_Sale_Return_Gate_Entry_Head.Modify_By, convert(varchar,TSPL_Sale_Return_Gate_Entry_Head.Modify_Date,103) as Modify_Date, TSPL_Sale_Return_Gate_Entry_Head.Comp_Code, TSPL_Sale_Return_Gate_Entry_Head.Posted, TSPL_Sale_Return_Gate_Entry_Head.isCancel, convert (varchar,TSPL_Sale_Return_Gate_Entry_Head.Cancel_Date,103) as Cancel_Date ,TSPL_Sale_Return_Gate_Entry_Detail.Line_No , TSPL_Sale_Return_Gate_Entry_Detail.Item_Code,TSPL_ITEM_MASTER.Item_Desc, TSPL_Sale_Return_Gate_Entry_Detail.UOM, TSPL_Sale_Return_Gate_Entry_Detail.Qty  from TSPL_Sale_Return_Gate_Entry_Detail  " & _
                                "  inner join  TSPL_Sale_Return_Gate_Entry_Head on TSPL_Sale_Return_Gate_Entry_Detail.Gate_Entry_No = TSPL_Sale_Return_Gate_Entry_Head.Gate_Entry_No " & _
                                "  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code= TSPL_Sale_Return_Gate_Entry_Detail.Item_Code " & _
                                "  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_Sale_Return_Gate_Entry_Head.Customer_Code " & _
                                "   where TSPL_Sale_Return_Gate_Entry_Head.Gate_Entry_No = '" + txtDocNo.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptGateNoEntry", "Sale Return Gate Entry No", txtDate.Value, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                frmCRV = Nothing
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try
        

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Private Sub txtTransporterCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTransporterCode._MYValidating
        Dim qry As String = Nothing
        Dim WhrCls As String = "TSPL_VENDOR_MASTER.Transporter   ='Y'"
       
        If clsCommon.CompairString(ddlDocType.SelectedValue, "") = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow("Please select Doc Type")
            ddlDocType.Focus()
            Exit Sub
        End If
        If clsCommon.CompairString(ddlDocType.SelectedValue, "FS") = CompairStringResult.Equal Then


        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "PS") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlDocType.SelectedValue, "MISS") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlDocType.SelectedValue, "MCCS") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlDocType.SelectedValue, "CSATRAN") = CompairStringResult.Equal Then
            qry = "select TSPL_VENDOR_MASTER.Vendor_Code as Transport_Id ,TSPL_VENDOR_MASTER.Vendor_Name as Transporter_Name from TSPL_VENDOR_MASTER "
            txtTransporterCode.Value = clsCommon.ShowSelectForm("SRGETransportNoPS", qry, "Transport_Id", WhrCls, txtTransporterCode.Value, "Transport_Id", isButtonClicked)

        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "BS") = CompairStringResult.Equal Then
        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "EXPS") = CompairStringResult.Equal Then
        End If
        

        If clsCommon.myLen(txtTransporterCode.Value) > 0 Then
            lblTransportName.Text = clsTransferDCC.GetTransporterName(txtTransporterCode.Value)
        Else
            lblTransportName.Text = ""
        End If
        DisableTpe()
    End Sub
    Sub SelectInvoiceItems()
        LoadBlankGrid()
        Dim objMRNHead As clsSalesReturnFreshSale = Nothing
        Dim objMisSaleHead As ClsScrapSaleHead = Nothing
        Dim objBulkSaleHead As ClsInvoiceBulkSale = Nothing
        isInsideLoadData = True
        Dim frm As New frmPendingSaleReturnGateEntry()
        frm.VendorCode = txtVendorNo.Value
        frm.strCurrCode = txtDocNo.Value
        frm.DocType = ddlDocType.SelectedValue
        frm.LocationCode = txtBillToLocation.Value
        Dim DocCode As New ArrayList
        frm.ShowDialog()
        If clsCommon.CompairString(ddlDocType.SelectedValue, "FS") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlDocType.SelectedValue, "PS") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlDocType.SelectedValue, "EXPS") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlDocType.SelectedValue, "MCCS") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlDocType.SelectedValue, "CSATRAN") = CompairStringResult.Equal Then
            If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
                If clsCommon.myLen(frm.ArrReturn(0).Document_Code) > 0 Then
                    objMRNHead = clsSalesReturnFreshSale.GetData(frm.ArrReturn(0).Document_Code, NavigatorType.Current)
                    If objMRNHead IsNot Nothing AndAlso clsCommon.myLen(objMRNHead.Document_Code) > 0 Then
                        LoadBlankGrid()
                    End If
                    'txtmulticapex.arrValueMember = objMRNHead.Document_Code
                End If

                For Each obj As clsSalesReturnFreshSaleDetail In frm.ArrReturn

                    Dim isFound As Boolean = False
                    If Not DocCode.Contains(obj.Document_Code) Then
                        DocCode.Add(obj.Document_Code)
                    End If
                    txtmulticapex.arrValueMember = DocCode

                    For ii As Integer = 0 To gv1.Rows.Count - 1

                        If clsCommon.CompairString(obj.Item_Code, clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Unit_code, clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)) = CompairStringResult.Equal Then
                            gv1.Rows(ii).Cells(colQty).Value = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) + obj.Qty
                            isFound = True
                            Exit For
                        End If

                        'txtmulticapex.arrValueMember 


                    Next
                    If Not isFound Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count - 1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = obj.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = obj.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.Qty
                    End If

                Next
            End If
        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "MISS") = CompairStringResult.Equal Then
            If frm.ArrMisSaleReturn IsNot Nothing AndAlso frm.ArrMisSaleReturn.Count > 0 Then
                If clsCommon.myLen(frm.ArrMisSaleReturn(0).shipment_No) > 0 Then
                    objMisSaleHead = ClsScrapSaleHead.GetData(frm.ArrMisSaleReturn(0).shipment_No, NavigatorType.Current)
                    If objMisSaleHead IsNot Nothing AndAlso clsCommon.myLen(objMisSaleHead.shipment_No) > 0 Then
                        LoadBlankGrid()
                    End If

                End If

                For Each obj As ClsScrapSaleDetail In frm.ArrMisSaleReturn
                    If Not DocCode.Contains(obj.shipment_No) Then
                        DocCode.Add(obj.shipment_No)
                    End If
                    txtmulticapex.arrValueMember = DocCode
                    Dim isFound As Boolean = False

                    For ii As Integer = 0 To gv1.Rows.Count - 1

                        If clsCommon.CompairString(obj.Item_Code, clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Unit_Code, clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)) = CompairStringResult.Equal Then
                            gv1.Rows(ii).Cells(colQty).Value = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) + obj.shipped_Qty
                            isFound = True
                            Exit For
                        End If

                    Next
                    If Not isFound Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count - 1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = obj.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = obj.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.shipped_Qty
                    End If

                Next
            End If
        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "BS") = CompairStringResult.Equal Then
            If frm.ArrBulkSaleReturn IsNot Nothing AndAlso frm.ArrBulkSaleReturn.Count > 0 Then
                If clsCommon.myLen(frm.ArrBulkSaleReturn(0).Document_No) > 0 Then
                    objBulkSaleHead = ClsInvoiceBulkSale.GetData(frm.ArrBulkSaleReturn(0).Document_No, "", NavigatorType.Current)
                    If objBulkSaleHead IsNot Nothing AndAlso clsCommon.myLen(objBulkSaleHead.Document_No) > 0 Then
                        LoadBlankGrid()
                    End If

                End If

                For Each obj As ClsInvoiceDetailBulkSale In frm.ArrBulkSaleReturn
                    If Not DocCode.Contains(obj.Document_No) Then
                        DocCode.Add(obj.Document_No)
                    End If
                    txtmulticapex.arrValueMember = DocCode
                    Dim isFound As Boolean = False

                    For ii As Integer = 0 To gv1.Rows.Count - 1

                        If clsCommon.CompairString(obj.Item_Code, clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Unit_code, clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)) = CompairStringResult.Equal Then
                            gv1.Rows(ii).Cells(colQty).Value = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) + obj.InvoiceQty
                            isFound = True
                            Exit For
                        End If

                    Next
                    If Not isFound Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count - 1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = obj.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsItemMaster.GetItemName(obj.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.InvoiceQty
                    End If

                Next
            End If
        End If
       
        isInsideLoadData = False

    End Sub

   
    Private Sub ddlDocType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlDocType.SelectedIndexChanged
        EnableDisableControl()

    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colItemCode) Then
                        OpenICodeList(False)
                    ElseIf e.Column Is gv1.Columns(colUnit) Then
                        OpenUOMList(False)
                    End If
                    isCellValueChangedOpen = False
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        reset()
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = ""
            qst = "select count(*) from TSPL_SALE_RETURN_GATE_ENTRY_HEAD where Gate_Entry_No='" + txtDocNo.Value + "'"

            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
      
        Dim qry As String = "select * from TSPL_SALE_RETURN_GATE_ENTRY_HEAD"

        Dim whrClas As String = ""
        LoadData(clsCommon.ShowSelectForm("SRGEDOCFINDER", qry, "Gate_Entry_No", whrClas, txtDocNo.Value, "TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Gate_Entry_Date", isButtonClicked, "TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Gate_Entry_Date"), NavigatorType.Current)

    End Sub

    Private Sub txtReqNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub

    Private Sub txtmulticapex__My_Click(sender As Object, e As EventArgs) Handles txtmulticapex._My_Click
        If clsCommon.CompairString(ddlDocType.SelectedValue, "") = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow("Please select Doc Type")
            ddlDocType.Focus()
            Exit Sub
        End If
        If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Customer")
            txtVendorNo.Focus()
            Exit Sub
        End If
        ' Ticket No : KDI/11/05/18-000309 By Prabhakar
        If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Location")
            txtBillToLocation.Focus()
            Exit Sub
        End If
        SelectInvoiceItems()

    End Sub

    ' Ticket No  KDI/02/05/18-000285 
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If clsCommon.myLen(txtDocNo.Value) > 0 Then
            If chkCancel.Checked = False Then
                common.clsCommon.MyMessageBoxShow("First select Cancel check box.", Me.Text)
                Return
            End If
            Dim isGateEntryNoExistOnScrapSaleReturn As Integer = clsDBFuncationality.getSingleValue(" select  count (*) from TSPL_SCRAPSALE_HEAD_Return where gate_entry_no = '" + txtDocNo.Value + "'  ")
            Dim isGateEntryNoExistOnSaleReturn As Integer = clsDBFuncationality.getSingleValue(" select  count (*) from TSPL_SD_SALE_RETURN_HEAD where gate_entry_no = '" + txtDocNo.Value + "'  ")
            If isGateEntryNoExistOnScrapSaleReturn = 0 AndAlso isGateEntryNoExistOnSaleReturn = 0 Then
                CancelData()
            Else
                common.clsCommon.MyMessageBoxShow("You can't cancel because Gate Entry No already used in Sale Return Document.", Me.Text)
                Return
            End If
        End If
    End Sub

    Sub CancelData()
        Try
            Dim msg As String = ""
            If (myMessages.cancelConfirm()) Then
                If (clssaleReturnGateEntryHead.CancelData(txtDocNo.Value)) Then
                    msg = "Successfully Canceled"
                End If
                If clsCommon.myLen(msg) > 0 Then
                    common.clsCommon.MyMessageBoxShow(msg)
                End If
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
End Class

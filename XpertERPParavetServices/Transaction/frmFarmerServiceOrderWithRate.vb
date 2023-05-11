Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Threading
Imports System.Data.Sql
Imports common

Public Class FrmFarmerServiceOrderWithRate
    Inherits FrmMainTranScreen
    Dim userCode As String = objCommonVar.CurrentUserCode
    Dim companyCode As String = objCommonVar.CurrentCompanyCode
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim qry As String
    Dim dt As DataTable
    Dim isOneItemOneVendor As Boolean = False
    Dim isnlevelcate As String = "N"
    Dim Is_stdpurrate_check As String = "0"
    Dim IsProceed As String = ""

    '****************************************************
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colServiceId As String = "Service Id"
    Const colGroupName As String = "Group Name"
    Const colServiceName As String = "Service Name"
    Const colCattleTypeCode As String = "Cattle Type_Code"
    Const colCattleTypeName As String = "Cattle Type_Name"
    Const colBreedTypeCode As String = "Breed Type Code"
    Const colBreedTypeName As String = "Breed Type Name"
    Const colCattleTagId As String = "Cattle Tag Id"
    Const colServicePrice As String = "Service Price"


    '========== Paid Amount ==============
    Const colPaidAmount As String = "Paid Amount"
    Const colReceiptNo As String = "Receipt No"
    Dim BillAmount As Double = 0.0

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt("FARSRVTRN")
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmFarmerServiceOrderWithRate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadBlankGrid()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        AddNew()
    End Sub
    Private Sub AddNew()
        txtServiceOrder.Value = ""
        dtpServiceOrderDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtServiceProviderType.Value = ""
        lblServiceProviderType.Text = ""
        txtServiceProviderName.Value = ""
        lblServiceProviderName.Text = ""
        txtFarmerId.Value = ""
        lblFarmerId.Text = ""
        MyNumBox2.Text = 0
        MyNumBox1.Text = 0
        txtPMC.Value = ""
        lblPMC.Text = ""
        txtMCC.Value = ""
        lblMCC.Text = ""
        txtHeadOffice.Text = ""
        txtZone.Value = ""
        lblZone.Text = ""
        txtRegion.Value = ""
        lblRegion.Text = ""
        txtArea.Value = ""
        lblArea.Text = ""
        txtBranch.Value = ""
        lblBranch.Text = ""
        dgvitem.DataSource = Nothing
        LoadBlankGrid()
        txtServiceOrder.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        RadButton1.Enabled = False
        txtServiceOrder.Focus()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    'Sub LoadBlankGridPaidAmount()
    '    gv2.AddNewRowPosition = SystemRowPosition.Bottom
    '    gv2.Rows.Clear()
    '    gv2.Columns.Clear()
    '    gv2.EnableFiltering = False

    '    Dim Paid_Amount As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    Paid_Amount.FormatString = ""
    '    Paid_Amount.HeaderText = "Service Price"
    '    Paid_Amount.Name = colPaidAmount
    '    Paid_Amount.Width = 70
    '    Paid_Amount.ReadOnly = False
    '    gv2.MasterTemplate.Columns.Add(Paid_Amount)

    '    Dim Receipt_No As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    Receipt_No.FormatString = ""
    '    Receipt_No.HeaderText = "Receipt No"
    '    Receipt_No.Name = colReceiptNo
    '    Receipt_No.Width = 150
    '    Receipt_No.ReadOnly = True
    '    gv2.MasterTemplate.Columns.Add(Receipt_No)

    '    gv2.AllowDeleteRow = True
    '    gv2.AllowAddNewRow = True
    '    gv2.ShowGroupPanel = False
    '    gv2.AllowColumnReorder = False
    '    gv2.AllowRowReorder = False
    '    gv2.EnableSorting = False
    '    gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
    '    gv2.MasterTemplate.ShowRowHeaderColumn = False


    'End Sub

    Sub LoadBlankGrid()

        dgvitem.AddNewRowPosition = SystemRowPosition.Bottom
        dgvitem.Rows.Clear()
        dgvitem.Columns.Clear()
        dgvitem.EnableFiltering = False

        Dim Service_Id As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Service_Id.FormatString = ""
        Service_Id.HeaderText = "Service Id"
        Service_Id.Name = colServiceId
        Service_Id.Width = 120
        Service_Id.ReadOnly = False
        Service_Id.TextImageRelation = TextImageRelation.TextBeforeImage
        Service_Id.HeaderImage = My.Resources.search4
        Service_Id.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(Service_Id)

        Dim Group_Name As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Group_Name.FormatString = ""
        Group_Name.HeaderText = "Group Name"
        Group_Name.Name = colGroupName
        Group_Name.Width = 150
        Group_Name.ReadOnly = False
        'Group_Name.TextImageRelation = TextImageRelation.TextBeforeImage
        'Group_Name.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'Group_Name.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(Group_Name)

        Dim Service_Name As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Service_Name.FormatString = ""
        Service_Name.HeaderText = "Service Name"
        Service_Name.Name = colServiceName
        Service_Name.Width = 150
        Service_Name.ReadOnly = True
        'Service_Name.TextImageRelation = TextImageRelation.TextBeforeImage
        'Service_Name.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'Service_Name.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(Service_Name)

        Dim Cattle_Type_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Cattle_Type_Code.FormatString = ""
        Cattle_Type_Code.HeaderText = "Cattle Type Code"
        Cattle_Type_Code.Name = colCattleTypeCode
        Cattle_Type_Code.Width = 150
        Cattle_Type_Code.ReadOnly = True
        dgvitem.MasterTemplate.Columns.Add(Cattle_Type_Code)

        Dim Cattle_Type_Name As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Cattle_Type_Name.FormatString = ""
        Cattle_Type_Name.HeaderText = "Cattle Type Name"
        Cattle_Type_Name.Name = colCattleTypeName
        Cattle_Type_Name.Width = 150
        Cattle_Type_Name.ReadOnly = True
        dgvitem.MasterTemplate.Columns.Add(Cattle_Type_Name)

        Dim Breed_Type_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Breed_Type_Code.FormatString = ""
        Breed_Type_Code.HeaderText = "Breed Type Code"
        Breed_Type_Code.Name = colBreedTypeCode
        Breed_Type_Code.Width = 150
        Breed_Type_Code.ReadOnly = True
        dgvitem.MasterTemplate.Columns.Add(Breed_Type_Code)

        Dim Breed_Type_Name As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Breed_Type_Name.FormatString = ""
        Breed_Type_Name.HeaderText = "Breed Type Name"
        Breed_Type_Name.Name = colBreedTypeName
        Breed_Type_Name.Width = 150
        Breed_Type_Name.ReadOnly = True
        dgvitem.MasterTemplate.Columns.Add(Breed_Type_Name)

        Dim Cattle_CTag_Id As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Cattle_CTag_Id.FormatString = ""
        Cattle_CTag_Id.HeaderText = "Cattle Tag Id"
        Cattle_CTag_Id.Name = colCattleTagId
        Cattle_CTag_Id.Width = 150
        Cattle_CTag_Id.ReadOnly = False
        dgvitem.MasterTemplate.Columns.Add(Cattle_CTag_Id)

        Dim Service_Price As GridViewDecimalColumn = New GridViewDecimalColumn()
        Service_Price.FormatString = ""
        Service_Price.HeaderText = "Service Price"
        Service_Price.Name = colServicePrice
        Service_Price.Width = 70
        Service_Price.ReadOnly = False
        dgvitem.MasterTemplate.Columns.Add(Service_Price)


        dgvitem.AllowDeleteRow = True
        dgvitem.AllowAddNewRow = True
        dgvitem.ShowGroupPanel = False
        dgvitem.AllowColumnReorder = False
        dgvitem.AllowRowReorder = False
        dgvitem.EnableSorting = False
        dgvitem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvitem.MasterTemplate.ShowRowHeaderColumn = False
    End Sub



    'Private Sub txtProviderType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtProviderType._MYValidating
    '    Dim query As String = "  select Designation_id as Code , Designation_Desc as Name from tspl_designation_Master  "
    '    txtCattleType.Value = clsCommon.ShowSelectForm("ProviderTypeVald", query, "Code", "", txtCattleType.Value, "Code", isButtonClicked)
    '    Dim desc As String = " select  Designation_Desc from  tspl_designation_Master where Designation_id='" & txtCattleType.Value & "'"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
    '    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
    '        lblCattleType.Text = clsCommon.myCstr(dt.Rows(0)("Designation_Desc"))
    '    Else
    '        lblCattleType.Text = ""
    '    End If
    'End Sub

    Private Sub txtServiceProviderType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtServiceProviderType._MYValidating
        Dim query As String = "  select Designation_id as Code , Designation_Desc as Name from tspl_designation_Master  "
        txtServiceProviderType.Value = clsCommon.ShowSelectForm("ProviderTypeVald", query, "Code", "", txtServiceProviderType.Value, "Code", isButtonClicked)
        Dim desc As String = " select  Designation_Desc from  tspl_designation_Master where Designation_id='" & txtServiceProviderType.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblServiceProviderType.Text = clsCommon.myCstr(dt.Rows(0)("Designation_Desc"))
        Else
            lblServiceProviderType.Text = ""
        End If
    End Sub
    Private Sub txtServiceProviderName__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtServiceProviderName._MYValidating
        Dim query As String = " select TSPL_EMPLOYEE_MASTER.EMP_CODE as [Code], TSPL_EMPLOYEE_MASTER.Emp_Name as [Name], TSPL_EMPLOYEE_MASTER.Designation as [Designation Id],tspl_designation_Master.Designation_Desc as [Designation Name],TSPL_EMPLOYEE_MASTER.Emp_type as [Emp Type] from TSPL_EMPLOYEE_MASTER left outer join tspl_designation_Master on tspl_designation_Master.Designation_id =TSPL_EMPLOYEE_MASTER.Designation   "
        txtServiceProviderName.Value = clsCommon.ShowSelectForm("UsedByVald", query, "Code", "TSPL_EMPLOYEE_MASTER.Emp_type='Paravet' ", txtServiceProviderName.Value, "Code", isButtonClicked)
        Dim desc As String = "select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE ='" & txtServiceProviderName.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblServiceProviderName.Text = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
        Else
            lblServiceProviderName.Text = ""
        End If
    End Sub
    Private Sub txtFarmerId__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFarmerId._MYValidating
        Dim query As String = " select TSPL_MP_MASTER.MP_Code as [Code], TSPL_MP_MASTER.MP_Name as [Name],TSPL_MP_MASTER.VLC_Code as [VLC Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [VLC Name]  , TSPL_VLC_MASTER_HEAD.MCC as [MCC Code],TSPL_MCC_MASTER.MCC_NAME as [MCC Name]   from TSPL_MP_MASTER  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MP_MASTER.VLC_Code  left outer join  TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC "
        txtFarmerId.Value = clsCommon.ShowSelectForm("FarmerVald", query, "Code", "", txtFarmerId.Value, "Code", isButtonClicked)
        'Dim desc As String = "select  MP_Name from  TSPL_MP_MASTER where MP_Code='" & txtFarmerId.Value & "'"
        Dim desc As String = " select TSPL_MP_MASTER.MP_Code, TSPL_MP_MASTER.MP_Name,TSPL_MP_MASTER.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name  , TSPL_VLC_MASTER_HEAD.MCC as MCC_Code,TSPL_MCC_MASTER.MCC_NAME   from TSPL_MP_MASTER  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MP_MASTER.VLC_Code  left outer join  TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC  where TSPL_MP_MASTER.MP_Code ='" & txtFarmerId.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblFarmerId.Text = clsCommon.myCstr(dt.Rows(0)("MP_Name"))
            txtPMC.Value = clsCommon.myCstr(dt.Rows(0)("VLC_Code"))
            lblPMC.Text = clsCommon.myCstr(dt.Rows(0)("VLC_Name"))
            txtMCC.Value = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
            lblMCC.Text = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
        Else
            lblFarmerId.Text = ""
            txtPMC.Value = ""
            lblPMC.Text = ""
            txtMCC.Value = ""
            lblMCC.Text = ""
        End If
    End Sub

    Sub OpenIServiceList(ByVal isButtonClick As Boolean)
        Dim whrcls As String = ""

        qry = "select TSPL_PARAVET_SERVICE_MASTER.Service_Code as Service_Code,TSPL_PARAVET_SERVICE_MASTER.Service_Group_Name,TSPL_PARAVET_SERVICE_MASTER.Service_Group_Desc,TSPL_PARAVET_SERVICE_MASTER.Cattle_Type_Code,TSPL_CATTLE_TYPE_MASTER.Cattle_Type_Name,TSPL_PARAVET_SERVICE_MASTER.Breed_Code,TSPL_BRED_TYPE_MASTER.Bred_Type_Name,TSPL_PARAVET_SERVICE_MASTER.Service_Charge from TSPL_PARAVET_SERVICE_MASTER left outer join TSPL_CATTLE_TYPE_MASTER on TSPL_CATTLE_TYPE_MASTER.Cattle_Type_Code =TSPL_PARAVET_SERVICE_MASTER.Cattle_Type_Code left outer join TSPL_BRED_TYPE_MASTER on TSPL_BRED_TYPE_MASTER.Bred_Type_Code =TSPL_PARAVET_SERVICE_MASTER.Breed_Code"
        dt = clsDBFuncationality.GetDataTable(qry)
        dgvitem.CurrentRow.Cells(colServiceId).Value = clsCommon.ShowSelectForm("Servicefinder@VID", qry, "Service_Code", whrcls, dgvitem.CurrentRow.Cells(colServiceId).Value, "Service_Code", False)
        qry = "select TSPL_PARAVET_SERVICE_MASTER.Service_Code ,TSPL_PARAVET_SERVICE_MASTER.Service_Group_Name,TSPL_PARAVET_SERVICE_MASTER.Service_Group_Desc,TSPL_PARAVET_SERVICE_MASTER.Cattle_Type_Code,TSPL_CATTLE_TYPE_MASTER.Cattle_Type_Name,TSPL_PARAVET_SERVICE_MASTER.Breed_Code,TSPL_BRED_TYPE_MASTER.Bred_Type_Name,TSPL_PARAVET_SERVICE_MASTER.Service_Charge from TSPL_PARAVET_SERVICE_MASTER left outer join TSPL_CATTLE_TYPE_MASTER on TSPL_CATTLE_TYPE_MASTER.Cattle_Type_Code =TSPL_PARAVET_SERVICE_MASTER.Cattle_Type_Code left outer join TSPL_BRED_TYPE_MASTER on TSPL_BRED_TYPE_MASTER.Bred_Type_Code =TSPL_PARAVET_SERVICE_MASTER.Breed_Code where TSPL_PARAVET_SERVICE_MASTER.Service_Code='" + dgvitem.CurrentRow.Cells(colServiceId).Value + "' "
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            dgvitem.CurrentRow.Cells(colGroupName).Value = clsCommon.myCstr(dt.Rows(0)("Service_Group_Name"))
            dgvitem.CurrentRow.Cells(colServiceName).Value = clsCommon.myCstr(dt.Rows(0)("Service_Group_Desc"))
            dgvitem.CurrentRow.Cells(colCattleTypeCode).Value = clsCommon.myCstr(dt.Rows(0)("Cattle_Type_Code"))

            dgvitem.CurrentRow.Cells(colCattleTypeName).Value = clsCommon.myCstr(dt.Rows(0)("Cattle_Type_Name"))
            dgvitem.CurrentRow.Cells(colBreedTypeCode).Value = clsCommon.myCstr(dt.Rows(0)("Breed_Code"))
            dgvitem.CurrentRow.Cells(colBreedTypeName).Value = clsCommon.myCstr(dt.Rows(0)("Bred_Type_Name"))
            dgvitem.CurrentRow.Cells(colServicePrice).Value = clsCommon.myCdbl(dt.Rows(0)("Service_Charge"))
        Else
            dgvitem.CurrentRow.Cells(colGroupName).Value = ""
            dgvitem.CurrentRow.Cells(colServiceName).Value = ""
            dgvitem.CurrentRow.Cells(colCattleTypeCode).Value = ""

            dgvitem.CurrentRow.Cells(colCattleTypeName).Value = ""
            dgvitem.CurrentRow.Cells(colBreedTypeCode).Value = ""
            dgvitem.CurrentRow.Cells(colBreedTypeName).Value = ""
            dgvitem.CurrentRow.Cells(colServicePrice).Value = False
        End If

    End Sub

    Sub OpenICattelTagId(ByVal isButtonClick As Boolean)
        Dim whrcls As String = ""
        Dim strCattleTypeCode = ""
        Dim strCattleBreedCode = ""
        If clsCommon.myLen(txtFarmerId.Value) <= 0 Then
            Throw New Exception("First Select Farmer.")
        End If
        If clsCommon.myLen(dgvitem.CurrentRow.Cells(colServiceId).Value) > 0 Then
            If clsCommon.myLen(dgvitem.CurrentRow.Cells(colCattleTypeCode).Value) > 0 Then
                strCattleTypeCode = dgvitem.CurrentRow.Cells(colCattleTypeCode).Value
            End If
            If clsCommon.myLen(dgvitem.CurrentRow.Cells(colBreedTypeCode).Value) > 0 Then
                strCattleBreedCode = dgvitem.CurrentRow.Cells(colBreedTypeCode).Value
            End If
        Else
            Throw New Exception("First Select Service.")
        End If
        qry = " select Tag_Id,Cattle_Code,NDDB_Code from TSPL_CATTLE_MASTER "
        whrcls = " Cattle_Type_Code= '" + strCattleTypeCode + "' and Bred_Type_Code = '" + strCattleBreedCode + "' and Farmer_Id = '" + txtFarmerId.Value + "'  "
        dgvitem.CurrentRow.Cells(colCattleTagId).Value = clsCommon.ShowSelectForm("TagIdfinder@VID", qry, "Tag_Id", whrcls, dgvitem.CurrentRow.Cells(colCattleTagId).Value, "Tag_Id", False)
    End Sub

    Private Sub dgvitem_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles dgvitem.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    Dim XR As Integer = dgvitem.CurrentRow.Index

                    If e.Column Is dgvitem.Columns(colServiceId) Then
                        OpenIServiceList(False)

                    ElseIf (e.Column Is dgvitem.Columns(colCattleTagId)) Then
                        OpenICattelTagId(False)
                    ElseIf e.Column Is dgvitem.Columns(colServicePrice) Then
                        CalculateBillAmount()
                        'ElseIf (e.Column Is dgvitem.Columns(colLocationCode)) Then
                        '    OpenLocation(False)
                    End If

                    CalculateBillAmount()
                    isCellValueChangedOpen = False
                End If
            End If

        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally

        End Try
    End Sub

    Function AllowToSave() As Boolean
        Try

            If clsCommon.myLen(txtServiceProviderType.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Service Provider Type.")
                txtServiceProviderType.Focus()
                Return False
            End If
            If clsCommon.myLen(txtServiceProviderName.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Service Provider Name.")
                txtServiceProviderName.Focus()
                Return False
            End If
            If clsCommon.myLen(txtFarmerId.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Farmer Id.")
                txtFarmerId.Focus()
                Return False
            End If
            ' isOneItemOneVendor = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchaseOneItemOneVendor, clsFixedParameterCode.PurchaseOneItemOneVendor, Nothing)) = 1, True, False)

            Dim arrIServiceCode As New List(Of String)()
            For ii As Integer = 0 To dgvitem.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(dgvitem.Rows(ii).Cells(colServiceId).Value)
                If clsCommon.myLen(strICode) > 0 Then
                    If clsCommon.myLen(dgvitem.Rows(ii).Cells(colCattleTagId).Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Please Select Cattle Tag Id for. At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                    If dgvitem.Rows(ii).Cells(colServicePrice).Value <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Service Price should not be left blank or zero. At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                    Dim strServiceId As String = clsCommon.myCstr(dgvitem.Rows(ii).Cells(colServiceId).Value)
                    Dim strCattleTagId As String = clsCommon.myCstr(dgvitem.Rows(ii).Cells(colCattleTagId).Value)

                    For jj As Integer = 0 To dgvitem.Rows.Count - 1
                        If (ii = jj) Then
                            Continue For
                        End If

                        If ((clsCommon.CompairString(strServiceId, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colServiceId).Value)) = CompairStringResult.Equal) And (clsCommon.CompairString(strCattleTagId, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colCattleTagId).Value)) = CompairStringResult.Equal)) Then

                            common.clsCommon.MyMessageBoxShow(" Service No (" & strServiceId & ") used for Cattle Tag Id (" & strCattleTagId & ")  At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + " should not be same ")
                            Return False
                        End If

                        'If ((clsCommon.CompairString(strICode, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colitemno).Value)) = CompairStringResult.Equal) And (clsCommon.CompairString(strILocation, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colLocationCode).Value)) = CompairStringResult.Equal)) Then
                        '    If ((clsCommon.CompairString(strprice, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colmrp).Value)) = CompairStringResult.Equal) Or (clsCommon.CompairString(strprice, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colmrp).Value)) = CompairStringResult.Greater) Or (clsCommon.CompairString(strprice, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colmrp).Value)) = CompairStringResult.Less)) Then
                        '        common.clsCommon.MyMessageBoxShow("MRP of two same Item " + strICode.Trim() + "( " + strIName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + " should not be same or different for same location")
                        '        Return False
                        '    End If

                        'End If
                    Next
                    '-------------------------------------------------
                    If Not arrIServiceCode.Contains(strICode) Then
                        arrIServiceCode.Add(strICode)
                    End If
                End If
            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function
    Private Sub CalculateBillAmount()
        BillAmount = 0.0
        For ii As Integer = 0 To dgvitem.Rows.Count - 1
            BillAmount = BillAmount + clsCommon.myCdbl(dgvitem.Rows(ii).Cells(colServicePrice).Value)
        Next
        MyNumBox2.Text = BillAmount
    End Sub
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsFarmerServiceOrderHeader()
                obj.Service_Order_No = txtServiceOrder.Value
                obj.Service_Order_Date = dtpServiceOrderDate.Value
                obj.Service_Provider_Type_ID = txtServiceProviderType.Value
                obj.Service_Provider_Name = txtServiceProviderName.Value
                obj.HO = txtHeadOffice.Text
                obj.ZONE = txtZone.Value
                obj.Region = txtRegion.Value
                obj.Area = txtArea.Value
                obj.Branch = txtBranch.Value
                obj.MCC = txtMCC.Value
                obj.PMC = txtPMC.Value
                obj.Farmer_Id = txtFarmerId.Value
                'obj.Cattle_Id =
                obj.Staff = txtStaff.Value
                obj.Bill_Amount = MyNumBox2.Text
                obj.Paid_Amount = MyNumBox1.Text
                obj.Posting_Date = obj.Posting_Date

                Dim Arr As New List(Of clsFarmerServiceOrderDetails)
                For Each grow As GridViewRowInfo In dgvitem.Rows
                    Dim objTr As New clsFarmerServiceOrderDetails()
                    objTr.Service_Order_No = txtServiceOrder.Value
                    objTr.Service_Id = clsCommon.myCstr(grow.Cells(colServiceId).Value)
                    objTr.Group_Name = clsCommon.myCstr(grow.Cells(colGroupName).Value)
                    objTr.Service_Name = clsCommon.myCstr(grow.Cells(colServiceName).Value)
                    objTr.Cattle_Type_Code = clsCommon.myCstr(grow.Cells(colCattleTypeCode).Value)
                    objTr.Cattle_Type_Name = clsCommon.myCstr(grow.Cells(colCattleTypeName).Value)
                    objTr.Breed_Type_Code = clsCommon.myCstr(grow.Cells(colBreedTypeCode).Value)
                    objTr.Breed_Type_Name = clsCommon.myCstr(grow.Cells(colBreedTypeName).Value)
                    objTr.Cattle_Tag_Id = clsCommon.myCstr(grow.Cells(colCattleTagId).Value)
                    objTr.Service_Price = clsCommon.myCdbl(grow.Cells(colServicePrice).Value)

                    If (clsCommon.myLen(objTr.Service_Id) > 0) Then
                        Arr.Add(objTr)
                    End If
                Next

                If (Arr Is Nothing OrElse Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at least one Service")
                    Return
                End If
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("select  count(*) from  TSPL_Farmer_Service_Order_Head where Service_Order_No='" & obj.Service_Order_No & "'")
                If ChkNewEntry > 0 Then
                    isNewEntry = False
                Else
                    isNewEntry = True
                    obj.Service_Order_No = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(obj.Service_Order_Date, "dd/MM/yyyy"), clsDocType.FarmerServiceOrder, "", "")
                    If clsCommon.myLen(obj.Service_Order_No) <= 0 Then
                        clsCommon.MyMessageBoxShow("Error In Service Order No Genertion")
                        Exit Sub
                    End If
                End If
                If (obj.SaveData(obj, isNewEntry, Arr)) Then
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.Service_Order_No) 'LoadData(obj.Service_Order_No, NavigatorType.Current)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                    RadButton1.Enabled = True
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub FrmFarmerServiceOrderWithRate_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            deleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
            GC.Collect()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        AddNew()
    End Sub

    Sub LoadData(ByVal strFarmerOrderNo As String)
        AddNew()
        isInsideLoadData = True
        LoadBlankGrid()
        Dim obj As clsFarmerServiceOrderHeader = clsFarmerServiceOrderHeader.GetData(strFarmerOrderNo, Nothing)
        isNewEntry = False
        If obj IsNot Nothing Then
            txtServiceOrder.Value = obj.Service_Order_No
            dtpServiceOrderDate.Value = obj.Service_Order_Date
            txtServiceProviderType.Value = obj.Service_Provider_Type_ID
            If clsCommon.myLen(obj.Service_Provider_Type_ID) > 0 Then
                lblServiceProviderType.Text = clsDBFuncationality.getSingleValue(" select  Designation_Desc from  tspl_designation_Master where Designation_id='" & obj.Service_Provider_Type_ID & "'")
            End If
            txtServiceProviderName.Value = obj.Service_Provider_Name
            If clsCommon.myLen(obj.Service_Provider_Name) > 0 Then
                lblServiceProviderName.Text = clsDBFuncationality.getSingleValue(" select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE ='" & obj.Service_Provider_Name & "'")
            End If
            txtFarmerId.Value = obj.Farmer_Id
            If clsCommon.myLen(obj.Farmer_Id) > 0 Then
                lblFarmerId.Text = clsDBFuncationality.getSingleValue(" select  MP_Name from  TSPL_MP_MASTER where MP_Code='" & obj.Farmer_Id & "'")
            End If
            MyNumBox2.Text = obj.Bill_Amount
            MyNumBox1.Text = obj.Paid_Amount
            txtPMC.Value = obj.PMC
            If clsCommon.myLen(obj.PMC) > 0 Then
                lblPMC.Text = clsDBFuncationality.getSingleValue(" select  VLC_Name from  TSPL_VLC_MASTER_HEAD where VLC_Code='" & obj.PMC & "'")
            End If
            txtMCC.Value = obj.MCC
            If clsCommon.myLen(obj.MCC) > 0 Then
                lblMCC.Text = clsDBFuncationality.getSingleValue(" select  MCC_NAME from  TSPL_MCC_MASTER where MCC_Code='" & obj.MCC & "'")
            End If
            txtHeadOffice.Text = obj.HO
            txtZone.Value = obj.ZONE
            If clsCommon.myLen(obj.ZONE) > 0 Then
                lblZone.Text = clsDBFuncationality.getSingleValue("select  Description from  TSPL_ZONE_MASTER where Zone_Code='" & obj.ZONE & "'")
            End If

            txtRegion.Value = obj.Region
            If clsCommon.myLen(obj.Region) > 0 Then
                lblRegion.Text = clsDBFuncationality.getSingleValue("select  REGION_NAME from  TSPL_REGION_MASTER where REGION_CODE='" & obj.Region & "'")
            End If
            txtArea.Value = obj.Area
            If clsCommon.myLen(obj.Area) > 0 Then
                lblArea.Text = clsDBFuncationality.getSingleValue("select Name from TSPL_AREA_MASTER where Code='" & obj.Area & "'")
            End If

            txtBranch.Value = obj.Branch
            If clsCommon.myLen(obj.Branch) > 0 Then
                lblBranch.Text = clsDBFuncationality.getSingleValue("select  Branch_Name from  TSPL_BRANCH_MASTER where Branch_Code='" & obj.Branch & "'")
            End If

            For Each objtr As clsFarmerServiceOrderDetails In obj.Arr
                Dim grow As GridViewRowInfo = dgvitem.Rows.AddNew()
                grow.Cells(colServiceId).Value = objtr.Service_Id
                grow.Cells(colGroupName).Value = objtr.Group_Name
                grow.Cells(colServiceName).Value = objtr.Service_Name
                grow.Cells(colCattleTypeCode).Value = objtr.Cattle_Type_Code
                grow.Cells(colCattleTypeName).Value = objtr.Cattle_Type_Name
                grow.Cells(colBreedTypeCode).Value = objtr.Breed_Type_Code
                grow.Cells(colBreedTypeName).Value = objtr.Breed_Type_Name
                grow.Cells(colCattleTagId).Value = objtr.Cattle_Tag_Id
                grow.Cells(colServicePrice).Value = objtr.Service_Price

            Next
            CalculateBillAmount()
            isInsideLoadData = False
            txtServiceOrder.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
            RadButton1.Enabled = True
        End If
    End Sub

    Private Sub txtServiceOrder__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtServiceOrder._MYNavigator

        Dim WhrCls As String = ""
       
        Dim qry As String = "select Service_Order_No  from TSPL_Farmer_Service_Order_Head  Where 2=2  "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Farmer_Service_Order_Head.Service_Order_No=(select MIN(Service_Order_No) from TSPL_Farmer_Service_Order_Head)"
            Case NavigatorType.Last
                qry += " and TSPL_Farmer_Service_Order_Head.Service_Order_No=(select MAX(Service_Order_No) from TSPL_Farmer_Service_Order_Head)"
            Case NavigatorType.Next
                qry += " and TSPL_Farmer_Service_Order_Head.Service_Order_No=(select Min(Service_Order_No) from TSPL_Farmer_Service_Order_Head where Service_Order_No > '" + txtServiceOrder.Value + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_Farmer_Service_Order_Head.Service_Order_No=(select Max(Service_Order_No) from TSPL_Farmer_Service_Order_Head where Service_Order_No < '" + txtServiceOrder.Value + "')"
            Case NavigatorType.Current
                qry += " and TSPL_Farmer_Service_Order_Head.Service_Order_No='" + txtServiceOrder.Value + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtServiceOrder.Value = clsCommon.myCstr(dt.Rows(0)("Service_Order_No"))
        End If
        LoadData(txtServiceOrder.Value)


        'Try
        '    LoadData(txtServiceOrder.Value, NavType)
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub

    Private Sub txtServiceOrder__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtServiceOrder._MYValidating
        Dim str As String = "select count(*) from TSPL_Farmer_Service_Order_Head where Service_Order_No = '" + txtServiceOrder.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtServiceOrder.MyReadOnly = False
        Else
            txtServiceOrder.MyReadOnly = True
        End If

        If txtServiceOrder.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = " select Service_Order_No, convert (varchar, service_Order_Date,103) as service_Order_Date ,Service_Provider_Type_ID,Service_Provider_Name,Farmer_Id,isnull (Bill_Amount,0) as Bill_Amount ,isnull (Paid_Amount,0) as Paid_Amount from TSPL_Farmer_Service_Order_Head "
            txtServiceOrder.Value = clsCommon.ShowSelectForm("TSPL_Farmer_Service_Order_Head", qry, "Service_Order_No", "", txtServiceOrder.Value, "TSPL_Farmer_Service_Order_Head.Service_Order_No", isButtonClicked)
            If clsCommon.myLen(txtServiceOrder.Value) > 0 Then
                LoadData(txtServiceOrder.Value)
            Else
                AddNew()
            End If
        End If
    End Sub

    Sub deleteData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(txtServiceOrder.Value) > 0 Then

                If clsFarmerServiceOrderHeader.deleteData(txtServiceOrder.Value, trans) Then
                    myMessages.delete()

                    trans.Commit()
                    AddNew()
                Else
                    clsCommon.MyMessageBoxShow("Can't delete the record")
                    trans.Rollback()
                End If
            Else

                clsCommon.MyMessageBoxShow("Please Select a document to delete")
                trans.Rollback()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            trans.Rollback()
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If myMessages.deleteConfirm() Then
            deleteData()
        End If
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub txtZone__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtZone._MYValidating
        Dim query As String = "  select Zone_Code as [Code],Description as [Zone Name] from TSPL_ZONE_MASTER "
        txtZone.Value = clsCommon.ShowSelectForm("ZoneCodevald", query, "Code", "", txtZone.Value, "Code", isButtonClicked)
        Dim desc As String = "select  Description from  TSPL_ZONE_MASTER where Zone_Code='" & txtZone.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblZone.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        Else
            lblZone.Text = ""
        End If
    End Sub


    Private Sub txtRegion__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRegion._MYValidating
        'select REGION_CODE,REGION_NAME from TSPL_REGION_MASTER
        Dim query As String = "  select REGION_CODE as [Code], REGION_NAME as [Description] from TSPL_REGION_MASTER "
        txtRegion.Value = clsCommon.ShowSelectForm("RegionVald", query, "Code", "", txtRegion.Value, "Code", isButtonClicked)
        Dim desc As String = "select  REGION_NAME from  TSPL_REGION_MASTER where REGION_CODE='" & txtRegion.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblRegion.Text = clsCommon.myCstr(dt.Rows(0)("REGION_NAME"))
        Else
            lblRegion.Text = ""
        End If
    End Sub

    Private Sub txtArea__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtArea._MYValidating
        If clsCommon.myLen(txtZone.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select Zone First", Me.Text)
            txtZone.Focus()
            txtZone.Select()
            Return
        End If
        Try
            Dim qry As String = " select Code,Name  from TSPL_AREA_MASTER "
            txtArea.Value = clsCommon.ShowSelectForm("AREAFND", qry, "Code", " Zone_Code='" + txtZone.Value + "'", txtArea.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtArea.Value) > 0 Then
                lblArea.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Name from TSPL_AREA_MASTER where Code='" + txtArea.Value + "'"))
            Else
                lblArea.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtBranch__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBranch._MYValidating
        Dim query As String = " select Branch_Code as [Code],Branch_Name as [Branch Name] from TSPL_BRANCH_MASTER "
        txtBranch.Value = clsCommon.ShowSelectForm("BranchCodevald", query, "Code", "", txtBranch.Value, "Code", isButtonClicked)
        Dim desc As String = "select  Branch_Name from  TSPL_BRANCH_MASTER where Branch_Code='" & txtBranch.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblBranch.Text = clsCommon.myCstr(dt.Rows(0)("Branch_Name"))
        Else
            lblBranch.Text = ""
        End If
    End Sub

End Class

'============Created By Monika 03/06/2014
'---------BM00000003535
'' work on clsCreateAllTable for Parametter Range Master History table getting error when save against ticket no. MIL/04/05/18-000019
Imports common
Imports System.Data.SqlClient
Public Class FrmParameterRangeMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim CheckParameterRangerProcurementTypewise As Integer = 0
    Dim IsItemMilkType As Integer = 0
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const colCode As String = "Code"
    Const colIsEdit As String = "colIsEdit"
    Const colisReject As String = "IsReject"
    Const colSLNo As String = "SL. No."
    Const colID As String = "colID"
    Const colDesc As String = "Description"
    Const colLower As String = "Lower_Range"
    Const colUpper As String = "Upper_Range"
    Const colValue As String = "Value"
    Const colVendorClass As String = "Vendor_Class"
    Const colStatus As String = "Status"
    Const colNature As String = "Nature"
    Const colLocCode As String = "Loc_Code"
    Const colLocDesc As String = "LocDesc"
    '    Const colCondtion As String = "colCondtion"
    Const colCondtionValue As String = "Condition_Value"
    Const colDate As String = "Effective_Date"
    Const colEndDate As String = "colEndDate"
    Dim isNewEntry As Boolean = True
    Dim isLoadData As Boolean = False
    Dim isValueChanged As Boolean = False
    Dim Row2 As Integer = 0
    Dim iscellvalue As Boolean = False

#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmParameterRangeMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub Reset()
        isLoadData = False
        isValueChanged = False
        gv.Rows.Clear()
        '        gv.Rows.AddNew()
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        isNewEntry = True
        btnsave.Text = "Save"
        btndelete.Enabled = False
        fndLoc.Value = ""
        txtLocName.Text = ""
        cmbVendorClass.SelectedIndex = 0
        dtpDocDate.Value = clsCommon.GETSERVERDATE()

    End Sub

    Sub LoadData()
        If clsCommon.myLen(fndLoc.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select a Location", Me.Text)
            Exit Sub
        End If
        If IsItemMilkType = 0 Then
            If clsCommon.myLen(cmbVendorClass.SelectedValue) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select a Vendor Class", Me.Text)
                Exit Sub
            End If
        Else
            If clsCommon.myLen(txtMilktypeCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select a Milk Type Code", Me.Text)
                Exit Sub
            End If
        End If
        If CheckParameterRangerProcurementTypewise = 1 Then
            If ddlBulProcType.SelectedValue = "" Then
                clsCommon.MyMessageBoxShow(Me, "Please select Procurement Type", Me.Text)
                Exit Sub
            End If
        End If

        Dim DtParam As DataTable = clsDBFuncationality.GetDataTable("select distinct Code from tspl_parameter_master")
        If DtParam Is Nothing OrElse DtParam.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No parameter found in master", Me.Text)
            Exit Sub
        End If
        Dim strParameterRange As String = ""
        If CheckParameterRangerProcurementTypewise = 1 Then
            strParameterRange = " and Procurement_Type = '" & ddlBulProcType.SelectedValue & "'"
        Else
            strParameterRange = ""
        End If
        Dim dtt As Date = dtpDocDate.Value
        Dim qry As String = " ;with ParameterRange as (select tspl_parameter_range_master.MIKL_TYPE_CODE,tspl_parameter_range_master.code,tspl_parameter_range_master.PK_Id,tspl_parameter_range_master.lower_range,tspl_parameter_range_master.loc_code,TSPL_LOCATION_MASTER.location_desc,tspl_parameter_range_master.upper_range,tspl_parameter_range_master.value,tspl_parameter_range_master.effective_date,tspl_parameter_range_master.End_Date,tspl_parameter_range_master.Condition_value,tspl_parameter_range_master.Status,tspl_parameter_master.description,tspl_parameter_master.nature,tspl_parameter_range_master.Vendor_Class,tspl_parameter_range_master.IsReject from tspl_parameter_range_master left outer join tspl_parameter_master on tspl_parameter_range_master.code=tspl_parameter_master.code  left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code=tspl_parameter_range_master.loc_code where effective_Date<='" & clsCommon.GetPrintDate(dtt, "dd/MMM/yyyy") & "' " & strParameterRange & "    ) "

        For i As Integer = 0 To DtParam.Rows.Count - 1
            If IsItemMilkType = 0 Then
                qry = qry & "   select * from (select  rank() over(order by effective_Date Desc) as SLNo,* from ParameterRange where loc_code='" & fndLoc.Value & "' and vendor_class='" & cmbVendorClass.SelectedValue & "' and Code='" & DtParam.Rows(i)("Code") & "' )xx where slno=1 "
            Else
                qry = qry & "   select * from (select  rank() over(order by effective_Date Desc) as SLNo,* from ParameterRange where loc_code='" & fndLoc.Value & "' and MIKL_TYPE_CODE='" & txtMilktypeCode.Value & "' and Code='" & DtParam.Rows(i)("Code") & "'   )xx where slno=1 "
            End If
            If i <> DtParam.Rows.Count - 1 Then
                qry = qry & " Union All "
            End If
        Next
        Try
            gv.Rows.Clear()
            LoadBlankGrid()
            isNewEntry = True
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            isLoadData = True
            'Dim i As Integer = 0
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                isNewEntry = False
                For Each dr As DataRow In dt.Rows
                    gv.Rows.AddNew()
                    'gv.Rows(gv.Rows.Count - 1).Cells(colSLNo).Value = (i + 1)
                    'i = i + 1
                    gv.Rows(gv.Rows.Count - 1).Cells(colIsEdit).Value = False
                    gv.Rows(gv.Rows.Count - 1).Cells(colCode).Value = clsCommon.myCstr(dr("code"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colID).Value = clsCommon.myCstr(dr("PK_Id"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colNature).Value = clsCommon.myCstr(dr("nature"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colDesc).Value = clsCommon.myCstr(dr("description"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colLocCode).Value = clsCommon.myCstr(dr("loc_code"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colLocDesc).Value = clsCommon.myCstr(dr("location_desc"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colVendorClass).Value = clsCommon.myCstr(dr("Vendor_Class"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colLower).Value = clsCommon.myCdbl(dr("lower_range"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colUpper).Value = clsCommon.myCdbl(dr("upper_range"))
                    Try
                        gv.Rows(gv.Rows.Count - 1).Cells(colValue).Value = clsCommon.myCdbl(dr("value"))
                    Catch ex1 As Exception
                    End Try
                    Try
                        gv.Rows(gv.Rows.Count - 1).Cells(colCondtionValue).Value = clsCommon.myCstr(dr("Condition_Value"))
                    Catch ex2 As Exception
                    End Try
                    gv.Rows(gv.Rows.Count - 1).Cells(colStatus).Value = clsCommon.myCstr(dr("Status"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colVendorClass).Value = clsCommon.myCstr(dr("Vendor_Class"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colisReject).Value = IIf(clsCommon.myCdbl(dr("IsReject")) = 1, "Yes", "No")


                    If (clsCommon.CompairString(gv.Rows(gv.Rows.Count - 1).Cells(colNature).Value, "A") = CompairStringResult.Equal) Then
                        gv.Rows(gv.Rows.Count - 1).Cells(colCondtionValue).ReadOnly = False
                        gv.Rows(gv.Rows.Count - 1).Cells(colLower).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colUpper).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colStatus).ReadOnly = True
                    ElseIf (clsCommon.CompairString(gv.Rows(gv.Rows.Count - 1).Cells(colNature).Value, "B") = CompairStringResult.Equal) Then
                        gv.Rows(gv.Rows.Count - 1).Cells(colCondtionValue).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colLower).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colUpper).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colStatus).ReadOnly = False
                    Else

                        gv.Rows(gv.Rows.Count - 1).Cells(colCondtionValue).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colLower).ReadOnly = False
                        gv.Rows(gv.Rows.Count - 1).Cells(colUpper).ReadOnly = False
                        gv.Rows(gv.Rows.Count - 1).Cells(colStatus).ReadOnly = True
                    End If
                    If clsCommon.myCdbl(dr("IsReject")) = 1 Then
                        gv.Rows(gv.Rows.Count - 1).Cells(colValue).ReadOnly = True
                        Try
                            gv.Rows(gv.Rows.Count - 1).Cells(colValue).Value = ""
                        Catch exxx As Exception
                        End Try
                    End If
                    Try
                        gv.Rows(gv.Rows.Count - 1).Cells(colDate).Value = Convert.ToDateTime(dr("effective_date"))
                    Catch exx As Exception
                        gv.Rows(gv.Rows.Count - 1).Cells(colDate).Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                    End Try

                    Try
                        If gv.Rows(gv.Rows.Count - 1).Cells(colDate).Value.ToString().Substring(6, 4) = "0001" Then
                            gv.Rows(gv.Rows.Count - 1).Cells(colDate).Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                        End If
                    Catch exx As Exception
                        gv.Rows(gv.Rows.Count - 1).Cells(colDate).Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                    End Try
                    If dr("End_Date") Is DBNull.Value Then
                        gv.Rows(gv.Rows.Count - 1).Cells(colEndDate).Value = Nothing
                    Else
                        gv.Rows(gv.Rows.Count - 1).Cells(colEndDate).Value = clsCommon.myCDate(dr("End_Date"))
                    End If

                    'gv.Rows.AddNew()
                Next

                btnsave.Text = "Update"
                btndelete.Enabled = True
                'UcAttachment1.SaveData("PARANGMST")
            End If
            'End If
            isLoadData = False
        Catch ex As Exception
            isNewEntry = True
            isLoadData = False
        End Try
    End Sub

    Private Sub FrmParameterRangeMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmParameterRangeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadProcurementType()
        CheckParameterRangerProcurementTypewise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CheckParameterRangerProcurementTypewise, clsFixedParameterCode.CheckParameterRangerProcurementTypewise, Nothing))
        IsItemMilkType = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isItemMilkType, clsFixedParameterCode.isItemMilkType, Nothing))
        SetUserMgmtNew()
        LoadBlankGrid()
        Reset()
        cmbVendorClass.DataSource = FillVendorClass()
        cmbVendorClass.DisplayMember = "Value"
        cmbVendorClass.ValueMember = "Value"
        'LoadData()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")

        RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
        RadPageView1.SelectedPage = RadPageViewPage1
        If IsItemMilkType = 1 Then
            lblVendorClass.Text = "Milk Type Code"
            cmbVendorClass.Visible = False
            txtMilktypeCode.Visible = True
        Else
            lblVendorClass.Text = "Vendor Class"
            cmbVendorClass.Visible = True
            txtMilktypeCode.Visible = False
        End If
        If CheckParameterRangerProcurementTypewise = 1 Then
            lblProcType.Visible = True
            ddlBulProcType.Visible = True
        Else
            lblProcType.Visible = False
            ddlBulProcType.Visible = False
        End If
    End Sub
    Private Sub txtMilktypeCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMilktypeCode._MYValidating
        Dim whr As String = ""
        txtMilktypeCode.Value = clsMilkTypeMaster.getFinder(whr, txtMilktypeCode.Value, isButtonClicked)
    End Sub
    'Function loadConditions() As DataTable
    '    Dim qry As String = " select 'Above' as Value union all select 'Below' as Value  union all  select 'Equal' as Value union all select 'Between'  as Value "
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '    Return dt
    'End Function
    Sub UpdateSlNo()
        If gv.Rows.Count > 0 Then
            For i As Integer = 0 To gv.Rows.Count - 1
                gv.Rows(i).Cells(colSLNo).Value = (i + 1)
            Next
        End If
    End Sub
    Sub LoadProcurementType()
        ddlBulProcType.DataSource = Nothing
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "C"
        dr("Name") = "Contractor"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "MCC"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "J"
        dr("Name") = "Job Work"
        dt.Rows.Add(dr)

        ddlBulProcType.DataSource = dt
        ddlBulProcType.DisplayMember = "Name"
        ddlBulProcType.ValueMember = "Code"
    End Sub
    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()
        Dim repoIsEdit As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsEdit.Name = colIsEdit
        repoIsEdit.Width = 60
        repoIsEdit.HeaderText = "Update"
        repoIsEdit.ReadOnly = True
        repoIsEdit.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoIsEdit)

        Dim repoSL As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoSL.Name = colSLNo
        'repoSL.Width = 60
        'repoSL.HeaderText = "SL. No."
        'repoSL.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(repoSL)


        repoSL = New GridViewTextBoxColumn()
        repoSL.Name = colID
        repoSL.Width = 60
        repoSL.HeaderText = "ID"
        repoSL.ReadOnly = True
        repoSL.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSL)

        Dim repocode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocode.Name = colCode
        repocode.Width = 150
        repocode.HeaderText = "Parameter Code"
        repocode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repocode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repocode)



        repocode = New GridViewTextBoxColumn()
        repocode.Name = colNature
        repocode.Width = 5
        repocode.HeaderText = "nature"
        'repocode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repocode.TextImageRelation = TextImageRelation.TextBeforeImage
        repocode.IsVisible = False
        gv.MasterTemplate.Columns.Add(repocode)

        Dim reponame As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponame.FormatString = ""
        reponame.Name = colDesc
        reponame.Width = 205
        reponame.HeaderText = "Description"
        reponame.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reponame)

        Dim repoValue2 As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoValue2.FormatString = ""
        repoValue2.Name = colisReject
        repoValue2.Width = 80
        repoValue2.HeaderText = colisReject
        repoValue2.ReadOnly = False
        repoValue2.DataSource = FillYesNoValue()
        repoValue2.DisplayMember = "Value"
        repoValue2.ValueMember = "Value"
        gv.MasterTemplate.Columns.Add(repoValue2)

        Dim repoValue1 As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoValue1.FormatString = ""
        repoValue1.Name = colVendorClass
        repoValue1.Width = 205
        repoValue1.HeaderText = "Vendor Class"
        repoValue1.ReadOnly = True
        repoValue1.DataSource = FillVendorClass()
        repoValue1.DisplayMember = "Value"
        repoValue1.ValueMember = "Value"
        If IsItemMilkType = 1 Then
            repoValue1.IsVisible = False
        Else
            repoValue1.IsVisible = True
        End If
        gv.MasterTemplate.Columns.Add(repoValue1)

        repocode = New GridViewTextBoxColumn()
        repocode.Name = colLocCode
        repocode.Width = 150
        repocode.HeaderText = "Location Code"
        repocode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repocode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repocode)

        reponame = New GridViewTextBoxColumn()
        reponame.FormatString = ""
        reponame.Name = colLocDesc
        reponame.Width = 205
        reponame.HeaderText = "Location Desc"
        reponame.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reponame)


        Dim repoValue As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoValue.FormatString = ""
        repoValue.Name = colCondtionValue
        repoValue.Width = 205
        repoValue.HeaderText = "Value"
        repoValue.ReadOnly = False
        repoValue.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoValue.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repoValue)

        repoValue1 = New GridViewComboBoxColumn()
        repoValue1.FormatString = ""
        repoValue1.Name = colStatus
        repoValue1.Width = 205
        repoValue1.HeaderText = "Status"
        repoValue1.ReadOnly = False
        repoValue1.DataSource = FillYesNoValue()
        repoValue1.DisplayMember = "Value"
        repoValue1.ValueMember = "Value"
        gv.MasterTemplate.Columns.Add(repoValue1)



        Dim repolower As GridViewDecimalColumn = New GridViewDecimalColumn()
        repolower.Name = colLower
        repolower.Width = 80
        repolower.FormatString = "{0:n3}"
        repolower.HeaderText = "Lower Range"
        repolower.DecimalPlaces = 3
        repolower.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repolower)

        Dim repoupper As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoupper.Name = colUpper
        repoupper.Width = 80
        repoupper.HeaderText = "Upper Range"
        repoupper.FormatString = "{0:n3}"
        repoupper.ReadOnly = False
        repoupper.DecimalPlaces = 3
        gv.MasterTemplate.Columns.Add(repoupper)

        Dim repoIncenDeduc As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIncenDeduc.FormatString = ""
        repoIncenDeduc.Name = colValue
        repoIncenDeduc.Width = 150
        repoIncenDeduc.HeaderText = "Incentive/Deduction"
        'repoIncenDeduc.MaxLength = 200
        repoIncenDeduc.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoIncenDeduc)

        Dim repodate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repodate.FormatString = ""
        repodate.Name = colDate
        repodate.Width = 80
        repodate.HeaderText = "Effective Date"
        gv.MasterTemplate.Columns.Add(repodate)

        repodate = New GridViewDateTimeColumn()
        repodate.FormatString = "{0:dd/MM/yyyy}"
        repodate.Name = colEndDate
        repodate.Width = 80
        repodate.HeaderText = "End Date"
        gv.MasterTemplate.Columns.Add(repodate)

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = True
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = True
        gv.EnableSorting = True
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.EnableFiltering = True
        'gv.Rows.AddNew()
    End Sub
    Function OpenParameterValueList(ByVal code As String, ByVal strValue As String) As String
        Dim qry As String = " select Value as Value from TSPL_PARAMEter_value_master "
        Dim whrcls As String = "  parameter_code='" & code & "'"
        strValue = clsCommon.myCstr(clsCommon.ShowSelectForm("ValueMaster", qry, "Value", whrcls, strValue, "Value", False))
        Return strValue
    End Function
    Function FillYesNoValue() As DataTable
        Dim dt As DataTable

        Dim qry As String = " select '' as value union all select 'Yes' as value union all select 'No' as value "

        dt = clsDBFuncationality.GetDataTable(qry)


        Return dt
    End Function

    Function FillVendorClass() As DataTable
        Dim dt As DataTable

        Dim qry As String = " select '' as value union all select 'A' as value union all select 'B' as value union all select 'C' as value union all select 'Other' as value "

        dt = clsDBFuncationality.GetDataTable(qry)


        Return dt
    End Function

    Function AllowToSave() As Boolean
        Try
            Dim code As String = ""
            Dim LocCode As String = ""
            Dim LocCode1 As String = ""
            Dim lrange As Decimal = Nothing
            Dim urange As Decimal = Nothing
            Dim value As String = Nothing
            Dim condition As String = Nothing
            Dim conditionValue As String = Nothing
            Dim code1 As String = ""
            Dim lrange1 As Decimal = Nothing
            Dim urange1 As Decimal = Nothing
            Dim isReject As Decimal = Nothing
            Dim isReject1 As Decimal = Nothing
            Dim value1 As String = Nothing
            Dim condition1 As String = Nothing
            Dim conditionValue1 As String = Nothing
            Dim Status As String = Nothing
            Dim Status1 As String = Nothing
            Dim VendorClass As String = Nothing
            Dim VendorClass1 As String = Nothing
            Dim nature As String = Nothing
            Dim EffDate As String = Nothing
            Dim EffDate1 As String = Nothing
            code = clsCommon.myCstr(gv.Rows(0).Cells(colCode).Value)

            If clsCommon.myLen(code) <= 0 Then
                Throw New Exception("Please fill atleast one parameter range")
            End If

            For ii As Integer = 0 To gv.Rows.Count - 1
                code = clsCommon.myCstr(gv.Rows(ii).Cells(colCode).Value)
                LocCode = clsCommon.myCstr(gv.Rows(ii).Cells(colLocCode).Value)
                nature = clsCommon.myCstr(gv.Rows(ii).Cells(colNature).Value)
                lrange = clsCommon.myCdbl(gv.Rows(ii).Cells(colLower).Value)
                urange = clsCommon.myCdbl(gv.Rows(ii).Cells(colUpper).Value)
                value = clsCommon.myCstr(gv.Rows(ii).Cells(colValue).Value)
                conditionValue = clsCommon.myCstr(gv.Rows(ii).Cells(colCondtionValue).Value)
                Status = clsCommon.myCstr(gv.Rows(ii).Cells(colStatus).Value)
                VendorClass = clsCommon.myCstr(gv.Rows(ii).Cells(colVendorClass).Value)
                If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colisReject).Value), "Yes") = CompairStringResult.Equal Then
                    isReject = 1
                Else
                    isReject = 0
                End If

                If IsItemMilkType = 0 Then
                    If clsCommon.myLen(VendorClass) <= 0 Then
                        Throw New Exception("Please select Vendor Class at Row no. " & (ii + 1))
                    End If
                Else
                    If clsCommon.myLen(txtMilktypeCode.Value) <= 0 Then
                        Throw New Exception("Please select Milk Type Code at Row no. " & (ii + 1))
                    End If
                End If

                If clsCommon.myLen(gv.Rows(ii).Cells(colDate).Value) > 0 Then
                    EffDate = clsCommon.GetPrintDate(gv.Rows(ii).Cells(colDate).Value, "dd/MMM/yyyy")
                Else
                    EffDate = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
                End If
                If clsCommon.myLen(LocCode) <= 0 Then
                    Throw New Exception("please fill Location code  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If
                If clsCommon.CompairString(nature, "r") = CompairStringResult.Equal Then
                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(lrange) <= 0 Then
                        Throw New Exception("please fill lower range  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(lrange) > 0 AndAlso clsCommon.myLen(urange) <= 0 Then
                        Throw New Exception("please fill Upper range  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(lrange) > 0 AndAlso (Not IsNumeric(lrange)) Then
                        Throw New Exception("lower range must be a number  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(urange) > 0 AndAlso (Not IsNumeric(urange)) Then
                        Throw New Exception(" Upper range must be a number  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(lrange) > 0 AndAlso clsCommon.myLen(urange) > 0 AndAlso IsNumeric(lrange) AndAlso IsNumeric(urange) AndAlso clsCommon.myCdbl(lrange) > clsCommon.myCdbl(urange) Then
                        Throw New Exception(" Lower range must not be larger than Upper range   At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If isReject = 0 Then
                        If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(lrange) > 0 AndAlso clsCommon.myLen(urange) > 0 AndAlso IsNumeric(lrange) AndAlso IsNumeric(urange) AndAlso clsCommon.myLen(value) <= 0 Then
                            Throw New Exception(" Please fill Incentive/Deduction Value   At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                        End If
                    End If
                ElseIf clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(colStatus) <= 0 Then
                        Throw New Exception("please select Status At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    If isReject = 0 Then
                        If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(colStatus) > 0 AndAlso clsCommon.myLen(value) <= 0 Then
                            Throw New Exception("please fill Incentive/Deduction value   At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                        End If
                    End If
                Else
                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(colCondtionValue) <= 0 Then
                        Throw New Exception("please Fill Value At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    If isReject = 0 Then
                        If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(colCondtionValue) > 0 AndAlso clsCommon.myLen(value) <= 0 Then
                            Throw New Exception("please fill Incentive/Deduction value   At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                        End If
                    End If
                End If
                If isReject = 1 Then
                    value = ""
                End If
                'For jj As Integer = 0 To gv.Rows.Count - 1
                '    code1 = clsCommon.myCstr(gv.Rows(jj).Cells(colCode).Value)
                '    LocCode1 = clsCommon.myCstr(gv.Rows(jj).Cells(colLocCode).Value)
                '    lrange1 = clsCommon.myCdbl(gv.Rows(jj).Cells(colLower).Value)
                '    urange1 = clsCommon.myCdbl(gv.Rows(jj).Cells(colUpper).Value)
                '    value1 = clsCommon.myCstr(gv.Rows(jj).Cells(colValue).Value)
                '    conditionValue1 = clsCommon.myCstr(gv.Rows(jj).Cells(colCondtionValue).Value)
                '    Status1 = clsCommon.myCstr(gv.Rows(jj).Cells(colStatus).Value)
                '    VendorClass1 = clsCommon.myCstr(gv.Rows(jj).Cells(colVendorClass).Value)

                'If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(jj).Cells(colisReject).Value), "Yes") = CompairStringResult.Equal Then
                '    isReject1 = 1
                'Else
                '    isReject1 = 0
                'End If
                'If isReject1 = 0 Then
                '    value1 = ""
                'End If
                'If clsCommon.myLen(gv.Rows(jj).Cells(colDate).Value) > 0 Then
                '    EffDate1 = clsCommon.GetPrintDate(gv.Rows(jj).Cells(colDate).Value, "dd/MMM/yyyy")
                'Else
                '    EffDate1 = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
                'End If
                'If ii <> jj AndAlso clsCommon.myLen(code) > 0 AndAlso clsCommon.CompairString(code, code1) = CompairStringResult.Equal AndAlso lrange = lrange1 AndAlso urange = urange1 AndAlso clsCommon.CompairString(conditionValue, conditionValue1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(Status, Status1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(LocCode, LocCode1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(EffDate, EffDate1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(VendorClass, VendorClass1) = CompairStringResult.Equal AndAlso isReject = isReject1 Then
                '    Throw New Exception("Duplicate Values At Row No. " + clsCommon.myCstr(CInt(jj) + 1) + " and " + (CInt(ii) + 1))
                'End If

                'Next

                If CheckDuplicateParameterInGrid(code, nature, lrange, urange, conditionValue, Status, isReject, LocCode, VendorClass, EffDate, clsCommon.myCdbl(value), ii, gv) Then
                    Throw New Exception("Duplicate Values At Row No. " & clsCommon.myCstr(CInt(ii) + 1) & " and " & (Row2 + 1))
                End If

                If clsCommon.CompairString(nature, "r") = CompairStringResult.Equal Then
                    If chkIdenticalRowsInGrid(code, LocCode, lrange, urange, value, EffDate, VendorClass, isReject, ii, gv) Then
                        Throw New Exception("Identical Rows Found   At Row No. " & clsCommon.myCstr(CInt(ii) + 1) & " and " & (Row2 + 1))
                    End If
                End If

                'If CheckDuplicateParameter(code, nature, lrange, urange, conditionValue, Status, isReject, LocCode, VendorClass, EffDate, clsCommon.myCdbl(value)) Then
                '    Throw New Exception("Value At row No " & clsCommon.myCstr(CInt(ii) + 1) & " is already in master ")
                'End If


                'If clsCommon.CompairString(nature, "r") = CompairStringResult.Equal Then
                '    If chkIdenticalRowsInMaster(code, LocCode, lrange, urange, value, EffDate, VendorClass, isReject) Then
                '        Throw New Exception("Identical Rows Found   At Row No. " & clsCommon.myCstr(CInt(ii) + 1) & " ( When Comparing existing records in master ) ")
                '    End If
                'End If

                'If chkDuplicateValuesInGrid(code, LocCode, lrange, urange, value, EffDate, nature, VendorClass, isReject, ii, gv) Then
                '    Throw New Exception("Identical Rows Found At Row no.  " + clsCommon.myCstr(CInt(ii) + 1) + "  and " & (Row2 + 1))
                'End If

            Next
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Function CheckDuplicateParameter(ByVal ParamCode As String, ByVal nature As String, ByVal LowerRange As Double, ByVal upperRange As Double, ByVal ConditionValue As String, ByVal Status As String, ByVal isreject As Integer, ByVal locCode As String, ByVal vendorclass As String, ByVal effectiveDate As Date, ByVal value As Double) As Boolean
        Dim rValue As Boolean = False
        Dim qry As String = " select count(*) from tspl_parameter_range_master where 1=1 and  "
        Dim whrCls As String = ""
        Dim whrCls1 As String = ""
        If clsCommon.CompairString(nature, "r") = CompairStringResult.Equal And isreject = 1 Then
            whrCls = "  code='" & ParamCode & "' and Lower_range=" & LowerRange & " and Upper_range=" & upperRange & " and  value=" & value & " and Loc_code='" & locCode & "' and Vendor_class='" & vendorclass & "' and convert(date,Effective_date,103)='" & clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") & "'"
        ElseIf clsCommon.CompairString(nature, "r") = CompairStringResult.Equal And isreject = 0 Then
            whrCls = "  code='" & ParamCode & "' and Lower_range=" & LowerRange & " and Upper_range=" & upperRange & " and isReject=" & isreject & " and Loc_code='" & locCode & "' and Vendor_class='" & vendorclass & "' and convert(date,Effective_date,103)='" & clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") & "'"
        ElseIf clsCommon.CompairString(nature, "a") = CompairStringResult.Equal And isreject = 1 Then
            whrCls = "  code='" & ParamCode & "' and Condition_value='" & ConditionValue & "' and value=" & value & " and Loc_code='" & locCode & "' and Vendor_class='" & vendorclass & "' and convert(date,Effective_date,103)='" & clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") & "'"
        ElseIf clsCommon.CompairString(nature, "a") = CompairStringResult.Equal And isreject = 0 Then
            whrCls = "  code='" & ParamCode & "' and Condition_value='" & ConditionValue & "' and value=" & value & " and Loc_code='" & locCode & "' and Vendor_class='" & vendorclass & "' and convert(date,Effective_date,103)='" & clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") & "'"
        ElseIf clsCommon.CompairString(nature, "b") = CompairStringResult.Equal And isreject = 1 Then
            whrCls = "  code='" & ParamCode & "' and Status='" & Status & "' and value=" & value & " and Loc_code='" & locCode & "' and Vendor_class='" & vendorclass & "' and convert(date,Effective_date,103)='" & clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") & "'"
        ElseIf clsCommon.CompairString(nature, "b") = CompairStringResult.Equal And isreject = 0 Then
            whrCls = "  code='" & ParamCode & "' and Status='" & Status & "' and value=" & value & " and Loc_code='" & locCode & "' and Vendor_class='" & vendorclass & "' and convert(date,Effective_date,103)='" & clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") & "'"
        End If
        If clsCommon.myLen(whrCls) > 0 Then
            qry = qry & whrCls
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
                rValue = True
            Else
                rValue = False
            End If
        End If
        Return rValue
    End Function

    Function CheckDuplicateParameterInGrid(ByVal ParamCode As String, ByVal nature As String, ByVal LowerRange As Double, ByVal upperRange As Double, ByVal ConditionValue As String, ByVal Status As String, ByVal isreject As Integer, ByVal locCode As String, ByVal vendorclass As String, ByVal effectiveDate As Date, ByVal value As Double, ByVal RowNo As Integer, ByVal gv1 As RadGridView) As Boolean
        Dim rValue As Boolean = False
        For i As Integer = 0 To gv1.Rows.Count - 1
            If i <> RowNo Then
                If clsCommon.CompairString(nature, "r") = CompairStringResult.Equal And isreject = 1 Then
                    If clsCommon.CompairString(ParamCode, gv1.Rows(i).Cells(colCode).Value) = CompairStringResult.Equal AndAlso LowerRange = clsCommon.myCdbl(gv1.Rows(i).Cells(colLower).Value) AndAlso upperRange = clsCommon.myCdbl(gv1.Rows(i).Cells(colUpper).Value) AndAlso value = clsCommon.myCdbl(gv1.Rows(i).Cells(colValue).Value) AndAlso clsCommon.CompairString(locCode, gv1.Rows(i).Cells(colLocCode).Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(vendorclass, gv1.Rows(i).Cells(colVendorClass).Value) = CompairStringResult.Equal AndAlso clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") = clsCommon.GetPrintDate(gv1.Rows(i).Cells(colDate).Value) Then
                        rValue = True
                        Row2 = i
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(nature, "r") = CompairStringResult.Equal And isreject = 0 Then
                    If clsCommon.CompairString(ParamCode, gv1.Rows(i).Cells(colCode).Value) = CompairStringResult.Equal AndAlso LowerRange = clsCommon.myCdbl(gv1.Rows(i).Cells(colLower).Value) AndAlso upperRange = clsCommon.myCdbl(gv1.Rows(i).Cells(colUpper).Value) AndAlso isreject = 0 AndAlso clsCommon.CompairString(locCode, gv1.Rows(i).Cells(colLocCode).Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(vendorclass, gv1.Rows(i).Cells(colVendorClass).Value) = CompairStringResult.Equal AndAlso clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") = clsCommon.GetPrintDate(gv1.Rows(i).Cells(colDate).Value) Then
                        rValue = True
                        Row2 = i
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(nature, "a") = CompairStringResult.Equal And isreject = 1 Then
                    If clsCommon.CompairString(ParamCode, gv1.Rows(i).Cells(colCode).Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(ConditionValue, gv1.Rows(i).Cells(colCondtionValue).Value) = CompairStringResult.Equal AndAlso value = clsCommon.myCdbl(gv1.Rows(i).Cells(colValue).Value) AndAlso clsCommon.CompairString(locCode, gv1.Rows(i).Cells(colLocCode).Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(vendorclass, gv1.Rows(i).Cells(colVendorClass).Value) = CompairStringResult.Equal AndAlso clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") = clsCommon.GetPrintDate(gv1.Rows(i).Cells(colDate).Value) Then
                        rValue = True
                        Row2 = i
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(nature, "a") = CompairStringResult.Equal And isreject = 0 Then
                    If clsCommon.CompairString(ParamCode, gv1.Rows(i).Cells(colCode).Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(ConditionValue, gv1.Rows(i).Cells(colCondtionValue).Value) = CompairStringResult.Equal AndAlso isreject = 0 AndAlso clsCommon.CompairString(locCode, gv1.Rows(i).Cells(colLocCode).Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(vendorclass, gv1.Rows(i).Cells(colVendorClass).Value) = CompairStringResult.Equal AndAlso clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") = clsCommon.GetPrintDate(gv1.Rows(i).Cells(colDate).Value) Then
                        rValue = True
                        Row2 = i
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(nature, "b") = CompairStringResult.Equal And isreject = 1 Then
                    If clsCommon.CompairString(ParamCode, gv1.Rows(i).Cells(colCode).Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(Status, gv1.Rows(i).Cells(colStatus).Value) = CompairStringResult.Equal AndAlso value = clsCommon.myCdbl(gv1.Rows(i).Cells(colValue).Value) AndAlso clsCommon.CompairString(locCode, gv1.Rows(i).Cells(colLocCode).Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(vendorclass, gv1.Rows(i).Cells(colVendorClass).Value) = CompairStringResult.Equal AndAlso clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") = clsCommon.GetPrintDate(gv1.Rows(i).Cells(colDate).Value) Then
                        rValue = True
                        Row2 = i
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(nature, "b") = CompairStringResult.Equal And isreject = 0 Then
                    If clsCommon.CompairString(ParamCode, gv1.Rows(i).Cells(colCode).Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(Status, gv1.Rows(i).Cells(colStatus).Value) = CompairStringResult.Equal AndAlso isreject = 0 AndAlso clsCommon.CompairString(locCode, gv1.Rows(i).Cells(colLocCode).Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(vendorclass, gv1.Rows(i).Cells(colVendorClass).Value) = CompairStringResult.Equal AndAlso clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") = clsCommon.GetPrintDate(gv1.Rows(i).Cells(colDate).Value) Then
                        rValue = True
                        Row2 = i
                        Exit For
                    End If
                End If
            End If
        Next

        Return rValue
    End Function
    Function AllowToImport(ByRef gv1 As RadGridView) As Boolean
        Try
            Dim code As String = ""
            Dim LocCode As String = ""
            Dim LocCode1 As String = ""
            Dim lrange As Decimal = Nothing
            Dim urange As Decimal = Nothing
            Dim value As String = Nothing
            Dim condition As String = Nothing
            Dim conditionValue As String = Nothing
            Dim code1 As String = ""
            Dim lrange1 As Decimal = Nothing
            Dim urange1 As Decimal = Nothing
            Dim value1 As String = Nothing
            Dim condition1 As String = Nothing
            Dim conditionValue1 As String = Nothing
            Dim Status As String = Nothing
            Dim Status1 As String = Nothing
            Dim VendorClass As String = Nothing
            Dim VendorClass1 As String = Nothing
            Dim nature As String = Nothing
            Dim EffDate As String = Nothing
            Dim EffDate1 As String = Nothing
            Dim isReject As Decimal = Nothing
            Dim isReject1 As Decimal = Nothing
            gv1.Columns.Add("isOK", "isOK")
            code = clsCommon.myCstr(gv1.Rows(0).Cells(colCode).Value)
            If clsCommon.myLen(code) <= 0 Then
                Throw New Exception("Please fill atleast one parameter range")
            End If

            For ii As Integer = 0 To gv1.Rows.Count - 1
                code = clsCommon.myCstr(gv1.Rows(ii).Cells(colCode).Value)
                If clsCommon.myLen(code) <= 0 Then
                    Throw New Exception("please fill Code At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If

                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(*) from tspl_parameter_master where code='" & code & "'")) = 0 Then
                    Throw New Exception("Parameter Code " & code & "  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + " Not found in master")
                End If
                If clsCommon.myLen(gv1.Rows(ii).Cells(colDate).Value) > 0 Then
                    EffDate = clsCommon.GetPrintDate(gv1.Rows(ii).Cells(colDate).Value, "dd/MMM/yyyy")
                Else
                    EffDate = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
                End If

                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select nature from tspl_parameter_master where code='" & code & "'"))
                lrange = clsCommon.myCdbl(gv1.Rows(ii).Cells(colLower).Value)
                urange = clsCommon.myCdbl(gv1.Rows(ii).Cells(colUpper).Value)
                value = clsCommon.myCstr(gv1.Rows(ii).Cells("Value").Value)
                LocCode = clsCommon.myCstr(gv1.Rows(ii).Cells("Loc_Code").Value)
                VendorClass = clsCommon.myCstr(gv1.Rows(ii).Cells("Vendor_Class").Value)

                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colisReject).Value), "Yes") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colisReject).Value), "1") = CompairStringResult.Equal Then
                    isReject = 1
                Else
                    isReject = 0
                End If
                If clsCommon.myLen(VendorClass) <= 0 Then
                    Throw New Exception("Please Fill Vendor Class at row no. " & (ii + 1))
                End If
                If clsCommon.CompairString(VendorClass, "A") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(VendorClass, "B") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(VendorClass, "C") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(VendorClass, "Other") <> CompairStringResult.Equal Then
                    Throw New Exception("Vendor Class Can be Either A/B/C/Other at row no. " & (ii + 1))
                End If
                If clsCommon.myLen(LocCode) <= 0 Then
                    Throw New Exception("please fill Location Code At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If

                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(*) from tspl_location_master where Location_code='" & LocCode & "'")) = 0 Then
                    Throw New Exception("Location Code " & LocCode & "  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + " Not found in master")
                End If

                conditionValue = clsCommon.myCstr(gv1.Rows(ii).Cells(colCondtionValue).Value)
                If clsCommon.CompairString(nature, "a") = CompairStringResult.Equal Then
                    If clsCommon.myLen(conditionValue) <= 0 Then
                        Throw New Exception("please fill Alphanumeric Condition Value At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select COUNT(*) from tspl_Parameter_value_master  where Parameter_CODE='" & code & "' and Value='" & conditionValue & "'")) = 0 Then
                        Throw New Exception("Parameter Alphanumeric Value " & conditionValue & "  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + " Not found in master")
                    End If
                End If

                If clsCommon.myLen(value) > 0 AndAlso isReject = 1 AndAlso clsCommon.myCdbl(value) <> 0 Then
                    Throw New Exception("You can not fill incentive/Deduction For Parameter of QC Rejection  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If

                Status = clsCommon.myCstr(gv1.Rows(ii).Cells(colStatus).Value)

                If clsCommon.CompairString(nature, "b") = CompairStringResult.Equal Then
                    If clsCommon.myLen(Status) <= 0 Then
                        Throw New Exception("please fill either Yes/No in Status Field  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    If Not (clsCommon.CompairString(Status, "Yes") = CompairStringResult.Equal Or clsCommon.CompairString(Status, "No") = CompairStringResult.Equal) Then
                        Throw New Exception("Status Can be Either Yes/No, But You have Field " & Status & "  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + " ")
                    End If
                End If
                If clsCommon.CompairString(nature, "r") = CompairStringResult.Equal Then
                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(lrange) <= 0 Then
                        Throw New Exception("please fill lower range  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(lrange) > 0 AndAlso clsCommon.myLen(urange) <= 0 Then
                        Throw New Exception("please fill Upper range  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(lrange) > 0 AndAlso (Not IsNumeric(lrange)) Then
                        Throw New Exception("lower range must be a number  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(urange) > 0 AndAlso (Not IsNumeric(urange)) Then
                        Throw New Exception(" Upper range must be a number  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(lrange) > 0 AndAlso clsCommon.myLen(urange) > 0 AndAlso IsNumeric(lrange) AndAlso IsNumeric(urange) AndAlso clsCommon.myCdbl(lrange) > clsCommon.myCdbl(urange) Then
                        Throw New Exception(" Lower range must not be larger than Upper range   At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If isReject = 0 AndAlso clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(lrange) > 0 AndAlso clsCommon.myLen(urange) > 0 AndAlso IsNumeric(lrange) AndAlso IsNumeric(urange) AndAlso clsCommon.myLen(value) <= 0 Then
                        Throw New Exception(" Please fill Incentive/Deduction Value   At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If isReject = 1 Then
                        value = ""
                    End If
                ElseIf clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(colStatus) <= 0 Then
                        Throw New Exception("please select Status At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(colStatus) > 0 AndAlso clsCommon.myLen(value) <= 0 AndAlso isReject = 0 Then
                        Throw New Exception("please fill Incentive/Deduction value   At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    If isReject = 1 Then
                        value = ""
                    End If
                Else
                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(colCondtionValue) <= 0 Then
                        Throw New Exception("please Fill Value At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    If isReject = 0 AndAlso clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(colCondtionValue) > 0 AndAlso clsCommon.myLen(value) <= 0 Then
                        Throw New Exception("please fill Incentive/Deduction value   At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    If isReject = 1 Then
                        value = ""
                    End If
                End If

                'For jj As Integer = 0 To gv1.Rows.Count - 1
                '    code1 = clsCommon.myCstr(gv1.Rows(jj).Cells(colCode).Value)
                '    LocCode1 = clsCommon.myCstr(gv1.Rows(jj).Cells("Loc_Code").Value)
                '    lrange1 = clsCommon.myCdbl(gv1.Rows(jj).Cells(colLower).Value)
                '    urange1 = clsCommon.myCdbl(gv1.Rows(jj).Cells(colUpper).Value)
                '    value1 = clsCommon.myCstr(gv1.Rows(jj).Cells("value").Value)
                '    conditionValue1 = clsCommon.myCstr(gv1.Rows(jj).Cells("Condition_value").Value)
                '    Status1 = clsCommon.myCstr(gv1.Rows(jj).Cells(colStatus).Value)
                '    VendorClass1 = clsCommon.myCstr(gv1.Rows(jj).Cells(colVendorClass).Value)

                '    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(jj).Cells(colisReject).Value), "Yes") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(jj).Cells(colisReject).Value), "1") = CompairStringResult.Equal Then
                '        isReject1 = 1
                '    Else
                '        isReject1 = 0
                '    End If
                '    If clsCommon.myLen(gv1.Rows(jj).Cells(colDate).Value) > 0 Then
                '        EffDate1 = clsCommon.GetPrintDate(gv1.Rows(jj).Cells(colDate).Value, "dd/MMM/yyyy")
                '    Else
                '        EffDate1 = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
                '    End If
                '    If ii <> jj AndAlso clsCommon.myLen(code) > 0 AndAlso clsCommon.CompairString(code, code1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(lrange, lrange1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(urange, urange1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(conditionValue, conditionValue1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(Status, Status1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(LocCode, LocCode1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(VendorClass, VendorClass1) = CompairStringResult.Equal Then
                '        Throw New Exception("Duplicate Values At Row No. " + clsCommon.myCstr(CInt(jj) + 1) + "")
                '    End If
                'Next
                If CheckDuplicateParameterInGrid(code, nature, lrange, urange, conditionValue, Status, isReject, LocCode, VendorClass, EffDate, clsCommon.myCdbl(value), ii, gv1) Then
                    Throw New Exception("Duplicate Values At Row No. " & clsCommon.myCstr(CInt(ii) + 1) & " and " & (Row2 + 1))
                End If

                If clsCommon.CompairString(nature, "r") = CompairStringResult.Equal Then
                    If chkIdenticalRowsInGrid(code, LocCode, lrange, urange, value, EffDate, VendorClass, isReject, ii, gv1) Then
                        Throw New Exception("Identical Rows Found   At Row No. " & clsCommon.myCstr(CInt(ii) + 1) & " and " & (Row2 + 1))
                    End If
                End If

                If CheckDuplicateParameter(code, nature, lrange, urange, conditionValue, Status, isReject, LocCode, VendorClass, EffDate, clsCommon.myCdbl(value)) Then
                    Throw New Exception("Value At row No " & clsCommon.myCstr(CInt(ii) + 1) & " is already in master ")
                End If


                If clsCommon.CompairString(nature, "r") = CompairStringResult.Equal Then
                    If chkIdenticalRowsInMaster(code, LocCode, lrange, urange, value, EffDate, VendorClass, isReject) Then
                        Throw New Exception("Identical Rows Found   At Row No. " & clsCommon.myCstr(CInt(ii) + 1) & " ( When Comparing existing records in master ) ")
                    End If
                End If


                Dim dtt As DataTable = clsDBFuncationality.GetDataTable("select * from tspl_parameter_range_master")
                If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
                    For jj As Integer = 0 To dtt.Rows.Count - 1
                        code1 = clsCommon.myCstr(dtt.Rows(jj)(colCode))
                        LocCode1 = clsCommon.myCstr(dtt.Rows(jj)(colLocCode))
                        lrange1 = clsCommon.myCdbl(dtt.Rows(jj)(colLower))
                        urange1 = clsCommon.myCdbl(dtt.Rows(jj)(colUpper))
                        value1 = clsCommon.myCstr(dtt.Rows(jj)(colValue))
                        conditionValue1 = clsCommon.myCstr(dtt.Rows(jj)(colCondtionValue))
                        Status1 = clsCommon.myCstr(dtt.Rows(jj)(colStatus))
                        VendorClass1 = clsCommon.myCstr(dtt.Rows(jj)(colVendorClass))
                        If clsCommon.myLen(dtt.Rows(jj)(colDate)) > 0 Then
                            EffDate1 = clsCommon.GetPrintDate(dtt.Rows(jj)(colDate), "dd/MMM/yyyy")
                        Else
                            EffDate1 = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(dtt.Rows(jj)(colisReject)), "Yes") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dtt.Rows(jj)(colisReject)), "1") = CompairStringResult.Equal Then
                            isReject1 = 1
                        Else
                            isReject1 = 0
                        End If
                        If clsCommon.myLen(code) > 0 AndAlso clsCommon.CompairString(code, code1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(lrange, lrange1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(urange, urange1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(conditionValue, conditionValue1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(Status, Status1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(LocCode, LocCode1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(EffDate, EffDate1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(VendorClass, VendorClass1) = CompairStringResult.Equal AndAlso isReject = isReject1 Then
                            ' Throw New Exception("Duplicate Values Found in Master")
                            gv1.Rows(ii).Cells("isOK").Value = "N"
                            Exit For
                        Else
                            gv1.Rows(ii).Cells("isOK").Value = "Y"
                        End If
                    Next
                End If
                '' If isReject = 0 Then
                'If chkDuplicateValuesInGrid(code, LocCode, lrange, urange, value, EffDate, nature, VendorClass, isReject, ii, gv1) Then
                '    Throw New Exception("Identical Rows Found At Row no.  " + clsCommon.myCstr(CInt(ii) + 1) + "  and " & (Row2 + 1))
                'End If
                ''End If
                ''If isReject = 0 Then
                'If chkDuplicateValuesInGridImport(code, LocCode, lrange, urange, value, EffDate, nature, VendorClass, isReject, ii, gv) Then
                '    Throw New Exception("Identical Rows Found in Master")
                '    '    gv1.Rows(ii).Cells("isOK").Value = "N"
                '    'Else
                '    '    gv1.Rows(ii).Cells("isOK").Value = "Y"
                'End If
                ''End If
            Next

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Function isBetween(ByVal val As Double, ByVal LowerRange As Double, ByVal UpperRange As Double) As Boolean
        If (val >= LowerRange AndAlso val <= UpperRange) Then
            Return True
        Else
            Return False
        End If
    End Function

    Function chkDuplicateValuesInGrid(ByVal paramCode As String, ByVal LocCode As String, ByVal LowerRange As Double, ByVal UpperRange As Double, ByVal value As String, ByVal strEffectiveDate As String, ByVal nature As String, ByVal VendorCls As String, ByVal isReject As Integer, ByVal RowNo As Integer, ByRef gv As RadGridView) As Boolean
        Dim rValue As Boolean = False
        If clsCommon.CompairString(nature, "r") = CompairStringResult.Equal Then
            Dim dtt As DataTable = clsDBFuncationality.GetDataTable("select * from tspl_parameter_range_master")
            If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
                For i As Integer = 0 To dtt.Rows.Count - 1
                    If i <> RowNo AndAlso clsCommon.CompairString(dtt.Rows(i)(colCode), paramCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(dtt.Rows(i)("Loc_Code"), LocCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.GetPrintDate(dtt.Rows(i)(colDate), "dd/MMM/yyyy"), strEffectiveDate) = CompairStringResult.Equal AndAlso clsCommon.CompairString(dtt.Rows(i)(colVendorClass), VendorCls) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(dtt.Rows(i)(colisReject)) = isReject Then
                        If Not (LowerRange = 0 AndAlso UpperRange = 0) Then
                            If isBetween(LowerRange, clsCommon.myCdbl(dtt.Rows(i)(colLower)), clsCommon.myCdbl(dtt.Rows(i)(colUpper))) OrElse isBetween(UpperRange, clsCommon.myCdbl(dtt.Rows(i)(colLower)), clsCommon.myCdbl(dtt.Rows(i)(colUpper))) OrElse isBetween(clsCommon.myCdbl(dtt.Rows(i)(colLower)), LowerRange, UpperRange) OrElse isBetween(LowerRange, clsCommon.myCdbl(dtt.Rows(i)(colUpper)), clsCommon.myCdbl(dtt.Rows(i)(colUpper))) Then
                                Row2 = i
                                rValue = True
                            End If
                        End If
                    End If
                Next
            End If
        End If
        Return rValue
    End Function
    Function chkIdenticalRowsInGrid(ByVal paramCode As String, ByVal LocCode As String, ByVal LowerRange As Double, ByVal UpperRange As Double, ByVal value As String, ByVal strEffectiveDate As String, ByVal VendorCls As String, ByVal isReject As Integer, ByVal RowNo As Integer, ByRef gv1 As RadGridView) As Boolean
        Dim rValue As Boolean = False
        If isReject = 1 Then
            For i As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(gv1.Rows(i).Cells(colCode).Value, paramCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gv1.Rows(i).Cells(colLocCode).Value, LocCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.GetPrintDate(gv1.Rows(i).Cells(colDate).Value, "dd/MMM/yyyy"), strEffectiveDate) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gv1.Rows(i).Cells(colVendorClass).Value, VendorCls) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.Rows(i).Cells(colisReject).Value) = isReject AndAlso RowNo <> i Then
                    If isBetween(LowerRange, clsCommon.myCdbl(gv1.Rows(i).Cells(colLower).Value), clsCommon.myCdbl(gv1.Rows(i).Cells(colUpper).Value)) OrElse isBetween(UpperRange, clsCommon.myCdbl(gv1.Rows(i).Cells(colLower).Value), clsCommon.myCdbl(gv1.Rows(i).Cells(colUpper).Value)) OrElse isBetween(clsCommon.myCdbl(gv1.Rows(i).Cells(colLower).Value), LowerRange, UpperRange) OrElse isBetween(LowerRange, clsCommon.myCdbl(gv1.Rows(i).Cells(colUpper).Value), clsCommon.myCdbl(gv1.Rows(i).Cells(colUpper).Value)) Then
                        Row2 = i
                        rValue = True
                        Exit For
                    End If
                End If
            Next
        Else
            For i As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(gv1.Rows(i).Cells(colCode).Value, paramCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gv1.Rows(i).Cells(colLocCode).Value, LocCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.GetPrintDate(gv1.Rows(i).Cells(colDate).Value, "dd/MMM/yyyy"), strEffectiveDate) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gv1.Rows(i).Cells(colVendorClass).Value, VendorCls) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.Rows(i).Cells(colisReject).Value) = isReject AndAlso RowNo <> i Then
                    If isBetween(LowerRange, clsCommon.myCdbl(gv1.Rows(i).Cells(colLower).Value), clsCommon.myCdbl(gv1.Rows(i).Cells(colUpper).Value)) OrElse isBetween(UpperRange, clsCommon.myCdbl(gv1.Rows(i).Cells(colLower).Value), clsCommon.myCdbl(gv1.Rows(i).Cells(colUpper).Value)) Then
                        Row2 = i
                        'OrElse isBetween(clsCommon.myCdbl(gv1.Rows(i).Cells(colLower).Value), LowerRange, UpperRange) OrElse isBetween(LowerRange, clsCommon.myCdbl(gv1.Rows(i).Cells(colUpper).Value), clsCommon.myCdbl(gv1.Rows(i).Cells(colUpper).Value))
                        rValue = True
                        Exit For
                    End If
                End If
            Next
        End If
        Return rValue
    End Function
    Function chkIdenticalRowsInMaster(ByVal paramCode As String, ByVal LocCode As String, ByVal LowerRange As Double, ByVal UpperRange As Double, ByVal value As String, ByVal strEffectiveDate As String, ByVal VendorCls As String, ByVal isReject As Integer) As Boolean
        Dim rValue As Boolean = False
        If isReject = 1 Then
            Dim dtt As DataTable = clsDBFuncationality.GetDataTable("select tspl_parameter_range_master.*,TSPL_PARAMETER_MASTER.Nature  from tspl_parameter_range_master left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_PARAMETER_RANGE_MASTER.Code  where TSPL_PARAMETER_MASTER.nature='r' and TSPL_PARAMETER_RANGE_MASTER.isReject=1  and TSPL_PARAMETER_RANGE_MASTER.code='" & paramCode & "' and TSPL_PARAMETER_RANGE_MASTER.Loc_Code='" & LocCode & "' and TSPL_PARAMETER_RANGE_MASTER.Vendor_class='" & VendorCls & "' and convert(date,TSPL_PARAMETER_RANGE_MASTER.effective_date,103)='" & clsCommon.GetPrintDate(strEffectiveDate, "dd/MMM/yyyy") & "'")
            If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
                For i As Integer = 0 To dtt.Rows.Count - 1
                    If isBetween(LowerRange, clsCommon.myCdbl(dtt.Rows(i)("Lower_range")), clsCommon.myCdbl(dtt.Rows(i)("Upper_range"))) OrElse isBetween(UpperRange, clsCommon.myCdbl(dtt.Rows(i)("Lower_range")), clsCommon.myCdbl(dtt.Rows(i)("Upper_range"))) OrElse isBetween(clsCommon.myCdbl(dtt.Rows(i)("Lower_range")), LowerRange, UpperRange) OrElse isBetween(UpperRange, clsCommon.myCdbl(dtt.Rows(i)("Lower_range")), clsCommon.myCdbl(dtt.Rows(i)("Upper_range"))) Then
                        rValue = True
                        Exit For
                    End If
                Next
            End If

        Else
            Dim dtt As DataTable = clsDBFuncationality.GetDataTable("select tspl_parameter_range_master.*,TSPL_PARAMETER_MASTER.Nature  from tspl_parameter_range_master left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_PARAMETER_RANGE_MASTER.Code  where TSPL_PARAMETER_MASTER.nature='r' and TSPL_PARAMETER_RANGE_MASTER.isReject=0  and TSPL_PARAMETER_RANGE_MASTER.code='" & paramCode & "' and TSPL_PARAMETER_RANGE_MASTER.Loc_Code='" & LocCode & "' and TSPL_PARAMETER_RANGE_MASTER.Vendor_class='" & VendorCls & "' and convert(date,TSPL_PARAMETER_RANGE_MASTER.effective_date,103)='" & clsCommon.GetPrintDate(strEffectiveDate, "dd/MMM/yyyy") & "'")
            If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
                For i As Integer = 0 To dtt.Rows.Count - 1
                    If isBetween(LowerRange, clsCommon.myCdbl(dtt.Rows(i)("Lower_range")), clsCommon.myCdbl(dtt.Rows(i)("Upper_range"))) OrElse isBetween(UpperRange, clsCommon.myCdbl(dtt.Rows(i)("Lower_range")), clsCommon.myCdbl(dtt.Rows(i)("Upper_range"))) OrElse isBetween(clsCommon.myCdbl(dtt.Rows(i)("Lower_range")), LowerRange, UpperRange) OrElse isBetween(UpperRange, clsCommon.myCdbl(dtt.Rows(i)("Lower_range")), clsCommon.myCdbl(dtt.Rows(i)("Upper_range"))) Then
                        rValue = True
                        Exit For
                    End If
                Next
            End If
        End If
        Return rValue
    End Function
    Function chkDuplicateValuesInGridImport(ByVal paramCode As String, ByVal LocCode As String, ByVal LowerRange As Double, ByVal UpperRange As Double, ByVal value As String, ByVal strEffectiveDate As String, ByVal nature As String, ByVal VendorCls As String, ByVal isReject As Integer, ByVal RowNo As Integer, ByRef gv1 As RadGridView) As Boolean
        Dim rValue As Boolean = False
        If clsCommon.CompairString(nature, "r") = CompairStringResult.Equal Then
            'Dim dtt As DataTable = clsDBFuncationality.GetDataTable("select * from tspl_parameter_range_master")
            'If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
            For i As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(gv1.Rows(i).Cells(colCode).Value, paramCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gv1.Rows(i).Cells(colLocCode).Value, LocCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.GetPrintDate(gv1.Rows(i).Cells(colDate).Value, "dd/MMM/yyyy"), strEffectiveDate) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gv1.Rows(i).Cells(colVendorClass).Value, VendorCls) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.Rows(i).Cells(colisReject).Value) = isReject Then
                    If isBetween(LowerRange, clsCommon.myCdbl(gv1.Rows(i).Cells(colLower).Value), clsCommon.myCdbl(gv1.Rows(i).Cells(colUpper).Value)) OrElse isBetween(UpperRange, clsCommon.myCdbl(gv1.Rows(i).Cells(colLower).Value), clsCommon.myCdbl(gv1.Rows(i).Cells(colUpper).Value)) OrElse isBetween(clsCommon.myCdbl(gv1.Rows(i).Cells(colLower).Value), LowerRange, UpperRange) OrElse isBetween(LowerRange, clsCommon.myCdbl(gv1.Rows(i).Cells(colUpper).Value), clsCommon.myCdbl(gv1.Rows(i).Cells(colUpper).Value)) Then
                        Row2 = i
                        rValue = True
                    End If
                End If
            Next
            'End If
        End If
        Return rValue
    End Function

    Sub SaveData()
        If CheckParameterRangerProcurementTypewise = 1 Then
            If ddlBulProcType.SelectedValue = "" Then
                clsCommon.MyMessageBoxShow(Me, "Please select Procurement Type", Me.Text)
                Exit Sub
            End If
        End If

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmParameterRangeMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim arr As New List(Of clsfrmParameterRangeMaster)


            For Each grow As GridViewRowInfo In gv.Rows
                If grow.Cells(colIsEdit).Value = True Then
                    Dim obj As New clsfrmParameterRangeMaster()
                    obj.PK_Id = clsCommon.myCstr(grow.Cells(colID).Value)
                    obj.code = clsCommon.myCstr(grow.Cells(colCode).Value)
                    obj.Loc_Code = clsCommon.myCstr(grow.Cells(colLocCode).Value)
                    obj.Lrange = clsCommon.myCdbl(grow.Cells(colLower).Value)
                    obj.Urange = clsCommon.myCdbl(grow.Cells(colUpper).Value)
                    obj.Value = clsCommon.myCstr(grow.Cells(colValue).Value)
                    obj.Status = clsCommon.myCstr(grow.Cells(colStatus).Value)
                    obj.Vendor_Class = clsCommon.myCstr(grow.Cells(colVendorClass).Value)
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colisReject).Value), "Yes") = CompairStringResult.Equal Then
                        obj.IsReject = 1
                    Else
                        obj.IsReject = 0
                    End If
                    Try
                        obj.Eff_date = Convert.ToDateTime(grow.Cells(colDate).Value)
                    Catch exx As Exception
                        obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                    End Try
                    Try
                        If clsCommon.myCstr(obj.Eff_date).Substring(6, 4) = "0001" Then
                            obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                        End If
                    Catch exx As Exception
                        obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                    End Try
                    If clsCommon.myLen(grow.Cells(colEndDate).Value) > 0 Then
                        obj.End_date = clsCommon.myCDate(grow.Cells(colEndDate).Value)
                    Else
                        obj.End_date = Nothing
                    End If


                    obj.Condition_Value = clsCommon.myCstr(grow.Cells(colCondtionValue).Value)
                    obj.MIKL_TYPE_CODE = txtMilktypeCode.Value
                    obj.Procurement_Type = ddlBulProcType.SelectedValue
                    If clsCommon.myLen(obj.code) > 0 Then
                        arr.Add(obj)
                    End If
                End If
            Next

            If clsfrmParameterRangeMaster.SaveData(arr, trans, False) Then
                trans.Commit()
                If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then

                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                End If
                btnsave.Text = "Update"
                btndelete.Enabled = True
                'UcAttachment1.SaveData("PARANGMST")
                LoadData()
            Else
                btnsave.Text = "Save"
                btndelete.Enabled = False
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Dim qry As String = "select count(*) from tspl_parameter_range_master where comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select tspl_parameter_range_master.Code ,tspl_parameter_master.Description,tspl_parameter_range_master.Loc_Code,tspl_location_master.location_desc as [Location Description],tspl_parameter_range_master.Effective_Date,tspl_parameter_range_master.Lower_Range ,tspl_parameter_range_master.Upper_Range ,tspl_parameter_range_master.Value,tspl_parameter_range_master.Condition_Value  ,tspl_parameter_range_master.Status  as [Status],tspl_parameter_range_master.Vendor_Class ,tspl_parameter_range_master.IsReject   from tspl_parameter_range_master left outer join tspl_parameter_master on tspl_parameter_range_master.code=tspl_parameter_master.code and tspl_parameter_master.comp_code=tspl_parameter_range_master.comp_code left outer join tspl_location_master on tspl_location_master.location_code=tspl_parameter_range_master.loc_code"
        Else
            qry = "select '' as Code,'' as Description,'' as [Loc_Code],'' as [Location Description],'' as [Effective_Date],'' as [Lower_Range],'' as [Upper_Range],'' as [Value],'' as Condition_Value,'' as Status,'' as [Vendor_Class],'' as [IsReject]"
        End If
        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Dim currentdate As Date = Date.Today
        Dim trans As SqlTransaction = Nothing
        If transportSql.importExcel(gv1, "Code", "Description", "Loc_Code", "Location Description", "Effective_Date", "Lower_Range", "Upper_Range", "Value", "Condition_Value", "Status", "Vendor_Class", "IsReject") Then
            Try
                clsCommon.ProgressBarShow()
                If AllowToImport(gv1) Then
                    trans = clsDBFuncationality.GetTransactin()
                    Dim arr As New List(Of clsfrmParameterRangeMaster)
                    For Each grow As GridViewRowInfo In gv1.Rows
                        Dim obj As New clsfrmParameterRangeMaster()
                        obj.code = clsCommon.myCstr(grow.Cells(colCode).Value)
                        obj.Loc_Code = clsCommon.myCstr(grow.Cells("Loc_Code").Value)
                        obj.Lrange = clsCommon.myCdbl(grow.Cells(colLower).Value)
                        obj.Urange = clsCommon.myCdbl(grow.Cells(colUpper).Value)
                        obj.Value = clsCommon.myCstr(grow.Cells("Value").Value)
                        obj.Status = clsCommon.myCstr(grow.Cells(colStatus).Value)
                        obj.Vendor_Class = clsCommon.myCstr(grow.Cells(colVendorClass).Value)
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colisReject).Value), "Yes") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colisReject).Value), "1") = CompairStringResult.Equal Then
                            obj.IsReject = 1
                        Else
                            obj.IsReject = 0
                        End If
                        Try
                            obj.Eff_date = Convert.ToDateTime(grow.Cells(colDate).Value)
                        Catch exx As Exception
                            obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                        End Try
                        Try
                            If clsCommon.myCstr(obj.Eff_date).Substring(6, 4) = "0001" Then
                                obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                            End If
                        Catch exx As Exception
                            obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                        End Try
                        'obj.Condition = clsCommon.myCstr(grow.Cells(colCondtion).Value)
                        obj.Condition_Value = clsCommon.myCstr(grow.Cells("Condition_Value").Value)
                        If clsCommon.myLen(obj.code) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells("isOK").Value), "N") <> CompairStringResult.Equal Then
                            arr.Add(obj)
                        End If
                    Next
                    If clsfrmParameterRangeMaster.SaveData(arr, trans, True) Then
                        trans.Commit()
                        clsCommon.ProgressBarHide()
                        clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
                    Else
                        trans.Rollback()
                        clsCommon.ProgressBarHide()
                        clsCommon.MyMessageBoxShow(Me, "No Data Transfer", Me.Text)
                    End If
                End If
                clsCommon.ProgressBarHide()
                Reset()
                LoadData()
            Catch ex As Exception
                Try
                    trans.Rollback()
                Catch exx As Exception
                End Try
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv1)
    End Sub

    'Private Sub gv_CellEditorInitialized(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellEditorInitialized
    '    If gv.CurrentColumn Is gv.Columns(colCode) Then
    '        Dim editor As Telerik.WinControls.UI.RadTextBoxEditor = TryCast(gv.ActiveEditor, RadTextBoxEditor)
    '        Dim oszlop As Telerik.WinControls.UI.GridViewTextBoxColumn = TryCast(gv.CurrentColumn, Telerik.WinControls.UI.GridViewTextBoxColumn)
    '        If editor IsNot Nothing And oszlop IsNot Nothing Then
    '            Dim editorElement As Telerik.WinControls.UI.RadTextBoxElement = TryCast(editor.EditorElement, RadTextBoxElement)

    '            Try
    '                RemoveHandler editorElement.KeyPress, AddressOf GV_KeyPress
    '            Catch ex As Exception
    '            End Try
    '            AddHandler editorElement.KeyPress, AddressOf GV_KeyPress
    '        End If
    '    End If
    'End Sub
    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isLoadData Then '------when on loaddata then it should not run
            If Not isValueChanged Then

                isValueChanged = True
                If gv.CurrentRow.Cells(colIsEdit).Value = False Then
                    gv.CurrentRow.Cells(colIsEdit).Value = True
                End If

                If (e.Column Is gv.Columns(colCode) OrElse e.Column Is gv.Columns(colCondtionValue) OrElse e.Column Is gv.Columns(colLocCode)) AndAlso (Not TypeOf e.Row Is GridViewFilteringRowInfo) Then
                    If clsCommon.CompairString(e.Column.Name, colCode) = CompairStringResult.Equal Then
                        If Not iscellvalue Then
                            OpenParameter()
                        End If
                        If clsCommon.myLen(gv.CurrentRow.Cells(colCode).Value) > 0 Then
                            gv.CurrentRow.Cells(colVendorClass).Value = clsCommon.myCstr(cmbVendorClass.SelectedValue)
                            gv.CurrentRow.Cells(colLocCode).Value = clsCommon.myCstr(fndLoc.Value)
                            gv.CurrentRow.Cells(colLocDesc).Value = clsCommon.myCstr(txtLocName.Text)
                        End If
                        If (clsCommon.CompairString(gv.CurrentRow.Cells(colNature).Value, "A") = CompairStringResult.Equal) Then
                            gv.CurrentRow.Cells(colCondtionValue).ReadOnly = False
                            gv.CurrentRow.Cells(colLower).ReadOnly = True
                            gv.CurrentRow.Cells(colUpper).ReadOnly = True
                            gv.CurrentRow.Cells(colStatus).ReadOnly = True
                        ElseIf (clsCommon.CompairString(gv.CurrentRow.Cells(colNature).Value, "B") = CompairStringResult.Equal) Then
                            gv.CurrentRow.Cells(colCondtionValue).ReadOnly = True
                            gv.CurrentRow.Cells(colLower).ReadOnly = True
                            gv.CurrentRow.Cells(colUpper).ReadOnly = True
                            gv.CurrentRow.Cells(colStatus).ReadOnly = False
                            'ElseIf (clsCommon.CompairString(gv.CurrentRow.Cells(colisReject).Value, "No") = CompairStringResult.Equal) Then

                            '    gv.CurrentRow.Cells(colCondtionValue).ReadOnly = True
                            '    gv.CurrentRow.Cells(colLower).ReadOnly = False
                            '    gv.CurrentRow.Cells(colUpper).ReadOnly = False
                            '    gv.CurrentRow.Cells(colStatus).ReadOnly = True
                        End If
                    ElseIf clsCommon.CompairString(e.Column.Name, colCondtionValue) = CompairStringResult.Equal Then
                        gv.CurrentRow.Cells(colCondtionValue).Value = OpenParameterValueList(gv.CurrentRow.Cells(colCode).Value, gv.CurrentRow.Cells(colCondtionValue).Value)
                    ElseIf clsCommon.CompairString(e.Column.Name, colLocCode) = CompairStringResult.Equal Then
                        OpenLocation()
                    ElseIf clsCommon.CompairString(e.Column.Name, colisReject) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colisReject).Value), "Yes") = CompairStringResult.Equal Then
                        'isLoadData = True
                        'gv.CurrentRow.Cells(colCondtionValue).ReadOnly = False
                        'gv.CurrentRow.Cells(colLower).ReadOnly = False
                        'gv.CurrentRow.Cells(colUpper).ReadOnly = False
                        'gv.CurrentRow.Cells(colStatus).ReadOnly = True
                        'gv.CurrentRow.Cells(colValue).ReadOnly = True
                        ''gv.CurrentRow.Cells(colCondtionValue).Value = ""
                        ''gv.CurrentRow.Cells(colLower).Value = 0
                        ''gv.CurrentRow.Cells(colUpper).Value = 0

                        ''gv.CurrentRow.Cells(colStatus).Value = ""
                        gv.CurrentRow.Cells(colValue).Value = 0
                        gv.CurrentRow.Cells(colValue).ReadOnly = True
                    End If

                End If

            End If
        End If
        isValueChanged = False
        iscellvalue = False
    End Sub

    Sub OpenParameter()
        iscellvalue = True
        Dim qry As String = "select Code,Description,Type,nature from tspl_parameter_master "
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("PMFND", qry, "Code", gv.CurrentRow.Cells(colCode).Value)
        If dr IsNot Nothing Then
            gv.CurrentRow.Cells(colCode).Value = clsCommon.myCstr(dr("code"))
            gv.CurrentRow.Cells(colNature).Value = clsCommon.myCstr(dr("nature"))
            gv.CurrentRow.Cells(colDesc).Value = clsCommon.myCstr(dr("description"))
        Else
            gv.CurrentRow.Cells(colCode).Value = ""
            gv.CurrentRow.Cells(colDesc).Value = ""
            gv.CurrentRow.Cells(colNature).Value = ""
        End If

    End Sub

    Sub OpenLocation()
        Dim whrCls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " location_code in ( " & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        gv.CurrentRow.Cells(colLocCode).Value = clsCommon.myCstr(clsLocation.getFinder(whrCls, gv.CurrentRow.Cells(colLocCode).Value, False))
        gv.CurrentRow.Cells(colLocDesc).Value = clsCommon.myCstr(clsLocation.GetName(clsCommon.myCstr(gv.CurrentRow.Cells(colLocCode).Value), Nothing))
    End Sub

    'Private Sub gv_CellValuePushed(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellValueEventArgs) Handles gv.CellValuePushed

    'End Sub

    'Private Sub gv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv.Click
    '    If gv.CurrentColumn Is gv.Columns(colCondtionValue) AndAlso clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(colCode).Value)) > 0 Then
    '        gv.CurrentCell.Value = clsCommon.myCstr(OpenParameterValueList(clsCommon.myCstr(gv.CurrentRow.Cells(colCode).Value), clsCommon.myCstr(gv.CurrentCell.Value)))
    '    End If
    'End Sub

    'Private Sub gv_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
    'If gv.RowCount > 0 Then
    '    Dim intCurrRow As Integer = gv.CurrentRow.Index
    '    If intCurrRow = gv.Rows.Count - 1 Then
    '        'gv.Rows.AddNew()
    '        gv.CurrentRow = gv.Rows(intCurrRow)
    '    End If
    'End If
    'End Sub

    Private Sub gv_CurrentRowChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gv.CurrentRowChanged
        If gv.CurrentRow IsNot Nothing AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentCell IsNot Nothing Then
            If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colisReject).Value), "Yes") = CompairStringResult.Equal Then
                'gv.CurrentRow.Cells(colCondtionValue).ReadOnly = True
                'gv.CurrentRow.Cells(colLower).ReadOnly = True
                'gv.CurrentRow.Cells(colUpper).ReadOnly = True
                'gv.CurrentRow.Cells(colStatus).ReadOnly = True
                'gv.CurrentRow.Cells(colCondtionValue).Value = ""
                'gv.CurrentRow.Cells(colLower).Value = 0
                'gv.CurrentRow.Cells(colUpper).Value = 0
                'gv.CurrentRow.Cells(colStatus).Value = ""
                gv.CurrentRow.Cells(colValue).Value = 0
                gv.CurrentRow.Cells(colValue).ReadOnly = True


            Else
                If (clsCommon.CompairString(gv.CurrentRow.Cells(colNature).Value, "A") = CompairStringResult.Equal) Then
                    gv.CurrentRow.Cells(colCondtionValue).ReadOnly = False
                    gv.CurrentRow.Cells(colLower).ReadOnly = True
                    gv.CurrentRow.Cells(colUpper).ReadOnly = True
                    gv.CurrentRow.Cells(colStatus).ReadOnly = True
                ElseIf (clsCommon.CompairString(gv.CurrentRow.Cells(colNature).Value, "B") = CompairStringResult.Equal) Then
                    gv.CurrentRow.Cells(colCondtionValue).ReadOnly = True
                    gv.CurrentRow.Cells(colLower).ReadOnly = True
                    gv.CurrentRow.Cells(colUpper).ReadOnly = True
                    gv.CurrentRow.Cells(colStatus).ReadOnly = False
                Else

                    gv.CurrentRow.Cells(colCondtionValue).ReadOnly = True
                    gv.CurrentRow.Cells(colLower).ReadOnly = False
                    gv.CurrentRow.Cells(colUpper).ReadOnly = False
                    gv.CurrentRow.Cells(colStatus).ReadOnly = True
                End If

            End If
        End If

    End Sub



    'Private Sub gv_UserAddedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv.UserAddedRow
    '    UpdateSlNo()
    'End Sub

    'Private Sub gv_UserAddingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserAddingRow

    'End Sub



    'Private Sub gv_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv.UserDeletedRow
    '    UpdateSlNo()
    'End Sub
    'Public Sub AllowOnlyNumeric(ByRef e As KeyPressEventArgs, Optional ByVal AllowedChar As String = "")
    '    Dim strAllowed As String() = AllowedChar.Split(",")
    '    Dim ienum As IEnumerator = strAllowed.GetEnumerator

    '    While (ienum.MoveNext)
    '        If e.KeyChar.ToString().ToLower = ienum.Current.ToString().ToLower Then
    '            Return
    '        End If
    '    End While

    '    If Not (IsNumeric(e.KeyChar) Or Asc(e.KeyChar) = 8) Then
    '        e.Handled = True
    '    End If

    'End Sub

    'Private Sub GV_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
    '    If gv.CurrentColumn Is gv.Columns(colCode) Then
    '        AllowOnlyNumeric(e, "")
    '    End If
    'End Sub
    Private Sub gv_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserDeletingRow

        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "It will delete the record permanently " + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
            Exit Sub
        Else
            Dim qry As String = " delete from TSPL_PARAMETER_RANGE_MASTER where   code= '" + clsCommon.myCstr(gv.CurrentRow.Cells(colCode).Value) + "' and lower_range='" + clsCommon.myCstr(gv.CurrentRow.Cells(colLower).Value) + "' and upper_range='" + clsCommon.myCstr(gv.CurrentRow.Cells(colUpper).Value) + "' and value='" + clsCommon.myCstr(gv.CurrentRow.Cells(colValue).Value) + "' and effective_date='" + clsCommon.myCstr(Convert.ToDateTime(gv.CurrentRow.Cells(colDate).Value).ToString("dd/MMM/yyyy")) + "'  and isnull(TSPL_PARAMETER_RANGE_MASTER.Status,'')='" + clsCommon.myCstr(gv.CurrentRow.Cells(colStatus).Value) + "' and isnull(TSPL_PARAMETER_RANGE_MASTER.Condition_value,'')='" + clsCommon.myCstr(gv.CurrentRow.Cells(colCondtionValue).Value) + "' and Loc_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colLocCode).Value) + "' and vendor_Class='" & clsCommon.myCstr(gv.CurrentRow.Cells(colVendorClass).Value) & "' and IsReject=" & IIf(clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colisReject).Value), "Yes") = CompairStringResult.Equal, 1, 0)
            clsDBFuncationality.ExecuteNonQuery(qry)
        End If

    End Sub

    Private Sub btnGO_Click(sender As Object, e As EventArgs) Handles btnGO.Click
        LoadData()
    End Sub

    Private Sub fndLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLoc._MYValidating
        Try
            Dim whrCls As String = ""
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " Location_Code in(" & objCommonVar.strCurrUserLocations & ")"
            End If
            fndLoc.Value = clsLocation.getFinder(whrCls, fndLoc.Value, isButtonClicked)
            txtLocName.Text = clsLocation.GetName(fndLoc.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If clsCommon.myLen(gv.Rows(0).Cells(colCode).Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Data Found to delete", Me.Text)
            Return
        End If

        If Not myMessages.deleteConfirm() Then
            Return
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim whrCls As String = ""
        If Not clsMccMaster.isCurrentUserHO(trans) Then

            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " where loc_Code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If

        Try
            Dim qry As String = "delete from tspl_parameter_range_master " & whrCls
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
            trans.Commit()
            Reset()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

End Class

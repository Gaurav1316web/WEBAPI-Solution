Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmWeightUomMaster
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        If btnSave.Visible = True Then
            rmExport.Enabled = True
            rmImport.Enabled = True
        Else
            rmExport.Enabled = False
            rmImport.Enabled = False
        End If
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmDepreciationField_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        AddNew()
        SetLength()
        Try
            Dim checkUnitMasterRecord As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select  count (*) from tspl_unit_master"))
            Dim chkWeightUomMasterRecord As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count (*) from TSPL_WEIGHT_UOM_MASTER"))
            If checkUnitMasterRecord = 0 AndAlso chkWeightUomMasterRecord = 0 Then
                Dim qry As String = ""
                qry = " insert into TSPL_UNIT_MASTER (Unit_Code,Unit_Desc,Conv_Factor,Created_By,Created_Date,Modify_By,Modify_Date,Comp_Code,Weight_Type,Item_Category) values ('KG','Kilogram','1','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "','" + objCommonVar.CurrentUserCode + "', '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "','Y','K')"
                clsDBFuncationality.ExecuteNonQuery(qry)
                qry = " insert into TSPL_UNIT_MASTER (Unit_Code,Unit_Desc,Conv_Factor,Created_By,Created_Date,Modify_By,Modify_Date,Comp_Code,Weight_Type,Item_Category) values ('LTR','Liter','1','" + objCommonVar.CurrentUserCode + "', '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "','" + objCommonVar.CurrentUserCode + "',  '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "','Y','L')"
                clsDBFuncationality.ExecuteNonQuery(qry)
                qry = " insert into TSPL_UNIT_MASTER (Unit_Code,Unit_Desc,Conv_Factor,Created_By,Created_Date,Modify_By,Modify_Date,Comp_Code,Weight_Type,Item_Category) values ('GM','Gram','1','" + objCommonVar.CurrentUserCode + "', '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "','" + objCommonVar.CurrentUserCode + "',  '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "','Y','K') "
                clsDBFuncationality.ExecuteNonQuery(qry)
                qry = " insert into TSPL_UNIT_MASTER (Unit_Code,Unit_Desc,Conv_Factor,Created_By,Created_Date,Modify_By,Modify_Date,Comp_Code,Weight_Type,Item_Category) values ('ML','Milliliter','1','" + objCommonVar.CurrentUserCode + "', '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "','" + objCommonVar.CurrentUserCode + "',  '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "','Y','L') "
                clsDBFuncationality.ExecuteNonQuery(qry)
                qry = " insert into TSPL_UNIT_MASTER (Unit_Code,Unit_Desc,Conv_Factor,Created_By,Created_Date,Modify_By,Modify_Date,Comp_Code,Weight_Type,Item_Category) values ('MT','Metric Ton','1','" + objCommonVar.CurrentUserCode + "', '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "','" + objCommonVar.CurrentUserCode + "',  '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "','Y','K') "
                clsDBFuncationality.ExecuteNonQuery(qry)


                qry = "insert into TSPL_WEIGHT_UOM_MASTER (Code,Description,Category,Modify_By,Modify_Date,comp_code,Created_By,Created_Date) values ('KG','Kilogram','KG', '" + objCommonVar.CurrentUserCode + "',  '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "' ,'" + objCommonVar.CurrentUserCode + "',  '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") + "')"
                clsDBFuncationality.ExecuteNonQuery(qry)
                qry = " insert into TSPL_WEIGHT_UOM_MASTER (Code,Description,Category,Modify_By,Modify_Date,comp_code,Created_By,Created_Date) values ('LTR','Liter','LTR', '" + objCommonVar.CurrentUserCode + "',  '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "' ,'" + objCommonVar.CurrentUserCode + "',  '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") + "')"
                clsDBFuncationality.ExecuteNonQuery(qry)
                qry = "insert into TSPL_WEIGHT_UOM_MASTER (Code,Description,Category,Modify_By,Modify_Date,comp_code,Created_By,Created_Date) values ('GM','Gram','KG', '" + objCommonVar.CurrentUserCode + "',  '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "' ,'" + objCommonVar.CurrentUserCode + "',  '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") + "')"
                clsDBFuncationality.ExecuteNonQuery(qry)
                qry = "insert into TSPL_WEIGHT_UOM_MASTER (Code,Description,Category,Modify_By,Modify_Date,comp_code,Created_By,Created_Date) values ('ML','Milliliter','LTR', '" + objCommonVar.CurrentUserCode + "',  '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "' ,'" + objCommonVar.CurrentUserCode + "',  '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") + "')"
                clsDBFuncationality.ExecuteNonQuery(qry)
                qry = "insert into TSPL_WEIGHT_UOM_MASTER (Code,Description,Category,Modify_By,Modify_Date,comp_code,Created_By,Created_Date) values ('MT','Metric Ton','KG', '" + objCommonVar.CurrentUserCode + "',  '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "' ,'" + objCommonVar.CurrentUserCode + "',  '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") + "')"
            End If
        Catch ex As Exception

        End Try
       

    End Sub
    Sub SetLength()
        txtCode.MyMaxLength = 12
        txtDesc.MaxLength = 100

    End Sub
    Sub AddNew()
        txtCode.MyReadOnly = False
        BlankAllControls()
        isNewEntry = True
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnSave.Text = "Save"
        LoadType()
    End Sub

    Sub BlankAllControls()
        txtCode.Value = ""
        txtDesc.Text = ""
        lblPending.Status = ERPTransactionStatus.Pending
    End Sub

    Function AllowToSave() As Boolean
        'If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Enter Code")
            txtCode.Focus()
            Return False
        End If
        'End If
        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Enter Description")
            txtDesc.Focus()
            Return False
        End If

        If clsCommon.CompairString(cboType.Text, "Select") = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow("Please Select Category")
            cboType.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub SaveData()
        Try
            If (AllowToSave()) Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmJWParameterMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim arr As New List(Of clsWeightUomMaster)
                Dim obj As New clsWeightUomMaster()
                obj.Code = txtCode.Value
                obj.Description = txtDesc.Text
                obj.Category = cboType.SelectedValue
                arr.Add(obj)
                If clsWeightUomMaster.SaveData(arr, isNewEntry) Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            txtCode.MyReadOnly = True
            Dim obj As New clsWeightUomMaster()
            obj = clsWeightUomMaster.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                txtCode.Value = obj.Code
                txtDesc.Text = obj.Description
                cboType.SelectedValue = obj.Category

                lblPending.Status = obj.POSTED
                If obj.POSTED = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                End If
            Else
                AddNew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Try
            Dim qst As String = "select count(*) from TSPL_JW_Parameter_MASTER where Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            If txtCode.MyReadOnly OrElse isButtonClicked Then
                LoadData(clsWeightUomMaster.getFinder("", txtCode.Value, isButtonClicked), NavigatorType.Current)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FrmDepreciationField_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsWeightUomMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveData()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub



    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim strDetail As String

        strDetail = " Select Code As [Code],Description As [Description], Category From TSPL_WEIGHT_UOM_MASTER "
        Dim ListImpExpColumns As List(Of String) = New List(Of String)({"Code", "Description", "Category"})
        ListImpExpColumnsMandatory = New List(Of String)({"Code", "Description", "Category"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Code"})
        transportSql.ExporttoExcel(strDetail, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim obj As clsWeightUomMaster = Nothing
        Dim currentdate As Date = Date.Today
        Dim ListImpExpColumns As List(Of String) = New List(Of String)({"Code", "Description", "Category"})
        If transportSql.importExcel(gv, ListImpExpColumns) Then
            Dim linno As Integer = 0
            Try
                Dim arr As New List(Of clsWeightUomMaster)
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsWeightUomMaster
                    linno += 1
                    Dim strcode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 30 Then
                        Throw New Exception("Length of Code should be max. 12 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Code = strcode

                    Dim strDesp As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If (String.IsNullOrEmpty(strDesp)) Or clsCommon.myLen(strDesp) > 100 Then
                        Throw New Exception("Length of Description should be max. 100 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Description = strDesp

                    Dim strType As String = clsCommon.myCstr(grow.Cells("Category").Value)
                    If clsCommon.CompairString(strType, "LTR") = CompairStringResult.Equal Then
                        strType = "LTR"
                    ElseIf clsCommon.CompairString(strType, "KG") = CompairStringResult.Equal Then
                        strType = "KG"
                    Else
                        Throw New Exception("Type Should be  'LTR', 'KG'  At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Category = strType

                    If clsCommon.myLen(strcode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_WEIGHT_UOM_MASTER  where Code='" + strcode + "' ") > 0 Then
                        isNewEntry = False
                    Else
                        isNewEntry = True
                    End If
                    arr.Add(obj)
                Next
                clsWeightUomMaster.SaveData(arr, isNewEntry)
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub


    Sub LoadType()

        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()

        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "LTR"
        dr("Name") = "LTR"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "KG"
        dr("Name") = "KG"
        dt.Rows.Add(dr)
        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
        cboType.SelectedIndex = 0
    End Sub

    Sub postData()
        Try
            If (myMessages.postConfirm()) Then
                'SaveData(True)
                clsWeightUomMaster.postData(txtCode.Value)
                clsCommon.MyMessageBoxShow("Successfully Posted")
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
   
    'Private Sub fndParamter__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    Dim typevalue As String = ""
    '    Dim qry As String = "select code,Description,type from TSPL_PARAMETER_MASTER "
    '    fndParamter.Value = clsCommon.ShowSelectForm("IMStruCode", qry, "Code", "", fndParamter.Value, "", isButtonClicked)
    '    txtCode.Value = fndParamter.Value
    '    txtDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Description from TSPL_PARAMETER_MASTER where code='" + fndParamter.Value + "'"))
    '    typevalue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  type from TSPL_PARAMETER_MASTER where code='" + fndParamter.Value + "'"))
    '    If clsCommon.CompairString(typevalue, "Fat") = CompairStringResult.Equal Then
    '        cboType.SelectedValue = "F"
    '    ElseIf clsCommon.CompairString(typevalue, "SNF") = CompairStringResult.Equal Then
    '        cboType.SelectedValue = "S"
    '    Else
    '        cboType.SelectedValue = "N"
    '    End If
    'End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        postData()
    End Sub

    Private Sub txtCode_Leave(sender As Object, e As EventArgs) Handles txtCode.Leave
        If clsCommon.myLen(txtCode.Value) > 0 Then
            Dim checkDoc As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count (*) from TSPL_WEIGHT_UOM_MASTER where code = '" + txtCode.Value + "'"))
            If checkDoc > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        End If
    End Sub
End Class


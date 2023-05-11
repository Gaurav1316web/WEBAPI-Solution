'-------------Created By Monika 02/04/2014=--------------------'
Imports System.Data.SqlClient
Imports common

Public Class FrmPriceGroupMapping
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim qry As String
    Dim dt As DataTable
    Dim IsNewEntry As Boolean = True

    Const colchkbox As Boolean = Nothing
    Const colpircecode As String = Nothing
    Const colpricedesc As String = Nothing

#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPriceGroupMapping)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            mbtnImport.Enabled = True
            mbtnExport.Enabled = True
        Else
            mbtnImport.Enabled = False
            mbtnExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub GridBind()

        grdprice.Rows.Clear()
        grdprice.DataSource = Nothing
        qry = "select distinct Price_code ,Price_Code_Desc from TSPL_PRICE_COMPONENT_MAPPING order by price_code"
        If clsCommon.myLen(txtpri_code.Value) > 0 AndAlso chkprinciple.Checked Then
            qry = "select distinct Price_code ,Price_Code_Desc from TSPL_PRICE_COMPONENT_MAPPING where vendor_code='" + txtpri_code.Value + "' order by price_code"
        End If
        dt = clsDBFuncationality.GetDataTable(qry)
        For Each dr As DataRow In dt.Rows
            grdPrice.Rows.AddNew()
            Try
                grdPrice.CurrentRow.Cells("Price Code").Value = clsCommon.myCstr(dr("Price_code").ToString())
                grdPrice.CurrentRow.Cells("Description").Value = clsCommon.myCstr(dr("Price_Code_Desc").ToString())
            Catch ex As Exception
            End Try
        Next




    End Sub

    Private Sub FrmPriceComponantMapping_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData(fndPriceGrp.Value)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Public Sub SetLength()
        fndPriceGrp.MyMaxLength = 12
        txtDesc.MaxLength = 100
        txtRemarks.MaxLength = 100
    End Sub

   
    Private Sub FrmPriceComponantMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        SetLength()
        ResetScreen()
        grdPrice.Rows.AddNew()
    End Sub

    Private Sub ResetScreen()
        txtpri_code.MendatroryField = False
        txtRemarks.Text = ""
        fndPriceGrp.Value = ""
        fndPriceGrp.MyReadOnly = False
        txtDesc.Text = ""
        txtpri_code.Enabled = False
        txtprinciple.Enabled = False
        txtpri_code.Value = ""
        txtprinciple.Text = ""
        chkprinciple.Checked = False

        GridBind()
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        grdPrice.Rows.AddNew()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            If (AllowToSave()) Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmPriceGroupMapping, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim Arr As New List(Of clsPriceGroupMapping)
                Dim ii As Integer = 0
                For Each grow As GridViewRowInfo In grdPrice.Rows
                    Dim obj As New clsPriceGroupMapping()
                    obj.Price_Code = clsCommon.myCstr(fndPriceGrp.Value)
                    obj.Price_Code_Desc = clsCommon.myCstr(txtDesc.Text)
                    obj.Remarks = clsCommon.myCstr(txtRemarks.Text)
                    obj.Price_Comp_Code = clsCommon.myCstr(grow.Cells("Price Code").Value)
                    obj.Price_Comp_Desc = clsCommon.myCstr(grow.Cells("Description").Value)

                    obj.vendorcode = ""
                    If chkprinciple.Checked Then
                        obj.vendorcode = txtpri_code.Value
                    End If

                    If grdPrice.Rows(ii).Cells("status").Value = True Then
                        obj.Status = "Y"
                    ElseIf grdPrice.Rows(ii).Cells("status").Value = False Then
                        obj.Status = "N"
                    End If

                    ii += 1
                    Arr.Add(obj)
                Next
                If (clsPriceGroupMapping.SaveData(fndPriceGrp.Value, Arr)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(fndPriceGrp.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        If fndPriceGrp.Value.Trim() = "" Then
            ShowMsg("Price Group Code can not be blank")
            fndPriceGrp.Focus()
            fndPriceGrp.Select()
            Return False
        ElseIf grdPrice.Rows.Count = 0 Then
            ShowMsg("Insert at least one Price Code")
            grdPrice.Focus()
            Return False
        ElseIf chkprinciple.Checked AndAlso clsCommon.myLen(txtpri_code.Value) <= 0 Then
            ShowMsg("Please Select Principle Code")
            txtpri_code.Focus()
            txtpri_code.Select()
            Return False
        ElseIf grdPrice.Rows.Count > 0 Then
            Dim am As Decimal = 0

            Try
                Dim dr As Telerik.WinControls.UI.GridViewRowInfo
                For Each dr In grdPrice.Rows
                    If dr.Index < grdPrice.Rows.Count Then

                        Dim drNext As Telerik.WinControls.UI.GridViewRowInfo
                        For Each drNext In grdPrice.Rows
                            If dr.Cells(0).Value.ToString() = "" Then
                                ShowMsg("Price Code  can not be blank at row positon " & dr.Index + 1 & "")
                                Return False
                            End If
                            If drNext.Index <> dr.Index Then
                                If drNext.Cells(0).Value = dr.Cells(0).Value Then
                                    ShowMsg("Duplicate price componant codes exists on rows " & dr.Index + 1 & " and " & drNext.Index + 1 & "")
                                    Return False
                                End If
                            End If
                        Next

                    End If
                Next
            Catch ex As Exception
            End Try
            
           
        Else
            Return True
        End If
        Return True
    End Function

    Private Sub ShowMsg(ByVal Msg As String)
        common.clsCommon.MyMessageBoxShow(Msg, "Price Group Mapping", MessageBoxButtons.OK, RadMessageIcon.Info)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData(fndPriceGrp.Value)
    End Sub

    Sub DeleteData(ByVal strPriceCode As String)
        Try
            If clsCommon.myLen(strPriceCode) > 0 Then
                If clsPriceGroupMapping.DeleteData(strPriceCode) Then
                    clsCommon.MyMessageBoxShow("Data deleted successfully.")
                    ResetScreen()
                End If
            Else
                clsCommon.MyMessageBoxShow("No Price Group Found To Delete.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        ResetScreen()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub mbtnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mbtnExport.Click
        Try
            Dim query As String = "select price_group_code as [Price Group Code],price_group_desc as [Price Group Description],Price_Code as [Price Code],Price_Code_Desc as [Price Code Desc],Remarks,Status,vendor_code as [Principle Code],'' as [Principle Name] from TSPL_PRICE_GROUP_MAPPING"
            transportSql.ExporttoExcel(query, Me)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub mbtnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mbtnImport.Click
        Dim Amt_AM As String = "0"
        ' Dim Cal_AM As String
        Dim Amt_DM As String = "0"
        '  Dim Cal_DM As String
        Dim Amt_EXDIS As String = "0"
        ' Dim Cal_EXDIS As String
        Dim Amt_PDISCOUNT As String = "0"
        ' Dim Cal_PDISCOUNT As String
        Dim Amt_SCHEME As String = "0"
        'Dim Cal_SCHEME As String
        Dim Amt_TM As String = "0"
        ' Dim Cal_TM As String
        Dim Amt_TPT As String = "0"
        ' Dim Cal_TPT As String
        Dim Amt_TPTOTH As String = "0"
        ' Dim Cal_TPTOTH As String
        Dim oldcode As String = ""
        Dim cunt As Integer = 0
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Dim ii As Integer = 1
        If transportSql.importExcel(dgv, "Price Group Code", "Price Group Description", "Price Code", "Price Code Desc", "Remarks", "Status", "Principle Code", "Principle Name") Then
            'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim strCurrDateTime As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
            Try
                Dim isSaved As Boolean = True
                clsCommon.ProgressBarShow()

                Dim Arr As New List(Of clsPriceGroupMapping)

                For Each grow As GridViewRowInfo In dgv.Rows
                    Dim obj As New clsPriceGroupMapping()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If strCode.Length > 20 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception(" Charge Price Group Code can not be blank or incorrect.")
                    End If
                    obj.Price_Code = strCode
                    fndPriceGrp.Value = strCode

                    If cunt = 0 Then
                        oldcode = fndPriceGrp.Value
                    End If

                    Dim strDec As String = clsCommon.myCstr(grow.Cells(1).Value)

                    obj.Price_Code_Desc = strDec


                    Dim strcode2 As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If strcode2.Length > 20 Then
                        Throw New Exception(" Invalid Price Code ")
                    End If
                    obj.Price_Comp_Code = strcode2

                    Dim strDec1 As String = clsCommon.myCstr(grow.Cells(3).Value)

                    obj.Price_Comp_Desc = strDec1


                    '---------------------------for creation of price code------------------------------------
                    qry = "select count(*) from TSPL_PRICE_COMPONENT_MAPPING where price_code='" + strcode2 + "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                    If check <= 0 Then
                        qry = "insert into TSPL_PRICE_COMPONENT_MAPPING (price_comp_code,price_comp_desc,price_code,price_code_desc,comp_code,created_by,created_date,modify_by,modify_date) values ('001','MARGIN','" + strcode2 + "','" + strDec1 + "','" + objCommonVar.CurrentCompanyCode + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "')"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                    End If
                    '--------------------------------------------------------------------------

                    Dim strCode3 As String = clsCommon.myCstr(grow.Cells(4).Value)

                    obj.Remarks = strCode3

                    Dim strDec3 As String = clsCommon.myCstr(grow.Cells(5).Value)
                    obj.Status = strDec3

                    '------------------------------------------------------
                    obj.vendorcode = clsCommon.myCstr(grow.Cells("principle code").Value)
                    Dim vendorname As String = clsCommon.myCstr(grow.Cells("principle name").Value)

                    If clsCommon.myLen(vendorname) > 0 Then
                        qry = "select vendor_code from tspl_vendor_master where vendor_name='" + vendorname + "'"
                        obj.vendorcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                        If clsCommon.myLen(obj.vendorcode) <= 0 Then
                            Throw New Exception("Please fill valid Principle Code of Principle " + vendorname + "")
                        End If
                    End If
                    '---------------------------------------------------------------

                    Try
                        isSaved = True
                        qry = "delete from TSPL_PRICE_GROUP_MAPPING_head where price_group_code='" + fndPriceGrp.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                        qry = "delete from TSPL_PRICE_GROUP_MAPPING where price_group_code='" + fndPriceGrp.Value + "' and price_code='" + strcode2 + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)

                        qry = "insert into TSPL_PRICE_GROUP_MAPPING (price_code,price_code_desc,price_group_code,remarks,price_group_desc,status,created_by,created_date,modify_by,modify_date,vendor_code,comp_code) values ('" + strcode2 + "','" + strDec1 + "','" + strCode + "','" + obj.Remarks + "','" + obj.Price_Code_Desc + "','" + obj.Status + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "','" + obj.vendorcode + "','" + objCommonVar.CurrentCompanyCode + "')"
                        clsDBFuncationality.ExecuteNonQuery(qry)

                        qry = "insert into TSPL_PRICE_GROUP_MAPPING_head (price_group_code,remarks,price_group_desc,created_by,created_date,modify_by,modify_date,vendor_code,comp_code) values ('" + obj.Price_Code + "','" + obj.Remarks + "','" + obj.Price_Code_Desc + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "','" + obj.vendorcode + "','" + objCommonVar.CurrentCompanyCode + "')"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                    Catch ex1 As Exception
                        isSaved = False
                        Throw New Exception(ex1.Message)
                    End Try
                Next


                fndPriceGrp.Value = ""
                If isSaved Then
                    'trans.Commit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
                End If
            Catch ex As Exception
                'trans.Rollback()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Error at row no:" + clsCommon.myCstr(ii) + "" + Environment.NewLine + "" + ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(dgv)
    End Sub

    Private Sub grdPrice_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
       
    End Sub

    Private Sub fndPrice__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndPriceGrp._MYValidating
        qry = "select count(*) from TSPL_PRICE_GROUP_MAPPING where Price_group_code ='" + fndPriceGrp.Value + "' "
        Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If Count <= 0 Then
            fndPriceGrp.MyReadOnly = False
        Else
            fndPriceGrp.MyReadOnly = True
        End If

        If fndPriceGrp.MyReadOnly OrElse isButtonClicked Then
            'qry = "SELECT distinct [price_group_code] as Code ,[Price_group_Desc] as [Description],Remarks FROM [TSPL_PRICE_GROUP_MAPPING]"
            '            fndPriceGrp.Value = clsCommon.ShowSelectForm("PRCMgrpFND", qry, "Code", "", fndPriceGrp.Value, "", isButtonClicked)
            fndPriceGrp.Value = clsPriceGroupMapping.getFinder("", fndPriceGrp.Value, isButtonClicked)
            LoadData(fndPriceGrp.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub fndPrice__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndPriceGrp._MYNavigator
        Try
            qry = "select count(*) from TSPL_PRICE_GROUP_MAPPING where Price_group_code='" + fndPriceGrp.Value + "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                fndPriceGrp.MyReadOnly = False
            Else
                fndPriceGrp.MyReadOnly = True
            End If
            LoadData(fndPriceGrp.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strPriceCode As String, ByVal NavType As NavigatorType)
        Try
            ResetScreen()

            If clsCommon.myLen(strPriceCode) <= 0 Then
                Return
            End If

            Dim arr As New List(Of clsPriceGroupMapping)
            arr = clsPriceGroupMapping.GetData(strPriceCode, NavType)
            Dim Count As Integer = 0
            If arr IsNot Nothing And arr.Count > 0 Then
                grdPrice.DataSource = Nothing
                grdPrice.Rows.Clear()
                For Each obj As clsPriceGroupMapping In arr
                    If Count = 0 Then
                        fndPriceGrp.Value = obj.Price_Code
                        txtDesc.Text = obj.Price_Code_Desc
                        txtRemarks.Text = obj.Remarks
                        
                    End If
                    grdPrice.Rows.AddNew()
                    grdPrice.CurrentRow.Cells("Price Code").Value = obj.Price_Comp_Code
                    grdPrice.CurrentRow.Cells("Description").Value = obj.Price_Comp_Desc

                    txtpri_code.Value = obj.vendorcode
                    chkprinciple.Checked = False
                    If clsCommon.myLen(txtpri_code.Value) > 0 Then
                        chkprinciple.Checked = True
                        txtprinciple.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" + txtpri_code.Value + "'"))
                    End If

                    If obj.Status = "Y" Then
                        grdPrice.CurrentRow.Cells("status").Value = True
                    ElseIf obj.Status = "N" Then
                        grdPrice.CurrentRow.Cells("status").Value = False
                    End If

                Next
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                IsNewEntry = False
            Else
                btnSave.Text = "Save"
                IsNewEntry = True
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkprinciple_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkprinciple.ToggleStateChanged
        txtpri_code.Enabled = False
        txtprinciple.Enabled = False
        txtpri_code.MendatroryField = False
        If chkprinciple.Checked Then
            txtpri_code.Enabled = True
            txtprinciple.Enabled = True
            txtpri_code.MendatroryField = True
        End If
    End Sub

    Private Sub txtpri_code__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtpri_code._MYValidating
        Dim qry As String = "select distinct TSPL_PRICE_COMPONENT_MAPPING.Vendor_Code as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name,(TSPL_VENDOR_MASTER.Add1+' '+TSPL_VENDOR_MASTER.Add2+' '+TSPL_VENDOR_MASTER.Add3) as Address,TSPL_VENDOR_MASTER.Vendor_Group_Code as [Vendor Group],TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc as [Group Description],TSPL_VENDOR_MASTER.City_Code_Desc as City,TSPL_VENDOR_MASTER.State,(TSPL_VENDOR_MASTER.Phone1+' '+TSPL_VENDOR_MASTER.Phone2) as Telephone,TSPL_VENDOR_MASTER.Contact_Person_Name as [Contact Person],TSPL_VENDOR_MASTER.Contact_Person_Phone as [Contact No.] from TSPL_VENDOR_MASTER right outer join TSPL_PRICE_COMPONENT_MAPPING on TSPL_PRICE_COMPONENT_MAPPING.vendor_code=tspl_vendor_master.vendor_code"
        txtpri_code.Value = clsCommon.ShowSelectForm("VNDFND", qry, "Code", "   isnull(TSPL_PRICE_COMPONENT_MAPPING.vendor_code,'')<>''", txtpri_code.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtpri_code.Value) > 0 Then
            txtprinciple.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" + txtpri_code.Value + "'"))
            GridBind()
        Else
            txtprinciple.Text = ""
            GridBind()
        End If
    End Sub
End Class

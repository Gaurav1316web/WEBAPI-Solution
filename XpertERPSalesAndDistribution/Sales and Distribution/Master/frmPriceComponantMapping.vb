'---Created By --[Pankaj Kumar Chaudhary]--against Ticket No--[BM00000002206]-Save, Update, Delete, Import, Export, SHortcut key, custom Controls
Imports System.Data.SqlClient
Imports common
Imports XpertERPEngine

Public Class FrmPriceComponantMapping
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim qry As String
    Dim dt As DataTable
    Dim IsNewEntry As Boolean = True
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.PriceComponentMapping)
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
        grdPrice.Rows.Clear()
        grdPrice.DataSource = Nothing
        qry = "select Price_Comp_code ,Price_Comp_Desc ,'Amount'as [PriceCalculationType] from TSPL_PRICE_COMPONENT_MASTER  order by Serial_Number "
        dt = clsDBFuncationality.GetDataTable(qry)
        For Each dr As DataRow In dt.Rows
            grdPrice.Rows.AddNew()
            grdPrice.CurrentRow.Cells("Price Componant Code").Value = clsCommon.myCstr(dr("Price_Comp_code"))
            grdPrice.CurrentRow.Cells("Description").Value = clsCommon.myCstr(dr("Price_Comp_Desc"))
            grdPrice.CurrentRow.Cells("Price Calculation Type").Value = clsCommon.myCstr(dr("PriceCalculationType"))
        Next
    End Sub

    Private Sub FrmPriceComponantMapping_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData(fndPrice.Value)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Public Sub SetLength()
        fndPrice.MyMaxLength = 12
        txtDesc.MaxLength = 100
        txtRemarks.MaxLength = 100
    End Sub

    Private Sub FrmPriceComponantMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        SetLength()
        ResetScreen()
        grdPrice.Columns("Amount").IsVisible = False
        '----------For Custom Fields----------
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        '---------End of For Custom Fields----
    End Sub

    Private Sub ResetScreen()
        txtpri_code.Value = ""
        txtprinciple.Text = ""
        txtRemarks.Text = ""
        fndPrice.Value = ""
        fndPrice.MyReadOnly = False
        txtDesc.Text = ""
        chkTransfer.Checked = False
        GridBind()
        btnSave.Text = "Save"
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            If (AllowToSave()) Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.PriceComponentMapping, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim Arr As New List(Of clsPriceComponentMapping)
                For Each grow As GridViewRowInfo In grdPrice.Rows
                    Dim obj As New clsPriceComponentMapping()
                    obj.principlecode = clsCommon.myCstr(txtpri_code.Value)

                    obj.Price_Code = clsCommon.myCstr(fndPrice.Value)
                    obj.Price_Code_Desc = clsCommon.myCstr(txtDesc.Text)
                    obj.Remarks = clsCommon.myCstr(txtRemarks.Text)
                    obj.Price_Comp_Code = clsCommon.myCstr(grow.Cells("Price Componant Code").Value)
                    obj.Price_Comp_Desc = clsCommon.myCstr(grow.Cells("Description").Value)
                    obj.Price_Calculation_Method = clsCommon.myCstr(grow.Cells("Price Calculation Type").Value)
                    obj.Amount = clsCommon.myCdbl(grow.Cells("Amount").Value)
                    obj.Transfer = clsCommon.myCdbl(chkTransfer.Checked)
                    Arr.Add(obj)
                Next
                If (clsPriceComponentMapping.SaveData(fndPrice.Value, Arr)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(fndPrice.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso fndPrice.Value.Trim() = "" Then
            ShowMsg("Price code can not be blank")
            fndPrice.Focus()
            Return False
        ElseIf grdPrice.Rows.Count = 0 Then
            ShowMsg("Insert at least one Price Componant")
            grdPrice.Focus()
            Return False
        ElseIf (New BAL.BALPriceComponant).GetPCMMaster(fndPrice.Value).Rows.Count > 0 And IsNewEntry Then
            ShowMsg("This price code is already in use")
            Return False
        ElseIf grdPrice.Rows.Count > 0 Then
            Dim am As Decimal = 0

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
                    If dr.Cells(2).Value.ToString.Trim() = "Percentage" Then
                        am += dr.Cells(3).Value
                        If dr.Cells("Amount").Value > 100 Then
                            ShowMsg("Percentage can not be greater than 100")
                            Return False
                        End If
                    End If
                End If
            Next
            If am > 100 Then
                ShowMsg("Total percentage can not exceed 100")
                Return False
            End If
        ElseIf grdPrice.Rows.Count > 100 Then
            ShowMsg("No of price componant can not exceed 10")
            Return False
        Else
            Return True
        End If
        Return True
    End Function

    Private Sub ShowMsg(ByVal Msg As String)
        common.clsCommon.MyMessageBoxShow(Msg, "Price Componant Mapping", MessageBoxButtons.OK, RadMessageIcon.Info)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData(fndPrice.Value)
    End Sub

    Sub DeleteData(ByVal strPriceCode As String)
        Try
            If clsCommon.myLen(strPriceCode) > 0 Then
                If clsPriceComponentMapping.DeleteData(strPriceCode) Then
                    clsCommon.MyMessageBoxShow("Data deleted successfully.")
                    ResetScreen()
                End If
            Else
                clsCommon.MyMessageBoxShow("No Customer found to delete.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

            Dim query As String = "select Price_Code as [Price Code],Price_Code_Desc as [Price Code Desc],Remarks,Price_Comp_Code as [Price Component Code], Price_Calculation_Method  as [Calculation Method],Amount,vendor_code as [Principle Code],'' as [Principle Name],Transfer as Transfer from TSPL_PRICE_COMPONENT_MAPPING "
            transportSql.ExporttoExcel(query, Me)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub mbtnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mbtnImport.Click
        Dim Amt_AM As String = "0"
        'Dim Cal_AM As String
        Dim Amt_DM As String = "0"
        'Dim Cal_DM As String
        Dim Amt_EXDIS As String = "0"
        ' Dim Cal_EXDIS As String
        Dim Amt_PDISCOUNT As String = "0"
        ' Dim Cal_PDISCOUNT As String
        Dim Amt_SCHEME As String = "0"
        ' Dim Cal_SCHEME As String
        Dim Amt_TM As String = "0"
        ' Dim Cal_TM As String
        Dim Amt_TPT As String = "0"
        'Dim Cal_TPT As String
        Dim Amt_TPTOTH As String = "0"
        'Dim Cal_TPTOTH As String
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Dim ii As Integer = 1
        If transportSql.importExcel(dgv, "Price Code", "Price Code Desc", "Remarks", "Price Component Code", "Calculation Method", "Amount") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim strCurrDateTime As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
            Try
                Dim isSaved As Boolean = True
                clsCommon.ProgressBarShow()

                For Each dgrv As GridViewRowInfo In dgv.Rows
                    Dim strPriceCode As String = clsCommon.myCstr(dgrv.Cells("Price Code").Value)
                    If clsCommon.myLen(strPriceCode) > 0 Then
                        If ii = 194 Then
                            ' Dim x As Integer
                        End If

                        If clsCommon.myLen(strPriceCode) > 12 Then
                            Throw New Exception("Check the length of Price Code At Row no:" + clsCommon.myCstr(ii))
                        End If
                        Dim strPriceComponentCode As String = clsCommon.myCstr(dgrv.Cells("Price Component Code").Value)
                        If clsCommon.myLen(strPriceCode) > 12 OrElse clsCommon.myLen(strPriceComponentCode) <= 0 Then
                            Throw New Exception("Check the length of Price Component Code")
                        End If

                        Dim qry As String = "select Price_Comp_code,Price_Comp_Desc from TSPL_PRICE_COMPONENT_MASTER where Price_Comp_code='" + strPriceComponentCode + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("Price Component Code " + strPriceComponentCode + " not exist")
                        End If
                        strPriceComponentCode = clsCommon.myCstr(dt.Rows(0)("Price_Comp_code"))

                        qry = "select Price_Component_Map_Code from TSPL_PRICE_COMPONENT_MAPPING where Price_Code='" + strPriceCode + "' and Price_Comp_Code='" + strPriceComponentCode + "'"
                        Dim strCode As String = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

                        'Dim strSubQuery As String = " '" + strPriceCode + "','" + clsCommon.myCstr(dgrv.Cells("Price Code Desc").Value) + "','" + clsCommon.myCstr(dgrv.Cells("Remarks").Value) + "'," & _
                        '"'" + objCommonVar.CurrentUserCode + "','" + strCurrDateTime + "','" + objCommonVar.CurrentUserCode + "','" + strCurrDateTime + "','" + objCommonVar.CurrentCompanyCode + "'"

                        Dim strPricceCompCode As String = clsCommon.myCstr(dgrv.Cells("Price Component Code").Value)
                        Dim Amt As Decimal = 0
                        Dim Discount_Percent As Decimal = 0
                        Dim Cal_Method As String = clsCommon.myCstr(dgrv.Cells("Calculation Method").Value)

                        Dim str As String = String.Compare(Cal_Method.Trim, "Percentage")

                        If clsCommon.CompairString(Cal_Method, "Percentage") = CompairStringResult.Equal Then
                            Discount_Percent = clsCommon.myCdbl(dgrv.Cells("Amount").Value)
                            If Discount_Percent > 100 Then
                                Throw New Exception("Percentage Never Be Greater Then 100")
                            End If
                        ElseIf clsCommon.CompairString(Cal_Method, "Amount") = CompairStringResult.Equal Then
                            Amt = clsCommon.myCdbl(dgrv.Cells("Amount").Value)
                        End If

                        '------------------------------------------------------
                        Dim vendorcode As String = clsCommon.myCstr(dgrv.Cells("principle code").Value)
                        Dim vendorname As String = clsCommon.myCstr(dgrv.Cells("principle name").Value)
                        Dim Transfer As Double = clsCommon.myCstr(dgrv.Cells("Transfer").Value)

                        If clsCommon.myLen(vendorname) > 0 Then
                            qry = "select vendor_code from tspl_vendor_master where vendor_name='" + vendorname + "'"
                            vendorcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                            If clsCommon.myLen(vendorcode) <= 0 Then
                                Throw New Exception("Please fill valid Principle Code of Principle " + vendorname + "")
                            End If
                        End If
                        '---------------------------------------------------------------

                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Price_Code_Desc", clsCommon.myCstr(dgrv.Cells("Price Code Desc").Value))
                        clsCommon.AddColumnsForChange(coll, "Price_Comp_Desc", clsCommon.myCstr(dt.Rows(0)("Price_Comp_Desc")))
                        clsCommon.AddColumnsForChange(coll, "Remarks", clsCommon.myCstr(dgrv.Cells("Remarks").Value))
                        clsCommon.AddColumnsForChange(coll, "Discount_Percent", Discount_Percent)
                        clsCommon.AddColumnsForChange(coll, "Amount", Amt)
                        clsCommon.AddColumnsForChange(coll, "Price_Calculation_Method", clsCommon.myCstr(dgrv.Cells("Calculation Method").Value))
                        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "Modify_Date", strCurrDateTime)
                        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "vendor_code", vendorcode)
                        clsCommon.AddColumnsForChange(coll, "Transfer", Transfer)

                        If strCode <= 0 Then
                            clsCommon.AddColumnsForChange(coll, "Price_Code", strPriceCode)
                            clsCommon.AddColumnsForChange(coll, "Price_Comp_Code", strPriceComponentCode)
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", strCurrDateTime)
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_COMPONENT_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_COMPONENT_MAPPING", OMInsertOrUpdate.Update, "Price_Component_Map_Code='" + strCode + "'", trans)
                        End If
                        ii = ii + 1
                    End If
                Next

                If isSaved Then
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
                End If
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Error at row no:" + clsCommon.myCstr(ii) + "" + Environment.NewLine + "" + ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(dgv)
    End Sub

    Private Sub grdPrice_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles grdPrice.CellDoubleClick
        If grdPrice.CurrentRow.Cells("Price Calculation Type").Value = "Amount" Then
            grdPrice.CurrentRow.Cells("Price Calculation Type").Value = "Percentage"
        Else
            grdPrice.CurrentRow.Cells("Price Calculation Type").Value = "Amount"
        End If
    End Sub

    Private Sub fndPrice__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndPrice._MYValidating
        qry = "select count(*) from TSPL_PRICE_COMPONENT_MAPPING where Price_code ='" + fndPrice.Value + "' "
        Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If Count <= 0 Then
            fndPrice.MyReadOnly = False
        Else
            fndPrice.MyReadOnly = True
        End If

        If fndPrice.MyReadOnly OrElse isButtonClicked Then
            'qry = "SELECT distinct [Price_code] as Code ,[Price_Code_Desc] as [Description] FROM [TSPL_PRICE_COMPONENT_MAPPING]"
            'fndPrice.Value = clsCommon.ShowSelectForm("PRCMapngFND", qry, "Code", "", fndPrice.Value, "", isButtonClicked)
            fndPrice.Value = clsPriceComponentMapping.getFinder("", fndPrice.Value, isButtonClicked)
            LoadData(fndPrice.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub fndPrice__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndPrice._MYNavigator
        Try
            qry = "select count(*) from TSPL_PRICE_COMPONENT_MAPPING where Price_code='" + fndPrice.Value + "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                fndPrice.MyReadOnly = False
            Else
                fndPrice.MyReadOnly = True
            End If
            LoadData(fndPrice.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strPriceCode As String, ByVal NavType As NavigatorType)
        Try
            ResetScreen()
            Dim arr As New List(Of clsPriceComponentMapping)
            arr = clsPriceComponentMapping.GetData(strPriceCode, NavType)
            Dim Count As Integer = 0
            If arr IsNot Nothing And arr.Count > 0 Then
                grdPrice.DataSource = Nothing
                grdPrice.Rows.Clear()
                For Each obj As clsPriceComponentMapping In arr
                    If Count = 0 Then
                        fndPrice.Value = obj.Price_Code
                        txtDesc.Text = obj.Price_Code_Desc
                        txtRemarks.Text = obj.Remarks

                        txtpri_code.Value = obj.principlecode
                        chkTransfer.Checked = obj.Transfer

                        If clsCommon.myLen(txtpri_code.Value) > 0 Then
                            txtprinciple.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" + txtpri_code.Value + "'"))
                        End If
                        ''For Custom Fields
                        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                            UcCustomFields1.GetData(obj.arrCustomFields)
                        End If
                        ''End of For Custom Fields
                    End If
                    grdPrice.Rows.AddNew()
                    grdPrice.CurrentRow.Cells("Price Componant Code").Value = obj.Price_Comp_Code
                    grdPrice.CurrentRow.Cells("Description").Value = obj.Price_Comp_Desc
                    grdPrice.CurrentRow.Cells("Price Calculation Type").Value = obj.Price_Calculation_Method
                    grdPrice.CurrentRow.Cells("Amount").Value = obj.Amount
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

    Private Sub txtpri_code__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtpri_code._MYValidating
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,(Add1+' '+Add2+' '+Add3) as Address,Vendor_Group_Code as [Vendor Group],Vendor_Group_Code_Desc as [Group Description],City_Code_Desc as City,State,(Phone1+' '+Phone2) as Telephone,Contact_Person_Name as [Contact Person],Contact_Person_Phone as [Contact No.] from TSPL_VENDOR_MASTER"
        txtpri_code.Value = clsCommon.ShowSelectForm("VNDFND", qry, "Code", "", txtpri_code.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtpri_code.Value) > 0 Then
            txtprinciple.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" + txtpri_code.Value + "'"))
        End If
    End Sub
End Class

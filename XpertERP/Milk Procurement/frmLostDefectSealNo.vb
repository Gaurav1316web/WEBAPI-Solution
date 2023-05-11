Imports common
Imports System.IO
Imports System.Data.SqlClient

'''' <summary>
'''' Created By Pankaj Jha Against Ticket No: BM00000003905 for entry of Lost/Defected Seal No on dated 13-09-2014
'''' </summary>
'''' <remarks></remarks>
Public Class FrmLostDefectSealNo
    Inherits FrmMainTranScreen
    Public Const colSelect As String = "colSelect"
    Public Const colSealNo As String = "SealNo"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As clsLostDefectSealNoHead = Nothing
    Sub reset()
        fndDocNo.Value = ""
        fndDocNo.MyReadOnly = False
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        loadBlankGv()
        txtRemarks.Text = ""
        lblPending.Status = ERPTransactionStatus.Pending
        dtpDocDate.Value = clsCommon.GETSERVERDATE()
        fndLocation.Value = clsGateEntry.getUsersDefaultLocation()
        lblLocationDec.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Dim qry As String = " select 'Lost' as value union all select 'Defect' as value "
        transportSql.FillComboBox(qry, ddlDocType, "value", "value")
        qry = " select 'Paper Seal' as value union all select 'Manual Seal' as value "
        transportSql.FillComboBox(qry, cmbSealType, "value", "value")

    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.LostDefectSealNo)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag

    End Sub
    Sub loadBlankGv()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = "Select "
        repoSelect.Name = colSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 100
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoSelect)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Seal No"
        repoCode.Name = colSealNo
        repoCode.Width = 280
        repoCode.ReadOnly = True

        gv.MasterTemplate.Columns.Add(repoCode)
        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.MasterTemplate.ShowColumnHeaders = True
        gv.EnableAlternatingRowColor = True
        gv.EnableFiltering = False
        gv.ShowFilteringRow = False
        '    gv.TableElement.TableHeaderHeight = 40

    End Sub

    Private Sub fndLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Dim whrCls As String = String.Empty
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " location_code in (" & objCommonVar.strCurrUserLocations & ") "
        End If
        fndLocation.Value = clsLocation.getFinder(whrCls, fndLocation.Value, isButtonClicked)
        lblLocationDec.Text = clsLocation.GetName(fndLocation.Value, Nothing)

    End Sub

    Private Sub FrmLostDefectSealNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            btnPost.PerformClick()
        End If
    End Sub

    Private Sub FrmLostDefectSerialNo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P For Post")
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Function AllowToSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select a Location")
                fndLocation.Focus()
                Return False
            End If
        End If
        If clsCommon.myLen(ddlDocType.Text) <= 0 Or clsCommon.CompairString(ddlDocType.Text, "Select") = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow("Please select a Document Type")
            Return False
        End If
        If clsCommon.myLen(cmbSealType.Text) <= 0 Or clsCommon.CompairString(cmbSealType.Text, "Select") = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow("Please select a Seal Type ")
            Return False
        End If
        Dim cntSelected As Integer = 0
        For i As Integer = 0 To gv.Rows.Count - 1
            If gv.Rows(i).Cells(colSelect).Value = True Then
                cntSelected = cntSelected + 1
            End If
        Next
        If cntSelected = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast one seal no")
            Return False
        End If
        Return True
    End Function


    Sub SaveData(ByVal isPosting)
        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.LostDefectSealNo, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            Dim trans As SqlTransaction = Nothing
            obj = New clsLostDefectSealNoHead()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            obj.Location_code = fndLocation.Value()
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy")
            If obj.isNewEntry Then
                obj.DOC_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(dtpDocDate.Value, "dd/MMM/yyyy"), clsDocType.LostDefectSealNo, "", obj.Location_code)
                If clsCommon.myLen(obj.DOC_No) <= 0 Then
                    clsCommon.MyMessageBoxShow("Error In Document No Genertion")
                    Exit Sub
                End If
            Else
                obj.DOC_No = clsCommon.myCstr(fndDocNo.Value)
            End If
            fndDocNo.Value = obj.DOC_No
            obj.Doc_type = clsCommon.myCstr(ddlDocType.Text)
            obj.DOC_DATE = clsCommon.myCDate(dtpDocDate.Value, "dd/MMM/yyyy")
            obj.Location_code = clsCommon.myCstr(fndLocation.Value)
            obj.remarks = clsCommon.myCstr(txtRemarks.Text)
            obj.seal_type = clsCommon.myCstr(cmbSealType.Text)
            If clsCommon.CompairString(cmbSealType.Text, "Paper Seal") = CompairStringResult.Equal Then
                obj.item_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Item_Code  from TSPL_ITEM_MASTER where Product_Type='PS' ", trans))
            End If
            If clsCommon.CompairString(cmbSealType.Text, "Manual Seal") = CompairStringResult.Equal Then
                obj.item_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Item_Code  from TSPL_ITEM_MASTER where Product_Type='MS' ", trans))
            End If
            Dim i As Integer = 0
            Dim objSeal As clsLostDefectSealNoDetails
            obj.arrObj = New List(Of clsLostDefectSealNoDetails)
            Dim cntQty As Integer = 0
            For i = 0 To gv.Rows.Count - 1
                If gv.Rows(i).Cells(colSelect).Value = True Then
                    objSeal = New clsLostDefectSealNoDetails()
                    objSeal.DOC_No = obj.DOC_No
                    objSeal.Seal_No = clsCommon.myCstr(gv.Rows(i).Cells(colSealNo).Value)
                    cntQty = cntQty + 1
                    obj.arrObj.Add(objSeal)
                End If
            Next
            obj.qty = cntQty
            obj.Modified_By = objCommonVar.CurrentUserCode
            obj.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy")
            obj.Comp_Code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy")
            End If
            If clsLostDefectSealNoHead.SaveData(obj, trans) Then
                trans.Commit()
                If Not isPosting Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully")
                    End If
                End If
                LoadData(obj.DOC_No, NavigatorType.Current)
                btnSave.Text = "Update"
                fndDocNo.MyReadOnly = True
                btnDelete.Enabled = True
                Exit Sub
            End If
            clsCommon.MyMessageBoxShow("Data Not Saved ")
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            fndDocNo.MyReadOnly = False
            trans.Rollback()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub deleteData()
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If clsDBFuncationality.getSingleValue("select count(*) from Tspl_Lost_defect_sealNo_Head where doc_no='" & fndDocNo.Value & "'", tran) > 0 Then
                    If clsCommon.MyMessageBoxShow("Want To Delete The Doc No : " & fndDocNo.Value & " ?", "Confirm", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        If clsLostDefectSealNoHead.deleteData(fndDocNo.Value, tran) Then
                            tran.Commit()
                            clsCommon.MyMessageBoxShow("Deleted successFully")
                            reset()
                        End If
                    End If
                Else
                    Throw New Exception("Doc No not Found to delete")
                End If
            Else
                Throw New Exception("Please Enter Doc  no to delete")
            End If
        Catch ex As Exception
            tran.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub



    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If AllowToSave() Then SaveData(False)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If clsCommon.myLen(fndDocNo.Value) > 0 Then
            'If deleteConfirm() Then
            deleteData()
            'End If
        Else
        clsCommon.MyMessageBoxShow("Please select a document to delete")
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        loadBlankGv()
        txtRemarks.Text = ""
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select a Location")
            fndLocation.Focus()
            Exit Sub
        End If
        Dim itmFound As Boolean = False
        If clsCommon.CompairString(cmbSealType.Text, "Paper Seal") = CompairStringResult.Equal Then
            Dim strIcode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Item_Code  from TSPL_ITEM_MASTER where Product_Type='PS' "))
            If clsCommon.myLen(strIcode) < 0 Then
            Else
                Dim qry As String = "select Auto_Sr_No as SealNo,Location_Code,Item_Code  from TSPL_SERIAL_ITEM where Item_Code ='" & strIcode & "' and Location_Code= '" & fndLocation.Value & "'and In_Out_Type='I' and Auto_Sr_No not in (select seal_no from Tspl_Lost_defect_sealNo_Details union all select seal_no from  TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details union all select seal_no from  TSPL_Milk_Transfer_In_Paper_Seal_Details)"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        gv.Rows.AddNew()
                        gv.Rows(i).Cells(colSelect).Value = False
                        gv.Rows(i).Cells(colSealNo).Value = dt.Rows(i)("SealNo")
                        itmFound = True
                    Next
                End If
            End If
        End If
        If clsCommon.CompairString(cmbSealType.Text, "Manual Seal") = CompairStringResult.Equal Then
            Dim strIcode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Item_Code  from TSPL_ITEM_MASTER where Product_Type='MS' "))
            If clsCommon.myLen(strIcode) < 0 Then
            Else
                Dim qry As String = "select Auto_Sr_No as SealNo,Location_Code,Item_Code  from TSPL_SERIAL_ITEM where Item_Code ='" & strIcode & "' and Location_Code= '" & fndLocation.Value & "'and In_Out_Type='I' and Auto_Sr_No not in (select seal_no from Tspl_Lost_defect_sealNo_Details union all select Seal_No1 as seal_No  from (select Seal_No1  from TSPL_MCC_Dispatch_Challan union all select  Seal_No2  from TSPL_MCC_Dispatch_Challan union all select Seal_No3  from TSPL_MCC_Dispatch_Challan union all select Seal_No4  from TSPL_MCC_Dispatch_Challan union all select Seal_No5  from TSPL_MCC_Dispatch_Challan union all select Seal_No6  from TSPL_MCC_Dispatch_Challan union all select Seal_No7  from TSPL_MCC_Dispatch_Challan union all select Seal_No8  from TSPL_MCC_Dispatch_Challan union all select Seal_No9  from TSPL_MCC_Dispatch_Challan union all select Seal_No10  from TSPL_MCC_Dispatch_Challan union all select New_Seal_No1  from tspl_milk_transfer_in union all select  New_Seal_No2  from tspl_milk_transfer_in union all select New_Seal_No3  from tspl_milk_transfer_in union all select New_Seal_No4  from tspl_milk_transfer_in union all select New_Seal_No5  from tspl_milk_transfer_in union all select New_Seal_No6  from tspl_milk_transfer_in union all select New_Seal_No7  from tspl_milk_transfer_in union all select New_Seal_No8  from tspl_milk_transfer_in union all select New_Seal_No9  from tspl_milk_transfer_in union all select New_Seal_No10  from tspl_milk_transfer_in  ) xx  where Seal_No1 <>'' )"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        gv.Rows.AddNew()
                        gv.Rows(i).Cells(colSelect).Value = False
                        gv.Rows(i).Cells(colSealNo).Value = dt.Rows(i)("SealNo")
                        itmFound = True
                    Next
                End If
            End If
        End If
        If Not itmFound Then
            clsCommon.MyMessageBoxShow("No Item found of type Paper Seal/Manual Seal")
        End If
    End Sub

    'Sub loadSealNo()
    '    Dim strItemCode As String = String.Empty
    '    strItemCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Item_Code  from TSPL_ITEM_MASTER where Product_Type='SN' "))


    'End Sub
    Sub LoadData(ByVal strCode As String, ByVal navType As NavigatorType)
        Try
            Dim obj As clsLostDefectSealNoHead = clsLostDefectSealNoHead.getData(fndDocNo.Value, navType)
            If obj IsNot Nothing Then
                reset()
                If obj.arrObj IsNot Nothing AndAlso obj.arrObj.Count > 0 Then
                    For i As Integer = 0 To obj.arrObj.Count - 1
                        gv.Rows.AddNew()
                        gv.Rows(i).Cells(colSelect).Value = True
                        gv.Rows(i).Cells(colSealNo).Value = obj.arrObj(i).Seal_No
                    Next
                End If
                fndDocNo.Value = obj.DOC_No
                dtpDocDate.Value = clsCommon.myCDate(obj.DOC_DATE, "dd/MM/yyyy")
                ddlDocType.Text = clsCommon.myCstr(obj.Doc_type)
                fndLocation.Value = clsCommon.myCstr(obj.Location_code)
                txtRemarks.Text = clsCommon.myCstr(obj.remarks)
                cmbSealType.Text = obj.seal_type
                lblLocationDec.Text = clsLocation.GetName(obj.Location_code, Nothing)
                If obj.isPosted = 0 Then
                    'lblPending.Status = ERPTransactionStatus.Pending
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    lblPending.Status = ERPTransactionStatus.Pending
                Else
                    '    lblPending.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    lblPending.Status = ERPTransactionStatus.Approved
                End If
            Else
                reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub PostData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                SaveData(True)
                If (clsLostDefectSealNoHead.PostData(MyBase.Form_ID, fndDocNo.Value)) Then
                    msg = "Successfully Posted"
                Else
                    qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + MyBase.Form_ID + "' "
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim level As String = dt.Rows(0)("LEVEL").ToString()
                        Dim NoOflevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))
                        If clsCommon.CompairString(level, "Level1") = CompairStringResult.Equal Then
                            msg = "Level 1 Approval done. "
                            If NoOflevel = 1 Then
                                msg += "Successfully Posted. "
                            Else
                                msg += "Level 2 Approval Required."
                            End If
                        ElseIf clsCommon.CompairString(level, "Level2") = CompairStringResult.Equal Then
                            msg = "Level 2 Approval done. "
                            If NoOflevel = 2 Then
                                msg += "Successfully Posted "
                            Else
                                msg += "Level 3 Approval Required."
                            End If
                        Else
                            msg = "Level 3 Approval done. Successfully Posted. "
                        End If
                    End If
                End If
                common.clsCommon.MyMessageBoxShow(msg)
                LoadData(fndDocNo.Value, NavigatorType.Current)
                End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub fndDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocNo._MYNavigator
        LoadData(fndDocNo.Value, NavType)
    End Sub

    Private Sub fndDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocNo._MYValidating
        Dim whrCls As String = String.Empty
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = "  location_code in (" & objCommonVar.strCurrUserLocations & ") "
        End If
        fndDocNo.Value = clsLostDefectSealNoHead.getFinder(whrCls, fndDocNo.Value, isButtonClicked)
        If clsCommon.myLen(fndDocNo.Value) > 0 Then
            LoadData(fndDocNo.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
End Class

'=========BM00000003488==========created by Monika
'==============BM00000003977------------------BM00000004016--BM00000004883
Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmCSAPriceMaster
    Inherits FrmMainTranScreen

#Region "variables"
    Dim ExciseEnableOnSalePatti As Boolean = False
    Dim ForUDLOnly As Boolean = False
    Dim AllowOtherItems As Boolean = False
    Dim ReportID As String = "CSAPRICEMASTER"
    Dim ButtonToolTip As New ToolTip()
    Dim isNewentry As Boolean = True
    Dim Errorcontrol As New clsErrorControl()
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False

    Const colSelect As String = "Select"
    Const colLineno As String = "LineNo"
    Const colItemCode As String = "ItemCode"
    Const colitemname As String = "ItemName"
    Const colUOM As String = "UOM"
    Const colDiffRate As String = "DiffRate"
    Const colLtrPerCse As String = "LtrPerCase"
    Const colCaseUOM As String = "CaseUOM"
    Const colPcsPerCase As String = "PcsPerCase"
    Const colPcsRate As String = "PcsRate"
    Const colCaseRate As String = "CaseRate"
    Const colOrgrate As String = "Rate"
    Const colMRP As String = "MRP"
    Const colIsMRP As String = "IsMRP"
    Const colDoubleClick As String = "doubleclick"
    Dim CSAPricePostedData As Boolean = False


    Const colImpDocCode As String = "colImpDocCode"
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCSAPriceMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag

        CSAPricePostedData = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowCSAPriceMasterPostedData, clsFixedParameterCode.AllowCSAPriceMasterPostedData, Nothing)) = 1, True, False)
        btnPost.Visible = MyBase.isPostFlag AndAlso CSAPricePostedData
        MyLabel10.Visible = CSAPricePostedData

        btnexport.Enabled = MyBase.isExport
        btnImport.Enabled = MyBase.isExport

        btnexport_detail.Visibility = ElementVisibility.Collapsed
        btnexport_location.Visibility = ElementVisibility.Collapsed
        btnimport_detail.Visibility = ElementVisibility.Collapsed
        btnimport_location.Visibility = ElementVisibility.Collapsed
        '--------------------------------------------------

        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub LoadLocation()
        Dim qry As String = "select Location_Code as Code,location_desc as Location from tspl_location_master where (isnull(csa_type,'N')='N' or isnull(csa_type,'')='')"
        qry += " and (isnull(git_type,'N')='N' or isnull(git_type,'')='') and (isnull(rejected_type,'N')='N' or isnull(rejected_type,'')='')"
        'qry += " and location_code not in (select location_code from TSPL_CSA_LOCATION_DETAIL where doc_no<>'" + txtCode.Value + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cbgLocation.DataSource = Nothing
        cbgLocation.DataSource = dt
        cbgLocation.DisplayMember = "Location"
        cbgLocation.ValueMember = "Code"
    End Sub

    Private Sub LoadCustomer()
        cbgCustomer.DataSource = Nothing
        Dim qry As String = "select cust_code as Code,customer_name as [Name] from tspl_customer_master where coalesce(csa_type,'N')='Y' "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub

    Private Sub FunReset()
        txtCode.Value = ""
        dtpDocDate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtDesc.Text = ""
        txtDesc.Text = clsCSAPriceMaster.GetLastDescription()
        txtuomName.Text = ""
        fndUOM.Value = ""
        chkSelect.Checked = False
        chkSelect.Text = ""
        txtrate.Text = ""
        cmbCSAType.SelectedValue = "CPD-DESI GHEE"

        fndUOM.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_unit_master where isnull(rt_rate,'N')='Y'"))
        txtuomName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_desc from tspl_unit_master where unit_code='" + fndUOM.Value + "'"))

        ChkOtherItem.Checked = False
        ChkOtherItem.Enabled = True
        dtpExpirydate.Text = clsCommon.GETSERVERDATE(Nothing).AddMonths(1)
        txtRevisionNo.Text = ""
        chkCustAll.IsChecked = False
        chkCustSelect.IsChecked = True
        cbgCustomer.Enabled = True
        cbgCustomer.UnCheckedAll()
        RadGroupBox3.Enabled = False ''is enabled when otheritem
        RadGroupBox5.Enabled = True
        RadGroupBox2.Enabled = True

        gv.Rows.Clear()
        txtTotalRow.Text = "Total Rows: 0"

        FillGrid()

        cmbTax.SelectedValue = ""
        LoadLocation()
        rbtnlocslct.IsChecked = True
        cbgLocation.UnCheckedAll()

        chkStateSelect.IsChecked = True
        cmbState.UnCheckedAll()

        isNewentry = True
        txtCode.MyReadOnly = True

        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnPost.Enabled = False

        cmbCSAType.Enabled = True
        cmbTax.Enabled = True
        fndUOM.Enabled = False

        txtrate.Enabled = True
        cmbCSAType.MendatroryField = True
        txtrate.MendatroryField = True

        If ForUDLOnly Then
            cmbTax.SelectedValue = "No"
        End If

        fndUOM.Focus()
        fndUOM.Select()

        btnAmendment.Visible = False

        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If ForUDLOnly AndAlso clsCommon.myLen(cmbCSAType.SelectedValue) <= 0 Then
                cmbCSAType.SelectedValue = "CPD-DESI GHEE"
            End If
            If ForUDLOnly AndAlso clsCommon.myLen(cmbTax.SelectedValue) <= 0 Then
                cmbTax.SelectedValue = "No"
            End If

            If Not ChkOtherItem.Checked AndAlso (clsCommon.CompairString(cmbCSAType.SelectedValue, "None") = CompairStringResult.Equal OrElse clsCommon.myLen(cmbCSAType.Text) <= 0) Then
                cmbCSAType.Select()
                cmbCSAType.Focus()
                RadPageView1.SelectedPage = RadPageViewPage1
                Errorcontrol.SetError(cmbCSAType, "Select CSA Type.")
                Throw New Exception("Select CSA Type.")
            Else
                Errorcontrol.ResetError(cmbCSAType)
            End If

            If clsCommon.CompairString(cmbTax.SelectedValue, "") = CompairStringResult.Equal Then
                RadPageView1.SelectedPage = RadPageViewPage1
                cmbTax.Select()
                Errorcontrol.SetError(cmbTax, "Select Tax Type.")
                Throw New Exception("Select Tax Type.")
            Else
                Errorcontrol.ResetError(cmbTax)
            End If

            If Not ChkOtherItem.Checked AndAlso clsCommon.myLen(fndUOM.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                fndUOM.Focus()
                fndUOM.Select()
                Errorcontrol.SetError(txtuomName, "Select CSA UOM Detail.")
                Throw New Exception("Select CSA UOM Detail.")
            Else
                Errorcontrol.ResetError(txtuomName)
            End If

            If Not ChkOtherItem.Checked AndAlso (clsCommon.myLen(txtrate.Text) <= 0 OrElse clsCommon.myCdbl(txtrate.Text) <= 0) Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtrate.Focus()
                txtrate.Select()
                Errorcontrol.SetError(txtrate, "Fill Rate For CSA.")
                Throw New Exception("Fill Rate For CSA.")
            Else
                Errorcontrol.ResetError(txtrate)
            End If

            If AllowOtherItems AndAlso (dtpExpirydate.Text Is Nothing OrElse clsCommon.myLen(dtpExpirydate.Text) <= 0 OrElse Not IsDate(dtpExpirydate.Text)) Then
                RadPageView1.SelectedPage = RadPageViewPage1
                dtpExpirydate.Focus()
                dtpExpirydate.Select()
                Errorcontrol.SetError(dtpExpirydate, "Select expiry date for document.")
                Throw New Exception("Please select expiry date for document.")
            Else
                Errorcontrol.ResetError(dtpExpirydate)
            End If

            If AllowOtherItems AndAlso clsCommon.myCDate(dtpExpirydate.Text) <= clsCommon.myCDate(dtpDocDate.Text) Then
                RadPageView1.SelectedPage = RadPageViewPage1
                dtpExpirydate.Focus()
                dtpExpirydate.Select()
                Errorcontrol.SetError(dtpExpirydate, "Expiry date should be greater then Document date.")
                Throw New Exception("Expiry date should be greater then Document date.")
            Else
                Errorcontrol.ResetError(dtpExpirydate)
            End If

            If cbgLocation.CheckedValue.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage3
                cbgLocation.Select()
                cbgLocation.Focus()
                Errorcontrol.SetError(cbgLocation, "Select location for price master.")
                Throw New Exception("Select location for price master.")
            Else
                Errorcontrol.ResetError(cbgLocation)
            End If

            If Not ChkOtherItem.Checked AndAlso cmbState.CheckedValue.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage3
                cmbState.Select()
                cmbState.Focus()
                Errorcontrol.SetError(cmbState, "Select customer state(s) for price master.")
                Throw New Exception("Select customer state(s) for price master.")
            Else
                Errorcontrol.ResetError(cmbState)
            End If

            If ChkOtherItem.Checked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage3
                cbgCustomer.Select()
                cbgCustomer.Focus()
                Errorcontrol.SetError(cbgCustomer, "Select customer for price master.")
                Throw New Exception("Select customer for price master.")
            Else
                Errorcontrol.ResetError(cbgCustomer)
            End If


            Dim qry As String = "select TSPL_CSA_PRICE_HEAD.doc_no from TSPL_CSA_PRICE_HEAD " & _
                    " left outer join TSPL_CSA_PRICE_STATE_DETAIL on TSPL_CSA_PRICE_HEAD.Doc_No=TSPL_CSA_PRICE_STATE_DETAIL.Doc_No left outer join TSPL_CSA_LOCATION_DETAIL on TSPL_CSA_LOCATION_DETAIL.Doc_No=TSPL_CSA_PRICE_HEAD.Doc_No " & _
                    " left outer join TSPL_CSA_PRICE_CSA_DETAIL on TSPL_CSA_PRICE_CSA_DETAIL.doc_no=TSPL_CSA_PRICE_HEAD.doc_no "
            qry += " where TSPL_CSA_PRICE_HEAD.doc_no <>'" + txtCode.Value + "' and TSPL_CSA_PRICE_HEAD.tax='" + cmbTax.SelectedValue + "'  " & _
                   " and TSPL_CSA_LOCATION_DETAIL.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            If AllowOtherItems AndAlso ChkOtherItem.Checked Then
                qry += " and TSPL_CSA_PRICE_HEAD.For_Other_Item=1 and TSPL_CSA_PRICE_CSA_DETAIL.cust_code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Else
                qry += " and TSPL_CSA_PRICE_STATE_DETAIL.State_Code in (" + clsCommon.GetMulcallString(cmbState.CheckedValue) + ")  and TSPL_CSA_PRICE_HEAD.csa_type='" + cmbCSAType.SelectedValue + "' and isnull(TSPL_CSA_PRICE_HEAD.For_Other_Item,0) =0 "
            End If

            'DONE BY STUTI ON 08/12/2016 FOR DATE
            qry += " AND convert(date,TSPL_CSA_PRICE_HEAD.DOC_DATE,103)='" + clsCommon.GetPrintDate(dtpDocDate.Text, "dd/MMM/yyyy") + "'"
            '=================END HERE=============

            Dim check As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

            If clsCommon.myLen(check) > 0 Then
                RadPageView1.SelectedPage = RadPageViewPage3
                If AllowOtherItems AndAlso ChkOtherItem.Checked Then
                    Throw New Exception("Selected Customer already mapped ,for more change edit old record (" + clsCommon.myCstr(check) + ") dated (" + clsCommon.GetPrintDate(dtpDocDate.Text, "dd/MM/yyyy") + ").")
                Else
                    Throw New Exception("Selected locations already mapped with selected states,for more change edit old record (" + clsCommon.myCstr(check) + ") dated (" + clsCommon.GetPrintDate(dtpDocDate.Text, "dd/MM/yyyy") + ").")
                End If
            End If

            If gv.Rows.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Throw New Exception("No record found for selected CSA to save.")
            End If

            Dim ii As Integer = 0
            For Each grow As GridViewRowInfo In gv.Rows
                If AllowOtherItems AndAlso ChkOtherItem.Checked Then
                    If clsCommon.myCBool(grow.Cells(colSelect).Value) = True Then
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 AndAlso clsCommon.myCdbl(grow.Cells(colDiffRate).Value) <= 0 Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            gv.CurrentRow = gv.Rows(grow.Index)
                            gv.CurrentColumn = gv.Columns(colItemCode)
                            Throw New Exception("Please fill unit rate at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If

                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(grow.Cells(colCaseUOM).Value)) <= 0 Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            gv.CurrentRow = gv.Rows(grow.Index)
                            gv.CurrentColumn = gv.Columns(colCaseUOM)
                            Throw New Exception("Please fill unit at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If

                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 AndAlso (grow.Tag Is Nothing OrElse TryCast(grow.Tag, List(Of clsCSAPriceDetail)).Count <= 0) Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            gv.CurrentRow = gv.Rows(grow.Index)
                            gv.CurrentColumn = gv.Columns(colItemCode)
                            Throw New Exception("Fill alternate unit rates at row no." + clsCommon.myCstr(grow.Index + 1) + ".")
                        End If
                        If ExciseEnableOnSalePatti AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colIsMRP).Value), "1") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(grow.Cells(colMRP).Value) <= 0 Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            gv.CurrentRow = gv.Rows(grow.Index)
                            gv.CurrentColumn = gv.Columns(colMRP)
                            Throw New Exception("Fill MRP at row no." + clsCommon.myCstr(grow.Index + 1) + "")
                        End If

                        ii += 1
                    End If ''when checked only then validation check
                Else
                    If clsCommon.myCBool(grow.Cells(colSelect).Value) = True Then
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 AndAlso clsCommon.myLen(grow.Cells(colDiffRate).Value) <= 0 Then ''because rate could be 0 ,-1 or else
                            RadPageView1.SelectedPage = RadPageViewPage1
                            gv.CurrentRow = gv.Rows(grow.Index)
                            gv.CurrentColumn = gv.Columns(colItemCode)
                            Throw New Exception("Please fill difference rate at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If

                        'If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 AndAlso clsCommon.myCdbl(grow.Cells(colLtrPerCse).Value) <= 0 Then
                        '    Throw New Exception("Please fill ltr per case at row no. " + clsCommon.myCstr(ii + 1) + "")
                        'End If

                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(grow.Cells(colCaseUOM).Value)) <= 0 Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            gv.CurrentRow = gv.Rows(grow.Index)
                            gv.CurrentColumn = gv.Columns(colCaseUOM)
                            Throw New Exception("Please fill case unit at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If

                        If ExciseEnableOnSalePatti AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colIsMRP).Value), "1") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(grow.Cells(colMRP).Value) <= 0 Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            gv.CurrentRow = gv.Rows(grow.Index)
                            gv.CurrentColumn = gv.Columns(colMRP)
                            Throw New Exception("Fill MRP at row no." + clsCommon.myCstr(grow.Index + 1) + "")
                        End If

                        ii += 1
                    End If
                End If ''end if cond.
            Next

            If ii <= 0 Then
                Throw New Exception("Please select atleast one row in grid.")
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData(False)
    End Sub

    Private Function SaveData(ByVal isPriceAmended As Boolean) As Boolean
        Dim obj As New clsCSAPriceMaster()

        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmCSAPriceMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return False
                End If
            End If

            RefreshLineNo()
            obj.Arr = New List(Of clsCSAPriceDetail)
            obj.Arr_Loc = New List(Of clsCSALocationDetail)
            obj.Arr_State = New List(Of clsCSAPriceStateDetail)
            obj.Arr_CSA = New List(Of clsCSAPriceCustomerDetail)

            obj.docno = clsCommon.myCstr(txtCode.Value)
            obj.Description = clsCommon.myCstr(txtDesc.Text)
            obj.csatype = clsCommon.myCstr(cmbCSAType.SelectedValue)
            obj.csauom = clsCommon.myCstr(fndUOM.Value)
            obj.rate = clsCommon.myCdbl(txtrate.Text)
            obj.taxtype = clsCommon.myCstr(cmbTax.SelectedValue)

            obj.ForOtherItem = ChkOtherItem.Checked
            If dtpDocDate.Text IsNot Nothing AndAlso clsCommon.myLen(dtpDocDate.Text) > 0 AndAlso IsDate(dtpDocDate.Text) Then
                obj.Doc_Date = clsCommon.myCDate(dtpDocDate.Text)
            End If
            obj.Revision_No = clsCommon.myCstr(txtRevisionNo.Text)
            If dtpExpirydate.Text IsNot Nothing AndAlso clsCommon.myLen(dtpExpirydate.Text) > 0 AndAlso IsDate(dtpExpirydate.Text) Then
                obj.Expiry_Date = clsCommon.myCDate(dtpExpirydate.Text)
            End If


            For Each grow As GridViewRowInfo In gv.Rows
                Dim objtr As New clsCSAPriceDetail()

                If clsCommon.myCBool(grow.Cells(colSelect).Value) = True Then
                    objtr.lineno = clsCommon.myCdbl(grow.Cells(colLineno).Value)
                    objtr.itemcode = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objtr.uom = clsCommon.myCstr(fndUOM.Value)
                    objtr.diffrate = clsCommon.myCdbl(grow.Cells(colDiffRate).Value)
                    objtr.ltr_per_case = clsCommon.myCdbl(grow.Cells(colLtrPerCse).Value)
                    objtr.pcs_per_case = clsCommon.myCdbl(grow.Cells(colPcsPerCase).Value)
                    objtr.org_rate = clsCommon.myCdbl(grow.Cells(colOrgrate).Value)
                    objtr.pcs_rate = clsCommon.myCdbl(grow.Cells(colPcsRate).Value)
                    objtr.case_rate = clsCommon.myCdbl(grow.Cells(colCaseRate).Value)
                    objtr.Case_UOM = clsCommon.myCstr(grow.Cells(colCaseUOM).Value)
                    objtr.MRP = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                    objtr.ForOtherItem = obj.ForOtherItem
                    objtr.Arr_OtherItem = New List(Of clsCSAPriceDetail)

                    If objtr.ForOtherItem Then
                        objtr.Arr_OtherItem = TryCast(grow.Tag, List(Of clsCSAPriceDetail))
                    End If

                    If clsCommon.myLen(objtr.itemcode) > 0 Then
                        obj.Arr.Add(objtr)
                    End If
                End If
            Next

            If cbgLocation.CheckedValue.Count > 0 Then
                For Each Location As String In cbgLocation.CheckedValue
                    Dim objtr As New clsCSALocationDetail()

                    objtr.docno = clsCommon.myCstr(txtCode.Value)
                    objtr.loc_code = clsCommon.myCstr(Location)

                    If clsCommon.myLen(objtr.loc_code) > 0 Then
                        obj.Arr_Loc.Add(objtr)
                    End If
                Next
            End If

            If cmbState.CheckedValue.Count > 0 Then
                For Each State As String In cmbState.CheckedValue
                    Dim objtr As New clsCSAPriceStateDetail()

                    objtr.DocNo = clsCommon.myCstr(txtCode.Value)
                    objtr.State_Code = clsCommon.myCstr(State)

                    If clsCommon.myLen(objtr.State_Code) > 0 Then
                        obj.Arr_State.Add(objtr)
                    End If
                Next
            End If

            If cbgCustomer.CheckedValue.Count > 0 Then
                For Each Cust As String In cbgCustomer.CheckedValue
                    Dim objtr As New clsCSAPriceCustomerDetail()

                    objtr.Doc_No = clsCommon.myCstr(txtCode.Value)
                    objtr.Cust_Code = clsCommon.myCstr(Cust)

                    If clsCommon.myLen(objtr.Cust_Code) > 0 Then
                        obj.Arr_CSA.Add(objtr)
                    End If
                Next
            End If

            If clsCSAPriceMaster.SaveData(obj, isNewentry) Then
                If Not isPriceAmended Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                End If
                txtCode.Value = obj.docno
                UcAttachment1.SaveData(txtCode.Value)

                LoadData(txtCode.Value, NavigatorType.Current)
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            Dim msg As String = ""
            If (myMessages.postConfirm()) Then
                If (clsCSAPriceMaster.PostData(MyBase.Form_ID, txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Record posted successfully.", Me.Text)

                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                Errorcontrol.SetError(txtCode, "Select document code for deletion.")
                Throw New Exception("Select document code for deletion.")
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            If Not (myMessages.deleteConfirm()) Then
                Return
            End If

            If clsCSAPriceMaster.DeleteData(txtCode.Value) Then
                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully.", Me.Text)
                FunReset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim obj As New clsCSAPriceMaster
        Try
            isInsideLoadData = True
            gv.FilterDescriptors.Clear()
            gv.Rows.Clear()

            btnsave.Text = "Save"
            btnsave.Enabled = True
            btndelete.Enabled = False
            btnPost.Enabled = False

            FunReset()

            obj = clsCSAPriceMaster.GetData(strCode, NavType)
            isNewentry = True

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.docno) > 0 Then
                isNewentry = False
                txtCode.Value = obj.docno
                txtDesc.Text = obj.Description
                cmbCSAType.SelectedValue = obj.csatype
                fndUOM.Value = obj.csauom
                txtuomName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_desc from tspl_unit_master where unit_code='" + obj.csauom + "'"))
                txtrate.Text = obj.rate
                cmbTax.SelectedValue = obj.taxtype
                chkSelect.Checked = True

                If obj.Doc_Date IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Date) > 0 AndAlso IsDate(obj.Doc_Date) Then
                    dtpDocDate.Text = obj.Doc_Date
                Else
                    dtpDocDate.Text = clsCommon.GETSERVERDATE()
                End If

                If obj.Expiry_Date IsNot Nothing AndAlso clsCommon.myLen(obj.Expiry_Date) > 0 AndAlso IsDate(obj.Expiry_Date) Then
                    dtpExpirydate.Text = obj.Expiry_Date
                Else
                    dtpExpirydate.Text = clsCommon.GETSERVERDATE()
                End If
                txtRevisionNo.Text = obj.Revision_No
                ChkOtherItem.Checked = obj.ForOtherItem
                ChkOtherItem.Enabled = False

                LoadLocation()

                Dim allitemcode As String = ""

                gv.Rows.Clear()

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsCSAPriceDetail In obj.Arr
                        gv.Rows.AddNew()

                        gv.Rows(gv.Rows.Count - 1).Cells(colSelect).Value = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colLineno).Value = clsCommon.myCstr(objtr.lineno)
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = objtr.itemcode
                        allitemcode = allitemcode + "','" + objtr.itemcode
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemname).Value = objtr.itemname
                        gv.Rows(gv.Rows.Count - 1).Cells(colUOM).Value = objtr.uom
                        gv.Rows(gv.Rows.Count - 1).Cells(colDiffRate).Value = objtr.diffrate
                        gv.Rows(gv.Rows.Count - 1).Cells(colLtrPerCse).Value = objtr.ltr_per_case
                        gv.Rows(gv.Rows.Count - 1).Cells(colPcsPerCase).Value = objtr.pcs_per_case
                        gv.Rows(gv.Rows.Count - 1).Cells(colOrgrate).Value = objtr.org_rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colPcsRate).Value = objtr.pcs_rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colCaseRate).Value = objtr.case_rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colCaseUOM).Value = objtr.Case_UOM
                        gv.Rows(gv.Rows.Count - 1).Cells(colMRP).Value = objtr.MRP
                        gv.Rows(gv.Rows.Count - 1).Cells(colIsMRP).Value = objtr.IS_MRP

                        gv.Rows(gv.Rows.Count - 1).Tag = Nothing
                        gv.Rows(gv.Rows.Count - 1).Cells(colDoubleClick).Value = "Double Click"

                        If ChkOtherItem.Checked Then
                            gv.Rows(gv.Rows.Count - 1).Tag = objtr.Arr_OtherItem
                        End If
                    Next

                    gv.CurrentRow = gv.Rows(0)
                    gv.CurrentColumn = gv.Columns(colItemCode)
                End If

                Dim location As New ArrayList()

                If obj.Arr_Loc IsNot Nothing AndAlso obj.Arr_Loc.Count > 0 Then
                    For Each objtr As clsCSALocationDetail In obj.Arr_Loc
                        location.Add(clsCommon.myCstr(objtr.loc_code))
                    Next
                End If
                rbtnlocslct.IsChecked = True

                cbgLocation.CheckedValue = location

                '===State=========
                location = New ArrayList()
                If obj.Arr_State IsNot Nothing AndAlso obj.Arr_State.Count > 0 Then
                    For Each objtr As clsCSAPriceStateDetail In obj.Arr_State
                        location.Add(clsCommon.myCstr(objtr.State_Code))
                    Next
                End If
                chkStateSelect.IsChecked = True
                cmbState.CheckedValue = location
                '----------------------

                '===Customer=========
                location = New ArrayList()
                If obj.Arr_CSA IsNot Nothing AndAlso obj.Arr_CSA.Count > 0 Then
                    For Each objtr As clsCSAPriceCustomerDetail In obj.Arr_CSA
                        location.Add(clsCommon.myCstr(objtr.Cust_Code))
                    Next
                End If
                chkCustSelect.IsChecked = True
                cbgCustomer.CheckedValue = location

                RadGroupBox3.Enabled = AllowOtherItems AndAlso ChkOtherItem.Checked
                RadGroupBox5.Enabled = True
                RadGroupBox2.Enabled = Not ChkOtherItem.Checked
                cmbCSAType.Enabled = Not ChkOtherItem.Checked
                txtrate.Enabled = Not ChkOtherItem.Checked
                cmbCSAType.MendatroryField = Not ChkOtherItem.Checked
                txtrate.MendatroryField = Not ChkOtherItem.Checked
                MyLabel1.Visible = Not ChkOtherItem.Checked
                cmbCSAType.Visible = Not ChkOtherItem.Checked
                MyLabel2.Visible = Not ChkOtherItem.Checked
                txtrate.Visible = Not ChkOtherItem.Checked
                '----------------------

                FillRESTItem(allitemcode)

                cmbCSAType.Enabled = False
                cmbTax.Enabled = False
                fndUOM.Enabled = False

                UcAttachment1.LoadData(txtCode.Value)
                txtCode.MyReadOnly = True
                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                If CSAPricePostedData Then
                    btnPost.Enabled = True
                Else
                    btnPost.Enabled = False
                End If


                If CSAPricePostedData AndAlso (clsCommon.myCdbl(obj.Posted)) = 1 Then
                    btnPost.Enabled = False
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                End If

                txtTotalRow.Text = "Total Rows: " + clsCommon.myCstr(gv.MasterView.Rows.Count())
            Else
                FunReset()
            End If

        Catch ex As Exception
            isNewentry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub FillRESTItem(ByVal ItemCode As String)
        Try
            gv.FilterDescriptors.Clear()
            If clsCommon.myLen(ItemCode) > 0 Then
                If ItemCode.Substring(0, 3) = "','" Then
                    ItemCode = ItemCode.Substring(3, ItemCode.Length - 3)
                End If

                Dim qry As String = "select item_code,item_desc,unit_code,Is_MRP from tspl_item_master where (item_code not in ('" + ItemCode + "')) and Item_Type in ('F','T') and isnull(Is_FreshItem,0)<>1"
                If ChkOtherItem.Checked Then
                    qry += " and csa_type <> '" + cmbCSAType.SelectedValue + "' "
                Else
                    qry += " and csa_type='" + cmbCSAType.SelectedValue + "' "
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        gv.Rows.AddNew()

                        gv.Rows(gv.Rows.Count - 1).Cells(colSelect).Value = False
                        gv.Rows(gv.Rows.Count - 1).Cells(colLineno).Value = clsCommon.myCstr(gv.Rows.Count)
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("item_code"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemname).Value = clsCommon.myCstr(dr("item_desc"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colIsMRP).Value = clsCommon.myCdbl(dr("Is_MRP"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(fndUOM.Value)

                        Dim uom_ltr As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(dr("item_code")) + "' and UOM_Code='" + fndUOM.Value + "'"))
                        Dim pcs_uom As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(dr("item_code")) + "' and stocking_unit='Y'"))
                        Dim case_uom As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(dr("item_code")) + "' and UOM_Code = '" + clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colCaseUOM).Value) + "'"))

                        If uom_ltr <= 0 Then
                            uom_ltr = 1
                        End If
                        If pcs_uom <= 0 Then
                            pcs_uom = 1
                        End If
                        If case_uom <= 0 Then
                            gv.Rows(gv.Rows.Count - 1).Cells(colLtrPerCse).Value = 0 ' System.Math.Round(case_uom / uom_ltr, 2)
                            gv.Rows(gv.Rows.Count - 1).Cells(colPcsPerCase).Value = 0 ' System.Math.Round(case_uom / pcs_uom, 2)
                        Else
                            gv.Rows(gv.Rows.Count - 1).Cells(colLtrPerCse).Value = System.Math.Round(case_uom / uom_ltr, 2)
                            gv.Rows(gv.Rows.Count - 1).Cells(colPcsPerCase).Value = System.Math.Round(case_uom / pcs_uom, 2)
                        End If

                        If AllowOtherItems AndAlso ChkOtherItem.Checked Then
                            OpenOtherItemUOM_Cal(gv.Rows.Count - 1, False)
                            gv.Rows(gv.Rows.Count - 1).Cells(colDoubleClick).Value = "Double Click"
                        End If ''end cond.

                    Next
                    gv.CurrentRow = gv.Rows(0)
                    gv.CurrentColumn = gv.Columns(colItemCode)

                    txtTotalRow.Text = "Total Rows: " + clsCommon.myCstr(gv.MasterView.Rows.Count())
                End If
            End If

            RefreshLineNo()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RefreshLineNo()
        For Each grow As GridViewRowInfo In gv.Rows
            If clsCommon.myLen(grow.Cells(colItemCode).Value) > 0 Then
                grow.Cells(colLineno).Value = grow.Index + 1
            End If
        Next
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(clsCommon.myCstr(txtCode.Value), NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Try
            Dim qry As String = "select count(*) from TSPL_CSA_PRICE_HEAD where doc_no='" + txtCode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                txtCode.MyReadOnly = True
            Else
                txtCode.MyReadOnly = False
            End If

            If txtCode.MyReadOnly OrElse isButtonClicked Then
                txtCode.Value = clsCommon.myCstr(clsCSAPriceMaster.GetFinder("", txtCode.Value, isButtonClicked))
                If clsCommon.myLen(txtCode.Value) > 0 Then
                    LoadData(txtCode.Value, NavigatorType.Current)
                Else
                    FunReset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndUOM__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndUOM._MYValidating
        Dim qry As String = "select distinct TSPL_ITEM_UOM_DETAIL.UOM_Code as Code,TSPL_UNIT_MASTER.Unit_Desc as Description from TSPL_ITEM_UOM_DETAIL left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code "
        fndUOM.Value = clsCommon.ShowSelectForm("UOMFND", qry, "Code", " TSPL_ITEM_UOM_DETAIL.uom_code in (select unit_code from tspl_unit_master where isnull(rt_rate,'N')='Y')", fndUOM.Value, "Code", isButtonClicked)

        If clsCommon.myLen(fndUOM.Value) > 0 Then
            txtuomName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_desc from tspl_unit_master where unit_code='" + fndUOM.Value + "'"))
        Else
            fndUOM.Value = ""
            txtuomName.Text = ""
        End If
    End Sub

    Private Sub FrmCSAPriceMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
            GC.Collect()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F11 Then
            btnAmendment.Visible = False
            If MyBase.isPostFlag AndAlso CSAPricePostedData AndAlso btnPost.Enabled = False AndAlso clsCommon.CompairString(btnsave.Text, "Update") = CompairStringResult.Equal Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "POAmendmentType"
                frm.strCode = "POAmendment"
                frm.ShowDialog()

                If frm.isPasswordCorrect Then
                    btnAmendment.Visible = True
                End If
            End If
        End If

        If (e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colCaseUOM)) Then
            isCellValueChanged = True
            OpenUOM(True)
            Dim uom_ltr As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value) + "' and UOM_Code='" + fndUOM.Value + "'"))
            Dim pcs_uom As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value) + "' and stocking_unit='Y'"))
            Dim case_uom As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value) + "' and UOM_Code = '" + clsCommon.myCstr(gv.CurrentRow.Cells(colCaseUOM).Value) + "'"))

            If uom_ltr <= 0 Then
                uom_ltr = 1
            End If
            If pcs_uom <= 0 Then
                pcs_uom = 1
            End If
            If case_uom <= 0 Then
                case_uom = 1
            End If
            gv.CurrentRow.Cells(colPcsPerCase).Value = System.Math.Round(case_uom / pcs_uom, 2)
            gv.CurrentRow.Cells(colLtrPerCse).Value = System.Math.Round(case_uom / uom_ltr, 2)
            CalDiffrate(gv.CurrentRow.Index, True)
            isCellValueChanged = False
        End If
    End Sub

    Private Sub FrmCSAPriceMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        AllowOtherItems = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowOtherItemOnCSAPriceMaster, clsFixedParameterCode.AllowOtherItemOnCSAPriceMaster, Nothing)) = 1, True, False)
        ForUDLOnly = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ForUDLOnly, clsFixedParameterCode.ForUDLOnly, Nothing)) = 1, True, False)
        ExciseEnableOnSalePatti = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableExciseONCSASalePatti, clsFixedParameterCode.EnableExciseONCSASalePatti, Nothing)) = 1, True, False)


        btnimport_location.Text = "Import Location/Customer State"
        btnexport_location.Text = "Export Location/Customer State"
        LoadBlankGrid()
        LoadComboBox()
        LoadTax()
        LoadLocation()
        LoadState()
        LoadCustomer()



        ChkOtherItem.Visible = AllowOtherItems
        RadGroupBox3.Visible = AllowOtherItems
        MyLabel5.Visible = AllowOtherItems
        txtRevisionNo.Visible = AllowOtherItems
        MyLabel7.Visible = AllowOtherItems
        dtpExpirydate.Visible = AllowOtherItems

        ''===============================
        RadLabel5.Visible = Not ForUDLOnly
        fndUOM.Visible = Not ForUDLOnly
        txtuomName.Visible = Not ForUDLOnly
        MyLabel3.Visible = Not ForUDLOnly
        cmbTax.Visible = Not ForUDLOnly
        chkSelect.Visible = Not ForUDLOnly
        '====================================================
        'gv.Columns(colIsMRP).IsVisible = False
        'If ForUDLOnly = True Then
        '    gv.Columns(colMRP).IsVisible = True
        'Else
        '    gv.Columns(colMRP).IsVisible = False
        'End If

        FunReset()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window.")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for post record.")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N for reset window.")
    End Sub

    Private Sub LoadState()
        Dim qry As String = "select state_code as Code,state_name as State from tspl_state_master"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cmbState.DataSource = Nothing

        cmbState.DataSource = dt
        cmbState.DisplayMember = "Name"
        cmbState.ValueMember = "Code"
    End Sub

    Private Sub LoadTax()
        Dim qry As String = "select '' as Code,'None' as Name union all select 'Yes' as Code,'Yes' as Name union all select 'No' as Code,'No' as Name"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cmbTax.DataSource = Nothing

        cmbTax.DataSource = dt
        cmbTax.DisplayMember = "Name"
        cmbTax.ValueMember = "Code"
    End Sub

    Private Sub LoadComboBox()
        Dim qry As String = "select 'CPD-DESI GHEE' as Code,'CPD-DESI GHEE' as Name " 'select 'None' as Code,'None' as Name union all   union all select 'BULK-DESI GHEE' as Code,'BULK-DESI GHEE' as Name union all select 'CPD-OTHER' as Code,'CPD-OTHER' as Name union all select 'BULK-OTHER' as Code,'BULK-OTHER' as Name
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cmbCSAType.DataSource = Nothing

        isInsideLoadData = True
        cmbCSAType.DataSource = dt
        cmbCSAType.DisplayMember = "Name"
        cmbCSAType.ValueMember = "Code"
        isInsideLoadData = False
    End Sub

    Private Sub LoadBlankGrid()
        gv.Columns.Clear()

        Dim repocheckbox As New GridViewCheckBoxColumn()
        repocheckbox = New GridViewCheckBoxColumn()
        repocheckbox.FormatString = ""
        repocheckbox.Name = colSelect
        repocheckbox.HeaderText = "  "
        repocheckbox.Width = 10
        repocheckbox.ThreeState = False
        repocheckbox.WrapText = True
        gv.MasterTemplate.Columns.Add(repocheckbox)

        Dim repolineno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno.Name = colLineno
        repolineno.FormatString = ""
        repolineno.Width = 80
        'repolineno.DecimalPlaces = 0
        repolineno.HeaderText = "S.No."
        repolineno.ReadOnly = True
        repolineno.WrapText = True
        gv.MasterTemplate.Columns.Add(repolineno)

        Dim repoitemcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoitemcode.Name = colItemCode
        repoitemcode.FormatString = ""
        repoitemcode.Width = 110
        repoitemcode.HeaderText = "Item Code"
        repoitemcode.ReadOnly = True
        repoitemcode.WrapText = True
        gv.MasterTemplate.Columns.Add(repoitemcode)

        Dim repoitemname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoitemname.Name = colitemname
        repoitemname.FormatString = ""
        repoitemname.Width = 300
        repoitemname.HeaderText = "Description"
        repoitemname.ReadOnly = True
        repoitemname.WrapText = True
        gv.MasterTemplate.Columns.Add(repoitemname)

        Dim repouom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouom.Name = colUOM
        repouom.FormatString = ""
        repouom.Width = 80
        repouom.HeaderText = "UOM"
        repouom.IsVisible = False
        repouom.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repouom.TextImageRelation = TextImageRelation.TextBeforeImage
        repouom.WrapText = True
        gv.MasterTemplate.Columns.Add(repouom)

        Dim repodiff As GridViewDecimalColumn = New GridViewDecimalColumn()
        repodiff.Name = colDiffRate
        repodiff.FormatString = ""
        repodiff.Width = 80
        repodiff.DecimalPlaces = 2
        repodiff.HeaderText = "Diff. Rate"
        repodiff.WrapText = True
        gv.MasterTemplate.Columns.Add(repodiff)

        Dim repoltrpercase As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoltrpercase.Name = colLtrPerCse
        repoltrpercase.FormatString = ""
        repoltrpercase.Width = 80
        repoltrpercase.HeaderText = "Ltr. Per Case"
        repoltrpercase.DecimalPlaces = 2
        repoltrpercase.ReadOnly = True
        repoltrpercase.WrapText = True
        gv.MasterTemplate.Columns.Add(repoltrpercase)

        Dim repouomcase As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouomcase.Name = colCaseUOM
        repouomcase.FormatString = ""
        repouomcase.Width = 80
        repouomcase.HeaderText = "Case UOM"
        repouomcase.IsVisible = True
        repouomcase.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repouomcase.TextImageRelation = TextImageRelation.TextBeforeImage
        repouomcase.WrapText = True
        gv.MasterTemplate.Columns.Add(repouomcase)

        Dim repopcspercase As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopcspercase.Name = colPcsPerCase
        repopcspercase.FormatString = ""
        repopcspercase.Width = 80
        repopcspercase.HeaderText = "Pcs Per Case"
        repopcspercase.DecimalPlaces = 2
        repopcspercase.ReadOnly = True
        repopcspercase.WrapText = True
        gv.MasterTemplate.Columns.Add(repopcspercase)

        Dim repopcsrate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopcsrate.Name = colPcsRate
        repopcsrate.FormatString = ""
        repopcsrate.Width = 80
        repopcsrate.HeaderText = "Rate/Pcs"
        repopcsrate.ReadOnly = True
        repopcsrate.WrapText = True
        gv.MasterTemplate.Columns.Add(repopcsrate)

        Dim repoltrrate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoltrrate.Name = colOrgrate
        repoltrrate.FormatString = ""
        repoltrrate.Width = 80
        repoltrrate.HeaderText = "Rate/Ltr."
        repoltrrate.ReadOnly = True
        repoltrrate.WrapText = True
        gv.MasterTemplate.Columns.Add(repoltrrate)

        Dim repocaserate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repocaserate.Name = colCaseRate
        repocaserate.FormatString = ""
        repocaserate.Width = 80
        repocaserate.HeaderText = "Rate/Case"
        repocaserate.ReadOnly = True
        repocaserate.WrapText = True
        gv.MasterTemplate.Columns.Add(repocaserate)


        Dim repomrp As GridViewDecimalColumn = New GridViewDecimalColumn()
        repomrp.Name = colMRP
        repomrp.FormatString = ""
        repomrp.Width = 80
        repomrp.HeaderText = "MRP"
        repomrp.ReadOnly = False
        repomrp.WrapText = True
        repomrp.VisibleInColumnChooser = ExciseEnableOnSalePatti
        repomrp.IsVisible = ExciseEnableOnSalePatti
        gv.MasterTemplate.Columns.Add(repomrp)


        Dim repoismrp As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoismrp.Name = colIsMRP
        repoismrp.FormatString = ""
        repoismrp.Width = 80
        repoismrp.HeaderText = "IsMRP"
        repoismrp.ReadOnly = True
        repoismrp.WrapText = True
        repoismrp.IsVisible = False
        repoismrp.VisibleInColumnChooser = False
        gv.MasterTemplate.Columns.Add(repoismrp)


        repouomcase = New GridViewTextBoxColumn()
        repouomcase.Name = colDoubleClick
        repouomcase.FormatString = ""
        repouomcase.Width = 100
        repouomcase.HeaderText = "Detail"
        repouomcase.IsVisible = True
        repouomcase.ReadOnly = True
        repouomcase.WrapText = True
        gv.MasterTemplate.Columns.Add(repouomcase)


        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False

        HideColumnForOtherItems()

        ReStoreGridLayout1()
    End Sub

    Private Sub HideColumnForOtherItems()
        gv.Columns(colDiffRate).HeaderText = IIf(ChkOtherItem.Checked, "Unit Rate", "Diff. Rate")
        gv.Columns(colCaseUOM).HeaderText = IIf(ChkOtherItem.Checked, "UOM", "Case UOM")
        gv.Columns(colLtrPerCse).IsVisible = Not ChkOtherItem.Checked
        gv.Columns(colLtrPerCse).VisibleInColumnChooser = Not ChkOtherItem.Checked
        gv.Columns(colPcsPerCase).IsVisible = Not ChkOtherItem.Checked
        gv.Columns(colPcsPerCase).VisibleInColumnChooser = Not ChkOtherItem.Checked
        gv.Columns(colPcsRate).IsVisible = Not ChkOtherItem.Checked
        gv.Columns(colPcsRate).VisibleInColumnChooser = Not ChkOtherItem.Checked
        gv.Columns(colOrgrate).IsVisible = Not ChkOtherItem.Checked
        gv.Columns(colOrgrate).VisibleInColumnChooser = Not ChkOtherItem.Checked
        gv.Columns(colCaseRate).IsVisible = Not ChkOtherItem.Checked
        gv.Columns(colCaseRate).VisibleInColumnChooser = Not ChkOtherItem.Checked

        gv.Columns(colDoubleClick).IsVisible = ChkOtherItem.Checked
        gv.Columns(colDoubleClick).VisibleInColumnChooser = ChkOtherItem.Checked
    End Sub

    Private Sub FillGrid()
        Try
            gv.FilterDescriptors.Clear()
            If Not ChkOtherItem.Checked AndAlso clsCommon.myLen(fndUOM.Value) <= 0 Then
                fndUOM.Focus()
                fndUOM.Select()
                cmbCSAType.SelectedText = "None"
                Errorcontrol.SetError(txtuomName, "Set RT unit in unit of measure first.")
                Throw New Exception("Set RT unit in unit of measure First.")
            Else
                Errorcontrol.ResetError(txtuomName)
            End If

            Dim qry As String = "select row_number() over(order by item_code) as sno,item_code,item_desc,unit_code,is_Mrp from tspl_item_master where Item_Type in ('F','T') and isnull(Is_FreshItem,0)<>1 "
            If ChkOtherItem.Checked Then
                qry += " and csa_type <> '" + cmbCSAType.SelectedValue + "' "
            Else
                qry += " and csa_type = '" + cmbCSAType.SelectedValue + "' "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Nothing)

            gv.Rows.Clear()
            isInsideLoadData = True
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv.Rows.AddNew()

                    gv.Rows(gv.Rows.Count - 1).Cells(colSelect).Value = False
                    gv.Rows(gv.Rows.Count - 1).Cells(colLineno).Value = clsCommon.myCstr(dr("sno"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("item_code"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colitemname).Value = clsCommon.myCstr(dr("item_desc"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colIsMRP).Value = clsCommon.myCdbl(dr("is_Mrp"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(fndUOM.Value)

                    If AllowOtherItems AndAlso ChkOtherItem.Checked Then
                        gv.Rows(gv.Rows.Count - 1).Cells(colCaseUOM).Value = clsCommon.myCstr(dr("unit_code"))
                    End If
                    Dim uom_ltr As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(dr("item_code")) + "' and UOM_Code='" + fndUOM.Value + "'", Nothing))
                    Dim pcs_uom As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(dr("item_code")) + "' and stocking_unit='Y'", Nothing))
                    Dim case_uom As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(dr("item_code")) + "' and UOM_Code = '" + clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colCaseUOM).Value) + "'", Nothing))

                    If uom_ltr <= 0 Then
                        uom_ltr = 1
                    End If
                    If pcs_uom <= 0 Then
                        pcs_uom = 1
                    End If
                    If case_uom <= 0 Then
                        gv.Rows(gv.Rows.Count - 1).Cells(colLtrPerCse).Value = 0 'System.Math.Round(case_uom / uom_ltr, 2)
                        gv.Rows(gv.Rows.Count - 1).Cells(colPcsPerCase).Value = 0 'System.Math.Round(case_uom / pcs_uom, 2)
                    Else
                        gv.Rows(gv.Rows.Count - 1).Cells(colLtrPerCse).Value = System.Math.Round(case_uom / uom_ltr, 2)
                        gv.Rows(gv.Rows.Count - 1).Cells(colPcsPerCase).Value = System.Math.Round(case_uom / pcs_uom, 2)
                    End If

                    gv.Rows(gv.Rows.Count - 1).Tag = Nothing
                    gv.Rows(gv.Rows.Count - 1).Cells(colDoubleClick).Value = "Double Click"
                Next

                gv.CurrentRow = gv.Rows(0)
                gv.CurrentColumn = gv.Columns(colItemCode)

                txtTotalRow.Text = "Total Rows: " + clsCommon.myCstr(gv.MasterView.Rows.Count())
            End If

            HideColumnForOtherItems()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        isInsideLoadData = False
    End Sub

    Private Sub cmbCSAType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCSAType.SelectedValueChanged
        Try
            If isInsideLoadData = True Then
                Return
            End If
            If clsCommon.CompairString(cmbCSAType.SelectedValue, "") = CompairStringResult.Equal Then
                gv.Rows.Clear()
            ElseIf clsCommon.CompairString(cmbCSAType.SelectedValue, "None") <> CompairStringResult.Equal Then
                FillGrid()
            End If
        Catch ex As Exception
            cmbCSAType.SelectedValue = "None"
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub OpenOtherItemUOM_Cal(ByVal IntRow As Integer, ByVal OpenDialogBox As Boolean)
        Dim qry As String = ""
        Dim dt As New DataTable()
        Dim obj As New clsCSAPriceDetail()
        Dim arr As New List(Of clsCSAPriceDetail)
        Try
            Dim UnitRate As Decimal = clsCommon.myCdbl(gv.Rows(IntRow).Cells(colDiffRate).Value)
            Dim MRPRate As Decimal = clsCommon.myCdbl(gv.Rows(IntRow).Cells(colMRP).Value)
            Dim whrcls As String = ""

            qry = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as [Code],tspl_unit_master.unit_desc as [Unit],round(cast((" + clsCommon.myCstr(UnitRate) + " * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) / (case when isnull(BaseUOm.Conversion_Factor,0)=0 then 1 else BaseUOm.Conversion_Factor end) as float),2) as [Unit Rate],round(cast((" + clsCommon.myCstr(MRPRate) + " * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) / (case when isnull(BaseUOm.Conversion_Factor,0)=0 then 1 else BaseUOm.Conversion_Factor end) as float),2) as [MRP] " & _
                      " from TSPL_ITEM_UOM_DETAIL left outer join tspl_unit_master on tspl_unit_master.unit_code=TSPL_ITEM_UOM_DETAIL.uom_code left outer join TSPL_ITEM_UOM_DETAIL as BaseUOm on BaseUOm.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and BaseUOm.UOM_Code='" + clsCommon.myCstr(gv.Rows(IntRow).Cells(colCaseUOM).Value) + "' "

            whrcls = " TSPL_ITEM_UOM_DETAIL.Item_Code='" + clsCommon.myCstr(gv.Rows(IntRow).Cells(colItemCode).Value) + "' "
            dt = New DataTable()

            If OpenDialogBox Then
                Dim strcode As String = ""
                strcode = clsCommon.myCstr(clsCommon.ShowSelectForm("OTHRITMPRC", qry, "Code", whrcls, strcode, "", True)) ''for show on double click
            End If

            ''===for fill in grid
            qry += " where " + whrcls
            dt = clsDBFuncationality.GetDataTable(qry)

            gv.Rows(IntRow).Tag = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    obj = New clsCSAPriceDetail()

                    obj.itemcode = clsCommon.myCstr(gv.Rows(IntRow).Cells(colItemCode).Value)
                    obj.uom = clsCommon.myCstr(dr("code"))
                    obj.diffrate = System.Math.Round(clsCommon.myCdbl(dr("unit rate")), 2)
                    obj.MRP = System.Math.Round(clsCommon.myCdbl(dr("MRP")), 2)
                    arr.Add(obj)
                Next

                gv.Rows(IntRow).Tag = arr

            End If
        Catch ex As Exception
            isCellValueChanged = False

            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            dt = Nothing
        End Try
    End Sub

    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try
            If AllowOtherItems AndAlso ChkOtherItem.Checked Then
                If e.Column Is gv.Columns(colDoubleClick) Then
                    OpenOtherItemUOM_Cal(gv.CurrentRow.Index, True)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    If (e.Column Is gv.Columns(colUOM)) Then
                        isCellValueChanged = True
                        'OpenUOM(False)
                        isCellValueChanged = False
                    End If
                    If (e.Column Is gv.Columns(colCaseUOM)) Then
                        isCellValueChanged = True
                        OpenUOM(False)

                        If AllowOtherItems AndAlso ChkOtherItem.Checked Then
                            OpenOtherItemUOM_Cal(gv.CurrentRow.Index, False)
                        Else
                            Dim uom_ltr As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value) + "' and UOM_Code='" + fndUOM.Value + "'"))
                            Dim pcs_uom As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value) + "' and stocking_unit='Y'"))
                            Dim case_uom As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value) + "' and UOM_Code = '" + clsCommon.myCstr(gv.CurrentRow.Cells(colCaseUOM).Value) + "'"))

                            If uom_ltr <= 0 Then
                                uom_ltr = 1
                            End If
                            If pcs_uom <= 0 Then
                                pcs_uom = 1
                            End If
                            If case_uom <= 0 Then
                                case_uom = 1
                            End If
                            gv.CurrentRow.Cells(colPcsPerCase).Value = System.Math.Round(case_uom / pcs_uom, 2)
                            gv.CurrentRow.Cells(colLtrPerCse).Value = System.Math.Round(case_uom / uom_ltr, 2)
                            CalDiffrate(gv.CurrentRow.Index, True)
                        End If
                        isCellValueChanged = False
                    End If
                    If (e.Column Is gv.Columns(colDiffRate)) OrElse (e.Column Is gv.Columns(colLtrPerCse)) OrElse (e.Column Is gv.Columns(colPcsPerCase)) OrElse (e.Column Is gv.Columns(colUOM)) Then
                        isCellValueChanged = True
                        If AllowOtherItems AndAlso ChkOtherItem.Checked Then
                            OpenOtherItemUOM_Cal(gv.CurrentRow.Index, False)
                        Else
                            CalDiffrate(CInt(gv.CurrentRow.Index), True)
                        End If
                        isCellValueChanged = False
                    End If

                    If (e.Column Is gv.Columns(colMRP)) Then
                        isCellValueChanged = True
                        If AllowOtherItems AndAlso ChkOtherItem.Checked Then
                            OpenOtherItemUOM_Cal(gv.CurrentRow.Index, False)
                        Else
                            CalDiffrate(CInt(gv.CurrentRow.Index), True)
                        End If
                        isCellValueChanged = False
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub OpenUOM(ByVal isButtonClicked As Boolean)
        Try
            Dim qry As String = "select distinct TSPL_ITEM_UOM_DETAIL.UOM_Code as Code,TSPL_UNIT_MASTER.Unit_Desc as Description from TSPL_ITEM_UOM_DETAIL left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code "
            gv.CurrentRow.Cells(colCaseUOM).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("UOM1FND", qry, "Code", " TSPL_ITEM_UOM_DETAIL.Item_Code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value) + "' ", clsCommon.myCstr(gv.CurrentRow.Cells(colCaseUOM).Value), "Code", isButtonClicked))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub CalDiffrate(ByVal XR As Integer, ByVal CellChanged As Boolean)
        Try
            Dim diffrate As Decimal = 0
            Dim orgrate As Decimal = 0
            Dim ltr_per_case As Decimal = 0
            Dim case_rate As Decimal = 0
            Dim pcs_per_case As Decimal = 0
            Dim uom As String = ""
            Dim stockingunit As String = ""
            Dim cnvrsn As Decimal = 1
            Dim case_cnvrsn As Decimal = 0

            If CellChanged Then
                uom = clsCommon.myCstr(gv.Rows(XR).Cells(colUOM).Value)
                Dim qry As String = "select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv.Rows(XR).Cells(colItemCode).Value) + "' and uom_code='" + uom + "'"
                cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                If cnvrsn <= 0 Then
                    cnvrsn = 1
                End If

                stockingunit = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select uom_code from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv.Rows(XR).Cells(colItemCode).Value) + "' and stocking_unit='Y'"))
                case_cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv.Rows(XR).Cells(colItemCode).Value) + "' and uom_code='" + clsCommon.myCstr(gv.Rows(XR).Cells(colCaseUOM).Value) + "'"))

                diffrate = clsCommon.myCdbl(gv.Rows(XR).Cells(colDiffRate).Value)
                gv.Rows(XR).Cells(colOrgrate).Value = (clsCommon.myCdbl(txtrate.Text) + clsCommon.myCdbl(diffrate))

                '========case rate====            
                orgrate = clsCommon.myCdbl(gv.Rows(XR).Cells(colOrgrate).Value)
                ltr_per_case = clsCommon.myCdbl(gv.Rows(XR).Cells(colLtrPerCse).Value)
                gv.Rows(XR).Cells(colCaseRate).Value = System.Math.Round((clsCommon.myCdbl(orgrate) * case_cnvrsn) / cnvrsn, 2)

                '====pcs rate=================           
                case_rate = clsCommon.myCdbl(gv.Rows(XR).Cells(colCaseRate).Value)
                pcs_per_case = clsCommon.myCdbl(gv.Rows(XR).Cells(colPcsPerCase).Value)
                If clsCommon.myCdbl(case_cnvrsn) > 0 Then
                    gv.Rows(XR).Cells(colPcsRate).Value = System.Math.Round((clsCommon.myCdbl(case_rate) / clsCommon.myCdbl(case_cnvrsn)), 2)
                Else
                    gv.Rows(XR).Cells(colPcsRate).Value = 0
                End If
            Else
                For Each grow As GridViewRowInfo In gv.Rows
                    uom = clsCommon.myCstr(grow.Cells(colUOM).Value)
                    Dim qry As String = "select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "' and uom_code='" + uom + "'"
                    cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                    If cnvrsn <= 0 Then
                        cnvrsn = 1
                    End If

                    stockingunit = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select uom_code from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "' and stocking_unit='Y'"))
                    case_cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "' and uom_code='" + clsCommon.myCstr(grow.Cells(colCaseUOM).Value) + "'"))

                    diffrate = clsCommon.myCdbl(grow.Cells(colDiffRate).Value)
                    grow.Cells(colOrgrate).Value = (clsCommon.myCdbl(txtrate.Text) + clsCommon.myCdbl(diffrate))

                    '========case rate====            
                    orgrate = clsCommon.myCdbl(grow.Cells(colOrgrate).Value)
                    ltr_per_case = clsCommon.myCdbl(grow.Cells(colLtrPerCse).Value)
                    grow.Cells(colCaseRate).Value = System.Math.Round((clsCommon.myCdbl(orgrate) * case_cnvrsn) / cnvrsn, 2)

                    '====pcs rate=================           
                    case_rate = clsCommon.myCdbl(grow.Cells(colCaseRate).Value)
                    pcs_per_case = clsCommon.myCdbl(grow.Cells(colPcsPerCase).Value)
                    If clsCommon.myCdbl(case_cnvrsn) > 0 Then
                        grow.Cells(colPcsRate).Value = System.Math.Round((clsCommon.myCdbl(case_rate) / clsCommon.myCdbl(case_cnvrsn)), 2)
                    Else
                        grow.Cells(colPcsRate).Value = 0
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtrate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtrate.TextChanged
        Try
            If gv.Rows.Count > 0 Then
                CalDiffrate(0, False)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsavelayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsavelayout.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub ReStoreGridLayout1()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btndeletelayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeletelayout.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Delete layout successfully", "Information", Me.Text)
    End Sub

    Private Sub rbtnlocall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnlocall.ToggleStateChanged
        cbgLocation.Enabled = rbtnlocslct.IsChecked
        If rbtnlocall.IsChecked Then
            cbgLocation.CheckedAll()
        Else
            cbgLocation.UnCheckedAll()
        End If
    End Sub

    Private Sub btnexport_detail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport_detail.Click
        Try
            Dim qry As String = "select count(*) from TSPL_CSA_PRICE_HEAD where TSPL_CSA_PRICE_HEAD.For_Other_Item<>'1'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            Dim whrcls As String = Nothing
            If check > 0 Then
                qry = "select TSPL_CSA_PRICE_HEAD.Doc_No,TSPL_CSA_PRICE_HEAD.Description,TSPL_CSA_PRICE_HEAD.CSA_Type,TSPL_CSA_PRICE_HEAD.CSA_UOM,TSPL_CSA_PRICE_HEAD.CSA_Rate,tspl_csa_price_head.TAX,TSPL_CSA_PRICE_DETAIL.Line_No,TSPL_CSA_PRICE_DETAIL.Item_Code,tspl_item_master.Item_Desc,TSPL_CSA_PRICE_DETAIL.Diff_Rate,TSPL_CSA_PRICE_DETAIL.Case_UOM as Item_Case_UOM "
                qry += "from TSPL_CSA_PRICE_HEAD left outer join TSPL_CSA_PRICE_DETAIL on TSPL_CSA_PRICE_HEAD.doc_no=TSPL_CSA_PRICE_DETAIL.doc_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_CSA_PRICE_DETAIL.item_code "
                whrcls = " and tspl_csa_price_head.For_Other_Item<>'1'"
                ',TSPL_CSA_PRICE_DETAIL.Ltr_Per_Case,TSPL_CSA_PRICE_DETAIL.Pcs_Per_Case,TSPL_CSA_PRICE_DETAIL.Pcs_Rate,TSPL_CSA_PRICE_DETAIL.Ltr_Rate,TSPL_CSA_PRICE_DETAIL.Case_Rate
            Else
                qry = "select '' as Doc_No,'' as Description,'' as CSA_Type,'' as CSA_UOM,0 as CSA_Rate,'No' as TAX,0 as Line_No,''as Item_Code,'' as Item_Desc,0 as Diff_Rate,'' as Item_Case_UOM " ',0 as Ltr_Per_Case,0 as Pcs_Per_Case,0 as Pcs_Rate,0 as Ltr_Rate,0 as Case_Rate
            End If

            transportSql.ExporttoExcel(qry, whrcls, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnexport_location_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport_location.Click
        Try
            Dim qry As String = "select count(*) from TSPL_CSA_PRICE_HEAD"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                qry = "select TSPL_CSA_LOCATION_DETAIL.Doc_No,TSPL_CSA_LOCATION_DETAIL.Location_Code,TSPL_CSA_PRICE_STATE_DETAIL.State_Code from TSPL_CSA_LOCATION_DETAIL "
                qry += "left outer join TSPL_CSA_PRICE_STATE_DETAIL on TSPL_CSA_PRICE_STATE_DETAIL.doc_no=TSPL_CSA_LOCATION_DETAIL.doc_no"
            Else
                qry = "select '' as Doc_No,'' as Location_Code,'' as State_Code"
            End If

            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnimport_detail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport_detail.Click
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim docno As String = ""
            Dim description As String = ""
            Dim csa_type As String = ""
            Dim csa_uom As String = ""
            Dim item_code As String = ""
            Dim item_name As String = ""
            Dim item_uom As String = ""
            Dim diff_rate As Decimal = 0
            Dim csa_rate As Decimal = 0
            Dim ltr_per_case As Decimal = 0
            Dim pcs_per_case As Decimal = 0
            Dim org_rate As Decimal = 0
            Dim pcs_rate As Decimal = 0
            Dim case_rate As Decimal = 0
            Dim lineno As Decimal = 0
            Dim qry As String = ""
            Dim check As Integer = 0
            Dim ii As Integer = 0
            Dim tax As String = ""
            Dim Item_Case_UOM As String = ""


            If transportSql.importExcel(gv1, "Doc_No", "Description", "CSA_Type", "CSA_UOM", "CSA_Rate", "TAX", "Line_No", "Item_Code", "Item_Desc", "Diff_Rate", "Item_Case_UOM") Then ', "Ltr_Per_Case", "Pcs_Per_Case", "Pcs_Rate", "Ltr_Rate", "Case_Rate"
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv1.Rows

                    docno = clsCommon.myCstr(grow.Cells("Doc_No").Value)
                    If clsCommon.myLen(docno) <= 0 Then
                        Throw New Exception("Fill document code at line no. " + clsCommon.myCstr(ii + 1) + "")
                    End If

                    description = clsCommon.myCstr(grow.Cells("Description").Value)
                    If description IsNot Nothing AndAlso clsCommon.myLen(description) > 200 Then
                        description = description.Substring(0, 200)
                    End If

                    csa_type = clsCommon.myCstr(grow.Cells("CSA_Type").Value)
                    If clsCommon.myLen(csa_type) <= 0 Then
                        Throw New Exception("Fill CSA Type at line no. " + clsCommon.myCstr(ii + 1) + "")
                    End If
                    If clsCommon.CompairString(csa_type, "CPD-DESI GHEE") <> CompairStringResult.Equal Then 'clsCommon.CompairString(csa_type, "CPD-OTHER") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(csa_type, "BULK-DESI GHEE") <> CompairStringResult.Equal AndAlso  AndAlso clsCommon.CompairString(csa_type, "BULK-OTHER") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(csa_type, "None") <> CompairStringResult.Equal
                        Throw New Exception("CSA Type should be CPD-DESI GHEE" + Environment.NewLine + "at line no. " + clsCommon.myCstr(ii + 1) + "")
                    End If

                    csa_uom = clsCommon.myCstr(grow.Cells("CSA_UOM").Value)
                    If clsCommon.myLen(csa_uom) <= 0 Then
                        Throw New Exception("Fill CSA Unit detail at line no. " + clsCommon.myCstr(ii + 1) + "")
                    End If
                    If clsCommon.myLen(csa_uom) > 0 Then
                        qry = "select count(*) from tspl_unit_master where unit_code='" + csa_uom + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled CSA unit detail is not valid,see at line no. " + clsCommon.myCstr(ii + 1) + "")
                        End If
                    End If

                    csa_rate = clsCommon.myCdbl(grow.Cells("CSA_Rate").Value)
                    If csa_rate <= 0 Then
                        Throw New Exception("Fill CSA rate at line no. " + clsCommon.myCstr(ii + 1) + "")
                    End If

                    tax = clsCommon.myCstr(grow.Cells("TAX").Value)
                    If clsCommon.myLen(tax) <= 0 Then
                        Throw New Exception("Fill Tax at Line No. " + clsCommon.myCstr(ii + 1) + "")
                    End If

                    lineno = clsCommon.myCdbl(grow.Cells("Line_No").Value)
                    If lineno <= 0 Then
                        Throw New Exception("Fill line no. at line no. " + clsCommon.myCstr(ii + 1) + "")
                    End If

                    item_code = clsCommon.myCstr(grow.Cells("Item_Code").Value)
                    item_name = clsCommon.myCstr(grow.Cells("Item_Desc").Value)
                    If clsCommon.myLen(item_code) <= 0 Then
                        Throw New Exception("Fill item detail at line no. " + clsCommon.myCstr(ii + 1) + "")
                    End If
                    If clsCommon.myLen(item_code) > 0 Then
                        qry = "select count(*) from tspl_item_master where item_code='" + item_code + "' and csa_type='" + csa_type + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled item detail is not valid,see at line no. " + clsCommon.myCstr(ii + 1) + "")
                        End If
                    End If

                    Item_Case_UOM = clsCommon.myCstr(grow.Cells("Item_Case_UOM").Value)
                    If clsCommon.myLen(Item_Case_UOM) <= 0 Then
                        Throw New Exception("Fill Item_Case_UOM detail at line no. " + clsCommon.myCstr(ii + 1) + "")
                    End If
                    If clsCommon.myLen(Item_Case_UOM) > 0 Then
                        qry = "select count(*) from tspl_item_uom_detail where uom_code='" + Item_Case_UOM + "' and item_code='" + item_code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Item_Case_UOM is not valid,see at line no. " + clsCommon.myCstr(ii + 1) + "")
                        End If
                    End If

                    item_uom = csa_uom   'clsCommon.myCstr(grow.Cells("UOM").Value)
                    If clsCommon.myLen(item_uom) <= 0 Then
                        item_uom = csa_uom
                        'Throw New Exception("Fill Item Unit detail at line no. " + clsCommon.myCstr(ii + 1) + "")
                    End If
                    If clsCommon.myLen(item_uom) > 0 Then
                        'qry = "select count(*) from TSPL_ITEM_UOM_DETAIL where uom_code='" + item_uom + "' and item_code='" + item_code + "'"
                        'check = clsDBFuncationality.getSingleValue(qry, trans)
                        'If check <= 0 Then
                        '    Throw New Exception("Filled Item unit detail is not valid,see at line no. " + clsCommon.myCstr(ii + 1) + "")
                        'End If
                    End If

                    diff_rate = clsCommon.myCdbl(grow.Cells("Diff_Rate").Value)

                    '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                    Dim uom_ltr As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + item_code + "' and UOM_Code='" + csa_uom + "'", trans))
                    Dim pcs_uom As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + item_code + "' and stocking_unit='Y'", trans))
                    Dim case_uom As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + item_code + "' and UOM_Code = '" + Item_Case_UOM + "'", trans))
                    Dim cnvrsn As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + item_code + "' and UOM_Code='" + item_uom + "'", trans))
                    If cnvrsn <= 0 Then
                        cnvrsn = 1
                    End If
                    If uom_ltr <= 0 Then
                        uom_ltr = 1
                    End If
                    If pcs_uom <= 0 Then
                        pcs_uom = 1
                    End If
                    If case_uom <= 0 Then
                        case_uom = 1
                    End If
                    pcs_per_case = System.Math.Round(case_uom / pcs_uom, 2)
                    ltr_per_case = System.Math.Round(case_uom / uom_ltr, 2)

                    org_rate = (clsCommon.myCdbl(csa_rate) + clsCommon.myCdbl(diff_rate))

                    '========case rate====            
                    case_rate = System.Math.Round((clsCommon.myCdbl(org_rate) * case_uom) / cnvrsn, 2)

                    '====pcs rate=================           

                    If clsCommon.myCdbl(case_uom) > 0 Then
                        pcs_rate = System.Math.Round((clsCommon.myCdbl(case_rate) / clsCommon.myCdbl(case_uom)), 2)
                    Else
                        pcs_rate = 0
                    End If
                    '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                    'ltr_per_case = clsCommon.myCdbl(grow.Cells("Ltr_Per_Case").Value)
                    'If ltr_per_case <= 0 Then
                    '    Throw New Exception("Fill ltr. per case at line no. " + clsCommon.myCstr(ii + 1) + "")
                    'End If

                    'pcs_per_case = clsCommon.myCdbl(grow.Cells("Pcs_Per_Case").Value)
                    'If pcs_per_case <= 0 Then
                    '    Throw New Exception("Fill pcs per case at line no. " + clsCommon.myCstr(ii + 1) + "")
                    'End If


                    'pcs_rate = clsCommon.myCdbl(grow.Cells("Pcs_Rate").Value)
                    'If pcs_rate <= 0 Then
                    '    Throw New Exception("Fill pcs rate at line no. " + clsCommon.myCstr(ii + 1) + "")
                    'End If

                    'org_rate = clsCommon.myCdbl(grow.Cells("Ltr_Rate").Value)
                    'If org_rate <= 0 Then
                    '    Throw New Exception("Fill ltr rate at line no. " + clsCommon.myCstr(ii + 1) + "")
                    'End If

                    'case_rate = clsCommon.myCdbl(grow.Cells("Case_Rate").Value)
                    'If case_rate <= 0 Then
                    '    Throw New Exception("Fill case rate at line no. " + clsCommon.myCstr(ii + 1) + "")
                    'End If

                    '=========================================================
                    'qry = "select count(*) from TSPL_CSA_PRICE_HEAD where doc_no <>'" + docno + "' and csa_type='" + csa_type + "' and tax='" + tax + "' and csa_uom='" + csa_uom + "'"
                    'check = clsDBFuncationality.getSingleValue(qry, trans)

                    'If check > 0 Then
                    '    Throw New Exception("Record already exist of CSA at line no. " + clsCommon.myCstr(ii + 1) + "")
                    'End If

                    qry = "delete from TSPL_CSA_PRICE_DETAIL where doc_no='" + docno + "' and item_code='" + item_code + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "select count(*) from TSPL_CSA_PRICE_HEAD where doc_no='" + docno + "' and TSPL_CSA_PRICE_HEAD.For_Other_Item<>'1'"
                    check = clsDBFuncationality.getSingleValue(qry, trans)

                    '=======Head====
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "doc_no", docno)
                    clsCommon.AddColumnsForChange(coll, "Description", description)
                    clsCommon.AddColumnsForChange(coll, "CSA_Type", csa_type)
                    clsCommon.AddColumnsForChange(coll, "CSA_UOM", csa_uom)
                    clsCommon.AddColumnsForChange(coll, "CSA_Rate", csa_rate)
                    clsCommon.AddColumnsForChange(coll, "TAX", tax)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                    If check <= 0 Then
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_HEAD", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_HEAD", OMInsertOrUpdate.Update, " doc_no='" + docno + "'", trans)
                    End If
                    '=======Detail====
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "doc_no", docno)
                    clsCommon.AddColumnsForChange(coll, "Line_no", lineno)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", item_code)
                    clsCommon.AddColumnsForChange(coll, "UOM", item_uom)
                    clsCommon.AddColumnsForChange(coll, "Diff_Rate", diff_rate)
                    clsCommon.AddColumnsForChange(coll, "Ltr_Rate", org_rate)
                    clsCommon.AddColumnsForChange(coll, "Case_Rate", case_rate)
                    clsCommon.AddColumnsForChange(coll, "Pcs_Rate", pcs_rate)
                    clsCommon.AddColumnsForChange(coll, "Ltr_Per_Case", ltr_per_case)
                    clsCommon.AddColumnsForChange(coll, "Pcs_Per_Case", pcs_per_case)
                    clsCommon.AddColumnsForChange(coll, "case_uom", Item_Case_UOM)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                    ii += 1
                Next

                trans.Commit()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
            End If

        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Me.Controls.Remove(gv1)
    End Sub

    Private Sub btnimport_location_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport_location.Click
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim docno As String = ""
            Dim location_code As String = ""
            Dim state_code As String = ""
            Dim qry As String = ""
            Dim check As Integer = 0


            If transportSql.importExcel(gv1, "Doc_No", "Location_Code", "State_Code") Then
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv1.Rows
                    docno = clsCommon.myCstr(grow.Cells("doc_no").Value)
                    If clsCommon.myLen(docno) <= 0 Then
                        Throw New Exception("Fill document code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    Else
                        qry = "select count(*) from tspl_csa_price_head where doc_no='" + docno + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("First import detail data.")
                        End If
                    End If

                    location_code = clsCommon.myCstr(grow.Cells("location_code").Value)
                    If clsCommon.myLen(location_code) <= 0 Then
                        Throw New Exception("Fill location code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    Else
                        qry = "select count(*) from tspl_location_master where location_code='" + location_code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled location not found,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    '======================state=================================
                    state_code = clsCommon.myCstr(grow.Cells("state_code").Value)
                    If clsCommon.myLen(state_code) <= 0 Then
                        Throw New Exception("Fill customer state code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    Else
                        qry = "select count(*) from tspl_state_master where state_code='" + state_code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled customer state not found,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    Dim csatype As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select csa_type from tspl_csa_price_head where doc_no='" + docno + "'", trans))
                    Dim taxtype As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tax from tspl_csa_price_head where doc_no='" + docno + "'", trans))

                    qry = "select count(*) from TSPL_CSA_PRICE_HEAD "
                    qry += "left outer join TSPL_CSA_PRICE_STATE_DETAIL on TSPL_CSA_PRICE_HEAD.Doc_No=TSPL_CSA_PRICE_STATE_DETAIL.Doc_No left outer join TSPL_CSA_LOCATION_DETAIL on TSPL_CSA_LOCATION_DETAIL.Doc_No=TSPL_CSA_PRICE_HEAD.Doc_No "
                    qry += "where TSPL_CSA_PRICE_HEAD.doc_no <>'" + docno + "' and TSPL_CSA_PRICE_HEAD.csa_type='" + csatype + "' and TSPL_CSA_PRICE_HEAD.tax='" + taxtype + "' "
                    qry += "and TSPL_CSA_LOCATION_DETAIL.Location_Code ='" + location_code + "' and TSPL_CSA_PRICE_STATE_DETAIL.State_Code ='" + state_code + "'"
                    check = clsDBFuncationality.getSingleValue(qry, trans)
                    If check > 0 Then
                        Throw New Exception("Filled location/state already mapped with other price master,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    qry = "select count(*) from TSPL_CSA_PRICE_STATE_DETAIL where doc_no='" + docno + "' and state_code='" + state_code + "'"
                    check = clsDBFuncationality.getSingleValue(qry, trans)

                    If check <= 0 Then
                        Dim coll As New Hashtable()

                        clsCommon.AddColumnsForChange(coll, "doc_no", docno)
                        clsCommon.AddColumnsForChange(coll, "state_code", state_code)

                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_STATE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    End If
                    '============================================================

                    qry = "select count(*) from TSPL_CSA_LOCATION_DETAIL where doc_no='" + docno + "' and location_code='" + location_code + "'"
                    check = clsDBFuncationality.getSingleValue(qry, trans)

                    If check <= 0 Then
                        Dim coll As New Hashtable()

                        clsCommon.AddColumnsForChange(coll, "doc_no", docno)
                        clsCommon.AddColumnsForChange(coll, "location_code", location_code)

                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_LOCATION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    End If
                Next

                trans.Commit()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
            End If

            FunReset()
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

        Me.Controls.Remove(gv1)
    End Sub

    Private Sub chkStateAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkStateAll.ToggleStateChanged, chkStateSelect.ToggleStateChanged
        cmbState.Enabled = chkStateSelect.IsChecked
        If chkStateAll.IsChecked Then
            cmbState.CheckedAll()
        Else
            cmbState.UnCheckedAll()
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No document found to Print", Me.Text)
        Else
            If txtCode.Value.Length > 0 Then
                funPrint(txtCode.Value)
            End If

        End If
    End Sub

    Public Sub funPrint(ByVal Code As String)
        Try
            Dim query = " select TSPL_CSA_PRICE_DETAIL.Doc_No, TSPL_CSA_PRICE_DETAIL.Item_Code,  TSPL_CSA_PRICE_DETAIL.UOM, TSPL_CSA_PRICE_HEAD.Description, TSPL_CSA_PRICE_HEAD.CSA_Rate, TSPL_CSA_PRICE_HEAD.CSA_UOM, TSPL_CSA_PRICE_DETAIL.Diff_Rate, TSPL_CSA_PRICE_DETAIL.Ltr_Rate, TSPL_CSA_PRICE_DETAIL.Case_Rate, TSPL_CSA_PRICE_DETAIL.Pcs_Rate, TSPL_CSA_PRICE_DETAIL.Ltr_Per_Case,  TSPL_CSA_PRICE_DETAIL.Pcs_Per_Case,  TSPL_CSA_PRICE_DETAIL.CASE_UOM, tspl_item_master.item_desc, TSPL_CSA_PRICE_HEAD.CSA_Type, TSPL_CSA_PRICE_HEAD.CSA_UOM,  TSPL_CSA_PRICE_HEAD.CSA_Rate, TSPL_CSA_PRICE_HEAD.TAX, TSPL_CSA_PRICE_HEAD.Created_By, convert(varchar, TSPL_CSA_PRICE_HEAD.Created_Date,103) as Created_Date, TSPL_CSA_PRICE_HEAD.Posted_By, convert ( varchar, TSPL_CSA_PRICE_HEAD.Posted_Date ,103) as Posted_Date , TSPL_COMPANY_MASTER.Comp_Name, Logo_Img, TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end as Comp_Address ,(select  STUFF((SELECT distinct ',' + TSPL_LOCATION_MASTER.Location_Desc From	TSPL_CSA_LOCATION_DETAIL left outer join TSPL_LOCATION_MASTER  on TSPL_CSA_LOCATION_DETAIL.Location_Code=TSPL_LOCATION_MASTER.Location_Code where TSPL_CSA_LOCATION_DETAIL.doc_no='" + Code + "'  FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,1,'') ) as Location from TSPL_CSA_PRICE_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_CSA_PRICE_DETAIL.item_code left outer join TSPL_CSA_PRICE_HEAD on TSPL_CSA_PRICE_HEAD.Doc_No =TSPL_CSA_PRICE_DETAIL.Doc_No left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_CSA_PRICE_HEAD.Comp_Code where TSPL_CSA_PRICE_DETAIL.doc_no='" + Code + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptCPDPriceMaster", "CPD Price Master")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to Print", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ChkOtherItem_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkOtherItem.ToggleStateChanged
        If isInsideLoadData Then
            Exit Sub
        End If

        cmbCSAType.Enabled = Not ChkOtherItem.Checked
        txtrate.Enabled = Not ChkOtherItem.Checked
        cmbCSAType.MendatroryField = Not ChkOtherItem.Checked
        txtrate.MendatroryField = Not ChkOtherItem.Checked

        MyLabel1.Visible = Not ChkOtherItem.Checked
        cmbCSAType.Visible = Not ChkOtherItem.Checked
        MyLabel2.Visible = Not ChkOtherItem.Checked
        txtrate.Visible = Not ChkOtherItem.Checked


        RadGroupBox3.Enabled = ChkOtherItem.Checked
        RadGroupBox5.Enabled = True
        RadGroupBox2.Enabled = Not ChkOtherItem.Checked

        FillGrid()

        If ChkOtherItem.Checked Then
            ReportID = ReportID + "&OtherItem"
        Else
            ReportID = ReportID.Replace("&OtherItem", "")
        End If
    End Sub

    Private Sub chkCustAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged, chkCustSelect.ToggleStateChanged
        cbgCustomer.Enabled = chkCustSelect.IsChecked
        If chkCustAll.IsChecked Then
            cbgCustomer.CheckedAll()
        Else
            cbgCustomer.UnCheckedAll()
        End If
    End Sub

    Private Sub gv_FilterChanged(sender As Object, e As GridViewCollectionChangedEventArgs) Handles gv.FilterChanged
        If gv.Rows.Count > 0 Then
            txtTotalRow.Text = "Total Rows: " + clsCommon.myCstr(gv.MasterView.Rows.Count())
        End If
    End Sub

    Private Function OpenOtherItemUOMCal(ByVal UnitRate As Decimal, ByVal MRP As Decimal, ByVal ItemCode As String, ByVal caseUOM As String, ByVal trans As SqlTransaction) As List(Of clsCSAPriceDetail)
        Dim qry As String = ""
        Dim dt As New DataTable()
        Dim obj As New clsCSAPriceDetail()
        Dim arr As New List(Of clsCSAPriceDetail)
        Try
            Dim whrcls As String = ""

            qry = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as [Code],tspl_unit_master.unit_desc as [Unit],round(cast((" + clsCommon.myCstr(UnitRate) + " * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) / (case when isnull(BaseUOm.Conversion_Factor,0)=0 then 1 else BaseUOm.Conversion_Factor end) as float),2) as [Unit Rate],round(cast((" + clsCommon.myCstr(MRP) + " * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) / (case when isnull(BaseUOm.Conversion_Factor,0)=0 then 1 else BaseUOm.Conversion_Factor end) as float),2) as [MRP] " & _
                      " from TSPL_ITEM_UOM_DETAIL left outer join tspl_unit_master on tspl_unit_master.unit_code=TSPL_ITEM_UOM_DETAIL.uom_code left outer join TSPL_ITEM_UOM_DETAIL as BaseUOm on BaseUOm.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and BaseUOm.UOM_Code='" + clsCommon.myCstr(caseUOM) + "' "
            whrcls = " TSPL_ITEM_UOM_DETAIL.Item_Code='" + clsCommon.myCstr(ItemCode) + "' "
            dt = New DataTable()

            ''===for fill in grid
            qry += " where " + whrcls
            dt = clsDBFuncationality.GetDataTable(qry, trans)


            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    obj = New clsCSAPriceDetail()

                    obj.itemcode = clsCommon.myCstr(ItemCode)
                    obj.uom = clsCommon.myCstr(dr("code"))
                    obj.diffrate = System.Math.Round(clsCommon.myCdbl(dr("unit rate")), 2)
                    obj.MRP = System.Math.Round(clsCommon.myCdbl(dr("MRP")), 2)
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            dt = Nothing
        End Try
        Return arr
    End Function

    Private Sub chkSelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkSelect.ToggleStateChanged
        If Not isInsideLoadData AndAlso gv.Rows.Count > 0 Then
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(colSelect).Value = chkSelect.Checked
            Next
        End If
    End Sub

    Private Sub btnCombinedExport_Click(sender As Object, e As EventArgs) Handles btnCombinedExport.Click
        Dim qry As String = "select TSPL_CSA_PRICE_HEAD.Doc_no as [Doc Code],convert(varchar,TSPL_CSA_PRICE_HEAD.Doc_Date,103) as Doc_Date,TSPL_CSA_PRICE_HEAD.[Description],TSPL_CSA_PRICE_HEAD.Tax as Included_Tax,TSPL_CSA_PRICE_HEAD.CSA_Rate as [Ceka Rate],case when coalesce(TSPL_CSA_PRICE_HEAD.For_Other_Item,0)=0 then 0 else 1 end as For_Other_Item,convert(varchar,TSPL_CSA_PRICE_HEAD.[Expiry_Date],103) as Expiry_On,TSPL_CSA_PRICE_DETAIL.Item_Code,TSPL_CSA_PRICE_DETAIL.CASE_UOM as [UnitCode],TSPL_CSA_PRICE_DETAIL.Diff_Rate as [CPD_Diff_Rate],TSPL_CSA_PRICE_CSA_DETAIL.Cust_Code,TSPL_CSA_PRICE_DETAIL.MRP,TSPL_CSA_PRICE_STATE_DETAIL.State_Code,TSPL_CSA_LOCATION_DETAIL.Location_Code from TSPL_CSA_PRICE_HEAD left outer join TSPL_CSA_PRICE_DETAIL on TSPL_CSA_PRICE_DETAIL.Doc_No=TSPL_CSA_PRICE_HEAD.Doc_No left outer join TSPL_CSA_PRICE_CSA_DETAIL on TSPL_CSA_PRICE_CSA_DETAIL.Doc_No=TSPL_CSA_PRICE_HEAD.Doc_No left outer join TSPL_CSA_PRICE_STATE_DETAIL on TSPL_CSA_PRICE_STATE_DETAIL.Doc_No=TSPL_CSA_PRICE_HEAD.Doc_No left outer join TSPL_CSA_LOCATION_DETAIL on TSPL_CSA_LOCATION_DETAIL.Doc_No=TSPL_CSA_PRICE_HEAD.Doc_No "
        transportSql.ExporttoExcel(qry, Me)
    End Sub


    Private Sub LoadDefaultDocinImport(ByVal gv1 As RadGridView)
        Try
            Dim repoLineNo As New GridViewTextBoxColumn()

            ''===============head=======================================
            repoLineNo = New GridViewTextBoxColumn()
            repoLineNo.FormatString = ""
            repoLineNo.HeaderText = "Doc Code"
            repoLineNo.Name = colImpDocCode
            repoLineNo.Width = 50
            repoLineNo.WrapText = True
            repoLineNo.IsVisible = False
            repoLineNo.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoLineNo)

            Dim arr As New ArrayList()
            Dim Unique_Identity As String = ""
            Dim strDocCode As String = ""
            For Each grow As GridViewRowInfo In gv1.Rows
                Unique_Identity = clsCommon.myCstr(grow.Cells("Doc Code").Value) + "&" + clsCommon.myCstr(grow.Cells("Doc_Date").Value) + "&" + clsCommon.myCstr(grow.Cells("Included_Tax").Value) + "&" + clsCommon.myCstr(grow.Cells("Ceka Rate").Value) + "&" + clsCommon.myCstr(grow.Cells("For_Other_Item").Value)

                If clsCommon.myLen(Unique_Identity) > 0 AndAlso Not arr.Contains(Unique_Identity) Then
                    arr.Add(Unique_Identity)
                    If clsCommon.myLen(strDocCode) <= 0 Then
                        strDocCode = "CPDUP00000000001"
                    Else
                        strDocCode = clsCommon.incval(strDocCode)
                    End If
                End If
                grow.Cells(colImpDocCode).Value = strDocCode
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnCombinedImport_Click(sender As Object, e As EventArgs) Handles btnCombinedImport.Click
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)

        Dim arrDocNo As New ArrayList()
        Dim Doc_Date As DateTime? = Nothing
        Dim Description As String = Nothing
        Dim CerkaRate As Decimal = 0
        Dim For_Other_Item As String = Nothing
        Dim Expiry_On As DateTime? = Nothing
        Dim Item_Code As String = Nothing
        Dim UnitCode As String = Nothing
        Dim CPD_Diff_Rate As Decimal = 0
        Dim Cust_Code As String = Nothing
        Dim State_Code As String = Nothing
        Dim Location_Code As String = Nothing
        Dim qry As String = Nothing
        Dim check As Integer = 0
        Dim Is_MRP As Integer = 0
        Dim Doc_No As String = Nothing
        Dim coll As New Hashtable
        Dim CSA_UOM As String = Nothing
        Dim Revision_No As String = Nothing
        Dim Line_No As Integer = 0
        Dim ltr_per_case As Decimal = 0
        Dim pcs_per_case As Decimal = 0
        Dim stockingunit As String = ""
        Dim cnvrsn As Decimal = 1
        Dim case_cnvrsn As Decimal = 0
        Dim diffrate As Decimal = 0
        Dim org_rate As Decimal = 0
        Dim case_rate As Decimal = 0
        Dim pcs_rate As Decimal = 0
        Dim StateCodeExist As String = Nothing
        Dim CustCodeExist As String = Nothing
        Dim LocationCodeExist As String = Nothing
        Dim IsDetailExist As String = Nothing
        Dim Wherecls As String = Nothing
        Dim MRP As Decimal = Nothing
        Dim doc_code As String = Nothing
        Dim IncludingTax As String = Nothing
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If transportSql.importExcel(gv1, "Doc Code", "Doc_Date", "Description", "Included_Tax", "Ceka Rate", "For_Other_Item", "Expiry_On", "Item_Code", "UnitCode", "CPD_Diff_Rate", "Cust_Code", "MRP", "State_Code", "Location_Code") Then

                ''do sorting of records for easy saving purpose.
                Dim dt As New DataTable()
                dt = gv1.DataSource()
                dt.DefaultView.Sort = " [Doc Code],[Doc_Date],[Included_Tax],[Ceka Rate],[For_Other_Item] "
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()

                gv1.DataSource = dt.DefaultView.ToTable()

                LoadDefaultDocinImport(gv1)
                ''======================end here========================

                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv1.Rows

                    doc_code = clsCommon.myCstr(grow.Cells("doc code").Value)
                    IncludingTax = clsCommon.myCstr(grow.Cells("included_tax").Value)

                    If clsCommon.myLen(IncludingTax) <= 0 Then
                        IncludingTax = "No"
                    End If
                    If clsCommon.myLen(IncludingTax) > 0 AndAlso clsCommon.CompairString(IncludingTax, "No") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(IncludingTax, "Yes") <> CompairStringResult.Equal Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Included_Tax should be Yes/No at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    For_Other_Item = clsCommon.myCstr(grow.Cells("For_Other_Item").Value)
                    CerkaRate = clsCommon.myCdbl(grow.Cells("Ceka Rate").Value)
                    Description = clsCommon.myCstr(grow.Cells("Description").Value)
                    If grow.Cells("Doc_Date").Value IsNot Nothing AndAlso clsCommon.myLen(grow.Cells("Doc_Date").Value) > 0 AndAlso IsDate(grow.Cells("Doc_Date").Value) Then
                        Doc_Date = clsCommon.myCDate(grow.Cells("Doc_Date").Value)
                    Else
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter Doc Date at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If (clsCommon.myLen(For_Other_Item) <= 0) Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter For Other Item at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(For_Other_Item), "0") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(For_Other_Item), "1") <> CompairStringResult.Equal Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter For Other Item 1 or 0 only at line no. " + clsCommon.myCstr(grow.Index + 1))
                    End If

                    If clsCommon.myLen(For_Other_Item) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(For_Other_Item), "0") = CompairStringResult.Equal AndAlso clsCommon.myLen(CerkaRate) <= 0 AndAlso clsCommon.CompairString(clsCommon.myCdbl(CerkaRate), 0) = CompairStringResult.Equal Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter Ceka Rate at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(For_Other_Item), "0") = CompairStringResult.Equal AndAlso clsCommon.myLen(CerkaRate) > 0 Then
                        If (clsCommon.CompairString(CerkaRate, "0")) = CompairStringResult.Equal Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Ceka rate can not be 0 at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If
                    If (clsCommon.myLen(grow.Cells("Expiry_On").Value) > 0) Then
                        If IsDate(grow.Cells("Expiry_On").Value) Then
                            Expiry_On = clsCommon.myCDate(grow.Cells("Expiry_On").Value)
                        ElseIf AllowOtherItems Then ''when setting is ON for other item then expiry is mandatory
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Enter Valid Expiry Date at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        ElseIf Not AllowOtherItems AndAlso Not IsDate(grow.Cells("Expiry_On").Value) Then
                            Expiry_On = Nothing
                        End If
                    End If

                    If AllowOtherItems = True Then
                        If clsCommon.myLen(grow.Cells("Expiry_On").Value) <= 0 Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Enter Expire Date at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        Else
                            Expiry_On = clsCommon.myCDate(grow.Cells("Expiry_On").Value)
                        End If
                    End If

                    If clsCommon.myLen(grow.Cells("Expiry_On").Value) > 0 Then
                        If clsCommon.myCDate(grow.Cells("Expiry_On").Value) <= clsCommon.myCDate(grow.Cells("Doc_Date").Value) Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Expiry Date must be greater than Doc Date at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        Else
                            Expiry_On = clsCommon.myCDate(grow.Cells("Expiry_On").Value)
                        End If
                    End If

                    Item_Code = clsCommon.myCstr(grow.Cells("Item_Code").Value)
                    If clsCommon.myLen(Item_Code) <= 0 Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter Item Code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If clsCommon.myLen(Item_Code) > 0 Then
                        qry = "select count(*) from TSPL_ITEM_MASTER where Item_Code='" + Item_Code + "' and Item_Type in ('F','T') and isnull(Is_FreshItem,0)<>1 "
                        If clsCommon.myCstr(For_Other_Item) = "1" Then
                            qry += " and CSA_Type<>'CPD-DESI GHEE'"
                        Else
                            qry += " and CSA_Type='CPD-DESI GHEE'"
                        End If
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Filled Item Code " + Item_Code + "is not valid or not finished, Trading and CPD Type,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    UnitCode = clsCommon.myCstr(grow.Cells("UnitCode").Value)

                    If clsCommon.myLen(UnitCode) <= 0 Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter Unit Code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If


                    If clsCommon.myLen(UnitCode) > 0 Then
                        qry = Nothing
                        qry = "select count(*) from TSPL_ITEM_UOM_DETAIL where Item_Code='" + Item_Code + "' and UOM_Code='" + UnitCode + "' "
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Filled Unit Code is not valid,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    CPD_Diff_Rate = clsCommon.myCdbl(grow.Cells("CPD_Diff_Rate").Value)
                    If clsCommon.myLen(CPD_Diff_Rate) <= 0 Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter CPD Diff. rate at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If clsCommon.myLen(CPD_Diff_Rate) > 0 AndAlso Not IsNumeric(CPD_Diff_Rate) Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("CPD Diff. rate should be numeric at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    'If clsCommon.myLen(CPD_Diff_Rate) > 0 Then
                    '    If (clsCommon.CompairString(CPD_Diff_Rate, "0")) = CompairStringResult.Equal Then
                    '        clsCommon.ProgressBarHide()
                    '        Throw New Exception("CPD Diff. rate can not be 0 at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    '    End If
                    'End If


                    Cust_Code = clsCommon.myCstr(grow.Cells("Cust_Code").Value)
                    If clsCommon.myLen(Cust_Code) > 0 Then
                        qry = Nothing
                        qry = "select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" + Cust_Code + "' and CSA_Type='Y' "
                        check = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                        If check <= 0 Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Filled Customer Code " + Cust_Code + "is not valid. First create Customer, see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")

                        End If
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(For_Other_Item), "1") = CompairStringResult.Equal AndAlso clsCommon.myLen(Cust_Code) <= 0 Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter Cust Code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    qry = Nothing
                    qry = "select Is_Mrp from TSPL_Item_Master where Item_Code='" + Item_Code + "'  "
                    Is_MRP = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                    If clsCommon.CompairString(clsCommon.myCstr(Is_MRP), 1) = CompairStringResult.Equal Then
                        MRP = clsCommon.myCdbl(grow.Cells("MRP").Value)
                    End If

                    If ExciseEnableOnSalePatti = True AndAlso clsCommon.CompairString(clsCommon.myCstr(Is_MRP), 1) = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(MRP) <= 0 Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Enter MRP at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If



                    State_Code = clsCommon.myCstr(grow.Cells("State_Code").Value)
                    If clsCommon.myLen(State_Code) > 0 Then
                        qry = Nothing
                        qry = "select count(*) from tspl_STate_Master where STATE_CODE='" + State_Code + "'  "
                        check = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                        If check <= 0 Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Filled State Code " + State_Code + "is not valid, see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")

                        End If
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(For_Other_Item), "0") = CompairStringResult.Equal AndAlso clsCommon.myLen(State_Code) <= 0 Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter State Code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    Location_Code = clsCommon.myCstr(grow.Cells("Location_Code").Value)
                    If clsCommon.myLen(Location_Code) <= 0 Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter Location Code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If clsCommon.myLen(Location_Code) > 0 Then
                        qry = Nothing
                        qry = "select count(*) from TSPL_LOCATION_MASTER where Location_Code='" + Location_Code + "'  "
                        check = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                        If check <= 0 Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Filled Location Code " + Location_Code + "is not valid, see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")

                        End If
                    End If


                    'DONE BY STUTI ON 08/12/2016 FOR DATE

                    If Not arrDocNo.Contains(clsCommon.myCstr(grow.Cells(colImpDocCode).Value)) Then
                        arrDocNo.Add(clsCommon.myCstr(grow.Cells(colImpDocCode).Value))

                        qry = "select TSPL_CSA_PRICE_HEAD.doc_no from TSPL_CSA_PRICE_HEAD " & _
                    " left outer join TSPL_CSA_PRICE_STATE_DETAIL on TSPL_CSA_PRICE_HEAD.Doc_No=TSPL_CSA_PRICE_STATE_DETAIL.Doc_No left outer join TSPL_CSA_LOCATION_DETAIL on TSPL_CSA_LOCATION_DETAIL.Doc_No=TSPL_CSA_PRICE_HEAD.Doc_No " & _
                    " left outer join TSPL_CSA_PRICE_CSA_DETAIL on TSPL_CSA_PRICE_CSA_DETAIL.doc_no=TSPL_CSA_PRICE_HEAD.doc_no "
                        qry += " where TSPL_CSA_PRICE_HEAD.doc_no <>'" + doc_code + "' and TSPL_CSA_PRICE_HEAD.tax='" + IncludingTax + "' "
                        If AllowOtherItems AndAlso For_Other_Item > 0 Then
                            qry += " and TSPL_CSA_PRICE_HEAD.For_Other_Item=1 and TSPL_CSA_PRICE_CSA_DETAIL.cust_code in ('" + Cust_Code + "') "
                        Else
                            qry += " and TSPL_CSA_PRICE_STATE_DETAIL.State_Code in ('" + State_Code + "')  and TSPL_CSA_PRICE_HEAD.csa_type='CPD-DESI GHEE' and isnull(TSPL_CSA_PRICE_HEAD.For_Other_Item,0) =0 "
                        End If

                        qry += " and TSPL_CSA_LOCATION_DETAIL.Location_Code in ('" + Location_Code + "')  AND convert(date,TSPL_CSA_PRICE_HEAD.DOC_DATE,103)='" + clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") + "'"

                        Dim check1 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                        If clsCommon.myLen(check1) > 0 Then
                            If AllowOtherItems AndAlso For_Other_Item > 0 Then
                                Throw New Exception("Selected Customer already mapped ,for more change edit old record (" + clsCommon.myCstr(check1) + ") dated (" + clsCommon.GetPrintDate(Doc_Date, "dd/MM/yyyy") + ").")
                            Else
                                Throw New Exception("Selected locations already mapped with selected states,for more change edit old record (" + clsCommon.myCstr(check1) + ") dated (" + clsCommon.GetPrintDate(Doc_Date, "dd/MM/yyyy") + ").")
                            End If
                        End If
                    End If
                    '=================END HERE=============
                    

                    'Insert TSPL_CSA_PRICE_HEAD
                    CSA_UOM = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_unit_master where isnull(rt_rate,'N')='Y'", trans))


                    Dim Doc_NoExist As String = ""
                    If clsCommon.myLen(doc_code) > 0 Then
                        qry = "select Doc_No from tspl_CSA_Price_Head where doc_no='" + doc_code + "' and For_Other_Item='" + For_Other_Item + "'"
                        Doc_NoExist = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    Else
                        qry = "select max(Doc_No) from tspl_CSA_Price_Head where Doc_Date='" + clsCommon.GetPrintDate(Doc_Date) + "' and For_Other_Item='" + For_Other_Item + "' "
                        Doc_NoExist = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    End If


                    If clsCommon.CompairString(Doc_NoExist, "") = CompairStringResult.Equal Then
                        Doc_No = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.CSAPRICEMAASTER, "", ""))
                    Else
                        Doc_No = Doc_NoExist
                        qry = Nothing
                        qry = "select Revision_No from TSPL_CSA_PRICE_head where doc_no='" + Doc_No + "'"
                        Revision_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                        If clsCommon.myLen(Revision_No) > 0 Then
                            Revision_No = clsCommon.incval(Revision_No)
                        ElseIf clsCommon.myLen(Revision_No) <= 0 Then
                            If Cust_Code IsNot Nothing AndAlso clsCommon.myLen(Revision_No) > 0 Then
                                If clsCommon.myLen(Cust_Code) > 3 Then
                                    Revision_No = clsCommon.myCstr(Cust_Code).Substring(0, 3) + "0000000001"
                                Else
                                    Revision_No = clsCommon.myCstr(Cust_Code) + "0000000001"
                                End If
                            ElseIf Location_Code IsNot Nothing AndAlso clsCommon.myLen(Location_Code) > 0 Then
                                Revision_No = ""
                                If clsCommon.myLen(State_Code) > 3 Then
                                    Revision_No += clsCommon.myCstr(State_Code).Substring(0, 3)
                                Else
                                    Revision_No += clsCommon.myCstr(State_Code)
                                End If
                                If clsCommon.myLen(Location_Code) > 3 Then
                                    Revision_No += clsCommon.myCstr(Location_Code).Substring(0, 3)
                                Else
                                    Revision_No += clsCommon.myCstr(Location_Code)
                                End If

                                Revision_No += "0000000001"

                            End If
                        End If
                    End If
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "doc_no", Doc_No)
                    clsCommon.AddColumnsForChange(coll, "Description", Description)
                    clsCommon.AddColumnsForChange(coll, "CSA_Type", "CPD-DESI GHEE")
                    clsCommon.AddColumnsForChange(coll, "CSA_UOM", CSA_UOM)
                    clsCommon.AddColumnsForChange(coll, "CSA_Rate", CerkaRate)
                    clsCommon.AddColumnsForChange(coll, "TAX", IncludingTax)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                    clsCommon.AddColumnsForChange(coll, "For_Other_Item", For_Other_Item)
                    clsCommon.AddColumnsForChange(coll, "Revision_No", Revision_No)

                    If Expiry_On IsNot Nothing AndAlso clsCommon.myLen(Expiry_On) > 0 AndAlso IsDate(Expiry_On) Then
                        clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(Expiry_On, "dd/MMM/yyyy"))
                    End If

                    If Doc_Date IsNot Nothing AndAlso clsCommon.myLen(Doc_Date) > 0 AndAlso IsDate(Doc_Date) Then
                        clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy"))
                    End If
                    If clsCommon.CompairString(Doc_NoExist, "") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_HEAD", OMInsertOrUpdate.Insert, "", trans)
                    Else

                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_HEAD", OMInsertOrUpdate.Update, "Doc_No='" + Doc_No + "'", trans)
                    End If



                    'End of Insert TSPL_CSA_PRICE_HEAD

                    'Insert TSPL_CSA_PRICE_Detail

                    coll = New Hashtable()

                    Line_No = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull((max(isnull(Line_No,1))+1),1) from TSPL_CSA_PRICE_DETAIL where Doc_No='" + Doc_No + "'", trans))

                    clsCommon.AddColumnsForChange(coll, "doc_no", Doc_No)

                    clsCommon.AddColumnsForChange(coll, "Item_Code", Item_Code)
                    clsCommon.AddColumnsForChange(coll, "UOM", CSA_UOM)
                    clsCommon.AddColumnsForChange(coll, "Diff_Rate", CPD_Diff_Rate)
                    If For_Other_Item = "0" Then
                        Dim uom_ltr As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(Item_Code) + "' and UOM_Code='" + CSA_UOM + "'", trans))
                        Dim pcs_uom As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(Item_Code) + "' and stocking_unit='Y'", trans))
                        Dim case_uom As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(Item_Code) + "' and UOM_Code = '" + CSA_UOM + "'", trans))

                        If uom_ltr <= 0 Then
                            uom_ltr = 1
                        End If
                        If pcs_uom <= 0 Then
                            pcs_uom = 1
                        End If
                        If case_uom <= 0 Then
                            ltr_per_case = 0
                            pcs_per_case = 0
                        Else
                            ltr_per_case = System.Math.Round(case_uom / uom_ltr, 2)
                            pcs_per_case = System.Math.Round(case_uom / pcs_uom, 2)
                        End If
                        qry = Nothing
                        qry = "select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(Item_Code) + "' and uom_code='" + UnitCode + "'"
                        cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                        If cnvrsn <= 0 Then
                            cnvrsn = 1
                        End If

                        stockingunit = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select uom_code from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(Item_Code) + "' and stocking_unit='Y'", trans))
                        case_cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(Item_Code) + "' and uom_code='" + UnitCode + "'", trans))

                        diffrate = CPD_Diff_Rate
                        org_rate = (clsCommon.myCdbl(CerkaRate) + clsCommon.myCdbl(diffrate))

                        '========case rate====            





                        case_rate = System.Math.Round((clsCommon.myCdbl(org_rate) * case_cnvrsn) / cnvrsn, 2)

                        '====pcs rate=================           

                        If clsCommon.myCdbl(case_cnvrsn) > 0 Then
                            pcs_rate = System.Math.Round((clsCommon.myCdbl(case_rate) / clsCommon.myCdbl(case_cnvrsn)), 2)
                        Else
                            pcs_rate = 0

                        End If
                    End If

                    clsCommon.AddColumnsForChange(coll, "Ltr_Rate", org_rate)
                    clsCommon.AddColumnsForChange(coll, "Case_Rate", case_rate)
                    clsCommon.AddColumnsForChange(coll, "Pcs_Rate", pcs_rate)
                    clsCommon.AddColumnsForChange(coll, "Ltr_Per_Case", ltr_per_case)
                    clsCommon.AddColumnsForChange(coll, "Pcs_Per_Case", pcs_per_case)
                    clsCommon.AddColumnsForChange(coll, "CASE_UOM", UnitCode, True)
                    clsCommon.AddColumnsForChange(coll, "MRP", MRP)

                    IsDetailExist = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CSA_PRICE_DETAIL where Doc_No='" + Doc_No + "' and Item_Code='" + Item_Code + "'", trans))
                    If IsDetailExist = "0" Then
                        clsCommon.AddColumnsForChange(coll, "Line_no", Line_No)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_DETAIL", OMInsertOrUpdate.Update, "Doc_No='" + Doc_No + "' and Item_Code='" + Item_Code + "'", trans)
                    End If
                    'End of Insert TSPL_CSA_PRICE_Detail

                    'Insert TSPL_CSA_Location_Detail
                    LocationCodeExist = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select count(*) from TSPL_CSA_LOCATION_DETAIL where Doc_No='" + Doc_No + "' and Location_Code='" + Location_Code + "'", trans))
                    If LocationCodeExist = "0" Then
                        coll = New Hashtable()

                        clsCommon.AddColumnsForChange(coll, "Doc_No", Doc_No)
                        clsCommon.AddColumnsForChange(coll, "Location_Code", Location_Code)

                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_LOCATION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    End If
                    'End of Insert TSPL_CSA_Location_Detail

                    'Insert TSPL_CSA_PRICE_STATE_DETAIL
                    If For_Other_Item = "0" Then
                        StateCodeExist = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CSA_PRICE_STATE_DETAIL where Doc_No='" + Doc_No + "' and State_Code='" + State_Code + "'", trans))
                        If StateCodeExist = "0" Then
                            coll = New Hashtable()

                            clsCommon.AddColumnsForChange(coll, "Doc_No", Doc_No)
                            clsCommon.AddColumnsForChange(coll, "State_Code", State_Code, True)

                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_STATE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                            'End of Insert TSPL_CSA_PRICE_STATE_DETAIL
                        End If
                    End If

                    'Insert TSPL_CSA_PRICE_CSA_DETAIL
                    If For_Other_Item = "1" Then
                        CustCodeExist = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CSA_PRICE_CSA_DETAIL where Doc_No='" + Doc_No + "' and Cust_Code='" + Cust_Code + "'", trans))
                        If CustCodeExist = "0" Then
                            coll = New Hashtable()

                            clsCommon.AddColumnsForChange(coll, "Doc_No", Doc_No)
                            clsCommon.AddColumnsForChange(coll, "Cust_Code", Cust_Code)

                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_CSA_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                        End If
                    End If
                    'End Insert TSPL_CSA_PRICE_CSA_DETAIL

                    'Insert in TSPL_CSA_PRICE_OTHER_ITEM_DETAIL
                    If For_Other_Item = "1" Then

                        Dim UnitRate As Decimal = CPD_Diff_Rate
                        Dim MRPAmt As Decimal = MRP
                        Dim whrcls As String = ""

                        qry = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as [Code],tspl_unit_master.unit_desc as [Unit],round(cast((" + clsCommon.myCstr(UnitRate) + " * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) / (case when isnull(BaseUOm.Conversion_Factor,0)=0 then 1 else BaseUOm.Conversion_Factor end) as float),2) as [Unit Rate],round(cast((" + clsCommon.myCstr(MRPAmt) + " * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) / (case when isnull(BaseUOm.Conversion_Factor,0)=0 then 1 else BaseUOm.Conversion_Factor end) as float),2) as [MRP] " & _
                                  " from TSPL_ITEM_UOM_DETAIL left outer join tspl_unit_master on tspl_unit_master.unit_code=TSPL_ITEM_UOM_DETAIL.uom_code left outer join TSPL_ITEM_UOM_DETAIL as BaseUOm on BaseUOm.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and BaseUOm.UOM_Code='" + clsCommon.myCstr(CSA_UOM) + "' "
                        whrcls = " TSPL_ITEM_UOM_DETAIL.Item_Code='" + clsCommon.myCstr(Item_Code) + "' "
                        dt = New DataTable()

                        '  If OpenDialogBox Then
                        'Dim strcode As String = ""
                        'strcode = clsCommon.myCstr(clsCommon.ShowSelectForm("OTHRITMPRC", qry, "Code", whrcls, strcode, "", True)) ''for show on double click
                        ' End If

                        ''===for fill in grid
                        qry += " where " + whrcls
                        dt = clsDBFuncationality.GetDataTable(qry, trans)

                        Dim ArrItemExist As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Count(*) from TSPL_CSA_PRICE_OTHER_ITEM_DETAIL where Doc_No='" + Doc_No + "' and Item_Code='" + Item_Code + "'", trans))
                        If ArrItemExist <> "0" Then
                            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_CSA_PRICE_OTHER_ITEM_DETAIL where Doc_No='" + Doc_No + "' and Item_Code='" + Item_Code + "'", trans)
                        End If
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            For Each dr As DataRow In dt.Rows
                                coll = New Hashtable()
                                Dim uom As String = clsCommon.myCstr(dr("code"))
                                Dim diffrateImp As String = System.Math.Round(clsCommon.myCdbl(dr("unit rate")), 2)
                                Dim MRP_Val As String = System.Math.Round(clsCommon.myCdbl(dr("MRP")), 2)
                                clsCommon.AddColumnsForChange(coll, "doc_no", Doc_No)
                                clsCommon.AddColumnsForChange(coll, "Item_Code", Item_Code)
                                clsCommon.AddColumnsForChange(coll, "UOM", uom)
                                clsCommon.AddColumnsForChange(coll, "Unit_Rate", diffrateImp)
                                clsCommon.AddColumnsForChange(coll, "MRP", MRP_Val)
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_OTHER_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                            Next
                        End If
                    End If
                    'End of Insert TSPL_CSA_PRICE_OTHER_ITEM_DETAIL 
                Next

                ''============history======================
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CSA_PRICE_HEAD", "doc_no", trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CSA_PRICE_DETAIL", "doc_no", trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CSA_LOCATION_DETAIL", "doc_no", trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CSA_PRICE_STATE_DETAIL", "doc_no", trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CSA_PRICE_OTHER_ITEM_DETAIL", "doc_no", trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CSA_PRICE_CSA_DETAIL", "doc_no", trans)
                ''========================================================

                trans.Commit()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
        End Try
        Me.Controls.Remove(gv1)
    End Sub


    Private Sub gv_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If e.Column Is gv.Columns(colMRP) Then
                    If clsCommon.CompairString(gv.CurrentRow.Cells(colIsMRP).Value, "1") = CompairStringResult.Equal Then
                        gv.CurrentRow.Cells(colMRP).ReadOnly = False
                    Else
                        gv.CurrentRow.Cells(colMRP).ReadOnly = True
                    End If
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAmendment_Click(sender As Object, e As EventArgs) Handles btnAmendment.Click
        Try
            Dim isAmendmentNo As Boolean = False
            Dim Reason As String = ""
            If clsCommon.CompairString(btnsave.Text, "Update") = CompairStringResult.Equal AndAlso btnPost.Visible AndAlso btnPost.Enabled = False Then
                If common.clsCommon.MyMessageBoxShow("Do you want to make Amendment", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    isAmendmentNo = True
                Else
                    Exit Sub
                End If

                If AllowToSave() AndAlso SaveData(isAmendmentNo) Then
                    clsCSAPriceMaster.PostData(MyBase.Form_ID, clsCommon.myCstr(txtCode.Value))
                    common.clsCommon.MyMessageBoxShow(Me, "Record Successfully Amendmented.", Me.Text)

                    LoadData(clsCommon.myCstr(txtCode.Value), NavigatorType.Current)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Post record first.", Me.Text)
            End If ''approved cond. check

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

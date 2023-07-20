Imports common
Public Class frmFrieghtRateMaster
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isNewEntry As Boolean = False
    Const ColSNo As String = "ColSNo"
    Const ColPKID As String = "ColPKID"
    Const colCustCode As String = "colCustCode"
    Const colCustName As String = "colCustName"
    Const colZoneCode As String = "colZoneCode"
    Const colUOMCode As String = "colUOMCode"
    Const colFrieghtRate As String = "colFrieghtRate"
    Private isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
#End Region
    Private Sub frmFrieghtRateMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        chkInactive.Enabled = False
        AddNew()
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            txtLocation.Value = clsDBFuncationality.getSingleValue("select Location_Code from TSPL_Location_Master where Location_Code in(" + clsCommon.myCstr(objCommonVar.strCurrUserLocations) + ")") 'clsCommon.myCstr(objCommonVar.strCurrUserLocations)
            lblLocationDesc.Text = clsLocation.GetName(txtLocation.Value, Nothing)
        End If
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Sub AddNew()
        lblStatus.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btnPost.Enabled = True
        btndelete.Enabled = True
        btnsave.Text = "Save"
        isNewEntry = True
        txtCode.Value = ""
        'txtLocation.Value = ""
        'lblLocationDesc.Text = ""
        txtDescription.Text = ""
        txtStartDate.Value = clsCommon.GETSERVERDATE()
        txtEndDate.Value = clsCommon.GETSERVERDATE()
        chkInactive.Enabled = False
        LoadBlankGrid()
        gv1.Rows.AddNew()
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = ColSNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "PKID"
        repoNumBox.Name = ColPKID
        repoNumBox.IsVisible = False
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)
        Dim repoCustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustCode.FormatString = ""
        repoCustCode.HeaderText = "Customer Code"
        repoCustCode.Name = colCustCode
        repoCustCode.Width = 150
        repoCustCode.IsVisible = True
        repoCustCode.TextImageRelation = TextImageRelation.TextBeforeImage
        'repoCustCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCustCode)
        Dim repoCustName = New GridViewTextBoxColumn()
        repoCustName.FormatString = ""
        repoCustName.HeaderText = "Customer Name"
        repoCustName.Name = colCustName
        repoCustName.Width = 200
        repoCustName.IsVisible = True
        repoCustName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCustName)
        Dim repoTextBox2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBox2.FormatString = ""
        repoTextBox2.HeaderText = "Zone"
        repoTextBox2.Name = colZoneCode
        'repoTextBox2.HeaderImage = Global.XpertERPBulkProcurement.My.Resources.Resources.search4
        repoTextBox2.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox2.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox2)
        Dim repoTextBoxUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBoxUOM.FormatString = ""
        repoTextBoxUOM.HeaderText = "UOM"
        repoTextBoxUOM.Name = colUOMCode
        repoTextBoxUOM.IsVisible = True
        repoTextBoxUOM.Width = 80
        repoTextBoxUOM.TextImageRelation = TextImageRelation.TextBeforeImage
        'repoTextBoxUOM.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTextBoxUOM)
        Dim repoTextBox = New GridViewDecimalColumn()
        repoTextBox.FormatString = "{0:n2}"
        repoTextBox.HeaderText = "Frieght Rate"
        repoTextBox.Name = colFrieghtRate
        repoTextBox.Width = 80
        repoTextBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoTextBox.DecimalPlaces = 2
        repoTextBox.IsVisible = True
        'repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)
        gv1.Enabled = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AllowDeleteRow = True
    End Sub
    Sub RefeshSNO()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
        Next
    End Sub
    Private Sub gv1_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gv1.UserDeletedRow
        Try
            RefeshSNO()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
    Private Sub gv1_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
            Else
                If lblStatus.Status = ERPTransactionStatus.Approved Then
                    clsFrieghtRateDetail.DeleteData(clsCommon.myCdbl(gv1.CurrentRow.Cells(ColPKID).Value))
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],MCC_Type as [MCC Type],CST_No as [CST No],Phone1,Phone2  from TSPL_Location_MASTER"
            Dim WhrCls As String = " Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("MulDS-BOLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            If txtCode.MyReadOnly OrElse isButtonClicked Then
                Dim whrClas As String = "2=2"
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrClas += " and Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
                End If
                Dim qry As String = "select PK_ID as [Code],From_Date as [Start Date],Location_Code as [Location],Created_Date as [Created Date],Modified_Date as [Modified Date] from TSPL_DCS_FOR_SALE_FRIEGHT"
                LoadData(clsCommon.myCDecimal(clsCommon.ShowSelectForm("RPTBDMST", qry, "Code", whrClas, txtCode.Value, "Code", isButtonClicked)), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_DCS_FOR_SALE_Frieght where PK_ID='" + txtCode.Value + "'"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qst += " and Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(clsCommon.myCDecimal(txtCode.Value), NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal PK_ID As Integer, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            LoadBlankGrid()
            Dim obj As New clsFrieghtRateMaster()
            obj = clsFrieghtRateMaster.GetData(PK_ID, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PK_ID) > 0) Then

                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                    btndelete.Enabled = False
                    If chkInactive.Checked = False Then
                        chkInactive.Enabled = True
                    End If

                Else
                    chkInactive.Enabled = False
                    btnsave.Enabled = True
                    btnPost.Enabled = True
                    btndelete.Enabled = True
                    btnsave.Text = "Update"
                End If
                lblStatus.Status = obj.Status
                txtCode.Value = obj.PK_ID
                txtStartDate.Value = obj.From_Date
                If obj.To_Date IsNot Nothing Then
                    txtEndDate.Value = obj.To_Date
                End If
                txtLocation.Value = obj.Location_Code
                lblLocationDesc.Text = obj.Location_Name
                txtDescription.Text = obj.Description
                chkInactive.Checked = IIf(obj.Inactive = 0, False, True)
                Dim PreviousSNo As Integer = -1
                Dim sl As Integer = 1
                If obj.Arr_FrieghtDetail IsNot Nothing AndAlso obj.Arr_FrieghtDetail.Count > 0 Then
                    For Each objTr As clsFrieghtRateDetail In obj.Arr_FrieghtDetail
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = sl
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColPKID).Value = objTr.PK_ID
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustCode).Value = objTr.Customer_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustName).Value = objTr.Customer_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colZoneCode).Value = objTr.Zone_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUOMCode).Value = objTr.UOM_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFrieghtRate).Value = objTr.Frieght_Rate
                        sl += 1
                    Next
                    gv1.Rows.AddNew()

                End If
            End If


        Catch ex As Exception

            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCDecimal(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colCustCode) Then
                        Dim whrcls As String = " Cust_Code in(select Customer_Code from TSPL_CUSTOMER_LOCATION_MAPPING where 2=2"
                        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                            whrcls += " AND Location_code in (" + objCommonVar.strCurrUserLocations + "))"
                        Else
                            whrcls += ")"
                        End If
                        gv1.CurrentRow.Cells(colCustCode).Value = clsCommon.myCstr(clsCustomerMaster.getFinder(whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value), False))
                        gv1.CurrentRow.Cells(colCustName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + gv1.CurrentRow.Cells(colCustCode).Value + "'"))
                    ElseIf e.Column Is gv1.Columns(colZoneCode) Then
                        Dim whrcls As String = " 2=2"
                        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                            whrcls += " AND Location_code in (" + objCommonVar.strCurrUserLocations + ")"

                        End If
                        gv1.CurrentRow.Cells(colZoneCode).Value = ClsZoneMaster.getFinder(whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colZoneCode).Value), False)
                    ElseIf e.Column Is gv1.Columns(colUOMCode) Then
                        gv1.CurrentRow.Cells(colUOMCode).Value = clsUnitMaster.getUnitFinder("", clsCommon.myCstr(gv1.CurrentRow.Cells(colUOMCode).Value), False)
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        'Prevent future date transaction

        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            txtLocation.Focus()
            Throw New Exception("Please select Location")
        End If
        Dim Arr As New List(Of String)

        For i As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows(i).Cells(colCustCode).Value) > 0 And clsCommon.myLen(gv1.Rows(i).Cells(colZoneCode).Value) > 0 And clsCommon.myLen(gv1.Rows(i).Cells(colUOMCode).Value) > 0 Then
                Dim str As String = gv1.Rows(i).Cells(colCustCode).Value + gv1.Rows(i).Cells(colZoneCode).Value + gv1.Rows(i).Cells(colUOMCode).Value

                If Arr.Contains(str) Then
                    Throw New Exception("Duplicate Data Found at Line No: " + clsCommon.myCstr(i + 1))
                Else
                    Arr.Add(str)
                End If
            End If


        Next
        Return True
    End Function
    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsFrieghtRateMaster()
                obj.PK_ID = clsCommon.myCDecimal(txtCode.Value)
                obj.From_Date = clsCommon.GetPrintDate(txtStartDate.Value)
                If txtEndDate.Checked Then
                    obj.To_Date = clsCommon.GetPrintDate(txtEndDate.Value)
                End If
                obj.Location_Code = txtLocation.Value
                obj.Description = txtDescription.Text
                If chkInactive.Checked Then
                    obj.Inactive = 1
                End If
                obj.Arr_FrieghtDetail = GetTRData(False)
                If (obj.Arr_FrieghtDetail Is Nothing OrElse obj.Arr_FrieghtDetail.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If
                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.PK_ID, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function GetTRData(ByVal isMissingOnly As Boolean) As List(Of clsFrieghtRateDetail)
        Dim Arr As New List(Of clsFrieghtRateDetail)
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colCustCode).Value) > 0 Then
                If clsCommon.myCDecimal(gv1.Rows(ii).Cells(colFrieghtRate).Value) > 0 Then
                    Dim flag As Boolean = True
                    If clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColPKID).Value) > 0 AndAlso isMissingOnly Then
                        flag = False
                    End If
                    If flag Then
                        Dim objTr As New clsFrieghtRateDetail()
                        'objTr.SNo = ii + 1
                        objTr.Customer_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colCustCode).Value)
                        objTr.Zone_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colZoneCode).Value)
                        objTr.UOM_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colUOMCode).Value)
                        objTr.Frieght_Rate = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colFrieghtRate).Value)
                        Arr.Add(objTr)
                    End If
                End If
            End If
        Next
        Return Arr
    End Function
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Please select document no to delete")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Delete the current document" + Environment.NewLine + "Are you sure ? ", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsFrieghtRateMaster.DeleteData(clsCommon.myCDecimal(txtCode.Value))
                clsCommon.MyMessageBoxShow(Me, "Data delete successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If

            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtCode.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsFrieghtRateMaster.PostData(clsCommon.myCDecimal(txtCode.Value))
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(clsCommon.myCDecimal(txtCode.Value), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub chkInactive_CheckStateChanged(sender As Object, e As EventArgs) Handles chkInactive.CheckStateChanged
        Try

            If (Not isInsideLoadData) Then
                If clsCommon.myLen(txtCode.Value) <= 0 Then
                    Throw New Exception("No document found to post")
                End If

                If clsCommon.MyMessageBoxShow(Me, "Inactive the Current Document [" + txtCode.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsFrieghtRateMaster.InactiveCode(clsCommon.myCDecimal(txtCode.Value))
                    clsCommon.MyMessageBoxShow(Me, "Inactive successfully", Me.Text)
                    chkInactive.Enabled = False
                    LoadData(clsCommon.myCDecimal(txtCode.Value), NavigatorType.Current)

                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub gv1_CellValidated(sender As Object, e As CellValidatedEventArgs) Handles gv1.CellValidated
        Try
            SetGridFocus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFocus()
        If gv1.CurrentCell IsNot Nothing Then
            Dim setNxtRow As Boolean = False
            If gv1.CurrentCell.ColumnInfo.Name = colCustCode Then
                gv1.CurrentColumn = gv1.Columns(colZoneCode)
            ElseIf gv1.CurrentCell.ColumnInfo.Name = colZoneCode Then
                gv1.CurrentColumn = gv1.Columns(colUOMCode)
            ElseIf gv1.CurrentCell.ColumnInfo.Name = colUOMCode Then
                gv1.CurrentColumn = gv1.Columns(colFrieghtRate)

            ElseIf gv1.CurrentCell.ColumnInfo.Name = colFrieghtRate Then
                setNxtRow = True

            End If
            If setNxtRow Then
                gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index + 1)
                gv1.CurrentColumn = gv1.Columns(colCustCode)
            End If
        End If
    End Sub

    Private Sub rmiImport_Click(sender As Object, e As EventArgs) Handles rmiImport.Click
        Import()
    End Sub

    Private Sub rmiExport_Click(sender As Object, e As EventArgs) Handles rmiExport.Click
        Export()
    End Sub
    Public Sub Import()
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim obj As New List(Of clsFrieghtRateDetail)
            Dim currentdate As Date = Date.Today
            If clsCommon.myLen(txtLocation.Value) > 0 Then

                If transportSql.importExcel(gv, "Customer Code", "Zone", "UOM", "Frieght Rate") Then

                    'Dim trans As SqlTransaction = Nothing
                    Dim linno As Integer = 0
                    Dim TempNewRecord As Boolean = False
                    Try
                        'trans = clsDBFuncationality.GetTransactin()
                        clsCommon.ProgressBarShow()
                        For Each grow As GridViewRowInfo In gv.Rows
                            Dim Arr As New clsFrieghtRateDetail()
                            linno += 1
                            If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Customer Code").Value))) Then
                                'Throw New Exception("Customer Code Cannot be empty" + clsCommon.myCstr(linno) + ".")
                                Continue For
                            Else
                                Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Customer from TSPL_DCS_FOR_SALE where Customer='" + clsCommon.myCstr(grow.Cells("Customer Code").Value) + "' and Location='" + clsCommon.myCstr(txtLocation.Value) + "'"))
                                Dim custName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + clsCommon.myCstr(grow.Cells("Customer Code").Value) + "'"))
                                If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Customer Code").Value)) = CompairStringResult.Equal Then
                                    Arr.Customer_Code = clsCommon.myCstr(grow.Cells("Customer Code").Value)
                                    Arr.Customer_Name = clsCommon.myCstr(custName)
                                Else
                                    'Throw New Exception("Customer Code Not Exists." + clsCommon.myCstr(linno) + ".")
                                    Continue For
                                End If
                            End If
                            If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Zone").Value))) Then
                                'Throw New Exception("Zone cannot be empty" + clsCommon.myCstr(linno) + ".")
                                Continue For
                            Else
                                Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Zone_Code from TSPL_Zone_Master where Zone_Code='" + clsCommon.myCstr(grow.Cells("Zone").Value) + "'"))
                                If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Zone").Value)) = CompairStringResult.Equal Then
                                    Arr.Zone_Code = clsCommon.myCstr(grow.Cells("Zone").Value)
                                Else
                                    'Throw New Exception("Zone Code Not Exists." + clsCommon.myCstr(linno) + ".")
                                    Continue For
                                End If
                            End If
                            If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("UOM").Value))) Then
                                'Throw New Exception("UOM Code cannot be empty" + clsCommon.myCstr(linno) + ".")
                                Continue For
                            Else
                                Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Unit_Code from TSPL_UNIT_MASTER where Unit_Code='" + clsCommon.myCstr(grow.Cells("UOM").Value) + "'"))
                                If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("UOM").Value)) = CompairStringResult.Equal Then
                                    Arr.UOM_Code = clsCommon.myCstr(grow.Cells("UOM").Value)
                                Else
                                    'Throw New Exception("UOM Code Not Exists." + clsCommon.myCstr(linno) + ".")
                                    Continue For
                                End If
                            End If
                            If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Frieght Rate").Value))) Then
                                'Throw New Exception("Frieght Rate cannot be empty" + clsCommon.myCstr(linno) + ".")
                                Continue For
                            Else
                                Arr.Frieght_Rate = clsCommon.myCDecimal(grow.Cells("Frieght Rate").Value)
                            End If
                            obj.Add(Arr)
                        Next
                        clsCommon.ProgressBarHide()
                        If clsCommon.MyMessageBoxShow(Me, "Total Correct Document [" + clsCommon.myCstr(obj.Count) + "] out of [" + clsCommon.myCstr(linno) + "] Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                            Dim sl As Integer = 1
                            If obj IsNot Nothing AndAlso obj.Count > 0 Then
                                isInsideLoadData = True
                                For Each objTr As clsFrieghtRateDetail In obj
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = sl
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColPKID).Value = objTr.PK_ID
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustCode).Value = objTr.Customer_Code
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustName).Value = objTr.Customer_Name
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colZoneCode).Value = objTr.Zone_Code
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUOMCode).Value = objTr.UOM_Code
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFrieghtRate).Value = objTr.Frieght_Rate
                                    sl += 1
                                    gv1.Rows.AddNew()
                                Next

                                isInsideLoadData = False
                            End If
                            common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                        Else
                            common.clsCommon.MyMessageBoxShow("Data Transfer Failed", Me.Text, MessageBoxButtons.OK)
                        End If

                        clsCommon.ProgressBarHide()

                    Catch ex As Exception
                        clsCommon.ProgressBarHide()
                        clsCommon.MyMessageBoxShow(ex.Message)
                    End Try
                Else
                    clsCommon.MyMessageBoxShow(Me, "Excel Sheet is not in expected format", Me.Text)

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Please Select Location", Me.Text)
            End If
            'clsCommon.ProgressBarHide()
            Me.Controls.Remove(gv)
        Catch ex As Exception
            'clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(ex.Message)

        End Try

    End Sub
    Public Sub Export()
        Try
            Dim str As String = "select Customer_Code as [Customer Code],Zone_Code as [Zone],UOM_Code as [UOM],Frieght_Rate as [Frieght Rate] from TSPL_DCS_FOR_SALE_FRIEGHT_DETAIL left join TSPL_DCS_FOR_SALE_FRIEGHT on TSPL_DCS_FOR_SALE_FRIEGHT.PK_ID=TSPL_DCS_FOR_SALE_FRIEGHT_DETAIL.REF_PK_ID "
            Dim whrCls As String = ""
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls += " and TSPL_DCS_FOR_SALE_FRIEGHT.Location_Code in(" + clsCommon.myCstr(objCommonVar.strCurrUserLocations) + ") "
            End If
            ListImpExpColumnsMandatory = New List(Of String)({"Customer Code", "Zone", "UOM", "Frieght Rate"})
            transportSql.ExporttoExcel(str, whrCls, Me)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
End Class
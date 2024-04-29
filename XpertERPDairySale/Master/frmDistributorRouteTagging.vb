Imports common
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class frmDistributorRouteTagging
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim arrLoc As String = Nothing
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    Dim SeparateDemandMilkandProduct As Boolean = True
    Dim IsInsieLoadData As Boolean
    Const colSNO As String = "SNo"
    Dim Prev As Integer = 0
    Const colCustomerCode As String = "CUSTOMERCODE"
    Const colCustomerName As String = "CUSTOMERNAME"
    Const colRouteNumber As String = "ROUTENUMBER"
    Const colRouteName As String = "ROUTEName"
    Private isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim SettNoOFCustomerForImportExport As Integer
    Dim AllowFinishGoodAsBatchItem As Boolean = False
    Const colCode As String = "colCode"
#End Region
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmBookingProductSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnpost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        'btnImport.Visible = MyBase.isExport
        If MyBase.isExport = True Then
            rmiImport.Enabled = True
            rmiExport.Enabled = True
        Else
            rmiImport.Enabled = False
            rmiExport.Enabled = False
        End If
    End Sub
    Private Sub frmDistributeRateTagging_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        SeparateDemandMilkandProduct = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SeparateDemandMilkandProduct, clsFixedParameterCode.SeparateDemandMilkandProduct, Nothing)) = 1, True, False)
        AddNew()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtStartDate.Value = clsCommon.GETSERVERDATE()
    End Sub
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Public Sub AddNew()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtStartDate.Value = clsCommon.GETSERVERDATE()
        txtEndDate.Value = txtStartDate.Value
        txtCode.Focus()
        txtRemark.Text = ""
        rbtnDistributor.IsChecked = True
        rbtnTPT.IsChecked = False
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        isNewEntry = True
        cmbItemType.Text = "Both"
        If Not SeparateDemandMilkandProduct Then
            lblItemType.Visible = False
            cmbItemType.Visible = False
        End If
        LoadBlankGrid()
    End Sub
    Sub RefeshSNO()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colSNO).Value = ii
        Next
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            AddNew()
            gv1.DataSource = Nothing
            gv1.Refresh()
            isInsideLoadData = True
            txtCode.MyReadOnly = True
            Dim obj As New clsDistributorRouteTagging()
            obj = clsDistributorRouteTagging.GetData(strCode, NavType, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                isNewEntry = False
                txtCode.Value = obj.Code
                txtStartDate.Value = obj.Start_Date
                cmbItemType.Text = obj.ItemType
                If clsCommon.myLen(obj.End_Date) > 0 Then
                    txtEndDate.Value = obj.End_Date
                Else
                    txtEndDate.Value = Nothing
                End If
                txtRemark.Text = obj.Remarks
                If obj.IS_Transpoter Then
                    rbtnTPT.IsChecked = True
                Else
                    rbtnDistributor.IsChecked = True
                End If
                Dim sno As Integer = 1
                If obj.Arr IsNot Nothing Then
                    For Each objrow As clsDistributorRouteTaggingDetail In obj.Arr
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNO).Value = sno
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRouteNumber).Value = objrow.Route_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRouteName).Value = objrow.Route_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomerCode).Value = objrow.Cust_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomerName).Value = objrow.Customer_Name
                        gv1.Rows.AddNew()
                        sno += 1
                    Next
                End If
                If clsCommon.myCdbl(ERPTransactionStatus.Approved) = clsCommon.myCdbl(obj.Status) Then
                    UsLock1.Status = obj.Status
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnpost.Enabled = False
                ElseIf ERPTransactionStatus.Pending = obj.Status Then
                    UsLock1.Status = obj.Status
                    btnsave.Enabled = True
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                    btnpost.Enabled = True
                End If
            End If
            isInsideLoadData = False
        Catch ex As Exception
            isInsideLoadData = False
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub LoadBlankGrid()
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = colSNO
        repoLineNo.Width = 50
        repoLineNo.IsVisible = True
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)
        Dim repoRouteNumber As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRouteNumber.FormatString = ""
        repoRouteNumber.HeaderText = "Route Code"
        repoRouteNumber.Name = colRouteNumber
        repoRouteNumber.HeaderImage = My.Resources.search4
        repoRouteNumber.TextImageRelation = TextImageRelation.TextBeforeImage
        repoRouteNumber.Width = 100
        repoRouteNumber.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoRouteNumber)
        Dim repoRouteName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRouteName.FormatString = ""
        repoRouteName.HeaderText = "Route Name"
        repoRouteName.Name = colRouteName
        repoRouteName.Width = 150
        repoRouteName.IsVisible = True
        repoRouteName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoRouteName)
        Dim repoCustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustCode.FormatString = ""
        If rbtnDistributor.IsChecked Then
            repoCustCode.HeaderText = "Distributer Code"
        Else
            repoCustCode.HeaderText = "Transpoter Code"
        End If
        repoCustCode.Name = colCustomerCode
        repoCustCode.HeaderImage = My.Resources.search4
        repoCustCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCustCode.Width = 100
        repoCustCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoCustCode)
        Dim repoCustName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustName.FormatString = ""
        If rbtnDistributor.IsChecked Then
            repoCustName.HeaderText = "Distributer Name"
        Else
            repoCustName.HeaderText = "Transpoter Name"
        End If
        repoCustName.Name = colCustomerName
        repoCustName.Width = 150
        repoCustName.IsVisible = True
        repoCustName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCustName)
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = True
        gv1.AllowRowReorder = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = False
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        gv1.AllowColumnChooser = True
        gv1.AllowColumnReorder = True
        gv1.Rows.AddNew()
    End Sub
    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qst As String = "select count(*) from TSPL_DISTRIBUTOR_ROUTE where Code='" + txtCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
        If count = 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim whrClas As String = ""
            Dim qry As String = "select Code,Start_Date As 'Start Date',End_Date As 'End Date',Remarks,Status from TSPL_DISTRIBUTOR_ROUTE"
            txtCode.Value = clsCommon.ShowSelectForm("DRT", qry, "Code", "", txtCode.Value, "TSPL_DISTRIBUTOR_ROUTE.Code ", isButtonClicked, "")
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub
    Public Function AllowToSave() As Boolean
        Try
            Dim obj As New List(Of clsDistributorRouteTaggingDetail)
            If gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Atleast Fill One Row", Me.Text)
                Return False
            End If
            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objTr As New clsDistributorRouteTaggingDetail()
                objTr.Route_No = clsCommon.myCstr(grow.Cells(colRouteNumber).Value)
                If clsCommon.myLen(objTr.Route_No) > 0 Then
                    obj.Add(objTr)
                End If
            Next
            Dim duplicatesRoute As New List(Of clsDistributorRouteTaggingDetail)
            duplicatesRoute = obj.GroupBy(Function(x) x.Route_No).Where(Function(group) group.Count() > 1).SelectMany(Function(group) group).ToList
            Dim strDRoute As String = String.Empty
            For Each duplicate As clsDistributorRouteTaggingDetail In duplicatesRoute
                strDRoute += "[" + duplicate.Route_No + "] "
            Next
            If clsCommon.myLen(strDRoute) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Duplicate Route Found - " + strDRoute + " ", Me.Text)
                Return False
            End If
        Catch ex As Exception
        End Try
        Return True
    End Function
    Private Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsDistributorRouteTagging()
                obj.Code = txtCode.Value
                obj.Start_Date = txtStartDate.Value
                If txtEndDate.Checked Then
                    obj.End_Date = txtEndDate.Value
                End If
                obj.Remarks = txtRemark.Text
                obj.ItemType = cmbItemType.Text
                If rbtnDistributor.IsChecked Then
                    obj.IS_Transpoter = False
                Else
                    obj.IS_Transpoter = True
                End If
                obj.Arr = New List(Of clsDistributorRouteTaggingDetail)
                For Each row As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsDistributorRouteTaggingDetail()
                    objTr.Route_No = clsCommon.myCstr(row.Cells(colRouteNumber).Value)
                    objTr.Cust_Code = clsCommon.myCstr(row.Cells(colCustomerCode).Value)
                    If (clsCommon.myLen(objTr.Route_No) > 0) AndAlso (clsCommon.myLen(objTr.Cust_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data save successfully.", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colCustomerCode) Then
                        Dim strCustCode As String = String.Empty
                        If rbtnDistributor.IsChecked Then
                            strCustCode = clsDistributorRouteTagging.getFinder(" IsDistributor='Y' ", clsCommon.myCstr(gv1.CurrentRow.Cells(colCustomerCode).Value), False)
                        Else
                            strCustCode = clsDistributorRouteTagging.getFinder(" form_type='TPT'", clsCommon.myCstr(gv1.CurrentRow.Cells(colCustomerCode).Value), False)
                        End If
                        gv1.CurrentRow.Cells(colCustomerCode).Value = strCustCode
                        gv1.CurrentRow.Cells(colCustomerName).Value = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustCode & "' ")
                    ElseIf e.Column Is gv1.Columns(colRouteNumber) Then
                        Dim strRouteCode As String = clsDistributorRouteTagging.getRouteFinder("", clsCommon.myCstr(gv1.CurrentRow.Cells(colRouteNumber).Value), False)
                        gv1.CurrentRow.Cells(colRouteNumber).Value = strRouteCode
                        gv1.CurrentRow.Cells(colRouteName).Value = clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" & strRouteCode & "' ")
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.Rows.Count > 0 Then
            If gv1.CurrentRow.Index = gv1.Rows.Count - 1 Then
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSNO).Value = gv1.Rows.Count
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 2)
            End If
        End If
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub gv_UserAddedRow(sender As Object, e As GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colSNO).Value = i + 1
            End If
        Next
    End Sub
    Private Sub gv_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gv1.UserDeletedRow
        Try
            RefeshSNO()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (clsDistributorRouteTagging.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        AddNew()
    End Sub
    Private Sub btnpost_Click(sender As Object, e As EventArgs) Handles btnpost.Click
        If clsCommon.myLen(txtCode.Value) > 0 Then
            PostData(txtCode.Value)
        Else
        End If
    End Sub
    Sub PostData(ByVal strCode As String)
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtCode.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsDistributorRouteTagging.PostData(clsCommon.myCstr(txtCode.Value))
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(clsCommon.myCstr(txtCode.Value), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtCode_MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_DISTRIBUTOR_ROUTE where Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(clsCommon.myCstr(txtCode.Value), NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            AddNew()
            Dim qry As String = "select TSPL_DISTRIBUTOR_ROUTE.Code as Code,case when TSPL_DISTRIBUTOR_ROUTE.Status=1 then 'posted' else 'Unposted' end as Posted from TSPL_DISTRIBUTOR_ROUTE"
            Dim strCode As String = clsCommon.ShowSelectForm("DistributeNoFndd1", qry, "Code", "", "Code")
            If clsCommon.myLen(strCode) > 0 Then
                LoadData(strCode, NavigatorType.Current)
                txtCode.Value = Nothing
            End If
            btnsave.Enabled = True
            btnpost.Enabled = True
            btndelete.Enabled = True
            isNewEntry = True
            UsLock1.Status = ERPTransactionStatus.Pending
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
            Dim obj As New List(Of clsDistributorRouteTaggingDetail)
            Dim currentdate As Date = Date.Today
            If transportSql.importExcel(gv, "Route Code", "Distributor Code", "IS Transpoter") Then
                'Dim trans As SqlTransaction = Nothing
                Dim linno As Integer = 0
                Dim TempNewRecord As Boolean = False
                Try
                    'trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        Dim Arr As New clsDistributorRouteTaggingDetail()
                        linno += 1
                        If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Route Code").Value))) Then
                            Continue For
                        Else
                            Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Route_No from TSPL_ROUTE_MASTER where Route_No='" + clsCommon.myCstr(grow.Cells("Route Code").Value) + "'"))
                            If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Route Code").Value)) = CompairStringResult.Equal Then
                                Arr.Route_No = clsCommon.myCstr(grow.Cells("Route Code").Value)
                            Else
                                Continue For
                            End If
                        End If
                        If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Distributor Code").Value)) AndAlso String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("IS Transpoter").Value))) Then
                            Continue For
                        Else
                            Dim str As String = String.Empty
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("IS Transpoter").Value), "Y") = CompairStringResult.Equal Then
                                str = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select cust_Code from TSPL_Customer_Master where cust_Code='" + clsCommon.myCstr(grow.Cells("Distributor Code").Value) + "' and Form_Type='TPT'"))
                            Else
                                str = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select cust_Code from TSPL_Customer_Master where cust_Code='" + clsCommon.myCstr(grow.Cells("Distributor Code").Value) + "' and IsDistributor='Y'"))
                            End If
                            If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Distributor Code").Value)) = CompairStringResult.Equal Then
                                Arr.Cust_Code = clsCommon.myCstr(grow.Cells("Distributor Code").Value)
                            Else
                                Continue For
                            End If
                        End If
                        obj.Add(Arr)
                    Next
                    Dim duplicatesRoute As New List(Of clsDistributorRouteTaggingDetail)
                    duplicatesRoute = obj.GroupBy(Function(x) x.Route_No).Where(Function(group) group.Count() > 1).SelectMany(Function(group) group).ToList
                    Dim strDRoute As String = String.Empty
                    For Each duplicate As clsDistributorRouteTaggingDetail In duplicatesRoute
                        strDRoute += "[" + duplicate.Route_No + "] "
                    Next
                    clsCommon.ProgressBarHide()
                    If clsCommon.myLen(strDRoute) > 0 Then
                        Throw New Exception("Duplicate Route Found -" + strDRoute)
                    Else
                        If clsCommon.MyMessageBoxShow(Me, "Total Correct Document [" + clsCommon.myCstr(obj.Count) + "] out of [" + clsCommon.myCstr(linno) + "] Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                            Dim sl As Integer = 1
                            LoadBlankGrid()
                            If obj IsNot Nothing AndAlso obj.Count > 0 Then
                                isInsideLoadData = True
                                For Each objTr As clsDistributorRouteTaggingDetail In obj
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSNO).Value = sl
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRouteNumber).Value = objTr.Route_No
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRouteName).Value = clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" + objTr.Route_No + "'")
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomerCode).Value = objTr.Cust_Code
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomerName).Value = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_Customer_Master where cust_code='" + objTr.Cust_Code + "'")
                                    sl += 1
                                    gv1.Rows.AddNew()
                                Next
                                isInsideLoadData = False
                            End If
                            common.clsCommon.MyMessageBoxShow("Data Load Completed!", Me.Text, MessageBoxButtons.OK)
                        Else
                            common.clsCommon.MyMessageBoxShow("Data Load Failed", Me.Text, MessageBoxButtons.OK)
                        End If
                    End If
                    clsCommon.ProgressBarHide()
                Catch ex As Exception
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            Else
                clsCommon.MyMessageBoxShow(Me, "Excel Sheet is not in expected format", Me.Text)
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub Export()
        Try
            Dim str As String = "select Route_No as [Route Code],Cust_Code as [Distributor Code],'' as [IS Transpoter] from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER"
            Dim whrCls As String = ""
            ListImpExpColumnsMandatory = New List(Of String)({"Route Code", "Distributor Code", "IS Transpoter"})
            transportSql.ExporttoExcel(str, whrCls, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtEndDate_CheckedChanged(sender As Object, e As EventArgs) Handles txtEndDate.CheckedChanged
        If txtEndDate.Checked Then
            txtEndDate.Value = clsCommon.GETSERVERDATE()
        Else
            txtEndDate.Value = Nothing
        End If
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim sqlqry As String = "   select ROW_NUMBER() Over(ORDER BY(Select 1)ASC)AS[S.NO.],'" + objCommonVar.CurrentUser + "' AS [User], TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Code,TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code,TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No,Convert(varchar(12),TSPL_DISTRIBUTOR_ROUTE.Start_Date,103) as Start_Date,
   TSPL_ROUTE_MASTER.Route_Desc,tspl_Company_master.Add1,tspl_Company_master.Add2,tspl_Company_master.Add3,tspl_Company_master.City_Code,
   tspl_Customer_Master.Customer_Name,tspl_Company_master.Comp_Name,tspl_Company_master.Pincode
FROM TSPL_DISTRIBUTOR_ROUTE_CUSTOMER
   Left Join TSPL_DISTRIBUTOR_ROUTE on TSPL_DISTRIBUTOR_ROUTE.Code=TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Code
   Left Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No
      Left Join tspl_Customer_Master on tspl_Customer_Master.Cust_Code=TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code
  left join tspl_Company_master on tspl_Company_master.Comp_Code=.TSPL_ROUTE_MASTER.Comp_Code
   WHERE TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Code ='" & txtCode.Value & "'"
            If rbtnTPT.IsChecked Then
                sqlqry += " and IS_Transpoter=1"
            End If
            Dim dtItem As DataTable
            dtItem = clsDBFuncationality.GetDataTable(sqlqry)
            If dtItem.Rows.Count > 0 Then
                Dim crysFrm As New frmCrystalReportViewer()
                crysFrm.funreport(CrystalReportFolder.PurchaseOrder, dtItem, "DistributorRouteTagging", "Distribute Route Tagging")
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rbtnDistributor_CheckStateChanged(sender As Object, e As EventArgs) Handles rbtnDistributor.CheckStateChanged
        If rbtnDistributor.IsChecked Then
            rbtnTPT.IsChecked = False
            If SeparateDemandMilkandProduct Then
                lblItemType.Visible = True
                cmbItemType.Visible = True
            End If
            LoadBlankGrid()
        End If
    End Sub
    Private Sub rbtnTPT_CheckStateChanged(sender As Object, e As EventArgs) Handles rbtnTPT.CheckStateChanged
        If rbtnTPT.IsChecked Then
            rbtnDistributor.IsChecked = False
            If SeparateDemandMilkandProduct Then
                lblItemType.Visible = False
                cmbItemType.Visible = False
            End If
            LoadBlankGrid()
        End If
    End Sub
End Class

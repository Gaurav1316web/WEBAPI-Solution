Imports common
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class frmDistributeRateTagging
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim arrLoc As String = Nothing
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
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
    Private Sub AddNewItem()
        txtCode.MyReadOnly = False
        txtStartDate.Value = clsCommon.GETSERVERDATE()
        txtEndDate.Value = clsCommon.GETSERVERDATE()
        txtRemark.Text = ""

        isNewEntry = True
    End Sub
    Private Sub frmDistributeRateTagging_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Code", "Varchar(30) not null PRIMARY KEY")
        coll.Add("Start_Date", "Date NOT NULL")
        coll.Add("End_Date", "Date NULL")
        coll.Add("Remarks", "Varchar(100) null")
        coll.Add("Status", "integer NULL")
        coll.Add("Created_By", "varchar(12) NOT NULL")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        coll.Add("Post_By", "varchar(12)  NULL")
        coll.Add("Post_Date", "Datetime  NULL")
        clsCommonFunctionality.CreateOrAlterTable("TSPL_DISTRIBUTOR_ROUTE", coll)
        coll = New Dictionary(Of String, String)()
        coll.Add("Code", "VARCHAR(30) not null REFERENCES TSPL_DISTRIBUTOR_ROUTE(Code)")
        coll.Add("Route_No", "VARCHAR(12) not null REFERENCES TSPL_ROUTE_MASTER(Route_No)")
        coll.Add("Cust_Code", "VARCHAR(12) not null REFERENCES TSPL_CUSTOMER_MASTER(Cust_Code)")
        clsCommonFunctionality.CreateOrAlterTable("TSPL_DISTRIBUTOR_ROUTE_CUSTOMER", coll)
        LoadBlankgv_Grid()
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Sub Reset()
        txtCode.Value = ""
        'txtStartDate.Value = ""
        'txtEndDate.Value = ""
        IsInsieLoadData = False
        gv.Rows.Clear()
        gv.Columns.Clear()
        btnsave.Text = "Save"
        btndelete.Enabled = False
        isNewEntry = True
        UsLock1.Status = ERPTransactionStatus.Pending
        LoadBlankgv_Grid()
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            LoadBlankgv_Grid()
            gv.DataSource = Nothing
            gv.Refresh()
            isInsideLoadData = True
            AddNewItem()
            txtCode.MyReadOnly = True
            Dim obj As New clsDistributeRateTagging()
            obj = clsDistributeRateTagging.GetData(strCode, NavType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                isNewEntry = False
                txtCode.Value = obj.Code
                txtStartDate.Value = obj.Start_Date
                If clsCommon.myLen(obj.End_Date) > 0 Then
                    txtEndDate.Value = obj.End_Date
                Else
                    txtEndDate.Value = Nothing
                End If

                txtRemark.Text = obj.Remarks
                If obj.arr IsNot Nothing Then
                    For Each objrow As clsDistributeRateTaggingDetail In obj.arr
                        gv.Rows(gv.Rows.Count - 1).Cells(colSNO).Value = objrow.SNo
                        gv.Rows(gv.Rows.Count - 1).Cells(colRouteNumber).Value = objrow.Route_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colRouteName).Value = objrow.Route_Desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colCustomerCode).Value = objrow.Cust_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colCustomerName).Value = objrow.Customer_Name
                        gv.Rows.AddNew()
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
    Sub LoadBlankgv_Grid()

        Dim qry As String = String.Empty
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "S No"
        repoLineNo.Name = colSNO
        repoLineNo.Width = 50
        repoLineNo.IsVisible = True
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoRouteNumber As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRouteNumber.FormatString = ""
        repoRouteNumber.HeaderText = "Route Code"
        repoRouteNumber.Name = colRouteNumber
        repoRouteNumber.HeaderImage = My.Resources.search4
        repoRouteNumber.TextImageRelation = TextImageRelation.TextBeforeImage
        repoRouteNumber.Width = 100
        repoRouteNumber.IsVisible = True
        gv.MasterTemplate.Columns.Add(repoRouteNumber)

        Dim repoRouteName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRouteName.FormatString = ""
        repoRouteName.HeaderText = "Route Name"
        repoRouteName.Name = colRouteName
        repoRouteName.Width = 150
        repoRouteName.IsVisible = True
        repoRouteName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoRouteName)

        Dim repoCustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustCode.FormatString = ""
        repoCustCode.HeaderText = "Distributer Code"
        repoCustCode.Name = colCustomerCode
        repoCustCode.HeaderImage = My.Resources.search4
        repoCustCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCustCode.Width = 100
        repoCustCode.IsVisible = True
        gv.MasterTemplate.Columns.Add(repoCustCode)

        Dim repoCustName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustName.FormatString = ""
        repoCustName.HeaderText = "Distributer Name"
        repoCustName.Name = colCustomerName
        repoCustName.Width = 150
        repoCustName.IsVisible = True
        repoCustName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoCustName)

        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = True
        gv.AllowRowReorder = False
        gv.ShowGroupPanel = False
        gv.EnableFiltering = False
        gv.EnableSorting = False
        gv.EnableGrouping = False
        gv.AllowColumnChooser = True
        gv.AllowColumnReorder = True
        gv.Rows.AddNew()
    End Sub


    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qst As String = "select count(*) from TSPL_DISTRIBUTOR_ROUTE where Code='" + txtCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
        If count = 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        LoadBlankgv_Grid()
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim whrClas As String = ""
            Dim qry As String = "select Code,Start_Date As 'Start Date',End_Date As 'End Date',Remarks,Status from TSPL_DISTRIBUTOR_ROUTE"
            txtCode.Value = clsCommon.ShowSelectForm("DRT", qry, "Code", "", txtCode.Value, "TSPL_DISTRIBUTOR_ROUTE.Code ", isButtonClicked, "")
            LoadData(txtCode.Value, NavigatorType.Current)
        End If

    End Sub

    Private Function SaveData() As Boolean
        Try
            Dim obj As New clsDistributeRateTagging()
            obj.Code = txtCode.Value
            obj.Start_Date = txtStartDate.Value
            If txtEndDate.Checked Then
                obj.End_Date = txtEndDate.Value
            End If
            obj.Remarks = txtRemark.Text
            obj.arr = New List(Of clsDistributeRateTaggingDetail)
            For Each row As GridViewRowInfo In gv.Rows
                Dim objTr As New clsDistributeRateTaggingDetail()
                objTr.Code = obj.Code
                objTr.Route_No = clsCommon.myCstr(row.Cells(colRouteNumber).Value)
                objTr.Cust_Code = clsCommon.myCstr(row.Cells(colCustomerCode).Value)
                If (clsCommon.myLen(objTr.Route_No) > 0) AndAlso (clsCommon.myLen(objTr.Cust_Code) > 0) Then
                    obj.arr.Add(objTr)
                End If
            Next

            If obj.arr.Count <= 0 Then
                Throw New Exception("Atleast Fill One Row")
                Return False
            End If

            Dim trans As SqlTransaction = Nothing
            Dim Sqlqry As String = "select count(1) from TSPL_DISTRIBUTOR_ROUTE
                                    where TSPL_DISTRIBUTOR_ROUTE.Code ='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sqlqry))
            If count = 0 Then
                isNewEntry = True
                obj.Code = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Start_Date), clsDocType.DistributeCode, clsDocTransactionType.DistributRateTag, "")
                txtCode.Value = obj.Code
            Else
                isNewEntry = False
            End If
            If (clsDistributeRateTagging.SaveData(obj, isNewEntry)) Then
                clsCommon.MyMessageBoxShow(Me, "Data save successfully.")
                LoadData(txtCode.Value, Nothing)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
        Return True
    End Function

    Private Sub gv_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv.Columns(colCustomerCode) Then
                        Dim strCustCode As String = clsDistributeRateTagging.getFinder("", clsCommon.myCstr(gv.CurrentRow.Cells(colCustomerCode).Value), False)
                        gv.CurrentRow.Cells(colCustomerCode).Value = strCustCode
                        gv.CurrentRow.Cells(colCustomerName).Value = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustCode & "' ")
                    ElseIf e.Column Is gv.Columns(colRouteNumber) Then
                        Dim strRouteCode As String = clsDistributeRateTagging.getRouteFinder("", clsCommon.myCstr(gv.CurrentRow.Cells(colRouteNumber).Value), False)
                        gv.CurrentRow.Cells(colRouteNumber).Value = strRouteCode
                        gv.CurrentRow.Cells(colRouteName).Value = clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" & strRouteCode & "' ")

                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub gv_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.Rows.Count > 0 Then
            If gv.CurrentRow.Index = gv.Rows.Count - 1 Then
                gv.Rows(gv.Rows.Count - 1).Cells(colSNO).Value = gv.Rows.Count
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(gv.Rows.Count - 2)

            End If
        End If

    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub gv_UserAddedRow(sender As Object, e As GridViewRowEventArgs) Handles gv.UserAddedRow
        For i As Integer = 0 To gv.Rows.Count - 1
            gv.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv.Rows(i).Cells(colSNO).Value = i + 1
            End If
        Next
    End Sub

    Private Sub gv_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gv.UserDeletedRow
        For i As Integer = 1 To gv.Rows.Count
            gv.Rows(i - 1).Cells(colSNO).Value = i
        Next
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (clsDistributeRateTagging.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNewItem()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        funReset()
    End Sub
    Sub funReset()
        LoadBlankgv_Grid()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtStartDate.Value = clsCommon.GETSERVERDATE()
        txtEndDate.Value = clsCommon.GETSERVERDATE()
        txtCode.Focus()
        txtRemark.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
    End Sub

    Private Sub btnpost_Click(sender As Object, e As EventArgs) Handles btnpost.Click
        If clsCommon.myLen(txtCode.Value) > 0 Then
            PostData(txtCode.Value)
        Else

        End If
    End Sub
    Sub PostData(ByVal strCode As String)
        Try
            Dim obj As New clsDistributeRateTagging()
            obj.Code = strCode
            obj.Post_By = clsCommon.myCstr(objCommonVar.CurrentUserCode)
            If (myMessages.postConfirm()) Then
                If clsCommon.myLen(txtCode.Value) > 0 Then
                    clsDistributeRateTagging.PostData(obj)
                    Dim msg = "Successfully Posted"
                    clsCommon.MyMessageBoxShow("Data Post Successfully", Me.Text)
                    LoadData(strCode, NavigatorType.Current)
                Else
                    Throw New Exception("No Data found to Post")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode_MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator


        txtCode.Value = clsDistributeRateTagging.Code_Navigation(NavType, txtCode.Value)
        LoadData(txtCode.Value, NavigatorType.Current)
    End Sub

    Function AllowToSave() As Boolean
        Try

            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                Throw New Exception("Please Select Code Detail")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function
    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            Dim qry As String = "select TSPL_DISTRIBUTOR_ROUTE.Code as Code,case when TSPL_DISTRIBUTOR_ROUTE.Status=1 then 'posted' else 'Unposted' end as Posted from TSPL_DISTRIBUTOR_ROUTE"
            Dim strCode As String = clsCommon.ShowSelectForm("DistributeNoFndd1", qry, "Code", "", "Code")
            If clsCommon.myLen(strCode) > 0 Then
                LoadData(strCode, NavigatorType.Current)
                txtCode.Value = strCode
                txtCode.MyReadOnly = False
                isNewEntry = True
                btnsave.Text = "Save"
                'lblTenderSeqNo.Text = ""
                btnsave.Enabled = True
                btndelete.Enabled = False
                btnpost.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class



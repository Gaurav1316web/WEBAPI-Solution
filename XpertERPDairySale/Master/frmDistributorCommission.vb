Imports common

Public Class frmDistributorCommission
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isNewEntry As Boolean = False
    Const ColSNo As String = "ColSNo"
    Const ColRouteCode As String = "ColRouteCode"
    Const colCustCode As String = "colCustCode"
    Const colCustName As String = "colCustName"
    Const colCRate As String = "colCRate"
    Private isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
#End Region
    Private Sub frmDistributorCommission_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtApplicableDate.Value = clsCommon.GETSERVERDATE()
        CommissionTab()
        AddNew()
    End Sub
    Sub LoadBlankGrid()
        GV1.Rows.Clear()
        GV1.Columns.Clear()
        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = ColSNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        GV1.MasterTemplate.Columns.Add(repoNumBox)
        Dim repoRouteCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRouteCode.FormatString = ""
        repoRouteCode.HeaderText = "Route Code"
        repoRouteCode.Name = ColRouteCode
        repoRouteCode.IsVisible = False
        repoRouteCode.ReadOnly = True
        repoRouteCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        GV1.MasterTemplate.Columns.Add(repoRouteCode)
        Dim repoCustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustCode.FormatString = ""
        repoCustCode.HeaderText = "Distributor Code"
        repoCustCode.Name = colCustCode
        repoCustCode.Width = 150
        repoCustCode.IsVisible = True
        repoCustCode.ReadOnly = True
        repoCustCode.TextImageRelation = TextImageRelation.TextBeforeImage
        'repoCustCode.ReadOnly = True
        GV1.MasterTemplate.Columns.Add(repoCustCode)
        Dim repoCustName = New GridViewTextBoxColumn()
        repoCustName.FormatString = ""
        repoCustName.HeaderText = "Distributor Name"
        repoCustName.Name = colCustName
        repoCustName.Width = 200
        repoCustName.IsVisible = True
        repoCustName.ReadOnly = True
        GV1.MasterTemplate.Columns.Add(repoCustName)

        Dim repoTextBox = New GridViewDecimalColumn()
        repoTextBox.FormatString = "{0:n4}"
        repoTextBox.HeaderText = "Rate Per UOM"
        repoTextBox.Name = colCRate
        repoTextBox.Width = 80
        repoTextBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoTextBox.DecimalPlaces = 4
        repoTextBox.IsVisible = True
        'repoTextBox.ReadOnly = True
        GV1.MasterTemplate.Columns.Add(repoTextBox)
        GV1.Enabled = True
        GV1.AllowAddNewRow = False
        GV1.ShowGroupPanel = False
        GV1.AllowColumnReorder = False
        GV1.AllowRowReorder = False
        GV1.EnableSorting = False
        GV1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GV1.MasterTemplate.ShowRowHeaderColumn = False
        GV1.TableElement.TableHeaderHeight = 40
        GV1.AllowDeleteRow = True
        GV1.Rows.AddNew()
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()

    End Sub
    Public Sub AddNew()
        isNewEntry = True
        lblStatus.Status = ERPTransactionStatus.Pending
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtApplicableDate.Value = clsCommon.GETSERVERDATE()
        txtItems.arrValueMember = Nothing
        txtItems.arrDispalyMember = Nothing
        txtUOM.Value = ""
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnGo.Enabled = True
        LoadBlankGrid()

    End Sub

    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Try
            Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
            txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster@CustomerWiseSalesReport", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtItems__My_Click(sender As Object, e As EventArgs) Handles txtItems._My_Click
        Try
            Dim qry As String = " select Item_code as Code,Item_Desc,Short_Description  from TSPL_ITEM_MASTER where Item_Type='F' "

            txtItems.arrValueMember = clsCommon.ShowMultipleSelectForm("CustomerCode@CustWiseSaleRpt", qry, "Code", "Code", txtItems.arrValueMember, txtItems.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
    Public Sub CommissionTab()
        Try


            Dim coll As Dictionary(Of String, String)
            coll = New Dictionary(Of String, String)()
            coll.Add("Doc_No", "Varchar(30) Not null Primary key")
            coll.Add("Document_Date", "datetime Not null")
            coll.Add("Applicable_Date", "datetime null")
            coll.Add("Commision_UOM", "varchar(12) null references TSPL_UNIT_MASTER(Unit_Code)")
            coll.Add("IsPosted", "integer NOT NULL DEFAULT 0")
            coll.Add("Created_By", "varchar(12)  Not NULL")
            coll.Add("Created_Date", "datetime  Not NULL")
            coll.Add("Modified_By", "varchar(12)  Not NULL")
            coll.Add("Modified_Date", "datetime  Not NULL")

            coll.Add("Posted_By", "varchar(12) NULL")
            coll.Add("Posted_Date", "datetime NULL")
            clsCommonFunctionality.CreateOrAlterTable(False, "TSPL_Distributor_Commission_Head", coll, "", True)

            Dim coll1 As Dictionary(Of String, String)
            coll1 = New Dictionary(Of String, String)()
            coll1.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
            coll1.Add("Doc_No", "Varchar(30) Not null REFERENCES TSPL_Distributor_Commission_Head(Doc_No)")
            coll1.Add("Route_Code", "Varchar(12) Not null  REFERENCES TSPL_ROUTE_MASTER(Route_No)")
            coll1.Add("Distributor_Code", "Varchar(12) Not null references TSPL_Customer_MASTER(Cust_Code)")
            coll1.Add("Rate", "Decimal(18,2) Not null")

            clsCommonFunctionality.CreateOrAlterTable(False, "TSPL_Distributor_Commission_Detail", coll1, "", True)

            Dim coll2 As Dictionary(Of String, String)

            coll2 = New Dictionary(Of String, String)()
            coll2.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
            coll2.Add("Doc_No", "Varchar(30) Not null REFERENCES TSPL_Distributor_Commission_Head(Doc_No)")
            coll2.Add("Item_Code", "Varchar(50) Not null  REFERENCES TSPL_ITEM_MASTER(Item_Code)")
            clsCommonFunctionality.CreateOrAlterTable(False, "TSPL_Distributor_Commission_Items", coll2, "", True)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim StrQry As String = "select TSPL_CUSTOMER_MASTER.Route_No,TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name 
from TSPL_DISTRIBUTOR_ROUTE
left join TSPL_DISTRIBUTOR_ROUTE_CUSTOMER on TSPL_DISTRIBUTOR_ROUTE.Code=TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Code
left join TSPL_CUSTOMER_MASTER on TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
where TSPL_DISTRIBUTOR_ROUTE.start_Date in (
select max(TSPL_DISTRIBUTOR_ROUTE.Start_Date) from TSPL_DISTRIBUTOR_ROUTE where TSPL_DISTRIBUTOR_ROUTE.Start_Date<='" + clsCommon.GetPrintDate(txtDate.Value) + "' and status=1 ) "
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(StrQry)
            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                Dim i As Integer = 1
                For Each dr As DataRow In dt1.Rows
                    GV1.Rows(GV1.Rows.Count - 1).Cells(ColSNo).Value = i
                    GV1.Rows(GV1.Rows.Count - 1).Cells(ColRouteCode).Value = clsCommon.myCstr(dr("Route_No"))
                    GV1.Rows(GV1.Rows.Count - 1).Cells(colCustCode).Value = clsCommon.myCstr(dr("cust_code"))
                    GV1.Rows(GV1.Rows.Count - 1).Cells(colCustName).Value = clsCommon.myCstr(dr("Customer_name"))
                    i = i + 1
                    GV1.Rows.AddNew()
                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "Not Found!", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

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
            Dim obj As New List(Of clsDistributorCommissionDetails)
            Dim currentdate As Date = Date.Today
            If clsCommon.myLen(txtUOM.Value) > 0 Then

                If transportSql.importExcel(gv, "Route Code", "Distributor Code", "Rate") Then

                    'Dim trans As SqlTransaction = Nothing
                    Dim linno As Integer = 0
                    Dim TempNewRecord As Boolean = False
                    Try
                        'trans = clsDBFuncationality.GetTransactin()
                        clsCommon.ProgressBarShow()
                        For Each grow As GridViewRowInfo In gv.Rows
                            Dim Arr As New clsDistributorCommissionDetails()
                            linno += 1
                            If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Route Code").Value))) Then
                                Continue For
                            Else
                                Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Route_No from TSPL_ROUTE_MASTER where Route_No='" + clsCommon.myCstr(grow.Cells("Route Code").Value) + "'"))
                                If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Route Code").Value)) = CompairStringResult.Equal Then
                                    Arr.Route_Code = clsCommon.myCstr(grow.Cells("Route Code").Value)
                                Else
                                    Continue For
                                End If
                            End If
                            If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Distributor Code").Value))) Then
                                Continue For
                            Else
                                Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select cust_Code from TSPL_Customer_Master where cust_Code='" + clsCommon.myCstr(grow.Cells("Distributor Code").Value) + "' and IsDistributor='Y'"))
                                If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Distributor Code").Value)) = CompairStringResult.Equal Then
                                    Arr.Distributor_Code = clsCommon.myCstr(grow.Cells("Distributor Code").Value)
                                Else
                                    Continue For
                                End If
                            End If

                            If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Rate").Value))) Then
                                Continue For
                            Else
                                Arr.Rate = clsCommon.myCDecimal(grow.Cells("Rate").Value)
                            End If
                            obj.Add(Arr)
                        Next
                        clsCommon.ProgressBarHide()
                        If clsCommon.MyMessageBoxShow(Me, "Total Correct Document [" + clsCommon.myCstr(obj.Count) + "] out of [" + clsCommon.myCstr(linno) + "] Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                            Dim sl As Integer = 1
                            AddNew()
                            If obj IsNot Nothing AndAlso obj.Count > 0 Then
                                isInsideLoadData = True
                                For Each objTr As clsDistributorCommissionDetails In obj
                                    GV1.Rows(GV1.Rows.Count - 1).Cells(ColSNo).Value = sl
                                    GV1.Rows(GV1.Rows.Count - 1).Cells(ColRouteCode).Value = objTr.Route_Code
                                    GV1.Rows(GV1.Rows.Count - 1).Cells(colCustCode).Value = objTr.Distributor_Code
                                    GV1.Rows(GV1.Rows.Count - 1).Cells(colCustName).Value = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_Customer_Master where cust_code='" + objTr.Distributor_Code + "'")
                                    GV1.Rows(GV1.Rows.Count - 1).Cells(colCRate).Value = objTr.Rate
                                    sl += 1
                                    GV1.Rows.AddNew()
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
                clsCommon.MyMessageBoxShow(Me, "Please Select Commission UOM", Me.Text)
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
            Dim str As String = "select Route_Code as [Route Code],Distributor_Code as [Distributor Code],Rate as [Rate] from TSPL_Distributor_Commission_Detail"
            Dim whrCls As String = ""

            ListImpExpColumnsMandatory = New List(Of String)({"Route Code", "Distributor Code", "Rate"})
            transportSql.ExporttoExcel(str, whrCls, Me)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub RefeshSNO()
        For ii As Integer = 1 To GV1.Rows.Count
            GV1.Rows(ii - 1).Cells(ColSNo).Value = ii
        Next
    End Sub
    Private Sub gv1_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles GV1.UserDeletedRow
        Try
            RefeshSNO()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles GV1.CurrentColumnChanged
        If GV1.RowCount > 0 Then
            Dim intCurrRow As Integer = GV1.CurrentRow.Index
            GV1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCDecimal(intCurrRow + 1)
            If intCurrRow = GV1.Rows.Count - 1 Then
                GV1.Rows.AddNew()
                GV1.CurrentRow = GV1.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles GV1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True


                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub LoadData(ByVal strDocNo As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True

            Dim obj As New clsDistributorCommission()
            obj = clsDistributorCommission.GetData(strDocNo, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_No) > 0) Then
                isNewEntry = False
                LoadBlankGrid()
                If obj.IsPosted = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    lblStatus.Status = ERPTransactionStatus.Approved
                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnSave.Text = "Update"
                    lblStatus.Status = ERPTransactionStatus.Pending
                End If
                btnGo.Enabled = False
                txtDocNo.Value = obj.Doc_No
                txtDate.Value = obj.Document_Date
                txtApplicableDate.Value = obj.Applicable_Date
                txtUOM.Value = obj.Commision_UOM
                txtItems.arrValueMember = obj.Items
                Dim sl As Integer = 1
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsDistributorCommissionDetails In obj.Arr

                        GV1.Rows(GV1.Rows.Count - 1).Cells(ColSNo).Value = sl
                        GV1.Rows(GV1.Rows.Count - 1).Cells(ColRouteCode).Value = objTr.Route_Code
                        GV1.Rows(GV1.Rows.Count - 1).Cells(colCustCode).Value = objTr.Distributor_Code
                        GV1.Rows(GV1.Rows.Count - 1).Cells(colCustName).Value = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_Customer_Master where cust_code='" + objTr.Distributor_Code + "'")
                        GV1.Rows(GV1.Rows.Count - 1).Cells(colCRate).Value = objTr.Rate
                        sl += 1
                        GV1.Rows.AddNew()
                    Next
                    'GV1.Rows.AddNew()

                End If



            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        'Prevent future date transaction

        If clsCommon.myLen(txtUOM.Value) <= 0 Then
            txtUOM.Focus()
            Throw New Exception("Please select UOM")
        End If
        'Dim Arr As New List(Of String)

        'For i As Integer = 0 To GV1.Rows.Count - 1
        '    If clsCommon.myLen(GV1.Rows(i).Cells(colCustCode).Value) > 0 And clsCommon.myLen(GV1.Rows(i).Cells(colZoneCode).Value) > 0 And clsCommon.myLen(GV1.Rows(i).Cells(colUOMCode).Value) > 0 Then
        '        Dim str As String = GV1.Rows(i).Cells(colCustCode).Value + GV1.Rows(i).Cells(colZoneCode).Value + GV1.Rows(i).Cells(colUOMCode).Value

        '        If Arr.Contains(str) Then
        '            Throw New Exception("Duplicate Data Found at Line No: " + clsCommon.myCstr(i + 1))
        '        Else
        '            Arr.Add(str)
        '        End If
        '    End If


        'Next
        Return True
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Public Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsDistributorCommission()
                obj.Doc_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Items = txtItems.arrValueMember
                obj.Applicable_Date = txtApplicableDate.Value
                obj.Commision_UOM = txtUOM.Value
                obj.Arr = GetTRData()
                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.Doc_No, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try

    End Sub
    Function GetTRData() As List(Of clsDistributorCommissionDetails)
        Dim Arr As New List(Of clsDistributorCommissionDetails)
        For ii As Integer = 0 To GV1.RowCount - 1
            If clsCommon.myLen(GV1.Rows(ii).Cells(colCustCode).Value) > 0 Then
                If clsCommon.myCDecimal(GV1.Rows(ii).Cells(colCRate).Value) > 0 Then


                    Dim objTr As New clsDistributorCommissionDetails()
                    'objTr.SNo = ii + 1
                    objTr.Distributor_Code = clsCommon.myCstr(GV1.Rows(ii).Cells(colCustCode).Value)
                    objTr.Route_Code = clsCommon.myCstr(GV1.Rows(ii).Cells(ColRouteCode).Value)
                    objTr.Rate = clsCommon.myCDecimal(GV1.Rows(ii).Cells(colCRate).Value)
                    Arr.Add(objTr)

                End If
            End If
        Next
        Return Arr
    End Function
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Public Sub PostData()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If

            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtDocNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsDistributorCommission.PostData(clsCommon.myCstr(txtDocNo.Value))
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(clsCommon.myCstr(txtDocNo.Value), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            If txtDocNo.MyReadOnly OrElse isButtonClicked Then
                Dim whrClas As String = "2=2"
                Dim qry As String = "select Doc_No as Code,Document_Date,Applicable_Date,Commision_UOM,Created_By,Created_Date,Modified_By,Modified_Date,IsPosted,Posted_Date from TSPL_Distributor_Commission_Head"
                LoadData(clsCommon.myCstr(clsCommon.ShowSelectForm("RPTBDMST", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked)), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_Distributor_Commission_Head where Doc_No='" + txtDocNo.Value + "'"

            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(clsCommon.myCstr(txtDocNo.Value), NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
Imports common
Imports System
Imports System.Text
Public Class frmRouteWiseSaleTarget

#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colSNo As String = "COLSNO"
    Const colRouteCode As String = "COLROUTECODE"
    Const colRouteName As String = "COLROUTENAME"
    Const colTargetQty As String = "COLTARGETQTY"
#End Region

    Private Sub frmRouteWiseSaleTarget_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Reset()
            createTable()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub createTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_Code", "varchar(30) NOT NULL Primary Key")
        coll.Add("Document_Date", "DateTime not NULL")
        coll.Add("Months", "Varchar(10) not NULL")
        coll.Add("UOM", "Varchar(10) NULL")
        coll.Add("Item_Sub_Category", "varchar(20) Not NULL references tspl_chapter_head(chapter_head_Code)")
        coll.Add("Remarks", "varchar(200) NULL")
        coll.Add("Status", "integer null")
        coll.Add("Created_By", "varchar(12)  NULL")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        coll.Add("Posted_By", "varchar(12) NULL")
        coll.Add("Posted_Date", "Datetime NULL")
        coll.Add("Inactive", "integer null")
        coll.Add("Inactive_By", "varchar(12) NULL")
        coll.Add("Inactive_Date", "Datetime NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_ROUTE_WISE_SALE_TARGET", coll, Nothing, True, True, "", "Document_Code", "Document_Date", True)

        coll = New Dictionary(Of String, String)()
        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Document_Code", "varchar(30) NOT NULL references TSPL_ROUTE_WISE_SALE_TARGET(Document_Code)")
        coll.Add("Route_Code", "varchar(12) NULL references TSPL_ROUTE_MASTER(Route_No)")
        coll.Add("Target_Qty", "Decimal(18,2) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_ROUTE_WISE_SALE_TARGET_DETAIL", coll, Nothing, True, True, "TSPL_ROUTE_WISE_SALE_TARGET", "DOCUMENT_CODE", "", True)
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        txtDocumentNo.Value = Nothing
        txtDocumentDate.Value = clsCommon.GETSERVERDATE()
        txtMonth.Value = txtDocumentDate.Value
        txtItemSubCateg.Value = Nothing
        lblItemSubCategName.Text = Nothing
        chkInactive.Checked = False
        lblStatus.Status = ERPTransactionStatus.Pending
        LoadBlankGrid()
    End Sub

    Private Sub LoadBlankGrid()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()

        Dim repoSNO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNO.FormatString = ""
        repoSNO.HeaderText = "S.No"
        repoSNO.Name = colSNo
        repoSNO.Width = 50
        repoSNO.ReadOnly = True
        repoSNO.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoSNO)

        Dim repoRouteCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRouteCode.FormatString = ""
        repoRouteCode.HeaderText = "Route Code"
        repoRouteCode.HeaderImage = My.Resources.search4
        repoRouteCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoRouteCode.Name = colRouteCode
        repoRouteCode.Width = 150
        repoRouteCode.ReadOnly = False
        repoRouteCode.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoRouteCode)

        Dim repoRouteName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRouteName.FormatString = ""
        repoRouteName.HeaderText = "Route Name"
        repoRouteName.Name = colRouteName
        repoRouteName.Width = 300
        repoRouteName.ReadOnly = True
        repoRouteName.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoRouteName)

        Dim repoTargetQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTargetQty.FormatString = "{0:n2}"
        repoTargetQty.HeaderText = "Target Qty"
        repoTargetQty.Name = colTargetQty
        repoTargetQty.Width = 200
        repoTargetQty.ReadOnly = False
        repoTargetQty.ShowUpDownButtons = True
        Gv1.MasterTemplate.Columns.Add(repoTargetQty)

        Gv1.Rows.AddNew()
        Gv1.AllowDeleteRow = True
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = False
        Gv1.AllowColumnReorder = False
        Gv1.AllowRowReorder = False
        Gv1.EnableSorting = False
        Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.AutoSizeRows = False
    End Sub

    Private Sub txtItemSubCateg__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtItemSubCateg._MYValidating
        Try
            Dim Qry As String = "select chapter_head_Code as Code,Description as [Description] from tspl_chapter_head"
            txtItemSubCateg.Value = clsCommon.ShowSelectForm("@ItemSubCateg", Qry, "Code", Nothing, txtItemSubCateg.Value, "Code", isButtonClicked)
            lblItemSubCategName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from tspl_chapter_head Where chapter_head_Code='" & txtItemSubCateg.Value & "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is Gv1.Columns(colRouteCode) Then
                        OpenRouteList(False)
                    ElseIf e.Column Is Gv1.Columns(colTargetQty) Then
                        Gv1.Rows.AddNew()
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenRouteList(ByVal isButtonClick As Boolean)
        Try
            Dim Qry As String = "Select Route_No As [Code],Route_Desc As [Description] from TSPL_ROUTE_MASTER"
            Gv1.CurrentRow.Cells(colRouteCode).Value = clsCommon.ShowSelectForm("@RouteCode", Qry, "Code", Nothing, Nothing, "Code", True)
            Gv1.CurrentRow.Cells(colRouteName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Route_Desc from TSPL_ROUTE_MASTER Where Route_No='" & clsCommon.myCstr(Gv1.CurrentRow.Cells(colRouteCode).Value) & "'"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub RefreshSNO()
        If Gv1 IsNot Nothing AndAlso Gv1.Rows.Count > 0 Then
            For i As Integer = 1 To Gv1.Rows.Count
                Gv1.Rows(i - 1).Cells(colSNo).Value = i
            Next
        End If
    End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(cboUOM.SelectedItem) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select UOM !", Me.Text)
                Return False
            End If
            If clsCommon.myLen(txtItemSubCateg.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Item Sub Category can't ne blank !", Me.Text)
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        SaveData(True)
    End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Try
            If AllowToSave() Then
                Dim obj As New clsRouteWiseSaleTarget()
                obj.Document_Code = txtDocumentNo.Value
                obj.Document_Date = txtDocumentDate.Value
                obj.Month = txtMonth.Value
                obj.UOM = clsCommon.myCstr(cboUOM.SelectedItem)
                obj.Item_Sub_Category = txtItemSubCateg.Value
                obj.Remarks = txtRemarks.Text
                If Gv1 IsNot Nothing AndAlso Gv1.Rows.Count > 0 Then
                    obj.Arr = New List(Of clsRouteWiseSaleTargetDetail)
                    For Each row In Gv1.Rows
                        Dim objTr As New clsRouteWiseSaleTargetDetail()
                        objTr.Route_Code = clsCommon.myCstr(row.Cells(colRouteCode).Value)
                        objTr.Target_Qty = clsCommon.myCDecimal(row.Cells(colTargetQty).Value)
                        If clsCommon.myLen(objTr.Route_Code) > 0 Then
                            obj.Arr.Add(objTr)
                        End If
                        objTr = Nothing
                    Next
                End If
                If clsCommon.myLen(obj.Document_Code) <= 0 Then
                    isNewEntry = True
                End If
                If obj.SaveData(obj, isNewEntry) Then
                    If isPost Then
                        If clsCommon.MyMessageBoxShow(Me, "Are you sure to post data ?", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes AndAlso obj.PostData(obj.Document_Code) Then
                            clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully.", Me.Text)
                        End If
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully.", Me.Text)
                    End If
                    LoadData(obj.Document_Code, Nothing)
                End If
                obj = Nothing
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub LoadData(ByVal strDoc As String, ByVal NavTyep As NavigatorType)
        Dim obj As clsRouteWiseSaleTarget = Nothing
        Try
            If clsCommon.myLen(strDoc) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Document code not found to load data !")
            Else
                obj = New clsRouteWiseSaleTarget()
                obj = obj.GetData(strDoc, NavTyep)
                If obj IsNot Nothing Then
                    isInsideLoadData = True
                    txtDocumentNo.Value = obj.Document_Code
                    txtDocumentDate.Value = obj.Document_Date
                    txtMonth.Value = obj.Month
                    cboUOM.SelectedText = obj.UOM
                    txtItemSubCateg.Value = obj.Item_Sub_Category
                    txtRemarks.Text = obj.Remarks
                    lblStatus.Status = IIf(obj.Status = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                    chkInactive.Checked = IIf(obj.Inactive = 1, True, False)
                    If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                        For Each row In obj.Arr
                            Gv1.Rows.AddNew()
                            Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSNo).Value = row.LineNo
                            Gv1.Rows(Gv1.Rows.Count - 1).Cells(colRouteCode).Value = row.Route_Code
                            Gv1.Rows(Gv1.Rows.Count - 1).Cells(colRouteName).Value = row.Route_Name
                            Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTargetQty).Value = row.Target_Qty
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            obj = Nothing
            Throw New Exception(ex.Message)
        End Try
        obj = Nothing
    End Sub


    Private Sub Gv1_CellValidated(sender As Object, e As CellValidatedEventArgs) Handles Gv1.CellValidated
        Try
            setGridFocus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub setGridFocus()
        Try
            If Gv1.CurrentColumn IsNot Nothing AndAlso clsCommon.CompairString(Gv1.CurrentColumn.Name, colTargetQty) = CompairStringResult.Equal Then
                Gv1.CurrentColumn = Gv1.Columns(colRouteCode)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub Gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles Gv1.CurrentColumnChanged
        Try
            RefreshSNO()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Gv1_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles Gv1.UserDeletedRow
        Try
            RefreshSNO()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocumentNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocumentNo._MYValidating
        Try
            Dim Qry As String = "Select Document_Code As DocumentCode,Document_Date As [Document Date],Months,Item_Sub_Category As [Item Sub Category],Case When IsNull(Status,0)=0 Then 'Pending' Else 'Approved' End As Status from TSPL_ROUTE_WISE_SALE_TARGET"
            LoadData(clsCommon.ShowSelectForm("@Doc", Qry, "DocumentCode", "", txtDocumentNo.Value, "TSPL_ROUTE_WISE_SALE_TARGET.Document_Date desc", isButtonClicked), Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
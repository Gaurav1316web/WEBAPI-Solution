Imports common
Public Class frmCommissionTPTCalculation
#Region "Variables"

    Const colLineNo As String = "COLLNO"
    Const colDate As String = "COLDDATE"
    Const colRoute As String = "COLROUTE"
    Const colQty As String = "COLQTY"
    Const colAmt As String = "COLAMT"
#End Region

    Private Sub frmCommissionTPTCalculation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankGridAC()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLine As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLine.FormatString = ""
        repoLine.HeaderText = "S.No."
        repoLine.Name = colLineNo
        'repoLine.HeaderImage = My.Resources.search4
        'repoLine.TextImageRelation = TextImageRelation.TextBeforeImage
        repoLine.Width = 150
        repoLine.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoLine)

        Dim repoDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Date"
        repoDate.Name = colDate
        repoDate.Width = 150
        repoDate.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoDate)

        Dim repoRoute As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRoute.FormatString = ""
        repoRoute.HeaderText = "Route"
        repoRoute.Name = colRoute
        repoRoute.Width = 150
        repoRoute.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoRoute.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoRoute)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 100
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoQty.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colAmt
        repoAmt.Width = 300
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmt.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoAmt)

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.BestFitColumns()
        'ReStoreGridLayout()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If obj IsNot Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = True
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Try
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub AddNew()
        txtDocNo.Value = Nothing
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtRemarks.Text = Nothing
        Reset()
        LoadBlankGridAC()
    End Sub

    Sub Reset()
        txtFromDate.Value = txtDate.Value
        txtToDate.Value = txtDate.Value
        txtMultRoute.arrValueMember = Nothing
        txtMultItems.arrValueMember = Nothing
        txtDistributorCode.Value = Nothing
        lblDistributorName.Text = Nothing
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultRoute__My_Click(sender As Object, e As EventArgs) Handles txtMultRoute._My_Click
        Try
            Dim qry As String = "select Route_No As [Route Code],Route_Desc As [Route Name] from TSPL_ROUTE_MASTER where 2=2 "
            txtMultRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("@Route", qry, "Route_No", "Route_Desc", txtMultRoute.arrValueMember, txtMultRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDistributorCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDistributorCode._MYValidating
        Try
            Dim strQry As String = "select Cust_Code As [Code],Customer_Name As [Distributor Name] from TSPL_Customer_Master"
            txtDistributorCode.Value = clsCommon.ShowSelectForm("@Distrinbutor", strQry, "Code", Nothing, txtDistributorCode.Value, "Code", isButtonClicked)
            lblDistributorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQry & " Where Cust_Code='" & txtDistributorCode.Value & "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultItems__My_Click(sender As Object, e As EventArgs) Handles txtMultItems._My_Click
        Try
            Dim qry As String = " select Item_Code As [Item Code],Item_Desc As [Item Name] from TSPL_ITEM_MASTER order by Item_Code "
            txtMultItems.arrValueMember = clsCommon.ShowMultipleSelectForm("@ItemMulSel", qry, "Item_Code", "Item_Code", txtMultItems.arrValueMember, txtMultItems.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
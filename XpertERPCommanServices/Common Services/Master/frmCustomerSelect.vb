Imports common
Public Class frmCustomerSelect
#Region "Variables"
    Public lvl As Integer = 0
    Public strusercode As String = ""
    Public strCode As String = ""
    Public arrIn As Dictionary(Of String, Object) = Nothing
    Public arrOut As Dictionary(Of String, Object) = Nothing
    Public isCancel As Boolean = False
#End Region

    Private Sub frmCustomerSelect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        
        LoadCustomer()
       
        rbtnCategorySelect.IsChecked = True
    End Sub
    
    Sub LoadCustomer()
        gvCategory.DataSource = Nothing
        '============29/07/205 add location code cond. because main location should also show in finder against ticket BM00000007506
        Dim qry As String = "" 'select cast( 1 as bit) as SEL,Cust_Code as CODE,Customer_Name as NAME from TSPL_CUSTOMER_MASTER where Cust_Group_Code = '" + strCode + "' order by Cust_Code"

        If clsCommon.myLen(strusercode) > 0 Then
            qry = "select cast((case when TSPL_CUSTOMER_GROUP_MAPPING_DETAIL.user_code is null then 0 else 1 end) as bit) as SEL "
            qry += " ,TSPL_CUSTOMER_MASTER.Cust_Code as CODE,TSPL_CUSTOMER_MASTER.Customer_Name as NAME "
            qry += " from TSPL_CUSTOMER_MASTER "
            qry += " left join TSPL_CUSTOMER_GROUP_MAPPING on TSPL_CUSTOMER_GROUP_MAPPING.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code "
            qry += " left join TSPL_CUSTOMER_GROUP_MAPPING_DETAIL on TSPL_CUSTOMER_GROUP_MAPPING_DETAIL.cust_code=TSPL_CUSTOMER_MASTER.cust_code  and "
            qry += " TSPL_CUSTOMER_GROUP_MAPPING_DETAIL.cust_group_code = TSPL_CUSTOMER_MASTER.cust_group_code "
            qry += " and TSPL_CUSTOMER_GROUP_MAPPING_DETAIL.User_Code=TSPL_CUSTOMER_GROUP_MAPPING.User_Code "
            qry += " where TSPL_CUSTOMER_MASTER.Cust_Group_Code = '" + strCode + "' "
            qry += " and TSPL_CUSTOMER_GROUP_MAPPING.user_code='" + strusercode + "' "
            qry += " order by TSPL_CUSTOMER_MASTER.Cust_Code"
        Else
            qry = "select cast( 1 as bit) as SEL,Cust_Code as CODE,Customer_Name as NAME from TSPL_CUSTOMER_MASTER where Cust_Group_Code = '" + strCode + "' order by Cust_Code"
        End If
        gvCategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Description"


        gvCategory.ShowGroupPanel = False
        gvCategory.AllowAddNewRow = False

        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.ShowFilteringRow = True
        gvCategory.EnableFiltering = True
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = True
        gvCategory.BestFitColumns()
    End Sub


   
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        If rbtnCategoryAll.IsChecked Then
            'arrOut = Nothing
            arrIn = New Dictionary(Of String, Object)
            For ii As Integer = 0 To gvCategory.Rows.Count - 1
                'If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
                arrIn.Add(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), gvCategory.Rows(ii).Tag)
                'End If
            Next
        Else
            arrIn = New Dictionary(Of String, Object)
            For ii As Integer = 0 To gvCategory.Rows.Count - 1
                If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
                    arrIn.Add(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), gvCategory.Rows(ii).Tag)
                End If
            Next
        End If
        Me.Close()
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        isCancel = True
        Me.Close()
    End Sub

    Private Sub gvCategory_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvCategory.CellDoubleClick
        'If lvl = 1 AndAlso clsCommon.myCBool(gvCategory.CurrentRow.Cells("SEL").Value) Then
        '    Dim frm As New frmCustomerSelect()
        '    frm.lvl = 2
        '    frm.strCode = clsCommon.myCstr(gvCategory.CurrentRow.Cells("CODE").Value)
        '    frm.arrIn = gvCategory.CurrentRow.Tag
        '    frm.ShowDialog()
        '    If Not frm.isCancel Then
        '        gvCategory.CurrentRow.Tag = frm.arrOut
        '    End If
        'End If
    End Sub

    'Private Sub rbtnCategoryAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
    '    gvCategory.Enabled = rbtnCategorySelect.IsChecked
    'End Sub

    Private Sub RadButton7_Click(sender As Object, e As EventArgs) Handles RadButton7.Click
        CheckedAll(gvCategory)
    End Sub

    Private Sub RadButton6_Click(sender As Object, e As EventArgs) Handles RadButton6.Click
        UnCheckedAll(gvCategory)
    End Sub
    Private Sub CheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.ChildRows.Count - 1
            gv.ChildRows(ii).Cells("SEL").Value = True
        Next
    End Sub

    Private Sub UnCheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged
        gvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub
End Class

Imports System.Data.SqlClient
Imports System.IO
Imports common

Public Class frmDemand_Sheet
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim isLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False

    Const colLineNo As String = "colLineNo"
    Const colCustCode As String = "colCustCode"
    Const colSetZero As String = "colSetZero"
    Const colItemCode As String = "colItemCode"

#End Region

    'Public Sub SetUserMgmtNew()
    ''MyBase.SetUserMgmt(clsUserMgtCode.frmbookingdairy)
    ' If Not (MyBase.isReadFlag) Then
    '    Throw New Exception("Permission Denied")
    '    Me.Close()
    '    Exit Sub
    'End If
    'btnSave.Visible = MyBase.isModifyFlag
    ''btnPost.Visible = MyBase.isPostFlag

    'If MyBase.isReverse Then
    '    btnreverse.Enabled = True
    'Else
    '    btnreverse.Enabled = False
    'End If
    'End Sub

    Private Sub frmDemandSheet_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        AddNew()
        ' SetUserMgmtNew()
        DemandSheetTable()

    End Sub

    Sub LoadBlankGrid()

        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        gv1.Rows.AddNew()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.IsPinned = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)
        Dim repoCustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustCode.FormatString = ""
        repoCustCode.HeaderText = "Booth Code"
        repoCustCode.Name = colCustCode
        repoCustCode.HeaderImage = My.Resources.search4
        repoCustCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCustCode.Width = 180
        repoCustCode.IsVisible = True
        repoCustCode.IsPinned = True
        repoCustCode.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoCustCode)
        Dim repoSetZero As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSetZero = New GridViewDecimalColumn()
        repoSetZero.FormatString = ""
        repoSetZero.HeaderText = "Set Zero"
        repoSetZero.Name = colSetZero
        repoSetZero.Width = 100
        repoSetZero.ReadOnly = False
        repoSetZero.IsPinned = True
        repoSetZero.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSetZero)
        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        qry = "select * from (select 'Fresh' as FreshAmbient,tspl_item_master.Item_Code,tspl_item_master.Short_Description ,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description as ItemDescNew,1 as RowNo,tspl_item_master.Sku_Seq   from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_FreshItem =1 AND TSPL_ITEM_MASTER.Is_Milk_Pouch =1 and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    and TSPL_ITEM_UOM_DETAIL.Uom_code in ('Crate')
    union
    select 'Fresh' as FreshAmbient,tspl_item_master.Item_Code ,tspl_item_master.Short_Description,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description as ItemDescNew,1 as RowNo,tspl_item_master.Sku_Seq  from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_FreshItem =1 AND TSPL_ITEM_MASTER.Is_Milk_Pouch =1 and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    and TSPL_ITEM_UOM_DETAIL.Uom_code in ('Crate')
    union all
    select 'Ambient' as FreshAmbient,tspl_item_master.Item_Code ,tspl_item_master.Short_Description,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description  as ItemDescNew,2 as RowNo,tspl_item_master.Sku_Seq   from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_Ambient=1   and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    and TSPL_ITEM_UOM_DETAIL.Default_UOM=1
    )z order by RowNo,Sku_Seq,Item_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim i As Integer = 1
            Dim obj As ItemValueClass = New ItemValueClass()
            For Each dr As DataRow In dt.Rows
                repoIName = New GridViewTextBoxColumn()
                repoIName.FormatString = ""
                repoIName.HeaderText = clsCommon.myCstr(dr("ItemDescNew"))
                obj = New ItemValueClass()
                obj.itemCode = clsCommon.myCstr(dr("Item_Code"))
                obj.itemDesc = clsCommon.myCstr(dr("Item_Desc"))
                obj.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                obj.IsFreshAmbient = clsCommon.myCstr(dr("FreshAmbient"))
                obj.ShortDesc = clsCommon.myCstr(dr("Short_Description"))
                repoIName.Tag = obj
                repoIName.Name = colItemCode + clsCommon.myCstr(i)
                repoIName.Width = 150
                repoIName.IsVisible = True
                i = i + 1
                gv1.MasterTemplate.Columns.Add(repoIName)
            Next
        End If



    End Sub

    Sub AddNew()
        LoadBlankGrid()
        Dim CurrDateTime As DateTime = clsCommon.GETSERVERDATE
        If clsCommon.myCdbl(CurrDateTime.Hour) >= 7 AndAlso clsCommon.myCdbl(CurrDateTime.Hour) < 10 Then
            txtDate.Value = clsCommon.GetPrintDate(CurrDateTime)
            txtShift.Text = "Evening"
        Else
            txtDate.Value = clsCommon.GetPrintDate(CurrDateTime.AddDays(1))
            txtShift.Text = "Evening"
        End If

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.Name = colCustCode Then
                        gv1.CurrentRow.Cells(colCustCode).Value = clsDistributorRouteTagging.getFinder(" IsDistributor='N' ", clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value), False)
                    End If
                    If e.Column.Index >= 3 AndAlso gv1.Rows.Count > 0 Then
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                        UpdateAllTotals()
                    End If
                    isCellValueChangedOpen = False
                    End If
                    isInsideLoadData = False

            End If

        Catch ex As Exception
            isInsideLoadData = False
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        If gv1.Rows(IntRowNo).Cells(colSetZero).Value = 1 Then
            For Each grow As GridViewRowInfo In gv1.Rows
                For dbColumn As Integer = 3 To gv1.Columns.Count - 1
                    grow.Cells(dbColumn).Value = "0"
                Next
            Next
        Else
            If gv1.Rows(IntRowNo).Cells(colCustCode).Value <> "" Then
                Dim obj As New clsDemandSheet()
                obj.DEMAND_Date = clsCommon.GetPrintDate(txtDate.Value)
                obj.Cust_Code = gv1.Rows(IntRowNo).Cells(colCustCode).Value
                obj.Set_Zero = gv1.Rows(IntRowNo).Cells(colSetZero).Value
                obj.ShiftType = txtShift.Text
                Dim k As Integer = 1
                For dblcolumns As Integer = 3 To gv1.Columns.Count - 1
                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                    k = k + 1
                    If obj1 IsNot Nothing Then
                        If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 Then  'AndAlso clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(dblcolumns).Value) > 0
                            obj.Item_Code = clsCommon.myCstr(obj1.itemCode)
                            obj.Qty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(dblcolumns).Value)
                            Try
                                Dim status As Boolean = obj.SaveData(obj)

                            Catch ex As Exception
                                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                            End Try
                        End If
                    End If

                Next
            End If
        End If
    End Sub
    Private Sub UpdateAllTotals()
        'UpdateColumnTotal()
    End Sub
    Private Sub UpdateColumnTotal()
        Try
            Dim TotalQty As Double = 0
            'For dbrows1 As Integer = 0 To gv1.Rows.Count - 1
            For dblcolumns As Integer = 3 To gv1.Columns.Count - 1
                TotalQty = 0
                For dbrows As Integer = 0 To gv1.Rows.Count - 1
                    TotalQty += clsCommon.myCdbl(gv1.Rows(dbrows).Cells(dblcolumns).Value)
                Next
                gv1.Rows(gv1.Rows.Count - 1).Cells(dblcolumns).Value = clsCommon.myCstr(TotalQty)
            Next
            'Next


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.Rows.Count > 0 Then
            If gv1.CurrentRow.Index = gv1.Rows.Count - 1 Then
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 2)
            End If
        End If
    End Sub

    Public Sub DemandSheetTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("DEMAND_Date", "datetime Not null")
        coll.Add("ShiftType", "VARCHAR(200)")
        coll.Add("Cust_Code", "varchar(12) null references TSPL_CUSTOMER_MASTER(Cust_Code)")
        coll.Add("Set_Zero", "integer NOT NULL")
        coll.Add("Item_Code", "Varchar(50) Not NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Qty", "Decimal (18,2) NULL")
        coll.Add("Created_By", "varchar(12)  Not NULL")
        coll.Add("Created_Date", "datetime  Not NULL")
        coll.Add("Modify_By", "varchar(12)  Not NULL")
        coll.Add("Modify_Date", "datetime  Not NULL")
        'coll.Add("Delete_Operation", "Char(1) NUll")
        clsCommonFunctionality.CreateOrAlterTable(False, "TSPL_DEMAND_SHEET", coll, "", True)

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        UpdateColumnTotal()
        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully")
    End Sub
End Class
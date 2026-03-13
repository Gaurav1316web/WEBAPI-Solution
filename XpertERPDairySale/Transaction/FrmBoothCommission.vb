Imports common

Public Class FrmBoothCommission

#Region "Variables"
    Dim isNewEntry As Boolean = False
    Dim InActiveDoc As Boolean = False
    Const colSNo As String = "colSNo"
    Const colICode As String = "colICode"
    Const colIName As String = "colIName"
    Const colCRate As String = "colCRate"
    Private isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
#End Region
    Public Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmBoothCommission_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        AddNew()
    End Sub
    Private Sub AddNew()
        Try
            isNewEntry = True
            txtDocNo.Value = ""
            txtDate.Value = clsCommon.GETSERVERDATE
            txtMonthYear.Value = txtDate.Value
            txtRemark.Text = ""
            txtComment.Text = ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator

    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim Monthyear As DateTime = clsCommon.GetPrintDate(txtMonthYear.Value)
            Dim FromDate As DateTime = New DateTime(Monthyear.Year, Monthyear.Month, 1)
            Dim ToDate As DateTime = New DateTime(Monthyear.Year, clsCommon.myCDate(txtMonthYear.Value).Month, DateTime.DaysInMonth(Monthyear.Year, Monthyear.Month))
            Dim strQry As String = "DECLARE @cols NVARCHAR(MAX) = ''; DECLARE @sql NVARCHAR(MAX); SELECT @cols = STUFF(( SELECT ',' + QUOTENAME(FORMAT(Document_Date, 'dd-MMM-yy')) FROM (SELECT DISTINCT Document_Date FROM TSPL_DEMAND_BOOKING_MASTER WHERE CONVERT(date, Document_Date, 103) BETWEEN '" & clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") & "' AND '" & clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") & "') t FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, ''); SET @sql = '
WITH DatePivot AS (
    SELECT 
        c.Cust_Code, 
        d.Item_Code, 
        FORMAT(m.Document_Date, ''dd-MMM-yy'') as DateCol,
        SUM((d.Qty*ICFCurrentUOM.Conversion_Factor)/ICFInLtr.Conversion_Factor) as DailyQty,
        SUM(SUM((d.Qty*ICFCurrentUOM.Conversion_Factor)/ICFInLtr.Conversion_Factor)) OVER (PARTITION BY c.Cust_Code, d.Item_Code) as TotalQty  
    FROM TSPL_DEMAND_BOOKING_MASTER m
    LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL d ON d.Document_No = m.Document_No
    LEFT JOIN TSPL_CUSTOMER_MASTER c ON c.Cust_Code = d.Cust_Code
	left join TSPL_ITEM_UOM_DETAIL as ICFCurrentUOM on ICFCurrentUOM.Item_Code=d.Item_Code and ICFCurrentUOM.UOM_Code=d.Unit_code
	left join TSPL_ITEM_UOM_DETAIL as ICFInLtr on ICFInLtr.Item_Code=d.Item_Code and ICFInLtr.UOM_Code=''LTR''
    WHERE CONVERT(date, m.Document_Date, 103) BETWEEN ''" & clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") & "'' AND ''" & clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") & "''
      AND m.Posted = 1 AND d.source_by = ''App'' 
    GROUP BY c.Cust_Code, d.Item_Code,d.Unit_Code, m.Document_Date
)
SELECT Cust_Code, Item_Code, ' + @cols + ', TotalQty
FROM DatePivot
PIVOT (
    SUM(DailyQty) FOR DateCol IN (' + @cols + ')
) pvt
ORDER BY Cust_Code, Item_Code'; EXEC sp_executesql @sql;"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                RadPageView1.SelectedPage = RadPageViewPage7
                gv2.GroupDescriptors.Clear()
                gv2.MasterTemplate.SummaryRowsBottom.Clear()
                gv2.DataSource = dt
                gv2.AutoExpandGroups = True
                gv2.ShowGroupPanel = True
                gv2.ShowRowHeaderColumn = False
                gv2.AllowAddNewRow = False
                gv2.AllowDeleteRow = False
                gv2.EnableFiltering = True
                gv2.ShowFilteringRow = True
                gv2.BestFitColumns()
            Else
                Throw New Exception("No Data Found to Display")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
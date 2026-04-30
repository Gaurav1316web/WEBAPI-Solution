Imports common

Public Class FrmBoothCommission

#Region "Variables"
    Dim isNewEntry As Boolean = False
    Dim isDayWiseValidate As Boolean = False
    Dim InActiveDoc As Boolean = False
    Const colLineNo As String = "colLineNo"
    Const colCustCode As String = "colCustCode"
    Const colCustName As String = "colCustName"
    Const colICode As String = "colICode"
    Const colIName As String = "colIName"
    Const colBCCode As String = "colBCCode"
    Const colBCPKID As String = "colBCPKID"
    Const colBCMinQty As String = "colDCMinQty"
    Const colBCUOM As String = "colBCUOM"
    Const colTotalQty As String = "colTotalQty"
    Const colBCRate As String = "colBCRate"
    Const colCAmt As String = "colCAmt"
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
        CreateTable()
        AddNew()
    End Sub
    Private Sub AddNew()
        Try
            isNewEntry = True
            isDayWiseValidate = False
            UsLock1.Status = ERPTransactionStatus.Pending
            txtDocNo.Value = ""
            txtDate.Value = clsCommon.GETSERVERDATE
            txtMonthYear.Value = txtDate.Value
            txtRemark.Text = ""
            txtComment.Text = ""
            chkMobileUser.Checked = True
            btnSave.Enabled = True
            btnDelete.Enabled = True
            btnPost.Enabled = True
            LoadBlankGrid()
            btnGo.Enabled = True
            gv2.DataSource = Nothing
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = colLineNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        Dim repoBoothCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBoothCode.FormatString = ""
        repoBoothCode.HeaderText = "Booth Code"
        repoBoothCode.Name = colCustCode
        repoBoothCode.IsVisible = True
        repoBoothCode.ReadOnly = True
        repoBoothCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBoothCode)

        Dim repoBoothName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBoothName.FormatString = ""
        repoBoothName.HeaderText = "Booth Name"
        repoBoothName.Name = colCustName
        repoBoothName.IsVisible = True
        repoBoothName.ReadOnly = True
        repoBoothName.Width = 200
        repoBoothName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBoothName)
        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.IsVisible = True
        repoICode.ReadOnly = True
        repoICode.Width = 200
        repoICode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoICode)
        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Desc"
        repoIName.Name = colIName
        repoIName.IsVisible = True
        repoIName.ReadOnly = True
        repoIName.Width = 200
        repoIName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoIName)
        Dim repoBCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBCCode.FormatString = ""
        repoBCCode.HeaderText = "Commission Code"
        repoBCCode.Name = colBCCode
        repoBCCode.IsVisible = False
        repoBCCode.ReadOnly = True
        repoBCCode.Width = 200
        repoBCCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBCCode)
        Dim repoBCPKID As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBCPKID.FormatString = ""
        repoBCPKID.HeaderText = "Commission PKID"
        repoBCPKID.Name = colBCPKID
        repoBCPKID.IsVisible = False
        repoBCPKID.ReadOnly = True
        repoBCPKID.Width = 200
        repoBCPKID.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBCPKID)

        Dim repoBCUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBCUOM.FormatString = ""
        repoBCUOM.HeaderText = "Commission UOM"
        repoBCUOM.Name = colBCUOM
        repoBCUOM.IsVisible = False
        repoBCUOM.ReadOnly = True
        repoBCUOM.Width = 200
        repoBCUOM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBCUOM)
        Dim repoBCMinQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBCMinQty.FormatString = "{0:n4}"
        repoBCMinQty.HeaderText = "Commission Min Qty"
        repoBCMinQty.Name = colBCMinQty
        repoBCMinQty.Width = 120
        repoBCMinQty.Minimum = 0
        repoBCMinQty.ShowUpDownButtons = False
        repoBCMinQty.Step = 0
        repoBCMinQty.DecimalPlaces = 4
        repoBCMinQty.IsVisible = True
        repoBCMinQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBCMinQty)
        Dim repoTotalQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalQty.FormatString = "{0:n4}"
        repoTotalQty.HeaderText = "Total Qty"
        repoTotalQty.Name = colTotalQty
        repoTotalQty.Width = 120
        repoTotalQty.Minimum = 0
        repoTotalQty.ShowUpDownButtons = False
        repoTotalQty.Step = 0
        repoTotalQty.DecimalPlaces = 4
        repoTotalQty.IsVisible = True
        repoTotalQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTotalQty)
        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = "{0:n4}"
        repoRate.HeaderText = "Commission Rate"
        repoRate.Name = colBCRate
        repoRate.Width = 120
        repoRate.Minimum = 0
        repoRate.ShowUpDownButtons = False
        repoRate.Step = 0
        repoRate.DecimalPlaces = 4
        repoRate.IsVisible = True
        repoRate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoRate)
        Dim repoCommisionAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCommisionAmt.FormatString = "{0:n4}"
        repoCommisionAmt.HeaderText = "Commission Amt"
        repoCommisionAmt.Name = colCAmt
        repoCommisionAmt.Width = 120
        repoCommisionAmt.Minimum = 0
        repoCommisionAmt.ShowUpDownButtons = False
        repoCommisionAmt.Step = 0
        repoCommisionAmt.DecimalPlaces = 4
        repoCommisionAmt.IsVisible = True
        repoCommisionAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCommisionAmt)
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
        gv1.BestFitColumns()
        gv1.Rows.AddNew()
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_BOOTH_COMMISSION_CALCULATION_Master where Document_Code='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(clsCommon.myCstr(txtDocNo.Value), NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            If txtDocNo.MyReadOnly OrElse isButtonClicked Then
                Dim whrClas As String = "2=2"
                Dim qry As String = "select Document_Code as Code,Document_Date,Month_Year,Is_Mobile_User,Posted from TSPL_BOOTH_COMMISSION_CALCULATION_Master "
                LoadData(clsCommon.myCstr(clsCommon.ShowSelectForm("fndDTHComm", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked, "Document_Date")), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim Monthyear As DateTime = clsCommon.GetPrintDate(txtMonthYear.Value)
            Dim FromDate As DateTime = New DateTime(Monthyear.Year, Monthyear.Month, 1)
            Dim ToDate As DateTime = New DateTime(Monthyear.Year, clsCommon.myCDate(txtMonthYear.Value).Month, DateTime.DaysInMonth(Monthyear.Year, Monthyear.Month))
            Dim SourceBy As String = ""
            If chkMobileUser.Checked Then
                SourceBy = "APP"
            Else
                SourceBy = "ERP"
            End If
            Dim IsDocExits As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from tspl_booth_commission_Calculation_master where convert(date,Month_Year,103)='" & clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") & "'"))
            If clsCommon.myLen(IsDocExits) <= 0 Then
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
      AND m.Posted = 1 AND d.source_by = ''" & SourceBy & "'' 
    GROUP BY c.Cust_Code, d.Item_Code,d.Unit_Code, m.Document_Date
)
SELECT Cust_Code, Item_Code, ' + @cols + ', TotalQty
FROM DatePivot
PIVOT (
    SUM(DailyQty) FOR DateCol IN (' + @cols + ')
) pvt
ORDER BY Cust_Code, Item_Code'; EXEC sp_executesql @sql;"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
                gv2.DataSource = Nothing
                gv2.Rows.Clear()
                gv2.Columns.Clear()
                gv2.GroupDescriptors.Clear()
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    'RadPageView1.SelectedPage = RadPageViewPage7
                    gv2.MasterTemplate.SummaryRowsBottom.Clear()
                    gv2.DataSource = dt
                    gv2.AutoExpandGroups = True
                    SetGridFormation()
                    gv2.ShowGroupPanel = False
                    gv2.ShowRowHeaderColumn = False
                    gv2.AllowAddNewRow = False
                    gv2.AllowDeleteRow = False
                    gv2.EnableFiltering = True
                    gv2.ShowFilteringRow = True
                    gv2.BestFitColumns()
                    GetBCDetail()
                    btnGo.Enabled = False
                Else
                    Throw New Exception("No Data Found to Display")
                End If
            Else
                Throw New Exception("Document already exists [" & IsDocExits & "]")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormation()
        gv2.TableElement.TableHeaderHeight = 40
        gv2.MasterTemplate.ShowRowHeaderColumn = True
        gv2.EnableFiltering = True
        gv2.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).IsVisible = True
            If ii >= 2 Then
                gv2.Columns(ii).FormatString = "{0:n2}"
            End If
        Next

    End Sub
    Private Sub GetBCDetail()
        Try
            Dim Monthyear As DateTime = clsCommon.GetPrintDate(txtMonthYear.Value)
            Dim NoofDays As Integer = clsCommon.myCdbl(DateTime.DaysInMonth(Monthyear.Year, Monthyear.Month))
            If gv2.Columns.Count - 3 = NoofDays Then
                isDayWiseValidate = True
            End If
            Dim IsValidPerDayQty As Boolean = False

            If isDayWiseValidate Then
                Dim strQry As String = ""
                Dim dt As DataTable = Nothing
                For intRows As Integer = 0 To gv2.Rows.Count - 1
                    strQry = "select top 1 TSPL_BOOTH_COMMISSION_MASTER.Document_Code,TSPL_BOOTH_COMMISSION_DETAIL.PK_ID,TSPL_BOOTH_COMMISSION_MASTER.Min_Per_Day_Qty,TSPL_BOOTH_COMMISSION_MASTER.Commision_UOM,TSPL_BOOTH_COMMISSION_DETAIL.Item_Code,TSPL_BOOTH_COMMISSION_DETAIL.Commission_Rate

from TSPL_BOOTH_COMMISSION_MASTER
left join TSPL_BOOTH_COMMISSION_DETAIL on TSPL_BOOTH_COMMISSION_MASTER.Document_Code = TSPL_BOOTH_COMMISSION_DETAIL.Document_Code
where 
TSPL_BOOTH_COMMISSION_MASTER.Posted=1 and TSPL_BOOTH_COMMISSION_MASTER.Inactive=0 and TSPL_BOOTH_COMMISSION_MASTER.Is_Mobile_User='" & IIf(chkMobileUser.Checked, "1", "0") & "'
 and TSPL_BOOTH_COMMISSION_MASTER.From_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' and TSPL_BOOTH_COMMISSION_DETAIL.Item_Code='" & clsCommon.myCstr(gv2.Rows(intRows).Cells(1).Value) & "'  and 2=(Case when TSPL_BOOTH_COMMISSION_MASTER.To_Date is null then 2 else (Case when TSPL_BOOTH_COMMISSION_MASTER.To_Date>'" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' then 2 else 3 end) end) order by From_Date desc "
                    dt = clsDBFuncationality.GetDataTable(strQry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Item_Code")), clsCommon.myCstr(gv2.Rows(intRows).Cells(1).Value)) = CompairStringResult.Equal Then
                            For intcol As Integer = 2 To gv2.Columns.Count - 3
                                If clsCommon.myCdbl(gv2.Rows(intRows).Cells(intcol).Value) >= clsCommon.myCdbl(dt.Rows(0)("Min_Per_Day_Qty")) Then
                                    IsValidPerDayQty = True
                                Else
                                    IsValidPerDayQty = False
                                    Exit For
                                End If
                            Next
                            If IsValidPerDayQty Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colCustCode).Value = clsCommon.myCstr(gv2.Rows(intRows).Cells(0).Value)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colCustName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" & clsCommon.myCstr(gv2.Rows(intRows).Cells(0).Value) & "'"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(gv2.Rows(intRows).Cells(1).Value)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Short_Description from TSPL_ITEM_MASTER where Item_Code='" & clsCommon.myCstr(gv2.Rows(intRows).Cells(1).Value) & "'"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBCCode).Value = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBCPKID).Value = clsCommon.myCdbl(dt.Rows(0)("PK_ID"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBCUOM).Value = clsCommon.myCstr(dt.Rows(0)("Commision_UOM"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBCMinQty).Value = clsCommon.myCdbl(dt.Rows(0)("Min_Per_Day_Qty"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalQty).Value = clsCommon.myCdbl(gv2.Rows(intRows).Cells(gv2.Columns.Count - 1).Value)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBCRate).Value = clsCommon.myCdbl(dt.Rows(0)("Commission_Rate"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colCAmt).Value = clsCommon.myCdbl(gv2.Rows(intRows).Cells(gv2.Columns.Count - 1).Value) * clsCommon.myCdbl(dt.Rows(0)("Commission_Rate"))
                                gv1.Rows.AddNew()

                            End If
                        End If

                    End If


                Next
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub CreateTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_Code", "Varchar(30) Not null Primary key")
        coll.Add("Document_Date", "date not null")
        coll.Add("Month_Year", "date not null")
        coll.Add("Is_Mobile_User", "Integer NOT NULL")
        coll.Add("Remark", "varchar(200) NULL")
        coll.Add("Comment", "varchar(200) NULL")
        coll.Add("Posted", "integer NULL")
        coll.Add("Created_By", "varchar(12)  Not NULL")
        coll.Add("Created_Date", "datetime  Not NULL")
        coll.Add("Modified_By", "varchar(12)  Not NULL")
        coll.Add("Modified_Date", "datetime  Not NULL")
        coll.Add("Posted_By", "varchar(12) NULL")
        coll.Add("Posted_Date", "datetime NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BOOTH_COMMISSION_CALCULATION_Master", coll, "", True, False, "", "Document_Code", "Document_Date", True)
        coll = New Dictionary(Of String, String)()
        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Document_Code", "Varchar(30) Not null references TSPL_BOOTH_COMMISSION_CALCULATION_Master(Document_Code)")
        coll.Add("Line_No", "integer Not Null")
        coll.Add("Booth_Code", "varchar(12) not null References TSPL_CUSTOMER_MASTER(Cust_Code)")
        coll.Add("Item_Code", "Varchar(50) NOT NULL references TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Commission_Code", "Varchar(30) NOT NULL references TSPL_BOOTH_COMMISSION_MASTER(Document_Code)")
        coll.Add("Commission_PK_ID", "integer NOT NULL references TSPL_BOOTH_COMMISSION_DETAIL(PK_ID)")
        coll.Add("Min_Per_Day_Qty", "decimal(18,2) NOT NULL")
        coll.Add("Commission_UOM", "Varchar(30) NOT NULL")
        coll.Add("Total_Qty", "decimal(18,4) not null")
        coll.Add("Commission_Rate", "decimal(18,4) not null")
        coll.Add("Commission_Amt", "decimal(18,4) not null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BOOTH_COMMISSION_CALCULATION_DETAIL", coll, "", True, False, "TSPL_BOOTH_COMMISSION_CALCULATION_Master", "Document_Code", "", True)
        coll = New Dictionary(Of String, String)()
        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Document_Code", "Varchar(30) Not null references TSPL_BOOTH_COMMISSION_CALCULATION_Master(Document_Code)")
        coll.Add("Demand_Date", "Date not null")
        coll.Add("Booth_Code", "varchar(12) not null References TSPL_CUSTOMER_MASTER(Cust_Code)")
        coll.Add("Item_Code", "Varchar(50) NOT NULL references TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Qty", "decimal(18,2) NOT NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BOOTH_COMMISSION_Monthly_Detail", coll, "", True, False, "TSPL_BOOTH_COMMISSION_CALCULATION_Master", "Document_Code", "", True)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Function AllowToSave() As Boolean
        Try

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsBoothCommissionCalculationMaster()
                obj.Document_Code = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Month_Year = txtMonthYear.Value
                obj.Is_Mobile_User = IIf(chkMobileUser.Checked, 1, 0)
                obj.Remark = txtRemark.Text
                obj.Comment = txtComment.Text
                obj.Arr = GetTRData()
                obj.Arr_Monthly = GetMTRData()
                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.Document_Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function GetTRData() As List(Of clsBoothCommissionCalculationDetail)
        Dim Arr As New List(Of clsBoothCommissionCalculationDetail)
        Try
            For ii As Integer = 0 To gv1.RowCount - 1
                If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                    Dim objTr As New clsBoothCommissionCalculationDetail()
                    objTr.Line_No = clsCommon.myCdbl(gv1.Rows(ii).Cells(colLineNo).Value)
                    objTr.Booth_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colCustCode).Value)
                    objTr.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                    objTr.Commission_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colBCCode).Value)
                    objTr.Commission_PK_ID = clsCommon.myCdbl(gv1.Rows(ii).Cells(colBCPKID).Value)
                    objTr.Commission_UOM = clsCommon.myCstr(gv1.Rows(ii).Cells(colBCUOM).Value)
                    objTr.Min_Per_Day_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colBCMinQty).Value)
                    objTr.Total_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotalQty).Value)
                    objTr.Commission_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colBCRate).Value)
                    objTr.Commission_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCAmt).Value)
                    Arr.Add(objTr)
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


        Return Arr
    End Function
    Function GetMTRData() As List(Of clsBoothCommissionMonthlyDetail)
        Dim Arr As New List(Of clsBoothCommissionMonthlyDetail)
        Try

            Dim Monthyear As DateTime = clsCommon.GetPrintDate(txtMonthYear.Value)
            Dim FromDate As DateTime = New DateTime(Monthyear.Year, Monthyear.Month, 1)
            Dim ToDate As DateTime = New DateTime(Monthyear.Year, clsCommon.myCDate(txtMonthYear.Value).Month, DateTime.DaysInMonth(Monthyear.Year, Monthyear.Month))
            Dim SourceBy As String = ""
            If chkMobileUser.Checked Then
                SourceBy = "APP"
            Else
                SourceBy = "ERP"
            End If

            Dim strQry As String = "SELECT TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_DEMAND_BOOKING_DETAIL.Item_Code, TSPL_DEMAND_BOOKING_MASTER.Document_Date, SUM((TSPL_DEMAND_BOOKING_DETAIL.Qty*ICFCurrentUOM.Conversion_Factor)/ICFInLtr.Conversion_Factor) as DailyQty FROM TSPL_DEMAND_BOOKING_MASTER 
LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL  ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No 
LEFT JOIN TSPL_CUSTOMER_MASTER  ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
left join TSPL_ITEM_UOM_DETAIL as ICFCurrentUOM on ICFCurrentUOM.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code and ICFCurrentUOM.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code 
left join TSPL_ITEM_UOM_DETAIL as ICFInLtr on ICFInLtr.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code and ICFInLtr.UOM_Code='LTR'
WHERE CONVERT(date, TSPL_DEMAND_BOOKING_MASTER.Document_Date, 103) BETWEEN '" & clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") & "' AND '" & clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") & "' AND TSPL_DEMAND_BOOKING_MASTER.Posted = 1 AND TSPL_DEMAND_BOOKING_DETAIL.source_by = '" & SourceBy & "' 
GROUP BY TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_DEMAND_BOOKING_DETAIL.Unit_Code, TSPL_DEMAND_BOOKING_MASTER.Document_Date"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim objTr As New clsBoothCommissionMonthlyDetail()
                    objTr.Demand_Date = clsCommon.myCDate(dr("Document_Date"))
                    objTr.Booth_Code = clsCommon.myCstr(dr("Cust_Code"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Qty = clsCommon.myCstr(dr("DailyQty"))
                    Arr.Add(objTr)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


        Return Arr
    End Function
    Private Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Try
            isInsideLoadData = True
            Dim obj As New clsBoothCommissionCalculationMaster()
            obj = clsBoothCommissionCalculationMaster.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then

                LoadBlankGrid()
                AddNew()
                isNewEntry = False
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
                txtDocNo.Value = obj.Document_Code
                txtDate.Value = obj.Document_Date
                txtMonthYear.Value = obj.Month_Year
                txtRemark.Text = obj.Remark
                txtComment.Text = obj.Comment
                chkMobileUser.Checked = IIf(obj.Is_Mobile_User = 1, True, False)


                Dim sl As Integer = 1
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsBoothCommissionCalculationDetail In obj.Arr
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustCode).Value = objTr.Booth_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustName).Value = objTr.Booth_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBCCode).Value = objTr.Commission_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBCPKID).Value = objTr.Commission_PK_ID
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBCUOM).Value = objTr.Commission_UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBCMinQty).Value = objTr.Min_Per_Day_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalQty).Value = objTr.Total_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBCRate).Value = objTr.Commission_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCAmt).Value = objTr.Commission_Amt
                        gv1.Rows.AddNew()
                    Next
                    'GV1.Rows.AddNew()
                End If
                If obj.Arr_Monthly IsNot Nothing AndAlso obj.Arr_Monthly.Count > 0 Then
                    LoadGV2(obj.Month_Year, chkMobileUser.Checked)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try

    End Sub
    Private Sub LoadGV2(ByVal Monyr As Date, ByVal isMobileUser As Boolean)
        Try
            Dim Monthyear As DateTime = clsCommon.GetPrintDate(Monyr)
            Dim FromDate As DateTime = New DateTime(Monthyear.Year, Monthyear.Month, 1)
            Dim ToDate As DateTime = New DateTime(Monthyear.Year, clsCommon.myCDate(txtMonthYear.Value).Month, DateTime.DaysInMonth(Monthyear.Year, Monthyear.Month))
            Dim SourceBy As String = ""
            If isMobileUser Then
                SourceBy = "APP"
            Else
                SourceBy = "ERP"
            End If

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
      AND m.Posted = 1 AND d.source_by = ''" & SourceBy & "'' 
    GROUP BY c.Cust_Code, d.Item_Code,d.Unit_Code, m.Document_Date
)
SELECT Cust_Code, Item_Code, ' + @cols + ', TotalQty
FROM DatePivot
PIVOT (
    SUM(DailyQty) FOR DateCol IN (' + @cols + ')
) pvt
ORDER BY Cust_Code, Item_Code'; EXEC sp_executesql @sql;"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            gv2.DataSource = Nothing
            gv2.Rows.Clear()
            gv2.Columns.Clear()
            gv2.GroupDescriptors.Clear()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                'RadPageView1.SelectedPage = RadPageViewPage7
                gv2.MasterTemplate.SummaryRowsBottom.Clear()
                gv2.DataSource = dt
                gv2.AutoExpandGroups = True
                SetGridFormation()
                gv2.ShowGroupPanel = False
                gv2.ShowRowHeaderColumn = False
                gv2.AllowAddNewRow = False
                gv2.AllowDeleteRow = False
                gv2.EnableFiltering = True
                gv2.ShowFilteringRow = True
                gv2.BestFitColumns()
                btnGo.Enabled = False
            Else
                Throw New Exception("No Data Found to Display")
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Private Sub PostData()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtDocNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsBoothCommissionCalculationMaster.PostData(clsCommon.myCstr(txtDocNo.Value))
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(clsCommon.myCstr(txtDocNo.Value), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("You Cannot Delete Record")
                Exit Sub
            End If
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsBoothCommissionCalculationMaster.DeleteData(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnreverse_Click(sender As Object, e As EventArgs) Handles btnreverse.Click

    End Sub
    Private Sub ReverseUnpostData()
        Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
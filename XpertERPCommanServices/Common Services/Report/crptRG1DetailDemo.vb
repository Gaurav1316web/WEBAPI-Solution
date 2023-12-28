Imports common
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Public Class frmRG1Demo
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim IsExportToExcel As Boolean = False


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.CrptRG1Detail1)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")
        'End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmRG1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub frmRG1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave

    End Sub


    Private Sub CrptRG1Detail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'frmDate.Value = System.DateTime.Now()
        'toDate.Value = System.DateTime.Now()
        frmDate.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE())
        toDate.Value = clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE())

        LoadLocation()
        chkLocAll.IsChecked = True
        LoadItems()
        chkItmAll.IsChecked = True
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ' ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")

        btnExportToExcel.Visible = False


    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "RG-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
    Sub LoadLocation()
        Dim qry As String = " select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type='Physical' and Excisable='T' "
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLoc.ValueMember = "Location_Code"
        cbgLoc.DisplayMember = "Location_Desc"
    End Sub
    Sub LoadItems()
        Dim qry11 As String = " select Item_Code,Item_Desc  from TSPL_ITEM_MASTER where Item_Type ='F' "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry11)
        cbgItem.ValueMember = "Item_Code"
        cbgItem.DisplayMember = "Item_Desc"
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If chkRG1MRPWise.Checked = True Then
                printMRPWISE()
            Else
                print()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        Try
            IsExportToExcel = True
            printMRPWISE()
            IsExportToExcel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub print()
        Dim startDate As String = clsCommon.GetPrintDate(frmDate.Value, "dd-MMM-yyyy hh:mm tt")
        Dim EndDate As String = clsCommon.GetPrintDate(toDate.Value, "dd-MMM-yyyy hh:mm tt")
        Dim startTime As String = frmDate.Value.ToString("hh:mm tt")
        Dim EndTime As String = toDate.Value.ToString("hh:mm tt")
        Dim dtData As DataTable = Nothing

        '************ DtFinal : Final DataTable
        Dim dtFinal As DataTable = New DataTable()
        dtFinal.Columns.Add("Sale_Invoice_No", GetType(String))
        dtFinal.Columns.Add("Sale_Invoice_Date", GetType(String))
        dtFinal.Columns.Add("Opening Qty", GetType(Decimal))
        dtFinal.Columns.Add("Qty", GetType(Decimal))
        dtFinal.Columns.Add("Total_Assessable_Amt", GetType(Decimal))
        dtFinal.Columns.Add("Tax1_Rate", GetType(String))
        dtFinal.Columns.Add("Tax2_Rate", GetType(String))
        dtFinal.Columns.Add("Tax3_Rate", GetType(String))
        dtFinal.Columns.Add("Tax1_Amt", GetType(Decimal))
        dtFinal.Columns.Add("Tax2_Amt", GetType(Decimal))
        dtFinal.Columns.Add("Tax3_Amt", GetType(Decimal))
        dtFinal.Columns.Add("ManuQty", GetType(Decimal))
        dtFinal.Columns.Add("ClosingQty", GetType(Decimal))
        dtFinal.Columns.Add("MRP_Amt", GetType(Decimal))
        dtFinal.Columns.Add("Item_Code", GetType(String))
        dtFinal.Columns.Add("Item_Desc", GetType(String))
        dtFinal.Columns.Add("StartDate", GetType(String))
        dtFinal.Columns.Add("End Date", GetType(String))
        dtFinal.Columns.Add("Start Time", GetType(String))
        dtFinal.Columns.Add("End Time", GetType(String))
        dtFinal.Columns.Add("compname", GetType(String))
        dtFinal.Columns.Add("address", GetType(String))
        dtFinal.Columns.Add("LocFilter", GetType(String))
        Dim DrFinal As DataRow = dtFinal.NewRow()
        '*********** End DataTable
        Dim Dics As New Dictionary(Of String, String)
        Dim OpenBal As Decimal = 0
        Dim CloseBal As Decimal = 0

         
        Dim Address As String
        If chkLocSelect.IsChecked AndAlso cbgLoc.CheckedValue.Count = 1 Then
            Address = "(select TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_LOCATION_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_LOCATION_MASTER.State) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end  from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Location_Code  =(xxxx.Location ) )   "
        Else
            Address = "(select TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end from TSPL_COMPANY_MASTER where TSPL_COMPANY_MASTER.Comp_Code =(xxxx.Comp_Code) )"
        End If
        Dim LocFilter As String = ""
        If cbgLoc.CheckedValue.Count > 0 Then
            LocFilter = clsCommon.GetMulcallString(cbgLoc.CheckedValue)
            LocFilter = LocFilter.Replace("'", "")
        End If
        Dim Qry As String = " select   Sale_Invoice_No, Sale_Invoice_Date, '" + startDate + "' AS StartDate, '" + EndDate + "' AS [End Date], '" + startTime + "' AS [Start Time]," & _
                          " '" + EndTime + "' AS [End Time],'" + LocFilter + "' as LocFilter,  (xxxx.op) as [Opening Qty], (xxxx.Item_Code ) as Item_Code, ( TSPL_ITEM_MASTER.Item_Desc) as [Item_Desc] ," & _
                          " (Qty ) AS Qty, (Item_Assessable_Rate)  AS Item_Assessable_Rate , (TAX1_Rate) as TAX1_Rate, (TAX1_Amt)  as [TAX1_Amt]," & _
                          "  (TAX2_Rate) as TAX2_Rate, (TAX2_Amt )  as [TAX2_Amt],(TAX3_Rate) as TAX3_Rate, (TAX3_Amt) as [TAX3_Amt],  ( MRP_Amt) as  MRP_Amt" & _
                          " ,(Location) as Location  ,(ManuQty) as ManuQty,(ClosingQty) as ClosingQty, (select Comp_Name  from TSPL_COMPANY_MASTER where TSPL_COMPANY_MASTER.Comp_Code=(xxxx.Comp_Code)  ) as compname," + Address + " as address  from  (select *  from   (  "

        'Qry += " Select  TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,0 as op,TSPL_SALE_INVOICE_HEAD .Comp_Code,  TSPL_SALE_INVOICE_HEAD.Date_Time_Removal as [Sale_Invoice_Date], TSPL_SALE_INVOICE_DETAIL.Item_Code,  TSPL_SALE_INVOICE_DETAIL.Item_Desc ," & _
        '" (SELECT     CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB' THEN     TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / (select Conversion_Factor " & _
        '" from TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB') ELSE TSPL_SALE_INVOICE_DETAIL.Invoice_Qty END AS Expr1)" & _
        '" AS Qty, CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB' THEN TSPL_SALE_INVOICE_DETAIL.Item_Assessable_Rate * (select Conversion_Factor " & _
        '" from TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB') ELSE TSPL_SALE_INVOICE_DETAIL.Item_Assessable_Rate " & _
        '" END AS Item_Assessable_Rate ,    TSPL_SALE_INVOICE_DETAIL.TAX1_Rate, TSPL_SALE_INVOICE_DETAIL.TAX1_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as [TAX1_Amt]," & _
        '" TSPL_SALE_INVOICE_DETAIL.TAX2_Rate,  TSPL_SALE_INVOICE_DETAIL.TAX2_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty    as [TAX2_Amt],     TSPL_SALE_INVOICE_DETAIL.TAX3_Rate," & _
        '" TSPL_SALE_INVOICE_DETAIL.TAX3_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as [TAX3_Amt], TSPL_SALE_INVOICE_HEAD.Date_Time_Removal," & _
        '" case when TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB' then   TSPL_SALE_INVOICE_DETAIL.MRP_Amt else  TSPL_SALE_INVOICE_DETAIL.MRP_Amt/ (select Conversion_Factor  from" & _
        '" TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB') end as MRP_Amt ,TSPL_SALE_INVOICE_HEAD.Location  ," & _
        '" 0 as [ManuQty],0 as ClosingQty ,Invoice_Qty  FROM  TSPL_SALE_INVOICE_HEAD INNER JOIN TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No =  " & _
        '" TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN TSPL_LOCATION_MASTER ON TSPL_SALE_INVOICE_HEAD.Location =   TSPL_LOCATION_MASTER.Location_Code " & _
        '" where TSPL_SALE_INVOICE_HEAD.Is_Post ='Y'   "
        Qry += " SELECT    TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Sale_Invoice_No,0 as op,TSPL_SD_SALE_INVOICE_HEAD .Comp_Code,  TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [Sale_Invoice_Date], TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,  TSPL_ITEM_MASTER.Item_Desc ,  TSPL_SD_SALE_INVOICE_DETAIL.Qty  AS Qty,   case when TSPL_SD_SALE_INVOICE_DETAIL.Qty=0 then 0 else TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Base_Amt/TSPL_SD_SALE_INVOICE_DETAIL.Qty end AS Item_Assessable_Rate ,    TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt as [TAX1_Amt], TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate,  TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt as [TAX2_Amt],     TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt as [TAX3_Amt], TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date_Time_Removal,  TSPL_SD_SALE_INVOICE_DETAIL.MRP  as MRP_Amt ,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as Location  , 0 as [ManuQty],0 as ClosingQty ,TSPL_SD_SALE_INVOICE_DETAIL.Qty as Invoice_Qty  "
        Qry += " FROM TSPL_SD_SALE_INVOICE_HEAD "
        Qry += " INNER JOIN TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code =   TSPL_SD_SALE_INVOICE_DETAIL.Document_Code "
        Qry += " INNER JOIN TSPL_LOCATION_MASTER ON TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =   TSPL_LOCATION_MASTER.Location_Code  "
        Qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code"
        Qry += " where TSPL_SD_SALE_INVOICE_HEAD.Status=1"
        Qry += Environment.NewLine + " union  all " + Environment.NewLine
        Qry += " select TSPL_ADJUSTMENT_HEADER.Adjustment_No  , 0, TSPL_ADJUSTMENT_HEADER.Comp_Code  ,convert(dateTIME,TSPL_ADJUSTMENT_HEADER.EntryDateTime,103) ," & _
                          " TSPL_ADJUSTMENT_DETAIL.Item_Code,TSPL_ADJUSTMENT_DETAIL.Item_Description ,0 as qty,0 as Total_Assessable_Amt, 0 ,0, 0 ,0, 0,0, '' AS Date_Time_Removal," & _
                          " case when TSPL_ADJUSTMENT_DETAIL.Unit_code = 'FB' then  TSPL_ADJUSTMENT_DETAIL.mrp else  TSPL_ADJUSTMENT_DETAIL.mrp/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL" & _
                          " where Item_Code =TSPL_ADJUSTMENT_DETAIL.Item_Code and UOM_Code ='FB') end as MRP_Amt,Location_Code, " & _
                          " case when Adjustment_Type='BI' then (Case When TSPL_ADJUSTMENT_DETAIL.Unit_Code='FB' Then TSPL_ADJUSTMENT_DETAIL.Item_Quantity/Conversion_Factor else TSPL_ADJUSTMENT_DETAIL.Item_Quantity End) Else Case when Adjustment_Type='BD' then (Case When TSPL_ADJUSTMENT_DETAIL.Unit_Code='FB' Then (TSPL_ADJUSTMENT_DETAIL.Item_Quantity/Conversion_Factor)*-1 Else TSPL_ADJUSTMENT_DETAIL.Item_Quantity*-1  end) end End  as [ManuQty],0 as ClosingQty ," & _
                          " TSPL_ADJUSTMENT_DETAIL.Item_Quantity  from TSPL_ADJUSTMENT_DETAIL inner join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_DETAIL.Adjustment_No =TSPL_ADJUSTMENT_HEADER .Adjustment_No " & _
                          " LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code " & _
                          " where Adjustment_Type in ('BI','BD') and TSPL_ADJUSTMENT_HEADER.ItemType='FM'   and TSPL_ADJUSTMENT_HEADER.Posted='Y' and TSPL_ADJUSTMENT_DETAIL.Unit_Code not in ('EC','EB','SH') AND TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' ) as final  " & _
                          "  )" & _
                          " as xxxx  inner join TSPL_ITEM_MASTER on xxxx.Item_Code =TSPL_ITEM_MASTER.Item_Code where " & _
                          " (Sale_Invoice_Date >=  convert(datetime,'" + startDate + "',103)) AND " & _
                          " (Sale_Invoice_Date <= convert(datetime,'" + EndDate + "',103)) "

        If chkLocSelect.IsChecked Then
            If cbgLoc.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Atleast One Location Code.", Me.Text)
                Return
            End If
            Qry += " and xxxx.Location in  (" + clsCommon.GetMulcallString(cbgLoc.CheckedValue) + ")  "
        Else
            Qry += " and xxxx.Location in   (" + clsCommon.GetMulcallString(cbgLoc.AllValue) + ")  "
        End If


        If chkItmSelect.IsChecked Then
            If cbgItem.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Atleast One Item Code.", Me.Text)
                Return
            End If
            Qry += " and xxxx.Item_Code in  (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")  "
        End If

        'Qry = Qry + " and xxxx.Item_Code in (select Item_Code  from TSPL_ITEM_MASTER where Item_Type ='F') "
        Qry = Qry + " order by xxxx.Item_Code  ,xxxx.Sale_Invoice_Date  "

        dtData = clsDBFuncationality.GetDataTable(Qry)

        For Each dr As DataRow In dtData.Rows
            DrFinal = dtFinal.NewRow()
            Dim itmCode As String = dr("Item_Code").ToString() + dr("MRP_Amt").ToString()
            Dim Qty As String = dr("Qty").ToString()
            DrFinal.Item("Sale_Invoice_Date") = Format(dr("Sale_Invoice_Date"), "dd/MM/yyyy hh:mm tt") '+ " " + Format(dr("Date_Time_Removal"), "hh:mm")
            DrFinal.Item("Opening Qty") = OpenBal
            DrFinal.Item("Qty") = Math.Round(CDec(dr("Qty").ToString()), 2)
            DrFinal.Item("Item_Code") = dr("Item_Code").ToString()
            DrFinal.Item("Item_Desc") = dr("Item_Desc").ToString()
            DrFinal.Item("Tax1_Rate") = dr("Tax1_Rate").ToString()
            DrFinal.Item("Tax2_Rate") = dr("Tax2_Rate").ToString()
            DrFinal.Item("Tax3_Rate") = dr("Tax3_Rate").ToString()
            DrFinal.Item("Tax1_Amt") = clsCommon.myCdbl(dr("Tax1_Amt").ToString())
            DrFinal.Item("Tax2_Amt") = clsCommon.myCdbl(dr("Tax2_Amt").ToString())
            DrFinal.Item("Tax3_Amt") = clsCommon.myCdbl(dr("Tax3_Amt").ToString())
            DrFinal.Item("ManuQty") = clsCommon.myCdbl(dr("ManuQty").ToString())
            DrFinal.Item("MRP_Amt") = clsCommon.myCdbl(dr("MRP_Amt").ToString())
            DrFinal.Item("StartDate") = dr("StartDate").ToString()
            DrFinal.Item("End Date") = dr("End Date").ToString()
            DrFinal.Item("start Time") = dr("start Time").ToString()
            DrFinal.Item("compname") = dr("compname").ToString()
            DrFinal.Item("address") = dr("address").ToString()
            DrFinal.Item("LocFilter") = dr("LocFilter").ToString()
            DrFinal.Item("End Time") = dr("End Time").ToString()
            DrFinal.Item("Sale_Invoice_No") = dr("Sale_Invoice_No").ToString()
            DrFinal.Item("Total_Assessable_Amt") = Math.Round(CDec(dr("Item_Assessable_Rate").ToString()), 2)
            CloseBal = 0
            DrFinal.Item("ClosingQty") = CloseBal
            dtFinal.Rows.Add(DrFinal)
        Next
        dtFinal.AcceptChanges()
        Dim CrptName As String = ""
        If chkSummary.Checked = True Then
            CrptName = "crptRG1Summary"
        Else
            CrptName = "crptRG1DetailsDemo"
        End If
        Dim fRMcrys As New frmCrystalReportViewer
        fRMcrys.funreport(CrystalReportFolder.CommonServices, dtFinal, CrptName, "Excise Report")
    End Sub
    Sub printMRPWISE()
        Try
            Dim startDate As String = clsCommon.GetPrintDate(frmDate.Value, "dd-MMM-yyyy hh:mm tt")
            Dim EndDate As String = clsCommon.GetPrintDate(toDate.Value, "dd-MMM-yyyy hh:mm tt")
            Dim startTime As String = frmDate.Value.ToString("hh:mm tt")
            Dim EndTime As String = toDate.Value.ToString("hh:mm tt")
            Dim dtData As DataTable = Nothing

            '************ DtFinal : Final DataTable
            Dim dtFinal As DataTable = New DataTable()
            dtFinal.Columns.Add("WHQty", GetType(Decimal))
            dtFinal.Columns.Add("Item_Code", GetType(String))
            dtFinal.Columns.Add("Item_Desc", GetType(String))
            dtFinal.Columns.Add("MRP_Amt", GetType(Decimal))
            dtFinal.Columns.Add("Opening Qty", GetType(Decimal))
            dtFinal.Columns.Add("ManuQty", GetType(Decimal))
            dtFinal.Columns.Add("TotalQty", GetType(Decimal))
            dtFinal.Columns.Add("Qty", GetType(Decimal))
            dtFinal.Columns.Add("ClosingQty", GetType(Decimal))
            dtFinal.Columns.Add("Total_Assessable_Amt", GetType(Decimal))
            dtFinal.Columns.Add("Total_Assessable_Value", GetType(Decimal))
            dtFinal.Columns.Add("Tax1_Rate", GetType(String))
            dtFinal.Columns.Add("Tax1_Amt", GetType(Decimal))
            dtFinal.Columns.Add("Tax2_Rate", GetType(String))
            dtFinal.Columns.Add("Tax2_Amt", GetType(Decimal))
            dtFinal.Columns.Add("Tax3_Rate", GetType(String))
            dtFinal.Columns.Add("Tax3_Amt", GetType(Decimal))
            dtFinal.Columns.Add("StartDate", GetType(String))
            dtFinal.Columns.Add("End Date", GetType(String))
            dtFinal.Columns.Add("Start Time", GetType(String))
            dtFinal.Columns.Add("End Time", GetType(String))
            dtFinal.Columns.Add("compname", GetType(String))
            dtFinal.Columns.Add("address", GetType(String))
            dtFinal.Columns.Add("LocFilter", GetType(String))
            dtFinal.Columns.Add("Sale_Invoice_Date", GetType(String))
            Dim DrFinal As DataRow = dtFinal.NewRow()
            '*********** End DataTable
            Dim Dics As New Dictionary(Of String, String)
            Dim OpenBal As Decimal = 0
            Dim CloseBal As Decimal = 0

            Dim Address As String
            If chkLocSelect.IsChecked AndAlso cbgLoc.CheckedValue.Count = 1 Then
                Address = "(select TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_LOCATION_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_LOCATION_MASTER.State) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end  from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Location_Code  =max(xxxx.Location ) )   "
            Else
                Address = "(select TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end from TSPL_COMPANY_MASTER where TSPL_COMPANY_MASTER.Comp_Code =max(xxxx.Comp_Code) )"

            End If
            Dim LocFilter As String = ""
            If cbgLoc.CheckedValue.Count > 0 Then
                LocFilter = clsCommon.GetMulcallString(cbgLoc.CheckedValue)
                LocFilter = LocFilter.Replace("'", "")
            End If
            Dim Qry As String = " select   SUM(WHQty) as WHQty,'" + startDate + "' AS StartDate, '" + EndDate + "' AS [End Date], '" + startTime + "' AS [Start Time]," & _
                                " '" + EndTime + "' AS [End Time],'" + LocFilter + "' as LocFilter,  SUM(xxxx.op) as [Opening Qty], (xxxx.Item_Code ) as Item_Code, MAX( TSPL_ITEM_MASTER.Item_Desc) as [Item_Desc] ," & _
                                " SUM(Qty ) AS Qty, max(Item_Assessable_Rate)  AS Item_Assessable_Rate , max(TAX1_Rate) as TAX1_Rate, sum(TAX1_Amt)  as [TAX1_Amt]," & _
                                "  max(TAX2_Rate) as TAX2_Rate, sum(TAX2_Amt )  as [TAX2_Amt],max(TAX3_Rate) as TAX3_Rate, sum(TAX3_Amt) as [TAX3_Amt],  MAX( MRP_Amt) as  MRP_Amt" & _
                                " ,max(Location) as Location  ,sum(ManuQty) as ManuQty,sum(ClosingQty) as ClosingQty , (select Comp_Name  from TSPL_COMPANY_MASTER where TSPL_COMPANY_MASTER.Comp_Code=max(xxxx.Comp_Code)  ) as compname, " & _
                                " " + Address + " as address  from  (select *  from   (  "
            Qry += " Select  0 as WHQty,0 as op,TSPL_SD_SALE_INVOICE_HEAD .Comp_Code,  TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [Sale_Invoice_Date], TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,  TSPL_ITEM_MASTER.Item_Desc , TSPL_SD_SALE_INVOICE_DETAIL.Qty,case when ISNULL( TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)=0 then 0 else  TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Base_Amt/TSPL_SD_SALE_INVOICE_DETAIL.Qty end as  Item_Assessable_Rate ,    TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt as [TAX1_Amt], TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate,  TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt as [TAX2_Amt],     TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt as [TAX3_Amt], TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date_Time_Removal,  TSPL_SD_SALE_INVOICE_DETAIL.MRP as MRP_Amt ,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as Location  , 0 as [ManuQty],0 as ClosingQty ,TSPL_SD_SALE_INVOICE_DETAIL.Qty as Invoice_Qty"
            Qry += " FROM TSPL_SD_SALE_INVOICE_HEAD "
            Qry += " INNER JOIN TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code =   TSPL_SD_SALE_INVOICE_DETAIL.Document_Code"
            Qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code"
            Qry += " INNER JOIN TSPL_LOCATION_MASTER ON TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =   TSPL_LOCATION_MASTER.Location_Code  "
            Qry += " where TSPL_SD_SALE_INVOICE_HEAD.Status =1  "

            ''Qry += "Select  0 as WHQty,0 as op,TSPL_SALE_INVOICE_HEAD .Comp_Code,  TSPL_SALE_INVOICE_HEAD.Date_Time_Removal as [Sale_Invoice_Date], TSPL_SALE_INVOICE_DETAIL.Item_Code,  TSPL_SALE_INVOICE_DETAIL.Item_Desc ," & _
            '" (SELECT     CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB' THEN     TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / (select Conversion_Factor " & _
            '" from TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB') ELSE TSPL_SALE_INVOICE_DETAIL.Invoice_Qty END AS Expr1)" & _
            '" AS Qty, CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB' THEN TSPL_SALE_INVOICE_DETAIL.Item_Assessable_Rate * (select Conversion_Factor " & _
            '" from TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB') ELSE TSPL_SALE_INVOICE_DETAIL.Item_Assessable_Rate " & _
            '" END AS Item_Assessable_Rate ,    TSPL_SALE_INVOICE_DETAIL.TAX1_Rate, TSPL_SALE_INVOICE_DETAIL.TAX1_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as [TAX1_Amt]," & _
            '" TSPL_SALE_INVOICE_DETAIL.TAX2_Rate,  TSPL_SALE_INVOICE_DETAIL.TAX2_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty    as [TAX2_Amt],     TSPL_SALE_INVOICE_DETAIL.TAX3_Rate," & _
            '" TSPL_SALE_INVOICE_DETAIL.TAX3_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as [TAX3_Amt], TSPL_SALE_INVOICE_HEAD.Date_Time_Removal," & _
            '" case when TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB' then   TSPL_SALE_INVOICE_DETAIL.MRP_Amt else  TSPL_SALE_INVOICE_DETAIL.MRP_Amt/ (select Conversion_Factor  from" & _
            '" TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB') end as MRP_Amt ,TSPL_SALE_INVOICE_HEAD.Location  ," & _
            '" 0 as [ManuQty],0 as ClosingQty ,Invoice_Qty  FROM  TSPL_SALE_INVOICE_HEAD INNER JOIN TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No =  " & _
            '" TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN TSPL_LOCATION_MASTER ON TSPL_SALE_INVOICE_HEAD.Location =   TSPL_LOCATION_MASTER.Location_Code " & _
            '" where TSPL_SALE_INVOICE_HEAD.Is_Post ='Y'  "
            Qry += Environment.NewLine + " union all " + Environment.NewLine
            'Qry += "SELECT  (Leakage_Qty + Breakage_Qty + Shortage_Qty) as WHQty,0,hd.Comp_Code  ,hd.Document_Date , " & _
            '                    "dl.Item_Code ,dl.Item_Description ,0 as Item_Qty , 0 as Assessable_Amt ,0 as TAX1_Rate ,0 as TAX1_Amt  , " & _
            '                    "0 as TAX2_Rate,0 as TAX2_Amt , 0 as TAX3_Rate,0 as TAX3_Amt , hd.Document_Date AS Date_Time_Removal , " & _
            '                    "case when dl.Unit_Code  = 'FB' then   dl.MRP else  dl.MRP/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where " & _
            '                    "Item_Code =dl.Item_Code and UOM_Code ='FB') end as MRP_Amt  , hd.Loc_Code ,0 as [ManuQty],0 as ClosingQty  , " & _
            '                    "0 as Item_Qty  from TSPL_WH_BREAKAGE_HEAD hd inner join TSPL_WH_BREAKAGE_DETAIL dl on hd.Document_No=dl.Document_No WHERE " & _
            '                    "hd.Is_Post=1  and dl.Unit_Code not in ('EC','EB','SH')  " & _
            '                    " union  all  "
            'Qry += "SELECT  0 as WHQty,0,hd.Comp_Code  ,hd.EntryDateTime ,  dl.Item_Code ,dl.Item_Desc ,dl.Item_Qty ," & _
            '                    " dl.Assessable_Amt ,dl.TAX1_Rate ,dl.TAX1_Amt  ,dl.TAX2_Rate,dl.TAX2_Amt ,  dl.TAX3_Rate,dl.TAX3_Amt , hd.Date_Time_Removal AS Date_Time_Removal ," & _
            '                    " case when dl.Uom  = 'FB' then   dl.MRP else  dl.MRP/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code =dl.Item_Code and UOM_Code ='FB') end as MRP_Amt  ," & _
            '                    " hd.From_Location ,0 as [ManuQty],0 as ClosingQty  ,dl.Item_Qty  from TSPL_TRANSFER_HEAD hd inner join TSPL_TRANSFER_DETAIL dl on hd.Transfer_No=dl.Transfer_No" & _
            '                    " WHERE hd.Item_Type <>'Empty' and hd.Post='Y'  and hd.Item_Type ='Full'  " & _
            '                    " union all "
            Qry += " select 0 as WHQty,0, TSPL_ADJUSTMENT_HEADER.Comp_Code,convert(dateTIME,TSPL_ADJUSTMENT_HEADER.EntryDateTime,103) ," & _
                                " TSPL_ADJUSTMENT_DETAIL.Item_Code,TSPL_ADJUSTMENT_DETAIL.Item_Description ,0 as qty,0 as Total_Assessable_Amt, 0 ,0, 0 ,0, 0,0, '' AS Date_Time_Removal," & _
                                " case when TSPL_ADJUSTMENT_DETAIL.Unit_code = 'FB' then  TSPL_ADJUSTMENT_DETAIL.mrp else  TSPL_ADJUSTMENT_DETAIL.mrp/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL" & _
                                " where Item_Code =TSPL_ADJUSTMENT_DETAIL.Item_Code and UOM_Code ='FB') end as MRP_Amt,Location_Code, " & _
                                " case when Adjustment_Type='BI' then (Case When TSPL_ADJUSTMENT_DETAIL.Unit_Code='FB' Then TSPL_ADJUSTMENT_DETAIL.Item_Quantity/Conversion_Factor else TSPL_ADJUSTMENT_DETAIL.Item_Quantity End) Else Case when Adjustment_Type='BD' then (Case When TSPL_ADJUSTMENT_DETAIL.Unit_Code='FB' Then (TSPL_ADJUSTMENT_DETAIL.Item_Quantity/Conversion_Factor)*-1 Else TSPL_ADJUSTMENT_DETAIL.Item_Quantity*-1  end) end End as [ManuQty],0 as ClosingQty ," & _
                                " TSPL_ADJUSTMENT_DETAIL.Item_Quantity  from TSPL_ADJUSTMENT_DETAIL inner join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_DETAIL.Adjustment_No =TSPL_ADJUSTMENT_HEADER .Adjustment_No " & _
                                " LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code " & _
                                " where Adjustment_Type in ('BI','BD') and TSPL_ADJUSTMENT_HEADER.ItemType='FM'   and " & _
                                " TSPL_ADJUSTMENT_HEADER.Posted='Y' and TSPL_ADJUSTMENT_DETAIL.Unit_Code not in ('EC','EB','SH') AND  " & _
                                " TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'   ) as final  " & _
                                " union all  "
            Qry += " select   0 as WHQty, SUM(qty),max(Comp_Code) ,'" + startDate + "' ,xxx.Item_Code,'' as itemdesc ,0 as qty,0 as Total_Assessable_Amt, 0 ,0, 0 ,0, 0,0," & _
                                " '' AS Date_Time_Removal,MRP_Amt  as MRP_Amt,xxx.Location , 0  as [ManuQty],0 as ClosingQty ,0  from ( "
            Qry += " SELECT  TSPL_SD_SALE_INVOICE_DETAIL.MRP as MRP_Amt,  TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as Location, TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Sale_Invoice_Date,  TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, -1* (TSPL_SD_SALE_INVOICE_DETAIL.Qty)  AS Qty, TSPL_SD_SALE_INVOICE_HEAD.Comp_Code "
            Qry += " FROM TSPL_SD_SALE_INVOICE_HEAD "
            Qry += " INNER JOIN    TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_SD_SALE_INVOICE_HEAD.Document_Date =  TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE "
            Qry += " INNER JOIN    TSPL_LOCATION_MASTER ON TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =   TSPL_LOCATION_MASTER.Location_Code "
            Qry += " where   (convert(datetime,Document_Date,103) < '" + startDate + "') and   TSPL_SD_SALE_INVOICE_HEAD.Status=1"

            'Qry += "SELECT case when TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB' " & _
            '                    " then   TSPL_SALE_INVOICE_DETAIL.MRP_Amt else  TSPL_SALE_INVOICE_DETAIL.MRP_Amt/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where " & _
            '                    " Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB') end as MRP_Amt,  TSPL_SALE_INVOICE_HEAD.Location, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, " & _
            '                    " TSPL_SALE_INVOICE_DETAIL.Item_Code, -1*  (SELECT     CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB' THEN   " & _
            '                    " TSPL_SALE_INVOICE_DETAIL.Invoice_Qty /  (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB')" & _
            '                    " ELSE TSPL_SALE_INVOICE_DETAIL.Invoice_Qty END AS Expr1)  AS Qty, TSPL_SALE_INVOICE_HEAD.Comp_Code FROM  TSPL_SALE_INVOICE_HEAD INNER JOIN    TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " & _
            '                    " TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN    TSPL_LOCATION_MASTER ON TSPL_SALE_INVOICE_HEAD.Location =   TSPL_LOCATION_MASTER.Location_Code" & _
            '                    " where   (convert(datetime,Date_Time_Removal,103) < '" + startDate + "') and   TSPL_SALE_INVOICE_HEAD.Is_Post='Y' "
            Qry += Environment.NewLine + " union all " + Environment.NewLine
            'Qry += " SELECT case when dl.Uom  = 'FB' then   dl.MRP else  dl.MRP/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL " & _
            '                    " where Item_Code =dl.Item_Code and UOM_Code ='FB') end as MRP_Amt, hd.From_Location,  hd.Posting_Date ,  dl.Item_Code ,dl.Item_Qty *-1 ,hd.Comp_Code   " & _
            '                    " from TSPL_TRANSFER_HEAD hd inner join TSPL_TRANSFER_DETAIL dl on hd.Transfer_No=dl.Transfer_No where " & _
            '                    " (convert(datetime,hd.EntryDateTime,103) < '" + startDate + "') and  hd.Post='Y' and hd.Item_Type ='Full' " & _
            '                    " union all "
            'Qry += "SELECT case when dl.Unit_Code  = 'FB' then   dl.MRP else  dl.MRP/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL  where Item_Code =dl.Item_Code and UOM_Code ='FB') end as MRP_Amt, " & _
            '                    "hd.Loc_Code,  hd.Posting_Date ,  dl.Item_Code ,(Leakage_Qty + Breakage_Qty + Shortage_Qty) *-1 ,hd.Comp_Code " & _
            '                    "from TSPL_WH_BREAKAGE_HEAD hd inner join TSPL_WH_BREAKAGE_DETAIL dl on hd.Document_No=dl.Document_No where " & _
            '                    "(convert(datetime,hd.Document_Date,103) < '" + startDate + "') and hd.Is_Post=1  and dl.Unit_Code not in ('EC','EB','SH')  " & _
            '                    " union all "
            Qry += "Select  case when TSPL_ADJUSTMENT_DETAIL.Unit_code = 'FB' then  TSPL_ADJUSTMENT_DETAIL.mrp else " & _
                                " TSPL_ADJUSTMENT_DETAIL.mrp/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ADJUSTMENT_DETAIL.Item_Code and UOM_Code ='FB') end as MRP_Amt ," & _
                                " TSPL_ADJUSTMENT_HEADER.Loc_Code, convert(datetime,TSPL_ADJUSTMENT_HEADER.Posting_Date,103) ,TSPL_ADJUSTMENT_DETAIL.Item_Code,case when Adjustment_Type='BI' then TSPL_ADJUSTMENT_DETAIL.Item_Quantity " & _
                                " when Adjustment_Type='BD'  then TSPL_ADJUSTMENT_DETAIL.Item_Quantity*-1 end ,TSPL_ADJUSTMENT_HEADER.Comp_Code     from TSPL_ADJUSTMENT_DETAIL inner join TSPL_ADJUSTMENT_HEADER on " & _
                                " TSPL_ADJUSTMENT_DETAIL.Adjustment_No =TSPL_ADJUSTMENT_HEADER .Adjustment_No   where Adjustment_Type in ('BI','BD') and " & _
                                " TSPL_ADJUSTMENT_HEADER.ItemType='FM' and TSPL_ADJUSTMENT_HEADER.Posted='Y'   and convert(datetime,TSPL_ADJUSTMENT_HEADER.EntryDateTime,103) < convert(datetime, '" + startDate + "',103) " & _
                                " and TSPL_ADJUSTMENT_DETAIL.Unit_Code not in ('EC','EB','SH')     ) as xxx group by xxx.Item_Code ,xxx.Location,xxx.MRP_Amt      )" & _
                                " as xxxx  inner join TSPL_ITEM_MASTER on xxxx.Item_Code =TSPL_ITEM_MASTER.Item_Code where " & _
                                " (Sale_Invoice_Date >=  convert(datetime,'" + startDate + "',103)) AND " & _
                                " (Sale_Invoice_Date <= convert(datetime,'" + EndDate + "',103)) "
            If chkLocSelect.IsChecked Then
                If cbgLoc.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Select Atleast One Location Code.", Me.Text)
                    Return
                End If
                Qry += " and xxxx.Location in  (" + clsCommon.GetMulcallString(cbgLoc.CheckedValue) + ")  "
            Else
                Qry += " and xxxx.Location in   (" + clsCommon.GetMulcallString(cbgLoc.AllValue) + ")  "
            End If


            If chkItmSelect.IsChecked Then
                If cbgItem.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Select Atleast One Item Code.", Me.Text)
                    Return
                End If
                Qry += " and xxxx.Item_Code in  (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")  "
            End If

            'Qry = Qry + " and xxxx.Item_Code in (select Item_Code  from TSPL_ITEM_MASTER where Item_Type ='F')"
            Qry = Qry + " group by xxxx.Item_Code ,xxxx.MRP_Amt  "

            dtData = clsDBFuncationality.GetDataTable(Qry)

            For Each dr As DataRow In dtData.Rows
                DrFinal = dtFinal.NewRow()
                Dim itmCode As String = dr("Item_Code").ToString() + dr("MRP_Amt").ToString()
                If Dics.ContainsValue(itmCode) = False Then
                    Dics.Add(itmCode, itmCode)
                    OpenBal = dr("Opening Qty").ToString()
                Else
                    OpenBal = CDec(CloseBal)
                End If
                Dim Qty As String = dr("Qty").ToString()
                ' DrFinal.Item("Sale_Invoice_Date") = Format(dr("Sale_Invoice_Date"), "dd/MM/yyyy hh:mm tt") '+ " " + Format(dr("Date_Time_Removal"), "hh:mm")
                DrFinal.Item("Item_Code") = dr("Item_Code").ToString()
                DrFinal.Item("Item_Desc") = dr("Item_Desc").ToString()
                DrFinal.Item("MRP_Amt") = clsCommon.myCdbl(dr("MRP_Amt").ToString())
                DrFinal.Item("Opening Qty") = OpenBal
                DrFinal.Item("ManuQty") = clsCommon.myCdbl(dr("ManuQty").ToString())
                DrFinal.Item("TotalQty") = clsCommon.myCdbl(dr("ManuQty")) + OpenBal
                DrFinal.Item("Qty") = Math.Round(CDec(dr("Qty").ToString()), 2)
                DrFinal.Item("WHQty") = Math.Round(CDec(dr("WHQty").ToString()), 2)
                CloseBal = 0
                CloseBal = CDec(OpenBal) - CDec(dr("Qty").ToString()) - CDec(dr("WHQty").ToString()) + CDec(dr("ManuQty").ToString())
                If CloseBal < 0 Then
                    CloseBal = CloseBal * -1
                End If
                DrFinal.Item("ClosingQty") = CloseBal
                DrFinal.Item("Total_Assessable_Amt") = Math.Round(CDec(dr("Item_Assessable_Rate").ToString()), 2) 'Rate
                DrFinal.Item("Total_Assessable_Value") = clsCommon.myCdbl(dr("Item_Assessable_Rate")) * clsCommon.myCdbl(dr("Qty"))
                DrFinal.Item("Tax1_Rate") = dr("Tax1_Rate").ToString()
                DrFinal.Item("Tax2_Rate") = dr("Tax2_Rate").ToString()
                DrFinal.Item("Tax3_Rate") = dr("Tax3_Rate").ToString()
                DrFinal.Item("Tax1_Amt") = clsCommon.myCdbl(dr("Tax1_Amt").ToString())
                DrFinal.Item("Tax2_Amt") = clsCommon.myCdbl(dr("Tax2_Amt").ToString())
                DrFinal.Item("Tax3_Amt") = clsCommon.myCdbl(dr("Tax3_Amt").ToString())
                DrFinal.Item("StartDate") = dr("StartDate").ToString()
                DrFinal.Item("End Date") = dr("End Date").ToString()
                DrFinal.Item("start Time") = dr("start Time").ToString()
                DrFinal.Item("End Time") = dr("End Time").ToString()
                DrFinal.Item("compname") = dr("compname").ToString()
                DrFinal.Item("address") = dr("address").ToString()
                DrFinal.Item("LocFilter") = dr("LocFilter").ToString()
                'DrFinal.Item("Sale_Invoice_No") = dr("Sale_Invoice_No").ToString()


                dtFinal.Rows.Add(DrFinal)
            Next
            dtFinal.AcceptChanges()
            Dim CrptName As String = ""
            If chkSummary.Checked = True Then
                CrptName = "crptRG1SummaryDemo"
            Else
                CrptName = "crptRG1DetailsMRPWiseDemo"
            End If
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtFinal
            FormatGrid()
            If IsExportToExcel Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("From : " + clsCommon.GetPrintDate(frmDate.Value, "dd/MMM/yyyy") + "   To  " + clsCommon.GetPrintDate(toDate.Value, "dd/MMM/yyyy"))
                clsCommon.MyExportToExcelGrid("RG1 Detail MRP Wise", gv, arrHeader, "RG1 Detail MRP Wise")
                Exit Sub
            End If
            Dim fRMcrys As New frmCrystalReportViewer
            fRMcrys.funreport(CrystalReportFolder.CommonServices, dtFinal, CrptName, "Excise Report")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub FormatGrid()
        Try
            gv.MasterTemplate.SummaryRowsBottom.Clear()

            'Dim strItemCode As String
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            For Each col As GridViewColumn In gv.Columns
                col.IsVisible = False
            Next

            gv.AllowAddNewRow = False

            gv.Columns("Item_Code").IsVisible = True
            gv.Columns("Item_Code").Width = 100
            gv.Columns("Item_Code").HeaderText = "Item Code"

            gv.Columns("Item_Desc").IsVisible = True
            gv.Columns("Item_Desc").Width = 100
            gv.Columns("Item_Desc").HeaderText = "Description"

            gv.Columns("MRP_Amt").IsVisible = True
            gv.Columns("MRP_Amt").Width = 80
            gv.Columns("MRP_Amt").HeaderText = "MRP"

            gv.Columns("Opening Qty").IsVisible = True
            gv.Columns("Opening Qty").Width = 100

            gv.Columns("ManuQty").IsVisible = True
            gv.Columns("ManuQty").Width = 70
            gv.Columns("ManuQty").HeaderText = "MFD Qty"

            gv.Columns("TotalQty").IsVisible = True
            gv.Columns("TotalQty").Width = 100
            gv.Columns("TotalQty").HeaderText = "Total Qty"

            gv.Columns("Qty").IsVisible = True
            gv.Columns("Qty").Width = 100
            gv.Columns("Qty").HeaderText = "Qty Clear"

            gv.Columns("ClosingQty").IsVisible = True
            gv.Columns("ClosingQty").Width = 100
            gv.Columns("ClosingQty").HeaderText = "Closing Stock"

            gv.Columns("Total_Assessable_Amt").IsVisible = True
            gv.Columns("Total_Assessable_Amt").Width = 100
            gv.Columns("Total_Assessable_Amt").HeaderText = "Assessable Rate"

            gv.Columns("Total_Assessable_Value").IsVisible = True
            gv.Columns("Total_Assessable_Value").Width = 100
            gv.Columns("Total_Assessable_Value").HeaderText = "Assessable Value"

            gv.Columns("Tax1_Rate").IsVisible = True
            gv.Columns("Tax1_Rate").Width = 100
            gv.Columns("Tax1_Rate").HeaderText = "Excise %"

            gv.Columns("Tax1_Amt").IsVisible = True
            gv.Columns("Tax1_Amt").Width = 100
            gv.Columns("Tax1_Amt").HeaderText = "Excise Amt"

            gv.Columns("Tax2_Rate").IsVisible = True
            gv.Columns("Tax2_Rate").Width = 100
            gv.Columns("Tax2_Rate").HeaderText = "Cess %"

            gv.Columns("Tax2_Amt").IsVisible = True
            gv.Columns("Tax2_Amt").Width = 100
            gv.Columns("Tax2_Amt").HeaderText = "Cess Amt"

            gv.Columns("Tax3_Rate").IsVisible = True
            gv.Columns("Tax3_Rate").Width = 100
            gv.Columns("Tax3_Rate").HeaderText = "H Cess %"

            gv.Columns("Tax3_Amt").IsVisible = True
            gv.Columns("Tax3_Amt").Width = 100
            gv.Columns("Tax3_Amt").HeaderText = "H Cess Amt"

            'Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item16)
            'gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkItmAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItmAll.ToggleStateChanged
        cbgItem.Enabled = Not chkItmAll.IsChecked
    End Sub

    Private Sub chkLocAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLoc.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub chkRG1MRPWise_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRG1MRPWise.CheckedChanged
        btnExportToExcel.Visible = False
        If chkRG1MRPWise.Checked = True Then
            chkSummary.Checked = False
            btnExportToExcel.Visible = True
        End If
    End Sub

    Private Sub chkSummary_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSummary.CheckedChanged
        If chkSummary.Checked = True Then
            chkRG1MRPWise.Checked = False
        End If
    End Sub


    Private Sub toDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles toDate.LostFocus
        Dim strDate As String
        strDate = (toDate.Value.ToShortTimeString)
        If strDate = "12:00 AM" Then
            toDate.Value = clsCommon.GetDateWithEndTime(toDate.Value)
        End If
    End Sub
End Class

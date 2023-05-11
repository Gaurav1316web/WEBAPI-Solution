'' ''For bug no BM00000000536

Imports common
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Public Class frmER1Demo
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmExciseChapterWise)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmExciseChapterWise_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()

        End If
    End Sub

    Private Sub ExciseChaperHeadWise_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ' ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")





        SetUserMgmtNew()
        'frmDate.Value = System.DateTime.Now()
        'toDate.Value = System.DateTime.Now()
        frmDate.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE())
        toDate.Value = clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE())

        rdbChapterWise.IsChecked = True
        LoadLocation()
        chkLocAll.IsChecked = True
        LoadItems()
        chkItmAll.IsChecked = True
        LoadChapterHead()
        chkChapterAll.IsChecked = True

        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Sub LoadChapterHead()
        Dim qry11 As String = " select Chapter_Head_Code,Description  from TSPL_CHAPTER_HEAD  "
        cbgChapter.DataSource = clsDBFuncationality.GetDataTable(qry11)
        cbgChapter.ValueMember = "Chapter_Head_Code"
        cbgChapter.DisplayMember = "Description"
    End Sub
    Sub LoadItems()
        Dim qry11 As String = " select Item_Code,Item_Desc  from TSPL_ITEM_MASTER where Item_Type ='F' "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry11)
        cbgItem.ValueMember = "Item_Code"
        cbgItem.DisplayMember = "Item_Desc"
    End Sub
    Sub LoadLocation()
        Dim qry As String = " select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type='Physical' and Excisable='T' "
        ' Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "

        cbgLoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLoc.ValueMember = "Location_Code"
        cbgLoc.DisplayMember = "Location_Desc"

    End Sub
 

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If rdbER1.IsChecked = True OrElse rdbtnStockReport.IsChecked = True OrElse rdbMTD.IsChecked = True Then
            printFORER1()
        Else
            print()
        End If
    End Sub
    Sub printFORER1()
        Dim startDate As String = clsCommon.GetPrintDate(frmDate.Value, "dd-MMM-yyyy hh:mm tt")
        Dim EndDate As String = clsCommon.GetPrintDate(toDate.Value, "dd-MMM-yyyy hh:mm tt")
        'Dim startDate As String = clsCommon.GetPrintDate(frmDate.Value, "yyyy-MM-dd hh:mm tt")
        'Dim EndDate As String = clsCommon.GetPrintDate(toDate.Value, "yyyy-MM-dd  hh:mm tt")
        Dim LocFilter As String = ""
        Dim ChapterFilter As String = ""
        Dim ItemFilter As String = ""

        Dim startTime As String = frmDate.Value.ToString("hh:mm tt")
        Dim EndTime As String = toDate.Value.ToString("hh:mm tt")
        Dim dtData As DataTable = Nothing

        '************ DtFinal : Final DataTable
        Dim dtFinal As DataTable = New DataTable()
        If rdbMTD.IsChecked Then
            dtFinal.Columns.Add("MTDManuf", GetType(Decimal))
            dtFinal.Columns.Add("YTDmanuf", GetType(Decimal))
            dtFinal.Columns.Add("MTDcleared", GetType(Decimal))
            dtFinal.Columns.Add("YTDcleared", GetType(Decimal))
        End If
        dtFinal.Columns.Add("WHQty", GetType(Decimal))
        dtFinal.Columns.Add("Chapter Head", GetType(String))
        dtFinal.Columns.Add("Chapter Desc", GetType(String))
        dtFinal.Columns.Add("Unit_code", GetType(String))
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
        dtFinal.Columns.Add("ChapterFilter", GetType(String))
        dtFinal.Columns.Add("ItemFilter", GetType(String))
        Dim DrFinal As DataRow = dtFinal.NewRow()
        '*********** End DataTable
        Dim Dics As New Dictionary(Of String, String)
        Dim OpenBal As String = 0
        Dim CloseBal As String = 0


        Dim Address As String
        If chkLocSelect.IsChecked AndAlso cbgLoc.CheckedValue.Count = 1 Then
            Address = "(select TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_LOCATION_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_LOCATION_MASTER.State) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end  from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Location_Code  =max(xxxx.Location ) )   "
        Else
            Address = "(select TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end from TSPL_COMPANY_MASTER where TSPL_COMPANY_MASTER.Comp_Code =max(xxxx.Comp_Code) )"
        End If


        If cbgChapter.CheckedValue.Count > 0 Then
            ChapterFilter = clsCommon.GetMulcallString(cbgChapter.CheckedValue)
            ChapterFilter = ChapterFilter.Replace("'", "")
        End If
        If cbgLoc.CheckedValue.Count > 0 Then
            LocFilter = clsCommon.GetMulcallString(cbgLoc.CheckedValue)
            LocFilter = LocFilter.Replace("'", "")
        End If

        If cbgItem.CheckedValue.Count > 0 Then
            ItemFilter = clsCommon.GetMulcallString(cbgItem.CheckedValue)
            ItemFilter = ItemFilter.Replace("'", "")
        End If
        Dim Qry As String = ""
        'query For sale invoice data
        'Qry += " SELECT   0 as WHQty,0 as [OpBal],TSPL_SALE_INVOICE_HEAD .Comp_Code , ( select  replace(convert(varchar(50),Conversion_Factor,103),'.00','')+' BTLS IN EACH CASE'  from TSPL_ITEM_UOM_DETAIL " & _
        '                   " where Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code='FB') as Unit_code,   (select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =TSPL_SALE_INVOICE_DETAIL.Item_Code) as [Chapter Head], " & _
        '                   " (select Description  from tspl_chapter_head where Chapter_Head_Code =(select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =TSPL_SALE_INVOICE_DETAIL.Item_Code)) as [Chapter Desc]," & _
        '                   " TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,  TSPL_SALE_INVOICE_HEAD.Date_Time_Removal as [Sale_Invoice_Date], TSPL_SALE_INVOICE_DETAIL.Item_Code,  TSPL_SALE_INVOICE_DETAIL.Item_Desc ," & _
        '                   " (SELECT     CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB' THEN     TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB') ELSE TSPL_SALE_INVOICE_DETAIL.Invoice_Qty END AS Expr1)  AS Qty," & _
        '                   " CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB' THEN      TSPL_SALE_INVOICE_DETAIL.Item_Assessable_Rate * (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL" & _
        '                   " where Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB') ELSE TSPL_SALE_INVOICE_DETAIL.Item_Assessable_Rate END AS Item_Assessable_Rate ," & _
        '                   " TSPL_SALE_INVOICE_DETAIL.TAX1_Rate, TSPL_SALE_INVOICE_DETAIL.TAX1_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as [TAX1_Amt], TSPL_SALE_INVOICE_DETAIL.TAX2_Rate," & _
        '                   " TSPL_SALE_INVOICE_DETAIL.TAX2_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty    as [TAX2_Amt],     TSPL_SALE_INVOICE_DETAIL.TAX3_Rate, TSPL_SALE_INVOICE_DETAIL.TAX3_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as [TAX3_Amt]," & _
        '                   " TSPL_SALE_INVOICE_HEAD.Date_Time_Removal, case when TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB' then   TSPL_SALE_INVOICE_DETAIL.MRP_Amt else  TSPL_SALE_INVOICE_DETAIL.MRP_Amt/(select Conversion_Factor  from " & _
        '                   " TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB') end as MRP_Amt ,TSPL_SALE_INVOICE_HEAD.Location  ,0 as [ManuQty],0 as ClosingQty   FROM " & _
        '                   " TSPL_SALE_INVOICE_HEAD INNER JOIN TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No =   TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN TSPL_LOCATION_MASTER ON TSPL_SALE_INVOICE_HEAD.Location =   TSPL_LOCATION_MASTER.Location_Code WHERE TSPL_SALE_INVOICE_HEAD   .Is_Post ='Y' " & _
        Qry += " SELECT   0 as WHQty,0 as [OpBal],TSPL_SD_SALE_INVOICE_HEAD .Comp_Code ,  TSPL_SD_SALE_INVOICE_DETAIL.Unit_code ,   (select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code) as [Chapter Head],  (select Description  from tspl_chapter_head where Chapter_Head_Code =(select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code)) as [Chapter Desc], TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Sale_Invoice_No,  TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [Sale_Invoice_Date], TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,  TSPL_ITEM_MASTER.Item_Desc ,TSPL_SD_SALE_INVOICE_DETAIL.Qty  AS Qty,case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)=0 then 0 else TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Base_Amt /TSPL_SD_SALE_INVOICE_DETAIL.Qty end   AS Item_Assessable_Rate , TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt as [TAX1_Amt], TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt as [TAX2_Amt],     TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt as [TAX3_Amt], TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date_Time_Removal,  TSPL_SD_SALE_INVOICE_DETAIL.MRP  as MRP_Amt ,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as Location  ,0 as [ManuQty],0 as ClosingQty   "
        Qry += " FROM TSPL_SD_SALE_INVOICE_HEAD "
        Qry += " INNER JOIN TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code =   TSPL_SD_SALE_INVOICE_DETAIL.Document_Code "
        Qry += " INNER JOIN TSPL_LOCATION_MASTER ON TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =   TSPL_LOCATION_MASTER.Location_Code "
        Qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code"
        Qry += " WHERE TSPL_SD_SALE_INVOICE_HEAD   .Status =1"
        Qry += Environment.NewLine + "  union all " + Environment.NewLine
        '  'query For Warehouse leakage breakage data
        '  Qry += " SELECT DISTINCT (Leakage_Qty + Breakage_Qty + Shortage_Qty) as WHQty,0 as [OP],hd.Comp_Code, ( select replace(convert(varchar(50),Conversion_Factor,103),'.00','')+' BTLS IN EACH CASE'  from TSPL_ITEM_UOM_DETAIL  where Item_Code=dl.Item_Code and UOM_Code='FB'), " & _
        ' "(select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =dl.Item_Code) as [Chapter Head]," & _
        ' "(select Description  from tspl_chapter_head where Chapter_Head_Code =(select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =dl.Item_Code)) as [Chapter Desc], " & _
        ' " hd.Document_No ,hd.Document_Date ,  dl.Item_Code ,dl.Item_Description ,0 as Item_Quantity ,0 as Assessable_Amt , " & _
        ' " 0 as TAX1_Rate ,0 as TAX1_Amt  , 0 as TAX2_Rate,0 as TAX2_Amt ,  0 as TAX3_Rate,0 as TAX3_Amt , " & _
        ' " hd.Document_Date AS Date_Time_Removal , case when dl.Unit_Code  = 'FB' then " & _
        ' " dl.MRP else  dl.MRP/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code =dl.Item_Code and UOM_Code ='FB') end as MRP_Amt, " & _
        ' " hd.Loc_Code ,0 as [ManuQty],0 as ClosingQty    from TSPL_WH_BREAKAGE_HEAD hd inner join TSPL_WH_BREAKAGE_DETAIL dl on " & _
        ' " hd.Document_No=dl.Document_No  WHERE  hd.Is_Post=1  and dl.Unit_Code not in ('EC','EB','SH') " & _
        'Environment.NewLine + "  union all " + Environment.NewLine
        '  'query For Transfer data
        '  Qry += " SELECT DISTINCT  0 as WHQty,0,hd.Comp_Code, ( select replace(convert(varchar(50),Conversion_Factor,103),'.00','')+' BTLS IN EACH CASE'  from TSPL_ITEM_UOM_DETAIL  where Item_Code=dl.Item_Code and UOM_Code='FB'), (select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =dl.Item_Code) as [Chapter Head]," & _
        ' " (select Description  from tspl_chapter_head where Chapter_Head_Code =(select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =dl.Item_Code)) as [Chapter Desc], hd.Transfer_No ,hd.EntryDateTime ,  dl.Item_Code ,dl.Item_Desc ,dl.Item_Qty ,dl.Assessable_Amt ,dl.TAX1_Rate ,dl.TAX1_Amt  ," & _
        ' " dl.TAX2_Rate,dl.TAX2_Amt ,  dl.TAX3_Rate,dl.TAX3_Amt , hd.Date_Time_Removal AS Date_Time_Removal , case when dl.Uom  = 'FB' then   dl.MRP else  dl.MRP/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code =dl.Item_Code and UOM_Code ='FB') end as MRP_Amt," & _
        ' " hd.From_Location ,0 as [ManuQty],0 as ClosingQty    from TSPL_TRANSFER_HEAD hd inner join TSPL_TRANSFER_DETAIL dl on hd.Transfer_No=dl.Transfer_No  WHERE hd.Item_Type <>'Empty' AND hd.Post='Y'  and hd.Item_Type ='Full' " & _
        ' Environment.NewLine + "  union all " + Environment.NewLine
        'query For adjustment manufactured data
        Qry += " select  0 as WHQty,0, TSPL_ADJUSTMENT_HEADER.Comp_Code , ( select replace(convert(varchar(50),Conversion_Factor,103),'.00','')+' BTLS IN EACH CASE'  from TSPL_ITEM_UOM_DETAIL  where Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code and UOM_Code='FB'),  " & _
       " (select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =TSPL_ADJUSTMENT_DETAIL.Item_Code) as [Chapter Head],(select Description  from tspl_chapter_head where Chapter_Head_Code =(select  Cheapter_Heads  from TSPL_ITEM_MASTER " & _
       " where Item_Code  =TSPL_ADJUSTMENT_DETAIL.Item_Code)) as [Chapter Desc]," & _
       " TSPL_ADJUSTMENT_DETAIL.Adjustment_No,convert(datetime,TSPL_ADJUSTMENT_HEADER.EntryDateTime,103) ,TSPL_ADJUSTMENT_DETAIL.Item_Code,TSPL_ADJUSTMENT_DETAIL.Item_Description ,0 as qty,0 as Total_Assessable_Amt, 0 ,0, 0 ,0, 0,0, '' AS Date_Time_Removal," & _
       " case when TSPL_ADJUSTMENT_DETAIL.Unit_code = 'FB' then  TSPL_ADJUSTMENT_DETAIL.mrp else  TSPL_ADJUSTMENT_DETAIL.mrp/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ADJUSTMENT_DETAIL.Item_Code and UOM_Code ='FB') end as MRP_Amt," & _
       " Location_Code, case when Adjustment_Type='BI' then (Case When TSPL_ADJUSTMENT_DETAIL.Unit_Code='FB' Then TSPL_ADJUSTMENT_DETAIL.Item_Quantity/Conversion_Factor else TSPL_ADJUSTMENT_DETAIL.Item_Quantity End) Else Case when Adjustment_Type='BD' then (Case When TSPL_ADJUSTMENT_DETAIL.Unit_Code='FB' Then (TSPL_ADJUSTMENT_DETAIL.Item_Quantity/Conversion_Factor)*-1 Else TSPL_ADJUSTMENT_DETAIL.Item_Quantity*-1  end) end End as [ManuQty],0 as ClosingQty " & _
       " from TSPL_ADJUSTMENT_DETAIL inner join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_DETAIL.Adjustment_No =TSPL_ADJUSTMENT_HEADER .Adjustment_No" & _
       " LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code " & _
       " where Adjustment_Type in ('BI','BD') and TSPL_ADJUSTMENT_HEADER.ItemType='FM'  AND  TSPL_ADJUSTMENT_HEADER.Posted='Y'  and TSPL_ADJUSTMENT_DETAIL.Unit_Code not in ('EC','EB','SH') AND TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' "
        Qry += Environment.NewLine + "  union all " + Environment.NewLine
        'main query For Opening balance data
        Qry += "select  0 as WHQty, SUM(Qty ),max(xxx.Comp_Code  ),MAX(xxx.Unit_code) as unit_code,  max(TSPL_ITEM_MASTER.Cheapter_Heads),   '' as [Chapter Desc],  '','" + startDate + "' ,xxx.Item_Code,'' ,0 as qty,0 as Total_Assessable_Amt, 0 ,0, 0 ,0, 0,0, '' AS Date_Time_Removal,xxx.MRP_Amt  as MRP_Amt,xxx.Location ,0   as [ManuQty],0 as ClosingQty  " & _
       " from ( "
        'query For Sale invoice Opening balance data
        ' Qry += "SELECT ( select  replace(convert(varchar(50),Conversion_Factor,103),'.00','')+' BTLS IN EACH CASE'  from TSPL_ITEM_UOM_DETAIL " & _
        ' " where Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code='FB') as Unit_code, case when TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB'  then   TSPL_SALE_INVOICE_DETAIL.MRP_Amt else  TSPL_SALE_INVOICE_DETAIL.MRP_Amt/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where  Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB') end as MRP_Amt," & _
        '" TSPL_SALE_INVOICE_HEAD.Location, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,  TSPL_SALE_INVOICE_DETAIL.Item_Code, -1*  (SELECT     CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB' THEN    TSPL_SALE_INVOICE_DETAIL.Invoice_Qty /  (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB') ELSE TSPL_SALE_INVOICE_DETAIL.Invoice_Qty END AS Expr1)  AS Qty ,TSPL_SALE_INVOICE_HEAD.Comp_Code" & _
        '" FROM  TSPL_SALE_INVOICE_HEAD INNER JOIN    TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No =  TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN    TSPL_LOCATION_MASTER ON TSPL_SALE_INVOICE_HEAD.Location =   TSPL_LOCATION_MASTER.Location_Code where" & _
        '" (convert(datetime,Date_Time_Removal,103) < '" + startDate + "') and   TSPL_SALE_INVOICE_HEAD.Is_Post='Y' "

        Qry += "SELECT TSPL_SD_SALE_INVOICE_DETAIL.Unit_code ,TSPL_SD_SALE_INVOICE_DETAIL.MRP as  MRP_Amt, TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as Location, TSPL_SD_SALE_INVOICE_HEAD.Document_Date as  Sale_Invoice_Date,  TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, -1*  (TSPL_SD_SALE_INVOICE_DETAIL.Qty)  AS Qty ,TSPL_SD_SALE_INVOICE_HEAD.Comp_Code "
        Qry += "  FROM TSPL_SD_SALE_INVOICE_HEAD "
        Qry += " INNER JOIN    TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code =  TSPL_SD_SALE_INVOICE_DETAIL.Document_Code "
        Qry += " INNER JOIN    TSPL_LOCATION_MASTER ON TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =   TSPL_LOCATION_MASTER.Location_Code "
        Qry += "  where (convert(datetime,Document_Date,103) < '" + startDate + "') and   TSPL_SD_SALE_INVOICE_HEAD.Status=1"
        Qry += Environment.NewLine + "  union all " + Environment.NewLine
        '  'query For Transfer Opening balance data
        '  Qry += "SELECT ( select replace(convert(varchar(50),Conversion_Factor,103),'.00','')+' BTLS IN EACH CASE'  from TSPL_ITEM_UOM_DETAIL  where Item_Code=dl.Item_Code and UOM_Code='FB') as unit_code,case when dl.Uom  = 'FB' then   dl.MRP else  dl.MRP/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL  where Item_Code =dl.Item_Code and UOM_Code ='FB') end as MRP_Amt, hd.From_Location,  hd.Posting_Date ,  dl.Item_Code ,dl.Item_Qty *-1 ,hd .Comp_Code " & _
        ' " from TSPL_TRANSFER_HEAD hd inner join TSPL_TRANSFER_DETAIL dl on hd.Transfer_No=dl.Transfer_No where  (convert(datetime,hd.EntryDateTime,103) < '" + startDate + "') and  hd.Post='Y' and hd.Item_Type ='Full' " & _
        'Environment.NewLine + "  union all " + Environment.NewLine
        '  'query For Warehouse leakage breakage Opening balance data
        '  Qry += "SELECT  ( select replace(convert(varchar(50),Conversion_Factor,103),'.00','')+' BTLS IN EACH CASE'  from TSPL_ITEM_UOM_DETAIL  where Item_Code=dl.Item_Code and UOM_Code='FB') as Unit_code, " & _
        '  "case when dl.Unit_Code  = 'FB' then   dl.MRP else  dl.MRP/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL " & _
        ' "where Item_Code =dl.Item_Code and UOM_Code ='FB') end as MRP_Amt, hd.Loc_Code,  hd.Posting_Date ,  dl.Item_Code , " & _
        ' "(Leakage_Qty + Breakage_Qty + Shortage_Qty) *-1 ,hd .Comp_Code  from TSPL_WH_BREAKAGE_HEAD hd inner join TSPL_WH_BREAKAGE_DETAIL dl on " & _
        ' "hd.Document_No=dl.Document_No where  (convert(datetime,hd.Document_Date,103) < '" + startDate + "') and " & _
        ' "hd.Is_Post=1  and dl.Unit_Code not in ('EC','EB','SH') " & _
        'Environment.NewLine + "  union all " + Environment.NewLine
        'query For adjustment Opening balance data
        Qry += "select   ( select replace(convert(varchar(50),Conversion_Factor,103),'.00','')+' BTLS IN EACH CASE'  from TSPL_ITEM_UOM_DETAIL  where Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code and UOM_Code='FB') as Unit_code,  " & _
        "case when TSPL_ADJUSTMENT_DETAIL.Unit_code = 'FB' then " & _
       " TSPL_ADJUSTMENT_DETAIL.mrp else  TSPL_ADJUSTMENT_DETAIL.mrp/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ADJUSTMENT_DETAIL.Item_Code and UOM_Code ='FB') end as MRP_Amt , TSPL_ADJUSTMENT_HEADER.Loc_Code, convert(datetime,TSPL_ADJUSTMENT_HEADER.Posting_Date,103) ,TSPL_ADJUSTMENT_DETAIL.Item_Code,case when Adjustment_Type='BI' then " & _
       " TSPL_ADJUSTMENT_DETAIL.Item_Quantity  when Adjustment_Type='BD'  then TSPL_ADJUSTMENT_DETAIL.Item_Quantity*-1 end ,TSPL_ADJUSTMENT_HEADER.Comp_Code   from TSPL_ADJUSTMENT_DETAIL inner join TSPL_ADJUSTMENT_HEADER on  TSPL_ADJUSTMENT_DETAIL.Adjustment_No =TSPL_ADJUSTMENT_HEADER .Adjustment_No   where Adjustment_Type in ('BI','BD') and TSPL_ADJUSTMENT_HEADER.Posted='Y'  and TSPL_ADJUSTMENT_HEADER.ItemType='FM' " & _
       " and convert(datetime,TSPL_ADJUSTMENT_HEADER.EntryDateTime,103) < '" + startDate + "'  and TSPL_ADJUSTMENT_DETAIL.Unit_Code not in ('EC','EB','SH')  "
        Qry += "   ) as xxx inner join TSPL_ITEM_MASTER on xxx.Item_Code =TSPL_ITEM_MASTER.Item_Code  group by xxx.Item_Code ,xxx.Location,xxx.MRP_Amt "
        'main query ends here
        Dim strDate, StrMonth, strYear As String
        Dim strMTD, strYTD As Date
        Dim PeriodQry As String

        strDate = "01"
        StrMonth = frmDate.Value.Month
        strYear = (toDate.Value.Year)

        strMTD = strDate & "/" & StrMonth & "/" & strYear
        strYTD = strDate & "/" & "01" & "/" & strYear

        Dim Monthdate As String = clsCommon.GetPrintDate(strMTD)
        Dim YearDate As String = clsCommon.GetPrintDate(strYTD)
        'Main query start here




        If rdbMTD.IsChecked = False Then
            PeriodQry = " select  DISTINCT SUM(xxxx.WHQty) as WHQty,'" + clsCommon.GetPrintDate(startDate, "dd/MM/yyyy") + "' AS StartDate, '" + clsCommon.GetPrintDate(EndDate, "dd/MM/yyyy") + "' AS [End Date], '" + startTime + "' AS [Start Time], '" + EndTime + "' AS [End Time],'" + ChapterFilter + "' as ChapterFilter,'" + LocFilter + "' as LocFilter,'" + ItemFilter + "' as ItemFilter," & _
                      " Sum(xxxx.OpBal ) as [Opening Qty],max(xxxx.Unit_code ) as Unit_code,    MAX(TSPL_ITEM_MASTER.Cheapter_Heads  ) as [Chapter Head],max(TSPL_CHAPTER_HEAD.Description  ) as [Chapter Desc]," & _
                      "  xxxx.Item_Code,  max(TSPL_ITEM_MASTER.Item_Desc) as Item_Desc ,  SUM(xxxx.Qty )  AS Qty, max(xxxx.Item_Assessable_Rate ) AS Item_Assessable_Rate ," & _
                      " max(xxxx.TAX1_Rate) as TAX1_Rate, SUM(xxxx.TAX1_Amt ) as [TAX1_Amt], max(xxxx.TAX2_Rate) as TAX2_Rate, SUM(xxxx.TAX2_Amt )    as [TAX2_Amt], " & _
                      " max(xxxx.TAX3_Rate) as TAX3_Rate, sum(xxxx.TAX3_Amt)   as [TAX3_Amt], isnull(xxxx.MRP_Amt,0)  as MRP_Amt ,max(xxxx.Location) as Location  ,SUM(xxxx.ManuQty ) as [ManuQty],sum(xxxx.ClosingQty)  as ClosingQty, (select Comp_Name  from TSPL_COMPANY_MASTER where TSPL_COMPANY_MASTER.Comp_Code=max(xxxx.Comp_Code)  ) as compname," + Address + " as address" & _
                      " ,0 as MTDManuf,0 as YTDmanuf,0 as MTDcleared,0 as YTDcleared from   ( " & Qry & " ) as xxxx  inner join TSPL_ITEM_MASTER on xxxx.Item_Code =TSPL_ITEM_MASTER.Item_Code  left outer join TSPL_CHAPTER_HEAD on xxxx.[Chapter Head] =TSPL_CHAPTER_HEAD.Chapter_Head_Code " & _
                      "  where    (Sale_Invoice_Date >=  convert(datetime,'" + startDate + "',103)) AND  (Sale_Invoice_Date <=  convert(datetime,'" + EndDate + "',103))  "
            Qry = PeriodQry

        Else

            If chkLocSelect.IsChecked AndAlso cbgLoc.CheckedValue.Count = 1 Then
                Address = "(select TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_LOCATION_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_LOCATION_MASTER.State) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end  from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Location_Code  =max(aa.Location ) )   "
            Else
                Address = "(select TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end from TSPL_COMPANY_MASTER where TSPL_COMPANY_MASTER.Comp_Code =max(aa.Comp_Code) )"
            End If


            PeriodQry = " select   SUM(aa.WHQty) as WHQty,'" + clsCommon.GetPrintDate(startDate, "dd/MM/yyyy") + "' AS StartDate, '" + clsCommon.GetPrintDate(EndDate, "dd/MM/yyyy") + "' AS [End Date], '" + startTime + "' AS [Start Time], '" + EndTime + "' AS [End Time],'" + ChapterFilter + "' as ChapterFilter,'" + LocFilter + "' as LocFilter,'" + ItemFilter + "' as ItemFilter," & _
                        " Sum(aa.OpBal )  as [Opening Qty],max(aa.Unit_code ) as Unit_code,'' as [Chapter Head],'' as [Chapter Desc]," & _
                        "  aa.Item_Code, '' as Item_Desc ,  SUM(aa.Qty )  AS Qty, max(aa.Item_Assessable_Rate ) AS Item_Assessable_Rate ," & _
                        " max(aa.TAX1_Rate) as TAX1_Rate, SUM(aa.TAX1_Amt ) as [TAX1_Amt], max(aa.TAX2_Rate) as TAX2_Rate, SUM(aa.TAX2_Amt )    as [TAX2_Amt], " & _
                        " max(aa.TAX3_Rate) as TAX3_Rate, sum(aa.TAX3_Amt)   as [TAX3_Amt], isnull(aa.MRP_Amt,0)  as MRP_Amt ,max(aa.Location) as Location  ,SUM(aa.ManuQty ) as [ManuQty],sum(aa.ClosingQty)  as ClosingQty, (select Comp_Name  from TSPL_COMPANY_MASTER where TSPL_COMPANY_MASTER.Comp_Code=max(aa.Comp_Code)  ) as compname," + Address + " as address" & _
                        " ,0 as MTDManuf,0 as YTDmanuf,0 as MTDcleared,0 as YTDcleared from   ( " & Qry & " ) as aa  " & _
                         "  where    (Sale_Invoice_Date >=  convert(datetime,'" + startDate + "',103)) AND  (Sale_Invoice_Date <=  convert(datetime,'" + EndDate + "',103))     "

            Dim monthQry As String = " select   SUM(aa.WHQty) as WHQty,'" + clsCommon.GetPrintDate(startDate, "dd/MM/yyyy") + "' AS StartDate, '" + clsCommon.GetPrintDate(EndDate, "dd/MM/yyyy") + "' AS [End Date], '" + startTime + "' AS [Start Time], '" + EndTime + "' AS [End Time],'" + ChapterFilter + "' as ChapterFilter,'" + LocFilter + "' as LocFilter,'" + ItemFilter + "' as ItemFilter," & _
                               " 0 as [Opening Qty],max(aa.Unit_code ) as Unit_code,   '' as [Chapter Head],'' as [Chapter Desc]," & _
                               "  aa.Item_Code,  '' as Item_Desc , 0  AS Qty, max(aa.Item_Assessable_Rate ) AS Item_Assessable_Rate ," & _
                               " max(aa.TAX1_Rate) as TAX1_Rate, SUM(aa.TAX1_Amt ) as [TAX1_Amt], max(aa.TAX2_Rate) as TAX2_Rate, SUM(aa.TAX2_Amt )    as [TAX2_Amt], " & _
                               " max(aa.TAX3_Rate) as TAX3_Rate, sum(aa.TAX3_Amt)   as [TAX3_Amt], isnull(aa.MRP_Amt,0)  as MRP_Amt ,max(aa.Location) as Location  ,0 as [ManuQty],sum(aa.ClosingQty)  as ClosingQty, (select Comp_Name  from TSPL_COMPANY_MASTER where TSPL_COMPANY_MASTER.Comp_Code=max(aa.Comp_Code)  ) as compname," + Address + " as address" & _
                               " ,SUM(aa.ManuQty ) as MTDManuf,0 as YTDmanuf, SUM(aa.Qty ) as MTDcleared,0 as YTDcleared from   ( " & Qry & " ) as aa " & _
                          "  where    (Sale_Invoice_Date >=  convert(datetime,'" + Monthdate + "',103)) AND  (Sale_Invoice_Date <=  convert(datetime,'" + EndDate + "',103)) "

            Dim YearQry As String = " select   SUM(aa.WHQty) as WHQty,'" + clsCommon.GetPrintDate(startDate, "dd/MM/yyyy") + "' AS StartDate, '" + clsCommon.GetPrintDate(EndDate, "dd/MM/yyyy") + "' AS [End Date], '" + startTime + "' AS [Start Time], '" + EndTime + "' AS [End Time],'" + ChapterFilter + "' as ChapterFilter,'" + LocFilter + "' as LocFilter,'" + ItemFilter + "' as ItemFilter," & _
                               " 0 as [Opening Qty],max(aa.Unit_code ) as Unit_code,  '' as [Chapter Head],'' as [Chapter Desc]," & _
                               "  aa.Item_Code, '' as Item_Desc ,  0 AS Qty, max(aa.Item_Assessable_Rate ) AS Item_Assessable_Rate ," & _
                               " max(aa.TAX1_Rate) as TAX1_Rate, SUM(aa.TAX1_Amt ) as [TAX1_Amt], max(aa.TAX2_Rate) as TAX2_Rate, SUM(aa.TAX2_Amt )    as [TAX2_Amt], " & _
                               " max(aa.TAX3_Rate) as TAX3_Rate, sum(aa.TAX3_Amt)   as [TAX3_Amt], isnull(aa.MRP_Amt,0)  as MRP_Amt ,max(aa.Location) as Location  ,0 as [ManuQty],sum(aa.ClosingQty)  as ClosingQty, (select Comp_Name  from TSPL_COMPANY_MASTER where TSPL_COMPANY_MASTER.Comp_Code=max(aa.Comp_Code)  ) as compname," + Address + " as address" & _
                               " ,0 as MTDManuf,SUM(aa.ManuQty ) as YTDmanuf,0 as MTDcleared,SUM(aa.Qty )  as YTDcleared from   ( " & Qry & " ) as aa " & _
                        "  where    (Sale_Invoice_Date >=  convert(datetime,'" + YearDate + "',103)) AND  (Sale_Invoice_Date <=  convert(datetime,'" + EndDate + "',103)) and Sale_Invoice_Date ! < ('2013-07-01')    "
            'select Adjustment_Date,500 + 4935,* from TSPL_ADJUSTMENT_HEADER  where Adjustment_Date ! < ('2013-07-01') 
            'select Adjustment_Date,* from TSPL_ADJUSTMENT_HEADER  where Adjustment_Date <= ('2013-06-30') 

            If chkLocSelect.IsChecked Then
                If cbgLoc.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please Select Atleast One Location Code.")
                    Return
                End If
                PeriodQry += " and aa.Location in   (" + clsCommon.GetMulcallString(cbgLoc.CheckedValue) + ") and aa.Item_Code in (select Item_Code  from TSPL_ITEM_MASTER where Item_Type ='F') group by aa.Item_Code, MRP_Amt  "
                monthQry += " and aa.Location in   (" + clsCommon.GetMulcallString(cbgLoc.CheckedValue) + ") and aa.Item_Code in (select Item_Code  from TSPL_ITEM_MASTER where Item_Type ='F') group by aa.Item_Code, MRP_Amt  "
                YearQry += " and aa.Location in   (" + clsCommon.GetMulcallString(cbgLoc.CheckedValue) + ") and aa.Item_Code in (select Item_Code  from TSPL_ITEM_MASTER where Item_Type ='F') group by aa.Item_Code, MRP_Amt  "
            Else
                PeriodQry += " and aa.Location in   (" + clsCommon.GetMulcallString(cbgLoc.AllValue) + ") and aa.Item_Code in (select Item_Code  from TSPL_ITEM_MASTER where Item_Type ='F') group by aa.Item_Code, MRP_Amt  "
                monthQry += " and aa.Location in   (" + clsCommon.GetMulcallString(cbgLoc.AllValue) + ") and aa.Item_Code in (select Item_Code  from TSPL_ITEM_MASTER where Item_Type ='F') group by aa.Item_Code, MRP_Amt  "
                YearQry += " and aa.Location in   (" + clsCommon.GetMulcallString(cbgLoc.AllValue) + ")  and aa.Item_Code in (select Item_Code  from TSPL_ITEM_MASTER where Item_Type ='F') group by aa.Item_Code, MRP_Amt "
            End If


            Qry = PeriodQry & " union all " & monthQry & " union all " & YearQry

            Qry = " select   SUM(xxxx.WHQty) as WHQty,'" + clsCommon.GetPrintDate(startDate, "dd/MM/yyyy") + "' AS StartDate, '" + clsCommon.GetPrintDate(EndDate, "dd/MM/yyyy") + "' AS [End Date], '" + startTime + "' AS [Start Time], '" + EndTime + "' AS [End Time],'" + ChapterFilter + "' as ChapterFilter,'" + LocFilter + "' as LocFilter,'" + ItemFilter + "' as ItemFilter," & _
                "  Sum(xxxx.[Opening Qty] ) as [Opening Qty],max(xxxx.Unit_code ) as Unit_code,    MAX(TSPL_ITEM_MASTER.Cheapter_Heads  ) as [Chapter Head], " & _
                "max(TSPL_CHAPTER_HEAD.Description  ) as [Chapter Desc],  xxxx.Item_Code,  max(TSPL_ITEM_MASTER.Item_Desc) as Item_Desc ,  " & _
                "SUM(xxxx.Qty )  AS Qty, max(xxxx.Item_Assessable_Rate ) AS Item_Assessable_Rate , max(xxxx.TAX1_Rate) as TAX1_Rate, " & _
                "SUM(xxxx.TAX1_Amt ) as [TAX1_Amt], max(xxxx.TAX2_Rate) as TAX2_Rate, SUM(xxxx.TAX2_Amt )    as [TAX2_Amt],  max(xxxx.TAX3_Rate) as TAX3_Rate, " & _
                "sum(xxxx.TAX3_Amt)   as [TAX3_Amt], isnull(xxxx.MRP_Amt,0)  as MRP_Amt ,max(xxxx.Location) as Location  ,SUM(xxxx.ManuQty ) as ManuQty, " & _
                "sum(xxxx.ClosingQty)  as ClosingQty,max(compname) as compname,max(address) as address,sum(MTDManuf) as MTDManuf,SUM(YTDmanuf) as YTDmanuf,SUM(MTDcleared) as MTDcleared,SUM(YTDcleared) as YTDcleared   " & _
                "from ( " & Qry & ") xxxx inner join TSPL_ITEM_MASTER on xxxx.Item_Code =TSPL_ITEM_MASTER.Item_Code  left outer join TSPL_CHAPTER_HEAD on xxxx.[Chapter Head] =TSPL_CHAPTER_HEAD.Chapter_Head_Code  where 2=2"
        End If



        If chkChapterSelect.IsChecked Then
            If cbgChapter.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast One Item Chapter Head Code.")
                Return
            End If
            Qry += " and xxxx.[Chapter Head] in  (" + clsCommon.GetMulcallString(cbgChapter.CheckedValue) + ")  "
        End If


        If chkLocSelect.IsChecked Then
            If cbgLoc.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast One Location Code.")
                Return
            End If
            Qry += " and xxxx.Location in   (" + clsCommon.GetMulcallString(cbgLoc.CheckedValue) + ")  "
        Else
            Qry += " and xxxx.Location in   (" + clsCommon.GetMulcallString(cbgLoc.AllValue) + ")  "
        End If


        If chkItmSelect.IsChecked Then
            If cbgItem.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast One Item Code.")
                Return
            End If
            Qry += " and xxxx.Item_Code in  (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")  "
        End If

        'Qry = Qry + " and xxxx.Item_Code in (select Item_Code  from TSPL_ITEM_MASTER where Item_Type ='F')"
        Qry = Qry + " group by xxxx.Item_Code, MRP_Amt"
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
            ' DrFinal.Item("Sale_Invoice_Date") = Format(dr("Sale_Invoice_Date"), "dd/MM/yyyy") + " " + Format(dr("Date_Time_Removal"), "hh:mm:ss")
            DrFinal.Item("Opening Qty") = OpenBal
            If rdbMTD.IsChecked Then
                DrFinal.Item("MTDManuf") = Math.Round(CDec(dr("MTDManuf").ToString()), 2)
                DrFinal.Item("YTDmanuf") = Math.Round(CDec(dr("YTDmanuf").ToString()), 2)
                DrFinal.Item("MTDcleared") = Math.Round(CDec(dr("MTDcleared").ToString()), 2)
                DrFinal.Item("YTDcleared") = Math.Round(CDec(dr("YTDcleared").ToString()), 2)
            End If
            DrFinal.Item("Qty") = Math.Round(CDec(dr("Qty").ToString()), 2)
            DrFinal.Item("WHQty") = Math.Round(CDec(dr("WHQty").ToString()), 2)
            DrFinal.Item("Item_Code") = dr("Item_Code").ToString()
            DrFinal.Item("Chapter Desc") = dr("Chapter Desc").ToString()
            DrFinal.Item("Chapter Head") = dr("Chapter Head").ToString()
            DrFinal.Item("Unit_code") = dr("Unit_code").ToString()
            DrFinal.Item("Item_Desc") = dr("Item_Desc").ToString()
            DrFinal.Item("Tax1_Rate") = dr("Tax1_Rate").ToString()
            DrFinal.Item("Tax2_Rate") = dr("Tax2_Rate").ToString()
            DrFinal.Item("Tax3_Rate") = dr("Tax3_Rate").ToString()
            DrFinal.Item("Tax1_Amt") = dr("Tax1_Amt").ToString()
            DrFinal.Item("Tax2_Amt") = dr("Tax2_Amt").ToString()
            DrFinal.Item("Tax3_Amt") = dr("Tax3_Amt").ToString()
            DrFinal.Item("ManuQty") = dr("ManuQty").ToString()
            DrFinal.Item("MRP_Amt") = clsCommon.myCdbl(dr("MRP_Amt").ToString())
            DrFinal.Item("StartDate") = dr("StartDate").ToString()
            DrFinal.Item("End Date") = dr("End Date").ToString()
            DrFinal.Item("start Time") = dr("start Time").ToString()
            DrFinal.Item("End Time") = dr("End Time").ToString()
            DrFinal.Item("compname") = dr("compname").ToString()
            DrFinal.Item("address") = dr("address").ToString()
            DrFinal.Item("LocFilter") = dr("LocFilter").ToString()
            DrFinal.Item("ChapterFilter") = dr("ChapterFilter").ToString()
            DrFinal.Item("ItemFilter") = dr("ItemFilter").ToString()
            ' DrFinal.Item("Sale_Invoice_No") = dr("Sale_Invoice_No").ToString()
            DrFinal.Item("Total_Assessable_Amt") = Math.Round(CDec(dr("Item_Assessable_Rate").ToString()), 2)
            CloseBal = 0
            CloseBal = CDec(OpenBal) - CDec(dr("Qty").ToString()) - CDec(dr("WHQty").ToString()) + CDec(dr("ManuQty").ToString())
            If CloseBal < 0 Then
                CloseBal = CloseBal * -1
            End If
            DrFinal.Item("ClosingQty") = CloseBal
            dtFinal.Rows.Add(DrFinal)
        Next
        dtFinal.AcceptChanges()
        Dim CrptName As String = ""
        If rdbChapterWise.IsChecked = True Then
            CrptName = "crptChapterHeadWiseDemo"
        ElseIf rdbER1.IsChecked = True Then
            CrptName = "crptER1Demo"
        ElseIf rdbtnStockReport.IsChecked Then
            CrptName = "crptERStockReportDemo"
        ElseIf rdbMTD.IsChecked Then
            CrptName = "crptERStockReportMtdYtdDemo"
        End If

        Dim FRMcrys As New frmCrystalReportViewer
        FRMcrys.funreport(CrystalReportFolder.CommonServices, dtFinal, CrptName, "Excise Report")
    End Sub
    Sub print()
        Try
            Dim startDate As String = clsCommon.GetPrintDate(frmDate.Value, "dd-MMM-yyyy hh:mm tt")
            Dim EndDate As String = clsCommon.GetPrintDate(toDate.Value, "dd-MMM-yyyy hh:mm tt")
            Dim LocFilter As String = ""
            Dim ChapterFilter As String = ""
            Dim ItemFilter As String = ""

            Dim startTime As String = frmDate.Value.ToString("hh:mm tt")
            Dim EndTime As String = toDate.Value.ToString("hh:mm tt")
            Dim dtData As DataTable = Nothing

            '************ DtFinal : Final DataTable
            Dim dtFinal As DataTable = New DataTable()
            dtFinal.Columns.Add("WHQty", GetType(Decimal))
            dtFinal.Columns.Add("Chapter Head", GetType(String))
            dtFinal.Columns.Add("Chapter Desc", GetType(String))
            dtFinal.Columns.Add("Unit_code", GetType(String))
            dtFinal.Columns.Add("Sale_Invoice_No", GetType(String))
            dtFinal.Columns.Add("Sale_Invoice_Date", GetType(String))
            dtFinal.Columns.Add("Opening Qty", GetType(Decimal))
            dtFinal.Columns.Add("Qty", GetType(Decimal))
            dtFinal.Columns.Add("Total_Assessable_Amt", GetType(Decimal))
            dtFinal.Columns.Add("AssblAmt", GetType(Decimal))
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
            dtFinal.Columns.Add("ChapterFilter", GetType(String))
            dtFinal.Columns.Add("ItemFilter", GetType(String))
            Dim DrFinal As DataRow = dtFinal.NewRow()
            '*********** End DataTable
            Dim Dics As New Dictionary(Of String, String)
            Dim OpenBal As String = 0
            Dim CloseBal As String = 0

            Dim Address As String
            If chkLocSelect.IsChecked AndAlso cbgLoc.CheckedValue.Count = 1 Then
                Address = "(select TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_LOCATION_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_LOCATION_MASTER.State) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end  from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Location_Code  =max(xxxx.Location ) )   "
            Else
                Address = "(select TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end from TSPL_COMPANY_MASTER where TSPL_COMPANY_MASTER.Comp_Code =max(xxxx.Comp_Code) )"
            End If
            If cbgChapter.CheckedValue.Count > 0 Then
                ChapterFilter = clsCommon.GetMulcallString(cbgChapter.CheckedValue)
                ChapterFilter = ChapterFilter.Replace("'", "")
            End If
            If cbgLoc.CheckedValue.Count > 0 Then
                LocFilter = clsCommon.GetMulcallString(cbgLoc.CheckedValue)
                LocFilter = LocFilter.Replace("'", "")
            End If

            If cbgItem.CheckedValue.Count > 0 Then
                ItemFilter = clsCommon.GetMulcallString(cbgItem.CheckedValue)
                ItemFilter = ItemFilter.Replace("'", "")
            End If

            'Main query start here

            Dim Qry As String = " select  DISTINCT SUM(WHQty) as WHQty,'" + clsCommon.GetPrintDate(startDate, "dd/MM/yyyy") + "' AS StartDate, '" + clsCommon.GetPrintDate(EndDate, "dd/MM/yyyy") + "' AS [End Date], '" + startTime + "' AS [Start Time], '" + EndTime + "' AS [End Time],'" + ChapterFilter + "' as ChapterFilter,'" + LocFilter + "' as LocFilter,'" + ItemFilter + "' as ItemFilter," & _
                              " Sum(xxxx.OpBal ) as [Opening Qty],max(xxxx.Unit_code ) as Unit_code,    MAX(TSPL_ITEM_MASTER.Cheapter_Heads  ) as [Chapter Head],max(TSPL_CHAPTER_HEAD.Description  ) as [Chapter Desc]," & _
                              "  xxxx.Item_Code,  max(TSPL_ITEM_MASTER.Item_Desc) as Item_Desc ,  SUM(xxxx.Qty )  AS Qty, max(xxxx.Item_Assessable_Rate ) AS Item_Assessable_Rate, SUM(AssblAmt) as AssblAmt ," & _
                              " max(xxxx.TAX1_Rate) as TAX1_Rate, SUM(xxxx.TAX1_Amt ) as [TAX1_Amt], max(xxxx.TAX2_Rate) as TAX2_Rate, SUM(xxxx.TAX2_Amt )    as [TAX2_Amt], " & _
                              " max(xxxx.TAX3_Rate) as TAX3_Rate, sum(xxxx.TAX3_Amt)   as [TAX3_Amt], max(xxxx.MRP_Amt)  as MRP_Amt ,max(xxxx.Location) as Location  ,SUM(xxxx.ManuQty ) as [ManuQty],sum(xxxx.ClosingQty)  as ClosingQty , (select Comp_Name  from TSPL_COMPANY_MASTER where TSPL_COMPANY_MASTER.Comp_Code=max(xxxx.Comp_Code)  ) as compname," + Address + " as address " & _
                              " from(" + Environment.NewLine
            'query For sale invoice data
            Qry += "Select WHQty,OpBal, Comp_Code, Unit_code, [Chapter Head], [Chapter Desc], Sale_Invoice_No, Sale_Invoice_Date, Item_Code, Item_Desc, Qty,case when isnull(Qty,0)=0 then 0 else Item_Assessable_Rate/Qty end as Item_Assessable_Rate, ( Item_Assessable_Rate) as AssblAmt, TAX1_Rate, TAX1_Amt, TAX2_Rate, TAX2_Amt, TAX3_Rate, TAX3_Amt, Date_Time_Removal, MRP_Amt, Location, ManuQty, ClosingQty from (  "
            Qry += " SELECT  0 as WHQty,0 as [OpBal],TSPL_SD_SALE_INVOICE_HEAD .Comp_Code,  TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,(select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code) as [Chapter Head],  (select Description  from tspl_chapter_head where Chapter_Head_Code =(select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code)) as [Chapter Desc], TSPL_SD_SALE_INVOICE_HEAD.Document_Code as  Sale_Invoice_No,  TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [Sale_Invoice_Date], TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,  TSPL_ITEM_MASTER.Item_Desc   ,TSPL_SD_SALE_INVOICE_DETAIL.Qty  AS Qty,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Base_Amt as  Item_Assessable_Rate , TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt as [TAX1_Amt], TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt as [TAX2_Amt],     TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt as [TAX3_Amt], TSPL_SD_SALE_INVOICE_HEAD.Document_Date as  Date_Time_Removal,  TSPL_SD_SALE_INVOICE_DETAIL.MRP  as MRP_Amt ,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as Location  ,0 as [ManuQty],0 as ClosingQty   " + Environment.NewLine
            Qry += " FROM  TSPL_SD_SALE_INVOICE_HEAD " + Environment.NewLine
            Qry += " INNER JOIN TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code =   TSPL_SD_SALE_INVOICE_DETAIL.Document_Code " + Environment.NewLine
            Qry += " INNER JOIN TSPL_LOCATION_MASTER ON TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =   TSPL_LOCATION_MASTER.Location_Code " + Environment.NewLine
            Qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code" + Environment.NewLine
            Qry += " WHERE TSPL_SD_SALE_INVOICE_HEAD.Status=1 AND TSPL_LOCATION_MASTER.Excisable='T'" + Environment.NewLine
            Qry += " ) YYY "
            Qry += Environment.NewLine + " union all " + Environment.NewLine
            'Qry += " SELECT  0 as WHQty,0 as [OpBal],TSPL_SALE_INVOICE_HEAD .Comp_Code, ( select  replace(convert(varchar(50),Conversion_Factor,103),'.00','')+' BTLS IN EACH CASE'  from TSPL_ITEM_UOM_DETAIL " & _
            '                  " where Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code='FB') as Unit_code,   (select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =TSPL_SALE_INVOICE_DETAIL.Item_Code) as [Chapter Head], " & _
            '                  " (select Description  from tspl_chapter_head where Chapter_Head_Code =(select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =TSPL_SALE_INVOICE_DETAIL.Item_Code)) as [Chapter Desc]," & _
            '                  " TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,  TSPL_SALE_INVOICE_HEAD.Date_Time_Removal as [Sale_Invoice_Date], TSPL_SALE_INVOICE_DETAIL.Item_Code,  TSPL_SALE_INVOICE_DETAIL.Item_Desc ," & _
            '                  " (SELECT     CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB' THEN     TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB') ELSE TSPL_SALE_INVOICE_DETAIL.Invoice_Qty END AS Expr1)  AS Qty," & _
            '                  " CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB' THEN      TSPL_SALE_INVOICE_DETAIL.Item_Assessable_Rate * (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL" & _
            '                  " where Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB') ELSE TSPL_SALE_INVOICE_DETAIL.Item_Assessable_Rate END AS Item_Assessable_Rate ," & _
            '                  " TSPL_SALE_INVOICE_DETAIL.TAX1_Rate, TSPL_SALE_INVOICE_DETAIL.TAX1_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as [TAX1_Amt], TSPL_SALE_INVOICE_DETAIL.TAX2_Rate," & _
            '                  " TSPL_SALE_INVOICE_DETAIL.TAX2_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty    as [TAX2_Amt],     TSPL_SALE_INVOICE_DETAIL.TAX3_Rate, TSPL_SALE_INVOICE_DETAIL.TAX3_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as [TAX3_Amt]," & _
            '                  " TSPL_SALE_INVOICE_HEAD.Date_Time_Removal, case when TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FC' then   TSPL_SALE_INVOICE_DETAIL.MRP_Amt else  TSPL_SALE_INVOICE_DETAIL.MRP_Amt*(select Conversion_Factor  from " & _
            '                  " TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB') end as MRP_Amt ,TSPL_SALE_INVOICE_HEAD.Location  ,0 as [ManuQty],0 as ClosingQty   FROM " & _
            '                  " TSPL_SALE_INVOICE_HEAD INNER JOIN TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No =   TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN TSPL_LOCATION_MASTER ON TSPL_SALE_INVOICE_HEAD.Location =   TSPL_LOCATION_MASTER.Location_Code WHERE TSPL_SALE_INVOICE_HEAD   .Is_Post ='Y' AND TSPL_LOCATION_MASTER.Excisable='T' ) YYY"
            '                   "  union all "
            ''query For Warehouse leakage breakage data
            'Qry += " SELECT DISTINCT (Leakage_Qty + Breakage_Qty + Shortage_Qty) as WHQty,0 as [OP],hd.Comp_Code, ( select replace(convert(varchar(50),Conversion_Factor,103),'.00','')+' BTLS IN EACH CASE'  from TSPL_ITEM_UOM_DETAIL  where Item_Code=dl.Item_Code and UOM_Code='FB'), " & _
            '               "(select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =dl.Item_Code) as [Chapter Head]," & _
            '               "(select Description  from tspl_chapter_head where Chapter_Head_Code =(select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =dl.Item_Code)) as [Chapter Desc], " & _
            '               " hd.Document_No ,hd.Document_Date ,  dl.Item_Code ,dl.Item_Description ,0 as Item_Quantity ,0 as Assessable_Amt, 0 as AssblAmt , " & _
            '               " 0 as TAX1_Rate ,0 as TAX1_Amt  , 0 as TAX2_Rate,0 as TAX2_Amt ,  0 as TAX3_Rate,0 as TAX3_Amt , " & _
            '               " hd.Document_Date AS Date_Time_Removal , case when dl.Unit_Code  = 'FB' then " & _
            '               " dl.MRP else  dl.MRP/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code =dl.Item_Code and UOM_Code ='FB') end as MRP_Amt, " & _
            '               " hd.Loc_Code ,0 as [ManuQty],0 as ClosingQty    from TSPL_WH_BREAKAGE_HEAD hd inner join TSPL_WH_BREAKAGE_DETAIL dl on " & _
            '               " hd.Document_No=dl.Document_No  WHERE  hd.Is_Post=1  and dl.Unit_Code not in ('EC','EB','SH') " & _
            '                  " union all "
            ''query For Transfer data
            'Qry += " SELECT DISTINCT 0 as WHQty,0,hd.Comp_Code, ( select replace(convert(varchar(50),Conversion_Factor,103),'.00','')+' BTLS IN EACH CASE'  from TSPL_ITEM_UOM_DETAIL  where Item_Code=dl.Item_Code and UOM_Code='FB'), (select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =dl.Item_Code) as [Chapter Head]," & _
            '                  " (select Description  from tspl_chapter_head where Chapter_Head_Code =(select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =dl.Item_Code)) as [Chapter Desc], hd.Transfer_No ,hd.EntryDateTime ,  dl.Item_Code ,dl.Item_Desc ,dl.Item_Qty ,dl.Assessable_Amt, (dl.Item_Qty * dl.Assessable_Amt) as AssblAmt ,dl.TAX1_Rate ,dl.TAX1_Amt  ," & _
            '                  " dl.TAX2_Rate,dl.TAX2_Amt ,  dl.TAX3_Rate,dl.TAX3_Amt , hd.Date_Time_Removal AS Date_Time_Removal , case when dl.Uom  = 'FC' then   dl.MRP else  dl.MRP* (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code =dl.Item_Code and UOM_Code ='FB') end as MRP_Amt," & _
            '                  " hd.From_Location ,0 as [ManuQty],0 as ClosingQty    from TSPL_TRANSFER_HEAD hd inner join TSPL_TRANSFER_DETAIL dl on hd.Transfer_No=dl.Transfer_No  WHERE hd.Item_Type <>'Empty' AND hd.Post='Y'  AND Transfer_Type='LO' " & _
            '                  "  union all "
            'query For adjustment manufactured data
            Qry += " select 0 as WHQty,0, TSPL_ADJUSTMENT_HEADER.Comp_Code , ( select replace(convert(varchar(50),Conversion_Factor,103),'.00','')+' BTLS IN EACH CASE'  from TSPL_ITEM_UOM_DETAIL  where Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code and UOM_Code='FB'),  " & _
                              " (select  Cheapter_Heads  from TSPL_ITEM_MASTER  where Item_Code  =TSPL_ADJUSTMENT_DETAIL.Item_Code) as [Chapter Head],(select Description  from tspl_chapter_head where Chapter_Head_Code =(select  Cheapter_Heads  from TSPL_ITEM_MASTER " & _
                              " where Item_Code  =TSPL_ADJUSTMENT_DETAIL.Item_Code)) as [Chapter Desc]," & _
                              " TSPL_ADJUSTMENT_DETAIL.Adjustment_No,convert(datetime,TSPL_ADJUSTMENT_HEADER.EntryDateTime,103) ,TSPL_ADJUSTMENT_DETAIL.Item_Code,TSPL_ADJUSTMENT_DETAIL.Item_Description ,0 as qty,0 as Total_Assessable_Amt, 0 as AssblAmt, 0 ,0, 0 ,0, 0,0, '' AS Date_Time_Removal," & _
                              " case when TSPL_ADJUSTMENT_DETAIL.Unit_code = 'FC' then  TSPL_ADJUSTMENT_DETAIL.mrp else  TSPL_ADJUSTMENT_DETAIL.mrp* (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ADJUSTMENT_DETAIL.Item_Code and UOM_Code ='FB') end as MRP_Amt," & _
                              " Location_Code, case when Adjustment_Type='BI' then (Case When TSPL_ADJUSTMENT_DETAIL.Unit_Code='FB' Then TSPL_ADJUSTMENT_DETAIL.Item_Quantity/Conversion_Factor else TSPL_ADJUSTMENT_DETAIL.Item_Quantity End) Else Case when Adjustment_Type='BD' then (Case When TSPL_ADJUSTMENT_DETAIL.Unit_Code='FB' Then (TSPL_ADJUSTMENT_DETAIL.Item_Quantity/Conversion_Factor)*-1 Else TSPL_ADJUSTMENT_DETAIL.Item_Quantity*-1  end) end End as [ManuQty],0 as ClosingQty " & _
                              " from TSPL_ADJUSTMENT_DETAIL inner join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_DETAIL.Adjustment_No =TSPL_ADJUSTMENT_HEADER .Adjustment_No" & _
                              " LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code " & _
                              " where Adjustment_Type in ('BI','BD') and TSPL_ADJUSTMENT_HEADER.ItemType='FM'  AND  TSPL_ADJUSTMENT_HEADER.Posted='Y'  and TSPL_ADJUSTMENT_DETAIL.Unit_Code not in ('EC','EB','SH') AND TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'  "
            Qry += Environment.NewLine + " Union all " + Environment.NewLine
            'main query For Opening balance data
            Qry += " select  0 as WHQty,SUM(Qty ),max(xxx.Comp_Code  ),'', '',   '' as [Chapter Desc],  '','" + startDate + "' ,xxx.Item_Code,'' ,0 as qty,0 as Total_Assessable_Amt, 0 as AssblAmt, 0 ,0, 0 ,0, 0,0, '' AS Date_Time_Removal,xxx.MRP_Amt  as MRP_Amt,xxx.Location ,0   as [ManuQty],0 as ClosingQty  " & _
                              " from ( "
            Qry += " SELECT  TSPL_SD_SALE_INVOICE_DETAIL.MRP as MRP_Amt, TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as Location, TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Sale_Invoice_Date,  TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, -1*   TSPL_SD_SALE_INVOICE_DETAIL.Qty  AS Qty ,TSPL_SD_SALE_INVOICE_HEAD.Comp_Code FROM  TSPL_SD_SALE_INVOICE_HEAD INNER JOIN    TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code  =  TSPL_SD_SALE_INVOICE_DETAIL.Document_Code INNER JOIN    TSPL_LOCATION_MASTER ON TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location  =   TSPL_LOCATION_MASTER.Location_Code where (convert(datetime,Document_Date,103) < '" + startDate + "') and   TSPL_SD_SALE_INVOICE_HEAD.Status=1   "
            Qry += Environment.NewLine + " Union all " + Environment.NewLine
            'query For Sale invoice Opening balance data
            'Qry += "SELECT case when TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB'  then   TSPL_SALE_INVOICE_DETAIL.MRP_Amt else  TSPL_SALE_INVOICE_DETAIL.MRP_Amt/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where  Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB') end as MRP_Amt," & _
            '                  " TSPL_SALE_INVOICE_HEAD.Location, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,  TSPL_SALE_INVOICE_DETAIL.Item_Code, -1*  (SELECT     CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB' THEN    TSPL_SALE_INVOICE_DETAIL.Invoice_Qty /  (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code ='FB') ELSE TSPL_SALE_INVOICE_DETAIL.Invoice_Qty END AS Expr1)  AS Qty ,TSPL_SALE_INVOICE_HEAD.Comp_Code" & _
            '                  " FROM  TSPL_SALE_INVOICE_HEAD INNER JOIN    TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No =  TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN    TSPL_LOCATION_MASTER ON TSPL_SALE_INVOICE_HEAD.Location =   TSPL_LOCATION_MASTER.Location_Code where" & _
            '                  " (convert(datetime,Date_Time_Removal,103) < '" + startDate + "') and   TSPL_SALE_INVOICE_HEAD.Is_Post='Y' "
            '                  " union all "
            ''query For Transfer Opening balance data
            'Qry += " SELECT case when dl.Uom  = 'FB' then   dl.MRP else  dl.MRP/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL  where Item_Code =dl.Item_Code and UOM_Code ='FB') end as MRP_Amt, hd.From_Location,  hd.Posting_Date ,  dl.Item_Code ,dl.Item_Qty *-1,hd .Comp_Code  " & _
            '                  " from TSPL_TRANSFER_HEAD hd inner join TSPL_TRANSFER_DETAIL dl on hd.Transfer_No=dl.Transfer_No where  (convert(datetime,hd.EntryDateTime,103) < '" + startDate + "') and  hd.Post='Y' and hd.Item_Type ='Full' " & _
            '                  " union all "
            ''query For Warehouse leakage breakage Opening balance data
            'Qry += "SELECT  case when dl.Unit_Code  = 'FB' then   dl.MRP else  dl.MRP/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL " & _
            '               "where Item_Code =dl.Item_Code and UOM_Code ='FB') end as MRP_Amt, hd.Loc_Code,  hd.Posting_Date ,  dl.Item_Code , " & _
            '               "(Leakage_Qty + Breakage_Qty + Shortage_Qty) *-1 ,hd .Comp_Code  from TSPL_WH_BREAKAGE_HEAD hd inner join TSPL_WH_BREAKAGE_DETAIL dl on " & _
            '               "hd.Document_No=dl.Document_No where  (convert(datetime,hd.Document_Date,103) < '" + startDate + "') and " & _
            '               "hd.Is_Post=1  and dl.Unit_Code not in ('EC','EB','SH') " & _
            '                  " union all "
            'query For adjustment Opening balance data
            Qry += " select  case when TSPL_ADJUSTMENT_DETAIL.Unit_code = 'FB' then " & _
                              " TSPL_ADJUSTMENT_DETAIL.mrp else  TSPL_ADJUSTMENT_DETAIL.mrp/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ADJUSTMENT_DETAIL.Item_Code and UOM_Code ='FB') end as MRP_Amt , TSPL_ADJUSTMENT_HEADER.Loc_Code, convert(datetime,TSPL_ADJUSTMENT_HEADER.Posting_Date,103) ,TSPL_ADJUSTMENT_DETAIL.Item_Code,case when Adjustment_Type='BI' then " & _
                              " TSPL_ADJUSTMENT_DETAIL.Item_Quantity  when Adjustment_Type='BD'  then TSPL_ADJUSTMENT_DETAIL.Item_Quantity*-1 end,TSPL_ADJUSTMENT_HEADER.Comp_Code   from TSPL_ADJUSTMENT_DETAIL inner join TSPL_ADJUSTMENT_HEADER on  TSPL_ADJUSTMENT_DETAIL.Adjustment_No =TSPL_ADJUSTMENT_HEADER .Adjustment_No   where Adjustment_Type in ('BI','BD') and TSPL_ADJUSTMENT_HEADER.Posted='Y'  and TSPL_ADJUSTMENT_HEADER.ItemType='FM' " & _
                              " and convert(datetime,TSPL_ADJUSTMENT_HEADER.EntryDateTime,103) < '" + startDate + "'  and TSPL_ADJUSTMENT_DETAIL.Unit_Code not in ('EC','EB','SH')  "
            Qry += "   ) as xxx group by xxx.Item_Code ,xxx.Location,xxx.MRP_Amt " & _
                              " ) as xxxx  inner join TSPL_ITEM_MASTER on xxxx.Item_Code =TSPL_ITEM_MASTER.Item_Code  left outer join TSPL_CHAPTER_HEAD on xxxx.[Chapter Head] =TSPL_CHAPTER_HEAD.Chapter_Head_Code " & _
                              "  where    (Sale_Invoice_Date >=  convert(datetime,'" + startDate + "',103)) AND  (Sale_Invoice_Date <=  convert(datetime,'" + EndDate + "',103))  "

            If chkChapterSelect.IsChecked Then
                If cbgChapter.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please Select Atleast One Item Chapter Head Code.")
                    Return
                End If
                Qry += " and xxxx.[Chapter Head] in  (" + clsCommon.GetMulcallString(cbgChapter.CheckedValue) + ")  "
            End If


            If chkLocSelect.IsChecked Then
                If cbgLoc.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please Select Atleast One Location Code.")
                    Return
                End If
                Qry += " and xxxx.Location in   (" + clsCommon.GetMulcallString(cbgLoc.CheckedValue) + ")  "
            Else
                Qry += " and xxxx.Location in   (" + clsCommon.GetMulcallString(cbgLoc.AllValue) + ")  "
            End If


            If chkItmSelect.IsChecked Then
                If cbgItem.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please Select Atleast One Item Code.")
                    Return
                End If
                Qry += " and xxxx.Item_Code in  (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")  "
            End If

            'Qry = Qry + " and xxxx.Item_Code in (select Item_Code  from TSPL_ITEM_MASTER where Item_Type ='F')"
            Qry = Qry + " group by xxxx.Item_Code      "

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
                ' DrFinal.Item("Sale_Invoice_Date") = Format(dr("Sale_Invoice_Date"), "dd/MM/yyyy") + " " + Format(dr("Date_Time_Removal"), "hh:mm:ss")
                DrFinal.Item("Opening Qty") = OpenBal
                DrFinal.Item("Qty") = Math.Round(CDec(dr("Qty").ToString()), 2)
                DrFinal.Item("WHQty") = Math.Round(CDec(dr("WHQty").ToString()), 2)
                DrFinal.Item("Item_Code") = dr("Item_Code").ToString()
                DrFinal.Item("Chapter Desc") = dr("Chapter Desc").ToString()
                DrFinal.Item("Chapter Head") = dr("Chapter Head").ToString()
                DrFinal.Item("Unit_code") = dr("Unit_code").ToString()
                DrFinal.Item("Item_Desc") = dr("Item_Desc").ToString()
                DrFinal.Item("Tax1_Rate") = dr("Tax1_Rate").ToString()
                DrFinal.Item("Tax2_Rate") = dr("Tax2_Rate").ToString()
                DrFinal.Item("Tax3_Rate") = dr("Tax3_Rate").ToString()
                DrFinal.Item("Tax1_Amt") = dr("Tax1_Amt").ToString()
                DrFinal.Item("Tax2_Amt") = dr("Tax2_Amt").ToString()
                DrFinal.Item("Tax3_Amt") = dr("Tax3_Amt").ToString()
                DrFinal.Item("ManuQty") = dr("ManuQty").ToString()
                DrFinal.Item("MRP_Amt") = clsCommon.myCdbl(dr("MRP_Amt").ToString())
                DrFinal.Item("StartDate") = dr("StartDate").ToString()
                DrFinal.Item("End Date") = dr("End Date").ToString()
                DrFinal.Item("start Time") = dr("start Time").ToString()
                DrFinal.Item("End Time") = dr("End Time").ToString()
                DrFinal.Item("compname") = dr("compname").ToString()
                DrFinal.Item("address") = dr("address").ToString()
                DrFinal.Item("LocFilter") = dr("LocFilter").ToString()
                DrFinal.Item("ChapterFilter") = dr("ChapterFilter").ToString()
                DrFinal.Item("ItemFilter") = dr("ItemFilter").ToString()

                ' DrFinal.Item("Sale_Invoice_No") = dr("Sale_Invoice_No").ToString()
                DrFinal.Item("Total_Assessable_Amt") = Math.Round(CDec(dr("Item_Assessable_Rate").ToString()), 2)
                DrFinal.Item("AssblAmt") = Math.Round(CDec(dr("AssblAmt")), 2)
                CloseBal = 0
                CloseBal = CDec(OpenBal) - CDec(dr("Qty").ToString()) - CDec(dr("WHQty").ToString()) + CDec(dr("ManuQty").ToString())
                If CloseBal < 0 Then
                    CloseBal = CloseBal * -1
                End If
                DrFinal.Item("ClosingQty") = CloseBal
                dtFinal.Rows.Add(DrFinal)
            Next
            dtFinal.AcceptChanges()
            Dim CrptName As String = ""
            If rdbChapterWise.IsChecked = True Then
                CrptName = "crptChapterHeadWiseDemo"
            ElseIf rdbER1.IsChecked = True Then
                CrptName = "crptER1Demo"
            End If

            Dim FRMcrys As New frmCrystalReportViewer
            FRMcrys.funreport(CrystalReportFolder.CommonServices, dtFinal, CrptName, "Excise Report")

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub chkChapterAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkChapterAll.ToggleStateChanged
        cbgChapter.Enabled = Not chkChapterAll.IsChecked
    End Sub
    Private Sub chkItmAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItmAll.ToggleStateChanged
        cbgItem.Enabled = Not chkItmAll.IsChecked
    End Sub
    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLoc.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub toDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles toDate.LostFocus
        Dim strDate As String
        strDate = (toDate.Value.ToShortTimeString)
        If strDate = "12:00 AM" Then
            toDate.Value = clsCommon.GetDateWithEndTime(toDate.Value)
        End If
    End Sub
End Class

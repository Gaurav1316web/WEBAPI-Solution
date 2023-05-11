''--07/08/2013_11:00AM--Created By--[Pankaj Kumar]
'---Updation By--[Pankaj  Kumar Chaudhary]----Against Ticket No.---[BM00000000745,BM00000000744,BM00000000862, BM00000001153,BM00000001190,BM00000001359
'                 ,BM00000001359]
'---Preeti gupta-ticket no-[BM00000003138]

Imports common
Imports System.Data.SqlClient

Public Class FrmStoresLedgerNew
    Inherits FrmMainTranScreen

    Dim dt As DataTable
    Dim qry As String = ""

    Private Sub FrmStoresLedgerNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        rbtnCategoryAll.IsChecked = True
        LoadLocation()
        LoadItem()
        LoadCategory()

        Reset()
        SetUserMgmtNew()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnStoresLedger)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Sub LoadLocation()
        Dim qry As String = " select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location_Code"
        cbgLocation.DisplayMember = "Location_Desc"
    End Sub

    Sub LoadItem()
        Dim qry As String = " select TSPL_ITEM_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc  from TSPL_ITEM_MASTER Where Item_Type  <> 'F' "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Item_Code"
        cbgLocation.DisplayMember = "Item_Desc"
    End Sub

    Sub LoadCategory()
        Dim qry As String = "select Code,Name,Parent from ("
        qry += " select ITEM_CATEGORY_STRUCT_CODE as Code,DESCRIPTION as Name, null as Parent,0 as Sno from TSPL_ITEM_CATEGORY_STRUCTURE"
        qry += " union all"
        qry += " select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE as Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as Name,ITEM_CATEGORY_STRUCT_CODE as Parent,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL as SNo from TSPL_ITEM_CATEGORY_STRUCT_DETAIL"
        qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE"
        qry += " Union all"
        qry += " select CODE,DESCRIPTION as Name,ITEM_CATEGORY_CODE as Parent,100 as SNo from TSPL_ITEM_CATEGORY_LEVEL_VALUES"
        qry += " )xxx order by Sno"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        tvCategory.DataSource = Nothing
        tvCategory.TreeViewElement.AutoSizeItems = True
        tvCategory.ShowLines = True
        tvCategory.ShowRootLines = True
        tvCategory.TreeViewElement.ViewElement.Margin = New Padding(4)
        tvCategory.ShowExpandCollapse = True
        tvCategory.TreeIndent = 15
        tvCategory.FullRowSelect = False
        tvCategory.ShowLines = True
        tvCategory.LineStyle = TreeLineStyle.Dot
        tvCategory.LineColor = Color.FromArgb(110, 153, 210)
        tvCategory.ExpandAnimation = ExpandAnimation.Opacity
        tvCategory.AllowEdit = False
        tvCategory.ShowRootLines = False
        tvCategory.TreeViewElement.AllowAlternatingRowColor = True
        tvCategory.TreeViewElement.AlternatingRowColor = Color.AliceBlue

        tvCategory.TreeViewElement.DrawBorder = True
        tvCategory.ValueMember = "Code"
        tvCategory.DisplayMember = "Name"
        tvCategory.ChildMember = "Code"
        tvCategory.ParentMember = "Parent"
        tvCategory.DataSource = dt
        tvCategory.CheckBoxes = True

        tvCategory.ExpandAll()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Sub Reset()
        dtpFrmDate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        chkItemAll.IsChecked = True
        chkLocAll.IsChecked = True
        rbtnCategoryAll.IsChecked = True
        LoadCategory()
        rdbDetail.IsChecked = True
        chkWithoutVal.Checked = False
        chkDetailed.Checked = True
        rbtnSRN.IsChecked = True
    End Sub

    Private Sub btnPrint_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            PrintData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub PrintData()
        Try
            If chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count = 0 Then
                Throw New Exception("Please select atleast single Item or select all.")
            ElseIf chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
                Throw New Exception("Please select atleast single Location or select all.")
            
            End If
            Dim DocType As String = "'SRN', 'ISSTRAN','ScrapIn','ISSTRAN', 'Purchase Return','RGP','NRGP', 'IC-AD'"
            If rbtnPurchase.IsChecked Then
                DocType = "'ISSTRAN','ScrapIn','ISSTRAN', 'Purchase Return','RGP','NRGP', 'IC-AD'"
            End If
            Dim stratDate As String = clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy")
            Dim endDate As String = clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy")
            qry = "Select distinct '" + stratDate + "' as StartDate, '" + endDate + "' as EndDate, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' as Printdate," ', TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code as Category_Code, TSPL_ITEM_CATEGORY_LEVEL.description as Category_Name,
            qry += " xxxx.Item_Code, xxxx.Item_Desc, Unit_Code, Source_Doc_Date, " 'TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values as Sub_Category_Code, TSPL_ITEM_CATEGORY_LEVEL_VALUES.description as Description, 
            qry += " CONVERT(date,Source_Doc_Date,103) as OrderDate, xxxx.Location_Code, DocNo , Location_Desc , RIType, RI, "
            qry += " (Case When InOut='I' Then 'R'  Else CAse When InOut='O' Then 'I' End end ) as Type,"
            qry += " ISNULL((Case When inOut='' Then Qty  End), 0) as BalQty, ISNULL((Case When inOut='' AND Qty>0 Then Net_Cost/Qty else 0 End), 0) as BalRate, ISNULL((Case When inOut='' Then Net_Cost   End), 0) as BalValue, "
            qry += " (Case When inOut='I' Then Qty Else 0 End) as RcptQty, (Case When inOut='I' AND Qty>0 Then Net_Cost/Qty else 0 End) as RcptRate, (Case When inOut='I' Then Net_Cost Else 0 End) as RcptValue, "
            qry += " (Case When inOut='O' Then Qty Else  0 End) as IssueQty, (Case When inOut='O' AND Qty>0 Then Net_Cost/Qty else 0 End) as IssueRate, (Case When inOut='O' Then Net_Cost Else 0 End) as IssueValue, "
            qry += " Case When InOut='I' Then (Case When RIType='IC-AD' Then Qty Else 0 End) Else 0 END as AdjIn_Qty, Case When InOut='I' Then (Case When RIType='IC-AD' AND Qty>0 Then Net_Cost/Qty Else 0 end) Else 0 END as AdjIn_Rate, Case When InOut='I' Then (Case When RIType='IC-AD' Then Net_Cost Else 0 End) Else 0 END as AdjIn_Value,"
            qry += " Case When InOut='O' Then (Case When RIType='IC-AD' Then Qty Else 0 End) Else 0 END as AdjOut_Qty, Case When InOut='O' Then (Case When RIType='IC-AD' AND Qty>0 Then Net_Cost/Qty Else 0 end) Else 0 END as AdjOut_Rate, Case When InOut='O' Then (Case When RIType='IC-AD' Then Net_Cost Else 0 End) Else 0 END as AdjOut_Value,"
            qry += " Case When RIType='SRN' Then Qty Else 0 End as QTY_P, Case When RIType='SRN' AND Qty>0 Then Net_Cost/Qty Else 0 end as SRN_Rate, Case When RIType='SRN' Then Net_Cost Else 0 End as VAL_P,"
            qry += " Case When RIType = ('ISSTRAN') AND InOut='O' Then Qty Else 0 End as ISS_Q, Case When RIType = ('ISSTRAN') AND InOut='O' AND Qty>0 Then  Net_Cost/Qty Else 0 End as ISS_Rate, Case When RIType = ('ISSTRAN') AND InOut='O' Then Net_Cost Else 0 End as VAL_I, 	 "
            qry += " Case When RIType = ('ISSTRAN') AND InOut='I' Then Qty Else 0 End as QTY_IR, Case When RIType = ('ISSTRAN') AND InOut='I' AND Qty>0 Then  Net_Cost/Qty Else 0 End as Rate_IR, Case When RIType = ('ISSTRAN') AND InOut='I' Then Net_Cost Else 0 End as VAL_IR, 	"
            qry += " Case When RIType='Purchase Return' Then Qty Else 0 End as QTY_PR, Case When RIType='Purchase Return' AND Qty>0 Then Net_Cost/Qty Else 0 End as Rate_PR, Case When RIType='Purchase Return' Then Net_Cost Else 0 End as VAL_PR, "
            qry += " Case When RIType='ScrapIn' Then Qty Else 0 End as QTY_MS, Case When RIType='ScrapIn' AND Qty>0 Then Net_Cost/Qty Else 0 End as Rate_MS, Case When RIType='ScrapIn' Then Net_Cost Else 0 End as VAL_MS, "
            qry += " Case When RIType='RGP' Then Qty Else 0 End as QTY_RGP, Case When RIType='RGP' AND Qty>0 Then Net_Cost/Qty Else 0 End as Rate_RGP, Case When RIType='RGP' Then Net_Cost Else 0 End as VAL_RGP, "
            qry += " Case When RIType='NRGP' Then Qty Else 0 End as QTY_NRGP, Case When RIType='NRGP' AND Qty>0 Then Net_Cost/Qty Else 0 End as Rate_NRGP, Case When RIType='NRGP' Then Net_Cost Else 0 End as VAL_NRGP,"
            qry += " RowNo, Party, Dept_Desc,PR_Remarks from  ("
            qry += " Select MAX(item_category) as item_category, MAX(Sub_item_category) as Sub_item_category, Item_Code, MAX(Item_Desc) as Item_Desc, Unit_Code, '' as Source_Doc_Date, Location_Code, Max(Location_Desc) as Location_Desc, '' as RIType, '' as RI, '' as InOut, 'Opening Balance' as [DocNo], SUM(Qty * case when InOut='I' then 1 else -1 end) as Qty, SUM(Net_Cost * case when InOut='I' then 1 else -1 end) as Net_Cost, 0 as RowNo, '' as Party, '' as Dept_Desc, MAX(Comp_Code) as Comp_Code ,MAX(PR_Remarks) as PR_Remarks from ("
            qry += " select TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code as item_category, TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values as Sub_item_category, TSPL_INVENTORY_MOVEMENT.Item_Code, TSPL_INVENTORY_MOVEMENT.Item_Desc,TSPL_INVENTORY_MOVEMENT.UOM as Unit_Code , TSPL_INVENTORY_MOVEMENT.Source_Doc_Date, TSPL_LOCATION_MASTER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, '' as RIType, '' as RI, TSPL_INVENTORY_MOVEMENT.inOut, TSPL_INVENTORY_MOVEMENT.Source_Doc_No , TSPL_INVENTORY_MOVEMENT.qty, TSPL_INVENTORY_MOVEMENT.Net_Cost, TSPL_INVENTORY_MOVEMENT.comp_code,case when TSPL_INVENTORY_MOVEMENT.Trans_Type='Purchase Return' then (Select Remarks  from TSPL_PR_HEAD where TSPL_PR_HEAD.PR_No = TSPL_INVENTORY_MOVEMENT.Source_Doc_No )    else '' end as PR_Remarks from TSPL_INVENTORY_MOVEMENT "
            qry += " Left outer Join TSPL_LOCATION_MASTER on TSPL_INVENTORY_MOVEMENT.Location_Code=TSPL_LOCATION_MASTER.Location_Code "
            qry += " Left Outer  Join TSPL_SRN_HEAD on TSPL_INVENTORY_MOVEMENT.Source_Doc_No=TSPL_SRN_HEAD.SRN_No "
            qry += " Left Outer Join TSPL_SCRAPINVOICE_HEAD ON TSPL_INVENTORY_MOVEMENT.Source_Doc_No=TSPL_SCRAPINVOICE_HEAD.invoice_No"
            qry += " LEFT OUTER JOIN  TSPL_IssueReturn_HEAD on TSPL_INVENTORY_MOVEMENT.Source_Doc_No= TSPL_IssueReturn_HEAD.Doc_No"
            qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code"
            qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.item_code=  TSPL_ITEM_MASTER.item_code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.code= TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values"
            qry += " Where CONVERT(Date, Source_Doc_Date, 103) < CONVERT(Date, '" + stratDate + "', 103) AND TSPL_INVENTORY_MOVEMENT.Trans_Type in (" + DocType + ") AND TSPL_LOCATION_MASTER.Location_Type='Physical' AND TSPL_ITEM_MASTER.Item_Type <>'F' AND ISNULL(TSPL_SCRAPINVOICE_HEAD.NRG_No,'')=''"
            qry += " ) xxx Group By Item_Code, Unit_Code, Location_Code   "
            qry += " Union All "
            qry += " select TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code as item_category, TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values as Sub_item_category, TSPL_INVENTORY_MOVEMENT.Item_Code, TSPL_INVENTORY_MOVEMENT.Item_Desc,TSPL_INVENTORY_MOVEMENT.UOM as Unit_Code, TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Location_Desc as  Location_Code, TSPL_INVENTORY_MOVEMENT.Trans_Type as RIType, '' as RI,  TSPL_INVENTORY_MOVEMENT.InOut, TSPL_INVENTORY_MOVEMENT.Source_Doc_No , TSPL_INVENTORY_MOVEMENT.qty, TSPL_INVENTORY_MOVEMENT.Net_Cost,1 as RowNo, TSPL_SRN_HEAD.Vendor_Name as Party, (Case When ISNULL(TSPL_IssueReturn_HEAD.Dept_Desc, '')='' Then'' else TSPL_IssueReturn_HEAD.Dept_Desc End  ) + ( Case When ISNULL(TSPL_SRN_HEAD.Dept_Desc, '')='' Then'' else TSPL_SRN_HEAD.Dept_Desc End  ) as Dept_Desc, TSPL_INVENTORY_MOVEMENT.Comp_Code,case when TSPL_INVENTORY_MOVEMENT.Trans_Type='Purchase Return' then (Select Remarks  from TSPL_PR_HEAD where TSPL_PR_HEAD.PR_No = TSPL_INVENTORY_MOVEMENT.Source_Doc_No )    else '' end as PR_Remarks from TSPL_INVENTORY_MOVEMENT "
            qry += " Left outer Join TSPL_LOCATION_MASTER on TSPL_INVENTORY_MOVEMENT.Location_Code=TSPL_LOCATION_MASTER.Location_Code "
            qry += " Left Outer  Join TSPL_SRN_HEAD on TSPL_INVENTORY_MOVEMENT.Source_Doc_No=TSPL_SRN_HEAD.SRN_No "
            qry += " Left Outer Join TSPL_SCRAPINVOICE_HEAD ON TSPL_INVENTORY_MOVEMENT.Source_Doc_No=TSPL_SCRAPINVOICE_HEAD.invoice_No"
            qry += " LEFT OUTER JOIN  TSPL_IssueReturn_HEAD on TSPL_INVENTORY_MOVEMENT.Source_Doc_No= TSPL_IssueReturn_HEAD.Doc_No"
            qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code "
            qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.item_code=  TSPL_ITEM_MASTER.item_code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.code= TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values"
            qry += " Where CONVERT(Date, Source_Doc_Date, 103)>= CONVERT(Date, '" + stratDate + "', 103) And CONVERT(Date, Source_Doc_Date, 103)<= CONVERT(Date, '" + endDate + "', 103) "
            qry += " AND TSPL_INVENTORY_MOVEMENT.Trans_Type in (" + DocType + ") AND TSPL_LOCATION_MASTER.Location_Type='Physical'AND TSPL_ITEM_MASTER.Item_Type <>'F' AND ISNULL(TSPL_SCRAPINVOICE_HEAD.NRG_No,'')=''"
            If rbtnPurchase.IsChecked Then
                qry += " Union All  "
                qry += " select TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code as item_category, TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values as Sub_item_category, TSPL_INVENTORY_MOVEMENT.Item_Code, TSPL_INVENTORY_MOVEMENT.Item_Desc, TSPL_INVENTORY_MOVEMENT.UOM, TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Location_Desc as  Location_Code, TSPL_INVENTORY_MOVEMENT.Trans_Type as RIType, '' as RI,  TSPL_INVENTORY_MOVEMENT.InOut, TSPL_PI_HEAD.PI_No As Source_Doc_No , TSPL_PI_DETAIL.PI_Qty As Qty, TSPL_PI_DETAIL.Item_Net_Amt,1 as RowNo, TSPL_SRN_HEAD.Vendor_Name as Party, (Case When ISNULL(TSPL_IssueReturn_HEAD.Dept_Desc, '')='' Then'' else TSPL_IssueReturn_HEAD.Dept_Desc End  ) + ( Case When ISNULL(TSPL_SRN_HEAD.Dept_Desc, '')='' Then'' else TSPL_SRN_HEAD.Dept_Desc End  ) as Dept_Desc, TSPL_INVENTORY_MOVEMENT.Comp_Code,case when TSPL_INVENTORY_MOVEMENT.Trans_Type='Purchase Return' then (Select Remarks  from TSPL_PR_HEAD where TSPL_PR_HEAD.PR_No = TSPL_INVENTORY_MOVEMENT.Source_Doc_No )    else '' end as PR_Remarks from TSPL_INVENTORY_MOVEMENT  "
                qry += " Left outer Join TSPL_LOCATION_MASTER on TSPL_INVENTORY_MOVEMENT.Location_Code=TSPL_LOCATION_MASTER.Location_Code  "
                qry += " Left Outer  Join TSPL_SRN_HEAD on TSPL_INVENTORY_MOVEMENT.Source_Doc_No=TSPL_SRN_HEAD.SRN_No "
                qry += " LEFT OUTER JOIN TSPL_PI_HEAD ON TSPL_PI_HEAD.Against_SRN=TSPL_SRN_HEAD.SRN_No "
                qry += " LEFT OUTER JOIN TSPL_PI_DETAIL ON TSPL_PI_DETAIL.PI_No=TSPL_PI_HEAD.PI_No AND TSPL_PI_DETAIL.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code AND TSPL_PI_DETAIL.Unit_code=TSPL_INVENTORY_MOVEMENT.UOM "
                qry += " LEFT OUTER JOIN  TSPL_IssueReturn_HEAD on TSPL_INVENTORY_MOVEMENT.Source_Doc_No= TSPL_IssueReturn_HEAD.Doc_No "
                qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code  "
                qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.item_code=  TSPL_ITEM_MASTER.item_code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.code= TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values"
                qry += " Where CONVERT(Date, Source_Doc_Date, 103)>= CONVERT(Date, '" + stratDate + "', 103) And CONVERT(Date, Source_Doc_Date, 103)<= CONVERT(Date, '" + endDate + "', 103)  AND TSPL_INVENTORY_MOVEMENT.Trans_Type ='SRN' AND TSPL_LOCATION_MASTER.Location_Type='Physical' AND ISNULL(TSPL_PI_HEAD.Against_SRN,'')<>''"
            End If
            qry += " )xxxx LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code=item_category"
            qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code"
            qry += " Left OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.code = Sub_item_category and TSPL_ITEM_CATEGORY_LEVEL_VALUES.code= TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code"
            qry += " Where 1 = 1 "

            If chkLocSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
                qry += " AND xxxx.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
            End If
            If chkItemSelect.IsChecked And cbgItem.CheckedValue.Count > 0 Then
                qry += " AND xxxx.Item_Code In (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
            End If
            'If chkItemCatSelect.IsChecked And cbgItmCategory.CheckedValue.Count > 0 Then
            '    qry += " AND item_category in (" + clsCommon.GetMulcallString(cbgItmCategory.CheckedValue) + ")"
            'End If
            'If chkItemSubCatSelect.IsChecked And cbgItemSubCategory.CheckedValue.Count > 0 Then
            '    qry += " And Sub_Category_Code in (" + clsCommon.GetMulcallString(cbgItemSubCategory.CheckedValue) + ")"
            'End If

            Dim whrcate As String = ""

            If rbtnCategorySelect.IsChecked Then
                Dim isFirstTime As Boolean = True
                qry += " and exists (select 1  from TSPL_ITEM_MASTER_CATEGORY where Item_code in (select distinct item_code from TSPL_INVENTORY_MOVEMENT) and ( " + Environment.NewLine
                For Each Ctr As RadTreeNode In tvCategory.CheckedNodes
                    If (Ctr.Checked) And Ctr.Parent IsNot Nothing Then
                        If Not isFirstTime Then
                            qry += " or "
                            whrcate += " or "
                        End If
                        qry += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
                        whrcate += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
                        isFirstTime = False
                    End If
                Next
                qry += " ))"
                If isFirstTime Then
                    Throw New Exception("Please select at least one Category")
                End If
            End If


            If clsCommon.myLen(whrcate) > 0 Then
                whrcate = " and " + whrcate
            End If

            qry = "select axa.*,( select distinct (select ','+TSPL_ITEM_CATEGORY_LEVEL.description, ','+TSPL_ITEM_CATEGORY_LEVEL_VALUES.description from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code where TSPL_ITEM_MASTER_CATEGORY.Item_code=axa.Item_Code " + whrcate + " for XML path(''))) as Category from (" + qry + ")axa"

            If rdbtnSummary.IsChecked Then
                qry = " Select StartDate, EndDate, Printdate, Item_Code, Item_Desc, Unit_Code, BalQty, BalRate, BalValue, RcptQty, RcptRate, RcptValue, IssueQty, IssueRate, IssueValue, AdjIn_Qty, AdjIn_Rate, AdjIn_Value, AdjOut_Qty, AdjOut_Rate, AdjOut_Value, QTY_P, SRN_Rate, VAL_P, ISS_Q, CONVERT(Decimal(18,2),(ISS_Q*ClosingRate)) as VAL_I, QTY_IR, CONVERT(Decimal(18,2),(QTY_IR*ClosingRate)) as VAL_IR, QTY_PR, VAL_PR, QTY_MS, VAL_MS, QTY_RGP, VAL_RGP, QTY_NRGP, VAL_NRGP, ClosingQty, Case When ClosingQty>0 Then ClosingBal Else 0 End As ClosingBal from ( Select MAX(StartDate) as StartDate, MAX(EndDate) as EndDate, MAX(Printdate) as Printdate, Item_Code, MAX(Item_Desc) as Item_Desc, Unit_Code, SUM(BalQty) as BalQty, Case When SUM(BalQty)=0 Then 0 Else SUM(BalValue)/SUM(BalQty) End as BalRate, SUM(BalValue) as BalValue, SUM(RcptQty) as RcptQty, Case When SUM(RcptQty)=0 Then 0 Else SUM(RcptValue)/SUM(RcptQty) End as RcptRate, SUM(RcptValue) as RcptValue, SUM(IssueQty) as IssueQty, case When SUM(IssueQty)=0 Then 0 Else SUM(IssueValue)/SUM(IssueQty) End as IssueRate, SUM(IssueValue) as IssueValue, SUM(QTY_P) as QTY_P, Case When SUM(QTY_P)=0 Then 0 else SUM(VAL_P)/SUM(QTY_P) End as SRN_Rate, SUM(VAL_P) as VAL_P, SUM(AdjIn_Qty) as AdjIn_Qty, Case When SUM(AdjIn_Qty)=0 Then 0 else SUM(AdjIn_Value)/SUM(AdjIn_Qty) End as AdjIn_Rate, SUM(AdjIn_Value) as AdjIn_Value, SUM(AdjIn_Qty) as AdjOut_Qty, Case When SUM(AdjOut_Qty)=0 Then 0 else SUM(AdjOut_Value)/SUM(AdjOut_Qty) End as AdjOut_Rate, SUM(AdjOut_Value) as AdjOut_Value, SUM(ISS_Q) as ISS_Q, SUM(VAL_I) as VAL_I, SUM(QTY_IR) as QTY_IR, SUM(VAL_IR) as VAL_IR, SUM(QTY_PR) as QTY_PR, SUM(VAL_PR) as VAL_PR, SUM(QTY_MS) as QTY_MS, SUM(VAL_MS) as VAL_MS, SUM(QTY_RGP) as QTY_RGP, SUM(VAL_RGP) as VAL_RGP, SUM(QTY_NRGP) as QTY_NRGP, SUM(VAL_NRGP) as VAL_NRGP, SUM(BalQty)+SUM(RcptQty)-SUM(IssueQty) as ClosingQty, SUM(BalValue)+SUM(RcptValue)-SUM(IssueValue) as ClosingBal, Case When SUM(BalQty)+SUM(RcptQty)-SUM(IssueQty)=0 Then 0 else CONVERT(Decimal(18,2),((SUM(BalValue)+SUM(RcptValue)-SUM(IssueValue))/(SUM(BalQty)+SUM(RcptQty)-SUM(IssueQty)))) End as ClosingRate from  (" + qry + ") Summary Group by  Item_Code, Unit_Code ) Final " 'Category_Name, Description, group by Category_Code, Sub_Category_Code, , MAX(Category_Name) as Category_Name, MAX(Description) as Description
            Else
                qry += " Order by Item_Code,RowNo "
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
            gvReport.DataSource = Nothing
            gvReport.Rows.Clear()
            gvReport.Columns.Clear()
            gvReport.DataSource = dt
            FormatGrid()
            Dim arr As New List(Of String)()
            Dim rptType As String = ""
            If rdbtnSummary.IsChecked Then
                rptType = "Summary"
            Else
                rptType = "Detail"
            End If
            arr.Add("StoresLedger Report (" + rptType + ")")
            arr.Add(" From :  " + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "  To : " + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy"))
            If dt.Rows.Count > 0 Then
                clsCommon.MyExportToExcelGrid("Stores Ledger", gvReport, arr, rptType)
            Else
                Throw New Exception("No data found.")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub FormatGrid()
        Try
            For Each col As GridViewColumn In gvReport.Columns
                col.IsVisible = False
                col.Width = 100
            Next
            'gvReport.Columns("Category_Name").IsVisible = True
            'gvReport.Columns("Category_Name").HeaderText = "Category"
            'gvReport.Columns("Description").IsVisible = True
            'gvReport.Columns("Description").HeaderText = "Sub Category"
            gvReport.Columns("Item_Code").IsVisible = True
            gvReport.Columns("Item_Code").HeaderText = "Item Code"
            gvReport.Columns("Item_Desc").IsVisible = True
            gvReport.Columns("Item_Desc").HeaderText = "Description"
            gvReport.Columns("Unit_Code").IsVisible = True
            gvReport.Columns("Unit_Code").HeaderText = "UOM"
            gvReport.Columns("BalQty").IsVisible = True
            gvReport.Columns("BalQty").HeaderText = "Opening Qty"
            
            If rdbtnSummary.IsChecked Then
                gvReport.Columns("ClosingQty").IsVisible = True
                gvReport.Columns("ClosingQty").HeaderText = "Closing Qty"
               
                If chkWithoutVal.Checked Then
                    gvReport.Columns("QTY_P").IsVisible = True
                    If rbtnPurchase.IsChecked Then
                        gvReport.Columns("QTY_P").HeaderText = "Purchase Qty"
                    End If
                    gvReport.Columns("ISS_Q").IsVisible = True
                    gvReport.Columns("QTY_IR").IsVisible = True
                    gvReport.Columns("QTY_PR").IsVisible = True
                    gvReport.Columns("QTY_MS").IsVisible = True
                    gvReport.Columns("QTY_RGP").IsVisible = True
                    gvReport.Columns("QTY_NRGP").IsVisible = True
                    gvReport.Columns("AdjIn_Qty").IsVisible = True
                    gvReport.Columns("AdjOut_Qty").IsVisible = True
                Else 'If chkDetailed.Checked Then
                    gvReport.Columns("BalValue").IsVisible = True
                    gvReport.Columns("BalValue").HeaderText = "Opening Val"
                    gvReport.Columns("AdjIn_Qty").IsVisible = True
                    gvReport.Columns("AdjIn_Rate").IsVisible = True
                    gvReport.Columns("AdjIn_Value").IsVisible = True
                    gvReport.Columns("AdjOut_Qty").IsVisible = True
                    gvReport.Columns("AdjOut_Rate").IsVisible = True
                    gvReport.Columns("AdjOut_Value").IsVisible = True
                    gvReport.Columns("QTY_P").IsVisible = True
                    gvReport.Columns("SRN_Rate").IsVisible = True
                    gvReport.Columns("VAL_P").IsVisible = True
                    If rbtnPurchase.IsChecked Then
                        gvReport.Columns("QTY_P").HeaderText = "Purchase Qty"
                        gvReport.Columns("SRN_Rate").HeaderText = "Purchase Rate"
                        gvReport.Columns("VAL_P").HeaderText = "Purchase Value"
                    End If
                    gvReport.Columns("ISS_Q").IsVisible = True
                    gvReport.Columns("VAL_I").IsVisible = True
                    gvReport.Columns("QTY_IR").IsVisible = True
                    gvReport.Columns("VAL_IR").IsVisible = True
                    gvReport.Columns("QTY_PR").IsVisible = True
                    gvReport.Columns("VAL_PR").IsVisible = True
                    gvReport.Columns("QTY_MS").IsVisible = True
                    gvReport.Columns("VAL_MS").IsVisible = True
                    gvReport.Columns("QTY_RGP").IsVisible = True
                    gvReport.Columns("VAL_RGP").IsVisible = True
                    gvReport.Columns("QTY_NRGP").IsVisible = True
                    gvReport.Columns("VAL_NRGP").IsVisible = True
                    gvReport.Columns("ClosingBal").IsVisible = True
                    gvReport.Columns("ClosingBal").HeaderText = "Closing Val"
                    'Else
                    '    gvReport.Columns("BalRate").IsVisible = True
                    '    gvReport.Columns("BalRate").HeaderText = "Opening Rate"
                    '    gvReport.Columns("BalValue").IsVisible = True
                    '    gvReport.Columns("BalValue").HeaderText = "Opening Val"
                    '    gvReport.Columns("RcptQty").IsVisible = True
                    '    gvReport.Columns("RcptQty").HeaderText = "Receipt Qty"
                    '    gvReport.Columns("RcptRate").IsVisible = True
                    '    gvReport.Columns("RcptRate").HeaderText = "Receipt Rate"
                    '    gvReport.Columns("RcptValue").IsVisible = True
                    '    gvReport.Columns("RcptValue").HeaderText = "Receipt Val"
                    '    gvReport.Columns("IssueQty").IsVisible = True
                    '    gvReport.Columns("IssueQty").HeaderText = "Issue Qty"
                    '    gvReport.Columns("IssueRate").IsVisible = True
                    '    gvReport.Columns("IssueRate").HeaderText = "Issue Rate"
                    '    gvReport.Columns("IssueValue").IsVisible = True
                    '    gvReport.Columns("IssueValue").HeaderText = "Issue Val"
                    '    gvReport.Columns("ClosingBal").IsVisible = True
                    '    gvReport.Columns("ClosingBal").HeaderText = "Closing Val"
                End If

                Try
                    gvReport.Columns("Category").IsVisible = True
                    gvReport.Columns("Category").HeaderText = "Item Category"
                Catch ex As Exception
                End Try
            Else
                gvReport.Columns("DocNo").IsVisible = True
                gvReport.Columns("DocNo").HeaderText = "Document"
                gvReport.Columns("Source_Doc_Date").IsVisible = True
                gvReport.Columns("Source_Doc_Date").HeaderText = "Date"
                gvReport.Columns("Location_Desc").IsVisible = True
                gvReport.Columns("Location_Desc").HeaderText = "Location"
                gvReport.Columns("RcptQty").IsVisible = False
                gvReport.Columns("RcptQty").HeaderText = "Receipt Qty"
                gvReport.Columns("IssueQty").IsVisible = False
                gvReport.Columns("IssueQty").HeaderText = "Issue Qty"
                gvReport.Columns("RIType").IsVisible = True
                gvReport.Columns("RIType").HeaderText = "Type"
                gvReport.Columns("PR_Remarks").IsVisible = True
                gvReport.Columns("PR_Remarks").HeaderText = "PR Remarks"
                If chkWithoutVal.Checked Then
                    gvReport.Columns("AdjIn_Qty").IsVisible = True
                    gvReport.Columns("AdjOut_Qty").IsVisible = True
                    gvReport.Columns("QTY_P").IsVisible = True
                    If rbtnPurchase.IsChecked Then
                        gvReport.Columns("QTY_P").HeaderText = "Purchase Qty"
                    End If
                    gvReport.Columns("ISS_Q").IsVisible = True
                    gvReport.Columns("QTY_IR").IsVisible = True
                    gvReport.Columns("QTY_PR").IsVisible = True
                    gvReport.Columns("QTY_MS").IsVisible = True
                    gvReport.Columns("QTY_RGP").IsVisible = True
                    gvReport.Columns("QTY_NRGP").IsVisible = True
                Else 'If chkDetailed.Checked Then
                    gvReport.Columns("BalValue").IsVisible = True
                    gvReport.Columns("BalValue").HeaderText = "Balance Val"
                    gvReport.Columns("QTY_P").IsVisible = True
                    gvReport.Columns("SRN_Rate").IsVisible = True
                    gvReport.Columns("VAL_P").IsVisible = True
                    gvReport.Columns("AdjIn_Qty").IsVisible = True
                    gvReport.Columns("AdjIn_Rate").IsVisible = True
                    gvReport.Columns("AdjIn_Value").IsVisible = True
                    gvReport.Columns("AdjOut_Qty").IsVisible = True
                    gvReport.Columns("AdjOut_Rate").IsVisible = True
                    gvReport.Columns("AdjOut_Value").IsVisible = True
                    If rbtnPurchase.IsChecked Then
                        gvReport.Columns("QTY_P").HeaderText = "Purchase Qty"
                        gvReport.Columns("SRN_Rate").HeaderText = "Purchase Rate"
                        gvReport.Columns("VAL_P").HeaderText = "Purchase Value"
                    End If
                    gvReport.Columns("ISS_Q").IsVisible = True
                    gvReport.Columns("VAL_I").IsVisible = True
                    gvReport.Columns("QTY_IR").IsVisible = True
                    gvReport.Columns("VAL_IR").IsVisible = True
                    gvReport.Columns("QTY_PR").IsVisible = True
                    gvReport.Columns("VAL_PR").IsVisible = True
                    gvReport.Columns("QTY_MS").IsVisible = True
                    gvReport.Columns("VAL_MS").IsVisible = True
                    gvReport.Columns("QTY_RGP").IsVisible = True
                    gvReport.Columns("VAL_RGP").IsVisible = True
                    gvReport.Columns("QTY_NRGP").IsVisible = True
                    gvReport.Columns("VAL_NRGP").IsVisible = True
                    'Else
                    '    gvReport.Columns("BalRate").IsVisible = True
                    '    gvReport.Columns("BalRate").HeaderText = "Opening Rate"
                    '    gvReport.Columns("BalValue").IsVisible = True
                    '    gvReport.Columns("BalValue").HeaderText = "Opening Val"
                    '    gvReport.Columns("RcptQty").IsVisible = True
                    '    gvReport.Columns("RcptQty").HeaderText = "Receipt Qty"
                    '    gvReport.Columns("RcptRate").IsVisible = True
                    '    gvReport.Columns("RcptRate").HeaderText = "Receipt Rate"
                    '    gvReport.Columns("RcptValue").IsVisible = True
                    '    gvReport.Columns("RcptValue").HeaderText = "Receipt Val"
                    '    gvReport.Columns("IssueQty").IsVisible = True
                    '    gvReport.Columns("IssueQty").HeaderText = "Issue Qty"
                    '    gvReport.Columns("IssueRate").IsVisible = True
                    '    gvReport.Columns("IssueRate").HeaderText = "Issue Rate"
                    '    gvReport.Columns("IssueValue").IsVisible = True
                    '    gvReport.Columns("IssueValue").HeaderText = "Issue Val"
                End If

                Try
                    gvReport.Columns("Category").IsVisible = True
                    gvReport.Columns("Category").HeaderText = "Item Category"
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)

        End Try
    End Sub

    Private Sub FrmStoresLedger_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = False
    End Sub

    Private Sub chkItemSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemSelect.ToggleStateChanged
        cbgItem.Enabled = True
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged, rbtnCategorySelect.ToggleStateChanged
        tvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Sub gvReport_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvReport.CellDoubleClick
        If gvReport.Rows.Count > 0 Then
            Dim strDoc
            strDoc = gvReport.CurrentRow.Cells("DocNo").Value
            Dim RITYPE As String = gvReport.CurrentRow.Cells("RITYPE").Value

            If RITYPE = "IC-AD" Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strDoc)
            ElseIf RITYPE = "ISSTRAN" Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnIssueReturn, strDoc)
            ElseIf RITYPE = "NRGP" Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strDoc)
            ElseIf RITYPE = "RGP" Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strDoc)
            ElseIf RITYPE = "SRN" Then
                If rbtnPurchase.IsChecked Then
                    strDoc = clsDBFuncationality.getSingleValue("select top(1) Against_SRN from TSPL_PI_HEAD where PI_No='" & strDoc & "'")
                End If
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, strDoc)

            ElseIf RITYPE = "ScrapIn" Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, strDoc)
            End If
        End If
    End Sub
End Class

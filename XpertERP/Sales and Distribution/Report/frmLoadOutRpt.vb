'****** Created By: Manoj Chauhan **********
'****** Start Date: 26/05/2011 3:00 PM    **********
'****** Table: TSPL_SHIPMENT_MASTER & TSPL_SHIPMENT_DETAILS   ******** 

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Collections
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports common

Public Class FrmLoadOutRpt
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Dim userCode, companyCode As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Dim sql As String
#Region "Page Load"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.crptLoadOut)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmLoadOutRpt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            fndLoadoutFrm.txtValue.Text = ""
            chkPre.Checked = False
        End If
    End Sub
    Private Sub FrmLoadOutRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")
        AddHandler fndLoadoutFrm.txtValue.TextChanged, AddressOf fndLoadOutFrom_TextChanged
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
#End Region

    Private Sub fndLoadOutFrom_TextChanged()
        sql = "Select Location FROM TSPL_SALE_INVOICE_HEAD WHERE Sale_Invoice_No='" + fndLoadoutFrm.txtValue.Text + "'"
        Dim location As String = connectSql.RunScalar(sql)
        sql = "Select Excisable from TSPL_LOCATION_MASTER WHERE Location_Code='" + location + "' "
        Dim LType As String = connectSql.RunScalar(sql)
        If LType = "F" Or LType = "N" Then
            chkPre.Checked = False
        End If
    End Sub
#Region "Finder"
    Private Sub Finder1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndLoadoutFrm.Load
        'fndLoadoutFrm.Query = "select Sale_Invoice_No as [Invoice No] ,Shipment_No as 'LoadOut No' ,Cust_Name as [Customer Name],Sale_Invoice_Date [Invoice Date]  from TSPL_SALE_INVOICE_HEAD "
        fndLoadoutFrm.Query = "select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as [Invoice No] ,TSPL_SALE_INVOICE_HEAD.Shipment_No as 'LoadOut No' ,TSPL_SALE_INVOICE_HEAD.Cust_Name as [Customer Name],Sale_Invoice_Date [Invoice Date],TSPL_LOCATION_MASTER.Location_Desc as [Location]  from TSPL_SALE_INVOICE_HEAD  inner join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code "
        fndLoadoutFrm.ConnectionString = connectSql.SqlCon()
        fndLoadoutFrm.Caption = "Sale Invoice Report"
        fndLoadoutFrm.ValueToSelect = "Invoice No"
        fndLoadoutFrm.ValueToSelect1 = "Invoice No"
    End Sub
#End Region

#Region "Button Click"

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print()
    End Sub
    Sub print()
        If fndLoadoutFrm.txtValue.Text = "" Then
            myMessages.blankValue("Invoice No")
            Exit Sub
        End If
        sql = "Select Location FROM TSPL_SALE_INVOICE_HEAD WHERE Sale_Invoice_No='" + fndLoadoutFrm.txtValue.Text + "'"
        Dim location As String = connectSql.RunScalar(sql)
        sql = "Select Excisable from TSPL_LOCATION_MASTER WHERE Location_Code='" + location + "' "
        Dim LType As String = connectSql.RunScalar(sql)
        funPrintReport(fndLoadoutFrm.txtValue.Text, LType, chkPre.Checked, "")

    End Sub


    Public Shared Sub funPrintReport(ByVal strInvoiceNo As String, ByVal locationType As String, ByVal prePrinted As Boolean, ByVal strShipmentNo As String)
        Try
            If clsCommon.myLen(strInvoiceNo) <= 0 Then
                Throw New Exception("Sale Invoice No not found to print")
            End If

            Dim whereclause As String
            whereclause = " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = '" + strInvoiceNo + "' "
            Dim strWithoutShall As String = ",'' as totalqty,"
            Dim str As String = ",'' as totalqty,"
            Dim qry As String = ""
            Dim dt As DataTable
            If clsCommon.CompairString(locationType, "T") = CompairStringResult.Equal Then
                qry = "select SUM(Qty) as Qty,MAX(Unit_Code) as Unit from("
                qry += " select invoice_qty/(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code ) as Qty, 'FC' as Unit_Code  from TSPL_SALE_INVOICE_DETAIL where Sale_Invoice_No = '" + strInvoiceNo + "' "
                qry += " ) xxx"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    str = ",'" + clsCommon.myCstr(dt.Rows(0)("Unit")) + " - " + clsCommon.myFormat(dt.Rows(0)("Qty")) + " ' as totalqty,"
                End If
            Else
                qry = "select  Unit_code as Unit,sum(invoice_qty) as Qty,(select shell_qty from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No='" + strInvoiceNo + "')as SH from TSPL_SALE_INVOICE_DETAIL where Sale_Invoice_No = '" + strInvoiceNo + "' group by Unit_code"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    str = ",'"
                    For Each dr As DataRow In dt.Rows
                        str = str + clsCommon.myCstr(dr("Unit")) + " - " + clsCommon.myCstr(dr("Qty")) + " "
                    Next
                    strWithoutShall = str + "'" + " as totalqty,"
                    str = str + " SH - " + clsCommon.myCstr(dt.Rows(0)("SH"))
                    str = str + "'" + " as totalqty,"
                End If
            End If
            Dim qryForGettingTax As String = "select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code"
            Dim strIsFOCItem As String = "(CASE WHEN TSPL_SALE_INVOICE_DETAIL.Scheme_Item='Y' or TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item='Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y' or TSPL_SALE_INVOICE_HEAD.Inv_Disc_Percent=100 THEN 'Y' ELSE 'N' END) as FOCItem "
            Dim strInvoiceType As String = "(case when TSPL_SALE_INVOICE_HEAD.Invoice_Type='Retail' then 'Retail Invoice' else  case when TSPL_SALE_INVOICE_HEAD.Invoice_Type='Tax' then 'Tax Invoice' else case when TSPL_SALE_INVOICE_HEAD.Invoice_Type='Excise' then 'Excise Challan' else ''  end end end) as Invoice_Type"
            Dim strQuery As String = ""
            Dim qryForShipToAdd As String = "(case when len(isnull(TSPL_SALE_INVOICE_HEAD.Ship_To,''))>0 then "
            qryForShipToAdd += " isnull(TSPL_SHIP_TO_LOCATION.Add1,'')+ (case when LEN(ISNULL(TSPL_SHIP_TO_LOCATION.Add2,''))>0 then ','else '' end )"
            qryForShipToAdd += " +isnull(TSPL_SHIP_TO_LOCATION.Add2,'')+(case when LEN(ISNULL(TSPL_SHIP_TO_LOCATION.Add3,''))>0 then ','else '' end )"
            qryForShipToAdd += " +isnull(TSPL_SHIP_TO_LOCATION.Add3,'')+(case when LEN(ISNULL(TSPL_SHIP_TO_LOCATION.Add4,''))>0 then ','else '' end )"
            qryForShipToAdd += " +isnull(TSPL_SHIP_TO_LOCATION.Add4,'')+(case when LEN(ISNULL(TSPL_SHIP_TO_LOCATION.City_Code,''))>0 then ','else '' end )"
            qryForShipToAdd += " +isnull(TSPL_SHIP_TO_LOCATION.City_Code,'')+(case when LEN(ISNULL(TSPL_SHIP_TO_LOCATION.State,''))>0 then ','else '' end )"
            qryForShipToAdd += " + isnull(TSPL_SHIP_TO_LOCATION.State,'') "
            qryForShipToAdd += " else TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 end)"

            If clsCommon.CompairString(locationType, "T") = CompairStringResult.Equal Then
                If prePrinted = True Then
                    strQuery = " SELECT   (select Transporter_name from TSPL_Transport_MASTER left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Transport_Id =TSPL_Transport_MASTER.Transport_Id where TSPL_VEHICLE_MASTER.Vehicle_Id = (select Vehicle_Code from TSPL_SALE_INVOICE_HEAD where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No='" + strInvoiceNo + "' )  ) as TPTName,   TSPL_SALE_INVOICE_HEAD.Cust_PONo,TSPL_SALE_INVOICE_HEAD.Mode_Of_Transport ,TSPL_SALE_INVOICE_HEAD.Vehicle_No ,TSPL_SALE_INVOICE_HEAD.KM_Reading,TSPL_CHAPTER_HEAD.Description as ChapterName," + qryForShipToAdd + " AS Address, " & _
                       "  TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,  convert(varchar(10),TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as Sale_Invoice_Date , TSPL_SALE_INVOICE_HEAD.Cust_Name, " & _
                       " isnull((" + qryForGettingTax + "= TSPL_SALE_INVOICE_HEAD.TAX1),'') as TAX1, TSPL_SALE_INVOICE_HEAD.TAX1_Rate, TSPL_SALE_INVOICE_HEAD.TAX1_Assessable_Amt, " & _
                       " TSPL_SALE_INVOICE_HEAD.TAX1_Amt, isnull((" + qryForGettingTax + "=TSPL_SALE_INVOICE_HEAD.TAX2),'') as TAX2, TSPL_SALE_INVOICE_HEAD.TAX2_Rate, " & _
                       " TSPL_SALE_INVOICE_HEAD.TAX2_Assessable_Amt,case when  TSPL_SALE_INVOICE_HEAD.Inv_Disc_Percent=100 then 0 else  TSPL_SALE_INVOICE_HEAD.TAX2_Amt end as TAX2_Amt,isnull((" + qryForGettingTax + "= TSPL_SALE_INVOICE_HEAD.TAX3),'') as TAX3, " & _
                       " TSPL_SALE_INVOICE_HEAD.TAX3_Rate, TSPL_SALE_INVOICE_HEAD.TAX3_Assessable_Amt,case when  TSPL_SALE_INVOICE_HEAD.Inv_Disc_Percent=100 then 0 else  TSPL_SALE_INVOICE_HEAD.TAX3_Amt end as TAX3_Amt, " & _
                       " isnull((" + qryForGettingTax + "=TSPL_SALE_INVOICE_HEAD.TAX4),'') as TAX4, TSPL_SALE_INVOICE_HEAD.TAX4_Rate, TSPL_SALE_INVOICE_HEAD.TAX4_Assessable_Amt, " & _
                       " case when  TSPL_SALE_INVOICE_HEAD.Inv_Disc_Percent=100 then 0 else TSPL_SALE_INVOICE_HEAD.TAX4_Amt end as TAX4_Amt, isnull((" + qryForGettingTax + "=TSPL_SALE_INVOICE_HEAD.TAX5),'') as TAX5, TSPL_SALE_INVOICE_HEAD.TAX5_Rate, " & _
                       " TSPL_SALE_INVOICE_HEAD.TAX5_Assessable_Amt,case when  TSPL_SALE_INVOICE_HEAD.Inv_Disc_Percent=100 then 0 else  TSPL_SALE_INVOICE_HEAD.TAX5_Amt end as TAX5_Amt,TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt , TSPL_SALE_INVOICE_DETAIL.Invoice_Qty, " & _
                       " TSPL_SALE_INVOICE_DETAIL.Item_Code, (TSPL_SALE_INVOICE_DETAIL.Item_Desc + Case WHEN (Scheme_Item='Y' AND ISNULL(Discount_Code,'')='') Then ' (T)' Else case When (Scheme_Item='Y' AND ISNULL(Discount_Code,'')<> '') THEN ' (S)' Else '' END END) as Item_Desc, TSPL_SALE_INVOICE_DETAIL.MRP_Amt, " & _
                       " case when Inv_Disc_Percent=100 then 0 else TSPL_SALE_INVOICE_DETAIL.Basic_Rate end  as Basic_Rate, TSPL_SALE_INVOICE_DETAIL.Item_Assessable_Rate, TSPL_SALE_INVOICE_DETAIL.Item_Net_Amt, " & _
                       " TSPL_SALE_INVOICE_DETAIL.TAX1 AS 'DTax1', TSPL_SALE_INVOICE_DETAIL.TAX1_Rate AS 'DTax1Rate', " & _
                       " TSPL_SALE_INVOICE_DETAIL.TAX1_Assessable_Amt AS 'DTax1Ass',TSPL_SALE_INVOICE_DETAIL.TAX1_Amt AS Dtax1Amt, Case When (Scheme_Item='Y' or TSPL_SALE_INVOICE_HEAD.Total_Disc_Percent=100 ) Then ((TSPL_SALE_INVOICE_DETAIL.TAX2_Amt*Invoice_Qty) + (TSPL_SALE_INVOICE_DETAIL.TAX3_Amt*Invoice_Qty)) Else 0 End As ExciseAmtFOC, ((TSPL_SALE_INVOICE_DETAIL.TAX2_Amt*Invoice_Qty) + (TSPL_SALE_INVOICE_DETAIL.TAX3_Amt*Invoice_Qty)) as ExciseAmtTotal, " & _
                       " TSPL_SALE_INVOICE_DETAIL.Total_Assessable_Amt, TSPL_SALE_INVOICE_DETAIL.Total_MRP_Amt, (Basic_Rate*Invoice_Qty ) as  [Total_Basic_Amt],"
                    'Change suggest by ashok dated 14/01/2014
                    'strQuery += " Case When  (TSPL_SALE_INVOICE_DETAIL.Scheme_Item='Y' or TSPL_SALE_INVOICE_HEAD.Total_Disc_Percent=100)   Then (Basic_Rate*Invoice_Qty) + (Total_Assessable_Amt*TSPL_SALE_INVOICE_DETAIL.TAX1_Rate/100) ELSE 0 End as [FOCAmt] , " & _

                    strQuery += " case when TSPL_SALE_INVOICE_HEAD.Inv_Disc_Percent=100 then (Total_Assessable_Amt*TSPL_SALE_INVOICE_DETAIL.TAX1_Rate/100)+( case when TSPL_SALE_INVOICE_DETAIL.Scheme_Item='Y' Then (Basic_Rate*Invoice_Qty) else 0 end)  else Case When  TSPL_SALE_INVOICE_DETAIL.Scheme_Item='Y'   Then (Basic_Rate*Invoice_Qty) + (Total_Assessable_Amt*TSPL_SALE_INVOICE_DETAIL.TAX1_Rate/100) ELSE 0 End end  as [FOCAmt] , " & _
                     " TSPL_SALE_INVOICE_DETAIL.Total_net_Amt, TSPL_SALE_INVOICE_DETAIL.Total_Tax_Amt, TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt, " & _
                      " TSPL_SALE_INVOICE_HEAD.Empty_Value,TSPL_SALE_INVOICE_DETAIL.Total_TPT,TSPL_SALE_INVOICE_HEAD.TPT as 'ttlTPT', " & _
                      " TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2" & _
                      "" + str + "TSPL_CUSTOMER_MASTER.Tin_No,TSPL_SALE_INVOICE_HEAD.Remarks  ,TSPL_SALE_INVOICE_HEAD.Description," + strInvoiceType + ",ISNULL((select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code =(case when TSPL_SALE_INVOICE_DETAIL.Unit_code='FC' then 'FB' else  case when TSPL_SALE_INVOICE_DETAIL.Unit_code='FB' then 'FC' else '' end end)),1) as [Conversion],(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code ) as OrgConversionFactor,TSPL_EMPLOYEE_MASTER.Emp_Name as SalesmanName , CONVERT(varchar(100),TSPL_SALE_INVOICE_HEAD.Date_Time_Removal ,108) as RemovelTime," + strIsFOCItem + "" & _
                      ",TSPL_SALE_INVOICE_HEAD.Inv_Discount_Amt, TSPL_SALE_INVOICE_HEAD.TAX1 as Tax1Code,TSPL_SALE_INVOICE_HEAD.TAX2 as Tax2Code,TSPL_SALE_INVOICE_HEAD.TAX3 as Tax3Code,TSPL_SALE_INVOICE_HEAD.TAX4 as Tax4Code,TSPL_SALE_INVOICE_HEAD.TAX5 as Tax5Code , ( case when Against_C_Form='0' then '' else 'Against C Form' end) as Is_AgainstFormF FROM  TSPL_CUSTOMER_MASTER INNER JOIN " & _
                      " TSPL_SALE_INVOICE_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_INVOICE_HEAD.Cust_Code INNER JOIN " & _
                      " TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer join TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code =TSPL_ITEM_MASTER .Item_Code left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code=TSPL_ITEM_MASTER.Cheapter_Heads " & _
                      " left outer join TSPL_COMPANY_MASTER on TSPL_SALE_INVOICE_HEAD.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_SALE_INVOICE_HEAD.Salesman_Code left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code and TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SALE_INVOICE_HEAD.Ship_To"
                    strQuery = strQuery & whereclause

                    strQuery += " order by TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id"
                    Dim dtBasic As DataTable = clsDBFuncationality.GetDataTable(strQuery)
                    SetItemWiseTax(prePrinted, dtBasic, strInvoiceNo)
                    frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dtBasic, EnumTecxpertPaperSize.PaperSize10x12, "rptSaleInvoiceExcisePrePrint", "Sales Report", True)
                Else
                    strQuery = " SELECT   TSPL_SALE_INVOICE_HEAD.Created_By,TSPL_SALE_INVOICE_HEAD.Modify_By,  TSPL_SALE_INVOICE_HEAD.Cust_PONo,TSPL_CHAPTER_HEAD.Description as ChapterName," + qryForShipToAdd + " AS Address, " & _
                            "  TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,  convert(varchar(10),TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as Sale_Invoice_Date, COnvert(Varchar,TSPL_SALE_INVOICE_HEAD.Date_Time_Removal,108) as RemovelTime , TSPL_SALE_INVOICE_HEAD.Cust_Name, " & _
                            "  (" + qryForGettingTax + "=TSPL_SALE_INVOICE_HEAD.TAX1) as TAX1, TSPL_SALE_INVOICE_HEAD.TAX1_Rate, TSPL_SALE_INVOICE_HEAD.TAX1_Assessable_Amt, " & _
                             " TSPL_SALE_INVOICE_HEAD.TAX1_Amt,(" + qryForGettingTax + " = TSPL_SALE_INVOICE_HEAD.TAX2) as TAX2 , TSPL_SALE_INVOICE_HEAD.TAX2_Rate, " & _
                             " TSPL_SALE_INVOICE_HEAD.TAX2_Assessable_Amt,case when  TSPL_SALE_INVOICE_HEAD.Inv_Disc_Percent=100 then 0 else  TSPL_SALE_INVOICE_HEAD.TAX2_Amt end as TAX2_Amt, (" + qryForGettingTax + " =TSPL_SALE_INVOICE_HEAD.TAX3) as TAX3, " & _
                             " TSPL_SALE_INVOICE_HEAD.TAX3_Rate, TSPL_SALE_INVOICE_HEAD.TAX3_Assessable_Amt,case when  TSPL_SALE_INVOICE_HEAD.Inv_Disc_Percent=100 then 0 else  TSPL_SALE_INVOICE_HEAD.TAX3_Amt end as TAX3_Amt, " & _
                             " (" + qryForGettingTax + " =TSPL_SALE_INVOICE_HEAD.TAX4) as TAX4, TSPL_SALE_INVOICE_HEAD.TAX4_Rate, TSPL_SALE_INVOICE_HEAD.TAX4_Assessable_Amt, " & _
                             " case when  TSPL_SALE_INVOICE_HEAD.Inv_Disc_Percent=100 then 0 else  TSPL_SALE_INVOICE_HEAD.TAX4_Amt end as TAX4_Amt ,isnull((" + qryForGettingTax + " = TSPL_SALE_INVOICE_HEAD.TAX5),'') as TAX5, TSPL_SALE_INVOICE_HEAD.TAX5_Rate, " & _
                             " TSPL_SALE_INVOICE_HEAD.TAX5_Assessable_Amt,case when  TSPL_SALE_INVOICE_HEAD.Inv_Disc_Percent=100 then 0 else  TSPL_SALE_INVOICE_HEAD.TAX5_Amt end as TAX5_Amt ,TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt , TSPL_SALE_INVOICE_DETAIL.Invoice_Qty, " & _
                             " TSPL_SALE_INVOICE_DETAIL.Item_Code, (TSPL_SALE_INVOICE_DETAIL.Item_Desc + Case WHEN (Scheme_Item='Y' AND ISNULL(Discount_Code,'')='') Then ' (T)' Else case When (Scheme_Item='Y' AND ISNULL(Discount_Code,'')<> '') THEN ' (S)' Else '' END END) as Item_Desc, TSPL_SALE_INVOICE_DETAIL.MRP_Amt, " & _
                             " case when Inv_Disc_Percent=100 then 0 else TSPL_SALE_INVOICE_DETAIL.Basic_Rate end as Basic_Rate , TSPL_SALE_INVOICE_DETAIL.Item_Assessable_Rate, TSPL_SALE_INVOICE_DETAIL.Item_Net_Amt, " & _
                             " TSPL_SALE_INVOICE_DETAIL.TAX1 AS 'DTax1', TSPL_SALE_INVOICE_DETAIL.TAX1_Rate AS 'DTax1Rate', " & _
                             " TSPL_SALE_INVOICE_DETAIL.TAX1_Assessable_Amt AS 'DTax1Ass'," & _
                             " TSPL_SALE_INVOICE_DETAIL.TAX1_Amt AS Dtax1Amt, Case When (Scheme_Item='Y' or TSPL_SALE_INVOICE_HEAD.Total_Disc_Percent=100 ) Then ((TSPL_SALE_INVOICE_DETAIL.TAX2_Amt*Invoice_Qty) + (TSPL_SALE_INVOICE_DETAIL.TAX3_Amt*Invoice_Qty)) Else 0 End As ExciseAmtFOC, ((TSPL_SALE_INVOICE_DETAIL.TAX2_Amt*Invoice_Qty) + (TSPL_SALE_INVOICE_DETAIL.TAX3_Amt*Invoice_Qty)) as ExciseAmtTotal, " & _
                             " TSPL_SALE_INVOICE_DETAIL.Total_Assessable_Amt, TSPL_SALE_INVOICE_DETAIL.Total_MRP_Amt, (Basic_Rate*Invoice_Qty ) as  [Total_Basic_Amt],"
                    'Change suggest by ashok dated 14/01/2014
                    'strQuery += "   Case When  (TSPL_SALE_INVOICE_DETAIL.Scheme_Item='Y' or TSPL_SALE_INVOICE_HEAD.Total_Disc_Percent=100) Then (Basic_Rate*Invoice_Qty) + (Total_Assessable_Amt*TSPL_SALE_INVOICE_DETAIL.TAX1_Rate/100) ELSE 0 End as [FOCAmt] , " & _

                    strQuery += " case when TSPL_SALE_INVOICE_HEAD.Inv_Disc_Percent=100  then (Total_Assessable_Amt*TSPL_SALE_INVOICE_DETAIL.TAX1_Rate/100)+( case when TSPL_SALE_INVOICE_DETAIL.Scheme_Item='Y' Then (Basic_Rate*Invoice_Qty) else 0 end) else Case When  TSPL_SALE_INVOICE_DETAIL.Scheme_Item='Y'   Then (Basic_Rate*Invoice_Qty) + (Total_Assessable_Amt*TSPL_SALE_INVOICE_DETAIL.TAX1_Rate/100) ELSE 0 End end  as [FOCAmt] , " & _
                             " TSPL_SALE_INVOICE_DETAIL.Total_net_Amt, TSPL_SALE_INVOICE_DETAIL.Total_Tax_Amt, TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt, " & _
                             " TSPL_SALE_INVOICE_HEAD.Empty_Value,TSPL_SALE_INVOICE_DETAIL.Total_TPT,TSPL_SALE_INVOICE_HEAD.TPT as 'ttlTPT', " & _
                             " TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Tin_No as CompTinNo, TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2, (Case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else TSPL_COMPANY_MASTER.Add1 + case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else ', '+ TSPL_COMPANY_MASTER.Add2 + Case When ISNULL(TSPL_COMPANY_MASTER.Add3,'')='' Then '' Else TSPL_COMPANY_MASTER.Add3 End End End) AS CompAddress  " & _
                             " " + str + "TSPL_CUSTOMER_MASTER.Tin_No,TSPL_SALE_INVOICE_HEAD.Remarks  ,TSPL_SALE_INVOICE_HEAD.Description," + strInvoiceType + ", ISNULL((select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code = (case when TSPL_SALE_INVOICE_DETAIL.Unit_code='FC' then 'FB' else  case when TSPL_SALE_INVOICE_DETAIL.Unit_code='FB' then 'FC' else '' end end)),1) as [Conversion] ,(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code ) as OrgConversionFactor,TSPL_EMPLOYEE_MASTER.Emp_Name as SalesmanName," + strIsFOCItem + "" & _
                             ", TSPL_SALE_INVOICE_HEAD.Inv_Discount_Amt, TSPL_SALE_INVOICE_HEAD.TAX1 as Tax1Code,TSPL_SALE_INVOICE_HEAD.TAX2 as Tax2Code,TSPL_SALE_INVOICE_HEAD.TAX3 as Tax3Code,TSPL_SALE_INVOICE_HEAD.TAX4 as Tax4Code,TSPL_SALE_INVOICE_HEAD.TAX5 as Tax5Code ,(select [USER_NAME] from TSPL_USER_MASTER where TSPL_USER_MASTER.User_Code=TSPL_SALE_INVOICE_HEAD.Created_By) as CreateByName" & _
                             ",(case when TSPL_SALE_INVOICE_HEAD.Is_Post='Y' then (select [USER_NAME] from TSPL_USER_MASTER where User_Code=TSPL_SALE_INVOICE_HEAD.Modify_By) else '' end) as PostByName,TSPL_SALE_INVOICE_HEAD.Salesman_Code, TSPL_EMPLOYEE_MASTER.Emp_Name as SalesmanName, TSPL_SALE_INVOICE_HEAD.Vehicle_No, TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Route_Desc, (select ISNULL( Transfer_No,'')  from TSPL_SHIPMENT_MASTER where Shipment_No=TSPL_SALE_INVOICE_HEAD.Shipment_No) as TransferNo," & _
                             "(select ISNULL( Customer_Invoice_No,'')  from TSPL_SHIPMENT_MASTER where Shipment_No=TSPL_SALE_INVOICE_HEAD.Shipment_No) as CustomerInvNo ,(case when TSPL_SALE_INVOICE_HEAD.is_Printed =1 then (select [USER_NAME] from TSPL_USER_MASTER where User_Code=TSPL_SALE_INVOICE_HEAD.Verify_By)else '' end ) as VerifyByName  FROM  TSPL_CUSTOMER_MASTER INNER JOIN " & _
                             " TSPL_SALE_INVOICE_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_INVOICE_HEAD.Cust_Code INNER JOIN " & _
                             " TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer join TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code =TSPL_ITEM_MASTER .Item_Code left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code=TSPL_ITEM_MASTER.Cheapter_Heads " & _
                             " left outer join TSPL_COMPANY_MASTER on TSPL_SALE_INVOICE_HEAD.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_SALE_INVOICE_HEAD.Salesman_Code left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code and TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SALE_INVOICE_HEAD.Ship_To "
                    strQuery = strQuery & whereclause
                    strQuery += " order by TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id"
                    Dim dtBasic As DataTable = clsDBFuncationality.GetDataTable(strQuery)
                    SetItemWiseTax(prePrinted, dtBasic, strInvoiceNo)
                    frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dtBasic, EnumTecxpertPaperSize.NA, "rptSaleInvoiceExcise", "Sales Report", True)
                End If
            Else
                If prePrinted = True Then
                    strQuery = "  SELECT   (select Transporter_name from TSPL_Transport_MASTER left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Transport_Id =TSPL_Transport_MASTER.Transport_Id where TSPL_VEHICLE_MASTER.Vehicle_Id = (select Vehicle_Code from TSPL_SALE_INVOICE_HEAD where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No='" + strInvoiceNo + "' )  ) as TPTName, TSPL_SALE_INVOICE_HEAD.Cust_PONo,TSPL_SALE_INVOICE_HEAD.Mode_Of_Transport ,TSPL_SALE_INVOICE_HEAD.Vehicle_No ,TSPL_SALE_INVOICE_HEAD.KM_Reading," + qryForShipToAdd + " AS Address, " & _
                        " TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,  convert(varchar(10),TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as Sale_Invoice_Date, TSPL_SALE_INVOICE_HEAD.Cust_Name, " & _
                        " (" + qryForGettingTax + "=TSPL_SALE_INVOICE_HEAD.TAX1) as TAX1, TSPL_SALE_INVOICE_HEAD.TAX1_Rate, TSPL_SALE_INVOICE_HEAD.TAX1_Assessable_Amt, " & _
                        " TSPL_SALE_INVOICE_HEAD.TAX1_Amt, (" + qryForGettingTax + "=TSPL_SALE_INVOICE_HEAD.TAX2) as TAX2, TSPL_SALE_INVOICE_HEAD.TAX2_Rate, " & _
                        " TSPL_SALE_INVOICE_HEAD.TAX2_Assessable_Amt, TSPL_SALE_INVOICE_HEAD.TAX2_Amt,(" + qryForGettingTax + "= TSPL_SALE_INVOICE_HEAD.TAX3) as TAX3, " & _
                        " TSPL_SALE_INVOICE_HEAD.TAX3_Rate, TSPL_SALE_INVOICE_HEAD.TAX3_Assessable_Amt, TSPL_SALE_INVOICE_HEAD.TAX3_Amt, " & _
                        " (" + qryForGettingTax + "=TSPL_SALE_INVOICE_HEAD.TAX4) as TAX4, TSPL_SALE_INVOICE_HEAD.TAX4_Rate, TSPL_SALE_INVOICE_HEAD.TAX4_Assessable_Amt, " & _
                        " TSPL_SALE_INVOICE_HEAD.TAX4_Amt, isnull((" + qryForGettingTax + "=TSPL_SALE_INVOICE_HEAD.TAX5),'') as TAX5, TSPL_SALE_INVOICE_HEAD.TAX5_Rate, " & _
                        " TSPL_SALE_INVOICE_HEAD.TAX5_Assessable_Amt, TSPL_SALE_INVOICE_HEAD.TAX5_Amt,TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt ,TSPL_SALE_INVOICE_HEAD.Vehicle_No,TSPL_SALE_INVOICE_HEAD.KM_Reading , TSPL_SALE_INVOICE_DETAIL.Invoice_Qty, " & _
                        " TSPL_SALE_INVOICE_DETAIL.Item_Code, TSPL_SALE_INVOICE_DETAIL.Item_Desc   + Case WHEN (Scheme_Item='Y' AND ISNULL(Discount_Code,'')='') Then ' (T)' Else case When (Scheme_Item='Y' AND ISNULL(Discount_Code,'')<> '') THEN ' (S)' Else '' END END as Item_Desc,    TSPL_SALE_INVOICE_DETAIL.MRP_Amt/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code =(case when TSPL_SALE_INVOICE_DETAIL.Unit_code='FC' then 'FB' else  case when TSPL_SALE_INVOICE_DETAIL.Unit_code='FB' then 'FC' else '' end end))  as [MRP_Amt], " & _
                        " TSPL_SALE_INVOICE_DETAIL.Basic_Rate, TSPL_SALE_INVOICE_DETAIL.Item_Assessable_Rate, TSPL_SALE_INVOICE_DETAIL.Item_Net_Amt, " & _
                        "  TSPL_SALE_INVOICE_DETAIL.TAX1 AS 'DTax1', TSPL_SALE_INVOICE_DETAIL.TAX1_Rate AS 'DTax1Rate', " & _
                        " TSPL_SALE_INVOICE_DETAIL.TAX1_Assessable_Amt AS 'DTax1Ass', '0.00' AS 'Dtax1Amt', " & _
                        " TSPL_SALE_INVOICE_DETAIL.Total_Assessable_Amt, TSPL_SALE_INVOICE_DETAIL.Total_MRP_Amt, TSPL_SALE_INVOICE_DETAIL.Total_Basic_Amt, " & _
                        "  TSPL_SALE_INVOICE_DETAIL.Total_net_Amt, TSPL_SALE_INVOICE_DETAIL.Total_Tax_Amt, TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt, " & _
                        " TSPL_SALE_INVOICE_HEAD.Empty_Value, TSPL_SALE_INVOICE_DETAIL.Total_TPT,TSPL_SALE_INVOICE_HEAD.TPT as 'ttlTPT', " & _
                        " TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  " & _
                        " " + str + "TSPL_CUSTOMER_MASTER.Tin_No,TSPL_SALE_INVOICE_HEAD.Remarks  ,TSPL_SALE_INVOICE_HEAD.Description," + strInvoiceType + ",(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code =(case when TSPL_SALE_INVOICE_DETAIL.Unit_code='FC' then 'FB' else  case when TSPL_SALE_INVOICE_DETAIL.Unit_code='FB' then 'FC' else '' end end)) as [Conversion],TSPL_EMPLOYEE_MASTER.Emp_Name as SalesmanName,case when Inv_Disc_Percent=100 then 0 else TSPL_SALE_INVOICE_HEAD.Inv_Discount_Amt end as Inv_Discount_Amt,TSPL_SALE_INVOICE_HEAD.TAX1 as Tax1Code,TSPL_SALE_INVOICE_HEAD.TAX2 as Tax2Code,TSPL_SALE_INVOICE_HEAD.TAX3 as Tax3Code,TSPL_SALE_INVOICE_HEAD.TAX4 as Tax4Code,TSPL_SALE_INVOICE_HEAD.TAX5 as Tax5Code ,case when len(ISNULL( TSPL_SALE_INVOICE_DETAIL.Main_Item,''))>0 then (select Sku_Seq from TSPL_ITEM_MASTER where Item_Code=TSPL_SALE_INVOICE_DETAIL.Main_Item ) else TSPL_ITEM_MASTER.Sku_Seq end as OrderBySKUNo,TSPL_SALE_INVOICE_DETAIL.Unit_code ," + strIsFOCItem + ",Discount_Code,(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code ) as OrgConversionFactor,TSPL_SALE_INVOICE_DETAIL.Unit_Code,TSPL_SALE_INVOICE_HEAD.Created_By FROM  TSPL_CUSTOMER_MASTER INNER JOIN " & _
                        "  TSPL_SALE_INVOICE_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_INVOICE_HEAD.Cust_Code INNER JOIN " & _
                        " TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No   left outer join TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code =TSPL_ITEM_MASTER .Item_Code " & _
                        " left outer join TSPL_COMPANY_MASTER on TSPL_SALE_INVOICE_HEAD.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_SALE_INVOICE_HEAD.Salesman_Code  left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code and TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SALE_INVOICE_HEAD.Ship_To"
                    strQuery = strQuery & whereclause
                    strQuery += " order by OrderBySKUNo,TSPL_SALE_INVOICE_DETAIL.Unit_code desc"
                    Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(strQuery)
                    SetItemWiseTax(prePrinted, dtNew, strInvoiceNo)
                    frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dtNew, EnumTecxpertPaperSize.Guntur10x12, "rptSaleInvoiceNonExcisePrePrint", "Sales Report", True)
                Else
                    Dim strRGBPETQty As String = "select SUM( case when isRGB=1 then FCQty else 0 end ) as RGBQty,SUM( case when isRGB=0 then FCQty else 0 end ) as PETQty from ("
                    strRGBPETQty += " select TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No, TSPL_SALE_INVOICE_DETAIL.Item_Code, TSPL_SALE_INVOICE_DETAIL.Unit_code, TSPL_SALE_INVOICE_DETAIL.invoice_qty, TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_SALE_INVOICE_DETAIL.invoice_qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as FCQty,case when TSPL_SALE_INVOICE_DETAIL.Empty_Value>0 then 1 else 0 end isRGB"
                    strRGBPETQty += " from TSPL_SALE_INVOICE_DETAIL "
                    strRGBPETQty += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code"
                    strRGBPETQty += " where Sale_Invoice_No = '" + strInvoiceNo + "'  "
                    strRGBPETQty += " )xxx group by Sale_Invoice_No"
                    dt = New DataTable()
                    dt = clsDBFuncationality.GetDataTable(strRGBPETQty)
                    strRGBPETQty = ""
                    If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                        strRGBPETQty += IIf(clsCommon.myCdbl(dt.Rows(0)("RGBQty")) > 0, "RGB Cases - " + clsCommon.myCstr(Math.Ceiling(clsCommon.myCdbl(dt.Rows(0)("RGBQty")))), "")
                        strRGBPETQty += IIf(clsCommon.myCdbl(dt.Rows(0)("RGBQty")) > 0, "  PET - " + clsCommon.myCstr(Math.Ceiling(clsCommon.myCdbl(dt.Rows(0)("PETQty")))), "")
                    End If
                    strQuery = "  SELECT TSPL_SALE_INVOICE_HEAD.Created_By,TSPL_SALE_INVOICE_HEAD.Modify_By,  case when TSPL_SALE_INVOICE_DETAIL.Empty_Value>0 then 'RGB' else 'PET' end isRGB,TSPL_SALE_INVOICE_HEAD.Cust_PONo," + qryForShipToAdd + " AS Address, " & _
                   " TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,  convert(varchar(10),TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as Sale_Invoice_Date, TSPL_SALE_INVOICE_HEAD.Cust_Name, " & _
                   " (" + qryForGettingTax + "= TSPL_SALE_INVOICE_HEAD.TAX1) as TAX1, TSPL_SALE_INVOICE_HEAD.TAX1_Rate, TSPL_SALE_INVOICE_HEAD.TAX1_Assessable_Amt, " & _
                   " TSPL_SALE_INVOICE_HEAD.TAX1_Amt, (" + qryForGettingTax + "=TSPL_SALE_INVOICE_HEAD.TAX2) as TAX2, TSPL_SALE_INVOICE_HEAD.TAX2_Rate, " & _
                   " TSPL_SALE_INVOICE_HEAD.TAX2_Assessable_Amt, TSPL_SALE_INVOICE_HEAD.TAX2_Amt,(" + qryForGettingTax + "= TSPL_SALE_INVOICE_HEAD.TAX3) as TAX3, " & _
                   " TSPL_SALE_INVOICE_HEAD.TAX3_Rate, TSPL_SALE_INVOICE_HEAD.TAX3_Assessable_Amt, TSPL_SALE_INVOICE_HEAD.TAX3_Amt, " & _
                   " (" + qryForGettingTax + "= TSPL_SALE_INVOICE_HEAD.TAX4) as TAX4, TSPL_SALE_INVOICE_HEAD.TAX4_Rate, TSPL_SALE_INVOICE_HEAD.TAX4_Assessable_Amt, " & _
                   " TSPL_SALE_INVOICE_HEAD.TAX4_Amt, (" + qryForGettingTax + "= TSPL_SALE_INVOICE_HEAD.TAX5) as TAX5, TSPL_SALE_INVOICE_HEAD.TAX5_Rate, " & _
                   " TSPL_SALE_INVOICE_HEAD.TAX5_Assessable_Amt, TSPL_SALE_INVOICE_HEAD.TAX5_Amt,TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt,TSPL_SALE_INVOICE_HEAD.Vehicle_No,TSPL_SALE_INVOICE_HEAD.KM_Reading , TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as OrgInvoice_Qty, " & _
                   " TSPL_SALE_INVOICE_DETAIL.Item_Code, TSPL_SALE_INVOICE_DETAIL.Item_Desc  + Case WHEN (Scheme_Item='Y' AND ISNULL(Discount_Code,'')='') Then ' (T)' Else case When (Scheme_Item='Y' AND ISNULL(Discount_Code,'')<> '') THEN ' (S)' Else '' END END as Item_Desc,   TSPL_SALE_INVOICE_DETAIL.MRP_Amt/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code = (case when TSPL_SALE_INVOICE_DETAIL.Unit_code='FC' then 'FB' else  case when TSPL_SALE_INVOICE_DETAIL.Unit_code='FB' then 'FC' else '' end end))  as [MRP_Amt], " & _
                   " TSPL_SALE_INVOICE_DETAIL.Basic_Rate as OrgBasic_Rate, TSPL_SALE_INVOICE_DETAIL.Item_Assessable_Rate, TSPL_SALE_INVOICE_DETAIL.Item_Net_Amt, " & _
                   "  TSPL_SALE_INVOICE_DETAIL.TAX1 AS 'DTax1', TSPL_SALE_INVOICE_DETAIL.TAX1_Rate AS 'DTax1Rate', " & _
                   " TSPL_SALE_INVOICE_DETAIL.TAX1_Assessable_Amt AS 'DTax1Ass', '0.00' AS 'Dtax1Amt', " & _
                   " TSPL_SALE_INVOICE_DETAIL.Total_Assessable_Amt, TSPL_SALE_INVOICE_DETAIL.Total_MRP_Amt, TSPL_SALE_INVOICE_DETAIL.Total_Basic_Amt, " & _
                   "  TSPL_SALE_INVOICE_DETAIL.Total_net_Amt, TSPL_SALE_INVOICE_DETAIL.Total_Tax_Amt as OrgTotal_Tax_Amt, TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt, " & _
                   " TSPL_SALE_INVOICE_HEAD.Empty_Value, TSPL_SALE_INVOICE_DETAIL.Total_TPT,TSPL_SALE_INVOICE_HEAD.TPT as 'ttlTPT', " & _
                   " TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  " & _
                   " " + str + "TSPL_CUSTOMER_MASTER.Tin_No,TSPL_SALE_INVOICE_HEAD.Remarks  ,TSPL_SALE_INVOICE_HEAD.Description," + strInvoiceType + " ,(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code = (case when TSPL_SALE_INVOICE_DETAIL.Unit_code='FC' then 'FB' else  case when TSPL_SALE_INVOICE_DETAIL.Unit_code='FB' then 'FC' else '' end end) ) as [Conversion], (CASE WHEN TSPL_SALE_INVOICE_DETAIL.Scheme_Item='Y' or TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item='Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y' or TSPL_SALE_INVOICE_HEAD.Inv_Disc_Percent=100 THEN 'Y' ELSE 'N' END) as FOCItem , case when Inv_Disc_Percent=100 then 0 else TSPL_SALE_INVOICE_HEAD.Inv_Discount_Amt end as Inv_Discount_Amt,TSPL_SALE_INVOICE_HEAD.TAX1 as Tax1Code,TSPL_SALE_INVOICE_HEAD.TAX2 as Tax2Code,TSPL_SALE_INVOICE_HEAD.TAX3 as Tax3Code,TSPL_SALE_INVOICE_HEAD.TAX4 as Tax4Code,TSPL_SALE_INVOICE_HEAD.TAX5 as Tax5Code" & _
                   ",(select [USER_NAME] from TSPL_USER_MASTER where TSPL_USER_MASTER.User_Code=TSPL_SALE_INVOICE_HEAD.Created_By) as CreateByName" & _
                   ",(case when TSPL_SALE_INVOICE_HEAD.Is_Post='Y' then (select [USER_NAME] from TSPL_USER_MASTER where User_Code=TSPL_SALE_INVOICE_HEAD.Modify_By) else '' end) as PostByName,TSPL_SALE_INVOICE_HEAD.Salesman_Code,TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Route_Desc, (select ISNULL( Transfer_No,'')  from TSPL_SHIPMENT_MASTER where Shipment_No=TSPL_SALE_INVOICE_HEAD.Shipment_No) as TransferNo,(select ISNULL( Customer_Invoice_No,'')  from TSPL_SHIPMENT_MASTER where Shipment_No=TSPL_SALE_INVOICE_HEAD.Shipment_No) as CustomerInvNo ,(case when TSPL_SALE_INVOICE_HEAD.is_Printed =1 then (select [USER_NAME] from TSPL_USER_MASTER where User_Code=TSPL_SALE_INVOICE_HEAD.Verify_By)else '' end ) as VerifyByName ,case when len(ISNULL( TSPL_SALE_INVOICE_DETAIL.Main_Item,''))>0 then (select Sku_Seq from TSPL_ITEM_MASTER where Item_Code=TSPL_SALE_INVOICE_DETAIL.Main_Item ) else TSPL_ITEM_MASTER.Sku_Seq end as OrderBySKUNo,TSPL_SALE_INVOICE_DETAIL.Unit_code " & _
                   "  ,(select top 1 GPCode from TSPL_GATEPASS_DETAIL where TSPL_GATEPASS_DETAIL.DocNo=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) as GPCode,(case when Credit_Invoice='Y' then 'Credit' else '' end )as Credit_Invoice,TSPL_COMPANY_MASTER.Phone1 as CompanyPhone1,TSPL_COMPANY_MASTER.Phone2 as CompanyPhone2,TSPL_COMPANY_MASTER.Tin_No AS CompamyTinNo,TSPL_COMPANY_MASTER.CST_LST as CompanyCSTNo,TSPL_COMPANY_MASTER.Add1+' '+TSPL_COMPANY_MASTER.Add2+' '+TSPL_COMPANY_MASTER.Add3 as CompanyAddress ,TSPL_EMPLOYEE_MASTER.Emp_Name as SalesmanName ,Discount_Code  " & _
                   ",(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code and UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code ) as OrgConversionFactor,TSPL_SALE_INVOICE_HEAD.Location ,(TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add4,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add4,'') else ' ' end) as LocationAddress, " & _
                   " TSPL_LOCATION_MASTER.Pin_Code  as locPin, TSPL_LOCATION_MASTER.TIN_No as LocTinNo, TSPL_LOCATION_MASTER.CST_No as locCSTNo, TSPL_LOCATION_MASTER.Phone1 as locPhone1, TSPL_LOCATION_MASTER.Phone2 as locPhone2" & _
                   " FROM TSPL_CUSTOMER_MASTER INNER JOIN " & _
                   "  TSPL_SALE_INVOICE_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_INVOICE_HEAD.Cust_Code INNER JOIN " & _
                   " TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No   left outer join TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code =TSPL_ITEM_MASTER .Item_Code " & _
                   " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.location_code=TSPL_SALE_INVOICE_HEAD.location " & _
                   " left outer join TSPL_COMPANY_MASTER on TSPL_SALE_INVOICE_HEAD.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_SALE_INVOICE_HEAD.Salesman_Code left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code and TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SALE_INVOICE_HEAD.Ship_To "
                    strQuery = strQuery & whereclause
                    strQuery = " select *,case when FOCItem='Y' and LEN(Discount_Code)>0 then OrgInvoice_Qty*OrgBasic_Rate  else 0 end as Total_Basic_Amt_FOC,OrgInvoice_Qty as Invoice_Qty,OrgBasic_Rate as Basic_Rate,case when FOCItem='Y' and LEN(Discount_Code)>0 then case when Item_Code like 'SM%' then OrgInvoice_Qty*(OrgBasic_Rate*5/100) else OrgInvoice_Qty*(OrgBasic_Rate*14.5/100) end else OrgTotal_Tax_Amt end as  Total_Tax_Amt ,case when FOCItem='Y' and LEN(Discount_Code)>0 then case when Item_Code like 'SM%' then OrgInvoice_Qty*(OrgBasic_Rate*5/100) else OrgInvoice_Qty*(OrgBasic_Rate*14.5/100) end else 0 end as  FOC_TAX_Discount_AMT,'" + strRGBPETQty + "' as RGBPETQty from (" + strQuery + ")xxx"
                    strQuery += " order by OrderBySKUNo,Unit_code desc"

                    Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(strQuery)
                    SetItemWiseTax(prePrinted, dtNew, strInvoiceNo)
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GUNTUR") = CompairStringResult.Equal Then
                        frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dtNew, EnumTecxpertPaperSize.NA, "rptSaleInvoiceNonExcise-G", "Sales Report", True)
                    Else
                        frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dtNew, EnumTecxpertPaperSize.NA, "rptSaleInvoiceNonExcise", "Sales Report", True)
                    End If

                End If
            End If

            If clsCommon.myLen(strShipmentNo) > 0 Then
                qry = "select Is_Post from TSPL_SHIPMENT_MASTER where Shipment_No='" + strShipmentNo + "'"
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "N") = CompairStringResult.Equal Then
                    Dim isSettingOn As Boolean = IIf(clsCommon.CompairString("Y", clsFixedParameter.GetData(clsFixedParameterType.PrintVerify, clsFixedParameterCode.SalesInvoice, Nothing)) = CompairStringResult.Equal, True, False)

                    Dim isCorrect As Integer = 0
                    If isSettingOn Then
                        If (common.clsCommon.MyMessageBoxShow("Have you checked and verified Sale Invoice properly ? ", "Load out", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                            isCorrect = 1
                        End If
                    Else
                        isCorrect = 1
                    End If
                    qry = " update TSPL_SHIPMENT_MASTER set Is_Printed='" + clsCommon.myCstr(isCorrect) + "',Verify_By='" + IIf(isCorrect = 1, objCommonVar.CurrentUserCode, "") + "' where Shipment_No='" + strShipmentNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)

                    qry = " update TSPL_SALE_INVOICE_HEAD set Is_Printed='" + clsCommon.myCstr(isCorrect) + "',Verify_By='" + IIf(isCorrect = 1, objCommonVar.CurrentUserCode, "") + "' where Sale_Invoice_No='" + strInvoiceNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Load out")
        End Try
    End Sub

    Private Shared Function SetItemWiseTax(ByVal isPrePrint As Boolean, ByVal dtAfterModify As DataTable, ByVal strShipFrm As String) As DataTable
        dtAfterModify.Columns.Add("TAX1_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Amt3", GetType(Double))

        dtAfterModify.Columns.Add("TAX2_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Amt3", GetType(Double))

        dtAfterModify.Columns.Add("TAX3_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Amt3", GetType(Double))

        dtAfterModify.Columns.Add("TAX4_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Amt3", GetType(Double))

        dtAfterModify.Columns.Add("TAX5_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Amt3", GetType(Double))


        'If Not isPrePrint Then
        Dim qry As String = "select Tax,Rate,SUM(Amt*Invoice_Qty) as TaxAmt" + Environment.NewLine
        qry += " from (" + Environment.NewLine
        qry += " select TAX1 as Tax,TAX1_Rate as Rate,TAX1_Amt as Amt,Invoice_Qty" + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1" + Environment.NewLine
        qry += " where Sale_Invoice_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += " union all " + Environment.NewLine
        qry += " select TAX2 as Tax,TAX2_Rate as Rate,TAX2_Amt as Amt,Invoice_Qty " + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2" + Environment.NewLine
        qry += " where Sale_Invoice_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += " union all " + Environment.NewLine
        qry += " select TAX3 as Tax,TAX3_Rate as Rate,TAX3_Amt as Amt,Invoice_Qty " + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3" + Environment.NewLine
        qry += " where Sale_Invoice_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += "  union all " + Environment.NewLine
        qry += " select TAX4 as Tax,TAX4_Rate as Rate,TAX4_Amt as Amt,Invoice_Qty " + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4" + Environment.NewLine
        qry += " where Sale_Invoice_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += " union all " + Environment.NewLine
        qry += " select TAX5 as Tax,TAX5_Rate as Rate,TAX5_Amt as Amt,Invoice_Qty " + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5" + Environment.NewLine
        qry += " where Sale_Invoice_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += " union all " + Environment.NewLine
        qry += " select TAX6 as Tax,TAX6_Rate as Rate,TAX6_Amt as Amt,Invoice_Qty " + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6" + Environment.NewLine
        qry += " where Sale_Invoice_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += " )xxx " + Environment.NewLine
        qry += " group by Tax,Rate   " + Environment.NewLine

        Dim qryMain As String = qry + " having Tax in( select Tax from(" + qry + ")xxxx group by tax having SUM(1)>1) and SUM(Amt*Invoice_Qty)>0"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryMain)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                For ii As Integer = 1 To 5
                    Dim strCol As String = "TAX" + clsCommon.myCstr(ii) + "Code"
                    If clsCommon.CompairString(clsCommon.myCstr(dtAfterModify.Rows(0)(strCol)), clsCommon.myCstr(dr("Tax"))) = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt1")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate1") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt1") = clsCommon.myCdbl(dr("TaxAmt"))
                            Next
                        ElseIf clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt2")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate2") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt2") = clsCommon.myCdbl(dr("TaxAmt"))
                            Next
                        ElseIf clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt3")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate3") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt3") = clsCommon.myCdbl(dr("TaxAmt"))
                            Next
                        Else
                            Throw New Exception("Printing Support only 3 Diffent Rates")
                        End If

                    End If
                Next
            Next
        End If
        'End If
        Return dtAfterModify
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        fndLoadoutFrm.txtValue.Text = ""
        chkPre.Checked = False
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Me.Close()
    End Sub

#End Region

    Private Sub chkPre_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkPre.ToggleStateChanged
        ''sql = "Select Location FROM TSPL_SALE_INVOICE_HEAD WHERE Sale_Invoice_No='" + fndLoadoutFrm.txtValue.Text + "'"
        ''Dim location As String = connectSql.RunScalar(sql)
        ''sql = "Select Excisable from TSPL_LOCATION_MASTER WHERE Location_Code='" + location + "' "
        ''Dim LType As String = connectSql.RunScalar(sql)
        ''If LType = "F" Or LType = "N" Then
        ''    If chkPre.Checked = True Then
        ''        fndLoadoutFrm.txtValue.Text = ""
        ''    End If
        ''End If

    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "SAL-INV-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
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
    '        If strTemp(1) = "0" Then 'Grant modify access

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function


End Class

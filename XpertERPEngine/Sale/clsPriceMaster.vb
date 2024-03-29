''updation by Richa Agarwal Against Ticket No. BM00000003706 (add field active)

Imports common
Imports System.Data.SqlClient

Public Class clsPriceMaster

#Region "Variables"
    Public Item_Price_ID As String
    Public Remarks As String
    Public Item_Code As String
    Public Item_MRP As Double
    Public Item_Basic_Price_Type As String
    Public Item_Baisc_Price As Double
    Public Markup_On As String
    Public Markup_Percent As String
    Public Basic_Price_On As String
    Public Landing_Cost As Double
    Public Purchase_Cost As Double
    Public Item_Selling_Price As Double
    Public Abatement As String
    Public Item_Basic_Net As Double
    Public Start_Date As Date
    Public End_Date As Date?
    Public Empty_Value_Shell As Double
    Public Can_Edit As Char
    Public NetLTPT As Double
    Public Price_Category As String
    Public Is_With_Tax As Char = ""
    Public Item_Basic_Price As Double
    Public Tax_Manipulation_On As String
    Public Location_Code As String
    Public Location_Desc As String
    Public Tax_group As String = ""
    Public TAX1 As String = ""
    Public TAX1_Rate As Double = 0
    Public TAX1_Amt As Double = 0

    Public TAX2 As String = ""
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0

    Public TAX3 As String = ""
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0

    Public TAX4 As String = ""
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0

    Public TAX5 As String = ""
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0

    Public TAX6 As String = ""
    Public TAX6_Rate As Double = 0
    Public TAX6_Amt As Double = 0

    Public TAX7 As String = ""
    Public TAX7_Rate As Double = 0
    Public TAX7_Amt As Double = 0

    Public TAX8 As String = ""
    Public TAX8_Rate As Double = 0
    Public TAX8_Amt As Double = 0

    Public TAX9 As String = ""
    Public TAX9_Rate As Double = 0
    Public TAX9_Amt As Double = 0

    Public TAX10 As String = ""
    Public TAX10_Rate As Double = 0
    Public TAX10_Amt As Double = 0

    Public Price_Code As String
    Public Price_Code_Desc As String

    Public Price_Comp1 As String
    Public Price_Comp_Desc1 As String
    Public Price_Rate1 As Double
    Public Price_Amount1 As Double

    Public Price_Comp2 As String
    Public Price_Comp_Desc2 As String
    Public Price_Rate2 As Double
    Public Price_Amount2 As Double

    Public Price_Comp3 As String
    Public Price_Comp_Desc3 As String
    Public Price_Rate3 As Double
    Public Price_Amount3 As Double

    Public Price_Comp4 As String
    Public Price_Comp_Desc4 As String
    Public Price_Rate4 As Double
    Public Price_Amount4 As Double

    Public Price_Comp5 As String
    Public Price_Comp_Desc5 As String
    Public Price_Rate5 As Double
    Public Price_Amount5 As Double

    Public Price_Comp6 As String
    Public Price_Comp_Desc6 As String
    Public Price_Rate6 As Double
    Public Price_Amount6 As Double

    Public Price_Comp7 As String
    Public Price_Comp_Desc7 As String
    Public Price_Rate7 As Double
    Public Price_Amount7 As Double

    Public Price_Comp8 As String
    Public Price_Comp_Desc8 As String
    Public Price_Rate8 As Double
    Public Price_Amount8 As Double

    Public Price_Comp9 As String
    Public Price_Comp_Desc9 As String
    Public Price_Rate9 As Double
    Public Price_Amount9 As Double

    Public Price_Comp10 As String
    Public Price_Comp_Desc10 As String
    Public Price_Rate10 As Double
    Public Price_Amount10 As Double

    Public Item_Rate As Double
    Public Liquid_Rate As Double
    Public Stock_Rate As Double
    Public Abatement_Rate As Double
    Public UOM As String
    Public Empty_Value_Bottle As Double
    Public Item_Price_Id_No As Integer
    Public Tax_Group_Type As String

    Public CurrentDate As String = clsCommon.GetPrintDate(DateTime.Now, "dd/MMM/yyyy")
    Public Form_ID As String = ""
    Public Is_Active As Integer = 0
    Public Is_For_Price As Integer = 0
    Public IsNewEntry As Boolean = False
    Public Stock_Price_Id As String = ""
    '=================added by preeti gupta===========
    Public type As String = Nothing
    Public Posted As Decimal = Nothing
    '=================================================
    Public Against_Plan_TR_Code As String = Nothing
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public Against_ItemwiseTaxCode As String = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        '  Dim qry As String = " select convert (varchar,TSPL_ITEM_PRICE_MASTER.Item_Price_ID) as Code,TSPL_ITEM_PRICE_MASTER.Location_Code as [Location Code] ,TSPL_ITEM_PRICE_MASTER.Item_Code +' - '+ TSPL_ITEM_MASTER.Item_Desc as [Item Code],ISNULL(TSPL_ITEM_MASTER.Short_Description,'') AS [Short Description] ,TSPL_ITEM_PRICE_MASTER.Item_MRP as [Item MRP] ,TSPL_ITEM_PRICE_MASTER.Item_Baisc_Price as [Item Baisc Price] ,TSPL_ITEM_PRICE_MASTER.Abatement as [Abatement] ,TSPL_ITEM_PRICE_MASTER.Item_Basic_Net as [Item Basic Net] ,TSPL_ITEM_PRICE_MASTER.Start_Date as [Start Date] ,TSPL_ITEM_PRICE_MASTER.End_Date as [End Date] ,TSPL_ITEM_PRICE_MASTER.Empty_Value_Shell as [Empty Value Shell] ,TSPL_ITEM_PRICE_MASTER.Can_Edit as [Can Edit] ,TSPL_ITEM_PRICE_MASTER.NetLTPT as [Net LTPT] ,TSPL_ITEM_PRICE_MASTER.Price_Category as [Price Category] ,TSPL_ITEM_PRICE_MASTER.Item_Basic_Price as [Item Basic Price] ,TSPL_ITEM_PRICE_MASTER.Tax_group as [Tax Group] ,TSPL_ITEM_PRICE_MASTER.TAX1 as [Tax1] ,TSPL_ITEM_PRICE_MASTER.TAX1_Rate as [Tax1 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX1_Amt as [Tax1 Amountt] ,TSPL_ITEM_PRICE_MASTER.TAX2 as [Tax2] ,TSPL_ITEM_PRICE_MASTER.TAX2_Rate as [Tax2 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX2_Amt as [Tax2 Amount] ,TSPL_ITEM_PRICE_MASTER.TAX3 as [Tax3] ,TSPL_ITEM_PRICE_MASTER.TAX3_Rate as [Tax3 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt as [Tax3 Amount] ,TSPL_ITEM_PRICE_MASTER.TAX4 as [Tax4] ,TSPL_ITEM_PRICE_MASTER.TAX4_Rate as [Tax4 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt as [Tax4 Amount] ,TSPL_ITEM_PRICE_MASTER.TAX5 as [Tax5] ,TSPL_ITEM_PRICE_MASTER.TAX5_Rate as [Tax5 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX5_Amt as [Tax5 Amount] ,TSPL_ITEM_PRICE_MASTER.TAX6 as [Tax6] ,TSPL_ITEM_PRICE_MASTER.TAX6_Rate as [Tax6 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX6_Amt as [Tax6 Amount] ,TSPL_ITEM_PRICE_MASTER.TAX7 as [Tax7] ,TSPL_ITEM_PRICE_MASTER.TAX7_Rate as [Tax7 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX7_Amt as [Tax7 Amount] ,TSPL_ITEM_PRICE_MASTER.TAX8 as [Tax8] ,TSPL_ITEM_PRICE_MASTER.TAX8_Rate as [Tax8 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX8_Amt as [Tax8 Amount] ,TSPL_ITEM_PRICE_MASTER.TAX9 as [Tax9] ,TSPL_ITEM_PRICE_MASTER.TAX9_Rate as [Tax9 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX9_Amt as [Tax9 Amount] ,TSPL_ITEM_PRICE_MASTER.TAX10 as [Tax10] ,TSPL_ITEM_PRICE_MASTER.TAX10_Rate as [Tax10 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX10_Amt as [Tax10 Amount] ,TSPL_ITEM_PRICE_MASTER.Price_Code as [Price Code] ,TSPL_ITEM_PRICE_MASTER.Price_Comp1 as [Price Component Code1] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc1 as [Price Component Description1] ,TSPL_ITEM_PRICE_MASTER.Price_Rate1 as [Price Rate1] ,TSPL_ITEM_PRICE_MASTER.Price_Amount1 as [Price Amount1] ,TSPL_ITEM_PRICE_MASTER.Price_Comp2 as [Price Component Code2] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc2 as [Price Component Description2] ,TSPL_ITEM_PRICE_MASTER.Price_Rate2 as [Price Rate2] ,TSPL_ITEM_PRICE_MASTER.Price_Amount2 as [Price Amount2] ,TSPL_ITEM_PRICE_MASTER.Price_Comp3 as [Price Component Code3] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc3 as [Price Component Description3] ,TSPL_ITEM_PRICE_MASTER.Price_Rate3 as [Price Rate3] ,TSPL_ITEM_PRICE_MASTER.Price_Amount3 as [Price Amount3] ,TSPL_ITEM_PRICE_MASTER.Price_Comp4 as [Price Component Code4] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc4 as [Price Component Description4] ,TSPL_ITEM_PRICE_MASTER.Price_Rate4 as [Price Rate4] ,TSPL_ITEM_PRICE_MASTER.Price_Amount4 as [Price Amount4] ,TSPL_ITEM_PRICE_MASTER.Price_Comp5 as [Price Component Code5] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc5 as [Price Component Description5] ,TSPL_ITEM_PRICE_MASTER.Price_Rate5 as [Price Rate5] ,TSPL_ITEM_PRICE_MASTER.Price_Amount5 as [Price Amount5] ,TSPL_ITEM_PRICE_MASTER.Price_Comp6 as [Price Component Code6] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc6 as [Price Component Description6] ,TSPL_ITEM_PRICE_MASTER.Price_Rate6 as [Price Rate6] ,TSPL_ITEM_PRICE_MASTER.Price_Amount6 as [Price Amount6] ,TSPL_ITEM_PRICE_MASTER.Price_Comp7 as [Price Component Code7] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc7 as [Price Component Description7] ,TSPL_ITEM_PRICE_MASTER.Price_Rate7 as [Price Rate7] ,TSPL_ITEM_PRICE_MASTER.Price_Amount7 as [Price Amount7] ,TSPL_ITEM_PRICE_MASTER.Price_Comp8 as [Price Component Code8] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc8 as [Price Comp Description8] ,TSPL_ITEM_PRICE_MASTER.Price_Rate8 as [Price Rate8] ,TSPL_ITEM_PRICE_MASTER.Price_Amount8 as [Price Amount8] ,TSPL_ITEM_PRICE_MASTER.Price_Comp9 as [Price Component Code9] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc9 as [Price Comp Description9] ,TSPL_ITEM_PRICE_MASTER.Price_Rate9 as [Price Rate9] ,TSPL_ITEM_PRICE_MASTER.Price_Amount9 as [Price Amount9] ,TSPL_ITEM_PRICE_MASTER.Price_Comp10 as [Price Component Code10] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc10 as [Price Component Desc10] ,TSPL_ITEM_PRICE_MASTER.Price_Rate10 as [Price Rate10] ,TSPL_ITEM_PRICE_MASTER.Price_Amount10 as [Price Amount10] ,TSPL_ITEM_PRICE_MASTER.Item_Rate as [Item Rate] ,TSPL_ITEM_PRICE_MASTER.Liquid_Rate as [Liquid Rate] ,TSPL_ITEM_PRICE_MASTER.Stock_Rate as [Stock Rate] ,TSPL_ITEM_PRICE_MASTER.Created_By as [Created By] ,TSPL_ITEM_PRICE_MASTER.Created_Date as [Created Date] ,TSPL_ITEM_PRICE_MASTER.Modify_By as [Modify By] ,TSPL_ITEM_PRICE_MASTER.Modify_Date as [Modify Date] ,TSPL_ITEM_PRICE_MASTER.Comp_Code as [Company Code] ,TSPL_ITEM_PRICE_MASTER.Abatement_Rate as [Abatement Rate] ,TSPL_ITEM_PRICE_MASTER.UOM as [UOM] ,TSPL_ITEM_PRICE_MASTER.Price_Code_Desc as [Price Code Description] ,TSPL_ITEM_PRICE_MASTER.Empty_Value_Bottle as [Empty Value Bottle] ,TSPL_ITEM_PRICE_MASTER.Item_Price_Id_No as [Item Price Id No] ,TSPL_ITEM_PRICE_MASTER.Tax_Group_Type as [Tax Group Type] ,TSPL_ITEM_PRICE_MASTER.Remarks as [Remarks] ,TSPL_ITEM_PRICE_MASTER.Item_Selling_Price as [Item Selling Price] ,TSPL_ITEM_PRICE_MASTER.Item_Basic_Price_Type as [Item Basic Price Type] ,TSPL_ITEM_PRICE_MASTER.Markup_On as [Markup On] ,TSPL_ITEM_PRICE_MASTER.Markup_Percent as [Markup Percent] ,TSPL_ITEM_PRICE_MASTER.Landing_Cost as [Landing Cost] ,TSPL_ITEM_PRICE_MASTER.Tax_Manipulation_On as [Tax Manipulation On] ,TSPL_ITEM_PRICE_MASTER.Purchase_Cost as [Purchase Cost] ,TSPL_ITEM_PRICE_MASTER.Basic_Price_On as [Basic Price On],TSPL_ITEM_PRICE_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_ITEM_PRICE_MASTER.Posted_By as [Approved by],TSPL_ITEM_PRICE_MASTER.Posted_Date as [Approved Date],case isnull(TSPL_ITEM_PRICE_MASTER.Posted,0) when '0' Then 'Pending' when '1' then 'Approved' else '' end as 'Status'  From TSPL_ITEM_PRICE_MASTER LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_ITEM_PRICE_MASTER.Location_Code "
        Dim qry As String = "  select convert (varchar,TSPL_ITEM_PRICE_MASTER.Item_Price_ID) as Code,TSPL_ITEM_PRICE_MASTER.Location_Code as [Location Code] ,TSPL_ITEM_PRICE_MASTER.Item_Code +' - '+ TSPL_ITEM_MASTER.Item_Desc as [Item Code],ISNULL(TSPL_ITEM_MASTER.Short_Description,'') AS [Short Description] ,TSPL_ITEM_PRICE_MASTER.Item_MRP as [Item MRP] ,TSPL_ITEM_PRICE_MASTER.Item_Baisc_Price as [Item Baisc Price] ,TSPL_ITEM_PRICE_MASTER.Item_Basic_Net as [Item Basic Net] ,TSPL_ITEM_PRICE_MASTER.Start_Date as [Start Date] ,TSPL_ITEM_PRICE_MASTER.End_Date as [End Date] ,TSPL_ITEM_PRICE_MASTER.Empty_Value_Shell as [Empty Value Shell] ,TSPL_ITEM_PRICE_MASTER.Can_Edit as [Can Edit] ,TSPL_ITEM_PRICE_MASTER.NetLTPT as [Net LTPT] ,TSPL_ITEM_PRICE_MASTER.Price_Category as [Price Category] ,TSPL_ITEM_PRICE_MASTER.Item_Basic_Price as [Item Basic Price] ,TSPL_ITEM_PRICE_MASTER.Tax_group as [Tax Group] ,TSPL_ITEM_PRICE_MASTER.TAX1 as [Tax1] ,TSPL_ITEM_PRICE_MASTER.TAX1_Rate as [Tax1 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX1_Amt as [Tax1 Amountt] ,TSPL_ITEM_PRICE_MASTER.TAX2 as [Tax2] ,TSPL_ITEM_PRICE_MASTER.TAX2_Rate as [Tax2 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX2_Amt as [Tax2 Amount] ,TSPL_ITEM_PRICE_MASTER.TAX3 as [Tax3] ,TSPL_ITEM_PRICE_MASTER.TAX3_Rate as [Tax3 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt as [Tax3 Amount] ,TSPL_ITEM_PRICE_MASTER.TAX4 as [Tax4] ,TSPL_ITEM_PRICE_MASTER.TAX4_Rate as [Tax4 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt as [Tax4 Amount] ,TSPL_ITEM_PRICE_MASTER.TAX5 as [Tax5] ,TSPL_ITEM_PRICE_MASTER.TAX5_Rate as [Tax5 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX5_Amt as [Tax5 Amount] ,TSPL_ITEM_PRICE_MASTER.TAX6 as [Tax6] ,TSPL_ITEM_PRICE_MASTER.TAX6_Rate as [Tax6 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX6_Amt as [Tax6 Amount] ,TSPL_ITEM_PRICE_MASTER.TAX7 as [Tax7] ,TSPL_ITEM_PRICE_MASTER.TAX7_Rate as [Tax7 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX7_Amt as [Tax7 Amount] ,TSPL_ITEM_PRICE_MASTER.TAX8 as [Tax8] ,TSPL_ITEM_PRICE_MASTER.TAX8_Rate as [Tax8 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX8_Amt as [Tax8 Amount] ,TSPL_ITEM_PRICE_MASTER.TAX9 as [Tax9] ,TSPL_ITEM_PRICE_MASTER.TAX9_Rate as [Tax9 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX9_Amt as [Tax9 Amount] ,TSPL_ITEM_PRICE_MASTER.TAX10 as [Tax10] ,TSPL_ITEM_PRICE_MASTER.TAX10_Rate as [Tax10 Rate] ,TSPL_ITEM_PRICE_MASTER.TAX10_Amt as [Tax10 Amount] ,TSPL_ITEM_PRICE_MASTER.Price_Code as [Price Code] ,TSPL_ITEM_PRICE_MASTER.Price_Comp1 as [Price Component Code1] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc1 as [Price Component Description1] ,TSPL_ITEM_PRICE_MASTER.Price_Rate1 as [Price Rate1] ,TSPL_ITEM_PRICE_MASTER.Price_Amount1 as [Price Amount1] ,TSPL_ITEM_PRICE_MASTER.Price_Comp2 as [Price Component Code2] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc2 as [Price Component Description2] ,TSPL_ITEM_PRICE_MASTER.Price_Rate2 as [Price Rate2] ,TSPL_ITEM_PRICE_MASTER.Price_Amount2 as [Price Amount2] ,TSPL_ITEM_PRICE_MASTER.Price_Comp3 as [Price Component Code3] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc3 as [Price Component Description3] ,TSPL_ITEM_PRICE_MASTER.Price_Rate3 as [Price Rate3] ,TSPL_ITEM_PRICE_MASTER.Price_Amount3 as [Price Amount3] ,TSPL_ITEM_PRICE_MASTER.Price_Comp4 as [Price Component Code4] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc4 as [Price Component Description4] ,TSPL_ITEM_PRICE_MASTER.Price_Rate4 as [Price Rate4] ,TSPL_ITEM_PRICE_MASTER.Price_Amount4 as [Price Amount4] ,TSPL_ITEM_PRICE_MASTER.Price_Comp5 as [Price Component Code5] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc5 as [Price Component Description5] ,TSPL_ITEM_PRICE_MASTER.Price_Rate5 as [Price Rate5] ,TSPL_ITEM_PRICE_MASTER.Price_Amount5 as [Price Amount5] ,TSPL_ITEM_PRICE_MASTER.Price_Comp6 as [Price Component Code6] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc6 as [Price Component Description6] ,TSPL_ITEM_PRICE_MASTER.Price_Rate6 as [Price Rate6] ,TSPL_ITEM_PRICE_MASTER.Price_Amount6 as [Price Amount6] ,TSPL_ITEM_PRICE_MASTER.Price_Comp7 as [Price Component Code7] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc7 as [Price Component Description7] ,TSPL_ITEM_PRICE_MASTER.Price_Rate7 as [Price Rate7] ,TSPL_ITEM_PRICE_MASTER.Price_Amount7 as [Price Amount7] ,TSPL_ITEM_PRICE_MASTER.Price_Comp8 as [Price Component Code8] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc8 as [Price Comp Description8] ,TSPL_ITEM_PRICE_MASTER.Price_Rate8 as [Price Rate8] ,TSPL_ITEM_PRICE_MASTER.Price_Amount8 as [Price Amount8] ,TSPL_ITEM_PRICE_MASTER.Price_Comp9 as [Price Component Code9] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc9 as [Price Comp Description9] ,TSPL_ITEM_PRICE_MASTER.Price_Rate9 as [Price Rate9] ,TSPL_ITEM_PRICE_MASTER.Price_Amount9 as [Price Amount9] ,TSPL_ITEM_PRICE_MASTER.Price_Comp10 as [Price Component Code10] ,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc10 as [Price Component Desc10] ,TSPL_ITEM_PRICE_MASTER.Price_Rate10 as [Price Rate10] ,TSPL_ITEM_PRICE_MASTER.Price_Amount10 as [Price Amount10] ,TSPL_ITEM_PRICE_MASTER.Item_Rate as [Item Rate] ,TSPL_ITEM_PRICE_MASTER.Liquid_Rate as [Liquid Rate] ,TSPL_ITEM_PRICE_MASTER.Stock_Rate as [Stock Rate] ,TSPL_ITEM_PRICE_MASTER.Created_By as [Created By] ,TSPL_ITEM_PRICE_MASTER.Created_Date as [Created Date] ,TSPL_ITEM_PRICE_MASTER.Modify_By as [Modify By] ,TSPL_ITEM_PRICE_MASTER.Modify_Date as [Modify Date] ,TSPL_ITEM_PRICE_MASTER.Comp_Code as [Company Code]  ,TSPL_ITEM_PRICE_MASTER.UOM as [UOM] ,TSPL_ITEM_PRICE_MASTER.Price_Code_Desc as [Price Code Description] ,TSPL_ITEM_PRICE_MASTER.Empty_Value_Bottle as [Empty Value Bottle] ,TSPL_ITEM_PRICE_MASTER.Item_Price_Id_No as [Item Price Id No] ,TSPL_ITEM_PRICE_MASTER.Tax_Group_Type as [Tax Group Type] ,TSPL_ITEM_PRICE_MASTER.Remarks as [Remarks] ,TSPL_ITEM_PRICE_MASTER.Item_Selling_Price as [Item Selling Price] ,TSPL_ITEM_PRICE_MASTER.Item_Basic_Price_Type as [Item Basic Price Type] ,TSPL_ITEM_PRICE_MASTER.Tax_Manipulation_On as [Tax Manipulation On] ,TSPL_ITEM_PRICE_MASTER.Basic_Price_On as [Basic Price On],TSPL_ITEM_PRICE_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_ITEM_PRICE_MASTER.Posted_By as [Approved by],TSPL_ITEM_PRICE_MASTER.Posted_Date as [Approved Date],case isnull(TSPL_ITEM_PRICE_MASTER.Posted,0) when '0' Then 'Pending' when '1' then 'Approved' else '' end as 'Status'  From TSPL_ITEM_PRICE_MASTER LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_ITEM_PRICE_MASTER.Location_Code "
        str = clsCommon.ShowSelectForm("PRCMSTFNDgrid", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'

    Public Shared Function SaveData(ByVal arr As List(Of clsPriceMaster), ByVal IsFromScreen As Boolean) As Boolean
        Return SaveData(arr, IsFromScreen, False)
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of clsPriceMaster), ByVal IsFromScreen As Boolean, ByVal IsWithBackCalculation As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(arr, IsFromScreen, IsWithBackCalculation, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of clsPriceMaster), ByVal IsFromScreen As Boolean, ByVal IsWithBackCalculation As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Stock_Price_Id As String = ""
            Dim Count As Integer = 0
            For Each obj As clsPriceMaster In arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Item_MRP", obj.Item_MRP)
                clsCommon.AddColumnsForChange(coll, "Item_Basic_Price_Type", obj.Item_Basic_Price_Type)
                clsCommon.AddColumnsForChange(coll, "Markup_On", obj.Markup_On)
                clsCommon.AddColumnsForChange(coll, "Markup_Percent", obj.Markup_Percent)
                clsCommon.AddColumnsForChange(coll, "Basic_Price_On", obj.Basic_Price_On)
                clsCommon.AddColumnsForChange(coll, "Landing_Cost", obj.Landing_Cost)
                clsCommon.AddColumnsForChange(coll, "Purchase_Cost", obj.Purchase_Cost)
                If obj.End_Date Is Nothing Or clsCommon.myLen(obj.End_Date) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
                Else
                    clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Empty_Value_Shell", obj.Empty_Value_Shell)
                clsCommon.AddColumnsForChange(coll, "Can_Edit", obj.Can_Edit)
                clsCommon.AddColumnsForChange(coll, "NetLTPT", obj.NetLTPT)
                clsCommon.AddColumnsForChange(coll, "Price_Category", obj.Price_Category)
                clsCommon.AddColumnsForChange(coll, "Is_With_Tax", obj.Is_With_Tax)
                clsCommon.AddColumnsForChange(coll, "Tax_Manipulation_On", obj.Tax_Manipulation_On)
                clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Tax_group", obj.Tax_group)

                clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)

                clsCommon.AddColumnsForChange(coll, "Price_Comp1", obj.Price_Comp1)
                clsCommon.AddColumnsForChange(coll, "Price_Comp_Desc1", obj.Price_Comp_Desc1)
                clsCommon.AddColumnsForChange(coll, "Price_Rate1", obj.Price_Rate1)
                clsCommon.AddColumnsForChange(coll, "Price_Amount1", obj.Price_Amount1)

                clsCommon.AddColumnsForChange(coll, "Price_Comp2", obj.Price_Comp2)
                clsCommon.AddColumnsForChange(coll, "Price_Comp_Desc2", obj.Price_Comp_Desc2)
                clsCommon.AddColumnsForChange(coll, "Price_Rate2", obj.Price_Rate2)
                clsCommon.AddColumnsForChange(coll, "Price_Amount2", obj.Price_Amount2)

                clsCommon.AddColumnsForChange(coll, "Price_Comp3", obj.Price_Comp3)
                clsCommon.AddColumnsForChange(coll, "Price_Comp_Desc3", obj.Price_Comp_Desc3)
                clsCommon.AddColumnsForChange(coll, "Price_Rate3", obj.Price_Rate3)
                clsCommon.AddColumnsForChange(coll, "Price_Amount3", obj.Price_Amount3)

                clsCommon.AddColumnsForChange(coll, "Price_Comp4", obj.Price_Comp4)
                clsCommon.AddColumnsForChange(coll, "Price_Comp_Desc4", obj.Price_Comp_Desc4)
                clsCommon.AddColumnsForChange(coll, "Price_Rate4", obj.Price_Rate4)
                clsCommon.AddColumnsForChange(coll, "Price_Amount4", obj.Price_Amount4)

                clsCommon.AddColumnsForChange(coll, "Price_Comp5", obj.Price_Comp5)
                clsCommon.AddColumnsForChange(coll, "Price_Comp_Desc5", obj.Price_Comp_Desc5)
                clsCommon.AddColumnsForChange(coll, "Price_Rate5", obj.Price_Rate5)
                clsCommon.AddColumnsForChange(coll, "Price_Amount5", obj.Price_Amount5)

                clsCommon.AddColumnsForChange(coll, "Price_Comp6", obj.Price_Comp6)
                clsCommon.AddColumnsForChange(coll, "Price_Comp_Desc6", obj.Price_Comp_Desc6)
                clsCommon.AddColumnsForChange(coll, "Price_Rate6", obj.Price_Rate6)
                clsCommon.AddColumnsForChange(coll, "Price_Amount6", obj.Price_Amount6)

                clsCommon.AddColumnsForChange(coll, "Price_Comp7", obj.Price_Comp7)
                clsCommon.AddColumnsForChange(coll, "Price_Comp_Desc7", obj.Price_Comp_Desc7)
                clsCommon.AddColumnsForChange(coll, "Price_Rate7", obj.Price_Rate7)
                clsCommon.AddColumnsForChange(coll, "Price_Amount7", obj.Price_Amount7)

                clsCommon.AddColumnsForChange(coll, "Price_Comp8", obj.Price_Comp8)
                clsCommon.AddColumnsForChange(coll, "Price_Comp_Desc8", obj.Price_Comp_Desc8)
                clsCommon.AddColumnsForChange(coll, "Price_Rate8", obj.Price_Rate8)
                clsCommon.AddColumnsForChange(coll, "Price_Amount8", obj.Price_Amount8)

                clsCommon.AddColumnsForChange(coll, "Price_Comp9", obj.Price_Comp9)
                clsCommon.AddColumnsForChange(coll, "Price_Comp_Desc9", obj.Price_Comp_Desc9)
                clsCommon.AddColumnsForChange(coll, "Price_Rate9", obj.Price_Rate9)
                clsCommon.AddColumnsForChange(coll, "Price_Amount9", obj.Price_Amount9)

                clsCommon.AddColumnsForChange(coll, "Price_Comp10", obj.Price_Comp10)
                clsCommon.AddColumnsForChange(coll, "Price_Comp_Desc10", obj.Price_Comp_Desc10)
                clsCommon.AddColumnsForChange(coll, "Price_Rate10", obj.Price_Rate10)
                clsCommon.AddColumnsForChange(coll, "Price_Amount10", obj.Price_Amount10)

                clsCommon.AddColumnsForChange(coll, "Item_Rate", obj.Item_Rate)
                clsCommon.AddColumnsForChange(coll, "Liquid_Rate", obj.Liquid_Rate)
                clsCommon.AddColumnsForChange(coll, "Stock_Rate", obj.Stock_Rate)
                clsCommon.AddColumnsForChange(coll, "Abatement", obj.Abatement)
                clsCommon.AddColumnsForChange(coll, "Abatement_Rate", obj.Abatement_Rate)
                clsCommon.AddColumnsForChange(coll, "Price_Code_Desc", obj.Price_Code_Desc)
                clsCommon.AddColumnsForChange(coll, "Empty_Value_Bottle", obj.Empty_Value_Bottle)
                clsCommon.AddColumnsForChange(coll, "Item_Price_Id_No", clsPriceMaster.GetItem_Price_Id_No(trans))
                clsCommon.AddColumnsForChange(coll, "Tax_Group_Type", obj.Tax_Group_Type)

                'commnted by priti for inserting basic price same as selling price 
                ''by balwinder changed if wh add item selling price=item basic price then update will calulate wrong so in Item_Selling_Price=Item_Selling_Price and Item_Basic_Price=Item_Basic_Price
                clsCommon.AddColumnsForChange(coll, "Item_Selling_Price", obj.Item_Selling_Price)
                clsCommon.AddColumnsForChange(coll, "Item_Basic_Price", obj.Item_Basic_Price)
                'clsCommon.AddColumnsForChange(coll, "Item_Selling_Price", obj.Item_Basic_Net + obj.TAX1_Amt + obj.TAX2_Amt + obj.TAX3_Amt + obj.TAX4_Amt + obj.TAX5_Amt + obj.TAX6_Amt + obj.TAX7_Amt + obj.TAX8_Amt + obj.TAX9_Amt + obj.TAX10_Amt
                clsCommon.AddColumnsForChange(coll, "Is_Active", obj.Is_Active)
                clsCommon.AddColumnsForChange(coll, "Is_For_Price", obj.Is_For_Price)
                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modify_Date", obj.CurrentDate)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Basic_Net", obj.Item_Basic_Net)
                'clsCommon.AddColumnsForChange(coll, "Item_Basic_Price", obj.Item_Basic_Price)
                clsCommon.AddColumnsForChange(coll, "Type", obj.type)
                clsCommon.AddColumnsForChange(coll, "Against_Plan_TR_Code", obj.Against_Plan_TR_Code, True)
                clsCommon.AddColumnsForChange(coll, "Against_ItemwiseTaxCode", obj.Against_ItemwiseTaxCode, True)

                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "DCC") = CompairStringResult.Equal Then
                    Count = clsDBFuncationality.getSingleValue("Select Count(*) From TSPL_ITEM_PRICE_MASTER WHERE item_code='" + obj.Item_Code + "' and Uom='" + obj.UOM + "' and  Convert(Date,start_date,103)=Convert(date,'" + obj.Start_Date + "',103) and price_code='" + obj.Price_Code + "' and item_basic_net=" + clsCommon.myCstr(obj.Item_Basic_Net) + " and ROUND(Item_Basic_Price,3)=ROUND(" + clsCommon.myCstr(obj.Item_Basic_Price) + ",3)", trans)
                    If Count <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", obj.CurrentDate)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_PRICE_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_PRICE_MASTER", OMInsertOrUpdate.Update, "item_code='" + obj.Item_Code + "' and Uom='" + obj.UOM + "' and  start_date=Convert(date,'" + obj.Start_Date + "',103) and price_code='" + obj.Price_Code + "' and item_basic_net=" + clsCommon.myCstr(obj.Item_Basic_Net) + " and ROUND(Item_Basic_Price,3)=ROUND(" + clsCommon.myCstr(obj.Item_Basic_Price) + ",3)", trans)
                    End If
                    obj.Item_Price_ID = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select IDENT_CURRENT('TSPL_ITEM_PRICE_MASTER')", trans))
                Else
                    Dim Price_id As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Price_Id from TSPL_ITEM_PRICE_MASTER WHERE Location_Code='" & obj.Location_Code & "' AND Item_Code='" & obj.Item_Code & "' AND UOM='" & obj.UOM & "' AND Price_Code='" & obj.Price_Code & "' AND Item_MRP=" & clsCommon.myCstr(obj.Item_MRP) & " AND Start_Date='" & clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy") & "' AND Is_Active=1" + IIf(clsCommon.myLen(obj.Item_Price_ID) > 0, "AND Item_Price_Id<>" + clsCommon.myCstr(obj.Item_Price_ID) + "", ""), trans))
                    If clsCommon.myLen(Price_id) > 0 Then
                        Throw New Exception("Price '" & Price_id & "' is already exists with Item : " & obj.Item_Code & " - " & obj.UOM & ", Location : " & obj.Location_Code & ", Price_Code : " & obj.Price_Code & ", MRP : " + clsCommon.myCstr(obj.Item_MRP) + ", Start Date : " + clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy") + ",Type :  " + IIf(clsCommon.myCstr(obj.type) = "T", "Transfer", "Sale") + "" + Environment.NewLine + "Please make Inactive or Delete it.")
                    End If
                    If obj.IsNewEntry Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", obj.CurrentDate)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_PRICE_MASTER", OMInsertOrUpdate.Insert, "", trans)
                        obj.Item_Price_ID = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select IDENT_CURRENT('TSPL_ITEM_PRICE_MASTER')", trans))
                        If Count = 0 And IsWithBackCalculation Then
                            Stock_Price_Id = obj.Item_Price_ID
                        End If
                        If IsFromScreen Then
                            clsDBFuncationality.ExecuteNonQuery("Update TSPL_ITEM_PRICE_MASTER set Stock_Price_Id='" & Stock_Price_Id & "' Where Item_Price_Id='" & obj.Item_Price_ID & "'", trans)
                        Else
                            clsDBFuncationality.ExecuteNonQuery("Update TSPL_ITEM_PRICE_MASTER set Stock_Price_Id='" & obj.Item_Price_ID & "' Where Item_Price_Id='" & obj.Item_Price_ID & "'", trans)
                        End If
                    Else
                        If Count = 0 And IsWithBackCalculation Then
                            Stock_Price_Id = obj.Item_Price_ID
                        End If

                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Item_Price_ID, "TSPL_ITEM_PRICE_MASTER", "Item_Price_Id", trans)

                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_PRICE_MASTER", OMInsertOrUpdate.Update, "Item_Price_Id='" & obj.Item_Price_ID & "'", trans)
                    End If
                End If
                Count += 1
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Function GetItem_Price_Id_No(ByVal trans As SqlTransaction) As Integer
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT isnull(MAX(Item_Price_Id_No ),0)+1 FROM TSPL_ITEM_PRICE_MASTER", trans))
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Price no not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim qry As String = "Update TSPL_ITEM_PRICE_MASTER set Posted=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Item_Price_Id='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function UnPostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Price no not found to unpost")
            End If
            Dim qry As String = "select Posted from TSPL_ITEM_PRICE_MASTER where Item_Price_Id='" + strDocNo + "'"
            If clsDBFuncationality.getSingleValue(qry, trans) = 0 Then
                Throw New Exception("Already unposted price Id" + strDocNo)
            End If

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_ITEM_PRICE_MASTER", "Item_Price_Id", trans)
            qry = "Update TSPL_ITEM_PRICE_MASTER set Posted=0, Posted_Date=null,Posted_By=null where Item_Price_Id='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    '----------------------------------Deletes the Price Code Component................
    Public Shared Function DeleteData(ByVal strItemCode As String, ByVal strUni_Code As String, ByVal StartDate As String, ByVal strPriceCode As String, ByVal item_basic_net As Double, ByVal Item_Basic_Price As Double, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from tspl_item_price_master where item_code='" + strItemCode + "' and Uom='" + strUni_Code + "' and  start_date=Convert(date,'" + StartDate + "',103) and price_code='" + strPriceCode + "' and item_basic_net=" + clsCommon.myCstr(item_basic_net) + " and Item_Basic_Price=" + clsCommon.myCstr(Item_Basic_Price) + ""
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsPriceMaster
        Dim obj As clsPriceMaster = Nothing
        Dim qry As String = "SELECT TSPL_ITEM_PRICE_MASTER.*, TSPL_LOCATION_MASTER.Location_Desc,TSPL_PRICE_COMPONENT_MAPPING.Transfer as Transfer from TSPL_ITEM_PRICE_MASTER Left Outer Join TSPL_LOCATION_MASTER ON TSPL_ITEM_PRICE_MASTER.Location_Code=TSPL_LOCATION_MASTER.Location_Code left join TSPL_PRICE_COMPONENT_MAPPING on TSPL_PRICE_COMPONENT_MAPPING.Price_Code =TSPL_ITEM_PRICE_MASTER.Price_Code  WHERE 1=1"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Item_Price_ID = (select MIN(Item_Price_ID) from TSPL_ITEM_PRICE_MASTER)"
            Case NavigatorType.Last
                qry += " and Item_Price_ID = (select Max(Item_Price_ID) from TSPL_ITEM_PRICE_MASTER)"
            Case NavigatorType.Next
                qry += " and Item_Price_ID = (select Min(Item_Price_ID) from TSPL_ITEM_PRICE_MASTER where  Item_Price_ID>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and Item_Price_ID = (select Max(Item_Price_ID) from TSPL_ITEM_PRICE_MASTER where Item_Price_ID<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and Item_Price_ID = '" + strCode + "'"
        End Select
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsPriceMaster()
                obj.Item_Price_ID = clsCommon.myCstr(dt.Rows(0)("Item_Price_ID"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Item_Selling_Price = clsCommon.myCdbl(dt.Rows(0)("Item_Selling_Price"))
                obj.Item_MRP = clsCommon.myCdbl(dt.Rows(0)("Item_MRP"))
                If clsCommon.myLen(dt.Rows(0)("End_Date")) > 0 Then
                    obj.End_Date = dt.Rows(0)("End_Date")
                End If
                obj.Item_Basic_Price_Type = clsCommon.myCstr(dt.Rows(0)("Item_Basic_Price_Type"))
                obj.Markup_On = clsCommon.myCstr(dt.Rows(0)("Markup_On"))
                obj.Markup_Percent = clsCommon.myCdbl(dt.Rows(0)("Markup_Percent"))
                obj.Basic_Price_On = clsCommon.myCstr(dt.Rows(0)("Basic_Price_On"))
                obj.Landing_Cost = clsCommon.myCdbl(dt.Rows(0)("Landing_Cost"))
                obj.Purchase_Cost = clsCommon.myCdbl(dt.Rows(0)("Purchase_Cost"))
                obj.Can_Edit = clsCommon.myCstr(dt.Rows(0)("Can_Edit"))
                obj.NetLTPT = clsCommon.myCdbl(dt.Rows(0)("NetLTPT"))
                obj.Price_Category = clsCommon.myCstr(dt.Rows(0)("Price_Category"))
                obj.Is_With_Tax = clsCommon.myCstr(dt.Rows(0)("Is_With_Tax"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                obj.Start_Date = dt.Rows(0)("Start_Date")
                obj.Item_Basic_Net = clsCommon.myCdbl(dt.Rows(0)("Item_Basic_Net"))
                obj.Item_Basic_Price = clsCommon.myCdbl(dt.Rows(0)("Item_Basic_Price"))
                obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                obj.Tax_Manipulation_On = clsCommon.myCstr(dt.Rows(0)("Tax_Manipulation_On"))
                obj.Tax_group = clsCommon.myCstr(dt.Rows(0)("Tax_group"))
                obj.Against_ItemwiseTaxCode = clsCommon.myCstr(dt.Rows(0)("Against_ItemwiseTaxCode"))
                obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
                obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
                obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))

                obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
                obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
                obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))

                obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
                obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
                obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))

                obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
                obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
                obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))

                obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
                obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
                obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))

                obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("TAX6"))
                obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
                obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt"))

                obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("TAX7"))
                obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
                obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt"))

                obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("TAX8"))
                obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
                obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt"))

                obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("TAX9"))
                obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))
                obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt"))

                obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("TAX10"))
                obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
                obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt"))

                obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
                obj.Price_Code_Desc = clsCommon.myCstr(dt.Rows(0)("Price_Code_Desc"))

                obj.Price_Comp1 = clsCommon.myCstr(dt.Rows(0)("Price_Comp1"))
                obj.Price_Comp_Desc1 = clsCommon.myCstr(dt.Rows(0)("Price_Comp_Desc1"))
                obj.Price_Rate1 = clsCommon.myCdbl(dt.Rows(0)("Price_Rate1"))
                obj.Price_Amount1 = clsCommon.myCdbl(dt.Rows(0)("Price_Amount1"))

                obj.Price_Comp2 = clsCommon.myCstr(dt.Rows(0)("Price_Comp2"))
                obj.Price_Comp_Desc2 = clsCommon.myCstr(dt.Rows(0)("Price_Comp_Desc2"))
                obj.Price_Rate2 = clsCommon.myCdbl(dt.Rows(0)("Price_Rate2"))
                obj.Price_Amount2 = clsCommon.myCdbl(dt.Rows(0)("Price_Amount2"))

                obj.Price_Comp3 = clsCommon.myCstr(dt.Rows(0)("Price_Comp3"))
                obj.Price_Comp_Desc3 = clsCommon.myCstr(dt.Rows(0)("Price_Comp_Desc3"))
                obj.Price_Rate3 = clsCommon.myCdbl(dt.Rows(0)("Price_Rate3"))
                obj.Price_Amount3 = clsCommon.myCdbl(dt.Rows(0)("Price_Amount3"))

                obj.Price_Comp4 = clsCommon.myCstr(dt.Rows(0)("Price_Comp4"))
                obj.Price_Comp_Desc4 = clsCommon.myCstr(dt.Rows(0)("Price_Comp_Desc4"))
                obj.Price_Rate4 = clsCommon.myCdbl(dt.Rows(0)("Price_Rate4"))
                obj.Price_Amount4 = clsCommon.myCdbl(dt.Rows(0)("Price_Amount4"))

                obj.Price_Comp5 = clsCommon.myCstr(dt.Rows(0)("Price_Comp5"))
                obj.Price_Comp_Desc5 = clsCommon.myCstr(dt.Rows(0)("Price_Comp_Desc5"))
                obj.Price_Rate5 = clsCommon.myCdbl(dt.Rows(0)("Price_Rate5"))
                obj.Price_Amount5 = clsCommon.myCdbl(dt.Rows(0)("Price_Amount5"))

                obj.Price_Comp6 = clsCommon.myCstr(dt.Rows(0)("Price_Comp6"))
                obj.Price_Comp_Desc6 = clsCommon.myCstr(dt.Rows(0)("Price_Comp_Desc6"))
                obj.Price_Rate6 = clsCommon.myCdbl(dt.Rows(0)("Price_Rate6"))
                obj.Price_Amount6 = clsCommon.myCdbl(dt.Rows(0)("Price_Amount6"))

                obj.Price_Comp7 = clsCommon.myCstr(dt.Rows(0)("Price_Comp7"))
                obj.Price_Comp_Desc7 = clsCommon.myCstr(dt.Rows(0)("Price_Comp_Desc7"))
                obj.Price_Rate7 = clsCommon.myCdbl(dt.Rows(0)("Price_Rate7"))
                obj.Price_Amount7 = clsCommon.myCdbl(dt.Rows(0)("Price_Amount7"))

                obj.Price_Comp8 = clsCommon.myCstr(dt.Rows(0)("Price_Comp8"))
                obj.Price_Comp_Desc8 = clsCommon.myCstr(dt.Rows(0)("Price_Comp_Desc8"))
                obj.Price_Rate8 = clsCommon.myCdbl(dt.Rows(0)("Price_Rate8"))
                obj.Price_Amount8 = clsCommon.myCdbl(dt.Rows(0)("Price_Amount8"))

                obj.Price_Comp9 = clsCommon.myCstr(dt.Rows(0)("Price_Comp9"))
                obj.Price_Comp_Desc9 = clsCommon.myCstr(dt.Rows(0)("Price_Comp_Desc9"))
                obj.Price_Rate9 = clsCommon.myCdbl(dt.Rows(0)("Price_Rate9"))
                obj.Price_Amount9 = clsCommon.myCdbl(dt.Rows(0)("Price_Amount9"))

                obj.Price_Comp10 = clsCommon.myCstr(dt.Rows(0)("Price_Comp10"))
                obj.Price_Comp_Desc10 = clsCommon.myCstr(dt.Rows(0)("Price_Comp_Desc10"))
                obj.Price_Rate10 = clsCommon.myCdbl(dt.Rows(0)("Price_Rate10"))
                obj.Price_Amount10 = clsCommon.myCdbl(dt.Rows(0)("Price_Amount10"))

                obj.Item_Rate = clsCommon.myCdbl(dt.Rows(0)("Item_Rate"))
                obj.Liquid_Rate = clsCommon.myCdbl(dt.Rows(0)("Liquid_Rate"))
                obj.Stock_Rate = clsCommon.myCdbl(dt.Rows(0)("Stock_Rate"))
                obj.Abatement_Rate = clsCommon.myCdbl(dt.Rows(0)("Abatement_Rate"))
                obj.Abatement = clsCommon.myCdbl(dt.Rows(0)("Abatement"))
                obj.Item_Price_Id_No = clsCommon.myCstr(dt.Rows(0)("Item_Price_Id_No"))
                obj.Tax_Group_Type = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Type"))
                obj.Is_Active = clsCommon.myCdbl(dt.Rows(0)("Is_Active"))
                obj.type = clsCommon.myCdbl(dt.Rows(0)("Transfer"))
                obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
                obj.Against_Plan_TR_Code = clsCommon.myCstr(dt.Rows(0)("Against_Plan_TR_Code"))
            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetAllConversionOfItem(ByVal strItemCode As String) As DataTable
        Return GetAllConversionOfItem(strItemCode, Nothing)
    End Function
    Public Shared Function GetAllConversionOfItem(ByVal strItemCode As String, ByVal tran As SqlTransaction) As DataTable
        Try
            strItemCode = "Select XXX.Item1 as Item, XXX.UOM1, XXX.UOM2, XXX.CF1/XXX.CF2 as Conversion_Factor from (" & _
    " Select IUD1.Item_Code as Item1, IUD1.UOM_Code as UOM1, IUD1.Conversion_Factor as CF1, IUD1.Stocking_Unit as STKUOM1," & _
    " IUD2.Item_Code as Item2, IUD2.UOM_Code as UOM2, IUD2.Conversion_Factor as CF2, IUD2.Stocking_Unit as STKUOM2  from TSPL_ITEM_UOM_DETAIL IUD1" & _
    " Cross Join TSPL_ITEM_UOM_DETAIL IUD2 WHERE IUD1.Item_Code=IUD2.Item_Code AND IUD1.Item_Code='" & strItemCode & "'" & _
    " ) XXX"
            Return clsDBFuncationality.GetDataTable(strItemCode, tran)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsPriceComponent

#Region "Variables"

    Public Price_Comp_code As String
    Public Price_Comp_Desc As String
    Public Price_Comp_Account_Code As String
    Public GL_Account_Desc As String
    Public GL_Account_App As Char = "F"
    Public TPT_Type As Char = "N"
    Public Serial_Number As Integer
    Public CurrentDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
 
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Price_Comp_code as [Code] ,Price_Comp_Desc as [Price Component Description] ,Price_Comp_Account_Code as [Price Componet Account Code] ,GL_Account_App as [GL Account App] ,Created_By as [Created By] ,Created_Date as [Created Date] ,Modify_By as [Modify By] ,Modify_Date as [Modify Date] ,Comp_Code as [Company Code] ,TPT_Type as [TPT Type] ,Serial_Number as [Serial Number]  From TSPL_PRICE_COMPONENT_MASTER   "
        str = clsCommon.ShowSelectForm("PRCCMPMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function SaveData(ByVal arr As List(Of clsPriceComponent)) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            For Each obj As clsPriceComponent In arr
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_PRICE_COMPONENT_MASTER where Price_Comp_code='" & obj.Price_Comp_code & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.Price_Comp_code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.PriceComponantMaster, "", "")
                        If clsCommon.myLen(obj.Price_Comp_code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                Dim Count As Integer = 0
                '----------------Check for TPT Type-----------
                If clsCommon.CompairString(obj.TPT_Type, "Y") = CompairStringResult.Equal Then
                    Count = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_PRICE_COMPONENT_MASTER WHERE TPT_Type='Y' AND Price_Comp_code<>'" + obj.Price_Comp_code + "'", trans))
                    If Count > 0 Then
                        Throw New Exception("Only one Component Code can be of TPT Type, That already exist.")
                    End If
                End If
                '----------------Check for Serial No-----------
                Count = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_PRICE_COMPONENT_MASTER WHERE Serial_Number =" + clsCommon.myCstr(obj.Serial_Number) + " AND Price_Comp_code<>'" + obj.Price_Comp_code + "'", trans))
                If Count > 0 Then
                    Throw New Exception("The Serial No '" + clsCommon.myCstr(obj.Serial_Number) + "' has been already used.")
                End If
                If clsCommon.CompairString(obj.GL_Account_App, "F") = CompairStringResult.Equal Then
                    obj.Price_Comp_Account_Code = ""
                End If

                Count = clsDBFuncationality.getSingleValue("select Count(*) from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code='" + obj.Price_Comp_code + "'", trans)
                If Count <= 0 Then '--- New Entry [Insert]
                    isSaved = isSaved AndAlso clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_PRICE_COMPONANT_MASTER_INSERT", New SqlParameter("@Price_Comp_code", obj.Price_Comp_code), New SqlParameter("@Price_Comp_Desc", obj.Price_Comp_Desc), New SqlParameter("@Serial_Number", obj.Serial_Number), New SqlParameter("@Price_Comp_Account_Code", obj.Price_Comp_Account_Code), New SqlParameter("@GL_Account_App", obj.GL_Account_App), New SqlParameter("@TPT_Type", obj.TPT_Type), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", obj.CurrentDate), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", obj.CurrentDate), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))
                Else '----Old Entry [Update]
                    clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_PRICE_COMPONANT_MASTER_UPDATE", New SqlParameter("@Price_Comp_code", obj.Price_Comp_code), New SqlParameter("@Price_Comp_Desc", obj.Price_Comp_Desc), New SqlParameter("@Serial_Number", obj.Serial_Number), New SqlParameter("@Price_Comp_Account_Code", obj.Price_Comp_Account_Code), New SqlParameter("@GL_Account_App", obj.GL_Account_App), New SqlParameter("@TPT_Type", obj.TPT_Type), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", obj.CurrentDate), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))
                End If
                '--------Save Custom Fields................
                isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Price_Comp_code, obj.arrCustomFields, trans)
            Next
            trans.Commit()
        Catch ex As Exception
            Throw New Exception(ex.Message)
            trans.Rollback()
        End Try
        Return isSaved
    End Function

    '----------------------------------Deletes the Price Code Component................
    Public Shared Function DeleteData(ByVal strComponentCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim obj As clsPriceComponent
            If clsCommon.myLen(strComponentCode) > 0 Then
                obj = clsPriceComponent.GetData(strComponentCode, NavigatorType.Current)
                If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Price_Comp_code) > 0) Then
                    clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_PRICE_COMPONENT_MASTER_DELETE", New SqlParameter("@Price_Comp_code", strComponentCode))
                    clsCustomFieldValues.DeleteData(obj.Form_ID, obj.Price_Comp_code, trans)
                Else
                    Throw New Exception("Document not found to delete.")
                End If
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsPriceComponent
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsPriceComponent
        Dim obj As clsPriceComponent = Nothing
        Dim qry As String = "SELECT [Price_Comp_code], [Price_Comp_Desc],[GL_Account_App],[TPT_Type],[Serial_Number], [Price_Comp_Account_Code], TSPL_GL_ACCOUNTS.Description FROM TSPL_PRICE_COMPONENT_MASTER LEFT OUTER JOIN TSPL_GL_ACCOUNTS ON TSPL_GL_ACCOUNTS.Account_Code=TSPL_PRICE_COMPONENT_MASTER.Price_Comp_Account_Code Where 1=1"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Price_Comp_code = (select MIN(Price_Comp_code) from TSPL_PRICE_COMPONENT_MASTER)"
            Case NavigatorType.Last
                qry += " and Price_Comp_code = (select Max(Price_Comp_code) from TSPL_PRICE_COMPONENT_MASTER)"
            Case NavigatorType.Next
                qry += " and Price_Comp_code = (select Min(Price_Comp_code) from TSPL_PRICE_COMPONENT_MASTER where  Price_Comp_code>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and Price_Comp_code = (select Max(Price_Comp_code) from TSPL_PRICE_COMPONENT_MASTER where Price_Comp_code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and Price_Comp_code = '" + strCode + "'"
        End Select
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsPriceComponent()
                obj.Price_Comp_code = clsCommon.myCstr(dt.Rows(0)("Price_Comp_code"))
                obj.Price_Comp_Desc = clsCommon.myCstr(dt.Rows(0)("Price_Comp_Desc"))
                obj.GL_Account_App = clsCommon.myCstr(dt.Rows(0)("GL_Account_App"))
                obj.Price_Comp_Account_Code = clsCommon.myCstr(dt.Rows(0)("Price_Comp_Account_Code"))
                obj.GL_Account_Desc = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.TPT_Type = clsCommon.myCstr(dt.Rows(0)("TPT_Type"))
                obj.Serial_Number = clsCommon.myCdbl(dt.Rows(0)("Serial_Number"))

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Return GetName(strCode, Nothing)
    End Function
    Public Shared Function GetName(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select Price_Comp_Desc from TSPL_PRICE_COMPONENT_MASTER where Price_Comp_code='" + strCode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function GetMethod(ByVal strPriceCode As String, ByVal strPriceComponentCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select Price_Calculation_Method from TSPL_PRICE_COMPONENT_MAPPING where Price_Code='" + strPriceCode + "' and Price_Comp_Code='" + strPriceComponentCode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function GetAllPriceComponent() As List(Of clsPriceComponent)
        Dim Arr As List(Of clsPriceComponent) = Nothing
        Dim qry As String = "select Price_Comp_code,Price_Comp_Desc from TSPL_PRICE_COMPONENT_MASTER order by Serial_Number"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Arr = New List(Of clsPriceComponent)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsPriceComponent
                obj.Price_Comp_code = clsCommon.myCstr(dr("Price_Comp_code"))
                obj.Price_Comp_Desc = clsCommon.myCstr(dr("Price_Comp_Desc"))
                Arr.Add(obj)
            Next
        End If
        Return Arr
    End Function

    Public Shared Function getGLAccountCode(ByVal strPriceCompCodeCode As String, ByVal trans As SqlTransaction) As String
        Try
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Price_Comp_Account_Code from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_Code='" + strPriceCompCodeCode + "'", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsPriceComponentMapping

#Region "Variables"
    Public principlecode As String = Nothing
    Public Price_Code As String
    Public Price_Code_Desc As String
    Public Remarks As String
    Public Price_Comp_Code As String
    Public Price_Comp_Desc As String
    Public Amount As Double
    Public Discount_Percent As Double
    Public Price_Calculation_Method As String
    Public CurrentDate As Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    '============added by preeti gupta=================
    Public Transfer As Decimal = 0
    '==================================================
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Price_Component_Map_Code as [Price Component Map Code] ,Price_Code as [Code] ,Price_Code_Desc as [Price Code Description] ,Remarks as [Remarks] ,Price_Comp_Code as [Price Component Code] ,Price_Comp_Desc as [Price Comp Description] ,Amount as [Amount] ,Discount_Percent as [Discount Percent] ,Created_By as [Created By] ,Created_Date as [Created Date] ,Modify_By as [Modify By] ,Modify_Date as [Modify Date] ,Comp_Code as [Company Code] ,Price_Calculation_Method as [Price Calculation Method] ,TSPL_PRICE_COMPONENT_MAPPING.Transfer  From TSPL_PRICE_COMPONENT_MAPPING   "
        str = clsCommon.ShowSelectForm("PRCCMPMAP", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'

    Public Shared Function SaveData(ByVal strPriceCode As String, ByVal arr As List(Of clsPriceComponentMapping)) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            'Price_Code, Price_Code_Desc, Remarks, Price_Comp_Code, Price_Comp_Desc, Amount, Discount_Percent, Price_Calculation_Method
            'Dim Del As New BAL.BALPriceComponant
            'Del.DeletePCMMaster(strPriceCode)
            Dim qry As String = "delete from TSPL_PRICE_COMPONENT_MAPPING where Price_code = '" + strPriceCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As clsPriceComponentMapping In arr
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_PRICE_COMPONENT_MAPPING where Price_Code='" & obj.Price_Code & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.Price_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.PriceComponantMapping, "", "")
                        If clsCommon.myLen(obj.Price_Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                isSaved = isSaved AndAlso clsDBFuncationality.SaveAStorePorcedure(trans, "InsertPCMMaster", New SqlParameter("@PriceCode", obj.Price_Code), New SqlParameter("@PriceDesc", obj.Price_Code_Desc), New SqlParameter("@Remarks", obj.Remarks), New SqlParameter("@PCMCode", obj.Price_Comp_Code), New SqlParameter("@PCMDesc", obj.Price_Comp_Desc), New SqlParameter("@Amount", obj.Amount), New SqlParameter("@Type", obj.Price_Calculation_Method), New SqlParameter("@CreatedBy", objCommonVar.CurrentUserCode), New SqlParameter("@CreatedDate", obj.CurrentDate), New SqlParameter("@Company", objCommonVar.CurrentCompanyCode), New SqlParameter("@price_calculation_method", obj.Price_Calculation_Method), New SqlParameter("@vendorcode", obj.principlecode))
                'Dim coll As New Hashtable()
                'clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                'clsCommon.AddColumnsForChange(coll, "Transfer", obj.Transfer)
                'clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_COMPONENT_MAPPING", OMInsertOrUpdate.Update, "price_code='" & obj.Price_Code & "'", trans)
                'clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_COMPONENT_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                clsDBFuncationality.ExecuteNonQuery("update TSPL_PRICE_COMPONENT_MAPPING set Transfer  =" & obj.Transfer & " where Price_Code ='" & obj.Price_Code & "'", trans)
            Next
            trans.Commit()
        Catch ex As Exception
            Throw New Exception(ex.Message)
            trans.Rollback()
        End Try
        Return isSaved
    End Function

    ''----------------------------------Deletes the Price Code Component................
    Public Shared Function DeleteData(ByVal strPriceCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim obj As clsPriceComponentMapping = Nothing
            If clsCommon.myLen(strPriceCode) > 0 Then
                Dim count As Integer = clsDBFuncationality.getSingleValue("select count(*) from tspl_item_price_master where Price_Code ='" + strPriceCode + "'")
                'obj = clsPriceComponentMapping.GetData(strPriceCode, NavigatorType.Current)
                'If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Price_Code) > 0) Then
                'Dim Del As New BAL.BALPriceComponant
                'Del.DeletePCMMaster(strPriceCode)
                If count = 0 Then
                    Dim qry As String = "delete from TSPL_PRICE_COMPONENT_MAPPING where Price_code = '" + strPriceCode + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Else
                    Throw New Exception("Record already used")
                End If
            End If
               
                'Else
                '    Throw New Exception("Document not found to delete.")
                'End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As List(Of clsPriceComponentMapping)
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As List(Of clsPriceComponentMapping)
        Dim obj As clsPriceComponentMapping = Nothing
        Dim qry As String = "SELECT distinct [Price_code] as [Price Code] ,[Price_Code_Desc] as [Description], Remarks,vendor_code,Transfer FROM [TSPL_PRICE_COMPONENT_MAPPING] where 2=2"
        Select Case NavType
            Case NavigatorType.Current
                qry += " and TSPL_PRICE_COMPONENT_MAPPING.Price_code in ('" + strCode + "')"
            Case NavigatorType.Next
                qry += " and TSPL_PRICE_COMPONENT_MAPPING .Price_code in (select min(Price_code ) from TSPL_PRICE_COMPONENT_MAPPING where Price_code  >'" + strCode + "')"
            Case NavigatorType.First
                qry += " and TSPL_PRICE_COMPONENT_MAPPING .Price_code in (select MIN(Price_code ) from TSPL_PRICE_COMPONENT_MAPPING)"
            Case NavigatorType.Last
                qry += " and TSPL_PRICE_COMPONENT_MAPPING .Price_code in (select Max(Price_code ) from TSPL_PRICE_COMPONENT_MAPPING)"
            Case NavigatorType.Previous
                qry += " and TSPL_PRICE_COMPONENT_MAPPING .Price_code in (select Max(Price_code ) from TSPL_PRICE_COMPONENT_MAPPING where Price_code  <'" + strCode + "')"
        End Select
        Dim arr As New List(Of clsPriceComponentMapping)
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                strCode = clsCommon.myCstr(dt.Rows(0)("Price Code"))
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select TSPL_PRICE_COMPONENT_MASTER.Price_Comp_code, TSPL_PRICE_COMPONENT_MASTER.Price_Comp_Desc, XXX.Price_Calculation_Method, XXX.Amount,XXX.vendor_code from TSPL_PRICE_COMPONENT_MASTER LEFT OUTER JOIN (Select Price_Comp_Code, Price_Calculation_Method, Case When Price_Calculation_Method='Amount' Then Amount Else Discount_Percent End as [Amount],TSPL_PRICE_COMPONENT_MAPPING.vendor_code from TSPL_PRICE_COMPONENT_MAPPING WHERE TSPL_PRICE_COMPONENT_MAPPING.Price_Code='" + strCode + "') XXX ON XXX.Price_Comp_Code=TSPL_PRICE_COMPONENT_MASTER.Price_Comp_code Order BY Serial_Number")
                For Each dr As DataRow In dt1.Rows
                    obj = New clsPriceComponentMapping()
                    obj.principlecode = clsCommon.myCstr(dt.Rows(0)("vendor_code"))

                    obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price Code"))
                    obj.Price_Code_Desc = clsCommon.myCstr(dt.Rows(0)("Description"))
                    obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))

                    obj.Price_Comp_Code = clsCommon.myCstr(dr("Price_Comp_Code"))
                    obj.Price_Comp_Desc = clsCommon.myCstr(dr("Price_Comp_Desc"))
                    obj.Price_Calculation_Method = clsCommon.myCstr(dr("Price_Calculation_Method"))
                    obj.Amount = clsCommon.myCdbl(dr("Amount"))
                    obj.Transfer = clsCommon.myCdbl(dt.Rows(0)("Transfer"))
                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetName(ByVal curcode As String, ByVal tran As SqlTransaction) As String
        Dim qry As String = "Select max(Price_Code_Desc) as Price_Code_Desc from TSPL_PRICE_COMPONENT_MAPPING  where Price_Code='" + curcode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, tran))
    End Function
End Class

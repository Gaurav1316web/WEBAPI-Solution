Imports common
Imports System.Data.SqlClient
Public Class clsMccMilkTransferPrice

#Region "variables"
    Public Price_Code As String = Nothing
    Public Price_Date As String = Nothing
    Public Cost_Calc_From_Date As String = Nothing
    Public Cost_Calc_To_Date As String = Nothing
    Public Transfer_Price_From_Date As String = Nothing
    Public Transfer_Price_To_Date As String = Nothing
    Public MCC_Code As String = Nothing
    Public Mcc_Name As String = Nothing
    Public Milk_Cost As Double = 0
    Public Primary_Transporter_Cost As Double = 0
    Public Chilling_Charge As Double = 0
    Public Mcc_Rent As Double = 0
    Public VSP_Charge As Double = 0
    Public Manpower_Cost As Double = 0
    Public Admin_Cost As Double = 0
    Public Procurement_Cost As Double = 0
    Public Secondary_Transporter_Cost As Double = 0
    Public Head_Cost_Per As Double = 0
    Public Head_Cost As Double = 0
    Public Total_Cost As Double = 0
    Public isPosted As Integer
    Public Posting_Date As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As String = Nothing
    Public Comp_Code As String = Nothing
    Public isNewEntry As Boolean = False
#End Region

    Public Shared Function getTransferPrice(ByVal mccCode As String, ByVal dt As Date) As Double
        Dim transPrice As Double = 0
        Dim strQry As String = "select total_cost from TSPL_MCC_MILK_TRANSFER_PRICE where '" & clsCommon.GetPrintDate(dt, "dd/MMM/yyyy") & "' between convert(date,TSPL_MCC_MILK_TRANSFER_PRICE.transfer_price_from_date,103)  and convert(date,TSPL_MCC_MILK_TRANSFER_PRICE.transfer_price_to_date,103) and MCC_Code='" & mccCode & "'  "
        transPrice = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strQry))
        Return transPrice
    End Function

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_MCC_MILK_TRANSFER_PRICE.Price_Code as [PriceCode] ,TSPL_MCC_MILK_TRANSFER_PRICE.Price_Date as [Price Date] ,TSPL_MCC_MILK_TRANSFER_PRICE.Cost_Calc_From_Date as [Cost Calculation From Date] ,TSPL_MCC_MILK_TRANSFER_PRICE.Cost_Calc_To_Date as [Cost Calculation To Date] ,TSPL_MCC_MILK_TRANSFER_PRICE.Transfer_Price_From_Date as [Transfer Price From Date] ,TSPL_MCC_MILK_TRANSFER_PRICE.Transfer_Price_To_Date as [Transfer Price To Date] ,TSPL_MCC_MILK_TRANSFER_PRICE.MCC_Code as [MCC Code] ,TSPL_MCC_MILK_TRANSFER_PRICE.Mcc_Name as [MCC Name] ,TSPL_MCC_MILK_TRANSFER_PRICE.Milk_Cost as [Milk Cost] ,TSPL_MCC_MILK_TRANSFER_PRICE.Primary_Transporter_Cost as [Primary Transporter Cost] ,TSPL_MCC_MILK_TRANSFER_PRICE.Chilling_Charge as [Chilling Charge] ,TSPL_MCC_MILK_TRANSFER_PRICE.Mcc_Rent as [MCC Rent] ,TSPL_MCC_MILK_TRANSFER_PRICE.VSP_Charge as [VSP Charge] ,TSPL_MCC_MILK_TRANSFER_PRICE.Manpower_Cost as [Manpower Cost] ,TSPL_MCC_MILK_TRANSFER_PRICE.Admin_Cost as [Admin Cost] ,TSPL_MCC_MILK_TRANSFER_PRICE.Procurement_Cost as [Procurement Cost] ,TSPL_MCC_MILK_TRANSFER_PRICE.Secondary_Transporter_Cost as [Secondary Transporter Cost] ,TSPL_MCC_MILK_TRANSFER_PRICE.Head_Cost_Per as [Head Cost (%)] ,TSPL_MCC_MILK_TRANSFER_PRICE.Head_Cost as [Head Cost] ,TSPL_MCC_MILK_TRANSFER_PRICE.Total_Cost as [Total Cost] ,case when isnull(TSPL_MCC_MILK_TRANSFER_PRICE.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,TSPL_MCC_MILK_TRANSFER_PRICE.Posting_Date as [Posting Date] ,TSPL_MCC_MILK_TRANSFER_PRICE.Created_By as [Created By] ,TSPL_MCC_MILK_TRANSFER_PRICE.Created_Date as [Created Date] ,TSPL_MCC_MILK_TRANSFER_PRICE.Modified_By as [Modified By] ,TSPL_MCC_MILK_TRANSFER_PRICE.Modified_Date as [Modified Date] ,TSPL_MCC_MILK_TRANSFER_PRICE.Comp_Code as [Company Code]  From TSPL_MCC_MILK_TRANSFER_PRICE   "
        str = clsCommon.ShowSelectForm("MccTranPr", qry, "PriceCode", whrcls, curcode, "PriceCode", isButtonClicked, "Price_Date")
        Return str
    End Function



    Public Shared Function SaveData(ByVal obj As clsMccMilkTransferPrice, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""

        Try
            If isNewEntry Then
                obj.Price_Code = clsERPFuncationality.GetNextCode(trans, obj.Price_Date, clsDocType.MccMilkTransferPrice, "", obj.MCC_Code)
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.MccMilkTransferPrice, obj.MCC_Code, clsCommon.myCDate(obj.Price_Date), trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Cost_Calc_From_Date", clsCommon.GetPrintDate(obj.Cost_Calc_From_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Cost_Calc_To_Date", clsCommon.GetPrintDate(obj.Cost_Calc_To_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Transfer_Price_From_Date", clsCommon.GetPrintDate(obj.Transfer_Price_From_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Transfer_Price_To_Date", clsCommon.GetPrintDate(obj.Transfer_Price_To_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code)
            clsCommon.AddColumnsForChange(coll, "Mcc_Name", obj.Mcc_Name)
            clsCommon.AddColumnsForChange(coll, "Milk_Cost", obj.Milk_Cost)
            clsCommon.AddColumnsForChange(coll, "Primary_Transporter_Cost", obj.Primary_Transporter_Cost)
            clsCommon.AddColumnsForChange(coll, "Chilling_Charge", obj.Chilling_Charge)
            clsCommon.AddColumnsForChange(coll, "Mcc_Rent", obj.Mcc_Rent)
            clsCommon.AddColumnsForChange(coll, "VSP_Charge", obj.VSP_Charge)
            clsCommon.AddColumnsForChange(coll, "Manpower_Cost", obj.Manpower_Cost)
            clsCommon.AddColumnsForChange(coll, "Admin_Cost", obj.Admin_Cost)
            clsCommon.AddColumnsForChange(coll, "Procurement_Cost", obj.Procurement_Cost)
            clsCommon.AddColumnsForChange(coll, "Secondary_Transporter_Cost", obj.Secondary_Transporter_Cost)
            clsCommon.AddColumnsForChange(coll, "Head_Cost_Per", obj.Head_Cost_Per)
            clsCommon.AddColumnsForChange(coll, "Head_Cost", obj.Head_Cost)
            clsCommon.AddColumnsForChange(coll, "Total_Cost", obj.Total_Cost)
            clsCommon.AddColumnsForChange(coll, "isPosted", obj.isPosted)

            If obj.isPosted = 1 Then
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_MILK_TRANSFER_PRICE", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_MILK_TRANSFER_PRICE", OMInsertOrUpdate.Update, "TSPL_MCC_MILK_TRANSFER_PRICE.Price_Code='" + obj.Price_Code + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
            Return False
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMccMilkTransferPrice
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMccMilkTransferPrice
        Dim obj As clsMccMilkTransferPrice = Nothing
        Dim qry As String = "select * from TSPL_MCC_MILK_TRANSFER_PRICE where 2=2 "

        Dim whrclas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrclas = " and Mcc_code in(" & objCommonVar.strCurrUserLocations & ") "
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MCC_MILK_TRANSFER_PRICE.Price_Code = (select MIN(Price_Code) from TSPL_MCC_MILK_TRANSFER_PRICE WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_MCC_MILK_TRANSFER_PRICE.Price_Code = (select Max(Price_Code) from TSPL_MCC_MILK_TRANSFER_PRICE WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_MCC_MILK_TRANSFER_PRICE.Price_Code = (select top 1 Price_Code from TSPL_MCC_MILK_TRANSFER_PRICE WHERE 1=1 " + whrclas + " and Price_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_MCC_MILK_TRANSFER_PRICE.Price_Code = (select Min(Price_Code) from TSPL_MCC_MILK_TRANSFER_PRICE where Price_Code>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_MCC_MILK_TRANSFER_PRICE.Price_Code = (select Max(Price_Code) from TSPL_MCC_MILK_TRANSFER_PRICE where Price_Code<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMccMilkTransferPrice
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.Price_Date = clsCommon.myCstr(dt.Rows(0)("Price_Date"))

            obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
            obj.Mcc_Name = clsCommon.myCstr(dt.Rows(0)("Mcc_Name"))
            obj.Cost_Calc_From_Date = clsCommon.myCstr(dt.Rows(0)("Cost_Calc_From_Date"))
            obj.Cost_Calc_To_Date = clsCommon.myCstr(dt.Rows(0)("Cost_Calc_To_Date"))
            obj.Transfer_Price_From_Date = clsCommon.myCstr(dt.Rows(0)("Transfer_Price_From_Date"))
            obj.Transfer_Price_To_Date = clsCommon.myCstr(dt.Rows(0)("Transfer_Price_To_Date"))

            obj.Milk_Cost = clsCommon.myCdbl(dt.Rows(0)("Milk_Cost"))
            obj.Primary_Transporter_Cost = clsCommon.myCdbl(dt.Rows(0)("Primary_Transporter_Cost"))
            obj.Chilling_Charge = clsCommon.myCdbl(dt.Rows(0)("Chilling_Charge"))
            obj.Mcc_Rent = clsCommon.myCdbl(dt.Rows(0)("Mcc_Rent"))
            obj.VSP_Charge = clsCommon.myCdbl(dt.Rows(0)("VSP_Charge"))

            obj.Manpower_Cost = clsCommon.myCdbl(dt.Rows(0)("Manpower_Cost"))
            obj.Admin_Cost = clsCommon.myCdbl(dt.Rows(0)("Admin_Cost"))
            obj.Procurement_Cost = clsCommon.myCdbl(dt.Rows(0)("Procurement_Cost"))
            obj.Secondary_Transporter_Cost = clsCommon.myCdbl(dt.Rows(0)("Secondary_Transporter_Cost"))
            obj.Head_Cost_Per = clsCommon.myCdbl(dt.Rows(0)("Head_Cost_Per"))
            obj.Head_Cost = clsCommon.myCdbl(dt.Rows(0)("Head_Cost"))
            obj.Total_Cost = clsCommon.myCdbl(dt.Rows(0)("total_cost"))
            obj.isPosted = clsCommon.myCdbl(dt.Rows(0)("isPosted"))
            If obj.isPosted = 1 Then
                obj.Posting_Date = clsCommon.GetPrintDate(dt.Rows(0)("posting_date"), "dd/MMM/yyyy")
            End If

        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Price Code not found to Delete")
        End If
        Try
            Dim qry As String = "delete from TSPL_MCC_MILK_TRANSFER_PRICE where Price_Code='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function

End Class

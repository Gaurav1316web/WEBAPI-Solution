'ClsBulkSalePriceChart'--------Created By Richa 24/11/2014 Against Ticket No 
Imports common
Imports System.Data.SqlClient
Public Class ClsBulkSaleReturn

#Region "Variable"
    Public Document_No As String = Nothing
    Public Document_Date As Date
    Public Customer_Code As String = Nothing
    Public Location_Code As String = Nothing
    Public Silo_No As String = Nothing
    Public Silo_Name As String = Nothing
    Public Customer_Name As String = Nothing
    Public Location_Name As String = Nothing
    Public InvoiceNo As String = Nothing
    Public Total_Amt As Double = 0
    Public RoundOffAmount As Double = 0
    Public Posted As Integer = 0
    Public Posting_Date As Date?
    Public Tanker_No As String = Nothing
    Public GateEntryNo As String = Nothing
    Public DispatchNo As String = Nothing
    Public Against As String = Nothing
    Public IsRejected As Integer = 0
    Public DispatchLocation As String = Nothing
    Public DispatchLocationName As String = Nothing
    Public Shared aLoc As String = Nothing
    Public arrSaleReturnDetailBulkSale As List(Of ClsSaleReturnDetailBulkSale) = Nothing
    Public Is_Cancelled As Integer = 0
    Public ActualTCSBaseAmount As Double = 0
    Public ChangedTCSBaseAmount As Double = 0
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing 'Not a table field
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = 0
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = 0
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = 0
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = 0
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = 0
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Amt As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Document_Amount As Double = 0

#End Region


    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "Select TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No as Code ,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date as Date from TSPL_SALE_RETURN_MASTER_BULKSALE  "
        str = clsCommon.ShowSelectForm("BulkSaleReturn", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function SaveData(ByVal obj As ClsBulkSaleReturn, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As ClsBulkSaleReturn, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Dim isSaved As Boolean = True
        Try
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_SALE_RETURN_MASTER_BULKSALE", "Document_No", "TSPL_SALE_RETURN_DETAIL_BULKSALE", "Document_No", trans)
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmBulkSaleReturn, obj.Location_Code, obj.Document_Date, trans)
            qry = "delete from TSPL_SALE_RETURN_DETAIL_BULKSALE where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                If clsCommon.CompairString(obj.Against, "Bulk Invoice") = CompairStringResult.Equal Then
                    If obj.Is_Cancelled = 1 Then
                        obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.BulkSaleReturn, clsDocTransactionType.SaleReturnCancel, obj.Location_Code)
                    Else
                        obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.BulkSaleReturn, clsDocTransactionType.NA, obj.Location_Code)
                    End If
                Else
                    obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.BulkSaleReturnDispatch, "", obj.Location_Code)
                End If

            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Silo_No", obj.Silo_No, True)
            clsCommon.AddColumnsForChange(coll, "InvoiceNo", obj.InvoiceNo, True)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)
            clsCommon.AddColumnsForChange(coll, "GateEntryNo", obj.GateEntryNo)
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "RoundOffAmount", obj.RoundOffAmount)
            clsCommon.AddColumnsForChange(coll, "DispatchNo", obj.DispatchNo, True)
            clsCommon.AddColumnsForChange(coll, "Against", obj.Against)
            clsCommon.AddColumnsForChange(coll, "IsRejected", obj.IsRejected)
            clsCommon.AddColumnsForChange(coll, "DispatchLocation", obj.DispatchLocation)
            clsCommon.AddColumnsForChange(coll, "DispatchLocationName", obj.DispatchLocationName)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Is_Cancelled", obj.Is_Cancelled)
            clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)
            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
            clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
            clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
            clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
            clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
            clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Document_Amount", obj.Document_Amount)
            clsCommon.AddColumnsForChange(coll, "ActualTCSBaseAmount", obj.ActualTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "ChangedTCSBaseAmount", obj.ChangedTCSBaseAmount)

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALE_RETURN_MASTER_BULKSALE", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALE_RETURN_MASTER_BULKSALE", OMInsertOrUpdate.Update, "TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No='" + obj.Document_No + "'", trans)
            End If
            isSaved = isSaved AndAlso ClsSaleReturnDetailBulkSale.saveData(obj.arrSaleReturnDetailBulkSale, obj.Document_No, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As ClsBulkSaleReturn
        Return GetData(strCode, arrLoc, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsBulkSaleReturn
        ''richa agarwal 14/10/2014
        ''Dim strfLocation As String = ""
        ''Dim strvirlocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select distinct ','+''+Location_Code  from TSPL_INVOICE_MASTER_BULKSALE for xml path ('')), 1,1,'') as Test from TSPL_INVOICE_MASTER_BULKSALE where InvoiceAgainst ='Against Dispatch Trade' group by InvoiceAgainst", trans))

        '' ''===================================

        ''If clsCommon.myLen(arrLoc) <= 0 Then
        ''    If clsCommon.myLen(FrmInvoiceBulkSale.aLoc) > 0 Then
        ''        arrLoc = FrmInvoiceBulkSale.aLoc
        ''        If clsCommon.myLen(strvirlocation) > 0 Then
        ''            strvirlocation = strvirlocation.Replace(",", "','")
        ''            If clsCommon.myLen(arrLoc) > 0 Then
        ''                strfLocation = arrLoc + ",'" + strvirlocation + "'"
        ''                arrLoc = strfLocation
        ''            Else
        ''                strfLocation = "'" + strvirlocation + "'"
        ''                arrLoc = strfLocation
        ''            End If
        ''        End If

        ''    ElseIf clsCommon.myLen(FrmDispatchBulkSale.Alocation) > 0 Then
        ''        arrLoc = FrmDispatchBulkSale.Alocation
        ''        If clsCommon.myLen(strvirlocation) > 0 Then
        ''            strvirlocation = strvirlocation.Replace(",", "','")
        ''            If clsCommon.myLen(arrLoc) > 0 Then
        ''                strfLocation = arrLoc + ",'" + strvirlocation + "'"
        ''                arrLoc = strfLocation
        ''            Else
        ''                strfLocation = "'" + strvirlocation + "'"
        ''                arrLoc = strfLocation
        ''            End If
        ''        End If
        ''        ''richa 18/11/2014
        ''    Else
        ''        arrLoc = clsDBFuncationality.getSingleValue("Select Location_Code from TSPL_INVOICE_MASTER_BULKSALE where Document_No='" & strCode & "'", trans)
        ''        arrLoc = "'" + arrLoc + "'"
        ''        If clsCommon.myLen(strvirlocation) > 0 Then
        ''            strvirlocation = strvirlocation.Replace(",", "','")
        ''            If clsCommon.myLen(arrLoc) > 0 Then
        ''                strfLocation = arrLoc + ",'" + strvirlocation + "'"
        ''                arrLoc = strfLocation
        ''            Else
        ''                strfLocation = "'" + strvirlocation + "'"
        ''                arrLoc = strfLocation
        ''            End If
        ''        End If
        ''    End If
        ''End If

        ' ''If clsCommon.myLen(arrLoc) <= 0 Then
        ' ''    If clsCommon.myLen(FrmInvoiceBulkSale.aLoc) > 0 Then
        ' ''        arrLoc = FrmInvoiceBulkSale.aLoc
        ' ''    ElseIf clsCommon.myLen(FrmDispatchBulkSale.Alocation) > 0 Then
        ' ''        arrLoc = FrmDispatchBulkSale.Alocation
        ' ''    End If
        'End If
        Dim obj As ClsBulkSaleReturn = Nothing
        Dim Arr As List(Of ClsBulkSaleReturn) = Nothing
        Dim qry As String = "Select TSPL_SALE_RETURN_MASTER_BULKSALE.DispatchLocationName,TSPL_SALE_RETURN_MASTER_BULKSALE.DispatchLocation,TSPL_SALE_RETURN_MASTER_BULKSALE.IsRejected,TSPL_SALE_RETURN_MASTER_BULKSALE.Against,TSPL_SALE_RETURN_MASTER_BULKSALE.DispatchNo,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No,TSPL_SALE_RETURN_MASTER_BULKSALE.Tanker_No,TSPL_SALE_RETURN_MASTER_BULKSALE.GateEntryNo,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date,TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code,TSPL_SALE_RETURN_MASTER_BULKSALE.Silo_No,TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code,TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,TSPL_SALE_RETURN_MASTER_BULKSALE.Posted,TSPL_SALE_RETURN_MASTER_BULKSALE.RoundOffAmount,TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo,TSPL_LOCATION_MASTER.Location_Desc,SubLocation.Location_Desc as Silo_Name,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SALE_RETURN_MASTER_BULKSALE.Is_Cancelled,TSPL_SALE_RETURN_MASTER_BULKSALE.ChangedTCSBaseAmount,TSPL_SALE_RETURN_MASTER_BULKSALE.ActualTCSBaseAmount,TSPL_SALE_RETURN_MASTER_BULKSALE.Tax_Group,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX1,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX1_Rate,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX1_Amt,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX1_Base_Amt,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX2,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX2_Rate,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX2_Amt,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX2_Base_Amt,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX3,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX3_Rate,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX3_Amt,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX3_Base_Amt,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX4,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX4_Rate,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX4_Amt,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX4_Base_Amt,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX5,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX5_Rate,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX5_Amt,TSPL_SALE_RETURN_MASTER_BULKSALE.TAX5_Base_Amt,TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Tax_Amt,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Amount,TSPL_SALE_RETURN_MASTER_BULKSALE.Tax_Calculation_Type from TSPL_SALE_RETURN_MASTER_BULKSALE Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code Left Outer Join TSPL_LOCATION_MASTER as SubLocation on SubLocation.Location_Code=TSPL_SALE_RETURN_MASTER_BULKSALE.Silo_No Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No = (select MIN(Document_No) from TSPL_SALE_RETURN_MASTER_BULKSALE WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No = (select Max(Document_No) from TSPL_SALE_RETURN_MASTER_BULKSALE WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No ='" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No = (select Min(Document_No) from TSPL_SALE_RETURN_MASTER_BULKSALE where Document_No>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No = (select Max(Document_No) from TSPL_SALE_RETURN_MASTER_BULKSALE where Document_No<'" + strCode + "' " + whrclas + " )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsBulkSaleReturn()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            obj.GateEntryNo = clsCommon.myCstr(dt.Rows(0)("GateEntryNo"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Silo_No = clsCommon.myCstr(dt.Rows(0)("Silo_No"))
            obj.Silo_Name = clsCommon.myCstr(dt.Rows(0)("Silo_Name"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.RoundOffAmount = clsCommon.myCdbl(dt.Rows(0)("RoundOffAmount"))
            obj.InvoiceNo = clsCommon.myCstr(dt.Rows(0)("InvoiceNo"))
            obj.DispatchNo = clsCommon.myCstr(dt.Rows(0)("DispatchNo"))
            obj.IsRejected = clsCommon.myCdbl(dt.Rows(0)("IsRejected"))
            obj.Against = clsCommon.myCstr(dt.Rows(0)("Against"))
            obj.DispatchLocation = clsCommon.myCstr(dt.Rows(0)("DispatchLocation"))
            obj.DispatchLocationName = clsCommon.myCstr(dt.Rows(0)("DispatchLocationName"))
            obj.Is_Cancelled = clsCommon.myCdbl(dt.Rows(0)("Is_Cancelled"))
            obj.ChangedTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ChangedTCSBaseAmount"))
            obj.ActualTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ActualTCSBaseAmount"))
            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
            obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
            obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Base_Amt"))
            obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
            obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
            obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
            obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Base_Amt"))
            obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
            obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
            obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Base_Amt"))
            obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
            obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
            obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
            obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
            obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Base_Amt"))
            obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
            obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
            obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
            obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Base_Amt"))
            obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
            obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
            obj.Document_Amount = clsCommon.myCdbl(dt.Rows(0)("Document_Amount"))

            obj.arrSaleReturnDetailBulkSale = ClsSaleReturnDetailBulkSale.getData(obj.Document_No, trans)

        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_Date,Location_Code from TSPL_SALE_RETURN_MASTER_BULKSALE where Document_No='" + strDocNo + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmBulkSaleReturn, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)

        End If
        Try
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_SALE_RETURN_MASTER_BULKSALE", "Document_No", "TSPL_SALE_RETURN_DETAIL_BULKSALE", "Document_No", trans)
            Dim qry As String = ""
            qry = "delete from TSPL_SALE_RETURN_DETAIL_BULKSALE where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SALE_RETURN_MASTER_BULKSALE where Document_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, arrLoc, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As ClsBulkSaleReturn = ClsBulkSaleReturn.GetData(strDocNo, "", NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmBulkSaleReturn, obj.Location_Code, obj.Document_Date, trans)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            ''richa 24/11/2014 for INventory movement
            If clsCommon.CompairString(obj.Against, "Bulk Invoice") = CompairStringResult.Equal Then

                Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
                Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)

                Dim strRgpNo As String = Nothing
                Dim intCounter As Integer = 0
                For Each objTr As ClsSaleReturnDetailBulkSale In obj.arrSaleReturnDetailBulkSale
                    intCounter = intCounter + 1
                    Dim strItemType As String = clsItemMaster.GetItemType(objTr.Item_Code, trans)
                    Dim strItemTypeToSave As String = ""
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        strItemTypeToSave = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        strItemTypeToSave = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        strItemTypeToSave = "FT"
                    Else
                        strItemTypeToSave = strItemType
                        'Throw New Exception("Item Type not found: " + strItemType)
                    End If

                    Dim objLocationDetails As New clsItemLocationDetails()
                    Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Unit_code, trans)
                    If ConvFac = 0 Then
                        Throw New Exception("Conversion Factor found zero for item :" + objTr.Item_Code + " and Uom:'" + objTr.Unit_code)
                    End If
                    objLocationDetails.Item_Code = objTr.Item_Code
                    objLocationDetails.Item_Desc = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + objTr.Item_Code + "' ", trans)
                    objLocationDetails.Location_Code = obj.Location_Code
                    objLocationDetails.Location_Desc = clsDBFuncationality.getSingleValue("Select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" + obj.Location_Code + "' ", trans)
                    objLocationDetails.Item_Qty = -1 * objTr.InvoiceQty
                    objLocationDetails.Amount = -1 * objTr.InvoiceAmount
                    objLocationDetails.MRP = 0 * ConvFac

                    objLocationDetails.ItemType = strItemTypeToSave
                    ArrLocationDetails.Add(objLocationDetails)

                    Dim SubLocation As String = obj.Silo_No

                    Dim objInventoryMovemnt As New clsInventoryMovementNew()
                    objInventoryMovemnt.InOut = "I"
                    If clsCommon.myLen(SubLocation) > 0 Then
                        objInventoryMovemnt.Location_Code = SubLocation
                        objInventoryMovemnt.main_location = obj.Location_Code
                    Else
                        objInventoryMovemnt.Location_Code = obj.Location_Code
                    End If

                    objInventoryMovemnt.Cust_Code = obj.Customer_Code
                    objInventoryMovemnt.Cust_Name = obj.Customer_Name

                    objInventoryMovemnt.Item_Code = objTr.Item_Code
                    objInventoryMovemnt.Item_Desc = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + objTr.Item_Code + "' ", trans)

                    ''richa agarwal 8 Jan,2019 ERO/07/01/19-000459
                    Dim UseKGLitreConversionInBulkSaleAsperCLRCalculation As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.UseKGLitreConversionInBulkSaleAsperCLRCalculation, clsFixedParameterCode.UseKGLitreConversionInBulkSaleAsperCLRCalculation, trans)) = 1, True, False))
                    If UseKGLitreConversionInBulkSaleAsperCLRCalculation = True Then
                        objInventoryMovemnt.Qty = objTr.InvoiceQty_in_Ltr
                        objInventoryMovemnt.UOM = "Ltr"
                    Else
                        objInventoryMovemnt.Qty = objTr.InvoiceQty
                        objInventoryMovemnt.UOM = objTr.Unit_code
                    End If

                    objInventoryMovemnt.MRP = objTr.InvoiceAmount / objTr.InvoiceQty
                    objInventoryMovemnt.Add_Cost = 0
                    objInventoryMovemnt.FAT_Per = objTr.InvoiceFatPer
                    objInventoryMovemnt.SNF_Per = objTr.InvoiceSNFPer
                    ''richa agarwal 16/08/2016
                    'objInventoryMovemnt.FAT_KG = 0
                    'objInventoryMovemnt.SNF_KG = 0

                    ''richa ERO/07/01/19-000459 21 Feb,2019
                    Dim dtDispatchDetail As DataTable = clsDBFuncationality.GetDataTable("select Basic_Cost ,Avg_Cost ,Fat_Rate  ,SNF_Rate  ,Fat_KG ,SNF_KG,Fat_Amt ,SNF_Amt ,Inventory_CrAcc ,Inventory_DrAcc  from tspl_inventory_movement_new where source_doc_no ='" & objTr.Dispatch_Code & "' and Trans_type='DispatchBS' ", trans)
                    If dtDispatchDetail IsNot Nothing AndAlso dtDispatchDetail.Rows.Count > 0 Then
                        objInventoryMovemnt.FAT_KG = clsCommon.myCdbl(dtDispatchDetail.Rows(0)("Fat_KG"))
                        objInventoryMovemnt.SNF_KG = clsCommon.myCdbl(dtDispatchDetail.Rows(0)("SNF_KG"))
                        objInventoryMovemnt.Fat_Rate = clsCommon.myCdbl(dtDispatchDetail.Rows(0)("Fat_Rate"))
                        objInventoryMovemnt.SNF_Rate = clsCommon.myCdbl(dtDispatchDetail.Rows(0)("SNF_Rate"))
                        objInventoryMovemnt.Fat_Amt = clsCommon.myCdbl(dtDispatchDetail.Rows(0)("Fat_Amt"))
                        objInventoryMovemnt.SNF_Amt = clsCommon.myCdbl(dtDispatchDetail.Rows(0)("SNF_Amt"))
                        objInventoryMovemnt.Avg_Cost = clsCommon.myCdbl(dtDispatchDetail.Rows(0)("Avg_Cost"))
                        objInventoryMovemnt.Basic_Cost = clsCommon.myCdbl(dtDispatchDetail.Rows(0)("Basic_Cost"))
                        objInventoryMovemnt.Inventory_CrAcc = clsCommon.myCstr(dtDispatchDetail.Rows(0)("Inventory_DrAcc"))
                        objInventoryMovemnt.Inventory_DrAcc = clsCommon.myCstr(dtDispatchDetail.Rows(0)("Inventory_CrAcc"))
                    Else
                        objInventoryMovemnt.FAT_KG = 0
                        objInventoryMovemnt.SNF_KG = 0
                        objInventoryMovemnt.Fat_Rate = 0
                        objInventoryMovemnt.SNF_Rate = 0
                        objInventoryMovemnt.Fat_Amt = 0
                        objInventoryMovemnt.SNF_Amt = 0
                    End If
                   
                    ''---------------------
                    objInventoryMovemnt.Net_Cost = objTr.InvoiceAmount

                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "FT"
                    End If
                    objInventoryMovemnt.ItemType = strItemTypeToSave
                    objInventoryMovemnt.CalculateAvgCost = False
                    ArrInventoryMovement.Add(objInventoryMovemnt)
                    ' End If
                Next
                isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrLocationDetails, trans)
                isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("SaleReturnBS", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)

                createARInvoice(obj, trans, "", "")
            Else

                ''richa 26/12/2018 for INventory movement BHA/12/11/18-000671
                Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
                Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)

                Dim strRgpNo As String = Nothing
                Dim intCounter As Integer = 0
                For Each objTr As ClsSaleReturnDetailBulkSale In obj.arrSaleReturnDetailBulkSale
                    intCounter = intCounter + 1
                    Dim strItemType As String = clsItemMaster.GetItemType(objTr.Item_Code, trans)
                    Dim strItemTypeToSave As String = ""
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        strItemTypeToSave = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        strItemTypeToSave = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        strItemTypeToSave = "FT"
                    Else
                        strItemTypeToSave = strItemType
                        'Throw New Exception("Item Type not found: " + strItemType)
                    End If

                    Dim SubLocation As String = obj.Silo_No

                    Dim objInventoryMovemnt As New clsInventoryMovementNew()
                    objInventoryMovemnt.InOut = "I"
                    If clsCommon.myLen(SubLocation) > 0 Then
                        objInventoryMovemnt.Location_Code = SubLocation
                        objInventoryMovemnt.main_location = obj.DispatchLocation
                    Else
                        objInventoryMovemnt.Location_Code = obj.DispatchLocation
                    End If

                    objInventoryMovemnt.Cust_Code = obj.Customer_Code
                    objInventoryMovemnt.Cust_Name = obj.Customer_Name

                    objInventoryMovemnt.Item_Code = objTr.Item_Code
                    objInventoryMovemnt.Item_Desc = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + objTr.Item_Code + "' ", trans)
                    ''richa agarwal 21 Feb,2019 ERO/07/01/19-000459
                    Dim UseKGLitreConversionInBulkSaleAsperCLRCalculation As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.UseKGLitreConversionInBulkSaleAsperCLRCalculation, clsFixedParameterCode.UseKGLitreConversionInBulkSaleAsperCLRCalculation, trans)) = 1, True, False))
                    If UseKGLitreConversionInBulkSaleAsperCLRCalculation = True Then
                        objInventoryMovemnt.Qty = objTr.DispatchQty_in_Ltr
                        objInventoryMovemnt.UOM = "Ltr"
                    Else
                        objInventoryMovemnt.Qty = objTr.DispatchQty
                        objInventoryMovemnt.UOM = objTr.Unit_code
                    End If

                    objInventoryMovemnt.MRP = objTr.DispatchAmount / objTr.DispatchQty
                    objInventoryMovemnt.Add_Cost = 0
                    objInventoryMovemnt.FAT_Per = objTr.DispatchFatPer
                    objInventoryMovemnt.SNF_Per = objTr.DispatchSNFPer

                    Dim dtDispatchDetail As DataTable = clsDBFuncationality.GetDataTable("select Basic_Cost ,Avg_Cost ,Fat_Rate  ,SNF_Rate  ,Fat_KG ,SNF_KG,Fat_Amt ,SNF_Amt ,Inventory_CrAcc ,Inventory_DrAcc  from tspl_inventory_movement_new where source_doc_no ='" & objTr.Dispatch_Code & "' and Trans_type='DispatchBS' ", trans)
                    If dtDispatchDetail IsNot Nothing AndAlso dtDispatchDetail.Rows.Count > 0 Then
                        objInventoryMovemnt.FAT_KG = clsCommon.myCdbl(dtDispatchDetail.Rows(0)("Fat_KG"))
                        objInventoryMovemnt.SNF_KG = clsCommon.myCdbl(dtDispatchDetail.Rows(0)("SNF_KG"))
                        objInventoryMovemnt.Fat_Rate = clsCommon.myCdbl(dtDispatchDetail.Rows(0)("Fat_Rate"))
                        objInventoryMovemnt.SNF_Rate = clsCommon.myCdbl(dtDispatchDetail.Rows(0)("SNF_Rate"))
                        objInventoryMovemnt.Fat_Amt = clsCommon.myCdbl(dtDispatchDetail.Rows(0)("Fat_Amt"))
                        objInventoryMovemnt.SNF_Amt = clsCommon.myCdbl(dtDispatchDetail.Rows(0)("SNF_Amt"))
                        objInventoryMovemnt.Avg_Cost = clsCommon.myCdbl(dtDispatchDetail.Rows(0)("Avg_Cost"))
                        objInventoryMovemnt.Basic_Cost = clsCommon.myCdbl(dtDispatchDetail.Rows(0)("Basic_Cost"))
                        objInventoryMovemnt.Inventory_CrAcc = clsCommon.myCstr(dtDispatchDetail.Rows(0)("Inventory_DrAcc"))
                        objInventoryMovemnt.Inventory_DrAcc = clsCommon.myCstr(dtDispatchDetail.Rows(0)("Inventory_CrAcc"))
                    Else
                        objInventoryMovemnt.FAT_KG = 0
                        objInventoryMovemnt.SNF_KG = 0
                        objInventoryMovemnt.Fat_Rate = 0
                        objInventoryMovemnt.SNF_Rate = 0
                        objInventoryMovemnt.Fat_Amt = 0
                        objInventoryMovemnt.SNF_Amt = 0
                    End If

                    ''---------------------
                    objInventoryMovemnt.Net_Cost = objTr.DispatchAmount

                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "FT"
                    End If
                    objInventoryMovemnt.ItemType = strItemTypeToSave


                    objInventoryMovemnt.CalculateAvgCost = False
                    ArrInventoryMovement.Add(objInventoryMovemnt)
                    ' End If
                Next
                isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("SaleReturnBS", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)

                ''-------------------------

                CreateJournalEntry(obj.Document_No, "", trans)
                Dim objGateOut As New ClsTankerOut()
                Dim gateoutentryno As String = clsDBFuncationality.getSingleValue("Select Document_No from TSPL_GATEOUT_SALE where GateEntryNo ='" & obj.GateEntryNo & "'", trans)
                If clsCommon.myLen(gateoutentryno) <= 0 Then
                    objGateOut.Document_Date = obj.Document_Date
                    objGateOut.GateEntryNo = obj.GateEntryNo
                    objGateOut.Tanker_No = obj.Tanker_No
                    objGateOut.Location_Code = obj.Location_Code
                    objGateOut.Customer_Code = obj.Customer_Code
                    objGateOut.IsGateOut = 1
                    ClsTankerOut.SaveData(objGateOut, True, trans)

                End If
                ''richa BHA/12/11/18-000671 as discussed it with Ranjana Mam tanker dispatch working stopped.
                ''isSaved = ClsBulkSaleReturn.SaveAndPostDispatchBulkSaleToTankerDispatch(obj.Document_No, trans)
            End If
            Dim qry = "Update TSPL_SALE_RETURN_MASTER_BULKSALE set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
            " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'isSaved = ClsBulkSaleReturn.SaveAndPostDispatchBulkSaleToTankerDispatch(obj.Document_No, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_SALE_RETURN_MASTER_BULKSALE", "Document_No", trans)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function SaveAndPostDispatchBulkSaleToTankerDispatch(DispatchReturnNo As String, trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Dim obj As clsMccDispatch = New clsMccDispatch()
        Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
        Dim objDispReturn As ClsBulkSaleReturn = ClsBulkSaleReturn.GetData(DispatchReturnNo, "", NavigatorType.Current, trans)
        Dim objDispBulkSale As ClsDispatchBulkSale = ClsDispatchBulkSale.GetData(objDispReturn.DispatchNo, "", NavigatorType.Current, trans)
        Dim objBulkPrice As ClsBulkSalePriceChart = ClsBulkSalePriceChart.GetData(objDispBulkSale.Price_Code, NavigatorType.Current, trans)
        Dim dtWeighment As DataTable = Nothing
        dtWeighment = clsDBFuncationality.GetDataTable("select * from TSPL_WEIGHMENT_DETAIL_BULKSALE where GateEntry_Document_No='" & objDispReturn.GateEntryNo & "'", trans)
        Dim dtParamFAT As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_PARAMETER_MASTER where Type='FAT'", trans)
        Dim dtParamSNF As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_PARAMETER_MASTER where Type='SNF'", trans)
        Dim dtParamCLR As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_PARAMETER_MASTER where Type='CLR'", trans)
        Dim dtParamCF As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_PARAMETER_MASTER where Type='CF'", trans)
        Dim objQc As ClsQualityCheckBulkSale = ClsQualityCheckBulkSale.GetData(objDispBulkSale.QC_Code, "", NavigatorType.Current, trans)
        If dtWeighment IsNot Nothing AndAlso dtWeighment.Rows.Count > 0 AndAlso objDispReturn IsNot Nothing AndAlso objDispBulkSale IsNot Nothing Then
            obj.isNewEntry = True
            obj.MCC_Code = objDispReturn.Location_Code
            obj.Chalan_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.MccDispatchChallan, "", obj.MCC_Code)
            If clsCommon.myLen(obj.Chalan_NO) <= 0 Then
                Throw New Exception("Error In Challan No Genertion")
                Exit Function
            End If
            obj.MCC_Name = clsCommon.myCstr(clsLocation.GetName(obj.MCC_Code, trans))
            obj.Dispatch_Date = clsCommon.myCDate(dt, "dd/MMM/yyyy hh:mm tt")
            obj.Tanker_Dispatch_To = IIf(clsERPFuncationality.isLocationMcc(objDispReturn.DispatchLocation, trans) = True, "MCC", "PLANT")
            obj.Mcc_Or_Plant_Code = objDispReturn.DispatchLocation
            obj.Tanker_No = objDispReturn.Tanker_No
            obj.Tanker_KM_Reading = 0

            obj.Drip_Marking = ""
            obj.Tanker_Full = "NO"
            obj.Control_Sample = "NO"
            obj.Name_Of_Custodian = "None"
            obj.Seal_No1 = ""
            obj.Seal_No2 = ""
            obj.Seal_No3 = ""
            obj.Seal_No4 = ""
            obj.Seal_No5 = ""
            obj.Seal_No6 = ""
            obj.Seal_No7 = ""
            obj.Seal_No8 = ""
            obj.Seal_No9 = ""
            obj.Seal_No10 = ""
            obj.Tare_Weight = clsCommon.myCdbl(dtWeighment.Rows(0)("tare_weight"))
            obj.Gross_Weight = clsCommon.myCdbl(dtWeighment.Rows(0)("Gross_Weight"))
            obj.Net_Qty = clsCommon.myCdbl(dtWeighment.Rows(0)("Net_Weight"))

            obj.control_sample_fat = 0
            obj.control_sample_snf = 0

            obj.Transfer_Price = clsCommon.myCdbl(objDispBulkSale.arrDispatchDetailBulkSale.Item(0).StandardRate)
            obj.Item_Code = clsCommon.myCstr(objDispBulkSale.arrDispatchDetailBulkSale.Item(0).Item_Code)
            obj.Item_Desc = clsCommon.myCstr(clsItemMaster.GetItemName(objDispBulkSale.arrDispatchDetailBulkSale.Item(0).Item_Code, trans))

            obj.Tanker_Transporter_Name = ""
            obj.Payment_Type = ""
            obj.Payment_Rate = ""
            obj.Charge_For = ""
            obj.Payment_Amount = 0
            obj.Chemist_Code = ""
            obj.Chemist_Name = ""

            obj.UOM_Code = clsCommon.myCstr(objDispBulkSale.arrDispatchDetailBulkSale.Item(0).Unit_code)
            obj.UOM_desc = ""

            obj.Remarks = ""



            obj.PriceCode = clsCommon.myCstr(objDispBulkSale.Price_Code)
            obj.FAT_W = clsCommon.myCdbl(objBulkPrice.Fat_Weightage)
            obj.SNF_W = clsCommon.myCdbl(objBulkPrice.Snf_Weightage)
            obj.FAT_R = clsCommon.myCdbl(objBulkPrice.Fat_Ratio)
            obj.SNF_R = clsCommon.myCdbl(objBulkPrice.Snf_Ratio)
            obj.FAT_RATE = clsCommon.myCdbl(objDispBulkSale.arrDispatchDetailBulkSale.Item(0).FatRate)
            obj.SNF_RATE = clsCommon.myCdbl(objDispBulkSale.arrDispatchDetailBulkSale.Item(0).SNFRate)
            obj.FAT_KG = clsCommon.myCdbl(objDispBulkSale.arrDispatchDetailBulkSale.Item(0).Fat_KG)
            obj.SNF_KG = clsCommon.myCdbl(objDispBulkSale.arrDispatchDetailBulkSale.Item(0).SNF_KG)
            obj.Amount = clsCommon.myCdbl(objDispBulkSale.Total_Amt)
            obj.isIntermittent = 0
            obj.CurrentLevel = 0
            obj.FinalLoc = ""
            obj.isReversed = 0
            Dim i As Integer = 0
            Dim objParam As New Mcc_Dispatch_Chalan_Parameter
            obj.arrParmValue = New List(Of Mcc_Dispatch_Chalan_Parameter)
            objParam = New Mcc_Dispatch_Chalan_Parameter
            objParam.Chalan_No = clsCommon.myCstr(obj.Chalan_NO)
            objParam.Param_Field_Code = clsCommon.myCstr(dtParamFAT.Rows(0)("Code"))
            objParam.Param_Field_Desc = clsCommon.myCstr(dtParamFAT.Rows(0)("Description"))
            objParam.Param_Field_Value = clsCommon.myCstr(objQc.Fat)
            objParam.Param_Type = "FAT"
            obj.arrParmValue.Add(objParam)


            'obj.arrParmValue = New List(Of Mcc_Dispatch_Chalan_Parameter)
            objParam = New Mcc_Dispatch_Chalan_Parameter
            objParam.Chalan_No = clsCommon.myCstr(obj.Chalan_NO)
            objParam.Param_Field_Code = clsCommon.myCstr(dtParamSNF.Rows(0)("Code"))
            objParam.Param_Field_Desc = clsCommon.myCstr(dtParamSNF.Rows(0)("Description"))
            objParam.Param_Field_Value = clsCommon.myCstr(objQc.SNF)
            objParam.Param_Type = "SNF"
            obj.arrParmValue.Add(objParam)

            'obj.arrParmValue = New List(Of Mcc_Dispatch_Chalan_Parameter)
            objParam = New Mcc_Dispatch_Chalan_Parameter
            objParam.Chalan_No = clsCommon.myCstr(obj.Chalan_NO)
            objParam.Param_Field_Code = clsCommon.myCstr(dtParamCLR.Rows(0)("Code"))
            objParam.Param_Field_Desc = clsCommon.myCstr(dtParamCLR.Rows(0)("Description"))
            objParam.Param_Field_Value = clsCommon.myCstr(objQc.CLR)
            objParam.Param_Type = "CLR"
            obj.arrParmValue.Add(objParam)

            'obj.arrParmValue = New List(Of Mcc_Dispatch_Chalan_Parameter)
            objParam = New Mcc_Dispatch_Chalan_Parameter
            objParam.Chalan_No = clsCommon.myCstr(obj.Chalan_NO)
            objParam.Param_Field_Code = clsCommon.myCstr(dtParamCF.Rows(0)("Code"))
            objParam.Param_Field_Desc = clsCommon.myCstr(dtParamCF.Rows(0)("Description"))
            objParam.Param_Field_Value = clsCommon.myCstr(objQc.Correction_Factor)
            objParam.Param_Type = "CF"
            obj.arrParmValue.Add(objParam)
            obj.Modified_By = objCommonVar.CurrentUserCode
            obj.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy")
            obj.Comp_Code = objCommonVar.CurrentCompanyCode
            obj.RefDocType = clsUserMgtCode.FrmBulkSaleReturn
            obj.RefDocNo = objDispReturn.Document_No
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy")
            End If
            obj.isBulkSaleData = True
            If clsMccDispatch.SaveData(obj, trans) Then
                isSaved = clsMccDispatch.PostData(clsUserMgtCode.frmMCCDispatch, obj.Chalan_NO, trans)
            Else
                isSaved = False
            End If
        End If





        Return isSaved

    End Function
    Public Shared Sub CreateJournalEntry(ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction)
        Dim obj As New ClsBulkSaleReturn
        obj = ClsBulkSaleReturn.GetData(strCode, arrLoc, NavigatorType.Current, trans)
        Dim ArryLstGLAC As ArrayList = New ArrayList()
        Dim strInventoryControlAc As String = ""
        Dim strShipmentClearingAC As String = ""
        Dim dblTotalCost As Double = 0
        Dim RecoControlACC As String = ""
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
            RecoControlACC = "I"
        End If

        strShipmentClearingAC = clsDBFuncationality.getSingleValue("SELECT PA.Shipment_Clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
          " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
           " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.arrSaleReturnDetailBulkSale.Item(0).Item_Code + "'", trans)
        strShipmentClearingAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strShipmentClearingAC, obj.Location_Code, trans)

        If clsCommon.myLen(strShipmentClearingAC) = 0 Then
            Throw New Exception("Please set Shipment clearing Account for first item")
        End If

        Dim dblCogsCost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end) as COst from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Source_Doc_No='" & obj.arrSaleReturnDetailBulkSale.Item(0).Dispatch_Code & "'", trans))

        Dim Acc() As String = {strShipmentClearingAC, -1 * dblCogsCost}
        ArryLstGLAC.Add(Acc)

        Dim strSql As String = "select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end as Cost from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code  where Source_Doc_No='" & obj.arrSaleReturnDetailBulkSale.Item(0).Dispatch_Code & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                strInventoryControlAc = clsDBFuncationality.getSingleValue("SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans)
                strInventoryControlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInventoryControlAc, obj.Location_Code, trans)

                If clsCommon.myLen(strInventoryControlAc) = 0 Then
                    Throw New Exception("Please set Inventory Control Account for first item")
                End If
                Dim Acc1() As String = {strInventoryControlAc, clsCommon.myCdbl(dr("Cost")), "", "", "", "", "", "", RecoControlACC}
                ArryLstGLAC.Add(Acc1)
                If clsCommon.CompairString(RecoControlACC, "I") = CompairStringResult.Equal Then
                    '' TEC/21/02/19-000428 by Richa on 14/02/2019
                    clsInventoryMovement.UpdateInvControlAccount(clsCommon.myCstr(strCode), "SaleReturnBS", clsCommon.myCstr(dr("Item_Code")), strInventoryControlAc, "", "", trans)
                    ''------------------
                End If
            Next
        End If

        '' BHA/30/10/18-000646 RICHA AGARWAL SEND CUSTOMER CODE AND CUSTOMER NAME INTO JOURNAL ENTRY AND TYPE C instead of O 30 Oct,2018
        transportSql.FunGrnlEntryWithTrans(obj.Location_Code, False, trans, obj.Document_Date, "Journal Entry Against Dispatch Bulk Sale Return for Document No " + obj.Document_No + " ", "DS-BR", "DISPATCH Bulk Sale Return", obj.Document_No, "", "C", obj.Customer_Code, clsCustomerMaster.GetName(obj.Customer_Code, trans), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , "", "")

    End Sub
    Public Shared Function createARInvoice(ByVal obj As ClsBulkSaleReturn, ByVal trans As SqlTransaction, ByVal strARNoForRecreate As String, ByVal strVoucherForRecreate As String) As Boolean
        ''''''''''''''''''''''''''''''''''For Making AR Invoice
        Dim objCustInv As New clsCustomerInvoiceHead()
        ''objCustInv.Document_No ''Will be Generateed
        objCustInv.Document_Date = obj.Document_Date
        objCustInv.Document_Type = "C"
        objCustInv.Document_Total = obj.Total_Amt
        objCustInv.Customer_Code = obj.Customer_Code
        objCustInv.RoundOffAmount = obj.RoundOffAmount
        objCustInv.Customer_Name = clsDBFuncationality.getSingleValue(" select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.Customer_Code + "'", trans)
        objCustInv.Posting_Date = obj.Document_Date
        Dim qry As String = " select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.Customer_Code + "'"
        objCustInv.Account_Set = clsDBFuncationality.getSingleValue(qry, trans)
        ''objCustInv.Order_No
        objCustInv.loc_code = clsLocation.GetSegmentCode(obj.Location_Code, trans)
        ' objCustInv.Against_Sale_No = obj.InvoiceNo
        objCustInv.On_Hold = 0
        objCustInv.Remarks = ""
        objCustInv.Description = ""
        objCustInv.Tax_Group = obj.Tax_Group
        objCustInv.TAX1 = obj.TAX1
        objCustInv.TAX1_Rate = obj.TAX1_Rate
        objCustInv.TAX1_Amt = obj.TAX1_Amt
        objCustInv.TAX2 = obj.TAX2
        objCustInv.TAX2_Rate = obj.TAX2_Rate
        objCustInv.TAX2_Amt = obj.TAX2_Amt
        objCustInv.TAX3 = obj.TAX3
        objCustInv.TAX3_Rate = obj.TAX3_Rate
        objCustInv.TAX3_Amt = obj.TAX3_Amt
        objCustInv.TAX4 = obj.TAX4
        objCustInv.TAX4_Rate = obj.TAX4_Rate
        objCustInv.TAX4_Amt = obj.TAX4_Amt
        objCustInv.TAX5 = obj.TAX5
        objCustInv.TAX5_Rate = obj.TAX5_Rate
        objCustInv.TAX5_Amt = obj.TAX5_Amt
        objCustInv.TAX6 = ""
        objCustInv.TAX6_Rate = 0
        objCustInv.TAX6_Amt = 0
        objCustInv.TAX7 = ""
        objCustInv.TAX7_Rate = 0
        objCustInv.TAX7_Amt = 0
        objCustInv.TAX8 = ""
        objCustInv.TAX8_Rate = 0
        objCustInv.TAX8_Amt = 0
        objCustInv.TAX9 = ""
        objCustInv.TAX9_Rate = 0
        objCustInv.TAX9_Amt = 0
        objCustInv.TAX10 = ""
        objCustInv.TAX10_Rate = 0
        objCustInv.TAX10_Amt = 0
        objCustInv.Total_Tax = obj.Total_Tax_Amt
        objCustInv.Tax1_BAmount = obj.TAX1_Base_Amt
        objCustInv.Tax2_BAmount = obj.TAX2_Base_Amt
        objCustInv.Tax3_BAmount = obj.TAX3_Base_Amt
        objCustInv.Tax4_BAmount = obj.TAX4_Base_Amt
        objCustInv.Tax5_BAmount = obj.TAX5_Base_Amt
        objCustInv.Tax6_BAmount = 0
        objCustInv.Tax7_BAmount = 0
        objCustInv.Tax8_BAmount = 0
        objCustInv.Tax9_BAmount = 0
        objCustInv.Tax10_BAmount = 0
        objCustInv.Balance_Amt = obj.Total_Amt
        objCustInv.Terms_Code = ""

        objCustInv.Return_Type = ""
        ''richa agarwal 08/04/2015
        ' objCustInv.Trans_Type = "BS"
        ''richa agarwal 25/06/2015 change bulk sale return type and send refdoctype BM00000007163
        objCustInv.Trans_Type = "BSR"
        objCustInv.RefDocType = "BS_Return"
        ''----------------------
        ''---------------------
        'qry = "select Terms_Code,Terms_Desc,No_Days from TSPL_TERMS_MASTER where Terms_Code='" + obj.Terms_Code + "'"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '    objCustInv.Terms_Description = clsCommon.myCstr(dt.Rows(0)("Terms_Desc"))
        '    objCustInv.Due_Date = obj.Document_Date.AddDays(clsCommon.myCdbl(dt.Rows(0)("No_Days")))
        'End If
        objCustInv.Discount_Percentage = 0
        objCustInv.Discount_Base = obj.Document_Amount
        objCustInv.Discount_Amount = 0
        objCustInv.Amount_Less_Discount = obj.Document_Amount
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable("select Receivable_Control_acct,Receipts_Discount_acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" + objCustInv.Account_Set + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objCustInv.Customer_Control_AC = clsCommon.myCstr(dt.Rows(0)("Receivable_Control_acct"))
            ''If clsCommon.myCdbl(obj.Discount_Amt) > 0 Then
            ''    objCustInv.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Receipts_Discount_acct"))
            ''End If
        End If

        If obj.TAX1_Amt > 0 AndAlso clsCommon.myLen(obj.TAX1) > 0 Then
            objCustInv.TAX1_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX1, trans)
            objCustInv.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX1_GLAC, obj.Location_Code, trans)
        End If
        If obj.TAX2_Amt > 0 AndAlso clsCommon.myLen(obj.TAX2) > 0 Then
            objCustInv.TAX2_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX2, trans)
            objCustInv.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX2_GLAC, obj.Location_Code, trans)
        End If
        If obj.TAX3_Amt > 0 AndAlso clsCommon.myLen(obj.TAX3) > 0 Then
            objCustInv.TAX3_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX3, trans)
            objCustInv.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX3_GLAC, obj.Location_Code, trans)
        End If
        If obj.TAX4_Amt > 0 AndAlso clsCommon.myLen(obj.TAX4) > 0 Then
            objCustInv.TAX4_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX4, trans)
            objCustInv.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX4_GLAC, obj.Location_Code, trans)
        End If
        If obj.TAX5_Amt > 0 AndAlso clsCommon.myLen(obj.TAX5) > 0 Then
            objCustInv.TAX5_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX5, trans)
            objCustInv.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX5_GLAC, obj.Location_Code, trans)
        End If
        'If obj.TAX6_Amt > 0 AndAlso clsCommon.myLen(obj.TAX6) > 0 Then
        '    objCustInv.TAX6_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX6, trans)
        '    objCustInv.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX6_GLAC, obj.Bill_To_Location, trans)
        'End If
        'If obj.TAX7_Amt > 0 AndAlso clsCommon.myLen(obj.TAX7) > 0 Then
        '    objCustInv.TAX7_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX7, trans)
        '    objCustInv.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX7_GLAC, obj.Bill_To_Location, trans)
        'End If
        'If obj.TAX8_Amt > 0 AndAlso clsCommon.myLen(obj.TAX8) > 0 Then
        '    objCustInv.TAX8_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX8, trans)
        '    objCustInv.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX8_GLAC, obj.Bill_To_Location, trans)
        'End If
        'If obj.TAX9_Amt > 0 AndAlso clsCommon.myLen(obj.TAX9) > 0 Then
        '    objCustInv.TAX9_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX9, trans)
        '    objCustInv.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX9_GLAC, obj.Bill_To_Location, trans)
        'End If
        'If obj.TAX10_Amt > 0 AndAlso clsCommon.myLen(obj.TAX10) > 0 Then
        '    objCustInv.TAX10_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX10, trans)
        '    objCustInv.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX10_GLAC, obj.Bill_To_Location, trans)
        'End If

        'objCustInv.RefDocType=
        'objCustInv.RefDocNo
        'objCustInv.Add_Charge_Code1 = obj.Add_Charge_Code1
        'objCustInv.Add_Charge_Name1 = obj.Add_Charge_Name1
        'objCustInv.Add_Charge_Amt1 = obj.Add_Charge_Amt1
        'objCustInv.Add_Charge_Code2 = obj.Add_Charge_Code2
        'objCustInv.Add_Charge_Name2 = obj.Add_Charge_Name2
        'objCustInv.Add_Charge_Amt2 = obj.Add_Charge_Amt2
        'objCustInv.Add_Charge_Code3 = obj.Add_Charge_Code3
        'objCustInv.Add_Charge_Name3 = obj.Add_Charge_Name3
        'objCustInv.Add_Charge_Amt3 = obj.Add_Charge_Amt3
        'objCustInv.Add_Charge_Code4 = obj.Add_Charge_Code4
        'objCustInv.Add_Charge_Name4 = obj.Add_Charge_Name4
        'objCustInv.Add_Charge_Amt4 = obj.Add_Charge_Amt4
        'objCustInv.Add_Charge_Code5 = obj.Add_Charge_Code5
        'objCustInv.Add_Charge_Name5 = obj.Add_Charge_Name5
        'objCustInv.Add_Charge_Amt5 = obj.Add_Charge_Amt5
        'objCustInv.Add_Charge_Code6 = obj.Add_Charge_Code6
        'objCustInv.Add_Charge_Name6 = obj.Add_Charge_Name6
        'objCustInv.Add_Charge_Amt6 = obj.Add_Charge_Amt6
        'objCustInv.Add_Charge_Code7 = obj.Add_Charge_Code7
        'objCustInv.Add_Charge_Name7 = obj.Add_Charge_Name7
        'objCustInv.Add_Charge_Amt7 = obj.Add_Charge_Amt7
        'objCustInv.Add_Charge_Code8 = obj.Add_Charge_Code8
        'objCustInv.Add_Charge_Name8 = obj.Add_Charge_Name8
        'objCustInv.Add_Charge_Amt8 = obj.Add_Charge_Amt8
        'objCustInv.Add_Charge_Code9 = obj.Add_Charge_Code9
        'objCustInv.Add_Charge_Name9 = obj.Add_Charge_Name9
        'objCustInv.Add_Charge_Amt9 = obj.Add_Charge_Amt9
        'objCustInv.Add_Charge_Code10 = obj.Add_Charge_Code10
        'objCustInv.Add_Charge_Name10 = obj.Add_Charge_Name10
        'objCustInv.Add_Charge_Amt10 = obj.Add_Charge_Amt10
        'objCustInv.Total_Add_Charge = obj.Total_Add_Charge
        objCustInv.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
        ''objCustInv.Status
        ''objCustInv.AgainstScrap
        objCustInv.Against_Sale_Return_No = obj.Document_No
        Dim counter As Integer = 1
        objCustInv.Arr = New List(Of clsCustomerInvoiceDetail)

        '''' for return Qty
        For Each objTr As ClsSaleReturnDetailBulkSale In obj.arrSaleReturnDetailBulkSale
            ' If clsCommon.CompairString(objTr.Scheme_Item, "N") = CompairStringResult.Equal Then
            Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()
            If objTr.InvoiceAmount > 0 Then
                objCustInvTR.SNo = counter
                'If clsCommon.CompairString(objTr.Row_Type, "Item") = CompairStringResult.Equal Then
                dt = clsItemMaster.GetSaleReturnAccGLAC(objTr.Item_Code, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set sale account for item" + objTr.Item_Code)
                End If
                objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Sales_Return_Account"))
                objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Location_Code, trans)
                objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)
                objCustInvTR.Reco_Control_Account = "S"
                'Else ''for row type misl.
                '    Dim objAC As clsAdditionalCharge = clsAdditionalCharge.GetData(objTr.Item_Code, NavigatorType.Current, trans)
                '    If objAC Is Nothing Then
                '        Throw New Exception("Please set GL Ac from addition charge" + objTr.Item_Code)
                '    End If
                '    objCustInvTR.GL_Account_Code = objAC.Account_Code
                '    objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Bill_To_Location, trans)
                '    objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)
                'End If

                objCustInvTR.Amount = objTr.InvoiceAmount
                objCustInvTR.Discount_Per = 0
                objCustInvTR.Discount = 0
                objCustInvTR.Amount_less_Discount = objTr.InvoiceAmount
                objCustInvTR.TAX1 = objTr.TAX1
                objCustInvTR.TAX1_Rate = objTr.TAX1_Rate
                objCustInvTR.TAX1_Amt = objTr.TAX1_Amt
                objCustInvTR.TAX2 = objTr.TAX2
                objCustInvTR.TAX2_Rate = objTr.TAX2_Rate
                objCustInvTR.TAX2_Amt = objTr.TAX2_Amt
                objCustInvTR.TAX3 = objTr.TAX3
                objCustInvTR.TAX3_Rate = objTr.TAX3_Rate
                objCustInvTR.TAX3_Amt = objTr.TAX3_Amt
                objCustInvTR.TAX4 = objTr.TAX4
                objCustInvTR.TAX4_Rate = objTr.TAX4_Rate
                objCustInvTR.TAX4_Amt = objTr.TAX4_Amt
                objCustInvTR.TAX5 = objTr.TAX5
                objCustInvTR.TAX5_Rate = objTr.TAX5_Rate
                objCustInvTR.TAX5_Amt = objTr.TAX5_Amt

                objCustInvTR.TAX6 = ""
                objCustInvTR.TAX6_Rate = 0
                objCustInvTR.TAX6_Amt = 0
                objCustInvTR.TAX6_Base_Amt = 0
                objCustInvTR.TAX7 = ""
                objCustInvTR.TAX7_Rate = 0
                objCustInvTR.TAX7_Amt = 0
                objCustInvTR.TAX7_Base_Amt = 0
                objCustInvTR.TAX8 = ""
                objCustInvTR.TAX8_Rate = 0
                objCustInvTR.TAX8_Amt = 0
                objCustInvTR.TAX8_Base_Amt = 0
                objCustInvTR.TAX9 = ""
                objCustInvTR.TAX9_Rate = 0
                objCustInvTR.TAX9_Amt = 0
                objCustInvTR.TAX9_Base_Amt = 0
                objCustInvTR.TAX10 = ""
                objCustInvTR.TAX10_Rate = 0
                objCustInvTR.TAX10_Amt = 0
                objCustInvTR.TAX10_Base_Amt = 0
                objCustInvTR.Total_Tax = objTr.Total_Tax_Amt
                objCustInvTR.Total_Amount = objTr.Item_Net_Amt
                objCustInvTR.Remarks = ""
                objCustInvTR.TAX1_Base_Amt = objTr.TAX1_Base_Amt
                objCustInvTR.TAX2_Base_Amt = objTr.TAX2_Base_Amt
                objCustInvTR.TAX3_Base_Amt = objTr.TAX3_Base_Amt
                objCustInvTR.TAX4_Base_Amt = objTr.TAX4_Base_Amt
                objCustInvTR.TAX5_Base_Amt = objTr.TAX5_Base_Amt
                'objCustInvTR.Comments=objTr.Comments
                objCustInv.Arr.Add(objCustInvTR)
                counter += 1
            End If
            ' End If
        Next


        '''' for Damage Qty
        'For Each objTr As ClsSaleReturnDetailBulkSale In obj.arrSaleReturnDetailBulkSale
        '    If clsCommon.CompairString(objTr.Scheme_Item, "N") = CompairStringResult.Equal Then
        '        Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()
        '        If objTr.Damage_Amount > 0 Then
        '            objCustInvTR.SNo = counter
        '            If clsCommon.CompairString(objTr.Row_Type, "Item") = CompairStringResult.Equal Then
        '                dt = clsItemMaster.GetDamageAccGLAC(objTr.Item_Code, trans)
        '                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
        '                    Throw New Exception("Please set Damage account for item" + objTr.Item_Code)
        '                End If
        '                objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Damaged_Goods"))
        '                objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Bill_To_Location, trans)
        '                objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)
        '            Else ''for row type misl.
        '                Dim objAC As clsAdditionalCharge = clsAdditionalCharge.GetData(objTr.Item_Code, NavigatorType.Current, trans)
        '                If objAC Is Nothing Then
        '                    Throw New Exception("Please set GL Ac from addition charge" + objTr.Item_Code)
        '                End If
        '                objCustInvTR.GL_Account_Code = objAC.Account_Code
        '                objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Bill_To_Location, trans)
        '                objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)
        '            End If

        '            objCustInvTR.Amount = objTr.Damage_Amount
        '            objCustInvTR.Discount_Per = objTr.Disc_Per
        '            objCustInvTR.Discount = objTr.Disc_Amt
        '            objCustInvTR.Amount_less_Discount = objTr.Damage_Amount
        '            objCustInvTR.TAX1 = objTr.TAX1
        '            objCustInvTR.TAX1_Rate = objTr.TAX1_Rate
        '            objCustInvTR.TAX1_Amt = objTr.TAX1_Amt
        '            objCustInvTR.TAX2 = objTr.TAX2
        '            objCustInvTR.TAX2_Rate = objTr.TAX2_Rate
        '            objCustInvTR.TAX2_Amt = objTr.TAX2_Amt
        '            objCustInvTR.TAX3 = objTr.TAX3
        '            objCustInvTR.TAX3_Rate = objTr.TAX3_Rate
        '            objCustInvTR.TAX3_Amt = objTr.TAX3_Amt
        '            objCustInvTR.TAX4 = objTr.TAX4
        '            objCustInvTR.TAX4_Rate = objTr.TAX4_Rate
        '            objCustInvTR.TAX4_Amt = objTr.TAX4_Amt
        '            objCustInvTR.TAX5 = objTr.TAX5
        '            objCustInvTR.TAX5_Rate = objTr.TAX5_Rate
        '            objCustInvTR.TAX5_Amt = objTr.TAX5_Amt
        '            objCustInvTR.TAX6 = objTr.TAX6
        '            objCustInvTR.TAX6_Rate = objTr.TAX6_Rate
        '            objCustInvTR.TAX6_Amt = objTr.TAX6_Amt
        '            objCustInvTR.TAX7 = objTr.TAX7
        '            objCustInvTR.TAX7_Rate = objTr.TAX7_Rate
        '            objCustInvTR.TAX7_Amt = objTr.TAX7_Amt
        '            objCustInvTR.TAX8 = objTr.TAX8
        '            objCustInvTR.TAX8_Rate = objTr.TAX8_Rate
        '            objCustInvTR.TAX8_Amt = objTr.TAX8_Amt
        '            objCustInvTR.TAX9 = objTr.TAX9
        '            objCustInvTR.TAX9_Rate = objTr.TAX9_Rate
        '            objCustInvTR.TAX9_Amt = objTr.TAX9_Amt
        '            objCustInvTR.TAX10 = objTr.TAX10
        '            objCustInvTR.TAX10_Rate = objTr.TAX10_Rate
        '            objCustInvTR.TAX10_Amt = objTr.TAX10_Amt
        '            objCustInvTR.Total_Tax = objTr.Total_Tax_Amt
        '            objCustInvTR.Total_Amount = objTr.Item_Net_Amt
        '            objCustInvTR.Remarks = objTr.Remarks
        '            'objCustInvTR.Comments=objTr.Comments
        '            objCustInv.Arr.Add(objCustInvTR)
        '            counter += 1
        '        End If
        '    End If
        'Next


        '''''  For Price only return type
        'If clsCommon.CompairString(obj.Return_Type, "P") = CompairStringResult.Equal Then
        '    For Each objTr As clsSalesReturnFreshSaleDetail In obj.Arr
        '        If clsCommon.CompairString(objTr.Scheme_Item, "N") = CompairStringResult.Equal Then
        '            Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()
        '            If objTr.Return_Amount = 0 AndAlso objTr.Damage_Amount = 0 AndAlso objTr.Amt_Less_Discount > 0 Then
        '                objCustInvTR.SNo = counter
        '                If clsCommon.CompairString(objTr.Row_Type, "Item") = CompairStringResult.Equal Then
        '                    dt = clsItemMaster.GetSaleReturnAccGLAC(objTr.Item_Code, trans)
        '                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
        '                        Throw New Exception("Please set sale account for item" + objTr.Item_Code)
        '                    End If
        '                    objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Sales_Return_Account"))
        '                    objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Bill_To_Location, trans)
        '                    objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)
        '                Else ''for row type misl.
        '                    Dim objAC As clsAdditionalCharge = clsAdditionalCharge.GetData(objTr.Item_Code, NavigatorType.Current, trans)
        '                    If objAC Is Nothing Then
        '                        Throw New Exception("Please set GL Ac from addition charge" + objTr.Item_Code)
        '                    End If
        '                    objCustInvTR.GL_Account_Code = objAC.Account_Code
        '                    objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Bill_To_Location, trans)
        '                    objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)
        '                End If

        '                objCustInvTR.Amount = objTr.Amt_Less_Discount
        '                objCustInvTR.Discount_Per = objTr.Disc_Per
        '                objCustInvTR.Discount = objTr.Disc_Amt
        '                objCustInvTR.Amount_less_Discount = objTr.Amt_Less_Discount
        '                objCustInvTR.TAX1 = objTr.TAX1
        '                objCustInvTR.TAX1_Rate = objTr.TAX1_Rate
        '                objCustInvTR.TAX1_Amt = objTr.TAX1_Amt
        '                objCustInvTR.TAX2 = objTr.TAX2
        '                objCustInvTR.TAX2_Rate = objTr.TAX2_Rate
        '                objCustInvTR.TAX2_Amt = objTr.TAX2_Amt
        '                objCustInvTR.TAX3 = objTr.TAX3
        '                objCustInvTR.TAX3_Rate = objTr.TAX3_Rate
        '                objCustInvTR.TAX3_Amt = objTr.TAX3_Amt
        '                objCustInvTR.TAX4 = objTr.TAX4
        '                objCustInvTR.TAX4_Rate = objTr.TAX4_Rate
        '                objCustInvTR.TAX4_Amt = objTr.TAX4_Amt
        '                objCustInvTR.TAX5 = objTr.TAX5
        '                objCustInvTR.TAX5_Rate = objTr.TAX5_Rate
        '                objCustInvTR.TAX5_Amt = objTr.TAX5_Amt
        '                objCustInvTR.TAX6 = objTr.TAX6
        '                objCustInvTR.TAX6_Rate = objTr.TAX6_Rate
        '                objCustInvTR.TAX6_Amt = objTr.TAX6_Amt
        '                objCustInvTR.TAX7 = objTr.TAX7
        '                objCustInvTR.TAX7_Rate = objTr.TAX7_Rate
        '                objCustInvTR.TAX7_Amt = objTr.TAX7_Amt
        '                objCustInvTR.TAX8 = objTr.TAX8
        '                objCustInvTR.TAX8_Rate = objTr.TAX8_Rate
        '                objCustInvTR.TAX8_Amt = objTr.TAX8_Amt
        '                objCustInvTR.TAX9 = objTr.TAX9
        '                objCustInvTR.TAX9_Rate = objTr.TAX9_Rate
        '                objCustInvTR.TAX9_Amt = objTr.TAX9_Amt
        '                objCustInvTR.TAX10 = objTr.TAX10
        '                objCustInvTR.TAX10_Rate = objTr.TAX10_Rate
        '                objCustInvTR.TAX10_Amt = objTr.TAX10_Amt
        '                objCustInvTR.Total_Tax = objTr.Total_Tax_Amt
        '                objCustInvTR.Total_Amount = objTr.Item_Net_Amt
        '                objCustInvTR.Remarks = objTr.Remarks
        '                'objCustInvTR.Comments=objTr.Comments
        '                objCustInv.Arr.Add(objCustInvTR)
        '                counter += 1
        '            End If
        '        End If
        '    Next
        ' End If
        ''richa TEC/21/02/19-000428 send form id 21 Feb,2019
        objCustInv.SaveData(objCustInv, True, trans, "SaleReturnBS", strVoucherForRecreate, strARNoForRecreate)
        clsCustomerInvoiceHead.PostData("SaleReturnBS", objCustInv.Document_No, "", trans)
        Return True
        Return False
    End Function
End Class

Public Class ClsSaleReturnDetailBulkSale
#Region "Variable"
    Public Document_No As String = Nothing
    Public Dispatch_Code As String = Nothing
    Public Dispatch_Date As Date?
    Public Item_Code As String = Nothing
    Public Unit_code As String = Nothing
    Public Tanker_Code As String = Nothing
    Public DispatchQty As Double = 0
    Public DispatchFatPer As Double = 0
    Public DispatchSNFPer As Double = 0
    Public DispatchRate As Double = 0
    Public DispatchAmount As Double = 0
    Public InvoiceQty As Double = 0
    Public InvoiceFatPer As Double = 0
    Public InvoiceSNFPer As Double = 0
    Public InvoiceRate As Double = 0
    Public InvoiceAmount As Double = 0
    Public InvoiceFatKG As Double = 0
    Public InvoiceSNFKG As Double = 0
    Public DispatchQty_in_Ltr As Decimal = 0
    Public InvoiceQty_in_Ltr As Decimal = 0
    Public TAX1 As String = Nothing
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Rate As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0

#End Region
    Public Shared Function saveData(ByVal arrObj As List(Of ClsSaleReturnDetailBulkSale), ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then

                For Each obj As ClsSaleReturnDetailBulkSale In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Dispatch_Code", obj.Dispatch_Code)
                    clsCommon.AddColumnsForChange(coll, "Dispatch_Date", clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                    clsCommon.AddColumnsForChange(coll, "Tanker_Code", obj.Tanker_Code, True)
                    clsCommon.AddColumnsForChange(coll, "DispatchQty", obj.DispatchQty)
                    clsCommon.AddColumnsForChange(coll, "DispatchFatPer", obj.DispatchFatPer)
                    clsCommon.AddColumnsForChange(coll, "DispatchSNFPer", obj.DispatchSNFPer)
                    clsCommon.AddColumnsForChange(coll, "DispatchRate", obj.DispatchRate)
                    clsCommon.AddColumnsForChange(coll, "DispatchAmount", obj.DispatchAmount)
                    clsCommon.AddColumnsForChange(coll, "InvoiceQty", obj.InvoiceQty)
                    clsCommon.AddColumnsForChange(coll, "InvoiceFatPer", obj.InvoiceFatPer)
                    clsCommon.AddColumnsForChange(coll, "InvoiceSNFPer", obj.InvoiceSNFPer)
                    clsCommon.AddColumnsForChange(coll, "InvoiceRate", obj.InvoiceRate)
                    clsCommon.AddColumnsForChange(coll, "InvoiceAmount", obj.InvoiceAmount)
                    clsCommon.AddColumnsForChange(coll, "InvoiceFatKG", obj.InvoiceFatKG)
                    clsCommon.AddColumnsForChange(coll, "InvoiceSNFKG", obj.InvoiceSNFKG)
                    clsCommon.AddColumnsForChange(coll, "DispatchQty_in_Ltr", obj.DispatchQty_in_Ltr)
                    clsCommon.AddColumnsForChange(coll, "InvoiceQty_in_Ltr", obj.InvoiceQty_in_Ltr)
                    clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
                    clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                    clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)

                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALE_RETURN_DETAIL_BULKSALE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of ClsSaleReturnDetailBulkSale)
        Try
            Dim arrObj As List(Of ClsSaleReturnDetailBulkSale) = Nothing
            Dim obj As ClsSaleReturnDetailBulkSale = Nothing
            Dim qry As String = "select * from TSPL_SALE_RETURN_DETAIL_BULKSALE where Document_No='" & strDocNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of ClsSaleReturnDetailBulkSale)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New ClsSaleReturnDetailBulkSale()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.Dispatch_Code = clsCommon.myCstr(dt.Rows(i)("Dispatch_Code"))
                    obj.Dispatch_Date = clsCommon.myCstr(dt.Rows(i)("Dispatch_Date"))
                    obj.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                    obj.Unit_code = clsCommon.myCstr(dt.Rows(i)("Unit_Code"))
                    obj.Tanker_Code = clsCommon.myCstr(dt.Rows(i)("Tanker_Code"))
                    obj.DispatchQty = clsCommon.myCdbl(dt.Rows(i)("DispatchQty"))
                    obj.DispatchFatPer = clsCommon.myCdbl(dt.Rows(i)("DispatchFatPer"))
                    obj.DispatchSNFPer = clsCommon.myCdbl(dt.Rows(i)("DispatchSNFPer"))
                    obj.DispatchRate = clsCommon.myCdbl(dt.Rows(i)("DispatchRate"))
                    obj.DispatchAmount = clsCommon.myCdbl(dt.Rows(i)("DispatchAmount"))
                    obj.InvoiceQty = clsCommon.myCdbl(dt.Rows(i)("InvoiceQty"))
                    obj.InvoiceFatPer = clsCommon.myCdbl(dt.Rows(i)("InvoiceFatPer"))
                    obj.InvoiceSNFPer = clsCommon.myCdbl(dt.Rows(i)("InvoiceSNFPer"))
                    obj.InvoiceRate = clsCommon.myCdbl(dt.Rows(i)("InvoiceRate"))
                    obj.InvoiceAmount = clsCommon.myCdbl(dt.Rows(i)("InvoiceAmount"))
                    obj.InvoiceFatKG = clsCommon.myCdbl(dt.Rows(i)("InvoiceFatKG"))
                    obj.InvoiceSNFKG = clsCommon.myCdbl(dt.Rows(i)("InvoiceSNFKG"))
                    obj.DispatchQty_in_Ltr = clsCommon.myCdbl(dt.Rows(i)("DispatchQty_in_Ltr"))
                    obj.InvoiceQty_in_Ltr = clsCommon.myCdbl(dt.Rows(i)("InvoiceQty_in_Ltr"))
                    obj.TAX1 = clsCommon.myCstr(dt.Rows(i)("TAX1"))
                    obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX1_Base_Amt"))
                    obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX1_Rate"))
                    obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX1_Amt"))
                    obj.TAX2 = clsCommon.myCstr(dt.Rows(i)("TAX2"))
                    obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX2_Base_Amt"))
                    obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX2_Rate"))
                    obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX2_Amt"))
                    obj.TAX3 = clsCommon.myCstr(dt.Rows(i)("TAX3"))
                    obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX3_Base_Amt"))
                    obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX3_Rate"))
                    obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX3_Amt"))
                    obj.TAX4 = clsCommon.myCstr(dt.Rows(i)("TAX4"))
                    obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX4_Base_Amt"))
                    obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX4_Rate"))
                    obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX4_Amt"))
                    obj.TAX5 = clsCommon.myCstr(dt.Rows(i)("TAX5"))
                    obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX5_Base_Amt"))
                    obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX5_Rate"))
                    obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX5_Amt"))
                    obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(i)("Total_Tax_Amt"))
                    obj.Item_Net_Amt = clsCommon.myCdbl(dt.Rows(i)("Item_Net_Amt"))
                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

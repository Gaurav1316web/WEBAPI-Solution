Imports common
Imports System.Data.SqlClient
Imports System.Data
Public Class clsAssetScrapSaleHead
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As Date
    Public cust_Code As String = Nothing
    Public ven_Code As String = Nothing
    Public cust_Name As String = Nothing

    Public Status As ERPTransactionStatus

    Public Posting_Date As String = Nothing
    Public Loc_Code As String = Nothing
    Public Loc_Name As String = Nothing


    Public Against_Vendor As Integer = 0
    Public Description As String = Nothing
    Public reff As String = Nothing
    Public Tax_Group As String = Nothing
    Public Tax_Desc As String = Nothing
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
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Double = 0
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Double = 0
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Double = 0
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Double = 0
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Double = 0
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Amt As Double = 0
    Public AddCode1 As String = Nothing
    Public AddDesc1 As String = Nothing
    Public AddAmt1 As Double = 0
    Public AddCode2 As String = Nothing
    Public AddDesc2 As String = Nothing
    Public AddAmt2 As Double = 0
    Public AddCode3 As String = Nothing
    Public AddDesc3 As String = Nothing
    Public AddAmt3 As Double = 0
    Public AddCode4 As String = Nothing
    Public AddDesc4 As String = Nothing
    Public AddAmt4 As Double = 0
    Public AddCode5 As String = Nothing
    Public AddDesc5 As String = Nothing
    Public AddAmt5 As Double = 0
    Public AddCode6 As String = Nothing
    Public AddDesc6 As String = Nothing
    Public AddAmt6 As Double = 0
    Public AddCode7 As String = Nothing
    Public AddDesc7 As String = Nothing
    Public AddAmt7 As Double = 0
    Public AddCode8 As String = Nothing
    Public AddDesc8 As String = Nothing
    Public AddAmt8 As Double = 0
    Public AddCode9 As String = Nothing
    Public AddDesc9 As String = Nothing
    Public AddAmt9 As Double = 0
    Public AddCode10 As String = Nothing
    Public AddDesc10 As String = Nothing
    Public AddAmt10 As Double = 0
    Public Discount_Base As Double = 0
    Public Discount_Amt As Double = 0
    Public Amount_Less_Discount As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Total_Add_Amt As Double = 0
    Public Doc_Amt As Double = 0




    Public Terms_Code As String = Nothing
    Public Due_Date As String = Nothing

    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public Comp_Code As String = Nothing

    Public Arr As List(Of clsAssetScrapSaleDetail) = Nothing
    Public Against_Scrap As Integer = 0

#End Region


    Public Function SaveData(ByVal obj As clsAssetScrapSaleHead, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Fixed Asset", "Disposal Entry", obj.Loc_Code, obj.Document_Date, trans)
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_ASSET_SCRAP_HEAD", "Document_No", "TSPL_ASSET_SCRAP_DETAIL", "Document_No", trans)
            End If
            Dim qry As String = "delete from TSPL_ASSET_SCRAP_DETAIL where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date, "dd/MMM/yyyy  hh:mm tt"), clsDocType.DisposalEntry, "", obj.Loc_Code)
            End If
            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "cust_Code", obj.cust_Code, True)

            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt "))
            clsCommon.AddColumnsForChange(coll, "posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Loc_Code", obj.Loc_Code)

            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "reff", obj.reff)
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Tax_Desc", obj.Tax_Desc)
            clsCommon.AddColumnsForChange(coll, "Total_Add_Amt", obj.Total_Add_Amt)

            clsCommon.AddColumnsForChange(coll, "Discount_Base", obj.Discount_Base)
            clsCommon.AddColumnsForChange(coll, "Discount_Amt", obj.Discount_Amt)
            clsCommon.AddColumnsForChange(coll, "Amount_Less_Discount", obj.Amount_Less_Discount)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "doc_Amt", obj.Doc_Amt)

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
            clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
            clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
            clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
            clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
            clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
            clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
            clsCommon.AddColumnsForChange(coll, "AddCode1", obj.AddCode1)
            clsCommon.AddColumnsForChange(coll, "AddDesc1", obj.AddDesc1)
            clsCommon.AddColumnsForChange(coll, "AddAmt1", obj.AddAmt1)
            clsCommon.AddColumnsForChange(coll, "AddCode2", obj.AddCode2)
            clsCommon.AddColumnsForChange(coll, "AddDesc2", obj.AddDesc2)
            clsCommon.AddColumnsForChange(coll, "AddAmt2", obj.AddAmt2)
            clsCommon.AddColumnsForChange(coll, "AddCode3", obj.AddCode3)
            clsCommon.AddColumnsForChange(coll, "AddDesc3", obj.AddDesc3)
            clsCommon.AddColumnsForChange(coll, "AddAmt3", obj.AddAmt3)
            clsCommon.AddColumnsForChange(coll, "AddCode4", obj.AddCode4)
            clsCommon.AddColumnsForChange(coll, "AddDesc4", obj.AddDesc4)
            clsCommon.AddColumnsForChange(coll, "AddAmt4", obj.AddAmt4)
            clsCommon.AddColumnsForChange(coll, "AddCode5", obj.AddCode5)
            clsCommon.AddColumnsForChange(coll, "AddDesc5", obj.AddDesc5)
            clsCommon.AddColumnsForChange(coll, "AddAmt5", obj.AddAmt5)
            clsCommon.AddColumnsForChange(coll, "AddCode6", obj.AddCode6)
            clsCommon.AddColumnsForChange(coll, "AddDesc6", obj.AddDesc6)
            clsCommon.AddColumnsForChange(coll, "AddAmt6", obj.AddAmt6)
            clsCommon.AddColumnsForChange(coll, "AddCode7", obj.AddCode7)
            clsCommon.AddColumnsForChange(coll, "AddDesc7", obj.AddDesc7)
            clsCommon.AddColumnsForChange(coll, "AddAmt7", obj.AddAmt7)
            clsCommon.AddColumnsForChange(coll, "AddCode8", obj.AddCode8)
            clsCommon.AddColumnsForChange(coll, "AddDesc8", obj.AddDesc8)
            clsCommon.AddColumnsForChange(coll, "AddAmt8", obj.AddAmt8)
            clsCommon.AddColumnsForChange(coll, "AddCode9", obj.AddCode9)
            clsCommon.AddColumnsForChange(coll, "AddDesc9", obj.AddDesc9)
            clsCommon.AddColumnsForChange(coll, "AddAmt9", obj.AddAmt9)
            clsCommon.AddColumnsForChange(coll, "AddCode10", obj.AddCode10)
            clsCommon.AddColumnsForChange(coll, "AddDesc10", obj.AddDesc10)
            clsCommon.AddColumnsForChange(coll, "AddAmt10", obj.AddAmt10)
            clsCommon.AddColumnsForChange(coll, "against_vendor", obj.Against_Vendor, True)
            clsCommon.AddColumnsForChange(coll, "Against_Scrap", obj.Against_Scrap, True)

            If clsCommon.myLen(obj.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
        
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_SCRAP_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_SCRAP_HEAD", OMInsertOrUpdate.Update, "TSPL_ASSET_SCRAP_HEAD.Document_No='" + obj.Document_No + "'", trans)
            End If

            isSaved = isSaved AndAlso clsAssetScrapSaleDetail.SaveData(obj.Document_No, Arr, trans)
         
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsAssetScrapSaleHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsAssetScrapSaleHead
        Dim obj As clsAssetScrapSaleHead = Nothing
        Dim qry As String = "SELECT TSPL_ASSET_SCRAP_HEAD.*,TSPL_CUSTOMER_MASTER.Customer_Name as  cust_Name,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_VENDOR_MASTER .Vendor_Name as vendor_Name FROM TSPL_ASSET_SCRAP_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_ASSET_SCRAP_HEAD.cust_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_ASSET_SCRAP_HEAD.cust_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code= TSPL_ASSET_SCRAP_HEAD.Loc_Code left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_ASSET_SCRAP_HEAD.Terms_Code where 2=2"
        Dim whrCls As String = ""


        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND loc_code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_ASSET_SCRAP_HEAD.Document_No = (select MIN(Document_No) from TSPL_ASSET_SCRAP_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_ASSET_SCRAP_HEAD.Document_No = (select Max(Document_No) from TSPL_ASSET_SCRAP_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_ASSET_SCRAP_HEAD.Document_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_ASSET_SCRAP_HEAD.Document_No = (select Min(Document_No) from TSPL_ASSET_SCRAP_HEAD where Document_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_ASSET_SCRAP_HEAD.Document_No = (select Max(Document_No) from TSPL_ASSET_SCRAP_HEAD where Document_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsAssetScrapSaleHead()
            obj.Against_Vendor = clsCommon.myCdbl(dt.Rows(0)("against_vendor"))
            obj.Against_Scrap = clsCommon.myCdbl(dt.Rows(0)("Against_Scrap"))
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.cust_Code = clsCommon.myCstr(dt.Rows(0)("cust_Code"))
            obj.cust_Name = clsCommon.myCstr(dt.Rows(0)("cust_Name"))
            obj.ven_Code = clsCommon.myCstr(dt.Rows(0)("vendor_Name"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("posting_Date"))
            obj.Loc_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
            obj.Loc_Name = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.reff = clsCommon.myCstr(dt.Rows(0)("reff"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.Tax_Desc = clsCommon.myCstr(dt.Rows(0)("Tax_Desc"))
            obj.Total_Add_Amt = clsCommon.myCstr(dt.Rows(0)("Total_Add_Amt"))
            obj.Discount_Base = clsCommon.myCstr(dt.Rows(0)("Discount_Base"))
            obj.Discount_Amt = clsCommon.myCstr(dt.Rows(0)("Discount_Amt"))
            obj.Amount_Less_Discount = clsCommon.myCstr(dt.Rows(0)("Amount_Less_Discount"))
            obj.Total_Tax_Amt = clsCommon.myCstr(dt.Rows(0)("Total_tax_amt"))
            obj.Doc_Amt = clsCommon.myCstr(dt.Rows(0)("Doc_Amt"))
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
            obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("TAX6"))
            obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
            obj.TAX6_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Base_Amt"))
            obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt"))
            obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("TAX7"))
            obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
            obj.TAX7_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Base_Amt"))
            obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt"))
            obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("TAX8"))
            obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
            obj.TAX8_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Base_Amt"))
            obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt"))
            obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("TAX9"))
            obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))
            obj.TAX9_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Base_Amt"))
            obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt"))
            obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("TAX10"))
            obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
            obj.TAX10_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Base_Amt"))
            obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt"))
            obj.AddCode1 = clsCommon.myCstr(dt.Rows(0)("AddCode1"))
            obj.AddDesc1 = clsCommon.myCstr(dt.Rows(0)("AddDesc1"))
            obj.AddAmt1 = clsCommon.myCdbl(dt.Rows(0)("AddAmt1"))
            obj.AddCode2 = clsCommon.myCstr(dt.Rows(0)("AddCode2"))
            obj.AddDesc2 = clsCommon.myCstr(dt.Rows(0)("AddDesc2"))
            obj.AddAmt2 = clsCommon.myCdbl(dt.Rows(0)("AddAmt2"))
            obj.AddCode3 = clsCommon.myCstr(dt.Rows(0)("AddCode3"))
            obj.AddDesc3 = clsCommon.myCstr(dt.Rows(0)("AddDesc3"))
            obj.AddAmt3 = clsCommon.myCdbl(dt.Rows(0)("AddAmt3"))
            obj.AddCode4 = clsCommon.myCstr(dt.Rows(0)("AddCode4"))
            obj.AddDesc4 = clsCommon.myCstr(dt.Rows(0)("AddDesc4"))
            obj.AddAmt4 = clsCommon.myCdbl(dt.Rows(0)("AddAmt4"))
            obj.AddCode5 = clsCommon.myCstr(dt.Rows(0)("AddCode5"))
            obj.AddDesc5 = clsCommon.myCstr(dt.Rows(0)("AddDesc5"))
            obj.AddAmt5 = clsCommon.myCdbl(dt.Rows(0)("AddAmt5"))
            obj.AddCode6 = clsCommon.myCstr(dt.Rows(0)("AddCode6"))
            obj.AddDesc6 = clsCommon.myCstr(dt.Rows(0)("AddDesc6"))
            obj.AddAmt6 = clsCommon.myCdbl(dt.Rows(0)("AddAmt6"))
            obj.AddCode7 = clsCommon.myCstr(dt.Rows(0)("AddCode7"))
            obj.AddDesc7 = clsCommon.myCstr(dt.Rows(0)("AddDesc7"))
            obj.AddAmt7 = clsCommon.myCdbl(dt.Rows(0)("AddAmt7"))
            obj.AddCode8 = clsCommon.myCstr(dt.Rows(0)("AddCode8"))
            obj.AddDesc8 = clsCommon.myCstr(dt.Rows(0)("AddDesc8"))
            obj.AddAmt8 = clsCommon.myCdbl(dt.Rows(0)("AddAmt8"))
            obj.AddCode9 = clsCommon.myCstr(dt.Rows(0)("AddCode9"))
            obj.AddDesc9 = clsCommon.myCstr(dt.Rows(0)("AddDesc9"))
            obj.AddAmt9 = clsCommon.myCdbl(dt.Rows(0)("AddAmt9"))
            obj.AddCode10 = clsCommon.myCstr(dt.Rows(0)("AddCode10"))
            obj.AddDesc10 = clsCommon.myCstr(dt.Rows(0)("AddDesc10"))
            obj.AddAmt10 = clsCommon.myCdbl(dt.Rows(0)("AddAmt10"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
            obj.Total_Add_Amt = obj.AddAmt1 + obj.AddAmt2 + obj.AddAmt3 + obj.AddAmt4 + obj.AddAmt5 + obj.AddAmt6 + obj.AddAmt7 + obj.AddAmt8 + obj.AddAmt9 + obj.AddAmt10

            qry = "SELECT TSPL_ASSET_SCRAP_DETAIL.* ,TSPL_ACQUISITION_DETAIL.Asset_Name ,TSPL_ITEM_MASTER.Item_Desc"
            qry += " FROM TSPL_ASSET_SCRAP_DETAIL "
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ASSET_SCRAP_DETAIL.Item_Code"
            qry += " left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_DETAIL.Asset_Code=TSPL_ASSET_SCRAP_DETAIL.Asset_Code"
            qry += " where TSPL_ASSET_SCRAP_DETAIL.Document_No='" + obj.Document_No + "' "
            qry += " ORDER BY TSPL_ASSET_SCRAP_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsAssetScrapSaleDetail)
                Dim objTr As clsAssetScrapSaleDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsAssetScrapSaleDetail
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Asset_Code = clsCommon.myCstr(dr("Asset_Code"))
                    objTr.Asset_Name = clsCommon.myCstr(dr("Asset_Name"))
                    objTr.Hirerachy_Code = clsCommon.myCstr(dr("Hirerachy_Code"))
                    objTr.CostCenter_Code = clsCommon.myCstr(dr("CostCenter_Code"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.UOM = clsCommon.myCstr(dr("UOM"))
                    objTr.Rate = clsCommon.myCdbl(dr("Rate"))
                    objTr.Amt = clsCommon.myCdbl(dr("Amt"))
                    objTr.Discount_Per = clsCommon.myCdbl(dr("Discount_Per"))
                    objTr.Discount_Amt = clsCommon.myCdbl(dr("Discount_Amt"))
                    objTr.Amt_After_Discount = clsCommon.myCdbl(dr("Amt_After_Discount"))
                    objTr.TAX1 = clsCommon.myCstr(dr("TAX1"))
                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                    objTr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                    objTr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                    objTr.TAX2 = clsCommon.myCstr(dr("TAX2"))
                    objTr.TAX2_Base_Amt = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                    objTr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                    objTr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                    objTr.TAX3 = clsCommon.myCstr(dr("TAX3"))
                    objTr.TAX3_Base_Amt = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                    objTr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                    objTr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                    objTr.TAX4 = clsCommon.myCstr(dr("TAX4"))
                    objTr.TAX4_Base_Amt = clsCommon.myCdbl(dr("TAX4_Base_Amt"))
                    objTr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                    objTr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                    objTr.TAX5 = clsCommon.myCstr(dr("TAX5"))
                    objTr.TAX5_Base_Amt = clsCommon.myCdbl(dr("TAX5_Base_Amt"))
                    objTr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                    objTr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                    objTr.TAX6 = clsCommon.myCstr(dr("TAX6"))
                    objTr.TAX6_Base_Amt = clsCommon.myCdbl(dr("TAX6_Base_Amt"))
                    objTr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                    objTr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                    objTr.TAX7 = clsCommon.myCstr(dr("TAX7"))
                    objTr.TAX7_Base_Amt = clsCommon.myCdbl(dr("TAX7_Base_Amt"))
                    objTr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                    objTr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                    objTr.TAX8 = clsCommon.myCstr(dr("TAX8"))
                    objTr.TAX8_Base_Amt = clsCommon.myCdbl(dr("TAX8_Base_Amt"))
                    objTr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                    objTr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                    objTr.TAX9 = clsCommon.myCstr(dr("TAX9"))
                    objTr.TAX9_Base_Amt = clsCommon.myCdbl(dr("TAX9_Base_Amt"))
                    objTr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                    objTr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                    objTr.TAX10 = clsCommon.myCstr(dr("TAX10"))
                    objTr.TAX10_Base_Amt = clsCommon.myCdbl(dr("TAX10_Base_Amt"))
                    objTr.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                    objTr.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
                    objTr.Tax_Amt = clsCommon.myCdbl(dr("Tax_Amt"))
                    objTr.Amt_After_Tax = clsCommon.myCdbl(dr("Amt_After_Tax"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            Dim isscrap As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Shipment No not found to Post")
            End If
            Dim obj As clsAssetScrapSaleHead = clsAssetScrapSaleHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            Dim ECustomerType = clsERPFuncationality.GetCustomerEInvoiceType(obj.cust_Code, trans)

            Dim qry As String = "Update TSPL_ASSET_SCRAP_HEAD set Status=1, Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt") + "',Modify_By='" + objCommonVar.CurrentUserCode + "',EInvoice_Type='" + ECustomerType + "'"
            qry += " where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


            '==================================Added by preeti gupta===============================
            For Each objtr As clsAssetScrapSaleDetail In obj.Arr
                If clsCommon.myLen(objtr.Asset_Code) > 0 Then
                    'Dim objProcess As New clsDepreciationCalculation()
                    'objProcess.GetDepreciationProcess(obj.Document_Date, obj.Document_No, False, obj.Loc_Code, objtr.Asset_Code, trans)

                    Dim arrLoc As New ArrayList()
                    arrLoc.Add(obj.Loc_Code)

                    Dim arrAsset As New ArrayList()
                    arrAsset.Add(objtr.Asset_Code)
                    clsAssetDepreciation.GetDepreciationCal(Nothing, True, obj.Document_Date, False, arrLoc, arrAsset, Nothing, Nothing, Nothing, trans)
                End If
            Next

            '========================================================================================

            CreateJE(obj, trans, "", "")


            ''richa agarwal 08 Mar,2021 check eInvoice Implementation

            If clsCommon.myLen(clsCommon.myCstr(obj.Tax_Group)) > 0 Then
                Dim isTaxTaxable As String = "N"
                isTaxTaxable = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select 'Y' from TSPL_TAX_GROUP_MASTER where Tax_Group_Code ='" & obj.Tax_Group & "' and Is_Tax_Exempted =0 and Tax_Group_Type ='S'", trans))
                If clsCommon.CompairString(ECustomerType, "BB") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(isTaxTaxable), "Y") = CompairStringResult.Equal AndAlso clsERPFuncationality.GetEInvoiceStatus(obj.Document_Date, trans) = True Then
                    If clsAssetScrapSaleHead.EInvoice_Implementation(obj.Document_No, obj.Loc_Code, trans) = True Then
                    Else
                        Throw New Exception("Invalid JSON Value")
                    End If
                End If
            End If



            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_ASSET_SCRAP_HEAD", "Document_No", trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CreateJE(ByVal obj As clsAssetScrapSaleHead, ByVal trans As SqlTransaction, ByVal strARNoForRecreate As String, ByVal strVoucherNoRecreate As String)
        'Create GL Entry
        Dim ArryLst As ArrayList = New ArrayList()
        '--------------Code For GL Entry
        If obj.Against_Scrap = 0 AndAlso clsCommon.myLen(obj.cust_Code) > 0 Then
            createARInvoice(obj, trans, strARNoForRecreate, strVoucherNoRecreate, "")
        Else
            '' GL changed by Panch Raj
            Dim qryAsset As String = clsAssetScrapSaleHead.GetAssetDisposalJEQuery(obj.Document_No)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryAsset, trans)
            For Each dr As DataRow In dt.Rows

                Dim Ac_Accum_Dep As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr.Item("Ac_Accum_Dep")), obj.Loc_Code, trans)
                Dim Ac_Control As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr.Item("Ac_Control")), obj.Loc_Code, trans)
                Dim PROFIT_AC As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr.Item("PROFIT_AC")), obj.Loc_Code, trans)
                Dim LOSS_AC As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr.Item("LOSS_AC")), obj.Loc_Code, trans)
                Dim strcostCentre As String = clsCommon.myCstr(dr.Item("CostCenter_Code"))
                Dim strHirerachy_Code As String = clsCommon.myCstr(dr.Item("Hirerachy_Code"))

                Dim Book_Source_value As Decimal = clsCommon.myCdbl(dr.Item("Book_Source_value"))
                Dim Sale_Amount As Decimal = clsCommon.myCdbl(dr.Item("Sale_Amount"))
                Dim Final_Dep_Amount As Decimal = clsCommon.myCdbl(dr.Item("Final_Dep_Amount"))

                '' Accumulated Depreciation a/c debit with depreciation amt
                Dim Acc2() As String = {Ac_Accum_Dep, Final_Dep_Amount, "", "", strHirerachy_Code, strcostCentre}
                ArryLst.Add(Acc2)

                '' Assets Control a/c credit with book source value 
                Dim Acc3() As String = {Ac_Control, -1 * Book_Source_value, "", "", strHirerachy_Code, strcostCentre}
                ArryLst.Add(Acc3)

                If (Book_Source_value - Final_Dep_Amount) > 0 Then
                    '' loss a/c debit with depreciation amt
                    Dim Acc5() As String = {LOSS_AC, (Book_Source_value - Final_Dep_Amount), "", "", strHirerachy_Code, strcostCentre}
                    ArryLst.Add(Acc5)
                End If

            Next
            transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, strVoucherNoRecreate, trans, obj.Document_Date, "Disposal Entry no -" + obj.Document_No + "", "AM-DE", "Disposal Entry", obj.Document_No, "", "C", obj.cust_Code, obj.cust_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
            '==========================
            If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyFinancialCostCenter, clsFixedParameterCode.ApplyFinancialCostCenter, trans)) = "1", True, False)) = True Then
                Dim strCostCenterCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Top 1 CostCenter_Code from TSPL_ASSET_SCRAP_DETAIL where Document_No = '" + obj.Document_No + "'", trans))
                Dim strHirerachyCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Top 1 Hirerachy_Code from TSPL_ASSET_SCRAP_DETAIL where Document_No =  '" + obj.Document_No + "'", trans))
                clsERPFuncationality.UpdateCostCenterAndHirerachyCodeOnJE(obj.Document_No, "AM-DE", strCostCenterCode, strHirerachyCode, trans)
            End If
        End If
        Return True
    End Function

    Public Shared Function EInvoice_Implementation(ByVal strDocNo As String, ByVal strLocation As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If

            Dim strtoken As String = ClsEInvoiceOFAPIs.IsGenerateAuthTokenNo_Required(objCommonVar.CurrentCompanyCode, strLocation, trans)
            If clsCommon.myLen(strtoken) > 0 Then
                Dim strQry As String = "select TSPL_Customer_master .Cust_Code ,TSPL_ASSET_SCRAP_HEAD.Document_No as DocNo,convert(date,TSPL_ASSET_SCRAP_HEAD.Document_Date,103) as DocDate,case when TSPL_Customer_Invoice_Head.Document_Type='D' then 'DBN' when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CRN' else 'INV' end as DocType ,'B2B' as SupTyp, 'N'  as IgstOnIntra,Bill_To_Location.GSTNO as SellerGSTINNo ,Bill_To_Location.location_desc as SellerLglNm,TSPL_COMPANY_MASTER.Comp_Name as SellerTrdNm,Bill_To_Location.Add1 as SellerAdd1,Bill_To_Location.Add2 as SellerAdd2 ,Bill_To_Location.City_Code  as SellerLoc,Bill_To_Location.Pin_Code  as SellerPincode,BillToLocation_State_Master.GST_STATE_Code as SellerStcd,Bill_To_Location.Phone1 as SellerPhone,Bill_To_Location.Email as SellerEmail,TSPL_Customer_master.GSTNo as BuyerGSTINNo ,TSPL_Customer_master.Customer_Name as BuyerLglNm,TSPL_Customer_master.Alies_name as BuyerTrdNm, Customer_State_Master.GST_STATE_Code as BuyerPOS,TSPL_Customer_master.Add1 as BuyerAdd1,TSPL_Customer_master.Add2 as BuyerAdd2 ,tspl_city_master.City_Name as BuyerLoc,cast(TSPL_Customer_master.PIN_NO as int) as BuyerPincode,Customer_State_Master.GST_STATE_Code as BuyerStcd,TSPL_Customer_master.Phone1 as BuyerPhone,TSPL_Customer_master.Email as BuyerEmail,TSPL_ASSET_SCRAP_DETAIL.Line_No as ItemSlNo,'N' as ItemIsServc,COAlESCE(TSPL_ITEM_MASTER.Item_Desc,TSPL_Additional_Charges.DESCRIPTION ) AS ItemPrdDesc,COAlESCE(TSPL_ITEM_MASTER.HSN_Code,TSPL_Additional_Charges.SAC_CODE) AS ItemHsnCd,TSPL_ASSET_SCRAP_DETAIL.Qty as ItemQty, TSPL_ASSET_SCRAP_DETAIL.UOM as ItemUnit,TSPL_ASSET_SCRAP_DETAIL.Rate as ItemUnitPrice,TSPL_ASSET_SCRAP_DETAIL.Amt as ItemTotAmt,TSPL_ASSET_SCRAP_DETAIL.Discount_Amt as ItemDiscount,TSPL_ASSET_SCRAP_DETAIL.Amt_After_Discount as ItemAssAmt,case when ISNULL(TSPL_ASSET_SCRAP_DETAIL .tax1,'') ='IGST' THEN TSPL_ASSET_SCRAP_DETAIL.TAX1_Rate when ISNULL(TSPL_ASSET_SCRAP_DETAIL .tax1,'') ='CGST' AND   ISNULL(TSPL_ASSET_SCRAP_DETAIL .tax2,'') ='SGST'  THEN TSPL_ASSET_SCRAP_DETAIL.TAX1_Rate+TSPL_ASSET_SCRAP_DETAIL.TAX2_Rate  ELSE 0 end as ItemGstRt, case when TSPL_ASSET_SCRAP_DETAIL .TAX1 ='SGST' AND TSPL_ASSET_SCRAP_DETAIL .TAX2  ='CGST' then TSPL_ASSET_SCRAP_DETAIL.TAX1_Amt when TSPL_ASSET_SCRAP_DETAIL .TAX1 ='CGST' AND TSPL_ASSET_SCRAP_DETAIL .TAX2  ='SGST' then TSPL_ASSET_SCRAP_DETAIL.TAX2_Amt else 0 end ItemSgstAmt,case when TSPL_ASSET_SCRAP_DETAIL .TAX1 ='IGST' then TSPL_ASSET_SCRAP_DETAIL.TAX1_Amt else 0 end ItemIgstAmt,
case when TSPL_ASSET_SCRAP_DETAIL .TAX1 ='CGST' AND TSPL_ASSET_SCRAP_DETAIL .TAX2  ='SGST' then TSPL_ASSET_SCRAP_DETAIL.TAX1_Amt when TSPL_ASSET_SCRAP_DETAIL .TAX1 ='SGST' AND TSPL_ASSET_SCRAP_DETAIL .TAX2  ='CGST' then TSPL_ASSET_SCRAP_DETAIL.TAX2_Amt else 0 end ItemCgstAmt,0 as ItemOthChrg,TSPL_ASSET_SCRAP_DETAIL.AMT_After_DISCOUNT+TSPL_ASSET_SCRAP_DETAIL.TAX1_AMT +case when isnull(TCS1.is_tcs,'')<>'Y' THEN  TSPL_ASSET_SCRAP_DETAIL.TAX2_AMT  ELSE 0 END  +case when isnull(TCS2.is_tcs,'')<>'Y' THEN  TSPL_ASSET_SCRAP_DETAIL.TAX3_AMT  ELSE 0 END as ItemTotItemVal,TSPL_ASSET_SCRAP_HEAD .Amount_Less_Discount as ValDtlsAssVal,case when TSPL_ASSET_SCRAP_HEAD .TAX1 ='CGST' AND TSPL_ASSET_SCRAP_HEAD .TAX2  ='SGST' then TSPL_ASSET_SCRAP_HEAD.TAX1_Amt when TSPL_ASSET_SCRAP_HEAD .TAX1 ='SGST' AND TSPL_ASSET_SCRAP_HEAD .TAX2  ='CGST' then TSPL_ASSET_SCRAP_HEAD.TAX2_Amt else 0 end ValDtlsCgstVal, case when TSPL_ASSET_SCRAP_HEAD .TAX1 ='SGST' AND TSPL_ASSET_SCRAP_HEAD .TAX2  ='CGST' then TSPL_ASSET_SCRAP_HEAD.TAX1_Amt when TSPL_ASSET_SCRAP_HEAD .TAX1 ='CGST' AND TSPL_ASSET_SCRAP_HEAD .TAX2  ='SGST' then TSPL_ASSET_SCRAP_HEAD.TAX2_Amt else 0 end ValDtlsSgstVal,case when TSPL_ASSET_SCRAP_HEAD .TAX1 ='IGST' then TSPL_ASSET_SCRAP_HEAD.TAX1_Amt else 0 end ValDtlsIgstVal,TSPL_ASSET_SCRAP_HEAD.Discount_Amt as ValDtlsDiscount,case when isnull(TCS1.is_tcs,'')='Y' THEN  TSPL_ASSET_SCRAP_HEAD.TAX2_AMT when isnull(TCS2.is_tcs,'')='Y' THEN  TSPL_ASSET_SCRAP_HEAD.TAX3_AMT ELSE 0 END as ValDtlsOthChrg,TSPL_ASSET_SCRAP_HEAD.Doc_Amt as ValDtlsTotInvVal,case when isnull(AddCode1,'')='ROUND OFF' then  TSPL_ASSET_SCRAP_HEAD.addAmt1 else 0 end  as ValDtlsRndOffAmt
from TSPL_ASSET_SCRAP_HEAD
Left Outer Join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.against_asset_disposal =TSPL_ASSET_SCRAP_HEAD.Document_No
Left Outer Join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code  ='" & objCommonVar.CurrentCompanyCode & "'
Left Outer Join TSPL_Customer_master on TSPL_Customer_master.Cust_Code  =TSPL_ASSET_SCRAP_HEAD.Cust_Code
left Outer Join TSPL_LOCATION_MASTER as Bill_To_Location on Bill_To_Location.Location_Code =TSPL_ASSET_SCRAP_HEAD.Loc_Code 
left outer join TSPL_ASSET_SCRAP_DETAIL on TSPL_ASSET_SCRAP_DETAIL.Document_No=TSPL_ASSET_SCRAP_HEAD.Document_No
left outer join tspl_item_master on tspl_item_master.Item_code=TSPL_ASSET_SCRAP_DETAIL.Item_code
left outer join TSPL_ADDITIONAL_CHARGES on TSPL_ADDITIONAL_CHARGES.CODE=TSPL_ASSET_SCRAP_DETAIL.Item_code
left outer join TSPL_STATE_MASTER as BillToLocation_State_Master on BillToLocation_State_Master.STATE_CODE  =Bill_To_Location.State
left outer join TSPL_STATE_MASTER as Customer_State_Master on Customer_State_Master.STATE_CODE  =TSPL_Customer_master.State 
left outer join tspl_city_master on tspl_city_master.city_code=TSPL_Customer_master.City_Code
left outer join tspl_tax_master as TCS1 on TCS1.Tax_Code =TSPL_ASSET_SCRAP_HEAD.Tax2
left outer join tspl_tax_master as TCS2 on TCS2.Tax_Code =TSPL_ASSET_SCRAP_HEAD.Tax3
where TSPL_ASSET_SCRAP_HEAD.Document_No ='" & strDocNo & "'"


                Dim objResult As Object = ClsEInvoiceOFAPIs.PostAuthTokenNo_withInvoiceData(objCommonVar.CurrentCompanyCode, strtoken, strQry, strLocation, trans)
                If objResult IsNot Nothing Then
                    'assign to variable
                    Dim AckNo As String = objResult.SelectToken("AckNo").ToString
                    Dim AckDt As String = objResult.SelectToken("AckDt").ToString
                    Dim Irn As String = objResult.SelectToken("Irn").ToString
                    Dim SignedQRCode As String = objResult.SelectToken("SignedQRCode").ToString
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_ASSET_SCRAP_HEAD set  IRN_No ='" & Irn & "',qr_code='" & SignedQRCode & "',ack_no='" & AckNo & "',ack_date='" & clsCommon.GetPrintDate(AckDt, "dd/MMM/yyyy hh:mm tt") & "' where Document_No ='" & strDocNo & "'", trans)

                    Dim TempByte As Byte() = clsERPFuncationality.GenerateMyQCCode(SignedQRCode)
                    clsDBFuncationality.UpdateImage("BarCode_Img", TempByte, "TSPL_ASSET_SCRAP_HEAD", "TSPL_ASSET_SCRAP_HEAD.Document_No='" & strDocNo & "'", trans)

                    'If objCommonVar.GenerateEWayBillWithEInvoice = True Then
                    '    Dim EwbNo As String = objResult.SelectToken("EwbNo").ToString
                    '    Dim EwbDt As String = objResult.SelectToken("EwbDt").ToString
                    '    Dim EwbValidTill As String = objResult.SelectToken("EwbValidTill").ToString
                    '    Dim Remarks As String = objResult.SelectToken("Remarks").ToString
                    '    If clsCommon.myLen(EwbDt) > 0 Then
                    '        EwbDt = clsCommon.GetPrintDate(EwbDt, "dd/MMM/yyyy hh:mm tt")
                    '    End If
                    '    If clsCommon.myLen(EwbValidTill) > 0 Then
                    '        EwbValidTill = clsCommon.GetPrintDate(EwbValidTill, "dd/MMM/yyyy hh:mm tt")
                    '    End If
                    '    clsDBFuncationality.ExecuteNonQuery("update TSPL_ASSET_SCRAP_HEAD set  EWayBillNo ='" & EwbNo & "',EwayBillDate=(CASE WHEN LEN('" & EwbDt & "')>0   THEN '" & EwbDt & "' ELSE NULL END) ,EwayBillValidDate=(CASE WHEN LEN('" & EwbValidTill & "')>0   THEN '" & EwbValidTill & "' ELSE NULL END)  , EWayBillRemarks = '" & Remarks & "'  where DOCUMENT_No ='" & strDocNo & "' ", trans)
                    'End If

                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetAssetDisposalJEQuery(ByVal Doc_No As String)
        '' Alert : dont rename the column names in this query. it is used in multiple places
        Dim qry As String = ""
        Dim DepQry As String = clsAssetDepreciation.GetAssetDepQuery()

        qry = " select ScrapD.Document_No,ScrapD.Asset_Code,ACQD.Book_Source_value,ACQD.Book_Source_Original_value,ScrapD.Amt_After_Discount as Sale_Amount,Dep.Final_Dep_Amount,Dep.Perm_Dep_Amount, " & Environment.NewLine &
              " Cust_Acc.Receivable_Control_acct, Dep_Acc.Disposal_Account, Dep_Acc.Disposal_Cost_Account, Dep_Acc.Ac_Dep_Account,Dep_Acc.Ac_Accum_Dep, Dep_Acc.Ac_Control, Dep_Acc.PROFIT_AC, Dep_Acc.LOSS_AC,ScrapD.CostCenter_Code,ScrapD.Hirerachy_Code " & Environment.NewLine &
              " from TSPL_ASSET_SCRAP_DETAIL ScrapD " & Environment.NewLine &
              " inner join TSPL_ASSET_SCRAP_HEAD ScrapH on ScrapD.Document_No=ScrapH.Document_No " & Environment.NewLine &
              " left join TSPL_ACQUISITION_DETAIL ACQD on ScrapD.Asset_Code=ACQD.Asset_Code " & Environment.NewLine &
              " left join (" & DepQry & ") as Dep on ScrapD.Asset_Code=Dep.Asset_Code " & Environment.NewLine &
              " left join TSPL_CUSTOMER_MASTER Cust on ScrapH.cust_Code=Cust.Cust_Code " & Environment.NewLine &
              " left join TSPL_CUSTOMER_ACCOUNT_SET Cust_Acc on Cust.Cust_Account=Cust_Acc.Cust_Account " & Environment.NewLine &
              " left join TSPL_Dep_AccountSet Dep_Acc on ACQD.AcSet_Code=Dep_Acc.AcSet_Code where 2=2 "
        If clsCommon.myLen(Doc_No) > 0 Then
            qry = qry & " and ScrapD.Document_No='" & Doc_No & "'"
        End If
        Return qry
    End Function
    Public Shared Function GetAssetDisposalJEQuery_ARInvoice(ByVal Doc_No As String)
        '' Alert : dont rename the column names in this query. it is used in multiple places
        Dim qry As String = ""
        Dim DepQry As String = clsAssetDepreciation.GetAssetDepQuery_ArInvoice()

        qry = " select ScrapD.Document_No,ScrapD.Asset_Code,ACQD.Book_Source_value,ACQD.Book_Source_Original_value,ScrapD.Amt_After_Discount as Sale_Amount,Dep.Final_Dep_Amount  + isnull(ACQD.depreciated_value,0) as Final_Dep_Amount ,Dep.Perm_Dep_Amount, " & Environment.NewLine &
              " Cust_Acc.Receivable_Control_acct, Dep_Acc.Disposal_Account, Dep_Acc.Disposal_Cost_Account, Dep_Acc.Ac_Dep_Account,Dep_Acc.Ac_Accum_Dep, Dep_Acc.Ac_Control, Dep_Acc.PROFIT_AC, Dep_Acc.LOSS_AC,ScrapD.CostCenter_Code,ScrapD.Hirerachy_Code " & Environment.NewLine &
              " from TSPL_ASSET_SCRAP_DETAIL ScrapD " & Environment.NewLine &
              " inner join TSPL_ASSET_SCRAP_HEAD ScrapH on ScrapD.Document_No=ScrapH.Document_No " & Environment.NewLine &
              " left join TSPL_ACQUISITION_DETAIL ACQD on ScrapD.Asset_Code=ACQD.Asset_Code " & Environment.NewLine &
              " left join (" & DepQry & ") as Dep on ScrapD.Asset_Code=Dep.Asset_Code " & Environment.NewLine &
              " left join TSPL_CUSTOMER_MASTER Cust on ScrapH.cust_Code=Cust.Cust_Code " & Environment.NewLine &
              " left join TSPL_CUSTOMER_ACCOUNT_SET Cust_Acc on Cust.Cust_Account=Cust_Acc.Cust_Account " & Environment.NewLine &
              " left join TSPL_Dep_AccountSet Dep_Acc on ACQD.AcSet_Code=Dep_Acc.AcSet_Code where 2=2 "
        If clsCommon.myLen(Doc_No) > 0 Then
            qry = qry & " and ScrapD.Document_No='" & Doc_No & "'"
        End If
        Return qry
    End Function
    Public Shared Function createARInvoice(ByVal obj As clsAssetScrapSaleHead, ByVal trans As SqlTransaction, ByVal strARNoForRecreate As String, ByVal strVoucherNoRecreate As String, ByVal strFormID As String) As Boolean
        ''''''''''''''''''''''''''''''''''For Making AR Invoice
        Dim objCustInv As New clsCustomerInvoiceHead()
        ''objCustInv.Document_No ''Will be Generateed
        objCustInv.RoundOffAmount = 0
        objCustInv.Document_Date = obj.Document_Date
        objCustInv.Document_Type = "I"
        objCustInv.Trans_Type = "ADE" '"PS"
        objCustInv.Document_Total = obj.Doc_Amt
        objCustInv.Customer_Code = obj.cust_Code
        objCustInv.Customer_Name = obj.cust_Name
        objCustInv.Posting_Date = obj.Document_Date
        Dim qry As String = " select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.cust_Code + "'"
        objCustInv.Account_Set = clsDBFuncationality.getSingleValue(qry, trans)
        ''objCustInv.Order_No
        objCustInv.loc_code = clsLocation.GetSegmentCode(obj.Loc_Code, trans)
        objCustInv.On_Hold = 0
        objCustInv.Remarks = "Automatically created AR against Disposal Entry- " & obj.Document_No & ""
        objCustInv.Description = obj.Description
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
        objCustInv.TAX6 = obj.TAX6
        objCustInv.TAX6_Rate = obj.TAX6_Rate
        objCustInv.TAX6_Amt = obj.TAX6_Amt
        objCustInv.TAX7 = obj.TAX7
        objCustInv.TAX7_Rate = obj.TAX7_Rate
        objCustInv.TAX7_Amt = obj.TAX7_Amt
        objCustInv.TAX8 = obj.TAX8
        objCustInv.TAX8_Rate = obj.TAX8_Rate
        objCustInv.TAX8_Amt = obj.TAX8_Amt
        objCustInv.TAX9 = obj.TAX9
        objCustInv.TAX9_Rate = obj.TAX9_Rate
        objCustInv.TAX9_Amt = obj.TAX9_Amt
        objCustInv.TAX10 = obj.TAX10
        objCustInv.TAX10_Rate = obj.TAX10_Rate
        objCustInv.TAX10_Amt = obj.TAX10_Amt
        objCustInv.Total_Tax = obj.Total_Tax_Amt
        objCustInv.Tax1_BAmount = obj.TAX1_Base_Amt
        objCustInv.Tax2_BAmount = obj.TAX2_Base_Amt
        objCustInv.Tax3_BAmount = obj.TAX3_Base_Amt
        objCustInv.Tax4_BAmount = obj.TAX4_Base_Amt
        objCustInv.Tax5_BAmount = obj.TAX5_Base_Amt
        objCustInv.Tax6_BAmount = obj.TAX6_Base_Amt
        objCustInv.Tax7_BAmount = obj.TAX7_Base_Amt
        objCustInv.Tax8_BAmount = obj.TAX8_Base_Amt
        objCustInv.Tax9_BAmount = obj.TAX9_Base_Amt
        objCustInv.Tax10_BAmount = obj.TAX10_Base_Amt
        objCustInv.Balance_Amt = obj.Doc_Amt
        objCustInv.Terms_Code = obj.Terms_Code
        objCustInv.PROJECT_ID = ""

        '' currency details
        objCustInv.CURRENCY_CODE = objCommonVar.BaseCurrencyCode
        objCustInv.ConvRate = 1
        objCustInv.ApplicableFrom = Nothing

        qry = "select Terms_Code,Terms_Desc,No_Days from TSPL_TERMS_MASTER where Terms_Code='" + obj.Terms_Code + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            objCustInv.Terms_Description = clsCommon.myCstr(dt.Rows(0)("Terms_Desc"))
            objCustInv.Due_Date = obj.Document_Date.AddDays(clsCommon.myCdbl(dt.Rows(0)("No_Days")))
        End If

        objCustInv.Discount_Percentage = IIf(obj.Discount_Base > 0, obj.Discount_Amt * 100 / obj.Discount_Base, 0)
        objCustInv.Discount_Base = obj.Discount_Base
        objCustInv.Discount_Amount = obj.Discount_Amt ''+ obj.HeadDisc_Amt + obj.HeadDisc_PerAmt
        ''objCustInv.Amount_Less_Discount = 
        dt = clsDBFuncationality.GetDataTable("select Receivable_Control_acct,Receipts_Discount_acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" + objCustInv.Account_Set + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objCustInv.Customer_Control_AC = clsCommon.myCstr(dt.Rows(0)("Receivable_Control_acct"))
            If clsCommon.myCdbl(obj.Discount_Amt) > 0 Then
                objCustInv.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Receipts_Discount_acct"))
            End If
        End If

        If obj.TAX1_Amt > 0 AndAlso clsCommon.myLen(obj.TAX1) > 0 Then
            objCustInv.TAX1_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX1, trans)
            objCustInv.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX1_GLAC, obj.Loc_Code, trans)
        End If
        If obj.TAX2_Amt > 0 AndAlso clsCommon.myLen(obj.TAX2) > 0 Then
            objCustInv.TAX2_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX2, trans)
            objCustInv.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX2_GLAC, obj.Loc_Code, trans)
        End If
        If obj.TAX3_Amt > 0 AndAlso clsCommon.myLen(obj.TAX3) > 0 Then
            objCustInv.TAX3_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX3, trans)
            objCustInv.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX3_GLAC, obj.Loc_Code, trans)
        End If
        If obj.TAX4_Amt > 0 AndAlso clsCommon.myLen(obj.TAX4) > 0 Then
            objCustInv.TAX4_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX4, trans)
            objCustInv.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX4_GLAC, obj.Loc_Code, trans)
        End If
        If obj.TAX5_Amt > 0 AndAlso clsCommon.myLen(obj.TAX5) > 0 Then
            objCustInv.TAX5_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX5, trans)
            objCustInv.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX5_GLAC, obj.Loc_Code, trans)
        End If
        If obj.TAX6_Amt > 0 AndAlso clsCommon.myLen(obj.TAX6) > 0 Then
            objCustInv.TAX6_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX6, trans)
            objCustInv.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX6_GLAC, obj.Loc_Code, trans)
        End If
        If obj.TAX7_Amt > 0 AndAlso clsCommon.myLen(obj.TAX7) > 0 Then
            objCustInv.TAX7_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX7, trans)
            objCustInv.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX7_GLAC, obj.Loc_Code, trans)
        End If
        If obj.TAX8_Amt > 0 AndAlso clsCommon.myLen(obj.TAX8) > 0 Then
            objCustInv.TAX8_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX8, trans)
            objCustInv.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX8_GLAC, obj.Loc_Code, trans)
        End If
        If obj.TAX9_Amt > 0 AndAlso clsCommon.myLen(obj.TAX9) > 0 Then
            objCustInv.TAX9_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX9, trans)
            objCustInv.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX9_GLAC, obj.Loc_Code, trans)
        End If
        If obj.TAX10_Amt > 0 AndAlso clsCommon.myLen(obj.TAX10) > 0 Then
            objCustInv.TAX10_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX10, trans)
            objCustInv.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX10_GLAC, obj.Loc_Code, trans)
        End If

        'objCustInv.RefDocType=
        'objCustInv.RefDocNo
        objCustInv.Add_Charge_Code1 = obj.AddCode1
        objCustInv.Add_Charge_Name1 = obj.AddDesc1
        objCustInv.Add_Charge_Amt1 = obj.AddAmt1
        objCustInv.Add_Charge_Code2 = obj.AddCode2
        objCustInv.Add_Charge_Name2 = obj.AddDesc2
        objCustInv.Add_Charge_Amt2 = obj.AddAmt2
        objCustInv.Add_Charge_Code3 = obj.AddCode3
        objCustInv.Add_Charge_Name3 = obj.AddDesc3
        objCustInv.Add_Charge_Amt3 = obj.AddAmt3
        objCustInv.Add_Charge_Code4 = obj.AddCode4
        objCustInv.Add_Charge_Name4 = obj.AddDesc4
        objCustInv.Add_Charge_Amt4 = obj.AddAmt4
        objCustInv.Add_Charge_Code5 = obj.AddCode5
        objCustInv.Add_Charge_Name5 = obj.AddDesc5
        objCustInv.Add_Charge_Amt5 = obj.AddAmt5
        objCustInv.Add_Charge_Code6 = obj.AddCode6
        objCustInv.Add_Charge_Name6 = obj.AddDesc6
        objCustInv.Add_Charge_Amt6 = obj.AddAmt6
        objCustInv.Add_Charge_Code7 = obj.AddCode7
        objCustInv.Add_Charge_Name7 = obj.AddDesc7
        objCustInv.Add_Charge_Amt7 = obj.AddAmt7
        objCustInv.Add_Charge_Code8 = obj.AddCode8
        objCustInv.Add_Charge_Name8 = obj.AddDesc8
        objCustInv.Add_Charge_Amt8 = obj.AddAmt8
        objCustInv.Add_Charge_Code9 = obj.AddCode9
        objCustInv.Add_Charge_Name9 = obj.AddDesc9
        objCustInv.Add_Charge_Amt9 = obj.AddAmt9
        objCustInv.Add_Charge_Code10 = obj.AddCode10
        objCustInv.Add_Charge_Name10 = obj.AddDesc10
        objCustInv.Add_Charge_Amt10 = obj.AddAmt10
        objCustInv.Total_Add_Charge = obj.Total_Add_Amt
        objCustInv.Tax_Calculation_Type = obj.Tax_Calculation_Type
        ''objCustInv.Status
        ''objCustInv.AgainstScrap
        objCustInv.Against_Sale_No = ""
        objCustInv.Against_Asset_Disposal = obj.Document_No

        Dim counter As Integer = 1
        objCustInv.Arr = New List(Of clsCustomerInvoiceDetail)
        For Each objTr As clsAssetScrapSaleDetail In obj.Arr
            Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()
            objCustInvTR.SNo = counter
            'dt = clsItemMaster.GetSaleAccGLAC(objTr.Item_Code, trans)
            'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            '    Throw New Exception("Please set sale account for item" + objTr.Item_Code)
            'End If
            ''Fill VendorInvoice details Data
            qry = "select TSPL_Dep_AccountSet.Ac_Control from TSPL_ACQUISITION_DETAIL left outer join  TSPL_Dep_AccountSet on  TSPL_Dep_AccountSet.AcSet_Code=TSPL_ACQUISITION_DETAIL.AcSet_Code where TSPL_ACQUISITION_DETAIL.Asset_Code='" & objTr.Asset_Code & "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Please set Asset Ctrl Account set for Asset " + objTr.Asset_Code)
            End If

            Dim strAssetCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Ac_Control"))
            strAssetCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strAssetCtrlAC, obj.Loc_Code, trans)
            'Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strAssetCtrlAC + "'", trans))

            objCustInvTR.GL_Account_Code = strAssetCtrlAC 'clsCommon.myCstr(dt.Rows(0)("Sales_Account"))
            objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Loc_Code, trans)
            objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)

            objCustInvTR.Hirerachy_Code = objTr.Hirerachy_Code
            objCustInvTR.Cost_Centre_Code = objTr.CostCenter_Code

            objCustInvTR.Amount = objTr.Amt
            objCustInvTR.Discount_Per = objTr.Discount_Per
            objCustInvTR.Discount = objTr.Discount_Amt
            objCustInvTR.Amount_less_Discount = objTr.Amt_After_Discount
            objCustInvTR.TAX1 = objTr.TAX1
            objCustInvTR.TAX1_Rate = objTr.TAX1_Rate
            objCustInvTR.TAX1_Amt = objTr.TAX1_Amt
            objCustInvTR.TAX1_Base_Amt = objTr.TAX1_Base_Amt
            objCustInvTR.TAX2 = objTr.TAX2
            objCustInvTR.TAX2_Rate = objTr.TAX2_Rate
            objCustInvTR.TAX2_Amt = objTr.TAX2_Amt
            objCustInvTR.TAX2_Base_Amt = objTr.TAX2_Base_Amt
            objCustInvTR.TAX3 = objTr.TAX3
            objCustInvTR.TAX3_Rate = objTr.TAX3_Rate
            objCustInvTR.TAX3_Amt = objTr.TAX3_Amt
            objCustInvTR.TAX3_Base_Amt = objTr.TAX3_Base_Amt
            objCustInvTR.TAX4 = objTr.TAX4
            objCustInvTR.TAX4_Rate = objTr.TAX4_Rate
            objCustInvTR.TAX4_Amt = objTr.TAX4_Amt
            objCustInvTR.TAX4_Base_Amt = objTr.TAX4_Base_Amt
            objCustInvTR.TAX5 = objTr.TAX5
            objCustInvTR.TAX5_Rate = objTr.TAX5_Rate
            objCustInvTR.TAX5_Amt = objTr.TAX5_Amt
            objCustInvTR.TAX5_Base_Amt = objTr.TAX5_Base_Amt
            objCustInvTR.TAX6 = objTr.TAX6
            objCustInvTR.TAX6_Rate = objTr.TAX6_Rate
            objCustInvTR.TAX6_Amt = objTr.TAX6_Amt
            objCustInvTR.TAX6_Base_Amt = objTr.TAX6_Base_Amt
            objCustInvTR.TAX7 = objTr.TAX7
            objCustInvTR.TAX7_Rate = objTr.TAX7_Rate
            objCustInvTR.TAX7_Amt = objTr.TAX7_Amt
            objCustInvTR.TAX7_Base_Amt = objTr.TAX7_Base_Amt
            objCustInvTR.TAX8 = objTr.TAX8
            objCustInvTR.TAX8_Rate = objTr.TAX8_Rate
            objCustInvTR.TAX8_Amt = objTr.TAX8_Amt
            objCustInvTR.TAX8_Base_Amt = objTr.TAX8_Base_Amt
            objCustInvTR.TAX9 = objTr.TAX9
            objCustInvTR.TAX9_Rate = objTr.TAX9_Rate
            objCustInvTR.TAX9_Amt = objTr.TAX9_Amt
            objCustInvTR.TAX9_Base_Amt = objTr.TAX9_Base_Amt
            objCustInvTR.TAX10 = objTr.TAX10
            objCustInvTR.TAX10_Rate = objTr.TAX10_Rate
            objCustInvTR.TAX10_Amt = objTr.TAX10_Amt
            objCustInvTR.TAX10_Base_Amt = objTr.TAX10_Base_Amt
            objCustInvTR.Total_Tax = objTr.Tax_Amt
            objCustInvTR.Total_Amount = objTr.Amt_After_Tax
            objCustInvTR.Remarks = ""
            objCustInvTR.TAX1_Base_Amt = objTr.TAX1_Base_Amt
            objCustInvTR.TAX2_Base_Amt = objTr.TAX2_Base_Amt
            objCustInvTR.TAX3_Base_Amt = objTr.TAX3_Base_Amt
            objCustInvTR.TAX4_Base_Amt = objTr.TAX4_Base_Amt
            objCustInvTR.TAX5_Base_Amt = objTr.TAX5_Base_Amt
            objCustInvTR.TAX6_Base_Amt = objTr.TAX6_Base_Amt
            objCustInvTR.TAX7_Base_Amt = objTr.TAX7_Base_Amt
            objCustInvTR.TAX8_Base_Amt = objTr.TAX8_Base_Amt
            objCustInvTR.TAX9_Base_Amt = objTr.TAX9_Base_Amt
            objCustInvTR.TAX10_Base_Amt = objTr.TAX10_Base_Amt
            'objCustInvTR.Comments=objTr.Comments
            objCustInv.Arr.Add(objCustInvTR)
            counter += 1
            'End If
        Next
        objCustInv.SaveData(objCustInv, True, trans, strFormID, strVoucherNoRecreate, strARNoForRecreate)
        clsCustomerInvoiceHead.PostData("", objCustInv.Document_No, "", trans)
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsAssetScrapSaleHead = clsAssetScrapSaleHead.GetData(strCode, NavigatorType.Current)
        Dim frm As New FrmFreeTxtBox1
        frm.Text = "Remarks for Delete"
        frm.ShowDialog()
        If clsCommon.myLen(frm.strRmks) > 0 Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                Try

                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_ASSET_SCRAP_HEAD", "Document_No", "TSPL_ASSET_SCRAP_DETAIL", "Document_No", trans)
                    Dim qry As String = "delete from TSPL_ASSET_SCRAP_DETAIL where Document_No='" + strCode + "'"
                    isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_ASSET_SCRAP_HEAD where Document_No='" + strCode + "'"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    If (isSaved) Then
                        trans.Commit()
                    Else
                        trans.Rollback()
                    End If
                Catch ex As Exception
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try
            End If
        End If
        Return isSaved
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim Qry As String = "select Status  from TSPL_ASSET_SCRAP_HEAD where Document_No='" + strCode + "'"
        If Not clsCommon.CompairString(clsDBFuncationality.getSingleValue(Qry, trans), "1") = CompairStringResult.Equal Then
            Throw New Exception("Transaction status should be posted for reverse and unpost")
        End If

        Try

            ' Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code in('IC-AD', 'GL-JE') and Source_Doc_No='" + strCode + "'", trans)
            'If clsCommon.myLen(VoucherNo) > 0 Then

            ''Qry = "delete TSPL_JOURNAL_DETAILS from TSPL_JOURNAL_DETAILS left outer join TSPL_JOURNAL_MASTER on  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No  where TSPL_JOURNAL_MASTER.Source_Doc_No in (select Document_Code from TSPL_ASSET_DEPRECIATION where Asset_Code in ( select Asset_Code from TSPL_ASSET_SCRAP_DETAIL where Document_No = '" + strCode + "'))"
            ''clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            ''Qry = "delete TSPL_JOURNAL_MASTER from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Source_Doc_No in (select Document_Code from TSPL_ASSET_DEPRECIATION where Asset_Code in ( select Asset_Code from TSPL_ASSET_SCRAP_DETAIL where Document_No = '" + strCode + "'))"
            ''clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            ''Qry = " delete TSPL_ASSET_DEPRECIATION from TSPL_ASSET_DEPRECIATION  where Asset_Code in ( select Asset_Code from TSPL_ASSET_SCRAP_DETAIL where Document_No = '" + strCode + "') "
            ''clsDBFuncationality.ExecuteNonQuery(Qry, trans)


            Dim dt As DataTable = Nothing
            Dim VoucherNo As String = String.Empty
            '' to check and delete asset depriciation 
            Qry = " select Document_Code from TSPL_ASSET_DEPRECIATION where Asset_Disposal_Code='" + strCode + "' "
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    VoucherNo = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AM-DP' and Source_Doc_No='" + clsCommon.myCstr(dr("Document_Code")) + "'", trans)
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                    Qry = "delete from TSPL_JOURNAL_DETAILS where TSPL_JOURNAL_DETAILS.Voucher_No ='" + VoucherNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    Qry = "delete from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Voucher_No ='" + VoucherNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(dr("Document_Code")), "TSPL_ASSET_DEPRECIATION", "Document_Code", trans)
                    Qry = "Delete from TSPL_ASSET_DEPRECIATION WHERE Document_Code='" + clsCommon.myCstr(dr("Document_Code")) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                Next
            End If



            Dim strARInvoiceNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Document_No from TSPL_Customer_Invoice_Head  where Against_asset_Disposal =  '" + strCode + "' ", trans))
            VoucherNo = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where  Source_Doc_No='" + strARInvoiceNo + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strARInvoiceNo, "TSPL_Customer_Invoice_Head", "Document_No", "TSPL_Customer_Invoice_Detail", "Document_No", trans)

            Qry = "delete from TSPL_Customer_Invoice_Detail where Document_No ='" + strARInvoiceNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = "delete from TSPL_Customer_Invoice_Head where Document_No ='" + strARInvoiceNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)


            'Qry = "delete from TSPL_ASSET_SCRAP_DETAIL where Document_No ='" + strCode + "'"
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            'Qry = "delete from TSPL_ASSET_SCRAP_HEAD where Document_No ='" + strCode + "'"
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_ASSET_SCRAP_HEAD set Status = 0 where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_ASSET_SCRAP_HEAD", "Document_No", trans)
        Catch ex As Exception
            Throw New Exception("Error in Document no [" + strCode + "]" + Environment.NewLine + ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CancelData(ByVal Form_Id As String, ByVal Doc_No As String, ByVal NavType As NavigatorType) As Boolean
        '' created by Sanjay
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsAssetScrapSaleHead = clsAssetScrapSaleHead.GetData(Doc_No, NavType, trans)

            If obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If

            If clsCommon.myLen(clsCommon.myCstr(obj.Tax_Group)) > 0 Then
                Dim isTaxTaxable As String = "N"
                isTaxTaxable = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select 'Y' from TSPL_TAX_GROUP_MASTER where Tax_Group_Code ='" & obj.Tax_Group & "' and Is_Tax_Exempted =0 and Tax_Group_Type ='S'", trans))
                Dim dtirn As DataTable = clsDBFuncationality.GetDataTable("select Einvoice_type,IRN_No,Loc_Code  from TSPL_ASSET_SCRAP_HEAD where Document_No='" & Doc_No & "'", trans)
                If dtirn IsNot Nothing AndAlso dtirn.Rows.Count > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(dtirn.Rows(0)("Einvoice_type")), "BB") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(isTaxTaxable), "Y") = CompairStringResult.Equal AndAlso clsERPFuncationality.GetEInvoiceStatus(obj.Document_Date, trans) = True Then
                        If ClsEInvoiceOFAPIs.EInvoice_Cancellation(Doc_No, clsCommon.myCstr(dtirn.Rows(0)("IRN_No")), clsCommon.myCstr(dtirn.Rows(0)("Loc_Code")), trans) = True Then
                        Else
                            Throw New Exception("Invalid JSON Value")
                        End If
                    End If
                End If
            End If

            '' transfer data into cancel table
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_ASSET_SCRAP_HEAD", "Document_No", "TSPL_ASSET_SCRAP_DETAIL", "Document_No", trans)

            '' cancel customer invoice data
            qry = "select Document_No from TSPL_Customer_Invoice_Head  where Against_asset_Disposal='" & Doc_No & "'"
            Dim strARInvoiceNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(strARInvoiceNo) > 0 Then
                clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, strARInvoiceNo, "TSPL_Customer_Invoice_Head", "Document_No", "TSPL_Customer_Invoice_Detail", "Document_No", trans)
            End If

            '' cancel journal master data invoice
            qry = "select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No in (select Document_No from TSPL_Customer_Invoice_Head  where Against_asset_Disposal='" & Doc_No & "')"
            Dim VoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
            End If

            '' cancel custom field data
            clsCommonFunctionality.SaveCancelDataMultKey(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CUSTOM_FIELD_VALUES", "Transaction_Code", "Program_Code", Form_Id, trans)

            ''delete data from multiple tables
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Asset_Code from TSPL_ASSET_SCRAP_DETAIL where Document_No = '" + Doc_No + "'", trans)
            'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            '    For Each dr As DataRow In dt.Rows
            '        clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, clsCommon.myCstr(dr("Asset_Code")), "TSPL_ASSET_DEPRECIATION", "Asset_Code", trans)
            '    Next
            'End If

            'dt = clsDBFuncationality.GetDataTable("select Voucher_No from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Source_Doc_No in (select Document_Code from TSPL_ASSET_DEPRECIATION where Asset_Code in ( select Asset_Code from TSPL_ASSET_SCRAP_DETAIL where Document_No = '" + Doc_No + "'))", trans)
            'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            '    For Each dr As DataRow In dt.Rows
            '        clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, clsCommon.myCstr(dr("Voucher_No")), "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
            '    Next
            'End If


            Dim dt As DataTable = Nothing
            Dim dt1 As DataTable = Nothing
            Dim VoucherNo1 As String = ""
            Dim qry1 As String = " select Document_Code from TSPL_ASSET_DEPRECIATION where Asset_Disposal_Code='" + Doc_No + "' "
            dt1 = clsDBFuncationality.GetDataTable(qry1, trans)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each dr As DataRow In dt1.Rows
                    VoucherNo1 = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AM-DP' and Source_Doc_No='" + clsCommon.myCstr(dr("Document_Code")) + "'", trans)
                    clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, clsCommon.myCstr(dr("Document_Code")), "TSPL_ASSET_DEPRECIATION", "Document_Code", trans)
                    clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, clsCommon.myCstr(VoucherNo1), "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Next
            End If


            ''qry = "delete TSPL_JOURNAL_DETAILS from TSPL_JOURNAL_DETAILS left outer join TSPL_JOURNAL_MASTER on  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No  where TSPL_JOURNAL_MASTER.Source_Doc_No in (select Document_Code from TSPL_ASSET_DEPRECIATION where Asset_Code in ( select Asset_Code from TSPL_ASSET_SCRAP_DETAIL where Document_No = '" + Doc_No + "'))"
            ''clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''qry = "delete TSPL_JOURNAL_MASTER from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Source_Doc_No in (select Document_Code from TSPL_ASSET_DEPRECIATION where Asset_Code in ( select Asset_Code from TSPL_ASSET_SCRAP_DETAIL where Document_No = '" + Doc_No + "'))"
            ''clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''qry = " delete TSPL_ASSET_DEPRECIATION from TSPL_ASSET_DEPRECIATION  where Asset_Code in ( select Asset_Code from TSPL_ASSET_SCRAP_DETAIL where Document_No = '" + Doc_No + "') "
            ''clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'Dim strARInvoiceNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Document_No from TSPL_Customer_Invoice_Head  where Against_asset_Disposal =  '" + Doc_No + "' ", trans))
            'Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where  Source_Doc_No='" + strARInvoiceNo + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            qry = "delete from TSPL_Customer_Invoice_Detail where Document_No ='" + strARInvoiceNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_Customer_Invoice_Head where Document_No ='" + strARInvoiceNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


            dt = Nothing
            VoucherNo = ""
            '' to check and delete asset depriciation 
            qry = " select Document_Code from TSPL_ASSET_DEPRECIATION where Asset_Disposal_Code='" + Doc_No + "' "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    VoucherNo = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AM-DP' and Source_Doc_No='" + clsCommon.myCstr(dr("Document_Code")) + "'", trans)

                    qry = "delete from TSPL_JOURNAL_DETAILS where TSPL_JOURNAL_DETAILS.Voucher_No ='" + VoucherNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Voucher_No ='" + VoucherNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "Delete from TSPL_ASSET_DEPRECIATION WHERE Document_Code='" + clsCommon.myCstr(dr("Document_Code")) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                Next
            End If


            qry = "delete from TSPL_ASSET_SCRAP_DETAIL where Document_No ='" + Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_ASSET_SCRAP_HEAD where Document_No ='" + Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            '' release objects 
            obj = Nothing
            qry = Nothing

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


End Class


Public Class clsAssetScrapSaleDetail
#Region "Variables"
    Public Document_No As String = Nothing
    Public Line_No As Integer = 0
    Public Asset_Code As String = ""
    Public Asset_Name As String = ""
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Qty As Double = 0
    Public UOM As String = ""
    Public Rate As Double = 0
    Public Amt As String = Nothing
    Public Discount_Per As Double = 0
    Public Discount_Amt As Double = 0
    Public Amt_After_Discount As String = Nothing
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
    Public TAX6 As String = Nothing
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Rate As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Rate As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Rate As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Rate As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Rate As Double = 0
    Public TAX10_Amt As Double = 0
    Public Tax_Amt As Double = 0
    Public Amt_After_Tax As Double = 0
    Public Hirerachy_Code As String = Nothing
    Public CostCenter_Code As String = Nothing
    Public CostCenter_Name As String = Nothing
#End Region

    Public Shared Function FinderItem(ByVal strCode As String, ByVal strItemType As String, ByVal isButtonClicked As Boolean) As clsAssetScrapSaleDetail
        Dim obj As clsAssetScrapSaleDetail = Nothing
        Dim qry As String = "select Item_Code as Code,Item_Desc as Name ,TSPL_Item_Category.Category_Name as [Item Category] ,TSPL_ITEM_SUB_CATEGORY.Description as [Sub Category] from  TSPL_ITEM_MASTER"
        qry += " left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code =TSPL_ITEM_MASTER.item_category "
        qry += "  left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code  =TSPL_ITEM_MASTER.Sub_item_category "

        Dim WhrCls As String = ""
        If clsCommon.myLen(strItemType) > 0 Then
            WhrCls = "Item_Type<>'" + strItemType + "'"
        End If
        strCode = clsCommon.ShowSelectForm("ItemFinder", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Item_Code,Item_Desc,UOM,cost from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsAssetScrapSaleDetail()
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                obj.Rate = clsCommon.myCstr(dt.Rows(0)("cost"))


            End If
        End If
        Return obj
    End Function

    Public Shared Function FinderItemBoth(ByVal strCode As String, ByVal strItemType As String, ByVal isButtonClicked As Boolean) As clsAssetScrapSaleDetail
        Dim obj As clsAssetScrapSaleDetail = Nothing
        Dim qry As String = "select Item_Code as Code,Item_Desc as Name ,TSPL_Item_Category.Category_Name as [Item Category] ,TSPL_ITEM_SUB_CATEGORY.Description as [Sub Category] from  TSPL_ITEM_MASTER"
        qry += " left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code =TSPL_ITEM_MASTER.item_category "
        qry += "  left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code  =TSPL_ITEM_MASTER.Sub_item_category "

        Dim WhrCls As String = ""
        If clsCommon.myLen(strItemType) > 0 Then
            If strItemType = "F" Then
                WhrCls = "Item_Type='F'"
            Else
                WhrCls = "Item_Type<>'F'"
            End If


        End If
        strCode = clsCommon.ShowSelectForm("ItemFinder", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Item_Code,Item_Desc,UOM,cost from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsAssetScrapSaleDetail()
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                obj.Rate = clsCommon.myCstr(dt.Rows(0)("cost"))


            End If
        End If
        Return obj
    End Function

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsAssetScrapSaleDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsAssetScrapSaleDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                ''richa ERO/13/07/21-001447
                clsCommon.AddColumnsForChange(coll, "Hirerachy_Code", obj.Hirerachy_Code, True)
                clsCommon.AddColumnsForChange(coll, "CostCenter_Code", obj.CostCenter_Code)

                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
                clsCommon.AddColumnsForChange(coll, "Discount_Per", obj.Discount_Per)
                clsCommon.AddColumnsForChange(coll, "Discount_Amt", obj.Discount_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax_Amt", obj.Tax_Amt)
                clsCommon.AddColumnsForChange(coll, "Amt_After_Discount", obj.Amt_After_Discount)
                clsCommon.AddColumnsForChange(coll, "Amt", obj.Amt)
                clsCommon.AddColumnsForChange(coll, "Amt_After_Tax", obj.Amt_After_Tax)
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
                clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)

                clsCommon.AddColumnsForChange(coll, "Asset_Code", obj.Asset_Code, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_SCRAP_DETAIL", OMInsertOrUpdate.Insert, "", trans)




            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalanceSRNQty(ByVal strSRNCode As String, ByVal strICode As String, ByVal strCurrPINNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " & _
            " select TSPL_SRN_DETAIL.Item_Code as ICode,TSPL_SRN_DETAIL.SRN_Qty as Qty,1 as RI from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_DETAIL.Status=0 and TSPL_SRN_HEAD.Status=1 and TSPL_SRN_DETAIL.SRN_No ='" + strSRNCode + "' and TSPL_SRN_DETAIL.Item_Code='" + strICode + "' and  TSPL_SRN_DETAIL.UOM='" + strUOM + "' and isnull(TSPL_SRN_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_SRN_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' " & _
            " union all " & _
            " select TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.PI_Qty as Qty,-1 as RI from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_DETAIL.SRN_Id='" + strSRNCode + "'   and TSPL_PI_DETAIL.Item_Code='" + strICode + "'  and  TSPL_PI_DETAIL.UOM='" + strUOM + "' and isnull(TSPL_PI_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_PI_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "'  and TSPL_PI_DETAIL.PI_No not in ('" + strCurrPINNo + "')  " & _
            " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function CompleteSRN(ByVal strDoccode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_SRN_DETAIL set Status=1 where SRN_No='" + strDoccode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function

End Class


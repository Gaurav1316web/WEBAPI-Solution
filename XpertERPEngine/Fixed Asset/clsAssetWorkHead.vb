Imports common
Imports System.Data.SqlClient
Public Class clsAssetWorkHead

#Region "Variables"

    Public Document_Code As String = Nothing
    Public Document_Date As DateTime = Nothing
    Public Asset_Code As String = Nothing
    Public Asset_Description As String = Nothing
    Public Location_Code As String = Nothing
    Public Location_Description As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Description As String = Nothing
    Public Remarks As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public On_Hold As Boolean = Nothing

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

    Public Total_Amt As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Net_Amt As Double = 0
    Public TDS_Amount As Double = 0
    Public _Type As String = ""
    Public GL_Account_Code As String

    Public Post_Date As DateTime? = Nothing
    Public Arr As List(Of clsAssetWorkDetail) = Nothing
    Public objPIRemittance As clsPIRemittance = Nothing
    Public Capex_SubCode As String
    Public Capex_Code As String
    Public Ref_Doc_No As String
    Public Ref_Doc_Type As String
#End Region

    Public Function SaveData(ByVal obj As clsAssetWorkHead, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsAssetWorkHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Fixed Asset", "Assset Work Expanses", obj.Location_Code, obj.Document_Date, trans)
        Try
            If Not isNewEntry Then
                Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Status from TSPL_ASSET_WORK_HEAD Where Document_Code='" + obj.Document_Code + "'", trans))
                If Status = 1 Then
                    Throw New Exception("This document is already posted.")
                End If
            End If
            Dim qry As String = "delete from TSPL_ASSET_WORK_DETAIL where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PI_REMITTANCE where Document_No='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Asset_Code", obj.Asset_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code, True)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
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
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Net_Amt", obj.Net_Amt)
            clsCommon.AddColumnsForChange(coll, "TDS_Amount", obj.TDS_Amount)
            clsCommon.AddColumnsForChange(coll, "Capex_SubCode", obj.Capex_SubCode, True)
            clsCommon.AddColumnsForChange(coll, "Capex_Code", obj.Capex_Code, True)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "_Type", obj._Type)
            clsCommon.AddColumnsForChange(coll, "GL_Account_Code", obj.GL_Account_Code, True)
            clsCommon.AddColumnsForChange(coll, "RefDocNo", obj.Ref_Doc_No)
            clsCommon.AddColumnsForChange(coll, "RefDocType", obj.Ref_Doc_Type)
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.AssetWork, "", obj.Location_Code)
                If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_WORK_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_WORK_HEAD", OMInsertOrUpdate.Update, "TSPL_ASSET_WORK_HEAD.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            '' validate iisue against purchase value done by Panch raj on 25-Jan-2018
            If clsCommon.myLen(obj.Capex_SubCode) > 0 Then
                clsCapexBudget.CheckNegativeIssueAgainstCapexPurchaseValue(obj.Capex_SubCode, "", trans)
                clsCapexBudget.CheckNegativeSubCapexBalance(obj.Capex_SubCode, trans)
            End If
            clsAssetWorkDetail.SaveData(obj.Document_Code, Arr, trans)
            clsPIRemittance.SaveData(obj.objPIRemittance, obj.Document_Code, obj.Document_Date, trans)
            '===============Added by preeti Gupta[10/01/2018]============================
            '' check Amount is used in po
            clsAssetWorkHead.CheckPOAmount(obj.Ref_Doc_No, trans)
            '================================================================
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function ValidateIssueAgainstCapexPurchaseValue(ByVal Sub_Capex_Code As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim PurchaseValue As Decimal = clsCapexBudget.GetSubCapexValue(Sub_Capex_Code, "", trans, "I")
            qry = " select sum((case when TSPL_IssueItemToAssembledAsset_Head.Doc_Type='Issue' then 1 else -1 end) * TSPL_IssueItemToAssembledAsset_Detail.Item_Net_Amt) " & _
                   " from TSPL_IssueItemToAssembledAsset_Detail inner join TSPL_IssueItemToAssembledAsset_Head on TSPL_IssueItemToAssembledAsset_Detail.Doc_No=TSPL_IssueItemToAssembledAsset_Head.Doc_No  " & _
                   " inner join TSPL_CAPEX_BUDGET_MASTER on TSPL_CAPEX_BUDGET_MASTER.CODE = TSPL_IssueItemToAssembledAsset_Detail.Capex_SubCode And TSPL_CAPEX_BUDGET_MASTER.Capex_Code = TSPL_IssueItemToAssembledAsset_Detail.Capex_Code" & _
                   " where 2 = 2" & _
                   " and TSPL_CAPEX_BUDGET_MASTER.CODE ='" & Sub_Capex_Code & "'" & _
                   " and isnull(TSPL_IssueItemToAssembledAsset_Detail.IsCapex,0)=1   and (TSPL_IssueItemToAssembledAsset_Detail.CapexType ='Regular' OR TSPL_IssueItemToAssembledAsset_Detail.CheckCapexLimit=0)"
            Dim IssueAgainstPValue As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            If (PurchaseValue - IssueAgainstPValue) < 0 Then
                Throw New Exception("Trying to issue more value than purchase against Sub Capex " & Sub_Capex_Code & "")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CheckPOAmount(ByVal StrPOCode As String, ByVal trans As SqlTransaction) As Boolean
       
        If clsCommon.myLen(StrPOCode) > 0 Then
            Dim strPoAmount As Decimal = 0
            Dim strBudget As Decimal = 0

            strPoAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(amt_after_tax ) as PO_AMOUNT from tspl_purchase_order_head where TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No ='" & StrPOCode & "' ", trans))
            strBudget = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SElect sum(TSPL_ASSET_WORK_HEAD.net_amt)as net_amt from TSPL_ASSET_WORK_HEAD where RefDocNo ='" & StrPOCode & "'  ", trans))
            If strPoAmount < strBudget Then
                Throw New Exception(" Total work expe. amount (" & strBudget & ") should be less then PO Amount (" & strPoAmount & ") ")
            Else
                Return True
            End If
        End If

        Return True
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsAssetWorkHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsAssetWorkHead
        Dim obj As clsAssetWorkHead = Nothing
        Dim qry As String = "SELECT TSPL_ASSET_WORK_HEAD.*,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_ACQUISITION_DETAIL.Asset_Name,TSPL_LOCATION_MASTER.Location_Desc FROM TSPL_ASSET_WORK_HEAD "
        qry += " left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_DETAIL.Asset_Code=TSPL_ASSET_WORK_HEAD.Asset_Code"
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_ASSET_WORK_HEAD.Location_Code"
        qry += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_ASSET_WORK_HEAD.Vendor_Code"
        qry += " where 2=2"
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_ASSET_WORK_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_ASSET_WORK_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_ASSET_WORK_HEAD.Document_Code = (select Max(Document_Code) from TSPL_ASSET_WORK_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_ASSET_WORK_HEAD.Document_Code = (select Min(Document_Code) from TSPL_ASSET_WORK_HEAD where Document_Code>'" + strDocumentNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_ASSET_WORK_HEAD.Document_Code = (select Max(Document_Code) from TSPL_ASSET_WORK_HEAD where Document_Code<'" + strDocumentNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_ASSET_WORK_HEAD.Document_Code = '" + strDocumentNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsAssetWorkHead()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Asset_Code = clsCommon.myCstr(dt.Rows(0)("Asset_Code"))

            obj._Type = clsCommon.myCstr(dt.Rows(0)("_Type"))
            obj.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("GL_Account_Code"))
            obj.Asset_Description = clsCommon.myCstr(dt.Rows(0)("Asset_Name"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Description = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
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
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
            obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
            obj.Net_Amt = clsCommon.myCdbl(dt.Rows(0)("Net_Amt"))
            '==============added by preeti
            obj.Capex_Code = clsCommon.myCstr(dt.Rows(0)("Capex_Code"))
            obj.Capex_SubCode = clsCommon.myCstr(dt.Rows(0)("Capex_SubCode"))
            obj.Ref_Doc_Type = clsCommon.myCstr(dt.Rows(0)("RefDocType"))
            obj.Ref_Doc_No = clsCommon.myCstr(dt.Rows(0)("RefDocNo"))
            '================================
            obj.TDS_Amount = clsCommon.myCdbl(dt.Rows(0)("TDS_Amount"))
            If dt.Rows(0)("Post_Date") IsNot DBNull.Value Then
                obj.Post_Date = clsCommon.myCDate(dt.Rows(0)("Post_Date"))
            End If
            obj.objPIRemittance = clsPIRemittance.GetData(obj.Document_Code, trans)

            qry = "SELECT TSPL_ASSET_WORK_DETAIL.*,TSPL_Additional_Charges.Description as Add_Charges_Name,TSPL_GL_ACCOUNTS.Description as GL_Acc_Desc  from TSPL_ASSET_WORK_DETAIL " + Environment.NewLine
            qry += " left outer join TSPL_Additional_Charges on TSPL_Additional_Charges.Code=TSPL_ASSET_WORK_DETAIL.Add_Charges_Code"
            qry += " left join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_ASSET_WORK_DETAIL.GL_Account_Code "
            qry += " where TSPL_ASSET_WORK_DETAIL.Document_Code='" + obj.Document_Code + "' ORDER BY TSPL_ASSET_WORK_DETAIL.SNo" + Environment.NewLine
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsAssetWorkDetail)
                Dim objTr As clsAssetWorkDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsAssetWorkDetail
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.SNo = clsCommon.myCdbl(clsCommon.myCdbl(dr("SNo")))
                    objTr.Add_Charges_Code = clsCommon.myCstr(dr("Add_Charges_Code"))
                    objTr.Add_Charges_Name = clsCommon.myCstr(dr("Add_Charges_Name"))
                    objTr.Hirerachy_Code = clsCommon.myCstr(dr("Hirerachy_Code"))
                    objTr.CostCenter_Code = clsCommon.myCstr(dr("CostCenter_Code"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
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
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))
                    objTr.GL_Account_Code = clsCommon.myCstr(dr("GL_Account_Code"))
                    objTr.GL_Account_Name = clsCommon.myCstr(dr("GL_Acc_Desc"))
                    objTr.IsUnclaimedTax = clsCommon.myCBool(dr("IsUnclaimedTax"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function
    Public Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)

        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Dim qry As String
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsAssetWorkHead = clsAssetWorkHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Post_Date, "dd/MM/yyyy"))
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Document No " + obj.Document_Code + " Is On Hold.Can't Post it")
            End If
            If clsCommon.CompairString(obj._Type, "Vendor") = CompairStringResult.Equal Then

                Dim isCreateInvoice As Boolean = False
                Dim isCreateDebinNote As Boolean = False
                For Each objPIDetail As clsAssetWorkDetail In obj.Arr
                    If objPIDetail.Amount > 0 Then
                        isCreateInvoice = True
                    ElseIf objPIDetail.Amount < 0 Then
                        isCreateDebinNote = True
                    End If
                Next

                If isCreateInvoice Then
                    Dim objVendorInvHead As New clsVedorInvoiceHead()
                    objVendorInvHead.Invoice_Entry_Date = clsCommon.myCDate(obj.Document_Date, "dd/MM/yyyy")
                    objVendorInvHead.Vendor_Code = obj.Vendor_Code
                    objVendorInvHead.Vendor_Name = obj.Vendor_Name
                    objVendorInvHead.Vendor_Invoice_No = ""
                    objVendorInvHead.Vendor_Invoice_Date = clsCommon.myCDate(obj.Document_Date, "dd/MM/yyyy")
                    objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.Location_Code, trans)
                    objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.Vendor_Code + "'", trans))
                    If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                        Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.Vendor_Name)
                    End If
                    objVendorInvHead.Against_Asset_Work = obj.Document_Code
                    objVendorInvHead.Document_Type = "I"
                    objVendorInvHead.Invoice_Type = "AP"

                    objVendorInvHead.On_Hold = False
                    objVendorInvHead.Description = "Vendor " + obj.Vendor_Code + "/" + obj.Vendor_Name + " .Against Asset Work " + obj.Document_Code + ""
                    objVendorInvHead.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                    objVendorInvHead.Tax_Group = obj.Tax_Group
                    objVendorInvHead.RefDocType = obj.Ref_Doc_Type
                    objVendorInvHead.RefDocNo = obj.Ref_Doc_No
                    If (clsCommon.myLen(obj.TAX1) > 0) Then
                        objVendorInvHead.TAX1 = obj.TAX1
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX1, trans) Then
                            objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
                            objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX2) > 0) Then
                        objVendorInvHead.TAX2 = obj.TAX2
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX2, trans) Then
                            objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
                            objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX3) > 0) Then
                        objVendorInvHead.TAX3 = obj.TAX3
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX3, trans) Then
                            objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
                            objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX4) > 0) Then
                        objVendorInvHead.TAX4 = obj.TAX4
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX4, trans) Then
                            objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
                            objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX5) > 0) Then
                        objVendorInvHead.TAX5 = obj.TAX5
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX5, trans) Then
                            objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
                            objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX6) > 0) Then
                        objVendorInvHead.TAX6 = obj.TAX6
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX6, trans) Then
                            objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
                            objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX7) > 0) Then
                        objVendorInvHead.TAX7 = obj.TAX7
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX7, trans) Then
                            objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
                            objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX8) > 0) Then
                        objVendorInvHead.TAX8 = obj.TAX8
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX8, trans) Then
                            objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
                            objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX9) > 0) Then
                        objVendorInvHead.TAX9 = obj.TAX9
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX9, trans) Then
                            objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
                            objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX10) > 0) Then
                        objVendorInvHead.TAX10 = obj.TAX10
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX10, trans) Then
                            objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
                            objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
                    End If

                    objVendorInvHead.Against_Acquisition = obj.Document_Code
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                        objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.Location_Code, trans)
                        If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                            objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                            objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.Location_Code, trans)
                        End If
                    End If
                    If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                        Throw New Exception("Please set the vendor payable Account")
                    End If
                    objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                    Dim ii As Integer = 0
                    Dim isFirstTime As Boolean = True
                    objVendorInvHead.Total_Landed_Amt = 0
                    For Each objPIDetail As clsAssetWorkDetail In obj.Arr
                        If objPIDetail.Amount > 0 Then
                            qry = "select (case when TSPL_ACQUISITION_DETAIL.Is_Assembled=0 then  TSPL_Dep_AccountSet.Ac_Control else TSPL_Dep_AccountSet.WIP_AC end) as Ac_Control from TSPL_ACQUISITION_DETAIL left outer join  TSPL_Dep_AccountSet on  TSPL_Dep_AccountSet.AcSet_Code=TSPL_ACQUISITION_DETAIL.AcSet_Code where TSPL_ACQUISITION_DETAIL.Asset_Code='" + obj.Asset_Code + "'"
                            dt = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                Throw New Exception("Please set Asset Ctrl Account set for Asset " + obj.Asset_Code)
                            End If

                            Dim strAssetCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Ac_Control"))
                            If clsCommon.myLen(strAssetCtrlAC) <= 0 Then
                                Throw New Exception("Please set WIP Account set for Asset " + obj.Asset_Code)
                            End If
                            strAssetCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strAssetCtrlAC, obj.Location_Code, trans)
                            Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strAssetCtrlAC + "'", trans))

                            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                            ii = ii + 1
                            objVendorInvDetail.Detail_Line_No = ii
                            objVendorInvDetail.GL_Account_Code = strAssetCtrlAC
                            objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
                            objVendorInvDetail.Amount = objPIDetail.Amount
                            objVendorInvDetail.Discount_Per = 0
                            objVendorInvDetail.Discount = 0
                            objVendorInvDetail.Amount_less_Discount = objPIDetail.Amount
                            objVendorInvDetail.Cost_Centre_Code = objPIDetail.CostCenter_Code
                            objVendorInvDetail.Hirerachy_Code = objPIDetail.Hirerachy_Code
                            objVendorInvDetail.TAX1 = objPIDetail.TAX1
                            objVendorInvDetail.TAX1_Rate = objPIDetail.TAX1_Rate
                            objVendorInvDetail.TAX1_Amt = objPIDetail.TAX1_Amt
                            objVendorInvDetail.TAX2 = objPIDetail.TAX2
                            objVendorInvDetail.TAX2_Rate = objPIDetail.TAX2_Rate
                            objVendorInvDetail.TAX2_Amt = objPIDetail.TAX2_Amt
                            objVendorInvDetail.TAX3 = objPIDetail.TAX3
                            objVendorInvDetail.TAX3_Rate = objPIDetail.TAX3_Rate
                            objVendorInvDetail.TAX3_Amt = objPIDetail.TAX3_Amt
                            objVendorInvDetail.TAX4 = objPIDetail.TAX4
                            objVendorInvDetail.TAX4_Rate = objPIDetail.TAX4_Rate
                            objVendorInvDetail.TAX4_Amt = objPIDetail.TAX4_Amt
                            objVendorInvDetail.TAX5 = objPIDetail.TAX5
                            objVendorInvDetail.TAX5_Rate = objPIDetail.TAX5_Rate
                            objVendorInvDetail.TAX5_Amt = objPIDetail.TAX5_Amt
                            objVendorInvDetail.TAX6 = objPIDetail.TAX6
                            objVendorInvDetail.TAX6_Rate = objPIDetail.TAX6_Rate
                            objVendorInvDetail.TAX6_Amt = objPIDetail.TAX6_Amt
                            objVendorInvDetail.TAX7 = objPIDetail.TAX7
                            objVendorInvDetail.TAX7_Rate = objPIDetail.TAX7_Rate
                            objVendorInvDetail.TAX7_Amt = objPIDetail.TAX7_Amt
                            objVendorInvDetail.TAX8 = objPIDetail.TAX8
                            objVendorInvDetail.TAX8_Rate = objPIDetail.TAX8_Rate
                            objVendorInvDetail.TAX8_Amt = objPIDetail.TAX8_Amt
                            objVendorInvDetail.TAX9 = objPIDetail.TAX9
                            objVendorInvDetail.TAX9_Rate = objPIDetail.TAX9_Rate
                            objVendorInvDetail.TAX9_Amt = objPIDetail.TAX9_Amt
                            objVendorInvDetail.TAX10 = objPIDetail.TAX10
                            objVendorInvDetail.TAX10_Rate = objPIDetail.TAX10_Rate
                            objVendorInvDetail.TAX10_Amt = objPIDetail.TAX10_Amt
                            objVendorInvDetail.Total_Tax = objPIDetail.Total_Tax_Amt
                            objVendorInvDetail.Total_Amount = objPIDetail.Item_Net_Amt
                            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                objVendorInvHead.Arr.Add(objVendorInvDetail)


                                If objPIDetail.TAX1_Amt > 0 Then
                                    objVendorInvHead.Tax1_BAmount += objPIDetail.TAX1_Base_Amt
                                    objVendorInvHead.TAX1_Amt += objPIDetail.TAX1_Amt
                                End If
                                If objPIDetail.TAX2_Amt > 0 Then
                                    objVendorInvHead.Tax2_BAmount += objPIDetail.TAX2_Base_Amt
                                    objVendorInvHead.TAX2_Amt += objPIDetail.TAX2_Amt
                                End If
                                If objPIDetail.TAX3_Amt > 0 Then
                                    objVendorInvHead.Tax3_BAmount += objPIDetail.TAX3_Base_Amt
                                    objVendorInvHead.TAX3_Amt += objPIDetail.TAX3_Amt
                                End If
                                If objPIDetail.TAX4_Amt > 0 Then
                                    objVendorInvHead.Tax4_BAmount += objPIDetail.TAX4_Base_Amt
                                    objVendorInvHead.TAX4_Amt += objPIDetail.TAX4_Amt
                                End If
                                If objPIDetail.TAX5_Amt > 0 Then
                                    objVendorInvHead.Tax5_BAmount += objPIDetail.TAX5_Base_Amt
                                    objVendorInvHead.TAX5_Amt += objPIDetail.TAX5_Amt
                                End If
                                If objPIDetail.TAX6_Amt > 0 Then
                                    objVendorInvHead.Tax6_BAmount += objPIDetail.TAX6_Base_Amt
                                    objVendorInvHead.TAX6_Amt += objPIDetail.TAX6_Amt
                                End If
                                If objPIDetail.TAX7_Amt > 0 Then
                                    objVendorInvHead.Tax7_BAmount += objPIDetail.TAX7_Base_Amt
                                    objVendorInvHead.TAX7_Amt += objPIDetail.TAX7_Amt
                                End If
                                If objPIDetail.TAX8_Amt > 0 Then
                                    objVendorInvHead.Tax8_BAmount += objPIDetail.TAX8_Base_Amt
                                    objVendorInvHead.TAX8_Amt += objPIDetail.TAX8_Amt
                                End If
                                If objPIDetail.TAX9_Amt > 0 Then
                                    objVendorInvHead.Tax9_BAmount += objPIDetail.TAX9_Base_Amt
                                    objVendorInvHead.TAX9_Amt += objPIDetail.TAX9_Amt
                                End If
                                If objPIDetail.TAX10_Amt > 0 Then
                                    objVendorInvHead.Tax10_BAmount += objPIDetail.TAX10_Base_Amt
                                    objVendorInvHead.TAX10_Amt += objPIDetail.TAX10_Amt
                                End If

                                objVendorInvHead.Total_Tax += objPIDetail.Total_Tax_Amt

                                objVendorInvHead.Discount_Base += objPIDetail.Amount
                                objVendorInvHead.Discount_Amount = 0
                                objVendorInvHead.Amount_Less_Discount += objPIDetail.Amount
                                objVendorInvHead.Document_Total += (objPIDetail.Amount + objPIDetail.Total_Tax_Amt)
                                objVendorInvHead.Balance_Amt = objVendorInvHead.Document_Total
                            End If
                        End If
                    Next
                    If obj.objPIRemittance IsNot Nothing Then
                        objVendorInvHead.RemittanceObject = New clsRemittance()
                        objVendorInvHead.RemittanceObject.Vendor_Code = obj.objPIRemittance.Vendor_Code
                        objVendorInvHead.RemittanceObject.Vendor_Name = obj.objPIRemittance.Vendor_Name
                        objVendorInvHead.RemittanceObject.Document_No = obj.objPIRemittance.Document_No
                        objVendorInvHead.RemittanceObject.Document_Date = obj.objPIRemittance.Document_Date
                        objVendorInvHead.RemittanceObject.Document_Type = obj.objPIRemittance.Document_Type
                        objVendorInvHead.RemittanceObject.Document_Amount = obj.objPIRemittance.Document_Amount
                        objVendorInvHead.RemittanceObject.Service_Type = obj.objPIRemittance.Service_Type
                        objVendorInvHead.RemittanceObject.Actual_TDS_Base = obj.objPIRemittance.Actual_TDS_Base
                        objVendorInvHead.RemittanceObject.Calculated_TDS_Base = obj.objPIRemittance.Calculated_TDS_Base
                        objVendorInvHead.RemittanceObject.Actual_TDS = obj.objPIRemittance.Actual_TDS
                        objVendorInvHead.RemittanceObject.Calculated_TDS = obj.objPIRemittance.Calculated_TDS
                        objVendorInvHead.RemittanceObject.Actual_Surcharge = obj.objPIRemittance.Actual_Surcharge
                        objVendorInvHead.RemittanceObject.Calculated_Surcharge = obj.objPIRemittance.Calculated_Surcharge
                        objVendorInvHead.RemittanceObject.Actual_Edu_Cess = obj.objPIRemittance.Actual_Edu_Cess
                        objVendorInvHead.RemittanceObject.Calculated_Edu_Cess = obj.objPIRemittance.Calculated_Edu_Cess
                        objVendorInvHead.RemittanceObject.Actual_Sec_Educess = obj.objPIRemittance.Actual_Sec_Educess
                        objVendorInvHead.RemittanceObject.Calculated_Sec_Educess = obj.objPIRemittance.Calculated_Sec_Educess
                        objVendorInvHead.RemittanceObject.Actual_Total_TDS = obj.objPIRemittance.Actual_Total_TDS
                        objVendorInvHead.RemittanceObject.Calculated_Total_TDS = obj.objPIRemittance.Calculated_Total_TDS
                        objVendorInvHead.RemittanceObject.Fiscal_Year = obj.objPIRemittance.Fiscal_Year
                        objVendorInvHead.RemittanceObject.Quarter = obj.objPIRemittance.Quarter
                        objVendorInvHead.RemittanceObject.Section_Code = obj.objPIRemittance.Section_Code
                        objVendorInvHead.RemittanceObject.Section_Description = obj.objPIRemittance.Section_Description
                        objVendorInvHead.RemittanceObject.Branch_Code = obj.objPIRemittance.Branch_Code
                        objVendorInvHead.RemittanceObject.Deduction_Code = obj.objPIRemittance.Deduction_Code
                        objVendorInvHead.RemittanceObject.TDS_Per = obj.objPIRemittance.TDS_Per
                        objVendorInvHead.RemittanceObject.Surcharge_Per = obj.objPIRemittance.Surcharge_Per
                        objVendorInvHead.RemittanceObject.Edu_Cess_Per = obj.objPIRemittance.Edu_Cess_Per
                        objVendorInvHead.RemittanceObject.Sec_Educess_Per = obj.objPIRemittance.Sec_Educess_Per
                        objVendorInvHead.RemittanceObject.Select_By = obj.objPIRemittance.Select_By
                        objVendorInvHead.RemittanceObject.IsTDSOverride = obj.objPIRemittance.IsTDSOverride
                        objVendorInvHead.RemittanceObject.IsApplyTDS = obj.objPIRemittance.IsApplyTDS

                        objVendorInvHead.TDS_Base_Actual_Amount = obj.objPIRemittance.Actual_TDS_Base
                        objVendorInvHead.TDS_Base_Calculated_Amount = obj.objPIRemittance.Calculated_TDS_Base
                        objVendorInvHead.TDS_Percentage = obj.objPIRemittance.TDS_Per
                        objVendorInvHead.TDS_Actual_Amount = obj.objPIRemittance.Actual_Total_TDS
                        objVendorInvHead.TDS_Calculated_Amount = obj.objPIRemittance.Calculated_Total_TDS
                        objVendorInvHead.Nature_of_deduction = obj.objPIRemittance.Deduction_Code
                        objVendorInvHead.Branch_Code = obj.objPIRemittance.Branch_Code
                        objVendorInvHead.Balance_Amt = obj.Total_Amt - obj.objPIRemittance.Actual_Total_TDS
                        objVendorInvHead.Section_Code = obj.objPIRemittance.Section_Code
                    End If
                    If (objVendorInvHead.Arr IsNot Nothing AndAlso objVendorInvHead.Arr.Count > 0) Then
                        objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                        clsVedorInvoiceHead.PostData(FormId, objVendorInvHead.Document_No, "", trans, obj.Document_Date)
                    End If
                End If

                If isCreateDebinNote Then
                    Dim objVendorInvHead As New clsVedorInvoiceHead()
                    objVendorInvHead.Invoice_Entry_Date = clsCommon.myCDate(obj.Document_Date, "dd/MM/yyyy")
                    objVendorInvHead.Vendor_Code = obj.Vendor_Code
                    objVendorInvHead.Vendor_Name = obj.Vendor_Name
                    objVendorInvHead.Vendor_Invoice_No = ""
                    objVendorInvHead.Vendor_Invoice_Date = clsCommon.myCDate(obj.Document_Date, "dd/MM/yyyy")
                    objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.Location_Code, trans)
                    objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.Vendor_Code + "'", trans))
                    If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                        Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.Vendor_Name)
                    End If
                    objVendorInvHead.Against_Asset_Work = obj.Document_Code
                    objVendorInvHead.Document_Type = "D"
                    objVendorInvHead.Invoice_Type = "AP"

                    objVendorInvHead.On_Hold = False
                    objVendorInvHead.Description = "Vendor " + obj.Vendor_Code + "/" + obj.Vendor_Name + " .Against Asset Work " + obj.Document_Code + ""
                    objVendorInvHead.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                    objVendorInvHead.Tax_Group = obj.Tax_Group
                    objVendorInvHead.RefDocType = obj.Ref_Doc_Type
                    objVendorInvHead.RefDocNo = obj.Ref_Doc_No
                    If (clsCommon.myLen(obj.TAX1) > 0) Then
                        objVendorInvHead.TAX1 = obj.TAX1
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX1, trans) Then
                            objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
                            objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX2) > 0) Then
                        objVendorInvHead.TAX2 = obj.TAX2
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX2, trans) Then
                            objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
                            objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX3) > 0) Then
                        objVendorInvHead.TAX3 = obj.TAX3
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX3, trans) Then
                            objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
                            objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX4) > 0) Then
                        objVendorInvHead.TAX4 = obj.TAX4
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX4, trans) Then
                            objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
                            objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX5) > 0) Then
                        objVendorInvHead.TAX5 = obj.TAX5
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX5, trans) Then
                            objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
                            objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX6) > 0) Then
                        objVendorInvHead.TAX6 = obj.TAX6
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX6, trans) Then
                            objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
                            objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX7) > 0) Then
                        objVendorInvHead.TAX7 = obj.TAX7
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX7, trans) Then
                            objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
                            objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX8) > 0) Then
                        objVendorInvHead.TAX8 = obj.TAX8
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX8, trans) Then
                            objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
                            objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX9) > 0) Then
                        objVendorInvHead.TAX9 = obj.TAX9
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX9, trans) Then
                            objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
                            objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX10) > 0) Then
                        objVendorInvHead.TAX10 = obj.TAX10
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX10, trans) Then
                            objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
                            objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.Location_Code, trans)
                        End If
                        objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
                    End If

                    objVendorInvHead.Against_Acquisition = obj.Document_Code
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                        objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.Location_Code, trans)
                        If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                            objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                            objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.Location_Code, trans)
                        End If
                    End If
                    If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                        Throw New Exception("Please set the vendor payable Account")
                    End If
                    objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                    Dim ii As Integer = 0
                    Dim isFirstTime As Boolean = True
                    objVendorInvHead.Total_Landed_Amt = 0
                    For Each objPIDetail As clsAssetWorkDetail In obj.Arr
                        If objPIDetail.Amount < 0 Then
                            qry = "select (case when TSPL_ACQUISITION_DETAIL.Is_Assembled=0 then  TSPL_Dep_AccountSet.Ac_Control else TSPL_Dep_AccountSet.WIP_AC end) as Ac_Control from TSPL_ACQUISITION_DETAIL left outer join  TSPL_Dep_AccountSet on  TSPL_Dep_AccountSet.AcSet_Code=TSPL_ACQUISITION_DETAIL.AcSet_Code where TSPL_ACQUISITION_DETAIL.Asset_Code='" + obj.Asset_Code + "'"
                            dt = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                Throw New Exception("Please set Asset Ctrl Account set for Asset " + obj.Asset_Code)
                            End If

                            Dim strAssetCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Ac_Control"))
                            If clsCommon.myLen(strAssetCtrlAC) <= 0 Then
                                Throw New Exception("Please set WIP Account set for Asset " + obj.Asset_Code)
                            End If
                            strAssetCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strAssetCtrlAC, obj.Location_Code, trans)
                            Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strAssetCtrlAC + "'", trans))

                            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                            ii = ii + 1
                            objVendorInvDetail.Detail_Line_No = ii
                            objVendorInvDetail.GL_Account_Code = strAssetCtrlAC
                            objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
                            objVendorInvDetail.Amount = Math.Abs(objPIDetail.Amount)
                            objVendorInvDetail.Discount_Per = 0
                            objVendorInvDetail.Discount = 0
                            objVendorInvDetail.Amount_less_Discount = Math.Abs(objPIDetail.Amount)
                            objVendorInvDetail.Cost_Centre_Code = objPIDetail.CostCenter_Code
                            objVendorInvDetail.Hirerachy_Code = objPIDetail.Hirerachy_Code
                            objVendorInvDetail.TAX1 = objPIDetail.TAX1
                            objVendorInvDetail.TAX1_Rate = Math.Abs(objPIDetail.TAX1_Rate)
                            objVendorInvDetail.TAX1_Amt = Math.Abs(objPIDetail.TAX1_Amt)
                            objVendorInvDetail.TAX2 = objPIDetail.TAX2
                            objVendorInvDetail.TAX2_Rate = Math.Abs(objPIDetail.TAX2_Rate)
                            objVendorInvDetail.TAX2_Amt = Math.Abs(objPIDetail.TAX2_Amt)
                            objVendorInvDetail.TAX3 = objPIDetail.TAX3
                            objVendorInvDetail.TAX3_Rate = Math.Abs(objPIDetail.TAX3_Rate)
                            objVendorInvDetail.TAX3_Amt = Math.Abs(objPIDetail.TAX3_Amt)
                            objVendorInvDetail.TAX4 = objPIDetail.TAX4
                            objVendorInvDetail.TAX4_Rate = Math.Abs(objPIDetail.TAX4_Rate)
                            objVendorInvDetail.TAX4_Amt = Math.Abs(objPIDetail.TAX4_Amt)
                            objVendorInvDetail.TAX5 = objPIDetail.TAX5
                            objVendorInvDetail.TAX5_Rate = Math.Abs(objPIDetail.TAX5_Rate)
                            objVendorInvDetail.TAX5_Amt = Math.Abs(objPIDetail.TAX5_Amt)
                            objVendorInvDetail.TAX6 = objPIDetail.TAX6
                            objVendorInvDetail.TAX6_Rate = Math.Abs(objPIDetail.TAX6_Rate)
                            objVendorInvDetail.TAX6_Amt = Math.Abs(objPIDetail.TAX6_Amt)
                            objVendorInvDetail.TAX7 = objPIDetail.TAX7
                            objVendorInvDetail.TAX7_Rate = Math.Abs(objPIDetail.TAX7_Rate)
                            objVendorInvDetail.TAX7_Amt = Math.Abs(objPIDetail.TAX7_Amt)
                            objVendorInvDetail.TAX8 = objPIDetail.TAX8
                            objVendorInvDetail.TAX8_Rate = Math.Abs(objPIDetail.TAX8_Rate)
                            objVendorInvDetail.TAX8_Amt = Math.Abs(objPIDetail.TAX8_Amt)
                            objVendorInvDetail.TAX9 = objPIDetail.TAX9
                            objVendorInvDetail.TAX9_Rate = Math.Abs(objPIDetail.TAX9_Rate)
                            objVendorInvDetail.TAX9_Amt = Math.Abs(objPIDetail.TAX9_Amt)
                            objVendorInvDetail.TAX10 = objPIDetail.TAX10
                            objVendorInvDetail.TAX10_Rate = Math.Abs(objPIDetail.TAX10_Rate)
                            objVendorInvDetail.TAX10_Amt = Math.Abs(objPIDetail.TAX10_Amt)
                            objVendorInvDetail.Total_Tax = Math.Abs(objPIDetail.Total_Tax_Amt)
                            objVendorInvDetail.Total_Amount = objPIDetail.Item_Net_Amt
                            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                objVendorInvHead.Arr.Add(objVendorInvDetail)
                                If Math.Abs(objPIDetail.TAX1_Amt) > 0 Then
                                    objVendorInvHead.Tax1_BAmount += Math.Abs(objPIDetail.TAX1_Base_Amt)
                                    objVendorInvHead.TAX1_Amt += Math.Abs(objPIDetail.TAX1_Amt)
                                End If
                                If Math.Abs(objPIDetail.TAX2_Amt) > 0 Then
                                    objVendorInvHead.Tax2_BAmount += Math.Abs(objPIDetail.TAX2_Base_Amt)
                                    objVendorInvHead.TAX2_Amt += Math.Abs(objPIDetail.TAX2_Amt)
                                End If
                                If Math.Abs(objPIDetail.TAX3_Amt) > 0 Then
                                    objVendorInvHead.Tax3_BAmount += Math.Abs(objPIDetail.TAX3_Base_Amt)
                                    objVendorInvHead.TAX3_Amt += Math.Abs(objPIDetail.TAX3_Amt)
                                End If
                                If Math.Abs(objPIDetail.TAX4_Amt) > 0 Then
                                    objVendorInvHead.Tax4_BAmount += Math.Abs(objPIDetail.TAX4_Base_Amt)
                                    objVendorInvHead.TAX4_Amt += Math.Abs(objPIDetail.TAX4_Amt)
                                End If
                                If Math.Abs(objPIDetail.TAX5_Amt) > 0 Then
                                    objVendorInvHead.Tax5_BAmount += Math.Abs(objPIDetail.TAX5_Base_Amt)
                                    objVendorInvHead.TAX5_Amt += Math.Abs(objPIDetail.TAX5_Amt)
                                End If
                                If Math.Abs(objPIDetail.TAX6_Amt) > 0 Then
                                    objVendorInvHead.Tax6_BAmount += Math.Abs(objPIDetail.TAX6_Base_Amt)
                                    objVendorInvHead.TAX6_Amt += Math.Abs(objPIDetail.TAX6_Amt)
                                End If
                                If Math.Abs(objPIDetail.TAX7_Amt) > 0 Then
                                    objVendorInvHead.Tax7_BAmount += (objPIDetail.TAX7_Base_Amt)
                                    objVendorInvHead.TAX7_Amt += Math.Abs(objPIDetail.TAX7_Amt)
                                End If
                                If Math.Abs(objPIDetail.TAX8_Amt) > 0 Then
                                    objVendorInvHead.Tax8_BAmount += Math.Abs(objPIDetail.TAX8_Base_Amt)
                                    objVendorInvHead.TAX8_Amt += Math.Abs(objPIDetail.TAX8_Amt)
                                End If
                                If Math.Abs(objPIDetail.TAX9_Amt) > 0 Then
                                    objVendorInvHead.Tax9_BAmount += Math.Abs(objPIDetail.TAX9_Base_Amt)
                                    objVendorInvHead.TAX9_Amt += Math.Abs(objPIDetail.TAX9_Amt)
                                End If
                                If Math.Abs(objPIDetail.TAX10_Amt) > 0 Then
                                    objVendorInvHead.Tax10_BAmount += Math.Abs(objPIDetail.TAX10_Base_Amt)
                                    objVendorInvHead.TAX10_Amt += Math.Abs(objPIDetail.TAX10_Amt)
                                End If
                                objVendorInvHead.Total_Tax += Math.Abs(objPIDetail.Total_Tax_Amt)
                                objVendorInvHead.Discount_Base += Math.Abs(objPIDetail.Amount)
                                objVendorInvHead.Discount_Amount = 0
                                objVendorInvHead.Amount_Less_Discount += Math.Abs(objPIDetail.Amount)
                                objVendorInvHead.Document_Total += Math.Abs(objPIDetail.Amount) + Math.Abs(objPIDetail.Total_Tax_Amt)
                                objVendorInvHead.Balance_Amt = objVendorInvHead.Document_Total
                            End If
                        End If
                    Next
                    If (objVendorInvHead.Arr IsNot Nothing AndAlso objVendorInvHead.Arr.Count > 0) Then
                        objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                        clsVedorInvoiceHead.PostData(FormId, objVendorInvHead.Document_No, "", trans, obj.Document_Date)
                    End If
                End If
            Else
                CreateJournalEntry(obj, trans)
            End If
            qry = "Update TSPL_ASSET_WORK_HEAD set Status=1, Post_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim UDLCapexAcquisionEntry As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.UDLCapexAcquisionEntry, clsFixedParameterCode.UDLCapexAcquisionEntry, trans)) = "1", True, False))
            If UDLCapexAcquisionEntry Then
                Dim qrySalvageRate As Double = clsDBFuncationality.getSingleValue("select Book_Salvage_Rate  from TSPL_ACQUISITION_DETAIL where Asset_Code  ='" & obj.Asset_Code & "'", trans)
                Dim chkStatusForAcquisitionEntry As Integer = clsDBFuncationality.getSingleValue("   select TSPL_ACQUISITION_HEAD.Status  from TSPL_ACQUISITION_DETAIL " &
                    " left join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_HEAD.Acquisition_Code =TSPL_ACQUISITION_DETAIL.Acquisition_Code " &
                     " where TSPL_ACQUISITION_DETAIL.Asset_Code ='" & obj.Asset_Code & "'", trans)
                If chkStatusForAcquisitionEntry = 0 Then
                    qry = "  UPDATE TSPL_ACQUISITION_DETAIL SET Book_Source_value=Book_Source_value+Exp_AMT.Amount,Book_Source_Original_value=Book_Source_Original_value+Exp_AMT.Amount,Book_Salvage_Value= (Book_Source_value+Exp_AMT.Amount)* " & qrySalvageRate & " / 100,Item_Net_Amt=Item_Net_Amt+Exp_AMT.Amount FROM  (" &
                     " SELECT TSPL_ASSET_WORK_DETAIL.Asset_Code,SUM(TSPL_ASSET_WORK_DETAIL.Item_Net_Amt  ) AS Amount  FROM TSPL_ASSET_WORK_DETAIL " &
                    " INNER JOIN TSPL_ASSET_WORK_HEAD ON TSPL_ASSET_WORK_DETAIL.Document_Code =TSPL_ASSET_WORK_HEAD.Document_Code   WHERE TSPL_ASSET_WORK_DETAIL.Asset_Code='" & obj.Asset_Code & "'  group by TSPL_ASSET_WORK_DETAIL.Asset_Code" &
                    " ) AS Exp_AMT " &
                    " where TSPL_ACQUISITION_DETAIL.Asset_Code = Exp_AMT.Asset_Code "

                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "   UPDATE TSPL_ACQUISITION_HEAD SET Total_Amt=Total_Amt+Exp_AMT.Amount,Net_Amt=Net_Amt+Exp_AMT.Amount FROM  (" &
                            " SELECT TSPL_ASSET_WORK_DETAIL.Asset_Code,SUM(TSPL_ASSET_WORK_DETAIL.Item_Net_Amt  ) AS Amount  FROM TSPL_ASSET_WORK_DETAIL " &
                             " INNER JOIN TSPL_ASSET_WORK_HEAD ON TSPL_ASSET_WORK_DETAIL.Document_Code =TSPL_ASSET_WORK_HEAD.Document_Code   WHERE TSPL_ASSET_WORK_DETAIL.Asset_Code='" & obj.Asset_Code & "'  group by TSPL_ASSET_WORK_DETAIL.Asset_Code" &
                            ") AS Exp_AMT " &
                            " where TSPL_ACQUISITION_HEAD.Acquisition_Code IN (SELECT Acquisition_Code FROM  TSPL_ACQUISITION_DETAIL WHERE Asset_Code='" & obj.Asset_Code & "')"

                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CreateJournalEntry(ByVal obj As clsAssetWorkHead, ByVal trans As SqlTransaction)

        Try
            'Dim StopGLForConsignment As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.StopGLEntryForConsignmentAtCSATransfer, clsFixedParameterCode.StopGLEntryForConsignmentAtCSATransfer, trans))
            ' ''if gl setting is ON then no consignment a/c debit and no GOSC a/c credit

            'Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)

            'If StopGLForConsignment = "1" Then
            '    isSkipCogsGL = False
            'End If

            'Dim obj As New clsCSATransfer
            'obj = clsCSATransfer.GetData(strCode, NavigatorType.Current, trans)
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim strAc_Expense As String = ""
            Dim strAc_Control As String = ""
            Dim isSaved As Boolean = True
            Dim qry As String
            Dim InnerQry As String



            InnerQry = " select Asset_Work.Asset_Code,Asset_Work.Ac_Control,GL_Asset.Description as Ac_Control_Desc,Asset_Work.Add_Charges_Code, " &
                       " Asset_Work.Ac_Expense,GL_Expense.Description as Ac_Expense_Desc,Asset_Work.Item_Net_Amt,Asset_Work.Document_Net_Amount,Asset_Work.costCenter_code,Asset_Work.Hirerachy_code  from ( " &
                       " select TSPL_ASSET_WORK_HEAD.Asset_Code,(case when TSPL_ACQUISITION_DETAIL.Is_Assembled=0 then  TSPL_Dep_AccountSet.Ac_Control else TSPL_Dep_AccountSet.WIP_AC end) as Ac_Control, " &
                       " TSPL_ASSET_WORK_DETAIL.Add_Charges_Code,coalesce(TSPL_Additional_Charges.Account_Code,TSPL_ASSET_WORK_DETAIL.gl_account_code) as Ac_Expense,TSPL_ASSET_WORK_DETAIL.Item_Net_Amt,TSPL_ASSET_WORK_HEAD.Net_Amt as Document_Net_Amount,TSPL_ASSET_WORK_DETAIL.costCenter_code,TSPL_ASSET_WORK_DETAIL.Hirerachy_code  " &
                       " from TSPL_ASSET_WORK_DETAIL inner join TSPL_ASSET_WORK_HEAD on TSPL_ASSET_WORK_DETAIL.Document_Code=TSPL_ASSET_WORK_HEAD.Document_Code " &
                       " left join TSPL_ACQUISITION_DETAIL on TSPL_ASSET_WORK_HEAD.Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code " &
                       " left outer join  TSPL_Dep_AccountSet on  TSPL_Dep_AccountSet.AcSet_Code=TSPL_ACQUISITION_DETAIL.AcSet_Code " &
                       " left join TSPL_Additional_Charges on TSPL_ASSET_WORK_DETAIL.Add_Charges_Code=TSPL_Additional_Charges.Code " &
                       " where TSPL_ASSET_WORK_HEAD.Document_Code='" & obj.Document_Code & "') Asset_Work " &
                       " left join TSPL_GL_ACCOUNTS GL_Asset on GL_Asset.Account_Code=Asset_Work.Ac_Control " &
                       " left join TSPL_GL_ACCOUNTS GL_Expense on GL_Expense.Account_Code=Asset_Work.Ac_Expense "
            qry = InnerQry

            '' Validation of GL Itemwise
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows

                    '' dr Ac_Control acc
                    strAc_Control = clsCommon.myCstr(dr.Item("Ac_Control"))
                    strAc_Control = clsERPFuncationality.ChangeGLAccountLocationSegment(strAc_Control, obj.Location_Code, trans)
                    If clsCommon.myLen(strAc_Control) = 0 Then
                        Throw New Exception("Please set Aset Contro/WIP Account for Asset " & clsCommon.myCstr(dr.Item("Asset_Code")) & "")
                    End If

                    '' cr Inv_Control_Account
                    strAc_Expense = clsCommon.myCstr(dr.Item("Ac_Expense"))
                    strAc_Expense = clsERPFuncationality.ChangeGLAccountLocationSegment(strAc_Expense, obj.Location_Code, trans)
                    If clsCommon.myLen(strAc_Expense) = 0 Then
                        Throw New Exception("Please set Expense Account for Addition Charge " & clsCommon.myCstr(dr.Item("Add_Charges_Code")) & "")
                    End If

                Next
            End If
            '' Create Financial Entry
            'qry = " select Cost_Of_Goods_Sold,Cost_Of_Goods_Sold_Desc,Consignment_Acct,Consignment_Acct_Desc," & _
            '      " Inv_Control_Account,Inv_Control_Account_Desc,GSOC_Acct,GSOC_Acct_Desc,SUM(qty) as qty, " & _
            '      " SUM(Transfer_Amount) as Transfer_Amount,SUM(coalesce(Item_Cost,0)) as Item_Cost from ( " & InnerQry & "" & _
            '      " ) as Final group by Cost_Of_Goods_Sold,Cost_Of_Goods_Sold_Desc, " & _
            '      " Consignment_Acct, Consignment_Acct_Desc, Inv_Control_Account, Inv_Control_Account_Desc,GSOC_Acct,GSOC_Acct_Desc"

            'dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                strAc_Control = clsCommon.myCstr(dt.Rows(0).Item("Ac_Control"))
                strAc_Control = clsERPFuncationality.ChangeGLAccountLocationSegment(strAc_Control, obj.Location_Code, trans)
                Dim Acc1() As String = {strAc_Control, 1 * clsCommon.myCdbl(dt.Rows(0)("Document_Net_Amount"))}
                ArryLstGLAC.Add(Acc1)
                For Each dr As DataRow In dt.Rows
                    '' cr Inv_Control_Account
                    strAc_Expense = clsERPFuncationality.ChangeGLAccountLocationSegment(strAc_Expense, obj.Location_Code, trans)
                    If clsCommon.myLen(strAc_Expense) = 0 Then
                        Throw New Exception("Please set Expense Account for Addition Charge " & clsCommon.myCstr(dr.Item("Add_Charges_Code")) & "")
                    End If
                    Dim Acc2() As String = {strAc_Expense, -1 * clsCommon.myCdbl(dr("Item_Net_Amt")), "", "", clsCommon.myCstr(dr("Hirerachy_code")), clsCommon.myCstr(dr("costCenter_code"))}
                    ArryLstGLAC.Add(Acc2)
                Next
            End If


            Dim GLDesc As String = "Journal Entry Against Asset Work Expenses- Doc No." & obj.Document_Code & " "
            Dim Remarks As String = "Journal Entry against Asset Work Expenses for Asset Code: - " & obj.Asset_Code & "  For Doc No. " & obj.Document_Code & ""

            '====================if already JV exist then update only========================
            qry = "select voucher_no from tspl_journal_master where Source_Doc_No='" + obj.Document_Code + "' and source_code='AS-WK'"
            Dim strRecreateVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

            If clsCommon.myLen(strRecreateVoucherNo) > 0 Then
                isSaved = clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Code, False, strRecreateVoucherNo, trans, obj.Document_Date, GLDesc, "AS-WK", "Asset Work", obj.Document_Date, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , Remarks, "", Nothing)
            Else
                isSaved = clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Code, False, trans, obj.Document_Date, GLDesc, "AS-WK", "Asset Work", obj.Document_Code, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , Remarks, "", Nothing)
            End If
            '==========================
            If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyFinancialCostCenter, clsFixedParameterCode.ApplyFinancialCostCenter, trans)) = "1", True, False)) = True Then
                Dim strCostCenterCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Top 1 CostCenter_Code from TSPL_ASSET_WORK_DETAIL where Document_Code = '" + obj.Document_Code + "'", trans))
                Dim strHirerachyCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Top 1 Hirerachy_Code from TSPL_ASSET_WORK_DETAIL where Document_Code =  '" + obj.Document_Code + "'", trans))
                clsERPFuncationality.UpdateCostCenterAndHirerachyCodeOnJE(obj.Document_Code, "AS-WK", strCostCenterCode, strHirerachyCode, trans)
            End If
            '==========================
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsAssetWorkHead = clsAssetWorkHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
            Try
                If (obj.Status = ERPTransactionStatus.Approved) Then
                    Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Post_Date, "dd/MM/yyyy"))
                End If
                Dim qry As String = "delete from TSPL_ASSET_WORK_DETAIL where Document_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_ASSET_WORK_HEAD where Document_Code='" + strCode + "'"
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
        Return isSaved
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            Dim obj As clsAssetWorkHead = clsAssetWorkHead.GetData(strCode, NavigatorType.Current, trans)
            If Not obj.Status = ERPTransactionStatus.Approved Then
                Throw New Exception("Transaction [" + obj.Document_Code + "] should be posted for reverse and unpost")
            End If

            Dim qry As String = "select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_Asset_Work='" + obj.Document_Code + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    clsVedorInvoiceHead.ReverseAndUnpost(clsCommon.myCstr(dr("Document_No")), trans)
                    clsVedorInvoiceHead.DeleteData(clsCommon.myCstr(dr("Document_No")), trans)
                Next
            End If
            qry = "select Voucher_No from TSPL_JOURNAL_MASTER where source_doc_no='" + obj.Document_Code + "' AND  Source_Code='AS-WK'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    qry = "delete from TSPL_JOURNAL_DETAILS where TSPL_JOURNAL_DETAILS.Voucher_No in (select Voucher_No from  TSPL_JOURNAL_MASTER where Source_Doc_No='" & clsCommon.myCstr(dr("Voucher_No")) & "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Source_Doc_No ='" & clsCommon.myCstr(dr("Voucher_No")) & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Next
            End If

            qry = "   update TSPL_ASSET_WORK_HEAD set Status=0,Post_Date=null where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsAssetWorkDetail
#Region "Variables"
    Public Document_Code As String = Nothing
    Public SNo As Integer = 0
    Public Add_Charges_Code As String = Nothing
    Public AssetsCode As String = Nothing
    Public Add_Charges_Name As String = Nothing
    Public GL_Account_Code As String = Nothing
    Public GL_Account_Name As String = Nothing
    Public Amount As Double = 0
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
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0
    Public IsUnclaimedTax As Integer = 0
    Public Hirerachy_Code As String = Nothing
    Public CostCenter_Code As String = Nothing
    Public CostCenter_Name As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsAssetWorkDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsAssetWorkDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Asset_Code", obj.AssetsCode)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "Add_Charges_Code", obj.Add_Charges_Code, True)
                clsCommon.AddColumnsForChange(coll, "Hirerachy_Code", obj.Hirerachy_Code, True)
                clsCommon.AddColumnsForChange(coll, "CostCenter_Code", obj.CostCenter_Code, True)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
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
                clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                clsCommon.AddColumnsForChange(coll, "GL_Account_Code", obj.GL_Account_Code)
                clsCommon.AddColumnsForChange(coll, "IsUnclaimedTax", obj.IsUnclaimedTax)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_WORK_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class

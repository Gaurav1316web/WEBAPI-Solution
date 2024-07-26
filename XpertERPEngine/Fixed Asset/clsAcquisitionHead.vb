Imports common
Imports System.Data.SqlClient
Public Class clsAcquisitionHead
#Region "Variables"
    Public statusnewold As String = Nothing
    Public Acquisition_Code As String = Nothing
    Public Assemble_Code As String = Nothing
    Public Acquisition_Date As DateTime = Nothing
    'Public Posting_Date As Date = Nothing
    Public Loc_Code As String = Nothing
    Public PO_No As String = Nothing
    Public SRN_No As String = Nothing
    Public PI_No As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Description As String = Nothing
    Public Vendor_Invoice_No As String = Nothing
    Public Remarks As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public On_Hold As Boolean = Nothing
    Public IS_Assemble As Boolean = Nothing
    Public Is_Visi_Type As Boolean = False
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
    Public Templete_Code As String = ""

    Public Total_Amt As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Net_Amt As Double = 0
    Public Post_Date As DateTime? = Nothing
    Public Acquisition_Type As String = ""
    Public Arr As List(Of clsAcquisitionDetail) = Nothing
    Public ArrAssemble As List(Of clsAssetAssembleDetail) = Nothing
    Public RemittanceObject As clsRemittance = Nothing

    Public Balance_Amt As Double = 0
    Public TDS_Base_Actual_Amount As Double = 0
    Public TDS_Base_Calculated_Amount As Double = 0
    Public TDS_Percentage As Double = 0
    Public TDS_Actual_Amount As Double = 0
    Public TDS_Calculated_Amount As Double = 0
    Public Nature_of_deduction As String = Nothing
    Public Branch_Code As String = Nothing
    Public Section_Code As String = Nothing
    'Public Arr1 As List(Of clsAcquisitionPendingSRN) = Nothing
    Public CapexSub_Code As String = Nothing
    Public Capex_Code As String = Nothing
    Public Add_Charge_Code1 As String = Nothing
    Public Add_Charge_Name1 As String = Nothing
    Public Add_Charge_Amt1 As Double = 0
    Public Add_Charge_Code2 As String = Nothing
    Public Add_Charge_Name2 As String = Nothing
    Public Add_Charge_Amt2 As Double = 0
    Public Add_Charge_Code3 As String = Nothing
    Public Add_Charge_Name3 As String = Nothing
    Public Add_Charge_Amt3 As Double = 0
    Public Add_Charge_Code4 As String = Nothing
    Public Add_Charge_Name4 As String = Nothing
    Public Add_Charge_Amt4 As Double = 0
    Public Add_Charge_Code5 As String = Nothing
    Public Add_Charge_Name5 As String = Nothing
    Public Add_Charge_Amt5 As Double = 0
    Public Add_Charge_Code6 As String = Nothing
    Public Add_Charge_Name6 As String = Nothing
    Public Add_Charge_Amt6 As Double = 0
    Public Add_Charge_Code7 As String = Nothing
    Public Add_Charge_Name7 As String = Nothing
    Public Add_Charge_Amt7 As Double = 0
    Public Add_Charge_Code8 As String = Nothing
    Public Add_Charge_Name8 As String = Nothing
    Public Add_Charge_Amt8 As Double = 0
    Public Add_Charge_Code9 As String = Nothing
    Public Add_Charge_Name9 As String = Nothing
    Public Add_Charge_Amt9 As Double = 0
    Public Add_Charge_Code10 As String = Nothing
    Public Add_Charge_Name10 As String = Nothing
    Public Add_Charge_Amt10 As Double = 0
    Public Total_Add_Charge As Double = 0

    Public Tax_Recoverable As Double = 0
    Public Tax_Non_Recoverable As Double = 0
    Public Opening_Assemble As Boolean
    Public Opening_Assemble_Amt As Decimal = 0
    Public Opening_Direct As Boolean
#End Region

    Public Function SaveData(ByVal obj As clsAcquisitionHead, ByVal isNewEntry As Boolean, ByVal isMakeAbandomentNo As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, isMakeAbandomentNo, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsAcquisitionHead, ByVal isNewEntry As Boolean, ByVal isMakeAbandomentNo As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFixedAsset, clsUserMgtCode.FAAcquisitionEntry, obj.Loc_Code, obj.Acquisition_Date, trans)
        Try
            If Not isNewEntry Then
                Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Status from TSPL_ACQUISITION_HEAD Where Acquisition_Code='" + obj.Acquisition_Code + "'", trans))
                If Status = 1 Then
                    Throw New Exception("This document is already posted.")
                End If
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Acquisition_Code, "TSPL_ACQUISITION_HEAD", "Acquisition_Code", "TSPL_ACQUISITION_DETAIL", "Acquisition_Code", "TSPL_REMITTANCE", "Document_No", trans)
            End If
            '' delete assemble detail
            Dim qry As String = "delete from TSPL_ASSET_ASSEMBLE_DETAIL where Acquisition_Code='" + obj.Acquisition_Code + "'  "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim arry As New ArrayList
            arry = GetListOfAssetAgainstIssue(obj.Acquisition_Code, True, trans)

            qry = "delete from TSPL_REMITTANCE where Document_No='" + obj.Acquisition_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_ACQUISITION_DETAIL where Acquisition_Code='" + obj.Acquisition_Code + "' and asset_Code not in (" + clsCommon.GetMulcallString(arry) + ") "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

         

            Dim strDocNo As String = ""

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Acquisition_Date", clsCommon.GetPrintDate(obj.Acquisition_Date, "dd/MMM/yyyy hh:mm tt"))            
            clsCommon.AddColumnsForChange(coll, "PO_No", obj.PO_No, True)
            clsCommon.AddColumnsForChange(coll, "SRN_No", obj.SRN_No, True)
            clsCommon.AddColumnsForChange(coll, "PI_No", obj.PI_No, True)
            clsCommon.AddColumnsForChange(coll, "Loc_Code", obj.Loc_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Vendor_Invoice_No", obj.Vendor_Invoice_No)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "IS_Assemble", IIf(obj.IS_Assemble, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Visi_Type", IIf(obj.Is_Visi_Type, 1, 0))
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
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Status_New_Old", obj.statusnewold)
            clsCommon.AddColumnsForChange(coll, "Templete_Code", obj.Templete_Code, True)
            clsCommon.AddColumnsForChange(coll, "Acquisition_Type", obj.Acquisition_Type, True)
            clsCommon.AddColumnsForChange(coll, "TDS_Base_Actual_Amount", obj.TDS_Base_Actual_Amount)
            clsCommon.AddColumnsForChange(coll, "TDS_Base_Calculated_Amount", obj.TDS_Base_Calculated_Amount)
            clsCommon.AddColumnsForChange(coll, "TDS_Percentage", obj.TDS_Percentage)
            clsCommon.AddColumnsForChange(coll, "TDS_Actual_Amount", Math.Round(obj.TDS_Actual_Amount, 0, MidpointRounding.AwayFromZero))
            clsCommon.AddColumnsForChange(coll, "TDS_Calculated_Amount", obj.TDS_Calculated_Amount)
            clsCommon.AddColumnsForChange(coll, "Nature_of_deduction", obj.Nature_of_deduction)
            clsCommon.AddColumnsForChange(coll, "Branch_Code", obj.Branch_Code)
            clsCommon.AddColumnsForChange(coll, "Section_Code", obj.Section_Code)
            clsCommon.AddColumnsForChange(coll, "Balance_Amt", obj.Net_Amt)

            clsCommon.AddColumnsForChange(coll, "CapexSub_Code", obj.CapexSub_Code)
            clsCommon.AddColumnsForChange(coll, "Capex_Code", obj.Capex_Code)

            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code1", obj.Add_Charge_Code1)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name1", obj.Add_Charge_Name1)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt1", obj.Add_Charge_Amt1)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code2", obj.Add_Charge_Code2)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name2", obj.Add_Charge_Name2)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt2", obj.Add_Charge_Amt2)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code3", obj.Add_Charge_Code3)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name3", obj.Add_Charge_Name3)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt3", obj.Add_Charge_Amt3)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code4", obj.Add_Charge_Code4)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name4", obj.Add_Charge_Name4)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt4", obj.Add_Charge_Amt4)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code5", obj.Add_Charge_Code5)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name5", obj.Add_Charge_Name5)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt5", obj.Add_Charge_Amt5)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code6", obj.Add_Charge_Code6)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name6", obj.Add_Charge_Name6)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt6", obj.Add_Charge_Amt6)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code7", obj.Add_Charge_Code7)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name7", obj.Add_Charge_Name7)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt7", obj.Add_Charge_Amt7)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code8", obj.Add_Charge_Code8)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name8", obj.Add_Charge_Name8)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt8", obj.Add_Charge_Amt8)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code9", obj.Add_Charge_Code9)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name9", obj.Add_Charge_Name9)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt9", obj.Add_Charge_Amt9)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code10", obj.Add_Charge_Code10)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name10", obj.Add_Charge_Name10)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt10", obj.Add_Charge_Amt10)

            clsCommon.AddColumnsForChange(coll, "Total_Add_Charge", obj.Total_Add_Charge)
            clsCommon.AddColumnsForChange(coll, "Tax_Recoverable", obj.Tax_Recoverable)
            clsCommon.AddColumnsForChange(coll, "Tax_Non_Recoverable", obj.Tax_Non_Recoverable)
            clsCommon.AddColumnsForChange(coll, "Opening_Assemble", If(obj.Opening_Assemble = True, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Opening_Assemble_Amt", obj.Opening_Assemble_Amt)
            clsCommon.AddColumnsForChange(coll, "Opening_Direct", If(obj.Opening_Direct = True, 1, 0))

            If isNewEntry Then
                obj.Acquisition_Code = clsERPFuncationality.GetNextCode(trans, obj.Acquisition_Date, clsDocType.AcquisitionEntry, "", obj.Loc_Code)
                If (clsCommon.myLen(obj.Acquisition_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Acquisition_Code", obj.Acquisition_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACQUISITION_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACQUISITION_HEAD", OMInsertOrUpdate.Update, "TSPL_ACQUISITION_HEAD.Acquisition_Code='" + obj.Acquisition_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsAcquisitionDetail.SaveData(obj.Acquisition_Code, Arr, trans)
            'isSaved = isSaved AndAlso clsAcquisitionPendingSRN.SaveData(obj.Acquisition_Code, Arr1, trans)
            isSaved = isSaved AndAlso clsRemittance.SaveData(obj.RemittanceObject, obj.Acquisition_Code, Loc_Code, trans)
            isSaved = isSaved AndAlso clsAssetAssembleDetail.SaveData(obj.Acquisition_Code, ArrAssemble, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetAssembledInfo(ByVal lstAsset As ArrayList, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = ""
        qry = GetAssembleQuery(lstAsset)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function
    Public Shared Function GetAssembleQuery(ByVal lstAsset As ArrayList) As String
        Dim qry As String = String.Empty

        qry = "   select 'Misc' as Type,TSPL_ASSET_WORK_HEAD.Document_Code as [Document Code],convert(varchar,TSPL_ASSET_WORK_HEAD.Document_Date,103) as [Document Date],TSPL_ASSET_WORK_head.Asset_Code as [Asset Code],TSPL_ASSET_WORK_DETAIL.Add_Charges_Code as [Item No],TSPL_ASSET_WORK_DETAIL.Add_Charges_Code as [Expenses],TSPL_ASSET_WORK_DETAIL.Amount,TSPL_ASSET_WORK_DETAIL.Total_Tax_Amt [Tax Amount],TSPL_ASSET_WORK_DETAIL.Item_Net_Amt as [Net Amount],'' as Hierarchy,'' as CostCenter from TSPL_ACQUISITION_DETAIL  left outer join TSPL_ACQUISITION_head on TSPL_ACQUISITION_head.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code left outer join TSPL_ASSET_WORK_DETAIL on TSPL_ASSET_WORK_DETAIL.Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code left outer join TSPL_ASSET_WORK_HEAD on TSPL_ASSET_WORK_DETAIL.Document_Code=TSPL_ASSET_WORK_HEAD.Document_Code"
        qry += " where TSPL_ASSET_WORK_head.Asset_Code in  (" & clsCommon.GetMulcallString(lstAsset) & ") and TSPL_ASSET_WORK_HEAD.Status=1"
        qry += " union all "
        qry += " select 'Item' as Type,TSPL_IssueItemToAssembledAsset_Head.Doc_No  as [Document Code],convert(varchar,TSPL_IssueItemToAssembledAsset_Head.Doc_Date ,103) as [Document Date],TSPL_IssueItemToAssembledAsset_Detail.Asset_Code as [Asset Code],TSPL_IssueItemToAssembledAsset_Detail.Item_Code as [Item No],TSPL_IssueItemToAssembledAsset_Detail.Item_Desc  as [Expenses],case when TSPL_IssueItemToAssembledAsset_Head.Doc_Type='Return' then (Amount*-1) else Amount end,case when TSPL_IssueItemToAssembledAsset_Head.Doc_Type='Return' then (TSPL_IssueItemToAssembledAsset_Detail.Total_Tax_Amt*-1) else TSPL_IssueItemToAssembledAsset_Detail.Total_Tax_Amt end [Tax Amount],case when TSPL_IssueItemToAssembledAsset_Head.Doc_Type='Return' then (TSPL_IssueItemToAssembledAsset_Detail.Item_Net_Amt*-1) else TSPL_IssueItemToAssembledAsset_Detail.Item_Net_Amt end as [Net Amount] "
        qry += " ,isnull(TSPL_IssueItemToAssembledAsset_Head.H_Code,'') as Hierarchy,isnull(TSPL_IssueItemToAssembledAsset_Head.CC_Code,'') as CostCenter "
        qry += " from TSPL_ACQUISITION_DETAIL"
        qry += " left outer join TSPL_ACQUISITION_head on TSPL_ACQUISITION_head.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code "
        qry += " left outer join TSPL_IssueItemToAssembledAsset_Detail on TSPL_IssueItemToAssembledAsset_Detail.Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code "
        qry += " left outer join TSPL_IssueItemToAssembledAsset_Head on TSPL_IssueItemToAssembledAsset_Detail.Doc_No =TSPL_IssueItemToAssembledAsset_Head.Doc_No "
        qry += " where TSPL_IssueItemToAssembledAsset_Detail.Asset_Code in (" & clsCommon.GetMulcallString(lstAsset) & ") and TSPL_IssueItemToAssembledAsset_Head.Status=1"
        Return qry
    End Function
    Public Shared Function GetAssembleCostQuery(ByVal Acquisition_Code As String, ByVal lstAsset As ArrayList) As String
        
        Dim qry As String = GetAssembleQuery(lstAsset)
        qry = "select Assemble.Type,Assemble.[Document Code],Assemble.[Document Date],Assemble.[Asset Code],Assemble.[Item No],Assemble.[Expenses],Assemble.Amount,Assemble.[Tax Amount],Assemble.[Net Amount],coalesce(TSPL_ASSET_ASSEMBLE_DETAIL.Distribute,'N') as Distribute,coalesce(TSPL_ASSET_ASSEMBLE_DETAIL.Distribute_Amount,0) as [Distribute Amount],coalesce(TSPL_ASSET_ASSEMBLE_DETAIL.Total_Amount,Assemble.[Net Amount]) as [Total Amount] " & _
        " ,Assemble.Hierarchy as [Hierarchy]" & _
        " ,Assemble.CostCenter as [CostCenter]" & _
        " from (" & qry & ") Assemble " & _
            " left join TSPL_ASSET_ASSEMBLE_DETAIL on Assemble.[Asset Code]=TSPL_ASSET_ASSEMBLE_DETAIL.Asset_Code and Assemble.[Document Code]=TSPL_ASSET_ASSEMBLE_DETAIL.Document_Code and Assemble.[Item No]=TSPL_ASSET_ASSEMBLE_DETAIL.Item_No "
        'where Acquisition_Code='" & Acquisition_Code & "'
        Return qry
    End Function
    Public Shared Function GetAssembleDetail(ByVal strDocNo As String, ByVal lstAsset As ArrayList, ByVal trans As SqlTransaction) As List(Of clsAssetAssembleDetail)
        Dim qry As String = ""
        Dim objList As New List(Of clsAssetAssembleDetail)
        Dim objTr As New clsAssetAssembleDetail
        qry = GetAssembleCostQuery(strDocNo, lstAsset)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        For Each drData As DataRow In dt.Rows
            objTr = New clsAssetAssembleDetail
            objTr.Acquisition_Code = strDocNo
            objTr.Asset_Code = clsCommon.myCstr(drData.Item("Asset Code"))
            objTr.Line_No = dt.Rows.IndexOf(drData) + 1
            objTr.Type = clsCommon.myCstr(drData.Item("Type"))
            objTr.Document_Code = clsCommon.myCstr(drData.Item("Document Code"))
            objTr.Document_Date = clsCommon.myCDate(drData.Item("Document Date"))
            objTr.Item_No = clsCommon.myCstr(drData.Item("Item No"))
            objTr.Item_Desc = clsCommon.myCstr(drData.Item("Expenses"))
            objTr.Amount = clsCommon.myCdbl(drData.Item("Amount"))
            objTr.Total_Tax_Amt = clsCommon.myCdbl(drData.Item("Tax Amount"))
            objTr.Item_Net_Amt = clsCommon.myCdbl(drData.Item("Net Amount"))
            objTr.Distribute = clsCommon.myCstr(drData.Item("Distribute"))
            objTr.Distribute_Amount = clsCommon.myCdbl(drData.Item("Distribute Amount"))
            objTr.Total_Amount = clsCommon.myCdbl(drData.Item("Total Amount"))
            'sanjay
            objTr.Hierarchy = clsCommon.myCstr(drData.Item("Hierarchy"))
            objTr.CostCenter = clsCommon.myCstr(drData.Item("CostCenter"))
            'sanjay
            objList.Add(objTr)
        Next
        Return objList
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsAcquisitionHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsAcquisitionHead
        Dim obj As clsAcquisitionHead = Nothing
        Dim qry As String = "SELECT TSPL_ACQUISITION_HEAD.*,TSPL_VENDOR_MASTER.Vendor_Name  FROM TSPL_ACQUISITION_HEAD  "
        qry += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_ACQUISITION_HEAD.Vendor_Code"
        qry += " where 2=2 and Acquisition_Type<>'Merge' "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_ACQUISITION_HEAD.Acquisition_Code = (select MIN(Acquisition_Code) from TSPL_ACQUISITION_HEAD where 1=1 and Acquisition_Type<>'Merge' " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_ACQUISITION_HEAD.Acquisition_Code = (select Max(Acquisition_Code) from TSPL_ACQUISITION_HEAD where 1=1 and Acquisition_Type<>'Merge' " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_ACQUISITION_HEAD.Acquisition_Code = (select Min(Acquisition_Code) from TSPL_ACQUISITION_HEAD where Acquisition_Code>'" + strPONo + "' and Acquisition_Type<>'Merge' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_ACQUISITION_HEAD.Acquisition_Code = (select Max(Acquisition_Code) from TSPL_ACQUISITION_HEAD where Acquisition_Code<'" + strPONo + "' and Acquisition_Type<>'Merge' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_ACQUISITION_HEAD.Acquisition_Code = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsAcquisitionHead()
            obj.RemittanceObject = clsRemittance.GetData(strPONo, trans)
            obj.Acquisition_Code = clsCommon.myCstr(dt.Rows(0)("Acquisition_Code"))
            obj.statusnewold = clsCommon.myCstr(dt.Rows(0)("Status_New_Old"))
            obj.Acquisition_Date = clsCommon.myCDate(dt.Rows(0)("Acquisition_Date"))
            obj.PO_No = clsCommon.myCstr(dt.Rows(0)("PO_No"))
            obj.SRN_No = clsCommon.myCstr(dt.Rows(0)("SRN_No"))
            obj.PI_No = clsCommon.myCstr(dt.Rows(0)("PI_No"))
            obj.Loc_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Vendor_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Vendor_Invoice_No"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
            obj.IS_Assemble = IIf(clsCommon.myCdbl(dt.Rows(0)("IS_Assemble")) = 1, True, False)
            obj.Is_Visi_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Visi_Type")) = 1, True, False)
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
            obj.Templete_Code = clsCommon.myCstr(dt.Rows(0)("Templete_Code"))
            obj.Acquisition_Type = clsCommon.myCstr(dt.Rows(0)("Acquisition_Type"))
            ''
            obj.TDS_Base_Actual_Amount = clsCommon.myCdbl(dt.Rows(0)("TDS_Base_Actual_Amount"))
            obj.TDS_Base_Calculated_Amount = clsCommon.myCdbl(dt.Rows(0)("TDS_Base_Calculated_Amount"))
            obj.TDS_Percentage = clsCommon.myCdbl(dt.Rows(0)("TDS_Percentage"))
            obj.TDS_Actual_Amount = clsCommon.myCdbl(dt.Rows(0)("TDS_Actual_Amount"))
            obj.TDS_Calculated_Amount = clsCommon.myCdbl(dt.Rows(0)("TDS_Calculated_Amount"))
            obj.Nature_of_deduction = clsCommon.myCstr(dt.Rows(0)("Nature_of_deduction"))
            obj.Branch_Code = clsCommon.myCstr(dt.Rows(0)("Branch_Code"))
            obj.Section_Code = clsCommon.myCstr(dt.Rows(0)("Section_Code"))
            obj.Balance_Amt = clsCommon.myCdbl(dt.Rows(0)("Balance_Amt"))
            If dt.Rows(0)("Post_Date") IsNot DBNull.Value Then
                obj.Post_Date = clsCommon.myCDate(dt.Rows(0)("Post_Date"))
            End If
            obj.Assemble_Code = clsCommon.myCstr(dt.Rows(0)("Assemble_Code"))
            obj.Capex_Code = clsCommon.myCstr(dt.Rows(0)("Capex_Code"))
            obj.CapexSub_Code = clsCommon.myCstr(dt.Rows(0)("CapexSub_Code"))
            obj.Add_Charge_Code1 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code1"))
            obj.Add_Charge_Name1 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name1"))
            obj.Add_Charge_Amt1 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt1"))

            obj.Add_Charge_Code2 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code2"))
            obj.Add_Charge_Name2 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name2"))
            obj.Add_Charge_Amt2 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt2"))

            obj.Add_Charge_Code3 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code3"))
            obj.Add_Charge_Name3 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name3"))
            obj.Add_Charge_Amt3 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt3"))

            obj.Add_Charge_Code4 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code4"))
            obj.Add_Charge_Name4 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name4"))
            obj.Add_Charge_Amt4 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt4"))

            obj.Add_Charge_Code5 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code5"))
            obj.Add_Charge_Name5 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name5"))
            obj.Add_Charge_Amt5 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt5"))

            obj.Add_Charge_Code6 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code6"))
            obj.Add_Charge_Name6 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name6"))
            obj.Add_Charge_Amt6 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt6"))

            obj.Add_Charge_Code7 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code7"))
            obj.Add_Charge_Name7 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name7"))
            obj.Add_Charge_Amt7 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt7"))

            obj.Add_Charge_Code8 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code8"))
            obj.Add_Charge_Name8 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name8"))
            obj.Add_Charge_Amt8 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt8"))

            obj.Add_Charge_Code9 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code9"))
            obj.Add_Charge_Name9 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name9"))
            obj.Add_Charge_Amt9 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt9"))

            obj.Add_Charge_Code10 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code10"))
            obj.Add_Charge_Name10 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name10"))
            obj.Add_Charge_Amt10 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt10"))

            obj.Total_Add_Charge = clsCommon.myCdbl(dt.Rows(0)("Total_Add_Charge"))
            obj.Tax_Recoverable = clsCommon.myCdbl(dt.Rows(0)("Tax_Recoverable"))
            obj.Tax_Non_Recoverable = clsCommon.myCdbl(dt.Rows(0)("Tax_Non_Recoverable"))

            obj.Opening_Assemble = clsCommon.myCBool(dt.Rows(0)("Opening_Assemble"))
            obj.Opening_Assemble_Amt = clsCommon.myCdbl(dt.Rows(0)("Opening_Assemble_Amt"))
            obj.Opening_Direct = clsCommon.myCBool(dt.Rows(0)("Opening_Direct"))

            qry = "SELECT  TSPL_ACQUISITION_DETAIL.*,TSPL_ASSET_CATEGORY.Description as Category_Name,TSPL_ASSET_GROUP.Description as Group_Code_Name,TSPL_Dep_AccountSet.AcSet_Desc as AcSet_Code_Name,TSPL_FA_COST_CENTER_MASTER.CostCenter_Name as CostCenter_Name,TSPL_DEPRECIATION_METHOD.Description as Dep_Method_Name,TSPL_DEPRECIATION_PERIODS.period_Desc as Dep_Period_Name,TSPL_FA_TEMPLATE_MASTER.Template_Name as Templete_Name,TSPL_ITEM_MASTER.Item_Desc as Item_Name,DepMethodTax.Description as  Dep_Method_Tax_Name from TSPL_ACQUISITION_DETAIL " + Environment.NewLine
            qry += " left outer join TSPL_ASSET_CATEGORY on TSPL_ASSET_CATEGORY.Category_Code=TSPL_ACQUISITION_DETAIL.Category_code" + Environment.NewLine
            qry += " left outer join TSPL_ASSET_GROUP on TSPL_ASSET_GROUP.Group_Code=TSPL_ACQUISITION_DETAIL.Group_Code" + Environment.NewLine
            qry += " left outer join TSPL_Dep_AccountSet on TSPL_Dep_AccountSet.AcSet_Code=TSPL_ACQUISITION_DETAIL.AcSet_Code" + Environment.NewLine
            qry += " left outer join TSPL_FA_COST_CENTER_MASTER on TSPL_FA_COST_CENTER_MASTER.CostCenter_Code=TSPL_ACQUISITION_DETAIL.CostCenter_Code" + Environment.NewLine
            qry += " left outer join TSPL_DEPRECIATION_METHOD on TSPL_DEPRECIATION_METHOD.Code=TSPL_ACQUISITION_DETAIL.Dep_Method_Code" + Environment.NewLine
            qry += " left outer join TSPL_DEPRECIATION_METHOD as DepMethodTax on DepMethodTax.Code=TSPL_ACQUISITION_DETAIL.Dep_Method_Tax_Code" + Environment.NewLine
            qry += " left outer join TSPL_DEPRECIATION_PERIODS on TSPL_DEPRECIATION_PERIODS.period_Code=TSPL_ACQUISITION_DETAIL.Dep_Period_Code" + Environment.NewLine
            qry += " left outer join TSPL_FA_TEMPLATE_MASTER on TSPL_FA_TEMPLATE_MASTER.Template_Code=TSPL_ACQUISITION_DETAIL.Templete_Code" + Environment.NewLine
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ACQUISITION_DETAIL.Item_Code" + Environment.NewLine
            qry += " where TSPL_ACQUISITION_DETAIL.Acquisition_Code='" + obj.Acquisition_Code + "' ORDER BY TSPL_ACQUISITION_DETAIL.SNo" + Environment.NewLine
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsAcquisitionDetail)
                Dim objTr As clsAcquisitionDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsAcquisitionDetail
                    objTr.Acquisition_Code = clsCommon.myCstr(dr("Acquisition_Code"))
                    objTr.SNo = clsCommon.myCdbl(clsCommon.myCdbl(dr("SNo")))
                    objTr.Asset_Code = clsCommon.myCstr(dr("Asset_Code"))
                    objTr.Asset_Name = clsCommon.myCstr(dr("Asset_Name"))
                    objTr.Templete_Code = clsCommon.myCstr(dr("Templete_Code"))
                    objTr.Templete_Name = clsCommon.myCstr(dr("Templete_Name"))
                    objTr.Category_code = clsCommon.myCstr(dr("Category_code"))
                    objTr.Category_Name = clsCommon.myCstr(dr("Category_Name"))
                    objTr.Group_Code = clsCommon.myCstr(dr("Group_Code"))
                    objTr.Group_Code_Name = clsCommon.myCstr(dr("Group_Code_Name"))
                    objTr.AcSet_Code = clsCommon.myCstr(dr("AcSet_Code"))
                    objTr.AcSet_Code_Name = clsCommon.myCstr(dr("AcSet_Code_Name"))
                    objTr.Hirerachy_Code = clsCommon.myCstr(dr("Hirerachy_Code"))
                    objTr.CostCenter_Code = clsCommon.myCstr(dr("CostCenter_Code"))
                    objTr.CostCenter_Name = clsCommon.myCstr(dr("CostCenter_Name"))
                    objTr.Acqusition_Date = clsCommon.myCDate(dr("Acqusition_Date"))

                    objTr.Dep_Method_Code = clsCommon.myCstr(dr("Dep_Method_Code"))
                    objTr.Dep_Method_Name = clsCommon.myCstr(dr("Dep_Method_Name"))

                    objTr.Dep_Method_Tax_Code = clsCommon.myCstr(dr("Dep_Method_Tax_Code"))
                    objTr.Dep_Method_Tax_Name = clsCommon.myCstr(dr("Dep_Method_Tax_Name"))

                    objTr.Dep_Period_Code = clsCommon.myCstr(dr("Dep_Period_Code"))
                    objTr.Dep_Period_Name = clsCommon.myCstr(dr("Dep_Period_Name"))
                    objTr.Put_To_Use = clsCommon.myCBool(dr("Put_To_Use"))
                    objTr.Start_Date = clsCommon.myCDate(dr("Start_Date"))
                    objTr.Dep_Rate = clsCommon.myCdbl(dr("Dep_Rate"))
                    objTr.Book_Estimated_Life = clsCommon.myCdbl(dr("Book_Estimated_Life"))
                    objTr.Book_Source_value = clsCommon.myCdbl(dr("Book_Source_value"))
                    objTr.Book_Source_Original_value = clsCommon.myCdbl(dr("Book_Source_Original_value"))
                    objTr.Book_Salvage_Value = clsCommon.myCdbl(dr("Book_Salvage_Value"))
                    objTr.Book_Salvage_Rate = clsCommon.myCdbl(dr("Book_Salvage_Rate"))

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

                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Name = clsCommon.myCstr(dr("Item_Name"))
                    objTr.Asset_Specification = clsCommon.myCstr(dr("Asset_Specification"))
                    objTr.Dep_Tax_Rate = clsCommon.myCdbl(dr("Dep_Tax_Rate"))
                    objTr.Is_Assembled = clsCommon.myCBool(dr("Is_Assembled"))
                    If clsCommon.myCstr(dr("Tax_Dep_Type")) = "F" Then
                        objTr.Tax_Dep_Type = "Formula"
                    Else
                        objTr.Tax_Dep_Type = "Manual"
                    End If
                    If clsCommon.myCstr(dr("Book_Dep_Type")) = "F" Then
                        objTr.Book_Dep_Type = "Formula"
                    Else
                        objTr.Book_Dep_Type = "Manual"
                    End If
                    objTr.SRN_No = clsCommon.myCstr(dr("SRN_No"))
                    objTr.SRN_Rate = clsCommon.myCdbl(dr("SRN_Rate"))
                    objTr.SRNQty = clsCommon.myCdbl(dr("SRNQty"))
                    objTr.PI_No = clsCommon.myCstr(dr("PI_No"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))

                    objTr.IsCapex = clsCommon.myCdbl(dr("IsCapex"))
                    objTr.CapexType = clsCommon.myCstr(dr("CapexType"))
                    objTr.Capex_Code = clsCommon.myCstr(dr("Capex_Code"))
                    objTr.CapexSub_Code = clsCommon.myCstr(dr("Capex_SubCode"))
                    ''------------------14/04/2017=======================
                    objTr.ItemAdd_Charge_Code1 = clsCommon.myCstr(dr("ItemAdd_Charge_Code1"))
                    objTr.ItemAdd_Charge_Code2 = clsCommon.myCstr(dr("ItemAdd_Charge_Code2"))
                    objTr.ItemAdd_Charge_Code3 = clsCommon.myCstr(dr("ItemAdd_Charge_Code3"))
                    objTr.ItemAdd_Charge_Code4 = clsCommon.myCstr(dr("ItemAdd_Charge_Code4"))
                    objTr.ItemAdd_Charge_Code5 = clsCommon.myCstr(dr("ItemAdd_Charge_Code5"))
                    objTr.ItemAdd_Charge_Code6 = clsCommon.myCstr(dr("ItemAdd_Charge_Code6"))
                    objTr.ItemAdd_Charge_Code7 = clsCommon.myCstr(dr("ItemAdd_Charge_Code7"))
                    objTr.ItemAdd_Charge_Code8 = clsCommon.myCstr(dr("ItemAdd_Charge_Code8"))
                    objTr.ItemAdd_Charge_Code9 = clsCommon.myCstr(dr("ItemAdd_Charge_Code9"))
                    objTr.ItemAdd_Charge_Code10 = clsCommon.myCstr(dr("ItemAdd_Charge_Code10"))
                    objTr.ItemAdd_Org_Charge_Amt1 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt1"))
                    objTr.ItemAdd_Org_Charge_Amt2 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt2"))
                    objTr.ItemAdd_Org_Charge_Amt3 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt3"))
                    objTr.ItemAdd_Org_Charge_Amt4 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt4"))
                    objTr.ItemAdd_Org_Charge_Amt5 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt5"))
                    objTr.ItemAdd_Org_Charge_Amt6 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt6"))
                    objTr.ItemAdd_Org_Charge_Amt7 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt7"))
                    objTr.ItemAdd_Org_Charge_Amt8 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt8"))
                    objTr.ItemAdd_Org_Charge_Amt9 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt9"))
                    objTr.ItemAdd_Org_Charge_Amt10 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt10"))
                    objTr.ItemAdd_Calc_Charge_Amt1 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt1"))
                    objTr.ItemAdd_Calc_Charge_Amt2 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt2"))
                    objTr.ItemAdd_Calc_Charge_Amt3 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt3"))
                    objTr.ItemAdd_Calc_Charge_Amt4 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt4"))
                    objTr.ItemAdd_Calc_Charge_Amt5 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt5"))
                    objTr.ItemAdd_Calc_Charge_Amt6 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt6"))
                    objTr.ItemAdd_Calc_Charge_Amt7 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt7"))
                    objTr.ItemAdd_Calc_Charge_Amt8 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt8"))
                    objTr.ItemAdd_Calc_Charge_Amt9 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt9"))
                    objTr.ItemAdd_Calc_Charge_Amt10 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt10"))
                    objTr.Total_ItemAdd_Charge = clsCommon.myCdbl(dr("Total_ItemAdd_Charge"))
                    objTr.Tax_Recoverable = clsCommon.myCdbl(dr("Tax_Recoverable"))
                    objTr.Tax_Non_Recoverable = clsCommon.myCdbl(dr("Tax_Non_Recoverable"))

                    objTr.Asset_Serial_No = clsCommon.myCstr(dr("Asset_Serial_No"))
                    objTr.Depreciated_Value = clsCommon.myCdbl(dr("Depreciated_Value"))
                    objTr.Asset_Expired_Life = clsCommon.myCdbl(dr("Asset_Expired_Life"))
                    ''=======================================================

                   

                    ''-----Added by Parteek 22/03/2017
                    'Dim UDLCapexAcquisionEntry As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.UDLCapexAcquisionEntry, clsFixedParameterCode.UDLCapexAcquisionEntry, Nothing)) = "1", True, False))
                    'If UDLCapexAcquisionEntry = True Then
                    '    Dim qry1 As String = " select Add_Charges_Code,Amount,TSPL_ASSET_WORK_head.Asset_Code from TSPL_ASSET_WORK_DETAIL"
                    '    qry1 += " left outer join TSPL_ASSET_WORK_HEAD on TSPL_ASSET_WORK_DETAIL.Document_Code=TSPL_ASSET_WORK_HEAD.Document_Code"
                    '    qry1 += " where TSPL_ASSET_WORK_head.Asset_Code='" & objTr.Asset_Code & "'"
                    '    Dim dt1 = New DataTable()
                    '    dt1 = clsDBFuncationality.GetDataTable(qry1, trans)
                    '    If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                    '        ' obj.Arr = New List(Of clsAcquisitionDetail)
                    '        '   Dim objTr As clsAcquisitionDetail
                    '        For Each dr1 As DataRow In dt1.Rows
                    '            objTr = New clsAcquisitionDetail
                    '            objTr.Asset_Code = clsCommon.myCstr(dr1("Asset_Code"))
                    '            objTr.Assetsfinder = clsCommon.myCstr(dr1("Add_charges_code"))
                    '            objTr.AssetsAmt = clsCommon.myCstr(dr1("Amount"))
                    '            'obj.Arr.Add(objTr)
                    '        Next
                    '    End If
                    'End If
                    '-----End 
                    obj.Arr.Add(objTr)
                Next
            End If
            obj.ArrAssemble = clsAssetAssembleDetail.GetDetail(obj.Acquisition_Code, trans)
        End If

        Return obj
    End Function

    Public Shared Function PostData(ByVal Form_Id As String, ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal oldnewstatus As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim dtPostDate As DateTime = clsCommon.GETSERVERDATE(trans)
            Dim obj As clsAcquisitionHead = clsAcquisitionHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Acquisition_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.Status = 1) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Post_Date, "dd/MM/yyyy"))
            End If
            If (isCheckForPosted AndAlso obj.On_Hold) Then
                Throw New Exception("Document No " + obj.Acquisition_Code + " Is On Hold.Can't Post it")
            End If
            obj.Assemble_Code = clsERPFuncationality.GetNextCode(trans, dtPostDate, clsDocType.FAAssembleAsset, "", obj.Loc_Code)
            For Each objAsset As clsAcquisitionDetail In obj.Arr
                If objAsset.Put_To_Use = False Then
                    Throw New Exception("Put To Date is mandatory on Posting the Acquisition Entry")
                End If
                If objAsset.Is_Assembled Then
                    If objAsset.Book_Source_value <= 0 Then
                        qry = "select COUNT(TSPL_ISSUEITEMTOASSEMBLEDASSET_DETAIL.asset_code) from TSPL_ISSUEITEMTOASSEMBLEDASSET_DETAIL left join TSPL_ISSUEITEMTOASSEMBLEDASSET_head on TSPL_ISSUEITEMTOASSEMBLEDASSET_head.Doc_No  = TSPL_ISSUEITEMTOASSEMBLEDASSET_DETAIL.Doc_No  where TSPL_ISSUEITEMTOASSEMBLEDASSET_DETAIL.Asset_Code='" & objAsset.Asset_Code & "' and Status='1' "
                        Dim totalRec As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                        If totalRec <= 0 Then
                            Throw New Exception("Asset '" & objAsset.Asset_Code & "' is not assembled. Please assemble it by issuing items against it from screen-Issue Items To Assemble Asset")
                        End If
                    End If
                End If
            Next

            If obj.Opening_Direct = True Then
            Else
                If (clsCommon.myLen(obj.Vendor_Code) > 0 AndAlso clsCommon.myLen(obj.SRN_No) <= 0) Then 'for new entry it allows to make AP invoice not for OLD
                    Dim objVendorInvHead As New clsVedorInvoiceHead()
                    objVendorInvHead.Invoice_Entry_Date = clsCommon.myCDate(obj.Acquisition_Date, "dd/MM/yyyy")
                    objVendorInvHead.Vendor_Code = obj.Vendor_Code
                    objVendorInvHead.Vendor_Name = obj.Vendor_Name
                    objVendorInvHead.Vendor_Invoice_No = obj.Vendor_Invoice_No
                    objVendorInvHead.Vendor_Invoice_Date = clsCommon.myCDate(obj.Acquisition_Date, "dd/MM/yyyy")
                    objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.Loc_Code, trans)
                    objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.Vendor_Code + "'", trans))
                    If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                        Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.Vendor_Name)
                    End If
                    objVendorInvHead.Invoice_Type = "AP"
                    objVendorInvHead.Document_Type = "I" ''For Purchase Invoice Type
                    objVendorInvHead.Total_Tax = obj.Total_Tax_Amt

                    objVendorInvHead.On_Hold = False


                    If obj.Acquisition_Type = "Direct" Then
                        objVendorInvHead.Description = "Vendor " + obj.Vendor_Code + "/" + obj.Vendor_Name + " .Against Acquisition " + obj.Acquisition_Code + ""
                    Else
                        objVendorInvHead.Description = "Vendor " + obj.Vendor_Code + "/" + obj.Vendor_Name + " .Against Acquisition " + obj.Acquisition_Code + ""
                    End If

                    objVendorInvHead.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                    objVendorInvHead.Tax_Group = obj.Tax_Group
                    If (clsCommon.myLen(obj.TAX1) > 0) Then
                        objVendorInvHead.TAX1 = obj.TAX1
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX1, trans) Then
                            objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
                            objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.Loc_Code, trans)
                        End If
                        objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
                        objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
                        objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
                    End If
                    If (clsCommon.myLen(obj.TAX2) > 0) Then
                        objVendorInvHead.TAX2 = obj.TAX2
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX2, trans) Then
                            objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
                            objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.Loc_Code, trans)
                        End If
                        objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
                        objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
                        objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
                    End If
                    If (clsCommon.myLen(obj.TAX3) > 0) Then
                        objVendorInvHead.TAX3 = obj.TAX3
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX3, trans) Then
                            objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
                            objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.Loc_Code, trans)
                        End If
                        objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
                        objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
                        objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
                    End If
                    If (clsCommon.myLen(obj.TAX4) > 0) Then
                        objVendorInvHead.TAX4 = obj.TAX4
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX4, trans) Then
                            objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
                            objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.Loc_Code, trans)
                        End If
                        objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
                        objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
                        objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
                    End If
                    If (clsCommon.myLen(obj.TAX5) > 0) Then
                        objVendorInvHead.TAX5 = obj.TAX5
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX5, trans) Then
                            objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
                            objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.Loc_Code, trans)

                        End If
                        objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
                        objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
                        objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
                    End If
                    If (clsCommon.myLen(obj.TAX6) > 0) Then
                        objVendorInvHead.TAX6 = obj.TAX6
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX6, trans) Then
                            objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
                            objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.Loc_Code, trans)
                        End If
                        objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
                        objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
                        objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
                    End If
                    If (clsCommon.myLen(obj.TAX7) > 0) Then
                        objVendorInvHead.TAX7 = obj.TAX7
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX7, trans) Then
                            objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
                            objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.Loc_Code, trans)

                        End If
                        objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
                        objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
                        objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
                    End If
                    If (clsCommon.myLen(obj.TAX8) > 0) Then
                        objVendorInvHead.TAX8 = obj.TAX8
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX8, trans) Then
                            objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
                            objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.Loc_Code, trans)
                        End If
                        objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
                        objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
                        objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
                    End If
                    If (clsCommon.myLen(obj.TAX9) > 0) Then
                        objVendorInvHead.TAX9 = obj.TAX9
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX9, trans) Then
                            objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
                            objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.Loc_Code, trans)
                        End If
                        objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
                        objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
                        objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
                    End If
                    If (clsCommon.myLen(obj.TAX10) > 0) Then
                        objVendorInvHead.TAX10 = obj.TAX10
                        If clsTaxMaster.ISTaxRecoverableAC(obj.TAX10, trans) Then
                            objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
                            objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.Loc_Code, trans)
                        End If
                        objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
                        objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
                        objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
                    End If

                    objVendorInvHead.Discount_Base = obj.Total_Amt
                    objVendorInvHead.Discount_Amount = 0
                    objVendorInvHead.Amount_Less_Discount = obj.Total_Amt
                    objVendorInvHead.Document_Total = obj.Total_Amt
                    objVendorInvHead.Balance_Amt = obj.Balance_Amt
                    objVendorInvHead.Against_Acquisition = obj.Acquisition_Code
                    Dim dt As DataTable

                    dt = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                        objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.Loc_Code, trans)

                        If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                            objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                            objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.Loc_Code, trans)
                        End If
                    End If
                    If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                        Throw New Exception("Please set the vendor payable Account")
                    End If
                    objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

                    objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
                    objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
                    objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

                    objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
                    objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
                    objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

                    objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
                    objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
                    objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

                    objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
                    objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
                    objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

                    objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
                    objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
                    objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

                    objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
                    objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
                    objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

                    objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
                    objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
                    objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

                    objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
                    objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
                    objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

                    objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
                    objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
                    objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

                    objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
                    objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
                    objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


                    objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                    Dim ii As Integer = 0
                    Dim isFirstTime As Boolean = True
                    objVendorInvHead.Total_Landed_Amt = 0
                    For Each objPIDetail As clsAcquisitionDetail In obj.Arr
                        ''Fill VendorInvoice details Data
                        qry = "select TSPL_Dep_AccountSet.Ac_Control from TSPL_ACQUISITION_DETAIL left outer join  TSPL_Dep_AccountSet on  TSPL_Dep_AccountSet.AcSet_Code=TSPL_ACQUISITION_DETAIL.AcSet_Code where TSPL_ACQUISITION_DETAIL.Asset_Code='" + objPIDetail.Asset_Code + "'"
                        dt = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("Please set Asset Ctrl Account set for Asset " + objPIDetail.Asset_Code)
                        End If

                        Dim strAssetCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Ac_Control"))
                        strAssetCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strAssetCtrlAC, obj.Loc_Code, trans)
                        Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strAssetCtrlAC + "'", trans))

                        Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                        ii = ii + 1
                        objVendorInvDetail.Detail_Line_No = ii
                        objVendorInvDetail.GL_Account_Code = strAssetCtrlAC
                        objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
                        objVendorInvDetail.Amount = objPIDetail.Book_Source_value
                        objVendorInvDetail.Discount_Per = 0
                        objVendorInvDetail.Discount = 0
                        objVendorInvDetail.Amount_less_Discount = objPIDetail.Book_Source_value
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
                        objVendorInvDetail.Total_Amount = objPIDetail.Item_Net_Amt - objPIDetail.Total_Tax_Amt

                        If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                            objVendorInvHead.Arr.Add(objVendorInvDetail)
                        End If

                        Dim ArrDBName As New List(Of String)
                        ArrDBName.Add(objCommonVar.CurrDatabase)
                    Next
                    If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                        Throw New Exception("No GL Account Found For AP Invoice")
                    End If

                    objVendorInvHead.SaveData(objVendorInvHead, True, trans, Nothing, False)
                    clsVedorInvoiceHead.PostData(Form_Id, objVendorInvHead.Document_No, "", trans, obj.Acquisition_Date, Nothing, False)
                End If
            End If
            If False Then
                'If clsCommon.myLen(obj.Add_Charge_Code1) > 0 Then ''Comment by balwinder on 13/05/2021 
                Dim objAssetWorkHead As New clsAssetWorkHead()
                objAssetWorkHead.Arr = New List(Of clsAssetWorkDetail)
                Dim ii As Integer = 0
                Dim isFirstTime As Boolean = True

                For Each objAssetDetail As clsAcquisitionDetail In obj.Arr
                    objAssetWorkHead.Arr = New List(Of clsAssetWorkDetail)
                    objAssetWorkHead.Document_Date = clsCommon.myCDate(obj.Acquisition_Date, "dd/MM/yyyy")
                    objAssetWorkHead.Vendor_Code = obj.Vendor_Code
                    objAssetWorkHead.Vendor_Name = obj.Vendor_Name
                    objAssetWorkHead.Location_Code = obj.Loc_Code
                    objAssetWorkHead.Total_Tax_Amt = obj.Total_Tax_Amt
                    objAssetWorkHead.Asset_Code = objAssetDetail.Asset_Code
                    objAssetWorkHead.Asset_Description = objAssetDetail.Asset_Name

                    If clsCommon.myLen(objAssetWorkHead.Vendor_Code) > 0 Then
                        objAssetWorkHead._Type = "Vendor"
                    Else
                        objAssetWorkHead._Type = "Other"
                    End If

                    qry = "select TSPL_Dep_AccountSet.Ac_Control from TSPL_ACQUISITION_DETAIL left outer join  TSPL_Dep_AccountSet on  TSPL_Dep_AccountSet.AcSet_Code=TSPL_ACQUISITION_DETAIL.AcSet_Code where TSPL_ACQUISITION_DETAIL.Asset_Code='" + objAssetDetail.Asset_Code + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please set Asset Ctrl Account set for Asset " + objAssetDetail.Asset_Code)
                    End If


                    Dim AssetAddationalChargeGLAcc As String = Nothing


                    Dim objAssetWorkDetail As New clsAssetWorkDetail()
                    ii = 0
                    If (clsCommon.myLen(objAssetDetail.ItemAdd_Charge_Code1) > 0) Then
                        AssetAddationalChargeGLAcc = clsDBFuncationality.getSingleValue("select Account_Code from TSPL_Additional_Charges where Code ='" & objAssetDetail.ItemAdd_Charge_Code1 & "'", trans)
                        objAssetWorkDetail.Add_Charges_Code = objAssetDetail.ItemAdd_Charge_Code1
                        objAssetWorkDetail.Amount = objAssetDetail.ItemAdd_Calc_Charge_Amt1
                        objAssetWorkDetail.Item_Net_Amt = objAssetDetail.ItemAdd_Calc_Charge_Amt1
                        objAssetWorkDetail.GL_Account_Code = AssetAddationalChargeGLAcc
                        objAssetWorkHead.Net_Amt = objAssetWorkHead.Net_Amt + objAssetDetail.ItemAdd_Calc_Charge_Amt1
                        ii = ii + 1
                        objAssetWorkDetail.SNo = ii
                        objAssetWorkHead.Arr.Add(objAssetWorkDetail)
                        objAssetWorkDetail = New clsAssetWorkDetail
                    End If

                    If (clsCommon.myLen(objAssetDetail.ItemAdd_Charge_Code2) > 0) Then
                        AssetAddationalChargeGLAcc = clsDBFuncationality.getSingleValue("select Account_Code from TSPL_Additional_Charges where Code ='" & objAssetDetail.ItemAdd_Charge_Code2 & "'", trans)
                        objAssetWorkDetail.Add_Charges_Code = objAssetDetail.ItemAdd_Charge_Code2
                        objAssetWorkDetail.Amount = objAssetDetail.ItemAdd_Calc_Charge_Amt2
                        objAssetWorkDetail.Item_Net_Amt = objAssetDetail.ItemAdd_Calc_Charge_Amt2
                        objAssetWorkDetail.GL_Account_Code = AssetAddationalChargeGLAcc
                        objAssetWorkHead.Net_Amt = objAssetWorkHead.Net_Amt + objAssetDetail.ItemAdd_Calc_Charge_Amt2
                        ii = ii + 1
                        objAssetWorkDetail.SNo = ii
                        objAssetWorkHead.Arr.Add(objAssetWorkDetail)
                        objAssetWorkDetail = New clsAssetWorkDetail
                    End If

                    If (clsCommon.myLen(objAssetDetail.ItemAdd_Charge_Code3) > 0) Then
                        AssetAddationalChargeGLAcc = clsDBFuncationality.getSingleValue("select Account_Code from TSPL_Additional_Charges where Code ='" & objAssetDetail.ItemAdd_Charge_Code3 & "'", trans)
                        objAssetWorkDetail.Add_Charges_Code = objAssetDetail.ItemAdd_Charge_Code3
                        objAssetWorkDetail.Amount = objAssetDetail.ItemAdd_Calc_Charge_Amt3
                        objAssetWorkDetail.Item_Net_Amt = objAssetDetail.ItemAdd_Calc_Charge_Amt3
                        objAssetWorkDetail.GL_Account_Code = AssetAddationalChargeGLAcc
                        objAssetWorkHead.Net_Amt = objAssetWorkHead.Net_Amt + objAssetDetail.ItemAdd_Calc_Charge_Amt3
                        ii = ii + 1
                        objAssetWorkDetail.SNo = ii
                        objAssetWorkHead.Arr.Add(objAssetWorkDetail)
                        objAssetWorkDetail = New clsAssetWorkDetail
                    End If
                    If (clsCommon.myLen(objAssetDetail.ItemAdd_Charge_Code4) > 0) Then
                        AssetAddationalChargeGLAcc = clsDBFuncationality.getSingleValue("select Account_Code from TSPL_Additional_Charges where Code ='" & objAssetDetail.ItemAdd_Charge_Code4 & "'", trans)
                        objAssetWorkDetail.Add_Charges_Code = objAssetDetail.ItemAdd_Charge_Code4
                        objAssetWorkDetail.Amount = objAssetDetail.ItemAdd_Calc_Charge_Amt4
                        objAssetWorkDetail.Item_Net_Amt = objAssetDetail.ItemAdd_Calc_Charge_Amt4
                        objAssetWorkDetail.GL_Account_Code = AssetAddationalChargeGLAcc
                        objAssetWorkHead.Net_Amt = objAssetWorkHead.Net_Amt + objAssetDetail.ItemAdd_Calc_Charge_Amt4
                        ii = ii + 1
                        objAssetWorkDetail.SNo = ii
                        objAssetWorkHead.Arr.Add(objAssetWorkDetail)
                        objAssetWorkDetail = New clsAssetWorkDetail
                    End If
                    If (clsCommon.myLen(objAssetDetail.ItemAdd_Charge_Code5) > 0) Then
                        AssetAddationalChargeGLAcc = clsDBFuncationality.getSingleValue("select Account_Code from TSPL_Additional_Charges where Code ='" & objAssetDetail.ItemAdd_Charge_Code5 & "'", trans)
                        objAssetWorkDetail.Add_Charges_Code = objAssetDetail.ItemAdd_Charge_Code5
                        objAssetWorkDetail.Amount = objAssetDetail.ItemAdd_Calc_Charge_Amt5
                        objAssetWorkDetail.Item_Net_Amt = objAssetDetail.ItemAdd_Calc_Charge_Amt5
                        objAssetWorkDetail.GL_Account_Code = AssetAddationalChargeGLAcc
                        objAssetWorkHead.Net_Amt = objAssetWorkHead.Net_Amt + objAssetDetail.ItemAdd_Calc_Charge_Amt5
                        ii = ii + 1
                        objAssetWorkDetail.SNo = ii
                        objAssetWorkHead.Arr.Add(objAssetWorkDetail)
                        objAssetWorkDetail = New clsAssetWorkDetail
                    End If

                    If (clsCommon.myLen(objAssetDetail.ItemAdd_Charge_Code6) > 0) Then
                        AssetAddationalChargeGLAcc = clsDBFuncationality.getSingleValue("select Account_Code from TSPL_Additional_Charges where Code ='" & objAssetDetail.ItemAdd_Charge_Code6 & "'", trans)
                        objAssetWorkDetail.Add_Charges_Code = objAssetDetail.ItemAdd_Charge_Code6
                        objAssetWorkDetail.Amount = objAssetDetail.ItemAdd_Calc_Charge_Amt6
                        objAssetWorkDetail.Item_Net_Amt = objAssetDetail.ItemAdd_Calc_Charge_Amt6
                        objAssetWorkHead.Net_Amt = objAssetWorkHead.Net_Amt + objAssetDetail.ItemAdd_Calc_Charge_Amt6
                        objAssetWorkDetail.GL_Account_Code = AssetAddationalChargeGLAcc
                        ii = ii + 1
                        objAssetWorkDetail.SNo = ii
                        objAssetWorkHead.Arr.Add(objAssetWorkDetail)
                        objAssetWorkDetail = New clsAssetWorkDetail
                    End If
                    If (clsCommon.myLen(objAssetDetail.ItemAdd_Charge_Code7) > 0) Then
                        AssetAddationalChargeGLAcc = clsDBFuncationality.getSingleValue("select Account_Code from TSPL_Additional_Charges where Code ='" & objAssetDetail.ItemAdd_Charge_Code7 & "'", trans)
                        objAssetWorkDetail.Add_Charges_Code = objAssetDetail.ItemAdd_Charge_Code7
                        objAssetWorkDetail.Amount = objAssetDetail.ItemAdd_Calc_Charge_Amt7
                        objAssetWorkDetail.GL_Account_Code = AssetAddationalChargeGLAcc
                        objAssetWorkDetail.Item_Net_Amt = objAssetDetail.ItemAdd_Calc_Charge_Amt7
                        objAssetWorkHead.Net_Amt = objAssetWorkHead.Net_Amt + objAssetDetail.ItemAdd_Calc_Charge_Amt7
                        objAssetWorkHead.Arr.Add(objAssetWorkDetail)
                        ii = ii + 1
                        objAssetWorkDetail.SNo = ii
                        objAssetWorkDetail = New clsAssetWorkDetail
                    End If
                    If (clsCommon.myLen(objAssetDetail.ItemAdd_Charge_Code8) > 0) Then
                        AssetAddationalChargeGLAcc = clsDBFuncationality.getSingleValue("select Account_Code from TSPL_Additional_Charges where Code ='" & objAssetDetail.ItemAdd_Charge_Code8 & "'", trans)
                        objAssetWorkDetail.Add_Charges_Code = objAssetDetail.ItemAdd_Charge_Code8
                        objAssetWorkDetail.Amount = objAssetDetail.ItemAdd_Calc_Charge_Amt8
                        objAssetWorkDetail.Item_Net_Amt = objAssetDetail.ItemAdd_Calc_Charge_Amt8
                        objAssetWorkHead.Net_Amt = objAssetWorkHead.Net_Amt + objAssetDetail.ItemAdd_Calc_Charge_Amt8
                        objAssetWorkDetail.GL_Account_Code = AssetAddationalChargeGLAcc
                        ii = ii + 1
                        objAssetWorkDetail.SNo = ii
                        objAssetWorkHead.Arr.Add(objAssetWorkDetail)
                        objAssetWorkDetail = New clsAssetWorkDetail
                    End If
                    If (clsCommon.myLen(objAssetDetail.ItemAdd_Charge_Code9) > 0) Then
                        AssetAddationalChargeGLAcc = clsDBFuncationality.getSingleValue("select Account_Code from TSPL_Additional_Charges where Code ='" & objAssetDetail.ItemAdd_Charge_Code9 & "'", trans)
                        objAssetWorkDetail.Add_Charges_Code = objAssetDetail.ItemAdd_Charge_Code9
                        objAssetWorkDetail.Amount = objAssetDetail.ItemAdd_Calc_Charge_Amt9
                        objAssetWorkHead.Net_Amt = objAssetWorkHead.Net_Amt + objAssetDetail.ItemAdd_Calc_Charge_Amt9
                        objAssetWorkDetail.Item_Net_Amt = objAssetDetail.ItemAdd_Calc_Charge_Amt9
                        objAssetWorkDetail.GL_Account_Code = AssetAddationalChargeGLAcc
                        ii = ii + 1
                        objAssetWorkDetail.SNo = ii
                        objAssetWorkHead.Arr.Add(objAssetWorkDetail)
                        objAssetWorkDetail = New clsAssetWorkDetail
                    End If
                    If (clsCommon.myLen(objAssetDetail.ItemAdd_Charge_Code10) > 0) Then
                        AssetAddationalChargeGLAcc = clsDBFuncationality.getSingleValue("select Account_Code from TSPL_Additional_Charges where Code ='" & objAssetDetail.ItemAdd_Charge_Code10 & "'", trans)
                        objAssetWorkDetail.Add_Charges_Code = objAssetDetail.ItemAdd_Charge_Code10
                        objAssetWorkDetail.Amount = objAssetDetail.ItemAdd_Calc_Charge_Amt10
                        objAssetWorkDetail.Item_Net_Amt = objAssetDetail.ItemAdd_Calc_Charge_Amt10
                        objAssetWorkDetail.GL_Account_Code = AssetAddationalChargeGLAcc
                        objAssetWorkHead.Net_Amt = objAssetWorkHead.Net_Amt + objAssetDetail.ItemAdd_Calc_Charge_Amt10
                        ii = ii + 1
                        objAssetWorkDetail.SNo = ii
                        objAssetWorkHead.Arr.Add(objAssetWorkDetail)
                        objAssetWorkDetail = New clsAssetWorkDetail
                    End If


                    If (clsCommon.myLen(objAssetWorkDetail.GL_Account_Code) > 0) Then
                        objAssetWorkHead.Arr.Add(objAssetWorkDetail)
                    End If
                    objAssetWorkHead.SaveData(objAssetWorkHead, True, trans)
                    clsAssetWorkHead.PostData(Form_Id, objAssetWorkHead.Document_Code, trans)
                    objAssetWorkHead.Arr = New List(Of clsAssetWorkDetail)
                    objAssetWorkHead = New clsAssetWorkHead()
                Next
            End If


            Dim settHitInvenotry As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FixedAssetAcquisitionEntryHitInventoryMovement, clsFixedParameterCode.FixedAssetAcquisitionEntryHitInventoryMovement, trans)) = 1)
            If settHitInvenotry Then
                If clsCommon.myLen(obj.SRN_No) > 0 Then
                    qry = "insert into TSPL_INVENTORY_MOVEMENT (Trans_Type, InOut,Location_Code,Item_Code,Item_Desc,Qty,UOM, Source_Doc_No, Source_Doc_Date, Entry_Date,Basic_Cost,Rec_Cost,Add_Cost,Net_Cost, Created_By,Comp_Code,ItemType, Punching_Date,MRP,Batch_No,FIFO_Cost,LIFO_Cost,Avg_Cost,Posting_Date,PI_Cost,Stock_UOM,Stock_Qty,MFG_Date,Expiry_Date,Item_Status,Assmbly_Status,IS_CONSUMPTION,Cust_Code,Cust_Name,Vendor_Code,Vendor_Name,Other_Location_Code,Other_Location_Desc,Fat_Per,SNF_Per,Fat_KG,SNF_KG,Fat_Rate,SNF_Rate,Fat_Amt,SNF_Amt,Inventory_DrAcc,Inventory_CrAcc,Is_Scheme_Item) " + Environment.NewLine +
                    "Select 'ASS-ACQ-ENT' as Trans_Type,'O' as InOut,Location_Code,Item_Code,Item_Desc,Qty,UOM,'" + obj.Acquisition_Code + "' as Source_Doc_No,'" + clsCommon.GetPrintDate(obj.Acquisition_Date, "dd/MM/yyyy") + "' as Source_Doc_Date,'" + clsCommon.GetPrintDate(obj.Acquisition_Date, "dd/MM/yyyy") + "' as Entry_Date,Basic_Cost,Rec_Cost,Add_Cost,Net_Cost,'" + objCommonVar.CurrentUserCode + "' as Created_By,Comp_Code,ItemType,'" + clsCommon.GetPrintDate(obj.Acquisition_Date, "dd/MMM/yyyy hh:mm:ss tt") + "' as Punching_Date,MRP,Batch_No,TSPL_PI_DETAIL.Landed_Cost_Amount as FIFO_Cost,TSPL_PI_DETAIL.Landed_Cost_Amount as LIFO_Cost,TSPL_PI_DETAIL.Landed_Cost_Amount as Avg_Cost,GetDate() as Posting_Date,PI_Cost,Stock_UOM,Stock_Qty,MFG_Date,Expiry_Date,Item_Status,Assmbly_Status,IS_CONSUMPTION,Cust_Code,Cust_Name,Vendor_Code,Vendor_Name,Other_Location_Code,Other_Location_Desc,Fat_Per,SNF_Per,Fat_KG,SNF_KG,Fat_Rate,SNF_Rate,Fat_Amt,SNF_Amt,Inventory_DrAcc,Inventory_CrAcc,Is_Scheme_Item from TSPL_INVENTORY_MOVEMENT  " + Environment.NewLine +
                    "left outer join ( select PIItem,sum(Landed_Cost_Amount*RI) as Landed_Cost_Amount from (
 select Item_Code as PIItem,Landed_Cost_Amount,1 as RI  from TSPL_PI_DETAIL where PI_No='" + obj.PI_No + "' 
 union all
 select Item_Code as PIItem,Landed_Cost_Amount,-1 as RI from tspl_PR_Detail where PI_Id='" + obj.PI_No + "'
 )x group by PIItem )TSPL_PI_DETAIL on  TSPL_PI_DETAIL.PIItem= TSPL_INVENTORY_MOVEMENT.Item_Code" + Environment.NewLine +
                    " where Source_Doc_No='" + obj.SRN_No + "' and Item_Code in (select  distinct Item_Code from TSPL_ACQUISITION_DETAIL where Acquisition_Code = '" + obj.Acquisition_Code + "') and trans_type='SRN'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            End If

            CreateJournalEntry(strDocNo, trans, "")
            'Dim strCostCenterCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Top 1 CostCenter_Code from TSPL_ACQUISITION_DETAIL where Acquisition_Code = '" + strDocNo + "'", trans))
            'Dim strHirerachyCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Top 1 Hirerachy_Code from TSPL_ACQUISITION_DETAIL where Acquisition_Code =  '" + strDocNo + "'", trans))
            'clsERPFuncationality.UpdateCostCenterAndHirerachyCodeOnJE(strDocNo, "AQ-AS", strCostCenterCode, strHirerachyCode, trans)

            qry = "Update TSPL_ACQUISITION_HEAD set Status=1, Post_Date='" + clsCommon.GetPrintDate(dtPostDate, "dd/MMM/yyyy hh:mm tt") + "',ASSEMBLE_CODE='" & obj.Assemble_Code & "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Acquisition_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_ACQUISITION_HEAD", "Acquisition_Code", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Sub CreateJournalEntry(ByVal strDocNo As String, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoForRecreateOnly As String = Nothing)
        Dim obj As clsAcquisitionHead = clsAcquisitionHead.GetData(strDocNo, NavigatorType.Current, trans)
        Dim strAcquisionDesc As String = ""
        Dim qry As String = ""
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            If clsCommon.myLen(obj.Description) > 0 Then
                strAcquisionDesc = "," & obj.Description
            End If
        Else
            strAcquisionDesc = ""
        End If
        If clsCommon.myLen(obj.Vendor_Code) > 0 Then
            If obj.IS_Assemble = True Then
                Dim ArryLst As ArrayList = New ArrayList()
                Dim strQ As String = "select xx.WIP_AC,xx.Payable_Account as Vendor_Control_Account, SUM(xx.Book_Source_value )  as Book_Source_value " &
                    " from ( select TSPL_ACQUISITION_DETAIL.Book_Source_value  ,TSPL_Dep_AccountSet.wip_ac as WIP_AC,TSPL_VENDOR_ACCOUNT_SET.Payable_Account " &
                    " from TSPL_ACQUISITION_DETAIL left join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_DETAIL.Acquisition_Code=TSPL_ACQUISITION_HEAD.Acquisition_Code  " &
                    " left outer join TSPL_Dep_AccountSet on TSPL_Dep_AccountSet.AcSet_Code=TSPL_ACQUISITION_DETAIL.AcSet_Code  " &
                    " left join TSPL_VENDOR_MASTER Vendor on TSPL_ACQUISITION_HEAD.Vendor_Code=Vendor.Vendor_Code " &
                    " left join TSPL_VENDOR_ACCOUNT_SET on Vendor.Vendor_Account=TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code " &
                    " where TSPL_ACQUISITION_DETAIL.Acquisition_Code ='" & strDocNo & "' and TSPL_ACQUISITION_DETAIL.Is_Assembled='1')  as xx  " &
                    " group by xx.WIP_AC ,xx.Payable_Account"
                Dim dtData As DataTable = clsDBFuncationality.GetDataTable(strQ, trans)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For i As Integer = 0 To dtData.Rows.Count - 1
                        Dim strWIP_AC As String = clsERPFuncationality.ChangeGLAccountLocationSegment(dtData.Rows(i)("WIP_AC"), obj.Loc_Code, trans)
                        If clsCommon.myLen(strWIP_AC) <= 0 Then
                            Throw New Exception("GL Account " & dtData.Rows(i)("WIP_AC") & " not Found For Location " & obj.Loc_Code & "")
                        End If
                        Dim Vendor_Control_Account As String = clsERPFuncationality.ChangeGLAccountLocationSegment(dtData.Rows(i)("Vendor_Control_Account"), obj.Loc_Code, trans)
                        If clsCommon.myLen(Vendor_Control_Account) <= 0 Then
                            Throw New Exception("GL Account " & dtData.Rows(i)("Vendor_Control_Account") & " not Found For Location " & obj.Loc_Code & "")
                        End If

                        ArryLst.Add(New String() {strWIP_AC, clsCommon.myCdbl(dtData.Rows(i)("Book_Source_value"))})
                        ArryLst.Add(New String() {Vendor_Control_Account, clsCommon.myCdbl(dtData.Rows(i)("Book_Source_value")) * -1})
                    Next
                    If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, strVoucherNoForRecreateOnly, trans, obj.Acquisition_Date, "Acquisition Entry Assembled Asset , Against Doc No:  " & strDocNo, "AQ-AS", "Acquisition Entry Assembled", strDocNo, "Acquisition Entry Assembled", "V", strDocNo, "Acquisition Entry Assembled", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
                    Else
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Acquisition_Date, "Acquisition Entry Assembled Asset , Against Doc No:  " & strDocNo, "AQ-AS", "Acquisition Entry Assembled", strDocNo, "Acquisition Entry Assembled", "V", strDocNo, "Acquisition Entry Assembled", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
                    End If

                End If
            ElseIf clsCommon.CompairString(obj.Acquisition_Type, "Direct") <> CompairStringResult.Equal And clsCommon.CompairString(obj.Acquisition_Type, "Asset") <> CompairStringResult.Equal Then
                Dim strq As String = "select xx.Ac_Control,xx.Payable_Account as Vendor_Control_Account, SUM(xx.Book_Source_value)  as Book_Source_value,sum(BookValue) as BookValue " &
                   " from ( select (AH.Book_Source_value+(CASE WHEN TX1.Tax_Recoverable='N' THEN  AH.TAX1_Amt ELSE 0 END)+(CASE WHEN TX2.Tax_Recoverable='N' THEN  AH.TAX2_Amt ELSE 0 END)+(CASE WHEN TX3.Tax_Recoverable='N' THEN  AH.TAX3_Amt ELSE 0 END)+(CASE WHEN TX4.Tax_Recoverable='N' THEN  AH.TAX4_Amt ELSE 0 END)+(CASE WHEN TX5.Tax_Recoverable='N' THEN  AH.TAX5_Amt ELSE 0 END)+(CASE WHEN TX6.Tax_Recoverable='N' THEN  AH.TAX6_Amt ELSE 0 END)+(CASE WHEN TX7.Tax_Recoverable='N' THEN  AH.TAX7_Amt ELSE 0 END)+(CASE WHEN TX8.Tax_Recoverable='N' THEN  AH.TAX8_Amt ELSE 0 END)+(CASE WHEN TX9.Tax_Recoverable='N' THEN  AH.TAX9_Amt ELSE 0 END)+(CASE WHEN TX10.Tax_Recoverable='N' THEN  AH.TAX10_Amt ELSE 0 END)) AS Book_Source_value  ,TSPL_Dep_AccountSet.Ac_Control,TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,ah.Book_Source_value as [BookValue] " &
                   " from TSPL_ACQUISITION_DETAIL AH left join TSPL_ACQUISITION_HEAD on AH.Acquisition_Code=TSPL_ACQUISITION_HEAD.Acquisition_Code " &
                   " left join TSPL_TAX_MASTER   TX1 on AH.TAX1=TX1.Tax_Code " &
                   " left join TSPL_TAX_MASTER   TX2 on AH.TAX2=TX2.Tax_Code " &
                   " left join TSPL_TAX_MASTER   TX3 on AH.TAX3=TX3.Tax_Code " &
                   " left join TSPL_TAX_MASTER   TX4 on AH.TAX4=TX4.Tax_Code " &
                   " left join TSPL_TAX_MASTER   TX5 on AH.TAX5=TX5.Tax_Code " &
                   " left join TSPL_TAX_MASTER   TX6 on AH.TAX6=TX6.Tax_Code " &
                   " left join TSPL_TAX_MASTER   TX7 on AH.TAX7=TX7.Tax_Code " &
                   " left join TSPL_TAX_MASTER   TX8 on AH.TAX8=TX8.Tax_Code " &
                   " left join TSPL_TAX_MASTER   TX9 on AH.TAX9=TX9.Tax_Code " &
                   " left join TSPL_TAX_MASTER   TX10 on AH.TAX10=TX10.Tax_Code " &
                   " left outer join TSPL_Dep_AccountSet on TSPL_Dep_AccountSet.AcSet_Code=AH.AcSet_Code " &
                   " left join TSPL_VENDOR_MASTER Vendor on TSPL_ACQUISITION_HEAD.Vendor_Code=Vendor.Vendor_Code " &
                   " left join TSPL_VENDOR_ACCOUNT_SET on Vendor.Vendor_Account=TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code " &
                   " where AH.Acquisition_Code ='" & strDocNo & "' and AH.Is_Assembled='0')  as xx  " &
                   " group by xx.Ac_Control ,xx.Payable_Account"
                Dim ArryLst As ArrayList = New ArrayList()
                Dim dtData As DataTable = clsDBFuncationality.GetDataTable(strq, trans)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For i As Integer = 0 To dtData.Rows.Count - 1
                        Dim Vendor_Control_Account As String = clsERPFuncationality.ChangeGLAccountLocationSegment(dtData.Rows(i)("Vendor_Control_Account"), obj.Loc_Code, trans)
                        If clsCommon.myLen(Vendor_Control_Account) <= 0 Then
                            Throw New Exception("GL Account " & dtData.Rows(i)("Vendor_Control_Account") & " not Found For Location " & obj.Loc_Code & "")
                        End If
                        Dim strAc_Control As String = clsERPFuncationality.ChangeGLAccountLocationSegment(dtData.Rows(i)("Ac_Control"), obj.Loc_Code, trans)
                        If clsCommon.myLen(strAc_Control) <= 0 Then
                            Throw New Exception("GL Account " & dtData.Rows(i)("Ac_Control") & " not Found For Location " & obj.Loc_Code & "")
                        End If
                        ArryLst.Add(New String() {Vendor_Control_Account, clsCommon.myCdbl(dtData.Rows(i)("BookValue"))})
                        ArryLst.Add(New String() {strAc_Control, clsCommon.myCdbl(dtData.Rows(i)("BookValue")) * -1})
                    Next
                    If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, strVoucherNoForRecreateOnly, trans, obj.Acquisition_Date, "Acquisition Entry Asset , Against Doc No:  " & strDocNo & strAcquisionDesc, "AQ-AS", "Acquisition Entry", strDocNo, "Acquisition Entry", "V", strDocNo, "Acquisition Entry", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
                    Else
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Acquisition_Date, "Acquisition Entry Asset , Against Doc No:  " & strDocNo & strAcquisionDesc, "AQ-AS", "Acquisition Entry", strDocNo, "Acquisition Entry", "V", strDocNo, "Acquisition Entry", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
                    End If

                End If

            ElseIf clsCommon.CompairString(obj.Acquisition_Type, "Asset") = CompairStringResult.Equal Then
                'If document against SRN then use SRN Ship To Location
                Dim SRNLocation As String = ""
                If clsCommon.myLen(obj.SRN_No) > 0 Then
                    SRNLocation = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select case when  len(isnull( Ship_To_Location,''))>0  then Ship_To_Location else Bill_To_Location end as Location_Code from TSPL_SRN_HEAD where SRN_No='" & obj.SRN_No & "'", trans))
                End If
                Dim strq As String = " select max(xx.Ac_Control) as Ac_Control,(xx.Inv_Control_Account ) as Inv_Control_Account , SUM(xx.Book_Source_value)  as Book_Source_value,sum(BookValue) as BookValue " &
                       " from ( select (AH.Item_Net_Amt) AS Book_Source_value  ,TSPL_Dep_AccountSet.Ac_Control,TSPL_VENDOR_ACCOUNT_SET.Payable_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account ,AH.Item_Code ,ah.Book_Source_value as [BookValue]  " &
                       " from TSPL_ACQUISITION_DETAIL AH left join TSPL_ACQUISITION_HEAD on AH.Acquisition_Code=TSPL_ACQUISITION_HEAD.Acquisition_Code " &
                        " left join TSPL_TAX_MASTER   TX1 on AH.TAX1=TX1.Tax_Code " &
                        " left join TSPL_TAX_MASTER   TX2 on AH.TAX2=TX2.Tax_Code " &
                        " left join TSPL_TAX_MASTER   TX3 on AH.TAX3=TX3.Tax_Code " &
                        " left join TSPL_TAX_MASTER   TX4 on AH.TAX4=TX4.Tax_Code " &
                        " left join TSPL_TAX_MASTER   TX5 on AH.TAX5=TX5.Tax_Code " &
                        " left join TSPL_TAX_MASTER   TX6 on AH.TAX6=TX6.Tax_Code " &
                        " left join TSPL_TAX_MASTER   TX7 on AH.TAX7=TX7.Tax_Code " &
                        " left join TSPL_TAX_MASTER   TX8 on AH.TAX8=TX8.Tax_Code " &
                        " left join TSPL_TAX_MASTER   TX9 on AH.TAX9=TX9.Tax_Code " &
                        " left join TSPL_TAX_MASTER   TX10 on AH.TAX10=TX10.Tax_Code " &
                        " left outer join TSPL_Dep_AccountSet on TSPL_Dep_AccountSet.AcSet_Code=AH.AcSet_Code " &
                        " left join TSPL_VENDOR_MASTER Vendor on TSPL_ACQUISITION_HEAD.Vendor_Code=Vendor.Vendor_Code " &
                        " left join tspl_item_master on  tspl_item_master.Item_Code = AH .Item_Code " &
                        " left join TSPL_PURCHASE_ACCOUNTS on  TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code = tspl_item_master.Purchase_Class_Code " &
                        " left join TSPL_VENDOR_ACCOUNT_SET on Vendor.Vendor_Account=TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code " &
                        " where AH.Acquisition_Code ='" + strDocNo + "' and AH.Is_Assembled='0')  as xx  " &
                        " group by xx.Inv_Control_Account "
                Dim ArryLst As ArrayList = New ArrayList()
                Dim dtData As DataTable = clsDBFuncationality.GetDataTable(strq, trans)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For i As Integer = 0 To dtData.Rows.Count - 1
                        If clsCommon.myLen(obj.SRN_No) > 0 Then
                            Dim strAc_Control As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dtData.Rows(i)("Ac_Control")), SRNLocation, trans)
                            If clsCommon.myLen(strAc_Control) <= 0 Then
                                Throw New Exception("GL Account " & dtData.Rows(i)("Ac_Control") & " not Found For Location " & SRNLocation & "")
                            End If
                            ArryLst.Add(New String() {strAc_Control, clsCommon.myCdbl(dtData.Rows(i)("BookValue"))})
                            Dim strInvControlAcc As String = String.Empty
                            strInvControlAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dtData.Rows(i)("Inv_Control_Account")), SRNLocation, trans)
                            If strInvControlAcc = "" Then
                                Throw New Exception("Purchase A/c set - Inventory control A/c  " & dtData.Rows(i)("Inv_Control_Account") & " not Found For Location " & SRNLocation & "")
                            End If
                            ArryLst.Add(New String() {strInvControlAcc, clsCommon.myCdbl(dtData.Rows(i)("BookValue")) * -1})
                        Else
                            Dim strAc_Control As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dtData.Rows(i)("Ac_Control")), obj.Loc_Code, trans)
                            If clsCommon.myLen(strAc_Control) <= 0 Then
                                Throw New Exception("GL Account " & dtData.Rows(i)("Ac_Control") & " not Found For Location " & obj.Loc_Code & "")
                            End If
                            ArryLst.Add(New String() {strAc_Control, clsCommon.myCdbl(dtData.Rows(i)("BookValue"))})
                            Dim strInvControlAcc As String = String.Empty
                            strInvControlAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dtData.Rows(i)("Inv_Control_Account")), obj.Loc_Code, trans)
                            If strInvControlAcc = "" Then
                                Throw New Exception("Purchase A/c set - Inventory control A/c  " & dtData.Rows(i)("Inv_Control_Account") & " not Found For Location " & obj.Loc_Code & "")
                            End If
                            ArryLst.Add(New String() {strInvControlAcc, clsCommon.myCdbl(dtData.Rows(i)("BookValue")) * -1})
                        End If
                    Next
                    If clsCommon.myLen(obj.SRN_No) > 0 Then
                        If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                            clsJournalMaster.FunGrnlEntryWithTrans(SRNLocation, False, strVoucherNoForRecreateOnly, trans, obj.Acquisition_Date, "Acquisition Entry Asset , Against Doc No:  " & strDocNo & strAcquisionDesc, "AQ-AS", "Acquisition Entry", strDocNo, "Acquisition Entry", "V", strDocNo, "Acquisition Entry", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
                        Else
                            clsJournalMaster.FunGrnlEntryWithTrans(SRNLocation, False, trans, obj.Acquisition_Date, "Acquisition Entry Asset , Against Doc No:  " & strDocNo & strAcquisionDesc, "AQ-AS", "Acquisition Entry", strDocNo, "Acquisition Entry", "V", strDocNo, "Acquisition Entry", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
                        End If

                    Else
                        If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                            clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, strVoucherNoForRecreateOnly, trans, obj.Acquisition_Date, "Acquisition Entry Asset , Against Doc No:  " & strDocNo & strAcquisionDesc, "AQ-AS", "Acquisition Entry", strDocNo, "Acquisition Entry", "V", strDocNo, "Acquisition Entry", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
                        Else
                            clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Acquisition_Date, "Acquisition Entry Asset , Against Doc No:  " & strDocNo & strAcquisionDesc, "AQ-AS", "Acquisition Entry", strDocNo, "Acquisition Entry", "V", strDocNo, "Acquisition Entry", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
                        End If

                    End If

                End If
            End If
        Else
            '' Je created by Panch Raj on 16-oct-2017
            If obj.IS_Assemble = True Then
                Dim ArryLst As ArrayList = New ArrayList()
                Dim strQ As String = "select xx.Ac_Control, SUM(xx.Book_Source_value )  as Book_Source_value " &
                " from ( select TSPL_ACQUISITION_DETAIL.Book_Source_value,TSPL_Dep_AccountSet.Ac_Control as Ac_Control " &
                " from TSPL_ACQUISITION_DETAIL left join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_DETAIL.Acquisition_Code=TSPL_ACQUISITION_HEAD.Acquisition_Code " &
                " left outer join TSPL_Dep_AccountSet on TSPL_Dep_AccountSet.AcSet_Code=TSPL_ACQUISITION_DETAIL.AcSet_Code " &
                " left join TSPL_VENDOR_MASTER Vendor on TSPL_ACQUISITION_HEAD.Vendor_Code=Vendor.Vendor_Code " &
                " left join TSPL_VENDOR_ACCOUNT_SET on Vendor.Vendor_Account=TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code " &
                " where TSPL_ACQUISITION_DETAIL.Acquisition_Code ='" & strDocNo & "' and TSPL_ACQUISITION_DETAIL.Is_Assembled='1')  as xx group by xx.Ac_Control"
                Dim dtData As DataTable = clsDBFuncationality.GetDataTable(strQ, trans)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For i As Integer = 0 To dtData.Rows.Count - 1
                        Dim strAc_Control As String = clsERPFuncationality.ChangeGLAccountLocationSegment(dtData.Rows(i)("Ac_Control"), obj.Loc_Code, trans)
                        If clsCommon.myLen(strAc_Control) <= 0 Then
                            Throw New Exception("GL Account " & dtData.Rows(i)("Ac_Control") & " not Found For Location " & obj.Loc_Code & "")
                        End If
                        ArryLst.Add(New String() {strAc_Control, clsCommon.myCdbl(dtData.Rows(i)("Book_Source_value"))})
                    Next
                    qry = " select TSPL_Dep_AccountSet.WIP_AC,(sum(coalesce(Assemble.Item_Net_Amt,0))+max(coalesce(TSPL_ACQUISITION_head.Opening_Assemble_Amt,0))) as Amount  from TSPL_ACQUISITION_DETAIL  " &
                          " left outer join TSPL_ACQUISITION_head on TSPL_ACQUISITION_head.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code " &
                          " left outer join (select Acquisition_Code,Asset_Code,Amount,Item_Net_Amt from TSPL_ASSET_ASSEMBLE_DETAIL where  (Distribute='Y' or Type='Item')) as Assemble " &
                          " on Assemble.Acquisition_Code =TSPL_ACQUISITION_head.Acquisition_Code  and TSPL_ACQUISITION_DETAIL.Asset_Code=Assemble.Asset_Code " &
                          " left  outer join TSPL_Dep_AccountSet on TSPL_Dep_AccountSet.AcSet_Code=TSPL_ACQUISITION_DETAIL.AcSet_Code " &
                          " where TSPL_ACQUISITION_DETAIL.Acquisition_Code ='" & strDocNo & "' group by TSPL_Dep_AccountSet.WIP_AC"
                    Dim drDataCr As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If drDataCr IsNot Nothing AndAlso drDataCr.Rows.Count > 0 Then
                        For i As Integer = 0 To drDataCr.Rows.Count - 1
                            Dim strWIP_AC As String = clsERPFuncationality.ChangeGLAccountLocationSegment(drDataCr.Rows(i)("WIP_AC"), obj.Loc_Code, trans)
                            If clsCommon.myLen(strWIP_AC) <= 0 Then
                                Throw New Exception("GL Account " & drDataCr.Rows(i)("WIP_AC") & " not Found For Location " & obj.Loc_Code & "")
                            End If
                            ArryLst.Add(New String() {strWIP_AC, clsCommon.myCdbl(drDataCr.Rows(i)("Amount")) * -1})
                        Next
                    Else
                        Throw New Exception("No Issue entry found  for Acquistion Entry -" & strDocNo & " hence WIP Account could not be mapped. ")
                    End If
                    If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, strVoucherNoForRecreateOnly, trans, obj.Acquisition_Date, "Acquisition Entry Assembled Asset , Against Doc No:  " & strDocNo & strAcquisionDesc, "AQ-AS", "Acquisition Entry Assembled", strDocNo, "Acquisition Entry Assembled", "V", strDocNo, "Acquisition Entry Assembled", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
                    Else
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Acquisition_Date, "Acquisition Entry Assembled Asset , Against Doc No:  " & strDocNo & strAcquisionDesc, "AQ-AS", "Acquisition Entry Assembled", strDocNo, "Acquisition Entry Assembled", "V", strDocNo, "Acquisition Entry Assembled", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
                    End If

                End If
            End If
        End If
        '===============
        If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyFinancialCostCenter, clsFixedParameterCode.ApplyFinancialCostCenter, trans)) = "1", True, False)) = True Then
            Dim strCostCenterCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Top 1 CostCenter_Code from TSPL_ACQUISITION_DETAIL where Acquisition_Code = '" + strDocNo + "'", trans))
            Dim strHirerachyCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Top 1 Hirerachy_Code from TSPL_ACQUISITION_DETAIL where Acquisition_Code =  '" + strDocNo + "'", trans))
            clsERPFuncationality.UpdateCostCenterAndHirerachyCodeOnJE(strDocNo, "AQ-AS", strCostCenterCode, strHirerachyCode, trans)
        End If
        '==============
    End Sub

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document not found to Delete")
        End If
        Dim obj As clsAcquisitionHead = clsAcquisitionHead.GetData(strCode, NavigatorType.Current, trans)

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Acquisition_Code) > 0) Then
            Try
                If (obj.Status = ERPTransactionStatus.Approved) Then
                    Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Post_Date, "dd/MM/yyyy"))
                End If
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_ACQUISITION_HEAD", "Acquisition_Code", "TSPL_ACQUISITION_DETAIL", "Acquisition_Code", trans)
                Dim qry As String = "delete from TSPL_ACQUISITION_DETAIL where Acquisition_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_ASSET_ASSEMBLE_DETAIL where Acquisition_Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_ACQUISITION_HEAD where Acquisition_Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function
    ''UDL/16/10/18-000231 richa 
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Qry As String = "select status  from TSPL_ACQUISITION_HEAD where Acquisition_Code='" + strCode + "'"
            If Not clsCommon.CompairString(clsDBFuncationality.getSingleValue(Qry, trans), "1") = CompairStringResult.Equal Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "select Document_Code from TSPL_ASSET_WORK_HEAD where asset_Code='FA00453 ASSET'"


            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AQ-AS' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where  Source_Code='AQ-AS' and Source_Doc_No='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            'Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AQ-AS' and Source_Doc_No='" + strCode + "')"
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            'Qry = "delete from TSPL_JOURNAL_MASTER where  Source_Code='AQ-AS' and Source_Doc_No='" + strCode + "'"
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_INVENTORY_MOVEMENT", "Source_Doc_No", trans)
            Qry = "delete from tspl_inventory_movement where source_doc_no='" + strCode + "' and Trans_Type='ASS-ACQ-ENT'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Dim strApInvoiceNo As String
            Dim dtAsset As DataTable = clsDBFuncationality.GetDataTable("select Document_code from TSPL_ASSET_WORK_HEAD where asset_code in (select Asset_Code from TSPL_ACQUISITION_DETAIL where Acquisition_Code ='" & strCode & "')", trans)
            If dtAsset IsNot Nothing AndAlso dtAsset.Rows.Count > 0 Then
                Dim strDocNo As String = ""
                For Each dr As DataRow In dtAsset.Rows
                    If clsCommon.myLen(strDocNo) > 0 Then
                        strDocNo += ","
                    End If
                    strDocNo += clsCommon.myCstr(dr("Document_code"))
                Next
                Throw New Exception("Can't unpost because there is some Asset Expense Entries" + Environment.NewLine + strDocNo)
            End If

            strApInvoiceNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_Acquisition='" + strCode + "'", trans))
            If clsCommon.myLen(strApInvoiceNo) > 0 Then
                clsVedorInvoiceHead.ReverseAndUnpost(strApInvoiceNo, trans)
                clsVedorInvoiceHead.DeleteData(strApInvoiceNo, trans)
            End If

            Qry = "Update TSPL_ACQUISITION_HEAD set status = 0 where Acquisition_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_ACQUISITION_HEAD", "Acquisition_Code", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    ''---------------

    Public Shared Function CheckCode(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim Qry As String = "select count(Acquisition_Code) from TSPL_ACQUISITION_HEAD where Acquisition_Code='" & Code & "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans))
        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function GetListOfAssetAgainstIssue(ByVal strAcquisitionNo As String, ByVal ShowPendingAlso As Boolean, ByVal trans As SqlTransaction) As ArrayList
        Dim qry As String = String.Empty
        'qry = " select distinct Asset_Code  from TSPL_IssueItemToAssembledAsset_Detail where Asset_Code in (select Asset_Code from TSPL_ACQUISITION_DETAIL where Acquisition_Code ='" & strAcquisitionNo & "')" & _
        '      " union all " & _
        '      " select DTL.Asset_Code from TSPL_VENDOR_INVOICE_DETAIL DTL " & _
        '      " inner join TSPL_VENDOR_INVOICE_HEAD HEAD on DTL.Document_No=HEAD.Document_No " & _
        '      " WHERE HEAD.Invoice_Type='VS' AND DTL.Item_Type='A' AND DTL.Asset_Code in (select Asset_Code from TSPL_ACQUISITION_DETAIL where Acquisition_Code ='" & strAcquisitionNo & "') AND LEN(COALESCE(HEAD.Posting_Date,''))>0"
        qry = clsItemIssueToAssembledAsset.GetAssembleQuery(strAcquisitionNo, ShowPendingAlso, trans)
        qry = "select distinct [Asset Code] as [Asset Code] from (" & qry & ") Final"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim ArryList As New ArrayList
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each strasset As DataRow In dt.Rows
                ArryList.Add(clsCommon.myCstr(strasset.Item("Asset Code")))
            Next
        End If
        Return ArryList
    End Function
    Public Shared Function GetAssembleAmount(ByVal Asset_Code As String, ByVal trans As SqlTransaction) As Decimal
        'Dim qry As String = " select sum(dtl.Item_Net_Amt)  from TSPL_IssueItemToAssembledAsset_Detail dtl " & _
        '                    " inner join TSPL_IssueItemToAssembledAsset_Head Head on dtl.Doc_No=Head.Doc_No " & _
        '                    " where Head.Status=0 and Head.Asset_Code in " & _
        '                    " (select Asset_Code from TSPL_ACQUISITION_DETAIL where Asset_Code='" & Asset_Code & "')"
        Dim arr As New ArrayList
        arr.Add(Asset_Code)
        Dim qry As String = clsItemIssueToAssembledAsset.GetAssembleQuery(arr, False)
        qry = "select sum([Net Amount]) as [Net Amount] from (" & qry & ")  Final "
        Dim amount As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return amount
    End Function
    Public Shared Function GetSerialData(ByVal strDocType As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsSerializeInvenotry)
        Dim qry As String = "select TSPL_SERIAL_ITEM.*,Tag_No from TSPL_SERIAL_ITEM  LEFT JOIN TSPL_VISI_MASTER ON Serial_No=Auto_Sr_No where Document_Type='" + strDocType + "' and Document_Code='" + strDocNo + "' "
        If clsCommon.CompairString("ISSTRAN", strDocType) = CompairStringResult.Equal OrElse clsCommon.CompairString("PROD_RN", strDocType) = CompairStringResult.Equal OrElse clsCommon.CompairString("PROD_IS", strDocType) = CompairStringResult.Equal OrElse clsCommon.CompairString("Transfer", strDocType) = CompairStringResult.Equal OrElse clsCommon.CompairString("ITransfer", strDocType) = CompairStringResult.Equal Then
            qry += " and TSPL_SERIAL_ITEM.In_Out_Type='O'"
        End If

        qry += "  order by Line_No"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim Arr As List(Of clsSerializeInvenotry) = Nothing
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsSerializeInvenotry)
            For Each dr As DataRow In dt.Rows
                Dim objTr As clsSerializeInvenotry = New clsSerializeInvenotry()
                objTr.Code = clsCommon.myCstr(dr("Code"))
                objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                objTr.Parent_Line_No = clsCommon.myCdbl(dr("Parent_Line_No"))
                objTr.Auto_Sr_No = clsCommon.myCstr(dr("Auto_Sr_No"))
                objTr.Auto_BIN_No = clsCommon.myCstr(dr("Auto_BIN_No"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                objTr.In_Out_Type = clsCommon.myCstr(dr("In_Out_Type"))
                objTr.Against_Inv_Movement_Trans_Id = clsCommon.myCdbl(dr("Against_Inv_Movement_Trans_Id"))
                objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objTr.Document_Date = clsCommon.myCDate(dr("Document_Date"))
                objTr.Tag_No = clsCommon.myCstr(dr("Tag_No"))
                objTr.Allow_QC = clsCommon.myCstr(dr("QC_Complete"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function
End Class

Public Class clsAcquisitionDetail
#Region "Variables"
    Public Acquisition_Code As String = Nothing
    Public SNo As Integer = 0
    Public Asset_Code As String = Nothing
    Public Asset_Name As String = Nothing
    Public Templete_Code As String = Nothing
    Public Templete_Name As String = Nothing ''Nota a table column

    Public Category_code As String = Nothing
    Public Category_Name As String = Nothing ''Nota a table column
    Public Group_Code As String = Nothing
    Public Group_Code_Name As String = Nothing ''Nota a table column
    Public AcSet_Code As String = Nothing
    Public AcSet_Code_Name As String = Nothing ''Nota a table column
    Public Hirerachy_Code As String = Nothing
    Public CostCenter_Code As String = Nothing
    Public CostCenter_Name As String = Nothing ''Nota a table column

    Public Acqusition_Date As Date
    Public Dep_Method_Code As String = Nothing
    Public Dep_Method_Name As String = Nothing ''Nota a table column
    Public Dep_Period_Code As String = Nothing
    Public Dep_Period_Name As String = Nothing ''Nota a table column

    Public Dep_Method_Tax_Code As String = Nothing
    Public Dep_Method_Tax_Name As String = Nothing ''Nota a table column


    Public Start_Date As Date = Nothing
    Public Dep_Rate As Double = 0 '
    Public Book_Estimated_Life As Double = 0
    Public Book_Source_value As Double = 0
    Public Book_Source_Original_value As Double = 0
    Public Book_Salvage_Rate As Double = 0
    Public Book_Salvage_Value As Double = 0
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

    Public Asset_Specification As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public Dep_Tax_Rate As Double = 0
    Public Is_Assembled As Boolean = False
    Public Prefix_Type As String = Nothing
    Public Book_Dep_Type As String
    Public Tax_Dep_Type As String
    '=====
    Public No_Of_Rows_Qty As Double = 0
    Public No_Of_Rows_Qty_for_discount As Double = 0
    Public SRNQty As Double = 0
    Public SRN_Rate As Double = 0
    Public AssetsAmt As Double = 0
    Public SRN_No As String = ""
    Public Unit_Code As String = ""
    Public PI_No As String = ""
    Public Assetsfinder As String = ""

    '=====================Added by preeti gupta==========
    Public CapexSub_Code As String = Nothing
    Public Capex_Code As String = Nothing
    Public CapexType As String = Nothing
    Public IsCapex As Double = 0
    Public CapexQty As Double = 0
    ''====================14/04/2017====================================
    Public ItemAdd_Charge_Code1 As String = Nothing
    Public ItemAdd_Org_Charge_Amt1 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt1 As Double = Nothing
    Public ItemAdd_Charge_Code9 As String = Nothing
    Public ItemAdd_Org_Charge_Amt9 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt9 As Double = Nothing
    Public ItemAdd_Charge_Code8 As String = Nothing
    Public ItemAdd_Org_Charge_Amt8 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt8 As Double = Nothing
    Public ItemAdd_Charge_Code7 As String = Nothing
    Public ItemAdd_Org_Charge_Amt7 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt7 As Double = Nothing
    Public ItemAdd_Charge_Code6 As String = Nothing
    Public ItemAdd_Org_Charge_Amt6 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt6 As Double = Nothing
    Public ItemAdd_Charge_Code5 As String = Nothing
    Public ItemAdd_Org_Charge_Amt5 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt5 As Double = Nothing
    Public ItemAdd_Charge_Code4 As String = Nothing
    Public ItemAdd_Org_Charge_Amt4 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt4 As Double = Nothing
    Public ItemAdd_Charge_Code3 As String = Nothing
    Public ItemAdd_Org_Charge_Amt3 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt3 As Double = Nothing
    Public ItemAdd_Charge_Code2 As String = Nothing
    Public ItemAdd_Org_Charge_Amt2 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt2 As Double = Nothing
    Public ItemAdd_Charge_Code10 As String = Nothing
    Public ItemAdd_Org_Charge_Amt10 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt10 As Double = Nothing
    Public Total_ItemAdd_Charge As Double = Nothing

    Public Tax_Recoverable As Double = Nothing
    Public Tax_Non_Recoverable As Double = Nothing
    Public Put_To_Use As Boolean
    Public Asset_Serial_No As String = ""
    Public Depreciated_Value As Decimal = 0
    Public Asset_Expired_Life As Decimal = 0
    ''==================================================================



#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsAcquisitionDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsAcquisitionDetail In Arr
                Dim dtTemp As DataTable
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Acquisition_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                If clsCommon.myLen(obj.Asset_Code) <= 0 Then
                    obj.Asset_Code = clsERPFuncationality.GetNextCode(trans, obj.Acqusition_Date, clsDocType.FixedAsset, "", "")
                    If clsCommon.myLen(obj.Asset_Code) <= 0 Then
                        Throw New Exception("Error While Generation Asset Code")
                    End If
                    'Dim prefixcounter As String = ""
                    'Dim prefixcounter1 As String = ""
                    'Dim series As Integer = 0
                    'Dim ItemPrefix As Integer = 0
                    'Dim ItemPrefixCode As String = ""
                    'Dim AssetCode As String = ""
                    'prefixcounter = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Prefix_Counter  from TSPL_ASSET_GROUP where Group_Code='" + obj.Group_Code + "'", trans))
                    'If clsCommon.myLen(prefixcounter) <= 0 Then
                    '    prefixcounter = "AS"
                    'End If
                    'prefixcounter1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select prefix_counter from tspl_asset_category where category_code='" + obj.Category_code + "'", trans))
                    'series = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Series from tspl_asset_category where category_code='" + obj.Category_code + "'", trans))
                    'obj.Asset_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MAX(Asset_Code) from TSPL_ACQUISITION_DETAIL WHERE category_code='" + obj.Category_code + "' and Group_Code='" + obj.Group_Code + "'", trans))
                    'AssetCode = prefixcounter1 & "/" & prefixcounter & "/"
                    'If clsCommon.myLen(obj.Asset_Code) <= 0 Then
                    '    If series <= 0 Then
                    '        AssetCode += "000000"
                    '    Else
                    '        For i As Integer = 1 To series - 1
                    '            AssetCode += "0"
                    '        Next
                    '    End If

                    '    AssetCode += "1"
                    '    obj.Asset_Code = AssetCode
                    'Else
                    '    obj.Asset_Code = clsCommon.incval(obj.Asset_Code)
                    '    dtTemp = clsDBFuncationality.GetDataTable("select Acquisition_Code from TSPL_ACQUISITION_DETAIL where Asset_Code='" & obj.Asset_Code & "'", trans) ''ERO/03/09/19-001017 By Balwinder on 04/09/2019
                    '    While (dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0)
                    '        obj.Asset_Code = clsCommon.incval(obj.Asset_Code)
                    '        dtTemp = clsDBFuncationality.GetDataTable("select Acquisition_Code from TSPL_ACQUISITION_DETAIL where Asset_Code='" & obj.Asset_Code & "'", trans)
                    '    End While
                    'End If
                End If

                dtTemp = clsDBFuncationality.GetDataTable("select Acquisition_Code from TSPL_ACQUISITION_DETAIL where Asset_Code='" & obj.Asset_Code & "' and Acquisition_Code = '" + strDocNo + "'", trans)
                If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 1 Then
                    Throw New Exception("Repeated Asset Code " + obj.Asset_Code)
                End If
                dtTemp = clsDBFuncationality.GetDataTable("select Acquisition_Code from TSPL_ACQUISITION_DETAIL where Asset_Code='" & obj.Asset_Code & "' and Acquisition_Code <> '" + strDocNo + "'", trans)
                If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                    Throw New Exception("Asset Code " + obj.Asset_Code + " Already used is Acquisition No " + clsCommon.myCstr(dtTemp.Rows(0)("Acquisition_Code")))
                End If


                clsCommon.AddColumnsForChange(coll, "Asset_Code", obj.Asset_Code)
                clsCommon.AddColumnsForChange(coll, "Asset_Name", obj.Asset_Name)
                clsCommon.AddColumnsForChange(coll, "Templete_Code", obj.Templete_Code, True)
                clsCommon.AddColumnsForChange(coll, "Dep_Period_Code", obj.Dep_Period_Code, True)
                clsCommon.AddColumnsForChange(coll, "Dep_Rate", obj.Dep_Rate)
                clsCommon.AddColumnsForChange(coll, "Category_code", obj.Category_code, True)
                clsCommon.AddColumnsForChange(coll, "Group_Code", obj.Group_Code, True)
                clsCommon.AddColumnsForChange(coll, "AcSet_Code", obj.AcSet_Code, True)

                clsCommon.AddColumnsForChange(coll, "Hirerachy_Code", obj.Hirerachy_Code, True)
                clsCommon.AddColumnsForChange(coll, "CostCenter_Code", obj.CostCenter_Code, True)
                clsCommon.AddColumnsForChange(coll, "prefix_type", obj.Prefix_Type)
                clsCommon.AddColumnsForChange(coll, "Asset_Specification", obj.Asset_Specification)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Dep_Tax_Rate", obj.Dep_Tax_Rate)

                clsCommon.AddColumnsForChange(coll, "Put_To_Use", If(obj.Put_To_Use = True, "1", "0"))
                clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Acqusition_Date", clsCommon.GetPrintDate(obj.Acqusition_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Dep_Method_Code", obj.Dep_Method_Code, True)
                clsCommon.AddColumnsForChange(coll, "Dep_Method_Tax_Code", obj.Dep_Method_Tax_Code, True)
                clsCommon.AddColumnsForChange(coll, "Book_Estimated_Life", obj.Book_Estimated_Life)
                clsCommon.AddColumnsForChange(coll, "Book_Source_value", obj.Book_Source_value)

                If clsCommon.myCdbl(obj.Book_Source_Original_value) = 0 Then
                    clsCommon.AddColumnsForChange(coll, "Book_Source_Original_value", obj.Book_Source_value)
                Else
                    clsCommon.AddColumnsForChange(coll, "Book_Source_Original_value", obj.Book_Source_Original_value)
                End If
                clsCommon.AddColumnsForChange(coll, "Book_Salvage_Value", obj.Book_Salvage_Value)
                clsCommon.AddColumnsForChange(coll, "Book_Salvage_Rate", obj.Book_Salvage_Rate)

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
                clsCommon.AddColumnsForChange(coll, "Is_Assembled", If(obj.Is_Assembled = True, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Book_Dep_Type", obj.Book_Dep_Type)
                clsCommon.AddColumnsForChange(coll, "Tax_Dep_Type", obj.Tax_Dep_Type)
                '========
                clsCommon.AddColumnsForChange(coll, "SRNQty", obj.SRNQty)
                clsCommon.AddColumnsForChange(coll, "SRN_Rate", obj.SRN_Rate)
                clsCommon.AddColumnsForChange(coll, "SRN_No", obj.SRN_No, True)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "PI_No", obj.PI_No, True)
                '=======
                clsCommon.AddColumnsForChange(coll, "IsCapex", obj.IsCapex)
                clsCommon.AddColumnsForChange(coll, "CapexType", obj.CapexType)
                clsCommon.AddColumnsForChange(coll, "Capex_Code", obj.Capex_Code, True)
                clsCommon.AddColumnsForChange(coll, "Capex_SubCode", obj.CapexSub_Code, True)
                'clsCommon.AddColumnsForChange(coll, "CapexQty", obj.CapexQty)
                ''==========================14/04/2017======================
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code1", obj.ItemAdd_Charge_Code1)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt1", obj.ItemAdd_Org_Charge_Amt1)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt1", obj.ItemAdd_Calc_Charge_Amt1)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code9", obj.ItemAdd_Charge_Code9)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt9", obj.ItemAdd_Org_Charge_Amt9)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt9", obj.ItemAdd_Calc_Charge_Amt9)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code8", obj.ItemAdd_Charge_Code8)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt8", obj.ItemAdd_Org_Charge_Amt8)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt8", obj.ItemAdd_Calc_Charge_Amt8)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code7", obj.ItemAdd_Charge_Code7)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt7", obj.ItemAdd_Org_Charge_Amt7)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt7", obj.ItemAdd_Calc_Charge_Amt7)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code6", obj.ItemAdd_Charge_Code6)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt6", obj.ItemAdd_Org_Charge_Amt6)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt6", obj.ItemAdd_Calc_Charge_Amt6)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code5", obj.ItemAdd_Charge_Code5)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt5", obj.ItemAdd_Org_Charge_Amt5)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt5", obj.ItemAdd_Calc_Charge_Amt5)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code4", obj.ItemAdd_Charge_Code4)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt4", obj.ItemAdd_Org_Charge_Amt4)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt4", obj.ItemAdd_Calc_Charge_Amt4)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code3", obj.ItemAdd_Charge_Code3)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt3", obj.ItemAdd_Org_Charge_Amt3)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt3", obj.ItemAdd_Calc_Charge_Amt3)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code2", obj.ItemAdd_Charge_Code2)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt2", obj.ItemAdd_Org_Charge_Amt2)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt2", obj.ItemAdd_Calc_Charge_Amt2)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code10", obj.ItemAdd_Charge_Code10)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt10", obj.ItemAdd_Org_Charge_Amt10)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt10", obj.ItemAdd_Calc_Charge_Amt10)
                clsCommon.AddColumnsForChange(coll, "Total_ItemAdd_Charge", obj.Total_ItemAdd_Charge)
                clsCommon.AddColumnsForChange(coll, "Tax_Recoverable", obj.Tax_Recoverable)
                clsCommon.AddColumnsForChange(coll, "Tax_Non_Recoverable", obj.Tax_Non_Recoverable)

                clsCommon.AddColumnsForChange(coll, "Asset_Serial_No", obj.Asset_Serial_No, True)
                clsCommon.AddColumnsForChange(coll, "Depreciated_Value", obj.Depreciated_Value)
                clsCommon.AddColumnsForChange(coll, "Asset_Expired_Life", obj.Asset_Expired_Life)
                ''=================================================================
                Dim isExistAssetCode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("Select Count (*) from TSPL_ACQUISITION_DETAIL where Asset_Code='" & obj.Asset_Code & "' and Acquisition_Code = '" + strDocNo + "' ", trans))
                If isExistAssetCode = True Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACQUISITION_DETAIL", OMInsertOrUpdate.Update, "TSPL_ACQUISITION_DETAIL.Acquisition_Code='" + obj.Acquisition_Code + "' and TSPL_ACQUISITION_DETAIL.Asset_Code = '" + obj.Asset_Code + "' ", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACQUISITION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                End If
            Next
        End If
        Return True
    End Function
    Public Shared Function CheckCode(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim Qry As String = "select count(Asset_Code) from TSPL_ACQUISITION_DETAIL where Asset_Code='" & Code & "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans))
        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Shared Function UpdateDecpreciationData(ByVal strDocNo As String, ByVal Arr As List(Of clsAcquisitionDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsAcquisitionDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Dep_Method_Code", obj.Dep_Method_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Dep_Method_Tax_Code", obj.Dep_Method_Tax_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Dep_Period_Code", obj.Dep_Period_Code)
                    clsCommon.AddColumnsForChange(coll, "Dep_Rate", obj.Dep_Rate)
                    clsCommon.AddColumnsForChange(coll, "Dep_Tax_Rate", obj.Dep_Tax_Rate)
                    clsCommon.AddColumnsForChange(coll, "Asset_Name", obj.Asset_Name)
                    clsCommon.AddColumnsForChange(coll, "Asset_Specification", obj.Asset_Specification)
                    clsCommon.AddColumnsForChange(coll, "Book_Salvage_Rate", obj.Book_Salvage_Rate)
                    clsCommon.AddColumnsForChange(coll, "Book_Salvage_Value", obj.Book_Salvage_Value)
                    clsCommon.AddColumnsForChange(coll, "Book_Estimated_Life", obj.Book_Estimated_Life)
                    clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACQUISITION_DETAIL", OMInsertOrUpdate.Update, "Acquisition_Code='" + strDocNo + "' and Asset_Code='" + obj.Asset_Code + "'", trans)
                Next
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function GetName(ByVal strAssetCode As String) As String
        Dim qry As String = "select Asset_Name from TSPL_ACQUISITION_DETAIL where Asset_Code='" + strAssetCode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Function
    Public Shared Function CheckDuplicateAsset(ByVal strAssetCode As String, ByVal Doc_No As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select Acquisition_Code from TSPL_ACQUISITION_DETAIL where Asset_Code='" & strAssetCode & "' and Acquisition_Code<>'" & Doc_No & "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function GetICode(ByVal strAssetCode As String) As String
        Dim qry As String = "select Item_Code from TSPL_ACQUISITION_DETAIL where Asset_Code='" + strAssetCode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function GetFinder(ByVal strCode As String, ByVal isButtonClicked As Boolean, Optional ByVal WhrCls As String = "") As clsAcquisitionDetail
        Dim obj As clsAcquisitionDetail = Nothing
        Dim qry As String = " select DTL.Asset_Code as Code,DTL.Asset_Name as Description,DTL.Templete_Code as [Template],DTL.Category_code as Category,DTL.Group_Code as [Group Code]," & _
                            " DTL.AcSet_Code as [Account Set Code],DTL.CostCenter_Code as [Cost Center],DTL.Acqusition_Date as [Acquisition Date]," & _
                            " Dep_Method_Code as [Depreciation Method],Dep_Period_Code as [Depreciation Period],Start_Date as [Start Date],Dep_Rate as [Depreciation Rate],Book_Estimated_Life as [Book Estimated Life], " & _
                            " DTL.Book_Source_value as [Book Source Value],DTL.Book_Source_Original_value as [Original Value],DTL.Asset_Specification as [Asset Speciation], " & _
                            " DTL.Book_Salvage_Value as [Book Solvage Value],ScrapD.Document_No as [Scrap Doc No] from TSPL_ACQUISITION_DETAIL DTL " & _
                            " inner join TSPL_ACQUISITION_HEAD ACQ ON DTL.Acquisition_Code=ACQ.Acquisition_Code " & _
                            " LEFT JOIN TSPL_ASSET_SCRAP_DETAIL ScrapD on DTL.Asset_Code=ScrapD.Asset_Code"
        strCode = clsCommon.ShowSelectForm("AAAsset", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)

        If clsCommon.myLen(strCode) > 0 Then
            qry = qry & " where DTL.Asset_Code ='" + strCode + "' "
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                obj = New clsAcquisitionDetail()
                obj.Asset_Code = strCode
                obj.Asset_Name = clsCommon.myCstr(dt.Rows(0).Item("Description"))
                obj.Acqusition_Date = clsCommon.myCDate(dt.Rows(0).Item("Acquisition Date"))
                obj.AcSet_Code = clsCommon.myCstr(dt.Rows(0).Item("Account Set Code"))
                obj.Asset_Specification = clsCommon.myCstr(dt.Rows(0).Item("Asset Speciation"))
            End If

        End If
        Return obj
    End Function
    Public Shared Function GetAssetQuery()
        Dim QryDep As String = clsAssetDepreciation.GetAssetDepQuery()
        Dim qry As String = " Select ACQD.Asset_Code,ACQD.Asset_Name,ACQD.Asset_Specification,ACQ.Loc_Code,ACQ.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_LOCATION_MASTER.Location_Desc,ACQ.Acquisition_Date,ACQ.Acquisition_Code,ACQD.Templete_Code,ACQD.Category_code,TSPL_ASSET_CATEGORY.Description as CatDesc, " &
                            " ACQD.Group_Code,TSPL_ASSET_GROUP.Description as GrpDesc ,ACQD.AcSet_Code,ACQD.CostCenter_Code,TSPL_FA_COST_CENTER_MASTER.CostCenter_Name,ACQD.Dep_Method_Code,ACQD.Dep_Period_Code,ACQD.Start_Date,ACQD.Dep_Rate," &
                            " ACQD.Dep_Tax_Rate,ACQD.Book_Estimated_Life,ACQD.Book_Source_value as BookValue,ACQD.Book_Source_Original_value as OriginalBookValue,ACQD.SRN_No,isnull(acqd.depreciated_value,0) as [Opening Depreciation],coalesce(Dep.Final_Dep_Amount,0) as Final_Dep_Amount,Final_Dep_Amount+ACQD.Depreciated_Value as [Total Final Depreeciated],  coalesce(Dep.Final_Dep_Rate,0) as Final_Dep_Rate,coalesce(Dep.Final_Dep_Rate_tax,0) as Final_Dep_Rate_tax,coalesce(Dep.Final_Dep_Amount_Tax,0) as Final_Dep_Amount_Tax,coalesce(AW.Addition_Amount,0) as Addition_Amount,"
        '--"(ACQD.Book_Source_value+coalesce(AW.Addition_Amount,0)-coalesce(Dep.Final_Dep_Amount,0)) as Asset_Value
        qry += "(ACQD.Book_Source_value+coalesce(AW.Addition_Amount,0)) as Asset_Value" &
                            ",(ACQD.Book_Source_value+coalesce(AW.Addition_Amount,0)-coalesce(Dep.Final_Dep_Amount_Tax,0)) as Asset_Value_Tax, " &
                            " ACQD.Total_Tax_Amt,ACQD.Book_Salvage_Rate,ACQD.Book_Salvage_Value,ACQD.Is_Assembled,ACQ.Acquisition_Type from TSPL_ACQUISITION_DETAIL ACQD left join TSPL_ACQUISITION_HEAD ACQ on ACQD.Acquisition_Code=ACQ.Acquisition_Code " &
                            " left join (select Asset_Code,sum(Net_Amt) as Addition_Amount from  TSPL_ASSET_WORK_HEAD where Status=1  group by Asset_Code) as AW on ACQD.Asset_Code=AW.Asset_Code " &
                            " left join (" & QryDep & ") Dep on ACQD.Asset_Code=Dep.Asset_Code " &
                            " left outer join TSPL_LOCATION_MASTER on ACQ.Loc_Code=TSPL_LOCATION_MASTER.Location_Code  " &
                            " left outer join ( select TSPL_FA_COST_CENTER_MASTER.CostCenter_Code , CostCenter_Name from TSPL_FA_COST_CENTER_MASTER  union  select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as CostCenter_Code , TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name as CostCenter_Name from  TSPL_COST_CENTRE_FINANCIAL ) as TSPL_FA_COST_CENTER_MASTER on ACQD.CostCenter_Code=TSPL_FA_COST_CENTER_MASTER.CostCenter_Code  " &
                            " left outer join TSPL_ASSET_CATEGORY on ACQD.Category_code=TSPL_ASSET_CATEGORY.Category_Code " &
                            " left outer join TSPL_ASSET_GROUP on ACQD.Group_Code=TSPL_ASSET_GROUP.Group_Code " &
                            " left outer join TSPL_VENDOR_MASTER on ACQ.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code where 2=2 "
        Return qry
    End Function
    Public Shared Function getFinder1(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Acquisition_Code as [Acquisition Code],Asset_Code as [Code],Asset_Name as [Asset Name],Acqusition_Date as [Acqusition Date],PurchaseOrder_Qty as [PO Qty],Templete_Code  as [Templete Code] from TSPL_ACQUISITION_DETAIL "
        str = clsCommon.ShowSelectForm("asset", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
End Class
'Public Class clsAcquisitionPendingSRN
'#Region "Variables"
'    Public Acquisition_Code As String = Nothing
'    Public Item_Code As String = Nothing
'    Public Item_Name As String = Nothing
'    Public SRN_QTY As Double = 0
'    Public Unit_Rate As Double = 0
'    Public Unit_Code As String = Nothing
'    Public Amount As Double = 0
'    Public SRN_No As String = Nothing
'    Public lineNo As Integer = 0
'    Public PI_No As Integer = 0
'#End Region
'    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr1 As List(Of clsAcquisitionPendingSRN), ByVal trans As SqlTransaction) As Boolean

'        If (Arr1 IsNot Nothing AndAlso Arr1.Count > 0) Then
'            For Each obj As clsAcquisitionPendingSRN In Arr1
'                Dim coll As New Hashtable()
'                Dim IsNewAsset As Boolean = False
'                clsCommon.AddColumnsForChange(coll, "Acquisition_Code", IIf(clsCommon.myLen(strDocNo) <= 0, obj.Acquisition_Code, strDocNo))
'                clsCommon.AddColumnsForChange(coll, "lineNo", obj.lineNo)
'                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
'                clsCommon.AddColumnsForChange(coll, "Item_Name", obj.Item_Name)
'                clsCommon.AddColumnsForChange(coll, "SRN_QTY", obj.SRN_QTY)
'                clsCommon.AddColumnsForChange(coll, "Unit_Rate", obj.Unit_Rate)
'                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
'                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
'                clsCommon.AddColumnsForChange(coll, "SRN_No", obj.SRN_No)
'                clsCommon.AddColumnsForChange(coll, "PI_No", obj.PI_No)
'                If IsNewAsset Then
'                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACQUISITION_SRN_HEAD", OMInsertOrUpdate.Insert, "", trans)
'                Else
'                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACQUISITION_SRN_HEAD", OMInsertOrUpdate.Update, "Acquisition_Code='" + IIf(clsCommon.myLen(strDocNo) <= 0, obj.Acquisition_Code, strDocNo) + "'", trans)
'                End If
    '            Next
    '        End If
    '        Return True
    '    End Function

    'End Class
    Public Class clsDepreciationCalParameterType
    Public ReportType As String
    Public TransactionDate As Date
    Public To_Date As Date
    Public Item_Code_List As ArrayList = Nothing
    Public Trans_Type_List As ArrayList = Nothing
    Public State_List As ArrayList = Nothing
    Public Location_Code_List As ArrayList = Nothing
    Public Customer_Code_List As ArrayList = Nothing
    Public Item_Group_List As ArrayList = Nothing
    Public Cust_Group_Code_List As ArrayList = Nothing
    Public Document_Code As String = ""
    Public Unit_Code As String = ""
    Public Other_Cond As String = ""


End Class
Public Class clsDepreciationParameter
    Public Const BNV As String = "BNV"
    Public Const BNVName As String = "Book Net Value"
    Public Const BEY As String = "BEY"
    Public Const BEYName As String = "Book Estimated Life"
    Public Const BSV As String = "BSV"
    Public Const BSVName As String = "Book Salvage Value"
    Public Const BSR As String = "BSR"
    Public Const BSRName As String = "Book Salvage Rate"
    Public Const BDT As String = "BDT"
    Public Const BDTName As String = "Book Accumulated Depreciation count"
    Public Const BRT As String = "BRT"
    Public Const BRTName As String = "Book Depreciation Rate"
    Public Const BCLD As String = "BCLD"
    Public Const BCLDays As String = "Book Current Life in Days"
End Class

Public Class clsAssetAssembleDetail
#Region "Variables"
    Public Acquisition_Code As String = Nothing
    Public Asset_Code As String = Nothing
    Public Line_No As Integer = 0
    Public Type As String = Nothing
    Public Document_Code As String = Nothing
    Public Document_Date As Date = Nothing
    Public Item_No As String = Nothing
    Public Item_Desc As String = Nothing
    Public Hierarchy As String = Nothing
    Public CostCenter As String = Nothing
    Public Amount As Decimal = 0
    Public Total_Tax_Amt As Decimal = 0
    Public Item_Net_Amt As Decimal = 0
    Public Distribute As String = Nothing '' Y  or N
    Public Distribute_Amount As Decimal = 0
    Public Total_Amount As Decimal = 0

#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsAssetAssembleDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsAssetAssembleDetail In Arr
                Dim coll As New Hashtable()

                obj.Acquisition_Code = strDocNo
                clsCommon.AddColumnsForChange(coll, "Acquisition_Code", obj.Acquisition_Code)
                clsCommon.AddColumnsForChange(coll, "Asset_Code", obj.Asset_Code)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code, True)
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd-MMM-yyyy"))
                clsCommon.AddColumnsForChange(coll, "Item_No", obj.Item_No, True)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc, True)
                'sanjay
                'clsCommon.AddColumnsForChange(coll, "Hierarchy_Code", obj.Hierarchy)
                'clsCommon.AddColumnsForChange(coll, "CostCenter_Code", obj.CostCenter)
                'sanjay
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                clsCommon.AddColumnsForChange(coll, "Distribute", obj.Distribute)
                clsCommon.AddColumnsForChange(coll, "Distribute_Amount", obj.Distribute_Amount)
                clsCommon.AddColumnsForChange(coll, "Total_Amount", obj.Total_Amount)

                ''=================================================================
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_ASSEMBLE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetDetail(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsAssetAssembleDetail)
        Dim qry As String = ""
        Dim objList As New List(Of clsAssetAssembleDetail)
        Dim objTr As New clsAssetAssembleDetail
        qry = "select Acquisition_Code,Asset_Code,Line_No,Type,Document_Code,Document_Date,Item_No,Item_Desc,Amount,Total_Tax_Amt,Item_Net_Amt,Distribute,Distribute_Amount,Total_Amount from TSPL_ASSET_ASSEMBLE_DETAIL where Acquisition_Code='" & strDocNo & "'" ',isnull(Hierarchy_Code,'') as Hierarchy_Code,isnull(CostCenter_Code,'') as CostCenter_Code
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        For Each drData As DataRow In dt.Rows
            objTr.Acquisition_Code = clsCommon.myCstr(drData.Item("Acquisition_Code"))
            objTr.Asset_Code = clsCommon.myCstr(drData.Item("Asset_Code"))
            objTr.Line_No = clsCommon.myCdbl(drData.Item("Line_No"))
            objTr.Type = clsCommon.myCstr(drData.Item("Type"))
            objTr.Document_Code = clsCommon.myCstr(drData.Item("Document_Code"))
            objTr.Document_Date = clsCommon.myCDate(drData.Item("Document_Date"))
            objTr.Item_No = clsCommon.myCstr(drData.Item("Item_No"))
            objTr.Item_Desc = clsCommon.myCstr(drData.Item("Item_Desc"))
            'sanjay
            'objTr.Hierarchy = clsCommon.myCstr(drData.Item("Hierarchy_Code"))
            'objTr.CostCenter = clsCommon.myCstr(drData.Item("CostCenter_Code"))
            'sanjay
            objTr.Amount = clsCommon.myCdbl(drData.Item("Amount"))
            objTr.Total_Tax_Amt = clsCommon.myCdbl(drData.Item("Total_Tax_Amt"))
            objTr.Item_Net_Amt = clsCommon.myCdbl(drData.Item("Item_Net_Amt"))
            objTr.Distribute = clsCommon.myCstr(drData.Item("Distribute"))
            objTr.Distribute_Amount = clsCommon.myCdbl(drData.Item("Distribute_Amount"))
            objTr.Total_Amount = clsCommon.myCdbl(drData.Item("Total_Amount"))
            objList.Add(objTr)
        Next
        Return objList
    End Function
End Class

'Asset Account Change

Public Class clsAssetAccountChangeHead
#Region "Variables"
    Public Doc_Code As String = Nothing
    Public Doc_Date As DateTime = Nothing
    Public Loc_Code As String = Nothing
    Public Acquisition_Code As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Post_Date As DateTime? = Nothing
    Public Arr As List(Of clsAssetAccountChangeDetail) = Nothing


#End Region

    Public Function SaveData(ByVal obj As clsAssetAccountChangeHead, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsAssetAccountChangeHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFixedAsset, clsUserMgtCode.frmAssetAccountChange, obj.Loc_Code, obj.Doc_Date, trans)
        Try
            If Not isNewEntry Then
                Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Status from TSPL_ASSET_ACCOUNT_CHANGE_HEAD Where Doc_Code='" + obj.Doc_Code + "'", trans))
                If Status = 1 Then
                    Throw New Exception("This document is already posted.")
                End If
            End If


            Dim qry As String = "delete from TSPL_ASSET_ACCOUNT_CHANGE_DETAIL where Doc_Code='" + obj.Doc_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Loc_Code", obj.Loc_Code)
            clsCommon.AddColumnsForChange(coll, "Acquisition_Code", obj.Acquisition_Code)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                obj.Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Doc_Date, clsDocType.AssetAccountChange, "", obj.Loc_Code)
                If (clsCommon.myLen(obj.Doc_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.Doc_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_ACCOUNT_CHANGE_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_ACCOUNT_CHANGE_HEAD", OMInsertOrUpdate.Update, "TSPL_ASSET_ACCOUNT_CHANGE_HEAD.Doc_Code='" + obj.Doc_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsAssetAccountChangeDetail.SaveData(obj.Doc_Code, Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsAssetAccountChangeHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsAssetAccountChangeHead
        Dim obj As clsAssetAccountChangeHead = Nothing
        Dim qry As String = "SELECT TSPL_ASSET_ACCOUNT_CHANGE_HEAD.*  FROM TSPL_ASSET_ACCOUNT_CHANGE_HEAD  "
        qry += " where 2=2  "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_ASSET_ACCOUNT_CHANGE_HEAD.Doc_Code = (select MIN(Doc_Code) from TSPL_ASSET_ACCOUNT_CHANGE_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_ASSET_ACCOUNT_CHANGE_HEAD.Doc_Code = (select Max(Doc_Code) from TSPL_ASSET_ACCOUNT_CHANGE_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_ASSET_ACCOUNT_CHANGE_HEAD.Doc_Code = (select Min(Doc_Code) from TSPL_ASSET_ACCOUNT_CHANGE_HEAD where Doc_Code>'" + strPONo + "'" + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_ASSET_ACCOUNT_CHANGE_HEAD.Doc_Code = (select Max(Doc_Code) from TSPL_ASSET_ACCOUNT_CHANGE_HEAD where Doc_Code<'" + strPONo + "'" + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_ASSET_ACCOUNT_CHANGE_HEAD.Doc_Code = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsAssetAccountChangeHead()
            obj.Doc_Code = clsCommon.myCstr(dt.Rows(0)("Doc_Code"))
            obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
            obj.Loc_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

            obj.Acquisition_Code = clsCommon.myCstr(dt.Rows(0)("Acquisition_Code"))


            If dt.Rows(0)("Post_Date") IsNot DBNull.Value Then
                obj.Post_Date = clsCommon.myCDate(dt.Rows(0)("Post_Date"))
            End If

            qry = "SELECT  TSPL_ASSET_ACCOUNT_CHANGE_DETAIL.* from TSPL_ASSET_ACCOUNT_CHANGE_DETAIL " + Environment.NewLine
            qry += " where TSPL_ASSET_ACCOUNT_CHANGE_DETAIL.Doc_Code='" + obj.Doc_Code + "' ORDER BY TSPL_ASSET_ACCOUNT_CHANGE_DETAIL.SNo" + Environment.NewLine
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsAssetAccountChangeDetail)
                Dim objTr As clsAssetAccountChangeDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsAssetAccountChangeDetail
                    objTr.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                    objTr.SNo = clsCommon.myCdbl(clsCommon.myCdbl(dr("SNo")))
                    objTr.Asset_Code = clsCommon.myCstr(dr("Asset_Code"))
                    objTr.Asset_Name = clsCommon.myCstr(dr("Asset_Name"))
                    objTr.Asset_Specification = clsCommon.myCstr(dr("Asset_Specification"))
                    objTr.AcSet_Code = clsCommon.myCstr(dr("AcSet_Code"))
                    objTr.AcSet_Code_Name = clsCommon.myCstr(dr("AcSet_Code_Name"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_amt"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Name = clsCommon.myCstr(dr("Item_Name"))
                    objTr.Ac_Code = clsCommon.myCstr(dr("Ac_Code"))
                    objTr.Ac_Name = clsCommon.myCstr(dr("Ac_Name"))
                    objTr.ChangedAc_Code = clsCommon.myCstr(dr("ChangedAc_Code"))
                    objTr.ChangedAc_Name = clsCommon.myCstr(dr("ChangedAc_Name"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.CostCenter_Code = clsCommon.myCstr(dr("CostCenter_Code"))
                    objTr.Hirerachy_Code = clsCommon.myCstr(dr("Hirerachy_Code"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function

    Public Shared Function PostData(ByVal Form_Id As String, ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String
        Try

            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim dtPostDate As DateTime = clsCommon.GETSERVERDATE(trans) ' clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsAssetAccountChangeHead = clsAssetAccountChangeHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.Status = 1) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Post_Date, "dd/MM/yyyy"))
            End If


            Dim ArryLst As ArrayList = New ArrayList()
            'Dim strQ As String = "select xx.Ac_Control, SUM(xx.Book_Source_value )  as Book_Source_value " &
            '        " from ( select TSPL_ACQUISITION_DETAIL.Book_Source_value,TSPL_Dep_AccountSet.Ac_Control as Ac_Control " &
            '        " from TSPL_ACQUISITION_DETAIL left join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_DETAIL.Acquisition_Code=TSPL_ACQUISITION_HEAD.Acquisition_Code " &
            '        " left outer join TSPL_Dep_AccountSet on TSPL_Dep_AccountSet.AcSet_Code=TSPL_ACQUISITION_DETAIL.AcSet_Code " &
            '        " left join TSPL_VENDOR_MASTER Vendor on TSPL_ACQUISITION_HEAD.Vendor_Code=Vendor.Vendor_Code " &
            '        " left join TSPL_VENDOR_ACCOUNT_SET on Vendor.Vendor_Account=TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code " &
            '        " where TSPL_ACQUISITION_DETAIL.Acquisition_Code ='" & strDocNo & "' and TSPL_ACQUISITION_DETAIL.Is_Assembled='1')  as xx group by xx.Ac_Control"
            Dim strQ As String = "select * from TSPL_ASSET_ACCOUNT_CHANGE_DETAIL" &
                     " where TSPL_ASSET_ACCOUNT_CHANGE_DETAIL.Doc_Code ='" & strDocNo & "'"
            Dim dtData As DataTable = clsDBFuncationality.GetDataTable(strQ, trans)
            If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                For i As Integer = 0 To dtData.Rows.Count - 1
                    Dim strAc_Control As String = clsERPFuncationality.ChangeGLAccountLocationSegment(dtData.Rows(i)("ChangedAc_Code"), obj.Loc_Code, trans)
                    If clsCommon.myLen(strAc_Control) <= 0 Then
                        Throw New Exception("GL Account " & dtData.Rows(i)("ChangedAc_Code") & " not Found For Location " & obj.Loc_Code & "")
                    End If
                    ArryLst.Add(New String() {strAc_Control, clsCommon.myCdbl(dtData.Rows(i)("Item_Net_Amt")), "", "", clsCommon.myCstr(dtData.Rows(i)("Hirerachy_code")), clsCommon.myCstr(dtData.Rows(i)("costCenter_code"))})

                    Dim strWIP_AC As String = clsERPFuncationality.ChangeGLAccountLocationSegment(dtData.Rows(i)("Ac_Code"), obj.Loc_Code, trans)
                    If clsCommon.myLen(strWIP_AC) <= 0 Then
                        Throw New Exception("GL Account " & dtData.Rows(i)("Ac_Code") & " not Found For Location " & obj.Loc_Code & "")
                    End If
                    ArryLst.Add(New String() {strWIP_AC, clsCommon.myCdbl(dtData.Rows(i)("Item_Net_Amt")) * -1, "", "", clsCommon.myCstr(dtData.Rows(i)("Hirerachy_code")), clsCommon.myCstr(dtData.Rows(i)("costCenter_code"))})
                Next
                'qry = " select TSPL_Dep_AccountSet.WIP_AC,(sum(coalesce(Assemble.Item_Net_Amt,0))+max(coalesce(TSPL_ACQUISITION_head.Opening_Assemble_Amt,0))) as Amount  from TSPL_ACQUISITION_DETAIL  " &
                '              " left outer join TSPL_ACQUISITION_head on TSPL_ACQUISITION_head.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code " &
                '              " left outer join (select Acquisition_Code,Asset_Code,Amount,Item_Net_Amt from TSPL_ASSET_ASSEMBLE_DETAIL where  (Distribute='Y' or Type='Item')) as Assemble " &
                '              " on Assemble.Acquisition_Code =TSPL_ACQUISITION_head.Acquisition_Code  and TSPL_ACQUISITION_DETAIL.Asset_Code=Assemble.Asset_Code " &
                '              " left  outer join TSPL_Dep_AccountSet on TSPL_Dep_AccountSet.AcSet_Code=TSPL_ACQUISITION_DETAIL.AcSet_Code " &
                '              " where TSPL_ACQUISITION_DETAIL.Acquisition_Code ='" & strDocNo & "' group by TSPL_Dep_AccountSet.WIP_AC"
                'Dim drDataCr As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                'If drDataCr IsNot Nothing AndAlso drDataCr.Rows.Count > 0 Then
                '    For i As Integer = 0 To drDataCr.Rows.Count - 1
                '        Dim strWIP_AC As String = clsERPFuncationality.ChangeGLAccountLocationSegment(drDataCr.Rows(i)("WIP_AC"), obj.Loc_Code, trans)
                '        If clsCommon.myLen(strWIP_AC) <= 0 Then
                '            Throw New Exception("GL Account " & drDataCr.Rows(i)("WIP_AC") & " not Found For Location " & obj.Loc_Code & "")
                '        End If
                '        ArryLst.Add(New String() {strWIP_AC, clsCommon.myCdbl(drDataCr.Rows(i)("Amount")) * -1})
                '    Next
                'Else
                '    Throw New Exception("No Issue entry found  for Acquistion Entry -" & strDocNo & " hence WIP Account could not be mapped. ")
                'End If
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Doc_Date, "Asset Account Change, Against Acquisition Code:  " & obj.Acquisition_Code, "AQ-AC", "Asset Account Change", strDocNo, "Asset Account Change", "V", strDocNo, "Asset Account Change", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
            End If
            '    End If
            'End If
            qry = "Update TSPL_ASSET_ACCOUNT_CHANGE_HEAD set Status=1, Post_Date='" + clsCommon.GetPrintDate(dtPostDate, "dd/MMM/yyyy hh:mm tt") + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Doc_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = ""
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document not found to Delete")
        End If
        Dim obj As clsAssetAccountChangeHead = clsAssetAccountChangeHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
            Try
                If (obj.Status = ERPTransactionStatus.Approved) Then
                    Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Post_Date, "dd/MM/yyyy"))
                End If

                qry = "delete from TSPL_ASSET_ACCOUNT_CHANGE_DETAIL where Doc_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_ASSET_ACCOUNT_CHANGE_HEAD where Doc_Code='" + strCode + "'"
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
    ''UDL/16/10/18-000231 richa 
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Qry As String = "select status  from TSPL_ASSET_ACCOUNT_CHANGE_HEAD where Doc_Code='" + strCode + "'"
            If Not clsCommon.CompairString(clsDBFuncationality.getSingleValue(Qry, trans), "1") = CompairStringResult.Equal Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AQ-AC' and Source_Doc_No='" + strCode + "')"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from TSPL_JOURNAL_MASTER where  Source_Code='AQ-AC' and Source_Doc_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)


            Qry = "Update TSPL_ASSET_ACCOUNT_CHANGE_HEAD set status = 0,post_date=null where Doc_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    ''---------------

    'Public Shared Function CheckCode(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
    '    Dim Qry As String = "select count(Acquisition_Code) from TSPL_ACQUISITION_HEAD where Acquisition_Code='" & Code & "'"
    '    Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans))
    '    If count > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function


End Class

Public Class clsAssetAccountChangeDetail
#Region "Variables"
    Public Doc_Code As String = Nothing
    Public SNo As Integer = 0
    Public Asset_Code As String = Nothing
    Public Asset_Name As String = Nothing
    Public Asset_Specification As String = Nothing
    Public AcSet_Code As String = Nothing
    Public AcSet_Code_Name As String = Nothing
    Public Item_Net_Amt As Double = 0
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public Ac_Code As String = Nothing
    Public Ac_Name As String = Nothing
    Public ChangedAc_Code As String = Nothing
    Public ChangedAc_Name As String = Nothing
    Public Remarks As String = Nothing
    Public Hirerachy_Code As String = Nothing
    Public CostCenter_Code As String = Nothing
    Public CostCenter_Name As String = Nothing

#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsAssetAccountChangeDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsAssetAccountChangeDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "Asset_Code", obj.Asset_Code)
                clsCommon.AddColumnsForChange(coll, "Asset_Name", obj.Asset_Name)
                clsCommon.AddColumnsForChange(coll, "Asset_Specification", obj.Asset_Specification)
                clsCommon.AddColumnsForChange(coll, "AcSet_Code", obj.AcSet_Code, True)
                clsCommon.AddColumnsForChange(coll, "AcSet_Code_Name", obj.AcSet_Code_Name, True)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Item_Name", obj.Item_Name, True)
                clsCommon.AddColumnsForChange(coll, "Ac_Code", obj.Ac_Code, True)
                clsCommon.AddColumnsForChange(coll, "Ac_Name", obj.Ac_Name, True)
                clsCommon.AddColumnsForChange(coll, "ChangedAc_Code", obj.ChangedAc_Code, True)
                clsCommon.AddColumnsForChange(coll, "ChangedAc_Name", obj.ChangedAc_Name, True)
                clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "CostCenter_Code", obj.CostCenter_Code, True)
                'clsCommon.AddColumnsForChange(coll, "CostCenter_Name", obj.CostCenter_Name)
                clsCommon.AddColumnsForChange(coll, "Hirerachy_Code", obj.Hirerachy_Code, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_ACCOUNT_CHANGE_DETAIL", OMInsertOrUpdate.Insert, "", trans)

            Next
        End If
        Return True
    End Function
End Class

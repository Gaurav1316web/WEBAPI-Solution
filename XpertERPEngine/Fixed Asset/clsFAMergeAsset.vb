Imports common
Imports System.Data.SqlClient
Public Class clsFAMergeHead
#Region "Variables"
    Public Merge_Asset_Code As String = Nothing
    Public statusnewold As String = Nothing
    Public Acquisition_Code As String = Nothing
    Public Acquisition_Date As DateTime = Nothing
    Public Loc_Code As String = Nothing
    Public PO_No As String = Nothing
    Public SRN_No As String = Nothing
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
    Public Arr As List(Of clsFAMergeDetail) = Nothing
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
#End Region

    Public Function SaveData(ByVal obj As clsFAMergeHead, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()

            Return True
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
    End Function

    Public Function SaveData(ByVal obj As clsFAMergeHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Fixed Asset", "Asset Merging Entry", obj.Loc_Code, obj.Acquisition_Date, trans)
        Try
            If Not isNewEntry Then
                Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Status from TSPL_ACQUISITION_HEAD Where Acquisition_Code='" + obj.Acquisition_Code + "'", trans))
                If Status = 1 Then
                    Throw New Exception("This document is already posted.")
                End If
            End If
            Dim arry As New ArrayList
            arry = GetListOfAssetAgainstIssue(obj.Acquisition_Code, True, trans)

            Dim qry As String = "delete from TSPL_ACQUISITION_DETAIL where Acquisition_Code='" + obj.Acquisition_Code + "' and asset_Code not in (" + clsCommon.GetMulcallString(arry) + ") "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_ACQUISITION_ASSET_MERGE_DETAIL where Acquisition_Code='" + obj.Acquisition_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_ACQUISITION_DETAIL set Asset_Merged=0,Merge_Asset_Code=NULL where Asset_Code='" & Merge_Asset_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_REMITTANCE where Document_No='" + obj.Acquisition_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Acquisition_Date", clsCommon.GetPrintDate(obj.Acquisition_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "PO_No", obj.PO_No, True)
            clsCommon.AddColumnsForChange(coll, "SRN_No", obj.SRN_No, True)
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
            isSaved = isSaved AndAlso clsFAMergeDetail.SaveData(obj.Acquisition_Code, Arr, trans)
            isSaved = isSaved AndAlso clsRemittance.SaveData(obj.RemittanceObject, obj.Acquisition_Code, Loc_Code, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsFAMergeHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsFAMergeHead
        Dim obj As clsFAMergeHead = Nothing
        Try
            Dim qry As String = "SELECT TSPL_ACQUISITION_HEAD.*,TSPL_VENDOR_MASTER.Vendor_Name  FROM TSPL_ACQUISITION_HEAD  "
            qry += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_ACQUISITION_HEAD.Vendor_Code"
            qry += " where 2=2 and TSPL_ACQUISITION_HEAD.Acquisition_Type='Merge' "
            Dim whrClas As String = ""
            Select Case NavType
                Case NavigatorType.First
                    qry += " and TSPL_ACQUISITION_HEAD.Acquisition_Code = (select MIN(Acquisition_Code) from TSPL_ACQUISITION_HEAD where 1=1 and TSPL_ACQUISITION_HEAD.Acquisition_Type='Merge' " + whrClas + ")"
                Case NavigatorType.Last
                    qry += " and TSPL_ACQUISITION_HEAD.Acquisition_Code = (select Max(Acquisition_Code) from TSPL_ACQUISITION_HEAD where 1=1 and TSPL_ACQUISITION_HEAD.Acquisition_Type='Merge' " + whrClas + ")"
                Case NavigatorType.Next
                    qry += " and TSPL_ACQUISITION_HEAD.Acquisition_Code = (select Min(Acquisition_Code) from TSPL_ACQUISITION_HEAD where Acquisition_Code>'" + strPONo + "' and TSPL_ACQUISITION_HEAD.Acquisition_Type='Merge' " + whrClas + ")"
                Case NavigatorType.Previous
                    qry += " and TSPL_ACQUISITION_HEAD.Acquisition_Code = (select Max(Acquisition_Code) from TSPL_ACQUISITION_HEAD where Acquisition_Code<'" + strPONo + "' and TSPL_ACQUISITION_HEAD.Acquisition_Type='Merge' " + whrClas + ")"
                Case NavigatorType.Current
                    qry += " and TSPL_ACQUISITION_HEAD.Acquisition_Code = '" + strPONo + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsFAMergeHead()
                obj.RemittanceObject = clsRemittance.GetData(strPONo, trans)
                obj.Acquisition_Code = clsCommon.myCstr(dt.Rows(0)("Acquisition_Code"))
                obj.statusnewold = clsCommon.myCstr(dt.Rows(0)("Status_New_Old"))
                obj.Acquisition_Date = clsCommon.myCDate(dt.Rows(0)("Acquisition_Date"))
                obj.PO_No = clsCommon.myCstr(dt.Rows(0)("PO_No"))
                obj.SRN_No = clsCommon.myCstr(dt.Rows(0)("SRN_No"))
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
                    obj.Arr = New List(Of clsFAMergeDetail)
                    Dim objTr As clsFAMergeDetail
                    For Each dr As DataRow In dt.Rows
                        objTr = New clsFAMergeDetail
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
                        objTr.CostCenter_Code = clsCommon.myCstr(dr("CostCenter_Code"))
                        objTr.CostCenter_Name = clsCommon.myCstr(dr("CostCenter_Name"))
                        objTr.Acqusition_Date = clsCommon.myCDate(dr("Acqusition_Date"))

                        objTr.Dep_Method_Code = clsCommon.myCstr(dr("Dep_Method_Code"))
                        objTr.Dep_Method_Name = clsCommon.myCstr(dr("Dep_Method_Name"))

                        objTr.Dep_Method_Tax_Code = clsCommon.myCstr(dr("Dep_Method_Tax_Code"))
                        objTr.Dep_Method_Tax_Name = clsCommon.myCstr(dr("Dep_Method_Tax_Name"))

                        objTr.Dep_Period_Code = clsCommon.myCstr(dr("Dep_Period_Code"))
                        objTr.Dep_Period_Name = clsCommon.myCstr(dr("Dep_Period_Name"))
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

                        objTr.arrMerged_AssetCode = New List(Of clsFAMergeDetail)
                        objTr.arrMerged_AssetCode = GetMergedAssetDetail(obj.Acquisition_Code, objTr.Asset_Code, trans)

                        obj.Arr.Add(objTr)
                    Next
                End If
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetMergedAssetDetail(ByVal strDocCode As String, ByVal Asset_Code As String, ByVal trans As SqlTransaction) As List(Of clsFAMergeDetail)
        Dim arr As New List(Of clsFAMergeDetail)
        Dim dt As New DataTable()
        Dim obj As New clsFAMergeDetail()
        Try
            Dim qry As String = "select old_asset_code,Net_Amt_After_Dep,Calc_Type from TSPL_ACQUISITION_ASSET_MERGE_DETAIL where Acquisition_Code='" + strDocCode + "' and Merge_Asset_Code='" + Asset_Code + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    obj = New clsFAMergeDetail()

                    obj.Asset_Code = clsCommon.myCstr(dr("old_asset_code"))
                    obj.Net_Amt_After_Dep = clsCommon.myCdbl(dr("Net_Amt_After_Dep"))
                    obj.Calc_Type = clsCommon.myCstr(dr("Calc_Type"))

                    arr.Add(obj)
                Next
            End If

            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
        End Try
    End Function

    Public Shared Function PostData(ByVal Form_Id As String, ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal oldnewstatus As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(Form_Id, strDocNo, isCheckForPosted, oldnewstatus, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal Form_Id As String, ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal oldnewstatus As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsFAMergeHead = clsFAMergeHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Acquisition_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.Status = 1) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Post_Date, "dd/MM/yyyy"))
            End If
            If (isCheckForPosted AndAlso obj.On_Hold) Then
                Throw New Exception("Document No " + obj.Acquisition_Code + " Is On Hold.Can't Post it")
            End If

            CreateJournalEntry(obj, trans)

            qry = "Update TSPL_ACQUISITION_HEAD set Status=1, Post_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Acquisition_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CreateJournalEntry(ByVal obj As clsFAMergeHead, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim ArryLst As ArrayList = New ArrayList()
            Dim qry As String = "select TSPL_ACQUISITION_DETAIL.Book_Source_value as Book_Source_value,TSPL_Dep_AccountSet.Ac_Control as WIP_AC " & _
                        " from TSPL_ACQUISITION_DETAIL left join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_DETAIL.Acquisition_Code=TSPL_ACQUISITION_HEAD.Acquisition_Code  " & _
                        " left outer join TSPL_Dep_AccountSet on TSPL_Dep_AccountSet.AcSet_Code=TSPL_ACQUISITION_DETAIL.AcSet_Code where TSPL_ACQUISITION_DETAIL.acquisition_code='" + obj.Acquisition_Code + "' " & _
                        " union all " & _
                        "select -1 * TSPL_ACQUISITION_ASSET_MERGE_DETAIL.net_amt_after_dep as Book_Source_value,TSPL_Dep_AccountSet.Ac_Control as WIP_AC " & _
                        " from TSPL_ACQUISITION_ASSET_MERGE_DETAIL left join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_ASSET_MERGE_DETAIL.Acquisition_Code=TSPL_ACQUISITION_HEAD.Acquisition_Code  " & _
                        " left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_ASSET_MERGE_DETAIL.old_asset_code=TSPL_ACQUISITION_DETAIL.asset_code " & _
                        " left outer join TSPL_Dep_AccountSet on TSPL_Dep_AccountSet.AcSet_Code=TSPL_ACQUISITION_DETAIL.AcSet_Code where TSPL_ACQUISITION_ASSET_MERGE_DETAIL.acquisition_code='" + obj.Acquisition_Code + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim strWIP_AC As String = clsERPFuncationality.ChangeGLAccountLocationSegment(dt.Rows(i)("WIP_AC"), obj.Loc_Code, trans)
                    If clsCommon.myLen(strWIP_AC) <= 0 Then
                        Throw New Exception("GL Account " & dt.Rows(i)("WIP_AC") & " not Found For Location " & obj.Loc_Code & "")
                    End If
                    
                    ArryLst.Add(New String() {strWIP_AC, clsCommon.myCdbl(dt.Rows(i)("Book_Source_value"))})
                Next
                transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Acquisition_Date, "Acquisition Entry of Merged Asset , Against Doc No:  " & obj.Acquisition_Code, "AQ-AS", "Acquisition Entry Merged", obj.Acquisition_Code, "Acquisition Entry Merged", "V", obj.Acquisition_Code, "Acquisition Entry Merged", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal Merge_Asset_Code As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, Merge_Asset_Code, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal Merge_Asset_Code As String, ByVal trans As SqlTransaction) As Boolean

        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Document not found to Delete")
            End If
            Dim obj As clsFAMergeHead = clsFAMergeHead.GetData(strCode, NavigatorType.Current, trans)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Acquisition_Code) > 0) Then
                If (obj.Status = ERPTransactionStatus.Approved) Then
                    Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Post_Date, "dd/MM/yyyy"))
                End If
                Dim qry As String = "delete from TSPL_ACQUISITION_DETAIL where Acquisition_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_ACQUISITION_ASSET_MERGE_DETAIL where Acquisition_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "update TSPL_ACQUISITION_DETAIL set Asset_Merged=0,Merge_Asset_Code=NULL where Asset_Code='" & Merge_Asset_Code & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_ACQUISITION_HEAD where Acquisition_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

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
        Dim arr As New ArrayList
        arr.Add(Asset_Code)
        Dim qry As String = clsItemIssueToAssembledAsset.GetAssembleQuery(arr, False)
        qry = "select sum([Net Amount]) as [Net Amount] from (" & qry & ")  Final "
        Dim amount As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return amount
    End Function

End Class

Public Class clsFAMergeDetail
#Region "Variables"
    Public arrMerged_AssetCode As New List(Of clsFAMergeDetail)
    Public Net_Amt_After_Dep As Double = Nothing
    Public Calc_Type As String = Nothing

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
    Public SRNQty As Double = 0
    Public SRN_Rate As Double = 0
    Public SRN_No As String = ""
    Public Unit_Code As String = ""
    Public PI_No As String = ""

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsFAMergeDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sno As Integer = 1
            For Each obj As clsFAMergeDetail In Arr
                Dim coll As New Hashtable()
                Dim IsNewAsset As Boolean = True

                Dim qry As String = "select count(*)  from TSPL_IssueItemToAssembledAsset_Detail where Asset_Code in (select Asset_Code from TSPL_ACQUISITION_DETAIL where Acquisition_Code ='" + strDocNo + "' and Asset_Code='" + obj.Asset_Code + "')"
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                    IsNewAsset = False
                Else
                    IsNewAsset = True
                End If
                obj.Acquisition_Code = strDocNo
                clsCommon.AddColumnsForChange(coll, "Acquisition_Code", IIf(clsCommon.myLen(strDocNo) <= 0, obj.Acquisition_Code, strDocNo))
                clsCommon.AddColumnsForChange(coll, "SNo", sno)
                sno += 1
                If clsCommon.myLen(obj.Asset_Code) <= 0 Then
                    IsNewAsset = True

                    Dim prefixcounter As String = ""
                    Dim prefixcounter1 As String = ""
                    Dim series As Integer = 0
                    Dim AssetCode As String = ""

                    '---------do changes--------asset code generated AssetGroupwise--------------------------------------
                    prefixcounter = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Prefix_Counter  from TSPL_ASSET_GROUP where Group_Code='" + obj.Group_Code + "'", trans))
                    If clsCommon.myLen(prefixcounter) <= 0 Then
                        prefixcounter = "AS"
                    End If
                    '---------do changes--------asset code generated categorywise--------------------------------------
                    prefixcounter1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select prefix_counter from tspl_asset_category where category_code='" + obj.Category_code + "'", trans))
                    series = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Series from tspl_asset_category where category_code='" + obj.Category_code + "'", trans))
                    obj.Asset_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MAX(Asset_Code) from TSPL_ACQUISITION_DETAIL WHERE category_code='" + obj.Category_code + "' and Group_Code='" + obj.Group_Code + "'", trans))
                    AssetCode = prefixcounter1 & "/" & prefixcounter & "/"

                    If clsCommon.myLen(obj.Asset_Code) <= 0 Then
                        If series <= 0 Then
                            AssetCode += "000000"
                        Else
                            For i As Integer = 1 To series - 1
                                AssetCode += "0"
                            Next
                        End If

                        AssetCode += "1"
                        obj.Asset_Code = AssetCode
                    Else
                        obj.Asset_Code = clsCommon.incval(obj.Asset_Code)
                    End If
                Else
                    IsNewAsset = Not CheckCode(obj.Asset_Code, trans)
                End If

                clsCommon.AddColumnsForChange(coll, "Asset_Code", obj.Asset_Code)
                clsCommon.AddColumnsForChange(coll, "Asset_Name", obj.Asset_Name)
                clsCommon.AddColumnsForChange(coll, "Templete_Code", obj.Templete_Code, True)
                clsCommon.AddColumnsForChange(coll, "Dep_Period_Code", obj.Dep_Period_Code, True)
                clsCommon.AddColumnsForChange(coll, "Dep_Rate", obj.Dep_Rate)
                clsCommon.AddColumnsForChange(coll, "Category_code", obj.Category_code, True)
                clsCommon.AddColumnsForChange(coll, "Group_Code", obj.Group_Code, True)
                clsCommon.AddColumnsForChange(coll, "AcSet_Code", obj.AcSet_Code, True)
                clsCommon.AddColumnsForChange(coll, "CostCenter_Code", obj.CostCenter_Code, True)
                clsCommon.AddColumnsForChange(coll, "prefix_type", obj.Prefix_Type)
                clsCommon.AddColumnsForChange(coll, "Asset_Specification", obj.Asset_Specification)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Dep_Tax_Rate", obj.Dep_Tax_Rate)

                clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Acqusition_Date", clsCommon.GetPrintDate(obj.Acqusition_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Dep_Method_Code", obj.Dep_Method_Code, True)
                clsCommon.AddColumnsForChange(coll, "Dep_Method_Tax_Code", obj.Dep_Method_Tax_Code, True)
                clsCommon.AddColumnsForChange(coll, "Book_Estimated_Life", obj.Book_Estimated_Life)
                clsCommon.AddColumnsForChange(coll, "Book_Source_value", obj.Book_Source_value)
                clsCommon.AddColumnsForChange(coll, "Book_Source_Original_value", obj.Book_Source_Original_value)
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
                clsCommon.AddColumnsForChange(coll, "Is_Assembled", obj.Is_Assembled)
                clsCommon.AddColumnsForChange(coll, "Book_Dep_Type", IIf(clsCommon.CompairString(obj.Book_Dep_Type, "Formula") = CompairStringResult.Equal, "F", "M"))
                clsCommon.AddColumnsForChange(coll, "Tax_Dep_Type", IIf(clsCommon.CompairString(obj.Tax_Dep_Type, "Formula") = CompairStringResult.Equal, "F", "M"))
                '========
                clsCommon.AddColumnsForChange(coll, "SRNQty", obj.SRNQty)
                clsCommon.AddColumnsForChange(coll, "SRN_Rate", obj.SRN_Rate)
                clsCommon.AddColumnsForChange(coll, "SRN_No", obj.SRN_No, True)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "PI_No", obj.PI_No, True)
                '=======
                If IsNewAsset Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACQUISITION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACQUISITION_DETAIL", OMInsertOrUpdate.Update, "Acquisition_Code='" & obj.Acquisition_Code & "' and Asset_Code='" & obj.Asset_Code & "'", trans)
                End If

                ''===============update old assets
                If obj.arrMerged_AssetCode IsNot Nothing AndAlso obj.arrMerged_AssetCode.Count > 0 Then
                    For Each oldassetcode As clsFAMergeDetail In obj.arrMerged_AssetCode
                        coll = New Hashtable()

                        clsCommon.AddColumnsForChange(coll, "Acquisition_Code", strDocNo)
                        clsCommon.AddColumnsForChange(coll, "Merge_Asset_Code", obj.Asset_Code)
                        clsCommon.AddColumnsForChange(coll, "Old_Asset_Code", oldassetcode.Asset_Code)
                        clsCommon.AddColumnsForChange(coll, "Net_Amt_After_Dep", oldassetcode.Net_Amt_After_Dep)
                        clsCommon.AddColumnsForChange(coll, "Calc_Type", oldassetcode.Calc_Type)

                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACQUISITION_ASSET_MERGE_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                        qry = "update TSPL_ACQUISITION_DETAIL set Asset_Merged=1,Merge_Asset_Code='" + obj.Asset_Code + "' where Asset_Code='" & oldassetcode.Asset_Code & "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    Next
                End If
                ''#------------------------------------------
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

    Public Shared Function UpdateDecpreciationData(ByVal strDocNo As String, ByVal Arr As List(Of clsFAMergeDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            UpdateDecpreciationData(strDocNo, Arr, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function UpdateDecpreciationData(ByVal strDocNo As String, ByVal Arr As List(Of clsFAMergeDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsFAMergeDetail In Arr
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

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetName(ByVal strAssetCode As String) As String
        Dim qry As String = "select Asset_Name from TSPL_ACQUISITION_DETAIL where Asset_Code='" + strAssetCode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Function

End Class

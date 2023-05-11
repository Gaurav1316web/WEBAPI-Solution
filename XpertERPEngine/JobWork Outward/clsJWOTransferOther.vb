Imports common
Imports System.Data.SqlClient
Public Class clsJWOTransferOtherHead
#Region "Variables"
    Public AgainstSRN_No As String = Nothing
    Public TRANSFER_NO As String
    Public TRANSFER_DATE As Date?
    Public From_Locaction As String
    Public To_Locaction As String
    Public Vendor_Code As String
    Public Remarks As String
    Public Status As Integer = 0
    Public Created_By As String
    Public Created_Date As Date?
    Public Modify_By As String
    Public Modify_Date As Date?
    Public Comp_code As String
    Public Vehicle_Code As String = Nothing
    Public Vehicle_No As String = Nothing

    Public Tax_Group As String = Nothing
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public TaxGroupName As String = Nothing 'Not a table field
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Decimal = 0
    Public TAX1_Base_Amt As Decimal = 0
    Public TAX1_Amt As Decimal = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Decimal = 0
    Public TAX2_Base_Amt As Decimal = 0
    Public TAX2_Amt As Decimal = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Decimal = 0
    Public TAX3_Base_Amt As Decimal = 0
    Public TAX3_Amt As Decimal = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Decimal = 0
    Public TAX4_Base_Amt As Decimal = 0
    Public TAX4_Amt As Decimal = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Decimal = 0
    Public TAX5_Base_Amt As Decimal = 0
    Public TAX5_Amt As Decimal = 0
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Decimal = 0
    Public TAX6_Base_Amt As Decimal = 0
    Public TAX6_Amt As Decimal = 0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Decimal = 0
    Public TAX7_Base_Amt As Decimal = 0
    Public TAX7_Amt As Decimal = 0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Decimal = 0
    Public TAX8_Base_Amt As Decimal = 0
    Public TAX8_Amt As Decimal = 0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Decimal = 0
    Public TAX9_Base_Amt As Decimal = 0
    Public TAX9_Amt As Decimal = 0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Decimal = 0
    Public TAX10_Base_Amt As Decimal = 0
    Public TAX10_Amt As Decimal = 0

    Public Total_Amount As Decimal = 0
    Public Total_Tax_Amount As Decimal = 0
    Public Total_Net_Amount As Decimal = 0
    Public Loading_Advice_No As String = Nothing
    Public Entry_Bill_No As String = Nothing


    Public Arr As List(Of clsJWOTransferOtherDetail) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsJWOTransferOtherHead, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As clsJWOTransferOtherHead, ByVal IsNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "JobWork Outward", "JobWork Milk Transfer", obj.From_Locaction, obj.TRANSFER_DATE, trans)
            'clsBatchInventory.DeleteData("JW-TO", obj.TRANSFER_NO, trans)
            'Dim qry As String = "delete from TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS where TRANSFER_NO='" + obj.TRANSFER_NO + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "TRANSFER_DATE", clsCommon.GetPrintDate(obj.TRANSFER_DATE, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "From_Locaction", obj.From_Locaction)
            clsCommon.AddColumnsForChange(coll, "To_Locaction", obj.To_Locaction)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
            clsCommon.AddColumnsForChange(coll, "Vehicle_No", obj.Vehicle_No)
            clsCommon.AddColumnsForChange(coll, "AgainstSRN_No", obj.AgainstSRN_No)


            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
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
            clsCommon.AddColumnsForChange(coll, "Total_Amount", obj.Total_Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amount", obj.Total_Tax_Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Net_Amount", obj.Total_Net_Amount)
            clsCommon.AddColumnsForChange(coll, "Loading_Advice_No", obj.Loading_Advice_No)
            clsCommon.AddColumnsForChange(coll, "Entry_Bill_No", obj.Entry_Bill_No)

            If IsNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                obj.TRANSFER_NO = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.JobWorkTransferOther, "", obj.From_Locaction) 'clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.DOC_DATE), clsDocType.ItemWiseTax, "", "")
                If clsCommon.myLen(obj.TRANSFER_NO) <= 0 Then
                    Throw New Exception("Error in code generation")
                End If
                clsCommon.AddColumnsForChange(coll, "TRANSFER_NO", obj.TRANSFER_NO)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else

                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.TRANSFER_NO), "TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD", "TRANSFER_NO", "TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS", "TRANSFER_NO", trans)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD", OMInsertOrUpdate.Update, "TRANSFER_NO='" & obj.TRANSFER_NO & "'", trans)
            End If
            Dim objtr As New clsJWOTransferOtherDetail
            objtr.SaveData(obj, trans)
            'trans.Commit()

        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal tran As SqlTransaction) As clsJWOTransferOtherHead
        Dim qry As String = "select TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.*,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName from TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD  left outer join  TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Tax_Group  Where 2=2 "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO = (select MIN(TRANSFER_NO) from TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO = (select Max(TRANSFER_NO) from TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO = (select Min(TRANSFER_NO) from TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD where TRANSFER_NO>'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO = (select Max(TRANSFER_NO) from TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD where TRANSFER_NO<'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO = '" + strDocNo + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        Dim obj As clsJWOTransferOtherHead = Nothing
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsJWOTransferOtherHead()
            obj.AgainstSRN_No = clsCommon.myCstr(dt.Rows(0)("AgainstSRN_No"))
            obj.TRANSFER_NO = clsCommon.myCstr(dt.Rows(0)("TRANSFER_NO"))
            obj.TRANSFER_DATE = clsCommon.myCDate(dt.Rows(0)("TRANSFER_DATE"))
            obj.From_Locaction = clsCommon.myCstr(dt.Rows(0)("From_Locaction"))
            obj.To_Locaction = clsCommon.myCstr(dt.Rows(0)("To_Locaction"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
            obj.Created_Date = clsCommon.myCstr(dt.Rows(0)("Created_Date"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Modify_By = clsCommon.myCstr(dt.Rows(0)("Modify_By"))
            obj.Modify_Date = clsCommon.myCDate(dt.Rows(0)("Modify_Date"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.Vehicle_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_No"))

            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("TaxGroupName"))
            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
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
            obj.Total_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Amount"))
            obj.Total_Tax_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amount"))
            obj.Total_Net_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Net_Amount"))
            obj.Loading_Advice_No = clsCommon.myCstr(dt.Rows(0)("Loading_Advice_No"))
            obj.Entry_Bill_No = clsCommon.myCstr(dt.Rows(0)("Entry_Bill_No"))

            qry = " select TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.* from TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS where TRANSFER_NO='" + obj.TRANSFER_NO + "' order by [line_No] "
            dt = clsDBFuncationality.GetDataTable(qry, tran)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsJWOTransferOtherDetail)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsJWOTransferOtherDetail
                    objtr.line_No = clsCommon.myCdbl(dr("line_No"))
                    objtr.TRANSFER_NO = clsCommon.myCstr(dr("TRANSFER_NO"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.UOM = clsCommon.myCstr(dr("UOM"))
                    objtr.Qty = clsCommon.myCstr(dr("Qty"))
                    objtr.Rate = clsCommon.myCstr(dr("Rate"))
                    objtr.Amount = clsCommon.myCstr(dr("Amount"))

                    objtr.TAX1 = clsCommon.myCstr(dr("TAX1"))
                    objtr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                    objtr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                    objtr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                    objtr.TAX2 = clsCommon.myCstr(dr("TAX2"))
                    objtr.TAX2_Base_Amt = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                    objtr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                    objtr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                    objtr.TAX3 = clsCommon.myCstr(dr("TAX3"))
                    objtr.TAX3_Base_Amt = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                    objtr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                    objtr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                    objtr.TAX4 = clsCommon.myCstr(dr("TAX4"))
                    objtr.TAX4_Base_Amt = clsCommon.myCdbl(dr("TAX4_Base_Amt"))
                    objtr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                    objtr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                    objtr.TAX5 = clsCommon.myCstr(dr("TAX5"))
                    objtr.TAX5_Base_Amt = clsCommon.myCdbl(dr("TAX5_Base_Amt"))
                    objtr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                    objtr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                    objtr.TAX6 = clsCommon.myCstr(dr("TAX6"))
                    objtr.TAX6_Base_Amt = clsCommon.myCdbl(dr("TAX6_Base_Amt"))
                    objtr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                    objtr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                    objtr.TAX7 = clsCommon.myCstr(dr("TAX7"))
                    objtr.TAX7_Base_Amt = clsCommon.myCdbl(dr("TAX7_Base_Amt"))
                    objtr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                    objtr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                    objtr.TAX8 = clsCommon.myCstr(dr("TAX8"))
                    objtr.TAX8_Base_Amt = clsCommon.myCdbl(dr("TAX8_Base_Amt"))
                    objtr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                    objtr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                    objtr.TAX9 = clsCommon.myCstr(dr("TAX9"))
                    objtr.TAX9_Base_Amt = clsCommon.myCdbl(dr("TAX9_Base_Amt"))
                    objtr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                    objtr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                    objtr.TAX10 = clsCommon.myCstr(dr("TAX10"))
                    objtr.TAX10_Base_Amt = clsCommon.myCdbl(dr("TAX10_Base_Amt"))
                    objtr.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                    objtr.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
                    objtr.Tax_Amt = clsCommon.myCdbl(dr("Tax_Amt"))
                    objtr.Net_Amt = clsCommon.myCdbl(dr("Net_Amt"))
                    objtr.ItemwiseTaxCode = clsCommon.myCstr(dr("ItemwiseTaxCode"))
                    objtr.arrBatchItem = clsBatchInventory.GetData("JW-TO", objtr.TRANSFER_NO, objtr.Item_Code, objtr.line_No, tran)
                    obj.Arr.Add(objtr)
                Next
            End If
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Transfer No not found to Delete")
        End If
        Dim obj As clsJWOTransferOtherHead = clsJWOTransferOtherHead.GetData(strCode, NavigatorType.Current, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.TRANSFER_NO) > 0) Then
            Try
                clsBatchInventory.DeleteData("JW-TO", strCode, trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD", "TRANSFER_NO", "TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS", "TRANSFER_NO", trans)
                Dim qry As String = "delete from TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS where TRANSFER_NO='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD where TRANSFER_NO='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function
    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Transaction Number not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsJWOTransferOtherHead = clsJWOTransferOtherHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.TRANSFER_NO) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Posted on :" & obj.Modify_Date & " ")
            End If
            HitInventory(trans, obj, strPostDate)
            CreateJournalEntry(obj, trans, "")
            Dim qry As String = "Update TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD set Status=1, Post_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where TRANSFER_NO='" + strDocNo + "' "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strDocNo), "TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD", "TRANSFER_NO", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Shared Function HitInventory(ByVal trans As SqlTransaction, ByVal obj As clsJWOTransferOtherHead, ByVal strTranferDate As String) As Boolean
        Dim ArrInventoryMovementOut As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim ArrInventoryMovementIn As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim addcost As Decimal = 0.0
        Dim PunchingTime As DateTime = obj.TRANSFER_DATE
        For Each objTr As clsJWOTransferOtherDetail In obj.Arr
            Dim basicost As Decimal = objTr.Rate
            Dim itemcost As Decimal = objTr.Amount
            Dim reccost As Decimal = 0.0
            Dim netcost As Decimal = clsCommon.myCdbl(itemcost)
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

            Dim objInventoryMovemnt As New clsInventoryMovement()
            objInventoryMovemnt.InOut = "O"
            objInventoryMovemnt.Location_Code = obj.From_Locaction

            objInventoryMovemnt.Item_Code = objTr.Item_Code
            objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(objTr.Item_Code, trans)
            objInventoryMovemnt.Qty = objTr.Qty
            objInventoryMovemnt.UOM = objTr.UOM
            objInventoryMovemnt.MRP = 0
            objInventoryMovemnt.Add_Cost = addcost
            objInventoryMovemnt.Net_Cost = netcost
            objInventoryMovemnt.ItemType = strItemTypeToSave
            objInventoryMovemnt.Basic_Cost = basicost
            objInventoryMovemnt.Rec_Cost = reccost
            objInventoryMovemnt.Punching_Date = PunchingTime
            ArrInventoryMovementOut.Add(objInventoryMovemnt)

            Dim objInventoryMovemnt1 As New clsInventoryMovement()
            objInventoryMovemnt1.InOut = "I"
            objInventoryMovemnt1.Location_Code = obj.To_Locaction

            objInventoryMovemnt1.Item_Code = objTr.Item_Code
            objInventoryMovemnt1.Item_Desc = clsItemMaster.GetItemName(objTr.Item_Code, trans)
            objInventoryMovemnt1.Qty = objTr.Qty
            objInventoryMovemnt1.UOM = objTr.UOM
            objInventoryMovemnt1.MRP = 0
            objInventoryMovemnt1.Add_Cost = addcost
            objInventoryMovemnt1.Net_Cost = netcost
            objInventoryMovemnt1.ItemType = strItemTypeToSave
            objInventoryMovemnt1.Basic_Cost = basicost
            objInventoryMovemnt1.Rec_Cost = 0
            objInventoryMovemnt1.Punching_Date = PunchingTime
            ArrInventoryMovementIn.Add(objInventoryMovemnt1)

        Next
        clsInventoryMovement.SaveData("JW-TO", obj.TRANSFER_NO, PunchingTime, clsCommon.GetPrintDate(PunchingTime, "dd/MM/yyyy"), ArrInventoryMovementOut, trans)
        clsInventoryMovement.SaveData("JW-TO", obj.TRANSFER_NO, PunchingTime, clsCommon.GetPrintDate(PunchingTime, "dd/MM/yyyy"), ArrInventoryMovementIn, trans)
        Return True
    End Function
    Public Shared Function CreateJournalEntry(obj As clsJWOTransferOtherHead, trans As SqlTransaction, strVoucherNoForRecreateOnly As String) As Boolean
        Try
            If clsCommon.myLen(obj.AgainstSRN_No) <= 0 Then
                Dim settJobWorkOutwardComsumeItemAccordingToBOM As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.JobWorkOutwardComsumeItemAccordingToBOM, clsFixedParameterCode.JobWorkOutwardComsumeItemAccordingToBOM, trans)) = 1)
                Dim FromLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & obj.From_Locaction & "'", trans))
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    Dim ArryLst As ArrayList = New ArrayList()
                    For i As Integer = 0 To obj.Arr.Count - 1
                        Dim dtAccount As DataTable = clsDBFuncationality.GetDataTable("select Purchase_Class_Code, Purchase_JobWork,Stock_Transfer_JobWork,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.RM_Consumption from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & obj.Arr(i).Item_Code & "') ", trans)
                        If dtAccount Is Nothing OrElse dtAccount.Rows.Count <= 0 Then
                            Throw New Exception("Please Map Purchase Account Set vendor " + obj.Vendor_Code)
                        End If

                        Dim DrAcc As String = clsCommon.myCstr(dtAccount.Rows(0)("Purchase_JobWork"))
                        If settJobWorkOutwardComsumeItemAccordingToBOM Then
                            If clsCommon.myLen(DrAcc) <= 0 Then
                                Throw New Exception("Please Map  Purchase Job Work A/C From Purchase Account Set [" + clsCommon.myCstr(dtAccount.Rows(0)("Purchase_Class_Code")) + "] For Item : " & obj.Arr(i).Item_Code & " (" & clsItemMaster.GetItemName(obj.Arr(i).Item_Code, trans) & ")")
                            End If
                        Else
                            DrAcc = clsCommon.myCstr(dtAccount.Rows(0)("RM_Consumption"))
                            If clsCommon.myLen(DrAcc) <= 0 Then
                                Throw New Exception("Please Map  Consumption A/C From Purchase Account Set [" + clsCommon.myCstr(dtAccount.Rows(0)("Purchase_Class_Code")) + "]  For Item : " & obj.Arr(i).Item_Code & " (" & clsItemMaster.GetItemName(obj.Arr(i).Item_Code, trans) & ")")
                            End If
                        End If
                        DrAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(DrAcc, FromLocationSegment, True, trans)
                        ArryLst.Add(New String() {DrAcc, obj.Arr(i).Amount})

                        Dim CrAmt As String = clsCommon.myCstr(dtAccount.Rows(0)("Stock_Transfer_JobWork"))
                        If settJobWorkOutwardComsumeItemAccordingToBOM Then
                            If clsCommon.myLen(CrAmt) <= 0 Then
                                Throw New Exception("Please Map Stock Transfer job work A/C From Purchase Account Set [" + clsCommon.myCstr(dtAccount.Rows(0)("Purchase_Class_Code")) + "]  For Item : " & obj.Arr(i).Item_Code & " (" & clsItemMaster.GetItemName(obj.Arr(i).Item_Code, trans) & ")")
                            End If
                        Else
                            CrAmt = clsCommon.myCstr(dtAccount.Rows(0)("Inv_Control_Account"))
                            If clsCommon.myLen(CrAmt) <= 0 Then
                                Throw New Exception("Please Map Inventory control A/C From Purchase Account Set [" + clsCommon.myCstr(dtAccount.Rows(0)("Purchase_Class_Code")) + "]  For Item : " & obj.Arr(i).Item_Code & " (" & clsItemMaster.GetItemName(obj.Arr(i).Item_Code, trans) & ")")
                            End If
                        End If
                        CrAmt = clsERPFuncationality.ChangeGLAccountLocationSegment(CrAmt, FromLocationSegment, True, trans)
                        ArryLst.Add(New String() {CrAmt, obj.Arr(i).Amount * -1})

                    Next
                    Dim GLDesc As String = "Journal Entry Against Job Work Transfer-Other - Doc No." & obj.TRANSFER_NO & " "
                    Dim Remarks As String = "Journal Entry against Job Work Transfer-Other from location -" & obj.From_Locaction & " to location- " & obj.To_Locaction & " For Doc No. " & obj.TRANSFER_NO & ""

                    If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                        transportSql.FunGrnlEntryWithTrans(obj.From_Locaction, False, strVoucherNoForRecreateOnly, trans, obj.TRANSFER_DATE, GLDesc, "JW-TF", "JWO Transfer", obj.TRANSFER_NO, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, obj.Remarks)
                    Else
                        transportSql.FunGrnlEntryWithTrans(obj.From_Locaction, False, trans, obj.TRANSFER_DATE, GLDesc, "JW-TF", "JWO Transfer", obj.TRANSFER_NO, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, obj.Remarks)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If
            Dim obj As clsJWOTransferOtherHead = clsJWOTransferOtherHead.GetData(strCode, NavigatorType.Current, trans)

            If obj Is Nothing OrElse clsCommon.myLen(obj.TRANSFER_NO) <= 0 Then
                Throw New Exception("Document no not found")
            End If

            If Not obj.Status = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            Dim Qry As String = ""
            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='JW-TF' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If


            clsBatchInventory.ReverseAndUnpost("JW-TO", strCode, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_SERIAL_ITEM", "Document_Code", trans)
            Qry = " delete from TSPL_SERIAL_ITEM where Document_Code ='" + strCode + "' and Document_Type ='JW-TO'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_INVENTORY_MOVEMENT", "Source_Doc_No", trans)
            Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type='JW-TO'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_INVENTORY_MOVEMENT_New", "Source_Doc_No", trans)
            Qry = "delete from TSPL_INVENTORY_MOVEMENT_New where Source_Doc_No='" + strCode + "' and Trans_Type='JW-TO'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD set Status = 0 where TRANSFER_NO='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD", "TRANSFER_NO", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsJWOTransferOtherDetail
#Region "Variables"
    Public TRANSFER_NO As String
    Public Item_Code As String
    Public UOM As String
    Public Qty As Decimal
    Public line_No As Integer
    Public Rate As Decimal
    Public Amount As Decimal

    Public TAX1 As String = Nothing
    Public TAX1_Base_Amt As Decimal = 0
    Public TAX1_Rate As Decimal = 0
    Public TAX1_Amt As Decimal = 0
    Public TAX2 As String = Nothing
    Public TAX2_Base_Amt As Decimal = 0
    Public TAX2_Rate As Decimal = 0
    Public TAX2_Amt As Decimal = 0
    Public TAX3 As String = Nothing
    Public TAX3_Base_Amt As Decimal = 0
    Public TAX3_Rate As Decimal = 0
    Public TAX3_Amt As Decimal = 0
    Public TAX4 As String = Nothing
    Public TAX4_Base_Amt As Decimal = 0
    Public TAX4_Rate As Decimal = 0
    Public TAX4_Amt As Decimal = 0
    Public TAX5 As String = Nothing
    Public TAX5_Base_Amt As Decimal = 0
    Public TAX5_Rate As Decimal = 0
    Public TAX5_Amt As Decimal = 0
    Public TAX6 As String = Nothing
    Public TAX6_Base_Amt As Decimal = 0
    Public TAX6_Rate As Decimal = 0
    Public TAX6_Amt As Decimal = 0
    Public TAX7 As String = Nothing
    Public TAX7_Base_Amt As Decimal = 0
    Public TAX7_Rate As Decimal = 0
    Public TAX7_Amt As Decimal = 0
    Public TAX8 As String = Nothing
    Public TAX8_Base_Amt As Decimal = 0
    Public TAX8_Rate As Decimal = 0
    Public TAX8_Amt As Decimal = 0
    Public TAX9 As String = Nothing
    Public TAX9_Base_Amt As Decimal = 0
    Public TAX9_Rate As Decimal = 0
    Public TAX9_Amt As Decimal = 0
    Public TAX10 As String = Nothing
    Public TAX10_Base_Amt As Decimal = 0
    Public TAX10_Rate As Decimal = 0
    Public TAX10_Amt As Decimal = 0
    Public Tax_Amt As Decimal = 0
    Public Net_Amt As Decimal = 0
    Public ItemwiseTaxCode As String = ""
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsJWOTransferOtherHead, ByVal trans As SqlTransaction) As Boolean
        Try
            clsBatchInventory.DeleteData("JW-TO", obj.TRANSFER_NO, trans)
            Dim qry As String = "delete from TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS where TRANSFER_NO='" + obj.TRANSFER_NO + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim ii As Integer = 0
            For Each objtr As clsJWOTransferOtherDetail In obj.Arr
                ii += 1
                objtr.line_No = ii
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "line_No", objtr.line_No)
                clsCommon.AddColumnsForChange(coll, "TRANSFER_NO", obj.TRANSFER_NO)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", objtr.UOM)
                clsCommon.AddColumnsForChange(coll, "Qty", objtr.Qty)
                clsCommon.AddColumnsForChange(coll, "Rate", objtr.Rate)
                clsCommon.AddColumnsForChange(coll, "Amount", objtr.Amount)


                clsCommon.AddColumnsForChange(coll, "TAX1", objtr.TAX1)
                clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", objtr.TAX1_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX1_Rate", objtr.TAX1_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX1_Amt", objtr.TAX1_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2", objtr.TAX2)
                clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", objtr.TAX2_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2_Rate", objtr.TAX2_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX2_Amt", objtr.TAX2_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3", objtr.TAX3)
                clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", objtr.TAX3_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3_Rate", objtr.TAX3_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX3_Amt", objtr.TAX3_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4", objtr.TAX4)
                clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", objtr.TAX4_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4_Rate", objtr.TAX4_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX4_Amt", objtr.TAX4_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5", objtr.TAX5)
                clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", objtr.TAX5_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5_Rate", objtr.TAX5_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX5_Amt", objtr.TAX5_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6", objtr.TAX6)
                clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", objtr.TAX6_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6_Rate", objtr.TAX6_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX6_Amt", objtr.TAX6_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7", objtr.TAX7)
                clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", objtr.TAX7_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7_Rate", objtr.TAX7_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX7_Amt", objtr.TAX7_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8", objtr.TAX8)
                clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", objtr.TAX8_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8_Rate", objtr.TAX8_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX8_Amt", objtr.TAX8_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9", objtr.TAX9)
                clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", objtr.TAX9_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9_Rate", objtr.TAX9_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX9_Amt", objtr.TAX9_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10", objtr.TAX10)
                clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", objtr.TAX10_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10_Rate", objtr.TAX10_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX10_Amt", objtr.TAX10_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax_Amt", objtr.Tax_Amt)
                clsCommon.AddColumnsForChange(coll, "Net_Amt", objtr.Net_Amt)
                clsCommon.AddColumnsForChange(coll, "ItemwiseTaxCode", objtr.ItemwiseTaxCode)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS", OMInsertOrUpdate.Insert, "", trans)

                clsBatchInventory.SaveData("JW-TO", obj.TRANSFER_NO, obj.TRANSFER_DATE, "O", objtr.Item_Code, obj.From_Locaction, objtr.line_No, 0, objtr.UOM, objtr.arrBatchItem, trans)
                clsBatchInventory.SaveData("JW-TO", obj.TRANSFER_NO, obj.TRANSFER_DATE, "I", objtr.Item_Code, obj.To_Locaction, objtr.line_No, 0, objtr.UOM, objtr.arrBatchItem, trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class



Imports System.Data.SqlClient
Imports common
Public Class clsGetKeys
    Public Shared Function GetUniqueKeyName(ByVal TableName As String, ByVal ArrColumns As ArrayList) As String
        Dim UKName As String = ""
        Try
            If ArrColumns Is Nothing OrElse ArrColumns.Count <= 0 Then
                Throw New Exception("Please provide Column ")
            End If
            Dim sQuery = "select unique_key_name from (
SELECT k.name AS unique_key_name
FROM sys.key_constraints k
INNER JOIN sys.index_columns ic ON k.parent_object_id = ic.object_id AND k.unique_index_id = ic.index_id
INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
WHERE k.type = 'UQ' 
AND k.parent_object_id = OBJECT_ID('" + TableName + "') 
AND c.name IN (" + clsCommon.GetMulcallString(ArrColumns) + ")
)xx group by unique_key_name "
            UKName = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sQuery))
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
        Return UKName
    End Function

    Public Shared Function GetForeignKeyName(ByVal strTableName As String, ByVal strColumnsName As String, ByVal tran As SqlTransaction) As String
        Dim FKName As String = ""
        Try
            Dim sQuery = " SELECT  A.CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS A, INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE B WHERE CONSTRAINT_TYPE = 'FOREIGN KEY' AND A.CONSTRAINT_NAME = B.CONSTRAINT_NAME and a.TABLE_NAME='" + strTableName + "' and b.COLUMN_NAME='" + strColumnsName + "' ORDER BY A.TABLE_NAME"
            FKName = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sQuery, tran))
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
        Return FKName
    End Function
End Class

Public Class clsGridColumn
    Public Const colDOCNo As String = "colDOCNo"

    Public Const colSelect As String = "colSelect"
    Public Const colSlno As String = "colSlno"
    Public Const colPurchaseInvoiceNo As String = "colPurchaseInvoiceNo"
    Public Const colPurchaseInvoiceDate As String = "colPurchaseInvoiceDate"
    Public Const colAPInvoiceNo As String = "colAPInvoiceNo"
    Public Const colAPInvoiceDate As String = "colAPInvoiceDate"
    Public Const colVLCCode As String = "colVLCCode"
    Public Const colVLCName As String = "colVLCName"
    Public Const colVLCUploaderCode As String = "colVLCUploaderCode"
    Public Const colMCCCode As String = "colMCCCode"
    Public Const colVendorCode As String = "colVendorCode"
    Public Const colVendorDesc As String = "colVendorDesc"
    Public Const colActualVSPCode As String = "colActualVSPCode"
    Public Const colActualVSPName As String = "colActualVSPName"
    Public Const colPayeeJointName As String = "colPayeeJointName"
    Public Const colPayeeJointBankCode As String = "colPayeeJointBankCode"
    Public Const colPayeeJointBankDesc As String = "colPayeeJointBankDesc"
    Public Const colPayeeJointAcNo As String = "colPayeeJointAcNo"
    Public Const colPayeeJointIFSC As String = "colPayeeJointIFSC"
    Public Const colPayeeJointBranchCode As String = "colPayeeJointBranchCode"
    Public Const colPayeeJointBranchDesc As String = "colPayeeJointBranchDesc"
    Public Const colMilkQty As String = "colMilkQty"
    Public Const colFATPer As String = "colFATPer"
    Public Const colFATKG As String = "colFATKG"
    Public Const colSNFPer As String = "colSNFPer"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colInvAmt As String = "colInvAmt"
    Public Const colEmpAmt As String = "colEmpAmt"
    Public Const colInvAndEmpAmt As String = "colInvAndEmpAmt"
    Public Const colSRNROAmt As String = "colSRNROAmt"
    Public Const colSRNNetAmount As String = "colSRNNetAmount"
    Public Const colHandlingCharges As String = "colHandlingCharges"
    Public Const colIncenAmt As String = "colIncenAmt"
    Public Const colIncenEmpAmt As String = "colIncenEmpAmt"
    Public Const colServiceChargeAmt As String = "colServiceChargeAmt"
    Public Const colTDSAmt As String = "colTDSAmt"
    Public Const colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt As String = "colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt"
    Public Const colVSPOwnSystemAmt As String = "colVSPOwnSystemAmt"
    Public Const colVSPOwnSystemDocNo As String = "colVSPOwnSystemDocNo"
    Public Const colHeadLoadDocNo As String = "colHeadLoadDocNo"
    Public Const colHeadLoadAmt As String = "colHeadLoadAmt"
    Public Const colInvDedDocNo As String = "colInvDedDocNo"
    Public Const colInvDeduc As String = "colInvDeduc"
    Public Const colReduceDeduc As String = "colReduceDeduc"
    Public Const colBankCode As String = "colBankCode"
    Public Const colBankDesc As String = "colBankDesc"
    Public Const colPayMode As String = "colPayMode"
    Public Const colChequeNo As String = "colChequeNo"
    Public Const colVSPAmount As String = "colVSPAmount"
    Public Const colMPAmount As String = "colMPAmount"
    Public Const colMPEMPAmount As String = "colMPEMPAmount"
    Public Const colMPIncentiveAmount As String = "colMPIncentiveAmount"
    Public Const colMPEMPIncentiveAmount As String = "colMPEMPIncentiveAmount"
    Public Const colMPNetAmount As String = "colMPNetAmount"



    Public Const colIsPaymentProcessHold As String = "colIsPaymentProcessHold"
    Public Const colMPVSPDiffAmount As String = "colMPVSPDiffAmount"
    Public Const colTotalEmp As String = "colTotalEmp"
    Public Const colMccSaleTotalAmount As String = "colMccSaleTotalAmount"
    Public Const colMccSaleReturnTotalAmount As String = "colMccSaleReturnTotalAmount"
    Public Const colItemIssueTotalAmount As String = "colItemIssueTotalAmount"
    Public Const colItemIssueReturnTotalAmount As String = "colItemIssueReturnTotalAmount"
    Public Const colDeductionTotalAmount As String = "colDeductionTotalAmount"
    Public Const colAssetLostAmount As String = "colAssetLostAmount"
    Public Const colAdvanceAmount As String = "colAdvanceAmount"

    Public Const colAdvanceKnockOffAmount As String = "colAdvanceKnockOffAmount"
    Public Const colTotalCreditNoteAmount As String = "colTotalCreditNoteAmount"
    Public Const colTotalCompulsoryAmount As String = "colTotalCompulsoryAmount"
    Public Const colPaybleAmt As String = "colPaybleAmt"
    Public Const colChequeDate As String = "colChequeDate"
End Class

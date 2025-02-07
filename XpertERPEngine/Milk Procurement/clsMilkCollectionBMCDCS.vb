Imports common
Imports System.Data.SqlClient

Public Class clsMilkCollectionBMCDCS

#Region "Variables"
    Public Document_ID As String
    Public IDate As DateTime
    Public MCC_Code As String
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posting_Date As DateTime? = Nothing
#End Region

    Public Shared Function PostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsMilkCollectionBMCDCS = clsMilkCollectionBMCDCS.GetData(strCode, NavigatorType.Current, trans)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MILK_COLLECTION_BMCDCS.IDate,TSPL_MILK_COLLECTION_BMCDCS.MCC_Code from TSPL_MILK_COLLECTION_BMCDCS where TSPL_MILK_COLLECTION_BMCDCS.PK_ID='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.MilkCollectionMCC, clsCommon.myCstr(dt.Rows(0)("Mcc_Code")), clsCommon.myCDate(dt.Rows(0)("IDate")), trans)
            End If

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_ID) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If

            'HistoryUpdate(strCode, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_BMCDCS", OMInsertOrUpdate.Update, "PK_ID='" + obj.Document_ID + "'", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkCollectionBMCDCS
        Return GetData(strPONo, NavType, trans, "")
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal strDetailWhrlCls As String) As clsMilkCollectionBMCDCS
        Dim obj As clsMilkCollectionBMCDCS = Nothing
        Dim qry As String = "select CAST(TSPL_MILK_COLLECTION_BMCDCS.Status as BIT) as Status, TSPL_MILK_COLLECTION_BMCDCS.PK_ID,
TSPL_MILK_COLLECTION_BMCDCS.IDate, TSPL_MILK_COLLECTION_BMCDCS.MCC_Code, TSPL_MILK_COLLECTION_BMCDCS.Posted_Date FROM TSPL_MILK_COLLECTION_BMCDCS 
where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MILK_COLLECTION_BMCDCS.PK_ID = (select MIN(PK_ID) from TSPL_MILK_COLLECTION_BMCDCS where 1=1)"
            Case NavigatorType.Last
                qry += " and TSPL_MILK_COLLECTION_BMCDCS.PK_ID = (select Max(PK_ID) from TSPL_MILK_COLLECTION_BMCDCS where 1=1)"
            Case NavigatorType.Next
                qry += " and TSPL_MILK_COLLECTION_BMCDCS.PK_ID = (select Min(PK_ID) from TSPL_MILK_COLLECTION_BMCDCS where PK_ID>'" + strPONo + "'  )"
            Case NavigatorType.Previous
                qry += " and TSPL_MILK_COLLECTION_BMCDCS.PK_ID = (select Max(PK_ID) from TSPL_MILK_COLLECTION_BMCDCS where PK_ID<'" + strPONo + "'  )"
            Case NavigatorType.Current
                qry += " and TSPL_MILK_COLLECTION_BMCDCS.PK_ID = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMilkCollectionBMCDCS
            obj.Document_ID = clsCommon.myCstr(dt.Rows(0)("PK_ID"))
            obj.IDate = clsCommon.GetPrintDate(dt.Rows(0)("IDate"), "dd/MMM/yyyy")
            obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
            obj.Status = IIf(clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posted_Date"))
            End If

            'obj.Arr = clsMilkCollectionMCCDetail.GetData(obj.Document_No, strDetailWhrlCls, trans)
        End If
        Return obj
    End Function

End Class

Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class clsBankOpeningReco

#Region "Variables"
    Public Code As String = Nothing
    Public Reco_Date As DateTime
    Public Type As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Cust_Code As String = Nothing
    Public Cust_Name As String = Nothing
    Public Cheque_No As String = Nothing
    Public Cheque_Date As DateTime
    Public Amt As String = Nothing
    Public Description As String = Nothing
    Public Bank_Code As String = Nothing
    Public Bank_Name As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = " select  TSPL_BANK_OPENING_RECO.Code,TSPL_BANK_OPENING_RECO.Reco_Date as RecoDate,TSPL_BANK_OPENING_RECO.Type,TSPL_BANK_OPENING_RECO.Vendor_Code as VendorCode,TSPL_VENDOR_MASTER.Vendor_Name as VendorName,TSPL_BANK_OPENING_RECO.Cust_Code as CustomerCode,TSPL_CUSTOMER_MASTER.Customer_Name as CustomerName,TSPL_BANK_OPENING_RECO.Bank_Code,TSPL_BANK_MASTER.DESCRIPTION as BankName,TSPL_BANK_OPENING_RECO.Cheque_No as ChequeNo,TSPL_BANK_OPENING_RECO.Cheque_Date as ChequeDate,TSPL_BANK_OPENING_RECO.Amt as Amount,TSPL_BANK_OPENING_RECO.Description From TSPL_BANK_OPENING_RECO left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_BANK_OPENING_RECO.Vendor_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BANK_OPENING_RECO.Cust_Code left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_OPENING_RECO.Bank_Code "
        Return clsCommon.ShowSelectForm("BankOpenRecoFnd", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBankOpeningReco
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_BANK_OPENING_RECO where Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function PostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strCode, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String = "Update TSPL_BANK_OPENING_RECO set Status=1,Post_By='" + objCommonVar.CurrentUserCode + "',Post_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' where Code ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsBankOpeningReco
        Dim obj As clsBankOpeningReco = Nothing
        Dim qry As String = "select TSPL_BANK_OPENING_RECO.*,TSPL_VENDOR_MASTER.Vendor_Name  ,TSPL_CUSTOMER_MASTER.Customer_Name as Cust_Name,TSPL_BANK_MASTER.DESCRIPTION as Bank_Name  from TSPL_BANK_OPENING_RECO left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_BANK_OPENING_RECO.Vendor_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BANK_OPENING_RECO.Cust_Code left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.Bank_Code=TSPL_BANK_OPENING_RECO.Bank_Code where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Code = (select MIN(Code) from TSPL_BANK_OPENING_RECO)"
            Case NavigatorType.Last
                qry += " and Code = (select Max(Code) from TSPL_BANK_OPENING_RECO)"
            Case NavigatorType.Next
                qry += " and Code = (select Min(Code) from TSPL_BANK_OPENING_RECO where  Code>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and Code = (select Max(Code) from TSPL_BANK_OPENING_RECO where Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsBankOpeningReco()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Reco_Date = clsCommon.myCDate(dt.Rows(0)("Reco_Date"))
            obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Cust_Name = clsCommon.myCstr(dt.Rows(0)("Cust_Name"))
            obj.Cheque_No = clsCommon.myCstr(dt.Rows(0)("Cheque_No"))
            obj.Cheque_Date = clsCommon.myCDate(dt.Rows(0)("Cheque_Date"))
            obj.Amt = clsCommon.myCdbl(dt.Rows(0)("Amt"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
            obj.Bank_Name = clsCommon.myCstr(dt.Rows(0)("Bank_Name"))
        End If
        Return obj
    End Function

    Public Shared Function SaveData(ByVal obj As clsBankOpeningReco, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
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

    Public Shared Function SaveData(ByVal obj As clsBankOpeningReco, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Reco_Date", clsCommon.GetPrintDate(obj.Reco_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code, True)
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code, True)
            clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code)
            clsCommon.AddColumnsForChange(coll, "Cheque_No", obj.Cheque_No)
            clsCommon.AddColumnsForChange(coll, "Cheque_Date", clsCommon.GetPrintDate(obj.Cheque_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Amt", obj.Amt)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select TSPL_GL_ACCOUNTS.Account_Seg_Code7 from TSPL_BANK_MASTER LEFT OUTER JOIN TSPL_GL_ACCOUNTS ON TSPL_GL_ACCOUNTS.Account_Code=TSPL_BANK_MASTER.BANKACC Where BANK_CODE='" + obj.Bank_Code + "'", trans)
                ''richa agarwal 16 Jan,2020 pass location segement as true
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.Reco_Date, clsDocType.OpeningBankReco, "", LocSegmentCode, True)
                If clsCommon.myLen(obj.Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_OPENING_RECO", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_OPENING_RECO", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function


End Class


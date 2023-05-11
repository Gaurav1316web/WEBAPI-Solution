Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsLockMPPaymentCycle

#Region "Variables"
    Public LOCK_CODE As String    
    Public MCC_Code As String
    Public FROM_DATE As Date
    Public TO_DATE As Date
    Public Description As String
    Public POSTED As String

#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_LOCK_MP_PC.LOCK_CODE as [Code] ,TSPL_LOCK_MP_PC.MCC_Code as [MCC Code] ,TSPL_LOCK_MP_PC.FROM_DATE as [Date From] ,TSPL_LOCK_MP_PC.TO_DATE as [Date To] ,TSPL_LOCK_MP_PC.DESCRIPTION as [Description] ,TSPL_LOCK_MP_PC.POSTED as [Posted],TSPL_LOCK_MP_PC.Created_By as [Created By] ,TSPL_LOCK_MP_PC.Created_Date as [Created Date] ,TSPL_LOCK_MP_PC.Modified_By as [Modified By] ,TSPL_LOCK_MP_PC.Modified_Date as [Modified Date]  From TSPL_LOCK_MP_PC  "
        str = clsCommon.ShowSelectForm("PAYPRDMST", qry, "Code", whrcls, curcode, "FROM_DATE", isButtonClicked)
        Return str
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsLockMPPaymentCycle
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select MCC_Code,FROM_DATE from TSPL_LOCK_MP_PC where LOCK_CODE='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFarmerPayment, clsUserMgtCode.frmLockMPCollectionPC, clsCommon.myCstr(dt.Rows(0)("MCC_Code")), clsCommon.myCDate(dt.Rows(0)("FROM_DATE")), trans)

            End If

            Dim qry As String
            qry = "delete from TSPL_LOCK_MP_PC where LOCK_CODE ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '' LOG FOR SYNC DATA
            clsSyncHeadTables.SaveSyncDelete("TSPL_LOCK_MP_PC", strCode, trans)
            trans.Commit() ''ERO/30/06/21-001422 by balwinder on 30/06/2021
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsLockMPPaymentCycle
        Dim obj As clsLockMPPaymentCycle = Nothing
        Dim qry As String = "select LOCK_CODE, MCC_Code, FROM_DATE, TO_DATE, DESCRIPTION,POSTED  from TSPL_LOCK_MP_PC where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and LOCK_CODE = (select MIN(LOCK_CODE) from TSPL_LOCK_MP_PC)"
            Case NavigatorType.Last
                qry += " and LOCK_CODE = (select Max(LOCK_CODE) from TSPL_LOCK_MP_PC)"
            Case NavigatorType.Next
                qry += " and LOCK_CODE = (select Min(LOCK_CODE) from TSPL_LOCK_MP_PC where  LOCK_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and LOCK_CODE = (select Max(LOCK_CODE) from TSPL_LOCK_MP_PC where LOCK_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and LOCK_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsLockMPPaymentCycle()
            obj.LOCK_CODE = clsCommon.myCstr(dt.Rows(0)("LOCK_CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
            obj.FROM_DATE = clsCommon.myCDate(dt.Rows(0)("FROM_DATE"))
            obj.TO_DATE = clsCommon.myCDate(dt.Rows(0)("TO_DATE"))
            obj.POSTED = clsCommon.myCstr(dt.Rows(0)("POSTED"))

        End If
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsLockMPPaymentCycle, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsLockMPPaymentCycle, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFarmerPayment, clsUserMgtCode.frmLockMPCollectionPC, obj.MCC_Code, obj.FROM_DATE, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code)
            clsCommon.AddColumnsForChange(coll, "POSTED", obj.POSTED)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.Description)
            clsCommon.AddColumnsForChange(coll, "FROM_DATE", clsCommon.GetPrintDate(obj.FROM_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "TO_DATE", clsCommon.GetPrintDate(obj.TO_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            '' update Sync Satatus
            clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", 0)
            If isNewEntry Then
                Dim qry As String = "select max(LOCK_CODE) as LOCK_CODE from TSPL_LOCK_MP_PC where MCC_Code='" + obj.MCC_Code + "' "
                obj.LOCK_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(obj.LOCK_CODE) <= 0 Then
                    obj.LOCK_CODE = "" & obj.MCC_Code & "" & "/LC000001"
                Else
                    obj.LOCK_CODE = clsCommon.incval(obj.LOCK_CODE)
                End If

                clsCommon.AddColumnsForChange(coll, "LOCK_CODE", obj.LOCK_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                qry = "SELECT Count(*) FROM TSPL_LOCK_MP_PC where LOCK_CODE= '" & obj.LOCK_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOCK_MP_PC", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOCK_MP_PC", OMInsertOrUpdate.Update, "LOCK_CODE='" + obj.LOCK_CODE + "'", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsLockMPPaymentCycle = clsLockMPPaymentCycle.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.LOCK_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFarmerPayment, clsUserMgtCode.frmLockMPCollectionPC, obj.MCC_Code, obj.FROM_DATE, trans)

            If (isCheckForPosted AndAlso obj.POSTED = "Y") Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = "Update TSPL_LOCK_MP_PC set POSTED='Y', MODIFIED_DATE='" & strPostDate & "',Modified_By='" + objCommonVar.CurrentUserCode + "' where LOCK_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetName(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select MCC_Code from TSPL_LOCK_MP_PC where LOCK_CODE ='" + strCode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function CheckNameExistness(ByVal strName As String, ByVal strExCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select LOCK_CODE from TSPL_LOCK_MP_PC where MCC_Code ='" + strName + "'  and LOCK_CODE <> '" + strExCode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function GetLastDay(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select DAY(TO_DATE) as 'Day' from TSPL_LOCK_MP_PC where LOCK_CODE ='" + strCode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function GetToDate(ByVal strCode As String, ByVal trans As SqlTransaction) As Date
        Dim qry As String = "select TO_DATE  from TSPL_LOCK_MP_PC where LOCK_CODE ='" + strCode + "' "
        Return clsCommon.myCDate(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function GetFromDate(ByVal strCode As String, ByVal trans As SqlTransaction) As Date
        Dim qry As String = "select FROM_DATE  from TSPL_LOCK_MP_PC where LOCK_CODE ='" + strCode + "' "
        Return clsCommon.myCDate(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function CheckNewPayPeriod(ByVal PayPeriodCode As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select LOCK_CODE from TSPL_LOCK_MP_PC where LOCK_CODE ='" + PayPeriodCode + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Shared Function GetPreviousPayPeriod(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "SELECT TOP 1 LOCK_CODE FROM TSPL_LOCK_MP_PC WHERE FROM_DATE< " & _
            " (SELECT FROM_DATE FROM TSPL_LOCK_MP_PC WHERE LOCK_CODE='" & strCode & "') ORDER BY FROM_DATE DESC"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function LockMPTransaction(ByVal Location As String, ByVal docDate As Date) As Boolean
        Return LockMPTransaction(Location, docDate, Nothing)
    End Function
    Public Shared Function LockMPTransaction(ByVal Location As String, ByVal docDate As Date, ByVal trans As SqlClient.SqlTransaction) As Boolean

        Dim ChkDateMPLock As String = clsDBFuncationality.getSingleValue("select TSPL_LOCK_MP_PC.LOCK_CODE  from TSPL_LOCK_MP_PC where  '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(docDate), "dd/MMM/yyyy") + "'  between Cast(FROM_DATE as Date)   and Cast(TO_DATE as Date) and MCC_Code ='" & Location & "'And POSTED = 'Y'", trans)
        If clsCommon.myLen(ChkDateMPLock) > 0 Then
            Throw New Exception("Transaction is Lock for  date " + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(docDate), "dd/MMM/yyyy") + " (" + ChkDateMPLock + ")")
        End If
        Return True
    End Function

    Public Shared Function ReverseAndUnpostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Document No not found to unpost")
            End If

            Dim obj As clsLockMPPaymentCycle = clsLockMPPaymentCycle.GetData(strCode, NavigatorType.Current, trans)
            If Not clsCommon.CompairString(obj.POSTED, "Y") = CompairStringResult.Equal Then
                Throw New Exception("Transaction [" + strCode + "] should be Posted")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFarmerPayment, clsUserMgtCode.frmLockMPCollectionPC, obj.MCC_Code, obj.FROM_DATE, trans)

            Dim qry As String = "select DOC_CODE from TSPL_MILK_PURCHASE_INVOICE_HEAD where TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE='" + obj.MCC_Code + "' and TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(obj.TO_DATE), "dd/MMM/yyyy hh:mm:ss tt") + "' and  TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(obj.TO_DATE), "dd/MMM/yyyy hh:mm:ss tt") + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("[ " + clsCommon.myCstr(dt.Rows.Count) + " ] Milk Purchase Invoice Geneated.can't reverse and unpost document " + obj.LOCK_CODE)
            End If
            
            qry = "Update TSPL_LOCK_MP_PC set POSTED='N'  where LOCK_CODE ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

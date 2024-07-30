Imports common
Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class clsGateEntryReturnCSA

#Region "Variables"
    Public GE_CODE As String
    Public GE_DATE As String
    Public REF_DOC_No As String
    Public Manual_Vehicle As String
    Public Customer As String
    Public Remarks As String
    Public Posted As Integer = 0
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curGE_CODE As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE as [CODE], TSPL_GATE_ENTRY_RETURN_CSA.GE_DATE as [Date],TSPL_GATE_ENTRY_RETURN_CSA.REF_DOC_No as [Ref Doc No],TSPL_GATE_ENTRY_RETURN_CSA.Manual_Vehicle as [Vehicle],TSPL_GATE_ENTRY_RETURN_CSA.Customer  as [Customer],TSPL_GATE_ENTRY_RETURN_CSA.Remarks as [Remarks] from TSPL_GATE_ENTRY_RETURN_CSA"
        str = clsCommon.ShowSelectForm("GateEntryRetFnd", qry, "CODE", whrcls, curGE_CODE, "CODE", isButtonClicked)
        Return str

    End Function
    Public Shared Function getRefDocFinder(ByVal whrcls As String, ByVal curGE_CODE As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select DOC_CODE as Code,Transfer_Date as Date,Cust_Code as Customer,From_Location_Code as Location from TSPL_CSA_TRANSFER_HEAD "
        str = clsCommon.ShowSelectForm("GateEntryRefRetFnd", qry, "Code", whrcls, curGE_CODE, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function GetRefDocDate(ByVal strcode As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select TSPL_CSA_TRANSFER_HEAD.Transfer_Date from TSPL_CSA_TRANSFER_HEAD where DOC_CODE='" + strcode + "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetCustomerCode(ByVal strcode As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select TSPL_CSA_TRANSFER_HEAD.Cust_Code from TSPL_CSA_TRANSFER_HEAD where DOC_CODE='" + strcode + "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetCustomerName(ByVal strcode As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + strcode + "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCODE As String, ByVal NavType As NavigatorType) As clsGateEntryReturnCSA
        Return GetData(strCODE, NavType, Nothing)
    End Function

    Public Shared Function PostData(ByVal strCODE As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strCODE) <= 0) Then
                Throw New Exception("GE_CODE not found to Post")
            End If
            'Dim qryLocationDate As String = "select GE_DATE from TSPL_GATE_ENTRY_RETURN_CSA where TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE ='" + obj.GE_CODE + "'   "
            'Dim dt As DataTable
            'dt = clsDBFuncationality.GetDataTable(qryLocationDate)
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.frmGateEntryReturnCS, "REF_DOC_No", clsCommon.myCDate(dt.Rows(0)("GE_DATE")), Nothing)

            Dim qry As String
            qry = "update TSPL_GATE_ENTRY_RETURN_CSA set Posted=1  where TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE ='" + strCODE + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function DeleteData(ByVal strCODE As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strCODE) <= 0) Then
                Throw New Exception("GE_CODE not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_GATE_ENTRY_RETURN_CSA where TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE ='" + strCODE + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

            Return True
        Catch ex As Exception

            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strGE_CODE As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsGateEntryReturnCSA
        Dim obj As clsGateEntryReturnCSA = Nothing
        Dim qry As String = "select * from TSPL_GATE_ENTRY_RETURN_CSA  where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE = (select MIN(TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE) from TSPL_GATE_ENTRY_RETURN_CSA)"
            Case NavigatorType.Last
                qry += " and TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE = (select Max(TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE) from TSPL_GATE_ENTRY_RETURN_CSA)"
            Case NavigatorType.Next
                qry += " and TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE = (select Min(TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE) from TSPL_GATE_ENTRY_RETURN_CSA where  TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE>'" + strGE_CODE + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE = (select Max(TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE) from TSPL_GATE_ENTRY_RETURN_CSA where TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE<'" + strGE_CODE + "')"
            Case NavigatorType.Current
                qry += " and TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE = '" + strGE_CODE + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsGateEntryReturnCSA()
            obj.GE_CODE = clsCommon.myCstr(dt.Rows(0)("GE_CODE"))
            obj.GE_DATE = clsCommon.myCDate(dt.Rows(0)("GE_DATE"))
            obj.REF_DOC_No = clsCommon.myCstr(dt.Rows(0)("REF_DOC_No"))
            obj.Customer = clsCommon.myCstr(dt.Rows(0)("Customer"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Manual_Vehicle = clsCommon.myCstr(dt.Rows(0)("Manual_Vehicle"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
        End If
        Return obj
    End Function

    Public Shared Function SaveData(ByVal obj As clsGateEntryReturnCSA, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
                isSaved = True
            End If
        Catch ex As Exception
            trans.Rollback()
            isSaved = False
            clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveData(ByVal obj As clsGateEntryReturnCSA, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Dim rbudget As String = Nothing
        Dim revno As String = Nothing
        Try
            Dim qryLocationDate As String = "select GE_DATE from TSPL_GATE_ENTRY_RETURN_CSA where TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE ='" + obj.GE_CODE + "'   "
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(qryLocationDate)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.frmGateEntryReturnCS, "REF_DOC_No", clsCommon.myCDate(dt.Rows(0)("GE_DATE")), trans)

            Dim coll As New Hashtable()
            revno = "0"
            clsCommon.AddColumnsForChange(coll, "GE_DATE", clsCommon.GetPrintDate(obj.GE_DATE, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "REF_DOC_No", obj.REF_DOC_No)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Manual_Vehicle", obj.Manual_Vehicle)
            clsCommon.AddColumnsForChange(coll, "Customer", obj.Customer)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                obj.GE_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), clsDocType.GateReturnCSASale, "", "")
                clsCommon.AddColumnsForChange(coll, "GE_CODE", obj.GE_CODE)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_GATE_ENTRY_RETURN_CSA where TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE= '" & obj.GE_CODE & "'"
                Dim check As Integer = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATE_ENTRY_RETURN_CSA", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This GE_CODE Is Already Exist")
                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATE_ENTRY_RETURN_CSA", OMInsertOrUpdate.Update, "TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE='" + obj.GE_CODE + "'", trans)
            End If
            '--------------------------------------------------------------------------------------------------------

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function CheckNewEntry(ByVal GE_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE from TSPL_GATE_ENTRY_RETURN_CSA where TSPL_GATE_ENTRY_RETURN_CSA.GE_CODE ='" + GE_CODE + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

End Class

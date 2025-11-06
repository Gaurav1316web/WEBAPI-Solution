Imports common
Imports System.Data.SqlClient

Public Class clsSaleIncentiveHeader
#Region "Variables"
    Public INCENTIVE_CODE As String = Nothing
    Public INCENTIVE_DATE As Date? = Nothing
    Public DESCRIPTION As String = Nothing
    Public FROM_DATE As Date? = Nothing
    Public TO_DATE As Date? = Nothing
    Public RANGE_UOM As String = Nothing
    Public INCENTIVE_UOM As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Post_Date As Date? = Nothing
    Public ArrIncentiveDetails As List(Of clsSaleIncentiveDetails) = Nothing
    Public ArrIncentiveCustomerMapping As ArrayList   ' List(Of clsSaleIncentiveCustomerMapping) = Nothing
    Public arrIncentiveStructureMapping As ArrayList  'List(Of clsSaleIncentiveSturctureMapping) = Nothing
    Public Form_ID As String = Nothing
    Public GL_Code As String = Nothing
    Public In_Active As Boolean = False
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select INCENTIVE_CODE as Code,convert(varchar,INCENTIVE_DATE,103) as [Incentive Date], DESCRIPTION as [Description], FROM_DATE as [From Date] , TO_DATE as [To Date], STATUS as Status, RANGE_UOM as [Range Uom], INCENTIVE_UOM as [Incentive Uom] from TSPL_SALES_INCENTIVE_HEADER  "
        str = clsCommon.ShowSelectForm("INCENTIVEFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked, "INCENTIVE_DATE")
        Return str
    End Function

    Public Function SaveData(ByVal obj As clsSaleIncentiveHeader, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
            Else
                trans.Rollback()
                Return False
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsSaleIncentiveHeader, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING WHERE INCENTIVE_CODE ='" + obj.INCENTIVE_CODE + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING WHERE INCENTIVE_CODE ='" + obj.INCENTIVE_CODE + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SALES_INCENTIVE_SLAB WHERE INCENTIVE_CODE ='" + obj.INCENTIVE_CODE + "'", trans)

            '============================================ Check Validation =============================================================
            ' , TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING.CUSTOMER_CODE,TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING.Structure_Code
            Dim qry As String = " select min (Convert (date,FROM_DATE,103)) as MinDate,max (Convert (date,TO_DATE,103)) as MaxDate from TSPL_SALES_INCENTIVE_HEADER " & _
                               " left outer join TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING on TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING.INCENTIVE_CODE = TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE " & _
                               " left outer join TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING on TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING.INCENTIVE_CODE = TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE " & _
                               " where TSPL_SALES_INCENTIVE_HEADER.In_Active=0 and " & _
                               " TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING.CUSTOMER_CODE in (" + clsCommon.GetMulcallString(obj.ArrIncentiveCustomerMapping) + ") and TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING.Structure_Code in (" + clsCommon.GetMulcallString(obj.arrIncentiveStructureMapping) + ") "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim minDate As String = clsCommon.myCstr(dt.Rows(0)("MinDate"))
                Dim maxDate As String = clsCommon.myCstr(dt.Rows(0)("MaxDate"))
                If clsCommon.myLen(minDate) > 0 AndAlso clsCommon.myLen(maxDate) > 0 Then
                    qry = " select distinct TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE from TSPL_SALES_INCENTIVE_HEADER " & _
                                        " left outer join TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING on TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING.INCENTIVE_CODE = TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE " & _
                                        " left outer join TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING on TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING.INCENTIVE_CODE = TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE " & _
                                        " where  TSPL_SALES_INCENTIVE_HEADER.In_Active=0 and convert (date, TSPL_SALES_INCENTIVE_HEADER.From_Date,103) <= convert (date, '" + obj.FROM_DATE + "',103)  and ( convert (date,TSPL_SALES_INCENTIVE_HEADER.TO_DATE,103) >= convert (date, '" + obj.TO_DATE + "',103) OR  convert (date,TSPL_SALES_INCENTIVE_HEADER.TO_DATE,103) <= convert (date, '" + obj.TO_DATE + "',103) )  " & _
                                        " and TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING.CUSTOMER_CODE in (" + clsCommon.GetMulcallString(obj.ArrIncentiveCustomerMapping) + ") and TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING.Structure_Code in (" + clsCommon.GetMulcallString(obj.arrIncentiveStructureMapping) + ") " & _
                                        " and ( convert (date, '" + clsCommon.myCDate(minDate) + "',103) between convert (date, '" + obj.FROM_DATE + "',103) and convert (date, '" + obj.TO_DATE + "',103) " & _
                                        "  or convert (date, '" + clsCommon.myCDate(maxDate) + "',103) between convert (date, '" + obj.FROM_DATE + "',103) and convert (date, '" + obj.TO_DATE + "',103) )"
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        qry = "cannnot save due to valid Incentive code exists.Please Check Details on Following :" + Environment.NewLine
                        For Each dr As DataRow In dt.Rows
                            qry += "Incentive Code [" + clsCommon.myCstr(dr("INCENTIVE_CODE")) + "] " 'and customer [" + clsCommon.myCstr(dr("CUSTOMER_CODE")) + "] and Item Structure [" + clsCommon.myCstr(dr("Structure_Code")) + "]"
                        Next
                        Throw New Exception(qry)
                    End If
                End If
            End If


            '============================================End Check Validation =============================================================


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "INCENTIVE_DATE", clsCommon.GetPrintDate(obj.INCENTIVE_DATE, "dd/MMM/yyyy hh:mm:ss tt "))
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "FROM_DATE", clsCommon.GetPrintDate(obj.FROM_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "TO_DATE", clsCommon.GetPrintDate(obj.TO_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "RANGE_UOM", obj.RANGE_UOM)
            clsCommon.AddColumnsForChange(coll, "INCENTIVE_UOM", obj.INCENTIVE_UOM)
            clsCommon.AddColumnsForChange(coll, "GL_Code", obj.GL_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                obj.INCENTIVE_CODE = clsERPFuncationality.GetNextCode(trans, obj.INCENTIVE_DATE, clsDocType.CustomerIncentiveMaster, "", "")
                clsCommon.AddColumnsForChange(coll, "INCENTIVE_CODE", obj.INCENTIVE_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "In_Active_By", Nothing, True)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALES_INCENTIVE_HEADER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALES_INCENTIVE_HEADER", OMInsertOrUpdate.Update, "INCENTIVE_CODE='" + obj.INCENTIVE_CODE + "'", trans)
            End If

            clsSaleIncentiveDetails.SaveData(obj.INCENTIVE_CODE, obj.ArrIncentiveDetails, obj.INCENTIVE_DATE, trans)
            clsSaleIncentiveSturctureMapping.SaveData(obj.INCENTIVE_CODE, obj.INCENTIVE_DATE, obj.arrIncentiveStructureMapping, trans)
            clsSaleIncentiveCustomerMapping.SaveData(obj.INCENTIVE_CODE, obj.FROM_DATE, obj.TO_DATE, obj.INCENTIVE_DATE, obj.ArrIncentiveCustomerMapping, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strSchemeCode As String, ByVal NavType As common.NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsSaleIncentiveHeader
        Dim obj As clsSaleIncentiveHeader = Nothing
        Dim qry As String = " SELECT TSPL_SALES_INCENTIVE_HEADER.* FROM TSPL_SALES_INCENTIVE_HEADER " & _
                            " where  2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE=(select MIN(INCENTIVE_CODE) from TSPL_SALES_INCENTIVE_HEADER Where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE=(select Max(INCENTIVE_CODE) from TSPL_SALES_INCENTIVE_HEADER Where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE=(select Min(INCENTIVE_CODE) from TSPL_SALES_INCENTIVE_HEADER where INCENTIVE_CODE > '" + strSchemeCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE=(select Max(INCENTIVE_CODE) from TSPL_SALES_INCENTIVE_HEADER where INCENTIVE_CODE < '" + strSchemeCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE='" + strSchemeCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSaleIncentiveHeader()
            'If dt.Rows(0)("MaxlimitStart_Date") IsNot DBNull.Value Then
            '    obj.MaxlimitStart_Date = clsCommon.myCDate(dt.Rows(0)("MaxlimitStart_Date"))
            'Else
            '    obj.MaxlimitStart_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
            'End If
            'If dt.Rows(0)("MaxlimitEnd_Date") IsNot DBNull.Value Then
            '    obj.MaxlimitEnd_Date = clsCommon.myCDate(dt.Rows(0)("MaxlimitEnd_Date"))
            'Else
            '    obj.MaxlimitEnd_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date")).AddYears(1)
            'End If
            'If dt.Rows(0)("Target_Sub_Type") IsNot DBNull.Value Then
            '    obj.Target_Sub_Type = clsCommon.myCstr(dt.Rows(0)("Target_Sub_Type"))
            'End If
            obj.INCENTIVE_CODE = clsCommon.myCstr(dt.Rows(0)("INCENTIVE_CODE"))
            obj.INCENTIVE_DATE = clsCommon.myCDate(dt.Rows(0)("INCENTIVE_DATE"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.FROM_DATE = clsCommon.myCDate(dt.Rows(0)("FROM_DATE"))
            'If dt.Rows(0)("Start_Date") IsNot DBNull.Value Then
            '    obj.Start_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
            'End If
            obj.TO_DATE = clsCommon.myCDate(dt.Rows(0)("TO_DATE"))
            obj.RANGE_UOM = clsCommon.myCstr(dt.Rows(0)("RANGE_UOM"))
            obj.INCENTIVE_UOM = clsCommon.myCstr(dt.Rows(0)("INCENTIVE_UOM"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) > 0, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.GL_Code = clsCommon.myCstr(dt.Rows(0)("GL_Code"))
            obj.In_Active = (clsCommon.myCdbl(dt.Rows(0)("In_Active")) = 1)

            '  Public Shared Function SaveData(ByVal strIncentiveCode As String, ByVal Arr As List(Of clsSaleIncentiveDetails), ByVal dtDocDate As DateTime, ByVal trans As SqlTransaction) As Boolean
            obj.ArrIncentiveDetails = clsSaleIncentiveDetails.GetData(obj.INCENTIVE_CODE, trans)
            obj.ArrIncentiveCustomerMapping = clsSaleIncentiveCustomerMapping.GetData(obj.INCENTIVE_CODE, trans)
            obj.arrIncentiveStructureMapping = clsSaleIncentiveSturctureMapping.GetData(obj.INCENTIVE_CODE, trans)
        End If
        Return obj
    End Function

    Public Shared Function fundelete(ByVal strIncentiveCode As String, ByVal trans As SqlTransaction) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsSaleIncentiveHeader
            If clsCommon.myLen(strIncentiveCode) > 0 Then
                obj = clsSaleIncentiveHeader.GetData(strIncentiveCode, NavigatorType.Current, trans)
            Else
                Throw New Exception("Document not found to delete.")
            End If
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING WHERE INCENTIVE_CODE ='" + obj.INCENTIVE_CODE + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING WHERE INCENTIVE_CODE ='" + obj.INCENTIVE_CODE + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SALES_INCENTIVE_SLAB WHERE INCENTIVE_CODE ='" + obj.INCENTIVE_CODE + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_SALES_INCENTIVE_HEADER Where INCENTIVE_CODE='" + obj.INCENTIVE_CODE + "'", trans)
            clsCustomFieldValues.DeleteData(obj.Form_ID, obj.INCENTIVE_CODE, trans)
            'trans.Commit()
            'Return True
        Catch ex As Exception
            'trans.Rollback()
            'clsCommon.MyMessageBoxShow(ex.Message)
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function postData(ByVal StrDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            postData(StrDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function postData(ByVal StrIncentiveNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(StrIncentiveNo) <= 0) Then
                Throw New Exception(" Incentive No not found to Post")
            End If
            clsERPFuncationality.IsDocumentAlreadyPosted("TSPL_SALES_INCENTIVE_HEADER", "INCENTIVE_CODE", StrIncentiveNo, "Status=1", trans)
            Dim obj As clsSaleIncentiveHeader = clsSaleIncentiveHeader.GetData(StrIncentiveNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.INCENTIVE_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.INCENTIVE_DATE, "dd/MM/yyyy"))
            End If
            Dim strQry As String = " update TSPL_SALES_INCENTIVE_HEADER set Status='1', Post_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' , Post_By = '" + objCommonVar.CurrentUserCode + "' where INCENTIVE_CODE='" & StrIncentiveNo & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function InActiveData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Incentive No not found to Post")
            End If
            Dim obj As clsSaleIncentiveHeader = clsSaleIncentiveHeader.GetData(strCode, NavigatorType.Current, Nothing)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.INCENTIVE_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If Not (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Document Should be posted for inactive")
            End If
            Dim strQry As String = " update TSPL_SALES_INCENTIVE_HEADER set In_Active='1',In_Active_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "' , In_Active_By = '" + objCommonVar.CurrentUserCode + "' where INCENTIVE_CODE='" & strCode & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsSaleIncentiveDetails
#Region "Variables"
    Public TR_CODE As String = Nothing
    Public INCENTIVE_CODE As String = Nothing
    Public SNO As Integer = 0
    Public FROM_RANGE As Double = 0.0
    Public TO_RANGE As Double = 0.0
    Public INCENTIVE As Double = 0.0

#End Region

    Public Shared Function SaveData(ByVal strIncentiveCode As String, ByVal Arr As List(Of clsSaleIncentiveDetails), ByVal dtDocDate As DateTime, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsSaleIncentiveDetails In Arr
                Dim coll As New Hashtable()
                obj.TR_CODE = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_CODE)
                clsCommon.AddColumnsForChange(coll, "SNO", obj.SNO)
                clsCommon.AddColumnsForChange(coll, "INCENTIVE_CODE", strIncentiveCode)
                clsCommon.AddColumnsForChange(coll, "FROM_RANGE", obj.FROM_RANGE)
                clsCommon.AddColumnsForChange(coll, "TO_RANGE", obj.TO_RANGE)
                clsCommon.AddColumnsForChange(coll, "INCENTIVE", obj.INCENTIVE)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALES_INCENTIVE_SLAB", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsSaleIncentiveDetails)
        Dim arr As New List(Of clsSaleIncentiveDetails)
        Dim qry As String = "Select TSPL_SALES_INCENTIVE_SLAB.* from TSPL_SALES_INCENTIVE_SLAB Where INCENTIVE_CODE='" + strCode + "'  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)


        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsSaleIncentiveDetails()
                obj.TR_CODE = clsCommon.myCstr(dr("TR_CODE"))
                obj.INCENTIVE_CODE = clsCommon.myCstr(dr("INCENTIVE_CODE"))
                obj.FROM_RANGE = clsCommon.myCdbl(dr("FROM_RANGE"))
                obj.TO_RANGE = clsCommon.myCdbl(dr("TO_RANGE"))
                obj.INCENTIVE = clsCommon.myCdbl(dr("INCENTIVE"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function


End Class

Public Class clsSaleIncentiveCustomerMapping
#Region "Variables"
    Public TR_CODE As String = Nothing
    Public INCENTIVE_CODE As String = Nothing
    Public CUSTOMER_CODE As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strIncentiveCode As String, ByVal fromDate As Date, ByVal ToDate As Date, ByVal dtDocDate As DateTime, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each strCustomerCode As String In Arr
                Dim coll As New Hashtable()
                Dim strTRCode As String = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_CODE", strTRCode)
                clsCommon.AddColumnsForChange(coll, "INCENTIVE_CODE", strIncentiveCode)
                clsCommon.AddColumnsForChange(coll, "CUSTOMER_CODE", strCustomerCode)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As ArrayList
        Dim arr As ArrayList = Nothing
        Dim qry As String = "Select TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING.* from TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING Where INCENTIVE_CODE='" + strCode + "'  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New ArrayList()
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("CUSTOMER_CODE")))
            Next
        End If
        Return arr
    End Function

End Class


Public Class clsSaleIncentiveSturctureMapping
#Region "variables"
    Public TR_CODE As String = Nothing
    Public INCENTIVE_CODE As String = Nothing
    Public STRUCTURE_CODE As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal dtDocDate As DateTime, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each strItemStrctCode As String In Arr
                    Dim coll As New Hashtable()
                    Dim strTRCode As String = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(coll, "TR_CODE", strTRCode)
                    clsCommon.AddColumnsForChange(coll, "INCENTIVE_CODE", strCode)
                    clsCommon.AddColumnsForChange(coll, "STRUCTURE_CODE", strItemStrctCode)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As ArrayList
        Dim arr As ArrayList = Nothing
        Dim qry As String = "Select TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING.* from TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING Where INCENTIVE_CODE='" + strCode + "'  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New ArrayList()
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("STRUCTURE_CODE")))
            Next
        End If
        Return arr
    End Function
End Class




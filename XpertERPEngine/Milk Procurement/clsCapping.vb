Imports System.Data.SqlClient
Imports common

Public Class clsCapping

#Region "Variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Start_Date As DateTime
    Public Start_Shift As String = Nothing
    Public End_Date As Date? = Nothing
    Public End_Shift As String = Nothing
    Public FAT As Decimal = Nothing
    Public SNF As Decimal = Nothing
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Inactive As Boolean = False
    Public ArrMCC As ArrayList = Nothing
    Public ArrVSP As ArrayList = Nothing

#End Region
    Public Function SaveData(ByVal obj As clsCapping, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsCapping, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_CAPPING", "Code", "TSPL_CAPPING_MCC", "Code", trans)

            Dim qry As String = "delete from TSPL_CAPPING_MCC where Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Start_Shift", obj.Start_Shift)
            If obj.End_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
                clsCommon.AddColumnsForChange(coll, "End_Shift", Nothing, True)
                clsCommon.AddColumnsForChange(coll, "End_Date_Created_By", Nothing, True)
                clsCommon.AddColumnsForChange(coll, "End_Date_Created_Date", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "End_Shift", obj.End_Shift)
                clsCommon.AddColumnsForChange(coll, "End_Date_Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "End_Date_Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            End If
            clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
            clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.Start_Date, clsDocType.CappingMaster, "", "")
                If (clsCommon.myLen(obj.Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CAPPING", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CAPPING", OMInsertOrUpdate.Update, "TSPL_CAPPING.Code='" + obj.Code + "'", trans)
            End If
            clsCappingMCC.SaveData(obj.Code, obj.ArrMCC, trans)
            clsCappingVSP.SaveData(obj.Code, obj.ArrVSP, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveEndDateData(ByVal obj As clsCapping) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_CAPPING", "Code", "TSPL_CAPPING_MCC", "Code", trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "End_Shift", obj.End_Shift)
            clsCommon.AddColumnsForChange(coll, "End_Date_Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "End_Date_Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CAPPING", OMInsertOrUpdate.Update, "TSPL_CAPPING.Code='" + obj.Code + "'", trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String = ""
        Try
            qry = "Delete from TSPL_CAPPING_VSP where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "Delete from TSPL_CAPPING_MCC where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "Delete from TSPL_CAPPING where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            tran.Commit()
        Catch err As Exception
            tran.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsCapping
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCapping
        Dim obj As clsCapping = Nothing
        Dim qry As String = ""
        qry = " select * from TSPL_CAPPING where 2=2 "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CAPPING.Code = (select MIN(Code) from TSPL_CAPPING where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_CAPPING.Code = (select Max(Code) from TSPL_CAPPING where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_CAPPING.Code = (select Min(Code) from TSPL_CAPPING where Code>'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Previous
                qry += " and TSPL_CAPPING.Code = (select Max(Code) from TSPL_CAPPING where Code<'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_CAPPING.Code = '" + strDocumentNo + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCapping()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Start_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
            If dt.Rows(0)("End_Date") IsNot DBNull.Value Then
                obj.End_Date = clsCommon.myCDate(dt.Rows(0)("End_Date"))
            Else
                obj.End_Date = Nothing
            End If
            obj.Start_Shift = clsCommon.myCstr(dt.Rows(0)("Start_Shift"))
            obj.End_Shift = clsCommon.myCstr(dt.Rows(0)("End_Shift"))
            obj.FAT = clsCommon.myCdbl(dt.Rows(0)("FAT"))
            obj.SNF = clsCommon.myCdbl(dt.Rows(0)("SNF"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Inactive = (clsCommon.myCdbl(dt.Rows(0)("Inactive")) = 1)

            qry = " select TSPL_CAPPING_MCC.MCC_Code from TSPL_CAPPING_MCC where TSPL_CAPPING_MCC.Code='" & obj.Code & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrMCC = New ArrayList()
                For Each dr As DataRow In dt.Rows
                    obj.ArrMCC.Add(clsCommon.myCstr(dr("MCC_Code")))
                Next
            End If

            qry = " select TSPL_CAPPING_VSP.VSP_Code from TSPL_CAPPING_VSP where TSPL_CAPPING_VSP.Code='" & obj.Code & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrVSP = New ArrayList()
                For Each dr As DataRow In dt.Rows
                    obj.ArrVSP.Add(clsCommon.myCstr(dr("VSP_Code")))
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim obj As clsCapping = clsCapping.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Posted = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post Date")
            End If

            Dim qry As String = "Update TSPL_CAPPING set Posted=1,Posted_By='" + objCommonVar.CurrentUserCode + "',Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") + "' where Code='" + obj.Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function InactiveData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found")
            End If
            Dim obj As clsCapping = clsCapping.GetData(strCode, NavigatorType.Current, trans)
            If obj Is Nothing OrElse obj.ArrMCC.Count <= 0 Then
                Throw New Exception("Invalid code")
            End If
            If obj.Posted <> ERPTransactionStatus.Approved Then
                Throw New Exception("Should be posted Document " + obj.Code)
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Inactive", 1)
            clsCommon.AddColumnsForChange(coll, "Inactive_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Inactive_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CAPPING", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetMappingCode(ByVal strMCC As String, ByVal strVSP As String, ByVal TransDate As DateTime, ByVal trans As SqlTransaction) As clsCapping
        Dim obj As clsCapping = Nothing
        'Dim qry As String = "select top 1 case when TSPL_CAPPING.Inactive=0 then TSPL_CAPPING.Code else '' end as  Code" + Environment.NewLine +
        '    "from TSPL_CAPPING " + Environment.NewLine +
        '    "left outer join TSPL_FARMER_PRO_VSP on TSPL_FARMER_PRO_VSP.Code = TSPL_CAPPING.Code" + Environment.NewLine +
        '    "left outer join TSPL_CAPPING_MCC on TSPL_CAPPING_MCC.Code = TSPL_CAPPING.Code" + Environment.NewLine +
        '    "where TSPL_FARMER_PRO_VSP.VSP_Code='" & strVSP & "' and  TSPL_CAPPING_MCC.MCC_Code='" & strMCC & "'" + Environment.NewLine +
        '    "and '" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "'>=TSPL_CAPPING.Start_Date  and (2= case when TSPL_CAPPING.End_Date is null then 2 else case when '" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "'<= TSPL_CAPPING.End_Date then 2 else 3 end end)  and TSPL_CAPPING.Posted=1 order by TSPL_CAPPING.Start_Date desc,TSPL_CAPPING.Code desc"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '    If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Code"))) > 0 Then
        '        obj = clsCapping.GetData(clsCommon.myCstr(dt.Rows(0)("Code")), NavigatorType.Current, trans)
        '    End If
        'End If
        Return obj
    End Function

    Public Shared Function GetLatestCodeByDate(ByVal strMCC As String, ByVal strVSP As String, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As String
        Dim qry As String = ""
        '        Dim qry As String = "select * from (" + Environment.NewLine +
        '"select *,(select top 1 case when TSPL_CAPPING.Inactive=0 then TSPL_CAPPING.Code else '' end as  Code" + Environment.NewLine +
        '"from TSPL_CAPPING " + Environment.NewLine +
        '"left outer join TSPL_FARMER_PRO_VSP on TSPL_FARMER_PRO_VSP.Code = TSPL_CAPPING.Code" + Environment.NewLine +
        '"left outer join TSPL_CAPPING_MCC on TSPL_CAPPING_MCC.Code = TSPL_CAPPING.Code" + Environment.NewLine +
        '"where TSPL_FARMER_PRO_VSP.VSP_Code='" + strVSP + "' and  TSPL_CAPPING_MCC.MCC_Code='" + strMCC + "'" + Environment.NewLine +
        '"and x.thedate>=TSPL_CAPPING.Start_Date  " + Environment.NewLine +
        '"and 2= (case when TSPL_CAPPING.Start_Shift='E' and x.thedate=TSPL_CAPPING.Start_Date and x.shift='M' then 3 else 2 end)" + Environment.NewLine +
        '"and (2= case when TSPL_CAPPING.End_Date is null then 2 else case when ((x.thedate<= TSPL_CAPPING.End_Date )" + Environment.NewLine +
        '"and (2= case when TSPL_CAPPING.End_Shift='M' and x.thedate=TSPL_CAPPING.End_Date and x.shift='E' then 3 else 2 end)) then 2 else 3 end end)  " + Environment.NewLine +
        '"and TSPL_CAPPING.Posted=1 order by TSPL_CAPPING.Start_Date desc,TSPL_CAPPING.Code desc) as PriceCode " + Environment.NewLine +
        '"from (" + Environment.NewLine +
        '"select * FROM ExplodeDates('" + clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") + "') as TabDateRange,(select 'M' as Shift Union all select 'E' as Shift )as TabShift ) x" + Environment.NewLine +
        '")xx where PriceCode is not null"
        Return qry
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_CAPPING.Code,TSPL_CAPPING.Description,TSPL_CAPPING.Start_Date,Start_Shift,End_Date,End_Shift from TSPL_CAPPING "
        str = clsCommon.ShowSelectForm("CAPing", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function

    Public Shared Function getFinderObeject(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As clsCapping
        Dim obj As clsCapping = Nothing
        Dim strCode As String = getFinder(whrcls, curcode, isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            obj = GetData(strCode, NavigatorType.Current)
        End If
        Return obj
    End Function
End Class

Public Class clsCappingMCC
#Region "variable"
    Public Code As String = Nothing
    Public MCC_Code As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each strMCC As String In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "MCC_Code", strMCC)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CAPPING_MCC", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class

Public Class clsCappingVSP
#Region "variable"
    Public Code As String = Nothing
    Public VSP_Code As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each strMCC As String In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "VSP_Code", strMCC)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CAPPING_VSP", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class

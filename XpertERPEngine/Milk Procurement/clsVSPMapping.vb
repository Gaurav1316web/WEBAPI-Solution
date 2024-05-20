Imports System.Data.SqlClient
Imports common

Public Class clsVSPMapping

#Region "Variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Start_Date As DateTime
    Public End_Date As Date?
    Public Commission_Code As String = Nothing
    Public Deduction_Code As String = Nothing
    Public Day_Wise_Incentive_Code As String = Nothing
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Inactive As Boolean = False
    Public ArrMCC As ArrayList = Nothing
    Public ArrRoute As ArrayList = Nothing
    Public ArrVSP As ArrayList = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsVSPMapping, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsVSPMapping, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_VSP_MAPPING", "Code", "TSPL_VSP_MAPPING_MCC", "Code", "TSPL_VSP_MAPPING_VSP", "Code", trans)

            Dim qry As String = "delete from TSPL_VSP_MAPPING_MCC where Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_VSP_MAPPING_VSP where Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
            If obj.End_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Commission_Code", obj.Commission_Code, True)
            clsCommon.AddColumnsForChange(coll, "Deduction_Code", obj.Deduction_Code, True)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_Code", obj.Day_Wise_Incentive_Code, True)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.Start_Date, clsDocType.VSPMapping, "", "")
                If (clsCommon.myLen(obj.Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_MAPPING", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_MAPPING", OMInsertOrUpdate.Update, "TSPL_VSP_MAPPING.Code='" + obj.Code + "'", trans)
            End If
            clsVSPMappingMCC.SaveData(obj.Code, obj.ArrMCC, trans)
            clsVSPMappingVSP.SaveData(obj.Code, obj.ArrVSP, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String = ""
        Try
            qry = "Delete from TSPL_VSP_MAPPING_MCC where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "Delete from TSPL_VSP_MAPPING_VSP where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "Delete from TSPL_VSP_MAPPING where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            tran.Commit()
        Catch err As Exception
            tran.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsVSPMapping
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsVSPMapping
        Dim obj As clsVSPMapping = Nothing
        Dim qry As String = ""
        qry = " select * from TSPL_VSP_MAPPING where 2=2 "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_VSP_MAPPING.Code = (select MIN(Code) from TSPL_VSP_MAPPING where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_VSP_MAPPING.Code = (select Max(Code) from TSPL_VSP_MAPPING where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_VSP_MAPPING.Code = (select Min(Code) from TSPL_VSP_MAPPING where Code>'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Previous
                qry += " and TSPL_VSP_MAPPING.Code = (select Max(Code) from TSPL_VSP_MAPPING where Code<'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_VSP_MAPPING.Code = '" + strDocumentNo + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsVSPMapping()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Start_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
            If dt.Rows(0)("End_Date") IsNot DBNull.Value Then
                obj.End_Date = clsCommon.myCDate(dt.Rows(0)("End_Date"))
            End If
            obj.Commission_Code = clsCommon.myCstr(dt.Rows(0)("Commission_Code"))
            obj.Deduction_Code = clsCommon.myCstr(dt.Rows(0)("Deduction_Code"))
            obj.Day_Wise_Incentive_Code = clsCommon.myCstr(dt.Rows(0)("Day_Wise_Incentive_Code"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Inactive = (clsCommon.myCdbl(dt.Rows(0)("Inactive")) = 1)

            qry = " select TSPL_VSP_MAPPING_MCC.MCC_Code from TSPL_VSP_MAPPING_MCC where TSPL_VSP_MAPPING_MCC.Code='" & obj.Code & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrMCC = New ArrayList()
                For Each dr As DataRow In dt.Rows
                    obj.ArrMCC.Add(clsCommon.myCstr(dr("MCC_Code")))
                Next
            End If

            qry = " select TSPL_VSP_MAPPING_VSP.VSP_Code,TSPL_VLC_MASTER_HEAD.Route_Code  from TSPL_VSP_MAPPING_VSP left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VSP_MAPPING_VSP.VSP_Code where TSPL_VSP_MAPPING_VSP.Code='" & obj.Code & "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.ArrVSP = New ArrayList()
                obj.ArrRoute = New ArrayList()
                For Each dr As DataRow In dt.Rows
                    obj.ArrVSP.Add(clsCommon.myCstr(dr("VSP_Code")))
                    If Not obj.ArrRoute.Contains(clsCommon.myCstr(dr("Route_Code"))) Then
                        obj.ArrRoute.Add(clsCommon.myCstr(dr("Route_Code")))
                    End If
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
            Dim obj As clsVSPMapping = clsVSPMapping.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Posted = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post Date")
            End If

            Dim qry As String = "Update TSPL_VSP_MAPPING set Posted=1,Posted_By='" + objCommonVar.CurrentUserCode + "',Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") + "' where Code='" + obj.Code + "' "
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
            Dim obj As clsVSPMapping = clsVSPMapping.GetData(strCode, NavigatorType.Current, trans)
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
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_MAPPING", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetMappingCode(ByVal strMCC As String, ByVal strVSP As String, ByVal TransDate As DateTime, ByVal trans As SqlTransaction) As clsVSPMapping
        Dim obj As clsVSPMapping = Nothing
        Dim qry As String = "select top 1 TSPL_VSP_MAPPING.Code " + Environment.NewLine +
            "from TSPL_VSP_MAPPING " + Environment.NewLine +
            "left outer join TSPL_VSP_MAPPING_VSP on TSPL_VSP_MAPPING_VSP.Code = TSPL_VSP_MAPPING.Code" + Environment.NewLine +
            "left outer join TSPL_VSP_MAPPING_MCC on TSPL_VSP_MAPPING_MCC.Code = TSPL_VSP_MAPPING.Code" + Environment.NewLine +
            "where TSPL_VSP_MAPPING_VSP.VSP_Code='" & strVSP & "' and  TSPL_VSP_MAPPING_MCC.MCC_Code='" & strMCC & "'" + Environment.NewLine +
            "and '" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "'>=TSPL_VSP_MAPPING.Start_Date  and (2= case when TSPL_VSP_MAPPING.End_Date is null then 2 else case when '" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "'<= TSPL_VSP_MAPPING.End_Date then 2 else 3 end end) and TSPL_VSP_MAPPING.Inactive=0 and TSPL_VSP_MAPPING.Posted=1 order by TSPL_VSP_MAPPING.Start_Date desc,TSPL_VSP_MAPPING.Code desc"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = clsVSPMapping.GetData(clsCommon.myCstr(dt.Rows(0)("Code")), NavigatorType.Current, trans)
        End If
        Return obj
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_VSP_MAPPING.Code,TSPL_VSP_MAPPING.Description,TSPL_VSP_MAPPING.Start_Date,End_Date,Commission_Code,Deduction_Code,Day_Wise_Incentive_Code   from TSPL_VSP_MAPPING "
        str = clsCommon.ShowSelectForm("vspMapfnd", qry, "Code", whrcls, curcode, "Code", isButtonClicked, "TSPL_VSP_MAPPING.Created_Date")
        Return str

    End Function

    Public Shared Function getFinderObeject(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As clsVSPMapping
        Dim obj As clsVSPMapping = Nothing
        Dim strCode As String = getFinder(whrcls, curcode, isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            obj = GetData(strCode, NavigatorType.Current)
        End If
        Return obj
    End Function
End Class

Public Class clsVSPMappingMCC
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
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_MAPPING_MCC", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class


Public Class clsVSPMappingVSP
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
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_MAPPING_VSP", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class
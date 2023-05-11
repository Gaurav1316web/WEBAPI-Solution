Imports System.Data.SqlClient
Imports common

Public Class clsMPIncetive

#Region "Variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Start_Date As DateTime
    Public Start_Shift As String = Nothing
    Public End_Date As Date? = Nothing
    Public End_Shift As String = Nothing
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Inactive As Boolean = False

    Public ArrMCC As ArrayList = Nothing
    Public ArrVSP As ArrayList = Nothing
    Public ArrMP As ArrayList = Nothing
    Public Arr As List(Of clsMPIncetiveDetail) = Nothing

#End Region
    Public Function SaveData(ByVal obj As clsMPIncetive, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsMPIncetive, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_MP_INCETIVE", "Code", "TSPL_MP_INCETIVE_MCC", "Code", "TSPL_MP_INCETIVE_VLC", "Code", trans)

            Dim qry As String = "delete from TSPL_MP_INCETIVE_MCC where Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MP_INCETIVE_VLC where Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "delete from TSPL_MP_INCETIVE_MP where Code='" + obj.Code + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MP_INCETIVE_DETAIL where Code='" + obj.Code + "'"
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
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.Start_Date, clsDocType.MPIncetiveSlab, "", "")
                If (clsCommon.myLen(obj.Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_INCETIVE", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_INCETIVE", OMInsertOrUpdate.Update, "TSPL_MP_INCETIVE.Code='" + obj.Code + "'", trans)
            End If
            clsMPIncetiveMCC.SaveData(obj.Code, obj.ArrMCC, trans)
            clsMPIncetiveVLC.SaveData(obj.Code, obj.ArrVSP, trans)
            'clsMPIncetiveMP.SaveData(obj.Code, obj.ArrMP, trans)
            clsMPIncetiveDetail.SaveData(obj.Start_Date, obj.Code, obj.Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveEndDateData(ByVal obj As clsMPIncetive) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_MP_INCETIVE", "Code", "TSPL_MP_INCETIVE_MCC", "Code", "TSPL_MP_INCETIVE_VLC", "Code", trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "End_Shift", obj.End_Shift)
            clsCommon.AddColumnsForChange(coll, "End_Date_Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "End_Date_Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_INCETIVE", OMInsertOrUpdate.Update, "TSPL_MP_INCETIVE.Code='" + obj.Code + "'", trans)
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
            qry = "Delete from TSPL_MP_INCETIVE_MCC where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "Delete from TSPL_MP_INCETIVE_VLC where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            ''qry = "Delete from TSPL_MP_INCETIVE_MP where Code='" + strCode + "'"
            ''clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "Delete from TSPL_MP_INCETIVE_DETAIL where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "Delete from TSPL_MP_INCETIVE where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            tran.Commit()
        Catch err As Exception
            tran.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsMPIncetive
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMPIncetive
        Dim obj As clsMPIncetive = Nothing
        Dim qry As String = ""
        qry = " select * from TSPL_MP_INCETIVE where 2=2 "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MP_INCETIVE.Code = (select MIN(Code) from TSPL_MP_INCETIVE where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_MP_INCETIVE.Code = (select Max(Code) from TSPL_MP_INCETIVE where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_MP_INCETIVE.Code = (select Min(Code) from TSPL_MP_INCETIVE where Code>'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Previous
                qry += " and TSPL_MP_INCETIVE.Code = (select Max(Code) from TSPL_MP_INCETIVE where Code<'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_MP_INCETIVE.Code = '" + strDocumentNo + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMPIncetive()
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
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Inactive = (clsCommon.myCdbl(dt.Rows(0)("Inactive")) = 1)

            qry = " select TSPL_MP_INCETIVE_MCC.MCC_Code from TSPL_MP_INCETIVE_MCC where TSPL_MP_INCETIVE_MCC.Code='" & obj.Code & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrMCC = New ArrayList()
                For Each dr As DataRow In dt.Rows
                    obj.ArrMCC.Add(clsCommon.myCstr(dr("MCC_Code")))
                Next
            End If

            qry = " select TSPL_MP_INCETIVE_VLC.VLC_Code from TSPL_MP_INCETIVE_VLC  where TSPL_MP_INCETIVE_VLC.Code='" & obj.Code & "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.ArrVSP = New ArrayList()
                For Each dr As DataRow In dt.Rows
                    obj.ArrVSP.Add(clsCommon.myCstr(dr("VLC_Code")))
                Next
            End If

            'qry = " select TSPL_MP_INCETIVE_MP.MP_Code from TSPL_MP_INCETIVE_MP  where TSPL_MP_INCETIVE_MP.Code='" & obj.Code & "'"
            'dt = clsDBFuncationality.GetDataTable(qry, trans)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    obj.ArrMP = New ArrayList()
            '    For Each dr As DataRow In dt.Rows
            '        obj.ArrMP.Add(clsCommon.myCstr(dr("MP_Code")))
            '    Next
            'End If

            obj.Arr = clsMPIncetiveDetail.GetData(obj.Code, trans)
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
            Dim obj As clsMPIncetive = clsMPIncetive.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Posted = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post Date")
            End If

            Dim qry As String = "Update TSPL_MP_INCETIVE set Posted=1,Posted_By='" + objCommonVar.CurrentUserCode + "',Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") + "' where Code='" + obj.Code + "' "
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
            Dim obj As clsMPIncetive = clsMPIncetive.GetData(strCode, NavigatorType.Current, trans)
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
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_INCETIVE", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetIncentive(ByVal SNF As String, ByVal strMCC As String, ByVal strVLC As String, ByVal TransDate As DateTime, ByVal trans As SqlTransaction) As clsMPIncetiveDetail
        Return GetIncentive(False, SNF, strMCC, strVLC, TransDate, trans)
    End Function

    Public Shared Function GetIncentive(ByVal isUploaderCode As Boolean, ByVal SNF As String, ByVal strMCC As String, ByVal strVLC As String, ByVal TransDate As DateTime, ByVal trans As SqlTransaction) As clsMPIncetiveDetail
        Dim qry As String = ""
        If isUploaderCode Then
            qry = "select VLC_Code from TSPL_VLC_MASTER_HEAD where MCC='" + strMCC + "' and  VLC_Code_VLC_Uploader='" + strVLC + "'"
            qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(qry) <= 0 Then
                Throw New Exception("Invalid VLC [" + strVLC + "] of MCC [" + strMCC + "]")
            Else
                strVLC = qry
            End If
            'qry = "select MP_Code from TSPL_MP_MASTER where VLC_Code='" + strVLC + "' and  MP_Code_VLC_Uploader='" + strMP + "'"
            'qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            'If clsCommon.myLen(qry) <= 0 Then
            '    Throw New Exception("Invalid MP [" + strMP + "] VLC [" + strVLC + "] of MCC [" + strMCC + "]")
            'Else
            '    strMP = qry
            'End If
        End If
        Dim obj As clsMPIncetiveDetail = Nothing
        qry = "select top 1 case when TSPL_MP_INCETIVE.Inactive=0 then TSPL_MP_INCETIVE.Code else '' end as  FindCode,TSPL_MP_INCETIVE_DETAIL.* " + Environment.NewLine +
            " from TSPL_MP_INCETIVE " + Environment.NewLine +
            " left outer join TSPL_MP_INCETIVE_MCC on TSPL_MP_INCETIVE_MCC.Code = TSPL_MP_INCETIVE.Code" + Environment.NewLine +
            " left outer join TSPL_MP_INCETIVE_VLC on TSPL_MP_INCETIVE_VLC.Code = TSPL_MP_INCETIVE.Code" + Environment.NewLine +
            " left outer join TSPL_MP_INCETIVE_DETAIL on TSPL_MP_INCETIVE_DETAIL.Code = TSPL_MP_INCETIVE.Code" + Environment.NewLine +
            " where TSPL_MP_INCETIVE_DETAIL.Slab_From <='" + SNF + "' and TSPL_MP_INCETIVE_DETAIL.Slab_To>='" + SNF + "'  and TSPL_MP_INCETIVE_VLC.VLC_Code='" & strVLC & "' and  TSPL_MP_INCETIVE_MCC.MCC_Code='" & strMCC & "'" + Environment.NewLine +
            " and '" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "'>=TSPL_MP_INCETIVE.Start_Date  and (2= case when TSPL_MP_INCETIVE.End_Date is null then 2 else case when '" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "'<= TSPL_MP_INCETIVE.End_Date then 2 else 3 end end)  and TSPL_MP_INCETIVE.Posted=1 order by TSPL_MP_INCETIVE.Start_Date desc,TSPL_MP_INCETIVE.Code desc"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("FindCode"))) > 0 Then
                obj = New clsMPIncetiveDetail()
                obj.TRCode = clsCommon.myCstr(dt.Rows(0)("TRCode"))
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.SNo = clsCommon.myCdbl(dt.Rows(0)("SNo"))
                obj.Slab_From = clsCommon.myCdbl(dt.Rows(0)("Slab_From"))
                obj.Slab_To = clsCommon.myCdbl(dt.Rows(0)("Slab_To"))
                obj.Slab_Value = clsCommon.myCdbl(dt.Rows(0)("Slab_Value"))
            End If
        End If
        Return obj
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_MP_INCETIVE.Code,TSPL_MP_INCETIVE.Description,TSPL_MP_INCETIVE.Start_Date,Start_Shift,End_Date,End_Shift from TSPL_MP_INCETIVE "
        str = clsCommon.ShowSelectForm("MPINCCodef", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function getFinderObeject(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As clsMPIncetive
        Dim obj As clsMPIncetive = Nothing
        Dim strCode As String = getFinder(whrcls, curcode, isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            obj = GetData(strCode, NavigatorType.Current)
        End If
        Return obj
    End Function
End Class

Public Class clsMPIncetiveMCC
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
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_INCETIVE_MCC", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class


Public Class clsMPIncetiveVLC
#Region "variable"
    Public Code As String = Nothing
    Public VLC_Code As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each strVLC As String In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "VLC_Code", strVLC)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_INCETIVE_VLC", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class

'Public Class clsMPIncetiveMP
'#Region "variable"
'    Public Code As String = Nothing
'    Public MP_Code As String = Nothing

'#End Region

'    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
'        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
'            For Each strMP As String In Arr
'                Dim coll As New Hashtable()
'                clsCommon.AddColumnsForChange(coll, "Code", strDocNo)
'                clsCommon.AddColumnsForChange(coll, "MP_Code", strMP)
'                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_INCETIVE_MP", OMInsertOrUpdate.Insert, "", trans)
'            Next
'        End If
'        Return True
'    End Function
'End Class

Public Class clsMPIncetiveDetail
#Region "variables"
    Public TRCode As String = Nothing
    Public SNo As Integer = 0
    Public Code As String = Nothing
    Public Slab_From As Decimal
    Public Slab_To As Decimal
    Public Slab_Value As Decimal
#End Region

    Public Shared Function SaveData(ByVal TransDate As DateTime, ByVal strDocNo As String, ByVal Arr As List(Of clsMPIncetiveDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim cnt As Integer = 0
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsMPIncetiveDetail In Arr
                    Dim coll As New Hashtable()
                    cnt += 1
                    clsCommon.AddColumnsForChange(coll, "TRCode", clsERPFuncationality.GetNextCode(trans, TransDate, clsDocType.Detail, clsDocTransactionType.Detail, ""))
                    clsCommon.AddColumnsForChange(coll, "Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "SNo", cnt)
                    clsCommon.AddColumnsForChange(coll, "Slab_From", clsCommon.myCdbl(obj.Slab_From))
                    clsCommon.AddColumnsForChange(coll, "Slab_To", clsCommon.myCdbl(obj.Slab_To))
                    clsCommon.AddColumnsForChange(coll, "Slab_Value", clsCommon.myCdbl(obj.Slab_Value))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_INCETIVE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal tran As SqlTransaction) As List(Of clsMPIncetiveDetail)
        Dim arr As New List(Of clsMPIncetiveDetail)
        Dim qry As String = "Select TSPL_MP_INCETIVE_DETAIL.* from TSPL_MP_INCETIVE_DETAIL Where Code='" + strCode + "'  order by SNo "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsMPIncetiveDetail()
                obj.SNo = CInt(dr("SNo"))
                obj.Slab_From = clsCommon.myCdbl(dr("Slab_From"))
                obj.Slab_To = clsCommon.myCdbl(dr("Slab_To"))
                obj.Slab_Value = clsCommon.myCdbl(dr("Slab_Value"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class
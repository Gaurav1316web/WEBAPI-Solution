Imports common
Imports System.Data.SqlClient

Public Class clsDeductionMappingHead
#Region "Variables"
    Public Doc_Code As String
    Public Doc_Date As DateTime
    Public Description As String
    Public Start_Date As DateTime
    Public End_Date As Date?
    Public Is_Round_Down As Boolean
    Public Post_Status As ERPTransactionStatus
    Public Arr As List(Of clsDeductionMappingDetail) = Nothing
    Public ArrMCC As ArrayList = Nothing
    Public ArrVLC As ArrayList = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsDeductionMappingHead, ByVal IsNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_DEDUCTION_MAPPING_DETAIL where Doc_Code='" + obj.Doc_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_DEDUCTION_MAPPING_VLC where Doc_Code='" + obj.Doc_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_DEDUCTION_MAPPING_MCC where Doc_Code='" + obj.Doc_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
            If obj.End_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Is_Round_Down", IIf(obj.Is_Round_Down, 1, 0))

            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If IsNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                obj.Doc_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.DeductionMapping, "", "")
                If clsCommon.myLen(obj.Doc_Code) <= 0 Then
                    Throw New Exception("Error in code generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.Doc_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEDUCTION_MAPPING_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEDUCTION_MAPPING_HEAD", OMInsertOrUpdate.Update, "Doc_Code='" & obj.Doc_Code & "'", trans)
            End If
            Dim objtr As New clsDeductionMappingDetail
            objtr.SaveData(obj.Doc_Code, obj.Arr, trans)
            objtr = Nothing

            Dim objtrMCC As New clsDeductionMappingMCC
            objtrMCC.SaveData(obj.Doc_Code, obj.ArrMCC, trans)
            objtrMCC = Nothing

            Dim objtrVLC As New clsDeductionMappingVLC
            objtrVLC.SaveData(obj.Doc_Code, obj.ArrVLC, trans)
            objtrVLC = Nothing
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal tran As SqlTransaction) As clsDeductionMappingHead
        Dim qry As String = "select * from TSPL_DEDUCTION_MAPPING_HEAD Where 2=2 "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DEDUCTION_MAPPING_HEAD.Doc_Code = (select MIN(Doc_Code) from TSPL_DEDUCTION_MAPPING_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_DEDUCTION_MAPPING_HEAD.Doc_Code = (select Max(Doc_Code) from TSPL_DEDUCTION_MAPPING_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_DEDUCTION_MAPPING_HEAD.Doc_Code = (select Min(Doc_Code) from TSPL_DEDUCTION_MAPPING_HEAD where Doc_Code>'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_DEDUCTION_MAPPING_HEAD.Doc_Code = (select Max(Doc_Code) from TSPL_DEDUCTION_MAPPING_HEAD where Doc_Code<'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_DEDUCTION_MAPPING_HEAD.Doc_Code = '" + strDocNo + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        Dim obj As clsDeductionMappingHead = Nothing
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDeductionMappingHead()
            obj.Doc_Code = clsCommon.myCstr(dt.Rows(0)("Doc_Code"))
            obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Start_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
            If dt.Rows(0)("End_Date") IsNot DBNull.Value Then
                obj.End_Date = clsCommon.myCDate(dt.Rows(0)("End_Date"))
            End If
            obj.Is_Round_Down = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Round_Down")) = 1, True, False)
            obj.Post_Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Post_Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)


            qry = "select TSPL_DEDUCTION_MAPPING_DETAIL.*,TSPL_DEDUCTION_MASTER.Description,TSPL_DEDUCTION_MASTER.GL_Account_Code  from TSPL_DEDUCTION_MAPPING_DETAIL left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_DEDUCTION_MAPPING_DETAIL.Deduction_Code where TSPL_DEDUCTION_MAPPING_DETAIL.Doc_Code='" + obj.Doc_Code + "' order by TSPL_DEDUCTION_MAPPING_DETAIL.SNo "
            dt = clsDBFuncationality.GetDataTable(qry, tran)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsDeductionMappingDetail)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsDeductionMappingDetail
                    objtr.SNo = clsCommon.myCdbl(dr("SNo"))
                    objtr.TR_Code = clsCommon.myCstr(dr("TR_Code"))
                    objtr.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                    objtr.Deduction_Code = clsCommon.myCstr(dr("Deduction_Code"))
                    objtr.DeductionName = clsCommon.myCstr(dr("Description"))
                    objtr.DeductionGLAccount = clsCommon.myCstr(dr("GL_Account_Code"))
                    objtr.Type = clsCommon.myCstr(dr("Type"))
                    objtr.Per = clsCommon.myCdbl(dr("Per"))
                    obj.Arr.Add(objtr)
                Next
            End If
            obj.ArrMCC = clsDeductionMappingMCC.GetData(obj.Doc_Code, tran)
            obj.ArrVLC = clsDeductionMappingVLC.GetData(obj.Doc_Code, tran)
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Purchase Order No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsDeductionMappingHead = clsDeductionMappingHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Post_Status = 1) Then
                Throw New Exception("Already Posted")
            End If

            Dim qry As String = "Update TSPL_DEDUCTION_MAPPING_HEAD set Post_Status=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Doc_Code='" + strDocNo + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
         
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Plan not found to Delete")
        End If
        Dim obj As clsDeductionMappingHead = clsDeductionMappingHead.GetData(strCode, NavigatorType.Current, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
            Try
                If (obj.Post_Status = ERPTransactionStatus.Approved) Then
                    Throw New Exception("Already Posted")
                End If
                Dim qry As String = "delete from TSPL_DEDUCTION_MAPPING_DETAIL where Doc_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_DEDUCTION_MAPPING_VLC where Doc_Code='" + obj.Doc_Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_DEDUCTION_MAPPING_MCC where Doc_Code='" + obj.Doc_Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_DEDUCTION_MAPPING_HEAD where Doc_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function

    Public Function GetLatestMappingCode(ByVal strMCCCode As String, ByVal strVSPCode As String, ByVal DocDate As DateTime, ByVal tran As SqlTransaction) As clsDeductionMappingHead
        Dim obj As clsDeductionMappingHead = Nothing
        Dim strVLCCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Code from TSPL_VLC_MASTER_HEAD where VSP_Code='" + strVSPCode + "'", tran))
        Dim qry As String = "select top 1 * from (" + Environment.NewLine + _
        "select * from (" + Environment.NewLine + _
        "select top 1 TSPL_DEDUCTION_MAPPING_HEAD.Doc_Code,TSPL_DEDUCTION_MAPPING_HEAD.Start_Date,TSPL_DEDUCTION_MAPPING_HEAD.Doc_Date from TSPL_DEDUCTION_MAPPING_HEAD  " + Environment.NewLine + _
        "left outer join TSPL_DEDUCTION_MAPPING_MCC on TSPL_DEDUCTION_MAPPING_MCC.Doc_Code=TSPL_DEDUCTION_MAPPING_HEAD.Doc_Code" + Environment.NewLine + _
        "inner join TSPL_DEDUCTION_MAPPING_VLC on TSPL_DEDUCTION_MAPPING_VLC.Doc_Code=TSPL_DEDUCTION_MAPPING_HEAD.Doc_Code" + Environment.NewLine + _
        "where TSPL_DEDUCTION_MAPPING_MCC.MCC_Code='" + strMCCCode + "' and TSPL_DEDUCTION_MAPPING_VLC.VLC_Code='" + strVLCCode + "' and Post_Status=1 " + Environment.NewLine + _
        "and Start_Date<='" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "' and 2=case when End_Date is null then 2 else case when End_Date>='" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "' then 2 else 3 end end" + Environment.NewLine + _
        "order by Start_Date desc,Doc_Date desc " + Environment.NewLine + _
        ")x" + Environment.NewLine + _
        "union all" + Environment.NewLine + _
        "select * from (" + Environment.NewLine + _
        "select top 1 TSPL_DEDUCTION_MAPPING_HEAD.Doc_Code,TSPL_DEDUCTION_MAPPING_HEAD.Start_Date,TSPL_DEDUCTION_MAPPING_HEAD.Doc_Date from TSPL_DEDUCTION_MAPPING_HEAD  " + Environment.NewLine + _
        "left outer join TSPL_DEDUCTION_MAPPING_MCC on TSPL_DEDUCTION_MAPPING_MCC.Doc_Code=TSPL_DEDUCTION_MAPPING_HEAD.Doc_Code" + Environment.NewLine + _
        "where  not exists(select 1 from TSPL_DEDUCTION_MAPPING_VLC where TSPL_DEDUCTION_MAPPING_VLC.Doc_Code=TSPL_DEDUCTION_MAPPING_HEAD.Doc_Code)  and TSPL_DEDUCTION_MAPPING_MCC.MCC_Code='" + strMCCCode + "' and Post_Status=1 " + Environment.NewLine + _
        "and Start_Date<='" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "' and 2=case when End_Date is null then 2 else case when End_Date>='" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "' then 2 else 3 end end" + Environment.NewLine + _
        "order by Start_Date desc,Doc_Date desc" + Environment.NewLine + _
        ")x" + Environment.NewLine + _
        ")xx order by Start_Date desc,Doc_Date desc"
        Dim strCode As String = clsDBFuncationality.getSingleValue(qry, tran)
        If clsCommon.myLen(strCode) > 0 Then
            obj = GetData(strCode, NavigatorType.Current, tran)
        End If
        Return obj
    End Function

    Public Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select Document_No from TSPL_VENDOR_INVOICE_HEAD where RefDocType='DED-MAP' and Description like '%" + strDocNo + "%'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Deduction Mapping code is used in AP Invoice No:" + clsCommon.myCstr(dt.Rows(0)("Document_No")))
            End If

            qry = "Update TSPL_DEDUCTION_MAPPING_HEAD set Post_Status=0, Posted_Date=null,Posted_By=null where Doc_Code='" + strDocNo + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsDeductionMappingMCC
#Region "variables"
    Public Doc_Code As String = Nothing
    Public MCC_Code As String
#End Region

    Public Function SaveData(ByVal strCode As String, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        Try
            For Each obj As String In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Doc_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "MCC_Code", obj)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEDUCTION_MAPPING_MCC", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As ArrayList
        Dim arr As New ArrayList
        Dim qry As String = "Select TSPL_DEDUCTION_MAPPING_MCC.* from TSPL_DEDUCTION_MAPPING_MCC Where Doc_Code='" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("MCC_Code")))
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsDeductionMappingVLC
#Region "variables"
    Public Doc_Code As String = Nothing
    Public VLC_Code As String
#End Region

    Public Function SaveData(ByVal strCode As String, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As String In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "VLC_Code", obj)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEDUCTION_MAPPING_VLC", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As ArrayList
        Dim arr As New ArrayList
        Dim qry As String = "Select TSPL_DEDUCTION_MAPPING_VLC.* from TSPL_DEDUCTION_MAPPING_VLC Where Doc_Code='" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("VLC_Code")))
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsDeductionMappingDetail
#Region "Variables"
    Public SNo As Integer
    Public TR_Code As String
    Public Doc_Code As String
    Public Deduction_Code As String
    Public DeductionName As String ''Not a table column
    Public DeductionGLAccount As String ''Not a table column
    Public Type As String
    Public Per As Double
#End Region

    Public Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsDeductionMappingDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim ii As Integer = 0
            For Each objtr As clsDeductionMappingDetail In Arr
                ii += 1
                Dim coll As New Hashtable()

                objtr.TR_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select max(TR_Code) from TSPL_DEDUCTION_MAPPING_DETAIL", trans))
                If clsCommon.myLen(objtr.TR_Code) <= 0 Then
                    objtr.TR_Code = "TR0000000000000001"
                Else
                    objtr.TR_Code = clsCommon.incval(objtr.TR_Code)
                End If
                If clsCommon.myLen(objtr.TR_Code) <= 0 Then
                    Throw New Exception("Error in code generation of Detail table")
                End If
                clsCommon.AddColumnsForChange(coll, "SNo", ii)
                clsCommon.AddColumnsForChange(coll, "Doc_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "TR_Code", objtr.TR_Code)
                clsCommon.AddColumnsForChange(coll, "Deduction_Code", objtr.Deduction_Code)
                clsCommon.AddColumnsForChange(coll, "Type", objtr.Type)
                clsCommon.AddColumnsForChange(coll, "Per", objtr.Per)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEDUCTION_MAPPING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class


Imports common
Imports System.Data.SqlClient
Public Class clsDispatchTransfer
    Public Doc_No As String = Nothing
    Public Doc_Date As String = Nothing
    Public OLD_FROM_Loc_Code As String = Nothing
    Public OLD_TO_LOC_TYPE As String = Nothing
    Public OLD_TO_Loc_Code As String = Nothing
    Public NEW_TO_LOC_TYPE As String = Nothing
    Public NEW_TO_Loc_Code As String = Nothing
    Public Challan_No As String = Nothing
    Public Comp_Code As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As String = Nothing
    Public isNewEntry As Boolean = False
    Public isPosted As Integer = 0
    Public Posting_Date As Date = Nothing

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select *  From tspl_MCC_dispatch_transfer "
            str = clsCommon.ShowSelectForm("DISPTRNS", qry, "Doc_No", whrcls, curcode, "Doc_No", isButtonClicked)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function SaveData(ByVal obj As clsDispatchTransfer, ByVal trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy")
            If obj.isNewEntry Then
                obj.Doc_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.DispatchTransfer, "", obj.OLD_FROM_Loc_Code)
                If clsCommon.myLen(obj.Doc_No) <= 0 Then
                    Throw New Exception("Error In Doc No Genertion")
                End If
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement MCC", "Tanker Location Change", obj.OLD_FROM_Loc_Code, clsCommon.myCDate(obj.Doc_Date), trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_No", clsCommon.myCstr(obj.Doc_No))
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "OLD_FROM_Loc_Code", clsCommon.myCstr(obj.OLD_FROM_Loc_Code), True)
            clsCommon.AddColumnsForChange(coll, "OLD_TO_LOC_TYPE", clsCommon.myCstr(obj.OLD_TO_LOC_TYPE))
            clsCommon.AddColumnsForChange(coll, "OLD_TO_Loc_Code", clsCommon.myCstr(obj.OLD_TO_Loc_Code))
            clsCommon.AddColumnsForChange(coll, "NEW_TO_LOC_TYPE", clsCommon.myCstr(obj.NEW_TO_LOC_TYPE))
            clsCommon.AddColumnsForChange(coll, "NEW_TO_Loc_Code", clsCommon.myCstr(obj.NEW_TO_Loc_Code))
            clsCommon.AddColumnsForChange(coll, "Challan_No", clsCommon.myCstr(obj.Challan_No))
            clsCommon.AddColumnsForChange(coll, "Modified_By", clsCommon.myCdbl(objCommonVar.CurrentUserCode))
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "isPosted", 0)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_DISPATCH_TRANSFER", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_DISPATCH_TRANSFER", OMInsertOrUpdate.Update, "TSPL_MCC_DISPATCH_TRANSFER.Doc_No='" + obj.Doc_No + "'", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function
    'Public Shared Function SaveData(ByVal StatusUpdateDocNo As String, ByVal arrProvisionDocNo As List(Of String), ByVal trans As SqlTransaction) As Boolean
    '    Try
    '        Dim issaved As Boolean = True
    '        Dim qry As String = " update tspl_provision_Entry set status='No',Status_Update_Doc_No='' where Status_Update_Doc_No='" & StatusUpdateDocNo & "' "
    '        issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '        If arrProvisionDocNo IsNot Nothing AndAlso arrProvisionDocNo.Count > 0 Then
    '            For i As Integer = 0 To arrProvisionDocNo.Count - 1
    '                qry = " update tspl_provision_Entry set status='Yes',Status_Update_Doc_No='" & StatusUpdateDocNo & "' where Doc_No='" & arrProvisionDocNo.Item(i).ToString & "' "
    '                issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            Next
    '        End If

    '        Return issaved
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    'Public Shared Function getProvisionDocNo(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of String)
    '    Dim arr As List(Of String) = New List(Of String)
    '    Dim qry As String = " select doc_No from TSPL_PROVISION_ENTRY where Status_Update_Doc_No='" & strDocNo & "'"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            arr.Add(clsCommon.myCstr(dt.Rows(i)("Doc_No")))
    '        Next
    '    End If
    '    Return arr
    'End Function
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType) As clsDispatchTransfer
        Dim obj As New clsDispatchTransfer
        Try

            Dim whrCls As String = String.Empty
            Dim qst As String = " select *   From TSPL_MCC_DISPATCH_TRANSFER   where 1=1 " & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_MCC_DISPATCH_TRANSFER.Doc_No in ('" + strCode + "')"
                Case NavigatorType.Next
                    qst += " and TSPL_MCC_DISPATCH_TRANSFER.Doc_No in (select min(Doc_No ) from TSPL_MCC_DISPATCH_TRANSFER where Doc_No  >'" + strCode + "' " & whrCls & ") "
                Case NavigatorType.First
                    qst += " and TSPL_MCC_DISPATCH_TRANSFER.Doc_No in (select MIN(Doc_No ) from TSPL_MCC_DISPATCH_TRANSFER where 1=1 " & whrCls & ") "
                Case NavigatorType.Last
                    qst += " and TSPL_MCC_DISPATCH_TRANSFER.Doc_No in (select Max(Doc_No ) from TSPL_MCC_DISPATCH_TRANSFER where 1=1 " & whrCls & ") "
                Case NavigatorType.Previous
                    qst += " and TSPL_MCC_DISPATCH_TRANSFER.Doc_No in (select Max(Doc_No ) from TSPL_MCC_DISPATCH_TRANSFER where Doc_No  <'" + strCode + "' " & whrCls & ") "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Doc_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
                obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
                obj.OLD_FROM_Loc_Code = clsCommon.myCstr(dt.Rows(0)("OLD_FROM_Loc_Code"))
                obj.OLD_TO_LOC_TYPE = clsCommon.myCstr(dt.Rows(0)("OLD_TO_LOC_TYPE"))
                obj.OLD_TO_Loc_Code = clsCommon.myCstr(dt.Rows(0)("OLD_TO_Loc_Code"))
                obj.NEW_TO_LOC_TYPE = clsCommon.myCstr(dt.Rows(0)("NEW_TO_LOC_TYPE"))
                obj.NEW_TO_Loc_Code = clsCommon.myCstr(dt.Rows(0)("NEW_TO_Loc_Code"))
                obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
                obj.isPosted = clsCommon.myCdbl(dt.Rows(0)("isPosted"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function deleteData(ByVal DocNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_MCC_DISPATCH_TRANSFER where  Doc_No='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function PostData(ByVal formId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            Dim objTran As clsDispatchTransfer = clsDispatchTransfer.getData(strDocNo, NavigatorType.Current)
            trans = clsDBFuncationality.GetTransactin()

            Dim isSaved As Boolean = True
            Dim strChallanNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Challan_No from TSPL_MCC_DISPATCH_TRANSFER where Doc_No='" & strDocNo & "'", trans))
            Dim obj As clsMccDispatch = clsMccDispatch.getData(strChallanNo, NavigatorType.Current, trans)
            Dim NEW_TO_Loc_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select NEW_TO_Loc_Code  from TSPL_MCC_DISPATCH_TRANSFER where Doc_No='" & strDocNo & "'", trans))
            Dim NEW_TO_LOC_TYPE As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select NEW_TO_LOC_TYPE   from TSPL_MCC_DISPATCH_TRANSFER where Doc_No='" & strDocNo & "'", trans))

            Dim coll As Hashtable = New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Modified_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
            clsCommon.AddColumnsForChange(coll, "Tanker_Dispatch_To", clsCommon.myCstr(NEW_TO_LOC_TYPE))
            clsCommon.AddColumnsForChange(coll, "Mcc_Or_Plant_Code", clsCommon.myCstr(NEW_TO_Loc_Code))
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            isSaved = isSaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_mcc_dispatch_challan", OMInsertOrUpdate.Update, "tspl_mcc_dispatch_challan.chalan_no='" + strChallanNo + "'", trans)
            Dim strQry As String = " update TSPL_MCC_DISPATCH_TRANSFER set isPosted='1',Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where Doc_no='" & strDocNo & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            ''GL Entry
            'Dim Amt As Double = clsCommon.myCdbl(obj.Amount)
            'Dim fromLoc As String = clsCommon.myCstr(obj.MCC_Code)
            'Dim toLocOLD As String = clsCommon.myCstr(obj.Mcc_Or_Plant_Code)
            'Dim toLocNEW As String = clsCommon.myCstr(NEW_TO_Loc_Code)
            'Dim gitLoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GIT_Location  from tspl_location_master where Location_Code='" & fromLoc & "'", trans))
            'If clsCommon.myLen(gitLoc) < 0 Then
            '    Throw New Exception(" Please map GIT Location for  " & fromLoc)
            'End If
            'Dim gitACOLD As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GL_Acc  from TSPL_ITEM_LOCATION_MAPPING  where Frm_Location ='" & fromLoc & "' and To_Location ='" & toLocOLD & "'", trans))
            'If clsCommon.myLen(gitACOLD) <= 0 Then
            '    Throw New Exception(" Please Map Transfer Location For from " & fromLoc & " To " & toLocOLD)
            'End If
            'Dim gitACNEW As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GL_Acc  from TSPL_ITEM_LOCATION_MAPPING  where Frm_Location ='" & fromLoc & "' and To_Location ='" & toLocNEW & "'", trans))
            'If clsCommon.myLen(gitACNEW) <= 0 Then
            '    Throw New Exception(" Please Map Transfer Location For from " & fromLoc & " To " & toLocNEW)
            'End If
            'Dim GItFromLocationOLD As String = clsERPFuncationality.ChangeGLAccountLocationSegment(gitACOLD, fromLoc, trans)
            'Dim GITGitLocationOLD As String = clsERPFuncationality.ChangeGLAccountLocationSegment(gitACOLD, gitLoc, trans)
            'Dim GItFromLocationNEw As String = clsERPFuncationality.ChangeGLAccountLocationSegment(gitACNEW, fromLoc, trans)
            'Dim GITGitLocationNEW As String = clsERPFuncationality.ChangeGLAccountLocationSegment(gitACNEW, gitLoc, trans)
            'Dim ArryLst As ArrayList = New ArrayList()
            'ArryLst.Add(New String() {GItFromLocationOLD, Amt * -1})
            'ArryLst.Add(New String() {GITGitLocationOLD, Amt})
            'ArryLst.Add(New String() {GItFromLocationNEw, Amt})
            'ArryLst.Add(New String() {GITGitLocationNEW, Amt * -1})
            'transportSql.FunGrnlEntryWithTrans(obj.MCC_Code, False, trans, clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MMM/yyyy"), " Against MCC Dispatch Location Transfer  -" + strDocNo + " For Milk transfer from " + fromLoc + " to " + toLocNEW, "DI-TR", "Dispatch Transfer", strDocNo, "", "C", obj.Item_Code, obj.Item_Desc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, " ", " Location Transfer OLD Location(From " & clsLocation.GetName(fromLoc, trans) & "  to " & clsLocation.GetName(toLocOLD, trans) & ") NEW LOCATION(From " & clsLocation.GetName(fromLoc, trans) & "  to " & clsLocation.GetName(toLocNEW, trans) & ") Against Dispatch Challan No: " & obj.Chalan_NO)
            ''            --------------------------
            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

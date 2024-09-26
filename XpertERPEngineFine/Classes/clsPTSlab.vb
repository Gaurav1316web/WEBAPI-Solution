'--09/05/2018--form Add By- Panch Raj against ticket : ERO/17/04/18-000087---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsPTSlab

#Region "Variables"
    Public PT_CODE As String
    Public PT_NAME As String
    Public APPLY_ON_BASIC As Integer
    Public APPLICABLE_FROM As Date
    Public STATE_CODE As String
    Public REMARKS As String
    Public ObjList As List(Of clsPTSlabDetails) = Nothing
    Dim objSlabDetails As New clsPTSlabDetails()

#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " Select PT_CODE as Code,PT_NAME as Name,APPLICABLE_FROM as [Applicable From],STATE_CODE as [State Code],REMARKS as Remarks from TSPL_PT_RULE_MASTER "
        str = clsCommon.ShowSelectForm("OTSLABFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsPTSlab
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try            
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If            
            Dim qry As String
            qry = "delete from TSPL_PT_DETAIL where PT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_PT_RULE_MASTER where PT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()           
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsPTSlab
        Dim obj As clsPTSlab = Nothing
        Dim qry As String = " select TSPL_PT_RULE_MASTER.* from TSPL_PT_RULE_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_PT_RULE_MASTER.PT_CODE = (select MIN(PT_CODE) from TSPL_PT_RULE_MASTER)"
            Case NavigatorType.Last
                qry += " and TSPL_PT_RULE_MASTER.PT_CODE = (select Max(PT_CODE) from TSPL_PT_RULE_MASTER)"
            Case NavigatorType.Next
                qry += " and TSPL_PT_RULE_MASTER.PT_CODE = (select Min(PT_CODE) from TSPL_PT_RULE_MASTER where  PT_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_PT_RULE_MASTER.PT_CODE = (select Max(PT_CODE) from TSPL_PT_RULE_MASTER where PT_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_PT_RULE_MASTER.PT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPTSlab()
            obj.PT_CODE = clsCommon.myCstr(dt.Rows(0)("PT_CODE"))
            obj.PT_NAME = clsCommon.myCstr(dt.Rows(0)("PT_NAME"))
            obj.STATE_CODE = clsCommon.myCstr(dt.Rows(0)("STATE_CODE"))
            obj.APPLICABLE_FROM = clsCommon.myCDate(dt.Rows(0)("APPLICABLE_FROM"))
            obj.REMARKS = clsCommon.myCstr(dt.Rows(0)("REMARKS"))            
            obj.ObjList = clsPTSlabDetails.GetData(obj.PT_CODE, trans)
        End If
        Return obj
    End Function

    Public Shared Function SaveData(ByVal obj As clsPTSlab, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            If clsPTSlab.SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsPTSlab, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PT_NAME", obj.PT_NAME)
            clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS)
            clsCommon.AddColumnsForChange(coll, "STATE_CODE", obj.STATE_CODE)
            clsCommon.AddColumnsForChange(coll, "APPLICABLE_FROM", clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd-MMM-yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    obj.PT_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.PTSLab, "", "")
                Else
                    If clsCommon.myLen(obj.PT_CODE) <= 0 Then
                        Throw New Exception("PT Code not entered from screen.")
                    End If
                End If
            End If
            If isNewEntry Then               
                clsCommon.AddColumnsForChange(coll, "PT_CODE", obj.PT_CODE)
                
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_PT_RULE_MASTER where PT_CODE= '" & obj.PT_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PT_RULE_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PT_RULE_MASTER", OMInsertOrUpdate.Update, "PT_CODE='" + obj.PT_CODE + "'", trans)
            End If
            clsPTSlabDetails.SaveData(obj.PT_CODE, obj.ObjList, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

End Class
Public Class clsPTSlabDetails

#Region "Variables"
    Public PT_CODE As String    
    Public _FROM As Decimal
    Public _TO As Decimal
    Public PT_AMOUNT As Decimal    
#End Region

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_PT_DETAIL where PT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsPTSlabDetails)
        Dim obj As clsPTSlabDetails = Nothing
        Dim ObjList As New List(Of clsPTSlabDetails)
        Dim qry As String = " select *  from TSPL_PT_DETAIL WHERE PT_CODE = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New clsPTSlabDetails()
                obj.PT_CODE = clsCommon.myCstr(dr("PT_CODE"))                
                obj._FROM = clsCommon.myCdbl(dr("SLAB_FROM"))
                obj._TO = clsCommon.myCdbl(dr("SLAB_TO"))
                obj.PT_AMOUNT = clsCommon.myCdbl(dr("PT_AMOUNT"))                
                ObjList.Add(obj)
            Next
        End If
        Return ObjList
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of clsPTSlabDetails), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_PT_DETAIL where PT_CODE = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As clsPTSlabDetails In ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "PT_CODE", strCode)                
                clsCommon.AddColumnsForChange(coll, "SLAB_FROM", obj._FROM)
                clsCommon.AddColumnsForChange(coll, "SLAB_TO", obj._TO)
                clsCommon.AddColumnsForChange(coll, "PT_AMOUNT", obj.PT_AMOUNT)                
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

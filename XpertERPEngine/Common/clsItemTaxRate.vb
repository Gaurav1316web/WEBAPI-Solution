Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsItemTaxRate

#Region "Variables"
    Public ITR_CODE As String    
    Public APPLICABLE_FROM As Date
    Public Description As String
    Public POSTED As String
    Public arr As List(Of clsItemTaxRateDetail)
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select ITR_CODE as Code,APPLICABLE_FROM as [Applicable From],Description,CREATED_BY as [Created By],CREATED_DATE as [Create Date],Posted  From TSPL_ITR_HEAD  "
        str = clsCommon.ShowSelectForm("ITR", qry, "Code", whrcls, curcode, "APPLICABLE_FROM", isButtonClicked)
        Return str
    End Function

    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsItemTaxRate
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
            qry = "delete from TSPL_ITR_DETAIL where ITR_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_ITR_HEAD where ITR_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '' LOG FOR SYNC DATA
            isSaved = isSaved AndAlso clsSyncHeadTables.SaveSyncDelete("TSPL_ITR_HEAD", strCode, trans)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsItemTaxRate
        Dim obj As clsItemTaxRate = Nothing
        Dim qry As String = "select ITR_CODE, APPLICABLE_FROM, DESCRIPTION  from TSPL_ITR_HEAD where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and ITR_CODE = (select MIN(ITR_CODE) from TSPL_ITR_HEAD)"
            Case NavigatorType.Last
                qry += " and ITR_CODE = (select Max(ITR_CODE) from TSPL_ITR_HEAD)"
            Case NavigatorType.Next
                qry += " and ITR_CODE = (select Min(ITR_CODE) from TSPL_ITR_HEAD where  ITR_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and ITR_CODE = (select Max(ITR_CODE) from TSPL_ITR_HEAD where ITR_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and ITR_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsItemTaxRate()
            obj.ITR_CODE = clsCommon.myCstr(dt.Rows(0)("ITR_CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))            
            obj.APPLICABLE_FROM = clsCommon.myCDate(dt.Rows(0)("APPLICABLE_FROM"))
            '' GET DETAIL DATA
            obj.arr = New List(Of clsItemTaxRateDetail)
            Dim objTr As New clsItemTaxRateDetail
            qry = " select TSPL_ITR_DETAIL.ITR_CODE,TSPL_ITR_DETAIL.ITEM_CODE,TSPL_ITEM_MASTER.ITEM_DESC,TSPL_ITR_DETAIL.TAX_GROUP_CODE,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,TSPL_ITR_DETAIL.Tax_Code,TSPL_ITR_DETAIL.RATE " & _
                  " from TSPL_ITR_DETAIL left join TSPL_ITEM_MASTER on TSPL_ITR_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE" & _
                  " LEFT JOIN TSPL_TAX_GROUP_MASTER ON TSPL_ITR_DETAIL.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code  where ITR_CODE='" & strCode & "'"
            Dim dtD As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtD.Rows.Count > 0 Then
                For Each drD As DataRow In dtD.Rows
                    objTr = New clsItemTaxRateDetail
                    objTr.ITR_CODE = obj.ITR_CODE
                    objTr.ITEM_CODE = clsCommon.myCstr(drD.Item("Item_Code"))
                    objTr.ITEM_Desc = clsCommon.myCstr(drD.Item("ITEM_Desc"))
                    objTr.TAX_GROUP_CODE = clsCommon.myCstr(drD.Item("TAX_GROUP_CODE"))
                    objTr.TAX_GROUP_Desc = clsCommon.myCstr(drD.Item("TAX_GROUP_Desc"))
                    objTr.Tax_Code = clsCommon.myCstr(drD.Item("Tax_Code"))
                    objTr.RATE = clsCommon.myCdbl(drD.Item("RATE"))
                    obj.arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function
    Public Function SaveData(ByVal obj As clsItemTaxRate, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsItemTaxRate, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        Try
            '' delete from detail table
            Dim qry As String
            qry = "delete from TSPL_ITR_DETAIL where ITR_CODE='" & obj.ITR_CODE & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()            
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.Description)
            clsCommon.AddColumnsForChange(coll, "POSTED", "N")
            clsCommon.AddColumnsForChange(coll, "APPLICABLE_FROM", clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd/MMM/yyyy"))            
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))            
            If isNewEntry Then
                qry = "select max(ITR_CODE) as ITR_CODE from TSPL_ITR_HEAD"
                obj.ITR_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(obj.ITR_CODE) <= 0 Then
                    obj.ITR_CODE = "ITR" & "/000001"
                Else
                    obj.ITR_CODE = clsCommon.incval(obj.ITR_CODE)
                End If

                clsCommon.AddColumnsForChange(coll, "ITR_CODE", obj.ITR_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                qry = "SELECT Count(*) FROM TSPL_ITR_HEAD where ITR_CODE= '" & obj.ITR_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITR_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITR_HEAD", OMInsertOrUpdate.Update, "ITR_CODE='" + obj.ITR_CODE + "'", trans)
            End If
            clsItemTaxRateDetail.saveData(obj, obj.ITR_CODE, trans)
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
            Dim obj As clsItemTaxRate = clsItemTaxRate.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.ITR_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = "Y") Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = "Update TSPL_ITR_HEAD set POSTED='Y', MODIFIED_DATE='" & strPostDate & "',Modified_By='" + objCommonVar.CurrentUserCode + "' where ITR_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetApplicableFrom(ByVal strCode As String, ByVal trans As SqlTransaction) As Date
        Dim qry As String = "select APPLICABLE_FROM  from TSPL_ITR_HEAD where ITR_CODE ='" + strCode + "' "
        Return clsCommon.myCDate(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    
End Class
Public Class clsItemTaxRateDetail

#Region "Variables"
    Public ITR_CODE As String
    Public TAX_GROUP_CODE As String
    Public TAX_GROUP_Desc As String
    'Public Description As String
    Public ITEM_CODE As String
    Public ITEM_Desc As String
    Public Tax_Code As String
    Public RATE As String
#End Region
    Public Shared Function saveData(ByVal obj As clsItemTaxRate, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If obj IsNot Nothing Then
                For Each objTr As clsItemTaxRateDetail In obj.arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "ITR_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "TAX_GROUP_CODE", objTr.TAX_GROUP_CODE)                    
                    clsCommon.AddColumnsForChange(coll, "ITEM_CODE", objTr.ITEM_CODE)
                    clsCommon.AddColumnsForChange(coll, "Tax_Code", objTr.Tax_Code)                
                    clsCommon.AddColumnsForChange(coll, "Rate", objTr.RATE)                    
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITR_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
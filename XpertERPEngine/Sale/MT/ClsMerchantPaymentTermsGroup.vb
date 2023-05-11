'--------Created By Richa 10/12/2014 Against Ticket No BM00000004983

Imports common
Imports System.Data.SqlClient
Public Class ClsMerchantPaymentTermsGroup
#Region "variables"
    Public Group_Code As String = Nothing
    Public Description As String = Nothing
  
    Public arrPaymentTermsgroupDetail As List(Of ClsMerchantPaymentTermsGroupDetail) = Nothing

#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code as [Code],TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Description, TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Created_By as [Created By] ,convert(date,TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Created_Date,103) as [Created Date],TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Modified_By as [Modified By],convert(date,TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Modified_Date,103) as [Modified Date] ,TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Comp_Code as [Comp Code] from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT"
        str = clsCommon.ShowSelectForm("MTPTGroup", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function SaveData(ByVal obj As ClsMerchantPaymentTermsGroup, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            qry = "delete from TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT where Group_Code='" & obj.Group_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
          
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Group_Code", obj.Group_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_TERMS_GROUP_MASTER_MT", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_TERMS_GROUP_MASTER_MT", OMInsertOrUpdate.Update, "TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code='" + obj.Group_Code + "'", trans)
            End If
            ClsMerchantPaymentTermsGroupDetail.saveData(obj.arrPaymentTermsgroupDetail, obj.Group_Code, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsMerchantPaymentTermsGroup
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsMerchantPaymentTermsGroup
        Dim obj As ClsMerchantPaymentTermsGroup = Nothing
        Dim Arr As List(Of ClsMerchantPaymentTermsGroup) = Nothing
        Dim qry As String = "Select Group_Code,Description FROM TSPL_PAYMENT_TERMS_GROUP_MASTER_MT where 1=1 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code = (select MIN(Group_Code) from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code = (select Max(Group_Code) from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT WHERE 1=1 " + whrclas + "  )"
            Case NavigatorType.Current
                qry += " and TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code = '" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code = (select Min(Group_Code) from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT where Group_Code>'" + strCode + "' " + whrclas + " )"
            Case NavigatorType.Previous
                qry += " and TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code = (select Max(Group_Code) from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT where Group_Code<'" + strCode + "' " + whrclas + " )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsMerchantPaymentTermsGroup()
            obj.Group_Code = clsCommon.myCstr(dt.Rows(0)("Group_Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.arrPaymentTermsgroupDetail = ClsMerchantPaymentTermsGroupDetail.getData(obj.Group_Code, trans)
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            Dim qry As String = ""
            qry = "delete from TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT where Group_Code='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT where Group_Code='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
   
End Class


Public Class ClsMerchantPaymentTermsGroupDetail
    Public Group_Code As String = String.Empty
    Public Terms_Code As String = String.Empty
   
    Public Shared Function saveData(ByVal arrObj As List(Of ClsMerchantPaymentTermsGroupDetail), ByVal strdocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then

                For Each obj As ClsMerchantPaymentTermsGroupDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Group_Code", strdocNo)
                    clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of ClsMerchantPaymentTermsGroupDetail)
        Try
            Dim arrObj As List(Of ClsMerchantPaymentTermsGroupDetail) = Nothing
            Dim obj As ClsMerchantPaymentTermsGroupDetail = Nothing
            Dim qry As String = "select * from TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT where Group_Code='" & strDocNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of ClsMerchantPaymentTermsGroupDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New ClsMerchantPaymentTermsGroupDetail()
                    obj.Group_Code = clsCommon.myCstr(dt.Rows(i)("Group_Code"))
                    obj.Terms_Code = clsCommon.myCstr(dt.Rows(i)("Terms_Code"))
                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

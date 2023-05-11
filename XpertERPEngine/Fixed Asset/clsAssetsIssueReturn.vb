Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsAssetsIssueReturn


#Region "Variables"

    Public IssueCode As String
    Public Type As String
    Public IssueDate As Date
    Public IssueTo As String
    Public IssueToName As String
    Public Remark As String
    Public Against_Issue_Code As String
    Public ReturnCode As String
    Public status As String
    Public ObjList As List(Of clsAssetsIssueReturnDetail)
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending

#End Region

    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsAssetsIssueReturn
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
            qry = "delete from TSPL_AssetIssueReturnDetail where IssueCode ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_AssetIssueReturn where IssueCode ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsAssetsIssueReturn
        Try

            Dim obj As New clsAssetsIssueReturn()
            Dim objtr As New clsAssetsIssueReturnDetail()

            Dim qry As String = " SELECT TSPL_AssetIssueReturn.*,Tspl_USER_MASTER.User_Name " _
                                & " FROM TSPL_AssetIssueReturn " _
                                & " left outer JOIN Tspl_USER_MASTER ON TSPL_AssetIssueReturn.IssueTo=Tspl_USER_MASTER.User_Code WHERE 1=1 "
            Select Case NavType
                Case NavigatorType.First
                    qry += " and IssueCode = (select MIN(IssueCode) from TSPL_AssetIssueReturn where TSPL_AssetIssueReturn.Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' )"
                Case NavigatorType.Last
                    qry += " and IssueCode = (select Max(IssueCode) from TSPL_AssetIssueReturn where TSPL_AssetIssueReturn.Comp_Code = '" + objCommonVar.CurrentCompanyCode + "')"
                Case NavigatorType.Next
                    qry += " and IssueCode = (select Min(IssueCode) from TSPL_AssetIssueReturn where  IssueCode>'" + strCode + "' and TSPL_AssetIssueReturn.Comp_Code = '" + objCommonVar.CurrentCompanyCode + "')"
                Case NavigatorType.Previous
                    qry += " and IssueCode = (select Max(IssueCode) from TSPL_AssetIssueReturn where IssueCode<'" + strCode + "' and TSPL_AssetIssueReturn.Comp_Code = '" + objCommonVar.CurrentCompanyCode + "')"
                Case NavigatorType.Current
                    qry += " and IssueCode = '" + strCode + "' and TSPL_AssetIssueReturn.Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.IssueCode = clsCommon.myCstr(dt.Rows(0)("IssueCode"))
                obj.IssueDate = clsCommon.myCstr(dt.Rows(0)("IssueDate"))
                obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
                obj.IssueTo = clsCommon.myCstr(dt.Rows(0)("IssueTo"))
                obj.IssueToName = clsCommon.myCstr(dt.Rows(0)("User_Name"))
                obj.Remark = clsCommon.myCstr(dt.Rows(0)("Remark"))
                obj.Against_Issue_Code = clsCommon.myCstr(dt.Rows(0)("Against_Issue_Code"))
                obj.ObjList = objtr.GetData(obj.IssueCode, trans)
                'obj.status = clsCommon.myCstr(obj.ObjList.Item(0).Status)
                obj.POSTED = IIf(clsCommon.myCstr(dt.Rows(0).Item("Posted")) = "1", ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Function SaveData(ByVal obj As clsAssetsIssueReturn, ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = True
        Dim qry As String = ""
        Dim strAssetCode As String = ""
        Try
            'If isNewEntry Then
            '    If strCode = "" Then
            '        qry = " select  MAX(IssueCode) from TSPL_AssetIssueReturn"
            '        strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            '        If clsCommon.myLen(strCode) <= 0 Then
            '            strCode = "AIR000000001"
            '        Else
            '            strCode = clsCommon.incval(strCode)
            '        End If
            '    End If
            '    obj.IssueCode = strCode
            'End If
            'If (clsCommon.myLen(obj.IssueCode) <= 0) Then
            '    Throw New Exception("Error in Document Code Generation")
            'End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            clsCommon.AddColumnsForChange(coll, "IssueTo", IIf(obj.IssueTo Is Nothing, DBNull.Value, obj.IssueTo))
            clsCommon.AddColumnsForChange(coll, "Remark", obj.Remark)
            clsCommon.AddColumnsForChange(coll, "IssueDate", clsCommon.GetPrintDate(obj.IssueDate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "MODIFY_BY", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Against_Issue_Code", obj.Against_Issue_Code, True)
            clsCommon.AddColumnsForChange(coll, "MODIFY_DATE", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            Dim query As String = " select * from TSPL_AssetIssueReturn  where IssueCode='" + obj.IssueCode + "'"
            Dim dtAssetIssueReturn As DataTable = clsDBFuncationality.GetDataTable(query, trans)
            If dtAssetIssueReturn.Rows.Count > 0 And obj.Type = "Issue" Then
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_AssetIssueReturn", OMInsertOrUpdate.Update, "TSPL_AssetIssueReturn.IssueCode='" + obj.IssueCode + "'", trans)
            Else
                'If obj.Type = "Return" Then
                '    Dim query1 As String = "update TSPL_AssetIssueReturn set Type='Return' where IssueCode='" + strCode + "' "
                '    clsDBFuncationality.ExecuteNonQuery(query1, trans)
                'End If
                qry = " select  MAX(IssueCode) from TSPL_AssetIssueReturn"
                strAssetCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(strAssetCode) <= 0 Then
                    strAssetCode = "AIR000000001"
                Else
                    strAssetCode = clsCommon.incval(strAssetCode)
                    obj.ReturnCode = strAssetCode

                    If clsCommon.myLen(strCode) <= 0 Then
                        obj.IssueCode = strAssetCode
                    End If

                End If
                clsCommon.AddColumnsForChange(coll, "IssueCode", strAssetCode)
                clsCommon.AddColumnsForChange(coll, "CREATE_BY", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "CREATE_DATE", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_AssetIssueReturn", OMInsertOrUpdate.Insert, "", trans)
            End If
            isSaved = isSaved AndAlso clsAssetsIssueReturnDetail.SaveData(obj.ObjList, strAssetCode, strCode, obj.Type, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
End Class

Public Class clsAssetsIssueReturnDetail
#Region "Variables"


    Public IssueCode As String
    Public Asset_Code As String
    Public Asset_Description As String
    Public Asset_Sno As String
    Public Qty As Double
    Public ReturnedQty As Double
    Public Asset_Type_Code As String
    Public Asset_Type_desc As String
    Public Status As String
    Public ReturnCode As String
    Public Shared ObjList As List(Of clsAssetsIssueReturnDetail)
#End Region

    Public Shared Function SaveData(ByVal Arr As List(Of clsAssetsIssueReturnDetail), ByVal strDocNo As String, ByVal AgainstIssueNo As String, ByVal type As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "Delete from TSPL_AssetIssueReturnDetail where IssueCode='" + strDocNo + "' "
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsAssetsIssueReturnDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "IssueCode", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Asset_Code", obj.Asset_Code)
                clsCommon.AddColumnsForChange(coll, "Asset_Description", obj.Asset_Description)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Asset_Type_Code", obj.Asset_Type_Code)
                If type = "Return" Then
                    Dim query As String = "update TSPL_AssetIssueReturnDetail set IsReturn=1, AGAINSTISSUENO='" + strDocNo + "' where IssueCode='" + AgainstIssueNo + "' "
                    clsDBFuncationality.ExecuteNonQuery(query, trans)
                    clsCommon.AddColumnsForChange(coll, "IsReturn", 1)
                    clsCommon.AddColumnsForChange(coll, "AGAINSTISSUENO", obj.ReturnCode)
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_AssetIssueReturnDetail", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsAssetsIssueReturnDetail)
        Dim objtr As clsAssetsIssueReturnDetail
        ObjList = New List(Of clsAssetsIssueReturnDetail)

        Dim qry As String = " "
        qry = "select IssueCode, Asset_Code, Asset_Description,AGAINSTISSUENO as ReturnCode ,Qty as IssuedQty, " & _
                       "(select isnull(sum(Qty),0)Qty from TSPL_AssetIssueReturnDetail AssIss where TSPL_AssetIssueReturnDetail.AgainstIssueNo= AssIss.IssueCode " & _
                       "and AssIss.Asset_Code=TSPL_AssetIssueReturnDetail.Asset_Code)as ReturnQty, Asset_Type_Code " & _
                       "from TSPL_AssetIssueReturnDetail where IssueCode='" + strCode + "' "

        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsAssetsIssueReturnDetail
                objtr.IssueCode = clsCommon.myCstr(dr("IssueCode"))
                objtr.ReturnCode = clsCommon.myCstr(dr("ReturnCode"))
                objtr.Asset_Code = clsCommon.myCstr(dr("Asset_Code"))
                objtr.Qty = clsCommon.myCdbl(dr("IssuedQty"))
                objtr.ReturnedQty = clsCommon.myCdbl(dr("ReturnQty"))
                objtr.Asset_Description = clsCommon.myCstr(dr("Asset_Description"))
                'objtr.Asset_Sno = clsCommon.myCstr(dr("Serial_No"))
                objtr.Asset_Type_Code = clsCommon.myCstr(dr("Asset_Type_Code"))
                'objtr.Asset_Type_desc = clsCommon.myCstr(dr("Asset_Type_Description"))
                If (objtr.Qty - objtr.ReturnedQty) = 0 Then
                    objtr.Status = "Returned"
                Else
                    objtr.Status = "Issued"
                End If
                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function

End Class

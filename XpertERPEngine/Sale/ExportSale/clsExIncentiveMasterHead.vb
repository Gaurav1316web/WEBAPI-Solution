Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsExIncentiveMasterHead

#Region "Variables"
    Public Doc_Code As String
    Public Description As String
    Public Doc_Date As Date
    Public Type As String = Nothing
    Public Pers As Decimal = Nothing

    Public ObjList As List(Of clsExIncentiveDetail) = Nothing
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsExIncentiveMasterHead
        Try
            Return GetData(strCode, NavType, Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsExIncentiveMasterHead, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso SaveData(obj, isNewEntry, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsExIncentiveMasterHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        Try
            If isNewEntry Then
                obj.Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Doc_Date, clsDocType.ExportIncentiveMaster, "", "")
            End If

            Dim qry As String = "DELETE FROM TSPL_ex_INCENTIVE_DETAIL WHERE Doc_Code='" + obj.Doc_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If (clsCommon.myLen(obj.Doc_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation.")
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode, True)
            clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.Doc_Code)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            clsCommon.AddColumnsForChange(coll, "Pers", obj.Pers)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Ex_Incentive_Head", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Ex_Incentive_Head", OMInsertOrUpdate.Update, "TSPL_Ex_Incentive_Head.doc_code='" + obj.Doc_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsExIncentiveDetail.SaveData(obj.Doc_Code, obj.ObjList, trans)


            Return True
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

    End Function

    Public Shared Function SaveIMPORTData(ByVal obj As clsExIncentiveMasterHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        Try
            If isNewEntry AndAlso clsCommon.myLen(obj.Doc_Code) <= 0 Then
                obj.Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Doc_Date, clsDocType.ExportIncentiveMaster, "", "")
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode, True)
            clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.Doc_Code)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            clsCommon.AddColumnsForChange(coll, "Pers", obj.Pers)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Ex_Incentive_Head", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Ex_Incentive_Head", OMInsertOrUpdate.Update, "TSPL_Ex_Incentive_Head.doc_code='" + obj.Doc_Code + "'", trans)
            End If

            '=============detail============================
            If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
                For Each objtr As clsExIncentiveDetail In obj.ObjList
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "LINE_NO", objtr.LINE_NO)
                    clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.Doc_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_code, True)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)
                    clsCommon.AddColumnsForChange(coll, "Incentive_Per", objtr.incentive_Per)
                    clsCommon.AddColumnsForChange(coll, "Incentive_Amount", objtr.Incentive_Amount)
                    clsCommon.AddColumnsForChange(coll, "type", objtr.Type)

                    Dim qry As String = "DELETE FROM TSPL_ex_INCENTIVE_DETAIL WHERE Doc_Code='" + obj.Doc_Code + "' and Item_Code='" + objtr.Item_code + "' and Unit_Code='" + objtr.Unit_Code + "' and type='" + objtr.Type + "'"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ex_INCENTIVE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            '===============================================
            Return isSaved
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsExIncentiveMasterHead
        Dim obj As New clsExIncentiveMasterHead()
        Try
            obj.ObjList = New List(Of clsExIncentiveDetail)

            Dim qry As String = "select TSPL_Ex_Incentive_Head.Doc_Code ,TSPL_Ex_Incentive_Head.Description ,TSPL_Ex_Incentive_Head.Doc_Date,TSPL_Ex_Incentive_Head.type,TSPL_Ex_Incentive_Head.pers from TSPL_Ex_Incentive_Head where 2=2"

            Select Case NavType
                Case NavigatorType.First
                    qry += " AND Doc_Code = (select MIN(Doc_Code) from TSPL_Ex_Incentive_Head)"
                Case NavigatorType.Last
                    qry += " AND Doc_Code = (select Max(Doc_Code) from TSPL_Ex_Incentive_Head)"
                Case NavigatorType.Next
                    qry += " AND Doc_Code = (select Min(Doc_Code) from TSPL_Ex_Incentive_Head where  Doc_Code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " AND Doc_Code = (select Max(Doc_Code) from TSPL_Ex_Incentive_Head where Doc_Code<'" + strCode + "')"
                Case NavigatorType.Current
                    qry += " AND Doc_Code = '" + strCode + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Doc_Code = clsCommon.myCstr(dt.Rows(0)("Doc_Code"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
                obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
                obj.Type = clsCommon.myCstr(dt.Rows(0)("type"))
                obj.Pers = clsCommon.myCdbl(dt.Rows(0)("pers"))

                obj.ObjList = clsExIncentiveDetail.GetIncentiveDetail(obj, trans)
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso DeleteData(strCode, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = True

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete.")
            End If

            Dim qry As String
            qry = "delete from TSPL_Ex_Incentive_detail where Doc_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_Ex_Incentive_Head where Doc_Code ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "select TSPL_Ex_Incentive_Head.Doc_Code as [Code],TSPL_Ex_Incentive_Head.Description  as [Description],TSPL_Ex_Incentive_Head.Doc_Date as [Date],(case when Type='FOB' then 'Duty Draw Back-1% on FOB' else case when type='BOF' then 'Basic Ocean Freight-25%' else case when type='BOK' then 'Basic Ocean Freight per KG' else case when type='VKGUY' then 'VisheshKrishi Gram udyogYojana-5% on FOB' else 'None' end end end end) as Type,TSPL_Ex_Incentive_Head.Pers from TSPL_Ex_Incentive_Head "
        Dim str As String = ""

        str = clsCommon.ShowSelectForm("Incentive", qry, "Code", whrCls, currCode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function GetTypeName(ByVal Type_Code As String) As String
        Dim str As String = Nothing

        If clsCommon.CompairString(str, "FOB") = CompairStringResult.Equal Then
            str = "Duty Draw Back-1% on FOB"
        ElseIf clsCommon.CompairString(str, "BOF") = CompairStringResult.Equal Then
            str = "Basic Ocean Freight-25%"
        ElseIf clsCommon.CompairString(str, "BOK") = CompairStringResult.Equal Then
            str = "Basic Ocean Freight per KG"
        ElseIf clsCommon.CompairString(str, "VKGUY") = CompairStringResult.Equal Then
            str = "VisheshKrishi Gram udyogYojana-5% on FOB"
        End If

        Return str
    End Function

    Public Shared Function CheckValidCode(ByVal Doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = "select count(*) from TSPL_Ex_Incentive_Head where Doc_Code='" + Doc_No + "'"
        Dim count As Integer = clsDBFuncationality.getSingleValue(qry, trans)
        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class

Public Class clsExIncentiveDetail
#Region "Variables"
    '' grid columns

    Public Doc_Code As String
    Public LINE_NO As Integer
    Public Item_code As String
    Public Item_Des As String
    Public Item_Type As String
    Public Unit_Code As String
    Public Type As String = Nothing
    Public incentive_Per As Decimal
    Public Incentive_Amount As Decimal
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsExIncentiveDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsExIncentiveDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO)
                    clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_code, True)
                    clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                    clsCommon.AddColumnsForChange(coll, "Incentive_Per", obj.incentive_Per)
                    clsCommon.AddColumnsForChange(coll, "Incentive_Amount", obj.Incentive_Amount)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ex_INCENTIVE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetIncentiveDetail(ByVal obj As clsExIncentiveMasterHead, ByVal trans As SqlTransaction) As List(Of clsExIncentiveDetail)
        Dim objtr As New clsExIncentiveDetail()
        Dim DT As New DataTable()
        Try
            Dim qry As String = ""
            qry = "SELECT TSPL_Ex_Incentive_Detail.Doc_Code ,TSPL_Ex_Incentive_Detail.LINE_NO ,TSPL_Ex_Incentive_Detail.Item_Code,TSPL_ITEM_MASTER .Item_Desc ,TSPL_ITEM_MASTER.Item_Type  ,TSPL_Ex_Incentive_Detail.Unit_Code ,TSPL_Ex_Incentive_Detail.Incentive_Per ,TSPL_Ex_Incentive_Detail.Incentive_Amount,TSPL_Ex_Incentive_Detail.Type  FROM TSPL_Ex_Incentive_Detail "
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code = TSPL_Ex_Incentive_Detail.Item_Code   WHERE Doc_Code='" & obj.Doc_Code & "' ORDER BY LINE_NO"
            DT = clsDBFuncationality.GetDataTable(qry, trans)
            objtr = New clsExIncentiveDetail
            Dim ObjList As New List(Of clsExIncentiveDetail)
            If (DT IsNot Nothing AndAlso DT.Rows.Count > 0) Then
                For Each dr As DataRow In DT.Rows
                    objtr = New clsExIncentiveDetail

                    objtr.LINE_NO = clsCommon.myCdbl(dr("LINE_NO"))
                    objtr.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                    objtr.Item_code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.Item_Des = clsCommon.myCstr(dr("Item_Desc"))
                    objtr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                    objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    objtr.incentive_Per = clsCommon.myCdbl(dr("Incentive_Per"))
                    objtr.Incentive_Amount = clsCommon.myCdbl(dr("Incentive_Amount"))
                    objtr.Type = clsCommon.myCstr(dr("Type"))

                    ObjList.Add(objtr)
                Next
            End If
            Return ObjList
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            DT = Nothing
            objtr = Nothing
        End Try
    End Function


End Class

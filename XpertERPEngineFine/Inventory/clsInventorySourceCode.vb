Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class clsInventorySourceCode
#Region "Variables"
    Public Code As String = Nothing
    Public Name As String = Nothing
    Public InOutType As String = Nothing
    Public Type As String = Nothing
    Public In_Category As String = Nothing
    Public Out_Category As String = Nothing
    Public Sequence As Integer = 0
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = " select Code,Name,TSPL_INVENTORY_SOURCE_CODE.Created_By as [Created By] ,Convert(varchar,TSPL_INVENTORY_SOURCE_CODE.Created_Date,103) as [Created Date] ,TSPL_INVENTORY_SOURCE_CODE.Modified_By as [Modified By] ,Convert(varchar,TSPL_INVENTORY_SOURCE_CODE.Modified_Date,103) as [Modified Date],Sequence  From TSPL_INVENTORY_SOURCE_CODE "
        Return clsCommon.ShowSelectForm("InvSourceCode", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsInventorySourceCode
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_INVENTORY_SOURCE_CODE where Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsInventorySourceCode
        Dim obj As clsInventorySourceCode = Nothing
        Dim qry As String = "select * from TSPL_INVENTORY_SOURCE_CODE where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Code = (select MIN(Code) from TSPL_INVENTORY_SOURCE_CODE)"
            Case NavigatorType.Last
                qry += " and Code = (select Max(Code) from TSPL_INVENTORY_SOURCE_CODE)"
            Case NavigatorType.Next
                qry += " and Code = (select Min(Code) from TSPL_INVENTORY_SOURCE_CODE where  Code>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and Code = (select Max(Code) from TSPL_INVENTORY_SOURCE_CODE where Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsInventorySourceCode()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
            obj.InOutType = clsCommon.myCstr(dt.Rows(0)("InOutType"))
            obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
            obj.Sequence = clsCommon.myCdbl(dt.Rows(0)("Sequence"))
            obj.In_Category = clsCommon.myCstr(dt.Rows(0)("In_Category"))
            obj.Out_Category = clsCommon.myCstr(dt.Rows(0)("Out_Category"))
        End If
        Return obj
    End Function

    Public Shared Function SaveData(ByVal obj As clsInventorySourceCode, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
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

    Public Shared Function SaveData(ByVal obj As clsInventorySourceCode, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "InOutType", obj.InOutType)
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            If clsCommon.CompairString(obj.InOutType, "In") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.InOutType, "All") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "In_Category", obj.In_Category)
            Else
                clsCommon.AddColumnsForChange(coll, "In_Category", Nothing)
            End If
            If clsCommon.CompairString(obj.InOutType, "Out") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.InOutType, "All") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Out_Category", obj.Out_Category)
            Else
                clsCommon.AddColumnsForChange(coll, "Out_Category", Nothing)
            End If

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Sequence", obj.Sequence)
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_INVENTORY_SOURCE_CODE where Code='" & obj.Code & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.InventorySourceCode, "", "")
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVENTORY_SOURCE_CODE", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVENTORY_SOURCE_CODE", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = "select Code from TSPL_INVENTORY_SOURCE_CODE where Code ='" + Code + "'   "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
End Class

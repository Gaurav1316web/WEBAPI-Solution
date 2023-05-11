Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsDiscountMaster

#Region "Variables"
    Public Code As String
    Public Description As String
    Public Account_Code As String
    Public Account_Description As String
    Public Discount As String
    Public Vsnd As String
    Public Other As String
    Public Sampling As String
    Public DiscountCategory As String
    Public skuwise As String
    Public IsOpening As Boolean

#End Region
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsDiscountMaster
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim obj As clsDiscountMaster = clsDiscountMaster.GetData(strCode, NavigatorType.Current)

            Dim qry As String
            qry = "delete from TSPL_Discount_Master where Code='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDiscountMaster
        Dim obj As clsDiscountMaster = Nothing
        Dim qry As String = "select Code, Description, Account_Code, Account_Description, Discount, VSND_Type, Discount_category_Code, Other ,skuwise,Sampling,IsOpening from TSPL_Discount_Master where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Code = (select MIN(Code) from TSPL_Discount_Master)"
            Case NavigatorType.Last
                qry += " and Code = (select Max(Code) from TSPL_Discount_Master)"
            Case NavigatorType.Next
                qry += " and Code = (select Min(Code) from TSPL_Discount_Master where  Code>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and Code = (select Max(Code) from TSPL_Discount_Master where Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDiscountMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Account_Code = clsCommon.myCstr(dt.Rows(0)("Account_Code"))
            obj.Account_Description = clsCommon.myCstr(dt.Rows(0)("Account_Description"))
            obj.Discount = clsCommon.myCstr(dt.Rows(0)("Discount"))
            obj.Vsnd = clsCommon.myCstr(dt.Rows(0)("VSND_Type"))
            obj.DiscountCategory = clsCommon.myCstr(dt.Rows(0)("Discount_category_Code"))
            obj.Other = clsCommon.myCstr(dt.Rows(0)("Other"))
            obj.skuwise = clsCommon.myCstr(dt.Rows(0)("skuwise"))
            obj.Sampling = clsCommon.myCstr(dt.Rows(0)("Sampling"))
            obj.IsOpening = clsCommon.myCBool(dt.Rows(0)("IsOpening"))
        End If
        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsDiscountMaster, ByVal isNewEntry As Boolean, ByVal DBArrList As List(Of String)) As Boolean
        Dim isSaved As Boolean = True
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            'Dim DBNameList As New List(Of String)=DBArrList
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Account_Code", obj.Account_Code)
            clsCommon.AddColumnsForChange(coll, "Account_Description", obj.Account_Description)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Discount ", obj.Discount)
            clsCommon.AddColumnsForChange(coll, "VSND_Type ", obj.Vsnd)
            clsCommon.AddColumnsForChange(coll, "discount_category_Code ", obj.DiscountCategory, True)
            clsCommon.AddColumnsForChange(coll, "Other ", obj.Other)
            clsCommon.AddColumnsForChange(coll, "Sampling ", obj.Sampling)
            clsCommon.AddColumnsForChange(coll, "skuwise ", obj.skuwise)
            clsCommon.AddColumnsForChange(coll, "IsOpening", obj.IsOpening)
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_DISCOUNT_MASTER where Code='" & obj.Code & "'")
                    If ChkNewEntry = 0 Then
                        obj.Code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.DiscountMaster, "", "")
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_DISCOUNT_MASTER where Code= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    'isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DISCOUNT_MASTER", OMInsertOrUpdate.Insert, "")
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll, DBArrList, "TSPL_DISCOUNT_MASTER", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If

            Else
                'isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DISCOUNT_MASTER", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'")
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll, DBArrList, "TSPL_DISCOUNT_MASTER", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function Check_IsOpening(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim Res As Boolean = False
        If clsCommon.myLen(strCode) > 0 Then
            Dim qry As String = "select IsOpening from TSPL_Discount_Master "
            qry += " WHERE Code = '" + strCode + "'"
            Res = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
        End If
        Return Res
    End Function

End Class

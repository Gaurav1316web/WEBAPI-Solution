Imports System.Data.SqlClient
Imports common
''By balwinder BHA/21/06/18-000068 on 26/06/2018
Public Class clsMilkRejectType

#Region "Variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Applicable_On As Integer
    Public Applicable_Per As Decimal
    Public Item_Code As String = Nothing
    Public Type As String = Nothing
    Public SNo As Integer
    Public Include_In_DBT As Boolean
#End Region

    Public Shared Function SaveData(ByVal obj As clsMilkRejectType) As Boolean
        Dim qry As String = ""
        Dim isNewEntry As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry1 As String = clsDBFuncationality.getSingleValue("Select Code from TSPL_MILK_REJECT_TYPE where code='" & obj.Code & "' ", trans)
        If clsCommon.myLen(qry1) > 0 Then
            isNewEntry = False
        End If
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Include_In_DBT", IIf(obj.Include_In_DBT, 1, 0), True)
            clsCommon.AddColumnsForChange(coll, "Applicable_On", obj.Applicable_On)
            clsCommon.AddColumnsForChange(coll, "Applicable_Per", obj.Applicable_Per)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type, True)
            clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_REJECT_TYPE", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_MILK_REJECT_TYPE", "Code", trans)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_REJECT_TYPE", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMilkRejectType
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal tran As SqlTransaction) As clsMilkRejectType
        Dim obj As clsMilkRejectType = Nothing
        Dim qry As String = "select TSPL_MILK_REJECT_TYPE.* from TSPL_MILK_REJECT_TYPE   where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MILK_REJECT_TYPE.Code = (select MIN(Code) from TSPL_MILK_REJECT_TYPE WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_MILK_REJECT_TYPE.Code = (select Max(Code) from TSPL_MILK_REJECT_TYPE WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_MILK_REJECT_TYPE.Code = (select TOP 1 Code from TSPL_MILK_REJECT_TYPE WHERE 1=1 " + whrclas + " and Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_MILK_REJECT_TYPE.Code = (select Min(Code) from TSPL_MILK_REJECT_TYPE where Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_MILK_REJECT_TYPE.Code = (select Max(Code) from TSPL_MILK_REJECT_TYPE where Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMilkRejectType()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            obj.Applicable_On = clsCommon.myCdbl(dt.Rows(0)("Applicable_On"))
            obj.Applicable_Per = clsCommon.myCdbl(dt.Rows(0)("Applicable_Per"))
            obj.Include_In_DBT = (clsCommon.myCdbl(dt.Rows(0)("Include_In_DBT")) = 1)
            obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
            obj.SNo = clsCommon.myCdbl(dt.Rows(0)("SNo"))
        End If
        Return obj
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_MILK_REJECT_TYPE where Code='" + strCode + "'"))
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = ""
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            qry = "select top 1 Reject_Type from tspl_milk_reject_detail where Reject_Type='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Reject Type - " + strCode + " is used in milk reject so can not delete it.")
            End If

            qry = "Delete from TSPL_MILK_REJECT_TYPE where TSPL_MILK_REJECT_TYPE.Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            tran.Commit()
        Catch err As Exception
            tran.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_MILK_REJECT_TYPE.Code,TSPL_MILK_REJECT_TYPE.Description,TSPL_MILK_REJECT_TYPE.Applicable_Per as [Applicable %],TSPL_MILK_REJECT_TYPE.Item_Code as [Item] from TSPL_MILK_REJECT_TYPE "
        str = clsCommon.ShowSelectForm("canmasterFnd", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function

    Public Shared Function getFinderObeject(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As clsMilkRejectType
        Dim obj As clsMilkRejectType = Nothing
        Dim strCode As String = getFinder(whrcls, curcode, isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            obj = GetData(strCode, NavigatorType.Current)
        End If
        Return obj
    End Function

    Public Shared Function GetCombo(ByVal ShowSelectRow As Boolean) As DataTable
        Dim qry As String = "select Code,description as Name from TSPL_MILK_REJECT_TYPE"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.NewRow
            dr("Code") = ""
            dr("Name") = "Select"
            dt.Rows.InsertAt(dr, 0)
        End If
        Return dt
    End Function

    Public Shared Function GetApplicableOn(ByVal Code As String, ByVal tran As SqlTransaction) As Integer
        '-1-Good;0-%;2-Rate;3-FATKg;4-SNFKg
        Dim intRet As Integer = -1
        Dim qry As String = "select Applicable_On from TSPL_MILK_REJECT_TYPE where Code='" + Code + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            intRet = clsCommon.myCDecimal(dt.Rows(0)("Applicable_On"))
        End If
        Return intRet
    End Function
End Class



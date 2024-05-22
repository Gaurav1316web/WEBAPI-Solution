Imports System.Data.SqlClient
Imports common
Public Class clsVSSDayWiseIncentive

#Region "Variables"
    Public Day_Wise_Incentive_Code As String = Nothing
    Public Day_Wise_Incentive_Name As String = Nothing

    Public Day_Wise_Incentive_From_1 As Decimal
    Public Day_Wise_Incentive_To_1 As Decimal
    Public Day_Wise_Incentive_Rate_1 As Decimal

    Public Day_Wise_Incentive_From_2 As Decimal
    Public Day_Wise_Incentive_To_2 As Decimal
    Public Day_Wise_Incentive_Rate_2 As Decimal

    Public Day_Wise_Incentive_From_3 As Decimal
    Public Day_Wise_Incentive_To_3 As Decimal
    Public Day_Wise_Incentive_Rate_3 As Decimal

    Public Day_Wise_Incentive_From_4 As Decimal
    Public Day_Wise_Incentive_To_4 As Decimal
    Public Day_Wise_Incentive_Rate_4 As Decimal

    Public Day_Wise_Incentive_From_5 As Decimal
    Public Day_Wise_Incentive_To_5 As Decimal
    Public Day_Wise_Incentive_Rate_5 As Decimal
#End Region

    Public Shared Function SaveData(ByVal obj As clsVSSDayWiseIncentive) As Boolean
        Dim qry As String = ""
        Dim isNewEntry As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry1 As String = clsDBFuncationality.getSingleValue("Select Day_Wise_Incentive_Code from TSPL_VSP_DAY_WISE_INCENTIVE_MASTER where Day_Wise_Incentive_Code='" & obj.Day_Wise_Incentive_Code & "' ", trans)
        If clsCommon.myLen(qry1) > 0 Then
            isNewEntry = False
        End If
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_Name", obj.Day_Wise_Incentive_Name)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_From_1", obj.Day_Wise_Incentive_From_1)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_To_1", obj.Day_Wise_Incentive_To_1)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_Rate_1", obj.Day_Wise_Incentive_Rate_1)

            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_From_2", obj.Day_Wise_Incentive_From_2)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_To_2", obj.Day_Wise_Incentive_To_2)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_Rate_2", obj.Day_Wise_Incentive_Rate_2)


            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_From_3", obj.Day_Wise_Incentive_From_3)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_To_3", obj.Day_Wise_Incentive_To_3)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_Rate_3", obj.Day_Wise_Incentive_Rate_3)


            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_From_4", obj.Day_Wise_Incentive_From_4)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_To_4", obj.Day_Wise_Incentive_To_4)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_Rate_4", obj.Day_Wise_Incentive_Rate_4)


            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_From_5", obj.Day_Wise_Incentive_From_5)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_To_5", obj.Day_Wise_Incentive_To_5)
            clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_Rate_5", obj.Day_Wise_Incentive_Rate_5)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Day_Wise_Incentive_Code", obj.Day_Wise_Incentive_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_DAY_WISE_INCENTIVE_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Day_Wise_Incentive_Code, "TSPL_VSP_DAY_WISE_INCENTIVE_MASTER", "Day_Wise_Incentive_Code", trans)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_DAY_WISE_INCENTIVE_MASTER", OMInsertOrUpdate.Update, "Day_Wise_Incentive_Code='" + obj.Day_Wise_Incentive_Code + "'", trans)
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsVSSDayWiseIncentive
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal tran As SqlTransaction) As clsVSSDayWiseIncentive
        Dim obj As clsVSSDayWiseIncentive = Nothing
        Dim qry As String = "select TSPL_VSP_DAY_WISE_INCENTIVE_MASTER.* from TSPL_VSP_DAY_WISE_INCENTIVE_MASTER   where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_VSP_DAY_WISE_INCENTIVE_MASTER.Day_Wise_Incentive_Code = (select MIN(Day_Wise_Incentive_Code) from TSPL_VSP_DAY_WISE_INCENTIVE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_VSP_DAY_WISE_INCENTIVE_MASTER.Day_Wise_Incentive_Code = (select Max(Day_Wise_Incentive_Code) from TSPL_VSP_DAY_WISE_INCENTIVE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_VSP_DAY_WISE_INCENTIVE_MASTER.Day_Wise_Incentive_Code = (select TOP 1 Day_Wise_Incentive_Code from TSPL_VSP_DAY_WISE_INCENTIVE_MASTER WHERE 1=1 " + whrclas + " and Day_Wise_Incentive_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_VSP_DAY_WISE_INCENTIVE_MASTER.Day_Wise_Incentive_Code = (select Min(Day_Wise_Incentive_Code) from TSPL_VSP_DAY_WISE_INCENTIVE_MASTER where Day_Wise_Incentive_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_VSP_DAY_WISE_INCENTIVE_MASTER.Day_Wise_Incentive_Code = (select Max(Day_Wise_Incentive_Code) from TSPL_VSP_DAY_WISE_INCENTIVE_MASTER where Day_Wise_Incentive_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsVSSDayWiseIncentive()
            obj.Day_Wise_Incentive_Code = clsCommon.myCstr(dt.Rows(0)("Day_Wise_Incentive_Code"))
            obj.Day_Wise_Incentive_Name = clsCommon.myCstr(dt.Rows(0)("Day_Wise_Incentive_Name"))
            obj.Day_Wise_Incentive_From_1 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_From_1"))
            obj.Day_Wise_Incentive_To_1 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_To_1"))
            obj.Day_Wise_Incentive_Rate_1 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_Rate_1"))

            obj.Day_Wise_Incentive_From_2 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_From_2"))
            obj.Day_Wise_Incentive_To_2 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_To_2"))
            obj.Day_Wise_Incentive_Rate_2 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_Rate_2"))

            obj.Day_Wise_Incentive_From_3 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_From_3"))
            obj.Day_Wise_Incentive_To_3 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_To_3"))
            obj.Day_Wise_Incentive_Rate_3 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_Rate_3"))

            obj.Day_Wise_Incentive_From_4 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_From_4"))
            obj.Day_Wise_Incentive_To_4 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_To_4"))
            obj.Day_Wise_Incentive_Rate_4 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_Rate_4"))

            obj.Day_Wise_Incentive_From_5 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_From_5"))
            obj.Day_Wise_Incentive_To_5 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_To_5"))
            obj.Day_Wise_Incentive_Rate_5 = clsCommon.myCdbl(dt.Rows(0)("Day_Wise_Incentive_Rate_5"))
        End If
        Return obj
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Day_Wise_Incentive_Name from TSPL_VSP_DAY_WISE_INCENTIVE_MASTER where Day_Wise_Incentive_Code='" + strCode + "'"))
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = ""
        Try
            qry = "Delete from TSPL_VSP_DAY_WISE_INCENTIVE_MASTER where TSPL_VSP_DAY_WISE_INCENTIVE_MASTER.Day_Wise_Incentive_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_VSP_DAY_WISE_INCENTIVE_MASTER.Day_Wise_Incentive_Code,TSPL_VSP_DAY_WISE_INCENTIVE_MASTER.Day_Wise_Incentive_Name from TSPL_VSP_DAY_WISE_INCENTIVE_MASTER "
        str = clsCommon.ShowSelectForm("DYWIfnd", qry, "Day_Wise_Incentive_Code", whrcls, curcode, "Day_Wise_Incentive_Code", isButtonClicked)
        Return str

    End Function

    Public Shared Function getFinderObeject(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As clsVSSDayWiseIncentive
        Dim obj As clsVSSDayWiseIncentive = Nothing
        Dim strCode As String = getFinder(whrcls, curcode, isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            obj = GetData(strCode, NavigatorType.Current)
        End If
        Return obj
    End Function
End Class
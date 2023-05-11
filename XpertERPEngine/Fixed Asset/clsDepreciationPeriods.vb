Imports common
Imports System.Data.SqlClient

Public Class clsDepreciationPeriods

#Region "variables"
    Public period_Code As String = Nothing
    Public period_Desc As String = Nothing

    Public Inactive As Char = Nothing
    Public FiscalPeriod1 As String = Nothing
    Public FiscalPeriodRun1 As String = Nothing
    Public FiscalPeriodRunPerm1 As String = Nothing

    Public FiscalPeriod2 As String = Nothing
    Public FiscalPeriodRun2 As String = Nothing
    Public FiscalPeriodRunPerm2 As String = Nothing

    Public FiscalPeriod3 As String = Nothing
    Public FiscalPeriodRun3 As String = Nothing
    Public FiscalPeriodRunPerm3 As String = Nothing

    Public FiscalPeriod4 As String = Nothing
    Public FiscalPeriodRun4 As String = Nothing
    Public FiscalPeriodRunPerm4 As String = Nothing

    Public FiscalPeriod5 As String = Nothing
    Public FiscalPeriodRun5 As String = Nothing
    Public FiscalPeriodRunPerm5 As String = Nothing

    Public FiscalPeriod6 As String = Nothing
    Public FiscalPeriodRun6 As String = Nothing
    Public FiscalPeriodRunPerm6 As String = Nothing

    Public FiscalPeriod7 As String = Nothing
    Public FiscalPeriodRun7 As String = Nothing
    Public FiscalPeriodRunPerm7 As String = Nothing

    Public FiscalPeriod8 As String = Nothing
    Public FiscalPeriodRun8 As String = Nothing
    Public FiscalPeriodRunPerm8 As String = Nothing

    Public FiscalPeriod9 As String = Nothing
    Public FiscalPeriodRun9 As String = Nothing
    Public FiscalPeriodRunPerm9 As String = Nothing

    Public FiscalPeriod10 As String = Nothing
    Public FiscalPeriodRun10 As String = Nothing
    Public FiscalPeriodRunPerm10 As String = Nothing

    Public FiscalPeriod11 As String = Nothing
    Public FiscalPeriodRun11 As String = Nothing
    Public FiscalPeriodRunPerm11 As String = Nothing

    Public FiscalPeriod12 As String = Nothing
    Public FiscalPeriodRun12 As String = Nothing
    Public FiscalPeriodRunPerm12 As String = Nothing

    Public Modify_By As DateTime
    Public Modify_Date As DateTime
    Public Created_By As DateTime
    Public Created_Date As DateTime
    Public Comp_Code As String
#End Region
    Public Function SaveData(ByVal obj As clsDepreciationPeriods, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "period_Desc", obj.period_Desc)
            clsCommon.AddColumnsForChange(coll, "Inactive", obj.Inactive)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriod1", obj.FiscalPeriod1)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRun1", obj.FiscalPeriodRun1)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRunPerm1", obj.FiscalPeriodRunPerm1)

            clsCommon.AddColumnsForChange(coll, "FiscalPeriod2", obj.FiscalPeriod2)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRun2", obj.FiscalPeriodRun2)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRunPerm2", obj.FiscalPeriodRunPerm2)

            clsCommon.AddColumnsForChange(coll, "FiscalPeriod3", obj.FiscalPeriod3)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRun3", obj.FiscalPeriodRun3)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRunPerm3", obj.FiscalPeriodRunPerm3)

            clsCommon.AddColumnsForChange(coll, "FiscalPeriod4", obj.FiscalPeriod4)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRun4", obj.FiscalPeriodRun4)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRunPerm4", obj.FiscalPeriodRunPerm4)

            clsCommon.AddColumnsForChange(coll, "FiscalPeriod5", obj.FiscalPeriod5)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRun5", obj.FiscalPeriodRun5)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRunPerm5", obj.FiscalPeriodRunPerm5)

            clsCommon.AddColumnsForChange(coll, "FiscalPeriod6", obj.FiscalPeriod6)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRun6", obj.FiscalPeriodRun6)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRunPerm6", obj.FiscalPeriodRunPerm6)

            clsCommon.AddColumnsForChange(coll, "FiscalPeriod7", obj.FiscalPeriod7)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRun7", obj.FiscalPeriodRun7)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRunPerm7", obj.FiscalPeriodRunPerm7)

            clsCommon.AddColumnsForChange(coll, "FiscalPeriod8", obj.FiscalPeriod8)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRun8", obj.FiscalPeriodRun8)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRunPerm8", obj.FiscalPeriodRunPerm8)

            clsCommon.AddColumnsForChange(coll, "FiscalPeriod9", obj.FiscalPeriod9)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRun9", obj.FiscalPeriodRun9)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRunPerm9", obj.FiscalPeriodRunPerm9)

            clsCommon.AddColumnsForChange(coll, "FiscalPeriod10", obj.FiscalPeriod10)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRun10", obj.FiscalPeriodRun10)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRunPerm10", obj.FiscalPeriodRunPerm10)

            clsCommon.AddColumnsForChange(coll, "FiscalPeriod11", obj.FiscalPeriod11)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRun11", obj.FiscalPeriodRun11)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRunPerm11", obj.FiscalPeriodRunPerm11)

            clsCommon.AddColumnsForChange(coll, "FiscalPeriod12", obj.FiscalPeriod12)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRun12", obj.FiscalPeriodRun12)
            clsCommon.AddColumnsForChange(coll, "FiscalPeriodRunPerm12", obj.FiscalPeriodRunPerm12)

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_DEPRECIATION_PERIODS where Period_Code='" & obj.period_Code & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.period_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.DepreciationPeriods, "", "")
                        If clsCommon.myLen(obj.period_Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "period_Code", obj.period_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEPRECIATION_PERIODS", OMInsertOrUpdate.Insert, "", trans)
            Else

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEPRECIATION_PERIODS", OMInsertOrUpdate.Update, "TSPL_DEPRECIATION_PERIODS.period_Code='" + obj.period_Code + "'", trans)
            End If

            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsDepreciationPeriods
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDepreciationPeriods
        Dim obj As clsDepreciationPeriods = Nothing
        Dim qry As String = "select period_Code,period_Desc,Inactive,FiscalPeriod1,FiscalPeriodRun1,FiscalPeriodRunPerm1,FiscalPeriod2,FiscalPeriodRun2,FiscalPeriodRunPerm2, " & _
        " FiscalPeriod3,FiscalPeriodRun3,FiscalPeriodRunPerm3,FiscalPeriod4,FiscalPeriodRun4,FiscalPeriodRunPerm4,FiscalPeriod5,FiscalPeriodRun5,FiscalPeriodRunPerm5, " & _
        " FiscalPeriod6,FiscalPeriodRun6,FiscalPeriodRunPerm6,FiscalPeriod7,FiscalPeriodRun7,FiscalPeriodRunPerm7, " & _
        " FiscalPeriod8,FiscalPeriodRun8,FiscalPeriodRunPerm8,FiscalPeriod9,FiscalPeriodRun9,FiscalPeriodRunPerm9,FiscalPeriod10,FiscalPeriodRun10,FiscalPeriodRunPerm10, " & _
        " FiscalPeriod11,FiscalPeriodRun11,FiscalPeriodRunPerm11,FiscalPeriod12,FiscalPeriodRun12,FiscalPeriodRunPerm12 from TSPL_DEPRECIATION_PERIODS where 2=2"
        Dim whrClas As String = ""

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DEPRECIATION_PERIODS.period_Code = (select MIN(period_Code) from TSPL_DEPRECIATION_PERIODS where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_DEPRECIATION_PERIODS.period_Code = (select Max(period_Code) from TSPL_DEPRECIATION_PERIODS where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_DEPRECIATION_PERIODS.period_Code = (select Min(period_Code) from TSPL_DEPRECIATION_PERIODS where period_Code>'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_DEPRECIATION_PERIODS.period_Code = (select Max(period_Code) from TSPL_DEPRECIATION_PERIODS where period_Code<'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += "  and TSPL_DEPRECIATION_PERIODS.period_Code = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDepreciationPeriods()
            obj.period_Code = clsCommon.myCstr(dt.Rows(0)("period_Code"))
            obj.period_Desc = clsCommon.myCstr(dt.Rows(0)("period_Desc"))
            obj.Inactive = clsCommon.myCstr(dt.Rows(0)("Inactive"))



            obj.FiscalPeriod1 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriod1"))
            obj.FiscalPeriodRun1 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRun1"))
            obj.FiscalPeriodRunPerm1 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRunPerm1"))

            obj.FiscalPeriod2 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriod2"))
            obj.FiscalPeriodRun2 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRun2"))
            obj.FiscalPeriodRunPerm2 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRunPerm2"))

            obj.FiscalPeriod3 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriod3"))
            obj.FiscalPeriodRun3 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRun3"))
            obj.FiscalPeriodRunPerm3 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRunPerm3"))

            obj.FiscalPeriod4 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriod4"))
            obj.FiscalPeriodRun4 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRun4"))
            obj.FiscalPeriodRunPerm4 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRunPerm4"))

            obj.FiscalPeriod5 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriod5"))
            obj.FiscalPeriodRun5 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRun5"))
            obj.FiscalPeriodRunPerm5 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRunPerm5"))

            obj.FiscalPeriod6 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriod6"))
            obj.FiscalPeriodRun6 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRun6"))
            obj.FiscalPeriodRunPerm6 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRunPerm6"))

            obj.FiscalPeriod7 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriod7"))
            obj.FiscalPeriodRun7 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRun7"))
            obj.FiscalPeriodRunPerm7 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRunPerm7"))

            obj.FiscalPeriod8 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriod8"))
            obj.FiscalPeriodRun8 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRun8"))
            obj.FiscalPeriodRunPerm8 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRunPerm8"))

            obj.FiscalPeriod9 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriod9"))
            obj.FiscalPeriodRun9 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRun9"))
            obj.FiscalPeriodRunPerm9 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRunPerm9"))

            obj.FiscalPeriod10 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriod10"))
            obj.FiscalPeriodRun10 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRun10"))
            obj.FiscalPeriodRunPerm10 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRunPerm10"))

            obj.FiscalPeriod11 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriod11"))
            obj.FiscalPeriodRun11 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRun11"))
            obj.FiscalPeriodRunPerm11 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRunPerm11"))

            obj.FiscalPeriod12 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriod12"))
            obj.FiscalPeriodRun12 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRun12"))
            obj.FiscalPeriodRunPerm12 = clsCommon.myCstr(dt.Rows(0)("FiscalPeriodRunPerm12"))
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try


            Dim qry As String = "delete from TSPL_DEPRECIATION_PERIODS where period_Code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If (isSaved) Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function



End Class
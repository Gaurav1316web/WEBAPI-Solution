Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsCostCentreFinancial
#Region "Variables"
    Public Cost_Center_Fin_Code As String = Nothing
    Public Cost_Center_Fin_Name As String = Nothing
    Public Cost_Center_Group As String = Nothing
    Public Hirerachy_Level_Code As String = Nothing
    Public Hirerachy_Level As String = Nothing
    Public Cost_Centre_Fin_Level_Code As String = Nothing
    Public Arr As List(Of ClsCostCentreFinancialDetail) = Nothing
    Public EnableHirerachyCostCentre As Double = 0
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as [Code] ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level_Code,'') AS [Hirerachy Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level,'') AS [Hirerachy Level] ,TSPL_COST_CENTRE_FINANCIAL.Created_By as [Created By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Created_Date,103) as [Created Date] ,TSPL_COST_CENTRE_FINANCIAL.Modified_By as [Modified By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Modified_Date,103) as [Modified Date]  From TSPL_COST_CENTRE_FINANCIAL "
        str = clsCommon.ShowSelectForm("CostCentreFin", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function GetName(ByVal CC_Code As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "SELECT Cost_Center_Fin_Name FROM TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code= '" & CC_Code & "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
   
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsCostCentreFinancial
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim EnableHirerachyCostCentre As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableHirerachyCostCentre, clsFixedParameterCode.EnableHirerachyCostCentre, Nothing))
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String

            qry = "delete from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
            If EnableHirerachyCostCentre = 1 Then
                qry = "delete from TSPL_COST_CENTRE_HIRERACHY_DETAIL where COST_CENTRE_HIRERACHY_CODE ='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsCostCentreFinancial
        Dim obj As ClsCostCentreFinancial = Nothing
        Dim EnableHirerachyCostCentre As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableHirerachyCostCentre, clsFixedParameterCode.EnableHirerachyCostCentre, Nothing))
        Dim qry As String = "select * from TSPL_COST_CENTRE_FINANCIAL where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Cost_Center_Fin_Code = (select MIN(Cost_Center_Fin_Code) from TSPL_COST_CENTRE_FINANCIAL)"
            Case NavigatorType.Last
                qry += " and Cost_Center_Fin_Code = (select Max(Cost_Center_Fin_Code) from TSPL_COST_CENTRE_FINANCIAL)"
            Case NavigatorType.Next
                qry += " and Cost_Center_Fin_Code = (select Min(Cost_Center_Fin_Code) from TSPL_COST_CENTRE_FINANCIAL where  Cost_Center_Fin_Code>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and Cost_Center_Fin_Code = (select Max(Cost_Center_Fin_Code) from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and Cost_Center_Fin_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsCostCentreFinancial()
            obj.Cost_Center_Fin_Code = clsCommon.myCstr(dt.Rows(0)("Cost_Center_Fin_Code"))
            obj.Cost_Center_Fin_Name = clsCommon.myCstr(dt.Rows(0)("Cost_Center_Fin_Name"))
            obj.Cost_Center_Group = clsCommon.myCstr(dt.Rows(0)("Cost_Center_Group"))
            obj.Hirerachy_Level_Code = clsCommon.myCstr(dt.Rows(0)("Hirerachy_Level_Code"))
            obj.Hirerachy_Level = clsCommon.myCstr(dt.Rows(0)("Hirerachy_Level"))
            obj.Cost_Centre_Fin_Level_Code = clsCommon.myCstr(dt.Rows(0)("Cost_Centre_Fin_Level_Code"))
        End If
        If EnableHirerachyCostCentre = 1 Then
            obj = ClsCostCentreFinancialDetail.GetCostCentreLevel(obj)
        End If
        Return obj
    End Function

    Public Shared Function SaveData(ByVal obj As ClsCostCentreFinancial, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As ClsCostCentreFinancial, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Cost_Center_Fin_Name", obj.Cost_Center_Fin_Name)
            clsCommon.AddColumnsForChange(coll, "Cost_Center_Group", obj.Cost_Center_Group, True)
            clsCommon.AddColumnsForChange(coll, "Cost_Centre_Fin_Level_Code", obj.Cost_Centre_Fin_Level_Code, True)
            clsCommon.AddColumnsForChange(coll, "Hirerachy_Level_Code", obj.Hirerachy_Level_Code)
            clsCommon.AddColumnsForChange(coll, "Hirerachy_Level", obj.Hirerachy_Level)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code='" & obj.Cost_Center_Fin_Code & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.Cost_Center_Fin_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.CostCentreFinancial, "", "")
                        If clsCommon.myLen(obj.Cost_Center_Fin_Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Cost_Center_Fin_Code", obj.Cost_Center_Fin_Code.ToUpper())

                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code= '" & obj.Cost_Center_Fin_Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_COST_CENTRE_FINANCIAL", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If
            Else
                'isSaved = isSaved AndAlso clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentCompanyCode, obj.Cost_Center_Fin_Code, "TSPL_COST_CENTRE_FINANCIAL", "Cost_Center_Fin_Code", trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_COST_CENTRE_FINANCIAL", OMInsertOrUpdate.Update, "Cost_Center_Fin_Code='" + obj.Cost_Center_Fin_Code + "'", trans)
            End If
            If obj.EnableHirerachyCostCentre = 1 Then
                isSaved = isSaved AndAlso ClsCostCentreFinancialDetail.SaveDetailData(obj, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select Cost_Center_Fin_Code from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
End Class
Public Class ClsCostCentreFinancialDetail
#Region "Variables"
    Public FLAG As Boolean = False
    Public COST_CENTRE_HIRERACHY_CODE As String = Nothing
    Public HIRERACHY_LEVEL_CODE1 As String = Nothing
    Public HIRERACHY_LEVEL_CODE2 As String = Nothing
    Public HIRERACHY_LEVEL_CODE3 As String = Nothing
    Public HIRERACHY_LEVEL As String = Nothing
#End Region
    '---------CostCentreLevel--------------------------
    Public Shared Function GetCostCentreLevel(ByVal obj As ClsCostCentreFinancial) As ClsCostCentreFinancial
        Try
            Dim qry As String = Nothing
            qry = "select distinct case when isnull(copy_table.Level_Code,'')='' then CAST(0 as bit) else CAST(1 as bit) end as Sel,TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE ,TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE1," & _
                 " TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE2,TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE3,TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL  From TSPL_COST_CENTRE_FINANCIAL " & _
                 " left outer join TSPL_COST_CENTRE_HIRERACHY_DETAIL on TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE=TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code " & _
                 " LEFT OUTER JOIN TSPL_COST_CENTRE_FINANCIAL AS TSPL_COST_CENTRE_FINANCIAL_Nmae on TSPL_COST_CENTRE_FINANCIAL_Nmae.Cost_Center_Fin_Code=TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE " & _
                 "  LEFT OUTER JOIN (SELECT CASE WHEN Hirerachy_Level='2' THEN HIRERACHY_LEVEL_CODE1 WHEN Hirerachy_Level='3' THEN HIRERACHY_LEVEL_CODE2 " & _
                 " WHEN Hirerachy_Level='4' THEN HIRERACHY_LEVEL_CODE3 END AS Level_Code,* FROM TSPL_COST_CENTRE_HIRERACHY_DETAIL WHERE COST_CENTRE_HIRERACHY_CODE='" + obj.Cost_Center_Fin_Code + "') AS copy_table on " & _
                 " copy_table.Level_Code  =TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE "
            If clsCommon.CompairString(clsCommon.myCstr(clsCommon.myCdbl(obj.Hirerachy_Level) - 1), "3") = CompairStringResult.Equal Then
                qry += " AND copy_table.HIRERACHY_LEVEL_CODE2=TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE2 "
            End If
            qry += " where TSPL_COST_CENTRE_HIRERACHY_DETAIL.Hirerachy_Level='" + clsCommon.myCstr(clsCommon.myCdbl(obj.Hirerachy_Level) - 1) + "' "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Nothing)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Arr = New List(Of ClsCostCentreFinancialDetail)
                Dim objTr As ClsCostCentreFinancialDetail
                For i As Integer = 0 To dt.Rows.Count - 1
                    objTr = New ClsCostCentreFinancialDetail
                    objTr.FLAG = clsCommon.myCdbl(dt.Rows(i)("Sel"))
                    objTr.COST_CENTRE_HIRERACHY_CODE = clsCommon.myCstr(dt.Rows(i)("COST_CENTRE_HIRERACHY_CODE"))
                    objTr.HIRERACHY_LEVEL_CODE1 = clsCommon.myCstr(dt.Rows(i)("HIRERACHY_LEVEL_CODE1"))
                    objTr.HIRERACHY_LEVEL_CODE2 = clsCommon.myCstr(dt.Rows(i)("HIRERACHY_LEVEL_CODE2"))
                    objTr.HIRERACHY_LEVEL_CODE3 = clsCommon.myCstr(dt.Rows(i)("HIRERACHY_LEVEL_CODE3"))
                    objTr.HIRERACHY_LEVEL = clsCommon.myCstr(dt.Rows(i)("HIRERACHY_LEVEL"))
                    obj.Arr.Add(objTr)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function SaveDetailData(ByVal objH As ClsCostCentreFinancial, ByVal trans As SqlTransaction) As Boolean

        Try
            If (objH.Arr IsNot Nothing AndAlso objH.Arr.Count > 0) Then
                Dim qry As String = "DELETE FROM TSPL_COST_CENTRE_HIRERACHY_DETAIL WHERE COST_CENTRE_HIRERACHY_CODE='" + objH.Cost_Center_Fin_Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                For Each obj As ClsCostCentreFinancialDetail In objH.Arr

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "COST_CENTRE_HIRERACHY_CODE", objH.Cost_Center_Fin_Code)
                    clsCommon.AddColumnsForChange(coll, "HIRERACHY_LEVEL_CODE1", obj.HIRERACHY_LEVEL_CODE1, True)
                    clsCommon.AddColumnsForChange(coll, "HIRERACHY_LEVEL_CODE2", obj.HIRERACHY_LEVEL_CODE2, True)
                    clsCommon.AddColumnsForChange(coll, "HIRERACHY_LEVEL_CODE3", obj.HIRERACHY_LEVEL_CODE3, True)
                    clsCommon.AddColumnsForChange(coll, "Hirerachy_Level", obj.HIRERACHY_LEVEL)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_COST_CENTRE_HIRERACHY_DETAIL", OMInsertOrUpdate.Insert, "TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE='" + objH.Cost_Center_Fin_Code + "' ", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function

    '--------------------------------------------
End Class

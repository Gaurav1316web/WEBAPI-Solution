Imports System.Data.SqlClient
Imports common
Public Class clsMilkCollectionLevels

#Region "variables"
    Public LEVEL_CODE As String = Nothing
    Public DESCRIPTION As String = Nothing
    Public PARENT_LEVEL_CODE As String = Nothing
    Public PARENT_LEVEL_CODE_Desc As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal obj As clsMilkCollectionLevels, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim qry As String = ""

        Try
            
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)
            clsCommon.AddColumnsForChange(coll, "PARENT_LEVEL_CODE", obj.PARENT_LEVEL_CODE, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "LEVEL_CODE", obj.LEVEL_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MilkCollectionLevels", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MilkCollectionLevels", OMInsertOrUpdate.Update, "TSPL_MilkCollectionLevels.LEVEL_CODE='" + obj.LEVEL_CODE + "'", trans)
            End If

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return True
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMilkCollectionLevels
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkCollectionLevels
        Dim obj As clsMilkCollectionLevels = Nothing
        Dim Arr As List(Of clsMilkCollectionLevels) = Nothing
        Dim qry As String = "select LEVEL_CODE,DESCRIPTION,PARENT_LEVEL_CODE from TSPL_MilkCollectionLevels where 2=2 "
        Dim whrclas As String = " and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MilkCollectionLevels.LEVEL_CODE = (select MIN(LEVEL_CODE) from TSPL_MilkCollectionLevels WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_MilkCollectionLevels.LEVEL_CODE = (select Max(LEVEL_CODE) from TSPL_MilkCollectionLevels WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_MilkCollectionLevels.LEVEL_CODE = (select top 1 LEVEL_CODE from TSPL_MilkCollectionLevels WHERE 1=1 " + whrclas + " and LEVEL_CODE='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_MilkCollectionLevels.LEVEL_CODE = (select Min(LEVEL_CODE) from TSPL_MilkCollectionLevels where LEVEL_CODE>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_MilkCollectionLevels.LEVEL_CODE = (select Max(LEVEL_CODE) from TSPL_MilkCollectionLevels where LEVEL_CODE<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMilkCollectionLevels()
            obj.LEVEL_CODE = clsCommon.myCstr(dt.Rows(0)("LEVEL_CODE"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.PARENT_LEVEL_CODE = clsCommon.myCstr(dt.Rows(0)("PARENT_LEVEL_CODE"))

        End If
        Return obj
    End Function
    Public Shared Function GetDesignationLevel() As DataTable
        Dim dt As DataTable
        Dim qry As String = "select Level as Code,LevelDESCRIPTION as DESCRIPTION from TSPL_Hierarchy_Master where Comp_Code='" & objCommonVar.CurrentCompanyCode & "' and Chechk=1"
        dt = clsDBFuncationality.GetDataTable(qry)

        Return dt
    End Function
End Class

Imports common
Imports System.Data.SqlClient

Public Class ClsVPFSettings
#Region "variables"
    Public Program_Code As String = Nothing
    Public Oval_Color As Integer = 0
    Public Oval_Blink_Color As Integer = 0
    Public Is_ColorAppliedForAll As Integer = 0
    Public Oval_Under_Oval As Integer = 0
    Public VPFScreenCode As Integer = 0
    Public Created_By As String = String.Empty
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = String.Empty
    Public Modified_Date As Date? = Nothing
    Public Comp_Code As String = String.Empty
#End Region
    Public Shared Function SaveData(ByVal obj As ClsVPFSettings) As Boolean
        Dim qry As String = ""
        Dim isSaved As Boolean = True
        Try
            clsDBFuncationality.ExecuteNonQuery("Delete FROM TSPL_VPF_Settings  ")
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Program_Code", obj.Program_Code)
            clsCommon.AddColumnsForChange(coll, "Oval_Color", obj.Oval_Color)
            clsCommon.AddColumnsForChange(coll, "Oval_Blink_Color", obj.Oval_Blink_Color)
            clsCommon.AddColumnsForChange(coll, "Is_ColorAppliedForAll", obj.Is_ColorAppliedForAll)
            clsCommon.AddColumnsForChange(coll, "Oval_Under_Oval", obj.Oval_Under_Oval)
            clsCommon.AddColumnsForChange(coll, "VPFScreenCode", obj.VPFScreenCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
         
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

            If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_VPF_Settings WHERE Program_Code='" + obj.Program_Code + "'") <= 0 Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VPF_Settings", OMInsertOrUpdate.Insert, "")
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VPF_Settings", OMInsertOrUpdate.Update, "Program_Code='" + obj.Program_Code + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsVPFSettings
        Dim obj As ClsVPFSettings = Nothing

        Dim qry As String = "Select * From TSPL_VPF_Settings where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_VPF_Settings.Program_Code = (select MIN(Program_Code) from TSPL_VPF_Settings)"
            Case NavigatorType.Last
                qry += " and TSPL_VPF_Settings.Program_Code = (select Max(Program_Code) from TSPL_VPF_Settings)"
            Case NavigatorType.Next
                qry += " and TSPL_VPF_Settings.Program_Code = (select Min(Program_Code) from TSPL_VPF_Settings where Program_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_VPF_Settings.Program_Code = (select Max(Program_Code) from TSPL_VPF_Settings where Program_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_VPF_Settings.Program_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsVPFSettings()
            obj.Program_Code = clsCommon.myCstr(dt.Rows(0)("Program_Code"))
            obj.Oval_Color = clsCommon.myCdbl(dt.Rows(0)("Oval_Color"))
            obj.Oval_Blink_Color = clsCommon.myCdbl(dt.Rows(0)("Oval_Blink_Color"))
            obj.Oval_Under_Oval = clsCommon.myCdbl(dt.Rows(0)("Oval_Under_Oval"))
            obj.Is_ColorAppliedForAll = clsCommon.myCdbl(dt.Rows(0)("Is_ColorAppliedForAll"))
            obj.VPFScreenCode = clsCommon.myCstr(dt.Rows(0)("VPFScreenCode"))
        End If
        Return obj
    End Function
    Public Shared Function GetColor() As DataTable
        Dim qry As String = " SELECT ISNULL(Oval_Color,0) AS Oval_Color,ISNULL(Oval_Blink_Color,0) AS Oval_Blink_Color,ISNULL(Is_ColorAppliedForAll,0) AS Is_ColorAppliedForAll,VPFScreenCode FROM TSPL_VPF_Settings "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
End Class


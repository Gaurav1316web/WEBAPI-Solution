Imports System.Data.SqlClient
Imports common
Public Class clsVSPDeduction

#Region "Variables"
    Public Deduction_Code As String = Nothing
    Public Deduction_Name As String = Nothing
    Public Deduction_On As Integer ''0-Rate,1-Percentage
    Public Deduction_Rate As Decimal
    Public Deduction_Minimum_FAT_Per As Decimal
    Public Deduction_Minimum_SNF_Per As Decimal
    Public Deduction_No_Of_Payment_Cycle_For_New_VSP As Integer
#End Region

    Public Shared Function SaveData(ByVal obj As clsVSPDeduction) As Boolean
        Dim qry As String = ""
        Dim isNewEntry As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry1 As String = clsDBFuncationality.getSingleValue("Select Deduction_Code from TSPL_VSP_DEDUCTION_MASTER where Deduction_Code='" & obj.Deduction_Code & "' ", trans)
        If clsCommon.myLen(qry1) > 0 Then
            isNewEntry = False
        End If
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Deduction_Name", obj.Deduction_Name)
            clsCommon.AddColumnsForChange(coll, "Deduction_On", obj.Deduction_On)
            clsCommon.AddColumnsForChange(coll, "Deduction_Rate", obj.Deduction_Rate)
            clsCommon.AddColumnsForChange(coll, "Deduction_Minimum_FAT_Per", obj.Deduction_Minimum_FAT_Per)
            clsCommon.AddColumnsForChange(coll, "Deduction_Minimum_SNF_Per", obj.Deduction_Minimum_SNF_Per)
            clsCommon.AddColumnsForChange(coll, "Deduction_No_Of_Payment_Cycle_For_New_VSP", obj.Deduction_No_Of_Payment_Cycle_For_New_VSP)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Deduction_Code", obj.Deduction_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_DEDUCTION_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Deduction_Code, "TSPL_VSP_DEDUCTION_MASTER", "Deduction_Code", trans)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_DEDUCTION_MASTER", OMInsertOrUpdate.Update, "Deduction_Code='" + obj.Deduction_Code + "'", trans)
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsVSPDeduction
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsVSPDeduction
        Dim obj As clsVSPDeduction = Nothing
        Dim qry As String = "select TSPL_VSP_DEDUCTION_MASTER.* from TSPL_VSP_DEDUCTION_MASTER   where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_VSP_DEDUCTION_MASTER.Deduction_Code = (select MIN(Deduction_Code) from TSPL_VSP_DEDUCTION_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_VSP_DEDUCTION_MASTER.Deduction_Code = (select Max(Deduction_Code) from TSPL_VSP_DEDUCTION_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_VSP_DEDUCTION_MASTER.Deduction_Code = (select TOP 1 Deduction_Code from TSPL_VSP_DEDUCTION_MASTER WHERE 1=1 " + whrclas + " and Deduction_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_VSP_DEDUCTION_MASTER.Deduction_Code = (select Min(Deduction_Code) from TSPL_VSP_DEDUCTION_MASTER where Deduction_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_VSP_DEDUCTION_MASTER.Deduction_Code = (select Max(Deduction_Code) from TSPL_VSP_DEDUCTION_MASTER where Deduction_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsVSPDeduction()
            obj.Deduction_Code = clsCommon.myCstr(dt.Rows(0)("Deduction_Code"))
            obj.Deduction_Name = clsCommon.myCstr(dt.Rows(0)("Deduction_Name"))
            obj.Deduction_On = clsCommon.myCdbl(dt.Rows(0)("Deduction_On"))
            obj.Deduction_Rate = clsCommon.myCdbl(dt.Rows(0)("Deduction_Rate"))
            obj.Deduction_Minimum_FAT_Per = clsCommon.myCdbl(dt.Rows(0)("Deduction_Minimum_FAT_Per"))
            obj.Deduction_Minimum_SNF_Per = clsCommon.myCdbl(dt.Rows(0)("Deduction_Minimum_SNF_Per"))
            obj.Deduction_No_Of_Payment_Cycle_For_New_VSP = clsCommon.myCdbl(dt.Rows(0)("Deduction_No_Of_Payment_Cycle_For_New_VSP"))
        End If
        Return obj
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Deduction_Name from TSPL_VSP_DEDUCTION_MASTER where Deduction_Code='" + strCode + "'"))
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = ""
        Try
            qry = "Delete from TSPL_VSP_DEDUCTION_MASTER where TSPL_VSP_DEDUCTION_MASTER.Deduction_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_VSP_DEDUCTION_MASTER.Deduction_Code,TSPL_VSP_DEDUCTION_MASTER.Deduction_Name from TSPL_VSP_DEDUCTION_MASTER "
        str = clsCommon.ShowSelectForm("VSPDedfnd", qry, "Deduction_Code", whrcls, curcode, "Deduction_Code", isButtonClicked, "TSPL_VSP_DEDUCTION_MASTER.Created_Date")
        Return str

    End Function

    Public Shared Function getFinderObeject(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As clsVSPDeduction
        Dim obj As clsVSPDeduction = Nothing
        Dim strCode As String = getFinder(whrcls, curcode, isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            obj = GetData(strCode, NavigatorType.Current)
        End If
        Return obj
    End Function
End Class
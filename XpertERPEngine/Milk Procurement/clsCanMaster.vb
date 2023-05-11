Imports System.Data.SqlClient
Imports common

Public Class clsCanMaster

#Region "Variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Tare_Weight As Decimal
#End Region

    Public Shared Function SaveData(ByVal obj As clsCanMaster) As Boolean
        Dim qry As String = ""
        Dim isNewEntry As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry1 As String = clsDBFuncationality.getSingleValue("Select Code from TSPL_CAN_MASTER where code='" & obj.Code & "' ", trans)
        If clsCommon.myLen(qry1) > 0 Then
            isNewEntry = False
        End If
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CAN_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_CAN_MASTER", "Code", trans)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CAN_MASTER", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsCanMaster
        Dim obj As clsCanMaster = Nothing
        Dim qry As String = "select TSPL_CAN_MASTER.* from TSPL_CAN_MASTER   where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CAN_MASTER.Code = (select MIN(Code) from TSPL_CAN_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_CAN_MASTER.Code = (select Max(Code) from TSPL_CAN_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_CAN_MASTER.Code = (select TOP 1 Code from TSPL_CAN_MASTER WHERE 1=1 " + whrclas + " and Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_CAN_MASTER.Code = (select Min(Code) from TSPL_CAN_MASTER where Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_CAN_MASTER.Code = (select Max(Code) from TSPL_CAN_MASTER where Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCanMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
        End If
        Return obj
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_CAN_MASTER where Code='" + strCode + "'"))
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = ""
        Try
            qry = "Delete from TSPL_CAN_MASTER where TSPL_CAN_MASTER.Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_CAN_MASTER.Code,TSPL_CAN_MASTER.Description,TSPL_CAN_MASTER.Tare_Weight as [Tare Weight] from TSPL_CAN_MASTER "
        str = clsCommon.ShowSelectForm("canmasterFnd", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function

    Public Shared Function getFinderObeject(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As clsCanMaster
        Dim obj As clsCanMaster = Nothing
        Dim strCode As String = getFinder(whrcls, curcode, isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            obj = GetData(strCode, NavigatorType.Current)
        End If
        Return obj
    End Function

    'Public Shared Function CheckNewEntry(ByVal Code As String, ByVal trans As SqlTransaction) As String
    '    Dim qry As String = "select Code from TSPL_CAN_MASTER where Code ='" + Code + "'   "
    '    Dim dt As DataTable
    '    dt = clsDBFuncationality.GetDataTable(qry, trans)
    '    If dt.Rows.Count > 0 Then
    '        Return False
    '    Else
    '        Return True
    '    End If

    'End Function

End Class


Public Class clsDockMaster

#Region "Variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public MCC_Code As String
    Public Default_Sample_Machine_1 As String = Nothing
    Public Default_Sample_Machine_2 As String = Nothing
    Public Default_Sample_Machine_3 As String = Nothing
    Public Default_Sample_Machine_4 As String = Nothing
    Public Default_Sample_Comport_1 As String = Nothing
    Public Default_Sample_Comport_2 As String = Nothing
    Public Default_Sample_Comport_3 As String = Nothing
    Public Default_Sample_Comport_4 As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As clsDockMaster) As Boolean
        Dim qry As String = ""
        Dim isNewEntry As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry1 As String = clsDBFuncationality.getSingleValue("Select Code from TSPL_DOCK_MASTER where code='" & obj.Code & "' ", trans)
        If clsCommon.myLen(qry1) > 0 Then
            isNewEntry = False
        End If
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            clsCommon.AddColumnsForChange(coll, "Default_Sample_Machine_1", obj.Default_Sample_Machine_1)
            clsCommon.AddColumnsForChange(coll, "Default_Sample_Machine_2", obj.Default_Sample_Machine_2)
            clsCommon.AddColumnsForChange(coll, "Default_Sample_Machine_3", obj.Default_Sample_Machine_3)
            clsCommon.AddColumnsForChange(coll, "Default_Sample_Machine_4", obj.Default_Sample_Machine_4)

            clsCommon.AddColumnsForChange(coll, "Default_Sample_Comport_1", obj.Default_Sample_Comport_1)
            clsCommon.AddColumnsForChange(coll, "Default_Sample_Comport_2", obj.Default_Sample_Comport_2)
            clsCommon.AddColumnsForChange(coll, "Default_Sample_Comport_3", obj.Default_Sample_Comport_3)
            clsCommon.AddColumnsForChange(coll, "Default_Sample_Comport_4", obj.Default_Sample_Comport_4)

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DOCK_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_DOCK_MASTER", "Code", trans)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DOCK_MASTER", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsDockMaster
        Dim obj As clsDockMaster = Nothing
        Dim qry As String = "select TSPL_DOCK_MASTER.* from TSPL_DOCK_MASTER   where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DOCK_MASTER.Code = (select MIN(Code) from TSPL_DOCK_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_DOCK_MASTER.Code = (select Max(Code) from TSPL_DOCK_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_DOCK_MASTER.Code = (select TOP 1 Code from TSPL_DOCK_MASTER WHERE 1=1 " + whrclas + " and Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_DOCK_MASTER.Code = (select Min(Code) from TSPL_DOCK_MASTER where Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_DOCK_MASTER.Code = (select Max(Code) from TSPL_DOCK_MASTER where Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDockMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))

            obj.Default_Sample_Machine_1 = clsCommon.myCstr(dt.Rows(0)("Default_Sample_Machine_1"))
            obj.Default_Sample_Machine_2 = clsCommon.myCstr(dt.Rows(0)("Default_Sample_Machine_2"))
            obj.Default_Sample_Machine_3 = clsCommon.myCstr(dt.Rows(0)("Default_Sample_Machine_3"))
            obj.Default_Sample_Machine_4 = clsCommon.myCstr(dt.Rows(0)("Default_Sample_Machine_4"))

            obj.Default_Sample_Comport_1 = clsCommon.myCstr(dt.Rows(0)("Default_Sample_Comport_1"))
            obj.Default_Sample_Comport_2 = clsCommon.myCstr(dt.Rows(0)("Default_Sample_Comport_2"))
            obj.Default_Sample_Comport_3 = clsCommon.myCstr(dt.Rows(0)("Default_Sample_Comport_3"))
            obj.Default_Sample_Comport_4 = clsCommon.myCstr(dt.Rows(0)("Default_Sample_Comport_4"))
        End If
        Return obj
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_DOCK_MASTER where Code='" + strCode + "'"))
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = ""
        Try
            qry = "Delete from TSPL_DOCK_MASTER where TSPL_DOCK_MASTER.Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_DOCK_MASTER.Code,TSPL_DOCK_MASTER.Description,TSPL_DOCK_MASTER.MCC_Code as [MCC Code] from TSPL_DOCK_MASTER "
        str = clsCommon.ShowSelectForm("DockmasterFnd", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function

    Public Shared Function getFinderObeject(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As clsDockMaster
        Dim obj As clsDockMaster = Nothing
        Dim strCode As String = getFinder(whrcls, curcode, isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            obj = GetData(strCode, NavigatorType.Current)
        End If
        Return obj
    End Function

     
    Public Shared Function DefaultSampleMachine(ByVal SNo1To4 As Integer, ByVal DockCode As String, ByVal trans As SqlTransaction) As String
        Dim sQuery As String = "select   coalesce(Default_Sample_Machine_" + clsCommon.myCstr(SNo1To4) + ",'E')  from TSPL_DOCK_MASTER where Code='" & DockCode & "'"
        Dim sampleMachine As String = clsDBFuncationality.getSingleValue(sQuery, trans)
        Return sampleMachine
    End Function

    Public Shared Function DefaultSampleComport(ByVal SNo1To4 As Integer, ByVal DockCode As String, ByVal trans As SqlTransaction) As String
        Dim sQuery As String = "select  Default_Sample_Comport_" + clsCommon.myCstr(SNo1To4) + "  from TSPL_DOCK_MASTER where Code='" & DockCode & "'"
        Dim sampleComport As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sQuery, trans))
        Return sampleComport
    End Function
End Class
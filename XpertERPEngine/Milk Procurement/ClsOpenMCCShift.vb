Imports System.Data.SqlClient
Imports common

Public Class ClsOpenMCCShift
#Region "variables"
    Public MCC_SHIFT_CODE As String = ""
    Public MCC_CODE As String = ""
    Public SHIFT As String = ""
    Public Remarks As String = ""
    Public MCC_SHIFT_DATE As Date? = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As Date? = Nothing
    Public MCC_NAME As String = ""
    Public System_Stock As Double = 0
    Public System_FAT_Per As Double = 0
    Public System_SNF_Per As Double = 0
    Public Actual_Stock As Double = 0
    Public Actual_FAT As Double = 0
    Public Actual_SNF As Double = 0
    Public Manual_Stock As Double = 0
    Public Manual_FAT As Double = 0
    Public Manual_SNF As Double = 0
    Public Manual_FAT_Per As Double = 0
    Public Manual_SNF_Per As Double = 0
    Public Actual_FAT_Per As Double = 0
    Public Actual_SNF_Per As Double = 0
    Public Is_Holiday As String = ""
    Public Status As String = ""
    Public Is_Manual As String = ""
    Public Is_Manual_Weighment As String = ""
    Public Is_Allow_Manual_Gate_Entry_Weighment As String = ""
    Public Is_Regular As Integer = 0
    Public Irregular_MCC_Code As String = ""
    Public Irregular_MCC_NAME As String = ""
    Public Form_ID As String = ""
    Public CLR As Double = 0
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
#End Region
    Public Function SaveData(ByVal obj As ClsOpenMCCShift, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
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
    Public Shared Function SaveData(ByVal obj As ClsOpenMCCShift, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmOpenMCCShift, obj.MCC_CODE, obj.MCC_SHIFT_DATE, trans)
            'If isNewEntry Then
            '    qry = "select  MAX(MCC_SHIFT_CODE)  from TSPL_OPEN_MCC_SHIFT"
            '    obj.MCC_SHIFT_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            '    If clsCommon.myLen(obj.MCC_SHIFT_CODE) <= 0 Then
            '        ' obj.MCC_SHIFT_CODE = "OMS0001"
            '        obj.MCC_SHIFT_CODE = clsERPFuncationality.GetNextCode(trans, obj.MCC_SHIFT_DATE, "Open MCC Shift", "", obj.MCC_CODE)
            '    Else
            '        obj.MCC_SHIFT_CODE = clsCommon.incval(obj.MCC_SHIFT_CODE)
            '    End If
            'End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
            clsCommon.AddColumnsForChange(coll, "MCC_SHIFT_DATE", clsCommon.GetPrintDate(obj.MCC_SHIFT_DATE, "dd/MMM/yyyy hh:mm tt")) 'clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "SHIFT", obj.SHIFT)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "System_Stock", obj.System_Stock)
            clsCommon.AddColumnsForChange(coll, "System_SNF_Per", obj.System_SNF_Per)
            clsCommon.AddColumnsForChange(coll, "System_FAT_Per", obj.System_FAT_Per)
            '===========Added By Rohit to save Actual and Manual Stock,FAT,SNF
            clsCommon.AddColumnsForChange(coll, "Actual_Stock", obj.Actual_Stock)
            clsCommon.AddColumnsForChange(coll, "Actual_FAT", obj.Actual_FAT)
            clsCommon.AddColumnsForChange(coll, "Actual_SNF_Per", obj.Actual_SNF_Per)
            clsCommon.AddColumnsForChange(coll, "Actual_FAT_Per", obj.Actual_FAT_Per)
            clsCommon.AddColumnsForChange(coll, "Actual_SNF", obj.Actual_SNF)
            clsCommon.AddColumnsForChange(coll, "Manual_Stock", obj.Manual_Stock)
            clsCommon.AddColumnsForChange(coll, "Manual_FAT", obj.Manual_FAT)
            clsCommon.AddColumnsForChange(coll, "Manual_SNF", obj.Manual_SNF)
            clsCommon.AddColumnsForChange(coll, "Manual_FAT_Per", obj.Manual_FAT_Per)
            clsCommon.AddColumnsForChange(coll, "Manual_SNF_Per", obj.Manual_SNF_Per)
            clsCommon.AddColumnsForChange(coll, "Is_Regular", obj.Is_Regular)
            clsCommon.AddColumnsForChange(coll, "IrRegular_Mcc_Code", obj.Irregular_MCC_Code)
            clsCommon.AddColumnsForChange(coll, "CLR", obj.CLR)
            '============================================
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            '' update Sync Satatus
            clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", 0)
            If isNewEntry Then
                'obj.MCC_SHIFT_CODE = clsERPFuncationality.GetNextCode(trans, obj.MCC_SHIFT_DATE, "Open MCC Shift", "", obj.MCC_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "MCC_SHIFT_CODE", obj.MCC_SHIFT_CODE)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OPEN_MCC_SHIFT", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OPEN_MCC_SHIFT", OMInsertOrUpdate.Update, "TSPL_OPEN_MCC_SHIFT.MCC_SHIFT_CODE='" + obj.MCC_SHIFT_CODE + "'", trans)
            End If
            IsSaved = IsSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.MCC_SHIFT_CODE, obj.arrCustomFields, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    'Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsOpenMCCShift
    '    Return GetData(strCode, NavType, Nothing)
    'End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As ClsOpenMCCShift
        Dim obj As ClsOpenMCCShift = Nothing
        Dim Arr As List(Of ClsOpenMCCShift) = Nothing
        Dim qry As String = "SELECT TSPL_OPEN_MCC_SHIFT.MCC_SHIFT_CODE,TSPL_OPEN_MCC_SHIFT.MCC_CODE,TSPL_OPEN_MCC_SHIFT.SHIFT,TSPL_OPEN_MCC_SHIFT.Remarks,Is_Allow_Manual_Entry_Weighment,TSPL_OPEN_MCC_SHIFT.MCC_SHIFT_DATE, TSPL_MCC_MASTER.MCC_NAME As [MCC NAME],System_Stock,System_SNF_Per,System_FAT_Per,Actual_Stock,Actual_FAT,Actual_FAT_Per,Actual_SNF_Per,Actual_SNF,Manual_Stock,Manual_FAT,Manual_SNF,Manual_FAT_Per,Manual_SNF_Per,Is_Allow_Manual_Entry,status,is_milk_holiday,is_regular,Irregular_Mcc_code,Irregular_MCC_MASTER.Mcc_name as Irregular_Mcc_Name,TSPL_OPEN_MCC_SHIFT.Is_Allow_Manual_Gate_Entry_Weighment,TSPL_OPEN_MCC_SHIFT.CLR FROM TSPL_OPEN_MCC_SHIFT LEFT OUTER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code =TSPL_OPEN_MCC_SHIFT.MCC_CODE LEFT OUTER JOIN TSPL_MCC_MASTER  Irregular_MCC_MASTER ON Irregular_MCC_MASTER.MCC_Code =TSPL_OPEN_MCC_SHIFT.Irregular_MCC_CODE where 2=2 "
        Dim whrclas As String = ""

        If clsCommon.myLen(arrLoc) > 0 Then
            qry += " AND TSPL_OPEN_MCC_SHIFT.MCC_CODE IN (" + arrLoc + ")"
            whrclas = " AND TSPL_OPEN_MCC_SHIFT.MCC_CODE IN (" + arrLoc + ")"
        End If

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_OPEN_MCC_SHIFT.MCC_SHIFT_CODE = (select MIN(MCC_SHIFT_CODE) from TSPL_OPEN_MCC_SHIFT WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_OPEN_MCC_SHIFT.MCC_SHIFT_CODE = (select Max(MCC_SHIFT_CODE) from TSPL_OPEN_MCC_SHIFT WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_OPEN_MCC_SHIFT.MCC_SHIFT_CODE = (select TOP 1 MCC_SHIFT_CODE from TSPL_OPEN_MCC_SHIFT WHERE 1=1 " + whrclas + " and MCC_SHIFT_CODE='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_OPEN_MCC_SHIFT.MCC_SHIFT_CODE = (select Min(MCC_SHIFT_CODE) from TSPL_OPEN_MCC_SHIFT where MCC_SHIFT_CODE > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_OPEN_MCC_SHIFT.MCC_SHIFT_CODE = (select Max(MCC_SHIFT_CODE) from TSPL_OPEN_MCC_SHIFT where MCC_SHIFT_CODE < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsOpenMCCShift()
            obj.MCC_SHIFT_CODE = clsCommon.myCstr(dt.Rows(0)("MCC_SHIFT_CODE"))
            obj.MCC_CODE = clsCommon.myCstr(dt.Rows(0)("MCC_CODE"))
            obj.SHIFT = clsCommon.myCstr(dt.Rows(0)("SHIFT"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.MCC_SHIFT_DATE = clsCommon.myCstr(dt.Rows(0)("MCC_SHIFT_DATE"))
            obj.MCC_NAME = clsCommon.myCstr(dt.Rows(0)("MCC NAME"))
            '=============Added by Rohit for Stock,FAt and SNF==================
            obj.Actual_Stock = clsCommon.myCdbl(dt.Rows(0)("Actual_Stock"))
            obj.Actual_FAT = clsCommon.myCdbl(dt.Rows(0)("Actual_FAT"))
            obj.Actual_SNF = clsCommon.myCdbl(dt.Rows(0)("Actual_SNF"))
            obj.Actual_FAT_Per = clsCommon.myCdbl(dt.Rows(0)("Actual_FAT_Per"))
            obj.Actual_SNF_Per = clsCommon.myCdbl(dt.Rows(0)("Actual_SNF_Per"))
            obj.System_Stock = clsCommon.myCdbl(dt.Rows(0)("System_Stock"))
            obj.System_FAT_Per = clsCommon.myCdbl(dt.Rows(0)("System_FAT_Per"))
            obj.System_SNF_Per = clsCommon.myCdbl(dt.Rows(0)("System_SNF_Per"))
            obj.Manual_Stock = clsCommon.myCdbl(dt.Rows(0)("Manual_Stock"))
            obj.Manual_FAT = clsCommon.myCdbl(dt.Rows(0)("Manual_FAT"))
            obj.Manual_SNF = clsCommon.myCdbl(dt.Rows(0)("Manual_SNF"))
            obj.Manual_FAT_Per = clsCommon.myCdbl(dt.Rows(0)("Manual_FAT_Per"))
            obj.Manual_SNF_Per = clsCommon.myCdbl(dt.Rows(0)("Manual_SNF_Per"))
            obj.Is_Manual = clsCommon.myCstr(dt.Rows(0)("Is_Allow_Manual_Entry"))
            obj.Is_Manual_Weighment = clsCommon.myCstr(dt.Rows(0)("Is_Allow_Manual_Entry_Weighment"))
            obj.Is_Allow_Manual_Gate_Entry_Weighment = clsCommon.myCstr(dt.Rows(0)("Is_Allow_Manual_Gate_Entry_Weighment"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.Is_Holiday = clsCommon.myCstr(dt.Rows(0)("is_milk_holiday"))
            obj.Is_Regular = clsCommon.myCstr(dt.Rows(0)("Is_Regular"))
            obj.Irregular_MCC_Code = clsCommon.myCstr(dt.Rows(0)("Irregular_Mcc_Code"))
            obj.Irregular_MCC_NAME = clsCommon.myCstr(dt.Rows(0)("Irregular_Mcc_name"))
            '===================================================
            obj.CLR = clsCommon.myCdbl(dt.Rows(0)("CLR"))

        End If
        Return obj
    End Function

    Public Shared Function Getstock(ByVal shift_date As Date, ByVal Mcc_Code As String) As DataTable
        ''richa agarwal TEC/28/03/19-000462 add item structure on setting based
        Dim ItemStructureMandatoryOnWeightConversion As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False))
        Dim DtStock As DataTable = clsDBFuncationality.GetDataTable("select round(sum(Qty),2) as Qty,round(SUM(FAT),2) as FAT,round(SUM(SNF),2) as SNF,Item_COde " _
                 & " from (select case when InOut='O'  then case when UOM='ltr' then -Qty*(Contained_Qty)  else -qty end else case when UOM='ltr' then " _
                 & " Qty*(Contained_Qty)  else qty end  end as Qty,case when InOut='O' then -FAT_KG else FAT_Kg end as FAT,case when InOut='O' then -SNF_KG " _
                 & " else SNF_KG end as SNF,Item_Code from TSPL_INVENTORY_MOVEMENT_NEW left join (select * from TSPL_WEIGHT_CONVERSION where " _
                 & " Container_UOM='ltr' and Contained_UOM='kg') conv on conv.Container_UOM=UOM and Contained_UOM='kg' where Item_Code=(select " _
                 & " Description from TSPL_FIXED_PARAMETER where Type='MCCDefaultMilkItem')" _
                 & " " & IIf(ItemStructureMandatoryOnWeightConversion = True, " and isnull(conv.Structure_Code,'') =(select Structure_Code  from TSPL_ITEM_MASTER where item_code=(select Description from TSPL_FIXED_PARAMETER where Type='MCCDefaultMilkItem'))", "") & " " _
                  & "  and convert(date,Source_Doc_Date,103)<=convert(date,'" & shift_date & "',103) and TSPL_INVENTORY_MOVEMENT_NEW.location_code='" & Mcc_Code & "')  tt group by Item_Code")
        Return DtStock
    End Function

    Public Shared Function GetShift() As DataTable
        Dim DT_Shift As DataTable = New DataTable
        DT_Shift.Columns.Add("Code", GetType(String))
        DT_Shift.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_Shift.NewRow()
        DR("Name") = "Morning"
        DR("Code") = "M"
        DT_Shift.Rows.Add(DR)

        DR = DT_Shift.NewRow()
        DR("Name") = "Evening"
        DR("Code") = "E"
        DT_Shift.Rows.Add(DR)
        DT_Shift.AcceptChanges()

        Return DT_Shift
    End Function

    'Public Shared Function CheckisShiftTimingAvailable(ByVal Mcc_Code As String, ByVal Shift As String)
    '    Try
    '        Dim sQuery As String = String.Empty
    '        If Shift.ToString.Contains("M") Then
    '            sQuery = "select Count(*) from tspl_Mcc_Master where Mcc_Code='" & Mcc_Code & "' and convert(time,shift_Opening_Time,103)<=" _
    '                & " convert(time,'" & clsCommon.GETSERVERDATE(Nothing, "HH:mm:ss tt") & "',103)  and convert(time,shift_Closing_Time,103)>= convert(time,'" & clsCommon.GETSERVERDATE(Nothing, "HH:mm:ss tt") & "',103)"
    '        Else
    '            sQuery = "select Count(*) from tspl_Mcc_Master where Mcc_Code='" & Mcc_Code & "' and convert(time,shift_Eve_Opening_Time,103)<=" _
    '                & " convert(time,'" & clsCommon.GETSERVERDATE(Nothing, "HH:mm:ss tt") & "',103)  and convert(time,shift_Eve_Closing_Time,103)>= convert(time,'" & clsCommon.GETSERVERDATE(Nothing, "HH:mm:ss tt") & "',103)"
    '        End If
    '        Dim Count As Integer = clsDBFuncationality.ExecuteNonQuery(sQuery)
    '        Return Count
    '    Catch ex As Exception
    '        Return ex.ToString()
    '    End Try
    'End Function

    Public Shared Function CheckisShiftTimingAvailable(ByVal Mcc_Code As String, ByVal Shift As String)
        Try
            Dim strFromColumn As String
            Dim strToColumn As String
            If Shift.ToString.Contains("M") Then
                strFromColumn = " shift_Opening_Time "
                strToColumn = " shift_Closing_Time "
            Else
                strFromColumn = " shift_Eve_Opening_Time "
                strToColumn = " shift_Eve_Closing_Time "
            End If

            Dim sQuery As String = "select COUNT(*) from (" & _
            " select OPTime,(case when CLTime < OPTime then DATEADD(day,1, CLTime) else CLTime end) as CLTime  from ( " & _
            " select cast( (replace( convert( varchar, GETDATE(),106),' ','/')+' '+" + strFromColumn + ") as datetime) as OPTime , " & _
            " cast( (replace( convert( varchar, GETDATE(),106),' ','/')+' '+" + strToColumn + ") as datetime) as CLTime  " & _
            " from TSPL_MCC_MASTER where MCC_Code='" + Mcc_Code + "')X " & _
            " )xx " & _
            " where 2=(case when OPTime=CLTime then 2 else " & _
            " case when  GETDATE()>=OPTime and GETDATE()<= CLTime  then 2 else 1 end end) "

            Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sQuery))
        Catch ex As Exception
            Return ex.ToString()
        End Try
    End Function
End Class

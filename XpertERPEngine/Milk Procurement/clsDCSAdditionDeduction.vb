Imports System.Data.SqlClient

Public Class clsDCSAdditionDeduction

#Region "Variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Start_Date As DateTime
    Public End_Date As Date? = Nothing
    Public Inactive As Boolean = False
    Public Saving As Integer = False ''0-Normal,1-Saving,2-Compulsory
    Public SNo As Integer
    Public Applicable_DCS_Type As Integer ''0-All,1=DCS,2=PDCS,3=BMC,4=Cluster,5=BMC Truck Sheet
    Public Nature_Type As Integer ''0=Addition,1=Deduction
    Public Applicable_Type As Integer ''0=Rate,1=Percent
    Public Applicable_On As Integer ''0=Qty,1=Amt
    Public Qty_UOM As Integer ''0=Receving UOM,1=Ltr,2=Kg
    Public Applicable_Value As Decimal
    Public GL_Account As String = Nothing
    Public MappingCode As String = Nothing
    Public Milk_Type As String
    Public RO_Decimal_Places As Integer
    Public RO_Increase_After As Integer
    Public Apply_TDS As Boolean
    Public Include_Shortage_Own_BMC As Boolean
    Public Subtract As Boolean
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Check_Saving_AC As Integer = 0


    Public Arr As ArrayList = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsDCSAdditionDeduction, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsDCSAdditionDeduction, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "Delete from TSPL_DCS_ADDITION_DEDUCTION_ADD_AMT  where Code='" + obj.Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
            If obj.End_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
                clsCommon.AddColumnsForChange(coll, "End_Date_Created_By", Nothing, True)
                clsCommon.AddColumnsForChange(coll, "End_Date_Created_Date", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "End_Date_Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "End_Date_Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            End If
            clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
            clsCommon.AddColumnsForChange(coll, "Saving", obj.Saving)
            clsCommon.AddColumnsForChange(coll, "Applicable_DCS_Type", obj.Applicable_DCS_Type)
            clsCommon.AddColumnsForChange(coll, "Nature_Type", obj.Nature_Type)
            clsCommon.AddColumnsForChange(coll, "Applicable_On", obj.Applicable_On)
            clsCommon.AddColumnsForChange(coll, "Qty_UOM", obj.Qty_UOM)
            clsCommon.AddColumnsForChange(coll, "Applicable_Type", obj.Applicable_Type)
            clsCommon.AddColumnsForChange(coll, "Applicable_Value", obj.Applicable_Value)
            clsCommon.AddColumnsForChange(coll, "GL_Account", obj.GL_Account)
            clsCommon.AddColumnsForChange(coll, "MappingCode", obj.MappingCode)
            clsCommon.AddColumnsForChange(coll, "Milk_Type", obj.Milk_Type, True)
            clsCommon.AddColumnsForChange(coll, "RO_Decimal_Places", obj.RO_Decimal_Places)
            clsCommon.AddColumnsForChange(coll, "RO_Increase_After", obj.RO_Increase_After)
            clsCommon.AddColumnsForChange(coll, "Apply_TDS", IIf(obj.Apply_TDS, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Include_Shortage_Own_BMC", IIf(obj.Include_Shortage_Own_BMC, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Subtract", IIf(obj.Subtract, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Check_Saving_AC", obj.Check_Saving_AC)
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.Start_Date, clsDocType.DCSAdditionDeduction, "", "")
                If (clsCommon.myLen(obj.Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_ADDITION_DEDUCTION", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_ADDITION_DEDUCTION", OMInsertOrUpdate.Update, "TSPL_DCS_ADDITION_DEDUCTION.Code='" + obj.Code + "'", trans)
            End If
            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                For Each str As String In obj.Arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                    clsCommon.AddColumnsForChange(coll, "Add_Of_Add_Ded_Code", str)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_ADDITION_DEDUCTION_ADD_AMT", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_DCS_ADDITION_DEDUCTION", "Code", "TSPL_DCS_ADDITION_DEDUCTION_ADD_AMT", "Code", trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveEndDateData(ByVal obj As clsDCSAdditionDeduction) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_DCS_ADDITION_DEDUCTION", "Code", "", "", "", "", trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "End_Date_Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "End_Date_Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_ADDITION_DEDUCTION", OMInsertOrUpdate.Update, "TSPL_DCS_ADDITION_DEDUCTION.Code='" + obj.Code + "'", trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String = ""
        Try
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_DCS_ADDITION_DEDUCTION", "Code", "TSPL_DCS_ADDITION_DEDUCTION_ADD_AMT", "Code", tran)

            qry = "Delete from TSPL_DCS_ADDITION_DEDUCTION_ADD_AMT  where Code='" + strCode + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "Delete from TSPL_DCS_ADDITION_DEDUCTION where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            tran.Commit()
        Catch err As Exception
            tran.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsDCSAdditionDeduction
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDCSAdditionDeduction
        Dim obj As clsDCSAdditionDeduction = Nothing
        Dim qry As String = ""
        qry = " select TSPL_DCS_ADDITION_DEDUCTION.* from TSPL_DCS_ADDITION_DEDUCTION where 2=2 "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DCS_ADDITION_DEDUCTION.Code = (select MIN(Code) from TSPL_DCS_ADDITION_DEDUCTION where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_DCS_ADDITION_DEDUCTION.Code = (select Max(Code) from TSPL_DCS_ADDITION_DEDUCTION where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_DCS_ADDITION_DEDUCTION.Code = (select Min(Code) from TSPL_DCS_ADDITION_DEDUCTION where Code>'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Previous
                qry += " and TSPL_DCS_ADDITION_DEDUCTION.Code = (select Max(Code) from TSPL_DCS_ADDITION_DEDUCTION where Code<'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_DCS_ADDITION_DEDUCTION.Code = '" + strDocumentNo + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDCSAdditionDeduction()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Start_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
            If dt.Rows(0)("End_Date") IsNot DBNull.Value Then
                obj.End_Date = clsCommon.myCDate(dt.Rows(0)("End_Date"))
            Else
                obj.End_Date = Nothing
            End If
            obj.Inactive = (clsCommon.myCdbl(dt.Rows(0)("Inactive")) = 1)
            obj.Saving = clsCommon.myCdbl(dt.Rows(0)("Saving"))
            obj.SNo = clsCommon.myCdbl(dt.Rows(0)("SNo"))
            obj.Applicable_DCS_Type = clsCommon.myCDecimal(dt.Rows(0)("Applicable_DCS_Type"))
            obj.Nature_Type = clsCommon.myCDecimal(dt.Rows(0)("Nature_Type"))
            obj.Applicable_On = clsCommon.myCDecimal(dt.Rows(0)("Applicable_On"))
            obj.Qty_UOM = clsCommon.myCDecimal(dt.Rows(0)("Qty_UOM"))
            obj.Applicable_Type = clsCommon.myCDecimal(dt.Rows(0)("Applicable_Type"))
            obj.Applicable_Value = clsCommon.myCDecimal(dt.Rows(0)("Applicable_Value"))
            obj.GL_Account = clsCommon.myCstr(dt.Rows(0)("GL_Account"))
            obj.MappingCode = clsCommon.myCstr(dt.Rows(0)("MappingCode"))
            obj.Milk_Type = clsCommon.myCstr(dt.Rows(0)("Milk_Type"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.RO_Decimal_Places = clsCommon.myCDecimal(dt.Rows(0)("RO_Decimal_Places"))
            obj.RO_Increase_After = clsCommon.myCDecimal(dt.Rows(0)("RO_Increase_After"))
            obj.Apply_TDS = IIf(clsCommon.myCdbl(dt.Rows(0)("Apply_TDS")) = 1, True, False)
            obj.Include_Shortage_Own_BMC = IIf(clsCommon.myCdbl(dt.Rows(0)("Include_Shortage_Own_BMC")) = 1, True, False)
            obj.Subtract = IIf(clsCommon.myCdbl(dt.Rows(0)("Subtract")) = 1, True, False)
            obj.Check_Saving_AC = clsCommon.myCdbl(dt.Rows(0)("Check_Saving_AC"))
            obj.Arr = Nothing
            qry = " select Add_Of_Add_Ded_Code from TSPL_DCS_ADDITION_DEDUCTION_ADD_AMT where Code='" + obj.Code + "' "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New ArrayList()
                For Each dr As DataRow In dt.Rows
                    obj.Arr.Add(clsCommon.myCstr(dr("Add_Of_Add_Ded_Code")))
                Next
            End If
        End If

        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim obj As clsDCSAdditionDeduction = clsDCSAdditionDeduction.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Posted = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post Date")
            End If

            Dim qry As String = "Update TSPL_DCS_ADDITION_DEDUCTION set Posted=1,Posted_By='" + objCommonVar.CurrentUserCode + "',Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") + "' where Code='" + obj.Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function InactiveData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found")
            End If
            Dim obj As clsDCSAdditionDeduction = clsDCSAdditionDeduction.GetData(strCode, NavigatorType.Current, trans)
            If obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0 Then
                Throw New Exception("Invalid code [" + strCode + "]")
            End If
            If obj.Posted <> ERPTransactionStatus.Approved Then
                Throw New Exception("Should be posted Document " + obj.Code)
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Inactive", 1)
            clsCommon.AddColumnsForChange(coll, "Inactive_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Inactive_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_ADDITION_DEDUCTION", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetLatestCodeByDate(ByVal FromDate As DateTime, ByVal ToDate As DateTime) As String
        Dim qry As String = "select TabDateRange.thedate, TSPL_DCS_ADDITION_DEDUCTION.Code,TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type,TSPL_DCS_ADDITION_DEDUCTION.Nature_Type,TSPL_DCS_ADDITION_DEDUCTION.Applicable_On,TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type,TSPL_DCS_ADDITION_DEDUCTION.Applicable_Value,TSPL_DCS_ADDITION_DEDUCTION.GL_Account
from ExplodeDates('" + clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") + "') as TabDateRange 
inner join TSPL_DCS_ADDITION_DEDUCTION on TabDateRange.thedate>=TSPL_DCS_ADDITION_DEDUCTION.Start_Date
where TSPL_DCS_ADDITION_DEDUCTION.Posted=1 and isnull(TSPL_DCS_ADDITION_DEDUCTION.Inactive,0)=0
and (2= case when TSPL_DCS_ADDITION_DEDUCTION.End_Date is null then 2 else case when TabDateRange.thedate<= TSPL_DCS_ADDITION_DEDUCTION.End_Date then 2 else 3 end end)  
order by TabDateRange.thedate"
        Return qry
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_DCS_ADDITION_DEDUCTION.Code,TSPL_DCS_ADDITION_DEDUCTION.Description,TSPL_DCS_ADDITION_DEDUCTION.Start_Date,End_Date from TSPL_DCS_ADDITION_DEDUCTION "
        str = clsCommon.ShowSelectForm("DCSAdDd", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function

    Public Shared Function getFinderObeject(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As clsDCSAdditionDeduction
        Dim obj As clsDCSAdditionDeduction = Nothing
        Dim strCode As String = getFinder(whrcls, curcode, isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            obj = GetData(strCode, NavigatorType.Current)
        End If
        Return obj
    End Function

    Public Shared Function GetName(ByVal Code As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_DCS_ADDITION_DEDUCTION where Code='" + Code + "'"))
    End Function

End Class



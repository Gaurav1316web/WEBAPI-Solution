Imports System.Data.SqlClient
Imports common

Public Class clsOwnBMCGainLossRate

#Region "Variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Start_Date As DateTime
    Public End_Date As Date? = Nothing
    Public Gain_FAT_Rate As Decimal = Nothing
    Public Gain_SNF_Rate As Decimal = Nothing
    Public Gain_FAT_Allow As Decimal = Nothing
    Public Gain_SNF_Allow As Decimal = Nothing

    Public Loss_FAT_Rate As Decimal = Nothing
    Public Loss_SNF_Rate As Decimal = Nothing
    Public Loss_FAT_Allow As Decimal = Nothing
    Public Loss_SNF_Allow As Decimal = Nothing
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Inactive As Boolean = False

#End Region

    Public Function SaveData(ByVal obj As clsOwnBMCGainLossRate, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsOwnBMCGainLossRate, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
            If obj.End_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Gain_FAT_Rate", obj.Gain_FAT_Rate)
            clsCommon.AddColumnsForChange(coll, "Gain_SNF_Rate", obj.Gain_SNF_Rate)
            clsCommon.AddColumnsForChange(coll, "Gain_FAT_Allow", obj.Gain_FAT_Allow, True)
            clsCommon.AddColumnsForChange(coll, "Gain_SNF_Allow", obj.Gain_SNF_Allow, True)
            clsCommon.AddColumnsForChange(coll, "Loss_FAT_Rate", obj.Loss_FAT_Rate)
            clsCommon.AddColumnsForChange(coll, "Loss_SNF_Rate", obj.Loss_SNF_Rate)
            clsCommon.AddColumnsForChange(coll, "Loss_FAT_Allow", obj.Loss_FAT_Allow, True)
            clsCommon.AddColumnsForChange(coll, "Loss_SNF_Allow", obj.Loss_SNF_Allow, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                'obj.Code = clsERPFuncationality.GetNextCode(trans, obj.Start_Date, clsDocType.OwnBMCGainLossRate, "", "")
                'If (clsCommon.myLen(obj.Code) <= 0) Then
                'Throw New Exception("Error in Code Generating")
                'End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OWN_BMC_GAIN_LOSS_RATE", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OWN_BMC_GAIN_LOSS_RATE", OMInsertOrUpdate.Update, "TSPL_OWN_BMC_GAIN_LOSS_RATE.Code='" + obj.Code + "'", trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String = ""
        Try
            qry = "Delete from TSPL_OWN_BMC_GAIN_LOSS_RATE where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            tran.Commit()
        Catch err As Exception
            tran.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsOwnBMCGainLossRate
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsOwnBMCGainLossRate
        Dim obj As clsOwnBMCGainLossRate = Nothing
        Dim qry As String = ""
        qry = " select * from TSPL_OWN_BMC_GAIN_LOSS_RATE where 2=2 "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_OWN_BMC_GAIN_LOSS_RATE.Code = (select MIN(Code) from TSPL_OWN_BMC_GAIN_LOSS_RATE where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_OWN_BMC_GAIN_LOSS_RATE.Code = (select Max(Code) from TSPL_OWN_BMC_GAIN_LOSS_RATE where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_OWN_BMC_GAIN_LOSS_RATE.Code = (select Min(Code) from TSPL_OWN_BMC_GAIN_LOSS_RATE where Code>'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Previous
                qry += " and TSPL_OWN_BMC_GAIN_LOSS_RATE.Code = (select Max(Code) from TSPL_OWN_BMC_GAIN_LOSS_RATE where Code<'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_OWN_BMC_GAIN_LOSS_RATE.Code = '" + strDocumentNo + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsOwnBMCGainLossRate()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Start_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
            If dt.Rows(0)("End_Date") IsNot DBNull.Value Then
                obj.End_Date = clsCommon.myCDate(dt.Rows(0)("End_Date"))
            Else
                obj.End_Date = Nothing
            End If
            obj.Gain_FAT_Rate = clsCommon.myCdbl(dt.Rows(0)("Gain_FAT_Rate"))
            obj.Gain_SNF_Rate = clsCommon.myCdbl(dt.Rows(0)("Gain_SNF_Rate"))

            obj.Gain_FAT_Allow = clsCommon.myCDecimal(dt.Rows(0)("Gain_FAT_Allow"))
            obj.Gain_SNF_Allow = clsCommon.myCDecimal(dt.Rows(0)("Gain_SNF_Allow"))

            obj.Loss_FAT_Rate = clsCommon.myCdbl(dt.Rows(0)("Loss_FAT_Rate"))
            obj.Loss_SNF_Rate = clsCommon.myCdbl(dt.Rows(0)("Loss_SNF_Rate"))

            obj.Loss_FAT_Allow = clsCommon.myCDecimal(dt.Rows(0)("Loss_FAT_Allow"))
            obj.Loss_SNF_Allow = clsCommon.myCDecimal(dt.Rows(0)("Loss_SNF_Allow"))

            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Inactive = (clsCommon.myCdbl(dt.Rows(0)("Inactive")) = 1)
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
            Dim obj As clsOwnBMCGainLossRate = clsOwnBMCGainLossRate.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Posted = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post Date")
            End If

            Dim qry As String = "Update TSPL_OWN_BMC_GAIN_LOSS_RATE set Posted=1,Posted_By='" + objCommonVar.CurrentUserCode + "',Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") + "' where Code='" + obj.Code + "' "
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
            Dim obj As clsOwnBMCGainLossRate = clsOwnBMCGainLossRate.GetData(strCode, NavigatorType.Current, trans)
            'If obj Is Nothing OrElse obj.ArrMCC.Count <= 0 Then
            '    Throw New Exception("Invalid code")
            'End If
            If obj.Posted <> ERPTransactionStatus.Approved Then
                Throw New Exception("Should be posted Document " + obj.Code)
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Inactive", 1)
            clsCommon.AddColumnsForChange(coll, "Inactive_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Inactive_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OWN_BMC_GAIN_LOSS_RATE", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_OWN_BMC_GAIN_LOSS_RATE.Code,TSPL_OWN_BMC_GAIN_LOSS_RATE.Description,TSPL_OWN_BMC_GAIN_LOSS_RATE.Start_Date,End_Date from TSPL_OWN_BMC_GAIN_LOSS_RATE "
        str = clsCommon.ShowSelectForm("CAPing", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function

    Public Shared Function getFinderObeject(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As clsOwnBMCGainLossRate
        Dim obj As clsOwnBMCGainLossRate = Nothing
        Dim strCode As String = getFinder(whrcls, curcode, isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            obj = GetData(strCode, NavigatorType.Current)
        End If
        Return obj
    End Function

End Class

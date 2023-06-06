Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports System.Drawing


Public Class clsScreenNotificationSchedule
    Public Scheduling As Char = "N"
    Public Module_Code As String = Nothing
    Public Screen_Code As String = Nothing
    Public Criteria As String = Nothing
    Public Before_After As String = Nothing
    Public Days As Integer = 0
    Public Quarter As String = Nothing
    Public Notification As String = ""
    Public Validation As String = Nothing
    Public Level As String = "Screen"

    Public Shared Function SaveData(ByVal Arr As List(Of clsScreenNotificationSchedule)) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            If clsScreenNotificationSchedule.SaveData(Arr, trans) Then
                trans.Commit()
            End If
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal Arr As List(Of clsScreenNotificationSchedule), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Count As Integer = 0
            Dim qry As String
            For Each obj As clsScreenNotificationSchedule In Arr
                qry = "Select COUNT(*) from TSPL_SCREEN_NOTIFICATION_SETTING WHERE Module_Code='" + obj.Module_Code + "' AND Screen_Code='" + obj.Screen_Code + "' AND Criteria='" + obj.Criteria + "' AND Quarter='" + obj.Quarter + "' AND Level='" + obj.Level + "'"
                Count = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Scheduling", obj.Scheduling)
                'clsCommon.AddColumnsForChange(coll, "Before_After", obj.Before_After)
                clsCommon.AddColumnsForChange(coll, "Days", obj.Days)
                clsCommon.AddColumnsForChange(coll, "Notification", obj.Notification)
                clsCommon.AddColumnsForChange(coll, "Validation", obj.Validation)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                If Count <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "Module_Code", obj.Module_Code)
                    clsCommon.AddColumnsForChange(coll, "Screen_Code", obj.Screen_Code)
                    clsCommon.AddColumnsForChange(coll, "Criteria", obj.Criteria)
                    clsCommon.AddColumnsForChange(coll, "Quarter", obj.Quarter)
                    clsCommon.AddColumnsForChange(coll, "Level", obj.Level)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCREEN_NOTIFICATION_SETTING", OMInsertOrUpdate.Insert, "", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCREEN_NOTIFICATION_SETTING", OMInsertOrUpdate.Update, "Module_Code='" + obj.Module_Code + "' AND Screen_Code='" + obj.Screen_Code + "' AND Criteria='" + obj.Criteria + "' AND Quarter='" + obj.Quarter + "' AND Level='" + obj.Level + "'", trans)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetScreenData(ByVal strModuleCode As String, ByVal DocType As String) As DataTable
        Try
            Dim qry As String = "Select Cast(Case When XXX.Scheduling='Y' Then 1 Else 0 End As Bit) as Scheduling, Program_Code as Screen_Code, Program_Name," & _
        " XXX.Criteria, XXX.Days, XXX.Quarter, XXX.Notification, XXX.Validation, XXX.Level from TSPL_PROGRAM_MASTER" & _
        " LEFT OUTER JOIN (Select Scheduling, Screen_Code, Criteria, Days, Quarter, Notification, Validation, Level from TSPL_SCREEN_NOTIFICATION_SETTING WHERE Level='Screen') XXX" & _
        " ON XXX.Screen_Code=TSPL_PROGRAM_MASTER.Program_Code" & _
        " WHERE Parent_Code in (Select Program_Code from TSPL_PROGRAM_MASTER Where Parent_Code='" + strModuleCode + "' AND Program_Code like '%" + DocType + "')"
            Return clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetLoginData(ByVal strModuleCode As String, ByVal DocType As String) As DataTable
        Try
            Dim qry As String = "Select Cast(Case When XXX.Scheduling='Y' Then 1 Else 0 End As Bit) as Scheduling, Program_Code as Screen_Code, Program_Name," & _
        " XXX.Criteria, XXX.Days, XXX.Quarter, XXX.Notification, XXX.Validation, XXX.Level from TSPL_PROGRAM_MASTER" & _
        " LEFT OUTER JOIN (Select Scheduling, Screen_Code, Criteria, Days, Quarter, Notification, Validation, Level from TSPL_SCREEN_NOTIFICATION_SETTING WHERE Level='Login') XXX" & _
        " ON XXX.Screen_Code=TSPL_PROGRAM_MASTER.Program_Code" & _
        " WHERE Parent_Code in (Select Program_Code from TSPL_PROGRAM_MASTER Where Parent_Code='" + strModuleCode + "' AND Program_Code like '%" + DocType + "')"
            Return clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetScreenNotificationInfo(ByVal strScreenCode As String, ByVal trans As SqlTransaction) As DataTable
        Try
            Dim qry As String = "select Criteria, Quarter, Notification, Validation from TSPL_SCREEN_NOTIFICATION_SETTING WHERE Level='Screen' AND Scheduling='Y' "
            qry += " AND Screen_Code='" + strScreenCode + "'"
            Return clsDBFuncationality.GetDataTable(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Sub ShowLoginNotifications(ByVal strUserCode As String)
        Try

            Dim sq As String = "select * from( select xxx.[Document Type] ,xxx.Reverse_Code ,xxx.Bank_Code,TSPL_BANK_MASTER.DESCRIPTION as [Bank Details] ,xxx.[Cheque No],xxx.[Cheque Date] ,xxx.Amount,TSPL_BANK_MASTER.Cheque_Validity_In_Days , DATEDIFF (DAY,convert(date,xxx.[Cheque Date],103),convert(date,CURRENT_TIMESTAMP,103)) as [DaysGone]  from (   select tspl_bank_reverse.Reverse_Document  as [Document Type],TSPL_BANK_REVERSE.Reverse_Code , TSPL_PAYMENT_HEADER.Bank_Code ,TSPL_PAYMENT_HEADER.Cheque_No as [Cheque No] ,convert(varchar,TSPL_PAYMENT_HEADER.Cheque_Date,103) as [Cheque Date] ,TSPL_PAYMENT_HEADER.Payment_Amount  as [Amount]  from TSPL_BANK_REVERSE left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No  where tspl_bank_reverse.Reverse_Document ='Payments' and TSPL_BANK_REVERSE.ischequeBounce='Y' and TSPL_Payment_HEADER.Payment_Code='Cheque'    union All     select tspl_bank_reverse.Reverse_Document  as [Document Type],TSPL_BANK_REVERSE.Reverse_Code,TSPL_RECEIPT_HEADER.Bank_Code , TSPL_RECEIPT_HEADER.Cheque_No as [Cheque No] ,convert(varchar,TSPL_RECEIPT_HEADER.Cheque_Date,103) as [Cheque Date] ,TSPL_RECEIPT_HEADER.Receipt_Amount as [Amount]  from TSPL_BANK_REVERSE left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_REVERSE.Document_No  where tspl_bank_reverse.Reverse_Document ='Receipts' and TSPL_BANK_REVERSE.ischequeBounce='Y' and TSPL_RECEIPT_HEADER.Payment_Code='Cheque') xxx  left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=xxx.Bank_Code ) yyy  where (yyy.DaysGone-yyy.Cheque_Validity_In_Days)>= (select zzz.days from (select Program_Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name,Parent_Code, TSPL_SCREEN_NOTIFICATION_SETTING.Days, TSPL_SCREEN_NOTIFICATION_SETTING.Notification,TSPL_SCREEN_NOTIFICATION_SETTING.criteria from TSPL_PROGRAM_MASTER    LEFT OUTER JOIN TSPL_SCREEN_NOTIFICATION_SETTING ON TSPL_SCREEN_NOTIFICATION_SETTING.Screen_Code=TSPL_PROGRAM_MASTER.Program_Code      where 2=2 and TSPL_SCREEN_NOTIFICATION_SETTING.Screen_Code='REVERSE-TRAN' and exists(select 1 from TSPL_GROUP_PROGRAM_MAPPING where TSPL_GROUP_PROGRAM_MAPPING.Authorized_Flag=1 and TSPL_GROUP_PROGRAM_MAPPING.Program_Code=TSPL_PROGRAM_MASTER.Program_Code and TSPL_GROUP_PROGRAM_MAPPING.Group_Code in (select Group_Code  from TSPL_USER_GROUP_MAPPING ))              AND TSPL_SCREEN_NOTIFICATION_SETTING.Scheduling='Y' AND TSPL_SCREEN_NOTIFICATION_SETTING.Level='Login')zzz) "
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sq)
            If dt1.Rows.Count > 0 Then
                ''To be Uncomment
                'FrmBounceAndExpiredChequeDetails.dgv.DataSource = dt1
                'FrmBounceAndExpiredChequeDetails.ShowDialog()
            End If

            Dim Count As Integer = 0
            Dim qry As String = "select Program_Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name,Parent_Code, TSPL_SCREEN_NOTIFICATION_SETTING.Days, TSPL_SCREEN_NOTIFICATION_SETTING.Notification,TSPL_SCREEN_NOTIFICATION_SETTING.criteria from TSPL_PROGRAM_MASTER"
            qry += " LEFT OUTER JOIN TSPL_SCREEN_NOTIFICATION_SETTING ON TSPL_SCREEN_NOTIFICATION_SETTING.Screen_Code=TSPL_PROGRAM_MASTER.Program_Code "
            qry += " where 2=2  and exists(select 1 from TSPL_GROUP_PROGRAM_MAPPING where TSPL_GROUP_PROGRAM_MAPPING.Authorized_Flag=1 and TSPL_GROUP_PROGRAM_MAPPING.Program_Code=TSPL_PROGRAM_MASTER.Program_Code and TSPL_GROUP_PROGRAM_MAPPING.Group_Code in (select Group_Code  from TSPL_USER_GROUP_MAPPING where User_Code='" + strUserCode + "')) "
            qry += " AND TSPL_SCREEN_NOTIFICATION_SETTING.Scheduling='Y' AND TSPL_SCREEN_NOTIFICATION_SETTING.Level='Login'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim strDate As String = Nothing
            For Each dr As DataRow In dt.Rows
                Count = 0
                Dim days As Integer = 0
                days = dr("days")

                Try
                    days = CInt(days)
                Catch ex As Exception
                    days = 0
                End Try

                If clsCommon.CompairString(clsUserMgtCode.PaymentEntryNew, dr("Program_Code")) = CompairStringResult.Equal Then

                    Count = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_PAYMENT_HEADER WHERE Posted<>'1' AND PDC_Cheque='Y' AND DATEDIFF(dd, GetDate(),Payment_Date)>=0")
                    If Count > 0 Then
                        clsCommon.MyMessageBoxShow(clsCommon.myCstr(dr("Notification")))
                    End If

                ElseIf clsCommon.CompairString(clsUserMgtCode.mbtnPurchaseOrder, dr("Program_Code")) = CompairStringResult.Equal Then
                    Count = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_PURCHASE_ORDER_HEAD WHERE Created_By='" + objCommonVar.CurrentUserCode + "' AND Status<>1")

                    '------------------14/04/2014-----------------------For Checking Delivery Date Vaidation-------------------------
                    If clsCommon.CompairString("Delivery Date", dr("criteria")) = CompairStringResult.Equal AndAlso CInt(days) >= 0 Then
                        Count = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_PURCHASE_ORDER_HEAD WHERE Created_By='" & objCommonVar.CurrentUserCode & "' AND Status<>1 and convert(date,PurchaseOrder_Date,103)<='" & strDate & "' and DATEDIFF(d,convert(date,delivery_date,103),convert(date,'" & strDate & "',103))>=" & days & "")
                    ElseIf clsCommon.CompairString("Delivery Date", dr("criteria")) = CompairStringResult.Equal AndAlso CInt(days) < 0 Then
                        Count = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_PURCHASE_ORDER_HEAD WHERE Created_By='" & objCommonVar.CurrentUserCode & "' AND Status<>1 and convert(date,PurchaseOrder_Date,103)>='" & strDate & "' and DATEDIFF(d,convert(date,delivery_date,103),convert(date,'" & strDate & "',103))<" & days & "")
                    End If
                    '-------------------------------------------------------------------------------------------------------------------

                    If Count > 0 Then
                        clsCommon.MyMessageBoxShow(clsCommon.myCstr(dr("Notification")))
                    End If
                ElseIf clsCommon.CompairString(clsUserMgtCode.frmSNSalesOrder, dr("Program_Code")) = CompairStringResult.Equal Then
                    strDate = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")

                    '-------------------------------14/04/2014-------------------------------------------------------
                    If clsCommon.CompairString("Delivery Date", dr("criteria")) = CompairStringResult.Equal AndAlso CInt(days) >= 0 Then
                        Count = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_SD_SALES_ORDER_HEAD WHERE Created_By='" & objCommonVar.CurrentUserCode & "' AND Status<>1 AND Convert(Date,Document_Date,103)<='" & strDate & "' AND DATEDIFF(d,Delivery_date,'" & strDate & "')>=" & days & "")
                    ElseIf clsCommon.CompairString("Delivery Date", dr("criteria")) = CompairStringResult.Equal AndAlso CInt(days) < 0 Then
                        Count = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_SD_SALES_ORDER_HEAD WHERE Created_By='" & objCommonVar.CurrentUserCode & "' AND Status<>1 AND Convert(Date,Document_Date,103)>='" & strDate & "' AND DATEDIFF(d,Delivery_date,'" & strDate & "')<" & days & "")
                    End If
                    '-------------------------------------------------------------------------------------------
                    If Count > 0 Then
                        clsCommon.MyMessageBoxShow(clsCommon.myCstr(dr("Notification")))
                    End If

                    'ElseIf clsCommon.CompairString(clsUserMgtCode.FrmBankGuaranteeMaster1, dr("program_code")) = CompairStringResult.Equal Then
                    Count = 0
                    strDate = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")

                    Dim xdate As String = ""

                    If clsCommon.CompairString("End Date", dr("criteria")) = CompairStringResult.Equal Then
                        xdate = "end_date"
                    ElseIf clsCommon.CompairString("Extended Date", dr("criteria")) = CompairStringResult.Equal Then
                        xdate = "extended_date"
                    End If

                    If CInt(days) >= 0 Then
                        Count = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_BANK_GUARANTEE_MASTER WHERE Created_By='" & objCommonVar.CurrentUserCode & "' AND Status<>'N'  and convert(date,Date,103)<=convert(date,'" & strDate & "',103) and DATEDIFF(d,convert(date," + xdate + ",103),convert(date,'" & strDate & "',103))>=" & days & "")
                    ElseIf CInt(days) < 0 Then
                        Count = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_BANK_GUARANTEE_MASTER WHERE Created_By='" & objCommonVar.CurrentUserCode & "' AND Status<>'N'  and convert(date,Date,103)>=convert(date,'" & strDate & "',103) and DATEDIFF(d,convert(date," + xdate + ",103),convert(date,'" & strDate & "',103))<" & days & "")
                    End If

                    If Count > 0 Then
                        clsCommon.MyMessageBoxShow(clsCommon.myCstr(dr("Notification")))
                    End If
                End If
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Shared Sub showNotification(ByVal message As String)
        Dim RadDesktopAlert1 As New RadDesktopAlert
        RadDesktopAlert1.AutoClose = True
        RadDesktopAlert1.ShowOptionsButton = False
        RadDesktopAlert1.AutoCloseDelay = 30
        RadDesktopAlert1.ShowCloseButton = True
        RadDesktopAlert1.FixedSize = New Size(529, 100)
        RadDesktopAlert1.CaptionText = " Notification: "
        RadDesktopAlert1.PopupAnimation = True
        RadDesktopAlert1.ContentText = message
        RadDesktopAlert1.Show()
    End Sub
    Public Shared Sub showNotification(ByVal message As String, ByVal caption As String)
        Dim RadDesktopAlert1 As New RadDesktopAlert
        RadDesktopAlert1.AutoClose = True
        'RadDesktopAlert1.ShowOptionsButton = True
        'RadDesktopAlert1.ButtonItems.Add 
        RadDesktopAlert1.AutoCloseDelay = 30
        RadDesktopAlert1.ShowCloseButton = True
        RadDesktopAlert1.FixedSize = New Size(529, 100)
        RadDesktopAlert1.CaptionText = caption
        RadDesktopAlert1.PopupAnimation = True
        RadDesktopAlert1.ContentText = message
        RadDesktopAlert1.Show()
    End Sub
End Class

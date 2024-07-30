' Created By Pankaj Jha on 03/07/204 Against Ticket No: BM00000002720
Imports System.Data.SqlClient
Imports common
Public Class clsPOBulkProc

    Public isNewEntry As Boolean = False
    Public PO_No As String = String.Empty
    Public Date_And_Time As Date = Nothing
    Public isPosted As Integer = 0
    Public Posting_Date As Date = Nothing
    Public location_Code As String = String.Empty
    Public Location_Desc As String = String.Empty
    Public Vendor_Code As String = String.Empty
    Public Vendor_Desc As String = String.Empty
    Public Created_By As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modify_By As String = String.Empty
    Public Modify_Date As String = String.Empty
    Public comp_code As String = String.Empty
    Public Arr As List(Of clsPOBulkProcDetails) = Nothing
    Public Price_Code As String = Nothing
    Public Rate As Integer = 0
    Public MIKL_TYPE_CODE As String = Nothing
    Public Qty As Integer = 0
    Public Gate_Entry_Type As String = Nothing

    Public Shared Function saveData(ByVal obj As clsPOBulkProc, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmGateEntrySale, obj.location_Code, obj.Date_And_Time, trans)
            Dim issaved As Boolean = True
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PO_No", obj.PO_No)
           
            '==============================Added by preeti gupta======================
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Date_And_Time", clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MMM/yyyy hh:mm:ss tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Date_And_Time", clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MMM/yyyy"))
            End If
            '===================================End=======================================

         
            clsCommon.AddColumnsForChange(coll, "isPosted", obj.isPosted)      
            clsCommon.AddColumnsForChange(coll, "location_Code", obj.location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code, True)
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
            clsCommon.AddColumnsForChange(coll, "MIKL_TYPE_CODE", obj.MIKL_TYPE_CODE, True)
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_Type", obj.Gate_Entry_Type)
            clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", clsCommon.myCstr(obj.comp_code))
            If obj.isNewEntry Or isHistory Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PO_BULK_MASTER", OMInsertOrUpdate.Insert, "", trans)

            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PO_BULK_MASTER", OMInsertOrUpdate.Update, "TSPL_PO_BULK_MASTER.PO_No='" + obj.PO_No + "'", trans)
            End If

            issaved = issaved AndAlso clsPOBulkProcDetails.SaveData(obj.PO_No, obj.Arr, trans)
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select TSPL_PO_BULK_MASTER.PO_No as [PONo] ,convert(varchar,TSPL_PO_BULK_MASTER.Date_And_Time,103) as [Date] ,case when TSPL_PO_BULK_MASTER.isPosted=0 then 'NO' else 'YES' end as [Posting Status],TSPL_PO_BULK_MASTER.location_Code as [Location Code] ,TSPL_PO_BULK_MASTER.Vendor_Code as [Vendor Code] ,TSPL_PO_BULK_MASTER.Created_By as [Created By] ,cast(convert(date,TSPL_PO_BULK_MASTER.Created_Date,103) as varchar) as [Created Date] ,TSPL_PO_BULK_MASTER.Modify_By as [Modify By] ,cast(convert(date,TSPL_PO_BULK_MASTER.Modify_Date,103)as varchar) as [Modify Date] ,TSPL_PO_BULK_MASTER.comp_code as [Company Code]  From TSPL_PO_BULK_MASTER "
            str = clsCommon.ShowSelectForm("POBulk", qry, "PONo", whrcls, curcode, "PONo", isButtonClicked)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function



    Public Shared Function getUsersDefaultLocation(Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim strLoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location  from TSPL_USER_MASTER   where user_code='" & objCommonVar.CurrentUserCode & "'", trans))
        Return strLoc
    End Function
    Public Shared Function postData(ByVal strGateEntryNo As String, ByVal docType As String, ByVal formId As String) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(strGateEntryNo) <= 0) Then
                Throw New Exception("PO  No not found to Post")
            End If

            Dim obj As clsPOBulkProc = clsPOBulkProc.getData(strGateEntryNo, docType, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.PO_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            trans = clsDBFuncationality.GetTransactin()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Gate Entry", "Gate Entry", obj.location_Code, obj.Date_And_Time, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            '--------------------
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(formId, "TSPL_PO_BULK_MASTER", "PO_No", obj.PO_No, trans)


            If isResult = False Then
                trans.Commit()
                Return False
            End If
            Dim strQry As String = " update TSPL_PO_BULK_MASTER set isPosted='1' where PO_No='" & strGateEntryNo & "'"
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            CreateSMSEmailContent(clsUserMgtCode.frmGateEntry, obj, trans)
            If isPosted Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
            Return isPosted
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Shared Sub CreateSMSEmailContent(ByVal Form_ID As String, ByVal obj As clsPOBulkProc, ByVal trans As SqlTransaction)
        '' create sms content
        Dim dtSMSEmail As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,EMail_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmPOBulkProc + "'", trans)
        Dim strSMSContent As String = ""
        Dim strEMailContent As String = ""
        If dtSMSEmail.Rows.Count > 0 Then
            strSMSContent = clsCommon.myCstr(dtSMSEmail.Rows(0).Item("SMS_Text"))
            strEMailContent = clsCommon.myCstr(dtSMSEmail.Rows(0).Item("EMail_Text"))
        End If

        'SMSCode Start
        If clsCommon.myLen(strSMSContent) > 0 Then
            Dim objSMSH As New clsSMSHead()
            objSMSH.SMS_Text = strSMSContent
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MMM/yyyy"))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(obj.PO_No))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Vendor_Code, clsCommon.myCstr(obj.Vendor_Code))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Vendor_Name, clsCommon.myCstr(obj.Vendor_Desc))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Code, clsCommon.myCstr(obj.location_Code))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Name, clsCommon.myCstr(obj.Location_Desc))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.PriceCode, clsCommon.myCstr(obj.Price_Code))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Rate, clsCommon.myCstr(obj.Rate))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.TotalQty, clsCommon.myCstr(obj.Qty))
            CreateSMSContent(objSMSH.SMS_Text, trans)
            'obj1.SMS_Content = objSMSH.SMS_Text
        End If

        'email content Start
        If clsCommon.myLen(strEMailContent) > 0 Then
            Dim objEmailH As New clsEMailHead
            objEmailH.Email_Text = strEMailContent
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MMM/yyyy"))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(obj.PO_No))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Vendor_Code, clsCommon.myCstr(obj.Vendor_Code))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Vendor_Name, clsCommon.myCstr(obj.Vendor_Desc))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Code, clsCommon.myCstr(obj.location_Code))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Name, clsCommon.myCstr(obj.Location_Desc))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.PriceCode, clsCommon.myCstr(obj.Price_Code))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.TotalQty, clsCommon.myCstr(obj.Qty))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Rate, clsCommon.myCstr(obj.Rate))
            CreateEmailContent(objEmailH.Email_Text, trans)
        End If

    End Sub
    Public Shared Sub CreateSMSContent(ByVal strSMSContent As String, ByVal trans As SqlTransaction)
        If clsCommon.myLen(strSMSContent) > 0 Then
            Dim objSMSH As New clsSMSHead()
            objSMSH.SMS_Text = strSMSContent
            objSMSH.arrMobilNo = New List(Of String)()
            objSMSH.SaveData(clsUserMgtCode.frmGateEntry, objSMSH, trans)
            objSMSH = Nothing
        End If
    End Sub
    Public Shared Sub CreateEmailContent(ByVal strEmailContent As String, ByVal trans As SqlTransaction)
        'MailCode Start
        If clsCommon.myLen(strEmailContent) > 0 Then
            Dim qry As String = "SELECT EMail_Subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmGateEntry + "'"
            Dim EmailSubject As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT EMail_Subject from TSPL_ES_Content where Form_ID='" & clsUserMgtCode.frmGateEntry & "'", trans))
            Dim objSMSH As New clsEMailHead()
            objSMSH.Email_Text = strEmailContent
            objSMSH.Email_Subject = EmailSubject
            objSMSH.arrEMail = New List(Of String)()
            objSMSH.SaveData(clsUserMgtCode.frmGateEntry, objSMSH, trans)
            objSMSH = Nothing
        End If
    End Sub
    '==================Added by preeti 
    Public Shared Function deleteData(ByVal strGateEntryNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            deleteData(strGateEntryNo, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try

    End Function
    Public Shared Function deleteData(ByVal strGateEntryNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_PO_BULK_DETAIL where  PO_No='" & strGateEntryNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PO_BULK_MASTER where  PO_No='" & strGateEntryNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            Return isDeleted
        Catch ex As Exception

            Return False
        End Try
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsPOBulkProc
        Dim obj As New clsPOBulkProc
        Try
            Dim qst As String = " select *   From TSPL_PO_BULK_MASTER   where 1=1  "
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    qst = qst & " and location_code in (" & objCommonVar.strCurrUserLocations & ") "
                End If
            End If
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_PO_BULK_MASTER.PO_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_PO_BULK_MASTER.PO_No in (select min(PO_No ) from TSPL_PO_BULK_MASTER where PO_No  >'" + strCode + "' and  location_code in (" & objCommonVar.strCurrUserLocations & ") )"
                Case NavigatorType.First
                    qst += " and TSPL_PO_BULK_MASTER.PO_No in (select MIN(PO_No ) from TSPL_PO_BULK_MASTER  where  location_code in (" & objCommonVar.strCurrUserLocations & "))"
                Case NavigatorType.Last
                    qst += " and TSPL_PO_BULK_MASTER.PO_No in (select Max(PO_No ) from TSPL_PO_BULK_MASTER where  location_code in (" & objCommonVar.strCurrUserLocations & "))"
                Case NavigatorType.Previous
                    qst += " and TSPL_PO_BULK_MASTER.PO_No in (select Max(PO_No ) from TSPL_PO_BULK_MASTER where PO_No  <'" + strCode + "' ) and location_code in (" & objCommonVar.strCurrUserLocations & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.PO_No = clsCommon.myCstr(dt.Rows(0)("PO_No"))
                obj.Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Date_And_Time"))
                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                obj.location_Code = clsCommon.myCstr(dt.Rows(0)("location_Code"))
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
                obj.Rate = clsCommon.myCdbl(dt.Rows(0)("Rate"))
                obj.Qty = clsCommon.myCdbl(dt.Rows(0)("Qty"))
                obj.MIKL_TYPE_CODE = clsCommon.myCstr(dt.Rows(0)("MIKL_TYPE_CODE"))
                obj.Gate_Entry_Type = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_Type"))
                obj.Arr = clsPOBulkProcDetails.GetData(obj.PO_No, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal docType As String, ByVal navtype As NavigatorType) As clsPOBulkProc
        Dim obj As New clsPOBulkProc
        Try
            Dim qst As String = " select *   From TSPL_PO_BULK_MASTER where 1=1 "
            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO() AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " and location_code in (" & objCommonVar.strCurrUserLocations & ") "
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_PO_BULK_MASTER.PO_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_PO_BULK_MASTER.PO_No in (select min(PO_No ) from TSPL_PO_BULK_MASTER where PO_No  >'" + strCode + "' and doc_type='" & docType & "' " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_PO_BULK_MASTER.PO_No in (select MIN(PO_No ) from TSPL_PO_BULK_MASTER where 1=1 " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_PO_BULK_MASTER.PO_No in (select Max(PO_No ) from TSPL_PO_BULK_MASTER where 1=1  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_PO_BULK_MASTER.PO_No in (select Max(PO_No ) from TSPL_PO_BULK_MASTER where PO_No  <'" + strCode + "' " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.PO_No = clsCommon.myCstr(dt.Rows(0)("PO_No"))
                obj.Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Date_And_Time"))
                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                obj.location_Code = clsCommon.myCstr(dt.Rows(0)("location_Code"))
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
                obj.Rate = clsCommon.myCdbl(dt.Rows(0)("Rate"))
                obj.Qty = clsCommon.myCdbl(dt.Rows(0)("Qty"))
                obj.MIKL_TYPE_CODE = clsCommon.myCstr(dt.Rows(0)("MIKL_TYPE_CODE"))
                obj.Gate_Entry_Type = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_Type"))
                obj.Arr = clsPOBulkProcDetails.GetData(obj.PO_No, Nothing)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal docType As String, ByVal navtype As NavigatorType, ByVal isUserCheck As Boolean) As clsPOBulkProc
        Dim obj As clsPOBulkProc = New clsPOBulkProc()
        If isUserCheck Then
            Return getData(strCode, docType, navtype)
        Else
            Try
                Dim qst As String = " select *   From TSPL_PO_BULK_MASTER   where 1=1  "
                Select Case navtype
                    Case NavigatorType.Current
                        qst += " and TSPL_PO_BULK_MASTER.PO_No in ('" + strCode + "') "
                    Case NavigatorType.Next
                        qst += " and TSPL_PO_BULK_MASTER.PO_No in (select min(PO_No ) from TSPL_PO_BULK_MASTER where PO_No  >'" + strCode + "' and doc_type='" & docType & "' )"
                    Case NavigatorType.First
                        qst += " and TSPL_PO_BULK_MASTER.PO_No in (select MIN(PO_No ) from TSPL_PO_BULK_MASTER where 1=1 )"
                    Case NavigatorType.Last
                        qst += " and TSPL_PO_BULK_MASTER.PO_No in (select Max(PO_No ) from TSPL_PO_BULK_MASTER where 1=1 )"
                    Case NavigatorType.Previous
                        qst += " and TSPL_PO_BULK_MASTER.PO_No in (select Max(PO_No ) from TSPL_PO_BULK_MASTER where PO_No  <'" + strCode + "'  )"
                End Select
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    obj.PO_No = clsCommon.myCstr(dt.Rows(0)("PO_No"))
                    obj.Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Date_And_Time"))
                    obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                    obj.location_Code = clsCommon.myCstr(dt.Rows(0)("location_Code"))
                    obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                    obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
                    obj.Rate = clsCommon.myCdbl(dt.Rows(0)("Rate"))
                    obj.Qty = clsCommon.myCdbl(dt.Rows(0)("Qty"))
                    obj.MIKL_TYPE_CODE = clsCommon.myCstr(dt.Rows(0)("MIKL_TYPE_CODE"))
                    obj.Gate_Entry_Type = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_Type"))
                    obj.Arr = clsPOBulkProcDetails.GetData(obj.PO_No, Nothing)
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End If
        Return obj
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strDocNo) <= 0 Then
                Throw New Exception("Please select a PO  No")
            End If
            Dim Qry As String = "select isPosted from TSPL_PO_BULK_MASTER where PO_No='" + strDocNo + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            Dim isUsed As Integer = clsDBFuncationality.getSingleValue("select SUM(row_Count ) from (select COUNT(*) as row_Count from  TSPL_Weighment_Detail where PO_No='" & strDocNo & "' union all select COUNT(*) as row_Count from tspl_quality_check where PO_No='" & strDocNo & "') xx ", trans)
            If isUsed > 0 Then
                Throw New Exception("PO  No is in use")
            End If
            Qry = "Update TSPL_PO_BULK_MASTER set isPosted = 0,Posting_Date=null where PO_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class



Public Class clsPOBulkProcDetails
#Region "Variables"
    Public PO_No As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public UOM As String = Nothing
    Public Qty As Integer = 0
    Public ManualRate As Double = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsPOBulkProcDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sQuery As String = "Delete from TSPL_PO_BULK_DETAIL where PO_No='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            For Each obj As clsPOBulkProcDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "PO_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "ManualRate", obj.ManualRate)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PO_BULK_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsPOBulkProcDetails)
        Dim arr As List(Of clsPOBulkProcDetails) = Nothing
        Dim qry As String
        qry = "select * from " & _
            "TSPL_PO_BULK_DETAIL where TSPL_PO_BULK_DETAIL.PO_No='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsPOBulkProcDetails)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsPOBulkProcDetails = New clsPOBulkProcDetails()
                obj.PO_No = clsCommon.myCstr(dr("PO_No"))
                obj.Line_No = clsCommon.myCstr(dr("Line_No"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.UOM = clsCommon.myCstr(dr("UOM"))
                obj.Qty = clsCommon.myCdbl(dr("Qty"))
                obj.ManualRate = clsCommon.myCdbl(dr("ManualRate"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

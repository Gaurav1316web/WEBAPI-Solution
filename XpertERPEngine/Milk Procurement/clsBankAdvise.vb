Imports common
Imports System.Data.SqlClient
Public Class clsBankAdvise
    Public Document_No As String = ""
    Public Document_Date As Date
    Public Payment_Process_Document_No As String = ""
    Public Created_By As String = ""
    Public Created_Date As Date
    Public Modified_By As String = ""
    Public Modified_Date As Date
    Public Remarks As String = ""
    Public Status As Integer


    Public Shared Function SaveData(ByVal obj As clsBankAdvise, ByVal isNewEntry As Boolean) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim dt As Date = clsCommon.myCDate(clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(Nothing, dt, clsDocType.BankAdvise, "", "", False)
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Payment_Process_Document_No", clsCommon.myCstr(obj.Payment_Process_Document_No))
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_ADVISE", OMInsertOrUpdate.Insert, "", Nothing)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_ADVISE", OMInsertOrUpdate.Update, "Document_No= '" + obj.Document_No + "'", Nothing)
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_BANK_ADVISE", "Document_No", "TSPL_BANK_ADVISE", "Document_No", Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function

    Public Shared Function GetBankAdviseData(ByVal strCode As String, ByVal navtype As NavigatorType) As clsBankAdvise
        Dim Qry As String = ""
        Dim obj As clsBankAdvise = Nothing
        Try
            Dim whrCls As String = String.Empty
            Qry = "Select * from TSPL_BANK_ADVISE where 1=1"
            Select Case navtype
                Case NavigatorType.Current
                    Qry += " and TSPL_BANK_ADVISE.Document_No in ('" + strCode + "')"
                Case NavigatorType.Next
                    Qry += " and TSPL_BANK_ADVISE.Document_No in (select min(Document_No ) from TSPL_BANK_ADVISE where Document_No  >'" + strCode + "' " & whrCls & ") "
                Case NavigatorType.First
                    Qry += " and TSPL_BANK_ADVISE.Document_No in (select MIN(Document_No ) from TSPL_BANK_ADVISE where 1=1 " & whrCls & ") "
                Case NavigatorType.Last
                    Qry += " and TSPL_BANK_ADVISE.Document_No in (select Max(Document_No ) from TSPL_BANK_ADVISE where 1=1 " & whrCls & ") "
                Case NavigatorType.Previous
                    Qry += " and TSPL_BANK_ADVISE.Document_No in (select Max(Document_No ) from TSPL_BANK_ADVISE where Document_No  <'" + strCode + "' " & whrCls & ") "
            End Select

            If navtype = 0 AndAlso clsCommon.myLen(strCode) > 0 Then
                Qry += " and TSPL_BANK_ADVISE.Document_No in ('" + strCode + "')"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsBankAdvise
                obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                obj.Document_Date = clsCommon.GetPrintDate(dt.Rows(0)("Document_Date"))
                obj.Payment_Process_Document_No = clsCommon.myCstr(clsCommon.myCstr(dt.Rows(0)("Payment_Process_Document_No")))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                If clsCommon.myCDecimal(dt.Rows(0)("Status")) > 0 Then
                    obj.Status = 1
                Else
                    obj.Status = 0
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function deleteData(ByVal strCode As String) As Boolean
        Try
            Dim Qry As String = "delete from TSPL_BANK_ADVISE where  Document_No='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function paymentProcessDetails(ByVal PPDocNo As String) As String
        Dim Qry As String = "Select TSPL_PAYMENT_PROCESS_HEAD.Doc_No As  [Document Code],TSPL_PAYMENT_PROCESS_HEAD.From_Date As [From Date],TSPL_PAYMENT_PROCESS_HEAD.To_Date As [To Date],TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader As [DCS Code],TSPL_PAYMENT_PROCESS_DETAIL.MCC_Code As [MCC Code],TSPL_LOCATION_MASTER.Location_Desc As [Area] from TSPL_PAYMENT_PROCESS_DETAIL
                                Left Outer Join TSPL_PAYMENT_PROCESS_HEAD On TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                left outer join TSPL_LOCATION_MASTER  On TSPL_LOCATION_MASTER.Location_Code=TSPL_PAYMENT_PROCESS_DETAIL.MCC_Code "
        If clsCommon.myLen(PPDocNo) > 0 Then
            Qry += " Where TSPL_PAYMENT_PROCESS_HEAD.FarmType='PP' And TSPL_PAYMENT_PROCESS_HEAD.Doc_No='" + PPDocNo + "'"
        End If
        Return Qry
    End Function

    Public Shared Function postData(ByVal strCode As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select Status,TSPL_PAYMENT_PROCESS_HEAD.Doc_No, TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date 
from TSPL_BANK_ADVISE
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_BANK_ADVISE.Payment_Process_Document_No
where Document_No='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, tran)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Invalid document No [" + strCode + "]")
            End If
            If clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1 Then
                Throw New Exception("Already posted document No [" + strCode + "]")
            End If
            Dim strDaterange As String = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("From_Date")), "dd") + " - " + clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("To_Date")), "dd MMM yyyy")
            CreateEmailContent(clsCommon.myCstr(dt.Rows(0)("Doc_No")), strDaterange, tran)

            Qry = "Update TSPL_BANK_ADVISE Set Status=1 where  Document_No='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, tran)
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function




    Public Shared Sub CreateEmailContent(ByVal strPPNo As String, ByVal strDateRange As String, trans As SqlTransaction)
        Dim Form_ID As String = clsUserMgtCode.frmBankAdvise
        Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + Form_ID + "'", trans)
        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 Then
                Dim qry As String = "select  TSPL_VENDOR_MASTER.Company_Bank_Current,max(TSPL_BANK_MASTER.DESCRIPTION) as Bank_Name,max(TSPL_BANK_MASTER.Email) as Email
 from TSPL_PAYMENT_PROCESS_DETAIL
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_VENDOR_MASTER.Company_Bank_Current
where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strPPNo + "'  and (isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0))>0 group by TSPL_VENDOR_MASTER.Company_Bank_Current"
                Dim dtBank As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtBank IsNot Nothing AndAlso dtBank.Rows.Count > 0 Then
                    For Each drBank As DataRow In dtBank.Rows
                        ''Note IF You do any changes than change in function frmVendorBankAdvice.Print(ByVal isPrint As Boolean) 
                        If clsCommon.myLen(drBank("Email")) <= 0 Then
                            Throw New Exception("Please Define email ID for bank [" + clsCommon.myCstr(drBank("Company_Bank_Current")) + "]")
                        End If

                        Dim objSMSH As New clsEMailHead()

                        objSMSH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                        objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(XpertERPEngine.frmEMailAndSMSSetting.DateRange, strDateRange)
                        objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(XpertERPEngine.frmEMailAndSMSSetting.Bank, clsCommon.myCstr(drBank("Bank_Name")))

                        objSMSH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                        objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.DateRange, strDateRange)
                        objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Bank, clsCommon.myCstr(drBank("Bank_Name")))

                        Dim MultipleFinderFillAuto As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MultipleFinderFillAuto, clsFixedParameterCode.MultipleFinderFillAuto, trans)) = 1)
                        Dim AreaWiseBilling As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AreaWiseBilling, clsFixedParameterCode.AreaWiseBilling, trans)) = 1)
                        Dim VendorBankAdviceForSWM As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VendorBankAdviceForSWM, clsFixedParameterCode.VendorBankAdviceForSWM, trans)) = 1)

                        Dim BaseQry As String = "select  '" + strDateRange + "' AS CycleRange,"
                        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                            BaseQry += " TSPL_Vendor_MASTER.Bank_Code+TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code as GRPColumn,"
                        Else
                            BaseQry += " TSPL_Vendor_MASTER.Bank_Code as GRPColumn,"
                        End If
                        BaseQry += " CASE WHEN TSPL_Vendor_MASTER.Bank_Code LIKE 'PNB%' THEN 'PNB Bank' ELSE 'Other Banks' END AS GRPColumns
,TSPL_COMPANY_MASTER.Bank_Name,TSPL_COMPANY_MASTER.BankAccountNo,TSPL_COMPANY_MASTER.BankIFSCCode,TSPL_COMPANY_MASTER.BankBranchAddress,
TSPL_BANK_MASTER.DESCRIPTION as [Company Bank], TSPL_BANK_MASTER.BANKACCNUMBER as [Company Bank Account No],TSPL_COMPANY_MASTER.Comp_Name
,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end as Comp_address
,case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone ,TSPL_COMPANY_MASTER.Regn_No,TSPL_MCC_MASTER.MCC_NAME 
,TSPL_PAYMENT_PROCESS_HEAD.From_Date,'GSTIN : '+ TSPL_COMPANY_MASTER.GSTReg_No as GSTReg_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_No," + IIf(MultipleFinderFillAuto = True, "", " TSPL_Location_MASTER.Location_Code,TSPL_Location_MASTER.Location_Desc, ") + " TSPL_Fiscal_Year_Master.Fiscal_Name
,TSPL_PAYMENT_CYCLE_GENERATED.Name as CycleNo ,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Name,TSPL_Vendor_MASTER.Bank_Code,TSPL_VENDOR_MASTER.Branch_Name,case when isnull(TSPL_Vendor_MASTER.Bank_Name,'')  = '' then  TSPL_Vendor_MASTER.Bank_Code else TSPL_Vendor_MASTER.Bank_Name end as Bank_Code_Desc,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Account_No,"
                        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                            BaseQry += " Round((isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0)),0) as Payable_Amount "
                        Else
                            If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.RoundOffBankAdvice, clsFixedParameterCode.RoundOffBankAdvice, trans)) = "1" Then
                                BaseQry += " Round((isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0)-isnull(TSPL_TRANSFER_TO_SAVING_DETAIL.Amount,0)),0) as Payable_Amount  "
                            Else
                                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "UDP") = CompairStringResult.Equal Then
                                    BaseQry += " (isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_TRANSFER_TO_SAVING_DETAIL.Amount,0))  as Payable_Amount  "

                                Else
                                    BaseQry += " (isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0)-isnull(TSPL_TRANSFER_TO_SAVING_DETAIL.Amount,0))  as Payable_Amount  "
                                End If
                            End If
                        End If
                        BaseQry += ",Case When TSPL_BANK_ADVISE.Status IS NULL OR TSPL_BANK_ADVISE.Status =0 Then 'Pending' Else 'Approved' End As [Bank Advice Status]  
from TSPL_PAYMENT_PROCESS_DETAIL 
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'
left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Start_Date<=TSPL_PAYMENT_PROCESS_HEAD.From_Date and TSPL_Fiscal_Year_Master.End_Date>=TSPL_PAYMENT_PROCESS_HEAD.From_Date
left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Company_Bank_Current "
                        If AreaWiseBilling = True Then
                            BaseQry += " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_PAYMENT_PROCESS_HEAD.Area_Location_Code"
                        Else
                            BaseQry += " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected"
                        End If
                        BaseQry += " left outer join TSPL_TRANSFER_TO_SAVING_DETAIL  on TSPL_PAYMENT_PROCESS_DETAIL.VSP_Code = TSPL_TRANSFER_TO_SAVING_DETAIL.Vendor_Code 
left outer join TSPL_BANK_ADVISE On TSPL_BANK_ADVISE.Payment_Process_Document_No=TSPL_PAYMENT_PROCESS_HEAD.Doc_No  
" + IIf(MultipleFinderFillAuto = True, "    ", " left outer join TSPL_Location_MASTER on TSPL_Location_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code and  TSPL_Location_MASTER.Rejected_Type='N' and TSPL_Location_MASTER.Location_Category='MCC' ") + "
left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert(date, TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103)<=convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103)>=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) " + IIf(MultipleFinderFillAuto = True, "  and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected  ", " and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code=TSPL_Location_MASTER.Location_Code ") + " 
where TSPL_PAYMENT_PROCESS_HEAD.isPrePosted = 1 and TSPL_BANK_MASTER.BANK_CODE='" + clsCommon.myCstr(drBank("Company_Bank_Current")) + "' 
and   TSPL_PAYMENT_PROCESS_HEAD.Doc_No='" + strPPNo + "'  "

                        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "CHT") <> CompairStringResult.Equal Then
                            BaseQry += "And (isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0))>0"
                        End If

                        'And (isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0))>0 "

                        Dim FinalQuery As String = ""
                        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                            FinalQuery = BaseQry + " order by Payee_Joint_Account_No asc"
                        Else
                            FinalQuery = BaseQry + " order by TSPL_Vendor_MASTER.Bank_Code,cast(VLC_CODE_Uploader as Int) "
                        End If
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(FinalQuery, trans)

                        Dim frmCRViewer As New frmCrystalReportViewer()
                        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                            objSMSH.Attachment_1_Path = frmCRViewer.EmailAttachment(CrystalReportFolder.MilkProcurement, dt, "crptBankAdvice", "Bank Advice")
                        ElseIf clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                            objSMSH.Attachment_1_Path = frmCRViewer.EmailAttachment(CrystalReportFolder.MilkProcurement, dt, "crptBankAdviceNewJPR", "Bank Advice")
                        ElseIf clsCommon.CompairString(objCommonVar.CurrComp_Code1, "UDP") = CompairStringResult.Equal AndAlso VendorBankAdviceForSWM = True Then
                            objSMSH.Attachment_1_Path = frmCRViewer.EmailAttachment(CrystalReportFolder.MilkProcurement, dt, "crptBankAdviceNewSWM", "Bank Advice")
                        Else
                            objSMSH.Attachment_1_Path = frmCRViewer.EmailAttachment(CrystalReportFolder.MilkProcurement, dt, "crptBankAdviceNew", "Bank Advice")
                        End If
                        objSMSH.arrEMail = New List(Of String)()
                        objSMSH.SaveData(Form_ID, objSMSH, trans)
                        objSMSH = Nothing
                        frmCRViewer = Nothing
                    Next
                End If

            End If
        End If
    End Sub
End Class
